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
        //�������
        public bool Execute()
        {
            ShowDialog();
            return true;
        }
        #region  Treeview���

        private void CreateView() {
            TreeNode node = treeView1.Nodes.Add("system", "ϵͳ�û�����", 1,1);
            node.Nodes.Add("user", "�û�", 2,2);
            node.Nodes.Add("group", "��", 3,3);
            node.Expand(); 
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e) {
            string key = e.Node.Name;
            if (key == "user") {
                userListView1.ShowType = ShowType.User;
                //tsbRights.Visible = false;
                tsbRights.Text = "��ɫ";

            } else if(key=="group") {
                userListView1.ShowType = ShowType.Group;
                //tsbRights.Visible = true;
                tsbRights.Text = "��Ȩ";
            }
        }

        #endregion
        //����
        private void tsbAdd_Click(object sender, EventArgs e) {
            userListView1.Add();                       
        }
        //�޸�
        private void tsbEdit_Click(object sender, EventArgs e) {
            userListView1.Edit(); 
        }
        //ɾ��
        private void tsbDel_Click(object sender, EventArgs e) {
            userListView1.Delete();  
        }
        //ˢ��
        private void tsbRefresh_Click(object sender, EventArgs e) {
            userListView1.Retrieve();
        }
        //�˳�
        private void tsbClose_Click(object sender, EventArgs e) {
            this.Close();
        }
        //Ȩ��
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