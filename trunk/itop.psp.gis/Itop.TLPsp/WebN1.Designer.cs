namespace Itop.TLPsp
{
    partial class WebN1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.NSelect = new System.Windows.Forms.RadioButton();
            this.SelAll = new System.Windows.Forms.RadioButton();
            this.buttonOK = new DevComponents.DotNetBar.ButtonX();
            this.buttonN0 = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.NSelect);
            this.groupBox1.Controls.Add(this.SelAll);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox1.Location = new System.Drawing.Point(22, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 76);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "网络N-1";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // NSelect
            // 
            this.NSelect.AutoSize = true;
            this.NSelect.Location = new System.Drawing.Point(64, 43);
            this.NSelect.Name = "NSelect";
            this.NSelect.Size = new System.Drawing.Size(95, 16);
            this.NSelect.TabIndex = 1;
            this.NSelect.TabStop = true;
            this.NSelect.Text = "选择部分网络";
            this.NSelect.UseVisualStyleBackColor = true;
            this.NSelect.CheckedChanged += new System.EventHandler(this.NSelect_CheckedChanged);
            // 
            // SelAll
            // 
            this.SelAll.AutoSize = true;
            this.SelAll.Location = new System.Drawing.Point(64, 20);
            this.SelAll.Name = "SelAll";
            this.SelAll.Size = new System.Drawing.Size(71, 16);
            this.SelAll.TabIndex = 0;
            this.SelAll.TabStop = true;
            this.SelAll.Text = "选择全网";
            this.SelAll.UseVisualStyleBackColor = true;
            this.SelAll.CheckedChanged += new System.EventHandler(this.SelAll_CheckedChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.ColorScheme.DockSiteBackColorGradientAngle = 0;
            this.buttonOK.Location = new System.Drawing.Point(39, 86);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 22);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "确定";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonN0
            // 
            this.buttonN0.ColorScheme.DockSiteBackColorGradientAngle = 0;
            this.buttonN0.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonN0.Location = new System.Drawing.Point(154, 86);
            this.buttonN0.Name = "buttonN0";
            this.buttonN0.Size = new System.Drawing.Size(75, 22);
            this.buttonN0.TabIndex = 2;
            this.buttonN0.Text = "否";
            this.buttonN0.Click += new System.EventHandler(this.buttonN0_Click);
            // 
            // WebN1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(210)))), ((int)(((byte)(248)))));
            this.CancelButton = this.buttonN0;
            this.ClientSize = new System.Drawing.Size(245, 108);
            this.Controls.Add(this.buttonN0);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WebN1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "可靠性分析";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton NSelect;
        private System.Windows.Forms.RadioButton SelAll;
        private DevComponents.DotNetBar.ButtonX buttonOK;
        private DevComponents.DotNetBar.ButtonX buttonN0;
    }
}