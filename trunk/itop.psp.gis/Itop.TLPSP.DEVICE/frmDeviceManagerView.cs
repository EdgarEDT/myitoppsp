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

namespace Itop.TLPSP.DEVICE
{
    /// <summary>
    /// 设备管理
    /// </summary>
    public partial class frmDeviceManagerView : Itop.Client.Base.FormModuleBase
    {
        public frmDeviceManagerView()
        {
            InitializeComponent();
            Init();
            InitDeviceType();
        }
        #region 初始化
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
           // bar.AddItems(new DevExpress.XtraBars.BarItem[] { barButtonItemclose });
            
            barQuery.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barPrint.Caption = "导出";
            barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barAdd.Caption = "查询";
            barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
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
 
        #endregion

        #region 私有方法
        /// <summary>
        /// 实例化类接口
        /// </summary>
        /// <param name="classname"></param>
        /// <returns></returns>
        private UCDeviceBase createInstance(string classname) {
            return Assembly.GetExecutingAssembly().CreateInstance(classname, false) as UCDeviceBase;
        }
        private void showDevice(UCDeviceBase device) {
            if (device == null) return;
            device.Dock = DockStyle.Fill;
            splitContainerControl1.Panel2.Controls.Add(device);
        }
        #endregion
        private List<T> convertdirecttolist<T>(Dictionary<string, T> col)
        {
            List<T> ss = new List<T>();
            foreach (KeyValuePair<string, T> keyvalue in col)
            {
                ss.Add(keyvalue.Value);
            }
            return ss;
        }
        #region 记录操作
        protected override void Add() {
            //if (curDevice != null)
            //{

            //}
            TreeListNode node = treeList1.FocusedNode;
            if (node == null)
            {
                MessageBox.Show("请选择设备类型以后再进行检索", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                XtraSelectfrm selecfrm = new XtraSelectfrm();
                selecfrm.type = curDevice.GetType();
                selecfrm.ShowDialog();
                if (selecfrm.DialogResult == DialogResult.OK)
                {
                    if (curDevice is UCDeviceBDZ)
                    {
                        if (selecfrm.getselecindex == 0)
                        {
                            curDevice.strCon = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND Title like'%" + selecfrm.DeviceName + "%'";
                            curDevice.SelDevices();
                        }
                        else if (selecfrm.getselecindex == 1)
                        {
                            curDevice.strCon = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND L1 ='" + selecfrm.devicevolt + "'";
                            curDevice.SelDevices();
                        }
                        else if (selecfrm.getselecindex == 2)
                        {
                            DataTable datatable = new DataTable();
                            datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSP_Substation_Info>(selecfrm.vistsubstationflag), typeof(PSP_Substation_Info));
                            curDevice.gridControl1.DataSource = datatable;
                        }

                    }
                    else if (curDevice is UCDeviceDY)
                    {
                        if (selecfrm.getselecindex == 0)
                        {
                            curDevice.strCon = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND Title like'%" + selecfrm.DeviceName + "%'";
                            curDevice.SelDevices();
                        }
                        else if (selecfrm.getselecindex == 1)
                        {
                            curDevice.strCon = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND S1 ='" + selecfrm.devicevolt + "'";
                            curDevice.SelDevices();
                        }
                        else if (selecfrm.getselecindex == 2)
                        {
                            DataTable datatable = new DataTable();
                            datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSP_PowerSubstation_Info>(selecfrm.vistpowerflag), typeof(PSP_PowerSubstation_Info));
                            curDevice.gridControl1.DataSource = datatable;
                        }
                    }
                    else
                    {
                        if (selecfrm.getselecindex != 2)
                        {
                            //if (shortflag)
                            //{
                            //    GetDevice.strCon = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + ProjectSuid + "'" + selecfrm.Sqlcondition;
                            //}
                            //else
                            curDevice.strCon = " where 1=1 " + selecfrm.Sqlcondition;
                            curDevice.SelDevices();
                        }
                        else
                        {
                            if (curDevice.GetType() == "01")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistbusflag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "05")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistlineflag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "02")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.visttrans2flag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "03")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.visttrans3flag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "04")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistfdjflag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "04")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistfdjflag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "06")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistdlqflag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "09")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistbldrflag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "10")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistcldkflag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "11")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistbldkflag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "12")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistfhflag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "13")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistmlflag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "14")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistml2flag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                        }

                    }

                }
            }
        }
        protected override void Edit() {
            if (curDevice != null)
                curDevice.Edit();
        }
        protected override void Del() {
            if (curDevice != null)
                curDevice.Delete();
        }
        protected override void Print() {
            try
            {
                DeviceHelper.ExportToExcelOld(curDevice.gridControl1, "", "");
            }
            catch { }
        }
        
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
                try {
                    device.Show();
                } catch { }
            }else
            {
                device = createInstance(dtype);
                device.ProjectID = Itop.Client.MIS.ProgUID;
                devicTypes.Add(dtype, device);
                showDevice(device);
            }
            
            if (curDevice != null　&& curDevice!=device) curDevice.Hide();
            curDevice = device;
            if (curDevice != null)
            {
                curDevice.ID = strID;
                curDevice.strCon = " where 1=1 and";
                curDevice.Init();
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
            Del();
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
            curDevice.UpdateNumber();
        }

        private void barImportPsasp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            frmImport dlg = new frmImport();
            dlg.Show(this);
            
        }
    }
}