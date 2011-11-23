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
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using Itop.Client.Base;
namespace Itop.Client.Layouts
{
    public partial class FrmLayoutContents : FormBase
    {
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



        public FrmLayoutContents()
        {
            InitializeComponent();
        }

        private void FrmLayoutContents_Load(object sender, EventArgs e)
        {
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

            ilist = Services.BaseService.GetList("SelectLayoutContentByLayoutID", layoutUID);
            dataTable = DataConverter.ToDataTable(ilist, typeof(LayoutContent));
            treeList1.DataSource = dataTable;
            isstate = true;

            treeList1.MoveFirst();
        }

        void dsoFramerWordControl1_OnFileSaved(object sender, EventArgs e)
        {
            SaveText();
        }



        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (treeList1.FocusedNode == null)
                return;



            string uid = treeList1.FocusedNode["UID"].ToString();
            LayoutContent obj = Services.BaseService.GetOneByKey<LayoutContent>(uid);
            WaitDialogForm wait=null;


            WordBuilder wb = new WordBuilder();
            //if (fb != null)
            //    wb.InsertFromStreamGzip(fb);

            try
            {
                wait = new WaitDialogForm("", "正在下载数据, 请稍候...");

                if (obj.Contents != null && obj.Contents.Length > 0)
                {
                    if (fb != null)
                    {
                        wb.InsertFromStreamGzip(obj.Contents);
                        dsoFramerWordControl1.FileData = wb.FileData;
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
                wait.Close();
            }
            catch (Exception ex)
            {
                wait.Close();
            MessageBox.Show(ex.Message);

        }
        }

        private void SaveText()
        {
            if (treeList1.FocusedNode == null)
                return;
            string uid = treeList1.FocusedNode["UID"].ToString();
            LayoutContent obj = Services.BaseService.GetOneByKey<LayoutContent>(uid);

            obj.Contents = dsoFramerWordControl1.FileDataGzip;
            WaitDialogForm wait = null;
            try
            {
                wait = new WaitDialogForm("", "正在保存数据, 请稍候...");
                Services.BaseService.Update("UpdateLayoutContentByte", obj);
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
            string parentid = "";
            if (treeList1.FocusedNode != null)
            {
                parentid = treeList1.FocusedNode["ParentID"].ToString();
            }


            LayoutContent obj = new LayoutContent();
            obj.UID = obj.UID + "|" + Itop.Client.MIS.ProgUID;
            obj.LayoutID = layoutUID;
            obj.ParentID = parentid;
            obj.CreateDate = DateTime.Now;
            FrmLayoutContentDialog dlg = new FrmLayoutContentDialog();
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
            string parentid = "";
            if (treeList1.FocusedNode == null)
                return;
            parentid = treeList1.FocusedNode["UID"].ToString();


            LayoutContent obj = new LayoutContent();
            obj.UID = obj.UID + "|" + Itop.Client.MIS.ProgUID;
            obj.LayoutID = layoutUID;
            obj.ParentID = parentid;
            obj.CreateDate = DateTime.Now;
            FrmLayoutContentDialog dlg = new FrmLayoutContentDialog();
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
            LayoutContent obj = Services.BaseService.GetOneByKey<LayoutContent>(uid);

            LayoutContent objCopy = new LayoutContent();
            DataConverter.CopyTo<LayoutContent>(obj, objCopy);

            FrmLayoutContentDialog dlg = new FrmLayoutContentDialog();
            dlg.Object = objCopy;

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            DataConverter.CopyTo<LayoutContent>(objCopy, obj);
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
                Services.BaseService.DeleteByKey<LayoutContent>(uid);
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
            wb.InsertFromFile("Very.doc");

            
            //TONLI.BZH.UI.DSOFramerWordControl wb = new DSOFramerWordControl();
            try
            {
                wait = new WaitDialogForm("", "正在导出, 请稍候...");

                
                IList<LayoutContent> ls = Services.BaseService.GetList<LayoutContent>("SelectLayoutContentByLayoutIDBlogData", layoutUID);
                System.Data.DataTable dts = DataConverter.ToDataTable((IList)ls, typeof(LayoutContent));

                //IList<byte[]> lbt = new List<byte[]>();



                IList<LayoutContent> lbt = new List<LayoutContent>();
                
                InitExe("", dts, lbt);
                GetTop(layoutUID + "|1", wb);


                object obj = "标题 1";


                Style testStyle = wb.wordApp.Application.ActiveDocument.Styles.get_Item(ref obj);
                object listObject = testStyle;
                wb.wordApp.Selection.set_Style(ref listObject);


                
                InitAdd(lbt, wb);
                GetTop(layoutUID + "|2", wb);
                

                //IList<LayoutContent> lbt = new List<LayoutContent>();
                //InitExe("", dts, lbt);
                //GetTop(layoutUID + "|1", wb);
                //InitAdd(lbt, wb);
                //GetTop(layoutUID + "|2", wb);
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

                fname = saveFileDialog1.FileName;
                wb.Save(fname, true);
                wb.Dispose();
                if (MsgBox.ShowYesNo("导出成功，是否打开该文档？") != DialogResult.Yes)
                    return;
                try
                {
                    System.Diagnostics.Process.Start(fname);
                }
                catch { }
            }
            else
            {
            
            }
        }




        private void InitAdd(IList<LayoutContent> ls, WordBuilder tx2)
        {
            foreach (LayoutContent bs in ls)
            {
                try
                {

                    //////tx2.wordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    //////tx2.wordApp.Selection.Font.Bold = 1;
                    //////tx2.wordApp.Selection.Font.Size = 18;
                    //MessageBox.Show(tx2.wordApp.Selection.get_Style().ToString());
                    //tx2.wordApp.Selection.
                    //tx2.wordApp.Selection.TypeText("\r\n");      

                    


tx2.wordApp.Selection.TypeText(bs.ChapterName+"\r\n");
object obj = "标题 1";


Style testStyle = tx2.wordApp.Application.ActiveDocument.Styles.get_Item(ref obj);
object listObject = testStyle;
tx2.wordApp.Selection.set_Style(ref listObject);
                    
tx2.InsertFromStreamGzip(bs.Contents);
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }


        private void InitExe(string parentid, System.Data.DataTable dts, IList<LayoutContent> ls)
        {
            DataRow[] rows = dts.Select(string.Format("parentid='{0}'", parentid));

            foreach (DataRow row in rows)
            {
                LayoutContent lc = new LayoutContent();
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

























        //private void InitExe(string parentid, DataTable dts, IList<LayoutContent> ls)
        //{
        //    DataRow[] rows = dts.Select(string.Format("parentid='{0}'", parentid));

        //    foreach (DataRow row in rows)
        //    {

        //        try
        //        {
        //            if (row["Contents"] != DBNull.Value)
        //            {
        //                byte[] bt = null;
        //                try { bt = (byte[])row["Contents"]; }
        //                catch { }
        //                LayoutContent lc = new LayoutContent();
        //                lc.ChapterName = row["ChapterName"].ToString();

        //                if (bt != null)
        //                {
        //                    lc.Contents = bt;
        //                }
        //                ls.Add(lc);
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //        }
        //        InitExe(row["UID"].ToString(), dts, ls);
        //    }
        //}

        private void InitAdd(IList<LayoutContent> ls, TONLI.BZH.UI.DSOFramerWordControl tx2)
        {
            foreach (LayoutContent bs in ls)
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
            FormLayoutType flt = new FormLayoutType();
            flt.Type = layoutUID+"|1";
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
            FormLayoutType flt = new FormLayoutType();
            flt.Type = layoutUID + "|2";
            flt.FLC = this;
            flt.ShowDialog();
        }



    }
}