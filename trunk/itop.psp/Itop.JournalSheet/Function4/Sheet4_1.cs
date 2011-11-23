using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using Itop.Domain.Table;
using Itop.Client.Common;
using Itop.Domain.Forecast;
using Itop.Domain.HistoryValue;
/******************************************************************************************************
 *  ClassName：Sheet4_1
 *  Action：sheet4_1主表的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 说明： 
 * psp_Types：在这个表通过ProjectID，和Flag=2寻找地区
 * psp_values values表的Typeid=表Types的id
 * psp_Years:查年份用 
 * 
 * 时间：2010-10-11
 *********************************************************************************************************/
namespace Itop.JournalSheet.Function4
{
    class Sheet4_1
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值。
        //private int GlobalFirstYear = 0;
        //private int GlobalEndYear = 0;
        private int yearCount = 0;
        private const int flag = 1;//标志为2
        IList<PSP_Years> listYear = null;
        public PSP_Years[] py;//装入起始年分和结束年份用来判断是否减去4年（2001-2004）
        private string projectId = null;
        private IList City = null;
        private IList County = null;
        private string[] strTitle1=new string[2];

        private Itop.JournalSheet.Function.PublicFunction PF = new Itop.JournalSheet.Function.PublicFunction();
        private Itop.JournalSheet.Function10.Sheet10_1 S10_1 = new Itop.JournalSheet.Function10.Sheet10_1();
        private Itop.JournalSheet.Function4.Sheet4_1_1 S4_1_1 = new Itop.JournalSheet.Function4.Sheet4_1_1();
        private FarPoint.Win.Spread.CellType.PercentCellType PC = new FarPoint.Win.Spread.CellType.PercentCellType();

        /// <summary>
        /// 初始化存放年份的数组
        /// </summary>
        private void InitArray(int year)
        {
            int index = 0;
            py = new PSP_Years[year];
            PSP_Years listI = null;
            for (int i = 0; i < (listYear.Count); ++i)
            {
                listI = (PSP_Years)listYear[i];
                if (listI.Year < 2001 || listI.Year > 2004)
                {
                    py[index] = listI;
                    index++;
                }

            }
        }
        public void SetSheet4_1Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            strTitle1[0]="市辖供电区";
            strTitle1[1] = "县级供电区";
            projectId = FB.ProjectUID;
            PSP_Years[] listI = new PSP_Years[2];
            //SelectYear();//查询年份
            //SelectDQ(FB, strTitle1[0]);//查询地区
            //SelectDQ(FB, strTitle1[1]);
            //listI[0] = (PSP_Years)listYear[0];
            //listI[1] = (PSP_Years)listYear[listYear.Count - 1];

            //yearCount = listYear.Count - 4 + 2;//年份=程序中现实的年份-4年（2001年到2004年）+2是有两行固定标题
            yearCount = 9;
            int IntColCount =17;
            int IntRowCount = 3 * yearCount + 2 + 3;//标题占3行，类型占2行，3个供电区总共占用的行数

            obj.SheetName = Title;
            //obj.ColumnCount = 20;
            //obj.RowCount = 100;
            obj.Columns.Count = IntColCount;
            obj.Rows.Count = IntRowCount;
            IntCol = obj.Columns.Count;

            S10_1.ColReadOnly(obj, IntColCount);
            PF.Sheet_GridandCenter(obj);

            #region 合并单元格
            string strTitle = "";
            IntRow = 3;
            strTitle = Title;
            PF.CreateSheetView(obj, IntRow, IntCol, 0, 0, Title);
            PF.SetSheetViewColumnsWidth(obj, 0, Title);
            IntCol = 1;
            strTitle = " 类    型 ";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //PF.SetRowHight(obj,IntRow,IntCol,strTitle);

            strTitle = " 年    份 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //PF.SetRowHight(obj, IntRow, IntCol, strTitle);

            strTitle = "全社会最大用电负荷";
            PF.CreateSheetView(obj, NextRowMerge -= 1, NextColMerge += 1, IntRow, IntCol += 1, strTitle);
            //PF.SetRowHight(obj, IntRow, IntCol, strTitle);
            strTitle = "规模（MW）";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge -= 1, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //PF.SetRowHight(obj, IntRow, IntCol, strTitle);

            strTitle = "增长率（%）";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //PF.SetRowHight(obj, IntRow, IntCol, strTitle);

            strTitle = "网供最大负荷";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge += 1, IntRow -= 1, IntCol += 1, strTitle);
            //PF.SetRowHight(obj, IntRow, IntCol, strTitle);
            strTitle = "规模（MW）";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge -= 1, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //PF.SetRowHight(obj, IntRow, IntCol, strTitle);
            strTitle = "增长率（%）";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //PF.SetRowHight(obj, IntRow, IntCol, strTitle);


            strTitle = "供电量";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge += 1, IntRow -= 1, IntCol += 1, strTitle);
            strTitle = "规模（亿kWh）";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge -= 1, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            strTitle = "增长率（%）";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);


            strTitle = "全社会用电量";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge += 1, IntRow -= 1, IntCol += 1, strTitle);
            strTitle = "规模（亿kWh）";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge -= 1, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            strTitle = "增长率（%）";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //PF.SetRowHight(obj, IntRow, IntCol, strTitle);

            strTitle = "三产及居民用电量（亿kWh）";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge = 4, IntRow -= 1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //PF.SetRowHight(obj, IntRow, IntCol, strTitle);

            strTitle = " 一     产  ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge -= 4, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            strTitle = " 二    产 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            strTitle = " 三    产 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            strTitle = " 居    民 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            strTitle = "人均用电量（kWh/人）";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow -= 1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //PF.SetRowHight(obj, IntRow, IntCol, strTitle);

            strTitle = "人均生活用电量（kWh/人）";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //PF.SetRowHight(obj, IntRow, IntCol, strTitle);

            strTitle = "农村居民人均生活用电量（kWh/人）";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            //PF.SetRowHight(obj, IntRow, IntCol, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;
            #endregion
            IntRow = 5;
            IntCol = 1;

            //InitArray(yearCount - 2);
            //SetLeftTitle(obj, IntRow, IntCol, listI[1].Year);//左侧标题
            SetLeftTitleNew(obj, IntRow, IntCol);//左侧标题
            WriteData(obj, IntRow, IntCol);//数据
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        private void SetLeftTitle(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol, int endYear)
        {
            int indexYear = 0;
            for (int i = 0; i < 3; ++i)//分区类型
            {
                if (i == 0)
                    PF.CreateSheetView(obj, ( yearCount), NextColMerge, i + IntRow, 0,strTitle1[0] );
                if (i == 1)
                    PF.CreateSheetView(obj, (yearCount), NextColMerge, (IntRow + i * yearCount), 0, strTitle1[1]);
                if (i == 2)
                    PF.CreateSheetView(obj, (yearCount), NextColMerge, (IntRow + i * yearCount), 0, "全地区");
            }
            for (int j = 0; j < 3; ++j)//年份
            {
                for (int i = 0; i < yearCount; ++i)
                {
                    if (i == yearCount - 1)
                    {
                        indexYear = 0;
                        if (endYear == 2010)
                        {
                            PF.CreateSheetView(obj, (1), NextColMerge, (i + IntRow + j * yearCount), 1, "“十一五”年均增长率");
                        }
                        if (endYear == 2009)
                        {
                            PF.CreateSheetView(obj, (1), NextColMerge, (i + IntRow + j * yearCount), 1, "2006-2009年均增长率");
                        }
                    }
                    else if (i == yearCount - 2)
                    {
                        PF.CreateSheetView(obj, (1), NextColMerge, (i + IntRow + j * yearCount), 1, "“十五”年均增长率");
                    }
                    else
                    {
                        PF.CreateSheetView(obj, (1), NextColMerge, (i + IntRow + j * yearCount), 1, py[indexYear].Year.ToString());
                        indexYear++;
                    }
                }
            }
        }
        private void WriteData(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            double  value=0;
            
            string strType = null;
            int year = 0;
            double IntTemp=0;
            int IntTempRow = 0;
            int IntTempRow1 = 0;
            string temp = null;
            string Nextyear = null;
            //IList DQ=null;
            for(int i=IntRow;i<(obj.RowCount-yearCount);++i)//市辖,县级
            {
                strType = (string)PF.ReturnStr(obj, i, 0);
                temp = (string)PF.ReturnStr(obj, i, 1);//年份
                int.TryParse(temp, out year);
                //if(i<City.Count*yearCount)
                //{
                //    //DQ=City;
                //}
                //if(i>=City.Count*yearCount&&i<(obj.RowCount-yearCount))
                //{
                //    //DQ=County;
                //}
                for(int j=2;j<obj.ColumnCount;++j)
                {
                    //不需要写入数据的4,6,8,10,15,16列,计算出来数据
                    switch(j)
                    {
                        case 2://全社会最大用电负荷/规模（MW）
                            if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                            {

                            }else 
                            {
                                //value = SelectValue(DQ, strType, year, "全社会最大用电负荷",DQ.Count);
                                value = SelectValueNew(strType, year, "全社会最大负荷（万kW）");
                                obj.SetValue(i, j, value);
                            }
                            break;
                        case 3://全社会最大用电负荷/增长率（%）
                            if(temp=="2000")
                            {

                            }else
                            {
                                Nextyear = (string)PF.ReturnStr(obj, i-1, 1);//上一层的年份
                                if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                                {
                                    if (temp == "“十五”年均增长率")
                                    {
                                        IntTempRow=S4_1_1.ReturnRow(obj, "2000", i, 1);
                                        IntTempRow1=S4_1_1.ReturnRow(obj, "2005", i, 1);
                                        obj.Cells[i, j].Formula = "POWER(C" + (IntTempRow1+1) + "/C" + (IntTempRow+1) + ",0.2)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                    if (temp == "2006-2009年均增长率")
                                    {
                                        IntTempRow = S4_1_1.ReturnRow(obj, "2006", i, 1);
                                        IntTempRow1 = S4_1_1.ReturnRow(obj, "2009", i, 1);
                                        obj.Cells[i, j].Formula = "POWER(C" + (1+IntTempRow1) + "/C" + (IntTempRow+1) + ",1/3)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                    if (temp == "“十一五”年均增长率")
                                    {
                                        IntTempRow = S4_1_1.ReturnRow(obj, "2005", i, 1);
                                        IntTempRow1 = S4_1_1.ReturnRow(obj, "2010", i, 1);
                                        obj.Cells[i, j].Formula = "POWER(C" + (IntTempRow1+1) + "/C" + (IntTempRow+1) + ",1/5)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                }
                                else
                                {
                                    IntTemp=S4_1_1.ReturnYearDifference(temp, Nextyear);
                                    IntTemp=1 / IntTemp;
                                    obj.Cells[i, j].Formula = "POWER(C" + (i+1) + "/C" + (i ) + "," + IntTemp + ")-1";
                                    obj.Cells[i, j].CellType = PC;//%
                                }
                            }
                            break;
                        case 4://网供最大负荷/规模（MW）
                            if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                            {

                            }
                            else
                            {
                                //value = SelectValue(DQ, strType, year, "网供最大负荷", DQ.Count);
                                value = SelectValueNew(strType, year, "网供最大负荷");
                                obj.SetValue(i, j, value);
                            }
                            break;
                        case 5://网供最大负荷/增长率（%）
                            if (temp == "2000")
                            {

                            }
                            else
                            {
                                Nextyear = (string)PF.ReturnStr(obj, i - 1, 1);//上一层的年份
                                if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                                {
                                    if (temp == "“十五”年均增长率")
                                    {
                                        IntTempRow = S4_1_1.ReturnRow(obj, "2000", i, 1);
                                        IntTempRow1 =S4_1_1. ReturnRow(obj, "2005", i, 1);
                                        obj.Cells[i, j].Formula = "POWER(E" + (IntTempRow1 + 1) + "/E" + (IntTempRow + 1) + ",0.2)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                    if (temp == "2006-2009年均增长率")
                                    {
                                        IntTempRow = S4_1_1.ReturnRow(obj, "2006", i, 1);
                                        IntTempRow1 =S4_1_1. ReturnRow(obj, "2009", i, 1);
                                        obj.Cells[i, j].Formula = "POWER(E" + (1 + IntTempRow1) + "/E" + (IntTempRow + 1) + ",1/3)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                    if (temp == "“十一五”年均增长率")
                                    {
                                        IntTempRow = S4_1_1.ReturnRow(obj, "2005", i, 1);
                                        IntTempRow1 =S4_1_1. ReturnRow(obj, "2010", i, 1);
                                        obj.Cells[i, j].Formula = "POWER(E" + (IntTempRow1 + 1) + "/E" + (IntTempRow + 1) + ",1/5)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                }
                                else
                                {
                                    IntTemp = S4_1_1.ReturnYearDifference(temp, Nextyear);
                                    IntTemp = 1 / IntTemp;
                                    obj.Cells[i, j].Formula = "POWER(E" + (i + 1) + "/E" + (i) + "," + IntTemp + ")-1";
                                    obj.Cells[i, j].CellType = PC;//%
                                }
                            }
                            break;
                        case 6://供电量/规模（MW）
                            if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                            {

                            }
                            else
                            {
                                //value = SelectValue(DQ, strType, year, "供电量", DQ.Count);
                                value = SelectValueNew( strType, year, "全社会供电量（亿kWh）");
                                obj.SetValue(i, j, value);
                            }
                            break;
                        case 7://供电量/增长率（%）
                            if (temp == "2000")
                            {

                            }
                            else
                            {
                                Nextyear = (string)PF.ReturnStr(obj, i - 1, 1);//上一层的年份
                                if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                                {
                                    if (temp == "“十五”年均增长率")
                                    {
                                        IntTempRow = S4_1_1.ReturnRow(obj, "2000", i, 1);
                                        IntTempRow1 = S4_1_1.ReturnRow(obj, "2005", i, 1);
                                        obj.Cells[i, j].Formula = "POWER(G" + (IntTempRow1 + 1) + "/G" + (IntTempRow + 1) + ",0.2)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                    if (temp == "2006-2009年均增长率")
                                    {
                                        IntTempRow = S4_1_1.ReturnRow(obj, "2006", i, 1);
                                        IntTempRow1 = S4_1_1.ReturnRow(obj, "2009", i, 1);
                                        obj.Cells[i, j].Formula = "POWER(G" + (1 + IntTempRow1) + "/G" + (IntTempRow + 1) + ",1/3)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                    if (temp == "“十一五”年均增长率")
                                    {
                                        IntTempRow = S4_1_1.ReturnRow(obj, "2005", i, 1);
                                        IntTempRow1 = S4_1_1.ReturnRow(obj, "2010", i, 1);
                                        obj.Cells[i, j].Formula = "POWER(G" + (IntTempRow1 + 1) + "/G" + (IntTempRow + 1) + ",1/5)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                }
                                else
                                {
                                    IntTemp = S4_1_1.ReturnYearDifference(temp, Nextyear);
                                    IntTemp = 1 / IntTemp;
                                    obj.Cells[i, j].Formula = "POWER(G" + (i + 1) + "/G" + (i) + "," + IntTemp + ")-1";
                                    obj.Cells[i, j].CellType = PC;//%
                                }
                            }
                            break;
                        case 8://全社会用电量/规模（MW）
                            if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                            {

                            }
                            else
                            {
                                obj.Cells[i,j].Formula="SUM(K"+(i+1)+":N"+(i+1)+")";
                            }
                            break;
                        case 9://全社会用电量/增长率（%）
                            if (temp == "2000")
                            {

                            }
                            else
                            {
                                Nextyear = (string)PF.ReturnStr(obj, i - 1, 1);//上一层的年份
                                if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                                {
                                    if (temp == "“十五”年均增长率")
                                    {
                                        IntTempRow = S4_1_1.ReturnRow(obj, "2000", i, 1);
                                        IntTempRow1 = S4_1_1.ReturnRow(obj, "2005", i, 1);
                                        obj.Cells[i, j].Formula = "POWER(I" + (IntTempRow1 + 1) + "/I" + (IntTempRow + 1) + ",0.2)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                    if (temp == "2006-2009年均增长率")
                                    {
                                        IntTempRow = S4_1_1.ReturnRow(obj, "2006", i, 1);
                                        IntTempRow1 = S4_1_1.ReturnRow(obj, "2009", i, 1);
                                        obj.Cells[i, j].Formula = "POWER(I" + (1 + IntTempRow1) + "/I" + (IntTempRow + 1) + ",1/3)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                    if (temp == "“十一五”年均增长率")
                                    {
                                        IntTempRow = S4_1_1.ReturnRow(obj, "2005", i, 1);
                                        IntTempRow1 = S4_1_1.ReturnRow(obj, "2010", i, 1);
                                        obj.Cells[i, j].Formula = "POWER(I" + (IntTempRow1 + 1) + "/I" + (IntTempRow + 1) + ",1/5)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                }
                                else
                                {
                                    IntTemp = S4_1_1.ReturnYearDifference(temp, Nextyear);
                                    IntTemp = 1 / IntTemp;
                                    obj.Cells[i, j].Formula = "POWER(I" + (i + 1) + "/I" + (i) + "," + IntTemp + ")-1";
                                    obj.Cells[i, j].CellType = PC;//%
                                }
                            } break;
                        case 10://三产及居民用电量（亿kWh)/一产
                            if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                            {

                            }
                            else
                            {
                                //value = SelectValue(DQ, strType, year, "一产", DQ.Count);
                                value = SelectValueNew(strType, year, "一产");
                                obj.SetValue(i, j, value);
                            }
                            break;
                        case 11://三产及居民用电量（亿kWh）/二产
                            if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                            {

                            }
                            else
                            {
                                //value = SelectValue(DQ, strType, year, "二产", DQ.Count);
                                value = SelectValueNew( strType, year, "二产");
                                obj.SetValue(i, j, value);
                            }
                            break;
                        case 12://三产及居民用电量（亿kWh）	/三产
                            if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                            {

                            }
                            else
                            {
                                //value = SelectValue(DQ, strType, year, "三产", DQ.Count);
                                value = SelectValueNew(strType, year, "三产");
                                obj.SetValue(i, j, value);
                            }
                            break;
                        case 13://三产及居民用电量（亿kWh）	/居民
                            if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                            {

                            }
                            else
                            {
                                //value = SelectValue(DQ, strType, year, "居民", DQ.Count);
                                value = SelectValueNew( strType, year, "居民用电量");
                                obj.SetValue(i, j, value);
                            }
                            break;
                        case 14://人均用电量（kWh/人）
                            if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                            {

                            }
                            else
                            {

                                obj.Cells[i, j].Formula = "I"+(i+1)+"*10000/"+S4_1_1.ReturnPopulation(temp,"城镇人口(万人)","1",this.projectId);
                            }
                            break;
                        case 15://人均生活用电量（kWh/人）
                            if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                            {

                            }
                            else
                            {
                                obj.Cells[i, j].Formula = "N" + (i + 1) + "*10000/" + S4_1_1.ReturnPopulation(temp, "城镇人口(万人)", "1", this.projectId);
                            }
                            break;
                        case 16://农村居民人均生活用电量（kWh/人）
                            if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                            {

                            }
                            else
                            {
                                //value = SelectValue(DQ, strType, year, "农村居民人均生活用电量（kWh/人）", DQ.Count);
                                value = SelectValueNew( strType, year, "农村居民人均生活用电量（kWh/人）");
                                obj.SetValue(i, j, value);
                            }
                            break;
                        default:
                            break;
                    }                   
                }
            }
                for (int i = (obj.RowCount - yearCount); i < obj.RowCount; ++i)//全地区
                {
                    for (int j = 2; j < obj.ColumnCount; ++j)
                    {
                        switch(j)
                        {
                            case 2:
                                obj.Cells[i, j].Formula = "C"+(i-yearCount+1)+"+C"+(i-2*yearCount+1);
                                break;
                            case 3:
                                obj.Cells[i, j].Formula = "D" + (i - yearCount + 1) + "+D" + (i - 2 * yearCount + 1);
                                obj.Cells[i, j].CellType = PC;//%
                                break;
                            case 4:
                                obj.Cells[i, j].Formula = "E" + (i - yearCount + 1) + "+E" + (i - 2 * yearCount + 1);
                                break;
                            case 5:
                                obj.Cells[i, j].Formula = "F" + (i - yearCount + 1) + "+F" + (i - 2 * yearCount + 1);
                                obj.Cells[i, j].CellType = PC;//%
                                break;
                            case 6:
                                obj.Cells[i, j].Formula = "G" + (i - yearCount + 1) + "+G" + (i - 2 * yearCount + 1);
                                break;
                            case 7:
                                obj.Cells[i, j].Formula = "H" + (i - yearCount + 1) + "+H" + (i - 2 * yearCount + 1);
                                obj.Cells[i, j].CellType = PC;//%
                                break;
                            case 8:
                                obj.Cells[i, j].Formula = "I" + (i - yearCount + 1) + "+I" + (i - 2 * yearCount + 1);
                                break;
                            case 9:
                                obj.Cells[i, j].Formula = "J" + (i - yearCount + 1) + "+J" + (i - 2 * yearCount + 1);
                                obj.Cells[i, j].CellType = PC;//%
                                break;
                            case 10:
                                obj.Cells[i, j].Formula = "K" + (i - yearCount + 1) + "+K" + (i - 2 * yearCount + 1);
                                break;
                            case 11:
                                obj.Cells[i, j].Formula = "L" + (i - yearCount + 1) + "+L" + (i - 2 * yearCount + 1);
                                break;
                            case 12:
                                obj.Cells[i, j].Formula = "M" + (i - yearCount + 1) + "+M" + (i - 2 * yearCount + 1);
                                break;
                            case 13:
                                obj.Cells[i, j].Formula = "N" + (i - yearCount + 1) + "+N" + (i - 2 * yearCount + 1);
                                break;
                            case 14:
                                obj.Cells[i, j].Formula = "O" + (i - yearCount + 1) + "+O" + (i - 2 * yearCount + 1);
                                break;
                            case 15:
                                obj.Cells[i, j].Formula = "P" + (i - yearCount + 1) + "+P" + (i - 2 * yearCount + 1);
                                break;
                            case 16:
                                obj.Cells[i, j].Formula = "Q" + (i - yearCount + 1) + "+Q" + (i - 2 * yearCount + 1);
                                break;

                            default:
                                break;
                        }
                    }
                }
           
        }
        /// <summary>
        /// 查询包含从那年开始到那年结束
        /// </summary>
        private void SelectYear()
        {
            string con = flag.ToString();//flag标志是2的年份
            listYear = Services.BaseService.GetList<PSP_Years>("SelectPSP_YearsListByFlag", con);

        }
        /// <summary>
        /// 查询市辖供电区和县级供电区有几个分区
        /// 由于市辖供电区和县级供电区是一级目录要记住它们的id号
        /// </summary>
        private void SelectDQ(Itop.Client.Base.FormBase FB, string strTitle)
        {
            string con = null;
            projectId = FB.ProjectUID;
            con = "ParentID=(select ID from psp_types where Title='" + strTitle + "' and projectID='" + projectId + "') group by Title";
            try
            {
                if (strTitle == "市辖供电区")
                {
                    City = (IList)Services.BaseService.GetList<string>("SelectPSP_TypesDQByWhere", con);
                }
                if (strTitle == "县级供电区")
                {
                    County = (IList)Services.BaseService.GetList<string>("SelectPSP_TypesDQByWhere", con);
                }
            }
            catch (System.Exception e)
            {
                //MessageBox.Show("没有" + strTitle + "的数据！！！");
            }
        }
        /// <summary>
        /// 返回具体的值
        /// </summary>
        /// <returns></returns>
        /// <param name="DQ">分区名称</param>
        /// <param name="strType">分区类型</param>
        /// <param name="year">年份</param>
        /// <param name="strTitle">列标题</param>
        /// <param name="DQCount">地区的数量</param>
        private double SelectValue(IList DQ, string strType, int year, string strTitle,int DQCount)
        {
            double value = 0;
            string con = null;
            for (int i = 0; i < DQCount; ++i)
            {
                if (strTitle == "一产" || strTitle == "二产" || strTitle == "三产" || strTitle == "居民")
                {
                    con = "year='" + year + "'and TypeID=(select Id from psp_Types where Title='" + strTitle + "' and  Flag2='" + flag + "'" +
                    "and projectID='" + projectId + "' and parentID= (select Id from psp_Types where Title='三产及居民用电量（亿kWh）'and  Flag2='" + flag + "'" +
                    "and projectID='" + projectId + "' and parentID=(select ID from psp_Types where Title='" + DQ[i] + "'and  Flag2='" + flag + "'and " +
                    "projectID='" + projectId + "' and parentID=(select ID from psp_Types where Title='" + strType + "'" +
                    "and  Flag2='" + flag + "'and projectID='" + projectId + "'))));";
                }
                else
                {
                    con = "year='" + year + "'and TypeID=(select Id from psp_Types where Title='" + strTitle + "'and  Flag2='" + flag + "'" +
                    "and projectID='" + projectId + "' and parentID=(select ID from psp_Types where Title='" + DQ[i] + "'and  Flag2='" + flag + "'and " +
                    "projectID='" + projectId + "' and parentID=(select ID from psp_Types where Title='" + strType + "'" +
                    "and  Flag2='" + flag + "'and projectID='" + projectId + "')));";
                }
                try
                {
                    value+= (double)Services.BaseService.GetObject("SelectPSP_ValuesOfValueByWhere", con);

                }
                catch (System.Exception e)
                {
                    //MessageBox.Show(e.Message);
                    value = 10;
                }
            }
            return value;
        }
        /// <summary>
        /// 新用的方法不分区
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="year"></param>
        /// <param name="strTitle"></param>
        /// <returns></returns>
        private double SelectValueNew(string strType, int year, string strTitle)
        {
            double value = 0.00;
            string sql = "";
            string strYear="y"+year;
            if (strTitle == "一产" || strTitle == "二产" || strTitle == "三产" || strTitle == "居民用电量")
            {
                sql = "select "+strYear+" from ps_history where title= '"+strType +"' and parentId=(select ID from ps_history where  title='"+strTitle+"'"
                           +" and parentId=(select id from ps_history where"
                            +" Col4='"+projectId +"'AND Forecast='"+flag+"'"
                           +" and title='全社会用电量（亿kWh）'))";
            }
            else
            {
                sql = "select "+strYear+" from ps_history where  title='"+strType+"'and parentId=( select id from ps_history "
                +" where Col4='"+projectId+"'AND Forecast='"+flag+"'"
                        +" and title='"+strTitle+"')";
            }
            try
            {
                value =(double) Services.BaseService.GetObject("SelectPs_HistoryPopulationByCondition", sql);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return value;
        }
        /// <summary>
        /// 写左侧标题最新
        /// </summary>
        private void SetLeftTitleNew(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            int indexYear = 2005;
            int endYear = 2010;
            for (int i = 0; i < 3; ++i)//分区类型
            {
                if (i == 0)
                    PF.CreateSheetView(obj, (yearCount), NextColMerge, i + IntRow, 0, strTitle1[0]);
                if (i == 1)
                    PF.CreateSheetView(obj, (yearCount), NextColMerge, (IntRow + i * yearCount), 0, strTitle1[1]);
                if (i == 2)
                    PF.CreateSheetView(obj, (yearCount), NextColMerge, (IntRow + i * yearCount), 0, "全地区");
            }
            for (int j = 0; j < 3; ++j)//年份
            {
                indexYear = 2005;
                for (int i = 0; i < yearCount; ++i)//8行数据
                {
                    if (i == yearCount - 1)
                    {
                        indexYear = 0;
                        if (endYear == 2010)
                        {
                            PF.CreateSheetView(obj, (1), NextColMerge, (i + IntRow + j * yearCount), 1, "“十一五”年均增长率");
                        }
                        if (endYear == 2009)
                        {
                            PF.CreateSheetView(obj, (1), NextColMerge, (i + IntRow + j * yearCount), 1, "2006-2009年均增长率");
                        }
                    }
                    else if (i == yearCount - 2)
                    {
                        PF.CreateSheetView(obj, (1), NextColMerge, (i + IntRow + j * yearCount), 1, "“十五”年均增长率");
                    }
                    else
                    {
                        if(i==0)
                        {
                            PF.CreateSheetView(obj, (1), NextColMerge, (i + IntRow + j * yearCount), 1, "2000");
                        }
                        else
                        {
                            PF.CreateSheetView(obj, (1), NextColMerge, (i + IntRow + j * yearCount), 1, indexYear.ToString());
                            indexYear++;
                        }
                    }
                }
            }
        }
    }
}
