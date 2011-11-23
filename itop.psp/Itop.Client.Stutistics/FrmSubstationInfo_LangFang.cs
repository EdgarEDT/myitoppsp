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
using Itop.Client.Common;
using Itop.Domain.Graphics;
using System.IO;
using DevExpress.XtraGrid.Views.BandedGrid;

namespace Itop.Client.Stutistics
{
    public partial class FrmSubstationInfo_LangFang : Itop.Client.Base.FormBase
    {
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
        public FrmSubstationInfo_LangFang()
        {
            InitializeComponent();
        }

        private string type = "1";  //变电站
        private string flag = "1";  //现状

        private void FrmSubstationInfo_Load(object sender, EventArgs e)
        {
            if (isSelect)
            {
                //InitializeComponent();
                this.barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }

            InitSodata2();
           
            this.ctrlSubstation_Info_LangFang1.GridView.GroupPanelText = this.Text;
            this.ctrlSubstation_Info_LangFang1.Type = type;
            this.ctrlSubstation_Info_LangFang1.Flag = flag;
            this.ctrlSubstation_Info_LangFang1.RefreshData1();

            foreach (GridColumn gc in this.ctrlSubstation_Info_LangFang1.GridView.Columns)
            {

                if (gc.FieldName.Substring(0, 1) == "S")
                {
                    gc.Visible = false;
                    gc.OptionsColumn.ShowInCustomizationForm = false;
                }


                //if (!(gc.FieldName == "Title" || gc.FieldName == "L9" || gc.FieldName == "L2" || gc.FieldName == "L1" || gc.FieldName == "L10"))
                //{
                //    gc.Visible = false;
                //    gc.OptionsColumn.ShowInCustomizationForm = false;
                //}
            }


            InitValues();
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
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                ctrlSubstation_Info_LangFang1.editright = false;
            }

            if (!DeleteRight)
            {
                barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barDel1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if(!PrintRight)
            {
                barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlSubstation_Info_LangFang1.AddObject();
        }

        private void barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlSubstation_Info_LangFang1.UpdateObject();
        }

        private void barDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlSubstation_Info_LangFang1.DeleteObject();
        }

        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlSubstation_Info_LangFang1.PrintPreview();
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
                frm.Type2 = "Substation";

             

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    InitValues();
                }

                //if (frm.ShowDialog() == DialogResult.OK)
                //{
                //    foreach(GridColumn gc in this.ctrlSubstation_Info_LangFang1.GridView.Columns)
                //    {
                //        if (gc.FieldName == frm.ClassType)
                //        {
                //            gc.Caption = frm.ClassName;
                //            gc.Visible = true;
                //            gc.OptionsColumn.ShowInCustomizationForm = true;
                //        }
                //    }




                    //if (gc.Columns[0].FieldName.Substring(0, 1) == "S")
                    //{
                    //    gc.Visible = false;
                    //    foreach (PowerSubstationLine pss in li)
                    //    {

                    //        if (gc.Columns[0].FieldName == pss.ClassType)
                    //        {
                    //            gc.Visible = true;
                    //            gc.Caption = pss.Title;
                    //            gc.Columns[0].Caption = pss.Title;
                    //            gc.Columns[0].Visible = true;
                    //            gc.Columns[0].OptionsColumn.ShowInCustomizationForm = true;

                    //        }
                    //    }
                    //}
                //}

            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
        }

        private void InitValues()
        {
            PowerSubstationLine psl = new PowerSubstationLine();
            psl.Flag = flag;
            psl.Type = type;
            psl.Type2 = "Substation";

            IList<PowerSubstationLine> li = Itop.Client.Common.Services.BaseService.GetList<PowerSubstationLine>("SelectPowerSubstationLineByFlagType", psl);

         
            foreach (GridBand gc in this.ctrlSubstation_Info_LangFang1.GridView.Bands)
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
        private void barEdit1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridColumn gc = this.ctrlSubstation_Info_LangFang1.GridView.FocusedColumn;
            if (gc == null)
                return;

            if (gc.FieldName.Substring(0, 1) != "S")
                return;

            FrmNewClass frm = new FrmNewClass();
            frm.ClassName = gc.Caption;
            frm.ClassType = gc.FieldName;
            frm.Type = type;
            frm.Flag = flag;
            frm.Type2 = "Substation";
            frm.IsUpdate = true;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                //gc.Caption = frm.ClassName;
                InitValues();
            }
        }

        private void barDel1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridColumn gc = this.ctrlSubstation_Info_LangFang1.GridView.FocusedColumn;
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

            foreach(GridBand gc1 in ctrlSubstation_Info_LangFang1.GridView.Bands )
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
            int colIndex = ctrlSubstation_Info_LangFang1.GridView.FocusedColumn.VisibleIndex;
            gc.Visible = false;
            gc.OptionsColumn.ShowInCustomizationForm = false;
            Substation_Info si = new Substation_Info();
            si.Title = gc.FieldName+"=''";
            si.Flag = flag;
            Itop.Client.Common.Services.BaseService.Update("UpdateSubstation_InfoByFlag", si);

            PowerSubstationLine psl = new PowerSubstationLine();
            psl.ClassType = gc.FieldName;
            psl.Flag = flag;
            psl.Type = type;
            psl.Title = gc.Caption;
            psl.Type2 = "Substation";
            Itop.Client.Common.Services.BaseService.Update("DeletePowerSubstationLineByAll", psl);

            if (colIndex >= ctrlSubstation_Info_LangFang1.GridView.VisibleColumns.Count)
            {
                colIndex--;
            }
            ctrlSubstation_Info_LangFang1.GridView.FocusedColumn = ctrlSubstation_Info_LangFang1.GridView.VisibleColumns[colIndex];

          //  ctrlSubstation_Info_LangFang1.GridView.FocusedColumn = ctrlSubstation_Info_LangFang1.GridView.VisibleColumns[this.ctrlSubstation_Info_LangFang1.GridView.FocusedColumn.ColumnHandle];

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gcontrol = this.ctrlSubstation_Info_LangFang1.GridControl;
            title = this.Text;
            unit = "";
            DialogResult = DialogResult.OK;
        }










        private void InitSodata1()
        {

            string sid = "1";

            Hashtable hs = new Hashtable();
            Hashtable hs2 = new Hashtable();

            IList<LineInfo> listLineInfo = Services.BaseService.GetList<LineInfo>("SelectLineInfoByPowerID", sid);
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
                        Line_Info line1 = new Line_Info();
                        line1.DY = int.Parse(l2.Voltage.ToUpper().Replace("KV", ""));
                        line1.Title = l2.LineName;
                        line1.Code = l2.UID;
                        line1.Flag = sid;
                        Services.BaseService.Create<Line_Info>(line1);
                    }
                    catch { }

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

                        try
                        {
                            substation1.L1 = int.Parse(s2.ObligateField1);

                        }
                        catch { }

                        try
                        {
                            substation1.L10 = double.Parse(s2.ObligateField2);
                        }
                        catch { }

                        try
                        {
                            substation1.L2 = Convert.ToDouble(s2.Number);
                        }
                        catch { }

                        try
                        {
                            substation1.L9 = Convert.ToDouble(s2.Burthen);
                        }
                        catch { }

                            try
                            {
                                substation1.L22 = s2.ObligateField5;//建设年份
                            }
                            catch { }

                            try
                            {
                                substation1.AreaName = s2.ObligateField6;//建设地点
                            }
                            catch { }
                        Services.BaseService.Create<Substation_Info>(substation1);


                        //Substation_Info substation1 = new Substation_Info();
                        //substation1.L1 = int.Parse(s2.ObligateField1);
                        //substation1.Title = s2.EleName;
                        //substation1.Code = s2.UID;
                        //substation1.Flag = sid;
                        //Services.BaseService.Create<Substation_Info>(substation1);
                    }
                    catch(Exception ex) {
                        System.Console.WriteLine(ex.Message);
                    }

                }
            }

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FileClass.ExportExcel(this.ctrlSubstation_Info_LangFang1.GridControl);
        }


        private void InsertSubstation_Info()
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
                    IList<Substation_Info> lii = new List<Substation_Info>();
                    DateTime s8 = DateTime.Now;
                    for (int i = 2; i < dts.Rows.Count; i++)
                    {
                        if (dts.Rows[i][1].ToString().IndexOf("合计") >= 0)
                            continue;

                        Substation_Info l1 = new Substation_Info();
                        foreach (DataColumn dc in dts.Columns)
                        {
                            columnname = dc.ColumnName;
                            if (dts.Rows[i][dc.ColumnName].ToString() == "")
                                continue;

                            switch (dc.ColumnName)
                            {
                                case "L2":
                                case "L9":
                                case "L10":
                                    double LL2 = 0;
                                    try
                                    {
                                        LL2 = Convert.ToDouble(dts.Rows[i][dc.ColumnName].ToString());
                                    }
                                    catch { }
                                    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, LL2, null);
                                    break;

                                case "L1":
                                case "L3":
                                    int LL3 = 0;
                                    try
                                    {
                                        LL3 = Convert.ToInt32(dts.Rows[i][dc.ColumnName].ToString());
                                    }
                                    catch { }
                                    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, LL3, null);
                                    break;
                                case "L13":
                                case "L14":
                                case "L15":
                                case "L16":
                                case "L17":
                                case "L18":
                                case "L19":
                                case "L20":
                                    ////int LL3 = 0;
                                    ////try
                                    ////{
                                    ////    LL3 = Convert.ToInt32(dts.Rows[i][dc.ColumnName].ToString());
                                    ////}
                                    ////catch { }
                                   
                                    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, dts.Rows[i][dc.ColumnName].ToString(), null);
                                    break;
                                default:
                                        l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, dts.Rows[i][dc.ColumnName].ToString(), null);
                                        break;
                            }
                        } 
                        l1.Flag = "1";
                        l1.CreateDate = s8.AddSeconds(i);
                        lii.Add(l1);
                    }

                    foreach (Substation_Info lll in lii)
                    {
                        Substation_Info l1 = new Substation_Info();
                        l1.AreaName = lll.AreaName;
                        l1.Title = lll.Title;
                        l1.Flag = "1";
                        object obj = Services.BaseService.GetObject("SelectSubstation_InfoByNameFlag", l1);
                        if (obj != null)
                        {
                            lll.UID = ((Substation_Info)obj).UID;
                            Services.BaseService.Update<Substation_Info>(lll);
                        }
                        else
                        {
                            Services.BaseService.Create<Substation_Info>(lll);
                        }
                    }
                    this.ctrlSubstation_Info_LangFang1.RefreshData();
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
            for (int k = 0; k <= fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++)
            {
                bool bl = false;
                GridColumn gc = this.ctrlSubstation_Info_LangFang1.GridView.VisibleColumns[k - 1];
                //dt.Columns.Add(gc.FieldName);
                //h1.Add(aa.ToString(), gc.FieldName);
                //aa++;

                foreach (GridColumn tlc in this.ctrlSubstation_Info_LangFang1.GridView.Columns)
                {
                    if (tlc.Caption == fpSpread1.Sheets[0].Cells[2, k].Text && (tlc.FieldName == "L23" || tlc.FieldName == "L13" || tlc.FieldName == "L14" || tlc.FieldName == "L15" || tlc.FieldName == "L16" || tlc.FieldName == "L17" || tlc.FieldName == "L18" || tlc.FieldName == "L19" || tlc.FieldName == "L20" || tlc.FieldName == "L7" || tlc.FieldName == "L8" || tlc.FieldName == "L11" || tlc.FieldName == "L12"))
                    {
                        //if (fpSpread1.Sheets[0].Cells[1, k].Text=="500"||fpSpread1.Sheets[0].Cells[1, k].Text=="220"||fpSpread1.Sheets[0].Cells[1, k].Text=="110"||fpSpread1.Sheets[0].Cells[1, k].Text=="35"||fpSpread1.Sheets[0].Cells[1, k].Text=="6"||fpSpread1.Sheets[0].Cells[1, k].Text=="10")
                        {
                            dt.Columns.Add(tlc.FieldName);
                            //h1.Add(aa.ToString(), tlc.FieldName);
                            if (!h1.Contains(tlc.Caption))
                            h1.Add(tlc.Caption, tlc.FieldName);
                            //aa++;
                        
                        }

                    }
                    //else if (tlc.Caption == fpSpread1.Sheets[0].Cells[2, k].Text && tlc.FieldName.IndexOf("年") >= 0)
                    //{
                    //    dt.Columns.Add(tlc.FieldName);
                    //}
                    else if (tlc.Caption == fpSpread1.Sheets[0].Cells[0, k].Text)
                    {
                        dt.Columns.Add(tlc.FieldName);
                        h1.Add(tlc.Caption, tlc.FieldName);
                        //h1.Add(aa.ToString(), tlc.FieldName);
                        //aa++;
                    }


                }
               
            }
            this.ctrlSubstation_Info_LangFang1.GridView.SaveLayoutToXml(@"C:\Documents and Settings\Administrator\桌面\aauuyy.xml");
            int m = 1;
            for (int i = m; i < fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; i++)
            {
                DataRow dr = dt.NewRow();
                str = "";
                for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
                {
                    try
                    {
                        str = str + fpSpread1.Sheets[0].Cells[i, j].Text;
                        //dr[h1[j.ToString()].ToString()] = fpSpread1.Sheets[0].Cells[i, j].Text;
                        //dr[j] = fpSpread1.Sheets[0].Cells[i, j].Text;
                        if (h1[fpSpread1.Sheets[0].Cells[2, j].Text] != null)
                            dr[h1[fpSpread1.Sheets[0].Cells[2, j].Text].ToString()] = fpSpread1.Sheets[0].Cells[i, j].Text;
                    }
                    catch { }
                }
                if (str != "")
                    dt.Rows.Add(dr);

            }
            return dt;
        }


        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertSubstation_Info();
            ////////try
            ////////{
            ////////    DataTable dts = new DataTable();
            ////////    OpenFileDialog op = new OpenFileDialog();
            ////////    op.Filter = "Excel文件(*.xls)|*.xls";
            ////////    if (op.ShowDialog() == DialogResult.OK)
            ////////    {
            ////////        dts = GetExcel(op.FileName);



            ////////        string s1 = "";
            ////////        string s2 = "";
            ////////        int s3 = 0;
            ////////        double s4 = 0;
            ////////        int s5 = 0;
            ////////        string s6 = "";
            ////////        string s7 = "";
            ////////        string s8 = "";
            ////////        string s9 = "";
            ////////        string s10 = "";
            ////////        double s11 = 0;
            ////////        double s12 = 0;
            ////////        string s13 = "";
            ////////        string s14 = "";

            ////////        IList<Substation_Info> lii = new List<Substation_Info>();

            ////////        DateTime dt = new DateTime();
            ////////        dt = DateTime.Now;
            ////////        for (int i = 2; i < dts.Rows.Count; i++)
            ////////        {
                        
            ////////            if (dts.Rows[i][0].ToString() != "")
            ////////                s1 = dts.Rows[i][0].ToString();
            ////////            if (dts.Rows[i][0].ToString().IndexOf("合计") >= 0)
            ////////                continue;

            ////////            s2 = dts.Rows[i][1].ToString();

            ////////            if (s2 == "")
            ////////                continue;
            ////////            try
            ////////            {
            ////////                s3 = int.Parse(dts.Rows[i][2].ToString());
            ////////            }
            ////////            catch { }
            ////////            try
            ////////            {
            ////////                s4 = double.Parse(dts.Rows[i][3].ToString());
            ////////            }
            ////////            catch { }
            ////////            try
            ////////            {
            ////////                s5 = int.Parse(dts.Rows[i][4].ToString());
            ////////            }
            ////////            catch { }
            ////////            s6 = dts.Rows[i][5].ToString();
            ////////            s7 = dts.Rows[i][6].ToString();
            ////////            s8 = dts.Rows[i][7].ToString();
            ////////            s9 = dts.Rows[i][8].ToString();
            ////////            s10 = dts.Rows[i][9].ToString();
            ////////            try
            ////////            {
            ////////                s11 = double.Parse(dts.Rows[i][12].ToString());
            ////////            }
            ////////            catch { }
            ////////            try
            ////////            {
            ////////                s12 = double.Parse(dts.Rows[i][13].ToString());
            ////////            }
            ////////            catch { }
            ////////            s13 = dts.Rows[i][10].ToString();
            ////////            s14 = dts.Rows[i][11].ToString();



            ////////            Substation_Info l1 = new Substation_Info();
            ////////            l1.AreaName = s1;
            ////////            l1.Title = s2;
            ////////            l1.Flag = "1";
            ////////            Substation_Info li = new Substation_Info();
            ////////            object obj = Services.BaseService.GetObject("SelectSubstation_InfoByNameFlag", l1);

            ////////            if (obj != null)
            ////////            {
            ////////                li = (Substation_Info)obj;


            ////////                li.AreaName = s1;
            ////////                li.Title = s2;
            ////////                li.L1 = s3;
            ////////                li.L2 = s4;
            ////////                li.L3 = s5;
            ////////                li.L4 = s6;
            ////////                li.L5 = s7;
            ////////                li.L6 = s8;
            ////////                li.L7 = s9;
            ////////                li.L8 = s10;
            ////////                li.L9 = s11;
            ////////                li.L10 = s12;
            ////////                li.L11 = s13;
            ////////                li.L12 = s14;
            ////////                Services.BaseService.Update<Substation_Info>(li);
            ////////            }
            ////////            else
            ////////            {
            ////////                li = new Substation_Info();
            ////////                li.CreateDate = dt.AddSeconds(i);
            ////////                li.AreaName = s1;
            ////////                li.Title = s2;
            ////////                li.L1 = s3;
            ////////                li.L2 = s4;
            ////////                li.L3 = s5;
            ////////                li.L4 = s6;
            ////////                li.L5 = s7;
            ////////                li.L6 = s8;
            ////////                li.L7 = s9;
            ////////                li.L8 = s10;
            ////////                li.L9 = s11;
            ////////                li.L10 = s12;
            ////////                li.L11 = s13;
            ////////                li.L12 = s14;
            ////////                li.Flag = "1";
                            
            ////////                lii.Add(li);
                        
            ////////            }
                      

            ////////        }



            ////////        foreach (Substation_Info lll in lii)
            ////////        {
            ////////            Services.BaseService.Create<Substation_Info>(lll);
            ////////        }

            ////////        this.ctrlSubstation_Info_LangFang1.RefreshData();


            ////////    }
            ////////}
            ////////catch { MessageBox.Show("导入格式不正确！"); }
        }

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

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MsgBox.ShowYesNo("该操作将清除所有数据，清除数据以后无法恢复,可能对其他用户的数据产生影响，请谨慎操作，你确定继续吗？") == DialogResult.No)
                return;

            Services.BaseService.Update("DeleteSubstation_InfoByFlag", null);
                MsgBox.Show("清除成功！");
                //InitSodata2();
                this.ctrlSubstation_Info_LangFang1.RefreshData1();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string filepath = Path.GetTempPath();
            this.ctrlSubstation_Info_LangFang1.bandedGridView1.SaveLayoutToXml(filepath + "SubstationLayOut.xml");
        }
        
    }


}            