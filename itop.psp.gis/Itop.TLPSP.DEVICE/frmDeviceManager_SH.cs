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
using Itop.Domain.Table;
using Itop.Client.Table;
namespace Itop.TLPSP.DEVICE
{
    /// <summary>
    /// 设备管理
    /// </summary>
    public partial class frmDeviceManager_SH : Itop.Client.Base.FormModuleBase
    {
        public frmDeviceManager_SH()
        {
            InitializeComponent();
            
        }
        #region 初始化
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            bar.AddItems(new DevExpress.XtraBars.BarItem[] { barButtonItemIn, barButtonItemOut, UpdateNumber, barButtonItemDel, AllDele, barImportPsasp, bardevicetemplate, bdzStatic, xlStatic,dycopy, barButtonItemclose });
            
            barQuery.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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
            
            // bardevicetemplate.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
             bdzStatic.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
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
           // bardevicetemplate.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bdzStatic.Caption = "电源统计";
            bdzStatic.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            dycopy.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
             splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
        }
        //线路入口
        public void xldevice()
        {
            this.Show();
            this.Text = "线路参数管理";
            this.WindowState = FormWindowState.Maximized;
            if (!string.IsNullOrEmpty(this.smmprog.ProgName))
                this.Text = this.smmprog.ProgName;
            string[] type = new string[] { "05" };
            InitDeviceType(type);
            xlStatic.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
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
            bdzStatic.Caption = "无功参数统计";
            bdzStatic.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            treeList1.FocusedNode = null;
        }
        //配电设备入口
        public void pddevice()
        {
            this.Show();
            this.Text = "配电参数管理";
            this.WindowState = FormWindowState.Maximized;
            if (!string.IsNullOrEmpty(this.smmprog.ProgName))
                this.Text = this.smmprog.ProgName;
            string[] type = new string[] { "73","55","74","75","80","70"};
            InitDeviceType(type);
            bdzStatic.Caption = "配电参数统计";
            bdzStatic.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
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
                curDevice.Add();
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
        #endregion
        private void barButtonItemOut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (curDevice.GetType()=="20"||curDevice.GetType()=="30")
                {
                    DeviceHelper.ExportToExcelandchildren(curDevice.gridControl1,"","");
                }
                else
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
                    if (curDevice.gridView1.Columns[i].Caption.Contains("查看"))
                    {
                        continue;
                    }
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
        private void barButtonItemIn_ItemClick_sh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            if (this.treeList1.FocusedNode != null && curDevice != null)
            {
                IList<string> filedList = new List<string>();
                IList<string> capList = new List<string>();
                for (int i = 0; i < curDevice.gridView1.Columns.Count; i++)
                {
                    if (curDevice.gridView1.Columns[i].Caption.Contains("查看"))
                    {
                        continue;
                    }
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
                    DataTable dt = curDevice.gridControl1.DataSource as DataTable;
                    //如果是变电站
                   if (curDevice.GetType()=="20")
                   {
                       UCDeviceBase ub = new UCDeviceMX();
                       //母线
                       filedList = new List<string>();
                      capList = new List<string>();
                       for (int i = 0; i < ub.gridView1.Columns.Count; i++)
                       {

                           capList.Add(ub.gridView1.Columns[i].Caption);
                           filedList.Add(ub.gridView1.Columns[i].FieldName);
                       }
                       DataTable table = DeviceHelper.GetExcelmainandchildren(op.FileName, filedList, capList,"母线",dt);
                       ub.UpdateInchildren(table);
                       //两绕组变压器
                      ub = new UCDeviceBYQ2();
                     filedList = new List<string>();
                      capList = new List<string>();
                       for (int i = 0; i < ub.gridView1.Columns.Count; i++)
                       {

                           capList.Add(ub.gridView1.Columns[i].Caption);
                           filedList.Add(ub.gridView1.Columns[i].FieldName);
                       }
                      table = DeviceHelper.GetExcelmainandchildren(op.FileName, filedList, capList, "两绕组变压器", dt);
                       ub.UpdateInchildren(table);
                       //三绕组变压器
                       filedList = new List<string>();
                       capList = new List<string>();
                       ub = new UCDeviceBYQ3();

                       for (int i = 0; i < ub.gridView1.Columns.Count; i++)
                       {

                           capList.Add(ub.gridView1.Columns[i].Caption);
                           filedList.Add(ub.gridView1.Columns[i].FieldName);
                       }
                       table = DeviceHelper.GetExcelmainandchildren(op.FileName, filedList, capList, "三绕组变压器", dt);
                       ub.UpdateInchildren(table);
                       //负荷
                       ub = new UCDeviceFH();
                      filedList = new List<string>();
                      capList = new List<string>();
                       for (int i = 0; i < ub.gridView1.Columns.Count; i++)
                       {

                           capList.Add(ub.gridView1.Columns[i].Caption);
                           filedList.Add(ub.gridView1.Columns[i].FieldName);
                       }
                       table = DeviceHelper.GetExcelmainandchildren(op.FileName, filedList, capList, "负荷", dt);
                       ub.UpdateInchildren(table);
                   }
                   else if (curDevice.GetType() == "30")  //电源
                   {
                       UCDeviceBase ub = new UCDeviceMX();
                       //母线
                       filedList = new List<string>();
                      capList = new List<string>();
                       for (int i = 0; i < ub.gridView1.Columns.Count; i++)
                       {

                           capList.Add(ub.gridView1.Columns[i].Caption);
                           filedList.Add(ub.gridView1.Columns[i].FieldName);
                       }
                       DataTable table = DeviceHelper.GetExcelmainandchildren(op.FileName, filedList, capList, "母线", dt);
                       ub.UpdateInchildren(table);
                       //两绕组变压器
                       ub = new UCDeviceBYQ2();
                      filedList = new List<string>();
                       capList = new List<string>();
                       for (int i = 0; i < ub.gridView1.Columns.Count; i++)
                       {

                           capList.Add(ub.gridView1.Columns[i].Caption);
                           filedList.Add(ub.gridView1.Columns[i].FieldName);
                       }
                       table = DeviceHelper.GetExcelmainandchildren(op.FileName, filedList, capList, "两绕组变压器", dt);
                       ub.UpdateInchildren(table);
                      
                       //发电机
                       filedList = new List<string>();
                       capList = new List<string>();
                       ub = new UCDeviceFDJ();

                       for (int i = 0; i < ub.gridView1.Columns.Count; i++)
                       {

                           capList.Add(ub.gridView1.Columns[i].Caption);
                           filedList.Add(ub.gridView1.Columns[i].FieldName);
                       }
                       table = DeviceHelper.GetExcelmainandchildren(op.FileName, filedList, capList, "发电机", dt);
                       ub.UpdateInchildren(table);
                   }

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
        private void UpdateNumber_ItemClick(object sender,DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (curDevice!=null)
            {
                curDevice.updatenumberflag = true;
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
     
        private void bdzStatic_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmyearSel fs = new FrmyearSel();
            if (fs.ShowDialog()==DialogResult.OK)
            {
                if (curDevice is UCDeviceBDZ)
                {
                    FrmLayoutSubstationInfo_AHTL fa = new FrmLayoutSubstationInfo_AHTL();
                    fa.BiandianzhanSH(fs.Year);
                }
                else if (curDevice is UCDeviceDY)
                {
                    FrmLayoutPowerSubstationInfo_SH fa = new FrmLayoutPowerSubstationInfo_SH();
                    fa.BiandianzhanSH(fs.Year);
                }
                if (curDevice.GetType() != "20" && curDevice.GetType() != "30" && curDevice.GetType() != "15")
                {
                    string[] array=new string[]{curDevice.GetType()};
                    FrmDeviceStatic fsd = new FrmDeviceStatic();
                    fsd.Init(array, fs.Year);
                    fsd.ShowDialog();
                }
            }
        }
        private void xlStatic_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmyearSel fs = new FrmyearSel();
            if (fs.ShowDialog() == DialogResult.OK)
            {
                FrmLayoutLine fa = new FrmLayoutLine();
                fa.Linestatic(fs.Year);
            }

        }
        private void dycopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            object obj = curDevice.SelectedDevice;
            if (obj is PSP_PowerSubstation_Info)
            {
                FrmcopyDYtitle frmdy = new FrmcopyDYtitle();
                if (frmdy.ShowDialog()==DialogResult.OK)
                {
                    PSP_PowerSubstation_Info obj1 = new PSP_PowerSubstation_Info();
                    Itop.Common.DataConverter.CopyTo<PSP_PowerSubstation_Info>(obj as PSP_PowerSubstation_Info, obj1);
                    obj1.UID = Guid.NewGuid().ToString().Substring(24);
                    obj1.Title = frmdy.DYTitle;
                    UCDeviceBase.DataService.Create<PSP_PowerSubstation_Info>(obj1);
                    IList<PSPDEV> listmx = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", "where 1=1 and SvgUID='" + ((PSP_PowerSubstation_Info)obj).UID + "'and type='01'");
                   int number=0;
                    foreach (PSPDEV pd in listmx)
                   {
                        number++;
                       PSPDEV pv = new PSPDEV();
                       Itop.Common.DataConverter.CopyTo<PSPDEV>(pd, pv);
                       pv.SUID = Guid.NewGuid().ToString();
                       pv.Name = frmdy.DYTitle + "G" + number;
                       pv.SvgUID = obj1.UID;
                       UCDeviceBase.DataService.Create<PSPDEV>(pv);
                   }
                    number = 0;
                    IList<PSPDEV> listbyq = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", "where 1=1 and SvgUID='" + ((PSP_PowerSubstation_Info)obj).UID + "'and type='02'");
                    foreach (PSPDEV pd in listbyq)
                    {
                        number++;
                        PSPDEV pv = new PSPDEV();
                        Itop.Common.DataConverter.CopyTo<PSPDEV>(pd, pv);
                        pv.SUID = Guid.NewGuid().ToString();
                        pv.Name = frmdy.DYTitle + "-变压器" + number;
                        pv.SvgUID = obj1.UID;
                        pv.IName = "";
                        pv.JName = "";
                        UCDeviceBase.DataService.Create<PSPDEV>(pv);
                    }
                    number = 0;
                    IList<PSPDEV> listfdj = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", "where 1=1 and SvgUID='" + ((PSP_PowerSubstation_Info)obj).UID + "'and type='04'");
                    foreach (PSPDEV pd in listfdj)
                    {
                        number++;
                        PSPDEV pv = new PSPDEV();
                        Itop.Common.DataConverter.CopyTo<PSPDEV>(pd, pv);
                        pv.SUID = Guid.NewGuid().ToString();
                        pv.Name = frmdy.DYTitle + "-发电机" + number;
                        pv.SvgUID = obj1.UID;
                        pv.IName = "";
                        UCDeviceBase.DataService.Create<PSPDEV>(pv);
                    }
                }
               
            }
            curDevice.Init();
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
                curDevice.strCon = " where 1=1 and";
                curDevice.Init();
            }
        }
    }
}