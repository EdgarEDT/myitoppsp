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
using Itop.Common;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.IO;
using System.Xml;
using Itop.Domain.Stutistics;
using Itop.Client.Stutistics;
using Itop.Client.ExpressCalculate;

namespace Itop.Client.Table
{
    public partial class FrmLayoutSubstationInfo_AHTL : Itop.Client.Base.FormBase
    {

        private string type = "1";  //线路
        private string type2 = "2";  //线路
        private string flag = "";  //现状

        string title = "";
        string unit = "";
        bool isSelect = false;

        private string leixing = "规划";
        string selectid = "";
        string selectname = "";

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

        public void Biandianzhan()
        {
            splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            barSubItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
          //  this.ctrlSubstation_Info1.GridView.Bands[7].Visible = false;
         //   this.ctrlSubstation_Info1.GridView.Bands[8].Visible = false;
         //  this.ctrlSubstation_Info1.GridView.Bands[0].Visible = true;
          //  this.ctrlSubstation_Info1.GridView.SortInfo.Add(this.ctrlSubstation_Info1.GridView.Columns["L1"], DevExpress.Data.ColumnSortOrder.Descending);
          //  this.ctrlSubstation_Info1.GridView.SortInfo.Add(this.ctrlSubstation_Info1.GridView.Columns["AreaName"], DevExpress.Data.ColumnSortOrder.Descending);
          //  this.ctrlSubstation_Info1.GridView.SortInfo.Add(this.ctrlSubstation_Info1.GridView.Columns["CreateDate"], DevExpress.Data.ColumnSortOrder.Ascending); 
            //this.ctrlSubstation_Info1.GridView.Columns["L7"].Visible = false;
            //this.ctrlSubstation_Info1.GridView.Columns["L8"].Visible = false;
            //this.ctrlSubstation_Info1.GridView.Columns["L11"].Visible = false;
            //this.ctrlSubstation_Info1.GridView.Columns["L12"].Visible = false;
            selectid = "1";
            selectname = "变电站数据表";
            leixing = "运行";
            this.Show();
            InitGridData();
           // InitGridDataSH("2014");
        }

        public void BiandianzhanSH(string con)
        {
            splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            barSubItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //  this.ctrlSubstation_Info1.GridView.Bands[7].Visible = false;
            //   this.ctrlSubstation_Info1.GridView.Bands[8].Visible = false;
            //  this.ctrlSubstation_Info1.GridView.Bands[0].Visible = true;
            //  this.ctrlSubstation_Info1.GridView.SortInfo.Add(this.ctrlSubstation_Info1.GridView.Columns["L1"], DevExpress.Data.ColumnSortOrder.Descending);
            //  this.ctrlSubstation_Info1.GridView.SortInfo.Add(this.ctrlSubstation_Info1.GridView.Columns["AreaName"], DevExpress.Data.ColumnSortOrder.Descending);
            //  this.ctrlSubstation_Info1.GridView.SortInfo.Add(this.ctrlSubstation_Info1.GridView.Columns["CreateDate"], DevExpress.Data.ColumnSortOrder.Ascending); 
            //this.ctrlSubstation_Info1.GridView.Columns["L7"].Visible = false;
            //this.ctrlSubstation_Info1.GridView.Columns["L8"].Visible = false;
            //this.ctrlSubstation_Info1.GridView.Columns["L11"].Visible = false;
            //this.ctrlSubstation_Info1.GridView.Columns["L12"].Visible = false;
            selectid = "1";
            selectname = "变电站数据表";
            leixing = "运行";
            InitGridDataSH(con);
            this.ShowDialog();
            
        }

        public void BiandianzhanLayout()
        {
            barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ctrlLayoutList1.GridView.DoubleClick += new EventHandler(GridView_DoubleClick);
            this.ctrlLayoutList1.GridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(GridView_FocusedRowChanged);
            
            this.Show();
            this.ctrlLayoutList1.Type = "2";
            this.ctrlLayoutList1.RefreshData1();

        }






        public FrmLayoutSubstationInfo_AHTL()
        {
            InitializeComponent();
        }

        private void FrmLayoutLineInfo_Load(object sender, EventArgs e)
        {
            if (!isSelect)
                barSelect.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            


            //foreach (GridColumn gc in this.ctrlSubstation_Info1.GridView.Columns)
            //{
            //    if (gc.FieldName.Substring(0, 1) == "S" && gc.FieldName.Substring(1, 1) != "2")
            //    {
            //        gc.Visible = false;
            //        gc.OptionsColumn.ShowInCustomizationForm = false;
            //    }
            //}
            InitValues();
            InitRight();
        }
        private void InitRight()
        {
            barSubItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (!AddRight)
            {
                barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barAdd1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (!EditRight)
            {
                barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barEdit1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem7.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.ctrlSubstation_Info1.editright = false;
                this.ctrlLayoutList1.editright = false;
            }
            if (!AddRight && !EditRight)
            {
                barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!DeleteRight)
            {
                barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barDel1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (!PrintRight)
            {
                barSubItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }

        }
        void GridView_DoubleClick(object sender, EventArgs e)
        {
            InitGridData();
        }

        void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            InitData();
        }

       


        private void InitData()
        {
            LayoutList ll = this.ctrlLayoutList1.FocusedObject;
            if (ll == null)
                return;
            InitSodata2();
            selectid = ll.UID;
            selectname = ll.ListName;
            InitGridData();
        }

        private void InitGridData()
        {
            this.ctrlSubstation_Info1.GridView.GroupPanelText = selectname;
            this.ctrlSubstation_Info1.Flag = selectid;
            ctrlSubstation_Info1.xmlflag = "guihua";
            this.ctrlSubstation_Info1.ProjectID = ProjectUID;
            this.ctrlSubstation_Info1.RefreshData1();
            this.ctrlSubstation_Info1.GridView.Bands[0].Visible = true;
            this.ctrlSubstation_Info1.GridView.Bands[0].Columns[0].Visible = true; 
            this.ctrlSubstation_Info1.CalcTotal();
        
        }
        private void InitGridDataSH(string con)
        {
            this.ctrlSubstation_Info1.GridView.GroupPanelText = selectname;
            this.ctrlSubstation_Info1.Flag = selectid;
            ctrlSubstation_Info1.xmlflag = "guihua";
            this.ctrlSubstation_Info1.ProjectID = Itop.Client.MIS.ProgUID;
            this.ctrlSubstation_Info1.RefreshData1();
            this.ctrlSubstation_Info1.GridView.Bands[0].Visible = true;
            this.ctrlSubstation_Info1.GridView.Bands[0].Columns[0].Visible = true;
            this.ctrlSubstation_Info1.CalcTotal(con);

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
            //InitGridData();
        }

        private void barDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlLayoutList1.DeleteObject();
        }

        private void barAdd1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ctrlSubstation_Info1.ProjectID = ProjectUID;
            this.ctrlSubstation_Info1.AddObject();
        }

        private void barEdit1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ctrlSubstation_Info1.ProjectID = ProjectUID;
            //this.ctrlSubstation_Info1.UpdateObject();
        }

        private void barDel1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlSubstation_Info1.DeleteObject();
        }

        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlSubstation_Info1.PrintPreview();
        }

        private void barPrint1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlSubstation_Info1.PrintPreview();
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
            string sid = selectid;

            if (sid == "")
                return;


            Hashtable hs1 = new Hashtable();
            Hashtable hs3 = new Hashtable();




            substation suub = new substation();
            suub.UID = sid;
            suub.EleName = leixing;
            IList<substation> listsubstation = Services.BaseService.GetList<substation>("SelectsubstationByPowerID1", sid);
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
                        substation1.AreaID = ProjectUID;
                        Services.BaseService.Create<Substation_Info>(substation1);
                    }
                    catch { }

                }
            }

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FrmNewClass frm = new FrmNewClass();
                frm.Type = type;
                frm.Flag = flag;
                frm.Type2 = "SubstationGuiHua";



                if (frm.ShowDialog() == DialogResult.OK)
                {
                    InitValues();
                }

                //if (frm.ShowDialog() == DialogResult.OK)
                //{
                //    foreach (GridColumn gc in this.ctrlSubstation_Info1.GridView.Columns)
                //    {
                //        if (gc.FieldName == frm.ClassType)
                //        {
                //            gc.Caption = frm.ClassName;
                //            gc.Visible = true;
                //            gc.OptionsColumn.ShowInCustomizationForm = true;
                //        }
                //    }
                //}

            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
        }
        private void InitValues()
        {
            PowerSubstationLine psl = new PowerSubstationLine();
            psl.Flag = flag;
            psl.Type = type;
            psl.Type2 = "SubstationGuiHua";

            IList<PowerSubstationLine> li = Itop.Client.Common.Services.BaseService.GetList<PowerSubstationLine>("SelectPowerSubstationLineByFlagType", psl);


            //foreach (GridBand gc in this.ctrlSubstation_Info1.GridView.Bands)
            //{
            //    try
            //    {
            //        if (gc.Columns[0].FieldName.Substring(0, 1) == "S" && gc.Columns[0].FieldName.Substring(1, 1) != "2")
            //        {
            //            gc.Visible = false;
            //            foreach (PowerSubstationLine pss in li)
            //            {

            //                if (gc.Columns[0].FieldName == pss.ClassType)
            //                {
            //                    gc.Visible = true;
            //                    gc.Caption = pss.Title;
            //                    gc.Columns[0].Caption = pss.Title;
            //                    gc.Columns[0].Visible = true;
            //                    gc.Columns[0].OptionsColumn.ShowInCustomizationForm = true;

            //                }
            //            }
            //        }
            //    }
            //    catch { }
            //}
        }
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridColumn gc = this.ctrlSubstation_Info1.GridView.FocusedColumn;
            if (gc == null)
                return;

            if (gc.FieldName.Substring(0, 1) != "S")
                return;

            FrmNewClass frm = new FrmNewClass();
            frm.ClassName = gc.Caption;
            frm.ClassType = gc.FieldName;
            frm.Type = type;
            frm.Flag = flag;
            frm.Type2 = "SubstationGuiHua";
            frm.IsUpdate = true;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                //gc.Caption = frm.ClassName;
                InitValues();
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridColumn gc = this.ctrlSubstation_Info1.GridView.FocusedColumn;
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

            foreach (GridBand gc1 in ctrlSubstation_Info1.GridView.Bands)
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
            gc.Visible = false;
            gc.OptionsColumn.ShowInCustomizationForm = false;
            Substation_Info si = new Substation_Info();
            si.Title = gc.FieldName + "=''";
            si.Flag = flag;
            Itop.Client.Common.Services.BaseService.Update("UpdateSubstation_InfoByFlag", si);

            PowerSubstationLine psl = new PowerSubstationLine();
            psl.ClassType = gc.FieldName;
            psl.Flag = flag;
            psl.Type = type;
            psl.Title = gc.Caption;
            psl.Type2 = "SubstationGuiHua";
            Itop.Client.Common.Services.BaseService.Update("DeletePowerSubstationLineByAll", psl);
        }

        private void barSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gcontrol = this.ctrlSubstation_Info1.GridControl;
            title = this.Text;
            unit = "";
            DialogResult = DialogResult.OK;
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            //IList<string> col = new List<string>();//SelectAreaNameGroupByConn
            //IList<string> area = new List<string>();
            //FrmOutput frm = new FrmOutput();
            //AddCol(ref col,ref area);
            //frm.Column = col; frm.Area = area;
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    for (int i = 0; i < ctrlSubstation_Info1.GridView.Bands.Count; i++)
            //    {
            //        for (int j = 0; j < frm.Column.Count; j++)
            //        {
            //            if (frm.Column[j] == ctrlSubstation_Info1.GridView.Bands[i].Caption)
            //                ctrlSubstation_Info1.GridView.Bands[i].Visible = false;
            //        }
            //    }
            //    string conn = " and (";
            //    for (int j = 0; j < frm.VolLev.Count; j++)
            //    {
            //        if(j==frm.VolLev.Count-1)
            //            conn += "L1=" + frm.VolLev[j] + ")";
            //        else
            //            conn += "L1=" + frm.VolLev[j] + " or ";
            //    }
            //    if(frm.Area.Count>0)
            //        conn += " and (";
            //    for (int i = 0; i < frm.Area.Count; i++)
            //    {

            //        if (frm.Area[i] == "空")
            //            frm.Area[i] = "";
            //        if(i==frm.Area.Count-1)
            //            conn += "AreaName='" + frm.Area[i] + "')";
            //        else
            //        {
            //            conn += "AreaName='" + frm.Area[i] + "' or ";
            //        }
            //    }
            //    this.ctrlSubstation_Info1.RefreshDataOut(conn);
            //    this.ctrlSubstation_Info1.CalcTotal(conn);
            //    FileClass.ExportExcel(this.ctrlSubstation_Info1.GridControl);
            //    for (int i = 0; i < ctrlSubstation_Info1.GridView.Bands.Count; i++)
            //    {
            //        ctrlSubstation_Info1.GridView.Bands[i].Visible = true;
            //    }
            //    this.ctrlSubstation_Info1.RefreshData1();
            //    this.ctrlSubstation_Info1.CalcTotal();
            //}
            FileClass.ExportExcel(this.ctrlSubstation_Info1.GridControl);
            
        }

        public void AddCol(ref IList<string> col,ref IList<string> area)
        {
            for (int i = 0; i < ctrlSubstation_Info1.GridView.Bands.Count; i++)
            {
                col.Add(ctrlSubstation_Info1.GridView.Bands[i].Caption);
            }
            IList<Substation_Info> list = Common.Services.BaseService.GetList<Substation_Info>("SelectAreaNameGroupByConn", " AreaID='"+ProjectUID+"'");
            area.Add("空");
            foreach (Substation_Info info in list)
            {
                area.Add(info.AreaName);
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ctrlLayoutList1.FocusedObject == null)
            {
                MsgBox.Show("没有记录存在，不可以保存样式！");
                return;
            }
            string filepath = Path.GetTempPath();
            this.ctrlSubstation_Info1.bandedGridView1.SaveLayoutToXml(filepath + "SubstationGuiHua.xml");

        }




        private void InsertSubstation_Info()
        {

            //LayoutList ll1 = this.ctrlLayoutList1.FocusedObject;
            //if (ll1 == null)
            //    return;

            string columnname = "";

            try
            {
                DataTable dts = new DataTable();
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Excel文件(*.xls)|*.xls";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    dts = GetExcel(op.FileName);
                    string str = "";
                    IList<Substation_Info> lii = new List<Substation_Info>();
                    DateTime s8 = DateTime.Now;
                    int x=0;
                    str = "";
                    for (int i = 0; i < dts.Rows.Count; i++)
                    {
                       
                        if (dts.Rows[i][1].ToString().IndexOf("合计") >= 0)
                            continue;

                        if (dts.Rows[i]["Title"].ToString() == "500千伏" || dts.Rows[i]["Title"].ToString() == "220千伏" || dts.Rows[i]["Title"].ToString() == "110千伏公变" || dts.Rows[i]["Title"].ToString() == "110千伏专变")
                        {
                            if (dts.Rows[i]["Title"].ToString() == "110千伏专变")
                            {
                                str = "专用";
                            }
                            else if (dts.Rows[i]["Title"].ToString() == "110千伏公变")
                                str = "公用";
                            else
                                str = "";
                            //continue;
                        }
                        try
                        {
                            int.Parse(dts.Rows[i]["S3"].ToString());
                        }
                        catch 
                        {
                        	continue;
                        }
                        Substation_Info l1 = new Substation_Info();
                        foreach (DataColumn dc in dts.Columns)
                        {
                            columnname = dc.ColumnName;
                            if (dts.Rows[i][dc.ColumnName].ToString() == "" )
                                continue;

                            switch (dc.ColumnName)
                            {
                                case "L2":
                                case "L9":
                                    double LL2 = 0;
                                    try
                                    {
                                        LL2 = Convert.ToDouble(dts.Rows[i][dc.ColumnName].ToString());
                                    }
                                    catch { }
                                    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, LL2, null);
                                    break;

                                case "L10":
                                    double L10 = 0;
                                    try
                                    {
                                        L10 = ChangeDou(dts.Rows[i][dc.ColumnName].ToString());
                                    }
                                    catch { }
                                    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, L10, null);
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

                                default:
                                    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, dts.Rows[i][dc.ColumnName].ToString(), null);
                                    break;
                            }
                        }
                        l1.S4 = str;
                        l1.Flag = selectid;
                        l1.CreateDate = s8.AddSeconds(i);
                        l1.AreaID = ProjectUID;
                        lii.Add(l1);
                    }

                    foreach (Substation_Info lll in lii)
                    {
                        Substation_Info l1 = new Substation_Info();
                        //l1.UID += "|" + ProjectUID;
                        l1.AreaName = lll.AreaName;
                        l1.Title = lll.Title;
                        l1.AreaID = ProjectUID;
                        l1.Flag = selectid;
                        try
                        {
                            int tx = int.Parse(l1.S2);
                            if (tx > DateTime.Now.Year)
                                l1.Flag = "2";
                            else
                                l1.Flag = "1";
                        }
                        catch { l1.Flag = "1"; }
                        string con = "AreaID='" + ProjectUID + "' and Title='" + l1.Title + "' and Flag='" + selectid + "'";
                        object obj = Services.BaseService.GetObject("SelectSubstation_InfoByCon", con);

                        ExpressionCalculator ca = new ExpressionCalculator();
                        if (lll.L4 != "")
                        {
                            if (ca.ISIllegal(ca.CharConverter(lll.L4)))
                            {
                                lll.L2 = ca.Calculator(ca.CharConverter(lll.L4), 2);
                            }
                        }
                        if (obj != null)
                        {
                            lll.UID = ((Substation_Info)obj).UID;

                            Services.BaseService.Update<Substation_Info>(lll);
                        }
                        else
                        {
                            lll.UID += "|" + ProjectUID;
                            Services.BaseService.Create<Substation_Info>(lll);
                        }
                    }
                    this.ctrlSubstation_Info1.CalcTotal();
                }
            }
            catch (Exception ex) { MsgBox.Show(columnname + ex.Message); MsgBox.Show("导入格式不正确！"); }
        }

        public double ChangeDou(string per)
        {
            string d = per.Substring(0, per.Length - 1);
            return double.Parse(d);
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
            string[] col = new string[14]{"S3", "AreaName", "Title", "L1", "L2", "L3", "L4","L5", "L9","L10","S1","S2","S4","S5" };
            IList<string> filedList = new List<string>();
            IList<string> capList = new List<string>();
            for (int i = 0; i < ctrlSubstation_Info1.GridView.Bands.Count; i++)
            {
                if (ctrlSubstation_Info1.GridView.Bands[i].Children.Count > 0)
                {
                    for (int j = 0; j < ctrlSubstation_Info1.GridView.Bands[i].Children.Count; j++)
                    {
                        capList.Add(ctrlSubstation_Info1.GridView.Bands[i].Children[j].Caption);
                        filedList.Add(ctrlSubstation_Info1.GridView.Bands[i].Children[j].Columns[0].FieldName);
                    }
                    continue;
                }
                capList.Add(ctrlSubstation_Info1.GridView.Bands[i].Caption);
                filedList.Add(ctrlSubstation_Info1.GridView.Bands[i].Columns[0].FieldName);
            }
            int[] colnum = new int[7] {0,2,6,8,9,7,11 };//导出的列
            int c = 0;
            //for (int k = 1; k <= fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++)
            //{
            //    bool bl = false;
            //    GridColumn gc = this.ctrlSubstation_Info1.GridView.VisibleColumns[k - 1];
            //    dt.Columns.Add(gc.FieldName);
            //    h1.Add(aa.ToString(), gc.FieldName);
            //    aa++;
            //}
            IList<string> fie = new List<string>();
            int cn=0;
            int gong = 65;
            int m = 1;
            string L1 = "";
            string S4 = "";
            string L2 = "";
            string AreaName = "";
            string temStr="";
            for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
            {
                if(capList.Contains(fpSpread1.Sheets[0].Cells[0, j].Text))
                    fie.Add(filedList[capList.IndexOf(fpSpread1.Sheets[0].Cells[0, j].Text)]);
                if (fpSpread1.Sheets[0].Cells[0, j].Text == "" && m==1)
                {
                    fie.Add("L13"); fie.Add("L14"); fie.Add("S6");
                    m = 2;
                }
            }

            for (int k = 0; k < fie.Count; k++)
            {
                dt.Columns.Add(fie[k]);
            }
            for (int i = m; i <= fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data); i++)
            {
                
                DataRow dr = dt.NewRow();
                str = "";
                for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
                {
                    dr[fie[j]] = fpSpread1.Sheets[0].Cells[i, j].Text;
                    try
                    {
                        if (fie[j] == "S6")
                        {
                            if (dr[fie[j]].ToString() == "")
                                dr[fie[j]] = string.Format("{0:f1}",double.Parse(dr["L14"].ToString())*100 / double.Parse(dr["L13"].ToString()));
                        }
                        if (fie[j] == "L10")
                        {
                            if (dr[fie[j]].ToString() == "")
                                dr[fie[j]] = string.Format("{0:f1}",double.Parse(dr["L9"].ToString())*100 / double.Parse(dr["L2"].ToString()));
                        }
                    }
                    catch { }
                }
               // GetL2(dr["L4"].ToString(), out L2);
               // dr["AreaName"] = AreaName; dr["L1"] = L1; dr["S4"] = S4; dr["L2"] = L2; dr["S3"] = ""; dr["S5"] = i.ToString();
               // if (str != "")
                    dt.Rows.Add(dr);
            }
            return dt;
        }

        public void GetL1S4(string text,out string L1,out string S4)
        {
            if (text.IndexOf("500千伏") != -1)
            {L1 = "500"; S4 = "";}
            else if (text.IndexOf("220千伏") != -1)
            {  L1 = "220"; S4 = "";}
            else if (text.IndexOf("110千伏公") != -1)
            { L1 = "110"; S4 = "公用";}
            else if (text.IndexOf("110千伏专") != -1)
            { L1 = "110"; S4 = "专用"; }
            else if (text.IndexOf("35千伏") != -1)
            { L1 = "35"; S4 = ""; }
            else if (text.IndexOf("10千伏") != -1)
            { L1 = "10"; S4 = ""; }
            else if (text.IndexOf("6千伏") != -1)
            { L1 = "6"; S4 = ""; }
            else
            { L1 = ""; S4 = ""; }
        }

        public void GetL2(string text, out string L2)
        {
            double temp = 0.0;
            if (text.IndexOf("×") != -1)
            {
                string[] t = text.Split('×');
                temp = double.Parse(t[0]);
                for (int i = 1; i < t.Length; i++)
                {
                    if (t[i] != "")
                        temp *= double.Parse(t[i]);
                }
                L2 = temp.ToString();
            }
            else if (text.IndexOf("+") != -1)
            {
                string[] t = text.Split('+');
                temp = double.Parse(t[0]);
                for (int i = 1; i < t.Length; i++)
                {
                    if (t[i] != "")
                        temp += double.Parse(t[i]);
                }
                L2 = temp.ToString();
            }
            else
            {
                L2 = text;
            }
        }



        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            InsertSubstation_Info();


            ////////////try
            ////////////{
            ////////////    DataTable dts = new DataTable();
            ////////////    OpenFileDialog op = new OpenFileDialog();
            ////////////    op.Filter = "Excel文件(*.xls)|*.xls";
            ////////////    if (op.ShowDialog() == DialogResult.OK)
            ////////////    {
            ////////////        dts = GetExcel(op.FileName);



            ////////////        string s1 = "";
            ////////////        string s2 = "";
            ////////////        int s3 = 0;
            ////////////        double s4 = 0;
            ////////////        int s5 = 0;
            ////////////        string s6 = "";
            ////////////        string s7 = "";
            ////////////        string s8 = "";
            ////////////        string s9 = "";
            ////////////        string s10 = "";
            ////////////        double s11 = 0;
            ////////////        double s12 = 0;
            ////////////        string s13 = "";
            ////////////        string s14 = "";

            ////////////        IList<Substation_Info> lii = new List<Substation_Info>();

            ////////////        DateTime dt = new DateTime();
            ////////////        dt = DateTime.Now;
            ////////////        for (int i = 2; i < dts.Rows.Count; i++)
            ////////////        {
                        
            ////////////            if (dts.Rows[i][0].ToString() != "")
            ////////////                s1 = dts.Rows[i][0].ToString();
            ////////////            if (dts.Rows[i][0].ToString().IndexOf("合计") >= 0)
            ////////////                continue;

            ////////////            s2 = dts.Rows[i][1].ToString();

            ////////////            if (s2 == "")
            ////////////                continue;
            ////////////            try
            ////////////            {
            ////////////                s3 = int.Parse(dts.Rows[i][2].ToString());
            ////////////            }
            ////////////            catch { }
            ////////////            try
            ////////////            {
            ////////////                s4 = double.Parse(dts.Rows[i][3].ToString());
            ////////////            }
            ////////////            catch { }
            ////////////            try
            ////////////            {
            ////////////                s5 = int.Parse(dts.Rows[i][4].ToString());
            ////////////            }
            ////////////            catch { }
            ////////////            s6 = dts.Rows[i][5].ToString();
            ////////////            s7 = dts.Rows[i][6].ToString();
            ////////////            s8 = dts.Rows[i][7].ToString();
            ////////////            s9 = dts.Rows[i][8].ToString();
            ////////////            s10 = dts.Rows[i][9].ToString();
            ////////////            try
            ////////////            {
            ////////////                s11 = double.Parse(dts.Rows[i][12].ToString());
            ////////////            }
            ////////////            catch { }
            ////////////            try
            ////////////            {
            ////////////                s12 = double.Parse(dts.Rows[i][13].ToString());
            ////////////            }
            ////////////            catch { }
            ////////////            s13 = dts.Rows[i][10].ToString();
            ////////////            s14 = dts.Rows[i][11].ToString();



            ////////////            Substation_Info l1 = new Substation_Info();
            ////////////            l1.AreaName = s1;
            ////////////            l1.Title = s2;
            ////////////            l1.Flag = ll1.UID;
            ////////////            Substation_Info li = new Substation_Info();
            ////////////            object obj = Services.BaseService.GetObject("SelectSubstation_InfoByNameFlag", l1);

            ////////////            if (obj != null)
            ////////////            {
            ////////////                li = (Substation_Info)obj;


            ////////////                li.AreaName = s1;
            ////////////                li.Title = s2;
            ////////////                li.L1 = s3;
            ////////////                li.L2 = s4;
            ////////////                li.L3 = s5;
            ////////////                li.L4 = s6;
            ////////////                li.L5 = s7;
            ////////////                li.L6 = s8;
            ////////////                li.L7 = s9;
            ////////////                li.L8 = s10;
            ////////////                li.L9 = s11;
            ////////////                li.L10 = s12;
            ////////////                li.L11 = s13;
            ////////////                li.L12 = s14;
            ////////////                Services.BaseService.Update<Substation_Info>(li);
            ////////////            }
            ////////////            else
            ////////////            {
            ////////////                li = new Substation_Info();
            ////////////                li.CreateDate = dt.AddSeconds(i);
            ////////////                li.AreaName = s1;
            ////////////                li.Title = s2;
            ////////////                li.L1 = s3;
            ////////////                li.L2 = s4;
            ////////////                li.L3 = s5;
            ////////////                li.L4 = s6;
            ////////////                li.L5 = s7;
            ////////////                li.L6 = s8;
            ////////////                li.L7 = s9;
            ////////////                li.L8 = s10;
            ////////////                li.L9 = s11;
            ////////////                li.L10 = s12;
            ////////////                li.L11 = s13;
            ////////////                li.L12 = s14;
            ////////////                li.Flag = ll1.UID;
            ////////////                lii.Add(li);
            ////////////                //Services.BaseService.Create<Substation_Info>(li);
                        
            ////////////            }
                     

            ////////////        }



            ////////////        foreach (Substation_Info lll in lii)
            ////////////        {
            ////////////            Services.BaseService.Create<Substation_Info>(lll);
                      
            ////////////        }

            ////////////        this.ctrlSubstation_Info1.RefreshData();


            ////////////    }
            ////////////}
            ////////////catch { MessageBox.Show("导入格式不正确！"); }
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string str="";

            try
            {
                str = System.Configuration.ConfigurationSettings.AppSettings["SvgID"];

            }
            catch { }
            DataTable dt = new DataTable();
            dt.Columns.Add("A");
            dt.Columns.Add("B");
            dt.Columns.Add("C", typeof(bool));
            dt.Columns.Add("D");

            IList<LayerGrade> li = Services.BaseService.GetList<LayerGrade>("SelectLayerGradeListBySvgDataUid", str);
            IList<SVG_LAYER> li1 = Services.BaseService.GetList<SVG_LAYER>("SelectSVG_LAYERBySvgID", str);

            DataTable dt1 = Itop.Common.DataConverter.ToDataTable((IList)li1);

            foreach (LayerGrade node in li)
            {
                DataRow row = dt.NewRow();
                row["A"] = node.SUID;
                row["B"] = node.Name;
                row["C"] = false;
                row["D"] = node.ParentID;
                dt.Rows.Add(row);
                DataRow[] rows = dt1.Select("YearID='" + node.SUID + "'");
                foreach (DataRow row1 in rows)
                {
                    DataRow row2 = dt.NewRow();
                    row2["A"] = row1["SUID"].ToString();
                    row2["B"] = row1["NAME"].ToString();
                    row2["C"] = false;
                    row2["D"] = node.SUID;
                    dt.Rows.Add(row2);
                }
            }



            //////SVGFILE sf = Services.BaseService.GetOneByKey<SVGFILE>(str);
            //////XmlDocument xd = new XmlDocument();
            //////xd.LoadXml(sf.SVGDATA);

            //////DataTable dt = new DataTable();
            //////dt.Columns.Add("A");
            //////dt.Columns.Add("B");
            //////dt.Columns.Add("C", typeof(bool));


            //////XmlNodeList nli = xd.GetElementsByTagName("layer");


            //////foreach (XmlNode node in nli)
            //////{
            //////    XmlElement xe = (XmlElement)node;
            //////    if (xe.GetAttribute("layerType") == "电网规划层")
            //////    {
            //////        DataRow row = dt.NewRow();
            //////        row["A"] = xe.GetAttribute("id");
            //////        row["B"] = xe.GetAttribute("label");
            //////        row["C"] = false;
            //////        dt.Rows.Add(row);
            //////    }
            //////}




            //foreach (DataRow rws in dt.Rows)
            //{
            //    rws["C"] = false;
            //}

            PowerPicSelect ppsn = new PowerPicSelect();
            ppsn.EachListID = selectid;

            IList<PowerPicSelect> liss = Services.BaseService.GetList<PowerPicSelect>("SelectPowerPicSelectList", ppsn);

            //foreach (PowerPicSelect pps in liss)
            //{
            //    foreach (DataRow rw in dt.Rows)
            //    {
            //        if (pps.PicSelectID == rw["A"].ToString())
            //            rw["C"] = true;
            //    }
            //}



            //FrmPicTypeSelect fpt = new FrmPicTypeSelect();
            FrmPicTreeSelect fpt = new FrmPicTreeSelect();
            fpt.DT = dt;
            if (fpt.ShowDialog() == DialogResult.OK)
            {
                dt = fpt.DT;

                int c = 0;
                foreach (PowerPicSelect pps1 in liss)
                {
                    c = 0;
                    foreach (DataRow rw in dt.Rows)
                    {
                        if (pps1.PicSelectID == rw["A"].ToString() && (bool)rw["C"])
                            c = 1;
                    }
                    if (c == 0)
                    {
                        Services.BaseService.Delete<PowerPicSelect>(pps1);
                    }
                }


                foreach (DataRow rw1 in dt.Rows)
                {
                    c = 0;
                    if ((bool)rw1["C"])
                    {
                        foreach (PowerPicSelect pps2 in liss)
                        {
                            if (pps2.PicSelectID == rw1["A"].ToString())
                                c = 1;
                        }
                        if (c == 0)
                        {
                            PowerPicSelect pp3 = new PowerPicSelect();
                            pp3.EachListID = selectid;
                            pp3.PicSelectID = rw1["A"].ToString();
                            
                            Services.BaseService.Create<PowerPicSelect>(pp3);
                        }
                    }
                }
            }


            InitSodata2();
            InitGridData();












        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MsgBox.ShowYesNo("该操作将清除所有数据，清除数据以后无法恢复,可能对其他用户的数据产生影响，请谨慎操作，你确定继续吗？") == DialogResult.No)
                return;
            string conn = "AreaID='" + ProjectUID + "'";
            Services.BaseService.Update("DeleteSubstation_InfoByConn", conn);
            MsgBox.Show("清除成功！");
            //InitSodata2();
                this.ctrlSubstation_Info1.RefreshData1();
                //this.ctrlSubstation_Info_TongLing1.RefreshData1();
                //this.ctrlSubstation_Info1.RefreshData1();
                //this.ctrlSubstation_Info_TongLing1.RefreshData1();
            
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCopy frm = new FrmCopy();
            frm.AssName = "Itop.Domain.Stutistics";
            frm.idName = "UID";
            frm.projectName = "AreaID";
            frm.bPare = false;
            frm.CurID = ProjectUID;
            frm.ClassName = "Itop.Domain.Stutistic.Substation_Info";
            frm.SelectString = "SelectSubstation_InfoByCon";
            frm.InsertString = "InsertSubstation_Info";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("导入成功");
                this.ctrlSubstation_Info1.RefreshData1();
                this.ctrlSubstation_Info1.CalcTotal();
            }
        }

        void repositoryItemCheckEdit1_CheckedChanged(object sender, System.EventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit chk = (DevExpress.XtraEditors.CheckEdit)sender;
            this.ctrlSubstation_Info1.Is_Fu(chk.Checked);
        }

        ////////////private DataTable GetExcel(string filepach)
        ////////////{
        ////////////    string str;
        ////////////    FarPoint.Win.Spread.FpSpread fpSpread1 = new FarPoint.Win.Spread.FpSpread();

        ////////////    try
        ////////////    {
        ////////////        fpSpread1.OpenExcel(filepach);
        ////////////    }
        ////////////    catch
        ////////////    {
        ////////////        string filepath1 = Path.GetTempPath() + "\\" + Path.GetFileName(filepach);
        ////////////        File.Copy(filepach, filepath1);
        ////////////        fpSpread1.OpenExcel(filepath1);
        ////////////        File.Delete(filepath1);
        ////////////    }
        ////////////    DataTable dt = new DataTable();
        ////////////    for (int k = 1; k <= fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++)
        ////////////    {
        ////////////        dt.Columns.Add("col" + k.ToString());
        ////////////    }


        ////////////    for (int i = 1; i < fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; i++)
        ////////////    {
        ////////////        DataRow dr = dt.NewRow();
        ////////////        str = "";
        ////////////        for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
        ////////////        {
        ////////////            str = str + fpSpread1.Sheets[0].Cells[i, j].Text;
        ////////////            dr[j] = fpSpread1.Sheets[0].Cells[i, j].Text;
        ////////////        }
        ////////////        if (str != "")
        ////////////            dt.Rows.Add(dr);

        ////////////    }
        ////////////    return dt;
        ////////////}
    }
}