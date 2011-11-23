using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
    public partial class FrmBDZAdd : FormBase
    {
        public FrmBDZAdd()
        {
            InitializeComponent();
        }

        string title = "";
        string remark = "";

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        private void FrmBDZAdd_Load(object sender, EventArgs e)
        {
            textEdit1.Text = title;
            memoEdit1.Text = remark;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            title = textEdit1.Text;
            remark = memoEdit1.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}