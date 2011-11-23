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
 *  ClassName：Sheet1
 *  Action：附表1 XX县2000～2009年国民生产总值GDP情况表的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表 ps_History,
 * 年份：2010-10-20
 */
namespace Itop.VogliteVillageSheets.EvolutionOutlineFunction
{
    class Sheet1
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private string projectID = "";
        private IList list = null;

        private Itop.JournalSheet.Function.PublicFunction PF = new Itop.JournalSheet.Function.PublicFunction();
        private Itop.VogliteVillageSheets.Function.PublicFunction m_PF = new Itop.VogliteVillageSheets.Function.PublicFunction();
        private FarPoint.Win.Spread.CellType.PercentCellType PC = new FarPoint.Win.Spread.CellType.PercentCellType();
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet_1Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int IntColCount = 5;
            int IntRowCount = 7+2 + 3;//标题占3行，分区类型占2行，1是其它用
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

            strTitle = "单位：万元、万人 万元/人";
            obj.AddSpanCell(IntRow, 0, 1, obj.Columns.Count);
            obj.SetValue(IntRow, 0, strTitle);
            //右对齐
            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

            strTitle = " 指 标 名 称";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow+=1, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " GDP";
            PF.CreateSheetView(obj, NextRowMerge-=1, NextColMerge, IntRow+= 2, IntCol , strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "人均GDP";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 第一产业比重";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow+= 1, IntCol , strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "第二产业比重";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "第三产业比重";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 总 人 口 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;
            //PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 给下拉列表添加年份
        /// </summary>
        public void InitYear(DevExpress.XtraBars.BarEditItem BE)
        {
            int FirstYear = 1990;
            int EndYear = 2060;
            for (int i = FirstYear; i <= EndYear; i++)
            {
                ((DevExpress.XtraEditors.Repository.RepositoryItemComboBox)BE.Edit).Items.Add(FirstYear.ToString());
                FirstYear++;
            }
        }
        /// <summary>
        /// 按照起始和结束年份写入列标题
        /// </summary>
        /// <param name="BeginYear"></param>
        /// <param name="EndYear"></param>
        public void SetColumnsTitle(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, int BeginYear, int EndYear)
        {
            int intTemp = BeginYear;
            Redraw(obj,BeginYear,EndYear);
            for (int i = BeginYear; i <= (EndYear + list.Count); ++i)
            {
                obj.AddSpanCell(4,(i - BeginYear + 1), 2, 1);
                //if(i!=EndYear+list.Count)//和下面eles对应
                //{
                    obj.SetValue(4, (i - BeginYear + 1), intTemp);
                    if(i==2006)
                    {
                        obj.SetValue(4, (i - BeginYear + 1), "“十五”年均增速");
                        intTemp--;
                    }
                    if(i==(2011+1))//中间空一行所以加一
                    {
                        obj.SetValue(4, (i - BeginYear + 1), "“十一五”年均增速");
                        intTemp--;
                    }
                    if(i==(2016+2))
                    {
                        obj.SetValue(4, (i - BeginYear + 1), "“十二五”年均增速");
                        intTemp--;
                    }
                    if(i==(2021+3))
                    {
                        obj.SetValue(4, (i - BeginYear + 1), "“十三五”年均增速");
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
            WriteData(FB, obj);
        }
        /// <summary>
        /// 重绘
        /// </summary>
        /// <param name="obj"></param>
        private void Redraw(FarPoint.Win.Spread.SheetView obj, int BeginYear, int EndYear)
        {
            //list.Clear();
            list = AddColumnTitle(BeginYear,EndYear);
            obj.RowCount = 0;
            obj.ColumnCount = 0;
            obj.ColumnCount = 1 + (EndYear - BeginYear + 1)+list.Count;

            int IntRowCount = 7 + 2 + 3;//标题占3行，分区类型占2行，1是其它用
            obj.RowCount = IntRowCount;
            string strTitle = "";
            IntRow = 3;

            PF.Sheet_GridandCenter(obj);//画边线，居中

            strTitle = "附表1 铜陵县"+BeginYear +"～"+EndYear+"年国民生产总值GDP情况表";
            PF.CreateSheetView(obj, IntRow, obj.ColumnCount, 0, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            IntCol = 1;

            strTitle = "单位：万元、万人 万元/人";
            obj.AddSpanCell(IntRow, 0, 1, obj.Columns.Count);
            obj.SetValue(IntRow, 0, strTitle);
            //右对齐
            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

            strTitle = " 指 标 名 称";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow += 1, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " GDP";
            PF.CreateSheetView(obj, NextRowMerge -= 1, NextColMerge, IntRow += 2, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "人均GDP";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 第一产业比重";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "第二产业比重";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "第三产业比重";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 总 人 口 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;

            m_PF.LockSheets(obj);
            //PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 从开始年份到结束年份包含几个十五计划
        /// </summary>
        /// <param name="BeginYear"></param>
        /// <param name="EndYear"></param>
        /// <returns></returns>
        private IList AddColumnTitle(int BeginYear,int EndYear)
        {
            IList list1 = new List<string>();
            if(BeginYear<=2001&&EndYear>=2005)
            {
                list1.Add( "“十五”年均增速");
            }
            if(BeginYear<=2006&&EndYear>=2010)
            {
                list1.Add("“十一五”年均增速");
            }
            if(BeginYear<=2011&&EndYear>=2015)
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
        /// 写入数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="CurrentYear">当前年份</param>
        /// <param name="strTitle">指标名称</param>
        private object SelectCurrentData(Itop.Client.Base.FormBase FB,  string CurrentYear,string strTitle)
        {
            string sql = null;
            object value = 0;
            string strTemp = "y" + CurrentYear;
            if (strTitle == " GDP")
            {
                strTitle = "全地区GDP（亿元）";
            }
            if (strTitle == " 第一产业比重")
            {
                strTitle = "第一产业比重";
            }
            if (strTitle == "第二产业比重")
            {
                strTitle = "第二产业比重";
            }
            if (strTitle == "第三产业比重")
            {
                strTitle = "第三产业比重";
            }
            if (strTitle == " 总 人 口 ")
            {
                strTitle = "铜陵县总人口";
            }
            sql = " select " + strTemp + " from ps_History  where Title='"+strTitle+"' and col4='"+FB.ProjectUID+"'";
            try
            {
                value = (double)Services.BaseService.GetObject("SelectPs_HistoryPopulationByCondition", sql);
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
        private void WriteData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj)
        {
            string strRowTitle = null;
            string strColTitle = null;
            for(int i=5;i<obj.RowCount;++i)
            {
                strRowTitle = (string)PF.ReturnStr(obj, i, 0);
                for(int j=1;j<obj.ColumnCount;++j)
                {
                    strColTitle =obj.GetValue(4,j).ToString();
                    if (strRowTitle == "人均GDP")
                    {

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
                            if (strRowTitle == " 第一产业比重" || strRowTitle == "第二产业比重" || strRowTitle == "第三产业比重")
                            {
                                obj.Cells[i, j].CellType = PC;//%
                            }
                            obj.SetValue(i, j, SelectCurrentData(FB, strColTitle, strRowTitle));
                        }
                    }
                  }
            }
            //向人均GDP那行回写数据
            for(int j=1; j<obj.ColumnCount;++j)
            {
                strColTitle = obj.GetValue(4, j).ToString();
                if (strColTitle == "“十五”年均增速" || strColTitle == "“十一五”年均增速" || strColTitle == "“十二五”年均增速" || strColTitle == "“十三五”年均增速")
                {
                    obj.Cells[7, j].Formula = "POWER(" + PF.GetColumnTitle(j - 5) + (8) + "/" + PF.GetColumnTitle(j-1) + 8 + ",1/5)-1";
                    obj.Cells[7, j].CellType = PC;//%
                }
                else
                {
                    obj.Cells[7, j].Formula = PF.GetColumnTitle(j) + (7) + "/" + PF.GetColumnTitle(j) + (12);
                }
            }
        }
    }
}
