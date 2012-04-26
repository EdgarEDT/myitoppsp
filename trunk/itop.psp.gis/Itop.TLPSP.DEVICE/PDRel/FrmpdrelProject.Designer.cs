namespace Itop.TLPSP.DEVICE
{
    partial class FrmpdrelProject
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmpdrelProject));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.ucPdreltype1 = new Itop.TLPSP.DEVICE.UcPdreltype();
            this.ucPdtypenode1 = new Itop.TLPSP.DEVICE.UcPdtypenode();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.ucPdreltype1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.ucPdtypenode1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1102, 547);
            this.splitContainerControl1.SplitterPosition = 239;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // ucPdreltype1
            // 
            this.ucPdreltype1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPdreltype1.Location = new System.Drawing.Point(0, 0);
            this.ucPdreltype1.Name = "ucPdreltype1";
            this.ucPdreltype1.Size = new System.Drawing.Size(279, 638);
            this.ucPdreltype1.TabIndex = 0;
            // 
            // ucPdtypenode1
            // 
            this.ucPdtypenode1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPdtypenode1.Location = new System.Drawing.Point(0, 0);
            this.ucPdtypenode1.Name = "ucPdtypenode1";
            this.ucPdtypenode1.ParentObj = ((Itop.Domain.Graphics.Ps_pdreltype)(resources.GetObject("ucPdtypenode1.ParentObj")));
            this.ucPdtypenode1.Size = new System.Drawing.Size(1000, 638);
            this.ucPdtypenode1.TabIndex = 0;
            // 
            // FrmpdrelProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 547);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FrmpdrelProject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "配电可靠性分析";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private UcPdreltype ucPdreltype1;
        private UcPdtypenode ucPdtypenode1;
    }
}