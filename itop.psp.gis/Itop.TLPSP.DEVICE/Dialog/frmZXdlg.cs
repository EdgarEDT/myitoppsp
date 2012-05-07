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
    public partial class frmZXdlg : Itop.Client.Base.FormBase
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
                dev.Type = "70";
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
        public frmZXdlg()
        {
            InitializeComponent();
            initList();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            intdata();
            initList();
        }
        private void initList()
        {
            string sql = "where AreaID='" + dev.SUID + "' and projectid='" + Itop.Client.MIS.ProgUID + "' ORDER BY Number";
            IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
           // Itop.Common.DataConverter.ToDataTable(list,typeof(PSPDEV));
            listBoxControl1.DataSource = list;
        }
        public void intdata()
        {
            string sql = " where (type='05' or type ='73') and ProjectID='" + Itop.Client.MIS.ProgUID + "'";
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
        public bool newflag = false;
        string projectid;
        public string ProjectID
        {
            get { return projectid; }
            set { projectid = value; }
        }
     
        public string AreaID
        {
            get { return dev.AreaID; }
            set {lookUpEdit1.EditValue = value; }
        }
        public int Number
        {
            get { return (int)spinEdit1.Value; }
            set { spinEdit1.Value = Convert.ToDecimal(value); }
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
            string sql = " where RateVolt=" + comboBox1.Text + " and type in('05','73')and ProjectID='"+Itop.Client.MIS.ProgUID+"'";
            IList list = Services.BaseService.GetList("SelectPSPDEVByCondition", sql);
            lookUpEdit1.Properties.DataSource = list;

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除设备吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                UCDeviceBase.DataService.Delete<PSPDEV>(listBoxControl1.SelectedItem as PSPDEV);
                initList();
            }

        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            frmDeviceSelect dlg = new frmDeviceSelect();
            dlg.InitDeviceType("70", "71", "72","57", "59", "61", "62", "63", "64"); //判断设备是否在杆塔上
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Dictionary<string, object> dic = dlg.GetSelectedDevice();
                PSPDEV devzx = dic["device"] as PSPDEV;
                devzx.AreaID = dev.SUID;
                Services.BaseService.Update<PSPDEV>(devzx);
                initList();
            }
        }

        private void lookUpEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis) {
                frmNodeTreedlg dlg= new frmNodeTreedlg();
                dlg.Pspdev = this.DeviceMx;
                if (dlg.ShowDialog() == DialogResult.OK) {
                    if (dlg.Pspdev != null) {
                        lookUpEdit1.EditValue = dlg.Pspdev.SUID;
                    }
                }
            }
        }
    }
}