				
using System;
using System.Collections.Generic;
using System.Text;

using System.Configuration;

namespace Itop.Client.Option {
    /// <summary>
    /// 客户端配置
    /// </summary>
    static public class Settings {
        private const string LastLoginUserNumber = "lastLoginUserNumber";

        /// <summary>
        /// 最近一次登录的人员的工号
        /// </summary>
        /// <returns>工号</returns>
        static public string GetLastLoginUserNumber() { 
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[LastLoginUserNumber] == null)
                return string.Empty;
            else
                return config.AppSettings.Settings[LastLoginUserNumber].Value;
        }

        /// <summary>
        /// 设置最近一次登录的人员的工号
        /// </summary>
        /// <param name="userNumber">工号</param>
        static public void SetLastLoginUserNumber(string userNumber) {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[LastLoginUserNumber] != null)
                config.AppSettings.Settings.Remove(LastLoginUserNumber);
            config.AppSettings.Settings.Add(LastLoginUserNumber, userNumber);
            config.Save();
          
            
        }
        /// <summary>
        /// 设置经伟度
        /// </summary>
        /// <param name="jd"></param>
        /// <param name="wd"></param>
        static public void SetJWDvalue(double jd, double wd)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings["jd"] != null)
                config.AppSettings.Settings.Remove("jd");
            config.AppSettings.Settings.Add("jd", jd.ToString());

            if (config.AppSettings.Settings["wd"] != null)
                config.AppSettings.Settings.Remove("wd");
            config.AppSettings.Settings.Add("wd", wd.ToString());

            config.Save();
        }
        static public void SetCityName(string cityname)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings["CityName"] != null)
                config.AppSettings.Settings.Remove("CityName");
            config.AppSettings.Settings.Add("CityName", cityname);        
            config.Save();
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
