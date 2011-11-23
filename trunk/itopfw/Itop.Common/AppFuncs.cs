using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Itop.Common {
    public class AppFuncs {
        public static Process RunningInstance() {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcesses();// Process.GetProcessesByName(current.ProcessName);

            //遍历正在有相同名字运行的例程
            foreach (Process process in processes) {
                //忽略现有的例程
                if (process.Id != current.Id) {
                    //确保例程从EXE文件运行
                    try {
                        if (process.MainModule.FileName == current.MainModule.FileName) {
                            //返回另一个例程实例
                            return process;
                        }
                    } catch { }
                }
            }
            //没有其它的例程，返回Null
            return null;
        }
        public static void HandleRunningInstance(Process instance) {
            //确保窗口没有被最小化或最大化
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOW);
            //设置真实例程为foreground window
            SetForegroundWindow(instance.MainWindowHandle);
        }
        public static void HandleRunningInstance(Process instance,int showType) {
            //确保窗口没有被最小化或最大化
            ShowWindowAsync(instance.MainWindowHandle, showType);
            //设置真实例程为foreground window
            SetForegroundWindow(instance.MainWindowHandle);
        }
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool
        SetForegroundWindow(IntPtr hWnd);
        public const int WS_SHOWNORMAL = 1;
        public const int WS_SHOWMIN = 2;
        public const int WS_SHOWMAX = 3;
        public const int WS_SHOW = 4;
    }
}
