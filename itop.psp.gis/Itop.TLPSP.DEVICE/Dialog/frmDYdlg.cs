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
using Itop.Domain.Table;
namespace Itop.TLPSP.DEVICE
{
    /// <summary>
    /// ��Դ���Ա༭����
    /// </summary>
    public partial class frmDYdlg : Itop.Client.Base.FormBase
    {

        public frmDYdlg() {
            InitializeComponent();
            Init();
            xtraTabPage2.PageVisible = false;//��
        }
        PSP_PowerSubstation_Info devObj = new PSP_PowerSubstation_Info();
        public PSP_PowerSubstation_Info DeviceMx {
            get {

                devObj.Title = textEdit1.Text;
                devObj.S1 =spinEdit1.Value.ToString();
                devObj.S2 = spinEdit2.Value.ToString();
                devObj.S3 = comboBoxEdit1.Text;
                devObj.Flag = comboBoxEdit2.Text == "�滮" ? "2" : "1";
                devObj.S5 = dq.Text;
                devObj.S9 = Area.Text;
                devObj.S10 = nylx.Text;
                devObj.S11 = fdl.Text;
                devObj.S12 = fdxss.Text;
                devObj.S13 = cyd.Text;
                devObj.S14 = td.Text;
                devObj.S8 = type2.Text;
                devObj.S29 = date1.Text;
                devObj.S30 = date2.Text;
                return devObj; }
            set {
                devObj = value;
                textEdit1.Text = devObj.Title;
                try { spinEdit1.Value = decimal.Parse(devObj.S1); } catch { }
                try {spinEdit2.Value =decimal.Parse(devObj.S2);  } catch { }
                comboBoxEdit1.Text = devObj.S3;
                comboBoxEdit2.Text = devObj.Flag == "2" ? "�滮" :"��״" ;
                dq.Text= devObj.S5;
                Area.Text = devObj.S9;
                nylx.Text = devObj.S10;
                fdl.Text = devObj.S11;
                fdxss.Text = devObj.S12;
                cyd.Text = devObj.S13;
                td.Text = devObj.S14;
                type2.Text = devObj.S8;
                date1.Text = devObj.S29;
                date2.Text = devObj.S30;
            }
        }
        protected void Init()
        {
            object o = new object();
            for (int i = -30; i <= 30; i++) {
                o = System.DateTime.Now.Year + i;
                comboBoxEdit1.Properties.Items.Add(o);
                
            }
            for (int i = -30; i <= 30; i++)
            {
                o = System.DateTime.Now.Year + i;
                date1.Properties.Items.Add(o);

            }
            for (int i = -30; i <= 30; i++)
            {
                o = System.DateTime.Now.Year + i;
                date2.Properties.Items.Add(o);

            }
            comboBoxEdit1.Text = DateTime.Today.Year.ToString();
            string ss = " ProjectID='" + Itop.Client.MIS.ProgUID + "' ";
            IList listq = UCDeviceBase.DataService.GetList("SelectPS_Table_AreaWHByConn", ss);
            Area.Properties.DataSource = listq;

             ss = " ProjectID='" + Itop.Client.MIS.ProgUID + "' ";
             listq = UCDeviceBase.DataService.GetList("SelectPS_Table_Area_TYPEByConn", ss);
            foreach (PS_Table_Area_TYPE at in listq)
            {
                dq.Properties.Items.Add(at.Title);
            }
            

            
        }
        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {
                   
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            freshxl();
        }
        string projectid;
        public string ProjectID {
            get { return projectid; }
            set { projectid = value; }
        }
        bool isread = true;
        /// <summary>
        /// �Ƿ�ֻ����Ϊtrueʱͼ�β��ܱ���
        /// </summary>
        public bool IsRead {
            get { return isread; }
            set { isread = value; }
        }

       

        private void simpleButton3_Click(object sender, EventArgs e) {
            frmDeviceSelect dlg = new frmDeviceSelect();
            dlg.InitDeviceType("01");
            if (dlg.ShowDialog() == DialogResult.OK) {
                Dictionary<string, object> dic = dlg.GetSelectedDevice();
                PSPDEV dev = dic["device"] as PSPDEV;
                dev.SvgUID = devObj.UID;
                Services.BaseService.Update<PSPDEV>(dev);
                freshxl();
            }
        }
        Dictionary<string, PSPDEV> dicDev = new Dictionary<string, PSPDEV>();
        private void freshxl() {
            string sql = "where svguid='" + devObj.UID + "' and projectid='" + Itop.Client.MIS.ProgUID + "' and Type='01' ORDER BY Number";
            IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            listBoxControl1.Items.Clear();
            dicDev.Clear();
            foreach (PSPDEV dev in list) {
                listBoxControl1.Items.Add(dev.Name);
                dicDev.Add(dev.Name, dev);
            }
        }

        private void listBoxControl1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Delete) {
                if (listBoxControl1.SelectedIndex >= 0) {
                    PSPDEV dev = dicDev[listBoxControl1.SelectedItem.ToString()];
                    if (dev != null) {
                        dev.SvgUID = "";
                        Services.BaseService.Update<PSPDEV>(dev);
                        listBoxControl1.Items.RemoveAt(listBoxControl1.SelectedIndex);
                    }
                }
            }
        }
    }
}