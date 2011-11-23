using System;
using System.Collections.Generic;
using System.Text;
/******************************************************************************************************
 *  ClassName：Sheet10_5
 *  Action：用来生成Sheet10_5表的调用方法
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表没用数据库，是计算得出
 *  时间：2010-10-11。
 *********************************************************************************************************/
namespace Itop.JournalSheet.Function10
{
    class Sheet10_5
    {
        private int ColCount = 0;//列数
        private int RowCount = 0;//行数
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();
        private Function.PublicFunction PF = new Function.PublicFunction();

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet10_5Title(FarPoint.Win.Spread.FpSpread Fpobj,FarPoint.Win.Spread.SheetView obj, string Title)
        {

            int IntColCount = 8;
            int IntRowCount = 1 + 2 + 3;//标题占3行，分区类型占2行，+2是有两行合并
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


            strTitle = " 地     市 ";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow, IntCol -= 1, strTitle);
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

            strTitle = " “十二五” ";
            PF.CreateSheetView(obj, NextRowMerge -= 1, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 合     计";
            PF.CreateSheetView(obj, NextRowMerge -= 1, NextColMerge, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;
            //加载数据
            this.WriteData(Fpobj,obj, IntRow, IntCol, Title);
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        /// <param name="Title"></param>
        private void WriteData(FarPoint.Win.Spread.FpSpread Fpobj,FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol, string Title)
        {
            string SheetN = "'"+Fpobj.Sheets[5].SheetName+"'!";
           string  strYear = null;
           int intDQ = 0;//当前行数
            for (int i = 0; i < obj.ColumnCount; ++i)
            {
                switch (i)
                {
                    case 1:
                        strYear = "D";
                        break;
                    case 2:
                        strYear = "E";
                        break;
                    case 3:
                        strYear = "F";
                        break;
                    case 4:
                        strYear = "G";
                        break;
                    case 5:
                        strYear = "H";
                        break;
                    case 6:
                        strYear = "I";
                        break;

                    default:
                        break;
                } 

                if (i == obj.ColumnCount - 1)
                {
                    obj.Cells[IntRow, i].Formula = "SUM(C6:G6)";
                }
                else if (i == 0)
                {
                    PF.CreateSheetView(obj, 1, 1, IntRow, IntCol, "铜陵");

                }
                else
                {
                    //找到10_4的最大行数就是要取的行数
                   intDQ= Fpobj.Sheets[5].RowCount;
                   obj.Cells[IntRow, i].Formula = SheetN + strYear + intDQ + "+" + SheetN + strYear + (intDQ - 1) + "+" + SheetN + strYear + (intDQ - 2) + "+" + SheetN + strYear + (intDQ - 3);
                        // "'表10-4'!E27+'表10-4'!E28+'表10-4'!E29+'表10-4'!E30";
                }
            }
        }
    }
}
