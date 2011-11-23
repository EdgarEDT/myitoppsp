#region 引用部分
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Drawing;

using Itop.Client.Base;
using Itop.Domain.Table;
using Itop.Client.Common;
using Itop.Domain.Forecast;
using Itop.Common;
using Itop.Domain.Layouts;

using Microsoft.Office.Interop.Excel;
using DevExpress.Utils;
using FarPoint.Win;
#endregion
/******************************************************************************************************
 *  ClassName：PublicFunction
 *  Action：用来生成各种报表的公共调用方法,sheet4_1报表的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个是公共函数类所有重用函数都在这里供别的表使用，这个类里有几个函数是写表4_1但是现在不用了
 *              
 * 时间：2010-10-11
 *********************************************************************************************************/
namespace Itop.JournalSheet.Function
{
    public  class PublicFunction
    {
        private int GlobalFirstYear = 0;
        private int GlobalEndYear = 0;
        private int  IntType = 1;
        private  string TwoParentID = null;
        private string ThreeParentID = null;
        private byte[] by1 = null;
        private byte[] bts = null;

        private const int FixationCol = 2;//年份下面有两个固定列，在这里设初始值为以后改变方便
        private FarPoint.Win.Spread.CellHorizontalAlignment HAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        private FarPoint.Win.Spread.CellVerticalAlignment VAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
        private Dictionary<string, Ps_History> resualt = new Dictionary<string, Ps_History>();
        private Itop.Client.Base.FormBase GlobalFormBase = null;

        //private DataTable dataTable = new DataTable();

        public int GolobalYearCount = 0;
        public int GolobalYears = 0;//记录有几个年份没有数据
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="obj">传入对象</param>
        /// <param name="RowStep">要合并单元格的数量行</param>
        /// <param name="ColStep">要合并单元格的数量列</param>
        /// <param name="Row">行</param>
        /// <param name="Col">列</param>
        /// <param name="Title">标题</param>
        public void Merger_Grid(FarPoint.Win.Spread.FpSpread obj, int RowStep, int ColStep, int Row, int Col, string Title)
        {
            FarPoint.Win.Spread.Cell acell;
            acell = obj.ActiveSheet.Cells[Row, Col];
            acell.ColumnSpan = ColStep;
            acell.RowSpan = RowStep;
            acell.Text = Title;

            acell.HorizontalAlignment = HAlignment;
            acell.VerticalAlignment = VAlignment;
        }
        /// <summary>
        /// 创建sheet
        /// </summary>
        /// <param name="form">要穿入的窗口</param>
        /// <param name="obj">fpspread对象</param>
        /// <param name="fpSpread1_Sheet3">要创建的对象名</param>
        /// <param name="Title">名称</param>
        private FarPoint.Win.Spread.SheetView CreateFpSpread(System.Windows.Forms.Form form, FarPoint.Win.Spread.FpSpread obj, FarPoint.Win.Spread.SheetView fpSpread1_Sheet3, string Title)
        {
            fpSpread1_Sheet3 = new FarPoint.Win.Spread.SheetView();
            fpSpread1_Sheet3.SheetName = Title;
            form.Controls.Add(obj);
            obj.Sheets.Add(fpSpread1_Sheet3);
            return fpSpread1_Sheet3;
        }
        /// <summary>
        /// 设置单元格列的宽度
        /// </summary>
        /// <param name="obj">fpstreap对象</param>
        /// <param name="Col">哪个列</param>
        /// <param name="Title">名字</param>
        public void SetColumnsWidth(FarPoint.Win.Spread.FpSpread obj, int Col, string Title)
        {
            int len = 0;
            const int Pixe = 12;//一个汉字是十个字节这里我多加2个
            len = Title.Length * Pixe;
            obj.ActiveSheet.SetColumnWidth(Col, len);
        }

        public FarPoint.Win.Spread.FpSpread CreateFpSpread()
        {
            FarPoint.Win.Spread.FpSpread fpSpread = new FarPoint.Win.Spread.FpSpread();
            return fpSpread;
        }
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="obj">传入对象</param>
        /// <param name="RowStep">要合并单元格的数量,行</param>
        /// <param name="ColStep">要合并单元格的数量,列</param>
        /// <param name="Row">行</param>
        /// <param name="Col">列</param>
        /// <param name="Title">标题</param>
        public void CreateSheetView(FarPoint.Win.Spread.SheetView obj, int RowStep, int ColStep, int IntRow, int Col, string strTitle)
        {
            /*
             * //以前的方法
            FarPoint.Win.Spread.Cell acell =null;
            if (obj.RowCount == 0 || obj.ColumnCount == 0)
            {
                obj.RowCount = 40;
                obj.ColumnCount = 20;
            }
                acell = obj.Cells[IntRow, Col];
                acell.ColumnSpan = ColStep;
                acell.RowSpan = RowStep;
                //acell.Text = strTitle;
                acell.Value = strTitle;

                acell.HorizontalAlignment = HAlignment;
                acell.VerticalAlignment = VAlignment;
             * */
            if (RowStep == 0)
                RowStep = 1;
            if (ColStep == 0)
                ColStep = 1;
            obj.AddSpanCell(IntRow, Col, RowStep, ColStep);
            obj.SetValue(IntRow, Col, strTitle);
            //水平和垂直均居中对齐。
            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            obj.Rows[IntRow].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
        }
        /// <summary>
        /// 设置单元格列的宽度
        /// </summary>
        /// <param name="obj">SheetView对象</param>
        /// <param name="Col">哪个列</param>
        /// <param name="Title">名字</param>
        public void SetSheetViewColumnsWidth(FarPoint.Win.Spread.SheetView obj, int Col, string Title)
        {
            int len = 0;
            const int Pixe = 12;//一个汉字是十个字节这里我多加2个
            len = Title.Length * Pixe;
            obj.SetColumnWidth(Col, len);
        }
        /// <summary>
        /// 创建Sheet
        /// </summary>
        public FarPoint.Win.Spread.SheetView CreateSheet(System.Windows.Forms.Form form, FarPoint.Win.Spread.FpSpread obj, string Title)
        {
            FarPoint.Win.Spread.SheetView fpSpread1_Sheet3 = new FarPoint.Win.Spread.SheetView();
            fpSpread1_Sheet3 = CreateFpSpread(form, obj, fpSpread1_Sheet3, Title);
            return fpSpread1_Sheet3;
        }
        /// <summary>
        /// 设置年分
        /// </summary>
        ///<param name="FB">传入fromBase对象</param>
        /// <param name="obj">要传入SheetView对象</param>
       
        /// <param name="IntRow">行数</param>
        /// <param name="IntCol">列数</param>
        /// <param name="RowStep">要合并几个行的单元格</param>
        /// <param name="ColStep">要合并几个列的单元格</param>
        /// <param name="Title">标题</param>
        public void SetYears(string Title, Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol, int RowStep, int ColStep)
        {
            GlobalFormBase = FB;
            Ps_YearRange py = new Ps_YearRange();
            py.Col4 = Title;
            py.Col5 =FB. ProjectUID;

            IList<Ps_YearRange> li = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);
            if (li.Count > 0)
            {
                GlobalFirstYear = li[0].StartYear;
                GlobalEndYear = li[0].FinishYear;
            }
            else
            {
                GlobalFirstYear = 2000;
                GlobalEndYear = 2009;
                py.BeginYear = 1990;
                py.FinishYear = GlobalEndYear;
                py.StartYear = GlobalFirstYear;
                py.EndYear = 2060;
                py.ID = Guid.NewGuid().ToString();
                Services.BaseService.Create<Ps_YearRange>(py);
            }
            GolobalYearCount = GlobalEndYear - GlobalFirstYear + FixationCol+1 ;//后面有两个固定列,加1是起始年的加入
            SetYearsSheet_1(FB,obj,GlobalFirstYear,GlobalEndYear,GolobalYearCount,IntRow,IntCol,RowStep,ColStep);
        }
        /// <summary>
        /// 清空表格
        /// </summary>
        /// <param name="obj">sheetView对象</param>
        public void RefreshSheet(FarPoint.Win.Spread.SheetView obj)
        {
            obj.Reset();
        }
        /// <summary>
        /// 设置sheet_1的年份
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="firstyear">起始年份</param>
        /// <param name="endyear">结束年份</param>
        /// <param name="YearsCount">要显示几年的数据</param>
        /// <param name="IntRow">要写入的起始行数</param>
        /// <param name="IntCol">要写入的起始列数</param>
        /// <param name="RowStep">要合并单元格行的数量</param>
        /// <param name="ColStep">要合并单元格列的数量</param>
        private void SetYearsSheet_1(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, int firstyear, int endyear, int YearsCount, int IntRow, int IntCol, int RowStep, int ColStep)
        {

            GolobalYears = 0;
            string Title =null ;
           string Title1 = "";
           string SingleTitle = null;
           for (int j = 0; j < 3; j++)//*3是类型有三个大框,
           {
               int TempYear = firstyear;
               switch(j)   
               {
                   case 0:
                       SingleTitle = "市辖供电区";
                       break;
                   case 1:
                       SingleTitle = "县辖供电区";
                       break;
                   case 2:
                       SingleTitle = "全地区";
                       break;
                   default:
                       SingleTitle = "";
                       break;
               }

               //if (SingleTitle != "全地区")//判断一级目录
               // {
                    for (int i = IntRow; i <((YearsCount + IntRow)); i++)
                    {
                        if ((YearsCount + IntRow - 2) == i)
                        {
                            Title = "“十五”年均增长率";
                            CreateSheetView(obj, RowStep, ColStep, ((i - GolobalYears) + j * YearsCount), IntCol, Title);
                            //SetRowHight(obj, ((i - GolobalYears) + j * YearsCount), IntCol, Title);
                            //SheetNFormula(obj, ((i - GolobalYears) + j * YearsCount), (IntCol), Title);
                        }
                        else if ((YearsCount + IntRow - 1) == i)
                        {

                            if (TempYear< 2011)//这里写2011要注意
                            {
                                Title = "2006-2009年均增长率";
                            }
                            else
                            {
                                Title = "“十一五”年均增长率";
                            }

                            CreateSheetView(obj, RowStep, ColStep, ((i - GolobalYears) + j * YearsCount), IntCol, Title);
                            SetSheetViewColumnsWidth(obj, IntCol, Title1);
                            //SetRowHight(obj, ((i - GolobalYears) + j * YearsCount), IntCol, Title);
                            //SheetNFormula(obj, ((i - GolobalYears) + j * YearsCount), (IntCol), Title);
                        }
                        else
                        {
                                Title = TempYear + " 年";
                            ////判断这个年份是否有数据
                            //if(YearIsData(Title,("y"+TempYear)))
                            if(TempYear!=2001&&TempYear!=2002&&TempYear!=2003&&TempYear!=2004)
                            {
                                CreateSheetView(obj, RowStep, ColStep, ((i - GolobalYears) + j * YearsCount), IntCol, Title);

                                Title1 = "全社会最大负荷（万kW）";
                                ViewSheet4_1Data(obj, Title1, FB, TempYear, ((i - GolobalYears) + j * YearsCount), (IntCol + 1), SingleTitle);
                                //SheetNFormula(obj, ((i - GolobalYears) + j * YearsCount), (IntCol + 2), Title1);

                                Title1 = "网供最大负荷";
                                ViewSheet4_1Data(obj, Title1, FB, TempYear, ((i - GolobalYears) + j * YearsCount), (IntCol + 3), SingleTitle);
                                //SheetNFormula(obj, ((i - GolobalYears) + j * YearsCount), (IntCol + 4), Title1);

                                Title1 = "全社会供电量（亿kWh）";
                                ViewSheet4_1Data(obj, Title1, FB, TempYear, ((i - GolobalYears) + j * YearsCount), (IntCol + 5), SingleTitle);
                                //SheetNFormula(obj, ((i - GolobalYears) + j * YearsCount), (IntCol + 6), Title1);

                                Title1 = "全社会用电量（亿kWh）";
                                ViewSheet4_1Data(obj, Title1, FB, TempYear, ((i - GolobalYears) + j * YearsCount), (IntCol + 7), SingleTitle);

                                //SheetNFormula(obj, ((i - GolobalYears) + j * YearsCount), (IntCol + 7), Title1);
                                //SheetNFormula(obj, ((i - GolobalYears) + j * YearsCount), (IntCol + 8), Title1);

                                Title1 = "一产";
                                ViewSheet4_1Data(obj, Title1, FB, TempYear, ((i - GolobalYears) + j * YearsCount), (IntCol + 9), SingleTitle);

                                Title1 = "二产";
                                ViewSheet4_1Data(obj, Title1, FB, TempYear, ((i - GolobalYears) + j * YearsCount), (IntCol + 10), SingleTitle);

                                Title1 = "三产";
                                ViewSheet4_1Data(obj, Title1, FB, TempYear, ((i - GolobalYears) + j * YearsCount), (IntCol + 11), SingleTitle);

                                Title1 = "居民";
                                ViewSheet4_1Data(obj, Title1, FB, TempYear, ((i - GolobalYears) + j * YearsCount), (IntCol + 12), SingleTitle);
                            }
                            else
                            {
                                GolobalYears += 1;//年份没有数据的总和
                            }
                            resualt.Clear();
                            ++TempYear;
                        }
                    }
               //}
           }
        }
        /// <summary>
        /// 显示数据
        /// </summary>
        private void ViewSheet4_1Data(FarPoint.Win.Spread.SheetView obj,string Title, Itop.Client.Base.FormBase FB,int FistYear,int IntRow,int IntCol,string SingleTitle)
        {

            string Temp = "y" + FistYear.ToString();
            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = IntType;
            psp_Type.Col4 = FB.ProjectUID;
            //IList<Ps_History> listTypes = Itop.Client.Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);

            //dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));
            //    for (int i = 0; i < YearCount ; i++)
            //    {
            //        if (dataTable.Columns[i].Caption == Temp)
            //        MessageBox.Show(listTypes[i]=Temp);
            //    }

            string con = null;
            Ps_History GDP1 = null;

            if (SingleTitle != "全地区")
            {
                if (Title == "全社会最大负荷（万kW）")
                {
                    con = "Title='" + SingleTitle + "'AND Col4='" + FB.ProjectUID + "'AND Forecast='" + IntType + "'";
                    GDP1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
                    TwoParentID = GDP1.ID;
                    //resualt.Add(SingleTitle, GDP1);

                    con = "Title='" + Title + "'AND Col4='" + FB.ProjectUID + "'AND Forecast='" + IntType + "'";
                    con += "AND ParentID='" + TwoParentID + "'";
                    GDP1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
                    resualt.Add(Title, GDP1);
                }
                else
                {

                    if ((Title == "一产") || (Title == "二产") || (Title == "三产") || (Title == "居民"))
                    {
                        con = "Title='" + Title + "'AND Col4='" + FB.ProjectUID + "'AND Forecast='" + IntType + "'";
                        con += "AND ParentID='" + ThreeParentID + "'";
                        GDP1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);

                    }
                    else
                    {
                        con = "Title='" + Title + "'AND Col4='" + FB.ProjectUID + "'AND Forecast='" + IntType + "'";
                        con += "AND ParentID='" + TwoParentID + "'";
                        GDP1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
                        if (Title == "全社会用电量（亿kWh）")
                        {
                            ThreeParentID = GDP1.ID;
                        }
                    }
                    resualt.Add(Title, GDP1);
                    //resualt1.Add(SingleTitle + Title, GDP1);
                }

                ////判断是否年份中有数字，没有就不显示
                //if (Gethistroyvalue<Ps_History>(resualt[Title], Temp) > 0.0000 || Gethistroyvalue<Ps_History>(resualt[Title], Temp) <0.0000)
                if (FistYear != 2001 && FistYear != 2002 && FistYear != 2003 && FistYear != 2004)
                {
                    if (Title != "全社会用电量（亿kWh）")
                    {
                        obj.SetValue(IntRow, IntCol, Gethistroyvalue<Ps_History>(resualt[Title], Temp));
                        //this.SetRowHight(obj, IntRow, IntCol, Gethistroyvalue<Ps_History>(resualt[Title], Temp));
                    }
                }
            }

        }
        /// <summary>
        /// 获得对象中某一属性的数值
        /// </summary>
        /// <param name="Ps">对象</param>
        /// <param name="name">数值</param>

        public  double Gethistroyvalue<T>(T ps, string name)
        {
            Type type = typeof(T);
            double psvalue = 0.0;
            foreach (PropertyInfo pi in type.GetProperties())
            {

                if (pi.Name == name)
                {
                    try
                    {
                        psvalue = Convert.ToDouble(pi.GetValue(ps, null));
                        break;
                    }
                    catch (System.Exception e)
                    {
                        //MessageBox.Show("没有此项数据！！！");
                    }
                }
                //pi.SetValue(dataObject, treeNode.GetValue(pi.Name), null);

            }
            return psvalue;
        }
        /// <summary>
        /// 设定单元格的算法
        /// </summary>
        /// <param name="obj">sheetView对象</param>
        /// <param name="IntRow">行数</param>
        /// <param name="IntCol">列数</param>
        public void SheetNFormula(FarPoint.Win.Spread.SheetView obj,int IntRow,int IntCol)
        {
            int floor=(GolobalYearCount - GolobalYears/3);
            int TwoThousandAndSix = 0;//具体哪行是2005年的
            int TwoThousandAndNine = 0;
            int len = 0;
            double  Pitch = 0;
            double CityPeople = 0.0;
            double VillagePeople = 0.0; 
            string strFirstYear = "";
            string strNextYear = "";
            string Temp = null;

            GetPopulation("乡村人口（万人）");
            GetPopulation("城镇人口（万人）");
            for (int j = 0; j < 3; j++)
            {
                for (int i = 1; i <= floor; i++)
                {
                    //Title1 = "全社会最大负荷（万kW）";
                    //MessageBox.Show(obj.GetValue(IntRow +i, IntCol-2).ToString());
                    //用下一层的年份减去上一层的年份
                    strFirstYear = obj.GetValue((IntRow + i - 1) + j * floor, IntCol - 2).ToString();
                    if (i != floor)
                    {
                        strNextYear = obj.GetValue(((IntRow + i) + j * floor), IntCol - 2).ToString();
                        Pitch = (double)YearsPitch(strFirstYear, strNextYear);
                        Pitch = (double)(1 / Pitch);
                        if (j==2)//全地区
                        {
                            SetValue_QDQ(obj, IntRow, IntCol-1, i, j, floor);
                        }
                        obj.Cells[IntRow + i + j * floor, IntCol].Formula = "POWER(((C" + (IntRow + i + 1 + (j * floor)) + ")/C" + (IntRow + i + (j * floor)) + "), " + Pitch + ")-1";
                        //SetRowHight(obj, (IntRow + i - 1 + j * floor), (IntCol), (obj.GetValue((IntRow + i + j * floor), (IntCol))));
                    }
                    if (strFirstYear == "“十五”年均增长率")
                    {
                        TwoThousandAndSix = YearRow(obj, "2005 年", IntRow + j * floor, IntCol);
                        TwoThousandAndNine = YearRow(obj, "2000 年", IntRow + j * floor, IntCol);
                        Pitch = (double)YearsPitch("2000 年", "2005 年");
                        Pitch = (double)(1 / Pitch);

                        //if (j == 2)//全地区
                        //{
                        //    SetValue_QDQ(obj, IntRow, IntCol, i, j, floor);
                        //}

                        obj.Cells[IntRow + i - 1 + j * floor, IntCol].Formula = "POWER(((C" + TwoThousandAndSix + ")/C" + TwoThousandAndNine + "), " + Pitch + ")-1";
                        //SetRowHight(obj, (IntRow + i - 1 + j * floor), (IntCol), (obj.GetValue((IntRow + i + j * floor), (IntCol))));
                    }
                    if (strFirstYear == "2006-2009年均增长率")
                    {
                        TwoThousandAndSix = YearRow(obj, "2009 年", IntRow + j * floor, IntCol);
                        TwoThousandAndNine = YearRow(obj, "2006 年", IntRow + j * floor, IntCol);
                        Pitch = (double)YearsPitch("2006 年", "2009 年");
                        Pitch = (double)(1 / Pitch);

                        //if (j == 2)//全地区
                        //{
                        //    SetValue_QDQ(obj, IntRow, IntCol, i, j, floor);
                        //}

                        obj.Cells[IntRow + i - 1 + j * floor, IntCol].Formula = "POWER(((C" + TwoThousandAndSix + ")/C" + TwoThousandAndNine + "), " + Pitch + ")-1";
                        
                        //SetRowHight(obj, (IntRow + i - 1 + j * floor), (IntCol), (obj.GetValue((IntRow + i + j * floor), (IntCol))));
                    }
                    if (strFirstYear == "“十一五”年均增长率")
                    {
                        TwoThousandAndSix = YearRow(obj, "2010 年", IntRow + j * floor, IntCol);
                        TwoThousandAndNine = YearRow(obj, "2005 年", IntRow + j * floor, IntCol);
                        Pitch = (double)YearsPitch("2005 年", "2010 年");
                        Pitch = (double)(1 / Pitch);

                        //if (j == 2)//全地区
                        //{
                        //    SetValue_QDQ(obj, IntRow, IntCol, i, j, floor);
                        //}

                        obj.Cells[IntRow + i - 1 + j * floor, IntCol].Formula = "POWER(((C" + TwoThousandAndSix + ")/C" + TwoThousandAndNine + "), " + Pitch + ")-1";
                        //try
                        //{
                        //    //SetRowHight(obj, (IntRow + i - 1 + j * floor), (IntCol), (obj.GetValue((IntRow + i + j * floor), (IntCol))));
                        //}
                        //catch (System.Exception ee)
                        //{
                        //    //MessageBox.Show(ee.Message);
                        //}
                    }
                    //  Title1 = "网供最大负荷";
                    if (i != floor)
                    {
                        if (j == 2)//全地区
                        {
                            SetValue_QDQ(obj, IntRow, IntCol+1, i, j, floor);
                        }
                        obj.Cells[IntRow + i + j * floor, IntCol + 2].Formula = "POWER(((E" + (IntRow + i + 1 + (j * floor)) + ")/E" + (IntRow + i + (j * floor)) + "), " + Pitch + ")-1";
                        //SetRowHight(obj, (IntRow + i - 1 + j * floor), (IntCol + 2), (obj.GetValue((IntRow + i + j * floor), (IntCol + 2))));
                    }
                    if (strFirstYear == "“十五”年均增长率")
                    {
                        TwoThousandAndSix = YearRow(obj, "2005 年", IntRow + j * floor, IntCol);
                        TwoThousandAndNine = YearRow(obj, "2000 年", IntRow + j * floor, IntCol);
                        Pitch = (double)YearsPitch("2000 年", "2005 年");
                        Pitch = (double)(1 / Pitch);
                        obj.Cells[IntRow + i - 1 + j * floor, IntCol + 2].Formula = "POWER(((E" + TwoThousandAndSix + ")/E" + TwoThousandAndNine + "), " + Pitch + ")-1";
                        //SetRowHight(obj, (IntRow + i - 1 + j * floor), (IntCol + 2), (obj.GetValue((IntRow + i + j * floor), (IntCol + 2))));
                    }
                    if (strFirstYear == "2006-2009年均增长率")
                    {
                        TwoThousandAndSix = YearRow(obj, "2009 年", IntRow + j * floor, IntCol);
                        TwoThousandAndNine = YearRow(obj, "2006 年", IntRow + j * floor, IntCol);
                        Pitch = (double)YearsPitch("2006 年", "2009 年");
                        Pitch = (double)(1 / Pitch);
                        obj.Cells[IntRow + i - 1 + j * floor, IntCol + 2].Formula = "POWER(((E" + TwoThousandAndSix + ")/E" + TwoThousandAndNine + "), " + Pitch + ")-1";
                        //SetRowHight(obj, (IntRow + i - 1 + j * floor), (IntCol + 2), (obj.GetValue((IntRow + i + j * floor), (IntCol + 2))));
                    }
                    if (strFirstYear == "“十一五”年均增长率")
                    {
                        TwoThousandAndSix = YearRow(obj, "2010 年", IntRow + j * floor, IntCol);
                        TwoThousandAndNine = YearRow(obj, "2005 年", IntRow + j * floor, IntCol);
                        Pitch = (double)YearsPitch("2005 年", "2010 年");
                        Pitch = (double)(1 / Pitch);
                        obj.Cells[IntRow + i - 1 + j * floor, IntCol + 2].Formula = "POWER(((E" + TwoThousandAndSix + ")/E" + TwoThousandAndNine + "), " + Pitch + ")-1";
                        //try
                        //{
                        //    SetRowHight(obj, (IntRow + i - 1 + j * floor), (IntCol + 2), (obj.GetValue((IntRow + i + j * floor), (IntCol + 2))));
                        //}
                        //catch (System.Exception ee)
                        //{

                        //}
                    }
                    // Title1 = "全社会供电量（亿kWh）";
                    if (i != floor)
                    {
                        if (j == 2)//全地区
                        {
                            SetValue_QDQ(obj, IntRow, IntCol+3, i, j, floor);
                        }
                        obj.Cells[IntRow + i + j * floor, IntCol + 4].Formula = "POWER(((G" + (IntRow + i + 1 + (j * floor)) + ")/G" + (IntRow + i + (j * floor)) + "), " + Pitch + ")-1";
                        //SetRowHight(obj, (IntRow + i - 1 + j * floor), (IntCol + 4), (obj.GetValue((IntRow + i + j * floor), (IntCol + 4))));
                    }
                    if (strFirstYear == "“十五”年均增长率")
                    {
                        TwoThousandAndSix = YearRow(obj, "2005 年", IntRow + j * floor, IntCol);
                        TwoThousandAndNine = YearRow(obj, "2000 年", IntRow + j * floor, IntCol);
                        Pitch = (double)YearsPitch("2000 年", "2005 年");
                        Pitch = (double)(1 / Pitch);
                        obj.Cells[IntRow + i - 1 + j * floor, IntCol + 4].Formula = "POWER(((G" + TwoThousandAndSix + ")/G" + TwoThousandAndNine + "), " + Pitch + ")-1";
                        //SetRowHight(obj, (IntRow + i - 1 + j * floor), (IntCol + 4), (obj.GetValue((IntRow + i + j * floor), (IntCol + 4))));
                    }
                    if (strFirstYear == "2006-2009年均增长率")
                    {
                        TwoThousandAndSix = YearRow(obj, "2009 年", IntRow + j * floor, IntCol);
                        TwoThousandAndNine = YearRow(obj, "2006 年", IntRow + j * floor, IntCol);
                        Pitch = (double)YearsPitch("2006 年", "2009 年");
                        Pitch = (double)(1 / Pitch);
                        obj.Cells[IntRow + i - 1 + j * floor, IntCol + 4].Formula = "POWER(((G" + TwoThousandAndSix + ")/G" + TwoThousandAndNine + "), " + Pitch + ")-1";
                        //SetRowHight(obj, (IntRow + i - 1 + j * floor), (IntCol + 4), (obj.GetValue((IntRow + i + j * floor), (IntCol + 4))));
                    }
                    if (strFirstYear == "“十一五”年均增长率")
                    {
                        TwoThousandAndSix = YearRow(obj, "2010 年", IntRow + j * floor, IntCol);
                        TwoThousandAndNine = YearRow(obj, "2005 年", IntRow + j * floor, IntCol);
                        Pitch = (double)YearsPitch("2005 年", "2010 年");
                        Pitch = (double)(1 / Pitch);
                        obj.Cells[IntRow + i - 1 + j * floor, IntCol + 4].Formula = "POWER(((G" + TwoThousandAndSix + ")/G" + TwoThousandAndNine + "), " + Pitch + ")-1";
                        //try
                        //{
                        //    SetRowHight(obj, (IntRow + i - 1 + j * floor), (IntCol + 4), (obj.GetValue((IntRow + i + j * floor), (IntCol + 4))));
                        //}
                        //catch (System.Exception ee)
                        //{

                        //}
                    }
                    //Title1 = "全社会用电量（亿kWh）";
                    obj.Cells[IntRow + i - 1 + j * floor, IntCol + 5].Formula = "SUM(k" + (IntRow + i + (j * floor)) + ":N" + (IntRow + i + (j * floor)) + ")";
                    //try
                    //{
                    //    SetRowHight(obj, (IntRow + i - 1 + j * floor), (IntCol + 5), (obj.GetValue((IntRow + i + j * floor), (IntCol + 5))));
                    //}
                    //catch (System.Exception ee)
                    //{

                    //}

                    if (i != floor)
                    {

                        obj.Cells[IntRow + i + j * floor, IntCol + 6].Formula = "POWER(((I" + (IntRow + i + 1 + (j * floor)) + ")/I" + (IntRow + i + (j * floor)) + "), " + Pitch + ")-1";
                        //SetRowHight(obj, (IntRow + i - 1 + j * floor), (IntCol + 6), (obj.GetValue((IntRow + i + j * floor), (IntCol + 6))));
                    }
                    if (strFirstYear == "“十五”年均增长率")
                    {
                        TwoThousandAndSix = YearRow(obj, "2005 年", IntRow + j * floor, IntCol);
                        TwoThousandAndNine = YearRow(obj, "2000 年", IntRow + j * floor, IntCol);
                        Pitch = (double)YearsPitch("2000 年", "2005 年");
                        Pitch = (double)(1 / Pitch);
                        obj.Cells[IntRow + i - 1 + j * floor, IntCol + 6].Formula = "POWER(((I" + TwoThousandAndSix + ")/I" + TwoThousandAndNine + "), " + Pitch + ")-1";
                        //SetRowHight(obj, (IntRow + i - 1 + j * floor), (IntCol + 6), (obj.GetValue((IntRow + i + j * floor), (IntCol + 6))));
                    }
                    if (strFirstYear == "2006-2009年均增长率")
                    {
                        TwoThousandAndSix = YearRow(obj, "2009 年", IntRow + j * floor, IntCol);
                        TwoThousandAndNine = YearRow(obj, "2006 年", IntRow + j * floor, IntCol);
                        Pitch = (double)YearsPitch("2006 年", "2009 年");
                        Pitch = (double)(1 / Pitch);
                        obj.Cells[IntRow + i - 1 + j * floor, IntCol + 6].Formula = "POWER(((I" + TwoThousandAndSix + ")/I" + TwoThousandAndNine + "), " + Pitch + ")-1";
                        //SetRowHight(obj, (IntRow + i - 1 + j * floor), (IntCol + 6), (obj.GetValue((IntRow + i + j * floor), (IntCol + 6))));
                    }
                    if (strFirstYear == "“十一五”年均增长率")
                    {
                        TwoThousandAndSix = YearRow(obj, "2010 年", IntRow + j * floor, IntCol);
                        TwoThousandAndNine = YearRow(obj, "2005 年", IntRow + j * floor, IntCol);
                        Pitch = (double)YearsPitch("2005 年", "2010 年");
                        Pitch = (double)(1 / Pitch);
                        obj.Cells[IntRow + i - 1 + j * floor, IntCol + 6].Formula = "POWER(((I" + TwoThousandAndSix + ")/I" + TwoThousandAndNine + "), " + Pitch + ")-1";
                        //try
                        //{
                        //     //SetRowHight(obj, (IntRow + i - 1 + j * floor), (IntCol + 6), (obj.GetValue((IntRow + i + j * floor), (IntCol + 6))));
                        //}catch(System.Exception ee)
                        //{

                        //}
                       
                    }
                    //title="一产"，二产，三产，居民
                        if (j == 2)//全地区
                        {
                            SetValue_QDQ(obj, IntRow, IntCol +7, i, j, floor);//一产
                            SetValue_QDQ(obj, IntRow, IntCol + 8, i, j, floor);//二产
                            SetValue_QDQ(obj, IntRow, IntCol + 9, i, j, floor);//三产
                            SetValue_QDQ(obj, IntRow, IntCol + 10, i, j, floor);//居民

                       }

                    len = strFirstYear.Length;
                    strFirstYear = strFirstYear.Remove(len - 2);
                    Temp = "y" + strFirstYear;
                    //人均用电量（kWh/人）
                    VillagePeople = Gethistroyvalue<Ps_History>(resualt["乡村人口（万人）"], Temp);
                    CityPeople = Gethistroyvalue<Ps_History>(resualt["城镇人口（万人）"], Temp);
                    if (i < (floor - 1))
                    {
                        obj.Cells[(IntRow + i - 1 + j * floor), (IntCol + 11)].Formula = "I" + (IntRow + i + (j * floor)) + "*10000/" + (VillagePeople + CityPeople);
                        //SetRowHight(obj, (IntRow + i - 1 + j * floor), (IntCol + 11), (obj.GetValue((IntRow + i + j * floor), (IntCol + 11))));
                        //人均生活用电量（kWh/人）
                        obj.Cells[(IntRow + i - 1 + j * floor), (IntCol + 12)].Formula = "N" + (IntRow + i + (j * floor)) + "*10000/" + (VillagePeople + CityPeople);
                        //SetRowHight(obj, (IntRow + i - 1 + j * floor), (IntCol + 12), (obj.GetValue((IntRow + i + j * floor), (IntCol + 12))));
                    }
                }
            }
            resualt.Clear();
        }
        /// <summary>
        /// 判断年份是否有数据
        /// </summary>
        /// <param name="Title">年份</param>
        /// <returns></returns>
        public bool YearIsData(string Title, string Year)
        {
            bool temp=false;//没有数据
            string con = "";
            Ps_History pf = new Ps_History();

            Ps_History GDP1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryList", pf);
            resualt.Add(Title, GDP1);
            if (Gethistroyvalue<Ps_History>(resualt[Title], Year) > 0.0000 || Gethistroyvalue<Ps_History>(resualt[Title], Year) < 0.0000)
            {
                temp = true;//有数据
            }
            return temp;
        }
        /// <summary>
        /// 判断下一层和上一层的年份的间隔
        /// </summary>
        /// <param name="FirstYear">起始年份</param>
        /// <param name="NextYear">下一层的年份</param>
        /// <returns>返回年份的间隔</returns>
        public int YearsPitch(string FirstYear,string NextYear)
        {
            int FirstYearLen = 0;
            int NextYearLen = 0;
            int IntPitch = 0;
            FirstYearLen=FirstYear.Length;
            NextYearLen = NextYear.Length;
           FirstYear= FirstYear.Remove(FirstYearLen - 2);
           NextYear= NextYear.Remove(NextYearLen-2);
            int.TryParse(FirstYear, out FirstYearLen);
            int.TryParse(NextYear,out NextYearLen);
            return IntPitch = NextYearLen - FirstYearLen;
        }
        /// <summary>
        /// 通过标题查找当前标题在哪行
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="obj">sheetView对象</param>
        /// <param name="IntRow">起始行</param>
        /// <param name="IntCol">起始列</param>
        /// <returns>行数</returns>
        public int YearRow(FarPoint.Win.Spread.SheetView obj, string Title, int IntRow, int IntCol)
        {
            int floor=(GolobalYearCount - GolobalYears/3);
            int TempRow = 0;
            string strTemp = null;
            for(int i=1;i<floor;i++)
            {
                strTemp = obj.GetValue(IntRow + i - 1, IntCol - 2).ToString();
                if(strTemp==Title)
                {
                    TempRow = IntRow + i ;//行数从0开始所以不用-1
                    break;
                }
            }
            return TempRow;
        }
        /// <summary>
        /// 取人口数
        /// </summary>
        /// <param name="Title"></param>
        private void  GetPopulation(string Title)
        {
            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = IntType;
            psp_Type.Col4 = GlobalFormBase.ProjectUID;
            string con = "Title='" + Title + "'AND Col4='" + GlobalFormBase.ProjectUID + "'AND Forecast='" + IntType + "'";
            Ps_History GDP1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
            resualt.Add(Title,GDP1);
        }
        /// <summary>
        /// 设置全地区的各项值
        /// </summary>
        /// <param name="obj">sheetView对象</param>
        /// <param name="IntRow">行数</param>
        /// <param name="IntCol">列数</param>
        /// <param name="j">列数</param>
        /// <param name="i">行数</param>
        /// <param name="floor">层数</param>
        public void SetValue_QDQ(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol,int i,int j,int floor)
        {
            double IntTemp = 0.0;
            double IntTemp1 = 0.0;
            string Temp = null;
            string Temp1 = null;
            if(i<(floor-1))
            {
                Temp = obj.GetValue((IntRow + i - 1), IntCol).ToString();
                double.TryParse(Temp, out IntTemp);
                Temp1 = obj.GetValue((IntRow + i - 1) + floor, IntCol).ToString();
                double.TryParse(Temp1, out IntTemp1);
                obj.SetValue(IntRow + i + j * floor - 1, IntCol, IntTemp + IntTemp1);
            }
        }
        /// <summary>
        /// 设置某列为只读
        /// </summary>
        /// <param name="obj">sheetView对象</param>
        /// <param name="IntCol">列数</param>
        public void ColReadOnly(FarPoint.Win.Spread.SheetView obj,int IntCol)
        {
            for (int i = 0; i < IntCol; i++)
            {
                if(i<(IntCol-1))//最后一列为读写
                {
                    obj.Columns[i].Locked = true;//列设置为只读
                }
            }
        }

        public void FrmWait(FarPoint.Win.Spread.FpSpread obj, Itop.Client.Base.FormBase FB)
        {
            EconomyAnalysis ec = Services.BaseService.GetOneByKey<EconomyAnalysis>(FB.ProjectUID);
            if (ec != null)
            {
                WaitDialogForm wait = null;
                try
                {
                    wait = new WaitDialogForm("", "正在加载数据, 请稍候...");
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(ec.Contents);
                    by1 = ec.Contents;
                    obj.Open(ms);

                    wait.Close();

                }
                catch { wait.Close(); }
            }
            else
            {
                ec.UID = FB.ProjectUID;
                ec.Contents = bts;
                //obj.ParentID = uid;
                ec.CreateDate = DateTime.Now;
                Services.BaseService.Update<EconomyAnalysis>(ec);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(ec.Contents);
                obj.Open(ms);
            }
        }
        /// <summary>
        /// 导出Excel
        /// </summary>
        public void ToExcel(FarPoint.Win.Spread.FpSpread obj)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string fname = "";
            saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fname = saveFileDialog1.FileName;

                try
                {
                    obj.SaveExcel(fname);
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
        ///导入Excel
        /// 
        /// <summary>
        public void InExcel(FarPoint.Win.Spread.FpSpread obj)
        {
            FarPoint.Excel.ExcelWarningList EWL = new FarPoint.Excel.ExcelWarningList();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string fname = "";
            saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fname = saveFileDialog1.FileName;

                try
                {
                    obj.SaveExcel(fname, FarPoint.Excel.ExcelSaveFlags.NoFormulas);
                    //fps.SaveExcel(fname);
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
        /// 保存文档
        /// </summary>
        public void SaveExcel(FarPoint.Win.Spread.FpSpread obj,string PathName)
        {
            WaitDialogForm wait = null;
            wait = new WaitDialogForm("", "正在保存数据, 请稍候...");
            //判断文件夹xls是否存在，不存在则创建
            if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\xls"))
            {
                Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\xls");
            }
            try
            {
                //保存excel文件
                obj.SaveExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\" + PathName + ".xls");
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
                string filename = System.Windows.Forms.Application.StartupPath + "\\xls\\" + PathName + ".xls";
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
                MsgBox.Show("保存成功");
            }
            catch (System.Exception ee)
            {
                wait.Close();
                MsgBox.Show("保存错误！确定您安装有Office Excel,或者关闭所有Excel文件重试");
            }
            

        }
        /// <summary>
        /// 返回保留2位小数
        /// </summary>
        /// <param name="dou">要传入的数值</param>
        /// <returns></returns>
        public string ReturnFormatStr(double dou)
        {
            string Place = null;
            Place = dou.ToString();
            Place = string.Format("{0:F}", dou);
            return Place;
        }       /// 导出excel
        /// </summary>
        /// <param name="ExelDt"></param>
        //protected void DoTranExcel(System.Data.DataSet ExelDt)
        //{
        //    int colIndex = 1, rowIndex = 1;
        //    Excel.Application excel;
        //    try
        //    {
        //        excel = new Excel.Application();
        //        excel.Application.Workbooks.Add(true);
        //        excel.Visible = true;
        //    }
        //    catch
        //    {
        //        MessageBox.Show("您可能没有安装Office，请安装再使用该功能");
        //        return;
        //    }
        //    //foreach(DataColumn col in this.ExelDt.Tables[0].Columns)
        //    //{
        //    //excel.Cells[1,colIndex]=col.ColumnName; colIndex++; 
        //    try
        //    {
        //        //}
        //        for (int i = 0; i < this.xdg.TableStyles[0].GridColumnStyles.Count; i++)
        //        {
        //            excel.Cells[1, colIndex] = this.xdg.TableStyles[0].GridColumnStyles[i].HeaderText; colIndex++;
        //        }
        //        foreach (DataRow row in ExelDt.Tables[0].Rows)
        //        {
        //            rowIndex++; colIndex = 0;
        //            foreach (DataColumn col in ExelDt.Tables[0].Columns)
        //            {
        //                colIndex++;
        //                if (colIndex == 1)
        //                {
        //                    excel.Cells[rowIndex, colIndex] = "'" + row[col.ColumnName].ToString();
        //                }
        //                else
        //                {
        //                    excel.Cells[rowIndex, colIndex] = row[col.ColumnName].ToString();
        //                }
        //            }
        //        }
        //    }
        //    catch (System.Exception)
        //    {
        //        MessageBox.Show("输出Excel有错误，请确认没有关闭Excel");
        //        return;
        //    }
        //}

        /// <summary>
        /// 通用的打印表格方法
        /// </summary>
        /// <param name="fpview"></param>
        /// <param name="fp"></param>
        /// <param name="index"></param>
  /*      public static void CommonPrint(FarPoint.Win.Spread.SheetView fpview, FpSpread fp, int index)
        {
            try
            {
                if (fpview.RowCount == 0)
                    return;
                FarPoint.Win.Spread.PrintInfo pi = new FarPoint.Win.Spread.PrintInfo();
                DialogResult result = MessageBox.Show("是否要横向打印?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                FarPoint.Win.Spread.StyleInfo style = new FarPoint.Win.Spread.StyleInfo();
                style.Border = new FarPoint.Win.LineBorder(Color.Black, 1);
                style.BackColor = Color.White;
                fpview.ColumnHeader.DefaultStyle = style;
                fpview.RowHeader.DefaultStyle = style;
                if (result == DialogResult.Yes)
                {
                    pi.Orientation = FarPoint.Win.Spread.PrintOrientation.Landscape;
                }
                else
                    pi.Orientation = FarPoint.Win.Spread.PrintOrientation.Portrait;
                FarPoint.Win.Spread.PrintMargin pm = new
                FarPoint.Win.Spread.PrintMargin();
                pm.Left = 100;
                pm.Right = 60;
                pm.Top = 100;
                pm.Bottom = 20;
                pi.FirstPageNumber = 1;
                pi.Footer = "当前第 /p 页/n共 /pc 页";
                pi.Margin = pm;
                pi.PageStart = 1;
                pi.Preview = true;
                pi.ShowBorder = true;
                pi.ShowColor = false;
                pi.ShowColumnHeaders = true;
                pi.ShowGrid = true;
                pi.ShowPrintDialog = true;
                pi.ShowRowHeaders = true;
                pi.ShowShadows = true;
                pi.ZoomFactor = 1;
                pi.ShowPrintDialog = true;
                FarPoint.Win.Spread.PrintInfo clone = new FarPoint.Win.Spread.PrintInfo(pi);
                fpview.PrintInfo = clone;
                fp.PrintSheet(index);
            }
            catch
            {
                MessageBox.Show("打印发生错误,请确认是否有连接好打印机");
            }

        }
    */
        /// <summary>
        /// 设表格文本自动换行
        /// </summary>
        /// <param name="tempsheet"></param>
        public void Sheet_Colautoenter(FarPoint.Win.Spread.FpSpread tempspread)
        {
            FarPoint.Win.Spread.CellType.TextCellType cellType = new FarPoint.Win.Spread.CellType.TextCellType();
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
        /// 移除工作簿
        /// </summary>
        public void RemoveSheetView(FarPoint.Win.Spread.FpSpread obj,int SheetCount)
        {
            
            for (int i = 0; i < SheetCount; ++i)
            {
                    obj.Sheets[i].RowCount = 0;
                    obj.Sheets[i].ColumnCount = 0;
                //obj.RemoveViewport(SheetCount, i);
            }
        }
        /// <summary>
        /// 设置整体工作簿的行高
        /// </summary>
        public void SetWholeRowHeight(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            int[] RowHight=new int[IntCol];
            const int RowHeight = 20;//默认的行高
            string strTemp = null;

            string Chinese = "汉字v ";
            //byte[]   bs   =   System.Text.ASCIIEncoding.BigEndianUnicode.GetBytes(Chinese); 
            ////先按BigEndianUnicode编码 
            //string   re   =   System.Text.ASCIIEncoding.ASCII.GetString(bs);
            //int lent = 0;
            //obj.GetRowHeight(6);
            try
            {
                for(int i=3;i<IntRow;++i)
                {
                    for(int j=0;j<IntCol;++j)
                    {
                        if (obj.GetValue(i, j)==null)
                        {
                            strTemp = "0";
                        }
                        else
                        {
                            strTemp = obj.GetValue(i,j).ToString();
                            //lent = System.Text.ASCIIEncoding.Default.GetByteCount(strTemp);
                        }
                        if (strTemp.Length * 13 > obj.Columns[j].Width)
                        {
                            RowHight[j] = int.Parse(Math.Ceiling(double.Parse((strTemp.Length * 13).ToString()) / double.Parse(obj.Columns[j].Width.ToString())).ToString());
                            RowHight[j] *= RowHeight;
                        }
                        else
                            RowHight[j] = (int)obj.Rows[i].Height;
                    }
                    //排序
                    Collate(RowHight, IntCol);
                    obj.SetRowHeight(i,RowHight[IntCol - 1]);
                    //obj.Rows[i].Height = RowHight[IntCol - 1];
                    //if (i == (IntRow - 1))
                    //{
                    //    MessageBox.Show(i.ToString() + "行高" + obj.Rows[i].Height);
                    //}
                }

            }
            catch(System.Exception e)
            {
                //RowHight[j] = obj.Rows[IntRow].Hitht;
                MessageBox.Show(e.Message);
            }
        }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 设置行高,用来自动换行,每个单元格
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        public void SetRowHeight(FarPoint.Win.Spread.SheetView obj,int IntRow,int IntCol,object  objTitle)
        {
            int RowHight = 1;
            //double temp = 0.0001;
            //RowHight=temp.ToString().Length;
            //MessageBox.Show(temp.ToString()+"长度："+RowHight.ToString());
            //MessageBox.Show(obj.Columns[IntCol].Width.ToString());列宽为60
            //temp=Convert.ToDouble(Title);
            //char[] ArrayByte = objTitle.ToString().ToCharArray();
            try
            {
                if (objTitle.ToString().Length*12 > obj.Columns[IntCol].Width)
                {
                    RowHight = int.Parse(Math.Ceiling(double.Parse((objTitle.ToString().Length * 12).ToString()) / double.Parse(obj.Columns[IntCol].Width.ToString())).ToString());
                    //MessageBox.Show(RowHight.ToString());
                    obj.SetRowHeight(IntRow,(RowHight * 20));
                }
                else
                {
                     
                     obj.SetRowHeight(IntRow,(int)obj.Rows[IntRow].Height);
                 }            
           }
           catch (System.Exception e)
            {
                //int.TryParse(obj.Rows[IntRow].Height.ToString(), out RowHight);
                //obj.Rows[IntRow].Height = 20F;
            }

        }
        /// <summary>
        /// 设表格文本自动换行
        /// </summary>
        /// <param name="tempsheet"></param>
        public void Sheet_AutoLineFeed(FarPoint.Win.Spread.FpSpread obj)
        {
            FarPoint.Win.Spread.CellType.TextCellType cellType = new FarPoint.Win.Spread.CellType.TextCellType();
            cellType.WordWrap = true;
            for (int i = 0; i < obj.Sheets.Count; i++)
            {
                for (int col = 0; col < obj.Sheets[i].ColumnCount; col++)
                {
                    obj.Sheets[i].Columns[col].CellType = cellType;
                }
            }


        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="IntArray"></param>
        private  void Collate(int [] IntArray,int IndexArray)
        {
            bool  exChange; // 交换标志
            int i=0;
            int j=0;
            int temp = 0;
            for(i=0;i<IndexArray;++i)
            {
                exChange=false; // 本趟排序开始前，交换标志应为假 
                for (j = IndexArray - 2; j >= i; j--)// 对当前无序区 IntArray[i..n] 自下向上扫描 
                {
                    if (IntArray[j + 1] < IntArray[j])
                    {
                        temp = IntArray[j + 1]; //IntArray[0] 不是哨兵，仅做暂存单元
                        //IntArray[0]=IntArray[j+1];
                        IntArray[j + 1] = IntArray[j];
                        //IntArray[j]=IntArray[0];
                        IntArray[j] = temp;
                        exChange = true; // 发生了交换，故将交换标志置为真
                    }
                    //else
                    //{
                    //    exChange = false;
                    //}
                }
                if (!exChange)
                {
                    return;
                }
            }
        }
        /// <summary>
        /// 本方法给表格设边框线并同时单元格内容水平垂直均居中
        /// </summary>
        /// <param name="tempsheet"></param>
        public void Sheet_GridandCenter(FarPoint.Win.Spread.SheetView obj)
        {
            //定义一个边框线
            LineBorder border = new LineBorder(Color.Black);
            int rowcount = obj.Rows.Count;
            int colcount = obj.Columns.Count;
            //for (int i = 0; i < rowcount; i++)
            //{
            //    ////设表格线
            //    //obj.Rows[i].Border = border;
            //    //水平和垂直均居中对齐
            //    //obj.Rows[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    //obj.Rows[i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //}
            for (int i = 0; i < rowcount; ++i)
            {
                obj.Rows[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                obj.Rows[i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                for (int j = 0; j < colcount; j++)
                {
                    //设表格线
                    obj.Cells[i,j].Border = border;
                    //水平和垂直均居中对齐
                    //obj.Columns[j].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    //obj.Columns[j].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                }
            }
        }
        /// <summary>
        /// 返回列标题
        /// </summary>
        /// <returns></returns>
        public string GetColumnTitle(int IntCol)
        {
            //stirng[] strColTitle = new string[obj.ColumnCount];
            string temp=null;
                switch(IntCol)
                {
                    case 0:
                        temp = "A";
                        break;
                    case 1:
                        temp = "B";
                        break;
                    case 2:
                        temp = "C";
                        break;
                    case 3:
                        temp = "D";
                        break;
                    case 4:
                        temp = "E";
                        break;
                    case 5:
                        temp = "F";
                        break;
                    case 6:
                        temp = "G";
                        break;
                    case 7:
                        temp = "H";
                        break;
                    case 8:
                        temp = "I";
                        break;
                    case 9:
                        temp = "J";
                        break;
                    case 10:
                        temp = "K";
                        break;
                    case 11:
                        temp = "L";
                        break;
                    case 12:
                        temp = "M";
                        break;
                    case 13:
                        temp = "N";
                        break;
                    case 14:
                        temp = "O";
                        break;
                    case 15:
                        temp = "P";
                        break;
                    case 16:
                        temp = "Q";
                        break;
                    case 17:
                        temp = "R";
                        break;
                    case 18:
                        temp = "S";
                        break;
                    case 19:
                        temp = "T";
                        break;
                    case 20:
                        temp = "U";
                        break;
                    case 21:
                        temp = "V";
                        break;
                    case 22:
                        temp = "W";
                        break;
                    case 23:
                        temp = "X";
                        break;
                    case 24:
                        temp = "Y";
                        break;
                    case 25:
                        temp = "Z";
                        break;

                    default:
                        break;
                }
                return temp;
        }
        /// <summary>
        /// 设置列宽，每个列最长的字符串为最大宽度
        /// </summary>
        public void SetWholeColumnsWidth(FarPoint.Win.Spread.SheetView obj)
        {
            int[] ArrColumnWidth = new int[obj.RowCount];
            const int ColumnWidth = 50;//默认的列宽
            const int Pixe = 13;//一个汉字是十个字节这里我多加2个
            string strTemp = null;
            try
            {
                for (int j = 0; j < obj.ColumnCount; ++j)//列
                {
                    for (int i = 0; i < obj.RowCount; ++i)//行
                    {
                        if (obj.GetValue(i, j) == null)
                        {
                            strTemp = "0";
                        }
                        else
                        {
                            strTemp = obj.GetValue(i, j).ToString();
                        }
                        if (strTemp.Length * Pixe > obj.Columns[j].Width)
                        {
                            ArrColumnWidth[i] = strTemp.Length * Pixe;
                        }
                        else
                            ArrColumnWidth[i] = (int)obj.Columns[j].Width;
                    }
                    //排序
                    Collate(ArrColumnWidth, obj.RowCount);
                    obj.SetColumnWidth(j, ArrColumnWidth[obj.RowCount - 1]);
                }
            }
            catch(System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 用来返回上一级合并单元格目录的内容,对象是object
        /// </summary>
        /// <returns></returns>
        /// <param name="col">要返回字符串所在的列</param>
        /// <param name="row">当前所在行</param>
        public object ReturnStr(FarPoint.Win.Spread.SheetView obj, int row, int col)
        {
            if (obj.Cells[row, col].Value != null)
            {
                return obj.Cells[row, col].Value;
            }
            else
            {
                if (row != 0)
                {
                    return ReturnStr(obj, row - 1, col);
                }
                else
                {
                    return "error!!!";
                }
            }
        }
    }
}
