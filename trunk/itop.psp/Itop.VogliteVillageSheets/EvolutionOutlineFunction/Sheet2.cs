using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using Itop.JournalSheet.Function;
using Itop.Domain.Forecast;
using Itop.Client.Common;
//////////////////////////////////////////////////////////////////////////
/*
 *  ClassName：Sheet2
 *  Action：附表2 铜陵县分镇/片区2000～2009年人口状况表的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表 ps_table_Areawh（地区表）,ps_table_AreaData(数据表)
 *              在写入（面积）数据时，按照起始年份中的土地面积写入,如果起始年份中没有土地面积数据就写入0。
 * 年份：2010-10-23
 */
namespace Itop.VogliteVillageSheets.EvolutionOutlineFunction
{
    class Sheet2
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private string projectID = "";
        private IList list = null;
        private IList AreaList = null;

        private Itop.JournalSheet.Function.PublicFunction PF = new Itop.JournalSheet.Function.PublicFunction();
        private Itop.VogliteVillageSheets.Function.PublicFunction m_PF = new Itop.VogliteVillageSheets.Function.PublicFunction();
        private FarPoint.Win.Spread.CellType.PercentCellType PC = new FarPoint.Win.Spread.CellType.PercentCellType();

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet_2Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            SelectDQ(FB);
            int IntColCount = 2;
            int IntRowCount =1+ AreaList.Count + 1 + 2 + 3;//标题占3行，分区类型占2行，1是其它用
            string title = null;

            obj.SheetName = Title;
            obj.Columns.Count = IntColCount;
            obj.Rows.Count = IntRowCount;
            IntCol = obj.Columns.Count;

            PF.Sheet_GridandCenter(obj);//画边线，居中
            m_PF.LockSheets(obj);

            string strTitle = "";
            IntRow = 3;
            strTitle = Title;
            PF.CreateSheetView(obj, IntRow, IntCol, 0, 0, Title);
            PF.SetSheetViewColumnsWidth(obj, 0, Title);
            IntCol = 1;

            strTitle = "单位：万人、平方公里";
            obj.AddSpanCell(IntRow, 0, 1, obj.Columns.Count);
            obj.SetValue(IntRow, 0, strTitle);
            //右对齐
            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

            strTitle = " 镇/片区";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow += 1, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "面积";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge, IntRow , IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, "60");
            for (int i = 0; i < AreaList.Count+1; ++i)
            {
                if(i!=AreaList.Count)
                {
                    PF.CreateSheetView(obj, 1, NextColMerge, (IntRow + i + 2), 0, AreaList[i].ToString());
                    PF.SetSheetViewColumnsWidth(obj, IntCol, AreaList[i].ToString());
                }
                else
                {
                    PF.CreateSheetView(obj, 1, NextColMerge, (IntRow + i + 2), 0, "合计");
                }
            }
     
            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;
            //PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 找出地区，是县级的
        /// </summary>
        private void SelectDQ(Itop.Client.Base.FormBase FB)
        {
            string sql = " ProjectId='" + FB.ProjectUID + "' and col1='县级'"+
                                " and Title  not like '%部地区%'";
            try
            {
                AreaList = Services.BaseService.GetList("SelectPS_Table_AreaWH_Title", sql);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 写入列标题
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="obj"></param>
        /// <param name="BeginYear"></param>
        /// <param name="EndYear"></param>
        public void SetColumnsTitle(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, int BeginYear, int EndYear)
        {
            int intTemp = BeginYear;
            const int coltemp = 2;//写入列的起始点
            Redraw(FB,obj, BeginYear, EndYear);
            for (int i = BeginYear; i <= (EndYear + list.Count); ++i)
            {
                obj.AddSpanCell(4, (i - BeginYear + coltemp), 2, 1);
                //if(i!=EndYear+list.Count)//和下面eles对应
                //{
                obj.SetValue(4, (i - BeginYear + coltemp), intTemp);
                if (i == 2006)
                {
                    obj.SetValue(4, (i - BeginYear + coltemp), "“十五”年均增速");
                    intTemp--;
                }
                if (i == (2011 + 1))//中间空一行所以加一
                {
                    obj.SetValue(4, (i - BeginYear + coltemp), "“十一五”年均增速");
                    intTemp--;
                }
                if (i == (2016 + 2))
                {
                    obj.SetValue(4, (i - BeginYear + coltemp), "“十二五”年均增速");
                    intTemp--;
                }
                if (i == (2021 + 3))
                {
                    obj.SetValue(4, (i - BeginYear + coltemp), "“十三五”年均增速");
                    intTemp--;
                }
                //}
                //else//这是预计的列现在先不用
                //{
                //    if (i != 2006 && i != (2011 + 1) && i != (2016 + 2) && i != (2021 + 3))
                //    {
                //        obj.SetValue(4, (i - BeginYear + 1), intTemp + "预计");
                //    }
                //    else
                //    {
                //        if (i == 2006)
                //        {
                //            obj.SetValue(4, (i - BeginYear + 1), "“十五”年均增速");
                //            intTemp--;
                //        }
                //        if (i == (2011 + 1))//中间空一行所以加一
                //        {
                //            obj.SetValue(4, (i - BeginYear + 1), "“十一五”年均增速");
                //            intTemp--;
                //        }
                //        if (i == (2016 + 2))
                //        {
                //            obj.SetValue(4, (i - BeginYear + 1), "“十二五”年均增速");
                //            intTemp--;
                //        }
                //        if (i == (2021 + 3))
                //        {
                //            obj.SetValue(4, (i - BeginYear + 1), "“十三五”年均增速");
                //            intTemp--;
                //        }
                //    }

                //}
                intTemp++;
            }
            list.Clear();
            WriteData(FB, obj, BeginYear.ToString());
        }
        /// <summary>
        /// 重绘
        /// </summary>
        /// <param name="obj"></param>
        private void Redraw(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, int BeginYear, int EndYear)
        {
            list = AddColumnTitle(BeginYear, EndYear);
            obj.RowCount = 0;
            obj.ColumnCount = 0;
            obj.ColumnCount = 1 + (EndYear - BeginYear + 1) + list.Count+1;

            SelectDQ(FB);
            int IntRowCount = 1+AreaList.Count + 1 + 2 + 3;//标题占3行，分区类型占2行，1是其它用
            string title = null;

            obj.Rows.Count = IntRowCount;
            IntCol = obj.Columns.Count;

            PF.Sheet_GridandCenter(obj);//画边线，居中
            m_PF.LockSheets(obj);

            string strTitle = "";
            IntRow = 3;
            strTitle = "附表2 铜陵县分镇/片区" + BeginYear + "～" + EndYear + "年人口状况表";
            PF.CreateSheetView(obj, IntRow, IntCol, 0, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            IntCol = 1;

            strTitle = "单位：万人、平方公里";
            obj.AddSpanCell(IntRow, 0, 1, obj.Columns.Count);
            obj.SetValue(IntRow, 0, strTitle);
            //右对齐
            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

            strTitle = " 镇/片区";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow += 1, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "面积";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, "60");
            for (int i = 0; i < AreaList.Count+1; ++i)
            {
                if(i!=AreaList.Count)
                {
                    PF.CreateSheetView(obj, 1, NextColMerge, (IntRow + i + 2), 0, AreaList[i].ToString());
                    PF.SetSheetViewColumnsWidth(obj, IntCol, AreaList[i].ToString());
                }
                else
                {
                    PF.CreateSheetView(obj, 1, NextColMerge, (IntRow + i + 2), 0, "合计");
                }
            }

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;
            //PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高

        }
        /// <summary>
        /// 从开始年份到结束年份包含几个十五计划
        /// </summary>
        /// <param name="BeginYear"></param>
        /// <param name="EndYear"></param>
        /// <returns></returns>
        private IList AddColumnTitle(int BeginYear, int EndYear)
        {
            IList list1 = new List<string>();
            if (BeginYear <= 2001 && EndYear >= 2005)
            {
                list1.Add("“十五”年均增速");
            }
            if (BeginYear <= 2006 && EndYear >= 2010)
            {
                list1.Add("“十一五”年均增速");
            }
            if (BeginYear <= 2011 && EndYear >= 2015)
            {
                list1.Add("“十二五”年均增速");
            }
            if (BeginYear <= 2016 && EndYear >= 2020)
            {
                list1.Add("“十三五”年均增速");
            }
            return list1;
        }
        /// <summary>
        /// 找到当前列的数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="CurrentYear">列标题包括（当前年份，土地面积）</param>
        /// <param name="strTitle">指标名称</param>
        private object SelectCurrentData(Itop.Client.Base.FormBase FB, string CurrentYear, string strTitle,string BeginYear)
        {
            string sql = null;
            string sql1 = null;
            string ID = null;
            object value = 0;
            //先找到地区的id
            sql = " ProjectId='" + FB.ProjectUID + "' and Area='" + strTitle + "'";
            try
            {
                ID = (string)Services.BaseService.GetObject("SelectAreaDataOfID", sql);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            if(CurrentYear=="面积")
            {
                sql1 = "select TotalArea from ps_table_AreaData where parentId='" + ID + "' and ProjectId='" + FB.ProjectUID + "' and yearf='" + BeginYear + "'";
            }
            else
            {
                sql1 = "select population from ps_table_AreaData where parentId='" + ID + "' and ProjectId='" + FB.ProjectUID + "' and yearf='" + CurrentYear + "'";
            }
            try
            {
                value = (double)Services.BaseService.GetObject("SelectAreaDataOfCurrent", sql1);
            }
            catch (Exception e)
            {
                //MessageBox.Show("错误，错误原因："+ e.Message,"提示错误",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            return value;
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="obj"></param>
        private void WriteData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj,string BeginYear)
        {
            string strRowTitle = null;
            string strColTitle = null;
            for (int i = 6; i < obj.RowCount; ++i)
            {
                strRowTitle = (string)PF.ReturnStr(obj, i, 0);
                for (int j = 1; j < obj.ColumnCount; ++j)
                {
                    strColTitle = obj.GetValue(4, j).ToString();
                    if (strRowTitle == "合计")
                    {
                        if (strColTitle == "“十五”年均增速" || strColTitle == "“十一五”年均增速" || strColTitle == "“十二五”年均增速" || strColTitle == "“十三五”年均增速")
                        {
                            if (strColTitle == "“十五”年均增速")
                            {
                                obj.Cells[i, j].Formula = "POWER(" + PF.GetColumnTitle(j - 1) + (i + 1) + "/" + PF.GetColumnTitle(j - 6) + (i + 1) + ",1/5)-1";
                            }
                            else
                            {
                                obj.Cells[i, j].Formula = "POWER(" + PF.GetColumnTitle(j - 1) + (i + 1) + "/" + PF.GetColumnTitle(j - 5) + (i + 1) + ",1/5)-1";
                            }
                            obj.Cells[i, j].CellType = PC;//%
                        }
                        else
                        {
                            obj.Cells[i, j].Formula = "Sum(" + PF.GetColumnTitle(j) + (i) + ":" + PF.GetColumnTitle(j) + (i - AreaList.Count+1 ) + ")";
                            //obj.Cells[i, j].Formula = "POWER(" + PF.GetColumnTitle(j) + (i) + "/" + PF.GetColumnTitle(j) + (i - AreaList.Count + 1) + ",1/" + AreaList.Count + ")-1";
                        }
                    }
                    else
                    {
                        if (strColTitle == "“十五”年均增速" || strColTitle == "“十一五”年均增速" || strColTitle == "“十二五”年均增速" || strColTitle == "“十三五”年均增速")
                        {
                            if (strColTitle == "“十五”年均增速")
                            {
                                obj.Cells[i, j].Formula = "POWER(" + PF.GetColumnTitle(j - 1) + (i + 1) + "/" + PF.GetColumnTitle(j - 6) + (i + 1) + ",1/5)-1";
                            }
                            else
                            {
                                obj.Cells[i, j].Formula = "POWER(" + PF.GetColumnTitle(j - 1) + (i + 1) + "/" + PF.GetColumnTitle(j - 5) + (i + 1) + ",1/5)-1";
                            }
                            obj.Cells[i, j].CellType = PC;//%
                        }
                        else
                        {
                            obj.SetValue(i, j, SelectCurrentData(FB, strColTitle, strRowTitle, BeginYear));
                        }
                    }
                }
            }
        }
    }
}
