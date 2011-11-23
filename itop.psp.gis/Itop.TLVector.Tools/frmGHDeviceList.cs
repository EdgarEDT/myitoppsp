using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using System.Xml;
using System.IO;
using DevExpress.XtraTreeList.Columns;
using System.Reflection;
using System.Collections;
using Itop.Client.Projects;
using Itop.TLPSP.DEVICE;

namespace ItopVector.Tools
{
    public partial class frmGHDeviceList : Itop.Client.Base.FormBase
    {
        public frmGHDeviceList()
        {

            InitializeComponent();          
        }

        private DataTable dt = new DataTable();
        private string projectid;        //���ID
        public  string ProjectID
        {
            get {
                return projectid;
            }
            set {
                projectid = value;
            }
        }
        private string projectSUID;   //��¼�����Ż��滮��ID
        public string ProjectSUID
        {
            get {
                return projectSUID;
            }
            set {
                projectSUID = value;
            }
        }

        public DataTable DT
        {
            set { dt = value; }
            get { return dt; }
        }
        public void Init()
        {
            dt.Columns.Add("A");
            dt.Columns.Add("B");
            dt.Columns.Add("C", typeof(bool));
            dt.Columns.Add("D");
            //Stream fs = Assembly.GetExecutingAssembly().GetManifestResourceStream("Itop.TLPSP.DEVICE.devicetypes.xml");
            string path = System.Windows.Forms.Application.StartupPath + "\\" + "devicetypes.xml";
            //Assembly.GetExecutingAssembly().GetManifestResourceStream
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            XmlNodeList nodes = xml.GetElementsByTagName("device");
            DataTable table = new DataTable();
            table.Columns.Add("id", typeof(string));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("class", typeof(string));
            foreach (XmlNode node in nodes)
            {
                if (((XmlElement)node).GetAttribute("name") == "���վ" || ((XmlElement)node).GetAttribute("name") == "��Դ" || ((XmlElement)node).GetAttribute("name") == "��·" || ((XmlElement)node).GetAttribute("name") == "�������ѹ��" || ((XmlElement)node).GetAttribute("name") == "�������ѹ��" || ((XmlElement)node).GetAttribute("name") == "�����" || ((XmlElement)node).GetAttribute("name") == "����")
                {
                    DataRow row = table.NewRow();
                    row["id"] = node.Attributes["id"].Value;
                    row["name"] = node.Attributes["name"].Value;
                    row["class"] = node.Attributes["class"].Value;
                    table.Rows.Add(row);
                   
                }
             
            }
            comboBox1.DataSource = table;
            comboBox1.DisplayMember = "name";
            {
                dt.Clear();
                object obj = comboBox1.SelectedItem;
                if (obj != null)
                {
                    DataRowView row1 = obj as DataRowView;
                    string id = row1.Row["id"].ToString();
                    IList list=null;
                    if (id=="20")
                    {
                        string con = " AreaID = '" + Itop.Client.MIS.ProgUID + "'";
                       
                         list= Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
                         foreach (PSP_Substation_Info dev in list)
                         {
                             PSP_GprogElevice pspDev = new PSP_GprogElevice();
                             pspDev.DeviceSUID = dev.UID;
                             pspDev.GprogUID = ProjectSUID;
                             pspDev.Type = "���վ";
                             IList list1 = Services.BaseService.GetList("SelectPSP_GprogEleviceByKey", pspDev);
                             if (list1.Count > 0)
                             {
                                 DataRow row = dt.NewRow();
                                 row["A"] = dev.UID;
                                 row["B"] = dev.Title;
                                 row["C"] = true;
                                 row["D"] = "���վ";
                                 dt.Rows.Add(row);
                             }
                             else
                             {
                                 DataRow row = dt.NewRow();
                                 row["A"] = dev.UID;
                                 row["B"] = dev.Title;
                                 row["C"] = false;
                                 row["D"] = "���վ";
                                 dt.Rows.Add(row);
                             }
                         }
                    }
                    else if (id=="30")
                    {
                        string con = " AreaID = '" + Itop.Client.MIS.ProgUID + "'";
                       
                        list = Services.BaseService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", con);
                        foreach (PSP_PowerSubstation_Info dev in list)
                        {
                            PSP_GprogElevice pspDev = new PSP_GprogElevice();
                            pspDev.DeviceSUID = dev.UID;
                            pspDev.GprogUID = ProjectSUID;
                            pspDev.Type = "��Դ";
                            IList list1 = Services.BaseService.GetList("SelectPSP_GprogEleviceByKey", pspDev);
                            if (list1.Count > 0)
                            {
                                DataRow row = dt.NewRow();
                                row["A"] = dev.UID;
                                row["B"] = dev.Title;
                                row["C"] = true;
                                row["D"]="��Դ";
                                dt.Rows.Add(row);
                            }
                            else
                            {
                                DataRow row = dt.NewRow();
                                row["A"] = dev.UID;
                                row["B"] = dev.Title;
                                row["C"] = false;
                                row["D"]="��Դ";
                                dt.Rows.Add(row);
                            }
                        }
                    }
                    else if (id=="05")
                    {
                         string con = " where type = '" + id + "'";
                        string con2 = " and ProjectID = '" + this.ProjectID + "'";
                        con = con + con2;
                        list = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                        foreach (PSPDEV dev in list)
                        {
                            PSP_GprogElevice pspDev = new PSP_GprogElevice();
                            pspDev.DeviceSUID = dev.SUID;
                            pspDev.GprogUID = ProjectSUID;
                            pspDev.Type = "��·";
                            IList list1 = Services.BaseService.GetList("SelectPSP_GprogEleviceByKey", pspDev);
                            if (list1.Count > 0)
                            {
                                DataRow row = dt.NewRow();
                                row["A"] = dev.SUID;
                                row["B"] = dev.Name;
                                row["C"] = true;
                                row["D"] = "��·";
                                dt.Rows.Add(row);
                            }
                            else
                            {
                                DataRow row = dt.NewRow();
                                row["A"] = dev.SUID;
                                row["B"] = dev.Name;
                                row["C"] = false;
                                row["D"] = "��·";
                                dt.Rows.Add(row);
                            }
                        }

                    }
                    else if (id == "02")
                    {
                        string con = " where type = '" + id + "'";
                        string con2 = " and ProjectID = '" + this.ProjectID + "'";
                        con = con + con2;
                        list = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                        foreach (PSPDEV dev in list)
                        {
                            PSP_GprogElevice pspDev = new PSP_GprogElevice();
                            pspDev.DeviceSUID = dev.SUID;
                            pspDev.GprogUID = ProjectSUID;
                            pspDev.Type = "�������ѹ��";
                            IList list1 = Services.BaseService.GetList("SelectPSP_GprogEleviceByKey", pspDev);
                            if (list1.Count > 0)
                            {
                                DataRow row = dt.NewRow();
                                row["A"] = dev.SUID;
                                row["B"] = dev.Name;
                                row["C"] = true;
                                row["D"] = "�������ѹ��";
                                dt.Rows.Add(row);
                            }
                            else
                            {
                                DataRow row = dt.NewRow();
                                row["A"] = dev.SUID;
                                row["B"] = dev.Name;
                                row["C"] = false;
                                row["D"] = "�������ѹ��";
                                dt.Rows.Add(row);
                            }
                        }

                    }
                    else if (id == "03")
                    {
                        string con = " where type = '" + id + "'";
                        string con2 = " and ProjectID = '" + this.ProjectID + "'";
                        con = con + con2;
                        list = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                        foreach (PSPDEV dev in list)
                        {
                            PSP_GprogElevice pspDev = new PSP_GprogElevice();
                            pspDev.DeviceSUID = dev.SUID;
                            pspDev.GprogUID = ProjectSUID;
                            pspDev.Type = "�������ѹ��";
                            IList list1 = Services.BaseService.GetList("SelectPSP_GprogEleviceByKey", pspDev);
                            if (list1.Count > 0)
                            {
                                DataRow row = dt.NewRow();
                                row["A"] = dev.SUID;
                                row["B"] = dev.Name;
                                row["C"] = true;
                                row["D"] = "�������ѹ��";
                                dt.Rows.Add(row);
                            }
                            else
                            {
                                DataRow row = dt.NewRow();
                                row["A"] = dev.SUID;
                                row["B"] = dev.Name;
                                row["C"] = false;
                                row["D"] = "�������ѹ��";
                                dt.Rows.Add(row);
                            }
                        }

                    }
                    else if (id == "04")
                    {
                        string con = " where type = '" + id + "'";
                        string con2 = " and ProjectID = '" + this.ProjectID + "'";
                        con = con + con2;
                        list = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                        foreach (PSPDEV dev in list)
                        {
                            PSP_GprogElevice pspDev = new PSP_GprogElevice();
                            pspDev.DeviceSUID = dev.SUID;
                            pspDev.GprogUID = ProjectSUID;
                            pspDev.Type = "�����";
                            IList list1 = Services.BaseService.GetList("SelectPSP_GprogEleviceByKey", pspDev);
                            if (list1.Count > 0)
                            {
                                DataRow row = dt.NewRow();
                                row["A"] = dev.SUID;
                                row["B"] = dev.Name;
                                row["C"] = true;
                                row["D"] = "�����";
                                dt.Rows.Add(row);
                            }
                            else
                            {
                                DataRow row = dt.NewRow();
                                row["A"] = dev.SUID;
                                row["B"] = dev.Name;
                                row["C"] = false;
                                row["D"] = "�����";
                                dt.Rows.Add(row);
                            }
                        }

                    }
                    else if (id == "12")
                    {
                        string con = " where type = '" + id + "'";
                        string con2 = " and ProjectID = '" + this.ProjectID + "'";
                        con = con + con2;
                        list = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                        foreach (PSPDEV dev in list)
                        {
                            PSP_GprogElevice pspDev = new PSP_GprogElevice();
                            pspDev.DeviceSUID = dev.SUID;
                            pspDev.GprogUID = ProjectSUID;
                            pspDev.Type = "����";
                            IList list1 = Services.BaseService.GetList("SelectPSP_GprogEleviceByKey", pspDev);
                            if (list1.Count > 0)
                            {
                                DataRow row = dt.NewRow();
                                row["A"] = dev.SUID;
                                row["B"] = dev.Name;
                                row["C"] = true;
                                row["D"] = "����";
                                dt.Rows.Add(row);
                            }
                            else
                            {
                                DataRow row = dt.NewRow();
                                row["A"] = dev.SUID;
                                row["B"] = dev.Name;
                                row["C"] = false;
                                row["D"] = "����";
                                dt.Rows.Add(row);
                            }
                        }

                    }               
                }
                gridControl1.DataSource = dt;      
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            dt = gridControl1.DataSource as DataTable;
            this.DialogResult = DialogResult.OK;
        }

        private void frmDeviceList_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = dt;      
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                gridView1.SetRowCellValue(i, "C", checkEdit1.Checked);
                if (!checkEdit1.Checked)
                {
                    gridView1.SetRowCellValue(i, "C", false);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt.Clear();
               object obj = comboBox1.SelectedItem;
               if (obj != null)
               {
                   DataRowView row1 = obj as DataRowView;
                   string id = row1.Row["id"].ToString();
                   IList list = null;
                   if (id == "20")
                   {
                       string con = " AreaID = '" + Itop.Client.MIS.ProgUID + "'";

                       list = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
                       foreach (PSP_Substation_Info dev in list)
                       {
                           PSP_GprogElevice pspDev = new PSP_GprogElevice();
                           pspDev.DeviceSUID = dev.UID;
                           pspDev.GprogUID = ProjectSUID;
                           pspDev.Type = "���վ";
                           IList list1 = Services.BaseService.GetList("SelectPSP_GprogEleviceByKey", pspDev);
                           if (list1.Count > 0)
                           {
                               DataRow row = dt.NewRow();
                               row["A"] = dev.UID;
                               row["B"] = dev.Title;
                               row["C"] = true;
                               row["D"] = "���վ";
                               dt.Rows.Add(row);
                           }
                           else
                           {
                               DataRow row = dt.NewRow();
                               row["A"] = dev.UID;
                               row["B"] = dev.Title;
                               row["C"] = false;
                               row["D"] = "���վ";
                               dt.Rows.Add(row);
                           }
                       }
                   }
                   else if (id == "30")
                   {
                       string con = " AreaID = '" + Itop.Client.MIS.ProgUID + "'";

                       list = Services.BaseService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", con);
                       foreach (PSP_PowerSubstation_Info dev in list)
                       {
                           PSP_GprogElevice pspDev = new PSP_GprogElevice();
                           pspDev.DeviceSUID = dev.UID;
                           pspDev.GprogUID = ProjectSUID;
                           pspDev.Type = "��Դ";
                           IList list1 = Services.BaseService.GetList("SelectPSP_GprogEleviceByKey", pspDev);
                           if (list1.Count > 0)
                           {
                               DataRow row = dt.NewRow();
                               row["A"] = dev.UID;
                               row["B"] = dev.Title;
                               row["C"] = true;
                               row["D"] = "��Դ";
                               dt.Rows.Add(row);
                           }
                           else
                           {
                               DataRow row = dt.NewRow();
                               row["A"] = dev.UID;
                               row["B"] = dev.Title;
                               row["C"] = false;
                               row["D"] = "��Դ";
                               dt.Rows.Add(row);
                           }
                       }
                   }
                   else if (id == "05")
                   {
                       string con = " where type = '" + id + "'";
                       string con2 = " and ProjectID = '" + this.ProjectID + "'";
                       con = con + con2;
                       list = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                       foreach (PSPDEV dev in list)
                       {
                           PSP_GprogElevice pspDev = new PSP_GprogElevice();
                           pspDev.DeviceSUID = dev.SUID;
                           pspDev.GprogUID = ProjectSUID;
                           pspDev.Type = "��·";
                           IList list1 = Services.BaseService.GetList("SelectPSP_GprogEleviceByKey", pspDev);
                           if (list1.Count > 0)
                           {
                               DataRow row = dt.NewRow();
                               row["A"] = dev.SUID;
                               row["B"] = dev.Name;
                               row["C"] = true;
                               row["D"] = "��·";
                               dt.Rows.Add(row);
                           }
                           else
                           {
                               DataRow row = dt.NewRow();
                               row["A"] = dev.SUID;
                               row["B"] = dev.Name;
                               row["C"] = false;
                               row["D"] = "��·";
                               dt.Rows.Add(row);
                           }
                       }

                   }
                   else if (id == "02")
                   {
                       string con = " where type = '" + id + "'";
                       string con2 = " and ProjectID = '" + this.ProjectID + "'";
                       con = con + con2;
                       list = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                       foreach (PSPDEV dev in list)
                       {
                           PSP_GprogElevice pspDev = new PSP_GprogElevice();
                           pspDev.DeviceSUID = dev.SUID;
                           pspDev.GprogUID = ProjectSUID;
                           pspDev.Type = "�������ѹ��";
                           IList list1 = Services.BaseService.GetList("SelectPSP_GprogEleviceByKey", pspDev);
                           if (list1.Count > 0)
                           {
                               DataRow row = dt.NewRow();
                               row["A"] = dev.SUID;
                               row["B"] = dev.Name;
                               row["C"] = true;
                               row["D"] = "�������ѹ��";
                               dt.Rows.Add(row);
                           }
                           else
                           {
                               DataRow row = dt.NewRow();
                               row["A"] = dev.SUID;
                               row["B"] = dev.Name;
                               row["C"] = false;
                               row["D"] = "�������ѹ��";
                               dt.Rows.Add(row);
                           }
                       }

                   }
                   else if (id == "03")
                   {
                       string con = " where type = '" + id + "'";
                       string con2 = " and ProjectID = '" + this.ProjectID + "'";
                       con = con + con2;
                       list = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                       foreach (PSPDEV dev in list)
                       {
                           PSP_GprogElevice pspDev = new PSP_GprogElevice();
                           pspDev.DeviceSUID = dev.SUID;
                           pspDev.GprogUID = ProjectSUID;
                           pspDev.Type = "�������ѹ��";
                           IList list1 = Services.BaseService.GetList("SelectPSP_GprogEleviceByKey", pspDev);
                           if (list1.Count > 0)
                           {
                               DataRow row = dt.NewRow();
                               row["A"] = dev.SUID;
                               row["B"] = dev.Name;
                               row["C"] = true;
                               row["D"] = "�������ѹ��";
                               dt.Rows.Add(row);
                           }
                           else
                           {
                               DataRow row = dt.NewRow();
                               row["A"] = dev.SUID;
                               row["B"] = dev.Name;
                               row["C"] = false;
                               row["D"] = "�������ѹ��";
                               dt.Rows.Add(row);
                           }
                       }

                   }
                   else if (id == "04")
                   {
                       string con = " where type = '" + id + "'";
                       string con2 = " and ProjectID = '" + this.ProjectID + "'";
                       con = con + con2;
                       list = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                       foreach (PSPDEV dev in list)
                       {
                           PSP_GprogElevice pspDev = new PSP_GprogElevice();
                           pspDev.DeviceSUID = dev.SUID;
                           pspDev.GprogUID = ProjectSUID;
                           pspDev.Type = "�����";
                           IList list1 = Services.BaseService.GetList("SelectPSP_GprogEleviceByKey", pspDev);
                           if (list1.Count > 0)
                           {
                               DataRow row = dt.NewRow();
                               row["A"] = dev.SUID;
                               row["B"] = dev.Name;
                               row["C"] = true;
                               row["D"] = "�����";
                               dt.Rows.Add(row);
                           }
                           else
                           {
                               DataRow row = dt.NewRow();
                               row["A"] = dev.SUID;
                               row["B"] = dev.Name;
                               row["C"] = false;
                               row["D"] = "�����";
                               dt.Rows.Add(row);
                           }
                       }

                   }
                   else if (id == "12")
                   {
                       string con = " where type = '" + id + "'";
                       string con2 = " and ProjectID = '" + this.ProjectID + "'";
                       con = con + con2;
                       list = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                       foreach (PSPDEV dev in list)
                       {
                           PSP_GprogElevice pspDev = new PSP_GprogElevice();
                           pspDev.DeviceSUID = dev.SUID;
                           pspDev.GprogUID = ProjectSUID;
                           pspDev.Type = "����";
                           IList list1 = Services.BaseService.GetList("SelectPSP_GprogEleviceByKey", pspDev);
                           if (list1.Count > 0)
                           {
                               DataRow row = dt.NewRow();
                               row["A"] = dev.SUID;
                               row["B"] = dev.Name;
                               row["C"] = true;
                               row["D"] = "����";
                               dt.Rows.Add(row);
                           }
                           else
                           {
                               DataRow row = dt.NewRow();
                               row["A"] = dev.SUID;
                               row["B"] = dev.Name;
                               row["C"] = false;
                               row["D"] = "����";
                               dt.Rows.Add(row);
                           }
                       }

                   }
               }
            gridControl1.DataSource = dt;            
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

            if (comboBox1.Text == null)
            {
                MessageBox.Show("��ѡ���豸�����Ժ��ٽ��м���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                object obj = comboBox1.SelectedItem;
                DataRowView row1 = obj as DataRowView;
                string id = row1.Row["id"].ToString();
                XtraSelectfrm selecfrm = new XtraSelectfrm();
                selecfrm.type= id;
                selecfrm.ShowDialog();
                if (selecfrm.DialogResult == DialogResult.OK)
                {
                    dt.Clear();
                    if (id =="20")
                    {
                       
                        if (selecfrm.getselecindex == 0)
                        {
                            string con = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND Title like'%" + selecfrm.DeviceName + "%'";
                           IList list = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
                           foreach (PSP_Substation_Info dev in list)
                           {

                               DataRow row = dt.NewRow();
                               row["A"] = dev.UID;
                               row["B"] = dev.Title;
                               row["C"] = true;
                               row["D"] = "���վ";
                               dt.Rows.Add(row);

                           }
                        }
                        else if (selecfrm.getselecindex == 1)
                        {
                            string con = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND L1 ='" + selecfrm.devicevolt + "'";
                            IList list = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
                            foreach (PSP_Substation_Info dev in list)
                            {

                                DataRow row = dt.NewRow();
                                row["A"] = dev.UID;
                                row["B"] = dev.Title;
                                row["C"] = true;
                                row["D"] = "���վ";
                                dt.Rows.Add(row);

                            }
                        }
                        else if (selecfrm.getselecindex == 2)
                        {
                            foreach (PSP_Substation_Info dev in convertdirecttolist<PSP_Substation_Info>(selecfrm.vistsubstationflag))
                            {
                                DataRow row = dt.NewRow();
                                row["A"] = dev.UID;
                                row["B"] = dev.Title;
                                row["C"] = true;
                                row["D"] = "���վ";
                                dt.Rows.Add(row);
                            }
                        }

                    }
                    else if (id == "30")
                    {
                        if (selecfrm.getselecindex == 0)
                        {
                            string con = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND Title like'%" + selecfrm.DeviceName + "%'";
                            IList list = Services.BaseService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", con);
                            foreach (PSP_PowerSubstation_Info dev in list)
                            {

                                DataRow row = dt.NewRow();
                                row["A"] = dev.UID;
                                row["B"] = dev.Title;
                                row["C"] = true;
                                row["D"] = "��Դ";
                                dt.Rows.Add(row);

                            }
                        }
                        else if (selecfrm.getselecindex == 1)
                        {
                            string con = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND S1 ='" + selecfrm.devicevolt + "'";
                            IList list = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
                            foreach (PSP_PowerSubstation_Info dev in list)
                            {

                                DataRow row = dt.NewRow();
                                row["A"] = dev.UID;
                                row["B"] = dev.Title;
                                row["C"] = true;
                                row["D"] = "��Դ";
                                dt.Rows.Add(row);

                            }
                        }
                        else if (selecfrm.getselecindex == 2)
                        {

                            foreach (PSP_PowerSubstation_Info dev in convertdirecttolist<PSP_PowerSubstation_Info>(selecfrm.vistpowerflag))
                            {
                                DataRow row = dt.NewRow();
                                row["A"] = dev.UID;
                                row["B"] = dev.Title;
                                row["C"] = true;
                                row["D"] = "��Դ";
                                dt.Rows.Add(row);
                            }
                        }
                    }
                    //else if(id=="05")
                    else if (id == "05")
                    {
                        if (selecfrm.getselecindex != 2)
                        {                          
                            if (obj != null)
                            {
                                if (id == "20")
                                {
                                }
                                string con = " where type = '" + id + "'";
                                string con2 = " and ProjectID = '" + this.ProjectID + "'";
                                con = con + con2 + selecfrm.Sqlcondition;
                                IList list = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                                foreach (PSPDEV dev in list)
                                {

                                    string type = dev.Type;
                                    if (type == "05")
                                    {
                                        type = "��·";
                                    }
                                    else if (type == "02")
                                    {
                                        type = "�������ѹ��";
                                    }
                                    else if (type == "03")
                                    {
                                        type = "�������ѹ��";
                                    }
                                    else if (type == "04")
                                    {
                                        type = "�����";
                                    }
                                    else if (type == "12")
                                    {
                                        type = "����";
                                    }
                                    DataRow row = dt.NewRow();
                                    row["A"] = dev.SUID;
                                    row["B"] = dev.Name;
                                    row["C"] = true;
                                    row["D"] = type;
                                    dt.Rows.Add(row);
                                }
                            }
                            gridControl1.DataSource = dt;
                        }
                        else
                        {                         
                          DataTable datatable = new DataTable();
                          datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistlineflag), typeof(PSPDEV));
                          selectelement(datatable);                         
                        }

                    }
                
                   
                }
            }
        }
         private List<T> convertdirecttolist<T>(Dictionary<string,T> col)
        {
            List<T> ss=new List<T>();
            foreach (KeyValuePair<string, T> keyvalue in col)
            {
                ss.Add(keyvalue.Value);
            }
            return ss;
        }
      private void selectelement(DataTable data)
      {
          dt.Clear();
          foreach (DataRow dr in data.Rows)
          {
              DataRow row = dt.NewRow();
              row["A"] = dr["SUID"].ToString();
              row["B"] = dr["name"].ToString();
              row["C"] = true;
              string type=dr["Type"].ToString();
              if (type=="05")
              {
                  type = "��·";
              }
              else if (type=="02")
              {
                  type = "�������ѹ��";
              }
              else if (type == "03")
              {
                  type = "�������ѹ��";
              }
              else if (type == "04")
              {
                  type = "�����";
              }
              else if (type == "12")
              {
                  type = "����";
              }
              row["D"] = type;
              dt.Rows.Add(row);
          }
          gridControl1.DataSource = dt;    
      }
    }
}