namespace Itop.TLPSP.DEVICE
{
    partial class PartRelform
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
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.Selbutt = new DevComponents.DotNetBar.ButtonX();
            this.comboV = new System.Windows.Forms.ComboBox();
            this.DefineDelete = new System.Windows.Forms.RadioButton();
            this.SelectVtype = new System.Windows.Forms.RadioButton();
            this.Compubton = new DevComponents.DotNetBar.ButtonX();
            this.Cancelbton = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Desktop;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx1.Controls.Add(this.Selbutt);
            this.panelEx1.Controls.Add(this.comboV);
            this.panelEx1.Controls.Add(this.DefineDelete);
            this.panelEx1.Controls.Add(this.SelectVtype);
            this.panelEx1.Location = new System.Drawing.Point(12, 12);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(268, 186);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // Selbutt
            // 
            this.Selbutt.ColorScheme.DockSiteBackColorGradientAngle = 0;
            this.Selbutt.Enabled = false;
            this.Selbutt.Location = new System.Drawing.Point(156, 96);
            this.Selbutt.Name = "Selbutt";
            this.Selbutt.Size = new System.Drawing.Size(75, 23);
            this.Selbutt.TabIndex = 5;
            this.Selbutt.Text = "选择";
            this.Selbutt.Click += new System.EventHandler(this.Selbutt_Click);
            // 
            // comboV
            // 
            this.comboV.Enabled = false;
            this.comboV.FormattingEnabled = true;
            this.comboV.Items.AddRange(new object[] {
            "500KV",
            "220KV",
            "110KV",
            "66KV",
            "35KV",
            "15.75KV",
            "10KV"});
            this.comboV.Location = new System.Drawing.Point(140, 39);
            this.comboV.Name = "comboV";
            this.comboV.Size = new System.Drawing.Size(91, 20);
            this.comboV.TabIndex = 3;
            // 
            // DefineDelete
            // 
            this.DefineDelete.AutoSize = true;
            this.DefineDelete.Location = new System.Drawing.Point(15, 96);
            this.DefineDelete.Name = "DefineDelete";
            this.DefineDelete.Size = new System.Drawing.Size(107, 16);
            this.DefineDelete.TabIndex = 2;
            this.DefineDelete.TabStop = true;
            this.DefineDelete.Text = "自定义删除方案";
            this.DefineDelete.UseVisualStyleBackColor = true;
            this.DefineDelete.CheckedChanged += new System.EventHandler(this.DefineDelete_CheckedChanged);
            // 
            // SelectVtype
            // 
            this.SelectVtype.AutoSize = true;
            this.SelectVtype.Location = new System.Drawing.Point(15, 43);
            this.SelectVtype.Name = "SelectVtype";
            this.SelectVtype.Size = new System.Drawing.Size(101, 16);
            this.SelectVtype.TabIndex = 1;
            this.SelectVtype.TabStop = true;
            this.SelectVtype.Text = "某电压等级N-1";
            this.SelectVtype.UseVisualStyleBackColor = true;
            this.SelectVtype.CheckedChanged += new System.EventHandler(this.SelectVtype_CheckedChanged);
            // 
            // Compubton
            // 
            this.Compubton.ColorScheme.DockSiteBackColorGradientAngle = 0;
            this.Compubton.Location = new System.Drawing.Point(27, 217);
            this.Compubton.Name = "Compubton";
            this.Compubton.Size = new System.Drawing.Size(75, 23);
            this.Compubton.TabIndex = 1;
            this.Compubton.Text = "计算";
            this.Compubton.Click += new System.EventHandler(this.Compubton_Click);
            // 
            // Cancelbton
            // 
            this.Cancelbton.ColorScheme.DockSiteBackColorGradientAngle = 0;
            this.Cancelbton.Location = new System.Drawing.Point(196, 216);
            this.Cancelbton.Name = "Cancelbton";
            this.Cancelbton.Size = new System.Drawing.Size(75, 23);
            this.Cancelbton.TabIndex = 2;
            this.Cancelbton.Text = "取消";
            this.Cancelbton.Click += new System.EventHandler(this.Cancelbton_Click);
            // 
            // PartRelform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(210)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(292, 249);
            this.Controls.Add(this.Cancelbton);
            this.Controls.Add(this.Compubton);
            this.Controls.Add(this.panelEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PartRelform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "部分线路选择方案";
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX Compubton;
        private DevComponents.DotNetBar.ButtonX Cancelbton;
        private System.Windows.Forms.ComboBox comboV;
        private System.Windows.Forms.RadioButton DefineDelete;
        private System.Windows.Forms.RadioButton SelectVtype;
        private DevComponents.DotNetBar.ButtonX Selbutt;
    }
}