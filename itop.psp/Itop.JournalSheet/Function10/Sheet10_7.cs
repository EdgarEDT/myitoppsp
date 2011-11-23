using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using Itop.Client.Common;
/******************************************************************************************************
 *  ClassName：Sheet10_7
 *  Action：用来生成Sheet10_7表的调用方法
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表是数据库表ps_table_TZGS
 *  时间：2010-10-11。
 *********************************************************************************************************/
namespace Itop.JournalSheet.Function10
{
    class Sheet10_7
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private IList City = null;//市辖供电区
        private IList County = null;//县级供电区
        private string[] AreaType = new string[2];//市辖，县级

        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();
        private Function10.Sheet10_1_1 S10_1_1 = new Function10.Sheet10_1_1();
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet10_7Title(FarPoint.Win.Spread.SheetView obj, string Title)
        {
            AreaType[0] = "市辖供电区";
            AreaType[1] = "县级供电区";
            SelectProgType(AreaType[0]);
            SelectProgType(AreaType[1]);
            int IntColCount = 9;
            int IntRowCount = (City.Count + County.Count + 2) + 2 + 3;//标题占3行，分区类型占2行，+2是有两个投资金额合计
            string title = null;

            obj.SheetName = Title;
            obj.Columns.Count = IntColCount;
            obj.Rows.Count = IntRowCount;
            IntCol = obj.Columns.Count;

            PF.Sheet_GridandCenter(obj);//画边线，居中
            S10_1.ColReadOnly(obj, IntColCount);
            //obj.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;

            string strTitle = "";
            IntRow = 3;
            strTitle = Title;
            PF.CreateSheetView(obj, IntRow, IntCol, 0, 0, Title);
            PF.SetSheetViewColumnsWidth(obj, 0, Title);
            IntCol = 1;


            strTitle = " 区     域";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 工程类别 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2010年";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
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
            WriteData(obj, IntRow, IntCol);//数据
            //WriteDataAmount(obj, IntRow, IntCol);//合计
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        private void SetLeftTitle(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            for (int i = 0; i < 2; ++i)
            {
                if (i == 0)
                {
                    PF.CreateSheetView(obj, (City.Count + 1), 1, IntRow + i, IntCol, AreaType[i]);
                }
                else
                {
                    PF.CreateSheetView(obj, (County.Count + 1), 1, IntRow + i * (City.Count + 1), IntCol, AreaType[i]);
                }
            }
            for (int i = 0; i < (City.Count + County.Count + 2); ++i)
            {
                if (i < City.Count + 1)
                {
                    if (i == City.Count)
                    {
                        PF.CreateSheetView(obj, (1), 1, IntRow + i, IntCol + 1, "投资合计");
                    }
                    else
                    {
                        PF.CreateSheetView(obj, (1), 1, IntRow + i, IntCol + 1, City[i].ToString());
                    }
                }
                else
                {
                    if (i == (City.Count + County.Count + 1))//最后一行
                    {
                        PF.CreateSheetView(obj, (1), 1, IntRow + i, IntCol + 1, "投资合计");
                    }
                    else
                    {
                        PF.CreateSheetView(obj, (1), 1, IntRow + i, IntCol + 1, County[(i - City.Count - 1)].ToString());
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
        private void WriteData(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            int year = 2010;
            double Amount = 0.0;
            string strTitle = null;
            string strProg = null;
            for (int i = 0; i < (City.Count + 1); ++i)//市辖
            {
                strProg = S10_1_1.ReturnNext0fStr(obj, IntRow + i, IntCol + 1, 1, 1);
                year = 2010;
                for (int j = 2; j < obj.ColumnCount; ++j)
                {
                    switch (j)
                    {
                        case 2:
                            strTitle = "C";
                            break;
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

                        default:
                            break;
                    }
                    if (j == obj.ColumnCount - 1)//最后一列
                    {
                        obj.Cells[IntRow + i, IntCol + j].Formula = "SUM(D" + (IntRow + i + 1) + ":H" + (IntRow + i + 1) + ")";
                    }
                    else
                    {
                        if (i == City.Count)//最后一行
                        {
                            obj.Cells[IntRow + i, IntCol + j].Formula = "SUM(" + strTitle + (IntRow + i + 1 - 1) + ":" + strTitle + (IntRow + i + 1 - City.Count) + ")";
                        }
                        else
                        {
                            Amount = ReturnProg(AreaType[0], strProg, year);
                            obj.SetValue(IntRow + i, IntCol + j, Amount);
                        }
                    }
                    year++;
                }
            }
            year = 2010;
            for (int i = (City.Count + 1); i < (County.Count + 1 + City.Count + 1); ++i)//县级
            {
                strProg = S10_1_1.ReturnNext0fStr(obj, IntRow + i, IntCol + 1, 1, 1);
                year = 2010;
                for (int j = 2; j < obj.ColumnCount; ++j)
                {
                    switch (j)
                    {
                        case 2:
                            strTitle = "C";
                            break;
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

                        default:
                            break;
                    }
                    if (j == obj.ColumnCount - 1)//最后一列
                    {
                        obj.Cells[IntRow + i, IntCol + j].Formula = "SUM(D" + (IntRow + i + 1) + ":H" + (IntRow + i + 1) + ")";
                    }
                    else
                    {
                        if (i == (City.Count + 1 + County.Count))//最后一行
                        {
                            obj.Cells[IntRow + i, IntCol + j].Formula = "SUM(" + strTitle + (IntRow + i + 1 - 1) + ":" + strTitle + (IntRow + i + 1 - County.Count) + ")";
                        }
                        else
                        {
                            Amount = ReturnProg(AreaType[1], strProg, year);
                            obj.SetValue(IntRow + i, IntCol + j, Amount);
                        }
                    } year++;
                }
            }
        }
        /// <summary>
        /// 查询工程类别
        /// </summary>
        /// <param name="strTitle">地区</param>
        private void SelectProgType(string strTitle)
        {
            string con = null;
            string Temp = " and cast(substring(BianInfo,1,charindex('@',BianInfo,0)-1) as int) < 35";
            try
            {
                if (strTitle == "市辖供电区")
                {
                    con = "DQ='" + strTitle + "' " + Temp + " group by ProgType";
                    City = Services.BaseService.GetList("SelectTZGSProgType", con);
                }
                else
                {
                    con = "DQ!='市辖供电区' " + Temp + "group by ProgType";
                    County = Services.BaseService.GetList("SelectTZGSProgType", con);
                }
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 返回工程的金额
        /// </summary>
        /// <title></title>
        /// <param name="Prog">工程名称</param>
        /// <returns></returns>
        private double ReturnProg(string Title, string Prog, int year)
        {
            double Amount = 0;
            string con = null;
            string Temp = " and cast(substring(BianInfo,1,charindex('@',BianInfo,0)-1) as int) <35";
            if (Title == null)
            {
                Title = "";
            }
            if (Prog == null)
            {
                Prog = "";
            }

            if (Title == "市辖供电区")
            {
                con = "DQ='" + Title + "' and  ProgType='" + Prog + "' and BuildYear='" + year + "'" + Temp;
            }
            else
            {

                con = "DQ!='" + Title + "' and  ProgType='" + Prog + "' and BuildYear='" + year + "'" + Temp;
            }
            try
            {
                Amount = (double)Services.BaseService.GetObject("SelectTZGSSUMAmount", con);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return Amount;
        }
    }
}
