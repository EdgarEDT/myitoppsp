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
    public partial class FrmPersonEdit : FormBase {
        public FrmPersonEdit() {
            InitializeComponent();
        }
        private bool isNew = false;

        private void FrmPersonEdit_Load(object sender, EventArgs e) {
            if (_smmuser == null) {
                _smmuser = new Smmuser();
                isNew = true;
            } else {
                tbUserid.Enabled = false;
            }

            tbUserid.DataBindings.Add("Text", DataObject, "Userid");
            tbUserName.DataBindings.Add("Text", DataObject, "UserName");
            tbPassword.DataBindings.Add("Text", DataObject, "Password");
            tbExpireDate.DataBindings.Add("Text", DataObject, "ExpireDate");
            //tbDisableflg.DataBindings.Add("Text", DataObject, "Disableflg");
            tbLastlogon.DataBindings.Add("Text", DataObject, "Lastlogon");

            tbRemark.DataBindings.Add("Text", DataObject, "Remark");

            if (DataObject.Disableflg == "Y") {
                tbDisableflg.Checked = true;
            }
            
        }
        private Itop.Domain.RightManager.Smmuser _smmuser;
        public Itop.Domain.RightManager.Smmuser DataObject {
            set { _smmuser = value; }
            get { return _smmuser; }
        }

        private void button1_Click(object sender, EventArgs e) {
           

        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
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