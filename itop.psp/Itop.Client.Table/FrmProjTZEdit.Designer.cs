namespace Itop.Client.Table
{
    partial class FrmProjTZEdit
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
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.label13 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.sptz1 = new DevExpress.XtraEditors.SpinEdit();
            this.sptz2 = new DevExpress.XtraEditors.SpinEdit();
            this.sptz3 = new DevExpress.XtraEditors.SpinEdit();
            this.sptz4 = new DevExpress.XtraEditors.SpinEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.sptz0 = new DevExpress.XtraEditors.SpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.sptz1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sptz2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sptz3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sptz4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sptz0.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton2
            // 
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(230, 197);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Text = "取消";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(133, 197);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "确定";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(21, 59);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(149, 12);
            this.label13.TabIndex = 25;
            this.label13.Text = "可行性研究项目投资金额：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 12);
            this.label1.TabIndex = 25;
            this.label1.Text = "初步设计项目投资金额：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 12);
            this.label4.TabIndex = 25;
            this.label4.Text = "施工设计项目投资金额：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(57, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "移交项目投资金额：";
            // 
            // sptz1
            // 
            this.sptz1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.sptz1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.sptz1.Location = new System.Drawing.Point(176, 54);
            this.sptz1.Name = "sptz1";
            this.sptz1.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.sptz1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.sptz1.Properties.DisplayFormat.FormatString = "n2";
            this.sptz1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.sptz1.Properties.EditFormat.FormatString = "n2";
            this.sptz1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.sptz1.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.sptz1.Properties.MaxValue = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.sptz1.Size = new System.Drawing.Size(129, 23);
            this.sptz1.TabIndex = 33;
            // 
            // sptz2
            // 
            this.sptz2.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.sptz2.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.sptz2.Location = new System.Drawing.Point(176, 84);
            this.sptz2.Name = "sptz2";
            this.sptz2.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.sptz2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.sptz2.Properties.DisplayFormat.FormatString = "n2";
            this.sptz2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.sptz2.Properties.EditFormat.FormatString = "n2";
            this.sptz2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.sptz2.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.sptz2.Properties.MaxValue = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.sptz2.Size = new System.Drawing.Size(129, 23);
            this.sptz2.TabIndex = 33;
            // 
            // sptz3
            // 
            this.sptz3.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.sptz3.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.sptz3.Location = new System.Drawing.Point(176, 119);
            this.sptz3.Name = "sptz3";
            this.sptz3.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.sptz3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.sptz3.Properties.DisplayFormat.FormatString = "n2";
            this.sptz3.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.sptz3.Properties.EditFormat.FormatString = "n2";
            this.sptz3.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.sptz3.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.sptz3.Properties.MaxValue = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.sptz3.Size = new System.Drawing.Size(129, 23);
            this.sptz3.TabIndex = 33;
            // 
            // sptz4
            // 
            this.sptz4.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.sptz4.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.sptz4.Location = new System.Drawing.Point(176, 149);
            this.sptz4.Name = "sptz4";
            this.sptz4.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.sptz4.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.sptz4.Properties.DisplayFormat.FormatString = "n2";
            this.sptz4.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.sptz4.Properties.EditFormat.FormatString = "n2";
            this.sptz4.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.sptz4.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.sptz4.Properties.MaxValue = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.sptz4.Size = new System.Drawing.Size(129, 23);
            this.sptz4.TabIndex = 33;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 25;
            this.label2.Text = "规划审查项目投资金额：";
            // 
            // sptz0
            // 
            this.sptz0.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.sptz0.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.sptz0.Location = new System.Drawing.Point(176, 23);
            this.sptz0.Name = "sptz0";
            this.sptz0.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.sptz0.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.sptz0.Properties.DisplayFormat.FormatString = "n2";
            this.sptz0.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.sptz0.Properties.EditFormat.FormatString = "n2";
            this.sptz0.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.sptz0.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.sptz0.Properties.MaxValue = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.sptz0.Size = new System.Drawing.Size(129, 23);
            this.sptz0.TabIndex = 33;
            // 
            // FrmProjTZEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 232);
            this.Controls.Add(this.sptz4);
            this.Controls.Add(this.sptz3);
            this.Controls.Add(this.sptz2);
            this.Controls.Add(this.sptz0);
            this.Controls.Add(this.sptz1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProjTZEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "投资编缉";
            this.Load += new System.EventHandler(this.FrmProjTZEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sptz1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sptz2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sptz3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sptz4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sptz0.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.SpinEdit sptz1;
        private DevExpress.XtraEditors.SpinEdit sptz2;
        private DevExpress.XtraEditors.SpinEdit sptz3;
        private DevExpress.XtraEditors.SpinEdit sptz4;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SpinEdit sptz0;
    }
}