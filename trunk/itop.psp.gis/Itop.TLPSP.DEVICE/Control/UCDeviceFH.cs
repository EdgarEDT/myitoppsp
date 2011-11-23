using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using Itop.Domain.Graphics;
using Itop.Client.Projects;
using System.Collections;


namespace Itop.TLPSP.DEVICE
{
    public partial class UCDeviceFH : Itop.TLPSP.DEVICE.UCDeviceBase
    {
        public UCDeviceFH()
        {
            InitializeComponent();
        }
          public override void Init()
        {
            con = " Type='12'";
            base.Init();
        }
        /// <summary>
        /// 检索设备
        /// </summary>
        public override void SelDevices()
        {
            con = "and Type='12'";
            base.SelDevices();
        }
        /// <summary>
        ///短路图数据查询入口
        /// </summary>
        public override void SelshortDevices()
        {
            con = " and Type='12'";
            base.SelshortDevices();
        }
        /// <summary>
        /// 设置设备显示列
        /// </summary>
        public override void InitColumns()
        {
            GridColumn column = gridView1.Columns.Add();
            column.Caption = "负荷名称";
            column.FieldName = "Name";
            column.Width = 100;
            column.VisibleIndex = 1;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "负荷编号";
            column.FieldName = "Number";
            column.Width = 100;
            column.VisibleIndex = 2;
            column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "节点类型";
            //column.FieldName = "NodeType";
            //column.Width = 100;
            //column.VisibleIndex = 3;
            //column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "负荷有功";
            column.FieldName = "InPutP";
            column.Width = 100;
            column.VisibleIndex = 4;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "负荷无功";
            column.FieldName = "InPutQ";
            column.Width = 100;
            column.VisibleIndex = 5;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "电压幅值";
            column.FieldName = "VoltR";
            column.Width = 100;
            column.VisibleIndex = 6;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "电压相角";
            column.FieldName = "VoltV";
            column.Width = 100;
            column.VisibleIndex = 7;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "恒阻抗百分比";
            column.FieldName = "Vipos";
            column.Width = 100;
            column.VisibleIndex = 8;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "负荷模型";
            column.FieldName = "NodeType";
            column.Width = 100;
            column.VisibleIndex = 9;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "转子电阻";
            column.FieldName = "HuganTQ2";
            column.Width = 100;
            column.VisibleIndex = 10;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "转子电抗";
            column.FieldName = "HuganTQ5";
            column.Width = 100;
            column.VisibleIndex = 11;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "定子电抗";
            column.FieldName = "HuganTQ4";
            column.Width = 100;
            column.VisibleIndex = 12;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "初始滑差";
            column.FieldName = "SkN";
            column.Width = 100;
            column.VisibleIndex = 13;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "连接母线";
            column.FieldName = "IName";
            column.Width = 100;
            column.VisibleIndex = 14;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "投产年份";
            column.FieldName = "OperationYear";
            column.Width = 100;
            column.VisibleIndex = 15;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "运行状态";
            column.FieldName = "KSwitchStatus";
            column.Width = 100;
            column.VisibleIndex = 16;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "单位";
            column.FieldName = "UnitFlag";
            column.Width = 100;
            column.VisibleIndex = 17;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "惯性时间常数";
            column.FieldName = "I0";
            column.Width = 100;
            column.VisibleIndex = 18;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "定子开路";
            column.FieldName = "P0";
            column.Width = 100;
            column.VisibleIndex = 18;
            column.OptionsColumn.AllowEdit = false;
        }
        public override string GetClassName()
        {
            return "PSPDEV";
        }
        public override string GetType()
        {
            return "12";
        }
        #region 记录操作
        /// <summary>
        ///并联电抗首末节点号进行更新
        /// </summary>
        public override void UpdateNumber()
        {
            try
            {
                if (updatenumberflag)
                {
                    con = "where projectid='" + Itop.Client.MIS.ProgUID + "' and Type='12'";

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
                        UCDeviceBase.DataService.Update<PSPDEV>(dev);
                    }
                    Init();
                    MessageBox.Show("更新成功！");
                }
                else
                {
                    con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + projectdeviceid + "' and pspdev.Type='12'";

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
        public override void Add()
        {
            //frmDLQdlg dlg = new frmDLQdlg();
            //dlg.Name = "";
            //if (dlg.ShowDialog() == DialogResult.OK) {
            //    //增加记录 
            //    PSPDEV dev = dlg.DeviceMx;
            //    dev.ProjectID = this.ProjectID;
            //    UCDeviceBase.DataService.Create("InsertPSPDEV", dev);
            //    DataRow row=datatable1.NewRow();
            //    Itop.Common.DataConverter.ObjectToRow(dev, row);
            //    datatable1.Rows.Add(row); 
            //}
            frmFHdlg dlg = new frmFHdlg();
            dlg.ProjectID = this.ProjectID;
            dlg.Name = "";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //增加记录 
                PSPDEV dev = dlg.DeviceMx;
                dev.ProjectID = this.ProjectID;
                UCDeviceBase.DataService.Create("InsertPSPDEV", dev);
                DataRow row = datatable1.NewRow();
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
                datatable1.Rows.Add(row);
            }
        }
        public override void Delete()
        {
            //删除记录     
            base.Delete();
        }
        public override void Edit()
        {
            frmFHdlg dlg = new frmFHdlg();
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                PSPDEV dev = Itop.Common.DataConverter.RowToObject<PSPDEV>(row);
                if (dev.Type == "12")
                {
                    if (dev.NodeType == "恒阻抗模型")
                    {
                        dev.NodeType = "0";
                    }
                    else
                    {
                        dev.NodeType = "1";
                    }
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
