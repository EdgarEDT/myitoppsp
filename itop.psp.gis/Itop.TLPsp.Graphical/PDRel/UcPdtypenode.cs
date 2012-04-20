﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using System.Collections;
using Itop.Common;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.IO;
using Itop.Client.Stutistics;
using System.Xml;
using ItopVector.Tools;
using Itop.Client.Base;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Itop.TLPSP.DEVICE;
using System.Reflection;
namespace Itop.TLPsp.Graphical
{
    public partial class UcPdtypenode : DevExpress.XtraEditors.XtraUserControl
    {
        public UcPdtypenode()
        {
            InitializeComponent();
        }
        private Ps_pdreltype parentobj=new Ps_pdreltype();
        private string pdreltypeid;
        DataTable dt = new DataTable();
        public Ps_pdreltype ParentObj
        {
            get
            {
                return parentobj;
            }
            set
            {
                parentobj = value;
                pdreltypeid = parentobj.ID;
            }
        }
        private void Init(string pdreltype)
        {
            TreeListColumn column1 = new TreeListColumn();
            column1.Caption = "样式";
            column1.FieldName = "devicetype";
            column1.VisibleIndex = 0;
            column1.Width = 20;
            column1.OptionsColumn.AllowEdit = false;
            column1.OptionsColumn.AllowSort = false;

            TreeListColumn column = new TreeListColumn();
            column.Caption = "元件";
            column.FieldName = "title";
            column.VisibleIndex =1;
            column.Width = 90;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            treeList1.Columns.AddRange(new TreeListColumn[] {
            column,column1});
            treeList1.KeyFieldName = "ID";
            treeList1.ParentFieldName = "Parentid";
            IList<Ps_pdtypenode> list1 = Services.BaseService.GetList<Ps_pdtypenode>("SelectPs_pdtypenodeByCon", "pdreltypeid='" + pdreltypeid + "'");
            dt = Itop.Common.DataConverter.ToDataTable((IList)list1, typeof(Ps_pdtypenode));
            this.treeList1.DataSource = dt;
        }
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

        #endregion
       //添加元件所关联的设备
        private void adducdevice(string Devicetype)
        {

            string dtype = DeviceTypeHelper.DeviceClassbyType(Devicetype);
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
                device.ID = Devicetype;
                try
                {
                    device.Show();
                }
                catch { }
            }
            else
            {
                device = createInstance(dtype);
                device.ID = Devicetype;
                device.ProjectID = Itop.Client.MIS.ProgUID;
                devicTypes.Add(dtype, device);
                showDevice(device);
            }

            if (curDevice != null && curDevice != device) curDevice.Hide();
            curDevice = device;
            if (curDevice != null)
            {
                
                //给一个空的选择
                curDevice.strCon = " where 1=1 and suid='1111'";
                curDevice.Init();
            }
        }
        private void treeList1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) return;
            TreeListNode node = treeList1.FocusedNode;
            if (node == null) return;
            string deviceid=node["ID"].ToString();
            string strID = node["devicetype"].ToString();
            string dtype = DeviceTypeHelper.DeviceClassbyType(strID);
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
                curDevice.strCon = " where 1=1 and suid='" + deviceid + "'";
                curDevice.Init();
            }
        }

        private void treeList1_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            string stateimage = e.Node.GetValue("devicetype").ToString();
            switch (stateimage)
            {
                case "01":
                    e.NodeImageIndex = 33;
                    break;
                case "73":
                    e.NodeImageIndex = 28;
                    break;
                case "74":
                    e.NodeImageIndex = 29;
                    break;
                case "55":
                    e.NodeImageIndex = 31;
                    break;
                case "06":
                    e.NodeImageIndex = 26;
                    break;
                case "80":
                    e.NodeImageIndex = 30;
                    break;
                case "75":
                    e.NodeImageIndex = 32;
                    break;
                default:
                    e.NodeImageIndex = 28;
                    break;
            }
        }
#region  添加元件
        //添加馈线
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            TreeListNode tln = treeList1.FocusedNode;

            if (tln != null)
            {
                DataRow row = (tln.TreeList.GetDataRecordByNode(tln) as DataRowView).Row;
                Ps_pdtypenode v = DataConverter.RowToObject<Ps_pdtypenode>(row);
                if (tln.GetValue("devicetype").ToString() == "01" || tln.GetValue("devicetype").ToString() == "73")
                {
                    adducdevice("73");
                    curDevice.Add();
                    PSPDEV pd = curDevice.SelectedDevice as PSPDEV;
                    //馈线段记录
                    Ps_pdtypenode pn = new Ps_pdtypenode();
                    pn.title = pd.Name;
                    pn.pdreltypeid = pdreltypeid;
                    pn.devicetype = "73";
                    pn.DeviceID = pd.SUID;
                    pn.ParentID = tln.GetValue("ID").ToString();
                    pn.Code = (tln.Level + 1).ToString() + "1" + (tln.Nodes.Count + 1).ToString();
                    Services.BaseService.Create<Ps_pdtypenode>(pn);
                    dt.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pn, dt.NewRow()));
                }
                else
                {
                    MsgBox.Show("请选择电源后，再操作！");
                    return;
                }
            }
        }
        //添加馈线段
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode tln = treeList1.FocusedNode;

            if (tln != null)
            {
                DataRow row = (tln.TreeList.GetDataRecordByNode(tln) as DataRowView).Row;
                Ps_pdtypenode v = DataConverter.RowToObject<Ps_pdtypenode>(row);
                if (tln.GetValue("devicetype").ToString() == "73")
                {
                    adducdevice("74");
                    curDevice.Add();

                    PSPDEV pd = curDevice.SelectedDevice as PSPDEV;
                    //馈线段记录
                    Ps_pdtypenode pn = new Ps_pdtypenode();
                    pn.title = pd.Name;
                    pn.pdreltypeid = pdreltypeid;
                    pn.devicetype = "74";
                    pn.DeviceID = pd.SUID;
                    pn.ParentID = tln.GetValue("ID").ToString();
                    pn.Code = (tln.Level + 1).ToString() + "2" + (tln.Nodes.Count + 1).ToString();
                    Services.BaseService.Create<Ps_pdtypenode>(pn);
                    dt.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pn, dt.NewRow()));
                }
                else
                {
                    MsgBox.Show("请选择馈线，再操作！");
                    return;
                }
            }
        }
        //添加负荷之路
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode tln = treeList1.FocusedNode;

            if (tln != null)
            {
                DataRow row = (tln.TreeList.GetDataRecordByNode(tln) as DataRowView).Row;
                Ps_pdtypenode v = DataConverter.RowToObject<Ps_pdtypenode>(row);
                if (tln.GetValue("devicetype").ToString() == "73")
                {
                    adducdevice("80");
                    curDevice.Add();

                    PSPDEV pd = curDevice.SelectedDevice as PSPDEV;
                    //馈线段记录
                    Ps_pdtypenode pn = new Ps_pdtypenode();
                    pn.title = pd.Name;
                    pn.pdreltypeid = pdreltypeid;
                    pn.devicetype = "80";
                    pn.DeviceID = pd.SUID;
                    pn.ParentID = tln.GetValue("ID").ToString();
                    pn.Code = (tln.Level + 1).ToString() + "3" + (tln.Nodes.Count + 1).ToString();
                    Services.BaseService.Create<Ps_pdtypenode>(pn);
                    dt.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pn, dt.NewRow()));
                }
                else
                {
                    MsgBox.Show("请选择馈线，再操作！");
                    return;
                }
            }
        }
        //添加联络线
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode tln = treeList1.FocusedNode;

            if (tln != null)
            {
                DataRow row = (tln.TreeList.GetDataRecordByNode(tln) as DataRowView).Row;
                Ps_pdtypenode v = DataConverter.RowToObject<Ps_pdtypenode>(row);
                if (tln.GetValue("devicetype").ToString() == "73")
                {
                    adducdevice("75");
                    curDevice.Add();

                    PSPDEV pd = curDevice.SelectedDevice as PSPDEV;
                    //馈线段记录
                    Ps_pdtypenode pn = new Ps_pdtypenode();
                    pn.title = pd.Name;
                    pn.pdreltypeid = pdreltypeid;
                    pn.devicetype = "75";
                    pn.DeviceID = pd.SUID;
                    pn.ParentID = tln.GetValue("ID").ToString();
                    pn.Code = (tln.Level + 1).ToString() + "5" + (tln.Nodes.Count + 1).ToString();
                    Services.BaseService.Create<Ps_pdtypenode>(pn);
                    dt.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pn, dt.NewRow()));
                }
                else
                {
                    MsgBox.Show("请选择馈线，再操作！");
                    return;
                }
            }
        }
        //添加断路器
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode tln = treeList1.FocusedNode;

            if (tln != null)
            {
                DataRow row = (tln.TreeList.GetDataRecordByNode(tln) as DataRowView).Row;
                Ps_pdtypenode v = DataConverter.RowToObject<Ps_pdtypenode>(row);
                if (tln.GetValue("devicetype").ToString() == "74")
                {
                    adducdevice("06");
                    curDevice.Add();

                    PSPDEV pd = curDevice.SelectedDevice as PSPDEV;
                    //馈线段记录
                    Ps_pdtypenode pn = new Ps_pdtypenode();
                    pn.title = pd.Name;
                    pn.pdreltypeid = pdreltypeid;
                    pn.devicetype = "06";
                    pn.DeviceID = pd.SUID;
                    pn.ParentID = tln.GetValue("ID").ToString();
                    pn.Code = (tln.Level + 1).ToString() + "4" + (tln.Nodes.Count + 1).ToString();
                    Services.BaseService.Create<Ps_pdtypenode>(pn);
                    dt.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pn, dt.NewRow()));
                }
                else
                {
                    MsgBox.Show("请选择馈线段，再操作！");
                    return;
                }
            }
        }
        //添加隔离开关
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode tln = treeList1.FocusedNode;

            if (tln != null)
            {
                DataRow row = (tln.TreeList.GetDataRecordByNode(tln) as DataRowView).Row;
                Ps_pdtypenode v = DataConverter.RowToObject<Ps_pdtypenode>(row);
                if (tln.GetValue("devicetype").ToString() == "74")
                {
                    adducdevice("55");
                    curDevice.Add();

                    PSPDEV pd = curDevice.SelectedDevice as PSPDEV;
                    //馈线段记录
                    Ps_pdtypenode pn = new Ps_pdtypenode();
                    pn.title = pd.Name;
                    pn.pdreltypeid = pdreltypeid;
                    pn.devicetype = "55";
                    pn.DeviceID = pd.SUID;
                    pn.ParentID = tln.GetValue("ID").ToString();
                    pn.Code = (tln.Level + 1).ToString() + "6" + (tln.Nodes.Count + 1).ToString();
                    Services.BaseService.Create<Ps_pdtypenode>(pn);
                    dt.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pn, dt.NewRow()));
                }
                else
                {
                    MsgBox.Show("请选择馈线段，再操作！");
                    return;
                }
            }
        }
#endregion
        //修改元件
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode tln = treeList1.FocusedNode;

            if (tln != null)
            {
                DataRow row = (tln.TreeList.GetDataRecordByNode(tln) as DataRowView).Row;
                Ps_pdtypenode v = DataConverter.RowToObject<Ps_pdtypenode>(row);

                curDevice.Edit();

                PSPDEV pd = curDevice.SelectedDevice as PSPDEV;
                v.title = pd.Name;
                Services.BaseService.Update<Ps_pdtypenode>(v);
                tln.SetValue("title", v.title);       
            }
        }
        //删除元件
        public void DeleteNode(TreeListNode tln)
        {

            
            if (tln.HasChildren)
            {
                for (int i = 0; i < tln.Nodes.Count; i++)
                {
                    DeleteNode(tln.Nodes[i]);
                }
                DeleteNode(tln);
            }
            else
            {
                Ps_pdtypenode pf = new Ps_pdtypenode();
                pf.ID = tln["ID"].ToString();
                string nodestr = tln["AreaName"].ToString();
                try
                {
                    TreeListNode node = tln.TreeList.FindNodeByKeyID(pf.ID);
                    if (node != null)
                        tln.TreeList.DeleteNode(node);
                    RemoveDataTableRow(dt, pf.ID);
                    Services.BaseService.Delete<Ps_pdtypenode>(pf);


                }
                catch (Exception e)
                {

                    MessageBox.Show(e.Message + "删除结点出错！");
                }

            }


        }
        public void RemoveDataTableRow(DataTable dt, string ID)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["ID"].ToString() == ID)
                {
                    dt.Rows.RemoveAt(i);
                    break;
                }
            }
        }
      
      

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
            {
                return;
            }

           
            if (MsgBox.ShowYesNo("是否删除？") != DialogResult.Yes)
            {
                return;
            }
            DeleteNode(tln);
        }

       
   



    }
}
