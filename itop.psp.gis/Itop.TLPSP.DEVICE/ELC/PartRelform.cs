using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Client.Base;
using System.IO;
using System.Threading;
using System.Xml.XPath;
using Itop.Client.Common;
using System.Diagnostics;

namespace Itop.TLPSP.DEVICE
{
    public partial class PartRelform : FormBase
    {
        PSPDEV leel = new PSPDEV();
        public List<int> lineVnumlist = new List<int>();
        public List<int> lineDnumlist = new List<int>();
        public string deleteprojec = null;
        public PartRelform()
        {
            InitializeComponent();
            //WireCategory wirewire = new WireCategory();
            //IList list1 = UCDeviceBase.DataService.GetList("SelectWireCategoryList", wirewire);
        }
        public PartRelform(PSPDEV pspDEV)
        {
            InitializeComponent();
            leel = pspDEV;
            //WireCategory wirewire = new WireCategory();
            //IList list1 = UCDeviceBase.DataService.GetList("SelectWireCategoryList", wirewire);
            //foreach (WireCategory sub in list1)
            //{
 
            //}

        }

        private void Compubton_Click(object sender, EventArgs e)
        {

            if (SelectVtype.Checked)
            {
                string tempp = comboV.Text;
                int tel = tempp.Length;
                tempp = tempp.Substring(0, tel - 2);
                //pspDev.VoltR = Convert.ToDouble(tempp);
                leel.RateVolt = Convert.ToDouble(tempp);
                //textBox2.Text=
                string  con= ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + leel.SvgUID + "'AND Type='05'AND RateVolt='" + tempp + "'";
                IList list1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                // int j = 0;
                if (list1.Count==0)
                {
                    MessageBox.Show("没有此电压等级的线路，请重新选择电压等级！");
                    return;
                }
                else
                {
                    for (int i = 0; i < list1.Count; i++)
                    {
                        PSPDEV psp = (PSPDEV)list1[i];
                        if (psp.KSwitchStatus == "0")
                        {
                            lineVnumlist.Add(psp.Number);
                        }
                    }
                    lineVnumlist.Sort();

                    this.DialogResult = DialogResult.Ignore;
                }
                
            }
            if (DefineDelete.Checked)
            {
                this.DialogResult = DialogResult.Yes;
            }
        }
        public string LineLevel                           //选择电压等级
        {
            get
            {
                if (comboV.Text == "")
                    comboV.Text = "0";
                return comboV.Text;
            }
            set
            {
                comboV.Text = value;
            }
        }

        private void SelectVtype_CheckedChanged(object sender, EventArgs e)
        {
            if (SelectVtype.Checked)
            {
                comboV.Enabled = true;
            }
            else
                comboV.Enabled = false;
        }

        private void DefineDelete_CheckedChanged(object sender, EventArgs e)
        {
            if (DefineDelete.Checked)
            {
                Selbutt.Enabled = true;
            }
            else
                Selbutt.Enabled = false;
        }

        private void Selbutt_Click(object sender, EventArgs e)
        {
            if (DefineDelete.Checked)
            {
                this.Visible = false;
                DefineDelform df = new DefineDelform(leel);
                df.ShowDialog();
                if (df.DialogResult==DialogResult.OK)
                {
                    this.Visible = true;
                    lineDnumlist = df.linenums;
                    lineVnumlist = df.transnums;
                    deleteprojec = df.Defineproject;
                }
            }
        }

        private void Cancelbton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}