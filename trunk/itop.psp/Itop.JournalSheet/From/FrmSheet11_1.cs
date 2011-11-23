using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

using Itop.Client.Base;
using DevExpress.Utils;


namespace Itop.JournalSheet.From
{
    public partial class FrmSheet11_1 : FormBase
    {
        private const int SheetCount=2;//。
        private bool IsCreateSheet = true;
        private string[] strTitle = new string[SheetCount];
        private FarPoint.Win.Spread.SheetView[] fpSheetN = new FarPoint.Win.Spread.SheetView[SheetCount];
        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function11.Sheet11_1 S11_1 = new Function11.Sheet11_1();
        private Function11.Sheet11_2 S11_2 = new Function11.Sheet11_2();

        public FrmSheet11_1()
        {
            InitializeComponent();
        }

        private void FrmSheet11_1_Load(object sender, EventArgs e)
        {
            AddData(fpSpread1);

        }
        private void LoadData()
        {
            this.Text = " 表11‑1 铜陵市配电网整体规划评价 ";
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            InitTitle();
            if (IsCreateSheet)//点击更新按钮
            {
                InitSheetTop();
            }
            S11_1.SetSheet11_1Title(fpSpread1.Sheets[0], strTitle[0]);
            if(IsCreateSheet)
            {
                S11_2.SetSheet11_2Title(fpSpread1, fpSpread1.Sheets[1], strTitle[1], false);
            }
            else//点击更新按钮
            {
                //先保存以前的数据
                S11_2.SaveData(fpSpread1.Sheets[1], 5, 4, 2, 12);
                //把界面清除
                fpSpread1.Sheets[1].RowCount = 0;//清空所有行列
                fpSpread1.Sheets[1].ColumnCount = 0;
                S11_2.SetSheet11_2Title(fpSpread1, fpSpread1.Sheets[1], strTitle[1], true);
            }

            this.AddCellChanged();//响应单元格改变
            PF.Sheet_AutoLineFeed(fpSpread1);//设置整个工作簿换行
        }
        /// <summary>
        /// 工作簿所有表都加载单元格自动改变响应
        /// </summary>
        private void AddCellChanged()
        {
            for (int i = 0; i < fpSpread1.Sheets.Count; ++i)
            {
                fpSpread1.Sheets[i].CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(FrmSheet11_1_CellChanged);
            }
        }
        /// <summary>
        /// 自动改变行高，与更新数据冲突现在不用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FrmSheet11_1_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
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
        private void InitTitle()
        {
            for (int i = 0; i < SheetCount; ++i)
            {
                if (i == 0)
                {
                    strTitle[i] = "表11‑1 铜陵市配电网整体规划评价";
                }
                if (i == 1)
                {
                    strTitle[i] = "附表56  铜陵市配电网规划成效部分指标分析";
                }
            }
        }
        private void InitSheetTop()
        {
            for (int i = 0; i < SheetCount; ++i)
            {
                //if(i==0)
                //{
                //    strTitle[i] = "表11‑1 铜陵市配电网整体规划评价";
                //}
                //if(i==1)
                //{
                //    strTitle[i] = "附表56  铜陵市配电网规划成效部分指标分析";
                //}
                fpSheetN[i] = PF.CreateSheet(this, fpSpread1, strTitle[i]);
            }
        }
        private void AddData(FarPoint.Win.Spread.FpSpread obj)
        {
            WaitDialogForm wait = null;
            wait = new WaitDialogForm("", "正在加载数据, 请稍候...");
            try
            {
                //打开Excel表格
                //清空工作表
                fpSpread1.Sheets.Clear();
                obj.OpenExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\Sheet11_1.xls");
                PF.SpreadRemoveEmptyCells(obj);
                this.AddCellChanged();
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
                obj.SaveExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\Sheet11_1.xls", FarPoint.Excel.ExcelSaveFlags.NoFlagsSet);
                // 定义要使用的Excel 组件接口
                // 定义Application 对象,此对象表示整个Excel 程序
                Microsoft.Office.Interop.Excel.Application excelApp = null;
                // 定义Workbook对象,此对象代表工作薄
                Microsoft.Office.Interop.Excel.Workbook workBook;
                // 定义Worksheet 对象,此对象表示Execel 中的一张工作表
                Microsoft.Office.Interop.Excel.Worksheet ws = null;
                Microsoft.Office.Interop.Excel.Range range = null;
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                string filename = System.Windows.Forms.Application.StartupPath + "\\xls\\Sheet11_1.xls";
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
            PF.SaveExcel(this.fpSpread1, "Sheet11_1");
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm newwait = new WaitDialogForm("", "正在更新数据, 请稍候...");
            ////由于SheetView已经没有了所以移除SheetView
            int SheetIndex = 0;
            //MessageBox.Show(fpSpread1.Sheets[0].SheetName);
            //===================================================
            //由于更新数据时间太长需要建个空表做初始界面
            //FarPoint.Win.Spread.SheetView fpSpread1_SheetN = new FarPoint.Win.Spread.SheetView();
            PF.CreateSheet(this, fpSpread1, "kk");
            SheetIndex = fpSpread1.ActiveSheetIndex;//记住当前的SheetView的索引值
            fpSpread1.ActiveSheet = fpSpread1.Sheets[fpSpread1.Sheets.Count - 1];//把当前界面给空表
            //PF.RemoveSheetView(this.fpSpread1, (fpSpread1.Sheets.Count - 1));//清空所有行列

            //由于是手写所以不加载第一个表只加载第二个表，第二个表是行动态的表还要判断有没有新加载的数据
            IsCreateSheet = false;
            LoadData();

            newwait.Close();
            IsCreateSheet = true;
            fpSpread1.ActiveSheet = fpSpread1.Sheets[SheetIndex];//把以前显示的界面在显示出来
            //删除临时SheetView
            fpSpread1.Sheets.Remove(fpSpread1.Sheets[fpSpread1.Sheets.Count - 1]);
            MessageBox.Show("更新数据完成！");
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
    }
}