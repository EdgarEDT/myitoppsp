using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Common;
using Itop.Domain.Layouts;
using System.Collections;
using Itop.Common;
using System.Diagnostics;
using DevExpress.Utils;
using Itop.Domain.RightManager;
using Itop.Client.Chen;
using Itop.Client.Stutistics;
using Itop.DLGH;
using System.Runtime.InteropServices;
using System.IO;

using DevExpress.XtraTreeList.Nodes;
using TONLI.BZH.UI;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using Itop.Domain.Table;
using Itop.Domain.Graphics;
using System.Reflection;
using Itop.Client.Base;
namespace Itop.Client.Layouts
{
    public partial class FrmGHBZTLContents : FormBase
    {
        public WordBuilder Staword = new WordBuilder();
        /// <summary>
        /// 卷号
        /// </summary>
        private string ProjID = Itop.Client.MIS.ProgUID;
        public static FrmGHBZTLContents ParentForm = new FrmGHBZTLContents();
        public static Word.Document _W_Doc=new Document ();
        public static Word.Document W_Doc
        {
            get
            {
                return _W_Doc;
                //return (Word.Document)ParentForm.dsoFramerWordControl1.AxFramerControl.ActiveDocument;
            }
            set
            {
                _W_Doc = value;
            }
        }
        public static Word.Bookmarks W_Bkm = null;
        //当前章节的UID
        public static string LayoutID="";
        public bool RefFalg = false;
        public class BQData
        {
           public  string BQname = "";
           public  object BQdata = null;
           public BQData()
           {
               BQname="";
               BQdata=null;
           }
        }
        public class MKData
        {
            public string ZJname = "";
            public List<BQData> Data= new List<BQData>();
            
            public int FindStr(string str)
            {
                int value=-1;
                for (int i = 0; i < Data.Count; i++)
                {
                    if (str==Data[i].BQname.ToString())
                    {
                        value=i;
                        break;
                    }
                }
              return value;
            }
        
        }
        public struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        public class SECURITY_ATTRIBUTES
        {
            public int nLength;
            public string lpSecurityDescriptor;
            public bool bInheritHandle;
        }

        public struct STARTUPINFO
        {
            public int cb;
            public string lpReserved;
            public string lpDesktop;
            public int lpTitle;
            public int dwX;
            public int dwY;
            public int dwXSize;
            public int dwYSize;
            public int dwXCountChars;
            public int dwYCountChars;
            public int dwFillAttribute;
            public int dwFlags;
            public int wShowWindow;
            public int cbReserved2;
            public byte lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }


        public string path = System.Windows.Forms.Application.StartupPath + "\\setting.ini";

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal,int size, string filePath);


        [DllImport("Kernel32.dll", CharSet = CharSet.Ansi)]
        public static extern bool CreateProcess(StringBuilder lpApplicationName, StringBuilder lpCommandLine,
                                                                    SECURITY_ATTRIBUTES lpProcessAttributes,
                                                                    SECURITY_ATTRIBUTES lpThreadAttributes,
                                                                    bool bInheritHandles,
                                                                    int dwCreationFlags,
                                                                    StringBuilder lpEnvironment,
                                                                    StringBuilder lpCurrentDirectory,
                                                                    ref STARTUPINFO lpStartupInfo,
                                                                    ref PROCESS_INFORMATION lpProcessInformation
                                                                    );


        public void IniWritevalue(string Section, string Key, string value)
        {
            WritePrivateProfileString(Section, Key, value, this.path);
        }

        public string IniReadvalue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);

            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
            return temp.ToString();
        } 


        [DllImport("Kernel32.dll", CharSet = CharSet.Ansi)]
        public static extern int WaitForSingleObject(IntPtr hHandle, int dwMilliseconds);


        [DllImport("Kernel32.dll", CharSet = CharSet.Ansi)]
        public static extern bool CloseHandle(IntPtr hObject);

        private LayoutsClass lcs = new LayoutsClass();

        private string layoutUID="";
        private IList ilist = new ArrayList();
        System.Data.DataTable dataTable = new System.Data.DataTable();
        private VsmdgroupProg vp = new VsmdgroupProg();
        bool isstate = false;
        byte[] fb = null;

        public string LayoutUID
        {
            set { layoutUID = value; }
        }

        public VsmdgroupProg RightObject
        {
            set { vp = value; }
        }

        public FrmGHBZTLContents()
        {
            InitializeComponent();
        }

        //word失去焦点后
        private void AxFramerControl_LostFocus(object sender, System.EventArgs e)
        {
            if (dsoFramerWordControl1.AxFramerControl.DocumentFullName!=null)
            {
                W_Doc = (Word.Document)dsoFramerWordControl1.AxFramerControl.ActiveDocument;
            }
            
        }
        private void FrmLayoutContents_Load(object sender, EventArgs e)
        {
            //更新数据标志
            RefFalg = true;
            //InitModule(layoutUID);
            //

            string path = System.Windows.Forms.Application.StartupPath + "\\BlogData";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (vp.ins != "0" || vp.ins != "0")
            dsoFramerWordControl1.OnFileSaved += new EventHandler(dsoFramerWordControl1_OnFileSaved);
           

            if (vp.upd=="0")
            {
                barEdititem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }

            if (vp.ins == "0")
            {
                barAdd1item.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barAdditem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (vp.del == "0")
                barDelitem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            if (vp.prn == "0")
                barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            ilist = Services.BaseService.GetList("SelectLayoutContentANTLByLayoutID", layoutUID);
            dataTable = DataConverter.ToDataTable(ilist, typeof(LayoutContentANTL));
            treeList1.DataSource = dataTable;
            isstate = true;

            treeList1.MoveFirst();
        }

        void dsoFramerWordControl1_OnFileSaved(object sender, EventArgs e)
        {
            SaveText();
        }


        //结点变化后重新改变word内容
        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            killAllProcess();
            //焦点发生变化时从数据库中提取新的word文件
            if (treeList1.FocusedNode == null)
                return;
            string uid = treeList1.FocusedNode["UID"].ToString();
            LayoutContentANTL obj = Services.BaseService.GetOneByKey<LayoutContentANTL>(uid);
            WaitDialogForm wait=null;
            WordBuilder wb = new WordBuilder();
          
            // public static Word.Bookmarks W_Bkm =


            //if (fb != null)
            //    wb.InsertFromStreamGzip(fb);

            //看一下当前word中的标签
            //改变一下当前记录的word的UID，以备更新标签时使用
            LayoutID = treeList1.FocusedNode["UID"].ToString();


            try
            {
                wait = new WaitDialogForm("", "正在下载数据, 请稍候...");

                if (obj.Contents != null && obj.Contents.Length > 0)
                {
                    if (fb != null)
                    {
                        wb.InsertFromStreamGzip(obj.Contents);
                        dsoFramerWordControl1.FileData = wb.FileData;
                        Staword = wb;
                        
                    }
                    else
                    {
                        dsoFramerWordControl1.FileDataGzip = obj.Contents;
                    }
                }
                else
                {
                    dsoFramerWordControl1.FileNew();
                }
                dsoFramerWordControl1.AxFramerControl.Menubar = true;
                if(dsoFramerWordControl1.AxFramerControl.DocumentFullName!=null)
                {
                 W_Doc = (Word.Document)dsoFramerWordControl1.AxFramerControl.ActiveDocument;
                }
                
                wait.Close();
               
            }
        
            catch (Exception ex)
            {
                wait.Close();
            MessageBox.Show(ex.Message);

            }
        }
        //保存数据
        private void SaveText()
        {
            if (treeList1.FocusedNode == null)
                return;
            string uid = treeList1.FocusedNode["UID"].ToString();
            LayoutContentANTL obj = Services.BaseService.GetOneByKey<LayoutContentANTL>(uid);

            obj.Contents = dsoFramerWordControl1.FileDataGzip;
            WaitDialogForm wait = null;
            try
            {
                wait = new WaitDialogForm("", "正在保存数据, 请稍候...");
                Services.BaseService.Update("UpdateLayoutContentANTLByte", obj);
                wait.Close();
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                wait.Close();
                return;
            }
              
        }



        private void barAdditem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //增加同级目录
            string parentid = "";
            if (treeList1.FocusedNode != null)
            {
                parentid = treeList1.FocusedNode["ParentID"].ToString();
            }


            LayoutContentANTL obj = new LayoutContentANTL();
            obj.UID = obj.UID + "|" + Itop.Client.MIS.ProgUID;
            obj.LayoutID = layoutUID;
            obj.ParentID = parentid;
            obj.CreateDate = DateTime.Now;
            FrmLayoutContentDialogANTL dlg = new FrmLayoutContentDialogANTL();
            dlg.Object = obj;
            dlg.IsCreate = true;

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            dataTable.Rows.Add(DataConverter.ObjectToRow(obj, dataTable.NewRow()));
        }

        private void barAdd1item_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //增加下级目录
            string parentid = "";
            if (treeList1.FocusedNode == null)
                return;
            parentid = treeList1.FocusedNode["UID"].ToString();


            LayoutContentANTL obj = new LayoutContentANTL();
            obj.UID = obj.UID + "|" + Itop.Client.MIS.ProgUID;
            obj.LayoutID = layoutUID;
            obj.ParentID = parentid;
            obj.CreateDate = DateTime.Now;
            FrmLayoutContentDialogANTL dlg = new FrmLayoutContentDialogANTL();
            dlg.Object = obj;
            dlg.IsCreate = true;

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            dataTable.Rows.Add(DataConverter.ObjectToRow(obj, dataTable.NewRow()));
        }

        private void barEdititem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
                return;
            string uid = treeList1.FocusedNode["UID"].ToString();
            LayoutContentANTL obj = Services.BaseService.GetOneByKey<LayoutContentANTL>(uid);

            LayoutContentANTL objCopy = new LayoutContentANTL();
            DataConverter.CopyTo<LayoutContentANTL>(obj, objCopy);

            FrmLayoutContentDialogANTL dlg = new FrmLayoutContentDialogANTL();
            dlg.Object = objCopy;

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            DataConverter.CopyTo<LayoutContentANTL>(objCopy, obj);
            treeList1.FocusedNode.SetValue("ChapterName", obj.ChapterName);
            treeList1.FocusedNode.SetValue("Remark", obj.Remark);
        }

        private void barDelitem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
                return;
            if (treeList1.FocusedNode.Nodes.Count > 0)
            {
                MsgBox.Show("有下级目录，不能删除！");
                return;
            }
            string uid = treeList1.FocusedNode["UID"].ToString();

            //请求确认
            if (MsgBox.ShowYesNo(Strings.SubmitDelete) != DialogResult.Yes)
            {
                return;
            }

            //执行删除操作
            try
            {
                Services.BaseService.DeleteByKey<LayoutContentANTL>(uid);
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return;
            }
            this.treeList1.Nodes.Remove(this.treeList1.FocusedNode);
        }

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            this.Close();
        }


        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                MsgBox.Show("请先建立章节。");
                return;
            }
            dsoFramerWordControl1.FilePrintDialog();
        }


        private bool treeSelect()
        {
            if (treeList1.FocusedNode == null)
            {
                MsgBox.Show("请选择章节！");
                return false;

            }
            else
            {
                return true;
            }
        }


        private void FrmLayoutContents_FormClosing(object sender, FormClosingEventArgs e)
        {


            string path = System.Windows.Forms.Application.StartupPath + "\\BlogData";
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
            catch
            { }
        }


        //导出
        private void barButtonItem26_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                MsgBox.Show("请先建立章节。");
                return;
            }
            try
            {
                dsoFramerWordControl1.DoPageSetupDialog();
            }
            catch { }
        }

        private void barButtonItem29_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
                return;



            string fname = "";
            saveFileDialog1.Filter = "Microsoft word (*.doc)|*.doc";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fname = saveFileDialog1.FileName;

                dsoFramerWordControl1.FileSave(fname,true);
                if (MsgBox.ShowYesNo("导出成功，是否打开该文档？") != DialogResult.Yes)
                    return;
                try
                {
                    System.Diagnostics.Process.Start(fname);
                }
                catch { }

            }

        }

        #region 全部导出

        private void barButtonItem30_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm wait = null;
            WordBuilder wb = new WordBuilder();
            //wb.InsertFromFile("Very.doc");

            
            //TONLI.BZH.UI.DSOFramerWordControl wb = new DSOFramerWordControl();
            try
            {
                wait = new WaitDialogForm("", "正在导出, 请稍候...");

                
                IList<LayoutContentANTL> ls = Services.BaseService.GetList<LayoutContentANTL>("SelectLayoutContentANTLByLayoutIDBlogData", layoutUID);
                System.Data.DataTable dts = DataConverter.ToDataTable((IList)ls, typeof(LayoutContentANTL));

                //IList<byte[]> lbt = new List<byte[]>();
                IList<LayoutContentANTL> lbt = new List<LayoutContentANTL>();
                
                InitExe("", dts, lbt);
                GetTop(layoutUID + "|1", wb);

                object obj = "标题 1";

                Style testStyle = wb.wordApp.Application.ActiveDocument.Styles.get_Item(ref obj);
                object listObject = testStyle;
                wb.wordApp.Selection.set_Style(ref listObject);
                InitAdd(lbt, wb);
                GetTop(layoutUID + "|2", wb);
            }
            catch
            {
                wb.Dispose();
                wait.Close();
                MsgBox.Show("导出失败");
                return;
            }
            wait.Close();

            string fname = "";
            saveFileDialog1.Filter = "Microsoft word (*.doc)|*.doc";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                fname = saveFileDialog1.FileName;
                wb.Save(fname, true);
                wb.Dispose();
                if (MsgBox.ShowYesNo("导出成功，是否打开该文档？") != DialogResult.Yes)
                    return;
                
                    System.Diagnostics.Process.Start(fname);
                }
                catch
                {
                    MsgBox.Show("该文件已经打开或文件名错误！");
                }
            }
            else
            {
            
            }
        }

        private void InitAdd(IList<LayoutContentANTL> ls, WordBuilder tx2)
        {
            foreach (LayoutContentANTL bs in ls)
            {
               
                try
                {
                    //注释部分为按章节名填加文档标题部分
                    //tx2.wordApp.Selection.TypeText(bs.ChapterName+"\r\n");
                    //object obj = "标题 1";
                    //Style testStyle = tx2.wordApp.Application.ActiveDocument.Styles.get_Item(ref obj);
                    //object listObject = testStyle;
                    //tx2.wordApp.Selection.set_Style(ref listObject);              
                    tx2.InsertFromStreamGzip(bs.Contents);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }

            }
        }


        private void InitExe(string parentid, System.Data.DataTable dts, IList<LayoutContentANTL> ls)
        {
            DataRow[] rows = dts.Select(string.Format("parentid='{0}'", parentid));

            foreach (DataRow row in rows)
            {
                LayoutContentANTL lc = new LayoutContentANTL();
                lc.ChapterName = row["ChapterName"].ToString();
                try
                {
                    if (row["Contents"] != DBNull.Value)
                    {
                        byte[] bt = null;
                        try { bt = (byte[])row["Contents"]; }
                        catch { }
                        if (bt != null)
                        {
                            //ls.Add(bt);
                            lc.Contents = bt;
                        }
                    }

                }
                catch (Exception ex)
                {
                }
                ls.Add(lc);
                InitExe(row["UID"].ToString(), dts, ls);
            }
        }



        private void InitAdd(IList<byte[]> ls, WordBuilder tx2)
        {
            foreach (byte[] bs in ls)
            {
                try
                {

                    tx2.InsertFromStreamGzip(bs);
                }
                catch
                {

                }

            }
        }


        private void InitExe(string parentid, System.Data.DataTable dts, IList<byte[]> ls)
        {
            DataRow[] rows = dts.Select(string.Format("parentid='{0}'", parentid));

            foreach (DataRow row in rows)
            {

                try
                {
                    if (row["Contents"] != DBNull.Value)
                    {
                        byte[] bt = null;
                        try { bt = (byte[])row["Contents"]; }
                        catch { }
                        if (bt != null)
                        {
                            ls.Add(bt);
                        }
                    }

                }
                catch (Exception ex)
                {
                }
                InitExe(row["UID"].ToString(), dts, ls);
            }
        }




        public void GetTop(string id, WordBuilder tx2)
        {
            byte[] vi = null;
            try
            {
                LayoutType lt1 = Services.BaseService.GetOneByKey<LayoutType>(id);
                if (lt1 != null)
                {
                    vi = lt1.ExcelData;
                    tx2.InsertFromStreamGzip(vi);
                }
            }
            catch { }
        }

        private void InitAdd(IList<LayoutContentANTL> ls, TONLI.BZH.UI.DSOFramerWordControl tx2)
        {
            foreach (LayoutContentANTL bs in ls)
            {




                try
                {
                    System.Drawing.Font font = new System.Drawing.Font("宋体", 16);
                    tx2.DoInsert("", font, TONLI.BZH.UI.WdParagraphAlignment.Left);
                    tx2.DoInsert("", font, TONLI.BZH.UI.WdParagraphAlignment.Left);
                    font = new System.Drawing.Font("宋体", 32);
                    tx2.DoInsert(bs.ChapterName, font, TONLI.BZH.UI.WdParagraphAlignment.Left);
                    tx2.DoInsert("", font, TONLI.BZH.UI.WdParagraphAlignment.Left);
                    font = new System.Drawing.Font("宋体", 16);
                    tx2.DoAppendFromStream(bs.Contents);
                }
                catch
                {

                }

            }
        }


        public void GetTop(string id, TONLI.BZH.UI.DSOFramerWordControl tx2)
        {
            byte[] vi = null;
            try
            {
                LayoutType lt1 = Services.BaseService.GetOneByKey<LayoutType>(id);
                if (lt1 != null)
                {
                    vi = lt1.ExcelData;
                    tx2.DoAppendFromStream(vi);
                }
            }
            catch { }
        }



        #endregion


        private void barButtonItem44_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormLayoutTypeANTL flt = new FormLayoutTypeANTL();
            flt.Type = layoutUID + "|1";
            flt.FLC = this;
            flt.ShowDialog();
        }


       
        public void InitModule(string id)
        {
            try
            {
                //dsoFramerWordControl1.FileNew();
                LayoutType lt1 = Services.BaseService.GetOneByKey<LayoutType>(id);
                if (lt1 == null)
                {
                    lt1 = new LayoutType();
                    LayoutType lt2 = Services.BaseService.GetOneByKey<LayoutType>("LayoutModule");
                    lt1.UID = id;
                    Services.BaseService.Create<LayoutType>(lt1);
                }
                fb = lt1.ExcelData;
            }
            catch { }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormLayoutTypeANTL flt = new FormLayoutTypeANTL();
            flt.Type = layoutUID + "|2";
            flt.FLC = this;
            flt.ShowDialog();
        }
         
        //private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    if (treeList1.Nodes.Count>0)
        //    {
        //        FrmGHBGdata fgh = new FrmGHBGdata();
        //        fgh.ShowDialog();
        //        if (fgh.DialogResult==DialogResult.OK)
        //        {
        //            string year = fgh.GHBGyear;
        //            MessageBox.Show("选择的规划报告年份是：" + fgh.GHBGyear);
        //           // RefrehDataAll(year);
        //            W_Doc = (Word.Document)dsoFramerWordControl1.AxFramerControl.ActiveDocument;
        //            W_Bkm = W_Doc.Bookmarks;
        //            Refreh_Chapter2(year,W_Bkm,"手动");
        //        }
        //    }
        //}

       private void  RefrehDataAll(string year,string type,string liexing)
       {
           //每个标题遍历一遍
           for (int i = 0; i < treeList1.Nodes.Count; i++)
           {
               if (RefFalg==false)
               {
                   return;
               }
               //遍历一类标题
               treeList1.FocusedNode = treeList1.Nodes[i];
               W_Doc = (Word.Document)dsoFramerWordControl1.AxFramerControl.ActiveDocument;
               W_Bkm = W_Doc.Bookmarks;
               string chaptername=treeList1.FocusedNode["ChapterName"].ToString();
               Change_Data_byChapter(chaptername, year, W_Bkm, type,liexing);
               if (treeList1.Nodes[i].Nodes.Count > 0)
               {
                   //遍历二类标题
                   for (int k = 0; k < treeList1.Nodes[i].Nodes.Count; k++)
                   {
                       if (RefFalg == false)
                       {
                           return;
                       }
                       treeList1.FocusedNode = treeList1.Nodes[i].Nodes[k];
                       W_Doc = (Word.Document)dsoFramerWordControl1.AxFramerControl.ActiveDocument;
                       W_Bkm = W_Doc.Bookmarks;
                       chaptername = treeList1.FocusedNode["ChapterName"].ToString();
                       Change_Data_byChapter(chaptername, year, W_Bkm, type, liexing);
                   }
               }
           }
           if (type == "自动")
	        {
                MessageBox.Show("已完成更新！");
	        }
         
       }
        //更新标签值自动/手动 type="自动"或者"手动"
       private void BookMark_RefreaDat(Word.Bookmarks BM, MKData objMKD,string type)
       {

           if (objMKD.Data.Count > 0)
           {
               if (type=="自动")
               {
                   for (int i = 0; i < objMKD.Data.Count; i++)
                   {
                       object mkname = objMKD.Data[i].BQname.ToString();
                       //存在标签则选择并更新值
                       if ( BM.Exists(mkname.ToString()))
                       {
                           Word.Range tmpRng = BM.get_Item(ref mkname).Range;
                           //有新值则更新
                           if (objMKD.Data[i].BQdata.ToString() != "保持原值")
                           {
                               tmpRng.Text = objMKD.Data[i].BQdata.ToString();
                               object oRng = tmpRng;
                               BM.Add(mkname.ToString(), ref oRng);
                           }
                           //没有查到新值则保持原值并用红色标出
                           else
                           {
                               tmpRng.Font.Color = Word.WdColor.wdColorRed;
                           }
                       }
                   }
                   
                   //保存
                   SaveData_Word();
                   //更新书签库
                   string conn = "LayoutID='" + LayoutID + "' order by StartP";
                   IList<LayoutBookMark> marklist = Common.Services.BaseService.GetList<LayoutBookMark>("SelectLayoutBookMarkList", conn);
                   for (int i = 0; i < marklist.Count; i++)
                   {
                       if (BM.Exists(marklist[i].UID.ToString()))
	                    {
                           object tempid=marklist[i].UID.ToString();
                    	   Word.Bookmark tempmk = BM.get_Item(ref tempid);
                           if (tempmk.Range.Text.Length>200)
                           {
                               marklist[i].MarkText = tempmk.Range.Text.Substring(0, 200);
                           }
                           else
                            {
                                marklist[i].MarkText=tempmk.Range.Text;
                            }
                           
                           marklist[i].StartP=tempmk.Start;
                           //更新数据库
                           Common.Services.BaseService.Update<LayoutBookMark>(marklist[i]);
	                    }
                   }   
               }
           }
               if (type=="手动")
               {
                   string conn = "LayoutID='" + LayoutID + "' order by StartP";
                   IList<LayoutBookMark> templist = Common.Services.BaseService.GetList<LayoutBookMark>("SelectLayoutBookMarkList", conn);
                   if (templist.Count==0)
                   {
                       return;
                   }
                   Frm_FindandChangeTL fft = new Frm_FindandChangeTL();
                   fft.W_Bkm = BM;
                   fft.mkd = objMKD;
                   fft.layoutID = LayoutID;
                   fft.ShowDialog();
                   if (fft.DialogResult==DialogResult.OK)
                   {
                       //更新书签库
                       string connstr = "LayoutID='" + LayoutID + "' order by StartP";
                       IList<LayoutBookMark> marklist = Common.Services.BaseService.GetList<LayoutBookMark>("SelectLayoutBookMarkList", connstr);
                       for (int i = 0; i < marklist.Count; i++)
                       {
                           if (BM.Exists(marklist[i].UID.ToString()))
                           {
                               object tempid = marklist[i].UID.ToString();
                               Word.Bookmark tempmk = BM.get_Item(ref tempid);
                               marklist[i].MarkText = tempmk.Range.Text;
                               marklist[i].StartP = tempmk.Start;
                               //更新数据库
                               Common.Services.BaseService.Update<LayoutBookMark>(marklist[i]);
                           }
                       }
                       //保存
                       SaveData_Word();
                   }
                   if (MessageBox.Show(objMKD.ZJname + "更新完成！继续或取消？", "询问", MessageBoxButtons.YesNo) == DialogResult.No)
                   {
                       RefFalg = false;
                   }
                  
               }
               

           
       }
        //Word文档变化后要时时更新数据
        public void SaveData_Word()
        {
            //更新后保存数据
            dsoFramerWordControl1.AxFramerControl.Save();
            SaveText();
        }
        //根据章节执行相应的操作
        private void Change_Data_byChapter(string chapter,string year,Word.Bookmarks W_bkm,string type,string leixing)
        {
            MKData mk_chaptertemp = new MKData();
            if (leixing=="配网")
            {
                switch (chapter)
                {
                    case "第一章":
                        Refreh_P_Chapter1(year, W_bkm, type);
                        break;
                    case "第二章":
                        Refreh_P_Chapter2(year, W_bkm, type);
                        break;
                    case "3.1":
                        Refreh_P_Chapter31(year, W_bkm, type);
                        break;
                    case "3.2":
                        Refreh_P_Chapter32(year, W_bkm, type);
                        break;
                    case "4.2":
                        Refreh_P_Chapter42(year, W_bkm, type);
                        break;
                    case "4.7":
                        Refreh_P_Chapter47(year, W_bkm, type);
                        break;
                    case "6.2":
                        Refreh_P_Chapter62(year, W_bkm, type);
                        break;
                    case "6.3":
                        Refreh_P_Chapter63(year, W_bkm, type);
                        break;
                    default:
                        //执行更新操作
                        BookMark_RefreaDat(W_bkm, mk_chaptertemp, type);
                        break;
                }
            }
            if (leixing == "农网")
            {
                switch (chapter)
                {
                    case "第二章":
                        Refreh_N_Chapter2(year, W_bkm, type);
                        break;
                    case "第三章":
                        Refreh_N_Chapter3(year, W_bkm, type);
                        break;
                    //case "3.1":
                    //    Refreh_P_Chapter31(year, W_bkm, type);
                    //    break;
                    //case "3.2":
                    //    Refreh_P_Chapter32(year, W_bkm, type);
                    //    break;
                    //case "4.2":
                    //    Refreh_P_Chapter42(year, W_bkm, type);
                    //    break;
                    //case "4.7":
                    //    Refreh_P_Chapter47(year, W_bkm, type);
                    //    break;
                    //case "6.2":
                    //    Refreh_P_Chapter62(year, W_bkm, type);
                    //    break;
                    //case "6.3":
                    //    Refreh_P_Chapter63(year, W_bkm, type);
                    //    break;
                    default:
                        //执行更新操作
                        BookMark_RefreaDat(W_bkm, mk_chaptertemp, type);
                        break;
                }
            }
            if (leixing == "其它")
            {
             //执行更新操作
                BookMark_RefreaDat(W_bkm, mk_chaptertemp, type);
            }
            
        }

        #region //配网处理第一章标签数据

        private void Refreh_P_Chapter1(string year, Word.Bookmarks W_bkm, string type)
        {
            string old_value = "保持原值";
            MKData mk_chapter1 = new MKData();
            mk_chapter1.ZJname = "第一章";

            BQData CP1_data1 = new BQData();
            CP1_data1.BQname = "B_M_0853d43c_e721_4a11_806f_992fab74c355";
            CP1_data1.BQdata = "测试值";
            mk_chapter1.Data.Add(CP1_data1);

            //执行更新操作
            BookMark_RefreaDat(W_bkm, mk_chapter1, type);
        }
        #endregion

        #region //配网处理第二章标签数据

        private void Refreh_P_Chapter2(string year, Word.Bookmarks W_bkm, string type)
        {
            int newyear = int.Parse(year) - 1;
            string old_value = "保持原值";
            MKData mk_chapter2 = new MKData();
            mk_chapter2.ZJname = "第二章";
            #region //城区面积标签
            BQData CP2_bq1 = new BQData();
            CP2_bq1.BQname = "B_M_7e135441_97ed_46be_9a4b_d2aab0ad3f9b";
            string tiaojian = "";
            tiaojian = " a.ProjectID='" + ProjID + "' and a.Area='铜陵市' and b.Yearf=" + newyear;
            if (Common.Services.BaseService.GetObject("SelectAreaDataCityArea_ByArea", tiaojian) != null)
            {
                CP2_bq1.BQdata = ((double)Common.Services.BaseService.GetObject("SelectAreaDataCityArea_ByArea", tiaojian)).ToString();
            }
            else
            {
                CP2_bq1.BQdata = old_value;
            }
            mk_chapter2.Data.Add(CP2_bq1);
            #endregion
            #region //人口年份标签
            BQData CP2_bq2 = new BQData();
            CP2_bq2.BQname = "B_M_0aad4d05_6cd0_483c_bc69_ef287b8783da";
            CP2_bq2.BQdata = newyear.ToString();
            mk_chapter2.Data.Add(CP2_bq2);
            #endregion


            #region  //全市人口标签
            //查铜陵市人口在Ps_Table_AreaData表中
            BQData CP2_bq3 = new BQData();
            CP2_bq3.BQname = "B_M_e037cd16_95be_43e9_bd33_874cffe66327";
            tiaojian = " a.ProjectID='" + ProjID + "' and a.Area='铜陵市' and b.Yearf=" + newyear;
            if (Common.Services.BaseService.GetObject("SelectAreaDataPopulation_ByArea", tiaojian) != null)
            {
                CP2_bq3.BQdata = ((double)Common.Services.BaseService.GetObject("SelectAreaDataPopulation_ByArea", tiaojian)).ToString();
            }
            else
            {
                CP2_bq3.BQdata = old_value;
            }
            mk_chapter2.Data.Add(CP2_bq3);
            #endregion
            #region //城域人口标签
            //查非铜陵市人口在Ps_Table_AreaData表中,用总人口减去非铜陵市人口=城域人口
            BQData CP2_bq4 = new BQData();
            CP2_bq4.BQname = "B_M_0a373bd0_619d_4e36_a5b1_0853c4527bbf";
            tiaojian = " a.ProjectID='" + ProjID + "' and a.Area!='铜陵市' and a.Area not like '%部地区'  and b.Yearf=" + newyear;
            if (Common.Services.BaseService.GetObject("SelectAreaDataPopulation_notcity_ByArea", tiaojian) != null)
            {
                CP2_bq4.BQdata = ((double)Common.Services.BaseService.GetObject("SelectAreaDataPopulation_notcity_ByArea", tiaojian)).ToString();
            }
            else
            {
                CP2_bq4.BQdata = old_value;
            }
            //如果全市人口或县区人口有一个没有数据则不用计算
            if (CP2_bq4.BQdata != old_value && CP2_bq3.BQdata != old_value)
            {
                double tempdb = double.Parse(CP2_bq3.BQdata.ToString()) - double.Parse(CP2_bq4.BQdata.ToString());
                CP2_bq4.BQdata = tempdb.ToString();
            }
            else
            {
                CP2_bq4.BQdata = old_value;
            }
            mk_chapter2.Data.Add(CP2_bq4);
            #endregion
            //
            #region //城域人口占全市人口比例
            BQData CP2_bq5 = new BQData();
            CP2_bq5.BQname = "B_M_9a4de2ea_1829_4470_8de7_117ed49c2640";
            if (CP2_bq4.BQdata != old_value && CP2_bq3.BQdata != old_value)
            {
                CP2_bq5.BQdata = (double.Parse(CP2_bq4.BQdata.ToString()) / double.Parse(CP2_bq3.BQdata.ToString())) * 100 + "%";
            }
            else
            {
                CP2_bq5.BQdata = old_value;
            }
            mk_chapter2.Data.Add(CP2_bq5);
            #endregion

            #region //GDP年份标签
            BQData CP2_bq6 = new BQData();
            CP2_bq6.BQname = "B_M_9afd4b9e_55b9_4d7a_aaa0_6cd6bcd47e74";
            CP2_bq6.BQdata = newyear.ToString();

            mk_chapter2.Data.Add(CP2_bq6);
            #endregion

            #region //GDP总值标签
            BQData CP2_bq7 = new BQData();
            CP2_bq7.BQname = "B_M_e23fa137_4dec_4d2d_b94c_f96016f312ef";
            tiaojian = "select  y" + newyear.ToString() + " from Ps_History where Title='全地区GDP（亿元）'  and Forecast=1 and Col4='" + ProjID + "'";
            if (Common.Services.BaseService.GetObject("SelectPs_HistoryPopulationByCondition", tiaojian) != null)
            {
                CP2_bq7.BQdata = ((double)Common.Services.BaseService.GetObject("SelectPs_HistoryPopulationByCondition", tiaojian)).ToString();
            }
            else
            {
                CP2_bq7.BQdata = old_value;
            }
            mk_chapter2.Data.Add(CP2_bq7);
            #endregion

            #region //GDP工业值标签
            BQData CP2_bq8 = new BQData();
            CP2_bq8.BQname = "B_M_c1470a2b_ac6e_4d8a_833d_1ffea2ccadc4";

            tiaojian = "select  b.y" + newyear + " from Ps_History as a ,Ps_History as b  where a.Title='全地区GDP（亿元）'  and a.Forecast=1 and a.Col4='" + ProjID + "' and a.ID=b.ParentID and b.Title='二产'";
            if (Common.Services.BaseService.GetObject("SelectPs_HistoryPopulationByCondition", tiaojian) != null)
            {
                CP2_bq8.BQdata = ((double)Common.Services.BaseService.GetObject("SelectPs_HistoryPopulationByCondition", tiaojian)).ToString();
            }
            else
            {
                CP2_bq8.BQdata = old_value;
            }
            mk_chapter2.Data.Add(CP2_bq8);
            #endregion

            #region //GDP工业值占总GDP的比重标签
            BQData CP2_bq9 = new BQData();
            CP2_bq9.BQname = "B_M_79eb2370_f77f_4e96_885e_a7a1de9e5dc9";
            if (CP2_bq7.BQdata != old_value && CP2_bq8.BQdata != old_value)
            {
                CP2_bq9.BQdata = Math.Round(100 * double.Parse(CP2_bq8.BQdata.ToString()) / double.Parse(CP2_bq7.BQdata.ToString()), 2) + "%";
            }
            else
            {
                CP2_bq9.BQdata = old_value;
            }
            mk_chapter2.Data.Add(CP2_bq9);
            #endregion

            #region //年份标签
            BQData CP2_bq10 = new BQData();
            CP2_bq10.BQname = "B_M_88c7cc6a_9945_4daa_aa71_62bc5a30a7e2";
            CP2_bq10.BQdata = newyear.ToString();
            mk_chapter2.Data.Add(CP2_bq10);
            #endregion

            #region //国内生产总值
            BQData CP2_bq11 = new BQData();
            CP2_bq11.BQname = "B_M_82b09a86_6fb8_44a2_b143_ab6f669dfe95";
            CP2_bq11.BQdata = CP2_bq7.BQdata;
            mk_chapter2.Data.Add(CP2_bq11);
            #endregion

            #region //国内生产总值比上年同期增长
            BQData CP2_bq12 = new BQData();
            CP2_bq12.BQname = "B_M_1db524bf_d798_496a_99cb_8dd3c6d9a0af";
            tiaojian = "select  y" + (newyear - 1) + " from Ps_History where Title='全地区GDP（亿元）'  and Forecast=1 and Col4='" + ProjID + "'";
            if (Common.Services.BaseService.GetObject("SelectPs_HistoryPopulationByCondition", tiaojian) != null)
            {
                double tempdb12 = ((double)Common.Services.BaseService.GetObject("SelectPs_HistoryPopulationByCondition", tiaojian));
                if (CP2_bq7.BQdata != old_value)
                {
                    CP2_bq12.BQdata = Math.Round(100 * (double.Parse(CP2_bq7.BQdata.ToString()) - tempdb12) / tempdb12, 2) + "";
                }
            }
            else
            {
                CP2_bq12.BQdata = old_value;
            }
            mk_chapter2.Data.Add(CP2_bq12);
            #endregion

            #region //年份
            BQData CP2_bq13 = new BQData();
            CP2_bq13.BQname = "B_M_863b8846_224e_424c_bc77_cd838e7e8721";
            CP2_bq13.BQdata = newyear.ToString();
            mk_chapter2.Data.Add(CP2_bq13);
            #endregion

            #region //年份
            BQData CP2_bq21 = new BQData();
            CP2_bq21.BQname = "B_M_369af6e5_06a0_4e58_b818_1448e2dd5ec7";
            CP2_bq21.BQdata = newyear.ToString();
            mk_chapter2.Data.Add(CP2_bq21);
            #endregion

            #region //年份
            BQData CP2_bq14 = new BQData();
            CP2_bq14.BQname = "B_M_18d8b106_2779_4c70_8fbf_3a53c4a46c1c";
            CP2_bq14.BQdata = newyear.ToString();
            mk_chapter2.Data.Add(CP2_bq14);
            #endregion

            #region //年份
            BQData CP2_bq15 = new BQData();
            CP2_bq15.BQname = "B_M_faa6c0e3_78d6_4942_88c4_384066f8466a";
            CP2_bq15.BQdata = newyear.ToString();
            mk_chapter2.Data.Add(CP2_bq15);
            #endregion
            #region //年份
            BQData CP2_bq16 = new BQData();
            CP2_bq16.BQname = "B_M_745905b2_e72f_4775_9899_695553815690";
            CP2_bq16.BQdata = newyear.ToString();
            mk_chapter2.Data.Add(CP2_bq16);
            #endregion
            #region //年份
            BQData CP2_bq17 = new BQData();
            CP2_bq17.BQname = "B_M_211f0bc9_38c3_4149_bd9a_f59c74032f93";
            CP2_bq17.BQdata = newyear.ToString();
            mk_chapter2.Data.Add(CP2_bq17);
            #endregion
            #region //年份
            BQData CP2_bq18 = new BQData();
            CP2_bq18.BQname = "B_M_17a059f8_8a86_481d_92cf_d9f6c747a45c";
            CP2_bq18.BQdata = newyear.ToString();
            mk_chapter2.Data.Add(CP2_bq18);
            #endregion
            #region //年份
            BQData CP2_bq19 = new BQData();
            CP2_bq19.BQname = "B_M_ab41934d_cfd8_4dca_a29d_5850f6de93b5";
            CP2_bq19.BQdata = newyear.ToString();
            mk_chapter2.Data.Add(CP2_bq19);
             #endregion
            #region //年份
            BQData CP2_bq20 = new BQData();
            CP2_bq20.BQname = "B_M_b8a0cb80_8955_478b_bd74_61fb9290ffef";
            CP2_bq20.BQdata = newyear.ToString();
            mk_chapter2.Data.Add(CP2_bq20);
             #endregion
            //下接22
            //执行更新操作
            BookMark_RefreaDat(W_bkm, mk_chapter2, type);
        }

        #endregion

        #region //配网处理3.1标签数据
        private void Refreh_P_Chapter31(string year, Word.Bookmarks W_bkm, string type)
        {
            int newyear = int.Parse(year) - 1;
            string old_value = "保持原值";
            MKData mk_chapter31 = new MKData();
            mk_chapter31.ZJname = "3.1";
            #region //报告年份标签
            BQData CP31_bq1 = new BQData();
            CP31_bq1.BQname = "B_M_53c447f1_da55_407a_8998_ee37a3ebdc94";
            CP31_bq1.BQdata = year;
            mk_chapter31.Data.Add(CP31_bq1);
            #endregion
            #region //新建变电站标签
            BQData CP31_bq2 = new BQData();
            CP31_bq2.BQname = "B_M_d5358e04_92fe_4456_a1c2_170158391e8b";
            string tiaojian = "";
            tiaojian = " a.ProjectID='" + ProjID + "' and b.Typeqf='sub' and Cast(a.BuildYear as int )>=" + year + "  and a.ID=b.ProjectID order by Cast(a.BuildYear as int )";
            IList<Ps_Table_TZGS> pttlist = Common.Services.BaseService.GetList<Ps_Table_TZGS>("SelectPs_Table_TZGSListBYwhere", tiaojian);
            if (pttlist.Count > 0)
            {
                for (int i = 0; i < pttlist.Count; i++)
                {
                    CP31_bq2.BQdata += pttlist[i].BuildYear + "年" + pttlist[i].Col3 + pttlist[i].Col1 + ",";
                }
                if (CP31_bq2.BQdata.ToString().Length > 0)
                {
                    CP31_bq2.BQdata = CP31_bq2.BQdata.ToString().Substring(0, CP31_bq2.BQdata.ToString().LastIndexOf(",") - 1);
                    CP31_bq2.BQdata += "。";
                }
            }
            else
            {
                CP31_bq2.BQdata = old_value;
            }
            mk_chapter31.Data.Add(CP31_bq2);
            #endregion
            #region //报告年份标签
            BQData CP31_bq3 = new BQData();
            CP31_bq3.BQname = "B_M_e238cb33_67fd_48ef_84cc_c8e8fca503d0";
            CP31_bq3.BQdata = newyear.ToString();
            mk_chapter31.Data.Add(CP31_bq3);
            #endregion
            //执行更新操作
            BookMark_RefreaDat(W_bkm, mk_chapter31, type);
        }
        #endregion

        #region //配网处理3.2标签数据
        private void Refreh_P_Chapter32(string year, Word.Bookmarks W_bkm, string type)
        {
            int newyear = int.Parse(year) - 1;
            string old_value = "保持原值";
            string tiaojian = "";
            MKData mk_chapter32 = new MKData();
            mk_chapter32.ZJname = "3.2";
            #region //报告年份标签
            BQData CP32_bq1 = new BQData();
            CP32_bq1.BQname = "B_M_da859e24_e3fe_4aef_bf17_44c2de39fb8e";
            CP32_bq1.BQdata = year;
            mk_chapter32.Data.Add(CP32_bq1);
            #endregion
            #region //报告年份标签
            BQData CP32_bq2 = new BQData();
            CP32_bq2.BQname = "B_M_8e6a49cb_6d9b_44e5_ac97_f75e4b032875";
            CP32_bq2.BQdata = year;
            mk_chapter32.Data.Add(CP32_bq2);
            #endregion
            #region //110kv及以上变电站座数
            BQData CP32_bq3 = new BQData();
            CP32_bq3.BQname = "B_M_69e46e67_d89f_4039_9b88_52a4958018a9";
            tiaojian = " Flag='1' and L1>=110 and AreaID='" + ProjID + "' and cast(S2 as int)<=" + year;

            if (Common.Services.BaseService.GetObject("SelectPSP_Substation_InfoCountall", tiaojian) != null)
            {
                CP32_bq3.BQdata = Common.Services.BaseService.GetObject("SelectPSP_Substation_InfoCountall", tiaojian).ToString();
            }
            else
            {
                CP32_bq3.BQdata = old_value;
            }
            mk_chapter32.Data.Add(CP32_bq3);
            #endregion
            #region //110kv及以上变电站总容量
            BQData CP32_bq4 = new BQData();
            CP32_bq4.BQname = "B_M_322b0bff_2663_48fa_883a_d188bed02933";
            tiaojian = " Flag='1' and L1>=110 and AreaID='" + ProjID + "' and cast(S2 as int)<=" + year;

            if (Common.Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", tiaojian) != null)
            {
                CP32_bq4.BQdata = Common.Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", tiaojian).ToString();
            }
            else
            {
                CP32_bq4.BQdata = old_value;
            }
            mk_chapter32.Data.Add(CP32_bq4);
            #endregion
            #region //220kv变电站座数
            BQData CP32_bq5 = new BQData();
            CP32_bq5.BQname = "B_M_3d99ab43_a1fc_4f0d_a68b_52ac29ceeff7";
            tiaojian = " Flag='1' and L1=220 and AreaID='" + ProjID + "' and cast(S2 as int)<=" + year;

            if (Common.Services.BaseService.GetObject("SelectPSP_Substation_InfoCountall", tiaojian) != null)
            {
                CP32_bq5.BQdata = Common.Services.BaseService.GetObject("SelectPSP_Substation_InfoCountall", tiaojian).ToString();
            }
            else
            {
                CP32_bq5.BQdata = old_value;
            }
            mk_chapter32.Data.Add(CP32_bq5);
             #endregion
            #region //220kv变电站情况
            BQData CP32_bq6 = new BQData();
            CP32_bq6.BQname = "B_M_cc006291_3a3a_4379_bd5b_dcc8e4281007";
            tiaojian = " Flag='1' and L1=220 and AreaID='" + ProjID + "' and cast(S2 as int)<=" + year;
            IList<PSP_Substation_Info> psilist = Common.Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", tiaojian);
            if (psilist.Count > 0)
            {
                int sum = 0;
                double volum = 0;
                CP32_bq6.BQdata = "即";
                for (int i = 0; i < psilist.Count; i++)
                {
                    CP32_bq6.BQdata += psilist[i].Title + "(" + psilist[i].L4 + "MVA)、";
                    //求主变台数和
                    sum += psilist[i].L3;
                    //总变电容量
                    volum += psilist[i].L2;
                }
                CP32_bq6.BQdata = CP32_bq6.BQdata.ToString().Substring(0, CP32_bq6.BQdata.ToString().LastIndexOf("、")) + ",主变" + sum + "台，220千伏总变电容量为" + volum + "MVA。";
            }
            else
            {
                CP32_bq6.BQdata = old_value;
            }
            mk_chapter32.Data.Add(CP32_bq6);
             #endregion

            #region //110kv公用变电站座数
            BQData CP32_bq7 = new BQData();
            CP32_bq7.BQname = "B_M_f6226296_fca6_4b15_a3d4_c971ee93dd4a";
            tiaojian = " Flag='1' and L1=110 and S4='公用' and AreaID='" + ProjID + "' and cast(S2 as int)<=" + year;

            if (Common.Services.BaseService.GetObject("SelectPSP_Substation_InfoCountall", tiaojian) != null)
            {
                CP32_bq7.BQdata = Common.Services.BaseService.GetObject("SelectPSP_Substation_InfoCountall", tiaojian).ToString();
            }
            else
            {
                CP32_bq7.BQdata = old_value;
            }
            mk_chapter32.Data.Add(CP32_bq7);
            #endregion
            #region //110kv公用变电站情况
            BQData CP32_bq8 = new BQData();
            CP32_bq8.BQname = "B_M_81f3ab93_3e66_4bcb_8a2b_8d026a5eb350";
            tiaojian = " Flag='1' and L1=110 and S4='公用' and AreaID='" + ProjID + "' and cast(S2 as int)<=" + year;
            psilist = Common.Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", tiaojian);
            if (psilist.Count > 0)
            {
                int sum = 0;
                double volum = 0;
                CP32_bq8.BQdata = "即";
                for (int i = 0; i < psilist.Count; i++)
                {
                    CP32_bq8.BQdata += psilist[i].Title + "(" + psilist[i].L4 + "MVA)、";
                    //求主变台数和
                    sum += psilist[i].L3;
                    //总变电容量
                    volum += psilist[i].L2;
                }
                CP32_bq8.BQdata = "主变" + sum + "台，主变总容量" + volum + "MVA，" + CP32_bq8.BQdata.ToString().Substring(0, CP32_bq8.BQdata.ToString().LastIndexOf("、")) + ";";
            }
            else
            {
                CP32_bq8.BQdata = old_value;
            }
            mk_chapter32.Data.Add(CP32_bq8);
            #endregion
            #region //110kv专用变电站情况
            BQData CP32_bq9 = new BQData();
            CP32_bq9.BQname = "B_M_eee02e7b_395d_44ca_bb43_a6841578bf28";
            tiaojian = " Flag='1' and L1=110 and S4='专用' and AreaID='" + ProjID + "' and cast(S2 as int)<=" + year;
            psilist = Common.Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", tiaojian);
            if (psilist.Count > 0)
            {

                double volum = 0;
                for (int i = 0; i < psilist.Count; i++)
                {
                    //总变电容量
                    volum += psilist[i].L2;
                }
                CP32_bq9.BQdata = "用户变" + psilist.Count + "座，主变总容量" + volum + "MVA。";
            }
            else
            {
                CP32_bq9.BQdata = old_value;
            }
            mk_chapter32.Data.Add(CP32_bq9);
            #endregion
            #region //35kv变电站座数
            BQData CP32_bq10 = new BQData();
            CP32_bq10.BQname = "B_M_62b80c14_a114_4990_a819_0787a7c4ddbd";
            tiaojian = " Flag='1' and L1=35 and AreaID='" + ProjID + "' and cast(S2 as int)<=" + year;

            if (Common.Services.BaseService.GetObject("SelectPSP_Substation_InfoCountall", tiaojian) != null)
            {
                CP32_bq10.BQdata = Common.Services.BaseService.GetObject("SelectPSP_Substation_InfoCountall", tiaojian).ToString();
            }
            else
            {
                CP32_bq10.BQdata = old_value;
            }
            mk_chapter32.Data.Add(CP32_bq10);
            #endregion
            #region //35kv变电站情况
            BQData CP32_bq11 = new BQData();
            CP32_bq11.BQname = "B_M_9fc1af03_1e42_499f_9de7_4b9c0622b18b";
            tiaojian = " Flag='1' and L1=35 and AreaID='" + ProjID + "' and cast(S2 as int)<=" + year;
            psilist = Common.Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", tiaojian);
            if (psilist.Count > 0)
            {
                int sum = 0;
                double volum = 0;
                CP32_bq11.BQdata = "即";
                for (int i = 0; i < psilist.Count; i++)
                {
                    CP32_bq11.BQdata += psilist[i].Title + "(" + psilist[i].L4 + "MVA)、";
                    //求主变台数和
                    sum += psilist[i].L3;
                    //总变电容量
                    volum += psilist[i].L2;
                }
                CP32_bq11.BQdata = sum + "台主变，变电容量" + volum + "MVA。" + CP32_bq11.BQdata.ToString().Substring(0, CP32_bq11.BQdata.ToString().LastIndexOf("、")) + ";";
            }
            else
            {
                CP32_bq11.BQdata = old_value;
            }
            mk_chapter32.Data.Add(CP32_bq11);
             #endregion

            #region //35kv线路情况
            BQData CP32_bq12 = new BQData();
            CP32_bq12.BQname = "B_M_00fabcf9_0efa_406b_9dfd_0508593877f5";
            tiaojian = " Type='05' and RateVolt=35 and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime))<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian) != null)
            {
                int sum = (int)Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian);
                double length = 0;
                if (Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian)!=null)
                {
                    length = (double)Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian);
                }
                CP32_bq12.BQdata = "线路" + sum + "条,长度" + length + "Km。";
            }
            else
            {
                CP32_bq12.BQdata = old_value;
            }
            mk_chapter32.Data.Add(CP32_bq12);
            #endregion

            #region //220kv线路条数 长度
            BQData CP32_bq13 = new BQData();
            CP32_bq13.BQname = "B_M_90bb1930_0d33_4aa1_97b1_6b384a66d1f0";
            BQData CP32_bq14 = new BQData();
            CP32_bq14.BQname = "B_M_c12dd71c_1b79_4033_9e19_d3abea6e1572";
            tiaojian = " Type='05' and RateVolt=220 and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime))<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian) != null)
            {
                int sum = (int)Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian);
                double length = 0; if (Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian)!=null) { length = (double)Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian);}
                CP32_bq13.BQdata = sum.ToString();
                CP32_bq14.BQdata = length.ToString();
            }
            else
            {
                CP32_bq13.BQdata = old_value;
                CP32_bq14.BQdata = old_value;
            }
            mk_chapter32.Data.Add(CP32_bq13);
            mk_chapter32.Data.Add(CP32_bq14);
            #endregion
            #region //110kv线路条数 长度（公用+专用）
            BQData CP32_bq15 = new BQData();
            CP32_bq15.BQname = "B_M_8197ede3_bd7f_4d25_9973_dfbacb3a06ae";
            BQData CP32_bq16 = new BQData();
            CP32_bq16.BQname = "B_M_961b8410_5345_4eec_b436_6d81c1ec8064";
            tiaojian = " Type='05' and RateVolt=110  and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime))<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian) != null)
            {
                int sum = (int)Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian);
                 double length = 0; if (Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian)!=null) { length = (double)Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian);}
                CP32_bq15.BQdata = sum.ToString();
                CP32_bq16.BQdata = length.ToString();
            }
            else
            {
                CP32_bq15.BQdata = old_value;
                CP32_bq16.BQdata = old_value;
            }
            mk_chapter32.Data.Add(CP32_bq15);
            mk_chapter32.Data.Add(CP32_bq16);
             #endregion
            #region //110kv公用线路条数 长度
            BQData CP32_bq17 = new BQData();
            CP32_bq17.BQname = "B_M_95538b84_eb3d_4c47_b4ea_a3ae308f2134";
            BQData CP32_bq18 = new BQData();
            CP32_bq18.BQname = "B_M_37ab5d85_9d99_44e7_9a3a_f2ddd82e422a";
            tiaojian = " Type='05' and RateVolt=110 and LineType2='公用' and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime))<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian) != null)
            {
                int sum = (int)Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian);
                double length=0;
                if (Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian)!=null)
                {
                     length = (double)Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian);
                }
                CP32_bq17.BQdata = sum.ToString();
                CP32_bq18.BQdata = length.ToString();
            }
            else
            {
                CP32_bq17.BQdata = old_value;
                CP32_bq18.BQdata = old_value;
            }
            mk_chapter32.Data.Add(CP32_bq17);
            mk_chapter32.Data.Add(CP32_bq18);
             #endregion
            #region //35kv变电站座数
            BQData CP32_bq19 = new BQData();
            CP32_bq19.BQname = "B_M_1617b1d3_b396_4772_a5f0_203f88014359";
            CP32_bq19.BQdata = CP32_bq10.BQdata;
            mk_chapter32.Data.Add(CP32_bq19);
             #endregion
            #region //35kv变电站情况
            BQData CP32_bq20 = new BQData();
            CP32_bq20.BQname = "B_M_a6870103_9afc_4c4b_9411_58e604e5fc55";
            tiaojian = " Flag='1' and L1=35 and AreaID='" + ProjID + "' and cast(S2 as int)<=" + year;
            psilist = Common.Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", tiaojian);
            if (psilist.Count > 0)
            {
                int sum = 0;
                double volum = 0;
                for (int i = 0; i < psilist.Count; i++)
                {
                    //求主变台数和
                    sum += psilist[i].L3;
                    //总变电容量
                    volum += psilist[i].L2;
                }
                CP32_bq20.BQdata = sum + "台主变，变电容量" + volum + "MVA,";
            }
            else
            {
                CP32_bq20.BQdata = old_value;
            }
            mk_chapter32.Data.Add(CP32_bq20);
             #endregion
            #region //35kv线路情况
            BQData CP32_bq21 = new BQData();
            CP32_bq21.BQname = "B_M_94c21c45_9673_459f_a9bd_07f49b0b87f7";
            tiaojian = " Type='05' and RateVolt=35 and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime))<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian) != null)
            {
                int sum = (int)Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian);
                 double length = 0; if (Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian)!=null) { length = (double)Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian);}
                CP32_bq21.BQdata = "线路" + sum + "条,长度" + length + "Km。";
            }
            else
            {
                CP32_bq21.BQdata = old_value;
            }
            mk_chapter32.Data.Add(CP32_bq21);
            #endregion
            #region //报告年份标签
            BQData CP32_bq22 = new BQData();
            CP32_bq22.BQname = "B_M_0c81f3a3_d492_4535_aa09_cbe68556d308";
            CP32_bq22.BQdata = year;
            mk_chapter32.Data.Add(CP32_bq22);
            #endregion
            #region //35kv-500kv变电站主变台数 容量
            BQData CP32_bq23 = new BQData();
            CP32_bq23.BQname = "B_M_e719d630_db40_4e0a_b4ea_913eb87545b2";
            BQData CP32_bq24 = new BQData();
            CP32_bq24.BQname = "B_M_356c1faa_ce1d_4547_8436_70ee84e3a45a";
            tiaojian = " Flag='1' and L1 between 35 and 500 and AreaID='" + ProjID + "' and cast(S2 as int)<=" + year;
            psilist = Common.Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", tiaojian);
            if (psilist.Count > 0)
            {
                int sum = 0;
                double volum = 0;
                for (int i = 0; i < psilist.Count; i++)
                {
                    //求主变台数和
                    sum += psilist[i].L3;
                    //总变电容量
                    volum += psilist[i].L2;
                }
                CP32_bq23.BQdata = sum.ToString();
                CP32_bq24.BQdata = volum.ToString();
            }
            else
            {
                CP32_bq23.BQdata = old_value;
                CP32_bq24.BQdata = old_value;
            }
            mk_chapter32.Data.Add(CP32_bq23);
            mk_chapter32.Data.Add(CP32_bq24);
            #endregion

            //执行更新操作
            BookMark_RefreaDat(W_bkm, mk_chapter32, type);
        }
        #endregion

        #region //配网处理4.2标签数据
        private void Refreh_P_Chapter42(string year, Word.Bookmarks W_bkm, string type)
        {
            int newyear = int.Parse(year) - 1;
            string old_value = "保持原值";
            string tiaojian = "";
            MKData mk_chapter42 = new MKData();
            mk_chapter42.ZJname = "4.2";
            #region //110kv变电站情况
            BQData CP42_bq1 = new BQData();
            CP42_bq1.BQname = "B_M_ae7086aa_ac94_4297_99f8_a17923efde59";
            tiaojian = " Flag='1' and L1=110 and AreaID='" + ProjID + "' and cast(S2 as int)<=" + year;
            IList<PSP_Substation_Info> psilist = Common.Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", tiaojian);
            if (psilist.Count > 0)
            {
                CP42_bq1.BQdata = "110KV";
                for (int i = 0; i < psilist.Count; i++)
                {
                    CP42_bq1.BQdata += psilist[i].Title + "、";
                }
                CP42_bq1.BQdata = CP42_bq1.BQdata.ToString().Substring(0, CP42_bq1.BQdata.ToString().LastIndexOf("、"));
           }
           else
           {
                CP42_bq1.BQdata=old_value;
           }
            mk_chapter42.Data.Add(CP42_bq1);
             #endregion
            #region //35kv变电站情况
            BQData CP42_bq2 = new BQData();
            CP42_bq2.BQname = "B_M_185877a0_ba34_46be_9b1c_cd6219a00afb";
            tiaojian = " Flag='1' and L1=35 and AreaID='" + ProjID + "' and cast(S2 as int)<=" + year;
            psilist = Common.Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", tiaojian);
            if (psilist.Count > 0)
            {
                CP42_bq2.BQdata = "35KV";
                for (int i = 0; i < psilist.Count; i++)
                {
                    CP42_bq2.BQdata += psilist[i].Title + "、";
                }
                CP42_bq2.BQdata = CP42_bq2.BQdata.ToString().Substring(0, CP42_bq2.BQdata.ToString().LastIndexOf("、"));
            }
            else
            {
                CP42_bq2.BQdata = old_value;
            }
            mk_chapter42.Data.Add(CP42_bq2);
            #endregion
            #region //10kv线路条数
            BQData CP42_bq3 = new BQData();
            CP42_bq3.BQname = "B_M_248dfc47_55fb_4075_8357_7af2851b1ef1";
            tiaojian = " Type='05' and RateVolt=10 and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime))<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian) != null)
            {
                CP42_bq3.BQdata = Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian).ToString();
            }
            else
            {
                CP42_bq3.BQdata = old_value;
            }
            mk_chapter42.Data.Add(CP42_bq3);
             #endregion
            #region //6kv线路条数
            BQData CP42_bq4 = new BQData();
            CP42_bq4.BQname = "B_M_70f9daf1_d750_4c0f_ae24_c3c9a245d814";
            tiaojian = " Type='05' and RateVolt=6 and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime))<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian) != null)
            {
                CP42_bq4.BQdata = Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian).ToString();
            }
            else
            {
                CP42_bq4.BQdata = old_value;
            }
            mk_chapter42.Data.Add(CP42_bq4);
             #endregion
            #region //10kv馈线路条数
            BQData CP42_bq5 = new BQData();
            CP42_bq5.BQname = "B_M_3499a257_f935_40eb_b8e9_1571c3ad2f57";
            CP42_bq5.BQdata = CP42_bq3.BQdata;
            mk_chapter42.Data.Add(CP42_bq5);
            #endregion
            #region //6kv馈线路条数
            BQData CP42_bq6 = new BQData();
            CP42_bq6.BQname = "B_M_d1215456_6686_48a3_a236_d081deca2bed";
            CP42_bq6.BQdata = CP42_bq4.BQdata;
            mk_chapter42.Data.Add(CP42_bq6);
             #endregion
            #region //10kv公用线路条数
            BQData CP42_bq7 = new BQData();
            CP42_bq7.BQname = "B_M_bb2a71ce_2a8c_4db8_a20c_6a879419a66c";
            tiaojian = " Type='05' and RateVolt=10 and LineType2='公用' and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime))<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian) != null)
            {
                CP42_bq7.BQdata = Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian).ToString();
            }
            else
            {
                CP42_bq7.BQdata = old_value;
            }
            mk_chapter42.Data.Add(CP42_bq7);
            #endregion
            #region //10kv专用线路条数
            BQData CP42_bq8 = new BQData();
            CP42_bq8.BQname = "B_M_7c3c0e14_3003_426d_825f_472600cf0692";
            tiaojian = " Type='05' and RateVolt=10 and LineType2='专用' and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime))<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian) != null)
            {
                CP42_bq8.BQdata = Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian).ToString();
            }
            else
            {
                CP42_bq8.BQdata = old_value;
            }
            mk_chapter42.Data.Add(CP42_bq8);
             #endregion
            #region //6kv专用或公用线路条数
            BQData CP42_bq9 = new BQData();
            CP42_bq9.BQname = "B_M_2ceec984_691d_449a_ac49_5f659d07efa6";
            tiaojian = " Type='05' and RateVolt=6 and LineType2='公用' and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime))<=" + year;       
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian) != null)
            {
                int sum = 0;
                sum=(int)Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian);
                CP42_bq9.BQdata = "6KV公用线" + sum + "条，";
            }
            tiaojian = " Type='05' and RateVolt=6 and LineType2='专用' and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime))<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian) != null)
            {
                int sum = 0;
                sum = (int)Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian);
                CP42_bq9.BQdata += "6KV专用线" + sum + "条，";
            }
            if (  CP42_bq9.BQdata.ToString().Length==0)
            {
                CP42_bq9.BQdata = old_value;
            }
            else
            {
                CP42_bq9.BQdata = CP42_bq9.BQdata.ToString().Substring(0, CP42_bq9.BQdata.ToString().LastIndexOf("，"));
            }
            mk_chapter42.Data.Add(CP42_bq9);
             #endregion
            #region //10kv公用线路长度
            BQData CP42_bq10 = new BQData();
            CP42_bq10.BQname = "B_M_f27b8979_ed40_4150_ab16_d603d0f4635e";
            tiaojian = " Type='05' and RateVolt=10 and LineType2='公用' and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime))<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian) != null)
            {
                CP42_bq10.BQdata = Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian).ToString();
            }
            else
            {
                CP42_bq10.BQdata = old_value;
            }
            mk_chapter42.Data.Add(CP42_bq10);
             #endregion
            #region //10kv公用线路电缆长度
            BQData CP42_bq11 = new BQData();
            CP42_bq11.BQname = "B_M_7fca31a5_4c90_46f7_8238_1b3b59455d10";
            tiaojian = " Type='05' and RateVolt=10 and LineType2='公用' and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime))<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_SUMLength2", tiaojian) != null)
            {
                CP42_bq11.BQdata = Common.Services.BaseService.GetObject("SelectPSPDEV_SUMLength2", tiaojian).ToString();
            }
            else
            {
                CP42_bq11.BQdata = old_value;
            }
            mk_chapter42.Data.Add(CP42_bq11);
             #endregion
            #region //10kv专用线路长度
            BQData CP42_bq12 = new BQData();
            CP42_bq12.BQname = "B_M_9d88e171_6073_4f9f_962c_e882f0e13e17";
            tiaojian = " Type='05' and RateVolt=10 and LineType2='专用' and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime))<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian) != null)
            {
                CP42_bq12.BQdata = Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian).ToString();
            }
            else
            {
                CP42_bq12.BQdata = old_value;
            }
            mk_chapter42.Data.Add(CP42_bq12);
             #endregion

            //执行更新操作
            BookMark_RefreaDat(W_bkm, mk_chapter42, type);

        }
        #endregion

        #region //配网处理4.7标签数据
        private void Refreh_P_Chapter47(string year, Word.Bookmarks W_bkm, string type)
        {
            int newyear = int.Parse(year) - 1;
            string old_value = "保持原值";
            string tiaojian = "";
            MKData mk_chapter47 = new MKData();
            mk_chapter47.ZJname = "4.7";
            #region //35kvc及以上变电站并联电容器组数
            BQData CP47_bq1 = new BQData();
            CP47_bq1.BQname = "B_M_e8e1b809_b903_4dee_8c96_4fc723c2f63d";
            tiaojian = "  c.L1>=35 and c.AreaID='" + ProjID + "' and cast(c.S2 as int)<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_Type09_Count", tiaojian)!=null)
            {

                CP47_bq1.BQdata = Common.Services.BaseService.GetObject("SelectPSPDEV_Type09_Count", tiaojian).ToString();
            }
            else
            {
                CP47_bq1.BQdata = old_value;
            }
            mk_chapter47.Data.Add(CP47_bq1);
            #endregion
            #region //35kvc及以上变电站并联电容器容量
            BQData CP47_bq2 = new BQData();
            CP47_bq2.BQname = "B_M_0e82ef75_53c9_4c47_8418_ec1d39bb1efb";
            tiaojian = "  c.L1>=35 and c.AreaID='" + ProjID + "' and cast(c.S2 as int)<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_Type09_sumVolue", tiaojian) != null)
            {

                CP47_bq2.BQdata = Common.Services.BaseService.GetObject("SelectPSPDEV_Type09_sumVolue", tiaojian).ToString();
            }
            else
            {
                CP47_bq2.BQdata = old_value;
            }
            mk_chapter47.Data.Add(CP47_bq2);
           #endregion
            #region //220kvc变电站并联电容器容量
            BQData CP47_bq3 = new BQData();
            CP47_bq3.BQname = "B_M_45453852_d69a_4652_a020_53156036c6ad";
            tiaojian = "  c.L1=220 and c.AreaID='" + ProjID + "' and cast(c.S2 as int)<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_Type09_sumVolue", tiaojian) != null)
            {

                CP47_bq3.BQdata = Common.Services.BaseService.GetObject("SelectPSPDEV_Type09_sumVolue", tiaojian).ToString();
            }
            else
            {
                CP47_bq3.BQdata = old_value;
            }
            mk_chapter47.Data.Add(CP47_bq3);
             #endregion
            #region //110kvc变电站并联电容器容量
            BQData CP47_bq4 = new BQData();
            CP47_bq4.BQname = "B_M_be65e5a8_49d0_4f59_b226_0d61df758e1c";
            tiaojian = "  c.L1=110 and c.AreaID='" + ProjID + "' and cast(c.S2 as int)<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_Type09_sumVolue", tiaojian) != null)
            {

                CP47_bq4.BQdata = Common.Services.BaseService.GetObject("SelectPSPDEV_Type09_sumVolue", tiaojian).ToString();
            }
            else
            {
                CP47_bq4.BQdata = old_value;
            }
            mk_chapter47.Data.Add(CP47_bq4);
             #endregion
            #region //35kvc变电站并联电容器容量
            BQData CP47_bq5 = new BQData();
            CP47_bq5.BQname = "B_M_096ec92e_61f8_464a_86b0_afef046c1443";
            tiaojian = "  c.L1=35 and c.AreaID='" + ProjID + "' and cast(c.S2 as int)<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_Type09_sumVolue", tiaojian) != null)
            {

                CP47_bq5.BQdata = Common.Services.BaseService.GetObject("SelectPSPDEV_Type09_sumVolue", tiaojian).ToString();
            }
            else
            {
                CP47_bq5.BQdata = old_value;
            }
            mk_chapter47.Data.Add(CP47_bq5);
             #endregion


            //执行更新操作
            BookMark_RefreaDat(W_bkm, mk_chapter47, type);

        }
        #endregion

        #region //配网处理6.2标签数据
        private void Refreh_P_Chapter62(string year, Word.Bookmarks W_bkm, string type)
        {
            int newyear = int.Parse(year) - 1;
            string old_value = "保持原值";
            string tiaojian = "";
            MKData mk_chapter62 = new MKData();
            mk_chapter62.ZJname = "6.2";
            #region //220kv线路条数 长度
            BQData CP62_bq1 = new BQData();
            CP62_bq1.BQname = "B_M_168d0da8_1bc2_4982_b7e8_f53eda0c7cb6";
            BQData CP62_bq2 = new BQData();
            CP62_bq2.BQname = "B_M_253e55ec_08f5_46ea_8738_a5d8d2af1fe8";
            tiaojian = " Type='05' and RateVolt=220 and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime))<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian) != null)
            {
                int sum = (int)Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian);
                 double length = 0; if (Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian)!=null) { length = (double)Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian);}
                CP62_bq1.BQdata = sum.ToString();
                CP62_bq2.BQdata = length.ToString();
            }
            else
            {
                CP62_bq1.BQdata = old_value;
                CP62_bq2.BQdata = old_value;
            }
            mk_chapter62.Data.Add(CP62_bq1);
            mk_chapter62.Data.Add(CP62_bq2);
            #endregion
            #region //110kv线路条数 长度
            BQData CP62_bq3 = new BQData();
            CP62_bq3.BQname = "B_M_d38de142_d082_433d_ac62_9992fac439fb";
            BQData CP62_bq4 = new BQData();
            CP62_bq4.BQname = "B_M_d57e726e_e388_4f6d_82e2_6fc75350116d";
            tiaojian = " Type='05' and RateVolt=110 and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime))<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian) != null)
            {
                int sum = (int)Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian);
                 double length = 0; if (Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian)!=null) { length = (double)Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian);}
                CP62_bq3.BQdata = sum.ToString();
                CP62_bq4.BQdata = length.ToString();
            }
            else
            {
                CP62_bq3.BQdata = old_value;
                CP62_bq4.BQdata = old_value;
            }
            mk_chapter62.Data.Add(CP62_bq3);
            mk_chapter62.Data.Add(CP62_bq4);
             #endregion
            #region //110kv公用线路条数 长度
            BQData CP62_bq5 = new BQData();
            CP62_bq5.BQname = "B_M_427b30dd_eb64_4a09_9494_19d230563b82";
            BQData CP62_bq6 = new BQData();
            CP62_bq6.BQname = "B_M_52b9d10c_ff36_4661_9a6c_4d796ded934b";
            tiaojian = " Type='05' and RateVolt=110 and LineType2='公用' and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime))<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian) != null)
            {
                int sum = (int)Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian);
                 double length = 0; if (Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian)!=null) { length = (double)Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian);}
                CP62_bq5.BQdata = sum.ToString();
                CP62_bq6.BQdata = length.ToString();
            }
            else
            {
                CP62_bq5.BQdata = old_value;
                CP62_bq6.BQdata = old_value;
            }
            mk_chapter62.Data.Add(CP62_bq5);
            mk_chapter62.Data.Add(CP62_bq6);
            #endregion

            //执行更新操作
            BookMark_RefreaDat(W_bkm, mk_chapter62, type);

        }
        #endregion

        #region //配网处理6.3标签数据
        private void Refreh_P_Chapter63(string year, Word.Bookmarks W_bkm, string type)
        {
            int newyear = int.Parse(year) - 1;
            string old_value = "保持原值";
            string tiaojian = "";
            MKData mk_chapter63 = new MKData();
            mk_chapter63.ZJname = "6.3";
            #region //110kv线路条数
            BQData CP63_bq1 = new BQData();
            CP63_bq1.BQname = "B_M_72931049_3544_4999_9d70_6d335036e83a";
            tiaojian = " Type='05' and RateVolt=110 and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime))<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian) != null)
            {
                int sum = (int)Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian);
                CP63_bq1.BQdata = sum.ToString();
            }
            else
            {
                CP63_bq1.BQdata = old_value;
            }
            mk_chapter63.Data.Add(CP63_bq1);
            #endregion

            #region //110kv变电站情况
            BQData CP63_bq2 = new BQData();
            CP63_bq2.BQname = "B_M_fb52a918_28f8_4477_8d49_4ea35ae83926";
            tiaojian = " Flag='1' and L1=110 and AreaID='" + ProjID + "' and cast(S2 as int) between 2006 and " + year;
            //新建110kv变电站座数
            int tempsum1 = 0;
            //扩建110kv变电站座数
            double tempsum2 = 0;
            //新建110kv变电站主变台数
            int tempsum3 = 0;
            //扩建110kv变电站主变台数
            double tempsum4 = 0;
            double tempdb1 = 0;
            double tempdb2 = 0;
            if (Common.Services.BaseService.GetObject("SelectPSP_Substation_InfoCountall", tiaojian) != null)
            {
                tempsum1 = (int)Common.Services.BaseService.GetObject("SelectPSP_Substation_InfoCountall", tiaojian);
            }
            tiaojian = " and a.BuildYear between 2006 and " + year + "  and a.Col3='扩建' and  a.ProjectID='" + ProjID + "'  and a.BianInfo like '%110%'";
            if (Common.Services.BaseService.GetObject("SelectTZGSsubnum", tiaojian) != null)
            {
                tempsum2 = (double)Common.Services.BaseService.GetObject("SelectTZGSsubnum", tiaojian);
            }
            tiaojian = " Flag='1' and L1=110 and AreaID='" + ProjID + "' and cast(S2 as int) between 2006 and " + year;
            if (Common.Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", tiaojian) != null)
            {
                tempsum3 = (int)Common.Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", tiaojian);
            }
            tiaojian = " and a.BuildYear between 2006 and " + year + "  and a.Col3='扩建' and  a.ProjectID='" + ProjID + "' and a.BianInfo like '%110%'";
            if (Common.Services.BaseService.GetObject("SelectTZGSsubTS", tiaojian) != null)
            {
                tempsum4 = (double)Common.Services.BaseService.GetObject("SelectTZGSsubTS", tiaojian);
            }

            tiaojian = " Flag='1' and L1=110 and AreaID='" + ProjID + "' and cast(S2 as int) between 2006 and " + year;
            if (Common.Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", tiaojian) != null)
            {
                tempdb1 = (double)Common.Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", tiaojian);
            }
            tiaojian = " and a.BuildYear between 2006 and " + year + "  and a.Col3='扩建' and  a.ProjectID='" + ProjID + "'  and a.BianInfo like '%110%'";
            if (Common.Services.BaseService.GetObject("SelectTZGSsubLL", tiaojian) != null)
            {
                tempdb2 = (double)Common.Services.BaseService.GetObject("SelectTZGSsubLL", tiaojian);
            }
            if (tempsum1!=0)
            {
                CP63_bq2.BQdata="已新建110kV变电站"+tempsum1+"座，";
            }
            if (tempsum2!=0)
	        {
                CP63_bq2.BQdata +="扩建"+tempsum2+"座，";
	        }
            if (tempsum3+tempsum4!=0)
	        {
                CP63_bq2.BQdata += "新增变压器" + (tempsum3 + tempsum4) + "台,";
	        }
            if (tempdb1+tempdb2!=0)
	        {
                CP63_bq2.BQdata += "110kV变电容量"+(tempdb1+tempdb2)+"MVA";
	        }
            if (CP63_bq2.BQdata.ToString().Length==0)
	        {
                CP63_bq2.BQdata=old_value;
	        }
              mk_chapter63.Data.Add(CP63_bq2);
            #endregion
            #region //110kv线路新增情况
            BQData CP63_bq3 = new BQData();
            CP63_bq3.BQname = "B_M_5845a5fa_0ec7_48d2_bf26_2214da5785da";
            tiaojian = " Type='05' and RateVolt=110  and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime)) between 2006 and " + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian) != null)
            {
                int sum = (int)Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian);
                 double length = 0; if (Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian)!=null) { length = (double)Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian);}
                CP63_bq3.BQdata = "110kV输电线路新增" + sum + "条，长度约" + length + "km";
            }
            else
	        {
                CP63_bq3.BQdata = old_value;
	        }
             mk_chapter63.Data.Add(CP63_bq3);
            #endregion
          
            #region //110kv变电站线路情况（公用专用长度等）
            BQData CP63_bq4 = new BQData();
            CP63_bq4.BQname = "B_M_b268af20_95ec_4675_87b6_31884a5f92ef";
            tiaojian = " Flag='1' and L1=110 and S4='公用' and AreaID='" + ProjID + "' and cast(S2 as int)<=" + year;
            IList<PSP_Substation_Info> psilist = Common.Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", tiaojian);
            if (psilist.Count > 0)
            {
                int sum = 0;
                double volum = 0;
                for (int i = 0; i < psilist.Count; i++)
                {
                    CP63_bq4.BQdata+= psilist[i].Title+"、";
                    //求主变台数和
                    sum += psilist[i].L3;
                    //总变电容量
                    volum += psilist[i].L2;
                }
                // 110kV公用变电所现有10座（东村变、新桥变、金顺变、开发区变、谢家垄变、五松变、西郊变、天桥变、乌木山变、大通变），总变电容量1003MVA；
                CP63_bq4.BQdata="110kV公用变电所现有"+psilist.Count+"座（"+CP63_bq4.BQdata.ToString().Substring(0,CP63_bq4.BQdata.ToString().LastIndexOf("、"))+"),";
                CP63_bq4.BQdata+="总变电容量"+volum+"MVA；";
            }
            tiaojian = " Type='05' and RateVolt=110 and LineType2='公用' and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime))<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian) != null)
            {
                int sum = (int)Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian);
                if (Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian)!=null && sum!=0)
                {
                     double length = 0; if (Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian)!=null) { length = (double)Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian);}
                    CP63_bq4.BQdata += "110kV公用线路" + length + "km、" + sum + "条。";
                }
               
            }
            tiaojian = " Flag='1' and L1=110 and S4='专用' and AreaID='" + ProjID + "' and cast(S2 as int)<=" + year;
            psilist = Common.Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", tiaojian);
            if (psilist.Count > 0)
            {
                int sum = 0;
                double volum = 0;
                string tempstr = "";
                for (int i = 0; i < psilist.Count; i++)
                {
                    tempstr += psilist[i].Title + "、";
                    //求主变台数和
                    sum += psilist[i].L3;
                    //总变电容量
                    volum += psilist[i].L2;
                }
                // 110kV用户变电所10座(金隆I变、金隆II变、海螺I变、海螺II变、金昌变、金威变、冬瓜山变、富鑫变、上峰变、铜冠冶化变)，共有主变20台，总变电容量571kVA；
                CP63_bq4.BQdata += "110kV用户变电所" + psilist.Count + "座（" + tempstr.Substring(0, tempstr.LastIndexOf("、")) + "),";
                CP63_bq4.BQdata+="共有主变"+sum+"台，总变电容量"+volum+"MVA；";
            }
             tiaojian = " Type='05' and RateVolt=110 and LineType2='专用' and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime))<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian) != null)
            {
                int sum = (int)Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian);
                if (Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian)!=null&&sum!=0)
                {
                  double length = 0; if (Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian)!=null) { length = (double)Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian);}
               //110kV用户线路84.96km、16条。
                CP63_bq4.BQdata+="110kV用户线路"+length+"km、"+sum+"条。";
                }
                
               
            }
            if ( CP63_bq4.BQdata.ToString().Length==0)
	        {
                CP63_bq4.BQdata=old_value;
	        }
            mk_chapter63.Data.Add(CP63_bq4);
            #endregion

            //执行更新操作
            BookMark_RefreaDat(W_bkm, mk_chapter63, type);

        }
        #endregion

        #region //农网处理第二章标签数据
        private void Refreh_N_Chapter2(string year, Word.Bookmarks W_bkm, string type)
        {
            int newyear = int.Parse(year) - 1;
            string old_value = "保持原值";
            string tiaojian = "";
            MKData mk_chapter2 = new MKData();
            mk_chapter2.ZJname = "第二章";
            #region //全县人口书签
            BQData CP2_bq1 = new BQData();
            CP2_bq1.BQname = "B_M_ceebc1ae_440a_426e_9fc0_1dafd06177d5";
            tiaojian = " a.ProjectID='" + ProjID + "' and (a.Area not like '%部地区%' and a.Area not like '%市' and a.Area not like '%县') and b.Yearf=" + newyear;
            if (Services.BaseService.GetObject("SelectAreaDataPopulation_notcity_ByArea", tiaojian) != null)
            {
                double tempdb = (double)Services.BaseService.GetObject("SelectAreaDataPopulation_notcity_ByArea", tiaojian);
                CP2_bq1.BQdata = tempdb.ToString();
            }
            else
            {
                CP2_bq1.BQdata = old_value;
            }
            mk_chapter2.Data.Add(CP2_bq1);
            #endregion
            #region //全县面积
            BQData CP2_bq2 = new BQData();
            CP2_bq2.BQname = "B_M_bed91ba9_3409_4e17_b47c_23a436748b69";
            if (Services.BaseService.GetObject("SelectAreaData_TotalArea_SUM", tiaojian) != null)
            {
                double tempdb = (double)Services.BaseService.GetObject("SelectAreaData_TotalArea_SUM", tiaojian);
                CP2_bq2.BQdata = tempdb.ToString();
            }
            else
            {
                CP2_bq2.BQdata = old_value;
            }
            mk_chapter2.Data.Add(CP2_bq2);
             #endregion
            
            #region //报告年份
            BQData CP2_bq3 = new BQData();
            CP2_bq3.BQname = "B_M_2a9a2aa4_b843_4060_b660_b3e873988285";
            CP2_bq3.BQdata = newyear.ToString();
            mk_chapter2.Data.Add(CP2_bq3);
             #endregion
            #region //全县人口
            BQData CP2_bq4 = new BQData();
            CP2_bq4.BQname = "B_M_5fc9d479_a28b_4d50_bcf9_982bdfb570e8";
            CP2_bq4.BQdata = CP2_bq1.BQdata;
            mk_chapter2.Data.Add(CP2_bq4);
            #endregion




            //执行更新操作
            BookMark_RefreaDat(W_bkm, mk_chapter2, type);
        }
        #endregion
        #region //农网处理第三章标签数据
        private void Refreh_N_Chapter3(string year, Word.Bookmarks W_bkm, string type)
        {
            int newyear = int.Parse(year) - 1;
            string old_value = "保持原值";
            string tiaojian = "";
            MKData mk_chapter3 = new MKData();
            mk_chapter3.ZJname = "第三章";
            #region //报告年份
            BQData CP3_bq1 = new BQData();
            CP3_bq1.BQname = "B_M_3032ef0f_0154_44ab_9c20_120417b6e472";
            CP3_bq1.BQdata = newyear.ToString();
            mk_chapter3.Data.Add(CP3_bq1);
            #endregion
            #region //220kv变电所情况
            BQData CP3_bq2 = new BQData();
            CP3_bq2.BQname = "B_M_b7155a1c_b7c2_4c94_9bbe_dea6b23e903e";
            tiaojian = " Flag='1' and L1=220 and AreaID='" + ProjID + "' and cast(S2 as int)<=" + year;
            IList<PSP_Substation_Info> psilist = Common.Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", tiaojian);
            if (psilist.Count > 0)
            {
                int sum = 0;
                double volum = 0;
                string tempstr = "";
                for (int i = 0; i < psilist.Count; i++)
                {
                    tempstr += psilist[i].Title + "(" + psilist[i].L4 + "MVA)、";
                    //求主变台数和
                    sum += psilist[i].L3;
                    //总变电容量
                    volum += psilist[i].L2;
                }
                CP3_bq2.BQdata = "220kV变电所" + psilist.Count + "座分别为:" + tempstr.Substring(0, tempstr.LastIndexOf("、")) + ",主变" + sum + "台，总容量" + volum + "MVA。";
            }
            else
            {
                CP3_bq2.BQdata = old_value;
            }

            mk_chapter3.Data.Add(CP3_bq2);
            #endregion
            #region //110kv变电所情况
            BQData CP3_bq3 = new BQData();
            CP3_bq3.BQname = "B_M_2f784b53_d1a3_4799_ad84_3743f012dacf";
            tiaojian = " Flag='1' and L1=110 and AreaID='" + ProjID + "' and cast(S2 as int)<=" + year;
             psilist = Common.Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", tiaojian);
            if (psilist.Count > 0)
            {
                int sum = 0;
                double volum = 0;
                string tempstr = "";
                for (int i = 0; i < psilist.Count; i++)
                {
                    tempstr += psilist[i].Title + "(" + psilist[i].L4 + "MVA)、";
                    //求主变台数和
                    sum += psilist[i].L3;
                    //总变电容量
                    volum += psilist[i].L2;
                }
                CP3_bq3.BQdata = "110kV变电所" + psilist.Count + "座分别为:" + tempstr.Substring(0, tempstr.LastIndexOf("、")) + ",主变" + sum + "台，总容量" + volum + "MVA。";
            }
            else
            {
                CP3_bq3.BQdata = old_value;
            }
            mk_chapter3.Data.Add(CP3_bq3);
            #endregion
            #region //报告年份
            BQData CP3_bq4 = new BQData();
            CP3_bq4.BQname = "B_M_23b3f0b7_9471_4d49_8ff3_ded78e94e15c";
            CP3_bq4.BQdata = newyear.ToString();
            mk_chapter3.Data.Add(CP3_bq4);
            #endregion
            #region //县概况
            //公用变电所坐数
            int sumGY_35_Bian = 0;
            //公用主变台数
            int sumGY_35_Tai = 0;
            //公用变电容量
            double volumGY_35 = 0;
            //公用线路条数
            int sumGY_35_Line = 0;
            //公用线路长度
            double lengthGY_35_Line = 0;

            //用户变电所坐数
            int sumZY_35_Bian = 0;
            //用户主变台数
            int sumZY_35_Tai = 0;
            //用户变电容量
            double volumZY_35 = 0;
            //用户线路条数
            int sumZY_35_Line = 0;
            //用户线路长度
            double lengthZY_35_Line = 0;

           
            //公用主变台数
            int sumGY_10_Tai = 0;
            //公用变电容量
            double volumGY_10 = 0;
            //用户主变台数
            int sumZY_10_Tai = 0;
            //用户变电容量
            double volumZY_10 = 0;
            //10kv总容量
            double volum_10_all = 0;


            //公用线路条数
            int sum_10_Line = 0;
            //公用线路长度
            double length_10_Line = 0;


            #endregion
            #region //35kv公用变电所情况
            BQData CP3_bq5 = new BQData();
            CP3_bq5.BQname = "B_M_6837b8e5_b472_4a41_b643_b68b333c86fa";
            tiaojian = " Flag='1' and L1=35 and S4='公用' and AreaID='" + ProjID + "' and cast(S2 as int)<=" + year;
             psilist = Common.Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", tiaojian);
            if (psilist.Count > 0)
            {
                sumGY_35_Bian = psilist.Count;
                int sum = 0;
                double volum = 0;
                string tempstr = "";
                for (int i = 0; i < psilist.Count; i++)
                {
                    tempstr += psilist[i].Title + "(" + psilist[i].L4 + "MVA)、";
                    //求主变台数和
                    sum += psilist[i].L3;
                    //总变电容量
                    volum += psilist[i].L2;
                }
                sumGY_10_Tai = sum;
                volumGY_35 = volum;
                //35kV公用变电站12座，共有主变20台，总变电容量131800kVA
                CP3_bq5.BQdata = "35kV公用变电站" + psilist.Count + "座,共有主变" + sum + "台，总变电容量" + volum + "MVA。";
            }
            else
            {
                CP3_bq5.BQdata = old_value;
            }
            mk_chapter3.Data.Add(CP3_bq5);
             #endregion
            #region //35kv公用线路情况
            BQData CP3_bq6 = new BQData();
            CP3_bq6.BQname = "B_M_e0cb72b3_6faa_46eb_987f_5373d37e2326";
            tiaojian = " Type='05' and RateVolt=35 and LineType2='公用' and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime))<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian) != null)
            {
                int sum = (int)Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian);
                 double length = 0; if (Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian)!=null) { length = (double)Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian);}
                CP3_bq6.BQdata = "35kV公用线路" + sum + "条共" + length + "km";
                sumGY_35_Line = sum;
                lengthGY_35_Line = length;
            }
             else
            {
                CP3_bq6.BQdata = old_value;
            }
            mk_chapter3.Data.Add(CP3_bq6);
             #endregion
            #region //35kv专用变电所情况
            BQData CP3_bq7 = new BQData();
            CP3_bq7.BQname = "B_M_6261af63_709a_45f2_abd5_ce827d7b66df";
            tiaojian = " Flag='1' and L1=35 and S4='专用' and AreaID='" + ProjID + "' and cast(S2 as int)<=" + year;
             psilist = Common.Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", tiaojian);
            if (psilist.Count > 0)
            {
                int sum = 0;
                double volum = 0;
                string tempstr = "";
                for (int i = 0; i < psilist.Count; i++)
                {
                    tempstr += psilist[i].Title + "(" + psilist[i].L4 + "MVA)、";
                    //求主变台数和
                    sum += psilist[i].L3;
                    //总变电容量
                    volum += psilist[i].L2;
                }
                //35kV用户变电站12座，共有主变20台，总变电容量131800kVA
                CP3_bq7.BQdata = "35kV用户变电站" + psilist.Count + "座,共有主变" + sum + "台，总变电容量" + volum + "MVA。";
                sumZY_35_Bian = psilist.Count;
                sumZY_35_Tai = sum;
                volumZY_35 = volum;
            }
            else
            {
                CP3_bq7.BQdata = old_value;
            }
            mk_chapter3.Data.Add(CP3_bq7);
             #endregion
            #region //35kv专用线路情况
            BQData CP3_bq8 = new BQData();
            CP3_bq8.BQname = "B_M_b28b0759_f7ee_4fbe_9771_4cd1f730a116";
            tiaojian = " Type='05' and RateVolt=35 and LineType2='专用' and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime))<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian) != null)
            {
                int sum = (int)Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian);
                double length = 0;
                if (Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian)!=null)
                {
                    length = (double)Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian);
                }
                CP3_bq8.BQdata = "35kV用户线路" + sum + "条共" + length + "km";
                sumZY_35_Line = sum;
                lengthZY_35_Line = length;
                if (sum==0)
                {
                    CP3_bq8.BQdata = old_value;
                }
            }
            else
	        {
                CP3_bq8.BQdata = old_value;
            }
            mk_chapter3.Data.Add(CP3_bq8);
            #endregion

            //10kV公用配变732台，总变电容量102180kVA，用户变740台, 变电容量149128kVA；总容量为251308kVA；10kV线路58 条共计808.975km。
            #region //10kv变电站线路情况
            BQData CP3_bq9 = new BQData();
            CP3_bq9.BQname = "B_M_10a9f505_2cdb_461d_a239_a34d61d5e3f7";
            tiaojian = " Flag='1' and L1=10 and S4='公用' and AreaID='" + ProjID + "' and cast(S2 as int)<=" + year;
             psilist = Common.Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", tiaojian);
            double volumall=0;
            int sumall = 0;
            int sumGY = 0;
            double volumGY = 0;
            if (psilist.Count > 0)
            {
                int sum = 0;
                double volum = 0;
                for (int i = 0; i < psilist.Count; i++)
                {
                    //求主变台数和
                    sum += psilist[i].L3;
                    //总变电容量
                    volum += psilist[i].L2;
                }
                sumGY = sum;
                sumGY_10_Tai = sum;
                volumGY = volum;
                volumGY_10 = volum;
                sumall += sum;
                volumall += volum;
                //35kV用户变电站12座，共有主变20台，总变电容量131800kVA
                CP3_bq9.BQdata = "10kV公用配变" + sum + "台,总变电容量" + sum + "台，总变电容量" + volum + "MVA，";
            }
            tiaojian = " Flag='1' and L1=10 and S4='专用' and AreaID='" + ProjID + "' and cast(S2 as int)<=" + year;
             psilist = Common.Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", tiaojian);
            if (psilist.Count > 0)
            {
                int sum = 0;
                double volum = 0;
                for (int i = 0; i < psilist.Count; i++)
                {
                    //求主变台数和
                    sum += psilist[i].L3;
                    //总变电容量
                    volum += psilist[i].L2;
                }
                sumall += sum;
                sumZY_10_Tai = sum;
                volumall+=volum;
                volumZY_10 = volum;
                volum_10_all = volumGY_10 + volumZY_10;
                //35kV用户变电站12座，共有主变20台，总变电容量131800kVA
                  CP3_bq9.BQdata += "用户变" + sum + "台,总变电容量" + sum + "台，变电容量" + volum + "MVA；总容量为" + volumall + "MVA；";
            }
            tiaojian = " Type='05' and RateVolt=10  and ProjectID='" + ProjID + "' and  year(cast(OperationYear as datetime))<=" + year;
            if (Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian) != null)
            {
                int sum = (int)Common.Services.BaseService.GetObject("SelectPSPDEV_CountAll", tiaojian);
                double length=0;
                if (Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian)!=null)
                {
                    length = (double)Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", tiaojian);
                }
                CP3_bq9.BQdata += "10kV线路" + sum + "条共计" + length + "km。";
                sum_10_Line = sum;
                length_10_Line = length;
            }
            if (CP3_bq9.BQdata.ToString().Length<5)
            {
                CP3_bq9.BQdata = old_value;
            }
            mk_chapter3.Data.Add(CP3_bq9);
            #endregion
            //2008年底，铜陵县公司有10kV配变1469台，累计容量251.3 MVA，其中公用配变732台，累计容量102.18MVA
            #region //10kv变电站线路情况
            BQData CP3_bq10 = new BQData();
            CP3_bq10.BQname = "B_M_2f0d9e1f_fac5_4489_ab0d_7a052e35149d";
            CP3_bq10.BQdata = newyear + "年底，铜陵县公司有10kV配变" + sumall + "台，累计容量" + volumall + " MVA，其中公用配变" + sumGY + "台，累计容量" + volumGY+"MVA";
            if (sumall==0)
            {
                CP3_bq10.BQdata = old_value;
            }
            mk_chapter3.Data.Add(CP3_bq10);
            #endregion
            //截止2008年底，铜陵县电网共有35kV公用变电站12座，共有主变20台，总变电容量138000kVA；
            //35kV公用线路16条共81.878km。35kV用户变电站13座，共有主变20台，总变电容量33865kVA； 
            //35kV用户线路10条共26.06km。10kV公用配变732台，总变电容量102180kVA，用户变740台, 
            //变电容量149128kVA；总容量为251308kVA；10kV线路58 条共计808.975km。
            #region //农网概述
            BQData CP3_bq11 = new BQData();
            CP3_bq11.BQname = "B_M_1b48d492_5022_4f59_b433_7b19343ae4b1";
            CP3_bq11.BQdata = "截止" + newyear + "年底，铜陵县电网共有";
            if (sumGY_35_Bian>0)
            {
                CP3_bq11.BQdata += "35kV公用变电站"+sumGY_35_Bian+"座，共有主变"+sumGY_35_Tai+"台，总变电容量"+volumGY_35+"MVA;";
            }
            if (sumGY_35_Line>0)
            {
                 CP3_bq11.BQdata += "35kV公用线路" + sumGY_35_Line + "条共" + lengthGY_35_Line + "km。";
            }
            if (sumZY_35_Bian>0)
	        {
		          CP3_bq11.BQdata +="35kV用户变电站" + sumZY_35_Bian + "座，共有主变" + sumZY_35_Tai + "台，总变电容量" + volumZY_35 + "MVA；";
	        }
            if (sumZY_35_Line>0)
            {
                 CP3_bq11.BQdata += "35kV用户线路"+sumZY_35_Line+"条共"+lengthZY_35_Line+"km。";
            }
            if (sumGY_10_Tai>0)
	        {
                CP3_bq11.BQdata +="10kV公用配变"+sumGY_10_Tai+"台，总变电容量"+volumGY_10+"MVA，";
	        }
            if (sumZY_10_Tai>0)
            {
                 CP3_bq11.BQdata += "用户变"+sumZY_10_Tai+"台,变电容量"+volumZY_10+"MVA；";
            }
            if (volum_10_all>0)
	        {
                 CP3_bq11.BQdata += "总容量为"+volum_10_all+"MVA；";
	        }
            if (sum_10_Line>0)
	        {
                CP3_bq11.BQdata += "10kV线路" + sum_10_Line + " 条共计" + length_10_Line + "km。";
	        }
            if (CP3_bq11.BQdata.ToString()=="截止" + newyear + "年底，铜陵县电网共有")
            {
                CP3_bq11.BQdata = old_value;
            }
            mk_chapter3.Data.Add(CP3_bq11);
            #endregion
            //执行更新操作
            BookMark_RefreaDat(W_bkm, mk_chapter3, type);
        }
         #endregion
        // 杀掉所有winword.exe进程 
        protected void killAllProcess() 
            { 
            System.Diagnostics.Process[] myPs; 
            myPs = System.Diagnostics.Process.GetProcesses(); 
            foreach (System.Diagnostics.Process p in myPs) 
            { 
            if (p.Id != 0) 
            { 
            string myS = "WINWORD.EXE" + p.ProcessName + " ID:" + p.Id.ToString(); 
            try 
            { 
            if (p.Modules != null) 
            if (p.Modules.Count > 0) 
            { 
            System.Diagnostics.ProcessModule pm = p.Modules[0]; 
            myS += "\n Modules[0].FileName:" + pm.FileName; 
            myS += "\n Modules[0].ModuleName:" + pm.ModuleName; 
            myS += "\n Modules[0].FileVersionInfo:\n" + pm.FileVersionInfo.ToString();
                //确保word进程是本程系启动而非用户自己启动的
            if ((pm.ModuleName.ToLower() == "winword.exe")&&(p.MainWindowTitle=="")) 
            p.Kill(); 
            } 
            } 
            catch 
            { } 
            finally 
            { 

            } 
            } 
            } 
            }
        //管理书签
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode==null)
            {
                MessageBox.Show("当前没有文档!");
                return;
            }
            FrmBookMark.CHID = LayoutID;
            FrmBookMark fbm = new FrmBookMark();
            fbm.Show();
            
        }
        //自动更新数据
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.Nodes.Count > 0)
            {
                RefFalg=true;
                FrmGHBGdata fgh = new FrmGHBGdata();
                fgh.ShowDialog();
                if (fgh.DialogResult == DialogResult.OK)
                {
                    string year = fgh.GHBGyear;
                    MessageBox.Show("选择的规划报告年份是：" + fgh.GHBGyear);
                   //配网规划
                    if (layoutUID=="c893cc58-eabf-4f21-8d14-67e617b5665d|87367824-3e0f-482e-995c-2abac6a521e3")
                    {
                        RefrehDataAll(year,"自动","配网");
                    }
                    //农网规划
                    else if (layoutUID == "8cacc023-d527-48bb-a38d-ed0e7b81efcc|1642b0a0-2a0c-4903-8a81-751299ec6f40")
                    {
                        RefrehDataAll(year,"自动","农网");
                    }
                    //其它
                    else 
                    {
                        RefrehDataAll(year,"自动","其它");
                    }
                }
            }
        }
        //手动更新数据
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            if (treeList1.Nodes.Count > 0)
            { 
                RefFalg=true;
                FrmGHBGdata fgh = new FrmGHBGdata();
                fgh.ShowDialog();
                if (fgh.DialogResult == DialogResult.OK)
                {
                    string year = fgh.GHBGyear;
                    MessageBox.Show("选择的规划报告年份是：" + fgh.GHBGyear);
                    //配网规划
                    if (layoutUID=="c893cc58-eabf-4f21-8d14-67e617b5665d|87367824-3e0f-482e-995c-2abac6a521e3")
                    {
                        RefrehDataAll(year,"手动","配网");
                    }
                    //农网规划
                    else if (layoutUID == "8cacc023-d527-48bb-a38d-ed0e7b81efcc|1642b0a0-2a0c-4903-8a81-751299ec6f40")
                    {
                        RefrehDataAll(year,"手动","农网");
                    }
                    //其它
                    else 
                    {
                        RefrehDataAll(year,"手动","其它");
                    }
                }
            }
        }

       



    }
}