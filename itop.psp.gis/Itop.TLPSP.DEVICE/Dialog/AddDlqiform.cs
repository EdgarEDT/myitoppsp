using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Domain.Table;
using Itop.Client.Common;
using System.Collections;
namespace Itop.TLPSP.DEVICE
{
    public partial class AddDlqiform : Itop.Client.Base.FormBase
    {
        PSPDEV dev = new PSPDEV();
        public PSPDEV DeviceMx
        {
            get
            {
                dev.Name = Name;
                dev.Number = (int)spinEdit8.Value;
                dev.HuganLine1 = SubstationName;
                if (!string.IsNullOrEmpty(outI))
                {
                    dev.HuganTQ1 = Convert.ToDouble(outI);
                }
                
                dev.HuganLine2 = DlqiType;
                dev.OperationYear = OperationYear;
                dev.Type = "06";
                dev.KSwitchStatus = KswitchStatus.ToString();
                dev.ISwitch = Iswitch.ToString();
                dev.DQ = comboBoxEdit8.Text;
                 dev.HuganTQ4= (double)spinEdit1.Value;
                dev.HuganTQ5= (double)spinEdit2.Value ;
                if (comboBoxEdit9.Properties.GetKeyValueByDisplayText(comboBoxEdit9.Text) != null)
                {
                    dev.AreaID = comboBoxEdit9.Properties.GetKeyValueByDisplayText(comboBoxEdit9.Text).ToString();
                }
                if (!string.IsNullOrEmpty(MinSwitchtime))
                {
                    dev.HuganTQ2 = Convert.ToDouble(MinSwitchtime);
                }
                if (!string.IsNullOrEmpty(DlqiZl))
                {
                    dev.HuganTQ3 = Convert.ToDouble(DlqiZl);
                }              
                dev.IName = belongbus;

                return dev;
            }
            set
            {
                dev = value;
                Name = dev.Name;
                Number = dev.Number;
                SubstationName = dev.HuganLine1;
                outI = dev.HuganTQ1.ToString();
                DlqiZl = dev.HuganTQ3.ToString();
                DlqiType = dev.HuganLine2;
                OperationYear = dev.OperationYear;
                MinSwitchtime = dev.HuganTQ1.ToString();
                if (string.IsNullOrEmpty(dev.IName)&&!string.IsNullOrEmpty(parentid))
                {
                    dev.IName = parentid;
                }
                belongbus = dev.IName;
                spinEdit1.Value =(decimal)dev.HuganTQ4;
                spinEdit2.Value = (decimal)dev.HuganTQ5;
                int f = 0;
                int.TryParse(dev.KSwitchStatus, out f);
                KswitchStatus = f;
                f = 1;
                int.TryParse(dev.ISwitch, out f);
                Iswitch = f;
                comboBoxEdit8.Text = dev.DQ;
                comboBoxEdit9.EditValue = dev.AreaID;  
                setBdzName();
            }
        }
        public AddDlqiform()
        {
            InitializeComponent();
            //intdata();
        }
        public  AddDlqiform(PSPDEV dev)
        {
            InitializeComponent();
            intdata(dev);
        }
        public AddDlqiform(PSPDEV dev,int i)
        {
            InitializeComponent();
            com(dev);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            intdata();
        }
        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
        public void com(PSPDEV dev)
        {
            subsedit.Text = dev.HuganLine1;
            Dlqiname.Text = dev.Name;
            spinEdit8.Text = dev.Number.ToString();
            textEdit1.Text = dev.HuganTQ1.ToString();
            comboBoxEdit1.Text = dev.HuganLine2;
            textEdit3.Text = dev.HuganTQ2.ToString();
            textEdit2.Text = dev.HuganTQ3.ToString();
            

        }
        public void intdata(PSPDEV dev)
        {
            subsedit.Text = dev.HuganLine1;   //读取母线节点所属的变电站
            object o = new object();
            for (int i = -30; i <= 30; i++)
            {
                o = System.DateTime.Now.Year + i;
                comboBoxEdit3.Properties.Items.Add(o);
            }
            string sql = " where  (type ='70'or type='01') and ProjectID='" + this.ProjectUID + "'";
            IList list = Services.BaseService.GetList("SelectPSPDEVByCondition", sql);

            lookUpEdit1.Properties.DataSource = list;
            string con = " where Type ='01' and ProjectID ='" + this.ProjectUID + "' order by name";
            list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            foreach (PSPDEV pspdev in list)
            {
                if (comboBoxEdit4.Properties.Items.IndexOf(pspdev.Name) == -1)
                {
                    comboBoxEdit4.Properties.Items.Add(pspdev.Name);
                    
                }
            }

            //Dlqiname.Text=
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
            string con = " where Type ='01'and ProjectID ='" + this.ProjectSUID + "'";
            IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            foreach (PSPDEV pspdev in list)
            {
                if (comboBoxEdit4.Properties.Items.IndexOf(pspdev.Name) == -1)
                {
                    comboBoxEdit4.Properties.Items.Add(pspdev.Name);

                }
            }
            string ss = " ProjectID='" + Itop.Client.MIS.ProgUID + "' ";
            IList listq = UCDeviceBase.DataService.GetList("SelectPS_Table_AreaWHByConn", ss);
            comboBoxEdit9.Properties.DataSource = listq;
            ss = " ProjectID='" + Itop.Client.MIS.ProgUID + "' ";
            listq = UCDeviceBase.DataService.GetList("SelectPS_Table_Area_TYPEByConn", ss);
            foreach (PS_Table_Area_TYPE at in listq)
            {
                comboBoxEdit8.Properties.Items.Add(at.Title);
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
        public string SubstationName
        {
            get
            {
                return subsedit.Text;
            }
            set
            {
                subsedit.Text = value;
            }
        }
        public string outI
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
        public string DlqiType
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
        public string MinSwitchtime
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
                return lookUpEdit1.EditValue.ToString();
            }
            set 
            {
                lookUpEdit1.EditValue = value;
            }
        }
        public string DlqiZl
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
        private string projectID;
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
        public int Iswitch
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
        string projectid;
        public string ProjectID
        {
            get { return projectid; }
            set { projectid = value; }
        }
        string parentid;
        public string ParentID
        {
            get { return parentid; }
            set { parentid = value; }
        }
        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = this.textEdit1.Text;
            e.Handled = e.KeyChar < '0' || e.KeyChar > '9';   //允许输入数字
            if (e.KeyChar == (char)8)   //允许输入回退键
            {
                e.Handled = false;
            }
            if (e.KeyChar == (char)46)
            {
                if (str == "")   //第一个不允许输入小数点
                {
                    e.Handled = true;
                    return;
                }
                else
                { //小数点不允许出现2次
                    foreach (char ch in str)
                    {
                        if (char.IsPunctuation(ch))
                        {
                            e.Handled = true;
                            return;
                        }
                    }
                    e.Handled = false;
                }
            }
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0)) e.Handled = true;
        }

        private void textEdit2_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = this.textEdit2.Text;
            e.Handled = e.KeyChar < '0' || e.KeyChar > '9';   //允许输入数字
            if (e.KeyChar == (char)8)   //允许输入回退键
            {
                e.Handled = false;
            }
            if (e.KeyChar == (char)46)
            {
                if (str == "")   //第一个不允许输入小数点
                {
                    e.Handled = true;
                    return;
                }
                else
                { //小数点不允许出现2次
                    foreach (char ch in str)
                    {
                        if (char.IsPunctuation(ch))
                        {
                            e.Handled = true;
                            return;
                        }
                    }
                    e.Handled = false;
                }
            }
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0)) e.Handled = true;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textEdit3_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = this.textEdit3.Text;
            e.Handled = e.KeyChar < '0' || e.KeyChar > '9';   //允许输入数字
            if (e.KeyChar == (char)8)   //允许输入回退键
            {
                e.Handled = false;
            }
            if (e.KeyChar == (char)46)
            {
                if (str == "")   //第一个不允许输入小数点
                {
                    e.Handled = true;
                    return;
                }
                else
                { //小数点不允许出现2次
                    foreach (char ch in str)
                    {
                        if (char.IsPunctuation(ch))
                        {
                            e.Handled = true;
                            return;
                        }
                    }
                    e.Handled = false;
                }
            }
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0)) e.Handled = true;
        }

        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
             
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
            panelControl1.Refresh();
        }

        private void comboBoxEdit4_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSPDEV devMX = new PSPDEV();
            string strCon = strCon = " WHERE Name = '" + comboBoxEdit4.Text + "' AND ProjectID = '" + this.ProjectSUID + "' AND Type = '01'";
            devMX = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", strCon);
            if (devMX != null)
            {
                dev.FirstNode = devMX.Number;
            }
        }


        private void subsedit_EditValueChanged(object sender, EventArgs e)
        {

        }
        private void setBdzName()
        {
            //显示所在位置的名称
            object obj = DeviceHelper.GetDevice<PSP_Substation_Info>(dev.SvgUID);

            if (obj != null)
            {
                subsedit.Text = ((PSP_Substation_Info)obj).Title;
                return;
            }
            obj = DeviceHelper.GetDevice<PSP_PowerSubstation_Info>(dev.SvgUID);
            if (obj != null)
            {
                subsedit.Text = ((PSP_PowerSubstation_Info)obj).Title;
                return;
            }

        }
        private void subsedit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmDeviceSelect dlg = new frmDeviceSelect();


            dlg.InitDeviceType("20", "30");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Dictionary<string, object> dic = dlg.GetSelectedDevice();
                subsedit.Text = dic["name"].ToString();
                dev.SvgUID = dic["id"].ToString();
            }
        }

        private void lookUpEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmDeviceSelect dlg = new frmDeviceSelect();


            dlg.InitDeviceType("01","70");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Dictionary<string, object> dic = dlg.GetSelectedDevice();
                lookUpEdit1.EditValue = dic["id"].ToString();
                PSPDEV pdv=dic["device"] as PSPDEV;
                if (pdv.Type=="01")
                {
                    dev.FirstNode=pdv.Number;
                }
                else{dev.FirstNode=pdv.FirstNode;}
                
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

        private void radioGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelControl2.Refresh();
        }

    }
}