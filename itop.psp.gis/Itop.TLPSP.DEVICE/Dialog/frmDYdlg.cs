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
        //�ж�ͼ�㴫����������һ�������
        public string StartYear = "";
        public PSP_PowerSubstation_Info DeviceMx {
            get {

                devObj.Title = textEdit1.Text;
                devObj.S1 =spinEdit1.Value.ToString();
                devObj.S2 = spinEdit2.Value.ToString();
                devObj.S3 = comboBoxEdit1.Text;
                devObj.Flag = comboBoxEdit2.Text == "�滮" ? "2" : "1";
                devObj.S5 = dq.Text;
                if (Area.EditValue!=null)
                {
                    devObj.S9 = Area.EditValue.ToString();
                }
                
                devObj.S10 = nylx.Text;
                devObj.S11 = fdl.Text;
                devObj.S12 = fdxss.Text;
                devObj.S13 = cyd.Text;
                devObj.S14 = td.Text;
                devObj.S8 = type2.Text;
                devObj.S29 = date1.Text;
                devObj.S30 = date2.Text;
                devObj.S18 = textEdit2.Text;
                devObj.AreaID = ProjectID;
                return devObj; }
            set {
                devObj = value;
                textEdit1.Text = devObj.Title;
                ProjectID = Itop.Client.MIS.ProgUID;
                try { spinEdit1.Value = decimal.Parse(devObj.S1); } catch { }
                try {spinEdit2.Value =decimal.Parse(devObj.S2);  } catch { }
                comboBoxEdit1.Text = devObj.S3;
                comboBoxEdit2.Text = devObj.Flag == "2" ? "�滮" :"��״" ;
                dq.Text= devObj.S5;
                ProjectID = Itop.Client.MIS.ProgUID;
                Area.EditValue= devObj.S9;
                nylx.Text = devObj.S10;
                fdl.Text = devObj.S11;
                fdxss.Text = devObj.S12;
                cyd.Text = devObj.S13;
                td.Text = devObj.S14;
                textEdit2.Text = devObj.S18;
                type2.Text = devObj.S8;
                date1.Text = devObj.S29;
                date2.Text = devObj.S30;

                if (!string.IsNullOrEmpty(StartYear))
                {
                    string sql = "RelatetableID='" + DeviceMx.UID + "' order by startYear";
                    IList<Psp_Attachtable> pl = Itop.Client.Common.Services.BaseService.GetList<Psp_Attachtable>("SelectPsp_AttachtableByCont", sql);
                    if (pl.Count > 0)
                    {
                        double rl = 0;
                        int ts = 0;
                        foreach (Psp_Attachtable pa in pl)
                        {
                            if (Convert.ToInt32(pa.startYear) <= Convert.ToInt32(StartYear) && Convert.ToInt32(pa.endYear) >= Convert.ToInt32(StartYear))
                            {
                                rl += Convert.ToDouble(pa.ZHI);
                                ts++;
                            }
                        }
                        spinEdit2.Value = (decimal)rl;
                        
                    }
                }
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
           date1.Text= DateTime.Today.Year.ToString();
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

        private void comboBoxEdit1_SelectedValueChanged(object sender, EventArgs e)
        {
            date1.Text = comboBoxEdit1.Text;
            if (Convert.ToInt32(comboBoxEdit1.Text) <= DateTime.Now.Year)
            {
                comboBoxEdit2.Text = "��״";
            }
            else
            {
                comboBoxEdit2.Text = "�滮";
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            double rl = 0;
            int bts = 0;
            FrmAttachtable frm = new FrmAttachtable();
            frm.Type = "0";
            frm.ParentID= DeviceMx.UID;
            frm.StartYear = DeviceMx.S29;
            frm.EndYear = DeviceMx.S30;
            frm.RelateTable = "PSP_PowerSubstation_Info";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = frm.datatable;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["S2"].ToString() == "�½�" || dt.Rows[i]["S2"].ToString() == "����" || dt.Rows[i]["S2"].ToString() == "Ͷ��")
                        {
                            if (!string.IsNullOrEmpty(DeviceMx.S29) && !string.IsNullOrEmpty(DeviceMx.S30))
                            {
                                if (Convert.ToInt32(dt.Rows[i]["startYear"]) >= Convert.ToInt32(DeviceMx.S29) && Convert.ToInt32(dt.Rows[i]["startYear"]) <= Convert.ToInt32(DeviceMx.S30))
                                {
                                    rl += Convert.ToDouble(dt.Rows[i]["ZHI"]);
                                    bts++;
                                }
                            }
                            else
                            {
                                rl += Convert.ToDouble(dt.Rows[i]["ZHI"]);
                                bts++;
                            }

                        }
                    }

                }
                spinEdit2.Value = (decimal)rl;
               
            }
        }
        public bool bcflag = false;
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(devObj.Title))
            {
                MessageBox.Show("���Ʋ���Ϊ�գ�");
                return;
            }
            if (string.IsNullOrEmpty(devObj.S3))
            {
                MessageBox.Show("��ѡ��Ͷ��ʱ�䣡");
                return;
            }
            if (Convert.ToDouble(devObj.S1) == 0)
            {
                MessageBox.Show("����д��ѹ�ȼ���");
                return;
            }
            PSP_PowerSubstation_Info obj = UCDeviceBase.DataService.GetOneByKey<PSP_PowerSubstation_Info>(DeviceMx);
            if (obj==null)
            {
                UCDeviceBase.DataService.Create<PSP_PowerSubstation_Info>(DeviceMx);
            }
            double rl = 0;
            int bts = 0;
            frmDeviceManager_children frmc = new frmDeviceManager_children();
            frmc.ParentObj = DeviceMx;
            string[] types = new string[] { "01", "02", "04" };
            frmc.childrendevice(types);
            if (frmc.DialogResult==DialogResult.OK)
            {
                string where = "where projectid='" + Itop.Client.MIS.ProgUID + "'and type='02'and SvgUID='" + DeviceMx.UID + "'";
                IList<PSPDEV> list = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", where);
                foreach (PSPDEV pd in list)
                {
                    if (!string.IsNullOrEmpty(pd.OperationYear) && !string.IsNullOrEmpty(pd.Date2) && pd.Date2.Length == 4 && !string.IsNullOrEmpty(DeviceMx.S29) && !string.IsNullOrEmpty(DeviceMx.S30))
                    {
                        if (Convert.ToInt32(pd.OperationYear) >= Convert.ToInt32(DeviceMx.S29) && Convert.ToInt32(pd.Date2) <= Convert.ToInt32(DeviceMx.S30))
                        {
                            rl +=(double) pd.Burthen;
                            bts++;
                        }
                    }
                    else
                    {
                        rl += (double)pd.Burthen;
                        bts++;
                    }
                }
                if (list.Count>0)
                {
                    spinEdit2.Value = (decimal)rl;
                }
                
                
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (!isread)
            {
                if (string.IsNullOrEmpty(textEdit1.Text))
                {
                    MessageBox.Show("���Ʋ���Ϊ�գ�");
                    return;
                }
                if (string.IsNullOrEmpty(comboBoxEdit1.Text))
                {
                    MessageBox.Show("��ѡ��Ͷ��ʱ�䣡");
                    return;
                }
                if (spinEdit1.Value == 0)
                {
                    MessageBox.Show("����д��ѹ�ȼ���");
                    return;
                }
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}