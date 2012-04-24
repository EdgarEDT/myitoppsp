namespace Itop.TLPSP.DEVICE
{
    partial class PDreltypefrmedit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.label2 = new System.Windows.Forms.Label();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.comboBoxEdit2 = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(111, 42);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(156, 21);
            this.comboBoxEdit1.TabIndex = 4;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(72, 138);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(87, 27);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "确定";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.comboBoxEdit2);
            this.groupControl1.Controls.Add(this.comboBoxEdit1);
            this.groupControl1.Controls.Add(this.simpleButton2);
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.textEdit1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(309, 170);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "类型";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "电源名称：";
            // 
            // simpleButton2
            // 
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(190, 138);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(87, 27);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Text = "取消";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "项目名称：";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(111, 42);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(156, 21);
            this.textEdit1.TabIndex = 0;
            // 
            // comboBoxEdit2
            // 
            this.comboBoxEdit2.Location = new System.Drawing.Point(111, 73);
            this.comboBoxEdit2.Name = "comboBoxEdit2";
            this.comboBoxEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit2.Properties.Click += new System.EventHandler(this.comboBoxEdit2_Properties_Click);
            this.comboBoxEdit2.Size = new System.Drawing.Size(156, 21);
            this.comboBoxEdit2.TabIndex = 4;
            // 
            // PDreltypefrmedit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 170);
            this.Controls.Add(this.groupControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PDreltypefrmedit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配电可靠性项目";
            this.Load += new System.EventHandler(this.PDtypefrmedit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit2;
    }
}