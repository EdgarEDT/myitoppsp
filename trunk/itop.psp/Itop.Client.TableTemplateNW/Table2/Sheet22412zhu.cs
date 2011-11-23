//********************************************************************************/
//
//此代码由BuildSheetFromExcel代码生成器自动生成.
//生成时间:2011-5-20 8:26:17
//
//********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Itop.Domain.Table;
using Itop.Client.Common;
using Itop.Domain.Forecast;
using System.Data;
using Itop.Common;
using Itop.Domain.Stutistics;
using Itop.Domain.Stutistic;
namespace Itop.Client.TableTemplateNW
{
    class Sheet22412zhu
    {
        //生成公共类的实体
        Tcommon TC = new Tcommon();
        //当前卷编号
        string ProjectID = Tcommon.ProjectID;
        //工作表行数
        int rowcount = 0;
        //工作表列数据
        int colcount = 0;
        //工作表第一行的表名
        string title = "";
        //工作表标签名
        string sheetname = "";
        public  void Build_Sheet(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            //表格共9 行10 列
            rowcount = 9;
            colcount = 10;
            //工作表第一行的标题
            title = TC.GetTableTitle(this.GetType().Name);
            //工作表有关年份
            //int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);
            //工作表名
            sheetname = title;
            //设定工作表行列值及标题和表名
            TC.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            //设定行列模式，以便写公式使用
            TC.Sheet_Referen_R1C1(obj_sheet);
            //设定表格列宽度
            obj_sheet.Columns[0].Width = 60;
            obj_sheet.Columns[1].Width = 120;
           
            //设定表格行高度
            obj_sheet.Rows[0].Height = 20;
            obj_sheet.Rows[1].Height = 20;
            obj_sheet.Rows[2].Height = 20;
            //写标题行内容

            //2行标题内容
            obj_sheet.AddSpanCell(1, 0, 2, 2);
            obj_sheet.AddSpanCell(1, 2, 1, 2);
            obj_sheet.SetValue(1, 2, "全市");
           

            //3行标题内容
            obj_sheet.SetValue(2, 2, "数量");
            obj_sheet.SetValue(2, 3, "比例");
          
            //写标题列内容

            //1列标题内容
         

            //2列标题内容
           
            //添加数据
            Sheet_AddData(obj_sheet);

            //设定表格线
            TC.Sheet_GridandCenter(obj_sheet);

            //锁定表格
            TC.Sheet_Locked(obj_sheet);
        }
   
       private void AddItemsRow(FarPoint.Win.Spread.SheetView obj_sheet, string DianYa, int rowstart)
        {
            obj_sheet.AddSpanCell(rowstart, 0, 3, 1);
            obj_sheet.SetValue(rowstart, 0, DianYa + "千伏");
            obj_sheet.SetValue(rowstart++, 1, "合计");
            obj_sheet.SetValue(rowstart++, 1, "典型接线");
            obj_sheet.SetValue(rowstart++, 1, "非典型接线");
        }
        private void AddItemsCol(FarPoint.Win.Spread.SheetView obj_sheet, string AreaName, int colstart)
        {

            obj_sheet.AddSpanCell(1, colstart, 1, 2);
            obj_sheet.SetValue(1, colstart, AreaName);
            obj_sheet.SetValue(2, colstart, "数量");
            obj_sheet.SetValue(2, colstart+1, "比例");
        }
         string JXFSDX=" and ( JXFS='单环网型' or JXFS='双环网型' or JXFS='两供一备' or JXFS='三供一备' or JXFS='多分段两联络' or JXFS='多分段三联络' )";
        string JXFSFDX = " and ( JXFS='辐射型' or JXFS='其他非典型接线')";
        //此处为动态添加数据方法
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);
            int startrow = 3;
            int startcol = 2;
            int itemlengthcol = 2;
            int itemlengthrow = 3;
            string sqlwhere = " ProjectID='" + Tcommon.ProjectID + "'";
            IList<PS_Table_AreaWH> ptalist = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", sqlwhere);
            //重新设定列数
            colcount = startcol + (ptalist.Count+1)* itemlengthcol;
            //查询变电站电压有多少等级条件(与线路等级相同)
            string tiaojian = " AreaID='" + Tcommon.ProjectID + "'  group by L1 order by L1 desc";
            //记录电压等级
            IList ptz = Services.BaseService.GetList("SelectPSP_Substation_InfoGroupL1", tiaojian);
            //重新设定行数
            rowcount = startrow + ptz.Count * itemlengthrow;
            TC.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            string strgz = "公用";
            string XLtiaojian = "";
            string dianya;
            int[] intcol=new int[ptalist.Count];
            //写入列分区
            for (int k= 0; k < ptalist.Count; k++)
            {
                AddItemsCol(obj_sheet, ptalist[k].Title,startcol+(k+1)*itemlengthcol);
                intcol[k]=startcol+(k+1)*itemlengthcol;
            }
            FarPoint.Win.Spread.CellType.PercentCellType celltype = new FarPoint.Win.Spread.CellType.PercentCellType();
            celltype.DecimalPlaces = 2;
            for (int i = 0; i < ptz.Count; i++)
            {
                dianya = ptz[i].ToString();
                //写行标题（电压等）
                AddItemsRow(obj_sheet, dianya, startrow + i * itemlengthrow);
                //全市求和
                TC.Sheet_WriteFormula_RowSum(obj_sheet, startrow + i * itemlengthrow + 1, startcol, 2, 1, startrow + i * itemlengthrow, startcol, 1);

                TC.Sheet_WriteFormula_ColSum_WritOne2(obj_sheet,startrow + i * itemlengthrow + 1,intcol,2,startcol);
                //百分比
                obj_sheet.Cells[startrow + i * itemlengthrow + 1, startcol+1 ].Formula = "R" + (startrow + i * itemlengthrow + 1 + 1) + "C" + (startcol +1) + "/" + "R" + (startrow + i * itemlengthrow + 1) + "C" + (startcol + 1);
                obj_sheet.Cells[startrow + i * itemlengthrow + 1, startcol + 1].CellType = celltype;
                obj_sheet.Cells[startrow + i * itemlengthrow + 2, startcol+1 ].Formula = "R" + (startrow + i * itemlengthrow + 2 + 1) + "C" + (startcol + 1) + "/" + "R" + (startrow + i * itemlengthrow + 1) + "C" + (startcol + 1);
                obj_sheet.Cells[startrow + i * itemlengthrow + 2, startcol + 1].CellType = celltype;
                for (int j = 0; j < ptalist.Count; j++)
                {
                    XLtiaojian = " year(cast(OperationYear as datetime))<=" + TableYearsAry[0] + " and  Type='05' and ProjectID='" + Tcommon.ProjectID + "' and LineType2='" + strgz + "'and RateVolt=" + dianya+" and AreaID='"+ ptalist[j].ID+"' "+JXFSDX;
                    //典型接线数
                    int DXnum = 0;
                    if (Services.BaseService.GetObject("SelectPSPDEV_CountAll", XLtiaojian) != null)
                    {
                        DXnum = (int)Services.BaseService.GetObject("SelectPSPDEV_CountAll", XLtiaojian);
                    }
                    obj_sheet.SetValue(startrow + i * itemlengthrow + 1, startcol + (j+1) * itemlengthcol, DXnum);
                    XLtiaojian = " year(cast(OperationYear as datetime))<=" + TableYearsAry[0] + " and  Type='05' and ProjectID='" + Tcommon.ProjectID + "' and LineType2='" + strgz + "'and RateVolt=" + dianya + " and AreaID='" + ptalist[j].ID + "' " + JXFSFDX;
                    //非典型接线数
                    int FDXnum = 0;
                    if (Services.BaseService.GetObject("SelectPSPDEV_CountAll", XLtiaojian) != null)
                    {
                        FDXnum = (int)Services.BaseService.GetObject("SelectPSPDEV_CountAll", XLtiaojian);
                    }
                    obj_sheet.SetValue(startrow + i * itemlengthrow + 2, startcol + (j+1) * itemlengthcol, FDXnum);
                    //分区求和
                    TC.Sheet_WriteFormula_RowSum(obj_sheet, startrow + i * itemlengthrow + 1, startcol + (j+1) * itemlengthcol, 2, 1, startrow + i * itemlengthrow, startcol + (j+1) * itemlengthcol, 1);
                    //百分比
                    obj_sheet.Cells[startrow + i * itemlengthrow + 1, startcol + (j+1) * itemlengthcol + 1].Formula = "R" + (startrow + i * itemlengthrow + 1 + 1) + "C" + (startcol + (j+1)* itemlengthcol + 1) + "/" + "R" + (startrow + i * itemlengthrow + 1) + "C" + (startcol + (j+1)* itemlengthcol + 1);
                    obj_sheet.Cells[startrow + i * itemlengthrow + 1, startcol + (j + 1) * itemlengthcol + 1].CellType = celltype;
                    obj_sheet.Cells[startrow + i * itemlengthrow + 2, startcol + (j+1) * itemlengthcol + 1].Formula = "R" + (startrow + i * itemlengthrow + 2 + 1) + "C" + (startcol + (j+1)* itemlengthcol + 1) + "/" + "R" + (startrow + i * itemlengthrow + 1) + "C" + (startcol + (j+1) * itemlengthcol + 1);
                    obj_sheet.Cells[startrow + i * itemlengthrow + 2, startcol + (j + 1) * itemlengthcol + 1].CellType = celltype;
                }
            }
            
        }



    }
}