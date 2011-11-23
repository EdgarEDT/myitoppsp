
using System.Runtime.InteropServices;
using System.Text;

namespace Itop.Common {
    /// <summary>
    /// Ini�ļ���д������
    /// </summary>
    public class IniFile {

        private string path;    //INI�ļ���

        [DllImport("kernel32")]

        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]

        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        //������дINI�ļ���API����

        /// <summary>
        /// ȡ�á�����INI�ļ���
        /// </summary>
        public string FileName { get { return path; } set { path = value; } }

        /// <summary>
        /// ��Ĺ��캯��������INI�ļ���
        /// </summary>
        /// <param name="INIPath"></param>
        public IniFile(string INIPath) {

            path = INIPath;

        }//��Ĺ��캯��������INI�ļ���


        /// <summary>
        /// дINI�ļ�
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void IniWriteValue(string Section, string Key, string Value) {

            WritePrivateProfileString(Section, Key, Value, this.path);

        }//дINI�ļ�
        static public void IniWriteValue(string filename,string Section, string Key, string Value) {

            WritePrivateProfileString(Section, Key, Value, filename);

        }//дINI�ļ�




        /// <summary>
        /// ��ȡINI�ļ�ָ��
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key) {
            return IniReadValue(Section, Key, "");

        }//��ȡINI�ļ�ָ��
        public string IniReadValue(string Section, string Key,string def) {

            StringBuilder temp = new StringBuilder(255);

            int i = GetPrivateProfileString(Section, Key, def, temp, 255, this.path);

            return temp.ToString();

        }//��ȡINI�ļ�ָ��
        static public string IniReadValue(string filename,string Section, string Key, string def) {

            StringBuilder temp = new StringBuilder(255);

            int i = GetPrivateProfileString(Section, Key, def, temp, 255, filename);

            return temp.ToString();

        }//��ȡINI�ļ�ָ��

    }
}
