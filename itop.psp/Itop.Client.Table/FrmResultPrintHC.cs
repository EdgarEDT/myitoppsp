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
    public partial class FrmResultPrintHC : FormBase
    {
        private string title = "";
        //private bool isselect = false;

        public string Title
        {
            get { return title; }
        }


        //public bool IsSelect
        //{
        //    set { isselect = value; }
        //}
        //private bool btzgs = false;
        //public bool bTzgs
        //{
        //    set { btzgs = value; }
        //}

        //public bool IsBand
        //{
        //    set { isband = value; }
        //}
        //private bool isband = true;

        private DataTable gridDataTable;
        public DataTable GridDataTable
        {
            get { return gridDataTable; }
            set { gridDataTable = value; }
        }

        //private IList<ChoosedYears> yearList = new List<ChoosedYears>();

        //public IList<ChoosedYears> YearList
        //{
        //    set { yearList = value; }
        //}
        //IList<string> yearList1;
        //public IList<string> YearList1
        //{
        //    set { yearList1 = value; }
        //}

        public FrmResultPrintHC()
        {
            InitializeComponent();
        }
        //private bool bBuild = false;
        //public bool BBuild
        //{
        //    set { bBuild = value; }
        //}
        private void Form1Print_Load(object sender, EventArgs e)
        {

                bandedGridView1.Columns[0].FieldName = "BianInfo";
                bandedGridView1.Columns[0].Caption = "序号";
                bandedGridView1.Columns[0].Width = 80;
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
                gridColumn15.Caption = "项目状态";
                gridColumn15.FieldName = "Flag";
                gridColumn15.Name = "Flag";
                gridColumn15.Visible = true;
                gridColumn15.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                gridColumn15.Width = 90;
                gridColumn15.VisibleIndex = 2;
                bande15.Columns.Add(gridColumn15);

                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                gridColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridColumn.Caption = "长度";
                gridColumn.FieldName = "Length";
                gridColumn.Name = "Length";
                gridColumn.Visible = true;
                gridColumn.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                gridColumn.Width = 90;
                gridColumn.VisibleIndex = 4;
                
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridColumn1.Caption = "容量";
                gridColumn1.FieldName = "Volumn";
                gridColumn1.Name = "Volumn";
                gridColumn1.Visible = true;
                gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                gridColumn1.Width = 90;
                gridColumn1.VisibleIndex = 4;
                DevExpress.XtraGrid.Views.BandedGrid.GridBand band4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridColumn4.Caption = "总投资";
                gridColumn4.FieldName = "AllVolumn";
                gridColumn4.Name = "AllVolumn";
                gridColumn4.DisplayFormat.FormatString = "n2";
                gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridColumn4.Visible = true;
                gridColumn4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                gridColumn4.Width = 90;
                gridColumn4.VisibleIndex = 5;
                band4.Columns.Add(gridColumn4);
                band4.Columns["AllVolumn"].DisplayFormat.FormatString = "n2";


                DevExpress.XtraGrid.Views.BandedGrid.GridBand band8 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn8 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridColumn8.Caption = "负责人";
                gridColumn8.FieldName = "Col2";
                gridColumn8.Name = "Col2";
                gridColumn8.Visible = true;
                gridColumn8.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                gridColumn8.Width = 90;
                gridColumn8.VisibleIndex = 8;
                band8.Columns.Add(gridColumn8);

                DevExpress.XtraGrid.Views.BandedGrid.GridBand band9 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn9 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridColumn9.Caption = "线路/变电站";
                gridColumn9.FieldName = "Col4";
                gridColumn9.Name = "Col4";
                gridColumn9.Visible = true;
                gridColumn9.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                gridColumn9.Width = 130;
                gridColumn9.VisibleIndex = 9;
                band9.Columns.Add(gridColumn9);

                DevExpress.XtraGrid.Views.BandedGrid.GridBand band10 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn10 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                gridColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridColumn10.Caption = "城区";
                gridColumn10.FieldName = "AreaName";
                gridColumn10.Name = "AreaName";
                gridColumn10.Visible = true;
                gridColumn10.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                gridColumn10.Width = 90;
                gridColumn10.VisibleIndex = 10;
                band10.Columns.Add(gridColumn10);

                DevExpress.XtraGrid.Views.BandedGrid.GridBand band11 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn11 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                gridColumn11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridColumn11.Caption = "电压";
                gridColumn11.FieldName = "FromID";
                gridColumn11.Name = "FromID";
                gridColumn11.Visible = true;
                gridColumn11.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                gridColumn11.Width = 90;
                gridColumn11.VisibleIndex = 11;
                band11.Columns.Add(gridColumn11);



                DevExpress.XtraGrid.Views.BandedGrid.GridBand band7 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridColumn7.Caption ="备注";
                gridColumn7.FieldName = "Col1";
                gridColumn7.Name = "Col1";
                gridColumn7.Visible = true;
                gridColumn7.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                gridColumn7.Width = 150;
                gridColumn7.VisibleIndex = 20;
                band7.Columns.Add(gridColumn7);

                band.Columns.Add(gridColumn);
                band.Columns.Add(gridColumn1);
                
                this.bandedGridView1.Bands.Add(bandd);
                this.bandedGridView1.Bands.Add(bande);
                this.bandedGridView1.Bands.Add(bande14);
                this.bandedGridView1.Bands.Add(band);
                this.bandedGridView1.Bands.Add(band8);
                this.bandedGridView1.Bands.Add(band9);
                this.bandedGridView1.Bands.Add(band10);
                this.bandedGridView1.Bands.Add(band11);
                this.bandedGridView1.Bands.Add(band4);
                this.bandedGridView1.Bands.Add(bande15);

                this.bandedGridView1.Bands.Add(band7);
               

                this.bandedGridView1.Columns.Add(gridColumn);
                this.bandedGridView1.Columns.Add(gridColumn1);
                this.bandedGridView1.Columns.Add(gridColumn2);
                this.bandedGridView1.Columns.Add(gridColumn3);
                this.bandedGridView1.Columns.Add(gridColumn4);
                //this.bandedGridView1.Columns.Add(gridColumn5);
                this.bandedGridView1.Columns.Add(gridColumn11);
                this.bandedGridView1.Columns.Add(gridColumn14);
                this.bandedGridView1.Columns.Add(gridColumn15);
                this.bandedGridView1.Columns.Add(gridColumn8);
                this.bandedGridView1.Columns.Add(gridColumn9);
                this.bandedGridView1.Columns.Add(gridColumn10);

                this.bandedGridView1.Columns.Add(gridColumn7);
               
            
                gridControl1.DataSource = gridDataTable;
           
            
            
            this.bandedGridView1.GroupPanelText = this.Text;
           
            



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