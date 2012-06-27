using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Itop.TLPSP.DEVICE
{
    public partial class frmSeldevtype : DevExpress.XtraEditors.XtraForm
    {
        public frmSeldevtype()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 单位
        /// </summary>
        public int UnitFlag
        {
            get
            {
                return radioGroup1.SelectedIndex;
            }
            set
            {
                radioGroup1.SelectedIndex = value;
            }
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0)
            {
                e.Graphics.Clear(Color.Red);
            }
            else if (radioGroup1.SelectedIndex == 1)
            { 
                e.Graphics.Clear(Color.Green);
            }
            else if (radioGroup1.SelectedIndex == 2)
            {
                e.Graphics.Clear(Color.Blue);
            }
            else if (radioGroup1.SelectedIndex == 3)
            {
                e.Graphics.Clear(Color.Yellow);
            }
                      
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelControl1.Refresh();

        }
    }
}