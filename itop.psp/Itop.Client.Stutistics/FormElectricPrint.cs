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
    public partial class FormElectricPrint : FormBase
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

        public FormElectricPrint()
        {
            InitializeComponent();
        }

        private void Form1Print_Load(object sender, EventArgs e)
        {
            if (!isselect)
            {
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }

            DevExpress.XtraGrid.Columns.GridColumn gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();

            DevExpress.XtraGrid.Columns.GridColumn gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            DevExpress.XtraGrid.Columns.GridColumn gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            DevExpress.XtraGrid.Columns.GridColumn gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            
            this.gridView1.GroupPanelText = this.Text;

            //int numi = 0;

            //DevExpress.XtraGrid.Views.BandedGrid.GridBand[] gridBandDate = new DevExpress.XtraGrid.Views.BandedGrid.GridBand[listCount * 2];
            //DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] gridColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[listCount * 2];
            int i = 1;
            foreach (DataColumn dc in GridDataTable.Columns)
            {
                //if (dc.ColumnName.IndexOf("Äê") > 0)
                //{

                    //DevExpress.XtraGrid.Views.BandedGrid.GridBand gbi = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                    //gbi.Caption = dc.ColumnName;
                    //gbi.Name = dc.ColumnName;
                    //gbi.AppearanceHeader.Options.UseTextOptions = true;
                    //gbi.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    //gridView1.Columns.Add(gbi);
                    //gridBand2.Children.Add(gbi);


                    //this.bandedGridColumn1.AppearanceHeader.Options.UseTextOptions = true;
                    //this.bandedGridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    //this.bandedGridColumn1.Caption = "ÏîÄ¿";
                    //this.bandedGridColumn1.FieldName = "Title";
                    //this.bandedGridColumn1.Name = "bandedGridColumn1";
                    //this.bandedGridColumn1.Visible = true;
                    //this.bandedGridColumn1.VisibleIndex = 0;

                //if (dc.ColumnName == "Title" || dc.ColumnName == "ID" || dc.ColumnName == "Code" || dc.ColumnName == "Flag" || dc.ColumnName == "Flag2" || dc.ColumnName == "CreateTime" || dc.ColumnName == "UpdateTime" || dc.ColumnName == "L1" || dc.ColumnName == "L2" || dc.ColumnName == "L3" || dc.ColumnName == "L4" || dc.ColumnName == "L5" || dc.ColumnName == "L6" || dc.ColumnName == "IsConn" || dc.ColumnName == "Remark" || dc.ColumnName == "EndYear" || dc.ColumnName == "StartYear" || dc.ColumnName == "ParentID")
                //{

                //}

                //else
                {
                    gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
                    gridColumn.FieldName = dc.ColumnName;
                    gridColumn.Name = "Column" + dc.ColumnName;
                    gridColumn.ColumnEdit = this.repositoryItemMemoEdit1;
                    gridColumn.VisibleIndex = i;
                    gridColumn.Caption = dc.Caption;
                    gridView1.Columns.Add(gridColumn);
                
                
                }
                i++;
            }

            gridColumn1.Visible = false;
            gridColumn1.VisibleIndex = -1;
            gridColumn2.Visible = false;
            gridColumn2.VisibleIndex =-1;
            gridColumn3.Visible = false;
            gridColumn3.VisibleIndex = -1;
            gridControl1.DataSource = GridDataTable;

          
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
            FileClass.ExportExcel(this.gridControl1);
        }
    }
}