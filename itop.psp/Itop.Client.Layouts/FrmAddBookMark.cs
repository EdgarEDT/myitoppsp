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
    public partial class FrmAddBookMark :Itop.Client.Base.FormBase
    {

        Word.Document BM_Word = new Microsoft.Office.Interop.Word.Document();
        Word.Bookmarks W_Bkm = null;
        public FrmAddBookMark()
        {
            InitializeComponent();
        }

        private void FrmAddBookMark_Load(object sender, EventArgs e)
        {

        }

        private void btnAddMark_Click(object sender, EventArgs e)
        {
           
        }

        private void btnCanser_Click(object sender, EventArgs e)
        {
           
        }

        private void btnChange_Click(object sender, EventArgs e)
        {

            BM_Word = FrmGHBZTLContents.W_Doc;
            W_Bkm = BM_Word.Bookmarks;
            if (BM_Word.Application.Selection.Range.Text == null)
            {
                MessageBox.Show("请选择添加书签的文本!");
                return;
            }
            if (txtMarkName.Text.Length == 0)
            {
                MessageBox.Show("请输入书签名称!");
                txtMarkName.Focus();
                return;
            }
            LayoutBookMark lbm = new LayoutBookMark();
            lbm.UID = "B_M_" + lbm.UID;
            lbm.LayoutID = FrmGHBZTLContents.LayoutID;
            lbm.MarkName = txtMarkName.Text;
            lbm.MarkDisc = txtMarkDisc.Text;
            if (chbType.Checked)
            {
                lbm.MarkType = "程序员";
            }
            else
            {
                lbm.MarkType = "用户";
            }
            Word.Range tmpRng = BM_Word.Application.Selection.Range;
            if (tmpRng.Bookmarks.Count > 0)
            {
                MessageBox.Show(tmpRng.Text + " 已是书签或包含已有书签!");
                return;
            }
            object oRng = tmpRng;
            W_Bkm.Add(lbm.UID.ToString(), ref oRng);
            object markid = lbm.UID.ToString();
            Word.Bookmark tempbk = W_Bkm.get_Item(ref markid);
            tempbk.Range.Font.Color = Word.WdColor.wdColorBlue;
            lbm.StartP = tempbk.Start;
            if (tempbk.Range.Text.Length > 140)
            {
                lbm.MarkText = tempbk.Range.Text.Substring(0, 140);
            }
            else
            {
                lbm.MarkText = tempbk.Range.Text;
            }
            try
            {
                Common.Services.BaseService.Create<LayoutBookMark>(lbm);
            }
            catch (Exception)
            {

                throw;
            }
            FrmBookMark.Parentfrm.InitGrid2();
            FrmBookMark.Parentfrm.Refresh();
            MessageBox.Show("书签添加成功！");
            //保存word
            FrmGHBZTLContents.ParentForm.SaveData_Word();
            txtMarkName.Text = "";
            txtMarkDisc.Text = "";
            txtMarkName.Focus();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}