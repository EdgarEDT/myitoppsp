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
 *  ClassName：Sheet9
 *  Action：附表9 截至2009年底铜陵县中压配电线路运行情况 的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：查找线路数据通过线路找到变电站,所用数据库表pspdev，PSP_Substation_Info
 * 年份：2010-10-27
 * 修改日期：2010-11-15
 */

namespace Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing
{
    class Sheet9
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
        public void SetSheet_9Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int IntColCount = 11;
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

            strTitle = "单位：千伏 安培 万千瓦 万千伏安 %";
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
                        strTitle = " 变电站   ";
                        break;
                    case 1:
                        strTitle = " 线路名称   ";
                        break;
                    case 2:
                        strTitle = "  电压等级  ";
                        break;
                    case 3:
                        strTitle = "   出口导线型号 ";
                        break;
                    case 4:
                        strTitle = " 线路类型   ";
                        break;
                    case 5:
                        strTitle = " 最大允许电流   ";
                        break;
                    case 6:
                        strTitle = "  线路最大负荷  ";
                        break;
                    case 7:
                        strTitle = "  线路负载率  ";
                        break;
                    case 8:
                        strTitle = "  该线路所带配变总容量  ";
                        break;
                    case 9:
                        strTitle = "  配变综合负载率  ";
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
            selectLine(FB, strEndYear);
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
            int IntColCount = 11;
            int RowCount = 0;
            if(list != null)
            {
                RowCount = list.Count;
            }
            int IntRowCount = RowCount + 1 + 2 + 3;//标题占3行，分区类型占2行
            string title = null;

            obj.Columns.Count = IntColCount;
            obj.Rows.Count = IntRowCount;
            IntCol = obj.Columns.Count;

            PF.Sheet_GridandCenter(obj);//画边线，居中
            m_PF.LockSheets(obj);

            string strTitle = "附表9 截至"+strEndYear+"年底铜陵县中压配电线路运行情况";
            IntRow = 3;
            obj.SheetName = strTitle;
            PF.CreateSheetView(obj, IntRow, IntCol, 0, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            IntCol = 1;

            strTitle = "单位：千伏 安培 万千瓦 万千伏安 %";
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
                        strTitle = " 变电站   ";
                        break;
                    case 1:
                        strTitle = " 线路名称   ";
                        break;
                    case 2:
                        strTitle = "  电压等级  ";
                        break;
                    case 3:
                        strTitle = "   出口导线型号 ";
                        break;
                    case 4:
                        strTitle = " 线路类型   ";
                        break;
                    case 5:
                        strTitle = " 最大允许电流   ";
                        break;
                    case 6:
                        strTitle = "  线路最大负荷  ";
                        break;
                    case 7:
                        strTitle = "  线路负载率  ";
                        break;
                    case 8:
                        strTitle = "  该线路所带配变总容量  ";
                        break;
                    case 9:
                        strTitle = "  配变综合负载率  ";
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
        private void WriteData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string strEndYear,int IntRow)
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
                            obj.SetValue(i,j,SelectBDZ(FB,pdev.SvgUID));
                            break;
                        case 2://线路名称
                            obj.SetValue(i, j, pdev.Name);
                            break;
                        case 3://电压等级
                            obj.SetValue(i, j, pdev.RateVolt);
                            break;
                        case 4://出口导线型号
                            obj.SetValue(i, j, pdev.LineType);
                            break;
                        case 5://线路类型
                            if (pdev.LineLength > pdev.Length2)
                                obj.SetValue(i, j, "架空");
                            if(pdev.Length2>pdev.LineLength )
                                obj.SetValue(i, j, "电缆");
                            break;
                        case 6://最大允许电流
                            obj.SetValue(i, j, pdev.Burthen);
                            break;
                        case 7://线路最大负荷,额定电流*电压*根号3
                            fH =decimal.ToDouble( pdev.Burthen) * pdev.RateVolt*Math.Pow(3,0.5);
                            obj.SetValue(i, j, fH);
                            break;
                        case 8://线路负载率
                            obj.Cells[i, j].Formula = "H"+(i+1)+"/G"+(i+1)+"*100";
                            obj.Cells[i, j].CellType = PC;
                            break;
                        case 9://该线路所带配变总容量
                            obj.SetValue(i,j,ReturnPB(FB,pdev.SUID,strEndYear));
                            break;
                        case 10://配变综合负载率
                            obj.Cells[i, j].Formula = "(H"+(i+1)+"*10*1.732/10000)/J"+(i+1)+"*100";
                            obj.Cells[i, j].CellType = PC;
                            break;
                        default: 
                            break;
                    }
                }
                temp++;
            }
        }
        /// <summary>
        /// 返回配变容量
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private double ReturnPB(Itop.Client.Base.FormBase FB, string id,string strEndYear)
        {
            double temp = 0.0;
            string sql = " ProjectID='"+FB.ProjectUID+"' and type between '50' and '52'"
                                +" and OperationYear<='"+strEndYear+"' and AreaID='"+id+"'";
            try
            {
                temp = (double)Services.BaseService.GetObject("SelectPSPDEV_SUMNum2", sql);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return temp;
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
                MessageBox.Show(e.Message);
            }
            return strTitle;
        }
        /// <summary>
        /// 查找线路数据
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="strEndYear"></param>
        private void selectLine(Itop.Client.Base.FormBase FB, string strEndYear)
        {
            string sql = "where OperationYear<='" + strEndYear + "' and type='05'"
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
    }
}
