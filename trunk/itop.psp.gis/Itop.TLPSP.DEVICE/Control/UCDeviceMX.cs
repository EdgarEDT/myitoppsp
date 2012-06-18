using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using System.Collections;
using Itop.Domain.Graphics;
using Itop.Client.Common;

namespace Itop.TLPSP.DEVICE
{
    /// <summary>
    /// 母线
    /// </summary>
    public partial class UCDeviceMX : Itop.TLPSP.DEVICE.UCDeviceBase
    {
        public UCDeviceMX() {
            InitializeComponent();            
        }        
        #region 初始设置
        /// <summary>
        /// 设备初始化
        /// </summary>
        public override void Init() {
            con = " Type='01'";
            base.Init();        
        }
        /// <summary>
        /// 检索设备
        /// </summary>
        public override void SelDevices()
        {
            con = " and Type='01'";
            base.SelDevices();
        }
        /// <summary>
        ///短路图数据查询入口
        /// </summary>
        public override void SelshortDevices()
        {
            con = " and Type='01'";
            base.SelshortDevices();
        }
        /// <summary>
        /// 设置设备显示列
        /// </summary>
        public override void InitColumns() {            
            GridColumn column = gridView1.Columns.Add();
            column.Caption = "母线名称";
            column.FieldName = "Name";
            column.Width = 100;
            column.VisibleIndex = 1;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "母线编号";
            column.FieldName = "Number";
            column.Width = 100;
            column.VisibleIndex = 2;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "节点类型";
            column.FieldName = "NodeType";
            column.Width = 100;
            column.VisibleIndex = 3;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "电压";
            column.FieldName = "VoltR";
            column.Width = 100;
            column.VisibleIndex = 4;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "相角";
            column.FieldName = "VoltV";
            column.Width = 100;
            column.VisibleIndex = 5;
            column.OptionsColumn.AllowEdit = false;
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
            column.Caption = "短路容量";
            column.FieldName = "Burthen";
            column.Width = 100;
            column.VisibleIndex = 8;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "发电有功";
            column.FieldName = "OutP";
            column.Width = 100;
            column.VisibleIndex = 9;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "发电无功";
            column.FieldName = "OutQ";
            column.Width = 100;
            column.VisibleIndex = 10;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "负荷有功";
            column.FieldName = "InPutP";
            column.Width = 100;
            column.VisibleIndex = 11;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "负荷无功";
            column.FieldName = "InPutQ";
            column.Width = 100;
            column.VisibleIndex = 12;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "电压上限";
            column.FieldName = "jV";
            column.Width = 100;
            column.VisibleIndex = 13;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "电压下限";
            column.FieldName = "iV";
            column.Width = 100;
            column.VisibleIndex = 14;
            column.OptionsColumn.AllowEdit = false;           
            column = gridView1.Columns.Add();
            column.Caption = "运行状态";
            column.FieldName = "KSwitchStatus";
            column.Width = 100;
            column.VisibleIndex = 15;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "单位";
            column.FieldName = "UnitFlag";
            column.Width = 100;
            column.VisibleIndex = 16;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "所属电站、电厂";
            column.FieldName = "SubstationName";
            column.Width = 100;
            column.VisibleIndex = 17;
            column.OptionsColumn.AllowEdit = false;

            //隐藏内容
            column = gridView1.Columns.Add();
            column.Caption = "是否发电机节点";
            column.FieldName = "LineLevel";
            column.Width = 100;
            column.VisibleIndex = 18;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "是否PV节点";
            column.FieldName = "LineType";
            column.Width = 100;
            column.VisibleIndex = 19;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "发电机有功上限";
            column.FieldName = "Vimax";
            column.Width = 100;
            column.VisibleIndex = 20;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "发电机有功下限";
            column.FieldName = "Vimin";
            column.Width = 100;
            column.VisibleIndex = 21;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "PV节点无功上限";
            column.FieldName = "Vjmax";
            column.Width = 100;
            column.VisibleIndex = 22;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "PV节点无功下限";
            column.FieldName = "Vjmin";
            column.Width = 100;
            column.VisibleIndex = 23;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "PV节点无功初值";
            column.FieldName = "Vk0";
            column.Width = 100;
            column.VisibleIndex = 24;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "投产时间";
            column.FieldName = "OperationYear";
            column.Width = 100;
            column.VisibleIndex = 25;
            column.OptionsColumn.AllowEdit = false;
        }
        #endregion
        public override string GetClassName()
        {
            return "PSPDEV";
        }
        public override string GetType()
        {
            return "01";
        }
        #region 记录操作
        /// <summary>
        ///母线进行重新排号
        /// </summary>
        public override void UpdateNumber()
        {
            try
            {
                if (updatenumberflag)
                {
                    con = "where projectid='" + Itop.Client.MIS.ProgUID + "' and Type='01' ORDER BY Number";

                    IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    int i = 0;
                    foreach (PSPDEV dev in list)
                    {
                        i++;
                        dev.Number = i;
                        UCDeviceBase.DataService.Update<PSPDEV>(dev);
                    }
                    Init();
                    MessageBox.Show("更新成功！");
                }
                else
                {
                    con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + projectdeviceid + "' and pspdev.Type='01' ORDER BY Number";
                    IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    int i = 0;
                    foreach (PSPDEV dev in list)
                    {
                        i++;
                        dev.Number = i;
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
            frmMXdlg dlg = new frmMXdlg();
            dlg.Name = "";
            PSPDEV p = new PSPDEV();
            p.ProjectID = this.ProjectID;
            p.SvgUID = ParentID;
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
            frmMXdlg dlg = new frmMXdlg(); 
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
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //更新记录
                    dev = dlg.DeviceMx;
                    dev.ProjectID = this.ProjectID;                  
                    UCDeviceBase.DataService.Update<PSPDEV>(dev);

                    string sql0 = " update PSPDEV set IName='"+dev.Name+"' where IName='"+dlg.oldName+"'";
                    Services.BaseService.Update("UpdateSQL",sql0);
                    string sql1 = " update PSPDEV set JName='" + dev.Name + "' where JName='" + dlg.oldName + "'";
                    Services.BaseService.Update("UpdateSQL",sql1);
                    string sql2 = " update PSPDEV set KName='" + dev.Name + "' where KName='" + dlg.oldName + "'";
                    Services.BaseService.Update("UpdateSQL", sql2);

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

