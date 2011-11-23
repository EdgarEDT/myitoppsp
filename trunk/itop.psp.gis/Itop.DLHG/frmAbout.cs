using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;

namespace Itop.DLGH
{
    public partial class frmAbout : FormBase
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.process1.StartInfo.FileName = "iexplore.exe";
            this.process1.StartInfo.Arguments = "www.Itop.com";
            this.process1.Start();
        }
    }
}