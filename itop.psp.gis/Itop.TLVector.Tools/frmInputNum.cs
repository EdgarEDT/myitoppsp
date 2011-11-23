﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmInputNum : FormBase
    {
        public string InputStr = "";

        public string InputStrSEL
        {
            get { return textEdit1.Text; }
            set { InputStr = value; }
        }

        public frmInputNum()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text=="")
            {
                MessageBox.Show("节点顺序不能为空。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            InputStr = textEdit1.Text;
            this.DialogResult = DialogResult.OK;
        }
     

        private void frmInputDialog_Load(object sender, EventArgs e)
        {
            textEdit1.Text = InputStr;
            textEdit1.Focus();
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
          
            if (e.KeyValue.ToString() == "13")
            {
                simpleButton1_Click(sender, new EventArgs());
            }
            if (e.KeyValue.ToString() == "27")
            {
                this.Close();
            }
        }

    }
}