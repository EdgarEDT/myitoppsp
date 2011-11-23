using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
using Itop.Domain.RightManager;
using Itop.Server.Interface;
using Itop.Common;
using Itop.Client.Resources;
using System.IO;
using Itop.Client.Option;
using System.Reflection;
using System.Diagnostics;
using Itop.Client.Projects;
using DevExpress.XtraTreeList.Nodes;
using System.Globalization;

namespace Itop.Client.Forms
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class FrmConsole : MDIChildForm, IFrmConsole
    {
        //是否第一次载入
        bool Isfristload = true;
        DataTable dt = new DataTable();
        string UserID = MIS.UserNumber;
        FileIni User_Ini = new FileIni(Application.StartupPath + "\\User.ini");
        #region 构造方法
        Project pj = null;
        public Project PJ
        {
            set { pj = value; }

        }

        Project pj1 = null;
        public Project PJ1
        {
            set { pj1 = value; }
        }

        Project XMGLpj = null;
        static string XMGLGUID = "6a743afa-f166-48f1-92ea-fed929e22cee";//项目卷宗管理UID
        public FrmConsole()
        {
            loginwait lw = new loginwait();
            lw.ShowDialog();

            InitializeComponent();
            m_actionId = ActionId.CONSOLE;

            this.FormClosing += delegate(object sender, FormClosingEventArgs e)
            {
                e.Cancel = e.CloseReason != CloseReason.MdiFormClosing;
            };

        }
        #endregion
        
        #region 字段
        IList<Project> list1 = new List<Project>();
        DataTable smmprogTable;
        private IBaseService sysService;
        static IList list = new ArrayList();

        delegate void InitUserControldelegate();


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
        #endregion

        #region 辅助方法

        public void InitProject()
        { }

        bool oneload = false;
        public void InitData()
        {//先根据分辩率调整图标
            oneload = true;
            Adapt_WindowsScreen();
            InitIco();
            //treeView.Nodes.Clear();
            //label3.Text = pj.ProjectName;
            InitRightControl();
            oneload = false;
            InitSelect();
            InitSelectControl();
            //treeView.Nodes.Clear();
            list = SysService.GetList<Smmprog>();
            if (list != null && list.Count > 0)
            {
                smmprogTable = DataConverter.ToDataTable(list, typeof(Smmprog));
                //ExpandNodes1(string.Empty);
                //ExpandNodes(treeView.Nodes, string.Empty);
            }
            //treeView.SelectedNode = treeView.Nodes[0];

            #region 添加左则菜单
            //AddMainMenu(list);
           

            #endregion

        }
        private void  AddMainMenu(IList list)
        {
             nbctSystem.Groups.Clear();
             if (list != null && list.Count > 0)
             {
                 smmprogTable = DataConverter.ToDataTable(list, typeof(Smmprog));
                 //ExpandNodes1(string.Empty);
                 AddItem();
             }
        }
        private void AddItem()
        {
            nbctSystem.Items.Clear();
            DataRow[] rows = smmprogTable.Select(string.Format("parentid='{0}' and ProgType='{1}'", string.Empty, "m"));
            IList list = SysService.GetList("SelectSmmprogByMeIco", null);
            DataTable dt_list = DataConverter.ToDataTable(list);
            nbctSystem.LargeImages = ImageListRes.GetimageList(38, dt_list);
            IList list2 = SysService.GetList("SelectSmmprogByFormIco", null);
            DataTable dt_list2 = DataConverter.ToDataTable(list2);
            nbctSystem.SmallImages = ImageListRes.GetimageList(28, dt_list2);
            foreach (DataRow row in rows)
            {
               
                DevExpress.XtraNavBar.NavBarGroup nbg = new DevExpress.XtraNavBar.NavBarGroup();
                nbg.Name = row["progname"].ToString();
                nbg.Tag = DataConverter.RowToObject<Smmprog>(row);
                nbg.Caption = row["progname"].ToString();
                nbg.LargeImage = ((ImageList)nbctSystem.LargeImages).Images[row["ProgIco"].ToString()];


                DataRow[] childrows = smmprogTable.Select(string.Format("parentid='{0}' and ProgType='{1}'", row["progid"].ToString(), "f"));
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
                    nbi.SmallImage = ((ImageList)nbctSystem.SmallImages).Images[itemrow["ProgIco"].ToString()];
                    nbi.Hint = row["Index"].ToString();
                    nbctSystem.Items.Add(nbi);
                    nbg.ItemLinks.Add(nbi);
                }
                nbctSystem.Groups.Add(nbg);
                nbctSystem.Refresh();
            }
        }
        //记录显示、模式，左下右框架距离
        private int Select_View = 0;
        private int Select_Type = 1;
        private int left_width = 200;
        private int down_width = 229;
        private int right_width = 560;
        private float FontSize = 9F;
        //存放图标尺寸
        // 左则列表，中间小图，中间大图的尺寸
        //分别为大中小三个尺寸的图标和文字
        private int[] image_size_B ={ 35, 28, 70 };
        private int[] image_size_M ={ 30, 24, 60 };
        private int[] image_size_L ={ 25, 20, 40 };
        private float Font_size_B = 12F;
        private float Font_size_M = 10F;
        private float Font_size_L = 8F;
        private int Base_image_size = 5;
        private float Base_font_size = 8F;
        public void Adapt_WindowsScreen()
        {
            
            int   width_screen,height_screen;
            int Result_screen;
            string   Resolution; 
            width_screen=System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width; 
            height_screen=System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            Result_screen = width_screen * height_screen;
            Resolution=width_screen.ToString()+ "*"+height_screen.ToString();
            if (Result_screen<=480000)
            {
                Base_image_size = 4;
                Base_font_size = 6F;
            }
            if (Result_screen > 480000 && Result_screen < 1024000)
            {
                Base_image_size = 5;
                Base_font_size = 8F;
            }
            if (Result_screen >= 1024000 && Result_screen <1764000)
            {
                Base_image_size = 6;
                Base_font_size = 9F;
            }
            if (Result_screen >= 1764000 && Result_screen < 2304000)
            {
                Base_image_size = 7;
                Base_font_size = 10F;
            }
            if (Result_screen > 2304000)
            {
                Base_image_size = 8;
                Base_font_size = 11F;
            }
          
            Change_Size();
        }
        private void Change_Size()
        {
            image_size_L[0] = Base_image_size*5;
            image_size_L[1] = Convert.ToInt32(image_size_L[0] * 0.8);
            image_size_L[2] = image_size_L[0] * 2;

            image_size_M[0] = Base_image_size * 6;
            image_size_M[1] = Convert.ToInt32(image_size_M[0] * 0.8);
            image_size_M[2] = image_size_M[0] * 2;

            image_size_B[0] = Base_image_size * 7;
            image_size_B[1] = Convert.ToInt32(image_size_B[0] * 0.8);
            image_size_B[2] = image_size_B[0] * 2;

            Font_size_L = Base_font_size;
            Font_size_M = Font_size_L + 2F;
            Font_size_B = Font_size_L + 4F;
           
        }
        private void Del_User_ini()
        {
            if (File.Exists(Application.StartupPath+"\\User.ini"))
            {
                User_Ini = new FileIni(Application.StartupPath + "\\User.ini");
                string Select_Views = User_Ini.ReadValue("Setting", UserID+"Select_View");
                string Select_Types = User_Ini.ReadValue("Setting", UserID+"Select_Type");
                string left_widths = User_Ini.ReadValue("Setting", UserID+"left_width");
                string down_widths = User_Ini.ReadValue("Setting", UserID+"down_width");
                string right_widths = User_Ini.ReadValue("Setting", UserID+"right_width");
                string FontSizes = User_Ini.ReadValue("Setting", UserID + "FontSize");
                try 
	            {	        
		           if (Select_Views!="")
	                {
                       Select_View=Convert.ToInt32(Select_Views.ToString());
	                }
                    if (Select_Types != "")
	                {
                       Select_Type=Convert.ToInt32(Select_Types.ToString());
	                }
                    if (left_widths != "")
	                {
                        left_width = Convert.ToInt32(left_widths.ToString());
	                }
                    if (down_widths != "")
	                {
                        down_width = Convert.ToInt32(down_widths.ToString());
	                }
                    if (right_widths != "")
	                {
                        right_width = Convert.ToInt32(right_widths.ToString());
	                }
                    if (FontSizes != "")
                    {
                        FontSize = float.Parse(FontSizes.ToString());
                    }
	              }
                catch (Exception)
                {
            		
	                throw;
                }
            }
            else
            {
                User_Ini = new FileIni(Application.StartupPath + "\\User.ini");
            }
            switch (Select_View)
            {
                case 0: toolStripMenu_Sl.PerformClick();
                    break;
                case 1: toolStripMenu_PP.PerformClick();
                    break;
                case 2: toolStripMenu_TB.PerformClick();
                    break;
                case 3: toolStripMenu_LB.PerformClick();
                    break;
                default:
                    break;
            }
            switch (Select_Type)
            {
                case 0: toolStripMenu_Big.PerformClick();
                    break;
                case 1: toolStripMenu_Mid.PerformClick();
                    break;
                case 2: toolStripMenu_Little.PerformClick();
                    break;
                default:
                    break;
            }
            splitContainer1.SplitterDistance = left_width;
            splitContainer3.SplitterDistance = down_width;
            splitContainer2.SplitterDistance = right_width;
            treeList1.Columns[0].AppearanceCell.Font = new System.Drawing.Font(treeList1.Columns[0].AppearanceCell.Font.FontFamily, FontSize);
            treeList1.RowHeight = Convert.ToInt32(20 * treeList1.Columns[0].AppearanceCell.Font.Size / 10F);
            treeList1.Refresh();

        }
        private void InitProRight()
        {
            添加项目ToolStripMenuItem.Visible = false;
            添加卷宗ToolStripMenuItem.Visible = false;
            修改ToolStripMenuItem.Visible = false;
            删除ToolStripMenuItem.Visible = false;
            恢复项目ToolStripMenuItem.Visible = false;
            关联图层ToolStripMenuItem.Visible = false;
            拷贝卷宗ToolStripMenuItem.Visible = false;
            VsmdgroupProg smdgroup = new VsmdgroupProg();
            //smdgroup = MIS.GetProgRight("5efeb782-6273-4734-a0ce-5588d22673a7", pj.UID);
           
            smdgroup = MIS.GetProgRight(MIS.ProgUID, pj.UID);
            bool b1 = false;
            try
            {
                b1 = (int.Parse(smdgroup.ins) > 0) ? true : false;
            }
            catch { }

            bool b2 = false;
            try
            {
                b2 = (int.Parse(smdgroup.upd) > 0) ? true : false;
            }
            catch { }

            bool b3 = false;
            try
            {
                b3 = (int.Parse(smdgroup.del) > 0) ? true : false;
            }
            catch { }

            bool b4 = false;
            try
            {
                b4 = (int.Parse(smdgroup.run) > 0) ? true : false;
            }
            catch { }

            if (b4)
            {
                if (b1)
                {
                    添加项目ToolStripMenuItem.Visible = true;
                    添加卷宗ToolStripMenuItem.Visible = true;
                }
                if (b2)
                    修改ToolStripMenuItem.Visible = true;
                if (b3)
                    删除ToolStripMenuItem.Visible = true;
            }
            if (MIS.UserNumber.ToLower() == "admin")
            {
                添加项目ToolStripMenuItem.Visible = true;
                添加卷宗ToolStripMenuItem.Visible = true;
                修改ToolStripMenuItem.Visible = true;
                删除ToolStripMenuItem.Visible = true;
                恢复项目ToolStripMenuItem.Visible = true;
                关联图层ToolStripMenuItem.Visible = true;
                拷贝卷宗ToolStripMenuItem.Visible = true;
            }
        
        }

        //private void ExpandNodes1(string parentid)
        //{
        //    DataRow[] rows = smmprogTable.Select(string.Format("parentid='{0}' and ProgType='{1}'", parentid, "m"));
        //    foreach (DataRow row in rows)
        //    {
        //        //TreeNode node = treeView.Nodes.Add(row["progname"].ToString());
        //        node.Tag = DataConverter.RowToObject<Smmprog>(row);
        //        node.Name = row["progid"].ToString();
        //        node.ImageKey = row["ProgIco"].ToString();
        //        node.SelectedImageKey = row["ProgIco"].ToString();
        //    }
        //}

        private void ExpandNodes(TreeNodeCollection parentNodes, string parentid)
        {
            DataRow[] rows = smmprogTable.Select(string.Format("parentid='{0}' and ProgType='{1}'", parentid, "m"));
            foreach (DataRow row in rows)
            {
                TreeNode node = parentNodes.Add(row["progname"].ToString());
                node.Tag = DataConverter.RowToObject<Smmprog>(row);
                node.Name = row["progid"].ToString();
                node.ImageKey = row["ProgIco"].ToString();
                node.SelectedImageKey = row["ProgIco"].ToString();
                ExpandNodes(node.Nodes, node.Name);
            }
        }



        private void InitForm()
        {
            //splitContainer1.Panel1.BackgroundImage = Itop.Client.Resources.ImageListRes.GetLeftPhoto();
            picMenu.Image = Itop.Client.Resources.ImageListRes.GetLeftPhoto();

            //treeView.BackColor = System.Drawing.Color.FromArgb(233, 248, 255);
            //listViewdown.BackColor = System.Drawing.Color.FromArgb(233, 248, 255);
           // splitContainer3.Panel1.BackColor = System.Drawing.Color.FromArgb(233, 248, 255);

            this.BackgroundImage = Itop.Client.Resources.ImageListRes.GetBannerPhoto();

            //InitUserControldelegate ic = new InitUserControldelegate(InitUserControl);
            //this.BeginInvoke(ic);
            InitIco();
        }

        private void InitIco()
        {
            try
            {
                IList list = SysService.GetList("SelectSmmprogByFormIco", null);
                DataTable dt_list = DataConverter.ToDataTable(list);
                listViewdown.SmallImageList = ImageListRes.GetimageList(image_size_M[1], dt_list);
                listViewdown.LargeImageList = ImageListRes.GetimageList(image_size_M[2], dt_list);
                this.listViewdown.Font = new System.Drawing.Font("宋体", Font_size_M, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));


                
            }
            catch (Exception ex)
            {

            }

            try
            {
                IList list = SysService.GetList("SelectSmmprogByMeIco", null);
                DataTable dt_list = DataConverter.ToDataTable(list);
                //treeView.ImageList = ImageListRes.GetimageList(image_size_M[0], dt_list);
                //this.treeView.Font = new System.Drawing.Font("宋体", Font_size_M, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            }
            catch (Exception ex)
            {

            }
     
        }

        private void InitUserControl()
        {

            //加入右侧用户控件
            this.splitContainer2.Panel2.Controls.Clear();

            string strCtrlConfig = Settings.GetValue("RightPanelControl");
            if (string.IsNullOrEmpty(strCtrlConfig))
            {
                return;
            }

            string[] strs = strCtrlConfig.Split(new char[] { ';' });
            if (strs.Length != 2)
            {
                return;
            }
            
            Assembly asm = Assembly.LoadFrom(Application.StartupPath + "\\" + strs[0]);
            UserControl ctrl = asm.CreateInstance(strs[1]) as UserControl;
            ctrl.Name = pj.UID;
            ctrl.Parent = this.splitContainer2.Panel2;
            ctrl.Dock = DockStyle.Fill;
        }


        #endregion

        #region 事件处理
        void Default_StyleChanged(object sender, EventArgs e)
        {

        }

        private void FrmConsole_Load(object sender, EventArgs e)
        {
                 

            InitForm();
            InitData();


            MIS.MFrmConsole = this;

            //关闭定时器
            timer_update.Enabled = false;
            UpdateSys update =new UpdateSys(timer_update_Tick);
            //异步执行检查更新
            this.BeginInvoke(update, new object[2] { null, null });
            //InitUserControldelegate ic = new InitUserControldelegate(InitUserControl);
            //this.BeginInvoke(ic);
            Del_User_ini();
            Isfristload = false;
            InitdownItem();
        }
        //添加底部固定基础数据项
        private void InitdownItem()
        {
            ////9844c01d-6198-4cee-9733-5160a818af1d基础数的id
            //DataRow[] rows = smmprogTable.Select(string.Format("parentid='{0}' and ProgType='{1}'", "9844c01d-6198-4cee-9733-5160a818af1d", "f"));
            //foreach (DataRow row in rows)
            //{
            //    VsmdgroupProg smdgroup = new VsmdgroupProg();
            //    smdgroup = MIS.GetProgRight(row["ProgId"].ToString(), MIS.ProgUID);
            //    if (smdgroup != null && Convert.ToInt32(smdgroup.run) > 0)
            //    {

            //        ListViewItem listItem = new ListViewItem();
            //        listItem.Text = row["ProgName"].ToString();
            //        listItem.Tag = DataConverter.RowToObject<Smmprog>(row);
            //        listItem.ImageKey = row["ProgIco"].ToString();
            //        if (row["Remark"] != null)
            //        {
            //            listItem.ToolTipText = row["Remark"].ToString();
            //        }
            //        listViewdown.Items.Add(listItem);
            //    }
            //}
            listViewdown.Items.Clear();
            SortedList<int, Smmprog> Slist = new SortedList<int, Smmprog>();
            foreach (DevExpress.XtraNavBar.NavBarItem nbi in nbctSystem.Items)
            {
                Smmprog prog = nbi.Tag as Smmprog;
                if (prog!=null)
                {

                    if (!string.IsNullOrEmpty(prog.Remark))
                    {
                        if (prog.Remark.Contains("fixed"))
                        {
                            int a = Convert.ToInt32(nbi.Hint.ToString());
                            int num = a * 50 + prog.Index;
                            Slist.Add(num, prog);
                          
                        }

                    }
                }
            }

            foreach (KeyValuePair<int, Smmprog> item in Slist)
            {
                    Smmprog prog = item.Value;
                    ListViewItem listItem = new ListViewItem();
                    listItem.Text = prog.ProgName;
                    listItem.Tag = prog;
                    listItem.ImageKey = prog.ProgIco;
                    listViewdown.Items.Add(listItem);
            }

          
           
        }
        private delegate void UpdateSys(object sender, EventArgs e);


        private void timer_Tick(object sender, EventArgs e)
        {
            labTime.Text = DateTime.Now.ToString("yyyy年MM月dd日  HH时mm分ss秒");
        }

        private void labExit_Click(object sender, EventArgs e)
        {
            Itop.Client.Login.UserLogoutCommand.Execute();
        }


        private void labAbout_Click(object sender, EventArgs e)
        {
            //打开关于窗体
            using (FrmAbout dlg = new FrmAbout())
            {
                dlg.ShowDialog();
            }
        }


        #endregion

        private void timer_update_Tick(object sender, EventArgs e) 
        {
            //////////string filename = Application.StartupPath + "\\Itop.UPDATE.exe";

            //////////if (File.Exists(filename))
            //////////{

            //////////    if (MIS.HasNewVersion())
            //////////    {
            //////////        timer_update.Enabled = false;

            //////////        if (MsgBox.ShowYesNo("发现新版本,是否立刻更新?") == DialogResult.Yes)
            //////////        {
            //////////            System.Diagnostics.Process.Start(filename, "RUN");
            //////////            timer_update.Interval = 600000;
            //////////        }
            //////////        timer_update.Enabled = true;
            //////////    }
            //////////    else
            //////////    {
            //////////        timer_update.Interval = 60000;
            //////////    }
            //////////}
            //////////else
            //////////{
            //////////    timer_update.Enabled = false;
            //////////}
        }

        private void label1_Click(object sender, EventArgs e)
        {
            string tempfile = Application.StartupPath;
            string filename = "help.chm";
            try
            {
                System.Diagnostics.Process.Start(tempfile + "\\" + filename);
            }
            catch
            {
                MsgBox.Show("找不到指定的文件help.chm。");
            }

        }

        private void FrmConsole_Resize(object sender, EventArgs e)
        {

            if (this.splitContainer1.SplitterDistance < 190)
            {
                this.splitContainer1.SplitterDistance = 220;
            }
            
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Smmprog sp = treeView.SelectedNode.Tag as Smmprog;       

            //listViewdown.Items.Clear();
            //label2.Text = treeView.SelectedNode.Text;
            ////流程图
            //if (treeView.SelectedNode.Parent!=null)
            //{
            //    frmsystempic.ChangePicture(treeView.SelectedNode.Parent.Text);
            //}
            //else
            //{
            //    frmsystempic.ChangePicture(treeView.SelectedNode.Text);
            //}
            
            //DataRow[] rows = smmprogTable.Select(string.Format("parentid='{0}' and ProgType='{1}'", treeView.SelectedNode.Name, "f"));
            //foreach (DataRow row in rows)
            //{
            //    VsmdgroupProg smdgroup = new VsmdgroupProg();
            //    smdgroup = MIS.GetProgRight(row["ProgId"].ToString(), MIS.ProgUID);
            //    if (smdgroup != null && Convert.ToInt32(smdgroup.run) > 0)
            //    {

            //        ListViewItem listItem = new ListViewItem();
            //        listItem.Text = row["ProgName"].ToString();
            //        listItem.Tag = DataConverter.RowToObject<Smmprog>(row);
            //        listItem.ImageKey = row["ProgIco"].ToString();
            //        if (row["Remark"] != null)
            //        {
            //            listItem.ToolTipText = row["Remark"].ToString();
            //        }
            //        listViewdown.Items.Add(listItem);
            //    }
            //}
        }

        private void listView_ItemActivate(object sender, EventArgs e)
        {

            Smmprog prog = listViewdown.FocusedItem.Tag as Smmprog;
            if (prog == null || string.IsNullOrEmpty(prog.AssemblyName))
                return;

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
                //InitUserControl();
                MIS.SaveLog(prog.ProgName, "关闭" + prog.ProgName);
            }
        }

        private void splitContainer2_Panel1_Resize(object sender, EventArgs e)
        {
            //listView.Height = splitContainer2.Panel1.Height - 20;
            //listView.Width = splitContainer2.Panel1.Width - 20;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            //InitPro();
            IList<Project> IlistProject = new List<Project>();
            IlistProject.Add(pj);
            IlistProject.Add(pj1);
            VsmdgroupProg smdgroup = new VsmdgroupProg();
            smdgroup = MIS.GetProgRight("36f9d9ad-3580-4db5-a55f-9a6fd35616e4", pj.UID);

            Smmprog prog = SysService.GetOneByKey<Smmprog>("36f9d9ad-3580-4db5-a55f-9a6fd35616e4");

            object[] para = new object[3];
            para.SetValue(IlistProject, 0);
            para.SetValue(smdgroup, 1);
            para.SetValue(prog, 2);

            object classInstance = null;
            //初始化标准数据
            Itop.Common.MethodInvoker.Execute("Itop.Client.Layouts.dll", "Itop.Client.Layouts.FrmFiles", "InitData", para, ref classInstance);
            para = new object[0];
            if (Itop.Common.MethodInvoker.Execute("Itop.Client.Layouts.dll", "Itop.Client.Layouts.FrmFiles", "Execute", para, ref classInstance) != null)
            {
                int dd=1;
                dd++;
            }



        }

        private void InitPro()
        {
            ProjectTreeList pl = new ProjectTreeList();
            if (pl.ShowDialog() == DialogResult.OK)
            {
                //bl = true;
                pj = pl.PJ;
                pj1 = pl.PJ1;
                //this.Close();
                //return;

                label3.Text = "当前项目:" + pj1.ProjectName + " - " + pj.ProjectName;
                //InitUserControl();
            }

            
        
        }


        private void InitRightControl()
        {
           // string s = "  IsGuiDang!='是' order by CreateDate";
            string s = "  IsGuiDang!='是' order by SortID";
            IList<Project> list = Services.BaseService.GetList<Project>("SelectProjectByWhere", s);
            dt = Itop.Common.DataConverter.ToDataTable((IList)list, typeof(Project));
            this.treeList1.DataSource = dt;
        }

        private void InitSelect()
        {
            if (treeList1.Nodes.Count > 0)
            {
                if (treeList1.Nodes[0].Nodes.Count > 0)
                {
                    treeList1.SetFocusedNode(treeList1.Nodes[0].Nodes[0]);
                }
            
            }
        }
        private void InitSelectControl()
        {
            TreeListNode tln = treeList1.FocusedNode;
            XMGLpj = null;
            if (tln == null)
                return;
            XMGLpj = new Project();
            XMGLpj.UID = tln["UID"].ToString();
            XMGLpj.ProjectName = tln["ProjectName"].ToString();
            if (tln.ParentNode == null)
                return;

            pj = new Project();
            pj.UID = tln["UID"].ToString();
            pj.ProjectName = tln["ProjectName"].ToString();

            pj1 = new Project();
            pj1.UID = tln.ParentNode["UID"].ToString();
            pj1.ProjectName = tln.ParentNode["ProjectName"].ToString();

            MIS.ProgUID = tln["UID"].ToString();

            string labeltext = "";
            labeltext = pj1.ProjectName + " - " + pj.ProjectName;
            if (labeltext.Length > 39)
            {
                label5.Text = labeltext.Substring(0, 35) + "...";

            }

            else
            {
                label5.Text = labeltext;
            }
            label5.Tag = labeltext;

        }


        private void TreeAdd()
        {
            Project obj = new Project();
            obj.UID = Guid.NewGuid().ToString();
            obj.CreateDate = DateTime.Now;
            obj.StartDate = DateTime.Now.Date;
            obj.PlanCompleteDate = DateTime.Now;
            obj.CompleteDate = DateTime.Now;
            obj.QualityDate = DateTime.Now;
            obj.BecomeEffective = DateTime.Now;
            obj.GuiDangTime = DateTime.Now;
            obj.GuiDangName = Itop.Client.MIS.UserNumber;

            //执行添加操作
            using (FrmProjectDialog dlg = new FrmProjectDialog())
            {
                dlg.IsCreate = true;    //设置新建标志
                dlg.Object = obj;
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                dt.Rows.Add(Itop.Common.DataConverter.ObjectToRow(obj, dt.NewRow()));
            }

            //将新对象加入到链表中

        }

        private void TreeAdd1()
        {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
            {
                return;
            }

            if (tln.ParentNode != null)
            {
                MessageBox.Show("请先选择项目！");
                return;
            }
               

            string uid = tln["UID"].ToString();

            Project obj = new Project();
            obj.UID = Guid.NewGuid().ToString();
            obj.CreateDate = DateTime.Now;
            obj.StartDate = DateTime.Now.Date;
            obj.PlanCompleteDate = DateTime.Now;
            obj.CompleteDate = DateTime.Now;
            obj.QualityDate = DateTime.Now;
            obj.BecomeEffective = DateTime.Now;
            obj.GuiDangTime = DateTime.Now;
            obj.ProjectManager = uid;
            obj.GuiDangName = Itop.Client.MIS.UserNumber;

            //执行添加操作
            using (FrmProjectDialog dlg = new FrmProjectDialog(""))
            {
                dlg.IsCreate = true;    //设置新建标志
                dlg.Object = obj;
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                dt.Rows.Add(Itop.Common.DataConverter.ObjectToRow(obj, dt.NewRow()));
            }
            //设置授权模块的权限
            VsmdgroupProg tempvp = new VsmdgroupProg();
            tempvp.Progid = "b9b2acb7-e093-4721-a92f-749c731b016e";
            tempvp.Groupno = "SystemManage";
            tempvp.ProjectUID = obj.UID;
            tempvp.run = "1";
            try
            {
                Services.BaseService.Create("InsertVsmdgroupProgwithvalue", tempvp);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }
        }

        private void TreeEdit()
        {

            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
            {
                return;
            }

            string uid = tln["UID"].ToString();

            Project pj = Services.BaseService.GetOneByKey<Project>(uid);
            //修改卷或项目
            if (tln.ParentNode==null)
            {
                //执行修改操作 项目
                using (FrmProjectDialog dlg = new FrmProjectDialog())
                {
                    dlg.Object = pj;   //绑定副本
                    if (dlg.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    tln.SetValue("ProjectName", dlg.Object.ProjectName);
                    tln.SetValue("ProjectCode", dlg.Object.ProjectCode);
                }

            }
            else
            {
                //执行修改操作 卷
                using (FrmProjectDialog dlg = new FrmProjectDialog(""))
                {
                    dlg.Object = pj;   //绑定副本
                    if (dlg.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    tln.SetValue("ProjectName", dlg.Object.ProjectName);
                    tln.SetValue("ProjectCode", dlg.Object.ProjectCode);
                }

            }

           

        }

        private void TreeDel()
        {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
            {
                return;
            }

            string uid = tln["UID"].ToString();
            if (MsgBox.ShowYesNo("是否删除？") != DialogResult.Yes)
            {
                return;
            }

            //Project pj = Services.BaseService.GetOneByKey<Project>(uid);


            //执行删除操作
            try
            {
                if (tln.Nodes.Count > 0)
                {
                    foreach (TreeListNode tln1 in tln.Nodes)
                    {
                        string uid1 = tln1["UID"].ToString();
                        Services.BaseService.Update("UpdateProjectByGuiDangName", uid1);
                    }
                }


                Services.BaseService.Update("UpdateProjectByGuiDangName", uid);
                treeList1.Nodes.Remove(tln);
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                return;
            }

        }


        

        private void TreeGD()
        {
            FrmProjectGD fp = new FrmProjectGD();
            if (fp.ShowDialog() == DialogResult.OK)
            {
                InitRightControl();

            }
        }

        private void TreeGL()
        {
            if (treeList1.FocusedNode != null)
            {
                if (treeList1.FocusedNode.ParentNode == null)
                {
                    MessageBox.Show("请选择卷宗。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                frmProgLayerManager p = new frmProgLayerManager();
                p.progid = treeList1.FocusedNode["UID"].ToString();
                p.Show();
            }
        }


        private void TreeCopy()
        {
            try
            {
                TreeListNode tln = treeList1.FocusedNode;
                if (tln == null)
                    return;
                TreeListNode tln1 = tln.ParentNode;
                if (tln1 == null)
                    return;
                object result = null;
                object classInstance = null;


                string id = Guid.NewGuid().ToString();
                Assembly asm = Assembly.LoadFrom(Application.StartupPath + "\\Itop.Client.DataCopy.dll");
                Type type = asm.GetType("Itop.Client.DataCopy.ModuleDataCopy", true);


                Type[] ptypes = new Type[2];
                ptypes[0] = typeof(string);
                ptypes[1] = typeof(string);

                object[] paramValues = new object[2];
                paramValues.SetValue(tln["UID"].ToString(), 0);
                paramValues.SetValue(id, 1);


                MethodInfo method = type.GetMethod("CopyData", ptypes);
                if (method != null)
                {
                    ParameterInfo[] paramInfos = method.GetParameters();
                    if (paramInfos.Length == paramValues.Length)
                    {
                        // 参数个数相同才会执行
                        object[] methodParams = new object[paramValues.Length];

                        for (int i = 0; i < paramValues.Length; i++)
                            methodParams[i] = Convert.ChangeType(paramValues[i], paramInfos[i].ParameterType, CultureInfo.InvariantCulture);
                        if (classInstance == null)
                        {
                            classInstance = (method.IsStatic) ? null : Activator.CreateInstance(type);
                        }
                        result = method.Invoke(classInstance, methodParams);
                    }

                }
                if (result.ToString() == "True")
                {
                    string uid = tln1["UID"].ToString();
                    Project obj = new Project();
                    obj.UID = id;
                    obj.ProjectName = tln["ProjectName"].ToString() + "副本";
                    obj.CreateDate = DateTime.Now;
                    obj.StartDate = DateTime.Now.Date;
                    obj.PlanCompleteDate = DateTime.Now;
                    obj.CompleteDate = DateTime.Now;
                    obj.QualityDate = DateTime.Now;
                    obj.BecomeEffective = DateTime.Now;
                    obj.GuiDangTime = DateTime.Now;
                    obj.ProjectManager = uid;
                    obj.GuiDangName = Itop.Client.MIS.UserNumber;
                    Services.BaseService.Create<Project>(obj);
                    dt.Rows.Add(Itop.Common.DataConverter.ObjectToRow(obj, dt.NewRow()));
                    //拷贝后设置授权模块的权限
                    VsmdgroupProg tempvp = new VsmdgroupProg();
                    tempvp.Progid = "b9b2acb7-e093-4721-a92f-749c731b016e";
                    tempvp.Groupno = "SystemManage";
                    tempvp.ProjectUID = id;
                    tempvp.run = "1";
                    try
                    {
                        Services.BaseService.Create("InsertVsmdgroupProgwithvalue", tempvp);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);

                    }
                }

            }
            catch (Exception e1) { MsgBox.Show("错误提示:" + e1.Message); }
        }
        private void ChangeButton(bool istrue)
        {
           
            bbtnadd.Enabled = istrue;

            bbtnAdditem.Enabled = istrue;

            bbtncopy.Enabled = istrue;
            

            bbtngl.Enabled = istrue;
           

            bbtndel.Enabled = istrue;
          

            bbtnRecor.Enabled = istrue;
            

            bbtnEdit.Enabled = istrue;
         
            bbtnup.Enabled = istrue;
            
            bbtndown.Enabled = istrue;
           


            contextMenuStrip1.Items[0].Enabled = istrue;
            contextMenuStrip1.Items[0].Visible = istrue;

            contextMenuStrip1.Items[1].Enabled = istrue;
            contextMenuStrip1.Items[1].Visible = istrue;


            contextMenuStrip1.Items[2].Enabled = istrue;
            contextMenuStrip1.Items[2].Visible = istrue;

            contextMenuStrip1.Items[3].Enabled = istrue;
            contextMenuStrip1.Items[3].Visible = istrue;

            contextMenuStrip1.Items[4].Enabled = istrue;
            contextMenuStrip1.Items[4].Visible = istrue;

            contextMenuStrip1.Items[5].Enabled = istrue;
            contextMenuStrip1.Items[5].Visible = istrue;



            contextMenuStrip1.Items[6].Enabled = istrue;
            contextMenuStrip1.Items[6].Visible = istrue;


            contextMenuStrip1.Items[7].Enabled = istrue;
            contextMenuStrip1.Items[7].Visible = istrue;


            contextMenuStrip1.Items[8].Enabled = istrue;
            contextMenuStrip1.Items[8].Visible = istrue;

            contextMenuStrip1.Items[9].Enabled = istrue;
            contextMenuStrip1.Items[9].Visible = istrue;

        }
        private void 添加项目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeAdd();
        }

        private void 添加卷宗ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeAdd1();
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeEdit();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeDel();
        }

        private void 恢复项目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeGD();
        }

        private void 关联图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeGL();
        }

        private void 拷贝卷宗ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeCopy();
        }
        private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrevMoveNode();
        }

        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NextMoveNode();
        }

        private void initButton()
        {
            VsmdgroupProg smdgroup = new VsmdgroupProg();
            bool isanmin = false;

            string strsql = "Groupno='SystemManage' and Userid='" + MIS.UserNumber + "'";
            IList list = SysService.GetList("SelectSmugroupByWhere", strsql);
            if (list.Count > 0)
            {
                isanmin = true;
            }
            if (XMGLpj == null | isanmin)
            {
                ChangeButton(false | isanmin);
                return;
            }
            smdgroup = MIS.GetProgRight(XMGLGUID, XMGLpj.UID);
            if (smdgroup != null && smdgroup.run == "1")
            {
                ChangeButton(true);

            }
            else
            {
                ChangeButton(false);
            }
        }
        public void UpdateUserState(Project p)
        {
            Smmuser user = SysService.GetOneByKey<Smmuser>(MIS.UserNumber);
            user.ExpireDate = p.ProjectName;
            SysService.Update<Smmuser>(user);
        }
        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            //DevExpress.XtraNavBar.NavBarGroup  lastgroup=null;
            string activegroupname = "";
            if (nbctSystem.ActiveGroup!=null)
            {
                //lastgroup=nbctSystem.ActiveGroup;
                activegroupname = nbctSystem.ActiveGroup.Name;
            }
            if (oneload)
            {
                return;
            }
            InitSelectControl();
            InitProRight();
            initButton();
            if (e.Node == null)
                return;
            if (e.Node.TreeList.GetDataRecordByNode(e.Node) ==null)
                return;
            DataRow row = (e.Node.TreeList.GetDataRecordByNode(e.Node) as DataRowView).Row;
            Project p = DataConverter.RowToObject<Project>(row);
            UpdateUserState(p);


            /////////////////////////////////////
            //if (treeView.SelectedNode == null)
            //{
            //    return;
            //}
            //Smmprog sp = treeView.SelectedNode.Tag as Smmprog;

            list = SysService.GetList<Smmprog>();
            AddMainMenu(list);

            //listViewdown.Items.Clear();
            //label2.Text = treeView.SelectedNode.Text;

            DataRow[] rows = smmprogTable.Select(string.Format("parentid='{0}' and ProgType='{1}'",nbctSystem.ActiveGroup.Name, "f"));


            //foreach (DataRow row2 in rows)
            //{
            //    VsmdgroupProg smdgroup = new VsmdgroupProg();
            //    smdgroup = MIS.GetProgRight(row2["ProgId"].ToString(), MIS.ProgUID);
            //    if (smdgroup != null && Convert.ToInt32(smdgroup.run) > 0)
            //    {

            //        ListViewItem listItem = new ListViewItem();
            //        listItem.Text = row2["ProgName"].ToString();
            //        listItem.Tag = DataConverter.RowToObject<Smmprog>(row2);
            //        listItem.ImageKey = row2["ProgIco"].ToString();
            //        listViewdown.Items.Add(listItem);
            //    }
            //}

          //if (lastgroup!=null)
          //  {
          //    nbctSystem.ActiveGroup=lastgroup;
              
          //    nbctSystem.Refresh();
              
          //  }
            if (activegroupname!="")
            {
                foreach (DevExpress.XtraNavBar.NavBarGroup group in nbctSystem.Groups)
                {
                    if (activegroupname==group.Name)
                    {
                        nbctSystem.ActiveGroup = group;
                        break;
                    }
                }
            }
            InitdownItem();
        }

        private void treeList1_AfterDragNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            string id = e.Node["UID"].ToString();
            string pid = e.Node["ProjectManager"].ToString();
            Project pj = SysService.GetOneByKey<Project>(id);
            pj.ProjectManager = pid;

            SysService.Update<Project>(pj);

            
        }

        private void treeList1_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if ((e.Node == treeList1.FocusedNode && e.Column != treeList1.FocusedColumn) || e.Node == null || e.Column == null) return;
            bool isFocusedCell = (e.Column == treeList1.FocusedColumn && e.Node == treeList1.FocusedNode);
            Brush brush = null;
            Rectangle r = e.Bounds;
            bool ellipse = false;

            string id = e.Node["UID"].ToString();
            Project pj = SysService.GetOneByKey<Project>(id);


            int b =0;
            string a = pj.Address;
            if (a != "")
            {
                b= int.Parse(a);
            }


            if (e.Column.FieldName == "ProjectName" && e.CellValue != null)
            {
                e.Appearance.ForeColor = Color.FromArgb(b);
                //brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.FromArgb(b), Color.FromArgb(b), 180);
            }
            if (brush != null)
            {
                e.Graphics.FillRectangle(brush, r);
                //r.Inflate(-2, 0);
                //if (ellipse)
                //{
                //    bool check = e.Node[5].Equals(true);
                //    Brush ellipseBrush = check ? Brushes.LightGreen : Brushes.LightSkyBlue;
                //    if (isFocusedCell) ellipseBrush = Brushes.Yellow;
                //    e.Graphics.FillEllipse(ellipseBrush, r);
                //}
                //e.Appearance.DrawString(e.Cache, e.CellText, r);
                //if (isFocusedCell)
                //    DevExpress.Utils.Paint.XPaint.Graphics.DrawFocusRectangle(e.Graphics, e.Bounds, SystemColors.WindowText, e.Appearance.BackColor);
                //e.Handled = true;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            TreeAdd();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            TreeAdd1();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            TreeEdit();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            TreeDel();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            TreeGD();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            TreeGL();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            TreeCopy();
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            PrevMoveNode();
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            NextMoveNode();
        }
        //上移
        private void PrevMoveNode()
        {

            if (treeList1.FocusedNode == null)
            {
                return; 
            }

            TreeListNode node = treeList1.FocusedNode;
            int i = 0, sortj = 0, sortj2 = 0;

            if (treeList1.FocusedNode.PrevNode != null)
            {
                string struid = treeList1.FocusedNode.PrevNode["UID"].ToString();
                string struid2 = treeList1.FocusedNode["UID"].ToString();
                if (struid != struid2)
                {
                    sortj = Convert.ToInt32(treeList1.FocusedNode.PrevNode["SortID"]);
                    sortj2 = Convert.ToInt32(treeList1.FocusedNode["SortID"]);
                    if (sortj2 == sortj)
                    {
                        sortj = sortj - 1;
                    }
                    Project pj = SysService.GetOneByKey<Project>(struid);
                    pj.SortID = sortj2;

                   
                    i = dt.Rows.IndexOf(dt.Select("UID='" + pj.UID + "'")[0]);
                    dt.Rows[i]["SortID"] = pj.SortID;
                    SysService.Update<Project>(pj);
                    pj = SysService.GetOneByKey<Project>(struid2);
                    pj.SortID = sortj;

                   
                    i = dt.Rows.IndexOf(dt.Select("UID='" + pj.UID + "'")[0]);
                    dt.Rows[i]["SortID"] = pj.SortID;
                    SysService.Update<Project>(pj);
                    treeList1.Refresh();

                }

            }


        }
        //下移
        private void NextMoveNode()
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }
            TreeListNode node = treeList1.FocusedNode;
            int i = 0, sortj = 0, sortj2 = 0;
            if (treeList1.FocusedNode.NextNode != null)
            {
                if (treeList1.FocusedNode["UID"].ToString() != treeList1.FocusedNode.NextNode["UID"].ToString())
                {
                    sortj = Convert.ToInt32(treeList1.FocusedNode.NextNode["SortID"]);
                    sortj2 = Convert.ToInt32(treeList1.FocusedNode["SortID"]);
                    if (sortj2 == sortj)
                    {
                        sortj = sortj + 1;
                    }
                    Project pj = SysService.GetOneByKey<Project>(treeList1.FocusedNode.NextNode["UID"].ToString());
                    pj.SortID = sortj2;

                   
                    i = dt.Rows.IndexOf(dt.Select("UID='" + pj.UID + "'")[0]);
                    dt.Rows[i]["SortID"] = pj.SortID;
                    SysService.Update<Project>(pj);
                    pj = SysService.GetOneByKey<Project>(treeList1.FocusedNode["UID"].ToString());
                    pj.SortID = sortj;

                   
                    i = dt.Rows.IndexOf(dt.Select("UID='" + pj.UID + "'")[0]);
                    dt.Rows[i]["SortID"] = pj.SortID;
                    SysService.Update<Project>(pj);
                    treeList1.Refresh();

                }

            }

        }

        private void treeList1_BeforeDragNode(object sender, DevExpress.XtraTreeList.BeforeDragNodeEventArgs e)
        {
            if (e.Node.ParentNode == null || !bbtnEdit.Enabled)
            {
                e.CanDrag = false;
                return;
            }
        }
        //缩略图
        private void toolStripMenu_Sl_Click(object sender, EventArgs e)
        {
            listViewdown.View = System.Windows.Forms.View.LargeIcon;
            toolStripMenu_Sl.Checked = true;
            toolStripMenu_PP.Checked = false;
            toolStripMenu_TB.Checked = false;
            toolStripMenu_LB.Checked = false;
            User_Ini.Writue("Setting", UserID + "Select_View", "0");
            
        }
        //平铺
        private void toolStripMenu_PP_Click(object sender, EventArgs e)
        {
            listViewdown.View = System.Windows.Forms.View.Tile;
            toolStripMenu_Sl.Checked = false;
            toolStripMenu_PP.Checked = true;
            toolStripMenu_TB.Checked = false;
            toolStripMenu_LB.Checked = false;
            User_Ini.Writue("Setting", UserID + "Select_View", "1");
        }
        //图标
        private void toolStripMenu_TB_Click(object sender, EventArgs e)
        {
            listViewdown.View = System.Windows.Forms.View.SmallIcon;
            toolStripMenu_Sl.Checked = false;
            toolStripMenu_PP.Checked = false;
            toolStripMenu_TB.Checked = true;
            toolStripMenu_LB.Checked = false;
            User_Ini.Writue("Setting", UserID + "Select_View", "2");
        }
        //列表
        private void toolStripMenu_LB_Click(object sender, EventArgs e)
        {
            listViewdown.View = System.Windows.Forms.View.List;  
            toolStripMenu_Sl.Checked = false;
            toolStripMenu_PP.Checked = false;
            toolStripMenu_TB.Checked = false;
            toolStripMenu_LB.Checked = true;
            User_Ini.Writue("Setting", UserID + "Select_View", "3");
        }
        //大图标
        private void toolStripMenu_Big_Click(object sender, EventArgs e)
        {
            Adapt_WindowsScreen();
            toolStripMenu_Big.Checked = true;
            toolStripMenu_Mid.Checked = false;
            toolStripMenu_Little.Checked = false;
             try
            {
                IList list = SysService.GetList("SelectSmmprogByFormIco", null);
                DataTable dt_list = DataConverter.ToDataTable(list);
                listViewdown.SmallImageList = ImageListRes.GetimageList(image_size_B[1], dt_list);
                listViewdown.LargeImageList = ImageListRes.GetimageList(image_size_B[2], dt_list);
                this.listViewdown.Font = new System.Drawing.Font("宋体", Font_size_B, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            }
            catch (Exception ex)
            {

            }

            try
            {
                IList list = SysService.GetList("SelectSmmprogByMeIco", null);
                DataTable dt_list = DataConverter.ToDataTable(list);
                //treeView.ImageList = ImageListRes.GetimageList(image_size_B[0], dt_list);
                //this.treeView.Font = new System.Drawing.Font("宋体", Font_size_B, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            }
            catch (Exception ex)
            {

            }
            User_Ini.Writue("Setting", UserID + "Select_Type", "0");
            
        }
        //中图标
        private void toolStripMenu_Mid_Click(object sender, EventArgs e)
        {
            Adapt_WindowsScreen();
            toolStripMenu_Big.Checked = false;
            toolStripMenu_Mid.Checked = true;
            toolStripMenu_Little.Checked = false;
             try
            {
                IList list = SysService.GetList("SelectSmmprogByFormIco", null);
                DataTable dt_list = DataConverter.ToDataTable(list);
                listViewdown.SmallImageList = ImageListRes.GetimageList(image_size_M[1], dt_list);
                listViewdown.LargeImageList = ImageListRes.GetimageList(image_size_M[2], dt_list);
                this.listViewdown.Font = new System.Drawing.Font("宋体", Font_size_M, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            }
            catch (Exception ex)
            {

            }

            try
            {
                IList list = SysService.GetList("SelectSmmprogByMeIco", null);
                DataTable dt_list = DataConverter.ToDataTable(list);
                //treeView.ImageList = ImageListRes.GetimageList(image_size_M[0], dt_list);
                //this.treeView.Font = new System.Drawing.Font("宋体", Font_size_M, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            }
            catch (Exception ex)
            {

            }
            User_Ini.Writue("Setting", UserID + "Select_Type", "1");
        }
        //小图标
        private void toolStripMenu_Little_Click(object sender, EventArgs e)
        {
            Adapt_WindowsScreen();
            toolStripMenu_Big.Checked = false;
            toolStripMenu_Mid.Checked = false;
            toolStripMenu_Little.Checked = true;
             try
            {
                IList list = SysService.GetList("SelectSmmprogByFormIco", null);
                DataTable dt_list = DataConverter.ToDataTable(list);
                listViewdown.SmallImageList = ImageListRes.GetimageList(image_size_L[1], dt_list);
                listViewdown.LargeImageList = ImageListRes.GetimageList(image_size_L[2], dt_list);
                this.listViewdown.Font = new System.Drawing.Font("宋体", Font_size_L, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            }
            catch (Exception ex)
            {

            }

            try
            {
                IList list = SysService.GetList("SelectSmmprogByMeIco", null);
                DataTable dt_list = DataConverter.ToDataTable(list);
                //treeView.ImageList = ImageListRes.GetimageList(image_size_L[0], dt_list);
                //this.treeView.Font = new System.Drawing.Font("宋体", Font_size_L, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            }
            catch (Exception ex)
            {

            }

            User_Ini.Writue("Setting", UserID + "Select_Type", "2");
        }

        private void 放大字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeList1.Columns[0].AppearanceCell.Font =new System.Drawing.Font(treeList1.Columns[0].AppearanceCell.Font.FontFamily, treeList1.Columns[0].AppearanceCell.Font.Size + 1F);
            treeList1.RowHeight = Convert.ToInt32(20 * treeList1.Columns[0].AppearanceCell.Font.Size / 10F);
            treeList1.Refresh();
            User_Ini.Writue("Setting", UserID + "FontSize", treeList1.Columns[0].AppearanceCell.Font.Size.ToString());
            
        }

        private void 缩小字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeList1.Columns[0].AppearanceCell.Font =new  System.Drawing.Font(treeList1.Columns[0].AppearanceCell.Font.FontFamily, treeList1.Columns[0].AppearanceCell.Font.Size -1F);
            treeList1.RowHeight = Convert.ToInt32(20 * treeList1.Columns[0].AppearanceCell.Font.Size / 10F);
            treeList1.Refresh();
            User_Ini.Writue("Setting", UserID + "FontSize", treeList1.Columns[0].AppearanceCell.Font.Size.ToString());

        }
        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            //treeView.Height = splitContainer1.Panel1.Height - 30;
            if (Isfristload)
            {
                return;
            }
            User_Ini.Writue("Setting", UserID + "left_width", splitContainer1.SplitterDistance.ToString());

        }

        private void splitContainer3_Panel2_Resize(object sender, EventArgs e)
        {
            if (Isfristload)
            {
                return;
            }
            User_Ini.Writue("Setting", UserID + "down_width", splitContainer3.SplitterDistance.ToString());
            
        }

        private void splitContainer2_Panel2_Resize(object sender, EventArgs e)
        {
            if (Isfristload)
            {
                return;
            }
            User_Ini.Writue("Setting", UserID + "right_width", splitContainer2.SplitterDistance.ToString());
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void nbctSystem_CustomDrawBackground(object sender, DevExpress.XtraNavBar.ViewInfo.CustomDrawObjectEventArgs e)
        {
            e.Appearance.ForeColor = Color.Black;
            e.Appearance.BackColor = Color.Navy;
            e.Appearance.BackColor2 = Color.FromArgb(192, 192, 255);
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void nbctSystem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

            ActiveItem(e.Link.Item);
            
        }

        private void ActiveItem(DevExpress.XtraNavBar.NavBarItem  nbi)
        {
            Smmprog prog = nbi.Tag as Smmprog;
            if (prog == null || string.IsNullOrEmpty(prog.AssemblyName))
                return;

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
                //InitUserControl();
                MIS.SaveLog(prog.ProgName, "关闭" + prog.ProgName);
            }
        }
      

        

        private void nbctSystem_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            
            Smmprog prog = e.Group.Tag as Smmprog;
            if (prog == null)
                return;
            label2.Text = prog.ProgName.ToString();
            if (!string.IsNullOrEmpty(prog.Remark))
            {
                if (prog.Remark.Contains("htm"))
                {
                    SetUrl(prog.Remark.ToString());
                }
            }

            
            
        }
        public void SetUrl(string url)
        {
            webBrowser1.Navigate(Application.StartupPath + "\\flowchart\\" + url);
           
            //this.webBrowser1.Refresh();
        }
        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (!e.Url.ToString().Contains("htm"))
            {
                e.Cancel = true;
                try
                {
                    string tempusee = e.Url.ToString();
                    tempusee = tempusee.Substring(tempusee.LastIndexOf('/') + 1, tempusee.Length - tempusee.LastIndexOf('/') - 1);
                    foreach (DevExpress.XtraNavBar.NavBarItem nbi in nbctSystem.Items)
                    {
                        if (nbi.Caption == tempusee)
                        {
                            ActiveItem(nbi);
                            break;
                        }
                    }

                }
                catch 
                {
                    
                }
                
            }
            
        }
      

        private void webBrowser1_SizeChanged(object sender, EventArgs e)
        {
            HtmlDocument document = this.webBrowser1.Document;
            if (document != null && document.Body != null)
            {
                if (document.Body.ScrollRectangle.Size.Width > webBrowser1.Size.Width || document.Body.ScrollRectangle.Height > webBrowser1.Size.Height)
                {
                    webBrowser1.ScrollBarsEnabled = true;
                }
                else
                {
                    webBrowser1.ScrollBarsEnabled = false;
                }
            }
        }
        //添加项目
        private void bbtnadd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeAdd();
        }
        //添加卷宗
        private void bbtnAdditem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeAdd1(); 
        }
        //修改
        private void bbtnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeEdit();
        }
        //删除
        private void bbtndel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeDel();
        }
        //关联
        private void bbtngl_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeGL();
        }
        //拷贝
        private void bbtncopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeCopy();
        }
        //上移
        private void bbtnup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrevMoveNode();
        }
        //下移
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NextMoveNode();
        }
        //恢复
        private void bbtnRecor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeGD();
        }

     
    }
}
