using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Stutistic;
using DevExpress.XtraGrid.Columns;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using System.Collections;
using DevExpress.XtraGrid.Views.BandedGrid;
using Itop.Common;
using System.IO;
using Itop.Client.Stutistics;
namespace Itop.TLPsp
{
    public partial class FrmLayoutLineInfo : Itop.Client.Base.FormBase
    {
        
        private string type = "2";  //线路
        private string type2 = "2";  //线路
        private string flag = "";  //现状

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



        public void Frm66KVLayout()
        {
            foreach (GridBand gc in this.ctrlLine_Info1.GridView.Bands)
            {
                try
                {


                    if (gc.Columns[0].FieldName.Substring(0, 1) == "K" || gc.Columns[0].FieldName.Substring(0, 1) == "S")
                    {
                        gc.Visible = false;
                    }

                    gc.Caption = gc.Columns[0].Caption;
                    gc.Width = 100;
                    gc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                }
                catch { }

            }

            type2 = "66";

            this.ctrlLine_Info1.gridBand17.Visible = false;


            if (isSelect)
            {
                //this.barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                this.Show();
            }
        }


        public void Frm10KVLayout()
        {


            foreach (GridBand gc in this.ctrlLine_Info1.GridView.Bands)
            {


                try
                {


                    if (gc.Columns[0].FieldName.Substring(0, 1) == "L" || gc.Columns[0].FieldName.Substring(0, 1) == "S")
                    {
                        gc.Visible = false;
                    }


                    gc.Caption = gc.Columns[0].Caption;
                    gc.Width = 100;
                    gc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                }
                catch { }

            }
            type2 = "1000";
            if (isSelect)
            {
                //this.barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                this.Show();
            }
        }



        public FrmLayoutLineInfo()
        {
            InitializeComponent();
        }

        private void FrmLayoutLineInfo_Load(object sender, EventArgs e)
        {
            this.ctrlLayoutList1.GridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(GridView_FocusedRowChanged);
            this.ctrlLayoutList1.Type = "1";
            this.ctrlLayoutList1.RefreshData1();
            //splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            this.ctrlLayoutList1.GridView.DoubleClick += new EventHandler(GridView_DoubleClick);



            InitVisuble();

            if (!isSelect)
                barSelect.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

        }

        void GridView_DoubleClick(object sender, EventArgs e)
        {
            InitGridData();
        }

        void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            InitGridData();
        }


        private void InitGridData()
        {
            LayoutList ll = this.ctrlLayoutList1.FocusedObject;
            if (ll == null)
                return;
            InitSodata1();

            this.ctrlLine_Info1.GridView.GroupPanelText = ll.ListName;
            this.ctrlLine_Info1.Type2 = type2;
            this.ctrlLine_Info1.RefreshData(ll.UID);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlLayoutList1.AddObject();
            InitGridData();
        }

        private void barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlLayoutList1.UpdateObject();
            InitGridData();
        }

        private void barDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlLayoutList1.DeleteObject();
        }

        private void barAdd1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlLine_Info1.AddObject();
        }

        private void barEdit1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlLine_Info1.UpdateObject();
        }

        private void barDel1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlLine_Info1.DeleteObject();
        }

        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlLayoutList1.PrintPreview();
        }

        private void barPrint1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlLine_Info1.PrintPreview();
        }


        private void InitSodata1()
        {
            if (ctrlLayoutList1.FocusedObject == null)
                return;

            string sid = ctrlLayoutList1.FocusedObject.UID;

            Hashtable hs = new Hashtable();
            Hashtable hs2 = new Hashtable();

            IList<LineInfo> listLineInfo = Services.BaseService.GetList<LineInfo>("SelectLineInfoByPowerID", sid);
            foreach (LineInfo l1 in listLineInfo)
            {
                hs.Add(Guid.NewGuid().ToString(), l1.UID);
            }


            IList<Line_Info> ll1 = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByFlag", sid);
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
                        Line_Info line1 = new Line_Info();

                        line1.Title = l2.LineName;
                        line1.Code = l2.UID;
                        line1.Flag = sid;
                        line1.CreateDate = DateTime.Now;
                        line1.L6 = (DateTime.Now).ToString();


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
                            line1.L5 = double.Parse(l2.Length);
                            line1.K5 = double.Parse(l2.Length);
                        }
                        catch { }


                        Services.BaseService.Create<Line_Info>(line1);
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }

                }
            }



        }



        private void InitSodata2()
        {
            if (ctrlLayoutList1.FocusedObject == null)
                return;

            string sid = ctrlLayoutList1.FocusedObject.UID;


            Hashtable hs1 = new Hashtable();
            Hashtable hs3 = new Hashtable();




            IList<substation> listsubstation = Services.BaseService.GetList<substation>("SelectsubstationByPowerID", sid);
            foreach (substation s1 in listsubstation)
            {
                hs1.Add(Guid.NewGuid().ToString(), s1.UID);
            }


            IList<Substation_Info> ll2 = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByFlag", sid);
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







        private void InitVisuble()
        {
            PowerSubstationLine psl = new PowerSubstationLine();
            psl.Flag = flag;
            psl.Type = type;
            psl.Type2 = type2;

            IList<PowerSubstationLine> li = Itop.Client.Common.Services.BaseService.GetList<PowerSubstationLine>("SelectPowerSubstationLineByFlagType", psl);


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

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FrmNewClass frm = new FrmNewClass();
                frm.Type = type;
                frm.Flag = flag;
                frm.Type2 = type2;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    InitVisuble();
                }

            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                InitVisuble();
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void barSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gcontrol = this.ctrlLine_Info1.GridControl;
            title = this.Text;
            unit = "";
            DialogResult = DialogResult.OK;
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FileClass.ExportExcel(this.ctrlLine_Info1.GridControl);
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ctrlLayoutList1.FocusedObject == null)
            {
                MsgBox.Show("没有记录存在，不可以保存样式！");
                return;
            }
            string filepath1 = Path.GetTempPath();
            switch (type2)
            {
                case "66":
                    this.ctrlLine_Info1.bandedGridView2.SaveLayoutToXml(filepath1 + "RefreshDatalayout1.xml");
                    break;

                case "10":
                    this.ctrlLine_Info1.bandedGridView2.SaveLayoutToXml(filepath1 + "RefreshDatalayout2.xml");
                    break;
            }
        }


         private void InsertLineData1()
        {
            LayoutList lla = this.ctrlLayoutList1.FocusedObject;
            if (lla == null)
                return;


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
                        foreach(DataColumn dc in dts.Columns)
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
                                DateTime LL3 = DateTime.Now;
                                try
                                {
                                    LL3 = Convert.ToDateTime(dts.Rows[i]["L6"].ToString());
                                }
                                catch { }
                                l1.L6 = LL3.ToString();
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
                        l1.Flag=lla.UID;
                        l1.CreateDate = s8.AddSeconds(i);
                        lii.Add(l1);
                    }

                    foreach (Line_Info lll in lii)
                    {
                        Line_Info l1 = new Line_Info();
                        l1.AreaName = lll.AreaName;
                        l1.Title = lll.Title;
                        l1.Flag = lla.UID;
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
            LayoutList lla = this.ctrlLayoutList1.FocusedObject;
            if (lla == null)
                return;


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
                        l1.Flag = lla.UID;
                        l1.CreateDate = s8.AddSeconds(i);
                        l1.L6 = (DateTime.Now).ToString();
                        lii.Add(l1);
                    }

                    foreach (Line_Info lll in lii)
                    {
                        Line_Info l1 = new Line_Info();
                        l1.AreaName = lll.AreaName;
                        l1.Title = lll.Title;
                        l1.Flag = lla.UID;
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
            catch (Exception ex) { MsgBox.Show(columnname+ex.Message); MsgBox.Show("导入格式不正确！"); }
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
            ////////    foreach(GridBand gb in this.ctrlLine_Info1.GridView.Bands)
            ////////    {
            ////////         if(gb.Caption==fpSpread1.Sheets[0].Cells[0, k-1].Text && gb.Columns.Count==1)
            ////////         {
            ////////             if (type2 == "66" && gb.Columns[0].FieldName == "K1")
            ////////                 continue;

            ////////             if (type2 == "10" && gb.Columns[0].FieldName == "L1")
            ////////                 continue;
            ////////            dt.Columns.Add(gb.Columns[0].FieldName);
                        
            ////////            h1.Add(aa.ToString(),gb.Columns[0].FieldName);
            ////////            aa++;
            ////////         }
            ////////    }
            ////////}
            string gridcolumn = "";
            for (int k = 1; k <= fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++)
            {
                bool bl = false;
                try
                {
                    GridColumn gc = this.ctrlLine_Info1.GridView.VisibleColumns[k - 1];
                    gridcolumn = gc.Caption;
                    dt.Columns.Add(gc.FieldName);
                    h1.Add(aa.ToString(), gc.FieldName);
                    aa++;
                }
                catch(Exception e) { MessageBox.Show(e.Message+" "+gridcolumn); }
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

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (type2)
            {
                case "66":
                    InsertLineData1();

                    break;

                case "10":
                    InsertLineData2();

                    break;

            }
        }


























        private void InsertData1()
        {
            LayoutList lla = this.ctrlLayoutList1.FocusedObject;
            if (lla == null)
                return;


            //////try
            //////{
                //////DataTable dts = new DataTable();
                //////OpenFileDialog op = new OpenFileDialog();
                //////op.Filter = "Excel文件(*.xls)|*.xls";
                //////if (op.ShowDialog() == DialogResult.OK)
                //////{
                //////    dts = GetExcel(op.FileName);






                //////    DateTime s8 = DateTime.Now;
                //////    DateTime dt = new DateTime();
                //////    dt = DateTime.Now;
                //////    IList<Line_Info> lii = new List<Line_Info>();


                //////    for (int i = 1; i < dts.Rows.Count; i++)
                //////    {
                //////        if (dts.Rows[i][1].ToString().IndexOf("合计") >= 0)
                //////            continue;

                        ////foreach(DataColumn bgc in this.ctrlLine_Info1.GridView.Columns)
                        ////{
                        ////    foreach(BandedGridColumn bgc in this.ctrlLine_Info1.GridView.Columns)
                        ////{
                        ////    if(bgc.Caption==
                        ////}
                        ////}
                        




                    ////    if (dts.Rows[i][0].ToString() != "")
                    ////        s1 = dts.Rows[i][0].ToString();

                    ////    s2 = dts.Rows[i][1].ToString();
                    ////    s3 = dts.Rows[i][2].ToString();
                    ////    s4 = dts.Rows[i][3].ToString();
                    ////    try
                    ////    {
                    ////        s5 = int.Parse(dts.Rows[i][4].ToString());
                    ////    }
                    ////    catch { }
                    ////    s6 = dts.Rows[i][5].ToString();
                    ////    s7 = 0;
                    ////    try
                    ////    {
                    ////        s7 = double.Parse(dts.Rows[i][6].ToString());
                    ////    }
                    ////    catch { }

                    ////    try
                    ////    {
                    ////        s8 = DateTime.Parse(dts.Rows[i][7].ToString());
                    ////    }
                    ////    catch { }
                    ////    Line_Info l1 = new Line_Info();
                    ////    l1.AreaName = s1;
                    ////    l1.Title = s2;
                    ////    l1.Flag = lla.UID;
                    ////    Line_Info li = new Line_Info();
                    ////    object obj = Services.BaseService.GetObject("SelectLine_InfoByNameFlag", l1);
                    ////    if (obj != null)
                    ////    {
                    ////        li = (Line_Info)obj;
                    ////        li.AreaName = s1;
                    ////        li.Title = s2;
                    ////        li.DY = li.DY;
                    ////        li.L1 = li.DY;
                    ////        li.L2 = s3;
                    ////        li.L3 = s4;
                    ////        li.L4 = s6;
                    ////        li.L5 = s7;
                    ////        li.L6 = s8;
                    ////        li.CreateDate = dt.AddSeconds(i);
                    ////        //  lii.Add(li);
                    ////        Services.BaseService.Update<Line_Info>(li);
                    ////    }
                    ////    else
                    ////    {
                    ////        li = new Line_Info();
                    ////        li.AreaName = s1;
                    ////        li.Title = s2;
                    ////        li.DY = s5;
                    ////        li.L1 = s5;
                    ////        li.L2 = s3;
                    ////        li.L3 = s4;
                    ////        li.L4 = s6;
                    ////        li.L5 = s7;
                    ////        li.L6 = s8;
                    ////        li.Flag = lla.UID;
                    ////        li.CreateDate = dt.AddSeconds(i);

                    ////        lii.Add(li);
                    ////        //Services.BaseService.Create<Line_Info>(li);

                    ////    }

                    ////}




                    ////foreach (Line_Info lll in lii)
                    ////{
                    ////    Services.BaseService.Create<Line_Info>(lll);
                    ////}

                    ////this.ctrlLine_Info1.RefreshData();


                //////}
            //////}
            //////catch { MsgBox.Show("导入格式不正确！"); }

        }
    }
}