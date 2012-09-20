				
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
using Itop.Domain;
using Itop.Client.Projects;
using Itop.Server;
using System.Diagnostics;
using System.IO;
namespace Itop.Client.Login {
    public partial class LoginForm : Itop.Client.Base.DialogForm {
        public LoginForm() {
            InitializeComponent();
            FormView.Paint(this);
            //this.Text = string.Format("登录 {0}", MIS.ApplicationCaption);

            // 通常的Dialog是不需要ShowInTaskbar，
            // 但是当第一登录的时候主窗体还没有创建，
            // 把它显示在Taskbar上，充当主窗体
            this.ShowInTaskbar = true;

            utxtuser.tbox.Text = Itop.Client.Option.Settings.GetLastLoginUserNumber();
            IniImage();
            ubmin.BarClick += new UserBar.barClick(ubmin_BarClick);
            ubclose.BarClick += new UserBar.barClick(ubclose_BarClick);
        }

        

       

        public LoginForm(bool reLogin)
            : this() {
            m_reLogin = reLogin;
           
            this.ShowInTaskbar = !m_reLogin;
            this.TopMost = true;
        }
        private void IniImage()
        {
            utxtuser.image = imageList1.Images[0];
            utxtpwd.image = imageList1.Images[1];
            utxtpwd.tbox.PasswordChar = '*';
            
           
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
            if (utxtuser.tbox.Text == string.Empty) {
                MsgBox.Show("工号没有输入");
                utxtuser.Focus();
                return;
            }

            ILoginAction loginAction = RemotingHelper.GetRemotingService<ILoginAction>();
            if (loginAction == null) {
                MsgBox.Show("ILoginAction没有被正确注册");
                return;
            }

            string token;
            string userNumber = utxtuser.tbox.Text.Trim();
            string password = utxtpwd.tbox.Text;
            LoginData data = new LoginData(userNumber, password);
            LoginStatus status;
            try {
                loginAction.Login(data, out token, out status);
            } catch (System.Net.Sockets.SocketException) {
                MsgBox.Show("无法连接服务器，请稍候重试");
                utxtpwd.tbox.Focus();
                return;
            }
            switch (status) {
                case LoginStatus.OK:
                    if (m_reLogin) {
                        // 原来的用户退出
                        if (!UserLogoutCommand.Exec(false)) {
                            MsgBox.Show("无法连接服务器，请稍候重试");
                            utxtpwd.tbox.Focus();
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
                    utxtuser.tbox.Focus();
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
                            CloseServer();
                            Application.Exit();
                        } else {
                            MsgBox.Show("密码输入错误次数超过三次，请稍候重试");
                            DialogResult = DialogResult.Cancel;
                        }
                    } else {
                        MsgBox.Show("密码输入错误，请重新输入密码");
                    }
                    utxtpwd.tbox.Text = "";
                    utxtpwd.tbox.Focus();
                    break;
                default:
                    break;
            }
        }

        private void txtUserid_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Return) {
                e.Handled = true;
                utxtpwd.tbox.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Return) {
                e.Handled = true;
                DoLogin();
            }
        }

        private void LoginForm_Shown(object sender, EventArgs e) {
            if (utxtuser.tbox.Text != string.Empty)
                utxtpwd.SetFocx();
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
            SetComboData();
            combCity.EditValue = RemotingHelper.CityName;

        }
        private void SetComboData()
        {
            try
            {
                IList<SysDataServer> dslist = ServicesSys.BaseService.GetList<SysDataServer>("SelectSysDataServerList", "");
                combCity.Properties.Columns.Clear();
                combCity.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                combCity.Properties.DataSource = dslist;
                combCity.Properties.DisplayMember = "CityName";
                combCity.Properties.ValueMember = "ID";
                combCity.Properties.NullText = "请选择城市";
                combCity.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CityName", "城市名称")});
            }
            catch (Exception ex)
            {

            }

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
            if (HasServer())
            {
                DoLogin();
            }
            else
            {
                StartServer();
                if (HasServer())
                {
                    DoLogin();
                }
            }
           
           
        }
        bool IsServerStart = false;
        private bool HasServer()
        {
            string cityid = RemotingHelper.CityName;
            SysDataServer ds = null;
            try
            {
                ds = ServicesSys.BaseService.GetOneByKey<SysDataServer>(cityid);
                if (combCity.EditValue == null)
                {
                    MsgBox.Show("请您选择正确的城市！");
                }

            }
            catch (Exception)
            {

                MessageBox.Show("无法连接到服务器！请确保服务器参数正确并确认服务器已启动");
                
            }
            return IsServerStart;
        }
        /// <summary>
        /// 启动本机服务
        /// </summary>
        private void StartServer()
        {
            CloseServer();
            string cityid = RemotingHelper.CityName;
            SysDataServer ds = null;
            try
            {
               ds = ServicesSys.BaseService.GetOneByKey<SysDataServer>(cityid);
               if (combCity.EditValue==null)
               {
                   IsServerStart= false;
                   return;
               }
               
            }
            catch (Exception)
            {
                IsServerStart= false;
                return;
            }
            if (ds==null)
            {
                return;
            }

                MIS.DataServer = ds;
                MIS.CityName = ds.CityName;
                CreateDir(ds.CityName);
                MIS.JD = ds.CityJD;
                MIS.WD = ds.CityWD;
                MIS.PYJD = ds.CityPYJD;
                MIS.PYWD = ds.CityPYWD;
                MIS.CityArea = ds.CityPYWD;

                Itop.Client.Option.Settings.SetJWDvalue(ds.CityJD, ds.CityWD);
                Itop.Client.Option.Settings.SetCityName(ds.CityName);
                Itop.Client.Option.Settings.SetPYJWDvalue(ds.CityPYJD, ds.CityPYWD);
                Itop.Client.Option.Settings.SetCityPYArea(ds.CityPYArea);
                int port=int.Parse(RemotingHelper.ServerPortSys);
                port++;
                ServerSettings.RemotingProtocol = RemotingHelper.ServerProtocolSys;
                ServerSettings.RemotingPort =port.ToString();
                ServerSettings.Pwd = ds.ServerPwd;
                ServerSettings.Uid = ds.ServerUser;
                ServerSettings.Database = ds.ServerName;
                ServerSettings.DataServer = ds.ServerAddress;
                ServerSettings.IsOneServer = "two";
                ServerSettings.Save();
                RemotingHelper.ServerAddress = "localhost";
                RemotingHelper.ServerPort = ServerSettings.RemotingPort;
                RemotingHelper.ServerProtocol = ServerSettings.RemotingProtocol;
               

                try
                {
                    ProcessStartInfo sysserver = new ProcessStartInfo(Application.StartupPath + "\\Server\\Itop.Server.exe");
                    sysserver.WorkingDirectory = Application.StartupPath + "\\Server";
                    MIS.curpro = System.Diagnostics.Process.Start(sysserver);
                    IsServerStart= true;
                    
                    
                }
                catch (Exception)
                {
                    IsServerStart= false;
                    throw;
                }
                
           
        }
        private void CloseServer()
        {
            if (MIS.curpro != null)
            {
                try
                {
                    MIS.curpro.Kill();
                }
                catch (Exception)
                {
                    
                   
                }
                
            }
        }
        #region 窗体美化w
        private bool m_isMouseDown = false;
        private Point m_mousePos = new Point();
        private void labtop_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void labtop_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void labtop_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (m_isMouseDown)
            {
                Point tempPos = Cursor.Position;
                this.Location = new Point(Location.X + (tempPos.X - m_mousePos.X), Location.Y + (tempPos.Y - m_mousePos.Y));
                m_mousePos = Cursor.Position;
            }
        }
        private void labtop_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            m_mousePos = Cursor.Position;
            m_isMouseDown = true;
        }

        private void labtop_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
            m_isMouseDown = false;
        }
        void ubclose_BarClick()
        {
            this.DialogResult = DialogResult.Cancel;
        }

        void ubmin_BarClick()
        {
            this.WindowState = FormWindowState.Minimized;
        }
        void userBar1_BarMin()
        {
           
        }
           #endregion
        private void sbtnData_Click(object sender, EventArgs e)
        {
            FrmSysData frm = new FrmSysData();
            frm.Show();
        }

        //判断地图文件夹是否存在，不存在创建
        private void CreateDir(string cityname)
        {
            string mappath=Application.StartupPath+"\\map";
            string citypath=mappath+"\\"+cityname;
            if (!Directory.Exists(mappath))
            {
                Directory.CreateDirectory(mappath);
                if (!Directory.Exists(citypath))
                {
                    Directory.CreateDirectory(citypath);
                }
            }
            else
            {
                if (!Directory.Exists(citypath))
                {
                    Directory.CreateDirectory(citypath);
                }

            }
        }
        private void lablogin_Click(object sender, EventArgs e)
        {
            if (HasServer())
            {
                DoLogin();
            }
            else
            {
                StartServer();
                if (HasServer())
                {
                    DoLogin();
                }
            }
        }
        private void labSet_Click(object sender, EventArgs e)
        {
            lablogin.ImageIndex = 0;
            loginsetting lst = new loginsetting();
            lst.ShowDialog();
            SetComboData();
            combCity.EditValue = RemotingHelper.CityName;
        }
        private void lablogin_MouseEnter(object sender, EventArgs e)
        {
            lablogin.ImageIndex = 1;
        }

        private void lablogin_MouseLeave(object sender, EventArgs e)
        {
            lablogin.ImageIndex = 0;
        }

        private void lablogin_MouseDown(object sender, MouseEventArgs e)
        {
            lablogin.ImageIndex = 2;
        }

        private void lablogin_MouseUp(object sender, MouseEventArgs e)
        {
            lablogin.ImageIndex = 0;
        }

       

        private void labSet_MouseEnter(object sender, EventArgs e)
        {
            labSet.ImageIndex = 4;

        }

        private void labSet_MouseLeave(object sender, EventArgs e)
        {
            labSet.ImageIndex = 3;
        }

        private void labSet_MouseUp(object sender, MouseEventArgs e)
        {
            labSet.ImageIndex = 3;
        }

        private void labSet_MouseDown(object sender, MouseEventArgs e)
        {
            labSet.ImageIndex = 5;
        }

        private void labSetServer_DoubleClick(object sender, EventArgs e)
        {
            FrmServerLogin frml=new FrmServerLogin ();
            
            if (frml.ShowDialog()==DialogResult.OK)
            {
                FrmSysData frm = new FrmSysData();
                frm.ShowDialog();
                StartServer();
                SetComboData();
            }
           
        }

        private void labSetServer_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void labSetServer_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult!=DialogResult.OK)
            {
                CloseServer();
            }
            
        }

        private void combCity_EditValueChanged(object sender, EventArgs e)
        {
            RemotingHelper.CityName = combCity.EditValue.ToString();

            StartServer();
          
        }




       
    }
}