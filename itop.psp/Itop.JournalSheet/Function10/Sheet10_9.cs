using System;
using System.Collections.Generic;
using System.Text;
/******************************************************************************************************
 *  ClassName：Sheet10_9
 *  Action：用来生成Sheet10_9表的调用方法
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表没用到数据库一部分手写，一部分是计算
 *  时间：2010-10-11。
 *********************************************************************************************************/
namespace Itop.JournalSheet.Function10
{

    class Sheet10_9
    {
        const int Funds = 3;//资金类别有三个大类 
        const int PublicNetworkSupport = 8;//公用网投资又分8个小类
        //const int CapitalInCash = 5;//资本金分5类
        const int NumberOfPlies = 11;//层数

        string[] strFunds = new string[Funds];
          string [,] strPublicNetworkSupport = new  string[2,PublicNetworkSupport];
        //string[] strCapitalInCash = new string[CapitalInCash];
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();
        private Function10.Sheet10_1_1 S10_1_1 = new Function10.Sheet10_1_1();
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet10_9Title(FarPoint.Win.Spread.SheetView obj, string Title)
        {

            int IntColCount = 9;
            int IntRowCount = NumberOfPlies + 2 + 3;//标题占3行，分区类型占2行，
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

            strTitle = " 资金类别 ";
            PF.CreateSheetView(obj, NextRowMerge+=1, NextColMerge+1, IntRow, IntCol-=1 , strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2010年";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 2, strTitle);
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
            //PF.SetWholeColumnsWidth(obj);//列宽
        }
        /// <summary>
        /// 写左侧标题
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        private void SetLeftTitle(FarPoint.Win.Spread.SheetView obj,int IntRow,int IntCol)
        {
            int index = 0;
            InitLeftTitle();
            for (int i = IntRow; i < (NumberOfPlies+IntRow); ++i)
            {
                if (i == IntRow)
                {
                   PF.CreateSheetView(obj, 1, 2, i, 0, strFunds[0]);
                   PF.SetSheetViewColumnsWidth(obj, 0, strFunds[0]);
               }
               if (i == NumberOfPlies + IntRow - 1)
                {
                    PF.CreateSheetView(obj, 1, 2, (i), 0, strFunds[2]);
                    PF.SetSheetViewColumnsWidth(obj, 0, strFunds[2]);
                }
                if (i == NumberOfPlies + IntRow - 2)
                {
                    PF.CreateSheetView(obj, 1, 2, (i), 0, strFunds[1]);
                    PF.SetSheetViewColumnsWidth(obj, 0, strFunds[1]);
                }
                if (i > IntRow && i < NumberOfPlies + IntRow - 2)
                {
                    PF.CreateSheetView(obj, 1, 1, i, 0, strPublicNetworkSupport[0, index].ToString());
                    //PF.SetSheetViewColumnsWidth(obj, 1, strPublicNetworkSupport[0, index].ToString());
                    PF.CreateSheetView(obj, 1, 1, i, 1, strPublicNetworkSupport[1, index].ToString());
                    //PF.SetSheetViewColumnsWidth(obj, 1, strPublicNetworkSupport[1, index].ToString());
                    index++;
                }
            }
        }
        /// <summary>
        /// 写数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        private void WriteData(FarPoint.Win.Spread.SheetView obj,int IntRow,int IntCol)
        {
            for(int i=IntRow;i<obj.RowCount;++i)
            {
                for(int j=2;j<obj.ColumnCount;++j)
                {
                    if(j==(obj.ColumnCount-1))
                    {
                        obj.Cells[i, j].Formula = "D"+(i+1)+"+E"+(i+1)+"+F"+(i+1)+"+G"+(i+1)+"+H"+(i+1);
                    }
                    else
                    {
                        if (i == IntRow || i == (IntRow + 1) || i == (obj.RowCount - 1))
                        {
                            switch(i)
                            {
                                case 5 :
                                    obj.Cells[i, j].Formula = PF.GetColumnTitle(j) + "7+" + PF.GetColumnTitle(j) + "13+" + PF.GetColumnTitle(j)+"14";
                                    break;
                                case 6:
                                    obj.Cells[i, j].Formula ="SUM("+ PF.GetColumnTitle(j) + "8:" + PF.GetColumnTitle(j) + "12)";
                                    break;
                                case 15:
                                    obj.Cells[i, j].Formula = PF.GetColumnTitle(j) + "6+" + PF.GetColumnTitle(j) + "15";
                                    break;

                                default:
                                    break;
                            }
                        }
                        else
                        {
                            obj.Cells[i, j].Locked = false;//手写
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 初始化左侧标题
        /// </summary>
        private void InitLeftTitle()
        {
            //string[] obj = new string[Dimensionality];
            for (int i = 0; i < PublicNetworkSupport; ++i)
            {
                switch(i)
                {
                    case 0:
                        strFunds[i] = "一、公用网投资";
                        strPublicNetworkSupport[0,i] = "1";
                        strPublicNetworkSupport[1,i] = "资本金";
                        break;
                    case 1:
                        strFunds[i] = "二、用户工程投资";
                        strPublicNetworkSupport[0,i] = "1.1";
                        strPublicNetworkSupport[1,i] = "自有资金";
                        break;
                    case 2:
                        strFunds[i] = "三、全地区合计";
                        strPublicNetworkSupport[0,i] = "1.2";
                        strPublicNetworkSupport[1,i] = "国家财政拨款";
                        break;
                    case 3:
                        strPublicNetworkSupport[0,i] = "1.3";
                        strPublicNetworkSupport[1,i] = "地方出资";
                        break;
                    case 4:
                        strPublicNetworkSupport[0,i] = "1.4";
                        strPublicNetworkSupport[1,i] = "居民小区配套费";
                        break;
                    case 5:
                        strPublicNetworkSupport[0,i] = "1.5";
                        strPublicNetworkSupport[1,i] = "其它资金";
                        break;
                    case 6:
                        strPublicNetworkSupport[0,i] = "2";
                        strPublicNetworkSupport[1,i] = "银行贷款";
                        break;
                    case 7:
                        strPublicNetworkSupport[0,i] = "3";
                        strPublicNetworkSupport[1,i] = "其它";
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
