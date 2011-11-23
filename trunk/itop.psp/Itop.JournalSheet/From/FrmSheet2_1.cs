using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

using Itop.Client.Common;
using Itop.Common;
using Itop.Client.Base;

using DevExpress.Utils;

namespace Itop.JournalSheet.From
{
    public partial class FrmSheet2_1 : FormBase
    {
        //private string type1 = "1";
        private int SheetIndex = 0;//记住当前工作簿的索引。
        private int ColCount = 0;//列数
        private int RowCount = 0;//行数
        private int MergeLength = 0;//合并单元格纵向初始值
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private bool IsCreateSheet = true;//是否创建sheetView

        private Itop.JournalSheet.Function.PublicFunction PF = new Itop.JournalSheet.Function.PublicFunction();
        private Function.Sheet2_2 S2_2 = new Function.Sheet2_2();
        private Function.Sheet2_3 S2_3 = new Function.Sheet2_3();
        private Function.Sheet2_4 S2_4 = new Function.Sheet2_4();
        private Itop.JournalSheet.Function.Sheet2_N S2_N = new Itop.JournalSheet.Function.Sheet2_N();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet2 = new  FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet3 =new  FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet4 =new  FarPoint.Win.Spread.SheetView();
        private Title T = new Title();

        private struct   Title
        {
            public string T1;
            public string T2;
            public string T3;
            public string T4;
        }
        public FrmSheet2_1()
        {
            InitializeComponent();
        }

        private void FrmSheet2_1_Load(object sender, EventArgs e)
        {
            AddData(this.fpSpread1);
        }
        private void AddSheet()
        {
            fpSpread1.Sheets.Add(fpSpread1_Sheet1);
            fpSpread1.Sheets.Add(fpSpread1_Sheet2);
            fpSpread1.Sheets.Add(fpSpread1_Sheet3);
            fpSpread1.Sheets.Add(fpSpread1_Sheet4);

        }
        private void LoadData()
        {
            #region 初始化标题
            T.T1 = "表2‑1  铜陵市经济社会历史发展情况";
            T.T2 = "表2‑2  2009年铜陵市经济社会发展情况";
            T.T3 = "表2‑3 铜陵市各地级市国民经济与社会发展目标";
            T.T4 = "表2‑4 铜陵市各地级市国民经济产业结构发展趋势";
	        #endregion

            this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            //if (IsCreateSheet)
            //{
                //fpSpread1_Sheet1 = PF.CreateSheet(this, fpSpread1, T.T1);
                //fpSpread1_Sheet2 = PF.CreateSheet(this, fpSpread1, T.T2);
                //fpSpread1_Sheet3 = PF.CreateSheet(this, fpSpread1, T.T3);
                //fpSpread1_Sheet4 = PF.CreateSheet(this, fpSpread1, T.T4);
                AddSheet();
            //}

            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            if(fpSpread1.ActiveSheetIndex==1)
            {
                this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            this.barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

        try
        {
            #region SetYears
            IntCol = 1;
            //if (!IsCreateSheet)
            //{
            //    fpSpread1.Sheets[0].RowCount = 0;
            //    fpSpread1.Sheets[0].ColumnCount = 0;
            //}

            S2_N.SetYears("全地区GDP（亿元）", this,fpSpread1.Sheets[0] , 5, IntCol-1, 1, 1);
            MergeLength = S2_N.GolobalYearCount - (S2_N.GolobalYears);//GolobalYears年份没有数据的不显示出来除3是多循环2个一级目录(市辖供电区,县级供电区)
            #endregion 
            SheetN2_1(this.fpSpread1.Sheets[0], T.T1);
            fpSpread1.Sheets[0].CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(FrmSheet2_1_CellChanged);
            //===========================
            S2_2.InitTitle();
            //if (!IsCreateSheet)
            //{
            //    fpSpread1.Sheets[1].RowCount = 0;
            //    fpSpread1.Sheets[1].ColumnCount = 0;
            //}
            SheetN2_2(fpSpread1.Sheets[1], T.T2);
            ////this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            if (SheetIndex == 1)
            {
                S2_2.ConnectionDate(fpSpread1.Sheets[1], this, this.barEditItem1);
            }
            //if (!IsCreateSheet)
            //{
            //    fpSpread1.Sheets[2].RowCount = 0;
            //    fpSpread1.Sheets[2].ColumnCount = 0;
            //}
            S2_2.AddBarEditYears(this.barEditItem1);
            fpSpread1.Sheets[1].CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(FrmSheet2_1_CellChanged);
            //=============================
            //this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            S2_3.InitTitle();
            SheetN2_3(fpSpread1.Sheets[2], T.T3);
            S2_3.ConnectionDate(fpSpread1.Sheets[2], this);
            fpSpread1.Sheets[2].CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(FrmSheet2_1_CellChanged);
            //========================================
            S2_4.InitTitle();
            //if (!IsCreateSheet)
            //{
            //    fpSpread1.Sheets[3].RowCount = 0;
            //    fpSpread1.Sheets[3].ColumnCount = 0;
            //}
            SheetN2_4(fpSpread1.Sheets[3], T.T4);
            S2_4.ConnectionDate(fpSpread1.Sheets[3], this);
            fpSpread1.Sheets[3].CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(FrmSheet2_1_CellChanged);

            PF.Sheet_AutoLineFeed(fpSpread1);//设置整个工作簿换行
        }
        catch (System.Exception e)
        {

        }
    }
        /// <summary>
        /// 自动改变行高，与更新数据冲突所以现在不用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FrmSheet2_1_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
        {
            int flag = 0;
            int ActiveSheet = 0;
            if (flag == 1)
            {
                flag = 0;
                return;
            }
            else
            {
                flag = 1;
                ActiveSheet = fpSpread1.ActiveSheetIndex;
                //PF.SetRowHeight(fpSpread1.Sheets[ActiveSheet], e.Row, e.Column, fpSpread1.Sheets[ActiveSheet].GetValue(e.Row, e.Column));
            }
        }
        /// <summary>
        /// 写2——1标题
        /// </summary>
        private void SheetN2_1(FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int IntColCount = 9;
            int IntRowCount =MergeLength + 2 + 3;//标题占3行，类型占2行，

            obj.SheetName = Title;
            obj.Columns.Count = IntColCount;
            obj.Rows.Count = IntRowCount;
            IntCol = obj.Columns.Count;

            PF.ColReadOnly(obj, IntColCount);
            PF.Sheet_GridandCenter(obj);

            #region 合并单元格
            string strTitle = "";
            IntRow = 3;
            strTitle = Title;
            PF.CreateSheetView(obj, IntRow, IntCol, 0, 0, Title);
            PF.SetSheetViewColumnsWidth(obj, 0, Title);
            IntCol = 1;
            strTitle = " 年     份 ";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "产业结构比例（%） ";
            PF.CreateSheetView(obj, NextRowMerge -=1, NextColMerge+=2, IntRow, IntCol +=1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //PF.SetRowHight(obj, (IntRow), IntCol, strTitle);

            strTitle = "一          产 ";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge -= 3, IntRow+=1, IntCol , strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "二          产 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge , IntRow, IntCol+=1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "三          产 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "年末总人口（万人）";
            PF.CreateSheetView(obj, NextRowMerge+=1, NextColMerge, IntRow-=1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //PF.SetRowHight(obj, (IntRow), IntCol, strTitle);

            strTitle = "人均GDP（万元/人）";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //PF.SetRowHight(obj, (IntRow), IntCol, strTitle);

            strTitle = "城镇人口（万人）";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //PF.SetRowHight(obj, (IntRow), IntCol, strTitle);

            strTitle = "乡村人口（万人）";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //PF.SetRowHight(obj, (IntRow), IntCol, strTitle);

            strTitle = "城镇化率（%）";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //PF.SetRowHight(obj, (IntRow), IntCol, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;
            //设置行高
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);
            #endregion
        }
        private void SheetN2_2(FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int IntColCount = 10;
            int IntRowCount = 1 + 2 + 3;//标题占3行，类型占2行，

            obj.SheetName = Title;
            obj.Columns.Count = IntColCount;
            obj.Rows.Count = IntRowCount;
            IntCol = obj.Columns.Count;

            PF.ColReadOnly(obj, IntColCount);
            PF.Sheet_GridandCenter(obj);
            #region 合并单元格
            string strTitle = "";
            IntRow = 3;
            strTitle = Title;
            PF.CreateSheetView(obj, IntRow, IntCol, 0, 0, Title);
            PF.SetSheetViewColumnsWidth(obj, 0, Title);
            IntCol = 1;
            strTitle = " 地市名称 ";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "土地面积（km2） ";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge , IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "建成区面积（km2） ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "GDP（亿元） ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "产业结构比例（%） ";
            PF.CreateSheetView(obj, NextRowMerge-=1, NextColMerge+=2, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "一          产 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge -= 2, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "二          产 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "三          产 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "年末总人口（万人）";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow-=1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "人均GDP（万元/人）";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);


            strTitle = "城镇化率（%）";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;

            //设置行高
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);
            #endregion
        }
        private void SheetN2_3(FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int IntColCount = 10;
            int IntRowCount = 1 +3 + 3;//标题占3行，类型占3行，

            obj.SheetName = Title;
            obj.Columns.Count = IntColCount;
            obj.Rows.Count = IntRowCount;
            IntCol = obj.Columns.Count;

            S2_3.ColReadOnly(obj, IntColCount);
            PF.Sheet_GridandCenter(obj);

            #region 合并单元格
            string strTitle = "";
            IntRow = 3;
            strTitle = Title;
            PF.CreateSheetView(obj, IntRow, IntCol, 0, 0, Title);
            PF.SetSheetViewColumnsWidth(obj, 0, Title);
            IntCol = 1;

            strTitle = "地市名称 ";
            PF.CreateSheetView(obj, NextRowMerge+=2, NextColMerge, IntRow, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "人          口 ";
            PF.CreateSheetView(obj, NextRowMerge -= 2, NextColMerge += 2, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "2010年（万人） ";
            PF.CreateSheetView(obj, NextRowMerge+=1, NextColMerge -= 2, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "2015年（万人） ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "年均增长率（%） ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "G          D          P ";
            PF.CreateSheetView(obj, NextRowMerge -= 1, NextColMerge += 2, IntRow-=1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "2010年（万人） ";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge -= 2, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "2015年（万人） ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "年均增长率（%） ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "人均G   D   P ";
            PF.CreateSheetView(obj, NextRowMerge -= 1, NextColMerge += 2, IntRow-=1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "2010年（万人） ";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge -= 2, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "2015年（万人） ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "年均增长率（%） ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;

            //设置行高
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);
            #endregion
        }
        private void SheetN2_4(FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int IntColCount = 15;
            int IntRowCount = 1 + 3 + 3;//标题占3行，类型占3行，

            obj.SheetName = Title;
            obj.Columns.Count = IntColCount;
            obj.Rows.Count = IntRowCount;
            IntCol = obj.Columns.Count;

            S2_4.ColReadOnly(obj, IntColCount);
            PF.Sheet_GridandCenter(obj);
            #region 合并单元格
            string strTitle = "";
            IntRow = 3;
            strTitle = Title;
            PF.CreateSheetView(obj, IntRow, IntCol, 0, 0, Title);
            PF.SetSheetViewColumnsWidth(obj, 0, Title);
            IntCol = 1;

            strTitle = "地市名称 ";
            PF.CreateSheetView(obj, NextRowMerge += 2, NextColMerge, IntRow, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "2010年 ";
            PF.CreateSheetView(obj, NextRowMerge -= 2, NextColMerge+=6, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "GDP（亿元） ";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge-=6, IntRow+=1, IntCol , strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "分产业结构GDP（亿元） ";
            PF.CreateSheetView(obj, NextRowMerge -= 1, NextColMerge += 2, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "一              产 ";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge -= 2, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "二              产 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "三              产 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "一产（%） ";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow-=1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "二产（%） ";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "三产（%） ";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);


            strTitle = "2015年";
            PF.CreateSheetView(obj, NextRowMerge -= 2, NextColMerge+=6, IntRow-=1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "GDP（亿元） ";
            PF.CreateSheetView(obj, NextRowMerge += 2, NextColMerge-=6, IntRow+=1, IntCol , strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "分产业结构GDP（亿元） ";
            PF.CreateSheetView(obj, NextRowMerge -= 2, NextColMerge += 2, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "一              产 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge -= 2, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "二              产 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "三              产 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "一产（%） ";
            PF.CreateSheetView(obj, NextRowMerge += 2, NextColMerge, IntRow-=1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "二产（%） ";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "三产（%） ";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;

            //设置行高
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);
            #endregion

        }
        /// <summary>
        /// click SheetTab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_SheetTabClick(object sender, FarPoint.Win.Spread.SheetTabClickEventArgs e)
        {
            switch(e.SheetTabIndex)
            {
                case 0:
                    this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    //SheetN2_1(fpSpread1_Sheet1, T.T1);
                    break;
                case 1:
                    this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem1.Width = 80;
                    //S2_2.InitTitle();
                    //SheetN2_2(fpSpread1_Sheet2, T.T2);
                    //fpSpread1_Sheet2 = PF.CreateSheet(this, fpSpread1, "表2‑2  2009年铜陵市经济社会发展情况");
                    S2_2.AddBarEditYears(this.barEditItem1);
                    break;
                case 2:
                    this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never ;
                    //S2_3.InitTitle();
                    ////SheetN2_3(fpSpread1_Sheet3, T.T3);
                    //S2_3.ConnectionDate(fpSpread1_Sheet3,this);
                    //fpSpread1_Sheet3 = PF.CreateSheet(this, fpSpread1, "表2‑3 铜陵市各地级市国民经济与社会发展目标");
                    break;
                case 3:
                    this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    //S2_4.InitTitle();
                    ////SheetN2_4(fpSpread1_Sheet4, T.T4);
                    //S2_4.ConnectionDate(fpSpread1_Sheet4,this);
                    //fpSpread1_Sheet4 = PF.CreateSheet(this, fpSpread1, "表2‑4 铜陵市各地级市国民经济产业结构发展趋势");
                    break;
                default:
                    //this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    //this.barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    //SheetN2_1(fpSpread1_Sheet1, T.T1);
                    break;
            }
        }
        /// <summary>
        /// barEditValue Chagned
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEditItem1_EditValueChanged(object sender, EventArgs e)
        {
            string year = null;
            int IntYear = 0;
            year = this.barEditItem1.EditValue.ToString();
            int.TryParse(year, out IntYear);
            if (IntYear < 2000 || IntYear > 2060)
            {
                MessageBox.Show("请输入2000年-2060年之间的数字！！！");
                this.barEditItem1.EditValue ="2000";
            }
            else
            {
                fpSpread1.Sheets[1].SheetName = "表2‑2  " + year + "年铜陵市经济社会发展情况";
                IntRow = 3;
                IntCol = fpSpread1.Sheets[1].Columns.Count;
                PF.CreateSheetView(fpSpread1.Sheets[1], IntRow, IntCol, 0, 0, fpSpread1.Sheets[1].SheetName);
                S2_2.ConnectionDate(this.fpSpread1.Sheets[1], this, this.barEditItem1);
            }
        }
        /// <summary>
        /// 重新计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //switch(fpSpread1.ActiveSheetIndex )
            //{
            //    case 2:
            //        S2_3.InitTitle();
            //        S2_3.ConnectionDate(fpSpread1_Sheet3, this);
            //        break;
            //    case 3:
            //        S2_4.SumValue = 1;
            //        S2_4.InitTitle();
            //        S2_4.ConnectionDate(fpSpread1_Sheet4, this);
            //        break;
            //    default:
            //        break;
            //}
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PF.SaveExcel(this.fpSpread1,"Sheet2_1");
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PF.ToExcel(fpSpread1);
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm newwait = new WaitDialogForm("", "正在更新数据, 请稍候...");
            ////由于SheetView已经没有了所以移除SheetView
           
            //由于更新数据时间太长需要建个空表做初始界面
            //FarPoint.Win.Spread.SheetView fpSpread1_Sheet5 = new FarPoint.Win.Spread.SheetView();
            //PF.CreateSheet(this, fpSpread1, "kk");
            SheetIndex = fpSpread1.ActiveSheetIndex;//记住当前的SheetView的索引值
            fpSpread1.ActiveSheet = fpSpread1.Sheets[fpSpread1.Sheets.Count-1];//把当前界面给空表

            //PF.RemoveSheetView(this.fpSpread1, (fpSpread1.Sheets.Count-1));
            //MessageBox.Show(fpSpread1.Sheets[0].SheetName);
            //===================================================

            this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            fpSpread1.Sheets.Clear();
            IsCreateSheet = false;
            LoadData();

            newwait.Close();
            IsCreateSheet = true;
            fpSpread1.ActiveSheet = fpSpread1.Sheets[SheetIndex];//把以前显示的界面在显示出来
            //删除临时SheetView
            //fpSpread1.Sheets.Remove(fpSpread1.Sheets[fpSpread1.Sheets.Count - 1]);
            if (SheetIndex == 1)//第二个工作簿
            {
                this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            MessageBox.Show("更新数据完成！");
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="obj"></param>
        private void AddData(FarPoint.Win.Spread.FpSpread obj)
        {
            WaitDialogForm wait = null;
            wait = new WaitDialogForm("", "正在加载数据, 请稍候...");
            try
            {
                //打开Excel表格
                //清空工作表
                fpSpread1.Sheets.Clear();
                obj.OpenExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\Sheet2_1.xls");
                PF.SpreadRemoveEmptyCells(obj);
                this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                for(int i=0;i<fpSpread1.Sheets.Count;++i)
                {
                    fpSpread1.Sheets[i].CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(FrmSheet2_1_CellChanged);
                }
            }
            catch (System.Exception e)
            {
                //如果打开出错则重新生成并保存
                LoadData();
                //判断文件夹是否存在，不存在则创建
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\xls"))
                {
                    Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\xls");
                }
                //保存EXcel文件
                obj.SaveExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\Sheet2_1.xls", FarPoint.Excel.ExcelSaveFlags.NoFlagsSet);
                // 定义要使用的Excel 组件接口
                // 定义Application 对象,此对象表示整个Excel 程序
                Microsoft.Office.Interop.Excel.Application excelApp = null;
                // 定义Workbook对象,此对象代表工作薄
                Microsoft.Office.Interop.Excel.Workbook workBook;
                // 定义Worksheet 对象,此对象表示Execel 中的一张工作表
                Microsoft.Office.Interop.Excel.Worksheet ws = null;
                Microsoft.Office.Interop.Excel.Range range = null;
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                string filename = System.Windows.Forms.Application.StartupPath + "\\xls\\Sheet2_1.xls";
                workBook = excelApp.Workbooks.Open(filename, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                for (int i = 1; i <= workBook.Worksheets.Count; i++)
                {

                    ws = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Worksheets[i];
                    //取消保护工作表
                    ws.Unprotect(Missing.Value);
                    //有数据的行数
                    int row = ws.UsedRange.Rows.Count;
                    //有数据的列数
                    int col = ws.UsedRange.Columns.Count;
                    //创建一个区域
                    range = ws.get_Range(ws.Cells[1, 1], ws.Cells[row, col]);
                    //设区域内的单元格自动换行
                    range.WrapText = true;
                    //保护工作表
                    ws.Protect(Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                }
                //保存工作簿
                workBook.Save();
                //关闭工作簿
                excelApp.Workbooks.Close();
            }
            wait.Close();
        }
    }
}