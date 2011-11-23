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
    public partial class frmImgInfoInput : FormBase
    {
        private string strName;

        private string strRemark;

        public string StrRemark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }

        public string StrName
        {
            get { return strName; }
            set { strName = value; }
        }

        public frmImgInfoInput()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtName.Text=="")
            {
                MessageBox.Show("名称不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            strName = txtName.Text;
            strRemark = txtRemark.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmImgInfoInput_Load(object sender, EventArgs e)
        {
            txtName.Text = strName;
            txtRemark.Text = strRemark;
        }
    }
}