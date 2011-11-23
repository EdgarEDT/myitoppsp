using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using Itop.Client.Base;
namespace ItopVector.Tools {
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

            this.checkBox1.Checked = PrintHelper.ShowMap;
            this.checkBox2.Checked = PrintHelper.ShowCenter;
            this.checkBox3.Checked = PrintHelper.ShowPolygon;
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
        private void toolStripButton1_Click(object sender, EventArgs e) {
            pageSetupDialog1.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e) {
            printDocument1.Print();
        }
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
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            PrintHelper.ShowMap = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e) {
            PrintHelper.ShowCenter = checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e) {
            PrintHelper.ShowPolygon = checkBox3.Checked;
        }

        private void toolStripButton4_Click(object sender, EventArgs e) {
            printPreviewControl1.InvalidatePreview();
        }
    }
}