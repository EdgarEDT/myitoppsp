using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Itop.Client.Layouts
{
    public class FrmCommon
    {
        public static void getDoc(byte[] bt, string filename)
        {
            BinaryWriter bw;
            FileStream fs;
            try
            {
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
                bw = new BinaryWriter(fs);
                bw.Write(bt);
                bw.Flush();
                bw.Close();
                fs.Close();

            }
            catch
            {

            }

        }
    }
}
