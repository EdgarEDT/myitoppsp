using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.TLPSP.DEVICE;
using Itop.TLPsp.Graphical;
using Itop.Client.Base;
namespace Itop.TLPsp.Desktop
{
    public partial class Form1 : FormBase
    {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            frmDeviceManager main = new frmDeviceManager();
            main.Show();
            
        }

        private void button2_Click(object sender, EventArgs e) {
            
            frmProjectManager main3 = new frmProjectManager();
            main3.Show();
        }

        private void button3_Click(object sender, EventArgs e) {
            frmTLpspGraphical main2 = new frmTLpspGraphical();
            main2.Show();
        }
    }
}