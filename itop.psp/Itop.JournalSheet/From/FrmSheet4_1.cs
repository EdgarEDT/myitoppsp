#region 引用部分
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Threading;

using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views;
using DevExpress.Data;

//using DevExpress.XtraTreeList.Columns;
//using DevExpress.XtraTreeList.Nodes;
//using DevExpress.XtraEditors.Repository;
//using DevExpress.XtraTreeList;
using DevExpress.Utils;

using Itop.Client.Base;
using Itop.Domain.Table;
using Itop.Client.Common;
using Itop.Client.History;
using Itop.Common;
using Itop.Domain.Layouts;

using Itop.Domain.Forecast;
#endregion
/*
 修改说明：现在在电力发展实际中取数据，分区县的附表4_1_1和4_2_1暂时隐藏
 * 修改人：吕静涛
 * 修改时间：2010-11-12
 */
namespace Itop.JournalSheet
{
    public partial class frmSheet4_1 : FormBase 
    {
        string type1 = "1";
        int ColCount = 0;//列数
        int RowCount = 0;//行数
        int MergeLength = 0;//合并单元格纵向初始值
        int IntCol = 0;
        int IntRow = 0;
        int NextRowMerge = 1;//合并单元格行初始值
        int NextColMerge = 1;//合并单元格列初始值
        private bool IsCreateSheet = true;
        private const int SheetCount = 8;//有5个sheet+3个附表
        private int SheetIndex = 0;//记录当前页的index
        private WaitDialogForm wait = null;

        //private FarPoint.Win.Spread.SheetView[] fpSheetN = new FarPoint.Win.Spread.SheetView[SheetCount];
        private string[] strTitle = new string[SheetCount];
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet2 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet3 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet4 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet5 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet6 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet7 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet8 = new FarPoint.Win.Spread.SheetView();
        //private FarPoint.Win.Spread.SheetView fpSpread1_Sheet9 = new FarPoint.Win.Spread.SheetView();
        //private int CurrentRow=0;//得到当前的单元格的行数
        //private int CurrentColumn=0;//得到当前单元格的列数

        private Itop.JournalSheet.Function.PublicFunction PF = new Itop.JournalSheet.Function.PublicFunction();
        private Itop.JournalSheet.Function4.Sheet4_1_1 S4_1_1 = new Itop.JournalSheet.Function4.Sheet4_1_1();
        private Itop.JournalSheet.Function4.Sheet4_1 S4_1 = new Itop.JournalSheet.Function4.Sheet4_1();
        private Itop.JournalSheet.Function4.Sheet4_2 S4_2 = new Itop.JournalSheet.Function4.Sheet4_2();
        private Itop.JournalSheet.Function4.Sheet4_2_1 S4_2_1 = new Itop.JournalSheet.Function4.Sheet4_2_1();
        private Itop.JournalSheet.Function4.Sheet4_3 S4_3 = new Itop.JournalSheet.Function4.Sheet4_3();
        private Itop.JournalSheet.Function4.Sheet4_4 S4_4 = new Itop.JournalSheet.Function4.Sheet4_4();
        private Itop.JournalSheet.Function4.Sheet4_4_1 S4_4_1 = new Itop.JournalSheet.Function4.Sheet4_4_1();
        private Itop.JournalSheet.Function4.Sheet4_5 S4_5 = new Itop.JournalSheet.Function4.Sheet4_5();
        private Itop.JournalSheet.Function10.Sheet10_11 S10_11 = new Itop.JournalSheet.Function10.Sheet10_11();
        //private FarPoint.Win.Spread.SheetView fpSpread1_Sheet2 = null;
        //private FarPoint.Win.Spread.SheetView fpSpread1_Sheet3 = null;
        //private FarPoint.Win.Spread.SheetView fpSpread1_Sheet4 = null;
        //DataTable dataTable = new DataTable();
        //bool bLoadingData = false;
        //bool _canEdit = true;
        int firstyear = 2000;
        int endyear = 2009;
        //private FarPoint.Win.Spread.FpSpread FPSpread1;
        //private DevExpress.Utils.HorzAlignment _colTitleAlign = DevExpress.Utils.HorzAlignment.Near;
        private int _colTitleWidth = 150;

         //TreeList xTreeList = new TreeList();
         //public TreeList LI
         //{
         //    get { return xTreeList; }
         //    set { xTreeList = value; }
         //}

        public int ColTitleWidth
        {
            get { return _colTitleWidth; }
            set { _colTitleWidth = value; }
        }

        public frmSheet4_1()
        {
            InitializeComponent();
            //隐藏时间设定按钮
            this.barButtonItem6.Visibility = DevExpress.XtraBars.BarItemVisibility.Never ;
            //隐藏打印按钮
            //this.barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
        }
        /// <summary>
        /// 初始化窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSheet4_1_Load(object sender, EventArgs e)
        {
            //wait = new WaitDialogForm("", "正在加载数据, 请稍候...");
            //LoadData();
            //wait.Close();
            AddData(this.fpSpread1);
        }
        /// <summary>
        /// 加载表中的数据
        /// </summary>
        private void LoadData()
        {
            #region draw lessons from
            /*
            DataTable dt = new DataTable();
            List<string> listColID = new List<string>();

            listColID.Add("Title");
            dt.Columns.Add("Title", typeof(string));
            dt.Columns["Title"].Caption = "项目";
            dt.Columns.Add("ParentID", typeof(string));

            foreach (TreeListColumn column in xTreeList.Columns)
            {
                if (column.FieldName.IndexOf("y") >= 0)
                {
                    listColID.Add(column.FieldName);
                    dt.Columns.Add(column.FieldName, typeof(double));
                }
                //else
                //    if (column.FieldName == "ParentID")
                //    {
                //        dt.Columns.Add("ParentID", typeof(string));
                //        listColID.Add("ParentID");
                //        dt.Columns["ParentID"].Caption = "父ID";
                //    }
            }
            listColID.Add("ParentID");
            dt.Columns["ParentID"].Caption = "父ID";

            int itemp = -4;
            int jtemp = -4;
            foreach (TreeListNode node in xTreeList.Nodes)
            {
                jtemp = itemp;
                //AddNodeDataToDataTable(dt, node, listColID, ref itemp, jtemp);
                // itemp++;
            }




            gridControl1.DataSource = dt;

            if (gridView1.Columns.Count > 0)
            {
                gridView1.Columns["Title"].Width = _colTitleWidth;
                gridView1.Columns["Title"].AppearanceCell.TextOptions.HAlignment = _colTitleAlign;
                gridView1.Columns["Title"].Caption = "项目";
                gridView1.Columns["ParentID"].Caption = "父ID";
                for (int i = 0; i < gridView1.Columns.Count; i++)
                {
                    GridColumn gridCol = gridView1.Columns[i];
                    gridCol.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    if (gridCol.FieldName.IndexOf("y") >= 0)
                    {
                        gridCol.Width = 80;
                        gridCol.Caption = gridCol.FieldName.Replace("y", "") + "年";
                        gridCol.DisplayFormat.FormatString = "n2";
                        gridCol.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    }
                    else if (gridCol.Caption.IndexOf("增长率") >= 0)
                    {
                        gridCol.Caption = "增长率";
                        gridCol.DisplayFormat.FormatString = "p2";
                        gridCol.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                        gridCol.Width = 80;
                    }
                }
                gridView1.Columns["ParentID"].VisibleIndex = gridView1.Columns.Count;
                gridView1.Columns["ParentID"].Visible = false;
            }
             * */         
#endregion
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //this.lookUpEdit1.Visible = false;
            InitTitle();
            //if (IsCreateSheet)
            //{
            //InitSheetTop();
                AddSheet();
            //}
            //显示标题,数据
            ViewTitle();
        }
        /// <summary>
        /// 格式化标题
        /// </summary>
        private void ViewTitle()
        {
            float SheetCount1 = SheetCount;
            float percent = 0;

            #region SetRowHeader
            //fpSpread1.ActiveSheet.RowHeader.Visible = true;
            ////FarPoint.Win.Spread.SheetView.DocumentModels dm = new FarPoint.Win.Spread.SheetView.DocumentModels();
            ////dm = fpSpread1.ActiveSheet.Models;
            //fpSpread1.ActiveSheet.RowHeaderColumnCount = 1;
            ////dm.RowHeaderSpan.Add(1, 1, 1, 4);
            #endregion

//==================================================================================================================
            /*这个是在电力实绩取数据现在不用，现在在分区县供电实绩*/
            #region SetYears表4_1主表的数据部分
            IntCol = 1;
           //   PF.SetYears("电力发展实绩", this, fpSpread1.Sheets[0], 5, IntCol, 1, 1);
           // MergeLength = PF.GolobalYearCount - (PF.GolobalYears / 3);//GolobalYears年份没有数据的不显示出来除3是多循环2个一级目录(市辖供电区,县级供电区)
           ////算法添加
           // PF.SheetNFormula(fpSpread1.Sheets[0],5,3);
            #endregion

            #region SetSheet_4_1主表
            if (!IsCreateSheet)
            {
                fpSpread1.Sheets[0].RowCount = 0;
                fpSpread1.Sheets[0].ColumnCount = 0;
                percent = 1 / SheetCount1;
                wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            }
            else
            {
                percent = 1 / SheetCount1;
                wait.SetCaption("正在加载数据..." + percent.ToString("P"));
            }
            S4_1.SetSheet4_1Title(this, this.fpSpread1.Sheets[0], strTitle[0]);
            //===========================================================================================================
            if (!IsCreateSheet)
            {
                fpSpread1.Sheets[1].RowCount = 0;
                fpSpread1.Sheets[1].ColumnCount = 0;
                percent = 2 / SheetCount1;
                wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            }
            else
            {
                percent = 2 / SheetCount1;
                wait.SetCaption("正在加载数据..." + percent.ToString("P"));
            }
            ////S4_1_1.SetSheet4_1_1Title(this, this.fpSpread1.Sheets[1], strTitle[1]);
            this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //this.lookUpEdit1.Visible = true;
            //S4_2_1.AddLookUpEditValue(this.lookUpEdit1,this);
            if (!IsCreateSheet)
            {
                fpSpread1.Sheets[2].RowCount = 0;
                fpSpread1.Sheets[2].ColumnCount = 0;
                percent = 3 / SheetCount1;
                wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            }
            else
            {
                percent = 3 / SheetCount1;
                wait.SetCaption("正在加载数据..." + percent.ToString("P"));
            }
            S4_2.SetSheet4_2Title(fpSpread1.Sheets[1], strTitle[1]);
            S4_2_1.AddBarEditItems(this.barEditItem2, this.barEditItem1, this);
            this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (!IsCreateSheet)
            {
                fpSpread1.Sheets[3].RowCount = 0;
                fpSpread1.Sheets[3].ColumnCount = 0;
                percent = 4 / SheetCount1;
                wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            }
            else
            {
                percent = 4 / SheetCount1;
                wait.SetCaption("正在加载数据..." + percent.ToString("P"));
            }
            ////S4_2_1.SetSheet4_2_1Title(fpSpread1.Sheets[3], strTitle[3]);

            //this.lookUpEdit1.Visible = false;
            if (!IsCreateSheet)
            {
                fpSpread1.Sheets[4].RowCount = 0;
                fpSpread1.Sheets[4].ColumnCount = 0;
                percent = 5 / SheetCount1;
                wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            }
            else
            {
                percent = 5 / SheetCount1;
                wait.SetCaption("正在加载数据..." + percent.ToString("P"));
            }
            S4_3.SetSheet4_3Title(fpSpread1, fpSpread1.Sheets[2], strTitle[2]);
            //this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            if (!IsCreateSheet)
            {
                fpSpread1.Sheets[5].RowCount = 0;
                fpSpread1.Sheets[5].ColumnCount = 0;
                percent = 6 / SheetCount1;
                wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            }
            else
            {
                percent = 6 / SheetCount1;
                wait.SetCaption("正在加载数据..." + percent.ToString("P"));
            }
            S4_4.SetSheet4_4Title(fpSpread1.Sheets[3], strTitle[3]);
            if (!IsCreateSheet)
            {
                fpSpread1.Sheets[6].RowCount = 0;
                fpSpread1.Sheets[6].ColumnCount = 0;
                percent = 7 / SheetCount1;
                wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            }
            else
            {
                percent = 7 / SheetCount1;
                wait.SetCaption("正在加载数据..." + percent.ToString("P"));
            }
            S4_4_1.SetSheet4_4_1Title(fpSpread1.Sheets[4], strTitle[4]);
            if (!IsCreateSheet)
            {
                fpSpread1.Sheets[7].RowCount = 0;
                fpSpread1.Sheets[7].ColumnCount = 0;
                percent = 8 / SheetCount1;
                wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            }
            else
            {
                percent = 8 / SheetCount1;
                wait.SetCaption("正在加载数据..." + percent.ToString("P"));
            }
            S4_5.SetSheet4_5Title(fpSpread1, fpSpread1.Sheets[5], strTitle[5]);
            //this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.AddCellChanged();
            PF.Sheet_AutoLineFeed(fpSpread1);//设置整个工作簿换行
            #endregion

        }
        private void InitTitle()
        {
            #region 初始化标题
            for (int i = 0; i < SheetCount; ++i)
            {
                switch (i)
                {
                    case 0:
                        strTitle[i] = "表4‑1  铜陵市负荷历史数据";
                        break;
                    //case 1:
                    //    //strTitle[i] = "附表29  铜陵市负荷历史数据";
                    //    break;
                    case 1:
                        strTitle[i] = "表4‑2  铜陵市分年度全社会用电量预测结果";
                        break;
                    //case 3:
                    //    //strTitle[i] = "附表30  铜陵市分区分年度用电量预测结果";
                    //    break;
                    case 2:
                        strTitle[i] = "表4‑3  铜陵市各地市分年度全社会用电量预测结果";
                        break;
                    case 3:
                        strTitle[i] = "表4-4  铜陵市分年度统调最大负荷预测结果";
                        break;
                    case 4:
                        strTitle[i] = "附表31  铜陵市分区分年度统调最大负荷预测结果";
                        break;
                    case 5:
                        strTitle[i] = "表4‑5  铜陵市分年度最大负荷预测结果";
                        break;
                             default:
                        break;
                }
            }
            #endregion
        }
        private void AddSheet()
        {
            fpSpread1.Sheets.Add(fpSpread1_Sheet1);
            fpSpread1.Sheets.Add(fpSpread1_Sheet2);
            fpSpread1.Sheets.Add(fpSpread1_Sheet3);
            fpSpread1.Sheets.Add(fpSpread1_Sheet4);
            fpSpread1.Sheets.Add(fpSpread1_Sheet5);
            fpSpread1.Sheets.Add(fpSpread1_Sheet6);
            //fpSpread1.Sheets.Add(fpSpread1_Sheet7);
            //fpSpread1.Sheets.Add(fpSpread1_Sheet8);
            //fpSpread1.Sheets.Add(fpSpread1_Sheet9);

        }
        private void InitSheetTop()
        {
            for (int i = 0; i < SheetCount; ++i)
            {
                //fpSheetN[i] = PF.CreateSheet(this, fpSpread1, strTitle[i]);
            }
        }
        /// <summary>
        /// 自动感应单元格变化这个方法太敏感导致行列值不稳定所以现在不用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmSheet4_1_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
        {
            int flag = 0;
            int ActiveSheet = 0;
            if(flag==1)
            {
                flag = 0;
                return;
            }
            else
            {
                flag =1;
                ActiveSheet = fpSpread1.ActiveSheetIndex;
                if (ActiveSheet >= 0 && ActiveSheet < SheetCount)
                {
                    //PF.SetRowHeight(fpSpread1.Sheets[ActiveSheet], e.Row, e.Column, fpSpread1.Sheets[ActiveSheet].GetValue(e.Row, e.Column));
                }
            }
        }
         /// <summary>
        /// 工作簿所有表都加载单元格自动改变响应
        /// </summary>
        private void AddCellChanged()
        {
            for (int i = 0; i < fpSpread1.Sheets.Count; ++i)//。
            {
                fpSpread1.Sheets[i].CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(frmSheet4_1_CellChanged);
            }
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
        /// 设定时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private   void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormYearSet fys = new FormYearSet();
            fys.TYPE = "表4‑1  铜陵市负荷历史数据";
            fys.PID = ProjectUID;
            if (fys.ShowDialog() != DialogResult.OK)
                return;
            firstyear = fys.SYEAR;
            endyear = fys.EYEAR;
            //PF.SetYears(fys.SYEAR, fys.EYEAR,fys.TYPE);
            //PF.RefreshSheet(fpSpread1_Sheet1);
            LoadData();
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            wait = new WaitDialogForm("", "正在更新数据, 请稍候...");
            ////由于SheetView已经没有了所以移除SheetView
            //MessageBox.Show(fpSpread1.Sheets[0].SheetName);
            //===================================================
            FarPoint.Win.Spread.SheetView obj = null;
            //由于更新数据时间太长需要建个空表做初始界面
            FarPoint.Win.Spread.SheetView fpSpread1_SheetN = new FarPoint.Win.Spread.SheetView();
            fpSpread1.Sheets.Add(fpSpread1_SheetN);
            obj = fpSpread1.ActiveSheet;
            fpSpread1.ActiveSheet = fpSpread1_SheetN;
            //PF.CreateSheet(this, fpSpread1, "kk");
            //SheetIndex = fpSpread1.ActiveSheetIndex;//记住当前的SheetView的索引值
            //fpSpread1.ActiveSheet=fpSpread1.Sheets[fpSpread1.Sheets.Count-1];//把当前界面给空表
            //PF.RemoveSheetView(this.fpSpread1, (fpSpread1.Sheets.Count - 1));//清空所有行列
            this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            //this.fpSpread1.Sheets.Clear();
            IsCreateSheet = false;

            //LoadData();
            fpSpread1.Sheets[0].ColumnCount = 0;
            fpSpread1.Sheets[0].RowCount = 0;
            S4_1.SetSheet4_1Title(this, this.fpSpread1.Sheets[0], "表4‑1  铜陵市负荷历史数据");

            //newwait.Close();
            IsCreateSheet = true;
            fpSpread1.ActiveSheet = fpSpread1.Sheets[SheetIndex];//把以前显示的界面在显示出来
            if(SheetIndex==2)
            {
                this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            //删除临时SheetView
            //fpSpread1.Sheets.Remove(fpSpread1.Sheets[fpSpread1.Sheets.Count - 1]);
            fpSpread1.Sheets.Remove(fpSpread1_SheetN);
            //MessageBox.Show("更新数据完成！");
            wait.Close();
        }
        /// <summary>
        /// 导出xls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PF.ToExcel(this.fpSpread1);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PF.SaveExcel(this.fpSpread1,"Sheet4_1" );
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="obj"></param>
        private void AddData(FarPoint.Win.Spread.FpSpread obj)
        {
            wait = new WaitDialogForm("", "正在加载数据, 请稍候...");
            try
            {
                //打开Excel表格
                //清空工作表
                fpSpread1.Sheets.Clear();
                obj.OpenExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\Sheet4_1.xls");
                PF.SpreadRemoveEmptyCells(obj);
                this.AddCellChanged();
                //S4_2_1.AddLookUpEditValue(this.lookUpEdit1,this);
                //this.lookUpEdit1.Visible = false;
                this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                S4_2_1.AddBarEditItems(this.barEditItem2, this.barEditItem1, this);
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
                obj.SaveExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\Sheet4_1.xls", FarPoint.Excel.ExcelSaveFlags.NoFlagsSet);
                // 定义要使用的Excel 组件接口
                // 定义Application 对象,此对象表示整个Excel 程序
                Microsoft.Office.Interop.Excel.Application excelApp = null;
                // 定义Workbook对象,此对象代表工作薄
                Microsoft.Office.Interop.Excel.Workbook workBook;
                // 定义Worksheet 对象,此对象表示Execel 中的一张工作表
                Microsoft.Office.Interop.Excel.Worksheet ws = null;
                Microsoft.Office.Interop.Excel.Range range = null;
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                string filename = System.Windows.Forms.Application.StartupPath + "\\xls\\Sheet4_1.xls";
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            ////MessageBox.Show("ok");
            //PF.SetRowHeight(fpSpread1.Sheets[e.View.ActiveSheetIndex], CurrentRow, CurrentColumn, fpSpread1.Sheets[e.View.ActiveSheetIndex].GetValue(CurrentRow, CurrentColumn));
        }
        /// <summary>
        /// 下拉框改变值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEditItem2_EditValueChanged(object sender, EventArgs e)
        {
            int id = 0;
            string temp = null;
            //传入选中的数据，连接数据库表
            id = ((DevExpress.XtraEditors.Repository.RepositoryItemComboBox)this.barEditItem2.Edit).Items.IndexOf(this.barEditItem2.EditValue);
            temp = (string)((DevExpress.XtraEditors.Repository.RepositoryItemComboBox)this.barEditItem1.Edit).Items[id];
            if (fpSpread1.ActiveSheetIndex == 1)
            {
                //fpSpread1.ActiveSheetIndex = 2;
                S4_2.SelectEditChange(fpSpread1,this.fpSpread1.Sheets[1], temp, this);
                //fpSpread1.ActiveSheetIndex = 3;
                //S4_2_1.SelectEditChange(this.fpSpread1.Sheets[3], temp, this);
                //fpSpread1.ActiveSheetIndex = 5;
                S4_4.SelectEditChange(this.fpSpread1, this.fpSpread1.Sheets[3], temp, this);
                //fpSpread1.ActiveSheetIndex = 6;
                S4_4_1.SelectEditChange(this.fpSpread1, fpSpread1.Sheets[4], temp, this);
                //fpSpread1.ActiveSheetIndex = 2;
            }
            if (fpSpread1.ActiveSheetIndex == 3)
            {
            }
            if(fpSpread1.ActiveSheetIndex==5)
            {
            }
            if(fpSpread1.ActiveSheetIndex ==6)
            {

            }
        }
/// <summary>
/// 工作簿改变
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void fpSpread1_SheetTabClick(object sender, FarPoint.Win.Spread.SheetTabClickEventArgs e)
        {
            if (e.SheetTabIndex == 1)
            {
                this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                //this.lookUpEdit1.Visible = true;
            }
            else
            {
                this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                //this.lookUpEdit1.Visible = false;
            }
        }
        /// <summary>
        /// 改变下拉框值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //传入选中的数据，连接数据库表

            //if (fpSpread1.ActiveSheetIndex == 2)
            //    S4_2.SelectEditChange(this.fpSpread1.Sheets[2], this.lookUpEdit1.EditValue, this);
            //if (fpSpread1.ActiveSheetIndex == 3)
            //    S4_2_1.SelectEditChange(this.fpSpread1.Sheets[3], this.lookUpEdit1.EditValue, this);
        }

    }
}