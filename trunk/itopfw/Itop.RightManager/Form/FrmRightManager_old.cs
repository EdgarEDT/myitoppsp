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

namespace Itop.RightManager.UI {
    public partial class FrmRightManager_old : Itop.Client.Base.FormBase
    {
        public FrmRightManager_old() {
            InitializeComponent();  
            CreateView();
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            userListView1.ShowType = ShowType.User;
        }
        //调用入口
        public bool Execute()
        {
            ShowDialog();
            return true;
        }
        #region  Treeview相关

        private void CreateView() {
            TreeNode node = treeView1.Nodes.Add("system", "系统用户和组", 1,1);
            node.Nodes.Add("user", "用户", 2,2);
            node.Nodes.Add("group", "组", 3,3);
            node.Expand(); 
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e) {
            string key = e.Node.Name;
            if (key == "user") {
                userListView1.ShowType = ShowType.User;
                //tsbRights.Visible = false;
                tsbRights.Text = "角色";

            } else if(key=="group") {
                userListView1.ShowType = ShowType.Group;
                //tsbRights.Visible = true;
                tsbRights.Text = "授权";
            }
        }

        #endregion
        //增加
        private void tsbAdd_Click(object sender, EventArgs e) {
            userListView1.Add();                       
        }
        //修改
        private void tsbEdit_Click(object sender, EventArgs e) {
            userListView1.Edit(); 
        }
        //删除
        private void tsbDel_Click(object sender, EventArgs e) {
            userListView1.Delete();  
        }
        //刷新
        private void tsbRefresh_Click(object sender, EventArgs e) {
            userListView1.Retrieve();
        }
        //退出
        private void tsbClose_Click(object sender, EventArgs e) {
            this.Close();
        }
        //权限
        private void tsbRights_Click(object sender, EventArgs e) {

            if (project.Count > 0)
            {
                userListView1.RightSetup(project[0].UID,project[0].ProjectName);
            }
            else
            {
                userListView1.RightSetup("","");
            }
        }

        private void FrmRightManager_Load(object sender, EventArgs e)
        {

        }      
        
    }
}