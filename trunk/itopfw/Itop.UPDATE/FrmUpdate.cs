using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Update;
using Itop.Common;
using System.Collections;
using System.Net;
using System.IO;
using System.Threading;

namespace Itop.UPDATE {
    public partial class FrmUpdate : Form {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRun">是否立刻运行更新</param>
        public FrmUpdate() {
            
            InitializeComponent();
        }
        protected override void OnFormClosed(FormClosedEventArgs e) {
            base.OnFormClosed(e);
            Application.Exit();
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            try {
                string str = Services.BaseService.GetObject("SelectBzhAppValue", "AppCaption") as string;
                this.Text += "-" + str;
                
            } catch { }
        }
        
        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
        }

        public bool UpdateFile() {
            button1.Enabled = false;
            int curPos = 0;
            int maxPos = 0;
            IList updateList = new ArrayList();
            try {
                maxPos = (int)Services.BaseService.GetObject("SelectSAppUpdateFileLength", null);
            } catch {
                MsgBox.Show("服务器连接失败，检查服务器设置是否正确。");

                return false;
            }

            this.progressBar1.PositionMax = maxPos;
            IList list = Services.BaseService.GetList<SAppUpdate>();
            string fileDir = Application.StartupPath + "\\Update";
            Directory.CreateDirectory(fileDir);
            //下载文件
            foreach (SAppUpdate obj in list) {
                curPos += (int)obj.FILESIZE;
                this.progressBar1.Position = curPos;
                this.progressBar1.Text = ((int)(curPos * 100 / maxPos)).ToString() + "%";
                this.progressBar1.Refresh();


                //this.listView1.Items.Add(obj.FILENAME).Tag=obj;
                string updatefile = fileDir + obj.FILEPATH;//下载路径

                string appfile = Application.StartupPath + obj.FILEPATH;//更新路径

                FileInfo fi = new FileInfo(appfile);
                //如果更新文件比地文件旧，则跳过更新
                string strDate1 = fi.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");

                if (fi.Exists && strDate1.CompareTo(obj.RQ) >= 0)
                    continue;
                //显示下载文件
                label1.Text = obj.FILENAME;

                label1.Refresh();
                SAppUpdate obj2 = Services.BaseService.GetOneByKey<SAppUpdate>(obj);

                //将文件写入下载路经
                using (FileStream fs = File.Create(updatefile)) {

                    fs.Write(obj2.FILEBLOB, 0, obj2.FILEBLOB.Length);

                    fs.Flush();
                    fs.Close();
                }

                FileInfo fi2 = new FileInfo(updatefile);

                fi2.LastWriteTime = DateTime.Parse(obj.RQ);
                //记录更新文件
                updateList.Add(obj);

            }

            if (updateList.Count == 0) {
                MessageBox.Show("您的系统已经是最新版本，不需要升级。", "提示");
                updateVerDate();
                return true;
            }
            DataTable table = DataConverter.ToDataTable(updateList);
            string strDate = Application.StartupPath + "\\Update\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
            table.WriteXml(strDate, XmlWriteMode.WriteSchema);
            if (File.Exists(Application.StartupPath + "\\Update\\Itop.update.copy.exe")) {
                File.Copy(Application.StartupPath + "\\Update\\Itop.update.copy.exe", Application.StartupPath + "\\Itop.UPDATE.Copy.exe", true);
            }
            try {

                System.Diagnostics.Process.Start(Application.StartupPath + "\\Itop.UPDATE.Copy.exe","\""+ strDate+"\"");
                updateVerDate();
                //Application.Exit();
            } catch {
                MsgBox.Show("系统更新失败！");
            }
            
            return true;

            //KillLanMsgProcess();
            ////更新文件
            //label1.Text = "开始更新数据...";
            //StringBuilder strbuilder = new StringBuilder();
            //foreach (string filepath in updateList) {
            //    string updatefile = Application.StartupPath + "\\update\\" + filepath;
            //    string appfile = Application.StartupPath + filepath;
            //    File.Copy(updatefile, appfile,true);
            //    strbuilder.AppendLine(filepath);
            //}
            //MessageBox.Show(strbuilder.ToString(),"更新列表");

            ////更新完毕起动系统
            //label1.Text = "数据更新完毕！";
            //updateVerDate();
            //System.Diagnostics.Process.Start("Itop.exe");
            //Application.Exit();       
        }
        private void button1_Click(object sender, EventArgs e) {
            if (UpdateFile()) {
                Application.Exit();
            } else {
                button1.Enabled = true;
            }
        }
        private void updateVerDate() {
            try {
                ConfigurationHelper.SetValue(Application.StartupPath + "\\Itop.exe", "UpdateDate", Services.BaseService.GetObject("SelectBzhAppValue", "applastdate").ToString());
            } catch { }
        }
        private void KillLanMsgProcess()//关闭所有正在运行的Itop.exe进程
        {
            System.Diagnostics.Process[] pTemp = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process pTempProcess in pTemp)
                if ((pTempProcess.ProcessName.ToLower() == ("Itop").ToLower()) || (pTempProcess.ProcessName.ToLower()) == ("Itop.exe").ToLower())
                    pTempProcess.Kill();
        }      
       

        private void btSetup_Click(object sender, EventArgs e) {
            using (FrmSetup dlg = new FrmSetup()) {
                dlg.TopMost = true;
                dlg.ShowDialog();
            }
        }
    }
}