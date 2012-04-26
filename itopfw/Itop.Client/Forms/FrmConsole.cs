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
        //�Ƿ��һ������
        bool Isfristload = true;
        DataTable dt = new DataTable();
        string UserID = MIS.UserNumber;
        //�жϵ�ǰ�û��Ƿ��ǹ���Ա
        private bool IsSystemUser()
        {
            bool isadmin = false;
            //ϵͳ����Ա
            string strsql = "Groupno='SystemManage' and Userid='" + MIS.UserNumber + "'";
            IList list = SysService.GetList("SelectSmugroupByWhere", strsql);
            if (list.Count > 0)
            {
                isadmin = true;
            }
            return isadmin;
        }
        FileIni User_Ini = new FileIni(Application.StartupPath + "\\User.ini");
        #region ���췽��
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

        static string XMGLGUID = "6a743afa-f166-48f1-92ea-fed929e22cee";//��Ŀ���ڹ���UID
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
        
        #region �ֶ�
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
                if (sysService == null) MsgBox.Show("IBaseService����û��ע��");
                return sysService;
            }
        }
        #endregion

        #region ��������

        public void InitProject()
        { }

        bool oneload = false;
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
        //��¼��ʾ��ģʽ�������ҿ�ܾ���
        private int Select_View = 0;
        private int Select_Type = 1;
        private int left_width = 200;
        private int down_width = 229;
        private int right_width = 260;
        private float FontSize = 9F;
        //�����û�ϰ�߼�¼
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

        public void InitData()
        {
            oneload = true;
            InitIco();
           
            InitRightControl();
            oneload = false;
           
            InitSelectControl();
         
            list = SysService.GetList<Smmprog>();
            if (list != null && list.Count > 0)
            {
                smmprogTable = DataConverter.ToDataTable(list, typeof(Smmprog));
            }


        }

        //��ȡͼ��
        private void InitForm()
        {
            picMenu.Image = Itop.Client.Resources.ImageListRes.GetLeftPhoto();
            this.BackgroundImage = Itop.Client.Resources.ImageListRes.GetBannerPhoto();
            InitIco();
        }
        //����ͼ�꼰���ִ�С
        private void InitIco()
        {
            try
            {
                IList list = SysService.GetList("SelectSmmprogByFormIco", null);
                DataTable dt_list = DataConverter.ToDataTable(list);
                listViewdown.SmallImageList = ImageListRes.GetimageList(30, dt_list);
                listViewdown.LargeImageList = ImageListRes.GetimageList(24, dt_list);
                this.listViewdown.Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
            catch (Exception ex)
            {

            }

            try
            {
                IList list = SysService.GetList("SelectSmmprogByMeIco", null);
                DataTable dt_list = DataConverter.ToDataTable(list);
            }
            catch (Exception ex)
            {

            }
     
        }
        #endregion

        #region �¼�����
        private void FrmConsole_Load(object sender, EventArgs e)
        {
            InitForm();
            MIS.MFrmConsole = this;
            Del_User_ini();
            Isfristload = false;
            InitData();
            InitdownItem();
        }
        //��ӵײ��̶�����������
        private void InitdownItem()
        {
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
       
        private void timer_Tick(object sender, EventArgs e)
        {
            labTime.Text = DateTime.Now.ToString("yyyy��MM��dd��  HHʱmm��ss��");
        }
        private void labExit_Click(object sender, EventArgs e)
        {
            Itop.Client.Login.UserLogoutCommand.Execute();
        }
        private void labAbout_Click(object sender, EventArgs e)
        {
            //�򿪹��ڴ���
            using (FrmAbout dlg = new FrmAbout())
            {
                dlg.ShowDialog();
            }
        }
        #endregion

        //����
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
                MsgBox.Show("�Ҳ���ָ�����ļ�help.chm��");
            }

        }
        //��֤���ĵ������㹻�Ŀ������ʾ
        private void FrmConsole_Resize(object sender, EventArgs e)
        {

            if (this.splitContainer1.SplitterDistance < 190)
            {
                this.splitContainer1.SplitterDistance = 220;
            }
            
        }
        //����ײ����
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
                    MsgBox.Show("����Ȩ�����");
                    return;
                }

                if (int.Parse(smdgroup.run) <= 0)
                {
                    MsgBox.Show("����Ȩ�����");
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
            //��ʼ����׼����
            Itop.Common.MethodInvoker.Execute(prog.AssemblyName, prog.ClassName, "InitData", para, ref classInstance);

            para = new object[0];

            if (Itop.Common.MethodInvoker.Execute(prog.AssemblyName, prog.ClassName, prog.MethodName, para, ref classInstance) != null)
            {
                //InitUserControl();
                MIS.SaveLog(prog.ProgName, "�ر�" + prog.ProgName);
            }
        }
        //����
        private void label4_Click(object sender, EventArgs e)
        {
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
            //��ʼ����׼����
            Itop.Common.MethodInvoker.Execute("Itop.Client.Layouts.dll", "Itop.Client.Layouts.FrmFiles", "InitData", para, ref classInstance);
            para = new object[0];
            if (Itop.Common.MethodInvoker.Execute("Itop.Client.Layouts.dll", "Itop.Client.Layouts.FrmFiles", "Execute", para, ref classInstance) != null)
            {
                int dd=1;
                dd++;
            }
        }
      
        private void InitRightControl()
        {
            string s = "  IsGuiDang!='��' order by SortID";
            IList<Project> list = Services.BaseService.GetList<Project>("SelectProjectByWhere", s);
            dt = Itop.Common.DataConverter.ToDataTable((IList)list, typeof(Project));
            //������Ŀ�û�
            //��ϵͳ����Ա

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
            
            this.treeList1.DataSource = dt;
        }
        private void InitSelectControl()
        {
            TreeListNode tln = treeList1.FocusedNode;
         
            if (tln == null)
                return;
            
            if (tln.ParentNode == null)
                return;

            pj = new Project();
            pj.UID = tln["UID"].ToString();
            pj.ProjectName = tln["ProjectName"].ToString();

            pj1 = new Project();
            pj1.UID = tln.ParentNode["UID"].ToString();
            pj1.ProjectName = tln.ParentNode["ProjectName"].ToString();

            MIS.ProgUID = tln["UID"].ToString();
            MIS.ProgName = tln["ProjectName"].ToString();
            MIS.ProgUserID = tln["GuiDangName"].ToString();

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
        //���Ŀ¼
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

            //ִ����Ӳ���
            using (FrmProjectDialog dlg = new FrmProjectDialog())
            {
                dlg.IsCreate = true;    //�����½���־
                dlg.Object = obj;
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                dt.Rows.Add(Itop.Common.DataConverter.ObjectToRow(obj, dt.NewRow()));
            }

            //���¶�����뵽������

        }
        //��Ӿ�
        private void TreeAdd1()
        {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
            {
                return;
            }

            if (tln.ParentNode != null)
            {
                MessageBox.Show("����ѡ����Ŀ��");
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

            //ִ����Ӳ���
            using (FrmProjectDialog dlg = new FrmProjectDialog(""))
            {
                dlg.IsCreate = true;    //�����½���־
                dlg.Object = obj;
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                dt.Rows.Add(Itop.Common.DataConverter.ObjectToRow(obj, dt.NewRow()));
            }
            //������Ȩģ���Ȩ��
            VsmdgroupProg tempvp = new VsmdgroupProg();
            
            tempvp.Progid = "b9b2acb7-e093-4721-a92f-749c731b016e";
            tempvp.Groupno = "SystemManage";

            IList<Smugroup> listUsergroup = Services.BaseService.GetList<Smugroup>("SelectSmugroupByWhere", "Userid='" + MIS.UserNumber + "'");
            if (listUsergroup.Count > 0)
            {
                tempvp.Groupno = listUsergroup[0].Groupno;
            }

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
        //�༭��
        private void TreeEdit()
        {

            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
            {
                return;
            }

            string uid = tln["UID"].ToString();

            Project pj = Services.BaseService.GetOneByKey<Project>(uid);
            //�޸ľ����Ŀ
            if (tln.ParentNode==null)
            {
                //ִ���޸Ĳ��� ��Ŀ
                using (FrmProjectDialog dlg = new FrmProjectDialog())
                {
                    dlg.Object = pj;   //�󶨸���
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
                //ִ���޸Ĳ��� ��
                using (FrmProjectDialog dlg = new FrmProjectDialog(""))
                {
                    dlg.Object = pj;   //�󶨸���
                    if (dlg.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    tln.SetValue("ProjectName", dlg.Object.ProjectName);
                    tln.SetValue("ProjectCode", dlg.Object.ProjectCode);
                }

            }

           

        }
        //ɾ����
        private void TreeDel()
        {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
            {
                return;
            }
            if (!IsSystemUser())
            {
                MsgBox.Show("����Ȩɾ��Ŀ¼��");
                return;
            }
            if (tln.Nodes.Count>0)
            {
                MsgBox.Show("��Ŀ¼��Ϊ�գ�����ɾ��������ɾ����Ŀ¼�µ���Ŀ��");
                return;
            }
            string uid = tln["UID"].ToString();
            if (MsgBox.ShowYesNo("�Ƿ�ɾ����") != DialogResult.Yes)
            {
                return;
            }

            


            //ִ��ɾ������
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
        //�ָ���Ŀ��
        private void TreeGD()
        {
            FrmProjectGD fp = new FrmProjectGD();
            if (fp.ShowDialog() == DialogResult.OK)
            {
                InitRightControl();

            }
        }
        //����ͼ��
        private void TreeGL()
        {
            if (treeList1.FocusedNode != null)
            {
                if (treeList1.FocusedNode.ParentNode == null)
                {
                    MessageBox.Show("��ѡ����ڡ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                frmProgLayerManager p = new frmProgLayerManager();
                p.progid = treeList1.FocusedNode["UID"].ToString();
                p.Show();
            }
        }
        //��������
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
                        // ����������ͬ�Ż�ִ��
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
                    obj.ProjectName = tln["ProjectName"].ToString() + "����";
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
                    //������������Ȩģ���Ȩ��
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
            catch (Exception e1) { MsgBox.Show("������ʾ:" + e1.Message); }
        }
        //��Ŀ�û�
        private void ProjUser()
        {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
            {
                return;
            }
            if (tln.ParentNode==null)
            {
                return;
            }

            string uid = tln["UID"].ToString();
            string userid=tln["GuiDangName"].ToString();
            frmProjUser frm = new frmProjUser(uid, userid);
            frm.ShowDialog();
        }

        //�ı䰴ť״̬
        private void ChangeButton(bool istrue)
        {
            
            bool cnnrun = false;
            //ѡ��ΪĿ¼
            if (treeList1.FocusedNode.ParentNode==null)
            {
                // ����Ա
                
                SetBarButtonEnabled(bbtnadd, true);
                SetBarButtonEnabled(bbtnAdditem, true);
                SetBarButtonEnabled(bbtnEdit, true);
                SetBarButtonEnabled(bbtndel, istrue);
                SetBarButtonEnabled(bbtngl, false);
                SetBarButtonEnabled(bbtnRecor, false);
                SetBarButtonEnabled(bbtncopy, false);
                SetBarButtonEnabled(bbtnuser, false);

                SetMeunEnabled(0, true);
                SetMeunEnabled(1, true);
                SetMeunEnabled(2, true);
                SetMeunEnabled(3, istrue);
                SetMeunEnabled(4, false);
                SetMeunEnabled(5, false);
                SetMeunEnabled(6, false);
                SetMeunEnabled(7, false);
               
            }
            //ѡ��Ϊ��Ŀ
            else
            {
                // ����Ա
                if (istrue)
                {
                    SetBarButtonEnabled(bbtnadd, true);
                    SetBarButtonEnabled(bbtnAdditem, true);
                    SetBarButtonEnabled(bbtnEdit, true);
                    SetBarButtonEnabled(bbtndel, true);
                    SetBarButtonEnabled(bbtngl, true);
                    SetBarButtonEnabled(bbtnRecor, true);
                    SetBarButtonEnabled(bbtncopy, true);
                    SetBarButtonEnabled(bbtnuser, true);

                    SetMeunEnabled(0, true);
                    SetMeunEnabled(1, true);
                    SetMeunEnabled(2, true);
                    SetMeunEnabled(3, true);
                    SetMeunEnabled(4, true);
                    SetMeunEnabled(5, true);
                    SetMeunEnabled(6, true);
                    SetMeunEnabled(7, true);
                }
                else
                {
                    //��Ŀ�����ˣ����û������ֲ�����
                    if (treeList1.FocusedNode["GuiDangName"].ToString()== MIS.UserNumber)
                    {
                        SetBarButtonEnabled(bbtnadd, true);
                        SetBarButtonEnabled(bbtnAdditem, false);
                        SetBarButtonEnabled(bbtnEdit, true);
                        SetBarButtonEnabled(bbtndel, true);
                        SetBarButtonEnabled(bbtngl, false);
                        SetBarButtonEnabled(bbtnRecor, false);
                        SetBarButtonEnabled(bbtncopy, true);
                        SetBarButtonEnabled(bbtnuser, true);

                        SetMeunEnabled(0, true);
                        SetMeunEnabled(1, false);
                        SetMeunEnabled(2, true);
                        SetMeunEnabled(3, true);
                        SetMeunEnabled(4, false);
                        SetMeunEnabled(5, false);
                        SetMeunEnabled(6, true);
                        SetMeunEnabled(7, true);
                    }
                    else
                    {

                        SetBarButtonEnabled(bbtnadd, true);
                        SetBarButtonEnabled(bbtnAdditem, false);
                        SetBarButtonEnabled(bbtnEdit, false);
                        SetBarButtonEnabled(bbtndel, false);
                        SetBarButtonEnabled(bbtngl, false);
                        SetBarButtonEnabled(bbtnRecor, false);
                        SetBarButtonEnabled(bbtncopy, false);
                        SetBarButtonEnabled(bbtnuser, false);

                        SetMeunEnabled(0, true);
                        SetMeunEnabled(1, false);
                        SetMeunEnabled(2, false);
                        SetMeunEnabled(3, false);
                        SetMeunEnabled(4, false);
                        SetMeunEnabled(5, false);
                        SetMeunEnabled(6, false);
                        SetMeunEnabled(7, false);
                    }


                }

               
            }

        }

        private void SetBarButtonEnabled(DevExpress.XtraBars.BarButtonItem bar, bool can)
        {
            if (can)
            {
                bar.Visibility= DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                bar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            bar.Enabled = can;
        }
        private void SetMeunEnabled(int index, bool can)
        {
            contextMenuStrip1.Items[index].Enabled = can;
            contextMenuStrip1.Items[index].Visible = can;
        }
        

        private void �����ĿToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeAdd();
        }

        private void ��Ӿ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeAdd1();
        }

        private void �޸�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeEdit();
        }

        private void ɾ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeDel();
        }

        private void �ָ���ĿToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeGD();
        }

        private void ����ͼ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeGL();
        }

        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeCopy();
        }

        private void ��Ŀ�û�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjUser();
        }

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrevMoveNode();
        }

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NextMoveNode();
        }

        //ȷ����ť�Ƿ���ã�ֻ��ϵͳ���������Ա��Ȩ����Ŀ�;����
        private void initButton()
        {

            bool isadmin = IsSystemUser();
            ChangeButton(isadmin);
         
        }
        //�����û�������״̬
        public void UpdateUserState(Project p)
        {
            Smmuser user = SysService.GetOneByKey<Smmuser>(MIS.UserNumber);
            user.ExpireDate = p.ProjectName;
            SysService.Update<Smmuser>(user);
        }
        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

            if (treeList1.FocusedNode==null)
            {
                return;
            }
            initButton();
            if (treeList1.FocusedNode.Nodes.Count==0&&treeList1.FocusedNode.ParentNode==null)
            {
                return;
            }
            string activegroupname = "";
            if (nbctSystem.ActiveGroup!=null)
            {
               
                activegroupname = nbctSystem.ActiveGroup.Name;
            }
            InitSelectControl();
         
            
            if (e.Node == null)
                return;
            if (e.Node.TreeList.GetDataRecordByNode(e.Node) ==null)
                return;
            DataRow row = (e.Node.TreeList.GetDataRecordByNode(e.Node) as DataRowView).Row;
            Project p = DataConverter.RowToObject<Project>(row);
            UpdateUserState(p);
            list = SysService.GetList<Smmprog>();
            AddMainMenu(list);
            DataRow[] rows = smmprogTable.Select(string.Format("parentid='{0}' and ProgType='{1}'",nbctSystem.ActiveGroup.Name, "f"));
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
        //�϶����
        private void treeList1_AfterDragNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            string id = e.Node["UID"].ToString();
            string pid = e.Node["ProjectManager"].ToString();
            Project pj = SysService.GetOneByKey<Project>(id);
            pj.ProjectManager = pid;
            SysService.Update<Project>(pj);

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

        //����
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
        //����
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

        private void nbctSystem_CustomDrawBackground(object sender, DevExpress.XtraNavBar.ViewInfo.CustomDrawObjectEventArgs e)
        {
            e.Appearance.ForeColor = Color.Black;
            e.Appearance.BackColor = Color.Navy;
            e.Appearance.BackColor2 = Color.FromArgb(192, 192, 255);
        }

        private void nbctSystem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

            ActiveItem(e.Link.Item);
            
        }
        //����ർ������ģ��
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
                    MsgBox.Show("����Ȩ�����");
                    return;
                }

                if (int.Parse(smdgroup.run) <= 0)
                {
                    MsgBox.Show("����Ȩ�����");
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
            //��ʼ����׼����
            Itop.Common.MethodInvoker.Execute(prog.AssemblyName, prog.ClassName, "InitData", para, ref classInstance);

            para = new object[0];

            if (Itop.Common.MethodInvoker.Execute(prog.AssemblyName, prog.ClassName, prog.MethodName, para, ref classInstance) != null)
            {
                //InitUserControl();
                MIS.SaveLog(prog.ProgName, "�ر�" + prog.ProgName);
            }
        }
        //��ǰ���������ı�
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
        //������ҳ�ĵ�ַ
        public void SetUrl(string url)
        {
            webBrowser1.Navigate(Application.StartupPath + "\\flowchart\\" + url);

        }
        //��ҳ����ת��ʱ
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
      
        //��ҳ��С�����ı�ʱ
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
        //�����Ŀ
        private void bbtnadd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeAdd();
        }
        //��Ӿ���
        private void bbtnAdditem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeAdd1(); 
        }
        //�޸�
        private void bbtnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeEdit();
        }
        //ɾ��
        private void bbtndel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeDel();
        }
        //����
        private void bbtngl_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeGL();
        }
        //����
        private void bbtncopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeCopy();
        }
        //����
        private void bbtnup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrevMoveNode();
        }
        //����
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NextMoveNode();
        }
        //�ָ�
        private void bbtnRecor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeGD();
        }
        //�û�
        private void bbtnuser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProjUser();
        }


        private void treeList1_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            if (e.Node.ParentNode == null)
            {
                e.Node.StateImageIndex = 1;

            }
            else
            {
                e.Node.StateImageIndex = 0;
            }
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


            int b = 0;
            string a = pj.Address;
            if (a != "")
            {
                b = int.Parse(a);
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

       

      
       

     
    }
}
