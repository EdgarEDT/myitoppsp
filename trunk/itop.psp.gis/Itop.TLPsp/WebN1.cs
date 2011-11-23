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
    public partial class WebN1 : FormBase
    {
        public WebN1()
        {
            InitializeComponent();
        }

        private void SelAll_CheckedChanged(object sender, EventArgs e)
        {
            /*
            if (SelAll.Checked)
                        {
                            this.DialogResult = DialogResult.OK;
                        }*/
            
        }

        private void NSelect_CheckedChanged(object sender, EventArgs e)
        {
           /*
             if (NSelect.Checked)
                        {
                            this.DialogResult = DialogResult.Ignore;
                        }*/
            
        }

        private void buttonN0_Click(object sender, EventArgs e)
        {
           // MessageBox.Show("请选择全网还是部分网络！");
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (SelAll.Checked)
            {
                this.DialogResult = DialogResult.OK;
            }
            else if (NSelect.Checked)
            {
                this.DialogResult = DialogResult.Ignore;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}