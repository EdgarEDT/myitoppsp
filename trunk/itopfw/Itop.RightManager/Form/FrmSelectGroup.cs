using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Server.Interface;
using Itop.Common;
using Itop.Domain.RightManager;
using Itop.Client.Base;

namespace Itop.RightManager
{
    public partial class FrmSelectGroup : FormBase
    {

        #region 变量，属性

        private DataTable smuGroupTable;
        private IBaseService smmprogService;
        private Hashtable groupItems=new Hashtable();

        public Hashtable GroupItems
        {
            get { return groupItems; }        
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

        public FrmSelectGroup()
        { 
            InitializeComponent();
        }

        private void FrmSelectGroup_Load(object sender, EventArgs e)
        {

            smuGroupTable = DataConverter.ToDataTable(SmmprogService.GetList("SelectSmmgroupList", typeof(Smmgroup)));

            foreach (DataRow row in smuGroupTable.Rows)
            {
                
                ListViewItem listItem = new ListViewItem();
                listItem.Text = row["Groupname"].ToString();
                listItem.ImageIndex = 0;
                listItem.Tag = DataConverter.RowToObject<Smmgroup>(row);
                listView.Items.Add(listItem);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView.SelectedItems)
            {
                groupItems.Add(((Smmgroup)item.Tag).Groupno, item.Tag);
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}