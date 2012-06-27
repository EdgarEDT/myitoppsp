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
using Itop.Client.Common;

namespace Itop.TLPSP.DEVICE
{
    /// <summary>
    /// 设备管理
    /// </summary>
    public partial class frmProjectManager_children : Itop.Client.Base.FormModuleBase
    {
        public frmProjectManager_children()
        {
            InitializeComponent();
            
        }
        #region 初始化
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            bar.AddItems(new DevExpress.XtraBars.BarItem[] { barButtonItemIn, barButtonItemOut, barDeleteDevice,UpdateNumber, barButtonItemDel, AllDele, barImportPsasp, bardevicetemplate, barButtonItemclose });
            
            barQuery.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barButtonItemDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //treeList1.FocusedNode = null;
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
        private void InitDeviceType(string[] type)
        {
            DeviceTypeHelper.InitDeviceTypes(treeList1,type);
        }
        private PSP_ELCPROJECT psp_proj;
        public PSP_ELCPROJECT Psp_proj
        {
            get { return psp_proj; }
            set { psp_proj = value; }
        }
        private object _parentobj;
        public object ParentObj
        {
            get { return _parentobj; }
            set {if (value!=null)
                {
                    if (value is PSP_Substation_Info)
                    {
                        parentid = (value as PSP_Substation_Info).UID;
                    }
                    else if (value is PSP_PowerSubstation_Info)
                    {
                        parentid = (value as PSP_PowerSubstation_Info).UID;
                    }
                    else
                    {
                        parentid = (value as PSPDEV).SUID;
                    }
                    _parentobj = value;
                }
            }
        }
        private string parentid;
        //父设备下的子设备维护操作 例如 母线 两绕组变压器 发电机等设备
        public void childrendevice(string[] type)
        {
            
            this.Text = "参数管理";
            this.WindowState = FormWindowState.Maximized;
            if (!string.IsNullOrEmpty(this.smmprog.ProgName))
                this.Text = this.smmprog.ProgName;
            InitDeviceType(type);
            //splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            treeList1.FocusedNode = null;
            barButtonItemclose.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barButtonItemIn.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barButtonItemOut.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barImportPsasp.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
          
            this.ShowDialog();
            
        }
        //边电站入口
         public void bdzdevice()
        {
            this.Show();
             this.Text = "变电站参数管理";
            this.WindowState = FormWindowState.Maximized;
            if(!string.IsNullOrEmpty(this.smmprog.ProgName))
            this.Text = this.smmprog.ProgName;
             string[] type=new string[]{"20"};
             InitDeviceType(type);
             splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            
             
        }
        //电源入口
        public void dydevice()
        {
            this.Show();
            this.Text = "电源参数管理";
            this.WindowState = FormWindowState.Maximized;
            if (!string.IsNullOrEmpty(this.smmprog.ProgName))
                this.Text = this.smmprog.ProgName;
            string[] type = new string[] { "30" };
            InitDeviceType(type);
            splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
        }
        //线路入口
        public void xldevice()
        {
            this.Show();
            this.Text = "电源参数管理";
            this.WindowState = FormWindowState.Maximized;
            if (!string.IsNullOrEmpty(this.smmprog.ProgName))
                this.Text = this.smmprog.ProgName;
            string[] type = new string[] { "05" };
            InitDeviceType(type);
            splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
        }
        //无功设备入口
        public void wgdevice()
        {
            this.Show();
            this.Text = "无功参数管理";
            this.WindowState = FormWindowState.Maximized;
            if (!string.IsNullOrEmpty(this.smmprog.ProgName))
                this.Text = this.smmprog.ProgName;
            string[] type = new string[] { "06","08","09","10","11","13","14","15","40" };
            InitDeviceType(type);
            //splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            treeList1.FocusedNode = null;
        }
        //配电设备入口
        public void pddevice()
        {
            this.Show();
            this.Text = "电源参数管理";
            this.WindowState = FormWindowState.Maximized;
            if (!string.IsNullOrEmpty(this.smmprog.ProgName))
                this.Text = this.smmprog.ProgName;
            string[] type = new string[] { "73","55","74","75","80","70"};
            InitDeviceType(type);
            //splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            treeList1.FocusedNode = null;
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

        #region 记录操作
        protected override void Add() {
            if (curDevice != null)
            {
                curDevice.ParentID = parentid;
                curDevice.ParentObj = ParentObj;
                curDevice.Add();
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
            base.Print();
        }
        
        #endregion

        #region 字段
        UCDeviceBase curDevice;
        Dictionary<string, UCDeviceBase> devicTypes = new Dictionary<string, UCDeviceBase>();

        #endregion


        private void treeList1_MouseClick(object sender, MouseEventArgs e) {
            //if (e.Button == MouseButtons.Right) return;
            //TreeListNode node = treeList1.FocusedNode;
            //if (node == null) return;
            //string strID = node["id"].ToString();   
            //string dtype = node["class"].ToString();
            //if (string.IsNullOrEmpty(dtype))
            //{
            //    if (curDevice != null)
            //    {
            //        curDevice.Hide();
            //    }
            //    return;
            //}
          
            //UCDeviceBase device = null;
            //if (devicTypes.ContainsKey(dtype))
            //{
            //    device = devicTypes[dtype];
            //    device.ID = strID;
            //    try
            //    {
            //        device.Show();
            //    }
            //    catch { }
            //}
            //else
            //{
            //    device = createInstance(dtype);
            //    device.ID = strID;
            //    device.ProjectID = Itop.Client.MIS.ProgUID;
            //    devicTypes.Add(dtype, device);
            //    showDevice(device);
            //}

            //if (curDevice != null && curDevice != device) curDevice.Hide();
            //curDevice = device;
            //if (curDevice != null)
            //{
            //    curDevice.strCon = " where 1=1 and";
            //    curDevice.Init();
            //}
            
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
            if (this.treeList1.FocusedNode != null && curDevice!=null)
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
            if (curDevice!=null)
            {
                curDevice.Alldel();
            }
            
        }
        //排除设备
        void barDeleteDevice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (psp_proj.ID == null || curDevice == null)
            {
                MessageBox.Show("选择有问题！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (curDevice.SelectedDevice != null)
            {
                if (curDevice.SelectedDevice.GetType() == typeof(PSPDEV))
                {
                    PSPDEV dev = curDevice.SelectedDevice as PSPDEV;
                    PSP_ElcDevice elcdevice = new PSP_ElcDevice();
                    elcdevice.ProjectSUID = psp_proj.ID;
                    elcdevice.DeviceSUID = dev.SUID;
                    Services.BaseService.Delete<PSP_ElcDevice>(elcdevice);

                }
                else if (curDevice.SelectedDevice.GetType() == typeof(PSP_Substation_Info))
                {
                    PSP_Substation_Info dev = curDevice.SelectedDevice as PSP_Substation_Info;
                    PSP_ElcDevice elcdevice = new PSP_ElcDevice();
                    elcdevice.ProjectSUID = psp_proj.ID;
                    elcdevice.DeviceSUID = dev.UID;
                    Services.BaseService.Delete<PSP_ElcDevice>(elcdevice);
                }
                else if (curDevice.SelectedDevice.GetType() == typeof(PSP_PowerSubstation_Info))
                {
                    PSP_PowerSubstation_Info dev = curDevice.SelectedDevice as PSP_PowerSubstation_Info;
                    PSP_ElcDevice elcdevice = new PSP_ElcDevice();
                    elcdevice.ProjectSUID = psp_proj.ID;
                    elcdevice.DeviceSUID = dev.UID;
                    Services.BaseService.Delete<PSP_ElcDevice>(elcdevice);
                }
            }


            if (curDevice != null)
            {
                if (curDevice.GetClassName() == "PSP_Substation_Info")
                {
                    curDevice.proInit(psp_proj.ID);
                }
                else if (curDevice.GetClassName() == "PSP_PowerSubstation_Info")
                {
                    curDevice.proInit(psp_proj.ID);
                }
                else if (curDevice.GetClassName() == "PSPDEV")
                {
                    if (ParentObj is PSP_Substation_Info)
                    {
                        curDevice.strCon = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + psp_proj.ID + "' and pspdev.SvgUID='" + ((PSP_Substation_Info)ParentObj).UID + "'and";
                    }
                    else if (ParentObj is PSP_PowerSubstation_Info)
                    {
                        curDevice.strCon = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + psp_proj.ID + "' and pspdev.SvgUID='" + ((PSP_PowerSubstation_Info)ParentObj).UID + "'and"; ;
                    }
                    else if (ParentObj is PSPDEV)
                    {
                        curDevice.strCon = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + psp_proj.ID + "' and pspdev.SvgUID='" + ((PSPDEV)ParentObj).SUID + "'and"; ;
                    }
                    curDevice.Init();
                }
            }
        }
        private void UpdateNumber_ItemClick(object sender,DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (curDevice!=null)
            {
                curDevice.projectdeviceid = psp_proj.ID;
                curDevice.updatenumberflag = false;
                curDevice.UpdateNumber();
            }
          
        }

        private void barImportPsasp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            frmImport dlg = new frmImport();
            dlg.Show(this);
            
        }
        private void bardevicetemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDeviceTemplateManager dlg = new frmDeviceTemplateManager();
            dlg.Show(this);

        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            TreeListNode node = treeList1.FocusedNode;
            if (node == null) return;
            string strID = node["id"].ToString();
            string dtype = node["class"].ToString();
            if (string.IsNullOrEmpty(dtype))
            {
                if (curDevice != null)
                {
                    curDevice.Hide();
                }
                return;
            }

            UCDeviceBase device = null;
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
                device.ProjectID = Itop.Client.MIS.ProgUID;
                devicTypes.Add(dtype, device);
                showDevice(device);
            }

            if (curDevice != null && curDevice != device) curDevice.Hide();
            curDevice = device;
            if (curDevice != null)
            {
                if (!string.IsNullOrEmpty(parentid))
                {
                    if (ParentObj is PSP_Substation_Info)
                    {
                        curDevice.strCon = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" +psp_proj.ID+ "' and pspdev.SvgUID='" + ((PSP_Substation_Info)ParentObj).UID + "'and";
                    }
                    else if (ParentObj is PSP_PowerSubstation_Info)
                    {
                        curDevice.strCon = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + psp_proj.ID + "' and pspdev.SvgUID='" + ((PSP_PowerSubstation_Info)ParentObj).UID + "'and"; ;
                    }
                    else if (ParentObj is PSPDEV)
                    {
                        curDevice.strCon = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + psp_proj.ID + "' and pspdev.SvgUID='" + ((PSPDEV)ParentObj).SUID + "'and"; ;
                    }
                    curDevice.Init();
                 
                }
                //else
                //{
                //    curDevice.strCon = " where 1=1 and";
                //    curDevice.Init();
                //}
                
            }
        }
    }
}