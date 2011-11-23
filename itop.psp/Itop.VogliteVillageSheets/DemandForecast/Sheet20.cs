using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using Itop.JournalSheet.Function;
using Itop.Domain.Forecast;
using Itop.Client.Common;
using Itop.Client.Stutistics;
//////////////////////////////////////////////////////////////////////////
/*
 *  ClassName：Sheet20
 *  Action：附表20 规划年铜陵县规划装机进度表 的数据写入

 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表psp_powerSubstationInfo，项目名称是：Title，项目类型：s10
 *              容量：s2，接入电压等级：s1，投产年份：s3
 * 年份：2010-10-29
 */

namespace Itop.VogliteVillageSheets.DemandForecast
{
    class Sheet20
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
        public void SetSheet_20Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            SelectedPower(FB);
            int Temp = 0;
            int IntColCount = 11;
            int IntRowCount = list.Count+1 + 2 + 3;//标题占3行，分区类型占2行

            int BringIntoPproductionTime = 6;//投产时间的列数

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

            strTitle = "  单位：万千瓦  千伏";
            obj.AddSpanCell(IntRow, 0, 1, obj.Columns.Count);
            obj.SetValue(IntRow, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            //右对齐

            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //列标题

            IntRow = 4;
            for (int j = 0; j < obj.ColumnCount - BringIntoPproductionTime; ++j)
            {
                switch (j)
                {
                    case 0:
                        strTitle = "序     号";
                	    break;
                    case 1:
                        strTitle = "项目名称";
                        break;
                    case 2:
                        strTitle = "类     型";
                        break;
                    case 3:
                        strTitle = "容     量";
                        break;
                    case 4:
                        strTitle = "接入电压等级";
                        break;

                }
                PF.CreateSheetView(obj, 2, 1, IntRow, j, strTitle);
                PF.SetSheetViewColumnsWidth(obj, j, strTitle);
            }
            strTitle = "投产时间";
            PF.CreateSheetView(obj, 1, BringIntoPproductionTime, IntRow, 5, strTitle);
            for (int i = 0; i < BringIntoPproductionTime;++i )
            {
                Temp = 2010 + i;
                strTitle = Temp.ToString();
                PF.CreateSheetView(obj, 1, 1, IntRow+1, 5+i, strTitle);
                PF.SetSheetViewColumnsWidth(obj, 5+i, strTitle);
            }

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 6;
            IntCol = 0;

            WriteData(obj, IntRow);
            //PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        private void WriteData(FarPoint.Win.Spread.SheetView obj, int IntRow)
        {
            PSP_PowerSubstationInfo ppi = null;
            for(int i=IntRow;i<(list.Count+IntRow);++i)
            {
                ppi = (PSP_PowerSubstationInfo)list[i - IntRow];
                for(int j=0;j<obj.ColumnCount;++j)
                {
                    switch (j)
                    {
                    case  0://序号
                            obj.SetValue(i,j,(i-IntRow+1));
                    	break;
                    case 1://项目名称
                        obj.SetValue(i, j, ppi.Title);
                        break;
                    case 2://类型
                        obj.SetValue(i, j, ppi.S10);
                        break;
                    case 3://容量
                        obj.SetValue(i, j, ppi.S2);
                        break;
                    case 4://接入电压等级
                        obj.SetValue(i, j, ppi.S1);
                        break;
                    case 5://投产时间,2010
                            if(ppi.S3=="2010")
                            {
                                obj.SetValue(i, j, "是");
                            }
                        break;
                    case 6://投产时间,2011
                        if (ppi.S3 == "2011")
                        {
                            obj.SetValue(i, j, "是");
                        }
                        break;
                    case 7://投产时间,2012
                        if (ppi.S3 == "2012")
                        {
                            obj.SetValue(i, j, "是");
                        }
                        break;
                    case 8://投产时间,2013
                        if (ppi.S3 == "2013")
                        {
                            obj.SetValue(i, j, "是");
                        }
                        break;
                    case 9://投产时间,2014
                        if (ppi.S3 == "2014")
                        {
                            obj.SetValue(i, j, "是");
                        }
                        break;
                    case 10://投产时间,2015
                        if (ppi.S3 == "2015")
                        {
                            obj.SetValue(i, j, "是");
                        }
                        break;

                    default:
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 查找电源数据
        /// </summary>
        /// <param name="FB"></param>
        private void SelectedPower(Itop.Client.Base.FormBase FB)
        {
            string sql = " AreaID='"+FB.ProjectUID+"'and s3 between '2010' and '2015'";
            try
            {
                list = Services.BaseService.GetList("SelectPSP_PowerSubstationInfoByConn", sql);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
