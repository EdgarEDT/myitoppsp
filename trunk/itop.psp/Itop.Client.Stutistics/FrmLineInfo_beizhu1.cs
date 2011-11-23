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
    public partial class FrmLineInfo_beizhu1 : Itop.Client.Base.FormBase
    {
        private string type = "1";  //线路
        private string type2 = "2";  //线路
        private string flag = "1";  //现状
        private string z = "1";
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




        public FrmLineInfo_beizhu1()
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

                    gc.Caption = gc.Columns[0].Caption;
                    gc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                }
                catch { }

            }

            this.ctrlLine_Info1.gridBand3.Visible = false;
            //this.barInsert.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            type2 = "66";


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


                    gc.Caption = gc.Columns[0].Caption;
                    gc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                }
                catch { }

            }
            type2 = "10";
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
                FrmNewClass_beizhu frm = new FrmNewClass_beizhu();
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

            FrmNewClass_beizhu frm = new FrmNewClass_beizhu();
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
            Itop.Client.Common.Services.BaseService.Update("UpdateLine_beizhuByFlag", si);

            Line_beicong psl = new Line_beicong();
            psl.ClassType = gc.FieldName;
            psl.Flag = flag;
            psl.Type = type;
            psl.Title = gc.Caption;
            psl.Type2 = type2;
            Itop.Client.Common.Services.BaseService.Update("DeleteLine_beicongByAll", psl);


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
            Line_beicong psl = new Line_beicong();
            psl.Flag = flag;
            psl.Type = type;
            psl.Type2 = type2;

            IList<Line_beicong> li = Itop.Client.Common.Services.BaseService.GetList<Line_beicong>("SelectLine_beicongByFlagType", psl);



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
                        foreach (Line_beicong pss in li)
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


            IList<Line_beizhu> ll1 = Common.Services.BaseService.GetList<Line_beizhu>("SelectLine_beizhuByFlag", sid);
            foreach (Line_beizhu ps in ll1)
            {
                hs2.Add(Guid.NewGuid().ToString(), ps.Code);
            }



            foreach (Line_beizhu p1 in ll1)
            {
                if (p1.Code != "" && !hs.ContainsValue(p1.Code))
                {
                    Services.BaseService.Delete<Line_beizhu>(p1);
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

                        Line_beizhu line1 = new Line_beizhu();

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
                            line1.L5 = double.Parse(l2.Length);
                            line1.K5 = double.Parse(l2.Length);
                        }
                        catch { }


                        Services.BaseService.Create<Line_beizhu>(line1);

                    }
                    catch (Exception ex) { MsgBox.Show(ex.Message); }

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
             InsertLineData1();
        }





        private void InsertLineData1()
        {
            Line_beizhu z1 = new Line_beizhu();
            Line_beicong z2 = new Line_beicong();
            //try
            //{
                DataTable dts = new DataTable();
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Excel文件(*.xls)|*.xls";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    dts = GetExcel(op.FileName);
                    for (int i = 1; i < dts.Rows.Count; i++)
                    {
                        try
                        {
                            z1.Title = dts.Rows[i][0].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S1 = dts.Rows[i][1].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S2 = dts.Rows[i][2].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S3 = dts.Rows[i][3].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S4 = dts.Rows[i][4].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S5 = dts.Rows[i][5].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S6 = dts.Rows[i][6].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S7 = dts.Rows[i][7].ToString();
                           
                        }
                        catch { }
                        try
                        {
                            z1.S8 = dts.Rows[i][8].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S9 = dts.Rows[i][9].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S10 = dts.Rows[i][10].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S11 = dts.Rows[i][11].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S12 = dts.Rows[i][12].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S13 = dts.Rows[i][13].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S14 = dts.Rows[i][14].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S15 = dts.Rows[i][15].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S16 = dts.Rows[i][16].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S17 = dts.Rows[i][17].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S18 = dts.Rows[i][18].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S19 = dts.Rows[i][19].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S20 = dts.Rows[i][20].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S21 = dts.Rows[i][21].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S22 = dts.Rows[i][22].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S23 = dts.Rows[i][23].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S24 = dts.Rows[i][24].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S25 = dts.Rows[i][25].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S26 = dts.Rows[i][26].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S27 = dts.Rows[i][27].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S28 = dts.Rows[i][28].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S29 = dts.Rows[i][29].ToString();
                        }
                        catch { }
                        try
                        {
                            z1.S30 = dts.Rows[i][30].ToString();
                        }
                        catch { }
                        if (z1.Title == "")
                            z1.Title = null;
                        if (z1.S1 == "")
                            z1.S1 = null;
                        if (z1.S2 == "")
                            z1.S2 = null;
                        if (z1.S3 == "")
                            z1.S3 = null;
                        if (z1.S4 == "")
                            z1.S4 = null;
                        if (z1.S5 == "")
                            z1.S5 = null;
                        if (z1.S6 == "")
                            z1.S6 = null;
                        if (z1.S7 == "")
                            z1.S7 = null;
                        if (z1.S8 == "")
                            z1.S8 = null;
                        if (z1.S9 == "")
                            z1.S9 = null;
                        if (z1.S10 == "")
                            z1.S10= null;
                        if (z1.S11 == "")
                            z1.S11 = null;
                        if (z1.S12== "")
                            z1.S12= null;
                        if (z1.S13== "")
                            z1.S13= null;
                        if (z1.S14== "")
                            z1.S14= null;
                        if (z1.S15== "")
                            z1.S15= null;
                        if (z1.S16== "")
                            z1.S16= null;
                        if (z1.S17== "")
                            z1.S17= null;
                        if (z1.S18== "")
                            z1.S18= null;
                        if (z1.S19== "")
                            z1.S19= null;
                        if (z1.S20== "")
                            z1.S20= null;
                        if (z1.S21== "")
                            z1.S21= null;
                        if (z1.S22== "")
                            z1.S22= null;
                        if (z1.S23== "")
                            z1.S23= null;
                        if (z1.S24== "")
                            z1.S24= null;
                        if (z1.S25== "")
                            z1.S25= null;
                        if (z1.S26== "")
                            z1.S26= null;
                        if (z1.S27== "")
                            z1.S27= null;
                        if (z1.S28== "")
                            z1.S28= null;
                        if (z1.S29== "")
                            z1.S29= null;
                        if (z1.S30== "")
                            z1.S30= null;
                        z1.Flag = z;
                        z1.AreaID = null;
                        z1.AreaName = null;
                        z1.Code = null;
                        z1.IsConn = null;
                        z1.K22 = null;
                        z1.K2 = null;
                        z1.K3 = null;
                        z1.L2 = null;
                        z1.L3 = null;
                        z1.L4 = null;
                        z1.L6 = DateTime.Now;
                        z1.SubsationID = null;
                        z1.SubstationName = null;
                        z1.UID = Guid.NewGuid().ToString();
                        Services.BaseService.Create<Line_beizhu>(z1);
                        foreach (DataColumn dc in dts.Columns)
                        {                         
                            for (int j = 1; j <= dts.Columns.Count; j++)
                            {
                            if (dc.Caption.IndexOf("自定义列") >= 0)
                            {


                            z2.Title = dc.ColumnName;
                            z2.Type = z;
                            z2.Flag = "1";
                            z2.Type2 = "66";
                            
                            z2.ClassType = "S"+j;
                            z2.UID = Guid.NewGuid().ToString();
                            Services.BaseService.Create<Line_beicong>(z2);
                            break;
                            }
                        }

                          
                        }

                       
                       

                    }
        
                    this.ctrlLine_Info1.RefreshData();


                }
            }
        //    catch { MsgBox.Show("导入格式不正确！"); }

        //}



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
            for (int k = 0; k < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++)
            {
                //dt.Columns.Add("col" + k.ToString());
                string aa = fpSpread1.Sheets[0].Cells[0, k].Value.ToString();
                dt.Columns.Add(aa);

            }


            for (int i = 0; i < fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; i++)
            {
                DataRow dr = dt.NewRow();
                str = "";
                for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
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
            FileClass.ExportExcel(this.ctrlLine_Info1.GridControl);
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MsgBox.ShowYesNo("该操作将清除所有数据，清除数据以后无法恢复,可能对其他用户的数据产生影响，请谨慎操作，你确定继续吗？") == DialogResult.No)
                return;

            Services.BaseService.Update("DeleteLine_beizhuByFlag", null);
            MsgBox.Show("清除成功！");
            //InitSodata1();
            this.ctrlLine_Info1.RefreshData();
        }


    }
}