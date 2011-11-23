using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Table;
using Itop.Client.Projects;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FormYearSet : FormBase
    {
        int syear = 2009;
        int eyear = 2020;
        public void SetTzgs()
        {
            label2.Visible = false;
            spinEdit2.Visible = false;
            label2.Text = "标准年";
        }
        public void SetTzgsXs()
        {
            label2.Visible = false;
            spinEdit2.Visible = false;
            label2.Text = "年增长系数：";
        }
        public double Xisu 
        {
            get { return double.Parse(spinEdit2.Text); }
            set { spinEdit1.Value = Convert.ToDecimal(value); }
        }

        public int SYEAR
        {
            set { syear = value; }
            get { return syear; }
        }

        public int EYEAR
        {
            set { eyear = value; }
            get { return eyear; }
        }
        string type="";
        string pid="";


        public string TYPE
        {
            set { type = value; }
            get { return type; }
        }

        public string PID
        {
            set { pid = value; }
            get { return pid; }
        }

        Ps_YearRange py = new Ps_YearRange();

        public FormYearSet()
        {
            InitializeComponent();
        }

        private void FormYearSet_Load(object sender, EventArgs e)
        {
            py.Col4 = type;
            py.Col5 = pid;

            IList<Ps_YearRange> li = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);
            if (li.Count > 0)
            {
                py = li[0];
                syear = py.StartYear;
                eyear = py.FinishYear;
                
            }
            else
            {
                py.BeginYear = 1990;
                py.FinishYear = eyear;
                py.StartYear = syear;
                py.EndYear = 2060;
                

                py.ID = Guid.NewGuid().ToString();
                Services.BaseService.Create<Ps_YearRange>(py);
            }

            spinEdit1.Value = syear;
            spinEdit2.Value = eyear;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            syear = Convert.ToInt32(spinEdit1.Value);
            eyear = Convert.ToInt32(spinEdit2.Value);

            py.FinishYear = eyear;
            py.StartYear = syear;
            Services.BaseService.Update<Ps_YearRange>(py);
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}