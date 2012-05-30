namespace Itop.Client
{
    partial class FrmServerLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmServerLogin));
            this.utxtpwd = new Itop.Client.UserText();
            this.utxtuser = new Itop.Client.UserText();
            this.m_labelUserNumber = new System.Windows.Forms.Label();
            this.m_labelPassword = new System.Windows.Forms.Label();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // utxtpwd
            // 
            this.utxtpwd.Location = new System.Drawing.Point(81, 63);
            this.utxtpwd.Name = "utxtpwd";
            this.utxtpwd.Size = new System.Drawing.Size(161, 22);
            this.utxtpwd.TabIndex = 5;
            // 
            // utxtuser
            // 
            this.utxtuser.Location = new System.Drawing.Point(81, 29);
            this.utxtuser.Name = "utxtuser";
            this.utxtuser.Size = new System.Drawing.Size(161, 22);
            this.utxtuser.TabIndex = 4;
            // 
            // m_labelUserNumber
            // 
            this.m_labelUserNumber.AutoSize = true;
            this.m_labelUserNumber.BackColor = System.Drawing.Color.Transparent;
            this.m_labelUserNumber.ForeColor = System.Drawing.Color.MidnightBlue;
            this.m_labelUserNumber.Location = new System.Drawing.Point(33, 34);
            this.m_labelUserNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_labelUserNumber.Name = "m_labelUserNumber";
            this.m_labelUserNumber.Size = new System.Drawing.Size(43, 14);
            this.m_labelUserNumber.TabIndex = 6;
            this.m_labelUserNumber.Text = "用户名";
            // 
            // m_labelPassword
            // 
            this.m_labelPassword.AutoSize = true;
            this.m_labelPassword.BackColor = System.Drawing.Color.Transparent;
            this.m_labelPassword.ForeColor = System.Drawing.Color.MidnightBlue;
            this.m_labelPassword.Location = new System.Drawing.Point(33, 66);
            this.m_labelPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_labelPassword.Name = "m_labelPassword";
            this.m_labelPassword.Size = new System.Drawing.Size(39, 14);
            this.m_labelPassword.TabIndex = 7;
            this.m_labelPassword.Text = "密  码";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(59, 104);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 8;
            this.btnLogin.Text = "确 定";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(150, 104);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取 消";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "male.ico");
            this.imageList1.Images.SetKeyName(1, "key.ico");
            // 
            // FrmServerLogin
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 139);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.utxtpwd);
            this.Controls.Add(this.utxtuser);
            this.Controls.Add(this.m_labelUserNumber);
            this.Controls.Add(this.m_labelPassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmServerLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录城市与数据库管理";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserText utxtpwd;
        private UserText utxtuser;
        private System.Windows.Forms.Label m_labelUserNumber;
        private System.Windows.Forms.Label m_labelPassword;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.ImageList imageList1;
    }
}