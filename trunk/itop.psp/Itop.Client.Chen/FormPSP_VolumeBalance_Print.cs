using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Common;
using Itop.Client.Chen;
using DevExpress.XtraGrid.Columns;
using Itop.Client.Base;
namespace Itop.Client.Chen
{
    public partial class FormPSP_VolumeBalance_Print : FormBase
    {
        private string title = "";
        private bool isselect = false;
        private bool PrintRight = false;
        public string Title
        {
            get { return title; }
        }


        public bool IsSelect
        {
            set { isselect = value; }
        }
        public bool PRintRight
        {
            set { PrintRight = value; }
        }
        private DataTable gridDataTable;
        public DataTable GridDataTable
        {
            get { return gridDataTable; }
            set { gridDataTable = value; }
        }

        public FormPSP_VolumeBalance_Print()
        {
            InitializeComponent();
        }

        private void Form1Print_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = gridDataTable;
            //this.gridView1.GroupPanelText = this.Text;
            //this.gridView1.OptionsView.ColumnAutoWidth = false;
            if(!PrintRight)
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never; 
            DevExpress.XtraGrid.Columns.GridColumn gridColumn = this.gridView1.Columns.ColumnByFieldName("序号");
           gridColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
           gridColumn.Visible = true;
           gridColumn.ColumnEdit = repositoryItemTextEdit1;
           repositoryItemTextEdit1.ReadOnly = true;
           gridColumn.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
           gridColumn.Width =60;

           gridColumn = this.gridView1.Columns.ColumnByFieldName("项目");        
           gridColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
           gridColumn.Visible = true;
           gridColumn.ColumnEdit = repositoryItemTextEdit1;
           repositoryItemTextEdit1.ReadOnly = true;
           gridColumn.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
           //gridColumn.Width = 10;
           foreach (DataColumn gd in gridDataTable.Columns)
           {
               if (gd.ColumnName.IndexOf("年") > 0)
               {
                   gridColumn = this.gridView1.Columns.ColumnByFieldName(gd.ColumnName);
                   gridColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                   gridColumn.Visible = true;
                   gridColumn.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                   gridColumn.ColumnEdit = repositoryItemMemoEdit1;
                   repositoryItemMemoEdit1.ReadOnly = true;
               
               }

           }


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

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FileClass.ExportExcel2(this.Text,"单位：万千瓦、万千伏安",this.gridControl1);
        }
    }
}