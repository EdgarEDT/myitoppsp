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
    public partial class frmThridTra : FormBase
    {
        private string UID;
        private string _vib;
        private string _vjb;
        private string _vkb;
        public frmThridTra(string svgDocumentUID)
        {
            svgUID = svgDocumentUID;
            InitializeComponent();
            InitCom();
        }
        public frmThridTra(PSPDEV dev, string svgDocumentUID)
        {
            svgUID = svgDocumentUID;
            InitializeComponent();
            InitCom();
            InitData(dev);
        }
        private void InitCom()
        {
            PSPDEV psp = new PSPDEV();
            psp.SvgUID = UID;
            psp.Lable = "母线节点";
            psp.Type = "Use";
            IList list = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType",psp);
            foreach (PSPDEV dev in list)
            {
                comboBoxEdit1.Properties.Items.Add(dev.Name);
                comboBoxEdit6.Properties.Items.Add(dev.Name);
                comboBoxEdit9.Properties.Items.Add(dev.Name);
            }   
        }
        private void InitData(PSPDEV dev)
        {
            if (dev!=null)
            {
                Name = dev.Name;
                ZeroTQ = dev.ZeroTQ.ToString();
                NeutralNodeTQ = dev.BigTQ.ToString();
                IName= dev.HuganLine1;
                JName= dev.HuganLine2;
                KName = dev.KName;
                comboBoxEdit2.Text = dev.LineLevel;
                comboBoxEdit5.Text = dev.LineType;
                comboBoxEdit8.Text = dev.LineStatus;
                comboBoxEdit3.Text = dev.HuganLine3;
                comboBoxEdit4.Text = dev.HuganLine4;
                comboBoxEdit7.Text = dev.KSwitchStatus;
                IK = dev.K.ToString();
                JK = dev.G.ToString();
                KK = dev.BigP.ToString();
                textBox4.Text = dev.HuganTQ1.ToString();
                JR = dev.HuganTQ2.ToString();
                KR = dev.HuganTQ3.ToString();
                textBox6.Text = dev.HuganTQ4.ToString();
                JTQ = dev.HuganTQ5.ToString();
                KTQ = dev.SmallTQ.ToString();
                P0 = dev.P0.ToString();
                I0 = dev.I0.ToString();
                SiN = dev.SiN.ToString();
                SjN = dev.SjN.ToString();
                SkN = dev.SkN.ToString();
                Vi0 = dev.Vi0.ToString();
                Vj0 = dev.Vj0.ToString();
                Vk0 = dev.Vk0.ToString();
                Pij = dev.Pij.ToString();
                Pjk = dev.Pjk.ToString();
                Pik = dev.Pik.ToString();
                Vij = dev.Vij.ToString();
                Vjk = dev.Vjk.ToString();
                Vik = dev.Vik.ToString();
                Vipos = dev.Vipos.ToString();
                Vjpos = dev.Vjpos.ToString();
                Vkpos = dev.Vkpos.ToString();
                Vistep = dev.Vistep.ToString();
                Vjstep = dev.Vjstep.ToString();
                Vkstep = dev.Vkstep.ToString();
                Vimax = dev.Vimax.ToString();
                Vjmax = dev.Vjmax.ToString();
                Vkmax = dev.Vkmax.ToString();
                Vimin = dev.Vimin.ToString();
                Vjmin = dev.Vjmin.ToString();
                Vkmin = dev.Vkmin.ToString();
                //Vib = dev.Vib.ToString();
                //Vjb = dev.Vjb.ToString();
                //Vkb = dev.Vkb.ToString();
            }
        }
        public string Name
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
        public string ZeroTQ
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
        public string NeutralNodeTQ
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
        public string IName
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
                    psp = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", psp);
                    if(psp!=null)
                    Vib = psp.ReferenceVolt.ToString();
                }
            }
        }
        public string IType
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
        public string ISwitchState
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
        public string IK
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
        public string IR
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
        public string ITQ
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
        public string KName
        {
            get
            {
                return comboBoxEdit9.Text;
            }
            set
            {
                comboBoxEdit9.Text = value;
                if (value != "")
                {
                    PSPDEV psp = new PSPDEV();
                    psp.Name = value;
                    psp.Type = "Use";
                    psp.SvgUID = svgUID;
                    psp = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", psp);
                    if(psp!=null)
                    Vkb = psp.ReferenceVolt.ToString();
                }
            }
        }
        public string KType
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
        public string KSwitchState
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
        public string KK
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
        public string KR
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
        public string KTQ
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

        public string JName
        {
            get
            {
                return comboBoxEdit6.Text;
            }
            set
            {
                comboBoxEdit6.Text = value;
                if (value != "")
                {
                    PSPDEV psp = new PSPDEV();
                    psp.Name = value;
                    psp.Type = "Use";
                    psp.SvgUID = svgUID;
                    psp = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", psp);
                    if(psp!=null)
                    Vjb = psp.ReferenceVolt.ToString();
                }
            }
        }
        public string JType
        {
            get
            {
                return comboBoxEdit8.Text;
            }
            set
            {
                comboBoxEdit8.Text = value;
            }
        }
        public string JSwitchState
        {
            get
            {
                return comboBoxEdit7.Text;
            }
            set
            {
                comboBoxEdit7.Text = value;
            }
        }
        public string JK
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
        public string JR
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
        public string JTQ
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
        public string P0
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
        public string I0
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
        public string SiN
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
        public string Vi0
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
        public string Pij
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
        public string Vij
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
        public string Vipos
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
        public string Vistep
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
        public string Vimax
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
        public string Vimin
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
        public string SjN
        {
            get
            {
                return textBox30.Text;
            }
            set
            {
                textBox30.Text = value;
            }
        }
        public string Vj0
        {
            get
            {
                return textBox29.Text;
            }
            set
            {
                textBox29.Text = value;
            }
        }
        public string Pjk
        {
            get
            {
                return textBox28.Text;
            }
            set
            {
                textBox28.Text = value;
            }
        }
        public string Vjk
        {
            get
            {
                return textBox27.Text;
            }
            set
            {
                textBox27.Text = value;
            }
        }
        public string Vjpos
        {
            get
            {
                return textBox26.Text;
            }
            set
            {
                textBox26.Text = value;
            }
        }
        public string Vjstep
        {
            get
            {
                return textBox25.Text;
            }
            set
            {
                textBox25.Text = value;
            }
        }
        public string Vjmax
        {
            get
            {
                return textBox24.Text;
            }
            set
            {
                textBox24.Text = value;
            }
        }
        public string Vjmin
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
        public string SkN
        {
            get
            {
                return textBox38.Text;
            }
            set
            {
                textBox38.Text = value;
            }
        }
        public string Vk0
        {
            get
            {
                return textBox37.Text;
            }
            set
            {
                textBox37.Text = value;
            }
        }
        public string Pik
        {
            get
            {
                return textBox36.Text;
            }
            set
            {
                textBox36.Text = value;
            }
        }
        public string Vik
        {
            get
            {
                return textBox35.Text;
            }
            set
            {
                textBox35.Text = value;
            }
        }
        public string Vkpos
        {
            get
            {
                return textBox34.Text;
            }
            set
            {
                textBox34.Text = value;
            }
        }
        public string Vkstep
        {
            get
            {
                return textBox33.Text;
            }
            set
            {
                textBox33.Text = value;
            }
        }
        public string Vkmax
        {
            get
            {
                return textBox32.Text;
            }
            set
            {
                textBox32.Text = value;
            }
        }
        public string Vkmin
        {
            get
            {
                return textBox31.Text;
            }
            set
            {
                textBox31.Text = value;
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
        public string Vjb
        {
            get
            {
                return _vjb;
            }
            set
            {
                _vjb= value;
            }
        }
        public string Vkb
        {
            get
            {
                return _vkb;
            }
            set
            {
                _vkb = value;
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = this.textBox1.Text;
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

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = this.textBox12.Text;
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

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = this.textBox11.Text;
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

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = this.textBox10.Text;
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

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = this.textBox9.Text;
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
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                double VPij = 0, VPjk = 0, VPik = 0, Pi = 0, Pj = 0, Pk = 0;
                VPij = Convert.ToDouble(Pij) * (Convert.ToDouble(SiN) / Convert.ToDouble(SjN)) * (Convert.ToDouble(SiN) / Convert.ToDouble(SjN));
                VPjk = Convert.ToDouble(Pjk) * (Convert.ToDouble(SiN) / Math.Min(Convert.ToDouble(SjN), Convert.ToDouble(SkN))) * (Convert.ToDouble(SiN) / Math.Min(Convert.ToDouble(SjN), Convert.ToDouble(SkN)));
                VPik = Convert.ToDouble(Pik) * (Convert.ToDouble(SiN) / Convert.ToDouble(SkN)) * (Convert.ToDouble(SiN) / Convert.ToDouble(SkN));
                Pi = (VPij + VPik - VPjk) / 2;
                Pj = (VPij + VPjk - VPik) / 2;
                Pk = (VPjk + VPik - VPij) / 2;
                double SN = Convert.ToDouble(SiN);
                double V = Convert.ToDouble(Vi0);
                if (Convert.ToDouble(Vj0) > V)
                {
                    V = Convert.ToDouble(Vj0);
                    SN = Convert.ToDouble(SjN);
                }
                if (Convert.ToDouble(Vk0) > V)
                {
                    V = Convert.ToDouble(Vk0);
                    SN = Convert.ToDouble(SkN);
                }
                IR = (Pi * 100 / (1000 * SN * SN)).ToString();
                JR = (Pj * 100 / (1000 * SN * SN)).ToString();
                KR = (Pk * 100 / (1000 * SN * SN)).ToString();
                double Vi = 0, Vj = 0, Vk = 0;
                Vi = (Convert.ToDouble(Vij) + Convert.ToDouble(Vik) - Convert.ToDouble(Vjk)) / 2;
                Vj = (Convert.ToDouble(Vij) + Convert.ToDouble(Vjk) - Convert.ToDouble(Vik)) / 2;
                Vk = (Convert.ToDouble(Vik) + Convert.ToDouble(Vjk) - Convert.ToDouble(Vij)) / 2;
                ITQ = (Vi * 100 / (100 * SN)).ToString();
                JTQ = (Vj * 100 / (100 * SN)).ToString();
                KTQ = (Vk * 100 / (100 * SN)).ToString();
                IK = ((Convert.ToDouble(Vimax) - Convert.ToDouble(Vi0) * Convert.ToDouble(Vistep) * (Convert.ToDouble(Vipos) - 1) / 100) / Convert.ToDouble(Vib)).ToString();
                JK = ((Convert.ToDouble(Vjmax) - Convert.ToDouble(Vj0) * Convert.ToDouble(Vjstep) * (Convert.ToDouble(Vjpos) - 1) / 100) / Convert.ToDouble(Vjb)).ToString();
                KK = ((Convert.ToDouble(Vkmax) - Convert.ToDouble(Vk0) * Convert.ToDouble(Vkstep) * (Convert.ToDouble(Vkpos) - 1) / 100) / Convert.ToDouble(Vkb)).ToString();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("请填写相应抽头信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }  
          
        }
    }
}