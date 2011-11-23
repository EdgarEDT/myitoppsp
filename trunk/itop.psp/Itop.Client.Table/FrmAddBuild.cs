using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmAddBuild : FormBase
    {
        public FrmAddBuild()
        {
            InitializeComponent();
            InitCom();
        }

        public void InitCom()
        {
            OperTable oper=new OperTable();
            IList<string> list = oper.GetLineS1("1=1");
            foreach (string str in list)
                comboBoxEdit1.Properties.Items.Add(str);
            if (comboBoxEdit1.Properties.Items.Count > 0)
                comboBoxEdit1.SelectedIndex = 0;
        }
        public string GetV
        {
            get { return comboBoxEdit1.Text; }
            set { comboBoxEdit1.Text = value; editV = value; }
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
            get { return this.textEdit1.Text; }
            set { textEdit1.Text = value; }
        }
        private string conn = "",editV="";
        private bool b_edit = false;
        public string Conn
        {
            set { conn = value; }
        }
        public bool BEdit
        {
            set { b_edit = value; }
        }
        
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (!(b_edit && editV == comboBoxEdit1.Text))
            {
                IList listTypes = Common.Services.BaseService.GetList("SelectPs_Table_BuildProByConn", conn + "'" + comboBoxEdit1.Text + "'");
                if (listTypes.Count > 0)
                {
                    MessageBox.Show("已经存在此电压等级的分类，请别选一个电压等级，如果没有，可以在造价表中添加相应项。");
                    return;
                }
            }
            if (ParentName == "")
            {
                MessageBox.Show("分类名不为空"); return;
            }
            this.DialogResult = DialogResult.OK;
        }


    }
}