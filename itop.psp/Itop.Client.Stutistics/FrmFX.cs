using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
    public partial class FrmFX : FormBase
    {
        string s1 = "";
        string s2 = "";
        string s3 = "";
        string s4 = "";

        public string S1
        {
            set { s1 = value; }
        }
        public string S2
        {
            set { s2 = value; }
        }
        public string S3
        {
            set { s3 = value; }
        }
        public string S4
        {
            set { s4 = value; }
        }

        public FrmFX()
        {
            InitializeComponent();
        }

        private void FrmFX_Load(object sender, EventArgs e)
        {
            textEdit1.Text = s1;
            textEdit2.Text = s2;
            textEdit3.Text = s3;

            textEdit4.Text = s4;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}