using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using Itop.Client.Common;
using Itop.Domain.Table;
using Itop.Domain.Stutistics;
/******************************************************************************************************
 *  ClassName：Sheet_2
 *  Action：表10-1  XX市输变电工程综合造价表的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表ps_table_tzmx（投资明细）ps_table_tzgs（投资估算）project_sum（造价明细）
 * 项目名称分不同的电压等级，每个电压等级的容量不一定相同
 * 年份：2010-10-11
 *********************************************************************************************************/
namespace Itop.JournalSheet.FunctionSupplement
{
    class Sheet_2
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值。
        private string unit = "MVA";
        private string strUnit = "万元/";
        private string strUnit1 = "km";
        private string strUnit2 = "座";
        private string strUnit3 = "台";

        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();


        private struct HighPressure//高压结构体
        {
            public IList INew;//新建
            public IList IExtends;//扩建
            public IList IRebuild;//改造
            public int[] Volume;//用来保存0新建，1扩建，2改造的容量的个数
            public IList[] INewVolume;//新建，电压等级的容量
            public IList[] IExtendsVolume;//扩建，电压等级的容量
            public IList[] IRebuildVolume;//改造，电压等级的容量
            public IList ILineAndJK;//架空和电缆的数据
            public int RowsCount;//行数
            public int titleCount;//左侧标题个数
            public string[] strTitle;//左侧标题
            public IList[]  Length;//架空线路或电缆的长度
            public Project_Sum list;//临时变量用于存放造价数据
        }
        private struct LowPressure//低压结构体
        {
            public IList ILow;//低压所有数据集合
            public const int IntCount = 10;//每条数据包含数据的个数
            public int RowsCount;//行数
            public string[] strTitle;
            public Project_Sum list;//临时变量用于存放造价数据
            public string[] strType;//造价类型
        }
        private LowPressure LP = new LowPressure();
        private HighPressure HP = new HighPressure();
        
        private void InitTitle()
        {
            HP.titleCount=5;
            HP.strTitle=new string[HP.titleCount];
            for (int i = 0; i < HP.titleCount;++i )
            {
                switch (i)
                {
                    case 0:
                        HP.strTitle[i] = "新建变电站";
                        break;
                    case 1:
                        HP.strTitle[i] = "扩建变电站";
                        break;
                    case 2:
                        HP.strTitle[i] = "改造变电站";
                        break;
                    case 3:
                        HP.strTitle[i] = "新建架空线路";
                        break;
                    case 4:
                        HP.strTitle[i] = "新建电缆线路(电气部分)";
                        break;

                    default:
                        break;
                }
            }

            LP.strType=new string[2];
            LP.strType[0]="架空";
            LP.strType[1]="电缆";

            LP.strTitle = new string[LowPressure.IntCount - 1];//不包含标题
            for (int i = 0; i < (LowPressure.IntCount - 1);++i )
            {
                switch (i)
                {
                    case 0:
                        LP.strTitle[i] = "新建架空线路";
                        break;
                    case 1:
                        LP.strTitle[i] = "新建电缆线路(电气部分)";
                        break;
                    case 2:
                        LP.strTitle[i] = "开关站";
                        break;
                    case 3:
                        LP.strTitle[i] = "箱式变电站";
                        break;
                    case 4:
                        LP.strTitle[i] = "配电室";
                        break;
                    case 5:
                        LP.strTitle[i] = "环网柜";
                        break;
                    case 6:
                        LP.strTitle[i] = "柱上变";
                        break;
                    case 7:
                        LP.strTitle[i] = "电缆分支箱";
                        break;
                    case 8:
                        LP.strTitle[i] = "低压主干线";
                        break;

                    default:
                        break;
                }
            }
        }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet_2Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            InitTitle();
            int IntColCount = 7;
            HP.RowsCount = ReturnRows(FB);//高压行数
            LP.ILow=ReturnLowRows("b");//低压
            LP.RowsCount = LP.ILow.Count * LowPressure.IntCount;
            int IntRowCount = 1 + LP.RowsCount+HP.RowsCount + 2 + 3;//标题占3行，分区类型占2行，1是其它用
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


            strTitle = " 类     别";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 项目名称";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 电压等级(kV) ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 规     模";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 单位工程综合造价 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 单     位 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 备     注";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;

            SetLeftTitle(obj, IntRow, IntCol);
            WriteHeightData(FB, obj, IntRow, IntCol);//高压数据
            WriteLowData(FB, obj, (IntRow+HP.RowsCount), IntCol);//低压数据
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 左侧标题
        /// </summary>
        private void SetLeftTitle(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            PF.CreateSheetView(obj, HP.RowsCount, 1, IntRow, 0, "高压配电网");
            PF.CreateSheetView(obj, LP.RowsCount, 1, (IntRow + HP.RowsCount), 0, "中低压配电网");
            PF.CreateSheetView(obj, 1, 1, (IntRow + HP.RowsCount + LP.RowsCount), 0, "其它");
/*高压部分*******************************************************************************************/
            PF.CreateSheetView(obj, HP.Volume[0], 1, IntRow, 1, HP.strTitle[0]);//新建
                PF.CreateSheetView(obj, HP.Volume[1], 1, IntRow += HP.Volume[0], 1, HP.strTitle[1]);//扩建
                PF.CreateSheetView(obj, HP.Volume[2], 1, IntRow += HP.Volume[1], 1, HP.strTitle[2]);//改造
                PF.CreateSheetView(obj, HP.INew.Count, 1, IntRow += HP.Volume[2], 1, HP.strTitle[3]);//新建架空
                PF.CreateSheetView(obj, HP.INew.Count, 1, IntRow += HP.INew.Count, 1, HP.strTitle[4]);//新建电缆
/* ********************************************************************************************************************** */
/** 低压部分***********************************************************************************************************/
                Ps_Table_TZGS pTT = new Ps_Table_TZGS();
            for(int i=0;i<LP.ILow.Count;++i)
            {
                pTT = (Ps_Table_TZGS)LP.ILow[i];
                for(int j=0;j<LowPressure.IntCount;++j)
                {
                    if(j==0)
                    {
                        PF.CreateSheetView(obj, 1, obj.ColumnCount, (5+HP.RowsCount  + j + i * LowPressure.IntCount), 1, pTT.Title);//标题
                    }
                    else
                    {
                        PF.CreateSheetView(obj, 1, 1, (5 + HP.RowsCount + j + i * LowPressure.IntCount), 1, LP.strTitle[j - 1]);//
                    }
                }
            }
        }
        /// <summary>
        /// 写入高压数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        private void WriteHeightData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            int RowTemp = IntRow;
            double Amount = 0;
            IList list = null;
            Ps_Table_TZGS pTT = new Ps_Table_TZGS();
            Ps_Table_TZMX pTT1 = new Ps_Table_TZMX();
            //IList list = null;
            //list = SelectVoltageClass(FB, "新建");
            for (int i = 0; i < HP.INew.Count; i++)
            {
                pTT = (Ps_Table_TZGS)HP.INew[i];
                PF.CreateSheetView(obj, HP.INewVolume[i].Count, 1, RowTemp, 2, pTT.BianInfo);//新建
                list = HP.INewVolume[i];
                for (int j = 0; j < list.Count; ++j)
                {
                    pTT1 = (Ps_Table_TZMX)list[j];
                    PF.CreateSheetView(obj, 1, 1, (RowTemp + j), 3, pTT1.Vol.ToString() + unit);//新建
                    Amount = ReturnAmount(pTT1, pTT, "sub");
                    obj.SetValue(RowTemp, 4, Amount);//单位工程综合造价
                    obj.SetValue(RowTemp,5,strUnit+unit);//单位
                    obj.SetValue(RowTemp, 6, pTT.Col1);//备注
                }
                if (HP.INewVolume[i].Count == 0)
                {
                    RowTemp += 1;
                }
                else
                {
                    RowTemp += HP.INewVolume[i].Count;
                }
            }
            //list = SelectVoltageClass(FB, "扩建");
            for (int i = 0; i < HP.IExtends.Count; i ++)
            {
                pTT = (Ps_Table_TZGS)HP.IExtends[i];
                PF.CreateSheetView(obj, HP.IExtendsVolume[i].Count, 1, RowTemp , 2, pTT.BianInfo);//扩建
                list = HP.IExtendsVolume[i];
                for (int j = 0; j < list.Count; ++j)
                {
                    pTT1 = (Ps_Table_TZMX)list[j];
                    PF.CreateSheetView(obj, 1, 1, (RowTemp + j), 3, pTT1.Vol.ToString() + unit);//扩建

                    Amount = ReturnAmount(pTT1, pTT, "sub");
                    obj.SetValue(RowTemp, 4, Amount);//单位工程综合造价   
                    obj.SetValue(RowTemp, 5, strUnit + unit);//单位
                    obj.SetValue(RowTemp, 6, pTT.Col1);//备注
                }
                if (HP.IExtendsVolume[i].Count==0)
                {
                    RowTemp += 1;
                }
                else
                {
                    RowTemp += HP.IExtendsVolume[i].Count;
                }
            }
            //list = SelectVoltageClass(FB, "改造");
            for (int i = 0; i < HP.IRebuild.Count; i ++)
            {
                pTT = (Ps_Table_TZGS)HP.IRebuild[i];
                PF.CreateSheetView(obj, HP.IRebuildVolume[i].Count, 1, RowTemp , 2, pTT.BianInfo);//改造
                list = HP.IRebuildVolume[i];
                for (int j = 0; j < list.Count; ++j)
                {
                    pTT1 = (Ps_Table_TZMX)list[j];
                    PF.CreateSheetView(obj, 1, 1, (RowTemp + j), 3, pTT1.Vol.ToString() + unit);//改造

                    Amount = ReturnAmount(pTT1, pTT, "sub");
                    obj.SetValue(RowTemp, 4, Amount);//单位工程综合造价
                    obj.SetValue(RowTemp, 5, strUnit + unit);//单位
                    obj.SetValue(RowTemp, 6, pTT.Col1);//备注
                }
                if (HP.IRebuildVolume[i].Count==0)
                {
                    RowTemp += 1;
                }
                else
                {
                    RowTemp += HP.IRebuildVolume[i].Count;
                }
            }
            //架空线路
            for(int i=0;i<HP.INew.Count;++i)
            {
                pTT = (Ps_Table_TZGS)HP.INew[i];
                ////if (HP.Length[i].Count == 0 || HP.Length[i] == null)
                ////{
                ////    intTemp = 1;
                ////}
                ////else
                ////{
                ////    intTemp = HP.Length[i].Count;
                ////}
                
                PF.CreateSheetView(obj, 1, 1, RowTemp +i, 2, pTT.BianInfo);//新建架空电压等级
                PF.CreateSheetView(obj, 1, 1, RowTemp+i, 3, pTT.Length.ToString());//新建架空线路
                list = HP.Length[i];
                for (int j = 0; j < list.Count;++j )
                {
                    pTT1 = (Ps_Table_TZMX)list[j];
                    Amount = ReturnAmount(pTT1, pTT, "line");
                    obj.SetValue(RowTemp + i, 4, Amount);//单位工程综合造价  
                    obj.SetValue(RowTemp, 5, strUnit + strUnit1);//单位
                    obj.SetValue(RowTemp, 6, pTT.Col1);//备注
                }

            }
            RowTemp += HP.INew.Count;
            //新建电缆线路
            for(int i=0;i<HP.INew.Count;++i)
            {
                pTT = (Ps_Table_TZGS)HP.INew[i];
                ////if (HP.Length[i].Count == 0 || HP.Length[i] == null)
                ////{
                ////    intTemp = 1;
                ////}
                ////else
                ////{
                ////    intTemp = HP.Length[i].Count;
                ////}
                PF.CreateSheetView(obj, 1, 1, RowTemp+i , 2, pTT.BianInfo);//新建电缆电压等级
                PF.CreateSheetView(obj, 1, 1, RowTemp+i, 3, pTT.Length2.ToString());//新建电缆线路
                list = HP.Length[i];
                for (int j = 0; j < list.Count; ++j)
                {
                    pTT1 = (Ps_Table_TZMX)list[j];

                    Amount = ReturnAmount(pTT1, pTT, "line");

                    obj.SetValue(RowTemp + i, 4, Amount);//单位工程综合造价  
                    obj.SetValue(RowTemp, 5, strUnit + strUnit1);//单位
                    obj.SetValue(RowTemp, 6, pTT.Col1);//备注
                }
            }
            RowTemp += HP.INew.Count;
        }
        /// <summary>
        /// 写入低压数据
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        private void WriteLowData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            Ps_Table_TZGS pTT = new Ps_Table_TZGS();
            Ps_Table_TZGS pTT1 = new Ps_Table_TZGS();
            Ps_Table_TZGS pTT2 = new Ps_Table_TZGS();
            string VolatageClass = null;
            string UnitTemp=null;
            string strTitle = null;
            IList listKG = null;
            IList listPB = null;
            for (int i = 0; i < LP.ILow.Count; ++i)
            {
                pTT = (Ps_Table_TZGS)LP.ILow[i];
                VolatageClass = ReturnLowVoltageClass("b", pTT.Title);
                listKG = ReturnLowRows("d");//开关
                listPB = ReturnLowRows("c");//配变
                pTT1 = (Ps_Table_TZGS)listPB[i];
                pTT2 = (Ps_Table_TZGS)listKG[i];
                for (int j = 0; j < LowPressure.IntCount; ++j)
                {
                    if(j==0||j==1)
                    {
                        UnitTemp = strUnit + strUnit1;
                        PF.CreateSheetView(obj, 1, 1, (IntRow + 1 + j + i * LowPressure.IntCount), 2, VolatageClass);//
                        PF.CreateSheetView(obj, 1, 1, (IntRow + 1 + j + i * LowPressure.IntCount), 3, pTT.Length.ToString());//
                        ReturnFabricationCost(LP.strType[j], VolatageClass);
                        if(LP.list!=null)
                        {
                            PF.CreateSheetView(obj, 1, 1, (IntRow + 1 + j + i * LowPressure.IntCount), 4, LP.list.Num.ToString());//
                            obj.SetValue((IntRow + 1 + j + i * LowPressure.IntCount), 6, LP.list.L4);
                        }
                        obj.SetValue((IntRow + 1 + j + i * LowPressure.IntCount), 5, UnitTemp);
                    }
                    else if (j == 2 || j == 3 || j == 4)//开关站,箱式变电站,配电室
                    {
                        UnitTemp = strUnit + strUnit2;
                        obj.SetValue((IntRow + 1 + j + i * LowPressure.IntCount), 5, UnitTemp);
                    }
                    else if (j == 5 || j == 6 || j == 7)//环网柜,柱上变,电缆分支箱
                    {
                        UnitTemp = strUnit + strUnit3;
                        obj.SetValue((IntRow + 1 + j + i * LowPressure.IntCount), 5, UnitTemp);
                    }
                    if (j == LowPressure.IntCount-2||j==LowPressure.IntCount-1)//最后一行
                    {
                        for (int col = 2; col < obj.ColumnCount; ++col)
                        {
                            //obj.SetValue((IntRow + 1 + j + i * LowPressure.IntCount), col,"kk");
                            obj.Cells[(IntRow + 1 + j + i * LowPressure.IntCount), col].Locked = false;//手写
                        }
                    }
                    if(j==LowPressure.IntCount-1)
                    {
                        //鼠标右键点击出现菜单

                    }

                    strTitle = (string)PF.ReturnStr(obj, (IntRow + 1 + j + i * LowPressure.IntCount), 1);
                    switch (strTitle)
                    {
                        case "开关站":
                            PF.CreateSheetView(obj, 1, 1, (IntRow + 1 + j + i * LowPressure.IntCount), 2, VolatageClass);//
                            PF.CreateSheetView(obj, 1, 1, (IntRow + 1 + j + i * LowPressure.IntCount), 3, pTT2.Num1.ToString());//
                            ReturnFabricationCostTitle("常规", VolatageClass, strTitle);
                            if (LP.list == null)
                            {
                                //MessageBox.Show(strTitle + "没有数据！！！");
                            }
                            else
                            {
                                PF.CreateSheetView(obj, 1, 1, (IntRow + 1 + j + i * LowPressure.IntCount), 4, LP.list.Num.ToString());//
                                obj.SetValue((IntRow + 1 + j + i * LowPressure.IntCount), 6, LP.list.L4);
                            }
                            break;
                        case "箱式变电站":
                            PF.CreateSheetView(obj, 1, 1, (IntRow + 1 + j + i * LowPressure.IntCount), 2, VolatageClass);//
                            PF.CreateSheetView(obj, 1, 1, (IntRow + 1 + j + i * LowPressure.IntCount), 3, pTT1.Num3.ToString());//
                            ReturnFabricationCostTitle("常规", VolatageClass, strTitle);
                            if (LP.list == null)
                            {
                                //MessageBox.Show(strTitle + "没有数据！！！");
                            }
                            else
                            {
                                PF.CreateSheetView(obj, 1, 1, (IntRow + 1 + j + i * LowPressure.IntCount), 4, LP.list.Num.ToString());//
                                obj.SetValue((IntRow + 1 + j + i * LowPressure.IntCount), 6, LP.list.L4);
                            }
                            break;
                        case "配电室":
                            PF.CreateSheetView(obj, 1, 1, (IntRow + 1 + j + i * LowPressure.IntCount), 2, VolatageClass);//
                            PF.CreateSheetView(obj, 1, 1, (IntRow + 1 + j + i * LowPressure.IntCount), 3, pTT1.Num1.ToString());//
                            ReturnFabricationCostTitle("常规", VolatageClass, strTitle);
                            if(LP.list==null)
                            {
                                //MessageBox.Show(strTitle + "没有数据！！！");
                            }else
                            {
                                PF.CreateSheetView(obj, 1, 1, (IntRow + 1 + j + i * LowPressure.IntCount), 4, LP.list.Num.ToString());//
                                obj.SetValue((IntRow + 1 + j + i * LowPressure.IntCount), 6, LP.list.L4);
                            }
                            break;
                        case "环网柜":
                            PF.CreateSheetView(obj, 1, 1, (IntRow + 1 + j + i * LowPressure.IntCount), 2, VolatageClass);//
                            PF.CreateSheetView(obj, 1, 1, (IntRow + 1 + j + i * LowPressure.IntCount), 3, pTT2.Num2.ToString());//
                            ReturnFabricationCostTitle("常规", VolatageClass, strTitle);
                            if (LP.list == null)
                            {
                                //MessageBox.Show(strTitle + "没有数据！！！");
                            }
                            else
                            {
                                PF.CreateSheetView(obj, 1, 1, (IntRow + 1 + j + i * LowPressure.IntCount), 4, LP.list.Num.ToString());//
                                obj.SetValue((IntRow + 1 + j + i * LowPressure.IntCount), 6, LP.list.L4);
                            }
                            break;
                        case "柱上变":
                            PF.CreateSheetView(obj, 1, 1, (IntRow + 1 + j + i * LowPressure.IntCount), 2, VolatageClass);//
                            PF.CreateSheetView(obj, 1, 1, (IntRow + 1 + j + i * LowPressure.IntCount), 3, pTT1.Num5.ToString());//
                            ReturnFabricationCostTitle("常规", VolatageClass, strTitle);
                            if (LP.list == null)
                            {
                                //MessageBox.Show(strTitle + "没有数据！！！");
                            }
                            else
                            {
                                PF.CreateSheetView(obj, 1, 1, (IntRow + 1 + j + i * LowPressure.IntCount), 4, LP.list.Num.ToString());//
                                obj.SetValue((IntRow + 1 + j + i * LowPressure.IntCount), 6, LP.list.L4);
                            }
                            break;
                        case "电缆分支箱":
                            PF.CreateSheetView(obj, 1, 1, (IntRow + 1 + j + i * LowPressure.IntCount), 2, VolatageClass);//
                            PF.CreateSheetView(obj, 1, 1, (IntRow + 1 + j + i * LowPressure.IntCount), 3, pTT2.Num4.ToString());//
                            ReturnFabricationCostTitle("常规", VolatageClass, strTitle);
                            if (LP.list == null)
                            {
                                //MessageBox.Show(strTitle + "没有数据！！！");
                            }
                            else
                            {
                                PF.CreateSheetView(obj, 1, 1, (IntRow + 1 + j + i * LowPressure.IntCount), 4, LP.list.Num.ToString());//
                                obj.SetValue((IntRow + 1 + j + i * LowPressure.IntCount), 6, LP.list.L4);
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// 返回单位工程综合造价
        /// </summary>
        /// <param name="pTT1"></param>
        /// <returns></returns>
        private double ReturnAmount(Ps_Table_TZMX pTT1, Ps_Table_TZGS pTT,string Typeqf)
        {
            string strTemp = null;
            int beginYear = 0;
            int EndYear = 0;
            int.TryParse(pTT1.BuildYear, out beginYear);
            int.TryParse(pTT1.BuildEnd, out EndYear);
            strTemp = null;
            for (int k = beginYear; k <= EndYear; ++k)
            {
                if (k == EndYear)
                    strTemp += "y" + k;
                else
                    strTemp += "y" + k + "+";
            }
            return  SelectAmount(strTemp, pTT.ID, "sub");
        }
        /// <summary>
        /// 返回要取得高压需要的行数
        /// </summary>
        /// <returns></returns>
        private int ReturnRows(Itop.Client.Base.FormBase FB)
        {
            int intTemp = 0;
            HP.INew = null;//每个项目有几个电压等级新建
            HP.IExtends = null; //扩建
            HP.IRebuild = null;//改造 
            HP.Volume=new int[3];//1扩建，2改造，0新建三个种类
            ////查询 新建，扩建，改造的电压等级
            HP.INew = SelectEvery(FB, "新建");
             HP.IExtends = SelectEvery(FB, "扩建");
            HP.IRebuild = SelectEvery(FB, "改造");
            
            ////每个电压等级有几个容量
            ReturnVoltage(HP.INew, "sub", "新建");
            if(HP.INewVolume[0].Count==0||HP.INewVolume==null)
            {
                intTemp = HP.INew.Count;
                HP.Volume[0] = HP.INew.Count;
            }
            else
            {
                intTemp = ReturnVoltageCount(HP.INew, HP.INewVolume);
                HP.Volume[0] = ReturnVoltageCount(HP.INew, HP.INewVolume);
            }
            ReturnVoltage(HP.IExtends, "sub", "扩建");
            if(HP.IExtendsVolume[0].Count==0||HP.IExtendsVolume==null)
            {
                intTemp += HP.IExtends.Count;
                HP.Volume[1] = HP.IExtends.Count;
            }
            else
            {
                intTemp += ReturnVoltageCount(HP.IExtends, HP.IExtendsVolume);
                HP.Volume[1] = ReturnVoltageCount(HP.IExtends, HP.IExtendsVolume);
            }
            ReturnVoltage(HP.IRebuild, "sub", "改造");
            if (HP.IRebuildVolume[0].Count==0||HP.IRebuildVolume==null )
            {
                intTemp += HP.IRebuild.Count;
                HP.Volume[2] = HP.IRebuild.Count;
            }
            else
            {
                intTemp += ReturnVoltageCount(HP.IRebuild, HP.IRebuildVolume);
                HP.Volume[2] = ReturnVoltageCount(HP.IRebuild, HP.IRebuildVolume);
            }
            //新建架空和新建电缆线路

            SelectLineAndJK();
            ReturnVol();//返回架空和电缆的长度在项目明细中暂时不用
            return intTemp + HP.ILineAndJK.Count*2;
        }
        /// <summary>
        /// 返回每个电压等级的容量的个数
        /// </summary>
        /// <returns></returns>
        /// <param name="list">电压等级数组</param>
        /// <param name="list1">电压等级容量数组</param>
        private int ReturnVoltageCount(IList list,IList[] list1)
        {
            int intTemp = 0;
            int listCount = 0;
            for(int i=0;i<list.Count;++i)
            {
                if(list1[i].Count==0)
                {
                    listCount = 1;
                }
                else
                {
                    listCount = list1[i].Count;
                }
                intTemp += listCount;
            }
            return intTemp;
        }
        /// <summary>
        /// 返回每个电压等级的容量
        /// </summary>
        /// <param name="list"></param>
        private void ReturnVoltage(IList list, string strType,string strTitle)
        {
            Ps_Table_TZGS pTT = new Ps_Table_TZGS();
            if (strTitle == "新建")
            {
                HP.INewVolume = new IList[list.Count];
                for (int i = 0; i < list.Count; ++i)
                {
                    pTT = (Ps_Table_TZGS)list[i];
                    SelectVoltageVolume(pTT.ID, strType, i,HP.INewVolume);
                }
                
            }
            if (strTitle == "扩建")
            {
                HP.IExtendsVolume = new IList[list.Count];
                for (int i = 0; i < list.Count; ++i)
                {
                    pTT = (Ps_Table_TZGS)list[i];
                    SelectVoltageVolume(pTT.ID, strType, i,HP.IExtendsVolume );
                }
            }
            if (strTitle == "改造")
            {
                HP.IRebuildVolume = new IList[list.Count];
                for (int i = 0; i < list.Count; ++i)
                {
                    pTT = (Ps_Table_TZGS)list[i];
                    SelectVoltageVolume(pTT.ID, strType, i,HP.IRebuildVolume);
                }
            }
        }
        /// <summary>
        /// 查询项目（高压）的电压等级数量
        /// </summary>
        /// <param name="strClass"></param>
        private IList SelectVoltageClass(Itop.Client.Base.FormBase FB, string strClass)
        {
            string con = null;
            IList IVoltageClass = null;
            con = "Select substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)  FROM  Ps_Table_TZGS a INNER JOIN "+
                         " Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN  "+
                          " Ps_Table_TZGS c ON a.ID = c.ParentID"+
                         " where b.Col4 = 'bian' and c.Col4='line'and  cast(substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1) as int)>= 35 "+
	                    "and (a.Col3= '"+strClass+"') and a.ProjectID='"+FB.ProjectUID+"' group by a.BianInfo";
            try
            {
                IVoltageClass = (IList)Services.BaseService.GetList("SelectTZGSEvery", con);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return IVoltageClass;
        }
        /// <summary>
        /// 每个电压等级有几个容量
        /// </summary>
        /// <param name="ParentID">父类id</param>
        /// <param name="strTypeqf">类型是线路(line)还是变电站(sub)</param>
        private void  SelectVoltageVolume(string ParentID, string strTypeqf,int index,IList list)
        {
            string con = "ProjectID='"+ParentID+"' and Typeqf='"+strTypeqf+"'";
            try
            {
                list[index] = (IList)Services.BaseService.GetList("SelectPs_Table_TZMXByValue", con);
                
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="strClass"></param>
        /// <returns></returns>
        private IList SelectEvery(Itop.Client.Base.FormBase FB, string strClass)
       {
           IList IVoltageClass = null;
           string con = "and  cast(substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1) as int)>= 35 "+
	                            "and (a.Col3= '"+strClass+"') and  a.ProjectID='"+FB.ProjectUID+"'";
           try
           {
               IVoltageClass = (IList )Services.BaseService.GetList("SelectPs_Table_TZGS_GCQD_lgm", con);
               
           }
           catch (System.Exception e)
           {
               //MessageBox.Show(e.Message);
           }
           return IVoltageClass;
       }
        /// <summary>
       /// 查询单位工程综合造价
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="Typeqf"></param>
        /// <returns></returns>
        private double SelectAmount(string year,string parentId,string Typeqf)
        {
            string con = "select SUM( "+year+") from ps_table_tzmx where Typeqf='"+Typeqf+"'and projectID='"+parentId+"'";
            double temp = 0;
            try
            {
                temp = (double)Services.BaseService.GetObject("SelectTZGSEvery", con);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return temp;
        }
        /// <summary>
        /// 查询架空和线路数据
        /// </summary>
        private void SelectLineAndJK()
        {
            string con="Select c.*  FROM  Ps_Table_TZGS a INNER JOIN "+
                             " Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN "+
                              " Ps_Table_TZGS c ON a.ID = c.ParentID "+
                              " where b.Col4 = 'bian' and c.Col4='line'and  cast(substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1) as int)>= 35"+ 
	                        " and (a.Col3= '新建') ";
            try
            {
                HP.ILineAndJK = (IList)Services.BaseService.GetList("SelectTZGSEveryField", con);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 查找线路和架空的项目明细表
        /// </summary>
        /// <param name="ParentID"></param>
        private void  SelectLineAndJKOfVal(string ParentID,int index)
        {
            string con = " Typeqf='line'and projectID='"+ParentID+"'";
            try
            {
                HP.Length[index] = (IList)Services.BaseService.GetList("SelectPs_Table_TZMXByValue", con);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }
        private void ReturnVol()
        {
            Ps_Table_TZGS pTT = null;
            HP.Length = new IList[HP.ILineAndJK.Count];
            for(int i=0;i<HP.ILineAndJK.Count;++i)
            {
                pTT = (Ps_Table_TZGS)HP.INew[i];
                SelectLineAndJKOfVal(pTT.ID,i);
            }
        }
        /// <summary>
        /// 返回低压数据
        /// </summary>
        private IList ReturnLowRows(string strValue)
        {
            string con = SQL_Con(strValue) + " and cast(substring(" + strValue + ".BianInfo,1,charindex('@'," + strValue + ".BianInfo,0)-1) as int)< 35 " +
                                " and (" + strValue + ".Col3= '新建') ";
            IList list = null;
            try
            {
                list = (IList)Services.BaseService.GetList("SelectTZGSEveryField", con);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return list;
        }
        /// <summary>
        /// 返回sql语句的前半部
        /// </summary>
        /// <param name="value"></param>
        private string SQL_Con(string  a)
        {
            string con = " SELECT  "+
              a+".ID, "+
              a+".AreaName,"+
              a+".Title,"+
              a+".Sort,"+
              a+".ProjectID,"+
              a+".BuildYear,"+
              a+".BuildEd,"+
              a+".Length,"+
              a+".Volumn,"+
              a+".ParentID,"+
              a+".FromID,"+
              a+".AllVolumn,"+
              a+".BefVolumn,"+
              a+".AftVolumn,"+
              a+".BianInfo,"+
              a+".LineInfo,"+
              a+".y1990,"+
              a+".y1991,"+
              a+".y1992,"+
              a+".y1993,"+
              a+".y1994,"+
              a+".y1995,"+
              a+".y1996,"+
               a+".y1997,"+
               a+".y1998,"+
               a+".y1999,"+
               a+".y2000,"+
               a+".y2001,"+
              a+".y2002,"+
              a+".y2003,"+
              a+".y2004,"+
              a+".y2005,"+
              a+".y2006,"+
              a+".y2007,"+
              a+".y2008,"+
              a+".y2009,"+
              a+".y2010,"+
              a+".y2011,"+
              a+".y2012,"+
              a+".y2013,"+
              a+".y2014,"+
              a+".y2015,"+
              a+".y2016,"+
              a+".y2017,"+
              a+".y2018,"+
              a+".y2019,"+
              a+".y2020,"+
              a+".y2021,"+
              a+".y2022,"+
              a+".y2023,"+
              a+".y2024,"+
              a+".y2025,"+
              a+".y2026,"+
              a+".y2027,"+
              a+".y2028,"+
              a+".y2029,"+
              a+".y2030,"+
              a+".y2031,"+
              a+".y2032,"+
              a+".y2033,"+
              a+".y2034,"+
              a+".y2035,"+
              a+".y2036,"+
              a+".y2037,"+
              a+".y2038,"+
              a+".y2039,"+
              a+".y2040,"+
              a+".y2041,"+
              a+".y2042,"+
              a+".y2043,"+
              a+".y2044,"+
              a+".y2045,"+
              a+".y2046,"+
              a+".y2047,"+
              a+".y2048,"+
              a+".y2049,"+
              a+".y2050,"+
              a+".y2051,"+
              a+".y2052,"+
              a+".y2053,"+
              a+".y2054,"+
              a+".y2055,"+
              a+".y2056,"+
              a+".y2057,"+
              a+".y2058,"+
              a+".y2059,"+
              a+".y2060,"+
              a+".Col1,"+
              a+".Col2,"+
              a+".Col3,"+
              a+".Col4,"+
              a+".DQ,"+
              a+".Num1,"+
              a+".Num2,"+
              a+".Num3,"+
              a+".Num4,"+
              a+".Num5,"+
              a+".WGNum,"+
              a+".Amount,"+
              a+".JGNum,"+
              a+".ProgType,"+
              a+".Length2,"+
              a+".Num6"+
              "  FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d"+
              " where  a.ID = b.ParentID AND b.Col4 = 'pw-line'"+
              " AND a.ID = c.ParentID AND c.Col4 = 'pw-pb'"+
              " and a.ID = d.ParentID AND d.Col4 = 'pw-kg'";
            return con;
        }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 返回电压等级（低压）
        /// </summary>
        /// <param name="a">低压有四条数据原始数据-a，线路数据-b，开关数据-c，配变数据-d.字母（a，b，c，d）代表这四条数据</param>
        private string ReturnLowVoltageClass(string a,string strTitle)
        {
            string con = "select substring("+a+".BianInfo,1,charindex('@',"+a+".BianInfo,0)-1) FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                                "  where  a.ID = b.ParentID AND b.Col4 = 'pw-line'"+
                               "   AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' "+
                                "  and a.ID = d.ParentID AND d.Col4 = 'pw-kg' and  "+
                                " cast(substring("+a+".BianInfo,1,charindex('@',"+a+".BianInfo,0)-1) as int)< 35"+ 
	                            "and ("+a+".Col3= '新建') and "+a+".Title='"+strTitle+"'";
            string temp = "";
            try
            {
                temp = (string)Services.BaseService.GetObject("SelectTZGSEvery", con);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return temp;
        }
        /// <summary>
        /// 低压返回造价数据
        /// </summary>
        /// <param name="strType">规格</param>
        /// <param name="strClass">电压等级</param>
        /// <returns></returns>
        private void ReturnFabricationCost(string strType,string strClass)
        {
            string con = " Type = '" + strType + "'and s1='" + strClass + "'";
            try
            {
                LP.list = (Project_Sum)Services.BaseService.GetObject("SelectProject_SumByAllVol", con);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 低压返回造价数据
        /// </summary>
        /// <param name="strType">规格</param>
        /// <param name="strClass">电压等级</param>
        /// <param name="strTitle">项目名称</param>
        /// <returns></returns>
        private void ReturnFabricationCostTitle(string strType, string strClass,string strTitle)
        {
            string con = " Type = '" + strType + "'and s1='" + strClass + "' and Name='" + strTitle + "'";
            try
            {
                LP.list = (Project_Sum)Services.BaseService.GetObject("SelectProject_SumByAllVol", con);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 返回高压变电站造价的一条记录,这个函数没用上
        /// </summary>
        private void ReturnHeightFabricationCostOfBDZ(string strTitle,string strClass)
        {
            string con = " s1='" + strClass + "' and Name like '" + strTitle + "'";
            try
            {
                HP.list = (Project_Sum)Services.BaseService.GetObject("SelectProject_SumByAllVol", con);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 返回高压架空和线路一条记录,这个函数没用上
        /// </summary>
        private void ReturnHeightFabricationCostOfJKAndLine(string strType,string strClass)
        {
            string con = " Type = '" + strType + "'and s1='" + strClass + "'";
            try
            {
                HP.list = (Project_Sum)Services.BaseService.GetObject("SelectProject_SumByAllVol", con);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }
    }
}
