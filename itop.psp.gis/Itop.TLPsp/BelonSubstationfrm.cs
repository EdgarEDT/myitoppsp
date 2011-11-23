using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class BelonSubstationfrm : FormBase
    {
        private string str_year = "";

        private string str_Power = "";
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
        public BelonSubstationfrm()
        {
            InitializeComponent();
        }
        public BelonSubstationfrm(PSPDEV pspDEV)
        {
            InitializeComponent();
            if (pspDEV.HuganLine1!=null)
            {
                mc.Text = pspDEV.HuganLine1;
            }
        }
        private void mc_EditValueChanged(object sender, EventArgs e)
        {

        }
        public string Name//±äµçÕ¾Ãû³Æ
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
        private void buttonEdit1_Properties_Click(object sender, EventArgs e)
        {
            string kv = str_Power;

            frmSelectSubByYear frm = new frmSelectSubByYear(1);
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
              
            }
            //this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}