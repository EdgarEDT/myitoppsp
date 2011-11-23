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
    public partial class frmLinenew : FormBase
    {
        PSPDEV leel = new PSPDEV();
        private string _tyear = "";
        public bool derefucelineflag=false;
        public string TYear
        {
            get { return tyear.Text; }
            set { _tyear = value; }
        }
        public string linevalue
        {
            get { return textEdit1.Text; }
            set{textEdit1.Text=value;}
        }
        public frmLinenew()
        {
            InitializeComponent();
            WireCategory wirewire = new WireCategory();
            IList list1 = Services.BaseService.GetList("SelectWireCategoryList", wirewire);
            foreach (WireCategory sub in list1)
            {
                comboBox22.Properties.Items.Add(sub.WireType);
                
                
            }
            //textBox1.Text = null;
            //textBox1.Select(); 
        }
        public frmLinenew(PSPDEV pspDev)
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
            this.textBox11.Leave += new System.EventHandler(this.textBox11_Leave);
            this.textBox11.TextChanged += new System.EventHandler(this.textBox11_TextChanged);
            this.comboBoxEdit1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit1_SelectedIndexChanged);
            this.comboBoxEdit1.Leave += new System.EventHandler(this.comboBoxEdit1_Leave);
            object[] o = new object[30];
           
            for (int i = 0; i < 30; i++)
            {
                o[i] = 2009 + i;
            }
            this.tyear.Properties.Items.AddRange(o);
            //this.tyear.Visible = false;
        }
        public void InitData(PSPDEV pspDev)
        {
            PSPDEV dev = new PSPDEV();
            dev.Number = pspDev.FirstNode;//�׽ڵ�
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

                dev.Number = pspDev.LastNode;//ĩ�ڵ�

                dev.SvgUID = pspDev.SvgUID;
                dev.Type = "Use";

                dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByNumberAndSvgUIDAndType", dev);
                if (dev.Name != null)
                {
                    textBox6.Text = dev.Name;
                }
                textBox6.Text = dev.Name;
            }
            textBox11.Text = pspDev.LineLength.ToString();//��·����
            comboBox22.Text = pspDev.LineType;//��·�ͺ�
            comboBox33.Text = pspDev.LineLevel;
            //if (comboBox33.Text == "")//����·˫��·
            //    comboBox33.Text = "����·";
            comboBox44.Text = pspDev.LineStatus;
            if (comboBox44.Text == "")//��·״̬
                comboBox44.Text = "����";
            if (pspDev.Name != null)
            {
                mc.Text = pspDev.Name;
                leel.Name = pspDev.Name;
            }

            comboBoxEdit1.Text = pspDev.VoltR.ToString();
            comboBoxEdit8.Text = pspDev.ReferenceVolt.ToString();
                
            textBox2.Text = pspDev.LineR.ToString(); //֧·����
            textBox3.Text = pspDev.LineTQ.ToString(); //֧·�翹
            textBox4.Text = pspDev.LineGNDC.ToString(); //֧·����
          
          
        }
        public string LineLength//��·����
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
        public string LineType//�����ͺ�
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
        public string LineLevel//����·����˫��·
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
        public string LineStatus//��·״̬�����л��߶Ͽ�
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
        public string Name//��·����
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
        public string LineR//����
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
        public string LineTQ//�翹
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
        public string LineGNDC//����
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
        public string LineLev //��ѹ�ȼ�
        {
            get
            {
                if (comboBoxEdit1.Text == "")
                    comboBoxEdit1.Text = "0";
                return comboBoxEdit1.Text;
            }
            set
            {
                comboBoxEdit1.Text = value;
            }
        }
        public string ReferenceVolt //��ѹ�ȼ�
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
        private void simpleButton1_MouseDown(object sender, MouseEventArgs e)
        {
            if (mc.Text == "")
            {
                MessageBox.Show("���Ʋ���Ϊ��!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                PSPDEV pspName = new PSPDEV();
                pspName.Name = mc.Text;
                pspName.SvgUID = leel.SvgUID;
                pspName.Type = "Polyline";
                IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                if (listName.Count > 1 || (listName.Count == 1 && ((PSPDEV)listName[0]).EleID != leel.EleID))
                {
                    MessageBox.Show("�����Ѵ��ڣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    simpleButton1.DialogResult = DialogResult.OK;
                }
            }                
        }

        private void comboBox22_SelectedIndexChanged(object sender, EventArgs e)
        {
            leel.LineLength = Convert.ToDouble(textBox11.Text);
            leel.LineType = comboBox22.Text;
            leel.Name = mc.Text;
            leel.LineStatus = comboBox44.Text;
            leel.LineLevel = "����·";

            WireCategory wirewire = new WireCategory();
            wirewire.WireType = comboBox22.Text;
            WireCategory wirewire2 = new WireCategory();
            wirewire.WireLevel = comboBoxEdit1.Text;
            wirewire2.WireType = comboBox22.Text;
            wirewire2 = (WireCategory)Services.BaseService.GetObject("SelectWireCategoryByKeyANDWireLevel", wirewire);

            if (wirewire2 != null)
            {
                leel.LineR = Convert.ToDouble(leel.LineLength) * wirewire2.WireR;

                leel.LineTQ = Convert.ToDouble(leel.LineLength) * wirewire2.WireTQ;

                leel.LineGNDC = Convert.ToDouble(leel.LineLength) * wirewire2.WireGNDC;
            }
            string tempp = comboBoxEdit1.Text;
            int tel = tempp.Length;
            if (tempp.Contains("kV") || tempp.Contains("KV") || tempp.Contains("kv") || tempp.Contains("Kv"))
            {
                tempp = tempp.Substring(0, tel - 2);
            }            
            //pspDev.VoltR = Convert.ToDouble(tempp);
            leel.VoltR = Convert.ToDouble(tempp);
            if (comboBoxEdit8.Text == "" || comboBoxEdit8.Text == null)
            {
                comboBoxEdit8.Text = "0";
            }
            leel.ReferenceVolt = Convert.ToDouble(comboBoxEdit8.Text);
            //textBox2.Text=

            InitData(leel);

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

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
                leel.LineLevel = "����·";


                WireCategory wirewire = new WireCategory();
                wirewire.WireType = comboBox22.Text;
                WireCategory wirewire2 = new WireCategory();
                wirewire.WireLevel = comboBoxEdit1.Text;
                wirewire2 = (WireCategory)Services.BaseService.GetObject("SelectWireCategoryByKeyANDWireLevel", wirewire);
                if (wirewire2 != null)
                {
                    leel.LineR = Convert.ToDouble(leel.LineLength) * wirewire2.WireR;

                    leel.LineTQ = Convert.ToDouble(leel.LineLength) * wirewire2.WireTQ;

                    leel.LineGNDC = Convert.ToDouble(leel.LineLength) * wirewire2.WireGNDC;
                }
                string tempp = comboBoxEdit1.Text;
                int tel = tempp.Length;
                //tempp = tempp.Substring(0, tel - 2);
                //pspDev.VoltR = Convert.ToDouble(tempp);
                leel.VoltR = Convert.ToDouble(tempp);
                //textBox2.Text=

                InitData(leel);
                
            }
        }
        private void comboBoxEdit1_Leave(object sender, EventArgs e)
        {
            //if (comboBoxEdit1.Text == "")
            //    comboBoxEdit1.Text = "0";

            //if (comboBoxEdit1.Text == leel.VoltR.ToString())
            //{ }
            //else
            //{
            //    WireCategory wirewire = new WireCategory();
            //    wirewire.WireLevel = comboBoxEdit1.Text;
            //    IList list1 = Services.BaseService.GetList("SelectWireCategoryListBYWireLevel", wirewire);
            //    comboBox22.Properties.Items.Clear();
            //    foreach (WireCategory sub in list1)
            //    {
            //        comboBox22.Properties.Items.Add(sub.WireType);
            //    }
            //    if (list1.Count == 0)
            //        comboBox22.Text = "";
            //    leel.LineLength = Convert.ToDouble(textBox11.Text);
            //    leel.LineType = comboBox22.Text;
            //    leel.Name = textBox1.Text;
            //    leel.LineStatus = comboBox44.Text;
            //    leel.LineLevel = "����·";


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

            //    InitData(leel);

            //}
        }
        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit1.Text == "")
                comboBoxEdit1.Text = "0";

            if (comboBoxEdit1.Text == leel.VoltR.ToString())
            { }
            else
            {
                WireCategory wirewire = new WireCategory();
                wirewire.WireLevel = comboBoxEdit1.Text;
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
                leel.LineLevel = "����·";

                string tempp = comboBoxEdit1.Text.Replace("KV", ""); ;
                int tel = tempp.Length;
                //tempp = tempp.Substring(0, tel-2);
                //pspDev.VoltR = Convert.ToDouble(tempp);
                leel.VoltR = Convert.ToDouble(tempp);
                if (comboBoxEdit8.Text==""||comboBoxEdit8.Text==null)
                {
                    comboBoxEdit8.Text = "0";
                }
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
                leel.ReferenceVolt = Convert.ToDouble(comboBoxEdit8.Text);
                InitData(leel);

            }
        }
        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = this.textBox11.Text;
            e.Handled = e.KeyChar < '0' || e.KeyChar > '9';   //������������
            if (e.KeyChar == (char)8)   //����������˼�
            {
                e.Handled = false;
            }
            if (e.KeyChar == (char)46)
            {
                if (str == "")   //��һ������������С����
                {
                    e.Handled = true;
                    return;
                }
                else
                { //С���㲻�������2��
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
            e.Handled = e.KeyChar < '0' || e.KeyChar > '9';   //������������
            if (e.KeyChar == (char)8)   //����������˼�
            {
                e.Handled = false;
            }
            if (e.KeyChar == (char)46)
            {
                if (str == "")   //��һ������������С����
                {
                    e.Handled = true;
                    return;
                }
                else
                { //С���㲻�������2��
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
            e.Handled = e.KeyChar < '0' || e.KeyChar > '9';   //������������
            if (e.KeyChar == (char)8)   //����������˼�
            {
                e.Handled = false;
            }
            if (e.KeyChar == (char)46)
            {
                if (str == "")   //��һ������������С����
                {
                    e.Handled = true;
                    return;
                }
                else
                { //С���㲻�������2��
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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = this.textBox4.Text;
            e.Handled = e.KeyChar < '0' || e.KeyChar > '9';   //������������
            if (e.KeyChar == (char)8)   //����������˼�
            {
                e.Handled = false;
            }
            if (e.KeyChar == (char)46)
            {
                if (str == "")   //��һ������������С����
                {
                    e.Handled = true;
                    return;
                }
                else
                { //С���㲻�������2��
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void mc_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string kv = comboBoxEdit1.Text.Replace("KV", "");

            frmSelectLineByYear frm = new frmSelectLineByYear();
            frm.Power = kv;
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            frm.ShowDialog();
            if (frm.line != null)
            {
                mc.Text = frm.line.LineName;
                textBox11.Text = frm.line.Length.ToString();
                comboBoxEdit1.Text = frm.line.Voltage.ToString() + "KV";
                comboBox22.Text = frm.line.LineType;
                comboBox44.Text = frm.line.ObligateField1;

            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmLinenew_Load(object sender, EventArgs e)
        {
            tyear.Text = _tyear;
            if (derefucelineflag)                          //��ʾ�����Ż��е������ֶ�
            {
                //this.label12.Visible = true;
               // this.tyear.Visible = true;
                this.label13.Visible = true;
                this.textEdit1.Visible = true;
               
            }
            else
            {
                //for (int i = 0; i < comboBox44.Properties.Items.Count; i++)
                //{
                //    if (comboBox44.Properties.Items[i].ToString() == "��ѡ" || comboBox44.Properties.Items[i].ToString()=="�ȴ�")
                //    {
                //        comboBox44.Properties.Items.RemoveAt(i);
                //    }
                //}
                comboBox44.Properties.Items.Clear();
                comboBox44.Properties.Items.Add("����");
                comboBox44.Properties.Items.Add("�Ͽ�");
            }
        }

        private void textBox3_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void textEdit1_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = this.textEdit1.Text;
            e.Handled = e.KeyChar < '0' || e.KeyChar > '9';   //������������
            if (e.KeyChar == (char)8)   //����������˼�
            {
                e.Handled = false;
            }
            if (e.KeyChar == (char)46)
            {
                if (str == "")   //��һ������������С����
                {
                    e.Handled = true;
                    return;
                }
                else
                { //С���㲻�������2��
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