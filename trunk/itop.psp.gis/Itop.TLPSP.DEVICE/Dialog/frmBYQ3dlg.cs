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
    /// ĸ�����Ա༭����
    /// </summary>
    public partial class frmBYQ3dlg : Itop.Client.Base.FormBase
    {
        PSPDEV dev = new PSPDEV();
        public PSPDEV DeviceMx
        {
            get{
                dev.Name = textEdit1.Text;
                dev.Number = (int)spinEdit8.Value;
                dev.OperationYear = OperationYear;
                dev.Type = "03";
                /// <summary>
                /// ĸ��
                /// </summary>
                dev.IName = comboBoxEdit3.Text;
                dev.JName = comboBoxEdit4.Text;
                dev.KName = comboBoxEdit7.Text;
                /// <summary>
                /// ����
                /// </summary>
                dev.ISwitch = comboBoxEdit5.Text;
                dev.JSwitch = comboBoxEdit6.Text;
                dev.HuganLine1 = comboBoxEdit2.Text;
                /// <summary>
                /// ��λ
                /// </summary>
                dev.UnitFlag = UnitFlag.ToString();
                /// <summary>
                /// ����״̬
                /// </summary>
                dev.KSwitchStatus = KswitchStatus.ToString();
                dev.HuganLine3 = (string)radioGroup2.EditValue;
                dev.HuganLine4 = (string)radioGroup4.EditValue;
                dev.HuganLine2 = (string)radioGroup7.EditValue;
                /// <summary>
                ///���ӷ�ʽ
                /// </summary>
                dev.LineLevel = (string)radioGroup6.EditValue;
                dev.LineType = (string)radioGroup5.EditValue;
                dev.LineStatus = (string)radioGroup8.EditValue;
                /// <summary>
                ///�����
                /// </summary>                
                dev.SiN = (double)spinEdit1.Value;
                dev.SjN = (double)spinEdit45.Value;
                dev.SkN = (double)spinEdit48.Value;
                /// <summary>
                /// ��ѹ
                /// </summary>
                dev.Vi0 = (double)spinEdit5.Value;            
                dev.Vj0 = (double)spinEdit2.Value;
                dev.Vk0 = (double)spinEdit50.Value;
                dev.Vib = (double)spinEdit47.Value;
                dev.Vjb = (double)spinEdit46.Value;
                dev.Vkb = (double)spinEdit49.Value;
                /// <summary>
                /// ���
                /// </summary>
                dev.K = (double)spinEdit52.Value;
                dev.StandardCurrent = (double)spinEdit3.Value;
                dev.BigP = (double)spinEdit51.Value;
                /// <summary>
                /// ���Ե�
                /// </summary>
                dev.SmallTQ = (double)spinEdit68.Value;
                dev.BigTQ = (double)spinEdit67.Value;
                /// <summary>
                /// ��·���
                /// </summary>
                dev.Pij = (double)spinEdit14.Value;
                dev.Pjk = (double)spinEdit15.Value;
                dev.Pik = (double)spinEdit16.Value;
                /// <summary>
                /// ��ѹ�ٷֱ�
                /// </summary>
                dev.Vij = (double)spinEdit19.Value;
                dev.Vjk = (double)spinEdit18.Value;
                dev.Vik = (double)spinEdit17.Value;

                dev.P0 = (double)spinEdit54.Value;
                dev.I0 = (double)spinEdit53.Value;
                /// <summary>
                /// �迹
                /// </summary>
                dev.HuganTQ1 = (double)spinEdit63.Value;
                dev.HuganTQ2 = (double)spinEdit62.Value;
                dev.HuganTQ3 = (double)spinEdit61.Value;
                dev.HuganTQ4 = (double)spinEdit60.Value;
                dev.HuganTQ5 = (double)spinEdit59.Value;
                dev.ZeroTQ =(double) spinEdit58.Value;
                dev.G = (double)spinEdit66.Value;
                dev.LineGNDC = (double)spinEdit64.Value;
                dev.iV = (double)spinEdit65.Value;
                dev.jV = (double)spinEdit69.Value;
                //��ͷ
                dev.Vipos = (double)spinEdit7.Value;
                dev.Vistep = (double)spinEdit6.Value;
                dev.Vimax = (double)spinEdit4.Value;
                dev.Vimin = (double)spinEdit9.Value;
                dev.Vjpos = (double)spinEdit13.Value;
                dev.Vjstep = (double)spinEdit12.Value;
                dev.Vjmax = (double)spinEdit11.Value;
                dev.Vjmin=(double)spinEdit10.Value;
                dev.Vkpos = (double)spinEdit44.Value;
                dev.Vkstep = (double)spinEdit22.Value;
                dev.Vkmax = (double)spinEdit21.Value;
                dev.Vkmin = (double)spinEdit20.Value;

                dev.Date1 = date1.Text;
                dev.Date2 = date2.Text;
                return dev;
            }
            set{
                dev = value;
                Name = dev.Name;
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
                comboBoxEdit7.Text = dev.KName;

                comboBoxEdit5.Text = dev.ISwitch;
                comboBoxEdit6.Text = dev.JSwitch;
                comboBoxEdit2.Text = dev.HuganLine1;

                radioGroup2.EditValue = dev.HuganLine3;
                radioGroup4.EditValue = dev.HuganLine4;
                radioGroup7.EditValue = dev.HuganLine2;

                radioGroup6.EditValue = dev.LineLevel;
                radioGroup5.EditValue = dev.LineType;
                radioGroup8.EditValue = dev.LineStatus;

                /// <summary>
                ///�����
                /// </summary>                
                spinEdit1.Value = (decimal)dev.SiN;
                spinEdit45.Value = (decimal)dev.SjN;
                spinEdit48.Value = (decimal)dev.SkN;
                /// <summary>
                /// ��ѹ
                /// </summary>
                spinEdit5.Value = (decimal)dev.Vi0;
                spinEdit2.Value = (decimal)dev.Vj0;
                spinEdit50.Value = (decimal)dev.Vk0;
                spinEdit47.Value = (decimal)dev.Vib;
                spinEdit46.Value = (decimal)dev.Vjb;
                spinEdit49.Value = (decimal)dev.Vkb;
                /// <summary>
                /// ���
                /// </summary>
                spinEdit52.Value = (decimal)dev.K;
                spinEdit3.Value = (decimal)dev.StandardCurrent;
                spinEdit51.Value = (decimal)dev.BigP;
                /// <summary>
                /// ���Ե�
                /// </summary>
                spinEdit68.Value = (decimal)dev.SmallTQ;
                spinEdit67.Value = (decimal)dev.BigTQ;
                /// <summary>
                /// ��·���
                /// </summary>
                spinEdit14.Value = (decimal)dev.Pij;
                spinEdit15.Value = (decimal)dev.Pjk;
                spinEdit16.Value = (decimal)dev.Pik;
                /// <summary>
                /// ��·���
                /// </summary>
                spinEdit19.Value = (decimal)dev.Vij;
                spinEdit18.Value = (decimal)dev.Vjk;
                spinEdit17.Value = (decimal)dev.Vik;

                spinEdit54.Value = (decimal)dev.P0;
                spinEdit53.Value = (decimal)dev.I0;
                /// <summary>
                /// �迹
                /// </summary>
                spinEdit63.Value = (decimal)dev.HuganTQ1;
                spinEdit62.Value = (decimal)dev.HuganTQ2;
                spinEdit61.Value = (decimal)dev.HuganTQ3;
                spinEdit60.Value = (decimal)dev.HuganTQ4;
                spinEdit59.Value = (decimal)dev.HuganTQ5;
                spinEdit58.Value = (decimal)dev.ZeroTQ;
                spinEdit66.Value = (decimal)dev.G;
                spinEdit64.Value = (decimal)dev.LineGNDC;
                spinEdit65.Value = (decimal)dev.iV;
                spinEdit69.Value = (decimal)dev.jV;
                //��ͷ
                spinEdit7.Value = (decimal)dev.Vipos;
                spinEdit6.Value = (decimal)dev.Vistep;
                spinEdit4.Value = (decimal)dev.Vimax;
                spinEdit9.Value = (decimal)dev.Vimin;
                spinEdit13.Value= (decimal)dev.Vjpos ;
                spinEdit12.Value = (decimal)dev.Vjstep;
                spinEdit11.Value = (decimal)dev.Vjmax;
                spinEdit10.Value= (decimal)dev.Vjmin ;
                spinEdit44.Value = (decimal)dev.Vkpos;
                spinEdit22.Value = (decimal)dev.Vkstep;
                spinEdit21.Value= (decimal)dev.Vkmax;
                spinEdit20.Value = (decimal)dev.Vkmin;

                date1.Text = dev.Date1;
                date2.Text = dev.Date2;
            }
        }
        public frmBYQ3dlg() {
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
                    comboBoxEdit7.Properties.Items.Add(dev.Name);
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
                    comboBoxEdit2.Properties.Items.Add(dev.Name);
                }
            }
            //û�еĻ� ����һ����ֵ
            if (list.Count > 0)
            {
                comboBoxEdit5.Text = (list[0] as PSPDEV).Name;
                comboBoxEdit6.Text = (list[0] as PSPDEV).Name;
                comboBoxEdit2.Text = (list[0] as PSPDEV).Name;
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
        #region ����
        /// <summary>
        /// ĸ������
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
        /// ĸ�߱��
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
        /// ���ѹ
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
        /// ��׼��ѹ
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
        /// Ͷ�����
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
        /// ��ѹ��ֵ
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
        /// ��ѹ���
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
        /// ��·����
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
        /// �����й�
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
        /// �����޹�
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
        /// �����й�
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
        /// �����޹�
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
        /// ��λ
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
        /// ����״̬
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
                    double VPij = 0, VPjk = 0, VPik = 0, Pi = 0, Pj = 0, Pk = 0;
                    VPij = Convert.ToDouble(DeviceMx.Pij) * (Convert.ToDouble(DeviceMx.SiN) / Convert.ToDouble(DeviceMx.SjN)) * (Convert.ToDouble(DeviceMx.SiN) / Convert.ToDouble(DeviceMx.SjN));
                    VPjk = Convert.ToDouble(DeviceMx.Pjk) * (Convert.ToDouble(DeviceMx.SiN) / Math.Min(Convert.ToDouble(DeviceMx.SjN), Convert.ToDouble(DeviceMx.SkN))) * (Convert.ToDouble(DeviceMx.SiN) / Math.Min(Convert.ToDouble(DeviceMx.SjN), Convert.ToDouble(DeviceMx.SkN)));
                    VPik = Convert.ToDouble(DeviceMx.Pik) * (Convert.ToDouble(DeviceMx.SiN) / Convert.ToDouble(DeviceMx.SkN)) * (Convert.ToDouble(DeviceMx.SiN) / Convert.ToDouble(DeviceMx.SkN));
                    Pi = (VPij + VPik - VPjk) / 2;
                    Pj = (VPij + VPjk - VPik) / 2;
                    Pk = (VPjk + VPik - VPij) / 2;
                    double SN = Convert.ToDouble(DeviceMx.SiN);
                    double V = Convert.ToDouble(DeviceMx.Vi0);
                    if (Convert.ToDouble(DeviceMx.Vj0) > V)
                    {
                        V = Convert.ToDouble(DeviceMx.Vj0);
                        SN = Convert.ToDouble(DeviceMx.SjN);
                    }
                    if (Convert.ToDouble(DeviceMx.Vk0) > V)
                    {
                        V = Convert.ToDouble(DeviceMx.Vk0);
                        SN = Convert.ToDouble(DeviceMx.SkN);
                    }
                    DeviceMx.HuganTQ1 = Pi * 100 / (1000 * SN * SN);
                    DeviceMx.HuganTQ2 = Pj * 100 / (1000 * SN * SN);
                    DeviceMx.HuganTQ3 = Pk * 100 / (1000 * SN * SN);
                    double Vi = 0, Vj = 0, Vk = 0;
                    Vi = (Convert.ToDouble(DeviceMx.Vij) + Convert.ToDouble(DeviceMx.Vik) - Convert.ToDouble(DeviceMx.Vjk)) / 2;
                    Vj = (Convert.ToDouble(DeviceMx.Vij) + Convert.ToDouble(DeviceMx.Vjk) - Convert.ToDouble(DeviceMx.Vik)) / 2;
                    Vk = (Convert.ToDouble(DeviceMx.Vik) + Convert.ToDouble(DeviceMx.Vjk) - Convert.ToDouble(DeviceMx.Vij)) / 2;
                    DeviceMx.HuganTQ4 = Vi * 100 / (100 * 100 * SN);
                    DeviceMx.HuganTQ5 = Vj * 100 / (100 * 100 * SN);
                    DeviceMx.ZeroTQ = (Vk * 100 / (100 * 100 * SN));
                    DeviceMx.K = (Convert.ToDouble(DeviceMx.Vimax) - Convert.ToDouble(DeviceMx.Vi0) * Convert.ToDouble(DeviceMx.Vistep) * (Convert.ToDouble(DeviceMx.Vipos) - 1) / 100) / Convert.ToDouble(DeviceMx.Vib);
                    DeviceMx.StandardCurrent = (Convert.ToDouble(DeviceMx.Vjmax) - Convert.ToDouble(DeviceMx.Vj0) * Convert.ToDouble(DeviceMx.Vjstep) * (Convert.ToDouble(DeviceMx.Vjpos) - 1) / 100) / Convert.ToDouble(DeviceMx.Vjb);
                    DeviceMx.BigP = (Convert.ToDouble(DeviceMx.Vkmax) - Convert.ToDouble(DeviceMx.Vk0) * Convert.ToDouble(DeviceMx.Vkstep) * (Convert.ToDouble(DeviceMx.Vkpos) - 1) / 100) / Convert.ToDouble(DeviceMx.Vkb);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("����д��Ӧ��ͷ��Ϣ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }  
            }
        }
           

        private void xtraTabPage2_Paint(object sender, PaintEventArgs e)
        {

        }
        /// <summary>
        /// ĸ������
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
            frmDevicetemplateSelect dlg = new frmDevicetemplateSelect();

            dlg.InitDeviceType("03");
            if (dlg.ShowDialog() == DialogResult.OK)
            {

                object obj = dlg.GetSelectedDevice()["device"];
                if (obj != null && obj is Template_PSPDEV)
                {
                    DeviceHelper.CopyTemplate(obj as Template_PSPDEV, dev);
                    DeviceMx = dev;
                }
            }
        }

        private void comboBoxEdit7_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSPDEV devMX = new PSPDEV();
            string strCon = strCon = " WHERE Name = '" + comboBoxEdit7.Text + "' AND ProjectID = '" + this.ProjectSUID + "' AND Type = '01'";
            devMX = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", strCon);
            if (devMX != null)
            {
                spinEdit50.Value = (decimal)devMX.RateVolt;
                spinEdit49.Value = (decimal)devMX.ReferenceVolt;
                dev.Flag = devMX.Number;
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void spinEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void comboBoxEdit1_SelectedValueChanged(object sender, EventArgs e)
        {
            date1.Text = comboBoxEdit1.Text;
        }
    }
}