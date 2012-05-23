namespace Itop.Client
{
    partial class FormDataSetup
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDataSetup));
            this.Tserver = new System.Windows.Forms.ComboBox();
            this.Tpassword = new System.Windows.Forms.TextBox();
            this.Tuserid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // Tserver
            // 
            this.Tserver.FormattingEnabled = true;
            this.Tserver.Location = new System.Drawing.Point(197, 121);
            this.Tserver.Name = "Tserver";
            this.Tserver.Size = new System.Drawing.Size(206, 22);
            this.Tserver.TabIndex = 8;
            // 
            // Tpassword
            // 
            this.Tpassword.Location = new System.Drawing.Point(196, 203);
            this.Tpassword.Name = "Tpassword";
            this.Tpassword.Size = new System.Drawing.Size(207, 22);
            this.Tpassword.TabIndex = 7;
            // 
            // Tuserid
            // 
            this.Tuserid.Location = new System.Drawing.Point(196, 162);
            this.Tuserid.Name = "Tuserid";
            this.Tuserid.Size = new System.Drawing.Size(207, 22);
            this.Tuserid.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(108, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "密　码(&P)：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(109, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "登录名(&L)：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器名称(&S)：";
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(251, 260);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(65, 29);
            this.BtnOk.TabIndex = 1;
            this.BtnOk.Text = "确定";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.No;
            this.BtnCancel.Location = new System.Drawing.Point(338, 260);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(65, 29);
            this.BtnCancel.TabIndex = 2;
            this.BtnCancel.Text = "取消";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.pictureBox2);
            this.panelControl1.Controls.Add(this.Tserver);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.BtnCancel);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.Tpassword);
            this.panelControl1.Controls.Add(this.Tuserid);
            this.panelControl1.Controls.Add(this.BtnOk);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(479, 322);
            this.panelControl1.TabIndex = 10;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox2.Location = new System.Drawing.Point(2, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(475, 81);
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // FormDataSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 322);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDataSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL MANAGE 2005";
            this.Load += new System.EventHandler(this.FormDataSetup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.TextBox Tpassword;
        private System.Windows.Forms.TextBox Tuserid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Tserver;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}