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
    public partial class frmViewScale : FormBase
    {
        private string scale = "";

        public string ViewScale
        {
            get { return scale; }
            set { scale = value; }
        }
        public frmViewScale()
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
                MessageBox.Show("请输入比例尺.", " 提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                if (Convert.ToDecimal(textEdit1.Text) > 0)
                {
                    ViewScale = textEdit1.Text;
                }
                else
                {
                    MessageBox.Show("比例尺必须为正整数", " 提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch
            {
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        


       
    }
}