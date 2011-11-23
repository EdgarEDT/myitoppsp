
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Itop.Client.MainMenu;
using Itop.Client.Console;
using Itop.Client.Forms;
using Itop.Client.Projects;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;

namespace Itop.Client {
    public partial class MainForm : XtraForm, IMainForm {
        public static MainForm CurrentForm;
        private FrmConsole m_mainConsoleForm;

        bool bl = false;
        public bool IsClose
        {
            get { return bl; }
        }



        public MainForm() {
            InitializeComponent();
            
            CurrentForm = this;
            statusStrip1.BackgroundImage =Itop.Client.Resources.ImageListRes.GetBottomPhoto();


            //ProjectTreeList pl = new ProjectTreeList();
            //if (pl.ShowDialog() != DialogResult.OK)
            //{
            //    bl = true;
            //    this.Close();
            //    return;
            //}
            this.Text = MIS.ApplicationCaption;

            this.FormClosing += delegate {
                Login.UserLogoutCommand.Exec(false);
            };

            SetStatusLabel();

            MIS.MainFormInterface = this;
            

            //创建控制台
            m_mainConsoleForm = new FrmConsole();
            //m_mainConsoleForm.PJ = pl.PJ;
            m_mainConsoleForm.MdiParent = this;
            m_mainConsoleForm.WindowState = FormWindowState.Maximized;
            m_mainConsoleForm.Show();
            

            m_mainMenu.Visible = false;
            // 创建主菜单
            ////////RefreshMainMenu();

            this.Shown += delegate {
                // TODO 下面的代码是临时性质的代码
                //Itop.Server.Interface.Forms.IFormsAction fa =
                //    Itop.Common.RemotingHelper.GetRemotingService<Itop.Server.Interface.Forms.IFormsAction>();
                //fa.CreateStoredProc(MIS.UserInfo);
            };
        }

        #region IMainForm 成员方法

        public void SetStatusLabel()
        {
            m_statusLabel.Text = string.Format("当前用户：{0} ", MIS.UserName);
            int imageindex;
            if (int.TryParse(MIS.UserLastLogon,out imageindex))
            {
               tooluser_image.Image = imageList2.Images[imageindex];
            }
        }

        //private DataSet m_mainMenuData;
        public void RefreshMainMenu() {
            
            MainmenuFactory.Create(m_mainMenu.Items);

            //创建“窗口”菜单，属于临时代码
            ToolStripMenuItem menuItem = new ToolStripMenuItem("窗口");
            
            m_mainMenu.Items.Add(menuItem);
            m_mainMenu.MdiWindowListItem = menuItem;
        }

        public void RefreshRecentMenu() {
            //m_mainConsoleForm.RefreshRecentMenu();
        }

        #endregion
    }
}