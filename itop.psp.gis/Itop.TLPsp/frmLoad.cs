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
    public partial class frmLoad : FormBase
    {
        private string UID;
        public frmLoad()
        {
            InitializeComponent();
            InitComb();
        }
        public frmLoad(string _UID)
        {
            UID = _UID;
            InitializeComponent();
            InitComb();
        }
        public frmLoad(PSPDEV dev)
        {
            InitializeComponent();
            InitComb();
            InitData(dev);
        }
        private void InitData(PSPDEV dev)
        {
            if (dev!=null)
            {
                Name = dev.Name;
                comboBoxEdit1.Text = dev.HuganLine1;
                comboBoxEdit2.Text = dev.HuganLine3;
                InPutP = dev.InPutP.ToString();
                InPutQ = dev.InPutQ.ToString();
                VoltR = dev.VoltR.ToString();
            }
        }
        private void InitComb()
        {
            PSPDEV psp = new PSPDEV();
            psp.SvgUID = UID;
            psp.Lable = "母线节点";
            psp.Type = "Use";
            IList list = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType",psp);
            foreach (PSPDEV dev in list)
            {
                comboBoxEdit1.Properties.Items.Add(dev.Name);
            }               
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
        public string FirstNodeName
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
        public string LoadSwitchState
        {
            get
            {
                return comboBoxEdit2.Text;
            }
            set
            {
                comboBoxEdit2.Text = value;
            }
        }
        public string InPutP
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
        public string InPutQ
        {
            get
            {
                return textBox3.Text;
            }
            set
            {
                textBox3.Text = value;
            }
        }
        public string VoltR
        {
            get
            {
                return textBox4.Text;
            }
            set
            {
                textBox4.Text = value;
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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = this.textBox3.Text;
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

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = this.textBox4.Text;
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
}