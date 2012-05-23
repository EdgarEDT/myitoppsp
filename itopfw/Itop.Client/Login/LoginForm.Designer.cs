			
namespace Itop.Client.Login {
    partial class LoginForm {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.panel2 = new System.Windows.Forms.Panel();
            this.ubclose = new Itop.Client.UserBar();
            this.ubmin = new Itop.Client.UserBar();
            this.utxtpwd = new Itop.Client.UserText();
            this.utxtuser = new Itop.Client.UserText();
            this.labtop = new DevExpress.XtraEditors.LabelControl();
            this.sbtnSetting = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnOk = new DevExpress.XtraEditors.SimpleButton();
            this.m_labelUserNumber = new System.Windows.Forms.Label();
            this.m_labelPassword = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.sbtnData = new DevExpress.XtraEditors.SimpleButton();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.sbtnData);
            this.panel2.Controls.Add(this.ubclose);
            this.panel2.Controls.Add(this.ubmin);
            this.panel2.Controls.Add(this.utxtpwd);
            this.panel2.Controls.Add(this.utxtuser);
            this.panel2.Controls.Add(this.labtop);
            this.panel2.Controls.Add(this.sbtnSetting);
            this.panel2.Controls.Add(this.sbtnExit);
            this.panel2.Controls.Add(this.sbtnOk);
            this.panel2.Controls.Add(this.m_labelUserNumber);
            this.panel2.Controls.Add(this.m_labelPassword);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(500, 300);
            this.panel2.TabIndex = 5;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // ubclose
            // 
            this.ubclose.BackColor = System.Drawing.Color.Transparent;
            this.ubclose.BarType = Itop.Client.UserBar.bartype.close;
            this.ubclose.Location = new System.Drawing.Point(465, 0);
            this.ubclose.Name = "ubclose";
            this.ubclose.Size = new System.Drawing.Size(32, 20);
            this.ubclose.TabIndex = 13;
            // 
            // ubmin
            // 
            this.ubmin.BackColor = System.Drawing.Color.Transparent;
            this.ubmin.BarType = Itop.Client.UserBar.bartype.min;
            this.ubmin.Location = new System.Drawing.Point(434, 0);
            this.ubmin.Name = "ubmin";
            this.ubmin.Size = new System.Drawing.Size(32, 20);
            this.ubmin.TabIndex = 12;
            // 
            // utxtpwd
            // 
            this.utxtpwd.Location = new System.Drawing.Point(105, 119);
            this.utxtpwd.Name = "utxtpwd";
            this.utxtpwd.Size = new System.Drawing.Size(161, 22);
            this.utxtpwd.TabIndex = 1;
            // 
            // utxtuser
            // 
            this.utxtuser.Location = new System.Drawing.Point(105, 85);
            this.utxtuser.Name = "utxtuser";
            this.utxtuser.Size = new System.Drawing.Size(161, 22);
            this.utxtuser.TabIndex = 0;
            // 
            // labtop
            // 
            this.labtop.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labtop.Appearance.Options.UseBackColor = true;
            this.labtop.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labtop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labtop.Dock = System.Windows.Forms.DockStyle.Top;
            this.labtop.Location = new System.Drawing.Point(0, 0);
            this.labtop.Name = "labtop";
            this.labtop.Size = new System.Drawing.Size(500, 66);
            this.labtop.TabIndex = 11;
            this.labtop.MouseLeave += new System.EventHandler(this.labtop_MouseLeave);
            this.labtop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labtop_MouseMove);
            this.labtop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labtop_MouseDown);
            this.labtop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labtop_MouseUp);
            this.labtop.MouseEnter += new System.EventHandler(this.labtop_MouseEnter);
            // 
            // sbtnSetting
            // 
            this.sbtnSetting.Location = new System.Drawing.Point(68, 167);
            this.sbtnSetting.Name = "sbtnSetting";
            this.sbtnSetting.Size = new System.Drawing.Size(58, 23);
            this.sbtnSetting.TabIndex = 2;
            this.sbtnSetting.Text = "设 置";
            this.sbtnSetting.Click += new System.EventHandler(this.sbtnSetting_Click);
            // 
            // sbtnExit
            // 
            this.sbtnExit.Location = new System.Drawing.Point(195, 167);
            this.sbtnExit.Name = "sbtnExit";
            this.sbtnExit.Size = new System.Drawing.Size(58, 23);
            this.sbtnExit.TabIndex = 4;
            this.sbtnExit.Text = "退 出";
            this.sbtnExit.Click += new System.EventHandler(this.sbtnExit_Click);
            // 
            // sbtnOk
            // 
            this.sbtnOk.Location = new System.Drawing.Point(131, 167);
            this.sbtnOk.Name = "sbtnOk";
            this.sbtnOk.Size = new System.Drawing.Size(58, 23);
            this.sbtnOk.TabIndex = 3;
            this.sbtnOk.Text = "登 录";
            this.sbtnOk.Click += new System.EventHandler(this.sbtnOk_Click);
            // 
            // m_labelUserNumber
            // 
            this.m_labelUserNumber.AutoSize = true;
            this.m_labelUserNumber.BackColor = System.Drawing.Color.Transparent;
            this.m_labelUserNumber.ForeColor = System.Drawing.Color.MidnightBlue;
            this.m_labelUserNumber.Location = new System.Drawing.Point(57, 90);
            this.m_labelUserNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_labelUserNumber.Name = "m_labelUserNumber";
            this.m_labelUserNumber.Size = new System.Drawing.Size(41, 12);
            this.m_labelUserNumber.TabIndex = 2;
            this.m_labelUserNumber.Text = "用户名";
            // 
            // m_labelPassword
            // 
            this.m_labelPassword.AutoSize = true;
            this.m_labelPassword.BackColor = System.Drawing.Color.Transparent;
            this.m_labelPassword.ForeColor = System.Drawing.Color.MidnightBlue;
            this.m_labelPassword.Location = new System.Drawing.Point(57, 122);
            this.m_labelPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_labelPassword.Name = "m_labelPassword";
            this.m_labelPassword.Size = new System.Drawing.Size(41, 12);
            this.m_labelPassword.TabIndex = 3;
            this.m_labelPassword.Text = "密  码";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "male.ico");
            this.imageList1.Images.SetKeyName(1, "key.ico");
            // 
            // sbtnData
            // 
            this.sbtnData.Location = new System.Drawing.Point(407, 251);
            this.sbtnData.Name = "sbtnData";
            this.sbtnData.Size = new System.Drawing.Size(75, 23);
            this.sbtnData.TabIndex = 14;
            this.sbtnData.Text = "数据库";
            this.sbtnData.Click += new System.EventHandler(this.sbtnData_Click);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.sbtnOk;
            this.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(500, 300);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "LoginForm";
            this.Text = "电网规划-登录";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.Shown += new System.EventHandler(this.LoginForm_Shown);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_labelUserNumber;
        private System.Windows.Forms.Label m_labelPassword;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton sbtnExit;
        private DevExpress.XtraEditors.SimpleButton sbtnSetting;
        private DevExpress.XtraEditors.SimpleButton sbtnOk;
        private DevExpress.XtraEditors.LabelControl labtop;
        private System.Windows.Forms.ImageList imageList1;
        private UserText utxtpwd;
        private UserText utxtuser;
        private UserBar ubmin;
        private UserBar ubclose;
        private DevExpress.XtraEditors.SimpleButton sbtnData;
    }
}