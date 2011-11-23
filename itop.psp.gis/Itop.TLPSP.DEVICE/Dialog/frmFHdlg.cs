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
namespace Itop.TLPSP.DEVICE
{
    public partial class frmFHdlg :Itop.Client.Base.FormBase
    {
        PSPDEV dev = new PSPDEV();
        public PSPDEV DeviceMx
        {
            get
            {
                dev.Name = Name;
                dev.Number = (int)spinEdit8.Value;

                if (!string.IsNullOrEmpty(InputP))
                {
                    dev.InPutP = Convert.ToDouble(InputP);
                }

                dev.UnitFlag = (string)radioGroup3.EditValue;
                dev.OperationYear = OperationYear;
                dev.Type = "12";
                dev.KSwitchStatus = KswitchStatus.ToString();
                if (!string.IsNullOrEmpty(InputQ))
                {
                    dev.InPutQ = Convert.ToDouble(InputQ);
                }
                if (!string.IsNullOrEmpty(VoltR))
                {
                    dev.VoltR = Convert.ToDouble(VoltR);
                }
                //if (!string.IsNullOrEmpty(spinEdit1.Text))
                //{
                //    dev.VoltV = Convert.ToDouble(spinEdit1.Text);
                //}
                if (!string.IsNullOrEmpty(spinEdit2.Text))
                {
                    dev.Vipos = Convert.ToDouble(spinEdit2.Text);
                }
                if (!string.IsNullOrEmpty(spinEdit10.Text))
                {
                    dev.HuganTQ2 = Convert.ToDouble(spinEdit10.Text);
                }
                if (!string.IsNullOrEmpty(spinEdit12.Text))
                {
                    dev.HuganTQ5 = Convert.ToDouble(spinEdit12.Text);
                }
                if (!string.IsNullOrEmpty(spinEdit16.Text))
                {
                    dev.HuganTQ4 = Convert.ToDouble(spinEdit16.Text);
                }
                if (!string.IsNullOrEmpty(spinEdit14.Text))
                {
                    dev.P0 = Convert.ToDouble(spinEdit14.Text);
                }
                if (!string.IsNullOrEmpty(spinEdit13.Text))
                {
                    dev.I0 = Convert.ToDouble(spinEdit13.Text);
                }
                if (!string.IsNullOrEmpty(spinEdit11.Text))
                {
                    dev.SiN = Convert.ToDouble(spinEdit11.Text);
                }
                if (!string.IsNullOrEmpty(spinEdit17.Text))
                {
                    dev.SjN = Convert.ToDouble(spinEdit17.Text);
                }
                if (!string.IsNullOrEmpty(spinEdit15.Text))
                {
                    dev.SkN = Convert.ToDouble(spinEdit15.Text);
                }
                dev.NodeType = (string)radioGroup2.EditValue;
                dev.IName = belongbus;
                dev.ProjectID = projectid;
                return dev;
            }
            set
            {
                dev = value;
                Name = dev.Name;
                Number = dev.Number;
                OperationYear = dev.OperationYear;
                radioGroup3.EditValue = dev.UnitFlag;
                InputP = dev.InPutP.ToString();
                InputQ = dev.InPutQ.ToString();
                VoltR = dev.VoltR.ToString();
                belongbus = dev.IName;
                projectid = dev.ProjectID;


                //spinEdit1.Text = dev.VoltV.ToString();
                spinEdit2.Text = dev.Vipos.ToString();      
                spinEdit10.Text = dev.HuganTQ2.ToString();
                spinEdit12.Text = dev.HuganTQ5.ToString();    
                spinEdit16.Text = dev.HuganTQ4.ToString();
                spinEdit14.Text = dev.P0.ToString();     
                spinEdit13.Text = dev.I0.ToString();       
                spinEdit11.Text = dev.SiN.ToString();      
                spinEdit17.Text = dev.SjN.ToString();  
                spinEdit15.Text = dev.SkN.ToString();

                int f = 0;
                int.TryParse(dev.KSwitchStatus, out f);
                KswitchStatus = f;
                f = 0;
                int.TryParse(dev.NodeType, out f);
                radioGroup2.SelectedIndex = f;
            }
        }
        public frmFHdlg()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            intdata();
        }
        public void intdata()
        {
            //subsedit.Text = dev.HuganLine1;   //读取母线节点所属的变电站
            object o = new object();
            for (int i = -30; i <= 30; i++)
            {
                o = System.DateTime.Now.Year + i;
                comboBoxEdit3.Properties.Items.Add(o);
            }
            string con = " where Type ='01'and ProjectID ='" + this.ProjectID + "' order by name";
            IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            foreach (PSPDEV pspdev in list)
            {
                if (comboBoxEdit4.Properties.Items.IndexOf(pspdev.Name) == -1)
                {
                    comboBoxEdit4.Properties.Items.Add(pspdev.Name);

                }
            }
            //Dlqiname.Text=
        }
        public string Name
        {
            get
            {
                return Dlqiname.Text;
            }
            set
            {
                Dlqiname.Text = value;
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
        public string OperationYear
        {
            get
            {
                return comboBoxEdit3.Text;
            }
            set
            {
                comboBoxEdit3.Text = value;
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
        public string InputP
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
        public string InputQ
        {
            get
            {
                return textEdit2.Text;
            }
            set
            {
                textEdit2.Text = value;
            }
        }
        public string VoltR
        {
            get
            {
                return textEdit3.Text;
            }
            set
            {
                textEdit3.Text = value;
            }
        }
        public string belongbus
        {
            get
            {
                return comboBoxEdit4.Text;
            }
            set
            {
                comboBoxEdit4.Text = value;
            }
        }
        string projectid;
        public string ProjectID
        {
            get { return projectid; }
            set { projectid = value; }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

 

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0)
            {
                e.Graphics.Clear(Color.Red);
            }
            else
            {
                e.Graphics.Clear(Color.Green);
            }            
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.panelControl1.Refresh();
        }

        private void comboBoxEdit4_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSPDEV devMX = new PSPDEV();
            string strCon = strCon = " WHERE Name = '" + comboBoxEdit4.Text + "' AND ProjectID = '" + this.ProjectID + "' AND Type = '01'";
            devMX = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", strCon);
            if (devMX != null)
            {
                dev.FirstNode = devMX.Number;
                dev.RateVolt = devMX.RateVolt;
                VoltR = devMX.RateVolt.ToString();
                dev.VoltR = devMX.VoltR;
                dev.VoltV = devMX.VoltV;
                dev.ReferenceVolt = devMX.ReferenceVolt;
            }
        }

        private void panelControl1_Paint_1(object sender, PaintEventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0)
            {
                e.Graphics.Clear(Color.Red);
            }
            else
            {
                e.Graphics.Clear(Color.Green);
            }            
        }

        private void radioGroup1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            this.panelControl1.Refresh();
        }

    }
}