using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
namespace Itop.TLPSP.DEVICE
{
    public class ExcelAccess
    {
        private string myFileName;

        public string MyFileName
        {
            get { return myFileName; }
            set { myFileName = value; }
        }
        Application myExcel;

        public Application MyExcel
        {
            get { return myExcel; }
            set { myExcel = value; }
        }
        Workbook myWorkBook;

        public Workbook MyWorkBook
        {
            get { return myWorkBook; }
            set
            {
                myWorkBook = value;
                myExcel = value.Application;
            }
        }


        /// <summary>
        /// 构造函数，不创建Excel工作薄
        /// </summary>
        public ExcelAccess()
        {

        }

        /// <summary>
        /// 构造函数，不创建Excel工作薄
        /// </summary>
        public ExcelAccess(Workbook workbook)
        {
            myExcel = workbook.Application;
            myWorkBook = workbook;
        }
        /// <summary>
        /// 创建Excel工作薄
        /// </summary>
        public void CreateExcel()
        {
            MyExcel = new Application();
            MyWorkBook = MyExcel.Workbooks.Add(true);
        }


        /// <summary>
        /// 显示工作薄
        /// </summary>
        public void ShowExcel()
        {
            MyExcel.Visible = true;
        }
        /// <summary>
        /// 打印预览工作薄
        /// </summary>
        public void PrintPreview()
        {
            Microsoft.Office.Interop.Excel.Worksheet ws = MyExcel.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;
            if (ws != null)
                ws.PrintPreview(true);
        }


        /// <summary>
        /// 将数据集合写入Excel
        /// </summary>
        /// <param name="data">要写入的二维数组数据</param>
        /// <param name="startRow">Excel起始行</param>
        /// <param name="startColumn">Excel起始列</param>
        public void SetCellValue(string[,] data, int startRow, int startColumn)
        {
            int rowCount = data.GetLength(0);
            int columnCount = data.GetLength(1);
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    MyExcel.Cells[startRow + i, startColumn + j] = "'" + data[i, j];
                }

            }
        }
        /// <summary>
        /// 复制页
        /// </summary>
        /// <param name="sourceNum">源页号</param>
        /// <param name="afterNum">排哪个页号后面</param>
        public void CopySheet(int sourceNum, int afterNum)
        {
            Worksheet mySheet = myWorkBook.Sheets[sourceNum] as Worksheet;
            mySheet.Copy(Type.Missing, myWorkBook.Sheets[afterNum]);
        }
        /// <summary>
        /// 删除页
        /// </summary>
        /// <param name="sheetNum">页号</param>
        public void DeleteSheet(int sheetNum)
        {
            myExcel.DisplayAlerts = false; //如果想删除某个sheet页，首先要将此项设为fasle。
            (myExcel.ActiveWorkbook.Sheets[1] as Worksheet).Delete();
        }
        /// <summary>
        /// 将数据写入指定的单元格
        /// </summary>
        /// <param name="data">要写入的数据</param>
        /// <param name="row">行号</param>
        /// <param name="column">列号</param>
        public void SetCellValue(string data, int row, int column)
        {
            MyExcel.Cells[row, column] = data;
        }

        /// <summary>
        /// 将DataTable对象写入Excel
        /// </summary>
        /// <param name="data">DataTable对象</param>
        /// <param name="startRow">Excel起始行</param>
        /// <param name="startColumn">Excel起始列</param>
        public void SetCellValue(System.Data.DataTable data, int startRow, int startColumn)
        {
            for (int i = 0; i < data.Rows.Count; i++)
            {
                for (int j = 0; j < data.Columns.Count; j++)
                {
                    MyExcel.Cells[startRow + i, startColumn + j] = "'" + data.Rows[i][j].ToString();
                }
            }
        }


        /// <summary>
        /// 读取指定单元格的数据
        /// </summary>
        /// <param name="rowNum">指定的行号</param>
        /// <param name="columnNum">指定的列号</param>
        /// <returns></returns>
        public string ReadCellValue(int rowNum, int columnNum)
        {
            Range range = MyExcel.get_Range(MyExcel.Cells[rowNum, columnNum], MyExcel.Cells[rowNum, columnNum]);
            return range.Text.ToString();
        }


        /// <summary>
        /// 向Excel中插入图片
        /// </summary>
        /// <param name="picPath">图片的绝对路径</param>
        public void InsertPicture(string picPath)
        {
            Worksheet workSheet = MyExcel.ActiveSheet as Worksheet;
           // workSheet.Shapes.AddPicture(picPath, MsoTriState.msoFalse, MsoTriState.msoCTrue, 10, 10, 150, 150);

        }


        /// <summary>
        /// 向Excel中插入图片
        /// </summary>
        /// <param name="picPath">图片的路径</param>
        /// <param name="top">上边距</param>
        /// <param name="left">左边距</param>
        /// <param name="height">高度</param>
        /// <param name="width">宽度</param>
        public void InsertPicture(string picPath, float top, float left, float height, float width)
        {

            Worksheet workSheet = MyExcel.ActiveSheet as Worksheet;
          // workSheet.Shapes.AddPicture(picPath, MsoTriState.msoFalse, MsoTriState.msoCTrue, left, top, width, height);
            
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="x1">开始的行号</param>
        /// <param name="y1">开始列</param>
        /// <param name="x2">结束行号</param>
        /// <param name="y2">结束列号</param>
        public void MergeCell(Worksheet excelWorkSheet, int x1, int y1, int x2, int y2)
        {

            myExcel.DisplayAlerts = false;// 关闭合并时提示
            Range range = (excelWorkSheet.get_Range(excelWorkSheet.Cells[x1, y1], excelWorkSheet.Cells[x2, y2]));
            range.Merge(oMissing);
            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            myExcel.DisplayAlerts = true;
        }
        object oMissing = Missing.Value;

        /// <summary>
        /// 打印表
        /// </summary>
        public void PrintWorkSheet(Worksheet excelWorkSheet)
        {
            Range range = excelWorkSheet.UsedRange;
            range.Select();
            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            excelWorkSheet.PageSetup.PrintTitleRows = "$1:$1";//每页都打印标题
            excelWorkSheet.PageSetup.PrintGridlines = true;//打印表格
            //range.PrintPreview(oMissing);
            range.PrintOut(oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
        }
        /// <summary>
        /// 重命名工作表
        /// </summary>
        /// <param name="sheetNum">工作表序号，从左到右，从1开始</param>
        /// <param name="newName">新的工作表名</param>
        public void ReNameWorkSheet(int sheetNum, string newName)
        {
            Worksheet workSheet = MyExcel.Worksheets[sheetNum] as Worksheet;
            workSheet.Name = newName;
        }

        /// <summary>
        /// 重名命工作表
        /// </summary>
        /// <param name="oldName">旧的工作表名</param>
        /// <param name="newName">新的工作表名</param>
        public void ReNameWorkSheet(string oldName, string newName)
        {
            Worksheet workSheet = MyExcel.Worksheets[oldName] as Worksheet;
            workSheet.Name = newName;
        }

        /// <summary>
        /// 新建工作表
        /// </summary>
        /// <param name="sheetName">工作表名</param>
        public void CreateWorkSheet(string sheetName)
        {
            Worksheet newWorkSheet = MyExcel.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing) as Worksheet;
            newWorkSheet.Name = sheetName;

        }

        /// <summary>
        /// 激活工作表
        /// </summary>
        /// <param name="sheetNum">工作表序号</param>
        public void ActiveSheet(int sheetNum)
        {

            Worksheet workSheet = MyExcel.Worksheets[sheetNum] as Worksheet;
            workSheet.Activate();
        }

        /// <summary>
        /// 激活工作表
        /// </summary>
        /// <param name="sheetName">工作表名</param>
        public void ActiveSheet(string sheetName)
        {
            Worksheet workSheet = MyExcel.Worksheets[sheetName] as Worksheet;
            workSheet.Activate();
        }

        /// <summary>
        /// 删除工作表
        /// </summary>
        /// <param name="sheetNum">工作表序号</param>
        public void DeleteWorkSheet(int sheetNum)
        {

            (MyExcel.Worksheets[sheetNum] as Worksheet).Delete();
        }

        /// <summary>
        /// 删除工作表
        /// </summary>
        /// <param name="sheetName"></param>
        public void DeleteWorkSheet(string sheetName)
        {

            (MyExcel.Worksheets[sheetName] as Worksheet).Delete();
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="startRow">开始行</param>
        /// <param name="startColumn">开始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        public void UnitCells(int startRow, int startColumn, int endRow, int endColumn)
        {

            Range range = MyExcel.get_Range(MyExcel.Cells[startRow, startColumn], MyExcel.Cells[endRow, endColumn]);
            range.MergeCells = true;
        }

        /// <summary>
        /// 设置对齐方式
        /// </summary>
        /// <param name="startRow">开始行</param>
        /// <param name="startColumn">开始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        /// <param name="hAlign">水平对齐</param>
        /// <param name="vAlign">垂直对齐</param>
        public void AlignmentCells(int startRow, int startColumn, int endRow, int endColumn, ExcelStyle.ExcelHAlign hAlign, ExcelStyle.ExcelVAlign vAlign)
        {
            Range range = MyExcel.get_Range(MyExcel.Cells[startRow, startColumn], MyExcel.Cells[endRow, endColumn]);
            range.VerticalAlignment = vAlign;
            range.HorizontalAlignment = hAlign;
        }


        /// <summary>
        /// 绘制指定单元格的边框
        /// </summary>
        /// <param name="startRow">起始行</param>
        /// <param name="startColumn">起始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        /// <param name="isDrawTop">是否画上外框</param>
        /// <param name="isDrawBottom">是否画下外框</param>
        /// <param name="isDrawLeft">是否画左外框</param>
        /// <param name="isDrawRight">是否画右外框</param>
        /// <param name="isDrawHInside">是否画水平内框</param>
        /// <param name="isDrawVInside">是否画垂直内框</param>
        /// <param name="isDrawDown">是否画斜向下线</param>
        /// <param name="isDrawUp">是否画斜向上线</param>
        /// <param name="lineStyle">线类型</param>
        /// <param name="borderWeight">线粗细</param>
        /// <param name="color">线颜色</param>

        public void DrawCellsFrame(int startRow, int startColumn, int endRow, int endColumn,
            bool isDrawTop, bool isDrawBottom, bool isDrawLeft, bool isDrawRight,
            bool isDrawHInside, bool isDrawVInside, bool isDrawDiagonalDown, bool isDrawDiagonalUp,
           ExcelStyle.LineStyle lineStyle, ExcelStyle.BorderWeight borderWeight, ExcelStyle.ColorIndex color)
        {
            //获取画边框的单元格
            Range range = MyExcel.get_Range(MyExcel.Cells[startRow, startColumn], MyExcel.Cells[endRow, endColumn]);

            //清除所有边框
            range.Borders[XlBordersIndex.xlEdgeTop].LineStyle = ExcelStyle.LineStyle.无;
            range.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = ExcelStyle.LineStyle.无;
            range.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = ExcelStyle.LineStyle.无;
            range.Borders[XlBordersIndex.xlEdgeRight].LineStyle = ExcelStyle.LineStyle.无;
            range.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = ExcelStyle.LineStyle.无;
            range.Borders[XlBordersIndex.xlInsideVertical].LineStyle = ExcelStyle.LineStyle.无;
            range.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = ExcelStyle.LineStyle.无;
            range.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = ExcelStyle.LineStyle.无;

            //以下是按参数画边框 
            if (isDrawTop)
            {
                range.Borders[XlBordersIndex.xlEdgeTop].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlEdgeTop].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlEdgeTop].ColorIndex = color;
            }

            if (isDrawBottom)
            {
                range.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlEdgeBottom].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlEdgeBottom].ColorIndex = color;
            }

            if (isDrawLeft)
            {
                range.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlEdgeLeft].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlEdgeLeft].ColorIndex = color;
            }

            if (isDrawRight)
            {
                range.Borders[XlBordersIndex.xlEdgeRight].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlEdgeRight].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlEdgeRight].ColorIndex = color;
            }

            if (isDrawVInside)
            {
                range.Borders[XlBordersIndex.xlInsideVertical].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlInsideVertical].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlInsideVertical].ColorIndex = color;
            }

            if (isDrawHInside)
            {
                range.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlInsideHorizontal].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlInsideHorizontal].ColorIndex = color;
            }

            if (isDrawDiagonalDown)
            {
                range.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlDiagonalDown].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlDiagonalDown].ColorIndex = color;
            }

            if (isDrawDiagonalUp)
            {
                range.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlDiagonalUp].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlDiagonalUp].ColorIndex = color;
            }

        }

        /// <summary>
        /// 设置单元格背景色及填充方式
        /// </summary>
        /// <param name="startRow">起始行</param>
        /// <param name="startColumn">起始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        /// <param name="color">颜色索引</param>
        public void CellsBackColor(int startRow, int startColumn, int endRow, int endColumn, ExcelStyle.ColorIndex color)
        {
            Range range = MyExcel.get_Range(MyExcel.Cells[startRow, startColumn], MyExcel.Cells[endRow, endColumn]);
            range.Interior.ColorIndex = color;
            range.Interior.Pattern = ExcelStyle.Pattern.Solid;
        }

        /// <summary>
        ///设置单元格背景色及填充方式
        /// </summary>
        /// <param name="startRow">开始行</param>
        /// <param name="startColumn">开始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        /// <param name="color">颜色索引</param>
        /// <param name="pattern">填充方式</param>
        public void CellsBackColor(int startRow, int startColumn, int endRow, int endColumn, ExcelStyle.ColorIndex color, ExcelStyle.Pattern pattern)
        {
            Range range = MyExcel.get_Range(MyExcel.Cells[startRow, startColumn], MyExcel.Cells[endRow, endColumn]);
            range.Interior.ColorIndex = color;
            range.Interior.Pattern = pattern;
        }

        /// <summary>
        /// 设置行高
        /// </summary>
        /// <param name="startRow">开始行</param>
        /// <param name="endRow">结束行</param>
        /// <param name="height">行高</param>
        public void SetRowHeight(int startRow, int endRow, float height)
        {
            Worksheet workSheet = MyExcel.ActiveSheet as Worksheet;
            Range range = workSheet.Rows[startRow.ToString() + ":" + endRow.ToString(), System.Type.Missing] as Range;
            range.RowHeight = height;
        }

        /// <summary>
        /// 设置列宽
        /// </summary>
        /// <param name="startColumn">开始列</param>
        /// <param name="endColumn">结束列</param>
        /// <param name="width">宽度</param>
        public void SetColumnWidth(string startColumn, string endColumn, float width)
        {
            Worksheet workSheet = MyExcel.ActiveSheet as Worksheet;
            Range range = workSheet.Columns[startColumn.ToString() + ":" + endColumn.ToString(), System.Type.Missing] as Range;
            range.ColumnWidth = width;
        }

        /// <summary>
        /// 自动调整行高
        /// </summary>
        /// <param name="rowNum">行号</param>
        public void RowAutoFit(int rowNum)
        {

            Worksheet workSheet = MyExcel.ActiveSheet as Worksheet;
            Range range = workSheet.Rows[rowNum.ToString() + ":" + rowNum.ToString(), System.Type.Missing] as Range;
            range.EntireColumn.AutoFit();
        }

        /// <summary>
        /// 设置列宽
        /// </summary>
        /// <param name="startColumn">起始列</param>
        /// <param name="endColumn">结束列</param>
        /// <param name="width">宽度</param>
        public void SetColumnWidth(int startColumn, int endColumn, float width)
        {

            string strStartColumn = GetColumnName(startColumn);
            string strEndColumn = GetColumnName(endColumn);

            Worksheet workSheet = MyExcel.ActiveSheet as Worksheet;
            Range range = workSheet.Columns[strStartColumn + ":" + strEndColumn, System.Type.Missing] as Range;
            range.ColumnWidth = width;

        }

        /// <summary>
        /// 设置字体颜色
        /// </summary>
        /// <param name="startRow">起始行</param>
        /// <param name="startColumn">起始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        /// <param name="color">颜色索引</param>
        public void SetFontColor(int startRow, int startColumn, int endRow, int endColumn, ExcelStyle.ColorIndex color)
        {
            Range range = MyExcel.get_Range(MyExcel.Cells[startRow, startColumn], MyExcel.Cells[endRow, endColumn]);
            range.Font.ColorIndex = color;
        }

        /// <summary>
        /// 设置字体样式
        /// </summary>
        /// <param name="startRow">起始行</param>
        /// <param name="startColumn">起始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        /// <param name="isBold">是否加粗</param>
        /// <param name="isItalic">是否斜体</param>
        /// <param name="underline">下划线类型</param>
        public void SetFontStyle(int startRow, int startColumn, int endRow, int endColumn, bool isBold, bool isItalic, ExcelStyle.UnderlineStyle underline)
        {
            Range range = MyExcel.get_Range(MyExcel.Cells[startRow, startColumn], MyExcel.Cells[endRow, endColumn]);
            range.Font.Bold = isBold;
            range.Font.Italic = isItalic;
            range.Font.Underline = underline;
        }

        /// <summary>
        /// 设置字体和大小
        /// </summary>
        /// <param name="startRow">开始行</param>
        /// <param name="startColumn">开始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        public void SetFontNameSize(int startRow, int startColumn, int endRow, int endColumn, string fontName, int fontSize)
        {
            Range range = MyExcel.get_Range(MyExcel.Cells[startRow, startColumn], MyExcel.Cells[endRow, endColumn]);
            range.Font.Name = fontName;
            range.Font.Size = fontSize;

        }

        /// <summary>
        /// 打开存在的Excel文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        public void Open(string fileName)
        {
            MyExcel = new Application();
            MyWorkBook = MyExcel.Workbooks.Add(fileName);
            MyFileName = fileName;
        }

        /// <summary>
        /// 保存Excel
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {

            if (MyFileName == "")
            {
                return false;
            }
            else
            {

                try
                {
                    MyWorkBook.Save();
                    return true;
                }
                catch (Exception ex)
                {

                    return false;
                }
            }
        }
        private string GetColumnName(int number)
        {
            int h, l;
            h = number / 26;
            l = number % 26;
            if (l == 0)
            {
                h -= 1;
                l = 26;
            }
            string s = GetLetter(h) + GetLetter(l);
            return s;
        }
        private string GetLetter(int number)
        {
            switch (number)
            {
                case 1:
                    return "A";
                case 2:
                    return "B";
                case 3:
                    return "C";
                case 4:
                    return "D";
                case 5:
                    return "E";
                case 6:
                    return "F";
                case 7:
                    return "G";
                case 8:
                    return "H";
                case 9:
                    return "I";
                case 10:
                    return "J";
                case 11:
                    return "K";
                case 12:
                    return "L";
                case 13:
                    return "M";
                case 14:
                    return "N";
                case 15:
                    return "O";
                case 16:
                    return "P";
                case 17:
                    return "Q";
                case 18:
                    return "R";
                case 19:
                    return "S";
                case 20:
                    return "T";
                case 21:
                    return "U";
                case 22:
                    return "V";
                case 23:
                    return "W";
                case 24:
                    return "X";
                case 25:
                    return "Y";
                case 26:
                    return "Z";
                default:
                    return "";
            }
        }
        public void CopyToClipboard(Range p_objFromRange)
        {
            p_objFromRange.Copy(Type.Missing);
        }
        public void PasteFromClipboard(Range p_objDestRange)
        {

            p_objDestRange.PasteSpecial(XlPasteType.xlPasteAll,
               XlPasteSpecialOperation.xlPasteSpecialOperationNone,
                Type.Missing, Type.Missing);
        }
        public void ProtectSheet(string p_strSheetName)
        {
            Worksheet objSheet = (Worksheet)myWorkBook.Sheets[p_strSheetName];
            objSheet.Protect(Type.Missing, false, Type.Missing, false, Type.Missing, Type.Missing,
                 Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                 Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        }
        /// <summary>
        /// 关闭excel对象,不保存
        /// </summary>
        /// <param name="isTrue"></param>
        private void CloseExcel(bool isTrue)
        {
            if (myWorkBook != null)
            {
                myWorkBook.Close(isTrue, oMissing, oMissing);
            }
            if (myExcel != null)
            {
                myExcel.Quit();
            }
        }
        /// <summary>
        /// 销毁excel对象
        /// </summary>
        private void ReleaseExcel()
        {
            if (myExcel != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject((object)myExcel);
            }

            if (myWorkBook != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject((object)myWorkBook);
            }

        }

        /// <summary>
        /// 销毁关闭的excel
        /// </summary>
        public void DisPoseExcel()
        {
            CloseExcel(false);
            ReleaseExcel();
            GC.Collect();
        }
        /// <summary>
        /// 单元格部分字体加粗
        /// </summary>
        /// <param name="startRow">起始行</param>
        /// <param name="startColumn">起始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        /// <param name="isBold">是否加粗</param>
        /// <param name="start">开始字体</param>
        /// <param name="length">长度</param>
        public void SetFontBold(int startRow, int startColumn, int endRow, int endColumn, bool isBold, int start, int length)
        {

            Range range = MyExcel.get_Range(MyExcel.Cells[startRow, startColumn], MyExcel.Cells[endRow, endColumn]);
            range.get_Characters(start, length).Font.Bold = isBold;
        }


    }
}
