using System;
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
using Itop.Client.Base;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Itop.TLPSP.DEVICE;
using System.Reflection;
namespace Itop.TLPSP.DEVICE
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
                Init();
            }
        }
        private void Init()
        {
           // TreeListColumn column1 = new TreeListColumn();
           // column1.Caption = "样式";
           // column1.FieldName = "devicetype";
           // column1.VisibleIndex = -1;
           // column1.OptionsColumn.AllowEdit = false;
           // column1.OptionsColumn.AllowSort = false;

           // TreeListColumn column = new TreeListColumn();
           // column.Caption = "元件";
           // column.FieldName = "title";
           // column.VisibleIndex =0;
           // column.Width = 40;
           // column.OptionsColumn.AllowEdit = false;
           // column.OptionsColumn.AllowSort = false;
           // treeList1.Columns.AddRange(new TreeListColumn[] {
           //column1, column});
            treeList1.KeyFieldName = "ID";
            treeList1.ParentFieldName = "ParentID";
            IList<Ps_pdtypenode> list1 = Services.BaseService.GetList<Ps_pdtypenode>("SelectPs_pdtypenodeByCon", "pdreltypeid='" + pdreltypeid + "' ORDER BY SUBSTRING(Code, 3, 1)");
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
            //return Assembly.GetExecutingAssembly().CreateInstance(classname, false) as UCDeviceBase;
            return Assembly.Load("Itop.TLPSP.DEVICE").CreateInstance(classname, false) as UCDeviceBase;
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
                curDevice.strCon = " where 1=1 and suid='1111' and";
                curDevice.Init();
            }
        }
        private void treeList1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) return;
            TreeListNode node = treeList1.FocusedNode;
            if (node == null) return;
            string deviceid = node["DeviceID"].ToString();
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
                curDevice.strCon = " where 1=1 and suid='" + deviceid + "'and ";
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
                    curDevice.ParentID = v.DeviceID;
                    curDevice.Add();
                    PSPDEV pd = curDevice.SelectedDevice as PSPDEV;
                    //馈线段记录
                    if (pd == null)
                        return;
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
                    curDevice.ParentID = v.DeviceID;
                    curDevice.Add();

                    PSPDEV pd = curDevice.SelectedDevice as PSPDEV;
                    //馈线段记录
                    if (pd == null)
                        return;
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
                    curDevice.ParentID = v.DeviceID;
                    curDevice.Add();

                    PSPDEV pd = curDevice.SelectedDevice as PSPDEV;
                    //馈线段记录
                    if (pd == null)
                        return;
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
                    curDevice.ParentID = v.DeviceID;
                    curDevice.Add();

                    PSPDEV pd = curDevice.SelectedDevice as PSPDEV;
                    //馈线段记录
                    if (pd == null)
                        return;
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
                    curDevice.ParentID = v.DeviceID;
                    curDevice.Add();

                    PSPDEV pd = curDevice.SelectedDevice as PSPDEV;
                    //馈线段记录
                    if (pd == null)
                        return;
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
                    curDevice.ParentID = v.DeviceID;
                    curDevice.Add();

                    PSPDEV pd = curDevice.SelectedDevice as PSPDEV;
                    //馈线段记录
                    if (pd == null)
                        return;
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

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode tln = treeList1.FocusedNode;
            frmDeviceSelect dlg = new frmDeviceSelect();
            if (tln != null)
            {
                DataRow row = (tln.TreeList.GetDataRecordByNode(tln) as DataRowView).Row;
                Ps_pdtypenode v = DataConverter.RowToObject<Ps_pdtypenode>(row);
                if (tln.GetValue("devicetype").ToString() == "01")
                {
                    //adducdevice("73");
                    //curDevice.Add();
                    //PSPDEV pd = curDevice.SelectedDevice as PSPDEV;
                    //馈线段记录
                    dlg.InitDeviceType("01", "73");
                }
                else if (tln.GetValue("devicetype").ToString() == "73")
                {
                    dlg.InitDeviceType("73", "74", "80", "75");

                }
                else if (tln.GetValue("devicetype").ToString() == "74")
                {
                    dlg.InitDeviceType("06", "55");

                }
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Dictionary<string, object> dic = dlg.GetSelectedDevice();
                    PSPDEV devzx = dic["device"] as PSPDEV;
                    //S1 = devzx.SUID;
                    Ps_pdtypenode pn = new Ps_pdtypenode();
                    pn.title = devzx.Name;
                    pn.pdreltypeid = pdreltypeid;
                    pn.devicetype = devzx.Type;
                    pn.DeviceID = devzx.SUID;
                    if (devzx.Type != "01")
                    {
                        pn.ParentID = tln.GetValue("ID").ToString();
                    }
                    switch (devzx.Type)
                    {
                        case "73":
                            pn.Code = (tln.Level + 1).ToString() + "1" + (tln.Nodes.Count + 1).ToString();
                            break;
                        case "74":
                            pn.Code = (tln.Level + 1).ToString() + "2" + (tln.Nodes.Count + 1).ToString();
                            break;
                        case "80":
                            pn.Code = (tln.Level + 1).ToString() + "3" + (tln.Nodes.Count + 1).ToString();
                            break;
                        case "75":
                            pn.Code = (tln.Level + 1).ToString() + "5" + (tln.Nodes.Count + 1).ToString();
                            break;
                        case "06":
                            pn.Code = (tln.Level + 1).ToString() + "4" + (tln.Nodes.Count + 1).ToString();
                            break;
                        case "55":
                            pn.Code = (tln.Level + 1).ToString() + "6" + (tln.Nodes.Count + 1).ToString();
                            break;
                        case "01":
                            pn.Code = treeList1.Nodes.Count.ToString();
                            break;
                    }

                    Services.BaseService.Create<Ps_pdtypenode>(pn);
                    dt.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pn, dt.NewRow()));
                }
            }
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPDEV psd = new PSPDEV();
            psd.SUID = parentobj.ID;
            psd = Services.BaseService.GetOneByKey<PSPDEV>(psd);
            if (psd!=null)
            {
                XtraPDrelfrm xf = new XtraPDrelfrm();
                xf.ParentObj = psd;
                xf.init();
                xf.ShowDialog();
            }
          
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Init();  //刷新元件树
            DataTable dt = new DataTable();
            frmfxlx fm = new frmfxlx();
            if (fm.ShowDialog()==DialogResult.OK)
            {
                dt = fm.DT1;
            }
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["B"])==1)
                {
                    TreeListNode tln = treeList1.FindNodeByKeyID(pdreltypeid);
                    relanalsy(tln,Convert.ToInt32(dr["D"]));
                }
            }
        }
        //XL为分析的线路 
        private void relanalsy(TreeListNode xl, int fxtype)
        {
            foreach (TreeListNode tln in xl.Nodes)
            {
                if (tln.GetValue("devicetype").ToString() == "73" && string.IsNullOrEmpty(tln.GetValue("S1").ToString()))
                {
                    //对子线路进行等值分析
                    dzanalsy(tln, fxtype);
                }
            }
            
            //求取主线相关联的负荷节点的 停电率 停电时间等
            //各个线路段对该层负荷点的 故障率和停运时间的影响
            Dictionary<string ,List<rresult>> rescol=new Dictionary<string,List<rresult>>();


        }
        //各个元件对负荷点的影响分析
        private void yjfx(TreeListNode xl,ref  Dictionary<string ,List<rresult>> rescol,int fxtype)
        {
            rresult rs = new rresult();
            List<yjandjd> xldcol = new List<yjandjd>();  //线路段集合
            List<yjandjd> fhzlcol = new List<yjandjd>();  //负荷支路集合
            List<PSPDEV> xlcol = new List<PSPDEV>();    //分支线路
            List<yjandjd> luxcol = new List<yjandjd>();//联络线集合
            
            for (int i = 0; i < xl.Nodes.Count;i++ )
            {

                if (xl.Nodes[i].GetValue("devicetype").ToString() == "74" && !xl.Nodes[i].GetValue("title").ToString().Contains("节点有问题"))
                {
                    PSPDEV pd = new PSPDEV();
                    pd.SUID = xl.Nodes[i].GetValue("DeviceID").ToString();
                    pd = Services.BaseService.GetOneByKey<PSPDEV>(pd);
                    if (pd!=null)
                    {
                       string sql = "where SUID='" +pd.IName+ "'and AreaID='" +xl.GetValue("ID").ToString() + "'and type='70'";
                        PSPDEV firstnode = UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", sql) as PSPDEV;
                        sql = "where SUID='" + pd.JName + "'and AreaID='" + xl.GetValue("ID").ToString() + "'and type='70'";
                        PSPDEV lastnode = UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", sql) as PSPDEV;
                        yjandjd jd = new yjandjd(pd, firstnode, lastnode);
                        xldcol.Add(jd);
                    }
                }
                else if (xl.Nodes[i].GetValue("devicetype").ToString() == "80" && !xl.Nodes[i].GetValue("title").ToString().Contains("节点有问题"))
                {
                    PSPDEV pd = new PSPDEV();
                    pd.SUID = xl.Nodes[i].GetValue("DeviceID").ToString();
                    pd = Services.BaseService.GetOneByKey<PSPDEV>(pd);
                    if (pd != null)
                    {
                        string sql = "where SUID='" + pd.IName + "'and AreaID='" + xl.GetValue("ID").ToString() + "'and type='70'";
                        PSPDEV firstnode = UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", sql) as PSPDEV;
                       
                        yjandjd jd = new yjandjd(pd, firstnode, null);
                        fhzlcol.Add(jd);
                    }
                }
                else if (xl.Nodes[i].GetValue("devicetype").ToString() == "75" && !xl.Nodes[i].GetValue("title").ToString().Contains("节点有问题"))
                {
                    PSPDEV pd = new PSPDEV();
                    pd.SUID = xl.Nodes[i].GetValue("DeviceID").ToString();
                    pd = Services.BaseService.GetOneByKey<PSPDEV>(pd);
                    if (pd != null)
                    {
                        PSPDEV firstnode=new PSPDEV();
                        if (pd.IName == xl.GetValue("ID").ToString())
                        {
                            string sql = "where SUID='" + pd.HuganLine1 + "'and AreaID='" + xl.GetValue("ID").ToString() + "'and type='70'";
                            firstnode = UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", sql) as PSPDEV;
                        }
                        if (pd.JName == xl.GetValue("ID").ToString())
                        {
                            string sql = "where SUID='" + pd.HuganLine2 + "'and AreaID='" + xl.GetValue("ID").ToString() + "'and type='70'";
                            firstnode = UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", sql) as PSPDEV;
                        }

                       
                        yjandjd jd = new yjandjd(pd, firstnode, null);
                        luxcol.Add(jd);
                    }
                }
                else if (xl.Nodes[i].GetValue("devicetype").ToString() == "73" )
                {
                    PSPDEV pd = new PSPDEV();
                    pd.SUID = xl.Nodes[i].GetValue("DeviceID").ToString();
                    pd = Services.BaseService.GetOneByKey<PSPDEV>(pd);
                    if (pd != null)
                    {
                        xlcol.Add(pd);
                    }
                }

            }
            //然后依次的分析
            for (int i = 0; i < xldcol.Count;i++ )
            {
                //判断该线路段是否有开关 如果有则直接进行
                yjandjd ps = xldcol[i];
                List<rresult> listfhtyl=new List<rresult>();   //记录元件对本线路各个负荷的影响
                rresult result=new rresult();
               string sql = "where IName='" + ps.YJ.SUID + "'and (type='06'or type='55')"; 
                IList<PSPDEV> listkg = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
               
                if (listkg.Count>0)    //线路段有开关
                {
                  for (int j=0;j<fhzlcol.Count;j++)
                  {
                      if (xldcol[i].FirstNode.Number>=fhzlcol[i].FirstNode.Number)          //负荷支路比起元件更靠近电源  停电方式都是一个样
                      {
                          result.deviceid=fhzlcol[j].YJ;
                          result.gzl=xldcol[i].YJ.HuganTQ3;
                          if (listkg[0].Type=="06")
                          {
                                 result.tysj=listkg[0].HuganTQ2;
                          }
                          else
                             result.tysj=listkg[0].HuganTQ1;
                          result.ntysj=result.gzl*result.tysj; 
                          listfhtyl.Add(result);

                         
                      }
                      else if (xldcol[i].LastNode.Number<fhzlcol[i].FirstNode.Number)
                      {
                         switch (fxtype)
                          {
                          case 1 :  //第一种方式
                                 result.deviceid=fhzlcol[j].YJ;
                                  result.gzl=xldcol[i].YJ.HuganTQ3;
                                 result.tysj=xldcol[i].YJ.HuganTQ4;
                                 result.ntysj=result.gzl*result.tysj; 
                                 listfhtyl.Add(result);
                            break;
                              case 2:  //第二种方式  判断联络线的节点的编号和firstnode的比较 如果大于它则影响不了前面 如果小于的话
                                 if (luxcol.Count>0)
                                 {
                                     if (luxcol[0].FirstNode.Number<xldcol[i].LastNode.Number)
                                     {
                                          result.deviceid=fhzlcol[j].YJ;
                                          result.gzl=xldcol[i].YJ.HuganTQ3;
                                         result.tysj=xldcol[i].YJ.HuganTQ4;
                                         result.ntysj=result.gzl*result.tysj; 
                                         listfhtyl.Add(result);
                                     }
                                     else
                                     {
                                          result.deviceid=fhzlcol[j].YJ;
                                          result.gzl=xldcol[i].YJ.HuganTQ3;
                                          if (listkg[0].Type=="06")
                                          {
                                              result.tysj=listkg[0].HuganTQ2>=luxcol[0].YJ.HuganTQ3?listkg[0].HuganTQ2:luxcol[0].YJ.HuganTQ3;
                                          }
                                          else
                                             result.tysj=listkg[0].HuganTQ1>=luxcol[0].YJ.HuganTQ3?listkg[0].HuganTQ1:luxcol[0].YJ.HuganTQ3;
                                          result.ntysj=result.gzl*result.tysj; 
                                          listfhtyl.Add(result);
                                     }
                                 }
                                 else
                                 {
                                          result.deviceid=fhzlcol[j].YJ;
                                          result.gzl=xldcol[i].YJ.HuganTQ3;
                                          if (listkg[0].Type=="06")
                                          {
                                              result.tysj=listkg[0].HuganTQ2>=luxcol[0].YJ.HuganTQ3?listkg[0].HuganTQ2:luxcol[0].YJ.HuganTQ3;
                                          }
                                          else
                                             result.tysj=listkg[0].HuganTQ1>=luxcol[0].YJ.HuganTQ3?listkg[0].HuganTQ1:luxcol[0].YJ.HuganTQ3;
                                          result.ntysj=result.gzl*result.tysj; 
                                          listfhtyl.Add(result);
                                 }
                                  
                                
                                  break;
                               case 3:  //第三种方式  判断联络线的节点的编号和firstnode的比较 如果大于它则影响不了前面 如果小于的话
                                           result.deviceid=fhzlcol[j].YJ;
                                          result.gzl=xldcol[i].YJ.HuganTQ3;
                                         result.tysj=xldcol[i].YJ.HuganTQ4*0.5;
                                         result.ntysj=result.gzl*result.tysj; 
                                         listfhtyl.Add(result);
                            
                              break;
                             case 4:       //第四种方式
                                    if (luxcol.Count>0)
                                 {
                                     if (luxcol[0].FirstNode.Number<xldcol[i].LastNode.Number)
                                     {
                                          result.deviceid=fhzlcol[j].YJ;
                                          result.gzl=xldcol[i].YJ.HuganTQ3;
                                         result.tysj=xldcol[i].YJ.HuganTQ4;
                                         result.ntysj=result.gzl*result.tysj; 
                                         listfhtyl.Add(result);
                                     }
                                     else
                                     {
                                          result.deviceid=fhzlcol[j].YJ;
                                          result.gzl=xldcol[i].YJ.HuganTQ3;
                                          if (listkg[0].Type=="06")
                                          {
                                              result.tysj=listkg[0].HuganTQ2;
                                          }
                                          else
                                             result.tysj=listkg[0].HuganTQ1;
                                          result.ntysj=result.gzl*result.tysj; 
                                          listfhtyl.Add(result);
                                     }
                                 }
                                 else
                                 {
                                          result.deviceid=fhzlcol[j].YJ;
                                          result.gzl=xldcol[i].YJ.HuganTQ3;
                                          if (listkg[0].Type=="06")
                                          {
                                              result.tysj=listkg[0].HuganTQ2;
                                          }
                                          else
                                             result.tysj=listkg[0].HuganTQ1;
                                          result.ntysj=result.gzl*result.tysj; 
                                          listfhtyl.Add(result);
                                 }
                                 break;
                             case 5:      //第五种方式
                                   if (luxcol.Count>0)
                                 {
                                     if (luxcol[0].FirstNode.Number<xldcol[i].LastNode.Number)
                                     {
                                          result.deviceid=fhzlcol[j].YJ;
                                          result.gzl=xldcol[i].YJ.HuganTQ3;
                                         result.tysj=xldcol[i].YJ.HuganTQ4;
                                         result.ntysj=result.gzl*result.tysj; 
                                         listfhtyl.Add(result);
                                     }
                                     else
                                     {
                                          result.deviceid=fhzlcol[j].YJ;
                                          result.gzl=xldcol[i].YJ.HuganTQ3;
                                          if (listkg[0].Type=="06")
                                          {
                                              result.tysj=listkg[0].HuganTQ2;
                                          }
                                          else
                                             result.tysj=listkg[0].HuganTQ1;
                                          result.ntysj=result.gzl*result.tysj; 
                                          listfhtyl.Add(result);
                                     }
                                 }
                                 else
                                 {
                                          result.deviceid=fhzlcol[j].YJ;
                                          result.gzl=xldcol[i].YJ.HuganTQ3;
                                          if (listkg[0].Type=="06")
                                          {
                                              result.tysj=listkg[0].HuganTQ2;
                                          }
                                          else
                                             result.tysj=listkg[0].HuganTQ1;
                                          result.ntysj=result.gzl*result.tysj; 
                                          listfhtyl.Add(result);
                                 }
                                 break;
                          }
                      }
                  }
                }
                else  //如果没有 找到前后线路段具有开关的
                {

                }
               
                for (int j = i + 1; j < xlcol.Count;j++ )
                {
                }
            }
        }
        //等值化分析
        private void dzanalsy(TreeListNode xl, int fxtype)
        {
            //判断是否有分支线 替代等值过程 将其分支线路的 停电率和故障时间等效到此节点上类型 关系表的S1=‘1’表示上行 S1=‘2’代表下行 D1 D2 D3 等效的值
            foreach (TreeListNode tln in xl.Nodes)
            {
                if (tln.GetValue("devicetype").ToString() == "73" && string.IsNullOrEmpty(tln.GetValue("S1").ToString()))
                {
                    tln.SetValue("S1", "1");

                    dzanalsy(tln, fxtype);
                 
                }

            }
            //判断支路信息将其等值化
            Zxdzh(xl, fxtype);
           
        }
        private void Zxdzh(TreeListNode zxl,int fxtype)
        {
            double gzl = 0, U=0, gztime = 0,dklv=0;
          //找到第一个线路段判断是否有断路器
            bool glkgflag = false;
            for (int i = 0; i < zxl.Nodes.Count;i++ )
            {
                if (zxl.Nodes[i].GetValue("devicetype").ToString()=="74")
                {
                    if (zxl.Nodes[i].Nodes.Count>0)
                    {
                        glkgflag = true;
                        PSPDEV pd =new PSPDEV();
                        pd.SUID=zxl.Nodes[i].Nodes[0].GetValue("DeviceID").ToString();
                         pd=Services.BaseService.GetOneByKey<PSPDEV>(pd);
                        if (pd!=null)
                        {
                            if (pd.Type=="06")
                            {
                                dklv=Convert.ToDouble(pd.HuganFirst);
                                gztime = pd.HuganTQ2;
                            }
                            else if (pd.Type=="55")
                            {
                                dklv = pd.HuganTQ4;
                                gztime = pd.HuganTQ1;
                            }
                          
                        }
                        
                    }
                    break;
                }
            }
            //如果首段存在断路器进行等值 第一种情况
            foreach (TreeListNode tln in zxl.Nodes)
            {
                if (tln.GetValue("devicetype").ToString() == "74")
                {
                    PSPDEV pd = new PSPDEV();
                    pd.SUID = tln.GetValue("DeviceID").ToString();
                    pd = Services.BaseService.GetOneByKey<PSPDEV>(pd);
                    if (pd != null)
                    {
                        gzl += pd.HuganTQ3;
                        U += (pd.HuganTQ3 * pd.HuganTQ4);
                    }

                }
                if (tln.GetValue("devicetype").ToString() == "80")
                {
                    PSPDEV pd = new PSPDEV();
                    pd.SUID = tln.GetValue("DeviceID").ToString();
                    pd = Services.BaseService.GetOneByKey<PSPDEV>(pd);
                    if (pd != null)
                    {
                        gzl += pd.HuganTQ2;
                        U += (pd.HuganTQ2 * pd.HuganTQ3);
                    }
                }
                if (tln.GetValue("devicetype").ToString() == "73" && tln.GetValue("S1").ToString() == "1")
                {
                    gzl += Convert.ToDouble(tln.GetValue("D1"));
                    U += Convert.ToDouble(tln.GetValue("D1")) * Convert.ToDouble(tln.GetValue("D2"));
                }
            }
            //等值的过程
            if (glkgflag)
            {
              
                zxl.SetValue("D1", (1 - dklv) * gzl);
                zxl.SetValue("D2", gztime);
            }
            else
            {
                zxl.SetValue("D1", gzl);
                zxl.SetValue("D2", U/gzl);
            }
           
        }

    }
}
