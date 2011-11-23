using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using System.Windows.Forms;
using Itop.Common;
using System.IO;
using System.Data;
using System.Reflection;
using System.Threading;
using FarPoint.Win.Spread;

namespace Itop.Client.Table
{
    public class ExportExcel
    {
        public static void ExportToExcelOld(GridControl gridControl,int row,string col,bool bHe)
        {
            //try
            //{
         //   Control.CheckForIllegalCrossThreadCalls = false;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                string fname = "";
                saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                     FrmGress frm = new FrmGress();
                  //   Thread thread = new Thread(new ThreadStart(frm.Show));
                  //   thread.Start();
                    
                    fname = saveFileDialog1.FileName;
                    try
                    {
                        gridControl.ExportToExcelOld(fname);
                        if (bHe)
                        {
                            FarPoint.Win.Spread.FpSpread fps = new FarPoint.Win.Spread.FpSpread();
                            fps.OpenExcel(fname);
                            SheetView sv = fps.Sheets[0];
                            for (int j = 0; j < sv.NonEmptyRowCount; j++)
                            {
                                for (int k = 0; k < sv.NonEmptyColumnCount; k++)
                                {
                                    sv.Cells[j, k].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                                }

                            }
                            fps.SaveExcel(fname);
                            // 定义要使用的Excel 组件接口
                            // 定义Application 对象,此对象表示整个Excel 程序
                            Microsoft.Office.Interop.Excel.Application excelApp = null;
                            // 定义Workbook对象,此对象代表工作薄
                            Microsoft.Office.Interop.Excel.Workbook workBook;
                            // 定义Worksheet 对象,此对象表示Execel 中的一张工作表
                            Microsoft.Office.Interop.Excel.Worksheet ws = null;
                            Microsoft.Office.Interop.Excel.Range range = null;
                            excelApp = new Microsoft.Office.Interop.Excel.Application();

                            workBook = excelApp.Workbooks.Open(fname, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                            for (int i = 1; i <= workBook.Worksheets.Count; i++)
                            {

                                ws = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Worksheets[i];
                                //取消保护工作表
                                ws.Unprotect(Missing.Value);
                                //有数据的行数
                                int row1 = ws.UsedRange.Rows.Count;
                                //有数据的列数
                                int col1 = ws.UsedRange.Columns.Count;
                                //创建一个区域
                                range = ws.get_Range(ws.Cells[1, 1], ws.Cells[row1, col1]);
                                //设区域内的单元格自动换行
                                range.Select();
                                range.NumberFormatLocal = "G/通用格式";

                                //保护工作表
                                ws.Protect(Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                            }
                            //保存工作簿
                            workBook.Save();
                            //关闭工作簿
                            excelApp.Workbooks.Close();
                            ChangeExcel(fname, row, col);
                            
                        }
                        else
                        {
                            FarPoint.Win.Spread.FpSpread fps = new FarPoint.Win.Spread.FpSpread();
                            fps.OpenExcel(fname);
                            SheetView sv = fps.Sheets[0];
                            for (int j = 0; j < sv.NonEmptyRowCount; j++)
                            {
                                for (int k = 0; k < sv.NonEmptyColumnCount; k++)
                                {
                                    sv.Cells[j, k].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                                }

                            }
                            fps.SaveExcel(fname);
                            // 定义要使用的Excel 组件接口
                            // 定义Application 对象,此对象表示整个Excel 程序
                            Microsoft.Office.Interop.Excel.Application excelApp = null;
                            // 定义Workbook对象,此对象代表工作薄
                            Microsoft.Office.Interop.Excel.Workbook workBook;
                            // 定义Worksheet 对象,此对象表示Execel 中的一张工作表
                            Microsoft.Office.Interop.Excel.Worksheet ws = null;
                            Microsoft.Office.Interop.Excel.Range range = null;
                            excelApp = new Microsoft.Office.Interop.Excel.Application();

                            workBook = excelApp.Workbooks.Open(fname, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                            for (int i = 1; i <= workBook.Worksheets.Count; i++)
                            {

                                ws = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Worksheets[i];
                                //取消保护工作表
                                ws.Unprotect(Missing.Value);
                                //有数据的行数
                                int row1 = ws.UsedRange.Rows.Count;
                                //有数据的列数
                                int col1 = ws.UsedRange.Columns.Count;
                                //创建一个区域
                                range = ws.get_Range(ws.Cells[1, 1], ws.Cells[row1, col1]);
                                //设区域内的单元格自动换行
                                range.Select();
                                range.NumberFormatLocal = "G/通用格式";

                                //保护工作表
                                ws.Protect(Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                            }
                            //保存工作簿
                            workBook.Save();
                            //关闭工作簿
                            excelApp.Workbooks.Close();
                            if (MsgBox.ShowYesNo("导出成功，是否打开该文档？") != DialogResult.Yes)
                                return;

                            System.Diagnostics.Process.Start(fname);
                        }
                    }
                    catch 
                    {
                        MsgBox.Show("无法保存"+fname+"。请用其他文件名保存文件，或将文件存至其他位置。");
                        return;
                    }
                }
        }

        public static void ExportToExcelOld(GridControl gridControl,string title,string dw)
        {
            //try
            //{
            //   Control.CheckForIllegalCrossThreadCalls = false;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string fname = "";
            saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FrmGress frm = new FrmGress();
                //   Thread thread = new Thread(new ThreadStart(frm.Show));
                //   thread.Start();

                fname = saveFileDialog1.FileName;
                try
                {
                    gridControl.ExportToExcelOld(fname);
                    CreateTitle(fname, title, dw);
                    FarPoint.Win.Spread.FpSpread fps = new FarPoint.Win.Spread.FpSpread();
                    fps.OpenExcel(fname);
                    SheetView sv = fps.Sheets[0];
                    for (int j = 0; j < sv.NonEmptyRowCount; j++)
                    {
                        for (int k = 0; k < sv.NonEmptyColumnCount; k++)
                        {
                            sv.Cells[j, k].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                        }

                    }
                    fps.SaveExcel(fname);
                    // 定义要使用的Excel 组件接口
                    // 定义Application 对象,此对象表示整个Excel 程序
                    Microsoft.Office.Interop.Excel.Application excelApp = null;
                    // 定义Workbook对象,此对象代表工作薄
                    Microsoft.Office.Interop.Excel.Workbook workBook;
                    // 定义Worksheet 对象,此对象表示Execel 中的一张工作表
                    Microsoft.Office.Interop.Excel.Worksheet ws = null;
                    Microsoft.Office.Interop.Excel.Range range = null;
                    excelApp = new Microsoft.Office.Interop.Excel.Application();

                    workBook = excelApp.Workbooks.Open(fname, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
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
                        range.Select();
                        range.NumberFormatLocal = "G/通用格式";

                        //保护工作表
                        ws.Protect(Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                    }
                    //保存工作簿
                    workBook.Save();
                    //关闭工作簿
                    excelApp.Workbooks.Close();
                        if (MsgBox.ShowYesNo("导出成功，是否打开该文档？") != DialogResult.Yes)
                            return;

                        System.Diagnostics.Process.Start(fname);
                    
                }
                catch
                {
                    MsgBox.Show("无法保存" + fname + "。请用其他文件名保存文件，或将文件存至其他位置。");
                    return;
                }
            }
        }
        /// <summary>
        /// 修改电子表格
        /// </summary>
        public static void ChangeExcel(string fileName,int row,string col)
        {
           
            try
            {
                //FrmGress frm = new FrmGress();
                //frm.Show();
              //  DevExpress.XtraEditors.ProgressBarControl gress = frm.progressBarControl1;
                //gress.Properties.Step = 5;
                //gress.PerformStep();
                object wrap = true;
                object addin = false;
                object Indent = 0;
                object Shrink = false;
                object Reading = true;
                object rows = row;
                object cols = col;
                Microsoft.Office.Interop.Excel.Application ep = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook wb = ep.Workbooks.Add(fileName);
                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;
                Microsoft.Office.Interop.Excel._Worksheet ws = (Microsoft.Office.Interop.Excel._Worksheet)sheets.get_Item(1);
               string first="";
               for (int x = row; x < 1000; x++)
               {
                   if (ws.get_Range(col + x.ToString(), Type.Missing).Value2 != null)
                   {
                       first = ws.get_Range(col + x.ToString(), Type.Missing).Value2.ToString();
                       break;
                   }
               }
                int start = 1;
                int end = 1;
                int temp = 1;
                string sec = "";
                string result = "";
                bool stop = false;
                for (int i = row; i < 1000; i++)
                {
                    stop = false;
                    if (ws.get_Range(col + Convert.ToString(i + 1), Type.Missing).Value2 != null)
                    {
                        sec = ws.get_Range(col + Convert.ToString(i + 1), Type.Missing).Value2.ToString();

                        if (first != sec)
                        {
                            result = ws.get_Range(col + Convert.ToString(i), Type.Missing).Value2.ToString();
                            first = ws.get_Range(col + Convert.ToString(i + 1), Type.Missing).Value2.ToString();
                            if (end == 1)
                                start = i + 1;
                            //end=1;
                            stop = true;
                        }
                        else
                        {
                            end++;
                            //ws.Cells[i + 1, col] = "";
                        }
                    }
                    else
                        stop = true;
                    if (stop && end > 1)
                    {
                        //gress.PerformStep();
                        end += start-1;
                        for (int k = start; k <= end; k++)
                            ws.Cells[k, col] = "";
                        Microsoft.Office.Interop.Excel.Range range = ws.get_Range(col + start.ToString(), col + end.ToString());

                        range.Merge(0); 
                      //  range.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                        range.VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                        range.WrapText = wrap;
                        range.AddIndent = addin;
                        range.IndentLevel = Indent;
                        range.ShrinkToFit = Shrink;
                        //range.ReadingOrder = Microsoft.Office.Interop.Excel.Constants.xlContext;
                        range.MergeCells = Reading;
                        range.Value2 = result;
                        end = 1;
                        start = i + 1;
                        if (ws.get_Range(col + Convert.ToString(i + 1), Type.Missing).Value2 == null)
                        {
                            range.Value2 = sec;
                           // gress.Properties.Step = 100;
                           // gress.PerformStep();
                            break;
                        }
                    }
                }
                ws.Activate();
                wb.Save();
              //  frm.Close();
                //ep.Quit();
                ep.Visible = true;

            }
            catch (Exception e) { MessageBox.Show(e.Message);  }
        }

        private static void CreateTitle(string fname,string title,string dw)
        {
            FarPoint.Win.Spread.FpSpread fps = new FarPoint.Win.Spread.FpSpread();
            fps.OpenExcel(fname);
            SheetView sv = fps.Sheets[0];

            int ColumnCount = sv.NonEmptyColumnCount;
            int RowCount = sv.NonEmptyRowCount;


            //sv.ColumnCount = ColumnCount;
            //sv.RowCount = RowCount;

            sv.AddRows(0, 2);
            sv.Cells[0, 0].Text = title;
            sv.Cells[0, 0].Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            sv.Cells[0, 0].HorizontalAlignment = CellHorizontalAlignment.Center;
            sv.Cells[0, 0].VerticalAlignment = CellVerticalAlignment.Center;
            sv.Cells[0, 0].Row.Height = 50;
            sv.Cells[0, 0].ColumnSpan = ColumnCount;


            sv.Cells[1, 0].Text = dw;
            sv.Cells[1, 0].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            sv.Cells[1, 0].HorizontalAlignment = CellHorizontalAlignment.Right;
            sv.Cells[1, 0].VerticalAlignment = CellVerticalAlignment.Center;
            sv.Cells[1, 0].ColumnSpan = ColumnCount;

            for (int i = 0; i < ColumnCount; i++)
            {
                sv.Cells[2, i].Row.Height = 40;
                sv.Cells[2, i].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }

            sv.AddRows(RowCount + 2, 2);
            sv.Cells[RowCount + 2, 0].Text = "建表时间:" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            sv.Cells[RowCount + 2, 0].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            sv.Cells[RowCount + 2, 0].HorizontalAlignment = CellHorizontalAlignment.Right;
            sv.Cells[RowCount + 2, 0].VerticalAlignment = CellVerticalAlignment.Center;
            sv.Cells[RowCount + 2, 0].ColumnSpan = ColumnCount;
            fps.SaveExcel(fname);
        }

    }
}
