using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmAreaStyle : FormBase
    {
        public FrmAreaStyle()
        {
            InitializeComponent();
        }      

        public string comBoxTEXT
        {
            get {
                return comboBoxEdit1.Text;
            }
            set {
                comboBoxEdit1.Text = value;
            }
        }

        public string SetLabelName
        {
            set { this.label1.Text = value; }
        } 

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }


    }
}