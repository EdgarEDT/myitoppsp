using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Utils;
using System.IO;
using Itop.Domain.Graphics;
using System.Collections;

namespace Itop.TLPSP.DEVICE
{
    public class FileReadDLL
    {
        private string projectSUID;
        private string projectid;
        private int dulutype;
        private double ratecaplity;
        private WaitDialogForm wf;
        private string con;
        private string [,] dianLiuResult;
        public FileReadDLL(string projectSUID, string projectid, int dulutype, double ratecaplity, WaitDialogForm wf, string con, string [,] dianLiuResult)
        {
            this.projectSUID = projectSUID;
            this.projectid = projectid;
            this.dulutype = dulutype;
            this.ratecaplity = ratecaplity;
            this.wf = wf;
            this.con = con;
            this.dianLiuResult = dianLiuResult;
           
        }
        public void str()
        {
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt"))
            {
            }
            else
            {
                return;
            }
            FileStream dianLiu = new FileStream(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt", FileMode.Open);
            StreamReader readLineDL = new StreamReader(dianLiu, System.Text.Encoding.Default);
            //string content = string.Empty;
            //content = readLineD.ReadToEnd();
            //MemoryStream ms = new MemoryStream(Encoding.GetEncoding("GB2312").GetBytes(content));//放入内存流，以便逐行读
            string strLineDL;
            string[] arrayDL;
            char[] charSplitDL = new char[] { ' ' };
            //using (StreamReader readLineDL = new StreamReader(ms)) {
            strLineDL = readLineDL.ReadLine();
            int j = 0;
            int linenum = 0;
            while (strLineDL != null)
            {
                arrayDL = strLineDL.Split(charSplitDL, StringSplitOptions.RemoveEmptyEntries);
                int m = 0;
                string[] dev = arrayDL;
                //dev.Initialize();
                //foreach (string str in arrayDL)
                //{
                //    if (str != "")
                //    {
                //        dev[m++] = str;
                //    }
                //}
                if (j == 0)
                {
                    //dianLiuResult.Append( "\r\n" + "故障母线：" + pspDev.Name + "\r\n";
                    //dianLiuResult.Append(dev[0] + "," + dev[1] + "," + dev[2] + "," + dev[3] + "," + dev[4] + "," + dev[5] + "," + dev[6] + "," + dev[7] + "," + dev[8] + "," +
                    //             dev[9] + "," + dev[10] + "," + dev[11] + "," + dev[12] + "," + dev[13] + "," + dev[14] + "\r\n");
                    for (int i = 0; i < 14;i++ )
                    {
                        dianLiuResult[j,i] = dev[i].ToString();
                    }
                }
                else
                {
                    if (dev[0] == "故障母线")
                    {            
                        dianLiuResult[j, 0] = "故障母线";
                        dianLiuResult[j, 1] = dev[1].ToString();
                        //dianLiuResult.Append("\r\n" + "故障母线：" + dev[1] + "\r\n");
                    }
                    else
                    {
                        PSPDEV CR = new PSPDEV();

                        if (dev[0] != "du")
                        {

                            con = " WHERE Name='" + dev[0] + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                        }
                        else
                            con = " WHERE Name='" + dev[1] + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";

                        CR = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);

                     
                        dianLiuResult[j,0]=dev[0].ToString();
                        dianLiuResult[j,1]=dev[1].ToString();
                        dianLiuResult[j, 2] = dev[2].ToString();
                        dianLiuResult[j,3]=(Convert.ToDouble(dev[3]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt)).ToString();
                        dianLiuResult[j,4]=dev[4].ToString();
                        dianLiuResult[j,5]=(Convert.ToDouble(dev[5]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt)).ToString();
                        dianLiuResult[j,6] =dev[6].ToString();
                        dianLiuResult[j,7]=(Convert.ToDouble(dev[7]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt)).ToString();
                        dianLiuResult[j,8]=(dev[8].ToString());
                        dianLiuResult[j,9]=((Convert.ToDouble(dev[9]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt)).ToString());
                        dianLiuResult[j,10]=(dev[10].ToString());
                        dianLiuResult[j,11]=((Convert.ToDouble(dev[11]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt)).ToString());
                        dianLiuResult[j,12]=(dev[12].ToString());
                        dianLiuResult[j,13]=((Convert.ToDouble(dev[13]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt)).ToString());
                        dianLiuResult[j,14]=(dev[14].ToString());
                        //dianLiuResult.Append(dev[0] + "," + dev[1] + "," + dev[2] + "," + Convert.ToDouble(dev[3]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[4] + "," + Convert.ToDouble(dev[5]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[6] + "," + Convert.ToDouble(dev[7]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[8] + "," +
                        //  Convert.ToDouble(dev[9]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[10] + "," + Convert.ToDouble(dev[11]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[12] + "," + Convert.ToDouble(dev[13]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[14] + "\r\n");
                        
                    }
                    //因为在线路电流输出时既有一般线路的电流、两绕组和三绕组线路的电流还有接地电容器和电抗器的电流，因此只将电流输出就行了

                }

                strLineDL = readLineDL.ReadLine();
                j++;
                linenum++;
                if (linenum>65500)
                {
                    break;
                }
               // wf.SetCaption(linenum.ToString());
            }
            readLineDL.Close();
            
            //}
        }
    }
}
