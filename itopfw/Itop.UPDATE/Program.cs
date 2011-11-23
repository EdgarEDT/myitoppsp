using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.Remoting;

namespace Itop.UPDATE {
    static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // 读取Remoting配置文件
            string fileName = Application.StartupPath + @"\Itop.exe.config";
            if (!System.IO.File.Exists(fileName)) {
                Itop.Common.MsgBox.Show("配置文件不存在，系统无法启动");
                return;
            }

            try {
                RemotingConfiguration.Configure(fileName, false);
            } catch {
                Itop.Common.MsgBox.Show("配置文件被破坏，请与软件服务商联系");
                return;
            }
            FrmUpdate frm = new FrmUpdate();
            frm.Show();
            if (args.Length == 1 && args[0].ToString() == "RUN")
                if (frm.UpdateFile()) {
                    frm.Close();
                    Application.Exit();
                    return;
                }
            Application.Run();

        }
    }
}