using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.Win;
using System.Drawing;
using System.Collections;

namespace Itop.Client.GYGH
{
    public class fpcommon
    {

        /// <summary>
        /// //本方法用于去掉多表格中多余的空行和空列
        /// </summary>
        /// <param name="tempspread"></param>
        public void SpreadRemoveEmptyCells(FarPoint.Win.Spread.FpSpread tempspread)
        {

            //定义无空单元格模式
            FarPoint.Win.Spread.Model.INonEmptyCells nec;
            //计算spread有多少个表格
            int sheetscount = tempspread.Sheets.Count;
            //定义行数
            int rowcount = 0;
            //定义列数
            int colcount = 0;
            for (int m = 0; m < sheetscount; m++)
            {
                nec = (FarPoint.Win.Spread.Model.INonEmptyCells)tempspread.Sheets[m].Models.Data;
                //计算无空单元格列数
                colcount = nec.NonEmptyColumnCount;
                //计算无空单元格行数
                rowcount = nec.NonEmptyRowCount;
                tempspread.Sheets[m].RowCount = rowcount;
                tempspread.Sheets[m].ColumnCount = colcount;
            }
        }
        /// <summary>
        /// 本方法用于清空fpread中的sheet内容，保留sheetname,这样数据更新时用户使用的sheet顺序不会变
        /// </summary>
        /// <param name="tempspread"></param>
        public void SpreadClearSheet(FarPoint.Win.Spread.FpSpread tempspread)
        {
            //通过设置行列数为零来清空数据
            int sheetscount = tempspread.Sheets.Count;
            for (int i = 0; i < sheetscount; i++)
            {
                tempspread.Sheets[i].RowCount = 0;
                tempspread.Sheets[i].ColumnCount = 0;
            }
        }

        /// <summary>
        /// 本方法用于设置工作表行列的显示模式为R1C1模式
        /// </summary>
        /// <param name="tempspread"></param>
        public void Sheet_Referen_R1C1(FarPoint.Win.Spread.SheetView obj_sheet)
        {

            obj_sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;


        }
        /// <summary>
        /// 本方法用于写入行求和公式
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="startrow">要求和部分起始行号</param>
        /// <param name="startcol">要求和部分起始列号</param>
        /// <param name="itemcount">要求和部分的项目数</param>
        /// <param name="itemlenth">每个项目行数</param>
        /// <param name="writerow">公式写入开始行</param>
        /// <param name="writecol">公式写入开始列</param>
        /// <param name="countcol">统计列数</param>
        public void Sheet_WriteFormula_RowSum(FarPoint.Win.Spread.SheetView obj_sheet, int startrow, int startcol, int itemcount, int itemlenth, int writerow, int writecol, int countcol)
        {

            for (int col = 0; col < countcol; col++)
            {
                for (int n = 0; n < itemlenth; n++)
                {
                    string SumFormula = "";
                    for (int m = 0; m < itemcount; m++)
                    {
                        SumFormula += "," + "R" + (startrow + m * itemlenth + n + 1) + "C" + (startcol + col + 1);
                    }
                    SumFormula = "SUM(" + SumFormula.Substring(1, SumFormula.Length - 1) + ")";
                    obj_sheet.Cells[writerow + n, writecol + col].Formula = SumFormula;
                }
            }

        }
        /// <summary>
        /// 本方法主要用于列求和统计写入公式，结果只有一列
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="startrow">起始行号</param>
        /// <param name="startcol">起始列号</param>
        /// <param name="countrow">统计总行数</param>
        /// <param name="countcol">统计总列数</param>
        /// <param name="writecol">写入列</param>
        public void Sheet_WriteFormula_ColSum_WritOne(FarPoint.Win.Spread.SheetView obj_sheet, int startrow, int startcol, int countrow, int countcol, int writecol)
        {
            for (int row = 0; row < countrow; row++)
            {
                string SumFormula = "";
                for (int col = 0; col < countcol; col++)
                {
                    SumFormula += "," + "R" + (startrow + row + 1) + "C" + (startcol + col + 1);
                }
                SumFormula = "SUM(" + SumFormula.Substring(1, SumFormula.Length - 1) + ")";
                obj_sheet.Cells[startrow + row, writecol].Formula = SumFormula;
            }

        }
        /// <summary>
        /// 本方法用来写形如three=one+two的列求和公式
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="onerow"></param>
        /// <param name="onecol"></param>
        /// <param name="twocol"></param>
        /// <param name="threecol"></param>
        /// <param name="rowcount"></param>
        public void Sheet_WriteFormula_OneCol_TwoCol_Threecol_sum(FarPoint.Win.Spread.SheetView obj_sheet, int onerow, int onecol, int twocol, int threecol, int rowcount)
        {
            for (int row = 0; row < rowcount; row++)
            {
                obj_sheet.Cells[onerow + row, threecol].Formula = "SUM(R" + (onerow + row + 1) + "C" + (onecol + 1) + ", R" + (onerow + row + 1) + "C" + (twocol + 1) + ")";
            }
        }
        /// <summary>
        /// 本方法主要用于写多列为一组的列求和公式(判断合并单元格)
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="startrow">开始行号</param>
        /// <param name="startcol">开始列号</param>
        /// <param name="rowcount">要统计的总行数</param>
        /// <param name="itemcount">要统计的项数</param>
        /// <param name="writerow">写入公式行号</param>
        /// <param name="writecol">写入公式列号</param>
        /// <param name="itemlenth">每项包含的列数</param>
        public void Sheet_WriteFormula_MoreColsum(FarPoint.Win.Spread.SheetView obj_sheet, int startrow, int startcol, int rowcount, int itemcount, int writerow, int writecol, int itemlenth)
        {

            for (int row = 0; row < rowcount; row++)
            {
                for (int m = 0; m < itemlenth; m++)
                {
                    string SumFormula = "";
                    for (int col = 0; col < itemcount; col++)
                    {
                        //这里可以跳过多行合并的情况
                        if (obj_sheet.Cells[startrow + row, col * itemlenth + startcol + m].Value != null)
                        {
                            SumFormula += "," + "R" + (startrow + row + 1) + "C" + (col * itemlenth + startcol + m + 1);

                        }
                        else
                        {
                            break;
                        }

                    }
                    if (SumFormula != "")
                    {
                        SumFormula = "SUM(" + SumFormula.Substring(1, SumFormula.Length - 1) + ")";
                        obj_sheet.Cells[writerow + row, writecol + m].Formula = SumFormula;
                    }

                }

            }


        }
        /// <summary>
        /// 本方法主要用于写多列为一组的列求和公式(不判断合并单元格)
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="startrow">开始行号</param>
        /// <param name="startcol">开始列号</param>
        /// <param name="rowcount">要统计的总行数</param>
        /// <param name="itemcount">要统计的项数</param>
        /// <param name="writerow">写入公式行号</param>
        /// <param name="writecol">写入公式列号</param>
        /// <param name="itemlenth">每项包含的列数</param>
        public void Sheet_WriteFormula_MoreColsum_NoSpan(FarPoint.Win.Spread.SheetView obj_sheet, int startrow, int startcol, int rowcount, int itemcount, int writerow, int writecol, int itemlenth)
        {

            for (int row = 0; row < rowcount; row++)
            {
                for (int m = 0; m < itemlenth; m++)
                {
                    string SumFormula = "";
                    for (int col = 0; col < itemcount; col++)
                    {
                        SumFormula += "," + "R" + (startrow + row + 1) + "C" + (col * itemlenth + startcol + m + 1);

                    }
                    SumFormula = "SUM(" + SumFormula.Substring(1, SumFormula.Length - 1) + ")";
                    obj_sheet.Cells[writerow + row, writecol + m].Formula = SumFormula;
                }

            }
        }
        /// <summary>
        /// 本方法主要用于单元格中写入数据0
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="startrow">起始行号</param>
        /// <param name="startcol">起始列号</param>
        /// <param name="rowcount">总行数</param>
        /// <param name="colcount">总列数</param>
        public void Sheet_WriteZero(FarPoint.Win.Spread.SheetView obj_sheet, int startrow, int startcol, int rowcount, int colcount)
        {
            for (int row = 0; row < rowcount; row++)
            {
                for (int col = 0; col < colcount; col++)
                {
                    obj_sheet.SetValue(startrow + row, startcol + col, 0);
                }
            }
        }
        /// <summary>
        /// 本方法设置表格只读
        /// </summary>
        /// <param name="obj_sheet"></param>
        public void Sheet_ReadOnly(FarPoint.Win.Spread.SheetView obj_sheet)
        {

            obj_sheet.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
        }
        /// <summary>
        /// 本方法给表格设边框线并同时单元格内容水平垂直均居中
        /// </summary>
        /// <param name="obj_sheet"></param>
        public void Sheet_GridandCenter(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            //定义一个边框线
            LineBorder border = new LineBorder(Color.Black);
            int rowcount = obj_sheet.Rows.Count;
            int colcount = obj_sheet.Columns.Count;

            for (int col = 0; col < colcount; col++)
            {
                for (int row = 0; row < rowcount; row++)
                {
                    //设表格线
                    obj_sheet.Cells[row, col].Border = border;

                }

                //水平和垂直均居中对齐
                obj_sheet.Columns[col].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                obj_sheet.Columns[col].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            }

        }
        /// <summary>
        /// 查找行合并单元格的内容
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="row">开始行号</param>
        /// <param name="col">开始列号</param>
        /// <returns></returns>
        public string Sheet_find_Rownotemptycell(FarPoint.Win.Spread.SheetView obj_sheet, int row, int col)
        {
            //对于合并单元格的表格，除合并行的第一行能找到数据外，其余行为null
            //本方法用于返回合并行的数据，递归向上找。返回值为“错误”表示没找到数据
            if (obj_sheet.Cells[row, col].Value != null)
            {
                return obj_sheet.Cells[row, col].Value.ToString();
            }
            else
            {
                if (row != 0)
                {
                    return Sheet_find_Rownotemptycell(obj_sheet, row - 1, col);
                }
                else
                {
                    return "!错误";
                }
            }
        }
        /// <summary>
        /// 查找列合并单元格的内容
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="row">开始行号</param>
        /// <param name="col">开始列号</param>
        /// <returns></returns>
        public string Sheet_find_Colnotemptycell(FarPoint.Win.Spread.SheetView obj_sheet, int row, int col)
        {
            //对于合并单元格的表格，除合并列的第一列能找到数据外，其余行为null
            //本方法用于返回合并列的数据，递归向上找。返回值为“错误”表示没找到数据
            if (obj_sheet.Cells[row, col].Value != null)
            {
                return obj_sheet.Cells[row, col].Value.ToString();
            }
            else
            {
                if (col != 0)
                {
                    return Sheet_find_Colnotemptycell(obj_sheet, row, col - 1);
                }
                else
                {
                    return "!错误";
                }
            }
        }
        /// <summary>
        /// 设置表格行列数， SheetName，合并第一行加表名
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="rowcount">行数</param>
        /// <param name="colcount">列数</param>
        /// <param name="title">报表名</param>
        /// <param name="sheetname">工作标签名</param>
        public void Sheet_RowCol_Title_Name(FarPoint.Win.Spread.SheetView obj_sheet, int rowcount, int colcount, string title, string sheetname)
        {
            obj_sheet.RowCount = rowcount;
            obj_sheet.ColumnCount = colcount;
            obj_sheet.SheetName = sheetname;
            obj_sheet.AddSpanCell(0, 0, 1, colcount);
            obj_sheet.SetValue(0, 0, title);
            obj_sheet.Rows[0].Height = 35;
        }
        /// <summary>
        /// 设表格文本自动换行
        /// </summary>
        /// <param name="obj_sheet"></param>
        public void Sheet_Colautoenter(FarPoint.Win.Spread.FpSpread tempspread)
        {
            //FarPoint.Win.Spread.CellType.TextCellType cellType = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.GeneralCellType cellType = new FarPoint.Win.Spread.CellType.GeneralCellType();
            cellType.WordWrap = true;
            for (int i = 0; i < tempspread.Sheets.Count; i++)
            {
                for (int col = 0; col < tempspread.Sheets[i].ColumnCount; col++)
                {
                    tempspread.Sheets[i].Columns[col].CellType = cellType;
                }
            }


        }
        /// <summary>
        /// 用于锁定表格
        /// </summary>
        /// <param name="obj_sheet"></param>
        public void Sheet_Locked(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            int rowcount = obj_sheet.RowCount;
            int colcount = obj_sheet.ColumnCount;

            for (int col = 0; col < colcount; col++)
            {
                obj_sheet.Columns[col].Locked = true;
            }
        }
        /// <summary>
        /// 将指定单元格解锁并设单元格格式为数值
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void Sheet_UnLockedandCellNumber(FarPoint.Win.Spread.SheetView obj_sheet, int row, int col)
        {
            obj_sheet.Cells[row, col].Locked = false;
           // obj_sheet.Cells[row, col].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
        }
        /// <summary>
        /// 本方法用来返回从指定列查找指定内容第一次匹配的行号
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="col">要查找内容的列号</param>
        /// <param name="strvalue">要查找的字符串值</param>
        /// <returns>如果返回-1表示没有找到</returns>
        public int Sheet_Find_Value(FarPoint.Win.Spread.SheetView obj_sheet, int col, string strvalue)
        {
            int flag = 0;
            int num = 0;
            for (int row = 0; row < obj_sheet.RowCount; row++)
            {
                if (obj_sheet.Cells[row, col].Value != null && obj_sheet.Cells[row, col].Value.ToString() == strvalue)
                {
                    flag = 1;
                    num = row;
                    break;
                }
            }
            if (flag != 0)
            {
                return num;
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// 用来反回中英文数字混排的字符串的长度，其中中文为2个英文或数字为1个
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int Text_Lenght(string str)
        {
            int len = 0;
            for (int i = 0; i < str.Length; i++)
            {
                byte[] byte_len = Encoding.Default.GetBytes(str.Substring(i, 1));
                if (byte_len.Length > 1)
                    len += 2;  //如果长度大于1，是中文，占两个字节，+2
                else
                    len += 1;  //如果长度等于1，是英文，占一个字节，+1
            }
            return len;
        }

        /// <summary>
        /// 本方法主要用于写除法公式，用于行求单项占总数百分比情况,形如电网现状3-6表
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="yrow">引用行号</param>
        /// <param name="ycol">引用列号</param>
        /// <param name="span">间隔行数(0表示无间隔)</param>
        /// <param name="wrow">写入公式行</param>
        /// <param name="wcol">写入公式列</param>
        /// <param name="itemcout">行项目数</param>
        /// <param name="colcount">共有多少列要写</param>
        /// <param name="perentrow">除数行号（固定的那一行）</param>
        public void Sheet_WriteFormula_Percent_Row(FarPoint.Win.Spread.SheetView obj_sheet, int yrow, int ycol, int span, int wrow, int wcol, int itemcout, int colcount, int perentrow)
        {
            FarPoint.Win.Spread.CellType.PercentCellType pct = new FarPoint.Win.Spread.CellType.PercentCellType();
            for (int i = 0; i < itemcout; i++)
            {
                for (int j = 0; j < colcount; j++)
                {
                    obj_sheet.Cells[wrow + i * (span + 1), wcol + j].Formula = "R" + (yrow + i * (span + 1) + 1) + "C" + (ycol + j + 1) + "/" + "R" + (perentrow + 1) + "C" + (ycol + j + 1);
                    obj_sheet.Cells[wrow + i * (span + 1), wcol + j].CellType = pct;
                }

            }
        }
        /// <summary>
        /// 本方法用于求列之间的百分比，形如配电网现状3-8中的百分比
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="startrow">求百分比的行号</param>
        /// <param name="startcol">求百分比的列号</param>
        /// <param name="items">项目数</param>
        /// <param name="length">每个项目长度</param>
        /// <param name="writerow">写入公式行号</param>
        /// <param name="writecol">写入公式列号</param>
        public void Sheet_WriteFormula_TwoCol_Percent(FarPoint.Win.Spread.SheetView obj_sheet, int startrow, int startcol, int items, int length, int writerow, int writecol)
        {
            FarPoint.Win.Spread.CellType.PercentCellType pct = new FarPoint.Win.Spread.CellType.PercentCellType();
            for (int i = 0; i < items; i++)
            {
                string SumFormula = "";
                //先算求合
                for (int j = 0; j < length; j++)
                {
                    SumFormula += "," + "R" + (startrow + i * length + j + 1) + "C" + (startcol + 1);
                }
                SumFormula = "SUM(" + SumFormula.Substring(1, SumFormula.Length - 1) + ")";
                //再写除法
                for (int k = 0; k < length; k++)
                {
                    obj_sheet.Cells[writerow + i * length + k, writecol].Formula = "R" + (startrow + i * length + k + 1) + "C" + (startcol + 1) + "/" + SumFormula;
                    obj_sheet.Cells[writerow + i * length + k, writecol].CellType = pct;
                }
            }
        }
        /// <summary>
        /// 本方法用于求某两列的比值， 结果写在第三列中 other/one,有百分比符号
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="onerow">除数行号</param>
        /// <param name="onecol">除数列号</param>
        /// <param name="anothercol">被除数列号</param>
        /// <param name="writercol">写入公式的列号</param>
        /// <param name="rowcount">共有多少行要统计</param>
        public void Sheet_WriteFormula_OneCol_Anotercol_percent(FarPoint.Win.Spread.SheetView obj_sheet, int onerow, int onecol, int anothercol, int writercol, int rowcount)
        {
            FarPoint.Win.Spread.CellType.PercentCellType pct = new FarPoint.Win.Spread.CellType.PercentCellType();
            for (int row = 0; row < rowcount; row++)
            {
                obj_sheet.Cells[onerow + row, writercol].Formula = "R" + (onerow + row + 1) + "C" + (anothercol + 1) + "/R" + (onerow + row + 1) + "C" + (onecol + 1);
                obj_sheet.Cells[onerow + row, writercol].CellType = pct;
            }
        }
        /// <summary>
        /// 本方法用于求某两列的比值， 结果写在第三列中 other/one,无百分比符号
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="onerow">除数行号</param>
        /// <param name="onecol">除数列号</param>
        /// <param name="anothercol">被除数列号</param>
        /// <param name="writercol">写入公式的列号</param>
        /// <param name="rowcount">共有多少行要统计</param>
        /// <param name="xiaoshuwei">结果保留小数位</param>
        public void Sheet_WriteFormula_OneCol_Anotercol_nopercent(FarPoint.Win.Spread.SheetView obj_sheet, int onerow, int onecol, int anothercol, int writercol, int rowcount,int xiaoshuwei)
        {
            //保留5位小数
            FarPoint.Win.Spread.CellType.NumberCellType newcelltype = new FarPoint.Win.Spread.CellType.NumberCellType();
            newcelltype.DecimalPlaces = xiaoshuwei;
            for (int row = 0; row < rowcount; row++)
            {
                obj_sheet.Cells[onerow + row, writercol].Formula = "R" + (onerow + row + 1) + "C" + (anothercol + 1) + "/R" + (onerow + row + 1) + "C" + (onecol + 1);
                obj_sheet.Cells[onerow + row, writercol].CellType = newcelltype;

            }
        }
        /// <summary>
        /// 本方法用于求比例，形如one/(two-three)
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="onerow"></param>
        /// <param name="onecol"></param>
        /// <param name="twocol"></param>
        /// <param name="threecol"></param>
        /// <param name="writecol"></param>
        /// <param name="rowcount"></param>
        public void Sheet_WriteFormula_OneCol_Twocol_Threecol_percent(FarPoint.Win.Spread.SheetView obj_sheet, int onerow, int onecol, int twocol, int threecol, int writecol, int rowcount)
        {
            FarPoint.Win.Spread.CellType.PercentCellType pct = new FarPoint.Win.Spread.CellType.PercentCellType();
            for (int row = 0; row < rowcount; row++)
            {
                obj_sheet.Cells[onerow + row, writecol].Formula = "R" + (onerow + row + 1) + "C" + (onecol + 1) + "/(R" + (onerow + row + 1) + "C" + (twocol + 1) + "-R" + (onerow + row + 1) + "C" + (threecol + 1) + ")";
                obj_sheet.Cells[onerow + row, writecol].CellType = pct;
            }

        }
        /// <summary>
        /// 本方法用于添加主表中的动态电压列标题
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="SxXjName">市辖县级名称及编号列表</param>
        /// <param name="startrow">动态列标题起始行号</param>
        /// <param name="obj_DY_List">动态电压列表</param>
        public void Sheet_AddItem_ZBonlyDY(FarPoint.Win.Spread.SheetView obj_sheet, List<string[]> SxXjName, int startrow, IList<double> obj_DY_List)
        {
            //添加列标题内容
            int dylength = obj_DY_List.Count;
            if (obj_DY_List.Count > 0)
            {
                for (int i = 0; i < SxXjName.Count; i++)
                {
                    for (int j = 0; j < obj_DY_List.Count; j++)
                    {
                        obj_sheet.SetValue(startrow + i * dylength + j, 2, obj_DY_List[j].ToString());
                    }
                    obj_sheet.AddSpanCell(startrow + i * dylength, 0, dylength, 1);
                    obj_sheet.SetValue(startrow + i * dylength, 0, SxXjName[i][0].ToString());
                    obj_sheet.AddSpanCell(startrow + i * dylength, 1, dylength, 1);
                    obj_sheet.SetValue(startrow + i * dylength, 1, SxXjName[i][1].ToString());
                }
            }

        }
        /// <summary>
        /// 本方法用于添加附表中的动态电压列标题，而且地区名是id需要转为name
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="area_key_id">areaid为key的哈希表</param>
        /// <param name="startrow">动态列标题起始行号</param>
        /// <param name="obj_DY_List">动态电压列表</param>
        /// <param name="SXareaid_List">市辖供电区的areaid列表</param>
        /// <param name="XJareaid_List">县级供电区的areaid列表</param>
        public void Sheet_AddItem_FBonlyDY(FarPoint.Win.Spread.SheetView obj_sheet, Hashtable area_key_id, int startrow, IList<double> obj_DY_List, IList<string> SXareaid_List, IList<string> XJareaid_List)
        {
            //写标题列内容
            int dylength = obj_DY_List.Count;
            if (obj_DY_List.Count > 0)
            {
                for (int i = 0; i < (2 + SXareaid_List.Count + XJareaid_List.Count); i++)
                {
                    string areaname = "";
                    if (i == 0 || i == (SXareaid_List.Count + 1))
                    {
                        areaname = "合计";
                    }
                    else
                    {
                        if (i < SXareaid_List.Count + 1)
                        {
                            if ( area_key_id[SXareaid_List[i - 1].ToString()]!=null)
                            {
                                areaname = area_key_id[SXareaid_List[i - 1].ToString()].ToString();
                            }
                            else
                            {
                                areaname = "";
                            }
                            
                        }
                        else
                        {
                            if (area_key_id[XJareaid_List[i - SXareaid_List.Count - 2].ToString()]!=null)
                            {
                                areaname = area_key_id[XJareaid_List[i - SXareaid_List.Count - 2].ToString()].ToString();
                            }
                            else
                            {
                                areaname = "";
                            }
                        }
                    }
                    for (int j = 0; j < obj_DY_List.Count; j++)
                    {
                        obj_sheet.SetValue(startrow + i * dylength + j, 2, obj_DY_List[j].ToString());
                    }
                    obj_sheet.AddSpanCell(startrow + i * dylength, 1, dylength, 1);
                    obj_sheet.SetValue(startrow + i * dylength, 1, areaname);

                }
                //写第一列数据
                obj_sheet.AddSpanCell(startrow, 0, (SXareaid_List.Count + 1) * dylength, 1);
                obj_sheet.SetValue(startrow, 0, "市辖供电区");
                obj_sheet.AddSpanCell(startrow + (SXareaid_List.Count + 1) * dylength, 0, (XJareaid_List.Count + 1) * dylength, 1);
                obj_sheet.SetValue(startrow + (SXareaid_List.Count + 1) * dylength, 0, "县级供电区");

            }
            
        }

        /// <summary>
        /// 返回字符串小括号小间的部分,仅限一对括号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FindUnits(string str)
        {
            try
            {
                if (str.Contains("(") && str.Contains(")"))
                {
                    return str.Substring(str.IndexOf("(") + 1, str.IndexOf(")") - str.IndexOf("(") - 1);
                }
                else if (str.Contains("（") && str.Contains("）"))
                {
                    return str.Substring(str.IndexOf("（") + 1, str.IndexOf("）") - str.IndexOf("（") - 1);
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception)
            {

                return string.Empty;
            }

        }
    }
    public struct Columqk
    {
        private string _colname;
        private int _CellType ;
        private int _weishu ;
        public Columqk(string a,int b,int c)
        {
            _colname = a;
            _CellType = b;
            _weishu = c;
        }
        public string colname 
        {
            get
            {
                return _colname;
            }
            set
            {
                _colname = value;
            }
        }
        public int CellType  //如果为百分比怎CellType="1" 数字为"2"
        {
            get
            {
                return _CellType;
            }
            set
            {
                _CellType = value;
            }
        }
        public int weishu       //为小数点后面的位数
        {
            get { return _weishu; }
            set { _weishu = value; }
        }
    }
}
