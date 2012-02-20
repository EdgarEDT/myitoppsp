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
using Itop.Client.Base;

namespace Itop.RightManager.UI { 
    public partial class FrmSmmgroupEdit : FormBase {
        public FrmSmmgroupEdit() {
            InitializeComponent();
        }
        private bool isNew = false;


        public IBaseService BaseService
        {
            get
            {
                return RemotingHelper.GetRemotingService<IBaseService>();
            }
        }


        private void FrmPersonEdit_Load(object sender, EventArgs e) {
            if (_smmgroup == null) {
                _smmgroup = new Smmgroup();
                isNew = true;
            } else {
                tbGroupno.Enabled = false;
            }

            tbGroupno.DataBindings.Add("Text", DataObject, "Groupno");
            tbGroupname.DataBindings.Add("Text", DataObject, "Groupname");
            tbRemark.DataBindings.Add("Text", DataObject, "Remark");

            IList<Smmuser> list = BaseService.GetList<Smmuser>("SelectSmmuserByGroupID", DataObject.Groupno);
            listView1.Items.Clear();
            listView1.Columns.Clear();

            listView1.Columns.Add("用户号", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("用户名", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("描述", 300, HorizontalAlignment.Left);

            foreach (Smmuser su in list)
            {
                ListViewItem item = listView1.Items.Add(su.Userid, 0);

                item.SubItems.Add(su.UserName);
                item.SubItems.Add(su.Remark);
                //item.Tag = data;
                //listView1.Items.Add(item);
            }

        }






        private Itop.Domain.RightManager.Smmgroup _smmgroup;
        public Itop.Domain.RightManager.Smmgroup DataObject {
            set { _smmgroup = value; }
            get { return _smmgroup; }
        }

       
        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string strErr = "";
                if (!SmmgroupRule.Check(DataObject, ref strErr, isNew))
                {
                    MsgBox.Show(strErr);
                    return;
                }

            }
            catch { MessageBox.Show("数据格式有误"); return; }
            DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}