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
    public partial class frmBdzSb : FormBase
    {
        public frmBdzSb() {
            InitializeComponent();
        }
        public string BDZUID {
            get { return ucbdZsbtopo1.BDZUID; }
            set {
                ucbdZsbtopo1.BDZUID = value;
            }
        }
        public UCBDZsbtopo UCBDZsbtop {
            get { return ucbdZsbtopo1; }
        }
        private void simpleButton1_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}