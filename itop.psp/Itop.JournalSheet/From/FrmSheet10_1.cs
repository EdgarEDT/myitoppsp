using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Threading;

using Itop.Client.Base;
using DevExpress.Utils;
using DevExpress.XtraEditors;

namespace Itop.JournalSheet.From
{
    public partial class FrmSheet10_1 : FormBase
    {
        private int SheetIndex = 0;
        private const int SheetCount = 14;//有11个sheet+3个附表。
        private bool IsCreateSheet = true;
        //private FormWait wait = new FormWait();
        private WaitDialogForm wait1 = null;


        //private FarPoint.Win.Spread.SheetView[] fpSheetN = new FarPoint.Win.Spread.SheetView[SheetCount];
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

        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();
        private Function10.Sheet10_1_1 S10_1_1 = new Function10.Sheet10_1_1();
        private Function10.Sheet10_2 S10_2 = new Function10.Sheet10_2();
        private Function10.Sheet10_3 S10_3 = new Function10.Sheet10_3();
        private Function10.Sheet10_3_1 S10_3_1 = new Function10.Sheet10_3_1();
        private Function10.Sheet10_4 S10_4 = new Function10.Sheet10_4();
        private Function10.Sheet10_4_1 S10_4_1 = new Function10.Sheet10_4_1();
        private Function10.Sheet10_5 S10_5 = new Function10.Sheet10_5();
        private Function10.Sheet10_6 S10_6 = new Function10.Sheet10_6();
        private Function10.Sheet10_7 S10_7 = new Function10.Sheet10_7();
        private Function10.Sheet10_8 S10_8 = new Function10.Sheet10_8();
        private Function10.Sheet10_9 S10_9 = new Function10.Sheet10_9();
        private Function10.Sheet10_10 S10_10 = new Function10.Sheet10_10();
        private Function10.Sheet10_11 S10_11 = new Function10.Sheet10_11();

        private string[] Title = new string[SheetCount];
        
        public FrmSheet10_1()
        {
            InitializeComponent();
        }

        private void FrmSheet10_1_Load(object sender, EventArgs e)
        {
            AddData(fpSpread1);
            //LoadData(AddDataWork);
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        private void LoadData()
        {
            float  percent = 0;
            float SheetCount1 = SheetCount;
            this.Text = " 铜陵市各电压等级配电网规划建设工程投资估算 ";
            InitTitle();
            AddSheet();
            this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            percent = (1 / SheetCount1);
            wait1.SetCaption("正在加载数据..." + percent.ToString("P"));
            S10_1.SetSheet10_1Title(fpSpread1_Sheet1, Title[0]);
            percent = (2 / SheetCount1);
            wait1.SetCaption( "正在加载数据..." + percent.ToString("P"));
            S10_1_1.SetSheet10_1_1Title(fpSpread1_Sheet2, Title[1]);
            percent = (3 / SheetCount1);
            wait1.SetCaption("正在加载数据..." + percent.ToString("P"));
            S10_2.SetSheet10_2Title(fpSpread1_Sheet3, Title[2]);
            percent = (4 / SheetCount1);
            wait1.SetCaption("正在加载数据..." + percent.ToString("P"));
            S10_3.SetSheet10_3Title(fpSpread1_Sheet4, Title[3]);
            percent = (5 / SheetCount1);
            wait1.SetCaption("正在加载数据..." + percent.ToString("P"));
            S10_3_1.SetSheet10_3_1Title(fpSpread1_Sheet5, Title[4]);
            percent = (6 / SheetCount1);
            wait1.SetCaption("正在加载数据..." + percent.ToString("P"));
            S10_4.SetSheet10_4Title(fpSpread1, fpSpread1_Sheet6, Title[5]);
            percent = (7 / SheetCount1);
            wait1.SetCaption("正在加载数据..." + percent.ToString("P"));
            S10_4_1.SetSheet10_4_1Title(fpSpread1, fpSpread1_Sheet7, Title[6]);
            percent = (8 / SheetCount1);
            wait1.SetCaption("正在加载数据..." + percent.ToString("P"));
            S10_5.SetSheet10_5Title(fpSpread1, fpSpread1_Sheet8, Title[7]);
            percent = (9 / SheetCount1);
            wait1.SetCaption("正在加载数据..." + percent.ToString("P"));
            S10_6.SetSheet10_6Title(fpSpread1_Sheet9, Title[8]);
            percent = (10 / SheetCount1);
            wait1.SetCaption("正在加载数据..." + percent.ToString("P"));
            S10_7.SetSheet10_7Title(fpSpread1_Sheet10, Title[9]);
            percent = (11 / SheetCount1);
            wait1.SetCaption("正在加载数据..." + percent.ToString("P"));
            S10_8.SetSheet10_8Title(fpSpread1_Sheet11, Title[10]);
            percent = (12 / SheetCount1);
            wait1.SetCaption("正在加载数据..." + percent.ToString("P"));
            S10_9.SetSheet10_9Title(fpSpread1_Sheet12, Title[11]);
            percent = (13 / SheetCount1);
            wait1.SetCaption("正在加载数据..." + percent.ToString("P"));
            S10_10.SetSheet10_10Title(fpSpread1_Sheet13, Title[12], false);//手写
                this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                S10_11.AddBarEditItems(this.barEditItem1, this.barEditItem2, this);
                SheetNClear(fpSpread1.Sheets[13]);
                percent = (14 / SheetCount1);
                wait1.SetCaption("正在加载数据..." + percent.ToString("P"));
                S10_11.SetSheet10_11Title(fpSpread1_Sheet14, Title[13]);

            this.AddCellChanged();//响应单元格改变
            this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            PF.Sheet_AutoLineFeed(fpSpread1);//设置整个工作簿换行
        }
        /// <summary>
        /// 点击更新按钮用
        /// </summary>
        private void Updateing()
        {
            float SheetCount1 = SheetCount;
            float percent = 0;
            this.Text = " 铜陵市各电压等级配电网规划建设工程投资估算 ";

            InitTitle();
            //if (IsCreateSheet)
            //{
            //InitSheetTop();
            AddSheet();
            //}
            ////Sheet10_1(fpSpread1.Sheets[0], Title[0]);
            ////Sheet10_1_1(fpSpread1.Sheets[1], Title[1]);


            this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            SheetNClear(fpSpread1.Sheets[0]);
            percent = (1 / SheetCount1);
            wait1.SetCaption("正在更新数据..." + percent.ToString("P"));
            S10_1.SetSheet10_1Title(fpSpread1.Sheets[0], Title[0]);

            SheetNClear(fpSpread1.Sheets[1]);
            percent = (2 / SheetCount1);
            wait1.SetCaption("正在更新数据..." + percent.ToString("P"));
            S10_1_1.SetSheet10_1_1Title(fpSpread1.Sheets[1], Title[1]);
            SheetNClear(fpSpread1.Sheets[2]);
            percent = (3 / SheetCount1);
            wait1.SetCaption("正在更新数据..." + percent.ToString("P"));
            S10_2.SetSheet10_2Title(fpSpread1.Sheets[2], Title[2]);
            SheetNClear(fpSpread1.Sheets[3]);
            percent = (4 / SheetCount1);
            wait1.SetCaption("正在更新数据..." + percent.ToString("P"));
            S10_3.SetSheet10_3Title(fpSpread1.Sheets[3], Title[3]);
            SheetNClear(fpSpread1.Sheets[4]);
            percent = (5 / SheetCount1);
            wait1.SetCaption("正在更新数据..." + percent.ToString("P"));
            S10_3_1.SetSheet10_3_1Title(fpSpread1.Sheets[4], Title[4]);
            SheetNClear(fpSpread1.Sheets[5]);
            percent = (6 / SheetCount1);
            wait1.SetCaption("正在更新数据..." + percent.ToString("P"));
            S10_4.SetSheet10_4Title(fpSpread1, fpSpread1.Sheets[5], Title[5]);
            SheetNClear(fpSpread1.Sheets[6]);
            percent = (7 / SheetCount1);
            wait1.SetCaption("正在更新数据..." + percent.ToString("P"));
            S10_4_1.SetSheet10_4_1Title(fpSpread1, fpSpread1.Sheets[6], Title[6]);
            SheetNClear(fpSpread1.Sheets[7]);
            percent = (8 / SheetCount1);
            wait1.SetCaption("正在更新数据..." + percent.ToString("P"));
            S10_5.SetSheet10_5Title(fpSpread1, fpSpread1.Sheets[7], Title[7]);
            SheetNClear(fpSpread1.Sheets[8]);
            percent = (9 / SheetCount1);
            wait1.SetCaption("正在更新数据..." + percent.ToString("P"));
            S10_6.SetSheet10_6Title(fpSpread1.Sheets[8], Title[8]);
            SheetNClear(fpSpread1.Sheets[9]);
            percent = (10 / SheetCount1);
            wait1.SetCaption("正在更新数据..." + percent.ToString("P"));
            S10_7.SetSheet10_7Title(fpSpread1.Sheets[9], Title[9]);
            SheetNClear(fpSpread1.Sheets[10]);
            percent = (11 / SheetCount1);
            wait1.SetCaption("正在更新数据..." + percent.ToString("P"));
            S10_8.SetSheet10_8Title(fpSpread1.Sheets[10], Title[10]);
            SheetNClear(fpSpread1.Sheets[11]);
            percent = (12 / SheetCount1);
            wait1.SetCaption("正在更新数据..." + percent.ToString("P"));
            S10_9.SetSheet10_9Title(fpSpread1.Sheets[11], Title[11]);//手写
            SheetNClear(fpSpread1.Sheets[12]);
            percent = (13/ SheetCount1);
            wait1.SetCaption("正在更新数据..." + percent.ToString("P"));
            S10_10.SetSheet10_10Title(fpSpread1.Sheets[12], Title[12], true);//手写
            this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            S10_11.AddBarEditItems(this.barEditItem1, this.barEditItem2, this);
            SheetNClear(fpSpread1.Sheets[13]);
            percent = (14 / SheetCount1);
            wait1.SetCaption("正在更新数据..." + percent.ToString("P"));
            S10_11.SetSheet10_11Title(fpSpread1.Sheets[13], Title[13]);

            this.AddCellChanged();//响应单元格改变
            this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            PF.Sheet_AutoLineFeed(fpSpread1);//设置整个工作簿换行
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
        /// 工作簿所有表都加载单元格自动改变响应,
        /// </summary>
        private void AddCellChanged()
        {
            for (int i = 0; i < fpSpread1.Sheets.Count; ++i)
            {
                fpSpread1.Sheets[i].CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(FrmSheet10_1_CellChanged);
            }
        }
        /// <summary>
        /// 自动改变行高,这个方法太敏感导致行列值会发生不稳定错误所以现在不用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FrmSheet10_1_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
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
                if (ActiveSheet >= 0 && ActiveSheet < SheetCount)
                {
                    //PF.SetRowHeight(fpSpread1.Sheets[this.SheetIndex], e.Row, e.Column, fpSpread1.Sheets[this.SheetIndex].GetValue(e.Row, e.Column));
                }
            }
        }
        private void InitTitle()
        {
            #region 初始化标题
            for (int i = 0; i < SheetCount; ++i)
            {
                switch (i)
                {
                    case 0:
                        Title[i] = "表10‑1  铜陵市各电压等级配电网规划建设工程投资估算  （亿元）";
                        break;
                    case 1:
                        Title[i] = "附表53  铜陵市配电网建设改造工程投资估算";
                        break;
                    case 2:
                        Title[i] = "表10‑2  铜陵市各地市配电网规划建设工程投资估算";
                        break;
                    case 3:
                        Title[i] = "表10‑3  铜陵市配电网用户工程投资估算（亿元）";
                        break;
                    case 4:
                        Title[i] = "附表54  铜陵市配电网用户工程投资估算";
                        break;
                    case 5:
                        Title[i] = "表10‑4  铜陵市配电网全口径投资估算（亿元）";
                        break;
                    case 6:
                        Title[i] = "附表55  铜陵市配电网全口径投资估算";
                        break;
                    case 7:
                        Title[i] = "表10‑5  铜陵市配电网规划建设工程全口径投资估算";
                        break;
                    case 8:
                        Title[i] = "表10‑6 铜陵市高压配电网新扩建工程投资分类统计（亿元）";
                        break;
                    case 9:
                        Title[i] = "表10‑7 铜陵市中低压配电网新扩建工程投资分类统计（亿元）";
                        break;
                    case 10:
                        Title[i] = "表10‑8 铜陵市配电网改造工程投资分类统计（亿元）";
                        break;
                    case 11:
                        Title[i] = "表10‑9  铜陵市配电网规划建设资金筹措表";
                        break;
                    case 12:
                        Title[i] = "表10‑10  铜陵市配电网“十二五”规划投资经济效益分析（财务部、四县公司）";
                        break;
                    case 13:
                        Title[i] = "表10‑11  配电网“十二五”规划经济效益指标";
                        break;
                    default:
                        break;
                }
            }
            #endregion
        }
        /// <summary>
        /// 初始化sheetview
        /// </summary>
        //private void InitSheetTop()
        //{
        //    for (int i = 0; i < SheetCount; ++i)
        //    {
        //        //fpSheetN[i] = PF.CreateSheet(this, fpSpread1, Title[i]);
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

        }
        private void fpSpread1_SheetTabClick(object sender, FarPoint.Win.Spread.SheetTabClickEventArgs e)
        {
            if(e.SheetTabIndex==13)
            {
                this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always ;
            }
            else
            {
                this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }
        ///// <summary>
        ///// 加载表头10_1
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <param name="Title"></param>
        //private void Sheet10_1(FarPoint.Win.Spread.SheetView obj, string Title)
        //{
        //    S10_1.SetSheet10_1Title(obj, Title);
        //}
        ///// <summary>
        ///// 添加10_1的附表
        ///// </summary>
        //private void Sheet10_1_1(FarPoint.Win.Spread.SheetView obj, string Title)
        //{
        //    S10_1_1.SetSheet10_1_1Title(obj,Title);
        //}
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
             wait1 = new WaitDialogForm("", "正在更新数据, 请稍候...");
            ////由于SheetView已经没有了所以移除SheetView
            //MessageBox.Show(fpSpread1.Sheets[0].SheetName);
            //===================================================
            //由于更新数据时间太长需要建个空表做初始界面
            FarPoint.Win.Spread.SheetView obj = null;
            FarPoint.Win.Spread.SheetView fpSpread1_SheetN = new FarPoint.Win.Spread.SheetView();
            SheetNClear(fpSpread1_SheetN);
            fpSpread1.Sheets.Add(fpSpread1_SheetN);
            obj = fpSpread1.ActiveSheet;
            fpSpread1.ActiveSheet = fpSpread1_SheetN;
            S10_10.SaveData(fpSpread1.Sheets[12], 5, 2, 1, 6);
            this.fpSpread1.Sheets.Clear();
            //PF.CreateSheet(this, fpSpread1, "kk");
            //SheetIndex = fpSpread1.ActiveSheetIndex;//记住当前的SheetView的索引值
            //fpSpread1.ActiveSheet = fpSpread1.Sheets[fpSpread1.Sheets.Count - 1];//把当前界面给空表
            //PF.RemoveSheetView(this.fpSpread1, (fpSpread1.Sheets.Count - 1));//清空所有行列
            ////由于表10_10是手写所以不用更新，所以不用清除
            //for (int i = 0; i < fpSpread1.Sheets.Count - 1; ++i)
            //{
            //    if(i!=12)
            //    {
            //        fpSpread1.Sheets[i].RowCount = 0;
            //        fpSpread1.Sheets[i].ColumnCount = 0;
            //    }
            //}
            //this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            IsCreateSheet = false;
            Updateing();

            IsCreateSheet = true;
            fpSpread1.ActiveSheet = fpSpread1.Sheets[SheetIndex];//把以前显示的界面在显示出来
            //删除临时SheetView
            //fpSpread1.Sheets.Remove(fpSpread1.Sheets[fpSpread1.Sheets.Count - 1]);
            fpSpread1.Sheets.Remove(fpSpread1_SheetN);
            //if (SheetIndex == fpSpread1.Sheets.Count - 1)
            //{
            //    this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always ;
            //}
            //MessageBox.Show("更新数据完成！");
            wait1.Close();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PF.SaveExcel(this.fpSpread1, "Sheet10_1");
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
        private void AddData(FarPoint.Win.Spread.FpSpread obj)
        {
            try
            {
                wait1 = new WaitDialogForm("", "正在加载数据, 请稍候...");
                //打开Excel表格
                //清空工作表
                fpSpread1.Sheets.Clear();
                obj.OpenExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\Sheet10_1.xls");
                PF.SpreadRemoveEmptyCells(obj);
                this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.AddCellChanged();
                S10_11.AddBarEditItems(this.barEditItem1, this.barEditItem2, this);//添加下拉菜单内容
                wait1.Close();
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
                obj.SaveExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\Sheet10_1.xls", FarPoint.Excel.ExcelSaveFlags.NoFlagsSet);
                // 定义要使用的Excel 组件接口
                // 定义Application 对象,此对象表示整个Excel 程序
                Microsoft.Office.Interop.Excel.Application excelApp = null;
                // 定义Workbook对象,此对象代表工作薄
                Microsoft.Office.Interop.Excel.Workbook workBook;
                // 定义Worksheet 对象,此对象表示Execel 中的一张工作表
                Microsoft.Office.Interop.Excel.Worksheet ws = null;
                Microsoft.Office.Interop.Excel.Range range = null;
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                string filename = System.Windows.Forms.Application.StartupPath + "\\xls\\Sheet10_1.xls";
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
            wait1.Close();
        }

        private void barEditItem1_EditValueChanged(object sender, EventArgs e)
        {
            int id = 0;
            string temp = null;
            //传入选中的数据，连接数据库表
            id = ((DevExpress.XtraEditors.Repository.RepositoryItemComboBox)this.barEditItem1.Edit).Items.IndexOf(this.barEditItem1.EditValue);
            temp = (string)((DevExpress.XtraEditors.Repository.RepositoryItemComboBox)this.barEditItem2.Edit).Items[id];
            S10_11.SelectEditChange(this.fpSpread1.Sheets[13], temp, this);
        }
        

    }
}