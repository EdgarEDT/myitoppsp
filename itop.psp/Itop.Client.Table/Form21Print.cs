using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Common;
using Itop.Client.Chen;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class Form21Print : FormBase
    {
        private string title = "";
        private bool isselect = false;

        public string Title
        {
            get { return title; }
        }


        public bool IsSelect
        {
            set { isselect = value; }
        }

        private bool isband = true;
        public bool IsBand
        {
            set { isband = value; }
        }


        private DataTable gridDataTable;
        public DataTable GridDataTable
        {
            get { return gridDataTable; }
            set { gridDataTable = value; }
        }
        public void SetGridWidth(int w1,string f1, int w2)
        {
            gridView1.Columns["Title"].Width = w1;
            gridView1.Columns["Title1"].Width = w2;
            gridView1.Columns["Title"].Caption = f1;
        }
        public Form21Print()
        {
            InitializeComponent();
        }

        private void Form1Print_Load(object sender, EventArgs e)
        {
            if (!isselect)
            {
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }


            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Title");
            dt1.Columns.Add("Title1");

            foreach (DataColumn dc in GridDataTable.Columns)
            {
                if (dc.ColumnName.IndexOf("定") > 0)
                    dt1.Columns.Add(dc.ColumnName, typeof(double));
            }

            string first = "";
            string title1="";
            foreach (DataRow row1 in GridDataTable.Rows)
            {
                try
                {
                    if (row1["Title"].ToString().Substring(0, 2) != "　　")
                    {
                        title1 = row1["Title"].ToString();
                        continue;
                    }
                }
                catch
                {
                    title1 = row1["Title"].ToString();
                    continue;
                }
                DataRow row = dt1.NewRow();
                if (first == title1)
                    row["Title"] = "";
                else
                {
                    row["Title"] = title1;
                    first = title1;
                }
                row["Title1"] = row1["Title"].ToString().Replace("　","");
                foreach (DataColumn dc in GridDataTable.Columns)
                {
                    if (dc.ColumnName.IndexOf("定") > 0)
                    {
                        if (row1[dc.ColumnName] == null || row1[dc.ColumnName] == DBNull.Value)
                            row[dc.ColumnName] = 0;
                        else
                            row[dc.ColumnName] = Convert.ToDouble(row1[dc.ColumnName].ToString());
                      
                    }
                }
                dt1.Rows.Add(row);

            }



            

            //int numi = 0;

            //DevExpress.XtraGrid.Views.BandedGrid.GridBand[] gridBandDate = new DevExpress.XtraGrid.Views.BandedGrid.GridBand[listCount * 2];
            //DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] gridColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[listCount * 2];
           
            foreach (DataColumn dc in GridDataTable.Columns)
            {
                if (dc.ColumnName.IndexOf("定") > 0)
                {
                    //DevExpress.XtraGrid.Views.BandedGrid.GridBand gbi = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                    //gbi.Caption = dc.ColumnName.Replace("定", "");
                    //gbi.Name = dc.ColumnName;
                    //gbi.Width = 75;
                    //gbi.AppearanceHeader.Options.UseTextOptions = true;
                    //gbi.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    //gridBand7.Children.Add(gbi);

                    DevExpress.XtraGrid.Columns.GridColumn gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
                    gridColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gridColumn.Caption = dc.ColumnName;
                    gridColumn.FieldName = dc.ColumnName;
                    gridColumn.Name = "Column"+dc.ColumnName;
                    gridColumn.Visible = true;
                    gridColumn.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                    gridColumn.Width = 89;
                    //gridColumn.DisplayFormat.FormatString = "n2";
                    //gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    //this.gridView1.Columns.Add(gridColumn);

                    gridColumn.VisibleIndex = 100;
                    this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn });




                    //gbi.Columns.Add(gridColumn);

                }



            }
            if(isband)
                gridControl1.DataSource = dt1;
            else
                gridControl1.DataSource = GridDataTable;
            this.gridView1.GroupPanelText = this.Text;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            



        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ComponentPrint.ShowPreview(this.gridControl1, this.gridView1.GroupPanelText);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            title = this.gridView1.GroupPanelText;
            this.DialogResult = DialogResult.OK;
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        private bool bHe = true;
        public bool BHe
        {
            set { bHe = value; }
        }
        private string title1 = "",dw1="";
        public string Title1
        {
            set { title1 = value; }
        }
        public string Dw1
        {
            set { dw1 = value; }
        }
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExportExcel.ExportToExcelOld(this.gridControl1,this.Text,dw1);//(this.gridControl1,1,"A",bHe);
        }
    }
}