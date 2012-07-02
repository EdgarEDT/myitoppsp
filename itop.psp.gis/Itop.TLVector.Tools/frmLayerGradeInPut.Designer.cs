namespace ItopVector.Tools
{
    partial class frmLayerGradeInPut
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCancle = new DevExpress.XtraEditors.SimpleButton();
            this.pi = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pi.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(86, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(177, 22);
            this.textBox1.TabIndex = 1;
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Location = new System.Drawing.Point(118, 131);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(65, 27);
            this.simpleButtonOK.TabIndex = 2;
            this.simpleButtonOK.Text = "确定";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // simpleButtonCancle
            // 
            this.simpleButtonCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButtonCancle.Location = new System.Drawing.Point(198, 131);
            this.simpleButtonCancle.Name = "simpleButtonCancle";
            this.simpleButtonCancle.Size = new System.Drawing.Size(65, 27);
            this.simpleButtonCancle.TabIndex = 3;
            this.simpleButtonCancle.Text = "取消";
            // 
            // pi
            // 
            this.pi.EditValue = "SUID";
            this.pi.Location = new System.Drawing.Point(86, 68);
            this.pi.Name = "pi";
            this.pi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.pi.Properties.CaseSensitiveSearch = true;
            this.pi.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SUID", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.pi.Properties.DisplayMember = "Name";
            this.pi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.pi.Properties.ValueMember = "SUID";
            this.pi.Size = new System.Drawing.Size(178, 21);
            this.pi.TabIndex = 6;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(20, 39);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "分级名称：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(20, 71);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "所属分级：";
            // 
            // frmLayerGradeInPut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 167);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.pi);
            this.Controls.Add(this.simpleButtonCancle);
            this.Controls.Add(this.simpleButtonOK);
            this.Controls.Add(this.textBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLayerGradeInPut";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "分级管理";
            ((System.ComponentModel.ISupportInitialize)(this.pi.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancle;
        private DevExpress.XtraEditors.LookUpEdit pi;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}