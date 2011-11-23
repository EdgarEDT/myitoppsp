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
    public partial class frmMLdlg : Itop.Client.Base.FormBase
    {

        PSPDEV dev = new PSPDEV();
        public PSPDEV DeviceMx
        {
            get
            {
                dev.Name = Name;
                dev.Number = (int)spinEdit8.Value;
                dev.OperationYear = OperationYear;
                dev.Type = "13";
                dev.KSwitchStatus = KswitchStatus.ToString();                
                dev.IName = Iname;               
                dev.JName = Jname;
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
               Iname = dev.IName;
                Jname = dev.JName;
                date1.Text = dev.Date1;
                date2.Text = dev.Date2;
                int f = 0;
                int.TryParse(dev.KSwitchStatus, out f);
                KswitchStatus = f;
            }
        }
        public frmMLdlg()
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
            //Dlqiname.Text=
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
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
        public string Iname
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
        public string Jname
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
        string projectid;
        public string ProjectID
        {
            get { return projectid; }
            set { projectid = value; }
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

    }
}