
using System.Runtime.InteropServices;
using System.Text;

namespace Itop.Common {
    /// <summary>
    /// Ini文件读写操作。
    /// </summary>
    public class IniFile {

        private string path;    //INI文件名

        [DllImport("kernel32")]

        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]

        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        //声明读写INI文件的API函数

        /// <summary>
        /// 取得、设置INI文件名
        /// </summary>
        public string FileName { get { return path; } set { path = value; } }

        /// <summary>
        /// 类的构造函数，传递INI文件名
        /// </summary>
        /// <param name="INIPath"></param>
        public IniFile(string INIPath) {

            path = INIPath;

        }//类的构造函数，传递INI文件名


        /// <summary>
        /// 写INI文件
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void IniWriteValue(string Section, string Key, string Value) {

            WritePrivateProfileString(Section, Key, Value, this.path);

        }//写INI文件
        static public void IniWriteValue(string filename,string Section, string Key, string Value) {

            WritePrivateProfileString(Section, Key, Value, filename);

        }//写INI文件




        /// <summary>
        /// 读取INI文件指定
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key) {
            return IniReadValue(Section, Key, "");

        }//读取INI文件指定
        public string IniReadValue(string Section, string Key,string def) {

            StringBuilder temp = new StringBuilder(255);

            int i = GetPrivateProfileString(Section, Key, def, temp, 255, this.path);

            return temp.ToString();

        }//读取INI文件指定
        static public string IniReadValue(string filename,string Section, string Key, string def) {

            StringBuilder temp = new StringBuilder(255);

            int i = GetPrivateProfileString(Section, Key, def, temp, 255, filename);

            return temp.ToString();

        }//读取INI文件指定

    }
}
