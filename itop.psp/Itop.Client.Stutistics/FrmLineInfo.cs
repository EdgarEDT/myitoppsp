using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client;
using Itop.Common;
using DevExpress.XtraGrid.Columns;
using Itop.Domain.Stutistic;
using System.Collections;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.IO;

namespace Itop.Client.Stutistics
{
    public partial class FrmLineInfo : Itop.Client.Base.FormBase
    {
        private string type = "2";  //线路
        private string type2 = "2";  //线路
        private string flag = "1";  //现状

        string title = "";
        string unit = "";
        bool isSelect = false;

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




        public FrmLineInfo()
        {
            InitializeComponent();
        }



        public void Frm66KV()
        {
            //foreach (GridColumn gc in this.ctrlLine_Info1.GridView.Columns)
            //{
            //    if (gc.FieldName.Substring(0, 1) == "K" || gc.FieldName.Substring(0, 1) == "S")

            //    {
            //        gc.Visible = false;
            //    }
                    
            //}
            
            foreach (GridBand gc in this.ctrlLine_Info1.GridView.Bands)
            {
                try
                {
                    

                    if (gc.Columns[0].FieldName.Substring(0, 1) == "K" || gc.Columns[0].FieldName.Substring(0, 1) == "S")
                    {
                        gc.Visible = false;
                    }
                    gc.Width = 100;
                    gc.Caption = gc.Columns[0].Caption;
                    gc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                }
                catch { }

            }

            this.ctrlLine_Info1.gridBand17.Visible = false;
            //this.barInsert.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            type2 = "66";
            
        　　 if (System.Configuration.ConfigurationSettings.AppSettings["LangFang"].ToString() == "廊坊") 
            {
                ctrlLine_Info1.gridBand2.Caption = "地线型号";
                ctrlLine_Info1.gridBand5.Visible = false;
                ctrlLine_Info1.gridBand6.Visible = false;
            }
            if (isSelect)
            {
                //InitializeComponent();
                this.barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                this.Show();
            }
        }


        public void Frm10KV()
        {
            

            //foreach (GridColumn gc in this.ctrlLine_Info1.GridView.Columns)
            //{
            //    //if (gc.FieldName.Substring(0, 1) == "L" || gc.FieldName.Substring(0, 1) == "S")
            //    //{
            //    //    gc.Visible = false;
            //    //    gc.OptionsColumn.ShowInCustomizationForm = false;
            //    //}

            //}


            foreach (GridBand gc in this.ctrlLine_Info1.GridView.Bands)
            {


                try
                {


                    if (gc.Columns[0].FieldName.Substring(0, 1) == "L" || gc.Columns[0].FieldName.Substring(0, 1) == "S")
                    {
                        gc.Visible = false;
                    }

                    gc.Width = 100;
                    gc.Caption = gc.Columns[0].Caption;
                    gc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                }
                catch { }

            }
            type2 = "10";
       　　  if (System.Configuration.ConfigurationSettings.AppSettings["LangFang"].ToString() == "廊坊") 
            {
                ctrlLine_Info1.gridBand2.Caption = "地线型号";
            }
            if (isSelect)
            {
                //InitializeComponent();
                this.barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                this.Show();
            }
        }



        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlLine_Info1.AddObject();
        }

        private void barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlLine_Info1.UpdateObject();
        }

        private void barDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlLine_Info1.DeleteObject();
        }

        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlLine_Info1.PrintPreview();
        }

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barAdd1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FrmNewClass frm = new FrmNewClass();
                frm.Type = type;
                frm.Flag = flag;
                frm.Type2 = type2;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    //foreach (GridColumn gc in this.ctrlLine_Info1.GridView.Columns)
                    //{
                    //    if (gc.FieldName == frm.ClassType)
                    //    {
                    //        gc.Caption = frm.ClassName;
                    //        gc.Visible = true;
                    //        gc.OptionsColumn.ShowInCustomizationForm = true;
                    //    }
                    //}
                    InitVisuble();
                }

            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
        }

        private void barEdit1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridColumn gc = this.ctrlLine_Info1.GridView.FocusedColumn;
            if (gc == null)
                return;

            if (gc.FieldName.Substring(0, 1) != "S")
                return;

            FrmNewClass frm = new FrmNewClass();
            frm.ClassName = gc.Caption;
            frm.ClassType = gc.FieldName;
            frm.Type = type;
            frm.Flag = flag;
            frm.Type2 = type2;
            frm.IsUpdate = true;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                //gc.Caption = frm.ClassName;
                InitVisuble();
            }
        }

        private void barDel1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridColumn gc = this.ctrlLine_Info1.GridView.FocusedColumn;
            
            if (gc == null)
                return;
            if (gc.FieldName.Substring(0, 1) != "S")
            {
                MsgBox.Show("不能删除固定列");
                return;
            }

            if (MsgBox.ShowYesNo("是否删除 " + gc.Caption + " 的所有数据？") != DialogResult.Yes)
            {
                return;
            }


            foreach (GridBand gc1 in this.ctrlLine_Info1.GridView.Bands)
            {
                try
                {
                    if (gc1.Columns[0].Name == gc.Name)
                    {
                        gc1.Visible = false;
                    }
                }
                catch { }
            }


            //gc.Visible = false;
            //InitVisuble();
            gc.OptionsColumn.ShowInCustomizationForm = false;
            Substation_Info si = new Substation_Info();
            si.Title = gc.FieldName + "=''";
            si.Flag = flag;
            Itop.Client.Common.Services.BaseService.Update("UpdateLine_InfoByFlag", si);

            PowerSubstationLine psl = new PowerSubstationLine();
            psl.ClassType = gc.FieldName;
            psl.Flag = flag;
            psl.Type = type;
            psl.Title = gc.Caption;
            psl.Type2 = type2;
            Itop.Client.Common.Services.BaseService.Update("DeletePowerSubstationLineByAll", psl);


        }

        private void FrmLineInfo_Load(object sender, EventArgs e)
        {
            //InitSodata1();


            this.ctrlLine_Info1.GridView.GroupPanelText = this.Text;
            this.ctrlLine_Info1.Type = type;
            this.ctrlLine_Info1.Flag = flag;
            this.ctrlLine_Info1.Type2 = type2;
            this.ctrlLine_Info1.RefreshData();
            InitVisuble();
            InitRight();
        }
        private void InitRight()
        {
            if (!AddRight)
            {
                barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barAdd1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (!EditRight)
            {
                barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barEdit1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
              
                barInsert.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                ctrlLine_Info1.editright = false;
            }

            if (!DeleteRight)
            {
                barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barDel1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            
            }

            if (!PrintRight)
            {
                barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }

        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gcontrol = this.ctrlLine_Info1.GridControl;
            title = this.Text;
            unit = "";
            DialogResult = DialogResult.OK;
        }


        private void InitVisuble()
        {
            PowerSubstationLine psl = new PowerSubstationLine();
            psl.Flag = flag;
            psl.Type = type;
            psl.Type2 = type2;

            IList<PowerSubstationLine> li = Itop.Client.Common.Services.BaseService.GetList<PowerSubstationLine>("SelectPowerSubstationLineByFlagType", psl);



            //foreach (GridColumn gc1 in this.ctrlLine_Info1.GridView.Columns)
            //{
            //    if (gc1.FieldName.Substring(0, 1) == "S")
            //    {
            //        foreach (PowerSubstationLine pss in li)
            //        {

            //            if (gc1.FieldName == pss.ClassType)
            //            {
            //                gc1.Caption = pss.Title;
            //                gc1.Visible = true;
            //                gc1.OptionsColumn.ShowInCustomizationForm = true;
            //            }
            //        }
            //    }

            //}
            

            foreach (GridBand gc in this.ctrlLine_Info1.GridView.Bands)
            {
                try
                {
                    if (gc.Columns[0].FieldName.Substring(0, 1) == "S")
                    {
                        gc.Visible = false;
                        foreach (PowerSubstationLine pss in li)
                        {

                            if (gc.Columns[0].FieldName == pss.ClassType)
                            {
                                gc.Visible = true;
                                gc.Caption = pss.Title;
                                gc.Columns[0].Caption = pss.Title;
                                gc.Columns[0].Visible = true;
                                gc.Columns[0].OptionsColumn.ShowInCustomizationForm = true;

                            }
                        }
                    }
                }
                catch { }

            }


        
        }






        private void InitSodata1()
        {


            string sid = "1";

            Hashtable hs = new Hashtable();
            Hashtable hs2 = new Hashtable();

            IList<LineInfo> listLineInfo = Services.BaseService.GetList<LineInfo>("SelectLineInfoByPowerIDStuff", sid);
            foreach (LineInfo l1 in listLineInfo)
            {
                hs.Add(Guid.NewGuid().ToString(), l1.UID);
            }


            IList<Line_Info> ll1 = Common.Services.BaseService.GetList<Line_Info>("SelectLine_InfoByFlag", sid);
            foreach (Line_Info ps in ll1)
            {
                hs2.Add(Guid.NewGuid().ToString(), ps.Code);
            }



            foreach (Line_Info p1 in ll1)
            {
                if (p1.Code != "" && !hs.ContainsValue(p1.Code))
                {
                    Services.BaseService.Delete<Line_Info>(p1);
                }
            }


            foreach (LineInfo l2 in listLineInfo)
            {
                if (!hs2.ContainsValue(l2.UID) && l2.Voltage != "")
                {
                    //添加
                    try
                    {
                        //Line_Info line1 = new Line_Info();
                        //line1.DY = int.Parse(l2.Voltage.ToUpper().Replace("KV", ""));
                        //line1.Title = l2.LineName;
                        //line1.Code = l2.UID;
                        //line1.Flag = sid;
                        //Services.BaseService.Create<Line_Info>(line1);

                        Line_Info line1 = new Line_Info();

                        line1.Title = l2.LineName;
                        line1.Code = l2.UID;
                        line1.Flag = sid;


                        try
                        {
                            int dy = int.Parse(l2.Voltage.ToUpper().Replace("KV", ""));
                            line1.DY = dy;
                            line1.K1 = dy;
                            line1.L1 = dy;
                        }
                        catch { }


                        try
                        {
                            line1.L4 = l2.LineType;
                            line1.K2 = l2.LineType;

                        }
                        catch { }
                        try
                        {
                            line1.L6 = l2.ObligateField3;
                        }
                        catch { line1.L6 = null; }

                        try
                        {
                            line1.L5 = double.Parse(l2.Length);
                            line1.K5 = double.Parse(l2.Length);
                        }
                        catch { }


                        Services.BaseService.Create<Line_Info>(line1);

                    }
                    catch(Exception ex) { MsgBox.Show(ex.Message);}

                }
            }



        }



        private void InitSodata2()
        {


            string sid = "1";


            Hashtable hs1 = new Hashtable();
            Hashtable hs3 = new Hashtable();




            IList<substation> listsubstation = Services.BaseService.GetList<substation>("SelectsubstationByPowerIDStuff", sid);
            foreach (substation s1 in listsubstation)
            {
                hs1.Add(Guid.NewGuid().ToString(), s1.UID);
            }


            IList<Substation_Info> ll2 = Common.Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByFlag", sid);
            foreach (Substation_Info ps in ll2)
            {
                hs3.Add(Guid.NewGuid().ToString(), ps.Code);
            }

            foreach (Substation_Info p2 in ll2)
            {

                if (p2.Code != "" && !hs1.ContainsValue(p2.Code))
                {
                    //删除
                    Services.BaseService.Delete<Substation_Info>(p2);
                }
            }


            foreach (substation s2 in listsubstation)
            {
                if (!hs3.ContainsValue(s2.UID) && s2.ObligateField1 != "")
                {
                    //添加
                    try
                    {
                        Substation_Info substation1 = new Substation_Info();
                        substation1.L1 = int.Parse(s2.ObligateField1);
                        substation1.Title = s2.EleName;
                        substation1.Code = s2.UID;
                        substation1.Flag = sid;
                        Services.BaseService.Create<Substation_Info>(substation1);
                    }
                    catch { }

                }
            }

        }

        private void barInsert_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (type2)
            { 
                case "66" :
                    InsertLineData1();
                   
                    break;

                case "10" :
                    InsertLineData2();
           
                    break;
            
            
            
            
            }

        }







        private void InsertLineData1()
        {
            try
            {
                DataTable dts = new DataTable();
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Excel文件(*.xls)|*.xls";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    dts = GetExcel(op.FileName);
                    IList<Line_Info> lii = new List<Line_Info>();
                    DateTime s8 = DateTime.Now;
                    for (int i = 1; i < dts.Rows.Count; i++)
                    {
                        if (dts.Rows[i][1].ToString().IndexOf("合计") >= 0)
                            continue;

                        Line_Info l1 = new Line_Info();
                        foreach (DataColumn dc in dts.Columns)
                        {
                            if (dc.ColumnName == "L1")
                            {
                                int LL1 = 0;
                                try
                                {
                                    LL1 = Convert.ToInt32(dts.Rows[i]["L1"].ToString());
                                }
                                catch { }
                                l1.L1 = LL1;
                                l1.DY = LL1;
                                l1.K1 = LL1;
                            }
                            else if (dc.ColumnName == "L5")
                            {
                                double LL2 = 0;
                                try
                                {
                                    LL2 = Convert.ToDouble(dts.Rows[i]["L5"].ToString());
                                }
                                catch { }
                                l1.L5 = LL2;
                            }
                            else if (dc.ColumnName == "L6")
                            {
                                //DateTime LL3 = DateTime.Now;
                                //try
                                //{
                                //    LL3 = Convert.ToDateTime(dts.Rows[i]["L6"].ToString());
                                //}
                                //catch { }
                                l1.L6 = dts.Rows[i]["L6"].ToString();
                            }
                            else if (dc.ColumnName == "K1")
                            {
                                continue;
                            }
                            else
                            {
                                l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, dts.Rows[i][dc.ColumnName].ToString(), null);
                            }
                        }
                        l1.Flag = "1";
                        l1.CreateDate = s8.AddSeconds(i);
                        lii.Add(l1);
                    }

                    foreach (Line_Info lll in lii)
                    {
                        Line_Info l1 = new Line_Info();
                        l1.AreaName = lll.AreaName;
                        l1.Title = lll.Title;
                        l1.Flag = "1";
                        object obj = Services.BaseService.GetObject("SelectLine_InfoByNameFlag", l1);
                        if (obj != null)
                        {
                            lll.UID = ((Line_Info)obj).UID;
                            Services.BaseService.Update<Line_Info>(lll);
                        }
                        else
                        {
                            Services.BaseService.Create<Line_Info>(lll);
                        }
                    }
                    this.ctrlLine_Info1.RefreshData();
                }
            }
            catch (Exception ex) { MsgBox.Show(ex.Message); MsgBox.Show("导入格式不正确！"); }
        }

        private void InsertLineData2()
        {
            string columnname = "";

            try
            {
                DataTable dts = new DataTable();
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Excel文件(*.xls)|*.xls";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    dts = GetExcel(op.FileName);
                    IList<Line_Info> lii = new List<Line_Info>();
                    DateTime s8 = DateTime.Now;
                    for (int i = 1; i < dts.Rows.Count; i++)
                    {
                        if (dts.Rows[i][1].ToString().IndexOf("合计") >= 0)
                            continue;

                        Line_Info l1 = new Line_Info();
                        foreach (DataColumn dc in dts.Columns)
                        {
                            columnname = dc.ColumnName;


                            switch (dc.ColumnName)
                            {
                                case "K1":
                                    int LL1 = 0;
                                    try
                                    {
                                        LL1 = Convert.ToInt32(dts.Rows[i]["K1"].ToString());
                                    }
                                    catch { }
                                    l1.L1 = LL1;
                                    l1.DY = LL1;
                                    l1.K1 = LL1;
                                    break;


                                case "K4":
                                case "K5":
                                case "K6":
                                case "K7":
                                case "K16":
                                case "K17":
                                case "K18":
                                case "K19":
                                case "K20":
                                case "K21":
                                    double LL2 = 0;
                                    try
                                    {
                                        LL2 = Convert.ToDouble(dts.Rows[i][dc.ColumnName].ToString());
                                    }
                                    catch { }
                                    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, LL2, null);
                                    break;

                                case "K8":
                                case "K9":
                                case "K10":
                                case "K11":
                                case "K12":
                                case "K13":
                                case "K14":
                                case "K15":
                                    int LL3 = 0;
                                    try
                                    {
                                        LL3 = Convert.ToInt32(dts.Rows[i][dc.ColumnName].ToString());
                                    }
                                    catch { }
                                    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, LL3, null);
                                    break;

                                default:
                                    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, dts.Rows[i][dc.ColumnName].ToString(), null);
                                    break;
                            }
                        }
                        l1.Flag = "1";
                        l1.CreateDate = s8.AddSeconds(i);
                        //l1.L6 = DateTime.Now;
                        lii.Add(l1);
                    }

                    foreach (Line_Info lll in lii)
                    {
                        Line_Info l1 = new Line_Info();
                        l1.AreaName = lll.AreaName;
                        l1.Title = lll.Title;
                        l1.Flag = "1";
                        object obj = Services.BaseService.GetObject("SelectLine_InfoByNameFlag", l1);
                        if (obj != null)
                        {
                            lll.UID = ((Line_Info)obj).UID;
                            if (System.Configuration.ConfigurationSettings.AppSettings["LangFang"].ToString() == "廊坊")
                            {
                                Services.BaseService.Update("UpdateLine_InfoXingHao", lll);
                            }
                            else
                           　 Services.BaseService.Update<Line_Info>(lll);
                        }
                        else
                        {
                            Services.BaseService.Create<Line_Info>(lll);
                        }
                    }
                    this.ctrlLine_Info1.RefreshData();
                }
            }
            catch (Exception ex) { MsgBox.Show(columnname + ex.Message); MsgBox.Show("导入格式不正确！"); }
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
            Hashtable h1 = new Hashtable();
            int aa = 0;
            ////////for (int k = 1; k <= fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++)
            ////////{
            ////////    bool bl = false;
            ////////    foreach (GridBand gb in this.ctrlLine_Info1.GridView.Bands)
            ////////    {
            ////////        if (gb.Caption == fpSpread1.Sheets[0].Cells[0, k - 1].Text && gb.Columns.Count == 1)
            ////////        {
            ////////            if (type2 == "66" && gb.Columns[0].FieldName == "K1")
            ////////                continue;

            ////////            if (type2 == "10" && gb.Columns[0].FieldName == "L1")
            ////////                continue;
            ////////            dt.Columns.Add(gb.Columns[0].FieldName);

            ////////            h1.Add(aa.ToString(), gb.Columns[0].FieldName);
            ////////            aa++;
            ////////        }
            ////////    }
            ////////}

            for (int k = 1; k <= fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++)
            {
                bool bl = false;
                GridColumn gc = this.ctrlLine_Info1.GridView.VisibleColumns[k - 1];
                dt.Columns.Add(gc.FieldName);
                h1.Add(aa.ToString(), gc.FieldName);
                aa++;
            }


            int m = 0;
            if (type2 == "10")
                m = 1;
            for (int i = m; i < fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; i++)
            {
                DataRow dr = dt.NewRow();
                str = "";
                for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
                {
                    str = str + fpSpread1.Sheets[0].Cells[i, j].Text;
                    dr[h1[j.ToString()].ToString()] = fpSpread1.Sheets[0].Cells[i, j].Text;
                }
                if (str != "")
                    dt.Rows.Add(dr);

            }
            return dt;
        }








        //////////////private void InsertLineData1()
        //////////////{
        //////////////    try
        //////////////    {
        //////////////        DataTable dts = new DataTable();
        //////////////        OpenFileDialog op = new OpenFileDialog();
        //////////////        op.Filter = "Excel文件(*.xls)|*.xls";
        //////////////        if (op.ShowDialog() == DialogResult.OK)
        //////////////        {
        //////////////            dts = GetExcel(op.FileName);



        //////////////            string s1 = "";
        //////////////            string s2 = "";
        //////////////            string s3 = "";
        //////////////            string s4 = "";
        //////////////            int s5 = 0;
        //////////////            string s6 = "";
        //////////////            double s7 = 0;
        //////////////            DateTime s8 = DateTime.Now;
        //////////////            DateTime dt = new DateTime();
        //////////////            dt = DateTime.Now;
        //////////////            IList<Line_Info> lii = new List<Line_Info>();


        //////////////            for (int i = 1; i < dts.Rows.Count; i++)
        //////////////            {
        //////////////                if (dts.Rows[i][1].ToString().IndexOf("合计") >= 0)
        //////////////                    continue;



        //////////////                if (dts.Rows[i][0].ToString() != "")
        //////////////                    s1 = dts.Rows[i][0].ToString();

        //////////////                s2 = dts.Rows[i][1].ToString();
        //////////////                s3 = dts.Rows[i][2].ToString();
        //////////////                s4 = dts.Rows[i][3].ToString();
        //////////////                try
        //////////////                {
        //////////////                    s5 = int.Parse(dts.Rows[i][4].ToString());
        //////////////                }
        //////////////                catch { }
        //////////////                s6 = dts.Rows[i][5].ToString();
        //////////////                s7 = 0;
        //////////////                try
        //////////////                {
        //////////////                    s7 = double.Parse(dts.Rows[i][6].ToString());
        //////////////                }
        //////////////                catch { }

        //////////////                try
        //////////////                {
        //////////////                    s8 = DateTime.Parse(dts.Rows[i][7].ToString());
        //////////////                }
        //////////////                catch { }
        //////////////                Line_Info l1 = new Line_Info();
        //////////////                l1.AreaName = s1;
        //////////////                l1.Title = s2;
        //////////////                l1.Flag = "1";
        //////////////                Line_Info li = new Line_Info();
        //////////////                object obj = Services.BaseService.GetObject("SelectLine_InfoByNameFlag", l1);
        //////////////                if (obj != null)
        //////////////                {
        //////////////                    li = (Line_Info)obj;
        //////////////                    li.AreaName = s1;
        //////////////                    li.Title = s2;
        //////////////                    li.DY = s5;
        //////////////                    li.L1 = li.DY;
        //////////////                    li.L2 = s3;
        //////////////                    li.L3 = s4;
        //////////////                    li.L4 = s6;
        //////////////                    li.L5 = s7;
        //////////////                    li.L6 = s8;
        //////////////                    lii.Add(li);
        //////////////                   // Services.BaseService.Update<Line_Info>(li);
        //////////////                }
        //////////////                else
        //////////////                {
        //////////////                    li = new Line_Info();
        //////////////                    li.AreaName = s1;
        //////////////                    li.Title = s2;
        //////////////                    li.DY = s5;
        //////////////                    li.L1 = li.DY;
        //////////////                    li.L2 = s3;
        //////////////                    li.L3 = s4;
        //////////////                    li.L4 = s6;
        //////////////                    li.L5 = s7;
        //////////////                    li.L6 = s8;
        //////////////                    li.Flag = "1";
        //////////////                    li.CreateDate = dt.AddSeconds(i);
        //////////////                    lii.Add(li);
        //////////////                  //  Services.BaseService.Create<Line_Info>(li);

        //////////////                }
                      
        //////////////            }

        //////////////            foreach (Line_Info lll in lii)
        //////////////            {
        //////////////                Services.BaseService.Update("UpdateLine_InfoByUID",lll);
        //////////////            }

        //////////////            this.ctrlLine_Info1.RefreshData();


        //////////////        }
        //////////////    }
        //////////////    catch { MsgBox.Show("导入格式不正确！"); }

        //////////////}




        //////////////private void InsertLineData2()
        //////////////{
        //////////////    try
        //////////////    {
        //////////////        DataTable dts = new DataTable();
        //////////////        OpenFileDialog op = new OpenFileDialog();
        //////////////        op.Filter = "Excel文件(*.xls)|*.xls";
        //////////////        if (op.ShowDialog() == DialogResult.OK)
        //////////////        {
        //////////////            dts = GetExcel(op.FileName);

        //////////////            IList<Line_Info> lii = new List<Line_Info>();

        //////////////            string year = dts.Rows[0][0].ToString();
        //////////////            string da = dts.Rows[1][1].ToString();

        //////////////            DateTime dt = new DateTime();
        //////////////            dt = DateTime.Now;
        //////////////            for (int i = 2; i < dts.Rows.Count; i++)
        //////////////            {
        //////////////                Line_Info li = new Line_Info();
        //////////////                li.AreaName = dts.Rows[i][0].ToString();
        //////////////                li.Title = dts.Rows[i][1].ToString();
                    
        //////////////                try
        //////////////                {
        //////////////                    li.DY = int.Parse(dts.Rows[i][2].ToString());
        //////////////                    li.K1 = li.DY;
        //////////////                    li.L4 = li.K2;
        //////////////                    li.K3 = dts.Rows[i][4].ToString();


        //////////////                    li.L1 = li.DY;
        //////////////                }
        //////////////                catch { }

        //////////////                li.K2 = dts.Rows[i][3].ToString();



        //////////////                double ss1 = 0;
        //////////////                double ss2 = 0;
        //////////////                double ss3 = 0;
        //////////////                double ss4 = 0;
        //////////////                double ss5 = 0;

        //////////////                try
        //////////////                {
        //////////////                    ss1 = double.Parse(dts.Rows[i][7].ToString());
        //////////////                }
        //////////////                catch { }
        //////////////                try
        //////////////                {
        //////////////                    ss2 = double.Parse(dts.Rows[i][8].ToString());
        //////////////                }
        //////////////                catch { }
        //////////////                try
        //////////////                {
        //////////////                    ss3 = double.Parse(dts.Rows[i][9].ToString());
        //////////////                }
        //////////////                catch { }

        //////////////                try
        //////////////                {
        //////////////                    ss4 = double.Parse(dts.Rows[i][13].ToString());
        //////////////                }
        //////////////                catch { }
        //////////////                try
        //////////////                {
        //////////////                    ss5 = double.Parse(dts.Rows[i][15].ToString());
        //////////////                }
        //////////////                catch { }


        //////////////                li.K5 = ss1 + ss2 + ss3;

        //////////////                li.K13 = Convert.ToInt32(ss4 + ss5);


        //////////////                double k1 = 0;//最大电流
        //////////////                double k2 = 0;//安全电流
        //////////////                double k3 = 0;//电压
        //////////////                double k4 = 0;//配变总容量
        //////////////                double g3 = 1.73205;

        //////////////                double m1 = 0;
        //////////////                double m2 = 0;

        //////////////                try
        //////////////                {
        //////////////                    k1 = double.Parse(dts.Rows[i][19].ToString());
        //////////////                }
        //////////////                catch { }

        //////////////                try
        //////////////                {
        //////////////                    k2 = double.Parse(dts.Rows[i][11].ToString());
        //////////////                }
        //////////////                catch { }

        //////////////                try
        //////////////                {
        //////////////                    k3 = Convert.ToDouble(li.DY);
        //////////////                }
        //////////////                catch { }

        //////////////                try
        //////////////                {
        //////////////                    k4 = double.Parse(dts.Rows[i][16].ToString());
        //////////////                }
        //////////////                catch { }

        //////////////                if (k2 != 0)
        //////////////                    m1 = k1 * 100 / k2;

        //////////////                if (k4 != 0)
        //////////////                    m2 = k1 * g3 * k3 * 100 / k4;

        //////////////                li.K19 = m1;
        //////////////                li.K17 = m2;



        //////////////                try { li.K4 = double.Parse(dts.Rows[i][5].ToString()); }
        //////////////                catch { }
        //////////////                try
        //////////////                {
        //////////////                    //li.K5 =double.Parse(dts.Rows[i][6].ToString());
        //////////////                    li.L5 = li.K5;
        //////////////                }
        //////////////                catch { }
        //////////////                try { li.K6 = double.Parse(dts.Rows[i][7].ToString()); }
        //////////////                catch { }

        //////////////                try { li.K20 = double.Parse(dts.Rows[i][8].ToString()); }
        //////////////                catch { }
        //////////////                try { li.K21 = double.Parse(dts.Rows[i][9].ToString()); }
        //////////////                catch { }

        //////////////                li.K22 = dts.Rows[i][10].ToString();
        //////////////                try { li.K8 = int.Parse(dts.Rows[i][11].ToString()); }
        //////////////                catch { }
        //////////////                try { li.K9 = int.Parse(dts.Rows[i][12].ToString()); }
        //////////////                catch { }
        //////////////                try { li.K10 = int.Parse(dts.Rows[i][13].ToString()); }
        //////////////                catch { }
        //////////////                try { li.K11 = int.Parse(dts.Rows[i][14].ToString()); }
        //////////////                catch { }
        //////////////                try { li.K12 = int.Parse(dts.Rows[i][15].ToString()); }
        //////////////                catch { }
        //////////////                //try { li.K13 = int.Parse(dts.Rows[i][16].ToString()); }
        //////////////                //catch { }
        //////////////                try { li.K14 = int.Parse(dts.Rows[i][17].ToString()); }
        //////////////                catch { }
        //////////////                try { li.K15 = int.Parse(dts.Rows[i][18].ToString()); }
        //////////////                catch { }
        //////////////                try { li.K16 = double.Parse(dts.Rows[i][19].ToString()); }
        //////////////                catch { }
        //////////////                //try { li.K17 = double.Parse(dts.Rows[i][20].ToString()); }
        //////////////                //catch { }
        //////////////                try { li.K18 = double.Parse(dts.Rows[i][21].ToString()); }
        //////////////                catch { }
        //////////////                //try { li.K19 = double.Parse(dts.Rows[i][22].ToString()); }
        //////////////                //catch { }







        //////////////                li.CreateDate = dt.AddSeconds(i);
        //////////////                  li.Flag = "1";

        //////////////                lii.Add(li);

        //////////////            }



        //////////////            foreach (Line_Info lll in lii)
        //////////////            {
        //////////////                Services.BaseService.Create<Line_Info>(lll);
        //////////////            }

        //////////////            this.ctrlLine_Info1.RefreshData();


        //////////////        }
        //////////////    }
        //////////////    catch { MsgBox.Show("导入格式不正确！"); }
        
        //////////////}




        //////////////private DataTable GetExcel(string filepach)
        //////////////{
        //////////////    string str;
        //////////////    FarPoint.Win.Spread.FpSpread fpSpread1 = new FarPoint.Win.Spread.FpSpread();

        //////////////    try
        //////////////    {
        //////////////        fpSpread1.OpenExcel(filepach);
        //////////////    }
        //////////////    catch
        //////////////    {
        //////////////        string filepath1 = Path.GetTempPath() + "\\" + Path.GetFileName(filepach);
        //////////////        File.Copy(filepach, filepath1);
        //////////////        fpSpread1.OpenExcel(filepath1);
        //////////////        File.Delete(filepath1);
        //////////////    }
        //////////////    DataTable dt = new DataTable();
        //////////////    for (int k = 1; k <= fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++)
        //////////////    {
        //////////////        dt.Columns.Add("col" + k.ToString());
        //////////////    }


        //////////////    for (int i = 0; i < fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; i++)
        //////////////    {
        //////////////        DataRow dr = dt.NewRow();
        //////////////        str = "";
        //////////////        for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
        //////////////        {
        //////////////            str = str + fpSpread1.Sheets[0].Cells[i, j].Text;
        //////////////            dr[j] = fpSpread1.Sheets[0].Cells[i, j].Text;
        //////////////        }
        //////////////        if (str != "")
        //////////////            dt.Rows.Add(dr);

        //////////////    }
        //////////////    return dt;
        //////////////}

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FileClass.ExportExcel(this.ctrlLine_Info1.GridControl);
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MsgBox.ShowYesNo("该操作将清除所有数据，清除数据以后无法恢复,可能对其他用户的数据产生影响，请谨慎操作，你确定继续吗？") == DialogResult.No)
                return;

            Services.BaseService.Update("DeleteLine_InfoByFlag", null);
                MsgBox.Show("清除成功！");
                //InitSodata1();
            this.ctrlLine_Info1.RefreshData();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          string  filepath1 = Path.GetTempPath();
           
            

            switch (type2)
            {
                case "66":
                    this.ctrlLine_Info1.bandedGridView2.SaveLayoutToXml(filepath1 + "layout1.xml");
                    break;

                case "10":
                    this.ctrlLine_Info1.bandedGridView2.SaveLayoutToXml(filepath1 + "layout2.xml");
                    break;




            }
        }
    }
}