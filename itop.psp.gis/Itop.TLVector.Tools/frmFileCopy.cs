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
    public partial class frmFileCopy : FormBase
    {
        private string newFileName = "";

        public string NewFileName
        {
            get { return newFileName; }
            set { newFileName = value; }
        }
        public frmFileCopy()
        {
            InitializeComponent();
        }
        public void InitData(string name)
        {
            textEdit1.Text = name;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(textEdit2.Text==""){
                MessageBox.Show("另存名称不能为空。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            newFileName = textEdit2.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}