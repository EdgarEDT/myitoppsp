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
namespace Itop.TLPsp.Graphical {
    public partial class UcPddate : DevExpress.XtraEditors.XtraUserControl {
        public UcPddate() {
            InitializeComponent();
        }
        private DataTable datatable = new DataTable();
        private string parentID;
        public string ParentID {
            get { return parentID; }
            set {
                parentID = value;
                if (!string.IsNullOrEmpty(value)) {
                    RefreshData("ParentID='" + value + "' order by TDdatetime");
                }
            }
        }
        PDrelregion parentObj = new PDrelregion();
        public PDrelregion ParentObj {
            get { return parentObj; }
            set {

                parentObj = value;
                if (value == null) {
                    parentID = null;
                } else {
                    ParentID = value.ID;
                }
            }
        }
        private void RefreshData(string con)
        {
            if (datatable != null) {
                datatable.Columns.Clear();
                gridView1.Columns.Clear();
            }
            AddFixColumn();
            IList<PDrelcontent> pl = Itop.Client.Common.Services.BaseService.GetList<PDrelcontent>("SelectPDrelcontentByWhere",con);
            datatable = Itop.Common.DataConverter.ToDataTable((IList)pl, typeof(PDrelcontent));
            gridControl1.DataSource = datatable;
        }
          private void AddFixColumn()
        {

            GridColumn column = new GridColumn();
            column.FieldName = "ID";
            column.VisibleIndex = -1;  
            this.gridView1.Columns.Add(column);

            column = new GridColumn();
            column.FieldName = "TDdatetime";
            column.Caption = "停电日期";
            column.VisibleIndex = 0;
            column.Width = 120;
            this.gridView1.Columns.Add(column);
            column = new GridColumn();
            column.FieldName = "TDtime";
            column.Caption = "停电持续时间";
            column.VisibleIndex = 1;
            column.Width = 100;
            this.gridView1.Columns.Add(column);
            column = new GridColumn();
            column.FieldName = "PeopleRegion";
            column.Caption = "用户范围";
            column.VisibleIndex = 2;
            column.Width = 70;
            this.gridView1.Columns.Add(column);
            column = new GridColumn();
            column.FieldName = "TDtype";
            column.Caption = "停电类型";
            column.VisibleIndex = 3;
            column.Width = 120;
            this.gridView1.Columns.Add(column);
            column = new GridColumn();
            column.FieldName = "AvgFH";
            column.Caption = "平均符合（KW）";
            column.VisibleIndex = 4;
            column.Width = 120;
            this.gridView1.Columns.Add(column);
        }
        
          private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
              PdDateEdit PDT = new PdDateEdit();
              PDT.parentobj = ParentObj;
              PDrelcontent pdr = new PDrelcontent();
              pdr.ParentID = ParentObj.ID;
              PDT.RowData = pdr;
              if (PDT.ShowDialog() == DialogResult.OK) {

                  pdr = PDT.RowData;
                  Itop.Client.Common.Services.BaseService.Create<PDrelcontent>(pdr);
                  
                  //datatable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pdr, datatable.NewRow()));
                  ((DataTable)gridControl1.DataSource).Rows.Add(Itop.Common.DataConverter.ObjectToRow(pdr, datatable.NewRow()));
                  //gridControl1.DataSource = datatable;
              }
          }

          private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
              if (row != null) {
                  PDrelcontent PD = Itop.Common.DataConverter.RowToObject<PDrelcontent>(row);
                  PdDateEdit PDT = new PdDateEdit();
                  PDT.parentobj = ParentObj;
                  PDT.RowData = PD;
                  if (PDT.ShowDialog() == DialogResult.OK) {

                     PD = PDT.RowData;
                      Itop.Client.Common.Services.BaseService.Update<PDrelcontent>(PD);
                      parentID = ParentObj.ID;
                      //datatable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pdr, datatable.NewRow()));
                      //((DataTable)gridControl1.DataSource).Rows.Add(Itop.Common.DataConverter.ObjectToRow(pdr, datatable.NewRow()));
                      //gridControl1.DataSource = datatable;
                  }
              }
          }
  

          private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
              DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
              if (row != null) {
                  PDrelcontent dev = Itop.Common.DataConverter.RowToObject<PDrelcontent>(row);
                  if (Itop.Common.MsgBox.ShowYesNo("是否确认删除?") == DialogResult.Yes) {
                      Itop.Client.Common.Services.BaseService.Delete<PDrelcontent>(dev);
                      ((DataTable)gridControl1.DataSource).Rows.Remove(row);
                  }
              }
          }


    }
}
