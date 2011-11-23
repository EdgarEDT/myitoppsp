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
    public partial class Form9Print_LangFang : FormBase
    {
        private string title = "";
        private bool isselect = false;
        private string tzgs = "";
        public string Title
        {
            get { return title; }
        }
        public string TZGS
        {
            set { tzgs = value; }
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

        public Form9Print_LangFang()
        {
            InitializeComponent();
        }

        private void Form1Print_Load(object sender, EventArgs e)
        {

            if (!isselect)
            {
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }
            if (tzgs == "tzgs")
            {
                this.gridColumn7.Visible = false;
            }
            else
            {
                this.gridColumn12.Visible = false;
                this.gridColumn13.Visible = false;
                this.gridColumn14.Visible = false;
            }
            DevExpress.XtraGrid.Columns.GridColumn gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();

            DevExpress.XtraGrid.Columns.GridColumn gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            DevExpress.XtraGrid.Columns.GridColumn gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            DevExpress.XtraGrid.Columns.GridColumn gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            
            this.gridView1.GroupPanelText = this.Text;
            DevExpress.XtraGrid.Columns.GridColumn Column1 = gridView1.Columns["L10"];
            Column1.DisplayFormat.FormatString = "n2";
            Column1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
       int i = 1;
            foreach (DataColumn dc in GridDataTable.Columns)
            {

                 if (dc.ColumnName == "Remark")
                {
                    gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
                    gridColumn3.FieldName = dc.ColumnName;
                    gridColumn3.Name = "Column" + dc.ColumnName;
                    gridColumn3.ColumnEdit = this.repositoryItemMemoEdit1;
                    gridColumn3.VisibleIndex = -1;
                    gridColumn3.Caption = "±¸×¢";
                    gridView1.Columns.Add(gridColumn3);
                }

                else if (dc.ColumnName == "Title" || dc.ColumnName == "ID" || dc.ColumnName == "Code" || dc.ColumnName == "Flag" || dc.ColumnName == "Flag2" || dc.ColumnName == "CreateTime" || dc.ColumnName == "UpdateTime" || dc.ColumnName == "L1" || dc.ColumnName == "L2" || dc.ColumnName == "L3" || dc.ColumnName == "L4" || dc.ColumnName == "L5" || dc.ColumnName == "L6" || dc.ColumnName == "L7" || dc.ColumnName == "L8" || dc.ColumnName == "L9" || dc.ColumnName == "L4" || dc.ColumnName == "L10" || dc.ColumnName == "L11" || dc.ColumnName == "L12" || dc.ColumnName == "L13" || dc.ColumnName == "L14"||dc.ColumnName == "L15" || dc.ColumnName == "L16" || dc.ColumnName == "L17" || dc.ColumnName == "L18" || dc.ColumnName == "L19"|| dc.ColumnName == "IsConn")
                {

                }

                else
                {
                    gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
                    gridColumn.FieldName = dc.ColumnName;
                    gridColumn.Name = "Column" + dc.ColumnName;
                    gridColumn.ColumnEdit = this.repositoryItemMemoEdit1;
                    gridColumn.VisibleIndex = i;
                    gridColumn.Caption = dc.ColumnName;
                    gridView1.Columns.Add(gridColumn);
                
                
                }
                i++;

                
            }

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
            //FileClass.ExportExcel(this.gridControl1);
            FileClass.ExportExcel(this.gridView1.GroupPanelText, "", this.gridControl1);
        }
    }
}