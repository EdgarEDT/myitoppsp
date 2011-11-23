using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Itop.Client.Layouts
{
    public partial class FrmGHBZTL : Itop.Client.Base.FormModuleBase
    {
        public FrmGHBZTL()
        {
            InitializeComponent();
        }

        private void FrmLayouts_Load(object sender, EventArgs e)
        {
            //bar.InsertItem(bar.ItemLinks[5], barButtonItem1);
            this.ctrlLayout1.RightObject = smdgroup;
            this.ctrlLayout1.Visu = true;
            this.ctrlLayout1.RefreshData();
        }

        protected override void Add()
        {
            this.ctrlLayout1.AddObject();
        }

        protected override void Edit()
        {
            this.ctrlLayout1.UpdateObject();
        }

        protected override void Del()
        {
            this.ctrlLayout1.DeleteObject();
        }

        protected override void Print()
        {
            this.ctrlLayout1.PrintPreview();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormLayoutType flt = new FormLayoutType();
            //flt.Type = 
            //flt.ShowDialog();
        }
    }
}