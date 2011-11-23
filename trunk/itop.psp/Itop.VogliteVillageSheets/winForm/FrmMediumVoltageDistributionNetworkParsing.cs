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
    public partial class FrmMediumVoltageDistributionNetworkParsing : FormBase
    {
        private int SheetIndex = 0;//记录当前页的index
        private bool IsCreateSheet = true;
        private WaitDialogForm wait = null;
        private int FirstYear = 1990;
        private int EndYear = 2060;
        private const int SheetN = 8;

        private FarPoint.Win.Spread.SheetView[] fpSpread1_SheetN = new FarPoint.Win.Spread.SheetView[SheetN];
        private Itop.JournalSheet.Function.PublicFunction PF = new Itop.JournalSheet.Function.PublicFunction();
        private Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing.Sheet6 S_6 = new Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing.Sheet6();
        private Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing.Sheet7 S_7 = new Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing.Sheet7();
        private Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing.Sheet8 S_8 = new Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing.Sheet8();
        private Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing.Sheet9 S_9 = new Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing.Sheet9();
        private Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing.Sheet10 S_10 = new Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing.Sheet10();
        private Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing.Sheet11 S_11 = new Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing.Sheet11();
        private Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing.Sheet12 S_12 = new Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing.Sheet12();
        private Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing.Sheet13 S_13 = new Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing.Sheet13();

        public FrmMediumVoltageDistributionNetworkParsing()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            S_6.InitYear(this.barEditItem1);
        }

        private void FrmMediumVoltageDistributionNetworkParsing_Load(object sender, EventArgs e)
        {
            InitControl();
            //LoadData();
            AddData(fpSpread1);
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            AddSheets();
            S_6.SetSheet_6Title(this, fpSpread1_SheetN[0], "附表6 截至2009年底铜陵县10kV线路基本情况");
            S_7.SetSheet_7Title(this, fpSpread1_SheetN[1], "附表7 截至2009年底铜陵县中压线路联络情况");
            S_8.SetSheet_8Title(this, fpSpread1_SheetN[2], "截至2009年底铜陵县35kV变电站联络情况");
            S_9.SetSheet_9Title(this, fpSpread1_SheetN[3], "附表9 截至2009年底铜陵县中压配电线路运行情况");
            S_10.SetSheet_10Title(this, fpSpread1_SheetN[4], "附表10 铜陵县中压线路“N-1”分析");
            S_11.SetSheet_11Title(this, fpSpread1_SheetN[5], "附表11 铜陵县110kV及35kV变电站主变故障或检修“N-1”通过情况");
            S_12.SetSheet_12Title(this, fpSpread1_SheetN[6], "附表12 截至2009年底铜陵县电网无功补偿容量统计表");
            S_13.SetSheet_13Title(this, fpSpread1_SheetN[7], "附表13 截至2009年底铜陵县配网基本情况统计");

        }
        /// <summary>
        /// 添加工作簿
        /// </summary>
        private void AddSheets()
        {
            for(int i=0;i<SheetN;++i)
            {
                fpSpread1_SheetN[i] = new FarPoint.Win.Spread.SheetView();
                fpSpread1.Sheets.Add( fpSpread1_SheetN[i]);
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
            SheetIndex = fpSpread1.ActiveSheetIndex;//记住当前的SheetView的索引值
            //fpSpread1.ActiveSheet=fpSpread1.Sheets[fpSpread1.Sheets.Count-1];//把当前界面给空表
            //PF.RemoveSheetView(this.fpSpread1, (fpSpread1.Sheets.Count - 1));//清空所有行列

            //this.fpSpread1.Sheets.Clear();
            IsCreateSheet = false;
            fpSpread1.Sheets[4].RowCount = 0;
            fpSpread1.Sheets[4].ColumnCount = 0;
            S_10.SetSheet_10Title(this, fpSpread1.Sheets[4], "附表10 铜陵县中压线路“N-1”分析");
            fpSpread1.Sheets[5].RowCount = 0;
            fpSpread1.Sheets[5].ColumnCount = 0;
            S_11.SetSheet_11Title(this, fpSpread1.Sheets[5], "附表11 铜陵县110kV及35kV变电站主变故障或检修“N-1”通过情况");

            //newwait.Close();
            IsCreateSheet = true;
            fpSpread1.ActiveSheet = obj;//fpSpread1.Sheets[SheetIndex];//把以前显示的界面在显示出来
            if (obj.SheetName == "附表10 铜陵县中压线路“N-1”分析" || obj.SheetName == "附表11 铜陵县110kV及35kV变电站主变故障或检修“N-1”通过情况")
            {
                this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            //删除临时SheetView
            //fpSpread1.Sheets.Remove(fpSpread1.Sheets[fpSpread1.Sheets.Count - 1]);
            fpSpread1.Sheets.Remove(fpSpread1_SheetN);
            //MessageBox.Show("更新数据完成！");
            wait.Close();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PF.SaveExcel(fpSpread1, "中压配电网分析");
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
        /// 改变bar值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEditItem1_EditValueChanged(object sender, EventArgs e)
        {
            S_6.SetColumnsTitle(this, fpSpread1.Sheets[0], barEditItem1.EditValue.ToString());
            S_7.SetColumnsTitle(this, fpSpread1.Sheets[1], barEditItem1.EditValue.ToString());
            S_8.SetColumnsTitle(this, fpSpread1.Sheets[2], barEditItem1.EditValue.ToString());
            S_9.SetColumnsTitle(this, fpSpread1.Sheets[3], barEditItem1.EditValue.ToString());
            S_12.SetColumnsTitle(this, fpSpread1.Sheets[6], barEditItem1.EditValue.ToString());
            S_13.SetColumnsTitle(this, fpSpread1.Sheets[7], barEditItem1.EditValue.ToString());
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
                obj.OpenExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\中压配电网分析.xls");
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
                obj.SaveExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\中压配电网分析.xls", FarPoint.Excel.ExcelSaveFlags.NoFlagsSet);
                // 定义要使用的Excel 组件接口
                // 定义Application 对象,此对象表示整个Excel 程序
                Microsoft.Office.Interop.Excel.Application excelApp = null;
                // 定义Workbook对象,此对象代表工作薄
                Microsoft.Office.Interop.Excel.Workbook workBook;
                // 定义Worksheet 对象,此对象表示Execel 中的一张工作表
                Microsoft.Office.Interop.Excel.Worksheet ws = null;
                Microsoft.Office.Interop.Excel.Range range = null;
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                string filename = System.Windows.Forms.Application.StartupPath + "\\xls\\中压配电网分析.xls";
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

        private void fpSpread1_TabIndexChanged(object sender, EventArgs e)
        {
        }

        private void fpSpread1_SheetTabClick(object sender, FarPoint.Win.Spread.SheetTabClickEventArgs e)
        {
            if (e.SheetTabIndex==4||e.SheetTabIndex==5)
            {
                this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else
            {
                this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
        }
    }
}