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
    /// <summary>
    /// 母线属性编辑窗口
    /// </summary>
    public partial class frmBYQ2templatedlg : Itop.Client.Base.FormBase
    {
        Template_PSPDEV dev = new Template_PSPDEV();
        public Template_PSPDEV DeviceMx
        {
            get{
                dev.TemplateType = textEdit1.Text;
                dev.Number = (int)spinEdit8.Value;
                dev.OperationYear = OperationYear;
                dev.Type = "02";     
                dev.IName = comboBoxEdit3.Text;
                dev.JName = comboBoxEdit4.Text;
                dev.ISwitch = comboBoxEdit5.Text;
                dev.JSwitch = comboBoxEdit6.Text;

                dev.UnitFlag = UnitFlag.ToString();
                dev.KSwitchStatus = KswitchStatus.ToString();

                dev.HuganLine3 = (string)radioGroup2.EditValue;
                dev.HuganLine4 = (string)radioGroup4.EditValue;

                dev.LineLevel = (string)radioGroup6.EditValue;
                dev.LineType = (string)radioGroup5.EditValue;
                dev.Burthen =spinEdit1.Value;
                dev.Vi0 = (double)spinEdit5.Value;
                dev.Vj0 = (double)spinEdit2.Value;
                dev.Vib = (double)spinEdit47.Value;
                dev.Vjb = (double)spinEdit46.Value;
                dev.K = (double)spinEdit3.Value;

                dev.Vipos = (double)spinEdit7.Value;
                dev.Vistep = (double)spinEdit6.Value;
                dev.Vimax = (double)spinEdit4.Value;
                dev.Vimin = (double)spinEdit9.Value;

                dev.Vjpos = (double)spinEdit13.Value;
                dev.Vjstep = (double)spinEdit12.Value;
                dev.Vjmax = (double)spinEdit11.Value;
                dev.Vimin = (double)spinEdit10.Value;

                dev.Pij = (double)spinEdit22.Value;
                dev.P0 = (double)spinEdit21.Value;
                dev.Vij = (double)spinEdit20.Value;
                dev.I0 = (double)spinEdit19.Value;

                dev.LineR = (double)spinEdit18.Value;
                dev.LineTQ = (double)spinEdit16.Value;
                dev.G = (double)spinEdit15.Value;
                dev.LineGNDC = (double)spinEdit14.Value;
                dev.iV = (double)spinEdit48.Value;
                dev.jV = (double)spinEdit45.Value;
                dev.BigTQ = (double)spinEdit44.Value;
                dev.SmallTQ = (double)spinEdit17.Value;
                dev.Date1 = date1.Text;
                dev.Date2 = date2.Text;
                return dev;
            }
            set{
                dev = value;
                Name = dev.TemplateType;
                Number = dev.Number;       
               
                OperationYear = dev.OperationYear;   
        
                int f = 0;                
                int.TryParse(dev.UnitFlag,out f);
                UnitFlag = f;
                f = 0;
                int.TryParse(dev.KSwitchStatus,out f);
                KswitchStatus = f;

                comboBoxEdit3.Text = dev.IName;
                comboBoxEdit4.Text = dev.JName;
                comboBoxEdit5.Text = dev.ISwitch;
                comboBoxEdit6.Text = dev.JSwitch;

                radioGroup2.EditValue = dev.HuganLine3;
                radioGroup4.EditValue = dev.HuganLine4;

                radioGroup6.EditValue = dev.LineLevel;
                radioGroup5.EditValue = dev.LineType;

                spinEdit1.Value =dev.Burthen;
                spinEdit5.Value = (decimal)dev.Vi0;
                spinEdit2.Value = (decimal)dev.Vj0;
                spinEdit47.Value = (decimal)dev.Vib;
                spinEdit46.Value = (decimal)dev.Vjb;
                spinEdit3.Value = (decimal)dev.K;

                spinEdit7.Value = (decimal)dev.Vipos;
                spinEdit6.Value = (decimal)dev.Vistep;
                spinEdit4.Value = (decimal)dev.Vimax;
                spinEdit9.Value = (decimal)dev.Vimin;

                spinEdit13.Value = (decimal)dev.Vjpos;
                spinEdit12.Value = (decimal)dev.Vjstep;
                spinEdit11.Value = (decimal)dev.Vjmax;
                spinEdit10.Value = (decimal)dev.Vimin;

                spinEdit22.Value = (decimal)dev.Pij;
                spinEdit21.Value = (decimal)dev.P0;
                spinEdit20.Value = (decimal)dev.Vij;
                spinEdit19.Value = (decimal)dev.I0;

                spinEdit18.Value = (decimal)dev.LineR;
                spinEdit16.Value = (decimal)dev.LineTQ;
                spinEdit15.Value = (decimal)dev.G;
                spinEdit14.Value = (decimal)dev.LineGNDC;
                spinEdit48.Value = (decimal)dev.iV;
                spinEdit45.Value = (decimal)dev.jV;
                spinEdit44.Value=(decimal)dev.BigTQ;
                spinEdit17.Value=(decimal)dev.SmallTQ;
                date1.Text = dev.Date1;
                date2.Text = dev.Date2;
 
            }
        }
        public frmBYQ2templatedlg()
        {
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
            string con = " where Type='01'AND  ProjectID ='" + this.ProjectSUID + "' order by name";
            IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            foreach (PSPDEV dev in list)
            {
                if (comboBoxEdit3.Properties.Items.IndexOf(dev.Name)==-1)
                {
                    comboBoxEdit3.Properties.Items.Add(dev.Name);
                    comboBoxEdit4.Properties.Items.Add(dev.Name);
                }
            }
            con = " where Type='07'AND  ProjectID ='" + this.ProjectSUID + "' order by name";
            list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            foreach (PSPDEV dev in list)
            {
                if (comboBoxEdit5.Properties.Items.IndexOf(dev.Name) == -1)
                {
                    comboBoxEdit5.Properties.Items.Add(dev.Name);
                    comboBoxEdit6.Properties.Items.Add(dev.Name);
                }
            }
            //没有的话 给赋一个初值
            if (list.Count > 0)
            {
                comboBoxEdit5.Text = (list[0] as PSPDEV).Name;
                comboBoxEdit6.Text = (list[0] as PSPDEV).Name;
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
                return projectID;
            }
            set{
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
        //public decimal ReferenceVolt
        //{
        //    get
        //    {
        //        return spinEdit11.Value;
        //    }
        //    set
        //    {
        //        spinEdit11.Value = value;
        //    }
        //}
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
        //public decimal Burthen
        //{
        //    //get
        //    //{
        //    //    return spinEdit10.Value;
        //    //}
        //    //set
        //    //{
        //    //    spinEdit10.Value = value;
        //    //}
        //}
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

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (radioGroup3.SelectedIndex==0)
            {   
            
            } 
            else
            {
                try
                {
                    if (DeviceMx.LineR.ToString() == "" || DeviceMx.LineR.ToString() == null)
                    {
                        DeviceMx.LineR = Convert.ToDouble(DeviceMx.Pij) * Convert.ToDouble(DeviceMx.Vi0) * Convert.ToDouble(DeviceMx.Vi0) * 100 / (1000 * Convert.ToDouble(DeviceMx.Burthen) * Convert.ToDouble(DeviceMx.Burthen) * Convert.ToDouble(DeviceMx.Vib) * Convert.ToDouble(DeviceMx.Vib));
                    }
                    if (DeviceMx.LineTQ.ToString() == "" || DeviceMx.LineTQ.ToString() == null)
                    {
                        DeviceMx.LineTQ = Convert.ToDouble(DeviceMx.Vij) * Convert.ToDouble(DeviceMx.Vi0) * Convert.ToDouble(DeviceMx.Vi0) * 100 / (100 * 100 * Convert.ToDouble(DeviceMx.Burthen) * Convert.ToDouble(DeviceMx.Vib) * Convert.ToDouble(DeviceMx.Vib));
                    }
                    if (DeviceMx.G.ToString() == "" || DeviceMx.G.ToString() == null)
                    {
                        DeviceMx.G = Convert.ToDouble(DeviceMx.P0) * Convert.ToDouble(DeviceMx.Vib) * Convert.ToDouble(DeviceMx.Vib) * Convert.ToDouble(DeviceMx.Burthen) / (1000 * Convert.ToDouble(DeviceMx.Burthen) * 100 * Convert.ToDouble(DeviceMx.Vi0) * Convert.ToDouble(DeviceMx.Vi0));
                    }
                    if (DeviceMx.LineGNDC.ToString() == "" || DeviceMx.LineGNDC.ToString() == null)
                    {
                        DeviceMx.LineGNDC = Convert.ToDouble(DeviceMx.I0) * Convert.ToDouble(DeviceMx.Vib) * Convert.ToDouble(DeviceMx.Vib) * Convert.ToDouble(DeviceMx.Burthen) / (100 * 100 * 100 * Convert.ToDouble(DeviceMx.Vi0) * Convert.ToDouble(DeviceMx.Vi0));
                    }

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("请填写相应抽头信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }          
            }
        }
           

        private void xtraTabPage2_Paint(object sender, PaintEventArgs e)
        {

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
        private void comboBoxEdit3_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSPDEV devMX = new PSPDEV();
            string strCon = strCon = " WHERE Name = '" + comboBoxEdit3.Text + "' AND ProjectID = '" + this.ProjectSUID + "' AND Type = '01'";
            devMX = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition",strCon);
            if (devMX!=null)
            {
                spinEdit5.Value = (decimal)devMX.RateVolt;
                spinEdit47.Value = (decimal)devMX.ReferenceVolt;
                dev.FirstNode = devMX.Number;
            }
        }

        private void comboBoxEdit4_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSPDEV devMX = new PSPDEV();
            string strCon = strCon = " WHERE Name = '" + comboBoxEdit4.Text + "' AND ProjectID = '" + this.ProjectSUID + "' AND Type = '01'";
            devMX = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", strCon);
            if (devMX != null)
            {
                spinEdit2.Value = (decimal)devMX.RateVolt;
                spinEdit46.Value = (decimal)devMX.ReferenceVolt;
                dev.LastNode = devMX.Number;
            }
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            if (spinEdit1.Value==0)
            {
                spinEdit1.Value = 100;
            }
            try
            {
                if (radioGroup3.SelectedIndex == 0)
                {
                    spinEdit18.Value = ((spinEdit22.Value * spinEdit5.Value * spinEdit5.Value) / (1000 * spinEdit1.Value * spinEdit1.Value)) * (100 / (spinEdit47.Value * spinEdit47.Value));
                    spinEdit16.Value = ((spinEdit20.Value * spinEdit5.Value * spinEdit5.Value) / (100 * spinEdit1.Value)) * (100 / (spinEdit47.Value * spinEdit47.Value));
                    spinEdit15.Value = (spinEdit21.Value / (1000 * spinEdit1.Value)) * (spinEdit1.Value / (spinEdit5.Value * spinEdit5.Value)) * ((spinEdit47.Value * spinEdit47.Value) / 100);
                    spinEdit14.Value = (spinEdit19.Value / 100) * (spinEdit1.Value / (spinEdit5.Value * spinEdit5.Value)) * ((spinEdit47.Value * spinEdit47.Value) / 100);

                }
                else
                {
                    spinEdit18.Value = ((spinEdit22.Value * spinEdit5.Value * spinEdit5.Value) / (1000 * spinEdit1.Value * spinEdit1.Value));
                    spinEdit16.Value = ((spinEdit20.Value * spinEdit5.Value * spinEdit5.Value) / (100 * spinEdit1.Value));
                    spinEdit15.Value = (spinEdit21.Value / (1000 * spinEdit1.Value)) * (spinEdit1.Value / (spinEdit5.Value * spinEdit5.Value));
                    spinEdit14.Value = (spinEdit19.Value / 100) * (spinEdit1.Value / (spinEdit5.Value * spinEdit5.Value));
                }  
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("变压器额定容量、一次侧额定电压、一次侧额定电流的值不能为零！");
                spinEdit22.Value = 0;
                spinEdit20.Value = 0;
                spinEdit21.Value = 0;
                spinEdit19.Value = 0;
            }
          
           
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void spinEdit47_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}