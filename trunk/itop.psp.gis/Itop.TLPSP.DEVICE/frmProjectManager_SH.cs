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
using Itop.Domain.Table;
namespace Itop.TLPSP.DEVICE {
    /// <summary>
    /// ��Ŀ����
    /// </summary>
    public partial class frmProjectManager_SH : Itop.Client.Base.FormModuleBase {
        DataTable datatable;
        protected string strID = null;
        public frmProjectManager_SH()
        {
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
                // MessageBox.Show("����ѡ��������㷽����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //splitContainerControl1.Panel2.Text = node["Name"].ToString();
        }
        #region ��ʼ��
        public new string ProjectUID {
            get {
                return Itop.Client.MIS.ProgUID;
            }
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            //bar1.AddItem(barButtonItem1);
            if (strID == null)
            {
                bar.AddItems(new DevExpress.XtraBars.BarItem[] { barSelectDevice, barDeleteDevice, barCopy, Autofpfh,bardetail, barButtonItem1, barButtonItem2, barButtonItem3, barButtonItem4, barORP, AllshortItem, RelcheckItem, jiaoliucheck, ZLcheck, barCheck, barUpdateNum });
                //bar.AddItem();
                barQuery.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                barORP.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                bar.AddItem(barSelectDevice);
            }
            InitDeviceType();
            treeList1.FocusedNode = null;
            treeList2.FocusedNode = null;

        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            if (this.Text == "") {
                this.Text = "��������";

            }
        }
        protected void Init() {

            this.WindowState = FormWindowState.Maximized;
            this.Text = this.smmprog.ProgName;

        }
        string filetype;
        PSP_ELCPROJECT parentobj;
        public void PspProject()
        { this.Show();
            
            this.WindowState = FormWindowState.Maximized;
            PSPProject pd = new PSPProject();
            pd.ProjectID = this.ProjectUID;
            pd.Initdata(false);
            filetype = "����";
            if (pd.ShowDialog() == DialogResult.OK)
            {
                strID = pd.FileSUID;
                parentobj = pd.Parentobj;
                this.Text = "��������-"+parentobj.Name;
               
                initbar(true);
            }
            else
            {
                this.Close();
            }
            
        }
        //flag=tureΪ����������� false Ϊ��·�������
        private void initbar(bool flag )
        {

            barQuery.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
           
            if (flag)
            { 
                barORP.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                 barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always; 
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Always; 
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                RelcheckItem.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                AllshortItem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                jiaoliucheck.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                ZLcheck.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else
            {
                barORP.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                RelcheckItem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                AllshortItem.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                jiaoliucheck.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                ZLcheck.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
        }
        public void ShortProject()
        { 
            this.Show();
            this.Text = "��·����";
            this.WindowState = FormWindowState.Maximized;
            PSPProject pd = new PSPProject();
            pd.ProjectID = this.ProjectUID;
            pd.Initdata(true);
            filetype = "��·";
            if (pd.ShowDialog() == DialogResult.OK)
            {
                strID = pd.FileSUID;
                parentobj = pd.Parentobj;
                 this.Text = "��·����-"+parentobj.Name;
               
                initbar(false);
            }
            else
            {
                this.Close();
            }
        }
        /// <summary>
        /// ��ʼ�豸����
        /// </summary>
        private void InitDeviceType() {
            TreeListColumn column = new TreeListColumn();
            column.Caption = "��Ŀ����";
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
            DeviceTypeHelper.initprojectDeviceTypes_SH1(treeList2);
        }

        #endregion

        #region ˽�з���
        /// <summary>
        /// ʵ������ӿ�
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

        #region ��¼����

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
                if (MessageBox.Show("ȷ��ɾ��ô?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) {
                    string id = node["ID"].ToString();
                    treeList1.DeleteNode(treeList1.FocusedNode);
                    PSP_ELCPROJECT pd = new PSP_ELCPROJECT();
                    pd.ID = id;
                    UCDeviceBase.DataService.Delete<PSP_ELCPROJECT>(pd);
                    //ɾ�������������豸��¼
                    string con = "where ProjectSUID = '"+pd.ID+"'";
                    IList list = Services.BaseService.GetList("SelectPSP_ElcDeviceByCondition", con);
                    foreach (PSP_ElcDevice pe in list)
                    {
                        UCDeviceBase.DataService.Delete<PSP_ElcDevice>(pe);
                    }
                    //ɾ��������ص�ͼ�μ�¼
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
            //TreeListNode node = treeList1.FocusedNode;
            //if (node == null) {
            //    MessageBox.Show("����ѡ��������㷽����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //strID = node["ID"].ToString();
            elc.LFC(strID, 1, 100);
            // base.Query();
        }
        void barSelectDevice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TreeListNode node;
            if (strID == null) {
                MessageBox.Show("����ѡ��������㷽����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmSeldevtype fm = new frmSeldevtype();
            int devtype = 0;
            if (fm.ShowDialog() == DialogResult.OK)
            {
                devtype = fm.UnitFlag;
            }
            else
                return;
            //strID = node["ID"].ToString();
            DataTable dt = new DataTable();
            frmDeviceList_sh frmDevList = new frmDeviceList_sh();
            frmDevList.ProjectID = this.ProjectUID;
            frmDevList.ProjectSUID = strID;
            frmDevList.BelongYear = parentobj.BelongYear;
            frmDevList.Devicetype = devtype;
            node = treeList2.FocusedNode;
            string devicenodename = null;
            if (node != null) {
                devicenodename = node["name"].ToString();
            }
            frmDevList.DeviceName = devicenodename;
            frmDevList.initCheckcombox();
            frmDevList.initcombox();
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

                    if (curDevice.GetClassName() == "PSP_Substation_Info")
                    {
                        curDevice.proInit(parentobj.ID);
                    }
                    else if (curDevice.GetClassName() == "PSP_PowerSubstation_Info")
                    {
                        curDevice.proInit(parentobj.ID);
                    }
                    else if (curDevice.GetClassName() == "PSPDEV")
                    {
                        curDevice.strCon = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID + "' and ";
                        curDevice.Init();
                    }
                }
            }
        }
        //�ų��豸
        void barDeleteDevice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (strID == null||curDevice==null) {
                MessageBox.Show("ѡ�������⣡", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
         if (curDevice.SelectedDevice!=null)
         {
             if (curDevice.SelectedDevice.GetType() == typeof(PSPDEV))
             {
                 PSPDEV dev = curDevice.SelectedDevice as PSPDEV;
                 PSP_ElcDevice elcdevice = new PSP_ElcDevice();
                 elcdevice.ProjectSUID = strID;
                 elcdevice.DeviceSUID = dev.SUID;
                 Services.BaseService.Delete<PSP_ElcDevice>(elcdevice);

             }
             else if (curDevice.SelectedDevice.GetType() == typeof(PSP_Substation_Info))
             {
                 PSP_Substation_Info dev = curDevice.SelectedDevice as PSP_Substation_Info;
                 PSP_ElcDevice elcdevice = new PSP_ElcDevice();
                 elcdevice.ProjectSUID = strID;
                 elcdevice.DeviceSUID = dev.UID;
                 Services.BaseService.Delete<PSP_ElcDevice>(elcdevice);
             }
             else if (curDevice.SelectedDevice.GetType() == typeof(PSP_PowerSubstation_Info))
             {
                 PSP_PowerSubstation_Info dev = curDevice.SelectedDevice as PSP_PowerSubstation_Info;
                 PSP_ElcDevice elcdevice = new PSP_ElcDevice();
                 elcdevice.ProjectSUID = strID;
                 elcdevice.DeviceSUID = dev.UID;
                 Services.BaseService.Delete<PSP_ElcDevice>(elcdevice);
             }
         }
           
         
            if (curDevice != null) {
                if (curDevice.GetClassName() == "PSP_Substation_Info")
                {
                    curDevice.proInit(parentobj.ID);
                }
                else if (curDevice.GetClassName() == "PSP_PowerSubstation_Info")
                {
                    curDevice.proInit(parentobj.ID);
                }
                else if (curDevice.GetClassName() == "PSPDEV")
                {
                    curDevice.strCon = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID + "' and ";
                    curDevice.Init();
                }
            }
        }
        #endregion

        #region �ֶ�
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
                if (string.IsNullOrEmpty(dtype))
                {
                    return;
                }
                if (node["id"].ToString() == "20" || node["id"].ToString()=="30")
                {
                    this.bardetail.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.barDeleteDevice.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
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
                    if (curDevice.GetClassName() == "PSP_Substation_Info")
                    {
                        curDevice.proInit(parentobj.ID);
                    }
                    else if (curDevice.GetClassName() == "PSP_PowerSubstation_Info")
                    {
                        curDevice.proInit(parentobj.ID);
                    }
                    else if (curDevice.GetClassName() == "PSPDEV")
                    {
                        curDevice.strCon = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID + "' and ";
                        curDevice.Init();
                    }
                   
                }
                splitContainerControl2.Panel2.Text = node["name"].ToString();
            } else {
                MessageBox.Show("����ѡ��������㷽����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

            ElectricLoadCal elc = new ElectricLoadCal();
            //TreeListNode node = treeList1.FocusedNode;
            if (strID == null) {
                MessageBox.Show("����ѡ��������㷽����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //WaitDialogForm wait = null;
            try {

                //wait = new WaitDialogForm("", "���ڴ�������, ���Ժ�...");
               // strID = node["ID"].ToString();
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
            //TreeListNode node = treeList1.FocusedNode;
            if (strID == null) {
                MessageBox.Show("����ѡ��������㷽����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //WaitDialogForm wait = null;
            try {

                //wait = new WaitDialogForm("", "���ڴ�������, ���Ժ�...");
                //strID = node["ID"].ToString();
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
           // TreeListNode node = treeList1.FocusedNode;
            if (strID == null) {
                MessageBox.Show("����ѡ��������㷽����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //WaitDialogForm wait = null;
            try {

                //wait = new WaitDialogForm("", "���ڴ�������, ���Ժ�...");
                //strID = node["ID"].ToString();
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
            //TreeListNode node = treeList1.FocusedNode;
            if (strID== null) {
                MessageBox.Show("����ѡ��������㷽����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //strID = node["ID"].ToString();
            if (elc.DataCheck(strID)) {
                MessageBox.Show("���ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ElectricLoadCal elc = new ElectricLoadCal();
           // TreeListNode node = treeList1.FocusedNode;
            if (strID == null) {
                MessageBox.Show("����ѡ��������㷽����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //WaitDialogForm wait = null;
            try {

                // wait = new WaitDialogForm("", "���ڴ�������, ���Ժ�...");
                //strID = node["ID"].ToString();
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
            //TreeListNode node = treeList1.FocusedNode;
            if (strID == null) {
                MessageBox.Show("����ѡ��������㷽����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //strID = node["ID"].ToString();
            //elc.CheckDL(strID, 100);
            int n3 = 0;
            ShorttypeForm shortfangshi = new ShorttypeForm();
            ShortTform shorttype = new ShortTform();
            frnReport wFrom = new frnReport();
            wFrom.Text = "��·����";
            wFrom.Show();
            wFrom.ShowText += "�����������\t" + System.DateTime.Now.ToString();
            if (shortfangshi.ShowDialog() == DialogResult.OK) {
                if (shortfangshi.Mathindex == 0) {
                    shorttype.ShowDialog();
                    if (shorttype.DialogResult == DialogResult.OK) {
                        switch (shorttype.DuanluType) {
                            case "����ӵ�":
                                n3 = 1;
                                break;

                            case "����ӵ�":
                                n3 = 3;
                                break;
                            case "�������":
                                n3 = 2;
                                break;
                            case "�������":
                                n3 = 0;
                                break;

                        }
                        elc.OutType = shorttype.Mathindex;
                        elc.Compuflag = shorttype.Compuflag;
                    } else
                        return;
                    //WaitDialogForm wait = null;
                    try {

                        // wait = new WaitDialogForm("", "���ڴ�������, ���Ժ�...");

                        elc.AllShort(strID, this.ProjectUID, n3, 100, wFrom);
                        //elc.ALLShortThread(strID, this.ProjectUID, n3, 100, wait);
                        //wait.Close();
                    } catch (Exception exc) {
                        wFrom.ShowText += "\r\n��·����ʧ��\t" + System.DateTime.Now.ToString();
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
                            MessageBox.Show("û��ѡ���·��ĸ�ߣ�");
                            return;
                        }
                        shorttype.ShowDialog();
                        if (shorttype.DialogResult == DialogResult.OK) {
                            switch (shorttype.DuanluType) {
                                case "����ӵ�":
                                    n3 = 1;
                                    break;

                                case "����ӵ�":
                                    n3 = 3;
                                    break;
                                case "�������":
                                    n3 = 2;
                                    break;
                                case "�������":
                                    n3 = 0;
                                    break;

                            }
                            elc.OutType = shorttype.Mathindex;
                            elc.Compuflag = shorttype.Compuflag;
                        } else
                            return;
                        // WaitDialogForm wait = null;
                        try {
                            //wait = new WaitDialogForm("", "���ڴ�������, ���Ժ�...");
                            elc.Partshort(strID, this.ProjectUID, n3, 100, list, wFrom);
                            //wait.Close();
                        } catch (Exception exc) {
                            wFrom.ShowText += "\r\n��·����ʧ��\t" + System.DateTime.Now.ToString();
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
            //TreeListNode node = treeList1.FocusedNode;
            if (string.IsNullOrEmpty(strID)) {
                MessageBox.Show("����ѡ��������㷽����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //strID = node["ID"].ToString();
            RelFormdialog reldialog = new RelFormdialog();
            // reldialog.Parent = this;
            reldialog.ShowDialog();
            if (reldialog.DialogResult == DialogResult.OK) {
                WaitDialogForm wait = null;
                try {
                    wait = new WaitDialogForm("", "���ڴ�������, ���Ժ�...");
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
                //���б�ѹ������
                FrmLayoutSubstationInfo layoutSubstation = new FrmLayoutSubstationInfo();
                layoutSubstation.Biandianzhan();

            } else if (reldialog.DialogResult == DialogResult.Cancel) {
                if (this.Owner != null)
                    this.Visible = false;
            }

        }
        private void jiaoliucheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ElectricShorti elc = new ElectricShorti();
            //TreeListNode node = treeList1.FocusedNode;
            if (string.IsNullOrEmpty(strID))
            {
                MessageBox.Show("����ѡ��������㷽����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.ZLcheck.Enabled = true;
            //strID = node["ID"].ToString();
            WaitDialogForm wait = null;
            try {
                wait = new WaitDialogForm("", "���ڴ�������, ���Ժ�...");
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
            //TreeListNode node = treeList1.FocusedNode;
            if (string.IsNullOrEmpty(strID))
            {
                MessageBox.Show("����ѡ��������㷽����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
           // strID = node["ID"].ToString();
            WaitDialogForm wait = null;
            try {
                wait = new WaitDialogForm("", "���ڴ�������, ���Ժ�...");
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
            //TreeListNode node = treeList1.FocusedNode;
            if (string.IsNullOrEmpty(strID))
            {
                MessageBox.Show("����ѡ��������㷽����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //strID = node["ID"].ToString();
            elc.LFCERS(strID, 4, 100);
        }
        private void barORP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ElectricLoadCal elc = new ElectricLoadCal();
           // TreeListNode node = treeList1.FocusedNode;
            if (string.IsNullOrEmpty(strID))
            {
                MessageBox.Show("����ѡ��������㷽����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //strID = node["ID"].ToString();
            elc.ORP(strID, 100);
        }
        private void barUpdateNum_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //TreeListNode node = treeList1.FocusedNode;
            if (string.IsNullOrEmpty(strID))
            {
                MessageBox.Show("����ѡ��������㷽����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //strID = node["ID"].ToString();
            WaitDialogForm wait = null;
            try {
                wait = new WaitDialogForm("", "���ڴ�������, ���Ժ�...");
                //if (curDevice != null) {
                //    curDevice.projectdeviceid = strID;
                //    curDevice.updatenumberflag = false;
                //    curDevice.UpdateNumber();
                //}
                AllUpdateNumber();
                wait.Close();

            } catch (Exception exc) {
                Debug.Fail(exc.Message);
                Itop.Client.Common.HandleException.TryCatch(exc);
                wait.Close();
                return;
            }

        }
        private void AllUpdateNumber()
        {
            //����ĸ�߱��
           string con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" +strID+ "' and pspdev.Type='01' ORDER BY Number";
            IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            int i = 0;
            foreach (PSPDEV dev in list)
            {
                i++;
                dev.Number = i;
                UCDeviceBase.DataService.Update<PSPDEV>(dev);
            }
            //������·���
            con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID + "' and pspdev.Type='05'";

           list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            foreach (PSPDEV dev in list)
            {
                string iname = dev.IName;
                if (iname != null)
                {
                    con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID + "'and pspdev.type='01'and pspdev.name='" + iname + "'";
                    PSPDEV psp = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                    if (psp != null)
                    {
                        dev.FirstNode = psp.Number;
                        dev.RateVolt = psp.RateVolt;
                        dev.ReferenceVolt = psp.ReferenceVolt;
                    }
                }

                string jname = dev.JName;
                if (jname != null)
                {
                    con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID + "'and pspdev.type='01'and pspdev.name='" + jname + "'";
                    PSPDEV psp = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                    if (psp != null)
                    {
                        dev.LastNode = psp.Number;
                    }
                }
                UCDeviceBase.DataService.Update<PSPDEV>(dev);
            }
            //���¸���
            con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID  + "' and pspdev.Type='12'";

            list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            foreach (PSPDEV dev in list)
            {
                string iname = dev.IName;
                if (iname != null)
                {
                    con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID  + "'and pspdev.type='01'and pspdev.name='" + iname + "'";
                    PSPDEV psp = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                    if (psp != null)
                    {
                        dev.FirstNode = psp.Number;
                        dev.RateVolt = psp.RateVolt;
                        dev.ReferenceVolt = psp.ReferenceVolt;
                    }
                }
                UCDeviceBase.DataService.Update<PSPDEV>(dev);
            }
            //���������� ��������
            con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID  + "' and pspdev.Type='02'";

           list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            foreach (PSPDEV dev in list)
            {
                string iname = dev.IName;
                if (iname != null)
                {
                    con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID  + "'and pspdev.type='01'and pspdev.name='" + iname + "'";
                    PSPDEV psp = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                    if (psp != null)
                    {
                        dev.FirstNode = psp.Number;
                        dev.Vi0 = psp.RateVolt;
                        dev.Vib = psp.ReferenceVolt;
                    }
                }

                string jname = dev.JName;
                if (jname != null)
                {
                    con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID  + "'and pspdev.type='01'and pspdev.name='" + jname + "'";
                    PSPDEV psp = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                    if (psp != null)
                    {
                        dev.LastNode = psp.Number;
                        dev.Vj0 = psp.RateVolt;
                        dev.Vjb = psp.ReferenceVolt;
                    }
                }
                UCDeviceBase.DataService.Update<PSPDEV>(dev);
            }

            con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID  + "' and pspdev.Type='03'";

             list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            foreach (PSPDEV dev in list)
            {
                string iname = dev.IName;
                if (iname != null)
                {
                    con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID  + "'and pspdev.type='01'and pspdev.name='" + iname + "'";
                    PSPDEV psp = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                    if (psp != null)
                    {
                        dev.FirstNode = psp.Number;
                        dev.Vi0 = psp.RateVolt;
                        dev.Vib = psp.ReferenceVolt;

                    }
                }

                string jname = dev.JName;
                if (jname != null)
                {
                    con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID  + "'and pspdev.type='01'and pspdev.name='" + jname + "'";
                    PSPDEV psp = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                    if (psp != null)
                    {
                        dev.LastNode = psp.Number;
                        dev.Vj0 = psp.RateVolt;
                        dev.Vjb = psp.ReferenceVolt;
                    }
                }
                string kname = dev.KName;
                if (kname != null)
                {
                    con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID  + "'and pspdev.type='01'and pspdev.name='" + kname + "'";
                    PSPDEV psp = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                    if (psp != null)
                    {
                        dev.Flag = psp.Number;
                        dev.Vk0 = psp.RateVolt;
                        dev.Vkb = psp.ReferenceVolt;
                    }
                }
                UCDeviceBase.DataService.Update<PSPDEV>(dev);
            }

            //���·����
            con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID  + "' and pspdev.Type='04'";

           list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            foreach (PSPDEV dev in list)
            {
                string iname = dev.IName;
                if (iname != null)
                {
                    con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID  + "'and pspdev.type='01'and pspdev.name='" + iname + "'";
                    PSPDEV psp = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                    if (psp != null)
                    {
                        dev.FirstNode = psp.Number;
                        dev.RateVolt = psp.RateVolt;
                        dev.ReferenceVolt = psp.ReferenceVolt;
                    }
                }
                UCDeviceBase.DataService.Update<PSPDEV>(dev);
            }

            //�����޹��豸
            con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID  + "' and pspdev.Type='11'";

           list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            foreach (PSPDEV dev in list)
            {
                string iname = dev.IName;
                if (iname != null)
                {
                    con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID  + "'and pspdev.type='01'and pspdev.name='" + iname + "'";
                    PSPDEV psp = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                    if (psp != null)
                    {
                        dev.FirstNode = psp.Number;
                        dev.ReferenceVolt = psp.ReferenceVolt;
                        dev.RateVolt = psp.RateVolt;
                    }
                }
                UCDeviceBase.DataService.Update<PSPDEV>(dev);
            }
            con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID  + "' and pspdev.Type='09'";

           list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            foreach (PSPDEV dev in list)
            {
                string iname = dev.IName;
                if (iname != null)
                {
                    con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID  + "'and pspdev.type='01'and pspdev.name='" + iname + "'";
                    PSPDEV psp = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                    if (psp != null)
                    {
                        dev.FirstNode = psp.Number;
                        dev.RateVolt = psp.RateVolt;
                        dev.ReferenceVolt = psp.ReferenceVolt;
                    }
                }
                UCDeviceBase.DataService.Update<PSPDEV>(dev);
            }
            con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID  + "' and pspdev.Type='10'";

           list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            foreach (PSPDEV dev in list)
            {
                string iname = dev.IName;
                if (iname != null)
                {
                    con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID  + "'and pspdev.type='05'and pspdev.name='" + iname + "'";
                    PSPDEV psp = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                    if (psp != null)
                    {
                        dev.FirstNode = psp.FirstNode;
                        dev.LastNode = psp.LastNode;
                        dev.RateVolt = psp.RateVolt;
                        dev.ReferenceVolt = psp.ReferenceVolt;
                    }
                }
                UCDeviceBase.DataService.Update<PSPDEV>(dev);
            }
            con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID  + "' and pspdev.Type='08'";

           list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            foreach (PSPDEV dev in list)
            {
                string iname = dev.IName;
                if (iname != null)
                {
                    con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID  + "'and pspdev.type='05'and pspdev.name='" + iname + "'";
                    PSPDEV psp = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                    if (psp != null)
                    {
                        dev.FirstNode = psp.FirstNode;
                        dev.LastNode = psp.LastNode;
                        dev.RateVolt = psp.RateVolt;
                        dev.ReferenceVolt = psp.ReferenceVolt;
                    }
                }
                UCDeviceBase.DataService.Update<PSPDEV>(dev);
            }

        }
        private void bardetail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //TreeListNode node = treeList1.FocusedNode;
             frmProjectManager_children frmc = new frmProjectManager_children();
            frmc.ParentObj =curDevice.SelectedDevice ;
            frmc.Psp_proj=parentobj;
            string[] types;
            if (curDevice.GetClassName()=="PSP_Substation_Info")
            {
            types=new string[]{"01","03","12"};
            }
            else
            {
              types=new string[]{"01","02","04"};
            }
            frmc.childrendevice(types);
            if (curDevice != null)
            {
                if (curDevice.GetClassName() == "PSP_Substation_Info")
                {
                    curDevice.proInit(parentobj.ID);
                }
                else if (curDevice.GetClassName() == "PSP_PowerSubstation_Info")
                {
                    curDevice.proInit(parentobj.ID);
                }
                else if (curDevice.GetClassName() == "PSPDEV")
                {
                    curDevice.strCon = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + strID + "' and ";
                    curDevice.Init();
                }
            }
            //if (frmc.DialogResult == DialogResult.OK)
            //{

            //}

        }
        private void barCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //TreeListNode node = treeList1.FocusedNode;
            if (string.IsNullOrEmpty(strID))
            {
                MessageBox.Show("����ѡ��������㷽����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            } else {
                if (MessageBox.Show("�Ƿ��Ʒ�����" + parentobj.Name,"��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    string name = "����" + parentobj.Name.ToString();
                    string strC = " Name like '" + name + "%'";
                    IList list1 = UCDeviceBase.DataService.GetList("SelectPSP_ELCPROJECTByWhere", strC);
                    if (list1.Count>0)
                    {
                        name = "����" + parentobj.Name + "-" + list1.Count.ToString();
                    }
                    string strCon = " WHERE ProjectSUID = '" + parentobj.ID + "'";
                    IList list = UCDeviceBase.DataService.GetList("SelectPSP_ElcDeviceByCondition", strCon);
                    PSP_ELCPROJECT elcPro = new PSP_ELCPROJECT();
                    elcPro.FileType = parentobj.FileType;
                    elcPro.Name = name;
                    elcPro.ProjectID = Itop.Client.MIS.ProgUID;
                    elcPro.Class = System.DateTime.Now.ToString();
                    DataRow row = datatable.NewRow();
                    Itop.Common.DataConverter.ObjectToRow(elcPro, row);
                    datatable.Rows.Add(row);
                    UCDeviceBase.DataService.Create<PSP_ELCPROJECT>(elcPro);
                    SVGFILE svg = UCDeviceBase.DataService.GetOneByKey<SVGFILE>(parentobj.ID);
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
        private void fpfhyj(PSP_Substation_Info sub,ref double zgfh,ref Dictionary<PSP_Substation_Info,IList<PSPDEV>> listfhfp)
        {
            //�����������ѹĸ��
            string con = "where SvgUID='" + sub.UID + "'and Type='01' and ProjectID='" + Itop.Client.MIS.ProgUID + "' order by RateVolt";
            IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
            List<int> listnum = new List<int>(); //��¼�Ǽ���ĸ�� Ҫ���п��ǵ�ϵͳ������\
            IList<PSPDEV> list1 = new List<PSPDEV>();
            double minv = sub.L1;
            if (list.Count==0)
            {
                return;
            }
            bool flag=false;
            for (int i = 0; i < list.Count;i++ )
            {
                if (list[i].RateVolt <= minv)
                {
                    minv = list[i].RateVolt;
                }
                if (list[i].RateVolt==sub.L1)
                {
                    flag=true;
                }
            }
            
            for (int i = 0; i < list.Count;i++ )
            {
                if (list[i].RateVolt == minv)
                {
                    con = "where IName='" + list[i].Name + "'and Type='12' and ProjectID='" + Itop.Client.MIS.ProgUID + "'";
                    PSPDEV obj = UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con)as PSPDEV;
                    if (obj != null)
                    {
                        list1.Add(obj);
                    }
                    else
                        list1.Add(list[i]);
                    
                }
                if (flag)
                {
                    if (list[i].RateVolt == sub.L1)
                    {
                        con = "where IName='" + list[i].Name + "'and Type='12' and ProjectID='" + Itop.Client.MIS.ProgUID + "'";
                        PSPDEV obj = UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con) as PSPDEV;
                        if (obj != null)
                        {
                            zgfh += obj.OutP;
                        }
                        else
                            zgfh += list[i].HuganTQ1;
                    }
                }
            }
            
            listfhfp[sub] = list1;
            
        }
        private void Autofpfh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Dictionary<string, Ps_Table_220Result> dic220;
            Dictionary<string, Ps_Table_110Result> dic110;
            Dictionary<string,IList<PSP_Substation_Info>> sub220list ;
            Dictionary<string, IList<PSP_Substation_Info>> sub110list;
            if (string.IsNullOrEmpty(parentobj.BelongYear))
            {
                parentobj.BelongYear = DateTime.Now.Year.ToString();
            }
            double tsl = 1;
            FrmAutofh fa = new FrmAutofh();
            fa.BelongYear = parentobj.BelongYear;
            if (fa.ShowDialog() == DialogResult.OK)
            {
                dic220 = fa.Dic220;
                dic110 = fa.Dic110;
                tsl = fa.TSL;
                sub110list = fa.Sub110list;
                sub220list = fa.Sub220list;
                List<string> listarea = fa.ListArea;
                //�ҳ��˾��µ����б��վ�µ� ���� ����������й� �޹�Ϊ�й���1/3
                for (int i = 0; i < listarea.Count;i++ )
                {
                    //�ñ��վ��ͬ��ѹ�ȼ��ĸ���
                    Dictionary<PSP_Substation_Info, IList<PSPDEV>> dicfh=new Dictionary<PSP_Substation_Info,IList<PSPDEV>>();
                    double sumbdzrl = 0;
                    double Fhfpz = 0;
                    //���110ƽ�����ڸõ�������ֻ���иõ����ļ���
                    if (dic110.ContainsKey(listarea[i]))
                    {
                        Ps_Table_110Result p110 = dic110[listarea[i]];
                      double qshfh= (double) p110.GetType().GetProperty("yf" + parentobj.BelongYear).GetValue(p110,null);
                        if (qshfh==0)
                        {
                            continue;
                        }
                        double zgfh = 0;
                        if (sub110list.ContainsKey(listarea[i]))  //���ڱ��վ
                        {
                            // �ҳ�220kv���վ��ֱ������
                            double zgfh220 = 0;
                            if (sub220list.ContainsKey(listarea[i]))
                            {
                                for (int j = 0; j < sub220list[listarea[i]].Count;j++ )
                                {
                                    //string con = "where SvgUID='" + sub220list[listarea[i]][j].UID + "'and Type='12' and ProjectID='" + Itop.Client.MIS.ProgUID + "'and VoltR='" + sub220list[listarea[i]][j].L1 + "'or  VoltR is null";
                                    //IList<PSPDEV> listfh = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
                                    //for (int m = 0; m < listfh.Count; m++)
                                    //{
                                    //    zgfh220 += listfh[m].OutP;
                                    //}
                                    fpfhyj(sub220list[listarea[i]][j], ref zgfh220, ref dicfh);
                                }
                            }
                            dicfh=new Dictionary<PSP_Substation_Info,IList<PSPDEV>>();
                            //Ϊ110
                            for (int j = 0; j < sub110list[listarea[i]].Count;j++ )
                            {
                                sumbdzrl += sub110list[listarea[i]][j].L2;
                                fpfhyj(sub110list[listarea[i]][j],ref zgfh,ref dicfh);
                                //string con = "where SvgUID='" + sub110list[listarea[i]][j].UID + "'and Type='12' and ProjectID='" + Itop.Client.MIS.ProgUID + "'and VoltR='" + sub110list[listarea[i]][j].L1 + "'or  VoltR is null";
                                //IList<PSPDEV> listfh = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
                                //if (listfh.Count>0)
                                //{
                                //    dicfh.Add(sub110list[listarea[i]][j], listfh);
                                //}
                                //else
                                //    continue;
                                //for (int m = 0; m < listfh.Count;m++ )
                                //{
                                //    zgfh += listfh[m].OutP;
                                //}
                            }
                            Fhfpz = qshfh - zgfh-zgfh220;
                            if (Fhfpz<=0)
                            {
                                continue;
                            }
                            for (int j = 0; j < sub110list[listarea[i]].Count; j++)
                            {
                                double fpl = 0;
                                fpl = ((sub110list[listarea[i]][j].L2 / sumbdzrl) * Fhfpz) / tsl;
                                int fhsum = dicfh[sub110list[listarea[i]][j]].Count;
                                foreach (PSPDEV fh in dicfh[sub110list[listarea[i]][j]])
                                {
                                   
                                    fh.InPutP = fpl / fhsum;
                                    fh.InPutQ = fpl / (3 * fhsum);
                                    UCDeviceBase.DataService.Update<PSPDEV>(fh);
                                }
                            }
                        }
                    }
                    else   //���������
                    {
                        //�ж�220ƽ����е������Ƿ����
                        if (dic220.ContainsKey(listarea[i]))
                        {
                            Ps_Table_220Result p220 = dic220[listarea[i]];
                            double qshfh = (double)p220.GetType().GetProperty("yf" + parentobj.BelongYear).GetValue(p220, null);
                            if (qshfh == 0)
                            {
                                continue;
                            }
                            double zgfh = 0;
                            if (sub220list.ContainsKey(listarea[i]))  //���ڱ��վ
                            {
                                
                              
                                //Ϊ220
                                for (int j = 0; j < sub220list[listarea[i]].Count; j++)
                                {
                                    sumbdzrl += sub220list[listarea[i]][j].L2;
                                    fpfhyj(sub220list[listarea[i]][j], ref zgfh, ref dicfh);
                                    //string con = "where SvgUID='" + sub220list[listarea[i]][j].UID + "'and Type='12' and ProjectID='" + Itop.Client.MIS.ProgUID + "'and VoltR='" + sub220list[listarea[i]][j].L1 + "'or  VoltR is null";
                                    //IList<PSPDEV> listfh = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
                                    //if (listfh.Count > 0)
                                    //{
                                    //    dicfh.Add(sub220list[listarea[i]][j], listfh);
                                    //}
                                    //else
                                    //    continue;
                                    //for (int m = 0; m < listfh.Count; m++)
                                    //{
                                    //    zgfh += listfh[m].OutP;
                                    //}
                                }
                                Fhfpz = qshfh - zgfh ;
                                if (Fhfpz <= 0)
                                {
                                    continue;
                                }
                                for (int j = 0; j < sub220list[listarea[i]].Count; j++)
                                {
                                    double fpl = 0;
                                    fpl = ((sub220list[listarea[i]][j].L2 / sumbdzrl) * Fhfpz) / tsl;
                                    int fhsum = dicfh[sub110list[listarea[i]][j]].Count;
                                    foreach (PSPDEV fh in dicfh[sub220list[listarea[i]][j]])
                                    {
                                        fh.InPutP = fpl / fhsum;
                                        fh.InPutQ = fpl / (3 * fhsum);
                                        UCDeviceBase.DataService.Update<PSPDEV>(fh);
                                    }
                                }
                            }

                        }
                    }
                }

            }
        }
    }
}