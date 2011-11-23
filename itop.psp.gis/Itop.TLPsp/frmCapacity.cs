using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using System.Collections;
using Itop.Client.Common;
using Itop.Client.Base;
namespace Itop.TLPsp
{
    public partial class frmCapacity : FormBase
    {
        private string UID;
        private string _ReferenceVolt;
        public string svgUID
        {
            get
            {
                return UID;
            }
            set
            {
                UID = value;
            }
        }
        public frmCapacity(string svgDocumentUID)
        {
            svgUID = svgDocumentUID;
            InitializeComponent();
            InitCom();
        }
        public frmCapacity(PSPDEV dev, string svgDocumentUID)
        {
            svgUID = svgDocumentUID;
            InitializeComponent();
            InitCom();
            InitData(dev);
        }
        private void InitData(PSPDEV dev)
        {
            if (dev!=null)
            {
                Name = dev.Name;
                PositiveTQ = dev.PositiveTQ.ToString();
                Lable = dev.Lable;
                FirstNodeName = dev.HuganLine1;
                //LastNodeName = dev.HuganLine2;
                belongline = dev.HuganLine4;
                VoltR = dev.VoltR.ToString();
               // ReferenceVolt = dev.ReferenceVolt.ToString();
                SwitchStatus = dev.HuganLine3;
            }
        }
        private void InitCom()
        {
            PSPDEV psp = new PSPDEV();
            psp.SvgUID = UID;
            psp.Lable = "母线节点";
            psp.Type = "Use";
            IList list = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", psp);
            foreach (PSPDEV dev in list)
            {
                busclass bus = new busclass(dev.Number, dev.Name);
                comboBoxEdit1.Properties.Items.Add(bus);
               // comboBoxEdit2.Properties.Items.Add(bus);

            }
            PSPDEV pspDev = new PSPDEV();
            pspDev.Type = "Polyline";
            pspDev.SvgUID = UID;
            pspDev.Lable = "支路";
            IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", pspDev);
            foreach (PSPDEV dev in list1)
            {
                busclass bus = new busclass(dev.Number, dev.Name);
                comboBoxEdit6.Properties.Items.Add(bus);
                // comboBoxEdit2.Properties.Items.Add(bus);

            }
            //psp.SvgUID=UID;
            //psp.Type = "";
            
        }
        public string Name
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }
        public string FirstNodeName
        {
            get
            {
                return comboBoxEdit1.Text;
            }
            set
            {
                comboBoxEdit1.Text = value;
                if (value != "")
                {
                    PSPDEV psp = new PSPDEV();
                    psp.Name = value;
                    psp.Type = "Use";
                    psp.SvgUID = svgUID;
                    psp = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", psp);
                    if(psp!=null)
                   ReferenceVolt = psp.ReferenceVolt.ToString();
                }
            }
        }
        public string VoltR
        {
            get
            {
                return comboBoxEdit5.Text;
            }
            set
            {
                comboBoxEdit5.Text = value;
            }
        }
        public string ReferenceVolt
        {
            get
            {
                return _ReferenceVolt;
            }
            set
            {
                _ReferenceVolt = value;
            }
        }
        //public string LastNodeName
        //{
        //    get
        //    {
        //        return comboBoxEdit2.Text;
        //    }
        //    set
        //    {
        //        comboBoxEdit2.Text = value;
        //    }
        //}
        public string belongline
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
        public string SwitchStatus
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
        public string Lable
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
        public string PositiveTQ
        {
            get
            {
                return textBox2.Text;
            }
            set
            {
                textBox2.Text = value;
            }
        }
        public void SetEnable(bool flag)
        {
            if (flag==true)
            {
                label2.Visible = false;
                comboBoxEdit1.Visible = false;
                label7.Visible = true;
                comboBoxEdit6.Visible = true;
                this.Text = "串联电容电抗器";
            } 
            else
            {
                label2.Visible = true;
                comboBoxEdit1.Visible = true;
                label7.Visible = false;
                comboBoxEdit6.Visible =false;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = this.textBox2.Text;
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


    }
    public class busclass
    {
        public int busnum;
        public string  busname;
        public busclass(int _busnum,string _busname)
        {
            busnum=_busnum;
            busname=_busname;
        }
        public override string ToString()
        {
            return busname;
        }   
    }
}