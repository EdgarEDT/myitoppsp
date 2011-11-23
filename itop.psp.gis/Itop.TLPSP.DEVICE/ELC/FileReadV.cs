using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Utils;
using System.IO;

namespace Itop.TLPSP.DEVICE
{
    public class FileReadV
    {
        private string projectSUID;
        private string projectid;
        private int dulutype;
        private double ratecaplity;
        private WaitDialogForm wf;
        private bool shortiflag;
        private StringBuilder duanResult;
        public FileReadV(string projectSUID, string projectid, int dulutype, double ratecaplity, WaitDialogForm wf, bool shortiflag, StringBuilder duanResult)
        {
            this.projectSUID = projectSUID;
            this.projectid = projectid;
            this.dulutype = dulutype;
            this.ratecaplity = ratecaplity;
            this.wf = wf;
            this.shortiflag = shortiflag;
            this.duanResult = duanResult;
        }
        public void str()
        {
            FileStream shorcuit = new FileStream(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt", FileMode.Open);
            StreamReader readLineGU = new StreamReader(shorcuit, System.Text.Encoding.Default);
            string strLineGU;
            string[] arrayGU;
            char[] charSplitGU = new char[] { ' ' };
            int intshorti = 0;
            while ((strLineGU = readLineGU.ReadLine()) != null)
            {

                arrayGU = strLineGU.Split(charSplitGU, StringSplitOptions.RemoveEmptyEntries);
                string[] shorti = arrayGU;
                shorti.Initialize();
                //int m = 0;
                //foreach (string str in arrayGU)
                //{

                //    if (str != "")
                //    {

                //        shorti[m++] = str.ToString();

                //    }
                //}
                if (intshorti == 0)
                {
                    if (!shortiflag)
                    {
                        duanResult.Append(shorti[0] + "," + shorti[1] + "," + shorti[3] + "\r\n");
                        shortiflag = true;
                    }

                }
                else
                    duanResult.Append(shorti[0] + "," + shorti[1] + "," + Convert.ToDouble(shorti[3]) + "\r\n");

                intshorti++;
                //wf.SetCaption(intshorti.ToString());
            }
            readLineGU.Close();
        }
    }
}
