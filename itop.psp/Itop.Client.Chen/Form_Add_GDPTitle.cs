using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
namespace Itop.Client.Chen
{
    public partial class Form_Add_GDPTitle : FormBase
    {
        string _gdptitle = string.Empty;

        public string GDPTitle
        {
            get { return _gdptitle; }
            set { _gdptitle = value; }
        }
        public Form_Add_GDPTitle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入分类名称！");
                return;
            }
            if (textBox1.Text != "")
            {
                GDPTitle = textBox1.Text;
                DialogResult = DialogResult.OK;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }


    }
}