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
    public partial class frmInJWD : FormBase
    {
        public bool GetCheck()
        {
            return checkBox1.Checked;
        }
        public string GetFileName()
        {
            return buttonEdit1.Text;
        }
        public frmInJWD()
        {
            InitializeComponent();
        }

        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void buttonEdit1_Properties_Click(object sender, EventArgs e)
        {
            
             openFileDialog1.Filter = "Excel文件(*.xls)|*.xls";
             if (openFileDialog1.ShowDialog() == DialogResult.OK)
             {
                 buttonEdit1.Text = openFileDialog1.FileName;
             }
        }

      
    }
}