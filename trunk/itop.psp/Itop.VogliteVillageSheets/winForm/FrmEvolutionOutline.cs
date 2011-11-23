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
    public partial class FrmEvolutionOutline : FormBase
    {
        private int SheetIndex = 0;//记录当前页的index
        private bool IsCreateSheet = true;
        private WaitDialogForm wait = null;
        private int FirstYear = 1990;
        private int EndYear = 2060;

        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet2 = new FarPoint.Win.Spread.SheetView();
        private Itop.JournalSheet.Function.PublicFunction PF = new Itop.JournalSheet.Function.PublicFunction();
        private Itop.VogliteVillageSheets.EvolutionOutlineFunction.Sheet1 S_1 = new Itop.VogliteVillageSheets.EvolutionOutlineFunction.Sheet1();
        private Itop.VogliteVillageSheets.EvolutionOutlineFunction.Sheet2 S_2 = new Itop.VogliteVillageSheets.EvolutionOutlineFunction.Sheet2();
        public FrmEvolutionOutline()
        {
            InitializeComponent();
        }

        private void FrmEvolutionOutline_Load(object sender, EventArgs e)
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
            S_1.InitYear(this.barEditItem1);
            S_1.InitYear(this.barEditItem2);
            this.barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }
        private void AddSheet()
        {
            fpSpread1.Sheets.Add(fpSpread1_Sheet1);
            fpSpread1.Sheets.Add(fpSpread1_Sheet2);
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            S_1.SetSheet_1Title(this, fpSpread1_Sheet1, "附表1 铜陵县2000～2009年国民生产总值GDP情况表");
            S_2.SetSheet_2Title(this, fpSpread1_Sheet2, "附表2 铜陵县分镇/片区2000～2009年人口状况表");
            AddSheet();
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
            //SheetIndex = fpSpread1.ActiveSheetIndex;//记住当前的SheetView的索引值
            //fpSpread1.ActiveSheet=fpSpread1.Sheets[fpSpread1.Sheets.Count-1];//把当前界面给空表
            PF.RemoveSheetView(this.fpSpread1, (fpSpread1.Sheets.Count - 1));//清空所有行列

            //this.fpSpread1.Sheets.Clear();
            IsCreateSheet = false;

            LoadData();

            //newwait.Close();
            IsCreateSheet = true;
            fpSpread1.ActiveSheet = fpSpread1.Sheets[SheetIndex];//把以前显示的界面在显示出来
            //if (SheetIndex == 2)
            //{
            //    this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //}
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
            PF.SaveExcel(fpSpread1, "铜陵县发展概况");
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
                obj.OpenExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\铜陵县发展概况.xls");
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
                obj.SaveExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\铜陵县发展概况.xls", FarPoint.Excel.ExcelSaveFlags.NoFlagsSet);
                // 定义要使用的Excel 组件接口
                // 定义Application 对象,此对象表示整个Excel 程序
                Microsoft.Office.Interop.Excel.Application excelApp = null;
                // 定义Workbook对象,此对象代表工作薄
                Microsoft.Office.Interop.Excel.Workbook workBook;
                // 定义Worksheet 对象,此对象表示Execel 中的一张工作表
                Microsoft.Office.Interop.Excel.Worksheet ws = null;
                Microsoft.Office.Interop.Excel.Range range = null;
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                string filename = System.Windows.Forms.Application.StartupPath + "\\xls\\铜陵县发展概况.xls";
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
        /// bar1改变值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEditItem1_EditValueChanged(object sender, EventArgs e)
        {
            int IntTemp=0;
            int.TryParse(this.barEditItem1.EditValue.ToString(),out  IntTemp);
            this.FirstYear = IntTemp;
        }
        /// <summary>
        /// bar2 值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEditItem2_EditValueChanged(object sender, EventArgs e)
        {
            int IntTemp = 0;
            int.TryParse(barEditItem2.EditValue.ToString(),out IntTemp);
            if(IntTemp>FirstYear)
            {
                EndYear = IntTemp;
                S_1.SetColumnsTitle(this, fpSpread1.Sheets[0], FirstYear, EndYear);
                S_2.SetColumnsTitle(this, fpSpread1.Sheets[1], FirstYear, EndYear);
            }
            else
            {
                MessageBox.Show("结束年份小于起始年份，请重新选择！！！");
            }
        }
    }
}