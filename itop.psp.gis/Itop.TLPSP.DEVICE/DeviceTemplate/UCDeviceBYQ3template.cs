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

namespace Itop.TLPSP.DEVICE
{
    /// <summary>
    /// 母线
    /// </summary>
    public partial class UCDeviceBYQ3template : Itop.TLPSP.DEVICE.UCDeviceBase
    {
        public UCDeviceBYQ3template()
        {
            InitializeComponent();            
        }        
        #region 初始设置
        /// <summary>
        /// 设备初始化
        /// </summary>
        public override void Init() {
            datatable1 = null;
            con = "type='03'";

            IList list = DataService.GetList("SelectTemplate_PSPDEVByWhere", con);
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(Template_PSPDEV));

            gridControl1.DataSource = datatable1;
            gridControl1.UseEmbeddedNavigator = true;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;     
        }
        /// <summary>
        /// 检索设备
        /// </summary>
        public override void SelDevices()
        {
            con = "and Type='03'";
            base.SelDevices();
        }
        /// <summary>
        ///短路图数据查询入口
        /// </summary>
        public override void SelshortDevices()
        {
            con = " and Type='03'";
            base.SelshortDevices();
        }
        /// <summary>
        /// 设置设备显示列
        /// </summary>
        public override void InitColumns() {            
            GridColumn column = gridView1.Columns.Add();
            column.Caption = "类型";
            column.FieldName = "TemplateType";
            column.Width = 100;
            column.VisibleIndex = 1;
            column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "编号";
            //column.FieldName = "Number";
            //column.Width = 100;
            //column.VisibleIndex = 2;
            //column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "高压侧母线";
            column.FieldName = "IName";
            column.Width = 100;
            column.VisibleIndex = 3;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "中压侧母线";
            column.FieldName = "JName";
            column.Width = 100;
            column.VisibleIndex = 4;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "低压侧母线";
            column.FieldName = "KName";
            column.Width = 100;
            column.VisibleIndex = 5;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "高压侧绕组类型";
            column.FieldName = "LineLevel";
            column.Width = 100;
            column.VisibleIndex =6;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "中压侧绕组类型";
            column.FieldName = "LineType";
            column.Width = 100;
            column.VisibleIndex =7;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "低压侧绕组类型";
            column.FieldName = "LineStatus";
            column.Width = 100;
            column.VisibleIndex = 8;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "中性点接地电阻";
            column.FieldName = "SmallTQ";
            column.Width = 100;
            column.VisibleIndex = 9;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "中性点接地电抗";
            column.FieldName = "BigTQ";
            column.Width = 100;
            column.VisibleIndex = 10;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "电纳";
            column.FieldName = "LineGNDC";
            column.Width = 100;
            column.VisibleIndex = 11;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "电导";
            column.FieldName = "G";
            column.Width = 100;
            column.VisibleIndex = 12;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "高压侧变比";
            column.FieldName = "K";
            column.Width = 100;
            column.VisibleIndex = 13;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "中压侧变比";
            column.FieldName = "StandardCurrent";
            column.Width = 100;
            column.VisibleIndex = 14;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "低压侧变比";
            column.FieldName = "BigP";
            column.Width = 100;
            column.VisibleIndex = 15;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "高压侧容量";
            column.FieldName = "SiN";
            column.Width = 100;
            column.VisibleIndex = 16;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "中压侧容量";
            column.FieldName = "SjN";
            column.Width = 100;
            column.VisibleIndex = 17;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "低压侧容量";
            column.FieldName = "SkN";
            column.Width = 100;
            column.VisibleIndex = 18;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "Ri";
            column.FieldName = "HuganTQ1";
            column.Width = 100;
            column.VisibleIndex = 19;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "Xi";
            column.FieldName = "HuganTQ4";
            column.Width = 100;
            column.VisibleIndex = 20;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "Rj";
            column.FieldName = "HuganTQ2";
            column.Width = 100;
            column.VisibleIndex = 21;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "Xj";
            column.FieldName = "HuganTQ5";
            column.Width = 100;
            column.VisibleIndex = 22;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "Rk";
            column.FieldName = "HuganTQ3";
            column.Width = 100;
            column.VisibleIndex = 23;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "Xk";
            column.FieldName = "ZeroTQ";
            column.Width = 100;
            column.VisibleIndex = 24;
            column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "运行状态";
            //column.FieldName = "KSwitchStatus";
            //column.Width = 100;
            //column.VisibleIndex = 25;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "单位";
            //column.FieldName = "UnitFlag";
            //column.Width = 100;
            //column.VisibleIndex = 26;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "投产年份";
            //column.FieldName = "OperationYear";
            //column.Width = 100;
            //column.VisibleIndex = 27;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "高压侧连接开关";
            //column.FieldName = "ISwitch";
            //column.Width = 100;
            //column.VisibleIndex = 28;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "中压侧连接开关";
            //column.FieldName = "JSwitch";
            //column.Width = 100;
            //column.VisibleIndex = 29;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "低压侧连接开关";
            //column.FieldName = "HuganLine1";
            //column.Width = 100;
            //column.VisibleIndex = 30;
            //column.OptionsColumn.AllowEdit = false;
        }
        #endregion
        public override string GetClassName()
        {
            return "Template_PSPDEV";
        }
        public override string GetType()
        {
            return "03";
        }
        #region 记录操作
        /// <summary>
        ///变压器首末节点号进行更新
        /// </summary>
        public override void UpdateNumber()
        {
            try
            {
                if (updatenumberflag)
                {
                    con = "where projectid='" + Itop.Client.MIS.ProgUID + "' and Type='03'";

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
                                dev.Vi0 = psp.RateVolt;
                                dev.Vib = psp.ReferenceVolt;

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
                                dev.Vj0 = psp.RateVolt;
                                dev.Vjb = psp.ReferenceVolt;
                            }
                        }
                        string kname = dev.KName;
                        if (kname != null)
                        {
                            con = "where projectid='" + Itop.Client.MIS.ProgUID + "'and type='01'and name='" + kname + "'";
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
                    Init();
                    MessageBox.Show("更新完成！");
                }
                else
                {
                    con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + projectdeviceid + "' and pspdev.Type='03'";

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
                                dev.Vi0 = psp.RateVolt;
                                dev.Vib = psp.ReferenceVolt;

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
                                dev.Vj0 = psp.RateVolt;
                                dev.Vjb = psp.ReferenceVolt;
                            }
                        }
                        string kname = dev.KName;
                        if (kname != null)
                        {
                            con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + projectdeviceid + "'and pspdev.type='01'and pspdev.name='" + kname + "'";
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
                    Init();
                    MessageBox.Show("更新完成！");
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
                    return Itop.Common.DataConverter.RowToObject<Template_PSPDEV>(row);
                }
                return base.SelectedDevice;
            }
        }
        public override void Add() {
            frmBYQ3templatedlg dlg = new frmBYQ3templatedlg();
            dlg.Name = "";
            dlg.ProjectSUID = this.ProjectID;
            if (dlg.ShowDialog() == DialogResult.OK) {
                //增加记录 
                Template_PSPDEV dev = dlg.DeviceMx;
                //dev.ProjectID = this.ProjectID;
                UCDeviceBase.DataService.Create<Template_PSPDEV>(dev);
                DataRow row=datatable1.NewRow();
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
                datatable1.Rows.Add(row); 
            }
        }
        public override void Delete() {
            //删除记录     
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                Template_PSPDEV dev = Itop.Common.DataConverter.RowToObject<Template_PSPDEV>(row);
                if (Itop.Common.MsgBox.ShowYesNo("是否确认删除［" + dev.TemplateType+ "］?") == DialogResult.Yes)
                {
                    UCDeviceBase.DataService.Delete<Template_PSPDEV>(dev);
                    ((DataTable)gridControl1.DataSource).Rows.Remove(row);
                }
            }
        }
        public override void Edit() {
            frmBYQ3templatedlg dlg = new frmBYQ3templatedlg(); 
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                Template_PSPDEV dev = Itop.Common.DataConverter.RowToObject<Template_PSPDEV>(row);
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
                    UCDeviceBase.DataService.Update<Template_PSPDEV>(dev);
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
                    //string sql = "S1='" + dev.Name + "'and S4='三绕组变压器'";
                    //IList<Psp_Attachtable> ilist = UCDeviceBase.DataService.GetList<Psp_Attachtable>("SelectPsp_AttachtableByCont", sql);
                    //if (ilist.Count > 0)
                    //{
                    //    Psp_Attachtable pdr = ilist[0];
                    //    pdr.ZHI =dev.SiN;
                    //    pdr.S3 = dev.OperationYear;
                    //    pdr.startYear = dev.Date1;
                    //    pdr.endYear = dev.Date2;
                    //    UCDeviceBase.DataService.Update<Psp_Attachtable>(pdr);
                    //}
                }
            }
        }
        
        #endregion
    }
}

