using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using Itop.Domain.Graphics;
using Itop.Client.Projects;
using System.Collections;

namespace Itop.TLPSP.DEVICE
{
    /// <summary>
    /// 所有设备面板的基类
    /// </summary>
    public partial class UCDeviceBase : UserControl
    {
        protected DataTable datatable1 = null;
        protected string con;
        protected string strCondition=" where 1=1 and ";
        public string ID;
        public string wjghuid = "";
        public bool updatenumberflag = true;
        public string projectdeviceid = "";
        public UCDeviceBase() {
            InitializeComponent();
            InitColumns();
                    
        }
        #region 初始设置
        /// <summary>
        /// 设备初始化
        /// </summary>
        public virtual void Init() {
            
            datatable1 = null;
            string con2 = " and ProjectID = '" + Itop.Client.MIS.ProgUID + "'";
            con = strCon + con + con2;
            IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            IList listNew = new List<PSPDEV>();
            foreach (PSPDEV dev in list)
            {
                if (dev.Type == "01")
                {
                     if (dev.NodeType == "0")
                    {
                        dev.NodeType = "平衡节点";
                    }
                    else if (dev.NodeType == "1")
                    {
                        dev.NodeType = "PQ节点";
                    }
                    else if (dev.NodeType == "2")
                    {
                        dev.NodeType = "PV节点";
                    }
                }
               
                if (dev.KSwitchStatus == "1")
                {
                    dev.KSwitchStatus = "退出运行";
                }
                else
                {
                    dev.KSwitchStatus = "投入运行";
                }
                if (dev.UnitFlag == "0")
                {
                    dev.UnitFlag = "p.u.";
                }
                else
                {
                    if (dev.Type == "01" || dev.Type == "04" || dev.Type == "12")
                    {
                        dev.UnitFlag = "kV/MW/MVar";
                    }
                    else
                    {
                        dev.UnitFlag = "Ohm/10-6Siem";
                    }
                }
                if (dev.Type=="01")
                {
                    object obj = DeviceHelper.GetDevice<PSP_PowerSubstation_Info>(dev.SvgUID);
                    if (obj != null)
                    {
                        dev.SubstationName = ((PSP_PowerSubstation_Info)obj).Title;
                    }
                     obj = DeviceHelper.GetDevice<PSP_Substation_Info>(dev.SvgUID);
                    if (obj!=null)
                    {
                        dev.SubstationName = ((PSP_Substation_Info)obj).Title;
                        
                    }
                   
                }
                else if (dev.Type=="12")
                {
                    if (dev.NodeType=="0")
                    {
                        dev.NodeType = "恒阻抗模型";
                    } 
                    else
                    {
                        dev.NodeType = "综合负荷";
                    }
                }
                 
                    
               
                listNew.Add(dev);
            }
            datatable1 = Itop.Common.DataConverter.ToDataTable(listNew, typeof(PSPDEV));
            gridControl1.DataSource = datatable1;
            gridControl1.UseEmbeddedNavigator = true;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            //创建一条开关数据 为了短路计算所用 如果没有则创建
           string sql = " where Type='07'and ProjectID='" + this.ProjectID + "' order by name";
            list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", sql);
            if (list.Count==0)
            {
                PSPDEV dev = new PSPDEV();
                dev.Type = "07";
                dev.Name = "1";
                dev.Number = 1;
                dev.KSwitchStatus = "0";
                dev.ProjectID = this.ProjectID;
                UCDeviceBase.DataService.Create("InsertPSPDEV", dev);
            }
        }
        /// <summary>
        /// 设备初始化
        /// </summary>
        public virtual void Init(IList list)
        {   
            IList listNew = new List<PSPDEV>();   
            foreach (PSPDEV dev in list)
            {
                if (dev.KSwitchStatus == "1")
                {
                    dev.KSwitchStatus = "退出运行";
                }
                else
                {
                    dev.KSwitchStatus = "投入运行";
                }
                if (dev.UnitFlag == "0")
                {
                    dev.UnitFlag = "p.u.";
                }
                else
                {
                    if (dev.Type == "01" || dev.Type == "04" || dev.Type == "12")
                    {
                        dev.UnitFlag = "kV/MW/MVar";
                    }
                    else
                    {
                        dev.UnitFlag = "Ohm/10-6Siem";
                    }
                }

                if (dev.Type == "12")
                {
                    if (dev.NodeType == "0")
                    {
                        dev.NodeType = "恒阻抗模型";
                    }
                    else
                    {
                        dev.NodeType = "综合负荷";
                    }
                }
                listNew.Add(dev);
            }      

            
         
            datatable1 = Itop.Common.DataConverter.ToDataTable(listNew, typeof(PSPDEV));
            gridControl1.DataSource = datatable1;
            gridControl1.UseEmbeddedNavigator = true;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
        }
        /// <summary>
        /// 专门用于潮流数据显示
        /// </summary>
        public virtual void PspInit()
        {

        }
        public virtual void PspInit(IList<object> list)
        {

        }
        public virtual void proInit(string year)
        {}
        IList<PSPDEV> valuelist = null;
  
        private IList<double> DY(string yearandtype)
        {
            string constr = "   ProjectID='" + ProjectID + "' and "+yearandtype+"  order by RateVolt desc";
            return UCDeviceBase.DataService.GetList<double>("SelectPSPDEV_RateVolt_distinct", constr);

        }
        //public IList<string> GetAreaID(double tempdb)
        //{

        //    string constr = "  ProjectID='" + ProjectID + "' and  year(cast(OperationYear as datetime))<" + nowyear + "  and Type='05'  and RateVolt=" + tempdb + " order by AreaID ";
        //    return UCDeviceBase.DataService.GetList<string>("SelectPSPDEV_GroupAreaID_DIs", constr);
        //}
        string[] que = new string[60] { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", 
            "十一","十二","十三","十四","十五","十六","十七","十八","十九","二十","二十一","二十二","二十三","二十四","二十五","二十六","二十七",
            "二十八","二十九","三十","三十一","三十二","三十三","三十四","三十五","三十六","三十七","三十八","三十九","四十","四十一","四十二","四十三","四十四",
            "四十五","四十六","四十七","四十八","四十九","五十","五十一","五十二","五十三","五十四","五十五","五十六","五十七","五十八","五十九","六十"};
        public virtual void Statictable(string yearandtype)
        {
            //string constrvalue = " where  ProjectID='@@@@@#@@'";
            //valuelist = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", constrvalue);
            gridView1.CustomDrawCell+=new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(gridView1_CustomDrawCell);
            int m = 1;
            IList<double> templist = DY(yearandtype);
            valuelist = new List<PSPDEV>();
            if (templist.Count != 0)
            {
                for (int i = 0; i < templist.Count; i++)
                {
                    PSPDEV tempdev = new PSPDEV();
                    // tempdev.ProjectID = que[i];
                    tempdev.Name = templist[i] + "设备";
                    tempdev.RateVolt = templist[i];
                    tempdev.Type = "T";
                    m = 1;


                    valuelist.Add(tempdev);
                    string constr = " where  ProjectID='" + ProjectID + "' and  " + yearandtype + " and RateVolt=" + templist[i] + "   order by Name";
                    IList<PSPDEV> linelist = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", constr);
                    for (int k = 0; k < linelist.Count; k++)
                    {
                        valuelist.Add(linelist[k]);
                    }
                }
            }
            else
            {
                PSPDEV tempdev = new PSPDEV();
                // tempdev.ProjectID = que[i];
                tempdev.Name = "设备";
                
                tempdev.Type = "T";
                m = 1;
                string constr = " where  ProjectID='" + ProjectID + "' and  " + yearandtype + " order by Name";
                IList<PSPDEV> linelist = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", constr);
                for (int k = 0; k < linelist.Count; k++)
                {
                    valuelist.Add(linelist[k]);
                }
            }
            IList listNew = new List<PSPDEV>();
            foreach (PSPDEV dev in valuelist)
            {
                if (dev.Type == "01")
                {
                    if (dev.NodeType == "0")
                    {
                        dev.NodeType = "平衡节点";
                    }
                    else if (dev.NodeType == "1")
                    {
                        dev.NodeType = "PQ节点";
                    }
                    else if (dev.NodeType == "2")
                    {
                        dev.NodeType = "PV节点";
                    }
                }

                if (dev.KSwitchStatus == "1")
                {
                    dev.KSwitchStatus = "退出运行";
                }
                else
                {
                    dev.KSwitchStatus = "投入运行";
                }
                if (dev.UnitFlag == "0")
                {
                    dev.UnitFlag = "p.u.";
                }
                else
                {
                    if (dev.Type == "01" || dev.Type == "04" || dev.Type == "12")
                    {
                        dev.UnitFlag = "kV/MW/MVar";
                    }
                    else
                    {
                        dev.UnitFlag = "Ohm/10-6Siem";
                    }
                }
                if (dev.Type == "01")
                {
                    object obj = DeviceHelper.GetDevice<PSP_PowerSubstation_Info>(dev.SvgUID);
                    if (obj != null)
                    {
                        dev.SubstationName = ((PSP_PowerSubstation_Info)obj).Title;
                    }
                    obj = DeviceHelper.GetDevice<PSP_Substation_Info>(dev.SvgUID);
                    if (obj != null)
                    {
                        dev.SubstationName = ((PSP_Substation_Info)obj).Title;

                    }

                }
                else if (dev.Type == "12")
                {
                    if (dev.NodeType == "0")
                    {
                        dev.NodeType = "恒阻抗模型";
                    }
                    else
                    {
                        dev.NodeType = "综合负荷";
                    }
                }



                listNew.Add(dev);
            }
            datatable1 = Itop.Common.DataConverter.ToDataTable(listNew, typeof(PSPDEV));
            gridControl1.DataSource = datatable1;
           
          

        }
      
        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if (gridView1.GetRowCellValue(e.RowHandle,"Type")=="T")
            //{
            //    e.r

            //}

            object dr = this.gridView1.GetRow(e.RowHandle);
            Brush brush = null;
            Rectangle r = e.Bounds;
            Color c1 = Color.FromArgb(175, 238, 238);
            Color c2 = Color.FromArgb(175, 238, 238);
            Color c3 = Color.FromArgb(245, 222, 180);
            Color c4 = Color.FromArgb(245, 222, 180);

            if (dr == null)
                return;
            int tempsum = 0;
            if (gridView1.GetRowCellValue(e.RowHandle, "Type") != null)
            {
                if (gridView1.GetRowCellValue(e.RowHandle, "Type") == "T")
                {
                    brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, c1, c2, 180);
                    e.Graphics.FillRectangle(brush, r);
                }
                else if (gridView1.GetRowCellValue(e.RowHandle, "Type") == "A" && e.Column.Name == "NUM")
                {
                    brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, c3, c4, 180);
                    e.Graphics.FillRectangle(brush, r);
                }
            }

        }
     
        /// <summary>
        /// 专门用于网架优化显示
        /// </summary>
        public virtual void WjghInit()
        {
            datatable1 = null;
            con = "WHERE projectid='"+Itop.Client.MIS.ProgUID+"' AND SUID IN(select DeviceSUID from PSP_GprogElevice where type='线路' and L2='0'and GprogUID='"+wjghuid+"')";
            IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSPDEV));
            gridControl1.DataSource = datatable1; 
        }
        /// <summary>
        /// 根据查询条件，重新选择设备
        /// </summary>
        public virtual void SelDevices()
        {
            datatable1 = null;
            string con2 = " and ProjectID = '" + Itop.Client.MIS.ProgUID + "'";
            con = strCon + con+con2;
            IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSPDEV));
            gridControl1.DataSource = datatable1;
        }
        /// <summary>
        ///短路图数据查询入口
        /// </summary>
        public virtual void SelshortDevices()
        {
            datatable1 = null;
            string con2 = " and ProjectID = '" + Itop.Client.MIS.ProgUID + "'";
            con = strCon + con + con2;
            IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            List<PSPDEV> delsum = new List<PSPDEV>();
            for (int i = 0; i < shortselelement.Count; i++)
            {
                if (shortselelement[i].selectflag)
                {
                    foreach (PSPDEV dev in list)
                    {
                        if (dev.SUID==shortselelement[i].suid)
                        {
                            delsum.Add(dev);
                        }
                    }
                   
                  
                }
            }
            for (int m = 0; m < delsum.Count; m++)
            {
                list.Remove(delsum[m]);
            }
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSPDEV));
            gridControl1.DataSource = datatable1;
        }
        /// <summary>
        /// 设置设备显示列
        /// </summary>
        public virtual void InitColumns()
        {
            
        }

        public virtual string GetClassName()
        {
            return "PSPDEV";
        }
        public List<eleclass> shortselelement = null;
        public List<eleclass> wjghselelement = null; 
        public virtual string GetType()
        {
            return "00";
        }

        public virtual void UpdateIn(DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i][0].ToString().IndexOf("合计") > 0 || table.Rows[i][1].ToString().IndexOf("合计") > 0)
                    continue;
                PSPDEV area = new PSPDEV();
                area.ProjectID = projectid;
                foreach (DataColumn col in table.Columns)
                {
                    try
                    {
                        if (table.Rows[i][col] != null)
                        {
                            string inserted = table.Rows[i][col].ToString();
                            Type type = area.GetType().GetProperty(col.ColumnName).PropertyType;//.GetValue(area, null).GetType();
                            if (type == typeof(int))
                                area.GetType().GetProperty(col.ColumnName).SetValue(area, int.Parse(inserted == "" ? "0" : inserted), null);
                            else if (type == typeof(string))
                            {
                                if (inserted=="投入运行")
                                {
                                    inserted = "0";
                                }
                                if (inserted == "退出运行")
                                {
                                    inserted = "1";
                                }
                                if (inserted=="平衡节点")
                                {
                                    inserted = "0";
                                }
                                if (inserted == "PQ节点")
                                {
                                    inserted = "1";
                                }
                                if (inserted == "PV节点")
                                {
                                    inserted = "2";
                                }
                                if (inserted == "kV/MW/MVar"||inserted=="Ohm/10-6Siem")
                                {
                                    inserted = "1";
                                }
                                if (inserted == "p.u.")
                                {
                                    inserted = "0";
                                }
                                if (inserted=="投入")
                                {
                                    inserted = "0";
                                }
                                if (inserted=="退出")
                                {
                                    inserted = "1";
                                }
                                area.GetType().GetProperty(col.ColumnName).SetValue(area, inserted, null);
                            }
                                
                            else if (type == typeof(decimal))
                                area.GetType().GetProperty(col.ColumnName).SetValue(area, decimal.Parse(inserted == "" ? "0" : inserted), null);
                            else if (type == typeof(double))
                                area.GetType().GetProperty(col.ColumnName).SetValue(area, double.Parse(inserted == "" ? "0.0" : inserted), null);
                        }
                    }
                    catch { MessageBox.Show(string.Format("第{0}行{1}列插入有问题", i.ToString(), col.Caption)); }
                }
                if (!string.IsNullOrEmpty(ParentID))
                {
                    area.SvgUID = ParentID;
                }
                area.Type = GetType();
                UCDeviceBase.DataService.Create<PSPDEV>(area);
                
            }
        }

        public virtual void UpdateInchildren(DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i][0].ToString().IndexOf("合计") > 0 || table.Rows[i][1].ToString().IndexOf("合计") > 0)
                    continue;
                PSPDEV area = new PSPDEV();
                area.ProjectID = Itop.Client.MIS.ProgUID;
                foreach (DataColumn col in table.Columns)
                {
                    try
                    {
                        if (table.Rows[i][col] != null)
                        {
                            string inserted = table.Rows[i][col].ToString();
                            Type type = area.GetType().GetProperty(col.ColumnName).PropertyType;//.GetValue(area, null).GetType();
                            if (type == typeof(int))
                                area.GetType().GetProperty(col.ColumnName).SetValue(area, int.Parse(inserted == "" ? "0" : inserted), null);
                            else if (type == typeof(string))
                            {
                                if (inserted == "投入运行")
                                {
                                    inserted = "0";
                                }
                                if (inserted == "退出运行")
                                {
                                    inserted = "1";
                                }
                                if (inserted == "平衡节点")
                                {
                                    inserted = "0";
                                }
                                if (inserted == "PQ节点")
                                {
                                    inserted = "1";
                                }
                                if (inserted == "PV节点")
                                {
                                    inserted = "2";
                                }
                                if (inserted == "kV/MW/MVar" || inserted == "Ohm/10-6Siem")
                                {
                                    inserted = "1";
                                }
                                if (inserted == "p.u.")
                                {
                                    inserted = "0";
                                }
                                if (inserted == "投入")
                                {
                                    inserted = "0";
                                }
                                if (inserted == "退出")
                                {
                                    inserted = "1";
                                }
                                area.GetType().GetProperty(col.ColumnName).SetValue(area, inserted, null);
                            }

                            else if (type == typeof(decimal))
                                area.GetType().GetProperty(col.ColumnName).SetValue(area, decimal.Parse(inserted == "" ? "0" : inserted), null);
                            else if (type == typeof(double))
                                area.GetType().GetProperty(col.ColumnName).SetValue(area, double.Parse(inserted == "" ? "0.0" : inserted), null);
                        }
                    }
                    catch { MessageBox.Show(string.Format("第{0}行{1}列插入有问题", i.ToString(), col.Caption)); }
                }
                //if (!string.IsNullOrEmpty(ParentID))
                //{
                //    area.SvgUID = ParentID;
                //}
                area.Type = GetType();
                UCDeviceBase.DataService.Create<PSPDEV>(area);

            }
        }
        #endregion 
        #region 记录操作
        public virtual object SelectedDevice {
            get { return null; }
        }
        public virtual void Add() {
        }
        public virtual void Delete() {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                PSPDEV dev = Itop.Common.DataConverter.RowToObject<PSPDEV>(row);
                if (Itop.Common.MsgBox.ShowYesNo("是否确认删除［" + dev.Name + "］?") == DialogResult.Yes) {
                    UCDeviceBase.DataService.Delete<PSPDEV>(dev);
                    ((DataTable)gridControl1.DataSource).Rows.Remove(row);
                }
            }
        }
        public virtual void Alldel()
        {
            if (Itop.Common.MsgBox.ShowYesNo("是否确认删除所有数据") == DialogResult.Yes)
            {
                DataTable dat = gridView1.GridControl.DataSource as DataTable;
                foreach (DataRow dr in dat.Rows)
                {
                    if (dr!=null)
                    {
                         PSPDEV dev = Itop.Common.DataConverter.RowToObject<PSPDEV>(dr);                       
                        UCDeviceBase.DataService.Delete<PSPDEV>(dev);
                        string str = "where DeviceSUID = '" + dev.SUID + "'";
                        IList list = UCDeviceBase.DataService.GetList("SelectPSP_ElcDeviceByCondition", str);
                        foreach (PSP_ElcDevice pe in list)
                        {
                            UCDeviceBase.DataService.Delete<PSP_ElcDevice>(pe);
                        }
                        
                    }
                }
                dat.Clear();
                gridView1.GridControl.DataSource = dat;
            }
          
            
        }
        public virtual void UpdateNumber()
        {

        }
        public virtual void Edit() {
        }
        public virtual void Save() {
        }
        public virtual void Print() {
        }
        #endregion
        #region 属性
        public GridControl GridControl {
            get { return gridControl1; }
        }
        string projectid;
        public  string ProjectID {
            get { return projectid; }
            set { projectid = value; }
        }
        private string parentid;
        public string ParentID
        {
            get { return parentid; }
            set { parentid = value; }
        }
        private object parentobj;
        public object ParentObj
        {
            get { return parentobj; }
            set { parentobj = value; }
        }
        public string strCon
        {
            get {
                return strCondition;
            }
            set {
                strCondition = value;
            }
        }
        static Itop.Server.Interface.IBaseService dataservice;
        /// <summary>
        /// 数据库交互接口
        /// </summary>
        public static Itop.Server.Interface.IBaseService DataService {
            get {
                if (dataservice == null) {
                    dataservice = Itop.Common.RemotingHelper.GetRemotingService<Itop.Server.Interface.IBaseService>();
                }
                return dataservice;
            }
            set { dataservice = value; }
        }
        #endregion
    }
    
}
