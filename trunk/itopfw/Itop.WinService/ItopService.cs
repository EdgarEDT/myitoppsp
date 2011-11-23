using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Runtime.Remoting;
using System.Xml;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;

namespace Itop.WinService {
    public partial class ItopService : ServiceBase {
        private static string serviceName = "ItopWinService0";
        public static string GetServiceName() {
            serviceName = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location).AppSettings.Settings["serviceName"].Value;
            return serviceName;
        }        
        public ItopService() {
            InitializeComponent();
            this.ServiceName = GetServiceName();
        }
        //Autolog                 是否自动写入系统的日志文件
        //CanHandlePowerEvent     服务时候接受电源事件
        //CanPauseAndContinue     服务是否接受暂停或继续运行的请求
        //CanShutdown             服务是否在运行它的计算机关闭时收到通知，以便能够调用 OnShutDown 过程
        //CanStop                 服务是否接受停止运行的请求
        //ServiceName             服务名
        protected override void OnStart(string[] args) {
            //FileStream fs = new FileStream(@"d:\Itop.WinService.txt", FileMode.OpenOrCreate, FileAccess.Write);
            //StreamWriter m_streamWriter = new StreamWriter(fs);
            //m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            //m_streamWriter.WriteLine("Itop.WinService:服务启动 " + DateTime.Now.ToString() + "\n");            
            //m_streamWriter.Flush();
            //m_streamWriter.Close();
            //fs.Close();
            //ServiceController
            LoadAssembly();
            StartRemote();            
        }

        protected override void OnStop() {
            //FileStream fs = new FileStream(@"d:\Itop.WinService.txt", FileMode.OpenOrCreate, FileAccess.Write);
            //StreamWriter m_streamWriter = new StreamWriter(fs);
            //m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            //m_streamWriter.WriteLine(" Itop.WinService:服务停止 " + DateTime.Now.ToString() + "\n");            
            //m_streamWriter.Flush();
            //m_streamWriter.Close();
            //fs.Close();           

        }
        private void Running() {
            //LoadAssembly();
            //StartRemote();
            //Process.Start(@"E:\do1tnet\公司项目\CRM\TFK\Itop\output\Server\Itop.Server.exe");
            while (true) {
                //你的处理
                Thread.Sleep(1000);//例如让线程休眠5秒
            }
        }

        public static void LoadAssembly() {
            XmlDocument doc = new XmlDocument();
            doc.Load(Application.StartupPath+"\\ExAssemly.xml");

            XmlNodeList list = doc.GetElementsByTagName("ExAssembly");
            XmlNode node = null;
            if (list.Count > 0) {

                node = list[0];
                string[] assemlies = node.InnerText.Split(","[0]);
                foreach (string str in assemlies) {
                    try {
                        Assembly.LoadFile(Application.StartupPath + "\\" + str.Trim());
                    } catch (Exception e) { MessageBox.Show(e.Message); }
                }
            }
        }
        public void StartRemote() {
            string fileName = Application.StartupPath+"\\Itop.Server.exe.config";
            try {
                RemotingConfiguration.Configure(fileName, false);
            } catch (Exception e) {
                //Program.Log.Error("Remoting服务启动失败.", e);
            }
        }
    }
}
