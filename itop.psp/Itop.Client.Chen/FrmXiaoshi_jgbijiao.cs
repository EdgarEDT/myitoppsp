using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.HistoryValue;
using DevExpress.XtraTreeList.Nodes;
using Itop.Client.Common;
using Itop.Client.Base;
namespace Itop.Client.Chen
{
    public partial class FrmXiaoshi_jgbijiao : FormBase
    {
        DataTable dt;
        private bool Isprint=false;
        public bool ISprint
        {
            set { Isprint = value; }
        
        }
        private bool Isedit = false;
        public bool ISedit
        {
            set { Isedit = value; }

        }
        private DataCommon dc = new DataCommon();
        private PSP_ForecastReports forecastReport = null;
        public DataTable DT
        {
           
            set { dt = value; } 
        }
        public FrmXiaoshi_jgbijiao(PSP_ForecastReports fr)
        {
            InitializeComponent();
            forecastReport = fr;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FrmXiaoshi_jgbijiao_Load(object sender, EventArgs e)
        {
            //DT.Rows.+		
            if (!Isprint)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            dt = dc.GetSortTable(dt, "ID", true);

            treeList1.DataSource = dt;
            treeList1.Columns["Title"].Caption = "分类名";
            treeList1.Columns["Title"].Width = 180;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Flag"].VisibleIndex = -1;
            treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["Flag2"].VisibleIndex = -1;
            treeList1.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["B"].VisibleIndex = -1;
            treeList1.Columns["B"].OptionsColumn.ShowInCustomizationForm = false;
            //for (int i = forecastReport.StartYear - forecastReport.HistoryYears-1; i < forecastReport.StartYear; i++)
            //{
            int i=0;
               foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName.IndexOf("年") > 0)
                {
                   i= Convert.ToInt32(col.ColumnName.Replace("年", ""));
                    if (i < forecastReport.StartYear)
                        treeList1.Columns[i + "年"].VisibleIndex = -1;
                    treeList1.Columns[i + "年"].OptionsColumn.ShowInCustomizationForm = false;
                }
            
            }
                RefreshChart2(dt);
           
        }
        private void AddDataToList(DataRow node, IList<PSP_P_Values> list, DataTable dt)
        {
            foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName.IndexOf("年") > 0)
                {
                    object obj = node[col.ColumnName];
                    if (obj != DBNull.Value)
                    {
                        if (Convert.ToInt32(col.ColumnName.Replace("年", "")) < forecastReport.StartYear)
                            continue;
                        PSP_P_Values v = new PSP_P_Values();
                        v.Flag2 = Convert.ToInt32(node["Flag2"]); 
                        v.TypeID = Convert.ToInt32(node["ID"]);
                        v.Caption = node["Title"].ToString() + "," + v.TypeID;
                        v.Year = Convert.ToInt32(col.ColumnName.Replace("年", ""));
                        v.Value = (double)node[col.ColumnName];

                        list.Add(v);
                    }
                }
            }
        }
        private void RefreshChart2(DataTable dt)
        {
            List<PSP_P_Values> listValues = new List<PSP_P_Values>();


            //chart1.DataBindCrossTab(

            foreach (DataRow nd in dt.Rows)
            {
               
                    //if (nd["ID"].ToString() == "80003" || nd["ID"].ToString() == "80004")
                        AddDataToList(nd, listValues, dt);
                
               
            }

            chart1.Series.Clear();

            //if (!CheckChartData(listValues))
            //{
            //    MsgBox.Show("预测数据有异常，曲线图无法显示！");
            //    return;
            //}Title

            chart1.DataBindCrossTab(listValues, "Caption", "Year", "Value", "");
            //chart1.DataBindTable(
            //chart1.DataBindCrossTab(dt, "Title", "Year", "Value", "");
            for (int i = 0; i < chart1.Series.Count; i++)
            {

                chart1.Series[i].Name = (i + 1).ToString() + "." + chart1.Series[i].Name.Substring(0, chart1.Series[i].Name.IndexOf(","));
                chart1.Series[i].Type = Dundas.Charting.WinControl.SeriesChartType.Line;
                chart1.Series[i].MarkerSize = 7;
                chart1.Series[i].MarkerStyle = (Dundas.Charting.WinControl.MarkerStyle)((i + 1) % 10);

            }

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ComponentPrint.ShowPreview(this.treeList1, "电量负荷预测");
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            chart1.Printing.PrintPreview();
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (!Isedit)
                e.Cancel = true;
        }


    }
}