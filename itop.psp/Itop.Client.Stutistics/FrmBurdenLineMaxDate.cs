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
using Microsoft.Office.Interop.Excel;
using Itop.Domain.Table;
using Itop.Common;
using Itop.Client.Base;

namespace Itop.Client.Stutistics
{
    public partial class FrmBurdenLineMaxDate : FormBase
    {
        private IList<PS_Table_AreaWH> AreaList = null;
        System.Data.DataTable dataTable = new System.Data.DataTable();
        private string title = "";
        private bool isselect = false;
        private bool printManage = true;

        public string Title
        {
            get { return title; }
        }


        public bool IsSelect
        {
            set { isselect = value; }
        }

        public bool PRINTManage
        {
            set { printManage = value; }
        }


        public FrmBurdenLineMaxDate()
        {
            InitializeComponent();
        }
        System.Data.DataTable dt = new System.Data.DataTable();
        //根据areaid返回Areaname
        private string FindArea(string Areaid)
        {
            string value = "无地区";
            if (AreaList.Count > 0)
            {
                for (int i = 0; i < AreaList.Count; i++)
                {
                    if (AreaList[i].ID == Areaid)
                    {
                        value = AreaList[i].Title.ToString();
                        break;
                    }
                }
            }
            return value;
        }
        private void FrmBurdenLineType_Load(object sender, EventArgs e)
        {
            string pjt = " ProjectID='" + MIS.ProgUID + "'";
            AreaList = Common.Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", pjt);
         
    
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
               // DataTable dataTable = new DataTable();
                IList<BurdenLine> list = Services.BaseService.GetList<BurdenLine>("SelectBurdenLineByMaxDate", " IsMaxDate = '1' and  uid like '%" + Itop.Client.MIS.ProgUID + "%'  order by year(BurdenDate)");
                dt = Itop.Common.DataConverter.ToDataTable((IList)list, typeof(BurdenLine));

                if (list.Count > 0)
                {
                    DateTime date1 = (DateTime)list[0].BurdenDate;
                    DateTime date2 = (DateTime)list[list.Count - 1].BurdenDate;

                    this.bandedGridView1.GroupPanelText = date1.ToString("yyyy年") + "/" + date2.ToString("yyyy年") + "最大负荷日负荷曲线表";
                }

                int numi = 0;
                int numj = 0;
                int numk = 0;
                int listCount = list.Count;

                DevExpress.XtraGrid.Views.BandedGrid.GridBand[] gridBand = new DevExpress.XtraGrid.Views.BandedGrid.GridBand[listCount];
                DevExpress.XtraGrid.Views.BandedGrid.GridBand[] gridBandDate = new DevExpress.XtraGrid.Views.BandedGrid.GridBand[listCount*2];
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] gridColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[listCount * 2];

                foreach (BurdenLine bl in list)
                {
                    DateTime dateTime = (DateTime)bl.BurdenDate;

                    gridBand[numi] = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                    gridBand[numi].Caption = FindArea(bl.AreaID)+"("+ dateTime.ToString("yyyy年MM月dd日")+")";
                    gridBand[numi].Name = "gridBand" + dateTime.ToString("yyyyMMdd");
                    gridBand[numi].AppearanceHeader.Options.UseTextOptions = true;
                    gridBand[numi].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    this.bandedGridView1.Bands.Add(gridBand[numi]);

                    gridBandDate[numj] = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                    gridBandDate[numj].Caption = "小时";
                    gridBandDate[numj].Name = "gridBandDateHour" + dateTime.ToString("yyyyMMdd");
                    gridBandDate[numj].AppearanceHeader.Options.UseTextOptions = true;
                    gridBandDate[numj].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gridBand[numi].Children.Add(gridBandDate[numj]);

                    gridColumn[numk] = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                    gridColumn[numk].Caption = "Year" + FindArea(bl.AreaID) + dateTime.ToString("yyyyMMdd");
                    gridColumn[numk].FieldName = "Year" + FindArea(bl.AreaID) + dateTime.ToString("yyyyMMdd");
                    gridColumn[numk].Name = "Year" + FindArea(bl.AreaID) + dateTime.ToString("yyyyMMdd");
                    gridColumn[numk].Visible = true;
                    gridColumn[numk].AppearanceCell.Options.UseTextOptions = true;
                    gridColumn[numk].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    dataTable.Columns.Add("Year" + FindArea(bl.AreaID) + dateTime.ToString("yyyyMMdd"), typeof(string));
                    bandedGridView1.Columns.Add(gridColumn[numk]);
                    gridBandDate[numj].Columns.Add(gridColumn[numk]);

                    numj++;
                    numk++;


                    gridBandDate[numj] = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                    gridBandDate[numj].Caption = "负荷";
                    gridBandDate[numj].Name = "gridBandDate" + dateTime.ToString("yyyyMMdd");
                    gridBandDate[numj].AppearanceHeader.Options.UseTextOptions = true;
                    gridBandDate[numj].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gridBand[numi].Children.Add(gridBandDate[numj]);

                    gridColumn[numk] = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                    gridColumn[numk].Caption = "Day" + FindArea(bl.AreaID) + dateTime.ToString("yyyyMMdd");
                    gridColumn[numk].FieldName = "Day" + FindArea(bl.AreaID) + dateTime.ToString("yyyyMMdd");
                    gridColumn[numk].Name = "Day" + FindArea(bl.AreaID) + dateTime.ToString("yyyyMMdd");
                    gridColumn[numk].Visible = true;
                    gridColumn[numk].DisplayFormat.FormatString = "n2";
                    gridColumn[numk].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    dataTable.Columns.Add("Day" + FindArea(bl.AreaID) + dateTime.ToString("yyyyMMdd"), typeof(double));

                    bandedGridView1.Columns.Add(gridColumn[numk]);
                    gridBandDate[numj].Columns.Add(gridColumn[numk]);

                    numi++;
                    numj++;
                    numk++;
                }



                for (int i = 1; i <= 24; i++)
                {
                    DataRow row = dataTable.NewRow();
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        if (column.ColumnName.Substring(0, 4) == "Year")
                            row[column] = i.ToString();
                    }


                    foreach (BurdenLine bl in list)
                    {
                        DateTime dateTime = (DateTime)bl.BurdenDate;
                        PropertyInfo pi = bl.GetType().GetProperty("Hour" + i.ToString());
                        if (pi.GetValue(bl, null) != null)
                        {
                            row["Day" + FindArea(bl.AreaID) + dateTime.ToString("yyyyMMdd")] = (double)pi.GetValue(bl, null);
                        }
                    }

                    dataTable.Rows.Add(row);


                }

                DataRow row1 = dataTable.NewRow();
                foreach (DataColumn column in dataTable.Columns)
                {
                    if (column.ColumnName.Substring(0, 4) == "Year")
                        row1[column] = "平均负荷率(%)";

                    foreach (BurdenLine bl in list)
                    {
                        DateTime dateTime = (DateTime)bl.BurdenDate;
                        PropertyInfo pi = bl.GetType().GetProperty("DayAverage");
                        if (pi.GetValue(bl, null) != null)
                        {
                            row1["Day" + FindArea(bl.AreaID) + dateTime.ToString("yyyyMMdd")] = (double)pi.GetValue(bl, null) * 100;
                        }




                    }
                }
                dataTable.Rows.Add(row1);


                row1 = dataTable.NewRow();
                foreach (DataColumn column in dataTable.Columns)
                {
                    if (column.ColumnName.Substring(0, 4) == "Year")
                        row1[column] = "最小负荷率(%)";

                    foreach (BurdenLine bl in list)
                    {
                        DateTime dateTime = (DateTime)bl.BurdenDate;
                        PropertyInfo pi = bl.GetType().GetProperty("MinAverage");
                        if (pi.GetValue(bl, null) != null)
                        {
                            row1["Day" + FindArea(bl.AreaID) + dateTime.ToString("yyyyMMdd")] = (double)pi.GetValue(bl, null) * 100;
                        }

                    }
                }
                dataTable.Rows.Add(row1);


                foreach (DataRow row in dt.Rows)
                {
                    string seriesName = "";
                    try
                    {
                        DateTime dateTime = (DateTime)row["BurdenDate"];
                        seriesName = FindArea(row["AreaID"].ToString()) +"_"+ dateTime.ToString("yyyy年MM月dd日");
                    }
                    catch { }
                    chart1.Series.Add(seriesName);
                    chart1.Series[seriesName].Type = SeriesChartType.Line;
                    chart1.Series[seriesName].BorderWidth = 2;

                    for (int colIndex = 3; colIndex < 26; colIndex++)
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
            if (columnname.Length > 4)
            {
                getText = columnname.Substring(4, columnname.Length - 4) + "时";
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
            //FileClass.ExportExcel(dataTable, this.bandedGridView1.GroupPanelText, false, true,false);
            FileClass.ExportExcel(this.bandedGridView1.GroupPanelText, "", this.gridControl1);
        }

        



       //////// /// <summary>
       //////// /// 
       //////// /// </summary>
       //////// /// <param name="datatable"></param>
       //////// public void ExportExcel(System.Data.DataTable datatable)
       //////// {
       ////////     //try
       ////////     //{
       ////////     SaveFileDialog saveFileDialog1 = new SaveFileDialog();
       ////////     string fname = "";
       ////////     saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
       ////////     if (saveFileDialog1.ShowDialog() == DialogResult.OK)
       ////////     {
       ////////         fname = saveFileDialog1.FileName;
       ////////         try
       ////////         {
       ////////             //gridControl.ExportToExcelOld(fname);
       ////////             exportexcel(datatable, fname);
       ////////             if (MsgBox.ShowYesNo("导出成功，是否打开该文档？") != DialogResult.Yes)
       ////////                 return;

       ////////             System.Diagnostics.Process.Start(fname);
       ////////         }
       ////////         catch
       ////////         {
       ////////             MsgBox.Show("无法保存" + fname + "。请用其他文件名保存文件，或将文件存至其他位置。");
       ////////             return;
       ////////         }
       ////////     }


       ////////     //return true;
       ////////     //}
       ////////     //catch { }
       //////// }
       //////// private void exportexcel(System.Data.DataTable dt, string filename)
       //////// {
       ////////     //System.Data.DataTable dt = new System.Data.DataTable();
            
       ////////     Microsoft.Office.Interop.Excel._Workbook oWB;
       ////////     Microsoft.Office.Interop.Excel.Series oSeries;
       ////////     //Microsoft.Office.Interop.Excel.Range oResizeRange;
       ////////     Microsoft.Office.Interop.Excel._Chart oChart;
       ////////     //String sMsg;
       ////////     //int iNumQtrs;
       ////////     GC.Collect();//系统的垃圾回收

       ////////     //string filename = @"C:\Documents and Settings\tongxl\桌面\nnn.xls";
       ////////     //Microsoft.Office.Interop.Excel.Application ep = new Microsoft.Office.Interop.Excel.Application();
       ////////     //Microsoft.Office.Interop.Excel._Workbook wb = ep.Workbooks.Add(filename);

       ////////     Microsoft.Office.Interop.Excel.Application ep = new Microsoft.Office.Interop.Excel.Application();
       ////////     Microsoft.Office.Interop.Excel._Workbook wb = ep.Workbooks.Add(true);

       ////////     ep.Visible = true;
       ////////     Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;
       ////////     Microsoft.Office.Interop.Excel._Worksheet ws = (Microsoft.Office.Interop.Excel._Worksheet)sheets.get_Item(1);// [System.Type.Missing];//.get.get_Item("xx");
       ////////     ws.UsedRange.Select();
       ////////     ws.UsedRange.Copy(System.Type.Missing);
       ////////     // wb.Charts.Add(System.Type.Missing, System.Type.Missing, 1, System.Type.Missing);
       ////////     int rowIndex = 1;
       ////////     int colIndex = 1;
       ////////     foreach (DataColumn col in dt.Columns)
       ////////     {
       ////////         ws.Cells[rowIndex, colIndex++] = col.ColumnName;
       ////////     }

       ////////     for (int drvIndex = 0; drvIndex < dt.Rows.Count; drvIndex++)
       ////////     {
       ////////         DataRow row = dt.Rows[drvIndex];
       ////////         colIndex = 1;
       ////////         foreach (DataColumn col in dt.Columns)
       ////////         {
       ////////             ws.Cells[drvIndex + 2, colIndex] = row[col.ColumnName].ToString();
       ////////             colIndex++;
       ////////         }
       ////////     }


       ////////     oWB = (Microsoft.Office.Interop.Excel._Workbook)ws.Parent;
       ////////     oChart = (Microsoft.Office.Interop.Excel._Chart)oWB.Charts.Add(Missing.Value, Missing.Value,
       ////////         Missing.Value, Missing.Value);

       ////////     oChart.ChartWizard(ws.get_Range(ws.Cells[1, 1], ws.Cells[30, 30]), Microsoft.Office.Interop.Excel.XlChartType.xlLine, Missing.Value,
       ////////  XlRowCol.xlColumns, true, true, true,
       ////////  this.bandedGridView1.GroupPanelText, Missing.Value, Missing.Value, Missing.Value);
       ////////     oSeries = (Microsoft.Office.Interop.Excel.Series)oChart.SeriesCollection(1);

       ////////     //string str = String.Empty;
       ////////     //for (int I = 1; I < 15; I++)

       ////////     //{
       ////////     //    str += I.ToString() + "\t";  
       ////////     //}
       ////////     //try { oSeries.XValues = str; }
       ////////     //catch { }
       ////////     //  oSeries.HasDataLabels = true;
       ////////     oChart.PlotVisibleOnly = false;
       ////////    // oChart.HasDataTable = true;



       ////////     Microsoft.Office.Interop.Excel.Axis axis = (Microsoft.Office.Interop.Excel.Axis)oChart.Axes(
       //////// Microsoft.Office.Interop.Excel.XlAxisType.xlValue,
       //////// Microsoft.Office.Interop.Excel.XlAxisGroup.xlPrimary);

       ////////     //axis.HasTitle = true;
       ////////     //axis.AxisTitle.Text = "Sales Figures";
       ////////     // axis.HasMinorGridlines=true;
       ////////     Microsoft.Office.Interop.Excel.Axis ax = (Microsoft.Office.Interop.Excel.Axis)oChart.Axes(
       ////////Microsoft.Office.Interop.Excel.XlAxisType.xlCategory, Microsoft.Office.Interop.Excel.XlAxisGroup.xlPrimary);

       ////////     //ax.HasTitle = true;
       ////////     //ax.AxisTitle.Text = "Sales Figures";
       ////////     ax.HasMajorGridlines = true;


       ////////   //  string filename = @"C:\Documents and Settings\tongxl\桌面\ccsb.xls";
       ////////     ws.SaveAs(filename, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
         
       //////// }
    }
}