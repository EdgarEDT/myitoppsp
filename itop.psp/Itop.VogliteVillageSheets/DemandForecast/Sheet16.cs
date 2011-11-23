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
 *  ClassName：Sheet16
 *  Action：附表16 铜陵县历年用电量及负荷分区统计 的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：数据库表PSP_Types，PSP_Values
 * 年份：2010-10-28
 */


namespace Itop.VogliteVillageSheets.DemandForecast
{
    class Sheet16
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
        public void SetSheet_16Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            const int rowStep = 4;
            int IntColCount = 13;
            int IntRowCount = 13 + 2 + 3;//标题占3行，分区类型占2行
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

            strTitle = "单位：万千瓦时  万千瓦  %";
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
                        strTitle = " 地区及项目   ";
                        break;
                    case 1:
                        strTitle = " 2000   ";
                        break;
                    case 2:
                        strTitle = " 2001   ";
                        break;
                    case 3:
                        strTitle = " 2002   ";
                        break;
                    case 4:
                        strTitle = " 2003   ";
                        break;
                    case 5:
                        strTitle = " 2004   ";
                        break;
                    case 6:
                        strTitle = " 2005   ";
                        break;
                    case 7:
                        strTitle = " “十五”年均增长率   ";
                        break;
                    case 8:
                        strTitle = " 2006   ";
                        break;
                    case 9:
                        strTitle = " 2007   ";
                        break;
                    case 10:
                        strTitle = " 2008   ";
                        break;
                    case 11:
                        strTitle = " 2009预计   ";
                        break;

                    default:
                        break;
                }
                PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
                PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            }
            //行标题
            IntRow = 5;
            strTitle = "东部地区";
            PF.CreateSheetView(obj, 4, 1, IntRow+=1, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            strTitle = "中部地区";
            PF.CreateSheetView(obj, 4, 1, IntRow += 4, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            strTitle = "南部地区";
            PF.CreateSheetView(obj, 4, 1, IntRow += 4, 0, strTitle);
            obj.Columns[0].Width = 100;
            IntRow = 6;
            for (int i = 0; i < 3 ; i++)
            {
                for (int j = 0; j < rowStep; ++j)
                {
                    if (j == 0)
                    {
                        strTitle = "全社会用电量";
                    }
                    if(j==1)
                    {
                        strTitle = "网供电量";
                    }
                    if (j == 2)
                    {
                        strTitle = "全社会最大负荷";
                    }
                    if (j ==3)
                    {
                        strTitle = "网供最大负荷";
                    }

                    PF.CreateSheetView(obj, 1, 1, (IntRow + (i * rowStep+j)), 1, strTitle);
                }
            }

                NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 6;
            IntCol = 0;

            WriteData(FB, obj, IntRow);
            //PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="obj"></param>
        private void WriteData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj,int IntRow)
        {
            string ID = null;//东部，中部，南部地区id
            string ProID = null;//项目的id号
            string strRowTitleDQ = null;//地区名
            string strRowTitleProject = null;//项目名称
            string strColTitle = null;
            for(int i=IntRow;i<obj.RowCount;++i)
            {
                strRowTitleDQ = PF.ReturnStr(obj, i, 0).ToString();
                strRowTitleProject = obj.GetValue(i, 1).ToString();
                ID = ReturnID(FB, strRowTitleDQ);
                ProID = ReturnProjectID(FB, strRowTitleProject, ID);
                for(int j=2;j<obj.ColumnCount;++j)
                {
                    strColTitle = obj.GetValue(IntRow-2, j).ToString();
                    if (strColTitle == " “十五”年均增长率   ")
                    {
                        obj.Cells[i, j].Formula = "POWER(" + PF.GetColumnTitle(j - 1) + (i + 1) + "/" + PF.GetColumnTitle(j - 6) + (i + 1) + ",1/5)-1";
                        obj.Cells[i, j].CellType = PC;
                    }
                    else
                    {
                        obj.SetValue(i,j,ReturnValue(FB,ProID,strColTitle));
                    }
                }
            }
        }
        /// <summary>
        /// 返回项目id
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="strTitle"></param>
        /// <param name="UpId"></param>
        /// <returns></returns>
        private string ReturnProjectID(Itop.Client.Base.FormBase FB, string strTitle,string UpId)
        {
            string sql = "projectID='"+FB.ProjectUID+"' and Flag2='2' and Title='"+strTitle+"' and parentID='"+UpId+"'";
            string id = null;
            int temp = 0;
            try
            {
                temp = (int)Services.BaseService.GetObject("SelectPSP_TypesOFID", sql);
                id = temp.ToString();
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return id;
        }
        /// <summary>
        /// 返回值
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="strTitle"></param>
        /// <returns></returns>
        private object ReturnValue(Itop.Client.Base.FormBase FB, string strTitleID,string CurrentYear)
        {
            int temp = 0;
            int.TryParse(strTitleID, out temp);
            int tempYear = 0;
            if (CurrentYear == " 2009预计   ")
            {
                CurrentYear = "2009";
            }
            int.TryParse(CurrentYear, out tempYear);
            string sql = "TypeID='" + temp + "' and year='" + tempYear + "'";
            object value = null;
            try
            {
                value = Services.BaseService.GetObject("SelectPSP_ValuesOfValueByWhere", sql);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return value;
        }
        /// <summary>
        /// 返回中部,南部，东部地区的id
        /// </summary>
        /// <returns></returns>
        private string ReturnID(Itop.Client.Base.FormBase FB,string strTitle)
        {
            string sql = " projectID='"+FB.ProjectUID+"' and Flag2='2' and ParentID='0' and Title='铜陵县'";
            string id = null;
            int temp = 0;
            try
            {
                temp = (int)Services.BaseService.GetObject("SelectPSP_TypesOFID", sql);
                id = temp.ToString();
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message );
            }
            sql = " projectID='" + FB.ProjectUID + "' and Flag2='2' and ParentID='"+id+"' and Title='"+strTitle+"'";
            try
            {
                temp = (int)Services.BaseService.GetObject("SelectPSP_TypesOFID", sql);
                id = temp.ToString();
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return id;
        }
    }
}
