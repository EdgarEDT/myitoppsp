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
using Itop.Common;
using Itop.Domain.Table;
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
    public partial class FrmBurdenLineType : FormBase
    {
        private IList<PS_Table_AreaWH> AreaList = null;
        private bool isselect = false;
        private string title = "";
        private bool printManage = true;
        System.Data.DataTable dataTable = new System.Data.DataTable();
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
            set { isselect = value; }
        }


        public FrmBurdenLineType()
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


                IList<BurdenLine> list = Services.BaseService.GetList<BurdenLine>("SelectBurdenLineByType", " IsType = '1' and  uid like '%" + Itop.Client.MIS.ProgUID + "%' order by year(BurdenDate)");
                IList<BurdenByte> li1 = Services.BaseService.GetList<BurdenByte>("SelectBurdenByteByYear", " IsType = '1' and  uid like '%" + Itop.Client.MIS.ProgUID + "%' group by year(burdendate)  order by year(BurdenDate)");
                IList<BurdenByte> li2 = Services.BaseService.GetList<BurdenByte>("SelectBurdenByteByIsType", " IsType = '1' and  uid like '%" + Itop.Client.MIS.ProgUID + "%'   order by year(BurdenDate)");
                dt = Itop.Common.DataConverter.ToDataTable((IList)list, typeof(BurdenLine));
                //chart1.DataSource = dt;

                int licount1 = li1.Count;
                int licount2 = li2.Count;

                int numi = 0;
                int numj = 0;
                int numk = 0;

                if (li1.Count > 0)
                {
                   // this.bandedGridView1.GroupPanelText = "本地区" + li1[0].BurdenYear.ToString() + "年/" + li1[licount1 - 1].BurdenYear.ToString() + "年夏季和冬季典型日负荷曲线表";
                    this.bandedGridView1.GroupPanelText = li1[0].BurdenYear.ToString() + "年/" + li1[licount1 - 1].BurdenYear.ToString() + "年夏季和冬季典型日负荷曲线表";
                }
                

                DevExpress.XtraGrid.Views.BandedGrid.GridBand[] gridBand = new DevExpress.XtraGrid.Views.BandedGrid.GridBand[licount1 * 2];
                DevExpress.XtraGrid.Views.BandedGrid.GridBand[] gridBandDate = new DevExpress.XtraGrid.Views.BandedGrid.GridBand[licount2];
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] gridColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[licount1 + licount2];


                foreach (BurdenByte bb1 in li1)
                {
                    gridBand[numi] = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                    gridBand[numi].Caption = bb1.BurdenYear.ToString() + "年";

                    gridBand[numi].Name = "gridBand" + bb1.BurdenYear.ToString();
                    gridBand[numi].AppearanceHeader.Options.UseTextOptions = true;
                    gridBand[numi].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    gridColumn[numk]=new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                    gridColumn[numk].Caption = "Year" + bb1.BurdenYear.ToString();
                    gridColumn[numk].FieldName = "Year"+bb1.BurdenYear.ToString();
                    gridColumn[numk].Name = "Year" + bb1.BurdenYear.ToString();
                    gridColumn[numk].Visible = true;
                    gridColumn[numk].Width = 100;
                    gridColumn[numk].AppearanceCell.Options.UseTextOptions = true;
                    gridColumn[numk].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    dataTable.Columns.Add("Year" + bb1.BurdenYear.ToString(), typeof(string));

                    bandedGridView1.Columns.Add(gridColumn[numk]);
                    gridBand[numi].Columns.Add(gridColumn[numk]);

                    this.bandedGridView1.Bands.Add(gridBand[numi]);
                    numi++;
                    numk++;

                    gridBand[numi] = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                    gridBand[numi].Caption = "典型负荷日";
                    gridBand[numi].Name = "gridBandBurden" + bb1.BurdenYear.ToString();
                    gridBand[numi].AppearanceHeader.Options.UseTextOptions = true;
                    gridBand[numi].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    this.bandedGridView1.Bands.Add(gridBand[numi]);
                    

                    foreach (BurdenByte bb2 in li2)
                    {
                        if (bb1.BurdenYear == bb2.BurdenYear)
                        {
                            DateTime datetime = bb2.BurdenDate;
                            gridBandDate[numj] = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                            gridBandDate[numj].Name = "gridBandDate" + datetime.ToString("yyyyMMdd");
                            //gridBandDate[numj].Caption = bb2.Season + "(" + datetime.ToString("MM月dd日") + ")";
                            //gridBandDate[numj].Caption = FindArea(bb2.AreaID).ToString()+"_"+bb2.Season + "(" + datetime.ToString("MM月dd日") + ")";
                            gridBandDate[numj].Caption =  bb2.Season + "(" + datetime.ToString("MM月dd日") + ")";
                            gridBandDate[numj].AppearanceHeader.Options.UseTextOptions = true;
                            gridBandDate[numj].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                            gridColumn[numk] = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                            gridColumn[numk].Caption = "Day" +FindArea(bb2.AreaID).ToString()+"_"+ datetime.ToString("yyyyMMdd");
                            gridColumn[numk].FieldName = "Day" + FindArea(bb2.AreaID).ToString() + "_" + datetime.ToString("yyyyMMdd");
                            gridColumn[numk].Name = "Day" + FindArea(bb2.AreaID).ToString() + "_" + datetime.ToString("yyyyMMdd");
                            gridColumn[numk].Visible = true;
                            gridColumn[numk].Width = 100;
                            gridColumn[numk].DisplayFormat.FormatString = "n2";
                            //gridColumn[numk].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            //dataTable.Columns.Add("Day" + FindArea(bb2.AreaID).ToString() + "_" + datetime.ToString("yyyyMMdd"), typeof(double));
                            dataTable.Columns.Add("Day" + FindArea(bb2.AreaID).ToString() + "_" + datetime.ToString("yyyyMMdd"), typeof(object));
                            bandedGridView1.Columns.Add(gridColumn[numk]);
                            gridBandDate[numj].Columns.Add(gridColumn[numk]);
                            gridBand[numi].Children.Add(gridBandDate[numj]);
                            numk++;
                            numj++;

                        }

                    }
                    numi++;
                }


                DataRow row1 = dataTable.NewRow();
                foreach (DataColumn column in dataTable.Columns)
                {
                    if (column.ColumnName.Substring(0, 4) == "Year")
                        row1[column] = "小时";
                }
                foreach (DataColumn column in dataTable.Columns)
                {
                    if (column.ColumnName.Substring(0, 3) == "Day")
                        row1[column] = column.ColumnName.Substring(3, column.ColumnName.IndexOf("_")-3);
                }
                dataTable.Rows.Add(row1);


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
                            row["Day" +FindArea(bl.AreaID).ToString()+ "_"+dateTime.ToString("yyyyMMdd")] = (double)pi.GetValue(bl, null);
                        }
                    }


                    //string aaa = "1111";
                    dataTable.Rows.Add(row);
                
                
                }



                 row1 = dataTable.NewRow();
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
                            row1["Day" + FindArea(bl.AreaID).ToString() + "_"+dateTime.ToString("yyyyMMdd")] = Math.Round((double)pi.GetValue(bl, null) * 100,4);
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
                            row1["Day" + FindArea(bl.AreaID).ToString() +"_"+ dateTime.ToString("yyyyMMdd")] = Math.Round((double)pi.GetValue(bl, null) * 100,4);
                        }

                    }
                }
                dataTable.Rows.Add(row1);



                foreach (DataRow row in dt.Rows)
                {
                    string seriesName="";
                    try
                    {
                        DateTime dateTime = (DateTime)row["BurdenDate"];
                        seriesName = FindArea(row["AreaID"].ToString()).ToString()+"_"+dateTime.ToString("yyyy年MM月dd日");
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
                getText = columnname.Substring(4, columnname.Length - 4)+"时";       
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
           // System.Data.DataTable dt = dataTable.Clone();
       
           //  object[] obj = new object[dt.Columns.Count];
           // for (int i = 0; i < dataTable.Rows.Count; i++)
           // {
           //     dataTable.Rows[i].ItemArray.CopyTo(obj, 0);
           //     dt.Rows.Add(obj);
           // }
           // dt.Columns.RemoveAt(2);
           // FileClass.ExportExcel(dt, this.bandedGridView1.GroupPanelText, false, true,false);
           //// ExportExcel(dt);
            FileClass.ExportExcel(this.bandedGridView1.GroupPanelText, "", this.gridControl1);
        }

        public void ExportExcel(System.Data.DataTable datatable)
        {
            //try
            //{
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string fname = "";
            saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fname = saveFileDialog1.FileName;
                try
                {
                    //gridControl.ExportToExcelOld(fname);
                    exportexcel(datatable, fname);
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


            //return true;
            //}
            //catch { }
        }
        private void exportexcel(System.Data.DataTable dt,string filename)
        {
            Microsoft.Office.Interop.Excel._Workbook oWB;
            Microsoft.Office.Interop.Excel.Series oSeries;
          //  Microsoft.Office.Interop.Excel.Range oResizeRange;
            Microsoft.Office.Interop.Excel._Chart oChart;
            //String sMsg;
            //int iNumQtrs;
            GC.Collect();//系统的垃圾回收

            //string filename = @"C:\Documents and Settings\tongxl\桌面\nnn.xls";
            //Microsoft.Office.Interop.Excel.Application ep = new Microsoft.Office.Interop.Excel.Application();
            //Microsoft.Office.Interop.Excel._Workbook wb = ep.Workbooks.Add(filename);

            Microsoft.Office.Interop.Excel.Application ep = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook wb = ep.Workbooks.Add(true);

            ep.Visible = true;
            Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;
            Microsoft.Office.Interop.Excel._Worksheet ws = (Microsoft.Office.Interop.Excel._Worksheet)sheets.get_Item(1);// [System.Type.Missing];//.get.get_Item("xx");
            ws.UsedRange.Select();
            ws.UsedRange.Copy(System.Type.Missing);
            // wb.Charts.Add(System.Type.Missing, System.Type.Missing, 1, System.Type.Missing);
           
            int rowIndex = 1;
            int colIndex = 1;
            foreach (DataColumn col in dt.Columns)
            {  
                ws.Cells[rowIndex, colIndex] = col.ColumnName;
            colIndex++;

            }

            for (int drvIndex = 0; drvIndex < dt.Rows.Count; drvIndex++)
            {
                DataRow row = dt.Rows[drvIndex];
                colIndex = 1;

                foreach (DataColumn col in dt.Columns)
                {
                    
                        ws.Cells[drvIndex + 2, colIndex] = row[col.ColumnName].ToString();
                        colIndex++;
                }
            }
            oWB = (Microsoft.Office.Interop.Excel._Workbook)ws.Parent;
            oChart = (Microsoft.Office.Interop.Excel._Chart)oWB.Charts.Add(Missing.Value, Missing.Value,
                Missing.Value, Missing.Value);

            oChart.ChartWizard(ws.get_Range(ws.Cells[1, 1], ws.Cells[30, 30]), Microsoft.Office.Interop.Excel.XlChartType.xlLine, Missing.Value,
         XlRowCol.xlColumns, true, true, true,
        this.bandedGridView1.GroupPanelText, Missing.Value, Missing.Value, Missing.Value);
            oSeries = (Microsoft.Office.Interop.Excel.Series)oChart.SeriesCollection(1);

            oChart.PlotVisibleOnly = false;
           // oChart.HasDataTable = true;

            Microsoft.Office.Interop.Excel.Axis axis = (Microsoft.Office.Interop.Excel.Axis)oChart.Axes(
        Microsoft.Office.Interop.Excel.XlAxisType.xlValue,
        Microsoft.Office.Interop.Excel.XlAxisGroup.xlPrimary);


            Microsoft.Office.Interop.Excel.Axis ax = (Microsoft.Office.Interop.Excel.Axis)oChart.Axes(
       Microsoft.Office.Interop.Excel.XlAxisType.xlCategory, Microsoft.Office.Interop.Excel.XlAxisGroup.xlPrimary);

            //ax.HasTitle = true;
            //ax.AxisTitle.Text = "Sales Figures";
            ax.HasMajorGridlines = true;

            //string filename = @"C:\Documents and Settings\tongxl\桌面\ccsb.xls";
            ws.SaveAs(filename, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

        }
    }
}