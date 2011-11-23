using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Stutistics;
using DevExpress.XtraGrid.Columns;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using System.Collections;
using DevExpress.XtraGrid.Views.BandedGrid;
using Itop.Common;
using System.IO;
using Itop.Domain.Stutistic;
using System.Xml;
using Itop.Domain.BaseData;
using Itop.Domain.Layouts;
using Itop.Client.Table;
//using Itop.Domain.Layouts;
using Itop.Domain.Graphics;

namespace Itop.Client.Table
{
    public partial class FrmLayoutLineInfo : Itop.Client.Base.FormBase
    {
        PSPDEV psdevlin = new PSPDEV();

        private string ProjectID=Itop.Client.MIS.ProgUID;


        private string type = "2";  //线路
        private string type2 = "2";  //线路
        private string flag = "2";  //现状
        private string leixing = "规划";

        string title = "";
        string unit = "";
        bool isSelect = false;

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
        public void PSDEV_line()
        {
            string constr = " ProjectID='" + ProjectID + "' and Type='05'";
            
        }


        public void Frm220KV()
        {
            splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            selectid = "10001";
            selectname = "现状表";
            leixing = "运行";
            flag = "1";
            type = selectid;
            barSubItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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
            ctrlLine_Info1.ProjectID = ProjectUID;
            type2 = "220";
            type = selectid;
            this.ctrlLine_Info1.FlagsType = "";
            if (System.Configuration.ConfigurationSettings.AppSettings["LangFang"].ToString() == "廊坊")
            {
                ctrlLine_Info1.gridBand2.Caption = "地线型号";
            }
            if (isSelect)
            {
                //this.barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {

                this.Show();
                InitGridData();
            }
        }
      


        public void Frm66KV()
        {
            splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            selectid = "1";
            selectname = "现状表";
            leixing = "运行";
            flag = "1";
            type = selectid;
            barSubItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            string temp = "";
            foreach (GridBand gc in this.ctrlLine_Info1.GridView.Bands)
            {
                try
                {
                    if (gc.Columns[0].FieldName.Substring(0, 1) == "K" || (gc.Columns[0].FieldName.Substring(0, 1) == "S" && gc.Columns[0].FieldName != "S2"))
                    {
                        gc.Visible = false;
                    }
                    else
                    { 
                        temp = gc.Columns[0].Caption; 
                    }
                    gc.Caption = gc.Columns[0].Caption;
                    gc.Width = 100;
                    gc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                }
                catch { }

            }

            type2 = "66";
            this.ctrlLine_Info1.FlagsType = "";
            this.ctrlLine_Info1.gridBand17.Visible = false;
            ctrlLine_Info1.ProjectID = ProjectUID;
            this.ctrlLine_Info1.GridView.Bands[31].Visible = false;
           // this.ctrlLine_Info1.gridBand33.Visible = false;
          //  this.ctrlLine_Info1.gridBand34.Visible = false;
           // this.ctrlLine_Info1.gridBand35.Visible = false;
            //if (System.Configuration.ConfigurationSettings.AppSettings["LangFang"].ToString() == "廊坊")
            //{
            //    ctrlLine_Info1.gridBand2.Caption = "地线型号";
            //    ctrlLine_Info1.bandedGridColumn41.Caption = "地线型号";
            //    ctrlLine_Info1.gridBand5.Visible = false;
            //    ctrlLine_Info1.gridBand6.Visible = false;
            //}

            if (isSelect)
            {
                //this.barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {

                this.Show();
                InitGridData();
            }
        }

        
        public void Frm66KVLayout()
        {
            barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ctrlLayoutList1.GridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(GridView_FocusedRowChanged);
            this.ctrlLayoutList1.GridView.DoubleClick += new EventHandler(GridView_DoubleClick);
            flag = "2";
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
            ctrlLine_Info1.ProjectID = ProjectUID;
            this.ctrlLine_Info1.gridBand17.Visible = false;
            if (System.Configuration.ConfigurationSettings.AppSettings["LangFang"].ToString() == "廊坊") 
            {
                ctrlLine_Info1.gridBand2.Caption = "地线型号";
                ctrlLine_Info1.gridBand5.Visible = false;
                ctrlLine_Info1.gridBand6.Visible = false;
            }

            if (isSelect)
            {
                //this.barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                this.Show();
                this.ctrlLayoutList1.Type = "1";
                this.ctrlLayoutList1.RefreshData1();
            }
        }


        public void Frm10KV()
        {
            splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            selectid = "1";
            selectname = "现状表";
            leixing = "运行";
            flag = "1";
            barSubItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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
            type2 = "10";
            type = selectid;
            this.ctrlLine_Info1.FlagsType = "";
            ctrlLine_Info1.ProjectID = ProjectUID;
            if (System.Configuration.ConfigurationSettings.AppSettings["LangFang"].ToString() == "廊坊")
            {
                ctrlLine_Info1.gridBand2.Caption = "地线型号";
            }
            if (isSelect)
            {
                //this.barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                
                this.Show();
                InitGridData();
            }
        }









        public void Frm10KVLayout()
        {
            flag = "2";
            this.ctrlLayoutList1.GridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(GridView_FocusedRowChanged);
            this.ctrlLayoutList1.GridView.DoubleClick += new EventHandler(GridView_DoubleClick);
            barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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
            type2 = "10";
            ctrlLine_Info1.ProjectID = ProjectUID;
            if (System.Configuration.ConfigurationSettings.AppSettings["LangFang"].ToString() == "廊坊") 
            {
                ctrlLine_Info1.gridBand2.Caption = "地线型号";
            }
            if (isSelect)
            {
                //this.barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                this.Show();
                this.ctrlLayoutList1.Type = "1";
                this.ctrlLayoutList1.RefreshData1();
            }
        }



        public FrmLayoutLineInfo()
        {
            InitializeComponent();
        }

        private void FrmLayoutLineInfo_Load(object sender, EventArgs e)
        {
            InitVisuble();

            if (!isSelect)
                barSelect.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            InitRight();
        }
        private void InitRight()
        {
            if (!AddRight)
            {
                barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barAdd1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }

            if (!EditRight)
            {
                barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barEdit1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                ctrlLayoutList1.editright = false;
                ctrlLine_Info1.editright = false;
            }

            if (!DeleteRight)
            {
                barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barDel1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem11.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!AddRight && !EditRight)
            {
                barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }
            if (!PrintRight)
            {
                barSubItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }

        }

        void GridView_DoubleClick(object sender, EventArgs e)
        {
            InitData();
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
            selectid = ll.UID;
            type = selectid;
            selectname = ll.ListName;
            this.ctrlLine_Info1.FlagsType = selectid;
            InitSodata1();
            InitGridData();
            InitValues();
            this.ctrlLine_Info1.RefreshLayout();
        }

        private void InitGridData()
        {
            this.ctrlLine_Info1.GridView.GroupPanelText = selectname;
            this.ctrlLine_Info1.Type2 = type2;
            this.ctrlLine_Info1.ProjectID = ProjectUID;
            this.ctrlLine_Info1.Flag = flag;
            this.ctrlLine_Info1.RefreshData(selectid);
            
        }
        private void InitValues()
        {
            PowerSubstationLine psl = new PowerSubstationLine();
            psl.Type = type;
            psl.Flag= flag;
            psl.Type2= type2;
            if (leixing != "运行")
            {
                LayoutList ll = this.ctrlLayoutList1.FocusedObject;
                if (ll == null)
                    return;
                psl.Type = ll.UID;

            }



            IList<PowerSubstationLine> li = Itop.Client.Common.Services.BaseService.GetList<PowerSubstationLine>("SelectPowerSubstationLineByFlagType", psl);


            foreach (GridBand gb in this.ctrlLine_Info1.GridView.Bands)
            {
                try
                {
                    GridColumn gc = gb.Columns[0];
                    if (gc.FieldName.Substring(0, 1) == "S")
                    {
                        gb.Visible = false;
                        foreach (PowerSubstationLine pss in li)
                        {

                            if (gc.FieldName == pss.ClassType)
                            {
                                gc.Visible = true;
                                gc.Caption = pss.Title;
                                gb.Caption = pss.Title;
                                //gc.OptionsColumn.ShowInCustomizationForm = true;

                            }
                        }
                    }
                }
                catch { }
            }


            //foreach (GridColumn gc in this.ctrlLine_Info1.GridView.Columns)
            //{
            //    try
            //    {
            //        if (gc.FieldName.Substring(0, 1) == "S")
            //        {
            //            gc.Visible = false;
            //            foreach (PowerSubstationLine pss in li)
            //            {

            //                if (gc.FieldName == pss.ClassType)
            //                {
            //                    gc.Visible = true;
            //                    gc.Caption = pss.Title;
            //                    gc.Caption = pss.Title;
            //                    gc.Visible = true;
            //                    gc.OptionsColumn.ShowInCustomizationForm = true;

            //                }
            //            }
            //        }
            //    }
            //    catch { }
            //}
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlLayoutList1.AddObject();
            InitData();
        }

        private void barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlLayoutList1.UpdateObject();
            InitData();
        }

        private void barDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlLayoutList1.DeleteObject();
        }

        private void barAdd1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlLine_Info1.AddObject(selectid);
        }

        private void barEdit1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlLine_Info1.UpdateObject(selectid);
        }

        private void barDel1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlLine_Info1.DeleteObject(selectid);
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
            ////if (ctrlLayoutList1.FocusedObject == null)
            ////    return;

            ////string sid = ctrlLayoutList1.FocusedObject.UID;

            string sid = selectid;

            if (sid == "")
                return;

            Hashtable hs = new Hashtable();
            Hashtable hs2 = new Hashtable();

            LineInfo lia1 = new LineInfo();
            lia1.LineName = "SELECT "
      +"t1.uid as UID, "
      +"EleID, "
      +"LineName, "
      +"Length, "
      +"LineType, "
      +"Voltage, "
      + "t2.PicSelectName as SvgUID, "
      +"LayerID, "
      +"ObligateField1, "
      +"ObligateField2, "
      +"ObligateField3, "
      +"ObligateField4, "
      + "ObligateField5,ObligateField6,ObligateField7 "
      + "FROM LineInfo t1,PSP_PowerPicSelect t2 where t1.LayerID=t2.PicSelectID and t2.EachListID='"+sid+"' and t1.ObligateField1='"+leixing+"'";

            IList<LineInfo> listLineInfo = Services.BaseService.GetList<LineInfo>("SelectLineInfoByWhere1", lia1);

            if (leixing == "运行")
            {
                IList<LineInfo> lla = new List<LineInfo>();
                Hashtable hsss1 = new Hashtable();
                foreach (LineInfo l1 in listLineInfo)
                {
                    if (!hsss1.ContainsKey(l1.LineName))
                    {
                        lla = new List<LineInfo>();
                        lla.Add(l1);
                        hsss1.Add(l1.LineName, lla);
                    }
                    else
                    {
                        IList<LineInfo> llb = (IList<LineInfo>)hsss1[l1.LineName];
                        llb.Add(l1);
                        hsss1[l1.LineName] = llb;
                    }
                }
                foreach (DictionaryEntry de1 in hsss1)
                {
                    string uuid=Guid.NewGuid().ToString();
                    int sff=10000;
                    IList<LineInfo> llc = (IList<LineInfo>)de1.Value;
                    foreach (LineInfo l2 in llc)
                    {
                        string yearss = l2.SvgUID;
                        int years1 = 0;
                        int years2 = DateTime.Now.Year-1;
                        try
                        {
                            years1 = int.Parse(yearss.Substring(0, 4));
                        }
                        catch { continue; }
                        if (Math.Abs(years2 - years1) <= sff)
                        {
                            sff = Math.Abs(years2 - years1);
                            if (hs.ContainsKey(uuid))
                            {
                                hs[uuid] = l2.UID;

                            }
                            else
                            {

                                hs.Add(uuid, l2.UID);
                            }
                        }
                    }
                }

            }
            else
            {

                foreach (LineInfo l1 in listLineInfo)
                {
                    hs.Add(Guid.NewGuid().ToString(), l1.UID);
                }
            }

            IList<Line_Info> ll1 = Common.Services.BaseService.GetList<Line_Info>("SelectLine_InfoByFlag", sid);
            foreach (Line_Info ps in ll1)
            {
                if(ps.Code!="")
                hs2.Add(ps.Code,ps.UID);
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
                if (hs.ContainsValue(l2.UID) && l2.Voltage != "")
                {

                    Line_Info line1 = new Line_Info();
                    if (hs2.ContainsKey(l2.UID))
                        line1 = Services.BaseService.GetOneByKey<Line_Info>(hs2[l2.UID].ToString());



                    line1.Title = l2.LineName;
                    line1.Code = l2.UID;
                    line1.Flag = sid;
                    if (System.Configuration.ConfigurationSettings.AppSettings["LangFang"].ToString() == "廊坊")
                    {
                        line1.AreaName = l2.ObligateField5;//此时为底线型号
                    }
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
                    line1.AreaID = ProjectUID;
                    try
                    {
                        line1.L6 = l2.ObligateField3;//投产年限
                    }
                    catch { line1.L6 = null; }


                    try
                    {
                        line1.L5 = double.Parse(l2.Length);
                        line1.K5 = double.Parse(l2.Length);
                    }
                    catch { }

                    try
                    {
                        if (!hs2.ContainsKey(l2.UID))
                        {
                            Services.BaseService.Create<Line_Info>(line1);
                        }
                        else
                        {
                            //line1.UID = hs2[l2.UID].ToString();
                            Services.BaseService.Update<Line_Info>(line1);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Console.Write(ex.Message);
                    }

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
                            substation1.L2 = double.Parse(s2.Number.ToString());
                        }
                        catch { }
                        substation1.L6 = s2.ObligateField5;
                        substation1.AreaName = s2.ObligateField6;
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
          //  this.ctrlLine_Info1.FlagsType = type;
            if (leixing != "运行")
            {
                LayoutList ll = this.ctrlLayoutList1.FocusedObject;
                if (ll == null)
                    return;

                psl.Type = ll.UID;
                //this.ctrlLine_Info1.FlagsType = ll.UID;
                type = ll.UID;
            }
            IList<PowerSubstationLine> li = Itop.Client.Common.Services.BaseService.GetList<PowerSubstationLine>("SelectPowerSubstationLineByFlagType", psl);


            foreach (GridBand gc in this.ctrlLine_Info1.GridView.Bands)
            {
                try
                {
                    if ((gc.Columns[0].FieldName.Substring(0, 1) == "S" && gc.Columns[0].FieldName != "S2") || gc.Columns[0].FieldName.Substring(0, 1) == "K")
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
            this.ctrlLine_Info1.Type = type;
            this.ctrlLine_Info1.Flag = flag;
            this.ctrlLine_Info1.Type2 = type2;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FrmNewClass frm = new FrmNewClass();
                frm.Type = type;
                frm.Flag = flag;
                frm.Type2 = type2;
                if (leixing != "运行")
                {
                    LayoutList ll = this.ctrlLayoutList1.FocusedObject;
                    if (ll == null)
                        return;
                    //frm.Flag = ll.UID;
                    frm.Type=ll.UID; ;
                }
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    InitVisuble();
                    SaveLayoutToXml();
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
            if (leixing != "运行")
            {
                LayoutList ll = this.ctrlLayoutList1.FocusedObject;
                if (ll == null)
                    return;

                frm.Type = ll.UID; ;
            }
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
            string title = "";
            title = gc.Caption;
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
            if (leixing != "运行")
            {
                LayoutList ll = this.ctrlLayoutList1.FocusedObject;
                if (ll == null)
                    return;
                //frm.Flag = ll.UID;
                psl.Type = ll.UID; ;
            }
            psl.Title = gc.Caption;
            psl.Title = title;
            psl.Type2 = type2;
            Itop.Client.Common.Services.BaseService.Update("DeletePowerSubstationLineByAll", psl);
            SaveLayoutToXml();
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
            //ctrlLine_Info1.GridView.Bands["gridBand2"].Visible = false;
            //ctrlLine_Info1.GridView.Bands["gridBand5"].Visible = false;
            //ctrlLine_Info1.GridView.Bands["gridBand6"].Visible = false;
            //ctrlLine_Info1.GridView.Bands["gridBand7"].Visible = false;
            //ctrlLine_Info1.GridView.Bands["gridBand10"].Visible = false;
            //FileClass.ExportExcel(this.ctrlLine_Info1.GridControl);
            //ctrlLine_Info1.GridView.Bands["gridBand2"].Visible = true;
            //ctrlLine_Info1.GridView.Bands["gridBand5"].Visible = true;
            //ctrlLine_Info1.GridView.Bands["gridBand6"].Visible = true;
            //ctrlLine_Info1.GridView.Bands["gridBand7"].Visible = true;
            //ctrlLine_Info1.GridView.Bands["gridBand10"].Visible = true;
            //FileClass.ExportExcel(this.ctrlLine_Info1.GridView.GroupPanelText, "", this.ctrlLine_Info1.GridControl);
            IList<string> col = new List<string>();
            IList<string> area = new List<string>();
            FrmOutput frm = new FrmOutput();
            AddCol(ref col);
            frm.Column = col; frm.Area = area;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < ctrlLine_Info1.GridView.Bands.Count; i++)
                {
                    for (int j = 0; j < frm.Column.Count; j++)
                    {
                        if (frm.Column[j] == ctrlLine_Info1.GridView.Bands[i].Caption)
                            ctrlLine_Info1.GridView.Bands[i].Visible = false;
                    }
                }
                string conn = " and (";
                for (int j = 0; j < frm.VolLev.Count; j++)
                {
                    if (j == frm.VolLev.Count - 1)
                        conn += "L1=" + frm.VolLev[j] + ")";
                    else
                        conn += "L1=" + frm.VolLev[j] + " or ";
                }
                if (frm.Area.Count > 0)
                    conn += " and (";
                for (int i = 0; i < frm.Area.Count; i++)
                {

                    if (frm.Area[i] == "空")
                        frm.Area[i] = "";
                    if (i == frm.Area.Count - 1)
                        conn += "AreaName='" + frm.Area[i] + "')";
                    else
                    {
                        conn += "AreaName='" + frm.Area[i] + "' or ";
                    }
                }
                this.ctrlLine_Info1.RefreshData(selectid,conn);
                FileClass.ExportExcel(this.ctrlLine_Info1.GridControl);
                for (int i = 0; i < ctrlLine_Info1.GridView.Bands.Count; i++)
                {
                    ctrlLine_Info1.GridView.Bands[i].Visible = true;
                }
                this.ctrlLine_Info1.RefreshData(selectid);
            }
        }
        public void AddCol(ref IList<string> col)
        {
            for (int i = 0; i < ctrlLine_Info1.GridView.Bands.Count; i++)
            {
                if(ctrlLine_Info1.GridView.Bands[i].Visible==true)
                col.Add(ctrlLine_Info1.GridView.Bands[i].Caption);
            }
        }
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (ctrlLayoutList1.FocusedObject == null)
            //{
            //    MsgBox.Show("没有记录存在，不可以保存样式！");
            //    return;
            //}
            string filepath1 = Path.GetTempPath();
            switch (type2)
            {
                case "66":
                    if (leixing != "运行")
                    {
                        LayoutList ll = this.ctrlLayoutList1.FocusedObject;
                        if (ll == null)
                            return;


                        //this.ctrlLine_Info1.bandedGridView2.SaveLayoutToXml(filepath1 +ll.UID +"RefreshDatalayout1.xml");
                        MemoryStream ms = new MemoryStream();
                        this.ctrlLine_Info1.bandedGridView2.SaveLayoutToStream(ms);
                        Econ ec = new Econ();
                        ec.UID = ll.UID+"RefreshDatalayout1";
                        ec.ExcelData = ms.ToArray();
                        IList<Econ> list = Services.BaseService.GetList<Econ>("SelectEconByKey", ec);
                        if (list.Count == 0)
                            Services.BaseService.Create<Econ>(ec);
                        else
                            Services.BaseService.Update<Econ>(ec);
                    }
                    else
                    {
                        //this.ctrlLine_Info1.bandedGridView2.SaveLayoutToXml(filepath1 + "RefreshDatalayout1.xml");
                        MemoryStream ms = new MemoryStream();
                        this.ctrlLine_Info1.bandedGridView2.SaveLayoutToStream(ms);
                        Econ ec = new Econ();
                        ec.UID = "RefreshDatalayout1";
                        ec.ExcelData = ms.ToArray();
                        IList<Econ> list = Services.BaseService.GetList<Econ>("SelectEconByKey", ec);
                        if (list.Count == 0)
                            Services.BaseService.Create<Econ>(ec);
                        else
                            Services.BaseService.Update<Econ>(ec);
                    }
                    break;

                case "10":
                    if (leixing != "运行")
                    {
                        LayoutList ll = this.ctrlLayoutList1.FocusedObject;
                        if (ll == null)
                            return;


                        //this.ctrlLine_Info1.bandedGridView2.SaveLayoutToXml(filepath1 + ll.UID + "RefreshDatalayout2.xml");
                        MemoryStream ms = new MemoryStream();
                        this.ctrlLine_Info1.bandedGridView2.SaveLayoutToStream(ms);
                        Econ ec = new Econ();
                        ec.UID = ll.UID+ "RefreshDatalayout2";
                        ec.ExcelData = ms.ToArray();
                        IList<Econ> list = Services.BaseService.GetList<Econ>("SelectEconByKey", ec);
                        if (list.Count == 0)
                            Services.BaseService.Create<Econ>(ec);
                        else
                            Services.BaseService.Update<Econ>(ec);
                    }
                    else
                    {
                        //this.ctrlLine_Info1.bandedGridView2.SaveLayoutToXml(filepath1 + "RefreshDatalayout2.xml");
                        MemoryStream ms = new MemoryStream();
                        this.ctrlLine_Info1.bandedGridView2.SaveLayoutToStream(ms);
                        Econ ec = new Econ();
                        ec.UID = "RefreshDatalayout2";
                        ec.ExcelData = ms.ToArray();
                        IList<Econ> list = Services.BaseService.GetList<Econ>("SelectEconByKey", ec);
                        if (list.Count == 0)
                            Services.BaseService.Create<Econ>(ec);
                        else
                            Services.BaseService.Update<Econ>(ec);
                    }
                    break;
            }
        }
        private void SaveLayoutToXml()
        {
            string filepath1 = Path.GetTempPath();
            switch (type2)
            {
                case "66":
                    if (leixing != "运行")
                    {
                        LayoutList ll = this.ctrlLayoutList1.FocusedObject;
                        if (ll == null)
                            return;


                        //this.ctrlLine_Info1.bandedGridView2.SaveLayoutToXml(filepath1 +ll.UID +"RefreshDatalayout1.xml");
                        MemoryStream ms = new MemoryStream();
                        this.ctrlLine_Info1.bandedGridView2.SaveLayoutToStream(ms);
                        Econ ec = new Econ();
                        ec.UID = ll.UID  + "RefreshDatalayout1";
                        ec.ExcelData = ms.ToArray();
                        IList<Econ> list = Services.BaseService.GetList<Econ>("SelectEconByKey", ec);
                        if (list.Count == 0)
                            Services.BaseService.Create<Econ>(ec);
                        else
                            Services.BaseService.Update<Econ>(ec);
                    }
                    else
                    {
                        //this.ctrlLine_Info1.bandedGridView2.SaveLayoutToXml(filepath1 + "RefreshDatalayout1.xml");
                        MemoryStream ms = new MemoryStream();
                        this.ctrlLine_Info1.bandedGridView2.SaveLayoutToStream(ms);
                        Econ ec = new Econ();
                        ec.UID = "RefreshDatalayout1";
                        ec.ExcelData = ms.ToArray();
                        IList<Econ> list = Services.BaseService.GetList<Econ>("SelectEconByKey", ec);
                        if (list.Count == 0)
                            Services.BaseService.Create<Econ>(ec);
                        else
                            Services.BaseService.Update<Econ>(ec);
                    }
                    break;

                case "10":
                    if (leixing != "运行")
                    {
                        LayoutList ll = this.ctrlLayoutList1.FocusedObject;
                        if (ll == null)
                            return;


                        //this.ctrlLine_Info1.bandedGridView2.SaveLayoutToXml(filepath1 + ll.UID + "RefreshDatalayout2.xml");
                        MemoryStream ms = new MemoryStream();
                        this.ctrlLine_Info1.bandedGridView2.SaveLayoutToStream(ms);
                        Econ ec = new Econ();
                        ec.UID = ll.UID + "RefreshDatalayout2";
                        ec.ExcelData = ms.ToArray();
                        IList<Econ> list = Services.BaseService.GetList<Econ>("SelectEconByKey", ec);
                        if (list.Count == 0)
                            Services.BaseService.Create<Econ>(ec);
                        else
                            Services.BaseService.Update<Econ>(ec);
                    }
                    else
                    {
                        //this.ctrlLine_Info1.bandedGridView2.SaveLayoutToXml(filepath1 + "RefreshDatalayout2.xml");
                        MemoryStream ms = new MemoryStream();
                        this.ctrlLine_Info1.bandedGridView2.SaveLayoutToStream(ms);
                        Econ ec = new Econ();
                        ec.UID = "RefreshDatalayout2";
                        ec.ExcelData = ms.ToArray();
                        IList<Econ> list = Services.BaseService.GetList<Econ>("SelectEconByKey", ec);
                        if (list.Count == 0)
                            Services.BaseService.Create<Econ>(ec);
                        else
                            Services.BaseService.Update<Econ>(ec);
                    }
                    break;
            }
        
        }

         private void InsertLineData1()
        {
            //LayoutList lla = this.ctrlLayoutList1.FocusedObject;
            //if (lla == null)
            //    return;

            if (selectid == "")
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
                    for (int i = 0; i < dts.Rows.Count; i++)
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
                        l1.Flag = selectid;
                        l1.CreateDate = s8.AddSeconds(i);
                        l1.AreaID = ProjectUID;
                        lii.Add(l1);
                    }

                    foreach (Line_Info lll in lii)
                    {
                        Line_Info l1 = new Line_Info();
                      //  l1.AreaName = lll.AreaName;
                        l1.Title = lll.Title;
                        l1.Flag = selectid;
                        l1.DY = lll.DY;
                        try
                        {
                            int tx = int.Parse(l1.L6);
                            if (tx > DateTime.Now.Year)
                                l1.Flag = "2";
                            else
                                l1.Flag = "1";
                        }
                        catch { l1.Flag = "1"; }
                        string conn = "Title='" + lll.Title + "' and Flag='" + l1.Flag + "' and DY='" + lll.DY + "'";
                        object obj = Services.BaseService.GetObject("SelectLine_InfoByConn", conn);
                        if (obj != null)
                        {
                            lll.UID = ((Line_Info)obj).UID;
                            lll.Code = ((Line_Info)obj).Code;
                            Services.BaseService.Update<Line_Info>(lll);
                        }
                        else
                        {
                            lll.UID += "|" + ProjectUID;
                            Services.BaseService.Create<Line_Info>(lll);
                        }
                    }
                    this.ctrlLine_Info1.RefreshData(selectid);
                }
            }
            catch (Exception ex) { MsgBox.Show(ex.Message); MsgBox.Show("导入格式不正确！"); }
         }

        private void InsertLineData2()
        {
            //LayoutList lla = this.ctrlLayoutList1.FocusedObject;
            //if (lla == null)
            //    return;
            if (selectid == "")
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
                    for (int i = 0; i < dts.Rows.Count; i++)
                    {
                        if (dts.Rows[i][1].ToString().IndexOf("合计") >= 0)
                            continue;

                        Line_Info l1 = new Line_Info();
                        foreach (DataColumn dc in dts.Columns)
                        {
                            columnname = dc.ColumnName;


                            switch (dc.ColumnName)
                            {
                                case "DY":
                                case "L1":
                                case "K1":
                                    int LL1 = 0;
                                    try
                                    {
                                        LL1 = Convert.ToInt32(dts.Rows[i][dc.ColumnName].ToString());
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
                        l1.Flag = selectid;
                        l1.CreateDate = s8.AddSeconds(i);
                        //l1.L6 = DateTime.Now;
                        lii.Add(l1);
                    }

                    foreach (Line_Info lll in lii)
                    {
                        Line_Info l1 = new Line_Info();
                        l1.AreaName = lll.AreaName;
                        l1.Title = lll.Title;
                        l1.Flag = selectid;
                        object obj = Services.BaseService.GetObject("SelectLine_InfoByNameFlag", l1);
                        if (obj != null)
                        {
                            lll.UID = ((Line_Info)obj).UID;
                            lll.Code = ((Line_Info)obj).Code;
                            Services.BaseService.Update<Line_Info>(lll);
                        }
                        else
                        {
                            Services.BaseService.Create<Line_Info>(lll);
                        }
                    }
                    this.ctrlLine_Info1.RefreshData(selectid);
                }
            }
            catch (Exception ex) { MsgBox.Show(columnname+ex.Message); MsgBox.Show("导入格式不正确！"); }
        }



        public double ChangeDou(string per)
        {
            string d = per.Substring(0, per.Length - 1);
            return double.Parse(d);
        }

        private DataTable GetExcel(string filepach)
        {
            //string str;
            //FarPoint.Win.Spread.FpSpread fpSpread1 = new FarPoint.Win.Spread.FpSpread();

            //try
            //{
            //    fpSpread1.OpenExcel(filepach);
            //}
            //catch
            //{
            //    string filepath1 = Path.GetTempPath() + "\\" + Path.GetFileName(filepach);
            //    File.Copy(filepach, filepath1);
            //    fpSpread1.OpenExcel(filepath1);
            //    File.Delete(filepath1);
            //}
            //DataTable dt = new DataTable();
            //Hashtable h1 = new Hashtable();
            //int aa = 0;
            //string[] col = new string[7] { "S2", "Title", "L1","L4", "L5","S4","S5" };
            //int[] colnum = new int[4] { 0, 1, 3, 4 };//导出的列
            //int c = 0;
            ////for (int k = 1; k <= fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++)
            ////{
            ////    bool bl = false;
            ////    GridColumn gc = this.ctrlSubstation_Info1.GridView.VisibleColumns[k - 1];
            ////    dt.Columns.Add(gc.FieldName);
            ////    h1.Add(aa.ToString(), gc.FieldName);
            ////    aa++;
            ////}
            //for (int k = 0; k < col.Length; k++)
            //{
            //    dt.Columns.Add(col[k]);
            //}
            //string[] cnNum = new string[9] { "一", "二", "三", "四", "五","六","七","八","九"};
            //int cn = 0;
            //int gong = 65;
            //int m = 2;
            //string L1 = "";
            //string S4 = "";
            //string L2 = "";
            //string AreaName = "";
            //string temStr = "";
            //// string L
            //if (fpSpread1.Sheets[0].Cells[1, 0].Text != "序号" && fpSpread1.Sheets[0].Cells[1, 0].Text != "")
            //    m = 1;
            //for (int i = m; i <= fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data); i++)
            //{
            //    c = 0;
            //    if (fpSpread1.Sheets[0].Cells[i, 0].Text.IndexOf(cnNum[cn])!=-1)
            //    {
            //        GetL1S4(fpSpread1.Sheets[0].Cells[i, 1].Text, out L1, out S4);
            //       // AreaName = "";
            //       // gong = 65;
            //        cn++;
            //        continue;
            //    }
            //    //else if ((temStr = fpSpread1.Sheets[0].Cells[i, 0].Text.Replace(" ", "")) == Convert.ToChar(gong).ToString().ToLower())
            //    //{
            //    //    AreaName = fpSpread1.Sheets[0].Cells[i, 1].Text;
            //    //    gong++;
            //    //    continue;
            //    //}
            //    DataRow dr = dt.NewRow();
            //    str = "";
            //    for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
            //    {
            //        str = str + fpSpread1.Sheets[0].Cells[i, j].Text;
            //        dr[col[colnum[c]]] = fpSpread1.Sheets[0].Cells[i, j].Text;
            //        c++;
            //    }
            //   // GetL2(dr["L4"].ToString(), out L2);
            //    dr["L1"] = L1; dr["S4"] = S4;  dr["S2"] = ""; dr["S5"] = i.ToString();
            //    if (str != "")
            //        dt.Rows.Add(dr);
            //}
            //return dt;
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
            IList<string> filedList = new List<string>();
            IList<string> capList = new List<string>();
            for (int i = 0; i < ctrlLine_Info1.GridView.Bands.Count; i++)
            {
                if (ctrlLine_Info1.GridView.Bands[i].Children.Count > 0)
                {
                    for (int j = 0; j < ctrlLine_Info1.GridView.Bands[i].Children.Count; j++)
                    {
                        capList.Add(ctrlLine_Info1.GridView.Bands[i].Children[j].Caption);
                        filedList.Add(ctrlLine_Info1.GridView.Bands[i].Children[j].Columns[0].FieldName);
                    }
                    continue;
                }
                capList.Add(ctrlLine_Info1.GridView.Bands[i].Caption);
                filedList.Add(ctrlLine_Info1.GridView.Bands[i].Columns[0].FieldName);
            }
            int[] colnum = new int[7] { 0, 2, 6, 8, 9, 7, 11 };//导出的列
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
            int cn = 0;
            int gong = 65;
            int m = 1;
            string L1 = "";
            string S4 = "";
            string L2 = "";
            string AreaName = "";
            string temStr = "";
            for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
            {
                if (capList.Contains(fpSpread1.Sheets[0].Cells[0, j].Text))
                    fie.Add(filedList[capList.IndexOf(fpSpread1.Sheets[0].Cells[0, j].Text)]);
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
                    
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public void GetL1S4(string text, out string L1, out string S4)
        {
            if (text.IndexOf("500千伏") != -1)
            { L1 = "500"; S4 = ""; }
            else if (text.IndexOf("220千伏") != -1)
            { L1 = "220"; S4 = ""; }
            else if (text.IndexOf("110千伏公") != -1)
            { L1 = "110"; S4 = "公用"; }
            else if (text.IndexOf("110千伏专") != -1)
            { L1 = "110"; S4 = "专用"; }
            else if (text.IndexOf("35千伏") != -1)
            { L1 = "35"; S4 = ""; }
            else
            { L1 = ""; S4 = ""; }
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

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string str="";

            try
            {
                str = System.Configuration.ConfigurationSettings.AppSettings["SvgID"];

            }
            catch { }

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
                            pp3.PicSelectName = rw1["B"].ToString();

                            Services.BaseService.Create<PowerPicSelect>(pp3);
                        }
                    }
                }
                
            }

            InitSodata1();
            
            InitGridData();



        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MsgBox.ShowYesNo("该操作将清除所有数据，清除数据以后无法恢复,可能对其他用户的数据产生影响，请谨慎操作，你确定继续吗？") == DialogResult.No)
                return;
            string conn = "AreaID='"+ProjectUID+"'";
            Services.BaseService.Update("DeleteLine_InfoByConn",conn );
            MsgBox.Show("清除成功！");
            //InitSodata2();
            //if (leixing == "规划")
            //{
                this.ctrlLine_Info1.RefreshData(selectid);
            //    //this.ctrlSubstation_Info_TongLing1.RefreshData1();
            //}
            //else if (leixing == "运行")
            //{
            //    this.ctrlLine_Info1.RefreshData(selectid);
            //    //this.ctrlSubstation_Info_TongLing1.RefreshData1();
            //}
        }

        private void barButtonItem10_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCopy frm = new FrmCopy();
            frm.AssName = "Itop.Domain.Stutistics";
            frm.idName = "UID";
            frm.projectName = "AreaID";
            frm.bPare = false;
            frm.CurID = ProjectUID;
            frm.ClassName = "Itop.Domain.Stutistic.Line_Info";
            frm.SelectString = "SelectLine_InfoByConn";
            frm.InsertString = "InsertLine_Info";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("导入成功");
                this.ctrlLine_Info1.RefreshData(selectid);
               // this.ctrlLine_Info1.CalcTotal();
            }
        }
    }
}