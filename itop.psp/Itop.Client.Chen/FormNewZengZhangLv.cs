using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.HistoryValue;
using Itop.Common;
using Itop.Client.Base;

namespace Itop.Client.Chen
{
    public partial class FormNewZengZhangLv : DevExpress.XtraEditors.XtraForm
    {
        private double fuhe = 0;



        public double Fuhe
        {
            get { return fuhe; }
            set { fuhe = value; }
        }

        public FormNewZengZhangLv()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            textBox1.Focus();
            fuhe = (double)calcEdit1.Value;

                this.DialogResult = DialogResult.OK;
         
        }

        private void FormNewYear_Load(object sender, EventArgs e)
        {
            calcEdit1.Value = (decimal)fuhe;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }
        private InputLanguage oldInput = null;
        private void spinEdit1_Enter(object sender, EventArgs e)
        {
            ////oldInput = InputLanguage.CurrentInputLanguage;
            ////InputLanguage.CurrentInputLanguage = null;
        }
    }
}