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
 *  ClassName：Sheet18
 *  Action：附表18 规划年铜陵县大用户统计信息表 的数据写入

 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用ps_history数据库表，col11字段：计划开工时间col12字段：计划投产时间，col6字段：备注

 *              y1990字段：用电量，y1991字段：最大用电负荷，forecast字段为2,col13字段：项目性质
 * 年份：2010-10-28
 */
namespace Itop.VogliteVillageSheets.DemandForecast
{
    class Sheet18
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
        public void SetSheet_18Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int Temp = 0;
            int IntColCount = 9;
            int IntRowCount =2 + 2 + 3;//标题占3行，分区类型占2行

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

            strTitle = "单位：万元，万元/人，%，万人";
            obj.AddSpanCell(IntRow, 0, 1, obj.Columns.Count);
            obj.SetValue(IntRow, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            //右对齐

            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //列标题

            strTitle = "序号";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow += 1, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            for (int i = 0; i < (IntColCount - 1); ++i)
            {
                switch (i)
                {
                    case 0:
                        strTitle = " 项目名称   ";
                        break;
                    case 1:
                        strTitle = " 项目性质   ";
                        break;
                    case 2:
                        strTitle = " 计划开工时间   ";
                        break;
                    case 3:
                        strTitle = " 计划投产时间   ";
                        break;
                    case 4:
                        strTitle = " 用电量   ";
                        break;
                    case 5:
                        strTitle = " 最大用电负荷   ";
                        break;
                    case 6:
                        strTitle = " 所属分区   ";
                        break;
                    case 7:
                        strTitle = " 备     注 ";
                        break;
                    default:
                        break;
                }
                PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
                PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            }


            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 6;
            IntCol = 0;

            SetLeftTitle(FB,obj,IntRow);
            //PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 设置左侧标题,写入数据
        /// </summary>
        /// <param name="obj"></param>
        private void SetLeftTitle(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, int IntRow)
        {
            int temp = 1;
            Ps_History ph = null;
            SelectData(FB);
            obj.RowCount = list.Count + IntRow;
            for(int i=IntRow;i<obj.RowCount;++i)
            {
                ph=(Ps_History)list[i-IntRow];
                for(int j=0;j<obj.ColumnCount;++j)
                {
                    switch (j)
                    {
                        case 0://序号
                            if (i != obj.RowCount - 1)
                            {
                                obj.SetValue(i, j, temp);
                            }
                            else
                            {
                                obj.SetValue(i, j, "合计");
                            }
                    	    break;
                        case 1://项目名称
                            if(i!=obj.RowCount-1)
                                obj.SetValue(i, j, ph.Title);
                            break;
                        case 2://项目性质
                            if (i != obj.RowCount - 1)
                                obj.SetValue(i, j, ph.Col13);
                            break;
                        case 3://计划开工时间

                            if (i != obj.RowCount - 1)
                                obj.SetValue(i, j, ph.Col11.Substring(0, 5));
                            break;
                        case 4://计划投产时间
                            if (i != obj.RowCount - 1)
                                obj.SetValue(i, j, ph.Col12.Substring(0, 5));
                            break;
                        case 5://用电量

                            if (i != obj.RowCount - 1)
                                obj.SetValue(i, j, ph.y1990);
                            else
                            {
                                obj.Cells[i, j].Formula = "Sum(F" + (1 + IntRow) + ":F" + i + ")";
                                obj.Cells[i, j].CellType = PC;
                            }
                            break;
                        case 6://最大用电负荷

                            if (i != obj.RowCount - 1)
                                obj.SetValue(i, j, ph.y1991);
                            else
                            {
                                obj.Cells[i, j].Formula = "Sum(G" + (IntRow + 1) + ":G" + i + ")";
                                obj.Cells[i, j].CellType = PC;
                            }
                            break;
                        case 7://所属分区

                            if (i != obj.RowCount - 1)
                                obj.SetValue(i, j, ReturnArea(FB, ph.Title));
                            break;
                        case 8://备注
                            if (i != obj.RowCount - 1)
                                obj.SetValue(i, j, ph.Col6);
                            break;
                        default:
                            break;
                    }
                }
                temp++;
            }
            PF.Sheet_GridandCenter(obj);//画边线，居中
        }
        /// <summary>
        /// 查询数据所在分区

        /// </summary>
        /// <param name="FB"></param>
        /// <param name="strTitle"></param>
        /// <returns></returns>
        private string ReturnArea(Itop.Client.Base.FormBase FB,string strTitle)
        {
            string value = null;
            string sql = "a.col4='"+FB.ProjectUID+"'and a.Forecast='2' and a.Title='"+strTitle+"'";
            try
            {
                value =(string) Services.BaseService.GetObject("selectPs_HistoryOfAREA", sql);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return value;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="FB"></param>
        private void SelectData(Itop.Client.Base.FormBase FB)
        {
            string sql = " a.col4='"+FB.ProjectUID+"'and a.Forecast='2' and b.Title='新增用户'";

            try
            {
                list = Services.BaseService.GetList("selectPs_HistoryOfDYH", sql);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
