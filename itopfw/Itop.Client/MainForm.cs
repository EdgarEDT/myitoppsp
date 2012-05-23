
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
using System.Globalization;

namespace Itop.Client {
    public partial class MainForm : XtraForm, IMainForm {
        public static MainForm CurrentForm;
        private FrmConsole m_mainConsoleForm;
        //private FrmMain m_mainConsoleForm;

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
            //m_mainConsoleForm = new FrmMain();
            //m_mainConsoleForm.PJ = pl.PJ;
            m_mainConsoleForm.MdiParent = this;
            m_mainConsoleForm.WindowState = FormWindowState.Maximized;
            m_mainConsoleForm.TopMost = true;
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
            timer1.Start();
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
        #region 显示农历日期
        string GetCNDate()
        {
            DateTime m_Date; //今天的日期
            int cny; //农历的年月日
            int cnm; //农历的年月日
            int cnd; //农历的年月日
            int icnm; //农历闰月

            m_Date = DateTime.Today;
            ChineseLunisolarCalendar cnCalendar = new ChineseLunisolarCalendar();
            cny = cnCalendar.GetSexagenaryYear(m_Date);
            cnm = cnCalendar.GetMonth(m_Date);
            cnd = cnCalendar.GetDayOfMonth(m_Date);
            icnm = cnCalendar.GetLeapMonth(cnCalendar.GetYear(m_Date));


            string txcns = "农历";
            const string szText1 = "癸甲乙丙丁戊己庚辛壬";
            const string szText2 = "亥子丑寅卯辰巳午未申酉戌";
            const string szText3 = "猪鼠牛虎免龙蛇马羊猴鸡狗";
            int tn = cny % 10; //天干
            int dn = cny % 12;  //地支
            //txcns += szText1.Substring(tn, 1);
            //txcns += szText2.Substring(dn, 1);
            //txcns += "(" + szText3.Substring(dn, 1) + ")年";

            //格式化月份显示
            string[] cnMonth ={ "", "正月", "二月", "三月", "四月", "五月", "六月"
                , "七月", "八月", "九月", "十月", "十一月", "十二月", "十二月" };
            if (icnm > 0)
            {
                for (int i = icnm + 1; i < 13; i++)
                    cnMonth[icnm] = cnMonth[icnm - 1];
                cnMonth[icnm] = "闰" + cnMonth[icnm];
            }
            txcns += cnMonth[cnm];
            string[] cnDay ={ "", "初一", "初二", "初三", "初四", "初五", "初六", "初七"
                , "初八", "初九", "初十", "十一", "十二", "十三"                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   , "十四", "十五", "十六"
                , "十七", "十八", "十九", "二十", "廿一", "廿二", "廿三", "廿四", "廿五"
                , "廿六", "廿七", "廿八", "廿九", "三十" };
            txcns += cnDay[cnd];
            return txcns;
        }
        #endregion
        private void timer1_Tick(object sender, EventArgs e)
        {
            toldate.Text =  DateTime.Now.ToString("D") + " " + DateTime.Now.ToString("dddd") +"  "+ GetCNDate() +"     ";
            toltime.Text =  DateTime.Now.ToString("HH时mm分ss秒"); 
        }
    }
}