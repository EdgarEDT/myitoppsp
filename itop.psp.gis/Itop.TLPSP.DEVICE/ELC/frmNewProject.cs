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
        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }
    }
}