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
 *  ClassName：Sheet6
 *  Action：附表6 截至2009年底铜陵县10kV线路基本情况的 数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：数据库表psp_substation_info，PSPDEV,先通过psp_substation_info找到变电站数据
 * 母线通过SvgUID关联到变电站（变电站UID=母线SvgUID），然后母线关联到线路（母线ID=线路Iname或者
 * 母线Id=线路JName）最后线路的SUID=配变的AreaID找到配变数据
 * !!!注意：现在暂时把PSPDEV的IName字段定位出线接口
 * 年份：2010-10-27
 * 修改：2010-11-01
 * 修改人：吕静涛
 */

namespace Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing
{
    class Sheet6
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private string projectID = "";
        private IList[] LineList = null;//线路数据
        private IList BDZList = null;//变电站数据

        private Itop.JournalSheet.Function.PublicFunction PF = new Itop.JournalSheet.Function.PublicFunction();
        private Itop.VogliteVillageSheets.Function.PublicFunction m_PF = new Itop.VogliteVillageSheets.Function.PublicFunction();
        private FarPoint.Win.Spread.CellType.PercentCellType PC = new FarPoint.Win.Spread.CellType.PercentCellType();
       //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet_6Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int IntColCount = 14;
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
                        strTitle = "  出口导线型号  ";
                        break;
                    case 3:
                        strTitle = "   架空线长度 ";
                        break;
                    case 4:
                        strTitle = " 电缆线长度   ";
                        break;
                    case 5:
                        strTitle = " 线路总长度   ";
                        break;
                    case 6:
                        strTitle = "  主干线路长度  ";
                        break;
                    case 11:
                        strTitle = "  配变台数  ";
                        break;
                    case 12:
                        strTitle = " 配变容量  ";
                        break;
                    default:
                        break;
                }
                if(i==7)
                {
                    strTitle = "  公变  ";
                    PF.CreateSheetView(obj, 1, 2, IntRow, IntCol += 1, strTitle);
                    PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
                    PF.CreateSheetView(obj, 1, 1, IntRow + 1, IntCol , "台数");
                }
                else if(i==8)
                {
                    PF.CreateSheetView(obj, 1, 1, IntRow + 1, IntCol += 1, "容量");
                }
                else if(i==9)
                {
                    strTitle = "  专变  ";
                    PF.CreateSheetView(obj, 1, 2, IntRow, IntCol += 1, strTitle);
                    PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
                    PF.CreateSheetView(obj, 1, 1, IntRow + 1, IntCol, "台数");
                }
                else if(i==10)
                {
                    PF.CreateSheetView(obj, 1, 1, IntRow + 1, IntCol += 1, "容量");
                }
                else
                {
                    PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
                    PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
                }
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
        public void SetColumnsTitle(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj,string strEndYear)
        {
            //查询变电站数据
            SelectBDZ(FB);
            SelectLine(FB,strEndYear,BDZList);
            //重绘
            ReDraw(FB,obj,strEndYear);
        }
        /// <summary>
        /// 重绘
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="obj"></param>
        /// <param name="strEndYear"></param>
        private void ReDraw(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string strEndYear)
        {
            int IntColCount = 14;
            int ColCount = 0;
            for(int i=0;i<BDZList.Count;++i)
            {
                ColCount += LineList[i].Count;
            }
            int IntRowCount = ColCount+1 + 2 + 3;//标题占3行，分区类型占2行

            obj.RowCount = 0;
            obj.ColumnCount = 0;
            obj.Columns.Count = IntColCount;
            obj.Rows.Count = IntRowCount;
            IntCol = obj.Columns.Count;

            PF.Sheet_GridandCenter(obj);//画边线，居中
            m_PF.LockSheets(obj);

            string strTitle = "附表6 截至" + strEndYear + "年底铜陵县10kV线路基本情况";
            obj.SheetName = strTitle;
            IntRow = 3;
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
                        strTitle = "  出口导线型号  ";
                        break;
                    case 3:
                        strTitle = "   架空线长度 ";
                        break;
                    case 4:
                        strTitle = " 电缆线长度   ";
                        break;
                    case 5:
                        strTitle = " 线路总长度   ";
                        break;
                    case 6:
                        strTitle = "  主干线路长度  ";
                        break;
                    case 11:
                        strTitle = "  配变台数  ";
                        break;
                    case 12:
                        strTitle = " 配变容量  ";
                        break;
                    default:
                        break;
                }
                if (i == 7)
                {
                    strTitle = "  公变  ";
                    PF.CreateSheetView(obj, 1, 2, IntRow, IntCol += 1, strTitle);
                    PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
                    PF.CreateSheetView(obj, 1, 1, IntRow + 1, IntCol, "台数");
                }
                else if (i == 8)
                {
                    PF.CreateSheetView(obj, 1, 1, IntRow + 1, IntCol += 1, "容量");
                }
                else if (i == 9)
                {
                    strTitle = "  专变  ";
                    PF.CreateSheetView(obj, 1, 2, IntRow, IntCol += 1, strTitle);
                    PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
                    PF.CreateSheetView(obj, 1, 1, IntRow + 1, IntCol, "台数");
                }
                else if (i == 10)
                {
                    PF.CreateSheetView(obj, 1, 1, IntRow + 1, IntCol += 1, "容量");
                }
                else
                {
                    PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
                    PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
                }
            }

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 6;
            IntCol = 0;

            WriteData(FB,obj,IntRow);
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="obj"></param>
        private void WriteData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, int IntRow)
        {
            int intTemp = 0;
            IList list=null;
            PSP_Substation_Info psi=null;//变电站
            PSPDEV pDEV = null;//线路
            PSPDEV PB = null;//配变
            for(int i=0;i<BDZList.Count;++i)
            {
                psi=(PSP_Substation_Info)BDZList[i];//变电站数据
                list=LineList[i];
                for(int j=0;j<LineList[i].Count;++j)
                {
                    pDEV = (PSPDEV)list[j];//线路数据
                    PB =   SelectPB(FB,pDEV.SUID);//配变数据
                    for(int c=0;c<obj.ColumnCount;++c)
                    {
                        switch (c)
                        {
                            case 0://序号
                                intTemp++;
                                obj.SetValue((intTemp-1+IntRow),c,intTemp);
                                break;
                            case 1://变电站
                                obj.SetValue((intTemp - 1 + IntRow), c, psi.Title);
                                break;
                            case 2://线路名称
                                obj.SetValue((intTemp - 1 + IntRow), c, pDEV.Name);
                                break;
                            case 3://出口导线型号
                                obj.SetValue((intTemp - 1 + IntRow), c, pDEV.LineType);
                                break;
                            case 4://架空线长度
                                obj.SetValue((intTemp-1+IntRow),c,pDEV.LineLength);
                                break;
                            case 5://电缆线长度
                                obj.SetValue((intTemp - 1 + IntRow), c, pDEV.Length2);
                                break;
                            case 6://线路总长度
                                obj.SetValue((intTemp - 1 + IntRow), c, (pDEV.LineLength+pDEV.Length2));
                                break;
                            case 7://主干线路长度
                                obj.SetValue((intTemp - 1 + IntRow), c, (pDEV.LineLength + pDEV.Length2));
                                break;
                            case 8://公变/台数
                                if(psi.S4=="公用")
                                {
                                    obj.SetValue((intTemp - 1 + IntRow), c, (psi.L3));
                                }
                                break;
                            case 9://公变/容量
                                if (psi.S4 == "公用")
                                {
                                    obj.SetValue((intTemp - 1 + IntRow), c, (psi.L2));
                                }
                                break;
                            case 10://专变/台数
                                if (psi.S4 == "专用")
                                {
                                    obj.SetValue((intTemp - 1 + IntRow), c, (psi.L3));
                                }
                                break;
                            case 11://专变/容量
                                if (psi.S4 == "专用")
                                {
                                    obj.SetValue((intTemp - 1 + IntRow), c, (psi.L2));
                                }
                                break;

                            case 12://配变台数
                                if(PB!=null)
                                    obj.SetValue((intTemp - 1 + IntRow), c,PB.Flag);
                                break;
                            case 13://配变容量
                                if(PB!=null)
                                    obj.SetValue((intTemp - 1 + IntRow), c, PB.Num2);
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 配变数据查询
        /// </summary>
        private PSPDEV  SelectPB(Itop.Client.Base.FormBase FB,string LineID)
        {
            PSPDEV pb = null;
            string sql = "  where ProjectID='" + FB.ProjectUID + "' and type='50' and AreaID='" + LineID + "'";
            try
            {
                pb =(PSPDEV) Services.BaseService.GetObject("SelectPSPDEVByCondition", sql);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return pb;
        }
        /// <summary>
        /// 查询变电站数据
        /// </summary>
        /// <param name="FB"></param>
        private void SelectBDZ(Itop.Client.Base.FormBase FB)
        {
            string sql = "  l1='10' and AreaID="
                                +"'" + FB.ProjectUID + "'";
            try
            {
                BDZList = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", sql);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 查询线路数据(每个变电站接线出口包含几条线路)
        /// </summary>
        /// <param name="FB"></param>
        private void SelectLine(Itop.Client.Base.FormBase FB,string strYear,IList  list)
        {
            PSP_Substation_Info  ps = null;
            string sql = null;
            string name = null;
            LineList = new IList[list.Count];
            for(int i=0;i<list.Count;++i)
            {
                ps = (PSP_Substation_Info)list[i];
                //查找母线名称
                sql = "  ProjectID='" + FB.ProjectUID + "' and type='01' and SvgUID='" + ps.UID + "'";
                try
                {
                    name = (string)Services.BaseService.GetObject("SelectPSPDEV_MXNAME",sql);
                }
                catch (System.Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                sql = " where ProjectID='" + FB.ProjectUID + "' and type='05' and (IName='" + name + "' or JName='"+name+"')"
                                    + "and  OperationYear<='" + strYear + "'and rateVolt='10'";
                try
                {
                    LineList[i] = Services.BaseService.GetList("SelectPSPDEVByCondition", sql);
                }
                catch (System.Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
        /// <summary>
        /// 给下拉列表添加年份
        /// </summary>
        public void InitYear(DevExpress.XtraBars.BarEditItem BE)
        {
            int FirstYear = 1990;
            int EndYear = 2060;
            for (int i = FirstYear; i <= EndYear; i++)
            {
                ((DevExpress.XtraEditors.Repository.RepositoryItemComboBox)BE.Edit).Items.Add(FirstYear.ToString());
                FirstYear++;
            }
        }
    }
}
