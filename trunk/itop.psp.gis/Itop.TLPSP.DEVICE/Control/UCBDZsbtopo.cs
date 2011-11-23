using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using DevExpress.XtraTreeList.Columns;
using DevExpress.Utils;

namespace Itop.TLPSP.DEVICE
{
    public partial class UCBDZsbtopo : UserControl
    {
        public UCBDZsbtopo() {
            InitializeComponent();
            addTreeColumns();
            addTableColumns();
            msgbox.Hide();
            //treeList1.BeforeExpand += new DevExpress.XtraTreeList.BeforeExpandEventHandler(treeList1_BeforeExpand);
            //treeList1.AfterExpand += new DevExpress.XtraTreeList.NodeEventHandler(treeList1_AfterExpand);
        }
        void treeList1_AfterExpand(object sender, DevExpress.XtraTreeList.NodeEventArgs e) {
        }
        void treeList1_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e) {
        }
        static WaitDialogForm msgbox = new WaitDialogForm("", "分析数据，请稍候。。。");
        string bdzuid = "";
        DataTable sbTable;
        PSPDEV pdev;
        bool showdeep;
        Dictionary<string, PSPDEV> topodevdic = new Dictionary<string, PSPDEV>();
        /// <summary>
        /// 深度拓扑分析
        /// </summary>
        public bool Showdeep {
            get { return showdeep; }
            set { showdeep = value; }
        }
        public string BDZUID {
            get { return bdzuid; }
            set {
                bdzuid = value;
            }
        }
        /// <summary>
        /// 用于分析拓扑的父结点设备包括（母线、线路）
        /// </summary>
        public PSPDEV ParentDev {
            get { return pdev; }
            set {
                pdev = value; showdeep = true;
            }
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            try {
                freshmx();
            } catch { }
        }
        /// <summary>
        /// 线路
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pid"></param>
        private void initxl(string uid, string pid,int grade) {
            //if (topodevdic.ContainsKey(uid)) return;
            //topodevdic.Add(uid, null);
            string sql = "where (iname='" + pid + "' or jname='" + pid + "') and projectid='" + Itop.Client.MIS.ProgUID + "' and Type='05' ORDER BY Number";
            IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            string spid = "";         
            foreach (PSPDEV dev in list) {
                DataRow row = addrow(dev.SUID, dev.SUID, uid, dev.Type, dev.Number, dev.Name,grade,dev.RateVolt);
                initzx(dev.SUID, dev.Name,grade+1);
                initkbs(dev.SUID, dev.Name, grade + 1);
                initdx(dev.SUID, dev.Name, grade + 1);
                //initdrq(dev.SUID, dev.Name);
                if (row == null) continue;
                if (dev.IName != pid) {
                    spid = dev.IName;
                } else {
                    spid = dev.JName;
                }
                if (!string.IsNullOrEmpty(spid)) {
                    initmx(dev.SUID, spid);
                }
            }
        }
        /// <summary>
        ///导线
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pid"></param>
        /// <summary>
    
        private void initdx(string uid, string pid, int grade)
        {
            string sql = "where (iname='" + uid + "') and projectid='" + Itop.Client.MIS.ProgUID + "' ORDER BY Number";
            IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            string spid = "";
            foreach (PSPDEV dev in list)
            {
               PSPDEV jDevice =  UCDeviceBase.DataService.GetOneByKey<PSPDEV>(dev.JName);
                if (jDevice!=null)
                {
                    addrow(jDevice.SUID, jDevice.SUID, uid, jDevice.Type, jDevice.Number, jDevice.Name, grade);
                    initdx(jDevice.SUID, jDevice.Name, grade + 1);
                }
            }
        }
        /// <summary>
        ///开闭所
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pid"></param>
        private void initkbssb(string uid, string pid, int grade)
        {
            string sql = "where AreaID = '" + uid + "'  and projectid='" + Itop.Client.MIS.ProgUID + "' and Type in ('63','64','71') ORDER BY Number";
            IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            string spid = "";
            foreach (PSPDEV dev in list)
            {
                DataRow row = addrow(dev.SUID, dev.SUID, uid, dev.Type, dev.Number, dev.Name, grade);
                initdx(dev.SUID, dev.Name, grade + 1);
            }
        }
         /// <summary>
        ///开闭所
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pid"></param>
        private void initkbs(string uid, string pid, int grade)
        {     
            string sql = "where AreaID = '" + uid + "'  and projectid='" + Itop.Client.MIS.ProgUID + "' and Type in ('56','58','54') ORDER BY Number";
            IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            string spid = "";


            foreach (PSPDEV dev in list)
            {
                DataRow row = addrow(dev.SUID, dev.SUID, uid, dev.Type, dev.Number, dev.Name, grade);
                initkbssb(dev.SUID, dev.Name, grade+1);
                initdx(dev.SUID, dev.Name, grade+1);
                //if (row == null) continue;
            }           
        }
        /// <summary>
        ///支线
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pid"></param>
        private void initzx(string uid, string pid,int grade)
        {
            //if (topodevdic.ContainsKey(uid)) return;
            //topodevdic.Add(uid, null);
            string sql = "where AreaID = '" + uid + "'  and projectid='" + Itop.Client.MIS.ProgUID + "' and Type in ('70','72','71') ORDER BY Number";
            IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            string spid = "";
            //foreach (PSPDEV dev in list)
            //{
            //    DataRow row = addrow(dev.SUID, dev.SUID, uid, dev.Type, dev.Number, dev.Name);
            //    if (row == null) continue;
            //    initzx(dev.SUID, dev.Name);                
            //}
            PSPDEV devFor = new PSPDEV();
            if (list.Count>0)
            {
                devFor = list[0] as PSPDEV;
            }
            for (int i = 0; i < list.Count;i++ )
            {
                PSPDEV dev = list[i] as PSPDEV;
                switch(dev.Type)
                {
                    case "70":
                        if (i == 0)
                        {
                            DataRow row = addrow(dev.SUID, dev.SUID, uid, dev.Type, dev.Number, dev.Name,grade);
                            initzx(dev.SUID, dev.Name, grade+1);
                            initdx(dev.SUID, dev.Name, grade + 1);
                        }
                        else
                        {
                            DataRow row = addrow(dev.SUID, dev.SUID, devFor.SUID, dev.Type, dev.Number, dev.Name, grade);
                            initzx(dev.SUID, dev.Name, grade+1);
                            initdx(dev.SUID, dev.Name, grade + 1);
                        }
                        break;
                    case "72":
                        if (i == 0)
                        {
                            initbyq(uid, pid, dev, grade);
                        }
                        else
                        {
                            initbyq(devFor.SUID, devFor.Name, dev, grade);
                        }
                        break;
                    default:
                        if (i == 0)
                        {
                            DataRow row = addrow(dev.SUID, dev.SUID, uid, dev.Type, dev.Number, dev.Name,grade);
                            initdx(dev.SUID, dev.Name, grade + 1);
                        }
                        else
                        {
                            DataRow row = addrow(dev.SUID, dev.SUID, devFor.SUID, dev.Type, dev.Number, dev.Name, grade);
                            initdx(dev.SUID, dev.Name, grade + 1);
                            
                        }
                        break;
                }
                if (dev.Type=="72")
                {
                }
                else
                {
                    devFor = dev;
                }
                
                
            }
        }
        /// <summary>
        ///道闸及断路器
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pid"></param>
        private void initdrq(string uid, string pid)
        {
            //if (topodevdic.ContainsKey(uid)) return;
            //topodevdic.Add(uid, null);
            string sql = "where AreaID = '" + uid + "'  and projectid='" + Itop.Client.MIS.ProgUID + "' and Type='71' ORDER BY Number";
            IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            sql = "where AreaID = '" + uid + "'  and projectid='" + Itop.Client.MIS.ProgUID + "' and Type='02' ORDER BY Number";
            IList<PSPDEV> listBYQ2 = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            sql = "where AreaID = '" + uid + "'  and projectid='" + Itop.Client.MIS.ProgUID + "' and Type='03' ORDER BY Number";
            IList<PSPDEV> listBYQ3 = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            string spid = "";
            foreach (PSPDEV dev in list)
            {
                DataRow row = addrow(dev.SUID, dev.SUID, uid, dev.Type, dev.Number, dev.Name);
                if (row == null) continue;                
            }
            string parID="";
            if (list.Count>0)
            {
                parID = ((PSPDEV)list[list.Count-1]).SUID;
            } 
            else
            {
                parID = uid;
            }
             
            foreach (PSPDEV dev in listBYQ2)
            {
                DataRow row = addrow(dev.SUID, dev.SUID, parID, dev.Type, dev.Number, dev.Name);
                if (row == null) continue;
            }
            foreach (PSPDEV dev in listBYQ3)
            {
                DataRow row = addrow(dev.SUID, dev.SUID, parID, dev.Type, dev.Number, dev.Name);
                if (row == null) continue;
            }
        }
        /// <summary>
        /// 负荷
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pid"></param>
        private void initfh(string uid, string pid) {
            //if (topodevdic.ContainsKey(uid)) return;
            //topodevdic.Add(uid, null);
            string sql = "where iname='" + pid + "' and projectid='" + Itop.Client.MIS.ProgUID + "' and Type='15' ORDER BY Number";
            IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            string spid = "";
            foreach (PSPDEV dev in list) {
                addrow(dev.SUID, dev.SUID, uid, dev.Type, dev.Number, dev.Name);
            }
        }
        /// <summary>
        /// 变压器
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pid"></param>
        private void initbyq(string uid, string pid,PSPDEV devBYQ,int grade)
        {
            //if (topodevdic.ContainsKey(uid)) return;
            //topodevdic.Add(uid, null);
            string sql = "where AreaID = '" + devBYQ.SUID + "'  and projectid='" + Itop.Client.MIS.ProgUID + "' and Type in ('71') ORDER BY Number";
            IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            string spid = "";      
            for (int i = 0; i < list.Count; i++)
            {
                PSPDEV dev = list[i] as PSPDEV;
                if (i == 0)
                {
                    DataRow row = addrow(dev.SUID, dev.SUID, uid, dev.Type, dev.Number, dev.Name,grade);
                    if (row == null) continue;
                }
                else
                {
                    DataRow row = addrow(dev.SUID, dev.SUID, ((PSPDEV)list[i - 1]).SUID, dev.Type, dev.Number, dev.Name,grade);
                    if (row == null) continue;
                }
            }
            if (list.Count>0)
            {
                DataRow row = addrow(devBYQ.SUID, devBYQ.SUID, ((PSPDEV)list[list.Count - 1]).SUID, devBYQ.Type, devBYQ.Number, devBYQ.Name,grade);
               
            }
            else
            {
                DataRow row = addrow(devBYQ.SUID, devBYQ.SUID, uid, devBYQ.Type, devBYQ.Number, devBYQ.Name,grade);                
            }
        }
        /// <summary>
        /// ２圈变压器
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pid"></param>
        private void initbyq2(string uid, string pid) {
            //if (topodevdic.ContainsKey(uid)) return;
            //topodevdic.Add(uid, null);
            string sql = "where (iname='" + pid + "' or jname='" + pid + "') and projectid='" + Itop.Client.MIS.ProgUID + "' and Type='02' ORDER BY Number";
            IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            string spid = "";
            foreach (PSPDEV dev in list) {
                addrow(dev.SUID, dev.SUID, uid, dev.Type, dev.Number, dev.Name);
                if (dev.IName != pid) {
                    spid = dev.IName;
                } else {
                    spid = dev.JName;
                }
                if (!string.IsNullOrEmpty(spid)) {
                    initmx(dev.SUID, spid);
                }
            }
        }
        /// <summary>
        /// 3圈变压器
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pid"></param>
        private void initbyq3(string uid, string pid) {
            //if (topodevdic.ContainsKey(uid + pid)) return;
            //topodevdic.Add(uid + pid, null);
            string sql = "where (iname='" + pid + "' or jname='" + pid + "' or kname='" + pid + "') and projectid='" + Itop.Client.MIS.ProgUID + "' and Type='03' ORDER BY Number";
            IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            string spid = "";
            foreach (PSPDEV dev in list) {
                addrow(dev.SUID, dev.SUID, uid, dev.Type, dev.Number, dev.Name);
                if (dev.IName != pid) {
                    spid = dev.IName;
                    if (!string.IsNullOrEmpty(spid)) {
                        initmx(dev.SUID, spid);
                    }
                }
                if (dev.JName != pid) {
                    spid = dev.JName;
                    if (!string.IsNullOrEmpty(spid)) {
                        initmx(dev.SUID, spid);
                    }
                }
                if (dev.KName != pid) {
                    spid = dev.KName;
                    if (!string.IsNullOrEmpty(spid)) {
                        initmx(dev.SUID, spid);
                    }
                }

            }
        }
        private void initmx(string uid, string pid) {
            if (topodevdic.ContainsKey(uid + pid)) return;
            topodevdic.Add(uid + pid, null);
            string sql = "where name='" + pid + "' and projectid='" + Itop.Client.MIS.ProgUID + "' and Type='01' ORDER BY Number";
            IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            foreach (PSPDEV dev in list) {
                //if (sbTable.Select("id='"+dev.SUID+"'").Length==0) {// 如果深度并且此母线记录少于２,避免死循环
                DataRow row = addrow(dev.SUID, dev.SUID, uid, dev.Type, dev.Number, dev.Name);
                if (Showdeep && row != null) {
                    initxl(dev.SUID, dev.Name,0);
                    initfh(dev.SUID, dev.Name);
                    initbyq2(dev.SUID, dev.Name);
                    initbyq3(dev.SUID, dev.Name);
                }
                //}
            }
            //treeList1.DataSource = sbTable;
        }
        public DataTable GetSBtable(string bdzid) {
            string sql = "where svguid='" + bdzid + "' and projectid='" + Itop.Client.MIS.ProgUID + "' and Type='01' ORDER BY Number";
            if (string.IsNullOrEmpty(bdzid)) {
                sql = "where  projectid='" + Itop.Client.MIS.ProgUID + "' and Type='01' ORDER BY Number";
                showdeep = false;
            }
            IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            if (list.Count > 0) {

                msgbox.Show();
                int n = 0;
                try {
                    string strmx = "";
                    foreach (PSPDEV dev in list) {
                        n++;
                        strmx = string.Format("({0}){1}__", n, dev.Name);
                        addrow(dev.SUID, dev.SUID, "root", dev.Type, dev.Number, dev.Name, dev.RateVolt);
                        msgbox.SetCaption(strmx+"线路");
                        initxl(dev.SUID, dev.Name,0);
                        msgbox.SetCaption(strmx+"负荷");
                        initfh(dev.SUID, dev.Name);
                        msgbox.SetCaption(strmx+"变压器２");
                        initbyq2(dev.SUID, dev.Name);
                        msgbox.SetCaption(strmx+"变压器３");
                        initbyq3(dev.SUID, dev.Name);             
                        //if (string.IsNullOrEmpty(bdzid)) break;
                    }
                } catch { }
                msgbox.Hide();

            } else if (pdev != null) {
                if (pdev.Type == "01") {
                    msgbox.Show();
                    initxl(pdev.SUID, pdev.Name,0);
                    initfh(pdev.SUID, pdev.Name);
                    initbyq2(pdev.SUID, pdev.Name);
                    initbyq3(pdev.SUID, pdev.Name);
                    msgbox.Hide();
                } else if (pdev.Type == "05") {

                }

            }
            return sbTable;
        }
        /// <summary>
        /// 初始母线列表
        /// </summary>
        private void freshmx() {
            if (BDZUID != "") {
                string sql = "where svguid='" + bdzuid + "' and projectid='" + Itop.Client.MIS.ProgUID + "' and Type='01' ORDER BY Number";
                IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
                if (list.Count > 0) {

                    msgbox.Show();
                    try {
                        foreach (PSPDEV dev in list) {
                            addrow(dev.SUID, dev.SUID, "root", dev.Type, dev.Number, dev.Name);
                            msgbox.SetCaption("线路");
                            initxl(dev.SUID, dev.Name,0);
                            msgbox.SetCaption("负荷");
                            initfh(dev.SUID, dev.Name);
                            msgbox.SetCaption("变压器２");
                            initbyq2(dev.SUID, dev.Name);
                            msgbox.SetCaption("变压器３");
                            initbyq3(dev.SUID, dev.Name);
                        }
                    } catch { }
                    msgbox.Hide();
                }
            } else if (pdev != null) {
                if (pdev.Type == "01") {
                    msgbox.Show();
                    initxl(pdev.SUID, pdev.Name,0);
                    initfh(pdev.SUID, pdev.Name);
                    initbyq2(pdev.SUID, pdev.Name);
                    initbyq3(pdev.SUID, pdev.Name);
                    msgbox.Hide();
                } else if (pdev.Type == "05") {

                }
            }
            treeList1.DataSource = sbTable;
        }
        private DataRow addrow(params object[] prop) {
            DataRow row = null;
            try {
                //if (sbTable.Select("uid='" + prop[0] + "'").Length == 0) 
                row = sbTable.Rows.Add(prop);
            } catch { }
            return row;
        }
        //添加固定列
        private void addTreeColumns() {
            TreeListColumn column = new TreeListColumn();
            column.FieldName = "uid";
            column.Caption = "uid";
            column.VisibleIndex = -1;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "id";
            column.Caption = "id";
            column.VisibleIndex = -1;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "parentid";
            column.Caption = "parentid";
            column.VisibleIndex = -1;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "type";
            column.Caption = "type";
            column.VisibleIndex = -1;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "bh";
            column.Caption = "编号";
            column.VisibleIndex = 3;
            column.Width = 20;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "name";
            column.Caption = "名称";
            column.VisibleIndex = 2;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "gradation";
            column.Caption = "节点层次";
            column.VisibleIndex = 2;
            column.Width = 20;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
        }
        private void addTableColumns() {
            sbTable = new DataTable();

            //DataColumn column = new DataColumn("id", typeof(string));
            sbTable.Columns.Add("uid", typeof(string));
            DataColumn dc1 = sbTable.Columns.Add("id", typeof(string));
            DataColumn dc2 = sbTable.Columns.Add("parentid", typeof(string));
            sbTable.PrimaryKey = new DataColumn[] { dc1, dc2 };
            sbTable.Columns.Add("type", typeof(string));
            sbTable.Columns.Add("bh", typeof(int));
            sbTable.Columns.Add("name", typeof(string));
            sbTable.Columns.Add("gradation", typeof(int));
            sbTable.Columns.Add("dy", typeof(double));
        }
    }
}
