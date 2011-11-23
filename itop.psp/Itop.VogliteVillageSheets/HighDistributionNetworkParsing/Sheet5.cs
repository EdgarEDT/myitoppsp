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
 *  ClassName：Sheet5
 *  Action：附表5 截至2009年底铜陵县35kV及以上高压线路基本情况 的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表 PSPDEV表,线路起讫这个列使用线路数据的IName,JName两个字段
 *              IName代表出线接口jName代表进线接口通过这两个字段查找母线，在通过母线SvgUID字段找到
 *              变电站
 * 年份：2010-10-26
 * 修改时间：2010-11-01
 * 修改人：吕静涛
 */

namespace Itop.VogliteVillageSheets.HighDistributionNetworkParsing
{
    class Sheet5
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private string projectID = "";
        private IList list = null;//35kv

        private Itop.JournalSheet.Function.PublicFunction PF = new Itop.JournalSheet.Function.PublicFunction();
        private Itop.VogliteVillageSheets.Function.PublicFunction m_PF = new Itop.VogliteVillageSheets.Function.PublicFunction();
        private FarPoint.Win.Spread.CellType.PercentCellType PC = new FarPoint.Win.Spread.CellType.PercentCellType();
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet_5Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
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

            strTitle = "单位：千伏 万千瓦 万千瓦时   % 小时 公里";
            obj.AddSpanCell(IntRow, 0, 1, obj.Columns.Count);
            obj.SetValue(IntRow, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            //右对齐
            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //列标题
            strTitle = "线路名称";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow += 1, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            for (int i = 0; i < (IntColCount-1); ++i)
            {
                switch (i)
                {
                    case 0:
                        strTitle = "  电压等级  ";
                        break;
                    case 1:
                        strTitle = "  导线型号  ";
                        break;
                    case 2:
                        strTitle = "  投运年限  ";
                        break;
                    case 3:
                        strTitle = "  线路起讫  ";
                        break;
                    case 4:
                        strTitle = "  线路长度  ";
                        break;
                    case 5:
                        strTitle = "  最大允许输送负荷  ";
                        break;
                    case 6:
                        strTitle = "  最大实际输送负荷  ";
                        break;
                    case 7:
                        strTitle = "  最大实际电量  ";
                        break;
                    case 8:
                        strTitle = "  线路负载率  ";
                        break;
                    case 9:
                        strTitle = "  最大负荷利用小时数  ";
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
        /// 写入数据
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="obj"></param>
        /// <param name="strYear"></param>
        public void WriteData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj,string strYear)
        {
            SelectPline(FB, strYear);//查询线路
            //重绘，
            Redraw(FB,obj,strYear);
        }
        /// <summary>
        /// 查询线路
        /// </summary>
        private void SelectPline(Itop.Client.Base.FormBase FB,string strYear)
        {
            string sql = null;
            sql = "where ProjectID='"+FB.ProjectUID+"' and type='05'"
                    + " and DQ!='市辖供电区' and OperationYear<='" + strYear + "' and RateVolt >='35';";
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
        /// 重绘，写入数据
        /// </summary>
        private void Redraw(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string strYear)
        {
            int IntColCount = 11;
            int IntRowCount = 0;
            if(list != null)
            {
                IntRowCount = list.Count + 1 + 2 + 3;//标题占3行，分区类型占2行
            }
            else
            {
                IntRowCount = IntRowCount + 1 + 2 + 3;//标题占3行，分区类型占2行
            }

            obj.Columns.Count = 0;
            obj.Rows.Count = 0;
            obj.Columns.Count = IntColCount;
            obj.Rows.Count = IntRowCount;
            IntCol = obj.Columns.Count;

            PF.Sheet_GridandCenter(obj);//画边线，居中
            m_PF.LockSheets(obj);

            string strTitle = "";
            IntRow = 3;
            strTitle = "附表5 截至" + strYear + "年底铜陵县35kV及以上高压线路基本情况";
            obj.SheetName = strTitle;
            PF.CreateSheetView(obj, IntRow, IntCol, 0, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            IntCol = 1;

            strTitle = "单位：千伏 万千瓦 万千瓦时   % 小时 公里";
            obj.AddSpanCell(IntRow, 0, 1, obj.Columns.Count);
            obj.SetValue(IntRow, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            //右对齐
            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //列标题
            strTitle = "线路名称";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow += 1, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            for (int i = 0; i < (IntColCount - 1); ++i)
            {
                switch (i)
                {
                    case 0:
                        strTitle = "  电压等级  ";
                        break;
                    case 1:
                        strTitle = "  导线型号  ";
                        break;
                    case 2:
                        strTitle = "  投运年限  ";
                        break;
                    case 3:
                        strTitle = "  线路起讫  ";
                        break;
                    case 4:
                        strTitle = "  线路长度  ";
                        break;
                    case 5:
                        strTitle = "  最大允许输送负荷  ";
                        break;
                    case 6:
                        strTitle = "  最大实际输送负荷  ";
                        break;
                    case 7:
                        strTitle = "  最大实际电量  ";
                        break;
                    case 8:
                        strTitle = "  线路负载率  ";
                        break;
                    case 9:
                        strTitle = "  最大负荷利用小时数  ";
                        break;
                }
                PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
                PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            }

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 6;
            IntCol = 0;

            LoadData(FB,obj,IntRow,strYear);
        }
        /// <summary>
        /// 加载查询后的数据
        /// </summary>
        private void LoadData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, int IntRow,string strYear)
        {
            string BDZname = null;
            PSPDEV pv=null;
            int RowCount = 0;
            if(list != null)
            {
                RowCount = list.Count;
            }
            for (int i = 0; i < RowCount; ++i)
            {
                pv=(PSPDEV)list[i];
                for(int j=0;j<obj.ColumnCount;++j)
                {
                    switch(j)
                    {
                        case 0://线路名称 
                            obj.SetValue((i + IntRow), j, pv.Name);
                            break;
                        case 1://电压等级 
                            obj.SetValue((i+IntRow),j,pv.RateVolt);
                            break;
                        case 2://导线型号
                            obj.SetValue((i+IntRow),j,pv.LineType);
                            break;
                        case 3://投运年限
                            obj.SetValue((i+ IntRow), j, pv.OperationYear);
                            break;
                        case 4://线路起讫
                            BDZname=SelectXLQQ(FB, pv.IName, pv.JName,strYear);
                            obj.SetValue((i + IntRow), j, BDZname);
                            break;
                        case 5://线路长度
                            obj.SetValue((i+ IntRow), j, (pv.LineLength+pv.Length2));
                            break;
                        case 6://最大允许输送负荷
                            obj.SetValue((i+IntRow),j,pv.Burthen);
                            break;
                        case 7://最大实际输送负荷
                            //obj.SetValue((i + IntRow), j, pv.Burthen);
                            obj.Cells[i + IntRow, j].Locked = false;
                            break;
                        case 8://最大实际电量
                            obj.Cells[i + IntRow, j].Locked = false;
                            break;
                        case 9://线路负载率
                            obj.Cells[(i + IntRow), j].Formula = "H" + (i + IntRow + 1) + "/G" + (i + IntRow+1);
                            break;
                        case 10://最大负荷利用小时数
                            //obj.Cells[i + IntRow, j].Locked = false;
                            obj.Cells[(i + IntRow), j].Formula = "I" + (i + IntRow + 1) + "/H" + (i + IntRow + 1);
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// 查询线路起讫数据
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="Iname"></param>
        /// <param name="Jname"></param>
        private string  SelectXLQQ(Itop.Client.Base.FormBase FB, string Iname,string Jname,string strYear)
        {
            //找母线关联的变电站id
            string sql = " ProjectID='" + FB.ProjectUID + "' and type='01' and Name='"+Iname+"'"
                                +" and DQ!='市辖供电区'and OperationYear<='"+strYear+"'";
            string sql1 = " ProjectID='" + FB.ProjectUID + "' and type='01' and Name='" + Jname + "'"
                                + " and DQ!='市辖供电区'and OperationYear<='" + strYear + "'";
            string nameI = "";//i测变电站名
            string nameJ = "";//j侧变电站名
            string SvgUIDI="";//i侧变电站id
            string SvgUIDJ="";//j侧变电站id
            try
            {
                SvgUIDI = (string)Services.BaseService.GetObject("SelectPSPDEV_SvgUID", sql);
                SvgUIDJ =(string) Services.BaseService.GetObject("SelectPSPDEV_SvgUID", sql1);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            sql = "  AreaID='"+FB.ProjectUID+"' and UID='"+SvgUIDI+"'";
            sql1 = "  AreaID='" + FB.ProjectUID + "' and UID='" + SvgUIDJ + "'";
            try
            {
                nameI = (string)Services.BaseService.GetObject("SelectPSP_Substation_InfoOFName", sql);
                nameJ = (string)Services.BaseService.GetObject("SelectPSP_Substation_InfoOFName", sql1);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return nameI + "-" + nameJ;
        }
    }
}
