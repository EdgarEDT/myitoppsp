using System;
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
using System.Collections;
namespace Itop.Client.Projects
{
    public partial class frmProjUser : FormBase
    {
        private string projid = "";
        private string userid = "";
        private bool havechange = false;
        public frmProjUser( string ProjUI,string UserID)
        {
            InitializeComponent();
            projid = ProjUI;
            userid = UserID;
        }
        Hashtable addtable = new Hashtable();
        Hashtable deletetable = new Hashtable();
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

        private void frmProjUser_Load(object sender, EventArgs e)
        {
            CreateView();
            CreateViewProj();
        }

        int rootimage = 49;
        int groupimage = 48;
        int un_group = 50;
        //显示系统所有用户
        private void CreateView()
        {
            treeView1.Nodes.Clear();
            TreeNode node = treeView1.Nodes.Add("system", "系统用户和组", rootimage, rootimage);
            node.Tag = "根";
            IList<Smmgroup> list = SysService.GetStrongList<Smmgroup>();
            for (int i = 0; i < list.Count; i++)
            {
               
                IList<Smugroup> listUser = SysService.GetList<Smugroup>("SelectSmugroupByWhere", "Groupno='" + list[i].Groupno + "'");
                if ( listUser.Count>0)
                {
                    TreeNode tempnode = node.Nodes.Add(list[i].Groupno, list[i].Groupname, groupimage, groupimage);
                    tempnode.Tag = "组";

                    for (int j = 0; j < listUser.Count; j++)
                    {
                        Smmuser user = new Smmuser();
                        user.Userid = listUser[j].Userid;
                        Smmuser tempuser = SysService.GetOneByKey<Smmuser>(user);
                        //tempuser.Lastlogon 存放用户图标id
                        if (tempuser != null)
                        {
                            int useimage = 0;
                            if (int.TryParse(tempuser.Lastlogon, out useimage))
                            {
                                useimage = int.Parse(tempuser.Lastlogon.ToString());
                            }
                            TreeNode tempnodeuser = tempnode.Nodes.Add(listUser[j].Userid, tempuser.UserName, useimage, useimage);
                            tempnodeuser.Tag = "用户";
                        }

                    }

                }
                

            }
            // 将未分组用户添加进一个组
            IList<Smmuser> list_user = SysService.GetStrongList<Smmuser>();
            TreeNode un_groupnode = new TreeNode();
            un_groupnode.Name = "未分组";
            un_groupnode.Text = "未分组用户";
            un_groupnode.ImageIndex = un_group;
            un_groupnode.SelectedImageIndex = un_group;
            un_groupnode.Tag = "未分组";
            bool un_groupuser = false;
            for (int k = 0; k < list_user.Count; k++)
            {
                string tempuserno = list_user[k].Userid;
                IList<Smugroup> listUsergroup = SysService.GetList<Smugroup>("SelectSmugroupByWhere", "Userid='" + tempuserno + "'");
                if (listUsergroup.Count == 0)
                {
                    un_groupuser = true;
                    int useimage = 0;
                    if (int.TryParse(list_user[k].Lastlogon, out useimage))
                    {
                        useimage = int.Parse(list_user[k].Lastlogon.ToString());
                    }
                    TreeNode tempnodeuser = un_groupnode.Nodes.Add(list_user[k].Userid, list_user[k].UserName, useimage, useimage);
                    tempnodeuser.Tag = "用户";

                }

            }
            if (un_groupuser)
            {
                node.Nodes.Add(un_groupnode);
            }
            node.Expand();
            node.ExpandAll();
           
        }
        //显示当前项目用户
        private void CreateViewProj()
        {
            treeView2.Nodes.Clear();
            TreeNode node = treeView2.Nodes.Add("system", "当前项目用户", groupimage, groupimage);
            node.Tag = "根";
            //添加当前用户
            Smmuser user = sysService.GetOneByKey<Smmuser>(userid);
            int useimage = 0;
            if (int.TryParse(user.Lastlogon, out useimage))
            {
                useimage = int.Parse(user.Lastlogon.ToString());
            }
            TreeNode currtennode= node.Nodes.Add(user.Userid, user.UserName+"（项目创建人）", useimage, useimage);
            currtennode.Tag = "创建人";

            IList<ProjectUser> list = SysService.GetList<ProjectUser>("SelectProjectbyWhere"," UID='"+projid+"'");
            for (int i = 0; i < list.Count; i++)
            {
                TreeNode tempnode = node.Nodes.Add(list[i].UserID, list[i].UserName, list[i].Sort, list[i].Sort);
                tempnode.Tag = "用户";

            }
            node.ExpandAll();

        }
        
        //移除当前项目所有用户
        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            if (treeView2.Nodes[0].Nodes.Count==1)
            {
                MessageBox.Show("不能册除项目创建人!!");
                return;
            }
            foreach (TreeNode  node in treeView2.Nodes[0].Nodes)
            {
                if (node.Tag!="创建人")
                {
                    treeView2.Nodes[0].Nodes.Remove(node);
                    havechange = true;
                }
            }
            
        }
        //移除用户
        private void btnRemoveOne_Click(object sender, EventArgs e)
        {
           
            if (treeView2.SelectedNode == null)
            {
                MessageBox.Show("请选择项目用户!");
                return;
            }
            if (treeView2.SelectedNode.Tag == "根")
            {
                MessageBox.Show("请选择项目用户!");
                return;
            }
            if (treeView2.SelectedNode.Tag == "创建人")
            {
                MessageBox.Show("不能册除项目创建人!");
                return;
            }
            
            treeView2.Nodes.Remove(treeView2.SelectedNode);
            havechange = true;
            
        }
        //添加用户
        private void btnAddOne_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                MessageBox.Show("请选择要添加的用户!");
                return;
            }
            if (treeView1.SelectedNode.Tag == "根" || treeView1.SelectedNode.Tag == "组")
            {
                MessageBox.Show("请选择要添加的用户!");
                return;
            }
            if (!treeView2.Nodes[0].Nodes.ContainsKey(treeView1.SelectedNode.Name))
            {
              TreeNode tempnode=  treeView2.Nodes[0].Nodes.Add(treeView1.SelectedNode.Name, treeView1.SelectedNode.Text, treeView1.SelectedNode.ImageIndex, treeView1.SelectedNode.ImageIndex);
              tempnode.Tag = "用户";
              havechange = true;
            }
            else
            {
                MessageBox.Show("项目用户已包含用户 "+treeView1.SelectedNode.Text+" !");
                return;
            }
        }
        //添加所有用户
        private void btnAddAll_Click(object sender, EventArgs e)
        {
            IList<Smmuser> list_user = SysService.GetStrongList<Smmuser>();
            for (int i = 0; i < list_user.Count; i++)
            {
                if (!treeView2.Nodes[0].Nodes.ContainsKey(list_user[i].Userid))
                {
                    Smmuser user = list_user[i];
                    int useimage = 0;
                    if (int.TryParse(user.Lastlogon, out useimage))
                    {
                        useimage = int.Parse(user.Lastlogon.ToString());
                    }
                    TreeNode currtennode = treeView2.Nodes[0].Nodes.Add(user.Userid, user.UserName , useimage, useimage);
                    currtennode.Tag = "用户";
                    havechange = true;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (havechange)
            {
                if (MessageBox.Show("确认保存更改？","询问",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    try
                    {
                        
                        sysService.GetList("DeleteProjectUserWhere", " UID='" + projid + "'");
                    }
                    catch (Exception)
                    {
                        
                        throw;
                    }
                    
                    foreach (TreeNode node in treeView2.Nodes[0].Nodes)
                    {
                        if (node.Tag != "创建人")
                        {
                            ProjectUser puser = new ProjectUser();
                            puser.UID = projid;
                            puser.UserID = node.Name;
                            puser.UserName = node.Text;
                            puser.Sort = node.ImageIndex;
                            try
                            {
                                sysService.Create<ProjectUser>(puser);
                            }
                            catch (Exception)
                            {
                                
                                throw;
                            }
                            

                        }
                    }
                    this.Close();
                }
            }
        }


       

       
    }
}
