using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using Itop.Client.Common;

/******************************************************************************************************
 *  ClassName：Sheet10_1_1
 *  Action：用来生成Sheet10_1附表的调用方法
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表ps_table_TZGS
 *  时间：2010-10-11。
 *********************************************************************************************************/
namespace Itop.JournalSheet.Function10
{
    class Sheet10_1_1
    {
        private int ColCount = 0;//列数
        private int RowCount = 0;//行数
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private const int VPCount = 8;//项目个数
        private const int VCCount = 3;//电压等级个数
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();
        private Function.PublicFunction PF = new Function.PublicFunction();
        private IList City = null;//市辖供电区
        private IList County = null;//县级供电区
        private IList ILIstArea = null;
        private IList ILIstArea1 = null;
        private Area SA = new Area();
        private struct Area
        {
            public string[] VoltageClass;
              public  string[] strTitle;
              public  int CityCount;
            public int CountyCount;
            public int AreaCount;
            public int SumCountyCount;
        }
        //public string[,,] Amount = null;
        //private Dictionary<string, int> resualt = new Dictionary<string, int>();//存放地区的集合


        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public  void SetSheet10_1_1Title(FarPoint.Win.Spread.SheetView obj, string Title)
        {
           
            int IntColCount = 11;
            int IntRowCount = (ReturnArea()+2) * VPCount + 2 + 3;//标题占3行，分区类型占2行，+2是有两行合并
            //Amount=new string[2,City.Count+County.Count,4];//用来保存变电工程，线路工程的和给Sheet10_4_1附表数据
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

            //加载数据
            this.WriteData(obj, IntRow + 2, IntCol - 1, Title);

            strTitle = " 分区类型 ";
            PF.CreateSheetView(obj, NextRowMerge+=1 , NextColMerge, IntRow, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 分区名称";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol+=1 , strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 电压等级";
            PF.CreateSheetView(obj, NextRowMerge-=1, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "      (kV)     ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow+=1, IntCol , strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 项     目";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow -= 1, IntCol += 1, strTitle);
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

            strTitle = " “十二五”合计 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;

            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        private void WriteData(FarPoint.Win.Spread.SheetView obj,int IntRow,int IntCol,string Title)
        {
            //int CityCount= AreaTitle("市辖供电区");
            SA.VoltageClass= new string[VCCount];
            for (int i = 0; i < VCCount; ++i)
            {
                if(i==0)
                {
                    SA.VoltageClass[i] = "110(66)";
                }
                if (i == 1)
                {
                   SA. VoltageClass[i] = "35";
                }
                if (i == 2)
                {
                    SA.VoltageClass[i] = "10(6、20)";
                }
            }
                //MessageBox.Show(obj.RowCount.ToString());
                //int CountyCount=AreaTitle("县级供电区");
                for (int i = 0; i < 2; ++i)//两个分区类型
                {
                    if (i == 0)
                    {
                        PF.CreateSheetView(obj, (SA.CityCount + 1) * VPCount, 1, IntRow + i, IntCol, "市辖供电区");
                    }
                    else
                    {
                        PF.CreateSheetView(obj, (SA.SumCountyCount + 1) * VPCount, 1, IntRow + i * (SA.CityCount + 1) * VPCount, IntCol, "县级供电区");
                    }
                    for (int j = 0; j < (SA.CityCount + 1); ++j)//+2是有两个合计,市
                    {
                        
                        for (int c = 0; c < VCCount; ++c)
                        {
                            PF.CreateSheetView(obj, 2, 1, (IntRow + c * (VCCount - 1)) + j * VPCount, IntCol + 2, SA.VoltageClass[c]);//电压等级
                        }
                        if (i == 0)
                        {
                            if (j == 0)
                            {

                                PF.CreateSheetView(obj, VPCount, 1, IntRow + j * VPCount, IntCol + 1, "合计");//分区名称
                            }
                            else
                            {

                                PF.CreateSheetView(obj, VPCount, 1, IntRow + j * VPCount, IntCol + 1,this.City[j-1].ToString());
                            }
                        }
                    }
                    for (int j = 0; j < (SA.SumCountyCount + 1); ++j)//县辖供电区
                    {
                        for (int c = 0; c < VCCount; ++c)
                        {
                            PF.CreateSheetView(obj, 2, 1, (IntRow + c * (VCCount - 1)) + j * VPCount + (SA.CityCount + 1) * VPCount, IntCol + 2, SA.VoltageClass[c]);//电压等级
                        }
                        if (j == 0)
                        {
                            PF.CreateSheetView(obj, VPCount, 1, (IntRow + j * VPCount + (SA.CityCount + 1) * VPCount), IntCol + 1, "合计");
                        }
                        else
                        {
                            PF.CreateSheetView(obj, VPCount, 1, (IntRow + j * VPCount + (SA.CityCount + 1) * VPCount), IntCol + 1, this.County[j-1].ToString());
                        }
                    }


                }
                int year = 2010;
                double Amount = 0;
                string strColTitle = null;
                string TempDQ = null;//地区
                string DQType = null;//地区类型
                //写入项目及以后的列
                for (int i = 0; i < (SA.SumCountyCount + 1 + (SA.CityCount + 1)); ++i)
                {
                    for (int j = 0; j < VPCount; ++j)//项目
                    {
                        year = 2010;
                        for (int col = 4; col < obj.ColumnCount; ++col)//数据
                        {
                            if (i != 0 && i != SA.CityCount + 1)
                            {
                                switch (j)
                                {
                                    case 0://
                                        PF.CreateSheetView(obj, 1, 1, (IntRow + i * VPCount + j), IntCol + 3, "变电工程");
                                        TempDQ = (ReturnNext0fStr(obj, (IntRow + i * VPCount + j), (IntCol + 1), j + 1, 1));
                                        if(i>=1+SA.CityCount)//总体行数减去市辖地区的数量就是县级地区
                                        {
                                            DQType = ReturnNext0fStr(obj, (IntRow + i * VPCount + j), (IntCol), (i * VPCount + j + 1-SA.CityCount*VPCount), 1);
                                        }
                                        else
                                        {
                                            DQType = ReturnNext0fStr(obj, (IntRow + i * VPCount + j), (IntCol), (i * VPCount + j + 1), 1);
                                        }
                                        Amount = SelectArea(year, ReturnSql(SA.VoltageClass[0]), DQType, TempDQ, "bian");
                                        obj.SetValue((IntRow + i * VPCount + j), (IntCol + col), Amount);
                                        if (col == obj.ColumnCount - 1)//最后一列
                                        {
                                            obj.Cells[(IntRow + i * VPCount + j), (IntCol + col)].Formula = "SUM(F" + (IntRow + i * VPCount + j+1) + ":J" + (IntRow + i * VPCount + j+1) + ")";
                                        }
                                        //MessageBox.Show(ReturnNext0fStr(obj, (IntRow + i * VPCount + j), (IntCol + 1), (j+1), 1));
                                        break;
                                    case 1:
                                        PF.CreateSheetView(obj, 1, 1, (IntRow + i * VPCount + j), IntCol + 3, "线路工程");
                                        TempDQ = (ReturnNext0fStr(obj, (IntRow + i * VPCount + j), (IntCol + 1), j + 1, 1));
                                        if (i >= 1 + SA.CityCount)//总体行数减去市辖地区的数量就是县级地区
                                        {
                                            DQType = ReturnNext0fStr(obj, (IntRow + i * VPCount + j), (IntCol), (i * VPCount + j + 1 - SA.CityCount * VPCount), 1);
                                        }
                                        else
                                        {
                                            DQType = ReturnNext0fStr(obj, (IntRow + i * VPCount + j), (IntCol), (i * VPCount + j + 1), 1);
                                        }
                                        Amount = SelectArea(year, ReturnSql(SA.VoltageClass[0]), DQType, TempDQ, "line");
                                        obj.SetValue((IntRow + i * VPCount + j), (IntCol + col), Amount);
                                        if (col == obj.ColumnCount - 1)//最后一列
                                        {
                                            obj.Cells[(IntRow + i * VPCount + j), (IntCol + col)].Formula = "SUM(F" + (IntRow + i * VPCount + j+1) + ":J" + (IntRow + i * VPCount + j+1) + ")";
                                        }
                                        break;
                                    case 2:
                                        PF.CreateSheetView(obj, 1, 1, (IntRow + i * VPCount + j), IntCol + 3, "变电工程");
                                        TempDQ = (ReturnNext0fStr(obj, (IntRow + i * VPCount + j), (IntCol + 1), j + 1, 1));
                                        if (i >= 1 + SA.CityCount)//总体行数减去市辖地区的数量就是县级地区
                                        {
                                            DQType = ReturnNext0fStr(obj, (IntRow + i * VPCount + j), (IntCol), (i * VPCount + j + 1 - SA.CityCount * VPCount), 1);
                                        }
                                        else
                                        {
                                            DQType = ReturnNext0fStr(obj, (IntRow + i * VPCount + j), (IntCol), (i * VPCount + j + 1), 1);
                                        }
                                        Amount = SelectArea(year, ReturnSql(SA.VoltageClass[1]), DQType, TempDQ, "bian");
                                        obj.SetValue((IntRow + i * VPCount + j), (IntCol + col), Amount);
                                        if (col == obj.ColumnCount - 1)//最后一列
                                        {
                                            obj.Cells[(IntRow + i * VPCount + j), (IntCol + col)].Formula = "SUM(F" + (IntRow + i * VPCount + j+1) + ":J" + (IntRow + i * VPCount + j+1) + ")";
                                        }
                                        break;
                                    case 3:
                                        PF.CreateSheetView(obj, 1, 1, (IntRow + i * VPCount + j), IntCol + 3, "线路工程");
                                        TempDQ = (ReturnNext0fStr(obj, (IntRow + i * VPCount + j), (IntCol + 1), j + 1, 1));
                                        if (i >= 1 + SA.CityCount)//总体行数减去市辖地区的数量就是县级地区
                                        {
                                            DQType = ReturnNext0fStr(obj, (IntRow + i * VPCount + j), (IntCol), (i * VPCount + j + 1 - SA.CityCount * VPCount), 1);
                                        }
                                        else
                                        {
                                            DQType = ReturnNext0fStr(obj, (IntRow + i * VPCount + j), (IntCol), (i * VPCount + j + 1), 1);
                                        }
                                        Amount = SelectArea(year, ReturnSql(SA.VoltageClass[1]), DQType, TempDQ, "line");
                                        obj.SetValue((IntRow + i * VPCount + j), (IntCol + col), Amount);
                                        if (col == obj.ColumnCount - 1)//最后一列
                                        {
                                            obj.Cells[(IntRow + i * VPCount + j), (IntCol + col)].Formula = "SUM(F" + (IntRow + i * VPCount + j+1) + ":J" + (IntRow + i * VPCount + j+1) + ")";
                                        }
                                        break;
                                    case 4:
                                        PF.CreateSheetView(obj, 1, 1, (IntRow + i * VPCount + j), IntCol + 3, "配变工程");
                                        TempDQ = (ReturnNext0fStr(obj, (IntRow + i * VPCount + j), (IntCol + 1), j + 1, 1));
                                        if (i >= 1 + SA.CityCount)//总体行数减去市辖地区的数量就是县级地区
                                        {
                                            DQType = ReturnNext0fStr(obj, (IntRow + i * VPCount + j), (IntCol), (i * VPCount + j + 1 - SA.CityCount * VPCount), 1);
                                        }
                                        else
                                        {
                                            DQType = ReturnNext0fStr(obj, (IntRow + i * VPCount + j), (IntCol), (i * VPCount + j + 1), 1);
                                        }
                                        Amount = SelectArea(year, ReturnSql(SA.VoltageClass[2]), DQType, TempDQ, "pw-pb");
                                        obj.SetValue((IntRow + i * VPCount + j), (IntCol + col), Amount);
                                        if (col == obj.ColumnCount - 1)//最后一列
                                        {
                                            obj.Cells[(IntRow + i * VPCount + j), (IntCol + col)].Formula = "SUM(F" + (IntRow + i * VPCount + j+1) + ":J" + (IntRow + i * VPCount + j+1) + ")";
                                        }
                                        break;
                                    case 5:
                                        PF.CreateSheetView(obj, 1, 1, (IntRow + i * VPCount + j), IntCol + 3, "线路工程");
                                        TempDQ = (ReturnNext0fStr(obj, (IntRow + i * VPCount + j), (IntCol + 1), j + 1, 1));
                                        if (i >= 1 + SA.CityCount)//总体行数减去市辖地区的数量就是县级地区
                                        {
                                            DQType = ReturnNext0fStr(obj, (IntRow + i * VPCount + j), (IntCol), (i * VPCount + j + 1 - SA.CityCount * VPCount), 1);
                                        }
                                        else
                                        {
                                            DQType = ReturnNext0fStr(obj, (IntRow + i * VPCount + j), (IntCol), (i * VPCount + j + 1), 1);
                                        }
                                        Amount = SelectArea(year, ReturnSql(SA.VoltageClass[2]), DQType, TempDQ, "pw-line");
                                        obj.SetValue((IntRow + i * VPCount + j), (IntCol + col), Amount);
                                        if (col == obj.ColumnCount - 1)//最后一列
                                        {
                                            obj.Cells[(IntRow + i * VPCount + j), (IntCol + col)].Formula = "SUM(F" + (IntRow + i * VPCount + j+1) + ":J" + (IntRow + i * VPCount + j+1) + ")";
                                        }
                                        break;
                                    case 6://手写
                                        PF.CreateSheetView(obj, 1, 1, (IntRow + i * VPCount + j), IntCol + 3, "低压工程");
                                        obj.Cells[(IntRow + i * VPCount + j), (IntCol + col)].Locked = false;//读写
                                        if (col == obj.ColumnCount - 1)//最后一列
                                        {
                                            obj.Cells[(IntRow + i * VPCount + j), (IntCol + col)].Locked = true;//读写
                                            obj.Cells[(IntRow + i * VPCount + j), (IntCol + col)].Formula = "SUM(F" + (IntRow + i * VPCount + j+1) + ":J" + (IntRow + i * VPCount + j+1) + ")";
                                        }
                                        break;
                                    case 7://项目的最后一个项目"分项合计"
                                        PF.CreateSheetView(obj, 1, 1, (IntRow + i * VPCount + j), IntCol + 3, "分项合计");
                                        //MessageBox.Show(ReturnNext0fStr(obj, (IntRow + i * VPCount + j), (IntCol + 1), (j + 1), 1));
                                        switch (col)
                                        {
                                            case 4:
                                                strColTitle = "E";
                                                break;
                                            case 5:
                                                strColTitle = "F";
                                                break;
                                            case 6:
                                                strColTitle = "G";
                                                break;
                                            case 7:
                                                strColTitle = "H";
                                                break;
                                            case 8:
                                                strColTitle = "I";
                                                break;
                                            case 9:
                                                strColTitle = "J";
                                                break;

                                            default:
                                                break;
                                        }
                                        if (col == obj.ColumnCount - 1)//最后一列
                                        {
                                            obj.Cells[(IntRow + i * VPCount + j), (IntCol + col)].Formula = "SUM(F" + (IntRow + i * VPCount + j+1) + ":J" + (IntRow + i * VPCount + j+1) + ")";
                                        }
                                        else
                                        {
                                            obj.Cells[(IntRow + i * VPCount + j), (IntCol + col)].Formula = "SUM(" + strColTitle + (IntRow + i * VPCount + j - VPCount+2) + ":" + strColTitle + (IntRow + i * VPCount + j - 1+1) + ")";
                                        }
                                        break;

                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                switch (j)
                                { 
                                    case 0:
                                        PF.CreateSheetView(obj, 1, 1, (IntRow + i * VPCount + j), IntCol + 3, "变电工程");
                                        break;
                                    case 1:
                                        PF.CreateSheetView(obj, 1, 1, (IntRow + i * VPCount + j), IntCol + 3, "线路工程");
                                        break;
                                    case 2:
                                        PF.CreateSheetView(obj, 1, 1, (IntRow + i * VPCount + j), IntCol + 3, "变电工程");
                                        break;
                                    case 3:
                                        PF.CreateSheetView(obj, 1, 1, (IntRow + i * VPCount + j), IntCol + 3, "线路工程");
                                        break;
                                    case 4:
                                        PF.CreateSheetView(obj, 1, 1, (IntRow + i * VPCount + j), IntCol + 3, "配变工程");
                                        break;
                                    case 5:
                                        PF.CreateSheetView(obj, 1, 1, (IntRow + i * VPCount + j), IntCol + 3, "线路工程");
                                        break;
                                    case 6:
                                        PF.CreateSheetView(obj, 1, 1, (IntRow + i * VPCount + j), IntCol + 3, "低压工程");
                                        break;
                                    case 7:
                                        PF.CreateSheetView(obj, 1, 1, (IntRow + i * VPCount + j), IntCol + 3, "分项合计	");
                                        break;

                                    default:
                                        break;
                                }
                            }
                            year++;
                         }
                      }
                }
            //写入合计的数据
                string Formula = null;
                for (int i = 0; i < (1); ++i)//市 CityCount
                {
                    for (int j = 0; j < VPCount; ++j)//项目
                    {
                        for (int col = 4; col < obj.ColumnCount; ++col)
                        {
                            switch (col)
                            { 
                                case 4:
                                    for (int c = 1; c <= SA.CityCount; ++c)
                                    {
                                        if(c==SA.CityCount)
                                        {
                                            Formula += "E" + ((IntRow + i * VPCount + j + 1) + c * VPCount) ;
                                            obj.Cells[(IntRow + i * VPCount + j), (IntCol + col)].Formula = Formula;
                                            Formula = "";
                                        }
                                        else
                                        {
                                            Formula += "E" + ((IntRow + i * VPCount + j + 1) + c * VPCount) + "+";
                                        }
                                    }
                                    break;
                                case 5:
                                    for (int c = 1; c <= SA.CityCount; ++c)
                                    {
                                        if (c == SA.CityCount)
                                        {
                                            Formula += "F" + ((IntRow + i * VPCount + j + 1) + c * VPCount);
                                            obj.Cells[(IntRow + i * VPCount + j), (IntCol + col)].Formula = Formula;
                                            Formula = "";
                                        }
                                        else
                                        {
                                            Formula += "F" + ((IntRow + i * VPCount + j + 1) + c * VPCount) + "+";
                                        }
                                    }
                                    break;
                                case 6:
                                    for (int c = 1; c <= SA.CityCount; ++c)
                                    {
                                        if (c == SA.CityCount)
                                        {
                                            Formula += "G" + ((IntRow + i * VPCount + j + 1) + c * VPCount);
                                            obj.Cells[(IntRow + i * VPCount + j), (IntCol + col)].Formula = Formula;
                                            Formula = "";
                                        }
                                        else
                                        {
                                            Formula += "G" + ((IntRow + i * VPCount + j + 1) + c * VPCount) + "+";
                                        }
                                    }
                                   break;
                                case 7:
                                    for (int c = 1; c <= SA.CityCount; ++c)
                                    {
                                        if (c == SA.CityCount)
                                        {
                                            Formula += "H" + ((IntRow + i * VPCount + j + 1) + c * VPCount);
                                            obj.Cells[(IntRow + i * VPCount + j), (IntCol + col)].Formula = Formula;
                                            Formula = "";
                                        }
                                        else
                                        {
                                            Formula += "H" + ((IntRow + i * VPCount + j + 1) + c * VPCount) + "+";
                                        }
                                    }
                                    break;
                                case 8:
                                    for (int c = 1; c <= SA.CityCount; ++c)
                                    {
                                        if (c == SA.CityCount)
                                        {
                                            Formula += "I" + ((IntRow + i * VPCount + j + 1) + c * VPCount);
                                            obj.Cells[(IntRow + i * VPCount + j), (IntCol + col)].Formula = Formula;
                                            Formula = "";
                                        }
                                        else
                                        {
                                            Formula += "I" + ((IntRow + i * VPCount + j + 1) + c * VPCount) + "+";
                                        }
                                    }
                                    break;
                                case 9:
                                    for (int c = 1; c <= SA.CityCount; ++c)
                                    {
                                        if (c == SA.CityCount)
                                        {
                                            Formula += "J" + ((IntRow + i * VPCount + j + 1) + c * VPCount);
                                            obj.Cells[(IntRow + i * VPCount + j), (IntCol + col)].Formula = Formula;
                                            Formula = "";
                                        }
                                        else
                                        {
                                            Formula += "J" + ((IntRow + i * VPCount + j + 1) + c * VPCount) + "+";
                                        }
                                    }
                                    break;
                                case 10://十二五
                                        obj.Cells[(IntRow + i * VPCount + j), (IntCol + col)].Formula = "SUM(F" + (IntRow + i * VPCount + j+1) + ":J" + (IntRow + i * VPCount + j+1) + ")";
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
                for (int i = 0; i < 1; ++i)//县SA.SumCountyCount
                {
                    for (int j = 0; j < VPCount; ++j)//项目
                    {
                        for (int col = 4; col < obj.ColumnCount; ++col)
                        {
                            switch (col)
                            {
                                case 4:
                                    for (int c = 1; c <= SA.SumCountyCount; ++c)
                                    {
                                        if (c == SA.SumCountyCount)
                                        {
                                            Formula += "E" + ((IntRow + i * VPCount + j + (SA.CityCount + 1) * VPCount + 1) + c * VPCount);
                                            obj.Cells[(IntRow + i * VPCount + j+(SA.CityCount+1)*VPCount), (IntCol + col)].Formula = Formula;
                                            Formula = "";
                                        }
                                        else
                                        {
                                            Formula += "E" + ((IntRow + i * VPCount + j + (SA.CityCount + 1) * VPCount + 1) + c * VPCount) + "+";
                                        }
                                    }
                                    break;
                                case 5:
                                    for (int c = 1; c <= SA.SumCountyCount; ++c)
                                    {
                                        if (c == SA.SumCountyCount)
                                        {
                                            Formula += "F" + ((IntRow + i * VPCount + j + (SA.CityCount + 1) * VPCount + 1) + c * VPCount);
                                            obj.Cells[(IntRow + i * VPCount + j + (SA.CityCount + 1) * VPCount), (IntCol + col)].Formula = Formula;
                                            Formula = "";
                                        }
                                        else
                                        {
                                            Formula += "F" + ((IntRow + i * VPCount + j + (SA.CityCount + 1) * VPCount + 1) + c * VPCount) + "+";
                                        }
                                    }
                                    break;
                                case 6:
                                    for (int c = 1; c <= SA.SumCountyCount; ++c)
                                    {
                                        if (c == SA.SumCountyCount)
                                        {
                                            Formula += "G" + ((IntRow + i * VPCount + j + (SA.CityCount + 1) * VPCount + 1) + c * VPCount);
                                            obj.Cells[(IntRow + i * VPCount + j + (SA.CityCount + 1) * VPCount), (IntCol + col)].Formula = Formula;
                                            Formula = "";
                                        }
                                        else
                                        {
                                            Formula += "G" + ((IntRow + i * VPCount + j + (SA.CityCount + 1) * VPCount + 1) + c * VPCount) + "+";
                                        }
                                    }
                                    break;
                                case 7:
                                    for (int c = 1; c <= SA.SumCountyCount; ++c)
                                    {
                                        if (c == SA.SumCountyCount)
                                        {
                                            Formula += "H" + ((IntRow + i * VPCount + j + (SA.CityCount + 1) * VPCount + 1) + c * VPCount);
                                            obj.Cells[(IntRow + i * VPCount + j + (SA.CityCount + 1) * VPCount), (IntCol + col)].Formula = Formula;
                                            Formula = "";
                                        }
                                        else
                                        {
                                            Formula += "H" + ((IntRow + i * VPCount + j + (SA.CityCount + 1) * VPCount + 1) + c * VPCount) + "+";
                                        }
                                    }
                                    break;
                                case 8:
                                    for (int c = 1; c <= SA.SumCountyCount; ++c)
                                    {
                                        if (c == SA.SumCountyCount)
                                        {
                                            Formula += "I" + ((IntRow + i * VPCount + j + (SA.CityCount + 1) * VPCount + 1) + c * VPCount);
                                            obj.Cells[(IntRow + i * VPCount + j + (SA.CityCount + 1) * VPCount), (IntCol + col)].Formula = Formula;
                                            Formula = "";
                                        }
                                        else
                                        {
                                            Formula += "I" + ((IntRow + i * VPCount + j + (SA.CityCount + 1) * VPCount + 1) + c * VPCount) + "+";
                                        }
                                    }
                                    break;
                                case 9:
                                    for (int c = 1; c <= SA.SumCountyCount; ++c)
                                    {
                                        if (c == SA.SumCountyCount)
                                        {
                                            Formula += "J" + ((IntRow + i * VPCount + j + (SA.CityCount + 1) * VPCount + 1) + c * VPCount);
                                            obj.Cells[(IntRow + i * VPCount + j + (SA.CityCount + 1) * VPCount), (IntCol + col)].Formula = Formula;
                                            Formula = "";
                                        }
                                        else
                                        {
                                            Formula += "J" + ((IntRow + i * VPCount + j + (SA.CityCount + 1) * VPCount + 1) + c * VPCount) + "+";
                                        }
                                    }
                                    break;
                                case 10://十二五
                                    obj.Cells[(IntRow + i * VPCount + j + (SA.CityCount + 1) * VPCount), (IntCol + col)].Formula = "SUM(F" + (IntRow + i * VPCount + j + (SA.CityCount + 1) * VPCount + 1) + ":J" + (IntRow + i * VPCount + j + (SA.CityCount + 1) * VPCount+1) + ")";
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
            }
        /// <summary>
        /// reurn Amount
        /// </summary>
        /// <param name="year">year</param>
        /// <param name="strAreaName">分区名称</param>
        /// <param name="BianInfo">项目类型,这个是把模糊查询语句写入</param>
        /// <param name="DQN">分区n</param>
        /// <param name="Col4">col4</param>
        /// <returns>返回投资金额</returns>
        private double SelectArea(int year, string BianInfo,string DQN,string strAreaName, string Col4)
        {
            string con = null;
            double  Amount = 0;
            try
            {
                 if (DQN == "市辖供电区")
                 {
                     con = "BuildYear ='" + year + "' and (" + BianInfo + ")and " +

                               "Col4='" + Col4 + "' and DQ='" + DQN + "' and AreaName='" + strAreaName + "'";
                     Amount = (double)Services.BaseService.GetObject("SelectTZGSSUMAmount", con);
                      
                 }
                 else 
                 {
                     con = "BuildYear ='" + year + "' and (" + BianInfo + ")and " +

                               "Col4='" + Col4 + "' and DQ !='市辖供电区' and AreaName='" + strAreaName + "'";
                     Amount = (double)Services.BaseService.GetObject("SelectTZGSSUMAmount", con);
                      
                 }
                
             }
            catch(System.Exception e)
            {
                //MessageBox.Show(e.Message);
                Amount = 0;
            }
            return Amount;
        }
        /// <summary>
        /// 返回任何行列中的字符如果有合并同类项的就向上查找
        /// </summary>
        /// <param name="obj">SheetView</param>
        /// <param name="IntRow">current row</param>
        /// <param name="IntCol">current column</param>
        /// <param name="IntRowStep">当前行所在合并同类项的第几层</param>
        /// <param name="IntColStep">当前所在列处在合并同类项的第几层</param>
        /// <returns></returns>
        public string ReturnNext0fStr(FarPoint.Win.Spread.SheetView obj,int IntRow,int IntCol,int IntRowStep ,int IntColStep)
        {
            string strReturn = null;
            for (int i = 0; i < IntRowStep; ++i)
            {
                for (int j = 0; j < IntColStep; ++j)
                {
                    if (IntRow - i < 0 && IntCol - j < 0)
                    {
                        return strReturn;
                    }
                    if (obj.GetValue((IntRow-i), (IntCol-j)) == null)//等于空就向上查找
                    {
                        
                        break;
                    }
                    else
                    {
                        strReturn = (string)obj.GetValue((IntRow - i), (IntCol - j));
                        break;
                    }
                }
            }
            return strReturn;
        }
        /// <summary>
        /// 取电压等级转变成sql语句
        /// </summary>
        /// <returns></returns>
        public string ReturnSql(string strVoltageClass)
        { 
            string Sql=null;
            switch (strVoltageClass)
           {
               case "110(66)":
                   Sql = "BianInfo like '110@%'or BianInfo like '66@%'";
                   break;
               case "35":
                   Sql = "BianInfo like '35@%'";
                   break;
                case "10(6、20)":
                    Sql = "BianInfo like '10@%'or BianInfo like '6@%' or BianInfo like '20@%'";
                    break;
               default:
                   break;
           }
           return Sql;
        }
        /// <summary>
        /// 返回地区数据
        /// </summary>
        /// <returns>不重复的地区数据</returns>
        private int ReturnArea()
        {
            //string conCity=null;
            //string conCounty=null;
            ////int CountyCount=this.ReturnCounty();
            //SA.strTitle = new string[SA.CityCount];
            //SA. AreaCount = new int[CountyCount];
            //SA.CityCount = new int[CountyCount];
            //SA.CountyCount = new int[CountyCount];
            int SumCount = 0;
            SA.SumCountyCount = 0;
            try
            {
                ////for (int i = 0; i < CountyCount; ++i)
                ////{
                //    //SA.strTitle[i] = County[i].ToString();
                //    //if (SA.strTitle[i] == "市辖供电区")
                //    //{
                //        conCity = " BuildYear between '2010' and '2015' and AreaName!='' and DQ='市辖供电区' group by AreaName";
                //        City = Services.BaseService.GetList("SelectTZGSAreaName", conCity);
                //        SA.CityCount = City.Count;
                //        //SA.AreaCount = ILIstArea.Count;
                //    //}
                //    //else
                //    //{
                //        conCounty = " BuildYear between '2010' and '2015' and AreaName!='' and DQ!='市辖供电区'group by AreaName";
                //        County  = Services.BaseService.GetList("SelectTZGSAreaName", conCounty);
                //        SA.CountyCount = County.Count;
                //        //SA.AreaCount = ILIstArea1.Count;
                //        //}

                        string con = null;
                        con = "DQ='市辖供电区' group by AreaName";
                        City = Services.BaseService.GetList("SelectTZGSAreaName", con);
                        SA.CityCount = City.Count;
                        con = "DQ!='市辖供电区' group by AreaName";
                        County = Services.BaseService.GetList("SelectTZGSAreaName", con);
                        SA.CountyCount = County.Count;

                        SA.SumCountyCount = SA.CountyCount;//县辖供电区数量 
                    SumCount = SA.CountyCount + SA.CityCount;//总数县辖供电区，和市辖供电区
                //}
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return SumCount;
        }
        /// <summary>
        /// 返回不重复地区数据
        /// </summary>
        /// <returns></returns>
        private int ReturnCounty()
        {
            int Count = 0;
            try
            {
                
                County = Services.BaseService.GetList("SelectTZGSDQ", "");
                Count = County.Count;
            }
            catch(System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return Count;
        }
        /// <summary>
        /// 获得对象中某一属性的数值
        /// </summary>
        /// <param name="Ps">对象</param>
        /// <param name="name">数值</param>
        private  int GetValue<T>(T obj, int Value)
        {
            Type type = typeof(T);
            //T  psvalue = default(T);
            int psvalue = 0;
            foreach (PropertyInfo pi in type.GetProperties())
            {

                if (pi.Name == obj.ToString())
                {
                    try
                    {
                        psvalue = Convert.ToInt32 (pi.GetValue(obj, null));
                        break;
                    }
                    catch (System.Exception e)
                    {
                        MessageBox.Show("没有此项数据！！！");
                    }
                }
                //pi.SetValue(dataObject, treeNode.GetValue(pi.Name), null);

            }
            return psvalue;
        }
        /// <summary>
        /// 返回固定标题所包含地区的数量
        /// </summary>
        /// <param name="strTitle"></param>
        /// <returns></returns>
        private int AreaTitle(string strTitle)
        {
            string con = null;
            IList ICount = null;
            int Count = 0;
            try
            {
                con = "DQ='" + strTitle + "'and BuildYear between '2010' and '2015'group by AreaName";
                ICount = Services.BaseService.GetList("SelectTZGSAreaName", con);
                Count = ICount.Count;
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return Count;
        }
        /// <summary>
        /// 转换分区名，如分区1=地区1
        /// </summary>
        /// <param name="Title">地区的名字</param>
        /// <returns></returns>
        private string ChangeFQ(string Title)
        {
            string  DQ = "";
            string DQ1 = "";
            DQ = Title.Substring(0, 1);
            DQ1 = Title.Substring(1,(Title.Length-1));
            if(DQ=="分")
            {
                DQ = "地"+DQ1;
            }
            if(DQ=="县")
            {
                DQ = "县";
            }
            return DQ;
        }
    }
}
