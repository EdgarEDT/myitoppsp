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
using Itop.Client;

namespace Itop.RightManager.UI {
    public partial class FrmUserManager : Itop.Client.Base.FormBase
    {
        public FrmUserManager()
        {
            InitializeComponent();
            this.Text = "�û��������";
            CreateView();
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            //userListView1.ShowType = ShowType.User;
        }
        ISmmuserService userService;
        //����
        public ISmmuserService UserService
        {
            get
            {
                if (userService == null)
                {
                    userService = Itop.Common.RemotingHelper.GetRemotingService<ISmmuserService>();
                }
                return userService;
            }
        }
        IBaseService groupService;
        //����
        public IBaseService GroupService
        {
            get
            {
                if (groupService == null)
                {
                    groupService = Itop.Common.RemotingHelper.GetRemotingService<IBaseService>();
                }
                return groupService;
            }
        }
        //�������
        public bool Execute()
        {
            ShowDialog();
            return true;
        }
        #region  Treeview���
        int rootimage = 49;
        int groupimage = 48;
        int un_group = 50;
        private void CreateView() {
            treeView1.Nodes.Clear();
            TreeNode node = treeView1.Nodes.Add("system", "ϵͳ�û�����", rootimage, rootimage);
            node.Tag="��";
            IList<Smmgroup> list = GroupService.GetStrongList<Smmgroup>();
            for (int i = 0; i < list.Count; i++)
			{
                TreeNode tempnode = node.Nodes.Add(list[i].Groupno, list[i].Groupname, groupimage, groupimage);
                 tempnode.Tag="��";
                 IList<Smugroup> listUser = GroupService.GetList<Smugroup>("SelectSmugroupByWhere", "Groupno='" + list[i].Groupno + "'");
                 for (int j = 0; j < listUser.Count; j++)
		         {
                     Smmuser user = new Smmuser();
                     user.Userid = listUser[j].Userid;
                    Smmuser tempuser= GroupService.GetOneByKey<Smmuser>(user);
                     //tempuser.Lastlogon ����û�ͼ��id
                    if (tempuser!=null)
                    {
                        int useimage = 0;
                        if (int.TryParse(tempuser.Lastlogon,out useimage))
                        {
                            useimage = int.Parse(tempuser.Lastlogon.ToString());
                        }
                        TreeNode tempnodeuser = tempnode.Nodes.Add(listUser[j].Userid, tempuser.UserName, useimage, useimage);
                        tempnodeuser.Tag = "�û�";
                    }
    			   
		         }

			}
            // ��δ�����û���ӽ�һ����
            IList<Smmuser> list_user = GroupService.GetStrongList<Smmuser>();
            TreeNode un_groupnode = new TreeNode();
            un_groupnode.Name = "δ����";
            un_groupnode.Text = "δ�����û�";
            un_groupnode.ImageIndex = un_group;
            un_groupnode.SelectedImageIndex = un_group;
            un_groupnode.Tag = "δ����";
            bool un_groupuser = false;
            for (int k = 0; k < list_user.Count; k++)
            {
                string tempuserno = list_user[k].Userid;
                IList<Smugroup> listUsergroup = GroupService.GetList<Smugroup>("SelectSmugroupByWhere", "Userid='" + tempuserno + "'");
                if (listUsergroup.Count==0)
                {
                    un_groupuser = true;
                    int useimage = 0;
                    if (int.TryParse(list_user[k].Lastlogon, out useimage))
                    {
                        useimage = int.Parse(list_user[k].Lastlogon.ToString());
                    }
                    TreeNode tempnodeuser = un_groupnode.Nodes.Add(list_user[k].Userid, list_user[k].UserName, useimage, useimage);
                    tempnodeuser.Tag = "�û�";

                }

            }
            if (un_groupuser)
            {
                node.Nodes.Add(un_groupnode);
            }
            node.ExpandAll();
            Btn_Show();
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e) 
        {
            //string key = e.Node.Name;
            //if (key == "user") {
            //    userListView1.ShowType = ShowType.User;
            //    //tsbRights.Visible = false;
            //    tsbRights.Text = "��ɫ";

            //} else if(key=="group") {
            //    userListView1.ShowType = ShowType.Group;
            //    //tsbRights.Visible = true;
            //    tsbRights.Text = "��Ȩ";
            //}
        }

        #endregion
        private void Btn_Show()
        {
            DevExpress.XtraBars.BarItemVisibility Canall= DevExpress.XtraBars.BarItemVisibility.Never;
             string currentuser = MIS.UserNumber;
         
            if (currentuser == "admin" )
            {
                Canall = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            barButtonItem1.Visibility = Canall;
            barButtonItem2.Visibility = Canall;
            barButtonItem4.Visibility = Canall;
           
        }
        //�޸�
        private void tsbEdit_Click(object sender, EventArgs e) 
        {
            if (treeView1.SelectedNode==null)
            {
                return;
            }
            if (treeView1.SelectedNode.Tag=="��")
            {
                EditGroup();
            }
            else if (treeView1.SelectedNode.Tag=="�û�")
            {
                EditUser();
            }
        }
        //ɾ��
        private void tsbDel_Click(object sender, EventArgs e) 
        {
            if (treeView1.SelectedNode == null)
            {
                return;
            }
            if (treeView1.SelectedNode.Tag == "��")
            {
                DeleteGroup();
            }
            else if (treeView1.SelectedNode.Tag == "�û�")
            {
                DeleteUser();
            }
        }
        //ˢ��
        private void tsbRefresh_Click(object sender, EventArgs e) {
            CreateView();
        }
        //�˳�
        private void tsbClose_Click(object sender, EventArgs e) {
            this.Close();
        }
        //Ȩ��
        private void tsbRights_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                return;
            }
            if (treeView1.SelectedNode.Tag == "��")
            {
                RightSetup();
            }
            else 
            {
                MessageBox.Show("��ѡ������ٽ�����Ȩ");
            }
           
        }
        ///// <summary>
        ///// ��Ȩ
        ///// </summary>
        //public void RightSetup()
        //{
        //    FrmProjectSelect frm = new FrmProjectSelect();
        //    frm.groupno = treeView1.SelectedNode.Name;
        //    frm.groupname = treeView1.SelectedNode.Text;
        //    frm.ShowDialog();
           
        //}

        /// <summary>
        /// ��Ȩ
        /// </summary>
        public void RightSetup()
        {
            FrmProjectSelect frm = new FrmProjectSelect();
            frm.groupno = treeView1.SelectedNode.Name;
            frm.groupname = treeView1.SelectedNode.Text;
            frm.ShowDialog();

            FrmGroupRights dlg = new FrmGroupRights();
            dlg.Groupno = treeView1.SelectedNode.Name; ;
            dlg.ProjectUID = MIS.ProgUID;
            dlg.ProjectName = MIS.ProgName;
            dlg.ShowDialog();

        }
        
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddGroup();
        }
        /// <summary>
        /// ������
        /// </summary>
        private void AddGroup()
        {
            using (FrmSmmgroupEdit dlg = new FrmSmmgroupEdit())
            {

                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    GroupService.Create<Smmgroup>(dlg.DataObject);
                    CreateView();
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            AddUser();
        }
        /// <summary>
        /// �����û�
        /// </summary>
        private void AddUser()
        {
            string currentuser = MIS.UserNumber;
            using (FrmSmmuserEdit_new dlg = new FrmSmmuserEdit_new())
            {
                dlg.currentuser = currentuser;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    
                    UserService.Create(dlg.DataObject);
                    CreateView();
                }
            }
        }
        /// <summary>
        /// �޸��û�
        /// </summary>
        private void EditUser()
        {

            string currentuser = MIS.UserNumber;
            string id = treeView1.SelectedNode.Name;
            if (currentuser != "admin" && currentuser != id)
            {
                MessageBox.Show("����Ȩ���д˲�����");
                return;
            }
            Smmuser data = UserService.GetOneByKey(id);
            using (FrmSmmuserEdit_new dlg = new FrmSmmuserEdit_new())
            {
                dlg.DataObject = data;
                dlg.UserNo = data.Userid;
                dlg.UserName = data.UserName;
                dlg.currentuser = currentuser;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    UserService.Update(data);

                    CreateView();
                }
                MainForm.CurrentForm.SetStatusLabel();
            }
            
        }
        /// <summary>
        /// �޸���
        /// </summary>
        private void EditGroup()
        {
            string currentuser = MIS.UserNumber;
            if (currentuser != "admin")
            {
                MessageBox.Show("����Ȩ���д˲�����");
                return;
            }
                string id = treeView1.SelectedNode.Name;
                Smmgroup data = GroupService.GetOneByKey<Smmgroup>(id);
                using (FrmSmmgroupEdit dlg = new FrmSmmgroupEdit())
                {
                    dlg.DataObject = data;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        GroupService.Update<Smmgroup>(data);

                        CreateView();
                    }
                }
           
        }
        /// <summary>
        /// ɾ���û�
        /// </summary>
        private void DeleteUser()
        {
            TreeNode tempnode=treeView1.SelectedNode;
            string id = treeView1.SelectedNode.Name;
            string username = treeView1.SelectedNode.Text;
            string currentuser = MIS.UserNumber;
         
            if (currentuser != "admin" )
            {
                MessageBox.Show("����Ȩ���д˲�����");
                return;
            }
            if (id == "admin")
            {
                MessageBox.Show("����ɾ������Ա��");
                return;
            }
            if (MsgBox.ShowYesNo("�Ƿ�ȷ��ɾ��[" + username + "]�û�?") == DialogResult.No) return;
            try
            {
                UserService.DeleteByKey(id);
                treeView1.Nodes.Remove(tempnode);
                
            }
            catch { MessageBox.Show("ɾ��ʧ�ܣ�"); }


        }
        /// <summary>
        /// ɾ����
        /// </summary>
        private void DeleteGroup()
        {
           TreeNode tempnode=treeView1.SelectedNode;
            string id = treeView1.SelectedNode.Name;
            string username = treeView1.SelectedNode.Text;
            if (id == "SystemManage")
            {
        
                MessageBox.Show("����ɾ��ϵͳ�����飡");
                return;
            }

            if (MsgBox.ShowYesNo("�Ƿ�ȷ��ɾ��[" + username + "]��?") == DialogResult.No) return;
            try
            {
                GroupService.DeleteByKey<Smmgroup>(id);
                treeView1.Nodes.Remove(tempnode);
            }
            catch { MessageBox.Show("ɾ��ʧ�ܣ�"); }
            
        }
        //�����
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddGroup();
        }
        //����û�
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddUser();
        }
        //�޸�
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                return;
            }
            if (treeView1.SelectedNode.Tag == "��")
            {
                EditGroup();
            }
            else if (treeView1.SelectedNode.Tag == "�û�")
            {
                EditUser();
            }
        }
        //ɾ��
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                return;
            }
            if (treeView1.SelectedNode.Tag == "��")
            {
                DeleteGroup();
            }
            else if (treeView1.SelectedNode.Tag == "�û�")
            {
                DeleteUser();
            }
        }
        //ˢ��
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CreateView();
        }
        //��Ȩ
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                return;
            }
            if (treeView1.SelectedNode.Tag == "��")
            {
                RightSetup();
            }
            else
            {
                MessageBox.Show("��ѡ������ٽ�����Ȩ");
            }
        }
        //�ر�
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        
    }
}