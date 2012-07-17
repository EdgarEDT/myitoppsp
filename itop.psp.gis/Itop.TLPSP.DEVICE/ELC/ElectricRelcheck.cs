using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using System.Windows.Forms;
using n1NL_DLL;
using DevExpress.Utils;
namespace Itop.TLPSP.DEVICE
{
    public class ElectricRelcheck
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
        public int brchcount, buscount, transcount,outbrchcount,outbuscount;        //记录全网参与潮流计算的支路数和母线数目 其中三绕组变压器有三条支路
        public bool CheckN(string projectSUID,string projectid,double ratedCapacity)
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
                            if (dev.FirstNode < 0 || dev.LastNode < 0 )
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
                            if (outbrchcount<125)
                            {
                                outbrchcount++;
                            }
                        }
                    }
                    foreach (PSPDEV dev in listBYQ2)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            if (dev.FirstNode < 0 || dev.LastNode < 0 )
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
                            if (dev.FirstNode < 0 || dev.LastNode < 0||dev.Flag<0)
                            {
                                string temp = "拓朴分析失败,";
                                temp += dev.Name;
                                temp += "没有正确连接,请进行处理！。";
                               System.Windows.Forms. MessageBox.Show(temp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            if (dev.Number == devtrans.LastNode || dev.Number == devtrans.FirstNode||dev.Number==devtrans.Flag)
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
                            if (outbuscount<125)
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
                if ((strBus.Contains("非数字") || strBus.Contains("正无穷大"))||( strBranch.Contains("非数字") || strBranch.Contains("正无穷大")))
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
        public double TLPSPVmin = 0.90, TLPSPVmax = 1.1;
        public void WebCalAndPrint(string projectSUID, string projectid, double ratedCapacity)            //网络N-1计算和输出
        {
            FileStream dh;
            StreamReader readLine;
            // StreamReader readLine;
            ArrayList list = new ArrayList();   //用来记录线路不能解裂的位数
            char[] charSplit;
            string strLine;
            string[] array1;
            StringBuilder outputZL = new StringBuilder(); 
            StringBuilder outputv=new StringBuilder();//记录直流计算结果 线路功率和节点电压
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
            WebN1 webn1 = new WebN1();
            webn1.ShowDialog();
            if (webn1.DialogResult == DialogResult.OK)    //进行全网计算
            {
                OuptResultForm outtype = new OuptResultForm();
                outtype.ShowDialog();
                if (outtype.DialogResult==DialogResult.Ignore)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "可靠性计算结果.xls"))
                    {
                        System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "可靠性计算结果.xls");
                        //OpenRead(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + ".xls");
                    }
                    else
                    {
                        MessageBox.Show("程序中不存在计算的结果，请重新计算后再尝试！");
                    }
                }
                else if (outtype.DialogResult==DialogResult.OK)
                {
                    if (!CheckN(projectSUID, projectid, ratedCapacity))
                    {
                        return;
                    }
                    WaitDialogForm wf = new WaitDialogForm("", "正在处理数据, 请稍候...");
                    try
                    {
                        string datatime = System.DateTime.Now.ToString();
                        System.Windows.Forms.Clipboard.Clear(); //去掉剪切板中的数据
                        //for (int i = 1; i <= brchcount + transcount; i++)//原先的
                       // 原先程序
                        for (int i = 1; i <= brchcount; i++)
                        {
                            n1NL_DLL.ZYZ nl = new n1NL_DLL.ZYZ();
                            nl.jianyan(i);
                            GC.Collect();
                            wf.SetCaption(i.ToString());
                        }
                        //n1NL_DLL.ZYZ nl = new n1NL_DLL.ZYZ();
                        //nl.jianyan();
                        //GC.Collect();
                        wf.Close();
                        //int* busnumber;

                        //N1Test.NBcal kk = new N1Test.NBcal();
                        //busnumber = kk.Show_Reliability();
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "可靠性计算结果.xls"))
                        {
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "可靠性计算结果.xls");
                            //OpenRead(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + ".xls");
                        }

                        double yinzi = 0, capability = 0, volt = 0, current = 0, standvolt = 1, Rad_to_Deg = 57.29577951;
                        string branchname = getbranchname();
                        string busname = getbusname();
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
                        capability = ratedCapacity;

                        string con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='01'AND PSPDEV.KSwitchStatus = '0'";
                        IList cont = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                        if (buscount < cont.Count)
                        {
                            MessageBox.Show("选择的母线又存在孤立的节点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            return;

                        }
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\VandTheta.txt"))
                        {
                        }
                        else
                        {
                            MessageBox.Show("数据不收敛，请调整参数后重新计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\lineP.txt"))
                        {
                        }
                        else
                        {
                            return;
                        }
                        dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\" + "lineP.txt", FileMode.Open);
                        dh2 = new FileStream(System.Windows.Forms.Application.StartupPath + "\\" + "VandTheta.txt", FileMode.Open);
                        readLine2 = new StreamReader(dh2, Encoding.Default);
                        readLine = new StreamReader(dh, Encoding.Default);
                        charSplit = new char[] { ' ' };
                        //strLine = readLine.ReadLine();

                        outputZL = new StringBuilder();
                        //outputBC=null;                    
                        outputZL.Append("全网可靠性结果报表" + "\r\n");
                        outputZL.Append("开断支路名称" + "," + "剩余网络线路功率Pij和Pji的有名值" + ",,");
                        for (int i = 0; i < outbrchcount - 1; i++)
                        {
                            outputZL.Append(",,");
                        }
                        outputZL.Append("是否越限" + "," + "\r\n");
                        outputZL.Append(",");
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
                            outputZL.Append(branchname);
                            outputZL.Append("," + "\r\n");
                            outputZL.Append(devzl[0] + ",");
                            //将合格写入到线路的字段中新添加的
                           con=",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Name='"+devzl[0]+"'AND PSPDEV.type='05'AND PSPDEV.ProjectID='"+projectid+"'" ;
                           // con = " WHERE Name='" + devzl[0] + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
                            IList listhg = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV psphg = null;
                            if (listhg.Count != 0)
                            {
                                psphg = (PSPDEV)listhg[0];
                            }

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
                                    con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Name='" + devzl[j * 3 + 1] + "'AND PSPDEV.type='05'AND PSPDEV.ProjectID='" + projectid + "'";
                                   // con = " WHERE Name='" + devzl[j * 3 + 1] + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
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
                                    double linXij = System.Math.Sqrt(3) * voltR * Ichange;
                                    if (j < 125)
                                    {
                                        outputZL.Append("'" + youming(devzl[j * 3 + 2], capability) + "," + "'" + youming(devzl[j * 3 + 3], capability) + ",");
                                    }

                                    if (maxSij >= linXij)
                                    {
                                        lineflag = false;
                                        lineclass _line = new lineclass(n, j);
                                        Overlinp.Add(_line);
                                        // OverPhege[n] = j;
                                    }

                                }
                                if (!lineflag)
                                {
                                    outputZL.Append("不合格");
                                    if (psphg != null)
                                    {
                                        psphg.HgFlag = "不合格";
                                    }

                                }
                                else
                                {
                                    outputZL.Append("合格");
                                    if (psphg != null)
                                    {
                                        psphg.HgFlag = "合格";
                                    }

                                }
                            }
                            else
                            {
                                outputZL.Append("该线路不可断");
                                if (psphg != null)
                                {
                                    psphg.HgFlag = "合格";
                                }

                            }
                            //写入合格
                            if (psphg != null)
                            {
                                Services.BaseService.Update<PSPDEV>(psphg);
                            }


                            //OverPhege[n] = Overlinp;
                            //Overlinp.Clear();
                            outputZL.Append("\r\n");
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
                                con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Name='" + devzl1[0] + "'AND PSPDEV.type='05'AND PSPDEV.ProjectID='" + projectid + "'";
                                //con = " WHERE Name='" + devzl1[0] + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
                                listhg = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                                if (listhg.Count != 0)
                                {
                                    psphg = (PSPDEV)listhg[0];
                                }

                                if (devzl1[1] != "-1")
                                {
                                    outputZL.Append(devzl1[0] + ",");
                                    for (int j = 0; j < brchcount; j++)
                                    {
                                        double pij = Convert.ToDouble(devzl1[j * 3 + 2].Substring(0, devzl1[j * 3 + 2].IndexOf('j') - 1)) * capability;
                                        double qij = Convert.ToDouble(devzl1[j * 3 + 2].Substring(devzl1[j * 3 + 2].IndexOf('j') + 1)) * capability;
                                        double pji = Convert.ToDouble(devzl1[j * 3 + 3].Substring(0, devzl1[j * 3 + 3].IndexOf('j') - 1)) * capability;
                                        double qji = Convert.ToDouble(devzl1[j * 3 + 3].Substring(devzl1[j * 3 + 3].IndexOf('j') + 1)) * capability;
                                        double Sij = System.Math.Sqrt(pij * pij + qij * qij);
                                        double Sji = System.Math.Sqrt(pji * pji + qji * qji);
                                        double maxSij = (Sij > Sji) ? Sij : Sji;
                                        con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Name='" + devzl1[j * 3 + 1] + "'AND PSPDEV.type='05'AND PSPDEV.ProjectID='" + projectid + "'";
                                        //con = " WHERE Name='" + devzl1[j * 3 + 1] + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
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
                                        double linXij = System.Math.Sqrt(3) * voltR * Ichange;
                                        // outputZL += "'" + devzl[j * 3 + 2] + "," + "'" + devzl[j * 3 + 3] + ",";
                                        if (maxSij >= linXij)
                                        {
                                            lineflag = false;
                                            lineclass subline = new lineclass(n, j);
                                            Overlinp.Add(subline);
                                            //OverPhege[n] = j;
                                        }
                                        if (j < 125)
                                            outputZL.Append("'" + youming(devzl1[j * 3 + 2], capability) + "," + "'" + youming(devzl1[j * 3 + 3], capability) + ",");    //在此还可以判断线路是否超载

                                    }
                                    if (!lineflag)
                                    {
                                        outputZL.Append("不合格");
                                        if (psphg != null)
                                        {
                                            psphg.HgFlag = "不合格";
                                        }

                                    }
                                    else
                                    {
                                        outputZL.Append("合格");
                                        if (psphg != null)
                                        {
                                            psphg.HgFlag = "合格";
                                        }

                                    }
                                    outputZL.Append("\r\n");
                                    //OverPhege[n] = Overlinp;
                                    //Overlinp.Clear();

                                }

                                else
                                {
                                    list.Add(n);
                                    outputZL.Append(devzl1[0] + "," + "为不可断裂的线路");
                                    if (psphg != null)
                                    {
                                        psphg.HgFlag = "合格";
                                    }

                                    outputZL.Append("\r\n");
                                }
                                if (psphg != null)
                                {
                                    Services.BaseService.Update<PSPDEV>(psphg);
                                }

                            }
                        }
                        outputZL.Append("注释：红色为线路超载" + "\r\n");
                        outputZL.Append("操作时间为：" + datatime);
                        outputZL.Append("\r\n");
                        outputZL.Append("单位：kA\\kV\\MW\\Mvar" + "\r\n");
                        readLine.Close();
                        if (File.Exists("result1.csv"))
                        {
                            File.Delete("result1.csv");
                        }

                        op = new FileStream("result1.csv", FileMode.OpenOrCreate);
                        str1 = new StreamWriter(op, Encoding.Default);
                        str1.Write(outputZL);
                        str1.Close();

                        outputv = new StringBuilder();
                        //将各个节点的电压写入其中
                        // strLine2 = readLine2.ReadLine();
                        n = 0;
                        bool busvflag1 = true;
                        outputv.Append("网络节点电压和相角" + "\r\n");
                        outputv.Append("开断支路名称" + "," + "节点电压的幅值和相角的有名值");
                        for (int i = 0; i < outbuscount; i++)
                        {
                            outputv.Append(",,");
                        }
                        outputv.Append("是否越限" + "," + "\r\n");
                        outputv.Append(",");
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
                            //for (int j = 0; j < buscount; j++)
                            //{
                            //    outputZL += devzl[3 * j + 1] + "," + ",";
                            //}
                            outputv.Append(busname);
                            outputv.Append("," + "\r\n");
                            outputv.Append(devzl[0] + ",");
                            if (devzl[1] != "-1")
                            {

                                for (int j = 0; j < buscount; j++)
                                {
                                    PSPDEV pspDev = new PSPDEV();
                                    con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Name='" + devzl[j * 3 + 1] + "'AND PSPDEV.type='01'AND PSPDEV.ProjectID='" + projectid + "'";
                                   // con = " WHERE Name='" + devzl[j * 3 + 1] + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                                    pspDev = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);

                                    if (pspDev != null && pspDev.ReferenceVolt != 0)
                                    {
                                        volt = pspDev.ReferenceVolt;
                                    }
                                    else
                                        volt = standvolt;
                                    if (j < 125)
                                        outputv.Append("'" + (Convert.ToDouble(devzl[j * 3 + 2]) * volt).ToString() + "," + "'" + devzl[j * 3 + 3] + ",");
                                    if (Convert.ToDouble(devzl[j * 3 + 2]) <= TLPSPVmin || Convert.ToDouble(devzl[j * 3 + 2]) >= TLPSPVmax)
                                    {
                                        busvflag1 = false;
                                        lineclass _vtheta = new lineclass(n, j);
                                        OverVp.Add(_vtheta);
                                        //OverVhege[n] = j;
                                    }
                                }
                                if (busvflag1)
                                {
                                    outputv.Append("合格");
                                }
                                else
                                {
                                    outputv.Append("不合格");
                                }
                            }
                            else
                            {
                                outputv.Append("不可断裂");
                            }
                            //OverVhege[n] = OverVp;
                            //OverVp.Clear();
                            outputv.Append("\r\n");

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
                                    outputv.Append(devzl1[0] + ",");
                                    for (int j = 0; j < outbuscount; j++)
                                    {
                                        PSPDEV pspDev = new PSPDEV();
                                        con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Name='" + devzl1[j * 3 + 1] + "'AND PSPDEV.type='01'AND PSPDEV.ProjectID='" + projectid + "'";
                                        //con = " WHERE Name='" + devzl1[j * 3 + 1] + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                                        pspDev = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);

                                        if (pspDev != null && pspDev.ReferenceVolt != 0)
                                        {
                                            volt = pspDev.ReferenceVolt;
                                        }
                                        else
                                            volt = standvolt;
                                        if (j < 125)
                                            outputv.Append("'" + (Convert.ToDouble(devzl1[j * 3 + 2]) * volt).ToString() + "," + "'" + devzl1[j * 3 + 3] + ",");   //在此还可以判断线路是否超载,如果超载加入一个标记,在excel里使其变为红色

                                        if (Convert.ToDouble(devzl1[j * 3 + 2]) >= TLPSPVmax || Convert.ToDouble(devzl1[j * 3 + 2]) <= TLPSPVmin)
                                        {
                                            lineclass vtheta = new lineclass(n, j);
                                            busvflag1 = false;
                                            OverVp.Add(vtheta);
                                            //OverVhege[n] = j;
                                        }
                                    }
                                    if (busvflag1)
                                    {
                                        outputv.Append("合格");
                                    }
                                    else
                                        outputv.Append("不合格");
                                    outputv.Append("\r\n");
                                    //OverPhege[n] = OverVp;
                                    //OverVp.Clear();
                                }
                                else
                                {
                                    // list.Add(n);
                                    outputv.Append(devzl1[0] + "," + "为不可断裂的线路" + ",,,,,,");
                                    outputv.Append("\r\n");
                                }

                            }
                        }
                        outputv.Append("注释：红色为节点电压超载" + "\r\n");
                        outputv.Append("节点电压合格范围为电压基准值的上下限，分别为" + TLPSPVmax + "和" + TLPSPVmin + "倍" + "\r\n");
                        outputv.Append("操作时间为：" + datatime + "\r\n");
                        outputv.Append("单位：kA\\kV\\MW\\Mvar" + "\r\n");
                        readLine2.Close();


                        //op = new FileStream(System.Windows.Forms.Application.StartupPath + "\\" + "reli.txt", FileMode.OpenOrCreate);
                        //str1 = new StreamWriter(op, Encoding.Default);
                        //str1.Write(outputZL);
                        //str1.Close();


                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + "result2.csv"))
                        {
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + "result2.csv");
                        }

                        op = new FileStream(System.Windows.Forms.Application.StartupPath + "\\" + "result2.csv", FileMode.OpenOrCreate);
                        str1 = new StreamWriter(op, Encoding.Default);
                        str1.Write(outputv);
                        str1.Close();

                        result1 = new Excel.Application();
                        result1.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\" + "result1.csv");
                        newWorksheet = (Excel.Worksheet)result1.Worksheets[1];
                        result1.Worksheets.Add(System.Reflection.Missing.Value, newWorksheet, 1, System.Reflection.Missing.Value);

                        Excel.Application result2 = new Excel.Application();
                        result2.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result2.csv");
                        Excel.Worksheet tempSheet = (Excel.Worksheet)result2.Worksheets.get_Item(1);
                        Excel.Worksheet newWorksheet1 = (Excel.Worksheet)result1.Worksheets.get_Item(2);
                        newWorksheet.Name = "一般线路可靠性";
                        newWorksheet1.Name = "节点电压可靠性";
                        result1.Visible = true;


                        tempSheet.Cells.Select();
                        tempSheet.Cells.Copy(System.Reflection.Missing.Value);
                        newWorksheet1.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);

                        newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 2 * outbrchcount + 2]).MergeCells = true;
                        newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Size = 20;
                        newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Name = "黑体";
                        newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        newWorksheet.get_Range(newWorksheet.Cells[2, 1], newWorksheet.Cells[3, 1]).MergeCells = true;
                        newWorksheet.get_Range(newWorksheet.Cells[2, 2], newWorksheet.Cells[2, 2 * outbrchcount + 1]).MergeCells = true;
                        newWorksheet.get_Range(newWorksheet.Cells[2, 2], newWorksheet.Cells[2, 2]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        newWorksheet.get_Range(newWorksheet.Cells[2, 2 * outbrchcount + 2], newWorksheet.Cells[3, 2 * outbrchcount + 2]).MergeCells = true;
                        for (int i = 0; i < outbrchcount; i++)
                        {
                            newWorksheet.get_Range(newWorksheet.Cells[3, 2 * i + 2], newWorksheet.Cells[3, 2 * i + 3]).MergeCells = true;
                            newWorksheet.get_Range(newWorksheet.Cells[3, 2 * i + 2], newWorksheet.Cells[3, 2 * i + 3]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        }
                        //// newWorksheet.get_Range(newWorksheet.Cells[4, 2], newWorksheet.Cells[4, brchcount + 1]).Interior.ColorIndex = 45;
                        // newWorksheet.get_Range(newWorksheet.Cells[4, 1], newWorksheet.Cells[3 + outbrchcount + transcount, 1]).Interior.ColorIndex = 6;
                        newWorksheet.get_Range(newWorksheet.Cells[4, 1], newWorksheet.Cells[3 + brchcount, 1]).Interior.ColorIndex = 6;
                        ////newWorksheet.get_Range(newWorksheet.Cells[5, 2], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, brchcount + 1]).NumberFormat = "0.0000_ ";
                        // newWorksheet.get_Range(newWorksheet.Cells[4, 2], newWorksheet.Cells[3 + outbrchcount + transcount, 2 * outbrchcount + 1]).NumberFormat = "@";
                        // newWorksheet.get_Range(newWorksheet.Cells[2, 1], newWorksheet.Cells[6 + outbrchcount + transcount, 2 * outbrchcount + 2]).Font.Name = "楷体_GB2312";
                        newWorksheet.get_Range(newWorksheet.Cells[4, 2], newWorksheet.Cells[3 + brchcount, 2 * outbrchcount + 1]).NumberFormat = "@";
                        newWorksheet.get_Range(newWorksheet.Cells[2, 1], newWorksheet.Cells[6 + brchcount , 2 * outbrchcount + 2]).Font.Name = "楷体_GB2312";
                        //在此处将其不合格的数据显示出来
                        //foreach (KeyValuePair<int, List<lineclass>> kvp in OverPhege)
                        //{
                        for (int i = 0; i < Overlinp.Count; i++)
                        {
                            if (Overlinp[i].linenum < 125)
                                newWorksheet.get_Range(newWorksheet.Cells[3 + Overlinp[i].row, 2 * Overlinp[i].linenum + 2], newWorksheet.Cells[3 + Overlinp[i].row, 2 * Overlinp[i].linenum + 3]).Interior.ColorIndex = 3;
                        }

                        //}
                        //补偿法中的数据处理过程

                        newWorksheet1.get_Range(newWorksheet1.Cells[1, 1], newWorksheet1.Cells[1, 2 * outbuscount + 2]).MergeCells = true;
                        newWorksheet1.get_Range(newWorksheet1.Cells[1, 1], newWorksheet1.Cells[1, 1]).Font.Size = 20;
                        newWorksheet1.get_Range(newWorksheet1.Cells[1, 1], newWorksheet1.Cells[1, 1]).Font.Name = "黑体";
                        newWorksheet1.get_Range(newWorksheet1.Cells[1, 1], newWorksheet1.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        newWorksheet1.get_Range(newWorksheet1.Cells[2, 2], newWorksheet1.Cells[2, 2]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        newWorksheet1.get_Range(newWorksheet1.Cells[2, 1], newWorksheet1.Cells[3, 1]).MergeCells = true;  //补偿法中前面开断合并
                        newWorksheet1.get_Range(newWorksheet1.Cells[2, 2], newWorksheet1.Cells[2, 2 * outbuscount + 1]).MergeCells = true;
                        newWorksheet1.get_Range(newWorksheet1.Cells[2, 2 * outbuscount + 2], newWorksheet1.Cells[3, 2 * outbuscount + 2]).MergeCells = true;   //合格合并
                        //newWorksheet1.get_Range(newWorksheet1.Cells[4, 1], newWorksheet1.Cells[4 + brchcount + transcount - 1, 1]).Interior.ColorIndex = 6;
                        newWorksheet1.get_Range(newWorksheet1.Cells[4, 1], newWorksheet1.Cells[4 + brchcount - 1, 1]).Interior.ColorIndex = 6;
                        for (int i = 0; i < outbuscount; i++)
                        {
                            newWorksheet1.get_Range(newWorksheet1.Cells[3, 2 * i + 2], newWorksheet1.Cells[3, 2 * i + 3]).MergeCells = true;
                            newWorksheet1.get_Range(newWorksheet1.Cells[3, 2 * i + 2], newWorksheet1.Cells[3, 2 * i + 3]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        }
                        //newWorksheet1.get_Range(newWorksheet1.Cells[7 + brchcount, 2], newWorksheet1.Cells[7 + brchcount, brchcount + 1]).Interior.ColorIndex = 45;
                        //newWorksheet1.get_Range(newWorksheet1.Cells[8 + brchcount, 1], newWorksheet1.Cells[8 + 2 * brchcount, 1]).Interior.ColorIndex = 6;
                        //newWorksheet1.get_Range(newWorksheet1.Cells[5, 2], newWorksheet1.Cells[newWorksheet1.UsedRange.Rows.Count, brchcount + 1]).NumberFormat = "0.0000_ ";
                        //newWorksheet1.get_Range(newWorksheet1.Cells[4, 2], newWorksheet1.Cells[4 +outbrchcount + transcount, 2 * outbuscount + 1]).NumberFormat = "@";
                        //newWorksheet1.get_Range(newWorksheet1.Cells[1, 1], newWorksheet1.Cells[6 + outbrchcount + transcount + 1, 2 * outbuscount + 2]).Font.Name = "楷体_GB2312";
                        newWorksheet1.get_Range(newWorksheet1.Cells[4, 2], newWorksheet1.Cells[4 + brchcount, 2 * outbuscount + 1]).NumberFormat = "@";
                        newWorksheet1.get_Range(newWorksheet1.Cells[1, 1], newWorksheet1.Cells[6 + brchcount + 1, 2 * outbuscount + 2]).Font.Name = "楷体_GB2312";
                        //foreach (KeyValuePair<int, int> kvp in OverVhege)
                        //{
                        for (int i = 0; i < OverVp.Count; i++)
                        {
                            if (OverVp[i].linenum < 125)
                                newWorksheet1.get_Range(newWorksheet1.Cells[3 + OverVp[i].row, 2 * OverVp[i].linenum + 2], newWorksheet1.Cells[3 + OverVp[i].row, 2 * OverVp[i].linenum + 3]).Interior.ColorIndex = 3;

                        }
                        newWorksheet.Rows.AutoFit();
                        newWorksheet.Columns.AutoFit();
                        newWorksheet1.Rows.AutoFit();
                        newWorksheet1.Columns.AutoFit();

                        //}
                        //补偿法中的数据处理过程
                        newWorksheet1.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "可靠性计算结果.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);

                        result1.Workbooks.Close();
                        result1.Quit();
                        System.Windows.Forms.Clipboard.Clear(); //去掉剪切板中的数据
                        ex = new Excel.Application();
                        ex.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "可靠性计算结果.xls");
                        ex.Visible = true;

                    }
                    catch (System.Exception e1)
                    {
                        MessageBox.Show("数据存在问题，请仔细检查后再进行结果计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        wf.Close();
                    }
                }

            }
            if (webn1.DialogResult == DialogResult.Ignore)
            {
                PSPDEV pspDEV = new PSPDEV();
                pspDEV.ProjectID = projectid;
                pspDEV.SvgUID = projectSUID; //为了把 项目ID和卷id传入
                PartRelform selregion = new PartRelform(pspDEV);
                selregion.ShowDialog();
                if (selregion.DialogResult == DialogResult.Ignore)
                {
                    DelLinenum = selregion.lineVnumlist;
                    //DelTransnum = null;
                    QyRelanalyst(projectSUID,projectid,ratedCapacity);
                }
                //IList list1 = UCDeviceBase.DataService.GetList("SelectPSPDEVBySvgUID", psp);
                //将进行潮流计算的文档传到选择网络中。
                else if (selregion.DialogResult == DialogResult.Yes)
                {
                    DelLinenum = selregion.lineDnumlist;
                    DelTransnum = selregion.lineVnumlist;
                    //DelTransnum = null;
                    QyRelanalyst(projectSUID,projectid,ratedCapacity);
                }
            }
            //Operateflag = false;
        }
        private List<int> DelLinenum = new List<int>();      //记录要进行断开的线路编号
        private List<int> DelTransnum = new List<int>();      //记录断开的变压器线路编号
        public void QyRelanalyst(string projectSUID, string projectid, double ratedCapacity)
        {
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
            StringBuilder outputZL = new StringBuilder();
            StringBuilder outputv = new StringBuilder();//记录直流计算结果 线路功率和节点电压
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
                return;
            }
            WaitDialogForm wf = new WaitDialogForm("", "正在处理数据, 请稍候...");
            try
            {
                string datatime = System.DateTime.Now.ToString();
                System.Windows.Forms.Clipboard.Clear(); //去掉剪切板中的数据
                int linenum = DelLinenum.Count + DelTransnum.Count;
                int count = 0;
               
                for (int i = 1; i <= DelLinenum.Count; i++) //此处进行所选的一般线路N-1
                {
                    n1NL_DLL.ZYZ nl = new n1NL_DLL.ZYZ();
                    nl.jianyan(DelLinenum[i - 1]);
                    GC.Collect();
                    count++;
                    wf.SetCaption(count.ToString());
                }
                for (int i = 1; i <= DelTransnum.Count; i++) //此处进行所选的变压器线路N-1
                {
                    n1NL_DLL.ZYZ nl = new n1NL_DLL.ZYZ();
                    nl.jianyan(DelTransnum[i - 1]);
                    GC.Collect();
                    count++;
                    wf.SetCaption(count.ToString());
                }
                wf.Close();
                //int* busnumber;

                //N1Test.NBcal kk = new N1Test.NBcal();
                //busnumber = kk.Show_Reliability();
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "部分线路可靠性计算结果.xls"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "部分线路可靠性计算结果.xls");
                    //OpenRead(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + ".xls");
                }

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

                    return;

                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\VandTheta.txt"))
                {
                }
                else
                {
                    MessageBox.Show("数据不收敛，请调整参数后重新计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\lineP.txt"))
                {
                }
                else
                {
                    return;
                }
                dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\" + "lineP.txt", FileMode.Open);
                dh2 = new FileStream(System.Windows.Forms.Application.StartupPath + "\\" + "VandTheta.txt", FileMode.Open);
                readLine2 = new StreamReader(dh2, Encoding.Default);
                readLine = new StreamReader(dh, Encoding.Default);
                charSplit = new char[] { ' ' };
                //strLine = readLine.ReadLine();

                outputZL = new StringBuilder();
                //outputBC=null;                    
                outputZL.Append("部分网可靠性结果报表" + "\r\n");
                outputZL.Append("开断支路" + "," + "剩余网络线路功率Pij和Pji的有名值" + ",,");
                for (int i = 0; i < outbrchcount - 1; i++)
                {
                    outputZL.Append(",,");
                }
                outputZL.Append("是否越限" + "," + "\r\n");
                outputZL.Append(",");
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
                    outputZL.Append(branchname);
                    outputZL.Append("," + "\r\n");
                    outputZL.Append(devzl[0] + ",");
                    bool lineflag = true;      //只要有一个不合格则就为不合格
                    //将线路
                    con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Name='" + devzl[0] + "' AND PSPDEV.type='05'AND PSPDEV.ProjectID='" + projectid + "'";
                    //con = " WHERE Name='" + devzl[0] + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
                  IList  listhg = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                  PSPDEV psphg = null;
                    if (listhg.Count!=0)
                    {
                        psphg = (PSPDEV)listhg[0];
                    }
                 
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

                            con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Name='" + devzl[j * 3 + 1] + "' AND PSPDEV.type='05'AND PSPDEV.ProjectID='" + projectid + "'";
                           // con = " WHERE Name='" + devzl[j * 3 + 1] + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
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
                            if (j<125)
                            {
                                outputZL.Append("'" + youming(devzl[j * 3 + 2], capability) + "," + "'" + youming(devzl[j * 3 + 3], capability) + ",");
                            }
                            
                            if (maxSij >= linXij)
                            {
                                lineflag = false;
                                lineclass _line = new lineclass(n, j);
                                Overlinp.Add(_line);
                                // OverPhege[n] = j;
                            }

                        }
                        if (!lineflag)
                        {
                            outputZL.Append("不合格");
                            if (psphg!=null)
                            {
                                psphg.HgFlag = "不合格";
                            }
                           
                        }
                        else
                        {
                            outputZL.Append("合格");
                            if (psphg!=null)
                            {
                                psphg.HgFlag = "合格";
                            }
                           
                        }
                    }
                    else
                    {
                        outputZL.Append("该线路不可断裂。");
                        if (psphg!=null)
                        {
                            psphg.HgFlag = "合格";
                        }
                        
                    }
                    if (psphg!=null)
                    {
                        Services.BaseService.Update<PSPDEV>(psphg);
                    }
                    
                    outputZL.Append("\r\n");
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
                        con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Name='" + devzl1[0] + "' AND PSPDEV.type='05'AND PSPDEV.ProjectID='" + projectid + "'";
                        //con = " WHERE Name='" + devzl1[0] + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
                        listhg = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                        if (listhg.Count!=0)
                        {
                            psphg = (PSPDEV)listhg[0];
                        }
                 
                        if (devzl1[1] != "-1")
                        {
                            outputZL.Append(devzl1[0] + ",");
                            for (int j = 0; j < brchcount; j++)
                            {
                                double pij = Convert.ToDouble(devzl1[j * 3 + 2].Substring(0, devzl1[j * 3 + 2].IndexOf('j') - 1)) * capability;
                                double qij = Convert.ToDouble(devzl1[j * 3 + 2].Substring(devzl1[j * 3 + 2].IndexOf('j') + 1)) * capability;
                                double pji = Convert.ToDouble(devzl1[j * 3 + 3].Substring(0, devzl1[j * 3 + 3].IndexOf('j') - 1)) * capability;
                                double qji = Convert.ToDouble(devzl1[j * 3 + 3].Substring(devzl1[j * 3 + 3].IndexOf('j') + 1)) * capability;
                                double Sij = System.Math.Sqrt(pij * pij + qij * qij);
                                double Sji = System.Math.Sqrt(pji * pji + qji * qji);
                                double maxSij = (Sij > Sji) ? Sij : Sji;
                                con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Name='" + devzl1[j * 3 + 1] + "' AND PSPDEV.type='05'AND PSPDEV.ProjectID='" + projectid + "'";
                                //con = " WHERE Name='" + devzl1[j * 3 + 1] + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
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
                                    lineclass linep = new lineclass(n, j);
                                    Overlinp.Add(linep);
                                    //OverPhege[n] = j;
                                }
                                if (j<125)
                                {
                                    outputZL.Append("'" + youming(devzl1[j * 3 + 2], capability) + "," + "'" + youming(devzl1[j * 3 + 3], capability) + ",");    //在此还可以判断线路是否超载
                                }
                               
                            }
                            if (!lineflag)
                            {
                                outputZL.Append("不合格") ;
                                if (psphg!=null)
                                {
                                    psphg.HgFlag = "不合格";
                                }
                                
                            }
                            else
                            {
                                outputZL.Append("合格") ;
                                if (psphg!=null)
                                {
                                    psphg.HgFlag = "合格";
                                }
                               
                            }
                            outputZL.Append("\r\n");


                        }

                        else
                        {
                            list.Add(n);
                            outputZL.Append( devzl1[0] + "," + "为不可断裂的线路" + "\r\n");
                            if (psphg!=null)
                            {
                                psphg.HgFlag = "合格";
                            }
                           
                        }
                        if (psphg!=null)
                        {
                            Services.BaseService.Update<PSPDEV>(psphg);
                        }
                       

                    }
                }
                outputZL.Append( "注释：红色为线路超载");
                outputZL.Append("\r\n");
                outputZL.Append("操作时间为：" + datatime + "\r\n");
                outputZL.Append("单位：kA\\kV\\MW\\Mvar" + "\r\n");
                readLine.Close();
                if (File.Exists("result1.csv"))
                {
                    File.Delete("result1.csv");
                }

                op = new FileStream("result1.csv", FileMode.OpenOrCreate);
                str1 = new StreamWriter(op, Encoding.Default);
                str1.Write(outputZL);
                str1.Close();

                outputv = new StringBuilder();
                //将各个节点的电压写入其中
                // strLine2 = readLine2.ReadLine();
                n = 0;
                bool busvflag1 = true;
                outputv.Append("网络节点电压和相角" + "\r\n");
                outputv.Append("开断支路名称" + "," + "节点电压的幅值和相角的有名值");
                for (int i = 0; i < outbuscount; i++)
                {
                    outputv.Append(",,");
                }
                outputv.Append("是否越限" + "," + "\r\n");
                 outputv.Append(",");
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
                     outputv.Append(busname);
                     outputv.Append("," + "\r\n");
                    
                    if (devzl1[1] != "-1")
                    {
                         outputv.Append(devzl1[0] + ",");
                        for (int j = 0; j < buscount; j++)
                        {
                            PSPDEV pspDev = new PSPDEV();
                            con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Name='" + devzl1[j * 3 + 1] + "'AND PSPDEV.type='01'AND PSPDEV.ProjectID='" + projectid + "'";
                           // con = " WHERE Name='" + devzl1[j * 3 + 1] + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                            pspDev =(PSPDEV) UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                           
                            if (pspDev != null && pspDev.ReferenceVolt != 0)
                            {
                                volt = pspDev.ReferenceVolt;
                            }
                            else
                                volt = standvolt;
                            if (j<125)
                            {
                                outputv.Append("'" + (Convert.ToDouble(devzl1[j * 3 + 2]) * volt).ToString() + "," + "'" + devzl1[j * 3 + 3] + ",");
                            }
                            
                            if (Convert.ToDouble(devzl1[j * 3 + 2]) >= TLPSPVmax || Convert.ToDouble(devzl1[j * 3 + 2]) <= TLPSPVmin)
                            {
                                busvflag1 = false;
                                lineclass _vtheta = new lineclass(n, j);
                                OverVp.Add(_vtheta);
                                // OverVhege[n] = j;
                            }
                        }
                        if (busvflag1)
                        {
                             outputv.Append("合格");
                        }
                        else
                             outputv.Append("不合格");
                         outputv.Append("\r\n");
                    }
                    else
                    {
                        outputv.Append(devzl1[0] + "," + "为不可断裂的线路" + ",,,,,,");
                         outputv.Append("\r\n");
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
                            outputv.Append(devzl[0] + ",");
                            for (int j = 0; j < buscount; j++)
                            {
                                PSPDEV pspDev = new PSPDEV();
                                con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Name='" + devzl[j * 3 + 1] + "'AND PSPDEV.type='01'AND PSPDEV.ProjectID='" + projectid + "'";
                               // con = " WHERE Name='" + devzl[j * 3 + 1] + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                                pspDev = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                                if (pspDev != null && pspDev.ReferenceVolt != 0)
                                {
                                    volt = pspDev.ReferenceVolt;
                                }
                                else
                                    volt = standvolt;
                                if (j<125)
                                {
                                    outputv.Append("'" + (Convert.ToDouble(devzl[j * 3 + 2]) * volt).ToString() + "," + "'" + devzl[j * 3 + 3] + ",");   //在此还可以判断线路是否超载,如果超载加入一个标记,在excel里使其变为红色
                                }
                                
                                if (Convert.ToDouble(devzl[j * 3 + 2]) >= TLPSPVmax || Convert.ToDouble(devzl[j * 3 + 2]) <= TLPSPVmin)
                                {
                                    busvflag1 = false;
                                    lineclass vtheta = new lineclass(n, j);
                                    OverVp.Add(vtheta);
                                    //OverVhege[n] = j;
                                }
                            }
                            if (busvflag1)
                            {
                                 outputv.Append("合格");
                            }
                            else
                                 outputv.Append("不合格");
                             outputv.Append("\r\n");

                        }
                        else
                        {
                            // list.Add(n);
                             outputv.Append(devzl[0] + "," + "为不可断裂的线路" + ",,,,,,");
                            outputv.Append("\r\n");
                        }

                    }
                }
                readLine2.Close();
                 outputv.Append( "注释：红色为节点电压超载" + "\r\n");
                 outputv.Append( "节点电压合格范围为电压基准值的上下限，分别为" + TLPSPVmax + "和" + TLPSPVmin + "倍" + "\r\n");
                 outputv.Append("操作时间为：" + datatime + "\r\n");
                 outputv.Append("单位：kA\\kV\\MW\\Mvar" + "\r\n");

                //op = new FileStream(System.Windows.Forms.Application.StartupPath + "\\" + "reli.txt", FileMode.OpenOrCreate);
                //str1 = new StreamWriter(op, Encoding.Default);
                //str1.Write(outputZL);
                //str1.Close();


                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + "result2.csv"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + "result2.csv");
                }

                op = new FileStream(System.Windows.Forms.Application.StartupPath + "\\" + "result2.csv", FileMode.OpenOrCreate);
                str1 = new StreamWriter(op, Encoding.Default);
                str1.Write(outputv);
                str1.Close();

                result1 = new Excel.Application();
                result1.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\" + "result1.csv");
                newWorksheet = (Excel.Worksheet)result1.Worksheets[1];
                result1.Worksheets.Add(System.Reflection.Missing.Value, newWorksheet, 1, System.Reflection.Missing.Value);

                Excel.Application result2 = new Excel.Application();
                result2.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result2.csv");
                Excel.Worksheet tempSheet = (Excel.Worksheet)result2.Worksheets.get_Item(1);
                Excel.Worksheet newWorksheet1 = (Excel.Worksheet)result1.Worksheets.get_Item(2);
                newWorksheet.Name = "一般线路可靠性";
                newWorksheet1.Name = "节点电压可靠性";
                result1.Visible = true;


                tempSheet.Cells.Select();
                tempSheet.Cells.Copy(System.Reflection.Missing.Value);
                newWorksheet1.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);

                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 2 * outbrchcount + 2]).MergeCells = true;
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Size = 20;
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Name = "黑体";
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                newWorksheet.get_Range(newWorksheet.Cells[2, 1], newWorksheet.Cells[3, 1]).MergeCells = true;
                newWorksheet.get_Range(newWorksheet.Cells[2, 2], newWorksheet.Cells[2, 2 * outbrchcount + 1]).MergeCells = true;
                newWorksheet.get_Range(newWorksheet.Cells[2, 2], newWorksheet.Cells[2, 2]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                newWorksheet.get_Range(newWorksheet.Cells[2, 2 * outbrchcount + 2], newWorksheet.Cells[3, 2 * outbrchcount + 2]).MergeCells = true;
                for (int i = 0; i < outbrchcount; i++)
                {
                    newWorksheet.get_Range(newWorksheet.Cells[3, 2 * i + 2], newWorksheet.Cells[3, 2 * i + 3]).MergeCells = true;
                    newWorksheet.get_Range(newWorksheet.Cells[3, 2 * i + 2], newWorksheet.Cells[3, 2 * i + 3]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                }
                // newWorksheet.get_Range(newWorksheet.Cells[4, 2], newWorksheet.Cells[4, brchcount + 1]).Interior.ColorIndex = 45;
                newWorksheet.get_Range(newWorksheet.Cells[4, 1], newWorksheet.Cells[3 + linenum, 1]).Interior.ColorIndex = 6;
                //newWorksheet.get_Range(newWorksheet.Cells[5, 2], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, brchcount + 1]).NumberFormat = "0.0000_ ";
                newWorksheet.get_Range(newWorksheet.Cells[4, 2], newWorksheet.Cells[3 + linenum, 2 * outbrchcount + 1]).NumberFormat = "@";
                newWorksheet.get_Range(newWorksheet.Cells[2, 1], newWorksheet.Cells[6 + linenum, 2 * outbrchcount + 2]).Font.Name = "楷体_GB2312";
                //在此处将其不合格的数据显示出来
                //foreach (KeyValuePair<int, int> kvp in OverPhege)
                //{
                for (int i = 0; i < Overlinp.Count; i++)
                {
                    if(Overlinp[i].linenum<125)
                    newWorksheet.get_Range(newWorksheet.Cells[3 + Overlinp[i].row, 2 * Overlinp[i].linenum + 2], newWorksheet.Cells[3 + Overlinp[i].row, 2 * Overlinp[i].linenum + 3]).Interior.ColorIndex = 3;
                }


                //}
                //补偿法中的数据处理过程
               newWorksheet1.get_Range(newWorksheet1.Cells[1, 1], newWorksheet1.Cells[1, 2 * outbuscount + 2]).MergeCells = true;
                newWorksheet1.get_Range(newWorksheet1.Cells[1, 1], newWorksheet1.Cells[1, 1]).Font.Size = 20;
                newWorksheet1.get_Range(newWorksheet1.Cells[1, 1], newWorksheet1.Cells[1, 1]).Font.Name = "黑体";
                newWorksheet1.get_Range(newWorksheet1.Cells[1, 1], newWorksheet1.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                newWorksheet1.get_Range(newWorksheet1.Cells[2, 2], newWorksheet1.Cells[2, 2]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                newWorksheet1.get_Range(newWorksheet1.Cells[2, 1], newWorksheet1.Cells[3, 1]).MergeCells = true;  //补偿法中前面开断合并
                newWorksheet1.get_Range(newWorksheet1.Cells[2, 2], newWorksheet1.Cells[2, 2 * outbuscount + 1]).MergeCells = true;
                newWorksheet1.get_Range(newWorksheet1.Cells[2, 2 * outbuscount + 2], newWorksheet1.Cells[3, 2 * outbuscount + 2]).MergeCells = true;   //合格合并
                newWorksheet1.get_Range(newWorksheet1.Cells[4, 1], newWorksheet1.Cells[4 + linenum - 1, 1]).Interior.ColorIndex = 6;
                for (int i = 0; i < outbuscount; i++)
                {
                    newWorksheet1.get_Range(newWorksheet1.Cells[3, 2 * i + 2], newWorksheet1.Cells[3, 2 * i + 3]).MergeCells = true;
                    newWorksheet1.get_Range(newWorksheet1.Cells[3, 2 * i + 2], newWorksheet1.Cells[3, 2 * i + 3]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                }
                //newWorksheet1.get_Range(newWorksheet1.Cells[7 + brchcount, 2], newWorksheet1.Cells[7 + brchcount, brchcount + 1]).Interior.ColorIndex = 45;
                //newWorksheet1.get_Range(newWorksheet1.Cells[8 + brchcount, 1], newWorksheet1.Cells[8 + 2 * brchcount, 1]).Interior.ColorIndex = 6;
                //newWorksheet1.get_Range(newWorksheet1.Cells[5, 2], newWorksheet1.Cells[newWorksheet1.UsedRange.Rows.Count, brchcount + 1]).NumberFormat = "0.0000_ ";
                newWorksheet1.get_Range(newWorksheet1.Cells[4, 2], newWorksheet1.Cells[4 + linenum, 2 * outbuscount + 1]).NumberFormat = "@";
                newWorksheet1.get_Range(newWorksheet1.Cells[1, 1], newWorksheet1.Cells[8 + linenum, 2 * outbuscount + 2]).Font.Name = "楷体_GB2312";
                //foreach (KeyValuePair<int, int> kvp in OverVhege)
                //{
                for (int i = 0; i < OverVp.Count; i++)
                {
                    if (OverVp[i].linenum<125)
                    
                    newWorksheet1.get_Range(newWorksheet1.Cells[3 + OverVp[i].row, 2 * OverVp[i].linenum + 2], newWorksheet1.Cells[3 + OverVp[i].row, 2 * OverVp[i].linenum + 3]).Interior.ColorIndex = 3;
                }
                newWorksheet.Rows.AutoFit();
                newWorksheet.Columns.AutoFit();
                newWorksheet1.Rows.AutoFit();
                newWorksheet1.Columns.AutoFit();
                //}
                //补偿法中的数据处理过程
                newWorksheet.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "部分线路可靠性计算结果.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);
                result1.Workbooks.Close();
                result1.Quit();
                System.Windows.Forms.Clipboard.Clear(); //去掉剪切板中的数据
                ex = new Excel.Application();
                ex.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "部分线路可靠性计算结果.xls");
                ex.Visible = true;
            }
            catch (System.Exception e1)
            {
                MessageBox.Show("数据存在问题，请仔细检查以后再看结果！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                wf.Close();
            }
           
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
                    for (int j = 0; j < outbrchcount; j++)
                    {

                        outputZL += devzl[3 * j + 1] + "," + ",";  //防止excel超出了列
                       
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
                    for (int j = 0; j < outbuscount; j++)              //防止excel超出了列
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
    public class lineclass
    {
        public int row;                  //记录第几行条线路断开时出现不合格的
        public int linenum;               //记录那个不合格的
        public lineclass(int _row, int _linenum)
        {
            row = _row;
            linenum = _linenum;
        }
    }
   
}

