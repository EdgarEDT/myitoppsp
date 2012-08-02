using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Itop.Common;
using Itop.Client.Base;
using System.Collections;
using System.Reflection;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Itop.Client.Common;
using Itop.Domain.Forecast;
using Itop.Domain.HistoryValue;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using Dundas.Charting.WinControl;
using Itop.Client.Using;
using Itop.Domain.Graphics;
using ItopVector.Tools;

namespace Itop.Client.Forecast.FormAlgorithm_New {
    public partial class FormSpatialforcast : FormBase {
        int type = 16;
        DataTable dataTable = new DataTable();
        private Ps_forecast_list forecastReport = null;
        private PublicFunction m_pf = new PublicFunction();
        IList<Ps_Calc> list1 = new List<Ps_Calc>();
        Ps_Calc pc1 = new Ps_Calc();
        bool bLoadingData = false;
        bool _canEdit = true;
        string firstyear = "0";
        string endyear = "0";
        bool selectdral = true;

        public bool CanEdit {
            get { return _canEdit; }
            set { _canEdit = value; EditRight = value; }
        }

        private bool EditRight = false;
        public FormSpatialforcast(Ps_forecast_list fr) {
        
            InitializeComponent();
            forecastReport = fr;
            Text = fr.Title;

            barButtonItem1.Glyph = Itop.ICON.Resource.新建; 
            barButtonItem2.Glyph = Itop.ICON.Resource.修改;
            barButtonItem5.Glyph = Itop.ICON.Resource.删除;
           
            barSubItem1.Glyph = Itop.ICON.Resource.发送;

            barButtonItem3.Glyph = Itop.ICON.Resource.发送;
            barButtonItem8.Glyph = Itop.ICON.Resource.工具;

            barButtonItem4.Glyph = Itop.ICON.Resource.审批;
            barButtonItem7.Glyph = Itop.ICON.Resource.保存;

            barButtonItem6.Glyph = Itop.ICON.Resource.关闭;
            chart_user1.SetColor += new chart_userSH.setcolor(chart_user1_SetColor);
        }

        void chart_user1_SetColor()
        {
            FormColor fc = new FormColor();
            fc.DT = dataTable;
            fc.ID = forecastReport.ID.ToString();
            fc.For = type;
            if (fc.ShowDialog() == DialogResult.OK)
                RefreshChart();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.Close();
        }

        private void FormSpatialforcast_Load(object sender, EventArgs e) {
           // HideToolBarButton();
            //chart1.Series.Clear();
            //Show();
            Application.DoEvents();
            //this.Cursor = Cursors.WaitCursor;
            //treeList1.BeginUpdate();
            LoadData();
            //treeList1.EndUpdate();
            RefreshChart();
            //this.Cursor = Cursors.Default;



            Ps_Forecast_Setup pfs = new Ps_Forecast_Setup();
            pfs.Forecast = type;
            pfs.ForecastID = forecastReport.ID;
            pfs.StartYear = int.Parse(firstyear.Replace("y", ""));
            pfs.EndYear = int.Parse(endyear.Replace("y", ""));

            IList<Ps_Forecast_Setup> li = Services.BaseService.GetList<Ps_Forecast_Setup>("SelectPs_Forecast_SetupByForecast", pfs);

            if (li.Count != 0) {
                if ((li[0].StartYear >= forecastReport.StartYear && forecastReport.EndYear >= li[0].EndYear)) {
                    firstyear = li[0].StartYear.ToString();
                    endyear = li[0].EndYear.ToString();
                } else {
                    endyear = "0";
                    firstyear = "0";
                }
            }
        }
        List<string> VisitColumnyear = new List<string>();
        private void LoadData() {
            //this.splitContainerControl1.SplitterPosition = (2 * this.splitterControl1.Width) / 3;
            //this.splitContainerControl2.SplitterPosition = splitContainerControl2.Height / 2;
            treeList1.DataSource = null;
            VisitColumnyear.Clear();
            bLoadingData = true;
            if (dataTable != null) {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
            }
            AddFixColumn();
            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.ForecastID = forecastReport.ID;
            psp_Type.Forecast = type;
            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);

            

            for (int i = forecastReport.StartYear; i <= forecastReport.YcEndYear; i++) {
                AddColumn(i);
             
                //foreach (Ps_Forecast_Math pm in listTypes)
                //{
                //   if (Convert.ToDouble(pm.GetType().GetProperty("y" + i.ToString()).GetValue(pm, null))!=0)
                //   {
                //       VisitColumnyear.Add("y" + i.ToString());
                //      AddColumn(i);
                //   }
                    
                //}
                
            }
            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Forecast_Math));
            treeList1.DataSource = dataTable;

            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;

            Application.DoEvents();
            bLoadingData = false;
        }

        //添加固定列
        private void AddFixColumn() {
            TreeListColumn column = new TreeListColumn();
            column.FieldName = "Title";
            column.Caption = "区域";
            column.VisibleIndex = 0;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "Sort";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "ForecastID";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});

            column = new TreeListColumn();
            column.FieldName = "Forecast";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});

            column = new TreeListColumn();
            column.FieldName = "ID";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "ParentID";
            column.VisibleIndex = -1;

            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
        }
        //添加年份后，新增一列
        private void AddColumn(int year) {
            TreeListColumn column = new TreeListColumn();

            column.FieldName = "y" + year;
            column.Tag = year;
            column.Caption = year + "年";
            column.Name = year.ToString();
            column.Width = 70;
            //column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            column.VisibleIndex = year;//有两列隐藏列

            // 
            // repositoryItemTextEdit1
            //
            RepositoryItemTextEdit repositoryItemTextEdit1 = new RepositoryItemTextEdit();
            repositoryItemTextEdit1.AutoHeight = false;
            repositoryItemTextEdit1.DisplayFormat.FormatString = "n2";
            repositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            repositoryItemTextEdit1.Mask.EditMask = "n2";
            repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

            column.ColumnEdit = repositoryItemTextEdit1;
            //column.DisplayFormat.FormatString = "#####################0.##";
            //column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            column.Format.FormatString = "#####################0.##";
            column.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});

        }

        private void CopyBaseColor(FORBaseColor bc1, FORBaseColor bc2) {
            bc1.UID = bc2.UID;
            bc1.Title = bc2.Title;
            bc1.Remark = bc2.Remark;
            bc1.Color = bc2.Color;
            bc1.Color1 = ColorTranslator.FromOle(bc2.Color);
        }
        private void RefreshChart() {
            ArrayList ht = new ArrayList();
            ht.Add(Color.Red);
            ht.Add(Color.Blue);
            ht.Add(Color.Green);
            ht.Add(Color.Yellow);
            ht.Add(Color.HotPink);
            ht.Add(Color.LawnGreen);
            ht.Add(Color.Khaki);
            ht.Add(Color.LightSlateGray);
            ht.Add(Color.LightSeaGreen);
            ht.Add(Color.Lime);
            ht.Add(Color.Black);
            ht.Add(Color.Brown);
            ht.Add(Color.Crimson);
            int m = 0;
            IList<FORBaseColor> list = Services.BaseService.GetList<FORBaseColor>("SelectFORBaseColorByWhere", "Remark='" + this.forecastReport.ID.ToString() + "-" + type + "'");

            IList<FORBaseColor> li = new List<FORBaseColor>();
            bool bl = false;
            ArrayList aldatablr = new ArrayList();
            foreach (DataRow row in dataTable.Rows) {
                aldatablr.Add(row["ID"].ToString());
            }
            foreach (DataRow row in dataTable.Rows) {
                if (aldatablr.Contains(row["ParentID"].ToString()))
                    continue;
                bl = false;
                foreach (FORBaseColor bc in list) {
                    if (row["Title"].ToString() == bc.Title) {
                        bl = true;
                        FORBaseColor bc1 = new FORBaseColor();
                        bc1.Color1 = Color.Blue;
                        CopyBaseColor(bc1, bc);
                        li.Add(bc1);
                    }


                }
                if (!bl) {
                    FORBaseColor bc1 = new FORBaseColor();
                    bc1.UID = Guid.NewGuid().ToString();
                    bc1.Remark = this.forecastReport.ID.ToString() + "-" + type;
                    bc1.Title = row["Title"].ToString();
                    bc1.Color = 16711680;
                    if (m == 0) {
                        Random rd = new Random();
                        m = rd.Next(100);
                    }
                    Color cl = (Color)ht[m % 13];
                    bc1.Color = ColorTranslator.ToOle(cl);
                    bc1.Color1 = cl;
                    //bc1.Color1 = Color.Blue;
                    Services.BaseService.Create<FORBaseColor>(bc1);
                    li.Add(bc1);
                }
                m++;
            }
            ArrayList hs = new ArrayList();
            foreach (FORBaseColor bc2 in li) {
                hs.Add(bc2.Color1);
            }
            List<Ps_Forecast_Math> listValues = new List<Ps_Forecast_Math>();

            for (int i = 0; i < treeList1.Nodes.Count; i++) {
                TreeListNode row = treeList1.Nodes[i];
                foreach (TreeListColumn col in treeList1.Columns) {
                    if (col.FieldName.IndexOf("y") > -1) {
                        object obj = row[col.FieldName];
                        if (obj != DBNull.Value) {
                            Ps_Forecast_Math v = new Ps_Forecast_Math();
                            v.ForecastID = forecastReport.ID;
                            v.ID = (string)row["ID"];
                            v.Title = row["Title"].ToString();
                            v.Sort = Convert.ToInt32(col.FieldName.Replace("y", ""));
                            v.y1990 = (double)row[col.FieldName];

                            listValues.Add(v);
                        }
                    }
                }


            }

            this.chart_user1.RefreshChart(listValues, "Title", "Sort", "y1990", hs);
        }


        private InputLanguage oldInput = null;

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            FormColor fc = new FormColor();
            fc.DT = dataTable;
            fc.ID = forecastReport.ID.ToString();
            fc.For = type;
            if (fc.ShowDialog() == DialogResult.OK)
                RefreshChart();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            Save();
        }
        private void Save() {

            //保存

            foreach (DataRow dataRow in dataTable.Rows) {

                TreeListNode row = treeList1.FindNodeByKeyID(dataRow["ID"]);

                //for (int i = 0; i < this.treeList1.Nodes.Count; i++)
                //{
                //    TreeListNode row = this.treeList1.Nodes[i];
                Ps_Forecast_Math v = new Ps_Forecast_Math();
                v.ID = row["ID"].ToString();
                foreach (TreeListColumn col in this.treeList1.Columns) {
                    if (col.FieldName.IndexOf("y") > -1) {
                        object obj = row[col.FieldName];
                        if (obj != DBNull.Value) {
                            v.GetType().GetProperty(col.FieldName).SetValue(v, obj, null);
                        }
                    }
                }

                try {
                    Services.BaseService.Update("UpdatePs_Forecast_MathByID", v);

                } catch { }
            }
            MsgBox.Show("保存成功！");
        }

       

        private void barButtonItem3_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            FormResult fr = new FormResult();
            fr.LI = this.treeList1;
            fr.Text = forecastReport.Title;
            fr.ShowDialog();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "JPEG文件(*.jpg)|*.jpg|BMP文件(*.bmp)|*.bmp|PNG文件(*.png)|*.png";
            if (sf.ShowDialog() != DialogResult.OK)
                return;

            Dundas.Charting.WinControl.ChartImageFormat ci = new Dundas.Charting.WinControl.ChartImageFormat();
            switch (sf.FilterIndex) {
                case 0:
                    ci = Dundas.Charting.WinControl.ChartImageFormat.Jpeg;
                    break;

                case 1:
                    ci = Dundas.Charting.WinControl.ChartImageFormat.Bmp;
                    break;

                case 2:
                    ci = Dundas.Charting.WinControl.ChartImageFormat.Png;
                    break;



            }
            this.chart_user1.chart1.SaveAsImage(sf.FileName, ci);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            FrmAddspatialarea FA = new FrmAddspatialarea();
            FA.init();
            if (FA.ShowDialog()==DialogResult.OK)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    if (dr["Title"]==FA.Areatitle)
                    {
                        MessageBox.Show("已经存在此地区！请重新选。");
                        return;
                    }
                }
                glebeProperty gp = new glebeProperty();
                gp.ParentEleID = "0";
                gp.SvgUID = "c5ec3bc7-9706-4cbd-9b8b-632d3606f933";
                gp.ObligateField16 = FA.Areatitle;
                IList<glebeProperty> svglist = Services.BaseService.GetList<glebeProperty>("SelectglebePropertyByObligateField16", gp);
                if (svglist.Count>0)
                {
                    frmMainProperty f = new frmMainProperty();
                    f.IsReadonly = true;
                    f.InitData(svglist[0], "", "","");
                   
                    if (f.ShowDialog()==DialogResult.OK)
                    {
                        Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();

                        psp_Type.ID = Guid.NewGuid().ToString();

                        psp_Type.Forecast = type;
                        psp_Type.ForecastID = forecastReport.ID;

                        psp_Type.Title = FA.Areatitle;
                        glebeYearValue gy = new glebeYearValue();
               
                        IList<glebeYearValue> yearlist = Services.BaseService.GetList<glebeYearValue>("SelectglebeYearValueBywhere", "ParentID='" + svglist[0].UID + "'");
                        for (int i = 0; i < yearlist.Count;i++ ) {
                            string y = "y" + yearlist[i].Year.ToString();
                            psp_Type.GetType().GetProperty(y).SetValue(psp_Type, yearlist[i].Burthen, null);
                        }
                        object obj = Services.BaseService.GetObject("SelectPs_Forecast_MathMaxID", null);
                        if (obj != null)
                            psp_Type.Sort = ((int)obj) + 1;
                        else
                            psp_Type.Sort = 1;

                        try {
                            Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type);
                            //psp_Type.ID = (int)Common.Services.BaseService.Create("InsertPSP_P_Types", psp_Type);
                            dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));


                        } catch (Exception ex) {
                            MsgBox.Show("增加区域出错：" + ex.Message);
                        }
                        RefreshChart();
                    }
                }
                else
                {
                    Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();

                    psp_Type.ID = Guid.NewGuid().ToString();

                    psp_Type.Forecast = type;
                    psp_Type.ForecastID = forecastReport.ID;

                    psp_Type.Title = FA.Areatitle;
                    //glebeYearValue gy = new glebeYearValue();

                    //IList<glebeYearValue> yearlist = Services.BaseService.GetList<glebeYearValue>("SelectglebeYearValueBywhere", "ParentID='" + svglist[0].UID + "'");
                    //for (int i = 0; i < yearlist.Count; i++) {
                    //    string y = "y" + yearlist[i].Year.ToString();
                    //    psp_Type.GetType().GetProperty(y).SetValue(psp_Type, y, null);
                    //}
                    object obj = Services.BaseService.GetObject("SelectPs_Forecast_MathMaxID", null);
                    if (obj != null)
                        psp_Type.Sort = ((int)obj) + 1;
                    else
                        psp_Type.Sort = 1;

                    try {
                        Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type);
                        //psp_Type.ID = (int)Common.Services.BaseService.Create("InsertPSP_P_Types", psp_Type);
                        dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));


                    } catch (Exception ex) {
                        MsgBox.Show("增加区域出错：" + ex.Message);
                    }
                    RefreshChart();
                }
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TreeListNode row = this.treeList1.FocusedNode;
            if (row == null) {
                return;
            }


            string parentid = row["ParentID"].ToString();


           // FormTypeTitle frm = new FormTypeTitle();
            string TypeTitle = row["Title"].ToString();
          
            frmMain_Spatial fmain = new frmMain_Spatial();
            frmMain_Spatial.MapType = "接线图";
            frmMain_Spatial.progtype = "城市规划层";
            string pid = "";
            fmain.Show();
            fmain.Owner = this;
            string progtype = "城市规划层";
            //if (progtype == "地理信息层") {
            //    fmain.ViewMenu();
            //}
            if (pid == "") {
                fmain.Open("c5ec3bc7-9706-4cbd-9b8b-632d3606f933","");
            } else {
                fmain.Open("c5ec3bc7-9706-4cbd-9b8b-632d3606f933", pid);
            }
           // LoadImage = true;
            fmain.InitShape();
            fmain.Init(progtype);
            fmain.InitScaleRatio(); 
            fmain.LayerManagerShow();
            fmain.OpenGHQYpropetty(TypeTitle);
         
            
            
            if (fmain.DialogResult==DialogResult.OK)
            {
                glebeProperty gp = new glebeProperty();
                gp.ParentEleID = "0";
                gp.SvgUID = "c5ec3bc7-9706-4cbd-9b8b-632d3606f933";
                gp.ObligateField16 = TypeTitle;
                IList<glebeProperty> svglist = Services.BaseService.GetList<glebeProperty>("SelectglebePropertyByObligateField16", gp);
                //重新对选中的数据进行更新
                 Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
                 ForecastClass1.TreeNodeToDataObject<Ps_Forecast_Math>(psp_Type, row);
                      
                        IList<glebeYearValue> yearlist = Services.BaseService.GetList<glebeYearValue>("SelectglebeYearValueBywhere", "ParentID='" + svglist[0].UID + "'");
                        for (int i = 0; i < yearlist.Count;i++ ) {
                            string y = "y" + yearlist[i].Year.ToString();
                            psp_Type.GetType().GetProperty(y).SetValue(psp_Type, yearlist[i].Burthen, null);
                            
                        }
                       

                        try {
                            Common.Services.BaseService.Update<Ps_Forecast_Math>(psp_Type);
                            //psp_Type.ID = (int)Common.Services.BaseService.Create("InsertPSP_P_Types", psp_Type);
                            //dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));
                            LoadData();
                            //treeList1.EndUpdate();
                            RefreshChart();
                        }
                catch(Exception ex)
                        {

                }
            }
        }

        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e) {
            TreeListNode row = this.treeList1.FocusedNode;
            if (row == null)
                return;
            Ps_Forecast_Math pf = new Ps_Forecast_Math();
            ForecastClass1.TreeNodeToDataObject<Ps_Forecast_Math>(pf, row);
            Services.BaseService.Update<Ps_Forecast_Math>(pf);
            //CalculateSum2(row);
            //aaa(row);
            RefreshChart();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TreeListNode row = this.treeList1.FocusedNode;
            if (row == null) {
                return;
            }

            if (row.Nodes.Count > 0) {
                MsgBox.Show("有下级分类，不可删除");
                return;
            }

            string parentid = row["ParentID"].ToString();



            if (MsgBox.ShowYesNo("是否删除分类 " + row["Title"].ToString() + "？") == DialogResult.Yes) {
                Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
                ForecastClass1.TreeNodeToDataObject(psp_Type, row);
                //psp_Type = Itop.Common.DataConverter.RowToObject<Ps_Forecast_Math>(row);
                Ps_Forecast_Math psp_Values = new Ps_Forecast_Math();
                psp_Values.ID = psp_Type.ID;

                try {
                    //DeletePSP_ValuesByType里面删除数据和分类
                    Common.Services.BaseService.Delete<Ps_Forecast_Math>(psp_Values);
                    FORBaseColor bc1 = new FORBaseColor();

                    bc1.Remark = forecastReport.ID + "-" + type;
                    bc1.Title = row["Title"].ToString();
                    Common.Services.BaseService.Update("DeleteFORBaseColorByTitleRemark", bc1);

                    this.treeList1.Nodes.Remove(row);
                } catch (Exception ex) {
                    this.Cursor = Cursors.WaitCursor;
                    LoadData();
                    this.Cursor = Cursors.Default;
                }
                RefreshChart();
            }
        }

    }
}