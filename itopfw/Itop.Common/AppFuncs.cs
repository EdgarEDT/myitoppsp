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

            //������������ͬ�������е�����
            foreach (Process process in processes) {
                //�������е�����
                if (process.Id != current.Id) {
                    //ȷ�����̴�EXE�ļ�����
                    try {
                        if (process.MainModule.FileName == current.MainModule.FileName) {
                            //������һ������ʵ��
                            return process;
                        }
                    } catch { }
                }
            }
            //û�����������̣�����Null
            return null;
        }
        public static void HandleRunningInstance(Process instance) {
            //ȷ������û�б���С�������
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOW);
            //������ʵ����Ϊforeground window
            SetForegroundWindow(instance.MainWindowHandle);
        }
        public static void HandleRunningInstance(Process instance,int showType) {
            //ȷ������û�б���С�������
            ShowWindowAsync(instance.MainWindowHandle, showType);
            //������ʵ����Ϊforeground window
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
