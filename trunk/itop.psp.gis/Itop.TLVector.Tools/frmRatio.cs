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
    public partial class frmRatio : FormBase
    {
        private string scale = "";

        public string ViewScale
        {
            get { return scale; }
            set { scale = value; }
        }
        public frmRatio()
        {
            InitializeComponent();
        }
        public void InitData(string ViewScale)
        {
            textEdit1.Text = ViewScale;
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(textEdit1.Text==""){
                MessageBox.Show("请输入容载比.", " 提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                if (Convert.ToDouble(textEdit1.Text) > 0)
                {
                    ViewScale = textEdit1.Text;
                }
                else
                {
                    MessageBox.Show("容载比必须为正数", " 提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch
            {
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmRatio_Load(object sender, EventArgs e)
        {
            textEdit1.Focus();
        }


       
    }
}