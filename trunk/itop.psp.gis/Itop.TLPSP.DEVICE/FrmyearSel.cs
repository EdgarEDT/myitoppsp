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
    public partial class FrmyearSel : DevExpress.XtraEditors.XtraForm
    {
        public FrmyearSel()
        {
            InitializeComponent();
        }
        private void initcombox()
        {
            for (int i = 0; i < 50;i++ )
            {
                string y = (1990 + i).ToString();
                comboBoxEdit1.Properties.Items.Add(y);
            }
            comboBoxEdit1.Text = DateTime.Now.Year.ToString();
        }
        public string Year
        {
            get { return comboBoxEdit1.Text; }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            initcombox();
        }
    }
}