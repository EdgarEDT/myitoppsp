using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmMapSetup : FormBase
    {
        public frmMapSetup() {
            InitializeComponent();
        }
        

        public int MapOpacity {
            get {
                return trackBar1.Value;             
            }
            set { 
                trackBar1.Value = value;
                numericUpDown1.Value = value;
            }
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            isinit = false;
        }
        bool isinit = true;//≥ı ºªØ
        private void trackBar1_ValueChanged(object sender, EventArgs e) {
            if (!isinit)
                numericUpDown1.Value = trackBar1.Value;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            if (!isinit) trackBar1.Value =(int) numericUpDown1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

    }
}