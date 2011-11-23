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
 *  ClassName：Sheet12
 *  Action：附表12 截至2009年底铜陵县电网无功补偿容量统计表 的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：所用数据库表pspdev，PSP_Substation_Info,这个表用并联电容器数据Type=09
 * 年份：2010-10-27
 * 修改时间：2010-11-15
 */

namespace Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing
{
    class Sheet12
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
        public void SetSheet_12Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int IntColCount = 6;
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

            strTitle = "单位：千伏   千乏";
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
                        strTitle = " 安装地点   ";
                        break;
                    case 1:
                        strTitle = " 电压   ";
                        break;
                    case 2:
                        strTitle = "  电容器型号  ";
                        break;
                    case 3:
                        strTitle = "  容   量 ";
                        break;
                    case 4:
                        strTitle = " 备   注   ";
                        break;
                    default:
                        break;
                }
                PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
                PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            }

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;
            //PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 根据下拉菜单从新打印报表
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="obj"></param>
        /// <param name="strEndYear"></param>
        public void SetColumnsTitle(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string strEndYear)
        {
            SelectedBDZ(FB, strEndYear);
            //重绘
            ReDraw(FB, obj, strEndYear);
        }
        /// <summary>
        /// 重绘
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="obj"></param>
        /// <param name="strEndYear"></param>
        private void ReDraw(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string strEndYear)
        {
            obj.RowCount = 0;
            obj.ColumnCount = 0;
            int IntColCount = 6;
            int IntRowCount = list.Count+1 + 2 + 3;//标题占3行，分区类型占2行
            string title = null;

            obj.Columns.Count = IntColCount;
            obj.Rows.Count = IntRowCount;
            IntCol = obj.Columns.Count;

            PF.Sheet_GridandCenter(obj);//画边线，居中
            m_PF.LockSheets(obj);

            string strTitle = "附表12 截至"+strEndYear+"年底铜陵县电网无功补偿容量统计表";
            IntRow = 3;
            obj.SheetName = strTitle;
            PF.CreateSheetView(obj, IntRow, IntCol, 0, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            IntCol = 1;

            strTitle = "单位：千伏   千乏";
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
                        strTitle = " 安装地点   ";
                        break;
                    case 1:
                        strTitle = " 电压   ";
                        break;
                    case 2:
                        strTitle = "  电容器型号  ";
                        break;
                    case 3:
                        strTitle = "  容   量 ";
                        break;
                    case 4:
                        strTitle = " 备   注   ";
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
            WriteData(FB, obj, strEndYear, IntRow);
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="obj"></param>
        /// <param name="strEndYear"></param>
        /// <param name="IntRow"></param>
        private void WriteData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string strEndYear,int IntRow)
        {
            PSPDEV pdev = null;
            PSP_Substation_Info psi=null;
            int Temp = 1;
            for(int i=IntRow;i<obj.RowCount;++i)
            {
                psi=(PSP_Substation_Info)list[i-IntRow];
                pdev = SelectedDRQ(FB, psi.UID,strEndYear);
                for(int j=0;j<obj.ColumnCount;++j)
                {
                    switch(j)
                    {
                        case 0://序号
                            obj.SetValue(i, j, Temp);
                            break;
                        case 1://安装地点
                            obj.SetValue(i, j, psi.Title);
                            break;
                        case 2://电压
                            obj.SetValue(i, j, psi.L1);
                            break;
                        case 3://电容器型号
                            if(pdev!=null)
                            {
                                obj.SetValue(i, j, pdev.LineType);
                            }
                            break;
                        case 4://容量
                            if (pdev != null)
                            {
                                obj.SetValue(i, j, pdev.Burthen);
                            }
                            break;
                        case 5://备注
                            obj.Cells[i,j].Locked =false;
                            break;
                        default:
                            break;
                    }
                }
                Temp++;
            }
        }
        /// <summary>
        /// 查询并联电容器数据
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private PSPDEV SelectedDRQ(Itop.Client.Base.FormBase FB,string id,string strEndYear)
        {
            PSPDEV pdev = null;
            string sql = "  where ProjectID='"+FB.ProjectUID+"' and Type='09'and OperationYear<='"+strEndYear+"' and Iname="
                             +"   (select Name from PSPDEV where ProjectID='"+FB.ProjectUID+"' and type='01' "
                              +"  and SvgUID='"+id+"' and OperationYear<='"+strEndYear+"')";
            try
            {
                pdev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", sql);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return pdev;
        }
        /// <summary>
        /// 查询变电站数据
        /// </summary>
        /// <param name="FB"></param>
        private void SelectedBDZ(Itop.Client.Base.FormBase FB,string strEndYear)
        {
            string sql = " AreaID='"+FB.ProjectUID+"' and L1='10' and S2<='"+strEndYear+"'";
            try
            {
                list = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", sql);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
