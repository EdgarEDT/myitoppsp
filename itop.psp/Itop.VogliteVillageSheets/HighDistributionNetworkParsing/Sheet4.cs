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
 *  ClassName：Sheet4
 *  Action：附表4 截至2009年底铜陵县110（35）kV变电站概况 的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表 PSP_Substation_Info,
 * 年份：2010-10-26
 */
namespace Itop.VogliteVillageSheets.HighDistributionNetworkParsing
{
    class Sheet4
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private string projectID = "";
        private IList list_110 = null;//110kv
        private IList list_35 = null;//35kv

        private Itop.JournalSheet.Function.PublicFunction PF = new Itop.JournalSheet.Function.PublicFunction();
        private Itop.VogliteVillageSheets.Function.PublicFunction m_PF = new Itop.VogliteVillageSheets.Function.PublicFunction();
        private FarPoint.Win.Spread.CellType.PercentCellType PC = new FarPoint.Win.Spread.CellType.PercentCellType();
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet_4Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int IntColCount = 16;
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

            strTitle = "单位：万千伏安 万千瓦  % ";
            obj.AddSpanCell(IntRow, 0, 1, obj.Columns.Count);
            obj.SetValue(IntRow, 0, strTitle);
            //右对齐
            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //列标题
            strTitle = "电压等级";
            PF.CreateSheetView(obj, NextRowMerge+=1, NextColMerge, IntRow+=1 , IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            for (int i = 0; i < 15; ++i)
            {
                switch (i)
                {
                    case 0:
                    strTitle = "变电站名称";
                    break;
                    case 1:
                        strTitle = "主变容量构成";
                        break;
                    case 2:
                        strTitle = "主变容量";
                        break;
                    case 3:
                        strTitle = "电压变比";
                        break;
                    case 4:
                        strTitle = "变电站高压侧主接线模式";
                        break;
                    case 5:
                        strTitle = "变电站中压侧主接线模式";
                        break;
                    case 6:
                        strTitle = "变电站低压侧主接线模式";
                        break;
                    case 7:
                        strTitle = "110kV出线回路";
                        break;
                    case 8:
                        strTitle = "35kV出线回路";
                        break;
                    case 9:
                        strTitle = "10kV出线回路";
                        break;
                    case 10:
                        strTitle = "无功补偿容量";
                        break;
                    case 11:
                        strTitle = "投运日期(年.月)";
                        break;
                    case 12:
                        strTitle = "所属分区";
                        break;
                    case 13:
                        strTitle = "2009年最大负荷";
                        break;
                    case 14:
                        strTitle = "负载率";
                        break;
                }
                PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
                PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            }
            //行标题
            strTitle = "110kV";
            PF.CreateSheetView(obj, NextRowMerge-=1, NextColMerge, IntRow+=2, IntCol -= 15, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            for (int i = 0; i < 4; ++i)
            {
                switch (i)
                {
                case 0:
                    strTitle = "小计";
                    break;
                case 1:
                    strTitle = "35kV";
                    break;
                case 2:
                    strTitle = "小计";
                    break;
                case 3:
                    strTitle = "合计";
                    break;

            }
                PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow+=1, IntCol, strTitle);
                //PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
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
        public void WriteData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj,string strYear)
        {
            //查询110,53有几条数据
            SelectData(FB,strYear);
            //重新绘制左侧标题,和数据
            ReDraw(FB,obj,strYear);
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        private void SelectData(Itop.Client.Base.FormBase FB,string strYear)
        {
            string sql_35 = null;
            string sql_110 = null;
            sql_35 = " (l1='35') and DQ!='市辖供电区' and AreaID="
                    + "'" + FB.ProjectUID + "' and AreaName like '%部地区' and s2 <'" + strYear + "'";
            sql_110 = " (l1='110') and DQ!='市辖供电区' and AreaID="
                    + "'" + FB.ProjectUID + "' and AreaName like '%部地区' and s2 <'" + strYear + "'";
            try
            {
                list_35 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", sql_35);
                list_110 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", sql_110);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 重绘,写入数据
        /// </summary>
        /// <param name="obj"></param>
        private void ReDraw(Itop.Client.Base.FormBase FB,FarPoint.Win.Spread.SheetView obj,string strYear)
        {
            obj.RowCount = 0;
            obj.ColumnCount = 0;
            int list_35Count = list_35.Count;
            int list_110Count = list_110.Count;
            int IntColCount = 16;
            if (list_110Count == 0)
                list_110Count = 1;
            if (list_35Count == 0)
                list_35Count = 1;
            int IntRowCount = 3 + list_35Count + list_110Count + 1 + 2 + 3;//标题占3行，分区类型占2行
            string title = null;

            obj.Columns.Count = IntColCount;
            obj.Rows.Count = IntRowCount;
            IntCol = obj.Columns.Count;

            PF.Sheet_GridandCenter(obj);//画边线，居中
            m_PF.LockSheets(obj);

            string strTitle = "";
            IntRow = 3;
            strTitle = "附表4 截至" + strYear + "年底铜陵县110（35）kV变电站概况";
            obj.SheetName = strTitle;
            PF.CreateSheetView(obj, IntRow, IntCol, 0, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            IntCol = 1;

            strTitle = "单位：万千伏安 万千瓦  % ";
            obj.AddSpanCell(IntRow, 0, 1, obj.Columns.Count);
            obj.SetValue(IntRow, 0, strTitle);
            //右对齐
            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            
            #region //列标题
            strTitle = "电压等级";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow += 1, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            for (int i = 0; i < 15; ++i)
            {
                switch (i)
                {
                    case 0:
                        strTitle = "变电站名称";
                        break;
                    case 1:
                        strTitle = "主变容量构成";
                        break;
                    case 2:
                        strTitle = "主变容量";
                        break;
                    case 3:
                        strTitle = "电压变比";
                        break;
                    case 4:
                        strTitle = "变电站高压侧主接线模式";
                        break;
                    case 5:
                        strTitle = "变电站中压侧主接线模式";
                        break;
                    case 6:
                        strTitle = "变电站低压侧主接线模式";
                        break;
                    case 7:
                        strTitle = "110kV出线回路";
                        break;
                    case 8:
                        strTitle = "35kV出线回路";
                        break;
                    case 9:
                        strTitle = "10kV出线回路";
                        break;
                    case 10:
                        strTitle = "无功补偿容量";
                        break;
                    case 11:
                        strTitle = "投运日期(年.月)";
                        break;
                    case 12:
                        strTitle = "所属分区";
                        break;
                    case 13:
                        strTitle = strYear + "年最大负荷";
                        break;
                    case 14:
                        strTitle = "负载率";
                        break;
                }
                PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
                PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            }

            #endregion
            #region 行标题
            strTitle = "110kV";
            if(list_110.Count==0)
            {
                NextRowMerge = 1;
            }
            else
            {
                NextRowMerge = list_110.Count;
            }
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow += 2, IntCol -= 15, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            for (int i = 0; i < 4; ++i)
            {
                switch (i)
                {
                    case 0:
                        strTitle = "小计";
                        NextRowMerge = 1;
                        if(list_110.Count==0)
                        {
                            IntRow += 1;
                        }
                        else
                        {
                            IntRow += list_110.Count;
                        }
                        break;
                    case 1:
                        strTitle = "35kV";
                        if(list_35.Count==0)
                        {
                            NextRowMerge = 1;
                            IntRow += 1;
                        }
                        else
                        {
                            NextRowMerge = list_35.Count;
                            IntRow +=1;
                        }
                        break;
                    case 2:
                        strTitle = "小计";
                        NextRowMerge = 1;
                        if(list_35.Count==0)
                        {
                            IntRow += 1;
                        }
                        else
                        {
                            IntRow += list_35.Count;
                        }
                        break;
                    case 3:
                        strTitle = "合计";
                        NextRowMerge = 1;
                        IntRow += 1;
                        break;

                }
                PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow , IntCol, strTitle);
                //PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            }
            #endregion
            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;
            //PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
            Write110and35(obj);
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="obj"></param>
        private void Write110and35(FarPoint.Win.Spread.SheetView obj)
        {
            const int IntRow = 6;
            int iTemp=0;
            int list_110Count = list_110.Count;
            if (list_110.Count == 0)
                list_110Count = 1;
            int list_35Count = list_35.Count;
            if (list_35.Count == 0)
                list_35Count = 1;
            PSP_Substation_Info psi = null;
            #region 110kv
            for (int i = IntRow; i < list_110.Count + IntRow; ++i)
            {
                psi = (PSP_Substation_Info)list_110[i - IntRow];
                for (int j = 1; j < obj.ColumnCount; ++j)
                {
                    switch (j)
                    {
                    case 1://变电站名称
                        obj.SetValue(i, j, psi.Title);
                    	break;
                    case 2://主变容量构成
                        obj.SetValue(i, j, psi.L4);
                        break;
                    case 3://主变容量
                        obj.SetValue(i, j, psi.L2);
                        break;
                    case 4://电压变比
                        //obj.SetValue(i, j, "没有“电压变比”数据");
                        break;
                    case 5://变电站高压侧主接线模式
                        //obj.SetValue(i, j, "没有“变电站高压侧主接线模式”数据");
                        break;
                    case 6://变电站中压侧主接线模式
                        //obj.SetValue(i, j, "没有“变电站中压侧主接线模式”数据");
                        break;
                    case 7://变电站低压侧主接线模式
                        //obj.SetValue(i, j, "没有“变电站低压侧主接线模式”数据");
                        break;
                    case 8://110kV出线回路
                        //obj.SetValue(i, j, "没有“110kv出线回路”数据");
                        break;
                    case 9://35kV出线回路
                        //obj.SetValue(i, j, "没有“35kv出线回路”数据");
                        break;
                    case 10://10kV出线回路
                        //obj.SetValue(i, j, "没有“10kV出线回路”数据");
                        break;
                    case 11://无功补偿容量
                        obj.SetValue(i, j, psi.L5);
                        break;
                    case 12://投运日期(年.月)
                        obj.SetValue(i, j, psi.S2);
                        break;
                    case 13://所属分区
                        obj.SetValue(i, j, psi.AreaName);
                        break;
                    case 14://2009年最大负荷
                        //obj.SetValue(i, j, "没有“2009年最大负荷”数据！！！");
                        break;
                    case 15://负载率
                        obj.Cells[i,j].Formula="D"+(i+1)+"/O"+(i+1);
                        obj.Cells[i, j].CellType = PC;
                        break;

                }
            }
            }
            #endregion
            //小计
            
            for(int j=1;j<obj.ColumnCount;++j)
            {
                if(j==2)
                {
                    obj.Cells[(IntRow + list_110Count), j].Formula = "D" + (IntRow + list_110Count + 1);
                    //obj.SetValue((IntRow + list_110.Count), j,obj.GetValue((IntRow+list_110.Count),j+1));
                }
                else if(j==3)
                {
                    obj.Cells[(IntRow + list_110Count), j].Formula = "SUM(D" + (IntRow + 1) + ":D" + (IntRow + list_110Count)+")";
                }else
                {
                    obj.Cells[(IntRow+list_110Count), j].Locked = false;
                }
            }
            #region 35kv
            iTemp=(IntRow+list_110Count +1);
            for (int i = iTemp; i < (list_35.Count + iTemp); ++i)
            {
                psi = (PSP_Substation_Info)list_35[i - iTemp];
                for (int j = 1; j < obj.ColumnCount; ++j)
                {
                    switch (j)
                    {
                        case 1://变电站名称
                            obj.SetValue(i, j, psi.Title);
                            break;
                        case 2://主变容量构成
                            obj.SetValue(i, j, psi.L4);
                            break;
                        case 3://主变容量
                            obj.SetValue(i, j, psi.L2);
                            break;
                        case 4://电压变比
                            //obj.SetValue(i, j, "没有“电压变比”数据");
                            break;
                        case 5://变电站高压侧主接线模式
                            //obj.SetValue(i, j, "没有“变电站高压侧主接线模式”数据");
                            break;
                        case 6://变电站中压侧主接线模式
                            //obj.SetValue(i, j, "没有“变电站中压侧主接线模式”数据");
                            break;
                        case 7://变电站低压侧主接线模式
                            //obj.SetValue(i, j, "没有“变电站低压侧主接线模式”数据");
                            break;
                        case 8://110kV出线回路
                            //obj.SetValue(i, j, "没有“110kv出线回路”数据");
                            break;
                        case 9://35kV出线回路
                            //obj.SetValue(i, j, "没有“35kv出线回路”数据");
                            break;
                        case 10://10kV出线回路
                            //obj.SetValue(i, j, "没有“10kV出线回路”数据");
                            break;
                        case 11://无功补偿容量
                            obj.SetValue(i, j, psi.L5);
                            break;
                        case 12://投运日期(年.月)
                            obj.SetValue(i, j, psi.S2);
                            break;
                        case 13://所属分区
                            obj.SetValue(i, j, psi.AreaName);
                            break;
                        case 14://2009年最大负荷
                            //obj.SetValue(i, j, "没有“2009年最大负荷”数据！！！");
                            break;
                        case 15://负载率
                            obj.Cells[i, j].Formula = "D" + (i + 1) + "/O" + (i + 1);
                            obj.Cells[i, j].CellType = PC;
                            break;

                    }
                }
            }
            #endregion
            //小计 合计
            iTemp = (IntRow + list_110Count+ list_35Count + 1);
            for (int i = 0; i < 2; ++i)
            {
                for (int j = 1; j < obj.ColumnCount; ++j)
                {
                    if(i==0)
                    {
                        if (j == 2)
                        {
                            obj.Cells[(iTemp+i), j].Formula = "D" + (iTemp+i + 1);
                            //obj.SetValue((IntRow + list_110.Count), j,obj.GetValue((IntRow+list_110.Count),j+1));
                        }
                        else if (j == 3)
                        {
                            obj.Cells[(iTemp+i), j].Formula = "SUM(D" + (iTemp+i - list_35.Count + 1) + ":D" + (iTemp+i) + ")";
                        }
                        else
                        {
                            obj.Cells[(iTemp+i), j].Locked = false;
                        }
                    }
                    else
                    {
                        obj.Cells[iTemp+i, j].Locked = false;
                    }
                }
            }
        }
    }
}
