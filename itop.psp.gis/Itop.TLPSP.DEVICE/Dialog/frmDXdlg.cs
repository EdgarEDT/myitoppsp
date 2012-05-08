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
    public partial class frmDXdlg : Itop.Client.Base.FormBase
    {
        PSPDEV dev = new PSPDEV();
        public IList<glebeProperty> glist = null;
        public bool newflag = false; //是否为新加的 false为修改 ture为新加的 创建一个可靠性分析的方案
        public PSPDEV DeviceMx
        {
            get{
                dev.Name = textEdit1.Text;
                dev.Number = (int)spinEdit8.Value;
                dev.RateVolt = (double)spinEdit5.Value;
                dev.ReferenceVolt = (double)ReferenceVolt;
                dev.OperationYear = OperationYear;
                dev.Type = "73";
                dev.VoltR = (double)VoltR;
                //dev.VoltV = (double)VoltV;
                dev.UnitFlag = UnitFlag.ToString();
                dev.KSwitchStatus = KswitchStatus.ToString();
                dev.LineType = comboBoxEdit2.Text;
                dev.LineLength = (double)spinEdit6.Value;
                //dev.NodeType = NodeType.ToString();
               // dev.IName = comboBoxEdit3.Text;
               // dev.JName = comboBoxEdit4.Text;
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
                if (lookUpEdit1.EditValue!=null)
                {
                    dev.HuganLine1 = lookUpEdit1.EditValue.ToString();
                }
                getLineName();
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
                int.TryParse(dev.UnitFlag,out f);
                UnitFlag = f;
                f = 0;
                int.TryParse(dev.KSwitchStatus,out f);
                KswitchStatus = f;
                f = 0;
                int.TryParse(dev.NodeType,out f);

                comboBoxEdit2.Text = dev.LineType;
                spinEdit6.Value = (decimal)dev.LineLength;
               // comboBoxEdit3.Text = dev.IName;
               // comboBoxEdit4.Text = dev.JName;
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
                //创建节点信息
                ucjd1.ParentObj = dev;
                setXL();
                setLineName();
                lookUpEdit1.EditValue = dev.HuganLine1;
                setBdzName();
                //NodeType = f;    
            }
        }
        protected void setXL()
        {
            WireCategory rc = new WireCategory();
            rc.WireLevel = dev.RateVolt.ToString();
            rc.WireType = dev.LineType;
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
        public frmDXdlg() {
            InitializeComponent();           
            radioGroup1.SelectedIndexChanged += new EventHandler(radioGroup1_SelectedIndexChanged);
            
        }
        protected void Init()
        {
            object o = new object();
            for (int i = -30; i <= 30; i++)
            {
                o = System.DateTime.Now.Year + i;
                comboBoxEdit1.Properties.Items.Add(o);
            }
            WireCategory wirewire = new WireCategory();
            wirewire.Type = "40";
            IList list1 = null;
            if (dev.RateVolt != 0)
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
            foreach (PSPDEV dev1 in list)
            {
                if (comboBoxEdit3.Properties.Items.IndexOf(dev.Name)==-1)
                {
                    comboBoxEdit3.Properties.Items.Add(dev1.Name);
                    comboBoxEdit4.Properties.Items.Add(dev1.Name);
                }
            }
            con = " where Type='07'and ProjectID='" + this.ProjectSUID + "' order by name";
            list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            foreach (PSPDEV dev1 in list)
            {
                if (comboBoxEdit5.Properties.Items.IndexOf(dev1.Name) == -1)
                {
                    comboBoxEdit5.Properties.Items.Add(dev1.Name);
                    comboBoxEdit6.Properties.Items.Add(dev1.Name);
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
        string parentid;
        public string ParentID
        {
            get { return parentid; }
            set { parentid = value; }
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

            
            }
            catch (Exception exc)
            {
                //HandleException.TryCatch(exc);
                return false;
            }

            return true;
        }

        private void xtraTabPage1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void setBdzName()
        {
            //显示所在位置的名称
            //显示所在位置的名称
            object obj = DeviceHelper.GetDevice<PSPDEV>(dev.IName);

            if (obj != null)
            {
                buttonEdit1.Text = ((PSPDEV)obj).Name;
                return;
            }

        }
        private void getLineName()
        {
            string sql = "where Name='" + buttonEdit2.Text+ "' and type in('73','05') and ProjectID='"+projectID+"'";
            IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            if (list.Count>0)
            {
                dev.JName = list[0].SUID;
            }
            else
            {
                dev.JName = "";
            }
        }
        private void setLineName()
        {
            //显示所在位置的名称
            if (string.IsNullOrEmpty(dev.JName)&&!string.IsNullOrEmpty(parentid))
            {
                dev.JName = parentid;
            }
            object obj = DeviceHelper.GetDevice<PSPDEV>(dev.JName);
            //获得上级线路的节点信息
            string sql = "where AreaID='" + dev.JName + "' and type='70' ORDER BY Number";
            IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            lookUpEdit1.Properties.DataSource = list;
            if (obj != null)
            {
                buttonEdit2.Text = ((PSPDEV)obj).Name;
                return;
            }

        }
        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmDeviceSelect dlg = new frmDeviceSelect();


            //dlg.InitDeviceType("05","07","54","55","56","57","62","63","64","70","71","73");
            dlg.InitDeviceType("01");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Dictionary<string, object> dic = dlg.GetSelectedDevice();
                buttonEdit1.Text = dic["name"].ToString();
                dev.IName = dic["id"].ToString();
            }
        }

        private void buttonEdit2_Click(object sender, EventArgs e)
        {
            frmDeviceSelect dlg = new frmDeviceSelect();


           // dlg.InitDeviceType("05", "07", "54", "55", "56", "57", "62", "63", "64", "70", "71", "73");
             dlg.InitDeviceType("05","73");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Dictionary<string, object> dic = dlg.GetSelectedDevice();

                if (!string.IsNullOrEmpty(dic["id"].ToString()))
                {
                    if (dic["id"].ToString()==dev.SUID)
                    {
                        MessageBox.Show("不能选自己作为父节支路！请重新选择");
                        return;
                    }
                    buttonEdit2.Text = dic["name"].ToString();
                    dev.JName = dic["id"].ToString();
                    string sql = "where AreaID='" + dic["id"].ToString() + "' and type='70' ORDER BY Number";
                    IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
                    lookUpEdit1.Properties.DataSource = list;
                }
               
            }
            //此线路的节点编号 极为该线路的首节点
        }
        //点击其他设备元件的时候
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage.Name == "xtraTabPage4")
            {
                if (newflag)
                {
                    //创建一个可靠性分析的方案 其id和馈线的ID相同。
                    Ps_pdreltype pdr = new Ps_pdreltype();
                    pdr.ID = dev.SUID;
                    pdr.ProjectID = this.projectID;
                    pdr.Createtime = DateTime.Now;
                    pdr.Title = textEdit1.Text;
                    pdr.S1 = dev.IName;
                    //pdr.PeopleSum = PDT.Peplesum;
                    //pdr.AreaName = PDT.Areaname;
                    //pdr.Year = PDT.Year;
                    Services.BaseService.Create<Ps_pdreltype>(pdr);
                    //创建电源
                    Ps_pdtypenode pn = new Ps_pdtypenode();
                    pn.pdreltypeid = pdr.ID;
                    pn.devicetype = "01";
                    PSPDEV devzx = new PSPDEV();
                    devzx.SUID = pdr.S1;
                    devzx = Services.BaseService.GetOneByKey<PSPDEV>(devzx);
                    if (devzx != null)
                    {
                        pn.title = devzx.Name;
                        pn.DeviceID = devzx.SUID;
                    }
                    else
                    {
                        pn.title = "无电源"+pdr.Title;

                    }
                    pn.Code = "0";
                    Services.BaseService.Create<Ps_pdtypenode>(pn);

                   //创建自身的元件树
                    Ps_pdtypenode pn1 = new Ps_pdtypenode();
                    pn1.ID = dev.SUID;
                    pn1.title = textEdit1.Text;
                    pn1.pdreltypeid = pdr.ID;
                    pn1.devicetype = "73";
                    pn1.DeviceID = dev.SUID;
                    pn1.ParentID = pn.ID;
                    pn1.Code = "111";
                    Services.BaseService.Create<Ps_pdtypenode>(pn1);
                    //如果有支路信息 则找到它的子类 创建到父类上
                    //if (!string.IsNullOrEmpty(dev.JName))
                    //{
                    //    pn = new Ps_pdtypenode();
                    //    pn.ID = dev.JName;
                    //    pn = Services.BaseService.GetOneByKey<Ps_pdtypenode>(pn);
                    //    if (pn!=null)
                    //    {
                    //        pn1 = new Ps_pdtypenode();
                           
                    //        pn1.title = textEdit1.Text;
                    //        pn1.pdreltypeid =pn.pdreltypeid;
                    //        pn1.devicetype = "73";
                    //        pn1.DeviceID = dev.SUID;
                    //        pn1.ParentID = pn.ID;
                    //        pn1.Code = "211";
                    //        Services.BaseService.Create<Ps_pdtypenode>(pn1);
                    //    }
                    //}
                    AddPDtypenode(pn1, "2", pn1.pdreltypeid);
                }
                else
                {
                    Ps_pdreltype pdr = new Ps_pdreltype();
                    pdr.ID = dev.SUID;
                    pdr = Services.BaseService.GetOneByKey<Ps_pdreltype>(pdr);
                    if (pdr!=null)
                    {
                        pdr.Title = textEdit1.Text;
                        pdr.S1 = dev.IName;
                        Services.BaseService.Update<Ps_pdreltype>(pdr);
                        //修改电源信息
                        IList<Ps_pdtypenode> list1 = Services.BaseService.GetList<Ps_pdtypenode>("SelectPs_pdtypenodeByCon", "pdreltypeid='" + pdr.ID + "' and devicetype = '01'");
                        Ps_pdtypenode pn = new Ps_pdtypenode();
                        if (list1.Count>0)
                      {
                          pn = list1[0];
                          pn.DeviceID = dev.IName;
                          Services.BaseService.Update<Ps_pdtypenode>(pn);
                      }
                        else
                      {
                        
                          pn.pdreltypeid = pdr.ID;
                          pn.devicetype = "01";
                          PSPDEV devzx = new PSPDEV();
                          devzx.SUID = pdr.S1;
                          devzx = Services.BaseService.GetOneByKey<PSPDEV>(devzx);
                          if (devzx != null)
                          {
                              pn.title = devzx.Name;
                              pn.DeviceID = devzx.SUID;
                          }
                          else
                          {
                              pn.title = "无电源" + pdr.Title;

                          }
                          pn.Code = "0";
                          Services.BaseService.Create<Ps_pdtypenode>(pn);
                      }
                      
                      //修改此馈线节点的信息

                      Ps_pdtypenode pdn = new Ps_pdtypenode();
                      pdn.ID = dev.SUID;
                      pdn = Services.BaseService.GetOneByKey<Ps_pdtypenode>(pdn);
                        if (pdn!=null)
                        {
                            pdn.title = dev.Name;
                            pdn.devicetype = "73";
                            pdn.ParentID = pn.ID;
                            Services.BaseService.Update<Ps_pdtypenode>(pdn);
                        }
                        else
                        {
                            pdn = new Ps_pdtypenode();
                            pdn.ID = dev.SUID;
                            pdn.title = textEdit1.Text;
                            pdn.pdreltypeid = pdr.ID;
                            pdn.devicetype = "73";
                            pdn.DeviceID = dev.SUID;
                            pdn.ParentID = pn.ID;
                            pdn.Code = "111";
                            Services.BaseService.Create<Ps_pdtypenode>(pdn);
                        }
                     //找到子类信息 修改子类信息
                        DeleteNode(pdn);
                        AddPDtypenode(pdn, "2", pdn.pdreltypeid);
                       
                    }
                    else
                    {
                        pdr = new Ps_pdreltype();
                        pdr.ID = dev.SUID;
                        pdr.ProjectID = this.projectID;
                        pdr.Createtime = DateTime.Now;
                        pdr.Title = textEdit1.Text;
                        pdr.S1 = dev.IName;
                        //pdr.PeopleSum = PDT.Peplesum;
                        //pdr.AreaName = PDT.Areaname;
                        //pdr.Year = PDT.Year;
                        Services.BaseService.Create<Ps_pdreltype>(pdr);
                        //创建电源
                        Ps_pdtypenode pn = new Ps_pdtypenode();
                        pn.pdreltypeid = pdr.ID;
                        pn.devicetype = "01";
                        PSPDEV devzx = new PSPDEV();
                        devzx.SUID = pdr.S1;
                        devzx = Services.BaseService.GetOneByKey<PSPDEV>(devzx);
                        if (devzx != null)
                        {
                            pn.title = devzx.Name;
                            pn.DeviceID = devzx.SUID;
                        }
                        else
                        {
                            pn.title = "无电源" + pdr.Title;

                        }
                        pn.Code = "0";
                        Services.BaseService.Create<Ps_pdtypenode>(pn);
                        //创建自身的元件树
                        Ps_pdtypenode pn1 = new Ps_pdtypenode();
                        pn1.ID = dev.SUID;
                        pn1.title = textEdit1.Text;
                        pn1.pdreltypeid = pdr.ID;
                        pn1.devicetype = "73";
                        pn1.DeviceID = dev.SUID;
                        pn1.ParentID = pn.ID;
                        pn1.Code = "111";
                        Services.BaseService.Create<Ps_pdtypenode>(pn1);
                        //if (!string.IsNullOrEmpty(dev.JName))
                        //{
                        //    pn = new Ps_pdtypenode();
                        //    pn.ID = dev.JName;
                        //    pn = Services.BaseService.GetOneByKey<Ps_pdtypenode>(pn);
                        //    if (pn != null)
                        //    {
                        //        pn1 = new Ps_pdtypenode();

                        //        pn1.title = textEdit1.Text;
                        //        pn1.pdreltypeid = pn.pdreltypeid;
                        //        pn1.devicetype = "73";
                        //        pn1.DeviceID = dev.SUID;
                        //        pn1.ParentID = pn.ID;
                        //        pn1.Code = "211";
                        //        Services.BaseService.Create<Ps_pdtypenode>(pn1);
                        //    }
                        //}
                        AddPDtypenode(pn1, "2", pn1.pdreltypeid);
                    }
                    
                    //pdr.PeopleSum = PDT.Peplesum;
                    //pdr.AreaName = PDT.Areaname;
                    //pdr.Year = PDT.Year;
                 
                   

                   
                }
                  //判断子支路是否有元件 如果没有 然后判断其有没有所带元件 加入
                //Ps_pdtypenode pnf = new Ps_pdtypenode();
                //pnf.ID = dev.SUID;
                //pnf = Services.BaseService.GetOneByKey<Ps_pdtypenode>(pnf);
                //   string sql = "where JName='" + dev.SUID + "' and type='73' ";
                //   IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
                //if (list.Count>0)
                //{
                //    int count = 0;
                //    foreach (PSPDEV ps in list)
                //    {
                //        count++;
                //        sql = "pdreltypeid='" + dev.SUID + "'and devicetype='73'and deviceID='" + ps.SUID + "'";
                //        Ps_pdtypenode ptn = UCDeviceBase.DataService.GetObject("SelectPs_pdtypenodeByCon", sql) as Ps_pdtypenode;
                //        if (ptn!=null)
                //        {
                //            //判断其附属的元件没有则添加
                //            AddPDtypenode(ps.SUID, (Convert.ToInt32(ptn.Code.Substring(0, 1)) + 1).ToString(), dev.SUID);
                //        }
                //        else
                //        {
                //            Ps_pdtypenode pn1 = new Ps_pdtypenode();

                //            pn1.title = ps.Name;
                //            pn1.pdreltypeid = dev.SUID;
                //            pn1.devicetype = "73";
                //            pn1.DeviceID = ps.SUID;
                //            pn1.ParentID = dev.SUID;
                //            pn1.Code =(Convert.ToInt32(pnf.Code.Substring(0,1))+1).ToString()+"1"+count.ToString();
                //            Services.BaseService.Create<Ps_pdtypenode>(pn1);
                //        }
                //    }
                //}
                ucdxchildnode1.DXObj = dev;
              
            }
        }
        //删除子类元件
        //删除元件
        public void DeleteNode(Ps_pdtypenode tln)
        {

           string sql = "ParentID='" + tln.ID + "'";
            IList<Ps_pdtypenode> list = UCDeviceBase.DataService.GetList<Ps_pdtypenode>("SelectPs_pdtypenodeByCon", sql);
            if (list.Count>0)
            {
                foreach (Ps_pdtypenode ps in list)
                {
                    DeleteNode(ps);
                    Services.BaseService.Delete<Ps_pdtypenode>(ps);
                }   
              
            }

        }
       //添加线路下面所有子元件
        private void AddPDtypenode(Ps_pdtypenode parentn,string level, string pdreltype)
        {
            //查找子线路
            int count = 0;
            string sql = "where JName='" + parentn.DeviceID + "'and type='73'";
            IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            foreach (PSPDEV ps in list)
            {

                sql = "pdreltypeid='" + pdreltype + "'and ParentID='" + parentn.ID + "'and DeviceID='" + ps.SUID + "'and devicetype='73'";
                Ps_pdtypenode ptn = UCDeviceBase.DataService.GetObject("SelectPs_pdtypenodeByCon", sql) as Ps_pdtypenode;
                if (ptn == null)
                {
                    count++;
                    ptn = new Ps_pdtypenode();

                    ptn.title = ps.Name;
                    ptn.pdreltypeid = pdreltype;
                    ptn.devicetype = "73";
                    ptn.DeviceID = ps.SUID;
                    ptn.ParentID = parentn.ID;
                    ptn.Code = level + "1" + count.ToString();
                    Services.BaseService.Create<Ps_pdtypenode>(ptn);

                }
                else
                {
                    ptn.ParentID = parentn.ID;
                    ptn.Code = level + "1" + count.ToString();
                    Services.BaseService.Update<Ps_pdtypenode>(ptn);
                }
                //加入它的下层元件
                AddPDtypenode(ptn, (Convert.ToInt32(level) + 1).ToString(), pdreltype);
            }
            //获线路的所有节点 根据其节点找到线路段 其中线路段是根据iname来寻找 负荷支路和联络线
            sql = "where AreaID='" + parentn.DeviceID + "'and type='70'and projectid='" + projectID + "' order by Number";
            IList<PSPDEV> listnode = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            string xgxlsuid = "('',", xgfhsuid = "('',", xgkxsuid = "('',";
            //找到和节点线连接的线路段
            foreach (PSPDEV nd in listnode)
            {
                sql = "where IName='" + nd.SUID + "'and AreaID='" + parentn.DeviceID + "'and type='74'";
                PSPDEV ps = UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", sql) as PSPDEV;
                if (ps!=null)
                {
                    sql = "pdreltypeid='" + pdreltype + "'and ParentID='" + parentn.ID + "'and DeviceID='" + ps.SUID + "'and devicetype='74'";
                    Ps_pdtypenode ptn = UCDeviceBase.DataService.GetObject("SelectPs_pdtypenodeByCon", sql) as Ps_pdtypenode;
                    if (ptn == null)
                    {
                        count++;
                        Ps_pdtypenode pn1 = new Ps_pdtypenode();

                        pn1.title = ps.Name;
                        pn1.pdreltypeid = pdreltype;
                        pn1.devicetype = "74";
                        pn1.DeviceID = ps.SUID;
                        pn1.ParentID = parentn.ID;
                        pn1.Code = level + "2" + count.ToString();
                        Services.BaseService.Create<Ps_pdtypenode>(pn1);
                        //添加断路器
                        sql = "where IName='" + ps.SUID + "'and type='06'";
                        int num = 0;
                        IList<PSPDEV> list1 = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
                        foreach (PSPDEV ps1 in list1)
                        {
                            num++;
                            Ps_pdtypenode pn2 = new Ps_pdtypenode();

                            pn2.title = ps.Name;
                            pn2.pdreltypeid = pdreltype;
                            pn2.devicetype = "06";
                            pn2.DeviceID = ps1.SUID;
                            pn2.ParentID = pn1.ID;
                            pn2.Code = (Convert.ToInt32(level) + 1).ToString() + "4" + num.ToString();
                            Services.BaseService.Create<Ps_pdtypenode>(pn2);
                        }

                        //添加隔离开关
                        sql = "where IName='" + ps.SUID + "'and type='55'";

                        list1 = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
                        foreach (PSPDEV ps1 in list1)
                        {
                            num++;
                            Ps_pdtypenode pn2 = new Ps_pdtypenode();

                            pn2.title = ps.Name;
                            pn2.pdreltypeid = pdreltype;
                            pn2.devicetype = "55";
                            pn2.DeviceID = ps1.SUID;
                            pn2.ParentID = pn1.ID;
                            pn2.Code = (Convert.ToInt32(level) + 1).ToString() + "6" + num.ToString();
                            Services.BaseService.Create<Ps_pdtypenode>(pn2);
                        }
                        xgxlsuid += "'" + ps.SUID + "',";
                    }
                }
                //负荷支路
                  sql = "where IName='" + nd.SUID + "'and AreaID='" + parentn.DeviceID + "'and type='80'";
                  ps = UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", sql) as PSPDEV;
                  if (ps != null)
                  {
                      sql = "pdreltypeid='" + pdreltype + "'and ParentID='" + parentn.ID + "'and DeviceID='" + ps.SUID + "'and devicetype='80'";
                      Ps_pdtypenode ptn = UCDeviceBase.DataService.GetObject("SelectPs_pdtypenodeByCon", sql) as Ps_pdtypenode;
                      if (ptn == null)
                      {
                          count++;
                          Ps_pdtypenode pn1 = new Ps_pdtypenode();

                          pn1.title = ps.Name;
                          pn1.pdreltypeid = pdreltype;
                          pn1.devicetype = "80";
                          pn1.DeviceID = ps.SUID;
                          pn1.ParentID = parentn.ID;
                          pn1.Code = level + "3" + count.ToString();
                          Services.BaseService.Create<Ps_pdtypenode>(pn1);
                      }
                      xgfhsuid += "'" + ps.SUID + "',";
                  }
              
                //创建联络线
                  sql = "where IName='" + parentn.DeviceID + "'or JName='" + parentn.DeviceID + " 'and type='75' and HuganLine1='" + nd.SUID + "'or HuganLine2='"+nd.SUID+"'";
                ps = UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", sql) as PSPDEV;
                  if (ps != null)
                  {
                      sql = "pdreltypeid='" + pdreltype + "'and ParentID='" + parentn.ID + "'and DeviceID='" + ps.SUID + "'and devicetype='75'";
                      Ps_pdtypenode ptn = UCDeviceBase.DataService.GetObject("SelectPs_pdtypenodeByCon", sql) as Ps_pdtypenode;
                      if (ptn == null)
                      {
                          count++;
                          Ps_pdtypenode pn1 = new Ps_pdtypenode();

                          pn1.title = ps.Name;
                          pn1.pdreltypeid = pdreltype;
                          pn1.devicetype = "75";
                          pn1.DeviceID = ps.SUID;
                          pn1.ParentID = parentn.ID;
                          pn1.Code = level + "5" + count.ToString();
                          Services.BaseService.Create<Ps_pdtypenode>(pn1);
                      }
                      xgxlsuid += "'" + ps.SUID + "',";
                  }
            }
            //去掉最后一个“，”
            xgxlsuid = xgxlsuid.Substring(0, xgxlsuid.LastIndexOf(','));
            xgxlsuid += ")";
            xgfhsuid = xgfhsuid.Substring(0, xgfhsuid.LastIndexOf(','));
            xgfhsuid += ")";
            xgkxsuid = xgkxsuid.Substring(0, xgkxsuid.LastIndexOf(','));
            xgkxsuid += ")";

            //创建没有查找线路段

            sql = "where AreaID='" + parentn.DeviceID + "'and type='74' and suid not in "+xgxlsuid+" order by FirstNode";
            list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            foreach (PSPDEV ps in list)
            {

                sql = "pdreltypeid='" + pdreltype + "'and ParentID='" + parentn.ID + "'and DeviceID='" + ps.SUID + "'and devicetype='74'";
                Ps_pdtypenode ptn = UCDeviceBase.DataService.GetObject("SelectPs_pdtypenodeByCon", sql) as Ps_pdtypenode;
                if (ptn == null)
                {
                    count++;
                    Ps_pdtypenode pn1 = new Ps_pdtypenode();

                    pn1.title ="节点有问题_"+ ps.Name;
                    pn1.pdreltypeid = pdreltype;
                    pn1.devicetype = "74";
                    pn1.DeviceID = ps.SUID;
                    pn1.ParentID = parentn.ID;
                    pn1.Code = level + "2" + count.ToString();
                    Services.BaseService.Create<Ps_pdtypenode>(pn1);
                    //添加断路器
                    sql = "where IName='" + ps.SUID + "'and type='06'";
                    int num = 0;
                    IList<PSPDEV> list1 = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
                    foreach (PSPDEV ps1 in list1)
                    {
                        num++;
                        Ps_pdtypenode pn2 = new Ps_pdtypenode();

                        pn2.title = ps.Name;
                        pn2.pdreltypeid = pdreltype;
                        pn2.devicetype = "06";
                        pn2.DeviceID = ps1.SUID;
                        pn2.ParentID = pn1.ID;
                        pn2.Code = (Convert.ToInt32(level) + 1).ToString() + "4" + num.ToString();
                        Services.BaseService.Create<Ps_pdtypenode>(pn2);
                    }

                    //添加隔离开关
                    sql = "where IName='" + ps.SUID + "'and type='55'";

                    list1 = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
                    foreach (PSPDEV ps1 in list1)
                    {
                        num++;
                        Ps_pdtypenode pn2 = new Ps_pdtypenode();

                        pn2.title = ps.Name;
                        pn2.pdreltypeid = pdreltype;
                        pn2.devicetype = "55";
                        pn2.DeviceID = ps1.SUID;
                        pn2.ParentID = pn1.ID;
                        pn2.Code = (Convert.ToInt32(level) + 1).ToString() + "6" + num.ToString();
                        Services.BaseService.Create<Ps_pdtypenode>(pn2);
                    }
                }
            }
            //查找负荷支路
            sql = "where AreaID='" + parentn.DeviceID + "'and type='80'  and suid not in " + xgxlsuid ;
            list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            foreach (PSPDEV ps in list)
            {

                sql = "pdreltypeid='" + pdreltype + "'and ParentID='" + parentn.ID + "'and DeviceID='" + ps.SUID + "'and devicetype='80'";
                Ps_pdtypenode ptn = UCDeviceBase.DataService.GetObject("SelectPs_pdtypenodeByCon", sql) as Ps_pdtypenode;
                if (ptn == null)
                {
                    count++;
                    Ps_pdtypenode pn1 = new Ps_pdtypenode();

                    pn1.title = "节点有问题_"+ps.Name;
                    pn1.pdreltypeid = pdreltype;
                    pn1.devicetype = "80";
                    pn1.DeviceID = ps.SUID;
                    pn1.ParentID = parentn.ID;
                    pn1.Code = level + "3" + count.ToString();
                    Services.BaseService.Create<Ps_pdtypenode>(pn1);
                }
            }
            //添加联络线
            sql = "where IName='" + parentn.DeviceID + "'or JName='" + parentn.DeviceID + "' and suid not in "+xgxlsuid+"and type='75'";
            list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            foreach (PSPDEV ps in list)
            {

                sql = "pdreltypeid='" + pdreltype + "'and ParentID='" + parentn.ID + "'and DeviceID='" + ps.SUID + "'and devicetype='75'";
                Ps_pdtypenode ptn = UCDeviceBase.DataService.GetObject("SelectPs_pdtypenodeByCon", sql) as Ps_pdtypenode;
                if (ptn == null)
                {
                    count++;
                    Ps_pdtypenode pn1 = new Ps_pdtypenode();

                    pn1.title ="节点有问题_"+ ps.Name;
                    pn1.pdreltypeid = pdreltype;
                    pn1.devicetype = "75";
                    pn1.DeviceID = ps.SUID;
                    pn1.ParentID = parentn.ID;
                    pn1.Code = level + "5" + count.ToString();
                    Services.BaseService.Create<Ps_pdtypenode>(pn1);
                }
            }


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