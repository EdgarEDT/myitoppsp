using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmAddXM : FormBase
    {
        public FrmAddXM()
        {
            InitializeComponent();
        }

        public string SetFrmName
        {
            set { this.Text = value; }
        }

        public string Comp
        {
            get { return this.textEdit1.Text; }
            set { this.textEdit1.Text = value; }
        }

        public string Build
        {
            get { return this.memoEdit1.Text; }
            set { this.memoEdit1.Text = value; }
        }

        public string Plan
        {
            get { return this.textEdit2.Text; }
            set { this.textEdit2.Text = value; }
        }

        public string Progre
        {
            get { return this.textEdit3.Text;}
            set { this.textEdit3.Text = value; }
        }

        public string Pow
        {
            get { return this.textEdit4.Text; }
            set { this.textEdit4.Text = value; }
        }

        public string Weig
        {
            get { return this.textEdit5.Text; }
            set { this.textEdit5.Text = value; }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}