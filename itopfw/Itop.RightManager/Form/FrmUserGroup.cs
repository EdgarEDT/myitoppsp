using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Server.Interface;
using Itop.Common;
using Itop.Domain.RightManager;
using System.Collections;
using Itop.Client.Base;

namespace Itop.RightManager
{
    public partial class FrmUserGroup : FormBase
    {
        #region 变量，属性
        private string userNo;
        private string userName;
        private Hashtable groupItems=new Hashtable();        
        private Hashtable addgroupItems=new Hashtable();
        private Hashtable deletegroupItems=new Hashtable();

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
        #endregion


        

        public FrmUserGroup()
        { 
            InitializeComponent();
        }

        private void FrmUserGroup_Load(object sender, EventArgs e)
        {
            smuGroupTable = DataConverter.ToDataTable(SmmprogService.GetList("SelectSmugroupByUserid",userNo ),typeof(Smugroup));

            foreach (DataRow row in smuGroupTable.Rows)
            {
                groupItems.Add(row["Groupno"], row);
                ListViewItem listItem = new ListViewItem();
                listItem.Text = row["Groupname"].ToString();
                listItem.Tag = DataConverter.RowToObject<Smugroup>(row);
                listView.Items.Add(listItem);
            }

            this.Text = userName + " 属性";            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmSelectGroup frm = new FrmSelectGroup();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                
                foreach (DictionaryEntry de in frm.GroupItems)
                {
                    if (!groupItems.Contains(de.Key) && !addgroupItems.Contains(de.Key))
                    {
                        addgroupItems.Add(de.Key, de.Value);
                        ListViewItem listItem = new ListViewItem();
                        listItem.Text = ((Smmgroup)de.Value).Groupname;
                        listItem.Tag = de.Value;
                        listView.Items.Add(listItem);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listView.FocusedItem == null)
                return;
            object listviewKey ="";

            if (addgroupItems.ContainsValue(listView.FocusedItem.Tag)){
                addgroupItems.Remove(((Smmgroup)listView.FocusedItem.Tag).Groupno);
            }
            else if(!deletegroupItems.ContainsValue(listView.FocusedItem.Tag)){

                deletegroupItems.Add(((Smugroup)listView.FocusedItem.Tag).Groupno,listView.FocusedItem.Tag);
            }

            listView.FocusedItem.Remove();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            foreach (DictionaryEntry de in addgroupItems)
            {
                Smugroup data = new Smugroup();
                data.Groupno = ((Smmgroup)de.Value).Groupno;
                data.Userid = userNo;
                SmmprogService.Create<Smugroup>(data);               
            }
            foreach (DictionaryEntry de in deletegroupItems)
            {
                SmmprogService.Delete<Smugroup>((Smugroup)de.Value);
            }
            
            this.DialogResult = DialogResult.OK;
        }
    }
}