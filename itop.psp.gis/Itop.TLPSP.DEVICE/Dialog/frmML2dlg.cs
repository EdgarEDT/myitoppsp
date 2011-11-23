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
    public partial class frmML2dlg : Itop.Client.Base.FormBase
    {
        PSPDEV dev = new PSPDEV();
        public PSPDEV DeviceMx
        {
            
            get
            {
                dev.Name = Name;
                dev.Number = (int)spinEdit8.Value;
                dev.OperationYear = OperationYear;
                dev.Type = "14";
                dev.LineLevel = SwitchStatus1.ToString();
                dev.LineType=SwitchStatus2.ToString();
                dev.LineStatus=SwitchStatus3.ToString();
                dev.HuganLine1=INodeName;
                dev.HuganLine2=JNodeName;
                dev.HuganLine3=ILineName;
                dev.HuganLine4=JLineName;
                dev.KName=ILoadName;
                dev.KSwitchStatus=JLoadName;
                dev.Date1 = date1.Text;
                dev.Date2 = date2.Text;
                return dev;
            }
            set
            {
                dev = value;
                Name = dev.Name;
                Number = dev.Number;
                OperationYear = dev.OperationYear;
                INodeName = dev.HuganLine1;
                JNodeName =dev.HuganLine2;
                ILineName = dev.HuganLine3;
                JLineName = dev.HuganLine4;
                ILoadName = dev.KName;
                JLoadName = dev.KSwitchStatus;

                date1.Text = dev.Date1;
                date2.Text = dev.Date2;
                int f = 0;
                int.TryParse(dev.LineLevel, out f);
                SwitchStatus1 = f;
                f = 0;
                int.TryParse(dev.LineType, out f);
                SwitchStatus2 = f;
                f = 0;
                int.TryParse(dev.LineStatus, out f);
                SwitchStatus3 = f;
            }
        }
        public frmML2dlg()
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
                if (comboBoxEdit1.Properties.Items.IndexOf(pspdev.Name) == -1)
                {
                    comboBoxEdit1.Properties.Items.Add(pspdev.Name);

                }

            }
            con = null;
            con = " where Type ='05'and ProjectID ='" + this.ProjectID + "' order by name";
            IList list1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            foreach (PSPDEV pspdev in list1)
            {
                if (comboBoxEdit2.Properties.Items.IndexOf(pspdev.Name) == -1)
                {
                    comboBoxEdit2.Properties.Items.Add(pspdev.Name);

                }
                if (comboBoxEdit5.Properties.Items.IndexOf(pspdev.Name) == -1)
                {
                    comboBoxEdit5.Properties.Items.Add(pspdev.Name);

                }

            }
            con = null;
            con = " where Type ='12'and ProjectID ='" + this.ProjectID + "' order by name";
            IList list2 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            foreach (PSPDEV pspdev in list2)
            {
                if (comboBoxEdit6.Properties.Items.IndexOf(pspdev.Name) == -1)
                {
                    comboBoxEdit6.Properties.Items.Add(pspdev.Name);

                }
                if (comboBoxEdit7.Properties.Items.IndexOf(pspdev.Name) == -1)
                {
                    comboBoxEdit7.Properties.Items.Add(pspdev.Name);

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
         string projectid;
        public string ProjectID
        {
            get { return projectid; }
            set { projectid = value; }
        }
        public string INodeName
        {
            get
            {
             return comboBoxEdit4.Text;
            }
            set
            {
                comboBoxEdit4.Text=value;
            }
        }
        public string JNodeName
        {
            get
            {
               return comboBoxEdit1.Text;
            }
            set
            {
                comboBoxEdit1.Text=value;
            }
        }
        public string ILineName
        {
            get
            {
                return comboBoxEdit2.Text;
            }
            set
            {
                comboBoxEdit2.Text=value;
            }
        }
        public string JLineName
        {
            get
            {
                return comboBoxEdit5.Text;
            }
            set
            {
                comboBoxEdit5.Text=value;
            }
        }
        public string ILoadName
        {
            get
            {
                return comboBoxEdit6.Text;
            }
            set
            {
                comboBoxEdit6.Text=value;
            }
        }
        public string JLoadName
        {
           get
            {
               return comboBoxEdit7.Text;
            }
            set
            {
                comboBoxEdit7.Text=value;
            }
        }
        public int SwitchStatus1
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
        public int SwitchStatus2
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
        public int SwitchStatus3
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

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {
            if (radioGroup2.SelectedIndex == 0)
            {
                e.Graphics.Clear(Color.Red);
            }
            else
            {
                e.Graphics.Clear(Color.Green);
            }            
        }

        private void panelControl3_Paint(object sender, PaintEventArgs e)
        {
            if (radioGroup3.SelectedIndex == 0)
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

        private void radioGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.panelControl2.Refresh();
        }

        private void radioGroup3_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.panelControl3.Refresh();
        }
        
    }
}