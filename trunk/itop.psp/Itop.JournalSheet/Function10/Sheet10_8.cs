using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using Itop.Client.Common;
/******************************************************************************************************
 *  ClassName：Sheet10_8
 *  Action：用来生成Sheet10_8表的调用方法
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表是从数据库表ps_Table_TZGS
 *  时间：2010-10-11。
 *********************************************************************************************************/
namespace Itop.JournalSheet.Function10
{
    class Sheet10_8
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        //private IList City = null;//市辖供电区
        //private IList County = null;//县级供电区
        private string[] AreaType = new string[2];//市辖，县级
        private const int IntProg = 3;
        private string[] strProg=new string[IntProg];//项目内容

        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();
        private Function10.Sheet10_1_1 S10_1_1 = new Function10.Sheet10_1_1();
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet10_8Title(FarPoint.Win.Spread.SheetView obj, string Title)
        {
            AreaType[0] = "市辖供电区";
            AreaType[1] = "县级供电区";

            strProg[0] = "高压";
            strProg[1] = "中低压";
            strProg[2] = "投资合计";

            int IntColCount = 9;
            int IntRowCount = (IntProg* 2) + 2 + 3;//标题占3行，分区类型占2行，
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
                    PF.CreateSheetView(obj, (IntProg), 1, IntRow + i, IntCol, AreaType[i]);
                }
                else
                {
                    PF.CreateSheetView(obj, (IntProg), 1, IntRow + i * (IntProg), IntCol, AreaType[i]);
                }
            }
            for (int i = 0; i < IntProg*2; ++i)
            {
                if (i < IntProg)
                {
                    if (i == IntProg-1)
                    {
                        PF.CreateSheetView(obj, (1), 1, IntRow + i, IntCol + 1, "投资合计");
                    }
                    else
                    {
                        PF.CreateSheetView(obj, (1), 1, IntRow + i, IntCol + 1, strProg[i].ToString());
                    }
                }
                else
                {
                    if (i == (IntProg * 2 - 1))//最后一行
                    {
                        PF.CreateSheetView(obj, (1), 1, IntRow + i, IntCol + 1, "投资合计");
                    }
                    else
                    {
                        PF.CreateSheetView(obj, (1), 1, IntRow + i, IntCol + 1, strProg[(i - IntProg )].ToString());
                    }
                }
            }
        }
        private void WriteData(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            int year = 2010;
            string strYear = null;
            double Amount = 0;
            for (int i = 0; i < IntProg;++i )//市辖
            {
                year = 2010;
                strYear = S10_1_1.ReturnNext0fStr(obj, IntRow + i, IntCol + 1, 1, 1);
                for (int j = 2; j < obj.ColumnCount; ++j)
                {
                    if(j==obj.ColumnCount-1)
                    {
                        obj.Cells[IntRow + i, IntCol + j].Formula = "SUM(D" + (IntRow + i + 1) + ":H" + (IntRow + i + 1) + ")";
                    }
                    else
                    {
                        if(i==IntProg)
                        {
                            obj.Cells[IntRow + i, IntCol + j].Formula = "SUM(" + PF.GetColumnTitle(j) + (IntRow + i + 1 - 1) + ":" + PF.GetColumnTitle(j) + (IntRow + i + 1 - 2) + ")";
                        }
                        else
                        {
                            Amount = SelectProgType(AreaType[0], strYear, year);
                            obj.SetValue(IntRow + i, IntCol + j, Amount);
                        }
                    }
                    year++;
                }
            }

            for (int i = IntProg; i < IntProg*2; ++i)//县级
            {
                year = 2010;
                strYear = S10_1_1.ReturnNext0fStr(obj, IntRow + i, IntCol + 1, 1, 1);
                for (int j = 2; j < obj.ColumnCount; ++j)
                {
                    if (j == obj.ColumnCount - 1)
                    {
                        obj.Cells[IntRow + i, IntCol + j].Formula = "SUM(D" + (IntRow + i + 1) + ":H" + (IntRow + i + 1) + ")";
                    }
                    else
                    {
                        if (i == (IntProg*2-1))
                        {
                            obj.Cells[IntRow + i, IntCol + j].Formula = "SUM(" + PF.GetColumnTitle(j) + (IntRow + i + 1 - 1) + ":" + PF.GetColumnTitle(j) + (IntRow + i + 1 - 2) + ")";
                        }
                        else
                        {
                            Amount = SelectProgType(AreaType[1], strYear, year);
                            obj.SetValue(IntRow + i, IntCol + j, Amount);
                        }
                    }
                    year++;
                }
            }
        }
        /// <summary>
        /// 查询工程类别
        /// </summary>
        /// <param name="strTitle">地区</param>
        private double SelectProgType(string strTitle,string strProg,int year)
        {
            string con = null;
            string Temp = null;
            double Amount = 0;
            if (strProg == "高压")
            {
                Temp = " and cast(substring(BianInfo,1,charindex('@',BianInfo,0)-1) as int) >= 35";
            }
            if(strProg=="中低压")
            {
                Temp = " and cast(substring(BianInfo,1,charindex('@',BianInfo,0)-1) as int) < 35";
            }
            try
            {
                if (strTitle == "市辖供电区")
                {
                    con = "DQ='" + strTitle + "' and BuildYear='"+year+"'"+Temp;
                    Amount = (double)Services.BaseService.GetObject("SelectTZGSSUMAmount", con);
                }
                else
                {
                    con = "DQ!='市辖供电区'  and BuildYear='"+year+"'"+Temp;
                    Amount = (double)Services.BaseService.GetObject("SelectTZGSSUMAmount", con);
                }
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return Amount;
        }
    }
}
