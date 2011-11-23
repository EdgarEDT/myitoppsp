using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Threading;

using Itop.Client.Common;
using Itop.Common;
using Itop.Client.Base;

using DevExpress.Utils;

namespace Itop.JournalSheet.From
{
    public partial class FrmSupplement : FormBase
    {
        private bool IsCreateSheet = true;
        private const int SheetCount = 16;//
        //private FarPoint.Win.Spread.SheetView[] fpSheetN = new FarPoint.Win.Spread.SheetView[SheetCount];
        private string[] strTitle = new string[SheetCount];
        private int SheetIndex = 0;
        private int CurrentRow = 0;//记住当前行数。
        private FormWait myProcessBar = null;
        private delegate bool IncreaseHandle(int nValue);
        private IncreaseHandle myIncrease = null;
        private  WaitDialogForm wait = null;

        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet2 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet3 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet4 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet5 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet6 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet7 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet8 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet9 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet10 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet11 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet12 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet13 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet14 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet15 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet16 = new FarPoint.Win.Spread.SheetView();

        private Itop.JournalSheet.Function.PublicFunction PF = new Itop.JournalSheet.Function.PublicFunction();
        Itop.JournalSheet.FunctionSupplement.Sheet_1 S_1 = new Itop.JournalSheet.FunctionSupplement.Sheet_1();
        Itop.JournalSheet.FunctionSupplement.Sheet_2 S_2 = new Itop.JournalSheet.FunctionSupplement.Sheet_2();
        Itop.JournalSheet.FunctionSupplement.Sheet_3 S_3 = new Itop.JournalSheet.FunctionSupplement.Sheet_3();
        Itop.JournalSheet.FunctionSupplement.Sheet_4 S_4 = new Itop.JournalSheet.FunctionSupplement.Sheet_4();
        Itop.JournalSheet.FunctionSupplement.Sheet_5 S_5 = new Itop.JournalSheet.FunctionSupplement.Sheet_5();
        Itop.JournalSheet.FunctionSupplement.Sheet_6 S_6 = new Itop.JournalSheet.FunctionSupplement.Sheet_6();
        Itop.JournalSheet.FunctionSupplement.Sheet_7 S_7 = new Itop.JournalSheet.FunctionSupplement.Sheet_7();
        Itop.JournalSheet.FunctionSupplement.Sheet_8 S_8 = new Itop.JournalSheet.FunctionSupplement.Sheet_8();
        Itop.JournalSheet.FunctionSupplement.Sheet_9 S_9= new Itop.JournalSheet.FunctionSupplement.Sheet_9();
        Itop.JournalSheet.FunctionSupplement.Sheet_10 S_10 = new Itop.JournalSheet.FunctionSupplement.Sheet_10();
        Itop.JournalSheet.FunctionSupplement.Sheet_11 S_11 = new Itop.JournalSheet.FunctionSupplement.Sheet_11();
        Itop.JournalSheet.FunctionSupplement.Sheet_12 S_12 = new Itop.JournalSheet.FunctionSupplement.Sheet_12();
        Itop.JournalSheet.FunctionSupplement.Sheet_13 S_13 = new Itop.JournalSheet.FunctionSupplement.Sheet_13();
        Itop.JournalSheet.FunctionSupplement.Sheet_14 S_14 = new Itop.JournalSheet.FunctionSupplement.Sheet_14();
        Itop.JournalSheet.FunctionSupplement.Sheet_15 S_15 = new Itop.JournalSheet.FunctionSupplement.Sheet_15();
        Itop.JournalSheet.FunctionSupplement.Sheet_16 S_16 = new Itop.JournalSheet.FunctionSupplement.Sheet_16();
        public FrmSupplement()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PF.ToExcel(this.fpSpread1);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PF.SaveExcel(this.fpSpread1, "Supplement");
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
             wait = new WaitDialogForm("", "正在更新数据, 请稍候...");
            ////由于SheetView已经没有了所以移除SheetView
            //MessageBox.Show(fpSpread1.Sheets[0].SheetName);
            //===================================================
            //由于更新数据时间太长需要建个空表做初始界面
            FarPoint.Win.Spread.SheetView obj = null;
            FarPoint.Win.Spread.SheetView fpSpread1_SheetN = new FarPoint.Win.Spread.SheetView();
            SheetNClear(fpSpread1_SheetN);
            fpSpread1.Sheets.Add(fpSpread1_SheetN);

            //PF.CreateSheet(this, fpSpread1, "kk");
            //SheetIndex = fpSpread1.ActiveSheetIndex;//记住当前的SheetView的索引值
            //fpSpread1.ActiveSheet = fpSpread1.Sheets[fpSpread1.Sheets.Count - 1];//把当前界面给空表
            //PF.RemoveSheetView(this.fpSpread1, (fpSpread1.Sheets.Count - 1));//清空所有行列

            obj = fpSpread1.ActiveSheet;
            fpSpread1.ActiveSheet = fpSpread1_SheetN;
            //清空表之前先保存手写的部分
            SaveHandWriting();
            this.fpSpread1.Sheets.Clear();

            IsCreateSheet = false;
            ////启动线程
            //Thread thdSub = new Thread(new ThreadStart(ThreadFun));
            //thdSub.Start();
            Updateing();

            IsCreateSheet = true;
            fpSpread1.ActiveSheet = fpSpread1.Sheets[SheetIndex];//把以前显示的界面在显示出来
            //删除临时SheetView
            fpSpread1.Sheets.Remove(fpSpread1_SheetN);
            if (SheetIndex == 2 || SheetIndex == 3 || SheetIndex == 4 || SheetIndex == 5 || SheetIndex == 6)
            {
                this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            } 
            //MessageBox.Show("更新数据完成！");
            wait.Close();
        }
        /// <summary>
        /// 保存手写部分供更新用
        /// </summary>
        private void SaveHandWriting()
        {
            S_1.SaveData(fpSpread1.Sheets[0], 5, 2, 1, 3);
            S_8.SaveData(fpSpread1.Sheets[7], 5, 3, 2, 7);
            S_9.SaveData(fpSpread1.Sheets[8], 5, 4, 10, 7);
            S_10.SaveData(fpSpread1.Sheets[9], 5, 4, 6, 6);
            S_12.SaveData(fpSpread1.Sheets[11], 5, 3, 3, 7);
        }
        /// <summary>
        /// 清空指定的表
        /// </summary>
        private void SheetNClear(FarPoint.Win.Spread.SheetView obj)
        {

            obj.RowCount = 0;
           obj.ColumnCount = 0;

        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void FrmSupplement_Load(object sender, EventArgs e)
        {
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            ////启动线程
            //Thread thdSub = new Thread(new ThreadStart(ThreadFun));
            //thdSub.Start();
            AddData(fpSpread1);
        }
        private void LoadData()
        {
            InitTitle();
            //if (IsCreateSheet)
            //{
            //    InitSheetTop();
            //}
            //else
            //{
            //    NewSheet();
            //}
             InitSheetData();//加载表数据
        }
        /// <summary>
        /// 加载数据,表头
        /// </summary>
        private void InitSheetData()
        {
            float SheetCount1 = SheetCount;
            float percent = 0;
            InitMenu();
            this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            percent = 1 / SheetCount1;
            wait.SetCaption("正在加载数据..."+percent.ToString("P"));
            S_1.SetSheet_1Title(this, fpSpread1_Sheet1, strTitle[0], false);//手写

            percent = 2 / SheetCount1;
            wait.SetCaption("正在加载数据..." + percent.ToString("P"));
            S_2.SetSheet_2Title(this, fpSpread1_Sheet2, strTitle[1]);
            /*从下面表开始连续5各表有年份下拉菜单*********************************/
            this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            S_3.AddBarEditItems(this.barEditItem1);
            S_3.strYear = this.barEditItem1.EditValue.ToString();
            percent = 3 / SheetCount1;
            wait.SetCaption("正在加载数据..." + percent.ToString("P"));
            S_3.SetSheet_3Title(this, fpSpread1_Sheet3, strTitle[2]);
            S_4.strYear = this.barEditItem1.EditValue.ToString();
            percent = 4 / SheetCount1;
            wait.SetCaption("正在加载数据..." + percent.ToString("P"));
            S_4.SetSheet_4Title(this, fpSpread1_Sheet4, strTitle[3]);
            S_5.strYear = this.barEditItem1.EditValue.ToString();
            percent = 5 / SheetCount1;
            wait.SetCaption("正在加载数据..." + percent.ToString("P"));
            S_5.SetSheet_5Title(this, fpSpread1_Sheet5, strTitle[4]);
            S_6.strYear = this.barEditItem1.EditValue.ToString();
            percent = 6 / SheetCount1;
            wait.SetCaption("正在加载数据..." + percent.ToString("P"));
            S_6.SetSheet_6Title(this, fpSpread1_Sheet6, strTitle[5]);
            S_7.strYear = this.barEditItem1.EditValue.ToString();
            percent = 7 / SheetCount1;
            wait.SetCaption("正在加载数据..." + percent.ToString("P"));
            S_7.SetSheet_7Title(this, fpSpread1_Sheet7, strTitle[6]);
            ///******************************************************************************************/
            this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            percent = 1 / SheetCount1;
            wait.SetCaption("正在加载数据..." + percent.ToString("P"));
            S_8.SetSheet_8Title(this, fpSpread1_Sheet8, strTitle[7], false);//手写
            percent = 9 / SheetCount1;
            wait.SetCaption("正在加载数据..." + percent.ToString("P"));
            S_9.SetSheet_9Title(this, fpSpread1_Sheet9, strTitle[8], false);//手写
            percent = 10 / SheetCount1;
            wait.SetCaption("正在加载数据..." + percent.ToString("P"));
            S_10.SetSheet_10Title(this, fpSpread1_Sheet10, strTitle[9], false);//手写

            percent = 11 / SheetCount1;
            wait.SetCaption("正在加载数据..." + percent.ToString("P"));
            S_11.SetSheet_11Title(this, fpSpread1_Sheet11, strTitle[10]);
            percent = 12 / SheetCount1;
            wait.SetCaption("正在加载数据..." + percent.ToString("P"));
            S_12.SetSheet_12Title(this, fpSpread1_Sheet12, strTitle[11], false);//手写

            percent = 13 / SheetCount1;
            wait.SetCaption("正在加载数据..." + percent.ToString("P"));
            S_13.SetSheet_13Title(this, fpSpread1_Sheet13, strTitle[12]);
            percent = 14 / SheetCount1;
            wait.SetCaption("正在加载数据..." + percent.ToString("P"));
            S_14.SetSheet_14Title(this, fpSpread1_Sheet14, strTitle[13]);
            percent = 15 / SheetCount1;
            wait.SetCaption("正在加载数据..." + percent.ToString("P"));
            S_15.SetSheet_15Title(this, fpSpread1_Sheet15, strTitle[14]);
            percent = 16 / SheetCount1;
            wait.SetCaption("正在加载数据..." + percent.ToString("P"));
            S_16.SetSheet_16Title(this, fpSpread1_Sheet16, strTitle[15]);

            AddSheet();
            this.AddCellChanged();
            PF.Sheet_AutoLineFeed(fpSpread1);//设置整个工作簿换行
        }
        /// <summary>
        /// 更新时加载数据
        /// </summary>
        private void Updateing()
        {
            float SheetCount1 = SheetCount;
            float percent = 0;
            AddSheet();
            InitMenu();
            this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            SheetNClear(fpSpread1.Sheets[0]);
            percent = 1 / SheetCount1;
            wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            S_1.SetSheet_1Title(this, fpSpread1.Sheets[0], strTitle[0], true);//手写
            SheetNClear(fpSpread1.Sheets[1]);
            percent = 2 / SheetCount1;
            wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            S_2.SetSheet_2Title(this, fpSpread1.Sheets[1], strTitle[1]);
            /*从下面表开始连续5各表有年份下拉菜单*********************************/
            this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            S_3.AddBarEditItems(this.barEditItem1);
            S_3.strYear = this.barEditItem1.EditValue.ToString();
            SheetNClear(fpSpread1.Sheets[2]);
            percent = 3 / SheetCount1;
            wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            S_3.SetSheet_3Title(this, fpSpread1.Sheets[2], strTitle[2]);
            S_4.strYear = this.barEditItem1.EditValue.ToString();
            SheetNClear(fpSpread1.Sheets[3]);
            percent = 4 / SheetCount1;
            wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            S_4.SetSheet_4Title(this, fpSpread1.Sheets[3], strTitle[3]);
            S_5.strYear = this.barEditItem1.EditValue.ToString();
            SheetNClear(fpSpread1.Sheets[4]);
            percent = 5 / SheetCount1;
            wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            S_5.SetSheet_5Title(this, fpSpread1.Sheets[4], strTitle[4]);
            S_6.strYear = this.barEditItem1.EditValue.ToString();
            SheetNClear(fpSpread1.Sheets[5]);
            percent = 6 / SheetCount1;
            wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            S_6.SetSheet_6Title(this, fpSpread1.Sheets[5], strTitle[5]);
            S_7.strYear = this.barEditItem1.EditValue.ToString();
            SheetNClear(fpSpread1.Sheets[6]);
            percent = 7 / SheetCount1;
            wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            S_7.SetSheet_7Title(this, fpSpread1.Sheets[6], strTitle[6]);
            ///******************************************************************************************/
            this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            SheetNClear(fpSpread1.Sheets[7]);
            percent = 8 / SheetCount1;
            wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            S_8.SetSheet_8Title(this, fpSpread1.Sheets[7], strTitle[7], true);//手写
            SheetNClear(fpSpread1.Sheets[8]);
            percent = 9 / SheetCount1;
            wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            S_9.SetSheet_9Title(this, fpSpread1.Sheets[8], strTitle[8], true);//手写
            SheetNClear(fpSpread1.Sheets[9]);
            percent = 10 / SheetCount1;
            wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            S_10.SetSheet_10Title(this, fpSpread1.Sheets[9], strTitle[9], true);//手写

            percent = 11 / SheetCount1;
            wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            S_11.SetSheet_11Title(this, fpSpread1.Sheets[10], strTitle[10]);
            SheetNClear(fpSpread1.Sheets[11]);
            percent = 12 / SheetCount1;
            wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            S_12.SetSheet_12Title(this, fpSpread1.Sheets[11], strTitle[11], true);//手写

            SheetNClear(fpSpread1.Sheets[12]);
            percent = 13 / SheetCount1;
            wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            S_13.SetSheet_13Title(this, fpSpread1.Sheets[12], strTitle[12]);
            SheetNClear(fpSpread1.Sheets[13]);
            percent = 14 / SheetCount1;
            wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            S_14.SetSheet_14Title(this, fpSpread1.Sheets[13], strTitle[13]);
            SheetNClear(fpSpread1.Sheets[14]);
            percent = 15 / SheetCount1;
            wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            S_15.SetSheet_15Title(this, fpSpread1.Sheets[14], strTitle[14]);
            SheetNClear(fpSpread1.Sheets[15]);
            percent = 16 / SheetCount1;
            wait.SetCaption("正在更新数据..." + percent.ToString("P"));
            S_16.SetSheet_16Title(this, fpSpread1.Sheets[15], strTitle[15]);
        }
        /// <summary>
        /// 初始化右键菜单
        /// </summary>
        private void InitMenu()
        {
            this.AddToolStripMenuItem.Visible = false;
            this.AddToolStripMenuItem.Text = "点击增加一行";
            this.fpSpread1.ContextMenuStrip = contextMenuStrip1;
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitTitle()
        {
            #region 初始化标题
            for (int i = 0; i < SheetCount; ++i)
            {
                switch (i)
                {
                    case 0:
                        strTitle[i] = "表7-13  铜陵市高压配电网三相短路电流计算结果";
                        break;
                    case 1:
                        strTitle[i] = "表10-1  铜陵市输变电工程综合造价表";
                        break;
                    case 2:
                        strTitle[i] = "附表4  铜陵市高压配电网变电站规模明细表(2009年)";
                        break;
                    case 3:
                        strTitle[i] = "附表7  铜陵市高压配电网线路规模明细表(2009年)";
                        break;
                    case 4:
                        strTitle[i] = "附表13  铜陵市中压配电网配变规模明细表（2009年）";
                        break;
                    case 5:
                        strTitle[i] = "附表15  铜陵市中压配电网线路规模明细表（2009年）";
                        break;
                    case 6:
                        strTitle[i] = "附表18  铜陵县（区）中压配电网开关、开闭站.环网柜及电缆分支箱规模明细表（2009年）";
                        break;
                    case 7:
                        strTitle[i] = "附表33  铜陵市高压配电网分区分年度网供负荷计算结果";
                        break;
                    case 8:
                        strTitle[i] = "附表34  铜陵市高压配电网主变容量需求分析";
                        break;
                    case 9:
                        strTitle[i] = "附表35  铜陵市高压配电网规划方案分区容载比校核结果";
                        break;
                    case 10:
                        strTitle[i] = "附表41  铜陵市高压配电网各变电站计算负荷";
                        break;
                    case 11:
                        strTitle[i] = "附表42  铜陵市中压配电网分区分年度网供负荷计算结果";
                        break;
                    case 12:
                        strTitle[i] = "附表43  铜陵市新增配变容量需求估算";
                        break;
                    case 13:
                        strTitle[i] = "附表44  铜陵市中压配变建设改造工程量统计";
                        break;
                    case 14:
                        strTitle[i] = "附表3-2 铜陵市中压配电网新建工程明细表";
                        break;
                    case 15:
                        strTitle[i] = "附表4-2 铜陵市中压配电网改造工程明细表";
                        break;

                    default:
                        break;
                }
            }
            #endregion
        }
        //private void NewSheet()
        //{
        //    for (int i = 0; i < SheetCount; ++i)
        //    {
        //        fpSheetN[i] = new FarPoint.Win.Spread.SheetView();
        //    }
        //}
        private void AddSheet()
        {
            fpSpread1.Sheets.Add(fpSpread1_Sheet1);
            fpSpread1.Sheets.Add(fpSpread1_Sheet2);
            fpSpread1.Sheets.Add(fpSpread1_Sheet3);
            fpSpread1.Sheets.Add(fpSpread1_Sheet4);
            fpSpread1.Sheets.Add(fpSpread1_Sheet5);
            fpSpread1.Sheets.Add(fpSpread1_Sheet6);
            fpSpread1.Sheets.Add(fpSpread1_Sheet7);
            fpSpread1.Sheets.Add(fpSpread1_Sheet8);
            fpSpread1.Sheets.Add(fpSpread1_Sheet9);
            fpSpread1.Sheets.Add(fpSpread1_Sheet10);
            fpSpread1.Sheets.Add(fpSpread1_Sheet11);
            fpSpread1.Sheets.Add(fpSpread1_Sheet12);
            fpSpread1.Sheets.Add(fpSpread1_Sheet13);
            fpSpread1.Sheets.Add(fpSpread1_Sheet14);
            fpSpread1.Sheets.Add(fpSpread1_Sheet15);
            fpSpread1.Sheets.Add(fpSpread1_Sheet16);

        }
        /// <summary>
        /// 初始化工作簿
        /// </summary>
        //private void InitSheetTop()
        //{
        //    NewSheet();
        //    for (int i = 0; i < SheetCount; ++i)
        //    {
        //        //fpSheetN[i] = PF.CreateSheet(this, fpSpread1, strTitle[i]);
        //        this.fpSpread1.Sheets.Add(fpSheetN[i]);
        //    }
        //}
        /// <summary>
        /// 自动改变行高，这个方法与更新数据冲突现在不用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FrmSupplement_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
        {
            int flag = 0;
            //int ActiveSheet = 0;
            if (flag == 1)
            {
                flag = 0;
                return;
            }
            else
            {
                flag = 1;
                //ActiveSheet = fpSpread1.ActiveSheetIndex;
                //PF.SetRowHeight(fpSpread1.Sheets[ActiveSheet], e.Row, e.Column, fpSpread1.Sheets[ActiveSheet].GetValue(e.Row, e.Column));
                //PF.SetRowHeight(fpSpread1.Sheets[this.SheetIndex], e.Row, e.Column, fpSpread1.Sheets[this.SheetIndex].GetValue(e.Row, e.Column));
            }
        }
        /// <summary>
        /// 工作簿所有表都加载单元格自动改变响应
        /// </summary>
        private void AddCellChanged()
        {
            for (int i = 0; i < fpSpread1.Sheets.Count; ++i)
            {
                fpSpread1.Sheets[i].CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(FrmSupplement_CellChanged);
            }
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="obj"></param>
        private void AddData(FarPoint.Win.Spread.FpSpread obj)
        {
            try
            {
                wait = new WaitDialogForm("", "正在加载数据, 请稍候...");
                //打开Excel表格
                //清空工作表
                fpSpread1.Sheets.Clear();
                obj.OpenExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\Supplement.xls");
                PF.SpreadRemoveEmptyCells(obj);
                this.AddCellChanged();
                InitMenu();//添加右键显示菜单功能
                InitTitle();
                SheetIndex=fpSpread1.ActiveSheetIndex;
                if (SheetIndex == 2 || SheetIndex == 3 || SheetIndex == 4 || SheetIndex == 5 || SheetIndex == 6)
                {
                    this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    S_3.AddBarEditItems(this.barEditItem1);
                }
                else
                {
                    this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                //wait.Close();
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
                obj.SaveExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\Supplement.xls", FarPoint.Excel.ExcelSaveFlags.NoFlagsSet);
                // 定义要使用的Excel 组件接口
                // 定义Application 对象,此对象表示整个Excel 程序
                Microsoft.Office.Interop.Excel.Application excelApp = null;
                // 定义Workbook对象,此对象代表工作薄
                Microsoft.Office.Interop.Excel.Workbook workBook;
                // 定义Worksheet 对象,此对象表示Execel 中的一张工作表
                Microsoft.Office.Interop.Excel.Worksheet ws = null;
                Microsoft.Office.Interop.Excel.Range range = null;
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                string filename = System.Windows.Forms.Application.StartupPath + "\\xls\\Supplement.xls";
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
        /// 单击右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            if(SheetIndex==1)
            {
                this.fpSpread1.Sheets[1].Rows.Add(this.fpSpread1.Sheets[1].RowCount, 1);
                this.fpSpread1.Sheets[1].Rows[this.fpSpread1.Sheets[1].RowCount - 1].Locked = false;
                PF.Sheet_GridandCenter(this.fpSpread1.Sheets[1]);//画边线，居中
            }
            //if(SheetIndex==11)
            //{
            //    this.fpSpread1.Sheets[1].Rows.Add(CurrentRow, 1);
            //    this.fpSpread1.Sheets[1].Rows[CurrentRow].Locked = false;
            //    PF.Sheet_GridandCenter(this.fpSpread1.Sheets[1]);//画边线，居中
            //}
        }

        /// <summary>
        /// 在哪个区域显示右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if(this.fpSpread1.Sheets[1].RowCount-1==e.Row )
            {
                this.AddToolStripMenuItem.Visible = true;
            }
            //else if(SheetIndex==11)
            //{
            //    this.AddToolStripMenuItem.Visible = true;
            //    CurrentRow = e.Row;
            //}
            else
            {
                this.AddToolStripMenuItem.Visible = false;
            }
        }
        /// <summary>
        /// 改变下拉列表内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEditItem1_EditValueChanged(object sender, EventArgs e)
        {
            string year = null;
            int IntYear = 0;
            year = this.barEditItem1.EditValue.ToString();
            int.TryParse(year, out IntYear);
            if (IntYear < 1970 || IntYear > 2060)
            {
                MessageBox.Show("请输入1970年-2060年之间的数字！！！");
                this.barEditItem1.EditValue = "1970";
            }
            else
            {
                if(this.SheetIndex ==2)
                {
                    S_3.strYear = year;
                    S_3.SetSheet_3Title(this, this.fpSpread1.Sheets[2], strTitle[2]);
                }
                if(this.SheetIndex==3)
                {
                    S_4.strYear = year;
                    S_4.SetSheet_4Title(this, this.fpSpread1.Sheets[3], strTitle[3]);
                }
                if(this.SheetIndex==4)
                {
                    S_5.strYear = year;
                    S_5.SetSheet_5Title(this, this.fpSpread1.Sheets[4], strTitle[4]);
                }
                if (this.SheetIndex == 5)
                {
                    S_6.strYear = year;
                    S_6.SetSheet_6Title(this, this.fpSpread1.Sheets[5], strTitle[5]);
                }
                if (this.SheetIndex == 6)
                {
                    S_7.strYear = year;
                    S_7.SetSheet_7Title(this, this.fpSpread1.Sheets[6], strTitle[6]);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_TabIndexChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 点击sheettab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_SheetTabClick(object sender, FarPoint.Win.Spread.SheetTabClickEventArgs e)
        {
            SheetIndex = e.SheetTabIndex;
            if (e.SheetTabIndex == 2 || e.SheetTabIndex == 3 || e.SheetTabIndex == 4 || e.SheetTabIndex == 5 || e.SheetTabIndex == 6)
            {
                this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                S_3.AddBarEditItems(this.barEditItem1);
            }
            else
            {
                this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }
        /// <summary>

        /// Open process bar window

        /// </summary>

        //private void ShowProcessBar()
        //{

        //    myProcessBar = new FormWait();
  
        //    // Init increase event

        //    myIncrease = new IncreaseHandle(myProcessBar.Increase);

        //    myProcessBar.ShowDialog();
        //    if (IsCreateSheet)
        //    {
        //        myProcessBar.Text = "正在加载数据请等待...";
        //        AddData(fpSpread1);
        //    }
        //    else
        //    {
        //        myProcessBar.Text = "正在更新数据请等待...";
        //        //Updateing();
        //    }
        //    myProcessBar = null;

        //}
        /// <summary>

        /// Sub thread function

        /// </summary>
        //private void ThreadFun()
        //{

        //    System.Windows.Forms.MethodInvoker mi = new System.Windows.Forms.MethodInvoker(ShowProcessBar);

        //    this.BeginInvoke(mi);


        //    Thread.Sleep(1000);//Sleep a while to show window
        //    //Application.DoEvents();

        //    bool blnIncreased = false;

        //    object objReturn = null;

        //    do
        //    {

        //        Thread.Sleep(50);
        //        objReturn = this.Invoke(this.myIncrease,

        //            new object[] { 1 });

        //        blnIncreased = (bool)objReturn;

        //    }

        //    while (blnIncreased);

        //}
    }
}