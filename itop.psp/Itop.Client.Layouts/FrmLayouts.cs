using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.RightManager;
namespace Itop.Client.Layouts
{
    public partial class FrmLayout : Itop.Client.Base.FormBase
    {
        public FrmLayout()
        {
            InitializeComponent();
            barButtonItem1.Glyph = Itop.ICON.Resource.新建;
            barButtonItem4.Glyph = Itop.ICON.Resource.修改;
            barButtonItem5.Glyph = Itop.ICON.Resource.删除;
            barButtonItem6.Glyph = Itop.ICON.Resource.修改;
            barButtonItem8.Glyph = Itop.ICON.Resource.打印;
            barButtonItem7.Glyph = Itop.ICON.Resource.关闭; 
        }
        //private VsmdgroupProg vp = new VsmdgroupProg();
        private void FrmLayouts_Load(object sender, EventArgs e)
        {
            //bar.InsertItem(bar.ItemLinks[5], barButtonItem1);
            this.ctrlLayout1.RightObject =smdgroup ;
            this.ctrlLayout1.Visu = true;
            this.ctrlLayout1.RefreshData();
        }

        //protected override void Add()
        //{
        //    this.ctrlLayout1.AddObject();
        //}

        //protected override void Edit()
        //{
        //    this.ctrlLayout1.UpdateObject();
        //}

        //protected override void Del()
        //{
        //    this.ctrlLayout1.DeleteObject();
        //}
        
        //protected override void Print()
        //{
        //    this.ctrlLayout1.PrintPreview();
        //}

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormLayoutType flt = new FormLayoutType();
            //flt.Type = 
            //flt.ShowDialog();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.ctrlLayout1.FocusedObject != null)
            {
                FrmLayoutContents dlg = new FrmLayoutContents();
                dlg.RightObject = smdgroup;
                dlg.LayoutUID = this.ctrlLayout1.FocusedObject.UID;
                dlg.ShowDialog();
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlLayout1.AddObject();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlLayout1.UpdateObject();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlLayout1.DeleteObject();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.ctrlLayout1.FocusedObject != null)
            {
                FrmLayoutContents dlg = new FrmLayoutContents();
                dlg.RightObject = smdgroup;
                dlg.LayoutUID = this.ctrlLayout1.FocusedObject.UID;
                dlg.ShowDialog();
            }
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlLayout1.PrintPreview();
        }
    }
}