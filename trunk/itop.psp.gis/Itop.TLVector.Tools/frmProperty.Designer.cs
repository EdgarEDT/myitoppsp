namespace ItopVector.Tools
{
    partial class frmProperty
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
            this.xyxs = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lx = new DevExpress.XtraEditors.LookUpEdit();
            this.remark = new DevExpress.XtraEditors.MemoEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.dl = new DevExpress.XtraEditors.TextEdit();
            this.fh = new DevExpress.XtraEditors.TextEdit();
            this.mj = new DevExpress.XtraEditors.TextEdit();
            this.bh = new DevExpress.XtraEditors.TextEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xyxs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lx.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.remark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mj.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bh.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.xyxs);
            this.panelControl1.Controls.Add(this.lx);
            this.panelControl1.Controls.Add(this.remark);
            this.panelControl1.Controls.Add(this.simpleButton2);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.dl);
            this.panelControl1.Controls.Add(this.fh);
            this.panelControl1.Controls.Add(this.mj);
            this.panelControl1.Controls.Add(this.bh);
            this.panelControl1.Controls.Add(this.label6);
            this.panelControl1.Controls.Add(this.label7);
            this.panelControl1.Controls.Add(this.label5);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(423, 304);
            this.panelControl1.TabIndex = 0;
            // 
            // xyxs
            // 
            this.xyxs.EditValue = "是";
            this.xyxs.Location = new System.Drawing.Point(112, 76);
            this.xyxs.Name = "xyxs";
            this.xyxs.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.xyxs.Properties.Items.AddRange(new object[] {
            "是",
            "否"});
            this.xyxs.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.xyxs.Size = new System.Drawing.Size(117, 21);
            this.xyxs.TabIndex = 15;
            this.xyxs.SelectedIndexChanged += new System.EventHandler(this.xs_SelectedIndexChanged);
            // 
            // lx
            // 
            this.lx.Location = new System.Drawing.Point(112, 43);
            this.lx.Name = "lx";
            this.lx.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lx.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("UID", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TypeName", "土地类型"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TypeStyle", "负荷密度"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ObligateField1", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ObligateField2", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ObligateField3", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lx.Properties.DisplayMember = "TypeName";
            this.lx.Properties.NullText = "";
            this.lx.Properties.ValueMember = "UID";
            this.lx.Size = new System.Drawing.Size(274, 21);
            this.lx.TabIndex = 3;
            this.lx.EditValueChanged += new System.EventHandler(this.lx_EditValueChanged);
            // 
            // remark
            // 
            this.remark.Location = new System.Drawing.Point(112, 174);
            this.remark.Name = "remark";
            this.remark.Size = new System.Drawing.Size(273, 69);
            this.remark.TabIndex = 9;
            // 
            // simpleButton2
            // 
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(331, 267);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(55, 27);
            this.simpleButton2.TabIndex = 11;
            this.simpleButton2.Text = "取消";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(265, 267);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(55, 27);
            this.simpleButton1.TabIndex = 10;
            this.simpleButton1.Text = "确定";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Location = new System.Drawing.Point(8, 193);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(386, 2);
            this.groupControl1.TabIndex = 12;
            this.groupControl1.Text = "groupControl1";
            // 
            // dl
            // 
            this.dl.Location = new System.Drawing.Point(94, 172);
            this.dl.Name = "dl";
            this.dl.Properties.Appearance.Options.UseTextOptions = true;
            this.dl.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.dl.Properties.DisplayFormat.FormatString = "####.##";
            this.dl.Properties.EditFormat.FormatString = "####.##";
            this.dl.Properties.Mask.EditMask = "n";
            this.dl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.dl.Properties.MaxLength = 50;
            this.dl.Size = new System.Drawing.Size(290, 21);
            this.dl.TabIndex = 14;
            this.dl.Visible = false;
            // 
            // fh
            // 
            this.fh.Location = new System.Drawing.Point(112, 140);
            this.fh.Name = "fh";
            this.fh.Properties.Appearance.Options.UseTextOptions = true;
            this.fh.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.fh.Properties.DisplayFormat.FormatString = "####.##";
            this.fh.Properties.EditFormat.FormatString = "####.##";
            this.fh.Properties.Mask.EditMask = "#####0.###";
            this.fh.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.fh.Properties.MaxLength = 10;
            this.fh.Size = new System.Drawing.Size(273, 21);
            this.fh.TabIndex = 7;
            // 
            // mj
            // 
            this.mj.Location = new System.Drawing.Point(112, 107);
            this.mj.Name = "mj";
            this.mj.Properties.Appearance.Options.UseTextOptions = true;
            this.mj.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.mj.Properties.DisplayFormat.FormatString = "####";
            this.mj.Properties.Mask.EditMask = "#####0.###";
            this.mj.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.mj.Properties.MaxLength = 9;
            this.mj.Size = new System.Drawing.Size(273, 21);
            this.mj.TabIndex = 5;
            // 
            // bh
            // 
            this.bh.Location = new System.Drawing.Point(112, 9);
            this.bh.Name = "bh";
            this.bh.Properties.MaxLength = 15;
            this.bh.Size = new System.Drawing.Size(273, 21);
            this.bh.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 176);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 14);
            this.label6.TabIndex = 8;
            this.label6.Text = "备注：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 14);
            this.label7.TabIndex = 2;
            this.label7.Text = "使用需用系数：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 14);
            this.label5.TabIndex = 2;
            this.label5.Text = "类型：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "负荷(MW)：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "编号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 14);
            this.label2.TabIndex = 13;
            this.label2.Text = "容量(MVA)：";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "面积(KM²)：";
            // 
            // frmProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 304);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProperty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地块属性";
            this.Load += new System.EventHandler(this.frmProperty_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xyxs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lx.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.remark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mj.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bh.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LookUpEdit lx;
        private DevExpress.XtraEditors.MemoEdit remark;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit dl;
        private DevExpress.XtraEditors.TextEdit fh;
        private DevExpress.XtraEditors.TextEdit mj;
        private DevExpress.XtraEditors.TextEdit bh;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.ComboBoxEdit xyxs;
        private System.Windows.Forms.Label label7;

    }
}