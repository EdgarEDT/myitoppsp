using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Table;
using System.Text.RegularExpressions;
using Itop.Domain.Stutistic;
using Itop.Domain.Graphics;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmProjTimeEdit : FormBase
    {
        public FrmProjTimeEdit()
        {
            InitializeComponent();
        }
        public Ps_Table_BuildPro Currentptb = null;
       
      
        private DateTime  ChangeTime(string str)
        {
            DateTime temptime = DateTime.Now;
            string result = "";
            if (str=="")
            {
               
            }
            else if (DateTime.TryParse(str, out temptime))
            {
               
            }
            return temptime;

        }
        private void FrmProjTimeEdit_Load(object sender, EventArgs e)
        {
            dateTime1.Value = ChangeTime((Currentptb.Stime1 == null || Currentptb.Stime1 == "") ? "" : Currentptb.Stime1);
            textBox1.Text = Currentptb.Stime1;
            if (Currentptb.Stime1==null||Currentptb.Stime1=="")
            {
                textBox1.Text = dateTime1.Value.ToShortDateString();
            }
            dateTime2.Value = ChangeTime((Currentptb.Stime2 == null || Currentptb.Stime2 == "") ? "" : Currentptb.Stime2);
            textBox2.Text = Currentptb.Stime2;
            dateTime3.Value = ChangeTime((Currentptb.Stime3 == null || Currentptb.Stime3 == "") ? "" : Currentptb.Stime3);
            textBox3.Text = Currentptb.Stime3;
            dateTime4.Value = ChangeTime((Currentptb.Stime4 == null || Currentptb.Stime4 == "") ? "" : Currentptb.Stime4);
            textBox4.Text = Currentptb.Stime4;
            dateTime5.Value = ChangeTime((Currentptb.Stime5 == null || Currentptb.Stime5 == "") ? "" : Currentptb.Stime5);
            textBox5.Text = Currentptb.Stime5;
            dateTime6.Value = ChangeTime((Currentptb.Stime6 == null || Currentptb.Stime6 == "") ? "" : Currentptb.Stime6);
            textBox6.Text = Currentptb.Stime6;
            dateTime7.Value = ChangeTime((Currentptb.Stime7 == null || Currentptb.Stime7 == "") ? "" : Currentptb.Stime7);
            textBox7.Text = Currentptb.Stime7;
            dateTime8.Value = ChangeTime((Currentptb.Stime8 == null || Currentptb.Stime8 == "") ? "" : Currentptb.Stime8);
            textBox8.Text = Currentptb.Stime8;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Currentptb.Stime1 = textBox1.Text;
            Currentptb.Stime2 = textBox2.Text;
            Currentptb.Stime3 = textBox3.Text;
            Currentptb.Stime4 = textBox4.Text;
            Currentptb.Stime5 = textBox5.Text;
            Currentptb.Stime6 = textBox6.Text;
            Currentptb.Stime7 = textBox7.Text;
            Currentptb.Stime8 = textBox8.Text;
            
            DialogResult = DialogResult.OK;
            this.Close();
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTime1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = dateTime1.Value.ToShortDateString();
        }

        private void dateTime2_ValueChanged(object sender, EventArgs e)
        {
            textBox2.Text = dateTime2.Value.ToShortDateString();
        }

        private void dateTime3_ValueChanged(object sender, EventArgs e)
        {
            textBox3.Text = dateTime3.Value.ToShortDateString();
        }

        private void dateTime4_ValueChanged(object sender, EventArgs e)
        {
            textBox4.Text = dateTime4.Value.ToShortDateString();
        }

        private void dateTime5_ValueChanged(object sender, EventArgs e)
        {
            textBox5.Text = dateTime5.Value.ToShortDateString();
        }

        private void dateTime6_ValueChanged(object sender, EventArgs e)
        {
            textBox6.Text = dateTime6.Value.ToShortDateString();
        }

        private void dateTime7_ValueChanged(object sender, EventArgs e)
        {
            textBox7.Text = dateTime7.Value.ToShortDateString();
        }

        private void dateTime8_ValueChanged(object sender, EventArgs e)
        {
            textBox8.Text = dateTime8.Value.ToShortDateString();
        }
      

    }
}