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
using DevExpress.XtraEditors.Repository;

namespace Itop.TLPSP.DEVICE
{
    /// <summary>
    /// 变电站
    /// </summary>
    public partial class UCDeviceBDZ : Itop.TLPSP.DEVICE.UCDeviceBase
    {
        public UCDeviceBDZ() {
            InitializeComponent();
        }

        #region 初始设置
        /// <summary>
        /// 设备初始化
        /// </summary>
        public override void Init() {
            datatable1 = null;
            if (DeviceHelper.bdzwhere != "")
            {
                con = strCon + " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND UID NOT IN (SELECT DeviceSUID FROM PSP_ELCDEVICE WHERE ProjectSUID ='" + this.ProjectID + "')";
            }
            else
            {
                con = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND UID NOT IN (SELECT DeviceSUID FROM PSP_ELCDEVICE WHERE ProjectSUID ='" + this.ProjectID + "')";
            }
            IList list = DataService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_Substation_Info));
            datatable1.Columns.Add("flag_", typeof(string), "IIF(flag='2','规划','现状')");
           
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

            IList list = DataService.GetList("SelectPSP_Substation_InfoListByWhere", con);

            //List<PSP_Substation_Info> delsum = new List<PSP_Substation_Info>();
            //foreach (PSP_Substation_Info dev in list)
            //{

            //    con = "WHERE ProjectSUID = '" + this.ProjectID + "'AND DeviceSUID='"+dev.UID+"' ";
            //    IList list1 = DataService.GetList("SelectPSP_ElcDeviceByCondition", con);
            //    if (list1.Count > 0)
            //        delsum.Add(dev);

            //}

            //for (int m = 0; m < delsum.Count;m++ )
            //{
            //    list.Remove(delsum[m]);
            //}
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_Substation_Info));
            datatable1.Columns.Add("flag_", typeof(string), "IIF(flag='2','规划','现状')");

            gridControl1.DataSource = datatable1;
        }
        public override void proInit(string year)
        {
            datatable1 = null;
            
                con = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND  UID IN (SELECT PSPDEV.SVGUID FROM PSPDEV, PSP_ELCDEVICE WHERE  PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + year + "')";
            
            
                //con = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND UID IN (SELECT DeviceSUID FROM PSP_ELCDEVICE WHERE ProjectSUID ='" + year + "')";
           
            IList list = DataService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            //if (!string.IsNullOrEmpty(year))
            //{
            //    List<PSP_Substation_Info> listremove = new List<PSP_Substation_Info>();
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        if (((PSP_Substation_Info)list[i]).S2.Length != 4)
            //        {
            //            listremove.Add((PSP_Substation_Info)list[i]);
            //        }
            //        else
            //        {
            //            if (Convert.ToDouble(((PSP_Substation_Info)list[i]).S2) > Convert.ToDouble(year))
            //            {
            //                listremove.Add((PSP_Substation_Info)list[i]);
            //            }
            //            if (((PSP_Substation_Info)list[i]).L29.Length == 4)
            //            {
            //                if (Convert.ToDouble(((PSP_Substation_Info)list[i]).L29) < Convert.ToDouble(year))
            //                {
            //                    listremove.Add((PSP_Substation_Info)list[i]);
            //                }
            //            }
            //        }
            //    }
            //    for (int i = 0; i < listremove.Count; i++)
            //    {
            //        list.Remove(listremove[i]);
            //    }
            //}
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_Substation_Info));
            datatable1.Columns.Add("flag_", typeof(string), "IIF(flag='2','规划','现状')");

            gridControl1.DataSource = datatable1;
            //隐藏详情
            gridView1.Columns["AreaID"].Visible = false;
            gridControl1.UseEmbeddedNavigator = true;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
        }
        public override void PspInit(IList<object> listUID)
        {
            datatable1 = null;
            con = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND  UID IN (SELECT PSPDEV.SVGUID FROM PSPDEV, PSP_ELCDEVICE WHERE  PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + this.ProjectID + "')";

            IList list = DataService.GetList("SelectPSP_Substation_InfoListByWhere", con);
          

            List<PSP_Substation_Info> delsum = new List<PSP_Substation_Info>();
            //foreach (PSP_Substation_Info dev in list)
            //{

            //    con = "WHERE ProjectSUID = '" + this.ProjectID + "'AND DeviceSUID='"+dev.UID+"' ";
            //    IList list1 = DataService.GetList("SelectPSP_ElcDeviceByCondition", con);
            //    if (list1.Count > 0)
            //        delsum.Add(dev);
                
            //}
            //list.Remove(listUID);
            bool psiflag = false;
            try
            { 
                for (int i = 0; i < list.Count;i++)
                {
                    psiflag = false;
                    foreach (PSP_Substation_Info psi in listUID)
                    {
                        if (psi.UID == ((PSP_Substation_Info)list[i]).UID)
                        {
                            psiflag = true;                                             
                        }
                    }
                    if (!psiflag)
                    {
                        delsum.Add((PSP_Substation_Info)list[i]);
                    }
                }
            }
            catch (Exception e)
            {}
           // list.Remove(delsum);

            datatable1 = Itop.Common.DataConverter.ToDataTable(delsum, typeof(PSP_Substation_Info));
            datatable1.Columns.Add("flag_", typeof(string), "IIF(flag='2','规划','现状')");

            gridControl1.DataSource = datatable1;
        }
        /// <summary>
        /// 专门用于网架优化显示
        /// </summary>
        public override void WjghInit()
        {
            datatable1 = null;
            con = "AreaID = '" + Itop.Client.MIS.ProgUID + "'AND UID IN(select DeviceSUID from PSP_GprogElevice where type='变电站' and L2='0'and GprogUID='"+wjghuid+"')";
            IList list = DataService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_Substation_Info));
            datatable1.Columns.Add("flag_", typeof(string), "IIF(flag='2','规划','现状')");

            gridControl1.DataSource = datatable1;
        }
        /// <summary>
        /// 根据查询条件，重新选择设备
        /// </summary>
        public override void SelDevices()
        {
            datatable1 = null;
            con = strCon ;
            IList list = DataService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_Substation_Info));
            datatable1.Columns.Add("flag_", typeof(string), "IIF(flag='2','规划','现状')");
            gridControl1.DataSource = datatable1;
        }
        /// <summary>
        /// 设置设备显示列
        /// </summary>
        public override void InitColumns() {
            GridColumn column = gridView1.Columns.Add();
            column.Caption = "变电站名称";
            column.FieldName = "Title";
            column.Width = 100;
            column.VisibleIndex = 1;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "电压等级";
            column.FieldName = "L1";
            column.Width = 100;
            column.VisibleIndex = 2;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "容量";
            column.FieldName = "L2";
            column.Width = 100;
            column.VisibleIndex = 3;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "容量构成";
            column.FieldName = "L4";
            column.Width = 100;
            column.VisibleIndex = 5;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "主变台数";
            column.FieldName = "L3";
            column.Width = 100;
            column.VisibleIndex = 6;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "负载率";
            column.FieldName = "L10";
            column.Width = 100;
            column.VisibleIndex = 7;
            column.OptionsColumn.AllowEdit = false;
           
            column = gridView1.Columns.Add();
            column.Caption = "投产年份";
            column.FieldName = "S2";
            column.Width = 100;
            column.VisibleIndex = 8;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "退役年份";
            column.FieldName = "L29";
            column.Width = 100;
            column.VisibleIndex = 9;
            column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "停产年份";
            //column.FieldName = "L12";
            //column.Width = 100;
            //column.VisibleIndex = 5;
            //column.OptionsColumn.AllowEdit = false;
            
            column = gridView1.Columns.Add();
            column.Caption = "电网类型";
            column.FieldName = "DQ";
            column.Width = 100;
            column.VisibleIndex = 10;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "所在区域";
            column.FieldName = "AreaName";
            column.Width = 100;
            column.VisibleIndex = 11;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "无功容量";
            column.FieldName = "L26";
            column.Width = 100;
            column.VisibleIndex = 12;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "无功补偿构成";
            column.FieldName = "L27";
            column.Width = 100;
            column.VisibleIndex = 13;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "查看";
            column.FieldName = "AreaID";
            column.Width = 100;
            column.VisibleIndex = 14;
           // column.OptionsColumn.AllowEdit = false;
            RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1 = new RepositoryItemHyperLinkEdit();
            repositoryItemHyperLinkEdit1.AutoHeight = false;
            repositoryItemHyperLinkEdit1.Caption = "站内详情";
            repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            repositoryItemHyperLinkEdit1.Click += new System.EventHandler(this.repositoryItemHyperLinkEdit1_Click);
            
            column.ColumnEdit =repositoryItemHyperLinkEdit1;

        }
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            int ihand = gridView1.FocusedRowHandle;
            if (ihand < 0)
                return;
            DataRow dr = gridView1.GetDataRow(ihand);
            PSP_Substation_Info pj = Itop.Common.DataConverter.RowToObject<PSP_Substation_Info>(dr);
            double rl = 0;
            int bts = 0;
            frmDeviceManager_children frmc = new frmDeviceManager_children();
            frmc.ParentObj = pj;
            string[] types = new string[] { "01", "02","03", "12" };
            frmc.childrendevice(types);
            if (frmc.DialogResult == DialogResult.OK)
            {
                string rlgc = "";
                string where = "where projectid='" + Itop.Client.MIS.ProgUID + "'and type in ('02','03')and SvgUID='" + pj.UID + "'";
                IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", where);
                foreach (PSPDEV pd in list)
                {
                    if (!string.IsNullOrEmpty(pd.OperationYear) && !string.IsNullOrEmpty(pd.Date2) && pd.Date2.Length == 4 && !string.IsNullOrEmpty(pj.L29) && !string.IsNullOrEmpty(pj.L28))
                    {
                        if (Convert.ToInt32(pd.OperationYear) >= Convert.ToInt32(pj.L28) && Convert.ToInt32(pd.Date2) <= Convert.ToInt32(pj.L29))
                        {
                            if (pd.Type == "03")
                            {
                                rl += pd.SiN;
                                rlgc += pd.SiN.ToString("G") + "+";
                            }
                            else
                            {
                                rl += (double)pd.Burthen;
                                rlgc += ((double)pd.Burthen).ToString("G") + "+";
                            }

                            bts++;
                        }
                    }
                    else
                    {
                        if (pd.Type == "03")
                        {
                            rl += pd.SiN;
                            rlgc += pd.SiN.ToString("G") + "+";
                        }
                        else
                        {
                            rl += (double)pd.Burthen;
                            rlgc += ((double)pd.Burthen).ToString("G") + "+";
                        }


                        bts++;
                    }
                }
                if (rlgc.Length > 0)
                {
                    pj.L4 = rlgc.Substring(0, rlgc.Length - 1);
                    dr["L4"] = pj.L4;
                }
                if (rl!=0)
                {
                    pj.L2 = rl;
                    pj.L3 = bts;

                    dr["L2"] = rl;
                    dr["L3"] = bts;
                }
              
                
                UCDeviceBase.DataService.Update<PSP_Substation_Info>(pj);
            }
        }
        #endregion
        #region 记录操作
        public override object SelectedDevice {
            get {
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                
                if (row != null) {
                    return Itop.Common.DataConverter.RowToObject<PSP_Substation_Info>(row);
                }
                return base.SelectedDevice;
            }
        }

        public override string GetClassName()
        {
            return "PSP_Substation_Info";
        }
        public override string GetType()
        {
            return "20";
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
                        PSP_Substation_Info dev = Itop.Common.DataConverter.RowToObject<PSP_Substation_Info>(dr);
                        UCDeviceBase.DataService.Delete<PSP_Substation_Info>(dev);
                        //删除第二层数据

                        string delcon = "SvgUID='" + dr["UID"].ToString() + "'and ProjectID = '" + Itop.Client.MIS.ProgUID + "'";
                        DataService.Update("DeletePSPDEVbywhere", delcon);
                    }
                }
                dat.Clear();
                gridView1.GridControl.DataSource = dat;
            }


        }
        public override void UpdateIn(DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i][0].ToString().IndexOf("合计") > 0 || table.Rows[i][1].ToString().IndexOf("合计") > 0)
                    continue;
                PSP_Substation_Info area = new PSP_Substation_Info();
                //area.UID += "|" + ProjectID;
                area.AreaID = ProjectID;
               // area.CreateDate = DateTime.Now;
                
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
                                else if(type==typeof(DateTime))
                                {

                                }
                            }
                        }
                    }
                    catch { MessageBox.Show(string.Format("第{0}行{1}列插入有问题", i.ToString(), col.Caption)); }
                }
                DataService.Create<PSP_Substation_Info>(area);
                
            }
        }
        public override void Add() {
            frmBDZdlg dlg = new frmBDZdlg();
            dlg.ProjectID = Itop.Client.MIS.ProgUID;
            dlg.Name = "";
            dlg.CsbuttonVisble(true);
            dlg.IsRead = false;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //增加记录 
                PSP_Substation_Info dev = dlg.DeviceMx;
                dev.AreaID = Itop.Client.MIS.ProgUID;
                if (dlg.bcflag)   //在点击设备参数的时候 已经进行了保存
                {
                    DataService.Update<PSP_Substation_Info>(dev);
                }
                else
                    DataService.Create<PSP_Substation_Info>(dev);
                DataRow row = datatable1.NewRow();
                Itop.Common.DataConverter.ObjectToRow(dev, row);
                datatable1.Rows.Add(row);
            }
            else
            {
                if (dlg.bcflag)
                {
                    PSP_Substation_Info dev = dlg.DeviceMx;
                    dev.AreaID = Itop.Client.MIS.ProgUID;
                    DataService.Update<PSP_Substation_Info>(dev);
                    DataRow row = datatable1.NewRow();
                    Itop.Common.DataConverter.ObjectToRow(dev, row);
                    datatable1.Rows.Add(row);
                }
            }
        }
        public override void Delete() {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null) {

                PSP_Substation_Info dev = Itop.Common.DataConverter.RowToObject<PSP_Substation_Info>(row);
                DialogResult dr = Itop.Common.MsgBox.ShowYesNo("是否确认删除变电站［"+dev.Title+"］?");
                if (dr == DialogResult.Yes) {
                    DataService.Delete<PSP_Substation_Info>(dev);
                    ((DataTable)gridControl1.DataSource).Rows.Remove(row);
                    //删除第二层数据

                    string delcon = "SvgUID='" + dev.UID + "'and ProjectID = '" + Itop.Client.MIS.ProgUID + "'";
                    DataService.Update("DeletePSPDEVbywhere", delcon);
                }
            }
        }
        public override void Edit() {
            frmBDZdlg dlg = new frmBDZdlg();
            dlg.CsbuttonVisble(true);
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null) {
                PSP_Substation_Info dev = Itop.Common.DataConverter.RowToObject<PSP_Substation_Info>(row);
                dlg.DeviceMx = dev;
                dlg.IsRead = false;
                if (dlg.ShowDialog() == DialogResult.OK) {
                    //更新记录
                    dev = dlg.DeviceMx;
                    dev.AreaID = Itop.Client.MIS.ProgUID;
                    DataService.Update<PSP_Substation_Info>(dev);
                    Itop.Common.DataConverter.ObjectToRow(dev, row);
                }
            }
        }
        public override void Save() {

        }
        public override void Print() {

        }
        #endregion
    }
}

