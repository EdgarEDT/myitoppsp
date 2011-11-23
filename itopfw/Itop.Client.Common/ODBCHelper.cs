using System;
using System.Collections.Generic;
using System.Text;
using Itop.Common;
using Itop.Server.Interface;
using System.Reflection;
using System.Windows.Forms;
using Itop.Domain;
using Microsoft.Win32;

namespace Itop.Client.Common
{
    public static class ODBCHelper {
        /// <summary>
        /// 建创修改ODBC
        /// </summary>
      
        public static void CreateODBC() {
            Itop.Server.Interface.IConfigService service = RemotingHelper.GetRemotingService<Itop.Server.Interface.IConfigService>();
            DataConfig data = service.GetDataConfig();
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\ODBC\ODBC.INI\ODBC Data Sources");
            key.SetValue("BZHZY", "SQL Server");
            key.Close();
            key = Registry.CurrentUser.CreateSubKey(@"Software\ODBC\ODBC.INI\BZHZY");
            key.SetValue("Driver", Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\SQLSRV32.dll");
            key.SetValue("Server", data.Datasource);
            key.SetValue("Database", data.Database);
            key.SetValue("LastUser", data.Userid);
            key.Close();
            SetSystemIni(data);
        }
        static void SetSystemIni(DataConfig data) {
            string filename = Application.StartupPath+"\\system.ini";
            string conn = string.Format("Provider=SQLOLEDB.1;User ID={0};Password={1};Persist Security Info=True;Initial Catalog={2};Data Source={3}", data.Userid, data.Password, data.Database, data.Datasource);
            try {
                Itop.Common.IniFile.IniWriteValue(filename, "ODBC连接", "DSN", "BZHZY");
                Itop.Common.IniFile.IniWriteValue(filename, "ODBC连接", "用户名", data.Userid);
                Itop.Common.IniFile.IniWriteValue(filename, "ODBC连接", "密码", data.Password);
                Itop.Common.IniFile.IniWriteValue(filename, "settings", "Conn", conn);               

            } catch { }
        }
    }
}
