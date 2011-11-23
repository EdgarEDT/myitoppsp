using System;
using System.Collections.Generic;
using System.Text;
/******************************************************************************************************
 *  ClassName：Sheet10_10
 *  Action：用来生成Sheet10_10表的调用方法
 * Author：吕静涛
 * Mender  ：吕静涛
 *  * 概述：手写表
 *  时间：2010-10-11

 *********************************************************************************************************/
namespace Itop.JournalSheet.Function10
{
    class Sheet10_10
    {
        private const int RowCount = 21;
        private string[,] LeftTitle = new string[2, RowCount];//左侧标题。
       
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();
        private Function10.Sheet10_1_1 S10_1_1 = new Function10.Sheet10_1_1();

        private struct SSheet_1
        {
            public string strID;//序号
            public string strProject;//项目
            public object[,] data;//数据

        }
        List<SSheet_1> SaveList = new List<SSheet_1>();
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet10_10Title(FarPoint.Win.Spread.SheetView obj, string Title,bool IsTrue)
        {

            int IntColCount = 8;
            int IntRowCount = RowCount + 2 + 3;//标题占3行，分区类型占2行，
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

            strTitle = " 序号 ";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge , IntRow, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            
            strTitle = " 项目 ";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge , IntRow, IntCol += 1, strTitle);
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


            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;
            SetLeftTitle(obj, IntRow, IntCol);//左侧标题
            if(!IsTrue)
            {
                WriteData(obj, IntRow, IntCol);//数据
            }else
            {
                ComparisonData(obj, 5, 2);
                WriteData(obj, IntRow, IntCol);//数据
            }
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
            //PF.SetWholeColumnsWidth(obj);//列宽
        }
        /// <summary>
        /// 写左侧标题
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        private void SetLeftTitle(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            int index = 0;
            InitLeftTitle();
            for (int i = 0; i < (RowCount); ++i)
            {
                if(i==1||i==3)
                {
                    PF.CreateSheetView(obj, 2, 1, (i+IntRow), 0, LeftTitle[0, i]);
                }
                else
                {
                    PF.CreateSheetView(obj, 1, 1, (i+IntRow), 0, LeftTitle[0, i]);
                }
                PF.CreateSheetView(obj, 1, 1, (i + IntRow), 1, LeftTitle[1, i]);
            }
        }
        /// <summary>
        /// 初始化左侧标题
        /// </summary>
        private void InitLeftTitle()
        {
            for(int i=0;i<RowCount;++i)
            {
                switch(i)
                {
                    case 0:
                        LeftTitle[0, i] = "1";
                        LeftTitle[1, i] = "年末固定资产净值（万元）";
                        break;
                    case 1:
                        LeftTitle[0, i] = "1.1";
                        LeftTitle[1, i] = "其中：220kV及以上";
                        break;
                    case 2:
                        //LeftTitle[0, i] = "1";
                        LeftTitle[1, i] = "输电网(万元)";
                        break;
                    case 3:
                        LeftTitle[0, i] = "1.2";
                        LeftTitle[1, i] = "110kV及以下";
                        break;
                    case 4:
                        //LeftTitle[0, i] = "1";
                        LeftTitle[1, i] = "配电网(万元)";
                        break;
                    case 5:
                        LeftTitle[0, i] = "2";
                        LeftTitle[1, i] = "电价";
                        break;
                    case 6:
                        LeftTitle[0, i] = "2.1";
                        LeftTitle[1, i] = "平均购电价（元/千kWh）";
                        break;
                    case 7:
                        LeftTitle[0, i] = "2.2";
                        LeftTitle[1, i] = "平均购电价（元/千kWh）";
                        break;
                    case 8:
                        LeftTitle[0, i] = "3";
                        LeftTitle[1, i] = "售电量（亿kWh）";
                        break;
                    case 9:
                        LeftTitle[0, i] = "4";
                        LeftTitle[1, i] = "资金投入（万元）";
                        break;
                    case 10:
                        LeftTitle[0, i] = "5";
                        LeftTitle[1, i] = "销售电量收入（万元）";
                        break;
                    case 11:
                        LeftTitle[0, i] = "6";
                        LeftTitle[1, i] = "销售成本（万元）";
                        break;
                    case 12:
                        LeftTitle[0, i] = "6.1";
                        LeftTitle[1, i] = "购电费（万元）";
                        break;
                    case 13:
                        LeftTitle[0, i] = "6.2";
                        LeftTitle[1, i] = "折旧（万元）";
                        break;
                    case 14:
                        LeftTitle[0, i] = "6.3";
                        LeftTitle[1, i] = "管理费（万元）";
                        break;
                    case 15:
                        LeftTitle[0, i] = "6.4";
                        LeftTitle[1, i] = "销售营业税（万元）";
                        break;
                    case 16:
                        LeftTitle[0, i] = "6.5";
                        LeftTitle[1, i] = "销售附加支出（万元）";
                        break;
                    case 17:
                        LeftTitle[0, i] = "7";
                        LeftTitle[1, i] = "销售利润（万元）";
                        break;
                    case 18:
                        LeftTitle[0, i] = "8";
                        LeftTitle[1, i] = "所得税（万元）";
                        break;
                    case 19:
                        LeftTitle[0, i] = "9";
                        LeftTitle[1, i] = "三金（万元）";
                        break;
                    case 20:
                        LeftTitle[0, i] = "10";
                        LeftTitle[1, i] = "可分配利润（万元）";
                        break;

                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 写数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        private void WriteData(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            for (int i = IntRow; i < obj.RowCount; ++i)
            {
                for (int j = 2; j < obj.ColumnCount; ++j)
                {
                    obj.Cells[i, j].Locked = false;//手写
                }
            }
        }
        private void ComparisonData(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            for (int i = IntRow; i < obj.RowCount; i += 1)//行从第IntRow行开始载入
            {
                for (int l = 0; l < SaveList.Count; ++l)
                {
                    if ((SaveList[l].strID == PF.ReturnStr(obj, i, 0).ToString()) && (SaveList[l].strProject == PF.ReturnStr(obj, i, 1).ToString()))
                    {
                        for (int j = IntCol; j < obj.ColumnCount; ++j)//
                        {
                            obj.SetValue(i, j, SaveList[l].data[0, j - IntCol]);

                        }
                        SaveList.Remove(SaveList[l]);
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 保存手写的数据，为了和新更新的数据比较
        /// 点击更新时把旧表保存在IList中
        /// </summary>
        /// <param name="BeginCol">要保存的起始行</param>
        /// <param name="BeginRow">要保存的起始列</param>
        /// <param name="indexX">二维数组的一维个数</param>
        /// <param name="indexY">二维数组的二维个数</param>
        public void SaveData(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol, int indexX, int indexY)
        {
            SaveList.Clear();
            for (int i = IntRow; i < (obj.RowCount); i += 1)//行从第IntRow行开始保存
            {
                SSheet_1 SS_1 = new SSheet_1();
                SS_1.strID = null;
                SS_1.strProject = null;
                SS_1.data = new object[indexX, indexY];
                for (int j = IntCol; j < obj.ColumnCount; ++j)//列从第IntCol列开始保存
                {
                    SS_1.strID = PF.ReturnStr(obj, i, 0).ToString();
                    SS_1.strProject = PF.ReturnStr(obj, i, 1).ToString();
                    SS_1.data[0, j - IntCol] = obj.Cells[i, j].Value;
                }
                SaveList.Add(SS_1);
            }

        }
    }
}
