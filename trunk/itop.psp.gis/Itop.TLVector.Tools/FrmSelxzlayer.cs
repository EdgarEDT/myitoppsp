using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ItopVector.Core.Figure;

namespace ItopVector.Tools
{
    public partial class FrmSelxzlayer : DevExpress.XtraEditors.XtraForm
    {
        public FrmSelxzlayer()
        {
            InitializeComponent();
        }
        public List<Layer> Layercol = null;
        private void init()
        {
            comboBoxEdit9.Properties.DataSource = Layercol;
        }
        protected override void  OnLoad(EventArgs e)
        {
 	         base.OnLoad(e);
             init();
        }
        public string sellayerid
        {
            get
            {
                return comboBoxEdit9.Properties.GetKeyValueByDisplayText(comboBoxEdit9.Text).ToString();
            }
        }
    }
}