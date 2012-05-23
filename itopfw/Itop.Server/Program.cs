			
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using System.Runtime.Remoting;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using Itop.Common.Logging;
using System.Configuration;
using System.Xml;
using Microsoft.Win32;


namespace Itop.Server {
    

    static class Program {

        public static ILogger Log;
        [DllImport("librcode2.dll", EntryPoint = "VerifyRCode")]
        private static extern int VerifyRCode(
            string lpszProductID,
            string lpszRCode
            );




        private static bool Regstate()
        {
            bool state = true;
            //暂时关闭注删功能，如有需要打开面面代码即可恢复注册
            /*
            string AppSysID = "Server";// Itop.Common.ConfigurationHelper.GetValue("AppSysID");
            RegistryKey rk = Registry.Users.CreateSubKey(".DEFAULT\\Software\\Itopsoft");
            if (rk.GetValue(AppSysID) == null)
            {
                state = false;
            }
            if (rk.GetValue(AppSysID) != null)
            {
                if (VerifyRCode(AppSysID, rk.GetValue(AppSysID).ToString()) == 0)
                {
                    state = false;
                }
                else
                {
                    state = true;
                }
            }
            if (System.Environment.MachineName.ToLower() == "rabbit") return true;
            */
            return state;

        }
        [STAThread]
        static void Main() {
            //程序只能运行一次实例
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Process instance = RunningInstance();
            //全局服务端只准运行一个实例
            if (Settings.IsOneServer=="1")
            {
                if (instance != null)
                {
                    HandleRunningInstance(instance);
                    Application.Exit();
                    return;
                }
                
            }
            
       
        reg:
            //RegistryKey rk = Registry.Users.CreateSubKey(".DEFAULT\\Software\\Itopsoft\\sbxj");
            //if (rk.GetValue("regkey") == null)
            //{
            //    frmReg fg = new frmReg();
            //    fg.ShowDialog();
            //}
            //if (rk.GetValue("regkey") != null)
            //{
            //    if (VerifyRCode("sbxj2007", rk.GetValue("regkey").ToString()) == 0)
            //    {
            //        frmReg fg = new frmReg();
            //        fg.ShowDialog();
            //    }
            //}



            if (!Regstate())
            {
                frmReg fg = new frmReg();
                fg.ShowDialog();
                return;
            }


            try
            {
                //把外部程序集加载到当前程序域中
                //Assembly.LoadFile(Application.StartupPath+"\\Itop.Domain.Ex.dll");

                LoadAssembly();
               

            } catch (Exception e) { MessageBox.Show(e.Message); }
            Log = Log4NetLoggerFactory.CreateLogger<FrmServerManager>("ItopServer");
            Log.Info("***************应用程序服务器准备启动****************");
           
            try {                
                Application.Run(new FrmServerManager());
            } catch (Exception ex) {
                Log.Error(string.Format("系统出现意外的错误\n\n错误信息：{0}", ex.Message));
                MessageBox.Show(string.Format("系统出现意外的错误\n\n错误信息：{0}", ex.Message));
                Application.Exit();
            }
        }

        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;

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
        public static void HandleRunningInstance(Process instance) {
           ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL);
           SetForegroundWindow(instance.MainWindowHandle);
        }
        public static void LoadAssembly() {
            XmlDocument doc = new XmlDocument();
            doc.Load("ExAssemly.xml");

            XmlNodeList list = doc.GetElementsByTagName("ExAssembly");
            XmlNode node =null;
            if (list.Count > 0) {

                node = list[0];
                string[] assemlies= node.InnerText.Split(","[0]);
                foreach (string str in assemlies) {
                    try {
                        Assembly.LoadFile(Application.StartupPath + "\\" + str.Trim());
                    } catch (Exception e) { MessageBox.Show(e.Message); }

                }
            }
            
        }
        
    }
}
