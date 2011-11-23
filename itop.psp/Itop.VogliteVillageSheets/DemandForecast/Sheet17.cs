using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using Itop.JournalSheet.Function;
using Itop.Domain.Forecast;
using Itop.Client.Common;
using DevExpress.Utils;
//////////////////////////////////////////////////////////////////////////
/*
 *  ClassName：Sheet17
 *  Action：附表17 规划年铜陵县经济发展预测结果表 的数据写入

 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这是个预测表这个表用数据库表ps_forecast_list，Ps_Forecast_Math
 * 年份：2010-10-28
 * 修改时间：2010-11-18
 * 修改者：吕静涛

 */

namespace Itop.VogliteVillageSheets.DemandForecast
{
    class Sheet17
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值

        private int NextColMerge = 1;//合并单元格列初始值

        private string projectID = "";
        private string programID = "";
        private IList list = null;
        private IList AreaList = null;
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
        public void SetSheet_17Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int Temp = 0;
            int IntColCount = 10;
            int IntRowCount = 7 + 2 + 3;//标题占3行，分区类型占2行

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
                        strTitle = " 指 标 名 称   ";
                        break;
                    case 1:
                        strTitle = "2010";
                        break;
                    case 2:
                        strTitle = "2011";
                        break;
                    case 3:
                        strTitle = "2012";
                        break;
                    case 4:
                        strTitle = "2013";
                        break;
                    case 5:
                        strTitle = "2014";
                        break;
                    case 6:
                        strTitle = "2015";
                        break;
                    case 7:
                        strTitle = "“十二五”年均增长率";
                        break;
                    case 8:
                        strTitle = "2020";
                        break;

                    default:
                        break;
                }
                PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
                PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            }
            //行标题

            IntRow = 6;
            for (int i = 0; i < IntRowCount - IntRow; ++i)
            {
                Temp = i + 1;
                strTitle =Temp.ToString();
                PF.CreateSheetView(obj, 1, 1, IntRow +i, 0, strTitle);
                obj.Columns[0].Width = 60;
            }
            IntRow = 6;
            for (int i = 0; i < IntRowCount - IntRow; i++)
            {
                    if (i == 0)
                    {
                        strTitle = "国民生产总值GDP";
                    }
                    if (i == 1)
                    {
                        strTitle = "人均GDP";
                    }
                    if (i == 2)
                    {
                        strTitle = "第一产业比重";
                    }
                    if (i == 3)
                    {
                        strTitle = "第二产业比重";
                    }
                    if (i == 4)
                    {
                        strTitle = "第三产业比重";
                    }
                    if (i == 5)
                    {
                        strTitle = "总人口";
                    }

                    PF.CreateSheetView(obj, 1, 1, (IntRow + (i)), 1, strTitle);
            }

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 6;
            IntCol = 0;

            //PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        private void WriteData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj,int IntRow)
        {
            string strRowTitle = "";
            string strColTitle = "";
            for(int i=IntRow;i<obj.RowCount;++i)
            {
                strRowTitle =(string) PF.ReturnStr(obj, i, 1);
                for(int j=2;j<obj.ColumnCount;++j)
                {
                    strColTitle = (string)obj.GetValue(IntRow-2, j);
                    if(strColTitle=="“十二五”年均增长率")
                    {
                        obj.Cells[i, j].Formula = "POWER(" + PF.GetColumnTitle(j - 1) + (i + 1) + "/" + PF.GetColumnTitle(j - 6) + (i + 1) + ",1/5)-1";
                        obj.Cells[i, j].CellType = PC;
                    }
                    else
                    {
                        obj.SetValue(i, j, ReturnValue(strRowTitle, strColTitle, programID));
                    }
                }
            }
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="strTitle"></param>
        /// <param name="CurrentYear"></param>
        /// <returns></returns>
        private object ReturnValue(string strTitle,string CurrentYear,string ParentID)
        {
            string strTemp = "y" + CurrentYear;
            if (strTitle == "总人口")
            {
                strTitle = "铜陵县总人口";
            }
            string sql = " select " + strTemp + " from Ps_Forecast_Math  where ForecastID='" + ParentID + "' and Forecast='1'"
                                +" and Title='"+strTitle+"'";
            object value = null;
            try
            {
                if (Services.BaseService.GetObject("SelectPFMdoubleWhatever", sql)!=null)
                {
                    value = (double)Services.BaseService.GetObject("SelectPFMdoubleWhatever", sql);
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return value;
        }
        /// <summary>
        /// 选中下拉菜单中的数据连接数据库表,方案
        /// </summary>
        /// <param name="BE"></param>
        public void SelectEditChange(FarPoint.Win.Spread.SheetView tempSheet, object obj, Itop.Client.Base.FormBase FB)
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
                wait = new WaitDialogForm("", "正在加载数据, 请稍候...");
                WriteData(FB, tempSheet, 6);//数据

                PF.Sheet_GridandCenter(tempSheet);//画边线，居中
                m_PF.LockSheets(tempSheet);
                wait.Close();

            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
