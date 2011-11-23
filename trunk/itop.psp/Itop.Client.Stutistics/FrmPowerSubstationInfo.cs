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
    public partial class FrmPowerSubstationInfo : Itop.Client.Base.FormBase
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
        public FrmPowerSubstationInfo()
        {
            InitializeComponent();
        }
        private string type = "3";  //电厂
        private string flag = "1";//现状
        private string type2 = "PowerSubstation";//现状

      
        private void FrmPowerSubstationInfo_Load(object sender, EventArgs e)
        {
            if (isSelect)
            {
                //InitializeComponent();
                this.barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }

            //InitSodata2();
            
            this.ctrlPSP_PowerSubstationInfo1.GridView.GroupPanelText = this.Text;    
            this.ctrlPSP_PowerSubstationInfo1.Text = this.Text;
            this.ctrlPSP_PowerSubstationInfo1.Type = type;
            //this.ctrlPSP_PowerSubstationInfo1.Flag = flag;
            this.ctrlPSP_PowerSubstationInfo1.Flag = flag;
            this.ctrlPSP_PowerSubstationInfo1.Type2 = type2;
            this.ctrlPSP_PowerSubstationInfo1.RefreshData1();

            try
            {
                foreach (GridColumn gc in this.ctrlPSP_PowerSubstationInfo1.GridView.Columns)
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
            }
            catch (ExecutionEngineException ex)
            {
                MsgBox.Show(ex.Message);
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
              
                this.ctrlPSP_PowerSubstationInfo1.editright =false;

            }

            if (!DeleteRight)
            {
                barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barDel1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (!PrintRight)
            {
                barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }

        }
        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPSP_PowerSubstationInfo1.AddObject();
        }

        private void barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPSP_PowerSubstationInfo1.UpdateObject();
        }

        private void barDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPSP_PowerSubstationInfo1.DeleteObject();
        }

        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPSP_PowerSubstationInfo1.PrintPreview();
        }

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          this.Close();
        }

        private void barAdd1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FrmPower_AddBands frm = new FrmPower_AddBands();
                frm.Type = type;
                frm.Flag = flag;
                frm.Type2 = "PowerSubstation" + "|" + Itop.Client.MIS.ProgUID.Substring(0, 20);
                frm.AddFlag = "powerflag";


                if (frm.ShowDialog() == DialogResult.OK)
                {
                    InitValues();
                }

            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        foreach(GridColumn gc in this.ctrlPSP_PowerSubstationInfo1.GridView.Columns)
            //        {
            //            if (gc.FieldName == frm.ClassType)
            //            {
            //                gc.Caption = frm.ClassName;
            //                gc.Visible = true;
            //                gc.OptionsColumn.ShowInCustomizationForm = true;
            //            }
            //        }




            //    if (gc.Columns[0].FieldName.Substring(0, 1) == "S")
            //    {
            //        gc.Visible = false;
            //        foreach (PowerSubstationLine pss in li)
            //        {

            //            if (gc.Columns[0].FieldName == pss.ClassType)
            //            {
            //                gc.Visible = true;
            //                gc.Caption = pss.Title;
            //                gc.Columns[0].Caption = pss.Title;
            //                gc.Columns[0].Visible = true;
            //                gc.Columns[0].OptionsColumn.ShowInCustomizationForm = true;

            //            }
            //        }
            //    }
            //}
            }

            catch (Exception ex)
            { MsgBox.Show(ex.Message); }
        }
     
        private void InitValues()
        {
            PowerSubstationLine psl = new PowerSubstationLine();
            psl.Flag = flag;
            //psl.Type = type;
            psl.Type2 = "PowerSubstation" + "|" + Itop.Client.MIS.ProgUID.Substring(0, 20);

            IList<PowerSubstationLine> li = Itop.Client.Common.Services.BaseService.GetList<PowerSubstationLine>("SelectPowerSubstationLineByFlagType2", psl);

            foreach (PowerSubstationLine pss in li)
            {
                foreach (GridColumn gc in this.ctrlPSP_PowerSubstationInfo1.GridView.Columns)
                {
                    try
                    {
                        if (gc.FieldName.Substring(0, 1) == "S")
                        {
                               //gc.Visible = false;
                                if (gc.FieldName == pss.ClassType)
                                {

                                    gc.Caption = pss.Title;
                                    gc.VisibleIndex = int.Parse(pss.Type);
                                    gc.Visible = true;
                                    //gc.Columns[0].Caption = pss.Title;
                                    //gc.Columns[0].Visible = true;
                                    //gc.Columns[0].OptionsColumn.ShowInCustomizationForm = true;

                                }
                        }
                    }
                    catch (Exception e)
                    { MsgBox.Show(e.Message); }

                }
            }
            
        }
        private void barEdit1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridColumn gc = this.ctrlPSP_PowerSubstationInfo1.GridView.FocusedColumn;
            if (gc == null)
                return;

            if (gc.FieldName.Substring(0, 1) != "S")
                return;

            FrmPower_AddBands frm = new FrmPower_AddBands();
            frm.ClassName = gc.Caption;
            frm.ClassType = gc.FieldName;
            frm.Type = type;
            frm.Flag = flag;
            frm.Type2="PowerSubstation" + "|" + Itop.Client.MIS.ProgUID.Substring(0, 20);
            frm.IsUpdate = true;
            frm.Text = "修改分类";
            if (frm.ShowDialog() == DialogResult.OK)
            {
               // gc.Caption = frm.ClassName;
                InitValues();
            }
        }

        private void barDel1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
       {
           GridColumn gc = this.ctrlPSP_PowerSubstationInfo1.GridView.FocusedColumn;
           if (gc == null)
               return;

           if (gc.FieldName.Substring(0, 1) != "S")
           {
               MsgBox.Show("不能删除固定列");
               return;
           }
           bool bl = false;
            for(int i=0;i<ctrlPSP_PowerSubstationInfo1.GridView.VisibleColumns.Count;i++)
            {
                if (gc.Caption == ctrlPSP_PowerSubstationInfo1.GridView.VisibleColumns[i].Caption)
                {
                    if (MsgBox.ShowYesNo("是否删除 " + gc.Caption + " 的所有数据？") != DialogResult.Yes)
                    {
                        return;
                    }
                    bl = true;
                    break;
                }
                else
                {
                    bl = false;
                }

            }
            if (bl == true)
            {
                int colIndex = ctrlPSP_PowerSubstationInfo1.GridView.FocusedColumn.VisibleIndex;

                foreach (GridColumn gc1 in ctrlPSP_PowerSubstationInfo1.GridView.Columns)
                {
                    try
                    {
                        if (gc1.Name == gc.Name)
                        {
                            gc1.Visible = false;
                        }
                    }
                    catch { }
                }

                gc.Visible = false;
                gc.OptionsColumn.ShowInCustomizationForm = false;
                PSP_PowerSubstationInfo si = new PSP_PowerSubstationInfo();
                si.Title = gc.FieldName + "=''";
                si.Flag = flag;
                Itop.Client.Common.Services.BaseService.Update("UpdatePSP_PowerSubstationInfoByFlag", si);

                PowerSubstationLine psl = new PowerSubstationLine();
                psl.ClassType = gc.FieldName;
                psl.Flag = flag;
                //psl.Type = type;
                psl.Title = gc.Caption;
                psl.Type2="PowerSubstation" + "|" + Itop.Client.MIS.ProgUID.Substring(0, 20);
                Itop.Client.Common.Services.BaseService.Update("DeletePowerSubstationLineByNoType", psl);


                if (colIndex >= ctrlPSP_PowerSubstationInfo1.GridView.VisibleColumns.Count)
                {
                    colIndex--;
                }
                ctrlPSP_PowerSubstationInfo1.GridView.FocusedColumn = ctrlPSP_PowerSubstationInfo1.GridView.VisibleColumns[colIndex];

            }
            else
            {
                return;
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gcontrol = this.ctrlPSP_PowerSubstationInfo1.GridControl;
            title = this.Text;
            unit = "";
            DialogResult = DialogResult.OK;
        }










        //////////private void InitSodata1()
        //////////{

        //////////    string sid = "1";

        //////////    Hashtable hs = new Hashtable();
        //////////    Hashtable hs2 = new Hashtable();

        //////////    IList<LineInfo> listLineInfo = Services.BaseService.GetList<LineInfo>("SelectLineInfoByPowerID", sid);
        //////////    foreach (LineInfo l1 in listLineInfo)
        //////////    {
        //////////        hs.Add(Guid.NewGuid().ToString(), l1.UID);
        //////////    }


        //////////    IList<Line_Info> ll1 = Common.Services.BaseService.GetList<Line_Info>("SelectLine_InfoByFlag", sid);
        //////////    foreach (Line_Info ps in ll1)
        //////////    {
        //////////        hs2.Add(Guid.NewGuid().ToString(), ps.Code);
        //////////    }



        //////////    foreach (Line_Info p1 in ll1)
        //////////    {
        //////////        if (p1.Code != "" && !hs.ContainsValue(p1.Code))
        //////////        {
        //////////            Services.BaseService.Delete<Line_Info>(p1);
        //////////        }
        //////////    }


        //////////    foreach (LineInfo l2 in listLineInfo)
        //////////    {
        //////////        if (!hs2.ContainsValue(l2.UID) && l2.Voltage != "")
        //////////        {
        //////////            //添加
        //////////            try
        //////////            {
        //////////                Line_Info line1 = new Line_Info();
        //////////                line1.DY = int.Parse(l2.Voltage.ToUpper().Replace("KV", ""));
        //////////                line1.Title = l2.LineName;
        //////////                line1.Code = l2.UID;
        //////////                line1.Flag = sid;
        //////////                Services.BaseService.Create<Line_Info>(line1);
        //////////            }
        //////////            catch { }

        //////////        }
        //////////    }



        //////////}



        //////////private void InitSodata2()
        //////////{


        //////////    string sid = "1";


        //////////    Hashtable hs1 = new Hashtable();
        //////////    Hashtable hs3 = new Hashtable();




        //////////    IList<substation> listsubstation = Services.BaseService.GetList<substation>("SelectsubstationByPowerIDStuff", sid);
        //////////    foreach (substation s1 in listsubstation)
        //////////    {
        //////////        hs1.Add(Guid.NewGuid().ToString(), s1.UID);
        //////////    }


        //////////    IList<PSP_PowerSubstationInfo> ll2 = Common.Services.BaseService.GetList<PSP_PowerSubstationInfo>("SelectSubstation_InfoByFlag", sid);
        //////////    foreach (PSP_PowerSubstationInfo ps in ll2)
        //////////    {
        //////////        hs3.Add(Guid.NewGuid().ToString(), ps.Code);
        //////////    }

        //////////    foreach (PSP_PowerSubstationInfo p2 in ll2)
        //////////    {

        //////////        if (p2.Code != "" && !hs1.ContainsValue(p2.Code))
        //////////        {
        //////////            //删除
        //////////            Services.BaseService.Delete<PSP_PowerSubstationInfo>(p2);
        //////////        }
        //////////    }


        //////////    foreach (substation s2 in listsubstation)
        //////////    {
        //////////        if (!hs3.ContainsValue(s2.UID) && s2.ObligateField1 != "")
        //////////        {
        //////////            //添加
        //////////            try
        //////////            {
        //////////                PSP_PowerSubstationInfo substation1 = new PSP_PowerSubstationInfo();
        //////////                substation1.L1 = int.Parse(s2.ObligateField1);
        //////////                substation1.Title = s2.EleName;
        //////////                substation1.Code = s2.UID;
        //////////                substation1.Flag = sid;

        //////////                try
        //////////                {
        //////////                    substation1.L1 = int.Parse(s2.ObligateField1);

        //////////                }
        //////////                catch { }

        //////////                try
        //////////                {
        //////////                    substation1.L10 = double.Parse(s2.ObligateField2);
        //////////                }
        //////////                catch { }

        //////////                try
        //////////                {
        //////////                    substation1.L2 = Convert.ToDouble(s2.Number);
        //////////                }
        //////////                catch { }

        //////////                try
        //////////                {
        //////////                    substation1.L9 = Convert.ToDouble(s2.Burthen);
        //////////                }
        //////////                catch { }
        //////////                Services.BaseService.Create<PSP_PowerSubstationInfo>(substation1);


        //////////                //PSP_PowerSubstationInfo substation1 = new PSP_PowerSubstationInfo();
        //////////                //substation1.L1 = int.Parse(s2.ObligateField1);
        //////////                //substation1.Title = s2.EleName;
        //////////                //substation1.Code = s2.UID;
        //////////                //substation1.Flag = sid;
        //////////                //Services.BaseService.Create<PSP_PowerSubstationInfo>(substation1);
        //////////            }
        //////////            catch { }

        //////////        }
        //////////    }

        //////////}

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FileClass.ExportExcel(this.ctrlPSP_PowerSubstationInfo1.GridView.GroupPanelText,"" ,this.ctrlPSP_PowerSubstationInfo1.GridControl);
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

                    IList<PSP_PowerSubstationInfo> lii = new List<PSP_PowerSubstationInfo>();

                    //string year = dts.Rows[0][0].ToString();
                    //string da = dts.Rows[1][1].ToString();
                    DateTime dt= DateTime.Now;
                    for (int i =0; i < dts.Rows.Count; i++)
                    {
                        PSP_PowerSubstationInfo li = new PSP_PowerSubstationInfo();

                            li.Title = dts.Rows[i][0].ToString();
                         
                            li.Flag = "1"+ "|" + Itop.Client.MIS.ProgUID.Substring(0, 20);
                            li.UID = Guid.NewGuid().ToString();
                            li.CreateDate = dt.AddSeconds(i);
                           // li.S1 = dts.Rows[i][1].ToString();

                            foreach (DataColumn dc in dts.Columns)
                            {
                                columnname = dc.ColumnName;
                                if (dts.Rows[i][dc.ColumnName].ToString() == "")
                                    continue;

                                string LL2 = "";
                                try
                                {
                                    LL2 = dts.Rows[i][dc.ColumnName].ToString();
                                }
                                catch { }
                                li.GetType().GetProperty(dc.ColumnName).SetValue(li, LL2, null);
                              
                            }
                            //try { li.PowerName = dts.Rows[i][1].ToString(); }
                            //catch { }
                            //try { li.S1 = dts.Rows[i][2].ToString(); }
                            //catch { }
                            //try { li.S2 = dts.Rows[i][3].ToString(); }
                            //catch { }

                            //try { li.S3 = dts.Rows[i][4].ToString(); }
                            //catch { }

                            //try { li.S4 = dts.Rows[i][5].ToString(); }
                            //catch { }

                            //try { li.S5 = dts.Rows[i][6].ToString(); }
                            //catch { }

                            //try { li.S6 = dts.Rows[i][7].ToString(); }
                            //catch { }

                            //try { li.S7 = dts.Rows[i][8].ToString(); }
                            //catch { }

                            //try { li.S8 = dts.Rows[i][9].ToString(); }
                            //catch { }

                            //try { li.S9 = dts.Rows[i][10].ToString(); }
                            //catch { }

                            //try { li.S10 = dts.Rows[i][11].ToString(); }
                            //catch { }

                            //try { li.S11 = dts.Rows[i][12].ToString(); }
                            //catch { }

                            //try { li.S12 = dts.Rows[i][13].ToString(); }
                            //catch { }


                            //try { li.S13 = dts.Rows[i][14].ToString(); }
                            //catch { }
                            //try { li.S14 = dts.Rows[i][15].ToString(); }
                            //catch { }
                            //try { li.S15 = dts.Rows[i][16].ToString(); }
                            //catch { }


                            //try { li.S16 = dts.Rows[i][17].ToString(); }
                            //catch { }

                            //try { li.S17 = dts.Rows[i][18].ToString(); }
                            //catch { }

                            //try { li.S18 = dts.Rows[i][19].ToString(); }
                            //catch { }

                            //try { li.S19 = dts.Rows[i][20].ToString(); }
                            //catch { }

                            //try { li.S20 = dts.Rows[i][21].ToString(); }
                            //catch { }
                            lii.Add(li);
                           
                   

                    }

                    foreach (PSP_PowerSubstationInfo lll in lii)
                    {
                      Services.BaseService.Update("UpdatePSP_PowerSubstationInfo", lll);
                    
                    }
           
                    this.ctrlPSP_PowerSubstationInfo1.RefreshData1();


                }
            }
            catch { MessageBox.Show(columnname+"导入格式不正确！"); }
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
           for (int k = 1; k <= fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++)
           {
             //  dt.Columns.Add("col" + k.ToString());
              
               GridColumn gc = this.ctrlPSP_PowerSubstationInfo1.GridView.VisibleColumns[k - 1];
               dt.Columns.Add(gc.FieldName);
           }


           for (int i = 3; i < fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data) ; i++)
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

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ctrlPSP_PowerSubstationInfo1.ObjectList.Count == 0)
            {
                MsgBox.Show("暂时没有要删除的数据！");
                return;
            }

            if (MsgBox.ShowYesNo("该操作将清除所有数据，清除数据以后无法恢复,可能对其他用户的数据产生影响，请谨慎操作，你确定继续吗？") == DialogResult.No)
                return;

            Services.BaseService.Update("DeletePSP_PowerSubstationInfoByFlags", flag+ "|" + Itop.Client.MIS.ProgUID.Substring(0, 20));
            MsgBox.Show("清除成功！");
           // InitSodata2();
            this.ctrlPSP_PowerSubstationInfo1.RefreshData();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //string filepath = Path.GetTempPath();
            //this.ctrlPSP_PowerSubstationInfo1.bandedGridView1.SaveLayoutToXml(filepath + "SubstationLayOut.xml");
        }

       
}

}
