using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Itop.Client.Layouts
{
    /// <summary>
    /// Summary description for FrmFiles.
    /// </summary>
    public class FrmFiles : Itop.Client.Base.FormModuleBase
    {
        private CtrlFiles ctrlFiles1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public FrmFiles()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFiles));
            this.SuspendLayout();
            // 
            // il
            // 
            this.il.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.il.ImageSize = new System.Drawing.Size(24, 24);
            this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
            this.il.Images.SetKeyName(0, "");
            this.il.Images.SetKeyName(1, "");
            this.il.Images.SetKeyName(2, "");
            this.il.Images.SetKeyName(3, "");
            this.il.Images.SetKeyName(4, "");
            // 
            // FrmFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(546, 384);
            this.Name = "FrmFiles";
            this.Text = "FrmFiles";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmFiles_Load);
            this.ResumeLayout(false);

        }
        #endregion

        private void FrmFiles_Load(object sender, EventArgs e)
        {
            ctrlFiles1 = new CtrlFiles();
            ctrlFiles1.BL = true;
            ctrlFiles1.Name = ProjectUID;
            this.Controls.Add(ctrlFiles1);
            ctrlFiles1.Dock = System.Windows.Forms.DockStyle.Fill;
        }
        public void Show()
        {
            this.ShowDialog();
        }
        protected override void Add()
        {
            ctrlFiles1.AddObject();
        }

        protected override void Edit()
        {
            ctrlFiles1.UpdateObject();
        }

        protected override void Del()
        {
            ctrlFiles1.DeleteObject();
        }

        protected override void Print()
        {
            this.ctrlFiles1.PrintPreview();
        }
    }
}

