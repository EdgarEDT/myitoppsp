using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using Itop.Client.Common;
/******************************************************************************************************
 *  ClassName：Sheet11_1
 *  Action：用来生成Sheet11_1表的调用方法
 * Author：吕静涛
 * Mender  ：吕静涛
 *概述：这个表用数据库表ps_Table_TZGS
 * 时间：2010-10-11。
 *********************************************************************************************************/
namespace Itop.JournalSheet.Function11
{
    class Sheet11_1
    {
        private const int RowCount = 2;//县级合计，地区合计
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private const int IntDQ = 5;//地市的个数
        private string[] ListDQ = new string[IntDQ];

        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();
        private Function10.Sheet10_3 S10_3 = new Function10.Sheet10_3();
        private Function.Sheet2_N S2_N = new Function.Sheet2_N();
        private FarPoint.Win.Spread.CellType.PercentCellType PC = new FarPoint.Win.Spread.CellType.PercentCellType();
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet11_1Title(FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int IntColCount = 15;
            //ListDQ=SelectRows();
            DQTaxis();
            int IntRowCount = (IntDQ + RowCount) * 2 + 2 + 3;//标题占3行，分区类型占2行，
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

            strTitle = " 年份 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 容载比 ";
            PF.CreateSheetView(obj, NextRowMerge-=1, NextColMerge+=1, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 110（66）kV ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge-=1, IntRow+=1, IntCol , strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 35kV ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
//====================================================================================================
            strTitle = " 供电可靠率（RS-3）（%） ";
            PF.CreateSheetView(obj, NextRowMerge+=1, NextColMerge, IntRow-=1, IntCol += 1, strTitle);
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
            WriteData(obj, IntRow, IntCol);//数据
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 写左侧标题
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        private void SetLeftTitle(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            int count=1;
            int index = 0;
            for (int i = 0; i < (IntDQ + 2); ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if(j==0)
                    {
                        if (i >= 2 && i < obj.RowCount - 1)
                        {
                            PF.CreateSheetView(obj, 2, 1, i*2 + IntRow, j, "2." + count);
                            count++;
                        }
                        if (i == IntDQ + 2 - 1)
                        {
                            PF.CreateSheetView(obj, 2, 1, i *2+ IntRow, j, "3");
                        }
                        if(i>=0&&i<2)
                        {
                            PF.CreateSheetView(obj, 2, 1, i*2 + IntRow, j, (i + 1).ToString());
                        }
                    }
                    if(j==1)
                    {
                        if (i != 1 && i != (IntDQ + 2) - 1)
                        {
                            PF.CreateSheetView(obj, 2, 1, i*2 + IntRow, j, S10_3.DataChangeType(ListDQ[index].ToString()));
                            index++;
                        }
                         if (i == 1)
                        {
                            PF.CreateSheetView(obj, 2, 1, i *2+ IntRow, j, "县级供电区");
                        }
                        if (i == (IntDQ + 2) - 1)
                        {
                            PF.CreateSheetView(obj, 2, 1, i*2 + IntRow, j, "全地区");
                        }
                    }
                    if(j==2)
                    {
                        for(int c=0;c<2;++c)
                        {
                            if(c==0)
                                PF.CreateSheetView(obj, 1, 1, i*2 + IntRow, j, "2009");
                            else
                                PF.CreateSheetView(obj, 1, 1, i*2 + IntRow+1, j, "2015");
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
        private void WriteData(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            for(int i=5;i<obj.RowCount;++i)
            {
                for(int j=3;j<obj.ColumnCount;++j)
                {
                    obj.Cells[i, j].Locked = false;//手写
                    if(j>4)
                    {
                        obj.Cells[i, j].CellType = PC;//%
                    }
                }
            }
        }
        /// <summary>
        /// 查询地区类型的个数
        /// </summary>
        /// <returns></returns>
        private IList SelectRows()
        {
            IList list =null;
            string con = null;
            con = "";
            try
            {
                list = Services.BaseService.GetList("SelectTZGSDQ", con);
            }
            catch(System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return list;
        }
        /// <summary>
        /// 按照报表的格式重新排序地区,
        /// </summary>
        /// <param name="ILDQType"></param>
        private void DQTaxis()
        {
            for (int i = 0; i < IntDQ; ++i)
            {
                switch (i)
                {
                    case 0:
                        ListDQ[i] = "市辖供电区";
                        break;
                    case 1:
                        ListDQ[i] = "县级直供直管";
                        break;
                    case 2:
                        ListDQ[i] = "县级控股";
                        break;
                    case 3:
                        ListDQ[i] = "县级参股";
                        break;
                    case 4:
                        ListDQ[i] = "县级代管";
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
