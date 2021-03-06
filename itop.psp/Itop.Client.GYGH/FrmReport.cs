using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Itop.Client.Common;
using Itop.Domain.Layouts;
using Itop.Domain.Table;
using Itop.Common;
using Itop.Domain.Graphics;
using Itop.Domain.Stutistic;

using System.Reflection;
using System.Diagnostics;
using DevExpress.Utils;
using Itop.Domain.RightManager;
using Itop.Client.Base;
using FarPoint.Win;
using Itop.Domain.Forecast;
using Microsoft.Office.Interop.Excel;

namespace Itop.Client.GYGH
{
    public partial class FrmReport : FormBase
    {
        

        fpcommon fc = new fpcommon();
        
        //定义一个边框线
        LineBorder border = new LineBorder(Color.Black);
        //工程号
        string ProjID = Itop.Client.MIS.ProgUID;
        public FrmReport()
        {
            InitializeComponent();
        }
        private void FrmGYDWGH_Load(object sender, EventArgs e)
        {

            //根据窗口变化全部添满
            fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            //添加表
            fpSpread1_addsheet();
            
            
        }
        private void SpreadRemoveEmptyCells(FarPoint.Win.Spread.FpSpread tempspread)
        {
            //本方法用于去掉多表格中多余的行和列（空）
            //定义无空单元格模式
           FarPoint.Win.Spread.Model.INonEmptyCells nec;
            //计算spread有多少个表格
           int sheetscount = tempspread.Sheets.Count;
            //定义行数
           int rowcount = 0;
            //定义列数
           int colcount = 0;
           for (int m = 0; m < sheetscount;m++ )
           {
               nec = (FarPoint.Win.Spread.Model.INonEmptyCells)tempspread.Sheets[m].Models.Data;
               //计算无空单元格列数
               colcount= nec.NonEmptyColumnCount;
               //计算无空单元格行数
               rowcount = nec.NonEmptyRowCount;
               tempspread.Sheets[m].RowCount = rowcount;
               tempspread.Sheets[m].ColumnCount = colcount;
           }
        }
        private void fpSpread1_addsheet()
        {
            WaitDialogForm wait = null;
            wait = new WaitDialogForm("", "正在加载数据, 请稍候...");
            try
            {
                //打开Excel表格
                fpSpread1.Sheets.Clear();
                fpSpread1.OpenExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\ztj.xls");
                SpreadRemoveEmptyCells(fpSpread1);
            }
            catch (System.Exception e)
            {
                //如果打开出错则重新生成并保存
                fpSpread1.Sheets.Clear();
                Firstadddata();
                //判断文件夹是否存在，不存在则创建
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\xls"))
                {
                    Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\xls");
                }
                fpSpread1.SaveExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\ztj.xls");
            }
            wait.Close();
        }   
        private void  recreatsheetbydt(string title, System.Data.DataTable dt, Dictionary<string, Columqk> viscol, int sheetindex)
        {
            FarPoint.Win.Spread.SheetView Sheet1 = new FarPoint.Win.Spread.SheetView();
            if (fpSpread1.Sheets.Count-1>=sheetindex)
            {
                Sheet1 = fpSpread1.Sheets[sheetindex];
            }
            else
            {
                Sheet1.SheetName = title;
                fpSpread1.Sheets.Add(Sheet1);
            }
            if (Sheet1.Rows.Count>0)
            {
                Sheet1.RowCount = 0;
                Sheet1.ColumnCount = 0;
            }
            Sheet1.Columns.Count = viscol.Count;
            Sheet1.Rows.Count = dt.Rows.Count + 1;
            //设表格线和居中
            fc.Sheet_GridandCenter(Sheet1);
            //设表格模式为R1C1
            fc.Sheet_Referen_R1C1(Sheet1);
            int visrowcount = 0;
            if (dt.Rows.Count > 0)
            {
                foreach (KeyValuePair<string, Columqk> kp in viscol)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (kp.Key == dt.Columns[j].ColumnName)
                        {
                            Sheet1.SetValue(0, visrowcount, viscol[dt.Columns[j].ColumnName].colname);
                            Sheet1.Columns[visrowcount].Width = 70;
                            if (viscol[dt.Columns[j].ColumnName].CellType == 1)
                            {
                                Sheet1.Columns[visrowcount].CellType = new FarPoint.Win.Spread.CellType.PercentCellType();
                            }
                            else if (viscol[dt.Columns[j].ColumnName].CellType == 2)
                            {
                                FarPoint.Win.Spread.CellType.NumberCellType newcelltype = new FarPoint.Win.Spread.CellType.NumberCellType();
                                newcelltype.DecimalPlaces = viscol[dt.Columns[j].ColumnName].weishu;
                                Sheet1.Columns[visrowcount].CellType = newcelltype;

                            }

                            for (int k = 0; k < dt.Rows.Count; k++)
                            {
                                Sheet1.SetValue(k + 1, visrowcount, dt.Rows[k][j].ToString());
                            }
                            visrowcount++;
                        }
                    }
                }
                //for (int i = 0; i < dt.Columns.Count; i++)
                //{
                //    if (viscol.ContainsKey(dt.Columns[i].ColumnName))
                //    {
                //        Sheet1.SetValue(0, visrowcount, viscol[dt.Columns[i].ColumnName].colname);
                //        Sheet1.Columns[visrowcount].Width = 70;
                //        if (viscol[dt.Columns[i].ColumnName].CellType == 1)
                //        {
                //            Sheet1.Columns[visrowcount].CellType = new FarPoint.Win.Spread.CellType.PercentCellType();
                //        }
                //        else if (viscol[dt.Columns[i].ColumnName].CellType == 2)
                //        {
                //            FarPoint.Win.Spread.CellType.NumberCellType newcelltype = new FarPoint.Win.Spread.CellType.NumberCellType();
                //            newcelltype.DecimalPlaces = viscol[dt.Columns[i].ColumnName].weishu;
                //            Sheet1.Columns[visrowcount].CellType = newcelltype;

                //        }

                //        for (int j = 0; j < dt.Rows.Count; j++)
                //        {
                //            Sheet1.SetValue(j + 1, visrowcount, dt.Rows[j][i].ToString());
                //        }
                //        visrowcount++;
                //    }

                //}
                Sheet1.Rows[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            }

            //使表格只读
            fc.Sheet_Locked(Sheet1);
        }
        /// <summary>
        /// 根据datatable创建一个工作表
        /// </summary>
        /// <param name="title"> 报表标题</param>
        /// <param name="dt">数据源</param>
        /// <param name="viscol">控制显示的列 列明为键 新名称为值</param>
        /// <param name="bl">图表是否带有图例，是true，否false</param>
        private void creatsheetbydt(string title, System.Data.DataTable dt, Dictionary<string, Columqk> viscol)
        {
            FarPoint.Win.Spread.SheetView Sheet1 = new FarPoint.Win.Spread.SheetView();
            Sheet1.SheetName = title;
            fpSpread1.Sheets.Add(Sheet1);
            Sheet1.Columns.Count = viscol.Count;
            Sheet1.Rows.Count = dt.Rows.Count + 1;
            //设表格线和居中
            fc.Sheet_GridandCenter(Sheet1);
            //设表格模式为R1C1
            fc.Sheet_Referen_R1C1(Sheet1);
            int visrowcount = 0;
            if (dt.Rows.Count>0)
            { 
               
                foreach ( KeyValuePair<string,Columqk> kp in viscol)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (kp.Key == dt.Columns[j].ColumnName)
                        {
                            Sheet1.SetValue(0, visrowcount, viscol[dt.Columns[j].ColumnName].colname);
                            Sheet1.Columns[visrowcount].Width = 70;
                            if (viscol[dt.Columns[j].ColumnName].CellType == 1)
                            {
                                Sheet1.Columns[visrowcount].CellType = new FarPoint.Win.Spread.CellType.PercentCellType();
                            }
                            else if (viscol[dt.Columns[j].ColumnName].CellType == 2)
                            {
                                FarPoint.Win.Spread.CellType.NumberCellType newcelltype = new FarPoint.Win.Spread.CellType.NumberCellType();
                                newcelltype.DecimalPlaces = viscol[dt.Columns[j].ColumnName].weishu;
                                Sheet1.Columns[visrowcount].CellType = newcelltype;

                            }

                            for (int k = 0; k < dt.Rows.Count; k++)
                            {
                                Sheet1.SetValue(k + 1, visrowcount, dt.Rows[k][j].ToString());
                            }
                            visrowcount++;
                        }
                    }
                }
             
            //    for (int i = 0; i < dt.Columns.Count;i++ )
            //   {
            //    if (viscol.ContainsKey(dt.Columns[i].ColumnName))
            //    {
            //        Sheet1.SetValue(0, visrowcount, viscol[dt.Columns[i].ColumnName].colname);
            //        Sheet1.Columns[visrowcount].Width = 70;
            //        if (viscol[dt.Columns[i].ColumnName].CellType == 1)
            //        {
            //            Sheet1.Columns[visrowcount].CellType = new FarPoint.Win.Spread.CellType.PercentCellType();
            //        }
            //        else if (viscol[dt.Columns[i].ColumnName].CellType == 2)
            //        {
            //            FarPoint.Win.Spread.CellType.NumberCellType newcelltype = new FarPoint.Win.Spread.CellType.NumberCellType();
            //            newcelltype.DecimalPlaces = viscol[dt.Columns[i].ColumnName].weishu;
            //            Sheet1.Columns[visrowcount].CellType = newcelltype;

            //        }

            //        for (int j = 0; j < dt.Rows.Count; j++)
            //        {
            //            Sheet1.SetValue(j + 1, visrowcount, dt.Rows[j][i].ToString());
            //        }
            //        visrowcount++;
            //    }
                                 
            //}
             Sheet1.Rows[0].CellType=new FarPoint.Win.Spread.CellType.TextCellType();
            }
            
            //使表格只读
            fc.Sheet_Locked(Sheet1);

        }
        private  void Firstadddata()
        {
            
            //逐步获取各个数据的datatable;

            //形成基础数据电力发展实绩历年地区生产
            build_dlhistoryGDP();
            //电力发展实绩分行业统计
            build_dlhistoryfhy();
            //电力发展实绩社会经济用电情况
            build_dlhistoryjjyd();
            //分区县供电实绩
            build_fqxgdsj();
            //全市拟建主要工业项目及用电需求情况表
            build_njxmandydl();
            //重点建设工业及用电需求表
            build_zdjsxm();
            //典型日最大负荷
            build_dxfh();
              //月最大负荷数据
            build_monthmax();
            //年最大负荷数据
        //IList<PS_Table_AreaWH> AreaList = null;

            build_YearMAX();
            //变电站情况表
            build_bdzqk();
             //输电线路情况表
            build_lineqk();


        }
#region 电力发展实际

        //形成基础数据电力发展实绩历年地区生产
        private void build_dlhistoryGDP()
        {
            Dictionary<string, Columqk> viscol = new Dictionary<string, Columqk>();
            string title = "电力发展实绩_历年地区生产总值";
            Ps_YearRange py = new Ps_YearRange();
            py.Col4 = "电力发展实绩";
            py.Col5 = Itop.Client.MIS.ProgUID;

            IList<Ps_YearRange> li = Itop.Client.Common.Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);
            int firstyear, endyear;
            if (li.Count > 0)
            {
                firstyear = li[0].StartYear;
                endyear = li[0].FinishYear;
            }
            else
            {
                firstyear = 2000;
                endyear = 2008;
                py.BeginYear = 1990;
                py.FinishYear = endyear;
                py.StartYear = firstyear;
                py.EndYear = 2060;
                py.ID = Guid.NewGuid().ToString();
                Itop.Client.Common.Services.BaseService.Create<Ps_YearRange>(py);
            }

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Year");
            viscol["Year"] = new Columqk("年份", 3, 2);
            dt.Columns.Add("GDP", typeof(double));
            viscol["GDP"] = new Columqk("GDP(亿元)", 2, 2);
            dt.Columns.Add("A", typeof(double));
            viscol["A"] = new Columqk("增长指数", 2, 2);
            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = 1;
            psp_Type.Col4 = Itop.Client.MIS.ProgUID;
            IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);
            System.Data.DataTable dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));

            DataRow[] rows1 = dataTable.Select("Title like '全地区GDP%'");
            DataRow[] rows2 = dataTable.Select("Title like '年末总人口%'");
            //找不到数据时给出提示
            if (rows1.Length == 0 || rows2.Length == 0)
            {
                MessageBox.Show("缺少‘全地区GDP’或‘年末总人口’ 数据,无法进行统计!");
                this.Close();
                return;
            }
            string pid = rows1[0]["ID"].ToString();
            DataRow[] rows3 = dataTable.Select("ParentID='" + pid + "'");

            int m = -1;
            for (int k = 0; k < rows3.Length; k++)
            {
                m++;
                dt.Columns.Add("m" + m, typeof(double));
                dt.Columns.Add("n" + m, typeof(double));

                viscol["m" + m] = new Columqk(rows3[k]["Title"].ToString(), 2, 2);


                viscol["n" + m] = new Columqk("比例(%)", 1, 2);
            }
            viscol["RK"] = new Columqk("人口(万人)", 2, 2);
            viscol["RJGDP"] = new Columqk("人均GDP（万元/人）", 1, 2);
            dt.Columns.Add("RK", typeof(double));
            dt.Columns.Add("RJGDP", typeof(double));
            double sum1 = 0;
            for (int i = firstyear; i <= endyear; i++)
            {
                //if (!ht.ContainsValue(i))
                //    continue;

                DataRow row = dt.NewRow();
                row["ID"] = Guid.NewGuid().ToString();
                row["Year"] = i;
                double sum = 0;
                try { sum = Convert.ToDouble(rows1[0]["y" + i]); }
                catch { }
                row["GDP"] = sum.ToString();

                if (i == firstyear)
                    sum1 = sum;

                if (sum1 != 0)
                    sum1 = sum * 100 / sum1;
                row["A"] = sum1.ToString("n2");
                sum1 = sum;
                for (int j = 0; j <= m; j++)
                {
                    double s = 0;
                    double y = 0;
                    try { s = Convert.ToDouble(rows3[j]["y" + i]); }
                    catch { }
                    row["m" + j] = s.ToString();
                    if (sum != 0)
                        y = s * 100 / sum;

                    row["n" + j] = detel_jd(y, 2);
                }
                double rk = 0;
                double rjgdp = 0;
                try { rk = Convert.ToDouble(rows2[0]["y" + i]); }
                catch { }
                row["RK"] = rk.ToString();

                if (rk != 0)
                    rjgdp = sum / rk;

                row["RJGDP"] = detel_jd(rjgdp, 4);
                dt.Rows.Add(row);

            }
            if (!shjjbyyear)
            {
                creatsheetbydt(title, dt, viscol);
            }

            else
            {
                recreatsheetbydt(title, dt, viscol, 0);
            }           


        }
//电力发展实绩分行业统计
        private void build_dlhistoryfhy()
        {
            Dictionary<string, Columqk> viscol = new Dictionary<string, Columqk>();
            string title = "电力发展实绩_分行业统计";
            Ps_YearRange py = new Ps_YearRange();
            py.Col4 = "电力发展实绩";
            py.Col5 = Itop.Client.MIS.ProgUID;
            int firstyear, endyear;
            IList<Ps_YearRange> li = Itop.Client.Common.Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);
            if (li.Count > 0)
            {
                firstyear = li[0].StartYear;
                endyear = li[0].FinishYear;
            }
            else
            {
                firstyear = 2000;
                endyear = 2008;
                py.BeginYear = 1990;
                py.FinishYear = endyear;
                py.StartYear = firstyear;
                py.EndYear = 2060;
                py.ID = Guid.NewGuid().ToString();
                Itop.Client.Common.Services.BaseService.Create<Ps_YearRange>(py);
            }

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Title");
            viscol["Title"] = new Columqk("标题", 3, 2);
            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = 1;
            psp_Type.Col4 = Itop.Client.MIS.ProgUID;
            IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);
            System.Data.DataTable dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));

            DataRow[] rows1 = dataTable.Select("Title like '全社会用电量%'");
            if (rows1.Length == 0)
            {
                MessageBox.Show("缺少‘全社会用电量’ 数据,无法进行统计!");
                this.Close();
                return;
            }
            string pid = rows1[0]["ID"].ToString();
            string tempTite = rows1[0]["Title"].ToString();
            //取出标题中的单位
           string Untis = fpcommon.FindUnits(tempTite);
            DataRow[] rows3 = dataTable.Select("ParentID='" + pid + "'");
            if (rows3.Length == 0)
            {
                MessageBox.Show("缺少‘全社会用电量’下的分行业用电数据,无法进行统计!");
                this.Close();
                return;
            }

            int m = -1;
            for (int i = firstyear; i <= endyear; i++)
            {
                //if (!ht.ContainsValue(i))
                //    continue;

                m++;
                dt.Columns.Add("y" + i, typeof(double));
                dt.Columns.Add("n" + i, typeof(double));
               
                viscol["n" + i] = new Columqk(i + "年百分比", 1, 2);
                
                if (Untis.Length > 0)
                {                  
                    viscol["y" + i] = new Columqk(i + "年" + "用电量(" + Untis + ")", 2, 2);
                }
                else
                {
                    viscol["y" + i] = new Columqk(i + "年" + "用电量", 2, 2);
                }
              
            }

            double sum = 0;
            try { sum = Convert.ToDouble(rows1[0]["y" + firstyear]); }
            catch { }


            DataRow row1 = dt.NewRow();
            row1["ID"] = Guid.NewGuid().ToString();
            row1["Title"] = "用电量总计";//rows1[0]["Title"].ToString();
            for (int k = 0; k < rows3.Length; k++)
            {



                DataRow row = dt.NewRow();
                row["ID"] = Guid.NewGuid().ToString();
                row["Title"] = rows3[k]["Title"].ToString();

                for (int j = firstyear; j <= endyear; j++)
                {
                    //if (!ht.ContainsValue(j))
                    //    continue;
                    sum = 0;
                    try
                    {
                        sum = Convert.ToDouble(rows1[0]["y" + j]);
                    }
                    catch { }

                    row1["y" + j] = sum;
                    row1["n" + j] = 1;
                    double sum1 = 0;
                    double sum2 = 0;
                    try { sum1 = Convert.ToDouble(rows3[k]["y" + j]); }
                    catch { }
                    row["y" + j] = sum1;
                    if (sum != 0)
                        sum2 = sum1 / sum;
                    row["n" + j] = sum2;
                }

                dt.Rows.Add(row);
            }
            dt.Rows.Add(row1);

            if (!shjjbyyear)
            {
                creatsheetbydt(title, dt, viscol);
            }

            else
            {
                recreatsheetbydt(title, dt, viscol, 1);
            }           

        }

        //电力发展实绩社会经济用电情况
        private void build_dlhistoryjjyd()
        {
           
            bool IsFist = true;
            int RealFistYear = 0; ;
            Dictionary<string, Columqk> viscol = new Dictionary<string, Columqk>();
            string title = "电力发展实绩_分行业统计";
            Ps_YearRange py = new Ps_YearRange();
            py.Col4 = "电力发展实绩";
            py.Col5 = Itop.Client.MIS.ProgUID;
            int firstyear, endyear;
            IList<Ps_YearRange> li = Itop.Client.Common.Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);
            if (li.Count > 0)
            {
                firstyear = li[0].StartYear;
                endyear = li[0].FinishYear;
            }
            else
            {
                firstyear = 1990;
                endyear = 2020;
                py.BeginYear = 1990;
                py.FinishYear = endyear;
                py.StartYear = firstyear;
                py.EndYear = 2060;
                py.ID = Guid.NewGuid().ToString();
                Itop.Client.Common.Services.BaseService.Create<Ps_YearRange>(py);
            }

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Title");
            viscol["Title"]=new Columqk("统计类别",3,2);
            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = 1;
            psp_Type.Col4 = Itop.Client.MIS.ProgUID;
            IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);
            System.Data.DataTable dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));

            DataRow[] rows1 = dataTable.Select("Title like '全地区GDP%'");
            // DataRow[] rows2 = dataTable.Select("Title like '全社会供电量%'");
            DataRow[] rows4 = dataTable.Select("Title like '全社会用电量%'");
            //DataRow[] rows5 = dataTable.Select("Title like '全社会最大负荷%'");
            DataRow[] rows7 = dataTable.Select("Title like '年末总人口%'");
            DataRow[] rows8 = dataTable.Select("Title='居民用电'");

            if (rows1.Length == 0)
            {
                MessageBox.Show("缺少全地区GDP数据!");
                this.Close();
                return;
            }
            //if (rows2.Length == 0)
            //{
            //    MessageBox.Show("缺少全社会供电量数据!");
            //    this.Close();
            //}
            if (rows4.Length == 0)
            {
                MessageBox.Show("缺少全社会用电量数据!");
                this.Close();
                return;
            }
            //if (rows5.Length == 0)
            //{
            //    MessageBox.Show("缺少全社会最大负荷数据!");
            //    this.Close();
            //}
            if (rows7.Length == 0)
            {
                MessageBox.Show("缺少年末总人口数据!");
                this.Close();
                return;
            }



            string GDPUnits = fpcommon.FindUnits(rows1[0]["Title"].ToString());
            //全社会供电量单位
            //AGdlUnits = Historytool.FindUnits(rows2[0]["Title"].ToString());
            //全社会用电量单位
            string AYdlUnits = fpcommon.FindUnits(rows4[0]["Title"].ToString());
            //全社会最大负荷单位
            // AMaxFhUnits = Historytool.FindUnits(rows5[0]["Title"].ToString());
            //年末总人口单位
           string NMARkUnits = fpcommon.FindUnits(rows7[0]["Title"].ToString());

            string pid = rows1[0]["ID"].ToString();
            string sid = rows4[0]["ID"].ToString();

            ///全地区GDP子类
            DataRow[] rows3 = dataTable.Select("ParentID='" + pid + "'");
            ///会社会用电量子类
            DataRow[] rows6 = dataTable.Select("ParentID='" + sid + "'");
            int m = -1;
           
            for (int i = firstyear; i <= endyear; i++)
            {
                //如果点击统计按钮
                if (shjjbyyear)
                {
                    if (!ht.ContainsValue(i))
                    continue;
                }
               
                if (IsFist)
                {
                    RealFistYear = i;
                    IsFist = false;
                }
                m++;
                dt.Columns.Add("y" + i, typeof(double));
                viscol["y"+i]=new Columqk(i+"年",2,2);
               

                //if (!ht1.ContainsValue(i))
                //    continue;
              
                dt.Columns.Add("m" + i, typeof(double));
                viscol["m"+i]=new Columqk("年增长率（%）",1,2);

            }

           
            double sum = 0;// 全地区GDP数据
            try { sum = Convert.ToDouble(rows1[0]["y" + firstyear]); }
            catch { }


            double sum51 = 0;// 全社会用电量数据
            try { sum51 = Convert.ToDouble(rows4[0]["y" + firstyear]); }
            catch { }
            double sum52 = 0;
            double sum53 = 0;

            double sum1 = 0;
            double sum2 = 0;
            double sum3 = 0;
            double sum4 = 0;
            double sum5 = 0;
            double sum6 = 0;
            double sum7 = 0;
            double sum8 = 0;
            double sum9 = 0;
            double sum10 = 0;
            double sum11 = 0;
            double sum12 = 0;
            double sum13 = 0;
            DataRow row = dt.NewRow();
            DataRow row3 = dt.NewRow();
            DataRow row4 = dt.NewRow();
            DataRow row5 = dt.NewRow();
            DataRow row6 = dt.NewRow();
            DataRow row7 = dt.NewRow();
            DataRow row8 = dt.NewRow();
            DataRow row9 = dt.NewRow();
            DataRow row10 = dt.NewRow();
            DataRow row11 = dt.NewRow();
            row["ID"] = Guid.NewGuid().ToString();
            row["Title"] = "一、地区生产总值(GDP," + GDPUnits + ")";

            m = firstyear;

            for (int j = firstyear; j <= endyear; j++)
            {
                if (shjjbyyear)
                {
                    if (!ht.ContainsValue(j))
                        continue;
                }
               
                try { sum1 = Convert.ToDouble(rows1[0]["y" + j]); }
                catch { }
                row["y" + j] = sum1;

                try { sum51 = Convert.ToDouble(rows4[0]["y" + j]); }
                catch { }

                if (m != firstyear)//表示不是第一年，以后的年份要算增长
                {
                    try { sum2 = Convert.ToDouble(rows1[0]["y" + (j - 1)]); }
                    catch { }

                    try { sum52 = Convert.ToDouble(rows4[0]["y" + (j - 1)]); }
                    catch { }
                    if (sum52 != 0)
                        sum53 = sum51 * 100 / sum52 - 100;//用电量增长


                    if (sum2 != 0)
                        sum3 = sum1 * 100 / sum2 - 100;//GDP增长 

                    row3["y" + j] = sum3;
                }
                else
                    row3["y" + j] = 1;

                try { sum4 = Convert.ToDouble(rows4[0]["y" + j]); }
                catch { }
                row4["y" + j] = sum4;//用电量



                //try { sum5 = Convert.ToDouble(rows5[0]["y" + j]); }
                //catch { }
                //row5["y" + j] = sum5;//最大负荷

                //if (sum5 != 0)
                //    sum6 = sum4 * 10000 / sum5;
                //row6["y" + j] = sum6;// 计算全社会最大负荷利用小时数

                //try { sum7 = Convert.ToDouble(rows2[0]["y" + j]); }
                //catch { }


                if (m != firstyear)
                {
                    //if (sum53 != 0)
                    //    sum8 = sum3 / sum53;
                    //row7["y" + j] = sum8;//原计算
                    if (sum3 != 0)
                        sum8 = sum53 / sum3;
                    row7["y" + j] = sum8;//弹性系数，电力消费增长速度与国民经济增长的比值lgm
                }
                else
                    row7["y" + j] = 1;

                try { sum9 = Convert.ToDouble(rows7[0]["y" + j]); }//年末人口
                catch { }

                if (sum9 != 0)
                {
                    if (AYdlUnits.Contains("亿") && NMARkUnits.Contains("万人"))//亿kWh  万人
                    {
                        sum10 = sum51 * 10000 / sum9;//人均用电量
                    }
                    else if (AYdlUnits.Contains("万") && NMARkUnits.Contains("万人"))//万kWh  万人
                    {
                        sum10 = sum51 / sum9;//人均用电量
                    }
                    else
                    {
                        MessageBox.Show("全社会用电量或年末总人口的单位错误！请用默认类别管理较对！（亿kWh,万kWh,万人）");
                        this.Close();
                        return;
                    }

                }
                row8["y" + j] = sum10;

                if (sum1 != 0)
                {
                    if (AYdlUnits.Contains("亿") && GDPUnits.Contains("亿元"))//亿kWh 亿元
                    {
                        sum11 = sum4 * 10000 / sum1;//单产耗能
                    }
                    else if (AYdlUnits.Contains("万") && GDPUnits.Contains("亿元"))//万kWh  亿元
                    {
                        sum11 = sum4 / sum1;//单产耗能
                    }
                    else
                    {
                        MessageBox.Show("全社会用电量或全地区GDP的单位错误！请用默认类别管理较对！（亿kWh,万kWh,亿元）");
                        this.Close();
                        return;
                    }
                }
                row9["y" + j] = sum11;//单产耗能

                if (rows8.Length > 0)
                {
                    try { sum12 = Convert.ToDouble(rows8[0]["y" + j]); }
                    catch { }
                    row11["y" + j] = sum12;

                    if (sum9 != 0)
                    {
                        if (AYdlUnits.Contains("亿") && NMARkUnits.Contains("万人"))//亿kWh 万人
                        {
                            sum13 = sum12 * 10000 / sum9;//居民用电
                        }
                        else if (AYdlUnits.Contains("万") && NMARkUnits.Contains("万人"))//万kWh  万人
                        {
                            sum13 = sum12 / sum9;//居民用电
                        }
                        else
                        {
                            MessageBox.Show("全社会用电量或年末总人口的单位错误！请用默认类别管理较对！（亿kWh,万kWh,万人）");
                            this.Close();
                            return;
                        }

                    }
                    row10["y" + j] = sum13;
                }
                else
                {
                    row10["y" + j] = sum13;
                }
                m++;
            }

            dt.Rows.Add(row);

            for (int k = 0; k < rows3.Length; k++)
            {
                double su1 = 0;
                double su2 = 0;
                double su3 = 0;



                DataRow ro1 = dt.NewRow();
                ro1["ID"] = Guid.NewGuid().ToString();
                ro1["Title"] = rows3[k]["Title"].ToString();

                DataRow ro2 = dt.NewRow();
                ro2["ID"] = Guid.NewGuid().ToString();
                ro2["Title"] = "比例（%）";

                for (int j = firstyear; j <= endyear; j++)
                {
                    if (shjjbyyear)
                    { 
                     if (!ht.ContainsValue(j))
                        continue;
                    }
                   
                    try { su1 = Convert.ToDouble(rows1[0]["y" + j]); }
                    catch { }
                    su2 = 0;
                    su3 = 0;
                    try { su2 = Convert.ToDouble(rows3[k]["y" + j]); }
                    catch { }
                    ro1["y" + j] = su2;

                    if (su1 != 0)
                        su3 = su2 * 100 / su1;
                    ro2["y" + j] = su3;

                }

                dt.Rows.Add(ro1);
                dt.Rows.Add(ro2);

            }

            row3["ID"] = Guid.NewGuid().ToString();
            row3["Title"] = "地区生产总值增长率（％）";
            dt.Rows.Add(row3);


            row4["ID"] = Guid.NewGuid().ToString();
            row4["Title"] = "二、全社会用电量(" + AYdlUnits + ")";
            dt.Rows.Add(row4);


            //row5["ID"] = Guid.NewGuid().ToString();
            //row5["Title"] = "最大负荷（万千瓦）";
            //dt.Rows.Add(row5);

            //row6["ID"] = Guid.NewGuid().ToString();
            //row6["Title"] = "最大负荷利用小时数(小时)";
            //dt.Rows.Add(row6);



            for (int k = 0; k < rows6.Length; k++)
            {
                double su1 = 0;
                double su2 = 0;
                double su3 = 0;



                DataRow ro1 = dt.NewRow();
                ro1["ID"] = Guid.NewGuid().ToString();
                ro1["Title"] = rows6[k]["Title"].ToString();

                DataRow ro2 = dt.NewRow();
                ro2["ID"] = Guid.NewGuid().ToString();
                ro2["Title"] = "比例（%）";

                for (int j = firstyear; j <= endyear; j++)
                {
                    if (shjjbyyear)
                    {
                        if (!ht.ContainsValue(j))
                            continue;
                    }
                   
                    try { su1 = Convert.ToDouble(rows4[0]["y" + j]); }
                    catch { }
                    su2 = 0;
                    su3 = 0;
                    try { su2 = Convert.ToDouble(rows6[k]["y" + j]); }
                    catch { }
                    ro1["y" + j] = su2;

                    if (su1 != 0)
                        su3 = su2 * 100 / su1;
                    ro2["y" + j] = su3;

                }

                dt.Rows.Add(ro1);
                dt.Rows.Add(ro2);

            }

            row7["ID"] = Guid.NewGuid().ToString();
            row7["Title"] = "弹性系数";
            dt.Rows.Add(row7);

            row8["ID"] = Guid.NewGuid().ToString();
            row8["Title"] = "人均用电量（千瓦时/人）";
            dt.Rows.Add(row8);

            row9["ID"] = Guid.NewGuid().ToString();
            row9["Title"] = "GDP单耗（千瓦时/万元）";
            dt.Rows.Add(row9);




            for (int k = 0; k < rows6.Length; k++)
            {
                double su1 = 0;
                double su2 = 0;
                double su3 = 0;

                DataRow ro1 = dt.NewRow();
                ro1["ID"] = Guid.NewGuid().ToString();
                ro1["Title"] = rows6[k]["Title"].ToString();

                for (int j = firstyear; j <= endyear; j++)
                {
                    if (shjjbyyear)
                    {
                        if (!ht.ContainsValue(j))
                            continue;
                    }
                   
                    su1 = 0;
                    su2 = 0;
                    su3 = 0;
                    try { su1 = Convert.ToDouble(rows6[k]["y" + j]); }
                    catch { }
                    try { su2 = Convert.ToDouble(rows3[k]["y" + j]); }
                    catch { }

                    if (su2 != 0)
                    { su3 = su1 * 10000 / su2; }
                    else
                        su3 = su1;
                    ro1["y" + j] = su3;
                }
                if (rows6[k]["Title"].ToString().IndexOf("居民") >= 0)
                    continue;
                dt.Rows.Add(ro1);

            }

            row10["ID"] = Guid.NewGuid().ToString();
            row10["Title"] = "居民用电（千瓦时/人）";
            dt.Rows.Add(row10);


            double d = 0;
            foreach (DataRow drw1 in dt.Rows)
            {
                try
                {
                    d = (double)drw1["y" + RealFistYear];
                }
                catch { }
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName.IndexOf("m") >= 0)
                    {
                        string s = dc.ColumnName.Replace("m", "");
                        int y1 = int.Parse(s);
                        double d1 = 0;
                        try
                        {
                            d1 = (double)drw1["y" + s];
                        }
                        catch { }

                        double sss = Math.Round(Math.Pow(d1 / d, 1.0 / (y1 - RealFistYear)) - 1, 4);

                        if (sss.ToString() == "非数字")
                            sss = 0;
                        drw1["m" + s] = sss;
                    }
                }
            }
            if (!shjjbyyear)
            {
                creatsheetbydt(title, dt, viscol);
            }

            else
            {
                recreatsheetbydt(title, dt, viscol, 2);
            }
            
        }
#endregion
#region 分区县供电实绩
        private void build_fqxgdsj()
        {
            Dictionary<string, Columqk> viscol = new Dictionary<string, Columqk>();
            string title = "分区县供电实绩";
            //对数据进行处理
             Ps_YearRange py = new Ps_YearRange();
            py.Col4 = "分区供电实绩";
            py.Col5 = ProjectUID;
            int firstyear, endyear;
            IList<Ps_YearRange> li = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);
            if (li.Count > 0)
            {
                firstyear = li[0].StartYear;
                endyear = li[0].FinishYear;
            }
            else
            {
                firstyear = 2000;
                endyear = 2008;
                py.BeginYear = 1990;
                py.FinishYear = endyear;
                py.StartYear = firstyear;
                py.EndYear = 2060;
                py.ID = Guid.NewGuid().ToString();
                Services.BaseService.Create<Ps_YearRange>(py);
            }

            System.Data.DataTable datatable = getfqgdsjtable();
            System.Data.DataTable dt = new System.Data.DataTable();
            List<string> listColID = new List<string>();

            listColID.Add("Title");
            dt.Columns.Add("Title", typeof(string));
            dt.Columns["Title"].Caption = "项目";
            viscol["Title"] = new Columqk("项目", 3, 2);
            dt.Columns.Add("ParentID", typeof(string));

            foreach (System.Data.DataColumn column in datatable.Columns)
            {
                if (column.ColumnName.IndexOf("y") >= 0)
                {
                    int year=Convert.ToInt32(column.ColumnName.Substring(1));
                    if (year>=firstyear &&year<=endyear)
                    {
                        listColID.Add(column.ColumnName);
                        dt.Columns.Add(column.ColumnName, typeof(double));
                        viscol[column.ColumnName] = new Columqk(year + "年", 2, 2);
                    }
                    
                }
                //else
                //    if (column.FieldName == "ParentID")
                //    {
                //        dt.Columns.Add("ParentID", typeof(string));
                //        listColID.Add("ParentID");
                //        dt.Columns["ParentID"].Caption = "父ID";
                //    }
            }
            listColID.Add("ParentID");
            dt.Columns["ParentID"].Caption = "父ID";

            int itemp = -4;
            int jtemp = -4;
            foreach (DataRow node in datatable.Rows)
            {
                jtemp = itemp;
                AddNodeDataToDataTable(ref dt, node, listColID, ref itemp, jtemp);
                // itemp++;
            }
            if (!shjjbyyear)
            {
                creatsheetbydt(title, dt, viscol);
            }

            else
            {
                recreatsheetbydt(title, dt, viscol, 3);
            }           
        }
        private  void AddNodeDataToDataTable( ref System.Data.DataTable dt, DataRow node, List<string> listColID, ref int i, int j)
        {
            DataRow newRow = dt.NewRow();
            DataRow[] drcol=node.Table.Select("ParentID='"+node["ID"].ToString()+"'");
            int chilnodecount=drcol.Length;
            foreach (string colID in listColID)
            {
                //分类名，第二层及以后在前面加空格
                if (colID == "Title" && chilnodecount!=0)
                {
                    newRow[colID] = "　　" + node[colID];
                }
                else if (colID == "ParentID" && chilnodecount != 0)
                {
                    newRow[colID] = j;
                }
                else
                {
                    newRow[colID] = node[colID];
                }
            }

            ////根分类结束后加空行

            //if (node.ParentNode == null && dt.Rows.Count > 0)
            //{
            //    dt.Rows.Add(new object[] { });
            //}
            
            dt.Rows.Add(newRow);
            j = i;
            i--;
            for (int k = 0; k < chilnodecount;k++ )
            {
                DataRow nd = drcol[k];
                AddNodeDataToDataTable(ref dt, nd, listColID, ref  i, j);
            }
            //foreach (DataRow nd in node.Table.Select("ParentID=" +node["ID"].ToString()))
            //{

            //    AddNodeDataToDataTable(ref dt, nd, listColID, ref  i, j);
               
            //    // i++;
            //}

        }

        string ProjectUID = Itop.Client.MIS.ProgUID;
        private System.Data.DataTable getfqgdsjtable()
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();
           

            //获得datatable

            int type = 4;
            string type1 = "4";
            ArrayList al = new ArrayList();
            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = type;
            psp_Type.Col4 = ProjectUID;
            IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);


            for (int c = 0; c < al.Count; c++)
            {
                bool bl = true;
                foreach (Ps_History ph in listTypes)
                {
                    if (al[c].ToString() == ph.Title)
                        bl = false;
                }
                if (bl)
                {
                    Ps_History pf = new Ps_History();
                    pf.ID = Guid.NewGuid().ToString() + "|" + ProjectUID;
                    pf.Forecast = type;
                    pf.ForecastID = type1;
                    pf.Title = al[c].ToString();
                    pf.Col4 = ProjectUID;
                    object obj = Services.BaseService.GetObject("SelectPs_HistoryMaxID", pf);
                    if (obj != null)
                        pf.Sort = ((int)obj) + 1;
                    else
                        pf.Sort = 1;
                    Services.BaseService.Create<Ps_History>(pf);
                    listTypes.Add(pf);
                }
            }
            dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));
            return dataTable;
        }

        
#endregion
        
#region  分区县用电情况
        //全市拟建主要工业项目及用电需求情况表
        List<string> cols = new List<string>();//复制列
        List<string> colyears = new List<string>();//负荷列
        List<string> colsum = new List<string>();//合计列
        System.Data.DataTable datatable = new System.Data.DataTable();
        System.Data.DataTable datatable2 = new System.Data.DataTable();
        private void build_njxmandydl()
        {
            Dictionary<string, Columqk> viscol = new Dictionary<string, Columqk>();
            string title = "分区县供电实绩_全市拟建主要工业项目及用电需求情况表";
            Ps_YearRange py = new Ps_YearRange();
            py.Col4 = "区县发展实绩";
            py.Col5 = ProjectUID;
            int firstyear, endyear;
            IList<Ps_YearRange> li = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);
            if (li.Count > 0)
            {
                firstyear = li[0].StartYear;
                endyear = li[0].FinishYear;
            }
            else
            {
                firstyear = 2001;
                endyear = DateTime.Today.Year - 1;
                py.BeginYear = 2001;
                py.FinishYear = endyear;
                py.StartYear = firstyear;
                py.EndYear = 2060;
                py.ID = Guid.NewGuid().ToString();
                Services.BaseService.Create<Ps_YearRange>(py);
            }
            datatable = getfqxdltable();
            datatable2 = getfqxdltable2(datatable);
            //形成统计表
           
            System.Data.DataTable showtable = new System.Data.DataTable();//要显示的datatable
           //setcols431
            cols.Clear();
            colyears.Clear();
            colsum.Clear();
            showtable.Columns.Add("Col1", typeof(string)).Caption = "编号";
            viscol["Col1"] = new Columqk("编号", 3, 2);
            cols.Add("Col1");
            showtable.Columns.Add("Title", typeof(string)).Caption = "企业(项目)名称";
            viscol["Title"] = new Columqk("企业(项目)名称", 3, 2);
            cols.Add("Title");
            showtable.Columns.Add("Col2", typeof(string)).Caption = "建设规模及内容";
            cols.Add("Col2");
            viscol["Col2"] = new Columqk("建设规模及内容", 3, 2);
            showtable.Columns.Add("Col7", typeof(string)).Caption = "计划建设年限";
            cols.Add("Col7");
            viscol["Col7"] = new Columqk("计划建设年限", 3, 2);
            showtable.Columns.Add("Col6", typeof(string)).Caption = "工作进展";
            cols.Add("Col6");
            viscol["Col6"] = new Columqk("工作进展", 3, 2);
            DataColumn dcol = showtable.Columns.Add("y1990", typeof(double));

            dcol.Caption = "用电量";
            viscol["y1990"] = new Columqk("用电量", 2, 2);
            //dcol = showtable.Columns.Add("y1990_sum", typeof(double));
            //dcol.Caption = "正常用电量";
            //dcol.Expression = "IIF(Title='小计',sum(y1990),y1990)";

            cols.Add("y1990");
            showtable.Columns.Add("y1991", typeof(double)).Caption = "负荷";
            viscol["y1991"] = new Columqk("负荷", 2, 2);
            cols.Add("y1991");

            colsum.Add("y1990");
            colsum.Add("y1991");
            ConvertTreeListToDataTable431(ref showtable, datatable, datatable2, firstyear, endyear);
            if (!shjjbyyear)
            {
                creatsheetbydt(title, showtable, viscol);
            }

            else
            {
                recreatsheetbydt(title, showtable, viscol, 5);
            }           

        }
        //重点建设项目统计
        private void build_zdjsxm()
        {
            Dictionary<string, Columqk> viscol = new Dictionary<string, Columqk>();
            string title = "分区县供电实绩_重点建设工业及用电需求表";
            Ps_YearRange py = new Ps_YearRange();
            py.Col4 = "区县发展实绩";
            py.Col5 = ProjectUID;
            int firstyear, endyear;
            IList<Ps_YearRange> li = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);
            if (li.Count > 0)
            {
                firstyear = li[0].StartYear;
                endyear = li[0].FinishYear;
            }
            else
            {
                firstyear = 2001;
                endyear = DateTime.Today.Year - 1;
                py.BeginYear = 2001;
                py.FinishYear = endyear;
                py.StartYear = firstyear;
                py.EndYear = 2060;
                py.ID = Guid.NewGuid().ToString();
                Services.BaseService.Create<Ps_YearRange>(py);
            }
            datatable = getfqxdltable();
            datatable2 = getfqxdltable2(datatable);
            //形成统计表

            System.Data.DataTable showtable = new System.Data.DataTable();//要显示的datatable
            //setcols431
            cols.Clear();
            colyears.Clear();
            colsum.Clear();
            showtable.Columns.Add("Col1", typeof(string)).Caption = "编号";
            viscol["Col1"] = new Columqk("编号", 3, 2);
            cols.Add("Col1");
            showtable.Columns.Add("Title", typeof(string)).Caption = "企业(项目)名称";
            viscol["Title"] = new Columqk("企业(项目)名称", 3, 2);
            cols.Add("Title");
            showtable.Columns.Add("Col2", typeof(string)).Caption = "建设规模及内容";
            cols.Add("Col2");
            viscol["Col2"] = new Columqk("建设规模及内容", 3, 2);
            showtable.Columns.Add("Col7", typeof(string)).Caption = "计划建设年限";
            cols.Add("Col7");
            viscol["Col7"] = new Columqk("计划建设年限", 3, 2);
            showtable.Columns.Add("Col6", typeof(string)).Caption = "工作进展";
            cols.Add("Col6");
            viscol["Col6"] = new Columqk("工作进展", 3, 2);
            DataColumn dcol = showtable.Columns.Add("y1990", typeof(double));

            dcol.Caption = "用电量";
            viscol["y1990"] = new Columqk("用电量", 2, 2);
            //dcol = showtable.Columns.Add("y1990_sum", typeof(double));
            //dcol.Caption = "正常用电量";
            //dcol.Expression = "IIF(Title='小计',sum(y1990),y1990)";

            cols.Add("y1990");
            showtable.Columns.Add("y1991", typeof(double)).Caption = "负荷";
            viscol["y1991"] = new Columqk("负荷", 2, 2);
            cols.Add("y1991");

            colsum.Add("y1990");
            colsum.Add("y1991");
            ConvertTreeListToDataTable511(ref showtable, datatable, datatable2, firstyear, endyear);
            if (!shjjbyyear)
            {
                creatsheetbydt(title, showtable, viscol);
            }

            else
            {
                recreatsheetbydt(title, showtable, viscol, 5);
            }           

        }
        /// <summary>
        /// 查找大用户节点
        /// </summary>
        /// <param name="node"></param>
        private DataRow findDYHParent(DataRow root)
        {
            DataRow retNode = null;
            DataRow[] childrowcol = root.Table.Select("ParentID='" + root["ID"].ToString() + "'");
            foreach (DataRow node in childrowcol)
            {
                if (node["Title"].ToString().Contains("用户"))
                {
                    retNode = node;
                    break;
                }
                retNode = findDYHParent(node);
            }
            return retNode;
        }
        private void ConvertTreeListToDataTable511(ref System.Data.DataTable showtable, System.Data.DataTable datasource, System.Data.DataTable datatable2, int firstyear, int endyear)
        {

            if (datasource.Rows.Count == 0) return;
            int count1 = 0;//区县计数a

            foreach (DataRow nd in datasource.Rows)
            {

                DataRow[] childrowcol = datasource.Select("ParentID='" + nd["ID"].ToString() + "'");
                if (childrowcol.Length < 4)
                    continue;

                foreach (DataRow node in childrowcol)
                {
                    DataRow newrow = null;
                    DataRow pnode = findDYHParent(node);

                    int count2 = 0;//区县大用户计数
                    int nt1 = 0;
                    if (pnode != null )
                    {
                         DataRow[] childrowcol1 = datasource.Select("ParentID='" + pnode["ID"].ToString() + "'");
                         if (childrowcol1.Length > 0)
                         {
                             count1++;
                             DataRow node1 = childrowcol1[0];
                             DataRow findnode = null;
                             int nexti = 0;
                             while (node1 != null)
                             {
                                 nexti++;
                                 if (node1["Title"].ToString().Contains("新增"))
                                 {
                                     findnode = node1;
                                     break;
                                 }
                                 node1 = childrowcol1[nexti];
                             }
                             if (findnode == null) continue;
                             string t1 = "";
                            // int nt1 = 0;
                             DataRow[] childrowcol2 = datasource.Select("ParentID='" + findnode["ID"].ToString() + "'");
                             foreach (DataRow node2 in childrowcol2)
                             {
                                 string t2 = node2["Col5"].ToString();
                                 if (t2 != t1)
                                 {//新分类开始
                                     count2 = 0;//分类计数清零
                                    // nt1++;
                                     t1 = t2;
                                     newrow = showtable.NewRow();
                                     newrow["Title"] = t1;
                                     newrow["Col1"] = "(" + getDX(nt1) + ")";
                                     showtable.Rows.Add(newrow);
                                 }
                                 count2++;
                                 newrow = createRow(ref showtable, node2);
                                 newrow["Col1"] = count2;
                                 setFh(newrow, node2["ID"].ToString());
                         }

                       
                        }
                    }
                }
            }
            //foreach (TreeListNode node in treeList1.Nodes[0].Nodes) {
           
            //}
            //现有小计
            DataRow row33 = createSumRow( ref showtable);
            row33["Title"] = "合计";
            row33["Col1"] = "(" + getDX(count1 + 1) + ")";
            showtable.Rows.Add(row33);
           
        }

        private void ConvertTreeListToDataTable431(ref System.Data.DataTable showtable,System.Data.DataTable datasource,System.Data.DataTable datatable2,int firstyear,int endyear)
        {
            if (datasource.Rows.Count == 0) return;
            int count1 = 0;//区县计数a

            foreach (DataRow nd in datasource.Rows)
            {
               
                DataRow[] childrowcol = datasource.Select("ParentID='" + nd["ID"].ToString() + "'");
                if (childrowcol.Length < 4)
                    continue;

                foreach (DataRow node in childrowcol)
                {
                   DataRow pnode = findDYHParent(node);
                   
                    if (pnode!=null)
                    {
                        DataRow[] childrowcol1 = datasource.Select("ParentID='" + pnode["ID"].ToString() + "'");
                        if (childrowcol1.Length > 0)
                        {
                            count1++;
                            DataRow newrow = showtable.NewRow();
                            newrow["Title"] = node["Title"];
                            newrow["Col1"] = getDX(count1);
                            showtable.Rows.Add(newrow);
                            int count2 = 0;//区县大用户计数
                            DataRow node1 = childrowcol1[0];
                            DataRow findnode = null;
                            int nexti = 0;
                            while (node1 != null)
                            {
                                nexti++;
                                if (node1["Title"].ToString().Contains("新增"))
                                {
                                    findnode = node1;
                                    break;
                                }
                                node1 = childrowcol1[nexti];
                            }
                            if (findnode == null) continue;
                            string t1 = "";
                            int nt1 = 0;
                            DataRow[] childrowcol2 = datasource.Select("ParentID='" + findnode["ID"].ToString() + "'");
                            foreach (DataRow node2 in childrowcol2)
                            {

                                //DataRowView row2 = node2.TreeList.GetDataRecordByNode(node2) as DataRowView;
                                string t2 = node2["Col5"].ToString();
                                if (t2 != t1)
                                {//新分类开始
                                    count2 = 0;//分类计数清零
                                    nt1++;
                                    t1 = t2;
                                    newrow = showtable.NewRow();
                                    newrow["Title"] = t1;
                                    newrow["Col1"] = "(" + getDX(nt1) + ")";
                                    showtable.Rows.Add(newrow);
                                }
                                count2++;
                                newrow = createRow(ref showtable, node2);
                                newrow["Col1"] = count2;
                                setFh(newrow, node2["ID"].ToString());
                            }
                        }
                    }
                  
                   
                }
            }

            //现有小计
            DataRow row33 = createSumRow(ref showtable);
            row33["Title"] = "合计";
            row33["Col1"] = getDX(count1 + 1);
            showtable.Rows.Add(row33);
           
        }
        string splitstr = "-";
        string dxs = "零一二三四五六七八九十";
        private string getDX(int num)
        {
            if (num > 19) return num.ToString();
            string str = num.ToString();
            string ret = "";
            int count = 0;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                string s = str[i].ToString();
                if (count == 0)
                {
                    ret = dxs[int.Parse(s)].ToString();
                }
                else if (count == 1)
                {
                    ret = "十" + ret;
                }
                count++;
            }
            return ret;
        }
        /// <summary>
        /// 生成合计行
        /// </summary>
        /// <param name="dt"></param>
        private DataRow createSumRow(ref System.Data.DataTable dt)
        {
            DataRow newrow = dt.NewRow();
            foreach (DataRow row in dt.Rows)
            {
                foreach (string col in colsum)
                {
                    double d1 = 0;
                    try { d1 = Convert.ToDouble(newrow[col]); }
                    catch { }

                    double d2 = 0;
                    try { d2 = Convert.ToDouble(row[col]); }
                    catch { }
                    newrow[col] = d1 + d2;
                }
            }
            return newrow;
        }
        /// <summary>
        /// row2的负荷到newrow
        /// </summary>
        /// <param name="row1"></param>
        /// <param name="row2"></param>
        private void setData2(DataRow newrow, DataRow row2)
        {
            foreach (string col in colyears)
            {
                newrow[col + "_"] = row2[col];
            }
        }
        private DataRow findrow(System.Data.DataTable table, string filter, string sort)
        {
            DataRow[] rows = table.Select(filter, sort);
            if (rows != null && rows.Length > 0)
            {

                return rows[0];
            }
            return null;

        }
        /// <summary>
        /// 设置负荷
        /// </summary>
        /// <param name="newrow"></param>
        /// <param name="row2"></param> 
        private void setFh(DataRow newrow, string id)
        {
           
            DataRow row22 = findrow(datatable2, "ID='" + id + splitstr + "'", "");
            if (row22 != null)
                setData2(newrow, row22);
        }

        private DataRow createRow(ref System.Data.DataTable dt, DataRow row)
        {
            DataRow newrow = dt.NewRow();
            foreach (string col in cols)
            {
                if (dt.Columns[col].DataType == datatable.Columns[col].DataType)
                    newrow[col] = row[col];
                else
                    newrow[col] = double.Parse(row[col].ToString());
            }
            dt.Rows.Add(newrow);
            return newrow;
        }
        private System.Data.DataTable getfqxdltable2( System.Data.DataTable datatable)
        {
            string type1 = "2";
            int type = 2;
            string type31 = "3";
            int type32 = 3;
            System.Data.DataTable dataTable2 = new System.Data.DataTable();
            ArrayList al = new ArrayList();
            al.Add("全社会");
            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = type32;
            psp_Type.Col4 = ProjectUID;
            IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);
            //foreach (Ps_History ph in listTypes)
            //    {
            //    Common.Services.BaseService.Delete<Ps_History>(ph);

            //    }
            if (listTypes.Count == 0)
            {

                DataRow[] node = datatable.Select("Title='"+ al[0].ToString()+"'") ;
                Ps_History pf = new Ps_History();
                pf.ID = node[0]["ID"] + splitstr;
                //pf.ParentID = "";
                pf.Sort = -1;
                pf.Forecast = type32;
                pf.ForecastID = type31;
                pf.Title = al[0].ToString();
                pf.Col4 = ProjectUID;
                Services.BaseService.Create<Ps_History>(pf);
                listTypes.Add(pf);
            }
            dataTable2 = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));
            return dataTable2;
        }
        private System.Data.DataTable getfqxdltable()
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();
            string type1 = "2";
            int type = 2;
           string type31 = "3";
           int type32 = 3;
           ArrayList al = new ArrayList();
           al.Add("全社会");
           //Dictionary<string, string> al = new Dictionary<string, string>();
           //al.Add("全社会用电量（亿kWh）", "");
           //al.Add(

           //IList<Base_Data> li1 = Common.Services.BaseService.GetStrongList<Base_Data>();
           //foreach (Base_Data bd in li1)
           //    al.Add(bd.Title);

           Ps_History psp_Type = new Ps_History();
           psp_Type.Forecast = type;
           psp_Type.Col4 = ProjectUID;
           IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);
           //foreach (Ps_History ph in listTypes)
           //    {
           //    Common.Services.BaseService.Delete<Ps_History>(ph);

           //    }
           if (listTypes.Count == 0)
           {
               Ps_History pf = new Ps_History();
               pf.ID = createID();
               //pf.ParentID = "";
               pf.Sort = -1;
               pf.Forecast = type;
               pf.ForecastID = type1;
               pf.Title = "全社会";
               pf.Col4 = ProjectUID;
               Services.BaseService.Create<Ps_History>(pf);
               listTypes.Add(pf);
               Ps_History pf2 = new Ps_History();
               pf2.ID = pf.ID + splitstr;
               //pf.ParentID = "";
               pf2.Sort = -1;
               pf2.Forecast = type32;
               pf2.ForecastID = type31;
               pf2.Title = pf.Title;
               pf2.Col4 = ProjectUID;
               Services.BaseService.Create<Ps_History>(pf2);
           }
           dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));
           return dataTable;
        }
        List<string> AreaList = new List<string>();
        private string createID()
        {
            string str = Guid.NewGuid().ToString();
            return ProjectUID + "_" + str.Substring(str.Length - 12);
        }
        /// <summary>
        /// 初始化分区名称
        /// </summary>
        private void Add_AreaName()
        {
            string ProjectID = Itop.Client.MIS.ProgUID;
            string connstr = " ProjectID='" + ProjectID + "'";
            try
            {
                AreaList = (List<string>)Common.Services.BaseService.GetList<string>("SelectPS_Table_AreaWH_Title", connstr);
            }
            catch (Exception e)
            {
                throw;
            }
        }
#endregion
#region 典型日负荷
        //典型日最大负荷
        private void build_dxfh()
        {
            Dictionary<string, Columqk> viscol = new Dictionary<string, Columqk>();
            string title = "典型日最大负荷数据";
            IList<BurdenLine> list = Services.BaseService.GetList<BurdenLine>("SelectBurdenLineByWhere", " uid like '%" + Itop.Client.MIS.ProgUID + "%' order by BurdenDate");
           System.Data.DataTable dT= Itop.Common.DataConverter.ToDataTable((IList)list, typeof(BurdenLine));
            foreach (DataRow dr in dT.Rows)
            {
                PS_Table_AreaWH pa = Client.Common.Services.BaseService.GetOneByKey<PS_Table_AreaWH>(dr["AreaID"].ToString());
                dr["AreaID"] = pa.Title;
               
            }
            viscol["AreaID"]=new Columqk("地区",3,2);
            viscol["BurdenDate"] = new Columqk("日期", 3, 2);
            viscol["Season"] = new Columqk("季节", 3, 2);
            viscol["Hour1"] = new Columqk("1时",2, 2);
            viscol["Hour2"] = new Columqk("2时", 2, 2);
            viscol["Hour3"] = new Columqk("3时", 2, 2);
            viscol["Hour4"] = new Columqk("4时", 2, 2);
            viscol["Hour5"] = new Columqk("5时", 2, 2);
            viscol["Hour6"] = new Columqk("6时", 2, 2);
            viscol["Hour7"] = new Columqk("7时", 2, 2);
            viscol["Hour8"] = new Columqk("8时", 2, 2);
            viscol["Hour9"] = new Columqk("9时", 2, 2);
            viscol["Hour10"] = new Columqk("10时", 2, 2);
            viscol["Hour11"] = new Columqk("11时", 2, 2);
            viscol["Hour12"] = new Columqk("12时", 2, 2);
            viscol["Hour13"] = new Columqk("13时", 2, 2);
            viscol["Hour14"] = new Columqk("14时", 2, 2);
            viscol["Hour15"] = new Columqk("15时", 2, 2);
            viscol["Hour16"] = new Columqk("16时", 2, 2);
            viscol["Hour17"] = new Columqk("17时", 2, 2);
            viscol["Hour18"] = new Columqk("18时", 2, 2);
            viscol["Hour19"] = new Columqk("19时", 2, 2);
            viscol["Hour20"] = new Columqk("20时", 2, 2);
            viscol["Hour21"] = new Columqk("21时", 2, 2);
            viscol["Hour22"] = new Columqk("22时", 2, 2);
            viscol["Hour23"] = new Columqk("23时", 2, 2);
            viscol["Hour24"] = new Columqk("24时", 2, 2);
            viscol["IsType"] = new Columqk("是否为典型日", 3, 2);
            viscol["IsMaxDate"] = new Columqk("是否为最大日", 3, 2);
            if (!shjjbyyear)
            {
                creatsheetbydt(title, dT, viscol);
            }

            else
            {
                recreatsheetbydt(title, dT, viscol, 6);
            }           

        }
        //月最大负荷数据
        private void build_monthmax()
        {
            Dictionary<string, Columqk> viscol = new Dictionary<string, Columqk>();
            string title = "月最大负荷数据";
            IList<BurdenMonth> list = Services.BaseService.GetList<BurdenMonth>("SelectBurdenMonthByWhere", " uid like '%" + Itop.Client.MIS.ProgUID + "%' order by BurdenYear ");
            System.Data.DataTable dt = Itop.Common.DataConverter.ToDataTable((IList)list, typeof(BurdenMonth));
            foreach (DataRow dr in dt.Rows)
            {
                PS_Table_AreaWH pa = Client.Common.Services.BaseService.GetOneByKey<PS_Table_AreaWH>(dr["AreaID"].ToString());
                dr["AreaID"] = pa.Title;

            }
            viscol["AreaID"] = new Columqk("地区", 3, 2);
            viscol["BurdenYear"] = new Columqk("年", 3, 2);
            viscol["Month1"] = new Columqk("1月", 2, 2);
            viscol["Month2"] = new Columqk("2月", 2, 2);
            viscol["Month3"] = new Columqk("3月", 2, 2);
            viscol["Month4"] = new Columqk("4月", 2, 2);
            viscol["Month5"] = new Columqk("5月", 2, 2); 
            viscol["Month6"] = new Columqk("6月", 2, 2);
            viscol["Month7"] = new Columqk("7月", 2, 2); 
            viscol["Month8"] = new Columqk("8月", 2, 2); 
            viscol["Month9"] = new Columqk("9月", 2, 2);
            viscol["Month10"] = new Columqk("10月", 2, 2);
            viscol["Month11"] = new Columqk("11月", 2, 2);
            viscol["Month12"] = new Columqk("12月", 2, 2);
            if (!shjjbyyear)
            {
                creatsheetbydt(title, dt, viscol);
            }

            else
            {
                recreatsheetbydt(title, dt, viscol, 7);
            }           
        }
        //年最大负荷数据
        //IList<PS_Table_AreaWH> AreaList = null;
        IList<BurdenYear> li = new List<BurdenYear>();
        private void build_YearMAX()
        {
            string pjt = " ProjectID='" + MIS.ProgUID + "'";
          IList<PS_Table_AreaWH>   AreaList = Common.Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", pjt);
            Dictionary<string, Columqk> viscol = new Dictionary<string, Columqk>();
            string title = "年最大负荷数据";
            IList<BurdenLine> list = Services.BaseService.GetList<BurdenLine>("SelectBurdenLineByWhere", " uid like '%" + Itop.Client.MIS.ProgUID + "%'  order by BurdenDate");

            int i = 1;
            int yeardata = 0;

            //IList<BurdenYear> li = new List<BurdenYear>();
            BurdenYear by = new BurdenYear();
            foreach (BurdenLine bl in list)
            {
                double burmax = 0;
                burmax = Math.Max(bl.Hour1, bl.Hour2);
                burmax = Math.Max(burmax, bl.Hour3);
                burmax = Math.Max(burmax, bl.Hour4);
                burmax = Math.Max(burmax, bl.Hour5);
                burmax = Math.Max(burmax, bl.Hour6);
                burmax = Math.Max(burmax, bl.Hour7);
                burmax = Math.Max(burmax, bl.Hour8);
                burmax = Math.Max(burmax, bl.Hour9);
                burmax = Math.Max(burmax, bl.Hour10);
                burmax = Math.Max(burmax, bl.Hour11);
                burmax = Math.Max(burmax, bl.Hour12);
                burmax = Math.Max(burmax, bl.Hour13);
                burmax = Math.Max(burmax, bl.Hour14);
                burmax = Math.Max(burmax, bl.Hour15);
                burmax = Math.Max(burmax, bl.Hour16);
                burmax = Math.Max(burmax, bl.Hour17);
                burmax = Math.Max(burmax, bl.Hour18);
                burmax = Math.Max(burmax, bl.Hour19);
                burmax = Math.Max(burmax, bl.Hour20);
                burmax = Math.Max(burmax, bl.Hour21);
                burmax = Math.Max(burmax, bl.Hour22);
                burmax = Math.Max(burmax, bl.Hour23);
                burmax = Math.Max(burmax, bl.Hour24);

                if (yeardata != bl.BurdenDate.Year)
                {
                    if (yeardata == 0)
                    {
                        yeardata = bl.BurdenDate.Year;
                        by = new BurdenYear();
                        by.BurdenYears = yeardata;
                        by.Values = burmax;
                        by.BurdenDate = bl.BurdenDate;
                        by.AreaID = bl.AreaID;
                    }
                    else
                    {
                        li.Add(by);
                        yeardata = bl.BurdenDate.Year;
                        by = new BurdenYear();
                        by.BurdenYears = yeardata;
                        by.Values = burmax;
                        by.BurdenDate = bl.BurdenDate;
                        by.AreaID = bl.AreaID;
                    }

                }
                else
                {
                    if (by.Values < burmax)
                    {
                        by.Values = burmax;
                        by.BurdenDate = bl.BurdenDate;
                        by.AreaID = bl.AreaID;
                    }




                    //by.Values = Math.Max(by.Values,burmax);
                    //by.BurdenDate = bl.BurdenDate;
                }
                if (i == list.Count)
                    li.Add(by);
                i++;
            }
           System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("s", typeof(string));
            viscol["s"] = new Columqk("时间",3,2);
            int listCount = li.Count;


            int numi = 0;
            int numj = 0;
            int numk = 0;
            foreach (BurdenYear bys in li)
            {
                numi++;
                numj++;
                numk++;




                dt.Columns.Add("Year" + bys.BurdenYears.ToString(), typeof(double));
                viscol["Year" + bys.BurdenYears.ToString()] = new Columqk(FindArea(bys.AreaID,AreaList) + "_" + bys.BurdenYears.ToString() + "年", 3, 2);
            }
            DataRow row = dt.NewRow();
            row["s"] = "负荷";
            foreach (BurdenYear bys1 in li)
            {
                row["Year" + bys1.BurdenYears.ToString()] = bys1.Values;
            }
            dt.Rows.Add(row);
            if(!shjjbyyear)
            {
                creatsheetbydt(title, dt, viscol);
            }

            else
            {
                recreatsheetbydt(title, dt, viscol, 8);
            }           
        }
        private string FindArea(string Areaid,IList<PS_Table_AreaWH> Al)
        {
            string value = "无地区";
            if (Al.Count > 0)
            {
                for (int i = 0; i < Al.Count; i++)
                {
                    if (Al[i].ID == Areaid)
                    {
                        value = Al[i].Title.ToString();
                        break;
                    }
                }
            }
            return value;
        }
#endregion
#region  设备拓扑情况
        private string Ghflag = "1";
        private string projectid = Itop.Client.MIS.ProgUID;
        private IList fu_list = null;
        private IList<PSPDEV> fu_list1 = null;
        private List<Substation_Info> fu_list_no = new List<Substation_Info>();
        string[] titlestr = new string[60];
        public System.Data.DataTable Calc(string addConn)
        {
            int x5 = 1, x1 = 1, x1z = 1, x2 = 1, x2z = 1, x35 = 1, x35z = 1, x10 = 1, x10z = 1, x6 = 1, x6z = 1, t5 = 0, t2 = 0, t2z = 0, t1 = 0, t1z = 0, t35 = 0, t35z = 0, t10 = 0, t10z = 0, t6 = 0, t6z = 0;
            double h5 = 0, h1 = 0, h1z = 0, h2 = 0, h2z = 0, h35 = 0, h35z = 0, h10 = 0, h10z = 0, h6 = 0, h6z = 0, z5 = 0, z1 = 0, z1z = 0, z2 = 0, z2z = 0, z35 = 0, z35z = 0, z10 = 0, z10z = 0, z6 = 0, z6z = 0; ;
            int index5 = -1, index2 = -1, index2z = -1, index1 = -1, index1z = -1, index35 = -1, index35z = -1, index10 = -1, index10z = -1, index6 = -1, index6z = -1;
            Hashtable table = new Hashtable();
            Hashtable table_500 = new Hashtable();
            Hashtable table_220 = new Hashtable();
            Hashtable table_220z = new Hashtable();
            Hashtable table_35 = new Hashtable();
            Hashtable table_35z = new Hashtable();
            Hashtable table_10 = new Hashtable();
            Hashtable table_10z = new Hashtable();
            Hashtable table_6 = new Hashtable();
            Hashtable table_6z = new Hashtable();
            IList<string> groupList_500 = new List<string>();
            IList<string> groupList_220 = new List<string>();
            IList<string> groupList_220z = new List<string>();
            IList<string> groupList_35 = new List<string>();
            IList<string> groupList_35z = new List<string>();
            IList<string> groupList_10 = new List<string>();
            IList<string> groupList_10z = new List<string>();
            IList<string> groupList_6 = new List<string>();
            IList<string> groupList_6z = new List<string>();

            bool five = true, one = true, onez = true, two = true, twoz = true, three = true, threez = true, ten = true, tenz = true, six = true, sixz = true;
            string area = "1@3$5q99z99";
            string area_500 = "1@3$5q99z99";
            string area_220 = "1@3$5q99z99";
            string area_220z = "1@3$5q99z99";
            string area_35 = "1@3$5q99z99";
            string area_35z = "1@3$5q99z99";
            string area_10 = "1@3$5q99z99";
            string area_10z = "1@3$5q99z99";
            string area_6 = "1@3$5q99z99";
            string area_6z = "1@3$5q99z99";
            int j = 0;
            int now = 0;
            string con = "AreaID='" + projectid + "'" + " and Flag='" + Ghflag + "'";
            con += addConn;
            con += " order by convert(int,L1) desc,S4,AreaName,CreateDate,convert(int,S5)";
            string[] que = new string[60] { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", 
            "十一","十二","十三","十四","十五","十六","十七","十八","十九","二十","二十一","二十二","二十三","二十四","二十五","二十六","二十七",
            "二十八","二十九","三十","三十一","三十二","三十三","三十四","三十五","三十六","三十七","三十八","三十九","四十","四十一","四十二","四十三","四十四",
            "四十五","四十六","四十七","四十八","四十九","五十","五十一","五十二","五十三","五十四","五十五","五十六","五十七","五十八","五十九","六十"};
            titlestr = que;
            //IList list = Common.Services.BaseService.GetList("SelectSubstation_InfoByCon", con);
            IList list = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", con);
            string conn = "L1=110";
            // IList groupList = Common.Services.BaseService.GetList("SelectAreaNameGroupByConn", conn);
            IList<string> groupList = new List<string>();
            Hashtable table2 = new Hashtable();
            IList<string> groupList2 = new List<string>();
            string area2 = "1@3$5q99z99";
            for (int i = 0; i < list.Count; i++)
            {
                if (((Substation_Info)list[i]).L1 == 500)
                {
                    if (((Substation_Info)list[i]).AreaName != area_500)
                    {
                        if (!table_500.Contains(((Substation_Info)list[i]).AreaName))
                        {

                            table_500.Add(((Substation_Info)list[i]).AreaName, i);
                            groupList_500.Add(((Substation_Info)list[i]).AreaName);
                        }
                        area_500 = ((Substation_Info)list[i]).AreaName;
                    }
                    if (five)
                    { index5 = i; five = false; }
                    ((Substation_Info)list[i]).S3 = x5.ToString();
                    h5 += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z5 += double.Parse(((Substation_Info)list[i]).L5);

                    }
                    catch { }
                    try
                    {

                        t5 += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x5++;
                }
                else if (((Substation_Info)list[i]).L1 == 220 && ((Substation_Info)list[i]).S4 == "专用")
                {
                    if (((Substation_Info)list[i]).AreaName != area_220z)
                    {
                        if (!table_220z.Contains(((Substation_Info)list[i]).AreaName))
                        {

                            table_220z.Add(((Substation_Info)list[i]).AreaName, i);
                            groupList_220z.Add(((Substation_Info)list[i]).AreaName);
                            //  table[((Substation_Info)list[i]).AreaName] = i;
                        }
                        area_220z = ((Substation_Info)list[i]).AreaName;
                    }
                    if (twoz)
                    { index2z = i; twoz = false; }
                    ((Substation_Info)list[i]).S3 = x2z.ToString();
                    h2z += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z2z += double.Parse(((Substation_Info)list[i]).L5);

                    }
                    catch { }
                    try
                    {

                        t2z += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x2z++;
                }
                else if (((Substation_Info)list[i]).L1 == 220 && ((Substation_Info)list[i]).S4 != "专用")
                {
                    if (((Substation_Info)list[i]).AreaName != area_220)
                    {
                        if (!table_220.Contains(((Substation_Info)list[i]).AreaName))
                        {

                            table_220.Add(((Substation_Info)list[i]).AreaName, i);
                            groupList_220.Add(((Substation_Info)list[i]).AreaName);
                            //  table[((Substation_Info)list[i]).AreaName] = i;
                        }
                        area_220 = ((Substation_Info)list[i]).AreaName;
                    }
                    if (two)
                    { index2 = i; two = false; }
                    ((Substation_Info)list[i]).S3 = x2.ToString();
                    h2 += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z2 += double.Parse(((Substation_Info)list[i]).L5);

                    }
                    catch { }
                    try
                    {

                        t2 += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x2++;
                }
                else if (((Substation_Info)list[i]).L1 == 110 && ((Substation_Info)list[i]).S4 == "专用")
                {
                    if (((Substation_Info)list[i]).AreaName != area2)
                    {
                        if (!table2.Contains(((Substation_Info)list[i]).AreaName))
                        {

                            table2.Add(((Substation_Info)list[i]).AreaName, i);
                            groupList2.Add(((Substation_Info)list[i]).AreaName);
                            //  table[((Substation_Info)list[i]).AreaName] = i;
                        }
                        area2 = ((Substation_Info)list[i]).AreaName;
                    }
                    if (onez)
                    { index1z = i; onez = false; }
                    ((Substation_Info)list[i]).S3 = x1z.ToString();
                    h1z += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z1z += double.Parse(((Substation_Info)list[i]).L5);

                    }
                    catch { }
                    try
                    {

                        t1z += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x1z++;
                }
                else if (((Substation_Info)list[i]).L1 == 110 && ((Substation_Info)list[i]).S4 != "专用")
                {
                    if (((Substation_Info)list[i]).AreaName != area)
                    {
                        if (!table.Contains(((Substation_Info)list[i]).AreaName))
                        {

                            table.Add(((Substation_Info)list[i]).AreaName, i);
                            groupList.Add(((Substation_Info)list[i]).AreaName);
                            //  table[((Substation_Info)list[i]).AreaName] = i;
                        }
                        area = ((Substation_Info)list[i]).AreaName;
                    }

                    if (one)
                    { index1 = i; one = false; }
                    ((Substation_Info)list[i]).S3 = x1.ToString();
                    h1 += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z1 += double.Parse(((Substation_Info)list[i]).L5);

                    }
                    catch { }
                    try
                    {

                        t1 += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x1++;
                }
                else if (((Substation_Info)list[i]).L1 == 35 && ((Substation_Info)list[i]).S4 == "专用")
                {
                    if (((Substation_Info)list[i]).AreaName != area_35z)
                    {
                        if (!table_35z.Contains(((Substation_Info)list[i]).AreaName))
                        {

                            table_35z.Add(((Substation_Info)list[i]).AreaName, i);
                            groupList_35z.Add(((Substation_Info)list[i]).AreaName);
                        }
                        area_35z = ((Substation_Info)list[i]).AreaName;
                    }
                    if (threez)
                    { index35z = i; threez = false; }
                    ((Substation_Info)list[i]).S3 = x35z.ToString();
                    h35z += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z35z += double.Parse(((Substation_Info)list[i]).L5);

                    }
                    catch { }
                    try
                    {

                        t35z += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x35z++;
                }
                else if (((Substation_Info)list[i]).L1 == 35 && ((Substation_Info)list[i]).S4 != "专用")
                {
                    if (((Substation_Info)list[i]).AreaName != area_35)
                    {
                        if (!table_35.Contains(((Substation_Info)list[i]).AreaName))
                        {

                            table_35.Add(((Substation_Info)list[i]).AreaName, i);
                            groupList_35.Add(((Substation_Info)list[i]).AreaName);
                        }
                        area_35 = ((Substation_Info)list[i]).AreaName;
                    }
                    if (three)
                    { index35 = i; three = false; }
                    ((Substation_Info)list[i]).S3 = x35.ToString();
                    h35 += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z35 += double.Parse(((Substation_Info)list[i]).L5);

                    }
                    catch { }
                    try
                    {

                        t35 += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x35++;
                }
                else if (((Substation_Info)list[i]).L1 == 10 && ((Substation_Info)list[i]).S4 == "专用")
                {
                    if (((Substation_Info)list[i]).AreaName != area_10z)
                    {
                        if (!table_10z.Contains(((Substation_Info)list[i]).AreaName))
                        {

                            table_10z.Add(((Substation_Info)list[i]).AreaName, i);
                            groupList_10z.Add(((Substation_Info)list[i]).AreaName);
                        }
                        area_10z = ((Substation_Info)list[i]).AreaName;
                    }
                    if (tenz)
                    { index10z = i; tenz = false; }
                    ((Substation_Info)list[i]).S3 = x10z.ToString();
                    h10z += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z10z += double.Parse(((Substation_Info)list[i]).L5);

                    }
                    catch { }
                    try
                    {

                        t10z += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x10z++;
                }
                else if (((Substation_Info)list[i]).L1 == 10 && ((Substation_Info)list[i]).S4 != "专用")
                {
                    if (((Substation_Info)list[i]).AreaName != area_10)
                    {
                        if (!table_10.Contains(((Substation_Info)list[i]).AreaName))
                        {

                            table_10.Add(((Substation_Info)list[i]).AreaName, i);
                            groupList_10.Add(((Substation_Info)list[i]).AreaName);
                        }
                        area_10 = ((Substation_Info)list[i]).AreaName;
                    }
                    if (ten)
                    { index10 = i; ten = false; }
                    ((Substation_Info)list[i]).S3 = x10.ToString();
                    h10 += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z10 += double.Parse(((Substation_Info)list[i]).L5);

                    }
                    catch { }
                    try
                    {

                        t10 += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x10++;
                }
                else if (((Substation_Info)list[i]).L1 == 6 && ((Substation_Info)list[i]).S4 == "专用")
                {
                    if (((Substation_Info)list[i]).AreaName != area_6z)
                    {
                        if (!table_6z.Contains(((Substation_Info)list[i]).AreaName))
                        {

                            table_6z.Add(((Substation_Info)list[i]).AreaName, i);
                            groupList_6z.Add(((Substation_Info)list[i]).AreaName);
                        }
                        area_6z = ((Substation_Info)list[i]).AreaName;
                    }
                    if (sixz)
                    { index6z = i; sixz = false; }
                    ((Substation_Info)list[i]).S3 = x6z.ToString();
                    h6z += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z6z += double.Parse(((Substation_Info)list[i]).L5);

                    }
                    catch { }
                    try
                    {

                        t6z += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x6z++;
                }
                else if (((Substation_Info)list[i]).L1 == 6 && ((Substation_Info)list[i]).S4 != "专用")
                {
                    if (((Substation_Info)list[i]).AreaName != area_6)
                    {
                        if (!table_6.Contains(((Substation_Info)list[i]).AreaName))
                        {

                            table_6.Add(((Substation_Info)list[i]).AreaName, i);
                            groupList_6.Add(((Substation_Info)list[i]).AreaName);
                        }
                        area_6 = ((Substation_Info)list[i]).AreaName;
                    }
                    if (six)
                    { index6 = i; six = false; }
                    ((Substation_Info)list[i]).S3 = x6.ToString();
                    h6 += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z6 += double.Parse(((Substation_Info)list[i]).L5);

                    }
                    catch { }
                    try
                    {

                        t6 += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x6++;
                }
            }
            if (x5 > 1)
            {
                Substation_Info info = new Substation_Info();
                info.S3 = que[j];
                j++;
                info.Title = "500千伏";
                info.L2 = h5;
                info.L5 = z5.ToString();
                info.L3 = t5;
                info.L1 = 500;
                info.S4 = "no";
                list.Insert(index5, info);//.Add(info);
                now++;
                for (int k = 0; k < groupList_500.Count; k++)
                {
                    Substation_Info infok = new Substation_Info();
                    infok.S3 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList[k];
                    infok.AreaName = groupList_500[k];
                    conn = "L1=500 and AreaID='" + projectid + "' and  AreaName='" + groupList_500[k] + "'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 500;
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", conn);
                    infok.L3 = 0;
                    foreach (Substation_Info temp in temList)
                    {
                        try
                        {
                            infok.L3 += (int)temp.L3;
                        }
                        catch
                        {

                        }
                    }
                    list.Insert(int.Parse(table_500[groupList_500[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x2 > 1)
            {
                Substation_Info info2 = new Substation_Info();
                info2.S3 = que[j];
                j++;
                info2.Title = "220千伏公变";
                info2.L2 = h2;
                info2.L5 = z2.ToString();
                info2.L3 = t2;
                info2.L1 = 220;
                info2.S4 = "no";
                list.Insert(index2 + now, info2);
                now++;
                for (int k = 0; k < groupList_220.Count; k++)
                {
                    Substation_Info infok = new Substation_Info();
                    infok.S3 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList[k];
                    infok.AreaName = groupList_220[k];
                    conn = "L1=220 and AreaID='" + projectid + "' and  AreaName='" + groupList_220[k] + "'  and S4!='专用'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 220;
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", conn);
                    infok.L3 = 0;
                    foreach (Substation_Info temp in temList)
                    {
                        try
                        {
                            infok.L3 += (int)temp.L3;
                        }
                        catch
                        {

                        }
                    }
                    list.Insert(int.Parse(table_220[groupList_220[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x2z > 1)
            {
                Substation_Info info2z = new Substation_Info();
                info2z.S3 = que[j];
                j++;
                info2z.Title = "220千伏专变";
                info2z.L2 = h2z;
                info2z.L5 = z2z.ToString();
                info2z.L3 = t2z;
                info2z.L1 = 220;
                info2z.S4 = "no";
                list.Insert(index2z + now, info2z);
                now++;

                for (int k = 0; k < groupList_220z.Count; k++)
                {
                    Substation_Info infok = new Substation_Info();
                    infok.S3 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList2[k];
                    infok.AreaName = groupList_220z[k];
                    conn = "L1=220 and AreaID='" + projectid + "' and  AreaName='" + groupList_220z[k] + "' and S4='专用'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 220;
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", conn);
                    infok.L3 = 0;
                    foreach (Substation_Info temp in temList)
                    {
                        try
                        {
                            infok.L3 += (int)temp.L3;
                        }
                        catch
                        {

                        }
                    }
                    list.Insert(int.Parse(table_220z[groupList_220z[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x1 > 1)
            {
                Substation_Info info1 = new Substation_Info();
                info1.S3 = que[j];
                j++;
                info1.Title = "110千伏公变";
                info1.L2 = h1;
                info1.L5 = z1.ToString();
                info1.L3 = t1;
                info1.L1 = 110;
                info1.S4 = "no";
                list.Insert(index1 + now, info1);
                now++;
                for (int k = 0; k < groupList.Count; k++)
                {
                    Substation_Info infok = new Substation_Info();
                    infok.S3 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList[k];
                    infok.AreaName = groupList[k];
                    conn = "L1=110 and AreaID='" + projectid + "' and  AreaName='" + groupList[k] + "'  and S4!='专用'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 110;
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", conn);
                    infok.L3 = 0;
                    foreach (Substation_Info temp in temList)
                    {
                        try
                        {
                            infok.L3 += (int)temp.L3;
                        }
                        catch
                        {

                        }
                    }
                    list.Insert(int.Parse(table[groupList[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x1z > 1)
            {
                Substation_Info info1z = new Substation_Info();
                info1z.S3 = que[j];
                j++;
                info1z.Title = "110千伏专变";
                info1z.L2 = h1z;
                info1z.L5 = z1z.ToString();
                info1z.L3 = t1z;
                info1z.L1 = 110;
                info1z.S4 = "no";
                list.Insert(index1z + now, info1z);
                now++;

                for (int k = 0; k < groupList2.Count; k++)
                {
                    Substation_Info infok = new Substation_Info();
                    infok.S3 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList2[k];
                    infok.AreaName = groupList2[k];
                    conn = "L1=110 and AreaID='" + projectid + "' and  AreaName='" + groupList2[k] + "' and S4='专用'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 110;
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", conn);
                    infok.L3 = 0;
                    foreach (Substation_Info temp in temList)
                    {
                        try
                        {
                            infok.L3 += (int)temp.L3;
                        }
                        catch
                        {

                        }
                    }
                    list.Insert(int.Parse(table2[groupList2[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x35 > 1)
            {
                Substation_Info info35 = new Substation_Info();
                info35.S3 = que[j];
                j++;
                info35.Title = "35千伏公变";
                info35.L2 = h35;
                info35.L5 = z35.ToString();
                info35.L3 = t35;
                info35.L1 = 35;
                info35.S4 = "no";
                list.Insert(index35 + now, info35);
                now++;
                for (int k = 0; k < groupList_35.Count; k++)
                {
                    Substation_Info infok = new Substation_Info();
                    infok.S3 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList[k];
                    infok.AreaName = groupList_35[k];
                    conn = "L1=35 and AreaID='" + projectid + "' and  AreaName='" + groupList_35[k] + "'  and S4!='专用' ";
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 35;
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", conn);
                    infok.L3 = 0;
                    foreach (Substation_Info temp in temList)
                    {
                        try
                        {
                            infok.L3 += (int)temp.L3;
                        }
                        catch
                        {

                        }
                    }
                    list.Insert(int.Parse(table_35[groupList_35[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x35z > 1)
            {
                Substation_Info info35z = new Substation_Info();
                info35z.S3 = que[j];
                j++;
                info35z.Title = "35千伏专变";
                info35z.L2 = h35z;
                info35z.L5 = z35z.ToString();
                info35z.L3 = t35z;
                info35z.L1 = 35;
                info35z.S4 = "no";
                list.Insert(index35z + now, info35z);
                now++;

                for (int k = 0; k < groupList_35z.Count; k++)
                {
                    Substation_Info infok = new Substation_Info();
                    infok.S3 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList2[k];
                    infok.AreaName = groupList_35z[k];
                    conn = "L1=35 and AreaID='" + projectid + "' and  AreaName='" + groupList_35z[k] + "' and S4='专用'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 35;
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", conn);
                    infok.L3 = 0;
                    foreach (Substation_Info temp in temList)
                    {
                        try
                        {
                            infok.L3 += (int)temp.L3;
                        }
                        catch
                        {

                        }
                    }
                    list.Insert(int.Parse(table_35z[groupList_35z[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x10 > 1)
            {
                Substation_Info info10 = new Substation_Info();
                info10.S3 = que[j];
                j++;
                info10.Title = "10千伏公变";
                info10.L2 = h10;
                info10.L5 = z10.ToString();
                info10.L3 = t10;
                info10.L1 = 10;
                info10.S4 = "no";
                list.Insert(index10 + now, info10);
                now++;
                for (int k = 0; k < groupList_10.Count; k++)
                {
                    Substation_Info infok = new Substation_Info();
                    infok.S3 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList[k];
                    infok.AreaName = groupList_10[k];
                    conn = "L1=10 and AreaID='" + projectid + "' and  AreaName='" + groupList_10[k] + "'  and S4!='专用'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 10;
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", conn);
                    infok.L3 = 0;
                    foreach (Substation_Info temp in temList)
                    {
                        try
                        {
                            infok.L3 += (int)temp.L3;
                        }
                        catch
                        {

                        }
                    }
                    list.Insert(int.Parse(table_10[groupList_10[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x10z > 1)
            {
                Substation_Info info10z = new Substation_Info();
                info10z.S3 = que[j];
                j++;
                info10z.Title = "10千伏专变";
                info10z.L2 = h10z;
                info10z.L5 = z10z.ToString();
                info10z.L3 = t10z;
                info10z.L1 = 10;
                info10z.S4 = "no";
                list.Insert(index10z + now, info10z);
                now++;

                for (int k = 0; k < groupList_10z.Count; k++)
                {
                    Substation_Info infok = new Substation_Info();
                    infok.S3 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList2[k];
                    infok.AreaName = groupList_10z[k];
                    conn = "L1=10 and AreaID='" + projectid + "' and  AreaName='" + groupList_10z[k] + "' and S4='专用'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 10;
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", conn);
                    infok.L3 = 0;
                    foreach (Substation_Info temp in temList)
                    {
                        try
                        {
                            infok.L3 += (int)temp.L3;
                        }
                        catch
                        {

                        }
                    }
                    list.Insert(int.Parse(table_10z[groupList_10z[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x6 > 1)
            {
                Substation_Info info6 = new Substation_Info();
                info6.S3 = que[j];
                j++;
                info6.Title = "6千伏公变";
                info6.L2 = h6;
                info6.L5 = z6.ToString();
                info6.L3 = t6;
                info6.L1 = 6;
                info6.S4 = "no";
                list.Insert(index6 + now, info6);
                now++;
                for (int k = 0; k < groupList_6.Count; k++)
                {
                    Substation_Info infok = new Substation_Info();
                    infok.S3 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList[k];
                    infok.AreaName = groupList_6[k];
                    conn = "L1=6 and AreaID='" + projectid + "' and  AreaName='" + groupList_6[k] + "'  and S4!='专用'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 6;
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", conn);
                    infok.L3 = 0;
                    foreach (Substation_Info temp in temList)
                    {
                        try
                        {
                            infok.L3 += (int)temp.L3;
                        }
                        catch
                        {

                        }
                    }
                    list.Insert(int.Parse(table_6[groupList_6[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x6z > 1)
            {
                Substation_Info info6z = new Substation_Info();
                info6z.S3 = que[j];
                j++;
                info6z.Title = "6千伏专变";
                info6z.L2 = h6z;
                info6z.L5 = z6z.ToString();
                info6z.L3 = t6z;
                info6z.L1 = 6;
                info6z.S4 = "no";
                list.Insert(index6z + now, info6z);
                now++;

                for (int k = 0; k < groupList_6z.Count; k++)
                {
                    Substation_Info infok = new Substation_Info();
                    infok.S3 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList2[k];
                    infok.AreaName = groupList_6z[k];
                    conn = "L1=6 and AreaID='" + projectid + "' and  AreaName='" + groupList_6z[k] + "' and S4='专用'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 6;
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", conn);
                    infok.L3 = 0;
                    foreach (Substation_Info temp in temList)
                    {
                        try
                        {
                            infok.L3 += (int)temp.L3;
                        }
                        catch
                        {

                        }
                    }
                    list.Insert(int.Parse(table_6z[groupList_6z[k]].ToString()) + now, infok);
                    now++;
                }
            }
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (((Substation_Info)list[i]).L9 != null && (double)((Substation_Info)list[i]).L2 != null)
                    {
                        double templ9 = (double)((Substation_Info)list[i]).L9;
                        double templ2 = (double)((Substation_Info)list[i]).L2;
                        ((Substation_Info)list[i]).L10 = (templ2 == 0 ? 0 : templ9 / templ2) * 100;
                        Substation_Info tempsub = Common.Services.BaseService.GetOneByKey<Substation_Info>(((Substation_Info)list[i]).UID);
                        tempsub.L10 = ((Substation_Info)list[i]).L10;
                        Common.Services.BaseService.Update<Substation_Info>(tempsub);
                    }
                }
            }
            catch (Exception ew)
            {

                MessageBox.Show("计算负载率出错" + ew.Message);
            }



            fu_list = list;

            list_copy1(list, fu_list_no);
            att_list(fu_list_no);
           System.Data.DataTable dataTable = Itop.Common.DataConverter.ToDataTable((IList)list, typeof(Substation_Info));
           return dataTable;
        }
        private void list_copy1(IList list1, IList list2)
        {
            for (int i = 0; i < list1.Count; i++)
            {
                list2.Add(list1[i]);
            }
        }

        private void att_list(IList templist)
        {
            for (int i = 0; i < templist.Count; i++)
            {
                if (char.IsLower(((Substation_Info)templist[i]).S3, 0))
                {
                    templist.RemoveAt(i);
                    i--;
                }
            }
        }
        //变电站情况表
        private void build_bdzqk()
        {
            Dictionary<string, Columqk> viscol = new Dictionary<string, Columqk>();
            string title = "变电站情况表";
            System.Data.DataTable dt = Calc("");
            viscol["S3"] = new Columqk("序号", 3, 2);
            viscol["AreaName"] = new Columqk("区域", 3, 2);
            viscol["L1"] = new Columqk("电压等级", 2, 2);
            viscol["L2"] = new Columqk("容量（MVA）", 3, 2);
            viscol["L3"] = new Columqk("变电台数(台)", 3, 2);
            viscol["L4"] = new Columqk("容量构成", 3, 2);
            viscol["L5"] = new Columqk("无功总容量（Mvar）", 2, 2);
            viscol["L6"] = new Columqk("无功补偿容量构成（Mvar）", 3, 2);
            viscol["L9"] = new Columqk("最大负荷（MW）", 2, 2);
            viscol["L10"] = new Columqk("负载率（%）", 1, 2);
            viscol["S2"] = new Columqk("投产时间及备注", 3, 2);
            viscol["DQ"] = new Columqk("电网类型", 3, 2);
            if (!shjjbyyear)
            {
                creatsheetbydt(title, dt, viscol);
            }

            else
            {
                recreatsheetbydt(title, dt, viscol, 9);
            }           


        }
        //输电线路情况表
        private void build_lineqk()
        {
            Dictionary<string, Columqk> viscol = new Dictionary<string, Columqk>();
            string title = "变电站情况表";
            System.Data.DataTable dt = InitData();
            viscol["Num"] = new Columqk("序号", 3, 2);
            viscol["Name"] = new Columqk("线路名称", 3, 2);
            viscol["RateVolt"] = new Columqk("电压等级", 3, 2);
            viscol["AreaID"] = new Columqk("区域", 3, 2);
            viscol["Iname"] = new Columqk("高压侧母线", 3, 2);
            viscol["Jname"] = new Columqk("低压侧母线", 3, 2);
            viscol["LineType"] = new Columqk("导线型号", 3, 2);
            viscol["LineLength"] = new Columqk("架空线长度", 2, 2);
            viscol["Length2"] = new Columqk("电缆线长度", 2, 2);
            viscol["OperationYear"] = new Columqk("投产年份", 3, 2);
            if (!shjjbyyear)
            {
                creatsheetbydt(title, dt, viscol);
            }

            else
            {
                recreatsheetbydt(title, dt, viscol, 10);
            }           



        }
        string[] que = new string[60] { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", 
            "十一","十二","十三","十四","十五","十六","十七","十八","十九","二十","二十一","二十二","二十三","二十四","二十五","二十六","二十七",
            "二十八","二十九","三十","三十一","三十二","三十三","三十四","三十五","三十六","三十七","三十八","三十九","四十","四十一","四十二","四十三","四十四",
            "四十五","四十六","四十七","四十八","四十九","五十","五十一","五十二","五十三","五十四","五十五","五十六","五十七","五十八","五十九","六十"};
        private string ProjectID = Itop.Client.MIS.ProgUID;
        private string nowyear = DateTime.Now.Year.ToString();
        private List<PSPDEV> fu_list_no1 = new List<PSPDEV>(); 
        public IList<double> DY()
        {
            string constr = "   ProjectID='" + ProjectID + "' and  year(cast(OperationYear as datetime))<" + nowyear + " and Type='05'  order by RateVolt desc";
            return Common.Services.BaseService.GetList<double>("SelectPSPDEV_RateVolt_distinct", constr);

        }
        public IList<string> GetAreaID(double tempdb)
        {

            string constr = "  ProjectID='" + ProjectID + "' and  year(cast(OperationYear as datetime))<" + nowyear + "  and Type='05'  and RateVolt=" + tempdb + " order by AreaID ";
            return Common.Services.BaseService.GetList<string>("SelectPSPDEV_GroupAreaID_DIs", constr);
        }
        private System.Data.DataTable InitData()
        {
            string constrvalue = " where  ProjectID='@@@@@#@@'";
          IList<PSPDEV>   valuelist = Common.Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", constrvalue);

            int m = 1;
            IList<double> templist = DY();
            if (templist.Count != 0)
            {
                for (int i = 0; i < templist.Count; i++)
                {
                    PSPDEV tempdev = new PSPDEV();
                    tempdev.ProjectID = que[i];
                    tempdev.Name = templist[i] + "KV线路";
                    tempdev.RateVolt = templist[i];
                    tempdev.Type = "T";
                    m = 1;

                    string lenthstr = "  ProjectID='" + ProjectID + "' and  year(cast(OperationYear as datetime))<" + nowyear + "  and Type='05'  and RateVolt=" + templist[i];
                    double linelength = (double)Common.Services.BaseService.GetObject("SelectPSPDEV_SUMLineLength", lenthstr);
                    double length2 = (double)Common.Services.BaseService.GetObject("SelectPSPDEV_SUMLength2", lenthstr);
                    tempdev.LineLength = linelength;
                    tempdev.Length2 = length2;
                    valuelist.Add(tempdev);
                    for (int j = 0; j < GetAreaID(templist[i]).Count; j++)
                    {
                        PSPDEV tempdev1 = new PSPDEV();
                        tempdev1.ProjectID = Convert.ToChar(j + 65).ToString().ToLower(); ;
                        tempdev1.RateVolt = templist[i];
                        tempdev1.AreaID = GetAreaID(templist[i])[j];
                        string lenthstra = "  ProjectID='" + ProjectID + "' and  year(cast(OperationYear as datetime))<" + nowyear + "  and Type='05'  and RateVolt=" + templist[i] + "  and AreaID='" + GetAreaID(templist[i])[j] + "'";
                        double linelengtha = (double)Common.Services.BaseService.GetObject("SelectPSPDEV_SUMLineLength", lenthstra);
                        double length2a = (double)Common.Services.BaseService.GetObject("SelectPSPDEV_SUMLength2", lenthstra);
                        tempdev1.LineLength = linelengtha;
                        tempdev1.Length2 = length2a;
                        tempdev1.Type = "A";
                        valuelist.Add(tempdev1);

                        string constr = " where  ProjectID='" + ProjectID + "' and  year(cast(OperationYear as datetime))<" + nowyear + "   and Type='05' and RateVolt=" + templist[i] + " and AreaID='" + GetAreaID(templist[i])[j] + "'  order by Name";
                        IList<PSPDEV> linelist = Common.Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", constr);
                        for (int k = 0; k < linelist.Count; k++)
                        {
                            linelist[k].ProjectID = m.ToString();
                            m++;
                            valuelist.Add(linelist[k]);
                        }

                    }
                }
            }
          
            fu_list1 = valuelist;

            list_copy(valuelist, fu_list_no1);
            att_list1(fu_list_no1);
            System.Data.DataTable dataTable = Itop.Common.DataConverter.ToDataTable((IList)valuelist, typeof(PSPDEV));
            foreach (DataRow dr in dataTable.Rows)
            {
                PS_Table_AreaWH pa = Client.Common.Services.BaseService.GetOneByKey<PS_Table_AreaWH>(dr["AreaID"].ToString());
                if (pa!=null)
                {
                    dr["AreaID"] = pa.Title;
                }
              

            }
            return dataTable;

        }
        private void list_copy(IList<PSPDEV> list1, IList list2)
        {
            for (int i = 0; i < list1.Count; i++)
            {
                list2.Add(list1[i]);
            }
        }

        private void att_list1(IList templist)
        {

            for (int i = 0; i < templist.Count; i++)
            {
                if (char.IsLower(((PSPDEV)templist[i]).ProjectID, 0))
                {
                    templist.RemoveAt(i);
                    i--;
                }

            }
        }

#endregion
        private double detel_jd(double tempdouble, int tempint)
        {
            return Math.Round(tempdouble, tempint);
        }
        private void Sheet_WriteZero(FarPoint.Win.Spread.SheetView obj_sheet, int startrow, int startcol, int rowcount, int colcount)
        {
            for (int row = 0; row < rowcount; row++)
            {
                for (int col = 0; col < colcount; col++)
                {
                    obj_sheet.SetValue(startrow + row, startcol + col, 0);
                }
            }
        }

        private void barBtnDiaoChu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    //fps.SaveExcel(fname);
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
                        //自动行高
                        //range.Cells.Rows.AutoFit();
                        //保护工作表
                        //ws.Protect(Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
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
        private void barBtnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        private void barBtnRefrehData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm newwait=new WaitDialogForm("", "正在更新数据, 请稍候...");
            //清空原有sheet的数据，但保留sheetname
            fc.SpreadClearSheet(fpSpread1);
            //生成一个空表用来保存当前表
            FarPoint.Win.Spread.SheetView obj_sheet = null;
            //生成一个空表，行列值都设为0用来做为程序处理时的当前表，这样可以提高处理速度
            FarPoint.Win.Spread.SheetView activesheet = new FarPoint.Win.Spread.SheetView();
            activesheet.RowCount = 0;
            activesheet.ColumnCount = 0;
            //添加空表
            fpSpread1.Sheets.Add(activesheet);
            //保留当前表，以备程序结束后还原当前表
            obj_sheet = fpSpread1.ActiveSheet;
            //将空表设为当前表
            fpSpread1.ActiveSheet = activesheet;
            shjjbyyear = true;
            //形成基础数据电力发展实绩历年地区生产
            build_dlhistoryGDP();
            //电力发展实绩分行业统计
            build_dlhistoryfhy();
            //电力发展实绩社会经济用电情况
            build_dlhistoryjjyd();
            //分区县供电实绩
            build_fqxgdsj();
            //分区县用电情况
            //全市拟建主要工业项目及用电需求情况表
            build_njxmandydl();
            //重点建设工业及用电需求表
            build_zdjsxm();
            //典型日最大负荷
            build_dxfh();
            //月最大负荷数据
            build_monthmax();
            //年最大负荷数据           
            build_YearMAX();
            //变电站情况表
            build_bdzqk();
            //输电线路情况表
            build_lineqk();
            shjjbyyear = false;
            fpSpread1.Sheets.Remove(activesheet);

            //移除空表
         
            //还原当前表
            fpSpread1.ActiveSheet = obj_sheet;
            newwait.Close();
            MessageBox.Show("更新数据完成！");
        }
        private void barBtnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm wait = null;
            wait = new WaitDialogForm("", "正在保存数据, 请稍候...");
            //判断文件夹xls是否存在，不存在则创建
            if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\xls"))
            {
                Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\xls");
            }
            fpSpread1.SaveExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\ztj.xls");
            wait.Close();
            MsgBox.Show("保存成功");


        }

        private void fpSpread1_SheetTabClick(object sender, FarPoint.Win.Spread.SheetTabClickEventArgs e)
        {
            switch (e.SheetTabIndex)
            {
               case 2:
                   barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                   break;
                default:
                    barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    break;

            }
        }
        bool shjjbyyear = false;
        Hashtable ht = new Hashtable();
        Hashtable ht1 = new Hashtable();
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<int> li = new List<int>();
            Ps_YearRange py = new Ps_YearRange();
            py.Col4 = "电力发展实绩";
            py.Col5 = Itop.Client.MIS.ProgUID;
            int firstyear, endyear;
            IList<Ps_YearRange> list = Itop.Client.Common.Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);
            if (list.Count > 0)
            {
                firstyear = list[0].StartYear;
                endyear = list[0].FinishYear;
            }
            else
            {
                firstyear = 1990;
                endyear = 2020;
                py.BeginYear = 1990;
                py.FinishYear = endyear;
                py.StartYear = firstyear;
                py.EndYear = 2060;
                py.ID = Guid.NewGuid().ToString();
                Itop.Client.Common.Services.BaseService.Create<Ps_YearRange>(py);
            }
            for (int i = firstyear; i <= endyear; i++)
            {
                li.Add(i);
            }

            FormChooseYears1 cy = new FormChooseYears1();
            cy.ListYearsForChoose = li;
            if (cy.ShowDialog() != DialogResult.OK)
                return;
   
            foreach (DataRow a in cy.DT.Rows)
            {
                if (a["B"].ToString() == "True")
                    ht.Add(Guid.NewGuid().ToString(), Convert.ToInt32(a["A"].ToString().Replace("年", "")));

                if (a["C"].ToString() == "True")
                    ht1.Add(Guid.NewGuid().ToString(), Convert.ToInt32(a["A"].ToString().Replace("年", "")));
            }
            shjjbyyear = true;
            //电力发展实绩社会经济用电情况
            build_dlhistoryjjyd();
            //再回到原始状态
            shjjbyyear = false;
        }
    }
}