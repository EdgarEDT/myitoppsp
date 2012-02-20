using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ItopVector.Tools;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using Itop.Client.Base;
namespace Itop.TLPsp
{
    public partial class frmYear : FormBase
    {
        public string uid = "";
        public frmYear()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmGrade f = new frmGrade();
            f.type = "3";
            f.InitData(uid);
            f.Show();
            f.CK();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmGrade f = new frmGrade();
            f.type = "1";
            f.InitData(uid);
            f.Show();
            f.CK();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            frmGrade f = new frmGrade();
            f.type = "2";
            f.InitData(uid);
            f.Show();
            f.CK();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            frmGrade f = new frmGrade();
            f.type = "3";
            f.InitData(uid);
            f.Show();
            f.CK();
        }
    }
}