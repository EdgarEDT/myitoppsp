using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
using Itop.Domain.Table;
using Itop.Domain.Layouts;
using Word = Microsoft.Office.Interop.Word;
//using Itop.Domain.Layouts;

namespace Itop.Client.Layouts
{
    public partial class FrmBookMark : FormBase
    {
        public static string CHID = "";
        public static FrmBookMark Parentfrm = new FrmBookMark();
        Word.Document BM_Word = new Microsoft.Office.Interop.Word.Document();
        Word.Bookmarks W_Bkm = null;
        //查找计数
        public int num = -1;

        public FrmBookMark()
        {
            InitializeComponent();
        }

        private void FrmAreaData_Load(object sender, EventArgs e)
        {
            InitGrid2();
        }

        public string ProjectID
        {
            get { return ProjectUID; }
        }

        public void InitGrid2()
        {
            string conn = "LayoutID='" + FrmGHBZTLContents.LayoutID + "' order by StartP";
            IList<LayoutBookMark> list = Common.Services.BaseService.GetList<LayoutBookMark>("SelectLayoutBookMarkList", conn);
            this.gridControl2.DataSource = list;
            
        }
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        private void barButtonItem7_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmAddBookMark fabm = new FrmAddBookMark();
            FrmBookMark.Parentfrm = this;
            fabm.Show();
        }

        //删除书签
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BM_Word = FrmGHBZTLContents.W_Doc;
            W_Bkm = BM_Word.Bookmarks;
            if (gridView2.SelectedRowsCount==0)
            {
                MessageBox.Show("请选择标签");
                return;
            }
            object bookmarkid = gridView2.GetRowCellDisplayText(this.gridView2.FocusedRowHandle, "UID").ToString();
            string bookmarkname=gridView2.GetRowCellDisplayText(this.gridView2.FocusedRowHandle, "MarkName").ToString();
            string booktype = gridView2.GetRowCellDisplayText(this.gridView2.FocusedRowHandle, "MarkType").ToString();
            Word.Range temprange = null;
            bool flag= W_Bkm.Exists("test");
            if (W_Bkm.Exists(bookmarkid.ToString()))
            {
                temprange = W_Bkm.get_Item(ref bookmarkid).Range;
                temprange.Select();
            }
            if (booktype == "程序员")
            {
                MessageBox.Show("您不能删除程序员定义的标签，否则程序无法正常更新数据！");
                return;
            }
            if (MessageBox.Show("确定要删除书签" + bookmarkname + "?","询问",  MessageBoxButtons.OKCancel)==DialogResult.OK)
            {
                //替换后标签就自动删除了
                if (W_Bkm.Exists(bookmarkid.ToString()))
                {
                    temprange.Text = temprange.Text;
                    temprange.Font.Color = Word.WdColor.wdColorBlack;
                }
               
                LayoutBookMark data = this.gridView2.GetRow(this.gridView2.FocusedRowHandle) as LayoutBookMark;// new Ps_Table_GDP();
                try
                {
                    Common.Services.BaseService.Delete<LayoutBookMark>(data);
                    InitGrid2();
                    this.Refresh();
                    MessageBox.Show("删除成功");
                    FrmGHBZTLContents.ParentForm.SaveData_Word();
                }
                catch (Exception)
                {
                    
                    throw;
                } 
            } 

        }
        //修改书签
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BM_Word = FrmGHBZTLContents.W_Doc;
            W_Bkm = BM_Word.Bookmarks;

            if (gridView2.SelectedRowsCount == 0)
            {
                MessageBox.Show("请选择标签");
                return;
            }
            string bookmarkname = gridView2.GetRowCellDisplayText(this.gridView2.FocusedRowHandle, "MarkName").ToString();
            string bookmarkid = gridView2.GetRowCellDisplayText(this.gridView2.FocusedRowHandle, "UID").ToString();
            string bookmarkdisc = gridView2.GetRowCellDisplayText(this.gridView2.FocusedRowHandle, "MarkDisc").ToString();
            LayoutBookMark data = this.gridView2.GetRow(this.gridView2.FocusedRowHandle) as LayoutBookMark;
            FrmEditBookMark tempfebk = new FrmEditBookMark();
            tempfebk.MarkName = bookmarkname;
            tempfebk.MarkDesc = bookmarkdisc;
            tempfebk.ShowDialog();
            if (tempfebk.DialogResult==DialogResult.OK)
            {
                data.MarkName = tempfebk.MarkName;
                data.MarkDisc = tempfebk.MarkDesc;
                try
                {
                    Common.Services.BaseService.Update<LayoutBookMark>(data);
                    InitGrid2();
                    this.Refresh();
                    MessageBox.Show("修改成功！");
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }

        }
        //定位书签
        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BM_Word = FrmGHBZTLContents.W_Doc;
            W_Bkm = BM_Word.Bookmarks;
            if (gridView2.SelectedRowsCount == 0)
            {
                MessageBox.Show("请选择标签");
                return;
            }
            object bookmarkid = gridView2.GetRowCellDisplayText(this.gridView2.FocusedRowHandle, "UID").ToString();
            string bookmarkname = gridView2.GetRowCellDisplayText(this.gridView2.FocusedRowHandle, "MarkName").ToString();
            Word.Range temprange = null;
            if (W_Bkm.Exists(bookmarkid.ToString()))
            {
                temprange = W_Bkm.get_Item(ref bookmarkid).Range;
                temprange.Select();
            }
            else
            {
                MessageBox.Show("无法定位，可能在Word中已删除了该标签");
            }
        }
        //查找名称
        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barEditItem1.EditValue==null)
            {
                MessageBox.Show("请输入要查找的书签的名称");
                return;
            }
            if ( gridView2.RowCount==0)
            {
                MessageBox.Show("没有书签可查找");
                return;
            }
            string tagstr = barEditItem1.EditValue.ToString();
            for (int i = num+1; i <= gridView2.RowCount; i++)
            {
                if (i==gridView2.RowCount)
                {
                    num = -1;
                    MessageBox.Show("已搜索到结尾");
                }
                if (gridView2.GetRowCellDisplayText(i, "MarkName").ToString().Contains(tagstr))
                {
                    gridView2.FocusedRowHandle = i;
                    this.Refresh();
                    num = i;
                    //定位该书签
                    BM_Word = FrmGHBZTLContents.W_Doc;
                    W_Bkm = BM_Word.Bookmarks;
                    object bookmarkid = gridView2.GetRowCellDisplayText(this.gridView2.FocusedRowHandle, "UID").ToString();
                    string bookmarkname = gridView2.GetRowCellDisplayText(this.gridView2.FocusedRowHandle, "MarkName").ToString();
                    Word.Range temprange = null;
                    if (W_Bkm.Exists(bookmarkid.ToString()))
                    {
                        temprange = W_Bkm.get_Item(ref bookmarkid).Range;
                        temprange.Select();
                    }
                    break;
                }
                
            }
            //barEditItem1.Caption;
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitGrid2();
        }

    }
}