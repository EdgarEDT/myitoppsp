using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using System.Collections;
using Itop.Client.Projects;

namespace Itop.TLPSP.DEVICE
{
    /// <summary>
    /// 母线属性编辑窗口
    /// </summary>
    public partial class frmBLDRdlg : Itop.Client.Base.FormBase
    {
        PSPDEV dev = new PSPDEV();
        public PSPDEV DeviceMx
        {
            get{
                dev.Name = textEdit1.Text;
                dev.Number = (int)spinEdit8.Value;
                dev.RateVolt = (double)spinEdit5.Value;
                dev.ReferenceVolt = (double)ReferenceVolt;
                dev.OperationYear = OperationYear;
                dev.Type = "09";        
                dev.UnitFlag = UnitFlag.ToString();
                dev.KSwitchStatus = KswitchStatus.ToString();
                dev.IName = comboBoxEdit4.Text;
                //dev.JName = comboBoxEdit7.Text;
                dev.Burthen = spinEdit1.Value;
                dev.P0 = (double)spinEdit2.Value;
                dev.LineTQ = (double)spinEdit3.Value;
                //dev.StandardCurrent = (double)spinEdit7.Value;
                dev.LineType = comboBoxEdit2.Text;
                dev.Date1 = date1.Text;
                dev.Date2 = date2.Text;
                return dev;
            }
            set{
                dev = value;
                comboBoxEdit2.Text = dev.LineType;
                Name = dev.Name;
                Number = dev.Number;
                RateVolt = (decimal)dev.RateVolt;
                ReferenceVolt = (decimal)dev.ReferenceVolt;
                OperationYear = dev.OperationYear;              
  
                int f = 0;                
                int.TryParse(dev.UnitFlag,out f);
                UnitFlag = f;
                f = 0;
                int.TryParse(dev.KSwitchStatus,out f);
                KswitchStatus = f;

                spinEdit1.Value = dev.Burthen;
                spinEdit3.Value = (decimal)dev.LineTQ;
                spinEdit2.Value = (decimal)dev.P0;
                comboBoxEdit4.Text = dev.IName;
                date1.Text = dev.Date1;
                date2.Text = dev.Date2;
                //comboBoxEdit7.Text = dev.JName;
                //spinEdit7.Value = (decimal)dev.StandardCurrent;
            }
        }
        public frmBLDRdlg() {
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
                if (comboBoxEdit4.Properties.Items.IndexOf(dev.Name) == -1)
                {
                    comboBoxEdit4.Properties.Items.Add(dev.Name);
                    comboBoxEdit7.Properties.Items.Add(dev.Name);
                }
            }
            WireCategory wirewire = new WireCategory();
            wirewire.Type = "65";
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
                if (comboBoxEdit2.Properties.Items.IndexOf(sub.WireType) == -1)
                {
                    comboBoxEdit2.Properties.Items.Add(sub.WireType);
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
        ///// <summary>
        ///// 电压幅值
        ///// </summary>
        //public decimal VoltR
        //{
        //    get
        //    {
        //        return spinEdit6.Value;
        //    }
        //    set
        //    {
        //        spinEdit6.Value = value;
        //    }
        //}
        ///// <summary>
        ///// 电压相角
        ///// </summary>
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
        ///// <summary>
        ///// 短路容量
        ///// </summary>
        //public decimal Burthen
        //{
        //    get
        //    {
        //        return spinEdit10.Value;
        //    }
        //    set
        //    {
        //        spinEdit10.Value = value;
        //    }
        //}
        /// <summary>
        /// 发电有功
        /// </summary>
        //public decimal OutP
        //{
        //    get
        //    {
        //        return spinEdit1.Value;
        //    }
        //    set
        //    {
        //        spinEdit1.Value = value;
        //    }
        //}
        /// <summary>
        /// 发电无功
        /// </summary>
        //public decimal OutQ
        //{
        //    get
        //    {
        //        return spinEdit2.Value;
        //    }
        //    set
        //    {
        //        spinEdit2.Value = value;
        //    }
        //}
        /// <summary>
        /// 负荷有功
        /// </summary>
        //public decimal InPutP
        //{
        //    get
        //    {
        //        return spinEdit3.Value;
        //    }
        //    set
        //    {
        //        spinEdit3.Value = value;
        //    }
        //}
        /// <summary>
        /// 负荷无功
        /// </summary>
        //public decimal InPutQ
        //{
        //    get
        //    {
        //        return spinEdit4.Value;
        //    }
        //    set
        //    {
        //        spinEdit4.Value = value;
        //    }
        //}
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

        private void comboBoxEdit4_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSPDEV devMX = new PSPDEV();
            string strCon = strCon = " WHERE Name = '" + comboBoxEdit4.Text + "' AND ProjectID = '" + this.ProjectSUID + "' AND Type = '01'";
            devMX = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", strCon);
            if (devMX != null)
            {
                dev.FirstNode = devMX.Number;
                ReferenceVolt =(decimal) devMX.ReferenceVolt;
                RateVolt =(decimal) devMX.RateVolt;
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

            spinEdit3.Value = (decimal)(1 / (314 * spinEdit2.Value) * (100 / (spinEdit11.Value * spinEdit11.Value)));
        }

        private void spinEdit11_EditValueChanged(object sender, EventArgs e)
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
    }
}