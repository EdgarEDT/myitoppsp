using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using System.IO;
using System.Configuration;
//using N1Test;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
namespace Itop.TLPSP.DEVICE
{
    public class ElectricWjgh
    {
        private string MXNodeType(string nodeType)
        {
            if (nodeType == "0")
            {
                return "3";
            }
            return nodeType;
        }
        public List<linedaixuan> waitlinecoll = new List<linedaixuan>();
        public List<eleclass> JDlinecol = new List<eleclass>();      //ĳһ�׶β��������·���� Ϊ��·״̬�仯
        public void JDlinecheck(string GprogUID,int Type)
        {
           
              
                JDlinecol.Clear();
                if (Type == 2)
                {
                    string strCon = "WHERE Type='01' AND SvgUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and Type in ('���վ','��Դ') and L1 in('����','����')) ";
                    string strCon2 = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·'and ZTstatus in('����','��ѡ')) ";                 
                        IList listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                        IList listXL = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon2);
                        foreach (PSPDEV dev in listXL)
                        {
                            if (dev.KSwitchStatus == "0")
                            {
                                bool fistflag = false;
                                bool lastflag = false;
                                foreach (PSPDEV pspdev in listMX)
                                {
                                    if (dev.IName == pspdev.Name)
                                    {
                                        fistflag = true;
                                    }
                                    if (dev.JName == pspdev.Name)
                                    {
                                        lastflag = true;
                                    }
                                }
                                if (lastflag && fistflag)
                                {
                                    eleclass el = new eleclass(dev.Name, dev.SUID, dev.Type, true);
                                    JDlinecol.Add(el);
                                }


                            }
                        }                                
                }
              if (Type == 3)
                {
                 string strCon = "WHERE Type='01' AND SvgUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and Type in ('���վ','��Դ') and L1 in('����','����','����')) ";
                 string strCon2 = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·' and JQstatus in('����','Ͷ��','��ѡ')) ";
                    IList listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    IList listXL = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon2);
                    foreach (PSPDEV dev in listXL)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            bool fistflag=false;
                            bool lastflag=false;
                            foreach (PSPDEV pspdev in listMX)
                            {
                                if (dev.IName == pspdev.Name)
                                {
                                    fistflag = true;
                                }
                                if (dev.JName == pspdev.Name)
                                {
                                    lastflag = true;
                                }
                            }
                            if (lastflag&&fistflag)
                            {
                                if (dev.LineStatus=="�ȴ�")
                                {
                                    dev.LineStatus = "��ѡ";
                                    Services.BaseService.Update<PSPDEV>(dev);
                                }
                                eleclass el = new eleclass(dev.Name, dev.SUID, dev.Type, true);
                                JDlinecol.Add(el);
                            }
                          
                           
                        }
                    }

               }
               if (Type == 4)
               {
                 string strCon = "WHERE Type='01' AND SvgUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and Type in ('���վ','��Դ') ) ";
                 string strCon2 = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·' and ZQstatus in('����','Ͷ��','��ѡ')) ";

                    IList listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    IList listXL = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon2);
                    foreach (PSPDEV dev in listXL)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            bool fistflag=false;
                            bool lastflag=false;
                            foreach (PSPDEV pspdev in listMX)
                            {
                              if (dev.IName==pspdev.Name)
                              {
                                  fistflag=true;
                              }
                                if (dev.JName==pspdev.Name)
                                {
                                    lastflag=true;
                                }
                            }
                            if (lastflag&&fistflag)
                            {
                                if (dev.LineStatus == "�ȴ�")
                                {
                                    dev.LineStatus = "��ѡ";
                                    Services.BaseService.Update<PSPDEV>(dev);
                                }
                                eleclass el = new eleclass(dev.Name, dev.SUID, dev.Type, true);
                                JDlinecol.Add(el);
                            }
                          
                           
                        }
                    }

                 }
        }
        /// <summary>
        /// ���߷�
        /// <param name="GprogUID">
        /// Ϊ�����Ż���ĿID
        /// </param>
        /// <param name="Type">
        /// Ϊ�Ǹ�ʱ�ڵ������Ż���1Ϊ���塢2���ڡ�3���ڡ�4Զ�ڣ�
        /// </param>
        /// </summary>
        public int  brchcount = 0,buscount = 0, transcount = 0;
        public bool Check(string GprogUID, int Type, double ratedCapacity)
        {
           
            try
            {
                //ɾ��������·
                string con = "WHERE SvgUID='" + GprogUID + "'AND ProjectID='" + Itop.Client.MIS.ProgUID + "'AND Type='05' AND KName ='������·'";
                IList list0 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                for (int i = 0; i < list0.Count; i++)
                {
                    PSPDEV pspDev = (PSPDEV)list0[i];
                    Services.BaseService.Delete<PSPDEV>(pspDev);
                }
                string strBus = null;
                string strBranch = null;
                waitlinecoll.Clear();
                if (Type==1)
                {
                    brchcount = 0;buscount = 0; transcount = 0;
                    string strCon = "WHERE Type='01' AND SvgUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and Type in ('���վ','��Դ')) ";
                    string strCon2 = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·') ";
                    string strCon3 = "WHERE Projectid='" + Itop.Client.MIS.ProgUID + "'";
                double Rad_to_Deg = 180 / Math.PI;
                {
                    
                    IList listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    IList listXL = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon2);
                    strCon2 = " and Type='02' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�������ѹ��')";
                    strCon = strCon3 + strCon2;
                    IList listBYQ2 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "and Type='03' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�������ѹ��')";
                    strCon = strCon3 + strCon2;
                    IList listBYQ3 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    foreach (PSPDEV dev in listXL)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            if (dev.FirstNode ==0 || dev.LastNode ==0)
                            {
                                string temp = "���ӷ���ʧ��,";
                                temp += dev.Name;
                                temp += "û����ȷ����,����д�����";
                                System.Windows.Forms.MessageBox.Show(temp, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return false;
                            }
                            if (strBranch != null && (dev.LineStatus == "����" || dev.LineStatus == "��ѡ" || dev.LineStatus == "Ͷ��"))
                            {
                                strBranch += "\r\n";
                            }
                            if (dev.LineStatus == "����" || dev.LineStatus == "��ѡ" || dev.LineStatus == "Ͷ��")
                            {
                                if (dev.UnitFlag == "0")
                                {
                                    strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + "0" + " " + "0";

                                }
                                else
                                {
                                    strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + "0";

                                }

                                brchcount++;

                            }
                          
                            if (dev.LineStatus == "��ѡ")
                            {
                                linedaixuan ll = new linedaixuan(brchcount, dev.SUID, dev.Name);
                                waitlinecoll.Add(ll);
                            }
                        }
                    }
                    foreach (PSPDEV dev in listBYQ2)
                    {
                        bool flag = false;
                        if (dev.KSwitchStatus == "0")
                        {
                      
                            if (dev.FirstNode != 0 && dev.LastNode != 0)
                            {
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
                    }

                    foreach (PSPDEV dev in listBYQ3)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                         if (dev.FirstNode!=0&&dev.LastNode!=0&&dev.Flag!=0)
                         {
                             
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
                           strCon3 = "WHERE Projectid='" + Itop.Client.MIS.ProgUID + "'and Type = '04' AND IName = '" + dev.Name + "'and SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�����')";
                            IList listFDJ = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon3);
                            string strCon4 = "WHERE Projectid='" + Itop.Client.MIS.ProgUID + "' and Type = '12' AND IName = '" + dev.Name + "' and SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='����')";
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
                                strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + (dev.VoltV * Rad_to_Deg).ToString() + " " + ((inputP - outP)).ToString() + " " + ((inputQ - outQ)).ToString());
                                //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.LineGNDC).ToString() + " " + "0" + " " + dev.Name.ToString())
                            }
                            else
                            {
                                outP += dev.OutP / ratedCapacity;
                                outQ += dev.OutQ / ratedCapacity;
                                inputP += dev.InPutP / ratedCapacity;
                                inputQ += dev.InPutQ / ratedCapacity;
                                strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + (dev.VoltV * Rad_to_Deg).ToString() + " " + (inputP- outP).ToString() + " " + (inputQ- outQ).ToString());
                                //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + ((dev.LineGNDC) * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + dev.Name.ToString());
                            }
                            buscount++;
                        }
                    }
                }
                }

                if (Type == 2)
                {
                    brchcount = 0; buscount = 0; transcount = 0;
                    string strCon = "WHERE Type='01' AND SvgUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and Type in ('���վ','��Դ') and L1 in('����','����')) ";
                    string strCon2 = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·'and ZTstatus in('����','��ѡ')) ";
                    string strCon3 = "WHERE ProjectID='" + Itop.Client.MIS.ProgUID + "'";
                   
                    double Rad_to_Deg = 180 / Math.PI;
                    {
                        List<PSPDEV> remvelement = new List<PSPDEV>();         //ɾ�������õ���·
                        IList listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                        IList listXL = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon2);
                        strCon2 = "and Type='02' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�������ѹ��' and L1 in('����','����'))";
                        strCon = strCon3 + strCon2;
                        IList listBYQ2 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                        strCon2 = "and Type='03' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�������ѹ��'and L1 in('����','����'))";
                        strCon = strCon3 + strCon2;
                        IList listBYQ3 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                     
                        foreach (PSPDEV dev in listXL)
                        {
                            if (dev.KSwitchStatus == "0")
                            {
                                bool fistflag = false;
                                bool lastflag = false;
                                bool flag = false;                //�Ѳ�������׶ε���·��ӵ� Ҫɾ������·���� Ϊ�����ĸ�����˷�������
                                foreach (PSPDEV pspdev in listMX)
                                {
                                    if (dev.IName == pspdev.Name)
                                    {
                                        fistflag = true;
                                    }
                                    if (dev.JName == pspdev.Name)
                                    {
                                        lastflag = true;
                                    }
                                }
                                if (lastflag && fistflag)
                                {
                                   
                                    if (strBranch != null && (dev.LineStatus == "����" || dev.LineStatus == "��ѡ" || dev.LineStatus == "Ͷ��"))
                                    {
                                        strBranch += "\r\n";
                                    }
                                    if (dev.LineStatus == "����" || dev.LineStatus == "��ѡ" || dev.LineStatus == "Ͷ��")
                                    {
                                        flag = true;
                                        if (dev.UnitFlag == "0")
                                        {
                                            strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + "0" + " " + "0";

                                        }
                                        else
                                        {
                                            strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + "0";

                                        }

                                        brchcount++;

                                    }
                                   
                                    if (dev.LineStatus == "��ѡ")
                                    {
                                        linedaixuan ll = new linedaixuan(brchcount, dev.SUID, dev.Name);
                                        waitlinecoll.Add(ll);
                                    }
                                }

                                if (!flag)
                                {
                                    remvelement.Add(dev);
                                }
                            }
                        }
                        foreach (PSPDEV dev in remvelement)
                        {
                            listXL.Remove(dev);
                        }
                        foreach (PSPDEV dev in listBYQ2)
                        {
                            bool flag = false;
                            if (dev.KSwitchStatus == "0")
                            {

                                if (dev.FirstNode != 0 && dev.LastNode != 0)
                                {
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
                        }

                        foreach (PSPDEV dev in listBYQ3)
                        {
                            if (dev.KSwitchStatus == "0")
                            {
                                if (dev.FirstNode != 0 && dev.LastNode != 0 && dev.Flag != 0)
                                {

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
                            //Ϊ�Ҳ�ļ��߷�����
                            string temp = "���˷���ʧ��";
                            bool flag = false;
                            for (int i = 0; i < busname.Count; i++)
                            {
                                PSPDEV pspdev = new PSPDEV();
                                 foreach(PSPDEV dev in linedengdai)
                                {
                                    if (dev.IName==busname[i]||dev.JName==busname[i])
                                    {
                                       
                                        strBranch += "\r\n";
                                        if (dev.UnitFlag == "0")
                                        {
                                            strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + "0" + " " + "0";

                                        }
                                        else
                                        {
                                            strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + "0";

                                        }
                                        dev.LineStatus = "����";
                                        Services.BaseService.Update<PSPDEV>(dev);
                                        brchcount++;
                                        lineyiyou.Add(dev);
                                        pspdev = dev;
                                        flag = true;
                                        break;
                                    }
                                     
                               }
                               linedengdai.Remove(pspdev);

                               if (!flag)
                               {
                                   temp += "��" + busname[i];
                                   temp += "Ϊ�����Ľڵ㣡";
                                   System.Windows.Forms.MessageBox.Show(temp, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                   return false;
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
                                strCon3 = "WHERE Projectid='" + Itop.Client.MIS.ProgUID + "'and Type = '04' AND IName = '" + dev.Name + "'and SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�����')";
                                IList listFDJ = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon3);
                                string strCon4 = "WHERE Projectid='" + Itop.Client.MIS.ProgUID + "' and Type = '12' AND IName = '" + dev.Name + "' and SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='����')";
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
                                    strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + (dev.VoltV * Rad_to_Deg).ToString() + " " + ((inputP -outP)).ToString() + " " + ((inputQ -outQ)).ToString());
                                    //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.LineGNDC).ToString() + " " + "0" + " " + dev.Name.ToString())
                                }
                                else
                                {
                                    outP += dev.OutP / ratedCapacity;
                                    outQ += dev.OutQ / ratedCapacity;
                                    inputP += dev.InPutP / ratedCapacity;
                                    inputQ += dev.InPutQ / ratedCapacity;
                                    strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + (dev.VoltV * Rad_to_Deg).ToString() + " " + (inputP - outP).ToString() + " " + (inputQ -outQ).ToString());
                                    //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + ((dev.LineGNDC) * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + dev.Name.ToString());
                                }
                                buscount++;
                            }
                        }
                    }
                }
              if (Type == 3)
              {
                  brchcount = 0; buscount = 0; transcount = 0;
                  string strCon = "WHERE Type='01' AND SvgUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and Type in ('���վ','��Դ') and L1 in('����','����','����')) ";
                  string strCon2 = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·' and JQstatus in('����','Ͷ��','��ѡ')) ";
                  string strCon3 = "WHERE Projectid='" + Itop.Client.MIS.ProgUID + "'";

                  double Rad_to_Deg = 180 / Math.PI;
                  {

                      IList listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                      IList listXL = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon2);
                      strCon2 = "and Type='02' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�������ѹ��' and L1 in('����','����','����'))";
                      strCon = strCon3 + strCon2;
                      IList listBYQ2 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                      strCon2 = "and Type='03' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�������ѹ��'and L1 in('����','����','����'))";
                      strCon = strCon3 + strCon2;
                      IList listBYQ3 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                      List<PSPDEV> remvelement = new List<PSPDEV>();
                      foreach (PSPDEV dev in listXL)
                      {

                          if (dev.KSwitchStatus == "0")
                          {
                              bool flag = false;
                              bool fistflag = false;
                              bool lastflag = false;
                              foreach (PSPDEV pspdev in listMX)
                              {
                                  if (dev.IName == pspdev.Name)
                                  {
                                      fistflag = true;
                                  }
                                  if (dev.JName == pspdev.Name)
                                  {
                                      lastflag = true;
                                  }
                              }
                              if (lastflag && fistflag)
                              {
                                  if (strBranch != null && (dev.LineStatus == "����" || dev.LineStatus == "��ѡ" || dev.LineStatus == "Ͷ��"))
                                  {
                                      strBranch += "\r\n";
                                  }
                                  if (dev.LineStatus == "����" || dev.LineStatus == "��ѡ" || dev.LineStatus == "Ͷ��")
                                  {
                                      flag = true;
                                      if (dev.UnitFlag == "0")
                                      {
                                          strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + "0" + " " + "0";

                                      }
                                      else
                                      {
                                          strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + "0";

                                      }

                                      brchcount++;

                                  }
                                  PSP_GprogElevice pg = new PSP_GprogElevice();
                                  pg.GprogUID = GprogUID;
                                  pg.DeviceSUID = dev.SUID;
                                  pg = (PSP_GprogElevice)Services.BaseService.GetObject("SelectPSP_GprogEleviceByKey", pg);

                                  if (dev.LineStatus == "��ѡ" && pg.JQstatus == "��ѡ")
                                  {
                                      linedaixuan ll = new linedaixuan(brchcount, dev.SUID, dev.Name);
                                      waitlinecoll.Add(ll);
                                  }
                              }
                              if (!flag)
                              {
                                  remvelement.Add(dev);
                              }

                          }
                      }
                      foreach (PSPDEV dev in remvelement)
                      {
                          listXL.Remove(dev);
                      }
                      foreach (PSPDEV dev in listBYQ2)
                      {
                          bool flag = false;
                          if (dev.KSwitchStatus == "0")
                          {

                              if (dev.FirstNode != 0 && dev.LastNode != 0)
                              {
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
                      }

                      foreach (PSPDEV dev in listBYQ3)
                      {
                          if (dev.KSwitchStatus == "0")
                          {
                              if (dev.FirstNode != 0 && dev.LastNode != 0 && dev.Flag != 0)
                              {

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
                          //Ϊ�Ҳ�ļ��߷�����
                          string temp = "���˷���ʧ��";
                          bool flag = false;
                          for (int i = 0; i < busname.Count; i++)
                          {
                              PSPDEV pspdev = new PSPDEV();
                              foreach (PSPDEV dev in linedengdai)
                              {
                                  if (dev.IName == busname[i] || dev.JName == busname[i])
                                  {

                                      strBranch += "\r\n";
                                      if (dev.UnitFlag == "0")
                                      {
                                          strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + "0" + " " + "0";

                                      }
                                      else
                                      {
                                          strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + "0";

                                      }
                                      dev.LineStatus = "����";
                                      Services.BaseService.Update<PSPDEV>(dev);
                                      brchcount++;
                                      lineyiyou.Add(dev);
                                      pspdev = dev;
                                      flag = true;
                                      break;
                                  }

                              }
                              linedengdai.Remove(pspdev);
                              if (!flag)
                              {
                                  temp += "��" + busname[i];
                                  temp += "Ϊ�����Ľڵ㣡";
                                  System.Windows.Forms.MessageBox.Show(temp, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                  return false;
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
                              strCon3 = "WHERE Projectid='" + Itop.Client.MIS.ProgUID + "'and Type = '04' AND IName = '" + dev.Name + "'and SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�����')";
                              IList listFDJ = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon3);
                              string strCon4 = "WHERE Projectid='" + Itop.Client.MIS.ProgUID + "' and Type = '12' AND IName = '" + dev.Name + "' and SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='����')";
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
                                  strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + (dev.VoltV * Rad_to_Deg).ToString() + " " + ((inputP - outP)).ToString() + " " + ((inputQ - outQ)).ToString());
                                  //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.LineGNDC).ToString() + " " + "0" + " " + dev.Name.ToString())
                              }
                              else
                              {
                                  outP += dev.OutP / ratedCapacity;
                                  outQ += dev.OutQ / ratedCapacity;
                                  inputP += dev.InPutP / ratedCapacity;
                                  inputQ += dev.InPutQ / ratedCapacity;
                                  strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + (dev.VoltV * Rad_to_Deg).ToString() + " " + (inputP - outQ).ToString() + " " + (inputQ - outQ).ToString());
                                  //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + ((dev.LineGNDC) * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + dev.Name.ToString());
                              }
                              buscount++;
                          }
                      }
                  }
              }
                  if (Type == 4)
               {
              brchcount = 0;buscount = 0; transcount = 0;
                 string strCon = "WHERE Type='01' AND SvgUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and Type in ('���վ','��Դ') ) ";
                 string strCon2 = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·' and ZQstatus in('����','Ͷ��','��ѡ')) ";
                 string strCon3 = "WHERE ProjectID='" + Itop.Client.MIS.ProgUID + "'";
                 
                double Rad_to_Deg = 180 / Math.PI;
                {
                    
                    IList listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    IList listXL = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon2);
                    strCon2 = "and Type='02' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�������ѹ��')";
                    strCon = strCon3 + strCon2;
                    IList listBYQ2 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    strCon2 = "and Type='03' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�������ѹ��')";
                    strCon = strCon3 + strCon2;
                    IList listBYQ3 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    List<PSPDEV> remvelement = new List<PSPDEV>();
                    foreach (PSPDEV dev in listXL)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            bool fistflag=false;
                            bool lastflag=false;
                            bool flag = false;
                            foreach (PSPDEV pspdev in listMX)
                            {
                              if (dev.IName==pspdev.Name)
                              {
                                  fistflag=true;
                              }
                                if (dev.JName==pspdev.Name)
                                {
                                    lastflag=true;
                                }
                            }
                            if (lastflag&&fistflag)
                            {
                                if (strBranch != null && (dev.LineStatus == "����" || dev.LineStatus == "��ѡ" || dev.LineStatus == "Ͷ��"))
                            {
                                strBranch += "\r\n";
                            }
                            if (dev.LineStatus == "����" || dev.LineStatus == "��ѡ" || dev.LineStatus == "Ͷ��")
                            {
                                flag = true;
                                if (dev.UnitFlag == "0")
                                {
                                    strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + "0" + " " + "0";

                                }
                                else
                                {
                                    strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + "0";

                                }

                                brchcount++;

                            }
                            PSP_GprogElevice pg = new PSP_GprogElevice();
                            pg.GprogUID = GprogUID;
                            pg.DeviceSUID = dev.SUID;
                            pg = (PSP_GprogElevice)Services.BaseService.GetObject("SelectPSP_GprogEleviceByKey", pg);
                            if (dev.LineStatus == "��ѡ"&&pg.ZQstatus=="��ѡ")
                            {
                                linedaixuan ll = new linedaixuan(brchcount, dev.SUID, dev.Name);
                                waitlinecoll.Add(ll);
                            }
                          }
                          
                           if (!flag)
                           {
                               remvelement.Add(dev);
                           }
                        }
                    }
                    foreach(PSPDEV dev in remvelement)
                    {
                        listXL.Remove(dev);
                    }
                    foreach (PSPDEV dev in listBYQ2)
                    {
                        bool flag = false;
                        if (dev.KSwitchStatus == "0")
                        {

                            if (dev.FirstNode != 0 && dev.LastNode != 0)
                            {
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
                    }

                    foreach (PSPDEV dev in listBYQ3)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            if (dev.FirstNode != 0 && dev.LastNode != 0 && dev.Flag != 0)
                            {

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
                        //Ϊ�Ҳ�ļ��߷�����
                        string temp = "���˷���ʧ��";
                        bool flag = false;
                        for (int i = 0; i < busname.Count; i++)
                        {
                            PSPDEV pspdev = new PSPDEV();
                            foreach (PSPDEV dev in linedengdai)
                            {
                                if (dev.IName == busname[i] || dev.JName == busname[i])
                                {
                                   
                                    strBranch += "\r\n";
                                    if (dev.UnitFlag == "0")
                                    {
                                        strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + "0" + " " + "0";

                                    }
                                    else
                                    {
                                        strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + "0";

                                    }
                                    dev.LineStatus = "����";
                                    Services.BaseService.Update<PSPDEV>(dev);
                                    brchcount++;
                                    lineyiyou.Add(dev);
                                    pspdev = dev;
                                    flag = true;
                                    break;
                                }

                            }
                            linedengdai.Remove(pspdev);

                            if (!flag)
                            {
                                temp += "��" + busname[i];
                                temp += "Ϊ�����Ľڵ㣡";
                                System.Windows.Forms.MessageBox.Show(temp, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return false;
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
                            strCon3 = "WHERE Projectid='" + Itop.Client.MIS.ProgUID + "'and Type = '04' AND IName = '" + dev.Name + "'and SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�����')";
                            IList listFDJ = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon3);
                            string strCon4 = "WHERE Projectid='" + Itop.Client.MIS.ProgUID + "' and Type = '12' AND IName = '" + dev.Name + "' and SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='����')";
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
                                strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + (dev.VoltV * Rad_to_Deg).ToString() + " " + ((inputP - outP)).ToString() + " " + ((inputQ - outQ)).ToString());
                                //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.LineGNDC).ToString() + " " + "0" + " " + dev.Name.ToString())
                            }
                            else
                            {
                                outP += dev.OutP / ratedCapacity;
                                outQ += dev.OutQ / ratedCapacity;
                                inputP += dev.InPutP / ratedCapacity;
                                inputQ += dev.InPutQ / ratedCapacity;
                                strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + (dev.VoltV * Rad_to_Deg).ToString() + " " + (inputP - outP).ToString() + " " + (inputQ -outQ).ToString());
                                //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + ((dev.LineGNDC) * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + dev.Name.ToString());
                            }
                            buscount++;
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
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH1.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\DH1.txt");
                }
                if ((strBus.Contains("������") || strBus.Contains("�������")) || (strBranch.Contains("������") || strBranch.Contains("�������")))
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
        /// <summary>
        /// ���߷�
        /// <param name="GprogUID">
        /// Ϊ�����Ż���ĿID
        /// </param>
        /// <param name="Type">
        /// Ϊ�Ǹ�ʱ�ڵ������Ż���1Ϊ���塢2���ڡ�3���ڡ�4Զ�ڣ�
        /// </param>
        /// </summary>
        public void jianxiancheck(string GprogUID,int Type,double ratedCapacity)
        {
            FileStream dh;
            StreamReader readLine;
            char[] charSplit;
            string strLine;
            string[] array1;
            string output = null;
            string[] array2;

            string strLine2;

            char[] charSplit2 = new char[] { ' ' };
            FileStream op;
            StreamWriter str1;
            FileStream dh2;
            StreamReader readLine2;

            if (!Check(GprogUID,Type,ratedCapacity))
            {
                return ;
            }
            N1Test.NBcal zl = new N1Test.NBcal();
            zl.ZLpsp();


            double capability = 0, volt = 0, current = 0, standvolt = 0, Rad_to_Deg = 57.29577951;
            capability = ratedCapacity;
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH1.txt"))
            {
            }
            else
            {
                return ;
            }
            dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH1.txt", FileMode.Open);

            readLine = new StreamReader(dh, Encoding.Default);
            charSplit = new char[] { ' ' };
            strLine = readLine.ReadLine();
            output = null;
            while (!string.IsNullOrEmpty(strLine))
            {
                array1 = strLine.Split(charSplit);


                string[] dev = new string[2];
                dev.Initialize();
                int i = 0;
                PSPDEV CR = new PSPDEV();
                //CR.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;

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
                            if (str != "NaN")
                            {
                                dev[i++] = Convert.ToDouble(str).ToString();
                            }
                            else
                            {
                                dev[i++] = str;
                            }

                        }
                    }

                }
               string con=" WHERE Type='05' AND Name='"+dev[0]+"' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·') " ;
                //string con = ",PSP_GprogElevice WHERE PSPDEV.Name='"+dev[0]+"'AND PSPDEV.SUID = PSP_GprogElevice.DeviceSUID AND PSP_GprogElevice.GprogUID = '" + GprogUID + "'AND PSPDEV.Type='05'AND PSPDEV.KSwitchStatus = '0'";
                CR =(PSPDEV) Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
               
                if (CR != null && CR.ReferenceVolt != 0)
                {
                    volt = CR.ReferenceVolt;
                }
                else
                    volt = standvolt;
                current = capability / (Math.Sqrt(3) * volt);
                if (CR != null)
                {
                    if (CR.LineStatus == "��ѡ")
                    {
                        for (int n = 0; n < waitlinecoll.Count; n++)            //������·����
                        {
                            if (waitlinecoll[n].linename == CR.Name)
                            {
                                double linepij = System.Math.Abs(Convert.ToDouble(dev[1])) * capability;
                               // XmlNode el = tlVectorControl1.SVGDocument.SelectSingleNode("svg/polyline[@layer='" + tlVectorControl1.SVGDocument.CurrentLayer.ID + "' and @id='" + CR.EleID + "']");
                                double linevalue = 0;
                                if (CR.HuganTQ1 != 0)
                                {
                                    linevalue = CR.HuganTQ1;
                                }
                                else
                                    linevalue = 1;
                                waitlinecoll[n].Suid = CR.SUID;
                                waitlinecoll[n].linepij = linepij;
                                waitlinecoll[n].linevalue = linevalue;
                                waitlinecoll[n].linetouzilv = linepij / linevalue;
                            }
                        }
                    }
                    //output += "'" + CR.Name.ToString() + "," + (Convert.ToDouble(dev[3]) * capability).ToString() + "," + (Convert.ToDouble(dev[4]) * capability).ToString() + "," + (Convert.ToDouble(dev[5]) * capability).ToString() + "," + (Convert.ToDouble(dev[6]) * capability).ToString() + "," + (Convert.ToDouble(dev[7]) * current).ToString() + "," + (Convert.ToDouble(dev[8]) * Rad_to_Deg).ToString() + "," + dev[11] + "," + "\r\n";
                }
                strLine = readLine.ReadLine();
            }
            readLine.Close();
            waitlinecoll.Sort();         //���д�С����  Ȼ�����ֱ�������ļ���


            bool lineflag = true;      //ֻҪ��һ�����ϸ����Ϊ���ϸ�
            bool jielieflag = true;    //�ж���û����·����


            for (int i = 0; i < waitlinecoll.Count; i++)
            {
                N1Test.NBcal kk = new N1Test.NBcal();
                kk.Show_KmRelia(waitlinecoll[i].linenum);
                //kk.Show_ZLKMPSP(waitlinecoll[i].linenum);
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ZL.txt"))
                {
                }
                else
                {
                    return ;
                }
                dh2 = new FileStream(System.Windows.Forms.Application.StartupPath + "\\ZL.txt", FileMode.Open);

                readLine2 = new StreamReader(dh2, Encoding.Default);
                charSplit2 = new char[] { ' ' };
                strLine2 = readLine2.ReadLine();
                output = null;
                while (!string.IsNullOrEmpty(strLine2))
                {
                    array2 = strLine2.Split(charSplit2);


                    string[] dev = new string[2 * brchcount + 1];
                    dev.Initialize();

                    PSPDEV CR = new PSPDEV();
                   // CR.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    int m = 0;
                    foreach (string str in array2)
                    {

                        if (str != "")
                        {

                            dev[m++] = str.ToString();

                        }
                    }
                    //���н��Ѻ͸����ж�



                    if (dev[1] != "-1")
                    {
                        for (int j = 0; j < brchcount; j++)
                        {

                            double pij = System.Math.Abs(Convert.ToDouble(dev[j * 2 + 2])) * capability;

                            string con = " WHERE Type='05' AND Name='" + dev[0] + "' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·') ";
                            //string con = ",PSP_GprogElevice WHERE PSPDEV.Name='" + dev[j * 2 + 1] + "' PSPDEV.SUID = PSP_GprogElevice.DeviceSUID AND PSP_GprogElevice.GprogUID = '" + GprogUID + "'AND PSPDEV.type='05'AND PSPDEV.KSwitchStatus = '0'";
                            PSPDEV pspline = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
                            double voltR = pspline.RateVolt;
                            double Ichange = (double)pspline.Burthen;
                            //double linXij = System.Math.Sqrt(3) * voltR * Ichange;
                            //double Ichange = (double)listware.WireChange;
                            double linXij = System.Math.Sqrt(3) * voltR * Ichange / 1000;
                            if (pij >= linXij)
                            {
                                lineflag = false;
                                //lineclass _line = new lineclass(n, j);
                                //Overlinp.Add(_line);
                                // OverPhege[n] = j;
                            }

                        }

                    }

                    else
                    {
                        jielieflag = false;
                    }

                    if (jielieflag && lineflag)
                    {
                       readLine2.Close();           //�رն�ȡ���ļ�


                        string con = " WHERE Type='05' AND Name='" + dev[0] + "' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·') "; 
                       // string con = ",PSP_GprogElevice WHERE PSPDEV.Name='" + dev[0] + "' PSPDEV.SUID = PSP_GprogElevice.DeviceSUID AND PSP_GprogElevice.GprogUID = '" + GprogUID + "'AND PSPDEV.type='05'AND PSPDEV.KSwitchStatus = '0'";
                        PSPDEV pspline = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
                        pspline.LineStatus = "�ȴ�";
                        Services.BaseService.Update<PSPDEV>(pspline);
                        jianxiancheck(GprogUID,Type,ratedCapacity);       //������һ�ֵļ��ߴ���
                        return ;
                    }
                    /*
                          else
                                                 break;*/
                    else
                    strLine2 = readLine2.ReadLine();
                }
                readLine2.Close();

            }
        }
       public List<PSPDEV> lineyiyou = new List<PSPDEV>();             //����ѡ����·�ϸ�ļ��뵽������·��
       public List<PSPDEV> linedengdai = new List<PSPDEV>();          //��¼���д�ѡ����·��һ�������Ժ�ͻᷢ���仯��
       public List<linedaixuan> waitlineindex = new List<linedaixuan>();   //��¼��ѡ��·�� Ͷ��ָ��Ĵ�С�������Ҿ�������
       public  List<PSPDEV> ercilinedengdai = new List<PSPDEV>();      //��¼���߷��ұߵ��㷨�� ��ĳ����·�Ͽ����ֹ����� ���ǾͿ��Խ�����·��״̬��Ϊ�ȴ� ���ҷ���������� �����ڳ������ʱ�ٸ�Ϊ����
       public List<linedaixuan> fuheline = new List<linedaixuan>();   //��¼���ָ��ɵ���· Ϊ����Ͷ��ָ���� 
       /// <summary>
       ///�����߷�
       /// <param name="GprogUID">
       /// Ϊ�����Ż���ĿID
       /// </param>
       /// <param name="Type">
       /// Ϊ�Ǹ�ʱ�ڵ������Ż���1Ϊ���塢2���ڡ�3���ڡ�4Զ�ڣ�
       /// </param>
       /// </summary>
        public void addlinecheck(string GprogUID,int Type,double ratacapality)
       {
           FileStream dh;
           StreamReader readLine;
           char[] charSplit;
           string strLine;
           string[] array1;

           char[] charSplit2 = new char[] { ' ' };
           string strCon = null;
           linedengdai.Clear();
           if (Type==2)
           {
            strCon = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·'and ZTstatus ='��ѡ') ";
           }
           if (Type == 3)
           {
               strCon = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·'and JQstatus ='��ѡ') ";
           }
           if (Type ==4)
           {
               strCon = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·'and ZQstatus ='��ѡ') ";
           }
           PSPDEV pspdev = new PSPDEV();
           IList list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
           for (int i = 0; i < list1.Count; i++)
           {
               bool flag=false;
               pspdev = list1[i] as PSPDEV;
               foreach (eleclass el in JDlinecol)
               {
                   
                   if (el.suid==pspdev.SUID)
                   {
                       flag = true;
                   }
               }
               if (flag)
               {
                   pspdev.LineStatus = "�ȴ�";
                   linedengdai.Add(pspdev);
                   Services.BaseService.Update<PSPDEV>(pspdev);
               }
              

           }
           lineyiyou.Clear();            //�����һ�εļ�¼  Ϊ��һ��ʱ�ڵ�Ӧ����׼��


          // linedengdai.Clear();          //�����һ�μ�¼��ѡ��· Ϊ��һ��ʱ�ڵ�Ӧ����׼��



           AA:
           if (!fuhecheck(GprogUID,Type,ratacapality))
           {
               waitlineindex.Clear();
               for (int j = 0; j < linedengdai.Count; j++)
               {
                   if (!Checkadd(GprogUID,Type,ratacapality,linedengdai[j].SUID))
                       return;
                   N1Test.NBcal zl = new N1Test.NBcal();
                   zl.ZLpsp();



                   double yinzi = 0, capability = 0, volt = 0, current = 0, standvolt = 0, Rad_to_Deg = 57.29577951;
                   capability = ratacapality;
                   if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH1.txt"))
                   {
                   }
                   else
                   {
                       return;
                   }
                   dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH1.txt", FileMode.Open);

                   readLine = new StreamReader(dh, Encoding.Default);
                   charSplit = new char[] { ' ' };
                   strLine = readLine.ReadLine();
                   double sumpij = 0.0;
                   bool lineflag = true;
                   while (!string.IsNullOrEmpty(strLine))
                   {
                       array1 = strLine.Split(charSplit);


                       string[] dev = new string[2];
                       dev.Initialize();
                       int i = 0;
                       PSPDEV CR = new PSPDEV();
                       //CR.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;

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
                                   if (str != "NaN")
                                   {
                                       dev[i++] = Convert.ToDouble(str).ToString();
                                   }
                                   else
                                   {
                                       dev[i++] = str;
                                   }

                               }
                           }

                       }
                       string con = " WHERE Type='05' AND Name='" + dev[0] + "'AND KSwitchStatus = '0' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·') ";
                      // string con = ",PSP_GprogElevice WHERE PSPDEV.Name='" + dev[0] + "' PSPDEV.SUID = PSP_GprogElevice.DeviceSUID AND PSP_GprogElevice.GprogUID = '" + GprogUID + "'AND PSPDEV.type='05'AND PSPDEV.KSwitchStatus = '0'";
                       CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
                       if (CR != null && CR.ReferenceVolt != 0)
                       {
                           volt = CR.ReferenceVolt;
                       }
                       else
                           volt = standvolt;
                       current = capability / (Math.Sqrt(3) * volt);
                       if (CR != null && !CR.Name.Contains("������·"))
                       {


                           double linepij = System.Math.Abs(Convert.ToDouble(dev[1])) * capability;
                           double voltR = CR.RateVolt;
                           double Ichange = (double)CR.Burthen;
                           double linXij = System.Math.Sqrt(3) * voltR * Ichange / 1000;
                           
                           if (linepij >= linXij)
                           {
                               lineflag = false;

                           }
                           for (int k = 0; k < fuheline.Count; k++)
                           {
                               if (CR.SUID == fuheline[k].Suid)
                               {
                                   sumpij += System.Math.Abs(fuheline[k].linepij - linepij);
                               }
                           }
                         
                       }
                       strLine = readLine.ReadLine();
                   }
                   readLine.Close();
                  //if (lineflag)              //���û�г��ֹ��������� ��ֹͣ���м���
                  // {
                  // //    PSPDEV pspb = (PSPDEV)linedengdai[j];
                  // //    pspb.LineStatus = "����";
                  // //    Services.BaseService.Update<PSPDEV>(pspb);
                  // //    lineyiyou.Add(pspb);
                  // //    for (int i = 0; i < linedengdai.Count; i++)
                  // //    {
                  // //        if (linedengdai[i].SUID == pspb.SUID)
                  // //        {
                  // //            linedengdai.RemoveAt(j);
                  // //        }
                  // //    }
                  //    return;
                  //  }
                   double linevalue = 0;
                   if (linedengdai[j].HuganTQ1!=0)
                   {
                       linevalue =linedengdai[j].HuganTQ1;
                   }
                   else
                       linevalue = 1;
                   linedaixuan linedai = new linedaixuan(linedengdai[j].Number, linedengdai[j].SUID, linedengdai[j].Name);
                   linedai.linepij = sumpij;
                   linedai.linevalue = linevalue;
                   linedai.linetouzilv = sumpij / linevalue;

                   waitlineindex.Add(linedai);
                   //}

               }
               waitlineindex.Sort();
               //�ڴ˴����ָ��������· ������·��״̬��Ϊ ���в��������еļ��������¼ �ڵȴ��ļ����ｫ����·ȥ�� ���½�����һ�ֵĲ���
               PSPDEV pspbianhua = new PSPDEV();
               if (waitlineindex.Count > 0)
               {
                   pspbianhua.SUID = waitlineindex[waitlineindex.Count - 1].Suid;
               }
               else
               {
                   //MessageBox.Show("û�г��ֹ����ɵ���·������鿴һ����·�����Ƿ��趨��ȷ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   return;
               }

               pspbianhua = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", pspbianhua);
               pspbianhua.LineStatus = "����";
               Services.BaseService.Update<PSPDEV>(pspbianhua);
               lineyiyou.Add(pspbianhua);
               for (int i = 0; i < linedengdai.Count; i++)
               {
                   if (linedengdai[i].SUID == pspbianhua.SUID)
                   {
                       linedengdai.RemoveAt(i);
                   }
               }
               
               goto AA;
           }
       }
       /// <summary>
       ///���ɼ�� �ҵ����ֹ����ɵ���·��
       /// <param name="GprogUID">
       /// Ϊ�����Ż���ĿID
       /// </param>
       /// <param name="Type">
       /// Ϊ�Ǹ�ʱ�ڵ������Ż���1Ϊ���塢2���ڡ�3���ڡ�4Զ�ڣ�
       /// </param>
       /// </summary>
        public bool fuhecheck(string GprogUID,int Type,double ratacapality)
        {

            FileStream dh;
            StreamReader readLine;
            char[] charSplit;
            string strLine;
            string[] array1;
            char[] charSplit2 = new char[] { ' ' };
                    
            fuheline.Clear();            //�����һ�εļ�¼  Ϊ��һ��ʱ�ڵ�Ӧ����׼��


            // linedengdai.Clear();          //�����һ�μ�¼��ѡ��· Ϊ��һ��ʱ�ڵ�Ӧ����׼��


            if (!Checkfuhe(GprogUID,Type,ratacapality))
                return true;
            N1Test.NBcal zl = new N1Test.NBcal();
            //zl.Show_KmRelia(1);
            zl.ZLpsp();



            double capability = 0, volt = 0, current = 0, standvolt = 0;
            capability = ratacapality;
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH1.txt"))
            {
            }
            else
            {
                return true;
            }
            dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH1.txt", FileMode.Open);

            readLine = new StreamReader(dh, Encoding.Default);
            charSplit = new char[] { ' ' };
            strLine = readLine.ReadLine();
            //output = null;
            //double sumpij = 0.0;
            bool lineflag = true;
            while (!string.IsNullOrEmpty(strLine))
            {
                array1 = strLine.Split(charSplit);


                string[] dev = new string[2];
                dev.Initialize();
                int i = 0;
                PSPDEV CR = new PSPDEV();
                //CR.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;

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
                            if (str != "NaN")
                            {
                                dev[i++] = Convert.ToDouble(str).ToString();
                            }
                            else
                            {
                                dev[i++] = str;
                            }

                        }
                    }

                }
                string con = " WHERE Type='05' AND Name='" + dev[0] + "'AND KSwitchStatus = '0' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·') ";
               // string con = ",PSP_GprogElevice WHERE PSPDEV.Name='" + dev[0] + "' PSPDEV.SUID = PSP_GprogElevice.DeviceSUID AND PSP_GprogElevice.GprogUID = '" + GprogUID + "'AND PSPDEV.type='05'AND PSPDEV.KSwitchStatus = '0'";
                CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
               
                if (CR != null && CR.ReferenceVolt != 0)
                {
                    volt = CR.ReferenceVolt;
                }
                else
                    volt = standvolt;
                current = capability / (Math.Sqrt(3) * volt);
                if (CR != null && !CR.Name.Contains("������·"))
                {


                    double linepij = System.Math.Abs(Convert.ToDouble(dev[1])) * ratacapality;
                    double voltR = CR.RateVolt;
                    double Ichange = (double)CR.Burthen;
                   
                    double linXij = System.Math.Sqrt(3) * voltR * Ichange / 1000;
                    if (linepij >= linXij)
                    {
                        lineflag = false;
                        linedaixuan ld = new linedaixuan(CR.Number, CR.SUID, CR.Name);
                        ld.linepij = linepij;
                        fuheline.Add(ld);

                    }

                    //  
                    //    //output += "'" + CR.Name.ToString() + "," + (Convert.ToDouble(dev[3]) * capability).ToString() + "," + (Convert.ToDouble(dev[4]) * capability).ToString() + "," + (Convert.ToDouble(dev[5]) * capability).ToString() + "," + (Convert.ToDouble(dev[6]) * capability).ToString() + "," + (Convert.ToDouble(dev[7]) * current).ToString() + "," + (Convert.ToDouble(dev[8]) * Rad_to_Deg).ToString() + "," + dev[11] + "," + "\r\n";
                }
                strLine = readLine.ReadLine();
            }
            readLine.Close();
            return lineflag;
        }
         /// <summary>
       ///�γ� bus��branch�ļ�
       /// <param name="GprogUID">
       /// Ϊ�����Ż���ĿID
       /// </param>
       /// <param name="Type">
       /// Ϊ�Ǹ�ʱ�ڵ������Ż���1Ϊ���塢2���ڡ�3���ڡ�4Զ�ڣ�
       /// </param>
       /// </summary>
        public bool Checkfuhe(string GprogUID,int Type,double ratacapality)
        {

            double current = 0;
            current = ratacapality;      //����ݶ���Ϊ100
            //����ȥ��֮ǰ��������·


            string con = "WHERE SvgUID='" + GprogUID + "'AND ProjectID='" + Itop.Client.MIS.ProgUID + "'AND Type='05' AND KName ='������·'";
            IList list0 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            for (int i = 0; i < list0.Count; i++)
            {
               PSPDEV pspDev = (PSPDEV)list0[i];
                Services.BaseService.Delete<PSPDEV>(pspDev);
            }
            brchcount = 0; buscount = 0; transcount = 0;
            string strCon = null; string strCon2 = null; string strCon3 = null, strConbyq = null; IList listBYQ2 = null, listBYQ3 = null;
            string strBus = null;
            string strBranch = null;
            strCon3 = "WHERE ProjectID='" + Itop.Client.MIS.ProgUID + "'";
            if (Type == 2)
            {
                strCon = "WHERE Type='01' AND SvgUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and Type in ('���վ','��Դ') and L1 in('����','����')) ";
                strCon2 = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·'and ZTstatus in('����','��ѡ')) ";

                strConbyq = strCon3 + "and Type='02' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�������ѹ��'and L1 in('����','����'))";
               listBYQ2 = Services.BaseService.GetList("SelectPSPDEVByCondition", strConbyq);

                strConbyq = strCon3 + "and Type='03' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�������ѹ��'and L1 in('����','����'))";
                listBYQ3 = Services.BaseService.GetList("SelectPSPDEVByCondition", strConbyq);

            }
            if (Type == 3)
            {
                strCon = "WHERE Type='01' AND SvgUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and Type in ('���վ','��Դ') and L1 in('����','����','����')) ";
                strCon2 = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·'and JQstatus in('����','��ѡ','Ͷ��')) ";
                //strCon3 = "WHERE ProjectID='" + Itop.Client.MIS.ProgUID + "'";
                strConbyq = strCon3 + "and Type='02' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�������ѹ��'and L1 in('����','����','����'))";
                listBYQ2 = Services.BaseService.GetList("SelectPSPDEVByCondition", strConbyq);

                strConbyq = strCon3 + "and Type='03' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�������ѹ��'and L1 in('����','����','����'))";
                listBYQ3 = Services.BaseService.GetList("SelectPSPDEVByCondition", strConbyq);
            }
            if (Type == 4)
            {
                strCon = "WHERE Type='01' AND SvgUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and Type in ('���վ','��Դ')) ";
                strCon2 = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·'and ZQstatus in('����','��ѡ','Ͷ��')) ";
                // strCon3 = "WHERE ProjectID='" + Itop.Client.MIS.ProgUID + "'";
                strConbyq = strCon3 + "and Type='02' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�������ѹ��')";
                listBYQ2 = Services.BaseService.GetList("SelectPSPDEVByCondition", strConbyq);

                strConbyq = strCon3 + "and Type='03' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�������ѹ��')";
                listBYQ3 = Services.BaseService.GetList("SelectPSPDEVByCondition", strConbyq);
            }

            double Rad_to_Deg = 180 / Math.PI;
            {

                IList listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                IList listXL = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon2);
               
                //��������·������
                List<PSPDEV> RemovElem = new List<PSPDEV>();
                foreach (PSPDEV dev in listXL)
                {
                    if (dev.KSwitchStatus == "0")
                    {
                        bool fistflag = false;
                        bool lastflag = false;
                        foreach (PSPDEV pspdev in listMX)
                        {
                            if (dev.IName == pspdev.Name)
                            {
                                fistflag = true;
                            }
                            if (dev.JName == pspdev.Name)
                            {
                                lastflag = true;
                            }
                        }
                        //��ĳ��ʱ�ڵ���·����

                        if (lastflag && fistflag)
                        {
                            dev.SvgUID = GprogUID;
                            dev.EleID = Type.ToString();
                            Services.BaseService.Update<PSPDEV>(dev);
                            if (dev.LineStatus == "�ȴ�")
                                continue;
                            brchcount++;

                        }
                        else
                            RemovElem.Add(dev);
                    }
                }
                foreach(PSPDEV dev in RemovElem)
                {
                    listXL.Remove(dev);
                }
                List<string> busname = new List<string>();
                foreach (PSPDEV dev in listMX)
                {
                    bool flag = false;
                    foreach (PSPDEV devline in listXL)
                    {
                        if ((dev.Number == devline.LastNode || dev.Number == devline.FirstNode) && (devline.LineStatus == "����" || devline.LineStatus == "��ѡ"))
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
                for (int i = 0; i < busname.Count; i++)
                {
                    PSPDEV psp = new PSPDEV();
                    con = "WHERE Name='"+busname[i]+"'AND Type='01'AND ProjectID='" + Itop.Client.MIS.ProgUID + "'";
                    //psp.Name = busname[i];
                    //psp.Type = "01";
                    // psp.SvgUID = GprogUID;
                    psp = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
                    if (psp.Number != 1)
                    {

                        PSPDEV pspline = new PSPDEV();
                        pspline.SvgUID = GprogUID;
                        pspline.EleID = Type.ToString();
                        pspline.FirstNode = psp.Number;
                        pspline.LastNode = psp.Number - 1;
                        pspline.Type = "05";
                        pspline.Lable = "֧·";
                        pspline.Name = "������·" + i;
                        pspline.KName = "������·";
                        pspline.LineStatus = "����";
                        pspline.Number = brchcount + i + 1;
                        pspline.SUID = Guid.NewGuid().ToString();
                        pspline.LineLength = 100;
                        pspline.RateVolt = psp.RateVolt;
                        pspline.ReferenceVolt = psp.ReferenceVolt;
                        pspline.LineR = 9999;
                        pspline.LineTQ = 9999;
                        pspline.KSwitchStatus = "0";
                        pspline.UnitFlag = "1";
                        pspline.ProjectID = Itop.Client.MIS.ProgUID;
                        Services.BaseService.Create<PSPDEV>(pspline);
                    }
                    else
                    {
                        PSPDEV pspline = new PSPDEV();
                        pspline.SvgUID = GprogUID;
                        pspline.EleID = Type.ToString();
                        pspline.FirstNode = psp.Number;
                        pspline.LastNode = psp.Number + 1;
                        pspline.Type = "05";
                        pspline.Lable = "֧·";
                        pspline.Name = "������·" + i;
                        pspline.KName = "������·";
                        pspline.LineStatus = "����";
                        pspline.Number = brchcount + i + 1;
                        pspline.SUID = Guid.NewGuid().ToString();
                        pspline.LineLength = 100;
                        pspline.RateVolt = psp.RateVolt;
                        pspline.ReferenceVolt = psp.ReferenceVolt;
                        pspline.LineR = 9999;
                        pspline.LineTQ = 9999;
                        pspline.KSwitchStatus = "0";
                        pspline.UnitFlag = "1";
                        pspline.ProjectID = Itop.Client.MIS.ProgUID;
                        Services.BaseService.Create<PSPDEV>(pspline);
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
                        strCon3 = "WHERE Projectid='" + Itop.Client.MIS.ProgUID + "'and Type = '04' AND IName = '" + dev.Name + "'and SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�����')";
                        IList listFDJ = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon3);
                        string strCon4 = "WHERE Projectid='" + Itop.Client.MIS.ProgUID + "' and Type = '12' AND IName = '" + dev.Name + "' and SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='����')";
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
                                outP += devFDJ.OutP / ratacapality;
                                outQ += devFDJ.OutQ / ratacapality;
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
                                inputP += devFH.InPutP / ratacapality;
                                inputQ += devFH.InPutQ / ratacapality;
                            }
                        }
                        if (dev.UnitFlag == "0")
                        {
                            outP += dev.OutP;
                            outQ += dev.OutQ;
                            inputP += dev.InPutP;
                            inputQ += dev.InPutQ;
                            strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + (dev.VoltV * Rad_to_Deg).ToString() + " " + ((inputP - outP)).ToString() + " " + ((inputQ - outQ)).ToString());
                            //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.LineGNDC).ToString() + " " + "0" + " " + dev.Name.ToString())
                        }
                        else
                        {
                            outP += dev.OutP / ratacapality;
                            outQ += dev.OutQ / ratacapality;
                            inputP += dev.InPutP / ratacapality;
                            inputQ += dev.InPutQ / ratacapality;
                            strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + (dev.VoltV * Rad_to_Deg).ToString() + " " + (inputP - outP).ToString() + " " + (inputQ - outQ).ToString());
                            //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + ((dev.LineGNDC) * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + dev.Name.ToString());
                        }
                        buscount++;
                    }
                }
                 con="WHERE Type='05'and SvgUID='"+GprogUID+"'and EleID='"+Type+"'and LineStatus ='����'and ProjectID='"+Itop.Client.MIS.ProgUID+"'";
                  listXL=Services.BaseService.GetList("SelectPSPDEVByCondition",con);
                    foreach (PSPDEV dev in listXL)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            bool fistflag = false;
                            bool lastflag = false;
                            foreach (PSPDEV pspdev in listMX)
                            {
                                if (dev.FirstNode == pspdev.Number)
                                {
                                    fistflag = true;
                                }
                                if (dev.LastNode == pspdev.Number)
                                {
                                    lastflag = true;
                                }
                            }
                            if (lastflag && fistflag)
                            {
                                if (strBranch != null && (dev.LineStatus == "����" || dev.LineStatus == "��ѡ"))
                                {
                                    strBranch += "\r\n";
                                }
                                if (dev.LineStatus == "����" || dev.LineStatus == "��ѡ")
                                {
                                    if (dev.UnitFlag == "0")
                                    {
                                        strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + "0" + " " + "0";

                                    }
                                    else
                                    {
                                        strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + (dev.LineR * ratacapality / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratacapality / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratacapality * 1000000)).ToString() + " " + "0" + " " + "0";

                                    }

                                    brchcount++;

                                }
                            }
                        }
                    }
                    foreach (PSPDEV dev in listBYQ2)
                    {
                        bool flag = false;
                        if (dev.KSwitchStatus == "0")
                        {

                            if (dev.FirstNode != 0 && dev.LastNode != 0)
                            {
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
                                    strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.LineR * ratacapality / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratacapality / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratacapality * 1000000)).ToString() + " " + dev.K.ToString() + " " + dev.G.ToString();

                                }
                                transcount++;
                            }

                        }
                    }

                    foreach (PSPDEV dev in listBYQ3)
                    {
                        if (dev.KSwitchStatus == "0")
                        {
                            if (dev.FirstNode != 0 && dev.LastNode != 0 && dev.Flag != 0)
                            {

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
                                    strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.HuganTQ1 * ratacapality / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.HuganTQ4 * ratacapality / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + "0" + " " + dev.K.ToString() + " " + "0";
                                    if (strBranch != null)
                                    {
                                        strBranch += "\r\n";
                                    }
                                    strBranch += dev.LastNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.HuganTQ2 * ratacapality / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.HuganTQ5 * ratacapality / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + "0" + " " + dev.StandardCurrent.ToString() + " " + "0";
                                    if (strBranch != null)
                                    {
                                        strBranch += "\r\n";
                                    }
                                    strBranch += dev.FirstNode.ToString() + " " + dev.Flag.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.HuganTQ3 * ratacapality / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.ZeroTQ * ratacapality / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + "0" + " " + dev.BigP.ToString() + " " + "0";
                                }
                                transcount += 3;
                            }

                        }
                    } 
            }
            //outParam1 += (volt + " " + current + " " + "-2" + " " + "-2" + " " + "-2" + " " + "-2" + " " + "-2" + " " + "-2" + ";" + "\r\n");
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\branch.txt"))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\branch.txt");
            }
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\bus.txt"))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\bus.txt");
            }
            //if (File.Exists("c:\\L9.txt"))
            //{
            //    File.Delete("c:\\L9.txt");
            //}
            FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\branch.txt"), FileMode.OpenOrCreate);
            StreamWriter str1 = new StreamWriter(VK, Encoding.Default);
            str1.Write(strBranch);
            str1.Close();
            FileStream L = new FileStream((System.Windows.Forms.Application.StartupPath + "\\bus.txt"), FileMode.OpenOrCreate);
            StreamWriter str2 = new StreamWriter(L, Encoding.Default);
            str2.Write(strBus);
            str2.Close();
            return true;
        }
            /// <summary>
       ///�Ҳ���߷� �������һ����·ȷ�������ֹ�����
       /// <param name="GprogUID">
       /// Ϊ�����Ż���ĿID
       /// </param>
       /// <param name="Type">
       /// Ϊ�Ǹ�ʱ�ڵ������Ż���1Ϊ���塢2���ڡ�3���ڡ�4Զ�ڣ�
       /// </param>
       /// </summary>
        public void addrightcheck(string GprogUID,int Type,double ratacapality)
        {
            FileStream dh;
            StreamReader readLine;
            char[] charSplit;
            string strLine;
            string[] array1;
            string[] array2;

            string strLine2;

            char[] charSplit2 = new char[] { ' ' };
            FileStream dh2;
            StreamReader readLine2;
            string strCon2 = null;
            //List<PSPDEV> linedengdai = new List<PSPDEV>();          //��¼���д�ѡ����·��һ�������Ժ�ͻᷢ���仯��
            if (!Check(GprogUID,Type,ratacapality))
            {
                return;
            }
            if (Type == 2)
            {
                //strCon = "WHERE Type='01' AND SvgUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and Type in ('���վ','��Դ') and L1 in('����','����')) ";
                strCon2 = "WHERE Type='05'AND LineStatus='����' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·'and ZTstatus in('����','��ѡ')) ";


            }
            if (Type == 3)
            {
               // strCon = "WHERE Type='01' AND SvgUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and Type in ('���վ','��Դ') and L1 in('����','����','����')) ";
                strCon2 = "WHERE Type='05'AND LineStatus='����' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·'and JQstatus in('����','��ѡ','Ͷ��')) ";
                //strCon3 = "WHERE ProjectID='" + Itop.Client.MIS.ProgUID + "'";

            }
            if (Type == 4)
            {
                //strCon = "WHERE Type='01' AND SvgUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and Type in ('���վ','��Դ')) ";
                strCon2 = "WHERE Type='05'AND LineStatus='����' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·'and ZQstatus in('����','��ѡ','Ͷ��')) ";
                // strCon3 = "WHERE ProjectID='" + Itop.Client.MIS.ProgUID + "'";

            }
            IList list1 = Services.BaseService.GetList("SelectPSPDEVByCondition",strCon2);
             double yinzi = 0, capability = 0, volt = 0, current = 0, standvolt = 0, Rad_to_Deg = 57.29577951;        
            capability = ratacapality;
            ercilinedengdai.Clear();//���֮ǰ�ļ�¼
            waitlineindex.Clear();

            for (int i = 0; i < list1.Count; i++)
            {
                N1Test.NBcal kk = new N1Test.NBcal();
                kk.Show_KmRelia(i + 1);
                //kk.Show_ZLKMPSP(i+1);
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ZL.txt"))
                {
                }
                else
                {
                    return;
                }
                dh2 = new FileStream(System.Windows.Forms.Application.StartupPath + "\\ZL.txt", FileMode.Open);

                readLine2 = new StreamReader(dh2, Encoding.Default);
                charSplit2 = new char[] { ' ' };
                strLine2 = readLine2.ReadLine();
                
                bool lineflag = true;
                string tiaochuname = "";
                bool jielieflag = true;
                while (!string.IsNullOrEmpty(strLine2))
                {
                    array2 = strLine2.Split(charSplit2);


                    string[] dev = new string[2 * brchcount + 1];
                    dev.Initialize();

                    PSPDEV CR = new PSPDEV();
                   // CR.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    int m = 0;
                    foreach (string str in array2)
                    {

                        if (str != "")
                        {

                            dev[m++] = str.ToString();

                        }
                    }
                    //���н��Ѻ͸����ж�



                    if (dev[1] != "-1")
                    {
                        for (int j = 0; j < brchcount; j++)
                        {

                            double pij = System.Math.Abs(Convert.ToDouble(dev[j * 2 + 2])) * ratacapality;

                            string con = " WHERE Type='05' AND Name='" + dev[0] + "'AND KSwitchStatus = '0' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·') ";
                            //string con = ",PSP_GprogElevice WHERE PSPDEV.Name='" + dev[j * 2 + 1] + "' PSPDEV.SUID = PSP_GprogElevice.DeviceSUID AND PSP_GprogElevice.GprogUID = '" + GprogUID + "'AND PSPDEV.type='05'AND PSPDEV.KSwitchStatus = '0'";

                            PSPDEV pspline = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
                            if (pspline != null && !pspline.Name.Contains("������·"))
                            {
                                double voltR = pspline.RateVolt;
                                double Ichange = (double)pspline.Burthen;

                                double linXij = System.Math.Sqrt(3) * voltR * Ichange / 1000;
                                if (pij >= linXij)
                                {
                                    lineflag = false;
                                    tiaochuname = dev[0];
                                    //lineclass _line = new lineclass(n, j);
                                    //Overlinp.Add(_line);
                                    // OverPhege[n] = j;
                                }
                            }


                        }

                    }

                    else
                    {
                        jielieflag = false;
                    }


                    strLine2 = readLine2.ReadLine();
                }
                readLine2.Close();
                if (!lineflag)
                {
                    PSPDEV psperci = new PSPDEV();
                    string con = " WHERE Type='05' AND Name='" + tiaochuname + "'AND KSwitchStatus = '0' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·') ";                          
                    psperci= (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con );
                    psperci.LineStatus = "�ȴ�";
                    Services.BaseService.Update<PSPDEV>(psperci);
                    ercilinedengdai.Add(psperci);
                    break;
                }
                else                  //û�г��ֹ�����
                    continue;
            }
            //����ӵ� �����¼���εȴ�����·û����ֹͣ��������� ��ʾ�Ͽ�����һ��Ҳû�г��ֹ����� 
            if (ercilinedengdai.Count == 0)
            {
                return;
            }
            //�˹��������һ����·ʹ�䲻���ֹ�����

            fuhecheck(GprogUID,Type,ratacapality);
            for (int j = 0; j < linedengdai.Count; j++)
            {
                if (!Checkadd(GprogUID, Type, ratacapality,linedengdai[j].SUID))
                    return;
                N1Test.NBcal zl = new N1Test.NBcal();
                //zl.Show_KmRelia(1);
                zl.ZLpsp();




                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH1.txt"))
                {
                }
                else
                {
                    return;
                }
                dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH1.txt", FileMode.Open);

                readLine = new StreamReader(dh, Encoding.Default);
                charSplit = new char[] { ' ' };
                strLine = readLine.ReadLine();
               
                double sumpij = 0.0;
                bool lineflag = true;
                while (!string.IsNullOrEmpty(strLine))
                {
                    array1 = strLine.Split(charSplit);


                    string[] dev = new string[2];
                    dev.Initialize();
                    int i = 0;
                    PSPDEV CR = new PSPDEV();
                    //CR.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;

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
                                if (str != "NaN")
                                {
                                    dev[i++] = Convert.ToDouble(str).ToString();
                                }
                                else
                                {
                                    dev[i++] = str;
                                }

                            }
                        }

                    }
                    string con = " WHERE Type='05' AND Name='" + dev[0] + "'AND KSwitchStatus = '0' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·') ";
                    //string con = ",PSP_GprogElevice WHERE PSPDEV.Name='" + dev[0] + "' PSPDEV.SUID = PSP_GprogElevice.DeviceSUID AND PSP_GprogElevice.GprogUID = '" + GprogUID + "'AND PSPDEV.type='05'AND PSPDEV.KSwitchStatus = '0'";
                    CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition",con);
                    if (CR != null && CR.ReferenceVolt != 0)
                    {
                        volt = CR.ReferenceVolt;
                    }
                    else
                        volt = standvolt;
                    current = capability / (Math.Sqrt(3) * volt);
                    if (CR != null && !CR.Name.Contains("������·"))
                    {


                        double linepij = Convert.ToDouble(dev[1]) * capability;
                        double voltR = CR.RateVolt;
                        double Ichange = (double)CR.Burthen;
                        double linXij = System.Math.Sqrt(3) * voltR * Ichange / 1000;
                        if (linepij >= linXij)
                        {
                            lineflag = false;

                            //sumpij += linepij;
                        }
                        for (int k = 0; k < fuheline.Count; k++)
                        {
                            if (CR.SUID == fuheline[k].Suid)
                            {
                                sumpij += System.Math.Abs(fuheline[k].linepij - linepij);
                            }
                        }
                        //  
                        //    //output += "'" + CR.Name.ToString() + "," + (Convert.ToDouble(dev[3]) * capability).ToString() + "," + (Convert.ToDouble(dev[4]) * capability).ToString() + "," + (Convert.ToDouble(dev[5]) * capability).ToString() + "," + (Convert.ToDouble(dev[6]) * capability).ToString() + "," + (Convert.ToDouble(dev[7]) * current).ToString() + "," + (Convert.ToDouble(dev[8]) * Rad_to_Deg).ToString() + "," + dev[11] + "," + "\r\n";
                    }

                    strLine = readLine.ReadLine();
                }
                readLine.Close();
                //if (lineflag)              //���û�г��ֹ��������� ��ֹͣ���м���
                //{
                //    PSPDEV pspb = (PSPDEV)linedengdai[j];
                //    pspb.LineStatus = "����";
                //    Services.BaseService.Update<PSPDEV>(pspb);
                //    lineyiyou.Add(pspb);
                //    for (int i = 0; i < linedengdai.Count; i++)
                //    {
                //        if (linedengdai[i].SUID == pspb.SUID)
                //        {
                //            linedengdai.RemoveAt(j);
                //        }
                //    }
                //    return;
                //}
                //else
                //{
                //XmlNode el = tlVectorControl1.SVGDocument.SelectSingleNode("svg/polyline[@layer='" + tlVectorControl1.SVGDocument.CurrentLayer.ID + "' and @id='" + linedengdai[j].EleID + "']");
                double linevalue = 0;
                if (linedengdai[j].HuganTQ1!=0)
                {
                    linevalue = linedengdai[j].HuganTQ1;
                }
                else
                    linevalue = 1;
                linedaixuan linedai = new linedaixuan(linedengdai[j].Number, linedengdai[j].SUID, linedengdai[j].Name);
                linedai.linepij = sumpij;
                linedai.linevalue = linevalue;
                linedai.linetouzilv = sumpij / linevalue;

                waitlineindex.Add(linedai);
                //}

            }
            waitlineindex.Sort();
            //�ڴ˴����ָ��������· ������·��״̬��Ϊ ���в��������еļ��������¼ �ڵȴ��ļ����ｫ����·ȥ�� ���½�����һ�ֵĲ���
            PSPDEV pspbianhua = new PSPDEV();
            if (waitlineindex.Count > 0)
            {
                pspbianhua.SUID = waitlineindex[waitlineindex.Count - 1].Suid;
            }
            else
            {
                //MessageBox.Show("û�г��ֹ����ɵ���·������鿴һ����·�����Ƿ��趨��ȷ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            pspbianhua = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", pspbianhua);
            pspbianhua.LineStatus = "����";
            Services.BaseService.Update<PSPDEV>(pspbianhua);
            lineyiyou.Add(pspbianhua);
            for (int i = 0; i < linedengdai.Count; i++)
            {
                if (linedengdai[i].SUID == pspbianhua.SUID)
                {
                    linedengdai.RemoveAt(i);
                }
            }
           

        }
        /// <summary>
        ///�ֱ�����һ����· ���м��
        /// <param name="GprogUID">
        /// Ϊ�����Ż���ĿID
        /// </param>
        /// <param name="Type">
        /// Ϊ�Ǹ�ʱ�ڵ������Ż���1Ϊ���塢2���ڡ�3���ڡ�4Զ�ڣ�
        /// </param>
        /// <param name="SUID">
        /// Ϊ������·��ID
        /// </param>
        /// </summary>
        private bool Checkadd(string GprogUID, int Type, double ratedCapacity, string SUID)
          {
              
            
              double current = 0;
              PSPDEV pspDev = new PSPDEV();             
              current = ratedCapacity;      //����ݶ���Ϊ100
              //����ȥ��֮ǰ��������·

              string con = "WHERE SvgUID='" + GprogUID + "'AND EleID='" + Type + "' AND ProjectID='" + Itop.Client.MIS.ProgUID + "'AND Type='05' AND KName ='������·'" ;
              IList list0 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
              for (int i = 0; i < list0.Count; i++)
              {
                  pspDev = (PSPDEV)list0[i];
                  Services.BaseService.Delete<PSPDEV>(pspDev);
              }
              brchcount = 0; buscount = 0; transcount = 0;
              string strCon = null; string strCon2 = null; string strCon3 = null, strConbyq = null; IList listBYQ2 = null, listBYQ3 = null;
               string strBus = null;
               string strBranch = null;
              if (Type==2)
              {
               strCon = "WHERE Type='01' AND SvgUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and Type in ('���վ','��Դ') and L1 in('����','����')) ";
               strCon2 = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·'and ZTstatus in('����','��ѡ')) ";
               strCon3 = "WHERE ProjectID='" + Itop.Client.MIS.ProgUID + "'";
               strConbyq = strCon3 + "and Type='02' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�������ѹ��'and L1 in('����','����'))";
               listBYQ2 = Services.BaseService.GetList("SelectPSPDEVByCondition", strConbyq);

               strConbyq = strCon3 + "and Type='03' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�������ѹ��'and L1 in('����','����'))";
               listBYQ3 = Services.BaseService.GetList("SelectPSPDEVByCondition", strConbyq);
              }
              if (Type == 3)
              {
                  strCon = "WHERE Type='01' AND SvgUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and Type in ('���վ','��Դ') and L1 in('����','����','����')) ";
                  strCon2 = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·'and JQstatus in('����','��ѡ','Ͷ��')) ";
                  strCon3 = "WHERE ProjectID='" + Itop.Client.MIS.ProgUID + "'";
                  strConbyq = strCon3 + "and Type='02' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�������ѹ��'and L1 in('����','����','����'))";
                  listBYQ2 = Services.BaseService.GetList("SelectPSPDEVByCondition", strConbyq);

                  strConbyq = strCon3 + "and Type='03' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�������ѹ��'and L1 in('����','����','����'))";
                  listBYQ3 = Services.BaseService.GetList("SelectPSPDEVByCondition", strConbyq);
              }
              if (Type ==4)
              {
                  strCon = "WHERE Type='01' AND SvgUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and Type in ('���վ','��Դ')) ";
                  strCon2 = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·'and ZQstatus in('����','��ѡ','Ͷ��')) ";
                  strCon3 = "WHERE ProjectID='" + Itop.Client.MIS.ProgUID + "'";
                  strConbyq = strCon3 + "and Type='02' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�������ѹ��')";
                  listBYQ2 = Services.BaseService.GetList("SelectPSPDEVByCondition", strConbyq);

                  strConbyq = strCon3 + "and Type='03' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�������ѹ��')";
                  listBYQ3 = Services.BaseService.GetList("SelectPSPDEVByCondition", strConbyq);
              }
                    double Rad_to_Deg = 180 / Math.PI;
                    {

                        IList listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                        IList listXL = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon2);
                       
                        List<PSPDEV> RemovElm = new List<PSPDEV>();  //ɾ����������׶εļ���
                        //��������·������
                        foreach (PSPDEV dev in listXL)
                        {
                            if (dev.KSwitchStatus == "0")
                            {
                                bool fistflag = false;
                                bool lastflag = false;
                                foreach (PSPDEV pspdev in listMX)
                                {
                                    if (dev.IName == pspdev.Name)
                                    {
                                        fistflag = true;
                                    }
                                    if (dev.JName == pspdev.Name)
                                    {
                                        lastflag = true;
                                    }
                                }
                                //��ĳ��ʱ�ڵ���·����

                                if (lastflag && fistflag)
                                {
                                    dev.SvgUID = GprogUID;
                                    dev.EleID = Type.ToString();
                                    Services.BaseService.Update<PSPDEV>(dev);
                                    if (dev.LineStatus == "�ȴ�" && dev.SUID != SUID)
                                        continue;
                                    brchcount++;

                                }
                                else
                                    RemovElm.Add(dev);
                            }
                        }
                        foreach (PSPDEV dev in RemovElm)
                        {
                            listXL.Remove(dev);
                        }
                            //���˷����Ƿ���ڹ���ĸ��ĸ�߽ڵ�
                        List<string> busname = new List<string>();
                        foreach (PSPDEV dev in listMX)
                        {
                            bool flag = false;
                            foreach (PSPDEV devline in listXL)
                            {
                                if ((dev.Number == devline.LastNode || dev.Number == devline.FirstNode )&& (devline.LineStatus == "����" || devline.LineStatus == "��ѡ" || devline.SUID ==SUID))
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
                      for( int i = 0; i < busname.Count; i++)
            
                        {
                            PSPDEV psp = new PSPDEV();
                            con = "WHERE Name='" + busname[i] + "'AND Type='01'AND ProjectID='" + Itop.Client.MIS.ProgUID + "'";
                            //psp.Name = busname[i];
                            //psp.Type = "01";
                           // psp.SvgUID = GprogUID;
                            psp = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
                            if (psp.Number != 1)
                            {

                                PSPDEV pspline = new PSPDEV();
                                pspline.SvgUID = GprogUID;
                                pspline.EleID = Type.ToString();
                                pspline.FirstNode = psp.Number;
                                pspline.LastNode = psp.Number - 1;
                                pspline.Type = "05";
                                pspline.Lable = "֧·";
                                pspline.Name = "������·" + i;
                                pspline.KName = "������·";
                                pspline.LineStatus = "����";
                                pspline.Number = brchcount + i + 1;
                                pspline.SUID = Guid.NewGuid().ToString();
                                pspline.LineLength = 100;
                                pspline.RateVolt = psp.RateVolt;
                                pspline.ReferenceVolt = psp.ReferenceVolt;
                                pspline.LineR = 9999;
                                pspline.LineTQ = 9999;
                                pspline.KSwitchStatus="0";
                                pspline.UnitFlag="1";
                                pspline.ProjectID = Itop.Client.MIS.ProgUID;
                                Services.BaseService.Create<PSPDEV>(pspline);
                            }
                            else
                            {
                                PSPDEV pspline = new PSPDEV();
                                pspline.SvgUID = GprogUID;
                                pspline.EleID=Type.ToString();
                                pspline.FirstNode = psp.Number;
                                pspline.LastNode = psp.Number + 1;
                                pspline.Type = "05";
                                pspline.Lable = "֧·";
                                pspline.Name = "������·" + i;
                                pspline.KName = "������·";
                                pspline.LineStatus = "����";
                                pspline.Number = brchcount + i + 1;
                                pspline.SUID = Guid.NewGuid().ToString();
                                pspline.LineLength = 100;
                                pspline.RateVolt = psp.RateVolt;
                                pspline.ReferenceVolt = psp.ReferenceVolt;
                                pspline.LineR = 9999;
                                pspline.LineTQ = 9999;
                                pspline.KSwitchStatus="0";
                                pspline.UnitFlag="1";
                                pspline.ProjectID = Itop.Client.MIS.ProgUID;
                                Services.BaseService.Create<PSPDEV>(pspline);
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
                                strCon3 = "WHERE Projectid='" + Itop.Client.MIS.ProgUID + "'and Type = '04' AND IName = '" + dev.Name + "'and SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='�����')";
                                IList listFDJ = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon3);
                                string strCon4 = "WHERE Projectid='" + Itop.Client.MIS.ProgUID + "' and Type = '12' AND IName = '" + dev.Name + "' and SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='����')";
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
                                    strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + (dev.VoltV * Rad_to_Deg).ToString() + " " + ((inputP - outP)).ToString() + " " + ((inputQ - outQ)).ToString());
                                    //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR).ToString() + " " + (dev.LineTQ).ToString() + " " + (dev.LineGNDC).ToString() + " " + "0" + " " + dev.Name.ToString())
                                }
                                else
                                {
                                    outP += dev.OutP / ratedCapacity;
                                    outQ += dev.OutQ / ratedCapacity;
                                    inputP += dev.InPutP / ratedCapacity;
                                    inputQ += dev.InPutQ / ratedCapacity;
                                    strBus += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + MXNodeType(dev.NodeType) + " " + (dev.VoltR / dev.ReferenceVolt).ToString() + " " + (dev.VoltV * Rad_to_Deg).ToString() + " " + (inputP - outP).ToString() + " " + (inputQ - outQ).ToString());
                                    //strData += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + ((dev.LineGNDC) * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + dev.Name.ToString());
                                }
                                buscount++;
                            }
                        }
                        brchcount=0;
                        con="WHERE Type='05'and SvgUID='"+GprogUID+"'and EleID='"+Type+"'and ProjectID='"+Itop.Client.MIS.ProgUID+"'";
                        listXL=Services.BaseService.GetList("SelectPSPDEVByCondition",con);
                        foreach (PSPDEV dev in listXL)
                        {
                          
                                if (dev.KSwitchStatus == "0")
                                {
                                    bool fistflag = false;
                                    bool lastflag = false;
                                    foreach (PSPDEV pspdev in listMX)
                                    {
                                        if (dev.FirstNode == pspdev.Number)
                                        {
                                            fistflag = true;
                                        }
                                        if (dev.LastNode == pspdev.Number)
                                        {
                                            lastflag = true;
                                        }
                                    }
                                    if (lastflag && fistflag)
                                    {
                                        if (strBranch != null && (dev.LineStatus == "����" || dev.LineStatus == "��ѡ" || dev.SUID == SUID))
                                        {
                                            strBranch += "\r\n";
                                        }
                                        if (dev.LineStatus == "����" || dev.LineStatus == "��ѡ" || dev.SUID == SUID)
                                        {
                                            if (dev.UnitFlag == "0")
                                            {
                                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + dev.LineR.ToString() + " " + dev.LineTQ.ToString() + " " + (dev.LineGNDC * 2).ToString() + " " + "0" + " " + "0";

                                            }
                                            else
                                            {
                                                strBranch += dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + (dev.LineR * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineTQ * ratedCapacity / (dev.ReferenceVolt * dev.ReferenceVolt)).ToString() + " " + (dev.LineGNDC * 2 * dev.ReferenceVolt * dev.ReferenceVolt / (ratedCapacity * 1000000)).ToString() + " " + "0" + " " + "0";

                                            }

                                            brchcount++;

                                        }
                                    }

                                }
                        
                           
                           
                        }
                        foreach (PSPDEV dev in listBYQ2)
                        {
                            bool flag = false;
                            if (dev.KSwitchStatus == "0")
                            {

                                if (dev.FirstNode != 0 && dev.LastNode != 0)
                                {
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
                        }

                        foreach (PSPDEV dev in listBYQ3)
                        {
                            if (dev.KSwitchStatus == "0")
                            {
                                if (dev.FirstNode != 0 && dev.LastNode != 0 && dev.Flag != 0)
                                {

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
              //if (File.Exists("c:\\L9.txt"))
              //{
              //    File.Delete("c:\\L9.txt");
              //}
              FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\branch.txt"), FileMode.OpenOrCreate);
              StreamWriter str1 = new StreamWriter(VK, Encoding.Default);
              str1.Write(strBranch);
              str1.Close();
              FileStream L = new FileStream((System.Windows.Forms.Application.StartupPath + "\\bus.txt"), FileMode.OpenOrCreate);
              StreamWriter str2 = new StreamWriter(L, Encoding.Default);
              str2.Write(strBus);
              str2.Close();
              return true;
          }
          /// <summary>
          ///�ֱ�����һ����· ���м��
          /// <param name="GprogUID">
          /// Ϊ�����Ż���ĿID
          /// </param>
          /// <param name="Type">
          /// Ϊ�Ǹ�ʱ�ڵ������Ż���1Ϊ���塢2���ڡ�3���ڡ�4Զ�ڣ�
          /// </param>
          /// <param name="SUID">
          /// Ϊ������·��ID
          /// </param>
          /// </summary>
        public List<LineInfo> LoadData(string GprogUID)
        {
            //�ڴ���ʱ����ͼ��Ĵ�ѡ���Ǵ������ԣ���ѡ�͵ȴ����Ծ���û����ǰ�Ĵ�ѡ


            List<LineInfo> clist = new List<LineInfo>();
            int c1 = 0;
            int c2 = 0;
            int c3 = 0;
            int d1 = 0;
            int d2 = 0;
            int d3 = 0;
            int e1 = 0;
            int e2 = 0;
            int e3 = 0;
            int f1 = 0;
            int f2 = 0;
            int f3 = 0;
            string con = null;
            con = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·'and ZTstatus='����') ";
            IList list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            f1 = list1.Count;
            con = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·'and ZTstatus='��ѡ') ";
            list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            f2 = list1.Count;
            con = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·'and JQstatus in('Ͷ��','��ѡ')) ";
            list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            c2= list1.Count;
            con = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·'and JQstatus ='Ͷ��') ";
            list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            c3 = list1.Count;
            con = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·'and ZQstatus in('Ͷ��','��ѡ')) ";
            list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            d2 = list1.Count;
            con = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·'and ZQstatus ='Ͷ��') ";
            list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            d3 = list1.Count;
            con = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·'and YQstatus in('Ͷ��','��ѡ')) ";
            list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            e2 = list1.Count;
            con = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + GprogUID + "'and type='��·'and YQstatus ='Ͷ��') ";
            list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            e3 = list1.Count;
           
            LineInfo l4 = new LineInfo();
            l4.SvgUID = GprogUID;
            l4.EleID = "1";              //�˲���˵�����Ǹ�ʱ�ڵ�ѡ��
            l4.ObligateField1 = "�������ܹ滮";
            l4.ObligateField2 = f1.ToString();
            l4.ObligateField3 = f2.ToString();
            //l1.ObligateField4 = c3.ToString();
            LineInfo l1 = new LineInfo();
            l1.SvgUID = GprogUID;
            l1.EleID = "2";
            l1.ObligateField1 = "�������ܹ滮";
            l1.ObligateField2 = f1.ToString();
            l1.ObligateField3 = c2.ToString();
            l1.ObligateField4 = c3.ToString();
            LineInfo l2 = new LineInfo();
            l2.SvgUID = GprogUID;
            l2.EleID = "3";
            l2.ObligateField1 = "�������ܹ滮";
            l2.ObligateField2 = f1.ToString();
            l2.ObligateField3 = d2.ToString();
            l2.ObligateField4 = d3.ToString();
            LineInfo l3 = new LineInfo();
            l3.SvgUID = GprogUID;
            l3.EleID = "4";
            l3.ObligateField1 = "Զ�����ܹ滮";
            l3.ObligateField2 = f1.ToString();
            l3.ObligateField3 = e2.ToString();
            l3.ObligateField4 = e3.ToString();
            clist.Add(l4);
            clist.Add(l1);
            clist.Add(l2);
            clist.Add(l3);
            return clist;
        }
        private string svguid = ConfigurationSettings.AppSettings.Get("SvgID");
        //�������ڵ�ʱ��ָ�pspdev����·������״̬ ��Ϊ���кʹ�ѡ��״̬
        public void initdat(string GprogUID)
        {
            //�ڴ˴�����ѡ���Ԫ���豸�����Խ��и���
            LayerGrade l1 = new LayerGrade();
            l1.Type = "1";
            l1.SvgDataUid = svguid;
            IList ttlist = Services.BaseService.GetList("SelectLayerGradeList5", l1);
            int yy1 = System.DateTime.Now.Year, yy2 = System.DateTime.Now.Year, yy3 = System.DateTime.Now.Year;
            if (ttlist.Count > 0)
            {
                LayerGrade n1 = (LayerGrade)ttlist[0];
                yy1 = Convert.ToInt32(n1.Name.Substring(0, 4));
            }
            l1.Type = "2";
            l1.SvgDataUid = svguid;
            ttlist = Services.BaseService.GetList("SelectLayerGradeList5", l1);
            if (ttlist.Count > 0)
            {
                LayerGrade n1 = (LayerGrade)ttlist[0];
                yy2 = Convert.ToInt32(n1.Name.Substring(0, 4));
            }
            l1.Type = "3";
            l1.SvgDataUid = svguid;
            ttlist = Services.BaseService.GetList("SelectLayerGradeList5", l1);
            if (ttlist.Count > 0)
            {
                LayerGrade n1 = (LayerGrade)ttlist[0];
                yy3 = Convert.ToInt32(n1.Name.Substring(0, 4));
            }
            string con = "GprogUID = '" + GprogUID + "' AND Type= '���վ'";

            IList list = Services.BaseService.GetList("SelectPSP_GprogEleviceByCondition", con);
            foreach (PSP_GprogElevice pg in list)
            {

                PSP_Substation_Info ps = new PSP_Substation_Info();
                ps.UID = pg.DeviceSUID;
                ps = (PSP_Substation_Info)Services.BaseService.GetObject("SelectPSP_Substation_InfoByKey", ps);
                if (ps != null)
                {
                    int s2 = 0;
                    if (!string.IsNullOrEmpty(ps.S2))
                    {
                        s2 = Convert.ToInt32(ps.S2);
                    }
                    if (s2 <= System.DateTime.Now.Year)
                    {
                        pg.L1 = "����";
                    }
                    else if (s2 > System.DateTime.Now.Year && Convert.ToInt32(ps.S2) <= yy1)
                    {
                        pg.L1 = "����";
                    }
                    else if (s2 > yy1 && Convert.ToInt32(ps.S2) <= yy2)
                    {
                        pg.L1 = "����";
                    }
                    else if (s2 > yy2 && Convert.ToInt32(ps.S2) <= yy3)
                    {
                        pg.L1 = "Զ��";
                    }
                    Services.BaseService.Update<PSP_GprogElevice>(pg);
                }
                else
                    Services.BaseService.Delete<PSP_GprogElevice>(pg);
            }
            con = "GprogUID = '" + GprogUID + "' AND Type= '��Դ'";

            list = Services.BaseService.GetList("SelectPSP_GprogEleviceByCondition", con);
            foreach (PSP_GprogElevice pg in list)
            {
                PSP_PowerSubstation_Info ps = new PSP_PowerSubstation_Info();
                ps.UID = pg.DeviceSUID;
                ps = (PSP_PowerSubstation_Info)Services.BaseService.GetObject("SelectPSP_PowerSubstation_InfoByKey", ps);
                if (ps != null)
                {
                    int s2 = 0;
                    if (!string.IsNullOrEmpty(ps.S3))
                    {
                        s2 = Convert.ToInt32(ps.S3);
                    }
                    if (s2 <= System.DateTime.Now.Year)
                    {
                        pg.L1 = "����";
                    }
                    else if (s2 > System.DateTime.Now.Year && Convert.ToInt32(ps.S3) <= yy1)
                    {
                        pg.L1 = "����";
                    }
                    else if (s2 > yy1 && Convert.ToInt32(ps.S3) <= yy2)
                    {
                        pg.L1 = "����";
                    }
                    else if (s2 >yy2 && Convert.ToInt32(ps.S3) <= yy3)
                    {
                        pg.L1 = "Զ��";
                    }
                    Services.BaseService.Update<PSP_GprogElevice>(pg);
                }
                else
                    Services.BaseService.Delete<PSP_GprogElevice>(pg);
            }
            con = "GprogUID = '" + GprogUID + "' AND Type= '��·'";

            list = Services.BaseService.GetList("SelectPSP_GprogEleviceByCondition", con);
            foreach (PSP_GprogElevice pg in list)
            {
                PSPDEV ps = new PSPDEV();
                ps.SUID = pg.DeviceSUID;
                ps = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", ps);
                if (ps != null)
                {
                    int s2 = 0;
                    if (!string.IsNullOrEmpty(ps.OperationYear))
                    {
                        s2 = Convert.ToInt32(ps.OperationYear);
                    }
                    if (s2 <= System.DateTime.Now.Year)
                    {
                        pg.L1 = "����";
                        ps.LineStatus = "����";
                    }
                    else
                    {
                        pg.L1 = "��ѡ";
                        ps.LineStatus = "��ѡ";
                    }
                    Services.BaseService.Update<PSP_GprogElevice>(pg);
                    Services.BaseService.Update<PSPDEV>(ps);
                }
                else
                    Services.BaseService.Delete<PSP_GprogElevice>(pg);
            }

        }
    }
}
