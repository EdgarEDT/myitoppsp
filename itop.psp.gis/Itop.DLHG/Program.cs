using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.Remoting;

namespace Itop.DLGH
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string fileName = Application.StartupPath + @"\Itop.DLGH.exe.config";
            if (!System.IO.File.Exists(fileName))
            {
                Itop.Common.MsgBox.Show("配置文件不存在，系统无法启动");
                return;
            }

            try
            {
                RemotingConfiguration.Configure(fileName, false);
            }
            catch
            {
                MessageBox.Show("配置文件被破坏，请与软件服务商联系");
                return;
            }
            //f3eafde4-50c5-4112-925c-c569513230f0
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmViewMain f = new frmViewMain();
            //frmSvgView f = new frmSvgView();
            //f.Open("f3eafde4-50c5-4112-925c-c569513230f0");
            Application.Run(f);
        }
    }
}