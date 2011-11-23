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
    public partial class frmRDQdlg : Itop.Client.Base.FormBase
    {
        PSPDEV dev = new PSPDEV();
        public void SetEnable()
        {
            this.label2.Visible = false;
            this.spts.Visible = false;
        }
        public PSPDEV DeviceMx
        {
            get
            {
                dev.EleID =txtbh.Text;
                dev.Name = txtnm.Text;
                dev.Flag = (int)spts.Value;
                dev.RateVolt = Convert.ToDouble(comboBox1.Text);
                dev.AreaID = lookUpEdit1.EditValue.ToString();
                dev.OperationYear = comboBoxEdit1.Text;
                //dev.Burthen = Convert.ToDecimal(sprl.Value);
                dev.Type = "71";
                dev.HgFlag = textEdit2.Text;
                dev.Number = (int)spinEdit1.Value;
                return dev;
            }
            set  
            {
                dev = value;
                txtbh.Text=dev.EleID;
                txtnm.Text=dev.Name ;
                spts.Value=(decimal)dev.Flag;
                comboBox1.Text=dev.RateVolt.ToString();
                lookUpEdit1.EditValue = dev.AreaID;
                comboBoxEdit1.Text = dev.OperationYear;
                textEdit2.Text = dev.HgFlag;
                spinEdit1.Value = (decimal)dev.Number;
                //sprl.Value=(decimal)dev.Burthen;
            }
        }
        public frmRDQdlg()
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
            string sql = " where RateVolt=" + comboBox1.Text + " and (type='05' or type ='70') and ProjectID='" + Itop.Client.MIS.ProgUID + "'";
            IList list = Services.BaseService.GetList("SelectPSPDEVByCondition", sql);   
            
            lookUpEdit1.Properties.DataSource = list;

            object o = new object();
            for (int i = -30; i <= 30; i++)
            {
                o = System.DateTime.Now.Year + i;
                comboBoxEdit1.Properties.Items.Add(o);

            }
            //comboBoxEdit1.Text = DateTime.Today.Year.ToString();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = " where RateVolt=" + comboBox1.Text + " and type='05' and ProjectID='"+Itop.Client.MIS.ProgUID+"'";
            IList list = Services.BaseService.GetList("SelectPSPDEVByCondition", sql);
            lookUpEdit1.Properties.DataSource = list;

        }

        private void lookUpEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                frmNodeTreedlg dlg = new frmNodeTreedlg();
                dlg.Pspdev = this.DeviceMx;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (dlg.Pspdev != null)
                    {
                        lookUpEdit1.EditValue = dlg.Pspdev.SUID;
                    }
                }
            }
        }

    }
}