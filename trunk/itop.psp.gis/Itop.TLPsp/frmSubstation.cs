using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Collections;
using System.Xml;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Configuration;
using Itop.Client;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using ItopVector.DrawArea;
using ItopVector.Core;
using ItopVector.Core.Func;
using ItopVector.Core.Document;
using ItopVector.Core.Figure;
using ItopVector.Core.Interface.Figure;
using Itop.Domain.Stutistic;
using Itop.Client.Base;
using System.IO;
using System.Threading;
using ItopVector.Tools;
using ItopVector.Core.Interface;
using System.Xml.XPath;
using ItopVector.Core.Types;
using System.Diagnostics;

namespace Itop.TLPsp
{
    public partial class frmSubstation : FormBase
    {
        PSPDEV leel = new PSPDEV();
        int i = 0;
        private string str_year = "";

        private string str_Power = "";

        private string _Eleid = "";
        private string _tyear = "";

        public string Str_Power
        {
            get { return str_Power; }
            set { str_Power = value; }
        }
        public string Str_year
        {
            get { return str_year; }
            set { str_year = value; }
        }
        public string EleID
        {
            get{return _Eleid;}
            set { _Eleid = value; }
        }
        public string TYear
        {
            get { return tyear.Text; }
            set { _tyear = value; }
        }
        public string ReferenceVolt
        {
            get { return comboBoxEdit1.Text; }
            set { comboBoxEdit1.Text = value; }
        }
        public frmSubstation()
        {
            InitializeComponent();
            //textBox1.Text = null;
            //textBox1.Select(); 
            //textBox7_DataError += new System.FormatException(textBox7_DataError);
            //this.textBox7.TextChanged += new System.EventHandler(this.textBox7_TextChanged);
            //this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            
        }
        void textBox7_DataError(object sender, System.FormatException e)
        {
            //data
            MessageBox.Show("提示");        
        }
        public frmSubstation(PSPDEV pspDev)
        {
            InitializeComponent();
            InitData(pspDev);
            mc.Select(); 
            //B8D2F8
            //this.textBox7.TextChanged += new System.EventHandler(this.textBox7_TextChanged);
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            this.textBox7.Leave += new EventHandler(textBox7_Leave);
            object[] o = new object[30];
            for (int i = 0; i < 30; i++)
            {
                o[i] = 2009 + i;
            }
            this.tyear.Properties.Items.AddRange(o);
           
        }
        

        void textBox7_Leave(object sender, EventArgs e)
        {

            if (textBox7.Text == "")
                textBox7.Text = "0";
            if (leel.Burthen == Convert.ToDecimal(textBox7.Text))
            {
               
            }
            else
            {
                PSPDEV powerfactor = new PSPDEV();
                powerfactor.Type = "Power";
                powerfactor.SvgUID = leel.SvgUID;
                powerfactor = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDAndType", powerfactor);

                //textBox2.Text =Convert.ToString(Convert.ToDouble(textBox7.Text) * powerfactor.BigP);
                leel.Burthen = Convert.ToDecimal(textBox7.Text);
                if (textBox5.Text == "")
                    textBox5.Text = "0";
                leel.VoltR = Convert.ToDouble(textBox5.Text);
                leel.Name = mc.Text;
                leel.InPutP = Convert.ToDouble(textBox4.Text);
                leel.InPutQ = Convert.ToDouble(textBox6.Text);
                leel.OutP = Convert.ToDouble(textBox2.Text);
                leel.OutQ = Convert.ToDouble(textBox3.Text);
                leel.ReferenceVolt = Convert.ToDouble(ReferenceVolt);
                if (powerfactor != null)
                {
                    if (leel.Lable == "变电站")
                    {
                        leel.InPutP = Convert.ToDouble(textBox7.Text) * powerfactor.BigP;
                        leel.InPutQ = Convert.ToDouble(textBox7.Text) * powerfactor.BigP * Math.Tan(Math.Acos(powerfactor.PowerFactor));
                    }
                    else if (leel.Lable == "电厂")
                    {
                        leel.OutP = Convert.ToDouble(textBox7.Text) * powerfactor.BigP;
                        leel.OutQ = Convert.ToDouble(textBox7.Text) * powerfactor.BigP * Math.Tan(Math.Acos(powerfactor.PowerFactor));
                    }
                }
                InitData(leel);
            }
        }
        public void InitData(PSPDEV pspDev)
        {
            //textBox1.Text = pspDev.Name;
            if (!string.IsNullOrEmpty(pspDev.Name))
            {
                mc.Text = pspDev.Name;
                //checkBox1.Checked = (pspDev.Burthen == 0 && pspDev.Lable == "变电站");
            }
            leel.EleID = pspDev.EleID;
            leel.Lable = pspDev.Lable;
            leel.SvgUID = pspDev.SvgUID;
            leel.Name = pspDev.Name;
            leel.OutP = pspDev.OutP;
            leel.OutQ = pspDev.OutQ;
            leel.InPutP = pspDev.InPutP;
            leel.InPutQ = pspDev.InPutQ;
            leel.NodeType = pspDev.NodeType;
            leel.VoltR = pspDev.VoltR;
            leel.Burthen = pspDev.Burthen;
            leel.Lable = pspDev.Lable;
            leel.ReferenceVolt = pspDev.ReferenceVolt;
            textBox5.Text = pspDev.VoltR.ToString();//节点电压
            if (pspDev.Lable == "母线节点")
            {
                //tabControl1.
                mc.Visible = true;
                comboBox1.Visible = false;
                textBox4.Visible = false;
                textBox6.Visible = false;
                textBox3.Visible = false;
                //textBox5.Visible = false;
                textBox7.Visible = false;
                textBox2.Visible = false;
                //label5.Visible = false;
                label8.Visible = false;
                label7.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label6.Visible = false;
                this.Height = 250;
                this.Width = 300;
                label1.Top = 40;
                mc.Top = 37;
                label5.Top = 80;
                textBox5.Top = 77;
                setEnabled(true);
                label10.Visible = false;
                tyear.Visible = false;
                tabControl1.TabPages.RemoveByKey("tabPage2");
                //tabPage2.
                //la
            }
            textBox7.Text = pspDev.Burthen.ToString();//变压器容量
            if (pspDev.NodeType == "0")//节点类型，是否为平衡点
            {
                comboBox1.Text = "是";
            }
            else
            {
                comboBox1.Text = "否";
            }
            
            //发电机输出P
            textBox3.Text = pspDev.OutQ.ToString("N3");//发电机输出Q
            textBox4.Text = pspDev.InPutP.ToString();//负荷吸收P
            textBox6.Text = pspDev.InPutQ.ToString("N3");//负荷吸收Q
            ReferenceVolt = pspDev.ReferenceVolt.ToString();
           
            textBox2.Text = pspDev.OutP.ToString();
        }
        public string OutP//发电机输出P
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
        public string OutQ//发电机输出Q
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
        public string InPutP//负荷吸收P
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
        public string InPutQ//负荷吸收Q
        {
            get
            {
                if (textBox6.Text == "")
                    textBox6.Text = "0";
                return textBox6.Text;
            }
            set
            {
                textBox6.Text = value;
            }
        }
        public string VoltR//节点电压
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
       
        public string NodeType//节点类型，是否为平衡点
        {
            get
            {
                return comboBox1.Text;
            }
            set
            {
                comboBox1.Text = value;
            }
        }
        public string Burthen//变压器容量
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
        public string Name//节点名称
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
        public string Change//节点名称
        {
            get
            {
                return i.ToString();
            }
            set
            {
                
            }
        }
        public bool IsTJ {//是否T接点
            get {
                return checkBox1.Checked;
            }
            set {
                checkBox1.Checked = value;
            }
        }
 
        private void simpleButton1_MouseDown(object sender, MouseEventArgs e)
        {
            if (mc.Text == "")
            {
                MessageBox.Show("名称不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                PSPDEV pspName = new PSPDEV();
                pspName.Name = mc.Text;
                pspName.SvgUID = leel.SvgUID;
                pspName.Type = "Use";
                IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                if (listName.Count > 1 || (listName.Count == 1 && ((PSPDEV)listName[0]).EleID != leel.EleID))
                {
                    MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    simpleButton1.DialogResult = DialogResult.OK;
                }
            }
                
            //frmSubstation dlg = new frmSubstation();
            //return;
        }


        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            i = 1;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            i = 2;
            //PSPDEV powerfactor = new PSPDEV();
            //powerfactor.Type = "Power";
            //powerfactor.SvgUID = leel.SvgUID;
            //powerfactor = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDAndType", powerfactor);
                        
            ////textBox2.Text =Convert.ToString(Convert.ToDouble(textBox7.Text) * powerfactor.BigP);
            //leel.Burthen = Convert.ToDecimal(textBox7.Text);
            //leel.VoltR = Convert.ToDouble(textBox5.Text);
            //leel.Name = textBox1.Text;
            //if (leel.Lable == "变电站")
            //{
            //    leel.InPutP = Convert.ToDouble(textBox7.Text) * powerfactor.BigP;
            //    leel.InPutQ = Convert.ToDouble(textBox7.Text) * powerfactor.BigP * Math.Tan(Math.Acos(powerfactor.PowerFactor));
            //}
            //else if (leel.Lable=="电厂")
            //{
            //    leel.OutP = Convert.ToDouble(textBox7.Text) * powerfactor.BigP;
            //    leel.OutQ = Convert.ToDouble(textBox7.Text) * powerfactor.BigP * Math.Tan(Math.Acos(powerfactor.PowerFactor));
            //}
            //    InitData(leel);
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

        private void tabPage1_Leave(object sender, EventArgs e)
        {

        }

        private void tabControl1_VisibleChanged(object sender, EventArgs e)
        {
            //if (leel.Lable=="母线节点")
            //    tabPage2.
        }

        private void frmSubstation_Load(object sender, EventArgs e)
        {
            tyear.Text = _tyear;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

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

        private void textBox4_KeyPress_1(object sender, KeyPressEventArgs e)
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

        private void mc_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string kv = str_Power;

            frmSelectSubByYear frm = new frmSelectSubByYear();
            frm.Power = kv;
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            frm.Str_year = str_year;
            frm.ShowDialog();
            if (frm.substat != null)
            {
                mc.Text = frm.substat.EleName;
                //textBox11.Text = frm.line.Length.ToString();
                //comboBoxEdit5.Text = frm.line.Voltage.ToString() + "KV";
                textBox5.Text = frm.substat.ObligateField1;
                textBox7.Text = frm.substat.Number.ToString();
                EleID = frm.substat.EleID;
            }
        }
        private void setEnabled(bool b) {
            mc.Enabled = b;
            textBox7.Enabled = b;
            comboBox1.Enabled = b;
            textBox2.Enabled = b;
            textBox3.Enabled = b;
            textBox4.Enabled = b;
            //textBox5.Enabled = b;
            textBox6.Enabled = b;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            if (checkBox1.Checked) {
                try {
                    if (mc.Text == "" || mc.Text.Substring(0, 2) != "T_")
                        mc.Text = "T_" + Guid.NewGuid().ToString().Substring(0, 8);
                } catch { }
                textBox7.Text = "0";
                comboBox1.Text = "否";
                textBox2.Text = "0";
                textBox3.Text = "0";
                textBox4.Text = "0";
                textBox6.Text = "0";
                setEnabled(false);
            } else {
                setEnabled(true);
            }
        }

        private void mc_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == (char)9 || e.KeyChar == (char)32)   //禁止有空格键操作
            {
                e.Handled = true;
            }
        }
    }
}