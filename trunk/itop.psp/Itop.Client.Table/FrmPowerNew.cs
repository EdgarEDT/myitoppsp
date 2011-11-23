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
    public partial class FrmPowerNew : FormBase
    {
        public FrmPowerNew()
        {
            InitializeComponent();
        }
        private string mark;
        public string Mark
        {
            get { return mark; }
            set { mark = value; }
        }
        private void FrmPsNew_Load(object sender, EventArgs e)
        {
            yearRange = oper.GetYearRange("Col5='" + GetProject + "' and Col4='" + mark + "'");
            InitYear();
        }
        private OperTable oper = new OperTable();
        private Ps_YearRange yearRange = new Ps_YearRange();
        public void InitYear()
        {
            for (int i = yearRange.BeginYear; i <= yearRange.EndYear; i++)
            {
                comboBoxEdit2.Properties.Items.Add(i.ToString());
                comboBoxEdit3.Properties.Items.Add(i.ToString());
            }
            comboBoxEdit2.Text = DateTime.Now.Year.ToString();
            comboBoxEdit3.Text = Convert.ToString(DateTime.Now.Year+1);
            comboBoxEdit1.SelectedIndex = 0;
        }

        public string GetVolumn
        {
            get { return this.spinEdit1.Text; }
        }
        public string GetTitle
        {
            get { return textEdit1.Text; }
        }
        public string GetYear2
        {
            get { return comboBoxEdit3.Text; }
        }
        public string GetYear1
        {
            get { return comboBoxEdit2.Text; }
        }
        private string parentid;
        public string ParentID
        {
            set { parentid = value; }
        }
        public string PowerType
        {
            get { return comboBoxEdit1.Text; }
        }
        public string GetProject
        {
            set { projectid = value; }
            get { return projectid; }
        }
        private string projectid;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (comboBoxEdit3.Text == "")
            {
                MessageBox.Show("竣工年份不能为空"); return;
            }
            if (comboBoxEdit2.Text != "")
            {
                if (int.Parse(comboBoxEdit3.Text) < int.Parse(comboBoxEdit2.Text))
                {
                    MessageBox.Show("结束年必须大于开工年!"); return;
                }
            }
            DialogResult = DialogResult.OK;
            Ps_Table_Edit edit = new Ps_Table_Edit();
            edit.ID += "|" + GetProject;
            edit.ParentID = parentid;
            edit.StartYear = comboBoxEdit2.Text;
            edit.FinishYear = comboBoxEdit3.Text;
            edit.ProjectID = projectid;
            edit.Status = "新建";
            edit.Volume = spinEdit1.Text;
            edit.Col1 = comboBoxEdit1.Text;
            try
            {
                edit.Sort = OperTable.GetChildMaxSort() + 1;
            }
            catch { edit.Sort = 4; }
            if (edit.Sort < 4)
                edit.Sort = 4;
            Common.Services.BaseService.Create("InsertPs_Table_Edit", edit);
        }
        private void comboBoxEdit2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit2.Text == "")
                return;
            comboBoxEdit3.Properties.Items.Clear();
            for (int i = int.Parse(comboBoxEdit2.Text); i <= yearRange.EndYear; i++)
            {
                comboBoxEdit3.Properties.Items.Add(i.ToString());
            }
            comboBoxEdit3.Text = Convert.ToString(int.Parse(comboBoxEdit2.Text) + 1);
        }
    }

}