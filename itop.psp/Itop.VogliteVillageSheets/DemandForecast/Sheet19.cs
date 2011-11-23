using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using Itop.JournalSheet.Function;
using Itop.Domain.Forecast;
using Itop.Client.Common;
using DevExpress.Utils;
using System.Threading;
//////////////////////////////////////////////////////////////////////////
/*
 *  ClassName：Sheet19
 *  Action：2010-2020年铜陵县县负荷预测表 的数据写入

 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表不用更新，用数据库表，ps_forecast_list(方案表),ps_Forecast_math
 *              先在ps_forecast_list表中找到需要的方案，方案的id值就是ps_Forecast_math
 *              表中的ForecastID值来关联数据
 * 年份：2010-10-29
 */

namespace Itop.VogliteVillageSheets.DemandForecast
{
    class Sheet19
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值

        private int NextColMerge = 1;//合并单元格列初始值

        private string projectID = "";
        private string programID = "";
        private IList list_QSHKJ = null;//全社会口径

        private IList list_WGKJ = null;//网供口径
        private IList list_East = null;//东部地区
        private IList list_Midst = null;//中部地区
        private IList list_Southern = null;//南部地区
        private WaitDialogForm wait = null;

        private Itop.JournalSheet.Function.PublicFunction PF = new Itop.JournalSheet.Function.PublicFunction();
        private Itop.VogliteVillageSheets.Function.PublicFunction m_PF = new Itop.VogliteVillageSheets.Function.PublicFunction();
        private FarPoint.Win.Spread.CellType.PercentCellType PC = new FarPoint.Win.Spread.CellType.PercentCellType();
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet_19Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int RowStep = 3;
            int IntColCount = 2+8;
            int IntRowCount = 23 + 2 + 3;//标题占3行，分区类型占2行

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

            strTitle = "  单位：万千瓦 万千瓦时";
            obj.AddSpanCell(IntRow, 0, 1, obj.Columns.Count);
            obj.SetValue(IntRow, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            //右对齐

            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //全社会口径

            strTitle = "全社会口径  ";
            PF.CreateSheetView(obj, NextRowMerge, obj.Columns.Count, IntRow += 1, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
           
            strTitle = "电     量   ";
            PF.CreateSheetView(obj, NextRowMerge += 3, NextColMerge, IntRow += 1, IntCol , strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            strTitle = "负     荷   ";
            PF.CreateSheetView(obj, 3 , NextColMerge, IntRow += 4, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            IntRow = 5;
            strTitle = "  方  案  ";
            PF.CreateSheetView(obj, 1, NextColMerge, (IntRow ), 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            for (int i = 0; i < 2; i++)
            {
                strTitle = "  高  ";
                PF.CreateSheetView(obj, 1, 1, (IntRow +1 + i * RowStep), 1, strTitle);
                strTitle = "  中  ";
                PF.CreateSheetView(obj, 1, 1, (IntRow + 2 + i * RowStep), 1, strTitle);
                strTitle = "  低  ";
                PF.CreateSheetView(obj, 1, 1, (IntRow + 3 + i * RowStep), 1, strTitle);
            }
            //strTitle = "  方  案  ";
            //PF.CreateSheetView(obj, 1, NextColMerge, (IntRow ), 2, strTitle);
            //PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //东部地区
            IntRow = 12;
            strTitle = "东部地区  ";
            PF.CreateSheetView(obj, 1, obj.Columns.Count, IntRow ,0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            strTitle = "电     量   ";
            PF.CreateSheetView(obj,1, 2, IntRow += 1, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            strTitle = "负     荷   ";
            PF.CreateSheetView(obj, 1, 2, IntRow += 1, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //南部地区
            IntRow = 12 + 3;
            strTitle = "南部地区  ";
            PF.CreateSheetView(obj, 1, obj.Columns.Count, IntRow, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            strTitle = "电     量   ";
            PF.CreateSheetView(obj, 1, 2, IntRow += 1, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            strTitle = "负     荷   ";
            PF.CreateSheetView(obj, 1, 2, IntRow += 1, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //网供口径
            IntRow = 12 + 3 + 3;
            strTitle = "网供口径  ";
            PF.CreateSheetView(obj, 1, obj.Columns.Count, IntRow, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            strTitle = "电     量   ";
            PF.CreateSheetView(obj, 3, 1, IntRow += 1, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            strTitle = "负     荷   ";
            PF.CreateSheetView(obj, 3, 1, IntRow += 3, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            IntRow = 12 + 3 + 3 + 1;
            RowStep = 3;
            for (int i = 0; i < 2; i++)
            {
                strTitle = "  高  ";
                PF.CreateSheetView(obj, 1, NextColMerge, (IntRow  + i * RowStep), 1, strTitle);
                strTitle = "  中  ";
                PF.CreateSheetView(obj, 1, NextColMerge, (IntRow + 1 + i * RowStep), 1, strTitle);
                strTitle = "  低  ";
                PF.CreateSheetView(obj, 1, NextColMerge, (IntRow + 2 + i * RowStep), 1, strTitle);
            }
            //中部地区
            IntRow = 12 + 3 + 3 + 7;
            strTitle = "中部地区  ";
            PF.CreateSheetView(obj, 1, obj.Columns.Count, IntRow, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            strTitle = "电     量   ";
            PF.CreateSheetView(obj, 1, 2, IntRow += 1, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            strTitle = "负     荷   ";
            PF.CreateSheetView(obj, 1, 2, IntRow += 1, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;
            AddColumnTitle(obj,IntRow);
            //PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        private void WriteData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, int IntRow)
        {
            Ps_Forecast_Math pfm1 = null;//高

            Ps_Forecast_Math pfm2 = null;//中

            Ps_Forecast_Math pfm3 = null;//低

            Ps_Forecast_Math value = null;
            list_QSHKJ=SelectDQData(FB,programID,"全社会口径  ");
            list_WGKJ = SelectDQData(FB, programID, "网供口径");
            list_East = SelectDQData(FB, programID, "东部地区");
            list_Midst  = SelectDQData(FB, programID, "中部地区");
            list_Southern  = SelectDQData(FB, programID, "南部地区");
            #region 全社会口径：电量
            try
            {
                for (int i = IntRow; i < list_QSHKJ.Count + IntRow; ++i)
                {
                    pfm1 = (Ps_Forecast_Math)list_QSHKJ[i - IntRow + 1];
                    if (pfm1 != null)
                    {
                        value = ReturnValue("电量  ", pfm1.ID, pfm1.Forecast);
                        if(value!=null)
                        {
                            for (int j = 2; j < obj.ColumnCount; ++j)
                            {
                                //全社会口径的数据
                                //电量，高中低预测
                                switch (j)
                                {
                                    case 2:
                                        obj.SetValue(i, j, value.y2010);
                                        break;
                                    case 3:
                                        obj.SetValue(i, j, value.y2011);
                                        break;
                                    case 4:
                                        obj.SetValue(i, j, value.y2012);
                                        break;
                                    case 5:
                                        obj.SetValue(i, j, value.y2013);
                                        break;
                                    case 6:
                                        obj.SetValue(i, j, value.y2014);
                                        break;
                                    case 7:
                                        obj.SetValue(i, j, value.y2015);
                                        break;
                                    case 8:
                                        obj.Cells[i, j].Formula = "POWER(" + PF.GetColumnTitle(j - 1) + (i + 1) + "/" + PF.GetColumnTitle(j - 6) + (i + 1) + ",1/5)-1";
                                        obj.Cells[i, j].CellType = PC;
                                        break;
                                    case 9:
                                        obj.SetValue(i, j, value.y2020);
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }
                        else
                        {
                            for (int j = 2; j < obj.ColumnCount; ++j)
                            {
                                obj.SetValue(i, j, null);
                            }
                        }

                    }
                }
            }
            catch (System.Exception e)
            {
            	
            }           
#endregion
            int Temp=(list_QSHKJ.Count+IntRow);
            #region 全社会口径：负荷
             try
            {
                for (int i = Temp; i < list_QSHKJ.Count + Temp; ++i)
                {
                    pfm1 = (Ps_Forecast_Math)list_QSHKJ[i - Temp + 1];
                    if (pfm1 != null)
                    {
                        value = ReturnValue("负荷  ", pfm1.ID, pfm1.Forecast);
                        if(value!=null)
                        {
                            for (int j = 2; j < obj.ColumnCount; ++j)
                            {
                                //全社会口径的数据
                                //电量，高中低预测
                                switch (j)
                                {
                                    case 2:
                                        obj.SetValue(i, j, value.y2010);
                                        break;
                                    case 3:
                                        obj.SetValue(i, j, value.y2011);
                                        break;
                                    case 4:
                                        obj.SetValue(i, j, value.y2012);
                                        break;
                                    case 5:
                                        obj.SetValue(i, j, value.y2013);
                                        break;
                                    case 6:
                                        obj.SetValue(i, j, value.y2014);
                                        break;
                                    case 7:
                                        obj.SetValue(i, j, value.y2015);
                                        break;
                                    case 8:
                                        obj.Cells[i, j].Formula = "POWER(" + PF.GetColumnTitle(j - 1) + (i + 1) + "/" + PF.GetColumnTitle(j - 6) + (i + 1) + ",1/5)-1";
                                        obj.Cells[i, j].CellType = PC;
                                        break;
                                    case 9:
                                        obj.SetValue(i, j, value.y2020);
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }
                        else
                        {
                            for (int j = 2; j < obj.ColumnCount; ++j)
                            {
                                obj.SetValue(i, j, null);
                            }
                        }

                    }
                }
            }
            catch (System.Exception e)
            {

            }           
            #endregion
            Temp += (list_QSHKJ.Count+1);
            #region 东部地区：全社会用电量

            try
            {
                for (int i = Temp; i <= 1 + Temp; ++i)
                {
                    pfm1 = (Ps_Forecast_Math)list_East[1];
                    if (pfm1 != null)
                    {
                        value = ReturnValue("全社会用电量", pfm1.ID, pfm1.Forecast);
                        if(value!=null)
                        {
                            for (int j = 2; j < obj.ColumnCount; ++j)
                            {
                                //全社会口径的数据
                                //电量，高中低预测
                                switch (j)
                                {
                                    case 2:
                                        obj.SetValue(i, j, value.y2010);
                                        break;
                                    case 3:
                                        obj.SetValue(i, j, value.y2011);
                                        break;
                                    case 4:
                                        obj.SetValue(i, j, value.y2012);
                                        break;
                                    case 5:
                                        obj.SetValue(i, j, value.y2013);
                                        break;
                                    case 6:
                                        obj.SetValue(i, j, value.y2014);
                                        break;
                                    case 7:
                                        obj.SetValue(i, j, value.y2015);
                                        break;
                                    case 8:
                                        obj.Cells[i, j].Formula = "POWER(" + PF.GetColumnTitle(j - 1) + (i + 1) + "/" + PF.GetColumnTitle(j - 6) + (i + 1) + ",1/5)-1";
                                        obj.Cells[i, j].CellType = PC;
                                        break;
                                    case 9:
                                        obj.SetValue(i, j, value.y2020);
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }
                        else
                        {
                            for (int j = 2; j < obj.ColumnCount; ++j)
                            {
                                obj.SetValue(i, j, null);
                            }
                        }

                    }
                }
            }
            catch (System.Exception e)
            {

            }
            #endregion
            Temp += 1;
            #region 东部地区：全社会最大负荷

            try
            {
                for (int i = Temp; i <= 1 + Temp; ++i)
                {
                    pfm1 = (Ps_Forecast_Math)list_East[1];
                    if (pfm1 != null)
                    {
                        value = ReturnValue("全社会最大负荷", pfm1.ID, pfm1.Forecast);
                        if(value!=null)
                        {
                            for (int j = 2; j < obj.ColumnCount; ++j)
                            {
                                //全社会口径的数据
                                //电量，高中低预测
                                switch (j)
                                {
                                    case 2:
                                        obj.SetValue(i, j, value.y2010);
                                        break;
                                    case 3:
                                        obj.SetValue(i, j, value.y2011);
                                        break;
                                    case 4:
                                        obj.SetValue(i, j, value.y2012);
                                        break;
                                    case 5:
                                        obj.SetValue(i, j, value.y2013);
                                        break;
                                    case 6:
                                        obj.SetValue(i, j, value.y2014);
                                        break;
                                    case 7:
                                        obj.SetValue(i, j, value.y2015);
                                        break;
                                    case 8:
                                        obj.Cells[i, j].Formula = "POWER(" + PF.GetColumnTitle(j - 1) + (i + 1) + "/" + PF.GetColumnTitle(j - 6) + (i + 1) + ",1/5)-1";
                                        obj.Cells[i, j].CellType = PC;
                                        break;
                                    case 9:
                                        obj.SetValue(i, j, value.y2020);
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }
                        else
                        {
                            for (int j = 2; j < obj.ColumnCount; ++j)
                            {
                                obj.SetValue(i, j, null);
                            }
                        }

                    }
                }
            }
            catch (System.Exception e)
            {

            }
            #endregion
            Temp += 2;
            #region 南部地区：全社会用电量

            try
            {
                for (int i = Temp; i <= 1 + Temp; ++i)
                {
                    pfm1 = (Ps_Forecast_Math)list_Southern[1];
                    if (pfm1 != null)
                    {
                        value = ReturnValue("全社会用电量", pfm1.ID, pfm1.Forecast);
                        if(value!=null)
                        {
                            for (int j = 2; j < obj.ColumnCount; ++j)
                            {
                                //全社会口径的数据
                                //电量，高中低预测
                                switch (j)
                                {
                                    case 2:
                                        obj.SetValue(i, j, value.y2010);
                                        break;
                                    case 3:
                                        obj.SetValue(i, j, value.y2011);
                                        break;
                                    case 4:
                                        obj.SetValue(i, j, value.y2012);
                                        break;
                                    case 5:
                                        obj.SetValue(i, j, value.y2013);
                                        break;
                                    case 6:
                                        obj.SetValue(i, j, value.y2014);
                                        break;
                                    case 7:
                                        obj.SetValue(i, j, value.y2015);
                                        break;
                                    case 8:
                                        obj.Cells[i, j].Formula = "POWER(" + PF.GetColumnTitle(j - 1) + (i + 1) + "/" + PF.GetColumnTitle(j - 6) + (i + 1) + ",1/5)-1";
                                        obj.Cells[i, j].CellType = PC;
                                        break;
                                    case 9:
                                        obj.SetValue(i, j, value.y2020);
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }
                        else
                        {
                            for (int j = 2; j < obj.ColumnCount; ++j)
                            {
                                obj.SetValue(i, j, null);
                            }
                        }
                    }
                }
            }
            catch (System.Exception e)
            {

            }
            #endregion
            Temp += 1;
            #region 南部地区：全社会最大负荷

            try
            {
                for (int i = Temp; i <= 1 + Temp; ++i)
                {
                    pfm1 = (Ps_Forecast_Math)list_Southern[1];
                    if (pfm1 != null)
                    {
                        value = ReturnValue("全社会最大负荷", pfm1.ID, pfm1.Forecast);
                        if(value!=null)
                        {
                            for (int j = 2; j < obj.ColumnCount; ++j)
                            {
                                //全社会口径的数据
                                //电量，高中低预测
                                switch (j)
                                {
                                    case 2:
                                        obj.SetValue(i, j, value.y2010);
                                        break;
                                    case 3:
                                        obj.SetValue(i, j, value.y2011);
                                        break;
                                    case 4:
                                        obj.SetValue(i, j, value.y2012);
                                        break;
                                    case 5:
                                        obj.SetValue(i, j, value.y2013);
                                        break;
                                    case 6:
                                        obj.SetValue(i, j, value.y2014);
                                        break;
                                    case 7:
                                        obj.SetValue(i, j, value.y2015);
                                        break;
                                    case 8:
                                        obj.Cells[i, j].Formula = "POWER(" + PF.GetColumnTitle(j - 1) + (i + 1) + "/" + PF.GetColumnTitle(j - 6) + (i + 1) + ",1/5)-1";
                                        obj.Cells[i, j].CellType = PC;
                                        break;
                                    case 9:
                                        obj.SetValue(i, j, value.y2020);
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }
                        else
                        {
                            for (int j = 2; j < obj.ColumnCount; ++j)
                            {
                                obj.SetValue(i, j, null);
                            }
                        }

                    }
                }
            }
            catch (System.Exception e)
            {

            }
            #endregion
            Temp += 2;
            #region 网供口径：电量

            try
            {
                for (int i = Temp; i < list_WGKJ.Count + Temp; ++i)
                {
                    pfm1 = (Ps_Forecast_Math)list_WGKJ[i - Temp + 1];
                    if (pfm1 != null)
                    {
                        value = ReturnValue("电量  ", pfm1.ID, pfm1.Forecast);
                        if(value!=null)
                        {
                            for (int j = 2; j < obj.ColumnCount; ++j)
                            {
                                //网供口径的数据

                                //电量，高中低预测
                                switch (j)
                                {
                                    case 2:
                                        obj.SetValue(i, j, value.y2010);
                                        break;
                                    case 3:
                                        obj.SetValue(i, j, value.y2011);
                                        break;
                                    case 4:
                                        obj.SetValue(i, j, value.y2012);
                                        break;
                                    case 5:
                                        obj.SetValue(i, j, value.y2013);
                                        break;
                                    case 6:
                                        obj.SetValue(i, j, value.y2014);
                                        break;
                                    case 7:
                                        obj.SetValue(i, j, value.y2015);
                                        break;
                                    case 8:
                                        obj.Cells[i, j].Formula = "POWER(" + PF.GetColumnTitle(j - 1) + (i + 1) + "/" + PF.GetColumnTitle(j - 6) + (i + 1) + ",1/5)-1";
                                        obj.Cells[i, j].CellType = PC;
                                        break;
                                    case 9:
                                        obj.SetValue(i, j, value.y2020);
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }
                        else
                        {
                            for (int j = 2; j < obj.ColumnCount; ++j)
                            {
                                obj.SetValue(i, j, null);
                            }
                        }
                    }
                }
            }
            catch (System.Exception e)
            {

            }
            #endregion
            Temp = (list_WGKJ.Count + Temp);
            #region 网供口径：负荷

            try
            {
                for (int i = Temp; i < list_WGKJ.Count + Temp; ++i)
                {
                    pfm1 = (Ps_Forecast_Math)list_WGKJ[i - Temp + 1];
                    if (pfm1 != null)
                    {
                        value = ReturnValue("负荷  ", pfm1.ID, pfm1.Forecast);
                        if(value!=null)
                        {
                            for (int j = 2; j < obj.ColumnCount; ++j)
                            {
                                //网供口径的数据

                                //电量，高中低预测
                                switch (j)
                                {
                                    case 2:
                                        obj.SetValue(i, j, value.y2010);
                                        break;
                                    case 3:
                                        obj.SetValue(i, j, value.y2011);
                                        break;
                                    case 4:
                                        obj.SetValue(i, j, value.y2012);
                                        break;
                                    case 5:
                                        obj.SetValue(i, j, value.y2013);
                                        break;
                                    case 6:
                                        obj.SetValue(i, j, value.y2014);
                                        break;
                                    case 7:
                                        obj.SetValue(i, j, value.y2015);
                                        break;
                                    case 8:
                                        obj.Cells[i, j].Formula = "POWER(" + PF.GetColumnTitle(j - 1) + (i + 1) + "/" + PF.GetColumnTitle(j - 6) + (i + 1) + ",1/5)-1";
                                        obj.Cells[i, j].CellType = PC;
                                        break;
                                    case 9:
                                        obj.SetValue(i, j, value.y2020);
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }
                        else
                        {
                            for (int j = 2; j < obj.ColumnCount; ++j)
                            {
                                obj.SetValue(i, j, null);
                            }
                        }

                    }
                }
            }
            catch (System.Exception e)
            {

            }
            #endregion
            Temp += (list_WGKJ.Count+1);
            #region 中部地区：全社会用电量

            try
            {
                for (int i = Temp; i <= 1 + Temp; ++i)
                {
                    pfm1 = (Ps_Forecast_Math)list_Midst[1];
                    if (pfm1 != null)
                    {
                        value = ReturnValue("全社会用电量", pfm1.ID, pfm1.Forecast);
                        if(value!=null)
                        {
                            for (int j = 2; j < obj.ColumnCount; ++j)
                            {
                                //全社会口径的数据
                                //电量，高中低预测
                                switch (j)
                                {
                                    case 2:
                                        obj.SetValue(i, j, value.y2010);
                                        break;
                                    case 3:
                                        obj.SetValue(i, j, value.y2011);
                                        break;
                                    case 4:
                                        obj.SetValue(i, j, value.y2012);
                                        break;
                                    case 5:
                                        obj.SetValue(i, j, value.y2013);
                                        break;
                                    case 6:
                                        obj.SetValue(i, j, value.y2014);
                                        break;
                                    case 7:
                                        obj.SetValue(i, j, value.y2015);
                                        break;
                                    case 8:
                                        obj.Cells[i, j].Formula = "POWER(" + PF.GetColumnTitle(j - 1) + (i + 1) + "/" + PF.GetColumnTitle(j - 6) + (i + 1) + ",1/5)-1";
                                        obj.Cells[i, j].CellType = PC;
                                        break;
                                    case 9:
                                        obj.SetValue(i, j, value.y2020);
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }
                        else
                        {
                            for (int j = 2; j < obj.ColumnCount; ++j)
                            {
                                obj.SetValue(i, j, null);
                            }
                        }
                    }
                }
            }
            catch (System.Exception e)
            {

            }
            #endregion
            Temp += 1;
            #region 中部地区：全社会最大负荷

            try
            {
                for (int i = Temp; i <= 1 + Temp; ++i)
                {
                    pfm1 = (Ps_Forecast_Math)list_Midst[1];
                    if (pfm1 != null)
                    {
                        value = ReturnValue("全社会最大负荷", pfm1.ID, pfm1.Forecast);
                        if(value!=null)
                        {
                            for (int j = 2; j < obj.ColumnCount; ++j)
                            {
                                switch (j)
                                {
                                    case 2:
                                        obj.SetValue(i, j, value.y2010);
                                        break;
                                    case 3:
                                        obj.SetValue(i, j, value.y2011);
                                        break;
                                    case 4:
                                        obj.SetValue(i, j, value.y2012);
                                        break;
                                    case 5:
                                        obj.SetValue(i, j, value.y2013);
                                        break;
                                    case 6:
                                        obj.SetValue(i, j, value.y2014);
                                        break;
                                    case 7:
                                        obj.SetValue(i, j, value.y2015);
                                        break;
                                    case 8:
                                        obj.Cells[i, j].Formula = "POWER(" + PF.GetColumnTitle(j - 1) + (i + 1) + "/" + PF.GetColumnTitle(j - 6) + (i + 1) + ",1/5)-1";
                                        obj.Cells[i, j].CellType = PC;
                                        break;
                                    case 9:
                                        obj.SetValue(i, j, value.y2020);
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }
                        else
                        {
                            for (int j = 2; j < obj.ColumnCount; ++j)
                            {
                                obj.SetValue(i, j, null);
                            }
                        }
                    }
                }
            }
            catch (System.Exception e)
            {

            }
            #endregion

        }
        /// <summary>
        /// 重绘
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="obj"></param>
        private void ReDraw(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj)
        {
            obj.RowCount = 0;
            obj.ColumnCount = 0;
            SetSheet_19Title(FB, obj, "2010-2020年铜陵县县负荷预测表");
        }
        /// <summary>
        /// 通过名称和预测号来取值

        /// </summary>
        /// <param name="parentID"></param>
        /// <param name="ForecastID"></param>
        /// <returns></returns>
        private Ps_Forecast_Math ReturnValue(string strTitle,string parentID, int ForecastID)
        {
            Ps_Forecast_Math list = null;
            string sql = " ForecastID='"+programID+"' and parentId='"+parentID+"' and Forecast='"+ForecastID+"'"
            +" and Title='"+strTitle+"'";
            try
            {
                list = (Ps_Forecast_Math)Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere", sql);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return list;
        }
        /// <summary>
        /// 查找数据,全社会口径 ,网供口径,东部地区,南部地区,中部地区
        /// </summary>
        private IList SelectDQData(Itop.Client.Base.FormBase FB,string programID,string DQ)
        {
            IList list = null;
            string sql = "ForecastID='"+programID+"' and Title='"+DQ+"' order by Forecast Asc";
            try
            {
                list = Services.BaseService.GetList("SelectPs_Forecast_MathByWhere", sql);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return list;
        }
        /// <summary>
        /// 添加列标题

        /// </summary>
        /// <param name="obj"></param>
        private void AddColumnTitle(FarPoint.Win.Spread.SheetView obj, int IntRow)
        {
            for(int i=2;i<obj.ColumnCount;++i)
            {
                switch (i)
                {
                case 2:
                    obj.SetValue(IntRow, i, "2010年");
                	break;
                case 3:
                    obj.SetValue(IntRow, i, "2011年");
                    break;
                case 4:
                    obj.SetValue(IntRow, i, "2012年");
                    break;
                case 5:
                    obj.SetValue(IntRow, i, "2013年");
                    break;
                case 6:
                    obj.SetValue(IntRow, i, "2014年");
                    break;
                case 7:
                    obj.SetValue(IntRow, i, "2015年");
                    break;
                case 8:
                    obj.SetValue(IntRow, i, "十二五年均增长率");
                    break;
                case 9:
                    obj.SetValue(IntRow, i, "2020年");
                    break;
                default:
                        break;
                }
            }
        }
        /// <summary>
        /// 查询方案内容，给下拉菜单用

        /// </summary>
        private IList SelectProgramme(Itop.Client.Base.FormBase FB)
        {
            IList Programme = null;//方案
            //Ps_forecast_list report = new Ps_forecast_list();
            //report.UserID =FB. ProjectUID;
            Programme = Services.BaseService.GetList("SelectPs_forecast_listByUserID", FB.ProjectUID);
            return Programme;
        }
        /// <summary>
        /// 选中下拉菜单中的数据连接数据库表,方案
        /// </summary>
        /// <param name="BE"></param>
        public  void SelectEditChange(FarPoint.Win.Spread.SheetView tempSheet, object obj, Itop.Client.Base.FormBase FB)
        {
            //string strTitle = null;
            string strID = obj.ToString();//方案名称
            string[] AreaType1 = new string[2];

            //Ps_forecast_list pfl = null;
            projectID = FB.ProjectUID;
            //string con1 = "Title='" + strTitle + "' and UserID='" + projectID + "'";
            try
            {
                //查询下拉菜单所选中的数据

                programID = strID;
                //Cursor cur = Cursor.Current;
                //Cursor.Current = Cursors.WaitCursor;
                //wait.Cursor = Cursors.WaitCursor;
                System.Threading.Thread.Sleep(2000);
                ReDraw(FB, tempSheet);
                WriteData(FB,tempSheet, 6);//数据

                PF.Sheet_GridandCenter(tempSheet);//画边线，居中
                m_PF.LockSheets(tempSheet);
                //Cursor.Current = cur;
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        /// <summary>
        /// add BareditItems of years
        /// </summary>
        /// <param name="BE">BarEditItem object</param>
        public void AddBarEditItems(DevExpress.XtraBars.BarEditItem BE, DevExpress.XtraBars.BarEditItem BE1, Itop.Client.Base.FormBase FB)
        {
            IList list = SelectProgramme(FB);
            Ps_forecast_list pfl = null;
            //BE.EditValue = "2000";
            //BE.EditValue = list;
            for (int i = 0; i < list.Count; ++i)
            {
                pfl = (Ps_forecast_list)list[i];
                ((DevExpress.XtraEditors.Repository.RepositoryItemComboBox)BE.Edit).Items.Add(pfl.Title);
                ((DevExpress.XtraEditors.Repository.RepositoryItemComboBox)BE1.Edit).Items.Add(pfl.ID);
            }
        }
    }
}
