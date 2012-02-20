namespace Itop.RightManager.UI {
    partial class FrmPersonEdit {
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
            this.tbUserid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbExpireDate = new System.Windows.Forms.TextBox();
            this.tbLastlogon = new System.Windows.Forms.TextBox();
            this.tbDisableflg = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
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
            this.label1.Text = "用户号";
            // 
            // tbUserid
            // 
            this.tbUserid.Location = new System.Drawing.Point(99, 31);
            this.tbUserid.MaxLength = 20;
            this.tbUserid.Name = "tbUserid";
            this.tbUserid.Size = new System.Drawing.Size(270, 22);
            this.tbUserid.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "用户名";
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(99, 63);
            this.tbUserName.MaxLength = 30;
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(270, 22);
            this.tbUserName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "密码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 14);
            this.label4.TabIndex = 2;
            this.label4.Text = "过期时间";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 14);
            this.label5.TabIndex = 2;
            this.label5.Text = "最近登录";
            this.label5.Visible = false;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(99, 94);
            this.tbPassword.MaxLength = 20;
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(270, 22);
            this.tbPassword.TabIndex = 2;
            // 
            // tbExpireDate
            // 
            this.tbExpireDate.Location = new System.Drawing.Point(99, 212);
            this.tbExpireDate.Name = "tbExpireDate";
            this.tbExpireDate.Size = new System.Drawing.Size(270, 22);
            this.tbExpireDate.TabIndex = 3;
            this.tbExpireDate.Visible = false;
            // 
            // tbLastlogon
            // 
            this.tbLastlogon.Location = new System.Drawing.Point(99, 244);
            this.tbLastlogon.Name = "tbLastlogon";
            this.tbLastlogon.Size = new System.Drawing.Size(270, 22);
            this.tbLastlogon.TabIndex = 4;
            this.tbLastlogon.Visible = false;
            // 
            // tbDisableflg
            // 
            this.tbDisableflg.AutoSize = true;
            this.tbDisableflg.Location = new System.Drawing.Point(99, 161);
            this.tbDisableflg.Name = "tbDisableflg";
            this.tbDisableflg.Size = new System.Drawing.Size(15, 14);
            this.tbDisableflg.TabIndex = 5;
            this.tbDisableflg.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 161);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 14);
            this.label6.TabIndex = 2;
            this.label6.Text = "是否禁用";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 14);
            this.label7.TabIndex = 9;
            this.label7.Text = "描述";
            // 
            // tbRemark
            // 
            this.tbRemark.Location = new System.Drawing.Point(99, 126);
            this.tbRemark.MaxLength = 50;
            this.tbRemark.Name = "tbRemark";
            this.tbRemark.Size = new System.Drawing.Size(270, 22);
            this.tbRemark.TabIndex = 10;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton2.Location = new System.Drawing.Point(301, 279);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 29);
            this.simpleButton2.TabIndex = 28;
            this.simpleButton2.Text = "取消(&C)";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Location = new System.Drawing.Point(211, 279);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 29);
            this.simpleButton1.TabIndex = 27;
            this.simpleButton1.Text = "确认(&O)";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // FrmPersonEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 320);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.tbRemark);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbDisableflg);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbLastlogon);
            this.Controls.Add(this.tbExpireDate);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbUserid);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmPersonEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "用户编辑";
            this.Load += new System.EventHandler(this.FrmPersonEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbUserid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbExpireDate;
        private System.Windows.Forms.TextBox tbLastlogon;
        private System.Windows.Forms.CheckBox tbDisableflg;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbRemark;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}