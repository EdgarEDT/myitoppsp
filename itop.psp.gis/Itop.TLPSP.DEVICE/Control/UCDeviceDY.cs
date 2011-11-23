using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using System.Collections;
using DevExpress.XtraGrid.Columns;

namespace Itop.TLPSP.DEVICE
{
    /// <summary>
    /// 电源
    /// </summary>
    public partial class UCDeviceDY : Itop.TLPSP.DEVICE.UCDeviceBase
    {
        public UCDeviceDY()
        {
            InitializeComponent();
        }

        #region 初始设置
        /// <summary>
        /// 设备初始化
        /// </summary>
        public override void Init()
        {
            datatable1 = null;
            con = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND UID NOT IN (SELECT DeviceSUID FROM PSP_ELCDEVICE WHERE ProjectSUID ='" + this.ProjectID + "')";

            IList list = DataService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", con);
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_PowerSubstation_Info));
            datatable1.Columns.Add("flag_", typeof(string), "IIF(flag=2,'规划','现状')");

            gridControl1.DataSource = datatable1;
            gridControl1.UseEmbeddedNavigator = true;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;

        }
        /// <summary>
        /// 专门用于潮流中的数据显示
        /// </summary>
        public override void PspInit()
        {
            datatable1 = null;
            con = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND  UID IN (SELECT PSPDEV.SVGUID FROM PSPDEV, PSP_ELCDEVICE WHERE  PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + this.ProjectID + "')";

            IList list = DataService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", con);
           
            List<PSP_PowerSubstation_Info> delsum = new List<PSP_PowerSubstation_Info>();
            foreach (PSP_PowerSubstation_Info dev in list)
            {

                con = "WHERE ProjectSUID = '" + this.ProjectID + "'AND DeviceSUID='" + dev.UID + "' ";
                IList list1 = DataService.GetList("SelectPSP_ElcDeviceByCondition", con);
                if (list1.Count > 0)
                    delsum.Add(dev);

            }
            for (int m = 0; m < delsum.Count; m++)
            {
                list.Remove(delsum[m]);
            }
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_PowerSubstation_Info));
            datatable1.Columns.Add("flag_", typeof(string), "IIF(flag=2,'规划','现状')");
            gridControl1.DataSource = datatable1;
        }
        public override void PspInit(IList<object> listUID)
        {
            datatable1 = null;
            con = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND  UID IN (SELECT PSPDEV.SVGUID FROM PSPDEV, PSP_ELCDEVICE WHERE  PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + this.ProjectID + "')";

            IList list = DataService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", con);

            //List<PSP_PowerSubstation_Info> delsum = new List<PSP_PowerSubstation_Info>();
            //foreach (PSP_PowerSubstation_Info dev in list)
            //{

            //    con = "WHERE ProjectSUID = '" + this.ProjectID + "'AND DeviceSUID='" + dev.UID + "' ";
            //    IList list1 = DataService.GetList("SelectPSP_ElcDeviceByCondition", con);
            //    if (list1.Count > 0)
            //        delsum.Add(dev);

            //}
            //for (int m = 0; m < delsum.Count; m++)
            //{
            //    list.Remove(delsum[m]);
            //}
            List<PSP_PowerSubstation_Info> delsum = new List<PSP_PowerSubstation_Info>();
            bool psiflag = false;
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    psiflag = false;
                    foreach (PSP_PowerSubstation_Info psi in listUID)
                    {
                        if (psi.UID == ((PSP_PowerSubstation_Info)list[i]).UID)
                        {
                            psiflag = true;
                        }
                    }
                    if (!psiflag)
                    {
                        delsum.Add((PSP_PowerSubstation_Info)list[i]);
                    }
                }
            }
            catch (Exception e)
            { }
            datatable1 = Itop.Common.DataConverter.ToDataTable(delsum, typeof(PSP_PowerSubstation_Info));
            datatable1.Columns.Add("flag_", typeof(string), "IIF(flag=2,'规划','现状')");
            gridControl1.DataSource = datatable1;
        }
        /// <summary>
        /// 专门用于网架优化显示
        /// </summary>
        public override void WjghInit()
        {
            datatable1 = null;
            con = "AreaID = '" + Itop.Client.MIS.ProgUID + "'AND UID IN(select DeviceSUID from PSP_GprogElevice where type='电源' and L2='0'AND GprojUID='"+wjghuid+"')";
            IList list = DataService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_PowerSubstation_Info));
            datatable1.Columns.Add("flag_", typeof(string), "IIF(flag=2,'规划','现状')");

            gridControl1.DataSource = datatable1;
        }
        /// <summary>
        /// 根据查询条件，重新选择设备
        /// </summary>
        public override void SelDevices()
        {
            datatable1 = null;
            con = strCon;
            IList list = DataService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", con);
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_PowerSubstation_Info));
            datatable1.Columns.Add("flag_", typeof(string), "IIF(flag=2,'规划','现状')");

            gridControl1.DataSource = datatable1;
        }
        /// <summary>
        /// 设置设备显示列
        /// </summary>
        public override void InitColumns()
        {
            GridColumn column = gridView1.Columns.Add();
            column.Caption = "名称";
            column.FieldName = "Title";
            column.Width = 100;
            column.VisibleIndex = 1;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "电压等级";
            column.FieldName = "S1";
            column.Width = 100;
            column.VisibleIndex = 2;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "容量";
            column.FieldName = "S2";
            column.Width = 100;
            column.VisibleIndex = 3;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "投产年份";
            column.FieldName = "S3";
            column.Width = 100;
            column.VisibleIndex = 4;
            column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "停产年份";
            //column.FieldName = "L12";
            //column.Width = 100;
            //column.VisibleIndex = 5;
            //column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "状态";
            column.FieldName = "flag_";
            column.Width = 100;
            column.VisibleIndex = 6;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "分区类型";
            column.FieldName = "S5";
            column.Width = 100;
            column.VisibleIndex = 7;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "分区名称";
            column.FieldName = "S9";
            column.Width = 100;
            column.VisibleIndex = 8;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "能源类型";
            column.FieldName = "S10";
            column.Width = 100;
            column.VisibleIndex = 9;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "发电量";
            column.FieldName = "S11";
            column.Width = 100;
            column.VisibleIndex = 10;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "发电利用小时数";
            column.FieldName = "S12";
            column.Width = 100;
            column.VisibleIndex = 11;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "厂用电";
            column.FieldName = "S13";
            column.Width = 100;
            column.VisibleIndex = 12;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "是否统调";
            column.FieldName = "S14";
            column.Width = 100;
            column.VisibleIndex = 13;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "公用自备";
            column.FieldName = "S8";
            column.Width = 100;
            column.VisibleIndex = 14;
            column.OptionsColumn.AllowEdit = false;
        }
        #endregion
        public override string GetClassName()
        {
            return "PSP_PowerSubstation_Info";
        }
        public override string GetType()
        {
            return "11";
        }
        public override void UpdateIn(DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i][0].ToString().IndexOf("合计") > 0 || table.Rows[i][1].ToString().IndexOf("合计") > 0)
                    continue;
                PSP_PowerSubstation_Info area = new PSP_PowerSubstation_Info();
                area.UID += "|" + ProjectID;
                area.AreaID = ProjectID;
                area.CreateDate = DateTime.Now;
                foreach (DataColumn col in table.Columns)
                {
                    try
                    {
                        if (table.Rows[i][col] != null)
                        {
                            string inserted = table.Rows[i][col].ToString();
                            if (inserted == "规划" || inserted == "现状")
                                area.GetType().GetProperty("Flag").SetValue(area, inserted == "规划" ? "2" : "1", null);
                            else
                            {
                                Type type = area.GetType().GetProperty(col.ColumnName).PropertyType;//.GetValue(area, null).GetType();
                                if (type == typeof(int))
                                    area.GetType().GetProperty(col.ColumnName).SetValue(area, int.Parse(inserted == "" ? "0" : inserted), null);
                                else if (type == typeof(string))
                                    area.GetType().GetProperty(col.ColumnName).SetValue(area, inserted, null);
                                else if (type == typeof(decimal))
                                    area.GetType().GetProperty(col.ColumnName).SetValue(area, decimal.Parse(inserted == "" ? "0" : inserted), null);
                                else if (type == typeof(double))
                                    area.GetType().GetProperty(col.ColumnName).SetValue(area, double.Parse(inserted == "" ? "0.0" : inserted), null);
                                else if (type == typeof(DateTime))
                                {

                                }
                            }
                        }
                    }
                    catch { MessageBox.Show(string.Format("第{0}行{1}列插入有问题", i.ToString(), col.Caption)); }
                }
                DataService.Create<PSP_PowerSubstation_Info>(area);

            }
        }
        #region 记录操作
        public override object SelectedDevice
        {
            get
            {
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);

                if (row != null)
                {
                    return Itop.Common.DataConverter.RowToObject<PSP_PowerSubstation_Info>(row);
                }
                return base.SelectedDevice;
            }
        }
        public override void Alldel()
        {
            if (Itop.Common.MsgBox.ShowYesNo("是否确认删除所有数据") == DialogResult.Yes)
            {
                DataTable dat = gridView1.GridControl.DataSource as DataTable;
                foreach (DataRow dr in dat.Rows)
                {
                    if (dr != null)
                    {
                        PSP_PowerSubstation_Info dev = Itop.Common.DataConverter.RowToObject<PSP_PowerSubstation_Info>(dr);
                        UCDeviceBase.DataService.Delete<PSP_PowerSubstation_Info>(dev);

                    }
                }
                dat.Clear();
                gridView1.GridControl.DataSource = dat;
            }


        }
        public override void Add()
        {
            frmDYdlg dlg = new frmDYdlg();
            dlg.ProjectID = this.ProjectID;
            dlg.Name = "";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //增加记录 
                PSP_PowerSubstation_Info dev = dlg.DeviceMx;
                dev.AreaID = this.ProjectID;
                DataService.Create<PSP_PowerSubstation_Info>(dev);
                DataRow row = datatable1.NewRow();
                Itop.Common.DataConverter.ObjectToRow(dev, row);
                datatable1.Rows.Add(row);
            }
        }
        public override void Delete()
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {

                PSP_PowerSubstation_Info dev = Itop.Common.DataConverter.RowToObject<PSP_PowerSubstation_Info>(row);
                DialogResult dr = Itop.Common.MsgBox.ShowYesNo("是否确认删除［" + dev.Title + "］?");
                if (dr == DialogResult.Yes)
                {
                    DataService.Delete<PSP_PowerSubstation_Info>(dev);
                    ((DataTable)gridControl1.DataSource).Rows.Remove(row);
                }
            }
        }
        public override void Edit()
        {
            frmDYdlg dlg = new frmDYdlg();
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                PSP_PowerSubstation_Info dev = Itop.Common.DataConverter.RowToObject<PSP_PowerSubstation_Info>(row);
                dlg.DeviceMx = dev;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //更新记录
                    dev = dlg.DeviceMx;
                    dev.AreaID = this.ProjectID;
                    DataService.Update<PSP_PowerSubstation_Info>(dev);
                    Itop.Common.DataConverter.ObjectToRow(dev, row);
                }
            }
        }
        public override void Save()
        {
        }
        public override void Print()
        {
        }
        #endregion
    }
}

