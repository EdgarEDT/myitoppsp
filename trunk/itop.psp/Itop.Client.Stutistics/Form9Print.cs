using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Common;
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
    public partial class Form9Print : FormBase
    {
        private string title = "";
        private bool isselect = false;

        public string Title
        {
            get { return title; }
        }


        public bool IsSelect
        {
            set { isselect = value; }
        }





        private DataTable gridDataTable;
        public DataTable GridDataTable
        {
            get { return gridDataTable; }
            set { gridDataTable = value; }
        }

        public Form9Print()
        {
            InitializeComponent();
        }

        private void Form1Print_Load(object sender, EventArgs e)
        {
            if (!isselect)
            {
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }

            DevExpress.XtraGrid.Columns.GridColumn gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();

            DevExpress.XtraGrid.Columns.GridColumn gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            DevExpress.XtraGrid.Columns.GridColumn gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            DevExpress.XtraGrid.Columns.GridColumn gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            
            this.gridView1.GroupPanelText = this.Text;

            //int numi = 0;

            //DevExpress.XtraGrid.Views.BandedGrid.GridBand[] gridBandDate = new DevExpress.XtraGrid.Views.BandedGrid.GridBand[listCount * 2];
            //DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] gridColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[listCount * 2];
            int i = 1;
            foreach (DataColumn dc in GridDataTable.Columns)
            {
                //if (dc.ColumnName.IndexOf("年") > 0)
                //{

                    //DevExpress.XtraGrid.Views.BandedGrid.GridBand gbi = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                    //gbi.Caption = dc.ColumnName;
                    //gbi.Name = dc.ColumnName;
                    //gbi.AppearanceHeader.Options.UseTextOptions = true;
                    //gbi.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    //gridView1.Columns.Add(gbi);
                    //gridBand2.Children.Add(gbi);


                    //this.bandedGridColumn1.AppearanceHeader.Options.UseTextOptions = true;
                    //this.bandedGridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    //this.bandedGridColumn1.Caption = "项目";
                    //this.bandedGridColumn1.FieldName = "Title";
                    //this.bandedGridColumn1.Name = "bandedGridColumn1";
                    //this.bandedGridColumn1.Visible = true;
                    //this.bandedGridColumn1.VisibleIndex = 0;



                //if (dc.ColumnName == "Code" || dc.ColumnName == "ID" || dc.ColumnName == "Title" || dc.ColumnName == "Flag2" || dc.ColumnName == "Flag")//; || dc.ColumnName == "Remark")
                //    continue;

                //if (dc.ColumnName.IndexOf("Photo")>=0)
                //{
                //    gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
                //    gridColumn.Caption = dc.ColumnName.Replace("Photo","");
                //    gridColumn.FieldName = dc.ColumnName;
                //    gridColumn.Name = "Column" + dc.ColumnName;
                //    gridColumn.Visible = true;
                //    gridColumn.VisibleIndex = i;
                //    gridColumn.ColumnEdit = this.repositoryItemMemoEdit1;
                //    gridView1.Columns.Add(gridColumn);
                //}


                if (dc.ColumnName == "StartYear")
                {
                    gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
                    gridColumn1.FieldName = dc.ColumnName;
                    gridColumn1.Name = "Column" + dc.ColumnName;
                    gridColumn1.ColumnEdit = this.repositoryItemMemoEdit1;
                    gridColumn1.VisibleIndex = 1000;
                    gridColumn1.Caption = "计划开工时间";
                    gridView1.Columns.Add(gridColumn1);
                }

                else if (dc.ColumnName == "EndYear")
                {
                    gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
                    gridColumn2.FieldName = dc.ColumnName;
                    gridColumn2.Name = "Column" + dc.ColumnName;
                    gridColumn2.ColumnEdit = this.repositoryItemMemoEdit1;
                    gridColumn2.VisibleIndex = 1001;
                    gridColumn2.Caption = "预计投产时间";
                    gridView1.Columns.Add(gridColumn2);
                }


                else if (dc.ColumnName == "Remark")
                {
                    gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
                    gridColumn3.FieldName = dc.ColumnName;
                    gridColumn3.Name = "Column" + dc.ColumnName;
                    gridColumn3.ColumnEdit = this.repositoryItemMemoEdit1;
                    gridColumn3.VisibleIndex = 1002;
                    gridColumn3.Caption = "备注";
                    gridView1.Columns.Add(gridColumn3);
                }

                else if (dc.ColumnName == "Title" || dc.ColumnName == "ID" || dc.ColumnName == "Code" || dc.ColumnName == "Flag" || dc.ColumnName == "Flag2" || dc.ColumnName == "CreateTime" || dc.ColumnName == "UpdateTime" || dc.ColumnName == "L1" || dc.ColumnName == "L2" || dc.ColumnName == "L3" || dc.ColumnName == "L4" || dc.ColumnName == "L5" || dc.ColumnName == "L6" || dc.ColumnName == "IsConn")
                {

                }

                else
                {
                    gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
                    gridColumn.FieldName = dc.ColumnName;
                    gridColumn.Name = "Column" + dc.ColumnName;
                    gridColumn.ColumnEdit = this.repositoryItemMemoEdit1;
                    gridColumn.VisibleIndex = i;
                    gridColumn.Caption = dc.ColumnName;
                    gridView1.Columns.Add(gridColumn);
                
                
                }
                i++;

                ////////////    gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
                ////////////    gridColumn.Caption = dc.ColumnName;
                ////////////    gridColumn.FieldName = dc.ColumnName;
                ////////////    gridColumn.Name = "Column"+dc.ColumnName;
                ////////////    gridColumn.Visible = true;
                ////////////    gridColumn.VisibleIndex = i;
                ////////////    gridColumn.ColumnEdit = this.repositoryItemMemoEdit1;
                ////////////    if (dc.ColumnName == "Remark")
                ////////////    {
                ////////////        gridColumn.VisibleIndex = 100;
                ////////////        gridColumn.Caption = "备注";
                ////////////    }
                ////////////    //gridColumn.DisplayFormat.FormatString = "n2";
                ////////////    //gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                ////////////    //bandedGridView1.Columns.Add(gridColumn);
                ////////////    gridView1.Columns.Add(gridColumn);
                ////////////    i++;
                //}
            }

            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 1000;
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 1001;
            gridColumn3.Visible = true;
            gridColumn3.VisibleIndex = 1002;
            gridControl1.DataSource = GridDataTable;

            //if (!isselect)
            //{
            //    //barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //    this.Hide();
            //    ComponentPrint.ShowPreview(this.gridControl1, this.bandedGridView1.GroupPanelText);
            //    this.Close();
            //}
            //else
            //{
            //    title = this.bandedGridView1.GroupPanelText;
            //    this.DialogResult = DialogResult.OK;
            //}



        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ComponentPrint.ShowPreview(this.gridControl1, this.gridView1.GroupPanelText);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            title = this.gridView1.GroupPanelText;
            this.DialogResult = DialogResult.OK;
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FileClass.ExportExcel(this.gridControl1);
        }
    }
}