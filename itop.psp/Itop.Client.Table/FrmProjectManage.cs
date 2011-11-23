using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.Utils;
using System.Collections;
using System.Reflection;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Itop.Client.Chen;
using Itop.Domain.Table;
using Itop.Client.Table;
using Itop.Client.Forecast;
using Itop.Client.Stutistics;
using Itop.Domain.Stutistics;
using Itop.Client.Common;
using Itop.Domain.RightManager;
using Itop.Domain.Layouts;
using Itop.Domain.Graphics;
using DevExpress.XtraEditors.Repository;
namespace Itop.Client.Table
{
    public partial class FrmProjectManage : Itop.Client.Base.FormBase
    {
        //附件专用纺号
        public string FixStr = "hcfl987654321";

        //当前选中项目ID
        private string Proj_ID = "";
        //登录用户id
     
      
        public string UserID = string.Empty;
        public string User_image = string.Empty;
        DataTable ProjDT;
        DataTable dataTable;
        private bool IS_FirstLoad = true;
        private TreeListNode lastEditNode = null;
        private TreeListColumn lastEditColumn = null;
        private string lastEditValue = string.Empty;
        private DataCommon dc = new DataCommon();
        TreeListNode treenode;
        private int typeFlag2 = 1;
        private OperTable oper = new OperTable();
        public Ps_YearRange yAnge = new Ps_YearRange();
        public Ps_YearRange yAngeXs = new Ps_YearRange();
        private bool _isSelect = false;
        bool isdel = false;
        private string CurentID="";
       
        public bool IsSelect
        {
            get { return _isSelect; }
            set { _isSelect = value; }
        }
        private string _title = "";

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private string _unit = "";

        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }
        DevExpress.XtraGrid.GridControl _gcontrol = null;

        public DevExpress.XtraGrid.GridControl Gcontrol
        {
            get { return _gcontrol; }
            set { _gcontrol = value; }
        }
        public FrmProjectManage()
        {
            InitializeComponent();
            //tssl_user.Text = "当前用户：" + UserID;
            tssl_user.Text = string.Format("当前用户：{0} ", MIS.UserName);
            int imageindex;
            if (int.TryParse(MIS.UserLastLogon, out imageindex))
            {
                tssl_useimage.Image = imageList2.Images[imageindex];
            }
            Skin();
            this.ctrlRtfAttachFilesTR1.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(treeList1_FocusedNodeChanged2);
        }

        void treeList1_FocusedNodeChanged2(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (ctrlRtfAttachFilesTR1.RowCount==0)
            {
                pictureBox1.Image = imageList3.Images[1];
            }
            else
            {
                pictureBox1.Image = imageList3.Images[0];
            }
        }

        private void Skin()
        {
           //   DevExpress.LookAndFeel.UserLookAndFeel skin = DevExpress.LookAndFeel.UserLookAndFeel.Default;
           // DevExpress.LookAndFeel.LookAndFeelStyle Style;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;

        }

        public string GetProjectID
        {
            get
            {
                string tempstr="";
                if (treeList_Pro.FocusedNode!=null)
                {
                    tempstr = treeList_Pro.FocusedNode.GetValue("UID").ToString();
                }
                return tempstr;
            }
        }

        bool ISfirstLoad = true;
        private void CreatFJ()
        {
            string conn = " C_UID='" + FixStr + "' and ParentID='0'";
            IList<RtfAttachFiles> list = Services.BaseService.GetList<RtfAttachFiles>("SelectRtfAttachFilesByWhere", conn);
            for (int i = 0; i < list.Count; i++)
            {
                Services.BaseService.Delete<RtfAttachFiles>(list[i]);
            }
            string[]  strary=new string[]  {"规划审查","可行性研究","获取支持文件","初步设计","施工设计","施工","投产运行","移交","其它"};
            for (int i = 0; i < strary.Length; i++)
			{
                RtfAttachFiles temprff=new RtfAttachFiles ();
                temprff.UID=FixStr+"0"+(i+1);
                temprff.C_UID=FixStr;
                temprff.Des=strary[i];
                temprff.ParentID="0";
                temprff.FileByte = new byte[0];
                temprff.CreateDate = DateTime.Now;
                 Services.BaseService.Create<RtfAttachFiles>(temprff);
			}
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
            string conn = " C_UID='" + FixStr + "' and ParentID='0'";
            IList<RtfAttachFiles> list = Services.BaseService.GetList<RtfAttachFiles>("SelectRtfAttachFilesByWhere", conn);
            if (list==null||list.Count<9)
            {
                CreatFJ();
            }
                


            string lasttime = User_Ini.ReadValue("Setting", "LastRreshTime");
            if (lasttime=="")
            {
                barStaticItem1.Caption = "最近更新时间：" + "无更新";
            }
            else
            {
                barStaticItem1.Caption = "最近更新时间：" + lasttime;
            }
           
            InitData_Proj();
            LoadData();
            InitSelect();
            AddItems();
        }
        private void AddItems()
        {
            string conn1 = "Col5='" + GetProjectID + "' and Col4='" + OperTable.tzgs + "'";
            yAnge = oper.GetYearRange(conn1);
            for (int i = yAnge.BeginYear; i <= yAnge.EndYear; i++)
            {
                txtbuildyear.Properties.Items.Add(i.ToString());
                txtbuilded.Properties.Items.Add(i.ToString());
            }
            txtbuildyear.Text = DateTime.Now.Year.ToString();
            txtbuilded.Text = DateTime.Now.AddYears(1).Year.ToString();

            string conn = "ProjectID='" + GetProjectID + "' order by Sort";
            IList<PS_Table_AreaWH> list1 = Common.Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);
            foreach (PS_Table_AreaWH area in list1)
            {
                txtareaname.Properties.Items.Add(area.Title);
            }
            //导线型号
            WireCategory wc = new WireCategory();
            wc.Type = "40";
            IList<WireCategory> list = Common.Services.BaseService.GetList<WireCategory>("SelectWireCategoryList", wc);
            for (int k = 0; k < list.Count; k++)
            {
                txtlintype.Properties.Items.Add(list[k].WireType);
            }
            //网区类型
            IList<string> areatyplist = null;
            if (Common.Services.BaseService.GetList<string>("SelectBuildProDifAreaType", "") != null)
            {
                areatyplist = Common.Services.BaseService.GetList<string>("SelectBuildProDifAreaType", "");

                for (int j = 0; j < areatyplist.Count; j++)
                {
                    txtareatype.Properties.Items.Add(areatyplist[j]);
                }
            }
        }
        private void InitData_Proj()
        {
            string s = "  IsGuiDang!='是' order by SortID";
            IList<Project> list =Services.BaseService.GetList<Project>("SelectProjectByWhere", s);
            
            for (int i = 0; i < list.Count; i++)
            {
                VsmdgroupProg smdgroup = new VsmdgroupProg();
                //授权管理模块的ID为b9b2acb7-e093-4721-a92f-749c731b016e
                //建设项目管理模块的ID1041fd6e-41ea-42df-a9f5-244ab23ce432
                smdgroup = MIS.GetProgRight("1041fd6e-41ea-42df-a9f5-244ab23ce432", list[i].UID, MIS.UserNumber);
                if (Convert.ToInt32(smdgroup.run) == 0 && list[i].ProjectManager != "")
                {
                    list.Remove(list[i]);
                    i--;
                   
                }
            }
            ProjDT = Itop.Common.DataConverter.ToDataTable((IList)list, typeof(Project));
            this.treeList_Pro.DataSource = ProjDT;
            ISfirstLoad = false;
       
        }

        string tong = "", g_col4 = "";
       
        
        public int[] GetYears()
        {
            Ps_YearRange yr = yAnge;
            int[] year = new int[4] { yr.BeginYear, yr.StartYear, yr.FinishYear, yr.EndYear };
            return year;
        }
        public void LoadData_1()
        {
            yAnge = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.tzgs + "'");
            yAngeXs = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.tzgsxs + "'");

        }
        private void RefreshData()  
        {
         Creat_JBtable();
         //Creat_XBtable(CurentID);
        }
        IList<Ps_Table_BuildPro> MainList =null;
        private void Show_JB_Columns()
        {
           
            int m = 0;
            for (int i = 0; i < treeList1.Columns.Count; i++)
            {
                treeList1.Columns[i].VisibleIndex = -1;
            }
            treeList1.Columns["Title"].Caption = "项目名称";
            treeList1.Columns["Title"].Width = 320;
            treeList1.Columns["Title"].MinWidth = 250;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Title"].VisibleIndex = m++;

            treeList1.Columns["FromID"].Caption = "电压等级";
            treeList1.Columns["FromID"].Width = 60;
            treeList1.Columns["FromID"].VisibleIndex = m++;
            treeList1.Columns["FromID"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["FromID"].OptionsColumn.AllowSort = false;


            treeList1.Columns["AreaName"].Caption = "区域";
            treeList1.Columns["AreaName"].Width = 70;
            treeList1.Columns["AreaName"].MinWidth = 50;
            treeList1.Columns["AreaName"].VisibleIndex = m++;
            treeList1.Columns["AreaName"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["AreaName"].OptionsColumn.AllowSort = false;
            

            treeList1.Columns["Col3"].Caption = "建设性质";
            treeList1.Columns["Col3"].Width = 60;
            treeList1.Columns["Col3"].MinWidth = 60;
            treeList1.Columns["Col3"].VisibleIndex = 4;
            treeList1.Columns["Col3"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Col3"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Col3"].VisibleIndex = m++;

            treeList1.Columns["AllVolumn"].Caption = "投资金额";
            treeList1.Columns["AllVolumn"].Width = 80;
            treeList1.Columns["AllVolumn"].MinWidth = 80;
            treeList1.Columns["AllVolumn"].Format.FormatString= "#####################0.##";
            treeList1.Columns["AllVolumn"].Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            treeList1.Columns["AllVolumn"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["AllVolumn"].OptionsColumn.AllowSort = false;
            treeList1.Columns["AllVolumn"].VisibleIndex = m++;

           

            treeList1.Columns["State"].Caption = "项目状态";
            treeList1.Columns["State"].Width = 80;
            treeList1.Columns["State"].MinWidth = 70;
            treeList1.Columns["State"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["State"].OptionsColumn.AllowSort = false;
            treeList1.Columns["State"].VisibleIndex = m++;

            treeList1.Columns["StateTime"].Caption = "当前状态时间";
            treeList1.Columns["StateTime"].Width = 80;
            treeList1.Columns["StateTime"].VisibleIndex = m++;
            treeList1.Columns["StateTime"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["StateTime"].OptionsColumn.AllowSort = false;

            treeList1.Columns["Col1"].Caption = "备注";
            treeList1.Columns["Col1"].Width = 100;
            treeList1.Columns["Col1"].MinWidth = 100;
            treeList1.Columns["Col1"].OptionsColumn.AllowEdit = true;
            treeList1.Columns["Col1"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Col1"].VisibleIndex = m++;
          
            treeList1.Columns["Sort"].SortOrder = SortOrder.Ascending;
        }
        private void Show_XB_Columns()
        {
            
            int m = 0;
            for (int i = 0; i < treeList2.Columns.Count; i++)
            {
                treeList2.Columns[i].VisibleIndex = -1;
            }
            treeList2.Columns["Title"].Caption = "项目名称";
            treeList2.Columns["Title"].Width = 320;
            treeList2.Columns["Title"].MinWidth = 250;
            treeList2.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList2.Columns["Title"].VisibleIndex = m++;

            treeList2.Columns["FromID"].Caption = "电压等级";
            treeList2.Columns["FromID"].Width = 80;
            treeList2.Columns["FromID"].VisibleIndex = m++;
            treeList2.Columns["FromID"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["FromID"].OptionsColumn.AllowSort = false;


            treeList2.Columns["AreaName"].Caption = "区域";
            treeList2.Columns["AreaName"].Width = 90;
            treeList2.Columns["AreaName"].MinWidth = 90;
            treeList2.Columns["AreaName"].VisibleIndex = m++;
            treeList2.Columns["AreaName"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["AreaName"].OptionsColumn.AllowSort = false;

            treeList2.Columns["AreaType"].Caption = "网区分类";
            treeList2.Columns["AreaType"].Width = 90;
            treeList2.Columns["AreaType"].MinWidth = 90;
            treeList2.Columns["AreaType"].VisibleIndex = m++;
            treeList2.Columns["AreaType"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["AreaType"].OptionsColumn.AllowSort = false;


            treeList2.Columns["Col3"].Caption = "建设性质";
            treeList2.Columns["Col3"].Width = 80;
            treeList2.Columns["Col3"].MinWidth = 80;
            treeList2.Columns["Col3"].VisibleIndex = 4;
            treeList2.Columns["Col3"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["Col3"].OptionsColumn.AllowSort = false;
            treeList2.Columns["Col3"].VisibleIndex = m++;

            treeList2.Columns["AllVolumn"].Caption = "投资金额";
            treeList2.Columns["AllVolumn"].Width = 100;
            treeList2.Columns["AllVolumn"].MinWidth = 100;
            treeList2.Columns["AllVolumn"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["AllVolumn"].OptionsColumn.AllowSort = false;
            treeList2.Columns["AllVolumn"].VisibleIndex = m++;


            treeList2.Columns["State"].Caption = "项目状态";
            treeList2.Columns["State"].Width = 80;
            treeList2.Columns["State"].MinWidth = 80;
            treeList2.Columns["State"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["State"].OptionsColumn.AllowSort = false;
            treeList2.Columns["State"].VisibleIndex = m++;

            treeList2.Columns["StateTime"].Caption = "当前状态时间";
            treeList2.Columns["StateTime"].Width = 80;
            treeList2.Columns["StateTime"].VisibleIndex = m++;
            treeList2.Columns["StateTime"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["StateTime"].OptionsColumn.AllowSort = false;

         
            //treeList2.Columns["Col4"].Caption = "变电站/线路";
            //treeList2.Columns["Col4"].Width = 80;
            //treeList2.Columns["Col4"].VisibleIndex = m++;
            //treeList2.Columns["Col4"].OptionsColumn.AllowEdit = false;
            //treeList2.Columns["Col4"].OptionsColumn.AllowSort = false;

            treeList2.Columns["Length"].Caption = "长度";
            treeList2.Columns["Length"].Width = 80;
            treeList2.Columns["Length"].MinWidth = 80;
            treeList2.Columns["Length"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["Length"].OptionsColumn.AllowSort = false;
            treeList2.Columns["Length"].VisibleIndex = m++;

            treeList2.Columns["Linetype"].Caption = "导线型号";
            treeList2.Columns["Linetype"].Width = 80;
            treeList2.Columns["Linetype"].MinWidth = 80;
            treeList2.Columns["Linetype"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["Linetype"].OptionsColumn.AllowSort = false;
            treeList2.Columns["Linetype"].VisibleIndex = m++;

            treeList2.Columns["Volumn"].Caption = "容量";
            treeList2.Columns["Volumn"].Width = 80;
            treeList2.Columns["Volumn"].MinWidth = 80;
            treeList2.Columns["Volumn"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["Volumn"].OptionsColumn.AllowSort = false;
            treeList2.Columns["Volumn"].VisibleIndex = m++;

            treeList2.Columns["Col5"].Caption = "主变台数";
            treeList2.Columns["Col5"].Width = 80;
            treeList2.Columns["Col5"].MinWidth = 80;
            treeList2.Columns["Col5"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["Col5"].OptionsColumn.AllowSort = false;
            treeList2.Columns["Col5"].VisibleIndex = m++;

            treeList2.Columns["BuildYear"].Caption = "开工年限";
            treeList2.Columns["BuildYear"].Width = 100;
            treeList2.Columns["BuildYear"].MinWidth = 100;
            treeList2.Columns["BuildYear"].VisibleIndex = m++;
            treeList2.Columns["BuildYear"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["BuildYear"].OptionsColumn.AllowSort = false;

            treeList2.Columns["BuildEd"].Caption = "竣工年限";
            treeList2.Columns["BuildEd"].Width = 100;
            treeList2.Columns["BuildEd"].MinWidth = 100;
            treeList2.Columns["BuildEd"].VisibleIndex = m++;
            treeList2.Columns["BuildEd"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["BuildEd"].OptionsColumn.AllowSort = false;


            treeList2.Columns["Col2"].Caption = "项目负责人";
            treeList2.Columns["Col2"].Width = 80;
            treeList2.Columns["Col2"].VisibleIndex = m++;

            treeList2.Columns["Stime1"].Caption = "规划审查时间";
            treeList2.Columns["Stime1"].Width = 100;
            treeList2.Columns["Stime1"].MinWidth = 100;
            treeList2.Columns["Stime1"].VisibleIndex = m++;
            treeList2.Columns["Stime1"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["Stime1"].OptionsColumn.AllowSort = false;


            treeList2.Columns["Stime2"].Caption = "可行性研究时间";
            treeList2.Columns["Stime2"].Width = 100;
            treeList2.Columns["Stime2"].MinWidth = 100;
            treeList2.Columns["Stime2"].VisibleIndex = m++;
            treeList2.Columns["Stime2"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["Stime2"].OptionsColumn.AllowSort = false;

            treeList2.Columns["Stime3"].Caption = "获取支持文件时间";
            treeList2.Columns["Stime3"].Width = 100;
            treeList2.Columns["Stime3"].MinWidth = 100;
            treeList2.Columns["Stime3"].VisibleIndex = m++;
            treeList2.Columns["Stime3"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["Stime3"].OptionsColumn.AllowSort = false;

            treeList2.Columns["Stime4"].Caption = "初步设计时间";
            treeList2.Columns["Stime4"].Width = 100;
            treeList2.Columns["Stime4"].MinWidth = 100;
            treeList2.Columns["Stime4"].VisibleIndex = m++;
            treeList2.Columns["Stime4"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["Stime4"].OptionsColumn.AllowSort = false;

            treeList2.Columns["Stime5"].Caption = "施工设计时间";
            treeList2.Columns["Stime5"].Width = 100;
            treeList2.Columns["Stime5"].MinWidth = 100;
            treeList2.Columns["Stime5"].VisibleIndex = m++;
            treeList2.Columns["Stime5"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["Stime5"].OptionsColumn.AllowSort = false;

            treeList2.Columns["Stime6"].Caption = "施工时间";
            treeList2.Columns["Stime6"].Width = 100;
            treeList2.Columns["Stime6"].MinWidth = 100;
            treeList2.Columns["Stime6"].VisibleIndex = m++;
            treeList2.Columns["Stime6"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["Stime6"].OptionsColumn.AllowSort = false;

            treeList2.Columns["Stime7"].Caption = "投产运行时间";
            treeList2.Columns["Stime7"].Width = 100;
            treeList2.Columns["Stime7"].MinWidth = 100;
            treeList2.Columns["Stime7"].VisibleIndex = m++;
            treeList2.Columns["Stime7"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["Stime7"].OptionsColumn.AllowSort = false;

            treeList2.Columns["Stime8"].Caption = "移交时间";
            treeList2.Columns["Stime8"].Width = 100;
            treeList2.Columns["Stime8"].MinWidth = 100;
            treeList2.Columns["Stime8"].VisibleIndex = m++;
            treeList2.Columns["Stime8"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["Stime8"].OptionsColumn.AllowSort = false;



            treeList2.Columns["Col6"].Caption = "可行性投资金额";
            treeList2.Columns["Col6"].Width = 120;
            treeList2.Columns["Col6"].MinWidth = 80;
            treeList2.Columns["Col6"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["Col6"].OptionsColumn.AllowSort = false;
            treeList2.Columns["Col6"].VisibleIndex = m++;

            treeList2.Columns["Col7"].Caption = "初步设计投资金额";
            treeList2.Columns["Col7"].Width = 120;
            treeList2.Columns["Col7"].MinWidth = 80;
            treeList2.Columns["Col7"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["Col7"].OptionsColumn.AllowSort = false;
            treeList2.Columns["Col7"].VisibleIndex = m++;

            treeList2.Columns["Col8"].Caption = "施工设计投资金额";
            treeList2.Columns["Col8"].Width = 120;
            treeList2.Columns["Col8"].MinWidth = 80;
            treeList2.Columns["Col8"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["Col8"].OptionsColumn.AllowSort = false;
            treeList2.Columns["Col8"].VisibleIndex = m++;

            treeList2.Columns["Col9"].Caption = "移交项目投资金额";
            treeList2.Columns["Col9"].Width = 120;
            treeList2.Columns["Col9"].MinWidth = 80;
            treeList2.Columns["Col9"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["Col9"].OptionsColumn.AllowSort = false;
            treeList2.Columns["Col9"].VisibleIndex = m++;


            treeList2.Columns["Col1"].Caption = "备注";
            treeList2.Columns["Col1"].Width = 300;
            treeList2.Columns["Col1"].MinWidth = 300;
            treeList2.Columns["Col1"].OptionsColumn.AllowEdit = true;
            treeList2.Columns["Col1"].OptionsColumn.AllowSort = false;
            treeList2.Columns["Col1"].VisibleIndex = m++;
           
            treeList2.Columns["Sort"].SortOrder = SortOrder.Ascending;
        }
        bool ReLoad = false;
        private void Creat_JBtable()
        {
            ReLoad = true;
            string con = " and ProjectID='" + GetProjectID + "|pro' ";
            IList<string > yearlist=Common.Services.BaseService.GetList<string >("SelectBuildProDifBuildEd", con);
            IList<string> areatypelist = Common.Services.BaseService.GetList<string>("SelectBuildProDifAreaType", con);
            if (MainList!=null)
            {
                MainList.Clear();
            }
           
            for (int i = 0; i < yearlist.Count; i++)
            {
                Ps_Table_BuildPro year = new Ps_Table_BuildPro();
               
                year.ID = Guid.NewGuid().ToString();
                year.Title = yearlist[i] + "年";
                year.Col10 = "no1";
                MainList.Add(year);
                for (int k = 0; k < areatypelist.Count; k++)
                {
                    Ps_Table_BuildPro areaType = new Ps_Table_BuildPro();
                    areaType.ID = Guid.NewGuid().ToString(); ;
                    areaType.Title = areatypelist[k];
                    areaType.ParentID = year.ID;
                    areaType.Col10 = "no2";
                    MainList.Add(areaType);
                    string sqlwhere = tong + "ProjectID='" + GetProjectID + "|pro" + "' and BuildEd='" + yearlist[i] + "' and AreaType='" + areatypelist[k] + "' order by cast(BuildEd as int) asc,cast(FromID as int) desc";
                    IList<Ps_Table_BuildPro> ptblist = Common.Services.BaseService.GetList<Ps_Table_BuildPro>("SelectPs_Table_BuildProByConn", sqlwhere);
                    for (int l = 0; l < ptblist.Count; l++)
                    {
                        areaType.AllVolumn += ptblist[l].AllVolumn;
                        ptblist[l].ParentID = areaType.ID;
                        MainList.Add(ptblist[l]);
                    }
                    year.AllVolumn += areaType.AllVolumn;
                }
            }
            treeList1.DataSource = null;
            treeList1.DataSource = MainList;
            DelJB(treeList1.Nodes);
            treeList1.ExpandAll();
            ReLoad = false;
            Set_Focus(CurentID);


        }
        private void DelJB(TreeListNodes nodes)
        {
            nodes.TreeList.BeginUpdate();
            for (int i = 0; i < nodes.Count; i++)
            {
                TreeListNode node=nodes[i];
                if (node.GetValue("Col10") != null && node.GetValue("Col10").ToString() == "no1"&&!node.HasChildren)
                {

                    node.TreeList.Nodes.Remove(node);
                    i--;
                }
                else 
                {
                    for (int k = 0; k < node.Nodes.Count; k++)
                    {
                        TreeListNode node2  = node.Nodes[k];
                        if (node2.GetValue("Col10") != null && node2.GetValue("Col10").ToString() == "no2" && !node2.HasChildren)
                        {
                            node2.TreeList.Nodes.Remove(node2);
                            k--;
                        }
                    }
                    if (node.GetValue("Col10") != null && node.GetValue("Col10").ToString() == "no1" && !node.HasChildren)
                    {

                        node.TreeList.Nodes.Remove(node);
                        i--;
                    }

                }

            }
            nodes.TreeList.EndUpdate();
        }
        private int tr2count = 0;
        private void Creat_XBtable( string str)
        {
            string con = " ProjectID='" + GetProjectID + "|pro' and ID='" + str + "'";
            IList<Ps_Table_BuildPro> templist = Services.BaseService.GetList<Ps_Table_BuildPro>("SelectPs_Table_BuildProByConn", con);
            treeList2.DataSource = templist;
            tr2count = templist.Count;
            if (treeList2.Nodes.Count!=0)
            {
                treeList2.FocusedNode = treeList2.Nodes[0];
            }

        }
        private void LoadData()
        {
            LoadData_1();
            
           
            string con = tong + "ProjectID='" + GetProjectID + "|pro" + "'";
            listTypes = Common.Services.BaseService.GetList("SelectPs_Table_BuildProByConn", con);
            // CalcYearVol();
            //  AddTotalRow(ref listTypes);
            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Table_BuildPro));
            //dataTable = dc.GetSortTable(dataTable, "Flag", true);
            MainList = Common.Services.BaseService.GetList<Ps_Table_BuildPro>("SelectPs_Table_BuildProByConn", con);

            treeList1.DataSource = dataTable;
            treeList2.DataSource = dataTable;

            Show_JB_Columns();
            Show_XB_Columns();
           
        }
        private IList listTypes;
       
        public void SetValueNull()
        {
            int[] year = GetYears();
            foreach (TreeListNode node in treeList1.Nodes)
            {
                if (node.GetValue("ParentID").ToString() == "0")
                {
                    for (int i = year[1]; i <= year[2]; i++)
                    {
                        node.SetValue("y" + i.ToString(), null);
                    }
                }
            }
        }
        public void CalcYearColumn()
        {
            int[] year = GetYears();
            for (int i = year[0]; i <=year[3]; i++)
            {
                treeList1.Columns["y" + i.ToString()].VisibleIndex = -1;
            }
            //for (int i = year[2] + 1; i <= year[3]; i++)
            //{
            //    treeList1.Columns["y" + i.ToString()].VisibleIndex = -1;
            //}
            //for (int i = year[1]; i <= year[2]; i++)
            //{
            //    treeList1.Columns["y" + i.ToString()].VisibleIndex = -1;

            //}
        }
        List<string> pasteCols = new List<string>();
       
       
        private void InitSelect()
        {
            if (treeList_Pro.Nodes.Count > 0)
            {
                if (treeList_Pro.Nodes[0].Nodes.Count > 0)
                {
                    treeList_Pro.SetFocusedNode(treeList_Pro.Nodes[0].Nodes[0]);
                }

            }
        }
        private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        //临时
        private void barButtonItem19_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ///      old卷编号                             new卷编号

            //37c8c0dc-5c3e-424f-9421-5381f3506bce  a79da376-bd9a-4099-b0fd-7a36989b6ee4 第一卷 河池电网“十二五”规划总报告

            //87367824-3e0f-482e-995c-2abac6a521e3  85c066c7-a4d7-469b-928b-5d9e86280400 第二卷 河池110千伏及以上电网规划

            //5e34b620-234e-43c6-880d-641b1754e5bf  88c1bfdf-da21-4771-955f-e90d5c3213ba 第三卷 河池城市10千伏及以下配电网规划
            //cbb342b0-1359-4a61-ac04-45f930c7b6bc  aeb3cc6a-8245-461d-9baf-c0f3466531e4 第二卷 河池110kV及以上电网规划

            //a299598c-52cd-46cd-a86b-8a9431224635  7a523b70-62de-4a2e-bd6d-69f2b8bf9e30 第三卷 河池市（市区）10kV及以下配电网规划
            //c3766715-2c65-4fdc-92fe-da6368bfa60c  d0265b75-2ede-40f0-ab1c-cf3fb2e3a79f 第四卷（第一册）宜州电网规划
            //94dea378-0602-4182-aa97-1fb3e29a282f  204ab2f4-c70a-4e74-8691-eaab8fa99a76 南丹县车河大厂10kV配网规划
            //4d138991-c0cc-4673-a8f7-9bcc7352be04  a521fd10-9709-4c09-9b9b-4531a0d09b42 第四卷（第一册）金城江区（城郊）电网规划
            //e98bd115-7ada-47da-b033-e0be5c9c8626  e24195c9-92ad-45c9-be2a-ada47f0dbd77 第四卷（第二册）宜州电网规划
            //fd3c794b-fc89-42aa-ad20-01345f5596c2  bbefe1d7-9467-4e00-b6bc-e388f89d6b10 第四卷（第三册）环江电网规划
            //38796b24-2b65-43e3-8c13-6f29ec55efe5  c910e022-9798-42c1-9441-347e485499fd 第四卷（第二册）环江电网规划
            //92b5775a-51a5-4bde-b8eb-a3cd11feddb0  06358d6d-40aa-4da5-ae6f-85e7a24677e7 第四卷（第三册）东兰电网规划
            //ddbe4400-af6a-4172-86f2-be0eeb08090a  31f4b4bb-3483-4426-b48d-f0ed383ed476 第四卷（第四册）东兰电网规划
            //541f1e29-9a37-4487-8c67-b1b8f8df0918  5a1e49e1-5594-4b20-834e-2b1cf743e3c5 第四卷（第五册）大华电网规划
            //33a4955e-ad42-4bd4-91f6-75faa0573beb  309c777b-b542-496c-90c7-b64a6c27147c 第四卷（第六册）罗城电网规划 
            //31006d5e-e511-4fe0-abc7-27d1cc79d904  ef0cb9db-fd97-45be-8b86-2400e32fd335 第五卷 河池电网“十二五”规划主要成果汇报

            //f8f1fc11-b659-4265-ab70-df896b78a080  d4b1c874-db5c-47cc-9880-201458f299a2 河池2009-2013年电网专题规划（审定版）
            //62fa9736-4073-4dfb-b981-1363bacaca94  cfb734b4-7520-4dc2-a6f4-00457294ccfc 河池电网“十二五”规划（送审版）

            Hashtable temphas = new Hashtable();
            temphas.Add("37c8c0dc-5c3e-424f-9421-5381f3506bce", "a79da376-bd9a-4099-b0fd-7a36989b6ee4");
            temphas.Add("87367824-3e0f-482e-995c-2abac6a521e3", "85c066c7-a4d7-469b-928b-5d9e86280400");
            temphas.Add("5e34b620-234e-43c6-880d-641b1754e5bf", "88c1bfdf-da21-4771-955f-e90d5c3213ba");
            temphas.Add("cbb342b0-1359-4a61-ac04-45f930c7b6bc", "aeb3cc6a-8245-461d-9baf-c0f3466531e4");
            temphas.Add("a299598c-52cd-46cd-a86b-8a9431224635", "7a523b70-62de-4a2e-bd6d-69f2b8bf9e30");
            temphas.Add("c3766715-2c65-4fdc-92fe-da6368bfa60c", "d0265b75-2ede-40f0-ab1c-cf3fb2e3a79f");
            temphas.Add("94dea378-0602-4182-aa97-1fb3e29a282f", "204ab2f4-c70a-4e74-8691-eaab8fa99a76");
            temphas.Add("4d138991-c0cc-4673-a8f7-9bcc7352be04", "a521fd10-9709-4c09-9b9b-4531a0d09b42");
            temphas.Add("e98bd115-7ada-47da-b033-e0be5c9c8626", "e24195c9-92ad-45c9-be2a-ada47f0dbd77");
            temphas.Add("fd3c794b-fc89-42aa-ad20-01345f5596c2", "bbefe1d7-9467-4e00-b6bc-e388f89d6b10");
            temphas.Add("38796b24-2b65-43e3-8c13-6f29ec55efe5", "c910e022-9798-42c1-9441-347e485499fd");
            temphas.Add("92b5775a-51a5-4bde-b8eb-a3cd11feddb0", "06358d6d-40aa-4da5-ae6f-85e7a24677e7");
            temphas.Add("ddbe4400-af6a-4172-86f2-be0eeb08090a", "31f4b4bb-3483-4426-b48d-f0ed383ed476");
            temphas.Add("541f1e29-9a37-4487-8c67-b1b8f8df0918", "5a1e49e1-5594-4b20-834e-2b1cf743e3c5");
            temphas.Add("33a4955e-ad42-4bd4-91f6-75faa0573beb", "309c777b-b542-496c-90c7-b64a6c27147c");
            temphas.Add("31006d5e-e511-4fe0-abc7-27d1cc79d904", "ef0cb9db-fd97-45be-8b86-2400e32fd335");
            temphas.Add("f8f1fc11-b659-4265-ab70-df896b78a080", "d4b1c874-db5c-47cc-9880-201458f299a2");
            temphas.Add("62fa9736-4073-4dfb-b981-1363bacaca94", "cfb734b4-7520-4dc2-a6f4-00457294ccfc");
            foreach (string  key in temphas.Keys)
            {
                string sql = " update Ps_History set Col4='" + temphas[key].ToString() + "' where Col4='" + key + "'";
                Common.Services.BaseService.Update("UpdatePs_Table_TZGS_SQL", sql);
                //Ps_History psp_Type = new Ps_History();
                //psp_Type.Forecast = type32;
            }
        }

        private void treeList_Pro_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            RefreshData();
            xtraTabControl1.SelectedTabPageIndex = 0;
            ButtonShow();
        }
        private void ButtonShow()
        {
            VsmdgroupProg smdgroup = new VsmdgroupProg();
            //授权管理模块的ID为b9b2acb7-e093-4721-a92f-749c731b016e
            //建设项目管理模块的ID1041fd6e-41ea-42df-a9f5-244ab23ce432
            smdgroup = MIS.GetProgRight("1041fd6e-41ea-42df-a9f5-244ab23ce432", GetProjectID, MIS.UserNumber);
            bool addright=false;
            bool editright = false;
            bool deleright = false;
            bool seleright = false;

            if (Convert.ToInt32(smdgroup.ins) == 1)
            {
                addright = true;
            }
            if (Convert.ToInt32(smdgroup.upd) == 1)
            {
                editright = true;
            }
            if (Convert.ToInt32(smdgroup.del) == 1)
            {
                deleright = true;
            }
            if (Convert.ToInt32(smdgroup.qry) == 1)
            {
                seleright = true;
            }
            barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //barButtonItem21.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barButtonItem20.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;  
            barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barSubItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

            if (!addright)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem21.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem20.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
             }
             if (!editright)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;         
             }
            if (!deleright)
            {
                barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!seleright)
            {
                barSubItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }
       
        private void treeList_Pro_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (ISfirstLoad)
            {
                return;
            }
                   Brush brush = null;
            Rectangle r = e.Bounds;
            bool ellipse = false;
            int b = 0;
            string a = e.Node["Address"].ToString();
            if (a != "")
            {
                b = int.Parse(a);
            }
            if (e.Column.FieldName == "ProjectName" && e.CellValue != null)
            {
                e.Appearance.ForeColor = Color.FromArgb(b);
            }
            if (brush != null)
            {
                e.Graphics.FillRectangle(brush, r);
            }
        }
        private void Set_Focus( string ID)
        {
            if (ID!=null)
            {
                TreeListNode tempnode = treeList1.FindNodeByKeyID(ID);
                treeList1.FocusedNode = tempnode;
            }
               
        }
        //重新计算父类
        private void Parentnode_JS(TreeListNode node)
        {
            double Length = 0;
            double Volumn = 0;
            double AllVolumn = 0;
          
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                Length += double.Parse(node.Nodes[i].GetValue("Length").ToString());
                Volumn += double.Parse(node.Nodes[i].GetValue("Volumn").ToString());
                AllVolumn += double.Parse(node.Nodes[i].GetValue("AllVolumn").ToString());
            }
            try
            {
                node.TreeList.BeginUpdate();
                node.SetValue("Length", Length);
                node.SetValue("Volumn", Volumn);
                node.SetValue("AllVolumn", AllVolumn);
                node.TreeList.EndUpdate();

            }
            catch (Exception)
            {

                throw;
            }


        }
        //重新计算父类
        private void Parentnode_JS(string ParentID)
        {
            double Length = 0;
            double Volumn = 0;
            double AllVolumn = 0;
            string sql=" ProjectID='" + GetProjectID + "|pro" + "' and ParentID='"+ParentID+"'";
            IList<Ps_Table_BuildPro> ptblist = Common.Services.BaseService.GetList<Ps_Table_BuildPro>("SelectPs_Table_BuildProByConn", sql);

            for (int i = 0; i < ptblist.Count; i++)
            {
                Length += ptblist[i].Length;
                Volumn += ptblist[i].Volumn;
                AllVolumn += ptblist[i].AllVolumn;
            }
            try
            {
                Ps_Table_BuildPro tempptb = Common.Services.BaseService.GetOneByKey<Ps_Table_BuildPro>(ParentID);
                tempptb.Length = Length;
                tempptb.Volumn = Volumn;
                tempptb.AllVolumn = AllVolumn;
                Common.Services.BaseService.Update<Ps_Table_BuildPro>(tempptb);
            }
            catch (Exception)
            {

                throw;
            }


        }
        //添加项目
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
            FrmAddProj frm = new FrmAddProj();
            Ps_Table_BuildPro tempptb = new Ps_Table_BuildPro();
            tempptb.ProjectID = GetProjectID + "|pro";
            frm.ProjectID = GetProjectID;
           
            frm.Currentptb = tempptb;
            if (frm.ShowDialog() == DialogResult.OK)
                {
            try
            {
                    Common.Services.BaseService.Create<Ps_Table_BuildPro>(frm.Currentptb);
                    //Parentnode_JS(ParentID);
                    RefreshData();
                    Set_Focus(tempptb.ID);
                }
            
            catch (Exception ex)
            {
                
                MessageBox.Show("添加项目错误："+ex.Message);
            }
           }

        }
        private static FileINI User_Ini = new FileINI(Application.StartupPath + "\\User.ini");

        //从规划更新数据

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm wait = null;
            wait = new WaitDialogForm("", "正在核查数据, 请稍候...");
           

            //记录规划中有而管理中没有的数
            List<Ps_Table_BuildPro> GHonlylist = new List<Ps_Table_BuildPro>();
            //记录规划和管理中都有但数据不同的项

            List<Ps_Table_BuildPro> Difflist = new List<Ps_Table_BuildPro>();
            //记录管理中有而规划中没有的数据

            List<Ps_Table_BuildPro> MAnageonlylist = new List<Ps_Table_BuildPro>();
            //最后要和并的项
            List<Ps_Table_BuildPro> lastlist = new List<Ps_Table_BuildPro>();

            Hashtable GHlist = new Hashtable();



            string con = tong + "ProjectID='" + GetProjectID + "' and  ParentID!='0' and ( Col4='bian' or Col4='line' or Col4='sbd' ) order by cast(BuildEd as int) asc,cast(FromID as int) desc";
            IList<Ps_Table_BuildPro> list = Common.Services.BaseService.GetList<Ps_Table_BuildPro>("SelectPs_Table_BuildProByConn", con);
            for (int j = 0; j < list.Count; j++)
            {
               
                Ps_Table_BuildPro tempptbgh = list[j];
                GHlist.Add(tempptbgh.ID + "|pro", tempptbgh.ID);
                Ps_Table_BuildPro nowptbgl = Common.Services.BaseService.GetOneByKey<Ps_Table_BuildPro>(tempptbgh.ID + "|pro");
                if (nowptbgl==null)
                {
                    list[j].Sort = 0;
                    list[j].Col10 = "gh";
                    GHonlylist.Add(list[j]);
                }
                else
                {
                    bool ISsame = true;
                    if (list[j].Title!=nowptbgl.Title)
                    {
                        ISsame = false;
                    }
                    if (list[j].Volumn != nowptbgl.Volumn)
                    {
                        ISsame = false;
                    }
                    if (list[j].Col5 != nowptbgl.Col5)
                    {
                        ISsame = false;
                    }
                    if (list[j].Length != nowptbgl.Length)
                    {
                        ISsame = false;
                    }
                    if (list[j].Linetype != nowptbgl.Linetype)
                    {
                        ISsame = false;
                    }
                    if (list[j].AreaName != nowptbgl.AreaName)
                    {
                        ISsame = false;
                    }
                    if (list[j].AreaType != nowptbgl.AreaType)
                    {
                        ISsame = false;
                    }
                    if (list[j].BuildYear != nowptbgl.BuildYear)
                    {
                        ISsame = false;
                    }

                    if (list[j].BuildEd != nowptbgl.BuildEd)
                    {
                        ISsame = false;
                    }
                    if (list[j].FromID != nowptbgl.FromID)
                    {
                        ISsame = false;
                    }
                    if (list[j].AllVolumn != nowptbgl.AllVolumn)
                    {
                        ISsame = false;
                    }
                    if (list[j].Col3 != nowptbgl.Col3)
                    {
                        ISsame = false;
                    }
                    if (!ISsame)
                    {
                        list[j].Sort = 0;
                        list[j].Col10 = "df";
                        Difflist.Add(list[j]);
                    }
                }
            }
            string con2 = " ProjectID='" + GetProjectID + "|pro' and ( Col4='变电站' or Col4='线路' or Col4='送变电' )  order by cast(BuildEd as int) asc,cast(FromID as int) desc";
            IList<Ps_Table_BuildPro> templistgl = Services.BaseService.GetList<Ps_Table_BuildPro>("SelectPs_Table_BuildProByConn", con2);
            for (int k = 0; k < templistgl.Count; k++)
            {
                //规划表中没有则添加到管理专有中

                if (!GHlist.ContainsKey(templistgl[k].ID))
                {
                    templistgl[k].Sort = 0;
                    templistgl[k].Col10 = "gl";
                    MAnageonlylist.Add(templistgl[k]);
                }
            }
            bool Flag = true;
            if (GHonlylist.Count!=0)
            {
                Flag = false;
                Ps_Table_BuildPro tempptb = new Ps_Table_BuildPro();
                tempptb.Sort = 0;
                tempptb.Col10 = "no";
                tempptb.Title = "A类：电网规划中有，项目管理中没有的项目";
                lastlist.Add(tempptb);
                for (int l = 0; l < GHonlylist.Count; l++)
                {
                    GHonlylist[l].ParentID = tempptb.ID;
                    lastlist.Add(GHonlylist[l]);
                }
            }
            if (Difflist.Count != 0)
            {
                Flag = false;
                Ps_Table_BuildPro tempptb = new Ps_Table_BuildPro();
                tempptb.Sort = 0;
                tempptb.Col10 = "no";
                tempptb.Title = "B类：电网规划与项目管理中数据差异项目";
                lastlist.Add(tempptb);
                for (int m = 0; m < Difflist.Count; m++)
                {
                    Difflist[m].ParentID = tempptb.ID;
                    lastlist.Add(Difflist[m]);
                }
            }
            if (MAnageonlylist.Count != 0)
            {
                Flag = false;
                Ps_Table_BuildPro tempptb = new Ps_Table_BuildPro();
                tempptb.Sort = 0;
                tempptb.Col10 = "no";
                tempptb.Title = "C类：项目管理中有，电网规划中没有的项目";
                lastlist.Add(tempptb);
                for (int n = 0; n < MAnageonlylist.Count; n++)
                {
                    MAnageonlylist[n].ParentID = tempptb.ID;
                    lastlist.Add(MAnageonlylist[n]);
                }
            }

            if (Flag)
            {

                wait.Close();
                MessageBox.Show("与建设项目表数据一致，不需要更新!");
                User_Ini.Writue("Setting", "LastRreshTime", DateTime.Now.ToShortDateString());
                barStaticItem1.Caption = "最近更新时间：" + DateTime.Now.ToShortDateString();
                return;
            }
            else
            {
                wait.Close();
               
                FrmProjShow frm = new FrmProjShow();
                frm.ptblist = lastlist;
                if (frm.ShowDialog()==DialogResult.OK)
                {
                    WaitDialogForm wait2 = null;
                    wait2 = new WaitDialogForm("", "正在进行数据处理, 请稍候...");
                    bool HaveRefresh = false;
                    lastlist = frm.ptblist;
                    
                    for (int i = 0; i < lastlist.Count; i++)
                    {
                        wait2.SetCaption( "正在进行数据处理, 请稍候" +((i+1)/lastlist.Count)*100+"%");
                       
                        if (lastlist[i].Col10.ToString()=="gh"&&lastlist[i].Sort==1)
                        {
                            HaveRefresh = true;
                            Ps_Table_BuildPro nowptb = lastlist[i];
                            nowptb.ID = nowptb.ID + "|pro";
                            nowptb.ParentID = "0";
                            if (nowptb.Col4 == "bian")
                            {
                                nowptb.Col4 = "变电站";
                            }
                            else if (nowptb.Col4 == "line")
                            {
                                nowptb.Col4 = "线路";
                            }
                            else if (nowptb.Col4 == "sbd")
                            {
                                nowptb.Col4 = "送变电";
                            }
                            nowptb.ProjectID = GetProjectID + "|pro";
                            nowptb.AllVolumn = Math.Round(nowptb.AllVolumn, 2);
                            nowptb.Volumn = Math.Round(nowptb.Volumn, 2);
                            nowptb.Length = Math.Round(nowptb.Length, 2);
                            nowptb.Col2 = "";
                            nowptb.State = nowptb.State = "规划审查";
                            nowptb.StateTime = DateTime.Now.ToShortDateString();
                            Common.Services.BaseService.Create<Ps_Table_BuildPro>(nowptb);
                        }
                        else if (lastlist[i].Col10.ToString() == "df" && lastlist[i].Sort == 1)
                        {
                            HaveRefresh = true;
                            Ps_Table_BuildPro tempptb = lastlist[i];
                            Ps_Table_BuildPro nowptb = Common.Services.BaseService.GetOneByKey<Ps_Table_BuildPro>(tempptb.ID + "|pro");
                            nowptb.Title = tempptb.Title;
                            nowptb.AreaType = tempptb.AreaType;
                            nowptb.AreaName = tempptb.AreaName;
                            nowptb.AllVolumn = Math.Round(tempptb.AllVolumn, 2);
                            nowptb.Volumn = Math.Round(tempptb.Volumn, 2);
                            nowptb.BuildYear = tempptb.BuildYear;
                            nowptb.BuildEd = tempptb.BuildEd;
                            nowptb.Col3 = tempptb.Col3;
                            nowptb.FromID = tempptb.FromID;
                            nowptb.Length = Math.Round(tempptb.Length, 2);
                            nowptb.Linetype = tempptb.Linetype;
                            nowptb.Col5 = tempptb.Col5;
                            Common.Services.BaseService.Update<Ps_Table_BuildPro>(nowptb);
                        }
                        else if (lastlist[i].Col10.ToString() == "gl" && lastlist[i].Sort == 1)
                        {
                            HaveRefresh = true;
                            //Common.Services.BaseService.DeleteByKey<Ps_Table_BuildPro>(lastlist[i].ID);
                            Common.Services.BaseService.Delete<Ps_Table_BuildPro>(lastlist[i]);
                        }
                    }
                    if (HaveRefresh)
                    {
                        wait2.Close();
                        User_Ini.Writue("Setting", "LastRreshTime", DateTime.Now.ToShortDateString());
                        barStaticItem1.Caption = "最近更新时间：" + DateTime.Now.ToShortDateString();
                        RefreshData();
                        MessageBox.Show("更新已完成！");
                    }
                    else
                    {
                        wait2.Close();
                        MessageBox.Show(" 您未做任何更新选择！");
                    }
                   
                }
                else
                {
                    MessageBox.Show("未做更新！");
                }
            }
        }

     
        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            FrmAddBuild_HC frm = new FrmAddBuild_HC();
            frm.Conn = tong + "ParentID='0' and ProjectID='" + GetProjectID+ "|pro"+ "' and FromID=";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                
                Ps_Table_BuildPro table_yd = new Ps_Table_BuildPro();
                table_yd.ID += "|" + GetProjectID;
                table_yd.Title = frm.ParentName;
                table_yd.ParentID = "0";
                table_yd.Sort = OperTable.GetBuildProMaxSort() + 1;
                table_yd.ProjectID = GetProjectID + "|pro";
                
                table_yd.FromID = frm.GetV;
                try
                {
                    Common.Services.BaseService.Create<Ps_Table_BuildPro>( table_yd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("增加分类出错：" + ex.Message);
                }
                RefreshData();
                Set_Focus(table_yd.ID);
                
            }
        }
        private void FoucsLocation(string id, TreeListNodes tlns)
        {
            foreach (TreeListNode tln in tlns)
            {
                if (tln["ID"].ToString() == id)
                {
                    treeList1.FocusedNode = tln;
                    return;
                }
                FoucsLocation(id, tln.Nodes);
            }

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (currentPTB==null)
            {
                MessageBox.Show("请先选中项目再进行修改");
                return;
            }
           
            FrmAddProj frm = new FrmAddProj();
            //Ps_Table_BuildPro tempptb = focusedNode.TreeList.GetDataRecordByNode(focusedNode) as Ps_Table_BuildPro;
            frm.Text = "修改项目信息";
            frm.ProjectID = GetProjectID;
          
            frm.Currentptb = currentPTB;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    frm.Currentptb.ParentID = "0";

                    Common.Services.BaseService.Update<Ps_Table_BuildPro>(frm.Currentptb);
                    
                    //Parentnode_JS(ParentID);
                    RefreshData();
                    Set_Focus(frm.Currentptb.ID);
                    MessageBox.Show("修改已完成！");
                }

                catch (Exception ex)
                {

                    MessageBox.Show("修改项目错误：" + ex.Message);
                }

            }

        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void FrmProjectManage_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确定要退出系统?", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
        //删除 
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (currentPTB==null)
            {
                return;
            }

            if (MessageBox.Show("删除项目[" + currentPTB.Title+ "]？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {

                    Common.Services.BaseService.Delete<Ps_Table_BuildPro>(currentPTB);

                }
                catch (Exception ew)
                {

                }
                RefreshData();
                
            }  
            
        }
        //统计
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           

        }
      
        private DataTable ResultDataTable(DataTable sourceDataTable, IList<string> listChoosedYears)
        {
            DataTable dtReturn = new DataTable();
            dtReturn = sourceDataTable;
            return dtReturn;
        }
        //把树控件内容按显示顺序生成到DataTable中

        private DataTable ConvertTreeListToDataTable(DevExpress.XtraTreeList.TreeList xTreeList, bool bRemove)
        {
            DataTable dt = new DataTable();
            List<string> listColID = new List<string>();

            listColID.Add("BianInfo");
            dt.Columns.Add("BianInfo", typeof(string));
            listColID.Add("FromID");
            dt.Columns.Add("FromID", typeof(string));
            listColID.Add("BuildYear");
            dt.Columns.Add("BuildYear", typeof(string));

            listColID.Add("AreaName");
            dt.Columns.Add("AreaName", typeof(string));

            listColID.Add("Flag");
            dt.Columns.Add("Flag", typeof(string));

            listColID.Add("BuildEd");
            dt.Columns.Add("BuildEd", typeof(string));

            listColID.Add("Length");
            dt.Columns.Add("Length", typeof(double));
            listColID.Add("Volumn");
            dt.Columns.Add("Volumn", typeof(double));
            listColID.Add("AllVolumn");
            dt.Columns.Add("AllVolumn", typeof(double));
                 listColID.Add("Title");
            dt.Columns.Add("Title", typeof(string));
            dt.Columns["Title"].Caption = "项目名称";
           for (int i = 1; i < 5; i++)
            {
                listColID.Add("Col" + i.ToString());
                dt.Columns.Add("Col" + i.ToString(), typeof(string));
            }
            myN = 1; youN = 0;
            foreach (TreeListNode node in xTreeList.Nodes)
            {
                AddNodeDataToDataTable(dt, node, listColID, bRemove);
            }

            return dt;
        }
        int myN = 1, youN = 0;
        private void AddNodeDataToDataTable(DataTable dt, TreeListNode node, List<string> listColID, bool bRemove)
        {
            DataRow newRow = dt.NewRow();
            foreach (string colID in listColID)
            {
                //分类名，第二层及以后在前面加空格
                if (colID == "Title" && node.ParentNode != null)
                {
                    newRow[colID] = "　　" + node[colID];
                }
                else
                {
                    newRow[colID] = node[colID];
                }
                if (colID == "BianInfo")
                {
                    if (node["ParentID"].ToString() == "0")
                    {
                        myN = 1;
                        newRow["BianInfo"] = OperTable.often[youN];
                        youN++;
                    }
                    else
                    {
                        newRow["BianInfo"] = myN.ToString();
                        myN++;
                    }

                }
            }
            
            dt.Rows.Add(newRow);
            
            foreach (TreeListNode nd in node.Nodes)
            {
                AddNodeDataToDataTable(dt, nd, listColID, bRemove);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ctrlRtfAttachFilesTR1.AddObject();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ctrlRtfAttachFilesTR1.UpdateObject();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            ctrlRtfAttachFilesTR1.DeleteObject();
        }
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                barButtonItem1.Enabled = true;
                barButtonItem21.Enabled = true;
                barButtonItem20.Enabled = true;
                barButtonItem2.Enabled = true;
                barButtonItem9.Enabled = true;
                barSubItem2.Enabled = true;
            }
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            
           
            if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                if (treeList1.Nodes.Count==0)
                {
                    xtraTabControl1.SelectedTabPageIndex = 0;
                    return;
                }
       
                Creat_XBtable(CurentID);
                barButtonItem1.Enabled = true;
                barButtonItem21.Enabled = true;
                barButtonItem20.Enabled = true;
                barButtonItem2.Enabled = true;
                barButtonItem9.Enabled = true;
                barSubItem2.Enabled = true;
                try
                {
                    Ps_Table_BuildPro tempptb = Common.Services.BaseService.GetOneByKey<Ps_Table_BuildPro>(Proj_ID);
                    if (tempptb != null)
                    {
                        tempptb.y1990 = ctrlRtfAttachFilesTR1.RowCount;
                        Common.Services.BaseService.Update<Ps_Table_BuildPro>(tempptb);
                        treeList2.BeginUpdate();
                        if (treeList2.FocusedNode!=null)
                        {
                            treeList2.FocusedNode.SetValue("y1990", ctrlRtfAttachFilesTR1.RowCount);

                        }
                        treeList2.EndUpdate();
                        treeList2.Refresh();
                    }
                }
                catch (Exception)
                {
                    
                    throw;
                }
               
            }
        }


        //简表统计

        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            FrmBuild_TJ frmbt = new FrmBuild_TJ();
            frmbt.treelist = treeList2;
            if (frmbt.ShowDialog() == DialogResult.OK)
            {
                string strcommon = "";
                if (frmbt.sql != "")
                {

                    strcommon =  " and " + frmbt.sql;

                }
                string stryear = "";
                string strareatype="";
                if (frmbt.endyear!="")
                {
                    stryear = " and Cast(BuildEd as int)<=" + frmbt.endyear;
                }
                if (frmbt.areatype!="")
                {
                    strareatype = " and AreaType='" + frmbt.areatype+"'";
                }
                
                string con1 = " and ProjectID='" + GetProjectID + "|pro' "+stryear;
                IList<string> yearlist = Common.Services.BaseService.GetList<string>("SelectBuildProDifBuildEd", con1);
                string con2 = " and ProjectID='" + GetProjectID + "|pro' " + stryear+strareatype;
                IList<string> areatypelist = Common.Services.BaseService.GetList<string>("SelectBuildProDifAreaType", con2);
                if (MainList != null)
                {
                    MainList.Clear();
                }

                for (int i = 0; i < yearlist.Count; i++)
                {
                    Ps_Table_BuildPro year = new Ps_Table_BuildPro();

                    year.ID = Guid.NewGuid().ToString();
                    year.Title = yearlist[i] + "年";
                    year.Col10 = "no1";
                    MainList.Add(year);
                    for (int k = 0; k < areatypelist.Count; k++)
                    {
                        Ps_Table_BuildPro areaType = new Ps_Table_BuildPro();
                        areaType.ID = Guid.NewGuid().ToString(); ;
                        areaType.Title = " "+areatypelist[k];
                        areaType.ParentID = year.ID;
                        areaType.Col10 = "no2";
                        MainList.Add(areaType);
                        string sqlwhere = tong + "ProjectID='" + GetProjectID + "|pro" + "' and BuildEd='" + yearlist[i] + "' and AreaType='" + areatypelist[k] + "' "+strcommon+"  order by cast(BuildEd as int) asc,cast(FromID as int) desc";
                        IList<Ps_Table_BuildPro> ptblist = Common.Services.BaseService.GetList<Ps_Table_BuildPro>("SelectPs_Table_BuildProByConn", sqlwhere);
                        for (int l = 0; l < ptblist.Count; l++)
                        {
                            areaType.AllVolumn += ptblist[l].AllVolumn;
                            areaType.Length += ptblist[l].Length;
                            areaType.Volumn += ptblist[l].Volumn;

                            areaType.Col5 = Delstr(areaType.Col5, ptblist[l].Col5).ToString();
                            areaType.Col6 = Delstr(areaType.Col6, ptblist[l].Col6).ToString();
                            areaType.Col7 = Delstr(areaType.Col7, ptblist[l].Col7).ToString();
                            areaType.Col8 = Delstr(areaType.Col8, ptblist[l].Col8).ToString();
                            areaType.Col9 = Delstr(areaType.Col9, ptblist[l].Col9).ToString();
                            
                            ptblist[l].ParentID = areaType.ID;
                            ptblist[l].Title = "   " + ptblist[l].Title;
                            MainList.Add(ptblist[l]);
                        }
                        year.AllVolumn += areaType.AllVolumn;

                        year.Length += areaType.Length;
                        year.Volumn += areaType.Volumn;

                        year.Col5 = Delstr(year.Col5, areaType.Col5).ToString();
                        year.Col6 = Delstr(year.Col6, areaType.Col6).ToString();
                        year.Col7 = Delstr(year.Col7, areaType.Col7).ToString();
                        year.Col8 = Delstr(year.Col8, areaType.Col8).ToString();
                        year.Col9 = Delstr(year.Col9, areaType.Col9).ToString();
                    }
                }
                treeList1.DataSource = null;
                treeList1.DataSource = MainList;
                DelJB(treeList1.Nodes);
                FrmResultPrintHC2 frm = new FrmResultPrintHC2();
                frm.Title = "项目管理简表统计";
                frm.dt = frmbt.dt;
                frm.treelist = treeList1;
                frm.ShowDialog();
                RefreshData();
            }
        }
        private double Delstr(object a,object b)
        {
            double adob = 0;
            double bdob = 0;
            if (a!=null)
            {
                double.TryParse(a.ToString(), out adob);
            }
            if (b != null)
            {
                double.TryParse(b.ToString(), out bdob);
            }
            return adob + bdob;
        }
        //详细表统计

        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
             string con = " ProjectID='" + GetProjectID + "|pro" + "'";
           
            FrmBuild_TJ frmbt = new FrmBuild_TJ();
            frmbt.treelist = treeList2;
            if (frmbt.ShowDialog() == DialogResult.OK)
            {
                string strcommon = "";
                if (frmbt.sql != "")
                {

                    strcommon = " and " + frmbt.sql;

                }
                string stryear = "";
                string strareatype = "";
                if (frmbt.endyear != "")
                {
                    stryear = " and Cast(BuildEd as int)<=" + frmbt.endyear;
                }
                if (frmbt.areatype != "")
                {
                    strareatype = " and AreaType='" + frmbt.areatype + "'";
                }

                con = con + strcommon + stryear + strareatype+" order by cast(FromID as int) desc,AreaType asc,AreaName asc";
                listTypes = Common.Services.BaseService.GetList("SelectPs_Table_BuildProByConn", con);

              
                treeList2.DataSource = null;
                treeList2.DataSource = listTypes;
                
                FrmResultPrintHC2 frm = new FrmResultPrintHC2();
                frm.Title = "项目管理详细统计";
                frm.treelist = treeList2;
                frm.dt = frmbt.dt;
                frm.ShowDialog();
                Creat_XBtable(CurentID);
            }
        }

        private void treeList2_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            if (e.Node.GetValue("y1990").ToString() == "0")
            {
                e.NodeImageIndex = 1;
            }
            else
            {
                e.NodeImageIndex = 0;
            }
        }
        public string StateTime
        {
            get { return dateStateTime.Value.ToShortDateString(); }
            set
            {
                DateTime temptime = DateTime.Now;
                if (DateTime.TryParse((value == null || value == "") ? temptime.ToString() : value.ToString(), out temptime))
                {

                }
                dateStateTime.Value = temptime;
            }
        }
        private DateTime ChangeTime(string str)
        {
            DateTime temptime = DateTime.Now;
            string result = "";
            if (str == "")
            {

            }
            else if (DateTime.TryParse(str, out temptime))
            {

            }
            return temptime;

        }
      
        Ps_Table_BuildPro currentPTB = null;
        private void Show_Detial()
        {
            ISfirstload = true;
            if (currentPTB!=null)
            {
               
                txtTile.Text = currentPTB.Title;
                txtallvolumn.Text = currentPTB.AllVolumn.ToString();
                sptz0.Text = currentPTB.AllVolumn.ToString();
                txtareaname.Text = currentPTB.AreaName;
                txtareatype.Text = currentPTB.AreaType;
                txtbuilded.Text = currentPTB.BuildEd;
                txtbuildyear.Text = currentPTB.BuildYear;
                txtcol1.Text = currentPTB.Col1;
                txtcol2.Text = currentPTB.Col2;
                txtCol3.Text = currentPTB.Col3;
                txtcol4.Text = currentPTB.Col4;
                txtcol5.Text = currentPTB.Col5;
                sptz1.Text = currentPTB.Col6;
                sptz2.Text = currentPTB.Col7;
                sptz3.Text = currentPTB.Col8;
                sptz4.Text = currentPTB.Col9;
                txtformid.Text = currentPTB.FromID;
                txtlength.Text = currentPTB.Length.ToString();
                txtlintype.Text = currentPTB.Linetype;
                txtstate.Text = currentPTB.State;
                StateTime = currentPTB.StateTime;
                txtvolumn.Text = currentPTB.Volumn.ToString();

                dateTime1.Value = ChangeTime((currentPTB.Stime1 == null || currentPTB.Stime1 == "") ? "" : currentPTB.Stime1);
                textBox1.Text = currentPTB.Stime1;
                if (currentPTB.Stime1 == null || currentPTB.Stime1 == "")
                {
                    textBox1.Text = dateTime1.Value.ToShortDateString();
                }

                dateTime2.Value = ChangeTime((currentPTB.Stime2 == null || currentPTB.Stime2 == "") ? "" : currentPTB.Stime2);
                textBox2.Text = currentPTB.Stime2;
                dateTime3.Value = ChangeTime((currentPTB.Stime3 == null || currentPTB.Stime3 == "") ? "" : currentPTB.Stime3);
                textBox3.Text = currentPTB.Stime3;
                dateTime4.Value = ChangeTime((currentPTB.Stime4 == null || currentPTB.Stime4 == "") ? "" : currentPTB.Stime4);
                textBox4.Text = currentPTB.Stime4;
                dateTime5.Value = ChangeTime((currentPTB.Stime5 == null || currentPTB.Stime5 == "") ? "" : currentPTB.Stime5);
                textBox5.Text = currentPTB.Stime5;
                dateTime6.Value = ChangeTime((currentPTB.Stime6 == null || currentPTB.Stime6 == "") ? "" : currentPTB.Stime6);
                textBox6.Text = currentPTB.Stime6;
                dateTime7.Value = ChangeTime((currentPTB.Stime7 == null || currentPTB.Stime7 == "") ? "" : currentPTB.Stime7);
                textBox7.Text = currentPTB.Stime7;
                dateTime8.Value = ChangeTime((currentPTB.Stime8 == null || currentPTB.Stime8 == "") ? "" : currentPTB.Stime8);
                textBox8.Text = currentPTB.Stime8;
                
               
            }
            else
            {
                txtTile.Text = "";
                txtallvolumn.Text = null;
                txtareaname.Text = "";
                txtareatype.Text = "";
                txtbuilded.Text = "";
                txtbuildyear.Text = "";
                txtcol1.Text = "";
                txtcol2.Text = "";
                txtCol3.Text = "";
                txtcol4.Text = "";
                txtcol5.Text = null;
                sptz0.Text = null;
                sptz1.Text = null;
                sptz2.Text = null;
                sptz3.Text = null;
                sptz4.Text = null;
                txtformid.Text = "";
                txtlength.Text =null;
                txtlintype.Text = "";
                txtstate.Text = "";
                dateStateTime.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                txtstime.Text = "";
                txtvolumn.Text = null;
                txtallvolumn.Text = null;
                pictureBox1.Image = imageList3.Images[1];
            }
            ISfirstload = false;
        }
        //保存修改
        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            if (currentPTB == null)
            {
                MessageBox.Show("请先选中项目再进行修改");
                return;
            }
           
            if (MessageBox.Show("确定要修改项目信息？","询问",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes)
            {
                return;
            }

             currentPTB.Title=txtTile.Text ;
             currentPTB.AllVolumn=double.Parse(txtallvolumn.Text) ;
            //sptz0.Text = currentPTB.AllVolumn.ToString();
             currentPTB.AreaName =txtareaname.Text;
            currentPTB.AreaType=txtareatype.Text;
              currentPTB.BuildEd=txtbuilded.Text;
             currentPTB.BuildYear=txtbuildyear.Text;
             currentPTB.Col1=txtcol1.Text;
            currentPTB.Col2=txtcol2.Text ;
            currentPTB.Col3= txtCol3.Text;
            currentPTB.Col4=txtcol4.Text;
            currentPTB.Col5=txtcol5.Text;
           currentPTB.Col6=sptz1.Text ;
              currentPTB.Col7=sptz2.Text;
             currentPTB.Col8=sptz3.Text;
            currentPTB.Col9=sptz4.Text;
            currentPTB.FromID=txtformid.Text;
            currentPTB.Length=double.Parse( txtlength.Text );
            currentPTB.Linetype=txtlintype.Text;
            currentPTB.State= txtstate.Text;
            currentPTB.StateTime = StateTime;
            currentPTB.Volumn=double.Parse(txtvolumn.Text);
            currentPTB.Stime1 = textBox1.Text;
           
             currentPTB.Stime2=textBox2.Text;
             currentPTB.Stime3=textBox3.Text ;
             currentPTB.Stime4=textBox4.Text ;
            currentPTB.Stime5=textBox5.Text;
             currentPTB.Stime6=textBox6.Text ;
           currentPTB.Stime7= textBox7.Text ;
            currentPTB.Stime8=textBox8.Text ;
            try
            {
                Common.Services.BaseService.Update<Ps_Table_BuildPro>(currentPTB);
                MessageBox.Show("修改已完成！");
                string oldstr=currentPTB.ID;
                RefreshData();
                Set_Focus(oldstr);
            }
            catch (Exception)
            {
                
                throw;
            }
         
                
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            bool tempflag = true;

            if (ReLoad)
            {
                tempflag = false;
            }
            if (treeList1.FocusedNode == null)
            {
                 tempflag = false;
           }
           else
           {
               if (treeList1.FocusedNode.GetValue("Col10") != null)
               {
                   if (treeList1.FocusedNode.GetValue("Col10").ToString() == "no1" || treeList1.FocusedNode.GetValue("Col10").ToString() == "no2")
                   {
                       tempflag = false;
                   }
               }
           }

            if (tempflag)
            {
                CurentID = treeList1.FocusedNode.GetValue("ID").ToString();
                //Creat_XBtable(CurentID);
                currentPTB = treeList1.FocusedNode.TreeList.GetDataRecordByNode(treeList1.FocusedNode) as Ps_Table_BuildPro;
            }
            else
            {
                currentPTB = null;
                CurentID = "";
            }
          
            Show_Detial();
            this.ctrlRtfAttachFilesTR1.Category = CurentID;
            this.ctrlRtfAttachFilesTR1.FixStr = FixStr;
            this.ctrlRtfAttachFilesTR1.RefreshData();
            
        }
        
        private void barButtonItem3_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex==0)
            {
                FrmResultPrintHC2 frm = new FrmResultPrintHC2();
                frm.Title = "项目管理简表";
                frm.treelist = treeList1;
                frm.ShowDialog();
            }
            else if (xtraTabControl1.SelectedTabPageIndex==1)
            {
                FrmResultPrintHC2 frm = new FrmResultPrintHC2();
                frm.Title = "项目管理详细内容";
                frm.treelist = treeList2;
                frm.ShowDialog();
            }
          
           

        }
        private void dateTime1_ValueChanged(object sender, EventArgs e)
        {
            if (ISfirstload)
            {
                return;
            }
            textBox1.Text = dateTime1.Value.ToShortDateString();
            currentPTB.Stime1 = textBox1.Text;
        }

        private void dateTime2_ValueChanged(object sender, EventArgs e)
        {
            if (ISfirstload)
            {
                return;
            }
            textBox2.Text = dateTime2.Value.ToShortDateString();
            currentPTB.Stime2 = textBox2.Text;
        }

        private void dateTime3_ValueChanged(object sender, EventArgs e)
        {
            if (ISfirstload)
            {
                return;
            }
            textBox3.Text = dateTime3.Value.ToShortDateString();
            currentPTB.Stime3 = textBox3.Text;
        }

        private void dateTime4_ValueChanged(object sender, EventArgs e)
        {
            if (ISfirstload)
            {
                return;
            }
            textBox4.Text = dateTime4.Value.ToShortDateString();
            currentPTB.Stime4 = textBox4.Text;
        }

        private void dateTime5_ValueChanged(object sender, EventArgs e)
        {
            if (ISfirstload)
            {
                return;
            }
            textBox5.Text = dateTime5.Value.ToShortDateString();
            currentPTB.Stime5 = textBox5.Text;
        }

        private void dateTime6_ValueChanged(object sender, EventArgs e)
        {
            if (ISfirstload)
            {
                return;
            }
            textBox6.Text = dateTime6.Value.ToShortDateString();
            currentPTB.Stime6 = textBox6.Text;
        }

        private void dateTime7_ValueChanged(object sender, EventArgs e)
        {
            if (ISfirstload)
            {
                return;
            }
            textBox7.Text = dateTime7.Value.ToShortDateString();
            currentPTB.Stime7 = textBox7.Text;
        }

        private void dateTime8_ValueChanged(object sender, EventArgs e)
        {
            if (ISfirstload)
            {
                return;
            }
            textBox8.Text = dateTime8.Value.ToShortDateString();
            currentPTB.Stime8 = textBox8.Text;
        }

        private void txtcol4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtcol4.Text == "线路")
            {
                txtlength.Text = currentPTB.Length.ToString();
                txtlintype.Text = currentPTB.Linetype;
                txtcol5.Text = currentPTB.Col5;
                txtvolumn.Text = currentPTB.Volumn.ToString();


                txtlength.Enabled = true;
                
                txtlintype.Enabled = true;

                txtvolumn.Enabled = false;
                txtcol5.Enabled = false;

                txtvolumn.Value = 0;
                txtcol5.Value = 0;
                

            }
            if (txtcol4.Text == "变电站")
            {

                txtlength.Text = currentPTB.Length.ToString();
                txtlintype.Text = currentPTB.Linetype;
                txtcol5.Text = currentPTB.Col5;
                txtvolumn.Text = currentPTB.Volumn.ToString();


                txtlength.Enabled = false;
                txtlintype.Enabled = false;

                txtvolumn.Enabled = true;
                txtcol5.Enabled = true;
                txtlintype.Text = "";
                txtlength.Value = 0;
                

            }
            if (txtcol4.Text == "送变电")
            {
                txtlength.Text = currentPTB.Length.ToString();
                txtlintype.Text = currentPTB.Linetype;
                txtcol5.Text = currentPTB.Col5;
                txtvolumn.Text = currentPTB.Volumn.ToString();

                txtlength.Enabled = true;
                txtlintype.Enabled = true;

                txtvolumn.Enabled = true;
                txtcol5.Enabled = true;
            }
        }
        bool zdfalg = false;

        private void txtallvolumn_EditValueChanged(object sender, EventArgs e)
        {
            if (!zdfalg)
            {
                zdfalg = true;
                sptz0.Value = txtallvolumn.Value;
                zdfalg = false;
            }
           
        }

        private void sptz0_EditValueChanged(object sender, EventArgs e)
        {
            if (!zdfalg)
            {
                zdfalg = true;
                txtallvolumn.Value = sptz0.Value;
                zdfalg = false;
            }
            
        }

        private void dateStateTime_ValueChanged(object sender, EventArgs e)
        {
            if (ChangeStatetime)
            {
                return;
            }
            txtstime.Text = dateStateTime.Value.ToShortDateString();

            switch (txtstate.Text)
            {
                case "规划审查":
                    dateTime1.Value = dateStateTime.Value;
                    break;
                case "可行性研究":
                    dateTime2.Value = dateStateTime.Value;
                    break;
                case "获取支持文件":
                    dateTime3.Value = dateStateTime.Value;
                    break;
                case "初步设计":
                    dateTime4.Value = dateStateTime.Value;
                    break;
                case "施工设计":
                    dateTime5.Value = dateStateTime.Value;
                    break;
                case "施工":
                    dateTime6.Value = dateStateTime.Value;
                    break;
                case "投产运行":
                    dateTime7.Value = dateStateTime.Value;
                    break;
                case "移交":
                    dateTime8.Value = dateStateTime.Value;
                    break;
                default:
                    break;
            }


        }
        bool ISfirstload = false;
        bool ChangeStatetime = false;
        private void txtstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ISfirstload)
            {

                ChangeStatetime = true;
                switch (txtstate.Text)
                {
                    case "规划审查":
                        
                        dateStateTime.Value = ChangeTime((currentPTB.Stime1 == null || currentPTB.Stime1 == "") ? "" : currentPTB.Stime1);
                        currentPTB.Stime1 = dateStateTime.Value.ToShortDateString();
                       
                        break;
                    case "可行性研究":
                        dateStateTime.Value = ChangeTime((currentPTB.Stime2 == null || currentPTB.Stime2 == "") ? "" : currentPTB.Stime2);
                        currentPTB.Stime2 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "获取支持文件":
                        dateStateTime.Value = ChangeTime((currentPTB.Stime3 == null || currentPTB.Stime3 == "") ? "" : currentPTB.Stime3);
                        currentPTB.Stime3 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "初步设计":
                        dateStateTime.Value = ChangeTime((currentPTB.Stime4 == null || currentPTB.Stime4 == "") ? "" : currentPTB.Stime4);
                        currentPTB.Stime4 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "施工设计":
                        dateStateTime.Value = ChangeTime((currentPTB.Stime5 == null || currentPTB.Stime5 == "") ? "" : currentPTB.Stime5);
                        currentPTB.Stime5 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "施工":
                        dateStateTime.Value = ChangeTime((currentPTB.Stime6 == null || currentPTB.Stime6 == "") ? "" : currentPTB.Stime6);
                        currentPTB.Stime6 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "投产运行":
                        dateStateTime.Value = ChangeTime((currentPTB.Stime7 == null || currentPTB.Stime7 == "") ? "" : currentPTB.Stime7);
                        currentPTB.Stime7 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "移交":
                        dateStateTime.Value = ChangeTime((currentPTB.Stime8 == null || currentPTB.Stime8 == "") ? "" : currentPTB.Stime8);
                        currentPTB.Stime8 = dateStateTime.Value.ToShortDateString();
                        break;
                    default:
                        break;
                }
                txtstime.Text = dateStateTime.Value.ToShortDateString();
                ChangeStatetime = false;
            }
        }
        private string[] que = new string[60] { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", 
            "十一","十二","十三","十四","十五","十六","十七","十八","十九","二十","二十一","二十二","二十三","二十四","二十五","二十六","二十七",
            "二十八","二十九","三十","三十一","三十二","三十三","三十四","三十五","三十六","三十七","三十八","三十九","四十","四十一","四十二","四十三","四十四",
            "四十五","四十六","四十七","四十八","四十九","五十","五十一","五十二","五十三","五十四","五十五","五十六","五十七","五十八","五十九","六十"};

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            if (ctrlRtfAttachFilesTR1.RowCount==0)
            {
                return;
            }
            DataTable tempdt = new DataTable();
            tempdt.Columns.Add("Des",typeof(string));
             tempdt.Columns.Add("FileName",typeof(string));
             tempdt.Columns.Add("FileType",typeof(string));
             tempdt.Columns.Add("FileSize",typeof(string));
             tempdt.Columns.Add("CreateDate",typeof(string));
             for (int i = 0; i < ctrlRtfAttachFilesTR1.treeList1.Nodes.Count; i++)
			{
                DataRow temrow = tempdt.NewRow();
                temrow["Des"] = que[i]+"."+ctrlRtfAttachFilesTR1.treeList1.Nodes[i].GetValue("Des");
                tempdt.Rows.Add(temrow);
                for (int k = 0; k < ctrlRtfAttachFilesTR1.treeList1.Nodes[i].Nodes.Count; k++)
                {
                    DataRow temrow2 = tempdt.NewRow();
                    temrow2["Des"] ="   "+ (k+1) + "." + ctrlRtfAttachFilesTR1.treeList1.Nodes[i].Nodes[k].GetValue("Des");
                    temrow2["FileName"] = ctrlRtfAttachFilesTR1.treeList1.Nodes[i].Nodes[k].GetValue("FileName");
                    temrow2["FileType"] = ctrlRtfAttachFilesTR1.treeList1.Nodes[i].Nodes[k].GetValue("FileType");
                    temrow2["FileSize"] = ctrlRtfAttachFilesTR1.treeList1.Nodes[i].Nodes[k].GetValue("FileSize");
                    temrow2["CreateDate"] = ctrlRtfAttachFilesTR1.treeList1.Nodes[i].Nodes[k].GetValue("CreateDate");
                    tempdt.Rows.Add(temrow2);
                }
               
			}

            FrmResultPrintHC2 frm = new FrmResultPrintHC2();
            frm.Title = "附件信息";
            frm.UseDataFalg = true;
            frm.dtsouse = tempdt;
            frm.treelist = ctrlRtfAttachFilesTR1.treeList1;
            frm.ShowDialog();
        }

       
      

       
      
    }
}