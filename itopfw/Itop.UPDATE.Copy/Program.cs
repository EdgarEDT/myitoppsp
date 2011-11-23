using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace Itop.UPDATE.Copy {
    static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try {
                if (!File.Exists(args[0])) {
                    MessageBox.Show("参数非法");
                    Application.Exit();
                    return;
                }
            } catch { Application.Exit(); return; }
            using (Form1 dlg = new Form1(args[0])) {
                dlg.Show();
                Application.DoEvents();
                dlg.UpdateFile();
            }

            Application.Exit();
        }
    }
}