namespace ItopVector.Tools
{
    partial class FrmSet
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
            this.txtjj = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.txtrzb = new DevExpress.XtraEditors.TextEdit();
            this.txtrl = new DevExpress.XtraEditors.TextEdit();
            this.txtnum = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtdy = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtjj.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtrzb.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtrl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtnum.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtjj
            // 
            this.txtjj.Location = new System.Drawing.Point(162, 17);
            this.txtjj.Name = "txtjj";
            this.txtjj.Properties.Mask.EditMask = "f0";
            this.txtjj.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtjj.Size = new System.Drawing.Size(73, 21);
            this.txtjj.TabIndex = 0;
            // 
            // simpleButton1
            // 
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(174, 197);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(64, 27);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "取消";
            // 
            // txtrzb
            // 
            this.txtrzb.Location = new System.Drawing.Point(162, 49);
            this.txtrzb.Name = "txtrzb";
            this.txtrzb.Size = new System.Drawing.Size(117, 21);
            this.txtrzb.TabIndex = 0;
            // 
            // txtrl
            // 
            this.txtrl.Location = new System.Drawing.Point(162, 112);
            this.txtrl.Name = "txtrl";
            this.txtrl.Size = new System.Drawing.Size(117, 21);
            this.txtrl.TabIndex = 0;
            this.txtrl.EditValueChanged += new System.EventHandler(this.txtrl_EditValueChanged);
            // 
            // txtnum
            // 
            this.txtnum.Location = new System.Drawing.Point(162, 143);
            this.txtnum.Name = "txtnum";
            this.txtnum.Properties.Mask.EditMask = "n0";
            this.txtnum.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtnum.Properties.MaxLength = 2;
            this.txtnum.Size = new System.Drawing.Size(117, 21);
            this.txtnum.TabIndex = 0;
            this.txtnum.EditValueChanged += new System.EventHandler(this.txtnum_EditValueChanged);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(103, 197);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(64, 27);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = "确定";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "变电站最小间距";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "容载比";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "电压等级";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 14);
            this.label4.TabIndex = 2;
            this.label4.Text = "变电容量";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 14);
            this.label5.TabIndex = 2;
            this.label5.Text = "新建变电站数量";
            // 
            // txtdy
            // 
            this.txtdy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtdy.FormattingEnabled = true;
            this.txtdy.Items.AddRange(new object[] {
            "500",
            "220",
            "110",
            "35"});
            this.txtdy.Location = new System.Drawing.Point(162, 82);
            this.txtdy.Name = "txtdy";
            this.txtdy.Size = new System.Drawing.Size(89, 22);
            this.txtdy.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(255, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 14);
            this.label6.TabIndex = 4;
            this.label6.Text = "kV";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(240, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 14);
            this.label7.TabIndex = 2;
            this.label7.Text = "（KM）";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(15, 180);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 14);
            this.label8.TabIndex = 2;
            // 
            // FrmSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 238);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtdy);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.txtnum);
            this.Controls.Add(this.txtrl);
            this.Controls.Add(this.txtrzb);
            this.Controls.Add(this.txtjj);
            this.Name = "FrmSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数设置";
            this.Load += new System.EventHandler(this.FrmSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtjj.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtrzb.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtrl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtnum.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtjj;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.TextEdit txtrzb;
        private DevExpress.XtraEditors.TextEdit txtrl;
        private DevExpress.XtraEditors.TextEdit txtnum;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox txtdy;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}