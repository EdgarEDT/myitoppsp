using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Domain.Table;
using Itop.Client.Projects;
using System.Collections;
using System.Configuration;
using System.IO;

namespace Itop.TLPSP.DEVICE
{
    /// <summary> 
    /// 母线属性编辑窗口
    /// </summary> 
    public partial class frmXLdlg : Itop.Client.Base.FormBase
    {
        PSPDEV dev = new PSPDEV();
        public IList<glebeProperty> glist = null;
        
        public PSPDEV DeviceMx
        {
            get{
                dev.Name = textEdit1.Text;
                dev.Number = (int)spinEdit8.Value;
                dev.RateVolt = (double)spinEdit5.Value;
                dev.ReferenceVolt = (double)ReferenceVolt;
                dev.OperationYear = OperationYear;
                dev.Type = "05";
                dev.VoltR = (double)VoltR;
                //dev.VoltV = (double)VoltV;
                dev.UnitFlag = UnitFlag.ToString();
                dev.KSwitchStatus = KswitchStatus.ToString();
                dev.LineType = comboBoxEdit2.Text;
                dev.LineLength = (double)spinEdit6.Value;
                //dev.NodeType = NodeType.ToString();
                dev.IName = comboBoxEdit3.Text;
                dev.JName = comboBoxEdit4.Text;
                dev.ISwitch = comboBoxEdit5.Text;
                dev.JSwitch = comboBoxEdit6.Text;
                dev.LineR = (double)spinEdit16.Value;
                dev.LineTQ = (double)spinEdit15.Value;
                dev.LineGNDC = (double)spinEdit14.Value;
                dev.ZeroR = (double)spinEdit4.Value;
                dev.ZeroTQ = (double)spinEdit3.Value;
                dev.ZeroGNDC = (double)spinEdit2.Value;
                dev.Burthen = spinEdit17.Value;
                dev.iV = (double)spinEdit21.Value;
                dev.jV = (double)spinEdit22.Value;
                dev.HuganTQ1 = (double)spinEdit18.Value;
                dev.JXFS = comboBoxEdit7.Text;
                dev.DQ = comboBoxEdit8.Text;
                dev.LineType2 = comlinetype2.Text;
                dev.Length2 =Convert.ToDouble(splength2.Value);
                dev.LLFS = comllfs.Text;
                dev.SwitchNum = Convert.ToInt32(spkg.Value);
                dev.Date1 = date1.Text;
                dev.Date2 = date2.Text;
                if (comboBoxEdit9.Properties.GetKeyValueByDisplayText(comboBoxEdit9.Text) != null)
                {
                    dev.AreaID = comboBoxEdit9.Properties.GetKeyValueByDisplayText(comboBoxEdit9.Text).ToString();
                }
                return dev;
            }
            set{
                dev = value;
                Name = dev.Name;
                Number = dev.Number;
                RateVolt = (decimal)dev.RateVolt;
                ReferenceVolt = (decimal)dev.ReferenceVolt;
                OperationYear = dev.OperationYear;               
                VoltR = (decimal)dev.VoltR;
                //VoltV = (decimal)dev.VoltV;
                int f = 0;
                if (dev.UnitFlag == "p.u."||dev.UnitFlag=="0")
                {
                    dev.UnitFlag = "0";
                }
                else
                {
                    dev.UnitFlag = "1";

                }
                int.TryParse(dev.UnitFlag,out f);
                UnitFlag = f;
                f = 0;
                if (dev.KSwitchStatus== "投入运行" || dev.KSwitchStatus == "0")
                {
                    dev.KSwitchStatus = "0";
                }
                else
                {
                    dev.KSwitchStatus = "1";

                }
                int.TryParse(dev.KSwitchStatus,out f);
                KswitchStatus = f;
                f = 0;
                int.TryParse(dev.NodeType,out f);

                comboBoxEdit2.Text = dev.LineType;
                spinEdit6.Value = (decimal)dev.LineLength;
                comboBoxEdit3.Text = dev.IName;
                comboBoxEdit4.Text = dev.JName;
                comboBoxEdit5.Text = dev.ISwitch;
                comboBoxEdit6.Text = dev.JSwitch;
                spinEdit16.Value = (decimal)dev.LineR;
                spinEdit15.Value = (decimal)dev.LineTQ;
                spinEdit14.Value = (decimal)dev.LineGNDC;
                spinEdit4.Value = (decimal)dev.ZeroR;
                spinEdit3.Value = (decimal)dev.ZeroTQ;
                spinEdit2.Value = (decimal)dev.ZeroGNDC;
                spinEdit17.Value = (decimal)dev.Burthen;
                spinEdit21.Value = (decimal)dev.iV;
                spinEdit22.Value = (decimal)dev.jV;
                spinEdit18.Value = (decimal)dev.HuganTQ1;
                comboBoxEdit7.Text = dev.JXFS;
                comboBoxEdit8.Text = dev.DQ;
                comboBoxEdit9.EditValue = dev.AreaID;  
                comlinetype2.Text = dev.LineType2;
                splength2.Value = Convert.ToDecimal(dev.Length2);
                comllfs.Text = dev.LLFS;
                date1.Text = dev.Date1;
                date2.Text = dev.Date2;
                spkg.Value = Convert.ToDecimal(dev.SwitchNum);
                setXL();
                setLineName();
                setBdzName();
                //NodeType = f;    
            }
        }
        protected void setXL()
        {
            WireCategory rc = new WireCategory();
            rc.WireLevel = DeviceMx.RateVolt.ToString();
            rc.WireType = DeviceMx.LineType;
            rc.Type = "40";
            rc = (WireCategory)UCDeviceBase.DataService.GetObject("SelectWireCategoryByKeyANDWireLevel", rc);
            if (rc!=null)
            {
                spinEdit1.Value = (decimal)rc.WireR;
                spinEdit7.Value = (decimal)rc.WireTQ;
                spinEdit9.Value = (decimal)rc.WireGNDC;
                spinEdit13.Value = (decimal)rc.ZeroR;
                spinEdit12.Value = (decimal)rc.ZeroTQ;
                spinEdit10.Value = (decimal)rc.ZeroGNDC;
                spinEdit17.Value = (decimal)rc.WireChange;
            }
        }
        public frmXLdlg() {
            InitializeComponent();
            gridControl.EmbeddedNavigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(EmbeddedNavigator_ButtonClick);
            radioGroup1.SelectedIndexChanged += new EventHandler(radioGroup1_SelectedIndexChanged);
        }

        void EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            if (e.Button.Tag.ToString() == "增加")
            {
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
        protected void Init()
        {
            object o = new object();
            for (int i = -30; i <= 30; i++)
            {
                o = System.DateTime.Now.Year + i;
                comboBoxEdit1.Properties.Items.Add(o);
            }
            for (int i = -30; i <= 30; i++)
            {
                o = System.DateTime.Now.Year + i;
                date1.Properties.Items.Add(o);
            } for (int i = -30; i <= 30; i++)
            {
                o = System.DateTime.Now.Year + i;
                date2.Properties.Items.Add(o);
            }
            WireCategory wirewire = new WireCategory();
            wirewire.Type = "40";
            IList list1 = null;
            if (DeviceMx.RateVolt != 0)
            {
                wirewire.WireLevel = DeviceMx.RateVolt.ToString();
                
                list1 = UCDeviceBase.DataService.GetList("SelectWireCategoryListBYWireLevel", wirewire);
            }
            else
                list1 = UCDeviceBase.DataService.GetList("SelectWireCategoryList", wirewire);
            foreach (WireCategory sub in list1)
            {
                if (comboBoxEdit2.Properties.Items.IndexOf(sub.WireType)==-1)
                {
                    comboBoxEdit2.Properties.Items.Add(sub.WireType);
                }
            }

            string ss = " ProjectID='" +Itop.Client.MIS.ProgUID+ "' ";
            IList listq = UCDeviceBase.DataService.GetList("SelectPS_Table_AreaWHByConn", ss);
            comboBoxEdit9.Properties.DataSource = listq;

            ss = " ProjectID='" + Itop.Client.MIS.ProgUID + "' ";
            listq = UCDeviceBase.DataService.GetList("SelectPS_Table_Area_TYPEByConn", ss);
            foreach (PS_Table_Area_TYPE at in listq)
            {
                comboBoxEdit8.Properties.Items.Add(at.Title);
            }
           // comboBoxEdit9.Properties.DataSource = listq;

            string con = " where Type='01'and ProjectID='" + this.ProjectSUID + "' order by name";
            IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            foreach (PSPDEV dev in list)
            {
                if (comboBoxEdit3.Properties.Items.IndexOf(dev.Name)==-1)
                {
                    comboBoxEdit3.Properties.Items.Add(dev.Name);
                    comboBoxEdit4.Properties.Items.Add(dev.Name);
                }
            }
            con = " where Type='07'and ProjectID='" + this.ProjectSUID + "' order by name";
            list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            foreach (PSPDEV dev in list)
            {
                if (comboBoxEdit5.Properties.Items.IndexOf(dev.Name) == -1)
                {
                    comboBoxEdit5.Properties.Items.Add(dev.Name);
                    comboBoxEdit6.Properties.Items.Add(dev.Name);
                }
            }
            if (DeviceHelper.xml1 == null)
            {
                textEdit1.Properties.Buttons[0].Visible = false;
            }
        }
        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {
            if (radioGroup1.SelectedIndex==0)
            {
                e.Graphics.Clear(Color.Red);
            } 
            else
            {
                e.Graphics.Clear(Color.Green);
            }            
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Init();
           
            RefreshData(dev.SUID);
            if (glist != null)
            {
                gridControl1.DataSource = glist;
            }
        }

        void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelControl1.Refresh();
        }
        #region 属性
        /// <summary>
        /// 母线名称
        /// </summary>
        public string Name 
        {
            get
            {
                return textEdit1.Text;
            }
            set
            {
                textEdit1.Text = value;
            }
        }
        protected string projectID;
        public string ProjectSUID
        {
            get{
                return Itop.Client.MIS.ProgUID;
            }
            set {
                projectID = value;
            }
        }
        /// <summary>
        /// 母线编号
        /// </summary>
        public decimal Number
        {
            get
            {
                return spinEdit8.Value;
            }
            set
            {
                spinEdit8.Value = value;
            }
        }       
        /// <summary>
        /// 额定电压
        /// </summary>
        public decimal RateVolt
        {
            get
            {
                return spinEdit5.Value;
            }
            set
            {
                spinEdit5.Value = value;
            }
        }
        /// <summary>
        /// 基准电压
        /// </summary>
        public decimal ReferenceVolt
        {
            get
            {
                return spinEdit11.Value;
            }
            set
            {
                spinEdit11.Value = value;
            }
        }
        /// <summary>
        /// 投产年份
        /// </summary>
        public string OperationYear
        {
            get
            {
                return comboBoxEdit1.Text;
            }
            set
            {
                comboBoxEdit1.Text = value;
            }
        }
        /// <summary>
        /// 电压幅值
        /// </summary>
        public decimal VoltR
        {
            get
            {
                return spinEdit6.Value;
            }
            set
            {
                spinEdit6.Value = value;
            }
        }
        /// <summary>
        /// 电压相角
        /// </summary>
        //public decimal VoltV
        //{
        //    get
        //    {
        //        return spinEdit7.Value;
        //    }
        //    set
        //    {
        //        spinEdit7.Value = value;
        //    }
        //}
        /// <summary>
        /// 短路容量
        /// </summary>
        public decimal Burthen
        {
            get
            {
                return spinEdit17.Value;
            }
            set
            {
                spinEdit17.Value = value;
            }
        }
        /// <summary>
        /// 发电有功
        /// </summary>
        public decimal OutP
        {
            get
            {
                return spinEdit1.Value;
            }
            set
            {
                spinEdit1.Value = value;
            }
        }
        /// <summary>
        /// 发电无功
        /// </summary>
        public decimal OutQ
        {
            get
            {
                return spinEdit2.Value;
            }
            set
            {
                spinEdit2.Value = value;
            }
        }
        /// <summary>
        /// 负荷有功
        /// </summary>
        public decimal InPutP
        {
            get
            {
                return spinEdit3.Value;
            }
            set
            {
                spinEdit3.Value = value;
            }
        }
        /// <summary>
        /// 负荷无功
        /// </summary>
        public decimal InPutQ
        {
            get
            {
                return spinEdit4.Value;
            }
            set
            {
                spinEdit4.Value = value;
            }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public int UnitFlag
        {
            get
            {
                return radioGroup3.SelectedIndex;
            }
            set
            {
                radioGroup3.SelectedIndex = value;
            }
        }
        /// <summary>
        /// 运行状态
        /// </summary>
        public int KswitchStatus
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
        public string LineType2
        {
            get { return comlinetype2.Text; }
            set { comlinetype2.Text = value; }
        }
        public double Length2
        {
            get { return Convert.ToDouble(splength2.Value); }
            set { splength2.Value = Convert.ToDecimal(value); }
        }
        public string LLFS
        {
            get { return comllfs.Text; }
            set { comllfs.Text = value; }
        }
        public int SwitchNum
        {
            get { return Convert.ToInt32(spkg.Value); }
            set { spkg.Value = Convert.ToDecimal(value); }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (radioGroup3.SelectedIndex==0)
            {
                spinEdit16.Value = spinEdit1.Value * spinEdit6.Value * 100 / (spinEdit11.Value * spinEdit11.Value);
                spinEdit15.Value = spinEdit7.Value * spinEdit6.Value * 100 / (spinEdit11.Value * spinEdit11.Value);
                spinEdit14.Value = spinEdit9.Value * spinEdit6.Value * (spinEdit11.Value * spinEdit11.Value)/100000000/2;
                spinEdit4.Value = spinEdit13.Value * spinEdit6.Value * 100 / (spinEdit11.Value * spinEdit11.Value);
                spinEdit3.Value = spinEdit12.Value * spinEdit6.Value * 100 / (spinEdit11.Value * spinEdit11.Value);
                spinEdit2.Value = spinEdit10.Value * spinEdit6.Value * (spinEdit11.Value * spinEdit11.Value) / 100000000 / 2;
            } 
            else
            {
                spinEdit16.Value = spinEdit1.Value * spinEdit6.Value;
                spinEdit15.Value = spinEdit7.Value * spinEdit6.Value;
                spinEdit14.Value = spinEdit9.Value * spinEdit6.Value / 2;
                spinEdit4.Value = spinEdit13.Value * spinEdit6.Value;
                spinEdit3.Value = spinEdit12.Value * spinEdit6.Value;
                spinEdit2.Value = spinEdit10.Value * spinEdit6.Value / 2;
            }
        }

        private void spinEdit5_Leave(object sender, EventArgs e)
        {

            if (comboBoxEdit2.SelectedText != null && comboBoxEdit2.SelectedText != "")
            {
                setXL();
            }            
        }

        private void comboBoxEdit2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBoxEdit2.SelectedText != null && comboBoxEdit2.SelectedText != "")
            {
                setXL();
            } 
        }

        private void comboBoxEdit3_SelectedIndexChanged(object sender, EventArgs e)
        {          
            PSPDEV devMX = new PSPDEV();
            string strCon = strCon = " WHERE Name = '" + comboBoxEdit3.Text + "' AND ProjectID = '" + this.ProjectSUID + "' AND Type = '01'";             
            devMX = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", strCon);
            if (devMX != null)
            {
                dev.FirstNode = devMX.Number;
                RateVolt =(decimal) devMX.RateVolt;
                ReferenceVolt = (decimal)devMX.ReferenceVolt;
            }
        }

        private void comboBoxEdit4_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSPDEV devMX = new PSPDEV();
            string strCon = strCon = " WHERE Name = '" + comboBoxEdit4.Text + "' AND ProjectID = '" + this.ProjectSUID + "' AND Type = '01'";
            devMX = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", strCon);
            if (devMX != null)
            {
                dev.LastNode = devMX.Number;
            }
        }

        private void mc_Properties_Click(object sender, EventArgs e)
        {
           
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (DeviceHelper.xml1 != null)
            {
                string styleValue = "";
                string ghType = ConfigurationSettings.AppSettings.Get("ghType");
                LineType lt = new LineType();
                lt.TypeName = dev.RateVolt.ToString("###") + "kV";
                lt = (LineType)Services.BaseService.GetObject("SelectLineTypeByTypeName", lt);
                if (lt != null)
                {
                    if (string.IsNullOrEmpty(comboBoxEdit1.Text))
                    {
                        styleValue = "stroke-dasharray:" + ghType + ";stroke-width:" + lt.ObligateField1 + ";";
                    }
                    else
                    {
                        if (Convert.ToInt32(comboBoxEdit1.Text) > DateTime.Now.Year)
                        {
                            styleValue = "stroke-dasharray:" + ghType + ";stroke-width:" + lt.ObligateField1 + ";";
                        }
                        else
                        {
                            styleValue = "stroke-width:" + lt.ObligateField1 + ";";
                        }
                    }
                   
                    //string aa= ColorTranslator.ToHtml(Color.Black);
                    styleValue = styleValue + "stroke:" + ColorTranslator.ToHtml(Color.FromArgb(Convert.ToInt32(lt.Color)));
                    //SvgElement se = DeviceHelper.xml1;
                    DeviceHelper.xml1.RemoveAttribute("style");
                    DeviceHelper.xml1.SetAttribute("style", styleValue);
                    DeviceHelper.xml1.SetAttribute("info-name", dev.Name);
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (DeviceHelper.xml1 == null) { return; }
            PSPDEV obj = new PSPDEV();
            string deviceid = DeviceHelper.xml1.GetAttribute("Deviceid");
            DeviceHelper.pspflag = false;
            DeviceHelper.Wjghflag = false;
            //if (string.IsNullOrEmpty(deviceid))
            //{
            obj = (PSPDEV)DeviceHelper.SelectDevice(DeviceType.XL, Itop.Client.MIS.ProgUID);

            if (obj is PSPDEV)
            {
                deviceid = ((PSPDEV)obj).SUID;

                obj.LayerID = DeviceHelper.layerid;
                obj.SvgUID = DeviceHelper.uid;
                obj.EleID = DeviceHelper.eleid;
                Services.BaseService.Update<PSPDEV>(obj);

                if (DeviceHelper.xml1 != null)
                {
                    DeviceHelper.xml1.SetAttribute("Deviceid", deviceid);
                    string styleValue = "";
                    string ghType = ConfigurationSettings.AppSettings.Get("ghType");
                    LineType lt = new LineType();
                    lt.TypeName = obj.RateVolt.ToString("###") + "kV";
                    lt = (LineType)Services.BaseService.GetObject("SelectLineTypeByTypeName", lt);
                    if (lt != null)
                    {
                        if (Convert.ToInt32(obj.OperationYear) > DateTime.Now.Year)
                        {
                            styleValue = "stroke-dasharray:" + ghType + ";stroke-width:" + lt.ObligateField1 + ";";
                        }
                        else
                        {
                            styleValue = "stroke-width:" + lt.ObligateField1 + ";";
                        }
                        //string aa= ColorTranslator.ToHtml(Color.Black);
                        styleValue = styleValue + "stroke:" + ColorTranslator.ToHtml(Color.FromArgb(Convert.ToInt32(lt.Color)));
                        //SvgElement se = DeviceHelper.xml1;
                        DeviceHelper.xml1.RemoveAttribute("style");
                        DeviceHelper.xml1.SetAttribute("style", styleValue);
                        DeviceHelper.xml1.SetAttribute("info-name", obj.Name);
                    }
                }
            }

            this.Close();
        }

        private void 增加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmImgAdd f = new frmImgAdd();
            f.Uid = dev.SUID;
            if (f.ShowDialog() == DialogResult.OK)
            {
                RefreshData(dev.SUID);
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
                System.Diagnostics.Process.Start("C:\\" + filename );

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
                Services.BaseService.Update("DeletePSP_ImgInfo",obj);
                RefreshData(dev.SUID);
                
            }
        }

        private void xtraTabPage1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void setBdzName()
        {
            //显示所在位置的名称
            object obj = DeviceHelper.GetDevice<PSP_Substation_Info>(dev.SvgUID);

            if (obj != null)
            {
                buttonEdit2.Text = ((PSP_Substation_Info)obj).Title;
                return;
            }
            obj = DeviceHelper.GetDevice<PSP_PowerSubstation_Info>(dev.SvgUID);
            if (obj != null)
            {
                buttonEdit2.Text = ((PSP_PowerSubstation_Info)obj).Title;
                return;
            }

        }
        private void setLineName()
        {
            //显示所在位置的名称
            object obj = DeviceHelper.GetDevice<PSPDEV>(dev.Lable);

            if (obj != null)
            {
                buttonEdit1.Text = ((PSPDEV)obj).Name;
                return;
            }

        }
        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmDeviceSelect dlg = new frmDeviceSelect();


            dlg.InitDeviceType("05");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Dictionary<string, object> dic = dlg.GetSelectedDevice();
                buttonEdit1.Text = dic["name"].ToString();
                dev.Lable = dic["id"].ToString();
            }
        }

        private void buttonEdit2_Click(object sender, EventArgs e)
        {
            frmDeviceSelect dlg = new frmDeviceSelect();


            dlg.InitDeviceType("20","30");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Dictionary<string, object> dic = dlg.GetSelectedDevice();
                buttonEdit2.Text = dic["name"].ToString();
                dev.SvgUID = dic["id"].ToString();
            }
        }

        private void frmXLdlg_Load(object sender, EventArgs e)
        {
           // this.Activate();
           // this.xtraTabControl1.Focus();
           // this.xtraTabPage1.Focus();
           // this.groupBox1.Focus();
           //bool flag= textEdit1.Focus();
           //textEdit1.SelectAll();

        }

        /// <summary>
        /// 母线类型
        /// </summary>
        //public int NodeType
        //{
        //    get
        //    {
        //        return radioGroup2.SelectedIndex;
        //    }
        //    set
        //    {
        //        radioGroup2.SelectedIndex = value;
        //    }
        //}
        #endregion  
    }
}