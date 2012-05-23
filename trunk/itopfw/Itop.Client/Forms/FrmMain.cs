using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using Itop.Client.Base;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils.Frames;
using System.Globalization;
using Itop.Server.Interface;
using Itop.Common;
using Itop.Domain.RightManager;
using Itop.Client.Projects;
using System.Collections;
using Itop.Client.Resources;

namespace Itop.Client
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class FrmMain : XtraForm
    {
        public FrmMain()
        {
            loginwait lw = new loginwait();
            lw.ShowDialog();

            InitializeComponent();
            FormView.Paint(this);
            ubmin.BarClick += new UserBar.barClick(ubmin_BarClick);
            ubclose.BarClick += new UserBar.barClick(ubclose_BarClick);
            listmian.BackColor = grmian.BackColor;
        }
        //判断当前用户是否是管理员
        private bool IsSystemUser()
        {
            bool isadmin = false;
            //系统管理员
            string strsql = "Groupno='SystemManage' and Userid='" + MIS.UserNumber + "'";
            IList list = SysService.GetList("SelectSmugroupByWhere", strsql);
            if (list.Count > 0)
            {
                isadmin = true;
            }
            return isadmin;
        }
        private string CurrtenDHtext = "";
       
        private IBaseService sysService;
        public IBaseService SysService
        {
            get
            {
                if (sysService == null)
                {
                    sysService = RemotingHelper.GetRemotingService<IBaseService>();
                }
                if (sysService == null) MsgBox.Show("IBaseService服务没有注册");
                return sysService;
            }
        }

        #region 移动窗体
        private bool m_isMouseDown = false;
        private Point m_mousePos = new Point();
        private void labelControl1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            m_mousePos = Cursor.Position;
            m_isMouseDown = true;
        }

        private void labelControl1_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
            m_isMouseDown = false;
        }

        private void labelControl1_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (m_isMouseDown)
            {
                Point tempPos = Cursor.Position;
                this.Location = new Point(Location.X + (tempPos.X - m_mousePos.X), Location.Y + (tempPos.Y - m_mousePos.Y));
                m_mousePos = Cursor.Position;
            }
        }

        private void labelControl1_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void labelControl1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }
       
        #endregion

        #region 窗体控制按钮
        void ubmin_BarClick()
        {
            this.WindowState = FormWindowState.Minimized;
        }
        void ubclose_BarClick()
        {
            if (MessageBox.Show("确定退出系统？","询问",MessageBoxButtons.OKCancel,MessageBoxIcon.Question)==DialogResult.OK)
            {
                this.Close();
            }
            
        }

       
        #endregion

        private void FrmMain_Load(object sender, EventArgs e)
        {
            timer1.Start();
            SetFixedValue();
            SetListViewIco();
            InitDataProj();
            InitDataMeun();
        }
        //设置基本显示数据
        private void SetFixedValue()
        {
           labdateyl.Text = GetCNDate();
           labdate.Text = DateTime.Now.ToString("D") + " " + DateTime.Now.ToString("dddd");
           labuser.Text = "        " + MIS.UserName;
        }
        #region 添加项目导航数据
        DataTable dt;
        private void InitDataProj()
        {
            string s = "  IsGuiDang!='是' order by SortID";
            IList<Project> list = SysService.GetList<Project>("SelectProjectByWhere", s);
            dt = Itop.Common.DataConverter.ToDataTable((IList)list, typeof(Project));
            //过滤项目用户
            //非系统管理员
            if (!IsSystemUser())
            {
                IList<ProjectUser> listuser = SysService.GetList<ProjectUser>("SelectProjectUserbyWhere", " UserID='" + MIS.UserNumber + "'");
                Hashtable hasproj = new Hashtable();
                for (int i = 0; i < listuser.Count; i++)
                {
                    hasproj.Add(listuser[i].UID, listuser[i].UserID);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ProjectManager"].ToString() != "" && dt.Rows[i]["GuiDangName"].ToString() != MIS.UserNumber && !hasproj.ContainsKey(dt.Rows[i]["UID"].ToString()))
                    {
                        dt.Rows.Remove(dt.Rows[i]);
                        i--;
                    }
                }
            }
            ShowProj(dt);
        }
        private void ShowProj(DataTable projdt)
        {
            nbcProj.Groups.Clear();
            nbcProj.Items.Clear();
            DataRow[] rows = projdt.Select(string.Format("ProjectManager='{0}' ", string.Empty));
            foreach (DataRow row in rows)
            {
                DevExpress.XtraNavBar.NavBarGroup nbg = new DevExpress.XtraNavBar.NavBarGroup();
                nbg.Name = row["ProjectName"].ToString();
                nbg.Tag = DataConverter.RowToObject<Project>(row);
                nbg.Caption = row["ProjectName"].ToString();
                nbg.LargeImage =imageList2.Images[0];
                nbg.Hint = row["ProjectName"].ToString();
                DataRow[] childrows = projdt.Select(string.Format("ProjectManager='{0}'", row["UID"].ToString()));
                foreach (DataRow itemrow in childrows)
                {
                  
                    DevExpress.XtraNavBar.NavBarItem nbi = new DevExpress.XtraNavBar.NavBarItem();
                    nbi.Name = itemrow["ProjectName"].ToString();
                    nbi.Tag = DataConverter.RowToObject<Project>(itemrow);
                    nbi.Caption = itemrow["ProjectName"].ToString();
                    nbi.SmallImage = imageList3.Images[3];
                    nbi.Hint = itemrow["ProjectName"].ToString();
                    nbcProj.Items.Add(nbi);
                    nbg.ItemLinks.Add(nbi);
                }
                nbcProj.Groups.Add(nbg);
                nbcProj.Refresh();
            }
        }
        #endregion
       
        #region 添加模块导航数据
        string activegroupname = "";
        DataTable smmprogTable;
        private void InitDataMeun()
        {
            IList list = SysService.GetList<Smmprog>();
            AddMainMenu(list);
            DataRow[] rows = smmprogTable.Select(string.Format("parentid='{0}' and ProgType='{1}'", nbcMeun.ActiveGroup.Name, "f"));
            if (activegroupname != "")
            {
                foreach (DevExpress.XtraNavBar.NavBarGroup group in nbcMeun.Groups)
                {
                    if (activegroupname == group.Name)
                    {
                        nbcMeun.ActiveGroup = group;
                        break;
                    }
                }
            }

        }
        private void AddMainMenu(IList list)
        {
            nbcMeun.Groups.Clear();
            if (list != null && list.Count > 0)
            {
                smmprogTable = DataConverter.ToDataTable(list, typeof(Smmprog));
                AddItem();
            }
        }
        private void AddItem()
        {
            nbcMeun.Items.Clear();
            DataRow[] rows = smmprogTable.Select(string.Format("parentid='{0}' and ProgType='{1}'", string.Empty, "m"));
            IList list = SysService.GetList("SelectSmmprogByMeIco", null);
            DataTable dt_list = DataConverter.ToDataTable(list);
            nbcMeun.LargeImages = ImageListRes.GetimageList(28, dt_list);
            IList list2 = SysService.GetList("SelectSmmprogByFormIco", null);
            DataTable dt_list2 = DataConverter.ToDataTable(list2);
            nbcMeun.SmallImages = ImageListRes.GetimageList(22, dt_list2);
            foreach (DataRow row in rows)
            {

                DevExpress.XtraNavBar.NavBarGroup nbg = new DevExpress.XtraNavBar.NavBarGroup();
                nbg.Name = row["progname"].ToString();
                nbg.Tag = DataConverter.RowToObject<Smmprog>(row);
                nbg.Caption = row["progname"].ToString();
                nbg.Hint = row["progname"].ToString();
                nbg.LargeImage = ((ImageList)nbcMeun.LargeImages).Images[row["ProgIco"].ToString()];

                DataRow[] childrows = smmprogTable.Select(string.Format("parentid='{0}' and ProgType='{1}'", row["progid"].ToString(),"m"));
                foreach (DataRow itemrow in childrows)
                {
                    VsmdgroupProg smdgroup2 = new VsmdgroupProg();
                    smdgroup2 = MIS.GetProgRight(itemrow["ProgId"].ToString(), MIS.ProgUID);
                    if (smdgroup2 != null && Convert.ToInt32(smdgroup2.run) <= 0)
                        continue;
                    DevExpress.XtraNavBar.NavBarItem nbi = new DevExpress.XtraNavBar.NavBarItem();
                    nbi.Name = itemrow["progname"].ToString();
                    nbi.Tag = DataConverter.RowToObject<Smmprog>(itemrow);
                    nbi.Caption = itemrow["progname"].ToString();
                    nbi.SmallImage = ((ImageList)nbcMeun.SmallImages).Images[itemrow["ProgIco"].ToString()];
                    nbi.Hint = itemrow["progname"].ToString();
                    nbcMeun.Items.Add(nbi);
                    nbg.ItemLinks.Add(nbi);
                }
                nbcMeun.Groups.Add(nbg);
                nbcMeun.Refresh();
            }
        }
        #endregion
       
        private void timer1_Tick(object sender, EventArgs e)
        {
            labtime.Text = "       " + DateTime.Now.ToLongTimeString();
        }

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
        private void SetListViewIco()
        {
            IList listico = SysService.GetList("SelectSmmprogByFormIco", null);
            DataTable dt_list = DataConverter.ToDataTable(listico);
            listmian.SmallImageList = ImageListRes.GetimageList(50, dt_list);
            listmian.LargeImageList = ImageListRes.GetimageList(64, dt_list);
        }
        private void nbcMeun_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {

            Smmprog prog = e.Group.Tag as Smmprog;
            CurrtenDHtext = prog.ProgName;
            grmian.Text = CurrtenDHtext;
            labhelp.Text = prog.Remark;
            if (prog == null)
                return;
            listmian.Groups.Clear();
            listmian.Items.Clear();
            IList list = SysService.GetList<Smmprog>();
            DataRow[] rowsf = smmprogTable.Select(string.Format("parentid='{0}' and ProgType='{1}'", prog.ProgId, "f"));
            DataRow[] rowsm = smmprogTable.Select(string.Format("parentid='{0}' and ProgType='{1}'", prog.ProgId, "m"));
            if (rowsm.Length>0)
            {
                ListViewGroup listgroup = listmian.Groups.Add(prog.ProgId, prog.ProgName);
               foreach (DataRow rows1 in rowsf)
               {
                   ListViewItem item = new ListViewItem();
                   item.Text = rows1["progname"].ToString();
                   item.Tag = DataConverter.RowToObject<Smmprog>(rows1);
                   item.ToolTipText = rows1["progname"].ToString();
                   item.ImageKey = rows1["ProgIco"].ToString();
                   item.Group = listgroup;
                   listmian.Items.Add(item);
               }
                foreach (DataRow rows2 in rowsm)
                {
                    ListViewGroup listgroupm = listmian.Groups.Add(rows2["ProgId"].ToString(), rows2["progname"].ToString());
                    DataRow[] rowsmf = smmprogTable.Select(string.Format("parentid='{0}' and ProgType='{1}'", rows2["ProgId"].ToString(), "f"));
                    foreach (DataRow rows3 in rowsf)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = rows3["progname"].ToString();
                        item.Tag = DataConverter.RowToObject<Smmprog>(rows3);
                        item.ToolTipText = rows3["progname"].ToString();
                        item.ImageKey = rows3["ProgIco"].ToString();
                        item.Group = listgroupm;
                        listmian.Items.Add(item);
                    }

                }
               
            }
            else
            {
                foreach (DataRow rows1 in rowsf)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = rows1["progname"].ToString();
                    item.Tag = DataConverter.RowToObject<Smmprog>(rows1);
                    item.ToolTipText = rows1["progname"].ToString();
                    item.ImageKey = rows1["ProgIco"].ToString();
                    listmian.Items.Add(item);
                }
            }

        }
        Project pj
        {
            set 
            { 
                pj = value;
                MIS.ProgUID = pj.UID;
            }
            get
            {
                if (nbcProj.SelectedLink==null)
                {
                    return null;
                }
                else
                {
                    Project resproj=nbcProj.SelectedLink.Item.Tag as Project;
                    MIS.ProgUID = resproj.UID;
                    return resproj;
                }
            }
        }
        Project pj1
        {
            get
            {
                if (nbcProj.ActiveGroup==null)
                {
                    return null;
                }
                else
                {
                    return nbcProj.ActiveGroup.Tag as Project;
                }
            }   
        }
        private void nbcMeun_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DevExpress.XtraNavBar.NavBarItem nbi = e.Link.Item;
            Smmprog prog = nbi.Tag as Smmprog;
            CurrtenDHtext = nbcMeun.ActiveGroup.Caption + ">>" + prog.ProgName;
            grmian.Text = CurrtenDHtext;
            labhelp.Text = prog.Remark;
            listmian.Groups.Clear();
            listmian.Items.Clear();
            DataRow[] rowsf = smmprogTable.Select(string.Format("parentid='{0}' and ProgType='{1}'", prog.ProgId, "f"));
            foreach (DataRow rows1 in rowsf)
            {
                ListViewItem item = new ListViewItem();
                item.Text = rows1["progname"].ToString();
                item.Tag = DataConverter.RowToObject<Smmprog>(rows1);
                item.ToolTipText = rows1["progname"].ToString();
                item.ImageKey = rows1["ProgIco"].ToString();
                listmian.Items.Add(item);
            }
            
        }

        private void nbcProj_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            if (e.Group.ItemLinks.Count>0)
            {
                e.Group.SelectedLinkIndex = 0;
            }
        }
        private void nbcProj_SelectedLinkChanged(object sender, DevExpress.XtraNavBar.ViewInfo.NavBarSelectedLinkChangedEventArgs e)
        {
            pj = e.Link.Item.Tag as Project;
            MIS.ProgUID = pj.UID;
        }

        private void nbcProj_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            pj = e.Link.Item.Tag as Project;
            MIS.ProgUID = pj.UID;
           
        }

        private void nbcMeun_GroupExpanded(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            nbcMeun.ActiveGroup = e.Group;
        }

        private void listmian_ItemActivate(object sender, EventArgs e)
        {

            Smmprog prog = listmian.FocusedItem.Tag as Smmprog;
            if (prog == null || string.IsNullOrEmpty(prog.AssemblyName))
                return;
            labhelp.Text = prog.Remark;
            IList<Project> IlistProject = new List<Project>();
            VsmdgroupProg smdgroup = new VsmdgroupProg();
            //string projectuid = "";
            IlistProject.Add(pj);
            IlistProject.Add(pj1);
            smdgroup = MIS.GetProgRight(prog.ProgId, pj.UID);
            bool bl = true;
            if (Itop.Client.MIS.UserNumber.ToLower() == "admin")
                bl = false;

            if (bl)
            {
                if (smdgroup.run == null)
                {
                    MsgBox.Show("您无权浏览！");
                    return;
                }

                if (int.Parse(smdgroup.run) <= 0)
                {
                    MsgBox.Show("您无权浏览！");
                    return;
                }
            }
            if (prog.AssemblyName.ToLower().Contains(".exe") && prog.ClassName == "")
            {
                int pos = prog.AssemblyName.ToLower().LastIndexOf(".exe");
                string param = prog.AssemblyName.Substring(pos + 4).Trim();
                string exe = prog.AssemblyName.Substring(0, pos) + ".exe";
                System.Diagnostics.Process.Start(Application.StartupPath + "\\" + exe, param);
                return;
            }

            object[] para = new object[3];
            para.SetValue(IlistProject, 0);
            para.SetValue(smdgroup, 1);
            para.SetValue(prog, 2);

            object classInstance = null;
            //初始化标准数据
            Itop.Common.MethodInvoker.Execute(prog.AssemblyName, prog.ClassName, "InitData", para, ref classInstance);

            para = new object[0];

            if (Itop.Common.MethodInvoker.Execute(prog.AssemblyName, prog.ClassName, prog.MethodName, para, ref classInstance) != null)
            {
                MIS.SaveLog(prog.ProgName, "打开" + prog.ProgName);
            }
        }

        private void listmian_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            Smmprog prog = listmian.FocusedItem.Tag as Smmprog;
            if (prog == null)
                return;
            grmian.Text = CurrtenDHtext + ">>" + prog.ProgName;
            labhelp.Text = prog.Remark;
        }

       
        

       
        

       
       
        
        
       

      
       

       


       

       


      

      
    }
}
