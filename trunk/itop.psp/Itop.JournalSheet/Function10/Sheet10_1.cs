using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Common;
using Itop.Domain.Table;


/******************************************************************************************************
 *  ClassName：Sheet10_1
 *  Action：用来生成Sheet10_1报表的调用方法
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表ps_Table_TZGS
 * 时间：2010-10-11。
 *********************************************************************************************************/
namespace Itop.JournalSheet.Function10
{
    class Sheet10_1
    {
        private int ColCount = 0;//列数
        private int RowCount = 0;//行数
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private const int VPCount = 9;//项目个数
        private const int VCCount = 3;//电压等级个数
        private const int TTCount = 7;//编号，类型个数
        private string[] Title0 = new string[TTCount];
        private string[] TitleType = new string[TTCount];
        private string[] TitleType1 = new string[TTCount];
        private string[] VoltageClass = new string[VCCount];
        private string[] VoltageProject = new string[VPCount];
        private const string TransformerEngineering = "变电工程";
        private const string RailwayLineEngineering = "线路工程";
        private const string DistributorEngineering = "配变工程";
        private Function.PublicFunction PF = new Function.PublicFunction();


        private void  InitDate()
        {
            string title = null;
            string title1 = null;
            int IntTemp = 0;
            for (int i = 0; i < VPCount; ++i)
            {
                if(i>=0&&i<=1)
                {
                    IntTemp = i + 1;
                    Title0[i] = IntTemp.ToString();
                }
                if(i>=2&&i<=5)
                {
                    IntTemp=i-1;
                    Title0[i] = "2." + IntTemp.ToString();
                }
                if(i==6)
                {
                    IntTemp=i-3;
                    Title0[i] = IntTemp.ToString(); 
                }
                
                switch(i)
                {
                    case 0:
                        title = "市辖供电区";
                        title1 = title;
                        VoltageClass[i] = "110（66）";
                        VoltageProject[i] = TransformerEngineering;
                        break;
                    case 1:
                        title = "县级供电区";
                        title1 = title;
                        VoltageClass[i] = "35";
                        VoltageProject[i] = RailwayLineEngineering;
                        break;
                    case 2:
                        title = "其中：直供直管";
                        title1 = "县级直供直管";
                        VoltageClass[i] = "10（6、20）";
                        VoltageProject[i] = TransformerEngineering;
                        break;
                    case 3:
                        title = "控股";
                        title1 = "县级控股";
                        VoltageProject[i] = RailwayLineEngineering;
                        break;
                    case 4:
                        title = "参股";
                        title1 = "县级参股";
                        VoltageProject[i] = DistributorEngineering;
                        break;
                    case 5:
                        title = "代管";
                        title1 = "县级代管";
                        VoltageProject[i] = RailwayLineEngineering;
                        break;
                    case 6:
                        title = "全地区合计";
                        title1 = title;
                        VoltageProject[i] = "低压工程";
                        break;
                    case 7:
                        title1 = title;
                        VoltageProject[i] = "其它工程";
                        break;
                    case 8:
                        
                        VoltageProject[i] = "合计";
                        break;

                    default:
                        break;
                }
                if (i < VPCount-2)
                {
                    TitleType[i] = title;
                    TitleType1[i] = title1;//用这个数组是与数据库中的字段对照
                }
            }
            
        }
        /// <summary>
        /// 写入行
        /// </summary>
        ///<param name="IntRow">起始行数</param>
        ///<param name="IntCol">起始列数</param>
        ///<param name="obj"></param>
        ///<param name="Title"></param>
        private void WriteData(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol, string Title)
        {
            int  year = 2010;
            int Voltage = 0;
            string ColTitle = null;
            int RowRI = 0;
            //Ps_Table_TZGS BianInfo = null;
            double SumAmount = 0.000;
            InitDate();
            #region 填写除了县级供电区和全地区合计的数据
            
            for (int j = 0; j < TTCount; ++j)//有7个类型
            {
                PF.CreateSheetView(obj, VPCount, 1, IntRow + j * VPCount, IntCol, Title0[j]);//编号
                PF.CreateSheetView(obj, VPCount, 1, IntRow + j * VPCount, IntCol + 1, TitleType[j]);//类型
                for (int c=0;c<VCCount;++c)
                {
                    PF.CreateSheetView(obj, 2, 1, (IntRow + c*(VCCount-1))+j*VPCount, IntCol + 2, VoltageClass[c]);//电压等级
                }
                for (int i = 0; i < VPCount; ++i)//9个项目
                {
                    if(i<VPCount-3)
                    {
                        PF.CreateSheetView(obj, 1, 1, (IntRow + i) + j * VPCount, IntCol + 3, VoltageProject[i]);//项目
                    }
                    else
                    {
                        PF.CreateSheetView(obj, 1, 2, (IntRow + i) + j * VPCount, IntCol + 2, VoltageProject[i]);//项目
                    }
                    //添加数据从数据库读取的数据
                    //这个循环是从列等于4开始
                    year = 2010;
                    if(i<2)
                    {
                        Voltage = 0;
                    }
                    if(i==2&&i<4)
                    {
                        Voltage=1;
                    }
                    if(i==4&&i<6)
                    {
                        Voltage=2;
                    }
                    for (int m = 4; m < obj.ColumnCount; ++m)//column
                    {
                        if(j!=1&&j!=TTCount-1)//不是县级供电区，全地区合计
                        {
                            switch (i)
                            {
                                case 0://变电工程
                                    SumAmount = ReturnVoltageClass(VoltageClass[Voltage], year, TitleType1[j], "bian");
                                    obj.SetValue((IntRow + i) + j * VPCount, IntCol + m, SumAmount);
                                    if (m == obj.ColumnCount - 1)//最后一列
                                    {
                                        obj.Cells[(IntRow + i) + j * VPCount, IntCol + m].Formula = "SUM(F" + ((IntRow + i) + j * VPCount + 1) + ":J" + ((IntRow + i) + j * VPCount + 1) + ")";
                                    }
                                    break;
                                case 1://线路工程
                                    SumAmount = ReturnVoltageClass(VoltageClass[Voltage], year, TitleType1[j], "line");
                                    obj.SetValue((IntRow + i) + j * VPCount, IntCol + m, SumAmount);
                                    if (m == obj.ColumnCount - 1)//最后一列
                                    {
                                        obj.Cells[(IntRow + i) + j * VPCount, IntCol + m].Formula = "SUM(F" + ((IntRow + i) + j * VPCount + 1) + ":J" + ((IntRow + i) + j * VPCount + 1) + ")";
                                    }
                                    break;
                                case 2://变电工程
                                    SumAmount = ReturnVoltageClass(VoltageClass[Voltage], year, TitleType1[j], "bian");
                                    obj.SetValue((IntRow + i) + j * VPCount, IntCol + m, SumAmount);
                                    if (m == obj.ColumnCount - 1)//最后一列
                                    {
                                        obj.Cells[(IntRow + i) + j * VPCount, IntCol + m].Formula = "SUM(F" + ((IntRow + i) + j * VPCount + 1) + ":J" + ((IntRow + i) + j * VPCount + 1) + ")";
                                    }
                                    break;
                                case 3://线路工程
                                    SumAmount = ReturnVoltageClass(VoltageClass[Voltage], year, TitleType1[j], "line");
                                    obj.SetValue((IntRow + i) + j * VPCount, IntCol + m, SumAmount);
                                    if (m == obj.ColumnCount - 1)//最后一列
                                    {
                                        obj.Cells[(IntRow + i) + j * VPCount, IntCol + m].Formula = "SUM(F" + ((IntRow + i) + j * VPCount + 1) + ":J" + ((IntRow + i) + j * VPCount + 1) + ")";
                                    }
                                    break;
                                case 4://配变工程
                                    SumAmount = ReturnVoltageClass(VoltageClass[Voltage], year, TitleType1[j], "pw-pb");
                                    obj.SetValue((IntRow + i) + j * VPCount, IntCol + m, SumAmount);
                                    if (m == obj.ColumnCount - 1)//最后一列
                                    {
                                        obj.Cells[(IntRow + i) + j * VPCount, IntCol + m].Formula = "SUM(F" + ((IntRow + i) + j * VPCount + 1) + ":J" + ((IntRow + i) + j * VPCount + 1) + ")";
                                    }
                                    break;
                                case 5://线路工程
                                    SumAmount = ReturnVoltageClass(VoltageClass[Voltage], year, TitleType1[j], "pw-line");
                                    obj.SetValue((IntRow + i) + j * VPCount, IntCol + m, SumAmount);
                                    if (m == obj.ColumnCount - 1)//最后一列
                                    {
                                        obj.Cells[(IntRow + i) + j * VPCount, IntCol + m].Formula = "SUM(F" + ((IntRow + i) + j * VPCount + 1) + ":J" + ((IntRow + i) + j * VPCount + 1) + ")";
                                    }
                                    break;
                                case 6://低压工程
                                    obj.Cells[(IntRow + i) + j * VPCount, IntCol + m].Locked = false;//可写
                                    if (m == obj.ColumnCount - 1)//最后一列
                                    {
                                        //obj.Cells[(IntRow + i) + j * VPCount, IntCol + m].Formula = "SUM(F" + ((IntRow + i) + j * VPCount + 1) + ":J" + ((IntRow + i) + j * VPCount + 1) + ")";

                                        RowRI = ((IntRow + i) + j * VPCount+1);
                                        obj.Cells[(IntRow + i) + j * VPCount, IntCol + m].Formula = "F" + RowRI + "+G" + RowRI + "+H" + RowRI + "+I" + RowRI + "+J" + RowRI;
                                        obj.Cells[(IntRow + i) + j * VPCount, IntCol + m].Locked = true;//只读
                                    }
                                    break;
                                case 7://其它工程
                                    obj.Cells[(IntRow + i) + j * VPCount, IntCol + m].Locked = false;//可写
                                    if (m == obj.ColumnCount - 1)//最后一列
                                    {
                                        //obj.Cells[(IntRow + i) + j * VPCount, IntCol + m].Formula = "SUM(F" + ((IntRow + i) + j * VPCount + 1) + ":J" + ((IntRow + i) + j * VPCount + 1) + ")";

                                        RowRI = ((IntRow + i) + j * VPCount+1);
                                        obj.Cells[(IntRow + i) + j * VPCount, IntCol + m].Formula = "F" + RowRI + "+G" + RowRI + "+H" + RowRI + "+I" + RowRI + "+J" + RowRI;
                                        obj.Cells[(IntRow + i) + j * VPCount, IntCol + m].Locked = true;//只读
                                    }
                                    break;
                                case 8://合计
                                    switch (m)
                                    {
                                        case 4:
                                            ColTitle = "E";
                                            break;
                                        case 5:
                                            ColTitle = "F";
                                            break;
                                        case 6:
                                            ColTitle = "G";
                                            break;
                                        case 7:
                                            ColTitle = "H";
                                            break;
                                        case 8:
                                            ColTitle = "I";
                                            break;
                                        case 9:
                                            ColTitle = "J";
                                            break;

                                        default:
                                            break;
                                    }
                                    if (m == obj.ColumnCount - 1)//最后一列
                                    {
                                        obj.Cells[(IntRow + i) + j * VPCount, IntCol + m].Locked = true;//只读
                                        obj.Cells[(IntRow + i) + j * VPCount, IntCol + m].Formula = "SUM(F" + ((IntRow + i) + j * VPCount + 1) + ":J" + ((IntRow + i) + j * VPCount + 1) + ")";
                                    }
                                    else
                                    {
                                        obj.Cells[(IntRow + i) + j * VPCount, IntCol + m].Formula = "SUM(" + ColTitle + ((IntRow + i) + j * VPCount - 7) + ":" + ColTitle + ((IntRow + i) + j * VPCount) + ")";
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }

                         year+=1;

                    }

                }
            }
            #endregion
            //因为先计算出县级一下的数据，所以最后写入县级数据，和全地区数据
            #region 填写县级供电区和全地区合计的数据
            
            IntRow = 14;
            IntCol = 0;
            int RangeRow = 45;//距离要写入的下一行隔着45行
            for(int i=0;i<2;++i)//有两个地区最后写数据
            {
                for (int r = 0; r < VPCount; ++r)
                {
                    for (int j = 4; j < obj.ColumnCount; ++j)
                    {
                        switch (j)
                        {
                            case 4:
                                ColTitle = "E";
                                if (i == 0)
                                {
                                    obj.Cells[IntRow + r + i * RangeRow, IntCol + j].Formula = ColTitle + (IntRow + r + i * RangeRow + VPCount + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow + VPCount * 2 + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow + VPCount * 3 + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow + VPCount * 4 + 1);//隔着45行在写入数据
                                }
                                if (i == 1)
                                {
                                    obj.Cells[IntRow + r + i * RangeRow, IntCol + j].Formula = ColTitle + (IntRow + r + i * RangeRow - VPCount * 6 + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow - VPCount * 5 + 1);
                                }
                                break;
                            case 5:
                                ColTitle = "F";
                                if (i == 0)
                                {
                                    obj.Cells[IntRow + r + i * RangeRow, IntCol + j].Formula = ColTitle + (IntRow + r + i * RangeRow + VPCount + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow + VPCount * 2 + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow + VPCount * 3 + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow + VPCount * 4 + 1);//隔着45行在写入数据
                                }
                                if (i == 1)
                                {
                                    obj.Cells[IntRow + r + i * RangeRow, IntCol + j].Formula = ColTitle + (IntRow + r + i * RangeRow - VPCount * 6 + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow - VPCount * 5 + 1);
                                }
                                break;
                            case 6:
                                ColTitle = "G";
                                if (i == 0)
                                {
                                    obj.Cells[IntRow + r + i * RangeRow, IntCol + j].Formula = ColTitle + (IntRow + r + i * RangeRow + VPCount + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow + VPCount * 2 + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow + VPCount * 3 + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow + VPCount * 4 + 1);//隔着45行在写入数据
                                }
                                if (i == 1)
                                {
                                    obj.Cells[IntRow + r + i * RangeRow, IntCol + j].Formula = ColTitle + (IntRow + r + i * RangeRow - VPCount * 6 + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow - VPCount * 5 + 1);
                                }
                                break;
                            case 7:
                                ColTitle = "H";
                                if (i == 0)
                                {
                                    obj.Cells[IntRow + r + i * RangeRow, IntCol + j].Formula = ColTitle + (IntRow + r + i * RangeRow + VPCount + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow + VPCount * 2 + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow + VPCount * 3 + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow + VPCount * 4 + 1);//隔着45行在写入数据
                                }
                                if (i == 1)
                                {
                                    obj.Cells[IntRow + r + i * RangeRow, IntCol + j].Formula = ColTitle + (IntRow + r + i * RangeRow - VPCount * 6 + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow - VPCount * 5 + 1);
                                }
                                break;
                            case 8:
                                ColTitle = "I";
                                if (i == 0)
                                {
                                    obj.Cells[IntRow + r + i * RangeRow, IntCol + j].Formula = ColTitle + (IntRow + r + i * RangeRow + VPCount + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow + VPCount * 2 + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow + VPCount * 3 + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow + VPCount * 4 + 1);//隔着45行在写入数据
                                }
                                if (i == 1)
                                {
                                    obj.Cells[IntRow + r + i * RangeRow, IntCol + j].Formula = ColTitle + (IntRow + r + i * RangeRow - VPCount * 6 + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow - VPCount * 5 + 1);
                                }
                                break;
                            case 9:
                                ColTitle = "J";
                                if (i == 0)
                                {
                                    obj.Cells[IntRow + r + i * RangeRow, IntCol + j].Formula = ColTitle + (IntRow + r + i * RangeRow + VPCount + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow + VPCount * 2 + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow + VPCount * 3 + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow + VPCount * 4 + 1);//隔着45行在写入数据
                                }
                                if (i == 1)
                                {
                                    obj.Cells[IntRow + r + i * RangeRow, IntCol + j].Formula = ColTitle + (IntRow + r + i * RangeRow - VPCount * 6 + 1) + "+" + ColTitle + (IntRow + r + i * RangeRow - VPCount * 5 + 1);
                                }
                                break;
                            case 10://最后一列
                                RowRI = (IntRow + r + i * RangeRow + 1);
                                obj.Cells[IntRow + r + i * RangeRow, IntCol + j].Formula = "F" + RowRI + "+G" + RowRI + "+H" + RowRI + "+I" + RowRI + "+J" + RowRI;
                                break;

                            default:
                                break;
                        }
                    }
                }
            #endregion
            }
        }
        /// <summary>
        /// return Amount
        /// </summary>
        /// <param name="obj">object</param>
        /// <param name="year">year</param>
        /// <param name="Territory">地区</param>
        /// <param name="BianInfo">是否为bian，line，pw-bian,pw-pb,pw-kg,pw</param>
        /// <returns></returns>
        private double ReturnVoltageClass(object obj, int year, string Territory, string Col4)
        {
            double temp = 0.0000;
            string con = "";
            //Ps_Table_TZGS PTT = null;
            
            try
            {
                if (obj.ToString() == VoltageClass[0])
                {
                    con = "BuildYear='" + year.ToString() + "' and Col4='" + Col4 + "'  and (BianInfo like '" + 110 + "@%' or BianInfo like '" + 66 + "@%') and DQ='" + Territory + "'";
                }
                if (obj.ToString() == VoltageClass[1])
                {
                    con = "BuildYear='" + year + "' and Col4='" + Col4 + "'  and BianInfo like '" + 35 + "@%' and DQ='" + Territory + "'";
                }
                if (obj.ToString() == VoltageClass[2])
                {
                    con = "BuildYear='" + year + "' and Col4='" + Col4 + "'  and (BianInfo like '" + 10 + "@%' or BianInfo like '" + 6 + "@%'  or BianInfo like '" + 20 + "@%')  and DQ='" + Territory + "'";
                }
                temp = (double)Services.BaseService.GetObject("SelectTZGSSUMAmount", con);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show("数据库没有数据！！！");
                //MessageBox.Show(e.Message.ToString());
                temp = 0.0;
            }
            return temp;
        }
        /// <summary>
        /// 设置某列为只读
        /// </summary>
        /// <param name="obj">sheetView对象</param>
        /// <param name="IntCol">列数</param>
        public  void ColReadOnly(FarPoint.Win.Spread.SheetView obj, int IntCol)
        {
            for (int i = 0; i < IntCol; i++)
            {
                    obj.Columns[i].Locked = true;//列设置为只读
            }
        }
        /// <summary>
        /// 加载10_1表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet10_1Title(FarPoint.Win.Spread.SheetView obj,string Title)
        {
            int IntColCount = 11;
            int IntRowCount = 64 + 1 + 3;//标题占3行，类型占1行，
            string title = null;

            obj.SheetName = Title;
            obj.Columns.Count = IntColCount;
            obj.Rows.Count = IntRowCount;
            IntCol = obj.Columns.Count;

            PF.Sheet_GridandCenter(obj);//画线，居中
            ColReadOnly(obj, IntColCount);
            //obj.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;

            string strTitle = "";
            IntRow = 3;
            strTitle = Title;
            PF.CreateSheetView(obj, IntRow, IntCol, 0, 0, Title);
            PF.SetSheetViewColumnsWidth(obj, 0, Title);
            IntCol = 1;

            //加载数据
            WriteData(obj, IntRow + 2, IntCol - 1, Title);

            strTitle = " 编     号 ";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 类     型 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 电压等级 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 项  目";
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

            strTitle = " “十二五”合计 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;

            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
    }
}
