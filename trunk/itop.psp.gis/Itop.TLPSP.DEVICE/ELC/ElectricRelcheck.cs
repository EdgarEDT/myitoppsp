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
        //��ö�·�����text�ı�
        //
        //projectsuidΪ��Ŀ��ID�� projectidΪ���̵ı�� ��������Ŀ�ľ��
        public int brchcount, buscount, transcount,outbrchcount,outbuscount;        //��¼ȫ�����볱�������֧·����ĸ����Ŀ �����������ѹ��������֧·
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
                                string temp = "���ӷ���ʧ��,";
                                temp += dev.Name;
                                temp += "û����ȷ����,����д�����";
                                System.Windows.Forms.MessageBox.Show(temp, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                string temp = "���ӷ���ʧ��,";
                                temp += dev.Name;
                                temp += "û����ȷ����,����д�����";
                                System.Windows.Forms.MessageBox.Show(temp, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                string temp = "���ӷ���ʧ��,";
                                temp += dev.Name;
                                temp += "û����ȷ����,����д�����";
                               System.Windows.Forms. MessageBox.Show(temp, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    //���˷����Ƿ���ڹ���ĸ��ĸ�߽ڵ�
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
                        string temp = "���˷���ʧ��";
                        for (int i = 0; i < busname.Count; i++)
                        {
                            temp += "��" + busname[i];

                        }
                        temp += "Ϊ�����Ľڵ㣡";
                        System.Windows.Forms.MessageBox.Show(temp, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if ((strBus.Contains("������") || strBus.Contains("�������"))||( strBranch.Contains("������") || strBranch.Contains("�������")))
                {
                    MessageBox.Show("ȱ�ٲ������������������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("���ݴ���������������ȫ���ٲ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
        public double TLPSPVmin = 0.90, TLPSPVmax = 1.1;
        public void WebCalAndPrint(string projectSUID, string projectid, double ratedCapacity)            //����N-1��������
        {
            FileStream dh;
            StreamReader readLine;
            // StreamReader readLine;
            ArrayList list = new ArrayList();   //������¼��·���ܽ��ѵ�λ��
            char[] charSplit;
            string strLine;
            string[] array1;
            StringBuilder outputZL = new StringBuilder(); 
            StringBuilder outputv=new StringBuilder();//��¼ֱ�������� ��·���ʺͽڵ��ѹ
            //string outputBC = null;   //��¼���������� �ڵ��ѹ
            string[] array2;

            string strLine2;

            char[] charSplit2 = new char[] { ' ' };
            List<lineclass> Overlinp = new List<lineclass>();
            List<lineclass> OverVp = new List<lineclass>();
            //Dictionary<int, List<lineclass>> OverPhege = new Dictionary<int, List<lineclass>>();       //Ϊ ��·���ʵļ��� ��ֵΪ�Ͽ���·�ı�ţ�ֵΪ�ڼ�����·�����˲��ϸ�
            //Dictionary<int, List<lineclass>> OverVhege = new Dictionary<int, List<lineclass>>();       //Ϊ �ڵ��ѹ�ļ��� ��ֵΪ�Ͽ���·�ı�ţ�ֵΪ�ڼ����ڵ�����˲��ϸ�
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
            if (webn1.DialogResult == DialogResult.OK)    //����ȫ������
            {
                OuptResultForm outtype = new OuptResultForm();
                outtype.ShowDialog();
                if (outtype.DialogResult==DialogResult.Ignore)
                {
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "�ɿ��Լ�����.xls"))
                    {
                        System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "�ɿ��Լ�����.xls");
                        //OpenRead(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + ".xls");
                    }
                    else
                    {
                        MessageBox.Show("�����в����ڼ���Ľ���������¼�����ٳ��ԣ�");
                    }
                }
                else if (outtype.DialogResult==DialogResult.OK)
                {
                    if (!CheckN(projectSUID, projectid, ratedCapacity))
                    {
                        return;
                    }
                    WaitDialogForm wf = new WaitDialogForm("", "���ڴ�������, ���Ժ�...");
                    try
                    {
                        string datatime = System.DateTime.Now.ToString();
                        System.Windows.Forms.Clipboard.Clear(); //ȥ�����а��е�����
                        //for (int i = 1; i <= brchcount + transcount; i++)//ԭ�ȵ�
                       // ԭ�ȳ���
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
                        //wf.Close();
                        //int* busnumber;

                        //N1Test.NBcal kk = new N1Test.NBcal();
                        //busnumber = kk.Show_Reliability();
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "�ɿ��Լ�����.xls"))
                        {
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "�ɿ��Լ�����.xls");
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
                        //    MessageBox.Show("�����û�׼���ٽ��м���!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            MessageBox.Show("ѡ���ĸ���ִ��ڹ����Ľڵ㣡", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            return;

                        }
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\VandTheta.txt"))
                        {
                        }
                        else
                        {
                            MessageBox.Show("���ݲ���������������������¼��㣡", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        outputZL.Append("ȫ���ɿ��Խ������" + "\r\n");
                        outputZL.Append("����֧·����" + "," + "ʣ��������·����Pij��Pji������ֵ" + ",,");
                        for (int i = 0; i < outbrchcount - 1; i++)
                        {
                            outputZL.Append(",,");
                        }
                        outputZL.Append("�Ƿ�Խ��" + "," + "\r\n");
                        outputZL.Append(",");
                        int n = 0; //��¼��·������

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
                            //���ϸ�д�뵽��·���ֶ�������ӵ�
                           con=",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.Name='"+devzl[0]+"'AND PSPDEV.type='05'AND PSPDEV.ProjectID='"+projectid+"'" ;
                           // con = " WHERE Name='" + devzl[0] + "' AND ProjectID = '" + projectid + "'" + "AND Type='05'";
                            IList listhg = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                            PSPDEV psphg = null;
                            if (listhg.Count != 0)
                            {
                                psphg = (PSPDEV)listhg[0];
                            }

                            bool lineflag = true;      //ֻҪ��һ�����ϸ����Ϊ���ϸ�
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
                                    //    MessageBox.Show(pspline.Name + "����·����û�����룬�޷����пɿ��Լ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                    outputZL.Append("���ϸ�");
                                    if (psphg != null)
                                    {
                                        psphg.HgFlag = "���ϸ�";
                                    }

                                }
                                else
                                {
                                    outputZL.Append("�ϸ�");
                                    if (psphg != null)
                                    {
                                        psphg.HgFlag = "�ϸ�";
                                    }

                                }
                            }
                            else
                            {
                                outputZL.Append("����·���ɶ�");
                                if (psphg != null)
                                {
                                    psphg.HgFlag = "�ϸ�";
                                }

                            }
                            //д��ϸ�
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
                                        //    MessageBox.Show(pspline.Name + "����·����û�����룬�޷����пɿ��Լ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                            outputZL.Append("'" + youming(devzl1[j * 3 + 2], capability) + "," + "'" + youming(devzl1[j * 3 + 3], capability) + ",");    //�ڴ˻������ж���·�Ƿ���

                                    }
                                    if (!lineflag)
                                    {
                                        outputZL.Append("���ϸ�");
                                        if (psphg != null)
                                        {
                                            psphg.HgFlag = "���ϸ�";
                                        }

                                    }
                                    else
                                    {
                                        outputZL.Append("�ϸ�");
                                        if (psphg != null)
                                        {
                                            psphg.HgFlag = "�ϸ�";
                                        }

                                    }
                                    outputZL.Append("\r\n");
                                    //OverPhege[n] = Overlinp;
                                    //Overlinp.Clear();

                                }

                                else
                                {
                                    list.Add(n);
                                    outputZL.Append(devzl1[0] + "," + "Ϊ���ɶ��ѵ���·");
                                    if (psphg != null)
                                    {
                                        psphg.HgFlag = "�ϸ�";
                                    }

                                    outputZL.Append("\r\n");
                                }
                                if (psphg != null)
                                {
                                    Services.BaseService.Update<PSPDEV>(psphg);
                                }

                            }
                        }
                        outputZL.Append("ע�ͣ���ɫΪ��·����" + "\r\n");
                        outputZL.Append("����ʱ��Ϊ��" + datatime);
                        outputZL.Append("\r\n");
                        outputZL.Append("��λ��kA\\kV\\MW\\Mvar" + "\r\n");
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
                        //�������ڵ�ĵ�ѹд������
                        // strLine2 = readLine2.ReadLine();
                        n = 0;
                        bool busvflag1 = true;
                        outputv.Append("����ڵ��ѹ�����" + "\r\n");
                        outputv.Append("����֧·����" + "," + "�ڵ��ѹ�ķ�ֵ����ǵ�����ֵ");
                        for (int i = 0; i < outbuscount; i++)
                        {
                            outputv.Append(",,");
                        }
                        outputv.Append("�Ƿ�Խ��" + "," + "\r\n");
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
                                    outputv.Append("�ϸ�");
                                }
                                else
                                {
                                    outputv.Append("���ϸ�");
                                }
                            }
                            else
                            {
                                outputv.Append("���ɶ���");
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
                                            outputv.Append("'" + (Convert.ToDouble(devzl1[j * 3 + 2]) * volt).ToString() + "," + "'" + devzl1[j * 3 + 3] + ",");   //�ڴ˻������ж���·�Ƿ���,������ؼ���һ�����,��excel��ʹ���Ϊ��ɫ

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
                                        outputv.Append("�ϸ�");
                                    }
                                    else
                                        outputv.Append("���ϸ�");
                                    outputv.Append("\r\n");
                                    //OverPhege[n] = OverVp;
                                    //OverVp.Clear();
                                }
                                else
                                {
                                    // list.Add(n);
                                    outputv.Append(devzl1[0] + "," + "Ϊ���ɶ��ѵ���·" + ",,,,,,");
                                    outputv.Append("\r\n");
                                }

                            }
                        }
                        outputv.Append("ע�ͣ���ɫΪ�ڵ��ѹ����" + "\r\n");
                        outputv.Append("�ڵ��ѹ�ϸ�ΧΪ��ѹ��׼ֵ�������ޣ��ֱ�Ϊ" + TLPSPVmax + "��" + TLPSPVmin + "��" + "\r\n");
                        outputv.Append("����ʱ��Ϊ��" + datatime + "\r\n");
                        outputv.Append("��λ��kA\\kV\\MW\\Mvar" + "\r\n");
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
                        newWorksheet.Name = "һ����·�ɿ���";
                        newWorksheet1.Name = "�ڵ��ѹ�ɿ���";
                        result1.Visible = true;


                        tempSheet.Cells.Select();
                        tempSheet.Cells.Copy(System.Reflection.Missing.Value);
                        newWorksheet1.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);

                        newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 2 * outbrchcount + 2]).MergeCells = true;
                        newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Size = 20;
                        newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Name = "����";
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
                        // newWorksheet.get_Range(newWorksheet.Cells[2, 1], newWorksheet.Cells[6 + outbrchcount + transcount, 2 * outbrchcount + 2]).Font.Name = "����_GB2312";
                        newWorksheet.get_Range(newWorksheet.Cells[4, 2], newWorksheet.Cells[3 + brchcount, 2 * outbrchcount + 1]).NumberFormat = "@";
                        newWorksheet.get_Range(newWorksheet.Cells[2, 1], newWorksheet.Cells[6 + brchcount , 2 * outbrchcount + 2]).Font.Name = "����_GB2312";
                        //�ڴ˴����䲻�ϸ��������ʾ����
                        //foreach (KeyValuePair<int, List<lineclass>> kvp in OverPhege)
                        //{
                        for (int i = 0; i < Overlinp.Count; i++)
                        {
                            if (Overlinp[i].linenum < 125)
                                newWorksheet.get_Range(newWorksheet.Cells[3 + Overlinp[i].row, 2 * Overlinp[i].linenum + 2], newWorksheet.Cells[3 + Overlinp[i].row, 2 * Overlinp[i].linenum + 3]).Interior.ColorIndex = 3;
                        }

                        //}
                        //�������е����ݴ������

                        newWorksheet1.get_Range(newWorksheet1.Cells[1, 1], newWorksheet1.Cells[1, 2 * outbuscount + 2]).MergeCells = true;
                        newWorksheet1.get_Range(newWorksheet1.Cells[1, 1], newWorksheet1.Cells[1, 1]).Font.Size = 20;
                        newWorksheet1.get_Range(newWorksheet1.Cells[1, 1], newWorksheet1.Cells[1, 1]).Font.Name = "����";
                        newWorksheet1.get_Range(newWorksheet1.Cells[1, 1], newWorksheet1.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        newWorksheet1.get_Range(newWorksheet1.Cells[2, 2], newWorksheet1.Cells[2, 2]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        newWorksheet1.get_Range(newWorksheet1.Cells[2, 1], newWorksheet1.Cells[3, 1]).MergeCells = true;  //��������ǰ�濪�Ϻϲ�
                        newWorksheet1.get_Range(newWorksheet1.Cells[2, 2], newWorksheet1.Cells[2, 2 * outbuscount + 1]).MergeCells = true;
                        newWorksheet1.get_Range(newWorksheet1.Cells[2, 2 * outbuscount + 2], newWorksheet1.Cells[3, 2 * outbuscount + 2]).MergeCells = true;   //�ϸ�ϲ�
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
                        //newWorksheet1.get_Range(newWorksheet1.Cells[1, 1], newWorksheet1.Cells[6 + outbrchcount + transcount + 1, 2 * outbuscount + 2]).Font.Name = "����_GB2312";
                        newWorksheet1.get_Range(newWorksheet1.Cells[4, 2], newWorksheet1.Cells[4 + brchcount, 2 * outbuscount + 1]).NumberFormat = "@";
                        newWorksheet1.get_Range(newWorksheet1.Cells[1, 1], newWorksheet1.Cells[6 + brchcount + 1, 2 * outbuscount + 2]).Font.Name = "����_GB2312";
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
                        //�������е����ݴ������
                        newWorksheet1.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "�ɿ��Լ�����.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);

                        result1.Workbooks.Close();
                        result1.Quit();
                        System.Windows.Forms.Clipboard.Clear(); //ȥ�����а��е�����
                        ex = new Excel.Application();
                        ex.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "�ɿ��Լ�����.xls");
                        ex.Visible = true;

                    }
                    catch (System.Exception e1)
                    {
                        MessageBox.Show("���ݴ������⣬����ϸ�����ٽ��н�����㣡", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        wf.Close();
                    }
                }

            }
            if (webn1.DialogResult == DialogResult.Ignore)
            {
                PSPDEV pspDEV = new PSPDEV();
                pspDEV.ProjectID = projectid;
                pspDEV.SvgUID = projectSUID; //Ϊ�˰� ��ĿID�;�id����
                PartRelform selregion = new PartRelform(pspDEV);
                selregion.ShowDialog();
                if (selregion.DialogResult == DialogResult.Ignore)
                {
                    DelLinenum = selregion.lineVnumlist;
                    //DelTransnum = null;
                    QyRelanalyst(projectSUID,projectid,ratedCapacity);
                }
                //IList list1 = UCDeviceBase.DataService.GetList("SelectPSPDEVBySvgUID", psp);
                //�����г���������ĵ�����ѡ�������С�
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
        private List<int> DelLinenum = new List<int>();      //��¼Ҫ���жϿ�����·���
        private List<int> DelTransnum = new List<int>();      //��¼�Ͽ��ı�ѹ����·���
        public void QyRelanalyst(string projectSUID, string projectid, double ratedCapacity)
        {
            FileStream dh;
            StreamReader readLine;
            // StreamReader readLine;
            ArrayList list = new ArrayList();   //������¼��·���ܽ��ѵ�λ��
            List<lineclass> Overlinp = new List<lineclass>();
            List<lineclass> OverVp = new List<lineclass>();
            //Dictionary<int, int> OverPhege = new Dictionary<int, int>();       //Ϊ ��·���ʵļ��� ��ֵΪ�Ͽ���·�ı�ţ�ֵΪ�ڼ�����·�����˲��ϸ�
            //Dictionary<int, int> OverVhege = new Dictionary<int, int>();       //Ϊ �ڵ��ѹ�ļ��� ��ֵΪ�Ͽ���·�ı�ţ�ֵΪ�ڼ����ڵ�����˲��ϸ�
            char[] charSplit;
            string strLine;
            string[] array1;
            StringBuilder outputZL = new StringBuilder();
            StringBuilder outputv = new StringBuilder();//��¼ֱ�������� ��·���ʺͽڵ��ѹ
            //string outputBC = null;   //��¼���������� �ڵ��ѹ
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
            WaitDialogForm wf = new WaitDialogForm("", "���ڴ�������, ���Ժ�...");
            try
            {
                string datatime = System.DateTime.Now.ToString();
                System.Windows.Forms.Clipboard.Clear(); //ȥ�����а��е�����
                int linenum = DelLinenum.Count + DelTransnum.Count;
                int count = 0;
               
                for (int i = 1; i <= DelLinenum.Count; i++) //�˴�������ѡ��һ����·N-1
                {
                    n1NL_DLL.ZYZ nl = new n1NL_DLL.ZYZ();
                    nl.jianyan(DelLinenum[i - 1]);
                    GC.Collect();
                    count++;
                    wf.SetCaption(count.ToString());
                }
                for (int i = 1; i <= DelTransnum.Count; i++) //�˴�������ѡ�ı�ѹ����·N-1
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
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "������·�ɿ��Լ�����.xls"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "������·�ɿ��Լ�����.xls");
                    //OpenRead(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + ".xls");
                }

                double yinzi = 0, capability = 0, volt = 0, current = 0, standvolt = 1, Rad_to_Deg = 57.29577951;
                //PSPDEV benchmark = new PSPDEV();
                //benchmark.Type = "power";
                //benchmark.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                //IList list3 = UCDeviceBase.DataService.GetList("SelectPSPDEVBySvgUIDAndType", benchmark);
                //if (list3 == null)
                //{
                //    MessageBox.Show("�����û�׼���ٽ��м���!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("ѡ���ĸ�ߴ��ڹ����ڵ㣡", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;

                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\VandTheta.txt"))
                {
                }
                else
                {
                    MessageBox.Show("���ݲ���������������������¼��㣡", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                outputZL.Append("�������ɿ��Խ������" + "\r\n");
                outputZL.Append("����֧·" + "," + "ʣ��������·����Pij��Pji������ֵ" + ",,");
                for (int i = 0; i < outbrchcount - 1; i++)
                {
                    outputZL.Append(",,");
                }
                outputZL.Append("�Ƿ�Խ��" + "," + "\r\n");
                outputZL.Append(",");
                int n = 0; //��¼��·������

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
                    bool lineflag = true;      //ֻҪ��һ�����ϸ����Ϊ���ϸ�
                    //����·
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
                            //    MessageBox.Show(pspline.Name + "����·����û�����룬�޷����пɿ��Լ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            outputZL.Append("���ϸ�");
                            if (psphg!=null)
                            {
                                psphg.HgFlag = "���ϸ�";
                            }
                           
                        }
                        else
                        {
                            outputZL.Append("�ϸ�");
                            if (psphg!=null)
                            {
                                psphg.HgFlag = "�ϸ�";
                            }
                           
                        }
                    }
                    else
                    {
                        outputZL.Append("����·���ɶ��ѡ�");
                        if (psphg!=null)
                        {
                            psphg.HgFlag = "�ϸ�";
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
                                //    MessageBox.Show(pspline.Name + "����·����û�����룬�޷����пɿ��Լ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                    outputZL.Append("'" + youming(devzl1[j * 3 + 2], capability) + "," + "'" + youming(devzl1[j * 3 + 3], capability) + ",");    //�ڴ˻������ж���·�Ƿ���
                                }
                               
                            }
                            if (!lineflag)
                            {
                                outputZL.Append("���ϸ�") ;
                                if (psphg!=null)
                                {
                                    psphg.HgFlag = "���ϸ�";
                                }
                                
                            }
                            else
                            {
                                outputZL.Append("�ϸ�") ;
                                if (psphg!=null)
                                {
                                    psphg.HgFlag = "�ϸ�";
                                }
                               
                            }
                            outputZL.Append("\r\n");


                        }

                        else
                        {
                            list.Add(n);
                            outputZL.Append( devzl1[0] + "," + "Ϊ���ɶ��ѵ���·" + "\r\n");
                            if (psphg!=null)
                            {
                                psphg.HgFlag = "�ϸ�";
                            }
                           
                        }
                        if (psphg!=null)
                        {
                            Services.BaseService.Update<PSPDEV>(psphg);
                        }
                       

                    }
                }
                outputZL.Append( "ע�ͣ���ɫΪ��·����");
                outputZL.Append("\r\n");
                outputZL.Append("����ʱ��Ϊ��" + datatime + "\r\n");
                outputZL.Append("��λ��kA\\kV\\MW\\Mvar" + "\r\n");
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
                //�������ڵ�ĵ�ѹд������
                // strLine2 = readLine2.ReadLine();
                n = 0;
                bool busvflag1 = true;
                outputv.Append("����ڵ��ѹ�����" + "\r\n");
                outputv.Append("����֧·����" + "," + "�ڵ��ѹ�ķ�ֵ����ǵ�����ֵ");
                for (int i = 0; i < outbuscount; i++)
                {
                    outputv.Append(",,");
                }
                outputv.Append("�Ƿ�Խ��" + "," + "\r\n");
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
                             outputv.Append("�ϸ�");
                        }
                        else
                             outputv.Append("���ϸ�");
                         outputv.Append("\r\n");
                    }
                    else
                    {
                        outputv.Append(devzl1[0] + "," + "Ϊ���ɶ��ѵ���·" + ",,,,,,");
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
                                    outputv.Append("'" + (Convert.ToDouble(devzl[j * 3 + 2]) * volt).ToString() + "," + "'" + devzl[j * 3 + 3] + ",");   //�ڴ˻������ж���·�Ƿ���,������ؼ���һ�����,��excel��ʹ���Ϊ��ɫ
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
                                 outputv.Append("�ϸ�");
                            }
                            else
                                 outputv.Append("���ϸ�");
                             outputv.Append("\r\n");

                        }
                        else
                        {
                            // list.Add(n);
                             outputv.Append(devzl[0] + "," + "Ϊ���ɶ��ѵ���·" + ",,,,,,");
                            outputv.Append("\r\n");
                        }

                    }
                }
                readLine2.Close();
                 outputv.Append( "ע�ͣ���ɫΪ�ڵ��ѹ����" + "\r\n");
                 outputv.Append( "�ڵ��ѹ�ϸ�ΧΪ��ѹ��׼ֵ�������ޣ��ֱ�Ϊ" + TLPSPVmax + "��" + TLPSPVmin + "��" + "\r\n");
                 outputv.Append("����ʱ��Ϊ��" + datatime + "\r\n");
                 outputv.Append("��λ��kA\\kV\\MW\\Mvar" + "\r\n");

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
                newWorksheet.Name = "һ����·�ɿ���";
                newWorksheet1.Name = "�ڵ��ѹ�ɿ���";
                result1.Visible = true;


                tempSheet.Cells.Select();
                tempSheet.Cells.Copy(System.Reflection.Missing.Value);
                newWorksheet1.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);

                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 2 * outbrchcount + 2]).MergeCells = true;
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Size = 20;
                newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Name = "����";
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
                newWorksheet.get_Range(newWorksheet.Cells[2, 1], newWorksheet.Cells[6 + linenum, 2 * outbrchcount + 2]).Font.Name = "����_GB2312";
                //�ڴ˴����䲻�ϸ��������ʾ����
                //foreach (KeyValuePair<int, int> kvp in OverPhege)
                //{
                for (int i = 0; i < Overlinp.Count; i++)
                {
                    if(Overlinp[i].linenum<125)
                    newWorksheet.get_Range(newWorksheet.Cells[3 + Overlinp[i].row, 2 * Overlinp[i].linenum + 2], newWorksheet.Cells[3 + Overlinp[i].row, 2 * Overlinp[i].linenum + 3]).Interior.ColorIndex = 3;
                }


                //}
                //�������е����ݴ������
               newWorksheet1.get_Range(newWorksheet1.Cells[1, 1], newWorksheet1.Cells[1, 2 * outbuscount + 2]).MergeCells = true;
                newWorksheet1.get_Range(newWorksheet1.Cells[1, 1], newWorksheet1.Cells[1, 1]).Font.Size = 20;
                newWorksheet1.get_Range(newWorksheet1.Cells[1, 1], newWorksheet1.Cells[1, 1]).Font.Name = "����";
                newWorksheet1.get_Range(newWorksheet1.Cells[1, 1], newWorksheet1.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                newWorksheet1.get_Range(newWorksheet1.Cells[2, 2], newWorksheet1.Cells[2, 2]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                newWorksheet1.get_Range(newWorksheet1.Cells[2, 1], newWorksheet1.Cells[3, 1]).MergeCells = true;  //��������ǰ�濪�Ϻϲ�
                newWorksheet1.get_Range(newWorksheet1.Cells[2, 2], newWorksheet1.Cells[2, 2 * outbuscount + 1]).MergeCells = true;
                newWorksheet1.get_Range(newWorksheet1.Cells[2, 2 * outbuscount + 2], newWorksheet1.Cells[3, 2 * outbuscount + 2]).MergeCells = true;   //�ϸ�ϲ�
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
                newWorksheet1.get_Range(newWorksheet1.Cells[1, 1], newWorksheet1.Cells[8 + linenum, 2 * outbuscount + 2]).Font.Name = "����_GB2312";
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
                //�������е����ݴ������
                newWorksheet.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "������·�ɿ��Լ�����.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);
                result1.Workbooks.Close();
                result1.Quit();
                System.Windows.Forms.Clipboard.Clear(); //ȥ�����а��е�����
                ex = new Excel.Application();
                ex.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\" + psproject.Name + "������·�ɿ��Լ�����.xls");
                ex.Visible = true;
            }
            catch (System.Exception e1)
            {
                MessageBox.Show("���ݴ������⣬����ϸ����Ժ��ٿ������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                        outputZL += devzl[3 * j + 1] + "," + ",";  //��ֹexcel��������
                       
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
                    for (int j = 0; j < outbuscount; j++)              //��ֹexcel��������
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
        public int row;                  //��¼�ڼ�������·�Ͽ�ʱ���ֲ��ϸ��
        public int linenum;               //��¼�Ǹ����ϸ��
        public lineclass(int _row, int _linenum)
        {
            row = _row;
            linenum = _linenum;
        }
    }
   
}

