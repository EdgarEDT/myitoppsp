using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
using System.Collections;
namespace ItopVector.Tools
{
    public partial class frmglebePropertyList : FormModuleBase
    {
        public frmglebePropertyList()
        {
            InitializeComponent();
        }
        public void InitData(string svgUid,string sid)
        {
            ctrlglebeProperty1.InitData(svgUid,sid);
        }
        public void InitDataSub(string svgUid,string sid)
        {
            ctrlglebeProperty1.InitDataSub(svgUid,sid);
        }
        public void InitDataSub(string svgUid, string sid,string tb)
        {
            ctrlglebeProperty1.InitDataSub(svgUid, sid,tb);
        }

        protected override void Print()
        {
            ctrlglebeProperty1.PrintPreview();
        }

        private void frmglebePropertyList_Load(object sender, EventArgs e)
        {
            barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.Text = ctrlglebeProperty1.GridControl.Text;
        }
    }
}