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
    /// �豸����
    /// </summary>
    public partial class frmDeviceManager_children : Itop.Client.Base.FormModuleBase
    {
        public frmDeviceManager_children()
        {
            InitializeComponent();
            
        }
        #region ��ʼ��
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            bar.AddItems(new DevExpress.XtraBars.BarItem[] { barButtonItemIn, barButtonItemOut,UpdateNumber, barButtonItemDel, AllDele, barButtonItemclose,barImportPsasp,bardevicetemplate });
            
            barQuery.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //treeList1.FocusedNode = null;
        }
        protected void Init() {
            this.Text = "�豸��������";
            this.WindowState = FormWindowState.Maximized;
            if(!string.IsNullOrEmpty(this.smmprog.ProgName))
            this.Text = this.smmprog.ProgName;
        }
        /// <summary>
        /// ��ʼ�豸����
        /// </summary>
        private void InitDeviceType() {

            DeviceTypeHelper.InitDeviceTypes(treeList1);
        }
        private void InitDeviceType(string[] type)
        {
            DeviceTypeHelper.InitDeviceTypes(treeList1,type);
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
        //���豸�µ����豸ά������ ���� ĸ�� �������ѹ�� ��������豸
        public void childrendevice(string[] type)
        {
            
            this.Text = "��������";
            this.WindowState = FormWindowState.Maximized;
            if (!string.IsNullOrEmpty(this.smmprog.ProgName))
                this.Text = this.smmprog.ProgName;
            InitDeviceType(type);
            //splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            treeList1.FocusedNode = null;
            this.ShowDialog();
        }
        //�ߵ�վ���
         public void bdzdevice()
        {
            this.Show();
             this.Text = "���վ��������";
            this.WindowState = FormWindowState.Maximized;
            if(!string.IsNullOrEmpty(this.smmprog.ProgName))
            this.Text = this.smmprog.ProgName;
             string[] type=new string[]{"20"};
             InitDeviceType(type);
             splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            
             
        }
        //��Դ���
        public void dydevice()
        {
            this.Show();
            this.Text = "��Դ��������";
            this.WindowState = FormWindowState.Maximized;
            if (!string.IsNullOrEmpty(this.smmprog.ProgName))
                this.Text = this.smmprog.ProgName;
            string[] type = new string[] { "30" };
            InitDeviceType(type);
            splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
        }
        //��·���
        public void xldevice()
        {
            this.Show();
            this.Text = "��Դ��������";
            this.WindowState = FormWindowState.Maximized;
            if (!string.IsNullOrEmpty(this.smmprog.ProgName))
                this.Text = this.smmprog.ProgName;
            string[] type = new string[] { "05" };
            InitDeviceType(type);
            splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
        }
        //�޹��豸���
        public void wgdevice()
        {
            this.Show();
            this.Text = "�޹���������";
            this.WindowState = FormWindowState.Maximized;
            if (!string.IsNullOrEmpty(this.smmprog.ProgName))
                this.Text = this.smmprog.ProgName;
            string[] type = new string[] { "06","08","09","10","11","13","14","15","40" };
            InitDeviceType(type);
            //splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            treeList1.FocusedNode = null;
        }
        //����豸���
        public void pddevice()
        {
            this.Show();
            this.Text = "��Դ��������";
            this.WindowState = FormWindowState.Maximized;
            if (!string.IsNullOrEmpty(this.smmprog.ProgName))
                this.Text = this.smmprog.ProgName;
            string[] type = new string[] { "73","55","74","75","80","70"};
            InitDeviceType(type);
            //splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            treeList1.FocusedNode = null;
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
            splitContainerControl1.Panel2.Controls.Add(device);
        }
        #endregion

        #region ��¼����
        protected override void Add() {
            if (curDevice != null)
            {
                curDevice.ParentID = parentid;
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

        #region �ֶ�
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
                op.Filter = "Excel�ļ�(*.xls)|*.xls";
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
                MessageBox.Show("û��ѡ���豸�����û�������һ����", "����EXCEL", MessageBoxButtons.OK);
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
                    curDevice.strCon = " where 1=1 and SvgUID='" + ((PSP_Substation_Info)ParentObj).UID + "'and";
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