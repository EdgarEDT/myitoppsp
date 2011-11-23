using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Layouts;
using Itop.Domain.Table;
using Itop.Common;
using Itop.Domain.Graphics;
using Itop.Domain.Stutistic;
using System.Reflection;
using System.Diagnostics;
using DevExpress.Utils;
using Itop.Domain.RightManager;
using Itop.Client.Base;
using FarPoint.Win;
using Itop.Domain.Forecast;
using Itop.Domain.HistoryValue;
using Itop.Domain.Chen;
using Itop.Client.Common;
namespace Itop.Client.Table
{
    public partial class FrmAddPN : FormBase
    {
        public FrmAddPN()
        {
            InitializeComponent();
            

        }

        public string SetFrmName
        {
            set { this.Text = value; }
        }
        public void SetCheckVisible()
        {
            this.checkEdit1.Visible = true;
        }
        public void SetCheckText(string t)
        {
            this.checkEdit1.Text = t;   
        }
        public bool BCheck
        {
            get { return checkEdit1.Checked; }
            set { checkEdit1.Checked = value; }
        }

        public string SetGroupName
        {
            set { this.groupControl1.Text = value; }
        }

        public string SetLabelName
        {
            set { this.label1.Text = value; }
        }

        public string ParentName
        {
            //get { return this.textEdit1.Text; }
            //set { textEdit1.Text = value; }
            get { return this.comboBoxEdit1.Text; }
            set { comboBoxEdit1.Text = value; }
        }
        public string Col1
        {
            get { return comboBoxEdit2.Text; }
            set { comboBoxEdit2.Text = value; }
        }
        public string Col2
        {
            get { return memoEdit1.Text; }
            set { memoEdit1.Text = value; }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void comboBoxEdit2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxEdit1.Properties.Items.Clear();
            string DQ = comboBoxEdit2.SelectedItem.ToString();
            string conn = "ProjectID='" + Itop.Client.MIS.ProgUID + "' and Col1='"+DQ+"' order by Sort";
            IList<PS_Table_AreaWH> list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);
            foreach (PS_Table_AreaWH area in list)
            {
                this.comboBoxEdit1.Properties.Items.Add(area.Title);
            }
        }

        private void FrmAddPN_Load(object sender, EventArgs e)
        {
            string DQ = "市区";
            string conn = "ProjectID='" + Itop.Client.MIS.ProgUID + "' and Col1='" + DQ + "' order by Sort";
            IList<PS_Table_AreaWH> list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);
            foreach (PS_Table_AreaWH area in list)
            {
                this.comboBoxEdit1.Properties.Items.Add(area.Title);
            }
        }


    }
}