using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Client.Projects;
using System.Collections;
namespace Itop.TLPSP.DEVICE
{
    public partial class frmHGdlg : Itop.Client.Base.FormBase
    {
        PSPDEV dev = new PSPDEV();
        public PSPDEV DeviceMx
        {
            get
            {
                dev.Number = (int)spinEdit8.Value;
                dev.UnitFlag = radioGroup3.EditValue.ToString();
                dev.HuganLine1 = comboBoxEdit4.Text;
                dev.HuganLine2 = comboBoxEdit1.Text;
                dev.HuganTQ1 = (double)spinEdit1.Value;
                dev.ReferenceVolt = (double)spinEdit2.Value;
                dev.Date1 = date1.Text;
                dev.Date2 = date2.Text;
                dev.Type = "15";
                return dev;
            }
            set
            {
                dev = value;
                spinEdit8.Value = (decimal)dev.Number;
                radioGroup3.EditValue = dev.UnitFlag;
                comboBoxEdit4.Text = dev.HuganLine1;
                comboBoxEdit1.Text = dev.HuganLine2;
                spinEdit1.Value = (decimal)dev.HuganTQ1;
                spinEdit2.Value = (decimal)dev.ReferenceVolt;
                date1.Text = dev.Date1;
                date2.Text = dev.Date2;
            }
        }
        public frmHGdlg()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            intdata();
        }
        public void intdata()
        {
            string con = " where Type ='05'and ProjectID ='" + this.ProjectID + "' order by name";
            IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            foreach (PSPDEV pspdev in list)
            {
                if (comboBoxEdit4.Properties.Items.IndexOf(pspdev.Name) == -1)
                {
                    comboBoxEdit4.Properties.Items.Add(pspdev.Name);
                    
                }
                if (comboBoxEdit1.Properties.Items.IndexOf(pspdev.Name) == -1)
                {
                    comboBoxEdit1.Properties.Items.Add(pspdev.Name);

                }
            }
            //Dlqiname.Text=
        }
        string projectid;
        public string ProjectID
        {
            get { return projectid; }
            set { projectid = value; }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}