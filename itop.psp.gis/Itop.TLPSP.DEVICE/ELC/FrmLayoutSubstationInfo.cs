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
using Itop.Domain.Stutistic;
using Itop.Client.Stutistics;
using System.Xml;
using Itop.Domain.Stutistics;

namespace Itop.TLPSP.DEVICE
{
    public partial class FrmLayoutSubstationInfo : Itop.Client.Base.FormBase
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
        List<int> row_num = new List<int>();   //记录要进行变色的行数
        bool kekaoflag = false;
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
        string strID;
        PSP_ELCPROJECT parentobj;
        public void Biandianzhan()
        {
            tucengflag = false;
            splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            barSubItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            selectid = "10000";           //为现状变电站
            selectname = "现状表";
            leixing = "运行";
            PSPProject pd = new PSPProject();
            pd.ProjectID = this.ProjectUID;
            pd.Initdata(false);
            
            if (pd.ShowDialog() == DialogResult.OK)
            {
                strID = pd.FileSUID;
                parentobj = pd.Parentobj;
                this.Text = "方案-" + parentobj.Name;
                this.Show();
                InitGridData_SH(parentobj);
            }
           
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






        public FrmLayoutSubstationInfo()
        {
            InitializeComponent();
        }

        private void FrmLayoutLineInfo_Load(object sender, EventArgs e)
        {
            if (!isSelect)
                barSelect.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            


            foreach (GridColumn gc in this.ctrlSubstation_Info1.GridView.Columns)
            {
                if (gc.FieldName.Substring(0, 1) == "S")
                {
                    gc.Visible = false;
                    gc.OptionsColumn.ShowInCustomizationForm = false;
                }
            }
            InitValues();
            InitRight();
        }
        private void InitRight()
        {
            barSubItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;  //将分类管理暂停
            barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never; //把关联图层隐藏
            barSubItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;// 隐藏变电站管理
            //if (!AddRight)
            //{
            //    barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //    barAdd1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //    barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //}

            //if (!EditRight)
            //{
            //    barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //    barEdit1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //    barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //    barButtonItem7.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //    this.ctrlSubstation_Info1.editright = false;
            //    this.ctrlLayoutList1.editright = false;
            //}
            //if (!AddRight && !EditRight)
            //{
            //    barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //}
            //if (!DeleteRight)
            //{
            //    barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //    barDel1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //    barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //}

            //if (!PrintRight)
            //{
            //    barSubItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            //}

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
        bool tucengflag = false;
        private void InitGridData()
        {
            if (!tucengflag)
            this.ctrlSubstation_Info1.GridView.GroupPanelText = "变压器可靠性检验表";
            this.ctrlSubstation_Info1.ProjectID = Itop.Client.MIS.ProgUID;
            this.ctrlSubstation_Info1.Flag = selectid;
            ctrlSubstation_Info1.xmlflag = "guihua";
            this.ctrlSubstation_Info1.RefreshData1();

        
        }
        private void InitGridData_SH(PSP_ELCPROJECT prj)
        {
            if (!tucengflag)
                this.ctrlSubstation_Info1.GridView.GroupPanelText = "变压器可靠性检验表";
            this.ctrlSubstation_Info1.ProjectID = Itop.Client.MIS.ProgUID;
            this.ctrlSubstation_Info1.Flag = selectid;
            ctrlSubstation_Info1.xmlflag = "guihua";
            this.ctrlSubstation_Info1.RefreshData1(prj);


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
            this.ctrlSubstation_Info1.ProjectID = Itop.Client.MIS.ProgUID;
            this.ctrlSubstation_Info1.AddObject();
        }

        private void barEdit1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlSubstation_Info1.ProjectID = Itop.Client.MIS.ProgUID;
            this.ctrlSubstation_Info1.UpdateObject();
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

            IList<LineInfo> listLineInfo = UCDeviceBase.DataService.GetList<LineInfo>("SelectLineInfoByPowerID", sid);
            foreach (LineInfo l1 in listLineInfo)
            {
                hs.Add(Guid.NewGuid().ToString(), l1.UID);
            }


            IList<Line_Info> ll1 = UCDeviceBase.DataService.GetList<Line_Info>("SelectLine_InfoByFlag", sid);
            foreach (Line_Info ps in ll1)
            {
                hs2.Add(Guid.NewGuid().ToString(), ps.Code);
            }



            foreach (Line_Info p1 in ll1)
            {
                if (p1.Code != "" && !hs.ContainsValue(p1.Code))
                {
                    UCDeviceBase.DataService.Delete<Line_Info>(p1);
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
                        UCDeviceBase.DataService.Create<Line_Info>(line1);
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
            IList<substation> listsubstation = UCDeviceBase.DataService.GetList<substation>("SelectsubstationByPowerID1", sid);
            foreach (substation s1 in listsubstation)
            {
                hs1.Add(Guid.NewGuid().ToString(), s1.UID);
            }


            IList<Substation_Info> ll2 = UCDeviceBase.DataService.GetList<Substation_Info>("SelectSubstation_InfoByFlag", sid);
            foreach (Substation_Info ps in ll2)
            {
                hs3.Add(Guid.NewGuid().ToString(), ps.Code);
            }

            foreach (Substation_Info p2 in ll2)
            {

                if (p2.Code != "" && !hs1.ContainsValue(p2.Code))
                {
                    //删除
                    UCDeviceBase.DataService.Delete<Substation_Info>(p2);
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
                            if (substation1.L10==null&&s2.Number!=0)
                            {
                                substation1.L10 = Convert.ToDouble(s2.Burthen) / Convert.ToDouble(s2.Number) * 100;
                            }
                        }
                        catch { }

                        try
                        {
                            if (s2.Number != 0)
                            {
                                substation1.L2 = (double)(s2.Number);
                            }
                            
                        }
                        catch { }
                         try
                        {
                          substation1.L3=Convert.ToInt32(s2.ObligateField7);
                        }
                        catch 
                        {
                        	
                        }
                        try
                        {
                            substation1.L9 = Convert.ToDouble(s2.Burthen);
                        }
                        catch { }
                        substation1.AreaID = Itop.Client.MIS.ProgUID;
                        UCDeviceBase.DataService.Create<Substation_Info>(substation1);
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

            IList<PowerSubstationLine> li = UCDeviceBase.DataService.GetList<PowerSubstationLine>("SelectPowerSubstationLineByFlagType", psl);


            foreach (GridBand gc in this.ctrlSubstation_Info1.GridView.Bands)
            {

                try
                {
                    if (gc.Columns[0].FieldName.Substring(0, 1) == "S")
                    {
                        gc.Visible = false;
                        //foreach (PowerSubstationLine pss in li)
                        //{

                        //    if (gc.Columns[0].FieldName == pss.ClassType)
                        //    {
                        //        gc.Visible = true;
                        //        gc.Caption = pss.Title;
                        //        gc.Columns[0].Caption = pss.Title;
                        //        gc.Columns[0].Visible = true;
                        //        gc.Columns[0].OptionsColumn.ShowInCustomizationForm = true;

                        //    }
                        //}
                    }
                }
                catch { }
            }
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
            UCDeviceBase.DataService.Update("UpdateSubstation_InfoByFlag", si);

            PowerSubstationLine psl = new PowerSubstationLine();
            psl.ClassType = gc.FieldName;
            psl.Flag = flag;
            psl.Type = type;
            psl.Title = gc.Caption;
            psl.Type2 = "SubstationGuiHua";
            UCDeviceBase.DataService.Update("DeletePowerSubstationLineByAll", psl);
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
            //FileClass.ExportExcel(this.ctrlSubstation_Info1.GridView.GroupPanelText,"", this.ctrlSubstation_Info1.GridControl);
            FileClass.ExportExcel(this.ctrlSubstation_Info1.GridControl);
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

            LayoutList ll1 = this.ctrlLayoutList1.FocusedObject;
            if (ll1 == null)
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
                    IList<Substation_Info> lii = new List<Substation_Info>();
                    DateTime s8 = DateTime.Now;
                    for (int i = 1; i < dts.Rows.Count; i++)
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

                                default:
                                    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, dts.Rows[i][dc.ColumnName].ToString(), null);
                                    break;
                            }
                        }
                        l1.Flag = ll1.UID;
                        l1.CreateDate = s8.AddSeconds(i);
                        lii.Add(l1);
                    }

                    foreach (Substation_Info lll in lii)
                    {
                        Substation_Info l1 = new Substation_Info();
                        l1.AreaName = lll.AreaName;
                        l1.Title = lll.Title;
                        l1.Flag = ll1.UID;
                        object obj = UCDeviceBase.DataService.GetObject("SelectSubstation_InfoByNameFlag", l1);
                        if (obj != null)
                        {
                            lll.UID = ((Substation_Info)obj).UID;
                            UCDeviceBase.DataService.Update<Substation_Info>(lll);
                        }
                        else
                        {
                            UCDeviceBase.DataService.Create<Substation_Info>(lll);
                        }
                    }
                    this.ctrlSubstation_Info1.RefreshData();
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
            for (int k = 1; k <= fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++)
            {
                bool bl = false;
                GridColumn gc = this.ctrlSubstation_Info1.GridView.VisibleColumns[k - 1];
                dt.Columns.Add(gc.FieldName);
                h1.Add(aa.ToString(), gc.FieldName);
                aa++;
            }

            int m = 3;
            for (int i = m; i < fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data); i++)
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





        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            InsertSubstation_Info();

            this.ctrlSubstation_Info1.GridView.GroupPanelText=null;
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
            ////////////            object obj = UCDeviceBase.DataService.GetObject("SelectSubstation_InfoByNameFlag", l1);

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
            ////////////                UCDeviceBase.DataService.Update<Substation_Info>(li);
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
            ////////////                //UCDeviceBase.DataService.Create<Substation_Info>(li);
                        
            ////////////            }
                     

            ////////////        }



            ////////////        foreach (Substation_Info lll in lii)
            ////////////        {
            ////////////            UCDeviceBase.DataService.Create<Substation_Info>(lll);
                      
            ////////////        }

            ////////////        this.ctrlSubstation_Info1.RefreshData();


            ////////////    }
            ////////////}
            ////////////catch { MessageBox.Show("导入格式不正确！"); }
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlSubstation_Info1.GridView.GroupPanelText = "";
            string str="";
            tucengflag = true; //显示关联的图层
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

            IList<LayerGrade> li = UCDeviceBase.DataService.GetList<LayerGrade>("SelectLayerGradeListBySvgDataUid", str);
            IList<SVG_LAYER> li1 = UCDeviceBase.DataService.GetList<SVG_LAYER>("SelectSVG_LAYERBySvgID", str);

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



            //////SVGFILE sf = UCDeviceBase.DataService.GetOneByKey<SVGFILE>(str);
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

            IList<PowerPicSelect> liss = UCDeviceBase.DataService.GetList<PowerPicSelect>("SelectPowerPicSelectList", ppsn);

            foreach (PowerPicSelect pps in liss)
            {
                foreach (DataRow rw in dt.Rows)
                {
                    if (pps.PicSelectID == rw["A"].ToString())
                        rw["C"] = true;
                }
            }



            //FrmPicTypeSelect fpt = new FrmPicTypeSelect();
            FrmPicTreeSelect fpt = new FrmPicTreeSelect();
            fpt.DT = dt;
            if (fpt.ShowDialog() == DialogResult.OK)
            {
                dt = fpt.DT;

                int c = 0;
                this.ctrlSubstation_Info1.GridView.GroupPanelText += "你选择的图层";
                foreach (PowerPicSelect pps1 in liss)
                {
                    c = 0;
                    foreach (DataRow rw in dt.Rows)
                    {
                       
                        if (pps1.PicSelectID == rw["A"].ToString() && (bool)rw["C"])
                        { c = 1; 
                           this.ctrlSubstation_Info1.GridView.GroupPanelText+="，"+rw["B"] .ToString();
                        }
                    }
                    if (c == 0)
                    {
                        UCDeviceBase.DataService.Delete<PowerPicSelect>(pps1);
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

                            UCDeviceBase.DataService.Create<PowerPicSelect>(pp3);
                        }
                    }
                }
            }


            InitSodata2();
            InitGridData();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MsgBox.ShowYesNo("清除数据以后无法恢复，你确定继续吗？") == DialogResult.No)
                return;
            UCDeviceBase.DataService.Update("DeleteSubstation_InfoByFlag", selectid);
            MsgBox.Show("清除成功！");
            //InitSodata2();
            if (leixing == "规划")
            {
                this.ctrlSubstation_Info1.RefreshData1();
                //this.ctrlSubstation_Info_TongLing1.RefreshData1();
            }
            else if (leixing == "运行")
            {
                this.ctrlSubstation_Info1.RefreshData1();
                //this.ctrlSubstation_Info_TongLing1.RefreshData1();
            }
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barRel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            for (int i = 0; i < this.ctrlSubstation_Info1.GridView.RowCount;i++ )
            {
                Substation_Info trans = this.ctrlSubstation_Info1.GridView.GetRow(i) as Substation_Info;
                int num=Convert.ToInt32(trans.L3) ;
                double fuhepercent=Convert.ToDouble(trans.L10);
                bool flag = false;
                int zeroflag = 0;                //如果主变台数为零时给个提醒
                switch (num)
                {
                    case 0:
                        {
                            zeroflag = 1;
                            break;
                        }
                    case 1:
                        {
                            zeroflag = 2;
                            break;
                        }
                    case 2:
                        {
                            if (fuhepercent>=65)
                            {
                                flag = true;
                            }
                            break;
                        }
                        case 3:
                        {
                            if (fuhepercent>=87)
                            {
                                flag = true;
                            }
                            break;
                        }
                    case 4:
                        {
                            if (fuhepercent >= 97)
                            {
                                flag = true;
                            }
                            break;
                        }
                  
                }
                if (flag)
                {
                    row_num.Add(i);
                    trans.S1 = "不合格";                  //此列是产生在分类管理中
                }
                else if (!flag&&zeroflag==0)
                {
                    trans.S1 = "合格";
                }
                else if (zeroflag==1)
                {
                    trans.S1 = "主变台数为零";
                }
                else if (zeroflag==2)
                {
                    trans.S1 = "此为单台主变";
                }
                UCDeviceBase.DataService.Update<Substation_Info>(trans);
            }

            foreach (GridBand gc in this.ctrlSubstation_Info1.GridView.Bands)
            {

                try
                {
                    if (gc.Columns[0].FieldName.Substring(0, 1) == "S")
                    {
                        gc.Visible = false;
                       
                    }
                   
                        if (gc.Columns[0].FieldName == "S1")
                        {
                            gc.Visible = true;
                            gc.Caption ="合格判断";
                            gc.Columns[0].Caption = "合格判断";
                            gc.Columns[0].Visible = true;
                            gc.Columns[0].OptionsColumn.ShowInCustomizationForm = true;

                        }
                   
                }
                catch { }
            }
            this.ctrlSubstation_Info1.kekaoflag = true;
            this.ctrlSubstation_Info1.row_num = row_num;
            //this.ctrlSubstation_Info1.RefreshData1();
            this.ctrlSubstation_Info1.RefreshData1(parentobj);
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