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
using Itop.Domain.Table;


namespace Itop.TLPSP.DEVICE
{
    public partial class frmDeviceList_sh : Itop.Client.Base.FormBase
    {
        public frmDeviceList_sh()
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
        private string belongyear;
        public string BelongYear
        {
            get
            {
                return belongyear;
            }
            set
            {
                belongyear = value;
            }
        }
        private int devcetype;
        public int Devicetype
        {
            get { return devcetype; }
            set { devcetype = value; }
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

        public void initCheckcombox()
        {
            switch (devcetype)
            {
            case 0 :   //为变电站
                    string ss = " AreaID='" + Itop.Client.MIS.ProgUID + "' ";
                    IList<PSP_Substation_Info> list = UCDeviceBase.DataService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", ss);
                    if (!string.IsNullOrEmpty(belongyear))   //根据参与计算设备属于那一年先进行一次筛选
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(list[i].S2) && list[i] .S2.Length == 4 && belongyear.Length == 4)
                            {
                                if (Convert.ToInt32(list[i].S2) > Convert.ToInt32(belongyear))
                                {
                                    list.RemoveAt(i);
                                    i--;
                                    continue;
                                }
                            }
                            if (!string.IsNullOrEmpty((list[i]).L29) && (list[i]).L29.Length == 4 && belongyear.Length == 4)
                            {
                                if (Convert.ToInt32((list[i]).L29) <Convert.ToInt32(belongyear))
                                {
                                    list.RemoveAt(i);
                                    i--;
                                    continue;
                                }
                            }
                        }
                    }
                    this.InitionData(this.comboBoxEdit4, "Title", "UID", "请选择", "地区", list);

            	break;
            case 1:   //为电源
                this.labelControl2.Text = "选择电源";
                ss = " AreaID='" + Itop.Client.MIS.ProgUID + "' ";
                IList<PSP_PowerSubstation_Info> list1 = UCDeviceBase.DataService.GetList<PSP_PowerSubstation_Info>("SelectPSP_PowerSubstation_InfoListByWhere", ss);
                if (!string.IsNullOrEmpty(belongyear))   //根据参与计算设备属于那一年先进行一次筛选
                {
                    for (int i = 0; i < list1.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(list1[i].S3) && list1[i].S3.Length == 4 && belongyear.Length == 4)
                        {
                            if (Convert.ToInt32(list1[i].S3) > Convert.ToInt32(belongyear))
                            {
                                list1.RemoveAt(i);
                                i--;
                                continue;
                            }
                        }
                        if (!string.IsNullOrEmpty((list1[i]).S30) && (list1[i]).S29.Length == 4 && belongyear.Length == 4)
                        {
                            if (Convert.ToInt32((list1[i]).S30) < Convert.ToInt32(belongyear))
                            {
                                list1.RemoveAt(i);
                                i--;
                                continue;
                            }
                        }
                    }
                }
                this.InitionData(this.comboBoxEdit4, "Title", "UID", "请选择", "地区", list1);

                break;
            case 2:   //线路

                this.labelControl2.Visible = false;
                this.comboBoxEdit4.Visible = false;
                break;
            case 3:   //无功设备

                this.labelControl2.Visible = false;
                this.comboBoxEdit4.Visible = false;
                break;
            }
            

        }
        public void initcombox()
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

                if (((XmlElement)node).GetAttribute("name") == "变电站" || ((XmlElement)node).GetAttribute("name") == "电源" || Convert.ToInt32(((XmlElement)node).GetAttribute("id")) >= 40)
                {
                    continue;
                }
               switch (devcetype)
               {
               case 0 :
                       if (((XmlElement)node).GetAttribute("id") == "01" || ((XmlElement)node).GetAttribute("id") == "02" || ((XmlElement)node).GetAttribute("id") == "03" || ((XmlElement)node).GetAttribute("id") == "12")
                       {
                           DataRow row = table.NewRow();
                           row["id"] = node.Attributes["id"].Value;
                           row["name"] = node.Attributes["name"].Value;
                           row["class"] = node.Attributes["class"].Value;
                           table.Rows.Add(row);
                       }
                    
               	break;
               case 1:
                if (((XmlElement)node).GetAttribute("id") == "01" || ((XmlElement)node).GetAttribute("id") == "02" || ((XmlElement)node).GetAttribute("id") == "04")
                {
                    DataRow row = table.NewRow();
                    row["id"] = node.Attributes["id"].Value;
                    row["name"] = node.Attributes["name"].Value;
                    row["class"] = node.Attributes["class"].Value;
                    table.Rows.Add(row);
                }
                break;
               case 2:
                if (((XmlElement)node).GetAttribute("id") == "05")
                {
                    DataRow row = table.NewRow();
                    row["id"] = node.Attributes["id"].Value;
                    row["name"] = node.Attributes["name"].Value;
                    row["class"] = node.Attributes["class"].Value;
                    table.Rows.Add(row);
                }
                   
                break;
               case 3:
                if (Convert.ToInt32(((XmlElement)node).GetAttribute("id")) < 40 && Convert.ToInt32(((XmlElement)node).GetAttribute("id")) >= 6 && Convert.ToInt32(((XmlElement)node).GetAttribute("id"))!=12)
                {
                    DataRow row = table.NewRow();
                    row["id"] = node.Attributes["id"].Value;
                    row["name"] = node.Attributes["name"].Value;
                    row["class"] = node.Attributes["class"].Value;
                    table.Rows.Add(row);
                }
                   
                break;
               }
                
            }
            comboBox1.Properties.DataSource = table;
            //comboBox1.DataSource = table;
            //comboBox1.DisplayMember = "name";
            if (DeviceName != null)
            {
                if (table.Select("name='"+DeviceName+"'").Length>0)
                {
                   
                   dt.Clear();
                  DataRow obj = table.Select("name='" + DeviceName + "'")[0];
                   if (obj != null)
                   {
                      
                       string id = obj["id"].ToString();
                       comboBox1.EditValue= id;
                   //    string con = " where type = '" + id + "'";
                   //    string con2 = " and ProjectID = '" + this.ProjectID + "'";
                   //    con = con + con2;
                   //    IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                   //    if (!string.IsNullOrEmpty(belongyear))   //根据参与计算设备属于那一年先进行一次筛选
                   //    {
                   //        for (int i = 0; i < list.Count; i++)
                   //        {
                   //            if (!string.IsNullOrEmpty((list[i] as PSPDEV).OperationYear) && (list[i] as PSPDEV).OperationYear.Length == 4 && belongyear.Length == 4)
                   //            {
                   //                if (Convert.ToInt32((list[i] as PSPDEV).OperationYear) > Convert.ToInt32(belongyear))
                   //                {
                   //                    list.RemoveAt(i);
                   //                    i--;
                   //                    continue;
                   //                }
                   //            }
                   //            if (!string.IsNullOrEmpty((list[i] as PSPDEV).Date2) && (list[i] as PSPDEV).Date2.Length == 4 && belongyear.Length == 4)
                   //            {
                   //                if (Convert.ToInt32((list[i] as PSPDEV).Date2) < Convert.ToInt32(belongyear))
                   //                {
                   //                    list.RemoveAt(i);
                   //                    i--;
                   //                    continue;
                   //                }
                   //            }
                   //        }
                   //    }
                   //    foreach (PSPDEV dev in list)
                   //    {
                   //        PSP_ElcDevice pspDev = new PSP_ElcDevice();
                   //        pspDev.DeviceSUID = dev.SUID;
                   //        pspDev.ProjectSUID = ProjectSUID;
                   //        IList list1 = UCDeviceBase.DataService.GetList("SelectPSP_ElcDeviceByKey", pspDev);
                   //        if (list1.Count > 0)
                   //        {
                   //            DataRow row = dt.NewRow();
                   //            row["A"] = dev.SUID;
                   //            row["B"] = dev.Name;
                   //            row["C"] = true;
                   //            dt.Rows.Add(row);
                   //        }
                   //        else
                   //        {
                   //            DataRow row = dt.NewRow();
                   //            row["A"] = dev.SUID;
                   //            row["B"] = dev.Name;
                   //            row["C"] = false;
                   //            dt.Rows.Add(row);
                   //        }
                   //    }
                   }
                   //gridControl1.DataSource = dt;

                }
                
            }
          
        }
        private void InitionData(DevExpress.XtraEditors.CheckedComboBoxEdit comboBox, string displayMember, string valueMember, string nullTest, string cnStr, object post)
        {
            comboBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            comboBox.Properties.DataSource = post;
            comboBox.Properties.DisplayMember = displayMember;
            comboBox.Properties.ValueMember = valueMember;
            comboBox.Properties.NullText = nullTest;
            comboBox.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(valueMember, "ID", 15, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(displayMember, cnStr)});
        }
        public DataTable DT
        {
            set { dt = value; }
            get { return dt; }
        }
        
        //public void Init()
        //{
        //    dt.Columns.Add("A");
        //    dt.Columns.Add("B");
        //    dt.Columns.Add("C", typeof(bool));
        //    Stream fs = Assembly.GetExecutingAssembly().GetManifestResourceStream("Itop.TLPSP.DEVICE.devicetypes.xml");
        //    //Assembly.GetExecutingAssembly().GetManifestResourceStream
        //    XmlDocument xml = new XmlDocument();
        //    xml.Load(fs);
        //    XmlNodeList nodes = xml.GetElementsByTagName("device");
        //    DataTable table = new DataTable();
        //    table.Columns.Add("id", typeof(string));
        //    table.Columns.Add("name", typeof(string));
        //    table.Columns.Add("class", typeof(string));
        //    foreach (XmlNode node in nodes)
        //    {

        //        if (((XmlElement)node).GetAttribute("name") == "变电站" || ((XmlElement)node).GetAttribute("name") == "电源" || Convert.ToInt32(((XmlElement)node).GetAttribute("id"))>=40)
        //        {
        //            continue;
        //        }
        //        DataRow row = table.NewRow();
        //        row["id"] = node.Attributes["id"].Value;
        //        row["name"] = node.Attributes["name"].Value;
        //        row["class"] = node.Attributes["class"].Value;
        //        table.Rows.Add(row);
               
        //    }
        //    //comboBox1.DataSource = table;
        //    //comboBox1.DisplayMember = "name";
        //    if (DeviceName!=null)
        //    {
        //        comboBox1.Text=DeviceName;
        //    }
        //    {
        //        dt.Clear();
        //        object obj = comboBox1.SelectedItem;
        //        if (obj != null)
        //        {
        //            DataRowView row1 = obj as DataRowView;
        //            string id = row1.Row["id"].ToString();
        //            string con = " where type = '" + id + "'";
        //            string con2 = " and ProjectID = '" + this.ProjectID + "'";
        //            con = con + con2;
        //            IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
        //            if (!string.IsNullOrEmpty(belongyear))   //根据参与计算设备属于那一年先进行一次筛选
        //            {
        //                for (int i = 0; i < list.Count;i++ )
        //                {
        //                    if (!string.IsNullOrEmpty((list[i] as PSPDEV).OperationYear) && (list[i] as PSPDEV).OperationYear.Length == 4 && belongyear.Length == 4 )
        //                    {
        //                        if (Convert.ToInt32((list[i] as PSPDEV).OperationYear) > Convert.ToInt32(belongyear))
        //                      {
        //                          list.RemoveAt(i);
        //                          i--;
        //                          continue;
        //                      }
        //                    }
        //                    if (!string.IsNullOrEmpty((list[i] as PSPDEV).Date2) && (list[i] as PSPDEV).Date2.Length == 4 && belongyear.Length == 4)
        //                    {
        //                        if (Convert.ToInt32((list[i] as PSPDEV).Date2) > Convert.ToInt32(belongyear))
        //                        {
        //                            list.RemoveAt(i);
        //                            i--;
        //                            continue;
        //                        }
        //                    }
        //                }
        //            }
        //            foreach (PSPDEV dev in list)
        //            {
        //                PSP_ElcDevice pspDev = new PSP_ElcDevice();
        //                pspDev.DeviceSUID = dev.SUID;
        //                pspDev.ProjectSUID = ProjectSUID;
        //                IList list1 = UCDeviceBase.DataService.GetList("SelectPSP_ElcDeviceByKey", pspDev);
        //                if (list1.Count > 0)
        //                {
        //                    DataRow row = dt.NewRow();
        //                    row["A"] = dev.SUID;
        //                    row["B"] = dev.Name;
        //                    row["C"] = true;
        //                    dt.Rows.Add(row);
        //                }
        //                else
        //                {
        //                    DataRow row = dt.NewRow();
        //                    row["A"] = dev.SUID;
        //                    row["B"] = dev.Name;
        //                    row["C"] = false;
        //                    dt.Rows.Add(row);
        //                }
        //            }
        //        }
        //        gridControl1.DataSource = dt;      
        //    }
        //}
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
                
                string id = comboBox1.EditValue.ToString();
                XtraSelectfrm selecfrm = new XtraSelectfrm();
                selecfrm.type= id;
                selecfrm.ShowDialog();
                if (selecfrm.DialogResult == DialogResult.OK)
                {
                                     
                        if (selecfrm.getselecindex != 2)
                        {
                            dt.Clear();
                           
                            if (!string.IsNullOrEmpty(id))
                            {
                               
                                string con = " where type = '" + id + "'";
                                string con2 = " and ProjectID = '" + this.ProjectID + "'";
                                con = con + con2+selecfrm.Sqlcondition;
                                IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                                if (!string.IsNullOrEmpty(belongyear))   //根据参与计算设备属于那一年先进行一次筛选
                                {
                                    for (int i = 0; i < list.Count; i++)
                                    {
                                        if (!string.IsNullOrEmpty((list[i] as PSPDEV).OperationYear) && (list[i] as PSPDEV).OperationYear.Length == 4 && belongyear.Length == 4 )
                                        {
                                            if (Convert.ToInt32((list[i] as PSPDEV).OperationYear) > Convert.ToInt32(belongyear))
                                            {
                                                list.RemoveAt(i);
                                                i--;
                                                continue;
                                            }
                                        }
                                        if (!string.IsNullOrEmpty((list[i] as PSPDEV).Date2) && (list[i] as PSPDEV).Date2.Length == 4 && belongyear.Length == 4)
                                        {
                                            if (Convert.ToInt32((list[i] as PSPDEV).Date2) < Convert.ToInt32(belongyear))
                                            {
                                                list.RemoveAt(i);
                                                i--;
                                                continue;
                                            }
                                        }
                                    }
                                }
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

      private void comboBox1_EditValueChanged(object sender, EventArgs e)
      {
          string bdzordy = string.Empty;
          if (devcetype == 0 || devcetype == 1)
          {
              string uid = string.Empty;
              List<string> listuid = new List<string>();
              foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in this.comboBoxEdit4.Properties.Items)
              {
                  if (item.CheckState == CheckState.Checked)
                  {
                      listuid.Add(item.Value.ToString());
                  }
              }
              for (int i = 0; i < listuid.Count; i++)
              {
                  uid += "'" + listuid[i] + "',";
              }
              uid = uid.TrimEnd(',');
              if (!string.IsNullOrEmpty(uid))
              {
                  bdzordy = "and SvgUID in(" + uid + ")";
              }
              if (string.IsNullOrEmpty(bdzordy))
              {
                  MessageBox.Show("请选择变电站或电源后再操作！");
                  return;
              }
          }
          dt.Clear();
          string id = comboBox1.EditValue.ToString();
          if (!string.IsNullOrEmpty(id))
          {
             
              string con = " where type = '" + id + "'";
              string con2 = " and ProjectID = '" + this.ProjectID + "'";
              con = con + con2;
              IList<PSPDEV> listmx=new List<PSPDEV>();
              if (!string.IsNullOrEmpty(bdzordy))
              {
                  con += bdzordy;
                  if (id!="01")
                  {
                      listmx = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", " where  ProjectID = '" + this.ProjectID + "'and type='01'"+bdzordy);
                      if (listmx.Count==0)
                      {
                          listmx = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", " where  ProjectID = '" + this.ProjectID + "'and type='01'and SvgUID IS NULL" );
                      }
                  }

                  
              }
             
              IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
              if (listmx.Count>0&&list.Count==0)
              {
                  string suidcol = string.Empty;
                  string ed=string.Empty;
                  for (int i = 0; i < listmx.Count;i++ )
                  {
                      ed += "'" + listmx[i].SUID + "',";
                  }
                  ed = ed.TrimEnd(',');
                  suidcol = "(" + ed + ")";
                  list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", "where type = '" + id + "'and ProjectID = '" + this.ProjectID + "'and (IName in "+suidcol+" or JName in"+suidcol+" )");
              }
              if (id=="01"&&list.Count==0)    //选择的母线没有数据时
              {
                  list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", " where  ProjectID = '" + this.ProjectID + "'and type='01'and SvgUID IS NULL");
              }
              //if (string.IsNullOrEmpty(bdzordy))       //对所有的数据进行 年份的筛选
              //{
                  if (!string.IsNullOrEmpty(belongyear))   //根据参与计算设备属于那一年先进行一次筛选
                  {
                      for (int i = 0; i < list.Count; i++)
                      {
                          if (!string.IsNullOrEmpty((list[i] as PSPDEV).OperationYear) && (list[i] as PSPDEV).OperationYear.Length == 4 && belongyear.Length == 4)
                          {
                              if (Convert.ToInt32((list[i] as PSPDEV).OperationYear) > Convert.ToInt32(belongyear))
                              {
                                  list.RemoveAt(i);
                                  i--;
                                  continue;
                              }
                          }
                          if (!string.IsNullOrEmpty((list[i] as PSPDEV).Date2) && (list[i] as PSPDEV).Date2.Length == 4 && belongyear.Length == 4)
                          {
                              if (Convert.ToInt32((list[i] as PSPDEV).Date2) <Convert.ToInt32(belongyear))
                              {
                                  list.RemoveAt(i);
                                  i--;
                                  continue;
                              }
                          }
                      }
                  }
             // }
              
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
}