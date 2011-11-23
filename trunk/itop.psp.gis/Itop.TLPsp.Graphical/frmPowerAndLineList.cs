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


namespace Itop.TLPsp.Graphical
{
    public partial class frmPowerAndLineList : Itop.Client.Base.FormBase
    { 
        public frmPowerAndLineList()
        {
            InitializeComponent();
            Init();
        }
        public frmPowerAndLineList(string obj,string projectSUID)
        {
            InitializeComponent();
            ProjectID = projectSUID;
            Init(obj);
        }
        private DataTable dt = new DataTable();
        private string projectid;
        public  string ProjectID
        {
            get {
                return projectid;
            }
            set {
                projectid = value;
            }
        }
        private string projectSUID;
        public string ProjectSUID
        {
            get {
                return projectSUID;
            }
            set {
                projectSUID = value;
            }
        }
        private string devicename;
        public string DeviceName
        {
            get
            {
                return devicename;
            }
            set
            {
                devicename = value;
            }
        }
        public string comBoxText
        {
            get { return comboBox1.Text; }
            set { comboBox1.Text = value; }
        }
        public DataTable DT
        {
            set { dt = value; }
            get { return dt; }
        }

        public void Init(string obj)
        {
            dt.Columns.Add("A");
            dt.Columns.Add("B");
            dt.Columns.Add("C", typeof(bool));
            //Stream fs = Assembly.GetExecutingAssembly().GetManifestResourceStream("Itop.TLPSP.DEVICE.devicetypes.xml");
            ////Assembly.GetExecutingAssembly().GetManifestResourceStream
            //XmlDocument xml = new XmlDocument();
            //xml.Load(fs);
            //XmlNodeList nodes = xml.GetElementsByTagName("device");
            DataTable table = new DataTable();
            table.Columns.Add("id", typeof(string));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("class", typeof(string));
            //foreach (XmlNode node in nodes)
            //{
            //    DataRow row = table.NewRow();
            //    row["id"] = node.Attributes["id"].Value;
            //    row["name"] = node.Attributes["name"].Value;
            //    row["class"] = node.Attributes["class"].Value;
            //    table.Rows.Add(row);
            //}
            //comboBox1.DataSource = table;
            //comboBox1.DisplayMember = "name";
            //if (DeviceName != null)
            //{
            //    comboBox1.Text = DeviceName;
            //}
       
            {
                dt.Clear();           
                if (obj != null)
                {                    
                //    DataRowView row1 = obj as DataRowView;
                    string id = obj;
                    string strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + ProjectID + "'";
                    string strCon2 = null;
                    string strCon = null;
       

                    strCon2 = " AND Type = '" + obj + "'";
                    strCon = strCon1 + strCon2;
                    IList list = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    foreach (PSPDEV dev in list)
                    {
                        PSP_ElcDevice pspDev = new PSP_ElcDevice();
                        pspDev.DeviceSUID = dev.SUID;
                        pspDev.ProjectSUID = ProjectSUID;
                        IList list1 = Services.BaseService.GetList("SelectPSP_ElcDeviceByKey", pspDev);
                        if (list1.Count > 0)
                        {
                            DataRow row = dt.NewRow();
                            row["A"] = dev.SUID;
                            row["B"] = dev.Name;
                            row["C"] = true;
                            dt.Rows.Add(row);
                        }
                        else
                        {
                            DataRow row = dt.NewRow();
                            row["A"] = dev.SUID;
                            row["B"] = dev.Name;
                            row["C"] = false;
                            dt.Rows.Add(row);
                        }
                    }
                }
                gridControl1.DataSource = dt;
            }
        }
        public void Init()
        {
            dt.Columns.Add("A");
            dt.Columns.Add("B");
            dt.Columns.Add("C", typeof(bool));
            Stream fs = Assembly.GetExecutingAssembly().GetManifestResourceStream("Itop.TLPSP.DEVICE.devicetypes.xml");
            //Assembly.GetExecutingAssembly().GetManifestResourceStream
            XmlDocument xml = new XmlDocument();
            xml.Load(fs);
            XmlNodeList nodes = xml.GetElementsByTagName("device");
            DataTable table = new DataTable();
            table.Columns.Add("id", typeof(string));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("class", typeof(string));
            foreach (XmlNode node in nodes)
            {
                DataRow row = table.NewRow();
                row["id"] = node.Attributes["id"].Value;
                row["name"] = node.Attributes["name"].Value;
                row["class"] = node.Attributes["class"].Value;
                table.Rows.Add(row);
            }
            comboBox1.DataSource = table;
            comboBox1.DisplayMember = "name";
            if (DeviceName!=null)
            {
                comboBox1.Text=DeviceName;
            }
            {
                dt.Clear();
                object obj = comboBox1.SelectedItem;
                if (obj != null)
                {
                    DataRowView row1 = obj as DataRowView;
                    string id = row1.Row["id"].ToString();
                    string con = " where type = '" + id + "'";
                    string con2 = " and ProjectID = '" + this.ProjectID + "'";
                    con = con + con2;
                    IList list = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                    foreach (PSPDEV dev in list)
                    {
                        PSP_ElcDevice pspDev = new PSP_ElcDevice();
                        pspDev.DeviceSUID = dev.SUID;
                        pspDev.ProjectSUID = ProjectSUID;
                        IList list1 = Services.BaseService.GetList("SelectPSP_ElcDeviceByKey", pspDev);
                        if (list1.Count > 0)
                        {
                            DataRow row = dt.NewRow();
                            row["A"] = dev.SUID;
                            row["B"] = dev.Name;
                            row["C"] = true;
                            dt.Rows.Add(row);
                        }
                        else
                        {
                            DataRow row = dt.NewRow();
                            row["A"] = dev.SUID;
                            row["B"] = dev.Name;
                            row["C"] = false;
                            dt.Rows.Add(row);
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
            if (obj!=null)
            {
                DataRowView row1 = obj as DataRowView;
                string id = row1.Row["id"].ToString();
                string con = " where type = '" + id + "'";
                string con2 = " and ProjectID = '" + this.ProjectID + "'";
                con = con + con2;
                IList list = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                foreach (PSPDEV dev in list)
                {
                    PSP_ElcDevice pspDev = new PSP_ElcDevice();
                    pspDev.DeviceSUID = dev.SUID;
                    pspDev.ProjectSUID = ProjectSUID;
                    IList list1 = Services.BaseService.GetList("SelectPSP_ElcDeviceByKey",pspDev);
                    if (list1.Count>0)
                    {
                        DataRow row = dt.NewRow();
                        row["A"] = dev.SUID;
                        row["B"] = dev.Name;
                        row["C"] = true;
                        dt.Rows.Add(row);
                    }
                    else
                    {
                        DataRow row = dt.NewRow();
                        row["A"] = dev.SUID;
                        row["B"] = dev.Name;
                        row["C"] = false;
                        dt.Rows.Add(row);
                    }
                }
            }
            gridControl1.DataSource = dt;            
        }
    }
}