//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-8-21 15:02:30
//
//********************************************************************************/
using System;
namespace Itop.Domain {
    /// <summary>
    /// ���ݿ���������
    /// </summary>
    [Serializable]
    public class DataConfig {
        public DataConfig() { }

        string datasource = "";

        string database = "";

        string userid = "";

        string password = "";

        #region ����
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

