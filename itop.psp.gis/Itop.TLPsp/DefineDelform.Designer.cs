namespace Itop.TLPsp
{
    partial class DefineDelform
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
            this.Findbutn = new DevComponents.DotNetBar.ButtonX();
            this.NodeNumfind = new DevComponents.DotNetBar.ButtonItem();
            this.NodeName = new DevComponents.DotNetBar.ButtonItem();
            this.label2 = new System.Windows.Forms.Label();
            this.Nodepanel = new System.Windows.Forms.Panel();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.LineNamepanel = new System.Windows.Forms.Panel();
            this.LinenametextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.WaitLinelist = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Selectlinelist = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Addbutn = new DevComponents.DotNetBar.ButtonX();
            this.OKbutn = new DevComponents.DotNetBar.ButtonX();
            this.Delbutn = new DevComponents.DotNetBar.ButtonX();
            this.Nodepanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            this.LineNamepanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Findbutn
            // 
            this.Findbutn.ColorScheme.DockSiteBackColorGradientAngle = 0;
            this.Findbutn.Location = new System.Drawing.Point(28, 22);
            this.Findbutn.Name = "Findbutn";
            this.Findbutn.Size = new System.Drawing.Size(75, 23);
            this.Findbutn.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.NodeNumfind,
            this.NodeName});
            this.Findbutn.TabIndex = 2;
            this.Findbutn.Text = "查找";
            // 
            // NodeNumfind
            // 
            this.NodeNumfind.Name = "NodeNumfind";
            this.NodeNumfind.Text = "通过节点名称查找";
            this.NodeNumfind.DoubleClick += new System.EventHandler(this.NodeNumfind_DoubleClick);
            this.NodeNumfind.Click += new System.EventHandler(this.NodeNumfind_Click);
            // 
            // NodeName
            // 
            this.NodeName.Name = "NodeName";
            this.NodeName.Text = "线路名查找";
            this.NodeName.Click += new System.EventHandler(this.NodeName_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "节点名称：";
            // 
            // Nodepanel
            // 
            this.Nodepanel.Controls.Add(this.comboBoxEdit1);
            this.Nodepanel.Controls.Add(this.label2);
            this.Nodepanel.Location = new System.Drawing.Point(109, 10);
            this.Nodepanel.Name = "Nodepanel";
            this.Nodepanel.Size = new System.Drawing.Size(231, 36);
            this.Nodepanel.TabIndex = 7;
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(57, 6);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Properties.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxEdit1_Properties_KeyPress);
            this.comboBoxEdit1.Size = new System.Drawing.Size(174, 23);
            this.comboBoxEdit1.TabIndex = 36;
            // 
            // LineNamepanel
            // 
            this.LineNamepanel.Controls.Add(this.LinenametextBox);
            this.LineNamepanel.Controls.Add(this.label4);
            this.LineNamepanel.Location = new System.Drawing.Point(109, 10);
            this.LineNamepanel.Name = "LineNamepanel";
            this.LineNamepanel.Size = new System.Drawing.Size(231, 37);
            this.LineNamepanel.TabIndex = 7;
            this.LineNamepanel.Visible = false;
            // 
            // LinenametextBox
            // 
            this.LinenametextBox.Location = new System.Drawing.Point(54, 14);
            this.LinenametextBox.Name = "LinenametextBox";
            this.LinenametextBox.Size = new System.Drawing.Size(156, 21);
            this.LinenametextBox.TabIndex = 9;
            this.LinenametextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LinenametextBox_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "线路名";
            // 
            // WaitLinelist
            // 
            this.WaitLinelist.FormattingEnabled = true;
            this.WaitLinelist.ItemHeight = 12;
            this.WaitLinelist.Location = new System.Drawing.Point(12, 89);
            this.WaitLinelist.Name = "WaitLinelist";
            this.WaitLinelist.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.WaitLinelist.Size = new System.Drawing.Size(120, 148);
            this.WaitLinelist.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "待选线路";
            // 
            // Selectlinelist
            // 
            this.Selectlinelist.FormattingEnabled = true;
            this.Selectlinelist.ItemHeight = 12;
            this.Selectlinelist.Location = new System.Drawing.Point(235, 89);
            this.Selectlinelist.Name = "Selectlinelist";
            this.Selectlinelist.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.Selectlinelist.Size = new System.Drawing.Size(120, 148);
            this.Selectlinelist.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(233, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "已选线路";
            // 
            // Addbutn
            // 
            this.Addbutn.ColorScheme.DockSiteBackColorGradientAngle = 0;
            this.Addbutn.Location = new System.Drawing.Point(150, 89);
            this.Addbutn.Name = "Addbutn";
            this.Addbutn.Size = new System.Drawing.Size(52, 23);
            this.Addbutn.TabIndex = 12;
            this.Addbutn.Text = ">>>>";
            this.Addbutn.Click += new System.EventHandler(this.Addbutn_Click);
            // 
            // OKbutn
            // 
            this.OKbutn.ColorScheme.DockSiteBackColorGradientAngle = 0;
            this.OKbutn.Location = new System.Drawing.Point(150, 214);
            this.OKbutn.Name = "OKbutn";
            this.OKbutn.Size = new System.Drawing.Size(52, 23);
            this.OKbutn.TabIndex = 13;
            this.OKbutn.Text = "OK";
            this.OKbutn.Click += new System.EventHandler(this.OKbutn_Click);
            // 
            // Delbutn
            // 
            this.Delbutn.ColorScheme.DockSiteBackColorGradientAngle = 0;
            this.Delbutn.Location = new System.Drawing.Point(150, 157);
            this.Delbutn.Name = "Delbutn";
            this.Delbutn.Size = new System.Drawing.Size(52, 23);
            this.Delbutn.TabIndex = 14;
            this.Delbutn.Text = "<<<<";
            this.Delbutn.Click += new System.EventHandler(this.Delbutn_Click);
            // 
            // DefineDelform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(210)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(369, 255);
            this.Controls.Add(this.Delbutn);
            this.Controls.Add(this.OKbutn);
            this.Controls.Add(this.LineNamepanel);
            this.Controls.Add(this.Addbutn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Selectlinelist);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.WaitLinelist);
            this.Controls.Add(this.Nodepanel);
            this.Controls.Add(this.Findbutn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DefineDelform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自定义删除线路";
            this.Load += new System.EventHandler(this.DefineDelform_Load);
            this.Nodepanel.ResumeLayout(false);
            this.Nodepanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            this.LineNamepanel.ResumeLayout(false);
            this.LineNamepanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX Findbutn;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.ButtonItem NodeNumfind;
        private DevComponents.DotNetBar.ButtonItem NodeName;
        private System.Windows.Forms.Panel Nodepanel;
        private System.Windows.Forms.Panel LineNamepanel;
        private System.Windows.Forms.TextBox LinenametextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox WaitLinelist;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox Selectlinelist;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.ButtonX Addbutn;
        private DevComponents.DotNetBar.ButtonX OKbutn;
        private DevComponents.DotNetBar.ButtonX Delbutn;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
    }
}