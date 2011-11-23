using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
namespace Itop.DLGH
{
    public partial class FomInput : FormBase
    {
        public string strName;
        public FomInput()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(textBox1.Text==""){
                MessageBox.Show("名称不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            strName = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
            
        }

        private void FomInput_Load(object sender, EventArgs e)
        {
            textBox1.Text = strName;
        }
    }
}