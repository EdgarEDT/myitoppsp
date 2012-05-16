using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Itop.TLPSP.DEVICE
{
    public partial class FrmResult1 : DevExpress.XtraEditors.XtraForm
    {
        public FrmResult1()
        {
            InitializeComponent();
        }

        public DataTable DT;
        public DataTable DT1;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            gridControl1.DataSource = DT;
            gridControl2.DataSource = DT1;

        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControl1.ExportToExcelOld("C:\\temp.xls");
            gridControl2.ExportToExcelOld("C:\\temp1.xls");
            System.Diagnostics.Process.Start("C:\\temp.xls");
            System.Diagnostics.Process.Start("C:\\temp1.xls");
        }
    }
}