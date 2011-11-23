//********************************************************************************/
//
//此代码由BuildSheetFromExcel代码生成器自动生成.
//生成时间:2011-5-19 21:33:43
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
namespace Itop.Client.TableTemplateNW
{
    class Sheet221
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
            //表格共8 行6 列
            rowcount = 8;
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
            obj_sheet.Columns[0].Width = 60;
            obj_sheet.Columns[1].Width = 100;
            obj_sheet.Columns[2].Width = 100;
            obj_sheet.Columns[3].Width = 80;
            obj_sheet.Columns[4].Width = 100;
            obj_sheet.Columns[5].Width = 80;
            //设定表格行高度
            obj_sheet.Rows[0].Height = 20;
            obj_sheet.Rows[1].Height = 20;
            obj_sheet.Rows[2].Height = 20;
            //写标题行内容

            //2行标题内容
            obj_sheet.AddSpanCell(1, 0, 2, 1);
            obj_sheet.AddSpanCell(1, 1, 2, 1);
            obj_sheet.SetValue(1, 2, "供电量");
            obj_sheet.AddSpanCell(1, 3, 2, 1);
            obj_sheet.SetValue(1, 3, "增长率（%）");
            obj_sheet.SetValue(1, 4, "供电负荷");
            obj_sheet.AddSpanCell(1, 5, 2, 1);
            obj_sheet.SetValue(1, 5, "增长率（%）");

            //3行标题内容
            obj_sheet.SetValue(2, 2, "（亿千瓦时）");
            obj_sheet.SetValue(2, 4, "（万千瓦）");
            //写标题列内容

            //1列标题内容
            obj_sheet.SetValue(3, 0, "1");
           

            //2列标题内容
            obj_sheet.SetValue(3, 1, "全市");
          
            //添加数据
            Sheet_AddData(obj_sheet);

            //设定表格线
            TC.Sheet_GridandCenter(obj_sheet);

            //锁定表格
            TC.Sheet_Locked(obj_sheet);
        }


        //此处为动态添加数据方法
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet)
        {

            int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);
            int startrow = 3;
            int itemlength = 9;
            string sqlwhere = " ProjectID='" + Tcommon.ProjectID + "'";
            IList<PS_Table_AreaWH> ptalist = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", sqlwhere);
            //重新设定行数
            obj_sheet.RowCount = startrow + (ptalist.Count + 1);
            for (int i = 0; i < ptalist.Count; i++)
            {
                obj_sheet.SetValue(startrow + 1 + i, 0, i + 2);
                obj_sheet.SetValue(startrow + 1 + i, 1, ptalist[i].Title);
                string sqlwhere2 = " ForecastID='4' and Col4='" + Tcommon.ProjectID + "' and Title='" + ptalist[i].Title + "'";
                IList<Ps_History> phlist = Services.BaseService.GetList<Ps_History>("SelectPs_HistoryBYconnstr", sqlwhere2);
                if (phlist.Count > 0)
                {
                    string yearstr = "y" + TableYearsAry[0].ToString();
                    string peryearstr = "y" + (TableYearsAry[0]-1);
                    string sqlwhere3 = " ForecastID='4' and Col4='" + Tcommon.ProjectID + "' and ParentID='" + phlist[0].ID + "'";
                    IList phlist3 = Services.BaseService.GetList("SelectPs_HistoryBYconnstr", sqlwhere3);
                    DataTable dt = DataConverter.ToDataTable(phlist3, typeof(Ps_History));
                    DataRow[] rows1 = dt.Select("Title like '供电量%'");
                    DataRow[] rows2 = dt.Select("Title like '供电负荷%'");
                    if (rows1.Length != 0)
                    {
                        obj_sheet.SetValue(startrow + 1 + i, 2 , rows1[0][yearstr]);
                        double tempdb = (Convert.ToDouble(rows1[0][yearstr]) - Convert.ToDouble(rows1[0][peryearstr])) / Convert.ToDouble(rows1[0][peryearstr]);
                        if (tempdb.ToString() == "非数字" || tempdb.ToString() == "正无穷大")
                            tempdb = 0;
                        obj_sheet.SetValue(startrow + 1 + i, 3, tempdb);
                        TC.SetSheetCellType(obj_sheet, startrow + 1 + i, 3, 2, 2);
                    }
                    else
                    {
                        TC.WriteQuestion(title, ptalist[i].Title + "无供电量数据", "查询 分区供电实绩，看是否有该区供电量数据", "");
                    
                    }
                    if (rows2.Length != 0)
                    {
                        obj_sheet.SetValue(startrow + 1 + i, 4, rows2[0][yearstr]);
                        double tempdb = (Convert.ToDouble(rows2[0][yearstr]) - Convert.ToDouble(rows2[0][peryearstr])) / Convert.ToDouble(rows2[0][peryearstr]);
                        if (tempdb.ToString() == "非数字" || tempdb.ToString() == "正无穷大")
                            tempdb = 0;
                        obj_sheet.SetValue(startrow + 1 + i, 5, tempdb);
                        TC.SetSheetCellType(obj_sheet, startrow + 1 + i, 5, 2, 2);
                    }
                    else
                    {
                        TC.WriteQuestion(title, ptalist[i].Title + "无供电负荷数据", "查询 分区供电实绩，看是否有该区供电负荷数据", "");
                    }

                }
               
            }
            TC.Sheet_WriteFormula_RowSum(obj_sheet, startrow + 1, 2, ptalist.Count, 1, startrow , 2, 1);
            TC.Sheet_WriteFormula_RowSum(obj_sheet, startrow + 1, 4, ptalist.Count, 1, startrow , 4, 1);

        }



    }
}
