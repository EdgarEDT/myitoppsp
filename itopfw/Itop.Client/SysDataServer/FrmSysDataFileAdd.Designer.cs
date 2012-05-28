namespace Itop.Client
{
    partial class FrmSysDataFileAdd
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
            this.txtFileName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCanser = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnopenfile = new DevExpress.XtraEditors.SimpleButton();
            this.txtFileDesc = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileDesc.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(98, 25);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(160, 21);
            this.txtFileName.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(32, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "文件名称：";
            // 
            // btnCanser
            // 
            this.btnCanser.Location = new System.Drawing.Point(265, 114);
            this.btnCanser.Name = "btnCanser";
            this.btnCanser.Size = new System.Drawing.Size(75, 23);
            this.btnCanser.TabIndex = 5;
            this.btnCanser.Text = "取 消";
            this.btnCanser.Click += new System.EventHandler(this.btnCanser_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(173, 114);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "确 定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnopenfile
            // 
            this.btnopenfile.Location = new System.Drawing.Point(265, 24);
            this.btnopenfile.Name = "btnopenfile";
            this.btnopenfile.Size = new System.Drawing.Size(57, 23);
            this.btnopenfile.TabIndex = 6;
            this.btnopenfile.Text = "选择文件";
            this.btnopenfile.Click += new System.EventHandler(this.btnopenfile_Click);
            // 
            // txtFileDesc
            // 
            this.txtFileDesc.Location = new System.Drawing.Point(98, 63);
            this.txtFileDesc.Name = "txtFileDesc";
            this.txtFileDesc.Size = new System.Drawing.Size(224, 21);
            this.txtFileDesc.TabIndex = 8;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(32, 66);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "文件描述：";
            // 
            // FrmSysDataFileAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 167);
            this.Controls.Add(this.txtFileDesc);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.btnopenfile);
            this.Controls.Add(this.btnCanser);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.labelControl1);
            this.Name = "FrmSysDataFileAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSysDataFileAdd";
            this.Load += new System.EventHandler(this.FrmSysDataFileAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileDesc.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtFileName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnCanser;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnopenfile;
        private DevExpress.XtraEditors.TextEdit txtFileDesc;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}