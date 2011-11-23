using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using System.IO;
using System.Reflection;
using System.Threading;

using Itop.Client.Base;
using Itop.JournalSheet.Function;

namespace Itop.VogliteVillageSheets.winForm
{
    public partial class FrmDemandForecast : FormBase
    {
        private int SheetIndex = 0;//记录当前页的index
        private bool IsCreateSheet = true;
        private WaitDialogForm wait = null;
        private int FirstYear = 1990;
        private int EndYear = 2060;
        private const int SheetN = 7;
        //定义个委托
        private  delegate void SelectEditChange1(FarPoint.Win.Spread.SheetView tempSheet, object obj, Itop.Client.Base.FormBase FB);
        private SelectEditChange1 sec=null;
        private FarPoint.Win.Spread.SheetView[] fpSpread1_SheetN = new FarPoint.Win.Spread.SheetView[SheetN];
        private Itop.JournalSheet.Function.PublicFunction PF = new Itop.JournalSheet.Function.PublicFunction();
        private Itop.VogliteVillageSheets.DemandForecast.Sheet14 S_14 = new Itop.VogliteVillageSheets.DemandForecast.Sheet14();
        private Itop.VogliteVillageSheets.DemandForecast.Sheet15 S_15 = new Itop.VogliteVillageSheets.DemandForecast.Sheet15();
        private Itop.VogliteVillageSheets.DemandForecast.Sheet16 S_16 = new Itop.VogliteVillageSheets.DemandForecast.Sheet16();
        private Itop.VogliteVillageSheets.DemandForecast.Sheet17 S_17 = new Itop.VogliteVillageSheets.DemandForecast.Sheet17();
        private Itop.VogliteVillageSheets.DemandForecast.Sheet18 S_18 = new Itop.VogliteVillageSheets.DemandForecast.Sheet18();
        private Itop.VogliteVillageSheets.DemandForecast.Sheet19 S_19 = new Itop.VogliteVillageSheets.DemandForecast.Sheet19();
        private Itop.VogliteVillageSheets.DemandForecast.Sheet20 S_20 = new Itop.VogliteVillageSheets.DemandForecast.Sheet20();

        public FrmDemandForecast()
        {
            InitializeComponent();
        }

        private void FrmDemandForecast_Load(object sender, EventArgs e)
        {
            //LoadData();
            AddData(fpSpread1);
            InitControl();
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            S_14.InitYear(this.barEditItem1);
            S_14.InitYear(this.barEditItem2);
            S_19.AddBarEditItems(this.barEditItem4, this.barEditItem3, this);

            if(fpSpread1.ActiveSheetIndex ==5 ||fpSpread1.ActiveSheetIndex==3)
            {
                this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.barStaticItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.barStaticItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.barEditItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else if(fpSpread1.ActiveSheetIndex==0)
            {
                this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.barStaticItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.barStaticItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.barEditItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else
            {
                this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.barStaticItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.barStaticItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.barEditItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            AddSheets();
            S_14.SetSheet_14Title(this, fpSpread1_SheetN[0], "附表14截至2009年底铜陵县负荷历史实绩统计表");
            S_15.SetSheet_15Title(this, fpSpread1_SheetN[1], "铜陵县分行业用电历史实绩统计表");
            S_16.SetSheet_16Title(this, fpSpread1_SheetN[2], "附表16 铜陵县历年用电量及负荷分区统计");
            //预测
            S_17.SetSheet_17Title(this, fpSpread1_SheetN[3], "附表17 规划年铜陵县经济发展预测结果表");
           
            S_18.SetSheet_18Title(this, fpSpread1_SheetN[4], "附表18 规划年铜陵县大用户统计信息表");
            //预测
            S_19.SetSheet_19Title(this, fpSpread1_SheetN[5], "2010-2020年铜陵县县负荷预测表");

            S_20.SetSheet_20Title(this, fpSpread1_SheetN[6], "附表20 规划年铜陵县规划装机进度表");

        }
        private void AddSheets()
        {
            for(int i=0;i<SheetN ;++i)
            {
                fpSpread1_SheetN[i] = new FarPoint.Win.Spread.SheetView();
                fpSpread1.Sheets.Add(fpSpread1_SheetN[i]);
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PF.ToExcel(fpSpread1);
        }
        /// <summary>
        /// 更新
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
            SheetIndex = fpSpread1.ActiveSheetIndex;//记住当前的SheetView的索引值
            //fpSpread1.ActiveSheet=fpSpread1.Sheets[fpSpread1.Sheets.Count-1];//把当前界面给空表
            //PF.RemoveSheetView(this.fpSpread1, (fpSpread1.Sheets.Count - 1));//清空所有行列

            //this.fpSpread1.Sheets.Clear();
            IsCreateSheet = false;
            fpSpread1.Sheets[1].RowCount = 0;
            fpSpread1.Sheets[1].ColumnCount = 0;
            S_15.SetSheet_15Title(this, fpSpread1.Sheets[1], "铜陵县分行业用电历史实绩统计表");
            fpSpread1.Sheets[2].RowCount = 0;
            fpSpread1.Sheets[2].ColumnCount = 0;
            S_16.SetSheet_16Title(this, fpSpread1.Sheets[2], "附表16 铜陵县历年用电量及负荷分区统计");
            //fpSpread1.Sheets[3].RowCount = 0;
            //fpSpread1.Sheets[3].ColumnCount = 0;
            //S_17.SetSheet_17Title(this, fpSpread1.Sheets[3], "附表17 规划年铜陵县经济发展预测结果表");
            fpSpread1.Sheets[4].RowCount = 0;
            fpSpread1.Sheets[4].ColumnCount = 0;
            S_18.SetSheet_18Title(this, fpSpread1.Sheets[4], "附表18 规划年铜陵县大用户统计信息表");
            //fpSpread1.Sheets[5].RowCount = 0;
            //fpSpread1.Sheets[5].ColumnCount = 0;
            //S_19.SetSheet_19Title(this, fpSpread1.Sheets[5], "2010-2020年铜陵县县负荷预测表");
            fpSpread1.Sheets[6].RowCount = 0;
            fpSpread1.Sheets[6].ColumnCount = 0;
            S_20.SetSheet_20Title(this, fpSpread1.Sheets[6], "附表20 规划年铜陵县规划装机进度表");

            //newwait.Close();
            IsCreateSheet = true;
            fpSpread1.ActiveSheet = obj;//fpSpread1.Sheets[SheetIndex];//把以前显示的界面在显示出来
            //if (obj.SheetName == "附表10 铜陵县中压线路“N-1”分析" || obj.SheetName == "附表11 铜陵县110kV及35kV变电站主变故障或检修“N-1”通过情况")
            //{
            //    this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //}
            //删除临时SheetView
            //fpSpread1.Sheets.Remove(fpSpread1.Sheets[fpSpread1.Sheets.Count - 1]);
            fpSpread1.Sheets.Remove(fpSpread1_SheetN);
            wait.Close();
            MessageBox.Show("更新数据完成！");
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PF.SaveExcel(fpSpread1, "电力需求预测和电源规划");
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
        /// <summary>
        /// 点击工作簿
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_SheetTabClick(object sender, FarPoint.Win.Spread.SheetTabClickEventArgs e)
        {
            if(e.SheetTabIndex==0)
            {
                this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.barStaticItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.barStaticItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.barEditItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else if (e.SheetTabIndex == 3)
            {
                this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.barStaticItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.barStaticItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.barEditItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else if (e.SheetTabIndex == 5)
            {
                this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.barStaticItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.barStaticItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.barEditItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                this.barEditItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.barStaticItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.barStaticItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }
        /// <summary>
        /// bar1改变值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEditItem1_EditValueChanged(object sender, EventArgs e)
        {
            int IntTemp = 0;
            int.TryParse(this.barEditItem1.EditValue.ToString(), out  IntTemp);
            this.FirstYear = IntTemp;
        }
        /// <summary>
        /// bar2改变值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEditItem2_EditValueChanged(object sender, EventArgs e)
        {
            int IntTemp = 0;
            int.TryParse(barEditItem2.EditValue.ToString(), out IntTemp);
            if (IntTemp > FirstYear)
            {
                EndYear = IntTemp;
                if(fpSpread1.ActiveSheetIndex ==0)
                {
                    S_14.SetColumnsTitle(this, fpSpread1.Sheets[0], FirstYear, EndYear);
                    //MessageBox.Show("0正确");
                }
                //if(fpSpread1.ActiveSheetIndex==1)
                //{
                //    S_15.SetColumnsTitle(this, fpSpread1.Sheets[1], FirstYear, EndYear);
                //}
                //if(fpSpread1.ActiveSheetIndex==5)
                //{
                //    S_19.SetColumnsTitle(this, fpSpread1.Sheets[5], FirstYear, EndYear);
                //}
            }
            else
            {
                MessageBox.Show("结束年份小于起始年份，请重新选择！！！");
            }
        }

        /// <summary>
        /// 加载数据同时保存数据到指定位置
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
                obj.OpenExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\电力需求预测和电源规划.xls");
                PF.SpreadRemoveEmptyCells(obj);
                //this.AddCellChanged();
                //this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                //S4_2_1.AddBarEditItems(this.barEditItem2, this.barEditItem1, this);
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
                obj.SaveExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\电力需求预测和电源规划.xls", FarPoint.Excel.ExcelSaveFlags.NoFlagsSet);
                // 定义要使用的Excel 组件接口
                // 定义Application 对象,此对象表示整个Excel 程序
                Microsoft.Office.Interop.Excel.Application excelApp = null;
                // 定义Workbook对象,此对象代表工作薄
                Microsoft.Office.Interop.Excel.Workbook workBook;
                // 定义Worksheet 对象,此对象表示Execel 中的一张工作表
                Microsoft.Office.Interop.Excel.Worksheet ws = null;
                Microsoft.Office.Interop.Excel.Range range = null;
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                string filename = System.Windows.Forms.Application.StartupPath + "\\xls\\电力需求预测和电源规划.xls";
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
        /// bar3改变值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEditItem4_EditValueChanged(object sender, EventArgs e)
        {
            int id = 0;
            string temp = null;
            //传入选中的数据，连接数据库表
            id = ((DevExpress.XtraEditors.Repository.RepositoryItemComboBox)this.barEditItem4.Edit).Items.IndexOf(this.barEditItem4.EditValue);
            temp = (string)((DevExpress.XtraEditors.Repository.RepositoryItemComboBox)this.barEditItem3.Edit).Items[id];
            if (fpSpread1.ActiveSheetIndex == 5)
            {
                wait = new WaitDialogForm("", "正在加载数据, 请稍候...");
                //Thread thread = new Thread(new ThreadStart(wait.Show));
                //sec = new SelectEditChange1(S_19.SelectEditChange);
                ////Thread thread1 = new Thread(new ThreadStart(new SelectEditChange1(S_19.SelectEditChange)));
                //thread.Start();
                S_19.SelectEditChange(this.fpSpread1.Sheets[5], temp, this);
                wait.Close();
                //thread.Abort();
            }
            if(fpSpread1.ActiveSheetIndex==3)
            {
                S_17.SelectEditChange(this.fpSpread1.Sheets[3], temp, this);
            }
        }
    }
}