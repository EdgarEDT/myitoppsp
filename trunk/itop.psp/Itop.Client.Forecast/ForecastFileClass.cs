using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using System.Windows.Forms;
using Itop.Common;
using System.IO;
using System.Data;
using System.Reflection;
using FarPoint.Win.Spread;

namespace Itop.Client.Forecast
{
    public class ForecastFileClass
    {
        public static void ExportExcel(GridControl gridControl)
        {
            //try
            //{

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                string fname = "";
                saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fname = saveFileDialog1.FileName;
                    //File.SetAttributes(fname, File.GetAttributes(fname) | FileAttributes.ReadOnly);

                    ////Create   the   file.   
                    //using (FileStream fs = new FileStream(fname, FileMode.OpenOrCreate, FileAccess.Read))
                    //{
                    //    if (!fs.CanWrite)
                    //    {
                    //        MsgBox.Show("文件不能被创建，请检查文件是否被打开");
                    //        return;
                    //    }
                    //}  



                    try
                    {
                        gridControl.ExportToExcelOld(fname);

                        if (MsgBox.ShowYesNo("导出成功，是否打开该文档？") != DialogResult.Yes)
                            return;
                    
                        System.Diagnostics.Process.Start(fname);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                        MsgBox.Show("无法保存"+fname+"。请用其他文件名保存文件，或将文件存至其他位置。");
                        return;
                    }
                }

                
                //return true;
            //}
            //catch { }
        }
        /// <summary>
        /// 导出到电子表格
        /// </summary>
        /// <param name="datatable"> 数据源datatable</param>
        /// <param name="charttitle">图表标题</param>
        /// <param name="tableflag">图标下方是否带有数据表，是true，否false</param>
        /// <param name="bl">图表是否带有图例，是true，否false</param>
        public static void ExportExcel( System.Data.DataTable datatable, string charttitle ,bool tableflag ,bool bl)
        {
            //try
            //{
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string fname = "";
            saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fname = saveFileDialog1.FileName;
                try
                {

                    //gridControl.ExportToExcelOld(fname);
                    exportexcel( datatable, fname,charttitle,tableflag,bl);
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


            //return true;
            //}
            //catch { }
        }

        private static void exportexcel( System.Data.DataTable dt, string filename,string charttitle ,bool tableflag ,bool bl)
        {
            //System.Data.DataTable dt = new System.Data.DataTable();

            if (dt == null) return;

            Microsoft.Office.Interop.Excel._Workbook oWB;
            Microsoft.Office.Interop.Excel.Series oSeries;
            //Microsoft.Office.Interop.Excel.Range oResizeRange;
            Microsoft.Office.Interop.Excel._Chart oChart;
            //String sMsg;
            //int iNumQtrs;
            GC.Collect();//系统的垃圾回收

            //string filename = @"C:\Documents and Settings\tongxl\桌面\nnn.xls";
            //Microsoft.Office.Interop.Excel.Application ep = new Microsoft.Office.Interop.Excel.Application();
            //Microsoft.Office.Interop.Excel._Workbook wb = ep.Workbooks.Add(filename);

            Microsoft.Office.Interop.Excel.Application ep = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook wb = ep.Workbooks.Add(true);


            if (ep == null)
            {
                MsgBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                return;
            }


            ep.Visible = true;
            Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;
            Microsoft.Office.Interop.Excel._Worksheet ws = (Microsoft.Office.Interop.Excel._Worksheet)sheets.get_Item(1);// [System.Type.Missing];//.get.get_Item("xx");
            ws.UsedRange.Select();
            ws.UsedRange.Copy(System.Type.Missing);
            // wb.Charts.Add(System.Type.Missing, System.Type.Missing, 1, System.Type.Missing);
            int rowIndex = 1;
            int colIndex = 1;
            foreach (DataColumn col in dt.Columns)
            {
                ws.Cells[rowIndex, colIndex++] = col.ColumnName;
            }

            for (int drvIndex = 0; drvIndex < dt.Rows.Count; drvIndex++)
            {
                DataRow row = dt.Rows[drvIndex];
                colIndex = 1;
                foreach (DataColumn col in dt.Columns)
                {
                    ws.Cells[drvIndex + 2, colIndex] = row[col.ColumnName].ToString();
                    colIndex++;
                }
            }


            oWB = (Microsoft.Office.Interop.Excel._Workbook)ws.Parent;
            oChart = (Microsoft.Office.Interop.Excel._Chart)oWB.Charts.Add(Missing.Value, Missing.Value,
                Missing.Value, Missing.Value);

            int rcount = dt.Rows.Count;
            int ccount = dt.Columns.Count;
            oChart.ChartWizard(ws.get_Range(ws.Cells[1, 1], ws.Cells[rcount + 2, ccount + 2]), Microsoft.Office.Interop.Excel.XlChartType.xlLine, Missing.Value,
         Microsoft.Office.Interop.Excel.XlRowCol.xlRows, 1, true, true,
         charttitle, Missing.Value, Missing.Value, Missing.Value);
            // oSeries = (Microsoft.Office.Interop.Excel.Series)oChart.SeriesCollection(1);
            
            //string str = String.Empty;
            //for (int I = 1; I < 15; I++)

            //{
            //    str += I.ToString() + "\t";  
            //}
            //try { oSeries.XValues = str; }
            //catch { }
            //  oSeries.HasDataLabels = true;
          //   string charttitle ,bool tableflag ,bool bl)
            if (tableflag==true)
                oChart.HasDataTable = true;
            else
                oChart.HasDataTable = false;
            oChart.PlotVisibleOnly = false;
            // 



            Microsoft.Office.Interop.Excel.Axis axis = (Microsoft.Office.Interop.Excel.Axis)oChart.Axes(
        Microsoft.Office.Interop.Excel.XlAxisType.xlValue,
        Microsoft.Office.Interop.Excel.XlAxisGroup.xlPrimary);

            //axis.HasTitle = true;
            //axis.AxisTitle.Text = "Sales Figures";
            // axis.HasMinorGridlines=true;
            Microsoft.Office.Interop.Excel.Axis ax = (Microsoft.Office.Interop.Excel.Axis)oChart.Axes(
       Microsoft.Office.Interop.Excel.XlAxisType.xlCategory, Microsoft.Office.Interop.Excel.XlAxisGroup.xlPrimary);

            //ax.HasTitle = true;
            //ax.AxisTitle.Text = "Sales Figures";
            ax.HasMajorGridlines = true;


            //  string filename = @"C:\Documents and Settings\tongxl\桌面\ccsb.xls";
          //  ws.SaveAs(filename, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            try
            {
                wb.Saved = true;
                wb.SaveCopyAs(filename);
            }
            catch (Exception ex)
            {
               
                MsgBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
            }
            ep.Quit();
          
            GC.Collect();//强行销毁





        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dw"></param>
        /// <param name="gc"></param>
        public static void ExportExcel(string title, string dw, GridControl gc)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string fname = "";
            saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fname = saveFileDialog1.FileName;
                    gc.ExportToExcelOld(fname);
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

        public static void ExportExcel2(string title, string dw, GridControl gc)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string fname = "";
            saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fname = saveFileDialog1.FileName;
                    gc.ExportToExcelOld(fname);
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
                        sv.Cells[2, i].Row.Height = 30;
                        sv.Cells[2, i].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                        if (i == 0)
                            sv.Columns[i].Width = 30;
                        else if (i == 1)
                            sv.Columns[i].Width = 120;
                        else 
                            sv.Columns[i].Width = 90;
                    }
                    for (int i = 3; i < RowCount; i++)
                    {
                        sv.Rows[i].Height = 30;
                        if (sv.Cells[i, 1].Text == "已立项目" || sv.Cells[i, 1].Text == "规划项目")
                        {
                            sv.Rows[i].Height = 90;
                        }

                    }

                    sv.AddRows(RowCount + 2, 2);
                    sv.Cells[RowCount + 2, 0].Text = "建表时间:" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                    sv.Cells[RowCount + 2, 0].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    sv.Cells[RowCount + 2, 0].HorizontalAlignment = CellHorizontalAlignment.Right;
                    sv.Cells[RowCount + 2, 0].VerticalAlignment = CellVerticalAlignment.Center;
                    sv.Cells[RowCount + 2, 0].ColumnSpan = ColumnCount;
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
    }
}
