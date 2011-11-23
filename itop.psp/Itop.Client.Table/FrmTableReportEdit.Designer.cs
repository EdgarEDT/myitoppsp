namespace Itop.Client.Table
{
    partial class FrmTableReportEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.meremark2 = new DevExpress.XtraEditors.MemoEdit();
            this.spinNum1 = new DevExpress.XtraEditors.SpinEdit();
            this.meremark = new DevExpress.XtraEditors.MemoEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtyears = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNewtableanme = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.txtoldtablename = new DevExpress.XtraEditors.TextEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTableID = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label8 = new System.Windows.Forms.Label();
            this.picimage = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meremark2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNum1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meremark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtyears.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewtableanme.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtoldtablename.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picimage.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "表标识：";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.picimage);
            this.groupControl1.Controls.Add(this.label8);
            this.groupControl1.Controls.Add(this.simpleButton3);
            this.groupControl1.Controls.Add(this.meremark2);
            this.groupControl1.Controls.Add(this.spinNum1);
            this.groupControl1.Controls.Add(this.meremark);
            this.groupControl1.Controls.Add(this.label6);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.txtyears);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.txtNewtableanme);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.txtoldtablename);
            this.groupControl1.Controls.Add(this.label7);
            this.groupControl1.Controls.Add(this.label9);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.txtTableID);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(597, 397);
            this.groupControl1.TabIndex = 7;
            this.groupControl1.Text = "报表信息";
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(404, 153);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(85, 27);
            this.simpleButton3.TabIndex = 24;
            this.simpleButton3.Text = "编辑图片";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // meremark2
            // 
            this.meremark2.Location = new System.Drawing.Point(79, 189);
            this.meremark2.Name = "meremark2";
            this.meremark2.Size = new System.Drawing.Size(217, 133);
            this.meremark2.TabIndex = 22;
            // 
            // spinNum1
            // 
            this.spinNum1.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinNum1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.spinNum1.Location = new System.Drawing.Point(373, 21);
            this.spinNum1.Name = "spinNum1";
            this.spinNum1.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.spinNum1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinNum1.Properties.DisplayFormat.FormatString = "n2";
            this.spinNum1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinNum1.Properties.EditFormat.FormatString = "n0";
            this.spinNum1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinNum1.Properties.MaxValue = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.spinNum1.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinNum1.Size = new System.Drawing.Size(207, 23);
            this.spinNum1.TabIndex = 21;
            // 
            // meremark
            // 
            this.meremark.Location = new System.Drawing.Point(79, 329);
            this.meremark.Name = "meremark";
            this.meremark.Size = new System.Drawing.Size(217, 62);
            this.meremark.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 194);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "说明：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 329);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "备注：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "年份：";
            // 
            // txtyears
            // 
            this.txtyears.Location = new System.Drawing.Point(79, 124);
            this.txtyears.Name = "txtyears";
            this.txtyears.Size = new System.Drawing.Size(501, 23);
            this.txtyears.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "新表名：";
            // 
            // txtNewtableanme
            // 
            this.txtNewtableanme.Location = new System.Drawing.Point(79, 91);
            this.txtNewtableanme.Name = "txtNewtableanme";
            this.txtNewtableanme.Size = new System.Drawing.Size(501, 23);
            this.txtNewtableanme.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "原表名：";
            // 
            // txtoldtablename
            // 
            this.txtoldtablename.Enabled = false;
            this.txtoldtablename.Location = new System.Drawing.Point(79, 56);
            this.txtoldtablename.Name = "txtoldtablename";
            this.txtoldtablename.Size = new System.Drawing.Size(501, 23);
            this.txtoldtablename.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(425, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 12);
            this.label7.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(326, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "序号：";
            // 
            // txtTableID
            // 
            this.txtTableID.Location = new System.Drawing.Point(79, 21);
            this.txtTableID.Name = "txtTableID";
            this.txtTableID.Size = new System.Drawing.Size(217, 23);
            this.txtTableID.TabIndex = 5;
            // 
            // simpleButton2
            // 
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(488, 403);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 25;
            this.simpleButton2.Text = "取消";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(407, 403);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 24;
            this.simpleButton1.Text = "确定";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(77, 160);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(299, 12);
            this.label8.TabIndex = 25;
            this.label8.Text = "年份数据中间请用“#”分隔，形如“2010#2011#2020”";
            // 
            // picimage
            // 
            this.picimage.Location = new System.Drawing.Point(325, 189);
            this.picimage.Name = "picimage";
            this.picimage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.picimage.Size = new System.Drawing.Size(255, 197);
            this.picimage.TabIndex = 26;
            // 
            // FrmTableReportEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 428);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.groupControl1);
            this.Name = "FrmTableReportEdit";
            this.Text = "FrmTableReportEdit";
            this.Load += new System.EventHandler(this.FrmTableReportEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meremark2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNum1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meremark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtyears.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewtableanme.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtoldtablename.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picimage.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtNewtableanme;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtoldtablename;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.TextEdit txtTableID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit txtyears;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.MemoEdit meremark;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraEditors.MemoEdit meremark2;
        private DevExpress.XtraEditors.SpinEdit spinNum1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.PictureEdit picimage;
    }
}