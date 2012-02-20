using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Server.Interface;
using Itop.Domain.RightManager;
using Itop.Common;
using System.Collections;
using System.IO;
using System.Configuration;
using Itop.Client.Resources;
using Itop.Client.Base;
namespace Itop.RightManager.UI { 
    public partial class FrmGroupRights : FormBase {
        public FrmGroupRights() {
            InitializeComponent();  
            
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            if (projectUID == "")
            {
                moduleType = "0";
            }
            CreateView();
        }
        //调用入口
        public bool Execute()
        {
            ShowDialog();
            return true;
        }
        private IBaseService smmprogService;

        public IBaseService SmmprogService {
            get {
                if (smmprogService == null) {
                    smmprogService = RemotingHelper.GetRemotingService<IBaseService>();
                }
                if (smmprogService == null) MsgBox.Show("IBaseService服务没有注册");
                return smmprogService;
            }
        }
        private string groupno;
        private string projectUID;
        private string projectName;
        private string moduleType="0";
        /// <summary>
        /// 组号
        /// </summary>
        public string Groupno {
            get { return groupno; }
            set { groupno = value; }
        }

        public string ProjectUID
        {
            get { return projectUID; }
            set { projectUID = value; }
        }

        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }

        #region  Treeview相关

        private void CreateView()
        {
            
                IList list = SmmprogService.GetList("SelectSmmprogByMeIco", null);
              
                DataTable dt_list = DataConverter.ToDataTable(list);
                treeView1.ImageList = ImageListRes.GetimageList(16, dt_list);
                treeView1.ImageList.Images.Add("Icsclient",imageList1.Images[1]);

           

            TreeNode node = treeView1.Nodes.Add("", "系统功能目录");
          
            node.ImageKey = "Icsclient";
            node.SelectedImageKey = "Icsclient";
            VsmdgroupProg data = new VsmdgroupProg();
            data.Groupno = groupno;
            data.ProgModuleType = moduleType;
            if(projectUID==string.Empty)
                projectUID="";
            data.ProjectUID = projectUID;
            

            try {
            SmmprogService.Create<VsmdgroupProg>(data);
            
            }
            catch(Exception e) {}

            smmprogTable = DataConverter.ToDataTable(SmmprogService.GetList("SelectSmmprogByModuleType", moduleType), typeof(Smmprog));
            smmprogTable.DefaultView.Sort = "index";

            //smdprogTable=DataConverter.ToDataTable(smmprogService.GetList("SelectVsmdgroupProgList",groupno),typeof(VsmdgroupProg));

            VsmdgroupProg vsmdgroupProg = new VsmdgroupProg();
            vsmdgroupProg.Groupno = groupno;
            vsmdgroupProg.ProgModuleType = moduleType;
            vsmdgroupProg.ProjectUID = projectUID;
            if (projectUID == "")
            {
                smdprogTable = DataConverter.ToDataTable(smmprogService.GetList("SelectVsmdgroupProgByModuleType", vsmdgroupProg), typeof(VsmdgroupProg));
            }
            else
            {
                smdprogTable = DataConverter.ToDataTable(smmprogService.GetList("SelectVsmdgroupProgByProject", vsmdgroupProg), typeof(VsmdgroupProg));
            }
           //除去那些在回收站内的模块
            for (int i = 0; i <smdprogTable.Rows.Count; i++)
            {
                if (smdprogTable.Rows[i]["ParentId"].ToString() == "a5a6aa87-d87b-48ec-b58d-d05a0ea1c8ee" || smdprogTable.Rows[i]["ProgId"].ToString() == "a5a6aa87-d87b-48ec-b58d-d05a0ea1c8ee")
                {
                    smdprogTable.Rows.Remove(smdprogTable.Rows[i]);
                    i--;
                }
            }
           
           
            smdprogTable.DefaultView.Sort = "index";
            groupRightsList1.dataGridView1.DataSource = smdprogTable;

            //////if(projectUID!="")
           ExpandNode(node, string.Empty);

           // node.Expand();
            treeView1.ExpandAll();
        }
        DataTable smdprogTable;
        DataTable smmprogTable;
        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e) {
            if (e.Node.Name != string.Empty) {
                e.Node.Nodes.Clear();
                ExpandNode(e.Node, e.Node.Name);
                e.Node.Name = string.Empty;
            }
        }

        private void ExpandNode(TreeNode parentNode, string parentid) {
            DataRow[] rows = smmprogTable.Select(string.Format("parentid='{0}' and progname<>'{1}' and ProgType='m'", parentid, "-"));
            foreach (DataRow row in rows) {
              
                
                
                
                TreeNode node = parentNode.Nodes.Add(row["progname"].ToString());
                node.Tag = DataConverter.RowToObject<Smmprog>(row);
                node.Name = row["progid"].ToString();
                node.ImageKey = row["ProgIco"].ToString();
                node.SelectedImageKey = row["ProgIco"].ToString();
                node.Nodes.Add("");
              
            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent==null)
            {
                smdprogTable.DefaultView.RowFilter = "";
            }
            else
            {
                Smmprog prog = e.Node.Tag as Smmprog;
                if (prog != null) 
                {               
                    smdprogTable.DefaultView.RowFilter=string.Format("parentid='{0}'", prog.ProgId);
                }
            }
            
        }
        #endregion
       

      
        //保存
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            treeView1.Focus();
            ArrayList list = new ArrayList();
            bool bl = true;
            try
            {
                bl = bool.Parse(System.Configuration.ConfigurationSettings.AppSettings["RightModule"]);
            }
            catch { }
            foreach (DataRow row in smdprogTable.GetChanges().Rows)
            {
                //

                // if(row["progname"].ToString()=="8aa3924f-fb51-49da-8cfa-26f79acb02b8")



                VsmdgroupProg vp = DataConverter.RowToObject<VsmdgroupProg>(row);

                if (vp.Progid == "8aa3924f-fb51-49da-8cfa-26f79acb02b8" && !bl)
                {
                    vp.ins = "0";
                    vp.upd = "0";
                    vp.del = "0";

                }

                list.Add(vp);
            }
            smmprogService.Update<VsmdgroupProg>(list);
            if (projectUID != "")
            {
                Itop.Client.MIS.MFrmConsole.InitProject();
            }
            else
            {
                Itop.Client.MIS.MFrmConsole.InitData();
            }
            MsgBox.Show("保存成功！");
        }

        private void FrmGroupRights_Load(object sender, EventArgs e)
        {
            //改变datagridveiw背景色
            this.groupRightsList1.dataGridView1.DefaultCellStyle.BackColor = this.BackColor;
            this.groupRightsList1.dataGridView1.RowHeadersDefaultCellStyle.BackColor=this.BackColor;
            this.groupRightsList1.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor=this.BackColor;
            this.groupRightsList1.dataGridView1.GridColor = System.Drawing.Color.White;
            


            if (projectUID != "")
            {
                this.Text = projectName + "授权管理";
            }
            else
            {
                this.Text = "系统权限授权管理";
            }
        }
        //刷新
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            treeView1.Nodes.Clear();
            CreateView();
        }
        //退出
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

  
        
    }
}