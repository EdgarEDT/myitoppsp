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
 *  ClassName：Sheet14
 *  Action：附表14截至2009年底铜陵县负荷历史实绩统计表 的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表不用更新,这个表用数据库表ps_History
 * 年份：2010-10-27
 * 修改人：吕静涛
 * 修改时间：2010-11-04
 */

namespace Itop.VogliteVillageSheets.DemandForecast
{
    class Sheet14
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
        public void SetSheet_14Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int temp = 0;
            int IntColCount = 2;
            int IntRowCount = 6 + 2 + 3;//标题占3行，分区类型占2行
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

            strTitle = "单位：万千瓦时  万千瓦   %";
            obj.AddSpanCell(IntRow, 0, 1, obj.Columns.Count);
            obj.SetValue(IntRow, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            //右对齐
            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //列标题
            strTitle = "序号";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow += 1, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            for (int i = 0; i < (IntColCount - 1); ++i)
            {
                switch (i)
                {
                    case 0:
                        strTitle = " 项     目   ";
                        break;
                    default:
                        break;
                }
                PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
                PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            }
            //行标题
            IntRow = 5;
            for (int i = 0; i < 6; ++i)
            {
                for (int j = 0; j < 2; ++j)
                {
                    switch (j)
                    {
                        case 0:
                            temp = i + 1;
                            strTitle = temp.ToString();
                            break;
                        case 1:
                            if(i==0)
                            {
                                strTitle = " 全社会用电量   ";
                            }
                            if (i == 1)
                            {
                                strTitle = " 全社会最大负荷   ";
                            }
                            if (i == 2)
                            {
                                strTitle = " 网供电量   ";
                            }
                            if (i == 3)
                            {
                                strTitle = " 售电量   ";
                            }
                            if (i == 4)
                            {
                                strTitle = " 网供最大负荷   ";
                            }
                            if (i == 5)
                            {
                                strTitle = " 网供最大负荷利用小时   ";
                            }
                            break;
                        default:
                            break;
                    }
                    PF.CreateSheetView(obj, 1, 1, (IntRow+i), j, strTitle);
                    PF.SetSheetViewColumnsWidth(obj, 1, strTitle);
                }
            }
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
            int RowBegin = 4;//行起点
            int ColBegin = 2;//列起点
            Redraw(obj, BeginYear, EndYear);
            for (int i = BeginYear; i <= (EndYear + list.Count); ++i)
            {
                obj.AddSpanCell(RowBegin, (i - BeginYear + ColBegin), 1, 1);
                //if(i!=EndYear+list.Count)//和下面eles对应
                //{
                obj.SetValue(RowBegin, (i - BeginYear + ColBegin), intTemp);
                if (i == 2006)
                {
                    obj.SetValue(RowBegin, (i - BeginYear + ColBegin), "“十五”年均增速");
                    intTemp--;
                }
                if (i == (2011 + 1))//中间空一行所以加一
                {
                    obj.SetValue(RowBegin, (i - BeginYear + ColBegin), "“十一五”年均增速");
                    intTemp--;
                }
                if (i == (2016 + 2))
                {
                    obj.SetValue(RowBegin, (i - BeginYear + ColBegin), "“十二五”年均增速");
                    intTemp--;
                }
                if (i == (2021 + 3))
                {
                    obj.SetValue(RowBegin, (i - BeginYear + ColBegin), "“十三五”年均增速");
                    intTemp--;
                }
            //    //}
            //    //else//这是预计的列现在先不用
            //    //{
            //    //    if (i != 2006 && i != (2011 + 1) && i != (2016 + 2) && i != (2021 + 3))
            //    //    {
            //    //        obj.SetValue(4, (i - BeginYear + 1), intTemp + "预计");
            //    //    }
            //    //    else
            //    //    {
            //    //        if (i == 2006)
            //    //        {
            //    //            obj.SetValue(4, (i - BeginYear + 1), "“十五”年均增速");
            //    //            intTemp--;
            //    //        }
            //    //        if (i == (2011 + 1))//中间空一行所以加一
            //    //        {
            //    //            obj.SetValue(4, (i - BeginYear + 1), "“十一五”年均增速");
            //    //            intTemp--;
            //    //        }
            //    //        if (i == (2016 + 2))
            //    //        {
            //    //            obj.SetValue(4, (i - BeginYear + 1), "“十二五”年均增速");
            //    //            intTemp--;
            //    //        }
            //    //        if (i == (2021 + 3))
            //    //        {
            //    //            obj.SetValue(4, (i - BeginYear + 1), "“十三五”年均增速");
            //    //            intTemp--;
            //    //        }
            //    //    }

            //    //}
                intTemp++;
            }
            list.Clear();
            WriteData(FB, obj, 5);
        }
        /// <summary>
        /// 重绘
        /// </summary>
        /// <param name="obj"></param>
        private void Redraw( FarPoint.Win.Spread.SheetView obj, int BeginYear, int EndYear)
        {
            int temp = 0;
            int IntColCount = 2;
            int IntRowCount = 6 + 2 + 3;//标题占3行，分区类型占2行
            list = AddColumnTitle(BeginYear, EndYear);
            obj.RowCount = 0;
            obj.ColumnCount = 0;
            obj.ColumnCount = 2 + (EndYear - BeginYear + 1) + list.Count;
            obj.RowCount = IntRowCount;
            IntColCount = obj.ColumnCount;

            IntCol = obj.Columns.Count;

            PF.Sheet_GridandCenter(obj);//画边线，居中
            m_PF.LockSheets(obj);

            string strTitle = "";
            IntRow = 3;
            strTitle = "附表14截至" + EndYear + "年底铜陵县负荷历史实绩统计表";
            obj.SheetName = strTitle;
            PF.CreateSheetView(obj, IntRow, IntCol, 0, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            IntCol = 1;

            strTitle = "单位：万千瓦时  万千瓦   %";
            obj.AddSpanCell(IntRow, 0, 1, obj.Columns.Count);
            obj.SetValue(IntRow, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            //右对齐
            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //列标题
            strTitle = "序号";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge, IntRow += 1, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            for (int i = 0; i < (IntColCount - 1); ++i)
            {
                
                switch (i)
                {
                    case 0:
                        strTitle = " 项     目   ";
                        break;
                    default:
                        break;
                }
                PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
                PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            }
            //行标题
            IntRow = 5;
            for (int i = 0; i < 6; ++i)
            {
                for (int j = 0; j < 2; ++j)
                {
                    switch (j)
                    {
                        case 0:
                            temp = i + 1;
                            strTitle = temp.ToString();
                            break;
                        case 1:
                            if (i == 0)
                            {
                                strTitle = " 全社会用电量   ";
                            }
                            if (i == 1)
                            {
                                strTitle = " 全社会最大负荷   ";
                            }
                            if (i == 2)
                            {
                                strTitle = " 网供电量   ";
                            }
                            if (i == 3)
                            {
                                strTitle = " 售电量   ";
                            }
                            if (i == 4)
                            {
                                strTitle = " 网供最大负荷   ";
                            }
                            if (i == 5)
                            {
                                strTitle = " 网供最大负荷利用小时   ";
                            }
                            break;
                        default:
                            break;
                    }
                    PF.CreateSheetView(obj, 1, 1, (IntRow + i), j, strTitle);
                    PF.SetSheetViewColumnsWidth(obj, 1, strTitle);
                }
            }
            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;

            m_PF.LockSheets(obj);

            //PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="obj"></param>
        private void WriteData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj,int IntRow)
        {
            string strRowTitle=null;
            string strColTitle = null;
            for(int i=IntRow;i<obj.RowCount;++i)
            {
                strRowTitle = PF.ReturnStr(obj, (i), 1).ToString();
                for(int j=2;j<obj.ColumnCount;++j)
                {
                    strColTitle =obj.GetValue((4), j).ToString();
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
                        if (strRowTitle != " 网供最大负荷利用小时   ")
                        {
                            obj.SetValue(i, j, SelectCurrentData(FB, strColTitle, strRowTitle));
                        }
                        else
                        {
                            obj.Cells[i, j].Formula = PF.GetColumnTitle(j) + (i-2) + "/" + PF.GetColumnTitle(j) + (i);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 查询当前数据
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="CurrentYear"></param>
        /// <param name="RowTitle"></param>
        private object SelectCurrentData(Itop.Client.Base.FormBase FB,string CurrentYear,string RowTitle)
        {
            string sql = null;
            object value = 0;
            string strTemp = "y" + CurrentYear;
            if (RowTitle == " 全社会用电量   ")
            {
                RowTitle = "全社会用电量（亿kWh）";
            }
            if (RowTitle == " 全社会最大负荷   ")
            {
                RowTitle = "全社会最大负荷（万kW）";
            }
            if (RowTitle == " 网供电量   ")
            {
                RowTitle = "网供电量";
            }
            if (RowTitle == " 售电量   ")
            {
                RowTitle = "售电量";
            }
            if (RowTitle == " 网供最大负荷   ")
            {
                RowTitle = "网供最大负荷";
            }
            sql = " select " + strTemp + " from ps_History  where Title='" + RowTitle + "' and col4='" + FB.ProjectUID + "'";
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
    }
}
