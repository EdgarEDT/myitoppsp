using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
using Itop.Client.Base;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
namespace Itop.TLPSP.DEVICE
{
    public partial class FrmAttachtable : DevExpress.XtraEditors.XtraForm
    {
        public FrmAttachtable()
        {
            InitializeComponent();
        }
        public DataTable datatable = new DataTable();
        private string parentID;
        public string ParentID
        {
            get { return parentID; }
            set
            {
                parentID = value;
                if (!string.IsNullOrEmpty(value))
                {
                    RefreshData("RelatetableID='" + value + "' order by startYear");
                }
            }
        }
        //PSP_Substation_Info parentObj = new PSP_Substation_Info();
        //public PSP_Substation_Info ParentObj
        //{
        //    get { return parentObj; }
        //    set
        //    {

        //        parentObj = value;
        //        if (value == null)
        //        {
        //            parentID = null;
        //        }
        //        else
        //        {
        //            ParentID = value.UID;
        //        }
        //    }
        //}
        public string Type = "1";
        private void RefreshData(string con)
        {
            if (datatable != null)
            {
                datatable.Columns.Clear();
                gridView1.Columns.Clear();
            }
            AddFixColumn();
            IList<Psp_Attachtable> pl = Itop.Client.Common.Services.BaseService.GetList<Psp_Attachtable>("SelectPsp_AttachtableByCont", con);
            datatable = Itop.Common.DataConverter.ToDataTable((IList)pl, typeof(Psp_Attachtable));
            gridControl1.DataSource = datatable;
        }
        private void AddFixColumn()
        {

            GridColumn column = new GridColumn();
            column.FieldName = "ID";
            column.VisibleIndex = -1;
            this.gridView1.Columns.Add(column);

            column = new GridColumn();
            column.FieldName = "S1";
            if (Type=="1")
            {
                column.Caption = "变压器名称";
            }
            else
            {
                column.Caption = "机组名称";
            }
            column.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            column.VisibleIndex = 0;
            column.OptionsColumn.AllowEdit = false;
            column.Width = 120;
            this.gridView1.Columns.Add(column);
            column = new GridColumn();
            column.FieldName = "ZHI";
            column.Caption = "容量";
            column.VisibleIndex = 1;
            column.OptionsColumn.AllowEdit = false;
            column.Width = 100;
            this.gridView1.Columns.Add(column);
            column = new GridColumn();
            column.FieldName = "S3";
            column.Caption = "新建时间";
            column.VisibleIndex = 2;
            column.OptionsColumn.AllowEdit = false;
            column.Width = 100;
            this.gridView1.Columns.Add(column);
            column = new GridColumn();
            column.FieldName = "startYear";
            column.Caption = "投产时间";
            column.VisibleIndex = 2;
            column.OptionsColumn.AllowEdit = false;
            column.Width =100;
            this.gridView1.Columns.Add(column);
            column = new GridColumn();
            column.FieldName = "endYear";
            column.Caption = "退出时间";
            column.VisibleIndex = 3;
            column.OptionsColumn.AllowEdit = false;
            column.Width = 120;
            this.gridView1.Columns.Add(column);
            if (Type!="1")
            {
                column = new GridColumn();
                column.FieldName = "D1";
                column.Caption = "丰水期出力率";
                column.VisibleIndex = 4;
                column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                column.DisplayFormat.FormatString = "n3";
                column.Width = 120;
                column.OptionsColumn.AllowEdit = false;
                this.gridView1.Columns.Add(column);
                column = new GridColumn();
                column.FieldName = "D2";
                column.Caption = "枯水期出力率";
                column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                column.DisplayFormat.FormatString = "n3";
                column.VisibleIndex = 5;
                column.Width = 120;
                column.OptionsColumn.AllowEdit = false;
                this.gridView1.Columns.Add(column);
            }
            column = new GridColumn();
            column.FieldName = "S2";
            column.Caption = "状态";
            column.VisibleIndex =-1;
            column.Width = 120;
            column.OptionsColumn.AllowEdit = false;
            this.gridView1.Columns.Add(column);

        }
        private string startyear;
        public string StartYear
        {
            get
            {
                return startyear;
            }
            set{
                startyear = value;
            }
        }
        private string endyear;
        public string EndYear
        {
            get { return endyear; }
            set { endyear = value; }
        }
        private string relatetable;
        public string RelateTable
        {
            get { return relatetable; }
            set { relatetable = value; }
        }
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Psp_AttachtableEdit PDT = new Psp_AttachtableEdit();
            if (string.IsNullOrEmpty(parentID))
            {
                Itop.Common.MsgBox.ShowYesNo("变电站或电源为空！");
                return;
            }
            // PDT.parentobj = ParentObj;
            Psp_Attachtable pdr = new Psp_Attachtable();
            pdr.Relatetable = relatetable;
            pdr.RelatetableID = parentID;
            pdr.startYear = startyear;
            pdr.endYear = endyear;
            pdr.D1 = 1;
            pdr.D2 = 1;
            pdr.S2 = "新建";
            PDT.SateType = "新建";
            pdr.S3 = DateTime.Now.Year.ToString();
            PDT.type = Type;
            PDT.RowData = pdr;
            if (PDT.ShowDialog() == DialogResult.OK)
            {

                pdr = PDT.RowData;
                
                Itop.Client.Common.Services.BaseService.Create<Psp_Attachtable>(pdr);

                //datatable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pdr, datatable.NewRow()));
                ((DataTable)gridControl1.DataSource).Rows.Add(Itop.Common.DataConverter.ObjectToRow(pdr, datatable.NewRow()));
                //gridControl1.DataSource = datatable;
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
             DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
             if (row != null) {
                 Psp_Attachtable PD = Itop.Common.DataConverter.RowToObject<Psp_Attachtable>(row);
                 Psp_AttachtableEdit PDT = new Psp_AttachtableEdit();
                 PDT.SateType = "修改";
                 PDT.type = Type;
                 PDT.RowData =PD;
                 if (PDT.ShowDialog() == DialogResult.OK)
                 {

                    
                     Itop.Client.Common.Services.BaseService.Update<Psp_Attachtable>(PDT.RowData);
                     datatable.Rows.Remove(row);
                     ((DataTable)gridControl1.DataSource).Rows.Add(Itop.Common.DataConverter.ObjectToRow(PDT.RowData, datatable.NewRow()));
                     //datatable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pdr, datatable.NewRow()));
                     

                     //gridControl1.DataSource = datatable;
                 }
             }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                row["S2"] = "退出";
            }
        }

        private void barButtonItem4_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                Psp_Attachtable PD = Itop.Common.DataConverter.RowToObject<Psp_Attachtable>(row);
                Psp_Attachtable pr = new Psp_Attachtable();
                Itop.Common.DataConverter.CopyTo<Psp_Attachtable>(PD,pr);
                pr.ID =Guid.NewGuid().ToString();
                pr.S2 = "扩容";
                PD.S2 = "作废";
                Psp_AttachtableEdit PDT = new Psp_AttachtableEdit();
                PDT.SateType = "扩容";
                PDT.type = Type;
                PDT.RowData = pr;
                if (PDT.ShowDialog() == DialogResult.OK)
                {

                    pr = PDT.RowData;

                    Itop.Client.Common.Services.BaseService.Create<Psp_Attachtable>(pr);
                    Itop.Client.Common.Services.BaseService.Update<Psp_Attachtable>(pr);
                    //datatable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pdr, datatable.NewRow()));
                    ((DataTable)gridControl1.DataSource).Rows.Add(Itop.Common.DataConverter.ObjectToRow(pr, datatable.NewRow()));
                    
                    //gridControl1.DataSource = datatable;
                }
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                Psp_Attachtable dev = Itop.Common.DataConverter.RowToObject<Psp_Attachtable>(row);
                if (Itop.Common.MsgBox.ShowYesNo("是否确认删除?") == DialogResult.Yes)
                {
                    Itop.Client.Common.Services.BaseService.Delete<Psp_Attachtable>(dev);
                    ((DataTable)gridControl1.DataSource).Rows.Remove(row);
                }
            }
        }


    }
}