using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using System.Collections;
using Itop.Client.Base;
namespace Itop.TLPSP.DEVICE
{
    public partial class frmDS : FormBase
    {
        public frmDS()
        {
            InitializeComponent();            
        }
        private string projectSUID;
        public string ProjectSUID
        {
            get
            {
                return projectSUID;
            }
            set 
            {
                projectSUID = value;
            }
        }
        public object PJ
        {
            get
            {
                return lookUpEdit1.Properties.GetKeyValueByDisplayText(lookUpEdit1.Text);
            }
        }
        public void InitData()
        {
            PSP_ELCPROJECT pj = new PSP_ELCPROJECT();
            pj.ProjectID = ProjectSUID;
            IList list = UCDeviceBase.DataService.GetList("SelectPSP_ELCPROJECTList", pj);
            DataTable dt = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_ELCPROJECT));            
            lookUpEdit1.Properties.DataSource = dt;
            lookUpEdit1.ItemIndex = 0;
            lookUpEdit1.Properties.DisplayMember = "Name";
            lookUpEdit1.Properties.ValueMember = "ID";
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
           //object str = lookUpEdit1.Properties.GetKeyValueByDisplayText(lookUpEdit1.Text);
        }
    }
}