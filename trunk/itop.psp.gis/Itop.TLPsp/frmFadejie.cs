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
    public partial class frmFadejie : FormBase
    {
        private string svgUID;
        private string EleID;
        public frmFadejie(string svgDocumentUID)
        {
            SvgUID = svgDocumentUID;
            InitializeComponent();
            textBox1.Text = null;
            textBox1.Select();
            PSPDEV psp = new PSPDEV();
            psp.SvgUID = SvgUID;
            psp.Lable = "母线节点";
            psp.Type = "use";
            IList list = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", psp);
            foreach (PSPDEV dev in list)
            {
                //comboBoxEdit1.Properties.Items.Add(dev.Name);
                comboBoxEdit2.Properties.Items.Add(dev.Name);
            }
        }
        public frmFadejie(PSPDEV pspDev, string svgDocumentUID)

        {
            SvgUID = svgDocumentUID;
            
            InitializeComponent();
            PSPDEV psp = new PSPDEV();
            psp.SvgUID = SvgUID;
            psp.Lable = "母线节点";
            psp.Type = "use";
            IList list = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", psp);
            foreach (PSPDEV dev in list)
            {
                //comboBoxEdit1.Properties.Items.Add(dev.Name);
                comboBoxEdit2.Properties.Items.Add(dev.Name);
            }
            textBox1.Select(); 
            InitData(pspDev);
            
        }
        public void InitData(PSPDEV pspDev)
        {
            svgUID = pspDev.SvgUID;
            EleID = pspDev.EleID;
            textBox1.Text = "";
            if (pspDev.Name != null)
            {
                textBox1.Text = pspDev.Name;
            }
            //PSPDEV dev = new PSPDEV();
            //dev.HuganLine1 = pspDev.HuganLine1;//首节点
            //dev.SvgUID = pspDev.SvgUID;
            //dev.Type = "gndline";
            //dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByNumberAndSvgUIDAndType", dev);
            //if (dev.Name != null)
            //{
            //    textBox2.Text = dev.Name;
            //}
   
                comboBoxEdit1.Text = pspDev.HuganLine3;
                comboBoxEdit2.Text = pspDev.HuganLine1;
   
            //dev.Number = pspDev.LastNode;
            //dev.SvgUID = pspDev.SvgUID;
            //dev.Type = "Use";
            //dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByNumberAndSvgUIDAndType", dev);
            //textBox3.Text = dev.Name;
            textBox4.Text = pspDev.OutP.ToString();//大方式电抗
            textBox5.Text = pspDev.OutQ.ToString();//小方式电抗
            comboBoxEdit5.Text = pspDev.VoltR.ToString();//电压等级
            textBox8.Text = pspDev.VoltV.ToString();
            textBox9.Text = pspDev.PositiveTQ.ToString();
            textBox10.Text = pspDev.ZeroTQ.ToString();
            VoltR = pspDev.VoltR.ToString();
            SiN = pspDev.SiN.ToString();
           // ReferenceVolt = pspDev.ReferenceVolt.ToString();
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
        //public string ReferenceVolt
        //{
        //    get
        //    {
        //        return comboBoxEdit8.Text;
        //    }
        //    set
        //    {
        //        comboBoxEdit8.Text = value;
        //    }
        //}
        public string OutP//大方式电抗
        {
            get
            {
                if (textBox4.Text == "")
                    textBox4.Text = "0";
                return textBox4.Text;
            }
            set
            {
                textBox4.Text = value;
            }
        }
        public string SiN//大方式电抗
        {
            get
            {
                if (textBox3.Text == "")
                    textBox3.Text = "0";
                return textBox3.Text;
            }
            set
            {
                textBox3.Text = value;
            }
        }
        public string OutQ//小方式电抗
        {
            get
            {
                if (textBox5.Text == "")
                    textBox5.Text = "0";
                return textBox5.Text;
            }
            set
            {
                textBox5.Text = value;
            }
        }
        //public string VoltR//电压等级
        //{
        //    get
        //    {
        //        return textBox7.Text;
        //    }
        //    set
        //    {
        //        textBox7.Text = value;
        //    }
        //}
        public string VoltV//电压相角
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
        public string FirstNodeName
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
        public string SwitchStatus
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
        public string PositiveTQ//正序电抗
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
        public string NegativeTQ//电压相角
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
        public string Name//支路路名称
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        public string SvgUID
        {
            get 
            {
                return svgUID;
            }
            set 
            {
                svgUID = value;
            }
        }
        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("名称不能为空!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                PSPDEV pspName = new PSPDEV();
                pspName.Name = textBox1.Text;
                pspName.SvgUID = svgUID;
                pspName.Type = "gndline";
                IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                if (listName.Count > 1||(listName.Count==1&&((PSPDEV)listName[0]).EleID!=EleID))
                {
                    MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    button1.DialogResult = DialogResult.OK;
                }
            }
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
    }
}