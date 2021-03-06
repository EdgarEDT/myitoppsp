using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Common;

using Itop.Domain.Table;
using Itop.Common;


using System.Reflection;
using System.Diagnostics;
using DevExpress.Utils;

using Itop.Client.Base;
using FarPoint.Win;

using System.IO;

namespace Itop.Client.TableTemplateNW
{
    public partial class FrmTable : FormBase
    {
        //公共类实例化
        Tcommon TC = new Tcommon();
        //Excel文件名
        private string ExcelName = "HCTable2.xls";
        //将表类实例化
        Table2.Sheet2242zhu OBJ2242zhu = new Itop.Client.TableTemplateNW.Table2.Sheet2242zhu();
        Sheet212 OBJ212 = new Sheet212();
        public FrmTable()
        {
            InitializeComponent();
        }

        private void FrmZDYGH_Load(object sender, EventArgs e)
        {
            //根据窗口变化全部添满
            fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            fpSpread_addsheet();
        }
        private void fpSpread_addsheet()
        {
            WaitDialogForm wait = null;
            wait = new WaitDialogForm("", "正在加载数据, 请稍候...");
            try
            {
                //打开Excel表格
                //清空工作表
                fpSpread1.Sheets.Clear();
                fpSpread1.OpenExcel(Tcommon.ExcelFilePath+ ExcelName);
                TC.SpreadRemoveEmptyCells(fpSpread1);
            }
            catch (System.Exception e)
            {
                //如果打开出错则重新生成并保存
                fpSpread1.Sheets.Clear();
                Firstadddata();
                //判断文件夹是否存在，不存在则创建
                if (!Directory.Exists(Tcommon.CurrentPath+Tcommon.ExcelDir))
                {
                    Directory.CreateDirectory(Tcommon.CurrentPath + Tcommon.ExcelDir);
                }
                //保存excel文件
                fpSpread1.SaveExcel(Tcommon.ExcelFilePath + ExcelName);
                //以下是打开文件设表格自动换行

                // 定义要使用的Excel 组件接口
                // 定义Application 对象,此对象表示整个Excel 程序
                Microsoft.Office.Interop.Excel.Application excelApp = null;
                // 定义Workbook对象,此对象代表工作薄
                Microsoft.Office.Interop.Excel.Workbook workBook;
                // 定义Worksheet 对象,此对象表示Execel 中的一张工作表
                Microsoft.Office.Interop.Excel.Worksheet ws = null;
                Microsoft.Office.Interop.Excel.Range range = null;
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                string filename = Tcommon.ExcelFilePath + ExcelName;
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

        private void Firstadddata()
        {
            //生成表2242zhu
            FarPoint.Win.Spread.SheetView sheet2242zhu = new FarPoint.Win.Spread.SheetView();
            //生成表Sheet212
            FarPoint.Win.Spread.SheetView Sheet212 = new FarPoint.Win.Spread.SheetView();
          

            //添加表2242zhu
            fpSpread1.Sheets.Add(sheet2242zhu);
            //添加表Sheet212
            fpSpread1.Sheets.Add(Sheet212);
          

            //创建表2242zhu
            OBJ2242zhu.Build_sheet(sheet2242zhu);
            //创建表Sheet212
            OBJ212.Build_Sheet(Sheet212);
            
         
             
        }
        private void barBtnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        //保存
        private void barBtnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm wait = null;
            wait = new WaitDialogForm("", "正在保存数据, 请稍候...");
            //判断文件夹是否存在，不存在则创建
            if (!Directory.Exists(Tcommon.CurrentPath + Tcommon.ExcelDir))
            {
                Directory.CreateDirectory(Tcommon.CurrentPath + Tcommon.ExcelDir);
            }
            //保存excel文件
            try
            {
                //保存excel文件
                fpSpread1.SaveExcel(Tcommon.ExcelFilePath + ExcelName);
                //以下是打开文件设表格自动换行

                // 定义要使用的Excel 组件接口
                // 定义Application 对象,此对象表示整个Excel 程序
                Microsoft.Office.Interop.Excel.Application excelApp = null;
                // 定义Workbook对象,此对象代表工作薄
                Microsoft.Office.Interop.Excel.Workbook workBook;
                // 定义Worksheet 对象,此对象表示Execel 中的一张工作表
                Microsoft.Office.Interop.Excel.Worksheet ws = null;
                Microsoft.Office.Interop.Excel.Range range = null;
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                string filename = Tcommon.ExcelFilePath + ExcelName;
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
                wait.Close();
                MessageBox.Show("保存成功");
                
            }
            catch (System.Exception ee)
            {
                wait.Close();
                MessageBox.Show("保存错误！确定您安装有Office Excel,或者关闭所有Excel文件重试");
            }
        }
        //更新数据
        private void barBtnRefreshData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm newwait = new WaitDialogForm("", "正在更新数据, 请稍候...");
            //生成一个空表用来保存当前表

            int m = fpSpread1.ActiveSheetIndex;
            //清除所有表
            fpSpread1.Sheets.Clear();
            Firstadddata();
            //还原当前表
            fpSpread1.ActiveSheetIndex = m;
            //设文本自动换行
            TC.Sheet_Colautoenter(fpSpread1);
            newwait.Close();
            MessageBox.Show("更新数据完成！");
            barButtonItem1.Enabled = true;
        }
        //导出
        private void barBtnDaochuExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string fname = "";
            saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fname = saveFileDialog1.FileName;

                try
                {
                    fpSpread1.SaveExcel(fname);
                    //以下是打开文件设表格自动换行

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
                        range.WrapText = true;
                        //保护工作表
                        //ws.Protect(Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    }
                    //保存工作簿
                    workBook.Save();
                    //关闭工作簿
                    excelApp.Workbooks.Close();
                    if (MessageBox.Show("导出成功，是否打开该文档？","询问",MessageBoxButtons.YesNo,MessageBoxIcon.Question) != DialogResult.Yes)
                        return;

                    System.Diagnostics.Process.Start(fname);

                }
                catch
                {
                    MessageBox.Show("无法保存" + fname + "。请用其他文件名保存文件，或将文件存至其他位置。");
                    return;
                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TC.Show_Question();
        }

    
    
    }
}