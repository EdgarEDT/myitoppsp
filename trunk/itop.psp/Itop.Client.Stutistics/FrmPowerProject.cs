using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Itop.Client.Stutistics
{
    public partial class FrmPowerProject : Itop.Client.Base.FormBase
    {
        string title = "";
        //string unit = "";
        bool isSelect = false;

        DevExpress.XtraGrid.GridControl gcontrol = null;

        public string Title
        {
            get { return title; }
        }

        public string Unit
        {
            get { return "单位:万千伏安、公里"; }
        }

        public DevExpress.XtraGrid.GridControl Gcontrol
        {
            get { return gcontrol; }
        }

        public bool IsSelect
        {
            set { isSelect = value; }
        }


        public FrmPowerProject()
        {
            InitializeComponent();
        }

        private void FrmPowerProject_Load(object sender, EventArgs e)
        {
            barButtonItem12.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (!isSelect)
            {
                barButtonItem11.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }


            this.ctrlPowerProjectList1.GridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(GridView_FocusedRowChanged);
            this.ctrlPowerProjectList1.RefreshData();
            this.ctrlPowerProjectList1.GridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(GridView_CellValueChanged);


            InitRight();
            //this.ctrlPowerProject1.BindData();
        }

        void GridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (this.ctrlPowerProjectList1.FocusedObject == null)
                return;
            this.ctrlPowerProject1.LineUID = this.ctrlPowerProjectList1.FocusedObject.UID;
            this.ctrlPowerProject1.LineName = this.ctrlPowerProjectList1.FocusedObject.ListName;
        }

        private void InitRight()
        {
            if (!AddRight)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (!EditRight)
            {
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem6.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.ctrlPowerProjectList1.AllowUpdate = false;
            }

            if (!DeleteRight)
            {
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem7.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (!PrintRight)
            {
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

        
        
        }

        void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (this.ctrlPowerProjectList1.FocusedObject == null)
                return;
            this.ctrlPowerProject1.LineUID = this.ctrlPowerProjectList1.FocusedObject.UID;
            this.ctrlPowerProject1.LineName = this.ctrlPowerProjectList1.FocusedObject.ListName;
            this.ctrlPowerProject1.RefreshData();
            //this.ctrlPowerProject1.GridView.GroupPanelText = this.ctrlPowerProjectList1.FocusedObject.ListName;
        }



        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerProjectList1.AddObject();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerProjectList1.UpdateObject();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerProjectList1.DeleteObject();
        }



        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerProject1.AddObject();
        }




        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerProjectList1.PrintPreview();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerProject1.UpdateObject();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerProject1.DeleteObject();
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerProject1.PrintPreview();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerProject1.AddObject1();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.ctrlPowerProjectList1.FocusedObject == null)
                return;

            ctrlPowerProject1.InitGrid();
            this.ctrlPowerProject1.gridControl1.Width = 2000;
            //this.ctrlPowerProject1.gridView1.OptionsView.ColumnAutoWidth = false;
            title = this.ctrlPowerProjectList1.FocusedObject.ListName;
            gcontrol = ctrlPowerProject1.gridControl1;
            this.DialogResult = DialogResult.OK;
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerProject1.InsertData();
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (this.ctrlPowerProjectList1.FocusedObject == null)
                return;
            ctrlPowerProject1.InitGrid();
            this.ctrlPowerProject1.gridControl1.Width = 2000;
            //this.ctrlPowerProject1.gridView1.OptionsView.ColumnAutoWidth = false;
            title = this.ctrlPowerProjectList1.FocusedObject.ListName;

            FileClass.ExportExcel(ctrlPowerProject1.gridControl1);
        }

        private void ctrlPowerProject1_Load(object sender, EventArgs e)
        {

        }
    }
}