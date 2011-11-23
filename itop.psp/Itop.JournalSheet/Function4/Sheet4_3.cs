using System;
using System.Collections.Generic;
using System.Text;
/******************************************************************************************************
 *  ClassName：Sheet4_3
 *  Action：,sheet4_3表的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表没用到数据库，是计算出来的
 * 时间：2010-10-11
 * 。
 *********************************************************************************************************/
namespace Itop.JournalSheet.Function4
{
    class Sheet4_3
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值

        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet4_3Title(FarPoint.Win.Spread.FpSpread Fpobj, FarPoint.Win.Spread.SheetView obj, string Title)
        {


            int IntColCount = 10;
            int IntRowCount = (1) + 2 + 3;//标题占3行，分区类型占2行，
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


            strTitle = " 地     市";
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


            strTitle = " “十二五”年均增长率";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2020年";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "“十三五”年均增长率";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "全地区";
            PF.CreateSheetView(obj, NextRowMerge-=1, NextColMerge, 5, 0, strTitle);
            //PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;
            //SetLeftTitle(obj, IntRow, IntCol);//左侧标题
            WriteData(Fpobj,obj, IntRow, IntCol);//数据
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        public  void WriteData(FarPoint.Win.Spread.FpSpread Fpobj, FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            string SheetN = "'" + Fpobj.Sheets[2].SheetName + "'!";
            string  strYear ="" ;
            int intDQ = (IntRow+1);//当前行数
            for (int i = 0; i < obj.ColumnCount; ++i)
            {
                switch (i)
                {
                    case 1:
                        strYear = "B";
                        break;
                    case 2:
                        strYear = "C";
                        break;
                    case 3:
                        strYear = "D";
                        break;
                    case 4:
                        strYear = "E";
                        break;
                    case 5:
                        strYear = "F";
                        break;
                    case 6:
                        strYear = "G";
                        break;
                    case 8:
                        strYear = "I";
                        break;
                    default:
                        break;
                }

                if (i == obj.ColumnCount - 1)
                {
                    obj.Cells[IntRow, i].Formula = "POWER(I6/G6,1/5)-1";
                }
                else if (i == obj.ColumnCount - 3)
                {
                    obj.Cells[IntRow, i].Formula = "POWER(G6/B6,1/5)-1";
                }
                else if(i==0)
                {

                }
                else
                {
                    obj.Cells[IntRow, i].Formula = SheetN + strYear + intDQ ;
                    // "'表10-4'!E27+'表10-4'!E28+'表10-4'!E29+'表10-4'!E30";
                }
            }
        }
    }
}
