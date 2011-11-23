using System;
using System.Collections.Generic;
using System.Text;

using Itop.Domain.Table;
using Itop.Domain.Forecast;
using Itop.Client.Common;


/******************************************************************************************************
 *  ClassName：Sheet2_1
 *  Action：用来生成Sheet2_1报表的调用方法
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表Ps_History
 * 时间：2010-10-11。
 *********************************************************************************************************/
namespace Itop.JournalSheet.Function
{
    class Sheet2_N
    {
        private int GlobalFirstYear = 0;
        private int GlobalEndYear = 0;
        private int IntType = 1;
        private const int FixationCol = 2;//年份下面有两个固定列，在这里设初始值为以后改变方便
        private string TwoParentID = null;
        private string ThreeParentID = null;
        private float []floatSum=new float[5];//用来装，一产，二产，三产的值

        private Itop.Client.Base.FormBase GlobalFormBase = null;
        private Itop.JournalSheet.Function.PublicFunction PF = new PublicFunction();
        private Dictionary<string, Ps_History> resualt = new Dictionary<string, Ps_History>();
        private FarPoint.Win.Spread.CellType.PercentCellType PC = new FarPoint.Win.Spread.CellType.PercentCellType();

        public int GolobalYearCount = 0;
        public int GolobalYears = 0;//记录有几个年份没有数据
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
            py.Col5 = FB.ProjectUID;

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
            GolobalYearCount = GlobalEndYear - GlobalFirstYear + FixationCol + 1;//后面有两个固定列,加1是起始年的加入
            SetYearsSheet2_1(FB, obj, GlobalFirstYear, GlobalEndYear, GolobalYearCount, IntRow, IntCol, RowStep, ColStep);
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
        private void SetYearsSheet2_1(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, int firstyear, int endyear, int YearsCount, int IntRow, int IntCol, int RowStep, int ColStep)
        {

            GolobalYears = 0;
            string Title = null;
            string Title1 = "";
            string SingleTitle = "全地区GDP（亿元）";
                int TempYear = firstyear;
                for (int i = IntRow; i < ((YearsCount + IntRow)); i++)
                {
                    if ((YearsCount + IntRow - 2) == i)
                    {
                        Title = "“十五”年均增长率";
                        PF.CreateSheetView(obj, RowStep, ColStep, ((i - GolobalYears)), IntCol, Title);
                        //PF.SetRowHight(obj, ((i - GolobalYears)), IntCol, Title);
                        FifteenYears(obj, (i - GolobalYears), IntCol);

                    }
                    else if ((YearsCount + IntRow - 1) == i)
                    {

                        if (TempYear < 2011)//这里写2011要注意
                        {
                            Title = "2006-2009年均增长率";
                            ElevenFiveYear(obj, Title, (i - GolobalYears), IntCol);
                        }
                        else
                        {
                            Title = "“十一五”年均增长率";
                            ElevenFiveYear(obj, Title, (i - GolobalYears), IntCol);
                        }

                        PF.CreateSheetView(obj, RowStep, ColStep, ((i - GolobalYears) ), IntCol, Title);
                        PF.SetSheetViewColumnsWidth(obj, IntCol, Title1);
                        //PF.SetRowHight(obj, ((i - GolobalYears)), IntCol, Title);
                    }
                    else
                    {
                        Title = TempYear + " 年";
                        if (TempYear != 2001 && TempYear != 2002 && TempYear != 2003 && TempYear != 2004)
                        {
                            PF.CreateSheetView(obj, RowStep, ColStep, ((i - GolobalYears) ), IntCol, Title);

                            Title1 = "一产";
                            SingleTitle = "全地区GDP（亿元）";
                            ViewSheet2_1Data(obj, Title1, FB, TempYear, ((i - GolobalYears)), (IntCol + 1), SingleTitle);
                            SetCellType(obj, PC, (i - GolobalYears), IntCol + 1);
                            //PF.SetRowHight(obj, ((i - GolobalYears)), IntCol+1, Title);

                            Title1 = "二产";
                            ViewSheet2_1Data(obj, Title1, FB, TempYear, ((i - GolobalYears)), (IntCol + 2), SingleTitle);
                            SetCellType(obj, PC, (i - GolobalYears), IntCol + 2);

                            Title1 = "三产";
                            ViewSheet2_1Data(obj, Title1, FB, TempYear, ((i - GolobalYears)), (IntCol + 3), SingleTitle);
                            SetCellType(obj, PC, (i - GolobalYears), IntCol + 3);

                            SingleTitle = "";

                            Title1 = "城镇人口（万人）";
                            ViewSheet2_1Data(obj, Title1, FB, TempYear, ((i - GolobalYears)), (IntCol + 6), SingleTitle);
                            //PF.SetRowHight(obj, ((i - GolobalYears)), (IntCol + 6), Title1);

                            Title1 = "乡村人口（万人）";
                            ViewSheet2_1Data(obj, Title1, FB, TempYear, ((i - GolobalYears)), (IntCol + 7), SingleTitle);
                            //PF.SetRowHight(obj, ((i - GolobalYears)), (IntCol + 6), Title1);

                            Title1 = "年末总人口（万人）";
                            AccountPopulation(obj, TempYear, ((i - GolobalYears)), (IntCol + 4));
                            //PF.SetRowHight(obj,  ((i - GolobalYears)), (IntCol + 6), Title1);

                            Title1 = "人均GDP（万元/人）";
                            PerCapitaGDP(obj,  FB, Title1,TempYear, ((i - GolobalYears)), (IntCol + 5));
                            //PF.SetRowHight(obj, ((i - GolobalYears)), (IntCol + 6), Title1);



                            Title1 = "城镇化率（%）";
                            AccountUrbanizationRate(obj, TempYear, ((i - GolobalYears)), (IntCol + 8));
                            SetCellType(obj, PC, (i - GolobalYears), IntCol + 8);
                            //PF.SetRowHight(obj, ((i - GolobalYears)), (IntCol + 8), Title1);
                        }
                        else
                        {
                            GolobalYears += 1;//年份没有数据的总和
                        }
                        resualt.Clear();
                        ++TempYear;
                    }
            }
        }
        /// <summary>
        /// 显示数据
        /// </summary>
        private void ViewSheet2_1Data(FarPoint.Win.Spread.SheetView obj, string Title, Itop.Client.Base.FormBase FB, int FistYear, int IntRow, int IntCol, string SingleTitle)
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
            //if (Title != "年末总人口（万人）" && Title != "城镇化率（%）")
            //{
                if ((Title == "一产") || (Title == "二产") || (Title == "三产") )
                    {
                        //con = "Title='" + SingleTitle + "'AND Col4='" + FB.ProjectUID + "'AND Forecast='" + IntType + "'";
                        //GDP1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
                        //TwoParentID = GDP1.ID;
                        //if (Title == "一产")
                        //{
                        //    resualt.Add(SingleTitle, GDP1);
                        //}

                        //con = "Title='" + Title + "'AND Col4='" + FB.ProjectUID + "'AND Forecast='" + IntType + "'";
                        //con += "AND ParentID='" + TwoParentID + "'";
                        //GDP1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
                        //resualt.Add(Title, GDP1);
                    }
                    else
                    {
                        con = "Title='" + Title + "'AND Col4='" + FB.ProjectUID + "'AND Forecast='" + IntType + "'";
                        GDP1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
                        
                        resualt.Add(Title, GDP1);
                    }

                    ////判断是否年份中有数字，没有就不显示
                    //if (Gethistroyvalue<Ps_History>(resualt[Title], Temp) > 0.0000 || Gethistroyvalue<Ps_History>(resualt[Title], Temp) <0.0000)
                    if (FistYear != 2001 && FistYear != 2002 && FistYear != 2003 && FistYear != 2004)
                    {
                        if ((Title == "一产") || (Title == "二产") || (Title == "三产"))
                        {
                            floatSum[4] = SumTertiaryIndustry(SingleTitle, FB,Temp,Title);
                           obj.SetValue(IntRow, IntCol, ((PF.Gethistroyvalue<Ps_History>(resualt[Title], Temp))/floatSum[4]));

                        }
                        else
                        {
                            obj.SetValue(IntRow, IntCol, PF.Gethistroyvalue<Ps_History>(resualt[Title], Temp));
                        }
                    }
                //}
        }
        /// <summary>
        /// 设置单元格的类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ICT"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        public void SetCellType(FarPoint.Win.Spread.SheetView obj,FarPoint.Win.Spread.CellType.ICellType ICT,int IntRow,int IntCol)
        {
            obj.Cells[IntRow, IntCol].CellType = ICT;
        }
        /// <summary>
        /// 计算三产的和
        /// </summary>
        /// <returns></returns>
        private float SumTertiaryIndustry(string SingleTitle, Itop.Client.Base.FormBase FB, string   years,string Title)
        {
            Ps_History GDP1 = null;
            string con=null;
            con = "Title='" + SingleTitle + "'AND Col4='" + FB.ProjectUID + "'AND Forecast='" + IntType + "'";
            GDP1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
            TwoParentID = GDP1.ID;
            if (Title == "一产")
            {
                Add(FB, "一产");
                Add(FB, "二产");
                Add(FB, "三产");
            }

            floatSum[0] = (float)PF.Gethistroyvalue<Ps_History>(resualt["一产"], years);
            floatSum[1] = (float)PF.Gethistroyvalue<Ps_History>(resualt["二产"], years);
            floatSum[2] = (float)PF.Gethistroyvalue<Ps_History>(resualt["三产"], years);

            floatSum[3] = floatSum[0]+floatSum[1]+floatSum[2];
            return floatSum[3];

        }
        /// <summary>
        /// 添加一产二产三产值
        /// </summary>
        /// <param name="Title"></param>
        private void Add(Itop.Client.Base.FormBase FB,string Title)
        {
            Ps_History GDP1 = null;
            string con = null;
            con = "Title='" + Title + "'AND Col4='" + FB.ProjectUID + "'AND Forecast='" + IntType + "'";
            con += "AND ParentID='" + TwoParentID + "'";
            GDP1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
            resualt.Add(Title, GDP1);
        }
        /// <summary>
        /// 计算人口总数
        /// </summary>
        /// <param name="obj">SheetView Object</param>
        /// <param name="Years">current year</param>
        /// <param name="IntRow">current row</param>
        /// <param name="IntCol">current column</param>
        private void AccountPopulation(FarPoint.Win.Spread.SheetView obj, int   Years, int IntRow, int IntCol)
        {
            string Temp = "y" + Years.ToString();
            float Temp2 = 0.0F;
            float Temp1 = 0.0F;
            Temp2 = (float)PF.Gethistroyvalue<Ps_History>(resualt["城镇人口（万人）"], Temp);
            Temp1 = (float)PF.Gethistroyvalue<Ps_History>(resualt["乡村人口（万人）"], Temp);
           obj.SetValue(IntRow, IntCol,PF.ReturnFormatStr(Temp2+Temp1));

        }
        /// <summary>
        /// 计算城镇化率
        /// </summary>
        /// <param name="obj">SheetView Object</param>
        /// <param name="Years">current year</param>
        /// <param name="IntRow">current row</param>
        /// <param name="IntCol">current column</param>
        private void AccountUrbanizationRate(FarPoint.Win.Spread.SheetView obj, int Years, int IntRow, int IntCol)
        {
            float Temp = 0;
            string Temp1 = "y" + Years.ToString();
            Temp = (float)PF.Gethistroyvalue<Ps_History>(resualt["城镇人口（万人）"], Temp1);

            obj.SetValue(IntRow, IntCol,PF.ReturnFormatStr( Temp/StrToFloat(obj.GetValue((IntRow ), IntCol-4).ToString())));
        }
        /// <summary>
        /// 人均GDP
        /// </summary>
        /// <param name="obj">SheetView object</param>
        /// <param name="Title">title</param>
        /// <param name="Years">current year</param>
        /// <param name="IntRow">current row</param>
        /// <param name="IntCol">current column</param>
        private void PerCapitaGDP(FarPoint.Win.Spread.SheetView obj,Itop.Client.Base.FormBase FB,string Title, int  Years, int IntRow, int IntCol)
        {
            float Temp = 0;
            string Temp1= "y" + Years.ToString();
            Ps_History GDP1 = null;
            string con = null;
            con = "Title='" + "全地区GDP（亿元）" + "'AND Col4='" + FB.ProjectUID + "'AND Forecast='" + IntType + "'";
            GDP1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
            resualt.Add("全地区GDP（亿元）", GDP1);

            Temp = (float)PF.Gethistroyvalue<Ps_History>(resualt["全地区GDP（亿元）"], Temp1);

            obj.SetValue(IntRow, IntCol, Temp / StrToFloat(obj.GetValue((IntRow), IntCol - 1).ToString()));
            //PF.SetRowHight(obj,IntRow, IntCol, Temp / StrToFloat(obj.GetValue((IntRow), IntCol - 1).ToString()));
        }
        /// <summary>
        /// “十五”年均增长率
        /// </summary>
        private void FifteenYears(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            string strFirstYear = "";
            string strNextYear = "";
            double  Pitch = 0;

            strFirstYear = obj.GetValue(5,0 ).ToString();
            strNextYear = obj.GetValue(6,0 ).ToString();
            Pitch = (double)PF.YearsPitch(strFirstYear, strNextYear);
            Pitch = (double)(1 / Pitch);
            //年末总人口（万人）
            SetCellType(obj, PC, (IntRow), IntCol + 4);
            obj.Cells[IntRow, IntCol + 4].Formula = "POWER(((E" + (7) + ")/E" + (6) + "), " + Pitch + ")-1";
            //人均GDP（万元/人）
            SetCellType(obj, PC, (IntRow), IntCol + 5);
            obj.Cells[IntRow, IntCol + 5].Formula = "POWER(((F" + (7) + ")/F" + (6) + "), " + Pitch + ")-1";
            //城镇人口（万人）
            SetCellType(obj, PC, (IntRow), IntCol + 6);
            obj.Cells[IntRow, IntCol + 6].Formula = "POWER(((G" + (7) + ")/G" + (6) + "), " + Pitch + ")-1";
            //乡村人口（万人）
            SetCellType(obj, PC, (IntRow), IntCol + 7);
            obj.Cells[IntRow, IntCol + 7].Formula = "POWER(((H" + (7) + ")/H" + (6) + "), " + Pitch + ")-1";
            
        }
        /// <summary>
        /// 十一五，还是2006-2009
        /// </summary>
        private void ElevenFiveYear(FarPoint.Win.Spread.SheetView obj, string Title,int IntRow, int IntCol)
        {
            int TwoThousandAndSix = 0;//具体哪行是2005年的
            int TwoThousandAndNine = 0;
            double Pitch = 0;

            if (Title == "2006-2009年均增长率")
            {
                TwoThousandAndNine = YearRow1(obj, "2009 年", 5, 0);
                TwoThousandAndSix = YearRow1(obj, "2005 年", 5, 0);
                Pitch = (double)PF.YearsPitch("2005 年", "2009 年");
                Pitch = (double)(1 / Pitch);

                //年末总人口（万人）
                SetCellType(obj, PC, (IntRow), IntCol + 4);
                obj.Cells[IntRow, IntCol + 4].Formula = "POWER(((E" + TwoThousandAndNine + ")/E" + TwoThousandAndSix + "), " + Pitch + ")-1";
                //人均GDP（万元/人）
                SetCellType(obj, PC, (IntRow), IntCol + 5);
                obj.Cells[IntRow, IntCol + 5].Formula = "POWER(((F" + TwoThousandAndNine + ")/F" + TwoThousandAndSix + "), " + Pitch + ")-1";
                //城镇人口（万人）
                SetCellType(obj, PC, (IntRow), IntCol + 6);
                obj.Cells[IntRow, IntCol + 6].Formula = "POWER(((G" + TwoThousandAndNine + ")/G" + TwoThousandAndSix + "), " + Pitch + ")-1";
                //乡村人口（万人）
                SetCellType(obj, PC, (IntRow), IntCol + 7);
                obj.Cells[IntRow, IntCol + 7].Formula = "POWER(((H" + TwoThousandAndNine + ")/H" + TwoThousandAndSix + "), " + Pitch + ")-1";
            }
           if( Title == "“十一五”年均增长率")
           {
               TwoThousandAndNine = YearRow1(obj, "2010 年", 5, 0);
               TwoThousandAndSix = YearRow1(obj, "2005 年", 5, 0);
               Pitch = (double)PF.YearsPitch("2005 年", "2010 年");
               Pitch = (double)(1 / Pitch);

               //年末总人口（万人）
               SetCellType(obj, PC, (IntRow), IntCol + 4);
               obj.Cells[IntRow, IntCol + 4].Formula = "POWER(((E" + TwoThousandAndNine + ")/E" + TwoThousandAndSix + "), " + Pitch + ")-1";
               //人均GDP（万元/人）
               SetCellType(obj, PC, (IntRow), IntCol + 5);
               obj.Cells[IntRow, IntCol + 5].Formula = "POWER(((F" + TwoThousandAndNine + ")/F" + TwoThousandAndSix + "), " + Pitch + ")-1";
               //城镇人口（万人）
               SetCellType(obj, PC, (IntRow), IntCol + 6);
               obj.Cells[IntRow, IntCol + 6].Formula = "POWER(((G" + TwoThousandAndNine + ")/G" + TwoThousandAndSix + "), " + Pitch + ")-1";
               //乡村人口（万人）
               SetCellType(obj, PC, (IntRow), IntCol + 7);
               obj.Cells[IntRow, IntCol + 7].Formula = "POWER(((H" + TwoThousandAndNine + ")/H" + TwoThousandAndSix + "), " + Pitch + ")-1";
           }
       }
        /// <summary>
        /// str to float
        /// </summary>
        /// <param name="FloatString">Object</param>
        /// <returns>float</returns>
        public float StrToFloat(object FloatString)
         {
             float result=0.0F;
            if (FloatString != null)
             {
                 if (float.TryParse(FloatString.ToString(), out result))
                     return result;
                 else
                 {
                   return (float)0.00;
                }
            }
           else
           {
                return (float)0.00;
           }  
      }
      /// <summary>
      /// 通过标题查找当前标题在哪行
      /// </summary>
      /// <param name="Title">标题</param>
      /// <param name="obj">sheetView对象</param>
      /// <param name="IntRow">起始行</param>
      /// <param name="IntCol">起始列</param>
      /// <returns>行数</returns>
      private  int YearRow1(FarPoint.Win.Spread.SheetView obj, string Title, int IntRow, int IntCol)
      {
          int floor = GolobalYearCount - GolobalYears ;
          int TempRow = 0;
          string strTemp = null;
          for (int i = 1; i < floor; i++)
          {
              strTemp = obj.GetValue(IntRow + i-1 , IntCol ).ToString();
              if (strTemp == Title)
              {
                  TempRow = IntRow + i;//行数从0开始所以不用-1
                  break;
              }
          }
          return TempRow;
      }
      ///// <summary>
      ///// 返回保留2位小数
      ///// </summary>
      ///// <param name="dou">要传入的数值</param>
      ///// <returns></returns>
      //private string ReturnFormatStr(double dou)
      //{
      //    string Place = null;
      //    Place = dou.ToString();
      //    Place = string.Format("{0:F}", dou);
      //    return Place;
      //}
  }


}
