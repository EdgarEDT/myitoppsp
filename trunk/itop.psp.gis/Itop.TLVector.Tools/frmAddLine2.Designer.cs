namespace ItopVector.Tools
{
    partial class frmAddLine2
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gt = new DevExpress.XtraEditors.GroupControl();
            this.gtlist = new DevExpress.XtraEditors.ListBoxControl();
            this.m1 = new DevExpress.XtraEditors.TextEdit();
            this.f1 = new DevExpress.XtraEditors.TextEdit();
            this.m2 = new DevExpress.XtraEditors.TextEdit();
            this.d1 = new DevExpress.XtraEditors.TextEdit();
            this.f2 = new DevExpress.XtraEditors.TextEdit();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.d2 = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.add = new DevExpress.XtraEditors.SimpleButton();
            this.del = new DevExpress.XtraEditors.SimpleButton();
            this.comboBox1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.dj = new DevExpress.XtraEditors.LookUpEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.mc = new DevExpress.XtraEditors.TextEdit();
            this.cd = new DevExpress.XtraEditors.TextEdit();
            this.xh = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gt)).BeginInit();
            this.gt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gtlist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.f1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.d1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.f2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.d2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBox1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dj.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xh.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gt);
            this.panelControl1.Controls.Add(this.comboBox1);
            this.panelControl1.Controls.Add(this.dj);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.simpleButton2);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.mc);
            this.panelControl1.Controls.Add(this.cd);
            this.panelControl1.Controls.Add(this.xh);
            this.panelControl1.Controls.Add(this.label5);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.label6);
            this.panelControl1.Controls.Add(this.label7);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(360, 265);
            this.panelControl1.TabIndex = 0;
            this.panelControl1.Text = "panelControl1";
            // 
            // gt
            // 
            this.gt.Controls.Add(this.gtlist);
            this.gt.Controls.Add(this.m1);
            this.gt.Controls.Add(this.f1);
            this.gt.Controls.Add(this.m2);
            this.gt.Controls.Add(this.d1);
            this.gt.Controls.Add(this.f2);
            this.gt.Controls.Add(this.label10);
            this.gt.Controls.Add(this.label9);
            this.gt.Controls.Add(this.label8);
            this.gt.Controls.Add(this.label1);
            this.gt.Controls.Add(this.d2);
            this.gt.Controls.Add(this.label2);
            this.gt.Controls.Add(this.add);
            this.gt.Controls.Add(this.del);
            this.gt.Location = new System.Drawing.Point(12, 42);
            this.gt.Name = "gt";
            this.gt.Size = new System.Drawing.Size(336, 168);
            this.gt.TabIndex = 2;
            this.gt.Text = "杆塔经纬度";
            // 
            // gtlist
            // 
            this.gtlist.Location = new System.Drawing.Point(150, 21);
            this.gtlist.Name = "gtlist";
            this.gtlist.Size = new System.Drawing.Size(180, 141);
            this.gtlist.TabIndex = 10;
            this.gtlist.DoubleClick += new System.EventHandler(this.gtlist_DoubleClick);
            // 
            // m1
            // 
            this.m1.EditValue = "00";
            this.m1.Location = new System.Drawing.Point(113, 38);
            this.m1.Name = "m1";
            this.m1.Properties.Mask.EditMask = "0##.#";
            this.m1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.m1.Properties.MaxLength = 3;
            this.m1.Size = new System.Drawing.Size(32, 21);
            this.m1.TabIndex = 3;
            this.m1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.d1_KeyUp);
            // 
            // f1
            // 
            this.f1.EditValue = "00";
            this.f1.Location = new System.Drawing.Point(79, 38);
            this.f1.Name = "f1";
            this.f1.Properties.Mask.EditMask = "0##";
            this.f1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.f1.Properties.MaxLength = 2;
            this.f1.Size = new System.Drawing.Size(32, 21);
            this.f1.TabIndex = 2;
            this.f1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.d1_KeyUp);
            // 
            // m2
            // 
            this.m2.EditValue = "00";
            this.m2.Location = new System.Drawing.Point(113, 67);
            this.m2.Name = "m2";
            this.m2.Properties.Mask.EditMask = "0##.#";
            this.m2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.m2.Properties.MaxLength = 3;
            this.m2.Size = new System.Drawing.Size(32, 21);
            this.m2.TabIndex = 7;
            this.m2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.d1_KeyUp);
            // 
            // d1
            // 
            this.d1.EditValue = "117";
            this.d1.Location = new System.Drawing.Point(44, 38);
            this.d1.Name = "d1";
            this.d1.Properties.Mask.EditMask = "###";
            this.d1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.d1.Properties.MaxLength = 3;
            this.d1.Properties.ReadOnly = true;
            this.d1.Size = new System.Drawing.Size(32, 21);
            this.d1.TabIndex = 1;
            this.d1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.d1_KeyUp);
            // 
            // f2
            // 
            this.f2.EditValue = "00";
            this.f2.Location = new System.Drawing.Point(79, 67);
            this.f2.Name = "f2";
            this.f2.Properties.Mask.EditMask = "0##";
            this.f2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.f2.Properties.MaxLength = 2;
            this.f2.Size = new System.Drawing.Size(32, 21);
            this.f2.TabIndex = 6;
            this.f2.EditValueChanged += new System.EventHandler(this.f2_EditValueChanged);
            this.f2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.d1_KeyUp);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(118, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "秒";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(85, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "分";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(50, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "度";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "经度：";
            // 
            // d2
            // 
            this.d2.EditValue = "30";
            this.d2.Location = new System.Drawing.Point(44, 67);
            this.d2.Name = "d2";
            this.d2.Properties.Mask.EditMask = "###";
            this.d2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.d2.Properties.MaxLength = 3;
            this.d2.Size = new System.Drawing.Size(32, 21);
            this.d2.TabIndex = 5;
            this.d2.EditValueChanged += new System.EventHandler(this.d2_EditValueChanged);
            this.d2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.d1_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "纬度：";
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(113, 102);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(31, 23);
            this.add.TabIndex = 8;
            this.add.Text = ">>";
            this.add.ToolTip = "添加杆塔";
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // del
            // 
            this.del.Location = new System.Drawing.Point(113, 131);
            this.del.Name = "del";
            this.del.Size = new System.Drawing.Size(31, 23);
            this.del.TabIndex = 9;
            this.del.Text = "<<";
            this.del.ToolTip = "删除杆塔";
            this.del.Click += new System.EventHandler(this.del_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.EditValue = "运行";
            this.comboBox1.Location = new System.Drawing.Point(116, 458);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBox1.Properties.Items.AddRange(new object[] {
            "运行",
            "规划"});
            this.comboBox1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBox1.Size = new System.Drawing.Size(117, 21);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.Visible = false;
            // 
            // dj
            // 
            this.dj.EditValue = "TypeName";
            this.dj.Location = new System.Drawing.Point(116, 429);
            this.dj.Name = "dj";
            this.dj.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dj.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TypeName"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Color", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ObligateField1", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
            this.dj.Properties.DisplayMember = "TypeName";
            this.dj.Properties.NullText = "";
            this.dj.Properties.ValueMember = "TypeName";
            this.dj.Size = new System.Drawing.Size(239, 21);
            this.dj.TabIndex = 8;
            this.dj.Visible = false;
            this.dj.EditValueChanged += new System.EventHandler(this.dj_EditValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 461);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "线路状态：";
            this.label3.Visible = false;
            // 
            // simpleButton2
            // 
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(285, 232);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(61, 23);
            this.simpleButton2.TabIndex = 12;
            this.simpleButton2.Text = "取消";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(218, 232);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(61, 23);
            this.simpleButton1.TabIndex = 11;
            this.simpleButton1.Text = "确定";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Location = new System.Drawing.Point(5, 216);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(348, 2);
            this.groupControl1.TabIndex = 13;
            this.groupControl1.Text = "groupControl1";
            // 
            // mc
            // 
            this.mc.Location = new System.Drawing.Point(109, 12);
            this.mc.Name = "mc";
            this.mc.Properties.Appearance.Options.UseTextOptions = true;
            this.mc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.mc.Properties.MaxLength = 20;
            this.mc.Size = new System.Drawing.Size(239, 21);
            this.mc.TabIndex = 1;
            // 
            // cd
            // 
            this.cd.Location = new System.Drawing.Point(116, 373);
            this.cd.Name = "cd";
            this.cd.Properties.Appearance.Options.UseTextOptions = true;
            this.cd.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.cd.Properties.Mask.EditMask = "#####.###";
            this.cd.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.cd.Properties.MaxLength = 9;
            this.cd.Size = new System.Drawing.Size(239, 21);
            this.cd.TabIndex = 4;
            this.cd.Visible = false;
            // 
            // xh
            // 
            this.xh.Location = new System.Drawing.Point(116, 401);
            this.xh.Name = "xh";
            this.xh.Size = new System.Drawing.Size(239, 21);
            this.xh.TabIndex = 6;
            this.xh.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 379);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "线路长度（KM）：";
            this.label5.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 435);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "电压等级：";
            this.label4.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "线路名称：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 407);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "导线型号：";
            this.label7.Visible = false;
            // 
            // frmAddLine2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 265);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmAddLine2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加线路";
            this.Load += new System.EventHandler(this.frmAddLine_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gt)).EndInit();
            this.gt.ResumeLayout(false);
            this.gt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gtlist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.f1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.d1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.f2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.d2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBox1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dj.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xh.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit d2;
        private DevExpress.XtraEditors.TextEdit d1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.GroupControl gt;
        private DevExpress.XtraEditors.ComboBoxEdit comboBox1;
        private DevExpress.XtraEditors.LookUpEdit dj;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit cd;
        private DevExpress.XtraEditors.TextEdit xh;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.ListBoxControl gtlist;
        private DevExpress.XtraEditors.SimpleButton add;
        private DevExpress.XtraEditors.SimpleButton del;
        private DevExpress.XtraEditors.TextEdit m1;
        private DevExpress.XtraEditors.TextEdit f1;
        private DevExpress.XtraEditors.TextEdit m2;
        private DevExpress.XtraEditors.TextEdit f2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.TextEdit mc;
    }
}