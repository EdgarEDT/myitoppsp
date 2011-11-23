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
    public partial class frmGNDLine : FormBase
    {
        string localsvgUID;
        PSPDEV leel = new PSPDEV();
        public frmGNDLine()
        {
            InitializeComponent();
        }
        public frmGNDLine(string svg)
        {
            localsvgUID = svg;
            InitializeComponent();
            InitData(svg);
            InitFrm(null);
        }
        public frmGNDLine(string svg,PSPDEV psp)
        {
            leel = psp;
            localsvgUID = svg;
            InitializeComponent();
            InitData(svg);
            InitFrm(psp);
        }
        public void InitData(string svgUID)
        {
            PSPDEV pspDev = new PSPDEV();
            pspDev.SvgUID = svgUID;
            pspDev.Type = "Use";
            IList list = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            foreach (PSPDEV dev in list)
            {
                comboBoxEdit1.Properties.Items.Add(dev.Name);
                comboBoxEdit2.Properties.Items.Add(dev.Name);
            }
        }
        public void InitFrm(PSPDEV dev)
        {
            if (dev==null)
            {
                textBox1.Text = "";
                textBox2.Text = "0";
                textBox3.Text = "0";
                textBox4.Text = "0";
                textBox5.Text = "0";
                textBox6.Text = "0";
                comboBoxEdit1.Text = "";
                comboBoxEdit2.Text = "";
                comboBoxEdit8.Text = "";
            }
            else
            {
                textBox1.Text = dev.Name;
                textBox2.Text = dev.LineR.ToString();
                textBox3.Text = dev.LineTQ.ToString();
                textBox4.Text = dev.LineGNDC.ToString();
                textBox5.Text = dev.K.ToString();
                textBox6.Text = dev.G.ToString();
                comboBoxEdit1.Text = dev.HuganLine1;
                comboBoxEdit2.Text = dev.HuganLine2;
                comboBoxEdit8.Text = dev.ReferenceVolt.ToString();            
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
        public string LineR
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

        public string LineTQ
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
        public string LineGNDC
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
        public string K
        {
            get
            {
                return textBox5.Text;
            }
            set
            {
                textBox5.Text = value;
            }
        }
        public string G
        {
            get
            {
                return textBox6.Text;
            }
            set
            {
                textBox6.Text = value;
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
        public string LastNodeName
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
        public string ReferenceVolt //电压等级
        {
            get
            {
                if (comboBoxEdit8.Text == "")
                    comboBoxEdit8.Text = "0";
                return comboBoxEdit8.Text;
            }
            set
            {
                comboBoxEdit8.Text = value;
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

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = this.textBox6.Text;
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

        private void simpleButton1_MouseDown(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("名称不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                PSPDEV pspName = new PSPDEV();
                pspName.Name = textBox1.Text;
                pspName.SvgUID = localsvgUID;
                pspName.Type = "TransformLine";
                IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                pspName.Type = "PolyLine";
                IList listName1 = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                if (listName.Count > 1 || (listName.Count == 1 && (((PSPDEV)listName[0]).SUID) != leel.SUID ) || listName1.Count > 1 || (listName1.Count == 1 && ((PSPDEV)listName1[0]).EleID != leel.EleID))
                {
                    MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    simpleButton1.DialogResult = DialogResult.OK;
                }
            }   
        }
    }
}