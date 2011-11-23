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
using Itop.Client.Base;
namespace Itop.TLPsp
{
    public partial class AddDlqiform : FormBase
    {
        public AddDlqiform()
        {
            InitializeComponent();
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
        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
        public void com(PSPDEV dev)
        {
            subsedit.Text = dev.HuganLine1;
            Dlqiname.Text = dev.Name;
            textEdit1.Text = dev.HuganTQ1.ToString();
            comboBoxEdit1.Text = dev.HuganLine2;
            textEdit3.Text = dev.HuganTQ2.ToString();
            textEdit2.Text = dev.HuganTQ3.ToString();
            comboBoxEdit2.Text = dev.KSwitchStatus;

        }
        public void intdata(PSPDEV dev)
        {
            subsedit.Text = dev.HuganLine1;   //读取母线节点所属的变电站
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
        public string DlqiSwitch
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
    }
}