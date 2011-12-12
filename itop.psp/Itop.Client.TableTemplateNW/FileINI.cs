using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Client.TableTemplateNW
{
    class FileINI
    {
       
        // ����INI�ļ���д�������� WritePrivateProfileString()

        [System.Runtime.InteropServices.DllImport("kernel32")]

        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        // ����INI�ļ��Ķ��������� GetPrivateProfileString()

        [System.Runtime.InteropServices.DllImport("kernel32")]

        private static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);

        private string sPath = null;

        public FileINI(string path)
        {

            this.sPath = path;

        }

        public void Writue(string section, string key, string value)
        {

            // section=���ýڣ�key=������value=��ֵ��path=·��

            WritePrivateProfileString(section, key, value, sPath);

        }

        public string ReadValue(string section, string key)
        {

            // ÿ�δ�ini�ж�ȡ�����ֽ�

            System.Text.StringBuilder temp = new System.Text.StringBuilder(255);

            // section=���ýڣ�key=������temp=���棬path=·��

            GetPrivateProfileString(section, key, "", temp, 255, sPath);

            return temp.ToString();

        }
    }
}