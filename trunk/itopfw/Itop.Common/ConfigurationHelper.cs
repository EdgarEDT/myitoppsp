				
using System;
using System.Collections.Generic;
using System.Text;

using System.Configuration;
using System.IO;

namespace Itop.Common {
    /// <summary>
    /// 应用程序配置*.config键值获取设置
    /// </summary>
    public class ConfigurationHelper {

       /// <summary>
       /// 设置键值
       /// </summary>
       /// <param name="key"></param>
       /// <param name="value"></param>
       /// <param name="exepath"></param>
        static public void SetValue(string exepath,string key, string value ) {
            string file = exepath;
            if (Path.GetExtension(exepath).ToLower() == ".config")
                file = Path.GetFileNameWithoutExtension(exepath);

            Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            if (config.AppSettings.Settings[key] != null)
                config.AppSettings.Settings.Remove(key);
            config.AppSettings.Settings.Add(key, value);
            config.Save();
        }
        /// <summary>
        /// 获取键值
        /// </summary>
        /// <param name="exepath">应用程序名</param>
        /// <param name="key">键</param>
        /// <param name="defvalue">默认值</param>
        /// <returns></returns>
        static public string GetValue(string exepath,string key,string defvalue) {
            string file = exepath;
            if (Path.GetExtension(exepath).ToLower() == ".config")
                file = Path.GetFileNameWithoutExtension(exepath);

            Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            if (config.AppSettings.Settings[key] == null)
                return string.Empty;
            else
                return config.AppSettings.Settings[key].Value;
        }
        /// <summary>
        /// 设置键值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        static public void SetValue(string key, string value) {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[key] != null)
                config.AppSettings.Settings.Remove(key);
            config.AppSettings.Settings.Add(key, value);
            config.Save();
        }
        /// <summary>
        /// 获取键值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        static public string GetValue(string key) {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[key] == null)
                return string.Empty;
            else
                return config.AppSettings.Settings[key].Value;
        }
    }
}
