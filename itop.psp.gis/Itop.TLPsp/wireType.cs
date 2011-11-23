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
    public partial class wireType : FormBase
    {
        string wirell;
        public wireType(string wirel)
        {
            wirell = wirel;
            InitializeComponent();
        }
        public string WireType
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

        public string WireR
        {
            get
            {
                if (textBox2.Text == "" || textBox2.Text == null)
                {
                    return "0";
                } 
                else
                {
                    return textBox2.Text;
                }
                
            }
            set
            {
                textBox2.Text = value;
            }
        }
        public string WireTQ
        {
            get
            {
                if (textBox3.Text == "" || textBox3.Text == null)
                {
                    return "0";
                }
                else
                {
                    return textBox3.Text;
                }
            }
            set
            {
                textBox3.Text = value;
            }
        }
        public string WireGNDC
        {
            get
            {
                if (textBox4.Text == "" || textBox4.Text == null)
                {
                    return "0";
                }
                else
                {
                    return textBox4.Text;
                }
            }
            set
            {
                textBox4.Text = value;
            }
        }
        public string WireChange
        {
            get
            {
                if (textBox5.Text == "" || textBox5.Text == null)
                {
                    return "0";
                }
                else
                {
                    return textBox5.Text;
                }
            }
            set
            {
                textBox5.Text = value;
            }
        }
        public string WireLevel
        {
            get
            {
                if (comboBoxEdit1.Text == "" || comboBoxEdit1.Text == null)
                {
                    return "0";
                }
                else
                {
                    return comboBoxEdit1.Text;
                }
            }
            set
            {
                comboBoxEdit1.Text = value;
            }
        }

        private void simpleButton1_MouseDown(object sender, MouseEventArgs e)
        {
            if (textBox1.Text==""||textBox1.Text==null)
            {
                MessageBox.Show("名称不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            } 
            else 
            {
                WireCategory wirewire = new WireCategory();

                wirewire.WireLevel = WireLevel;
                IList list = Services.BaseService.GetList("SelectWireCategoryListBYWireLevel", wirewire);
                foreach (WireCategory wire in list)
                {
                    if (wire.WireType==textBox1.Text)
                    {
                        MessageBox.Show("型号不能重复。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                this.DialogResult = DialogResult.OK;
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

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = this.textBox5.Text;
            e.Handled = e.KeyChar < '0' || e.KeyChar > '9';   //允许输入数字
            if (e.KeyChar == (char)8)   //允许输入回退键

            {
                e.Handled = false;
            }

            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0)) e.Handled = true;
        }

        private void wireType_Load(object sender, EventArgs e)
        {

        }

        //private void textEdit1_EditValueChanged(object sender, EventArgs e)
        //{

        //}

        //private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    string str = this.textEdit1.Text;
        //    //e.Handled = e.KeyChar < '0' || e.KeyChar > '9';   //允许输入数字
        //    if (e.KeyChar == (char)8)   //允许输入回退键
        //    {
        //        e.Handled = false;
        //    }

        //    //if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0)) e.Handled = true;
        
        //}
    }
}