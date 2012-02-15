using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Xml;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using Itop.Domain.Graphics;
using System.Collections;
using Itop.Client.Common;
using DevExpress.Utils;
namespace Itop.TLPSP.DEVICE {
    /// <summary>
    /// 项目管理
    /// </summary>
    public partial class frmProjectManager : Itop.Client.Base.FormModuleBase {
        DataTable datatable;
        protected string strID = null;
        public frmProjectManager() {
            InitializeComponent();
            Init();
        }
        public void SetMode(string faid) {
            splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;

            barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            strID = faid;
            if (curDevice != null) {
                curDevice.strCon = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID + "' and ";
                curDevice.Init();
                this.Text = ((PSP_ELCPROJECT)DeviceHelper.GetDevice<PSP_ELCPROJECT>(strID)).Name.ToString();
            } else {
                // MessageBox.Show("请先选择电气计算方案！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //splitContainerControl1.Panel2.Text = node["Name"].ToString();
        }
        #region 初始化
        public new string ProjectUID {
            get {
                return Itop.Client.MIS.ProgUID;
            }
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            //bar1.AddItem(barButtonItem1);  
            if (strID == null) {
                bar.AddItems(new DevExpress.XtraBars.BarItem[] { barSelectDevice, barDeleteDevice, barCopy, barButtonItem1, barButtonItem2, barButtonItem3, barButtonItem4, barORP, AllshortItem, RelcheckItem, jiaoliucheck, ZLcheck, barCheck, barUpdateNum });
                //bar.AddItem();
                barQuery.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                barORP.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            } else {
                bar.AddItem(barSelectDevice);
            }
            InitDeviceType();
            treeList1.FocusedNode = null;
            treeList2.FocusedNode = null;

        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            if (this.Text == "") {
                this.Text = "电气计算";
            }
        }
        protected void Init() {

            this.WindowState = FormWindowState.Maximized;
            this.Text = this.smmprog.ProgName;

        }
        /// <summary>
        /// 初始设备分类
        /// </summary>
        private void InitDeviceType() {
            TreeListColumn column = new TreeListColumn();
            column.Caption = "项目名称";
            column.FieldName = "Name";
            column.VisibleIndex = 0;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            PSP_ELCPROJECT pj = new PSP_ELCPROJECT();
            pj.ProjectID = Itop.Client.MIS.ProgUID;
            IList list = UCDeviceBase.DataService.GetList("SelectPSP_ELCPROJECTList", pj);
            datatable = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_ELCPROJECT));
            treeList1.DataSource = datatable;

           // DeviceTypeHelper.InitDeviceTypes(treeList2);
            DeviceTypeHelper.initprojectDeviceTypes(treeList2);
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
            splitContainerControl2.Panel2.Controls.Add(device);
        }
        #endregion

        #region 记录操作

        protected override void Add() {
            frmNewProject frmprojectDLG = new frmNewProject();
            frmprojectDLG.Name = "";
            if (frmprojectDLG.ShowDialog() == DialogResult.OK) {
                PSP_ELCPROJECT pd = new PSP_ELCPROJECT();
                pd.Name = frmprojectDLG.Name;
                pd.FileType = frmprojectDLG.FileType;
                pd.Class = System.DateTime.Now.ToString();
                pd.ProjectID = Itop.Client.MIS.ProgUID;
                UCDeviceBase.DataService.Create<PSP_ELCPROJECT>(pd);
                DataRow row = datatable.NewRow();
                Itop.Common.DataConverter.ObjectToRow(pd, row);
                datatable.Rows.Add(row);
            }
        }
        protected override void Edit() {
            TreeListNode node = treeList1.FocusedNode;
            if (node != null) {
                string id = node["ID"].ToString();
                PSP_ELCPROJECT pd = new PSP_ELCPROJECT();
                pd.ID = id;
                pd.Name = node["Name"].ToString();
                pd.Class = node["Class"].ToString();
                pd.FileType = node["FileType"].ToString();
                pd.ProjectID = node["ProjectID"].ToString();
                frmNewProject frmprojectDLG = new frmNewProject();
                frmprojectDLG.Name = pd.Name;
                frmprojectDLG.FileType = pd.FileType;
                if (frmprojectDLG.ShowDialog() == DialogResult.OK) {
                    node["Name"] = frmprojectDLG.Name;
                    pd.Name = frmprojectDLG.Name;
                    pd.FileType = frmprojectDLG.FileType;
                    UCDeviceBase.DataService.Update("UpdatePSP_ELCPROJECT", pd);
                    SVGFILE svgFile = new SVGFILE();
                    svgFile.SUID =pd.ID;
                    svgFile= (SVGFILE)Services.BaseService.GetObject("SelectSVGFILEByKey", svgFile);
                    if (svgFile!=null)
                    {
                        svgFile.FILENAME = pd.Name;
                        UCDeviceBase.DataService.Update<SVGFILE>(svgFile);

                    }

                    treeList1.Refresh();
                }
            }

        }
        protected override void Del() {
            TreeListNode node = treeList1.FocusedNode;
            if (node != null) {
                if (MessageBox.Show("确定删除么?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) {
                    string id = node["ID"].ToString();
                    treeList1.DeleteNode(treeList1.FocusedNode);
                    PSP_ELCPROJECT pd = new PSP_ELCPROJECT();
                    pd.ID = id;
                    UCDeviceBase.DataService.Delete<PSP_ELCPROJECT>(pd);
                    //删除和其相连的设备记录
                    string con = "where ProjectSUID = '"+pd.ID+"'";
                    IList list = Services.BaseService.GetList("SelectPSP_ElcDeviceByCondition", con);
                    foreach (PSP_ElcDevice pe in list)
                    {
                        UCDeviceBase.DataService.Delete<PSP_ElcDevice>(pe);
                    }
                    //删除和其相关的图形记录
                    SVGFILE svgFile = new SVGFILE();
                    svgFile.SUID = pd.ID;
                    svgFile = (SVGFILE)Services.BaseService.GetObject("SelectSVGFILEByKey", svgFile);
                    if (svgFile!=null)
                    {
                        UCDeviceBase.DataService.Delete<SVGFILE>(svgFile);
                    }
                    treeList1.FocusedNode = null;
                    strID = null;
                }
            }
        }
        protected override void Print() {
            base.Print();
        }
        protected override void Query() {
            ElectricLoadCal elc = new ElectricLoadCal();
            TreeListNode node = treeList1.FocusedNode;
            if (node == null) {
                MessageBox.Show("请先选择电气计算方案！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            strID = node["ID"].ToString();
            elc.LFC(strID, 1, 100);
            // base.Query();
        }
        void barSelectDevice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TreeListNode node;
            if (strID == null) {
                MessageBox.Show("请先选择电气计算方案！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //strID = node["ID"].ToString();
            DataTable dt = new DataTable();
            frmDeviceList frmDevList = new frmDeviceList();
            frmDevList.ProjectID = this.ProjectUID;
            frmDevList.ProjectSUID = strID;
            node = treeList2.FocusedNode;
            string devicenodename = null;
            if (node != null) {
                devicenodename = node["name"].ToString();
            }
            frmDevList.DeviceName = devicenodename;
            frmDevList.Init();
            if (frmDevList.ShowDialog() == DialogResult.OK) {
                foreach (DataRow row in frmDevList.DT.Rows) {
                    try {
                        if ((bool)row["C"]) {
                            PSP_ElcDevice elcDevice = new PSP_ElcDevice();
                            elcDevice.DeviceSUID = row["A"].ToString();
                            elcDevice.ProjectSUID = strID;
                            UCDeviceBase.DataService.Create<PSP_ElcDevice>(elcDevice);
                        } else {
                            PSP_ElcDevice elcDevice = new PSP_ElcDevice();
                            elcDevice.DeviceSUID = row["A"].ToString();
                            elcDevice.ProjectSUID = strID;
                            UCDeviceBase.DataService.Delete<PSP_ElcDevice>(elcDevice);
                        }
                    } catch (System.Exception ex) {

                    }
                }
                if (curDevice != null) {
                    curDevice.strCon = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID + "' and ";
                    curDevice.Init();
                }
            }
        }
        //排除设备
        void barDeleteDevice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (strID == null) {
                MessageBox.Show("请先选择电气计算方案！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            PSPDEV dev = curDevice.SelectedDevice as PSPDEV;
            PSP_ElcDevice elcdevice = new PSP_ElcDevice();
            elcdevice.ProjectSUID = strID;
            elcdevice.DeviceSUID = dev.SUID;
            Services.BaseService.Delete<PSP_ElcDevice>(elcdevice);
            if (curDevice != null) {
                curDevice.strCon = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID + "' and ";
                curDevice.Init();
            }
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
            strID = node["ID"].ToString();
            if (curDevice != null) {
                curDevice.strCon = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID + "' and ";
                curDevice.Init();
            }
            splitContainerControl1.Panel2.Text = node["Name"].ToString();
        }

        private void treeList2_MouseClick(object sender, MouseEventArgs e) {
            if (strID != null) {
                //TreeListNode node1 = treeList1.FocusedNode;
                //if (node1 == null) return;
                //strID = node1["ID"].ToString();
                if (e.Button == MouseButtons.Right) return;
                TreeListNode node = treeList2.FocusedNode;
                if (node == null) return;
                string dtype = node["class"].ToString();
                UCDeviceBase device = null;
                if (devicTypes.ContainsKey(dtype)) {
                    device = devicTypes[dtype];
                    try {
                        device.Show();
                    } catch { }
                } else {
                    device = createInstance(dtype);
                    device.ProjectID = this.ProjectUID;
                    devicTypes.Add(dtype, device);
                    showDevice(device);
                }

                if (curDevice != null && curDevice != device) curDevice.Hide();
                curDevice = device;
                if (curDevice != null) {
                    curDevice.strCon = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID + "' and ";
                    curDevice.Init();
                }
                splitContainerControl2.Panel2.Text = node["name"].ToString();
            } else {
                MessageBox.Show("请先选择电气计算方案！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

            ElectricLoadCal elc = new ElectricLoadCal();
            TreeListNode node = treeList1.FocusedNode;
            if (node == null) {
                MessageBox.Show("请先选择电气计算方案！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //WaitDialogForm wait = null;
            try {

                //wait = new WaitDialogForm("", "正在处理数据, 请稍候...");
                strID = node["ID"].ToString();
                elc.LFCER(strID, 1, 100);
                //elc.ALLShortThread(strID, this.ProjectUID, n3, 100, wait);
                // wait.Close();
            } catch (Exception exc) {
                Debug.Fail(exc.Message);
                Itop.Client.Common.HandleException.TryCatch(exc);
                //wait.Close();
                return;


            }


        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ElectricLoadCal elc = new ElectricLoadCal();
            TreeListNode node = treeList1.FocusedNode;
            if (node == null) {
                MessageBox.Show("请先选择电气计算方案！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //WaitDialogForm wait = null;
            try {

                //wait = new WaitDialogForm("", "正在处理数据, 请稍候...");
                strID = node["ID"].ToString();
                elc.LFCER(strID, 2, 100);
                //elc.ALLShortThread(strID, this.ProjectUID, n3, 100, wait);
                //wait.Close();
            } catch (Exception exc) {
                Debug.Fail(exc.Message);
                Itop.Client.Common.HandleException.TryCatch(exc);
                //wait.Close();
                return;


            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ElectricLoadCal elc = new ElectricLoadCal();
            TreeListNode node = treeList1.FocusedNode;
            if (node == null) {
                MessageBox.Show("请先选择电气计算方案！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //WaitDialogForm wait = null;
            try {

                //wait = new WaitDialogForm("", "正在处理数据, 请稍候...");
                strID = node["ID"].ToString();
                elc.LFCER(strID, 3, 100);
                //elc.ALLShortThread(strID, this.ProjectUID, n3, 100, wait);
                //wait.Close();
            } catch (Exception exc) {
                Debug.Fail(exc.Message);
                Itop.Client.Common.HandleException.TryCatch(exc);
                //wait.Close();
                return;


            }
        }
        private void barCheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ElectricLoadCal elc = new ElectricLoadCal();
            TreeListNode node = treeList1.FocusedNode;
            if (node == null) {
                MessageBox.Show("请先选择电气计算方案！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            strID = node["ID"].ToString();
            if (elc.DataCheck(strID)) {
                MessageBox.Show("检测成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ElectricLoadCal elc = new ElectricLoadCal();
            TreeListNode node = treeList1.FocusedNode;
            if (node == null) {
                MessageBox.Show("请先选择电气计算方案！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //WaitDialogForm wait = null;
            try {

                // wait = new WaitDialogForm("", "正在处理数据, 请稍候...");
                strID = node["ID"].ToString();
                elc.LFCER(strID, 4, 100);
                //elc.ALLShortThread(strID, this.ProjectUID, n3, 100, wait);
                // wait.Close();
            } catch (Exception exc) {
                Debug.Fail(exc.Message);
                Itop.Client.Common.HandleException.TryCatch(exc);
                //wait.Close();
                return;


            }
        }
        private void AllshortItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ElectricShorti elc = new ElectricShorti();
            TreeListNode node = treeList1.FocusedNode;
            if (node == null) {
                MessageBox.Show("请先选择电气计算方案！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            strID = node["ID"].ToString();
            //elc.CheckDL(strID, 100);
            int n3 = 0;
            ShorttypeForm shortfangshi = new ShorttypeForm();
            ShortTform shorttype = new ShortTform();
            frnReport wFrom = new frnReport();
            wFrom.Text = "短路计算";
            wFrom.Show();
            wFrom.ShowText += "进行相关设置\t" + System.DateTime.Now.ToString();
            if (shortfangshi.ShowDialog() == DialogResult.OK) {
                if (shortfangshi.Mathindex == 0) {
                    shorttype.ShowDialog();
                    if (shorttype.DialogResult == DialogResult.OK) {
                        switch (shorttype.DuanluType) {
                            case "单相接地":
                                n3 = 1;
                                break;

                            case "两相接地":
                                n3 = 3;
                                break;
                            case "两相故障":
                                n3 = 2;
                                break;
                            case "三相故障":
                                n3 = 0;
                                break;

                        }
                        elc.OutType = shorttype.Mathindex;
                        elc.Compuflag = shorttype.Compuflag;
                    } else
                        return;
                    //WaitDialogForm wait = null;
                    try {

                        // wait = new WaitDialogForm("", "正在处理数据, 请稍候...");

                        elc.AllShort(strID, this.ProjectUID, n3, 100, wFrom);
                        //elc.ALLShortThread(strID, this.ProjectUID, n3, 100, wait);
                        //wait.Close();
                    } catch (Exception exc) {
                        wFrom.ShowText += "\r\n短路计算失败\t" + System.DateTime.Now.ToString();
                        Debug.Fail(exc.Message);
                        Itop.Client.Common.HandleException.TryCatch(exc);
                        // wait.Close();
                        return;


                    }

                }
                if (shortfangshi.Mathindex == 1) {
                    SelshortbusForm1 selbusfrm = new SelshortbusForm1();
                    selbusfrm.ProjectSUID = strID;
                    selbusfrm.ProjectID = Itop.Client.MIS.ProgUID;
                    selbusfrm.ShowDialog();
                    if (selbusfrm.DialogResult == DialogResult.OK) {
                        List<PSPDEV> list = new List<PSPDEV>();
                        foreach (DataRow row in selbusfrm.DT.Rows) {
                            try {
                                if ((bool)row["C"]) {
                                    PSPDEV psp = new PSPDEV();
                                    psp.SUID = row["A"].ToString();

                                    psp = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByKey", psp);
                                    if (psp != null) {
                                        list.Add(psp);
                                    }
                                }

                            } catch (System.Exception ex) {

                            }
                        }
                        if (list.Count == 0) {
                            MessageBox.Show("没有选择短路的母线！");
                            return;
                        }
                        shorttype.ShowDialog();
                        if (shorttype.DialogResult == DialogResult.OK) {
                            switch (shorttype.DuanluType) {
                                case "单相接地":
                                    n3 = 1;
                                    break;

                                case "两相接地":
                                    n3 = 3;
                                    break;
                                case "两相故障":
                                    n3 = 2;
                                    break;
                                case "三相故障":
                                    n3 = 0;
                                    break;

                            }
                            elc.OutType = shorttype.Mathindex;
                            elc.Compuflag = shorttype.Compuflag;
                        } else
                            return;
                        // WaitDialogForm wait = null;
                        try {
                            //wait = new WaitDialogForm("", "正在处理数据, 请稍候...");
                            elc.Partshort(strID, this.ProjectUID, n3, 100, list, wFrom);
                            //wait.Close();
                        } catch (Exception exc) {
                            wFrom.ShowText += "\r\n短路计算失败\t" + System.DateTime.Now.ToString();
                            Debug.Fail(exc.Message);
                            Itop.Client.Common.HandleException.TryCatch(exc);
                            //wait.Close();
                            return;


                        }

                    }
                }


            }
        }
        private void RelcheckItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

            ElectricRelcheck elc = new ElectricRelcheck();
            TreeListNode node = treeList1.FocusedNode;
            if (node == null) {
                MessageBox.Show("请先选择电气计算方案！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            strID = node["ID"].ToString();
            RelFormdialog reldialog = new RelFormdialog();
            // reldialog.Parent = this;
            reldialog.ShowDialog();
            if (reldialog.DialogResult == DialogResult.OK) {
                WaitDialogForm wait = null;
                try {
                    wait = new WaitDialogForm("", "正在处理数据, 请稍候...");
                    wait.Close();
                    elc.WebCalAndPrint(strID, this.ProjectUID, 100);

                } catch (Exception exc) {
                    Debug.Fail(exc.Message);
                    Itop.Client.Common.HandleException.TryCatch(exc);
                    wait.Close();
                    return;


                }
            } else if (reldialog.DialogResult == DialogResult.Ignore) {
                if (this.Owner != null)
                    this.Visible = false;
                //进行变压器检验
                FrmLayoutSubstationInfo layoutSubstation = new FrmLayoutSubstationInfo();
                layoutSubstation.Biandianzhan();

            } else if (reldialog.DialogResult == DialogResult.Cancel) {
                if (this.Owner != null)
                    this.Visible = false;
            }

        }
        private void jiaoliucheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ElectricShorti elc = new ElectricShorti();
            TreeListNode node = treeList1.FocusedNode;
            if (node == null) {
                MessageBox.Show("请先选择电气计算方案！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.ZLcheck.Enabled = true;
            strID = node["ID"].ToString();
            WaitDialogForm wait = null;
            try {
                wait = new WaitDialogForm("", "正在处理数据, 请稍候...");
                wait.Close();
                elc.Allshortcheck(strID, this.ProjectUID, 100, 2);
                curDevice.Init();
            } catch (Exception exc) {
                Debug.Fail(exc.Message);
                Itop.Client.Common.HandleException.TryCatch(exc);
                wait.Close();
                return;


            }
        }
        private void ZLcheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ElectricShorti elc = new ElectricShorti();
            TreeListNode node = treeList1.FocusedNode;
            if (node == null) {
                MessageBox.Show("请先选择电气计算方案！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            strID = node["ID"].ToString();
            WaitDialogForm wait = null;
            try {
                wait = new WaitDialogForm("", "正在处理数据, 请稍候...");
                wait.Close();
                elc.Allshortcheck(strID, this.ProjectUID, 100, 3);

            } catch (Exception exc) {
                Debug.Fail(exc.Message);
                Itop.Client.Common.HandleException.TryCatch(exc);
                wait.Close();
                return;
            }

        }
        private void barNL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ElectricLoadCal elc = new ElectricLoadCal();
            TreeListNode node = treeList1.FocusedNode;
            if (node == null) {
                MessageBox.Show("请先选择电气计算方案！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            strID = node["ID"].ToString();
            elc.LFCERS(strID, 4, 100);
        }
        private void barORP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ElectricLoadCal elc = new ElectricLoadCal();
            TreeListNode node = treeList1.FocusedNode;
            if (node == null) {
                MessageBox.Show("请先选择电气计算方案！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            strID = node["ID"].ToString();
            elc.ORP(strID, 100);
        }
        private void barUpdateNum_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TreeListNode node = treeList1.FocusedNode;
            if (node == null) {
                MessageBox.Show("请先选择电气计算方案！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            strID = node["ID"].ToString();
            WaitDialogForm wait = null;
            try {
                wait = new WaitDialogForm("", "正在处理数据, 请稍候...");
                if (curDevice != null) {
                    curDevice.projectdeviceid = strID;
                    curDevice.updatenumberflag = false;
                    curDevice.UpdateNumber();
                }

                wait.Close();

            } catch (Exception exc) {
                Debug.Fail(exc.Message);
                Itop.Client.Common.HandleException.TryCatch(exc);
                wait.Close();
                return;
            }

        }
        private void barCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TreeListNode node = treeList1.FocusedNode;
            if (node == null) {
                MessageBox.Show("请先选择电气计算方案！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            } else {
                if (MessageBox.Show("是否复制方案：" + node["Name"].ToString() + "?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) {
                    string name = "副本" + node["Name"].ToString();
                    string strC = " Name like '" + name + "%'";
                    IList list1 = UCDeviceBase.DataService.GetList("SelectPSP_ELCPROJECTByWhere", strC);
                    if (list1.Count>0)
                    {
                        name = "副本" + node["Name"].ToString() + "-" + list1.Count.ToString();
                    }
                    string strCon = " WHERE ProjectSUID = '" + node["ID"].ToString() + "'";
                    IList list = UCDeviceBase.DataService.GetList("SelectPSP_ElcDeviceByCondition", strCon);
                    PSP_ELCPROJECT elcPro = new PSP_ELCPROJECT();
                    elcPro.FileType = node["FileType"].ToString();
                    elcPro.Name = name;
                    elcPro.ProjectID = Itop.Client.MIS.ProgUID;
                    elcPro.Class = System.DateTime.Now.ToString();
                    DataRow row = datatable.NewRow();
                    Itop.Common.DataConverter.ObjectToRow(elcPro, row);
                    datatable.Rows.Add(row);
                    UCDeviceBase.DataService.Create<PSP_ELCPROJECT>(elcPro);
                    SVGFILE svg = UCDeviceBase.DataService.GetOneByKey<SVGFILE>(node["ID"].ToString());
                    SVGFILE svgNew = new SVGFILE();
                    svgNew.SUID = elcPro.ID;
                    svgNew.FILENAME = elcPro.Name;
                    svgNew.SVGDATA = svg.SVGDATA;
                    UCDeviceBase.DataService.Create<SVGFILE>(svgNew);
                    foreach (PSP_ElcDevice elcDEV in list) {
                        PSP_ElcDevice elcElement = new PSP_ElcDevice();
                        elcElement.ProjectSUID = elcPro.ID;
                        elcElement.DeviceSUID = elcDEV.DeviceSUID;
                        UCDeviceBase.DataService.Create<PSP_ElcDevice>(elcElement);
                    }
                }
                return;
            }
        }
    }
}