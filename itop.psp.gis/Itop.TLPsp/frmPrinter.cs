using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using Itop.Client.Base;
namespace Itop.TLPsp {
    public partial class frmPrinter : FormBase {
        public static PageSettings PageSettings;
        
        public frmPrinter() {
            InitializeComponent();
            if (PageSettings == null) {
                PageSettings = new PageSettings();
                PageSettings.Margins.Bottom=50;
                PageSettings.Margins.Left = 50;
                PageSettings.Margins.Right = 50;
                PageSettings.Margins.Top = 50;
            }
            printDocument1.DefaultPageSettings = PageSettings;
            pageSetupDialog1.PageSettings = PageSettings;

            barEditItem1.EditValue = (object)PrintHelper.ShowMap;
            barEditItem2.EditValue = (object)PrintHelper.ShowCenter;
            barEditItem3.EditValue = (object)PrintHelper.ShowPolygon;


            //this.checkBox1.Checked = PrintHelper.ShowMap;
            //this.checkBox2.Checked = PrintHelper.ShowCenter;
            //this.checkBox3.Checked = PrintHelper.ShowPolygon;
        }
        public PrintHelper printHelper = null;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
            if(printHelper!=null)
                printHelper.pdoc_PrintPage(sender, e);
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            printPreviewControl1.AutoZoom = true;
            zoom = printPreviewControl1.Zoom;
        }
        double zoom = 0f;
       
    
        private void printPreviewControl1_MouseClick(object sender, MouseEventArgs e) {
            double f1 = printPreviewControl1.Zoom;
            if (f1 <= 1) {
                f1 *= 1.6;
            } else {
                f1 = zoom;
            }
            printPreviewControl1.Zoom = f1;
        }

        private void toolStripButton3_Click(object sender, EventArgs e) {
           
        }
       
        //����
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pageSetupDialog1.ShowDialog() == DialogResult.OK)
            {
                printPreviewControl1.InvalidatePreview();
            }
        }
        //ˢ��
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            printPreviewControl1.InvalidatePreview();
        }
        //��ӡ
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            printDocument1.Print();
        }
        //�ر�
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        //��ʾ��ͼ
        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit chk = (DevExpress.XtraEditors.CheckEdit)sender;
            PrintHelper.ShowMap = chk.Checked;
        }
        //������ʾ
        private void repositoryItemCheckEdit2_CheckedChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit chk = (DevExpress.XtraEditors.CheckEdit)sender;
            PrintHelper.ShowCenter = chk.Checked;
        }
        //������ʾ
        private void repositoryItemCheckEdit3_CheckedChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit chk = (DevExpress.XtraEditors.CheckEdit)sender;
            PrintHelper.ShowPolygon = chk.Checked;
        }

        
    }
}