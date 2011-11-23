using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
namespace Itop.TLPSP.DEVICE
{
    public partial class frnReport : FormBase
    {
        public frnReport()
        {
            InitializeComponent();
        }
        public string ShowText
        {
            get 
            {
                return textBox1.Text;               
            }
            set
            {
                textBox1.Text = value;
                textBox1.Refresh();
                Application.DoEvents();
            }
        }
    }
}