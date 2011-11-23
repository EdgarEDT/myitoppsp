using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Stutistic;

namespace Itop.Client.Stutistics
{
    public partial class FrmBurdenLineForecast : Itop.Client.Base.FormModuleBase
    {
        public FrmBurdenLineForecast()
        {
            InitializeComponent();
        }

        string title = "";
        string unit = "";
        bool isSelect = false;

        DevExpress.XtraGrid.GridControl gcontrol = null;
        DataTable dataTable = new DataTable();

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

        private void FrmBurdenLineForecast_Load(object sender, EventArgs e)
        {
            dataTable.Columns.Add("A", typeof(string));
            dataTable.Columns.Add("B", typeof(bool));


            barBurdenLineForecast.ImageIndex = 5;

            barBurdenLineForecast.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            bar.InsertItem(bar.ItemLinks[4], barBurdenLineForecast);

            this.ctrlBurdenLineForecast1.RefreshData();
            this.ctrlBurdenLineForecast1.GridView.GroupPanelText = this.Text;

            if (!EditRight)
                this.ctrlBurdenLineForecast1.AllowUpdate = false;

            
        }

        protected override void Add()
        {
            this.ctrlBurdenLineForecast1.AddObject();
        }

        protected override void Edit()
        {
            this.ctrlBurdenLineForecast1.UpdateObject();
        }

        protected override void Del()
        {
            this.ctrlBurdenLineForecast1.DeleteObject();
        }

        protected override void Print()
        {
            this.ctrlBurdenLineForecast1.PrintPreview();
        }

        private void barBurdenLineForecast_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IList<BurdenLineForecast> li1 = this.ctrlBurdenLineForecast1.ObjectList;


            dataTable.Rows.Clear();
            foreach (BurdenLineForecast bl in li1)
            {
                DataRow newRow = dataTable.NewRow();
                newRow["A"] = bl.BurdenYear;
                newRow["B"] = false;
                dataTable.Rows.Add(newRow);
            }

            FormChooseYears cy = new FormChooseYears();
            cy.DT = dataTable;
            if (cy.ShowDialog() != DialogResult.OK)
                return;


            





            FrmBurdenLineForecastType frm = new FrmBurdenLineForecastType();
            frm.DT = cy.DT;

            frm.IsSelect = isSelect;
            if (!PrintRight)
                frm.PRINTManage = false;

            if (frm.ShowDialog() == DialogResult.OK && isSelect)
            {
                gcontrol = frm.gridControl1;
                title = frm.Title;
                unit = "µ¥Î»£ºÍòÇ§Íß";
                DialogResult = DialogResult.OK;
            }
        }
    }
}