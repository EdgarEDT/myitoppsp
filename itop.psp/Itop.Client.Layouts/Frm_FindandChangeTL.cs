using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using Itop.Domain.Layouts;
using System.Reflection;
using Itop.Client.Base;
namespace Itop.Client.Layouts
{
    public partial class Frm_FindandChangeTL : FormBase
    {
        public bool saveflag = false;
        public string oldstr = "";
        public string newstr = "";
        //List<LayoutBookMark> marklist = new List<LayoutBookMark>();
        IList<LayoutBookMark> marklist = null;
        public object data_object = null;
        public Frm_FindandChangeTL()
        {
            InitializeComponent();
        }
        public Itop.Client.Layouts.FrmGHBZTLContents.MKData mkd = new FrmGHBZTLContents.MKData();
        public Word.Bookmarks W_Bkm = null;
        public Word.Bookmark tempmk = null;
        private bool flag = false;
        private int num = 0;
        private int TestNum = 0;
        public string layoutID = "";
       // private List<string> strlist = new List<string>();
        private SortedList<int, string> strlist = new SortedList<int, string>();
        private void Frm_FindandChangeTL_Load(object sender, EventArgs e)
        {
            adddata();
            string conn = "LayoutID='" + layoutID + "' order by StartP";
            IList<LayoutBookMark> templist = Common.Services.BaseService.GetList<LayoutBookMark>("SelectLayoutBookMarkList", conn);
            marklist = templist;
            if (marklist.Count==0)
            {
                MessageBox.Show("无书签！请先添加书签");
                this.Close();
                return;
            }
            if (W_Bkm.Exists(marklist[0].UID.ToString()))
            {
                object tempid = marklist[0].UID.ToString();
                tempmk = W_Bkm.get_Item(ref tempid);
                tempmk.Select();
                oldstr = tempmk.Range.Text;
                txtold.Text = oldstr;
                if (mkd.FindStr(marklist[0].UID.ToString())!=-1)
                {
                    txtnew.Text = mkd.Data[mkd.FindStr(marklist[0].UID.ToString())].BQdata.ToString();
                    data_object = mkd.Data[mkd.FindStr(marklist[0].UID.ToString())].BQdata;
                    newstr = txtnew.Text;
                }
                else
                {
                    newstr = "暂无值";
                    txtnew.Text = newstr;
                    txtnew.SelectAll();
                }
            } 
        }
        private void adddata()
        {
            string conn = "LayoutID='" + layoutID + "' order by StartP";
            IList<LayoutBookMark> list = Common.Services.BaseService.GetList<LayoutBookMark>("SelectLayoutBookMarkList", conn);
            this.gridControl2.DataSource = list;
        }
        private void gridRefreh()
        {
            if (num<gridView2.RowCount)
            {
                gridView2.FocusedRowHandle = num;
            }
        }
        //private void readBK()
        //{
        //    W_Bkm.DefaultSorting = Word.WdBookmarkSortBy.wdSortByLocation;
            
        //    foreach (Word.Bookmark bk  in W_Bkm)
        //    {
        //        strlist.Add(bk.Start,bk.Name);
        //    }
        //}

        //替换并到下一个
        private void btnChangeandNext_Click(object sender, EventArgs e)
        {
            object tempid = tempmk.Name.ToString();
            Word.Range tmpRng = tempmk.Range;
            tmpRng.Text = txtnew.Text;
            object oRng = tmpRng;
            W_Bkm.Add(tempid.ToString(), ref oRng);
            if (txtold.Text==txtnew.Text)
            {
                tmpRng.Font.Color = Word.WdColor.wdColorGold;
            }
            if (num >= marklist.Count-1)
            {
                MessageBox.Show("标签已到结尾");
                return;
            }
            num = num + 1;;
            if (W_Bkm.Exists(marklist[num].UID.ToString()))
            {
                object tempid2 = marklist[num].UID.ToString();
                tempmk = W_Bkm.get_Item(ref tempid2);
                tempmk.Select();
                oldstr = tempmk.Range.Text;
                txtold.Text = oldstr;
                if (mkd.FindStr(marklist[num].UID.ToString()) != -1)
                {
                    txtnew.Text = mkd.Data[mkd.FindStr(marklist[num].UID.ToString())].BQdata.ToString();
                    newstr = txtnew.Text;
                    txtnew.SelectAll();
                }
                else
                {
                    newstr = "暂无值";
                    txtnew.Text = newstr;
                    txtnew.SelectAll();
                }
            }
            gridRefreh();
        }
        //替换
        private void btnChange_Click(object sender, EventArgs e)
        {
            object NoThing=Missing.Value;
            object tempid = tempmk.Name.ToString();
            Word.Range tmpRng = tempmk.Range;
           // //tempmk.Range.
           // Word.Document tempdoc = FrmGHBZTLContents.W_Doc;
           //Word.Table temptable= tempdoc.Tables.Add(tmpRng, 1, 1, ref NoThing, ref NoThing);
           //temptable = (Word.Table)data_object;
           // //tempmk.Range = data_object;
            tmpRng.Text = txtnew.Text;
            object oRng = tmpRng;
            W_Bkm.Add(tempid.ToString(), ref oRng);
            if (txtold.Text == txtnew.Text)
            {
                tmpRng.Font.Color = Word.WdColor.wdColorGold;
            }
        }
        //下一个
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (num < marklist.Count-1 )
            {
                num = num + 1; ;
                if (W_Bkm.Exists(marklist[num].UID.ToString()))
                {
                    object tempid = marklist[num].UID.ToString();
                    tempmk = W_Bkm.get_Item(ref tempid);
                    tempmk.Select();
                    oldstr = tempmk.Range.Text;
                    txtold.Text = oldstr;
                    if (mkd.FindStr(marklist[num].UID.ToString()) != -1)
                    {
                        txtnew.Text = mkd.Data[mkd.FindStr(marklist[num].UID.ToString())].BQdata.ToString();
                        newstr = txtnew.Text;
                        txtnew.SelectAll();
                    }
                    else
                    {
                        newstr = "暂无值";
                        txtnew.Text = newstr;
                        txtnew.SelectAll();
                    }
                }
                gridRefreh();
            }
            else
            {
                MessageBox.Show("已到书签结尾！");
            }
            
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            saveflag = true;
            this.Close();
        }

        private void btnCanser_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (TestNum<strlist.Count)
            {
                object mkname2 = strlist.Values[TestNum].ToString();
                Word.Range tmpRng2 = W_Bkm.get_Item(ref mkname2).Range;
                txtold.Text = tmpRng2.Text;
                tmpRng2.Select();
                TestNum++;
            }
            else
            {
                MessageBox.Show("已到标签结尾");
                TestNum = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Word.Document dc = W_Bkm.Application.ActiveDocument;
            Word.Range tmpRng = dc.Application.Selection.Range;
            //object mkname = mkd.Data[num].BQname.ToString();
            //Word.Range tmpRng = W_Bkm.get_Item(ref mkname).Range;
            object oRng = tmpRng;
            W_Bkm.Add(txtnew.Text.ToString(), ref oRng);
        }
        //关闭窗体
        private void Frm_FindandChangeTL_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!saveflag)
            {
                if (MessageBox.Show("您还没有保存，确定退出吗？", "询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)==DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }
            
            }
        }
      
       
        //当表格光标发生变化时 
         private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (marklist!=null)
            {
                num = gridView2.FocusedRowHandle - 1;
                btnNext.PerformClick();
            } 
        }
       
    }
}