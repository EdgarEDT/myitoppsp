using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;

namespace Itop.TLPSP.DEVICE
{
    /// <summary>
    /// 母线属性编辑窗口
    /// </summary>
    public partial class frmMXdlg : Itop.Client.Base.FormBase
    {
        PSPDEV dev = new PSPDEV();
        public string oldName = "";
        public PSPDEV DeviceMx
        {
            get{
                dev.Name = textEdit1.Text;
                dev.Number = (int)spinEdit8.Value;
                dev.RateVolt = (double)spinEdit5.Value;
                dev.ReferenceVolt = (double)ReferenceVolt;
                dev.OperationYear = OperationYear;
                dev.Type = "01";
                dev.VoltR = (double)VoltR;
                dev.VoltV = (double)VoltV;
                dev.UnitFlag = UnitFlag.ToString();
                dev.KSwitchStatus = KswitchStatus.ToString();
                dev.NodeType = NodeType.ToString();
                dev.InPutP = (double)InPutP;
                dev.InPutQ = (double)InPutQ;
                dev.OutP = (double)OutP;
                dev.OutQ = (double)OutQ;
                dev.Burthen = Burthen;
                dev.LineLevel = checkBox1.Checked?"1":"0";
                dev.LineType = checkBox2.Checked ? "1" : "0";
                dev.Vimin = (double)spinEdit13.Value;
                dev.Vimax = (double)spinEdit14.Value;
                dev.Vjmin = (double)spinEdit12.Value;
                dev.Vjmax = (double)spinEdit9.Value;
                dev.Vk0 = (double)spinEdit15.Value;
                dev.jV = (double)spinEdit17.Value;
                dev.iV = (double)spinEdit16.Value;
                dev.Date1 = date1.Text;
                dev.Date2 = date2.Text;
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
                VoltV = (decimal)dev.VoltV;
                int f = 0;                
                int.TryParse(dev.UnitFlag,out f);
                UnitFlag = f;
                f = 0;
                int.TryParse(dev.KSwitchStatus,out f);
                KswitchStatus = f;
                f = 0;
                int.TryParse(dev.NodeType,out f);
                NodeType = f;
                InPutP = (decimal)dev.InPutP;
                InPutQ = (decimal)dev.InPutQ;
                OutP = (decimal)dev.OutP;
                OutQ = (decimal)dev.OutQ;
                Burthen = dev.Burthen;
                checkBox1.CheckState = dev.LineLevel=="1"?CheckState.Checked:CheckState.Unchecked ;
                checkBox2.CheckState = dev.LineType == "1" ? CheckState.Checked : CheckState.Unchecked;
                spinEdit13.Value= (decimal)dev.Vimin;
                spinEdit14.Value = (decimal)dev.Vimax;
                spinEdit12.Value = (decimal)dev.Vjmin;
                spinEdit9.Value = (decimal)dev.Vjmax;
                spinEdit15.Value = (decimal)dev.Vk0;
                spinEdit17.Value = (decimal)dev.jV;
                spinEdit16.Value = (decimal)dev.iV;
                date1.Text = dev.Date1;
                date2.Text = dev.Date2;
                setBdzName();
            }
        }
        public frmMXdlg() {
            InitializeComponent();
            Init();
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
            oldName = textEdit1.Text;
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
        #endregion  

        private void setBdzName() {
            //显示所在位置的名称
            object obj =DeviceHelper.GetDevice<PSP_Substation_Info>(dev.SvgUID);
            
            if (obj != null) {
                buttonEdit1.Text = ((PSP_Substation_Info)obj).Title;
                return;
            }
            obj = DeviceHelper.GetDevice<PSP_PowerSubstation_Info>(dev.SvgUID);
            if (obj != null) {
                buttonEdit1.Text = ((PSP_PowerSubstation_Info)obj).Title;
                return;
            }
            
        }
        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            frmDeviceSelect dlg = new frmDeviceSelect();
            
            
            dlg.InitDeviceType("20", "30");
            if (dlg.ShowDialog() == DialogResult.OK) {
                Dictionary<string,object> dic = dlg.GetSelectedDevice();
                buttonEdit1.Text = dic["name"].ToString();
                dev.SvgUID = dic["id"].ToString();
            }
        }
 
    }
}