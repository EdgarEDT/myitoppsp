using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DevExpress.XtraGrid;
using System.Windows.Forms;
using Itop.Common;
using FarPoint.Win.Spread;
using System.Reflection;
namespace Itop.Client.PWTable
{
    class FileClass
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

                try
                {
                    gridControl.ExportToExcelOld(fname);
                    FarPoint.Win.Spread.FpSpread fps = new FarPoint.Win.Spread.FpSpread();
                    fps.OpenExcel(fname);
                    SheetView sv = fps.Sheets[0];


                    for (int j = 0; j < sv.NonEmptyRowCount; j++)
                    {
                        for (int k = 0; k < sv.NonEmptyColumnCount; k++)
                        {
                            sv.Cells[j,k].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
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


            //return true;
            //}
            //catch { }
        }
    }
}
