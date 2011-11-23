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
        /// <param name="isRun">�Ƿ��������и���</param>
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
                MsgBox.Show("����������ʧ�ܣ��������������Ƿ���ȷ��");

                return false;
            }

            this.progressBar1.PositionMax = maxPos;
            IList list = Services.BaseService.GetList<SAppUpdate>();
            string fileDir = Application.StartupPath + "\\Update";
            Directory.CreateDirectory(fileDir);
            //�����ļ�
            foreach (SAppUpdate obj in list) {
                curPos += (int)obj.FILESIZE;
                this.progressBar1.Position = curPos;
                this.progressBar1.Text = ((int)(curPos * 100 / maxPos)).ToString() + "%";
                this.progressBar1.Refresh();


                //this.listView1.Items.Add(obj.FILENAME).Tag=obj;
                string updatefile = fileDir + obj.FILEPATH;//����·��

                string appfile = Application.StartupPath + obj.FILEPATH;//����·��

                FileInfo fi = new FileInfo(appfile);
                //��������ļ��ȵ��ļ��ɣ�����������
                string strDate1 = fi.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");

                if (fi.Exists && strDate1.CompareTo(obj.RQ) >= 0)
                    continue;
                //��ʾ�����ļ�
                label1.Text = obj.FILENAME;

                label1.Refresh();
                SAppUpdate obj2 = Services.BaseService.GetOneByKey<SAppUpdate>(obj);

                //���ļ�д������·��
                using (FileStream fs = File.Create(updatefile)) {

                    fs.Write(obj2.FILEBLOB, 0, obj2.FILEBLOB.Length);

                    fs.Flush();
                    fs.Close();
                }

                FileInfo fi2 = new FileInfo(updatefile);

                fi2.LastWriteTime = DateTime.Parse(obj.RQ);
                //��¼�����ļ�
                updateList.Add(obj);

            }

            if (updateList.Count == 0) {
                MessageBox.Show("����ϵͳ�Ѿ������°汾������Ҫ������", "��ʾ");
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
                MsgBox.Show("ϵͳ����ʧ�ܣ�");
            }
            
            return true;

            //KillLanMsgProcess();
            ////�����ļ�
            //label1.Text = "��ʼ��������...";
            //StringBuilder strbuilder = new StringBuilder();
            //foreach (string filepath in updateList) {
            //    string updatefile = Application.StartupPath + "\\update\\" + filepath;
            //    string appfile = Application.StartupPath + filepath;
            //    File.Copy(updatefile, appfile,true);
            //    strbuilder.AppendLine(filepath);
            //}
            //MessageBox.Show(strbuilder.ToString(),"�����б�");

            ////���������ϵͳ
            //label1.Text = "���ݸ�����ϣ�";
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
        private void KillLanMsgProcess()//�ر������������е�Itop.exe����
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