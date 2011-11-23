using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Itop.Client.Stutistics
{
    public partial class FrmPowerStuff : Itop.Client.Base.FormBase
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


        public FrmPowerStuff()
        {
            InitializeComponent();
            IList<int> list = new List<int>();
            BindingList<int> bindingList = new BindingList<int>(list);
        }

        private void FrmPowerStuff_Load(object sender, EventArgs e)
        {
            barButtonItem12.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (!isSelect)
            {
                barButtonItem11.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }


            this.ctrlPowerStuffList1.GridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(GridView_FocusedRowChanged);
            this.ctrlPowerStuffList1.GridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(GridView_CellValueChanged);
            this.ctrlPowerStuffList1.RefreshData();

            if (!EditRight)
                this.ctrlPowerStuffList1.AllowUpdate = false;
        }

        void GridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (this.ctrlPowerStuffList1.FocusedObject == null)
                return;
            this.ctrlPowerStuff1.LineUID = this.ctrlPowerStuffList1.FocusedObject.UID;
            this.ctrlPowerStuff1.LineName = this.ctrlPowerStuffList1.FocusedObject.ListName;
        }

        void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (this.ctrlPowerStuffList1.FocusedObject == null)
                return;
            this.ctrlPowerStuff1.LineUID = this.ctrlPowerStuffList1.FocusedObject.UID;
            this.ctrlPowerStuff1.LineName = this.ctrlPowerStuffList1.FocusedObject.ListName;
            this.ctrlPowerStuff1.RefreshData();
            Right();

            //this.ctrlPowerStuff1.GridView.GroupPanelText = this.ctrlPowerStuffList1.FocusedObject.ListName;
        }
        private void Right()
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

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerStuffList1.AddObject();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerStuffList1.UpdateObject();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerStuffList1.DeleteObject();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerStuff1.AddObject();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerStuff1.UpdateObject();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerStuff1.DeleteObject();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerStuffList1.PrintPreview();
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerStuff1.PrintPreview();
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerStuff1.AddObject1();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.ctrlPowerStuffList1.FocusedObject == null)
                return;

            ctrlPowerStuff1.InitGrid();
            title = this.ctrlPowerStuffList1.FocusedObject.ListName;
            ctrlPowerStuff1.gridView1.OptionsView.ColumnAutoWidth = false;
            ctrlPowerStuff1.gridControl1.Width = 1000;
            gcontrol = ctrlPowerStuff1.gridControl1;
            this.DialogResult = DialogResult.OK;

        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerStuff1.InsertData();
        }
    }
}