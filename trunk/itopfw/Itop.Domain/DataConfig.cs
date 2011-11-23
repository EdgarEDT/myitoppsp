//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-8-21 15:02:30
//
//********************************************************************************/
using System;
namespace Itop.Domain {
    /// <summary>
    /// 数据库连接配置
    /// </summary>
    [Serializable]
    public class DataConfig {
        public DataConfig() { }

        string datasource = "";

        string database = "";

        string userid = "";

        string password = "";

        #region 属性
        public string Datasource {
            get { return datasource; }
            set { datasource = value; }
        }
        public string Database {
            get { return database; }
            set { database = value; }
        }
        public string Userid {
            get { return userid; }
            set { userid = value; }
        }
        public string Password {
            get { return password; }
            set { password = value; }
        }
        #endregion


    }
}

