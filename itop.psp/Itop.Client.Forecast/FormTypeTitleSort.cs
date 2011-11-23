using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Itop.Client.Forecast
{
    public partial class FormTypeTitleSort : DevExpress.XtraEditors.XtraForm
    {
        private string typeTitle = string.Empty;

        public string TypeTitle
        {
            get { return typeTitle; }
            set { typeTitle = value; }
        }

        public int Sortid {
            get { return (int)spinEdit1.Value; }
            set { spinEdit1.Value = value; }
        }
        public FormTypeTitleSort()
        {
            InitializeComponent();
        }

        private void FormTypeTitle_Load(object sender, EventArgs e)
        {
            textEdit1.Text = TypeTitle;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(textEdit1.Text == string.Empty)
            {
                Itop.Common.MsgBox.Show("请输入分类名称！");
                return;
            }

            //if (textEdit1.Text == typeTitle)
            //{
            //    DialogResult = DialogResult.Cancel;
            //}
            //else
            {
                typeTitle = textEdit1.Text;
                DialogResult = DialogResult.OK;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }
    }
}