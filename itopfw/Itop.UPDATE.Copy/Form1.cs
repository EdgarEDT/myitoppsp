using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Itop.UPDATE.Copy {
    public partial class Form1 : Form {
        public Form1(string file) {
            fileName = file;
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);           

        }
        public void UpdateFile() {
            DataTable table = new DataTable();
            table.ReadXml(fileName);

            KillLanMsgProcess();
            Thread.Sleep(2000);
            //�����ļ�

            StringBuilder strbuilder = new StringBuilder();
            string filepath = string.Empty;
            foreach (DataRow row in table.Rows) {
                filepath = row["filepath"].ToString();
                if (filepath.ToLower().LastIndexOf("Itop.update.copy") >= 0) continue;
                string updatefile = Application.StartupPath + "\\update\\" + filepath;
                string appfile = Application.StartupPath + filepath;
                labFile.Text = filepath;
                Application.DoEvents();
                File.Copy(updatefile, appfile, true);
                strbuilder.AppendLine(filepath);
            }
            MessageBox.Show("�������!", "����");

            //���������ϵͳ

            System.Diagnostics.Process.Start("Itop.exe");
            Application.Exit();
        }
        string fileName;
        //�ر������������е�Itop.exe����
        private void KillLanMsgProcess() {
            System.Diagnostics.Process[] pTemp = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process pTempProcess in pTemp)
                if ((pTempProcess.ProcessName.ToLower() == ("Itop").ToLower()) || (pTempProcess.ProcessName.ToLower()) == ("Itop.exe").ToLower())
                    pTempProcess.Kill();
        }
    }
}