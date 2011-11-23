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
using ItopVector.Tools;
using Itop.Client.Base;
namespace Itop.TLPsp
{
    public partial class frmLine : FormBase
    {
        PSPDEV leel = new PSPDEV();
        private string _referencevolt;
        public frmLine()
        {
            InitializeComponent();
            WireCategory wirewire = new WireCategory();
            IList list1 = Services.BaseService.GetList("SelectWireCategoryList", wirewire);
            foreach (WireCategory sub in list1)
            {
                comboBox22.Properties.Items.Add(sub.WireType);
            }
            mc.Text = null;
            mc.Select(); 
        }
        public frmLine(PSPDEV pspDev)

        {
            InitializeComponent();
            mc.Select();
            WireCategory wirewire = new WireCategory();
            wirewire.WireLevel = pspDev.VoltR.ToString();
            IList list1 = Services.BaseService.GetList("SelectWireCategoryListBYWireLevel", wirewire);
            foreach (WireCategory sub in list1)
            {
                comboBox22.Properties.Items.Add(sub.WireType);
            }
            InitData(pspDev);
            this.comboBox22.SelectedIndexChanged += new System.EventHandler(this.comboBox22_SelectedIndexChanged);
            this.comboBoxEdit5.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit5_SelectedIndexChanged);
            
            this.textBox11.Leave += new System.EventHandler(this.textBox11_Leave);
            this.textBox11.TextChanged += new System.EventHandler(this.textBox11_TextChanged);
        }
        public void InitData(PSPDEV pspDev)
        {
            PSPDEV dev = new PSPDEV();
            dev.Number = pspDev.FirstNode;
            dev.SvgUID = pspDev.SvgUID;
            leel.EleID = pspDev.EleID;
            leel.SvgUID = pspDev.SvgUID;
            leel.Name = pspDev.Name;
            leel.LineType = pspDev.LineType;
            leel.LineLength = pspDev.LineLength;
            leel.FirstNode = pspDev.FirstNode;
            leel.LastNode = pspDev.LastNode;
            leel.LineStatus = pspDev.LineStatus;
            leel.VoltR = pspDev.VoltR;
            dev.Type = "Use";
            dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByNumberAndSvgUIDAndType", dev);
            if (dev != null)
            {
                if (dev.Name != null)
                {
                    textBox5.Text = dev.Name;
                }

                dev.Number = pspDev.LastNode;//末节点

                dev.SvgUID = pspDev.SvgUID;
                dev.Type = "Use";

                dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByNumberAndSvgUIDAndType", dev);
                if (dev.Name != null)
                {
                    textBox6.Text = dev.Name;
                }
                textBox6.Text = dev.Name;
            }
            textBox11.Text = pspDev.LineLength.ToString();
            comboBox22.Text = pspDev.LineType;
            comboBox33.Text = pspDev.LineLevel;
            //if (comboBox33.Text=="")
            //comboBox33.Text="单回路";
            comboBox44.Text = pspDev.LineStatus;
            //if (comboBox44.Text == "")
            //    comboBox44.Text = "运行";
            if(pspDev.Name!=null)
            {
                mc.Text = pspDev.Name;
                leel.Name = pspDev.Name;
            }
            textBox2.Text = pspDev.PositiveR.ToString();
            textBox3.Text = pspDev.PositiveTQ.ToString();
            textBox4.Text = pspDev.ZeroR.ToString();
            
            textBox7.Text = pspDev.ZeroTQ.ToString();
            textBox8.Text = pspDev.HuganTQ1.ToString();
            textBox9.Text = pspDev.HuganTQ2.ToString();
            textBox10.Text = pspDev.HuganTQ3.ToString();
            textBox12.Text = pspDev.HuganTQ4.ToString();
            textBox13.Text = pspDev.HuganTQ5.ToString();
            comboBoxEdit1.Text = pspDev.HuganLine1;
            comboBoxEdit2.Text = pspDev.HuganLine2;
            comboBoxEdit3.Text = pspDev.HuganLine3;
            comboBoxEdit4.Text = pspDev.HuganLine4;
            PositiveES = pspDev.SmallTQ.ToString();
            ZeroES = pspDev.BigTQ.ToString();
            //ISwitchStatus = pspDev.KName;
            //JSwitchStatus = pspDev.KSwitchStatus;
            comboBoxEdit6.Text = pspDev.KName;
            comboBoxEdit7.Text = pspDev.KSwitchStatus;
            //comboBox5.Text = pspDev.HuganFirst.ToString();
            ReferenceVolt = pspDev.ReferenceVolt.ToString();
            if (pspDev.Name != null)
            {
                textBox14.Text = pspDev.Name;            
            }
            comboBoxEdit5.Text = pspDev.VoltR.ToString();

            if (pspDev.HuganFirst == 1)
            {
                comboBox5.Text = "是";
            }
            else
            {
                comboBox5.Text = "否";
            }
            //textBox33.Text = pspDev.LineR.ToString();
            //textBox2.Text = pspDev.LineTQ.ToString();
            //textBox3.Text = pspDev.LineGNDC.ToString();
            //textBox4.Text = pspDev.LineChange.ToString();
            //if (textBox4.Text == "0")
            //{
            //    textBox4.Text = "1";
            //}
            //if (pspDev.Flag == 0)
            //{
            //    comboBox1.Text = "否";
            //}
            //else
            //{
            //    comboBox1.Text = "是";
            //}
        }
        public string LineLength
        {
            get
            {
                if (textBox11.Text == "")
                    textBox11.Text = "0";
                return textBox11.Text;
            }
            set
            {
                textBox11.Text = value;
            }
        }
        public string ISwitchStatus
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
        public string JSwitchStatus
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
        public string ReferenceVolt
        {
            get
            {
                return _referencevolt;
            }
            set
            {
                _referencevolt = value;
            }
        }
        public string PositiveES
        {
            get
            {
                if (textBox15.Text == "")
                    textBox15.Text = "0";
                return textBox15.Text;
            }
            set
            {
                textBox15.Text = value;
            }
        }
        public string ZeroES
        {
            get
            {
                if (textBox16.Text == "")
                    textBox16.Text = "0";
                return textBox16.Text;
            }
            set
            {
                textBox16.Text = value;
            }
        }
        public string LineLev //电压等级
        {
            get
            {
                if (comboBoxEdit5.Text == "")
                    comboBoxEdit5.Text = "0";
                return comboBoxEdit5.Text;
            }
            set
            {
                comboBoxEdit5.Text = value;
            }
        }
        public string LineType
        {
            get
            {

                return comboBox22.Text;
            }
            set
            {
                comboBox22.Text = value;
            }
        }
        public string LineLevel
        {
            get
            {
                if (comboBoxEdit5.Text == "")
                    comboBoxEdit5.Text = "0";
                return comboBoxEdit5.Text;
            }
            set
            {
                comboBoxEdit5.Text = value;
            }
        }
        public string LineStatus
        {
            get
            {
                return comboBox44.Text;
            }
            set
            {
                comboBox44.Text = value;
            }
        }
        public string Name
        {
            get
            {
                return mc.Text;
            }
            set
            {
                mc.Text = value;
            }
        }
        public string PositiveR
        {
            get
            {
                if (textBox2.Text == "")
                    textBox2.Text = "0";
                return textBox2.Text;
            }
            set
            {
                textBox2.Text = value;
            }
        }
        public string PositiveTQ
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
        public string ZeroR
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
        public string ZeroTQ
        {
            get
            {
                if (textBox7.Text == "")
                    textBox7.Text = "0";
                return textBox7.Text;
            }
            set
            {
                textBox7.Text = value;
            }
        }
        public string HuganTQ1
        {
            get
            {
                if (textBox8.Text == "")
                    textBox8.Text = "0";
                return textBox8.Text;
            }
            set
            {
                textBox8.Text = value;
            }
        }
        public string HuganTQ2
        {
            get
            {
                if (textBox9.Text == "")
                    textBox9.Text = "0";
                return textBox9.Text;
            }
            set
            {
                textBox9.Text = value;
            }
        }
        public string HuganTQ3
        {
            get
            {
                if (textBox10.Text == "")
                    textBox10.Text = "0";
                return textBox10.Text;
            }
            set
            {
                textBox10.Text = value;
            }
        }
        public string HuganTQ4
        {
            get
            {
                if (textBox12.Text == "")
                    textBox12.Text = "0";
                return textBox12.Text;
            }
            set
            {
                textBox12.Text = value;
            }
        }
        public string HuganTQ5
        {
            get
            {
                if (textBox13.Text == "")
                    textBox13.Text = "0";
                return textBox13.Text;
            }
            set
            {
                textBox13.Text = value;
            }
        }
        public string HuganLine1
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
        public string HuganLine2
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
        public string HuganLine3
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
        public string HuganLine4
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
        public string HuganFirst
        {
            get
            {
                return comboBox5.Text;
            }
            set
            {
                comboBox5.Text = value;
            }
        }

        private void frmProperty_Load(object sender, EventArgs e)
        {

            
            PSPDEV dev = new PSPDEV();
            //dev.Number = pspDev.FirstNode;
            dev.SvgUID = leel.SvgUID;
            dev.Type = "Polyline";
            IList list = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", dev);
            foreach (PSPDEV sub in list)
            {
                if (sub.Name != leel.Name && sub.Lable=="支路")
                {
                    comboBoxEdit1.Properties.Items.Add(sub.Name);
                    comboBoxEdit2.Properties.Items.Add(sub.Name);
                    comboBoxEdit3.Properties.Items.Add(sub.Name);
                    comboBoxEdit4.Properties.Items.Add(sub.Name);
                }
            }
            comboBoxEdit1.Properties.Items.Add(" ");
            comboBoxEdit2.Properties.Items.Add(" ");
            comboBoxEdit3.Properties.Items.Add(" ");
            comboBoxEdit4.Properties.Items.Add(" ");
        }

        private void comboBoxEdit4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_MouseDown(object sender, MouseEventArgs e)
        {
            if (mc.Text == "")
            {
                MessageBox.Show("名称不能为空!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                PSPDEV pspName = new PSPDEV();
                pspName.Name = mc.Text;
                pspName.SvgUID = leel.SvgUID;
                pspName.Type = "Polyline";
                IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                if (listName.Count > 1||(listName.Count==1&&((PSPDEV)listName[0]).EleID!=leel.EleID))
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
        private void comboBox22_SelectedIndexChanged(object sender, EventArgs e)
        {
            leel.LineLength = Convert.ToDouble(textBox11.Text);
            leel.LineType = comboBox22.Text;
            leel.Name = mc.Text;
            leel.LineStatus = comboBox44.Text;
            leel.LineLevel = "单回路";

            WireCategory wirewire = new WireCategory();
            wirewire.WireType = comboBox22.Text;
            WireCategory wirewire2 = new WireCategory();
            wirewire.WireLevel = comboBoxEdit5.Text;
            wirewire2.WireType = comboBox22.Text;
            wirewire2 = (WireCategory)Services.BaseService.GetObject("SelectWireCategoryByKeyANDWireLevel", wirewire);

            if (wirewire2 != null)
            {
                leel.LineR = Convert.ToDouble(leel.LineLength) * wirewire2.WireR;

                leel.LineTQ = Convert.ToDouble(leel.LineLength) * wirewire2.WireTQ;

                leel.LineGNDC = Convert.ToDouble(leel.LineLength) * wirewire2.WireGNDC;

                leel.PositiveTQ = Convert.ToDouble(leel.LineLength) * wirewire2.WireTQ;
                leel.ZeroTQ = leel.PositiveTQ * 3;
            }
            string tempp = comboBoxEdit5.Text;
            int tel = tempp.Length;
            //tempp = tempp.Substring(0, tel - 2);
            //pspDev.VoltR = Convert.ToDouble(tempp);
            leel.VoltR = Convert.ToDouble(tempp);
            //textBox2.Text=

            InitData(leel);

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }
        private void comboBoxEdit5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit5.Text == "")
                comboBoxEdit5.Text = "0";

            if (comboBoxEdit5.Text == leel.VoltR.ToString())
            { }
            else
            {
                WireCategory wirewire = new WireCategory();
                wirewire.WireLevel = comboBoxEdit5.Text;
                IList list1 = Services.BaseService.GetList("SelectWireCategoryListBYWireLevel", wirewire);
                comboBox22.Properties.Items.Clear();
                foreach (WireCategory sub in list1)
                {
                    comboBox22.Properties.Items.Add(sub.WireType);
                }
                //if (list1.Count == 0)
                comboBox22.Text = "";

                leel.LineLength = Convert.ToDouble(textBox11.Text);
                leel.LineType = comboBox22.Text;
                leel.Name = mc.Text;
                leel.LineStatus = comboBox44.Text;
                leel.LineLevel = "单回路";

                string tempp = comboBoxEdit5.Text;
                int tel = tempp.Length;
               // tempp = tempp.Substring(0, tel - 2);
                //pspDev.VoltR = Convert.ToDouble(tempp);
                leel.VoltR = Convert.ToDouble(tempp);

                //    WireCategory wirewire = new WireCategory();
                //    wirewire.WireType = comboBox22.Text;
                //    WireCategory wirewire2 = new WireCategory();
                //    wirewire2 = (WireCategory)Services.BaseService.GetObject("SelectWireCategoryByKey", wirewire);
                //    if (wirewire2 != null)
                //    {
                //        leel.LineR = Convert.ToDouble(leel.LineLength) * wirewire2.WireR;

                //        leel.LineTQ = Convert.ToDouble(leel.LineLength) * wirewire2.WireTQ;

                //        leel.LineGNDC = Convert.ToDouble(leel.LineLength) * wirewire2.WireGNDC;
                //    }
                //    //textBox2.Text=

                InitData(leel);

            }
        }

        private void textBox11_Leave(object sender, EventArgs e)
        {
            if (textBox11.Text == "")
                textBox11.Text = "0";
            //MessageBox.Show("hello");
            if (textBox11.Text == leel.LineLength.ToString())
            { }
            else
            {
                leel.LineLength = Convert.ToDouble(textBox11.Text);
                leel.LineType = comboBox22.Text;
                leel.Name = mc.Text;
                leel.LineStatus = comboBox44.Text;
                leel.LineLevel = "单回路";


                WireCategory wirewire = new WireCategory();
                wirewire.WireType = comboBox22.Text;
                WireCategory wirewire2 = new WireCategory();
                wirewire.WireLevel = comboBoxEdit5.Text;
                wirewire2 = (WireCategory)Services.BaseService.GetObject("SelectWireCategoryByKeyANDWireLevel", wirewire);
                if (wirewire2 != null)
                {
                    leel.LineR = Convert.ToDouble(leel.LineLength) * wirewire2.WireR;

                    leel.LineTQ = Convert.ToDouble(leel.LineLength) * wirewire2.WireTQ;

                    leel.LineGNDC = Convert.ToDouble(leel.LineLength) * wirewire2.WireGNDC;
                    leel.PositiveTQ = Convert.ToDouble(leel.LineLength) * wirewire2.WireTQ;
                    leel.ZeroTQ = leel.PositiveTQ * 3;
                }
                //textBox2.Text=

                string tempp = comboBoxEdit5.Text;
                int tel = tempp.Length;
                //tempp = tempp.Substring(0, tel - 2);
                //pspDev.VoltR = Convert.ToDouble(tempp);
                leel.VoltR = Convert.ToDouble(tempp);

                InitData(leel);

            }
        }



        private void mc_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string kv = comboBoxEdit5.Text.Replace("KV", "");
           
            frmSelectLineByYear frm = new frmSelectLineByYear();
            frm.Power = kv;
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            frm.ShowDialog();
            if(frm.line!=null){
                mc.Text = frm.line.LineName;
                textBox11.Text = frm.line.Length.ToString();
                comboBoxEdit5.Text = frm.line.Voltage.ToString()+"KV";
                comboBox22.Text = frm.line.LineType;
                comboBox44.Text = frm.line.ObligateField1;

            }
        }

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = this.textBox15.Text;
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

        private void textBox16_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = this.textBox16.Text;
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

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

    }
}