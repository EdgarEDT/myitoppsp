				
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Itop.Common;
using Itop.Server.Interface.Login;
using Itop.Client.Option;
using System.Reflection;

namespace Itop.Client.Login {
    public partial class LoginForm : Itop.Client.Base.DialogForm {
        public LoginForm() {
            InitializeComponent();

            //this.Text = string.Format("登录 {0}", MIS.ApplicationCaption);

            // 通常的Dialog是不需要ShowInTaskbar，
            // 但是当第一登录的时候主窗体还没有创建，
            // 把它显示在Taskbar上，充当主窗体
            this.ShowInTaskbar = true;

            txtUserid.Text = Itop.Client.Option.Settings.GetLastLoginUserNumber();
        }

        public LoginForm(bool reLogin)
            : this() {
            m_reLogin = reLogin;

            this.ShowInTaskbar = !m_reLogin;
        }

        /// <summary>
        /// 登录时候密码错误次数
        /// </summary>
        private Dictionary<string, int> m_error = new Dictionary<string, int>();

        /// <summary>
        /// 是否是重新登录, true: 是重新登录
        /// </summary>
        private bool m_reLogin = false;

        private void button1_Click(object sender, EventArgs e) {
           
        }

        /// <summary>
        /// 执行登录
        /// </summary>
        private void DoLogin() {
            if (txtUserid.Text == string.Empty) {
                MsgBox.Show("工号没有输入");
                txtUserid.Focus();
                return;
            }

            ILoginAction loginAction = RemotingHelper.GetRemotingService<ILoginAction>();
            if (loginAction == null) {
                MsgBox.Show("ILoginAction没有被正确注册");
                return;
            }

            string token;
            string userNumber = txtUserid.Text.Trim();
            string password = txtPassword.Text;
            LoginData data = new LoginData(userNumber, password);
            LoginStatus status;
            try {
                loginAction.Login(data, out token, out status);
            } catch (System.Net.Sockets.SocketException) {
                MsgBox.Show("无法连接服务器，请稍候重试");
                txtPassword.Focus();
                return;
            }
            switch (status) {
                case LoginStatus.OK:
                    if (m_reLogin) {
                        // 原来的用户退出
                        if (!UserLogoutCommand.Exec(false)) {
                            MsgBox.Show("无法连接服务器，请稍候重试");
                            txtPassword.Focus();
                            return;

                        }

                        //MIS.WriteApplicationLog("系统", "退出系统");
                    }
                    DialogResult = DialogResult.OK;
                    MIS.Token = token;
                    MIS.UserNumber = data.UserNumber;
                    //MIS.WriteApplicationLog("系统", "登录系统");

                    // 记录最近一次登录的用户的工号
                    Itop.Client.Option.Settings.SetLastLoginUserNumber(userNumber);
                    
                    break;
                case LoginStatus.InvalidUser:
                    MsgBox.Show("工号输入错误");
                    txtUserid.Focus();
                    break;
                case LoginStatus.InvalidPassword:
                    if (m_error.ContainsKey(userNumber)) {
                        // 错误加一
                        m_error[userNumber]++;
                    } else {
                        // 第一次错误
                        m_error.Add(userNumber, 1);
                    }

                    if (m_error[userNumber] >= 3) {
                        if (!m_reLogin) {
                            MsgBox.Show("密码输入错误次数超过三次，无法登录系统");
                            Application.Exit();
                        } else {
                            MsgBox.Show("密码输入错误次数超过三次，请稍候重试");
                            DialogResult = DialogResult.Cancel;
                        }
                    } else {
                        MsgBox.Show("密码输入错误，请重新输入密码");
                    }
                    txtPassword.Text = "";
                    txtPassword.Focus();
                    break;
                default:
                    break;
            }
        }

        private void txtUserid_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Return) {
                e.Handled = true;
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Return) {
                e.Handled = true;
                DoLogin();
            }
        }

        private void LoginForm_Shown(object sender, EventArgs e) {
            if (txtUserid.Text != string.Empty)
                txtPassword.Focus();
        }

        private void txtUserid_TextChanged(object sender, EventArgs e) {

        }

        private void linkClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            this.DialogResult = DialogResult.Cancel;
        }

        private void linkClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.BackgroundImage =  Itop.Client.Resources.ImageListRes.GetLoginPhoto();
            DevExpress.XtraGrid.Localization.GridLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraGridLocalizationCHS();
            DevExpress.XtraTreeList.Localization.TreeListLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraTreeListLocalizationCHS();
            DevExpress.XtraPrinting.Localization.PreviewLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraPrintingLocalizationCHS();
            DevExpress.XtraVerticalGrid.Localization.VGridLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraVerticalGridLocalizationCHS();
            DevExpress.XtraBars.Localization.BarLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraBarsLocalizationCHS();
            DevExpress.XtraEditors.Controls.Localizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraEditorsLocalizationCHS();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sbtnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void sbtnSetting_Click(object sender, EventArgs e)
        {
            loginsetting lst = new loginsetting();
            lst.ShowDialog();
        }

        private void sbtnOk_Click(object sender, EventArgs e)
        {
            DoLogin();
        }



       
    }
}