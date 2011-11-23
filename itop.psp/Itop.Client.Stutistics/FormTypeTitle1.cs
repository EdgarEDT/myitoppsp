using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Itop.Client.Stutistics
{
    public partial class FormTypeTitle1 : DevExpress.XtraEditors.XtraForm
    {
        private string typeTitle = string.Empty;
        private string typeName = string.Empty;
        private int dydj = 220;
        private bool isparent = false;

        public string TypeTitle
        {
            get { return typeTitle; }
            set { typeTitle = value; }
        }

        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        public int Dydj
        {
            get { return dydj; }
            set { dydj = value; }
        }

        public bool IsParent
        {
            get { return isparent; }
            set { isparent = value; }
        }

        public FormTypeTitle1()
        {
            InitializeComponent();
        }

        private void FormTypeTitle_Load(object sender, EventArgs e)
        {
            textEdit1.Text = TypeTitle;
            comboBoxEdit1.Text = typeName;
            comboBoxEdit2.Text = dydj.ToString();

            if (comboBoxEdit1.Text == "")
                comboBoxEdit1.SelectedIndex = 0;
            if (comboBoxEdit2.Text == "")
                comboBoxEdit2.SelectedIndex = 0;
            if (!isparent)
            {
                comboBoxEdit1.Visible = false;
                comboBoxEdit2.Visible = false;
                label1.Visible = false;
                label3.Visible = false;

                label2.Location = new Point(20, 51);
                textEdit1.Location = new Point(98, 48);
                //comboBoxEdit1.Properties.ReadOnly = true;
                //comboBoxEdit2.Properties.ReadOnly = true;


            }
            groupBox1.TabStop = false;
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(textEdit1.Text == string.Empty)
            {
                Itop.Common.MsgBox.Show("项目名称不能为空！");
                return;
            }

            //if (textEdit1.Text == typeTitle)
            //{
            //    DialogResult = DialogResult.Cancel;
            //}
            //else
            //{
            //    typeTitle = textEdit1.Text;
            //    DialogResult = DialogResult.OK;
            //}


            typeTitle = textEdit1.Text;
            typeName = comboBoxEdit1.Text;
            if(comboBoxEdit2.SelectedIndex>-1)
            dydj = int.Parse(comboBoxEdit2.Text);
            DialogResult = DialogResult.OK;
        }

        private void comboBoxEdit2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}