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
    public partial class frmTwoTra : FormBase
    {
        private string UID;
        private string _vib;
        private string _vjb;
        public frmTwoTra(string svgDocumentUID)
        {
            svgUID = svgDocumentUID;
            InitializeComponent();
            InitCom();
        }
        public frmTwoTra(PSPDEV dev, string svgDocumentUID)
        {
            svgUID = svgDocumentUID;
            InitializeComponent();
            InitData(dev);
            InitCom();
        }
        private void InitData(PSPDEV dev)
        {
            if (dev!=null)
            {
                Name = dev.Name;
                FirstName = dev.HuganLine1;
                LastName = dev.HuganLine2;
                FirstSwitchState = dev.HuganLine3;
                LastSwitchState = dev.HuganLine4;
                FirstType = dev.LineLevel;
                LastType = dev.LineType;
                K = dev.K.ToString();
                PositiveR = dev.PositiveR.ToString();
                PositiveTQ = dev.PositiveTQ.ToString();
                ZeroR = dev.ZeroR.ToString();
                ZeroTQ = dev.ZeroTQ.ToString();
                NeutralNodeTQ = dev.BigTQ.ToString();
                NeutralNodeR = dev.SmallTQ.ToString();
                Pij = dev.Pij.ToString();
                Vij = dev.Vij.ToString();
                Vi0 = dev.Vi0.ToString();
                Vipos = dev.Vipos.ToString();
                Vistep = dev.Vistep.ToString();
                Vimax = dev.Vimax.ToString();
                Vimin = dev.Vimin.ToString();
                P0 = dev.P0.ToString();
                I0 = dev.I0.ToString();
                SiN = dev.SiN.ToString();
                Vj0 = dev.Vj0.ToString();
                Vjpos = dev.Vjpos.ToString();
                Vjstep = dev.Vjstep.ToString();
                Vjmax = dev.Vjmax.ToString();
                Vjmin = dev.Vjmin.ToString();
                //Vib = dev.Vib.ToString();
                //Vjb = dev.Vjb.ToString();
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
                comboBoxEdit1.Properties.Items.Add(dev.Name);
                comboBoxEdit2.Properties.Items.Add(dev.Name);
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
        public string FirstName
        {
            get
            {
               
                return comboBoxEdit1.Text;
            }
            set
            {
                comboBoxEdit1.Text = value;
                if (value!="")
                {
                    PSPDEV psp = new PSPDEV();
                    psp.Name = value;
                    psp.Type = "Use";
                    psp.SvgUID = svgUID;
                    psp =(PSPDEV) Services.BaseService.GetObject("SelectPSPDEVByName", psp);
                    if(psp!=null)
                    Vib = psp.ReferenceVolt.ToString();
                }
            }
        }
        public string LastName
        {
            get
            {
                return comboBoxEdit2.Text;
            }
            set
            {
                comboBoxEdit2.Text = value;
                if (value != "")
                {
                    PSPDEV psp = new PSPDEV();
                    psp.Name = value;
                    psp.Type = "Use";
                    psp.SvgUID = svgUID;
                    psp =(PSPDEV) Services.BaseService.GetObject("SelectPSPDEVByName", psp);
                    if (psp!=null)
                    Vjb = psp.ReferenceVolt.ToString();
                }
            }
        }
        public string FirstType
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
        public string LastType
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
        public string FirstSwitchState
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
        public string LastSwitchState
        {
            get
            {
                return comboBoxEdit6.Text;
            }
            set
            {
                comboBoxEdit6.Text = value;
            }
        }
        public string K
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
        public string PositiveR
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
        public string PositiveTQ
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
        public string ZeroR
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
        public string ZeroTQ
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
        public string NeutralNodeTQ
        {
            get
            {
                return textBox7.Text;
            }
            set
            {
                textBox7.Text = value;
            }
        }
        public string NeutralNodeR
        {
            get
            {
                return textBox8.Text;
            }
            set
            {
                textBox8.Text = value;
            }
        }
        public string Pij
        {
            get
            {
                return textBox9.Text;
            }
            set
            {
                textBox9.Text = value;
            }
        }
        public string Vij
        {
            get
            {
                return textBox10.Text;
            }
            set
            {
                textBox10.Text = value;
            }
        }
        public string Vi0
        {
            get
            {
                return textBox17.Text;
            }
            set
            {
                textBox17.Text = value;
            }
        }
        public string Vipos
        {
            get
            {
                return textBox11.Text;
            }
            set
            {
                textBox11.Text = value;
            }
        }
        public string Vistep
        {
            get
            {
                return textBox12.Text;
            }
            set
            {
                textBox12.Text = value;
            }
        }
        public string Vimax
        {
            get
            {
                return textBox13.Text;
            }
            set
            {
                textBox13.Text = value;
            }
        }
        public string Vimin
        {
            get
            {
                return textBox14.Text;
            }
            set
            {
                textBox14.Text = value;
            }
        }
        public string P0
        {
            get
            {
                return textBox15.Text;
            }
            set
            {
                textBox15.Text = value;
            }
        }
        public string I0
        {
            get
            {
                return textBox16.Text;
            }
            set
            {
                textBox16.Text = value;
            }
        }
        public string SiN
        {
            get
            {
                return textBox23.Text;
            }
            set
            {
                textBox23.Text = value;
            }
        }
        public string Vj0
        {
            get
            {
                return textBox18.Text;
            }
            set
            {
                textBox18.Text = value;
            }
        }
        public string Vjpos
        {
            get
            {
                return textBox22.Text;
            }
            set
            {
                textBox22.Text = value;
            }
        }
        public string Vjstep
        {
            get
            {
                return textBox21.Text;
            }
            set
            {
                textBox21.Text = value;
            }
        }
        public string Vjmax
        {
            get
            {
                return textBox20.Text;
            }
            set
            {
                textBox20.Text = value;
            }
        }
        public string Vjmin
        {
            get
            {
                return textBox19.Text;
            }
            set
            {
                textBox19.Text = value;
            }
        }
        public string Vjb
        {
            get
            {
                return _vjb;
            }
            set
            {
                _vjb = value;
            }
        }
        public string Vib
        {
            get
            {
                return _vib;
            }
            set
            {
                _vib = value;
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

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = this.textBox7.Text;
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

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = this.textBox8.Text;
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

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (PositiveR==""||PositiveR==null)
                {
                    PositiveR = (Convert.ToDouble(Pij) * Convert.ToDouble(Vi0) * Convert.ToDouble(Vi0) * 100 / (1000 * Convert.ToDouble(SiN) * Convert.ToDouble(SiN) * Convert.ToDouble(Vib) * Convert.ToDouble(Vib))).ToString();
                }
                if (PositiveTQ==""||PositiveR==null)
                {
                    PositiveTQ = (Convert.ToDouble(Vij) * Convert.ToDouble(Vi0) * Convert.ToDouble(Vi0) * 100 / (100*100 * Convert.ToDouble(SiN) * Convert.ToDouble(Vib) * Convert.ToDouble(Vib))).ToString();
                }
                if (ZeroR==""||ZeroR==null)
                {
                    ZeroR = (Convert.ToDouble(P0) * Convert.ToDouble(Vib) * Convert.ToDouble(Vib) * Convert.ToDouble(SiN) / (1000 * Convert.ToDouble(SiN) * 100 * Convert.ToDouble(Vi0) * Convert.ToDouble(Vi0))).ToString();
                }
                if (ZeroTQ==""||ZeroTQ==null)
                {
                    ZeroTQ = (Convert.ToDouble(I0) * Convert.ToDouble(Vib) * Convert.ToDouble(Vib) * Convert.ToDouble(SiN) / (100*100 * 100 * Convert.ToDouble(Vi0) * Convert.ToDouble(Vi0))).ToString();
                }
               
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("请填写相应抽头信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }          
        }
    }
}