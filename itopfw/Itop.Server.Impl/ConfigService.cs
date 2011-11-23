using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using Itop.Domain;
using Itop.Server.Interface;
using System.Xml;
using System.Windows.Forms;

namespace Itop.Server.Impl {
    /// <summary>
    /// 服务器端配置
    /// </summary>
    public class ConfigService: MarshalByRefObject, IConfigService {
        
        #region IConfigService 成员
        /// <summary>
        /// 获取数据库连接配置对象
        /// </summary>
        /// <returns></returns>
        public DataConfig GetDataConfig() {
            DataConfig config = new DataConfig();
            XmlDocument xml = new XmlDocument();
            xml.Load(Application.StartupPath + "\\DataConfig\\database.config");
            XmlNodeList list = xml.GetElementsByTagName("add");
            foreach (XmlNode node in list) {
                string key = node.Attributes["key"].Value;
                switch (key) {
                    case "datasource":
                        config.Datasource = node.Attributes["value"].Value;
                        break;
                    case "database":
                        config.Database = node.Attributes["value"].Value;
                        break;
                    case "userid":
                        config.Userid = node.Attributes["value"].Value;
                        break;
                    case "password":
                        config.Password = node.Attributes["value"].Value;
                        break;
                    default:
                        break;
                }
            }
            return config;
        }
        public bool SetDataConfig(DataConfig data) {
            DataConfig config = new DataConfig();
            XmlDocument xml = new XmlDocument();
            xml.Load(Application.StartupPath + "\\DataConfig\\database.config");
            XmlNodeList list = xml.GetElementsByTagName("add");
            foreach (XmlNode node in list) {
                string key = node.Attributes["key"].Value;
                switch (key) {
                    case "datasource":
                        node.Attributes["value"].Value = config.Datasource;
                        break;
                    case "database":
                        node.Attributes["value"].Value = config.Database;
                        break;
                    case "userid":
                        node.Attributes["value"].Value= config.Userid ;
                        break;
                    case "password":
                        node.Attributes["value"].Value = config.Password;
                        break;
                    default:
                        break;
                }
            }
            try {
                xml.Save(Application.StartupPath + "\\DataConfig\\database.config");
                
            } catch { return false; }
            return true;
        }
        #endregion
    }
}
