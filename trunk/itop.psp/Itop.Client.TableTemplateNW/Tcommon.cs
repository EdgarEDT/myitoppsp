using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.Win;
using System.Drawing;
using System.Collections;
using System.IO;
using FarPoint.Win;
using System.Windows.Forms;
using Itop.Client;
using Itop.Domain.Table;
namespace Itop.Client.TableTemplateNW
{
    class Tcommon
    {
        #region 公用变量
        /// <summary>
        /// 规划城市名称
        /// 在User.ini文件内的CityName后
        /// </summary>
        public static string CityName
        {
            get
            {
                string cityname = "";
                FileINI User_Ini = new FileINI(Application.StartupPath + "\\User.ini");
                if (File.Exists(Application.StartupPath + "\\User.ini"))
                {
                    User_Ini = new FileINI(Application.StartupPath + "\\User.ini");
                    cityname = User_Ini.ReadValue("Setting", "CityName");
                    if (cityname == "")
                    {
                        User_Ini.Writue("Setting", "CityName", "城市名称");
                        cityname = User_Ini.ReadValue("Setting", "CityName");
                    }
                }
                return cityname;
            }
        }

        /// <summary>
        /// 当前卷编号
        /// </summary>
        public static string ProjectID
        {
            get
            {

                return Itop.Client.MIS.ProgUID;
            }
        }
        public static int CurrentYear
        {
            get
            {
                return DateTime.Now.Year;
            }
        }
        /// <summary>
        /// 文件夹名
        /// </summary>
        public static string ExcelDir = "xls";
        /// <summary>
        /// 程序路径(已加\)
        /// </summary>
        public static string CurrentPath
        {
            get
            {
                return System.Windows.Forms.Application.StartupPath + "\\";
            }
        }
        /// <summary>
        /// 文件路径名(已加\)
        /// </summary>
        public static string ExcelFilePath
        {
            get
            {
                return System.Windows.Forms.Application.StartupPath + "\\" + Tcommon.ExcelDir + "\\";
            }
        }
        #endregion
        #region 阿拉伯数字转中文
        private string[] que = new string[60] { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", 
            "十一","十二","十三","十四","十五","十六","十七","十八","十九","二十","二十一","二十二","二十三","二十四","二十五","二十六","二十七",
            "二十八","二十九","三十","三十一","三十二","三十三","三十四","三十五","三十六","三十七","三十八","三十九","四十","四十一","四十二","四十三","四十四",
            "四十五","四十六","四十七","四十八","四十九","五十","五十一","五十二","五十三","五十四","五十五","五十六","五十七","五十八","五十九","六十"};

        /// <summary>
        /// 将阿拉伯数字转为中文大写一、二...
        /// </summary>
        /// <param name="number">数字(0--60)</param>
        /// <returns>反回转换后的中文数字</returns>
        public string CHNumberToChar(int number)
        {
            if (number <= 10 && number > 0)
            {
                return que[number - 1];
            }
            else
            {
                return "超出范围";
            }
        }

        #endregion
        #region 计算几何增长率
        /// <summary>
        /// 计算几何增长率 (lastdouble/basedouble)^1/num-1
        /// </summary>
        /// <param name="basedouble"></param>
        /// <param name="lastdouble"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public double AverageIncreasing(double basedouble, double lastdouble, int num)
        {
            double db = Math.Pow(lastdouble / basedouble, 1.0 / num) - 1;
            if (db.ToString() == "非数字" || db.ToString() == "正无穷大")
                db = 0;
            return db;

        }
        #endregion
        #region 表标题及年份
        /// <summary>
        /// 根据表标识返回表标题名
        /// </summary>
        /// <param name="TableID">表标识</param>
        /// <returns>字符</returns>
        public string GetTableTitle(string TableID)
        {
            string con = " TableID='" + TableID + "' and ProjectID='" + Tcommon.ProjectID + "'";
            IList<Ps_Table_Report> templist = Common.Services.BaseService.GetList<Ps_Table_Report>("SelectPs_Table_ReportListByConn", con);
            if (templist.Count > 0)
            {
                return templist[0].TableNewName;
            }
            else
            {
                return "暂无表名，请到报表信息管理处登记！";
            }

        }
        public int[] getRowList(string str)
        {
            string[] id = str.Split(",".ToCharArray());
            int[] list = new int[id.Length - 1];
            for (int i = 0; i < id.Length - 1; i++)
            {
                if (id[i] != "")
                {
                    list[i] = Convert.ToInt32(id[i]);
                }
            }
            return list;
        }
        /// <summary>
        /// 根据表标识返回表年份
        /// </summary>
        /// <param name="TableID">表标识</param>
        /// <returns>整数数组</returns>
        public int[] GetTableYears(string TableID)
        {
            int[] intary = null;
            string con = " TableID='" + TableID + "' and ProjectID='" + Tcommon.ProjectID + "'";
            IList<Ps_Table_Report> templist = Common.Services.BaseService.GetList<Ps_Table_Report>("SelectPs_Table_ReportListByConn", con);
            if (templist.Count > 0)
            {

                string[] ary = templist[0].Years.Split('#');
                intary = new int[ary.Length];
                for (int i = 0; i < ary.Length; i++)
                {
                    intary[i] = Convert.ToInt32(ary[i]);
                }
            }
            else
            {
                intary = new int[1];
                intary[0] = CurrentYear;
            }
            return intary;

        }
        #endregion
        #region 处理txt文件
        private string rn = "\r\n";
        private static int QuestionNO = 0;
        private static string fileName = "Question" + DateTime.Now.ToShortDateString() + ".txt";
        /// <summary>
        /// 记录问题到txt 文档，以备结束后查看
        /// </summary>
        /// <param name="TableNO">表编号</param>
        /// <param name="QuestionDes">问题描述</param>
        /// <param name="Reason">可能原因</param>
        /// <param name="Remark">备注</param>
        public void WriteQuestion(string TableNO, string QuestionDes, string Reason, string Remark)
        {
            if (Tcommon.QuestionNO == 0)
            {
                Del_QuestionTxt();
            }
            Tcommon.QuestionNO += 1;

            string tempno = "";
            if (Tcommon.QuestionNO > 0 && Tcommon.QuestionNO < 10)
            {
                tempno = "00000" + Tcommon.QuestionNO;
            }
            else if (Tcommon.QuestionNO >= 10 && Tcommon.QuestionNO < 100)
            {
                tempno = "0000" + Tcommon.QuestionNO;
            }
            else if (Tcommon.QuestionNO >= 100 && Tcommon.QuestionNO < 1000)
            {
                tempno = "000" + Tcommon.QuestionNO;
            }
            else if (Tcommon.QuestionNO >= 1000 && Tcommon.QuestionNO < 10000)
            {
                tempno = "00" + Tcommon.QuestionNO;
            }
            else if (Tcommon.QuestionNO >= 10000 && Tcommon.QuestionNO < 100000)
            {
                tempno = "0" + Tcommon.QuestionNO;
            }
            else
            {
                tempno = Tcommon.QuestionNO.ToString();
            }
            string tempstr = tempno + "      " + TableNO + "       " + QuestionDes + "       " + Reason + "    " + Remark + "  " + rn;
            string txtrar = File.ReadAllText(Tcommon.fileName);
            tempstr = txtrar + tempstr;
            StreamWriter generateTxt = new StreamWriter(Tcommon.fileName, false);
            //写入值
            generateTxt.Write(tempstr);
            //关闭
            generateTxt.Close();
        }
        private void Del_QuestionTxt()
        {
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + Tcommon.fileName))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + Tcommon.fileName);
            }
            try
            {
                StreamWriter generateTxt = new StreamWriter(Tcommon.fileName, false);
                //写入值
                generateTxt.Write("问题清单" + rn);
                //关闭
                generateTxt.Close();
            }
            catch (Exception ex)
            {
                Tcommon.fileName = "Question" + DateTime.Now.ToShortDateString() + DateTime.Now.Second.ToString() + ".txt";
                StreamWriter generateTxt = new StreamWriter(Tcommon.fileName, false);
                //写入值
                generateTxt.Write("问题清单" + rn);
                //关闭
                generateTxt.Close();

            }
        }
        /// <summary>
        /// 显示文题
        /// </summary>
        public void Show_Question()
        {
            if (MessageBox.Show("是否查看报表可能问题文档？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\" + Tcommon.fileName);
        }
        #endregion
        #region farpoint控制

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
        /// 本方法用来设定单元格格式
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="row">行</param>
        /// <param name="col">列</param>
        /// <param name="Type">类行1为number，2为Prsent</param>
        /// <param name="DecimalPlaces"></param>
        public void SetSheetCellType(FarPoint.Win.Spread.SheetView obj_sheet, int row, int col, int Type, int DecimalPlaces)
        {

            FarPoint.Win.Spread.CellType.PercentCellType percelltype = new FarPoint.Win.Spread.CellType.PercentCellType();
            percelltype.DecimalPlaces = DecimalPlaces;
            FarPoint.Win.Spread.CellType.NumberCellType numcelltype = new FarPoint.Win.Spread.CellType.NumberCellType();
            numcelltype.DecimalPlaces = DecimalPlaces;
            if (Type == 1)
            {
                obj_sheet.Cells[row, col].CellType = numcelltype;
            }
            else if (Type == 2)
            {
                obj_sheet.Cells[row, col].CellType = percelltype;
            }
        }
        /// <summary>
        /// 本方法用于写入几何平均增长率 (lastColnum/baseColnum)^1/num-1
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="baseColnum">基数列</param>
        /// <param name="lastColnum">最终列</param>
        /// <param name="num">平均数长（多少列）</param>
        /// <param name="writerow">写入行</param>
        /// <param name="writecol">写入列</param>
        /// <param name="RowCount">统计行</param>
        /// <param name="DecimalPlaces">小数位数</param>
        /// <param name="Present">是否显示百分比类形</param>
        public void Sheet_WriteFormula_RowAveInsering(FarPoint.Win.Spread.SheetView obj_sheet, int baseColnum, int lastColnum, int num, int writerow, int writecol, int RowCount, int DecimalPlaces, bool Present)
        {

            FarPoint.Win.Spread.CellType.PercentCellType percelltype = new FarPoint.Win.Spread.CellType.PercentCellType();
            percelltype.DecimalPlaces = DecimalPlaces;
            FarPoint.Win.Spread.CellType.NumberCellType numcelltype = new FarPoint.Win.Spread.CellType.NumberCellType();
            numcelltype.DecimalPlaces = DecimalPlaces;
            for (int i = 0; i < RowCount; i++)
            {
                obj_sheet.Cells[writerow + i, writecol].Formula = " Power(R" + (writerow + i + 1) + "C" + (1 + lastColnum) + "/R" + (writerow + i + 1) + "C" + (baseColnum + 1) + "," + (1.000 / num) + ")-1";
                if (Present)
                {
                    obj_sheet.Cells[writerow + i, writecol].CellType = percelltype;
                }
                else
                {
                    obj_sheet.Cells[writerow + i, writecol].CellType = numcelltype;
                }
            }
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
        /// 本方法用于写入行求和公式(统计部分)
        /// 1   a
        ///     b
        ///     c
        ///     d
        /// 2   a
        ///     b
        ///     c
        ///     d
        ///总  a
        ///     b
        ///     c
        ///     d
        /// itemcount=2
        /// itemlenth=3
        /// 
        /// 如要统计c 项 和d 项
        /// 则
        /// startrownum=2
        /// willrowcount=2
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="startrow">要求和部分起始行号</param>
        /// <param name="startcol">要求和部分起始列号</param>
        /// <param name="itemcount">要求和部分的项目数</param>
        /// <param name="itemlenth">每个项目行数</param>
        /// <param name="writerow">公式写入开始行</param>
        /// <param name="writecol">公式写入开始列</param>
        /// <param name="startrownum">统计开始行在项目中的位置（从0开始）</param>
        /// <param name="willrowcount">将要统计的行数（小于项目行数）</param>
        /// <param name="countcol">统计列数</param
        public void Sheet_WriteFormula_RowSum2(FarPoint.Win.Spread.SheetView obj_sheet, int startrow, int startcol, int itemcount, int itemlenth, int writerow, int writecol, int startrownum, int willrowcount, int countcol)
        {

            for (int col = 0; col < countcol; col++)
            {
                for (int n = 0; n < itemlenth; n++)
                {
                    if (n < startrownum || n > startrownum + willrowcount)
                    {

                    }
                    else
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

        }
        /// <summary>
        /// 本方法用于写入指定行求和公式
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="rownum">存放行号的数组</param>
        /// <param name="startcol">起始列</param>
        /// <param name="writerow">写入公式行号</param>
        /// <param name="writecol">写入公式列号</param>
        /// <param name="countcol">统计列数</param>
        public void Sheet_WriteFormula_RowSum3(FarPoint.Win.Spread.SheetView obj_sheet, int[] rownum, int startcol, int writerow, int writecol, int countcol)
        {

            for (int col = 0; col < countcol; col++)
            {
                string SumFormula = "";
                for (int m = 0; m < rownum.Length; m++)
                {
                    SumFormula += "," + "R" + (rownum[m] + 1) + "C" + (startcol + col + 1);
                }
                SumFormula = "SUM(" + SumFormula.Substring(1, SumFormula.Length - 1) + ")";
                obj_sheet.Cells[writerow, writecol + col].Formula = SumFormula;


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
        /// 本方法主要用于指定列求和统计写入公式
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="startrow"></param>
        /// <param name="colnum">列数组</param>
        /// <param name="countrow"></param>
        /// <param name="writecol"></param>
        public void Sheet_WriteFormula_ColSum_WritOne2(FarPoint.Win.Spread.SheetView obj_sheet, int startrow, int[] colnum, int countrow,  int writecol)
        {
            for (int row = 0; row < countrow; row++)
            {
                string SumFormula = "";
                for (int col = 0; col < colnum.Length; col++)
                {
                    SumFormula += "," + "R" + (startrow + row + 1) + "C" + (colnum[col]  + 1);
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
        /// 本方法用于求某两行的比值， 结果写在第三行中 other/one,无百分比符号
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="onerow"></param>
        /// <param name="onecol"></param>
        /// <param name="anotherrow"></param>
        /// <param name="writerRow"></param>
        /// <param name="colcount"></param>
        public void Sheet_WriteFormula_OneRow_AnoterRow_nopercent(FarPoint.Win.Spread.SheetView obj_sheet, int onerow, int onecol, int anotherrow, int writerRow, int colcount)
        {
            FarPoint.Win.Spread.CellType.NumberCellType pct = new FarPoint.Win.Spread.CellType.NumberCellType();
            pct.DecimalPlaces = 2;
            for (int col = 0; col < colcount; col++)
            {
                obj_sheet.Cells[writerRow, onecol + col].Formula = "R" + (anotherrow + 1) + "C" + (onecol + col + 1) + "/R" + (onerow + 1) + "C" + (onecol + col + 1);

                obj_sheet.Cells[writerRow, onecol + col].CellType = pct;
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
        public void Sheet_WriteFormula_OneCol_Anotercol_nopercent(FarPoint.Win.Spread.SheetView obj_sheet, int onerow, int onecol, int anothercol, int writercol, int rowcount, int xiaoshuwei)
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

        #endregion farpoint控制

    }
}
