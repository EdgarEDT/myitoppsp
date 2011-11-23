using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.HistoryValue;
using Itop.Client.Common;
using Itop.Domain.Stutistic;
using System.Collections;
using Dundas.Charting.WinControl;
using DevExpress.XtraGrid.Columns;
using System.IO;
using Itop.Common;
using Itop.Domain.Table;
namespace Itop.Client.Stutistics
{
    public partial class FrmBurdenMonth : Itop.Client.Base.FormModuleBase
    {
        private IList<PS_Table_AreaWH> AreaList = null;

        System.Data.DataTable dt = new System.Data.DataTable();
        DataTable dataTable = new DataTable();
        private string title = "";
        private string ModuleFlag = "FrmBurdenMonth";
        public string Title
        {
            get { return title; }
        }
        bool isSelect = false;
        DevExpress.XtraGrid.GridControl gcontrol = null;
        public bool IsSelect
        {
            set { isSelect = value; }
        }

        public DevExpress.XtraGrid.GridControl Gcontrol
        {
            get { return this.ctrlBurdenMonth1.GridControl; }
        }

        public FrmBurdenMonth()
        {
            InitializeComponent();
        }

        private void FrmBurdenMonth_Load(object sender, EventArgs e)
        {
            string pjt = " ProjectID='" + MIS.ProgUID + "'";
            AreaList = Common.Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", pjt);
           
            this.ctrlBurdenMonth1.FBM = this;
            this.ctrlBurdenMonth1.RefreshData();
            this.ctrlBurdenMonth1.GridView.GroupPanelText = smmprog.ProgName;

            barButtonItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

            if (isSelect)
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

            barButtonItem1.ImageIndex = 5;
            barButtonItem2.ImageIndex = 6;
            barButtonItem1.ImageIndex = 7;
           // barButtonItem3.ImageIndex = 6;
            barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonItem4.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonItem3.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            bar.InsertItem(bar.ItemLinks[4], barButtonItem1);
            bar.InsertItem(bar.ItemLinks[5], barButtonItem4);
            bar.InsertItem(bar.ItemLinks[6], barButtonItem2);
            //bar.InsertItem(bar.ItemLinks[6], barButtonItem3);

            InitChartData();

            UpdataChart();

        }
        private void InitChartData()
        {


            dataTable.Columns.Add("年度");
            dataTable.Columns.Add("AreaID");
            for (int i = 1; i <= 12; i++)
            {
                string str = i.ToString() + "月";
                dataTable.Columns.Add(str);
            }
        
        
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
        public void UpdataChart()
        {
            try
            {
                chart2.Series.Clear();
                //IList<BurdenMonth> list = Services.BaseService.GetStrongList<BurdenMonth>();
                IList<BurdenMonth> list = Services.BaseService.GetList<BurdenMonth>("SelectBurdenMonthByWhere", " uid like '%" + Itop.Client.MIS.ProgUID + "%'  order by BurdenYear");
				
                //dt = Itop.Common.DataConverter.ToDataTable((IList)list, typeof(BurdenMonth));

                dataTable.Rows.Clear();
                foreach (BurdenMonth mouth in list)
                {
                    DataRow dr = dataTable.NewRow();
                    dr["年度"] = mouth.BurdenYear.ToString();
                    dr["1月"] = mouth.Month1.ToString();
                    dr["2月"] = mouth.Month2.ToString();
                    dr["3月"] = mouth.Month3.ToString();
                    dr["4月"] = mouth.Month4.ToString();
                    dr["5月"] = mouth.Month5.ToString();
                    dr["6月"] = mouth.Month6.ToString();
                    dr["7月"] = mouth.Month7.ToString();
                    dr["8月"] = mouth.Month8.ToString();
                    dr["9月"] = mouth.Month9.ToString();
                    dr["10月"] = mouth.Month10.ToString();
                    dr["11月"] = mouth.Month11.ToString();
                    dr["12月"] = mouth.Month12.ToString();
                    dr["AreaID"] = mouth.AreaID.ToString();
                    dataTable.Rows.Add(dr);
                }


                foreach (DataRow row in dataTable.Rows)
                {
                    string seriesName = "";
                    try
                    {
                        // DateTime dateTime = (DateTime)row["年度"];
                        seriesName =FindArea(row["AreaID"].ToString())+"_"+ row["年度"].ToString() + "年";
                    }
                    catch { }
                    chart2.Series.Add(seriesName);
                    chart2.Series[seriesName].Type = SeriesChartType.Line;
                    chart2.Series[seriesName].BorderWidth = 1;

                    for (int colIndex = 1; colIndex < dataTable.Columns.Count; colIndex++)
                    {
                        if (dataTable.Columns[colIndex].ColumnName!="AreaID")
                        {
                            string columnName = dataTable.Columns[colIndex].ColumnName;
                            double YVal = Convert.ToDouble(row[columnName].ToString());

                            chart2.Series[seriesName].Points.AddXY(columnName, YVal);

                        }
                       
                    }
                }

            }
            catch (Exception ex) { Itop.Common.MsgBox.Show(ex.Message); }

            chart2.ResetAutoValues();

        }

        protected override void Add()
        {
            this.ctrlBurdenMonth1.AddObject();
            UpdataChart();
        }

        protected override void Edit()
        {
            this.ctrlBurdenMonth1.UpdateObject();
            UpdataChart();
        }

        protected override void Del()
        {
            this.ctrlBurdenMonth1.DeleteObject();
            UpdataChart();
        }

        protected override void Print()
        {
            this.ctrlBurdenMonth1.PrintPreview();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FileClass.ExportExcel(dataTable, this.ctrlBurdenMonth1.GridView.GroupPanelText, false, true, true);   
            FileClass.ExportExcel(this.ctrlBurdenMonth1.GridView.GroupPanelText, "", this.ctrlBurdenMonth1.GridControl);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            title = this.ctrlBurdenMonth1.GridView.GroupPanelText;
            this.DialogResult = DialogResult.OK;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSP_YearVisibleIndex yvi = new PSP_YearVisibleIndex();

            for (int i = 0; i < ctrlBurdenMonth1.GridView.Columns.Count; i++)
            {
                if (ctrlBurdenMonth1.GridView.Columns[i].VisibleIndex > 1)
                {
                    yvi.Year = ctrlBurdenMonth1.GridView.Columns[i].FieldName.ToString();
                    yvi.ModuleFlag = ModuleFlag;
                    yvi.VisibleIndex = ctrlBurdenMonth1.GridView.Columns[i].VisibleIndex;
                    Common.Services.BaseService.Update("UpdatePSP_YearVisibleIndexbyExists", yvi);
                }
            }
        }
        private DataTable GetExcel(string filepach)
        {
            string str;
            FarPoint.Win.Spread.FpSpread fpSpread1 = new FarPoint.Win.Spread.FpSpread();

            try
            {
                fpSpread1.OpenExcel(filepach);
            }
            catch
            {
                string filepath1 = Path.GetTempPath() + "\\" + Path.GetFileName(filepach);
                File.Copy(filepach, filepath1);
                fpSpread1.OpenExcel(filepath1);
                File.Delete(filepath1);
            }
            DataTable dt = new DataTable();
            //for (int k = 1; k <= fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++)
            //{

            //}
            foreach (GridColumn gc in ctrlBurdenMonth1.GridView.VisibleColumns)
                dt.Columns.Add(gc.Caption);

           
            for (int i = 3; i < fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data); i++)
            {
                DataRow dr = dt.NewRow();
                str = "";
                for (int j = 0; j <= fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data); j++)
                {
                    str = str + fpSpread1.Sheets[0].Cells[i, j].Text;
                    dr[j] = fpSpread1.Sheets[0].Cells[i, j].Text;
                }
                if (str != "")
                    dt.Rows.Add(dr);

            }
            return dt;
        }
         private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
         {
             try
             {
                 DataTable dts = new DataTable();
                 OpenFileDialog op = new OpenFileDialog();
                 op.Filter = "Excel文件(*.xls)|*.xls";
                 if (op.ShowDialog() == DialogResult.OK)
                 {
                     dts = GetExcel(op.FileName);
                     for (int k = 0; k < dts.Rows.Count; k++)
                     {
                         string year = dts.Rows[k][1].ToString();
                         //string da = dts.Rows[1][1].ToString();
                         //string jj = da.Substring(0, 2);
                         //string day = year + da.Substring(2, da.Length - 2).Replace("(", "").Replace(")", "").Replace("（", "").Replace("）", "");

                         BurdenMonth bl = new BurdenMonth();
                         bl.UID = bl.UID + "|" + Itop.Client.MIS.ProgUID;
                         bl.CreateDate = DateTime.Now;

                         bl.UpdateDate= DateTime.Now;
                         bl.BurdenYear =  int.Parse(year);
                         //try
                         //{
                         //    bl. = DateTime.Parse(year);
                         //}
                         //catch
                         //{
                         //    bl.BurdenDate = DateTime.Now;
                         //}
                         string pjt = " ProjectID='" + MIS.ProgUID + "'and Title='" + dts.Rows[k][0].ToString() + "'";
                         PS_Table_AreaWH lt = (PS_Table_AreaWH)Common.Services.BaseService.GetObject("SelectPS_Table_AreaWHByConn", pjt);
                         if (lt != null)
                         {
                             bl.AreaID = lt.ID;
                         }

                         bl.Title="BurdenYear ='" +bl.BurdenYear+"' and  uid like '%" + Itop.Client.MIS.ProgUID + "%' and AreaID='"+bl.AreaID+"'";
                         IList<BurdenMonth> lis = Common.Services.BaseService.GetList<BurdenMonth>("SelectBurdenMonthByWhere", bl.Title);

                         foreach (BurdenMonth bltemp in lis)
                         {

                             bl.UID = bltemp.UID;

                             bl.CreateDate = bltemp.CreateDate;

                             bl.UpdateDate = bltemp.UpdateDate;
                             break;
                            

                         }

                        
                        try{
                            bl.Month1 = double.Parse(dts.Rows[k][2].ToString());
                        }
                        catch { }
                        try{
                         bl.Month2 = double.Parse(dts.Rows[k][3].ToString());
                        }
                        catch { }
                        try{
                         bl.Month3 = double.Parse(dts.Rows[k][4].ToString());
                        }
                        catch { }
                        try{
                         bl.Month4 = double.Parse(dts.Rows[k][5].ToString());
                        }
                        catch { }
                        try{
                         bl.Month5 = double.Parse(dts.Rows[k][6].ToString());
                        }
                        catch { }
                        try{
                         bl.Month6 = double.Parse(dts.Rows[k][7].ToString());
                        }
                        catch { }
                        try{
                         bl.Month7 = double.Parse(dts.Rows[k][8].ToString());
                        }
                        catch { }
                        try{
                         bl.Month8 = double.Parse(dts.Rows[k][9].ToString());
                        }
                        catch { }
                        try{
                         bl.Month9 = double.Parse(dts.Rows[k][10].ToString());
                        }
                        catch { }
                        try{
                         bl.Month10 = double.Parse(dts.Rows[k][11].ToString());
                        }
                        catch { }
                        try{
                         bl.Month11 = double.Parse(dts.Rows[k][12].ToString());
                        }
                        catch { }
                        try{
                         bl.Month12 = double.Parse(dts.Rows[k][13].ToString());
                        }
                        catch { }
                    
                         


                         double minData = 0;
                         double maxData = 0;
                         double sumData = 0;

                     

                       

                         
                         Common.Services.BaseService.Delete<BurdenMonth>(bl);
                         Common.Services.BaseService.Create<BurdenMonth>(bl);
                         ctrlBurdenMonth1.ObjectList.Add(bl);

                     }
                     ctrlBurdenMonth1.RefreshData();
                     UpdataChart();
                     MsgBox.Show("已导入成功！");
                 }
             }
             catch { MsgBox.Show("导入格式不正确！"); }
         }
    }
}