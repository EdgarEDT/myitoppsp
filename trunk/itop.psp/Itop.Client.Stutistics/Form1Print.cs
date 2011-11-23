using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Common;
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
    public partial class Form1Print : FormBase
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





        private DataTable gridDataTable;
        public DataTable GridDataTable
        {
            get { return gridDataTable; }
            set { gridDataTable = value; }
        }

        public Form1Print()
        {
            InitializeComponent();
        }

        private void Form1Print_Load(object sender, EventArgs e)
        {
            if (!isselect)
            {
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }


            gridControl1.DataSource = GridDataTable;
            this.bandedGridView1.GroupPanelText = this.Text;

            //int numi = 0;

            //DevExpress.XtraGrid.Views.BandedGrid.GridBand[] gridBandDate = new DevExpress.XtraGrid.Views.BandedGrid.GridBand[listCount * 2];
            //DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] gridColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[listCount * 2];
            foreach (DataColumn dc in GridDataTable.Columns)
            {
                if (dc.ColumnName.IndexOf("Äê") > 0)
                {
                    DevExpress.XtraGrid.Views.BandedGrid.GridBand gbi = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                    gbi.Caption = dc.ColumnName;
                    gbi.Name = dc.ColumnName;
                    gbi.AppearanceHeader.Options.UseTextOptions = true;
                    gbi.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gridBand2.Children.Add(gbi);

                    DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                    gridColumn.Caption = dc.ColumnName;
                    gridColumn.FieldName = dc.ColumnName;
                    gridColumn.Name = "Column"+dc.ColumnName;
                    gridColumn.Visible = true;
                    gridColumn.DisplayFormat.FormatString = "n2";
                    gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    bandedGridView1.Columns.Add(gridColumn);
                    gbi.Columns.Add(gridColumn);

                }
            }


            //if (!isselect)
            //{
            //    //barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //    this.Hide();
            //    ComponentPrint.ShowPreview(this.gridControl1, this.bandedGridView1.GroupPanelText);
            //    this.Close();
            //}
            //else
            //{
            //    title = this.bandedGridView1.GroupPanelText;
            //    this.DialogResult = DialogResult.OK;
            //}



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
            ComponentPrint.ShowPreview(this.gridControl1, this.bandedGridView1.GroupPanelText);
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