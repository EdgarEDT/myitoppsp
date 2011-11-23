using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using Itop.Client.Common;
using Itop.JournalSheet.From;
/******************************************************************************************************
 *  ClassName：Sheet10_3
 *  Action：用来生成Sheet10_3表的调用方法
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表ps_table_TZGS
 *  时间：2010-10-11。
 *********************************************************************************************************/
namespace Itop.JournalSheet.Function10
{
    class Sheet10_3//:FrmSheet10_1
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private IList DQType=null;
        private const int VCCount = 4;//电压等级个数+低压工程
        private string[] VoltageClass = new string[VCCount];
        private string[] Number = null;
        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();
        private Function10.Sheet10_1_1 S10_1_1=new Function10.Sheet10_1_1();
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet10_3Title(FarPoint.Win.Spread.SheetView obj, string Title)
        {
            SelectDQ();
            InitTitle();
            int IntColCount = 10;
            int IntRowCount = (DQType.Count+2)*VCCount + 2 + 3;//标题占3行，分区类型占2行，
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


            strTitle = " 编     号";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "类     型";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "   电  压  等  级  ";
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
            SetLeftTitle(obj,IntRow,IntCol);//左侧标题
            WriteData(obj, IntRow, IntCol);//数据
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 填写左侧表行头
        /// </summary>
        private void SetLeftTitle(FarPoint.Win.Spread.SheetView obj,int IntRow,int IntCol)
        {
            int n = 1;
            int IntDQType = 0;
            for(int i=0;i<(DQType.Count+2);++i)//+2是有县地区合计，全地区合计
            {

                if(i==1)
                {
                    PF.CreateSheetView(obj, VCCount, NextColMerge, IntRow + i*VCCount, IntCol + 1, "县级供电区");
                    PF.CreateSheetView(obj, VCCount, NextColMerge, IntRow + i * VCCount, IntCol , "2");
                }
                if(i==(DQType.Count+2-1))
                {
                    PF.CreateSheetView(obj, VCCount, NextColMerge, IntRow + i*VCCount, IntCol + 1, "全地区合计");
                    PF.CreateSheetView(obj, VCCount, NextColMerge, IntRow + i * VCCount, IntCol , "3");
                }
                if(i!=1&&i!=(DQType.Count+2-1))
                {
                    
                    //switch(DQType[IntDQType].ToString())
                    //{
                    //    case "其中：直供直管":
                    PF.CreateSheetView(obj, VCCount, NextColMerge, IntRow + i * VCCount, IntCol + 1, DataChangeType(DQType[IntDQType].ToString()));
                        //    break;
                        //default:
                        //    break;
                    //}
                    if(i==0)
                    {
                        PF.CreateSheetView(obj, VCCount, NextColMerge, IntRow + i * VCCount, IntCol , "1");
                    }
                    else
                    {
                        PF.CreateSheetView(obj, VCCount, NextColMerge, IntRow + i * VCCount, IntCol , "2."+IntDQType.ToString());
                    }
                    IntDQType ++;
                }
                for(int j=0;j<VCCount;++j)//电压等级
                {
                    PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow + i * VCCount+j, IntCol+2, this.VoltageClass[j]);
                }
            }
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        protected void WriteData(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            int year = 2010;
            double Amount = 0;
            string strTitle = null;
            string strYear = null;
            //写数据，合计最后写
            for (int i = 0; i < DQType.Count + 2; ++i)
            {
                for (int j = 0; j < VCCount; ++j)
                {
                    year = 2010;
                    strTitle = TypeChangeData(S10_1_1.ReturnNext0fStr(obj, (IntRow + i * VCCount + j), (IntCol + 1), j + 1, 1));
                    for (int col = 3; col < obj.ColumnCount; ++col)
                    {
                        if (i != 1 && i != (DQType.Count + 2 - 1))
                        {
                            switch (j)
                            {
                                case 0:
                                    if (col == (obj.ColumnCount - 1))
                                    {
                                        obj.Cells[(IntRow + i * VCCount + j), (IntCol + col)].Formula = "Sum(E" + (IntRow + i * VCCount + j + 1) + ":I" + (IntRow + i * VCCount + j + 1) + ")";
                                    }
                                    else
                                    {
                                        Amount = SelectAmount(year, strTitle, S10_1_1.ReturnSql(VoltageClass[0]));
                                        obj.SetValue((IntRow + i * VCCount + j), (IntCol + col), Amount);
                                    }
                                    break;
                                case 1:
                                    if (col == (obj.ColumnCount - 1))
                                    {
                                        obj.Cells[(IntRow + i * VCCount + j), (IntCol + col)].Formula = "Sum(E" + (IntRow + i * VCCount + j + 1) + ":I" + (IntRow + i * VCCount + j + 1) + ")";
                                    }
                                    else
                                    {
                                        Amount = SelectAmount(year, strTitle, S10_1_1.ReturnSql(VoltageClass[1]));
                                        obj.SetValue((IntRow + i * VCCount + j), (IntCol + col), Amount);
                                    }
                                    break;
                                case 2:
                                    if (col == (obj.ColumnCount - 1))
                                    {
                                        obj.Cells[(IntRow + i * VCCount + j), (IntCol + col)].Formula = "Sum(E" + (IntRow + i * VCCount + j + 1) + ":I" + (IntRow + i * VCCount + j + 1) + ")";
                                    }
                                    else
                                    {
                                        Amount = SelectAmount(year, strTitle, S10_1_1.ReturnSql(VoltageClass[2]));
                                        obj.SetValue((IntRow + i * VCCount + j), (IntCol + col), Amount);
                                    }
                                    break;
                                case 3://低压工程，手写
                                    if (col == (obj.ColumnCount - 1))
                                    {
                                        obj.Cells[(IntRow + i * VCCount + j), (IntCol + col)].Locked = true;
                                        obj.Cells[(IntRow + i * VCCount + j), (IntCol + col)].Formula = "Sum(E" + (IntRow + i * VCCount + j + 1) + ":I" + (IntRow + i * VCCount + j + 1) + ")";
                                    }
                                    else
                                    {
                                        obj.Cells[(IntRow + i * VCCount + j), (IntCol + col)].Locked = false;
                                        obj.SetValue((IntRow + i * VCCount + j), (IntCol + col), 0);
                                    }
                                    break;

                                default:
                                    break;
                            }
                        }
                        else//写合计数据
                        {
                            switch (year)
                            {
                                case 2010:
                                    strYear = "D";
                                    break;
                                case 2011:
                                    strYear = "E";
                                    break;
                                case 2012:
                                    strYear = "F";
                                    break;
                                case 2013:
                                    strYear = "G";
                                    break;
                                case 2014:
                                    strYear = "H";
                                    break;
                                case 2015:
                                    strYear = "I";
                                    break;

                                default:
                                    break;
                            }
                            switch (j)
                            {
                                case 0:
                                    if (col == (obj.ColumnCount - 1))
                                    {
                                        obj.Cells[(IntRow + i * VCCount + j), (IntCol + col)].Formula = "Sum(E" + (IntRow + i * VCCount + j + 1) + ":I" + (IntRow + i * VCCount + j + 1) + ")";
                                    }
                                    else
                                    {
                                        if (i == 1)
                                        {
                                            obj.Cells[(IntRow + i * VCCount + j), (IntCol + col)].Formula = strYear + ((IntRow + i * VCCount + j + 1) + VCCount) + "+" + strYear + ((IntRow + i * VCCount + j + 1) + 2 * VCCount) + "+" + strYear + ((IntRow + i * VCCount + j + 1) + 3 * VCCount);// + "+" + strYear + ((IntRow + i * VCCount + j + 1) + 4 * VCCount);
                                        }
                                        else
                                        {
                                            obj.Cells[(IntRow + i * VCCount + j), (IntCol + col)].Formula = strYear + ((IntRow + i * VCCount + j + 1) - (DQType.Count + 1) * VCCount) + "+" + strYear + ((IntRow + i * VCCount + j + 1) - (DQType.Count) * VCCount);
                                        }
                                    }
                                    break;
                                case 1:
                                    if (col == (obj.ColumnCount - 1))
                                    {
                                        obj.Cells[(IntRow + i * VCCount + j), (IntCol + col)].Formula = "Sum(E" + (IntRow + i * VCCount + j + 1) + ":I" + (IntRow + i * VCCount + j + 1) + ")";
                                    }
                                    else
                                    {
                                        if (i == 1)
                                        {
                                            obj.Cells[(IntRow + i * VCCount + j), (IntCol + col)].Formula = strYear + ((IntRow + i * VCCount + j + 1) + VCCount) + "+" + strYear + ((IntRow + i * VCCount + j + 1) + 2 * VCCount) + "+" + strYear + ((IntRow + i * VCCount + j + 1) + 3 * VCCount);// +"+" + strYear + ((IntRow + i * VCCount + j + 1) + 4 * VCCount);
                                        }
                                        else
                                        {
                                            obj.Cells[(IntRow + i * VCCount + j), (IntCol + col)].Formula = strYear + ((IntRow + i * VCCount + j + 1) - (DQType.Count + 1) * VCCount) + "+" + strYear + ((IntRow + i * VCCount + j + 1) - (DQType.Count) * VCCount);
                                        }
                                    }
                                    break;
                                case 2:
                                    if (col == (obj.ColumnCount - 1))
                                    {
                                        obj.Cells[(IntRow + i * VCCount + j), (IntCol + col)].Formula = "Sum(E" + (IntRow + i * VCCount + j + 1) + ":I" + (IntRow + i * VCCount + j + 1) + ")";
                                    }
                                    else
                                    {
                                        if (i == 1)
                                        {
                                            obj.Cells[(IntRow + i * VCCount + j), (IntCol + col)].Formula = strYear + ((IntRow + i * VCCount + j + 1) + VCCount) + "+" + strYear + ((IntRow + i * VCCount + j + 1) + 2 * VCCount) + "+" + strYear + ((IntRow + i * VCCount + j + 1) + 3 * VCCount);// +"+" + strYear + ((IntRow + i * VCCount + j + 1) + 4 * VCCount);
                                        }
                                        else
                                        {
                                            obj.Cells[(IntRow + i * VCCount + j), (IntCol + col)].Formula = strYear + ((IntRow + i * VCCount + j + 1) - (DQType.Count + 1) * VCCount) + "+" + strYear + ((IntRow + i * VCCount + j + 1) - (DQType.Count) * VCCount);
                                        }

                                    }
                                    break;
                                case 3://低压工程，手写
                                    if (col == (obj.ColumnCount - 1))
                                    {
                                        obj.Cells[(IntRow + i * VCCount + j), (IntCol + col)].Formula = "Sum(E" + (IntRow + i * VCCount + j + 1) + ":I" + (IntRow + i * VCCount + j + 1) + ")";
                                    }
                                    else
                                    {
                                        if (i == 1)
                                        {
                                            obj.Cells[(IntRow + i * VCCount + j), (IntCol + col)].Formula = strYear + ((IntRow + i * VCCount + j + 1) + VCCount) + "+" + strYear + ((IntRow + i * VCCount + j + 1) + 2 * VCCount) + "+" + strYear + ((IntRow + i * VCCount + j + 1) + 3 * VCCount);// +"+" + strYear + ((IntRow + i * VCCount + j + 1) + 4 * VCCount);
                                        }
                                        else
                                        {
                                            obj.Cells[(IntRow + i * VCCount + j), (IntCol + col)].Formula = strYear + ((IntRow + i * VCCount + j + 1) - (DQType.Count + 1) * VCCount) + "+" + strYear + ((IntRow + i * VCCount + j + 1) - (DQType.Count) * VCCount);
                                        }

                                    }
                                    break;

                                default:
                                    break;
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
             DQType = Services.BaseService.GetList("SelectTZGSDQ", "");
             DQTaxis(DQType);
        }
        /// <summary>
        /// 初始化标题
        /// </summary>
        private void InitTitle()
        {
            for(int i=0;i<VCCount;++i)
            {
                switch(i)
                {
                    case 0:
                        VoltageClass[i] = "110(66)";
                        break;
                    case 1:
                        VoltageClass[i] = "35";
                        break;
                    case 2:
                        VoltageClass[i] = "10(6、20)";
                        break;
                    case 3:
                        VoltageClass[i] = "低压工程";
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 返回当年的资金
        /// </summary>
        /// <param name="year">current year</param>
        /// <param name="DQN">地区</param>
        /// <param name="BianInfo">电压等级</param>
        /// <returns></returns>
        public double SelectAmount(int year,string DQN,string BianInfo)
        {
            string con = null;
            double Amount = 0;
            try
            {
                     con = "BuildYear ='" + year + "' and (" + BianInfo + ")" +
                              " and DQ='" + DQN + "'";
                    Amount = (double)Services.BaseService.GetObject("SelectTZGSSUMAmount", con);
            }
            catch (System.Exception e)
            {

            }

            return Amount;
        }
        /// <summary>
        /// 把类型转换成显示格式
        /// </summary>
        public  string DataChangeType(string strTitle)
        {
            string temp = null;
            switch (strTitle)
                {
                    case"市辖供电区":
                        temp="市辖供电区";
                    break;
                    case "县级直供直管":
                        temp = "其中：直供直管";
                        break;
                    case "县级控股":
                        temp = "控股";
                        break;
                    case "县级参股":
                        temp = "参股";
                        break;
                    case "县级代管":
                        temp = "代管";
                        break;

                    default:
                        break;
                }
                return temp;
        }
        /// <summary>
        /// 把类型转换成数据库格式
        /// </summary>
        public  string  TypeChangeData(string Title)
        {
            string temp = null;
            switch(Title)
            {
                case "市辖供电区":
                    temp = "市辖供电区";
                    break;
                case "其中：直供直管":
                   temp= "县级直供直管";
                    break;
                case "控股":
                    temp = "县级控股";
                    break;
                case "参股":
                    temp = "县级参股";
                    break;
                case "代管":
                    temp = "县级代管";
                    break;

                default:
                    break;
            }
            return temp;
        }
        /// <summary>
        /// 重新排序地区,按照报表的格式
        /// </summary>
        /// <param name="ILDQType"></param>
        private void  DQTaxis(IList ILDQType)
        {
            for (int i = 0; i < ILDQType.Count; ++i)
            {
                switch(i)
                {
                    case 0:
                        DQType[i] = "市辖供电区";
                        break;
                    case 1:
                        DQType[i] = "县级直供直管";
                        break;
                    case 2:
                        DQType[i] = "县级控股";
                        break;
                    case 3:
                        DQType[i] = "县级参股";
                        break;
                    case 4:
                        DQType[i] = "县级代管";
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
