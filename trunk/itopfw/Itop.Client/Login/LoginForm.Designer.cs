			
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
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.sbtnOk = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labSetServer = new DevExpress.XtraEditors.LabelControl();
            this.labSetData = new System.Windows.Forms.Label();
            this.labSet = new System.Windows.Forms.Label();
            this.lablogin = new System.Windows.Forms.Label();
            this.sbtnData = new DevExpress.XtraEditors.SimpleButton();
            this.ubclose = new Itop.Client.UserBar();
            this.ubmin = new Itop.Client.UserBar();
            this.utxtpwd = new Itop.Client.UserText();
            this.utxtuser = new Itop.Client.UserText();
            this.labtop = new DevExpress.XtraEditors.LabelControl();
            this.m_labelUserNumber = new System.Windows.Forms.Label();
            this.m_labelPassword = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "初始登录300.png");
            this.imageList2.Images.SetKeyName(1, "滑动登录300.png");
            this.imageList2.Images.SetKeyName(2, "点击登录300.png");
            this.imageList2.Images.SetKeyName(3, "初始设置500.png");
            this.imageList2.Images.SetKeyName(4, "滑动设置500.png");
            this.imageList2.Images.SetKeyName(5, "点击设置500.png");
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "male.ico");
            this.imageList1.Images.SetKeyName(1, "key.ico");
            // 
            // sbtnOk
            // 
            this.sbtnOk.Location = new System.Drawing.Point(292, 193);
            this.sbtnOk.Name = "sbtnOk";
            this.sbtnOk.Size = new System.Drawing.Size(37, 15);
            this.sbtnOk.TabIndex = 3;
            this.sbtnOk.Text = "登 录";
            this.sbtnOk.Click += new System.EventHandler(this.sbtnOk_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.labSetServer);
            this.panel2.Controls.Add(this.labSetData);
            this.panel2.Controls.Add(this.labSet);
            this.panel2.Controls.Add(this.lablogin);
            this.panel2.Controls.Add(this.sbtnData);
            this.panel2.Controls.Add(this.ubclose);
            this.panel2.Controls.Add(this.ubmin);
            this.panel2.Controls.Add(this.utxtpwd);
            this.panel2.Controls.Add(this.utxtuser);
            this.panel2.Controls.Add(this.labtop);
            this.panel2.Controls.Add(this.m_labelUserNumber);
            this.panel2.Controls.Add(this.m_labelPassword);
            this.panel2.Controls.Add(this.sbtnOk);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(500, 300);
            this.panel2.TabIndex = 5;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // labSetServer
            // 
            this.labSetServer.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labSetServer.Location = new System.Drawing.Point(62, 33);
            this.labSetServer.Name = "labSetServer";
            this.labSetServer.Size = new System.Drawing.Size(63, 43);
            this.labSetServer.TabIndex = 18;
            this.labSetServer.ToolTip = "双击进入城市数据库管理";
            this.labSetServer.ToolTipTitle = "提示：";
            this.labSetServer.DoubleClick += new System.EventHandler(this.labSetServer_DoubleClick);
            this.labSetServer.MouseLeave += new System.EventHandler(this.labSetServer_MouseLeave);
            this.labSetServer.MouseEnter += new System.EventHandler(this.labSetServer_MouseEnter);
            // 
            // labSetData
            // 
            this.labSetData.ImageList = this.imageList2;
            this.labSetData.Location = new System.Drawing.Point(70, 37);
            this.labSetData.Name = "labSetData";
            this.labSetData.Size = new System.Drawing.Size(81, 58);
            this.labSetData.TabIndex = 17;
            // 
            // labSet
            // 
            this.labSet.ImageIndex = 3;
            this.labSet.ImageList = this.imageList2;
            this.labSet.Location = new System.Drawing.Point(357, 187);
            this.labSet.Name = "labSet";
            this.labSet.Size = new System.Drawing.Size(58, 23);
            this.labSet.TabIndex = 3;
            this.labSet.MouseLeave += new System.EventHandler(this.labSet_MouseLeave);
            this.labSet.Click += new System.EventHandler(this.labSet_Click);
            this.labSet.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labSet_MouseDown);
            this.labSet.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labSet_MouseUp);
            this.labSet.MouseEnter += new System.EventHandler(this.labSet_MouseEnter);
            // 
            // lablogin
            // 
            this.lablogin.ImageIndex = 0;
            this.lablogin.ImageList = this.imageList2;
            this.lablogin.Location = new System.Drawing.Point(279, 187);
            this.lablogin.Name = "lablogin";
            this.lablogin.Size = new System.Drawing.Size(58, 23);
            this.lablogin.TabIndex = 2;
            this.lablogin.MouseLeave += new System.EventHandler(this.lablogin_MouseLeave);
            this.lablogin.Click += new System.EventHandler(this.lablogin_Click);
            this.lablogin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lablogin_MouseDown);
            this.lablogin.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lablogin_MouseUp);
            this.lablogin.MouseEnter += new System.EventHandler(this.lablogin_MouseEnter);
            // 
            // sbtnData
            // 
            this.sbtnData.Location = new System.Drawing.Point(407, 251);
            this.sbtnData.Name = "sbtnData";
            this.sbtnData.Size = new System.Drawing.Size(75, 23);
            this.sbtnData.TabIndex = 14;
            this.sbtnData.Text = "数据库";
            this.sbtnData.Visible = false;
            this.sbtnData.Click += new System.EventHandler(this.sbtnData_Click);
            // 
            // ubclose
            // 
            this.ubclose.BackColor = System.Drawing.Color.Transparent;
            this.ubclose.BarType = Itop.Client.UserBar.bartype.close;
            this.ubclose.Location = new System.Drawing.Point(470, -2);
            this.ubclose.Name = "ubclose";
            this.ubclose.Size = new System.Drawing.Size(32, 20);
            this.ubclose.TabIndex = 13;
            // 
            // ubmin
            // 
            this.ubmin.BackColor = System.Drawing.Color.Transparent;
            this.ubmin.BarType = Itop.Client.UserBar.bartype.min;
            this.ubmin.Location = new System.Drawing.Point(439, -2);
            this.ubmin.Name = "ubmin";
            this.ubmin.Size = new System.Drawing.Size(32, 20);
            this.ubmin.TabIndex = 12;
            // 
            // utxtpwd
            // 
            this.utxtpwd.Location = new System.Drawing.Point(271, 153);
            this.utxtpwd.Name = "utxtpwd";
            this.utxtpwd.Size = new System.Drawing.Size(161, 22);
            this.utxtpwd.TabIndex = 1;
            // 
            // utxtuser
            // 
            this.utxtuser.Location = new System.Drawing.Point(271, 119);
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
            // m_labelUserNumber
            // 
            this.m_labelUserNumber.AutoSize = true;
            this.m_labelUserNumber.BackColor = System.Drawing.Color.Transparent;
            this.m_labelUserNumber.ForeColor = System.Drawing.Color.MidnightBlue;
            this.m_labelUserNumber.Location = new System.Drawing.Point(223, 124);
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
            this.m_labelPassword.Location = new System.Drawing.Point(223, 156);
            this.m_labelPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_labelPassword.Name = "m_labelPassword";
            this.m_labelPassword.Size = new System.Drawing.Size(41, 12);
            this.m_labelPassword.TabIndex = 3;
            this.m_labelPassword.Text = "密  码";
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "LoginForm";
            this.Text = "电网规划-登录";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.Shown += new System.EventHandler(this.LoginForm_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LoginForm_FormClosed);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_labelUserNumber;
        private System.Windows.Forms.Label m_labelPassword;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton sbtnOk;
        private DevExpress.XtraEditors.LabelControl labtop;
        private System.Windows.Forms.ImageList imageList1;
        private UserText utxtpwd;
        private UserText utxtuser;
        private UserBar ubmin;
        private UserBar ubclose;
        private DevExpress.XtraEditors.SimpleButton sbtnData;
        private System.Windows.Forms.Label lablogin;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.Label labSet;
        private System.Windows.Forms.Label labSetData;
        private DevExpress.XtraEditors.LabelControl labSetServer;
    }
}