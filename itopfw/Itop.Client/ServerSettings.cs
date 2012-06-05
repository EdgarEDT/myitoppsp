using System;
using System.Collections.Generic;
using System.Text;

using System.Configuration;
using System.Xml;
using System.Collections;
using System.Windows.Forms;


namespace Itop.Client
{
   internal static class ServerSettings
    {
         /// <summary>
        /// 服务器端配置
        /// </summary>

       static ServerSettings()
       {
            //remoting配置 
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            XmlDocument doc = new XmlDocument();
            //file1 =  config.FilePath;
            file1 = Application.StartupPath + "\\Server\\Itop.Server.exe.config";
            doc.Load(file1);
            XmlNode node = doc.SelectSingleNode("configuration/system.runtime.remoting/application/channels/channel");

            if (node != null && node is XmlElement) {
                channelElement = node as XmlElement;
            }
            //数据库配置
            XmlDocument doc2 = new XmlDocument();
            doc2.Load(file2);
            if (doc2 != null) {
                dbDictionary = new Dictionary<string, XmlElement>();
                XmlNodeList list = doc2.GetElementsByTagName("add");
                foreach (XmlElement element in list) {
                    dbDictionary[element.Attributes["key"].Value] = element;
                }
            }            
        }
        private static string file1 = "";
        private static string file2 = "Server\\dataconfig\\database.config";
        private static IDictionary<string, XmlElement> dbDictionary;
        private static string remotingPort = string.Empty;//端口
        private static string remotingProtocol = string.Empty;//协议
        private static string dataServer = string.Empty;// 数据库服务器
        private static string database = string.Empty;// 数据库名
        private static string uid = string.Empty;// 用户
        private static string pwd = string.Empty;// 口令
        private static string isOneServer = string.Empty;//单实例
        private static string processid = string.Empty;
        private static XmlElement channelElement;
        /// <summary>
        /// 保存配置
        /// </summary>
        public static void Save() {
            channelElement.OwnerDocument.Save(file1);
            dbDictionary["datasource"].OwnerDocument.Save(file2);
        }
        #region "属性"
        /// <summary>
        /// Remoting通道协议.目前支持两种:Http/Tcp
        /// </summary>
        public static string RemotingProtocol {
            get {
                if (remotingProtocol == string.Empty && channelElement != null) {

                    remotingProtocol = channelElement.Attributes["ref"].Value;
                }
                return ServerSettings.remotingProtocol;

            }
            set {
                ServerSettings.remotingProtocol = value;
                channelElement.Attributes["ref"].Value = value;
            }
        }
        /// <summary>
        /// 端口
        /// </summary>
        internal static string RemotingPort {
            get {
                if (remotingPort == string.Empty && channelElement != null) {
                    remotingPort = channelElement.Attributes["port"].Value;
                }
                return ServerSettings.remotingPort;
            }
            set {
                ServerSettings.remotingPort = value;
                channelElement.Attributes["port"].Value = value;
            }
        }
        /// <summary>
        /// 数据库服务器
        /// </summary>
        public static string DataServer {
            get {
                if (dataServer == string.Empty) {
                    dataServer = dbDictionary["datasource"].Attributes["value"].Value;
                }
                return ServerSettings.dataServer;
            }
            set {
                ServerSettings.dataServer = value;
                dbDictionary["datasource"].Attributes["value"].Value = value;
            }
        }

        /// <summary>
        /// 数据库名
        /// </summary>
        public static string Database {
            get {

                if (database == string.Empty) {
                    database = dbDictionary["database"].Attributes["value"].Value;
                }
                return ServerSettings.database;
            }
            set {
                ServerSettings.database = value;
                dbDictionary["database"].Attributes["value"].Value = value;
            }
        }

        /// <summary>
        /// 数据库用户
        /// </summary>
        public static string Uid {
            get {
                if (uid == string.Empty) {
                    uid = dbDictionary["userid"].Attributes["value"].Value;
                }
                return ServerSettings.uid;
            }
            set {
                ServerSettings.uid = value;
                dbDictionary["userid"].Attributes["value"].Value = value;
            }
        }

        /// <summary>
        ///  口令
        /// </summary>
        public static string Pwd {
            get {
                if (pwd == string.Empty) {
                    pwd = dbDictionary["password"].Attributes["value"].Value;
                }
                return ServerSettings.pwd;
            }
            set {
                ServerSettings.pwd = value;
                dbDictionary["password"].Attributes["value"].Value = value;
            }
        }
        /// <summary>
        ///  单实例
        /// </summary>
        public static string  IsOneServer
        {
            get
            {
                if (isOneServer == string.Empty)
                {
                    isOneServer = dbDictionary["isOneServer"].Attributes["value"].Value;
                }
                return ServerSettings.isOneServer;
            }
            set
            {
                ServerSettings.isOneServer = value;
                dbDictionary["isOneServer"].Attributes["value"].Value = value;
            }
        }
        /// <summary>
        ///  进程ID
        /// </summary>
        public static string ProcessID
        {
            get
            {
                if (processid == string.Empty)
                {
                    processid = dbDictionary["ProcessID"].Attributes["value"].Value;
                }
                return ServerSettings.processid;
            }
            set
            {
                ServerSettings.processid = value;
                dbDictionary["ProcessID"].Attributes["value"].Value = value;
            }
        }
        #endregion
    }
}