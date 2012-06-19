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
    public partial class frmFDJdlg : Itop.Client.Base.FormBase
    {
        PSPDEV dev = new PSPDEV();
        public PSPDEV DeviceMx
        {
            get{
                dev.Name = textEdit1.Text;
                dev.Number = (int)spinEdit8.Value;
                dev.OperationYear = OperationYear;
                dev.Type = "04";
                dev.NodeType = (string)radioGroup2.EditValue;
                dev.UnitFlag = (string)radioGroup3.EditValue;
                dev.KSwitchStatus = (string)radioGroup1.EditValue;
                dev.IName = comboBoxEdit4.Text;
                dev.RateVolt = (double)spinEdit5.Value;
                dev.ReferenceVolt = (double)spinEdit12.Value;
                dev.P0 = (double)spinEdit9.Value;
                dev.I0 = (double)spinEdit11.Value;
                dev.SiN = (double)spinEdit6.Value;
                dev.SjN = (double)spinEdit13.Value;
                dev.SkN = (double)spinEdit15.Value;
                dev.iV = (double)spinEdit7.Value;
                dev.jV = (double)spinEdit14.Value;
                dev.kV = (double)spinEdit16.Value;
                dev.PositiveR = (double)spinEdit17.Value;
                dev.ZeroTQ = (double)spinEdit19.Value;
                dev.PositiveTQ = (double)spinEdit18.Value;
                dev.OutP = (double)spinEdit10.Value;
                dev.OutQ = (double)spinEdit1.Value;
                dev.Vimax = (double)spinEdit2.Value;
                dev.Vimin = (double)spinEdit20.Value;
                dev.Vjmax = (double)spinEdit3.Value;
                dev.Vjmin = (double)spinEdit4.Value;
                dev.Vkb = (double)spinEdit21.Value;
                dev.ProjectID = ProjectSUID;
                dev.LineType = textEdit2.Text;
                dev.Date1 = date1.Text;
                dev.Date2 = date2.Text;
                return dev;
            }
            set{
                dev = value;
                Name = dev.Name;
                Number = dev.Number;
                OperationYear = dev.OperationYear;

                radioGroup2.EditValue = dev.NodeType;
                radioGroup3.EditValue = dev.UnitFlag;
                radioGroup1.EditValue = dev.KSwitchStatus;
                comboBoxEdit4.Text = dev.IName;
                spinEdit5.Value = (decimal)dev.RateVolt;
                spinEdit12.Value = (decimal)dev.ReferenceVolt;
                spinEdit9.Value = (decimal)dev.P0;
                spinEdit11.Value = (decimal)dev.I0;
                spinEdit6.Value = (decimal)dev.SiN;
                spinEdit13.Value = (decimal)dev.SjN;
                spinEdit15.Value = (decimal)dev.SkN;
                spinEdit7.Value = (decimal)dev.iV;
                spinEdit14.Value = (decimal)dev.jV;
                spinEdit16.Value = (decimal)dev.kV;
                spinEdit17.Value = (decimal)dev.PositiveR;
                spinEdit19.Value = (decimal)dev.ZeroTQ;
                spinEdit18.Value = (decimal)dev.PositiveTQ;
                spinEdit10.Value = (decimal)dev.OutP;
                spinEdit1.Value = (decimal)dev.OutQ;
                spinEdit2.Value = (decimal)dev.Vimax;
                spinEdit20.Value = (decimal)dev.Vimin;
                spinEdit3.Value = (decimal)dev.Vjmax;
                spinEdit4.Value = (decimal)dev.Vjmin;
                spinEdit21.Value = (decimal)dev.Vkb;
                textEdit2.Text = dev.LineType;
                date1.Text = dev.Date1;
                date2.Text = dev.Date2;
                ProjectSUID = dev.ProjectID;
            }
        }
        public frmFDJdlg() {
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
            string con = "where Type='01'AND  ProjectID ='" + this.ProjectSUID + "' and SvgUID='" + dev.SvgUID + "' order by name";

            IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            if (list.Count == 0)
            {
                con = " where Type='01'AND  ProjectID ='" + this.ProjectSUID + "' order by name";
                list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            }
            foreach (PSPDEV dev1 in list)
            {
                if (comboBoxEdit4.Properties.Items.IndexOf(dev1.Name) == -1)
                {          
                    comboBoxEdit4.Properties.Items.Add(dev1.Name);
                }
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
                return spinEdit12.Value;
            }
            set
            {
                spinEdit12.Value = value;
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
        public decimal VoltV
        {
            get
            {
                return spinEdit7.Value;
            }
            set
            {
                spinEdit7.Value = value;
            }
        }
        /// <summary>
        /// 短路容量
        /// </summary>
        public decimal Burthen
        {
            get
            {
                return spinEdit10.Value;
            }
            set
            {
                spinEdit10.Value = value;
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
        /// <summary>
        /// 母线类型
        /// </summary>
        public int NodeType
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
        private string projectID;
        public string ProjectSUID
        {
            get
            {
                return projectID;
            }
            set
            {
                projectID = value;
            }
        }
        #endregion  

        private void comboBoxEdit4_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSPDEV devMX = new PSPDEV();
            string strCon = strCon = " WHERE Name = '" + comboBoxEdit4.Text + "' AND ProjectID = '" + this.ProjectSUID + "' AND Type = '01'";
            devMX = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", strCon);
            if (devMX != null)
            {
                dev.FirstNode = devMX.Number;
                RateVolt = (decimal)devMX.RateVolt;
                dev.VoltR = devMX.VoltR;
                dev.VoltV = devMX.VoltV;
                ReferenceVolt =(decimal) devMX.ReferenceVolt;
            }
        }
    }
}