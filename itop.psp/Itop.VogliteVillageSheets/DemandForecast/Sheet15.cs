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
 *  ClassName：Sheet15
 *  Action：铜陵县分行业用电历史实绩统计表 的数据写入

 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：表用数据库表ps_History,这个表不用更新

 * 年份：2010-10-28
 * 修改人：吕静涛

 * 修改日期：2010-11-04
 */


namespace Itop.VogliteVillageSheets.DemandForecast
{
    class Sheet15
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
        public void SetSheet_15Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int IntColCount = 13;
            int IntRowCount = 25 + 2 + 3;//标题占3行，分区类型占2行

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

            strTitle = "单位：万千瓦时";
            obj.AddSpanCell(IntRow, 0, 1, obj.Columns.Count);
            obj.SetValue(IntRow, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            //右对齐

            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //列标题

            strTitle = "序号";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow += 1, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 项     目   ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //行标题

            for (int j = 0; j < 2; ++j)
            {
                IntRow = 5;
                if (j == 0)
                {
                    strTitle = "一";
                }
                else
                {
                    strTitle = "全社会用电总计";
                }
                PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, strTitle);
                PF.SetSheetViewColumnsWidth(obj, j, strTitle);

                if(j==0)
                {
                    strTitle = "A";
                }
                else
                {
                    strTitle = "全行业用电合计";
                }
                PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, strTitle);
                PF.SetSheetViewColumnsWidth(obj, j, strTitle);
                if(j==0)
                {
                    strTitle = "1";
                }
                else
                {
                    strTitle = "第一产业";
                }
                PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, strTitle);
                PF.SetSheetViewColumnsWidth(obj, j, strTitle);
                if(j==0)
                {
                    strTitle = "2";
                }
                else
                {
                    strTitle = "第二产业";
                }
                PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, strTitle);
                PF.SetSheetViewColumnsWidth(obj, j, strTitle);
                if(j==0)
                {
                    strTitle = "3";
                }
                else
                {
                    strTitle = "第三产业";
                }
                PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, strTitle);
                PF.SetSheetViewColumnsWidth(obj, j, strTitle);
                if(j==0)
                {
                    strTitle = "B";
                }
                else
                {
                    strTitle = "居民生活用电合计";
                }
                PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, strTitle);
                PF.SetSheetViewColumnsWidth(obj, j, strTitle);
                if(j==0)
                {
                    strTitle = "二";
                }
                else
                {
                    strTitle = "全行业用电分类";
                }
                PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, strTitle);
                PF.SetSheetViewColumnsWidth(obj, j, strTitle);
                if(j==0)
                {
                    strTitle = "1";
                    PF.CreateSheetView(obj, 2, 1, (IntRow += 1), j, strTitle);
                    PF.SetSheetViewColumnsWidth(obj, j, strTitle);
                }
                else
                {
                    PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, "农、林、牧、渔业");
                    PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, "所占百分比（%）");
                }
                if(j==0)
                {
                    strTitle = "2";
                    PF.CreateSheetView(obj, 2, 1, (IntRow += 2), j, strTitle);
                    PF.SetSheetViewColumnsWidth(obj, j, strTitle);
                }
                else
                {
                    PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, "工业");
                    PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, "所占百分比（%）");
                }
                if(j==0)
                {
                    strTitle = "3";
                    PF.CreateSheetView(obj, 2, 1, (IntRow += 2), j, strTitle);
                    PF.SetSheetViewColumnsWidth(obj, j, strTitle);
                }
                else
                {
                    PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, "建筑业");
                    PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, "所占百分比（%）");
                }
                if(j==0)
                {
                    strTitle = "4";
                    PF.CreateSheetView(obj, 2, 1, (IntRow += 2), j, strTitle);
                    PF.SetSheetViewColumnsWidth(obj, j, strTitle);
                }
                else
                {
                    PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, "交通运输、仓储、邮政业");
                    PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, "所占百分比（%）");
                }
                if(j==0)
                {
                    strTitle = "5";
                    PF.CreateSheetView(obj, 2, 1, (IntRow += 2), 0, strTitle);
                    PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
                }
                else
                {
                    PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, "信息传输、计算机服务和软件业");
                    PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, "所占百分比（%）");
                }
                if(j==0)
                {
                    strTitle = "6";
                    PF.CreateSheetView(obj, 2, 1, (IntRow += 2), 0, strTitle);
                    PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
                }
                else
                {
                    PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, "商业、住宿和餐饮业");
                    PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, "所占百分比（%）");
                }
                if(j==0)
                {
                    strTitle = "7";
                    PF.CreateSheetView(obj, 2, 1, (IntRow += 2), j, strTitle);
                    PF.SetSheetViewColumnsWidth(obj, j, strTitle);
                }
                else
                {
                    PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, "金融、房地产、商务及居民服务业");
                    PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, "所占百分比（%）");
                }
                if(j==0)
                {
                    strTitle = "8";
                    PF.CreateSheetView(obj, 2, 1, (IntRow += 2), j, strTitle);
                    PF.SetSheetViewColumnsWidth(obj, j, strTitle);
                }
                else
                {
                    PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, "公共事业及管理组织");
                    PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, "所占百分比（%）");
                }
                if(j==0)
                {
                    strTitle = "9";
                    PF.CreateSheetView(obj, 1, 1, (IntRow += 2), j, strTitle);
                    obj.Columns[0].Width = 60;
                }
                else
                {
                    strTitle = "合计";
                    PF.CreateSheetView(obj, 1, 1, (IntRow += 1), j, strTitle);
                    obj.Columns[1].Width = 200;
                }
            }

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 4;
            IntCol = 0;

            SetColtitle(obj, IntRow);
            WriteData(FB,obj, (IntRow+2));
            //PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 写入列标题

        /// </summary>
        /// <param name="obj"></param>
        private void SetColtitle(FarPoint.Win.Spread.SheetView obj,int IntRow)
        {
            int intTitle = 2000;
            int temp = 0;
            string strTitle = "“十五”年均增长率";
            for (int j = 2; j < obj.ColumnCount; ++j)
            {
                if(j==8)
                {

                    obj.SetValue(IntRow, j, strTitle);
                    obj.SetColumnWidth(j,(strTitle.Length*13));
                }
                else
                {
                    obj.SetValue(IntRow, j, intTitle);
                    intTitle++;
                }
                obj.AddSpanCell(IntRow, j, 2, 1);
            }
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="obj"></param>
        private void WriteData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, int IntRow)
        {
            string strRowTitle = null;
            string strColTitle = null;
            Ps_History ps1 = null;//全社会用电总计
            Ps_History ps2 = null;//全行业用电合计

            Ps_History ps3 = null;//全行业用电分类

            ps1=ReturnStrID(FB, "全社会用电量（亿kWh）");//全社会用电总计
            ps2 = ReturnStrID(FB, "全行业用电合计");//全行业用电合计

            ps3 = ReturnStrID(FB, "全行业用电分类");//全行业用电分类

            for (int i = IntRow; i < obj.RowCount; ++i)
            {
                strRowTitle = PF.ReturnStr(obj, (i), 1).ToString();
                for (int j = 2; j < obj.ColumnCount; ++j)
                {
                    strColTitle = obj.GetValue((4), j).ToString();
                    if (strColTitle == "“十五”年均增长率" || strColTitle == "“十一五”年均增长率" || strColTitle == "“十二五”年均增长率" || strColTitle == "“十三五”年均增长率")
                    {
                        if (strColTitle == "“十五”年均增长率")
                        {
                            if (strRowTitle != "所占百分比（%）")
                            {
                                obj.Cells[i, j].Formula = "POWER(" + PF.GetColumnTitle(j - 1) + (i + 1) + "/" + PF.GetColumnTitle(j - 6) + (i + 1) + ",1/5)-1";
                            }
                        }
                        else
                        {
                            obj.Cells[i, j].Formula = "POWER(" + PF.GetColumnTitle(j - 1) + (i + 1) + "/" + PF.GetColumnTitle(j - 5) + (i + 1) + ",1/5)-1";
                        }
                        obj.Cells[i, j].CellType = PC;//%
                    }
                    else
                    {
                        if (strRowTitle == "所占百分比（%）")
                        {
                            obj.Cells[i, j].Formula = PF.GetColumnTitle(j) + (i) + "/" + PF.GetColumnTitle(j) + (8);
                            obj.Cells[i, j].CellType = PC;//%
                        }
                        else if(strRowTitle=="合计")
                        {
                            obj.Cells[i, j].Formula = "SUM("+PF.GetColumnTitle(j) + (14) + "," + PF.GetColumnTitle(j) + (16)
                                                                    + "," + PF.GetColumnTitle(j) + (18) + "," + PF.GetColumnTitle(j) + (20)
                                                                    + "," + PF.GetColumnTitle(j) + (22) + "," + PF.GetColumnTitle(j) + (24)
                                                                    + "," + PF.GetColumnTitle(j) + (26) + "," + PF.GetColumnTitle(j) + (28) + ")";
                        }
                        else if (strRowTitle == "公共事业及管理组织")
                        {
                            obj.Cells[i, j].Formula = PF.GetColumnTitle(j) + (8) + "-" + PF.GetColumnTitle(j) + (14)
                                                                    + "-" + PF.GetColumnTitle(j) + (16) + "-" + PF.GetColumnTitle(j) + (18)
                                                                    + "-" + PF.GetColumnTitle(j) + (20) + "-" + PF.GetColumnTitle(j) + (22)
                                                                    + "-" + PF.GetColumnTitle(j) + (24) + "-" + PF.GetColumnTitle(j) + (26) ;
                        }
                        else
                        {
                            obj.SetValue(i, j, SelectCurrentData(FB, strColTitle, strRowTitle,ps1,ps2,ps3));
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
        private object SelectCurrentData(Itop.Client.Base.FormBase FB, string CurrentYear, string RowTitle, Ps_History ps1, Ps_History ps2, Ps_History ps3)
        {
            string sql = null;
            object value = 0;
            string strTemp = "y" + CurrentYear;
            Ps_History ph=null;
            if (RowTitle == "全社会用电总计")
            {
                RowTitle = "全社会用电量（亿kWh）";
            }
            if (RowTitle == "全行业用电合计" || RowTitle == "居民生活用电合计")
            {
                sql = " select " + strTemp + " from ps_History  where Title='" + RowTitle + "' and col4='" + FB.ProjectUID + "' and ParentID='"+ps1.ID+"'";
            }
            else if (RowTitle == "第一产业" || RowTitle == "第二产业" || RowTitle == "第三产业")
            {
                sql = " select " + strTemp + " from ps_History  where Title='" + RowTitle + "' and col4='" + FB.ProjectUID + "' and ParentID='" + ps2.ID + "'";
            }
            else if (RowTitle == "农、林、牧、渔业" || RowTitle == "工业" || RowTitle == "建筑业" ||
                       RowTitle == "交通运输、仓储、邮政业" || RowTitle == "信息传输、计算机服务和软件业"
              || RowTitle == "商业、住宿和餐饮业" || RowTitle == "金融、房地产、商务及居民服务业"
           || RowTitle == "公共事业及管理组织")
            {
                sql = " select " + strTemp + " from ps_History  where Title='" + RowTitle + "' and col4='" + FB.ProjectUID + "' and ParentID='" + ps3.ID + "'";
            }
            else
            {
                sql = " select " + strTemp + " from ps_History  where Title='" + RowTitle + "' and col4='" + FB.ProjectUID + "'";
            }
            try
            {
                value = (double)Services.BaseService.GetObject("SelectPs_HistoryPopulationByCondition", sql);
            }
            catch (Exception e)
            {
                MessageBox.Show("错误，错误原因：" + e.Message, "提示错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return value;
        }
        /// <summary>
        /// 返回上级目录的id
        /// </summary>
        /// <returns></returns>
        private Ps_History  ReturnStrID(Itop.Client.Base.FormBase FB,string strTitle)
        {
            string sql = "col4='"+FB.ProjectUID+"' and Title='"+strTitle+"'";
            Ps_History ph = null;
            try
            {
                ph = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", sql);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return ph;
        }
    }
}
