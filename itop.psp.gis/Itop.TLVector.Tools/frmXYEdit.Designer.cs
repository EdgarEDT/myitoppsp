namespace ItopVector.Tools
{
    partial class frmXYEdit
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
            this.gt = new DevExpress.XtraEditors.GroupControl();
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
            this.ok = new DevExpress.XtraEditors.SimpleButton();
            this.cel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gt)).BeginInit();
            this.gt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.f1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.d1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.f2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.d2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gt
            // 
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
            this.gt.Controls.Add(this.ok);
            this.gt.Controls.Add(this.cel);
            this.gt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gt.Location = new System.Drawing.Point(0, 0);
            this.gt.Name = "gt";
            this.gt.Size = new System.Drawing.Size(156, 134);
            this.gt.TabIndex = 0;
            // 
            // m1
            // 
            this.m1.EditValue = "00";
            this.m1.Location = new System.Drawing.Point(113, 38);
            this.m1.Name = "m1";
            this.m1.Properties.Mask.EditMask = "##.#";
            this.m1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.m1.Properties.MaxLength = 3;
            this.m1.Size = new System.Drawing.Size(32, 23);
            this.m1.TabIndex = 3;
            // 
            // f1
            // 
            this.f1.EditValue = "00";
            this.f1.Location = new System.Drawing.Point(79, 38);
            this.f1.Name = "f1";
            this.f1.Properties.Mask.EditMask = "##";
            this.f1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.f1.Properties.MaxLength = 2;
            this.f1.Size = new System.Drawing.Size(32, 23);
            this.f1.TabIndex = 2;
            // 
            // m2
            // 
            this.m2.EditValue = "00";
            this.m2.Location = new System.Drawing.Point(113, 67);
            this.m2.Name = "m2";
            this.m2.Properties.Mask.EditMask = "##.#";
            this.m2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.m2.Properties.MaxLength = 3;
            this.m2.Size = new System.Drawing.Size(32, 23);
            this.m2.TabIndex = 7;
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
            this.d1.Size = new System.Drawing.Size(32, 23);
            this.d1.TabIndex = 1;
            // 
            // f2
            // 
            this.f2.EditValue = "00";
            this.f2.Location = new System.Drawing.Point(79, 67);
            this.f2.Name = "f2";
            this.f2.Properties.Mask.EditMask = "##";
            this.f2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.f2.Properties.MaxLength = 2;
            this.f2.Size = new System.Drawing.Size(32, 23);
            this.f2.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(118, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 12;
            this.label10.Text = "秒";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(85, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "分";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(50, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 10;
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
            this.d2.Size = new System.Drawing.Size(32, 23);
            this.d2.TabIndex = 5;
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
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(54, 102);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(44, 23);
            this.ok.TabIndex = 8;
            this.ok.Text = "确定";
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // cel
            // 
            this.cel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cel.Location = new System.Drawing.Point(101, 102);
            this.cel.Name = "cel";
            this.cel.Size = new System.Drawing.Size(44, 23);
            this.cel.TabIndex = 9;
            this.cel.Text = "取消";
            // 
            // frmXYEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(156, 134);
            this.Controls.Add(this.gt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmXYEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改经纬度";
            ((System.ComponentModel.ISupportInitialize)(this.gt)).EndInit();
            this.gt.ResumeLayout(false);
            this.gt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.f1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.d1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.f2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.d2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gt;
        private DevExpress.XtraEditors.TextEdit m1;
        private DevExpress.XtraEditors.TextEdit f1;
        private DevExpress.XtraEditors.TextEdit m2;
        private DevExpress.XtraEditors.TextEdit d1;
        private DevExpress.XtraEditors.TextEdit f2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit d2;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton ok;
        private DevExpress.XtraEditors.SimpleButton cel;

    }
}