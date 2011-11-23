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


namespace Itop.TLPSP.DEVICE
{
    public partial class frmDeviceList : Itop.Client.Base.FormBase
    { 
        public frmDeviceList()
        {

            InitializeComponent();          
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

                if (((XmlElement)node).GetAttribute("name") == "变电站" || ((XmlElement)node).GetAttribute("name") == "电源" || Convert.ToInt32(((XmlElement)node).GetAttribute("id"))>=40)
                {
                    continue;
                }
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
                    IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    foreach (PSPDEV dev in list)
                    {
                        PSP_ElcDevice pspDev = new PSP_ElcDevice();
                        pspDev.DeviceSUID = dev.SUID;
                        pspDev.ProjectSUID = ProjectSUID;
                        IList list1 = UCDeviceBase.DataService.GetList("SelectPSP_ElcDeviceByKey", pspDev);
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
                IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                foreach (PSPDEV dev in list)
                {
                    PSP_ElcDevice pspDev = new PSP_ElcDevice();
                    pspDev.DeviceSUID = dev.SUID;
                    pspDev.ProjectSUID = ProjectSUID;
                    IList list1 = UCDeviceBase.DataService.GetList("SelectPSP_ElcDeviceByKey",pspDev);
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

        private void simpleButton3_Click(object sender, EventArgs e)
        {

            if (comboBox1.Text == null)
            {
                MessageBox.Show("请选择设备类型以后再进行检索", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                     
                        if (selecfrm.getselecindex != 2)
                        {
                            dt.Clear();
                           
                            if (obj != null)
                            {
                               
                                string con = " where type = '" + id + "'";
                                string con2 = " and ProjectID = '" + this.ProjectID + "'";
                                con = con + con2+selecfrm.Sqlcondition;
                                IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                                foreach (PSPDEV dev in list)
                                {
                                   
                                    DataRow row = dt.NewRow();
                                        row["A"] = dev.SUID;
                                        row["B"] = dev.Name;
                                        row["C"] = true;
                                        dt.Rows.Add(row);
                                  
                                }
                            }
                            gridControl1.DataSource = dt;            
                           
                        }
                        else
                        {
                            if (id == "01")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistbusflag), typeof(PSPDEV));
                                selectelement(datatable);
                            }
                            else if (id == "05")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistlineflag), typeof(PSPDEV));
                                selectelement(datatable);
                            }
                            else if (id == "02")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.visttrans2flag), typeof(PSPDEV));
                                selectelement(datatable);
                            }
                            else if (id == "03")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.visttrans3flag), typeof(PSPDEV));
                                selectelement(datatable);
                            }
                            else if (id == "04")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistfdjflag), typeof(PSPDEV));
                                selectelement(datatable);
                            }
                            else if (id == "06")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistdlqflag), typeof(PSPDEV));
                                selectelement(datatable);
                            }
                            else if (id == "08")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistcldrflag), typeof(PSPDEV));
                                selectelement(datatable);
                            }
                            else if (id == "09")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistbldrflag), typeof(PSPDEV));
                                selectelement(datatable);
                            }
                            else if (id == "10")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistcldkflag), typeof(PSPDEV));
                                selectelement(datatable);
                            }
                            else if (id == "11")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistbldkflag), typeof(PSPDEV));
                                selectelement(datatable);
                            }
                            else if (id == "12")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistfhflag), typeof(PSPDEV));
                                selectelement(datatable);
                            }
                            else if (id == "13")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistmlflag), typeof(PSPDEV));
                                selectelement(datatable);
                            }
                            else if (id == "14")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistml2flag), typeof(PSPDEV));
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
              dt.Rows.Add(row);
          }
          gridControl1.DataSource = dt;    
      }
    }
}