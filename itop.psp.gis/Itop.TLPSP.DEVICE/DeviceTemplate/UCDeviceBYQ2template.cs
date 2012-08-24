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
    public partial class UCDeviceBYQ2template : Itop.TLPSP.DEVICE.UCDeviceBase
    {
        public UCDeviceBYQ2template()
        {
            InitializeComponent();            
        }        
        #region 初始设置
        /// <summary>
        /// 设备初始化
        /// </summary>
        public override void Init() {
            datatable1 = null;
            con = "type='02'";

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
            con = "and Type='02'";
            base.SelDevices();
        }
        /// <summary>
        ///短路图数据查询入口
        /// </summary>
        public override void SelshortDevices()
        {
            con = " and Type='02'";
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
            //column = gridView1.Columns.Add();
            //column.Caption = "高压侧母线";
            //column.FieldName = "IName";
            //column.Width = 100;
            //column.VisibleIndex = 3;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "低压侧母线";
            //column.FieldName = "JName";
            //column.Width = 100;
            //column.VisibleIndex = 4;
            //column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "电阻";
            column.FieldName = "LineR";
            column.Width = 100;
            column.VisibleIndex = 5;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "电抗";
            column.FieldName = "LineTQ";
            column.Width = 100;
            column.VisibleIndex = 6;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "电纳";
            column.FieldName = "LineGNDC";
            column.Width = 100;
            column.VisibleIndex = 7;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "电导";
            column.FieldName = "G";
            column.Width = 100;
            column.VisibleIndex = 8;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "变比";
            column.FieldName = "K";
            column.Width = 100;
            column.VisibleIndex = 9;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "短路容量";
            column.FieldName = "Burthen";
            column.Width = 100;
            column.VisibleIndex = 10;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "高压侧连接方式";
            column.FieldName = "LineLevel";
            column.Width = 100;
            column.VisibleIndex = 11;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "低压侧连接方式";
            column.FieldName = "LineType";
            column.Width = 100;
            column.VisibleIndex = 12;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "中心点接地电抗";
            column.FieldName = "BigTQ";
            column.Width = 100;
            column.VisibleIndex = 13;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "中心点接地电阻";
            column.FieldName = "SmallTQ";
            column.Width = 100;
            column.VisibleIndex = 14;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "高压侧额定电压";
            column.FieldName = "Vi0";
            column.Width = 100;
            column.VisibleIndex = 15;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "低压侧额定电压";
            column.FieldName = "Vj0";
            column.Width = 100;
            column.VisibleIndex = 16;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "高压侧基准电压";
            column.FieldName = "Vib";
            column.Width = 100;
            column.VisibleIndex = 17;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "低压侧基准电压";
            column.FieldName = "Vjb";
            column.Width = 100;
            column.VisibleIndex = 18;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "运行状态";
            column.FieldName = "KSwitchStatus";
            column.Width = 100;
            column.VisibleIndex = 19;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "单位";
            column.FieldName = "UnitFlag";
            column.Width = 100;
            column.VisibleIndex = 20;
            column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "投产年份";
            //column.FieldName = "OperationYear";
            //column.Width = 100;
            //column.VisibleIndex = 21;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "高压侧连接开关";
            //column.FieldName = "ISwitch";
            //column.Width = 100;
            //column.VisibleIndex = 22;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "低压侧连接开关";
            //column.FieldName = "JSwitch";
            //column.Width = 100;
            //column.VisibleIndex = 23;
            //column.OptionsColumn.AllowEdit = false;
        }
        #endregion
        public override string GetClassName()
        {
            return "Template_PSPDEV";
        }
        public override string GetType()
        {
            return "02";
        }
        #region 记录操作
        public override void UpdateIn(DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i][0].ToString().IndexOf("合计") > 0 || table.Rows[i][1].ToString().IndexOf("合计") > 0)
                    continue;
                Template_PSPDEV area = new Template_PSPDEV();
                //area.ProjectID = projectid;
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
                if (!string.IsNullOrEmpty(ParentID))
                {
                    area.SvgUID = ParentID;
                }
                area.Type = GetType();
                UCDeviceBase.DataService.Create<Template_PSPDEV>(area);

            }
        }
        /// <summary>
        ///变压器首末节点号进行更新
        /// </summary>
        public override void UpdateNumber()
        {
            try
            {
                if (updatenumberflag)
                {
                    con = "where projectid='" + Itop.Client.MIS.ProgUID + "' and Type='02'";

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
                        UCDeviceBase.DataService.Update<PSPDEV>(dev);
                    }
                    Init();
                    MessageBox.Show("更新完成！");
                }
                else
                {
                    con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + projectdeviceid + "' and pspdev.Type='02'";

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
            frmBYQ2templatedlg dlg = new frmBYQ2templatedlg();
            dlg.Name = "";
            dlg.ProjectSUID = this.ProjectID;
            if (dlg.ShowDialog() == DialogResult.OK) {
                //增加记录 
                Template_PSPDEV dev = dlg.DeviceMx;
               // dev.ProjectID = this.ProjectID;
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
            //删除记录     
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                Template_PSPDEV dev = Itop.Common.DataConverter.RowToObject<Template_PSPDEV>(row);
                if (Itop.Common.MsgBox.ShowYesNo("是否确认删除［" + dev.TemplateType + "］?") == DialogResult.Yes)
                {
                    UCDeviceBase.DataService.Delete<Template_PSPDEV>(dev);
                    ((DataTable)gridControl1.DataSource).Rows.Remove(row);
                }
            }
        }
        public override void Edit() {
            frmBYQ2templatedlg dlg = new frmBYQ2templatedlg(); 
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
                    //
                    //string sql = "S1='" + dev.Name + "'and S4='两绕组变压器'";
                    //IList<Psp_Attachtable> ilist = UCDeviceBase.DataService.GetList<Psp_Attachtable>("SelectPsp_AttachtableByCont", sql);
                    //if (ilist.Count>0)
                    //{
                    //    Psp_Attachtable pdr = ilist[0];
                    //    pdr.ZHI =(double) dev.Burthen;
                    //    pdr.S3 = dev.OperationYear;
                    //    pdr.startYear = dev.Date1;
                    //    pdr.endYear= dev.Date2;
                    //    UCDeviceBase.DataService.Update<Psp_Attachtable>(pdr);
                    //}
                }
            }
        }
        
        #endregion
    }
}

