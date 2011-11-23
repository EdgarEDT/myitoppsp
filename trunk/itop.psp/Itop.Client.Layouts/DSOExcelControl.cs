using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
//using Office = Microsoft.Office.Core;
using System.IO;
using System.Reflection;
using TONLI.BZH.UI;

namespace Itop.Client.Layouts
{
    public partial class DSOExcelControl : UserControl
    {
        private bool isdispalytitlebar = true;
        private bool isdispalymenubar = true;
        private bool isdispalytoolbar = true;

        private Microsoft.Office.Interop.Excel.Application excelapp;

        Excel.Workbook wk;
        string tempPath;//临时文件目录
        string fileName;//文件路径
        bool isTempFile = false;//是否临时文件

        string desktopPath;
        public event EventHandler OnFileSaved;//保存文件时发生
        bool isModified;//文档是否被修改
        bool isReadOnly = false;

        object Nothing = System.Type.Missing;
        object missing = System.Type.Missing;

        


        public byte[] FileDataGzip
        {
            get
            {
                byte[] buff = new byte[0];
                if (File.Exists(fileName))
                {
                    string filecopy = fileName + "_";
                    string gzFile = string.Empty;
                    //复制文件
                    File.Copy(fileName, filecopy);
                    //压缩文件
                    try
                    {
                        gzFile = GZipHelper.Compress(filecopy);

                    }
                    catch (Exception err)
                    {
                        throw new Exception("压缩失败。", err);
                    }
                    finally
                    {
                        File.Delete(filecopy);
                    }
                    //读取文件
                    using (FileStream fs = File.OpenRead(gzFile))
                    {
                        buff = new byte[(int)fs.Length];
                        fs.Read(buff, 0, (int)fs.Length);
                    }

                    File.Delete(gzFile);

                }
                return buff;
            }
            set
            {
                if (value == null || value.Length == 0) return;
                string gzFile = GetTempFileName() + ".gz";
                //创建文件
                using (FileStream fs = File.Create(gzFile))
                {
                    fs.Write(value, 0, value.Length);
                }

                string file2 = string.Empty;

                //解压文件
                try
                {
                    file2 = GZipHelper.UnCompress(gzFile);
                }
                catch (Exception err)
                {
                    throw new ApplicationException("不是有效的压缩文件.\n" + err.Message);
                }
                finally
                {
                    File.Delete(gzFile);
                }

                //打开文件
                FileOpen(file2);
                fileName = file2;
                isTempFile = true;
            }
        }



        public DSOExcelControl()
        {
            try
            {
                initCom();
            }
            catch { MessageBox.Show("启动Excel失败，检查系统是否已安装Office"); return; }
            InitializeComponent();
            desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            tempPath = Path.GetTempPath();

        }
        private void initCom()
        {
            System.Diagnostics.Process[] pTemp = System.Diagnostics.Process.GetProcesses();
            int count = 0;
            foreach (System.Diagnostics.Process pTempProcess in pTemp)
            {
                if ((pTempProcess.ProcessName.ToLower() == ("excel").ToLower()) || (pTempProcess.ProcessName.ToLower()) == "excel.exe" || (pTempProcess.ProcessName.ToLower()) == "winexcel.exe" || (pTempProcess.ProcessName.ToLower()) == "winexcel")
                {
                    count++;
                }
            }
            // 保持有两个WordApp

            for (int i = 0; i < (2 - count); i++)
            {
                excelapp = new Excel.ApplicationClass();
            }
        }

        #region 公共属性
        public bool IsDispalyTitleBar
        {
            get { return isdispalytitlebar; }
            set
            {
                isdispalytitlebar = value;
                if (isdispalytitlebar == false)
                    axFramerControl1.Titlebar = false;
                else
                    axFramerControl1.Titlebar = true;
            }
        }

        public bool IsDispalyMenuBar
        {
            get
            {
                return isdispalymenubar;

            }
            set
            {
                isdispalymenubar = value;

            }
        }
        public bool IsDispalyToolBar
        {
            get
            {
                return isdispalytoolbar;

            }
            set
            {
                isdispalytoolbar = value;

            }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(0)]
        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set { isReadOnly = value; }
        }
        public AxDSOFramer.AxFramerControl AxFramerControl
        {
            get
            {
                return axFramerControl1;
            }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(0)]
        public bool IsModified
        {
            get { return isModified; }
            set { isModified = value; }
        }
        [Browsable(false)]
        public bool IsTempFile
        {
            get { return isTempFile; }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(0)]
        public string TempPath
        {
            get
            {
                if (string.IsNullOrEmpty(tempPath))
                {
                    tempPath = Application.StartupPath + "\\LocalSettings\\TEMP";
                }
                if (!Directory.Exists(tempPath))
                {
                    Directory.CreateDirectory(tempPath);
                }

                return tempPath;
            }
            set
            {
                tempPath = value;
            }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(0)]
        public byte[] FileData
        {
            get
            {
                byte[] buff = new byte[0];
                if (File.Exists(fileName))
                {
                    string filecopy = fileName + "_";
                    File.Copy(fileName, filecopy);

                    using (FileStream fs = File.OpenRead(filecopy))
                    {
                        buff = new byte[(int)fs.Length];
                        fs.Read(buff, 0, (int)fs.Length);
                    }
                    File.Delete(filecopy);

                }
                return buff;
            }
            set
            {
                if (value == null || value.Length == 0) return;
                string file2 = GetTempFileName();

                using (FileStream fs = File.Create(file2))
                {
                    fs.Write(value, 0, value.Length);
                }

                FileOpen(file2);
                fileName = file2;
                isTempFile = true;
            }
        }

        [Browsable(false)]

        public string FileName
        {
            get { return fileName; }
        }
        public bool IsDirty
        {
            get
            {
                bool ret = false;
                try
                {
                    ret = this.axFramerControl1.IsDirty;
                }
                catch { }
                return ret;
            }
        }
        #endregion

        #region axFramerControl1 事件

        void axFramerControl1_OnDocumentClosed(object sender, EventArgs e)
        {

            if (isTempFile && File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            fileName = string.Empty;
            isTempFile = false;
            isModified = false;
        }

        void axFramerControl1_OnDocumentOpened(object sender, AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent e)
        {
            fileName = e.file;
        }

        ////void axFramerControl1_BeforeDocumentSaved(object sender, AxDSOFramer._DFramerCtlEvents_BeforeDocumentSavedEvent e)
        ////{

        ////}

        ////void axFramerControl1_BeforeDocumentClosed(object sender, AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent e)
        ////{
        ////    //(e.document as Word.Document).Application.DocumentChange -= new Microsoft.Office.Interop.Word.ApplicationEvents4_DocumentChangeEventHandler(Application_DocumentChange);

        ////}

        void axFramerControl1_OnFileCommand(object sender, AxDSOFramer._DFramerCtlEvents_OnFileCommandEvent e)
        {
            switch (e.item)
            {
                case DSOFramer.dsoFileCommandType.dsoFileNew:
                    e.cancel = true;
                    FileNew();
                    break;
                case DSOFramer.dsoFileCommandType.dsoFileOpen:
                    e.cancel = true;
                    FileOpen();
                    break;
                case DSOFramer.dsoFileCommandType.dsoFileClose:
                    e.cancel = true;
                    FileClose();
                    break;
                case DSOFramer.dsoFileCommandType.dsoFileSave:
                    e.cancel = true;

                    FileSave();
                    break;
                case DSOFramer.dsoFileCommandType.dsoFileSaveAs:
                    e.cancel = true;
                    // FileSaveAs();
                    break;
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        ///  //生成一个唯一的临时文件名
        /// </summary>
        /// <returns></returns>
        private string GetTempFileName()
        {
            return TempPath + "\\~" + Guid.NewGuid().ToString() + ".xls";
        }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {

            wk = axFramerControl1.ActiveDocument as Excel.Workbook;
            try
            {
                wk.Close(Missing.Value, Missing.Value, Missing.Value);

                excelapp.Quit();

                if (wk != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(wk);
                    wk = null;
                }
                if (wk != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(wk);
                    wk = null;
                }
                GC.Collect();
            }
            catch
            {
                if (wk != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(wk);
                    wk = null;
                }
                if (wk != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(wk);
                    wk = null;
                }
                GC.Collect();

            }
        }
        /// <summary>
        /// 新建文件
        /// </summary>
        public void FileNew()
        {
            this.axFramerControl1.CreateNew("Excel.Sheet.8");

            //////wk = axFramerControl1.ActiveDocument as Excel.Workbook;

            //////if (isdispalytitlebar == false)
            //////    axFramerControl1.Titlebar = false;

            //////if (isdispalymenubar == false)
            //////{
            //////    wk.Application.CommandBars[2].Visible = false;
            //////}
            //////else
            //////{
            //////    wk.Application.CommandBars[2].Visible = true;
            //////}


            //////if (isdispalytoolbar == false)
            //////{
            //////    wk.Application.CommandBars[3].Visible = false;
            //////    wk.Application.CommandBars[4].Visible = false;
            //////}
            //////else
            //////{
            //////    wk.Application.CommandBars[3].Visible = true;
            //////    wk.Application.CommandBars[4].Visible = true;
            //////}
        }
        /// <summary>
        /// 打开指定文件
        /// </summary>
        public void FileOpen()
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Excel文件(*.xls)|*.xls";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string copyfile = GetTempFileName();

                    File.Copy(dlg.FileName, copyfile);
                    FileOpen(copyfile);
                    isTempFile = true;
                    fileName = copyfile;
                }
            }
        }
        public void FilePrintDialog()
        {
            axFramerControl1.ShowDialog(DSOFramer.dsoShowDialogType.dsoDialogPrint);
        }
        /// <summary>
        /// 打开文件指定文件
        /// </summary>
        /// <param name="fileName"></param>
        public void FileOpen(string filename)
        {
            axFramerControl1.Open(filename);//, false, null, null, null);
        }
        /// <summary>
        /// 读取文件内容放入buff缓冲区
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public byte[] FileData1(string filename)
        {

            byte[] buff = new byte[0];

            string filecopy = filename + "_";
            File.Copy(filename, filecopy);

            using (FileStream fs = File.OpenRead(filecopy))
            {
                buff = new byte[(int)fs.Length];
                fs.Read(buff, 0, (int)fs.Length);
            }
            File.Delete(filecopy);
            return buff;

        }
        /// <summary>
        /// 打开流文件
        /// </summary>
        /// <param name="filename"> 文件名 </param>
        /// <param name="buff">数组缓冲区</param>
        public void OpenStreamFile(string filename, byte[] buff)
        {
            FileStream Fs = new FileStream(filename, FileMode.Open);
            Fs.Read(buff, 0, (int)Fs.Length);
            Fs.Flush();
            Fs.Close();
            Fs = null;
            return;
        }
        /// <summary>
        /// 文档发生变化时发生
        /// </summary>
        void Application_DocumentChange()
        {

            isModified = true;
        }
        /// <summary>
        /// 关闭文件
        /// </summary>
        public void FileClose()
        {
            axFramerControl1.Close();
        }


        public void FileSave()
        {

            string file = axFramerControl1.DocumentFullName;

            if (string.IsNullOrEmpty(file))
            {
                file = GetTempFileName();
                axFramerControl1.Save(file, true, null, null);
                fileName = file;
                isTempFile = true;

            }
            else
            {
                axFramerControl1.Save();
            }

            if (OnFileSaved != null)
            {
                OnFileSaved(this, null);
            }
            isModified = false;
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="overwrite">是否覆盖已有文件</param>
        public void FileSave(string filename)
        {
            Excel.Workbook doc = axFramerControl1.ActiveDocument as Excel.Workbook;



            doc.Saved = true;
            doc.SaveCopyAs(filename);
            excelapp.Quit();

            GC.Collect();//强行销毁

        }


        /// <summary>
        ///  保存流文件
        /// </summary>
        /// <param name="fname"></param>
        /// <param name="buff"></param>
        public void saveStreamFile(string fname, byte[] buff)
        {
            try
            {
                FileStream Fs = new FileStream(fname, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                Fs.Write(buff, 0, (int)buff.Length);
                Fs.Flush();
                Fs.Close();
                return;
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private void axFramerControl1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            isModified = true;
        }

        private string saveFile(byte[] buff)
        {

            string file2 = GetTempFileName();
            //创建文件
            using (FileStream fs = File.Create(file2))
            {
                fs.Write(buff, 0, buff.Length);
                fs.Flush();
            }

            return file2;
        }
        public void DoPageSetupDialog()
        {
            axFramerControl1.ShowDialog(DSOFramer.dsoShowDialogType.dsoDialogPageSetup);
        }

        #region 插入对象
        /// <summary>
        ///插入sheet页    
        /// </summary>
        public void DoInsertSheet()
        {
            Excel.Workbook doc = axFramerControl1.ActiveDocument as Excel.Workbook;
            doc.Application.Sheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);

        }
        #endregion
        /// <summary>
        /// 是否显示菜单栏函数
        /// </summary>
        /// <param name="menubarvisible">隐藏菜单false，否true</param>
        public void Onisdispalymenubar(bool menubarvisible)
        {
            Excel.Workbook doc = axFramerControl1.ActiveDocument as Excel.Workbook;
            if (menubarvisible == true)
                doc.Application.CommandBars[2].Visible = true;
            else
                doc.Application.CommandBars[2].Visible = false;
            

        }
        /// <summary>
        ///  是否显示工具栏函数
        /// </summary>
        /// <param name="toolbarvisible">隐藏菜单false，否true</param>
        public void Onisdispalytoolbar(bool toolbarvisible)
        {
            Excel.Workbook doc = axFramerControl1.ActiveDocument as Excel.Workbook;
            if (toolbarvisible == true)
            {
                doc.Application.CommandBars[3].Visible = true;
                doc.Application.CommandBars[4].Visible = true;
            }
            else
            {
                doc.Application.CommandBars[3].Visible = false;
                doc.Application.CommandBars[4].Visible = false;
            }

        }

        /// <summary>
        /// 是否显示公式栏函数
        /// </summary>
        /// <param name="FormulaBarVisible">隐藏菜单false，否true</param>
        public void OnisdislayFormulaBar(bool FormulaBarVisible)
        {
            Excel.Workbook doc = axFramerControl1.ActiveDocument as Excel.Workbook;
            doc.Application.DisplayFormulaBar = FormulaBarVisible;
        }
        public void NOdispalyOtherbar()
        {
            Excel.Workbook doc = axFramerControl1.ActiveDocument as Excel.Workbook;
            for (int j = 5; j < 30; j++)
                doc.Application.CommandBars[j].Visible = false;
            doc.Application.DisplayFormulaBar = false;

        }
        public void Paste()
        {
            try
            {
                axFramerControl1.Focus();
                SendKeys.Send("^V");
                //PostMessage((int)axFramerControl1.Handle, 0x0302, 86, 0);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            initCom();
        }

    }
}
