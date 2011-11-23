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
    class Sheet22411zhu
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
        public void Build_Sheet(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            //表格共10 行6 列
            rowcount = 10;
            colcount = 6;
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
            obj_sheet.Columns[0].Width = 120;
            obj_sheet.Columns[1].Width = 60;
            obj_sheet.Columns[2].Width = 60;
            obj_sheet.Columns[3].Width = 60;
            obj_sheet.Columns[4].Width = 60;
            obj_sheet.Columns[5].Width = 60;
            //设定表格行高度
            obj_sheet.Rows[0].Height = 20;
            obj_sheet.Rows[1].Height = 20;
            //写标题行内容

            //2行标题内容
            obj_sheet.SetValue(1, 1, "全市");
           
            //写标题列内容

            //1列标题内容
           
            //添加数据
            Sheet_AddData(obj_sheet);

            //设定表格线
            TC.Sheet_GridandCenter(obj_sheet);

            //锁定表格
            TC.Sheet_Locked(obj_sheet);
        }
        private void AddItems(FarPoint.Win.Spread.SheetView obj_sheet, string DianYa, int rowstart)
        {


            obj_sheet.SetValue(rowstart++, 0, DianYa + "千伏变电站数量");
            obj_sheet.SetValue(rowstart++, 0, DianYa + "千伏变电容量");
            obj_sheet.SetValue(rowstart++, 0, DianYa + "千伏线路长度");

           
        }

        //此处为动态添加数据方法
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);
            int startrow = 2;
            int itemlength = 1;
            string sqlwhere = " ProjectID='" + Tcommon.ProjectID + "'";
            IList<PS_Table_AreaWH> ptalist = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", sqlwhere);
            //重新设定列数
            colcount = 2 + ptalist.Count  * itemlength;
            //查询变电站电压有多少等级条件(与线路等级相同)
            string tiaojian = " AreaID='" + Tcommon.ProjectID + "'  group by L1 order by L1 desc";
            //记录电压等级
            IList ptz = Services.BaseService.GetList("SelectPSP_Substation_InfoGroupL1", tiaojian);
            //重新设定行数
            rowcount = 2 + ptz.Count * 4;
            TC.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            string strgz = "公用";
            string BDtiaojian = "";
            string XLtiaojian = "";
            string dianya;
            //写入行分区
            for (int k= 0; k < ptalist.Count; k++)
            {
                obj_sheet.SetValue(1, 2 + k, ptalist[k].Title);
            }
            for (int i = 0; i < ptz.Count; i++)
            {
                dianya = ptz[i].ToString();
                //写列标题
                obj_sheet.SetValue(startrow + i * 4, 0, TC.CHNumberToChar(i + 1) + "、" + ptz[i] + "千伏");
                AddItems(obj_sheet, ptz[i].ToString(), startrow + 1 + i * 4);
                TC.Sheet_WriteFormula_ColSum_WritOne(obj_sheet, startrow + i * 4+1, 2, 3, ptalist.Count, 1);

                for (int j = 0; j < ptalist.Count; j++)
                {
                    BDtiaojian = " S2!='' and CAST(substring(S2,1,4) as int)<=" + TableYearsAry[0] + " and AreaID='" + Tcommon.ProjectID + "' and S4='" + strgz + "' and L1=" + dianya + " and AreaName='" + ptalist[j].Title + "'";
                    XLtiaojian = " year(cast(OperationYear as datetime))<=" + TableYearsAry[0] + " and  Type='05' and ProjectID='" + Tcommon.ProjectID + "' and LineType2='" + strgz + "'and RateVolt=" + dianya+" and AreaID='"+ ptalist[j].ID+"'";
                    //变电站台数
                    int BDsum = 0;
                    if (Services.BaseService.GetObject("SelectPSP_Substation_InfoCountall", BDtiaojian) != null)
                    {
                        BDsum = (int)Services.BaseService.GetObject("SelectPSP_Substation_InfoCountall", BDtiaojian);
                    }
                    obj_sheet.SetValue(startrow + i * 4 + 1, 2 + j, BDsum);
                    //变电容量（MVA）
                    double BDRLsum = 0;
                    if (Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", BDtiaojian) != null)
                    {
                        BDRLsum = (double)Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", BDtiaojian);
                    }
                    obj_sheet.SetValue(startrow + i * 4 + 2, 2 + j, BDRLsum);  
                    //线路长度
                     double XLlength = 0;
                     if (Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", XLtiaojian) != null)
                    {
                        XLlength = (double)Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", XLtiaojian);
                    }
                    obj_sheet.SetValue(startrow + i * 4 + 3, 2 + j, XLlength);
                }
            }
            
        }



    }
}