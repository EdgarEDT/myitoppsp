using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
namespace Itop.TLPSP.DEVICE
{
    public partial class frmNewProject : FormBase
    {
        public frmNewProject()
        {
            InitializeComponent();
            
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            for (int i = 0; i <= 30;i++ )
            {
                comboBoxEdit2.Properties.Items.Add((2000 + i).ToString());
            }
        }
        public void init()
        {
            if (!flag)
            {
                comboBoxEdit1.Text = "潮流";
                comboBoxEdit1.Enabled = false;
            }
            else
            {
                comboBoxEdit1.Text = "短路";
                comboBoxEdit1.Enabled = false;
            }
        }
        public bool flag = false;
        public string Name
        {
            get{
                return textEdit1.Text;
            }
            set{
                textEdit1.Text = value;
            }
        }
        public string FileType
        {
            get
            {
                return comboBoxEdit1.Text;
            }
            set
            {
                comboBoxEdit1.Text = value;
            }
        }
        public string BelongYear
        {
            get
            {
                return comboBoxEdit2.Text;
            }
            set
            {
                comboBoxEdit2.Text = value;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }
    }
}