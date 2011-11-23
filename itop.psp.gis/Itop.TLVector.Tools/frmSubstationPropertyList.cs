using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;

namespace ItopVector.Tools
{
    public partial class frmSubstationPropertyList : FormModuleBase
    {
        private DevExpress.XtraGrid.GridControl grid;
        private bool isselect = false;

        public bool IsSelect
        {
            get { return isselect; }
            set { isselect = value; }
        }
        public DevExpress.XtraGrid.GridControl DevGrid
        {
            get { return grid; }
            set { grid = value; }
        }
        public frmSubstationPropertyList()
        {

            InitializeComponent();
            if(!isselect){
                btsel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }
        public void InitData(string svgUid,string sid)
        {
            ctrlSubStationProperty1.InitData(svgUid,sid);
        }
       
        protected override void Print()
        {
            ctrlSubStationProperty1.PrintPreview();
        }

        private void frmglebePropertyList_Load(object sender, EventArgs e)
        {
            barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.Text = ctrlSubStationProperty1.GridControl.Text;
        }

        private void btsel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grid = ctrlSubStationProperty1.GridControl;
            this.DialogResult = DialogResult.OK;

        }
    }
}