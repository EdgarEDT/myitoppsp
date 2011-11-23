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
 *  ClassName：Sheet7
 *  Action：附表7 截至2009年底铜陵县中压线路联络情况 的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：数据库表psp_substation_info，PSPDEV,先查询线路数据，在查询母线数据
 * 通过母线找到变电站,开闭所类型55，环网柜类型56，分段柜类型61，联络开关类型62，分段开关类型63
 * 支线开关类型64
 * 年份：2010-10-27
 * 修改人：吕静涛
 * 修改日期：2010-11-02
 */


namespace Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing
{
    class Sheet7
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private string projectID = "";
        private IList list = null;
        private IList LineList = null;//线路数据

        private Itop.JournalSheet.Function.PublicFunction PF = new Itop.JournalSheet.Function.PublicFunction();
        private Itop.VogliteVillageSheets.Function.PublicFunction m_PF = new Itop.VogliteVillageSheets.Function.PublicFunction();
        private FarPoint.Win.Spread.CellType.PercentCellType PC = new FarPoint.Win.Spread.CellType.PercentCellType();
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet_7Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int IntColCount = 15;
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

            strTitle = "单位：公里 万千伏安 台";
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
                        strTitle = "   线路类型 ";
                        break;
                    case 4:
                        strTitle = " 主干线径   ";
                        break;
                    case 5:
                        strTitle = " 联络线路   ";
                        break;
                    case 6:
                        strTitle = "  联络线路/所属变电站  ";
                        break;
                    case 7:
                        strTitle = "  联络模式  ";
                        break;
                    case 8:
                        strTitle = "  开闭所  ";
                        break;
                    case 9:
                        strTitle = "  环网柜 ";
                        break;
                    case 10:
                        strTitle = "  分段柜 ";
                        break;

                    case 11:
                        strTitle = "  联络开关  ";
                        break;
                    case 12:
                        strTitle = " 分段开关  ";
                        break;
                    case 13:
                        strTitle = " 支线开关  ";
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
            SelectLine(FB, strEndYear);
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
            int IntColCount = 15;
            int RowCount = 0;
            if(LineList != null)
            {
                RowCount = LineList.Count;
            }
            int IntRowCount = RowCount + 1 + 2 + 3;//标题占3行，分区类型占2行

            obj.RowCount = 0;
            obj.ColumnCount = 0;
            obj.Columns.Count = IntColCount;
            obj.Rows.Count = IntRowCount;
            IntCol = obj.Columns.Count;

            PF.Sheet_GridandCenter(obj);//画边线，居中
            m_PF.LockSheets(obj);

            string strTitle = "附表7 截至" + strEndYear + "年底铜陵县中压线路联络情况";
            IntRow = 3;
            obj.SheetName = strTitle;
            PF.CreateSheetView(obj, IntRow, IntCol, 0, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            IntCol = 1;

            strTitle = "单位：公里 万千伏安 台";
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
                        strTitle = "   线路类型 ";
                        break;
                    case 4:
                        strTitle = " 主干线径   ";
                        break;
                    case 5:
                        strTitle = " 联络线路   ";
                        break;
                    case 6:
                        strTitle = "  联络线路/所属变电站  ";
                        break;
                    case 7:
                        strTitle = "  联络模式  ";
                        break;
                    case 8:
                        strTitle = "  开闭所  ";
                        break;
                    case 9:
                        strTitle = "  环网柜 ";
                        break;
                    case 10:
                        strTitle = "  分段柜 ";
                        break;

                    case 11:
                        strTitle = "  联络开关  ";
                        break;
                    case 12:
                        strTitle = " 分段开关  ";
                        break;
                    case 13:
                        strTitle = " 支线开关  ";
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
            WriteData(FB,obj,strEndYear,IntRow);
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="obj"></param>
        /// <param name="strEndYear"></param>
        private void WriteData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string strEndYear,int IntRow)
        {
            int intTemp = 0;
            string BDZName = "";
            PSPDEV pLine = null;
            int lineRow = 0;
            if(LineList != null)
            {
                lineRow = LineList.Count;
            }
            for (int i = 0; i < lineRow; ++i)
            {
                pLine=(PSPDEV)LineList[i];
                if(pLine.IName !=null||pLine.JName!=null)
                {
                    BDZName=SelectBDZ(FB, pLine.IName,pLine.JName);
                }
                for (int j = 0; j < obj.ColumnCount; ++j)
                {
                    switch (j)
                    {
                        case 0://序号
                            intTemp++;
                            obj.SetValue((IntRow+i),j,intTemp);
                            break;
                        case 1://变电站
                            obj.SetValue((IntRow + i), j, BDZName);
                            break;
                        case 2://线路名称
                            obj.SetValue((IntRow + i), j, pLine.Name);
                            break;
                        case 3://电压等级
                            obj.SetValue((IntRow + i), j, pLine.RateVolt);
                            break;
                        case 4://线路类型
                            if(pLine.LineLength >pLine.Length2 )
                            {
                                obj.SetValue((IntRow + i), j, "架空");
                            }
                            else 
                            {
                                obj.SetValue((IntRow + i), j, "线路");
                            }
                            break;
                        case 5://主干线径
                            
                            break;
                        case 6://联络线路
                            obj.SetValue((IntRow + i), j, ReturnLLName(FB, strEndYear, pLine.SUID));
                            break;
                        case 7:// 联络线路/所属变电站
                            obj.SetValue((IntRow + i), j, ReturnLLOfBDZ_Name(FB, strEndYear, pLine.SUID));
                            break;
                        case 8://联络模式
                            obj.SetValue((IntRow + i), j, pLine.JXFS);
                            break;
                        case 9://开闭所
                                obj.SetValue((IntRow + i), j, SelectCount(FB,strEndYear,pLine.SUID,"55"));
                            break;
                        case 10://环网柜
                            obj.SetValue((IntRow + i), j, SelectCount(FB, strEndYear, pLine.SUID, "56"));
                            break;
                        case 11://分段柜
                            obj.SetValue((IntRow + i), j, SelectCount(FB, strEndYear, pLine.SUID, "61"));
                            break;

                        case 12://联络开关
                            obj.SetValue((IntRow + i), j, SelectCount(FB, strEndYear, pLine.SUID, "62"));
                            break;
                        case 13://分段开关
                            obj.SetValue((IntRow + i), j, SelectCount(FB, strEndYear, pLine.SUID, "63"));
                            break;
                        case 14://支线开关
                            obj.SetValue((IntRow + i), j, SelectCount(FB, strEndYear, pLine.SUID, "64"));
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// 返回联络线名称
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="strYear"></param>
        /// <param name="LineID"></param>
        /// <returns></returns>
        private string ReturnLLName(Itop.Client.Base.FormBase FB,string strYear,string LineID)
        {
            string strTitle = "";
            string sql = "  a.OperationYear<='"+strYear+"'and a.rateVolt='10'"
                              + "  and a.ProjectID='" + FB.ProjectUID + "' and a.type='05' and a.SUID='"+LineID+"'";
            try
            {
                if (Services.BaseService.GetObject("SelectPSPDEV_LLLINEOfName", sql)!=null)
                {
                    strTitle = (string)Services.BaseService.GetObject("SelectPSPDEV_LLLINEOfName", sql);
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return strTitle;
        }
        /// <summary>
        /// 返回联络线路所属变电站的名称
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="strYear"></param>
        /// <param name="LineID"></param>
        /// <returns></returns>
        private string ReturnLLOfBDZ_Name(Itop.Client.Base.FormBase FB, string strYear, string LineID)
        {
            string strTitle = "";
            string sql = "  a.OperationYear<='"+strYear+"'and a.rateVolt='10'"
                              +"  and a.ProjectID='"+FB.ProjectUID+"' and a.type='05' and a.SUID='"+LineID+"'";
            try
            {
                if (Services.BaseService.GetObject("SelectPSPDEV_LLOfBDZ_Name", sql)!=null)
                {
                    strTitle = (string)Services.BaseService.GetObject("SelectPSPDEV_LLOfBDZ_Name", sql);
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return strTitle;
        }
        /// <summary>
        /// 查询开闭所,环网柜，分段柜，联络开关，分段开关，支线开关，数量
        /// </summary>
        /// <param name="LineID">线路id</param>
        /// <param name="strYear">年份</param>
        /// <param name="type">类型</param>
        private int  SelectCount(Itop.Client.Base.FormBase FB, string strYear,string LineID,string type)
        {
            int psp = 0;
            string sql = "select count(*) from pspdev  where ProjectID='" + FB.ProjectUID + "' and type='" + type + "' and AreaID='" + LineID + "'"
                            +" and OperationYear<='" + strYear + "' ";
            try
            {
                if (Services.BaseService.GetObject("SelectPSPDEV_LLLINE", sql)!=null)
                {
                    psp = (int)Services.BaseService.GetObject("SelectPSPDEV_LLLINE", sql);
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return psp;
        }
        /// <summary>
        /// 查询线路数据
        /// </summary>
        private void SelectLine(Itop.Client.Base.FormBase FB, string strYear)
        {
            string sql = " where ProjectID='" + FB.ProjectUID + "' and type='05'"
                                +" and OperationYear<='" + strYear + "'and rateVolt='10'";
            try
            {
                LineList = Services.BaseService.GetList("SelectPSPDEVByCondition", sql);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="list"></param>
        private string  SelectBDZ(Itop.Client.Base.FormBase FB,string Iname,string Jname)
        {
            //通过母线名称查找变电站id
            string SvgUID = "";
            string name=null;
            string sql = " ProjectID='" + FB.ProjectUID + "' and type='01' and ( Name='"+Iname+"' or Name='"+Jname+"')"
                                +"  and RateVolt='10'";
            try
            {
                SvgUID =(string) Services.BaseService.GetObject("SelectPSPDEV_SvgUID", sql);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            sql = "  AreaID='"
                        + FB.ProjectUID + "' and UID='" + SvgUID + "'";
            try
            {
                name = (string)Services.BaseService.GetObject("SelectPSP_Substation_InfoOFName", sql);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return name;
        }
    }
}
