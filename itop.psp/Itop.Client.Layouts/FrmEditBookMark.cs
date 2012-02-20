using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Word=Microsoft.Office.Interop.Word;
using Itop.Domain.Layouts;
namespace Itop.Client.Layouts
{
    public partial class FrmEditBookMark :Itop.Client.Base.FormBase
    {

        Word.Document BM_Word = new Microsoft.Office.Interop.Word.Document();
        Word.Bookmarks W_Bkm = null;
        public string MarkName = "";
        public string MarkDesc = "";
        public FrmEditBookMark()
        {
            InitializeComponent();
        }

        private void FrmEditBookMark_Load(object sender, EventArgs e)
        {
            txtMarkName.Text = MarkName;
            txtMarkDisc.Text = MarkDesc;
        }

        private void btnAddMark_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCanser_Click(object sender, EventArgs e)
        {
           
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (txtMarkName.Text.Length == 0)
            {
                MessageBox.Show("书签名称不能为空");
                return;
            }
            MarkName = txtMarkName.Text;
            MarkDesc = txtMarkDisc.Text;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}