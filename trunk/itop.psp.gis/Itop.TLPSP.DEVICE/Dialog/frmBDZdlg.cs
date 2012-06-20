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
    /// 变电站属性编辑窗口
    /// </summary>
    public partial class frmBDZdlg : Itop.Client.Base.FormBase
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


        public frmBDZdlg() {
            InitializeComponent();
            gridControl.EmbeddedNavigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(EmbeddedNavigator_ButtonClick);
            Init();
               
        }

        void EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            if(e.Button.Tag.ToString()=="增加"){
                增加ToolStripMenuItem_Click(sender, new EventArgs());
            }
            if (e.Button.Tag.ToString() == "删除")
            {
                删除ToolStripMenuItem_Click(sender, new EventArgs());
            }
            if (e.Button.Tag.ToString() == "查看")
            {
                查看ToolStripMenuItem_Click(sender, new EventArgs());
            }
        }
        PSP_Substation_Info bdz = new PSP_Substation_Info();
        public bool IsTJ
        {//是否T接点
            get
            {
                return checkBox1.Checked;
            }
            set
            {
                checkBox1.Checked = value;
            }
        }
        //判断图层传过来的是哪一年的数据
        public string StartYear = "";
        public PSP_Substation_Info DeviceMx {
            get {

                bdz.Title = textEdit1.Text;
                bdz.L1 =(int)spinEdit1.Value;
                bdz.L2 = (double)spinEdit2.Value;
                bdz.L4 = textEdit2.Text;
                bdz.L9 =(double) spinEdit3.Value;
                bdz.L10 = (double)spinEdit5.Value;
                bdz.L3 = (int)spinEdit4.Value;
                bdz.S2 = comboBoxEdit1.Text;
                bdz.Flag = comboBoxEdit2.Text == "规划" ? "2" : "1";
                if (comboBoxEdit8.Properties.GetKeyValueByDisplayText(comboBoxEdit8.Text) != null)
                {
                    bdz.DQ = comboBoxEdit8.Text;
                }
                bdz.S4 = comboBoxEdit3.Text;
                if (comboBoxEdit9.Properties.GetKeyValueByDisplayText(comboBoxEdit9.Text) != null)
                {
                    //bdz.AreaName = comboBoxEdit9.Properties.GetKeyValueByDisplayText(comboBoxEdit9.Text).ToString();
                    bdz.AreaName = comboBoxEdit9.Text;
                }
                bdz.L28 = date1.Text;
                bdz.L29 = date2.Text;
                return bdz; }
            set {
                bdz = value;
                textEdit1.Text = bdz.Title;
                try { spinEdit1.Value = bdz.L1; } catch { }
                try {spinEdit2.Value =(decimal) bdz.L2;  } catch { }
                textEdit2.Text = bdz.L4;
                try { spinEdit3.Value = Convert.ToDecimal(bdz.L9); }
                catch { }
                try { spinEdit4.Value = (decimal)bdz.L3; }
                catch { }
                try { spinEdit5.Value = (decimal)bdz.L10; }
                catch { }
                comboBoxEdit1.Text = bdz.S2; 
                comboBoxEdit2.Text = bdz.Flag == "2" ? "规划" :"现状" ;
                comboBoxEdit8.EditValue= bdz.DQ;
                comboBoxEdit9.EditValue = bdz.AreaName;
                comboBoxEdit3.Text = bdz.S4;
                date1.Text = bdz.L28;
                date2.Text = bdz.L29;
                ucGraph1.Open(value.UID);
                ucGraph2.Open(value.UID+Itop.Client.MIS.ProgUID.Substring(0,8));
                freshxl();
                if (!string.IsNullOrEmpty(StartYear))
                {
                    string sql = "RelatetableID='" + bdz.UID + "' order by startYear";
                    IList<Psp_Attachtable> pl = Itop.Client.Common.Services.BaseService.GetList<Psp_Attachtable>("SelectPsp_AttachtableByCont", sql);
                    if (pl.Count > 0)
                    {
                        double rl = 0;
                        int ts = 0;
                        foreach (Psp_Attachtable pa in pl)
                        {
                            if (!string.IsNullOrEmpty(pa.startYear) && !string.IsNullOrEmpty(pa.endYear))
                            {
                                if (Convert.ToInt32(pa.startYear) <= Convert.ToInt32(StartYear) && Convert.ToInt32(pa.endYear) >= Convert.ToInt32(StartYear))
                                {
                                    rl += Convert.ToDouble(pa.ZHI);
                                    ts++;
                                }
                            }
                            else
                            {
                                rl += Convert.ToDouble(pa.ZHI);
                                ts++;
                            }
                            
                        }
                        spinEdit2.Value = (decimal)rl;
                        spinEdit4.Value=(decimal)ts;
                    }
                }
            }
        }
        /// <summary>
        /// 初始母线列表
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
            date1.Text = DateTime.Today.Year.ToString();
            if (DeviceHelper.xml1 == null)
            {
                textEdit1.Properties.Buttons[0].Visible = false;
            }

            string ss = " ProjectID='" + Itop.Client.MIS.ProgUID + "' ";
            IList listq = UCDeviceBase.DataService.GetList("SelectPS_Table_AreaWHByConn", ss);
            comboBoxEdit9.Properties.DataSource = listq;

             ss = " ProjectID='" + Itop.Client.MIS.ProgUID + "' ";
             listq = UCDeviceBase.DataService.GetList("SelectPS_Table_Area_TYPEByConn", ss);
            comboBoxEdit8.Properties.DataSource = listq;
          
            
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
            StartYear = "";
            //spinEdit3.Text = burthen.ToString();
            spinEdit3.Text =DeviceMx.L9.ToString();
        }
        string projectid;
        public string ProjectID {
            get { return projectid; }
            set { projectid = value; }
        }
        bool isread = true;
        /// <summary>
        /// 是否只读，为true时图形不能保存
        /// </summary>
        public bool IsRead {
            get { return isread; }
            set { isread = value; }
        }
        private void simpleButton1_Click(object sender, EventArgs e) {
            if (!isread) {
                ucGraph1.Save(DeviceMx.UID,DeviceMx.AreaID=="");
                ucGraph2.Save("", DeviceMx.AreaID == "");
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
                            sb.ObligateField3 = "规划";
                        }
                        else if (((PSP_Substation_Info)obj).Flag == "1")
                        {
                            sb.ObligateField3 = "现行";
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
                            sb.ObligateField3 = "规划";
                        }
                        else if (((PSP_Substation_Info)obj).Flag == "1")
                        {
                            sb.ObligateField3 = "现行";
                        }
                        Services.BaseService.Create<substation>(sb);
                    }
                }
                this.Close();
                return;

            }

            this.Close();
        }

        private void 增加ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void 查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PSP_ImgInfo obj = FocusedObject;
            if (obj == null)
            {
                return;
            }
            if (MessageBox.Show("确定要删除么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
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

        private void ucGraph1_Load(object sender, EventArgs e)
        {

        }

        private void comboBoxEdit1_SelectedValueChanged(object sender, EventArgs e)
        {
            date1.Text = comboBoxEdit1.Text;
            if (Convert.ToInt32(comboBoxEdit1.Text)<=DateTime.Now.Year)
            {
                comboBoxEdit2.Text = "现状";
            }
            else
            {
                comboBoxEdit2.Text = "规划";
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            double rl = 0;
            int bts = 0;
            FrmAttachtable frm = new FrmAttachtable();
            frm.Type = "1";
            frm.ParentID = bdz.UID;
            frm.StartYear = bdz.L28;
            frm.EndYear = bdz.L29;
            frm.RelateTable = "PSP_Substation_Info";
            if (frm.ShowDialog()==DialogResult.OK)
            {
                DataTable dt = frm.datatable;
                if (dt.Rows.Count>0)
                {
                    for (int i = 0; i < dt.Rows.Count;i++ )
                    {
                        if (dt.Rows[i]["S2"].ToString() == "新建" || dt.Rows[i]["S2"].ToString() == "扩容" || dt.Rows[i]["S2"].ToString() == "投产")
                        {
                            if (!string.IsNullOrEmpty(bdz.L28) && !string.IsNullOrEmpty(bdz.L29))
                            {
                                if (Convert.ToInt32(dt.Rows[i]["startYear"]) >= Convert.ToInt32(bdz.L28) && Convert.ToInt32(dt.Rows[i]["startYear"]) <= Convert.ToInt32(bdz.L29))
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
               spinEdit2.Value=(decimal)rl;
              spinEdit4.Value =(decimal)bts;
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            frmDeviceManager_children frmc = new frmDeviceManager_children();
            frmc.ParentObj = DeviceMx;
            string[] types=new string[]{"01","03","12"};
            frmc.childrendevice(types);

        }
       
    }
}