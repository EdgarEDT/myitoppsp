using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Common;
using Itop.Client.Base;

namespace Itop.Client.History
{
    public partial class FormFqHistory_Param : FormBase
    {
        public string Title = string.Empty;
        public decimal tsl = 0;
        public FormFqHistory_Param()
        {
            InitializeComponent();
        }
        private void FormFqHistory_Param_Load(object sender, EventArgs e)
        {
            this.Text = Title;
            spinEdit1.Value = tsl;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (spinEdit1.Value==tsl)
            {
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                tsl = spinEdit1.Value;
                DialogResult = DialogResult.OK;
            }
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

     
    }
}