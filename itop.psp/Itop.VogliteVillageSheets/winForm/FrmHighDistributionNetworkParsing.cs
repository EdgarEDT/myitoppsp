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

using Itop.Client.Base;
using Itop.JournalSheet.Function;

namespace Itop.VogliteVillageSheets.winForm
{
    public partial class FrmHighDistributionNetworkParsing : FormBase
    {
        private int SheetIndex = 0;//记录当前页的index
        private bool IsCreateSheet = true;
        private WaitDialogForm wait = null;
        private int FirstYear = 1990;
        private int EndYear = 2060;

        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet3 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet4 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet5 = new FarPoint.Win.Spread.SheetView();
        private Itop.JournalSheet.Function.PublicFunction PF = new Itop.JournalSheet.Function.PublicFunction();
        private Itop.VogliteVillageSheets.HighDistributionNetworkParsing.Sheet3 S_3 = new Itop.VogliteVillageSheets.HighDistributionNetworkParsing.Sheet3();
        private Itop.VogliteVillageSheets.HighDistributionNetworkParsing.Sheet4 S_4 = new Itop.VogliteVillageSheets.HighDistributionNetworkParsing.Sheet4();
        private Itop.VogliteVillageSheets.HighDistributionNetworkParsing.Sheet5 S_5 = new Itop.VogliteVillageSheets.HighDistributionNetworkParsing.Sheet5();

        public FrmHighDistributionNetworkParsing()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            S_3.SetSheet_3Title(this, fpSpread1_Sheet3, "附表3 截至2009年底铜陵县电源统计表");
            S_4.SetSheet_4Title(this, fpSpread1_Sheet4, "附表4 截至2009年底铜陵县110（35）kV变电站概况");
            S_5.SetSheet_5Title(this, fpSpread1_Sheet5, "附表5 截至2009年底铜陵县35kV及以上高压线路基本情况");
            AddSheet();
        }
        /// <summary>
        /// 添加工作簿
        /// </summary>
        private void AddSheet()
        {
            fpSpread1.Sheets.Add(fpSpread1_Sheet3);
            fpSpread1.Sheets.Add(fpSpread1_Sheet4);
            fpSpread1.Sheets.Add(fpSpread1_Sheet5);
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
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PF.SaveExcel(fpSpread1, "高压配电网分析");
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
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
                obj.OpenExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\高压配电网分析.xls");
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
                obj.SaveExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\高压配电网分析.xls", FarPoint.Excel.ExcelSaveFlags.NoFlagsSet);
                // 定义要使用的Excel 组件接口
                // 定义Application 对象,此对象表示整个Excel 程序
                Microsoft.Office.Interop.Excel.Application excelApp = null;
                // 定义Workbook对象,此对象代表工作薄
                Microsoft.Office.Interop.Excel.Workbook workBook;
                // 定义Worksheet 对象,此对象表示Execel 中的一张工作表
                Microsoft.Office.Interop.Excel.Worksheet ws = null;
                Microsoft.Office.Interop.Excel.Range range = null;
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                string filename = System.Windows.Forms.Application.StartupPath + "\\xls\\高压配电网分析.xls";
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

        private void FrmHighDistributionNetworkParsing_Load(object sender, EventArgs e)
        {
            InitControl();
            //LoadData();
            AddData(fpSpread1);
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            S_3.InitYear(this.barEditItem1);
        }
        /// <summary>
        /// bar改变值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEditItem1_EditValueChanged(object sender, EventArgs e)
        {
            S_3.SetColumnsTitle(this, fpSpread1.Sheets[0], FirstYear, EndYear, barEditItem1.EditValue.ToString());
            S_4.WriteData(this, fpSpread1.Sheets[1],barEditItem1.EditValue.ToString());
            S_5.WriteData(this, fpSpread1.Sheets[2], barEditItem1.EditValue.ToString());
        }
    }
}