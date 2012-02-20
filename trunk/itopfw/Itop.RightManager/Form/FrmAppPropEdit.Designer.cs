namespace Itop.RightManager.UI {
    partial class FrmAppPropEdit {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.tbPropName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPropValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbRemark = new System.Windows.Forms.TextBox();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "参数名";
            // 
            // tbPropName
            // 
            this.tbPropName.Location = new System.Drawing.Point(99, 31);
            this.tbPropName.Name = "tbPropName";
            this.tbPropName.Size = new System.Drawing.Size(270, 22);
            this.tbPropName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "参数值";
            // 
            // tbPropValue
            // 
            this.tbPropValue.Location = new System.Drawing.Point(99, 63);
            this.tbPropValue.Name = "tbPropValue";
            this.tbPropValue.Size = new System.Drawing.Size(270, 22);
            this.tbPropValue.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "备注";
            // 
            // tbRemark
            // 
            this.tbRemark.Location = new System.Drawing.Point(99, 94);
            this.tbRemark.Name = "tbRemark";
            this.tbRemark.Size = new System.Drawing.Size(270, 22);
            this.tbRemark.TabIndex = 2;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(294, 260);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 29);
            this.simpleButton2.TabIndex = 20;
            this.simpleButton2.Text = "取消(&C)";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(204, 260);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 29);
            this.simpleButton1.TabIndex = 19;
            this.simpleButton1.Text = "确认(&O)";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // FrmAppPropEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 320);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbRemark);
            this.Controls.Add(this.tbPropValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbPropName);
            this.Controls.Add(this.label1);
            this.Name = "FrmAppPropEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "系统参数编辑";
            this.Load += new System.EventHandler(this.FrmPersonEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPropName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPropValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbRemark;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}