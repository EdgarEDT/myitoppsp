using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Itop.Client.History
{
    public partial class FormFqHistoryDF_add : DevExpress.XtraEditors.XtraForm
    {
        private string typeTitle = string.Empty;

        public string TypeTitle
        {
            get { return typeTitle; }
            set { typeTitle = value; }
        }
      
        
        public FormFqHistoryDF_add()
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
                Itop.Common.MsgBox.Show("请输入类别名称！");
                return;
            }
                typeTitle = textEdit1.Text;
                DialogResult = DialogResult.OK;
            
        }

        

    }
}