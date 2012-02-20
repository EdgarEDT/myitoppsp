namespace Itop.TLPsp
{
    partial class frmDuanlu
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxEdit2 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEdit3 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.hScrollBar2 = new DevExpress.XtraEditors.HScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEdit4 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit4.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "短路类型";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "距首节点距离";
            this.label3.VisibleChanged += new System.EventHandler(this.visualChanged2);
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 223);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "电抗方式";
            this.label4.Visible = false;
            // 
            // comboBoxEdit2
            // 
            this.comboBoxEdit2.EditValue = "大方式电抗";
            this.comboBoxEdit2.Location = new System.Drawing.Point(126, 179);
            this.comboBoxEdit2.Name = "comboBoxEdit2";
            this.comboBoxEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit2.Properties.Items.AddRange(new object[] {
            "大方式电抗",
            "小方式电抗"});
            this.comboBoxEdit2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit2.Size = new System.Drawing.Size(140, 21);
            this.comboBoxEdit2.TabIndex = 5;
            this.comboBoxEdit2.Visible = false;
            // 
            // comboBoxEdit3
            // 
            this.comboBoxEdit3.EditValue = "";
            this.comboBoxEdit3.Location = new System.Drawing.Point(126, 61);
            this.comboBoxEdit3.Name = "comboBoxEdit3";
            this.comboBoxEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit3.Properties.Items.AddRange(new object[] {
            "单相接地",
            "两相接地",
            "两相故障",
            "三相故障"});
            this.comboBoxEdit3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit3.Size = new System.Drawing.Size(140, 21);
            this.comboBoxEdit3.TabIndex = 6;
            // 
            // hScrollBar2
            // 
            this.hScrollBar2.Location = new System.Drawing.Point(108, 56);
            this.hScrollBar2.Name = "hScrollBar2";
            this.hScrollBar2.Size = new System.Drawing.Size(8, 8);
            this.hScrollBar2.TabIndex = 10;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(126, 17);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(140, 23);
            this.hScrollBar1.TabIndex = 12;
            this.hScrollBar1.VisibleChanged += new System.EventHandler(this.visualChanged);
            this.hScrollBar1.ValueChanged += new System.EventHandler(this.hScrollBar1_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(112, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "0";
            this.label5.VisibleChanged += new System.EventHandler(this.visualChanged2);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "图形输出设置";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "结果报表显示";
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(126, 146);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Properties.Items.AddRange(new object[] {
            "是",
            "否"});
            this.comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit1.Size = new System.Drawing.Size(140, 21);
            this.comboBoxEdit1.TabIndex = 17;
            // 
            // comboBoxEdit4
            // 
            this.comboBoxEdit4.Location = new System.Drawing.Point(126, 102);
            this.comboBoxEdit4.Name = "comboBoxEdit4";
            this.comboBoxEdit4.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit4.Properties.Items.AddRange(new object[] {
            "图形输出节点电压",
            "图形输出短路电流",
            "无标注"});
            this.comboBoxEdit4.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit4.Size = new System.Drawing.Size(140, 21);
            this.comboBoxEdit4.TabIndex = 18;
            // 
            // simpleButton2
            // 
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(176, 206);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 29);
            this.simpleButton2.TabIndex = 22;
            this.simpleButton2.Text = "取消(&C)";
            // 
            // simpleButton1
            // 
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.simpleButton1.Location = new System.Drawing.Point(86, 206);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 29);
            this.simpleButton1.TabIndex = 21;
            this.simpleButton1.Text = "确认(&O)";
            // 
            // frmDuanlu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(292, 254);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.comboBoxEdit4);
            this.Controls.Add(this.comboBoxEdit1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.hScrollBar2);
            this.Controls.Add(this.comboBoxEdit3);
            this.Controls.Add(this.comboBoxEdit2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmDuanlu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "短路计算";
            this.Load += new System.EventHandler(this.frmProperty_Load);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit4.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit2;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit3;
        private DevExpress.XtraEditors.HScrollBar hScrollBar2;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit4;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}