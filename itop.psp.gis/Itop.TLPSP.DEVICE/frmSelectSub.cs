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
    public partial class frmSelectSub : FormBase
    {
        public frmSelectSub()
        {
            InitializeComponent();
        }
        public int SelectSub
        {
            get {
                return radioGroup1.SelectedIndex;
            }
            set {
                radioGroup1.SelectedIndex = value;
            }
        }

        private void frmSelectSub_Load(object sender, EventArgs e)
        {
            MaximumSize = new Size(303, 198);
            MinimumSize = new Size(303, 198);
        }
    }
}