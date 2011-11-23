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
    public partial class frmLinePropertyList : FormModuleBase
    {
        public frmLinePropertyList()
        {
            InitializeComponent();
        }
        public void InitData(string svgUid,string sid)
        {
            ctrlLineProperty1.InitData(svgUid,sid);
        }
       
        protected override void Print()
        {
            ctrlLineProperty1.PrintPreview();
        }

        private void frmglebePropertyList_Load(object sender, EventArgs e)
        {
            barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.Text = ctrlLineProperty1.GridControl.Text;

            this.bar.InsertItem(this.bar.ItemLinks[0], barButtonItem1);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            FileClass.ExportExcel(this.ctrlLineProperty1.GridControl);
        }
    }
}