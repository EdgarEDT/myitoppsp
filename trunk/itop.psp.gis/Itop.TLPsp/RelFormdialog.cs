using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
namespace Itop.TLPsp
{
    public partial class RelFormdialog : FormBase
    {
        public RelFormdialog()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            
            //this.Parent.Visible = false;
        }
    }
}