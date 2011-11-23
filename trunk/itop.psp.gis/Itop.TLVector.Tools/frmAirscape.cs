using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ItopVector;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmAirscape : FormBase
    {
        public frmAirscape()
        {
            InitializeComponent();
        }
        public void InitData(ItopVectorControl vector)
        {
            miniatureView1.VectorControl = vector;
        }

        private void frmAirscape_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            //this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}