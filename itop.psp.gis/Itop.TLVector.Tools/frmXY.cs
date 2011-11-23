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
    public partial class frmXY : FormBase
    {
        
        public frmXY()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(textEdit1.Text==""){
                MessageBox.Show("X坐标不能为空。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            if (textEdit2.Text == "")
            {
                MessageBox.Show("Y坐标不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public decimal GetX()
        {
            return Convert.ToDecimal(textEdit1.Text);
        }
        public decimal GetY()
        {
            return Convert.ToDecimal(textEdit2.Text);
        }
    }
}