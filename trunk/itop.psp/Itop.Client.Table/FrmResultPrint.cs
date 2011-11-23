using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Common;
using Itop.Client.Chen;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmResultPrint : FormBase
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
        private bool btzgs = false;
        public bool bTzgs
        {
            set { btzgs = value; }
        }

        public bool IsBand
        {
            set { isband = value; }
        }
        private bool isband = true;

        private DataTable gridDataTable;
        public DataTable GridDataTable
        {
            get { return gridDataTable; }
            set { gridDataTable = value; }
        }

        private IList<ChoosedYears> yearList = new List<ChoosedYears>();

        public IList<ChoosedYears> YearList
        {
            set { yearList = value; }
        }
        IList<string> yearList1;
        public IList<string> YearList1
        {
            set { yearList1 = value; }
        }

        public FrmResultPrint()
        {
            InitializeComponent();
        }
        private bool bBuild = false;
        public bool BBuild
        {
            set { bBuild = value; }
        }
        private void Form1Print_Load(object sender, EventArgs e)
        {
            if (!isselect)
            {
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }


            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Title");
            dt1.Columns.Add("Title1");

            foreach (DataColumn dc in GridDataTable.Columns)
            {
                if (dc.ColumnName.IndexOf("年") > 0)
                    dt1.Columns.Add(dc.ColumnName, typeof(double));
            }


            string title1="";
            string first = "";
            foreach (DataRow row1 in GridDataTable.Rows)
            {
                try
                {
                    if (row1["Title"].ToString().Substring(0, 2) != "　　")
                    {
                        title1 = row1["Title"].ToString();
                        continue;
                    }
                }
                catch
                {
                    title1 = row1["Title"].ToString();
                    continue;
                }

                DataRow row = dt1.NewRow();
                if (first == title1)
                    row["Title"] = "";
                else
                {
                    row["Title"] = title1;
                    first = title1;
                }
                row["Title1"] = row1["Title"].ToString().Replace("　","");
                foreach (DataColumn dc in GridDataTable.Columns)
                {
                    if (dc.ColumnName.IndexOf("年") > 0)
                    {
                        if (row1[dc.ColumnName] == null || row1[dc.ColumnName] == DBNull.Value)
                            row[dc.ColumnName] = 0;
                        else
                            row[dc.ColumnName] = Convert.ToDouble(row1[dc.ColumnName].ToString());
                      
                    }
                }
                dt1.Rows.Add(row);

            }

            if (isband)
            {
                foreach (ChoosedYears year in yearList)
                {
                    DevExpress.XtraGrid.Views.BandedGrid.GridBand band = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                    band.Caption = year.Year.ToString() + "年";
                    band.Name = "gridBand" + year.Year.ToString();
                    band.AppearanceHeader.Options.UseTextOptions = true;
                    band.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                    gridColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gridColumn.Caption = "丰";
                    gridColumn.FieldName = year.Year.ToString() + "年丰";
                    gridColumn.Name = "Column" + year.Year.ToString();
                    gridColumn.Visible = true;
                    gridColumn.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                    gridColumn.Width = 50;
                    gridColumn.VisibleIndex = year.Year;

                    DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                    gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gridColumn1.Caption = "枯";
                    gridColumn1.FieldName = year.Year.ToString() + "年枯";
                    gridColumn1.Name = "Column" + year.Year.ToString();
                    gridColumn1.Visible = true;
                    gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                    gridColumn1.Width = 50;
                    gridColumn1.VisibleIndex = year.Year;
                    band.Columns.Add(gridColumn);
                    band.Columns.Add(gridColumn1);
                    this.bandedGridView1.Bands.Add(band);
                    this.bandedGridView1.Columns.Add(gridColumn);
                    this.bandedGridView1.Columns.Add(gridColumn1);
                }
                gridControl1.DataSource = dt1;
            }
            else if (btzgs)
            {
                bandedGridView1.Columns[0].FieldName = "FromID";
                bandedGridView1.Columns[0].Caption = "序号";
                bandedGridView1.Columns[0].Width = 50;
                bandedGridView1.Columns[1].FieldName = "Title";

                DevExpress.XtraGrid.Views.BandedGrid.GridBand bandd = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridColumn2.Caption = "开工年限";
                gridColumn2.FieldName = "BuildYear";
                gridColumn2.Name = "BuildYear";
                gridColumn2.Visible = true;
                gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                gridColumn2.Width = 100;
                gridColumn2.VisibleIndex = 2;
                bandd.Columns.Add(gridColumn2);

                DevExpress.XtraGrid.Views.BandedGrid.GridBand bande = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridColumn3.Caption = "竣工年限";
                gridColumn3.FieldName = "BuildEd";
                gridColumn3.Name = "BuildEd";
                gridColumn3.Visible = true;
                gridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                gridColumn3.Width = 100;
                gridColumn3.VisibleIndex = 3;
                bande.Columns.Add(gridColumn3);

                DevExpress.XtraGrid.Views.BandedGrid.GridBand band = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                band.Caption = "建设规模";
                band.Name = "gridBandG";
                band.AppearanceHeader.Options.UseTextOptions = true;
                band.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                
                DevExpress.XtraGrid.Views.BandedGrid.GridBand bande14 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn14 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                gridColumn14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridColumn14.Caption = "建设性质";
                gridColumn14.FieldName = "Col3";
                gridColumn14.Name = "Col3";
                gridColumn14.Visible = true;
                gridColumn14.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                gridColumn14.Width = 100;
                gridColumn14.VisibleIndex = 1;
                bande14.Columns.Add(gridColumn14);
                


                DevExpress.XtraGrid.Views.BandedGrid.GridBand bande15 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn15 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                gridColumn15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridColumn15.Caption = "导线型号";
                gridColumn15.FieldName = "Linetyp";
                gridColumn15.Name = "Linetyp";
                gridColumn15.Visible = true;
                gridColumn15.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                gridColumn15.Width = 100;
                gridColumn15.VisibleIndex = 2;
                bande15.Columns.Add(gridColumn15);

                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                gridColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridColumn.Caption = "长度";
                gridColumn.FieldName = "Length";
                gridColumn.Name = "Length";
                gridColumn.Visible = true;
                gridColumn.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                gridColumn.Width = 100;
                gridColumn.VisibleIndex = 4;
                
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridColumn1.Caption = "容量";
                gridColumn1.FieldName = "Volumn";
                gridColumn1.Name = "Volumn";
                gridColumn1.Visible = true;
                gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                gridColumn1.Width = 100;
                gridColumn1.VisibleIndex = 4;
                DevExpress.XtraGrid.Views.BandedGrid.GridBand band4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridColumn4.Caption = "总投资";
                gridColumn4.FieldName = "AllVolumn";
                gridColumn4.Name = "AllVolumn";
                gridColumn4.DisplayFormat.FormatString = " #####################0.##";
                gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridColumn4.Visible = true;
                gridColumn4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                gridColumn4.Width = 80;
                gridColumn4.VisibleIndex = 5;
                band4.Columns.Add(gridColumn4);
                band4.Columns["AllVolumn"].DisplayFormat.FormatString = " #####################0.##";

                DevExpress.XtraGrid.Views.BandedGrid.GridBand band5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridColumn5.Caption = Convert.ToString(int.Parse(yearList1[0]) - 1) + "年底前投资";
                gridColumn5.FieldName = "BefVolumn";
                gridColumn5.Name = "BefVolumn";
                gridColumn5.DisplayFormat.FormatString = " #####################0.##";
                gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridColumn5.Visible = true;
                gridColumn5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                gridColumn5.Width = 150;
                gridColumn5.VisibleIndex = 6;
                band5.Columns.Add(gridColumn5);

                DevExpress.XtraGrid.Views.BandedGrid.GridBand band1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                band1.Caption = bBuild?"":yearList1[0] + "年～" + yearList1[4] + "年投资";
                band1.Name = "gridBandA";
                band1.AppearanceHeader.Options.UseTextOptions = true;
                band1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumny = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                gridColumny.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridColumny.Caption = "合计";
                gridColumny.FieldName = "AftVolumn";
                gridColumny.Name = "AftVolumn";
                gridColumny.DisplayFormat.FormatString = " #####################0.##";
                gridColumny.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridColumny.Visible = true;
                gridColumny.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                gridColumny.Width = 80;
                gridColumny.VisibleIndex =7;
                if(!bBuild)
                    band1.Columns.Add(gridColumny);
                foreach (string y in yearList1)
                {
                    DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnx = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                    gridColumnx.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gridColumnx.Caption = y+"年";
                    gridColumnx.FieldName = "y"+y;
                    gridColumnx.Name = "y" + y;
                    gridColumnx.Visible = true;
                    gridColumnx.DisplayFormat.FormatString = " #####################0.##";
                    gridColumnx.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gridColumnx.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                    gridColumnx.Width = 80;
                    gridColumnx.VisibleIndex = int.Parse(y);
                    band1.Columns.Add(gridColumnx);
                    this.bandedGridView1.Columns.Add(gridColumnx);
                }

                DevExpress.XtraGrid.Views.BandedGrid.GridBand band7 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridColumn7.Caption ="备注";
                gridColumn7.FieldName = "Col1";
                gridColumn7.Name = "Col1";
                gridColumn7.Visible = true;
                gridColumn7.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                gridColumn7.Width = 150;
                gridColumn7.VisibleIndex = 8;
                band7.Columns.Add(gridColumn7);
                //if(!Line)
                //    band.Columns.Add(gridColumn);
                //else
                //    band.Columns.Add(gridColumn1);

                if (!Line)
                {
                    band.Columns.Add(gridColumn);
                    band.Columns.Add(gridColumn1);
                }
                this.bandedGridView1.Bands.Add(bandd);
                this.bandedGridView1.Bands.Add(bande);
                if(!bBuild)
                    //this.bandedGridView1.Bands.Add(band);
                if (!bBuild)
                {
                    this.bandedGridView1.Bands.Add(band4);
                    this.bandedGridView1.Bands.Add(band5);
                }
                else
                {
                    this.bandedGridView1.Bands.Add(bande14);
                    if(!Line)
                        this.bandedGridView1.Bands.Add(bande15);
                }
                this.bandedGridView1.Bands.Add(band1);
                this.bandedGridView1.Bands.Add(band7);
                this.bandedGridView1.Columns.Add(gridColumn);
                this.bandedGridView1.Columns.Add(gridColumn1);
                this.bandedGridView1.Columns.Add(gridColumn2);
                this.bandedGridView1.Columns.Add(gridColumn3);
                this.bandedGridView1.Columns.Add(gridColumn4);
                this.bandedGridView1.Columns.Add(gridColumn5);
                this.bandedGridView1.Columns.Add(gridColumn14);
                this.bandedGridView1.Columns.Add(gridColumn15);

                this.bandedGridView1.Columns.Add(gridColumn7);
                this.bandedGridView1.Columns.Add(gridColumny);
                gridControl1.DataSource = gridDataTable;
            }
            else
            {

                int numi = 0;

                //DevExpress.XtraGrid.Views.BandedGrid.GridBand[] gridBandDate = new DevExpress.XtraGrid.Views.BandedGrid.GridBand[listCount * 2];
                // DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] gridColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[listCount * 2];
                foreach (DataColumn dc in GridDataTable.Columns)
                {
                    if (dc.ColumnName.IndexOf("年") > 0)
                    {
                        //DevExpress.XtraGrid.Views.BandedGrid.GridBand gbi = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                        //gbi.Caption = dc.ColumnName.Replace("年", "");
                        //gbi.Name = dc.ColumnName;
                        //gbi.Width = 75;
                        //gbi.AppearanceHeader.Options.UseTextOptions = true;
                        //gbi.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        //gridBand7.Children.Add(gbi);
                        DevExpress.XtraGrid.Views.BandedGrid.GridBand band = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();

                        DevExpress.XtraGrid.Columns.GridColumn gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
                        gridColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gridColumn.Caption = dc.ColumnName;
                        gridColumn.FieldName = "yf";
                        gridColumn.Name = "Column" + dc.ColumnName;
                        gridColumn.Visible = true;
                        gridColumn.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                        gridColumn.Width = 89;
                        //gridColumn.DisplayFormat.FormatString = " #####################0.##";
                        //gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        //this.gridView1.Columns.Add(gridColumn);

                        gridColumn.VisibleIndex = 100;
                        this.bandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn });




                        //gbi.Columns.Add(gridColumn);

                    }

                    gridControl1.DataSource = GridDataTable;

                }
            }
            
            this.bandedGridView1.GroupPanelText = this.Text;
            this.bandedGridView1.OptionsView.ColumnAutoWidth = false;
            



        }
        private bool line = false,build=false;
        public bool Line
        {
            get { return line; }
            set { line = value; }
        }
        public bool Build
        {
            get { return build; }
            set { build = value; }
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
            ComponentPrint.ShowPreview(this.gridControl1, this.bandedGridView1.GroupPanelText);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            title = this.bandedGridView1.GroupPanelText;
            this.DialogResult = DialogResult.OK;
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        private bool bHe = true;
        public bool BHe
        {
            set { bHe = value; }
        }
        private string title1 = "", dw1 = "";
        public string Title1
        {
            set { title1 = value; }
        }
        public string Dw1
        {
            set { dw1 = value; }
        }
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           // FileClass.ExportExcel(this.gridControl1);
            ExportExcel.ExportToExcelOld(this.gridControl1,this.Text,dw1);
        }
    }
}