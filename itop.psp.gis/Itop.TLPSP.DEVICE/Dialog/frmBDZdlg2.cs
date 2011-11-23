using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using System.Collections;
using System.IO;

namespace Itop.TLPSP.DEVICE 
{
    /// <summary>
    /// ���վ���Ա༭����
    /// </summary>
    public partial class frmBDZdlg2 : Itop.Client.Base.FormBase
    {

        decimal burthen = 0;
        private string uid = "";

        public string Uid
        {
            get { return uid; }
            set { uid = value; }
        }
        private string layerid = "";

        public string Layerid
        {
            get { return layerid; }
            set { layerid = value; }
        }
        private string eleid = "";

        public string Eleid
        {
            get { return eleid; }
            set { eleid = value; }
        }


        public frmBDZdlg2() {
            InitializeComponent();
            Init();
               
        }
        PSP_Substation_Info bdz = new PSP_Substation_Info();
        public bool IsTJ
        {//�Ƿ�T�ӵ�
            get
            {
                return checkBox1.Checked;
            }
            set
            {
                checkBox1.Checked = value;
            }
        }
        public PSP_Substation_Info DeviceMx {
            get {

                bdz.Title = textEdit1.Text;
                bdz.L1 =(int)spinEdit1.Value;
                bdz.L2 = (double)spinEdit2.Value;
                bdz.L4 = textEdit2.Text;
                bdz.L9 =(double) spinEdit3.Value;
                bdz.L3 = (int)spinEdit4.Value;
                bdz.S2 = comboBoxEdit1.Text;
                bdz.Flag = comboBoxEdit2.Text == "�滮" ? "2" : "1";
                bdz.DQ = comboBoxEdit8.Text;
                bdz.S4 = comboBoxEdit3.Text;
                if (comboBoxEdit9.Properties.GetKeyValueByDisplayText(comboBoxEdit9.Text) != null)
                {
                    //bdz.AreaName = comboBoxEdit9.Properties.GetKeyValueByDisplayText(comboBoxEdit9.Text).ToString();
                    bdz.AreaName = comboBoxEdit9.Text;
                }

                bdz.L15 = n1.Value.ToString();
                bdz.L16 = n2.Value.ToString();
                bdz.L17 = n3.Value.ToString();
                bdz.L18 = n4.Value.ToString();
                bdz.L19 = n5.Value.ToString();
                bdz.L20 = n6.Value.ToString();
                bdz.L21 = n7.Value.ToString();
                bdz.L22 = n8.Value.ToString();
                bdz.L23 = n9.Value.ToString();
                bdz.L24 = n10.Value.ToString();
                bdz.L25 = n11.Value.ToString();
                bdz.L26 = n12.Value.ToString();
                bdz.L27 = n13.Value.ToString();
                bdz.L28 = n14.Value.ToString();
                bdz.L29 = n15.Value.ToString();
                bdz.L11 = drq.Value.ToString();
                bdz.L12 = drqgc.Text;
                bdz.L13 = dkq.Value.ToString();
                bdz.L14 = dkqgc.Text;
                bdz.S10 = jxfs.Text;
                return bdz; }
            set {
                bdz = value;
                textEdit1.Text = bdz.Title;
                try { spinEdit1.Value = bdz.L1; } catch { }
                try {spinEdit2.Value =(decimal) bdz.L2;  } catch { }
                textEdit2.Text = bdz.L4;
                //try { spinEdit3.Value = Convert.ToDecimal(bdz.L9); }
                //catch { }
                try { spinEdit4.Value = (decimal)bdz.L3; }
                catch { }
                comboBoxEdit1.Text = bdz.S2; 
                comboBoxEdit2.Text = bdz.Flag == "2" ? "�滮" :"��״" ;
                comboBoxEdit8.Text = bdz.DQ;
                comboBoxEdit9.EditValue = bdz.AreaName;
                comboBoxEdit3.Text = bdz.S4;

                if (bdz.L15!=""){
                    n1.Value=Convert.ToDecimal(bdz.L15);
                }
                if (bdz.L16 != "")
                {
                    n2.Value = Convert.ToDecimal(bdz.L16);
                }
                if (bdz.L17 != "")
                {
                    n3.Value = Convert.ToDecimal(bdz.L17);
                }
                if (bdz.L18 != "")
                {
                    n4.Value = Convert.ToDecimal(bdz.L18);
                }
                if (bdz.L19 != "")
                {
                    n5.Value = Convert.ToDecimal(bdz.L19);
                }
                if (bdz.L20 != "")
                {
                    n6.Value = Convert.ToDecimal(bdz.L20);
                }
                if (bdz.L21 != "")
                {
                    n7.Value = Convert.ToDecimal(bdz.L21);
                }
                if (bdz.L22 != "")
                {
                    n8.Value = Convert.ToDecimal(bdz.L22);
                }
                if (bdz.L23 != "")
                {
                    n9.Value = Convert.ToDecimal(bdz.L23);
                }
                if (bdz.L24 != "")
                {
                    n10.Value = Convert.ToDecimal(bdz.L24);
                }
                if (bdz.L25 != "")
                {
                    n11.Value = Convert.ToDecimal(bdz.L25);
                }
                if (bdz.L26 != "")
                {
                    n12.Value = Convert.ToDecimal(bdz.L26);
                }
                if (bdz.L27 != "")
                {
                    n13.Value = Convert.ToDecimal(bdz.L27);
                }
                if (bdz.L28 != "")
                {
                    n14.Value = Convert.ToDecimal(bdz.L28);
                }
                if (bdz.L29 != "")
                {
                    n15.Value = Convert.ToDecimal(bdz.L29);
                }
                if (bdz.L11 != "")
                {
                    drq.Value = Convert.ToDecimal(bdz.L11);
                }
               
                drqgc.Text=bdz.L12;
                if (bdz.L13 != "")
                {
                    dkq.Value = Convert.ToDecimal(bdz.L13);
                }

                dkqgc.Text = bdz.L14;
                jxfs.Text=bdz.S10;

                ucGraph1.Open(value.UID);
                freshxl();
            }
        }
        /// <summary>
        /// ��ʼĸ���б�
        /// </summary>
        private void freshxl() {
            string sql = "where svguid='"+bdz.UID+"' and projectid='" + Itop.Client.MIS.ProgUID + "' and Type='01' ORDER BY Number";
            IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            listBoxControl1.Items.Clear();
            foreach (PSPDEV dev in list) {
                listBoxControl1.Items.Add(dev.Name);
            }
        }
        protected void Init()
        {
            object o = new object();
            for (int i = -30; i <= 30; i++) {
                o = System.DateTime.Now.Year + i;
                comboBoxEdit1.Properties.Items.Add(o);
                
            }
            comboBoxEdit1.Text = DateTime.Today.Year.ToString();
            if (DeviceHelper.xml1 == null)
            {
                textEdit1.Properties.Buttons[0].Visible = false;
            }

            string ss = " ProjectID='" + Itop.Client.MIS.ProgUID + "' ";
            IList listq = UCDeviceBase.DataService.GetList("SelectPS_Table_AreaWHByConn", ss);
            comboBoxEdit9.Properties.DataSource = listq;

            
          

        }
        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {
                   
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RefreshData(DeviceMx.UID);
            PSPDEV p = new PSPDEV();
            if (DeviceHelper.xml1 != null)
            {
                p.SvgUID = DeviceHelper.xml1.GetAttribute("Deviceid");
            }
            else
            {
                p.SvgUID = DeviceMx.UID;
            }
            p.Type = "01";
            if (p.SvgUID == "") return;
            IList l2 = UCDeviceBase.DataService.GetList("SelectPSPDEVBySvgUIDAndType", p);
            for (int i = 0; i < l2.Count; i++)
            {
                PSPDEV _dev1 = (PSPDEV)l2[i];
                string _sql = " where (IName='" + _dev1.Name + "' or JName='" + _dev1.Name + "') and type='05' and ProjectID='"+Itop.Client.MIS.ProgUID+"' ";
                IList l3 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", _sql);
                for (int j = 0; j < l3.Count; j++)
                {
                    burthen = burthen + ((PSPDEV)l3[j]).Burthen;
                }
            }
            //spinEdit3.Text = burthen.ToString();
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
        private void simpleButton1_Click(object sender, EventArgs e) {
            if (!isread) {
                ucGraph1.Save(DeviceMx.UID,DeviceMx.AreaID=="");
            }
        }

        private void mc_Properties_Click(object sender, EventArgs e)
        {
          
        }

        private void textEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (DeviceHelper.xml1 == null) { return; }
            object obj = null;
            obj = DeviceHelper.SelectDevice(DeviceType.BDZ, Itop.Client.MIS.ProgUID);

            if (obj is PSP_Substation_Info)
            {
                string deviceid = ((PSP_Substation_Info)obj).UID;
                ((PSP_Substation_Info)obj).LayerID = layerid;
                ((PSP_Substation_Info)obj).EleID = eleid;
                Services.BaseService.Update<PSP_Substation_Info>(((PSP_Substation_Info)obj));

                if (DeviceHelper.xml1 != null)
                {
                    DeviceHelper.xml1.SetAttribute("Deviceid", deviceid);
                    substation sb = new substation();
                    sb.UID = ((PSP_Substation_Info)obj).UID;
                    sb = (substation)Services.BaseService.GetObject("SelectsubstationByKey", sb);
                    if (sb != null)
                    {

                        sb.SvgUID = uid;
                        sb.LayerID = layerid;
                        sb.EleID = DeviceHelper.xml1.GetAttribute("id");
                        if (((PSP_Substation_Info)obj).Flag == "2")
                        {
                            sb.ObligateField3 = "�滮";
                        }
                        else if (((PSP_Substation_Info)obj).Flag == "1")
                        {
                            sb.ObligateField3 = "����";
                        }
                        Services.BaseService.Update<substation>(sb);
                    }
                    else
                    {
                        sb = new substation();
                        sb.UID = ((PSP_Substation_Info)obj).UID;
                        sb.SvgUID = uid;
                        sb.EleID = DeviceHelper.xml1.GetAttribute("id");
                        sb.LayerID = layerid;
                        if (((PSP_Substation_Info)obj).Flag == "2")
                        {
                            sb.ObligateField3 = "�滮";
                        }
                        else if (((PSP_Substation_Info)obj).Flag == "1")
                        {
                            sb.ObligateField3 = "����";
                        }
                        Services.BaseService.Create<substation>(sb);
                    }
                }
                this.Close();
                return;

            }

            this.Close();
        }

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmImgAdd f = new frmImgAdd();
            f.Uid = DeviceMx.UID;
            if (f.ShowDialog() == DialogResult.OK)
            {
                RefreshData(DeviceMx.UID);
            }
        }
        public bool RefreshData(string id)
        {
            try
            {
                PSP_ImgInfo img = new PSP_ImgInfo();
                img.TreeID = id;
                IList<PSP_ImgInfo> list = Services.BaseService.GetList<PSP_ImgInfo>("SelectPSP_ImgInfoList", img);

                this.gridControl.DataSource = list;
            }
            catch (Exception exc)
            {
                //HandleException.TryCatch(exc);
                return false;
            }

            return true;
        }

        private void gridControl_DoubleClick(object sender, EventArgs e)
        {
            OpenFile();
        }
        public PSP_ImgInfo FocusedObject
        {
            get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PSP_ImgInfo; }
        }
        public void OpenFile()
        {
            PSP_ImgInfo obj = FocusedObject;
            if (obj == null)
            {
                return;
            }
            obj = Services.BaseService.GetOneByKey<PSP_ImgInfo>(obj.UID);
            BinaryWriter bw;
            FileStream fs;
            try
            {
                byte[] bt = obj.Image;
                string filename = obj.Name;
                fs = new FileStream("C:\\" + filename, FileMode.Create, FileAccess.Write);
                bw = new BinaryWriter(fs);
                bw.Write(bt);
                bw.Flush();
                bw.Close();
                fs.Close();
                System.Diagnostics.Process.Start("C:\\" + filename);

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void �鿴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void ɾ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PSP_ImgInfo obj = FocusedObject;
            if (obj == null)
            {
                return;
            }
            if (MessageBox.Show("ȷ��Ҫɾ��ô��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
            {
                Services.BaseService.Update("DeletePSP_ImgInfo", obj);
                RefreshData(DeviceMx.UID);

            }
        }

        private void simpleButton3_Click(object sender, EventArgs e) {
            frmDeviceSelect dlg = new frmDeviceSelect();
            dlg.InitDeviceType("01");
            if (dlg.ShowDialog() == DialogResult.OK) {
                Dictionary<string,object> dic= dlg.GetSelectedDevice();
                PSPDEV dev = dic["device"] as PSPDEV;
                dev.SvgUID = bdz.UID;
                Services.BaseService.Update<PSPDEV>(dev);
                freshxl();
            }
        }

        private void spinEdit9_EditValueChanged(object sender, EventArgs e)
        {

        }
       
    }
}