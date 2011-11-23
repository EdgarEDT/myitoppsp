using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Stutistic;
using System.IO;
using System.Reflection;
using Itop.Common;
using Itop.Domain.HistoryValue;
using Itop.Client.Common;
using System.Collections;
using Dundas.Charting.WinControl;
using DevExpress.XtraGrid.Columns;
using Itop.Domain.Table;
namespace Itop.Client.Stutistics
{
    public partial class FrmBurdenLine : Itop.Client.Base.FormModuleBase
    {
        string title = "";
        string unit = "";
        bool isSelect = false;
        string ModuleFlag = "BurdenLine";
        DevExpress.XtraGrid.GridControl gcontrol = null;
        private IList<PS_Table_AreaWH> AreaList=null;
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


        public FrmBurdenLine()
        {
            InitializeComponent();

        }

        private void FrmBurdenLine_Load(object sender, EventArgs e)
        {
             string pjt = " ProjectID='" + MIS.ProgUID + "'";
            AreaList = Common.Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", pjt);
           
            barAdd.ImageIndex = 3;
            barEdit.ImageIndex = 4;
            barDel.ImageIndex = 5;
            barPrint.ImageIndex = 6;
            barClose.ImageIndex =7;

            barBurdenLine.ImageIndex = 2;
            barMaxBurden.ImageIndex = 1;
            barButtonItem2.ImageIndex =0;
            barButtonItem7.ImageIndex=0;
          //  barButtonItem6.ImageIndex = 0;
   
            barBurdenLine.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barMaxBurden.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonItem6.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonItem7.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

            bar.InsertItem(bar.ItemLinks[4], barBurdenLine);
            bar.InsertItem(bar.ItemLinks[5], barMaxBurden);
            bar.InsertItem(bar.ItemLinks[6], barButtonItem2);
            bar.InsertItem(bar.ItemLinks[7], barButtonItem7);



            this.ctrlBurdenLine1.FBL = this;
         //   this.ctrlBurdenLine1.ColumnsVisibleIndex(ModuleFlag);//传递模块标志
            this.ctrlBurdenLine1.RefreshData();

            if (!EditRight)
            {
                this.ctrlBurdenLine1.AllowUpdate = false;
                this.ctrlBurdenLine1.editright = false;
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            refeshchart();
        }

        void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void Add()
        {
            
            this.ctrlBurdenLine1.AddObject();
            refeshchart();
        }

        protected override void Edit()
        {
           
            this.ctrlBurdenLine1.UpdateObject();
            refeshchart();
        }

        protected override void Del()
        {
            chart1.Series.Clear();
            this.ctrlBurdenLine1.DeleteObject();
            refeshchart();
        }

        protected override void Print()
        {        
            this.ctrlBurdenLine1.PrintPreview();   
        }

        private void barBurdenLine_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmBurdenLineType frm = new FrmBurdenLineType();

            frm.IsSelect = isSelect;
            if (!PrintRight)
                frm.PRINTManage = false;

            if (frm.ShowDialog() == DialogResult.OK && isSelect)
            {
                gcontrol = frm.gridControl1;
                title = frm.Title;
                unit = "单位：万千瓦";
                DialogResult = DialogResult.OK;
            }


            //frm.ShowDialog();
        }

        private void barMaxBurden_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmBurdenLineMaxDate frm = new FrmBurdenLineMaxDate();
            //frm.ShowDialog();
            if (!PrintRight)
                frm.PRINTManage = false;
            frm.IsSelect = isSelect;

            if (frm.ShowDialog() == DialogResult.OK && isSelect)
            {
                gcontrol = frm.gridControl1;
                title = frm.Title;
                unit = "单位：万千瓦";
                DialogResult = DialogResult.OK;
            }
        }
    

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            

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
                string filepath1=Path.GetTempPath() + "\\"+ Path.GetFileName(filepach);
                File.Copy(filepach,filepath1) ;
                fpSpread1.OpenExcel(filepath1);
                File.Delete(filepath1);
            }
            DataTable dt = new DataTable();
            //for (int k = 1; k <= fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++)
            //{
               
            //}
            foreach (GridColumn gc in ctrlBurdenLine1.GridView.VisibleColumns)
                dt.Columns.Add(gc.Caption);

            //dt.Columns.RemoveAt(27);
            //dt.Columns.RemoveAt(26);
            for (int i = 3; i < fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data) ; i++)
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

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

                        BurdenLine bl = new BurdenLine();
                        bl.UID = bl.UID + "|" + Itop.Client.MIS.ProgUID;
                     try{
                        bl.BurdenDate = DateTime.Parse(year);
                     }catch{
                         bl.BurdenDate = DateTime.Now;
                     }
                     bl.UID = "Datename(year,BurdenDate)='" + bl.BurdenDate.Year
                         + "' and (Datename (month,BurdenDate)='" + bl.BurdenDate.Month + "'or Datename (month,BurdenDate)='0" + bl.BurdenDate.Month
                         + "') and (Datename(day,BurdenDate) ='" + bl.BurdenDate.Day + " ' or Datename(day,BurdenDate) ='0" + bl.BurdenDate.Day
                         + "') and  uid like '%" + Itop.Client.MIS.ProgUID + "%'  ";
                     IList<BurdenLine> lis = Common.Services.BaseService.GetList<BurdenLine>("SelectBurdenLineByWhere", bl.UID);
                     bool flag = false;
                     foreach (BurdenLine bltemp in lis)
                     {

                         flag = true;
                         bl.UID = bltemp.UID;
                         break;

                         
                         //else
                         //{
                         //    if (bl.BurdenDate.Year < 1900)
                         //        bl.BurdenDate = DateTime.Now;
                         //}

                     }
                        if (!flag)
                        {
                            bl.UID = Guid.NewGuid().ToString() + "|" + Itop.Client.MIS.ProgUID;
                        }

                     string pjt = " ProjectID='" + MIS.ProgUID + "'and Title='" + dts.Rows[k][0].ToString() + "'";
                     PS_Table_AreaWH lt = (PS_Table_AreaWH)Common.Services.BaseService.GetObject("SelectPS_Table_AreaWHByConn", pjt);
                     if (lt != null)
                     {
                         bl.AreaID = lt.ID;
                     }
                       
                        if (dts.Rows[k][2].ToString().Length>1)
                        bl.Season = dts.Rows[k][2].ToString().Substring(0, 2);
                        try{
                            bl.Hour1 = double.Parse(dts.Rows[k][3].ToString());
                        }
                        catch{}
                        try{
                        bl.Hour2 = double.Parse(dts.Rows[k][4].ToString());
                        }
                        catch{}
                        try{
                        bl.Hour3 = double.Parse(dts.Rows[k][5].ToString());
                        }
                        catch{}
                        try{
                        bl.Hour4 = double.Parse(dts.Rows[k][6].ToString());
                        }
                        catch{}
                        try{
                        bl.Hour5 = double.Parse(dts.Rows[k][7].ToString());
                       
                        }
                        catch{}
                        try{
                         bl.Hour6 = double.Parse(dts.Rows[k][8].ToString());
                        }
                        catch{}
                        try{
                        bl.Hour7 = double.Parse(dts.Rows[k][9].ToString());
                        }
                        catch{}
                        try{
                        bl.Hour8 = double.Parse(dts.Rows[k][10].ToString());
                        }
                        catch{}
                        try{
                        bl.Hour9 = double.Parse(dts.Rows[k][11].ToString());
                        }
                        
                        catch{}
                        try{
                        bl.Hour10 = double.Parse(dts.Rows[k][12].ToString());
                        }
                        catch{}
                        try{
                        bl.Hour11 = double.Parse(dts.Rows[k][13].ToString());
                        }
                        
                        catch{}
                        try{
                        bl.Hour12 = double.Parse(dts.Rows[k][14].ToString());
                        }
                        catch{}
                        try{
                        bl.Hour13 = double.Parse(dts.Rows[k][15].ToString());
                        }
                      
                        catch{}
                        try{
                        bl.Hour14 = double.Parse(dts.Rows[k][16].ToString());
                        }
                        catch{}
                        try{
                        bl.Hour15 = double.Parse(dts.Rows[k][17].ToString());
                        }
                        catch{}
                        try{
                        bl.Hour16 = double.Parse(dts.Rows[k][18].ToString());
                        }
                        catch{}
                        try{
                        bl.Hour17 = double.Parse(dts.Rows[k][19].ToString());
                        }
                        catch{}
                        try{
                        bl.Hour18 = double.Parse(dts.Rows[k][20].ToString());
                        }
                        catch{}
                        try{
                        bl.Hour19 = double.Parse(dts.Rows[k][21].ToString());
                        }
                        catch{}
                        try{
                        bl.Hour20 = double.Parse(dts.Rows[k][22].ToString());
                        }
                        catch { }
                        try
                        {
                        bl.Hour21 = double.Parse(dts.Rows[k][23].ToString());
                        }
                        catch{}
                        try{
                        bl.Hour22 = double.Parse(dts.Rows[k][24].ToString());
                        }
                        catch{}
                        try{
                        bl.Hour23 = double.Parse(dts.Rows[k][25].ToString());
                        }
                        catch{}
                        try{
                        bl.Hour24 = double.Parse(dts.Rows[k][26].ToString());
                        }
                        catch{}
                        try
                        {
                            if (dts.Rows[k][27].ToString()=="选中")
	                        {
                                bl.IsType = true;
	                        }
                            else
                            {
                                bl.IsType = false;
                            }
                        }
                        catch 
                        {
                        }
                        try
                        {
                            if (dts.Rows[k][28].ToString() == "选中")
                            {
                                bl.IsMaxDate = true;
                            }
                            else
                            {
                                bl.IsMaxDate = false;
                            }
                        }
                        catch
                        {
                        }


                        double minData = 0;
                        double maxData = 0;
                        double sumData = 0;

                        for (int i = 1; i <= 24; i++)
                        {
                            PropertyInfo pi = bl.GetType().GetProperty("Hour" + i.ToString());
                            if (i == 1)
                            {
                                minData = (double)pi.GetValue(bl, null);
                                maxData = (double)pi.GetValue(bl, null);
                                sumData = (double)pi.GetValue(bl, null);
                            }
                            else
                            {
                                minData = Math.Min((double)pi.GetValue(bl, null), minData);
                                maxData = Math.Max((double)pi.GetValue(bl, null), maxData);
                                sumData += (double)pi.GetValue(bl, null);
                            }
                        }

                        bl.DayAverage = sumData / (24 * maxData);
                        bl.MinAverage = minData / maxData;

                       // bl.IsType = true;
                        Common.Services.BaseService.Delete<BurdenLine>(bl);
                        Common.Services.BaseService.Create<BurdenLine>(bl);
                        ctrlBurdenLine1.ObjectList.Add(bl);
                        
                    }
                    ctrlBurdenLine1.RefreshData();
                    refeshchart();
                    MsgBox.Show("已成功导入数据 ！");
                }
            }
            catch { MsgBox.Show("导入格式不正确！"); }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //PSP_YearVisibleIndex yvi = new PSP_YearVisibleIndex();

            //for (int i = 0; i < ctrlBurdenLine1.GridView.Columns.Count; i++)
            //{
            //    if (ctrlBurdenLine1.GridView.Columns[i].VisibleIndex > 1)
            //    {
            //        yvi.Year = ctrlBurdenLine1.GridView.Columns[i].FieldName.ToString();
            //        yvi.ModuleFlag = ModuleFlag;
            //        yvi.VisibleIndex = ctrlBurdenLine1.GridView.Columns[i].VisibleIndex;
            //        Common.Services.BaseService.Update("UpdatePSP_YearVisibleIndexbyExists", yvi);
            //    }
            //}
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //PSP_YearVisibleIndex yvi = new PSP_YearVisibleIndex();

            //for (int i = 0; i < ctrlBurdenLine1.GridView.Columns.Count; i++)
            //{
            //    if (ctrlBurdenLine1.GridView.Columns[i].VisibleIndex > 1)
            //    {
            //        yvi.Year = ctrlBurdenLine1.GridView.Columns[i].FieldName.ToString();
            //        yvi.ModuleFlag = ModuleFlag;
            //        yvi.VisibleIndex = ctrlBurdenLine1.GridView.Columns[i].VisibleIndex;
            //        Common.Services.BaseService.Update("UpdatePSP_YearVisibleIndexbyExists", yvi);
            //    }
            //}
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSP_YearVisibleIndex yvi = new PSP_YearVisibleIndex();

            for (int i = 0; i < ctrlBurdenLine1.GridView.Columns.Count; i++)
            {
                if (ctrlBurdenLine1.GridView.Columns[i].VisibleIndex > 1)
                {
                    yvi.Year = ctrlBurdenLine1.GridView.Columns[i].FieldName.ToString();
                    yvi.ModuleFlag = ModuleFlag;
                    yvi.VisibleIndex = ctrlBurdenLine1.GridView.Columns[i].VisibleIndex;
                    Common.Services.BaseService.Update("UpdatePSP_YearVisibleIndexbyExists", yvi);
                }
            }
        }
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FileClass.ExportToExcelOld(ctrlBurdenLine1.GridView.GroupPanelText, "", ctrlBurdenLine1.GridControl);
        }
        //根据areaid返回Areaname
        private string FindArea(string Areaid)
        {
            string value = "无地区";
            if (AreaList.Count>0)
            {
                for (int i = 0; i < AreaList.Count; i++)
                {
                    if (AreaList[i].ID==Areaid)
                    {
                        value= AreaList[i].Title.ToString(); 
                        break;
                    }
                }
            }
            return value;
        }
        public void refeshchart2()
        {
            

            chart1.Series.Clear();
            Hashtable hatemp = new Hashtable();
            DataTable dt = new DataTable();
            DataTable dttemp = new DataTable();
            //IList<BurdenLine> list = Services.BaseService.GetList<BurdenLine>("SelectBurdenLineByMaxDate", null);
            IList<BurdenLine> list = Services.BaseService.GetList<BurdenLine>("SelectBurdenLineByWhere", " uid like '%" + Itop.Client.MIS.ProgUID + "%'  order by BurdenDate");      
                
            dt = Itop.Common.DataConverter.ToDataTable((IList)list, typeof(BurdenLine));
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn dc in dt.Columns)
                    if (dc.ColumnName == "BurdenDate")
                        hatemp.Add(dr["BurdenDate"].ToString(), "(最大负荷日)");
                      
            }
            list = Services.BaseService.GetList<BurdenLine>("SelectBurdenLineByType", null);
            dttemp = Itop.Common.DataConverter.ToDataTable((IList)list, typeof(BurdenLine));
            foreach (DataRow dr in dttemp.Rows)
            {
                foreach (DataColumn dc in dttemp.Columns)
                    if (dc.ColumnName == "BurdenDate" && !hatemp.ContainsKey(dr["BurdenDate"].ToString()))
                        hatemp.Add(dr["BurdenDate"].ToString(), "(典型负荷日)");
                        
            }
            foreach (DataRow dr in dttemp.Rows)
            {
                DataRow dtnew = dt.NewRow();
                foreach (DataColumn dc in dttemp.Columns)
                {
                    dtnew[dc.ColumnName] = dr[dc.ColumnName];
                }
                dt.Rows.Add(dtnew);
            
            }
            list.Clear();
            foreach (DataRow row in dt.Rows)
            {
                string seriesName = "";
                try
                {
                    string strtemp = "";
                    if (hatemp[row["BurdenDate"].ToString()]!=null) 
                        strtemp = hatemp[row["BurdenDate"].ToString()].ToString();
                    DateTime dateTime = (DateTime)row["BurdenDate"];
                    seriesName = FindArea(row["AreaID"].ToString()) + "_" + dateTime.ToString("yyyy年MM月dd日") + strtemp;
                }
                catch { }
                try
                {
                    chart1.Series.Add(seriesName);

                    chart1.Series[seriesName].Type = SeriesChartType.Line;
                    chart1.Series[seriesName].BorderWidth = 2;
                    //chart1.Series[seriesName].ShowLabelAsValue = false;
                    for (int colIndex = 3; colIndex < 26; colIndex++)
                    {
                        string columnName = dt.Columns[colIndex].ColumnName;
                        double YVal = (double)row[columnName];
                        try
                        {
                           
                            chart1.Series[seriesName].Points.AddXY(GetText(columnName), YVal);
                           
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }

                catch
                { }
            }
            chart1.ResetAutoValues();

        }









        public void refeshchart()
        {


            chart1.Series.Clear();
            chart2.Series.Clear();
            Hashtable hatemp = new Hashtable();
            DataTable dt = new DataTable();
            DataTable dttemp = new DataTable();
            //IList<BurdenLine> list = Services.BaseService.GetList<BurdenLine>("SelectBurdenLineByMaxDate", null);
            IList<BurdenLine> list = Services.BaseService.GetList<BurdenLine>("SelectBurdenLineByWhere", " uid like '%" + Itop.Client.MIS.ProgUID + "%'  order by BurdenDate");      
                


            dt = Itop.Common.DataConverter.ToDataTable((IList)list, typeof(BurdenLine));
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["IsType"].ToString() == "1")
                {
                    foreach (DataColumn dc in dt.Columns)
                        if (dc.ColumnName == "BurdenDate")
                            hatemp.Add(dr["BurdenDate"].ToString(), "(最大负荷日)");
                }

                if (dr["IsMaxDate"].ToString() == "1")
                {
                    foreach (DataColumn dc in dt.Columns)
                        if (dc.ColumnName == "BurdenDate" && !hatemp.ContainsKey(dr["BurdenDate"].ToString()))
                            hatemp.Add(dr["BurdenDate"].ToString(), "(典型负荷日)");
                }

            }
            //list = Services.BaseService.GetList<BurdenLine>("SelectBurdenLineByType", null);
            //dttemp = Itop.Common.DataConverter.ToDataTable((IList)list, typeof(BurdenLine));
            //foreach (DataRow dr in dttemp.Rows)
            //{
            //    foreach (DataColumn dc in dttemp.Columns)
            //        if (dc.ColumnName == "BurdenDate" && !hatemp.ContainsKey(dr["BurdenDate"].ToString()))
            //            hatemp.Add(dr["BurdenDate"].ToString(), "(典型负荷日)");

            //}
            foreach (DataRow dr in dttemp.Rows)
            {
                DataRow dtnew = dt.NewRow();
                foreach (DataColumn dc in dttemp.Columns)
                {
                    //dtnew[dc.ColumnName] = dr[dc.ColumnName];
                }
                dt.Rows.Add(dtnew);

            }
            list.Clear();
            foreach (DataRow row in dt.Rows)
            {
                string seriesName = "";
                try
                {
                    string strtemp = "";
                    if (hatemp[row["BurdenDate"].ToString()] != null)
                        strtemp = hatemp[row["BurdenDate"].ToString()].ToString();
                    DateTime dateTime = (DateTime)row["BurdenDate"];
                    seriesName = FindArea(row["AreaID"].ToString()) + "_" + dateTime.ToString("yyyy年MM月dd日") + strtemp;
                }
                catch { }

                double maxval = 0;
                try
                {
                    chart1.Series.Add(seriesName);
                    chart1.Series[seriesName].Type = SeriesChartType.Line;
                    chart1.Series[seriesName].BorderWidth = 2;
                    chart1.Series[seriesName].ShowLabelAsValue = checkBox1.Checked;

                    chart2.Series.Add(seriesName);
                    chart2.Series[seriesName].Type = SeriesChartType.Line;
                    chart2.Series[seriesName].BorderWidth = 2;
                    //chart2.Series[seriesName].ShowLabelAsValue = true;
                    //chart1.Series[seriesName].ShowLabelAsValue = false;

                    for (int colIndex = 3; colIndex <= 26; colIndex++)
                    {
                        string columnName = dt.Columns[colIndex].ColumnName;
                        double YVal = (double)row[columnName];
                        maxval = Math.Max(YVal, maxval);
                    }

                    //for (int colIndex = 3; colIndex < 26; colIndex++)
                    for (int colIndex = 3; colIndex <= 26; colIndex++)
                    {
                        string columnName = dt.Columns[colIndex].ColumnName;
                        double YVal = (double)row[columnName];
                        double YVal1 = 0;
                        if (maxval != 0)
                            YVal1 = YVal / maxval;
                        try
                        {
                            chart1.Series[seriesName].Points.AddXY(GetText(columnName), YVal);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }

                        try
                        {
                            chart2.Series[seriesName].Points.AddXY(GetText(columnName), YVal1);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }

                catch
                { }
            }
            chart1.ResetAutoValues();
            chart2.ResetAutoValues();
        }





        private string GetText(string columnname)
        {
            string getText = "";
            if (columnname.Length > 4)
            {
                getText = columnname.Substring(4, columnname.Length - 4) + "时";
            }
            return getText;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            refeshchart();
        }

     
       
    }
}