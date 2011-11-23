﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using Itop.Client.Common;
/******************************************************************************************************
 *  ClassName：Sheet10_4_1
 *  Action：用来生成Sheet10_4付表的调用方法
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表ps_table_TZGS,
 *  时间：2010-10-11。
 *********************************************************************************************************/
namespace Itop.JournalSheet.Function10
{
    class Sheet10_4_1
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private const int VCCount = 4;//电压等级个数
        private const int VCCount10_1 = 8;//表10_1电压等级个数
        private const int VCCount10_3 = 3;//表10_3电压等级个数
        private string[] VoltageClass = new string[VCCount];
        private IList City = null;
        private IList County = null;
        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();
        private Function10.Sheet10_1_1 S10_1_1 = new Function10.Sheet10_1_1();
        private Function10.Sheet10_4 S10_4 = new Function10.Sheet10_4();

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet10_4_1Title(FarPoint.Win.Spread.FpSpread Fpobj, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            SelectDQ();
            //InitTitle();
            int IntColCount = 10;
            int IntRowCount = (City.Count + County.Count + 2) * VCCount + 2 + 3;//标题占3行，分区类型占2行，
            string title = null;

            obj.SheetName = Title;
            obj.Columns.Count = IntColCount;
            obj.Rows.Count = IntRowCount;
            IntCol = obj.Columns.Count;

            PF.Sheet_GridandCenter(obj);//画线，居中
            S10_1.ColReadOnly(obj, IntColCount);
            //obj.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;

            string strTitle = "";
            IntRow = 3;
            strTitle = Title;
            PF.CreateSheetView(obj, IntRow, IntCol, 0, 0, Title);
            PF.SetSheetViewColumnsWidth(obj, 0, Title);
            IntCol = 1;

            strTitle = "分     区";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge, IntRow, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "类     型";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge, IntRow+=1, IntCol , strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "分     区";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge, IntRow-=1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "名     称";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "项     目";
            PF.CreateSheetView(obj, NextRowMerge+=1, NextColMerge, IntRow -= 1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2010年";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2011年 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2012年 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2013年 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2014年 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2015年";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);


            strTitle = " “十二五”合计";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;
            SetLeftTitle(obj, IntRow, IntCol);//左侧标题
            WriteData(Fpobj, obj, IntRow, IntCol);//数据
            WriteDataAmount(obj, IntRow, IntCol);//合计
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 填写左侧表行头
        /// </summary>
        private void SetLeftTitle(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            int index = 0;
            for (int c = 0; c < VCCount; ++c)//初始化电压等级
            {
                switch (c)
                {
                    case 0:
                        VoltageClass[c] = "110(66)电网";
                        break;
                    case 1:
                        VoltageClass[c] = "35电网";
                        break;
                    case 2:
                        VoltageClass[c] = "10(6、20)电网";
                        break;
                    case 3:
                        VoltageClass[c] = "低压电网";
                        break;

                    default:
                        break;
                }
            }
            for (int i = 0; i < 2; ++i)
            {
                if (i == 0)
                {
                    PF.CreateSheetView(obj, ((City.Count + 1) * VCCount), NextColMerge, IntRow, IntCol, "市辖供电区");
                    for (int j = 0; j < (City.Count + 1); ++j)//市辖供电区
                    {
                        if (j == 0)
                        {
                            PF.CreateSheetView(obj, VCCount, NextColMerge, IntRow + j * VCCount, IntCol + 1, "合计");
                        }
                        else
                        {
                            PF.CreateSheetView(obj, VCCount, NextColMerge, IntRow + j * VCCount, IntCol + 1, City[index].ToString());
                            index++;
                        }
                        for (int c = 0; c < VCCount; ++c)
                        {
                            PF.CreateSheetView(obj, 1, NextColMerge, IntRow + j * VCCount + c, IntCol + 2, VoltageClass[c]);
                        }
                    }
                }
                else
                {
                    PF.CreateSheetView(obj, ((County.Count + 1) * VCCount), NextColMerge, IntRow + ((City.Count + 1) * VCCount), IntCol, "县级供电区");
                    index = 0;
                    for (int j = (City.Count + 1); j < ((County.Count + 1) + (City.Count + 1)); ++j)//县级供电区
                    {
                        if (j == (City.Count + 1))
                        {
                            PF.CreateSheetView(obj, VCCount, NextColMerge, IntRow + j * VCCount, IntCol + 1, "合计");
                        }
                        else
                        {
                            PF.CreateSheetView(obj, VCCount, NextColMerge, IntRow + j * VCCount, IntCol + 1, County[index].ToString());
                            index++;
                        }
                        for (int c = 0; c < VCCount; ++c)
                        {
                            PF.CreateSheetView(obj, 1, NextColMerge, IntRow + j * VCCount + c, IntCol + 2, VoltageClass[c]);
                        }
                    }
                }

            }
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        private void WriteData(FarPoint.Win.Spread.FpSpread Fpobj, FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            int year = 2010;
            double Amount = 0;
            string strAmount = null;
            string strTitle = null;
            string strYear = null;
            string strYear1 = null;
            for (int i = 1; i < (City.Count + 1); ++i)//市辖不带合计
            {
                for (int c = 0; c < VCCount; ++c)
                {
                    strTitle = S10_1_1.ReturnNext0fStr(obj, (IntRow + i * VCCount), (IntCol + 1), c + 1, 1);
                    year = 2010;
                    for (int col = 3; col < obj.ColumnCount; ++col)
                    {
                        switch (year)
                        {
                            case 2010:
                                strYear = "D";
                                strYear1 = "E";
                                break;
                            case 2011:
                                strYear = "E";
                                strYear1 = "F";
                                break;
                            case 2012:
                                strYear = "F";
                                strYear1 = "G";
                                break;
                            case 2013:
                                strYear = "G";
                                strYear1 = "H";
                                break;
                            case 2014:
                                strYear = "H";
                                strYear1 = "I";
                                break;
                            case 2015:
                                strYear = "I";
                                strYear1 = "J";
                                break;

                            default:
                                break;
                        } 
                        if (col == (obj.ColumnCount - 1))
                        {
                            obj.Cells[(IntRow + i * VCCount + c), (IntCol + col)].Formula = "Sum(E" + (IntRow + i * VCCount + c + 1) + ":I" + (IntRow + i * VCCount + c + 1) + ")";
                        }
                        else
                        {
                            switch (c)
                            {
                                case 0:
                                    strAmount = "SUM('" + Fpobj.Sheets[1].SheetName + "'!" + strYear1 + (IntRow + i * VCCount10_1 + c + 1 ) + ":" + strYear1 + (IntRow + i * VCCount10_1 + c + 2 ) + ",'" + Fpobj.Sheets[4].SheetName + "'!" + strYear + (IntRow + i * VCCount10_3 + c + 1) + ")";
                                    obj.Cells[(IntRow + i * VCCount + c), (IntCol + col)].Formula = strAmount;
                                    break;
                                case 1:
                                    strAmount = "SUM('" + Fpobj.Sheets[1].SheetName + "'!" + strYear1 + (IntRow + i * VCCount10_1 + c + 1 ) + ":" + strYear1 + (IntRow + i * VCCount10_1 + c + 2 ) + ",'" + Fpobj.Sheets[4].SheetName + "'!" + strYear + (IntRow + i * VCCount10_3 + c + 1 ) + ")";
                                    obj.Cells[(IntRow + i * VCCount + c), (IntCol + col)].Formula = strAmount;
                                    break;
                                case 2:
                                    strAmount = "SUM('" + Fpobj.Sheets[1].SheetName + "'!" + strYear1 + (IntRow + i * VCCount10_1 + c + 1 ) + ":" + strYear1 + (IntRow + i * VCCount10_1 + c + 2 ) + ",'" + Fpobj.Sheets[4].SheetName + "'!" + strYear + (IntRow + i * VCCount10_3 + c + 1 ) + ")";
                                    obj.Cells[(IntRow + i * VCCount + c), (IntCol + col)].Formula = strAmount;
                                    break;
                                case 3://低压电网
                                    strAmount = "'" + Fpobj.Sheets[1].SheetName + "'!" + strYear1 + (IntRow + i * VCCount10_1 + c + 1 ); //"SUM('" + Fpobj.Sheets[1].SheetName + "'!" + strYear1 + (IntRow + i * VCCount + c + 1 + 4) + ":" + strYear1 + (IntRow + i * VCCount + c + 2 + 4) + ",'" + Fpobj.Sheets[4].SheetName + "'!" + strYear + (IntRow + i * VCCount + c + 1 - 1) + ")";
                                    obj.Cells[(IntRow + i * VCCount + c), (IntCol + col)].Formula = strAmount;
                                    break;
                                default:
                                    break;
                            }
                            year++;
                        }

                    }
                }
            }
            for (int i = (City.Count + 2); i < (City.Count + County.Count + 2); ++i)//县级不带合计
            {
                for (int c = 0; c < VCCount; ++c)
                {
                    strTitle = S10_1_1.ReturnNext0fStr(obj, (IntRow + i * VCCount), (IntCol + 1), c + 1, 1);
                    year = 2010;
                    for (int col = 3; col < obj.ColumnCount; ++col)
                    {
                        switch (year)
                        {
                            case 2010:
                                strYear = "D";
                                strYear1 = "E";
                                break;
                            case 2011:
                                strYear = "E";
                                strYear1 = "F";
                                break;
                            case 2012:
                                strYear = "F";
                                strYear1 = "G";
                                break;
                            case 2013:
                                strYear = "G";
                                strYear1 = "H";
                                break;
                            case 2014:
                                strYear = "H";
                                strYear1 = "I";
                                break;
                            case 2015:
                                strYear = "I";
                                strYear1 = "J";
                                break;

                            default:
                                break;
                        } if (col == (obj.ColumnCount - 1))
                        {
                            obj.Cells[(IntRow + i * VCCount + c), (IntCol + col)].Formula = "Sum(E" + (IntRow + i * VCCount + c + 1) + ":I" + (IntRow + i * VCCount + c + 1) + ")";
                        }
                        else
                        {
                            switch (c)
                            {
                                case 0:

                                    strAmount = "SUM('" + Fpobj.Sheets[1].SheetName + "'!" + strYear1 + (IntRow + i * VCCount10_1 + c + 1 ) + ":" + strYear1 + (IntRow + i * VCCount10_1 + c + 2 ) + ",'" + Fpobj.Sheets[4].SheetName + "'!" + strYear + (IntRow + i * VCCount10_3 + c + 1 ) + ")";
                                    obj.Cells[(IntRow + i * VCCount + c), (IntCol + col)].Formula = strAmount;
                                    break;
                                case 1:
                                    strAmount = "SUM('" + Fpobj.Sheets[1].SheetName + "'!" + strYear1 + (IntRow + i * VCCount10_1 + c + 1 ) + ":" + strYear1 + (IntRow + i * VCCount10_1 + c + 2 ) + ",'" + Fpobj.Sheets[4].SheetName + "'!" + strYear + (IntRow + i * VCCount10_3 + c + 1 ) + ")";
                                    obj.Cells[(IntRow + i * VCCount + c), (IntCol + col)].Formula = strAmount;
                                    break;
                                case 2:
                                    strAmount = "SUM('" + Fpobj.Sheets[1].SheetName + "'!" + strYear1 + (IntRow + i * VCCount10_1 + c + 1 ) + ":" + strYear1 + (IntRow + i * VCCount10_1 + c + 2 ) + ",'" + Fpobj.Sheets[4].SheetName + "'!" + strYear + (IntRow + i * VCCount10_3 + c + 1 ) + ")";
                                    obj.Cells[(IntRow + i * VCCount + c), (IntCol + col)].Formula = strAmount;
                                    break;
                                case 3://低压电网
                                    strAmount = "'" + Fpobj.Sheets[1].SheetName + "'!" + strYear1 + (IntRow + i * VCCount10_1 + c + 1 );// "SUM('" + Fpobj.Sheets[1].SheetName + "'!" + strYear1 + (IntRow + i * VCCount + c + 1 + 24) + ":" + strYear1 + (IntRow + i * VCCount + c + 2 + 24) + ",'" + Fpobj.Sheets[4].SheetName + "'!" + strYear + (IntRow + i * VCCount + c + 1 - 6) + ")";
                                    obj.Cells[(IntRow + i * VCCount + c), (IntCol + col)].Formula = strAmount;
                                    break;
                                default:
                                    break;
                            }
                            year++;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 计算合计数
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        private void WriteDataAmount(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            int year = 2010;
            string strTitle = null;
            string strTemp = null;
            for (int i = 0; i < 2; ++i)
            {
                for (int c = 0; c < VCCount; ++c)
                {
                    year = 2010;
                    for (int col = 3; col < obj.ColumnCount; ++col)
                    {
                        switch (col)
                        {
                            case 3:
                                strTitle = "D";
                                break;
                            case 4:
                                strTitle = "E";
                                break;
                            case 5:
                                strTitle = "F";
                                break;
                            case 6:
                                strTitle = "G";
                                break;
                            case 7:
                                strTitle = "H";
                                break;
                            case 8:
                                strTitle = "I";
                                break;
                            case 9:
                                strTitle = "J";
                                break;

                            default:
                                break;
                        }
                        if (i == 0)
                        {
                            if (col == obj.ColumnCount - 1)
                            {
                                obj.Cells[(IntRow + c), (IntCol + col)].Formula = "Sum(E" + (IntRow + c + 1) + ":I" + (IntRow + c + 1) + ")";
                            }
                            else
                            {
                                switch (c)
                                {
                                    case 0:
                                        strTemp = "";
                                        for (int j = 1; j <= City.Count; ++j)
                                        {
                                            if (j < City.Count)
                                            {
                                                strTemp += strTitle + (IntRow + j * VCCount + 1 + c) + "+";
                                            }
                                            else
                                            {
                                                strTemp += strTitle + (IntRow + j * VCCount + 1 + c);
                                                obj.Cells[(IntRow + c), (IntCol + col)].Formula = strTemp;
                                            }
                                        }
                                        break;
                                    case 1:
                                        strTemp = "";
                                        for (int j = 1; j <= City.Count; ++j)
                                        {
                                            if (j < City.Count)
                                            {
                                                strTemp += strTitle + (IntRow + j * VCCount + 1 + c) + "+";
                                            }
                                            else
                                            {
                                                strTemp += strTitle + (IntRow + j * VCCount + 1 + c);
                                                obj.Cells[(IntRow + c), (IntCol + col)].Formula = strTemp;
                                            }
                                        }
                                        break;
                                    case 2:
                                        strTemp = "";
                                        for (int j = 1; j <= City.Count; ++j)
                                        {
                                            if (j < City.Count)
                                            {
                                                strTemp += strTitle + (IntRow + j * VCCount + 1 + c) + "+";
                                            }
                                            else
                                            {
                                                strTemp += strTitle + (IntRow + j * VCCount + 1 + c);
                                                obj.Cells[(IntRow + c), (IntCol + col)].Formula = strTemp;
                                            }
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        else
                        {
                            if (col == obj.ColumnCount - 1)
                            {
                                obj.Cells[((IntRow) + (City.Count + 1) * VCCount + c), (IntCol + col)].Formula = "Sum(E" + ((IntRow + 1) + (City.Count + 1) * VCCount + c) + ":I" + ((IntRow + 1) + (City.Count + 1) * VCCount + c) + ")";
                            }
                            else
                            {
                                switch (c)
                                {
                                    case 0:
                                        strTemp = "";
                                        for (int j = 1; j <= County.Count; ++j)
                                        {
                                            if (j < County.Count)
                                            {
                                                strTemp += strTitle + ((IntRow + 1) + j * VCCount + (City.Count + 1) * VCCount + c) + "+";
                                            }
                                            else
                                            {
                                                strTemp += strTitle + ((IntRow + 1) + j * VCCount + (City.Count + 1) * VCCount + c);
                                                obj.Cells[((IntRow) + (City.Count + 1) * VCCount + c), (IntCol + col)].Formula = strTemp;
                                            }
                                        }
                                        break;
                                    case 1:
                                        strTemp = "";
                                        for (int j = 1; j <= County.Count; ++j)
                                        {
                                            if (j < County.Count)
                                            {
                                                strTemp += strTitle + ((IntRow + 1) + j * VCCount + (City.Count + 1) * VCCount + c) + "+";
                                            }
                                            else
                                            {
                                                strTemp += strTitle + ((IntRow + 1) + j * VCCount + (City.Count + 1) * VCCount + c);
                                                obj.Cells[((IntRow) + (City.Count + 1) * VCCount + c), (IntCol + col)].Formula = strTemp;
                                            }
                                        }
                                        break;
                                    case 2:
                                        strTemp = "";
                                        for (int j = 1; j <= County.Count; ++j)
                                        {
                                            if (j < County.Count)
                                            {
                                                strTemp += strTitle + ((IntRow + 1) + j * VCCount + (City.Count + 1) * VCCount + c) + "+";
                                            }
                                            else
                                            {
                                                strTemp += strTitle + ((IntRow + 1) + j * VCCount + (City.Count + 1) * VCCount + c);
                                                obj.Cells[((IntRow) + (City.Count + 1) * VCCount + c), (IntCol + col)].Formula = strTemp;
                                                //obj.SetValue(((IntRow) + (City.Count + 1) * VCCount + c), (IntCol + col),"kk");
                                            }
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        year++;
                    }
                }
            }
        }
        /// <summary>
        /// 返回地区名
        /// </summary>
        private void SelectDQ()
        {
            //AreaNameType = Services.BaseService.GetList("SelectTZGSOnAreaName", "");
            string con = null;
            con = "DQ='市辖供电区' group by AreaName";
            City = Services.BaseService.GetList("SelectTZGSAreaName", con);
            con = "DQ!='市辖供电区' group by AreaName";
            County = Services.BaseService.GetList("SelectTZGSAreaName", con);
        }
    }
}
