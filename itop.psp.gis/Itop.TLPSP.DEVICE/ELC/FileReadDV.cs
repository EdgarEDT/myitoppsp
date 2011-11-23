using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Utils;
using System.IO;
using Itop.Domain.Graphics;
using System.Web.UI;

namespace Itop.TLPSP.DEVICE
{
    public class FileReadDV
    {
        private string projectSUID;
        private string projectid;
        private int dulutype;
        private double ratecaplity;
        private WaitDialogForm wf;
        private string con;
        private StringBuilder dianYaResult;
        private delegate void MyInvoke(int num);
        private void SetNum(int num)
        {
            wf.SetCaption(num.ToString());
        }
        public FileReadDV(string projectSUID, string projectid, int dulutype, double ratecaplity, WaitDialogForm wf, string con, StringBuilder dianYaResult)
        {
            this.projectSUID = projectSUID;
            this.projectid = projectid;
            this.dulutype = dulutype;
            this.ratecaplity = ratecaplity;
            this.wf = wf;
            this.con = con;
            this.dianYaResult = dianYaResult;      

        }
        public void str()
        {
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt"))
            {
            }
            else
            {
                return;
            }
            FileStream dianYa = new FileStream(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt", FileMode.Open);
            StreamReader readLineDY = new StreamReader(dianYa, System.Text.Encoding.Default);
            //string content = string.Empty;
            //content = readLineD.ReadToEnd();
            //MemoryStream ms = new MemoryStream(Encoding.GetEncoding("GB2312").GetBytes(content));//放入内存流，以便逐行读
            string strLineDY;
            string[] arrayDY;
            char[] charSplitDY = new char[] { ' ' };
            //StreamReader readLineDY = new StreamReader(ms);
            strLineDY = readLineDY.ReadLine();
            int j = 0;
            int muxiannum = 0;
            while (strLineDY != null)
            {
                arrayDY = strLineDY.Split(charSplitDY, StringSplitOptions.RemoveEmptyEntries);

                //int m = 0;
                string[] dev = arrayDY;
                //dev.Initialize();
                //foreach (string str in arrayDY)
                //{
                //    if (str != "")
                //    {
                //        dev[m++] = str;
                //    }
                //}
                if (j == 0)
                {
                    //dianYaResult += "\r\n" + "故障母线：" + pspDev.Name + "\r\n";
                    dianYaResult.Append(dev[0] + "," + dev[1] + "," + dev[2] + "," + dev[3] + "," + dev[4] + "," + dev[5] + "," + dev[6] + "," + dev[7] + "," + dev[8] + "," +
 dev[9] + "," + dev[10] + "," + dev[11] + "," + dev[12] + "," + dev[13] + "\r\n");
                }
                else
                {
                    if (dev[0] == "故障母线")
                    {
                        dianYaResult.Append("\r\n" + "故障母线：" + dev[1] + "\r\n");
                    }
                    else
                    {
                        bool dianyaflag = true;     //判断此母线是短路点母线还是一般的母线
                        PSPDEV CR = new PSPDEV();

                        if (dev[1] != "du")
                        {

                            con = " WHERE Name='" + dev[1] + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            CR = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                            if (CR == null)
                            {
                                dianyaflag = false;
                            }
                        }
                        //else
                        //{
                        //    dianyaflag = false;
                        //    CR.Name = duanluname;
                        //    CR = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByNameANDSVG", CR);
                        //}
                        if (dianyaflag)
                            dianYaResult.Append(dev[0] + "," + dev[1] + "," + Convert.ToDouble(dev[2]) * CR.ReferenceVolt + "," + dev[3] + "," + Convert.ToDouble(dev[4]) * CR.ReferenceVolt + "," + dev[5] + "," + Convert.ToDouble(dev[6]) * CR.ReferenceVolt + "," + dev[7] + "," + Convert.ToDouble(dev[8]) * CR.ReferenceVolt + "," +
                                dev[9] + "," + Convert.ToDouble(dev[10]) * CR.ReferenceVolt + "," + dev[11] + "," + Convert.ToDouble(dev[12]) * CR.ReferenceVolt + "," + dev[13] + "\r\n");
                        //else
                        //    dianYaResult.Append( dev[0] + "," + duanluname + "上短路点" + "," + Convert.ToDouble(dev[2]) * CR.ReferenceVolt + "," + dev[3] + "," + Convert.ToDouble(dev[4]) * CR.ReferenceVolt + "," + dev[5] + "," + Convert.ToDouble(dev[6]) * CR.ReferenceVolt + "," + dev[7] + "," + Convert.ToDouble(dev[8]) * CR.ReferenceVolt + "," +
                        //       dev[9] + "," + Convert.ToDouble(dev[10]) * CR.ReferenceVolt + "," + dev[11] + Convert.ToDouble(dev[12]) * CR.ReferenceVolt + "," + dev[13] + "\r\n";
                    }


                }
                strLineDY = readLineDY.ReadLine();
                muxiannum++;
                j++;
                if (muxiannum>65500)
                {
                    break;
                }
            }
            readLineDY.Close();
        }
    }
}
