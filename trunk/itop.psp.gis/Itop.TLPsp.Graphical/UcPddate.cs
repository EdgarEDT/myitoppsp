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
                    RefreshData(" where ParentID='" + value + "' order by TDdatetime");
                }
            }
        }
        private void RefreshData(string con)
        {
            AddFixColumn();
            IList<PDrelcontent> pl = Itop.Client.Common.Services.BaseService.GetListByWhere<PDrelcontent>(con);
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
    }
}
