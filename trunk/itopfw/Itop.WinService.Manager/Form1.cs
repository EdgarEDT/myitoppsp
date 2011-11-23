using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Threading;
using System.Net;
using Itop.WinService.Manager.Properties;
using System.Management;
using System.Diagnostics;
using System.IO;

namespace Itop.WinService.Manager {
    public partial class Form1 : Form {
        Dictionary<string, ServiceController> serviceList;
        public Form1() {
            InitializeComponent();
            
            serviceList = new Dictionary<string, ServiceController>();
            this.notifyIcon1.Icon =  Resources.stop1;
            Init(Dns.GetHostName());

            this.WindowState = FormWindowState.Minimized;
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            this.notifyIcon1.Visible = true;
            this.Hide();
        }
        protected override void OnClosing(CancelEventArgs e) {
            base.OnClosing(e);
            if (!this.notifyIcon1.Visible) {
                e.Cancel = true;
                this.notifyIcon1.Visible = true;
                this.Hide();
            }
        }
        private string selectedServiceName=string.Empty;
        private void Init(string machineName) {
            serviceList.Clear();
            listBox1.Items.Clear();
            ServiceController[] services=null;
            try {
                services = ServiceController.GetServices(machineName);
            } catch(Exception e) {
                MessageBox.Show(e.Message);
                return;
            }

            foreach (ServiceController sc in services) {
                if (sc.ServiceName.ToLower().StartsWith("Itop")) {
                    serviceList.Add(sc.ServiceName, sc);
                }
            }

            foreach (string key in serviceList.Keys) {
                this.listBox1.Items.Add(key);
            }
            if (this.listBox1.Items.Count > 0) {
                this.listBox1.SelectedIndex = 0;
            }
        }
        private void Start() {
            ServiceController sc = serviceList[selectedServiceName];
            sc.Start();
            select(selectedServiceName);
        }
        private void Stop() {
            ServiceController sc = serviceList[selectedServiceName];
            sc.Stop();
            select(selectedServiceName);
        }
        private void ReStart() {
            ServiceController sc = serviceList[selectedServiceName];
            sc.Stop();
            sc.Refresh();
            select(selectedServiceName);
            while (sc.Status == ServiceControllerStatus.StopPending) { sc.Refresh(); }
            sc.Start();
            select(selectedServiceName);
        }
        private void Setup() {
            using (FrmSetup dlg = new FrmSetup()) {
                if (dlg.ShowDialog() == DialogResult.OK) {
                    if (MessageBox.Show("参数已被修改,是否重启服务?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                        ReStart();
                    }
                }                
            }
        }
        //private void aa(){
        //    ConnectionOptions options = new ConnectionOptions();
        //    ManagementScope scope = new ManagementScope("\\\\192.168.0.22\\root\\cimv2", options);
        //    scope.Connect();
        //    ObjectQuery query = new ObjectQuery("SELECT  * FROM Win32_Process");
        //    ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
        //    ManagementObjectCollection queryCollection = searcher.Get();
        //    foreach (ManagementObject m in queryCollection) {
        //        // Display the remote computer information
        //        MessageBox.Show("Process Name : " + m["Name"]);
        //        MessageBox.Show("User Name : " + m["ProcessID"]);
        //        MessageBox.Show("ProcessID : " + m["ParentProcessID"]);
        //    }
        //    //再获取某进程使用的CPU及内存等信息
        //    ObjectQuery query2 = new ObjectQuery("SELECT * FROM Win32_PerfFormattedData_PerfProc_Process");
        //    ManagementObjectSearcher searcher2 = new ManagementObjectSearcher(scope, query);
        //    ManagementObjectCollection queryCollection2 = searcher2.Get();
        //    foreach (ManagementObject m in queryCollection2) {
        //        // Display the remote computer information
        //        MessageBox.Show("CPU : " + m["PercentProcessorTime"]);
        //        MessageBox.Show("Memory : " + m["VirtualBytes"]);
        //    }

        //}
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
            if (listBox1.SelectedIndex >= 0) {
                selectedServiceName = listBox1.SelectedItem.ToString();
                select(selectedServiceName);
            }
        }
        private void select(string service) {
            if (!serviceList.ContainsKey(service)) return;
            ServiceController sc = serviceList[service];
            sc.Refresh();
            this.label4.Text = sc.ServiceName;
            if (sc.Status == ServiceControllerStatus.Running || sc.Status== ServiceControllerStatus.StartPending) {
                this.btStart.Enabled = false;
                this.btStop.Enabled = true;
                this.label5.Text = "已启动";

            } else if (sc.Status == ServiceControllerStatus.Stopped || sc.Status == ServiceControllerStatus.StopPending) {
                this.btStop.Enabled = false;
                this.btStart.Enabled = true;
                this.label5.Text = "已停止";
            }
            bool isStart = btStop.Enabled;
            this.btSetup.Enabled = isStart;
            this.mStart.Enabled = !isStart;
            this.mStop.Enabled = isStart;
            this.mRestart.Enabled = isStart;
            this.notifyIcon1.Icon = isStart? Resources.start1:Resources.stop1;
            try {
                this.textBox2.Text = "";
                Process current = Process.GetProcessesByName(sc.ServiceName)[0];
                string exePath = current.MainModule.FileName;
                this.textBox2.Text = exePath;
                Itop.Server.Settings.Location = Path.GetDirectoryName(exePath);
                Itop.Server.Settings.Refresh();
            } catch { }
    
        }

        private void btStart_Click(object sender, EventArgs e) {

            Start();
        }

        private void btStop_Click(object sender, EventArgs e) {
            Stop();
        }

        private void button1_Click(object sender, EventArgs e) {
            
            IPHostEntry myDnsToIP = new IPHostEntry();
            try {

                
                //Dns.Resolve 方法: 将 DNS 主机名或以点分隔的四部分表示法格式的 //  IP 地址解析为 IPHostEntry实例
                myDnsToIP = Dns.GetHostEntry(textBox1.Text.ToString());
                //刷新服务列表

            } catch {
                MessageBox.Show("没有找到对应IP");
            }

            Init(myDnsToIP.HostName);

        }

        private void btStart_EnabledChanged(object sender, EventArgs e) {
            this.btSetup.Enabled = !btStart.Enabled;
        }

        private void btSetup_Click(object sender, EventArgs e) {
            Setup();
        }
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            switch (e.ClickedItem.Name) {
                case "mShow":
                    notifyIcon1_MouseDoubleClick(sender, null);
                    break;
                case "mSetup":
                    Setup();
                    break;
                case "mRestart":
                    ReStart();
                    break;
                case "mStart":
                    Start();
                    break;
                case "mStop":
                    Stop();
                    break;
                case "mExit":
                    Exit();
                    break;
                default:
                    break;
            }
        }
        private void Exit() {
            if (MessageBox.Show("是否关闭" + this.Text + "? ", "ItopServer", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) {
               
                Application.Exit();
            }

        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) {
            this.Show();
            this.notifyIcon1.Visible = false;
            if (this.WindowState == FormWindowState.Minimized) {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void timer1_Tick(object sender, EventArgs e) {
            //select(selectedServiceName);
        }

       

        private void listBox1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.F5) {
                Init(Dns.GetHostName());
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            Init(Dns.GetHostName());
        }
    }
}