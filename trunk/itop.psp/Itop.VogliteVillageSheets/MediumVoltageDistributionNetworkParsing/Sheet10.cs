using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using Itop.JournalSheet.Function;
using Itop.Domain.Forecast;
using Itop.Client.Common;
using Itop.Domain.Graphics;
//////////////////////////////////////////////////////////////////////////
/*
 *  ClassName：Sheet10
 *  Action：附表10 铜陵县中压线路“N-1”分析 的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表需要更新数据数据库表是：pspdev，PSP_Substation_Info
 * 年份：2010-10-27
 * 修改时间：2010-11-15
 */

namespace Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing
{
    class Sheet10
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
        public void SetSheet_10Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            selectLine(FB);
            int IntColCount = 8;
            int RowCount = 0;
            if(list != null)
            {
                RowCount = list.Count;
            }
            int IntRowCount = RowCount+1 + 2 + 3;//标题占3行，分区类型占2行
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

            strTitle = "单位：万千瓦";
            obj.AddSpanCell(IntRow, 0, 1, obj.Columns.Count);
            obj.SetValue(IntRow, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            //右对齐
            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //列标题
            strTitle = "序号";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow += 1, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            for (int i = 0; i < (IntColCount - 3); ++i)
            {
                switch (i)
                {
                    case 0:
                        strTitle = " 变电站   ";
                        break;
                    case 1:
                        strTitle = " 线路名称   ";
                        break;
                    case 2:
                        strTitle = "  电压等级  ";
                        break;
                    case 3:
                        strTitle = "   线路负荷 ";
                        break;
                    case 4:
                        strTitle = " 联络模式   ";
                        break;
                    case 5:
                        strTitle = "  联络线路  ";
                        break;
                    default:
                        break;
                }
                PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
                PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            }
            strTitle="线路N－1";
            PF.CreateSheetView(obj, 1, 2, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            strTitle = "损失负荷";
            PF.CreateSheetView(obj, 1, 1, IntRow+1, IntCol , strTitle);
            strTitle = "校验结果";
            PF.CreateSheetView(obj, 1, 1, IntRow + 1, IntCol+1, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 6;
            IntCol = 0;
            //PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
            WriteData(FB, obj, IntRow);
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        private void WriteData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj,int IntRow)
        {
            PSPDEV pdev = null;
            int temp = 1;
            double fH = 0.0;
            for (int i = IntRow; i < obj.RowCount; ++i)
            {
                pdev = (PSPDEV)list[i - IntRow];
                for(int j=0;j<obj.ColumnCount;++j)
                {
                    switch (j)
                    {
                        case 0://序号
                            obj.SetValue(i, j, temp);
                            break;
                        case 1://变电站
                            obj.SetValue(i, j, SelectBDZ(FB, pdev.SvgUID));
                            break;
                        case 2://线路名称
                            obj.SetValue(i, j, pdev.Name);
                            break;
                        case 3://线路负荷
                            fH = decimal.ToDouble(pdev.Burthen) * pdev.RateVolt * Math.Pow(3, 0.5);
                            obj.SetValue(i, j, fH);
                            break;
                        case 4://联络模式
                            obj.SetValue(i, j, pdev.LLFS);
                            break;
                        case 5://联络线路
                            obj.Cells[i, j].Locked = false;
                            break;
                        case 6://损失负荷
                            obj.Cells[i, j].Locked = false;
                            break;
                        case 7://校验结果
                            obj.Cells[i, j].Formula = "F"+(i+1)+"-1";
                            break;

                        default:
                            break;
                    }
                }
                temp++;
            }
        }
        /// <summary>
        /// 查找线路数据
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="strEndYear"></param>
        private void selectLine(Itop.Client.Base.FormBase FB)
        {
            string sql = "where  type='05'"
                            + " and RateVolt='10'and ProjectID='" + FB.ProjectUID + "'";
            try
            {
                list = Services.BaseService.GetList("SelectPSPDEVByCondition", sql);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 返回变电站名称
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private string SelectBDZ(Itop.Client.Base.FormBase FB, string id)
        {
            string sql = " UID='" + id + "' and AreaID='" + FB.ProjectUID + "'";
            string strTitle = "";
            try
            {
                strTitle = (string)Services.BaseService.GetObject("SelectPSP_Substation_InfoOFName", sql);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return strTitle;
        }
    }
}
