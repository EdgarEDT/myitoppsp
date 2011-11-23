using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Common;
using Itop.Client.Chen;
using System.Collections;
using Itop.Client.Base;

namespace Itop.Client.Stutistics
{
    public partial class FrmBigUserCurrentMonthPrint : FormBase
    {
        private string title = "";
        private bool isselect = false;
        private Hashtable hash =null;
        private Hashtable hash2 = null;
        private DataCommon dcsort = new DataCommon();
        private bool IsPrint = false;
        public string Title
        {
            get { return title; }
        }


        public bool IsSelect
        {
            set { isselect = value; }
        }
        public bool ISPrint
        {
            set { IsPrint = value; }
        }
        public  Hashtable HASH
        {
            set { hash = value; }
        }
        public Hashtable HASH2
        {
            set { hash2 = value; }
        }




        private DataTable gridDataTable;
        public DataTable GridDataTable
        {
            get { return gridDataTable; }
            set { gridDataTable = value; }
        }

        public FrmBigUserCurrentMonthPrint()
        {
            InitializeComponent();
        }

        private void FrmBigUserCurrentMonthPrint_Load(object sender, EventArgs e)
        {
            if (!isselect)
            {
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }
            if (!IsPrint)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }


            //DataTable dt1 = new DataTable();
            //dt1.Columns.Add("Title");
            //dt1.Columns.Add("Title1");
            //dt1.Columns.Add("Title2");
            //foreach (DataColumn dc in GridDataTable.Columns)
            //{
            //    if (dc.ColumnName.IndexOf("年") > 0)
            //        dt1.Columns.Add(dc.ColumnName, typeof(double));
            //}
            ////GridDataTable = dcsort.GetSortTable(GridDataTable, "ID", true);

            ////GridDataTable = dcsort.GetSortTable(GridDataTable, "ID,Flag", true);

            //string title="";
            //string title1 = "";
            //foreach (DataRow row1 in GridDataTable.Rows)
            //{
            //    if (!hash.ContainsValue(row1["Title"]) &&!hash2.ContainsValue(row1["Title"]))
            //    {
            //        title = row1["Title"].ToString(); ;
            //        continue;
            //    }
            //    if (hash.ContainsValue(row1["Title"]))
            //    {
            //         title1 = row1["Title"].ToString(); ;
            //        continue;
            //    }
            //    if (hash2.ContainsValue(row1["Title"]))
            //    {
            //        DataRow rowtemp1 = dt1.NewRow();
            //        rowtemp1["Title"] = title;
            //        rowtemp1["Title1"] = title1;
            //        addrowtodt1(row1, rowtemp1, ref dt1);
            //    }
                //if (hash2.ContainsValue(row1["Title"]))
                //{
                //    string title = row1["Title"].ToString(); ;
                //    continue;
                //}
                //DataRow rowtemp = dt1.NewRow();
                //addrowtodt1(row1,rowtemp, ref dt1);

                //DataRow[] drtemp = GridDataTable.Select("ParentID=" + row1["ID"].ToString());
                //foreach (DataRow dr in drtemp)
                //{
                //    if (!hash.ContainsValue(dr["Title"]))
                //        continue;
                //    DataRow rowtemp1 = dt1.NewRow();
                //    rowtemp1["Title"] = row1["Title"].ToString();
                //    addrowtodt1(dr, rowtemp1, ref dt1);
                //    DataRow[] drtemp2 = GridDataTable.Select("ParentID=" + dr["ID"].ToString());
                   
                //    foreach (DataRow dr2 in drtemp2)
                //    {
                //        if (!hash2.ContainsValue(dr2["Title"]))
                //            continue;
                //        DataRow rowtemp2 = dt1.NewRow();
                //        rowtemp2["Title"] = row1["Title"].ToString();
                //        rowtemp2["Title1"] = dr["Title"].ToString();
                //        addrowtodt1(dr2,rowtemp2,ref dt1);
                    
                //    }
                //}
            //}
            //string title1 = "";
            //foreach (DataRow row1 in GridDataTable.Rows)
            //{
            //    if (row1["Title"].ToString().Substring(0, 2) != "　　")
            //    {
            //        title1 = row1["Title"].ToString();
            //        continue;
            //    }
            //    DataRow row = dt1.NewRow();
            //    row["Title"] = title1;
            //    row["Title1"] = row1["Title"].ToString().Replace("　", "");
            //    foreach (DataColumn dc in GridDataTable.Columns)
            //    {
            //        if (dc.ColumnName.IndexOf("年") > 0)
            //        {
            //            try
            //            {
            //                row[dc.ColumnName] = Convert.ToDouble(row1[dc.ColumnName].ToString());
            //            }
            //            catch { row[dc.ColumnName] = 0; }
            //        }
            //    }
            //    dt1.Rows.Add(row);

            //}


            

            //int numi = 0;

            //DevExpress.XtraGrid.Views.BandedGrid.GridBand[] gridBandDate = new DevExpress.XtraGrid.Views.BandedGrid.GridBand[listCount * 2];
            //DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] gridColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[listCount * 2];
            //foreach (DataColumn dc in GridDataTable.Columns)
            //{
            //    if (dc.ColumnName.IndexOf("月") > 0)
            //    {
            //        //DevExpress.XtraGrid.Views.BandedGrid.GridBand gbi = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            //        //gbi.Caption = dc.ColumnName.Replace("年", "");
            //        //gbi.Name = dc.ColumnName;
            //        //gbi.Width = 75;
            //        //gbi.AppearanceHeader.Options.UseTextOptions = true;
            //        //gbi.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //        //gridBand7.Children.Add(gbi);

            //        DevExpress.XtraGrid.Columns.GridColumn gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            //        gridColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //        gridColumn.Caption = dc.ColumnName;
            //        gridColumn.FieldName = dc.ColumnName;
            //        gridColumn.Name = "Column"+dc.ColumnName;
            //        gridColumn.Visible = true;
            //        gridColumn.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            //        //gridColumn.Width = 89;
            //        //gridColumn.DisplayFormat.FormatString = "n2";
            //        //gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //        //this.gridView1.Columns.Add(gridColumn);

            //        gridColumn.VisibleIndex = 100;
            //        this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn });




            //        //gbi.Columns.Add(gridColumn);

            //    }



            //}
            //gridControl1.DataSource = dt1;
            gridControl1.DataSource = GridDataTable;
            this.gridView1.GroupPanelText = this.Text;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            



        }
        private void addrowtodt1(DataRow dr, DataRow row, ref DataTable dt1)
        {
            //bool IScontiue = true;
            //if (Ishash1)
            //{
            //    if (!hash.ContainsValue(dr["Title"]))
            //        IScontiue=false;
            //}
            //else
            //{
            //    if (!hash2.ContainsValue(dr["Title"]))
            //        IScontiue = false;
            //}
            //if (IScontiue)
            //{
               

                //row["Title1"] = row1["Title"].ToString().Replace("　", "");
            foreach (DataColumn dc in GridDataTable.Columns)
                {
                    if (dc.ColumnName.IndexOf("年") > 0)
                    {
                        try
                        {
                            row[dc.ColumnName] = Convert.ToDouble(dr[dc.ColumnName].ToString());
                        }
                        catch { row[dc.ColumnName] = 0; }
                    }
                    else
                    {

                        if (hash.ContainsValue(dr[dc.ColumnName]))
                        {
                            row["Title1"] = dr[dc.ColumnName].ToString();
                            continue;
                        }
                        else

                            if (hash2.ContainsValue(dr[dc.ColumnName]))
                            {
                                row["Title2"] = dr[dc.ColumnName].ToString();
                                continue;
                            }
                            else
                                 if (dc.ColumnName=="Title")
                          {
                                row["Title"] = dr[dc.ColumnName].ToString();
                                continue;
                          }
                            
                    }
                }
                dt1.Rows.Add(row);
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
            this.Close();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FileClass.ExportExcel(this.gridControl1);
            FileClass.ExportExcel(this.gridView1.GroupPanelText, "单位:万千瓦时 ", this.gridControl1);
        }

      
    }
}