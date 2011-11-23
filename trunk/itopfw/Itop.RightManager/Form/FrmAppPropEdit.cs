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
    public partial class FrmAppPropEdit : FormBase { 
        public FrmAppPropEdit() {
            InitializeComponent();
        }
        ////private bool isNew = false;

        private void FrmPersonEdit_Load(object sender, EventArgs e) {
            if (_data == null) {
                _data = new Itop.Domain.SAppProps();
                //isNew = true;
            } else {
                tbPropName.Enabled = false;
            }

            tbPropName.DataBindings.Add("Text", DataObject, "PropName");
            tbPropValue.DataBindings.Add("Text", DataObject, "PropValue");
            tbRemark.DataBindings.Add("Text", DataObject, "Remark");
        }
        private Itop.Domain.SAppProps _data;
        public Itop.Domain.SAppProps DataObject {
            set { _data = value; }
            get { return _data; }
        }

        private void button1_Click(object sender, EventArgs e) {
            try {
                //string strErr = "";
                //if (!SmmgroupRule.Check(DataObject, ref strErr,isNew)) {
                //    MsgBox.Show(strErr);
                //    return;
                //}               
               
            } catch { MessageBox.Show("数据格式有误"); return; }
            DialogResult = DialogResult.OK;

        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}