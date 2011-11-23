using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Itop.Server.Interface;
using Itop.Domain.RightManager;
using Itop.Common;
using Itop.RightManager.UI;

namespace Itop.RightManager {
    public enum ShowType {
        None,
        User,//用户
        Group //组
    }
    public partial class UserListView : UserControl,IRecord {
        public UserListView() {
            InitializeComponent();
        }
        ISmmuserService userService;
        //服务
        public ISmmuserService UserService {
            get {
                if (userService == null) {
                    userService = Itop.Common.RemotingHelper.GetRemotingService<ISmmuserService>();
                }
                return userService;
            }
        }
        IBaseService groupService;
        //服务
        public IBaseService GroupService {
            get {
                if (groupService == null) {
                    groupService = Itop.Common.RemotingHelper.GetRemotingService<IBaseService>();
                }
                return groupService;
            }
        }
        ShowType showType;

        /// <summary>
        ///  
        /// </summary>
        /// 
        [DesignerSerializationVisibility(0)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ShowType ShowType {
            get { return showType; }

            set {

                if (showType == value) return;
                showType = value;
                if (showType == ShowType.User)
                    CreateViewUser();
                else
                    CreateViewGroup();
            }
        }
        private void CreateViewUser() {

            listView1.Items.Clear();
            listView1.Columns.Clear();
            
            listView1.Columns.Add( "用户号",100,HorizontalAlignment.Left);
            listView1.Columns.Add("用户名", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("描述", 300, HorizontalAlignment.Left);
            RetrieveUser();
            
        }
        private void CreateViewGroup() {
            listView1.Items.Clear();
            listView1.Columns.Clear();

            listView1.Columns.Add("组号", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("组名", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("描述", 300, HorizontalAlignment.Left);
            RetrieveGroup();
        }

        private void RetrieveUser() {
            IList<Smmuser> list = UserService.GetStrongList();
            IEnumerator<Smmuser> ie = list.GetEnumerator();
            while (ie.MoveNext()) {
                Smmuser data = ie.Current;
                ListViewItem item = listView1.Items.Add(data.Userid, 0);

                item.SubItems.Add(data.UserName);
                item.SubItems.Add(data.Remark);
                item.Tag = data;
            }
        }
        private void RetrieveGroup() {
            IList<Smmgroup> list = GroupService.GetStrongList<Smmgroup>();
            IEnumerator<Smmgroup> ie = list.GetEnumerator();
            while (ie.MoveNext()) {
                Smmgroup data = ie.Current;
                ListViewItem item = listView1.Items.Add(data.Groupno, 1);

                item.SubItems.Add(data.Groupname);
                item.SubItems.Add(data.Remark);
                item.Tag = data;
            }

        }


        #region IRecord 成员

        public void Add() {
            if (showType == ShowType.User)
                AddUser();
            else if (showType == ShowType.Group)
                AddGroup();                
            
        }
        /// <summary>
        /// 增加用户
        /// </summary>
        private void AddUser() {

            using (FrmPersonEdit dlg = new FrmPersonEdit()) {

                if (dlg.ShowDialog() == DialogResult.OK) {

                    UserService.Create(dlg.DataObject);
                    Retrieve();
                }
            } 
        }
        /// <summary>
        /// 增加组
        /// </summary>
        private void AddGroup() {
            using (FrmSmmgroupEdit dlg = new FrmSmmgroupEdit()) {

                if (dlg.ShowDialog() == DialogResult.OK) {

                    GroupService.Create<Smmgroup>(dlg.DataObject);
                    Retrieve();
                }
            } 
        }

        public void Edit() {
            if (showType == ShowType.User)
                EditUser();
            else if (showType == ShowType.Group)
                EditGroup(); 
            
        }

        private void EditUser() {
            if (listView1.SelectedItems.Count > 0) {
                ListViewItem item = listView1.SelectedItems[0];
                string id = item.SubItems[0].Text;
                Smmuser data = UserService.GetOneByKey(id);
                using (FrmPersonEdit dlg = new FrmPersonEdit()) {
                    dlg.DataObject = data;
                    if (dlg.ShowDialog() == DialogResult.OK) {
                        UserService.Update(data);

                        Retrieve();
                    }
                }
            }
        }

        private void EditGroup() {
            if (listView1.SelectedItems.Count > 0) {
                ListViewItem item = listView1.SelectedItems[0];
                string id = item.SubItems[0].Text;
                Smmgroup data = GroupService.GetOneByKey<Smmgroup>(id);
                using (FrmSmmgroupEdit dlg = new FrmSmmgroupEdit()) {
                    dlg.DataObject = data;
                    if (dlg.ShowDialog() == DialogResult.OK) {
                        GroupService.Update<Smmgroup>(data);

                        Retrieve();
                    }
                }
            }
        }

        public void Look() {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Delete() {
            if (showType == ShowType.User)
                DeleteUser();
            else if (showType == ShowType.Group)
                DeleteGroup(); 
        }
        private void DeleteUser() {
            if (listView1.SelectedItems.Count > 0) {

                ListViewItem item = listView1.SelectedItems[0];
                string id = item.SubItems[0].Text;
                if (MsgBox.ShowYesNo("是否确认删除[" + id + "]用户?") == DialogResult.No) return;
                try {
                    UserService.DeleteByKey(id);
                    listView1.Items.Remove(item);
                } catch { MessageBox.Show("删除失败！"); }
            }

        }
        private void DeleteGroup() {
            if (listView1.SelectedItems.Count > 0) {

                ListViewItem item = listView1.SelectedItems[0];
                string id = item.SubItems[0].Text;
                if (MsgBox.ShowYesNo("是否确认删除[" + id + "]组?") == DialogResult.No) return;
                try {
                    GroupService.DeleteByKey<Smmgroup>(id);
                    listView1.Items.Remove(item);
                } catch { MessageBox.Show("删除失败！"); }
            }

        }

        public void Retrieve() {
            if (listView1.Columns.Count > 0) {
                listView1.Items.Clear();
                if (showType == ShowType.User)
                    RetrieveUser();
                else
                    RetrieveGroup();
            }
        }

        #endregion
        public void RightSetup(string projectUID,string projectName) {
            if (listView1.SelectedItems.Count > 0) 
            {
                ListViewItem item = listView1.SelectedItems[0];
                string id = item.SubItems[0].Text;
                if (showType == ShowType.Group)
                {   
                    FrmGroupRights dlg = new FrmGroupRights();
                    dlg.Groupno = id;
                    dlg.ProjectUID = projectUID;
                    dlg.ProjectName = projectName;
                    dlg.ShowDialog();
                }
                else if (showType == ShowType.User)
                {
                    FrmUserGroup dlg = new FrmUserGroup();
                    dlg.UserNo = id;
                    dlg.UserName = item.SubItems[1].Text;
                    if(dlg.ShowDialog()==DialogResult.OK)
                        MsgBox.Show("保存成功");
                }
            }
        }
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e) {
            Edit();
        }
    }
}
