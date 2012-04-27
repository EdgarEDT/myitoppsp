using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using System.Collections;
using Itop.Client.Projects;
using Itop.Domain.Graphics;
using Itop.Domain.Table;

namespace Itop.TLPSP.DEVICE
{
    /// <summary>
    /// 母线
    /// </summary>
    public partial class UCDeviceDX : Itop.TLPSP.DEVICE.UCDeviceBase
    {
        
        public UCDeviceDX() {
            InitializeComponent();            
        }        
        #region 初始设置
        /// <summary>
        /// 设备初始化
        /// </summary>
        public override void Init() {
            con = " Type='73'";
            base.Init();          
        }
        /// <summary>
        /// 专门用于潮流中的数据显示
        /// </summary>
        public override void PspInit()
        {
            datatable1 = null;
            con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + this.ProjectID + "' and Type='73'";

            IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSPDEV));
            gridControl1.DataSource = datatable1;          
        }
        /// <summary>
        /// 检索设备
        /// </summary>
        public override void SelDevices()
        {
            con = "and Type='73'";
            base.SelDevices();
        }
        /// <summary>
        ///短路图数据查询入口
        /// </summary>
        public override void SelshortDevices()
        {
            con = " and Type='73'";
            base.SelshortDevices();
        }
        /// <summary>
        ///线路首末节点号进行更新
        /// </summary>
        public override void UpdateNumber()
        {
            try
            {
                if (updatenumberflag)
                {
                    con = "where projectid='" + Itop.Client.MIS.ProgUID + "' and Type='73'";

                    IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    foreach (PSPDEV dev in list)
                    {
                        string iname = dev.IName;
                        if (iname != null)
                        {
                            con = "where projectid='" + Itop.Client.MIS.ProgUID + "'and type='01'and name='" + iname + "'";
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
                            con = "where projectid='" + Itop.Client.MIS.ProgUID + "'and type='01'and name='" + jname + "'";
                            PSPDEV psp = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                            if (psp != null)
                            {
                                dev.LastNode = psp.Number;
                            }
                        }
                        UCDeviceBase.DataService.Update<PSPDEV>(dev);
                    }
                    Init();
                    MessageBox.Show("更新成功！");
                }
                else
                {
                    con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + projectdeviceid + "' and pspdev.Type='05'";

                    IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    foreach (PSPDEV dev in list)
                    {
                        string iname = dev.IName;
                        if (iname != null)
                        {
                            con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + projectdeviceid + "'and pspdev.type='01'and pspdev.name='" + iname + "'";
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
                            con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + projectdeviceid + "'and pspdev.type='01'and pspdev.name='" + jname + "'";
                            PSPDEV psp = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                            if (psp != null)
                            {
                                dev.LastNode = psp.Number;
                            }
                        }
                        UCDeviceBase.DataService.Update<PSPDEV>(dev);
                    }
                    Init();
                    MessageBox.Show("更新成功！");
                }
                
            }
            catch (System.Exception ex)
            {
            	
            }
           
        }
        /// <summary>
        /// 设置设备显示列
        /// </summary>
        public override void InitColumns() {
            string s=" ProjectID='"+Itop.Client.MIS.ProgUID+"'"; 
            IList list=Services.BaseService.GetList("SelectPS_Table_AreaWHByConn",s);
            DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1=new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            //repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // repositoryItemLookUpEdit1
            // 
            repositoryItemLookUpEdit1.AutoHeight = false;
            repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            repositoryItemLookUpEdit1.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Title")});
            repositoryItemLookUpEdit1.DisplayMember = "Title";
            repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            repositoryItemLookUpEdit1.NullText = "";
            repositoryItemLookUpEdit1.ValueMember = "ID";
            repositoryItemLookUpEdit1.DataSource = list;

            GridColumn column = gridView1.Columns.Add();
            column.Caption = "线路名称";
            column.FieldName = "Name";
            column.Width = 100;
            column.VisibleIndex = 1;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "线路编号";
            column.FieldName = "Number";
            column.Width = 100;
            column.VisibleIndex = 2;
            column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "i侧母线名";
            //column.FieldName = "IName";
            //column.Width = 100;
            //column.VisibleIndex = 3;
            //column = gridView1.Columns.Add();
            //column.Caption = "j侧母线名";
            //column.FieldName = "JName";
            //column.Width = 100;
            //column.VisibleIndex = 3;
            //column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "基准电压";
            column.FieldName = "ReferenceVolt";
            column.Width = 100;
            column.VisibleIndex = 6;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "额定电压";
            column.FieldName = "RateVolt";
            column.Width = 100;
            column.VisibleIndex = 7;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "线路长度";
            column.FieldName = "LineLength";
            column.Width = 100;
            column.VisibleIndex = 8;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "导线型号";
            column.FieldName = "LineType";
            column.Width = 100;
            column.VisibleIndex = 8;
            column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "电阻";
            //column.FieldName = "LineR";
            //column.Width = 100;
            //column.VisibleIndex =9;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "电抗";
            //column.FieldName = "LineTQ";
            //column.Width = 100;
            //column.VisibleIndex = 10;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "B/2";
            //column.FieldName = "LineGNDC";
            //column.Width = 100;
            //column.VisibleIndex = 11;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "零序电阻";
            //column.FieldName = "ZeroR";
            //column.Width = 100;
            //column.VisibleIndex = 12;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "零序电抗";
            //column.FieldName = "ZeroTQ";
            //column.Width = 100;
            //column.VisibleIndex = 14;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "B0/2";
            //column.FieldName = "ZeroGNDC";
            //column.Width = 100;
            //column.VisibleIndex = 15;
            //column.OptionsColumn.AllowEdit = false;         
            column = gridView1.Columns.Add();
            column.Caption = "额定容量";
            column.FieldName = "Burthen";
            column.Width = 100;
            column.VisibleIndex = 16;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "运行状态";
            column.FieldName = "KSwitchStatus";
            column.Width = 100;
            column.VisibleIndex = 17;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "单位";
            column.FieldName = "UnitFlag";
            column.Width = 100;
            column.VisibleIndex = 18;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "投产年份";
            column.FieldName = "OperationYear";
            column.Width = 100;
            column.VisibleIndex = 19;
            column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "一次侧连接开关";
            //column.FieldName = "ISwitch";
            //column.Width = 100;
            //column.VisibleIndex = 20;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "二次侧连接开关";
            //column.FieldName = "JSwitch";
            //column.Width = 100;
            //column.VisibleIndex = 21;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "电网类型";
            //column.FieldName = "DQ";
            //column.Width = 100;
            //column.VisibleIndex = 22;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "所在区域";
            //column.FieldName = "AreaID";
            //column.ColumnEdit = repositoryItemLookUpEdit1;
            //column.Width = 100;
            //column.VisibleIndex = 23;
            //column.OptionsColumn.AllowEdit = false;


          
        }
        #endregion
        public override string GetClassName()
        {
            return "PSPDEV";
        }
        public override string GetType()
        {
            return "73";
        }
        #region 记录操作
        public override object SelectedDevice
        {
            get
            {
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);

                if (row != null)
                {
                    return Itop.Common.DataConverter.RowToObject<PSPDEV>(row);
                }
                return base.SelectedDevice;
            }
        }
        public override void Add() {
            frmDXdlg dlg = new frmDXdlg();
            dlg.ProjectSUID = this.ProjectID;
            dlg.newflag = true;
            dlg.Name = "";
            PSPDEV p = new PSPDEV();
            p.ProjectID = this.ProjectID;
            p.JName = ParentID;
            dlg.DeviceMx = p;
            if (dlg.ShowDialog() == DialogResult.OK) {
                //增加记录 
                PSPDEV dev = dlg.DeviceMx;
                dev.ProjectID = this.ProjectID;
                UCDeviceBase.DataService.Create("InsertPSPDEV", dev);
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
                else
                {
                    dev.NodeType = null;
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
                DataRow row=datatable1.NewRow();
                Itop.Common.DataConverter.ObjectToRow(dev, row);
                datatable1.Rows.Add(row); 
            }
        }
        public override void Delete() {
            //删除记录     
            base.Delete();
        }
        public override void Edit() {
            frmDXdlg dlg = new frmDXdlg(); 
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                PSPDEV dev = Itop.Common.DataConverter.RowToObject<PSPDEV>(row);
                if (dev.NodeType == "平衡节点")
                {
                    dev.NodeType = "0";
                }
                else if (dev.NodeType == "PQ节点")
                {
                    dev.NodeType = "1";
                }
                else if (dev.NodeType == "PV节点")
                {
                    dev.NodeType = "2";
                }
                if (dev.KSwitchStatus == "退出运行")
                {
                    dev.KSwitchStatus = "1";
                }
                else
                {
                    dev.KSwitchStatus = "0";
                }
                if (dev.UnitFlag == "p.u.")
                {
                    dev.UnitFlag = "0";
                }
                else
                {
                    dev.UnitFlag = "1";

                }
                dlg.DeviceMx = dev;
                dlg.ProjectSUID = dev.ProjectID;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //更新记录
                    dev = dlg.DeviceMx;
                    dev.ProjectID = this.ProjectID;
                    UCDeviceBase.DataService.Update<PSPDEV>(dev);
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
                    else
                    {
                        dev.NodeType = null;
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
                    Itop.Common.DataConverter.ObjectToRow(dev, row);
                }
            }
        }
        
        #endregion
    }
}

