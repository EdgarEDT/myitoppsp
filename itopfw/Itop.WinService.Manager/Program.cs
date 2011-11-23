using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Diagnostics;
using System.Reflection;

namespace Itop.WinService.Manager {
    static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Process instance = RunningInstance();
            if (instance != null) {
                //HandleRunningInstance(instance);
                Application.Exit();
                return;
            }

            Application.Run(new Form1());
        }
        public static Process RunningInstance() {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes) {
                if (process.Id != current.Id) {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") ==
                        current.MainModule.FileName) {
                        return process;
                    }
                }
            }
            return null;
        }
    }
}