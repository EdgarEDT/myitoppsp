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
    public partial class frmPWdlg : Itop.Client.Base.FormBase
    {
        PSPDEV dev = new PSPDEV();
      
        public PSPDEV DeviceMx
        {
            get
            {
                dev.EleID =txtbh.Text;
                dev.Name = txtnm.Text;
                dev.Flag = (int)spts.Value;
                dev.RateVolt = Convert.ToDouble(comboBox1.Text);
                dev.Num2 = Convert.ToDouble(sprl.Value);
                dev.AreaID = lookUpEdit1.EditValue.ToString();
                dev.OperationYear = comboBoxEdit1.Text;
                dev.Date1 = date1.Text;
                dev.Date2 = date2.Text;
                //dev.Type = ;
                return dev;
            }
            set  
            {
                dev = value;
                txtbh.Text=dev.EleID;
                txtnm.Text=dev.Name ;
                spts.Value=(decimal)dev.Flag;
                comboBox1.Text=dev.RateVolt.ToString();
                sprl.Value=(decimal)dev.Num2;
                lookUpEdit1.EditValue = dev.AreaID;
                comboBoxEdit1.Text = dev.OperationYear;
                date1.Text = dev.Date1;
                date2.Text = dev.Date2;
            }
        }
        public frmPWdlg()
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
            if (this.Text != "ÅäµçÊÒÐÅÏ¢")
            {
                spts.Properties.ReadOnly = true;
            }
           // comboBoxEdit1.Text = DateTime.Today.Year.ToString();
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
            string sql = " where RateVolt=" + comboBox1.Text + " and type='05' and ProjectID='" + Itop.Client.MIS.ProgUID + "'";
            IList list = Services.BaseService.GetList("SelectPSPDEVByCondition", sql);
            lookUpEdit1.Properties.DataSource = list;

        }
    }
}