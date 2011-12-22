namespace Itop.TLPSP.DEVICE
{
    partial class RelFormdialog
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
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // buttonX1
            // 
            this.buttonX1.ColorScheme.DockSiteBackColorGradientAngle = 0;
            this.buttonX1.Location = new System.Drawing.Point(17, 25);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(103, 23);
            this.buttonX1.TabIndex = 0;
            this.buttonX1.Text = "线路可靠性";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.ColorScheme.DockSiteBackColorGradientAngle = 0;
            this.buttonX2.Location = new System.Drawing.Point(17, 78);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(103, 23);
            this.buttonX2.TabIndex = 1;
            this.buttonX2.Text = "变压器可靠性";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // buttonX3
            // 
            this.buttonX3.ColorScheme.DockSiteBackColorGradientAngle = 0;
            this.buttonX3.Location = new System.Drawing.Point(17, 122);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(103, 23);
            this.buttonX3.TabIndex = 2;
            this.buttonX3.Text = "供电可靠性";
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
            // 
            // RelFormdialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(136, 170);
            this.Controls.Add(this.buttonX3);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RelFormdialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "可靠性分析";
            this.Load += new System.EventHandler(this.RelFormdialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.ButtonX buttonX3;
    }
}