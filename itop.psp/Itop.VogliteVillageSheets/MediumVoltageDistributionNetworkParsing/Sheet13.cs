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
 *  ClassName：Sheet13
 *  Action：附表13 截至2009年底铜陵县配网基本情况统计 的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：用数据库表pspdev
 * 年份：2010-10-27
 * 修改时间：2010-11-15
 */

namespace Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing
{
    class Sheet13
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private string projectID = "";
        private IList list = null;
        private const int TitleCount = 19;
        private string[] ProjectTitle = new string[TitleCount];

        private Itop.JournalSheet.Function.PublicFunction PF = new Itop.JournalSheet.Function.PublicFunction();
        private Itop.VogliteVillageSheets.Function.PublicFunction m_PF = new Itop.VogliteVillageSheets.Function.PublicFunction();
        private FarPoint.Win.Spread.CellType.PercentCellType PC = new FarPoint.Win.Spread.CellType.PercentCellType();
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet_13Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int IntColCount = 3;
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

            strTitle = "单位：万千瓦 万千伏安 千伏 座 台";
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
                        strTitle = " 项                目   ";
                        break;
                    case 1:
                        strTitle = " 统计   ";
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
            int IntColCount = 3;
            int IntRowCount = TitleCount+1 + 2 + 3;//标题占3行，分区类型占2行
            string title = null;

            obj.Columns.Count = IntColCount;
            obj.Rows.Count = IntRowCount;
            IntCol = obj.Columns.Count;

            PF.Sheet_GridandCenter(obj);//画边线，居中
            m_PF.LockSheets(obj);

            string strTitle = "附表13 截至"+strEndYear+"年底铜陵县配网基本情况统计";
            IntRow = 3;
            obj.SheetName = strTitle;
            PF.CreateSheetView(obj, IntRow, IntCol, 0, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            IntCol = 1;

            strTitle = "单位：万千瓦 万千伏安 千伏 座 台";
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
                        strTitle = " 项                目   ";
                        break;
                    case 1:
                        strTitle = " 统计   ";
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
            int temp = 1;
            InitTitle();
            for (int i = IntRow; i < obj.RowCount; ++i)
            {

                for(int j=0;j<obj.ColumnCount;++j)
                {
                    switch(j)
                    {
                        case 0://序号
                            obj.SetValue(i, j, temp);
                            break;
                        case 1://项目
                            obj.SetValue(i, j, ProjectTitle[i-IntRow]);
                            break;
                        case 2://统计
                            obj.SetValue(i, j, "");
                            break;

                        default:
                            break;
                    }
                }
                temp++;
            }
            //写入数据
            obj.SetValue(IntRow, 2, Return10KVGY(FB, strEndYear));//10kV公用线路总条数
            obj.SetValue(IntRow + 1, 2, ReturnJK(FB, strEndYear));//10kV公用架空线路长度（裸导线）
            obj.SetValue(IntRow + 2, 2, ReturnJY(FB,strEndYear));//10kV公用架空线路长度（绝缘导线）
            obj.SetValue(IntRow + 3, 2, ReturnGY_Line(FB,strEndYear));//10kV公用电缆线路长度 
            obj.SetValue(IntRow+4,2,Return10KVZY(FB,strEndYear));//10kV专用线路条数
            obj.SetValue(IntRow+5,2,Return10KVZY_line(FB,strEndYear));//10kV专用线路长度 
            obj.SetValue(IntRow+6,2,Return04KVLine(FB,strEndYear));//0.4kV线路长度
            obj.SetValue(IntRow+7,2,ReturnPBTS(FB,strEndYear));//配电变压器总台数
            obj.SetValue(IntRow+8,2,ReturnPBRL(FB,strEndYear));//配电变压器总容量 
            obj.SetValue(IntRow+9,2,ReturnPBGYTS(FB,strEndYear,"公用"));//公用配电变压器台数
            obj.SetValue(IntRow+10,2,ReturnPBGYRL(FB,strEndYear,"公用"));//公用配电变压器容量 
            obj.SetValue(IntRow+11,2,ReturnPBGYTS(FB,strEndYear,"专用"));//专用配电变压器台数
            obj.SetValue(IntRow+12,2,ReturnPBGYRL(FB,strEndYear,"专用"));//专用配电变压器容量
            obj.SetValue(IntRow+13,2,ReturnAllofCount(FB,strEndYear ,"51"));//箱式变电站 
            obj.SetValue(IntRow + 14, 2, ReturnXSRL(FB,strEndYear));//箱式变电站容量
            obj.SetValue(IntRow+15,2,ReturnAllofCount(FB,strEndYear,"55"));//开闭所（站）
            obj.SetValue(IntRow+16,2,ReturnAllofCount(FB,strEndYear,"50"));//配电房（站）
            obj.SetValue(IntRow+17,2,ReturnAllofCount(FB,strEndYear,"56"));//环网开关柜 
            obj.SetValue(IntRow+18,2,ReturnAllofCount(FB,strEndYear,"57"));//柱上开关  
        }
        /// <summary>
        /// 初始化标题
        /// </summary>
        private void InitTitle()
        {
            for(int i=0;i<TitleCount;++i)
            {
                switch(i)
                {
                    case 0:
                        ProjectTitle[0] = "10kV公用线路总条数";
                        break;
                    case 1:
                        ProjectTitle[1] = "10kV公用架空线路长度（裸导线）";
                        break;
                    case 2:
                        ProjectTitle[2] = "10kV公用架空线路长度（绝缘导线）";
                        break;
                    case 3:
                        ProjectTitle[3] = "10kV公用电缆线路长度";
                        break;
                    case 4:
                        ProjectTitle[4] = "10kV专用线路条数";
                        break;
                    case 5:
                        ProjectTitle[5] = "10kV专用线路长度";
                        break;
                    case 6:
                        ProjectTitle[6] = "0.4kV线路长度";
                        break;
                    case 7:
                        ProjectTitle[7] = "配电变压器总台数";
                        break;
                    case 8:
                        ProjectTitle[8] = "配电变压器总容量";
                        break;
                    case 9:
                        ProjectTitle[9] = "公用配电变压器台数";
                        break;
                    case 10:
                        ProjectTitle[10] = "公用配电变压器容量";
                        break;
                    case 11:
                        ProjectTitle[11] = "专用配电变压器台数";
                        break;
                    case 12:
                        ProjectTitle[12] = "专用配电变压器容量";
                        break;
                    case 13:
                        ProjectTitle[13] = "箱式变电站";
                        break;
                    case 14:
                        ProjectTitle[14] = "箱式变电站容量";
                        break;
                    case 15:
                        ProjectTitle[15] = "开闭所（站）";
                        break;
                    case 16:
                        ProjectTitle[16] = "配电房（站）";
                        break;
                    case 17:
                        ProjectTitle[17] = "环网开关柜";
                        break;
                    case 18:
                        ProjectTitle[18] = "柱上开关";
                        break;

                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 10kV公用线路总条数
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="strEndYear"></param>
        /// <returns></returns>
        private int Return10KVGY(Itop.Client.Base.FormBase FB, string strEndYear)
        {
            int temp = 0;
            string sql = " ProjectID='"+FB.ProjectUID+"' and Type='05' and RateVolt='10'"
                          +"  and OperationYear<='"+strEndYear+"' and LineType2='公用'";
            try
            {
                temp = (int)Services.BaseService.GetObject("SelectPSPDEV_CountAll", sql);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return temp;
        }
        /// <summary>
        /// 10kV公用架空线路长度（裸导线）
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="strEndYear"></param>
        /// <returns></returns>
        private double ReturnJK(Itop.Client.Base.FormBase FB, string strEndYear)
        {
            string sql = " ProjectID='" + FB.ProjectUID + "' and Type='05' and RateVolt='10'"
                           + "     and OperationYear<='" + strEndYear + "'";
            double temp = 0;
            try
            {
                temp = (double)Services.BaseService.GetObject("SelectPSPDEV_SUMLineLength", sql);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return temp;
        }
        /// <summary>
        /// 10kV公用架空线路长度（绝缘导线）
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="strEndYear"></param>
        /// <returns></returns>
        private double ReturnJY(Itop.Client.Base.FormBase FB, string strEndYear)
        {
            string sql = " ProjectID='" + FB.ProjectUID + "' and Type='05' and RateVolt='10'"
                           + "     and OperationYear<='" + strEndYear + "'";
            double temp = 0;
            try
            {
                temp =(double) Services.BaseService.GetObject("SelectPSPDEV_SUMNum1", sql);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return temp;
        }
        /// <summary>
        /// 10kV公用电缆线路长度 
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="strEndYear"></param>
        /// <returns></returns>
        private double ReturnGY_Line(Itop.Client.Base.FormBase FB, string strEndYear)
        {
            string sql = " ProjectID='" + FB.ProjectUID + "' and Type='05' and RateVolt='10'"
                           + "     and OperationYear<='" + strEndYear + "' and LineType2='公用'";
            double temp = 0;
            try
            {
                temp =(double) Services.BaseService.GetObject("SelectPSPDEV_SUMLength2", sql);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return temp;
        }
        /// <summary>
        /// 10kV专用线路条数
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="strEndYear"></param>
        /// <returns></returns>
        private int Return10KVZY(Itop.Client.Base.FormBase FB, string strEndYear)
        {
            int temp = 0;
            string sql = " ProjectID='" + FB.ProjectUID + "' and Type='05' and RateVolt='10'"
                          + "  and OperationYear<='" + strEndYear + "' and LineType2='专用'";
            try
            {
                temp = (int)Services.BaseService.GetObject("SelectPSPDEV_CountAll", sql);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return temp;
        }
        /// <summary>
        /// 10kV专用线路长度  
        /// </summary>
        /// <returns></returns>
        private double Return10KVZY_line(Itop.Client.Base.FormBase FB, string strEndYear)
        {
            string sql = " ProjectID='" + FB.ProjectUID + "' and Type='05' and RateVolt='10'"
                          + "  and OperationYear<='" + strEndYear + "' and LineType2='专用'";
            double temp = 0;
            try
            {
                temp =(double) Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", sql);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return temp;
        }
        /// <summary>
        /// 0.4kV线路长度
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="strEndYear"></param>
        /// <returns></returns>
        private double Return04KVLine(Itop.Client.Base.FormBase FB, string strEndYear)
        {
            double temp = 0.0;
            string sql = " ProjectID='" + FB.ProjectUID + "' and Type='05' and RateVolt='0.4'"
                          + "  and OperationYear<='" + strEndYear + "' ";
            try
            {
                if (Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", sql)!=null)
                {
                    temp = (double)Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", sql);
                }
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return temp;
        }
        /// <summary>
        /// 配电变压器总台数
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="strEndYear"></param>
        /// <returns></returns>
        private int ReturnPBTS(Itop.Client.Base.FormBase FB, string strEndYear)
        {
            int temp = 0;
            string sql = " ProjectID='" + FB.ProjectUID + "' and Type='50' and OperationYear<='" + strEndYear + "' ";
            try
            {
                temp = (int)Services.BaseService.GetObject("SelectPSPDEV_SUMOfFlag", sql);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return temp;
        }
        /// <summary>
        /// 配电变压器总容量  
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="strEndYear"></param>
        /// <returns></returns>
        private double ReturnPBRL(Itop.Client.Base.FormBase FB, string strEndYear)
        {
            string sql = "ProjectID='"+FB.ProjectUID+"' and Type='50'and OperationYear<='"+strEndYear+"'";
            double temp = 0.0;
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
        /// 公用或专用配电变压器台数
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="strEndYear"></param>
        /// <returns></returns>
        private int ReturnPBGYTS(Itop.Client.Base.FormBase FB, string strEndYear,string GZ)
        {
            string sql = "c.s4='"+GZ+"'and a.ProjectID='"+FB.ProjectUID+"' and a.Type='50'and a.OperationYear<='"+strEndYear+"'";
            int temp = 0;
            try
            {
                temp = (int)Services.BaseService.GetObject("SelectPSPDEV_SUMFlagOfGZ", sql);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return temp;
        }
        /// <summary>
        /// 公用或专用配电变压器容量 
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="strEndYear"></param>
        /// <returns></returns>
        private double ReturnPBGYRL(Itop.Client.Base.FormBase FB, string strEndYear,string GZ)
        {
            string sql = "c.s4='" + GZ + "'and a.ProjectID='" + FB.ProjectUID + "' and a.Type='50'and a.OperationYear<='" + strEndYear + "'";
            double temp = 0.0;
            try
            {
                temp = (double)Services.BaseService.GetObject("SelectPSPDEV_SUMNum2OfGZ", sql);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return temp;
        }
        /// <summary>
        /// 返回统计值各种类型
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="strEndYear"></param>
        /// <param name="strType"></param>
        /// <returns></returns>
        private int ReturnAllofCount(Itop.Client.Base.FormBase FB, string strEndYear, string strType)
        {
            int temp = 0;
            string sql = "ProjectID='"+FB.ProjectUID+"' and Type='"+strType +"'and OperationYear<='"+strEndYear+"'";
            try
            {
                temp = (int)Services.BaseService.GetObject("SelectPSPDEV_CountAll", sql);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return temp;
        }
        /// <summary>
        /// 箱式变电站容量  
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="strEndYear"></param>
        /// <returns></returns>
        private double ReturnXSRL(Itop.Client.Base.FormBase FB, string strEndYear)
        {
            string sql = "ProjectID='" + FB.ProjectUID + "' and Type='51'and OperationYear<='" + strEndYear + "'";
            double temp = 0.0;
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
    }
}
