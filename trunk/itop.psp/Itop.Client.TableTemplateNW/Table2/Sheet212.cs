//********************************************************************************/
//
//此代码由BuildSheetFromExcel代码生成器自动生成.
//生成时间:2011-5-19 9:05:55
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
    class Sheet212
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
            //工作表有关年份
            int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);

            //表格共29 行8 列
            rowcount = 29;
            colcount = 2 + TableYearsAry.Length+1;
            //工作表第一行的标题
            title = TC.GetTableTitle(this.GetType().Name);
           
            //工作表名
            sheetname = title;
            //设定工作表行列值及标题和表名
            TC.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            //设定行列模式，以便写公式使用
            TC.Sheet_Referen_R1C1(obj_sheet);
            //设定表格列宽度
            obj_sheet.Columns[0].Width = 60;
            obj_sheet.Columns[1].Width = 150;
            
            //设定表格行高度
            obj_sheet.Rows[0].Height = 20;
            obj_sheet.Rows[1].Height = 20;
            //写标题行内容

            //2行标题内容
            obj_sheet.SetValue(1, 0, "分区");
            obj_sheet.SetValue(1, 1, "指标名称");
            for (int i = 0; i < TableYearsAry.Length; i++)
            {
                obj_sheet.SetValue(1, 2 + i, TableYearsAry[i]);
                obj_sheet.Columns[2 + i].Width = 60;
               
            }

            obj_sheet.SetValue(1, 2 + TableYearsAry.Length, "年均增长率(%)");
            obj_sheet.Columns[2 + TableYearsAry.Length].Width = 120;
          
            //写标题列内容

            //添加数据
            Sheet_AddData(obj_sheet);

            //设定表格线
            TC.Sheet_GridandCenter(obj_sheet);

            //锁定表格
            TC.Sheet_Locked(obj_sheet);
        }

        private void AddItems(FarPoint.Win.Spread.SheetView obj_sheet,string Area, int rowstart)
        {
            obj_sheet.AddSpanCell(rowstart, 0, 9, 1);
            obj_sheet.SetValue(rowstart, 0, Area);
            obj_sheet.SetValue(rowstart++, 1, "国内生产总值（亿元）");
            obj_sheet.SetValue(rowstart++, 1, "第一产业");
            obj_sheet.SetValue(rowstart++, 1, "第二产业");
            obj_sheet.SetValue(rowstart++, 1, "第三产业");
            obj_sheet.SetValue(rowstart++, 1, "人口（万人）");
            obj_sheet.SetValue(rowstart++, 1, "人均GDP（万元）");
            obj_sheet.SetValue(rowstart++, 1, "行政面积（平方千米）");
            obj_sheet.SetValue(rowstart++, 1, "建成区面积（平方千米）");
            obj_sheet.SetValue(rowstart++, 1, "城镇化率（%）");
        }
        //此处为动态添加数据方法
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);
            int startrow = 2;
            int itemlength = 9;
            string sqlwhere = " ProjectID='" + Tcommon.ProjectID + "'";
            IList<PS_Table_AreaWH> ptalist = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", sqlwhere);
           //重新设定行数
            obj_sheet.RowCount = startrow + (ptalist.Count + 1) * itemlength;


            string sqlwheretemp = " ForecastID='4' and Col4='" + Tcommon.ProjectID + "'";
            IList phlisttemp= Services.BaseService.GetList("SelectPs_HistoryBYconnstr", sqlwheretemp);
            DataTable dttemp = DataConverter.ToDataTable(phlisttemp, typeof(Ps_History));
                    
            Ps_History AllRk = new Ps_History();
            Ps_History CZrk = new Ps_History();
            DataRow RowAllrk = dttemp.NewRow(); ;
            DataRow RowCZrk = dttemp.NewRow();

            RowAllrk = DataConverter.ObjectToRow(AllRk,RowAllrk);
            RowCZrk = DataConverter.ObjectToRow(CZrk, RowCZrk);
            for (int i = 0; i < ptalist.Count; i++)
            {
                AddItems(obj_sheet, ptalist[i].Title, startrow + i * itemlength);
                string sqlwhere2 = " ForecastID='4' and Col4='" + Tcommon.ProjectID + "' and Title='" + ptalist[i].Title + "'";
                IList<Ps_History> phlist = Services.BaseService.GetList<Ps_History>("SelectPs_HistoryBYconnstr", sqlwhere2);
                if (phlist.Count > 0)
                {
                    string sqlwhere3 = " ForecastID='4' and Col4='" + Tcommon.ProjectID + "' and ParentID='" + phlist[0].ID + "'";
                    IList phlist3 = Services.BaseService.GetList("SelectPs_HistoryBYconnstr", sqlwhere3);
                    DataTable dt = DataConverter.ToDataTable(phlist3, typeof(Ps_History));
                    DataRow[] rows1 = dt.Select("Title like '一产%'");
                    DataRow[] rows2 = dt.Select("Title like '二产%'");
                    DataRow[] rows3 = dt.Select("Title like '三产%'");
                    DataRow[] rows4 = dt.Select("Title like '人口%'");
                    
                     DataRow[] rows7=null;
                     DataRow[] rows8 = null;
                    if (rows4.Length!=0)
                    {
                        string sqlwhere4 = " ForecastID='4' and Col4='" + Tcommon.ProjectID + "' and ParentID='" + rows4[0]["ID"] + "'";
                        IList phlist4 = Services.BaseService.GetList("SelectPs_HistoryBYconnstr", sqlwhere4);
                        DataTable dt2 = DataConverter.ToDataTable(phlist4, typeof(Ps_History));
                        rows7 = dt2.Select("Title like '城镇人口%'");
                        rows8 = dt2.Select("Title like '乡村人口%'");
                    }
                   
                    DataRow[] rows5 = dt.Select("Title like '行政面积%'");
                    DataRow[] rows6 = dt.Select("Title like '建成区面积%'");
                    //国内生产总值（亿元）=一产+二产+三产
                    TC.Sheet_WriteFormula_RowSum(obj_sheet, startrow + i * itemlength + 1, 2, 3, 1, startrow + i * itemlength, 2, TableYearsAry.Length);
                    //人均GDP（万元）=国内生产总值（亿元）/人口；
                    TC.Sheet_WriteFormula_OneRow_AnoterRow_nopercent(obj_sheet, startrow + i * itemlength + 4, 2, startrow + i * itemlength, startrow + i * itemlength + 5, TableYearsAry.Length);

                    for (int j = 0; j < TableYearsAry.Length; j++)
                    {
                        int m = 0;
                        //一产
                        string yearstr="y" + TableYearsAry[j].ToString();
                        m++;
                        if (rows1.Length != 0)
                        {
                            obj_sheet.SetValue(startrow + i * itemlength + m, 2 + j, rows1[0][yearstr]);
                        }
                        else
                        {
                            TC.WriteQuestion(title, ptalist[i].Title + "无一产数据", "查询 分区供电实绩，看是否有该区一产数据", "");
                        }
                        //二产
                        m++;
                        if (rows2.Length != 0)
                        {
                            obj_sheet.SetValue(startrow + i * itemlength + m, 2 + j, rows2[0][yearstr]);
                        }
                        else
                        {
                            TC.WriteQuestion(title, ptalist[i].Title + "无二产数据", "查询 分区供电实绩，看是否有该区二产数据", "");
                        }
                        //三产
                        m++;
                        if (rows3.Length != 0)
                        {
                            obj_sheet.SetValue(startrow + i * itemlength + m, 2 + j, rows3[0][yearstr]);
                        }
                        else
                        {
                            TC.WriteQuestion(title, ptalist[i].Title + "无三产数据", "查询 分区供电实绩，看是否有该区三产数据", "");
                        }
                        //人口
                        m++;
                        if (rows4.Length != 0)
                        {
                            obj_sheet.SetValue(startrow + i * itemlength + m, 2 + j, rows4[0][yearstr]);
                          
                        }
                        else
                        {
                            TC.WriteQuestion(title, ptalist[i].Title + "无人口数据", "查询 分区供电实绩，看是否有该区人口数据", "");
                        }
                        m++;
                        //行政面积（平方千米）
                        m++;
                        if (rows5.Length != 0)
                        {
                            obj_sheet.SetValue(startrow + i * itemlength + m, 2 + j, rows5[0][yearstr]);
                        }
                        else
                        {
                            TC.WriteQuestion(title, ptalist[i].Title + "无行政面积数据", "查询 分区供电实绩，看是否有该区行政面积数据", "");
                        }
                        //建成区面积（平方千米）
                        m++;
                        if (rows6.Length != 0)
                        {
                            obj_sheet.SetValue(startrow + i * itemlength + m, 2 + j, rows6[0][yearstr]);
                        }
                        else
                        {
                            TC.WriteQuestion(title, ptalist[i].Title + "无建成区面积数据", "查询 分区供电实绩，看是否有该区建成区面积数据", "");
                        }
                        //城镇化率（%）
                        m++;
                        if (rows7 != null &&rows4.Length!=0)
                        {
                            obj_sheet.SetValue(startrow + i * itemlength + m, 2 + j, Convert.ToDouble(rows7[0][yearstr] )/ Convert.ToDouble(rows4[0][yearstr]));
                            FarPoint.Win.Spread.CellType.PercentCellType  newcelltype =new FarPoint.Win.Spread.CellType.PercentCellType();
                            newcelltype.DecimalPlaces = 2;
                            obj_sheet.Cells[startrow + i * itemlength + m, 2 + j].CellType = newcelltype;
                           
                            RowAllrk[yearstr] = Convert.ToDouble(RowAllrk[yearstr]) + Convert.ToDouble(rows4[0][yearstr]);
                            RowCZrk[yearstr] = Convert.ToDouble(RowCZrk[yearstr]) + Convert.ToDouble(rows7[0][yearstr]);
                          
                        }
                        else
                        {
                            TC.WriteQuestion(title, ptalist[i].Title + "无城镇人口或总人口数据", "查询 分区供电实绩，看是否有该区城镇人口或总人口数据", "");
                        }

                    }

                }
            }


            AddItems(obj_sheet, "全市",  startrow + ptalist.Count * itemlength);
            TC.Sheet_WriteFormula_RowSum2(obj_sheet, startrow, 2, ptalist.Count, itemlength, startrow + ptalist.Count * itemlength, 2, 1, 3, TableYearsAry.Length);
            TC.Sheet_WriteFormula_RowSum2(obj_sheet, startrow, 2, ptalist.Count, itemlength, startrow + ptalist.Count * itemlength, 2, 6, 2, TableYearsAry.Length);
           
            //国内生产总值（亿元）=一产+二产+三产
            TC.Sheet_WriteFormula_RowSum(obj_sheet, startrow + ptalist.Count * itemlength+1, 2, 3, 1, startrow + ptalist.Count * itemlength , 2, TableYearsAry.Length);

            //人均GDP（万元）=国内生产总值（亿元）/人口；
            TC.Sheet_WriteFormula_OneRow_AnoterRow_nopercent(obj_sheet, startrow + ptalist.Count * itemlength + 4, 2, startrow + ptalist.Count * itemlength, startrow + ptalist.Count * itemlength + 5,TableYearsAry.Length);
           //最后城镇化率
            for (int k = 0; k < TableYearsAry.Length; k++)
            {
               
                string yearstr = "y" + TableYearsAry[k].ToString();
                obj_sheet.SetValue(startrow + ptalist.Count * itemlength + 8, 2 + k, Convert.ToDouble(RowCZrk[yearstr]) / Convert.ToDouble(RowAllrk[yearstr]));
                FarPoint.Win.Spread.CellType.PercentCellType newcelltype = new FarPoint.Win.Spread.CellType.PercentCellType();
                newcelltype.DecimalPlaces = 2;
                obj_sheet.Cells[startrow + ptalist.Count * itemlength + 8, 2 + k].CellType = newcelltype;
            }
            //几何年平均增长率
            for (int l = 0; l < (ptalist.Count+1)*itemlength; l++)
            {
                obj_sheet.Cells[startrow + l, 2 + TableYearsAry.Length].Formula = " Power(R" + (startrow + l + 1) + "C" + (2 + TableYearsAry.Length) + "/R" + (startrow + l + 1) + "C" + 3 + "," + (1.000 / TableYearsAry.Length) + ")-1";
                FarPoint.Win.Spread.CellType.PercentCellType newcelltype = new FarPoint.Win.Spread.CellType.PercentCellType();
                newcelltype.DecimalPlaces = 2;
                obj_sheet.Cells[startrow + l, 2 + TableYearsAry.Length].CellType = newcelltype;
            }
                          
        }



    }
}
