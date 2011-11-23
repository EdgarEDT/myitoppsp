using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Table;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmPowerAdd : FormBase
    {
        public FrmPowerAdd()
        {
            InitializeComponent();
        }

        Ps_YearRange yearR = new Ps_YearRange();
        private OperTable oper = new OperTable();
        public string GetProject
        {
            set { projectid = value; }
            get { return projectid; }
        }
        private string mark;
        public string Mark
        {
            get { return mark; }
            set { mark = value; }
        }
        private string projectid;
        public void LoadYear()
        {
            yearR = oper.GetYearRange("Col5='" + GetProject + "' and Col4='" + mark + "'");

            for (int i = yearR.BeginYear; i <= yearR.FinishYear; i++)
            {
                comboBoxEdit2.Properties.Items.Add(i.ToString());
            }
            comboBoxEdit2.SelectedIndex = 0;
            comboBoxEdit1.SelectedIndex = 0;
        }

        public string ProjectName
        {
            get { return textEdit1.Text; }
            set { textEdit1.Text = value; }
        }

        public string PowerType
        {
            get { return comboBoxEdit1.Text; }
            set { comboBoxEdit1.Text = value; }
        }

        public string PowerFac
        {
            get { return spinEdit1.Text.ToString(); }
            set { spinEdit1.Value = Convert.ToDecimal(value); }
        }

        public string StartYear
        {
            get { return comboBoxEdit2.Text; }
            set { comboBoxEdit2.Text = value; }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void FrmPowerAdd_Load(object sender, EventArgs e)
        {
            LoadYear();
        }
    }
}