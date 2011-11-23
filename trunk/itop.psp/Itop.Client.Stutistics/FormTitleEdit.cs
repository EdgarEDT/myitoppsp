using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Client.Common;
using Itop.Domain.Stutistics;

namespace Itop.Client.Stutistics
{
    public partial class FormTitleEdit : DevExpress.XtraEditors.XtraForm
    {
        private string typeTitle = string.Empty;
        private int s1 = 1;
        private string sid = "";
        private bool isPower = false;

        private ArrayList al = new ArrayList();



        private bool isupdate = false;
        public bool Isupdate
        {
            get { return isupdate; }
            set { isupdate = value; }
        }

        public string TypeTitle
        {
            get { return typeTitle; }
            set { typeTitle = value; }
        }

        //public bool IsStuff
        //{
        //    get { return isstuff; }
        //    set { isstuff = value; }
        //}


        public string Sid
        {
            get { return sid; }
            set { sid = value; }
        }


        public int PowerType
        {
            get { return s1; }
            set { s1 = value; }
        }

        public FormTitleEdit()
        {
            InitializeComponent();
        }

        private void FormTypeTitle_Load(object sender, EventArgs e)
        {
            textEdit1.Text = TypeTitle;

            if (s1 == 1)
            {
                comboBoxEdit1.SelectedIndex = 0;
            }

            else if (s1 == 2)
            {
                comboBoxEdit1.SelectedIndex = 1;
            }

            else
            {
                comboBoxEdit1.SelectedIndex = 0;
            }

            if (isupdate)
                comboBoxEdit1.Enabled = false;
        }







        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text == string.Empty)
            {
                Itop.Common.MsgBox.Show("项目名称不能为空！");
                return;
            }

            if (textEdit1.Text == typeTitle)
            {
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                typeTitle = textEdit1.Text;

                if (comboBoxEdit1.SelectedIndex == 0)
                    s1 = 1;
                if (comboBoxEdit1.SelectedIndex == 1)
                    s1 = 2;


                DialogResult = DialogResult.OK;
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {


            

        }
    }
}