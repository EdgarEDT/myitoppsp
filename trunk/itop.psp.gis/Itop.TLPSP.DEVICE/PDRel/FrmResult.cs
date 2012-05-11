using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
namespace Itop.TLPSP.DEVICE.PDRel
{
    public partial class FrmResult : DevExpress.XtraEditors.XtraForm
    {
        public FrmResult()
        {
            InitializeComponent();
        }
        public DataTable DT;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            gridControl1.DataSource = DT;
        } 
          private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
          {
              gridControl1.ExportToExcelOld("C:\\temp.xls");
              System.Diagnostics.Process.Start("C:\\temp.xls");
          }
    }
}