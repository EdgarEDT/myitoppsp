using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using System.IO;
using System.Reflection;
using System.Xml;
using DevExpress.XtraTreeList.Columns;
using Itop.Domain.Graphics;
using System.Collections;
using Itop.Client.Common;
using Itop.Client;
using Itop.Client.Base;
namespace Itop.TLPSP.DEVICE
{
    public partial class frmDeviceSelect : FormBase
    {
        private string UCType = "";
        private IList<object> listUID;
        public IList<object> ListUID
        {
            get
            {
                return listUID;
            }
            set
            {
                listUID = value;
            }
        }
        public frmDeviceSelect()
        {
            InitializeComponent();

        }
        #region 初始化
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            treeList1.FocusedNode = null;
            if (bdzwhere != "" && xlwhere != "")
            {
                simpleButton1.Visible = false;
                simpleButton3.Visible = false;
                simpleButton4.Visible = false;
            }
        }
        protected void Init()
        {
           
        }

        /// <summary>
        /// 初始设备分类
        /// </summary>
        private void InitDeviceTypeAll()
        {
            Stream fs = Assembly.GetExecutingAssembly().GetManifestResourceStream("Itop.TLPSP.DEVICE.devicetypes.xml");
            //Assembly.GetExecutingAssembly().GetManifestResourceStream
            XmlDocument xml = new XmlDocument();
            xml.Load(fs);
            XmlNodeList nodes = xml.GetElementsByTagName("device");
            DataTable table = new DataTable();
            table.Columns.Add("id", typeof(string));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("class", typeof(string));
            TreeListColumn column = new TreeListColumn();
            column.Caption = "设备分类";
            column.FieldName = "name";
            column.VisibleIndex = 0;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            foreach (XmlNode node in nodes)
            {
                DataRow row = table.NewRow();

                row["id"] = node.Attributes["id"].Value;
                row["name"] = node.Attributes["name"].Value;
                row["class"] = node.Attributes["class"].Value;
                table.Rows.Add(row);
            }
            treeList1.DataSource = table;
        }
        public void InitDeviceType(params string[] type)
        {
            if (type.Length == 0)
            {
                InitDeviceTypeAll();
                return;
            }
            ArrayList list = new ArrayList();
            list.AddRange(type);
            UCType = type[0];
            Stream fs = Assembly.GetExecutingAssembly().GetManifestResourceStream("Itop.TLPSP.DEVICE.devicetypes.xml");
            //Assembly.GetExecutingAssembly().GetManifestResourceStream
            XmlDocument xml = new XmlDocument();
            xml.Load(fs);
            XmlNodeList nodes = xml.GetElementsByTagName("device");
            DataTable table = new DataTable();
            table.Columns.Add("id", typeof(string));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("class", typeof(string));
            TreeListColumn column = new TreeListColumn();
            column.Caption = "设备分类";
            column.FieldName = "name";
            column.VisibleIndex = 0;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            foreach (XmlNode node in nodes)
            {
                
                string stype = node.Attributes["id"].Value;
                if (!list.Contains(stype)) continue;
                DataRow row = table.NewRow();
                row["id"] = node.Attributes["id"].Value;
                row["name"] = node.Attributes["name"].Value;
                row["class"] = node.Attributes["class"].Value;
                table.Rows.Add(row);
                if(stype=="05"){
                    //DataRow row2 = table.NewRow();
                    //row2["id"] = "01";
                    //row2["name"] = "母线";
                    //row2["class"] = "Itop.TLPSP.DEVICE.UCDeviceMX";
                    //table.Rows.Add(row2);
                }
            }
            treeList1.DataSource = table;
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 实例化类接口
        /// </summary>
        /// <param name="classname"></param>
        /// <returns></returns>
        private UCDeviceBase createInstance(string classname)
        {
            return Assembly.GetExecutingAssembly().CreateInstance(classname, false) as UCDeviceBase;
        }
        private void showDevice(UCDeviceBase device)
        {
            if (device == null) return;
            device.Dock = DockStyle.Fill;
            splitContainerControl1.Panel2.Controls.Add(device);
        }
        #endregion

        #region 字段
        UCDeviceBase curDevice;
        Dictionary<string, UCDeviceBase> devicTypes = new Dictionary<string, UCDeviceBase>();

        Dictionary<string, object> devic = new Dictionary<string, object>();
        #endregion
        private string bdzwhere="";
        public string BdzWhere
        {
            set { bdzwhere = value; }
            get { return bdzwhere; }
        }
        private string xlwhere = "";
        public string XlWhere
        {
            set { xlwhere = value; }
            get { return xlwhere; }
        }

        protected string projectID;
        public string ProjectID
        {
            get { return projectID; }
            set
            {
                projectID = value;
            }
        }
        //为某个项目的ID
        protected string projectsuid;
        public string ProjectSuid
        {
            get { return projectsuid; }
            set { projectsuid = value; }
        }
        public bool shortflag = false;
        public bool pspflag = false;
        public bool wjghflag = false;
        public string wjghuid = "";
        public List<eleclass> shortselelement = new List<eleclass>(); //进行判断在短路中此元件是否已经在显示图上没有
        public List<eleclass> wjghselelement = null;               //判断在接线图中进行网架优化时图元是否已经画上了
        public Dictionary<string, object> GetSelectedDevice()
        {
            return devic;
        }
        UCDeviceBase device = null;
        public UCDeviceBase GetDevice
        {
            get
            {
                return device;
            }
        }
        private void treeList1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) return;
            TreeListNode node = treeList1.FocusedNode;
            if (node == null) return;
            string strID = node["id"].ToString();
            string dtype = node["class"].ToString();
            //UCDeviceBase device = null;
            //主网的设备取消新建功能
            if (Convert.ToInt32(strID) <= 15)
            {
                simpleButton4.Enabled = false;
            }
            else
            {
                simpleButton4.Enabled = true;
            }
            if (devicTypes.ContainsKey(dtype))
            {
                device = devicTypes[dtype];
                device.ID = strID;
                try
                {
                    device.Show();
                    
                }
                catch { }
            }
            else
            {
                device = createInstance(dtype);
                device.ID = strID;
                device.ProjectID = ProjectID;
                devicTypes.Add(dtype, device);
                showDevice(device);
            }
            if (!shortflag)
            {
                if (!pspflag)
                {
                    if (!wjghflag)
                    {
                        if (curDevice != null && curDevice != device) curDevice.Hide();
                        curDevice = device;
                        if (curDevice != null)
                        {
                            if (xlwhere != "" || bdzwhere != "")
                            {
                                if (strID == "05")
                                {
                                    curDevice.strCon = xlwhere;
                                }
                                if (strID == "20")
                                {
                                    curDevice.strCon = bdzwhere;
                                }

                            }
                            else
                            {
                                curDevice.strCon = " where 1=1 and ";
                            }
                            curDevice.Init();
                        }
                    }
                    else
                    {
                        if (curDevice != null && curDevice != device) curDevice.Hide();
                        curDevice = device;
                        if (curDevice != null)
                        {
                            curDevice.wjghuid = wjghuid;
                            curDevice.strCon = " where 1=1 and ";
                            curDevice.WjghInit();
                        }
                    }
                }
                else
                {
                    if (curDevice != null && curDevice != device) curDevice.Hide();
                    curDevice = device;
                    if (curDevice != null)
                    {

                        curDevice.strCon = " where 1=1 and";
                        curDevice.PspInit(listUID);
                    }
                }

            }
            else if (shortflag)
            {

                if (curDevice != null && curDevice != device) curDevice.Hide();
                curDevice = device;
                if (curDevice != null)
                {
                    curDevice.strCon = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + ProjectSuid + "'";
                    curDevice.shortselelement = shortselelement;
                    curDevice.SelshortDevices();
                    // IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    // List<PSPDEV> delsum=new List<PSPDEV>();
                    // for (int i = 0; i < shortselelement.Count;i++ )
                    // {
                    //     if (shortselelement[i].selectflag)
                    //     {
                    //         PSPDEV psp = new PSPDEV();
                    //         psp.SUID = shortselelement[i].suid;
                    //         psp =(PSPDEV) UCDeviceBase.DataService.GetObject("SelectPSPDEVByKey", psp);
                    //         delsum.Add(psp);
                    //     }
                    // }
                    // for (int m = 0; m < delsum.Count; m++)
                    // {
                    //     list.Remove(delsum[m]);
                    // }
                    //DataTable datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSPDEV));
                    //curDevice.gridControl1.Refresh();
                    //curDevice.gridControl1.DataSource = datatable1;            
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            object dev = null;

            if (curDevice != null)
            {
                dev = curDevice.SelectedDevice;
                if (dev != null)
                {
                    if (dev is PSP_Substation_Info)
                    {
                        devic.Add("id", ((PSP_Substation_Info)dev).UID);
                        devic.Add("name", ((PSP_Substation_Info)dev).Title);
                        devic.Add("device", dev);
                    } if (dev is PSP_PowerSubstation_Info)
                    {
                        devic.Add("id", ((PSP_PowerSubstation_Info)dev).UID);
                        devic.Add("name", ((PSP_PowerSubstation_Info)dev).Title);
                        devic.Add("device", dev);
                    }
                    if (dev is PSPDEV)
                    {
                        devic.Add("id", ((PSPDEV)dev).SUID);
                        devic.Add("name", ((PSPDEV)dev).Name);
                        devic.Add("device", dev);
                    }

                }
                else
                {
                    return;
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            TreeListNode node = treeList1.FocusedNode;
            if (node == null)
            {
                MessageBox.Show("请选择设备类型以后再进行检索", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                XtraSelectfrm selecfrm = new XtraSelectfrm();
                selecfrm.type = GetDevice.GetType();
                selecfrm.ShowDialog();
                if (selecfrm.DialogResult == DialogResult.OK)
                {
                    if (GetDevice is UCDeviceBDZ)
                    {
                        if (selecfrm.getselecindex == 0)
                        {
                            GetDevice.strCon = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND Title like'%" + selecfrm.DeviceName + "%'";
                            GetDevice.SelDevices();
                        }
                        else if (selecfrm.getselecindex == 1)
                        {
                            GetDevice.strCon = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND L1 ='" + selecfrm.devicevolt + "'";
                            GetDevice.SelDevices();
                        }
                        else if (selecfrm.getselecindex == 2)
                        {
                            DataTable datatable = new DataTable();
                            datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSP_Substation_Info>(selecfrm.vistsubstationflag), typeof(PSP_Substation_Info));
                            GetDevice.gridControl1.DataSource = datatable;
                        }

                    }
                    else if (GetDevice is UCDeviceDY)
                    {
                        if (selecfrm.getselecindex == 0)
                        {
                            GetDevice.strCon = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND Title like'%" + selecfrm.DeviceName + "%'";
                            GetDevice.SelDevices();
                        }
                        else if (selecfrm.getselecindex == 1)
                        {
                            GetDevice.strCon = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND S1 ='" + selecfrm.devicevolt + "'";
                            GetDevice.SelDevices();
                        }
                        else if (selecfrm.getselecindex == 2)
                        {
                            DataTable datatable = new DataTable();
                            datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSP_PowerSubstation_Info>(selecfrm.vistpowerflag), typeof(PSP_PowerSubstation_Info));
                            GetDevice.gridControl1.DataSource = datatable;
                        }
                    }
                    else
                    {
                        if (selecfrm.getselecindex != 2)
                        {
                            if (shortflag)
                            {
                                GetDevice.strCon = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + ProjectSuid + "'" + selecfrm.Sqlcondition;
                            }
                            else
                                GetDevice.strCon = " where 1=1 " + selecfrm.Sqlcondition;
                            GetDevice.SelDevices();
                        }
                        else
                        {
                            if (GetDevice.GetType() == "01")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistbusflag), typeof(PSPDEV));
                                GetDevice.gridControl1.DataSource = datatable;
                            }
                            else if (GetDevice.GetType() == "05")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistlineflag), typeof(PSPDEV));
                                GetDevice.gridControl1.DataSource = datatable;
                            }
                            else if (GetDevice.GetType() == "02")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.visttrans2flag), typeof(PSPDEV));
                                GetDevice.gridControl1.DataSource = datatable;
                            }
                            else if (GetDevice.GetType() == "03")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.visttrans3flag), typeof(PSPDEV));
                                GetDevice.gridControl1.DataSource = datatable;
                            }
                            else if (GetDevice.GetType() == "04")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistfdjflag), typeof(PSPDEV));
                                GetDevice.gridControl1.DataSource = datatable;
                            }
                            else if (GetDevice.GetType() == "04")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistfdjflag), typeof(PSPDEV));
                                GetDevice.gridControl1.DataSource = datatable;
                            }
                            else if (GetDevice.GetType() == "06")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistdlqflag), typeof(PSPDEV));
                                GetDevice.gridControl1.DataSource = datatable;
                            }
                            else if (GetDevice.GetType() == "09")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistbldrflag), typeof(PSPDEV));
                                GetDevice.gridControl1.DataSource = datatable;
                            }
                            else if (GetDevice.GetType() == "10")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistcldkflag), typeof(PSPDEV));
                                GetDevice.gridControl1.DataSource = datatable;
                            }
                            else if (GetDevice.GetType() == "11")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistbldkflag), typeof(PSPDEV));
                                GetDevice.gridControl1.DataSource = datatable;
                            }
                            else if (GetDevice.GetType() == "12")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistfhflag), typeof(PSPDEV));
                                GetDevice.gridControl1.DataSource = datatable;
                            }
                            else if (GetDevice.GetType() == "13")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistmlflag), typeof(PSPDEV));
                                GetDevice.gridControl1.DataSource = datatable;
                            }
                            else if (GetDevice.GetType() == "14")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistml2flag), typeof(PSPDEV));
                                GetDevice.gridControl1.DataSource = datatable;
                            }
                        }

                    }

                }
            }

        }
        private List<T> convertdirecttolist<T>(Dictionary<string, T> col)
        {
            List<T> ss = new List<T>();
            foreach (KeyValuePair<string, T> keyvalue in col)
            {
                ss.Add(keyvalue.Value);
            }
            return ss;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (GetDevice == null)
            {
                MessageBox.Show("请选择设备种类。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            if (GetDevice.GetType() == "20")
            {

                frmBDZdlg dlg = new frmBDZdlg();
                //dlg.DeviceMx = dev as PSP_Substation_Info;
                dlg.IsRead = false;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.DeviceMx.AreaID = MIS.ProgUID;                    
                    UCDeviceBase.DataService.Create<PSP_Substation_Info>(dlg.DeviceMx);
                    devic.Add("id", dlg.DeviceMx.UID);
                    devic.Add("name", dlg.DeviceMx.Title);
                    devic.Add("device", dlg.DeviceMx);
                }
            }

            if (GetDevice.GetType() == "30")
            {
                frmDYdlg dlg21 = new frmDYdlg();
                //dlg21.DeviceMx = dev as PSP_PowerSubstation_Info;
                dlg21.IsRead = false;
                if (dlg21.ShowDialog() == DialogResult.OK)
                {
                    dlg21.DeviceMx.AreaID = MIS.ProgUID;
                    UCDeviceBase.DataService.Create<PSP_PowerSubstation_Info>(dlg21.DeviceMx);
                    devic.Add("id", dlg21.DeviceMx.UID);
                    devic.Add("name", dlg21.DeviceMx.Title);
                    devic.Add("device", dlg21.DeviceMx);
                }
            }
            if (GetDevice.GetType() == "05")
            {
                frmXLdlg dlg5 = new frmXLdlg();
                //dlg5.DeviceMx = dev as PSPDEV;
                if (dlg5.ShowDialog() == DialogResult.OK)
                {
                    dlg5.DeviceMx.ProjectID = MIS.ProgUID;
                    UCDeviceBase.DataService.Create<PSPDEV>(dlg5.DeviceMx);
                    devic.Add("id", dlg5.DeviceMx.SUID);
                    devic.Add("name", dlg5.DeviceMx.Name);
                    devic.Add("device", dlg5.DeviceMx);
                    frmDS fd = new frmDS();
                    fd.ProjectSUID = ProjectID;
                    fd.InitData();               
                    if (fd.ShowDialog() == DialogResult.OK)
                    {
                        PSP_ElcDevice pe = new PSP_ElcDevice();
                        pe.ProjectSUID = (string)fd.PJ;
                        pe.DeviceSUID = dlg5.DeviceMx.SUID;
                        UCDeviceBase.DataService.Create<PSP_ElcDevice>(pe);
                       
                    }
                    
                }
            }
            if (GetDevice.GetType()=="70")
            {
                frmZXdlg dlg = new frmZXdlg();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.DeviceMx.ProjectID = MIS.ProgUID;               
                    UCDeviceBase.DataService.Create<PSPDEV>(dlg.DeviceMx);
                    devic.Add("id", dlg.DeviceMx.SUID);
                    devic.Add("name", dlg.DeviceMx.Name);
                    devic.Add("device", dlg.DeviceMx);
                }
            }
            if (GetDevice.GetType() == "71")
            {
                frmRDQdlg dlg = new frmRDQdlg();
                
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.DeviceMx.ProjectID = MIS.ProgUID;
                    UCDeviceBase.DataService.Create<PSPDEV>(dlg.DeviceMx);
                    devic.Add("id", dlg.DeviceMx.SUID);
                    devic.Add("name", dlg.DeviceMx.Name);
                    devic.Add("device", dlg.DeviceMx);
                }
            }
            if (GetDevice.GetType() == "72")
            {
                frmBYQTWOdlg dlg = new frmBYQTWOdlg();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.DeviceMx.ProjectID = MIS.ProgUID;
                    UCDeviceBase.DataService.Create<PSPDEV>(dlg.DeviceMx);
                    devic.Add("id", dlg.DeviceMx.SUID);
                    devic.Add("name", dlg.DeviceMx.Name);
                    devic.Add("device", dlg.DeviceMx);
                }
            }
            if (GetDevice.GetType() == "50")
            {
                frmPWdlg dlg = new frmPWdlg();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.DeviceMx.ProjectID = MIS.ProgUID;
                    dlg.DeviceMx.Type = "50";
                    UCDeviceBase.DataService.Create<PSPDEV>(dlg.DeviceMx);
                    devic.Add("id", dlg.DeviceMx.SUID);
                    devic.Add("name", dlg.DeviceMx.Name);
                    devic.Add("device", dlg.DeviceMx);
                }
            }
            if (GetDevice.GetType() == "51")
            {
                frmPWdlg dlg = new frmPWdlg();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.DeviceMx.ProjectID = MIS.ProgUID;
                    dlg.DeviceMx.Type = "51";
                    UCDeviceBase.DataService.Create<PSPDEV>(dlg.DeviceMx);
                    devic.Add("id", dlg.DeviceMx.SUID);
                    devic.Add("name", dlg.DeviceMx.Name);
                    devic.Add("device", dlg.DeviceMx);
                }
            }
            if (GetDevice.GetType() == "52")
            {
                frmPWdlg dlg = new frmPWdlg();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.DeviceMx.ProjectID = MIS.ProgUID;
                    dlg.DeviceMx.Type = "52";
                    UCDeviceBase.DataService.Create<PSPDEV>(dlg.DeviceMx);
                    devic.Add("id", dlg.DeviceMx.SUID);
                    devic.Add("name", dlg.DeviceMx.Name);
                    devic.Add("device", dlg.DeviceMx);
                }
            }
            if (GetDevice.GetType() == "55")
            {
                frmPWKGdlg dlg = new frmPWKGdlg();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.DeviceMx.ProjectID = MIS.ProgUID;
                    dlg.DeviceMx.Type = "55";
                    UCDeviceBase.DataService.Create<PSPDEV>(dlg.DeviceMx);
                    devic.Add("id", dlg.DeviceMx.SUID);
                    devic.Add("name", dlg.DeviceMx.Name);
                    devic.Add("device", dlg.DeviceMx);
                }
            }
            if (GetDevice.GetType() == "56")
            {
                frmPWKGdlg dlg = new frmPWKGdlg();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.DeviceMx.ProjectID = MIS.ProgUID;
                    dlg.DeviceMx.Type = "56";
                    UCDeviceBase.DataService.Create<PSPDEV>(dlg.DeviceMx);
                    devic.Add("id", dlg.DeviceMx.SUID);
                    devic.Add("name", dlg.DeviceMx.Name);
                    devic.Add("device", dlg.DeviceMx);
                }
            }
            if (GetDevice.GetType() == "57")
            {
                frmPWKGdlg dlg = new frmPWKGdlg();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.DeviceMx.ProjectID = MIS.ProgUID;
                    dlg.DeviceMx.Type = "57";
                    UCDeviceBase.DataService.Create<PSPDEV>(dlg.DeviceMx);
                    devic.Add("id", dlg.DeviceMx.SUID);
                    devic.Add("name", dlg.DeviceMx.Name);
                    devic.Add("device", dlg.DeviceMx);
                }
            }
            if (GetDevice.GetType() == "58")
            {
                frmPWKGdlg dlg = new frmPWKGdlg();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.DeviceMx.ProjectID = MIS.ProgUID;
                    dlg.DeviceMx.Type = "58";
                    UCDeviceBase.DataService.Create<PSPDEV>(dlg.DeviceMx);
                    devic.Add("id", dlg.DeviceMx.SUID);
                    devic.Add("name", dlg.DeviceMx.Name);
                    devic.Add("device", dlg.DeviceMx);
                }
            }
            if (GetDevice.GetType() == "59")
            {
                frmPWKGdlg dlg = new frmPWKGdlg();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.DeviceMx.ProjectID = MIS.ProgUID;
                    dlg.DeviceMx.Type = "59";
                    UCDeviceBase.DataService.Create<PSPDEV>(dlg.DeviceMx);
                    devic.Add("id", dlg.DeviceMx.SUID);
                    devic.Add("name", dlg.DeviceMx.Name);
                    devic.Add("device", dlg.DeviceMx);
                }
            }
            if (GetDevice.GetType() == "61")
            {
                frmPWKGdlg dlg = new frmPWKGdlg();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.DeviceMx.ProjectID = MIS.ProgUID;
                    dlg.DeviceMx.Type = "61";
                    UCDeviceBase.DataService.Create<PSPDEV>(dlg.DeviceMx);
                    devic.Add("id", dlg.DeviceMx.SUID);
                    devic.Add("name", dlg.DeviceMx.Name);
                    devic.Add("device", dlg.DeviceMx);
                }
            }
            if (GetDevice.GetType() == "62")
            {
                frmPWKGdlg dlg = new frmPWKGdlg();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.DeviceMx.ProjectID = MIS.ProgUID;
                    dlg.DeviceMx.Type = "62";
                    UCDeviceBase.DataService.Create<PSPDEV>(dlg.DeviceMx);
                    devic.Add("id", dlg.DeviceMx.SUID);
                    devic.Add("name", dlg.DeviceMx.Name);
                    devic.Add("device", dlg.DeviceMx);
                }
            }
            if (GetDevice.GetType() == "63")
            {
                frmPWKGdlg dlg = new frmPWKGdlg();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.DeviceMx.ProjectID = MIS.ProgUID;
                    dlg.DeviceMx.Type = "63";
                    UCDeviceBase.DataService.Create<PSPDEV>(dlg.DeviceMx);
                    devic.Add("id", dlg.DeviceMx.SUID);
                    devic.Add("name", dlg.DeviceMx.Name);
                    devic.Add("device", dlg.DeviceMx);
                }
            }
            if (GetDevice.GetType() == "64")
            {
                frmPWKGdlg dlg = new frmPWKGdlg();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.DeviceMx.ProjectID = MIS.ProgUID;
                    dlg.DeviceMx.Type = "64";
                    UCDeviceBase.DataService.Create<PSPDEV>(dlg.DeviceMx);
                    devic.Add("id", dlg.DeviceMx.SUID);
                    devic.Add("name", dlg.DeviceMx.Name);
                    devic.Add("device", dlg.DeviceMx);
                }
            }
            if (GetDevice.GetType() == "74")
            {
                frmFZXdlg dlg = new frmFZXdlg();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.DeviceMx.ProjectID = MIS.ProgUID;
                    dlg.DeviceMx.Type = "74";
                    UCDeviceBase.DataService.Create<PSPDEV>(dlg.DeviceMx);
                    devic.Add("id", dlg.DeviceMx.SUID);
                    devic.Add("name", dlg.DeviceMx.Name);
                    devic.Add("device", dlg.DeviceMx);
                }
            }
            if (GetDevice.GetType() == "73")
            {
                frmDXdlg dlg = new frmDXdlg();
                dlg.ProjectSUID = this.ProjectID;
                dlg.Name = "";
                PSPDEV p = new PSPDEV();
                p.ProjectID = this.ProjectID;
                dlg.DeviceMx = p;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.DeviceMx.ProjectID = MIS.ProgUID;
                    dlg.DeviceMx.Type = "73";
                  
                    UCDeviceBase.DataService.Create<PSPDEV>(dlg.DeviceMx);
                    devic.Add("id", dlg.DeviceMx.SUID);
                    devic.Add("name", dlg.DeviceMx.Name);
                    devic.Add("device", dlg.DeviceMx);
                }
            }  
            if (GetDevice.GetType() == "75")
            {
                frmLUXdlg dlg = new frmLUXdlg();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.DeviceMx.ProjectID = MIS.ProgUID;
                    dlg.DeviceMx.Type = "75";
                    UCDeviceBase.DataService.Create<PSPDEV>(dlg.DeviceMx);
                    devic.Add("id", dlg.DeviceMx.SUID);
                    devic.Add("name", dlg.DeviceMx.Name);                             
                    devic.Add("device", dlg.DeviceMx);     
                }
            }
            if (device!=null&&devic.Count>0)
            {
                this.DialogResult = DialogResult.OK;
            } 
            else
            {
                this.DialogResult = DialogResult.Cancel;
                
            }          
        }
    }
}