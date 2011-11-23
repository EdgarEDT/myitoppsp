using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using System.IO;
using shortcir_dll;
using ShortBuscir_dll;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using System.Windows.Forms;
using DevExpress.Utils;
using System.Threading;
namespace Itop.TLPSP.DEVICE
{
    public class ElectricShorti
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
        private int _outtype;
        //输出方式 是全部输出 还是只是输出短路电流
        public int OutType
        {
            get { return _outtype; }
            set { _outtype = value; }
        }
        public bool CheckDLL(string projectSUID, string projectid, double ratedCapacity)
        {
            double PI = Math.PI / 180.00;
            try
            {
                string strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'";
                string strCon2 = null;
                string strCon = null;
                string strData = null;
                string strBus = null;
                string strBranch = null;
                {
                    strCon2 = "AND PSPDEV.Type = '01' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listMX = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '05' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listXL = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '02' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listBYQ2 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '03' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listBYQ3 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '04' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listGen = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '08' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listCLDR = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '09' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listBlDR = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '10' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listCLDK = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '11' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listBLDK = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '12' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listFH = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '13' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listML = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '14' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listML2 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '15' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listHG = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    string linetxt = null;
                    int number = 0;

                    foreach (PSPDEV dev in listXL)
                    {

                        if (dev.KSwitchStatus == "0")
                        {

                            string con = " WHERE Name='" + dev.ISwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + dev.JSwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";

                            IList listjswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                            PSPDEV pspjswitch = (PSPDEV)listjswitch[0];
                            //strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Type = '01'AND PSPDEV.Name='" + dev.IName + "'";
                            //PSPDEV iname = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", strCon1);
                            //strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Type = '01'AND PSPDEV.Name='" + dev.JName + "'";
                            //PSPDEV jname = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", strCon1);
                            string iswitch = null;
                            string jswitch = null;
                            if (pspiswitch.KSwitchStatus == "0")
                            {
                                iswitch = "1";
                            }
                            else
                                iswitch = "0";
                            if (pspjswitch.KSwitchStatus == "0")
                            {
                                jswitch = "1";
                            }
                            else
                                jswitch = "0";
                            if (linetxt != null)
                            {
                                linetxt += "\r\n";
                            }

                            if (dev.UnitFlag == "0")
                            {
                                linetxt += dev.Name + " " + dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Number.ToString() + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + (dev.ZeroR).ToString() + " " + (dev.ZeroTQ).ToString() + " " + (dev.ZeroGNDC * 2).ToString() + " " + iswitch + " " + jswitch;
                                // linetxt += dev.Name + " " + iname.Number.ToString() + " " + jname.Number.ToString() + " " + dev.Number.ToString() + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + (dev.ZeroR).ToString() + " " + (dev.ZeroTQ).ToString() + " " + (dev.ZeroGNDC).ToString() + " " + iswitch + " " + jswitch;
                            }
                            else
                            {
                                linetxt += dev.Name + " " + dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Number.ToString() + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + (dev.ZeroR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.ZeroTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.ZeroGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + iswitch + " " + jswitch;
                                //linetxt += dev.Name + " " + iname.Number.ToString() + " " + jname.Number.ToString() + " " + dev.Number.ToString() + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + (dev.ZeroR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.ZeroTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.ZeroGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + iswitch + " " + jswitch;
                            }
                        }


                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\line.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\line.txt");
                    }
                    FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\line.txt"), FileMode.OpenOrCreate);
                    StreamWriter str1 = new StreamWriter(VK, System.Text.Encoding.Default);
                    str1.Write(linetxt);
                    str1.Close();
                    string trans2 = null;
                    foreach (PSPDEV dev in listBYQ2)
                    {
                        if (dev.KSwitchStatus == "0")
                        {

                            string con = " WHERE Name='" + dev.ISwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + dev.JSwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listjswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                            PSPDEV pspjswitch = (PSPDEV)listjswitch[0];
                            //strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Type = '01'AND PSPDEV.Name='" + dev.IName + "'";
                            //PSPDEV iname = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", strCon1);
                            //strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Type = '01'AND PSPDEV.Name='" + dev.JName + "'";
                            //PSPDEV jname = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", strCon1);
                            string iswitch = null;
                            string jswitch = null;
                            if (pspiswitch.KSwitchStatus == "0")
                            {
                                iswitch = "1";
                            }
                            else
                                iswitch = "0";
                            if (pspjswitch.KSwitchStatus == "0")
                            {
                                jswitch = "1";
                            }
                            else
                                jswitch = "0";
                            if (trans2 != null)
                            {
                                trans2 += "\r\n";
                            }
                            if (dev.UnitFlag == "0")
                            {
                                trans2 += dev.Name.ToString() + " " + dev.Number.ToString() + " " + dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.G.ToString() + " " + dev.LineGNDC.ToString() + " " + dev.K.ToString() + " " + (dev.LineLevel).ToString() + " " + dev.LineType.ToString() + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + dev.SmallTQ.ToString() + " " + dev.BigTQ.ToString() + " " + iswitch + " " + jswitch;
                                //strBranch += dev.Name.ToString() + " " + dev.Number.ToString() + " " + iname.Number.ToString() + " " + jname.Number.ToString() + " " + dev.G.ToString() + " " + dev.LineGNDC.ToString() + " " + dev.K.ToString() + " " + (dev.LineLevel).ToString() + " " + dev.LineType.ToString() + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + dev.SmallTQ.ToString() + " " + dev.BigTQ.ToString() + " " + iswitch + " " + jswitch;
                            }
                            else
                            {

                                trans2 += dev.Name + " " + dev.Number.ToString() + " " + dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + (dev.G * dev.Vib * dev.Vib) / (ratedCapacity * 1000000) + " " + (dev.LineGNDC * dev.Vib * dev.Vib) / (ratedCapacity * 1000000) + " " + dev.K + " " + dev.LineLevel + " " + dev.LineType + " " + (dev.LineR * ratedCapacity) / (dev.Vib * dev.Vib) + " " + (dev.LineTQ * ratedCapacity) / (dev.Vib * dev.Vib) + " "
                            + dev.SmallTQ * ratedCapacity / (dev.Vib * dev.Vib) + " " + dev.BigTQ * ratedCapacity / (dev.Vib * dev.Vib) + " " + iswitch + " " + jswitch;
                                // trans2 += dev.Name + " " + dev.Number.ToString() + " " + iname.Number.ToString() + " " + jname.Number.ToString() + " " + (dev.G * dev.Vib * dev.Vib) / (ratedCapacity * 1000000) + " " + (dev.LineGNDC * dev.Vib * dev.Vib) / (ratedCapacity * 1000000) + " " + dev.K + " " + dev.LineLevel + " " + dev.LineType + " " + (dev.LineR * ratedCapacity) / (dev.Vib * dev.Vib) + " " + (dev.LineTQ * ratedCapacity) / (dev.Vib * dev.Vib) + " "
                                //+ dev.SmallTQ * ratedCapacity / (dev.Vib * dev.Vib) + " " + dev.BigTQ * ratedCapacity / (dev.Vib * dev.Vib) + " " + iswitch + " " + jswitch;

                            }
                        }
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\trans2.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\trans2.txt");
                    }
                    FileStream tran = new FileStream((System.Windows.Forms.Application.StartupPath + "\\trans2.txt"), FileMode.OpenOrCreate);
                    StreamWriter str2 = new StreamWriter(tran, System.Text.Encoding.Default);
                    str2.Write(trans2);
                    str2.Close();
                    string trans3 = null;
                    foreach (PSPDEV dev in listBYQ3)
                    {
                        if (dev.KSwitchStatus == "0")
                        {

                            dev.ReferenceVolt = 1;
                            string con = " WHERE Name='" + dev.ISwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + dev.JSwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listjswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + dev.HuganLine1 + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listkswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                            PSPDEV pspjswitch = (PSPDEV)listjswitch[0];
                            PSPDEV pspkswitch = (PSPDEV)listkswitch[0];
                            //strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Type = '01'AND PSPDEV.Name='" + dev.IName + "'";
                            //PSPDEV iname = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", strCon1);
                            //strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Type = '01'AND PSPDEV.Name='" + dev.JName + "'";
                            //PSPDEV jname = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", strCon1);
                            //strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Type = '01'AND PSPDEV.Name='" + dev.KName + "'";
                            //PSPDEV kname = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", strCon1);
                            string iswitch = null;
                            string jswitch = null;
                            string kswitch = null;
                            if (pspiswitch.KSwitchStatus == "0")
                            {
                                iswitch = "1";
                            }
                            else
                                iswitch = "0";
                            if (pspjswitch.KSwitchStatus == "0")
                            {
                                jswitch = "1";
                            }
                            else
                                jswitch = "0";
                            if (pspkswitch.KSwitchStatus == "0")
                            {
                                kswitch = "1";
                            }
                            else
                                kswitch = "0";
                            if (trans3 != null)
                            {
                                trans3 += "\r\n";
                            }
                            if (dev.UnitFlag == "0")
                            {
                                trans3 += dev.Name + " " + dev.Number.ToString() + " " + dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + dev.G.ToString() + " " + dev.LineGNDC.ToString() + " " + dev.K + " " + dev.StandardCurrent + " " + dev.BigP + " " + iswitch + " " + jswitch + " " + kswitch + " " + dev.HuganTQ1.ToString() + " " + dev.HuganTQ4.ToString() + " " + dev.HuganTQ2.ToString() + " " + dev.HuganTQ5.ToString() + " " + dev.HuganTQ3.ToString() + " " + dev.ZeroTQ.ToString() + " " + dev.SmallTQ.ToString() + " " + dev.BigTQ.ToString() + " " + dev.LineLevel + " " + dev.LineType + " " + dev.LineStatus;
                                //trans3 += dev.Name + " " + dev.Number.ToString() + " " + iname.Number.ToString() + " " + jname.Number.ToString() + " " + kname.Number.ToString() + " " + dev.G.ToString() + " " + dev.LineGNDC.ToString() + " " + dev.K + " " + dev.StandardCurrent + " " + dev.BigP + " " + iswitch + " " + jswitch + " " + kswitch + " " + dev.HuganTQ1.ToString() + " " + dev.HuganTQ4.ToString() + " " + dev.HuganTQ2.ToString() + " " + dev.HuganTQ5.ToString() + " " + dev.HuganTQ3.ToString() + " " + dev.HuganFirst.ToString() + " " + dev.SmallTQ.ToString() + " " + dev.BigTQ.ToString() + " " + dev.LineLevel + " " + dev.LineType + " " + dev.LineStatus;
                            }
                            else
                            {
                                trans3 += dev.Name + " " + dev.Number.ToString() + " " + dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + (dev.G * dev.ReferenceVolt * dev.ReferenceVolt) / (ratedCapacity * 1000000) + " " + (dev.LineGNDC * dev.ReferenceVolt * dev.ReferenceVolt) / (ratedCapacity * 1000000) + " " + dev.K + " " + dev.StandardCurrent + " " + dev.BigP + " " + iswitch + " " + jswitch + " " + kswitch + " " + (dev.HuganTQ1 * ratedCapacity) / (dev.Vib * dev.Vib) + " " + (dev.HuganTQ4 * ratedCapacity) / (dev.Vib * dev.Vib) +
                               " " + (dev.HuganTQ2 * ratedCapacity) / (dev.Vjb * dev.Vjb) + " " + dev.HuganTQ5 * ratedCapacity / (dev.Vjb * dev.Vjb) + " " + (dev.HuganTQ3 * ratedCapacity) / (dev.Vkb * dev.Vkb) + " " + (dev.ZeroTQ * ratedCapacity) / (dev.Vkb * dev.Vkb) + " " + dev.SmallTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt) + " " + dev.BigTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt) + " " + dev.LineLevel + " " + dev.LineType + " " + dev.LineStatus;
                                //trans3 += dev.Name + " " + dev.Number.ToString() + " " + iname.Number.ToString() + " " + jname.Number.ToString() + " " + kname.Number.ToString() + " " + (dev.G * dev.ReferenceVolt * dev.ReferenceVolt) / (ratedCapacity * 1000000) + " " + (dev.LineGNDC * dev.ReferenceVolt * dev.ReferenceVolt) / (ratedCapacity * 1000000) + " " + dev.K + " " + dev.StandardCurrent + " " + dev.BigP + " " + iswitch + " " + jswitch + " " + kswitch + " " + (dev.HuganTQ1 * ratedCapacity) / (dev.Vib * dev.Vib) + " " + (dev.HuganTQ4 * ratedCapacity) / (dev.Vib * dev.Vib) +
                                //" " + (dev.HuganTQ2 * ratedCapacity) / (dev.Vjb * dev.Vjb) + " " + dev.HuganTQ5 * ratedCapacity / (dev.Vjb * dev.Vjb) + " " + (dev.HuganTQ3 * ratedCapacity) / (dev.Vkb * dev.Vkb) + " " + ((double)dev.HuganFirst * ratedCapacity) / (dev.Vkb * dev.Vkb) + " " + dev.SmallTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt) + " " + dev.BigTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt) + " " + dev.LineLevel + " " + dev.LineType + " " + dev.LineStatus;
                            }
                        }
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\trans3.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\trans3.txt");
                    }
                    FileStream tranThrid = new FileStream((System.Windows.Forms.Application.StartupPath + "\\trans3.txt"), FileMode.OpenOrCreate);
                    StreamWriter str3 = new StreamWriter(tranThrid, System.Text.Encoding.Default);
                    str3.Write(trans3);
                    str3.Close();
                    string capacitor_earth = null;
                    foreach (PSPDEV dev in listBlDR)
                    {
                        if (dev.KSwitchStatus == "0")
                        {

                            string con = " WHERE Name='" + dev.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                            string iswitch = null;

                            if (capacitor_earth != null)
                            {
                                capacitor_earth += "\r\n";
                            }
                            if (dev.UnitFlag == "0")
                            {
                                capacitor_earth += dev.Name + " " + dev.Number.ToString() + " " + pspiswitch.Number.ToString() + " " + dev.LineTQ.ToString() + " " + 1;
                            }
                            else
                                capacitor_earth += dev.Name + " " + dev.Number.ToString() + " " + pspiswitch.Number.ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + 1;
                        }
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\capacitor_earth.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\capacitor_earth.txt");
                    }
                    FileStream capacitor = new FileStream((System.Windows.Forms.Application.StartupPath + "\\capacitor_earth.txt"), FileMode.OpenOrCreate);
                    StreamWriter str4 = new StreamWriter(capacitor, System.Text.Encoding.Default);
                    str4.Write(capacitor_earth);
                    str4.Close();
                    string inductor_earth = null;
                    foreach (PSPDEV dev in listBLDK)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            string con = " WHERE Name='" + dev.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                            string iswitch = null;

                            if (inductor_earth != null)
                            {
                                inductor_earth += "\r\n";
                            }
                            if (dev.UnitFlag == "0")
                            {
                                inductor_earth += dev.Name + " " + dev.Number.ToString() + " " + pspiswitch.Number.ToString() + " " + dev.LineTQ.ToString() + " " + 1;
                            }
                            else
                                inductor_earth += dev.Name + " " + dev.Number.ToString() + " " + pspiswitch.Number.ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + 1;
                        }
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\inductor_earth.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\inductor_earth.txt");
                    }
                    FileStream inductor = new FileStream((System.Windows.Forms.Application.StartupPath + "\\inductor_earth.txt"), FileMode.OpenOrCreate);
                    StreamWriter str5 = new StreamWriter(inductor, System.Text.Encoding.Default);
                    str5.Write(inductor_earth);
                    str5.Close();
                    string capacitor_line = null;
                    foreach (PSPDEV dev in listCLDR)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            string con = " WHERE Name='" + dev.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];

                            if (capacitor_line != null)
                            {
                                capacitor_line += "\r\n";
                            }
                            if (dev.UnitFlag == "0")
                            {
                                capacitor_line += dev.Name + " " + dev.Number.ToString() + " " + dev.FirstNode + " " + dev.LastNode + " " + pspiswitch.Number + " " + dev.LineTQ.ToString() + " " + "1";
                            }
                            else
                                capacitor_line += dev.Name + " " + dev.Number + " " + dev.FirstNode + " " + dev.LastNode + " " + pspiswitch.Number + " " + ratedCapacity * 1000000 / (dev.LineTQ * 314 * dev.ReferenceVolt * dev.ReferenceVolt) + " " + "1";
                        }
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\capacitor_line.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\capacitor_line.txt");
                    }
                    FileStream cap = new FileStream((System.Windows.Forms.Application.StartupPath + "\\capacitor_line.txt"), FileMode.OpenOrCreate);
                    StreamWriter str6 = new StreamWriter(cap, System.Text.Encoding.Default);
                    str6.Write(capacitor_line);
                    str6.Close();
                    string inductor_line = null;
                    foreach (PSPDEV dev in listCLDK)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            string con = " WHERE Name='" + dev.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];

                            if (inductor_line != null)
                            {
                                inductor_line += "\r\n";
                            }
                            if (dev.UnitFlag == "0")
                            {
                                inductor_line += dev.Name + " " + dev.Number.ToString() + " " + dev.FirstNode + " " + dev.LastNode + " " + pspiswitch.Number + " " + dev.LineTQ.ToString() + " " + "1";
                            }
                            else
                                inductor_line += dev.Name + " " + dev.Number + " " + dev.FirstNode + " " + dev.LastNode + " " + pspiswitch.Number + " " + dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt) + " " + "1";
                        }
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\inductor_line.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\inductor_line.txt");
                    }
                    FileStream ind = new FileStream((System.Windows.Forms.Application.StartupPath + "\\inductor_line.txt"), FileMode.OpenOrCreate);
                    StreamWriter str7 = new StreamWriter(ind, System.Text.Encoding.Default);
                    str7.Write(inductor_line);
                    str7.Close();
                    string loadline = null;
                    int count = 0;
                    foreach (PSPDEV dev in listFH)
                    {
                        if (dev.KSwitchStatus == "0")
                        {

                            count++;
                            string con = " WHERE  Name='" + dev.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                            con = " WHERE  DeviceSUID='" + pspiswitch.SUID + "' AND ProjectSUID = '" + projectSUID + "'";
                            IList listDEVICE = UCDeviceBase.DataService.GetList("SelectPSP_ElcDeviceByCondition", con);
                            PSP_ElcDevice device = (PSP_ElcDevice)listDEVICE[0];

                            if (loadline != null)
                            {
                                loadline += "\r\n";
                            }
                            if (dev.UnitFlag == "0")
                            {
                                loadline += dev.Name + " " + dev.Number.ToString() + " " + pspiswitch.Number + " " + dev.InPutP + " " + dev.InPutQ + " " + Convert.ToDouble(device.COL2) / dev.ReferenceVolt + " " + "1" + " " + Convert.ToDouble(device.COL3) + " " + dev.Vipos + " " + dev.HuganTQ2 + " " + dev.HuganTQ5 + " " + dev.HuganTQ4 + " " + dev.SkN + " " + dev.P0 + " " + dev.NodeType;
                            }
                            else
                                loadline += dev.Name + " " + dev.Number.ToString() + " " + pspiswitch.Number + " " + dev.InPutP / ratedCapacity + " " + dev.InPutQ / ratedCapacity + " " + Convert.ToDouble(device.COL2) / dev.ReferenceVolt + " " + "1" + " " + Convert.ToDouble(device.COL3) + " " + dev.Vipos + " " + dev.HuganTQ2 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt) + " " + dev.HuganTQ5 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt) + " " + dev.HuganTQ4 * 100 / (dev.ReferenceVolt * dev.ReferenceVolt) + " " + dev.SkN + " " + dev.P0 + " " + dev.NodeType;
                        }
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Load.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\Load.txt");
                    }
                    FileStream load = new FileStream((System.Windows.Forms.Application.StartupPath + "\\Load.txt"), FileMode.OpenOrCreate);
                    StreamWriter str8 = new StreamWriter(load, System.Text.Encoding.Default);
                    str8.Write(loadline);
                    str8.Close();
                    string genline = null;
                    foreach (PSPDEV dev in listGen)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            string con = " WHERE Name='" + dev.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                            con = " WHERE  DeviceSUID='" + pspiswitch.SUID + "' AND ProjectSUID = '" + projectSUID + "'";
                            IList listDEVICE = UCDeviceBase.DataService.GetList("SelectPSP_ElcDeviceByCondition", con);
                            PSP_ElcDevice device = (PSP_ElcDevice)listDEVICE[0];
                            if (genline != null)
                            {
                                genline += "\r\n";
                            }
                            if (dev.UnitFlag == "0")
                            {
                                genline += dev.Name + " " + dev.Number.ToString() + " " + pspiswitch.Number + " " + "1" + " " + dev.SjN.ToString() + " " + dev.PositiveTQ.ToString() + " " + dev.SkN.ToString() + " " + dev.OutP + " " + dev.OutQ + " " + Convert.ToDouble(device.COL2) / dev.ReferenceVolt + " " + Convert.ToDouble(device.COL3) + " " + dev.Vkb;
                            }
                            else
                                genline += dev.Name + " " + dev.Number + " " + pspiswitch.Number + " " + "1" + " " + dev.SjN * (dev.RateVolt * dev.RateVolt) * ratedCapacity / (dev.Vkb * dev.RateVolt * dev.RateVolt) + " " + dev.PositiveTQ * (dev.RateVolt * dev.RateVolt) * ratedCapacity / (dev.Vkb * dev.RateVolt * dev.RateVolt) + " " + dev.SkN * (dev.RateVolt * dev.RateVolt) * ratedCapacity / (dev.Vkb * dev.RateVolt * dev.RateVolt) + " " + dev.OutP / ratedCapacity + " " + dev.OutQ / ratedCapacity + " " + Convert.ToDouble(device.COL2) / dev.ReferenceVolt + " " + Convert.ToDouble(device.COL3) + " " + dev.Vkb / ratedCapacity;
                        }
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\gen.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\gen.txt");
                    }
                    FileStream gen = new FileStream((System.Windows.Forms.Application.StartupPath + "\\gen.txt"), FileMode.OpenOrCreate);
                    StreamWriter str9 = new StreamWriter(gen, System.Text.Encoding.Default);
                    str9.Write(genline);
                    str9.Close();
                    string mulian = null;
                    foreach (PSPDEV dev in listML)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            string con = " WHERE Name='" + dev.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                            con = " WHERE Name='" + dev.JName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            IList listjswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspjswitch = (PSPDEV)listjswitch[0];
                            if (mulian != null)
                            {
                                mulian += "\r\n";
                            }
                            mulian += dev.Number + " " + pspiswitch.Number + " " + pspjswitch.Number + " " + "1";
                        }
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\mulian.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\mulian.txt");
                    }
                    FileStream mu = new FileStream((System.Windows.Forms.Application.StartupPath + "\\mulian.txt"), FileMode.OpenOrCreate);
                    StreamWriter str10 = new StreamWriter(mu, System.Text.Encoding.Default);
                    str10.Write(mulian);
                    str10.Close();
                    string mulian23 = null;
                    foreach (PSPDEV dev in listML2)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            string con = " WHERE Name='" + dev.HuganLine1 + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            IList listinode = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspinode = (PSPDEV)listinode[0];
                            con = " WHERE Name='" + dev.HuganLine2 + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            IList listjnode = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspjnode = (PSPDEV)listjnode[0];
                            con = " WHERE Name='" + dev.HuganLine3 + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
                            IList listiline = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiline = (PSPDEV)listinode[0];
                            con = " WHERE Name='" + dev.HuganLine4 + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
                            IList listjline = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspjline = (PSPDEV)listjnode[0];
                            con = " WHERE Name='" + dev.KName + "' AND ProjectID = '" + projectid + "'" + "AND Type='13'";
                            IList listiload = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiload = (PSPDEV)listiload[0];
                            con = " WHERE Name='" + dev.KSwitchStatus + "' AND ProjectID = '" + projectid + "'" + "AND Type='13'";
                            IList listjload = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspjload = (PSPDEV)listjload[0];
                            string switch1 = "0";
                            if (dev.LineLevel == "0")
                            {
                                switch1 = "1";
                            }
                            string switch2 = "0";
                            if (dev.LineType == "0")
                            {
                                switch2 = "1";
                            }
                            string switch3 = "0";
                            if (dev.LineStatus == "0")
                            {
                                switch3 = "1";
                            }
                            if (mulian23 != null)
                            {
                                mulian23 += "\r\n";
                            }
                            mulian23 += dev.Number + " " + pspinode.Number + " " + pspjnode.Number + " " + pspiline.Number + " " + pspiload.Number + " " + pspjline.Number + " " + pspjload.Number + " " + switch1 + " " + switch2 + " " + switch3;
                        }
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\mulian23.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\mulian23.txt");
                    }
                    FileStream mul = new FileStream((System.Windows.Forms.Application.StartupPath + "\\mulian23.txt"), FileMode.OpenOrCreate);
                    StreamWriter str11 = new StreamWriter(mul, System.Text.Encoding.Default);
                    str11.Write(mulian23);
                    str11.Close();
                    string mutl_ind = null;
                    foreach (PSPDEV dev in listHG)
                    {

                        string con = " WHERE Name='" + dev.HuganLine1 + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
                        IList listiline = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                        PSPDEV pspiline = (PSPDEV)listiline[0];
                        con = " WHERE Name='" + dev.HuganLine2 + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
                        IList listjline = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                        PSPDEV pspjline = (PSPDEV)listjline[0];


                        if (mutl_ind != null)
                        {
                            mutl_ind += "\r\n";
                        }
                        if (dev.UnitFlag == "0")
                        {
                            mutl_ind += dev.Number + " " + pspiline.Number + " " + pspjline.Number + " " + dev.HuganTQ1;
                        }
                        else

                            mutl_ind += dev.Number + " " + pspiline.Number + " " + pspjline.Number + " " + dev.HuganTQ1 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt);

                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\mutl_ind.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\mutl_ind.txt");
                    }
                    FileStream Hu = new FileStream((System.Windows.Forms.Application.StartupPath + "\\mutl_ind.txt"), FileMode.OpenOrCreate);
                    StreamWriter str13 = new StreamWriter(Hu, System.Text.Encoding.Default);
                    str13.Write(mutl_ind);
                    str13.Close();
                    string bus = null;
                    int fautnumber = 0;
                    string conM = null;
                    foreach (PSPDEV dev in listMX)
                    {
                        conM = " WHERE  DeviceSUID='" + dev.SUID + "' AND ProjectSUID = '" + projectSUID + "'";
                        IList listDEVICE = UCDeviceBase.DataService.GetList("SelectPSP_ElcDeviceByCondition", conM);
                        PSP_ElcDevice device = (PSP_ElcDevice)listDEVICE[0];
                        if (dev.KSwitchStatus == "0")
                        {
                            fautnumber++;
                            if (bus != null)
                            {
                                bus += "\r\n";
                            }

                            if (dev.UnitFlag == "0")
                            {
                                bus += dev.Name + " " + dev.Number + " " + Convert.ToDouble(device.COL2) / dev.ReferenceVolt + " " + Convert.ToDouble(device.COL3) + " " + dev.ReferenceVolt;
                            }
                            else
                            {
                                bus += dev.Name + " " + dev.Number + " " + Convert.ToDouble(device.COL2) / dev.ReferenceVolt + " " + Convert.ToDouble(device.COL3) + " " + dev.ReferenceVolt;
                            }
                        }
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\bus.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\bus.txt");
                    }
                    FileStream bu = new FileStream((System.Windows.Forms.Application.StartupPath + "\\bus.txt"), FileMode.OpenOrCreate);
                    StreamWriter str12 = new StreamWriter(bu, System.Text.Encoding.Default);
                    str12.Write(bus);
                    str12.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Zmatrixcheck.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\Zmatrixcheck.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Fmatrixcheck.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\Fmatrixcheck.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Lmatrixcheck.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\Lmatrixcheck.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt");
                    }

                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("数据存在问题请输入完全后再操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
        public bool CheckDL(string projectSUID, string projectid, double ratedCapacity)
        {

            try
            {
                string strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'";
                string strCon2 = null;
                string strCon = null;
                string strData = null;
                string strBus = null;
                string strBranch = null;
                {
                    strCon2 = "AND PSPDEV.Type = '01' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listMX = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '05' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listXL = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '02' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listBYQ2 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '03' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listBYQ3 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '04' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listGen = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '08' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listCLDR = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '09' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listBlDR = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '10' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listCLDK = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '11' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listBLDK = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '12' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listFH = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '13' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listML = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '14' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listML2 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "AND PSPDEV.Type = '15' ORDER BY PSPDEV.Number";
                    strCon = strCon1 + strCon2;
                    IList listHG = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
                    string linetxt = null;
                    int number = 0;

                    foreach (PSPDEV dev in listXL)
                    {

                        if (dev.KSwitchStatus == "0")
                        {

                            string con = " WHERE Name='" + dev.ISwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + dev.JSwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";

                            IList listjswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                            PSPDEV pspjswitch = (PSPDEV)listjswitch[0];
                            //strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Type = '01'AND PSPDEV.Name='" + dev.IName + "'";
                            //PSPDEV iname = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", strCon1);
                            //strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Type = '01'AND PSPDEV.Name='" + dev.JName + "'";
                            //PSPDEV jname = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", strCon1);
                            string iswitch = null;
                            string jswitch = null;
                            if (pspiswitch.KSwitchStatus == "0")
                            {
                                iswitch = "1";
                            }
                            else
                                iswitch = "0";
                            if (pspjswitch.KSwitchStatus == "0")
                            {
                                jswitch = "1";
                            }
                            else
                                jswitch = "0";
                            if (linetxt != null)
                            {
                                linetxt += "\r\n";
                            }

                            if (dev.UnitFlag == "0")
                            {
                                linetxt += dev.Name + " " + dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Number.ToString() + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + (dev.ZeroR).ToString() + " " + (dev.ZeroTQ).ToString() + " " + (dev.ZeroGNDC * 2).ToString() + " " + iswitch + " " + jswitch;
                                // linetxt += dev.Name + " " + iname.Number.ToString() + " " + jname.Number.ToString() + " " + dev.Number.ToString() + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + (dev.ZeroR).ToString() + " " + (dev.ZeroTQ).ToString() + " " + (dev.ZeroGNDC).ToString() + " " + iswitch + " " + jswitch;
                            }
                            else
                            {
                                linetxt += dev.Name + " " + dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Number.ToString() + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + (dev.ZeroR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.ZeroTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.ZeroGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + iswitch + " " + jswitch;
                                //linetxt += dev.Name + " " + iname.Number.ToString() + " " + jname.Number.ToString() + " " + dev.Number.ToString() + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + (dev.ZeroR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.ZeroTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.ZeroGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + iswitch + " " + jswitch;
                            }
                        }


                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\line.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\line.txt");
                    }
                    FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\line.txt"), FileMode.OpenOrCreate);
                    StreamWriter str1 = new StreamWriter(VK, System.Text.Encoding.Default);
                    str1.Write(linetxt);
                    str1.Close();
                    string trans2 = null;
                    foreach (PSPDEV dev in listBYQ2)
                    {
                        if (dev.KSwitchStatus == "0")
                        {

                            string con = " WHERE Name='" + dev.ISwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + dev.JSwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listjswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                            PSPDEV pspjswitch = (PSPDEV)listjswitch[0];
                            //strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Type = '01'AND PSPDEV.Name='" + dev.IName + "'";
                            //PSPDEV iname = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", strCon1);
                            //strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Type = '01'AND PSPDEV.Name='" + dev.JName + "'";
                            //PSPDEV jname = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", strCon1);
                            string iswitch = null;
                            string jswitch = null;
                            if (pspiswitch.KSwitchStatus == "0")
                            {
                                iswitch = "1";
                            }
                            else
                                iswitch = "0";
                            if (pspjswitch.KSwitchStatus == "0")
                            {
                                jswitch = "1";
                            }
                            else
                                jswitch = "0";
                            if (trans2 != null)
                            {
                                trans2 += "\r\n";
                            }
                            if (dev.UnitFlag == "0")
                            {
                                trans2 += dev.Name.ToString() + " " + dev.Number.ToString() + " " + dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.G.ToString() + " " + dev.LineGNDC.ToString() + " " + dev.K.ToString() + " " + (dev.LineLevel).ToString() + " " + dev.LineType.ToString() + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + dev.SmallTQ.ToString() + " " + dev.BigTQ.ToString() + " " + iswitch + " " + jswitch;
                                //strBranch += dev.Name.ToString() + " " + dev.Number.ToString() + " " + iname.Number.ToString() + " " + jname.Number.ToString() + " " + dev.G.ToString() + " " + dev.LineGNDC.ToString() + " " + dev.K.ToString() + " " + (dev.LineLevel).ToString() + " " + dev.LineType.ToString() + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + dev.SmallTQ.ToString() + " " + dev.BigTQ.ToString() + " " + iswitch + " " + jswitch;
                            }
                            else
                            {

                                trans2 += dev.Name + " " + dev.Number.ToString() + " " + dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + (dev.G * dev.Vib * dev.Vib) / (ratedCapacity * 1000000) + " " + (dev.LineGNDC * dev.Vib * dev.Vib) / (ratedCapacity * 1000000) + " " + dev.K + " " + dev.LineLevel + " " + dev.LineType + " " + (dev.LineR * ratedCapacity) / (dev.Vib * dev.Vib) + " " + (dev.LineTQ * ratedCapacity) / (dev.Vib * dev.Vib) + " "
                            + dev.SmallTQ * ratedCapacity / (dev.Vib * dev.Vib) + " " + dev.BigTQ * ratedCapacity / (dev.Vib * dev.Vib) + " " + iswitch + " " + jswitch;
                                // trans2 += dev.Name + " " + dev.Number.ToString() + " " + iname.Number.ToString() + " " + jname.Number.ToString() + " " + (dev.G * dev.Vib * dev.Vib) / (ratedCapacity * 1000000) + " " + (dev.LineGNDC * dev.Vib * dev.Vib) / (ratedCapacity * 1000000) + " " + dev.K + " " + dev.LineLevel + " " + dev.LineType + " " + (dev.LineR * ratedCapacity) / (dev.Vib * dev.Vib) + " " + (dev.LineTQ * ratedCapacity) / (dev.Vib * dev.Vib) + " "
                                //+ dev.SmallTQ * ratedCapacity / (dev.Vib * dev.Vib) + " " + dev.BigTQ * ratedCapacity / (dev.Vib * dev.Vib) + " " + iswitch + " " + jswitch;

                            }
                        }
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\trans2.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\trans2.txt");
                    }
                    FileStream tran = new FileStream((System.Windows.Forms.Application.StartupPath + "\\trans2.txt"), FileMode.OpenOrCreate);
                    StreamWriter str2 = new StreamWriter(tran, System.Text.Encoding.Default);
                    str2.Write(trans2);
                    str2.Close();
                    string trans3 = null;
                   
                    foreach (PSPDEV dev in listBYQ3)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                        
                            dev.ReferenceVolt = 1;
                            string con = " WHERE Name='" + dev.ISwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + dev.JSwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listjswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + dev.HuganLine1 + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listkswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                            PSPDEV pspjswitch = (PSPDEV)listjswitch[0];
                            PSPDEV pspkswitch = (PSPDEV)listkswitch[0];
                            //strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Type = '01'AND PSPDEV.Name='" + dev.IName + "'";
                            //PSPDEV iname = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", strCon1);
                            //strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Type = '01'AND PSPDEV.Name='" + dev.JName + "'";
                            //PSPDEV jname = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", strCon1);
                            //strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Type = '01'AND PSPDEV.Name='" + dev.KName + "'";
                            //PSPDEV kname = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", strCon1);
                            string iswitch = null;
                            string jswitch = null;
                            string kswitch = null;
                            if (pspiswitch.KSwitchStatus == "0")
                            {
                                iswitch = "1";
                            }
                            else
                                iswitch = "0";
                            if (pspjswitch.KSwitchStatus == "0")
                            {
                                jswitch = "1";
                            }
                            else
                                jswitch = "0";
                            if (pspkswitch.KSwitchStatus == "0")
                            {
                                kswitch = "1";
                            }
                            else
                                kswitch = "0";
                            if (trans3 != null)
                            {
                                trans3 += "\r\n";
                            }
                            if (dev.UnitFlag == "0")
                            {
                                trans3 += dev.Name + " " + dev.Number.ToString() + " " + dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + dev.G.ToString() + " " + dev.LineGNDC.ToString() + " " + dev.K + " " + dev.StandardCurrent + " " + dev.BigP + " " + iswitch + " " + jswitch + " " + kswitch + " " + dev.HuganTQ1.ToString() + " " + dev.HuganTQ4.ToString() + " " + dev.HuganTQ2.ToString() + " " + dev.HuganTQ5.ToString() + " " + dev.HuganTQ3.ToString() + " " + dev.ZeroTQ.ToString() + " " + dev.SmallTQ.ToString() + " " + dev.BigTQ.ToString() + " " + dev.LineLevel + " " + dev.LineType + " " + dev.LineStatus;
                                //trans3 += dev.Name + " " + dev.Number.ToString() + " " + iname.Number.ToString() + " " + jname.Number.ToString() + " " + kname.Number.ToString() + " " + dev.G.ToString() + " " + dev.LineGNDC.ToString() + " " + dev.K + " " + dev.StandardCurrent + " " + dev.BigP + " " + iswitch + " " + jswitch + " " + kswitch + " " + dev.HuganTQ1.ToString() + " " + dev.HuganTQ4.ToString() + " " + dev.HuganTQ2.ToString() + " " + dev.HuganTQ5.ToString() + " " + dev.HuganTQ3.ToString() + " " + dev.HuganFirst.ToString() + " " + dev.SmallTQ.ToString() + " " + dev.BigTQ.ToString() + " " + dev.LineLevel + " " + dev.LineType + " " + dev.LineStatus;
                            }
                            else
                            {
                                trans3 += dev.Name + " " + dev.Number.ToString() + " " + dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + (dev.G * dev.ReferenceVolt * dev.ReferenceVolt) / (ratedCapacity * 1000000) + " " + (dev.LineGNDC * dev.ReferenceVolt * dev.ReferenceVolt) / (ratedCapacity * 1000000) + " " + dev.K + " " + dev.StandardCurrent + " " + dev.BigP + " " + iswitch + " " + jswitch + " " + kswitch + " " + (dev.HuganTQ1 * ratedCapacity) / (dev.Vib * dev.Vib) + " " + (dev.HuganTQ4 * ratedCapacity) / (dev.Vib * dev.Vib) +
                               " " + (dev.HuganTQ2 * ratedCapacity) / (dev.Vjb * dev.Vjb) + " " + dev.HuganTQ5 * ratedCapacity / (dev.Vjb * dev.Vjb) + " " + (dev.HuganTQ3 * ratedCapacity) / (dev.Vkb * dev.Vkb) + " " + (dev.ZeroTQ * ratedCapacity) / (dev.Vkb * dev.Vkb) + " " + dev.SmallTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt) + " " + dev.BigTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt) + " " + dev.LineLevel + " " + dev.LineType + " " + dev.LineStatus;
                                //trans3 += dev.Name + " " + dev.Number.ToString() + " " + iname.Number.ToString() + " " + jname.Number.ToString() + " " + kname.Number.ToString() + " " + (dev.G * dev.ReferenceVolt * dev.ReferenceVolt) / (ratedCapacity * 1000000) + " " + (dev.LineGNDC * dev.ReferenceVolt * dev.ReferenceVolt) / (ratedCapacity * 1000000) + " " + dev.K + " " + dev.StandardCurrent + " " + dev.BigP + " " + iswitch + " " + jswitch + " " + kswitch + " " + (dev.HuganTQ1 * ratedCapacity) / (dev.Vib * dev.Vib) + " " + (dev.HuganTQ4 * ratedCapacity) / (dev.Vib * dev.Vib) +
                                //" " + (dev.HuganTQ2 * ratedCapacity) / (dev.Vjb * dev.Vjb) + " " + dev.HuganTQ5 * ratedCapacity / (dev.Vjb * dev.Vjb) + " " + (dev.HuganTQ3 * ratedCapacity) / (dev.Vkb * dev.Vkb) + " " + ((double)dev.HuganFirst * ratedCapacity) / (dev.Vkb * dev.Vkb) + " " + dev.SmallTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt) + " " + dev.BigTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt) + " " + dev.LineLevel + " " + dev.LineType + " " + dev.LineStatus;
                            }
                        }
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\trans3.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\trans3.txt");
                    }
                    FileStream tranThrid = new FileStream((System.Windows.Forms.Application.StartupPath + "\\trans3.txt"), FileMode.OpenOrCreate);
                    StreamWriter str3 = new StreamWriter(tranThrid, System.Text.Encoding.Default);
                    str3.Write(trans3);
                    str3.Close();
                    string capacitor_earth = null;
                    foreach (PSPDEV dev in listBlDR)
                    {
                        if (dev.KSwitchStatus == "0")
                        {

                            string con = " WHERE Name='" + dev.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                            string iswitch = null;

                            if (capacitor_earth != null)
                            {
                                capacitor_earth += "\r\n";
                            }
                            if (dev.UnitFlag == "0")
                            {
                                capacitor_earth += dev.Name + " " + dev.Number.ToString() + " " + pspiswitch.Number.ToString() + " " + dev.LineTQ.ToString() + " " + 1;
                            }
                            else
                                capacitor_earth += dev.Name + " " + dev.Number.ToString() + " " + pspiswitch.Number.ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + 1;
                        }
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\capacitor_earth.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\capacitor_earth.txt");
                    }
                    FileStream capacitor = new FileStream((System.Windows.Forms.Application.StartupPath + "\\capacitor_earth.txt"), FileMode.OpenOrCreate);
                    StreamWriter str4 = new StreamWriter(capacitor, System.Text.Encoding.Default);
                    str4.Write(capacitor_earth);
                    str4.Close();
                    string inductor_earth = null;
                    foreach (PSPDEV dev in listBLDK)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            string con = " WHERE Name='" + dev.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                            string iswitch = null;

                            if (inductor_earth != null)
                            {
                                inductor_earth += "\r\n";
                            }
                            if (dev.UnitFlag == "0")
                            {
                                inductor_earth += dev.Name + " " + dev.Number.ToString() + " " + pspiswitch.Number.ToString() + " " + dev.LineTQ.ToString() + " " + 1;
                            }
                            else
                                inductor_earth += dev.Name + " " + dev.Number.ToString() + " " + pspiswitch.Number.ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + 1;
                        }
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\inductor_earth.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\inductor_earth.txt");
                    }
                    FileStream inductor = new FileStream((System.Windows.Forms.Application.StartupPath + "\\inductor_earth.txt"), FileMode.OpenOrCreate);
                    StreamWriter str5 = new StreamWriter(inductor, System.Text.Encoding.Default);
                    str5.Write(inductor_earth);
                    str5.Close();
                    string capacitor_line = null;
                    foreach (PSPDEV dev in listCLDR)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            string con = " WHERE Name='" + dev.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];

                            if (capacitor_line != null)
                            {
                                capacitor_line += "\r\n";
                            }
                            if (dev.UnitFlag == "0")
                            {
                                capacitor_line += dev.Name + " " + dev.Number.ToString() + " " + dev.FirstNode + " " + dev.LastNode + " " + pspiswitch.Number + " " + dev.LineTQ.ToString() + " " + "1";
                            }
                            else
                                capacitor_line += dev.Name + " " + dev.Number + " " + dev.FirstNode + " " + dev.LastNode + " " + pspiswitch.Number + " " + ratedCapacity * 1000000 / (dev.LineTQ * 314 * dev.ReferenceVolt * dev.ReferenceVolt) + " " + "1";
                        }
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\capacitor_line.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\capacitor_line.txt");
                    }
                    FileStream cap = new FileStream((System.Windows.Forms.Application.StartupPath + "\\capacitor_line.txt"), FileMode.OpenOrCreate);
                    StreamWriter str6 = new StreamWriter(cap, System.Text.Encoding.Default);
                    str6.Write(capacitor_line);
                    str6.Close();
                    string inductor_line = null;
                    foreach (PSPDEV dev in listCLDK)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            string con = " WHERE Name='" + dev.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];

                            if (inductor_line != null)
                            {
                                inductor_line += "\r\n";
                            }
                            if (dev.UnitFlag == "0")
                            {
                                inductor_line += dev.Name + " " + dev.Number.ToString() + " " + dev.FirstNode + " " + dev.LastNode + " " + pspiswitch.Number + " " + dev.LineTQ.ToString() + " " + "1";
                            }
                            else
                                inductor_line += dev.Name + " " + dev.Number + " " + dev.FirstNode + " " + dev.LastNode + " " + pspiswitch.Number + " " + dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt) + " " + "1";
                        }
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\inductor_line.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\inductor_line.txt");
                    }
                    FileStream ind = new FileStream((System.Windows.Forms.Application.StartupPath + "\\inductor_line.txt"), FileMode.OpenOrCreate);
                    StreamWriter str7 = new StreamWriter(ind, System.Text.Encoding.Default);
                    str7.Write(inductor_line);
                    str7.Close();
                    string loadline = null;
                    foreach (PSPDEV dev in listFH)
                    {
                        if (dev.KSwitchStatus == "0")
                        {

                            string con = " WHERE Name='" + dev.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];

                            if (loadline != null)
                            {
                                loadline += "\r\n";
                            }
                            if (dev.UnitFlag == "0")
                            {
                                loadline += dev.Name + " " + dev.Number.ToString() + " " + pspiswitch.Number + " " + dev.InPutP + " " + dev.InPutQ + " " + dev.VoltR / dev.ReferenceVolt + " " + "1" + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + "0";
                            }
                            else
                                loadline += dev.Name + " " + dev.Number + " " + pspiswitch.Number + " " + dev.InPutP / ratedCapacity + " " + dev.InPutQ / ratedCapacity + " " + dev.VoltR / dev.ReferenceVolt + " " + "1" + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + "0";
                        }
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Load.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\Load.txt");
                    }
                    FileStream load = new FileStream((System.Windows.Forms.Application.StartupPath + "\\Load.txt"), FileMode.OpenOrCreate);
                    StreamWriter str8 = new StreamWriter(load, System.Text.Encoding.Default);
                    str8.Write(loadline);
                    str8.Close();
                    string genline = null;
                    foreach (PSPDEV dev in listGen)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            string con = " WHERE Name='" + dev.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];

                            if (genline != null)
                            {
                                genline += "\r\n";
                            }
                            if (dev.UnitFlag == "0")
                            {
                                genline += dev.Name + " " + dev.Number.ToString() + " " + pspiswitch.Number + " " + "1" + " " + dev.SjN.ToString() + " " + dev.PositiveTQ.ToString() + " " + dev.SkN.ToString() + " " + dev.OutP + " " + dev.OutQ + " " + "0" + " " + "0" + " " + dev.Vkb; ;
                            }
                            else
                                genline += dev.Name + " " + dev.Number + " " + pspiswitch.Number + " " + "1" + " " + dev.SjN * (dev.RateVolt * dev.RateVolt) * ratedCapacity / (dev.Vkb * dev.RateVolt * dev.RateVolt) + " " + dev.PositiveTQ * (dev.RateVolt * dev.RateVolt) * ratedCapacity / (dev.Vkb * dev.RateVolt * dev.RateVolt) + " " + dev.SkN * (dev.RateVolt * dev.RateVolt) * ratedCapacity / (dev.Vkb * dev.RateVolt * dev.RateVolt) + " " + dev.OutP / ratedCapacity + " " + dev.OutQ / ratedCapacity + " " + "0" + " " + "0" + " " + dev.Vkb / ratedCapacity;
                        }
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\gen.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\gen.txt");
                    }
                    FileStream gen = new FileStream((System.Windows.Forms.Application.StartupPath + "\\gen.txt"), FileMode.OpenOrCreate);
                    StreamWriter str9 = new StreamWriter(gen, System.Text.Encoding.Default);
                    str9.Write(genline);
                    str9.Close();
                    string mulian = null;
                    foreach (PSPDEV dev in listML)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            string con = " WHERE Name='" + dev.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                            con = " WHERE Name='" + dev.JName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            IList listjswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspjswitch = (PSPDEV)listjswitch[0];
                            if (mulian != null)
                            {
                                mulian += "\r\n";
                            }
                            mulian += dev.Number + " " + pspiswitch.Number + " " + pspjswitch.Number + " " + "1";
                        }
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\mulian.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\mulian.txt");
                    }
                    FileStream mu = new FileStream((System.Windows.Forms.Application.StartupPath + "\\mulian.txt"), FileMode.OpenOrCreate);
                    StreamWriter str10 = new StreamWriter(mu, System.Text.Encoding.Default);
                    str10.Write(mulian);
                    str10.Close();
                    string mulian23 = null;
                    foreach (PSPDEV dev in listML2)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            string con = " WHERE Name='" + dev.HuganLine1 + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            IList listinode = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspinode = (PSPDEV)listinode[0];
                            con = " WHERE Name='" + dev.HuganLine2 + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            IList listjnode = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspjnode = (PSPDEV)listjnode[0];
                            con = " WHERE Name='" + dev.HuganLine3 + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
                            IList listiline = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiline = (PSPDEV)listinode[0];
                            con = " WHERE Name='" + dev.HuganLine4 + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
                            IList listjline = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspjline = (PSPDEV)listjnode[0];
                            con = " WHERE Name='" + dev.KName + "' AND ProjectID = '" + projectid + "'" + "AND Type='13'";
                            IList listiload = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiload = (PSPDEV)listiload[0];
                            con = " WHERE Name='" + dev.KSwitchStatus + "' AND ProjectID = '" + projectid + "'" + "AND Type='13'";
                            IList listjload = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspjload = (PSPDEV)listjload[0];
                            string switch1 = "0";
                            if (dev.LineLevel == "0")
                            {
                                switch1 = "1";
                            }
                            string switch2 = "0";
                            if (dev.LineType == "0")
                            {
                                switch2 = "1";
                            }
                            string switch3 = "0";
                            if (dev.LineStatus == "0")
                            {
                                switch3 = "1";
                            }
                            if (mulian23 != null)
                            {
                                mulian23 += "\r\n";
                            }
                            mulian23 += dev.Number + " " + pspinode.Number + " " + pspjnode.Number + " " + pspiline.Number + " " + pspiload.Number + " " + pspjline.Number + " " + pspjload.Number + " " + switch1 + " " + switch2 + " " + switch3;
                        }
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\mulian23.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\mulian23.txt");
                    }
                    FileStream mul = new FileStream((System.Windows.Forms.Application.StartupPath + "\\mulian23.txt"), FileMode.OpenOrCreate);
                    StreamWriter str11 = new StreamWriter(mul, System.Text.Encoding.Default);
                    str11.Write(mulian23);
                    str11.Close();
                    string mutl_ind = null;
                    foreach (PSPDEV dev in listHG)
                    {

                        string con = " WHERE Name='" + dev.HuganLine1 + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
                        IList listiline = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                        PSPDEV pspiline = (PSPDEV)listiline[0];
                        con = " WHERE Name='" + dev.HuganLine2 + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
                        IList listjline = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                        PSPDEV pspjline = (PSPDEV)listjline[0];


                        if (mutl_ind != null)
                        {
                            mutl_ind += "\r\n";
                        }
                        if (dev.UnitFlag == "0")
                        {
                            mutl_ind += dev.Number + " " + pspiline.Number + " " + pspjline.Number + " " + dev.HuganTQ1;
                        }
                        else

                            mutl_ind += dev.Number + " " + pspiline.Number + " " + pspjline.Number + " " + dev.HuganTQ1 * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt);

                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\mutl_ind.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\mutl_ind.txt");
                    }
                    FileStream Hu = new FileStream((System.Windows.Forms.Application.StartupPath + "\\mutl_ind.txt"), FileMode.OpenOrCreate);
                    StreamWriter str13 = new StreamWriter(Hu, System.Text.Encoding.Default);
                    str13.Write(mutl_ind);
                    str13.Close();
                    string bus = null;
                    int fautnumber = 0;
                    foreach (PSPDEV dev in listMX)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            fautnumber++;
                            if (bus != null)
                            {
                                bus += "\r\n";
                            }

                            if (dev.UnitFlag == "0")
                            {
                                bus += dev.Name + " " + dev.Number + " " + dev.RateVolt / dev.ReferenceVolt + " " + dev.VoltV + " " + dev.ReferenceVolt;
                            }
                            else
                            {
                                bus += dev.Name + " " + dev.Number + " " + dev.RateVolt / dev.ReferenceVolt + " " + dev.VoltV + " " + dev.ReferenceVolt;
                            }
                        }
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\bus.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\bus.txt");
                    }
                    FileStream bu = new FileStream((System.Windows.Forms.Application.StartupPath + "\\bus.txt"), FileMode.OpenOrCreate);
                    StreamWriter str12 = new StreamWriter(bu, System.Text.Encoding.Default);
                    str12.Write(bus);
                    str12.Close();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Zmatrixcheck.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\Zmatrixcheck.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Fmatrixcheck.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\Fmatrixcheck.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Lmatrixcheck.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\Lmatrixcheck.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt");
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt");
                    }

                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("数据存在问题请输入完全后再操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
        private string _p1, _p2; private int _p3; private double _p4;
        public string P1
        {
            get
            {
                return _p1;
            }
            set
            {
                _p1 = value;
            }
        }
        public string P2
        {
            get
            {
                return _p2;
            }
            set
            {
                _p2 = value;
            }
        }
        public int P3
        {
            get
            {
                return _p3;
            }
            set
            {
                _p3 = value;
            }
        }
        public double P4
        {
            get
            {
                return _p4;
            }
            set
            {
                _p4 = value;
            }
        }
        public void temp()
        {
            Allshort(P1, P2, P3, P4);
        }

        public void Partshort(string projectSUID, string projectid, int dulutype, double ratecaplity, List<PSPDEV> list1)
        {
            int cishu = 0;           //记录第多少次出现内存问题
            try
            {
                if (Compuflag == 1)
                {
                    ElectricLoadCal elcc = new ElectricLoadCal();
                    elcc.LFCS(projectSUID, 1, (float)ratecaplity);
                    if (!CheckDLL(projectSUID, projectid, ratecaplity))
                    {
                        return;
                    }
                }
                else
                {
                    if (!CheckDL(projectSUID, projectid, ratecaplity))
                    {
                        return;
                    }
                }

                //if (!CheckDL(projectSUID, projectid, ratecaplity))
                //{
                //    return;
                //}
                System.Windows.Forms.Clipboard.Clear();
                Dictionary<int, double> nodeshorti = new Dictionary<int, double>();      //记录母线有没有进行过短路 
                KeyValuePair<int, double> maxshorti = new KeyValuePair<int, double>(); //取出短路的最大短路电流

                string con = null;
                PSPDEV pspDev = new PSPDEV();
                //IList list1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);

                PSPDEV psp = new PSPDEV();

                string data = System.DateTime.Now.ToString("d");
                string time = System.DateTime.Now.ToString("T");
                string duanResult = null;
                duanResult += "短路电流简表" + "\r\n" + "\r\n";
                duanResult += "短路作业号：1" + "\r\n";
                duanResult += "短路计算日期：" + data + " " + "时间：" + time + "\r\n";
                duanResult += "单位：kA" + "\r\n";
                string dianYaResult = null;
                dianYaResult += "母线电压结果" + "\r\n" + "\r\n";
                dianYaResult += "短路作业号：1" + "\r\n";
                dianYaResult += "短路计算日期：" + data + " " + "时间：" + time + "\r\n";
                dianYaResult += "单位：幅值( p.u. )  角度(deg.)" + "\r\n";
                string dianLiuResult = null;
                dianLiuResult += "支路电流结果" + "\r\n" + "\r\n";
                dianLiuResult += "短路作业号：1" + "\r\n";
                dianLiuResult += "短路计算日期：" + data + " " + "时间：" + time + "\r\n";
                dianLiuResult += "单位：幅值( p.u. )  角度(deg.)" + "\r\n";
                int intshorti = 0;        //第一行记录的为要读短路电流的属性说明
                bool shortiflag = false;
                int muxiannum = 0;         //记录一个母线短路后 有多少个记录母线电压
                int linenum = 0;           //记录一个母线短路 有多少个线路电流
                shortbuscir shortCutCal = new shortbuscir(Compuflag);
                for (int i = 0; i < list1.Count; i++)
                {
                    cishu++;
                    pspDev = list1[i] as PSPDEV;
                    bool flag = false;
                    string dlr = null;
                    con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='05'AND PSPDEV.KSwitchStatus = '0'AND (PSPDEV.IName='" + pspDev.Name + "'OR PSPDEV.JName='" + pspDev.Name + "')order by PSPDEV.number";
                    IList list2 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    for (int j = 0; j < list2.Count; j++)
                    {
                        psp = list2[j] as PSPDEV;
                        con = " WHERE Name='" + psp.ISwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                        IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                        con = " WHERE Name='" + psp.JSwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                        IList listjswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                        PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                        PSPDEV pspjswitch = (PSPDEV)listjswitch[0];

                        if (pspDev.Number == psp.FirstNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                        {

                            flag = true;
                            dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "0 " + " " + dulutype;

                        }
                        if (pspDev.Number == psp.LastNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                        {
                            flag = true;
                            dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "1 " + " " + dulutype;
                        }
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\fault.txt"))
                        {
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\fault.txt");
                        }
                        if (flag)
                        {

                            break;                 //跳出本循环 进行母线的另外一个母线短路
                        }
                        if (!flag)
                            continue;
                        //写入错误中
                    }
                    //如果在一般线路中没有则在两绕组中进行
                    if (!flag)
                    {
                        con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='02'AND PSPDEV.KSwitchStatus = '0'AND(PSPDEV.IName='" + pspDev.Name + "'OR PSPDEV.JName='" + pspDev.Name + "') order by PSPDEV.number";
                        IList list3 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                        for (int j = 0; j < list3.Count; j++)
                        {
                            dlr = null;
                            psp = list3[j] as PSPDEV;
                            //PSPDEV devFirst = new PSPDEV();

                            //con = " WHERE Name='" + psp.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            //devFirst = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                            //PSPDEV devLast = new PSPDEV();


                            //con = " WHERE Name='" + psp.JName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            //devLast = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + psp.ISwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + psp.JSwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listjswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                            PSPDEV pspjswitch = (PSPDEV)listjswitch[0];
                            if (pspDev.Number == psp.FirstNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                            {

                                flag = true;
                                dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "0" + " " + dulutype;

                            }
                            if (pspDev.Number == psp.LastNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                            {
                                flag = true;
                                dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "1" + " " + dulutype;
                            }
                            if (flag)
                            {
                                break;                 //跳出本循环 进行母线的另外一个母线短路
                            }
                            if (!flag)
                                continue;
                            //写入错误中
                        }
                    }
                    if (!flag)
                    {
                        con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='03'AND PSPDEV.KSwitchStatus = '0'AND(PSPDEV.IName='" + pspDev.Name + "'OR PSPDEV.JName='" + pspDev.Name + "'OR PSPDEV.KName='" + pspDev.Name + "') order by PSPDEV.number";
                        IList list4 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                        for (int j = 0; j < list4.Count; j++)
                        {
                            dlr = null;
                            psp = list4[j] as PSPDEV;
                            //PSPDEV devINode = new PSPDEV();

                            //con = " WHERE Name='" + psp.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            //devINode = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                            //PSPDEV devJNode = new PSPDEV();

                            //con = " WHERE Name='" + psp.JName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            //devJNode = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                            //PSPDEV devKNode = new PSPDEV();

                            //con = " WHERE Name='" + psp.KName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            //devKNode = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);

                            con = " WHERE Name='" + psp.ISwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + psp.JSwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listjswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + psp.HuganLine1 + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listkswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                            PSPDEV pspjswitch = (PSPDEV)listjswitch[0];
                            PSPDEV pspkswitch = (PSPDEV)listkswitch[0];
                            if (pspDev.Number == psp.FirstNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0" && pspkswitch.KSwitchStatus == "0")
                            {

                                flag = true;
                                dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "0" + " " + dulutype;

                            }
                            if (pspDev.Number == psp.LastNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0" && pspkswitch.KSwitchStatus == "0")
                            {
                                flag = true;
                                dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "1" + " " + dulutype;
                            }
                            if (pspDev.Number == psp.Flag && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0" && pspkswitch.KSwitchStatus == "0")
                            {
                                flag = true;
                                dlr = "0" + " " + psp.FirstNode + " " + psp.Flag + " " + psp.Number + " " + "1" + " " + dulutype;
                            }

                            if (flag)
                            {
                                break;                 //跳出本循环 进行母线的另外一个母线短路
                            }
                            if (!flag)
                                continue;
                            //写入错误中
                        }
                    }
                    if (flag)
                    {
                        FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\fault.txt"), FileMode.OpenOrCreate);
                        StreamWriter str11 = new StreamWriter(VK);
                        str11.Write(dlr);
                        str11.Close();
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                        {
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt");
                        }
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt"))
                        {
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt");
                        }
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt"))
                        {
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt");
                        }
                        //shortcir shortCutCal = new shortcir();
                        shortCutCal.Show_shortcir(Compuflag, OutType, 1);
                        GC.Collect();
                        //bool matrixflag=true;                //用来判断是否导纳矩阵的逆矩阵是否存在逆矩阵
                        string matrixstr = null;
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Zmatrixcheck.txt"))
                        {
                            matrixstr = "正序导纳矩阵";
                            // matrixflag = false;
                        }

                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Fmatrixcheck.txt"))
                        {
                            // matrixflag = false;
                            matrixstr += "负序导纳矩阵";
                        }

                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Lmatrixcheck.txt"))
                        {
                            //matrixflag = false;
                            matrixstr += "零序导纳矩阵";
                        }
                        if (matrixstr != null)
                        {
                            System.Windows.Forms.MessageBox.Show(matrixstr + "不存在逆矩阵，请调整参数后再进行计算！", "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                            return;
                        }
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                        {
                        }
                        else
                        {
                            return;
                        }

                        FileStream shorcuit = new FileStream(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt", FileMode.Open);
                        StreamReader readLineGU = new StreamReader(shorcuit, System.Text.Encoding.Default);
                        string strLineGU;
                        string[] arrayGU;
                        char[] charSplitGU = new char[] { ' ' };
                        intshorti = 0;
                        while ((strLineGU = readLineGU.ReadLine()) != null)
                        {


                            arrayGU = strLineGU.Split(charSplitGU);
                            string[] shorti = new string[4];
                            shorti.Initialize();
                            int m = 0;
                            foreach (string str in arrayGU)
                            {

                                if (str != "")
                                {

                                    shorti[m++] = str.ToString();

                                }
                            }
                            if (intshorti == 0)
                            {
                                if (!shortiflag)
                                {
                                    duanResult += shorti[0] + "," + shorti[1] + "," + shorti[3] + "\r\n";
                                    shortiflag = true;
                                }

                            }
                            else
                                duanResult += shorti[0] + "," + shorti[1] + "," + Convert.ToDouble(shorti[3]) * ratecaplity / (Math.Sqrt(3) * pspDev.ReferenceVolt) + "\r\n";

                            intshorti++;
                        }
                        readLineGU.Close();
                        if (OutType == 0)
                        {
                            //**读取三序电压的值
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt"))
                            {
                            }
                            else
                            {
                                return;
                            }
                            FileStream dianYa = new FileStream(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt", FileMode.Open);
                            StreamReader readLineDY = new StreamReader(dianYa, System.Text.Encoding.Default);
                            string strLineDY;
                            string[] arrayDY;
                            char[] charSplitDY = new char[] { ' ' };
                            strLineDY = readLineDY.ReadLine();
                            int j = 0;
                            muxiannum = 0;
                            while (strLineDY != null)
                            {
                                arrayDY = strLineDY.Split(charSplitDY);

                                int m = 0;
                                string[] dev = new string[14];
                                dev.Initialize();
                                foreach (string str in arrayDY)
                                {
                                    if (str != "")
                                    {
                                        dev[m++] = str;
                                    }
                                }
                                if (j == 0)
                                {
                                    dianYaResult += "\r\n" + "故障母线：" + pspDev.Name + "\r\n";
                                    dianYaResult += dev[0] + "," + dev[1] + "," + dev[2] + "," + dev[3] + "," + dev[4] + "," + dev[5] + "," + dev[6] + "," + dev[7] + "," + dev[8] + "," +
             dev[9] + "," + dev[10] + "," + dev[11] + "," + dev[12] + "," + dev[13] + "\r\n";
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
                                        dianYaResult += dev[0] + "," + dev[1] + "," + Convert.ToDouble(dev[2]) * CR.ReferenceVolt + "," + dev[3] + "," + Convert.ToDouble(dev[4]) * CR.ReferenceVolt + "," + dev[5] + "," + Convert.ToDouble(dev[6]) * CR.ReferenceVolt + "," + dev[7] + "," + Convert.ToDouble(dev[8]) * CR.ReferenceVolt + "," +
                                            dev[9] + "," + Convert.ToDouble(dev[10]) * CR.ReferenceVolt + "," + dev[11] + "," + Convert.ToDouble(dev[12]) * CR.ReferenceVolt + "," + dev[13] + "\r\n";
                                    //else
                                    //    dianYaResult += dev[0] + "," + duanluname + "上短路点" + "," + Convert.ToDouble(dev[2]) * CR.ReferenceVolt + "," + dev[3] + "," + Convert.ToDouble(dev[4]) * CR.ReferenceVolt + "," + dev[5] + "," + Convert.ToDouble(dev[6]) * CR.ReferenceVolt + "," + dev[7] + "," + Convert.ToDouble(dev[8]) * CR.ReferenceVolt + "," +
                                    //       dev[9] + "," + Convert.ToDouble(dev[10]) * CR.ReferenceVolt + "," + dev[11] + Convert.ToDouble(dev[12]) * CR.ReferenceVolt + "," + dev[13] + "\r\n";

                                }
                                strLineDY = readLineDY.ReadLine();
                                muxiannum++;
                                j++;
                            }
                            readLineDY.Close();
                            //**读取三序电流的值
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt"))
                            {
                            }
                            else
                            {
                                return;
                            }
                            FileStream dianLiu = new FileStream(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt", FileMode.Open);
                            StreamReader readLineDL = new StreamReader(dianLiu, System.Text.Encoding.Default);
                            string strLineDL;
                            string[] arrayDL;
                            char[] charSplitDL = new char[] { ' ' };
                            strLineDL = readLineDL.ReadLine();
                            j = 0;
                            linenum = 0;
                            while (strLineDL != null)
                            {
                                arrayDL = strLineDL.Split(charSplitDL);
                                int m = 0;
                                string[] dev = new string[15];
                                dev.Initialize();
                                foreach (string str in arrayDL)
                                {
                                    if (str != "")
                                    {
                                        dev[m++] = str;
                                    }
                                }
                                if (j == 0)
                                {
                                    dianLiuResult += "\r\n" + "故障母线：" + pspDev.Name + "\r\n";
                                    dianLiuResult += dev[0] + "," + dev[1] + "," + dev[2] + "," + dev[3] + "," + dev[4] + "," + dev[5] + "," + dev[6] + "," + dev[7] + "," + dev[8] + "," +
                                                 dev[9] + "," + dev[10] + "," + dev[11] + "," + dev[12] + "," + dev[13] + "," + dev[14] + "\r\n";
                                }
                                else
                                {

                                    //因为在线路电流输出时既有一般线路的电流、两绕组和三绕组线路的电流还有接地电容器和电抗器的电流，因此只将电流输出就行了
                                    PSPDEV CR = new PSPDEV();

                                    if (dev[0] != "du")
                                    {

                                        con = " WHERE Name='" + dev[0] + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                                    }
                                    else
                                        con = " WHERE Name='" + dev[1] + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";

                                    CR = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);

                                    dianLiuResult += dev[0] + "," + dev[1] + "," + dev[2] + "," + Convert.ToDouble(dev[3]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[4] + "," + Convert.ToDouble(dev[5]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[6] + "," + Convert.ToDouble(dev[7]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[8] + "," +
                                      Convert.ToDouble(dev[9]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[10] + "," + Convert.ToDouble(dev[11]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[12] + "," + Convert.ToDouble(dev[13]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[14] + "\r\n";
                                }

                                strLineDL = readLineDL.ReadLine();
                                j++;
                                linenum++;
                            }
                            readLineDL.Close();

                        }
                    }

                }
                //写入报表中
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result.csv"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\result.csv");
                }
                FileStream tempGU = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result.csv"), FileMode.OpenOrCreate);
                StreamWriter strGU = new StreamWriter(tempGU, Encoding.Default);
                strGU.Write(duanResult);
                strGU.Close();
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result1.csv"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                }
                FileStream tempDY = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result1.csv"), FileMode.OpenOrCreate);
                StreamWriter strDY = new StreamWriter(tempDY, Encoding.Default);
                strDY.Write(dianYaResult);
                strDY.Close();
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result2.csv"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\result2.csv");
                }
                FileStream tempDL = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result2.csv"), FileMode.OpenOrCreate);
                StreamWriter strDL = new StreamWriter(tempDL, Encoding.Default);
                strDL.Write(dianLiuResult);
                strDL.Close();
                PSP_ELCPROJECT psproject = new PSP_ELCPROJECT();
                psproject.ID = projectSUID;
                psproject = (PSP_ELCPROJECT)UCDeviceBase.DataService.GetObject("SelectPSP_ELCPROJECTByKey", psproject);
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "全网短路计算结果.xls"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "全网短路计算结果.xls");
                }

                Excel.Application ex;
                Excel.Worksheet xSheet;
                Excel.Application result1;
                Excel.Application result2;
                Excel.Worksheet tempSheet;
                Excel.Worksheet tempSheet1;
                Excel.Worksheet newWorksheet;
                Excel.Worksheet newWorkSheet1;

                object oMissing = System.Reflection.Missing.Value;
                ex = new Excel.Application();
                ex.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result.csv");

                xSheet = (Excel.Worksheet)ex.Worksheets[1];
                ex.Worksheets.Add(System.Reflection.Missing.Value, xSheet, 1, System.Reflection.Missing.Value);
                xSheet = (Excel.Worksheet)ex.Worksheets[2];
                ex.Worksheets.Add(System.Reflection.Missing.Value, xSheet, 1, System.Reflection.Missing.Value);
                xSheet = (Excel.Worksheet)ex.Worksheets[1];
                result1 = new Excel.Application();
                result1.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                result2 = new Excel.Application();
                result2.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result2.csv");
                tempSheet = (Excel.Worksheet)result1.Worksheets.get_Item(1);
                tempSheet1 = (Excel.Worksheet)result2.Worksheets.get_Item(1);
                newWorksheet = (Excel.Worksheet)ex.Worksheets.get_Item(2);
                newWorkSheet1 = (Excel.Worksheet)ex.Worksheets.get_Item(3);
                newWorksheet.Name = "母线电压";
                newWorkSheet1.Name = "支路电流";
                xSheet.Name = "短路电流";
                ex.Visible = true;

                tempSheet.Cells.Select();
                tempSheet.Cells.Copy(System.Reflection.Missing.Value);
                newWorksheet.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                tempSheet1.Cells.Select();
                tempSheet1.Cells.Copy(System.Reflection.Missing.Value);
                newWorkSheet1.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);

                xSheet.UsedRange.Font.Name = "楷体_GB2312";
                newWorksheet.UsedRange.Font.Name = "楷体_GB2312";
                newWorkSheet1.UsedRange.Font.Name = "楷体_GB2312";
                //记录的为短路电流格式
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 3]).MergeCells = true;
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Size = 20;
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Name = "黑体";
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xSheet.get_Range(xSheet.Cells[6, 1], xSheet.Cells[6, 3]).Interior.ColorIndex = 45;
                xSheet.get_Range(xSheet.Cells[7, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 1]).Interior.ColorIndex = 6;
                xSheet.get_Range(xSheet.Cells[4, 3], xSheet.Cells[xSheet.UsedRange.Rows.Count, 13]).NumberFormat = "0.0000_ ";
                //母线电压显示方式
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 14]).MergeCells = true;
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Size = 20;
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Name = "黑体";
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                if (OutType == 0)
                {
                    for (int m = 0; m < cishu; m++)
                    {
                        newWorksheet.get_Range(newWorksheet.Cells[m * (muxiannum + 2) + 8, 1], newWorksheet.Cells[m * (muxiannum + 2) + 8, 14]).Interior.ColorIndex = 45;
                        newWorksheet.get_Range(newWorksheet.Cells[m * (muxiannum + 2) + 9, 1], newWorksheet.Cells[m * (muxiannum + 2) + 8 + muxiannum - 1, 1]).Interior.ColorIndex = 6;
                        newWorksheet.get_Range(newWorksheet.Cells[m * (muxiannum + 2) + 9, 3], newWorksheet.Cells[m * (muxiannum + 2) + 8 + muxiannum - 1, 13]).NumberFormat = "0.0000_ ";
                    }
                }



                //线路三序电流的显示方式
                newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 15]).MergeCells = true;
                newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 1]).Font.Size = 20;
                newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 1]).Font.Name = "黑体";
                newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                if (OutType == 0)
                {
                    for (int m = 0; m < cishu; m++)
                    {
                        newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum + 2) + 8, 1], newWorkSheet1.Cells[m * (linenum + 2) + 8, 15]).Interior.ColorIndex = 45;
                        newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum + 2) + 9, 1], newWorkSheet1.Cells[m * (linenum + 2) + 8 + linenum - 1, 1]).Interior.ColorIndex = 6;
                        newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum + 2) + 9, 2], newWorkSheet1.Cells[m * (linenum + 2) + 8 + linenum - 1, 2]).Interior.ColorIndex = 6;
                        newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum + 2) + 9, 3], newWorkSheet1.Cells[m * (linenum + 2) + 8 + linenum - 1, 3]).Interior.ColorIndex = 6;
                        newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum + 2) + 9, 4], newWorkSheet1.Cells[m * (linenum + 2) + 8 + linenum - 1, 14]).NumberFormat = "0.0000_ ";
                    }
                }


                xSheet.Rows.AutoFit();
                xSheet.Columns.AutoFit();
                newWorksheet.Rows.AutoFit();
                newWorksheet.Columns.AutoFit();
                newWorkSheet1.Rows.AutoFit();
                newWorkSheet1.Columns.AutoFit();
                newWorksheet.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "全网短路计算结果.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);
                System.Windows.Forms.Clipboard.Clear();
                result1.Workbooks.Close();
                result1.Quit();
                result2.Workbooks.Close();
                result2.Quit();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("数据存在问题请输入完全后再操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        public void Partshort(string projectSUID, string projectid, int dulutype, double ratecaplity, List<PSPDEV> list1,frnReport wFrom)
        {
            int cishu = 0;           //记录第多少次出现内存问题
            try
            {
                wFrom.ShowText += "\r\n开始准备短路计算数据\t" + System.DateTime.Now.ToString();
                if (Compuflag == 1)
                {
                    ElectricLoadCal elcc = new ElectricLoadCal();
                    elcc.LFCS(projectSUID, 1, (float)ratecaplity);
                    if (!CheckDLL(projectSUID, projectid, ratecaplity))
                    {
                        return;
                    }
                }
                else
                {
                    if (!CheckDL(projectSUID, projectid, ratecaplity))
                    {
                        return;
                    }
                }

                //if (!CheckDL(projectSUID, projectid, ratecaplity))
                //{
                //    return;
                //}
                System.Windows.Forms.Clipboard.Clear();
                Dictionary<int, double> nodeshorti = new Dictionary<int, double>();      //记录母线有没有进行过短路 
                KeyValuePair<int, double> maxshorti = new KeyValuePair<int, double>(); //取出短路的最大短路电流

                string con = null;
                PSPDEV pspDev = new PSPDEV();
                //IList list1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);

                PSPDEV psp = new PSPDEV();

                string data = System.DateTime.Now.ToString("d");
                string time = System.DateTime.Now.ToString("T");
                string duanResult = null;
                duanResult += "短路电流简表" + "\r\n" + "\r\n";
                duanResult += "短路作业号：1" + "\r\n";
                duanResult += "短路计算日期：" + data + " " + "时间：" + time + "\r\n";
                duanResult += "单位：kA" + "\r\n";
                string dianYaResult = null;
                dianYaResult += "母线电压结果" + "\r\n" + "\r\n";
                dianYaResult += "短路作业号：1" + "\r\n";
                dianYaResult += "短路计算日期：" + data + " " + "时间：" + time + "\r\n";
                dianYaResult += "单位：幅值( p.u. )  角度(deg.)" + "\r\n";
                string dianLiuResult = null;
                dianLiuResult += "支路电流结果" + "\r\n" + "\r\n";
                dianLiuResult += "短路作业号：1" + "\r\n";
                dianLiuResult += "短路计算日期：" + data + " " + "时间：" + time + "\r\n";
                dianLiuResult += "单位：幅值( p.u. )  角度(deg.)" + "\r\n";
                int intshorti = 0;        //第一行记录的为要读短路电流的属性说明
                bool shortiflag = false;
                int muxiannum = 0;         //记录一个母线短路后 有多少个记录母线电压
                int linenum = 0;           //记录一个母线短路 有多少个线路电流
                shortbuscir shortCutCal = new shortbuscir(Compuflag);
                for (int i = 0; i < list1.Count; i++)
                {
                    cishu++;
                    pspDev = list1[i] as PSPDEV;
                    bool flag = false;
                    string dlr = null;
                    con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='05'AND PSPDEV.KSwitchStatus = '0'AND (PSPDEV.IName='" + pspDev.Name + "'OR PSPDEV.JName='" + pspDev.Name + "')order by PSPDEV.number";
                    IList list2 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    for (int j = 0; j < list2.Count; j++)
                    {
                        psp = list2[j] as PSPDEV;
                        con = " WHERE Name='" + psp.ISwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                        IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                        con = " WHERE Name='" + psp.JSwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                        IList listjswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                        PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                        PSPDEV pspjswitch = (PSPDEV)listjswitch[0];

                        if (pspDev.Number == psp.FirstNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                        {

                            flag = true;
                            dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "0 " + " " + dulutype;

                        }
                        if (pspDev.Number == psp.LastNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                        {
                            flag = true;
                            dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "1 " + " " + dulutype;
                        }
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\fault.txt"))
                        {
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\fault.txt");
                        }
                        if (flag)
                        {

                            break;                 //跳出本循环 进行母线的另外一个母线短路
                        }
                        if (!flag)
                            continue;
                        //写入错误中
                    }
                    //如果在一般线路中没有则在两绕组中进行
                    if (!flag)
                    {
                        con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='02'AND PSPDEV.KSwitchStatus = '0'AND(PSPDEV.IName='" + pspDev.Name + "'OR PSPDEV.JName='" + pspDev.Name + "') order by PSPDEV.number";
                        IList list3 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                        for (int j = 0; j < list3.Count; j++)
                        {
                            dlr = null;
                            psp = list3[j] as PSPDEV;
                            //PSPDEV devFirst = new PSPDEV();

                            //con = " WHERE Name='" + psp.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            //devFirst = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                            //PSPDEV devLast = new PSPDEV();


                            //con = " WHERE Name='" + psp.JName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            //devLast = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + psp.ISwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + psp.JSwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listjswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                            PSPDEV pspjswitch = (PSPDEV)listjswitch[0];
                            if (pspDev.Number == psp.FirstNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                            {

                                flag = true;
                                dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "0" + " " + dulutype;

                            }
                            if (pspDev.Number == psp.LastNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                            {
                                flag = true;
                                dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "1" + " " + dulutype;
                            }
                            if (flag)
                            {
                                break;                 //跳出本循环 进行母线的另外一个母线短路
                            }
                            if (!flag)
                                continue;
                            //写入错误中
                        }
                    }
                    if (!flag)
                    {
                        con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='03'AND PSPDEV.KSwitchStatus = '0'AND(PSPDEV.IName='" + pspDev.Name + "'OR PSPDEV.JName='" + pspDev.Name + "'OR PSPDEV.KName='" + pspDev.Name + "') order by PSPDEV.number";
                        IList list4 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                        for (int j = 0; j < list4.Count; j++)
                        {
                            dlr = null;
                            psp = list4[j] as PSPDEV;
                            //PSPDEV devINode = new PSPDEV();

                            //con = " WHERE Name='" + psp.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            //devINode = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                            //PSPDEV devJNode = new PSPDEV();

                            //con = " WHERE Name='" + psp.JName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            //devJNode = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                            //PSPDEV devKNode = new PSPDEV();

                            //con = " WHERE Name='" + psp.KName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            //devKNode = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);

                            con = " WHERE Name='" + psp.ISwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + psp.JSwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listjswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + psp.HuganLine1 + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listkswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                            PSPDEV pspjswitch = (PSPDEV)listjswitch[0];
                            PSPDEV pspkswitch = (PSPDEV)listkswitch[0];
                            if (pspDev.Number == psp.FirstNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0" && pspkswitch.KSwitchStatus == "0")
                            {

                                flag = true;
                                dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "0" + " " + dulutype;

                            }
                            if (pspDev.Number == psp.LastNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0" && pspkswitch.KSwitchStatus == "0")
                            {
                                flag = true;
                                dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "1" + " " + dulutype;
                            }
                            if (pspDev.Number == psp.Flag && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0" && pspkswitch.KSwitchStatus == "0")
                            {
                                flag = true;
                                dlr = "0" + " " + psp.FirstNode + " " + psp.Flag + " " + psp.Number + " " + "1" + " " + dulutype;
                            }

                            if (flag)
                            {
                                break;                 //跳出本循环 进行母线的另外一个母线短路
                            }
                            if (!flag)
                                continue;
                            //写入错误中
                        }
                    }
                    if (flag)
                    {
                        //FileStream VK = new FileStream(, FileMode.OpenOrCreate);
                        
                        StreamWriter str11 = new StreamWriter((System.Windows.Forms.Application.StartupPath + "\\fault.txt"),false);
                        str11.Write(dlr);
                        str11.Close();
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                        {
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt");
                        }
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt"))
                        {
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt");
                        }
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt"))
                        {
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt");
                        }
                        wFrom.ShowText += "\r\n开始进行短路计算\t" + System.DateTime.Now.ToString();
                        //shortcir shortCutCal = new shortcir();
                        shortCutCal.Show_shortcir(Compuflag, OutType, 1);
                        GC.Collect();
                        //bool matrixflag=true;                //用来判断是否导纳矩阵的逆矩阵是否存在逆矩阵
                        string matrixstr = null;
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Zmatrixcheck.txt"))
                        {
                            matrixstr = "正序导纳矩阵";
                            // matrixflag = false;
                        }

                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Fmatrixcheck.txt"))
                        {
                            // matrixflag = false;
                            matrixstr += "负序导纳矩阵";
                        }

                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Lmatrixcheck.txt"))
                        {
                            //matrixflag = false;
                            matrixstr += "零序导纳矩阵";
                        }
                        if (matrixstr != null)
                        {
                            wFrom.ShowText += "\r\n短路计算失败\t" + System.DateTime.Now.ToString();
                            System.Windows.Forms.MessageBox.Show(matrixstr + "不存在逆矩阵，请调整参数后再进行计算！", "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                            return;
                        }
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                        {
                        }
                        else
                        {
                            wFrom.ShowText += "\r\n短路计算失败\t" + System.DateTime.Now.ToString();
                            return;
                        }
                        wFrom.ShowText += "\r\n开始处理短路计算结果\t" + System.DateTime.Now.ToString();
                        FileStream shorcuit = new FileStream(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt", FileMode.Open);
                        StreamReader readLineGU = new StreamReader(shorcuit, System.Text.Encoding.Default);
                        string strLineGU;
                        string[] arrayGU;
                        char[] charSplitGU = new char[] { ' ' };
                        intshorti = 0;
                        while ((strLineGU = readLineGU.ReadLine()) != null)
                        {


                            arrayGU = strLineGU.Split(charSplitGU);
                            string[] shorti = new string[4];
                            shorti.Initialize();
                            int m = 0;
                            foreach (string str in arrayGU)
                            {

                                if (str != "")
                                {

                                    shorti[m++] = str.ToString();

                                }
                            }
                            if (intshorti == 0)
                            {
                                if (!shortiflag)
                                {
                                    duanResult += shorti[0] + "," + shorti[1] + "," + shorti[3] + "\r\n";
                                    shortiflag = true;
                                }

                            }
                            else
                                duanResult += shorti[0] + "," + shorti[1] + "," + Convert.ToDouble(shorti[3]) * ratecaplity / (Math.Sqrt(3) * pspDev.ReferenceVolt) + "\r\n";

                            intshorti++;
                        }
                        readLineGU.Close();
                        if (OutType == 0)
                        {
                            //**读取三序电压的值
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt"))
                            {
                            }
                            else
                            {
                                wFrom.ShowText += "\r\n短路计算失败\t" + System.DateTime.Now.ToString();
                                return;
                            }
                            FileStream dianYa = new FileStream(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt", FileMode.Open);
                            StreamReader readLineDY = new StreamReader(dianYa, System.Text.Encoding.Default);
                            string strLineDY;
                            string[] arrayDY;
                            char[] charSplitDY = new char[] { ' ' };
                            strLineDY = readLineDY.ReadLine();
                            int j = 0;
                            muxiannum = 0;
                            while (strLineDY != null)
                            {
                                arrayDY = strLineDY.Split(charSplitDY);

                                int m = 0;
                                string[] dev = new string[14];
                                dev.Initialize();
                                foreach (string str in arrayDY)
                                {
                                    if (str != "")
                                    {
                                        dev[m++] = str;
                                    }
                                }
                                if (j == 0)
                                {
                                    dianYaResult += "\r\n" + "故障母线：" + pspDev.Name + "\r\n";
                                    dianYaResult += dev[0] + "," + dev[1] + "," + dev[2] + "," + dev[3] + "," + dev[4] + "," + dev[5] + "," + dev[6] + "," + dev[7] + "," + dev[8] + "," +
             dev[9] + "," + dev[10] + "," + dev[11] + "," + dev[12] + "," + dev[13] + "\r\n";
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
                                        dianYaResult += dev[0] + "," + dev[1] + "," + Convert.ToDouble(dev[2]) * CR.ReferenceVolt + "," + dev[3] + "," + Convert.ToDouble(dev[4]) * CR.ReferenceVolt + "," + dev[5] + "," + Convert.ToDouble(dev[6]) * CR.ReferenceVolt + "," + dev[7] + "," + Convert.ToDouble(dev[8]) * CR.ReferenceVolt + "," +
                                            dev[9] + "," + Convert.ToDouble(dev[10]) * CR.ReferenceVolt + "," + dev[11] + "," + Convert.ToDouble(dev[12]) * CR.ReferenceVolt + "," + dev[13] + "\r\n";
                                    //else
                                    //    dianYaResult += dev[0] + "," + duanluname + "上短路点" + "," + Convert.ToDouble(dev[2]) * CR.ReferenceVolt + "," + dev[3] + "," + Convert.ToDouble(dev[4]) * CR.ReferenceVolt + "," + dev[5] + "," + Convert.ToDouble(dev[6]) * CR.ReferenceVolt + "," + dev[7] + "," + Convert.ToDouble(dev[8]) * CR.ReferenceVolt + "," +
                                    //       dev[9] + "," + Convert.ToDouble(dev[10]) * CR.ReferenceVolt + "," + dev[11] + Convert.ToDouble(dev[12]) * CR.ReferenceVolt + "," + dev[13] + "\r\n";

                                }
                                strLineDY = readLineDY.ReadLine();
                                muxiannum++;
                                j++;
                            }
                            readLineDY.Close();
                            //**读取三序电流的值
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt"))
                            {
                            }
                            else
                            {
                                wFrom.ShowText += "\r\n短路计算失败\t" + System.DateTime.Now.ToString();
                                return;
                            }
                            FileStream dianLiu = new FileStream(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt", FileMode.Open);
                            StreamReader readLineDL = new StreamReader(dianLiu, System.Text.Encoding.Default);
                            string strLineDL;
                            string[] arrayDL;
                            char[] charSplitDL = new char[] { ' ' };
                            strLineDL = readLineDL.ReadLine();
                            j = 0;
                            linenum = 0;
                            while (strLineDL != null)
                            {
                                arrayDL = strLineDL.Split(charSplitDL);
                                int m = 0;
                                string[] dev = new string[15];
                                dev.Initialize();
                                foreach (string str in arrayDL)
                                {
                                    if (str != "")
                                    {
                                        dev[m++] = str;
                                    }
                                }
                                if (j == 0)
                                {
                                    dianLiuResult += "\r\n" + "故障母线：" + pspDev.Name + "\r\n";
                                    dianLiuResult += dev[0] + "," + dev[1] + "," + dev[2] + "," + dev[3] + "," + dev[4] + "," + dev[5] + "," + dev[6] + "," + dev[7] + "," + dev[8] + "," +
                                                 dev[9] + "," + dev[10] + "," + dev[11] + "," + dev[12] + "," + dev[13] + "," + dev[14] + "\r\n";
                                }
                                else
                                {

                                    //因为在线路电流输出时既有一般线路的电流、两绕组和三绕组线路的电流还有接地电容器和电抗器的电流，因此只将电流输出就行了
                                    PSPDEV CR = new PSPDEV();

                                    if (dev[0] != "du")
                                    {

                                        con = " WHERE Name='" + dev[0] + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                                    }
                                    else
                                        con = " WHERE Name='" + dev[1] + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";

                                    CR = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);

                                    dianLiuResult += dev[0] + "," + dev[1] + "," + dev[2] + "," + Convert.ToDouble(dev[3]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[4] + "," + Convert.ToDouble(dev[5]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[6] + "," + Convert.ToDouble(dev[7]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[8] + "," +
                                      Convert.ToDouble(dev[9]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[10] + "," + Convert.ToDouble(dev[11]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[12] + "," + Convert.ToDouble(dev[13]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[14] + "\r\n";
                                }

                                strLineDL = readLineDL.ReadLine();
                                j++;
                                linenum++;
                            }
                            readLineDL.Close();

                        }
                    }

                }
                wFrom.ShowText += "\r\n开始生成报表\t" + System.DateTime.Now.ToString();
                //写入报表中
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result.csv"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\result.csv");
                }
                FileStream tempGU = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result.csv"), FileMode.OpenOrCreate);
                StreamWriter strGU = new StreamWriter(tempGU, Encoding.Default);
                strGU.Write(duanResult);
                strGU.Close();
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result1.csv"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                }
                FileStream tempDY = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result1.csv"), FileMode.OpenOrCreate);
                StreamWriter strDY = new StreamWriter(tempDY, Encoding.Default);
                strDY.Write(dianYaResult);
                strDY.Close();
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result2.csv"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\result2.csv");
                }
                FileStream tempDL = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result2.csv"), FileMode.OpenOrCreate);
                StreamWriter strDL = new StreamWriter(tempDL, Encoding.Default);
                strDL.Write(dianLiuResult);
                strDL.Close();
                PSP_ELCPROJECT psproject = new PSP_ELCPROJECT();
                psproject.ID = projectSUID;
                psproject = (PSP_ELCPROJECT)UCDeviceBase.DataService.GetObject("SelectPSP_ELCPROJECTByKey", psproject);
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "全网短路计算结果.xls"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "全网短路计算结果.xls");
                }

                Excel.Application ex;
                Excel.Worksheet xSheet;
                Excel.Application result1;
                Excel.Application result2;
                Excel.Worksheet tempSheet;
                Excel.Worksheet tempSheet1;
                Excel.Worksheet newWorksheet;
                Excel.Worksheet newWorkSheet1;

                object oMissing = System.Reflection.Missing.Value;
                ex = new Excel.Application();
                ex.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result.csv");

                xSheet = (Excel.Worksheet)ex.Worksheets[1];
                ex.Worksheets.Add(System.Reflection.Missing.Value, xSheet, 1, System.Reflection.Missing.Value);
                xSheet = (Excel.Worksheet)ex.Worksheets[2];
                ex.Worksheets.Add(System.Reflection.Missing.Value, xSheet, 1, System.Reflection.Missing.Value);
                xSheet = (Excel.Worksheet)ex.Worksheets[1];
                result1 = new Excel.Application();
                result1.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                result2 = new Excel.Application();
                result2.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result2.csv");
                tempSheet = (Excel.Worksheet)result1.Worksheets.get_Item(1);
                tempSheet1 = (Excel.Worksheet)result2.Worksheets.get_Item(1);
                newWorksheet = (Excel.Worksheet)ex.Worksheets.get_Item(2);
                newWorkSheet1 = (Excel.Worksheet)ex.Worksheets.get_Item(3);
                newWorksheet.Name = "母线电压";
                newWorkSheet1.Name = "支路电流";
                xSheet.Name = "短路电流";
                ex.Visible = true;

                tempSheet.Cells.Select();
                tempSheet.Cells.Copy(System.Reflection.Missing.Value);
                newWorksheet.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                tempSheet1.Cells.Select();
                tempSheet1.Cells.Copy(System.Reflection.Missing.Value);
                newWorkSheet1.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);

                xSheet.UsedRange.Font.Name = "楷体_GB2312";
                newWorksheet.UsedRange.Font.Name = "楷体_GB2312";
                newWorkSheet1.UsedRange.Font.Name = "楷体_GB2312";
                //记录的为短路电流格式
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 3]).MergeCells = true;
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Size = 20;
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Name = "黑体";
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xSheet.get_Range(xSheet.Cells[6, 1], xSheet.Cells[6, 3]).Interior.ColorIndex = 45;
                xSheet.get_Range(xSheet.Cells[7, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 1]).Interior.ColorIndex = 6;
                xSheet.get_Range(xSheet.Cells[4, 3], xSheet.Cells[xSheet.UsedRange.Rows.Count, 13]).NumberFormat = "0.0000_ ";
                //母线电压显示方式
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 14]).MergeCells = true;
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Size = 20;
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Name = "黑体";
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                if (OutType == 0)
                {
                    for (int m = 0; m < cishu; m++)
                    {
                        newWorksheet.get_Range(newWorksheet.Cells[m * (muxiannum + 2) + 8, 1], newWorksheet.Cells[m * (muxiannum + 2) + 8, 14]).Interior.ColorIndex = 45;
                        newWorksheet.get_Range(newWorksheet.Cells[m * (muxiannum + 2) + 9, 1], newWorksheet.Cells[m * (muxiannum + 2) + 8 + muxiannum - 1, 1]).Interior.ColorIndex = 6;
                        newWorksheet.get_Range(newWorksheet.Cells[m * (muxiannum + 2) + 9, 3], newWorksheet.Cells[m * (muxiannum + 2) + 8 + muxiannum - 1, 13]).NumberFormat = "0.0000_ ";
                    }
                }



                //线路三序电流的显示方式
                newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 15]).MergeCells = true;
                newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 1]).Font.Size = 20;
                newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 1]).Font.Name = "黑体";
                newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                if (OutType == 0)
                {
                    for (int m = 0; m < cishu; m++)
                    {
                        newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum + 2) + 8, 1], newWorkSheet1.Cells[m * (linenum + 2) + 8, 15]).Interior.ColorIndex = 45;
                        newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum + 2) + 9, 1], newWorkSheet1.Cells[m * (linenum + 2) + 8 + linenum - 1, 1]).Interior.ColorIndex = 6;
                        newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum + 2) + 9, 2], newWorkSheet1.Cells[m * (linenum + 2) + 8 + linenum - 1, 2]).Interior.ColorIndex = 6;
                        newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum + 2) + 9, 3], newWorkSheet1.Cells[m * (linenum + 2) + 8 + linenum - 1, 3]).Interior.ColorIndex = 6;
                        newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum + 2) + 9, 4], newWorkSheet1.Cells[m * (linenum + 2) + 8 + linenum - 1, 14]).NumberFormat = "0.0000_ ";
                    }
                }


                xSheet.Rows.AutoFit();
                xSheet.Columns.AutoFit();
                newWorksheet.Rows.AutoFit();
                newWorksheet.Columns.AutoFit();
                newWorkSheet1.Rows.AutoFit();
                newWorkSheet1.Columns.AutoFit();
                newWorksheet.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "全网短路计算结果.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);
                System.Windows.Forms.Clipboard.Clear();
                result1.Workbooks.Close();
                result1.Quit();
                result2.Workbooks.Close();
                result2.Quit();
                wFrom.ShowText += "\r\n短路计算结束\t" + System.DateTime.Now.ToString();
            }
            catch (System.Exception ex)
            {
                wFrom.ShowText += "\r\n短路计算失败\t" + System.DateTime.Now.ToString();
                MessageBox.Show("数据存在问题请输入完全后再操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        private int _compuflag = 0;
        public int Compuflag
        {
            get { return _compuflag; }
            set { _compuflag = value; }
        }
        private void test()
        {

        }
        public void FileReadDL(string projectSUID, string projectid, int dulutype, double ratecaplity, WaitDialogForm wf, string con, StringBuilder dianLiuResult)
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
            string strLineDL;
            string[] arrayDL;
            char[] charSplitDL = new char[] { ' ' };
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
                    dianLiuResult.Append(dev[0] + "," + dev[1] + "," + dev[2] + "," + dev[3] + "," + dev[4] + "," + dev[5] + "," + dev[6] + "," + dev[7] + "," + dev[8] + "," +
                                 dev[9] + "," + dev[10] + "," + dev[11] + "," + dev[12] + "," + dev[13] + "," + dev[14] + "\r\n");
                }
                else
                {
                    if (dev[0] == "故障母线")
                    {
                        dianLiuResult.Append("\r\n" + "故障母线：" + dev[1] + "\r\n");
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

                        dianLiuResult.Append(dev[0] + "," + dev[1] + "," + dev[2] + "," + Convert.ToDouble(dev[3]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[4] + "," + Convert.ToDouble(dev[5]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[6] + "," + Convert.ToDouble(dev[7]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[8] + "," +
                          Convert.ToDouble(dev[9]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[10] + "," + Convert.ToDouble(dev[11]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[12] + "," + Convert.ToDouble(dev[13]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[14] + "\r\n");
                    }
                    //因为在线路电流输出时既有一般线路的电流、两绕组和三绕组线路的电流还有接地电容器和电抗器的电流，因此只将电流输出就行了

                }

                strLineDL = readLineDL.ReadLine();
                j++;
                linenum++;
                wf.SetCaption(linenum.ToString());
            }
            readLineDL.Close();


        }
        public void FileReadDV(string projectSUID, string projectid, int dulutype, double ratecaplity, WaitDialogForm wf, string con, StringBuilder dianYaResult)
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
            string strLineDY;
            string[] arrayDY;
            char[] charSplitDY = new char[] { ' ' };
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
                wf.SetCaption(muxiannum.ToString());
            }
            readLineDY.Close();
        }
        public void FileReadV(string projectSUID, string projectid, int dulutype, double ratecaplity, WaitDialogForm wf, bool shortiflag, StringBuilder duanResult)
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
                wf.SetCaption(intshorti.ToString());
            }
            readLineGU.Close();
        }
        public void ALLShortThread(string projectSUID, string projectid, int dulutype, double ratecaplity, WaitDialogForm wf)
        {
            try
            {
                if (!CheckDL(projectSUID, projectid, ratecaplity))
                {
                    return;
                }
                System.Windows.Forms.Clipboard.Clear();
                Dictionary<int, double> nodeshorti = new Dictionary<int, double>();      //记录母线有没有进行过短路 
                KeyValuePair<int, double> maxshorti = new KeyValuePair<int, double>(); //取出短路的最大短路电流

                string con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='01'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
                PSPDEV pspDev = new PSPDEV();
                IList list1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type in ('05','03','02') AND PSPDEV.KSwitchStatus = '0'order by PSPDEV.number";
                IList list2 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);

                PSPDEV psp = new PSPDEV();
                psp = list2[0] as PSPDEV;
                string data = System.DateTime.Now.ToString("d");
                string time = System.DateTime.Now.ToString("T");
                string[,] dataDianliu = new string[65536, 15];

                StringBuilder duanResult = new StringBuilder();
                duanResult.Append("短路电流简表" + "\r\n" + "\r\n");
                duanResult.Append("短路作业号：1" + "\r\n");
                duanResult.Append("短路计算日期：" + data + " " + "时间：" + time + "\r\n");
                duanResult.Append("单位：kA" + "\r\n");
                StringBuilder dianYaResult = new StringBuilder();
                dianYaResult.Append("母线电压结果" + "\r\n" + "\r\n");
                dianYaResult.Append("短路作业号：1" + "\r\n");
                dianYaResult.Append("短路计算日期：" + data + " " + "时间：" + time + "\r\n");
                dianYaResult.Append("单位：幅值( p.u. )  角度(deg.)" + "\r\n");
                StringBuilder dianLiuResult = new StringBuilder();
                dianLiuResult.Append("支路电流结果" + "\r\n" + "\r\n");
                dianLiuResult.Append("短路作业号：1" + "\r\n");
                dianLiuResult.Append("短路计算日期：" + data + " " + "时间：" + time + "\r\n");
                dianLiuResult.Append("单位：幅值( p.u. )  角度(deg.)" + "\r\n");
                int intshorti = 0;        //第一行记录的为要读短路电流的属性说明
                bool shortiflag = false;
                int muxiannum = 0;         //记录一个母线短路后 有多少个记录母线电压
                int linenum = 0;           //记录一个母线短路 有多少个线路电流
                shortbuscir shortCutCal = new shortbuscir(Compuflag);
                string dlr = null;

                if (psp != null)
                {
                    dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "0 " + " " + dulutype;
                }
                else
                {
                    return;
                }

                FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\fault.txt"), FileMode.OpenOrCreate);
                StreamWriter str11 = new StreamWriter(VK);
                str11.Write(dlr);
                str11.Close();
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt");
                }
                shortCutCal.Show_shortcir(Compuflag, OutType, 0);


                string matrixstr = null;
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Zmatrixcheck.txt"))
                {
                    matrixstr = "正序导纳矩阵";
                    // matrixflag = false;
                }

                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Fmatrixcheck.txt"))
                {
                    // matrixflag = false;
                    matrixstr += "负序导纳矩阵";
                }

                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Lmatrixcheck.txt"))
                {
                    //matrixflag = false;
                    matrixstr += "零序导纳矩阵";
                }
                if (matrixstr != null)
                {
                    System.Windows.Forms.MessageBox.Show(matrixstr + "不存在逆矩阵，请调整参数后再进行计算！", "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    return;
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                {
                }
                else
                {
                    return;
                }
                ArrayList al = new ArrayList();
                FileReadV fv = new FileReadV(projectSUID, projectid, dulutype, ratecaplity, wf, shortiflag, duanResult);
                FileReadDLL fdl = new FileReadDLL(projectSUID, projectid, dulutype, ratecaplity, wf, con, dataDianliu);
                FileReadDV fdv = new FileReadDV(projectSUID, projectid, dulutype, ratecaplity, wf, con, dianYaResult);
                Thread shori = new Thread(new ThreadStart(fv.str));
                Thread dianya = new Thread(new ThreadStart(fdv.str));
                Thread dianliu = new Thread(new ThreadStart(fdl.str));
                shori.Start();
                if (OutType == 0)
                {
                    dianya.Start();
                    dianliu.Start();
                }


                //写入报表中
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result.csv"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\result.csv");
                }
                FileStream tempGU = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result.csv"), FileMode.OpenOrCreate);
                StreamWriter strGU = new StreamWriter(tempGU, Encoding.Default);
                strGU.Write(duanResult.ToString());
                strGU.Close();
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result1.csv"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                }
                FileStream tempDY = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result1.csv"), FileMode.OpenOrCreate);
                StreamWriter strDY = new StreamWriter(tempDY, Encoding.Default);
                strDY.Write(dianYaResult.ToString());
                strDY.Close();
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result2.csv"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\result2.csv");
                }
                FileStream tempDL = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result2.csv"), FileMode.OpenOrCreate);
                StreamWriter strDL = new StreamWriter(tempDL, Encoding.Default);
                strDL.Write(dianLiuResult);
                strDL.Close();
                PSP_ELCPROJECT psproject = new PSP_ELCPROJECT();
                psproject.ID = projectSUID;
                psproject = (PSP_ELCPROJECT)UCDeviceBase.DataService.GetObject("SelectPSP_ELCPROJECTByKey", psproject);
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "全网短路计算结果.xls"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "全网短路计算结果.xls");
                }

                Excel.Application ex;
                Excel.Worksheet xSheet;
                Excel.Application result1;
                Excel.Application result2;
                Excel.Worksheet tempSheet;
                Excel.Worksheet tempSheet1;
                Excel.Worksheet newWorksheet;
                Excel.Worksheet newWorkSheet1;

                object oMissing = System.Reflection.Missing.Value;
                ex = new Excel.Application();
                ex.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result.csv");

                xSheet = (Excel.Worksheet)ex.Worksheets[1];
                ex.Worksheets.Add(System.Reflection.Missing.Value, xSheet, 1, System.Reflection.Missing.Value);
                xSheet = (Excel.Worksheet)ex.Worksheets[2];
                ex.Worksheets.Add(System.Reflection.Missing.Value, xSheet, 1, System.Reflection.Missing.Value);
                xSheet = (Excel.Worksheet)ex.Worksheets[1];
                result1 = new Excel.Application();
                result1.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                result2 = new Excel.Application();
                result2.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result2.csv");
                tempSheet = (Excel.Worksheet)result1.Worksheets.get_Item(1);
                tempSheet1 = (Excel.Worksheet)result2.Worksheets.get_Item(1);
                newWorksheet = (Excel.Worksheet)ex.Worksheets.get_Item(2);
                newWorkSheet1 = (Excel.Worksheet)ex.Worksheets.get_Item(3);
                newWorksheet.Name = "母线电压";
                newWorkSheet1.Name = "支路电流";
                xSheet.Name = "短路电流";
                ex.Visible = true;

                tempSheet.Cells.Select();
                tempSheet.Cells.Copy(System.Reflection.Missing.Value);
                newWorksheet.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                tempSheet1.Cells.Select();
                tempSheet1.Cells.Copy(System.Reflection.Missing.Value);
                newWorkSheet1.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);


                while (shori.IsAlive || dianya.IsAlive || dianliu.IsAlive)
                {
                    newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[65536, 20]).Clear();
                    newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[65536, 20]).Value2 = dataDianliu;


                }
                xSheet.UsedRange.Font.Name = "楷体_GB2312";
                newWorksheet.UsedRange.Font.Name = "楷体_GB2312";
                newWorkSheet1.UsedRange.Font.Name = "楷体_GB2312";
                //记录的为短路电流格式
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 3]).MergeCells = true;
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Size = 20;
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Name = "黑体";
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xSheet.get_Range(xSheet.Cells[6, 1], xSheet.Cells[6, 3]).Interior.ColorIndex = 45;
                xSheet.get_Range(xSheet.Cells[7, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 1]).Interior.ColorIndex = 6;
                xSheet.get_Range(xSheet.Cells[4, 3], xSheet.Cells[xSheet.UsedRange.Rows.Count, 13]).NumberFormat = "0.0000_ ";
                //母线电压显示方式
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 14]).MergeCells = true;
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Size = 20;
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Name = "黑体";
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                if (OutType == 0)
                {
                    for (int m = 0; m < muxiannum - 1; m++)
                    {
                        newWorksheet.get_Range(newWorksheet.Cells[m * (muxiannum + 2) + 8, 1], newWorksheet.Cells[m * (muxiannum + 2) + 8, 14]).Interior.ColorIndex = 45;
                        newWorksheet.get_Range(newWorksheet.Cells[m * (muxiannum + 2) + 9, 1], newWorksheet.Cells[m * (muxiannum + 2) + 8 + muxiannum - 1, 1]).Interior.ColorIndex = 6;
                        newWorksheet.get_Range(newWorksheet.Cells[m * (muxiannum + 2) + 9, 3], newWorksheet.Cells[m * (muxiannum + 2) + 8 + muxiannum - 1, 13]).NumberFormat = "0.0000_ ";
                    }

                }

                //线路三序电流的显示方式
                newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 15]).MergeCells = true;
                newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 1]).Font.Size = 20;
                newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 1]).Font.Name = "黑体";
                newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                if (OutType == 0)
                {
                    for (int m = 0; m < muxiannum - 1; m++)
                    {
                        newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum + 2) + 8, 1], newWorkSheet1.Cells[m * (linenum + 2) + 8, 15]).Interior.ColorIndex = 45;
                        newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum + 2) + 9, 1], newWorkSheet1.Cells[m * (linenum + 2) + 8 + linenum - 1, 1]).Interior.ColorIndex = 6;
                        newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum + 2) + 9, 2], newWorkSheet1.Cells[m * (linenum + 2) + 8 + linenum - 1, 2]).Interior.ColorIndex = 6;
                        newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum + 2) + 9, 3], newWorkSheet1.Cells[m * (linenum + 2) + 8 + linenum - 1, 3]).Interior.ColorIndex = 6;
                        newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum + 2) + 9, 4], newWorkSheet1.Cells[m * (linenum + 2) + 8 + linenum - 1, 14]).NumberFormat = "0.0000_ ";
                    }
                }


                xSheet.Rows.AutoFit();
                xSheet.Columns.AutoFit();
                newWorksheet.Rows.AutoFit();
                newWorksheet.Columns.AutoFit();
                newWorkSheet1.Rows.AutoFit();
                newWorkSheet1.Columns.AutoFit();
                newWorksheet.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "全网短路计算结果.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);
                System.Windows.Forms.Clipboard.Clear();

                result1.Workbooks.Close();
                result1.Quit();
                result2.Workbooks.Close();
                result2.Quit();
                GC.Collect();
            }
            catch (System.Exception ex)
            {

            }

        }
        public void AllShortWJ(string projectSUID, string projectid, int dulutype, double ratecaplity, WaitDialogForm wf)
        {
            try
            {
                OutType = 1;
                if (Compuflag == 1)
                {
                    ElectricLoadCal elcc = new ElectricLoadCal();
                    elcc.LFCS(projectSUID, 1, (float)ratecaplity);
                    if (!CheckDLL(projectSUID, projectid, ratecaplity))
                    {
                        return;
                    }
                }
                else
                {
                    if (!CheckDL(projectSUID, projectid, ratecaplity))
                    {
                        return;
                    }
                }


                System.Windows.Forms.Clipboard.Clear();
                Dictionary<int, double> nodeshorti = new Dictionary<int, double>();      //记录母线有没有进行过短路 
                KeyValuePair<int, double> maxshorti = new KeyValuePair<int, double>(); //取出短路的最大短路电流

                string con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='01'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
                PSPDEV pspDev = new PSPDEV();
                IList list1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type in ('05','03','02') AND PSPDEV.KSwitchStatus = '0'order by PSPDEV.number";
                IList list2 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);

                PSPDEV psp = new PSPDEV();
                psp = list2[0] as PSPDEV;
                string data = System.DateTime.Now.ToString("d");
                string time = System.DateTime.Now.ToString("T");
                StringBuilder duanResult = new StringBuilder();
                duanResult.Append("短路电流简表" + "\r\n" + "\r\n");
                duanResult.Append("短路作业号：1" + "\r\n");
                duanResult.Append("短路计算日期：" + data + " " + "时间：" + time + "\r\n");
                duanResult.Append("单位：kA" + "\r\n");
                StringBuilder dianYaResult = new StringBuilder();
                dianYaResult.Append("母线电压结果" + "\r\n" + "\r\n");
                dianYaResult.Append("短路作业号：1" + "\r\n");
                dianYaResult.Append("短路计算日期：" + data + " " + "时间：" + time + "\r\n");
                dianYaResult.Append("单位：幅值( p.u. )  角度(deg.)" + "\r\n");
                StringBuilder dianLiuResult = new StringBuilder();
                dianLiuResult.Append("支路电流结果" + "\r\n" + "\r\n");
                dianLiuResult.Append("短路作业号：1" + "\r\n");
                dianLiuResult.Append("短路计算日期：" + data + " " + "时间：" + time + "\r\n");
                dianLiuResult.Append("单位：幅值( p.u. )  角度(deg.)" + "\r\n");
                int intshorti = 0;        //第一行记录的为要读短路电流的属性说明
                bool shortiflag = false;
                int muxiannum = 0;         //记录一个母线短路后 有多少个记录母线电压
                int linenum = 0;           //记录一个母线短路 有多少个线路电流
                shortbuscir shortCutCal = new shortbuscir(Compuflag);
                string dlr = null;
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\fault.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\fault.txt");
                }
                if (psp != null)
                {
                    dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "0 " + " " + dulutype;
                }
                else
                {
                    return;
                }

                FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\fault.txt"), FileMode.OpenOrCreate);
                StreamWriter str11 = new StreamWriter(VK);
                str11.Write(dlr);
                str11.Close();
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt");
                }
                shortCutCal.Show_shortcir(Compuflag, OutType, 0);


                string matrixstr = null;
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Zmatrixcheck.txt"))
                {
                    matrixstr = "正序导纳矩阵";
                    // matrixflag = false;
                }

                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Fmatrixcheck.txt"))
                {
                    // matrixflag = false;
                    matrixstr += "负序导纳矩阵";
                }

                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Lmatrixcheck.txt"))
                {
                    //matrixflag = false;
                    matrixstr += "零序导纳矩阵";
                }
                if (matrixstr != null)
                {
                    System.Windows.Forms.MessageBox.Show(matrixstr + "不存在逆矩阵，请调整参数后再进行计算！", "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    return;
                }
            }
            catch (System.Exception ex)
            {

            }
        }
        public void AllShort(string projectSUID, string projectid, int dulutype, double ratecaplity, frnReport wFrom)
        {
            try
            {
                wFrom.ShowText += "\r\n开始准备短路计算数据\t" + System.DateTime.Now.ToString();
                //OutType = 1;
                if (Compuflag == 1)
                {
                    ElectricLoadCal elcc = new ElectricLoadCal();
                    elcc.LFCS(projectSUID, 1, (float)ratecaplity);
                    if (!CheckDLL(projectSUID, projectid, ratecaplity))
                    {
                        return;
                    }
                }
                else
                {
                    if (!CheckDL(projectSUID, projectid, ratecaplity))
                    {
                        return;
                    }
                }


                System.Windows.Forms.Clipboard.Clear();
                Dictionary<int, double> nodeshorti = new Dictionary<int, double>();      //记录母线有没有进行过短路 
                KeyValuePair<int, double> maxshorti = new KeyValuePair<int, double>(); //取出短路的最大短路电流

                string con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='01'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
                PSPDEV pspDev = new PSPDEV();
                IList list1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type in ('05','03','02') AND PSPDEV.KSwitchStatus = '0'order by PSPDEV.number";
                IList list2 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);

                PSPDEV psp = new PSPDEV();
                psp = list2[0] as PSPDEV;
                string data = System.DateTime.Now.ToString("d");
                string time = System.DateTime.Now.ToString("T");
                StringBuilder duanResult = new StringBuilder();
                duanResult.Append("短路电流简表" + "\r\n" + "\r\n");
                duanResult.Append("短路作业号：1" + "\r\n");
                duanResult.Append("短路计算日期：" + data + " " + "时间：" + time + "\r\n");
                duanResult.Append("单位：kA" + "\r\n");
                StringBuilder dianYaResult = new StringBuilder();
                dianYaResult.Append("母线电压结果" + "\r\n" + "\r\n");
                dianYaResult.Append("短路作业号：1" + "\r\n");
                dianYaResult.Append("短路计算日期：" + data + " " + "时间：" + time + "\r\n");
                dianYaResult.Append("单位：幅值( p.u. )  角度(deg.)" + "\r\n");
                StringBuilder dianLiuResult = new StringBuilder();
                dianLiuResult.Append("支路电流结果" + "\r\n" + "\r\n");
                dianLiuResult.Append("短路作业号：1" + "\r\n");
                dianLiuResult.Append("短路计算日期：" + data + " " + "时间：" + time + "\r\n");
                dianLiuResult.Append("单位：幅值( p.u. )  角度(deg.)" + "\r\n");
                int intshorti = 0;        //第一行记录的为要读短路电流的属性说明
                bool shortiflag = false;
                int muxiannum = 0;         //记录一个母线短路后 有多少个记录母线电压
                int linenum = 0;           //记录一个母线短路 有多少个线路电流
                shortbuscir shortCutCal = new shortbuscir(Compuflag);
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\fault.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\fault.txt");
                }
                string dlr = null;

                if (psp != null)
                {
                    dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "0 " + " " + dulutype;
                }
                else
                {
                    return;
                }

                FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\fault.txt"), FileMode.OpenOrCreate);
                StreamWriter str11 = new StreamWriter(VK);
                str11.Write(dlr);
                str11.Close();
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt");
                }
                wFrom.ShowText += "\r\n开始进行短路计算\t" + System.DateTime.Now.ToString();
                shortCutCal.Show_shortcir(Compuflag, OutType, 0);


                string matrixstr = null;
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Zmatrixcheck.txt"))
                {
                    matrixstr = "正序导纳矩阵";
                    // matrixflag = false;
                }

                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Fmatrixcheck.txt"))
                {
                    // matrixflag = false;
                    matrixstr += "负序导纳矩阵";
                }

                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Lmatrixcheck.txt"))
                {
                    //matrixflag = false;
                    matrixstr += "零序导纳矩阵";
                }
                if (matrixstr != null)
                {
                    System.Windows.Forms.MessageBox.Show(matrixstr + "不存在逆矩阵，请调整参数后再进行计算！", "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    return;
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                {
                }
                else
                {
                    wFrom.ShowText += "\r\n短路计算失败\t" + System.DateTime.Now.ToString();
                    return;
                }
                wFrom.ShowText += "\r\n开始处理短路计算结果\t" + System.DateTime.Now.ToString();
                FileStream shorcuit = new FileStream(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt", FileMode.Open);
                StreamReader readLineGU = new StreamReader(shorcuit, System.Text.Encoding.Default);
                string strLineGU;
                string[] arrayGU;
                char[] charSplitGU = new char[] { ' ' };
                intshorti = 0;
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
                   // wf.SetCaption(intshorti.ToString());
                }
                readLineGU.Close();
                if (OutType == 0)
                {
                    //**读取三序电压的值
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt"))
                    {
                    }
                    else
                    {
                        wFrom.ShowText += "\r\n短路计算失败\t" + System.DateTime.Now.ToString();
                        return;
                    }
                    FileStream dianYa = new FileStream(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt", FileMode.Open);
                    StreamReader readLineDY = new StreamReader(dianYa, System.Text.Encoding.Default);
                    string strLineDY;
                    string[] arrayDY;
                    char[] charSplitDY = new char[] { ' ' };
                    strLineDY = readLineDY.ReadLine();
                    int j = 0;
                    muxiannum = 0;
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
                                muxiannum++;
                            }
                            else
                            {
                                bool dianyaflag = true;     //判断此母线是短路点母线还是一般的母线
                                PSPDEV CR = new PSPDEV();

                                if (dev[1] != "du")
                                {
                                    con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='01'AND PSPDEV.Name='" + dev[1] + "'";
                                    //con = " WHERE Name='" + dev[1] + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
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
                       
                        j++;
                        //wf.SetCaption(muxiannum.ToString());
                    }
                    readLineDY.Close();
                    //**读取三序电流的值
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt"))
                    {
                    }
                    else
                    {
                        wFrom.ShowText += "\r\n短路计算失败\t" + System.DateTime.Now.ToString();
                        return;
                    }
                    FileStream dianLiu = new FileStream(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt", FileMode.Open);
                    StreamReader readLineDL = new StreamReader(dianLiu, System.Text.Encoding.Default);
                    string strLineDL;
                    string[] arrayDL;
                    char[] charSplitDL = new char[] { ' ' };
                    strLineDL = readLineDL.ReadLine();
                    j = 0;
                    int jxflag = 0;   //记录第一条母线短路时的线路个数
                    linenum = 0;
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
                            dianLiuResult.Append(dev[0] + "," + dev[1] + "," + dev[2] + "," + dev[3] + "," + dev[4] + "," + dev[5] + "," + dev[6] + "," + dev[7] + "," + dev[8] + "," +
                                         dev[9] + "," + dev[10] + "," + dev[11] + "," + dev[12] + "," + dev[13] + "," + dev[14] + "\r\n");
                        }
                        else
                        {
                            if (dev[0] == "故障母线")
                            {
                                dianLiuResult.Append("\r\n" + "故障母线：" + dev[1] + "\r\n");
                                jxflag++;
                            }
                            else
                            {
                                PSPDEV CR = new PSPDEV();

                                if (dev[0] != "du")
                                {
                                    con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='01'AND PSPDEV.Name='" + dev[0] + "'";
                                    //con = " WHERE Name='" + dev[0] + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                                }
                                else
                                    con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='01'AND PSPDEV.Name='" + dev[1] + "'";
                                    //con = " WHERE Name='" + dev[1] + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";

                                CR = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);

                                dianLiuResult.Append(dev[0] + "," + dev[1] + "," + dev[2] + "," + Convert.ToDouble(dev[3]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[4] + "," + Convert.ToDouble(dev[5]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[6] + "," + Convert.ToDouble(dev[7]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[8] + "," +
                                  Convert.ToDouble(dev[9]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[10] + "," + Convert.ToDouble(dev[11]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[12] + "," + Convert.ToDouble(dev[13]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[14] + "\r\n");
                            }
                            //因为在线路电流输出时既有一般线路的电流、两绕组和三绕组线路的电流还有接地电容器和电抗器的电流，因此只将电流输出就行了

                        }

                        strLineDL = readLineDL.ReadLine();
                        j++;
                        if (jxflag==1)
                        {
                            linenum++;
                        }
                        
                       // wf.SetCaption(linenum.ToString());
                    }
                    readLineDL.Close();

                }
                wFrom.ShowText += "\r\n开始生成报表\t" + System.DateTime.Now.ToString();
                //写入报表中
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result.csv"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\result.csv");
                }
                FileStream tempGU = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result.csv"), FileMode.OpenOrCreate);
                StreamWriter strGU = new StreamWriter(tempGU, Encoding.Default);
                strGU.Write(duanResult.ToString());
                strGU.Close();
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result1.csv"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                }
                FileStream tempDY = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result1.csv"), FileMode.OpenOrCreate);
                StreamWriter strDY = new StreamWriter(tempDY, Encoding.Default);
                strDY.Write(dianYaResult.ToString());
                strDY.Close();
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result2.csv"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\result2.csv");
                }
                FileStream tempDL = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result2.csv"), FileMode.OpenOrCreate);
                StreamWriter strDL = new StreamWriter(tempDL, Encoding.Default);
                strDL.Write(dianLiuResult);
                strDL.Close();
                PSP_ELCPROJECT psproject = new PSP_ELCPROJECT();
                psproject.ID = projectSUID;
                psproject = (PSP_ELCPROJECT)UCDeviceBase.DataService.GetObject("SelectPSP_ELCPROJECTByKey", psproject);
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "全网短路计算结果.xls"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "全网短路计算结果.xls");
                }

                Excel.Application ex;
                Excel.Worksheet xSheet;
                Excel.Application result1;
                Excel.Application result2;
                Excel.Worksheet tempSheet;
                Excel.Worksheet tempSheet1;
                Excel.Worksheet newWorksheet;
                Excel.Worksheet newWorkSheet1;

                object oMissing = System.Reflection.Missing.Value;
                ex = new Excel.Application();
                ex.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result.csv");

                xSheet = (Excel.Worksheet)ex.Worksheets[1];
              
               
                xSheet.Name = "短路电流";
                ex.Visible = true;

               

                xSheet.UsedRange.Font.Name = "楷体_GB2312";
               
                //记录的为短路电流格式
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 3]).MergeCells = true;
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Size = 20;
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Name = "黑体";
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xSheet.get_Range(xSheet.Cells[6, 1], xSheet.Cells[6, 3]).Interior.ColorIndex = 45;
                xSheet.get_Range(xSheet.Cells[7, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 1]).Interior.ColorIndex = 6;
                xSheet.get_Range(xSheet.Cells[4, 3], xSheet.Cells[xSheet.UsedRange.Rows.Count, 13]).NumberFormat = "0.0000_ ";
                if (muxiannum>50)
                {
                    if (OutType == 0)
                    {
                         if (MessageBox.Show("在显示三序电压时，总得数据超出了报表的承受范围，选择是否输出?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
                     {
                         OutType=1;
                     }
                    }
                   
                    
                }
                if (OutType == 0)
                {
                    ex.Worksheets.Add(System.Reflection.Missing.Value, xSheet, 1, System.Reflection.Missing.Value);
                    xSheet = (Excel.Worksheet)ex.Worksheets[2];
                    ex.Worksheets.Add(System.Reflection.Missing.Value, xSheet, 1, System.Reflection.Missing.Value);
                    xSheet = (Excel.Worksheet)ex.Worksheets[1];
                    result1 = new Excel.Application();
                    result1.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                    result2 = new Excel.Application();
                    result2.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result2.csv");
                    tempSheet = (Excel.Worksheet)result1.Worksheets.get_Item(1);
                    tempSheet1 = (Excel.Worksheet)result2.Worksheets.get_Item(1);
                    newWorksheet = (Excel.Worksheet)ex.Worksheets.get_Item(2);
                    newWorkSheet1 = (Excel.Worksheet)ex.Worksheets.get_Item(3);
                    newWorksheet.Name = "母线电压";
                    newWorkSheet1.Name = "支路电流";
                    tempSheet.Cells.Select();
                    tempSheet.Cells.Copy(System.Reflection.Missing.Value);
                    newWorksheet.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                    tempSheet1.Cells.Select();
                    tempSheet1.Cells.Copy(System.Reflection.Missing.Value);
                    newWorkSheet1.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                    newWorksheet.UsedRange.Font.Name = "楷体_GB2312";
                    newWorkSheet1.UsedRange.Font.Name = "楷体_GB2312";
                    //母线电压显示方式
                    newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 14]).MergeCells = true;
                    newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Size = 20;
                    newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Name = "黑体";
                    newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    if (OutType == 0)
                    {
                        for (int m = 0; m < muxiannum ; m++)
                        {
                            newWorksheet.get_Range(newWorksheet.Cells[m * (muxiannum + 2) + 8, 1], newWorksheet.Cells[m * (muxiannum + 2) + 8, 14]).Interior.ColorIndex = 45;
                            newWorksheet.get_Range(newWorksheet.Cells[m * (muxiannum + 2) + 9, 1], newWorksheet.Cells[m * (muxiannum + 2) + 8 + muxiannum, 1]).Interior.ColorIndex = 6;
                            newWorksheet.get_Range(newWorksheet.Cells[m * (muxiannum + 2) + 9, 3], newWorksheet.Cells[m * (muxiannum + 2) + 8 + muxiannum - 1, 13]).NumberFormat = "0.0000_ ";
                        }

                    }

                    ////线路三序电流的显示方式
                    newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 15]).MergeCells = true;
                    newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 1]).Font.Size = 20;
                    newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 1]).Font.Name = "黑体";
                    newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    if (OutType == 0)
                    {
                        for (int m = 0; m < muxiannum; m++)
                        {
                            newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum-1 + 2) + 8, 1], newWorkSheet1.Cells[m * (linenum-1 + 2) + 8, 15]).Interior.ColorIndex = 45;
                            newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum - 1 + 2) + 9, 1], newWorkSheet1.Cells[m * (linenum - 1 + 2) + 8 + linenum - 1, 1]).Interior.ColorIndex = 6;
                            newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum - 1 + 2) + 9, 2], newWorkSheet1.Cells[m * (linenum - 1 + 2) + 8 + linenum - 1, 2]).Interior.ColorIndex = 6;
                            newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum - 1 + 2) + 9, 3], newWorkSheet1.Cells[m * (linenum - 1 + 2) + 8 + linenum - 1, 3]).Interior.ColorIndex = 6;
                            newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum - 1 + 2) + 9, 4], newWorkSheet1.Cells[m * (linenum - 1 + 2) + 8 + linenum - 1, 14]).NumberFormat = "0.0000_ ";
                        }
                    }
                    System.Windows.Forms.Clipboard.Clear();
                    newWorksheet.Rows.AutoFit();
                    newWorksheet.Columns.AutoFit();
                    newWorkSheet1.Rows.AutoFit();
                    newWorkSheet1.Columns.AutoFit();
                    newWorksheet.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "全网短路计算结果.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);
                    result1.Workbooks.Close();
                    result1.Quit();
                    result2.Workbooks.Close();
                    result2.Quit();
                }


                xSheet.Rows.AutoFit();
                xSheet.Columns.AutoFit();
               
                System.Windows.Forms.Clipboard.Clear();
                ex.DisplayAlerts = false;
               
                wFrom.ShowText += "\r\n短路计算结束\t" + System.DateTime.Now.ToString();
            }
            catch (System.Exception ex)
            {
                wFrom.ShowText += "\r\n短路计算失败\t" + System.DateTime.Now.ToString();
                MessageBox.Show("短路数据存在问题，阻抗矩阵不存在或者存在孤立节点，请查证后再进行！");
            }

        }
        public void AllShorti(string projectSUID, string projectid, int dulutype, double ratecaplity, Dictionary<string, double> shortcap) //将所有的短路电流进行不留
        {
            try
            {

                //OutType = 1;
                if (Compuflag == 1)
                {
                    ElectricLoadCal elcc = new ElectricLoadCal();
                    elcc.LFCS(projectSUID, 1, (float)ratecaplity);
                    if (!CheckDLL(projectSUID, projectid, ratecaplity))
                    {
                        return;
                    }
                }
                else
                {
                    if (!CheckDL(projectSUID, projectid, ratecaplity))
                    {
                        return;
                    }
                }


                System.Windows.Forms.Clipboard.Clear();
                Dictionary<int, double> nodeshorti = new Dictionary<int, double>();      //记录母线有没有进行过短路 
                KeyValuePair<int, double> maxshorti = new KeyValuePair<int, double>(); //取出短路的最大短路电流

                string con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='01'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
                PSPDEV pspDev = new PSPDEV();
                IList list1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type in ('05','03','02') AND PSPDEV.KSwitchStatus = '0'order by PSPDEV.number";
                IList list2 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);

                PSPDEV psp = new PSPDEV();
                psp = list2[0] as PSPDEV;
                string data = System.DateTime.Now.ToString("d");
                string time = System.DateTime.Now.ToString("T");

                int intshorti = 0;        //第一行记录的为要读短路电流的属性说明
                bool shortiflag = false;
                int muxiannum = 0;         //记录一个母线短路后 有多少个记录母线电压
                int linenum = 0;           //记录一个母线短路 有多少个线路电流
                shortbuscir shortCutCal = new shortbuscir(Compuflag);
                string dlr = null;

                if (psp != null)
                {
                    dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "0 " + " " + dulutype;
                }
                else
                {
                    return;
                }

                FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\fault.txt"), FileMode.OpenOrCreate);
                StreamWriter str11 = new StreamWriter(VK);
                str11.Write(dlr);
                str11.Close();
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt");
                }
                shortCutCal.Show_shortcir(Compuflag, OutType, 0);


                string matrixstr = null;
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Zmatrixcheck.txt"))
                {
                    matrixstr = "正序导纳矩阵";
                    // matrixflag = false;
                }

                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Fmatrixcheck.txt"))
                {
                    // matrixflag = false;
                    matrixstr += "负序导纳矩阵";
                }

                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Lmatrixcheck.txt"))
                {
                    //matrixflag = false;
                    matrixstr += "零序导纳矩阵";
                }
                if (matrixstr != null)
                {
                    System.Windows.Forms.MessageBox.Show(matrixstr + "不存在逆矩阵，请调整参数后再进行计算！", "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    return;
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                {
                }
                else
                {
                    return;
                }

                FileStream shorcuit = new FileStream(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt", FileMode.Open);
                StreamReader readLineGU = new StreamReader(shorcuit, System.Text.Encoding.Default);
                string strLineGU;
                string[] arrayGU;
                char[] charSplitGU = new char[] { ' ' };
                intshorti = 0;
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
                            //duanResult.Append(shorti[0] + "," + shorti[1] + "," + shorti[3] + "\r\n");
                            shortiflag = true;
                        }

                    }
                    else
                        shortcap[shorti[1]] = Convert.ToDouble(shorti[3]);

                    intshorti++;
                    //wf.SetCaption(intshorti.ToString());
                }
                readLineGU.Close();


            }
            catch (System.Exception ex)
            {
                MessageBox.Show("短路数据存在问题，阻抗矩阵不存在或者存在孤立节点，请查证后再进行！");
            }
        }
        public void Allshort(string projectSUID, string projectid, int dulutype, double ratecaplity)
        {
            int cishu = 0;           //记录第多少次出现内存问题
            try
            {


                if (!CheckDL(projectSUID, projectid, ratecaplity))
                {
                    return;
                }
                System.Windows.Forms.Clipboard.Clear();
                Dictionary<int, double> nodeshorti = new Dictionary<int, double>();      //记录母线有没有进行过短路 
                KeyValuePair<int, double> maxshorti = new KeyValuePair<int, double>(); //取出短路的最大短路电流

                string con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='01'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
                PSPDEV pspDev = new PSPDEV();
                IList list1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);

                PSPDEV psp = new PSPDEV();

                string data = System.DateTime.Now.ToString("d");
                string time = System.DateTime.Now.ToString("T");
                string duanResult = null;
                duanResult += "短路电流简表" + "\r\n" + "\r\n";
                duanResult += "短路作业号：1" + "\r\n";
                duanResult += "短路计算日期：" + data + " " + "时间：" + time + "\r\n";
                duanResult += "单位：kA" + "\r\n";
                string dianYaResult = null;
                dianYaResult += "母线电压结果" + "\r\n" + "\r\n";
                dianYaResult += "短路作业号：1" + "\r\n";
                dianYaResult += "短路计算日期：" + data + " " + "时间：" + time + "\r\n";
                dianYaResult += "单位：幅值( p.u. )  角度(deg.)" + "\r\n";
                string dianLiuResult = null;
                dianLiuResult += "支路电流结果" + "\r\n" + "\r\n";
                dianLiuResult += "短路作业号：1" + "\r\n";
                dianLiuResult += "短路计算日期：" + data + " " + "时间：" + time + "\r\n";
                dianLiuResult += "单位：幅值( p.u. )  角度(deg.)" + "\r\n";
                int intshorti = 0;        //第一行记录的为要读短路电流的属性说明
                bool shortiflag = false;
                int muxiannum = 0;         //记录一个母线短路后 有多少个记录母线电压
                int linenum = 0;           //记录一个母线短路 有多少个线路电流
                shortbuscir shortCutCal = new shortbuscir(Compuflag);
                for (int i = 0; i < list1.Count; i++)
                {
                    cishu++;
                    pspDev = list1[i] as PSPDEV;
                    bool flag = false;
                    string dlr = null;
                    con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='05'AND(PSPDEV.IName='" + pspDev.Name + "'OR PSPDEV.JName='" + pspDev.Name + "') AND PSPDEV.KSwitchStatus = '0'order by PSPDEV.number";
                    IList list2 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    for (int j = 0; j < list2.Count; j++)
                    {
                        psp = list2[j] as PSPDEV;
                        con = " WHERE Name='" + psp.ISwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                        IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                        con = " WHERE Name='" + psp.JSwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                        IList listjswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                        PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                        PSPDEV pspjswitch = (PSPDEV)listjswitch[0];

                        if (pspDev.Number == psp.FirstNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                        {

                            flag = true;
                            dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "0 " + " " + dulutype;

                        }
                        if (pspDev.Number == psp.LastNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                        {
                            flag = true;
                            dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "1 " + " " + dulutype;
                        }
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\fault.txt"))
                        {
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\fault.txt");
                        }
                        if (flag)
                        {

                            break;                 //跳出本循环 进行母线的另外一个母线短路
                        }
                        if (!flag)
                            continue;
                        //写入错误中
                    }
                    //如果在一般线路中没有则在两绕组中进行
                    if (!flag)
                    {
                        con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='02'AND PSPDEV.KSwitchStatus = '0'and (PSPDEV.IName='" + pspDev.Name + "'OR PSPDEV.JName='" + pspDev.Name + "') order by PSPDEV.number";
                        IList list3 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                        for (int j = 0; j < list3.Count; j++)
                        {
                            dlr = null;
                            psp = list3[j] as PSPDEV;
                            //PSPDEV devFirst = new PSPDEV();

                            //con = " WHERE Name='" + psp.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            //devFirst = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                            //PSPDEV devLast = new PSPDEV();


                            //con = " WHERE Name='" + psp.JName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            //devLast = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + psp.ISwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + psp.JSwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listjswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                            PSPDEV pspjswitch = (PSPDEV)listjswitch[0];
                            if (pspDev.Number == psp.FirstNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                            {

                                flag = true;
                                dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "0" + " " + dulutype;

                            }
                            if (pspDev.Number == psp.LastNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                            {
                                flag = true;
                                dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "1" + " " + dulutype;
                            }
                            if (flag)
                            {
                                break;                 //跳出本循环 进行母线的另外一个母线短路
                            }
                            if (!flag)
                                continue;
                            //写入错误中
                        }
                    }
                    if (!flag)
                    {
                        con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='03'AND(PSPDEV.IName='" + pspDev.Name + "'OR PSPDEV.JName='" + pspDev.Name + "'OR PSPDEV.KName='" + pspDev.Name + "')AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
                        IList list4 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                        for (int j = 0; j < list4.Count; j++)
                        {
                            dlr = null;
                            psp = list4[j] as PSPDEV;
                            //PSPDEV devINode = new PSPDEV();

                            //con = " WHERE Name='" + psp.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            //devINode = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                            //PSPDEV devJNode = new PSPDEV();

                            //con = " WHERE Name='" + psp.JName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            //devJNode = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                            //PSPDEV devKNode = new PSPDEV();

                            //con = " WHERE Name='" + psp.KName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            //devKNode = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);

                            con = " WHERE Name='" + psp.ISwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + psp.JSwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listjswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + psp.HuganLine1 + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listkswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                            PSPDEV pspjswitch = (PSPDEV)listjswitch[0];
                            PSPDEV pspkswitch = (PSPDEV)listkswitch[0];
                            if (pspDev.Number == psp.FirstNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0" && pspkswitch.KSwitchStatus == "0")
                            {

                                flag = true;
                                dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "0" + " " + dulutype;

                            }
                            if (pspDev.Number == psp.LastNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0" && pspkswitch.KSwitchStatus == "0")
                            {
                                flag = true;
                                dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "1" + " " + dulutype;
                            }
                            if (pspDev.Number == psp.Flag && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0" && pspkswitch.KSwitchStatus == "0")
                            {
                                flag = true;
                                dlr = "0" + " " + psp.FirstNode + " " + psp.Flag + " " + psp.Number + " " + "1" + " " + dulutype;
                            }

                            if (flag)
                            {
                                break;                 //跳出本循环 进行母线的另外一个母线短路
                            }
                            if (!flag)
                                continue;
                            //写入错误中
                        }
                    }
                    if (flag)
                    {
                        FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\fault.txt"), FileMode.OpenOrCreate);
                        StreamWriter str11 = new StreamWriter(VK);
                        str11.Write(dlr);
                        str11.Close();
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                        {
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt");
                        }
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt"))
                        {
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt");
                        }
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt"))
                        {
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt");
                        }

                        shortCutCal.Show_shortcir(Compuflag, OutType, 1);
                        GC.Collect();
                        //bool matrixflag=true;                //用来判断是否导纳矩阵的逆矩阵是否存在逆矩阵
                        string matrixstr = null;
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Zmatrixcheck.txt"))
                        {
                            matrixstr = "正序导纳矩阵";
                            // matrixflag = false;
                        }

                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Fmatrixcheck.txt"))
                        {
                            // matrixflag = false;
                            matrixstr += "负序导纳矩阵";
                        }

                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Lmatrixcheck.txt"))
                        {
                            //matrixflag = false;
                            matrixstr += "零序导纳矩阵";
                        }
                        if (matrixstr != null)
                        {
                            System.Windows.Forms.MessageBox.Show(matrixstr + "不存在逆矩阵，请调整参数后再进行计算！", "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                            return;
                        }
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                        {
                        }
                        else
                        {
                            return;
                        }

                        FileStream shorcuit = new FileStream(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt", FileMode.Open);
                        StreamReader readLineGU = new StreamReader(shorcuit, System.Text.Encoding.Default);
                        string strLineGU;
                        string[] arrayGU;
                        char[] charSplitGU = new char[] { ' ' };
                        intshorti = 0;
                        while ((strLineGU = readLineGU.ReadLine()) != null)
                        {


                            arrayGU = strLineGU.Split(charSplitGU);
                            string[] shorti = new string[4];
                            shorti.Initialize();
                            int m = 0;
                            foreach (string str in arrayGU)
                            {

                                if (str != "")
                                {

                                    shorti[m++] = str.ToString();

                                }
                            }
                            if (intshorti == 0)
                            {
                                if (!shortiflag)
                                {
                                    duanResult += shorti[0] + "," + shorti[1] + "," + shorti[3] + "\r\n";
                                    shortiflag = true;
                                }

                            }
                            else
                                duanResult += shorti[0] + "," + shorti[1] + "," + Convert.ToDouble(shorti[3]) * ratecaplity / (Math.Sqrt(3) * pspDev.ReferenceVolt) + "\r\n";

                            intshorti++;
                        }
                        readLineGU.Close();
                        if (OutType == 0)
                        {
                            //**读取三序电压的值
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt"))
                            {
                            }
                            else
                            {
                                return;
                            }
                            FileStream dianYa = new FileStream(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt", FileMode.Open);
                            StreamReader readLineDY = new StreamReader(dianYa, System.Text.Encoding.Default);
                            string strLineDY;
                            string[] arrayDY;
                            char[] charSplitDY = new char[] { ' ' };
                            strLineDY = readLineDY.ReadLine();
                            int j = 0;
                            muxiannum = 0;
                            while (strLineDY != null)
                            {
                                arrayDY = strLineDY.Split(charSplitDY);

                                int m = 0;
                                string[] dev = new string[14];
                                dev.Initialize();
                                foreach (string str in arrayDY)
                                {
                                    if (str != "")
                                    {
                                        dev[m++] = str;
                                    }
                                }
                                if (j == 0)
                                {
                                    dianYaResult += "\r\n" + "故障母线：" + pspDev.Name + "\r\n";
                                    dianYaResult += dev[0] + "," + dev[1] + "," + dev[2] + "," + dev[3] + "," + dev[4] + "," + dev[5] + "," + dev[6] + "," + dev[7] + "," + dev[8] + "," +
             dev[9] + "," + dev[10] + "," + dev[11] + "," + dev[12] + "," + dev[13] + "\r\n";
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
                                        dianYaResult += dev[0] + "," + dev[1] + "," + Convert.ToDouble(dev[2]) * CR.ReferenceVolt + "," + dev[3] + "," + Convert.ToDouble(dev[4]) * CR.ReferenceVolt + "," + dev[5] + "," + Convert.ToDouble(dev[6]) * CR.ReferenceVolt + "," + dev[7] + "," + Convert.ToDouble(dev[8]) * CR.ReferenceVolt + "," +
                                            dev[9] + "," + Convert.ToDouble(dev[10]) * CR.ReferenceVolt + "," + dev[11] + "," + Convert.ToDouble(dev[12]) * CR.ReferenceVolt + "," + dev[13] + "\r\n";
                                    //else
                                    //    dianYaResult += dev[0] + "," + duanluname + "上短路点" + "," + Convert.ToDouble(dev[2]) * CR.ReferenceVolt + "," + dev[3] + "," + Convert.ToDouble(dev[4]) * CR.ReferenceVolt + "," + dev[5] + "," + Convert.ToDouble(dev[6]) * CR.ReferenceVolt + "," + dev[7] + "," + Convert.ToDouble(dev[8]) * CR.ReferenceVolt + "," +
                                    //       dev[9] + "," + Convert.ToDouble(dev[10]) * CR.ReferenceVolt + "," + dev[11] + Convert.ToDouble(dev[12]) * CR.ReferenceVolt + "," + dev[13] + "\r\n";

                                }
                                strLineDY = readLineDY.ReadLine();
                                muxiannum++;
                                j++;
                            }
                            readLineDY.Close();
                            //**读取三序电流的值
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt"))
                            {
                            }
                            else
                            {
                                return;
                            }
                            FileStream dianLiu = new FileStream(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt", FileMode.Open);
                            StreamReader readLineDL = new StreamReader(dianLiu, System.Text.Encoding.Default);
                            string strLineDL;
                            string[] arrayDL;
                            char[] charSplitDL = new char[] { ' ' };
                            strLineDL = readLineDL.ReadLine();
                            j = 0;
                            linenum = 0;
                            while (strLineDL != null)
                            {
                                arrayDL = strLineDL.Split(charSplitDL);
                                int m = 0;
                                string[] dev = new string[15];
                                dev.Initialize();
                                foreach (string str in arrayDL)
                                {
                                    if (str != "")
                                    {
                                        dev[m++] = str;
                                    }
                                }
                                if (j == 0)
                                {
                                    dianLiuResult += "\r\n" + "故障母线：" + pspDev.Name + "\r\n";
                                    dianLiuResult += dev[0] + "," + dev[1] + "," + dev[2] + "," + dev[3] + "," + dev[4] + "," + dev[5] + "," + dev[6] + "," + dev[7] + "," + dev[8] + "," +
                                                 dev[9] + "," + dev[10] + "," + dev[11] + "," + dev[12] + "," + dev[13] + "," + dev[14] + "\r\n";
                                }
                                else
                                {

                                    //因为在线路电流输出时既有一般线路的电流、两绕组和三绕组线路的电流还有接地电容器和电抗器的电流，因此只将电流输出就行了
                                    PSPDEV CR = new PSPDEV();

                                    if (dev[0] != "du")
                                    {

                                        con = " WHERE Name='" + dev[0] + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                                    }
                                    else
                                        con = " WHERE Name='" + dev[1] + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";

                                    CR = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);

                                    dianLiuResult += dev[0] + "," + dev[1] + "," + dev[2] + "," + Convert.ToDouble(dev[3]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[4] + "," + Convert.ToDouble(dev[5]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[6] + "," + Convert.ToDouble(dev[7]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[8] + "," +
                                      Convert.ToDouble(dev[9]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[10] + "," + Convert.ToDouble(dev[11]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[12] + "," + Convert.ToDouble(dev[13]) * ratecaplity / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[14] + "\r\n";
                                }

                                strLineDL = readLineDL.ReadLine();
                                j++;
                                linenum++;
                            }
                            readLineDL.Close();

                        }
                    }

                }
                //写入报表中
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result.csv"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\result.csv");
                }
                FileStream tempGU = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result.csv"), FileMode.OpenOrCreate);
                StreamWriter strGU = new StreamWriter(tempGU, Encoding.Default);
                strGU.Write(duanResult);
                strGU.Close();
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result1.csv"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                }
                FileStream tempDY = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result1.csv"), FileMode.OpenOrCreate);
                StreamWriter strDY = new StreamWriter(tempDY, Encoding.Default);
                strDY.Write(dianYaResult);
                strDY.Close();
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result2.csv"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\result2.csv");
                }
                FileStream tempDL = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result2.csv"), FileMode.OpenOrCreate);
                StreamWriter strDL = new StreamWriter(tempDL, Encoding.Default);
                strDL.Write(dianLiuResult);
                strDL.Close();
                PSP_ELCPROJECT psproject = new PSP_ELCPROJECT();
                psproject.ID = projectSUID;
                psproject = (PSP_ELCPROJECT)UCDeviceBase.DataService.GetObject("SelectPSP_ELCPROJECTByKey", psproject);
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "全网短路计算结果.xls"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "全网短路计算结果.xls");
                }

                Excel.Application ex;
                Excel.Worksheet xSheet;
                Excel.Application result1;
                Excel.Application result2;
                Excel.Worksheet tempSheet;
                Excel.Worksheet tempSheet1;
                Excel.Worksheet newWorksheet;
                Excel.Worksheet newWorkSheet1;

                object oMissing = System.Reflection.Missing.Value;
                ex = new Excel.Application();
                ex.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result.csv");

                xSheet = (Excel.Worksheet)ex.Worksheets[1];
                ex.Worksheets.Add(System.Reflection.Missing.Value, xSheet, 1, System.Reflection.Missing.Value);
                xSheet = (Excel.Worksheet)ex.Worksheets[2];
                ex.Worksheets.Add(System.Reflection.Missing.Value, xSheet, 1, System.Reflection.Missing.Value);
                xSheet = (Excel.Worksheet)ex.Worksheets[1];
                result1 = new Excel.Application();
                result1.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                result2 = new Excel.Application();
                result2.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result2.csv");
                tempSheet = (Excel.Worksheet)result1.Worksheets.get_Item(1);
                tempSheet1 = (Excel.Worksheet)result2.Worksheets.get_Item(1);
                newWorksheet = (Excel.Worksheet)ex.Worksheets.get_Item(2);
                newWorkSheet1 = (Excel.Worksheet)ex.Worksheets.get_Item(3);
                newWorksheet.Name = "母线电压";
                newWorkSheet1.Name = "支路电流";
                xSheet.Name = "短路电流";
                ex.Visible = true;

                tempSheet.Cells.Select();
                tempSheet.Cells.Copy(System.Reflection.Missing.Value);
                newWorksheet.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                tempSheet1.Cells.Select();
                tempSheet1.Cells.Copy(System.Reflection.Missing.Value);
                newWorkSheet1.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);

                xSheet.UsedRange.Font.Name = "楷体_GB2312";
                newWorksheet.UsedRange.Font.Name = "楷体_GB2312";
                newWorkSheet1.UsedRange.Font.Name = "楷体_GB2312";
                //记录的为短路电流格式
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 3]).MergeCells = true;
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Size = 20;
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Name = "黑体";
                xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xSheet.get_Range(xSheet.Cells[6, 1], xSheet.Cells[6, 3]).Interior.ColorIndex = 45;
                xSheet.get_Range(xSheet.Cells[7, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 1]).Interior.ColorIndex = 6;
                xSheet.get_Range(xSheet.Cells[4, 3], xSheet.Cells[xSheet.UsedRange.Rows.Count, 13]).NumberFormat = "0.0000_ ";
                //母线电压显示方式
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 14]).MergeCells = true;
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Size = 20;
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Name = "黑体";
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                if (OutType == 0)
                {
                    for (int m = 0; m < muxiannum - 1; m++)
                    {
                        newWorksheet.get_Range(newWorksheet.Cells[m * (muxiannum + 2) + 8, 1], newWorksheet.Cells[m * (muxiannum + 2) + 8, 14]).Interior.ColorIndex = 45;
                        newWorksheet.get_Range(newWorksheet.Cells[m * (muxiannum + 2) + 9, 1], newWorksheet.Cells[m * (muxiannum + 2) + 8 + muxiannum - 1, 1]).Interior.ColorIndex = 6;
                        newWorksheet.get_Range(newWorksheet.Cells[m * (muxiannum + 2) + 9, 3], newWorksheet.Cells[m * (muxiannum + 2) + 8 + muxiannum - 1, 13]).NumberFormat = "0.0000_ ";
                    }

                }

                //线路三序电流的显示方式
                newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 15]).MergeCells = true;
                newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 1]).Font.Size = 20;
                newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 1]).Font.Name = "黑体";
                newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                if (OutType == 0)
                {
                    for (int m = 0; m < muxiannum - 1; m++)
                    {
                        newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum + 2) + 8, 1], newWorkSheet1.Cells[m * (linenum + 2) + 8, 15]).Interior.ColorIndex = 45;
                        newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum + 2) + 9, 1], newWorkSheet1.Cells[m * (linenum + 2) + 8 + linenum - 1, 1]).Interior.ColorIndex = 6;
                        newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum + 2) + 9, 2], newWorkSheet1.Cells[m * (linenum + 2) + 8 + linenum - 1, 2]).Interior.ColorIndex = 6;
                        newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum + 2) + 9, 3], newWorkSheet1.Cells[m * (linenum + 2) + 8 + linenum - 1, 3]).Interior.ColorIndex = 6;
                        newWorkSheet1.get_Range(newWorkSheet1.Cells[m * (linenum + 2) + 9, 4], newWorkSheet1.Cells[m * (linenum + 2) + 8 + linenum - 1, 14]).NumberFormat = "0.0000_ ";
                    }
                }


                xSheet.Rows.AutoFit();
                xSheet.Columns.AutoFit();
                newWorksheet.Rows.AutoFit();
                newWorksheet.Columns.AutoFit();
                newWorkSheet1.Rows.AutoFit();
                newWorkSheet1.Columns.AutoFit();
                newWorksheet.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "全网短路计算结果.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);
                System.Windows.Forms.Clipboard.Clear();
                result1.Workbooks.Close();
                result1.Quit();
                result2.Workbooks.Close();
                result2.Quit();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("数据存在问题请输入完全后再操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        public void AllshortcheckYL(string projectSUID, string projectid, double ratecaplity, int caozuoi)  //根据操作的次序依次显示
        {

            try
            {
                if (!CheckDL(projectSUID, projectid, ratecaplity))
                {
                    return;
                }
                Dictionary<int, double> nodeshorti = new Dictionary<int, double>();      //记录母线有没有进行过短路 
                KeyValuePair<int, double> maxshorti = new KeyValuePair<int, double>(); //取出短路的最大短路电流
                string con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='01'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
                PSPDEV pspDev = new PSPDEV();
                IList list1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);

                PSPDEV psp = new PSPDEV();
                shortbuscir shortCutCal = new shortbuscir(Compuflag);
                for (int i = 0; i < list1.Count; i++)
                {
                    pspDev = list1[i] as PSPDEV;
                    bool flag = false;
                    string dlr = null;
                    con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='05'AND PSPDEV.KSwitchStatus = '0'AND (PSPDEV.IName='" + pspDev.Name + "'OR PSPDEV.JName='" + pspDev.Name + "')order by PSPDEV.number";

                    IList list2 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);

                    for (int j = 0; j < list2.Count; j++)
                    {
                        psp = list2[j] as PSPDEV;
                        con = " WHERE Name='" + psp.ISwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                        IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                        con = " WHERE Name='" + psp.JSwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                        IList listjswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                        PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                        PSPDEV pspjswitch = (PSPDEV)listjswitch[0];

                        if (pspDev.Number == psp.FirstNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                        {

                            flag = true;
                            dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "0 " + " " + "0 ";

                        }
                        if (pspDev.Number == psp.LastNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                        {
                            flag = true;
                            dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "1 " + " " + "0 ";
                        }
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\fault.txt"))
                        {
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\fault.txt");
                        }
                        if (flag)
                        {
                            FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\fault.txt"), FileMode.OpenOrCreate);
                            StreamWriter str11 = new StreamWriter(VK);
                            str11.Write(dlr);
                            str11.Close();
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                            {
                                File.Delete(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt");
                            }
                            //shortcir shortCutCal = new shortcir();
                            shortCutCal.Show_shortcir(0, 0, 1);
                            //bool matrixflag=true;                //用来判断是否导纳矩阵的逆矩阵是否存在逆矩阵
                            string matrixstr = null;
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Zmatrixcheck.txt"))
                            {
                                matrixstr = "正序导纳矩阵";
                                // matrixflag = false;
                            }

                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Fmatrixcheck.txt"))
                            {
                                // matrixflag = false;
                                matrixstr += "负序导纳矩阵";
                            }

                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Lmatrixcheck.txt"))
                            {
                                //matrixflag = false;
                                matrixstr += "零序导纳矩阵";
                            }
                            if (matrixstr != null)
                            {
                                MessageBox.Show(matrixstr + "不存在逆矩阵，请调整参数后再进行计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                            {
                            }
                            else
                            {
                                return;
                            }

                            FileStream shorcuit = new FileStream(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt", FileMode.Open);
                            StreamReader readLineGU = new StreamReader(shorcuit, System.Text.Encoding.Default);
                            string strLineGU;
                            string[] arrayGU;
                            char[] charSplitGU = new char[] { ' ' };

                            while ((strLineGU = readLineGU.ReadLine()) != null)
                            {

                                while ((strLineGU = readLineGU.ReadLine()) != null)
                                {
                                    arrayGU = strLineGU.Split(charSplitGU);
                                    string[] shorti = new string[4];
                                    shorti.Initialize();
                                    int m = 0;
                                    foreach (string str in arrayGU)
                                    {

                                        if (str != "")
                                        {

                                            shorti[m++] = str.ToString();

                                        }
                                    }

                                    nodeshorti[pspDev.Number] = Convert.ToDouble(shorti[3]) * 100 / (Math.Sqrt(3) * pspDev.ReferenceVolt);
                                }
                            }
                            readLineGU.Close();
                            break;                 //跳出本循环 进行母线的另外一个母线短路
                        }
                        if (!flag)
                            continue;
                        //写入错误中
                    }
                    //如果在一般线路中没有则在两绕组中进行
                    if (!flag)
                    {
                        con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='02'AND PSPDEV.KSwitchStatus = '0'AND(PSPDEV.IName='" + pspDev.Name + "'OR PSPDEV.JName='" + pspDev.Name + "') order by PSPDEV.number";
                        IList list3 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                        for (int j = 0; j < list3.Count; j++)
                        {
                            dlr = null;
                            psp = list3[j] as PSPDEV;
                            //PSPDEV devFirst = new PSPDEV();

                            //con = " WHERE Name='" + psp.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            //devFirst = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                            //PSPDEV devLast = new PSPDEV();


                            //con = " WHERE Name='" + psp.JName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            //devLast = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + psp.ISwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + psp.JSwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listjswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                            PSPDEV pspjswitch = (PSPDEV)listjswitch[0];
                            if (pspDev.Number == psp.FirstNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                            {

                                flag = true;
                                dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "0" + " " + "0";

                            }
                            if (pspDev.Number == psp.LastNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                            {
                                flag = true;
                                dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "1" + " " + "0";
                            }
                            if (flag)
                            {
                                FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\fault.txt"), FileMode.OpenOrCreate);
                                StreamWriter str11 = new StreamWriter(VK);
                                str11.Write(dlr);
                                str11.Close();
                                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                                {
                                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt");
                                }
                                // shortcir shortCutCal = new shortcir();
                                shortCutCal.Show_shortcir(0, 0, 1);
                                //bool matrixflag=true;                //用来判断是否导纳矩阵的逆矩阵是否存在逆矩阵
                                string matrixstr = null;
                                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Zmatrixcheck.txt"))
                                {
                                    matrixstr = "正序导纳矩阵";
                                    // matrixflag = false;
                                }

                                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Fmatrixcheck.txt"))
                                {
                                    // matrixflag = false;
                                    matrixstr += "负序导纳矩阵";
                                }

                                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Lmatrixcheck.txt"))
                                {
                                    //matrixflag = false;
                                    matrixstr += "零序导纳矩阵";
                                }
                                if (matrixstr != null)
                                {
                                    MessageBox.Show(matrixstr + "不存在逆矩阵，请调整参数后再进行计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                                {
                                }
                                else
                                {
                                    return;
                                }

                                FileStream shorcuit = new FileStream(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt", FileMode.Open);
                                StreamReader readLineGU = new StreamReader(shorcuit, System.Text.Encoding.Default);
                                string strLineGU;
                                string[] arrayGU;
                                char[] charSplitGU = new char[] { ' ' };

                                while ((strLineGU = readLineGU.ReadLine()) != null)
                                {

                                    while ((strLineGU = readLineGU.ReadLine()) != null)
                                    {
                                        arrayGU = strLineGU.Split(charSplitGU);
                                        string[] shorti = new string[4];
                                        shorti.Initialize();
                                        int m = 0;
                                        foreach (string str in arrayGU)
                                        {

                                            if (str != "")
                                            {

                                                shorti[m++] = str.ToString();

                                            }
                                        }

                                        nodeshorti[pspDev.Number] = Convert.ToDouble(shorti[3]) * 100 / (Math.Sqrt(3) * pspDev.ReferenceVolt);
                                    }
                                }
                                readLineGU.Close();
                                break;                 //跳出本循环 进行母线的另外一个母线短路
                            }
                            if (!flag)
                                continue;
                            //写入错误中
                        }
                    }
                    if (!flag)
                    {
                        con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='03'AND PSPDEV.KSwitchStatus = '0' AND(PSPDEV.IName='" + pspDev.Name + "'OR PSPDEV.JName='" + pspDev.Name + "'OR PSPDEV.KName='" + pspDev.Name + "') order by PSPDEV.number";
                        IList list4 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                        for (int j = 0; j < list4.Count; j++)
                        {
                            dlr = null;
                            psp = list4[j] as PSPDEV;
                            //PSPDEV devINode = new PSPDEV();

                            //con = " WHERE Name='" + psp.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            //devINode = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                            //PSPDEV devJNode = new PSPDEV();

                            //con = " WHERE Name='" + psp.JName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            //devJNode = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                            //PSPDEV devKNode = new PSPDEV();

                            //con = " WHERE Name='" + psp.KName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            //devKNode = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);

                            con = " WHERE Name='" + psp.ISwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listiswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + psp.JSwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listjswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            con = " WHERE Name='" + psp.HuganLine1 + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                            IList listkswitch = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                            PSPDEV pspjswitch = (PSPDEV)listjswitch[0];
                            PSPDEV pspkswitch = (PSPDEV)listkswitch[0];
                            if (pspDev.Number == psp.FirstNode & pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0" && pspkswitch.KSwitchStatus == "0")
                            {

                                flag = true;
                                dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "0" + " " + "0";

                            }
                            if (pspDev.Number == psp.LastNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0" && pspkswitch.KSwitchStatus == "0")
                            {
                                flag = true;
                                dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "1" + " " + "0";
                            }
                            if (pspDev.Number == psp.Flag && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0" && pspkswitch.KSwitchStatus == "0")
                            {
                                flag = true;
                                dlr = "0" + " " + psp.FirstNode + " " + psp.Flag + " " + psp.Number + " " + "1" + " " + "0";
                            }


                            if (flag)
                            {
                                FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\fault.txt"), FileMode.OpenOrCreate);
                                StreamWriter str11 = new StreamWriter(VK);
                                str11.Write(dlr);
                                str11.Close();
                                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                                {
                                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt");
                                }
                                //shortcir shortCutCal = new shortcir();
                                shortCutCal.Show_shortcir(0, 0, 1);
                                //bool matrixflag=true;                //用来判断是否导纳矩阵的逆矩阵是否存在逆矩阵
                                string matrixstr = null;
                                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Zmatrixcheck.txt"))
                                {
                                    matrixstr = "正序导纳矩阵";
                                    // matrixflag = false;
                                }

                                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Fmatrixcheck.txt"))
                                {
                                    // matrixflag = false;
                                    matrixstr += "负序导纳矩阵";
                                }

                                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Lmatrixcheck.txt"))
                                {
                                    //matrixflag = false;
                                    matrixstr += "零序导纳矩阵";
                                }
                                if (matrixstr != null)
                                {
                                    MessageBox.Show(matrixstr + "不存在逆矩阵，请调整参数后再进行计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                                {
                                }
                                else
                                {
                                    return;
                                }

                                FileStream shorcuit = new FileStream(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt", FileMode.Open);
                                StreamReader readLineGU = new StreamReader(shorcuit, System.Text.Encoding.Default);
                                string strLineGU;
                                string[] arrayGU;
                                char[] charSplitGU = new char[] { ' ' };

                                while ((strLineGU = readLineGU.ReadLine()) != null)
                                {

                                    while ((strLineGU = readLineGU.ReadLine()) != null)
                                    {
                                        arrayGU = strLineGU.Split(charSplitGU);
                                        string[] shorti = new string[4];
                                        shorti.Initialize();
                                        int m = 0;
                                        foreach (string str in arrayGU)
                                        {

                                            if (str != "")
                                            {

                                                shorti[m++] = str.ToString();

                                            }
                                        }

                                        nodeshorti[pspDev.Number] = Convert.ToDouble(shorti[3]) * 100 / (Math.Sqrt(3) * pspDev.ReferenceVolt);
                                    }
                                }
                                readLineGU.Close();
                                break;                 //跳出本循环 进行母线的另外一个母线短路
                            }
                            if (!flag)
                                continue;
                            //写入错误中
                        }
                    }
                }
                //找出短路电流最大的值
                //maxshorti.Key = 1;
                //maxshorti.Value = nodeshorti[1];
                foreach (KeyValuePair<int, double> keyvalue in nodeshorti)
                {
                    if (keyvalue.Value > maxshorti.Value)
                    {
                        maxshorti = keyvalue;
                    }
                }

                //首先取出断路器 判断它的母线在不在 如果不在就将其删除 然后与额定电压进行比较 
                con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='06'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
                IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);

                for (int i = 0; i < list.Count; i++)
                {
                    bool flag = false;
                    pspDev = list[i] as PSPDEV;
                    for (int j = 0; j < list1.Count; j++)
                    {
                        psp = list1[j] as PSPDEV;
                        if (pspDev.IName == psp.Name)
                            flag = true;

                    }
                    if (!flag)
                    {
                        UCDeviceBase.DataService.Delete<PSPDEV>(pspDev);
                    }
                }
                con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='06'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
                list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                for (int i = 0; i < list.Count; i++)
                {
                    pspDev = list[i] as PSPDEV;
                    pspDev.HuganLine3 = "";
                    pspDev.KName = "";
                    if (pspDev.KSwitchStatus == "0")
                    {
                        pspDev.OutP = maxshorti.Value;
                        if (maxshorti.Value > pspDev.HuganTQ1)
                        {
                            pspDev.HuganLine3 = "不合格";
                        }
                        else
                        {
                            pspDev.HuganLine3 = "合格";
                        }
                        pspDev.HuganLine4 = "";
                        if (pspDev.HuganLine3 == "合格")
                        {
                            pspDev.KName = "合格";
                        }
                        else
                            pspDev.KName = "不合格";
                    }

                    UCDeviceBase.DataService.Update<PSPDEV>(pspDev);
                }
                switch (caozuoi)
                {
                    case 1:           //全部短路检验
                        {
                            pspDev.SvgUID = projectSUID;
                            pspDev.Type = "06";
                            DlqiCheckform dlqicheckform = new DlqiCheckform(pspDev);
                            dlqicheckform.getusercltr.gridView.GroupPanelText = "断路器开断能力评估初步结果表";
                            dlqicheckform.ShowDialog();
                            break;
                        }
                    case 2:             //最大短路检验        
                        {
                            con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='06'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
                            list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            for (int i = 0; i < list.Count; i++)
                            {
                                pspDev = list[i] as PSPDEV;
                                if (pspDev.KSwitchStatus == "0")
                                {
                                    con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='01'AND PSPDEV.KSwitchStatus = '0'AND PSPDEV.Name='" + pspDev.IName + "'";

                                    IList list4 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                                    psp = list4[0] as PSPDEV;
                                    try
                                    {
                                        pspDev.OutQ = nodeshorti[psp.Number];
                                        if (pspDev.HuganLine3 == "不合格")
                                        {
                                            if (pspDev.OutQ <= pspDev.HuganTQ1)
                                            {
                                                pspDev.HuganLine3 = "合格";
                                            }
                                        }
                                        pspDev.HuganLine4 = "";

                                        if (pspDev.HuganLine3 == "合格")
                                        {
                                            pspDev.KName = "合格";
                                        }
                                        else
                                            pspDev.KName = "不合格";
                                        UCDeviceBase.DataService.Update<PSPDEV>(pspDev);
                                    }
                                    catch (System.Exception ex)
                                    {
                                        MessageBox.Show("短路数据不完整");
                                    }
                                }


                            }
                            pspDev.SvgUID = projectSUID;
                            pspDev.Type = "06";
                            DlqiCheckform dlqicheckform = new DlqiCheckform(pspDev);
                            dlqicheckform.getusercltr.gridView.GroupPanelText = "最大短路校核结果表";
                            dlqicheckform.ShowDialog();
                            break;
                        }
                    case 3:          //断路器直流检验
                        {
                            con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='06'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
                            list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            for (int i = 0; i < list.Count; i++)
                            {
                                pspDev = list[i] as PSPDEV;
                                if (pspDev.KSwitchStatus == "0")
                                {
                                    double tx = 0.0;
                                    if (pspDev.HuganLine2 == "自脱扣断路器")
                                    {
                                        tx = 0.0;
                                    }
                                    else if (pspDev.HuganLine2 == "辅助动力脱扣的断路器")
                                    {
                                        tx = 10;
                                    }
                                    pspDev.HuganTQ4 = (pspDev.OutP / pspDev.HuganTQ1) * Math.Exp((-pspDev.HuganTQ2 - tx) / 45) * 100;
                                    pspDev.HuganTQ5 = (pspDev.OutQ / pspDev.HuganTQ1) * Math.Exp((-pspDev.HuganTQ2 - tx) / 45) * 100;
                                    if (pspDev.HuganTQ3 >= pspDev.HuganTQ4)
                                    {
                                        pspDev.HuganLine4 = "合格";
                                    }
                                    if (pspDev.HuganTQ3 >= pspDev.HuganTQ5)
                                    {
                                        pspDev.HuganLine4 = "合格";
                                    }
                                    else if (pspDev.HuganTQ3 < pspDev.HuganTQ5)
                                    {
                                        pspDev.HuganLine4 = "不合格";
                                    }
                                    if (pspDev.HuganLine3 == "合格" && pspDev.HuganLine4 == "合格")
                                    {
                                        pspDev.KName = "合格";
                                    }
                                    else
                                    {
                                        pspDev.KName = "不合格";
                                    }

                                    UCDeviceBase.DataService.Update<PSPDEV>(pspDev);
                                }

                            }
                            pspDev.SvgUID = projectSUID;
                            pspDev.Type = "06";
                            DlqiCheckform dlqicheckform = new DlqiCheckform(pspDev);
                            dlqicheckform.getusercltr.gridView.GroupPanelText = "断路器开端能力最终评估表";
                            dlqicheckform.ShowDialog();
                            break;
                        }

                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("短路计算存在问题请检查短路计算是否能进行！");
            }

        }
        public void Allshortcheck(string projectSUID, string projectid, double ratecaplity, int caozuoi)  //根据操作的次序依次显示
        {

            try
            {
                string con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='01'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
                PSPDEV pspDev = new PSPDEV();
                IList list1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                PSPDEV psp = new PSPDEV();
                Dictionary<string, double> nodeshorti = new Dictionary<string, double>();      //记录母线有没有进行过短路 
                KeyValuePair<string, double> maxshorti = new KeyValuePair<string, double>();
                con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='06'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
                IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                if (caozuoi != 3)
                {
                    AllShorti(projectSUID, projectid, 0, ratecaplity, nodeshorti);
                    //找出短路电流最大的值
                    //maxshorti.Key = 1;
                    //maxshorti.Value = nodeshorti[1];

                    foreach (KeyValuePair<string, double> keyvalue in nodeshorti)
                    {
                        if (keyvalue.Value > maxshorti.Value)
                        {
                            maxshorti = keyvalue;
                        }
                    }

                    //首先取出断路器 判断它的母线在不在 如果不在就将其删除 然后与额定电压进行比较 


                    for (int i = 0; i < list.Count; i++)
                    {
                        bool flag = false;
                        pspDev = list[i] as PSPDEV;
                        for (int j = 0; j < list1.Count; j++)
                        {
                            psp = list1[j] as PSPDEV;
                            if (pspDev.IName == psp.Name)
                                flag = true;

                        }
                        if (!flag)
                        {
                            UCDeviceBase.DataService.Delete<PSPDEV>(pspDev);
                        }
                    }
                    con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='06'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
                    list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    for (int i = 0; i < list.Count; i++)
                    {
                        pspDev = list[i] as PSPDEV;
                        pspDev.HuganLine3 = "";
                        pspDev.KName = "";
                        if (pspDev.KSwitchStatus == "0")
                        {
                            pspDev.OutP = maxshorti.Value;
                            if (maxshorti.Value > pspDev.HuganTQ1)
                            {
                                pspDev.HuganLine3 = "不合格";
                            }
                            else
                            {
                                pspDev.HuganLine3 = "合格";
                            }
                            pspDev.HuganLine4 = "";
                            if (pspDev.HuganLine3 == "合格")
                            {
                                pspDev.KName = "合格";
                            }
                            else
                                pspDev.KName = "不合格";
                        }

                        UCDeviceBase.DataService.Update<PSPDEV>(pspDev);
                    }
                }

                switch (caozuoi)
                {
                    case 1:           //全部短路检验
                        {
                            pspDev.SvgUID = projectSUID;
                            pspDev.Type = "06";
                            DlqiCheckform dlqicheckform = new DlqiCheckform(pspDev);
                            dlqicheckform.getusercltr.gridView.GroupPanelText = "断路器开断能力评估初步结果表";
                            dlqicheckform.ShowDialog();
                            break;
                        }
                    case 2:             //最大短路检验        
                        {
                            con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='06'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
                            list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            for (int i = 0; i < list.Count; i++)
                            {
                                pspDev = list[i] as PSPDEV;
                                if (pspDev.KSwitchStatus == "0")
                                {
                                    con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='01'AND PSPDEV.KSwitchStatus = '0'AND PSPDEV.Name='" + pspDev.IName + "'";

                                    IList list4 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                                    psp = list4[0] as PSPDEV;
                                    try
                                    {
                                        pspDev.OutQ = nodeshorti[psp.Name];
                                        if (pspDev.HuganLine3 == "不合格")
                                        {
                                            if (pspDev.OutQ <= pspDev.HuganTQ1)
                                            {
                                                pspDev.HuganLine3 = "合格";
                                            }
                                        }
                                        pspDev.HuganLine4 = "";

                                        if (pspDev.HuganLine3 == "合格")
                                        {
                                            pspDev.KName = "合格";
                                        }
                                        else
                                            pspDev.KName = "不合格";
                                        UCDeviceBase.DataService.Update<PSPDEV>(pspDev);
                                    }
                                    catch (System.Exception ex)
                                    {
                                        MessageBox.Show("短路数据不完整");
                                    }
                                }


                            }
                            pspDev.SvgUID = projectSUID;
                            pspDev.Type = "06";
                            DlqiCheckform dlqicheckform = new DlqiCheckform(pspDev);
                            dlqicheckform.getusercltr.gridView.GroupPanelText = "最大短路校核结果表";
                            dlqicheckform.ShowDialog();
                            break;
                        }
                    case 3:          //断路器直流检验
                        {
                            con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='06'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
                            list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            for (int i = 0; i < list.Count; i++)
                            {
                                pspDev = list[i] as PSPDEV;
                                if (pspDev.KSwitchStatus == "0")
                                {
                                    double tx = 0.0;
                                    if (pspDev.HuganLine2 == "自脱扣断路器")
                                    {
                                        tx = 0.0;
                                    }
                                    else if (pspDev.HuganLine2 == "辅助动力脱扣的断路器")
                                    {
                                        tx = 10;
                                    }
                                    pspDev.HuganTQ4 = (pspDev.OutP / pspDev.HuganTQ1) * Math.Exp((-pspDev.HuganTQ2 - tx) / 45) * 100;
                                    pspDev.HuganTQ5 = (pspDev.OutQ / pspDev.HuganTQ1) * Math.Exp((-pspDev.HuganTQ2 - tx) / 45) * 100;
                                    if (pspDev.HuganTQ3 >= pspDev.HuganTQ4)
                                    {
                                        pspDev.HuganLine4 = "合格";
                                    }
                                    if (pspDev.HuganTQ3 >= pspDev.HuganTQ5)
                                    {
                                        pspDev.HuganLine4 = "合格";
                                    }
                                    else if (pspDev.HuganTQ3 < pspDev.HuganTQ5)
                                    {
                                        pspDev.HuganLine4 = "不合格";
                                    }
                                    if (pspDev.HuganLine3 == "合格" && pspDev.HuganLine4 == "合格")
                                    {
                                        pspDev.KName = "合格";
                                    }
                                    else
                                    {
                                        pspDev.KName = "不合格";
                                    }

                                    UCDeviceBase.DataService.Update<PSPDEV>(pspDev);
                                }

                            }
                            pspDev.SvgUID = projectSUID;
                            pspDev.Type = "06";
                            DlqiCheckform dlqicheckform = new DlqiCheckform(pspDev);
                            dlqicheckform.getusercltr.gridView.GroupPanelText = "断路器开断能力最终评估表";
                            dlqicheckform.ShowDialog();
                            break;
                        }

                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("短路计算存在问题请检查短路计算是否能进行！");
            }

        }
    }
}
