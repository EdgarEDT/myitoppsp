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
    public partial class frmTextInput : FormBase
    {
        public string Content;
        public frmTextInput()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Content = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}