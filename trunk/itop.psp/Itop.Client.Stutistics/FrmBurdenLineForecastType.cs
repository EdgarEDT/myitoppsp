using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Stutistic;
using Itop.Client.Common;
using System.Collections;
using System.Reflection;
using Dundas.Charting.WinControl;
using Itop.Client.Base;

namespace Itop.Client.Stutistics
{
    public partial class FrmBurdenLineForecastType : FormBase
    {
        private string title = "";
        private bool isselect = false;
        private bool printManage = true;
        private DataTable dataTable = new DataTable();
        private DataTable dts = new DataTable();

        public string Title
        {
            get { return title; }     
        }
        public bool PRINTManage
        {
            set { printManage = value; }
        }

        public bool IsSelect
        {
            set {  isselect=value; }
        }

        public DataTable DT
        {
            set { dts = value; }
            get { return dts; }   
        }



        public FrmBurdenLineForecastType()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();

        private void FrmBurdenLineType_Load(object sender, EventArgs e)
        {
            if (!printManage)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }


            if (!isselect)
            {
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            
            }
            
            try
            {
                chart1.Series.Clear();
                //DataTable dataTable = new DataTable();
                IList<BurdenLineForecast> list = Services.BaseService.GetStrongList<BurdenLineForecast>();

                foreach (BurdenLineForecast bf in list)
                {
                    foreach (DataRow rw in dts.Rows)
                    {
                        if (bf.BurdenYear == int.Parse(rw["A"].ToString()) && (bool)rw["B"])
                            bf.UID = "A";
                    }
                }

                dt.Rows.Clear();


                dt = Itop.Common.DataConverter.ToDataTable((IList)list, typeof(BurdenLineForecast));


                int date1 = 0;//(int)list[0].BurdenYear;
                int date2 = 0;// (int)list[list.Count - 1].BurdenYear;

                int ss=1;
                foreach (BurdenLineForecast bf in list)
                {
                    if (bf.UID == "A")
                    {
                        if(ss<2)
                        date1 = (int)bf.BurdenYear;
                        date2 = (int)bf.BurdenYear;
                        ss++;
                    }
                }
                this.bandedGridView1.GroupPanelText = "本地区" + date1.ToString() + "年/" + date2.ToString() + "年冬夏典型日负荷曲线预测表";
                ////if (list.Count > 0)
                ////{
                ////    int date1 = (int)list[0].BurdenYear;
                ////    int date2 = (int)list[list.Count - 1].BurdenYear;

                ////    this.bandedGridView1.GroupPanelText = "本地区" + date1.ToString() + "年/" + date2.ToString() + "年冬夏典型日负荷曲线预测表";
                ////}

                int numi = 0;
                int numj = 0;
                int numk = 0;
                int listCount = list.Count;
                this.bandedGridView1.Bands.Clear();
                bandedGridView1.Columns.Clear();

                DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBands1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                gridBands1.Caption = "  ";
                gridBands1.Name = "gridBands1";
                gridBands1.Width = 150;
                gridBands1.AppearanceHeader.Options.UseTextOptions = true;
                gridBands1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                this.bandedGridView1.Bands.Add(gridBands1);

                //DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBands2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                //gridBands2.Caption = "   ";
                //gridBands2.Name = "gridBands2";
                //gridBands2.AppearanceHeader.Options.UseTextOptions = true;
                //gridBands2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                //gridBands1.Children.Add(gridBands2);

                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumns1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                gridColumns1.Caption = "";
                gridColumns1.FieldName = "YearBanded";
                gridColumns1.Name = "Year";
                gridColumns1.Visible = true;
                gridColumns1.Width = 150;
                gridColumns1.AppearanceCell.Options.UseTextOptions = true;
                gridColumns1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                bandedGridView1.Columns.Add(gridColumns1);
                gridBands1.Columns.Add(gridColumns1);
                dataTable.Columns.Add("YearBanded", typeof(string));

                DevExpress.XtraGrid.Views.BandedGrid.GridBand[] gridBand = new DevExpress.XtraGrid.Views.BandedGrid.GridBand[listCount];
                DevExpress.XtraGrid.Views.BandedGrid.GridBand[] gridBandDate = new DevExpress.XtraGrid.Views.BandedGrid.GridBand[listCount*2];
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] gridColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[listCount * 2];

                foreach (BurdenLineForecast bl in list)
                {
                    if (bl.UID != "A")
                        continue;


                    string blyear = bl.BurdenYear.ToString();

                    gridBand[numi] = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                    gridBand[numi].Caption = blyear + "年典型负荷日";
                    gridBand[numi].Name = "gridBand" + blyear;
                    gridBand[numi].AppearanceHeader.Options.UseTextOptions = true;
                    gridBand[numi].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    this.bandedGridView1.Bands.Add(gridBand[numi]);

                    gridBandDate[numj] = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                    gridBandDate[numj].Caption = "夏季";
                    gridBandDate[numj].Name = "gridBandDateHour" + blyear;
                    gridBandDate[numj].AppearanceHeader.Options.UseTextOptions = true;
                    gridBandDate[numj].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gridBand[numi].Children.Add(gridBandDate[numj]);

                    gridColumn[numk] = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                    gridColumn[numk].Caption = "Month" + blyear;
                    gridColumn[numk].FieldName = "Month" + blyear;
                    gridColumn[numk].Name = "Month" + blyear;
                    gridColumn[numk].Visible = true;
                    gridColumn[numk].DisplayFormat.FormatString = "n2";
                    gridColumn[numk].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    dataTable.Columns.Add("Month" + blyear, typeof(double));
                    bandedGridView1.Columns.Add(gridColumn[numk]);
                    gridBandDate[numj].Columns.Add(gridColumn[numk]);

                    numj++;
                    numk++;


                    gridBandDate[numj] = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                    gridBandDate[numj].Caption = "冬季";
                    gridBandDate[numj].Name = "gridBandDate" + blyear;
                    gridBandDate[numj].AppearanceHeader.Options.UseTextOptions = true;
                    gridBandDate[numj].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gridBand[numi].Children.Add(gridBandDate[numj]);

                    gridColumn[numk] = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                    gridColumn[numk].Caption = "Day" + blyear;
                    gridColumn[numk].FieldName = "Day" + blyear;
                    gridColumn[numk].Name = "Day" + blyear;
                    gridColumn[numk].Visible = true;
                    gridColumn[numk].DisplayFormat.FormatString = "n2";
                    gridColumn[numk].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    dataTable.Columns.Add("Day" + blyear, typeof(double));

                    bandedGridView1.Columns.Add(gridColumn[numk]);
                    gridBandDate[numj].Columns.Add(gridColumn[numk]);

                    numi++;
                    numj++;
                    numk++;
                }


                DataRow row1 = dataTable.NewRow();
                foreach (DataColumn column in dataTable.Columns)
                {
                    if (column.ColumnName == "YearBanded")
                        row1[column] = "Tmax";

                    foreach (BurdenLineForecast bl in list)
                    {
                        if (bl.UID != "A")
                            continue;

                        PropertyInfo pi = bl.GetType().GetProperty("SummerData");
                        if (pi.GetValue(bl, null) != null)
                        {
                            row1["Month" + bl.BurdenYear.ToString()] = (double)pi.GetValue(bl, null);
                        }


                        pi = bl.GetType().GetProperty("WinterData");
                        if (pi.GetValue(bl, null) != null)
                        {
                            row1["Day" + bl.BurdenYear.ToString()] = (double)pi.GetValue(bl, null);
                        }
                    }









                }
                dataTable.Rows.Add(row1);


                row1 = dataTable.NewRow();
                foreach (DataColumn column in dataTable.Columns)
                {
                    if (column.ColumnName == "YearBanded")
                        row1[column] = "平均负荷率(%)";

                    foreach (BurdenLineForecast bl in list)
                    {
                        if (bl.UID != "A")
                            continue;

                        PropertyInfo pi = bl.GetType().GetProperty("SummerDayAverage");
                        if (pi.GetValue(bl, null) != null)
                        {
                            row1["Month" + bl.BurdenYear.ToString()] = (double)pi.GetValue(bl, null) ;
                        }


                        pi = bl.GetType().GetProperty("WinterDayAverage");
                        if (pi.GetValue(bl, null) != null)
                        {
                            row1["Day" + bl.BurdenYear.ToString()] = (double)pi.GetValue(bl, null) ;
                        }
                    }
                }
                dataTable.Rows.Add(row1);


                row1 = dataTable.NewRow();
                foreach (DataColumn column in dataTable.Columns)
                {
                    if (column.ColumnName == "YearBanded")
                        row1[column] = "最小负荷率(%)";

                    foreach (BurdenLineForecast bl in list)
                    {
                        if (bl.UID != "A")
                            continue;

                        PropertyInfo pi = bl.GetType().GetProperty("SummerMinAverage");
                        if (pi.GetValue(bl, null) != null)
                        {
                            row1["Month" + bl.BurdenYear.ToString()] = (double)pi.GetValue(bl, null) ;
                        }


                        pi = bl.GetType().GetProperty("WinterMinAverage");
                        if (pi.GetValue(bl, null) != null)
                        {
                            row1["Day" + bl.BurdenYear.ToString()] = (double)pi.GetValue(bl, null) ;
                        }
                    }
                }
                dataTable.Rows.Add(row1);


                foreach (DataRow row in dt.Rows)
                {
                    if (row["UID"].ToString() != "A")
                        continue;
                    string seriesName = "";
                    try
                    {
                        seriesName = row["BurdenYear"].ToString()+"年";
                    }
                    catch { }
                    chart1.Series.Add(seriesName);
                    chart1.Series[seriesName].Type = SeriesChartType.Line;
                    chart1.Series[seriesName].BorderWidth = 2;

                    for (int colIndex = 2; colIndex < 6; colIndex++)
                    {
                        string columnName = dt.Columns[colIndex].ColumnName;
                        double YVal = (double)row[columnName];

                        chart1.Series[seriesName].Points.AddXY(GetText(columnName), YVal);
                        
                    }
                }


                gridControl1.DataSource = dataTable;
                this.bandedGridView1.OptionsView.ShowColumnHeaders = false;

                
            }
            catch (Exception ex) { Itop.Common.MsgBox.Show(ex.Message); }

        }

        private string GetText(string columnname)
        {
            string getText = "";
            switch (columnname)
            { 
                case "SummerDayAverage":
                    getText= "夏季平均负荷率";
                    break;
                case "SummerMinAverage":
                    getText = "夏季最小负荷率";
                    break;
                case "WinterDayAverage":
                    getText = "冬季平均负荷率";
                    break;
                case "WinterMinAverage":
                    getText = "冬季最小负荷率";
                    break;          
            }
            return getText; 
        
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ComponentPrint.ShowPreview(this.gridControl1, this.bandedGridView1.GroupPanelText);
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            chart1.Printing.PrintPreview();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            title = this.bandedGridView1.GroupPanelText;
            this.DialogResult = DialogResult.OK;
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FileClass.ExportExcel(this.gridControl1);
        }
    }
}