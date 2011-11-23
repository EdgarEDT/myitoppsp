using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Stutistic;
using Itop.Client.Common;
using Itop.Common;
using Itop.Domain.Table;
using Dundas.Charting.WinControl;

namespace Itop.Client.Stutistics
{
    public partial class FrmBurdenMaxYear1 : Itop.Client.Base.FormBase
    {
        private IList<PS_Table_AreaWH> AreaList = null;
        string title = "";
        string unit = "";
        bool isSelect = false;
        public bool print = true;
        DevExpress.XtraGrid.GridControl gcontrol = null;

        public string Title
        {
            get { return title; }
        }

        public string Unit
        {
            get { return unit; }
        }

        public DevExpress.XtraGrid.GridControl Gcontrol
        {
            get { return gcontrol; }
        }

        public bool IsSelect
        {
            set { isSelect = value; }
        }




        IList<BurdenYear> li = new List<BurdenYear>();




        public IList<BurdenYear> ObjList
        {
            set { li = value; }
        }



        public FrmBurdenMaxYear1()
        {
            InitializeComponent();
        }

        private void FrmBurdenMaxYear_Load(object sender, EventArgs e)
        {
            string pjt = " ProjectID='" + MIS.ProgUID + "'";
            AreaList = Common.Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", pjt);
           
            bandedGridView1.GroupPanelText = this.Text;

            if (isSelect)
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
           


            DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand1.Caption = "时间";
            gridBand1.Width = 100;
            gridBand1.Name = "gridBand1";
            gridBand1.AppearanceHeader.Options.UseTextOptions = true;
            gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridView1.Bands.Add(gridBand1);

            ////DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            ////gridBand2.Caption = "时间";
            ////gridBand2.Name = "gridBand2";
            ////gridBand2.AppearanceHeader.Options.UseTextOptions = true;
            ////gridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            ////gridBand1.Children.Add(gridBand2);

            DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridColumn1.Caption = "时间";
            gridColumn1.FieldName = "s" ;
            gridColumn1.Name = "gridColumn1";
            gridColumn1.Visible = true;
            gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandedGridView1.Columns.Add(gridColumn1);
            gridBand1.Columns.Add(gridColumn1);
            



            DataTable dt = new DataTable();
            dt.Columns.Add("s", typeof(string));




            int listCount = li.Count;


            int numi = 0;
            int numj = 0;
            int numk = 0;

            DevExpress.XtraGrid.Views.BandedGrid.GridBand[] gridBand = new DevExpress.XtraGrid.Views.BandedGrid.GridBand[listCount];
            DevExpress.XtraGrid.Views.BandedGrid.GridBand[] gridBandDate = new DevExpress.XtraGrid.Views.BandedGrid.GridBand[listCount];
            DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] gridColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[listCount];


            foreach (BurdenYear bys in li)
            {
                gridBand[numi] = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                gridBand[numi].Name = "gridBand" + bys.BurdenYears.ToString();
                if (bys.BurdenYears<10000)
                { gridBand[numi].Caption = FindArea(bys.AreaID)+"_"+bys.BurdenYears.ToString() + "年"; }
                else
                { gridBand[numi].Caption = "增长率"; }
                gridBand[numi].Width = 100;
                gridBand[numi].AppearanceHeader.Options.UseTextOptions = true;
                gridBand[numi].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                this.bandedGridView1.Bands.Add(gridBand[numi]);

                gridBandDate[numj] = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                //gridBandDate[numj].Name = "gridBand" + bys.BurdenDate.ToString("yyyyMMdd");
                if (bys.BurdenYears < 10000)
                { gridBandDate[numj].Caption = bys.BurdenDate.ToString("MM月dd日");
                gridBand[numi].Children.Add(gridBandDate[numj]);
                }
                else
                { gridBand[numj].Caption = "增长率"; }
                gridBandDate[numj].AppearanceHeader.Options.UseTextOptions = true;
                gridBandDate[numj].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                //gridBand[numi].Children.Add(gridBandDate[numj]);

                gridColumn[numk] = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                gridColumn[numk].FieldName = "Year" + bys.BurdenYears.ToString();
                gridColumn[numk].Caption = "Year" + bys.BurdenYears.ToString();
                gridColumn[numk].Name = "Year" + bys.BurdenYears.ToString();
                gridColumn[numk].Visible = true;
                gridColumn[numk].Width = 120;
                gridColumn[numk].AppearanceCell.Options.UseTextOptions = true;
                gridColumn[numk].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                if (bys.BurdenYears > 10000)
                {
                    gridColumn[numk].DisplayFormat.FormatString = "p2";
                    gridColumn[numk].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                    gridBand[numi].Columns.Add(gridColumn[numk]);
                }
                else
                {
                    gridBandDate[numj].Columns.Add(gridColumn[numk]);
                }

                bandedGridView1.Columns.Add(gridColumn[numk]);
                //gridBand[numi].Columns.Add(gridColumn[numk]);
                numi++;
                numj++;
                numk++;



                //if(bys.BurdenYears!=99999)
                dt.Columns.Add("Year" + bys.BurdenYears.ToString(), typeof(double));
            }

            DataRow row = dt.NewRow();
            row["s"] = "负荷";
            foreach (BurdenYear bys1 in li)
            {
                row["Year" + bys1.BurdenYears.ToString()] = bys1.Values;
            }
            dt.Rows.Add(row);

            gridControl1.DataSource = dt;



            chart1.Series.Clear();


            string seriesName = "最大负荷";
            chart1.Series.Add(seriesName);
            chart1.Series[seriesName].Type = SeriesChartType.Line;
            chart1.Series[seriesName].BorderWidth = 2;

            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.ColumnName == "s")
                    continue;

                try { if (int.Parse(dc.ColumnName.Replace("Year", "")) > 10000)continue; }
                catch { }

                string coname = dc.ColumnName.Replace("Year", "");

                double YVal = 0;
                try
                {
                    YVal = (double)dt.Rows[0][dc.ColumnName];
                }
                catch { }
                chart1.Series[seriesName].Points.AddXY(coname, YVal);
            }
            if(!print)
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }



        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ComponentPrint.ShowPreview(this.gridControl1, this.bandedGridView1.GroupPanelText);
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //FileClass.ExportExcel(this.gridControl1);
                FileClass.ExportExcel(this.bandedGridView1.GroupPanelText, "", this.gridControl1);
            }
            catch { }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            title = this.bandedGridView1.GroupPanelText;
            gcontrol = this.gridControl1;
            this.DialogResult = DialogResult.OK;
        }
        //根据areaid返回Areaname
        private string FindArea(string Areaid)
        {
            string value = "无地区";
            if (AreaList.Count > 0)
            {
                for (int i = 0; i < AreaList.Count; i++)
                {
                    if (AreaList[i].ID == Areaid)
                    {
                        value = AreaList[i].Title.ToString();
                        break;
                    }
                }
            }
            return value;
        }


    }
}