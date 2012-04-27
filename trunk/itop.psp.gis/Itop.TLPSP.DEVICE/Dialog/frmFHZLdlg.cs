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
    public partial class frmFHZLdlg : Itop.Client.Base.FormBase
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
                if (lookUpEdit1.EditValue!=null)
                {
                    dev.AreaID = lookUpEdit1.EditValue.ToString();
                }
                if (lookUpEdit2.EditValue!=null)
                {
                    dev.IName = lookUpEdit2.EditValue.ToString();
                }
             
                dev.OperationYear = comboBoxEdit1.Text;
                //dev.Burthen = Convert.ToDecimal(sprl.Value);             
                dev.HgFlag = textEdit2.Text;
                dev.Number = (int)spinEdit1.Value;
                //dev.Date1 = date1.Text;
                //dev.Date2 = date2.Text;
                dev.ISwitch = Iswitch.ToString();
                dev.JSwitch = Jswitch.ToString();
                dev.HuganTQ1 = (double)spinEdit2.Value;
                dev.HuganTQ2 = (double)spinEdit3.Value;
                dev.HuganTQ4 = (double)spinEdit4.Value;
          

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
                lookUpEdit2.EditValue = dev.IName;
                comboBoxEdit1.Text = dev.OperationYear;
                textEdit2.Text = dev.HgFlag;
                spinEdit1.Value = (decimal)dev.Number;
                int f = 1;
                int.TryParse(dev.ISwitch, out f);
                Iswitch = f;
                f = 1;
                int.TryParse(dev.JSwitch, out f);
                Jswitch = f;

                //date1.Text = dev.Date1;
                //date2.Text = dev.Date2;
                spinEdit2.Value = (decimal)spinEdit2.Value;
                spinEdit3.Value = (decimal)spinEdit3.Value;
                spinEdit4.Value = (decimal)spinEdit4.Value;
             
                //sprl.Value=(decimal)dev.Burthen;
            }
        }
        public frmFHZLdlg()
        {
            InitializeComponent();
        }
        public int Iswitch
        {
            get
            {
                return radioGroup2.SelectedIndex;
            }
            set
            {
                radioGroup2.SelectedIndex = value;
            }
        }
        public int Jswitch
        {
            get
            {
                return radioGroup1.SelectedIndex;
            }
            set
            {
                radioGroup1.SelectedIndex = value;
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            intdata();
            initList();
        }
        public void intdata()
        {
            string sql = " where RateVolt=" + comboBox1.Text + " and (type='05' or type ='73') and ProjectID='" + Itop.Client.MIS.ProgUID + "'";
            IList list = Services.BaseService.GetList("SelectPSPDEVByCondition", sql);
            lookUpEdit1.Properties.DataSource = list;
            sql = "type ='70' and ProjectID='" + Itop.Client.MIS.ProgUID + "'";
             list = Services.BaseService.GetList("SelectPSPDEVByCondition", sql);
            lookUpEdit2.Properties.DataSource = list;
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
        private void initList()
        {
            string sql = "where AreaID='" + dev.SUID + "' and projectid='" + Itop.Client.MIS.ProgUID + "' ORDER BY Number";
            IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            // Itop.Common.DataConverter.ToDataTable(list,typeof(PSPDEV));
            //listBoxControl1.DataSource = list;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = " where RateVolt=" + comboBox1.Text + " and type='05' and ProjectID='"+Itop.Client.MIS.ProgUID+"'";
            IList list = Services.BaseService.GetList("SelectPSPDEVByCondition", sql);
            lookUpEdit1.Properties.DataSource = list;

        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除设备吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                //UCDeviceBase.DataService.Delete<PSPDEV>(listBoxControl1.SelectedItem as PSPDEV);
                initList();
            }

        }
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            frmDeviceSelect dlg = new frmDeviceSelect();
            dlg.InitDeviceType("70", "71", "72", "57", "59", "61", "62", "63", "64");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Dictionary<string, object> dic = dlg.GetSelectedDevice();
                PSPDEV devzx = dic["device"] as PSPDEV;
                devzx.AreaID = dev.SUID;
                Services.BaseService.Update<PSPDEV>(devzx);
                initList();
            }
        }

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {
            if (radioGroup2.SelectedIndex == 0)
            {
                e.Graphics.Clear(Color.Red);
            }
            else
            {
                e.Graphics.Clear(Color.Green);
            }          
        }

        private void lookUpEdit1_Properties_Click(object sender, EventArgs e)
        {
             frmDeviceSelect dlg = new frmDeviceSelect();
            dlg.InitDeviceType("05", "73");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Dictionary<string, object> dic = dlg.GetSelectedDevice();
                PSPDEV devzx = dic["device"] as PSPDEV;
                lookUpEdit1.EditValue = dev.SUID;
                string sql = " where  (type ='70') and AreaID='" + devzx.SUID+ "'";
                IList list = Services.BaseService.GetList("SelectPSPDEVByCondition", sql);
                lookUpEdit2.Properties.DataSource = list;
                
            }
        }

    }
}