using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;

namespace Itop.Client.Table
{
    public partial class frmEnterpriseList : FormModuleBase 
    {
        public frmEnterpriseList()
        {
            InitializeComponent();
        }
        public void InitData()
        {
            ctrlLineProperty1.RefreshData();
        }
       
        protected override void Print()
        {
            ctrlLineProperty1.PrintPreview();
        }

        protected override void Add()
        {
            ctrlLineProperty1.AddObject();
        }
        protected override void Edit()
        {
            ctrlLineProperty1.UpdateObject();
        }
        protected override void Del()
        {
            ctrlLineProperty1.DeleteObject();
        }
        private void frmglebePropertyList_Load(object sender, EventArgs e)
        {
            barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
         

            //this.bar.InsertItem(this.bar.ItemLinks[0], barButtonItem1);
            InitData();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            FileClass.ExportExcel(this.ctrlLineProperty1.GridControl);
        }
    }
}