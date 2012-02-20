using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.RightManager;
using Itop.Server.Interface;
using Itop.BusinessRule.RightManager;
using Itop.Common;
using System.Collections;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;
using Itop.Client;
using Itop.Client.Base;
namespace Itop.RightManager.UI { 
    public partial class FrmSmmuserEdit_new : FormBase {
        public FrmSmmuserEdit_new() {
            InitializeComponent();
        }
        private string userNo;
        private string userName;
        public string currentuser = string.Empty;
        private bool isNew = false;
        private Hashtable groupItems = new Hashtable();       
        private Hashtable addgroupItems = new Hashtable();
        private Hashtable deletegroupItems = new Hashtable();
        private DataTable smuGroupTable;
        private IBaseService smmprogService;
        public string UserNo
        {
            get { return userNo; }
            set { userNo = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public IBaseService SmmprogService
        {
            get
            {
                if (smmprogService == null)
                {
                    smmprogService = RemotingHelper.GetRemotingService<IBaseService>();
                }
                if (smmprogService == null) MsgBox.Show("IBaseService服务没有注册");
                return smmprogService;
            }
        }
        private void FrmSmmuserEdit_new_Load(object sender, EventArgs e) {
            listView1.View = System.Windows.Forms.View.List;
            if (_smmuser == null) 
            {
                _smmuser = new Smmuser();
                isNew = true;
            } 
            else
            {
                tbUserid.Enabled = false;
            }
            for (int i = 0; i < imageList2.Images.Count; i++)
            {
                //imageComboBoxEdit1
                ImageComboBoxItem tempItem = new ImageComboBoxItem();
                //tempItem.Description = i.ToString();
                tempItem.ImageIndex = i;
                tempItem.Value = i;
                imageComboBoxEdit1.Properties.Items.Add(tempItem);
            }

            tbUserid.DataBindings.Add("Text", DataObject, "Userid");
            tbUserName.DataBindings.Add("Text", DataObject, "UserName");
            tbPassword.DataBindings.Add("Text", DataObject, "Password");
            tbExpireDate.DataBindings.Add("Text", DataObject, "ExpireDate");
            //tbDisableflg.DataBindings.Add("Text", DataObject, "Disableflg");
            //tbLastlogon.DataBindings.Add("Text", DataObject, "Lastlogon");

            tbRemark.DataBindings.Add("Text", DataObject, "Remark");
            int tempint = 0;
            if (int.TryParse(DataObject.Lastlogon,out tempint))
	        {
                imageComboBoxEdit1.SelectedIndex = tempint;
	        }
            else
	        {
                imageComboBoxEdit1.SelectedIndex = tempint;
	        }
            
            if (DataObject.Disableflg == "Y") {
                tbDisableflg.Checked = true;
            }
            smuGroupTable = DataConverter.ToDataTable(SmmprogService.GetList("SelectSmugroupByUserid", userNo), typeof(Smugroup));

            foreach (DataRow row in smuGroupTable.Rows)
            {
                groupItems.Add(row["Groupno"], row);
                ListViewItem listItem = new ListViewItem();
                listItem.Name = row["Groupno"].ToString();
                listItem.Text = row["Groupname"].ToString();
                listItem.ImageIndex = 0;
                listItem.Tag = DataConverter.RowToObject<Smugroup>(row);
                listView1.Items.Add(listItem);
            }
            if (currentuser!="admin")
            {
                btn_addgroup.Enabled = false;
                btn_DeleteGroup.Enabled = false;
                
            }
            
        }
        
        private Itop.Domain.RightManager.Smmuser _smmuser;
        public Itop.Domain.RightManager.Smmuser DataObject {
            set { _smmuser = value; }
            get { return _smmuser; }
        }

        private void button1_Click(object sender, EventArgs e) 
        {
           

        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btn_addgroup_Click(object sender, EventArgs e)
        {
            if (tbUserid.Text.Length==0)
            {
                MessageBox.Show("请先添加用户信息");
                return;
            }
            FrmSelectGroup frm = new FrmSelectGroup();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                foreach (DictionaryEntry de in frm.GroupItems)
                {
                    if (!groupItems.Contains(de.Key) && !addgroupItems.Contains(de.Key))
                    {
                        addgroupItems.Add(de.Key, de.Value);
                        ListViewItem listItem = new ListViewItem();
                        listItem.Name = de.Key.ToString(); ;
                        listItem.Text = ((Smmgroup)de.Value).Groupname;
                        listItem.ImageIndex = 0;
                        
                        listItem.Tag = de.Value;
                        listView1.Items.Add(listItem);
                    }
                }
            }
        }

        private void btn_DeleteGroup_Click(object sender, EventArgs e)
        {
           
            
        }

        private void imageComboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //ShowValues(teImageComboBoxEdit1, sender as ImageComboBoxEdit);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string strErr = "";
                if (!SmmuserRule.Check(DataObject, ref strErr, isNew))
                {
                    MsgBox.Show(strErr);
                    return;
                }

                if (tbDisableflg.Checked)
                    DataObject.Disableflg = "Y";
                else
                    DataObject.Disableflg = "N";
                DataObject.Lastlogon = imageComboBoxEdit1.SelectedIndex.ToString();
                foreach (DictionaryEntry de in addgroupItems)
                {
                    Smugroup data = new Smugroup();
                    data.Groupno = ((Smmgroup)de.Value).Groupno;
                    data.Userid = tbUserid.Text;
                    SmmprogService.Create<Smugroup>(data);
                }
                if (!isNew)
                {
                    foreach (DictionaryEntry de in deletegroupItems)
                    {
                        SmmprogService.Delete<Smugroup>((Smugroup)de.Value);
                    }
                }


                this.DialogResult = DialogResult.OK;


            }
            catch { MessageBox.Show("数据格式有误"); return; }
            DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_addgroup_Click_1(object sender, EventArgs e)
        {
            if (tbUserid.Text.Length == 0)
            {
                MessageBox.Show("请先添加用户信息");
                return;
            }
            FrmSelectGroup frm = new FrmSelectGroup();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                foreach (DictionaryEntry de in frm.GroupItems)
                {
                    if (!groupItems.Contains(de.Key) && !addgroupItems.Contains(de.Key))
                    {
                        addgroupItems.Add(de.Key, de.Value);
                        ListViewItem listItem = new ListViewItem();
                        listItem.Name = de.Key.ToString(); ;
                        listItem.Text = ((Smmgroup)de.Value).Groupname;
                        listItem.ImageIndex = 0;

                        listItem.Tag = de.Value;
                        listView1.Items.Add(listItem);
                    }
                }
            }
        }

        private void btn_DeleteGroup_Click_1(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("没有添加分组信息，无法删除！");
                return;
            }
            if (listView1.FocusedItem == null)
            {
                MessageBox.Show("请选择要删除的分组！");
                return;
            }

            object listviewKey = "";
            if (tbUserid.Text == "admin" && listView1.FocusedItem.Name == "SystemManage")
            {
                MessageBox.Show("不能将管理员从管理组删除！");
                return;
            }

            if (addgroupItems.ContainsValue(listView1.FocusedItem.Tag))
            {
                addgroupItems.Remove(((Smmgroup)listView1.FocusedItem.Tag).Groupno);
            }
            else if (!deletegroupItems.ContainsValue(listView1.FocusedItem.Tag))
            {

                deletegroupItems.Add(((Smugroup)listView1.FocusedItem.Tag).Groupno, listView1.FocusedItem.Tag);
            }

            listView1.FocusedItem.Remove();
        }

       

    




       
    }
}