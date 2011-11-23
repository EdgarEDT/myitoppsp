using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Xml;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using Itop.Domain.Graphics;
using Itop.TLPSP.DEVICE.Mysql;
using Itop.TLPSP.DEVICE;
using System.Collections;

namespace ItopVector.Tools
{
    /// <summary>
    /// 设备管理
    /// </summary>
    public partial class frmRelationDevice : Itop.Client.Base.FormBase
    {
        public frmRelationDevice() {
            InitializeComponent();
            //Init();
            //InitDeviceType();
        }
        private IList deviceList=null;
        public IList DeviceList
        {
            get
            {
                return deviceList;
            }
            set
            {
                deviceList = value;
            }
        }
        private string UCType = "";
        #region 初始化
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            //bar.AddItems(new DevExpress.XtraBars.BarItem[] { barButtonItemIn, barButtonItemOut,UpdateNumber, barButtonItemDel, AllDele, barButtonItemclose,barImportPsasp });


            //barQuery.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //barClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            treeList1.FocusedNode = null;
        }
        protected void Init() {
            this.Text = "设备参数管理";
            this.WindowState = FormWindowState.Maximized;
            if(!string.IsNullOrEmpty(this.smmprog.ProgName))
            this.Text = this.smmprog.ProgName;
        }
        /// <summary>
        /// 初始设备分类
        /// </summary>
        private void InitDeviceType() {
            DeviceTypeHelper.InitDeviceTypes(treeList1);
        }
        public void InitDeviceType(params string[] type)
        {
            if (type.Length == 0)
            {
                InitDeviceType();
                return;
            }
            ArrayList list = new ArrayList();
            list.AddRange(type);
            UCType = type[0];
            Stream fs = Assembly.Load("Itop.TLPSP.DEVICE").GetManifestResourceStream("Itop.TLPSP.DEVICE.devicetypes.xml");
            //Stream fs = Assembly.GetExecutingAssembly().GetManifestResourceStream("Itop.TLPSP.DEVICE.devicetypes.xml");
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
                if (stype == "05")
                {
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
        private UCDeviceBase createInstance(string classname) {
            return Assembly.Load("Itop.TLPSP.DEVICE").CreateInstance(classname, false) as UCDeviceBase;
        }
        private void showDevice(UCDeviceBase device) {
            if (device == null) return;
            device.Dock = DockStyle.Fill;
            splitContainerControl1.Panel2.Controls.Add(device);
        }
        #endregion

        #region 记录操作
        //protected override void Add() {
        //    if (curDevice != null)
        //        curDevice.Add();
        //}
        //protected override void Edit() {
        //    if (curDevice != null)
        //        curDevice.Edit();
        //}
        //protected override void Del() {
        //    if (curDevice != null)
        //        curDevice.Delete();
        //}
        //protected override void Print() {
        //    base.Print();
        //}
        
        #endregion

        #region 字段
        UCDeviceBase curDevice;
        Dictionary<string, UCDeviceBase> devicTypes = new Dictionary<string, UCDeviceBase>();

        #endregion

        private void treeList1_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) return;
            TreeListNode node = treeList1.FocusedNode;
            if (node == null) return;
            string strID = node["id"].ToString();   
            string dtype = node["class"].ToString();
            
            UCDeviceBase device = null;
            if (devicTypes.ContainsKey(dtype)) {
                device = devicTypes[dtype];
                device.ID = strID;
                try {
                    device.Show();
                } catch { }
            }else
            {
                device = createInstance(dtype);
                device.ID = strID;
                device.ProjectID = Itop.Client.MIS.ProgUID;
                devicTypes.Add(dtype, device);
                showDevice(device);
            }
            
            if (curDevice != null　&& curDevice!=device) curDevice.Hide();
            curDevice = device;
            if (curDevice != null)
            {
                IList listTemp = new List<PSPDEV>();
                foreach (PSPDEV dev in deviceList)
                {
                    if (dev.Type == strID)
                    {
                        listTemp.Add(dev);
                    }
                }
                curDevice.Init(listTemp);
            }
        }

        private void barButtonItemOut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DeviceHelper.ExportToExcelOld(curDevice.gridControl1, "", "");
            }
            catch { }
        }
        
        private void barButtonItemIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.treeList1.FocusedNode!=null)
            {
                IList<string> filedList = new List<string>();
                IList<string> capList = new List<string>();
                for (int i = 0; i < curDevice.gridView1.Columns.Count; i++)
                {
                    capList.Add(curDevice.gridView1.Columns[i].Caption);
                    filedList.Add(curDevice.gridView1.Columns[i].FieldName);
                }
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Excel文件(*.xls)|*.xls";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        DataTable table = DeviceHelper.GetExcel(op.FileName, filedList, capList);
                        curDevice.UpdateIn(table);
                        
                    }
                    catch (Exception a) { MessageBox.Show(a.Message); }
                    curDevice.Init();
                }
            }
            else
                MessageBox.Show("没有选择设备（如果没有请添加一个）", "导入EXCEL", MessageBoxButtons.OK);
        }
        private void barButtonItemDel_ItemClick(object sender,DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Del();
        }
        private void barButtonItemclose_ItemClick(object sender,DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void AllDele_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            curDevice.Alldel();
        }
        private void UpdateNumber_ItemClick(object sender,DevExpress.XtraBars.ItemClickEventArgs e)
        {
            curDevice.updatenumberflag = true;
            curDevice.UpdateNumber();
        }

        private void barImportPsasp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            frmImport dlg = new frmImport();
            dlg.Show(this);
            
        }
    }
}