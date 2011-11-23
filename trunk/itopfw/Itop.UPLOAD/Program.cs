using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Itop.UPLOAD {
    static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // 读取Remoting配置文件
            string fileName = Application.StartupPath + @"\Itop.exe.config";
            if (!System.IO.File.Exists(fileName)) {
                Itop.Common.MsgBox.Show("配置文件不存在，系统无法启动");
                return;
            }

            try {
                System.Runtime.Remoting.RemotingConfiguration.Configure(fileName, false);
            } catch {
                Itop.Common.MsgBox.Show("配置文件被破坏，请与软件服务商联系");
                return;
            }
            Application.Run(new FrmUpload());
        }
    }
}