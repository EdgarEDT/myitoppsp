using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Itop.Domain.Graphics;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Itop.Client.Common;
namespace Itop.TLPSP.DEVICE
{
    public class ElcRel
    {
         private string MXNodeType(string nodeType)
        {
            if (nodeType == "0")
            {
                return "3";
            }
            return nodeType;
        }
        //获得短路计算的text文本
        //
        //projectsuid为项目的ID号 projectid为工程的编号 即整个项目的卷号
        public int brchcount, buscount, transcount, outbrchcount, outbuscount;        //记录全网参与潮流计算的支路数和母线数目 其中三绕组变压器有三条支路


        public bool CheckN(string projectSUID, string projectid, double ratedCapacity)
        {
            brchcount = 0; buscount = 0; transcount = 0; outbrchcount = 0; outbuscount = 0;
            try
            {
                string strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'";
                string strCon2 = null;
                string strCon = null;
                string strBus = null;
                string strBranch = null;
                double Rad_to_Deg = 180 / Math.PI;
                {
                    strCon2 = " AND Type = '01'";
                    strCon = strCon1 + strCon2;
                    IList listMX = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = " AND Type = '05'";
                    strCon = strCon1 + strCon2;
                    IList listXL = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = " AND Type = '02'";
                    strCon = strCon1 + strCon2;
                    IList listBYQ2 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = " AND Type = '03'";
                    strCon = strCon1 + strCon2;
                    IList listBYQ3 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    foreach (PSPDEV dev in listXL)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            if (dev.FirstNode < 0 || dev.LastNode < 0)
                            {
                                string temp = "拓朴分析失败,";
                                temp += dev.Name;
                                temp += "没有正确连接,请进行处理！。";
                                System.Windows.Forms.MessageBox.Show(temp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return false;
                            }
                            if (strBranch != null)
                            {
                                strBranch += "\r\n";
                            }
                            if (dev.UnitFlag == "0")
                            {
                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + "0" + " " + "0";

                            }
                            else
                            {
                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + "0";

                            }
                            brchcount++;
                            if (outbrchcount < 125)
                            {
                                outbrchcount++;
                            }
                        }
                    }
                    foreach (PSPDEV dev in listBYQ2)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            if (dev.FirstNode < 0 || dev.LastNode < 0)
                            {
                                string temp = "拓朴分析失败,";
                                temp += dev.Name;
                                temp += "没有正确连接,请进行处理！。";
                                System.Windows.Forms.MessageBox.Show(temp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return false;
                            }
                            if (strBranch != null)
                            {
                                strBranch += "\r\n";
                            }
                            if (dev.UnitFlag == "0")
                            {
                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + dev.K.ToString() + " " + dev.G.ToString();

                            }
                            else
                            {
                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + dev.K.ToString() + " " + dev.G.ToString();

                            }
                            transcount++;
                        }
                    }

                    foreach (PSPDEV dev in listBYQ3)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            if (dev.FirstNode < 0 || dev.LastNode < 0 || dev.Flag < 0)
                            {
                                string temp = "拓朴分析失败,";
                                temp += dev.Name;
                                temp += "没有正确连接,请进行处理！。";
                                System.Windows.Forms.MessageBox.Show(temp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return false;
                            }

                            if (dev.UnitFlag == "0")
                            {
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.HuganTQ1.ToString() + " " + dev.HuganTQ4.ToString() + " " + "0" + " " + dev.K.ToString() + " " + "0";
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                strBranch += dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.HuganTQ2.ToString() + " " + dev.HuganTQ5.ToString() + " " + "0" + " " + dev.StandardCurrent.ToString() + " " + "0";
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                strBranch += dev.FirstNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.HuganTQ3.ToString() + " " + dev.ZeroTQ.ToString() + " " + "0" + " " + dev.BigP.ToString() + " " + "0";

                            }
                            else
                            {
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.HuganTQ1 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.HuganTQ4 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + "0" + " " + dev.K.ToString() + " " + "0";
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                strBranch += dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.HuganTQ2 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.HuganTQ5 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + "0" + " " + dev.StandardCurrent.ToString() + " " + "0";
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                strBranch += dev.FirstNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.HuganTQ3 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.ZeroTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + "0" + " " + dev.BigP.ToString() + " " + "0";
                            }
                            transcount += 3;
                        }
                    }
                    //拓扑分析是否存在孤立母线母线节点
                    List<string> busname = new List<string>();
                    foreach (PSPDEV dev in listMX)
                    {
                        bool flag = false;
                        foreach (PSPDEV devline in listXL)
                        {
                            if (dev.Number == devline.LastNode || dev.Number == devline.FirstNode)
                            {
                                flag = true;
                                break;
                            }
                        }
                        foreach (PSPDEV devtrans in listBYQ2)
                        {
                            if (dev.Number == devtrans.LastNode || dev.Number == devtrans.FirstNode)
                            {
                                flag = true;
                                break;
                            }
                        }
                        foreach (PSPDEV devtrans in listBYQ3)
                        {
                            if (dev.Number == devtrans.LastNode || dev.Number == devtrans.FirstNode || dev.Number == devtrans.Flag)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            busname.Add(dev.Name);
                        }
                    }
                    if (busname.Count > 0)
                    {
                        string temp = "拓扑分析失败";
                        for (int i = 0; i < busname.Count; i++)
                        {
                            temp += "，" + busname[i];

                        }
                        temp += "为孤立的节点！";
                        System.Windows.Forms.MessageBox.Show(temp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    foreach (PSPDEV dev in listMX)
                    {


                        if (dev.KSwitchStatus == "0")
                        {
                            if (strBus != null)
                            {
                                strBus += "\r\n";
                            }
                            double outP = 0;
                            double outQ = 0;
                            double inputP = 0;
                            double inputQ = 0;
                            string strCon3 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "' AND Type = '04' AND IName = '" + dev.Name + "'";
                            IList listFDJ = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon3);
                            string strCon4 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "' AND Type = '12' AND IName = '" + dev.Name + "'";
                            IList listFH = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon4);
                            foreach (PSPDEV devFDJ in listFDJ)
                            {
                                if (devFDJ.UnitFlag == "0")
                                {
                                    outP += devFDJ.OutP;
                                    outQ += devFDJ.OutQ;
                                }
                                else
                                {
                                    outP += devFDJ.OutP / ratedCapacity;
                                    outQ += devFDJ.OutQ / ratedCapacity;
                                }
                            }
                            foreach (PSPDEV devFH in listFH)
                            {
                                if (devFH.UnitFlag == "0")
                                {
                                    inputP += devFH.InPutP;
                                    inputQ += devFH.InPutQ;
                                }
                                else
                                {
                                    inputP += devFH.InPutP / ratedCapacity;
                                    inputQ += devFH.InPutQ / ratedCapacity;
                                }
                            }
                            if (dev.UnitFlag == "0")
                            {
                                outP += dev.OutP;
                                outQ += dev.OutQ;
                                inputP += dev.InPutP;
                                inputQ += dev.InPutQ;
                                strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + dev.VoltV.ToString() + " " + ((inputP - outP)).ToString() + " " + ((inputQ - outQ)).ToString());
                                //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.LineGNDC).ToString() + " " + "0" + " " + dev.Name.ToString())
                            }
                            else
                            {
                                outP += dev.OutP / ratedCapacity;
                                outQ += dev.OutQ / ratedCapacity;
                                inputP += dev.InPutP / ratedCapacity;
                                inputQ += dev.InPutQ / ratedCapacity;
                                strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + dev.VoltV.ToString() + " " + ((inputP - outP)).ToString() + " " + ((inputQ - outQ)).ToString());
                                //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + ((dev.LineGNDC) * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + dev.Name.ToString());
                            }
                            buscount++;
                            if (outbuscount < 125)
                            {
                                outbuscount++;
                            }
                        }
                    }
                }

                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\branch.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\branch.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\bus.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\bus.txt");
                }

                FileStream VK1 = new FileStream((System.Windows.Forms.Application.StartupPath + "\\branch.txt"), FileMode.OpenOrCreate);
                StreamWriter str3 = new StreamWriter(VK1, Encoding.Default);
                str3.Write(strBranch);
                str3.Close();
                FileStream L = new FileStream((System.Windows.Forms.Application.StartupPath + "\\bus.txt"), FileMode.OpenOrCreate);
                StreamWriter str2 = new StreamWriter(L, Encoding.Default);
                str2.Write(strBus);
                str2.Close();
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\VandTheta.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\VandTheta.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\lineP.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\lineP.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\transP.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\transP.txt");
                }
                if ((strBus.Contains("非数字") || strBus.Contains("正无穷大")) || (strBranch.Contains("非数字") || strBranch.Contains("正无穷大")))
                {
                    MessageBox.Show("缺少参数，请检查输入参数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("数据存在问题请输入完全后再操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
        public double TLPSPVmin = 0.95, TLPSPVmax = 1.05;
        public bool WebCalAndPrint(string projectSUID, string projectid, double ratedCapacity)            //网络N-1计算和输出

        {
            bool flag = true;
            FileStream dh;
            StreamReader readLine;
            // StreamReader readLine;
            ArrayList list = new ArrayList();   //用来记录线路不能解裂的位数

            char[] charSplit;
            string strLine;
            string[] array1;
            string outputZL ="";
            string outputv ="";//记录直流计算结果 线路功率和节点电压

            //string outputBC = null;   //记录补偿计算结果 节点电压
            string[] array2;

            string strLine2;

            char[] charSplit2 = new char[] { ' ' };
            List<lineclass> Overlinp = new List<lineclass>();
            List<lineclass> OverVp = new List<lineclass>();
            //Dictionary<int, List<lineclass>> OverPhege = new Dictionary<int, List<lineclass>>();       //为 线路功率的检验 键值为断开线路的编号，值为第几条线路出现了不合格

            //Dictionary<int, List<lineclass>> OverVhege = new Dictionary<int, List<lineclass>>();       //为 节点电压的检验 键值为断开线路的编号，值为第几个节点出现了不合格

            FileStream op;
            StreamWriter str1;
            FileStream dh2;
            StreamReader readLine2;
            Excel.Application ex;
            //Excel.Worksheet xSheet;
            Excel.Application result1;
            //Excel.Worksheet tempSheet;
            Excel.Worksheet newWorksheet;
            PSP_ELCPROJECT psproject = new PSP_ELCPROJECT();
            psproject.ID = projectSUID;
            psproject = (PSP_ELCPROJECT)UCDeviceBase.DataService.GetObject("SelectPSP_ELCPROJECTByKey", psproject);

            if (true)    //进行全网计算
            {
                if (!CheckN(projectSUID,projectid,ratedCapacity))
                {
                    return false;
                }

                try
                {
                    string datatime = System.DateTime.Now.ToString();
                    System.Windows.Forms.Clipboard.Clear(); //去掉剪切板中的数据

                    if (brchcount >= 50)
                    {
                        for (int i = 1; i <= 5; i++)
                        {
                            n1NL_DLL.ZYZ nl = new n1NL_DLL.ZYZ();
                            nl.jianyan(i);
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= brchcount; i++)
                        {
                            n1NL_DLL.ZYZ nl = new n1NL_DLL.ZYZ();
                            nl.jianyan(i);
                        }
                    }
                    //int* busnumber;

                    //N1Test.NBcal kk = new N1Test.NBcal();                    //busnumber = kk.Show_Reliability();


                    double yinzi = 0, capability = 0, volt = 0, current = 0, standvolt = 1, Rad_to_Deg = 57.29577951;
                   // string branchname = getbranchname();
                   // string busname = getbusname();
                   
                    capability = ratedCapacity;
                    
                    string con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='01'AND PSPDEV.KSwitchStatus = '0'";
                    IList cont = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    if (buscount < cont.Count)
                    {
                        MessageBox.Show("选择的母线又存在孤立的节点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return false;

                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\VandTheta.txt"))
                    {
                    }
                    else
                    {
                        MessageBox.Show("数据不收敛，请调整参数后重新计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\lineP.txt"))
                    {
                    }
                    else
                    {
                        return false;
                    }
                    dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\" + "lineP.txt", FileMode.Open);
                    dh2 = new FileStream(System.Windows.Forms.Application.StartupPath + "\\" + "VandTheta.txt", FileMode.Open);
                    readLine2 = new StreamReader(dh2, Encoding.Default);
                    readLine = new StreamReader(dh, Encoding.Default);
                    charSplit = new char[] { ' ' };
                    //strLine = readLine.ReadLine();

                    outputZL = null;
                    //outputBC=null;                    
                   // outputZL += ("全网可靠性结果报表" + "\r\n");
                   //// outputZL += ("开断支路名称" + "," + "剩余网络线路功率Pij和Pji的有名值" + ",,");
                   // for (int i = 0; i < brchcount - 1; i++)
                   // {
                   //     outputZL += (",,");
                   // }
                   // outputZL += ("是否越限" + "," + "\r\n");
                   // outputZL += ",";
                    int n = 0; //记录线路的行数


                    while ((strLine = readLine.ReadLine()) != null)
                    {
                        array1 = strLine.Split(charSplit);

                        string[] devzl = new string[3 * brchcount + 1];

                        devzl.Initialize();
                        int i = 0;

                        n++;
                        foreach (string str in array1)
                        {
                            if (str != "")
                            {

                                devzl[i++] = str.ToString();

                            }
                        }
                        //for (int j = 0; j < brchcount; j++)
                        //{
                        //    outputZL += devzl[3 * j + 1] + "," + ",";
                        //}
                        //outputZL += branchname;
                        //outputZL += ("," + "\r\n");
                        //outputZL += devzl[0] + ",";
                        bool lineflag = true;      //只要有一个不合格则就为不合格
                        if (devzl[1] != "-1")
                        {
                            for (int j = 0; j < brchcount; j++)
                            {

                                double pij = Convert.ToDouble(devzl[j * 3 + 2].Substring(0, devzl[j * 3 + 2].IndexOf('j') - 1)) * capability;
                                double qij = Convert.ToDouble(devzl[j * 3 + 2].Substring(devzl[j * 3 + 2].IndexOf('j') + 1)) * capability;
                                double pji = Convert.ToDouble(devzl[j * 3 + 3].Substring(0, devzl[j * 3 + 3].IndexOf('j') - 1)) * capability;
                                double qji = Convert.ToDouble(devzl[j * 3 + 3].Substring(devzl[j * 3 + 3].IndexOf('j') + 1)) * capability;
                                double Sij = System.Math.Sqrt(pij * pij + qij * qij);
                                double Sji = System.Math.Sqrt(pji * pji + qji * qji);
                                double maxSij = (Sij > Sji) ? Sij : Sji;
                                PSPDEV psp = new PSPDEV();
                                con = " WHERE Name='" + devzl[j * 3 + 1] + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
                                IList listName = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                                PSPDEV pspline = (PSPDEV)listName[0];
                                double voltR = pspline.RateVolt;
                                //WireCategory wirewire = new WireCategory();
                                //wirewire.WireType = pspline.LineType;
                                //if (pspline.LineType == null || pspline.LineType == "")
                                //{
                                //    MessageBox.Show(pspline.Name + "的线路类型没有输入，无法进行可靠性检验", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    return;
                                //}
                                //WireCategory listware = (WireCategory)UCDeviceBase.DataService.GetObject("SelectWireCategoryByKey", wirewire);
                                double Ichange = (double)pspline.Burthen;
                                double linXij = System.Math.Sqrt(3) * voltR * Ichange ;
                               // outputZL += "'" + youming(devzl[j * 3 + 2], capability) + "," + "'" + youming(devzl[j * 3 + 3], capability) + ",";
                                if (maxSij >= linXij)
                                {
                                    lineflag = false;
                                    flag = false;
                                    lineclass _line = new lineclass(n, j);
                                    Overlinp.Add(_line);
                                    // OverPhege[n] = j;
                                }

                            }
                            if (!lineflag)
                            {
                                outputZL += "不合格";
                            }
                            else
                                outputZL += "合格";
                        }
                        else
                        {
                            outputZL += "该线路不可断。";
                        }
                        //OverPhege[n] = Overlinp;
                        //Overlinp.Clear();
                        //outputZL += "\r\n";
                        while ((strLine = readLine.ReadLine()) != null)
                        {
                            array1 = strLine.Split(charSplit);

                            string[] devzl1 = new string[3 * brchcount + 1];

                            devzl1.Initialize();

                            n++;
                            i = 0;
                            foreach (string str in array1)
                            {
                                if (str != "")
                                {

                                    devzl1[i++] = str.ToString();

                                }
                            }
                            if (devzl1[1] != "-1")
                            {
                                outputZL += devzl1[0] + ",";
                                for (int j = 0; j < brchcount; j++)
                                {
                                    double pij = Convert.ToDouble(devzl1[j * 3 + 2].Substring(0, devzl1[j * 3 + 2].IndexOf('j') - 1)) * capability;
                                    double qij = Convert.ToDouble(devzl1[j * 3 + 2].Substring(devzl1[j * 3 + 2].IndexOf('j') + 1)) * capability;
                                    double pji = Convert.ToDouble(devzl1[j * 3 + 3].Substring(0, devzl1[j * 3 + 3].IndexOf('j') - 1)) * capability;
                                    double qji = Convert.ToDouble(devzl1[j * 3 + 3].Substring(devzl1[j * 3 + 3].IndexOf('j') + 1)) * capability;
                                    double Sij = System.Math.Sqrt(pij * pij + qij * qij);
                                    double Sji = System.Math.Sqrt(pji * pji + qji * qji);
                                    double maxSij = (Sij > Sji) ? Sij : Sji;
                                    
                                    con = " WHERE Name='" + devzl1[j * 3 + 1] + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
                                    IList listName = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                                   
                                    PSPDEV pspline = (PSPDEV)listName[0];
                                    double voltR = pspline.RateVolt;
                                    //WireCategory wirewire = new WireCategory();
                                    //wirewire.WireType = pspline.LineType;
                                    //if (pspline.LineType == null || pspline.LineType == "")
                                    //{
                                    //    MessageBox.Show(pspline.Name + "的线路类型没有输入，无法进行可靠性检验", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //    return;
                                    //}
                                    //WireCategory listware = (WireCategory)UCDeviceBase.DataService.GetObject("SelectWireCategoryByKey", wirewire);
                                    double Ichange = (double)pspline.Burthen;
                                    double linXij = System.Math.Sqrt(3) * voltR * Ichange ;
                                    // outputZL += "'" + devzl[j * 3 + 2] + "," + "'" + devzl[j * 3 + 3] + ",";
                                    if (maxSij >= linXij)
                                    {
                                        lineflag = false;
                                        flag = false;
                                        return flag;
                                        lineclass subline = new lineclass(n, j);
                                        Overlinp.Add(subline);
                                        //OverPhege[n] = j;
                                    }
                                    outputZL += "'" + youming(devzl1[j * 3 + 2], capability) + "," + "'" + youming(devzl1[j * 3 + 3], capability) + ",";    //在此还可以判断线路是否超载


                                }
                                if (!lineflag)
                                {
                                    outputZL += "不合格";
                                }
                                else
                                    outputZL += "合格";
                                outputZL += "\r\n";
                                //OverPhege[n] = Overlinp;
                                //Overlinp.Clear();

                            }

                            else
                            {
                                list.Add(n);
                                outputZL += devzl1[0] + "," + "为不可断裂的线路";
                                outputZL += "\r\n";
                            }

                        }
                    }
                    outputZL += "注释：红色为线路超载" + "\r\n";
                    outputZL += "操作时间为：" + datatime;
                    outputZL += "\r\n";
                    outputZL += "单位：kA\\kV\\MW\\Mvar" + "\r\n";
                    readLine.Close();
                    if (File.Exists("result1.csv"))
                    {
                        File.Delete("result1.csv");
                    }

                    op = new FileStream("result1.csv", FileMode.OpenOrCreate);
                    str1 = new StreamWriter(op, Encoding.Default);
                    str1.Write(outputZL);
                    str1.Close();

                    outputZL = null;
                    //将各个节点的电压写入其中
                    // strLine2 = readLine2.ReadLine();
                    n = 0;
                    bool busvflag1 = true;
                    outputZL += ("网络节点电压和相角" + "\r\n");
                    outputZL += ("开断支路名称" + "," + "节点电压的幅值和相角的有名值");
                    for (int i = 0; i < buscount; i++)
                    {
                        outputZL += (",,");
                    }
                    outputZL += ("是否越限" + "," + "\r\n");
                    outputZL += ",";
                    while ((strLine2 = readLine2.ReadLine()) != null)
                    {
                        array2 = strLine2.Split(charSplit);

                        string[] devzl = new string[buscount * 3 + 1];

                        devzl.Initialize();
                        int i = 0;

                        n++;
                        foreach (string str in array2)
                        {
                            if (str != "")
                            {

                                devzl[i++] = str.ToString();

                            }
                        }
                      
                       // outputZL += busname;
                        outputZL += ("," + "\r\n");
                        outputZL += devzl[0] + ",";
                        if (devzl[1] != "-1")
                        {
                           
                            for (int j = 0; j < buscount; j++)
                            {
                                PSPDEV pspDev = new PSPDEV();
                                con = " WHERE Name='" + devzl[j * 3 + 1] + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                                pspDev =(PSPDEV) UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                               
                                if (pspDev != null && pspDev.ReferenceVolt != 0)
                                {
                                    volt = pspDev.ReferenceVolt;
                                }
                                else
                                    volt = standvolt;
                                outputZL += "'" + (Convert.ToDouble(devzl[j * 3 + 2]) * volt).ToString() + "," + "'" + devzl[j * 3 + 3] + ",";
                                if (Convert.ToDouble(devzl[j * 3 + 2]) <= TLPSPVmin || Convert.ToDouble(devzl[j * 3 + 2]) >= TLPSPVmax)
                                {
                                    busvflag1 = false;
                                    flag = false;
                                    return flag;
                                    lineclass _vtheta = new lineclass(n, j);
                                    OverVp.Add(_vtheta);
                                    //OverVhege[n] = j;
                                }
                            }
                            if (busvflag1)
                            {
                                outputZL += "合格";
                            }
                            else
                            {
                                outputZL += "不合格";
                            }
                        }
                        else
                        {
                            outputZL += "不可断裂";
                        }
                        //OverVhege[n] = OverVp;
                        //OverVp.Clear();
                        outputZL += "\r\n";

                        while ((strLine2 = readLine2.ReadLine()) != null)
                        {
                            busvflag1 = true;
                            array2 = strLine2.Split(charSplit);

                            string[] devzl1 = new string[buscount * 3 + 1];

                            devzl1.Initialize();

                            n++;
                            i = 0;
                            foreach (string str in array2)
                            {
                                if (str != "")
                                {

                                    devzl1[i++] = str.ToString();

                                }
                            }
                            
                            if (devzl1[1] != "-1")
                            {
                                outputZL += devzl1[0] + ",";
                                for (int j = 0; j < buscount; j++)
                                {
                                    PSPDEV pspDev = new PSPDEV();
                                    con = " WHERE Name='" + devzl1[j * 3 + 1] + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                                    pspDev =(PSPDEV) UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                                   
                                    if (pspDev != null && pspDev.ReferenceVolt != 0)
                                    {
                                        volt = pspDev.ReferenceVolt;
                                    }
                                    else
                                        volt = standvolt;
                                    outputZL += "'" + (Convert.ToDouble(devzl1[j * 3 + 2]) * volt).ToString() + "," + "'" + devzl1[j * 3 + 3] + ",";   //在此还可以判断线路是否超载,如果超载加入一个标记,在excel里使其变为红色


                                    if (Convert.ToDouble(devzl1[j * 3 + 2]) >= TLPSPVmax || Convert.ToDouble(devzl1[j * 3 + 2]) <= TLPSPVmin)
                                    {
                                        lineclass vtheta = new lineclass(n, j);
                                        busvflag1 = false;
                                        flag = false;
                                        return flag;
                                        OverVp.Add(vtheta);
                                        //OverVhege[n] = j;
                                    }
                                }
                                if (busvflag1)
                                {
                                    outputZL += "合格";
                                }
                                else
                                    outputZL += "不合格";
                                outputZL += "\r\n";
                                //OverPhege[n] = OverVp;
                                //OverVp.Clear();
                            }
                            else
                            {
                                // list.Add(n);
                                outputZL += devzl1[0] + "," + "为不可断裂的线路" + ",,,,,,";
                                outputZL += "\r\n";
                            }

                        }
                    }
                    outputZL += "注释：红色为节点电压超载" + "\r\n";
                    outputZL += "节点电压合格范围为电压基准值的上下限，分别为" + TLPSPVmax + "和" + TLPSPVmin + "倍" + "\r\n";
                    outputZL += "操作时间为：" + datatime + "\r\n";
                    outputZL += "单位：kA\\kV\\MW\\Mvar" + "\r\n";
                    readLine2.Close();


                    //op = new FileStream(System.Windows.Forms.Application.StartupPath + "\\" + "reli.txt", FileMode.OpenOrCreate);
                    //str1 = new StreamWriter(op, Encoding.Default);
                    //str1.Write(outputZL);
                    //str1.Close();


                }
                catch (System.Exception e1)
                {
                    MessageBox.Show("数据存在问题，请仔细检查后再进行结果计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //if (webn1.DialogResult == DialogResult.Ignore)
            //{
            //    PSPDEV pspDEV = new PSPDEV();
            //    pspDEV.ProjectID = projectid;
            //    pspDEV.SvgUID = projectSUID; //为了把 项目ID和卷id传入
            //    PartRelform selregion = new PartRelform(pspDEV);
            //    selregion.ShowDialog();
            //    if (selregion.DialogResult == DialogResult.Ignore)
            //    {
            //        DelLinenum = selregion.lineVnumlist;
            //        //DelTransnum = null;
            //       flag =  QyRelanalyst(projectSUID,projectid,ratedCapacity);
            //    }
            //    //IList list1 = UCDeviceBase.DataService.GetList("SelectPSPDEVBySvgUID", psp);
            //    //将进行潮流计算的文档传到选择网络中。

            //    else if (selregion.DialogResult == DialogResult.Yes)
            //    {
            //        DelLinenum = selregion.lineDnumlist;
            //        DelTransnum = selregion.lineVnumlist;
            //        //DelTransnum = null;
            //       flag =  QyRelanalyst(projectSUID,projectid,ratedCapacity);
            //    }
            //}
            //Operateflag = false;
            return flag;
        }
        private List<int> DelLinenum = new List<int>();      //记录要进行断开的线路编号

        private List<int> DelTransnum = new List<int>();      //记录断开的变压器线路编号
        public bool QyRelanalyst(string projectSUID, string projectid, double ratedCapacity)
        {
            bool flag = true;
            FileStream dh;
            StreamReader readLine;
            // StreamReader readLine;
            ArrayList list = new ArrayList();   //用来记录线路不能解裂的位数

            List<lineclass> Overlinp = new List<lineclass>();
            List<lineclass> OverVp = new List<lineclass>();
            //Dictionary<int, int> OverPhege = new Dictionary<int, int>();       //为 线路功率的检验 键值为断开线路的编号，值为第几条线路出现了不合格

            //Dictionary<int, int> OverVhege = new Dictionary<int, int>();       //为 节点电压的检验 键值为断开线路的编号，值为第几个节点出现了不合格

            char[] charSplit;
            string strLine;
            string[] array1;
            string outputZL = null;   //记录直流计算结果 线路功率和节点电压

            //string outputBC = null;   //记录补偿计算结果 节点电压
            string[] array2;

            string strLine2;
            PSP_ELCPROJECT psproject = new PSP_ELCPROJECT();
            psproject.ID = projectSUID;
            psproject = (PSP_ELCPROJECT)UCDeviceBase.DataService.GetObject("SelectPSP_ELCPROJECTByKey", psproject);
            char[] charSplit2 = new char[] { ' ' };
            FileStream op;
            StreamWriter str1;
            FileStream dh2;
            StreamReader readLine2;
            Excel.Application ex;
            //Excel.Worksheet xSheet;
            Excel.Application result1;
            //Excel.Worksheet tempSheet;
            Excel.Worksheet newWorksheet;
            if (!CheckN(projectSUID,projectid,ratedCapacity))
            {
                return false;
            }

            try
            {
                string datatime = System.DateTime.Now.ToString();
                System.Windows.Forms.Clipboard.Clear(); //去掉剪切板中的数据

                int linenum = DelLinenum.Count + DelTransnum.Count;
                for (int i = 1; i <= DelLinenum.Count; i++) //此处进行所选的一般线路N-1
                {
                    n1NL_DLL.ZYZ nl = new n1NL_DLL.ZYZ();
                    nl.jianyan(DelLinenum[i - 1]);
                }
                for (int i = 1; i <= DelTransnum.Count; i++) //此处进行所选的变压器线路N-1
                {
                    n1NL_DLL.ZYZ nl = new n1NL_DLL.ZYZ();
                    nl.jianyan(DelTransnum[i - 1]);
                }

                //int* busnumber;

                //N1Test.NBcal kk = new N1Test.NBcal();
                //busnumber = kk.Show_Reliability();             

                double yinzi = 0, capability = 0, volt = 0, current = 0, standvolt = 1, Rad_to_Deg = 57.29577951;
                //PSPDEV benchmark = new PSPDEV();
                //benchmark.Type = "power";
                //benchmark.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                //IList list3 = UCDeviceBase.DataService.GetList("SelectPSPDEVBySvgUIDAndType", benchmark);
                //if (list3 == null)
                //{
                //    MessageBox.Show("请设置基准后再进行计算!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                //foreach (PSPDEV dev in list3)
                //{
                //    yinzi = Convert.ToDouble(dev.PowerFactor);
                //    capability = Convert.ToDouble(dev.StandardCurrent);
                //    volt = Convert.ToDouble(dev.StandardVolt);
                //    standvolt = volt;
                //    TLPSPVmin = dev.iV;
                //    TLPSPVmax = dev.jV;
                //    if (dev.PowerFactor == 0)
                //    {
                //        yinzi = 1;
                //    }
                //    if (dev.StandardCurrent == 0)
                //    {
                //        capability = 1;
                //    }
                //    if (dev.StandardVolt == 0)
                //    {
                //        volt = 1;
                //        standvolt = 1;
                //    }
                //    current = capability / (Math.Sqrt(3) * volt);


                //}
                string branchname = getbranchname();
                string busname = getbusname();
                capability = ratedCapacity;
                string con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='01'AND PSPDEV.KSwitchStatus = '0'";
                IList cont = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                if (buscount < cont.Count)
                {
                    MessageBox.Show("选择的母线存在孤立节点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return false;

                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\VandTheta.txt"))
                {
                }
                else
                {
                    MessageBox.Show("数据不收敛，请调整参数后重新计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\lineP.txt"))
                {
                }
                else
                {
                    return false;
                }
                dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\" + "lineP.txt", FileMode.Open);
                dh2 = new FileStream(System.Windows.Forms.Application.StartupPath + "\\" + "VandTheta.txt", FileMode.Open);
                readLine2 = new StreamReader(dh2, Encoding.Default);
                readLine = new StreamReader(dh, Encoding.Default);
                charSplit = new char[] { ' ' };
                //strLine = readLine.ReadLine();

                outputZL = null;
                //outputBC=null;                    
                outputZL += ("部分网可靠性结果报表" + "\r\n");
                outputZL += ("开断支路" + "," + "剩余网络线路功率Pij和Pji的有名值" + ",,");
                for (int i = 0; i < brchcount - 1; i++)
                {
                    outputZL += (",,");
                }
                outputZL += ("是否越限" + "," + "\r\n");
                outputZL += ",";
                int n = 0; //记录线路的行数


                while ((strLine = readLine.ReadLine()) != null)
                {
                    array1 = strLine.Split(charSplit);

                    string[] devzl = new string[3 * brchcount + 1];

                    devzl.Initialize();
                    int i = 0;

                    n++;
                    foreach (string str in array1)
                    {
                        if (str != "")
                        {

                            devzl[i++] = str.ToString();

                        }
                    }
                    //for (int j = 0; j < brchcount; j++)
                    //{
                    //    outputZL += devzl[3 * j + 1] + "," + ",";
                    //}
                    outputZL += branchname;
                    outputZL += ("," + "\r\n");
                    outputZL += devzl[0] + ",";
                    bool lineflag = true;      //只要有一个不合格则就为不合格
                    if (devzl[1] != "-1")
                    {
                        for (int j = 0; j < brchcount; j++)
                        {

                            double pij = Convert.ToDouble(devzl[j * 3 + 2].Substring(0, devzl[j * 3 + 2].IndexOf('j') - 1)) * capability;
                            double qij = Convert.ToDouble(devzl[j * 3 + 2].Substring(devzl[j * 3 + 2].IndexOf('j') + 1)) * capability;
                            double pji = Convert.ToDouble(devzl[j * 3 + 3].Substring(0, devzl[j * 3 + 3].IndexOf('j') - 1)) * capability;
                            double qji = Convert.ToDouble(devzl[j * 3 + 3].Substring(devzl[j * 3 + 3].IndexOf('j') + 1)) * capability;
                            double Sij = System.Math.Sqrt(pij * pij + qij * qij);
                            double Sji = System.Math.Sqrt(pji * pji + qji * qji);
                            double maxSij = (Sij > Sji) ? Sij : Sji;
                           

                            con = " WHERE Name='" + devzl[j * 3 + 1] + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
                            IList listName = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspline = (PSPDEV)listName[0];
                            double voltR = pspline.RateVolt;
                            //WireCategory wirewire = new WireCategory();
                            //wirewire.WireType = pspline.LineType;
                            //if (pspline.LineType == null || pspline.LineType == "")
                            //{
                            //    MessageBox.Show(pspline.Name + "的线路类型没有输入，无法进行可靠性检验", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    return;
                            //}
                            //WireCategory listware = (WireCategory)UCDeviceBase.DataService.GetObject("SelectWireCategoryByKey", wirewire);
                            double Ichange = (double)pspline.Burthen;
                            double linXij = System.Math.Sqrt(3) * voltR * Ichange ;
                            outputZL += "'" + youming(devzl[j * 3 + 2], capability) + "," + "'" + youming(devzl[j * 3 + 3], capability) + ",";
                            if (maxSij >= linXij)
                            {
                                lineflag = false;
                                flag = false;
                                lineclass _line = new lineclass(n, j);
                                Overlinp.Add(_line);
                                // OverPhege[n] = j;
                            }

                        }
                        if (!lineflag)
                        {
                            outputZL += "不合格";
                        }
                        else
                        {
                            outputZL += "合格";
                        }
                    }
                    else
                    {
                        outputZL += "该线路不可断裂。";
                    }

                    outputZL += "\r\n";
                    while ((strLine = readLine.ReadLine()) != null)
                    {
                        array1 = strLine.Split(charSplit);

                        string[] devzl1 = new string[3 * brchcount + 1];

                        devzl1.Initialize();

                        n++;
                        i = 0;
                        foreach (string str in array1)
                        {
                            if (str != "")
                            {

                                devzl1[i++] = str.ToString();

                            }
                        }
                        
                        if (devzl1[1] != "-1")
                        {
                            outputZL += devzl1[0] + ",";
                            for (int j = 0; j < brchcount; j++)
                            {
                                double pij = Convert.ToDouble(devzl1[j * 3 + 2].Substring(0, devzl1[j * 3 + 2].IndexOf('j') - 1)) * capability;
                                double qij = Convert.ToDouble(devzl1[j * 3 + 2].Substring(devzl1[j * 3 + 2].IndexOf('j') + 1)) * capability;
                                double pji = Convert.ToDouble(devzl1[j * 3 + 3].Substring(0, devzl1[j * 3 + 3].IndexOf('j') - 1)) * capability;
                                double qji = Convert.ToDouble(devzl1[j * 3 + 3].Substring(devzl1[j * 3 + 3].IndexOf('j') + 1)) * capability;
                                double Sij = System.Math.Sqrt(pij * pij + qij * qij);
                                double Sji = System.Math.Sqrt(pji * pji + qji * qji);
                                double maxSij = (Sij > Sji) ? Sij : Sji;
                                con = " WHERE Name='" + devzl1[j * 3 + 1] + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
                                IList listName = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                                PSPDEV pspline = (PSPDEV)listName[0];
                                double voltR = pspline.RateVolt;
                                //WireCategory wirewire = new WireCategory();
                                //wirewire.WireType = pspline.LineType;
                                //if (pspline.LineType == null || pspline.LineType == "")
                                //{
                                //    MessageBox.Show(pspline.Name + "的线路类型没有输入，无法进行可靠性检验", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    return;
                                //}
                                //WireCategory listware = (WireCategory)UCDeviceBase.DataService.GetObject("SelectWireCategoryByKey", wirewire);
                                double Ichange = (double)pspline.Burthen;
                                double linXij = System.Math.Sqrt(3) * voltR * Ichange ;
                                // outputZL += "'" + devzl[j * 3 + 2] + "," + "'" + devzl[j * 3 + 3] + ",";
                                if (maxSij >= linXij)
                                {
                                    lineflag = false;
                                    flag = false;
                                    lineclass linep = new lineclass(n, j);
                                    Overlinp.Add(linep);
                                    //OverPhege[n] = j;
                                }
                                outputZL += "'" + youming(devzl1[j * 3 + 2], capability) + "," + "'" + youming(devzl1[j * 3 + 3], capability) + ",";    //在此还可以判断线路是否超载

                            }
                            if (!lineflag)
                            {
                                outputZL += "不合格";
                            }
                            else
                                outputZL += "合格";
                            outputZL += "\r\n";


                        }

                        else
                        {
                            list.Add(n);
                            outputZL += devzl1[0] + "," + "为不可断裂的线路" + "\r\n";

                        }

                    }
                }
                outputZL += "注释：红色为线路超载";
                outputZL += "\r\n";
                outputZL += "操作时间为：" + datatime + "\r\n";
                outputZL += "单位：kA\\kV\\MW\\Mvar" + "\r\n";
                readLine.Close();
                if (File.Exists("result1.csv"))
                {
                    File.Delete("result1.csv");
                }

                op = new FileStream("result1.csv", FileMode.OpenOrCreate);
                str1 = new StreamWriter(op, Encoding.Default);
                str1.Write(outputZL);
                str1.Close();

                outputZL = null;
                //将各个节点的电压写入其中
                // strLine2 = readLine2.ReadLine();
                n = 0;
                bool busvflag1 = true;
                outputZL += ("网络节点电压和相角" + "\r\n");
                outputZL += ("开断支路名称" + "," + "节点电压的幅值和相角的有名值");
                for (int i = 0; i < buscount; i++)
                {
                    outputZL += (",,");
                }
                outputZL += ("是否越限" + "," + "\r\n");
                outputZL += ",";
                while ((strLine2 = readLine2.ReadLine()) != null)
                {
                    array2 = strLine2.Split(charSplit);

                    string[] devzl1 = new string[buscount * 3 + 1];

                    devzl1.Initialize();
                    int i = 0;

                    n++;
                    foreach (string str in array2)
                    {
                        if (str != "")
                        {

                            devzl1[i++] = str.ToString();

                        }
                    }
                    //for (int j = 0; j < buscount; j++)
                    //{
                    //    outputZL += devzl1[3 * j + 1] + "," + ",";
                    //}
                    outputZL += busname;
                    outputZL += ("," + "\r\n");
                    
                    if (devzl1[1] != "-1")
                    {
                        outputZL += devzl1[0] + ",";
                        for (int j = 0; j < buscount; j++)
                        {
                            PSPDEV pspDev = new PSPDEV();
                            con = " WHERE Name='" + devzl1[j * 3 + 1] + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            pspDev =(PSPDEV) UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                           
                            if (pspDev != null && pspDev.ReferenceVolt != 0)
                            {
                                volt = pspDev.ReferenceVolt;
                            }
                            else
                                volt = standvolt;
                            outputZL += "'" + (Convert.ToDouble(devzl1[j * 3 + 2]) * volt).ToString() + "," + "'" + devzl1[j * 3 + 3] + ",";
                            if (Convert.ToDouble(devzl1[j * 3 + 2]) >= TLPSPVmax || Convert.ToDouble(devzl1[j * 3 + 2]) <= TLPSPVmin)
                            {
                                busvflag1 = false;
                                flag = false;
                                lineclass _vtheta = new lineclass(n, j);
                                OverVp.Add(_vtheta);
                                // OverVhege[n] = j;
                            }
                        }
                        if (busvflag1)
                        {
                            outputZL += "合格";
                        }
                        else
                            outputZL += "不合格";
                        outputZL += "\r\n";
                    }
                    else
                    {
                        outputZL += devzl1[0] + "," + "为不可断裂的线路" + ",,,,,,";
                        outputZL += "\r\n";
                    }

                    while ((strLine2 = readLine2.ReadLine()) != null)
                    {
                        busvflag1 = true;
                        array2 = strLine2.Split(charSplit);

                        string[] devzl = new string[buscount * 3 + 1];

                        devzl.Initialize();

                        n++;
                        i = 0;
                        foreach (string str in array2)
                        {
                            if (str != "")
                            {

                                devzl[i++] = str.ToString();

                            }
                        }
                       
                        if (devzl[1] != "-1")
                        {
                            outputZL += devzl[0] + ",";
                            for (int j = 0; j < buscount; j++)
                            {
                                PSPDEV pspDev = new PSPDEV();
                                con = " WHERE Name='" + devzl[j * 3 + 1] + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                                pspDev = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                                if (pspDev != null && pspDev.ReferenceVolt != 0)
                                {
                                    volt = pspDev.ReferenceVolt;
                                }
                                else
                                    volt = standvolt;
                                outputZL += "'" + (Convert.ToDouble(devzl[j * 3 + 2]) * volt).ToString() + "," + "'" + devzl[j * 3 + 3] + ",";   //在此还可以判断线路是否超载,如果超载加入一个标记,在excel里使其变为红色

                                if (Convert.ToDouble(devzl[j * 3 + 2]) >= TLPSPVmax || Convert.ToDouble(devzl[j * 3 + 2]) <= TLPSPVmin)
                                {
                                    busvflag1 = false;
                                    flag = false;
                                    lineclass vtheta = new lineclass(n, j);
                                    OverVp.Add(vtheta);
                                    //OverVhege[n] = j;
                                }
                            }
                            if (busvflag1)
                            {
                                outputZL += "合格";
                            }
                            else
                                outputZL += "不合格";
                            outputZL += "\r\n";

                        }
                        else
                        {
                            // list.Add(n);
                            outputZL += devzl[0] + "," + "为不可断裂的线路" + ",,,,,,";
                            outputZL += "\r\n";
                        }

                    }
                }
                readLine2.Close();
                outputZL += "注释：红色为节点电压超载" + "\r\n";
                outputZL += "节点电压合格范围为电压基准值的上下限，分别为" + TLPSPVmax + "和" + TLPSPVmin + "倍" + "\r\n";
                outputZL += "操作时间为：" + datatime + "\r\n";
                outputZL += "单位：kA\\kV\\MW\\Mvar" + "\r\n";

                //op = new FileStream(System.Windows.Forms.Application.StartupPath + "\\" + "reli.txt", FileMode.OpenOrCreate);
                //str1 = new StreamWriter(op, Encoding.Default);
                //str1.Write(outputZL);
                //str1.Close();



            }
            catch (System.Exception e1)
            {
                MessageBox.Show("数据存在问题，请仔细检查以后再看结果！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return flag;
        }

        private string youming(string pij, double capability)
        {
            int lenth = pij.Length;
            int jlenth = pij.IndexOf('j');
            string shi = null;
            if (lenth > jlenth)
            {
                string a = pij.Substring(jlenth - 1, 1);
                if (pij.Substring(jlenth - 1, 1) == "-")
                {
                    shi = (Convert.ToDouble(pij.Substring(0, jlenth - 1)) * capability).ToString() + "-j" + (Convert.ToDouble(pij.Substring(jlenth + 1)) * capability);
                }
                else if (pij.Substring(jlenth - 1, 1) == "+")
                {
                    shi = (Convert.ToDouble(pij.Substring(0, jlenth - 1)) * capability).ToString() + "+j" + (Convert.ToDouble(pij.Substring(jlenth + 1)) * capability);
                }
            }

            return shi;
        }
        private string getbranchname()
        {
            FileStream dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\" + "lineP.txt", FileMode.Open);


            StreamReader readLine = new StreamReader(dh, Encoding.Default);
            char[] charSplit = new char[] { ' ' };
            //strLine = readLine.ReadLine();

            string outputZL = null;
            string strLine=null;
            while ((strLine = readLine.ReadLine()) != null)
            {
               string[] array1 = strLine.Split(charSplit);

                string[] devzl = new string[3 * brchcount + 1];

                devzl.Initialize();
                int i = 0;

               
                foreach (string str in array1)
                {
                    if (str != "")
                    {

                        devzl[i++] = str.ToString();

                    }
                }
                
                if (devzl[1] != "-1")
                {
                    for (int j = 0; j < brchcount; j++)
                    {
                        outputZL += devzl[3 * j + 1] + "," + ",";
                       
                    }
                    break;
                }
                  
            }
            readLine.Close();
            return outputZL;

        }
        private string getbusname()
        {
           
            FileStream dh2 = new FileStream(System.Windows.Forms.Application.StartupPath + "\\" + "VandTheta.txt", FileMode.Open);
            StreamReader readLine2 = new StreamReader(dh2, Encoding.Default);
          
            char[] charSplit = new char[] { ' ' };
            //strLine = readLine.ReadLine();
            string strLine=null;
            string outputZL = null;
            while ((strLine = readLine2.ReadLine()) != null)
            {
                string[] array1 = strLine.Split(charSplit);

                string[] devzl = new string[3 * buscount + 1];

                devzl.Initialize();
                int i = 0;

              
                foreach (string str in array1)
                {
                    if (str != "")
                    {

                        devzl[i++] = str.ToString();

                    }
                }

                if (devzl[1] != "-1")
                {
                    for (int j = 0; j < buscount; j++)
                    {
                        outputZL += devzl[3 * j + 1] + "," + ",";

                    }
                    break;
                }

            }
            readLine2.Close();
            return outputZL;
        }
       }    
}
