using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Common;
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
    public partial class FormTypeTitleRemark : FormBase
    {
        private string typeTitle = string.Empty;
        private string typeRemark = string.Empty;

        public string TypeTitle
        {
            get { return typeTitle; }
            set { typeTitle = value; }
        }
        public string TypeRemark
        {
            get { return typeRemark; }
            set { typeRemark = value; }
        }
        public FormTypeTitleRemark()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }


        private void FormTypeTitleRemark_Load(object sender, EventArgs e)
        {
            textBox1.Text = typeTitle;
            textBox2.Text = typeRemark;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                MsgBox.Show("单位名称不能为空！");
                return;
            }
            typeTitle = textBox1.Text;
            typeRemark = textBox2.Text;
            DialogResult = DialogResult.OK;
        }
    }
}