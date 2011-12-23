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
using ItopVector.Tools;
using Itop.Client.Base;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;

namespace ItopVector.Tools {
    public partial class CtrlglebeYearValue : DevExpress.XtraEditors.XtraUserControl {
        public CtrlglebeYearValue() {
            InitializeComponent();
        }
        private DataTable datatable = new DataTable();
        private string parentID;
        public string ParentID {
            get { return parentID; }
            set {
                parentID = value;
                if (!string.IsNullOrEmpty(value)) {
                    RefreshData("ParentID='" + value + "' order by Year");
                }
            }
        }
        glebeProperty parentObj = new glebeProperty();
        public glebeProperty ParentObj {
            get { return parentObj; }
            set {

                parentObj = value;
                if (value == null) {
                    parentID = null;
                } else {
                    ParentID = value.UID;
                }
            }
        }
        private void RefreshData(string con) {
            if (datatable != null) {
                datatable.Columns.Clear();
                gridView1.Columns.Clear();
            }
            AddFixColumn();
            IList<glebeYearValue> pl = Itop.Client.Common.Services.BaseService.GetList<glebeYearValue>("SelectglebeYearValueBywhere", con);
            if (pl.Count>0)
            {
                datatable = Itop.Common.DataConverter.ToDataTable((IList)pl, typeof(glebeYearValue));
                gridControl1.DataSource = datatable;
            }
           
        }
        public void Refresh()
        {
            foreach (DataRow dr in datatable.Rows)
            {
                glebeYearValue gyv = Itop.Common.DataConverter.RowToObject<glebeYearValue>(dr);
                gyv.Burthen = Convert.ToDouble(ParentObj.Burthen) * gyv.FHmdTz;
                gyv.AvgFHmd = Convert.ToDouble(ParentObj.Burthen / (ParentObj.Area + Convert.ToDecimal(ParentObj.ObligateField10))) * gyv.FHmdTz;
                Itop.Client.Common.Services.BaseService.Update<glebeYearValue>(gyv);
            }
            parentID = ParentObj.UID;
        }
        private void AddFixColumn() {

            GridColumn column = new GridColumn();
            column.FieldName = "ID";
            column.VisibleIndex = -1;
            this.gridView1.Columns.Add(column);

            column = new GridColumn();
            column.FieldName = "Year";
            column.Caption = "年份";
            column.VisibleIndex = 0;
            column.Width = 120;
            this.gridView1.Columns.Add(column);
            column = new GridColumn();
            column.FieldName = "Burthen";
            column.Caption = "负荷（MW）";
            column.VisibleIndex = 1;
            column.Width = 100;
            this.gridView1.Columns.Add(column);
            column = new GridColumn();
            column.FieldName = "AvgFHmd";
            column.Caption = "平均负荷密度（MW/KM2）";
            column.VisibleIndex = 2;
            column.Width = 120;
            this.gridView1.Columns.Add(column);
            column = new GridColumn();
            column.FieldName = "FHmdTz";
            column.Caption = "负荷密度调整";
            column.VisibleIndex = 3;
            column.Width = 120;
            this.gridView1.Columns.Add(column);
            
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            FrmgleYearEdit PDT = new FrmgleYearEdit();
            if (ParentObj == null) {
                Itop.Common.MsgBox.ShowYesNo("配电区域为空！");
                return;
            }
            PDT.ParentObj = ParentObj;
            glebeYearValue pdr = new glebeYearValue();
            pdr.ParentID = ParentObj.UID;
            PDT.RowData = pdr;
            if (PDT.ShowDialog() == DialogResult.OK) {

                pdr = PDT.RowData;
                
                Itop.Client.Common.Services.BaseService.Create<glebeYearValue>(pdr);

                //datatable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pdr, datatable.NewRow()));
                ((DataTable)gridControl1.DataSource).Rows.Add(Itop.Common.DataConverter.ObjectToRow(pdr, datatable.NewRow()));
                //gridControl1.DataSource = datatable;
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null) {
                glebeYearValue PD = Itop.Common.DataConverter.RowToObject<glebeYearValue>(row);
                FrmgleYearEdit PDT = new FrmgleYearEdit();
                PDT.ParentObj = ParentObj;
                PDT.RowData = PD;
                if (PDT.ShowDialog() == DialogResult.OK) {

                    PD = PDT.RowData;
                    //if (PD.TDdatetime.Year != ParentObj.Year) {
                    //    Itop.Common.MsgBox.ShowYesNo("停电日期和年份不符！");
                    //    return;
                    //}
                    Itop.Client.Common.Services.BaseService.Update<PDrelcontent>(PD);
                    ParentID = ParentObj.UID;
                    //datatable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pdr, datatable.NewRow()));
                    //((DataTable)gridControl1.DataSource).Rows.Add(Itop.Common.DataConverter.ObjectToRow(pdr, datatable.NewRow()));
                    //gridControl1.DataSource = datatable;
                }
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null) {
                glebeYearValue dev = Itop.Common.DataConverter.RowToObject<glebeYearValue>(row);
                if (Itop.Common.MsgBox.ShowYesNo("是否确认删除?") == DialogResult.Yes) {
                    Itop.Client.Common.Services.BaseService.Delete<glebeYearValue>(dev);
                    ((DataTable)gridControl1.DataSource).Rows.Remove(row);
                }
            }
        }
    }
}
