using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using Itop.Client.Common;
/******************************************************************************************************
 *  ClassName：Sheet11_2
 *  Action：用来生成Sheet11_2表的调用方法
 * Author：吕静涛
 * Mender  ：吕静涛
 *概述：这个表是个手写表，用数据库表ps_Table_TZGS
 * 时间：2010-10-11。
 *********************************************************************************************************/
namespace Itop.JournalSheet.Function11
{
    class Sheet11_2
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private IList City = null;//市辖供电区
        private IList County = null;//县级供电区
        private string[] AreaType = new string[2];//市辖，县级
        private const int VCCount = 2;//年份
        private string[] VoltageClass = new string[VCCount];

        //Function10.Sheet10_1_1 S10_1_1 = new Function10.Sheet10_1_1();
        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();
        private Function10.Sheet10_3 S10_3 = new Function10.Sheet10_3();
        private Function.Sheet2_N S2_N = new Function.Sheet2_N();
        private FarPoint.Win.Spread.CellType.PercentCellType PC = new FarPoint.Win.Spread.CellType.PercentCellType();

        private  struct SSheet11_2
        {
            public string strType;//类型
            public string strDQ;//地区
            public object[,] data;//数据

        }
        List<SSheet11_2> list = new List<SSheet11_2>();

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet11_2Title(FarPoint.Win.Spread.FpSpread FpObj,FarPoint.Win.Spread.SheetView obj, string Title, bool IsTrue)
        {
            int IntColCount = 16;
            AreaType[0] = "市辖供电区";
            AreaType[1] = "县级供电区";
            SelectDQ();
            int IntRowCount = (City.Count+County.Count+2) * VCCount + 2 + 3;//标题占3行，分区类型占2行，
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

            strTitle = " 编号 ";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 类型 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 分区名称 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 年份 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 容载比 ";
            PF.CreateSheetView(obj, NextRowMerge -= 1, NextColMerge += 1, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 110（66）kV ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge -= 1, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 35kV ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //====================================================================================================
            strTitle = " 供电可靠率（RS-3）（%） ";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow -= 1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 110kV及以下综合线损率（%） ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 10kV及以下综合线损率（%） ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 低压线损率（%） ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 综合电压合格率（%） ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 一户一表率（%） ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 农村居民户通电率（%） ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 电气化县比例（%） ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 电气化乡（镇）比例（%） ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 电气化村比例（%） ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);



            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;
            SetLeftTitle(obj, IntRow, IntCol);//左侧标题
            WriteData(FpObj, obj, IntRow, IntCol, IsTrue);//数据
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 填写左侧表行头
        /// </summary>
        private void SetLeftTitle(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            int index = 0;
            for (int c = 0; c < VCCount; ++c)//初始化电压等级
            {
                switch (c)
                {
                    case 0:
                        VoltageClass[c] = "2009";
                        break;
                    case 1:
                        VoltageClass[c] = "2015";
                        break;

                    default:
                        break;
                }
            }
            for (int i = 0; i < 2; ++i)
            {
                if (i == 0)
                {
                    PF.CreateSheetView(obj, ((City.Count + 1) * VCCount), NextColMerge, IntRow, IntCol, "1");
                    PF.CreateSheetView(obj, ((City.Count + 1) * VCCount), NextColMerge, IntRow, IntCol+1, "市辖供电区");
                    for (int j = 0; j < (City.Count + 1); ++j)//市辖供电区
                    {
                        if (j == 0)
                        {
                            PF.CreateSheetView(obj, VCCount, NextColMerge, IntRow + j * VCCount, IntCol + 2, "合计");
                        }
                        else
                        {
                            PF.CreateSheetView(obj, VCCount, NextColMerge, IntRow + j * VCCount, IntCol + 2, City[index].ToString());
                            index++;
                        }
                        for (int c = 0; c < VCCount; ++c)
                        {
                            PF.CreateSheetView(obj, 1, NextColMerge, IntRow + j * VCCount + c, IntCol + 3, VoltageClass[c]);
                        }
                    }
                }
                else
                {
                    PF.CreateSheetView(obj, ((County.Count + 1) * VCCount), NextColMerge, IntRow + ((City.Count + 1) * VCCount), IntCol, "2");
                    PF.CreateSheetView(obj, ((County.Count + 1) * VCCount), NextColMerge, IntRow + ((City.Count + 1) * VCCount), IntCol+1, "县级供电区");
                    index = 0;
                    for (int j = (City.Count + 1); j < ((County.Count + 1) + (City.Count + 1)); ++j)//县级供电区
                    {
                        if (j == (City.Count + 1))
                        {
                            PF.CreateSheetView(obj, VCCount, NextColMerge, IntRow + j * VCCount, IntCol + 2, "合计");
                        }
                        else
                        {
                            PF.CreateSheetView(obj, VCCount, NextColMerge, IntRow + j * VCCount, IntCol + 2, County[index].ToString());
                            index++;
                        }
                        for (int c = 0; c < VCCount; ++c)
                        {
                            PF.CreateSheetView(obj, 1, NextColMerge, IntRow + j * VCCount + c, IntCol + 3, VoltageClass[c]);
                        }
                    }
                }

            }
        }
        /// <summary>
        /// 写数据，手写
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        private void WriteData(FarPoint.Win.Spread.FpSpread FpObj, FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol, bool IsTrue)
        {
            if (!IsTrue)//不是更新
            {
                for (int i = 5; i < obj.RowCount; ++i)
                {
                    for (int j = 3; j < obj.ColumnCount; ++j)
                    {
                        obj.Cells[i, j].Locked = false;//手写
                        if (j > 5)
                        {
                            obj.Cells[i, j].CellType = PC;//%
                        }
                    }
                }
            }
            else//需要更新数据了
            {
                
                ComparisonData(obj, 5, 4);
            }
        }
        /// <summary>
        /// 返回地区名
        /// </summary>
        private void SelectDQ()
        {
            //AreaNameType = Services.BaseService.GetList("SelectTZGSOnAreaName", "");
            string con = null;
            con = "DQ='市辖供电区' and (BuildYear='2009' or BuildYear='2015' ) group by AreaName";
            City = Services.BaseService.GetList("SelectTZGSAreaName", con);
            con = "DQ!='市辖供电区' and (BuildYear='2009' or BuildYear='2015' ) group by AreaName";
            County = Services.BaseService.GetList("SelectTZGSAreaName", con);
        }
        /// <summary>
        /// 在左侧标题写入的情况下比较数据,然后写入数据
        /// </summary>
        /// <param name="obj"></param>
        private  void ComparisonData(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            for (int i = 5; i < obj.RowCount; ++i)
            {
                for (int j = 3; j < obj.ColumnCount; ++j)
                {
                    obj.Cells[i, j].Locked = false;//手写
                    if (j > 5)
                    {
                        obj.Cells[i, j].CellType = PC;//%
                    }
                }
            }
            for (int i = IntRow; i < obj.RowCount; i += 2)//行从第IntRow行开始载入
            {
                for (int l = 0; l < list.Count; ++l)
                {
                    if ((list[l].strType == PF.ReturnStr(obj, i, 1).ToString()) && (list[l].strDQ == PF.ReturnStr(obj, i, 2).ToString()))
                    {
                        for (int j = IntCol; j < obj.ColumnCount; ++j)//
                        {
                                obj.SetValue(i, j, list[l].data[0, j - IntCol]);
                                obj.SetValue(i + 1, j, list[l].data[1, j - IntCol]);
                        }
                        list.Remove(list[l]);
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
        public  void SaveData(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol,int indexX,int indexY)
        {
            list.Clear();
            for (int i = IntRow; i < (obj.RowCount); i += 2)//行从第IntRow行开始保存
            {
                SSheet11_2 SS11_2 = new SSheet11_2();
                SS11_2.strDQ = null;
                SS11_2.strType = null;
                SS11_2.data = new object[indexX, indexY];
                for (int j = IntCol; j < obj.ColumnCount; ++j)//列从第IntCol列开始保存
                {
                    SS11_2.strType =  PF.ReturnStr(obj, i,1).ToString();
                    SS11_2.strDQ = PF.ReturnStr(obj, i, 2).ToString();
                    SS11_2.data[0, j - IntCol] = obj.Cells[i, j].Value;
                    SS11_2.data[1, j - IntCol] = obj.Cells[i + 1, j].Value;
                }
                list.Add(SS11_2);
            }

        }
    }
}
