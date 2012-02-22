using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using System.IO;
using NR_PowerFlow;
using PQ_POWERFLOWLib;
using Gauss_Seidel;
using ZYZ_POWER;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using NiuLa_IdleOptimize;
using TLPSP_IdleOptimize;
using TLPSP_Disflow;
using Netron.GraphLib.Maths;
using DevExpress.Utils;
namespace Itop.TLPSP.DEVICE
{
    public class ElectricLoadCal
    {
        public ElectricLoadCal()
        {

        }
        private string MXNodeType(string nodeType)
        {
            if (nodeType == "0")
            {
                return "3";
            }
            return nodeType;
        }
        public bool DFS(IList branchlist, IList buslist, string projectSUID, float ratedCapacity)
        {
            string strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'";
            string strCon2 = null;
            string strCon = null;

            double Rad_to_Deg = 180 / Math.PI;
            double Deg_to_Rad = Math.PI / 180;

            strCon2 = " AND Type = '01'";
            strCon = strCon1 + strCon2;
            IList listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
            strCon2 = " AND Type = '05'";
            strCon = strCon1 + strCon2;
            IList listXL = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
            strCon2 = " AND Type = '02'";
            strCon = strCon1 + strCon2;
            IList listBYQ2 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
            strCon2 = " AND Type = '03'";
            strCon = strCon1 + strCon2;
            IList listBYQ3 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);

            foreach (PSPDEV devBUS in buslist)
            {
                ArrayList listLine = new ArrayList();
                ArrayList listLineTemp = new ArrayList();
                IList listBusTxt = new List<PSPDEV>();
                bool visited = false;
                foreach (PSPDEV line in listXL)
                {
                    if (line.FirstNode == devBUS.Number && !branchlist.Contains(line))
                    {
                        if (visited == false)
                        {
                            visited = true;
                            listLine.Add(line);
                        }
                        else if (visited == true)
                        {
                            listLineTemp.Add(line);
                        }
                    }
                }
                PSPDEV lineTemp = new PSPDEV();
                if (listLineTemp.Count > 0)
                {
                    lineTemp = (PSPDEV)listLineTemp[listLineTemp.Count - 1];
                }
                else
                {
                    lineTemp = (PSPDEV)listLine[listLine.Count - 1];
                }
                bool dFlag = true;
                bool flag = true;
                while ((lineTemp.LastNode > 0 && !branchlist.Contains(lineTemp)) || dFlag)
                {
                    bool TFlag = false;
                    foreach (PSPDEV lineDEV in branchlist)
                    {
                        if (lineDEV.SUID == lineTemp.SUID)
                        {
                            TFlag = true;
                        }
                    }
                    if (TFlag == true)
                    {
                        break;
                    }
                    flag = true;
                    while (flag)
                    {
                        visited = false;
                        flag = false;
                        lineTemp = (PSPDEV)listLine[listLine.Count - 1];
                        foreach (PSPDEV line in listXL)
                        {

                            if (line.FirstNode == lineTemp.LastNode && !branchlist.Contains(line.SUID))
                            {
                                bool cFlag = false;
                                foreach (PSPDEV lineDEV in branchlist)
                                {
                                    if (lineDEV.SUID == line.SUID)
                                    {
                                        cFlag = true;
                                    }
                                }
                                if (cFlag == true)
                                {
                                    continue;
                                }
                                flag = true;
                                if (visited == false)
                                {
                                    visited = true;
                                    listLine.Add(line);
                                }
                                else if (visited == true)
                                {
                                    listLineTemp.Add(line);
                                }
                            }
                        }
                    }
                    dFlag = false;
                    if (listLineTemp.Count > 0)
                    {
                        lineTemp = (PSPDEV)listLineTemp[listLineTemp.Count - 1];
                        if (!branchlist.Contains(lineTemp))
                        {
                            listLine.Add(lineTemp);
                        }
                        listLineTemp.Remove(lineTemp);
                    }
                    else
                    {
                        lineTemp = new PSPDEV();
                    }
                }
                listBusTxt.Add(devBUS);
                foreach (PSPDEV busDEV in listMX)
                {
                    foreach (PSPDEV lineD in listLine)
                    {
                        if (busDEV.Number == lineD.LastNode)
                        {
                            listBusTxt.Add(busDEV);
                        }
                    }
                }
                string strData = null;
                string strBus = null;
                string strBranch = null;
                foreach (PSPDEV dev in listLine)
                {
                    if (dev.KSwitchStatus == "0")
                    {
                        if (strBranch != null)
                        {
                            strBranch += "\r\n";
                        }
                        if (strData != null)
                        {
                            strData += "\r\n";
                        }
                        if (dev.UnitFlag == "0")
                        {
                            strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + "0";
                            //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.LineGNDC).ToString() + " " + "0" + " " + dev.Name.ToString());
                        }
                        else
                        {
                            strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0";
                            //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + ((dev.LineGNDC) * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + dev.Name.ToString());
                        }
                    }
                }
                foreach (PSPDEV dev in listBYQ2)
                {
                    if (dev.KSwitchStatus == "0")
                    {
                        if (strBranch != null)
                        {
                            strBranch += "\r\n";
                        }
                        if (strData != null)
                        {
                            //strData += "\r\n";
                        }
                        if (dev.UnitFlag == "0")
                        {
                            strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + dev.K.ToString();
                            // strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "1" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.K).ToString() + " " + dev.G.ToString() + " " + dev.Name.ToString());
                        }
                        else
                        {
                            strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + dev.K.ToString();
                            // strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "1" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.K.ToString()).ToString() + " " + dev.G.ToString() + " " + dev.Name.ToString());
                        }
                    }
                }
                foreach (PSPDEV dev in listBYQ3)
                {
                    if (dev.KSwitchStatus == "0")
                    {

                        if (strData != null)
                        {
                            // strData += "\r\n";
                        }
                        if (dev.UnitFlag == "0")
                        {
                            if (strBranch != null)
                            {
                                strBranch += "\r\n";
                            }
                            strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.HuganTQ1.ToString() + " " + dev.HuganTQ4.ToString() + " " + "0" + " " + dev.K.ToString();
                            //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "1" + " " + (dev.HuganTQ1).ToString() + " " + (dev.HuganTQ4).ToString() + " " + (dev.K).ToString() + " " + "0" + " " + dev.Name.ToString());
                            if (strBranch != null)
                            {
                                strBranch += "\r\n";
                            }
                            strBranch += dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.HuganTQ2.ToString() + " " + dev.HuganTQ5.ToString() + " " + "0" + " " + dev.StandardCurrent.ToString();
                            //strData += (dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + "1" + " " + (dev.HuganTQ2).ToString() + " " + (dev.HuganTQ5).ToString() + " " + (dev.StandardCurrent).ToString() + " " + "0" + " " + dev.Name.ToString());
                            if (strBranch != null)
                            {
                                strBranch += "\r\n";
                            }
                            strBranch += dev.FirstNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.HuganTQ3.ToString() + " " + dev.ZeroTQ.ToString() + " " + "0" + " " + dev.BigP.ToString();
                            //strData += (dev.FirstNode.ToString() + " " + dev.Flag.ToString() + " " + "1" + " " + (dev.HuganTQ3).ToString() + " " + (dev.HuganFirst).ToString() + " " + (dev.BigP).ToString() + " " + "0" + " " + dev.Name.ToString());

                        }
                        else
                        {
                            if (strBranch != null)
                            {
                                strBranch += "\r\n";
                            }
                            strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.HuganTQ1 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.HuganTQ4 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + "0" + " " + dev.K.ToString();
                            // strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "1" + " " + (dev.HuganTQ1 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.HuganTQ4 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.K).ToString() + " " + "0" + " " + dev.Name.ToString());
                            if (strBranch != null)
                            {
                                strBranch += "\r\n";
                            }
                            strBranch += dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.HuganTQ2 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.HuganTQ5 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + "0" + " " + dev.StandardCurrent.ToString();
                            //strData += (dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + "1" + " " + (dev.HuganTQ2 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.HuganTQ5 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.StandardCurrent).ToString() + " " + "0" + " " + dev.Name.ToString());
                            if (strBranch != null)
                            {
                                strBranch += "\r\n";
                            }
                            strBranch += dev.FirstNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.HuganTQ3 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.ZeroTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + "0" + " " + dev.BigP.ToString();
                            //strData += (dev.FirstNode.ToString() + " " + dev.Flag.ToString() + " " + "1" + " " + (dev.HuganTQ3 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + ((float)dev.HuganFirst * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.BigP).ToString() + " " + "0" + " " + dev.Name.ToString());
                        }
                    }
                }
                foreach (PSPDEV dev in listBusTxt)
                {
                    if (dev.KSwitchStatus == "0")
                    {
                        if (strBus != null)
                        {
                            strBus += "\r\n";
                        }
                        if (strData != null)
                        {
                            //strData += "\r\n";
                        }
                        if (dev.UnitFlag == "0")
                        {
                            strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR).ToString() + " " + dev.VoltV.ToString() + " " + ((dev.InPutP - dev.OutP)).ToString() + " " + ((dev.InPutQ - dev.OutQ)).ToString());
                            //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.LineGNDC).ToString() + " " + "0" + " " + dev.Name.ToString());
                            //if (dev.NodeType == "1")
                            //{
                            //    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + ((dev.OutP)).ToString() + " " + ((dev.OutQ)).ToString());
                            //}
                            //else if (dev.NodeType == "2")
                            //{
                            //    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + ((dev.OutP)).ToString() + " " + (dev.VoltR / dev.ReferenceVolt).ToString());
                            //}
                            //else if (dev.NodeType == "0")
                            //{
                            //    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + "0");
                            //}
                        }
                        else
                        {
                            strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + dev.VoltV.ToString() + " " + ((dev.InPutP - dev.OutP) / ratedCapacity).ToString() + " " + ((dev.InPutQ - dev.OutQ) / ratedCapacity).ToString());
                            //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + ((dev.LineGNDC) * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + dev.Name.ToString());
                            //if (dev.NodeType == "1")
                            //{
                            //    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + ((dev.OutP) / ratedCapacity).ToString() + " " + ((dev.OutQ) / ratedCapacity).ToString());
                            //}
                            //else if (dev.NodeType == "2")
                            //{
                            //    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + ((dev.OutP) / ratedCapacity).ToString() + " " + (dev.VoltR / dev.ReferenceVolt).ToString());
                            //}
                            //else if (dev.NodeType == "0")
                            //{
                            //    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + "0");
                            //}
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
                if (strBus.Contains("非数字") || strBus.Contains("正无穷大") || strBranch.Contains("非数字") || strBranch.Contains("正无穷大"))
                {
                    MessageBox.Show("缺少参数，请检查输入参数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF1.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\PF1.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH1.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\DH1.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH1.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\IH1.txt");
                }
                DisFlowCal disf = new DisFlowCal();
                disf.CurrentCal(1);
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF1.txt"))
                {
                }
                else
                {
                    return false;
                }
                FileStream pf = new FileStream(System.Windows.Forms.Application.StartupPath + "\\PF1.txt", FileMode.Open);
                StreamReader readLine = new StreamReader(pf, Encoding.Default);
                char[] charSplit = new char[] { ' ' };
                string strLine = readLine.ReadLine();
                while (strLine != null && strLine != "")
                {
                    string[] array1 = strLine.Split(charSplit);
                    strCon2 = " AND Type= '01' AND Number = " + array1[0];
                    strCon = strCon1 + strCon2;
                    PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                    if (devMX != null)
                    {
                        PSP_ElcDevice elcDev = new PSP_ElcDevice();
                        elcDev.ProjectSUID = projectSUID;
                        elcDev.DeviceSUID = devMX.SUID;
                        elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                        if (elcDev != null)
                        {
                            elcDev.COL1 = devMX.Name;
                            elcDev.COL19 = devMX.ReferenceVolt.ToString();
                            elcDev.COL20 = ratedCapacity.ToString();
                            double temp = 0.0;
                            double.TryParse(array1[1], out temp);
                            elcDev.COL2 = (temp * (devMX.ReferenceVolt)).ToString();
                            temp = 0.0;
                            double.TryParse(array1[2], out temp);
                            elcDev.COL3 = (temp * Rad_to_Deg).ToString();
                            temp = 0.0;
                            double.TryParse(array1[3], out temp);
                            elcDev.COL4 = (temp * ratedCapacity).ToString();
                            temp = 0.0;
                            double.TryParse(array1[4], out temp);
                            elcDev.COL5 = (temp * ratedCapacity).ToString();

                            Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                        }
                    }
                    strLine = readLine.ReadLine();
                }
                readLine.Close();
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH1.txt"))
                {
                }
                else
                {
                    return false;
                }
                FileStream dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH1.txt", FileMode.Open);
                readLine = new StreamReader(dh, Encoding.Default);
                strLine = readLine.ReadLine();
                while (strLine != null && strLine != "")
                {
                    string[] array1 = strLine.Split(charSplit);
                    strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                    strCon = strCon1 + strCon2;
                    PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                    if (devMX != null)
                    {
                        PSP_ElcDevice elcDev = new PSP_ElcDevice();
                        elcDev.ProjectSUID = projectSUID;
                        elcDev.DeviceSUID = devMX.SUID;
                        elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                        if (elcDev != null)
                        {
                            elcDev.COL1 = devMX.Name;
                            elcDev.COL2 = devMX.FirstNode.ToString();
                            elcDev.COL3 = devMX.LastNode.ToString();
                            elcDev.COL19 = devMX.ReferenceVolt.ToString();
                            if (Convert.ToDouble(devMX.Burthen) ==0.0)
                            {
                                elcDev.COL20 = ratedCapacity.ToString();
                            }
                            else
                                elcDev.COL20 = devMX.Burthen.ToString();
                            
                            double temp = 0.0;
                            double.TryParse(array1[3], out temp);
                            elcDev.COL4 = (temp * ratedCapacity).ToString();
                            temp = 0.0;
                            double.TryParse(array1[4], out temp);
                            elcDev.COL5 = (temp * ratedCapacity).ToString();
                            temp = 0.0;
                            double.TryParse(array1[5], out temp);
                            elcDev.COL6 = (temp * ratedCapacity).ToString();
                            temp = 0.0;
                            double.TryParse(array1[6], out temp);
                            elcDev.COL7 = (temp * ratedCapacity).ToString();

                            temp = 0.0;
                            double.TryParse(array1[7], out temp);
                            elcDev.COL8 = (temp * ratedCapacity).ToString();
                            temp = 0.0;
                            double.TryParse(array1[8], out temp);
                            elcDev.COL9 = (temp * ratedCapacity).ToString();
                            temp = 0.0;
                            double.TryParse(array1[9], out temp);
                            elcDev.COL10 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();

                            temp = 0.0;
                            double.TryParse(array1[10], out temp);
                            elcDev.COL11 = (temp * Rad_to_Deg).ToString();
                            temp = 0.0;
                            double.TryParse(array1[11], out temp);
                            elcDev.COL12 = (temp * (devMX.ReferenceVolt)).ToString();
                            temp = 0.0;
                            double.TryParse(array1[12], out temp);
                            elcDev.COL13 = (temp * (devMX.ReferenceVolt)).ToString();
                            PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                            Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                        }
                    }
                    strLine = readLine.ReadLine();
                }
                readLine.Close();
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH1.txt"))
                {
                }
                else
                {
                    return false;
                }
                FileStream ih = new FileStream(System.Windows.Forms.Application.StartupPath + "\\IH1.txt", FileMode.Open);
                readLine = new StreamReader(ih, Encoding.Default);
                strLine = readLine.ReadLine();
                while (strLine != null && strLine != "")
                {
                    string[] array1 = strLine.Split(charSplit);
                    strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                    strCon = strCon1 + strCon2;
                    PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                    if (devMX != null)
                    {
                        PSP_ElcDevice elcDev = new PSP_ElcDevice();
                        elcDev.ProjectSUID = projectSUID;
                        elcDev.DeviceSUID = devMX.SUID;
                        elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                        if (elcDev != null)
                        {
                            elcDev.COL1 = devMX.Name;
                            elcDev.COL2 = devMX.FirstNode.ToString();
                            elcDev.COL3 = devMX.LastNode.ToString();
                            elcDev.COL19 = devMX.ReferenceVolt.ToString();
                            if (Convert.ToDouble(devMX.Burthen) == 0.0)
                            {
                                elcDev.COL20 = ratedCapacity.ToString();
                            }
                            else
                                elcDev.COL20 = devMX.Burthen.ToString();
                            double temp = 0.0;
                            double.TryParse(array1[3], out temp);
                            elcDev.COL14 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();
                            temp = 0.0;
                            double.TryParse(array1[4], out temp);
                            elcDev.COL15 = (temp * Rad_to_Deg).ToString();
                            PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                            Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                        }
                    }
                    strLine = readLine.ReadLine();
                }
                readLine.Close();
            }
            foreach (PSPDEV devLine in branchlist)
            {
                PSP_ElcDevice elcDev = new PSP_ElcDevice();
                elcDev.ProjectSUID = projectSUID;
                elcDev.DeviceSUID = devLine.SUID;
                elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                strCon2 = " AND Type= '01' AND Number = " + devLine.FirstNode;
                strCon = strCon1 + strCon2;
                PSPDEV devMXI = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);

                PSP_ElcDevice elcDevi = new PSP_ElcDevice();
                elcDevi.ProjectSUID = projectSUID;
                elcDevi.DeviceSUID = devMXI.SUID;
                elcDevi = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDevi);

                strCon2 = " AND Type= '01' AND Number = " + devLine.LastNode;
                strCon = strCon1 + strCon2;
                PSPDEV devMXJ = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);

                PSP_ElcDevice elcDevj = new PSP_ElcDevice();
                elcDevj.ProjectSUID = projectSUID;
                elcDevj.DeviceSUID = devMXJ.SUID;
                elcDevj = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDevj);
                complex Vi = new complex((Convert.ToDouble(GetColValue(elcDevi, 0).COL2) / devMXI.ReferenceVolt) * Math.Sin(Convert.ToDouble(GetColValue(elcDevi, 0).COL3) * Deg_to_Rad), (Convert.ToDouble(GetColValue(elcDevi, 0).COL2) / devMXI.ReferenceVolt) * Math.Cos(Convert.ToDouble(GetColValue(elcDevi, 0).COL3) * Deg_to_Rad));
                complex Vj = new complex((Convert.ToDouble(GetColValue(elcDevj, 0).COL2) / devMXJ.ReferenceVolt) * Math.Sin(Convert.ToDouble(GetColValue(elcDevj, 0).COL3) * Deg_to_Rad), (Convert.ToDouble(GetColValue(elcDevj, 0).COL2) / devMXJ.ReferenceVolt) * Math.Cos(Convert.ToDouble(GetColValue(elcDevj, 0).COL3) * Deg_to_Rad));
                complex Iij;
                complex Iji;
                if (devLine.UnitFlag == "0")
                {
                    Iij = (Vi.complex_minus(Vj)).complex_divide(new complex(devLine.LineR, devLine.LineTQ));
                    Iji = (Vj.complex_minus(Vi)).complex_divide(new complex(devLine.LineR, devLine.LineTQ));
                }
                else
                {
                    Iij = (Vi.complex_minus(Vj)).complex_divide(new complex(devLine.LineR * ratedCapacity / (devLine.ReferenceVolt * devLine.ReferenceVolt), devLine.LineTQ * ratedCapacity / (devLine.ReferenceVolt * devLine.ReferenceVolt)));
                    Iji = (Vj.complex_minus(Vi)).complex_divide(new complex(devLine.LineR * ratedCapacity / (devLine.ReferenceVolt * devLine.ReferenceVolt), devLine.LineTQ * ratedCapacity / (devLine.ReferenceVolt * devLine.ReferenceVolt)));
                }

                complex Pij = Vi.complex_multi(Iij);
                complex Pji = Vj.complex_multi(Iji);

                if (elcDev != null)
                {
                    elcDev.COL1 = devLine.Name;
                    elcDev.COL2 = devLine.FirstNode.ToString();
                    elcDev.COL3 = devLine.LastNode.ToString();
                    elcDev.COL19 = devLine.ReferenceVolt.ToString();
                    elcDev.COL20 = ratedCapacity.ToString();


                    elcDev.COL4 = (Pij.Real * ratedCapacity).ToString();
                    elcDev.COL5 = (Pij.Image * ratedCapacity).ToString();

                    elcDev.COL6 = ((Pij.Real - Pji.Real) * ratedCapacity).ToString();
                    elcDev.COL7 = ((Pij.Image - Pij.Image) * ratedCapacity).ToString();


                    elcDev.COL8 = (Pji.Real * ratedCapacity).ToString();

                    elcDev.COL9 = (Pji.Image * ratedCapacity).ToString();

                    elcDev.COL12 = GetColValue(elcDevi, 0).COL2;

                    elcDev.COL13 = GetColValue(elcDevj, 0).COL2;

                    elcDev.COL14 = (Math.Sqrt(Iij.Real * Iij.Real + Iij.Image * Iij.Image) * ratedCapacity / (Math.Sqrt(3) * devLine.ReferenceVolt)).ToString();

                    elcDev.COL15 = (Math.Atan(Iij.Image / Iij.Real) * Rad_to_Deg).ToString();
                    PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                    Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                }
            }
            return true;
        }
        private struct gltj
        {

            public gltj(double _inputp, double _inputQ, double _outP, double _outQ)
            {
                inputP = _inputp;
                inputQ = _inputQ;
                outP = _outP;
                outQ = _outQ;
            }
            public double inputP;
            public double inputQ;
            public double outP;
            public double outQ;

        }
        Dictionary<string, gltj> mxflag = new Dictionary<string, gltj>();
        public void checkfhandfdj(string projectSUID, double ratedCapacity)
        {
            string strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'";
            string strCon2 = null;
            string strCon = null;
            strCon2 = " AND Type = '01'";
            strCon = strCon1 + strCon2;
            IList<PSPDEV> listMX = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", strCon);
            strCon2 = " AND Type = '05'";
            strCon = strCon1 + strCon2;
            IList listXL = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
            strCon2 = " AND Type = '02'";
            strCon = strCon1 + strCon2;
            IList listBYQ2 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
            strCon2 = " AND Type = '03'";
            strCon = strCon1 + strCon2;
            IList listBYQ3 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);


            //foreach (PSPDEV ps in listMX)
            //{
            //    mxflag[ps.Name] = false;
            //}
            //两绕组变压器
            foreach (PSPDEV ps in listBYQ2)
            {
                PSPDEV ipsp = getpsp(ps.IName, projectSUID);
                PSPDEV jpsp = getpsp(ps.JName, projectSUID);
                PSPDEV psdymax = new PSPDEV();
                PSPDEV psdymin = new PSPDEV();
                gltj tj = new gltj(0, 0, 0, 0);
                if (ipsp.ReferenceVolt > jpsp.ReferenceVolt)
                {
                    psdymax = ipsp;
                    psdymin = jpsp;
                }
                else
                {
                    psdymax = jpsp;
                    psdymin = ipsp;
                }

                IList listFH = getfh(psdymin.Name, projectSUID);
                IList listFDJ = getfdj(psdymin.Name, projectSUID);
                if (mxflag.ContainsKey(psdymax.SUID))
                {
                    tj = mxflag[psdymax.SUID];
                }
                foreach (PSPDEV devFDJ in listFDJ)
                {
                    if (devFDJ.UnitFlag == "0")
                    {
                        tj.outP += devFDJ.OutP;
                        tj.outQ += devFDJ.OutQ;
                    }
                    else
                    {
                        tj.outP += devFDJ.OutP / ratedCapacity;
                        tj.outQ += devFDJ.OutQ / ratedCapacity;
                    }
                }
                foreach (PSPDEV devFH in listFH)
                {
                    if (devFH.UnitFlag == "0")
                    {
                        tj.inputP += devFH.InPutP;
                        tj.inputQ += devFH.InPutQ;
                    }
                    else
                    {
                        tj.inputP += devFH.InPutP / ratedCapacity;
                        tj.inputQ += devFH.InPutQ / ratedCapacity;
                    }
                }
                mxflag[psdymax.SUID] = tj;
            }
            //三绕组变压器
            foreach (PSPDEV ps in listBYQ3)
            {
                PSPDEV ipsp = getpsp(ps.IName, projectSUID);
                PSPDEV jpsp = getpsp(ps.JName, projectSUID);
                PSPDEV kpsp = getpsp(ps.KName, projectSUID);
                PSPDEV psdymax = new PSPDEV();
                PSPDEV psdymin1 = new PSPDEV();
                PSPDEV psdymin2 = new PSPDEV();
                gltj tj = new gltj(0, 0, 0, 0);
                if (ipsp.ReferenceVolt > jpsp.ReferenceVolt && ipsp.ReferenceVolt > kpsp.ReferenceVolt)
                {
                    psdymax = ipsp;
                    psdymin1 = jpsp;
                    psdymin2 = kpsp;
                }
                if (jpsp.ReferenceVolt > ipsp.ReferenceVolt && jpsp.ReferenceVolt > kpsp.ReferenceVolt)
                {
                    psdymax = jpsp;
                    psdymin1 = ipsp;
                    psdymin2 = kpsp;
                }
                if (kpsp.ReferenceVolt > ipsp.ReferenceVolt && kpsp.ReferenceVolt > jpsp.ReferenceVolt)
                {
                    psdymax = kpsp;
                    psdymin1 = ipsp;
                    psdymin2 = jpsp;
                }

                string strCon3 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "' AND Type = '04' AND IName in( '" + psdymin1.Name + "','" + psdymin2.Name + "')";
                IList listFDJ = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon3);
                string strCon4 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "' AND Type = '12' AND IName in( '" + psdymin1.Name + "','" + psdymin2.Name + "')";
                IList listFH = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon4);

                if (mxflag.ContainsKey(psdymax.SUID))
                {
                    tj = mxflag[psdymax.SUID];
                }
                foreach (PSPDEV devFDJ in listFDJ)
                {
                    if (devFDJ.UnitFlag == "0")
                    {
                        tj.outP += devFDJ.OutP;
                        tj.outQ += devFDJ.OutQ;
                    }
                    else
                    {
                        tj.outP += devFDJ.OutP / ratedCapacity;
                        tj.outQ += devFDJ.OutQ / ratedCapacity;
                    }
                }
                foreach (PSPDEV devFH in listFH)
                {
                    if (devFH.UnitFlag == "0")
                    {
                        tj.inputP += devFH.InPutP;
                        tj.inputQ += devFH.InPutQ;
                    }
                    else
                    {
                        tj.inputP += devFH.InPutP / ratedCapacity;
                        tj.inputQ += devFH.InPutQ / ratedCapacity;
                    }
                }
                mxflag[psdymax.SUID] = tj;
            }

        }
        private PSPDEV getpsp(string name, string projectSUID)
        {
            string strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'";
            string strCon2 = " AND Type = '01' AND Name='" + name + "'";
            string strCon = strCon1 + strCon2;
            PSPDEV listMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
            return listMX;
        }
        private IList getfdj(string name, string projectSUID)
        {
            string strCon3 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "' AND Type = '04' AND IName = '" + name + "'";
            IList listFDJ = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon3);
            return listFDJ;
        }
        private IList getfh(string name, string projectSUID)
        {
            string strCon4 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "' AND Type = '12' AND IName = '" + name + "'";
            IList listFH = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon4);
            return listFH;
        }

        public bool LFCS(string projectSUID, int type, float ratedCapacity)
        {
            try
            {
                string strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'  AND KSwitchStatus ='0'";
                string strCon2 = null;
                string strCon = null;
                string strData = null;
                string strBus = null;
                string strBranch = null;
                double Rad_to_Deg =  Math.PI / 180;
                {
                   // checkfhandfdj(projectSUID, ratedCapacity);
                    strCon2 = " AND Type = '01'";
                    strCon = strCon1 + strCon2;
                    IList listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = " AND Type = '05'";
                    strCon = strCon1 + strCon2;
                    IList listXL = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = " AND Type = '02'";
                    strCon = strCon1 + strCon2;
                    IList listBYQ2 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = " AND Type = '03'";
                    strCon = strCon1 + strCon2;
                    IList listBYQ3 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    strData += (listXL.Count + listBYQ2.Count + listBYQ3.Count * 3).ToString() + " " + listMX.Count.ToString() + " " + listMX.Count.ToString() + " " + "0.00001" + " " + "100" + " " + "0" + " " + "0";
                    foreach (PSPDEV dev in listXL)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            if (strBranch != null)
                            {
                                strBranch += "\r\n";
                            }
                            if (strData != null)
                            {
                                strData += "\r\n";
                            }
                            if (dev.FirstNode==dev.LastNode)
                            {
                                if (dev.UnitFlag == "0")
                                {
                                    strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "3" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + "0" + " " + "0";
                                    strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.LineGNDC).ToString() + " " + "0" + " " + dev.Name.ToString());
                                }
                                else
                                {
                                    strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "3" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + "0";
                                    strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + ((dev.LineGNDC) * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + dev.Name.ToString());
                                }
                            } 
                            else
                            {
                                if (dev.UnitFlag == "0")
                                {
                                    strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + "0" + " " + "0";
                                    strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.LineGNDC).ToString() + " " + "0" + " " + dev.Name.ToString());
                                }
                                else
                                {
                                    strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + "0";
                                    strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + ((dev.LineGNDC) * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + dev.Name.ToString());
                                }
                            }
  
                        }
                    }
                    foreach (PSPDEV dev in listBYQ2)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            if (strBranch != null)
                            {
                                strBranch += "\r\n";
                            }
                            if (strData != null)
                            {
                                strData += "\r\n";
                            }
                            if (dev.UnitFlag == "0")
                            {
                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + dev.K.ToString() + " " + dev.G.ToString();
                                strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "1" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.K).ToString() + " " + dev.G.ToString() + " " + dev.Name.ToString());
                            }
                            else
                            {
                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + dev.K.ToString() + " " + dev.G.ToString();
                                strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "1" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.K.ToString()).ToString() + " " + dev.G.ToString() + " " + dev.Name.ToString());
                            }
                        }
                    }
                    foreach (PSPDEV dev in listBYQ3)
                    {
                        if (dev.KSwitchStatus == "0")
                        {

                            if (dev.UnitFlag == "0")
                            {
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                if (strData != null)
                                {
                                    strData += "\r\n";
                                }
                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.HuganTQ1.ToString() + " " + dev.HuganTQ4.ToString() + " " + "0" + " " + dev.K.ToString() + " " + "0";
                                strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "1" + " " + (dev.HuganTQ1).ToString() + " " + (dev.HuganTQ4).ToString() + " " + (dev.K).ToString() + " " + "0" + " " + dev.Name.ToString());
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                if (strData != null)
                                {
                                    strData += "\r\n";
                                }
                                strBranch += dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.HuganTQ2.ToString() + " " + dev.HuganTQ5.ToString() + " " + "0" + " " + dev.StandardCurrent.ToString() + " " + "0";
                                strData += (dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + "1" + " " + (dev.HuganTQ2).ToString() + " " + (dev.HuganTQ5).ToString() + " " + (dev.StandardCurrent).ToString() + " " + "0" + " " + dev.Name.ToString());
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                if (strData != null)
                                {
                                    strData += "\r\n";
                                }
                                strBranch += dev.FirstNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.HuganTQ3.ToString() + " " + dev.ZeroTQ.ToString() + " " + "0" + " " + dev.BigP.ToString() + " " + "0";
                                strData += (dev.FirstNode.ToString() + " " + dev.Flag.ToString() + " " + "1" + " " + (dev.HuganTQ3).ToString() + " " + (dev.ZeroTQ).ToString() + " " + (dev.BigP).ToString() + " " + "0" + " " + dev.Name.ToString());

                            }
                            else
                            {
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                if (strData != null)
                                {
                                    strData += "\r\n";
                                }
                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.HuganTQ1 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.HuganTQ4 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + "0" + " " + dev.K.ToString() + " " + "0";
                                strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "1" + " " + (dev.HuganTQ1 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.HuganTQ4 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.K).ToString() + " " + "0" + " " + dev.Name.ToString());
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                if (strData != null)
                                {
                                    strData += "\r\n";
                                }
                                strBranch += dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.HuganTQ2 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.HuganTQ5 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + "0" + " " + dev.StandardCurrent.ToString() + " " + "0";
                                strData += (dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + "1" + " " + (dev.HuganTQ2 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.HuganTQ5 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.StandardCurrent).ToString() + " " + "0" + " " + dev.Name.ToString());
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                if (strData != null)
                                {
                                    strData += "\r\n";
                                }
                                strBranch += dev.FirstNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.HuganTQ3 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.ZeroTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + "0" + " " + dev.BigP.ToString() + " " + "0";
                                strData += (dev.FirstNode.ToString() + " " + dev.Flag.ToString() + " " + "1" + " " + (dev.HuganTQ3 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.ZeroTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.BigP).ToString() + " " + "0" + " " + dev.Name.ToString());
                            }
                        }
                    }
                    foreach (PSPDEV dev in listMX)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            if (strBus != null)
                            {
                                strBus += "\r\n";
                            }
                            if (strData != null)
                            {
                                strData += "\r\n";
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
                            //if (mxflag.ContainsKey(dev.SUID))
                            //{
                            //    gltj tj = mxflag[dev.SUID];
                            //    outP += tj.outP;
                            //    outQ += tj.outQ;
                            //    inputP += tj.inputP;
                            //    inputQ += tj.inputQ;
                            //}
                           
                            if (dev.UnitFlag == "0")
                            {
                                outP += dev.OutP;
                                outQ += dev.OutQ;
                                inputP += dev.InPutP;
                                inputQ += dev.InPutQ ;
                                strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR ).ToString() + " " + (dev.VoltV * Rad_to_Deg).ToString() + " " + ((inputP - outP)).ToString() + " " + ((inputQ - outQ)).ToString());
                                //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.LineGNDC).ToString() + " " + "0" + " " + dev.Name.ToString());
                                if (dev.NodeType == "1")
                                {
                                    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + ((outP)).ToString() + " " + ((outQ)).ToString());
                                }
                                else if (dev.NodeType == "2")
                                {
                                    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + ((outP)).ToString() + " " + (dev.VoltR).ToString());
                                }
                                else if (dev.NodeType == "0")
                                {
                                    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + (dev.VoltR).ToString() + " " + "0");
                                }
                            }
                            else
                            {
                                outP += dev.OutP / ratedCapacity;
                                outQ += dev.OutQ / ratedCapacity;
                                inputP += dev.InPutP / ratedCapacity;
                                inputQ += dev.InPutQ / ratedCapacity;
                                strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + (dev.VoltV * Rad_to_Deg).ToString() + " " + ((inputP - outP)).ToString() + " " + ((inputQ - outQ)).ToString());
                                //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + ((dev.LineGNDC) * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + dev.Name.ToString());
                                if (dev.NodeType == "1")
                                {
                                    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + (outP).ToString() + " " + (outQ).ToString());
                                }
                                else if (dev.NodeType == "2")
                                {
                                    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + (outP).ToString() + " " + (dev.VoltR / dev.ReferenceVolt).ToString());
                                }
                                else if (dev.NodeType == "0")
                                {
                                    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + "0");
                                }
                            }
                        }
                    }
                    foreach (PSPDEV dev in listMX)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            if (strData != null)
                            {
                                strData += "\r\n";
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
                            }
                            else
                            {
                                outP += dev.OutP / ratedCapacity;
                                outQ += dev.OutQ / ratedCapacity;
                                inputP += dev.InPutP / ratedCapacity;
                                inputQ += dev.InPutQ / ratedCapacity;
                            }
                            strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + ((inputP)).ToString() + " " + ((inputQ)).ToString());

                        }
                    }
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\data.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\data.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\branch.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\branch.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\bus.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\bus.txt");
                }
                FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\data.txt"), FileMode.OpenOrCreate);
                StreamWriter str1 = new StreamWriter(VK, Encoding.Default);
                str1.Write(strData);
                str1.Close();

                FileStream VK1 = new FileStream((System.Windows.Forms.Application.StartupPath + "\\branch.txt"), FileMode.OpenOrCreate);
                StreamWriter str3 = new StreamWriter(VK1, Encoding.Default);
                str3.Write(strBranch);
                str3.Close();
                FileStream L = new FileStream((System.Windows.Forms.Application.StartupPath + "\\bus.txt"), FileMode.OpenOrCreate);
                StreamWriter str2 = new StreamWriter(L, Encoding.Default);
                str2.Write(strBus);
                str2.Close();

                if (strData.Contains("非数字") || strData.Contains("正无穷大") || strBus.Contains("非数字") || strBus.Contains("正无穷大") || strBranch.Contains("非数字") || strBranch.Contains("正无穷大"))
                {
                    MessageBox.Show("缺少参数，请检查输入参数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                if (type == 1)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF1.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\PF1.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH1.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\DH1.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH1.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\IH1.txt");
                    }
                    NIULA nr = new NIULA();
                    nr.CurrentCal();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF1.txt"))
                    {
                    }
                    else
                    {
                        return false;
                    }
                    FileStream pf = new FileStream(System.Windows.Forms.Application.StartupPath + "\\PF1.txt", FileMode.Open);
                    StreamReader readLine = new StreamReader(pf, Encoding.Default);
                    char[] charSplit = new char[] { ' ' };
                    string strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '01' AND Number = " + array1[0];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL1 = devMX.Name;
                                elcDev.COL19 = devMX.ReferenceVolt.ToString();
                                elcDev.COL20 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[1], out temp);
                                elcDev.COL2 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[2], out temp);
                                elcDev.COL3 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL4 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL5 = (temp * ratedCapacity).ToString();

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH1.txt"))
                    {
                    }
                    else
                    {
                        return false;
                    }
                    FileStream dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH1.txt", FileMode.Open);
                    readLine = new StreamReader(dh, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL1 = devMX.Name;
                                elcDev.COL2 = devMX.FirstNode.ToString();
                                elcDev.COL3 = devMX.LastNode.ToString();
                                elcDev.COL19 = devMX.ReferenceVolt.ToString();
                                if (Convert.ToDouble(devMX.Burthen) == 0.0)
                                {
                                    elcDev.COL20 = ratedCapacity.ToString();
                                }
                                else
                                    elcDev.COL20 = devMX.Burthen.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL4 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL5 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[5], out temp);
                                elcDev.COL6 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[6], out temp);
                                elcDev.COL7 = (temp * ratedCapacity).ToString();

                                temp = 0.0;
                                double.TryParse(array1[7], out temp);
                                elcDev.COL8 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[8], out temp);
                                elcDev.COL9 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[9], out temp);
                                elcDev.COL10 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();

                                temp = 0.0;
                                double.TryParse(array1[10], out temp);
                                elcDev.COL11 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[11], out temp);
                                elcDev.COL12 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[12], out temp);
                                elcDev.COL13 = (temp * (devMX.ReferenceVolt)).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH1.txt"))
                    {
                    }
                    else
                    {
                        return false;
                    }
                    FileStream ih = new FileStream(System.Windows.Forms.Application.StartupPath + "\\IH1.txt", FileMode.Open);
                    readLine = new StreamReader(ih, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL1 = devMX.Name;
                                elcDev.COL2 = devMX.FirstNode.ToString();
                                elcDev.COL3 = devMX.LastNode.ToString();
                                elcDev.COL19 = devMX.ReferenceVolt.ToString();
                                if (Convert.ToDouble(devMX.Burthen) == 0.0)
                                {
                                    elcDev.COL20 = ratedCapacity.ToString();
                                }
                                else
                                    elcDev.COL20 = devMX.Burthen.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL14 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL15 = (temp * Rad_to_Deg).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                }
                else if (type == 2)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF2.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\PF2.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH2.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\DH2.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH2.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\IH2.txt");
                    }
                    PQ_PowerFlowCalClass pq = new PQ_PowerFlowCalClass();
                    pq.CurrentCal();

                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF2.txt"))
                    {
                    }
                    else
                    {
                        return false;
                    }
                    FileStream pf = new FileStream(System.Windows.Forms.Application.StartupPath + "\\PF2.txt", FileMode.Open);
                    StreamReader readLine = new StreamReader(pf, Encoding.Default);
                    char[] charSplit = new char[] { ' ' };
                    string strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '01' AND Number = " + array1[0];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL21 = devMX.Name;
                                elcDev.COL39 = devMX.ReferenceVolt.ToString();
                                elcDev.COL40 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[1], out temp);
                                elcDev.COL22 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[2], out temp);
                                elcDev.COL23 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL24 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL25 = (temp * ratedCapacity).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH2.txt"))
                    {
                    }
                    else
                    {
                        return false;
                    }
                    FileStream dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH2.txt", FileMode.Open);
                    readLine = new StreamReader(dh, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL21 = devMX.Name;
                                elcDev.COL22 = devMX.FirstNode.ToString();
                                elcDev.COL23 = devMX.LastNode.ToString();
                                elcDev.COL39 = devMX.ReferenceVolt.ToString();
                                elcDev.COL40 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL24 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL25 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[5], out temp);
                                elcDev.COL26 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[6], out temp);
                                elcDev.COL27 = (temp * ratedCapacity).ToString();

                                temp = 0.0;
                                double.TryParse(array1[7], out temp);
                                elcDev.COL28 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[8], out temp);
                                elcDev.COL29 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[9], out temp);
                                elcDev.COL30 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();

                                temp = 0.0;
                                double.TryParse(array1[10], out temp);
                                elcDev.COL31 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[11], out temp);
                                elcDev.COL32 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[12], out temp);
                                elcDev.COL33 = (temp * (devMX.ReferenceVolt)).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH2.txt"))
                    {
                    }
                    else
                    {
                        return false;
                    }
                    FileStream ih = new FileStream(System.Windows.Forms.Application.StartupPath + "\\IH2.txt", FileMode.Open);
                    readLine = new StreamReader(ih, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL21 = devMX.Name;
                                elcDev.COL22 = devMX.FirstNode.ToString();
                                elcDev.COL23 = devMX.LastNode.ToString();
                                elcDev.COL39 = devMX.ReferenceVolt.ToString();
                                elcDev.COL40 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL34 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL35 = (temp * Rad_to_Deg).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                }
                else if (type == 3)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF3.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\PF3.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH3.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\DH3.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH3.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\IH3.txt");
                    }
                    Gauss gs = new Gauss();
                    gs.CurrentCal();

                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF3.txt"))
                    {
                    }
                    else
                    {
                        return false;
                    }
                    FileStream pf = new FileStream(System.Windows.Forms.Application.StartupPath + "\\PF3.txt", FileMode.Open);
                    StreamReader readLine = new StreamReader(pf, Encoding.Default);
                    char[] charSplit = new char[] { ' ' };
                    string strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '01' AND Number = " + array1[0];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL41 = devMX.Name;
                                elcDev.COL59 = devMX.ReferenceVolt.ToString();
                                elcDev.COL60 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[1], out temp);
                                elcDev.COL42 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[2], out temp);
                                elcDev.COL43 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL44 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL45 = (temp * ratedCapacity).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH3.txt"))
                    {
                    }
                    else
                    {
                        return false;
                    }
                    FileStream dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH3.txt", FileMode.Open);
                    readLine = new StreamReader(dh, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL41 = devMX.Name;
                                elcDev.COL42 = devMX.FirstNode.ToString();
                                elcDev.COL43 = devMX.LastNode.ToString();
                                elcDev.COL59 = devMX.ReferenceVolt.ToString();
                                elcDev.COL60 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL44 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL45 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[5], out temp);
                                elcDev.COL46 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[6], out temp);
                                elcDev.COL47 = (temp * ratedCapacity).ToString();

                                temp = 0.0;
                                double.TryParse(array1[7], out temp);
                                elcDev.COL48 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[8], out temp);
                                elcDev.COL49 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[9], out temp);
                                elcDev.COL50 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();

                                temp = 0.0;
                                double.TryParse(array1[10], out temp);
                                elcDev.COL51 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[11], out temp);
                                elcDev.COL52 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[12], out temp);
                                elcDev.COL53 = (temp * (devMX.ReferenceVolt)).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH3.txt"))
                    {
                    }
                    else
                    {
                        return false;
                    }
                    FileStream ih = new FileStream(System.Windows.Forms.Application.StartupPath + "\\IH3.txt", FileMode.Open);
                    readLine = new StreamReader(ih, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL41 = devMX.Name;
                                elcDev.COL42 = devMX.FirstNode.ToString();
                                elcDev.COL43 = devMX.LastNode.ToString();
                                elcDev.COL59 = devMX.ReferenceVolt.ToString();
                                elcDev.COL60 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL54 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL55 = (temp * Rad_to_Deg).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                }
                else if (type == 4)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF4.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\PF4.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH4.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\DH4.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH4.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\IH4.txt");
                    }
                    ZYZ zy = new ZYZ();
                    zy.CurrentCal();

                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF4.txt"))
                    {
                    }
                    else
                    {
                        return false;
                    }
                    FileStream pf = new FileStream(System.Windows.Forms.Application.StartupPath + "\\PF4.txt", FileMode.Open);
                    StreamReader readLine = new StreamReader(pf, Encoding.Default);
                    char[] charSplit = new char[] { ' ' };
                    string strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '01' AND Number = " + array1[0];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL61 = devMX.Name;
                                elcDev.COL79 = devMX.ReferenceVolt.ToString();
                                elcDev.COL80 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[1], out temp);
                                elcDev.COL62 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[2], out temp);
                                elcDev.COL63 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL64 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL65 = (temp * ratedCapacity).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH4.txt"))
                    {
                    }
                    else
                    {
                        return false;
                    }
                    FileStream dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH4.txt", FileMode.Open);
                    readLine = new StreamReader(dh, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL61 = devMX.Name;
                                elcDev.COL62 = devMX.FirstNode.ToString();
                                elcDev.COL63 = devMX.LastNode.ToString();
                                elcDev.COL79 = devMX.ReferenceVolt.ToString();
                                elcDev.COL80 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL64 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL65 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[5], out temp);
                                elcDev.COL66 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[6], out temp);
                                elcDev.COL67 = (temp * ratedCapacity).ToString();

                                temp = 0.0;
                                double.TryParse(array1[7], out temp);
                                elcDev.COL68 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[8], out temp);
                                elcDev.COL69 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[9], out temp);
                                elcDev.COL70 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();

                                temp = 0.0;
                                double.TryParse(array1[10], out temp);
                                elcDev.COL71 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[11], out temp);
                                elcDev.COL72 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[12], out temp);
                                elcDev.COL73 = (temp * (devMX.ReferenceVolt)).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH4.txt"))
                    {
                    }
                    else
                    {
                        return false;
                    }
                    FileStream ih = new FileStream(System.Windows.Forms.Application.StartupPath + "\\IH4.txt", FileMode.Open);
                    readLine = new StreamReader(ih, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL61 = devMX.Name;
                                elcDev.COL62 = devMX.FirstNode.ToString();
                                elcDev.COL63 = devMX.LastNode.ToString();
                                elcDev.COL79 = devMX.ReferenceVolt.ToString();
                                elcDev.COL80 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL74 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL75 = (temp * Rad_to_Deg).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("潮流计算结果不收敛，请检查输入参数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;


        }

        public bool DataCheck(string projectSUID)
        {
            string strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'";
            string strCon2 = null;
            string strCon = null;
            {
                WaitDialogForm wait = new WaitDialogForm("", "正在处理数据, 请稍候...");
                strCon2 = " AND Type = '01'";
                strCon = strCon1 + strCon2;
                IList listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                strCon2 = " AND Type = '05'";
                strCon = strCon1 + strCon2;
                IList listXL = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                strCon2 = " AND Type = '02'";
                strCon = strCon1 + strCon2;
                IList listBYQ2 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                strCon2 = " AND Type = '03'";
                strCon = strCon1 + strCon2;
                IList listBYQ3 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                Hashtable ht = new Hashtable();

                foreach (PSPDEV dev in listMX)
                {
                    if (dev.Number <= 0)
                    {
                        wait.Close();
                        MessageBox.Show("母线" + dev.Name + "编号不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    else
                    {
                        if (ht.Contains(dev.Number))
                        {
                            wait.Close();
                            MessageBox.Show("母线" + dev.Name + "," + ((PSPDEV)ht[dev.Number]).Name + "编号重复", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                        else
                        {
                            ht.Add(dev.Number, dev);
                        }
                    }
                    if (dev.KSwitchStatus=="投入运行"||dev.KSwitchStatus=="退出运行")
                    {
                        wait.Close();
                        MessageBox.Show("母线" + dev.Name + "运行方式重新点击一次", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                ht.Clear();
                foreach (PSPDEV dev in listXL)
                {
                    if (dev.Number <= 0)
                    {
                        wait.Close();
                        MessageBox.Show("线路" + dev.Name + "编号不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    else
                    {
                        if (dev.FirstNode <= 0)
                        {
                            wait.Close();
                            MessageBox.Show("线路" + dev.Name + "没有i侧母线", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                        if (dev.LastNode <= 0)
                        {
                            wait.Close();
                            MessageBox.Show("线路" + dev.Name + "没有j侧母线", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                        if (ht.Contains(dev.Number))
                        {
                            wait.Close();
                            MessageBox.Show("线路" + dev.Name + "," + ((PSPDEV)ht[dev.Number]).Name + "编号重复", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                        else
                        {
                            ht.Add(dev.Number, dev);
                        }
                    }
                    if (dev.KSwitchStatus == "投入运行" || dev.KSwitchStatus == "退出运行")
                    {
                        wait.Close();
                        MessageBox.Show("线路" + dev.Name + "运行方式重新点击一次", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                foreach (PSPDEV dev in listBYQ2)
                {
                    if (dev.FirstNode <= 0)
                    {
                        wait.Close();
                        MessageBox.Show("两绕组变压器" + dev.Name + "没有i侧母线", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    if (dev.LastNode <= 0)
                    {
                        wait.Close();
                        MessageBox.Show("两绕组变压器" + dev.Name + "没有j侧母线", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    if (dev.KSwitchStatus == "投入运行" || dev.KSwitchStatus == "退出运行")
                    {
                        wait.Close();
                        MessageBox.Show("两绕组变压器" + dev.Name + "运行方式重新点击一次", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                foreach (PSPDEV dev in listBYQ3)
                {
                    if (dev.FirstNode <= 0)
                    {
                        wait.Close();
                        MessageBox.Show("三绕组变压器" + dev.Name + "没有i侧母线", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    if (dev.LastNode <= 0)
                    {
                        wait.Close();
                        MessageBox.Show("三绕组变压器" + dev.Name + "没有j侧母线", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    if (dev.Flag <= 0)
                    {
                        wait.Close();
                        MessageBox.Show("三绕组变压器" + dev.Name + "没有k侧母线", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    if (dev.KSwitchStatus == "投入运行" || dev.KSwitchStatus == "退出运行")
                    {
                        wait.Close();
                        MessageBox.Show("三绕组变压器" + dev.Name + "运行方式重新点击一次", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                wait.Close();
            }
           
            return true;

        }
        public bool ORP(string projectSUID, float ratedCapacity)
        {
            frnReport wFrom = new frnReport();
            try
            {
          
                wFrom.Text = "无功优化";
                wFrom.Show();
                string strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "' AND KSwitchStatus ='0'";
                string strCon2 = null;
                string strCon = null;
                string strData = null;
                string strBus = null;
                string strBranch = null;
                wFrom.ShowText += "正在准备数据\t" + System.DateTime.Now.ToString();
                double Rad_to_Deg = 180 / Math.PI;
                {
                    strCon2 = " AND Type = '01'";
                    strCon = strCon1 + strCon2;
                    IList listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = " AND Type = '05'";
                    strCon = strCon1 + strCon2;
                    IList listXL = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = " AND Type = '02'";
                    strCon = strCon1 + strCon2;
                    IList listBYQ2 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = " AND Type = '03'";
                    strCon = strCon1 + strCon2;
                    IList listBYQ3 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    strData += (listXL.Count + listBYQ2.Count + listBYQ3.Count * 3).ToString() + " " + listMX.Count.ToString() + " " + listMX.Count.ToString() + " " + "0.00001" + " " + "100" + " " + "0" + " " + "0";
                    foreach (PSPDEV dev in listXL)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            if (strBranch != null)
                            {
                                strBranch += "\r\n";
                            }
                            if (dev.FirstNode == dev.LastNode)
                            {
                                if (dev.UnitFlag == "0")
                                {
                                    strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "3" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + "0" + " " + "0" + " " + dev.iV.ToString() + " " + dev.jV.ToString();

                                }
                                else
                                {
                                    strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "3" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + "0" + " " + (dev.iV / ratedCapacity).ToString() + " " + (dev.jV / ratedCapacity).ToString();

                                }
                            }
                            else
                            {
                                if (dev.UnitFlag == "0")
                                {
                                    strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + "0" + " " + "0" + " " + "0" + " " + "0";

                                }
                                else
                                {
                                    strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + "0" + " " + "0" + " " + "0";

                                }
                            }
                        }
                    }
                    foreach (PSPDEV dev in listBYQ2)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            if (strBranch != null)
                            {
                                strBranch += "\r\n";
                            }
                            if (dev.UnitFlag == "0")
                            {
                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + dev.K.ToString() + " " + dev.G.ToString() + " " + dev.iV.ToString() + " " + dev.jV.ToString();

                            }
                            else
                            {
                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + dev.K.ToString() + " " + dev.G.ToString() + " " + dev.iV.ToString() + " " + dev.jV.ToString();

                            }
                        }
                    }
                    foreach (PSPDEV dev in listBYQ3)
                    {
                        if (dev.KSwitchStatus == "0")
                        {

                            if (dev.UnitFlag == "0")
                            {
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.HuganTQ1.ToString() + " " + dev.HuganTQ4.ToString() + " " + "0" + " " + dev.K.ToString() + " " + dev.G.ToString() + " " + dev.iV.ToString() + " " + dev.jV.ToString();
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                strBranch += dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.HuganTQ2.ToString() + " " + dev.HuganTQ5.ToString() + " " + "0" + " " + dev.StandardCurrent.ToString() + " " + dev.G.ToString() + " " + dev.iV.ToString() + " " + dev.jV.ToString();
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                strBranch += dev.FirstNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.HuganTQ3.ToString() + " " + dev.ZeroTQ.ToString() + " " + "0" + " " + dev.BigP.ToString() + " " + dev.G.ToString() + " " + dev.iV.ToString() + " " + dev.jV.ToString();


                            }
                            else
                            {
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.HuganTQ1 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.HuganTQ4 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + "0" + " " + dev.K.ToString() + " " + dev.G.ToString() + " " + dev.iV.ToString() + " " + dev.jV.ToString();
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                strBranch += dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.HuganTQ2 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.HuganTQ5 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + "0" + " " + dev.StandardCurrent.ToString() + " " + dev.G.ToString() + " " + dev.iV.ToString() + " " + dev.jV.ToString();
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                strBranch += dev.FirstNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.HuganTQ3 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.ZeroTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + "0" + " " + dev.BigP.ToString() + " " + dev.G.ToString() + " " + dev.iV.ToString() + " " + dev.jV.ToString();

                            }
                        }
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
                            //if (mxflag.ContainsKey(dev.SUID))
                            //{
                            //    gltj tj = mxflag[dev.SUID];
                            //    outP += tj.outP;
                            //    outQ += tj.outQ;
                            //    inputP += tj.inputP;
                            //    inputQ += tj.inputQ;
                            //}

                            if (dev.UnitFlag == "0")
                            {
                                outP += dev.OutP;
                                outQ += dev.OutQ;
                                inputP += dev.InPutP;
                                inputQ += dev.InPutQ;
                                strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR).ToString() + " " + dev.VoltV.ToString() + " " + ((outP)).ToString() + " " + ((outQ)).ToString() + " " + ((inputP)).ToString() + " " + ((inputQ)).ToString() + " " + dev.LineLevel.ToString() + " " + dev.LineType.ToString() + " " + ((dev.Vjmin)).ToString() + " " + ((dev.Vjmax)).ToString() + " " + ((dev.iV)).ToString() + " " + ((dev.jV)).ToString() + " " + ((dev.Vimin)).ToString() + " " + ((dev.Vimax)).ToString() + " " + ((dev.Vk0)).ToString());
                                //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.LineGNDC).ToString() + " " + "0" + " " + dev.Name.ToString());

                            }
                            else
                            {
                                outP += dev.OutP/ratedCapacity;
                                outQ += dev.OutQ / ratedCapacity;
                                inputP += dev.InPutP / ratedCapacity;
                                inputQ += dev.InPutQ / ratedCapacity;
                                strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + dev.VoltV.ToString() + " " + (outP).ToString() + " " + (outQ).ToString() + " " + (inputP).ToString() + " " + (inputQ).ToString() + " " + dev.LineLevel.ToString() + " " + dev.LineType.ToString() + " " + ((dev.Vjmin) / ratedCapacity).ToString() + " " + ((dev.Vjmax) / ratedCapacity).ToString() + " " + ((dev.iV) / dev.ReferenceVolt).ToString() + " " + ((dev.jV) / dev.ReferenceVolt).ToString() + " " + ((dev.Vimin) / ratedCapacity).ToString() + " " + ((dev.Vimax) / ratedCapacity).ToString() + " " + ((dev.Vk0) / ratedCapacity).ToString());
                                //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + ((dev.LineGNDC) * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + dev.Name.ToString());

                            }
                        }
                    }
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\data.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\data.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\branch.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\branch.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\bus.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\bus.txt");
                }
                FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\data.txt"), FileMode.OpenOrCreate);
                StreamWriter strVK = new StreamWriter(VK, Encoding.Default);
                strVK.Write(strData);
                strVK.Close();

                FileStream VK1 = new FileStream((System.Windows.Forms.Application.StartupPath + "\\branch.txt"), FileMode.OpenOrCreate);
                StreamWriter strVK1 = new StreamWriter(VK1, Encoding.Default);
                strVK1.Write(strBranch);
                strVK1.Close();
                FileStream L = new FileStream((System.Windows.Forms.Application.StartupPath + "\\bus.txt"), FileMode.OpenOrCreate);
                StreamWriter strL = new StreamWriter(L, Encoding.Default);
                strL.Write(strBus);
                strL.Close();

                if (strBus.Contains("非数字") || strBus.Contains("正无穷大") || strBranch.Contains("非数字") || strBranch.Contains("正无穷大"))
                {
                    wFrom.ShowText += "\r\n缺少参数，计算失败\t" + System.DateTime.Now.ToString();
                    MessageBox.Show("缺少参数，请检查输入参数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Volt.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\Volt.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Transformer.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\Transformer.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Generator.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\Generator.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\GND.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\GND.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\busidle.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\busidle.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\branchidle.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\branchidle.txt");
                }
                wFrom.ShowText += "\r\n开始潮流计算\t" + System.DateTime.Now.ToString();
                NiuLaIdle niulaidle = new NiuLaIdle();
                niulaidle.CurrentCal();
                wFrom.ShowText += "\r\n根据潮流计算结果，开始进行无功优化计算\t" + System.DateTime.Now.ToString();
                IdleOptimize idlOptimize = new IdleOptimize();
                idlOptimize.Optimize();
                wFrom.ShowText += "\r\n开始处理计算结果，形成报表\t" + System.DateTime.Now.ToString();
                string outV = null;
                string outT = null;
                string outG = null;
                string outQC = null;
                FileStream dhoutV = new FileStream(System.Windows.Forms.Application.StartupPath + "\\Volt.txt", FileMode.Open);
                StreamReader readLineoutV = new StreamReader(dhoutV, Encoding.Default);

                FileStream dhoutT = new FileStream(System.Windows.Forms.Application.StartupPath + "\\Transformer.txt", FileMode.Open);
                StreamReader readLineoutT = new StreamReader(dhoutT, Encoding.Default);

                FileStream dhoutG = new FileStream(System.Windows.Forms.Application.StartupPath + "\\Generator.txt", FileMode.Open);
                StreamReader readLineoutG = new StreamReader(dhoutG, Encoding.Default);

                FileStream dhoutQC = new FileStream(System.Windows.Forms.Application.StartupPath + "\\GND.txt", FileMode.Open);
                StreamReader readLineoutQC = new StreamReader(dhoutQC, Encoding.Default);

                FileStream dhoutPQ = new FileStream(System.Windows.Forms.Application.StartupPath + "\\busidle.txt", FileMode.Open);
                StreamReader readLineoutPQ = new StreamReader(dhoutPQ, Encoding.Default);
                IList listGEN = new List<object>();
                IList listBUS = new List<object>();
                char[] charSplit = new char[] { ' ' };
                string strLine = readLineoutV.ReadLine();
                string[] arry;
                outV += ("电压调整表" + "\r\n" + "\r\n");
                outV += ("计算日期：" + System.DateTime.Now.ToString() + "\r\n" + "\r\n");
                outV += ("单位：KV" + "\r\n" + "\r\n");
                outV += ("母线名" + "," + "调整后电压" + "," + "调整前电压" + "," + "电压下限" + "," + "电压上限" + "\r\n");
                PSPDEV CR;
                double voltExcursion = 0;
                int numFlag = 0, numVolt = 0;
                int num = 0;
                string flag;
                while (strLine != null)
                {
                    arry = strLine.Split(charSplit);
                    strCon2 = " AND Type= '01' AND Name = '" + arry[0] + "'"; 
                    strCon = strCon1 + strCon2;
                    CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                    outV += arry[0] + "," + Convert.ToDouble(arry[1]) * CR.ReferenceVolt + "," + Convert.ToDouble(arry[2]) * CR.ReferenceVolt + "," + Convert.ToDouble(arry[3]) * CR.ReferenceVolt + "," + Convert.ToDouble(arry[4]) * CR.ReferenceVolt + "\r\n";

                    //voltExcursion = (Convert.ToDouble(arry[1]) * CR.ReferenceVolt - CR.RateVolt) / CR.RateVolt;
                    //flag = voltFlag(voltExcursion, CR.RateVolt);
                    if (Convert.ToDouble(arry[1]) >= Convert.ToDouble(arry[3]) && Convert.ToDouble(arry[1]) <= Convert.ToDouble(arry[4]))
                    {
                        numFlag++;
                    }
                    //voltExcursion = (Convert.ToDouble(arry[2]) * CR.ReferenceVolt - CR.RateVolt) / CR.RateVolt;
                    //flag = voltFlag(voltExcursion, CR.RateVolt);
                    if (Convert.ToDouble(arry[2]) >= Convert.ToDouble(arry[3]) && Convert.ToDouble(arry[2]) <= Convert.ToDouble(arry[4]))
                    {
                        numVolt++;
                    }
                    num++;
                    strLine = readLineoutV.ReadLine();
                }
                readLineoutV.Close();
                outV += "优化前电压合格率" + "," + (float)((float)numVolt / (float)num) + "\r\n";
                outV += "优化后电压合格率" + "," + (float)((float)numFlag / (float)num) + "\r\n";
                string strLineT = readLineoutT.ReadLine();
                string[] arryT;
                outT += ("变压器调整表" + "\r\n" + "\r\n");
                outT += ("计算日期：" + System.DateTime.Now.ToString() + "\r\n" + "\r\n");
                outT += ("单位：p.u." + "\r\n" + "\r\n");
                outT += ("I侧母线" + "," + "J侧母线" + "," + "支路名" + "," + "调整后变比" + "," + "调整前变比" + "," + "变比下限" + "," + "变比上限" + "\r\n");

                while (strLineT != null)
                {
                    arryT = strLineT.Split(charSplit);
                    outT += arryT[0] + "," + arryT[1] + "," + arryT[2] + "," + arryT[3] + "," + arryT[4] + "," + arryT[5] + "," + arryT[6] + "\r\n";
                    strLineT = readLineoutT.ReadLine();
                }
                readLineoutT.Close();

                string strLineG = readLineoutG.ReadLine();
                string[] arryG;
                outG += ("发电调整表" + "\r\n" + "\r\n");
                outG += ("计算日期：" + System.DateTime.Now.ToString() + "\r\n" + "\r\n");
                outG += ("单位：KV/MW/Mvar" + "\r\n" + "\r\n");
                outG += ("母线名" + "," + "控制类型" + "," + "调整后电压" + "," + "调整后有功" + "," + "调整后无功" + "," + "调整前电压" + "," + "调整前有功" + "," + "调整前无功" + "\r\n");

                while (strLineG != null)
                {
                    arryG = strLineG.Split(charSplit);
                    //arry = strLine.Split(charSplit);
                    strCon2 = " AND Type= '01' AND Name = '" + arryG[0] + "'";
                    strCon = strCon1 + strCon2;
                    CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                    outG += arryG[0] + "," + arryG[1] + "," + Convert.ToDouble(arryG[2]) * CR.ReferenceVolt + "," + Convert.ToDouble(arryG[3]) * ratedCapacity + "," + Convert.ToDouble(arryG[4]) * ratedCapacity + "," + Convert.ToDouble(arryG[5]) * CR.ReferenceVolt + "," + Convert.ToDouble(arryG[6]) * ratedCapacity + "," + Convert.ToDouble(arryG[7]) * ratedCapacity + "\r\n";
                    strLineG = readLineoutG.ReadLine();
                    listGEN.Add(arryG);
                }
                readLineoutG.Close();

                string strBUS = readLineoutPQ.ReadLine();
                string[] arryBUS;
                double OUTP = 0;
                double OUTQ = 0;
                double OUTP1 = 0;
                double OUTQ1 = 0;
                while (strBUS != null)
                {
                    arryBUS = strBUS.Split(charSplit, StringSplitOptions.RemoveEmptyEntries);
                    OUTP1 += Convert.ToDouble(arryBUS[7]) - Convert.ToDouble(arryBUS[5]);
                    OUTQ1 += Convert.ToDouble(arryBUS[8]) - Convert.ToDouble(arryBUS[6]);       
                    strBUS = readLineoutPQ.ReadLine();
                }
                foreach (string[] ab in listGEN)
                {
                    OUTP += Convert.ToDouble(ab[3]) - Convert.ToDouble(ab[6]);
                    OUTQ += Convert.ToDouble(ab[4]) - Convert.ToDouble(ab[7]);                  
                }
                OUTP = Math.Abs(OUTP);
                OUTQ = Math.Abs(OUTQ);
                readLineoutPQ.Close();
               // outG += "优化前功率因数" + "," + OUTP1 / Math.Sqrt(OUTP1 * OUTP1 + OUTQ1 * OUTQ1) + "\r\n";
              //  outG += "优化后功率因数" + "," + (Math.Abs(OUTP1) + OUTP) / Math.Sqrt((Math.Abs(OUTP1) + OUTP) * (OUTP1 + OUTP) + (Math.Abs(OUTQ1) - OUTQ) * (Math.Abs(OUTQ1) - OUTQ)) + "\r\n";

                string strLineQC = readLineoutQC.ReadLine();
                string[] arryQC;
                outQC += ("无功补偿调整表" + "\r\n" + "\r\n");
                outQC += ("计算日期：" + System.DateTime.Now.ToString() + "\r\n" + "\r\n");
                outQC += ("单位：KV/Mvar" + "\r\n" + "\r\n");
                outQC += ("母线名" + "," + "支路名" + "," + "调整后电压" + "," + "补偿的电容" + "," + "补偿的电抗" + "," + "调整前电压" + "\r\n");

                while (strLineQC != null)
                {
                    arryQC = strLineQC.Split(charSplit);
                   // arry = strLine.Split(charSplit);
                    strCon2 = " AND Type= '01' AND Name = '" + arryQC[0] + "'";
                    strCon = strCon1 + strCon2;
                    CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                    outQC += arryQC[0] + "," + arryQC[1] + "," + Convert.ToDouble(arryQC[2]) * CR.ReferenceVolt + "," + Convert.ToDouble(arryQC[3]) * ratedCapacity + "," + Convert.ToDouble(arryQC[4]) * ratedCapacity + "," + Convert.ToDouble(arryQC[5]) * CR.ReferenceVolt + "\r\n";
                    strLineQC = readLineoutQC.ReadLine();
                }
                readLineoutQC.Close();

                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result1.csv"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                }
                FileStream temp1 = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result1.csv"), FileMode.OpenOrCreate);
                StreamWriter str1 = new StreamWriter(temp1, Encoding.Default);
                str1.Write(outV);
                str1.Close();

                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result2.csv"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\result2.csv");
                }
                FileStream temp2 = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result2.csv"), FileMode.OpenOrCreate);
                StreamWriter str2 = new StreamWriter(temp2, Encoding.Default);
                str2.Write(outT);
                str2.Close();
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result3.csv"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\result3.csv");
                }
                FileStream temp3 = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result3.csv"), FileMode.OpenOrCreate);
                StreamWriter str3 = new StreamWriter(temp3, Encoding.Default);
                str3.Write(outG);
                str3.Close();

                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result4.csv"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\result4.csv");
                }
                FileStream temp4 = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result4.csv"), FileMode.OpenOrCreate);
                StreamWriter str4 = new StreamWriter(temp4, Encoding.Default);
                str4.Write(outQC);
                str4.Close();

                Excel.Application result1 = new Excel.Application();
                result1.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                Excel.Worksheet xSheet1 = (Excel.Worksheet)result1.Worksheets[1];
                result1.Worksheets.Add(System.Reflection.Missing.Value, xSheet1, 3, System.Reflection.Missing.Value);

                Excel.Application result2 = new Excel.Application();
                result2.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result2.csv");
                Excel.Worksheet tempSheet2 = (Excel.Worksheet)result2.Worksheets.get_Item(1);
                Excel.Application result3 = new Excel.Application();
                result3.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result3.csv");
                Excel.Worksheet tempSheet3 = (Excel.Worksheet)result3.Worksheets.get_Item(1);
                Excel.Application result4 = new Excel.Application();
                result4.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result4.csv");
                Excel.Worksheet tempSheet4 = (Excel.Worksheet)result4.Worksheets.get_Item(1);
                Excel.Worksheet newWorksheet2 = (Excel.Worksheet)result1.Worksheets.get_Item(2);
                Excel.Worksheet newWorksheet3 = (Excel.Worksheet)result1.Worksheets.get_Item(3);
                Excel.Worksheet newWorksheet4 = (Excel.Worksheet)result1.Worksheets.get_Item(4);
                newWorksheet2.Name = "变压器调整表";
                newWorksheet3.Name = "发电机调整表";
                newWorksheet4.Name = "无功补偿调整表";
                xSheet1.Name = "电压调整表";
                result1.Visible = true;

                tempSheet2.Cells.Select();
                tempSheet2.Cells.Copy(System.Reflection.Missing.Value);
                newWorksheet2.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                tempSheet3.Cells.Select();
                tempSheet3.Cells.Copy(System.Reflection.Missing.Value);
                newWorksheet3.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                tempSheet4.Cells.Select();
                tempSheet4.Cells.Copy(System.Reflection.Missing.Value);
                newWorksheet4.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);

                System.Windows.Forms.Clipboard.Clear();

                xSheet1.get_Range(xSheet1.Cells[1, 1], xSheet1.Cells[1, 5]).MergeCells = true;
                xSheet1.get_Range(xSheet1.Cells[3, 1], xSheet1.Cells[3, 5]).MergeCells = true;
                xSheet1.get_Range(xSheet1.Cells[5, 1], xSheet1.Cells[5, 5]).MergeCells = true;
                xSheet1.get_Range(xSheet1.Cells[1, 1], xSheet1.Cells[1, 1]).Font.Size = 16;
                xSheet1.get_Range(xSheet1.Cells[1, 1], xSheet1.Cells[7, 5]).Font.Name = "黑体";
                xSheet1.get_Range(xSheet1.Cells[1, 1], xSheet1.Cells[1, 1]).Font.ColorIndex = 3;
                xSheet1.get_Range(xSheet1.Cells[6, 1], xSheet1.Cells[xSheet1.UsedRange.Rows.Count, 5]).NumberFormat = "0.00_ ";
                xSheet1.get_Range(xSheet1.Cells[xSheet1.UsedRange.Rows.Count - 1, 2], xSheet1.Cells[xSheet1.UsedRange.Rows.Count - 1, 2]).NumberFormatLocal = "0.00%";
                xSheet1.get_Range(xSheet1.Cells[xSheet1.UsedRange.Rows.Count, 2], xSheet1.Cells[xSheet1.UsedRange.Rows.Count, 2]).NumberFormatLocal = "0.00%";
                //xSheet1.get_Range(xSheet1.Cells[1, 1], xSheet1.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //xSheet1.get_Range(xSheet1.Cells[1, 1], xSheet1.Cells[1, 1]).Interior.ColorIndex = 3;
                //xSheet1.get_Range(xSheet1.Cells[6, 1], xSheet1.Cells[xSheet1.UsedRange.Rows.Count, 1]).Interior.ColorIndex = 6;

                newWorksheet2.get_Range(newWorksheet2.Cells[1, 1], newWorksheet2.Cells[1, 7]).MergeCells = true;
                newWorksheet2.get_Range(newWorksheet2.Cells[3, 1], newWorksheet2.Cells[3, 7]).MergeCells = true;
                newWorksheet2.get_Range(newWorksheet2.Cells[5, 1], newWorksheet2.Cells[5, 7]).MergeCells = true;
                newWorksheet2.get_Range(newWorksheet2.Cells[1, 1], newWorksheet2.Cells[1, 1]).Font.Size = 16;
                newWorksheet2.get_Range(newWorksheet2.Cells[1, 1], newWorksheet2.Cells[7, 7]).Font.Name = "黑体";
                newWorksheet2.get_Range(newWorksheet2.Cells[1, 1], newWorksheet2.Cells[1, 1]).Font.ColorIndex = 3;
                newWorksheet2.get_Range(newWorksheet2.Cells[6, 1], newWorksheet2.Cells[newWorksheet2.UsedRange.Rows.Count, 7]).NumberFormat = "0.00_ ";

                newWorksheet3.get_Range(newWorksheet3.Cells[1, 1], newWorksheet3.Cells[1, 8]).MergeCells = true;
                newWorksheet3.get_Range(newWorksheet3.Cells[3, 1], newWorksheet3.Cells[3, 8]).MergeCells = true;
                newWorksheet3.get_Range(newWorksheet3.Cells[5, 1], newWorksheet3.Cells[5, 8]).MergeCells = true;
                newWorksheet3.get_Range(newWorksheet3.Cells[1, 1], newWorksheet3.Cells[1, 1]).Font.Size = 16;
                newWorksheet3.get_Range(newWorksheet3.Cells[1, 1], newWorksheet3.Cells[7, 8]).Font.Name = "黑体";
                newWorksheet3.get_Range(newWorksheet3.Cells[1, 1], newWorksheet3.Cells[1, 1]).Font.ColorIndex = 3;
                newWorksheet3.get_Range(newWorksheet3.Cells[6, 1], newWorksheet3.Cells[newWorksheet3.UsedRange.Rows.Count, 8]).NumberFormat = "0.00_ ";
                newWorksheet4.get_Range(newWorksheet4.Cells[1, 1], newWorksheet4.Cells[1, 6]).MergeCells = true;
                newWorksheet4.get_Range(newWorksheet4.Cells[3, 1], newWorksheet4.Cells[3, 6]).MergeCells = true;
                newWorksheet4.get_Range(newWorksheet4.Cells[5, 1], newWorksheet4.Cells[5, 6]).MergeCells = true;
                newWorksheet4.get_Range(newWorksheet4.Cells[1, 1], newWorksheet4.Cells[1, 1]).Font.Size = 16;
                newWorksheet4.get_Range(newWorksheet4.Cells[1, 1], newWorksheet4.Cells[7, 6]).Font.Name = "黑体";
                newWorksheet4.get_Range(newWorksheet4.Cells[1, 1], newWorksheet4.Cells[1, 1]).Font.ColorIndex = 3;
                newWorksheet4.get_Range(newWorksheet4.Cells[6, 1], newWorksheet4.Cells[newWorksheet4.UsedRange.Rows.Count, 6]).NumberFormat = "0.00_ ";

                xSheet1.Rows.AutoFit();
                xSheet1.Columns.AutoFit();
                newWorksheet2.Rows.AutoFit();
                newWorksheet2.Columns.AutoFit();
                newWorksheet3.Rows.AutoFit();
                newWorksheet3.Columns.AutoFit();
                newWorksheet4.Rows.AutoFit();
                newWorksheet4.Columns.AutoFit();

               // xSheet1.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "无功优化.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);
                result1.DisplayAlerts = false;
                result2.DisplayAlerts = false;
                result3.DisplayAlerts = false;
                result4.DisplayAlerts = false;
                result2.Workbooks.Close();
                result2.Quit();
                result3.Workbooks.Close();
                result3.Quit();
                result4.Workbooks.Close();
                result4.Quit();
                wFrom.ShowText += "\r\n计算成功\t" + System.DateTime.Now.ToString();
                GC.Collect();
                return true;
            }
            catch (System.Exception ex)
            {
                wFrom.ShowText += "\r\n计算失败\t" + System.DateTime.Now.ToString();
                return false;
            }
        }
        private DeviceCOL GetColValue(PSP_ElcDevice elcDEV, int order)
        {
            DeviceCOL devCol = new DeviceCOL();
            if (order == 0)
            {
                devCol.COL1 =String.IsNullOrEmpty(elcDEV.COL1) ? "0" : elcDEV.COL1;
                devCol.COL2 = String.IsNullOrEmpty(elcDEV.COL2) ? "0" : elcDEV.COL2;
                devCol.COL3 = String.IsNullOrEmpty(elcDEV.COL3) ? "0" : elcDEV.COL3;
                devCol.COL4 = String.IsNullOrEmpty(elcDEV.COL4) ? "0" : elcDEV.COL4;
                devCol.COL5 = String.IsNullOrEmpty(elcDEV.COL5) ? "0" : elcDEV.COL5;
                devCol.COL6 = String.IsNullOrEmpty(elcDEV.COL6) ? "0" : elcDEV.COL6;
                devCol.COL7 = String.IsNullOrEmpty(elcDEV.COL7) ? "0" : elcDEV.COL7;
                devCol.COL8 = String.IsNullOrEmpty(elcDEV.COL8) ? "0" : elcDEV.COL8;
                devCol.COL9 = String.IsNullOrEmpty(elcDEV.COL9) ? "0" : elcDEV.COL9;
                devCol.COL10 = String.IsNullOrEmpty(elcDEV.COL10) ? "0" : elcDEV.COL10;
                devCol.COL11 = String.IsNullOrEmpty(elcDEV.COL11) ? "0" : elcDEV.COL11;
                devCol.COL12 = String.IsNullOrEmpty(elcDEV.COL12) ? "0" : elcDEV.COL12;
                devCol.COL13 = String.IsNullOrEmpty(elcDEV.COL13) ? "0" : elcDEV.COL13;
                devCol.COL14 = String.IsNullOrEmpty(elcDEV.COL14) ? "0" : elcDEV.COL14;
                devCol.COL15 = String.IsNullOrEmpty(elcDEV.COL15) ? "0" : elcDEV.COL15;
                devCol.COL16 = String.IsNullOrEmpty(elcDEV.COL16) ? "0" : elcDEV.COL16;
                devCol.COL17 = String.IsNullOrEmpty(elcDEV.COL17) ? "0" : elcDEV.COL17;
                devCol.COL18 = String.IsNullOrEmpty(elcDEV.COL18) ? "0" : elcDEV.COL18;
                devCol.COL19 = String.IsNullOrEmpty(elcDEV.COL19) ? "0" : elcDEV.COL19;
                devCol.COL20 = String.IsNullOrEmpty(elcDEV.COL20) ? "0" : elcDEV.COL20;
            }
            else if (order == 1)
            {
                devCol.COL1 =  String.IsNullOrEmpty(elcDEV.COL21) ? "0" : elcDEV.COL21;
                devCol.COL2 =  String.IsNullOrEmpty(elcDEV.COL22) ? "0" : elcDEV.COL22;
                devCol.COL3 =  String.IsNullOrEmpty(elcDEV.COL23) ? "0" : elcDEV.COL23;
                devCol.COL4 = String.IsNullOrEmpty(elcDEV.COL24) ? "0" : elcDEV.COL24;
                devCol.COL5 =  String.IsNullOrEmpty(elcDEV.COL25) ? "0" : elcDEV.COL25;
                devCol.COL6 =  String.IsNullOrEmpty(elcDEV.COL26) ? "0" : elcDEV.COL26;
                devCol.COL7 =  String.IsNullOrEmpty(elcDEV.COL27) ? "0" : elcDEV.COL27;
                devCol.COL8 =  String.IsNullOrEmpty(elcDEV.COL28) ? "0" : elcDEV.COL28;
                devCol.COL9 =  String.IsNullOrEmpty(elcDEV.COL29) ? "0" : elcDEV.COL29;
                devCol.COL10 = String.IsNullOrEmpty(elcDEV.COL30) ? "0" : elcDEV.COL30;
                devCol.COL11 = String.IsNullOrEmpty(elcDEV.COL31) ? "0" : elcDEV.COL31;
                devCol.COL12 = String.IsNullOrEmpty(elcDEV.COL32) ? "0" : elcDEV.COL32;
                devCol.COL13 = String.IsNullOrEmpty(elcDEV.COL33) ? "0" : elcDEV.COL33;
                devCol.COL14 = String.IsNullOrEmpty(elcDEV.COL34) ? "0" : elcDEV.COL34;
                devCol.COL15 = String.IsNullOrEmpty(elcDEV.COL35) ? "0" : elcDEV.COL35;
                devCol.COL16 = String.IsNullOrEmpty(elcDEV.COL36) ? "0" : elcDEV.COL36;
                devCol.COL17 = String.IsNullOrEmpty(elcDEV.COL37) ? "0" : elcDEV.COL37;
                devCol.COL18 = String.IsNullOrEmpty(elcDEV.COL38) ? "0" : elcDEV.COL38;
                devCol.COL19 =  String.IsNullOrEmpty(elcDEV.COL39) ? "0" : elcDEV.COL39;
                devCol.COL20 = String.IsNullOrEmpty(elcDEV.COL40) ? "0" : elcDEV.COL40;
            }
            else if (order == 2)
            {
                devCol.COL1 =  String.IsNullOrEmpty(elcDEV.COL41) ? "0" : elcDEV.COL41;
                devCol.COL2 = String.IsNullOrEmpty(elcDEV.COL42) ? "0" : elcDEV.COL42;
                devCol.COL3 = String.IsNullOrEmpty(elcDEV.COL43) ? "0" : elcDEV.COL43;
                devCol.COL4 =  String.IsNullOrEmpty(elcDEV.COL44) ? "0" : elcDEV.COL44;
                devCol.COL5 =  String.IsNullOrEmpty(elcDEV.COL45) ? "0" : elcDEV.COL45;
                devCol.COL6 =String.IsNullOrEmpty(elcDEV.COL46) ? "0" : elcDEV.COL46;
                devCol.COL7 =  String.IsNullOrEmpty(elcDEV.COL47) ? "0" : elcDEV.COL47;
                devCol.COL8 =  String.IsNullOrEmpty(elcDEV.COL48) ? "0" : elcDEV.COL48;
                devCol.COL9 = String.IsNullOrEmpty(elcDEV.COL49) ? "0" : elcDEV.COL49;
                devCol.COL10 =  String.IsNullOrEmpty(elcDEV.COL50) ? "0" : elcDEV.COL50;
                devCol.COL11 = String.IsNullOrEmpty(elcDEV.COL51) ? "0" : elcDEV.COL51;
                devCol.COL12 = String.IsNullOrEmpty(elcDEV.COL52) ? "0" : elcDEV.COL52;
                devCol.COL13 =  String.IsNullOrEmpty(elcDEV.COL53) ? "0" : elcDEV.COL53;
                devCol.COL14 =  String.IsNullOrEmpty(elcDEV.COL54) ? "0" : elcDEV.COL54;
                devCol.COL15 = String.IsNullOrEmpty(elcDEV.COL55) ? "0" : elcDEV.COL55;
                devCol.COL16 =  String.IsNullOrEmpty(elcDEV.COL56) ? "0" : elcDEV.COL56;
                devCol.COL17 = String.IsNullOrEmpty(elcDEV.COL57) ? "0" : elcDEV.COL57;
                devCol.COL18 =String.IsNullOrEmpty(elcDEV.COL58) ? "0" : elcDEV.COL58;
                devCol.COL19 = String.IsNullOrEmpty(elcDEV.COL59) ? "0" : elcDEV.COL59;
                devCol.COL20 =  String.IsNullOrEmpty(elcDEV.COL60) ? "0" : elcDEV.COL60;
            }
            else if (order == 3)
            {
                devCol.COL1 = String.IsNullOrEmpty(elcDEV.COL61) ? "0" : elcDEV.COL61;
                devCol.COL2 = String.IsNullOrEmpty(elcDEV.COL62) ? "0" : elcDEV.COL62;
                devCol.COL3 =  String.IsNullOrEmpty(elcDEV.COL63) ? "0" : elcDEV.COL63;
                devCol.COL4 = String.IsNullOrEmpty(elcDEV.COL64) ? "0" : elcDEV.COL64;
                devCol.COL5 = String.IsNullOrEmpty(elcDEV.COL65) ? "0" : elcDEV.COL65;
                devCol.COL6 = String.IsNullOrEmpty(elcDEV.COL66) ? "0" : elcDEV.COL66;
                devCol.COL7 =  String.IsNullOrEmpty(elcDEV.COL67) ? "0" : elcDEV.COL67;
                devCol.COL8 = String.IsNullOrEmpty(elcDEV.COL68) ? "0" : elcDEV.COL68;
                devCol.COL9 = String.IsNullOrEmpty(elcDEV.COL69) ? "0" : elcDEV.COL69;
                devCol.COL10 = String.IsNullOrEmpty(elcDEV.COL70) ? "0" : elcDEV.COL70;
                devCol.COL11 = String.IsNullOrEmpty(elcDEV.COL71) ? "0" : elcDEV.COL71;
                devCol.COL12 =  String.IsNullOrEmpty(elcDEV.COL72) ? "0" : elcDEV.COL72;
                devCol.COL13 = String.IsNullOrEmpty(elcDEV.COL73) ? "0" : elcDEV.COL73;
                devCol.COL14 =  String.IsNullOrEmpty(elcDEV.COL74) ? "0" : elcDEV.COL74;
                devCol.COL15 =  String.IsNullOrEmpty(elcDEV.COL75) ? "0" : elcDEV.COL75;
                devCol.COL16 =  String.IsNullOrEmpty(elcDEV.COL76) ? "0" : elcDEV.COL76;
                devCol.COL17 =  String.IsNullOrEmpty(elcDEV.COL77) ? "0" : elcDEV.COL77;
                devCol.COL18 = String.IsNullOrEmpty(elcDEV.COL78) ? "0" : elcDEV.COL78;
                devCol.COL19 = String.IsNullOrEmpty(elcDEV.COL79) ? "0" : elcDEV.COL79;
                devCol.COL20 =  String.IsNullOrEmpty(elcDEV.COL80) ? "0" : elcDEV.COL80;
            }
            return devCol;
        }
        private string voltLineLoss(double voltLoss, double rateVolt, string type)
        {
            switch (type)
            {
                case "05":
                    if (rateVolt >= 220)
                    {
                        if (Math.Abs(voltLoss) > 0.03)
                        {
                            return "不合格";
                        }
                        else
                        {
                            return "合格";
                        }
                    }
                    else if (rateVolt >= 66 && rateVolt < 220)
                    {
                        if (Math.Abs(voltLoss) > 0.075)
                        {
                            return "不合格";
                        }
                        else
                        {
                            return "合格";
                        }
                    }
                    else if (rateVolt >= 35 && rateVolt < 66)
                    {
                        if (Math.Abs(voltLoss) > 0.05)
                        {
                            return "不合格";
                        }
                        else
                        {
                            return "合格";
                        }
                    }
                    else if (rateVolt < 35)
                    {
                        if (Math.Abs(voltLoss) > 0.1)
                        {
                            return "不合格";
                        }
                        else
                        {
                            return "合格";
                        }
                    }
                    else
                    {
                        if (Math.Abs(voltLoss) > 0.03)
                        {
                            return "不合格";
                        }
                        else
                        {
                            return "合格";
                        }
                    }
                    break;
                case "02":
                case "03":
                    if (rateVolt >= 220)
                    {
                        if (Math.Abs(voltLoss) > 0.02)
                        {
                            return "不合格";
                        }
                        else
                        {
                            return "合格";
                        }
                    }
                    else if (rateVolt >= 66 && rateVolt < 220)
                    {
                        if (Math.Abs(voltLoss) > 0.05)
                        {
                            return "不合格";
                        }
                        else
                        {
                            return "合格";
                        }
                    }
                    else if (rateVolt >= 35 && rateVolt < 66)
                    {
                        if (Math.Abs(voltLoss) > 0.045)
                        {
                            return "不合格";
                        }
                        else
                        {
                            return "合格";
                        }
                    }
                    else if (rateVolt < 35)
                    {
                        if (Math.Abs(voltLoss) > 0.04)
                        {
                            return "不合格";
                        }
                        else
                        {
                            return "合格";
                        }
                    }
                    else
                    {
                        if (Math.Abs(voltLoss) > 0.02)
                        {
                            return "不合格";
                        }
                        else
                        {
                            return "合格";
                        }
                    }
                    break;
                default:
                    return "不合格";
                    break;
            }
        }
        private string voltFlag(double voltExcursion, double rateVolt)
        {
            if (rateVolt >= 35)
            {
                if (Math.Abs(voltExcursion) > 0.05)
                {
                    return "不合格";
                }
                else
                {
                    return "合格";
                }

            }
            else if (rateVolt < 20 && rateVolt > 0.22)
            {
                if (Math.Abs(voltExcursion) > 0.07)
                {
                    return "不合格";
                }
                else
                {
                    return "合格";
                }
            }
            else if (rateVolt == 0.22)
            {
                if (voltExcursion > 0.075 || voltExcursion < -0.1)
                {
                    return "不合格";
                }
                else
                {
                    return "合格";
                }
            }
            else
            {
                if (Math.Abs(voltExcursion) > 0.05)
                {
                    return "不合格";
                }
                else
                {
                    return "合格";
                }
            }
        }
        public void VE(string projectSUID, float ratedCapacity)
        {
            int type = 2;
            frnReport wFrom = new frnReport();
            wFrom.Text = "电压质量评估";
            wFrom.Show();
            wFrom.ShowText += "正在收集信息\t" + System.DateTime.Now.ToString();
            try
            {
                LFC(projectSUID, type, ratedCapacity,wFrom);

                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "电压质量评估.xls"))
                {
                    //System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + "线损计算结果.xls");
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "电压质量评估.xls");
                    //OpenRead(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + ".xls");
                }


                wFrom.ShowText += "\r\n正在生成报表\t" + System.DateTime.Now.ToString();


                string output = null;
                output += ("全网母线(发电、负荷)电压质量评估 " + "\r\n" + "\r\n");
                output += ("单位：kA\\kV\\MW\\Mvar" + "\r\n" + "\r\n");
                output += ("计算日期：" + System.DateTime.Now.ToString() + "\r\n" + "\r\n");
                output += ("母线名" + "," + "电压偏移" + "," + "是否合格" + "\r\n");
                string strCon = ",PSPDEV WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'" + " AND PSPDEV.Type = '01'  order by PSPDEV.RateVolt,PSPDEV.Name";
                IList list = Services.BaseService.GetList("SelectPSP_ElcDeviceByCondition", strCon);
                double numFlag = 0, num = 0; ;
                foreach (PSP_ElcDevice elcDEV in list)
                {
                    PSPDEV dev = new PSPDEV();
                    dev.SUID = elcDEV.DeviceSUID;
                    dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", dev);
                    //double vTemp = Convert.ToDouble(GetColValue(elcDEV,type-1));
                    //double vTemp1 =  TLPSPVmin * dev.RateVolt;
                    //double vTemp2 =  TLPSPVmax * dev.RateVolt;
                    if (dev == null)
                    {
                        continue;
                    }
                    double voltExcursion = 0.0;
                    string flag = "";
                    voltExcursion = (Convert.ToDouble(GetColValue(elcDEV, type - 1).COL2) - dev.RateVolt) / dev.RateVolt;
                    flag = voltFlag(voltExcursion, dev.RateVolt);
                    if (flag == "合格")
                    {
                        numFlag++;
                    }
                    num++;
                    output += dev.Name + "," + voltExcursion.ToString() + "," + flag + "\r\n";
                }
                if (num == 0)
                {
                    num = 1;
                }
                output += "电压合格率" + "," + (double)(numFlag / num) + "\r\n";
                try
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result.csv"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\result.csv");
                    }
                }
                catch (System.Exception ex2)
                {
                    MessageBox.Show("请关闭相关Excel后再查看结果！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                FileStream op = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result.csv"), FileMode.OpenOrCreate);
                StreamWriter str1 = new StreamWriter(op, Encoding.Default);
                str1.Write(output);
                str1.Close();

                output = null;

                output += ("全网交流线电压质量评估" + "\r\n" + "\r\n");
                output += ("单位：kA\\kV\\MW\\Mvar" + "\r\n" + "\r\n");
                output += ("计算日期：" + System.DateTime.Now.ToString() + "\r\n" + "\r\n");
                output += ("支路名称" + "," + "电压损失" + "," + "是否合格" + "\r\n");

                string strCon1 = ",PSPDEV WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'" + " AND PSPDEV.Type = '05'  order by PSPDEV.RateVolt,PSPDEV.Name";
                IList list1 = Services.BaseService.GetList("SelectPSP_ElcDeviceByCondition", strCon1);

                foreach (PSP_ElcDevice elcDEV in list1)
                {
                    PSPDEV dev = new PSPDEV();
                    dev.SUID = elcDEV.DeviceSUID;
                    dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", dev);
                    //double vTemp = Convert.ToDouble(GetColValue(elcDEV,type-1));
                    //double vTemp1 =  TLPSPVmin * dev.RateVolt;
                    //double vTemp2 =  TLPSPVmax * dev.RateVolt;
                    double voltLoss = 0.0;
                    string lineFlag = "";
                    voltLoss = (Convert.ToDouble(GetColValue(elcDEV, type - 1).COL12) - Convert.ToDouble(GetColValue(elcDEV, type - 1).COL13)) / dev.RateVolt;
                    lineFlag = voltLineLoss(voltLoss, dev.RateVolt, dev.Type);
                    output += dev.Name + "," + voltLoss.ToString() + "," + lineFlag + "\r\n";
                }
                try
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result1.csv"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                    }
                }
                catch (System.Exception ex3)
                {
                    MessageBox.Show("请关闭相关Excel后再查看结果！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                op = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result1.csv"), FileMode.OpenOrCreate);
                str1 = new StreamWriter(op, Encoding.Default);
                str1.Write(output);
                str1.Close();
                Excel.Application ex;
                Excel.Worksheet xSheet;
                Excel.Application result1;
                Excel.Worksheet tempSheet;
                Excel.Worksheet newWorksheet;
                ex = new Excel.Application();
                ex.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result.csv");
                xSheet = (Excel.Worksheet)ex.Worksheets[1];
                ex.Worksheets.Add(System.Reflection.Missing.Value, xSheet, 1, System.Reflection.Missing.Value);

                result1 = new Excel.Application();
                result1.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                tempSheet = (Excel.Worksheet)result1.Worksheets.get_Item(1);
                newWorksheet = (Excel.Worksheet)ex.Worksheets.get_Item(2);
                newWorksheet.Name = "支路电压质量评估";
                xSheet.Name = "母线电压质量评估";
                ex.Visible = true;

                tempSheet.Cells.Select();
                tempSheet.Cells.Copy(System.Reflection.Missing.Value);
                newWorksheet.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);

                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 3]).MergeCells = true;
                xSheet.get_Range(xSheet.Cells[3, 1], xSheet.Cells[3, 2]).MergeCells = true;
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Size = 16;
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Name = "黑体";
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xSheet.get_Range(xSheet.Cells[7, 1], xSheet.Cells[7, 3]).Interior.ColorIndex = 45;
                xSheet.get_Range(xSheet.Cells[8, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 1]).Interior.ColorIndex = 6;

                xSheet.get_Range(xSheet.Cells[6, 2], xSheet.Cells[xSheet.UsedRange.Rows.Count, 3]).NumberFormat = "0.0000_ ";
                xSheet.get_Range(xSheet.Cells[3, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 3]).Font.Name = "楷体_GB2312";
                xSheet.get_Range(xSheet.Cells[6, 2], xSheet.Cells[xSheet.UsedRange.Rows.Count, 2]).NumberFormatLocal = "0.0000%";


                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 3]).MergeCells = true;
                newWorksheet.get_Range(newWorksheet.Cells[3, 1], newWorksheet.Cells[3, 2]).MergeCells = true;
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Size = 16;
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Name = "黑体";
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                newWorksheet.get_Range(newWorksheet.Cells[7, 1], newWorksheet.Cells[7, 3]).Interior.ColorIndex = 45;
                newWorksheet.get_Range(newWorksheet.Cells[8, 1], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 1]).Interior.ColorIndex = 6;

                newWorksheet.get_Range(newWorksheet.Cells[6, 2], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 3]).NumberFormat = "0.0000_ ";
                newWorksheet.get_Range(newWorksheet.Cells[3, 1], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 3]).Font.Name = "楷体_GB2312";
                newWorksheet.get_Range(newWorksheet.Cells[6, 2], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 2]).NumberFormatLocal = "0.0000%";
                //op = new FileStream((System.Windows.Forms.Application.StartupPath + "\\fck.excel"), FileMode.OpenOrCreate);
                //str1 = new StreamWriter(op, Encoding.Default);
                newWorksheet.Columns.AutoFit();
                newWorksheet.Rows.AutoFit();
                xSheet.Rows.AutoFit();
                xSheet.Columns.AutoFit();

                newWorksheet.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "电压质量评估.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);


                ex.DisplayAlerts = false;
                System.Windows.Forms.Clipboard.Clear();
                result1.Workbooks.Close();
                result1.Quit();
            }
            catch (System.Exception ex)
            {
                wFrom.ShowText += "\r\n报表生成失败\t" + System.DateTime.Now.ToString();
            }
            wFrom.ShowText += "\r\n电压质量评估结束\t" + System.DateTime.Now.ToString();
        }
       public struct lossyw
        {
          private double _tempyg;
          private double _tempwg;
           public double tempyg
           {
               get
               {
                   return _tempyg;
               }
               set
               {
                   _tempyg = value;
               }
           }
           public double tempwg
           {
               get
               {
                   return _tempwg;
               }
               set
               {
                   _tempwg = value;
               }
           }
        }
        private Dictionary<double, lossyw> dydir = new Dictionary<double, lossyw>();
        private void getyg(double referecevolt,double tempyg,double tempwg)
        {
           if (dydir.ContainsKey(referecevolt))
           {
               lossyw ly = dydir[referecevolt];
               ly.tempyg += tempyg; 
               ly.tempwg += tempwg;
               dydir.Remove(referecevolt);
               dydir.Add(referecevolt, ly);
               
           }
            else
           {
               lossyw ly=new lossyw();
               ly.tempwg=tempwg;
               ly.tempyg=tempyg;
               dydir.Add(referecevolt, ly);
           }
        }
        public void PLE(string projectSUID, int type, float ratedCapacity)
        {
            dydir.Clear();
            string strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'";
            string strCon2 = null;
            string strCon = null;
            frnReport wFrom = new frnReport();
            wFrom.Text = "线损计算";
            wFrom.Show();
            wFrom.ShowText += "正在收集信息\t" + System.DateTime.Now.ToString();
            try
            {
                LFC(projectSUID, type, ratedCapacity,wFrom);
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "线损计算结果.xls"))
                {
                    //System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + "线损计算结果.xls");
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "线损计算结果.xls");
                    //OpenRead(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + ".xls");
                }
                //else
                //{

                double yinzi = 0, capability = 0, volt = 0, standvolt = 0, current = 0, Rad_to_Deg = 57.29577951;
                //PSPDEV benchmark = new PSPDEV();
                //benchmark.Type = "power";
                //benchmark.ProjectID = Itop.Client.MIS.ProgUID;
                //IList list3 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", benchmark);
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
                //    }
                //    volt = standvolt;
                //    current = capability / (Math.Sqrt(3) * volt);

                //}
                //capability = 100;

                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH2.txt"))
                {
                }
                else
                {
                    wFrom.ShowText += "\r\n计算失败\t" + System.DateTime.Now.ToString();
                    return;
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH2.txt"))
                {
                }
                else
                {
                    wFrom.ShowText += "\r\n计算失败\t" + System.DateTime.Now.ToString();
                    return;
                }
                FileStream dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH2.txt", FileMode.Open);
                StreamReader readLine = new StreamReader(dh, Encoding.Default);
                char[] charSplit = new char[] { ' ' };
                string strLine = readLine.ReadLine();
                double temp1 = 0;
                double temp2 = 0;
                string output = null;
                string[] array1 = strLine.Split(charSplit);
                wFrom.ShowText += "\r\n正在形成报表\t" + System.DateTime.Now.ToString();
                string strCon13 = ",PSPDEV WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'" + " AND PSPDEV.Type = '01'  order by PSPDEV.RateVolt,PSPDEV.Name";
                IList list = Services.BaseService.GetList("SelectPSP_ElcDeviceByCondition", strCon13);
                double tempAD = 0;
                //int count = 0;
                foreach (PSP_ElcDevice elcDEV in list)
                {
                  
                    PSPDEV dev = new PSPDEV();
                    dev.SUID = elcDEV.DeviceSUID;
                    dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", dev);

                    string strCon3 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "' AND Type = '04' AND IName = '" + dev.Name + "'";
                    IList listFDJ = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon3);
                    string strCon4 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "' AND Type = '12' AND IName = '" + dev.Name + "'";
                    IList listFH = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon4);
                    foreach (PSPDEV devFDJ in listFDJ)
                    {
                        if (devFDJ.UnitFlag == "0")
                        {
                            tempAD += devFDJ.OutP;
                            //outQ += devFDJ.OutQ;
                        }
                        else
                        {
                            tempAD += devFDJ.OutP / ratedCapacity;
                            //outQ += devFDJ.OutQ / ratedCapacity;
                        }
                    }
                    foreach (PSPDEV devFH in listFH)
                    {
                        if (devFH.UnitFlag == "0")
                        {
                            if(devFH.InPutP<0)
                            {
                                tempAD += Math.Abs(devFH.InPutP);
                            }              
                        }
                        else
                        {
                            if (devFH.InPutP < 0)
                            {
                                tempAD += Math.Abs(devFH.InPutP / ratedCapacity);
                            }                     
                        }
                    }
                    if (dev.OutP>0)
                    {
                        if (dev.UnitFlag == "0")
                        {
                            if (dev.OutP>0)
                            {
                                tempAD += dev.OutP;
                            }                            
                            //outQ += devFDJ.OutQ;
                        }
                        else
                        {
                            if (dev.OutP>0)
                            {
                                tempAD += dev.OutP / ratedCapacity;
                            }                            
                            //outQ += devFDJ.OutQ / ratedCapacity;
                        }
                    } 
                    else if (dev.InPutP<0)
                    {
                        if (dev.UnitFlag == "0")
                        {
                            if (dev.InPutP < 0)
                            {
                                tempAD += Math.Abs(dev.InPutP);
                            }
                        }
                        else
                        {
                            if (dev.InPutP < 0)
                            {
                                tempAD += Math.Abs(dev.InPutP / ratedCapacity);
                            }
                        }
                    }       
                    if (Convert.ToDouble(GetColValue(elcDEV, 1).COL4) < 0 && dev.NodeType == "0")
                    {
                        tempAD += Convert.ToDouble(GetColValue(elcDEV, 1).COL4)/ratedCapacity;
                    }
                }
                if (tempAD == 0)
                {
                    tempAD = 1;
                }
                output += ("全网线损结果报表" + "\r\n" + "\r\n");
                output += ("单位：MW" + "\r\n" + "\r\n");
                output += ("计算日期：" + System.DateTime.Now.ToString() + "\r\n" + "\r\n");
                output += ("支路名称" + "," + "支路类型" + "," + "导线型号" + "," + "导线长度" + "," + "有功损耗" + "," + "无功损耗" + "," + "线损率" + "\r\n");
               
                while (strLine != null && strLine != "")
                {
                    array1 = strLine.Split(charSplit);
                    //count++;

                    string[] dev = new string[20];
                    dev.Initialize();
                    int i = 0;


                    foreach (string str in array1)
                    {
                        if (str != "")
                        {
                            if (i == 0)
                            {
                                dev[i++] = str.ToString();
                            }
                            else
                            {
                                if (!str.Contains("NAN") && !str.Contains("IND"))//!= "NaN"
                                {
                                    dev[i++] = Convert.ToDouble(str).ToString();
                                }
                                else
                                {
                                    //dev[i++] = str;
                                    dev[i++] = "0";
                                }

                            }
                        }

                    }
                    strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                    strCon = strCon1 + strCon2;
                    PSPDEV CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);

                    if (CR != null)
                    {
                        //if (Math.Abs(Convert.ToDouble(dev[5]))>Math.Abs(Convert.ToDouble(dev[3])))
                        //{
                        //    dev[3] = dev[7];
                        //}
                        if (Math.Abs(Convert.ToDouble(dev[3]))==0)
                        {
                            output += CR.Name + "," + "普通线路" + "," + CR.LineType + "," + CR.LineLength + "," + (Convert.ToDouble(dev[5]) * 100 + Convert.ToDouble(dev[9]) * CR.ReferenceVolt * CR.ReferenceVolt * 100 / 1000000).ToString() + "," + (Convert.ToDouble(dev[6]) * 100 + Convert.ToDouble(dev[10]) * CR.ReferenceVolt * CR.ReferenceVolt * 100 / 1000000).ToString() + "," + "0" + "\r\n";
                        }
                        else
                        {
                            output += CR.Name + "," + "普通线路" + "," + CR.LineType + "," + CR.LineLength + "," + (Convert.ToDouble(dev[5]) * 100 + Convert.ToDouble(dev[9]) * CR.ReferenceVolt * CR.ReferenceVolt * 100 / 1000000).ToString() + "," + (Convert.ToDouble(dev[6]) * 100 + Convert.ToDouble(dev[10]) * CR.ReferenceVolt * CR.ReferenceVolt * 100 / 1000000).ToString() + "," + Math.Abs(((Convert.ToDouble(dev[5]) * 100 + (Convert.ToDouble(dev[9]) * CR.ReferenceVolt * CR.ReferenceVolt * 100) / 1000000) / (Convert.ToDouble(dev[3]) * 100))).ToString() + "\r\n";
                        }
                        
                        temp1 += Convert.ToDouble(dev[5]) * 100 + Convert.ToDouble(dev[9]) * CR.ReferenceVolt * CR.ReferenceVolt * 100 / 1000000;
                        temp2 += Convert.ToDouble(dev[6]) * 100 + Convert.ToDouble(dev[10]) * CR.ReferenceVolt * CR.ReferenceVolt * 100 / 1000000;
                        getyg(CR.ReferenceVolt, Convert.ToDouble(dev[5]) * 100 + Convert.ToDouble(dev[9]) * CR.ReferenceVolt * CR.ReferenceVolt * 100 / 1000000, Convert.ToDouble(dev[6]) * 100 + Convert.ToDouble(dev[10]) * CR.ReferenceVolt * CR.ReferenceVolt * 100 / 1000000);
                    }
                    else
                    {
                        strCon2 = " AND Type= '02' AND Name = '" + array1[0] + "'";
                        strCon = strCon1 + strCon2;
                        CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (CR == null)
                        {
                            strCon2 = " AND Type= '03' AND Name = '" + array1[0] + "'";
                            strCon = strCon1 + strCon2;
                            CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        }
                        if (CR != null)
                        {
                            if (Math.Abs(Convert.ToDouble(dev[5])) > Math.Abs(Convert.ToDouble(dev[3])))
                            {
                                dev[3] = dev[7];
                            }
                            output += CR.Name + "," + "变压器支路" + "," + CR.LineType + "," + CR.LineLength + "," + (Convert.ToDouble(dev[5]) * 100 + Convert.ToDouble(dev[9]) * CR.ReferenceVolt * CR.ReferenceVolt * 100 / 1000000).ToString() + "," + (Convert.ToDouble(dev[6]) * 100 + Convert.ToDouble(dev[10]) * CR.ReferenceVolt * CR.ReferenceVolt * 100 / 1000000).ToString() + "," + Math.Abs(((Convert.ToDouble(dev[5]) * 100 + Convert.ToDouble(dev[9]) * CR.ReferenceVolt * CR.ReferenceVolt * 100 / 1000000) / (Convert.ToDouble(dev[3]) * 100))).ToString() + "\r\n";
                            temp1 += Convert.ToDouble(dev[5]) * 100 + Convert.ToDouble(dev[9]) * CR.ReferenceVolt * CR.ReferenceVolt * 100 / 1000000;
                            temp2 += Convert.ToDouble(dev[6]) * 100 + Convert.ToDouble(dev[10]) * CR.ReferenceVolt * CR.ReferenceVolt * 100 / 1000000;
                            getyg(CR.ReferenceVolt, Convert.ToDouble(dev[5]) * 100 + Convert.ToDouble(dev[9]) * CR.ReferenceVolt * CR.ReferenceVolt * 100 / 1000000, Convert.ToDouble(dev[6]) * 100 + Convert.ToDouble(dev[10]) * CR.ReferenceVolt * CR.ReferenceVolt * 100 / 1000000);
                        }
                    }

                    strLine = readLine.ReadLine();
                }
                readLine.Close();


                output += ("总损耗" + "," + "线路 " + "," + "" + "," + "" + "," + temp1 + "," + temp2 + "," + temp1 / (tempAD * ratedCapacity)+ "\r\n");
                if (dydir.Keys.Count>1)
                {
                    foreach (KeyValuePair<double, lossyw> keyvalue in dydir)
                    {
                        output += ("电压等级为" + keyvalue.Key.ToString() + "," + "线路" + "," + "" + "," + "" + "," + keyvalue.Value.tempyg + "," + keyvalue.Value.tempwg + "," + keyvalue.Value.tempyg / (tempAD * ratedCapacity) + "\r\n");
                    }
                 
                    
                }
                try
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result1.csv"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                    }
                }
                catch (System.Exception ex10)
                {
                    MessageBox.Show("请关闭相关Excel后再查看结果！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                FileStream op = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result1.csv"), FileMode.OpenOrCreate);
                StreamWriter str1 = new StreamWriter(op, Encoding.Default);
                str1.Write(output);
                str1.Close();


                Excel.Application ex = new Excel.Application();
                ex.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                Excel.Worksheet xSheet = (Excel.Worksheet)ex.Worksheets[1];
                //ex.Worksheets.Add(System.Reflection.Missing.Value, xSheet, 1, System.Reflection.Missing.Value);

                //result1 = new Excel.Application();
                //result1.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                //tempSheet = (Excel.Worksheet)result1.Worksheets.get_Item(1);
                //newWorksheet = (Excel.Worksheet)ex.Worksheets.get_Item(1);
                //newWorksheet.Name = "线损结果";
                xSheet.Name = "线损结果";
                ex.Visible = true;

                //tempSheet.Cells.Select();
                //tempSheet.Cells.Copy(System.Reflection.Missing.Value);
                //newWorksheet.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);

                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 7]).MergeCells = true;
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Size = 20;
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Name = "黑体";
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xSheet.get_Range(xSheet.Cells[7, 1], xSheet.Cells[7, 7]).Interior.ColorIndex = 45;
                xSheet.get_Range(xSheet.Cells[8, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 1]).Interior.ColorIndex = 6;
                //xSheet.get_Range(xSheet.Cells[6, 2], xSheet.Cells[xSheet.UsedRange.Rows.Count, 2]).ColumnWidth = 12;
                xSheet.get_Range(xSheet.Cells[8, 2], xSheet.Cells[xSheet.UsedRange.Rows.Count, 2]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xSheet.get_Range(xSheet.Cells[8, 2], xSheet.Cells[xSheet.UsedRange.Rows.Count, 7]).NumberFormat = "0.0000_ ";
                xSheet.get_Range(xSheet.Cells[3, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 7]).Font.Name = "楷体_GB2312";
                xSheet.get_Range(xSheet.Cells[8, 7], xSheet.Cells[xSheet.UsedRange.Rows.Count, 7]).NumberFormatLocal = "0.0000%";
                //newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 9]).MergeCells = true;
                //newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Size = 20;
                //newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Name = "黑体";
                //newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //newWorksheet.get_Range(newWorksheet.Cells[5, 1], newWorksheet.Cells[5, 8]).Interior.ColorIndex = 45;
                //newWorksheet.get_Range(newWorksheet.Cells[6, 1], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 1]).Interior.ColorIndex = 6;
                //newWorksheet.get_Range(newWorksheet.Cells[6, 2], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 9]).NumberFormat = "0.0000_ ";
                //newWorksheet.get_Range(newWorksheet.Cells[3, 1], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 9]).Font.Name = "楷体_GB2312";

                //op = new FileStream((System.Windows.Forms.Application.StartupPath + "\\fck.excel"), FileMode.OpenOrCreate);
                //str1 = new StreamWriter(op, Encoding.Default);
                xSheet.Rows.AutoFit();
                xSheet.Columns.AutoFit();

                //result1.Save(System.Windows.Forms.Application.StartupPath + "\\fck.xls");

                //newWorksheet.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + fn + "线损计算结果.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);

                //str1.Write();
                //op.Close();

                xSheet.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "线损计算结果.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);

                System.Windows.Forms.Clipboard.Clear();
                //result1.Workbooks.Close();
                //result1.Quit();
                //ex.Workbooks.Close();
                //ex.Quit();                
            }
            //}
            catch (System.Exception e1)
            {
                wFrom.ShowText += "\r\n报表生成失败\t" + System.DateTime.Now.ToString();
                MessageBox.Show("请进行潮流计算后再查看结果！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            wFrom.ShowText += "\r\n线损计算结束\t" + System.DateTime.Now.ToString();
        }
        public void DFSER(IList branchlist, IList buslist, string projectSUID, float ratedCapacity, int type)
        {
            DFS(branchlist, buslist, projectSUID, ratedCapacity);
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "配网潮流计算结果.xls"))
            {
                //System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + "线损计算结果.xls");
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "配网潮流计算结果.xls");
                //OpenRead(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + ".xls");
            }
            string output = null;
            output += ("全网母线(发电、负荷)结果报表 " + "\r\n" + "\r\n");
            output += ("单位：kA\\kV\\MW\\Mvar" + "\r\n" + "\r\n");
            output += ("计算日期：" + System.DateTime.Now.ToString() + "\r\n" + "\r\n");
            output += ("母线名" + "," + "电压幅值" + "," + "电压相角" + "," + "有功发电" + "," + "无功发电" + "," + "有功负荷" + "," + "无功负荷" + "," + "越限标志" + "," + "过载标志" + "\r\n");
            string strCon = ",PSPDEV WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'" + " AND PSPDEV.Type = '01'";
            IList list = Services.BaseService.GetList("SelectPSP_ElcDeviceByCondition", strCon);
            foreach (PSP_ElcDevice elcDEV in list)
            {
                PSPDEV dev = new PSPDEV();
                dev.SUID = elcDEV.DeviceSUID;
                dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", dev);
                //double vTemp = Convert.ToDouble(GetColValue(elcDEV,type-1));
                //double vTemp1 =  TLPSPVmin * dev.RateVolt;
                //double vTemp2 =  TLPSPVmax * dev.RateVolt;
                string voltF = "否";
                string pF = "否";
                if (Convert.ToDouble(GetColValue(elcDEV, type - 1).COL2) < dev.iV || Convert.ToDouble(GetColValue(elcDEV, type - 1).COL2) > dev.jV)
                {
                    voltF = "是";
                }
                if (Convert.ToDouble(GetColValue(elcDEV, type - 1).COL2) > (double)dev.Burthen)
                {
                    pF = "是";
                }
                if (Convert.ToDouble(GetColValue(elcDEV, type - 1).COL2) < 0)
                {
                    output += dev.Name + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL2).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL3).ToString() + "," + "0" + "," + "0" + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL4).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL5).ToString() + "," + voltF + "," + pF + "\r\n";
                }
                else
                {
                    output += dev.Name + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL2).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL3).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL4).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL5).ToString() + "," + "0" + "," + "0" + "," + voltF + "," + pF + "\r\n";
                }
            }

            try
            {
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result.csv"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\result.csv");
                }
            }
            catch (System.Exception ex2)
            {
                MessageBox.Show("请关闭相关Excel后再查看结果！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            FileStream op = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result.csv"), FileMode.OpenOrCreate);
            StreamWriter str1 = new StreamWriter(op, Encoding.Default);
            str1.Write(output);
            str1.Close();

            output = null;

            output += ("全网交流线结果报表" + "\r\n" + "\r\n");
            output += ("单位：kA\\kV\\MW\\Mvar" + "\r\n" + "\r\n");
            output += ("计算日期：" + System.DateTime.Now.ToString() + "\r\n" + "\r\n");
            output += ("支路名称" + "," + "支路有功" + "," + "支路无功" + "," + "有功损耗" + "," + "无功损耗" + "," + "电流幅值" + "," + "电流相角" + "," + "越限标志" + "," + "\r\n");

            string strCon1 = ",PSPDEV WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'" + " AND PSPDEV.Type = '05'";
            IList list1 = Services.BaseService.GetList("SelectPSP_ElcDeviceByCondition", strCon1);

            foreach (PSP_ElcDevice elcDEV in list1)
            {
                PSPDEV dev = new PSPDEV();
                dev.SUID = elcDEV.DeviceSUID;
                dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", dev);
                string lineF = "否";
                if (Convert.ToDouble(GetColValue(elcDEV, type - 1).COL14) > (double)dev.Burthen)
                {
                    lineF = "是";
                }
                //double vTemp = Convert.ToDouble(GetColValue(elcDEV,type-1));
                //double vTemp1 =  TLPSPVmin * dev.RateVolt;
                //double vTemp2 =  TLPSPVmax * dev.RateVolt;
                output += dev.Name.ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL4).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL5).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL6).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL7).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL14).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL15).ToString() + "," + lineF + "," + "\r\n";
            }
            try
            {
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result1.csv"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                }
            }
            catch (System.Exception ex3)
            {
                MessageBox.Show("请关闭相关Excel后再查看结果！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            op = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result1.csv"), FileMode.OpenOrCreate);
            str1 = new StreamWriter(op, Encoding.Default);
            str1.Write(output);
            str1.Close();
            Excel.Application ex;
            Excel.Worksheet xSheet;
            Excel.Application result1;
            Excel.Worksheet tempSheet;
            Excel.Worksheet newWorksheet;
            ex = new Excel.Application();
            ex.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result.csv");
            xSheet = (Excel.Worksheet)ex.Worksheets[1];
            ex.Worksheets.Add(System.Reflection.Missing.Value, xSheet, 1, System.Reflection.Missing.Value);

            result1 = new Excel.Application();
            result1.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
            tempSheet = (Excel.Worksheet)result1.Worksheets.get_Item(1);
            newWorksheet = (Excel.Worksheet)ex.Worksheets.get_Item(2);
            newWorksheet.Name = "线路电流";
            xSheet.Name = "母线潮流";
            ex.Visible = true;

            tempSheet.Cells.Select();
            tempSheet.Cells.Copy(System.Reflection.Missing.Value);
            newWorksheet.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);

            xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 9]).MergeCells = true;
            xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Size = 20;
            xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Name = "黑体";
            xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            xSheet.get_Range(xSheet.Cells[7, 1], xSheet.Cells[7, 9]).Interior.ColorIndex = 45;
            xSheet.get_Range(xSheet.Cells[8, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 1]).Interior.ColorIndex = 6;
            xSheet.get_Range(xSheet.Cells[8, 2], xSheet.Cells[xSheet.UsedRange.Rows.Count, 9]).NumberFormat = "0.0000_ ";
            xSheet.get_Range(xSheet.Cells[3, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 9]).Font.Name = "楷体_GB2312";
            //xSheet.get_Range(xSheet.Cells[3, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 1]).NumberFormatLocal = "@";

            newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 9]).MergeCells = true;
            newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Size = 20;
            newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Name = "黑体";
            newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            newWorksheet.get_Range(newWorksheet.Cells[7, 1], newWorksheet.Cells[7, 8]).Interior.ColorIndex = 45;
            newWorksheet.get_Range(newWorksheet.Cells[8, 1], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 1]).Interior.ColorIndex = 6;
            // newWorksheet.get_Range(newWorksheet.Cells[6, 1], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 1]).NumberFormatLocal = "@";
            newWorksheet.get_Range(newWorksheet.Cells[8, 2], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 9]).NumberFormat = "0.0000_ ";
            newWorksheet.get_Range(newWorksheet.Cells[3, 1], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 9]).Font.Name = "楷体_GB2312";

            //op = new FileStream((System.Windows.Forms.Application.StartupPath + "\\fck.excel"), FileMode.OpenOrCreate);
            //str1 = new StreamWriter(op, Encoding.Default);
            xSheet.Rows.AutoFit();
            xSheet.Columns.AutoFit();
            newWorksheet.Rows.AutoFit();
            newWorksheet.Columns.AutoFit();

            //result1.Save(System.Windows.Forms.Application.StartupPath + "\\fck.xls");


            newWorksheet.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "配网潮流计算结果.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);

            System.Windows.Forms.Clipboard.Clear();
            result1.Workbooks.Close();
            result1.Quit();

        }

        public bool LFCER(string projectSUID, int type, float ratedCapacity)
        {
            frnReport wForm = new frnReport();
            switch(type)
            {
                case 1:
                    wForm.Text = "牛拉法潮流计算";
                    break;
                case 2:
                    wForm.Text = "PQ分解法潮流计算";
                    break;
                case 3:
                    wForm.Text = "高斯—赛德尔法计算潮流计算";
                    break;
                case 4:
                    wForm.Text = "最优乘子法潮流计算";
                    break;
                default:
                    wForm.Text = "潮流计算";
                    break;
            }
            wForm.Show();
            wForm.ShowText += "选择结果输出方式\t" + System.DateTime.Now.ToString();
            bool flag = false;
            int iResult = 0;
            frmSelectSub frmSub = new frmSelectSub();
            frmSub.ShowDialog();
            if (frmSub.DialogResult==DialogResult.OK)
            {
                iResult = frmSub.SelectSub;
            }
            else if (frmSub.DialogResult== DialogResult.Cancel)
            {
                wForm.Close();
                return false;
            }
            System.Windows.Forms.Clipboard.Clear(); 
            flag = LFC(projectSUID, type, ratedCapacity,wForm);
            if (flag == false)
            {
                wForm.ShowText += "\r\n潮流计算结果不收敛，请检查数据\t" + System.DateTime.Now.ToString();
                wForm.ShowText += "\r\n计算失败\t" + System.DateTime.Now.ToString();
                return false;
            }
            try
            {
                if (type == 1)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "牛拉法计算结果.xls"))
                    {
                        //System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + "线损计算结果.xls");
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "牛拉法计算结果.xls");
                        //OpenRead(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + ".xls");
                    }
                }
                else if (type == 2)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "PQ分解法计算结果.xls"))
                    {
                        //System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + "线损计算结果.xls");
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "PQ分解法计算结果.xls");
                        //OpenRead(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + ".xls");
                    }

                }
                else if (type == 3)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "高斯—赛德尔法计算结果.xls"))
                    {
                        //System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + "线损计算结果.xls");
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "高斯—赛德尔法计算结果.xls");
                        //OpenRead(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + ".xls");
                    }

                }
                else if (type == 4)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "最优乘子法计算结果.xls"))
                    {
                        //System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + "线损计算结果.xls");
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "最优乘子法计算结果.xls");
                        //OpenRead(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + ".xls");
                    }
                }
                else if (type == 5)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "配网潮流计算结果.xls"))
                    {
                        //System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + "线损计算结果.xls");
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "配网潮流计算结果.xls");
                        //OpenRead(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + ".xls");
                    }
                }

                wForm.ShowText += "\r\n正在形成报表\t" + System.DateTime.Now.ToString();

                string output = null;
                output += ("全网母线(发电、负荷)结果报表 " + "\r\n" + "\r\n");
                output += ("单位：kA\\kV\\MW\\Mvar" + "\r\n" + "\r\n");
                output += ("计算日期：" + System.DateTime.Now.ToString() + "\r\n" + "\r\n");
                output += ("母线名" + "," + "电压幅值" + "," + "电压相角" + "," + "有功发电" + "," + "无功发电" + "," + "有功负荷" + "," + "无功负荷" + "," + "越限标志" + "," + "过载标志" + "\r\n");
                string strCon = ",PSPDEV WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'" + " AND PSPDEV.Type = '01' order by PSPDEV.RateVolt,PSPDEV.Name";
                IList list = Services.BaseService.GetList("SelectPSP_ElcDeviceByCondition", strCon);
                foreach (PSP_ElcDevice elcDEV in list)
                {
                    PSPDEV dev = new PSPDEV(); 
                    dev.SUID = elcDEV.DeviceSUID;
                    dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", dev);
                    //double vTemp = Convert.ToDouble(GetColValue(elcDEV,type-1));
                    //double vTemp1 =  TLPSPVmin * dev.RateVolt;
                    //double vTemp2 =  TLPSPVmax * dev.RateVolt;
                    string voltF = "否";
                    string pF = "否";
                    if (dev.KSwitchStatus=="1")
                    {
                        continue;
                    }
                    if (Convert.ToDouble(GetColValue(elcDEV, type - 1).COL2) < dev.iV || Convert.ToDouble(GetColValue(elcDEV, type - 1).COL2) > dev.jV)
                    {
                        voltF = "是";
                    }
                    double tempi = Convert.ToDouble(GetColValue(elcDEV, type - 1).COL4);
                    double tempj = Convert.ToDouble(GetColValue(elcDEV, type - 1).COL5);
                    double temptotal = Math.Sqrt(tempi * tempi + tempj * tempj);
                    if (temptotal > (double)dev.Burthen)
                    {
                        pF = "是";
                    }

                    if (Convert.ToDouble(GetColValue(elcDEV, type - 1).COL4) > 0)
                    {
                        if (iResult == 0)
                        {
                            output += dev.Name + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL2).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL3).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL4).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL5).ToString() + "," + "0" + "," + "0" + "," + voltF + "," + pF + "\r\n";
                        }
                        else
                        {
                            object obj = DeviceHelper.GetDevice<PSP_Substation_Info>(dev.SvgUID);

                            if (obj != null)
                            {
                                output += ((PSP_Substation_Info)obj).Title + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL2).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL3).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL4).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL5).ToString() + "," + "0" + "," + "0" + "," + voltF + "," + pF + "\r\n";                              
                       
                            } 
                            else
                            {
                                obj = DeviceHelper.GetDevice<PSP_PowerSubstation_Info>(dev.SvgUID);
                                if (obj != null)
                                {
                                    output += ((PSP_PowerSubstation_Info)obj).Title + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL2).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL3).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL4).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL5).ToString() + "," + "0" + "," + "0" + "," + voltF + "," + pF + "\r\n";                              
                              
                                }
                            }

                        }                        
                    }
                    else
                    {
                        if (iResult == 0)
                        {
                            output += dev.Name + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL2).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL3).ToString() + "," + "0" + "," + "0" + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL4).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL5).ToString() + "," + voltF + "," + pF + "\r\n";
                        }
                        else
                        {
                            object obj = DeviceHelper.GetDevice<PSP_Substation_Info>(dev.SvgUID);

                            if (obj != null)
                            {
                                output += ((PSP_Substation_Info)obj).Title + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL2).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL3).ToString() + "," + "0" + "," + "0" + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL4).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL5).ToString() + "," + voltF + "," + pF + "\r\n";                                

                            }
                            else
                            {
                                obj = DeviceHelper.GetDevice<PSP_PowerSubstation_Info>(dev.SvgUID);
                                if (obj != null)
                                {
                                    output += ((PSP_PowerSubstation_Info)obj).Title + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL2).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL3).ToString() + "," + "0" + "," + "0" + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL4).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL5).ToString() + "," + voltF + "," + pF + "\r\n";                               
                                   

                                }
                            }

                        }                        
                    }
                }

                try
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result.csv"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\result.csv");
                    }
                }
                catch (System.Exception ex2)
                {
                    MessageBox.Show("请关闭相关Excel后再查看结果！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                FileStream op = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result.csv"), FileMode.OpenOrCreate);
                StreamWriter str1 = new StreamWriter(op, Encoding.Default);
                str1.Write(output);
                str1.Close();

                output = null;

                output += ("全网交流线结果报表" + "\r\n" + "\r\n");
                output += ("单位：kA\\kV\\MW\\Mvar" + "\r\n" + "\r\n");
                output += ("计算日期：" + System.DateTime.Now.ToString() + "\r\n" + "\r\n");
                output += ("支路名称" + "," + "支路有功" + "," + "支路无功" + "," + "有功损耗" + "," + "无功损耗" + "," + "电流幅值" + "," + "电流相角" + "," + "越限标志" + "," + "\r\n");

                string strCon1 = ",PSPDEV WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'" + " AND PSPDEV.Type = '05'  order by PSPDEV.RateVolt,PSPDEV.Name";
                IList list1 = Services.BaseService.GetList("SelectPSP_ElcDeviceByCondition", strCon1);

                foreach (PSP_ElcDevice elcDEV in list1)
                {
                    PSPDEV dev = new PSPDEV();
                    dev.SUID = elcDEV.DeviceSUID;
                    dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", dev);
                    string lineF = "否";
                    if (dev.KSwitchStatus == "1")
                    {
                        continue;
                    }
                    if (Convert.ToDouble(GetColValue(elcDEV, type - 1).COL14)*1000 > (double)dev.Burthen)
                    {
                        lineF = "是";
                    }
                    //double vTemp = Convert.ToDouble(GetColValue(elcDEV,type-1));
                    //double vTemp1 =  TLPSPVmin * dev.RateVolt;
                    //double vTemp2 =  TLPSPVmax * dev.RateVolt;
                    output += dev.Name.ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL4).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL5).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL6).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL7).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL14).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL15).ToString() + "," + lineF + "," + "\r\n";
                }
                try
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result1.csv"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                    }
                }
                catch (System.Exception ex3)
                {
                    MessageBox.Show("请关闭相关Excel后再查看结果！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                op = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result1.csv"), FileMode.OpenOrCreate);
                str1 = new StreamWriter(op, Encoding.Default);
                str1.Write(output);
                str1.Close();
                Excel.Application ex;
                Excel.Worksheet xSheet;
                Excel.Application result1;
                Excel.Worksheet tempSheet;
                Excel.Worksheet newWorksheet;
                ex = new Excel.Application();
                ex.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result.csv");
                xSheet = (Excel.Worksheet)ex.Worksheets[1];
                ex.Worksheets.Add(System.Reflection.Missing.Value, xSheet, 1, System.Reflection.Missing.Value);

                result1 = new Excel.Application();
                result1.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                tempSheet = (Excel.Worksheet)result1.Worksheets.get_Item(1);
                newWorksheet = (Excel.Worksheet)ex.Worksheets.get_Item(2);
                newWorksheet.Name = "线路电流";
                xSheet.Name = "母线潮流";
                ex.Visible = true;

                tempSheet.Cells.Select();
                tempSheet.Cells.Copy(System.Reflection.Missing.Value);
                newWorksheet.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);

                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 9]).MergeCells = true;
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Size = 20;
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Name = "黑体";
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xSheet.get_Range(xSheet.Cells[7, 1], xSheet.Cells[7, 9]).Interior.ColorIndex = 45;
                xSheet.get_Range(xSheet.Cells[8, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 1]).Interior.ColorIndex = 6;
                xSheet.get_Range(xSheet.Cells[8, 2], xSheet.Cells[xSheet.UsedRange.Rows.Count, 9]).NumberFormat = "0.0000_ ";
                xSheet.get_Range(xSheet.Cells[3, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 9]).Font.Name = "楷体_GB2312";
                //xSheet.get_Range(xSheet.Cells[3, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 1]).NumberFormatLocal = "@";

                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 9]).MergeCells = true;
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Size = 20;
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Name = "黑体";
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                newWorksheet.get_Range(newWorksheet.Cells[7, 1], newWorksheet.Cells[7, 8]).Interior.ColorIndex = 45;
                newWorksheet.get_Range(newWorksheet.Cells[8, 1], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 1]).Interior.ColorIndex = 6;
                // newWorksheet.get_Range(newWorksheet.Cells[6, 1], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 1]).NumberFormatLocal = "@";
                newWorksheet.get_Range(newWorksheet.Cells[8, 2], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 9]).NumberFormat = "0.0000_ ";
                newWorksheet.get_Range(newWorksheet.Cells[3, 1], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 9]).Font.Name = "楷体_GB2312";

                //op = new FileStream((System.Windows.Forms.Application.StartupPath + "\\fck.excel"), FileMode.OpenOrCreate);
                //str1 = new StreamWriter(op, Encoding.Default);
                xSheet.Rows.AutoFit();
                xSheet.Columns.AutoFit();
                newWorksheet.Rows.AutoFit();
                newWorksheet.Columns.AutoFit();

                //result1.Save(System.Windows.Forms.Application.StartupPath + "\\fck.xls");

                if (type == 1)
                {
                    newWorksheet.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "牛拉法计算结果.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);
                }
                else if (type == 2)
                {
                    newWorksheet.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "PQ分解法计算结果.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);
                }
                else if (type == 3)
                {
                    newWorksheet.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "高斯—赛德尔法计算结果.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);
                }
                else if (type == 4)
                {
                    newWorksheet.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "最优乘子法计算结果.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);
                }
                else if (type == 5)
                {
                    newWorksheet.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "配网潮流计算结果.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);
                }
                System.Windows.Forms.Clipboard.Clear();
                result1.Workbooks.Close();
                result1.Quit();

            }
            catch (System.Exception ex)
            {
                wForm.ShowText += "\r\n计算失败\t" + System.DateTime.Now.ToString();
                return false;
            }
            wForm.ShowText += "\r\n计算成功\t" + System.DateTime.Now.ToString();
            return flag;
        }
        public bool LFCERS(string projectSUID, int type, float ratedCapacity)
        {
            bool flag = false;
            flag = LFCS(projectSUID, type, ratedCapacity);
            if (flag == false)
            {
                return false;
            }
            try
            {
                if (type == 1)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "牛拉法计算结果.xls"))
                    {
                        //System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + "线损计算结果.xls");
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "牛拉法计算结果.xls");
                        //OpenRead(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + ".xls");
                    }
                }
                else if (type == 2)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "PQ分解法计算结果.xls"))
                    {
                        //System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + "线损计算结果.xls");
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "PQ分解法计算结果.xls");
                        //OpenRead(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + ".xls");
                    }

                }
                else if (type == 3)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "高斯—赛德尔法计算结果.xls"))
                    {
                        //System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + "线损计算结果.xls");
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "高斯—赛德尔法计算结果.xls");
                        //OpenRead(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + ".xls");
                    }

                }
                else if (type == 4)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "最优乘子法计算结果.xls"))
                    {
                        //System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + "线损计算结果.xls");
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "最优乘子法计算结果.xls");
                        //OpenRead(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + ".xls");
                    }
                }
                else if (type == 5)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "配网潮流计算结果.xls"))
                    {
                        //System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + "线损计算结果.xls");
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "配网潮流计算结果.xls");
                        //OpenRead(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + ".xls");
                    }
                }



                string output = null;
                output += ("全网母线(发电、负荷)结果报表 " + "\r\n" + "\r\n");
                output += ("单位：kA\\kV\\MW\\Mvar" + "\r\n" + "\r\n");
                output += ("计算日期：" + System.DateTime.Now.ToString() + "\r\n" + "\r\n");
                output += ("母线名" + "," + "电压幅值" + "," + "电压相角" + "," + "有功发电" + "," + "无功发电" + "," + "有功负荷" + "," + "无功负荷" + "," + "越限标志" + "," + "过载标志" + "\r\n");
                string strCon = ",PSPDEV WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'" + " AND PSPDEV.Type = '01'";
                IList list = Services.BaseService.GetList("SelectPSP_ElcDeviceByCondition", strCon);
                foreach (PSP_ElcDevice elcDEV in list)
                {
                    PSPDEV dev = new PSPDEV();
                    dev.SUID = elcDEV.DeviceSUID;
                    dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", dev);
                    //double vTemp = Convert.ToDouble(GetColValue(elcDEV,type-1));
                    //double vTemp1 =  TLPSPVmin * dev.RateVolt;
                    //double vTemp2 =  TLPSPVmax * dev.RateVolt;
                    string voltF = "否";
                    string pF = "否";
                    if (dev.KSwitchStatus=="1")
                    {
                        continue;
                    }
                    if (Convert.ToDouble(GetColValue(elcDEV, type - 1).COL2) < dev.iV || Convert.ToDouble(GetColValue(elcDEV, type - 1).COL2) > dev.jV)
                    {
                        voltF = "是";
                    }
                    if (Convert.ToDouble(GetColValue(elcDEV, type - 1).COL2) > (double)dev.Burthen)
                    {
                        pF = "是";
                    }
                    if (Convert.ToDouble(GetColValue(elcDEV, type - 1).COL4) < 0)
                    {
                        output += dev.Name + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL2).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL3).ToString() + "," + "0" + "," + "0" + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL4).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL5).ToString() + "," + voltF + "," + pF + "\r\n";
                    }
                    else
                    {
                        output += dev.Name + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL2).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL3).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL4).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL5).ToString() + "," + "0" + "," + "0" + "," + voltF + "," + pF + "\r\n";
                    }
                }

                try
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result.csv"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\result.csv");
                    }
                }
                catch (System.Exception ex2)
                {
                    MessageBox.Show("请关闭相关Excel后再查看结果！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                FileStream op = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result.csv"), FileMode.OpenOrCreate);
                StreamWriter str1 = new StreamWriter(op, Encoding.Default);
                str1.Write(output);
                str1.Close();

                output = null;

                output += ("全网交流线结果报表" + "\r\n" + "\r\n");
                output += ("单位：kA\\kV\\MW\\Mvar" + "\r\n" + "\r\n");
                output += ("计算日期：" + System.DateTime.Now.ToString() + "\r\n" + "\r\n");
                output += ("支路名称" + "," + "支路有功" + "," + "支路无功" + "," + "有功损耗" + "," + "无功损耗" + "," + "电流幅值" + "," + "电流相角" + "," + "越限标志" + "," + "\r\n");

                string strCon1 = ",PSPDEV WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'" + " AND PSPDEV.Type = '05'";
                IList list1 = Services.BaseService.GetList("SelectPSP_ElcDeviceByCondition", strCon1);

                foreach (PSP_ElcDevice elcDEV in list1)
                {
                    PSPDEV dev = new PSPDEV();
                    dev.SUID = elcDEV.DeviceSUID;
                    dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", dev);
                    string lineF = "否";
                    if (dev.KSwitchStatus=="1")
                    {
                        continue;
                    }
                    if (Convert.ToDouble(GetColValue(elcDEV, type - 1).COL14) > (double)dev.Burthen)
                    {
                        lineF = "是";
                    }
                    //double vTemp = Convert.ToDouble(GetColValue(elcDEV,type-1));
                    //double vTemp1 =  TLPSPVmin * dev.RateVolt;
                    //double vTemp2 =  TLPSPVmax * dev.RateVolt;
                    output += dev.Name.ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL4).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL5).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL6).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL7).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL14).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, type - 1).COL15).ToString() + "," + lineF + "," + "\r\n";
                }
                try
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result1.csv"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                    }
                }
                catch (System.Exception ex3)
                {
                    MessageBox.Show("请关闭相关Excel后再查看结果！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                op = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result1.csv"), FileMode.OpenOrCreate);
                str1 = new StreamWriter(op, Encoding.Default);
                str1.Write(output);
                str1.Close();
                Excel.Application ex;
                Excel.Worksheet xSheet;
                Excel.Application result1;
                Excel.Worksheet tempSheet;
                Excel.Worksheet newWorksheet;
                ex = new Excel.Application();
                ex.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result.csv");
                xSheet = (Excel.Worksheet)ex.Worksheets[1];
                ex.Worksheets.Add(System.Reflection.Missing.Value, xSheet, 1, System.Reflection.Missing.Value);

                result1 = new Excel.Application();
                result1.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                tempSheet = (Excel.Worksheet)result1.Worksheets.get_Item(1);
                newWorksheet = (Excel.Worksheet)ex.Worksheets.get_Item(2);
                newWorksheet.Name = "线路电流";
                xSheet.Name = "母线潮流";
                ex.Visible = true;

                tempSheet.Cells.Select();
                tempSheet.Cells.Copy(System.Reflection.Missing.Value);
                newWorksheet.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);

                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 9]).MergeCells = true;
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Size = 20;
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Name = "黑体";
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xSheet.get_Range(xSheet.Cells[7, 1], xSheet.Cells[7, 9]).Interior.ColorIndex = 45;
                xSheet.get_Range(xSheet.Cells[8, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 1]).Interior.ColorIndex = 6;
                xSheet.get_Range(xSheet.Cells[8, 2], xSheet.Cells[xSheet.UsedRange.Rows.Count, 9]).NumberFormat = "0.0000_ ";
                xSheet.get_Range(xSheet.Cells[3, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 9]).Font.Name = "楷体_GB2312";
                //xSheet.get_Range(xSheet.Cells[3, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 1]).NumberFormatLocal = "@";

                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 9]).MergeCells = true;
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Size = 20;
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Name = "黑体";
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                newWorksheet.get_Range(newWorksheet.Cells[7, 1], newWorksheet.Cells[7, 8]).Interior.ColorIndex = 45;
                newWorksheet.get_Range(newWorksheet.Cells[8, 1], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 1]).Interior.ColorIndex = 6;
                // newWorksheet.get_Range(newWorksheet.Cells[6, 1], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 1]).NumberFormatLocal = "@";
                newWorksheet.get_Range(newWorksheet.Cells[8, 2], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 9]).NumberFormat = "0.0000_ ";
                newWorksheet.get_Range(newWorksheet.Cells[3, 1], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 9]).Font.Name = "楷体_GB2312";

                //op = new FileStream((System.Windows.Forms.Application.StartupPath + "\\fck.excel"), FileMode.OpenOrCreate);
                //str1 = new StreamWriter(op, Encoding.Default);
                xSheet.Rows.AutoFit();
                xSheet.Columns.AutoFit();
                newWorksheet.Rows.AutoFit();
                newWorksheet.Columns.AutoFit();

                //result1.Save(System.Windows.Forms.Application.StartupPath + "\\fck.xls");

                if (type == 1)
                {
                    newWorksheet.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "牛拉法计算结果.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);
                }
                else if (type == 2)
                {
                    newWorksheet.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "PQ分解法计算结果.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);
                }
                else if (type == 3)
                {
                    newWorksheet.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "高斯—赛德尔法计算结果.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);
                }
                else if (type == 4)
                {
                    newWorksheet.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "最优乘子法计算结果.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);
                }
                else if (type == 5)
                {
                    newWorksheet.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + projectSUID + "配网潮流计算结果.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);
                }
                System.Windows.Forms.Clipboard.Clear();
                result1.Workbooks.Close();
                result1.Quit();

            }
            catch (System.Exception ex)
            {
                return false;
            }
            return flag;
        }

        public bool LFC(string projectSUID, int type, float ratedCapacity)
        {
            //WaitDialogForm wait = new WaitDialogForm("","正在进行计算，请等待！");
            //wait.SetCaption("正在处理数据。。。");
            try
            {

                string strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "' AND KSwitchStatus ='0'";
                string strCon2 = null;
                string strCon = null;
                string strData = null;
                string strBus = null;
                string strBranch = null;
                double Rad_to_Deg = Math.PI / 180 ;
                {
                    strCon2 = " AND Type = '01'";
                    strCon = strCon1 + strCon2;
                    IList listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = " AND Type = '05'";
                    strCon = strCon1 + strCon2;
                    IList listXL = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = " AND Type = '02'";
                    strCon = strCon1 + strCon2;
                    IList listBYQ2 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = " AND Type = '03'";
                    strCon = strCon1 + strCon2;
                    IList listBYQ3 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    strData += (listXL.Count + listBYQ2.Count + listBYQ3.Count * 3).ToString() + " " + listMX.Count.ToString() + " " + listMX.Count.ToString() + " " + "0.00001" + " " + "100" + " " + "0" + " " + "0";
                    foreach (PSPDEV dev in listXL)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            if (strBranch != null)
                            {
                                strBranch += "\r\n";
                            }
                            if (strData != null)
                            {
                                strData += "\r\n";
                            }
                            if (dev.FirstNode==dev.LastNode)
                            {
                                if (dev.UnitFlag == "0")
                                {
                                    strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "3" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + "0" + " " + "0";
                                    strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.LineGNDC).ToString() + " " + "0" + " " + dev.Name.ToString());
                                }
                                else
                                {
                                    strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "3" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + "0";
                                    strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + ((dev.LineGNDC) * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + dev.Name.ToString());
                                }
                            } 
                            else
                            {
                                if (dev.UnitFlag == "0")
                                {
                                    strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + "0" + " " + "0";
                                    strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.LineGNDC).ToString() + " " + "0" + " " + dev.Name.ToString());
                                }
                                else
                                {
                                    strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + "0";
                                    strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + ((dev.LineGNDC) * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + dev.Name.ToString());
                                }
                            }

                        }
                    }
                    foreach (PSPDEV dev in listBYQ2)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            if (strBranch != null)
                            {
                                strBranch += "\r\n";
                            }
                            if (strData != null)
                            {
                                strData += "\r\n";
                            }
                            if (dev.UnitFlag == "0")
                            {
                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + dev.K.ToString() + " " + dev.G.ToString();
                                strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "1" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.K).ToString() + " " + dev.G.ToString() + " " + dev.Name.ToString());
                            }
                            else
                            {
                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + dev.K.ToString() + " " + dev.G.ToString();
                                strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "1" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.K.ToString()).ToString() + " " + dev.G.ToString() + " " + dev.Name.ToString());
                            }
                        }
                    }
                    foreach (PSPDEV dev in listBYQ3)
                    {
                        if (dev.KSwitchStatus == "0")
                        {

                            if (dev.UnitFlag == "0")
                            {
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                if (strData != null)
                                {
                                    strData += "\r\n";
                                }
                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.HuganTQ1.ToString() + " " + dev.HuganTQ4.ToString() + " " + "0" + " " + dev.K.ToString() + " " + "0";
                                strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "1" + " " + (dev.HuganTQ1).ToString() + " " + (dev.HuganTQ4).ToString() + " " + (dev.K).ToString() + " " + "0" + " " + dev.Name.ToString());
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                if (strData != null)
                                {
                                    strData += "\r\n";
                                }
                                strBranch += dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.HuganTQ2.ToString() + " " + dev.HuganTQ5.ToString() + " " + "0" + " " + dev.StandardCurrent.ToString() + " " + "0";

                                strData += (dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + "1" + " " + (dev.HuganTQ2).ToString() + " " + (dev.HuganTQ5).ToString() + " " + (dev.StandardCurrent).ToString() + " " + "0" + " " + dev.Name.ToString());
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                if (strData != null)
                                {
                                    strData += "\r\n";
                                }
                                strBranch += dev.FirstNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.HuganTQ3.ToString() + " " + dev.ZeroTQ.ToString() + " " + "0" + " " + dev.BigP.ToString() + " " + "0";
                                strData += (dev.FirstNode.ToString() + " " + dev.Flag.ToString() + " " + "1" + " " + (dev.HuganTQ3).ToString() + " " + (dev.ZeroTQ).ToString() + " " + (dev.BigP).ToString() + " " + "0" + " " + dev.Name.ToString());

                            }
                            else
                            {
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                if (strData != null)
                                {
                                    strData += "\r\n";
                                }
                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.HuganTQ1 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.HuganTQ4 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + "0" + " " + dev.K.ToString() + " " + "0";
                                strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "1" + " " + (dev.HuganTQ1 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.HuganTQ4 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.K).ToString() + " " + "0" + " " + dev.Name.ToString());
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                if (strData != null)
                                {
                                    strData += "\r\n";
                                }
                                strBranch += dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.HuganTQ2 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.HuganTQ5 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + "0" + " " + dev.StandardCurrent.ToString() + " " + "0";
                                strData += (dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + "1" + " " + (dev.HuganTQ2 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.HuganTQ5 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.StandardCurrent).ToString() + " " + "0" + " " + dev.Name.ToString());
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                if (strData != null)
                                {
                                    strData += "\r\n";
                                }
                                strBranch += dev.FirstNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.HuganTQ3 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.ZeroTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + "0" + " " + dev.BigP.ToString() + " " + "0";
                                strData += (dev.FirstNode.ToString() + " " + dev.Flag.ToString() + " " + "1" + " " + (dev.HuganTQ3 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.ZeroTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.BigP).ToString() + " " + "0" + " " + dev.Name.ToString());
                            }
                        }
                    }
                    //foreach (PSPDEV dev in listMX)
                    //{
                    //    if (dev.KSwitchStatus == "0")
                    //    {
                    //        if (strBus != null)
                    //        {
                    //            strBus += "\r\n";
                    //        }
                    //        if (strData != null)
                    //        {
                    //            strData += "\r\n";
                    //        }
                    //        if (dev.UnitFlag == "0")
                    //        {
                    //            strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR).ToString() + " " + (dev.VoltV*Rad_to_Deg).ToString() + " " + ((dev.InPutP - dev.OutP)).ToString() + " " + ((dev.InPutQ - dev.OutQ)).ToString());
                    //            //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.LineGNDC).ToString() + " " + "0" + " " + dev.Name.ToString());
                    //            if (dev.NodeType == "1")
                    //            {
                    //                strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + ((dev.OutP)).ToString() + " " + ((dev.OutQ)).ToString());
                    //            }
                    //            else if (dev.NodeType == "2")
                    //            {
                    //                strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + ((dev.OutP)).ToString() + " " + (dev.VoltR).ToString());
                    //            }
                    //            else if (dev.NodeType == "0")
                    //            {
                    //                strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + (dev.VoltR).ToString() + " " + "0");
                    //            }
                    //        }
                    //        else
                    //        {
                    //            strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + (dev.VoltV * Rad_to_Deg).ToString() + " " + ((dev.InPutP - dev.OutP) / ratedCapacity).ToString() + " " + ((dev.InPutQ - dev.OutQ) / ratedCapacity).ToString());
                    //            //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + ((dev.LineGNDC) * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + dev.Name.ToString());
                    //            if (dev.NodeType == "1")
                    //            {
                    //                strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + ((dev.OutP) / ratedCapacity).ToString() + " " + ((dev.OutQ) / ratedCapacity).ToString());
                    //            }
                    //            else if (dev.NodeType == "2")
                    //            {
                    //                strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + ((dev.OutP) / ratedCapacity).ToString() + " " + (dev.VoltR / dev.ReferenceVolt).ToString());
                    //            }
                    //            else if (dev.NodeType == "0")
                    //            {
                    //                strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + "0");
                    //            }
                    //        }
                    //    }
                    //}
                    //foreach (PSPDEV dev in listMX)
                    //{
                    //    if (dev.KSwitchStatus == "0")
                    //    {
                    //        if (strData != null)
                    //        {
                    //            strData += "\r\n";
                    //        }
                    //        if (dev.UnitFlag == "0")
                    //        {
                    //            strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + ((dev.InPutP)).ToString() + " " + ((dev.InPutQ)).ToString());
                    //        }
                    //        else
                    //        {
                    //            strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + ((dev.InPutP) / ratedCapacity).ToString() + " " + ((dev.InPutQ) / ratedCapacity).ToString());
                    //        }
                    //    }
                    //}
                    foreach (PSPDEV dev in listMX)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            if (strBus != null)
                            {
                                strBus += "\r\n";
                            }
                            if (strData != null)
                            {
                                strData += "\r\n";
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
                            //if (mxflag.ContainsKey(dev.SUID))
                            //{
                            //    gltj tj = mxflag[dev.SUID];
                            //    outP += tj.outP;
                            //    outQ += tj.outQ;
                            //    inputP += tj.inputP;
                            //    inputQ += tj.inputQ;
                            //}

                            if (dev.UnitFlag == "0")
                            {
                                outP += dev.OutP;
                                outQ += dev.OutQ;
                                inputP += dev.InPutP;
                                inputQ += dev.InPutQ;
                                strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR ).ToString() + " " + (dev.VoltV * Rad_to_Deg).ToString() + " " + ((inputP - outP)).ToString() + " " + ((inputQ - outQ)).ToString());
                                //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.LineGNDC).ToString() + " " + "0" + " " + dev.Name.ToString());
                                if (dev.NodeType == "1")
                                {
                                    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + ((outP)).ToString() + " " + ((outQ)).ToString());
                                }
                                else if (dev.NodeType == "2")
                                {
                                    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + ((outP)).ToString() + " " + (dev.VoltR).ToString());
                                }
                                else if (dev.NodeType == "0")
                                {
                                    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + (dev.VoltR).ToString() + " " + "0");
                                }
                            }
                            else
                            {
                                outP += dev.OutP / ratedCapacity;
                                outQ += dev.OutQ / ratedCapacity;
                                inputP += dev.InPutP / ratedCapacity;
                                inputQ += dev.InPutQ / ratedCapacity;
                                strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + (dev.VoltV * Rad_to_Deg).ToString() + " " + ((inputP - outP)).ToString() + " " + ((inputQ - outQ)).ToString());
                                //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + ((dev.LineGNDC) * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + dev.Name.ToString());
                                if (dev.NodeType == "1")
                                {
                                    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + (outP).ToString() + " " + (outQ).ToString());
                                }
                                else if (dev.NodeType == "2")
                                {
                                    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + (outP).ToString() + " " + (dev.VoltR / dev.ReferenceVolt).ToString());
                                }
                                else if (dev.NodeType == "0")
                                {
                                    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + "0");
                                }
                            }
                        }
                    }
                    foreach (PSPDEV dev in listMX)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            if (strData != null)
                            {
                                strData += "\r\n";
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
                            if (dev.UnitFlag=="0")
                            {
                                outP += dev.OutP;
                                outQ += dev.OutQ;
                                inputP += dev.InPutP;
                                inputQ += dev.InPutQ;
                            } 
                            else
                            {
                                outP += dev.OutP / ratedCapacity;
                                outQ += dev.OutQ / ratedCapacity;
                                inputP += dev.InPutP / ratedCapacity;
                                inputQ += dev.InPutQ / ratedCapacity;
                            }
                            strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + ((inputP)).ToString() + " " + ((inputQ)).ToString());

                        }
                    }
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\data.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\data.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\branch.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\branch.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\bus.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\bus.txt");
                }
                FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\data.txt"), FileMode.OpenOrCreate);
                StreamWriter str1 = new StreamWriter(VK, Encoding.Default);
                str1.Write(strData);
                str1.Close();

                FileStream VK1 = new FileStream((System.Windows.Forms.Application.StartupPath + "\\branch.txt"), FileMode.OpenOrCreate);
                StreamWriter str3 = new StreamWriter(VK1, Encoding.Default);
                str3.Write(strBranch);
                str3.Close();
                FileStream L = new FileStream((System.Windows.Forms.Application.StartupPath + "\\bus.txt"), FileMode.OpenOrCreate);
                StreamWriter str2 = new StreamWriter(L, Encoding.Default);
                str2.Write(strBus);
                str2.Close();
                if (strData.Contains("非数字") || strData.Contains("正无穷大") || strBus.Contains("非数字") || strBus.Contains("正无穷大") || strBranch.Contains("非数字") || strBranch.Contains("正无穷大"))
                {
                    //wait.Close();
                    MessageBox.Show("缺少参数，请检查输入参数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                //wait.SetCaption("正在进行计算。。。");              
                if (type == 1)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF1.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\PF1.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH1.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\DH1.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH1.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\IH1.txt");
                    }
                    NIULA nr = new NIULA();
                    nr.CurrentCal();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF1.txt"))
                    {
                    }
                    else
                    {
                        //wait.Close();
                        return false;
                    }
                    FileStream pf = new FileStream(System.Windows.Forms.Application.StartupPath + "\\PF1.txt", FileMode.Open);
                    StreamReader readLine = new StreamReader(pf, Encoding.Default);
                    char[] charSplit = new char[] { ' ' };
                    string strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '01' AND Number = " + array1[0];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL1 = devMX.Name;
                                elcDev.COL19 = devMX.ReferenceVolt.ToString();
                                elcDev.COL20 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[1], out temp);
                                elcDev.COL2 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[2], out temp);
                                elcDev.COL3 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL4 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL5 = (temp * ratedCapacity).ToString();

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH1.txt"))
                    {
                    }
                    else
                    {
                        //wait.Close();
                        return false;
                    }
                    FileStream dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH1.txt", FileMode.Open);
                    readLine = new StreamReader(dh, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL1 = devMX.Name;
                                elcDev.COL2 = devMX.FirstNode.ToString();
                                elcDev.COL3 = devMX.LastNode.ToString();
                                elcDev.COL19 = devMX.ReferenceVolt.ToString();
                                elcDev.COL20 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL4 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL5 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[5], out temp);
                                elcDev.COL6 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[6], out temp);
                                elcDev.COL7 = (temp * ratedCapacity).ToString();

                                temp = 0.0;
                                double.TryParse(array1[7], out temp);
                                elcDev.COL8 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[8], out temp);
                                elcDev.COL9 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[9], out temp);
                                elcDev.COL10 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();

                                temp = 0.0;
                                double.TryParse(array1[10], out temp);
                                elcDev.COL11 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[11], out temp);
                                elcDev.COL12 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[12], out temp);
                                elcDev.COL13 = (temp * (devMX.ReferenceVolt)).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH1.txt"))
                    {
                    }
                    else
                    {
                        //wait.Close();
                        return false;
                    }
                    FileStream ih = new FileStream(System.Windows.Forms.Application.StartupPath + "\\IH1.txt", FileMode.Open);
                    readLine = new StreamReader(ih, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL1 = devMX.Name;
                                elcDev.COL2 = devMX.FirstNode.ToString();
                                elcDev.COL3 = devMX.LastNode.ToString();
                                elcDev.COL19 = devMX.ReferenceVolt.ToString();
                                elcDev.COL20 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL14 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL15 = (temp * Rad_to_Deg).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                }
                else if (type == 2)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF2.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\PF2.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH2.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\DH2.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH2.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\IH2.txt");
                    }
                    PQ_PowerFlowCalClass pq = new PQ_PowerFlowCalClass();
                    pq.CurrentCal();

                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF2.txt"))
                    {
                    }
                    else
                    {
                        //wait.Close();
                        return false;
                    }
                    FileStream pf = new FileStream(System.Windows.Forms.Application.StartupPath + "\\PF2.txt", FileMode.Open);
                    StreamReader readLine = new StreamReader(pf, Encoding.Default);
                    char[] charSplit = new char[] { ' ' };
                    string strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '01' AND Number = " + array1[0];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL21 = devMX.Name;
                                elcDev.COL39 = devMX.ReferenceVolt.ToString();
                                elcDev.COL40 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[1], out temp);
                                elcDev.COL22 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[2], out temp);
                                elcDev.COL23 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL24 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL25 = (temp * ratedCapacity).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH2.txt"))
                    {
                    }
                    else
                    {
                        //wait.Close();
                        return false;
                    }
                    FileStream dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH2.txt", FileMode.Open);
                    readLine = new StreamReader(dh, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL21 = devMX.Name;
                                elcDev.COL22 = devMX.FirstNode.ToString();
                                elcDev.COL23 = devMX.LastNode.ToString();
                                elcDev.COL39 = devMX.ReferenceVolt.ToString();
                                elcDev.COL40 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL24 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL25 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[5], out temp);
                                elcDev.COL26 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[6], out temp);
                                elcDev.COL27 = (temp * ratedCapacity).ToString();

                                temp = 0.0;
                                double.TryParse(array1[7], out temp);
                                elcDev.COL28 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[8], out temp);
                                elcDev.COL29 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[9], out temp);
                                elcDev.COL30 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();

                                temp = 0.0;
                                double.TryParse(array1[10], out temp);
                                elcDev.COL31 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[11], out temp);
                                elcDev.COL32 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[12], out temp);
                                elcDev.COL33 = (temp * (devMX.ReferenceVolt)).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH2.txt"))
                    {
                    }
                    else
                    {
                        //wait.Close();
                        return false;
                    }
                    FileStream ih = new FileStream(System.Windows.Forms.Application.StartupPath + "\\IH2.txt", FileMode.Open);
                    readLine = new StreamReader(ih, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL21 = devMX.Name;
                                elcDev.COL22 = devMX.FirstNode.ToString();
                                elcDev.COL23 = devMX.LastNode.ToString();
                                elcDev.COL39 = devMX.ReferenceVolt.ToString();
                                elcDev.COL40 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL34 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL35 = (temp * Rad_to_Deg).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                }
                else if (type == 3)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF3.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\PF3.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH3.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\DH3.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH3.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\IH3.txt");
                    }
                    Gauss gs = new Gauss();
                    gs.CurrentCal();

                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF3.txt"))
                    {
                    }
                    else
                    {
                        //wait.Close();
                        return false;
                    }
                    FileStream pf = new FileStream(System.Windows.Forms.Application.StartupPath + "\\PF3.txt", FileMode.Open);
                    StreamReader readLine = new StreamReader(pf, Encoding.Default);
                    char[] charSplit = new char[] { ' ' };
                    string strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '01' AND Number = " + array1[0];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL41 = devMX.Name;
                                elcDev.COL59 = devMX.ReferenceVolt.ToString();
                                elcDev.COL60 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[1], out temp);
                                elcDev.COL42 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[2], out temp);
                                elcDev.COL43 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL44 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL45 = (temp * ratedCapacity).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH3.txt"))
                    {
                    }
                    else
                    {
                       // wait.Close();
                        return false;
                    }
                    FileStream dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH3.txt", FileMode.Open);
                    readLine = new StreamReader(dh, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL41 = devMX.Name;
                                elcDev.COL42 = devMX.FirstNode.ToString();
                                elcDev.COL43 = devMX.LastNode.ToString();
                                elcDev.COL59 = devMX.ReferenceVolt.ToString();
                                elcDev.COL60 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL44 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL45 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[5], out temp);
                                elcDev.COL46 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[6], out temp);
                                elcDev.COL47 = (temp * ratedCapacity).ToString();

                                temp = 0.0;
                                double.TryParse(array1[7], out temp);
                                elcDev.COL48 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[8], out temp);
                                elcDev.COL49 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[9], out temp);
                                elcDev.COL50 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();

                                temp = 0.0;
                                double.TryParse(array1[10], out temp);
                                elcDev.COL51 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[11], out temp);
                                elcDev.COL52 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[12], out temp);
                                elcDev.COL53 = (temp * (devMX.ReferenceVolt)).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH3.txt"))
                    {
                    }
                    else
                    {
                        //wait.Close();
                        return false;
                    }
                    FileStream ih = new FileStream(System.Windows.Forms.Application.StartupPath + "\\IH3.txt", FileMode.Open);
                    readLine = new StreamReader(ih, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL41 = devMX.Name;
                                elcDev.COL42 = devMX.FirstNode.ToString();
                                elcDev.COL43 = devMX.LastNode.ToString();
                                elcDev.COL59 = devMX.ReferenceVolt.ToString();
                                elcDev.COL60 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL54 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL55 = (temp * Rad_to_Deg).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                }
                else if (type == 4)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF4.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\PF4.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH4.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\DH4.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH4.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\IH4.txt");
                    }
                    ZYZ zy = new ZYZ();
                    zy.CurrentCal();

                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF4.txt"))
                    {
                    }
                    else
                    {
                        //wait.Close();
                        return false;
                    }
                    FileStream pf = new FileStream(System.Windows.Forms.Application.StartupPath + "\\PF4.txt", FileMode.Open);
                    StreamReader readLine = new StreamReader(pf, Encoding.Default);
                    char[] charSplit = new char[] { ' ' };
                    string strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '01' AND Number = " + array1[0];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL61 = devMX.Name;
                                elcDev.COL79 = devMX.ReferenceVolt.ToString();
                                elcDev.COL80 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[1], out temp);
                                elcDev.COL62 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[2], out temp);
                                elcDev.COL63 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL64 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL65 = (temp * ratedCapacity).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH4.txt"))
                    {
                    }
                    else
                    {
                        //wait.Close();
                        return false;
                    }
                    FileStream dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH4.txt", FileMode.Open);
                    readLine = new StreamReader(dh, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL61 = devMX.Name;
                                elcDev.COL62 = devMX.FirstNode.ToString();
                                elcDev.COL63 = devMX.LastNode.ToString();
                                elcDev.COL79 = devMX.ReferenceVolt.ToString();
                                elcDev.COL80 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL64 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL65 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[5], out temp);
                                elcDev.COL66 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[6], out temp);
                                elcDev.COL67 = (temp * ratedCapacity).ToString();

                                temp = 0.0;
                                double.TryParse(array1[7], out temp);
                                elcDev.COL68 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[8], out temp);
                                elcDev.COL69 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[9], out temp);
                                elcDev.COL70 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();

                                temp = 0.0;
                                double.TryParse(array1[10], out temp);
                                elcDev.COL71 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[11], out temp);
                                elcDev.COL72 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[12], out temp);
                                elcDev.COL73 = (temp * (devMX.ReferenceVolt)).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH4.txt"))
                    {
                    }
                    else
                    {
                        //wait.Close();
                        return false;
                    }
                    FileStream ih = new FileStream(System.Windows.Forms.Application.StartupPath + "\\IH4.txt", FileMode.Open);
                    readLine = new StreamReader(ih, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL61 = devMX.Name;
                                elcDev.COL62 = devMX.FirstNode.ToString();
                                elcDev.COL63 = devMX.LastNode.ToString();
                                elcDev.COL79 = devMX.ReferenceVolt.ToString();
                                elcDev.COL80 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL74 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL75 = (temp * Rad_to_Deg).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                }
                //wait.Close();
            }
            catch (System.Exception ex)
            {
                //wait.Close();
                MessageBox.Show("潮流计算结果不收敛，请检查输入参数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
        public bool LFC(string projectSUID, int type, float ratedCapacity,frnReport wForm)
        {
            //WaitDialogForm wait = new WaitDialogForm("","正在进行计算，请等待！");
            //wait.SetCaption("正在处理数据。。。");
            try
            {
                wForm.ShowText += "\r\n正在准备潮流计算数据\t" + System.DateTime.Now.ToString();
                string strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "' AND KSwitchStatus ='0'";
                string strCon2 = null;
                string strCon = null;
                string strData = null;
                string strBus = null;
                string strBranch = null;
                double Rad_to_Deg = Math.PI / 180;
                {
                    strCon2 = " AND Type = '01'";
                    strCon = strCon1 + strCon2;
                    IList listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = " AND Type = '05'";
                    strCon = strCon1 + strCon2;
                    IList listXL = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = " AND Type = '02'";
                    strCon = strCon1 + strCon2;
                    IList listBYQ2 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = " AND Type = '03'";
                    strCon = strCon1 + strCon2;
                    IList listBYQ3 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    strData += (listXL.Count + listBYQ2.Count + listBYQ3.Count * 3).ToString() + " " + listMX.Count.ToString() + " " + listMX.Count.ToString() + " " + "0.00001" + " " + "100" + " " + "0" + " " + "0";
                    foreach (PSPDEV dev in listXL)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            if (strBranch != null)
                            {
                                strBranch += "\r\n";
                            }
                            if (strData != null)
                            {
                                strData += "\r\n";
                            }
                            if (dev.FirstNode == dev.LastNode)
                            {
                                if (dev.UnitFlag == "0")
                                {
                                    strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "3" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + "0" + " " + "0";
                                    strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.LineGNDC).ToString() + " " + "0" + " " + dev.Name.ToString());
                                }
                                else
                                {
                                    strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "3" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + "0";
                                    strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + ((dev.LineGNDC) * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + dev.Name.ToString());
                                }
                            }
                            else
                            {
                                if (dev.UnitFlag == "0")
                                {
                                    strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + "0" + " " + "0";
                                    strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.LineGNDC).ToString() + " " + "0" + " " + dev.Name.ToString());
                                }
                                else
                                {
                                    strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + "0";
                                    strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + ((dev.LineGNDC) * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + dev.Name.ToString());
                                }
                            }

                        }
                    }
                    foreach (PSPDEV dev in listBYQ2)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            if (strBranch != null)
                            {
                                strBranch += "\r\n";
                            }
                            if (strData != null)
                            {
                                strData += "\r\n";
                            }
                            if (dev.UnitFlag == "0")
                            {
                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + dev.K.ToString() + " " + dev.G.ToString();
                                strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "1" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.K).ToString() + " " + dev.G.ToString() + " " + dev.Name.ToString());
                            }
                            else
                            {
                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + dev.K.ToString() + " " + dev.G.ToString();
                                strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "1" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.K.ToString()).ToString() + " " + dev.G.ToString() + " " + dev.Name.ToString());
                            }
                        }
                    }
                    foreach (PSPDEV dev in listBYQ3)
                    {
                        if (dev.KSwitchStatus == "0")
                        {

                            if (dev.UnitFlag == "0")
                            {
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                if (strData != null)
                                {
                                    strData += "\r\n";
                                }
                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.HuganTQ1.ToString() + " " + dev.HuganTQ4.ToString() + " " + "0" + " " + dev.K.ToString() + " " + "0";
                                strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "1" + " " + (dev.HuganTQ1).ToString() + " " + (dev.HuganTQ4).ToString() + " " + (dev.K).ToString() + " " + "0" + " " + dev.Name.ToString());
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                if (strData != null)
                                {
                                    strData += "\r\n";
                                }
                                strBranch += dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.HuganTQ2.ToString() + " " + dev.HuganTQ5.ToString() + " " + "0" + " " + dev.StandardCurrent.ToString() + " " + "0";

                                strData += (dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + "1" + " " + (dev.HuganTQ2).ToString() + " " + (dev.HuganTQ5).ToString() + " " + (dev.StandardCurrent).ToString() + " " + "0" + " " + dev.Name.ToString());
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                if (strData != null)
                                {
                                    strData += "\r\n";
                                }
                                strBranch += dev.FirstNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + dev.HuganTQ3.ToString() + " " + dev.ZeroTQ.ToString() + " " + "0" + " " + dev.BigP.ToString() + " " + "0";
                                strData += (dev.FirstNode.ToString() + " " + dev.Flag.ToString() + " " + "1" + " " + (dev.HuganTQ3).ToString() + " " + (dev.ZeroTQ).ToString() + " " + (dev.BigP).ToString() + " " + "0" + " " + dev.Name.ToString());

                            }
                            else
                            {
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                if (strData != null)
                                {
                                    strData += "\r\n";
                                }
                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.HuganTQ1 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.HuganTQ4 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + "0" + " " + dev.K.ToString() + " " + "0";
                                strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "1" + " " + (dev.HuganTQ1 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.HuganTQ4 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.K).ToString() + " " + "0" + " " + dev.Name.ToString());
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                if (strData != null)
                                {
                                    strData += "\r\n";
                                }
                                strBranch += dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.HuganTQ2 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.HuganTQ5 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + "0" + " " + dev.StandardCurrent.ToString() + " " + "0";
                                strData += (dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + "1" + " " + (dev.HuganTQ2 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.HuganTQ5 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.StandardCurrent).ToString() + " " + "0" + " " + dev.Name.ToString());
                                if (strBranch != null)
                                {
                                    strBranch += "\r\n";
                                }
                                if (strData != null)
                                {
                                    strData += "\r\n";
                                }
                                strBranch += dev.FirstNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.HuganTQ3 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.ZeroTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + "0" + " " + dev.BigP.ToString() + " " + "0";
                                strData += (dev.FirstNode.ToString() + " " + dev.Flag.ToString() + " " + "1" + " " + (dev.HuganTQ3 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.ZeroTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.BigP).ToString() + " " + "0" + " " + dev.Name.ToString());
                            }
                        }
                    }
                    //foreach (PSPDEV dev in listMX)
                    //{
                    //    if (dev.KSwitchStatus == "0")
                    //    {
                    //        if (strBus != null)
                    //        {
                    //            strBus += "\r\n";
                    //        }
                    //        if (strData != null)
                    //        {
                    //            strData += "\r\n";
                    //        }
                    //        if (dev.UnitFlag == "0")
                    //        {
                    //            strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR).ToString() + " " + (dev.VoltV*Rad_to_Deg).ToString() + " " + ((dev.InPutP - dev.OutP)).ToString() + " " + ((dev.InPutQ - dev.OutQ)).ToString());
                    //            //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.LineGNDC).ToString() + " " + "0" + " " + dev.Name.ToString());
                    //            if (dev.NodeType == "1")
                    //            {
                    //                strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + ((dev.OutP)).ToString() + " " + ((dev.OutQ)).ToString());
                    //            }
                    //            else if (dev.NodeType == "2")
                    //            {
                    //                strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + ((dev.OutP)).ToString() + " " + (dev.VoltR).ToString());
                    //            }
                    //            else if (dev.NodeType == "0")
                    //            {
                    //                strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + (dev.VoltR).ToString() + " " + "0");
                    //            }
                    //        }
                    //        else
                    //        {
                    //            strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + (dev.VoltV * Rad_to_Deg).ToString() + " " + ((dev.InPutP - dev.OutP) / ratedCapacity).ToString() + " " + ((dev.InPutQ - dev.OutQ) / ratedCapacity).ToString());
                    //            //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + ((dev.LineGNDC) * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + dev.Name.ToString());
                    //            if (dev.NodeType == "1")
                    //            {
                    //                strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + ((dev.OutP) / ratedCapacity).ToString() + " " + ((dev.OutQ) / ratedCapacity).ToString());
                    //            }
                    //            else if (dev.NodeType == "2")
                    //            {
                    //                strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + ((dev.OutP) / ratedCapacity).ToString() + " " + (dev.VoltR / dev.ReferenceVolt).ToString());
                    //            }
                    //            else if (dev.NodeType == "0")
                    //            {
                    //                strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + "0");
                    //            }
                    //        }
                    //    }
                    //}
                    //foreach (PSPDEV dev in listMX)
                    //{
                    //    if (dev.KSwitchStatus == "0")
                    //    {
                    //        if (strData != null)
                    //        {
                    //            strData += "\r\n";
                    //        }
                    //        if (dev.UnitFlag == "0")
                    //        {
                    //            strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + ((dev.InPutP)).ToString() + " " + ((dev.InPutQ)).ToString());
                    //        }
                    //        else
                    //        {
                    //            strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + ((dev.InPutP) / ratedCapacity).ToString() + " " + ((dev.InPutQ) / ratedCapacity).ToString());
                    //        }
                    //    }
                    //}
                    foreach (PSPDEV dev in listMX)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            if (strBus != null)
                            {
                                strBus += "\r\n";
                            }
                            if (strData != null)
                            {
                                strData += "\r\n";
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
                            //if (mxflag.ContainsKey(dev.SUID))
                            //{
                            //    gltj tj = mxflag[dev.SUID];
                            //    outP += tj.outP;
                            //    outQ += tj.outQ;
                            //    inputP += tj.inputP;
                            //    inputQ += tj.inputQ;
                            //}

                            if (dev.UnitFlag == "0")
                            {
                                outP += dev.OutP;
                                outQ += dev.OutQ;
                                inputP += dev.InPutP;
                                inputQ += dev.InPutQ;
                                strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR).ToString() + " " + (dev.VoltV * Rad_to_Deg).ToString() + " " + ((inputP - outP)).ToString() + " " + ((inputQ - outQ)).ToString());
                                //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.LineGNDC).ToString() + " " + "0" + " " + dev.Name.ToString());
                                if (dev.NodeType == "1")
                                {
                                    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + ((outP)).ToString() + " " + ((outQ)).ToString());
                                }
                                else if (dev.NodeType == "2")
                                {
                                    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + ((outP)).ToString() + " " + (dev.VoltR).ToString());
                                }
                                else if (dev.NodeType == "0")
                                {
                                    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + (dev.VoltR).ToString() + " " + "0");
                                }
                            }
                            else
                            {
                                outP += dev.OutP / ratedCapacity;
                                outQ += dev.OutQ / ratedCapacity;
                                inputP += dev.InPutP / ratedCapacity;
                                inputQ += dev.InPutQ / ratedCapacity;
                                strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + (dev.VoltV * Rad_to_Deg).ToString() + " " + ((inputP - outP)).ToString() + " " + ((inputQ - outQ)).ToString());
                                //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + ((dev.LineGNDC) * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + dev.Name.ToString());
                                if (dev.NodeType == "1")
                                {
                                    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + (outP).ToString() + " " + (outQ).ToString());
                                }
                                else if (dev.NodeType == "2")
                                {
                                    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + (outP).ToString() + " " + (dev.VoltR / dev.ReferenceVolt).ToString());
                                }
                                else if (dev.NodeType == "0")
                                {
                                    strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + "0");
                                }
                            }
                        }
                    }
                    foreach (PSPDEV dev in listMX)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            if (strData != null)
                            {
                                strData += "\r\n";
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
                            }
                            else
                            {
                                outP += dev.OutP / ratedCapacity;
                                outQ += dev.OutQ / ratedCapacity;
                                inputP += dev.InPutP / ratedCapacity;
                                inputQ += dev.InPutQ / ratedCapacity;
                            }
                            strData += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + ((inputP)).ToString() + " " + ((inputQ)).ToString());

                        }
                    }
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\data.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\data.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\branch.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\branch.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\bus.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\bus.txt");
                }
                FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\data.txt"), FileMode.OpenOrCreate);
                StreamWriter str1 = new StreamWriter(VK, Encoding.Default);
                str1.Write(strData);
                str1.Close();

                FileStream VK1 = new FileStream((System.Windows.Forms.Application.StartupPath + "\\branch.txt"), FileMode.OpenOrCreate);
                StreamWriter str3 = new StreamWriter(VK1, Encoding.Default);
                str3.Write(strBranch);
                str3.Close();
                FileStream L = new FileStream((System.Windows.Forms.Application.StartupPath + "\\bus.txt"), FileMode.OpenOrCreate);
                StreamWriter str2 = new StreamWriter(L, Encoding.Default);
                str2.Write(strBus);
                str2.Close();
                if (strData.Contains("非数字") || strData.Contains("正无穷大") || strBus.Contains("非数字") || strBus.Contains("正无穷大") || strBranch.Contains("非数字") || strBranch.Contains("正无穷大"))
                {
                    //wait.Close();
                    MessageBox.Show("缺少参数，请检查输入参数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                //wait.SetCaption("正在进行计算。。。");              
                if (type == 1)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF1.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\PF1.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH1.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\DH1.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH1.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\IH1.txt");
                    }
                    wForm.ShowText += "\r\n正在进行迭代\t" + System.DateTime.Now.ToString();
                    NIULA nr = new NIULA();
                    nr.CurrentCal();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF1.txt"))
                    {
                    }
                    else
                    {
                        //wait.Close();
                        return false;
                    }
                    FileStream pf = new FileStream(System.Windows.Forms.Application.StartupPath + "\\PF1.txt", FileMode.Open);
                    StreamReader readLine = new StreamReader(pf, Encoding.Default);
                    char[] charSplit = new char[] { ' ' };
                    string strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '01' AND Number = " + array1[0];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL1 = devMX.Name;
                                elcDev.COL19 = devMX.ReferenceVolt.ToString();
                                elcDev.COL20 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[1], out temp);
                                elcDev.COL2 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[2], out temp);
                                elcDev.COL3 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL4 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL5 = (temp * ratedCapacity).ToString();

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH1.txt"))
                    {
                    }
                    else
                    {
                        //wait.Close();
                        return false;
                    }
                    FileStream dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH1.txt", FileMode.Open);
                    readLine = new StreamReader(dh, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL1 = devMX.Name;
                                elcDev.COL2 = devMX.FirstNode.ToString();
                                elcDev.COL3 = devMX.LastNode.ToString();
                                elcDev.COL19 = devMX.ReferenceVolt.ToString();
                                if (Convert.ToDouble(devMX.Burthen) == 0.0)
                                {
                                    elcDev.COL20 = ratedCapacity.ToString();
                                }
                                else
                                    elcDev.COL20 = devMX.Burthen.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL4 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL5 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[5], out temp);
                                elcDev.COL6 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[6], out temp);
                                elcDev.COL7 = (temp * ratedCapacity).ToString();

                                temp = 0.0;
                                double.TryParse(array1[7], out temp);
                                elcDev.COL8 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[8], out temp);
                                elcDev.COL9 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[9], out temp);
                                elcDev.COL10 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();

                                temp = 0.0;
                                double.TryParse(array1[10], out temp);
                                elcDev.COL11 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[11], out temp);
                                elcDev.COL12 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[12], out temp);
                                elcDev.COL13 = (temp * (devMX.ReferenceVolt)).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH1.txt"))
                    {
                    }
                    else
                    {
                        //wait.Close();
                        return false;
                    }
                    FileStream ih = new FileStream(System.Windows.Forms.Application.StartupPath + "\\IH1.txt", FileMode.Open);
                    readLine = new StreamReader(ih, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL1 = devMX.Name;
                                elcDev.COL2 = devMX.FirstNode.ToString();
                                elcDev.COL3 = devMX.LastNode.ToString();
                                elcDev.COL19 = devMX.ReferenceVolt.ToString();
                                if (Convert.ToDouble(devMX.Burthen) == 0.0)
                                {
                                    elcDev.COL20 = ratedCapacity.ToString();
                                }
                                else
                                    elcDev.COL20 = devMX.Burthen.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL14 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL15 = (temp * Rad_to_Deg).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                }
                else if (type == 2)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF2.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\PF2.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH2.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\DH2.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH2.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\IH2.txt");
                    }
                    wForm.ShowText += "\r\n正在进行迭代\t" + System.DateTime.Now.ToString();
                    
                    PQ_PowerFlowCalClass pq = new PQ_PowerFlowCalClass();        
                    pq.CurrentCal();                    
                    GC.Collect();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF2.txt"))
                    {
                    }
                    else
                    {
                        //wait.Close();
                        return false;
                    }
                    FileStream pf = new FileStream(System.Windows.Forms.Application.StartupPath + "\\PF2.txt", FileMode.Open);
                    StreamReader readLine = new StreamReader(pf, Encoding.Default);
                    char[] charSplit = new char[] { ' ' };
                    string strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '01' AND Number = " + array1[0];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL21 = devMX.Name;
                                elcDev.COL39 = devMX.ReferenceVolt.ToString();
                                elcDev.COL40 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[1], out temp);
                                elcDev.COL22 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[2], out temp);
                                elcDev.COL23 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL24 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL25 = (temp * ratedCapacity).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH2.txt"))
                    {
                    }
                    else
                    {
                        //wait.Close();
                        return false;
                    }
                    FileStream dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH2.txt", FileMode.Open);
                    readLine = new StreamReader(dh, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL21 = devMX.Name;
                                elcDev.COL22 = devMX.FirstNode.ToString();
                                elcDev.COL23 = devMX.LastNode.ToString();
                                elcDev.COL39 = devMX.ReferenceVolt.ToString();
                                elcDev.COL40 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL24 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL25 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[5], out temp);
                                elcDev.COL26 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[6], out temp);
                                elcDev.COL27 = (temp * ratedCapacity).ToString();

                                temp = 0.0;
                                double.TryParse(array1[7], out temp);
                                elcDev.COL28 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[8], out temp);
                                elcDev.COL29 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[9], out temp);
                                elcDev.COL30 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();

                                temp = 0.0;
                                double.TryParse(array1[10], out temp);
                                elcDev.COL31 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[11], out temp);
                                elcDev.COL32 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[12], out temp);
                                elcDev.COL33 = (temp * (devMX.ReferenceVolt)).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH2.txt"))
                    {
                    }
                    else
                    {
                        //wait.Close();
                        return false;
                    }
                    FileStream ih = new FileStream(System.Windows.Forms.Application.StartupPath + "\\IH2.txt", FileMode.Open);
                    readLine = new StreamReader(ih, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL21 = devMX.Name;
                                elcDev.COL22 = devMX.FirstNode.ToString();
                                elcDev.COL23 = devMX.LastNode.ToString();
                                elcDev.COL39 = devMX.ReferenceVolt.ToString();
                                elcDev.COL40 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL34 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL35 = (temp * Rad_to_Deg).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                }
                else if (type == 3)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF3.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\PF3.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH3.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\DH3.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH3.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\IH3.txt");
                    }
                    wForm.ShowText += "\r\n正在进行迭代\t" + System.DateTime.Now.ToString();
                    Gauss gs = new Gauss();
                    gs.CurrentCal();

                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF3.txt"))
                    {
                    }
                    else
                    {
                        //wait.Close();
                        return false;
                    }
                    FileStream pf = new FileStream(System.Windows.Forms.Application.StartupPath + "\\PF3.txt", FileMode.Open);
                    StreamReader readLine = new StreamReader(pf, Encoding.Default);
                    char[] charSplit = new char[] { ' ' };
                    string strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '01' AND Number = " + array1[0];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL41 = devMX.Name;
                                elcDev.COL59 = devMX.ReferenceVolt.ToString();
                                elcDev.COL60 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[1], out temp);
                                elcDev.COL42 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[2], out temp);
                                elcDev.COL43 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL44 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL45 = (temp * ratedCapacity).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH3.txt"))
                    {
                    }
                    else
                    {
                        // wait.Close();
                        return false;
                    }
                    FileStream dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH3.txt", FileMode.Open);
                    readLine = new StreamReader(dh, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL41 = devMX.Name;
                                elcDev.COL42 = devMX.FirstNode.ToString();
                                elcDev.COL43 = devMX.LastNode.ToString();
                                elcDev.COL59 = devMX.ReferenceVolt.ToString();
                                elcDev.COL60 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL44 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL45 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[5], out temp);
                                elcDev.COL46 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[6], out temp);
                                elcDev.COL47 = (temp * ratedCapacity).ToString();

                                temp = 0.0;
                                double.TryParse(array1[7], out temp);
                                elcDev.COL48 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[8], out temp);
                                elcDev.COL49 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[9], out temp);
                                elcDev.COL50 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();

                                temp = 0.0;
                                double.TryParse(array1[10], out temp);
                                elcDev.COL51 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[11], out temp);
                                elcDev.COL52 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[12], out temp);
                                elcDev.COL53 = (temp * (devMX.ReferenceVolt)).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH3.txt"))
                    {
                    }
                    else
                    {
                        //wait.Close();
                        return false;
                    }
                    FileStream ih = new FileStream(System.Windows.Forms.Application.StartupPath + "\\IH3.txt", FileMode.Open);
                    readLine = new StreamReader(ih, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL41 = devMX.Name;
                                elcDev.COL42 = devMX.FirstNode.ToString();
                                elcDev.COL43 = devMX.LastNode.ToString();
                                elcDev.COL59 = devMX.ReferenceVolt.ToString();
                                elcDev.COL60 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL54 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL55 = (temp * Rad_to_Deg).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                }
                else if (type == 4)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF4.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\PF4.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH4.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\DH4.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH4.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\IH4.txt");
                    }
                    wForm.ShowText += "\r\n正在进行迭代\t" + System.DateTime.Now.ToString();
                    ZYZ zy = new ZYZ();
                    zy.CurrentCal();

                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF4.txt"))
                    {
                    }
                    else
                    {
                        //wait.Close();
                        return false;
                    }
                    FileStream pf = new FileStream(System.Windows.Forms.Application.StartupPath + "\\PF4.txt", FileMode.Open);
                    StreamReader readLine = new StreamReader(pf, Encoding.Default);
                    char[] charSplit = new char[] { ' ' };
                    string strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '01' AND Number = " + array1[0];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL61 = devMX.Name;
                                elcDev.COL79 = devMX.ReferenceVolt.ToString();
                                elcDev.COL80 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[1], out temp);
                                elcDev.COL62 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[2], out temp);
                                elcDev.COL63 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL64 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL65 = (temp * ratedCapacity).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH4.txt"))
                    {
                    }
                    else
                    {
                        //wait.Close();
                        return false;
                    }
                    FileStream dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH4.txt", FileMode.Open);
                    readLine = new StreamReader(dh, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL61 = devMX.Name;
                                elcDev.COL62 = devMX.FirstNode.ToString();
                                elcDev.COL63 = devMX.LastNode.ToString();
                                elcDev.COL79 = devMX.ReferenceVolt.ToString();
                                elcDev.COL80 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL64 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL65 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[5], out temp);
                                elcDev.COL66 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[6], out temp);
                                elcDev.COL67 = (temp * ratedCapacity).ToString();

                                temp = 0.0;
                                double.TryParse(array1[7], out temp);
                                elcDev.COL68 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[8], out temp);
                                elcDev.COL69 = (temp * ratedCapacity).ToString();
                                temp = 0.0;
                                double.TryParse(array1[9], out temp);
                                elcDev.COL70 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();

                                temp = 0.0;
                                double.TryParse(array1[10], out temp);
                                elcDev.COL71 = (temp * Rad_to_Deg).ToString();
                                temp = 0.0;
                                double.TryParse(array1[11], out temp);
                                elcDev.COL72 = (temp * (devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[12], out temp);
                                elcDev.COL73 = (temp * (devMX.ReferenceVolt)).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH4.txt"))
                    {
                    }
                    else
                    {
                        //wait.Close();
                        return false;
                    }
                    FileStream ih = new FileStream(System.Windows.Forms.Application.StartupPath + "\\IH4.txt", FileMode.Open);
                    readLine = new StreamReader(ih, Encoding.Default);
                    strLine = readLine.ReadLine();
                    while (strLine != null && strLine != "")
                    {
                        string[] array1 = strLine.Split(charSplit);
                        strCon2 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                        strCon = strCon1 + strCon2;
                        PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                        if (devMX != null)
                        {
                            PSP_ElcDevice elcDev = new PSP_ElcDevice();
                            elcDev.ProjectSUID = projectSUID;
                            elcDev.DeviceSUID = devMX.SUID;
                            elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                            if (elcDev != null)
                            {
                                elcDev.COL61 = devMX.Name;
                                elcDev.COL62 = devMX.FirstNode.ToString();
                                elcDev.COL63 = devMX.LastNode.ToString();
                                elcDev.COL79 = devMX.ReferenceVolt.ToString();
                                elcDev.COL80 = ratedCapacity.ToString();
                                double temp = 0.0;
                                double.TryParse(array1[3], out temp);
                                elcDev.COL74 = (temp * ratedCapacity / (Math.Sqrt(3) * devMX.ReferenceVolt)).ToString();
                                temp = 0.0;
                                double.TryParse(array1[4], out temp);
                                elcDev.COL75 = (temp * Rad_to_Deg).ToString();
                                PSP_ElcDevice elcTemp = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);

                                Services.BaseService.Update<PSP_ElcDevice>(elcDev);
                            }
                        }
                        strLine = readLine.ReadLine();
                    }
                    readLine.Close();
                }
                //wait.Close();
            }
            catch (System.Exception ex)
            {
                //wait.Close();
                MessageBox.Show("潮流计算结果不收敛，请检查输入参数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
    }
}
