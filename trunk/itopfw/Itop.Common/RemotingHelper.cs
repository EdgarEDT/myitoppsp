
using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.Remoting;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace Itop.Common {
    /// <summary>
    /// 远程调用助手类
    /// </summary>
    static public class RemotingHelper {
        private static string serverAddress = string.Empty;
        private static string serverPort = string.Empty;
        private static string serverProtocol = string.Empty;
        private static string serverUrl = string.Empty;
        private static string configFileName = string.Empty;

        public static bool isWeb = false;
        static RemotingHelper() {
            InitConfig();
        }
        private static void InitConfig() {
            Configuration config = getConfig();
            try {
                serverAddress = config.AppSettings.Settings["serverAddress"].Value;
                serverPort = config.AppSettings.Settings["serverPort"].Value;
                serverProtocol = config.AppSettings.Settings["serverProtocol"].Value;
            } catch {
                try {
                    serverAddress = "localhost";
                    serverProtocol = "TCP";
                    serverPort = "5150";
                    config.AppSettings.Settings.Add("serverAddress", serverAddress);
                    config.AppSettings.Settings.Add("serverPort", serverPort);
                    config.AppSettings.Settings.Add("serverProtocol", serverProtocol);
                    config.Save();
                } catch { }
            }
            
            updateServerUrl(false);
        }
        private static Configuration getConfig() {

            Configuration config = null;
            string exePath = configFileName;
            if (isWeb) {
                try {
                    config = ConfigurationManager.OpenExeConfiguration(exePath);
                } catch { }
            } else {
                if (!string.IsNullOrEmpty(configFileName))
                    exePath = Application.StartupPath + "\\" + configFileName;

                if (!string.IsNullOrEmpty(exePath) && File.Exists(exePath)) {
                    try {
                        string path = Path.GetDirectoryName(exePath) + "\\" + Path.GetFileNameWithoutExtension(exePath);
                        config = ConfigurationManager.OpenExeConfiguration(path);
                    } catch {
                        MessageBox.Show("Remoting配置加载失败", "提示");
                    }
                } else {
                    try {
                        config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    } catch { }
                }
            }
            return config;

        }
        /// <summary>
        /// Remoting服务器设置文件
        /// </summary>
        public static string ConfigFileName {
            get { return RemotingHelper.configFileName; }
            set {
                RemotingHelper.configFileName = value;
                InitConfig();
            }
        }
        /// <summary>
        /// 服务器IP地址
        /// </summary>
        private static string ServerIP {
            get {
                if (serverUrl == string.Empty) {
                    Configuration config = getConfig();
                    serverAddress = config.AppSettings.Settings["serverAddress"].Value;
                    serverPort = config.AppSettings.Settings["serverPort"].Value;
                    serverProtocol = config.AppSettings.Settings["serverProtocol"].Value;
                    updateServerUrl(false);
                }
                return serverUrl;
            }
        }
        private static void updateServerUrl(bool updateConfig) {
            if (updateConfig) {
                try {
                    Configuration config = getConfig();
                    config.AppSettings.Settings["serverAddress"].Value = serverAddress;
                    config.AppSettings.Settings["serverPort"].Value = serverPort;
                    config.AppSettings.Settings["serverProtocol"].Value = serverProtocol;
                    config.Save();
                } catch { }
            }
            serverUrl = string.Format("{0}://{1}:{2}/", serverProtocol, serverAddress, serverPort);
        }
        /// <summary>
        /// 服務器IP或機器名
        /// </summary>
        public static string ServerAddress {
            get {
                return serverAddress;
            }
            set {
                serverAddress = value;
                updateServerUrl(true);
            }
        }
        /// <summary>
        /// 服務器端口
        /// </summary>
        public static string ServerPort {
            get {
                return serverPort;
            }
            set {
                serverPort = value;
                updateServerUrl(true);
            }
        }
        /// <summary>
        /// 服务信道类型http或tcp
        /// </summary>
        public static string ServerProtocol {
            get {
                return serverProtocol;
            }
            set {
                serverProtocol = value;
                updateServerUrl(true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetRemotingService<T>() {
            foreach (WellKnownClientTypeEntry entry in RemotingConfiguration.GetRegisteredWellKnownClientTypes())
                if (entry.ObjectType == typeof(T))
                    return (T)Activator.GetObject(typeof(T), ServerIP + entry.ObjectUrl);

            return default(T);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objecturl"></param>
        /// <returns></returns>
        public static T GetRemotingService<T>(string objecturl) {
            foreach (WellKnownClientTypeEntry entry in RemotingConfiguration.GetRegisteredWellKnownClientTypes())
                if (entry.ObjectType == typeof(T) && entry.ObjectUrl == objecturl)
                    return (T)Activator.GetObject(typeof(T), ServerIP + entry.ObjectUrl);

            return default(T);
        }
    }
}
