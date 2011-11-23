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
using DevExpress.XtraEditors.Repository;
using Itop.Client.Using;
using Itop.Domain.Table;
namespace Itop.Client.History
{
    /// <summary>
    /// 用电量－大户
    /// </summary>
    public partial class FormHistory2 : FormBase
    {
        string type1 = "2";
        int type = 2;
        string type31 = "3";
        int type32 = 3;
        DataTable dataTable = new DataTable();
        DataTable dataTable2 = new DataTable();
        bool bLoadingData = false;
        bool _canEdit = true;
        int firstyear = 2000;
        int endyear = 2008;
        string splitstr="-";
        string where1 = "";
        string where2 = "";
        public FormHistory2() {
            InitializeComponent();
            treeList1.OptionsView.AutoWidth = false;
            treeList2.OptionsView.AutoWidth = false;

            barButtonItem6.Glyph = Itop.ICON.Resource.关闭;
        }

        private void HideToolBarButton() {

        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            
        }
        private void Form8Forecast_Load(object sender, EventArgs e) {
            Application.DoEvents();
            Ps_YearRange py = new Ps_YearRange();
            py.Col4 = "区县发展实绩";
            py.Col5 = ProjectUID;

            IList<Ps_YearRange> li = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);
            if (li.Count > 0) {
                firstyear = li[0].StartYear;
                endyear = li[0].FinishYear;
            } else {
                firstyear = 2001;
                endyear = DateTime.Today.Year -1;
                py.BeginYear = 2001;
                py.FinishYear = endyear;
                py.StartYear = firstyear;
                py.EndYear = 2060;
                py.ID = Guid.NewGuid().ToString();
                Services.BaseService.Create<Ps_YearRange>(py);
            }
            if (!AddRight)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!EditRight)
            {
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btCopy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!DeleteRight)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                
            }
            LoadData();
            //RefreshChart();
            where1 = string.Format(" Forecast ={0} and Col4 ='{1}' ", type, ProjectUID);
            where2 = string.Format(" Forecast ={0} and Col4 ='{1}' ", type32, ProjectUID);
            initpasteCols();
        }
        /// <summary>
        /// 初始化粘贴列
        /// </summary>
        private void initpasteCols() {
            pasteCols.Add("Title");
            pasteCols.Add("Col2");
            pasteCols.Add("y1990");
            pasteCols.Add("y1991");
            for (int i = firstyear; i <= endyear; i++) {
                pasteCols.Add("y" + i);
            }
        }
        List<string> pasteCols = new List<string>();//数据粘贴列
        private void LoadData() {
            //this.splitContainerControl1.SplitterPosition = (2* this.splitterControl1.Width) / 3;
            this.splitContainerControl2.SplitterPosition = splitContainerControl2.Height / 2;
            bLoadingData = true;
            if (dataTable != null) {
                dataTable.Columns.Clear();
                treeList1.ClearNodes();
                treeList1.Columns.Clear();
                dataTable2.Columns.Clear();
                treeList2.Columns.Clear();
            }
            AddFixColumn();
            for (int i = firstyear; i <= endyear; i++) {
                AddColumn(i);
            }
            treeList1.Columns.AssignTo(treeList2.Columns);
            //treeList2.Columns["Sort"].SortOrder = SortOrder.Ascending;
            treeList2.Columns.RemoveAt(1);
            treeList2.Columns.RemoveAt(1);
            treeList2.Columns.RemoveAt(1);
            
            load1();
            load2();
            treeList1.CollapseAll();
            treeList2.CollapseAll();
            if (treeList1.Nodes.Count > 1)
            {
                treeList1.MoveFirst();
                treeList1.Nodes[1].Expanded = true;
            }
           
            if (treeList2.Nodes.Count > 1)
            {
                treeList2.MoveFirst();
                treeList2.FocusedNode = treeList2.Nodes[0];
                treeList2.Nodes[1].Expanded = true;
            }
            bLoadingData = false;
        }
        
        private void load1() {
            ArrayList al = new ArrayList();
            al.Add("全社会");
            //Dictionary<string, string> al = new Dictionary<string, string>();
            //al.Add("全社会用电量（亿kWh）", "");
            //al.Add(

            //IList<Base_Data> li1 = Common.Services.BaseService.GetStrongList<Base_Data>();
            //foreach (Base_Data bd in li1)
            //    al.Add(bd.Title);

            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = type;
            psp_Type.Col4 = ProjectUID;
            IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);
            //foreach (Ps_History ph in listTypes)
            //    {
            //    Common.Services.BaseService.Delete<Ps_History>(ph);

            //    }
            if (listTypes.Count == 0) {
                Ps_History pf = new Ps_History();
                pf.ID = createID();
                //pf.ParentID = "";
                pf.Sort =  - 1;
                pf.Forecast = type;
                pf.ForecastID = type1;
                pf.Title = "全社会";
                pf.Col4 = ProjectUID;
                Services.BaseService.Create<Ps_History>(pf);
                listTypes.Add(pf);
                Ps_History pf2 = new Ps_History();
                pf2.ID = pf.ID + splitstr;
                //pf.ParentID = "";
                pf2.Sort = -1;
                pf2.Forecast = type32;
                pf2.ForecastID = type31;
                pf2.Title = pf.Title;
                pf2.Col4 = ProjectUID;
                Services.BaseService.Create<Ps_History>(pf2);
            }
            dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));
            treeList1.BeginInit();
            treeList1.DataSource = dataTable;
            
            treeList1.Columns["Sort"].SortOrder = SortOrder.Ascending;
            treeList1.EndInit();
            //Application.DoEvents();
            //treeList1.Nodes[0].Expanded = true;
            
        }
        private void load2() {
            ArrayList al = new ArrayList();
            al.Add("全社会");
            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = type32;
            psp_Type.Col4 = ProjectUID;
            IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);
            //foreach (Ps_History ph in listTypes)
            //    {
            //    Common.Services.BaseService.Delete<Ps_History>(ph);

            //    }
            if (listTypes.Count == 0) {
                
                TreeListNode node = treeList1.FindNodeByFieldValue("Title", al[0].ToString());
                Ps_History pf = new Ps_History();
                pf.ID = node["ID"] + splitstr;
                //pf.ParentID = "";
                pf.Sort = -1;
                pf.Forecast = type32;
                pf.ForecastID = type31;
                pf.Title = al[0].ToString();
                pf.Col4 = ProjectUID;
                Services.BaseService.Create<Ps_History>(pf);
                listTypes.Add(pf);
            }
            dataTable2 = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));
            treeList2.DataSource = dataTable2;
            treeList2.Columns["Sort"].SortOrder = SortOrder.Ascending;
            //Application.DoEvents();
            //treeList2.Nodes[0].Expanded = true;
            
        }
        //添加固定列
        private void AddFixColumn() {
            TreeListColumn column = new TreeListColumn();
            column.FieldName = "Title";
            column.Caption = "分类名";
            column.VisibleIndex = 0;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "Col2";
            column.Caption = "产品生产能力";
            column.VisibleIndex = 1;
            column.Width = 150;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "y1990";
            column.Caption = "正常电量";
            column.VisibleIndex = 2;
            column.Width = 70;
            //column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "y1991";
            column.Caption = "正常负荷";
            column.VisibleIndex = 3;
            column.Width = 70;
            //column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "Sort";
            column.Caption = "序号";
            column.VisibleIndex = 1;
            column.Width = 50;
            column.OptionsColumn.AllowEdit = false;
            //column.SortOrder = SortOrder.Ascending;
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

            column = new TreeListColumn();
            column.FieldName = "COL5";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] { column });

            column = new TreeListColumn();
            column.FieldName = "COL6";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] { column });

            column = new TreeListColumn();
            column.FieldName = "COL7";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] { column });
        }
        //添加年份后，新增一列
        private void AddColumn(int year) {
            TreeListColumn column = new TreeListColumn();

            column.FieldName = "y" + year;
            column.Tag = year;
            column.Caption = year + "年";
            column.Name = year.ToString();
            column.Width = 60;
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
        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
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
            //this.chart1.SaveAsImage(sf.FileName, ci);
        }

        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            FormColor fc = new FormColor();
            fc.DT = dataTable;
            fc.ID = "电力发展实绩";
            //fc.For =0;
            //if (fc.ShowDialog() == DialogResult.OK)
            //RefreshChart();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.Close();
        }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

            //FileClass.ExportToExcelOld(this.forecastReport.Title, "", this.gridControl1);
        }

        private void Save() {
            //保存
            foreach (DataRow dataRow in dataTable.Rows) {

                try {
                    Ps_History v = Itop.Common.DataConverter.RowToObject<Ps_History>(dataRow);
                    Services.BaseService.Update("UpdatePs_HistoryByID", v);

                } catch { }
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            List<int> li = new List<int>();
            for (int i = firstyear; i <= endyear; i++) {
                li.Add(i);
            }

            FormChooseYears cy = new FormChooseYears();
            cy.ListYearsForChoose = li;
            if (cy.ShowDialog() != DialogResult.OK)
                return;
            Hashtable ht = new Hashtable();

            foreach (DataRow a in cy.DT.Rows) {
                if (a["B"].ToString() == "True")
                    ht.Add(Guid.NewGuid().ToString(), Convert.ToInt32(a["A"].ToString().Replace("年", "")));
            }
            FormGdpView fgv = new FormGdpView();
            fgv.ProjectUID = ProjectUID;
            fgv.HT = ht;
            fgv.ShowDialog();
        }


        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e) {
            //表格数据发生变化
            if (e.Column.FieldName.Substring(0, 1) != "y") return;

            //if ((e.Node.ParentNode.ParentNode == null && !e.Node.HasChildren))
            //{
            //    ;
            //    Common.Services.BaseService.Update<Ps_History>(DataConverter.RowToObject<Ps_History>((e.Node.TreeList.GetDataRecordByNode(e.Node) as DataRowView).Row));
            //    return;
            //}
            double d=0;
            if (!double.TryParse(e.Value.ToString(),out d)) return;
            try {
                CalculateSum(e.Node, e.Column);
            }
            catch { }
            if (!e.Node.ParentNode["Title"].ToString().Contains("全地区"))
            cacsumsehui(e.Column.FieldName);
            //Save();
            //RefreshChart();
        }
        private void treeList2_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Substring(0, 1) != "y") return;

            //if ((e.Node.ParentNode.ParentNode == null && !e.Node.HasChildren))
            //{
            //    ;
            //    Common.Services.BaseService.Update<Ps_History>(DataConverter.RowToObject<Ps_History>((e.Node.TreeList.GetDataRecordByNode(e.Node) as DataRowView).Row));
            //    return;
            //}
            CalculateSum2(e.Node, e.Column);
            if (!e.Node.ParentNode["Title"].ToString().Contains("全地区"))
            cacsumsehui2(e.Column.FieldName);
        }
        #region 分类管理
        /// <summary>
        /// 新建用户增加
        /// </summary>
        private void newHistory() {
            TreeListNode focusedNode = treeList1.FocusedNode;
            FormHistory2_add2 frm = new FormHistory2_add2();
            frm.Text = "增加分类";
            if (focusedNode.Nodes.Count > 0) {
                TreeListNode lastnode=focusedNode.Nodes[focusedNode.Nodes.Count - 1];
                frm.Sortid = (int)lastnode["Sort"] + 1;
                //frm.COL5 = lastnode["COL5"].ToString();
            }
            if (frm.ShowDialog() == DialogResult.OK) {
                Ps_History pf = new Ps_History();
                pf.ID = createID();
                pf.Forecast = type;
                pf.ForecastID = type1;
                pf.Title = frm.TypeTitle;
                pf.Col2 = frm.COL10;
                pf.Col11 = frm.COL11;
                pf.Col12 = frm.COL12;
                pf.Col13 = frm.COL13;
                pf.ParentID = focusedNode["ID"].ToString();
                pf.Col4 = ProjectUID;
                pf.Col5 = frm.COL5;
                pf.Col6 = frm.COL6;
                pf.Col7 = frm.COL7;
                pf.Sort = frm.Sortid;               

                Ps_History pf2 = new Ps_History();
                pf2.ID = pf.ID + splitstr;
                pf2.Forecast = type32;
                pf2.ForecastID = type31;
                pf2.Title = pf.Title;
                pf2.ParentID = pf.ParentID + splitstr;
                pf2.Col4 = ProjectUID;
                pf2.Col5 = frm.COL5;
                pf2.Col6 = frm.COL6;
                pf2.Col7 = frm.COL7;
                pf2.Sort = frm.Sortid;
                pf2.Col11 = frm.COL11;
                pf2.Col12 = frm.COL12;
                pf2.Col13 = frm.COL13;
                try {
                    Services.BaseService.Create<Ps_History>(pf);
                    Services.BaseService.Create<Ps_History>(pf2);
                    treeList1.BeginInit();
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf, dataTable.NewRow()));
                    treeList1.EndInit();
                    treeList2.BeginInit();
                    dataTable2.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf2, dataTable2.NewRow()));
                    treeList2.EndInit();
                    //LoadData();
                    //insertNode(focusedNode, pf);
                } catch (Exception ex) { MsgBox.Show("增加分类出错：" + ex.Message); }
            }

        }
        private void editHistory() {
            FormHistory2_add2 frm = new FormHistory2_add2();
            frm.TypeTitle = treeList1.FocusedNode.GetValue("Title").ToString();
            frm.Text = "修改分类";
            Ps_History v = new Ps_History();
            v = Services.BaseService.GetOneByKey<Ps_History>(treeList1.FocusedNode["ID"].ToString());

            frm.COL10 = v.Col2;
            frm.Sortid = v.Sort;
            frm.COL5 = v.Col5;
            frm.COL6 = v.Col6;
            frm.COL7 = v.Col7;
            frm.COL11 = v.Col11;
            frm.COL12 = v.Col12;
            frm.COL13 = v.Col13;
            //frm.COL11 = v.y1990;
            //frm.COL12 = v.y1991;

            if (frm.ShowDialog() == DialogResult.OK) {
                //TreeNodeToDataObject<Ps_History>(v, treeList1.FocusedNode);
                v.Title = frm.TypeTitle;                                
                v.Col2 = frm.COL10;
                v.Col5 = frm.COL5;
                v.Col6 = frm.COL6;
                v.Col7 = frm.COL7;
                v.Col11 = frm.COL11;
                v.Col12 = frm.COL12;
                v.Col13 = frm.COL13;
                    //v.y1990 = frm.COL11;
                    //v.y1991 = frm.COL12;                
                v.Sort = frm.Sortid;
                Ps_History v2 = Services.BaseService.GetOneByKey<Ps_History>(v.ID + splitstr);
                v2.Title = v.Title;
                v2.Col5 = frm.COL5;
                v2.Col6 = frm.COL6;
                v2.Col7 = frm.COL7;
                v2.Col11 = frm.COL11;
                v2.Col12 = frm.COL12;
                v2.Col13 = frm.COL13;
                v2.Sort = v.Sort;
                try {
                    try { treeList1.FocusedNode["Col2"] = v.Col2; } catch { }
                    treeList1.FocusedNode["Col5"] = v.Col5;
                    treeList1.FocusedNode["Col6"] = v.Col6;
                    treeList1.FocusedNode["Col7"] = v.Col7;
                    try { treeList1.FocusedNode.SetValue("Title", v.Title); } catch { }
                    try { treeList1.FocusedNode.SetValue("Sort", v.Sort); } catch { }
                    Common.Services.BaseService.Update<Ps_History>(v);
                    Common.Services.BaseService.Update<Ps_History>(v2);
                    TreeListNode node = treeList2.FindNodeByKeyID(v2.ID);
                    try { node["Title"] = v2.Title; } catch { }
                    try { node["Sort"] = v2.Sort; } catch { }

                } catch (Exception ex) { MsgBox.Show("修改分类出错：" + ex.Message); }

            }
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TreeListNode focusedNode = treeList1.FocusedNode;

            if (focusedNode == null) {
                return;
            }

            if (focusedNode["Title"].ToString().Contains("新增")) {
                newHistory();
                return;
            }

            FormHistory2_add frm = new FormHistory2_add();
            frm.Text = "增加分类";
            if (focusedNode.Nodes.Count > 0)
                frm.Sortid = (int)focusedNode.Nodes[focusedNode.Nodes.Count - 1]["Sort"] + 1;
            if (frm.ShowDialog() == DialogResult.OK) {
                Ps_History pf = new Ps_History();
                pf.ID = createID();
                pf.Forecast = type;
                pf.ForecastID = type1;
                pf.Title = frm.TypeTitle;
                pf.Col2 = frm.COL10;
                //pf.y1990 =frm.COL11;
                //pf.y1991 = frm.COL12;
                pf.ParentID = focusedNode["ID"].ToString();
                pf.Col4 = ProjectUID;
                pf.Sort = frm.Sortid;
                //object obj = Services.BaseService.GetObject("SelectPs_HistoryMaxID", null);
                //if (obj != null)
                //    pf.Sort = ((int)obj) + 1;
                //else
                //    pf.Sort = 1;
                
                Ps_History pf2 = new Ps_History();
                pf2.ID = pf.ID + splitstr;
                pf2.Forecast = type32;
                pf2.ForecastID = type31;
                pf2.Title = pf.Title;
                if (pf.Title.Contains("用电量"))
                    pf2.Title.Replace("用电量", "负荷");
                else
                    if (pf.Title.Contains("电量"))
                        pf2.Title.Replace("电量", "负荷"); 
                    else
                        pf2.Title = pf.Title;
                pf2.ParentID = pf.ParentID + splitstr;
                pf2.Col4 = ProjectUID;
                pf2.Sort = frm.Sortid;
                try {
                   
                    Services.BaseService.Create<Ps_History>(pf);
                    Services.BaseService.Create<Ps_History>(pf2);
                    treeList1.BeginInit();
                    
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf, dataTable.NewRow()));
                    treeList1.EndInit();
                    treeList2.BeginInit();
                    dataTable2.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf2, dataTable2.NewRow()));
                    treeList2.EndInit();
                    //LoadData();
                    //insertNode(focusedNode, pf);
                } catch (Exception ex) { MsgBox.Show("增加分类出错：" + ex.Message); }
            }

        }
        private void insertNode(TreeListNode pnode,Ps_History obj) {
            
        }
        private string createID() {
            string str =Guid.NewGuid().ToString();
            return ProjectUID + "_" + str.Substring(str.Length -12);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
                return;
            if (tln.HasChildren) {
                MsgBox.Show("有子类时不可删除！");
                return;
            }
            

            if (MsgBox.ShowYesNo("是否删除分类 " + tln.GetValue("Title") + "？") == DialogResult.Yes) {
                Ps_History pf = new Ps_History();
                pf.ID = tln["ID"].ToString();

                try {
                    //DeletePSP_ValuesByType里面删除数据和分类
                    Common.Services.BaseService.Delete<Ps_History>(pf);
                    pf.ID = pf.ID + splitstr;
                    Common.Services.BaseService.Delete<Ps_History>(pf);
                    TreeListNode node = treeList2.FindNodeByKeyID(pf.ID);
                    if (node != null)
                        treeList2.DeleteNode(node);
                    treeList1.DeleteNode(treeList1.FocusedNode);
                    
                } catch { }
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //修改
            if (treeList1.FocusedNode == null)
                return;

            //if (treeList1.FocusedNode.ParentNode == null) {
            //    MsgBox.Show("该目录不可修改");
            //    return;
            //}
            if (treeList1.FocusedNode.ParentNode != null)
            if (treeList1.FocusedNode.ParentNode["Title"].ToString().Contains("新增")) {
                editHistory();
                return;
            }

            FormHistory2_add frm = new FormHistory2_add();
            frm.TypeTitle = treeList1.FocusedNode.GetValue("Title").ToString();
            frm.Text = "修改分类";
            Ps_History v = new Ps_History();
            v = Services.BaseService.GetOneByKey<Ps_History>(treeList1.FocusedNode["ID"].ToString());
            
            frm.COL10 = v.Col2;
            frm.Sortid = v.Sort;      
            //frm.COL11 = v.y1990;
            //frm.COL12 = v.y1991;

            if (frm.ShowDialog() == DialogResult.OK) {
                                //TreeNodeToDataObject<Ps_History>(v, treeList1.FocusedNode);
                v.Title = frm.TypeTitle;
                Ps_History v2 = Services.BaseService.GetOneByKey<Ps_History>(v.ID + splitstr);
                if (v.Title == "常规用电量")
                    v2.Title = "常规负荷";
                else
                    if (v.Title == "大用户电量")
                        v2.Title = "大用户负荷";
                    else
                        if (v.Title.Contains("用电量"))
                            v2.Title = v.Title.Replace("用电量", "负荷");
                        else
                            v2.Title = v.Title;
                if (treeList1.FocusedNode.Nodes.Count == 0) {
                    v.Col2 = frm.COL10;            
                    //v.y1990 = frm.COL11;
                    //v.y1991 = frm.COL12;
                }
                
                v.Sort = frm.Sortid;
                v2.Sort = v.Sort;
                try {
                    try { treeList1.FocusedNode["Col2"] = v.Col2; } catch { }
                    //try {treeList1.FocusedNode.SetValue("y1990", v.y1990);} catch { }
                    //try {treeList1.FocusedNode.SetValue("y1991", v.y1991);} catch { }
                    try {treeList1.FocusedNode.SetValue("Title", v.Title);} catch { }
                    try { treeList1.FocusedNode.SetValue("Sort", v.Sort); } catch { }
                    Common.Services.BaseService.Update<Ps_History>(v);
                    Common.Services.BaseService.Update<Ps_History>(v2);
                    TreeListNode node = treeList2.FindNodeByKeyID(v2.ID);
                    try { node["Title"] = v2.Title; } catch { }
                    try { node["Sort"] = v2.Sort; } catch { }

                } catch (Exception ex) { MsgBox.Show("修改分类出错：" + ex.Message); }

            }
        }
        #endregion
        static public void TreeNodeToDataObject<T>(T dataObject, DevExpress.XtraTreeList.Nodes.TreeListNode treeNode) {
            Type type = typeof(T);
            foreach (PropertyInfo pi in type.GetProperties()) {
                if (pi.Name.Substring(0, 1) == "y") {
                    object obj =treeNode.GetValue(pi.Name);
                    try {
                        pi.SetValue(dataObject, obj != DBNull.Value ? obj : 0, null);
                    } catch (Exception err) { MessageBox.Show(err.Message); }
                }
                //if (pi.Name == "COL10" || pi.Name == "y1991" || pi.Name == "y1991") {
                //    pi.SetValue(dataObject, treeNode.GetValue(pi.Name), null);
                //}
            }
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e) {
            if (treeList1.FocusedNode.HasChildren) {
                e.Cancel = true;
            }
            if(!EditRight)
            {
             e.Cancel = true;
            }
        }
        private void treeList2_ShowingEditor(object sender, CancelEventArgs e) {
            if (treeList2.FocusedNode.HasChildren) {
                e.Cancel = true;
            }
            if (!EditRight)
            {
                e.Cancel = true;
            }
        }
        //private void CalculateSum(TreeListNode node, TreeListColumn column) {
            
        //    Ps_History v = Services.BaseService.GetOneByKey<Ps_History>(node["ID"].ToString());
        //    TreeNodeToDataObject<Ps_History>(v, node);
        //    Common.Services.BaseService.Update<Ps_History>(v);
        //    TreeListNode parentNode = node.ParentNode;
        //    if (parentNode == null) {
        //        return;
        //    }
        //    if (node["Title"].ToString() == "同时率") return;
        //    double sum = 0;
        //    foreach (TreeListNode nd in parentNode.Nodes) {
        //        if (nd["Title"].ToString() == "同时率") continue;
        //        if (nd.ParentNode.ParentNode == null && !nd.HasChildren)
        //        {
        //            continue;
        //        }
        //        object value = nd.GetValue(column.FieldName);
        //        if (value != null && value != DBNull.Value) {
        //            sum += Convert.ToDouble(value);
        //        }
        //    }
        //    if (sum != 0) {
        //        parentNode.SetValue(column.FieldName, sum);
        //        v = Services.BaseService.GetOneByKey<Ps_History>(parentNode["ID"].ToString());
        //        TreeNodeToDataObject<Ps_History>(v, parentNode);

        //        Common.Services.BaseService.Update<Ps_History>(v);
        //    } else
        //        parentNode.SetValue(column.FieldName, null);
        //    CalculateSum(parentNode, column);
        //}
        private void CalculateSum(TreeListNode node, TreeListColumn column)
        {
            //Ps_Forecast_Math v = Services.BaseService.GetOneByKey<Ps_Forecast_Math>(node["ID"].ToString());
            //TreeNodeToDataObject<Ps_Forecast_Math>(v, node);
            DataRow row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;
            Ps_History v = DataConverter.RowToObject<Ps_History>(row);
            Common.Services.BaseService.Update<Ps_History>(v);
            TreeListNode parentNode = node.ParentNode;
            if (parentNode == null)
            {
                if (node["Title"].ToString().Contains("各县合计"))
                {
                    DataRow[] drlist = dataTable.Select("Title like '%全社会%'");
                    double value = 0;
                    foreach (DataRow dr in drlist)
                    {
                        if(dr["Title"].ToString().Contains("全地区"))
                        {
                         int i = dataTable.Rows.IndexOf(dr);
                            dataTable.Rows[i][column.FieldName]=node[column.FieldName];
                            v = DataConverter.RowToObject<Ps_History>(dataTable.Rows[i]);

                    
                            Common.Services.BaseService.Update<Ps_Forecast_Math>(v);
                        }
                    
                    
                    }
                }
                return;
            }
            if (node["Title"].ToString() == "同时率") return;

            double sum = 0;
            foreach (TreeListNode nd in parentNode.Nodes)
            {
                if (nd["Title"].ToString() == "同时率") continue;
                if (parentNode["Title"].ToString().Contains("合计") && !nd.HasChildren) continue;
                object value = nd.GetValue(column.FieldName);
                if (value != null && value != DBNull.Value)
                {
                    sum += Convert.ToDouble(value);
                }
            }
            if (sum != 0)
            {
                parentNode.SetValue(column.FieldName, sum);
                row = (parentNode.TreeList.GetDataRecordByNode(parentNode) as DataRowView).Row;
                v = DataConverter.RowToObject<Ps_History>(row);

                //v = Services.BaseService.GetOneByKey<Ps_History>(parentNode["ID"].ToString());
                //TreeNodeToDataObject<Ps_History>(v, parentNode);

                Common.Services.BaseService.Update<Ps_Forecast_Math>(v);
            }
            else
                parentNode.SetValue(column.FieldName, null);
            CalculateSum(parentNode, column);
        }
        private void CalculateSum2(TreeListNode node, TreeListColumn column)
        {
            //Ps_Forecast_Math v = Services.BaseService.GetOneByKey<Ps_History>(node["ID"].ToString());
            //TreeNodeToDataObject<Ps_History>(v, node);
            DataRow row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;
            Ps_History v = DataConverter.RowToObject<Ps_History>(row);
            Common.Services.BaseService.Update<Ps_History>(v);
            TreeListNode parentNode = node.ParentNode;
            if (parentNode == null)
            {
                if (node["Title"].ToString().Contains("各县合计"))
                {
                    DataRow[] drlist = dataTable2.Select("Title like '%全社会%'");
                    double value = 0;
                    foreach (DataRow dr in drlist)
                    {
                        if (dr["Title"].ToString().Contains("全地区"))
                        {
                            int i = dataTable2.Rows.IndexOf(dr);
                            dataTable2.Rows[i][column.FieldName] = node[column.FieldName];
                            v = DataConverter.RowToObject<Ps_History>(dataTable2.Rows[i]);


                            Common.Services.BaseService.Update<Ps_Forecast_Math>(v);
                        }


                    }
                }
                return;
            }
            //if (node["Title"].ToString() == "同时率") return;
            //if (node["Title"].ToString() == "同时率") 
            //{ 


            //}
            double sum = 0;
            double valuetemp = 0;
            bool istsl = false;
            foreach (TreeListNode nd in parentNode.Nodes)
            {

                object value = null;
                if (nd["Title"].ToString() == "同时率")
                {
                    istsl = true;
                    value = nd.GetValue(column.FieldName);
                    if (value != null && value != DBNull.Value)
                    {
                        valuetemp = Convert.ToDouble(value);
                        continue;
                    }

                }
                if (!nd.HasChildren && istsl && parentNode["Title"].ToString().Contains("合计"))
                    continue;
                value = nd.GetValue(column.FieldName);
                if (value != null && value != DBNull.Value)
                {
                    if (istsl)
                        sum += valuetemp * Convert.ToDouble(value);
                    else
                        sum += Convert.ToDouble(value);
                }
            }
            if (sum != 0)
            {
                parentNode.SetValue(column.FieldName, sum);
                row = (parentNode.TreeList.GetDataRecordByNode(parentNode) as DataRowView).Row;
                v = DataConverter.RowToObject<Ps_History>(row);

                //v = Services.BaseService.GetOneByKey<Ps_History>(parentNode["ID"].ToString());
                //TreeNodeToDataObject<Ps_History>(v, parentNode);

                Common.Services.BaseService.Update<Ps_History>(v);
            }
            else
                parentNode.SetValue(column.FieldName, null);
            CalculateSum2(parentNode, column);
        }
        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e) {
            if (e.Node != null) {
                string id = e.Node["ID"]+splitstr;
                TreeListNode node = treeList2.FindNodeByKeyID(id);
                if (node != null) {
                    treeList2.Selection.Clear();
                    treeList2.SetFocusedNode(node);
                }  
            }
        }
        private void reloadTree2() {
            dataTable2.Clear();
            load2();
        }
        private void treeList1_AfterDragNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e) {
            string id = e.Node["ID"].ToString();

            Ps_History v = Services.BaseService.GetOneByKey<Ps_History>(id);
            if (v != null) {
                v.ParentID = e.Node["ParentID"].ToString();
                Services.BaseService.Update<Ps_History>(v);
                v = Services.BaseService.GetOneByKey<Ps_History>(id + splitstr);
                if (v != null) {
                    v.ParentID = e.Node["ParentID"].ToString() + splitstr;
                    Services.BaseService.Update<Ps_History>(v);
                    reloadTree2();
                }
            }
        }
        #region 统计表
        /// <summary>
        /// 市拟建主要工业项目及用电需求情况表
        /// </summary>
        private void tj431() {

            Formtj432 f = new Formtj432();
            f.Text = "全市拟建主要工业项目及用电需求情况表";
            f.CanPrint = base.EditRight;
            
            setcols431();
            f.GridDataTable = ConvertTreeListToDataTable431(treeList1);
            f.ShowDialog();
            //}
        }
        private void tj511() {

            Formtj432 f = new Formtj432();
            f.Text = treeList1.FocusedNode["Title"].ToString() + "重点建设工业及用电需求表";
            f.CanPrint = base.EditRight;

            setcols431();
            showtable.Columns["Col2"].Caption = "项目名称";
            f.GridDataTable = ConvertTreeListToDataTable511(treeList1);
            f.ShowDialog();
            //}
        }
        /// <summary>
        /// 现有大工业用户产能及用电情况
        /// </summary>
        private void tj432() {
            
            Formtj432 f = new Formtj432();
            f.Text = "现有大工业用户产能及用电情况";
            f.CanPrint = base.EditRight;
            List<int> list = new List<int>();
            list.Add(endyear);
            setcols432(list);
            f.GridDataTable = ConvertTreeListToDataTable(treeList1);
            f.ShowDialog();
            //}
        }
        /// <summary>
        /// 全市历年分类规模用电情况表
        /// </summary>
        private void tj433() {
            FormChooseYears frm = new FormChooseYears();
            foreach (TreeListColumn column in treeList1.Columns) {
                if (column.Caption.IndexOf("年") > 0) {
                    frm.ListYearsForChoose.Add((int)column.Tag);
                }
            }
            if (frm.ShowDialog() == DialogResult.OK) {
                Formtj432 f = new Formtj432();
                f.Text = "全市历年分类规模用电情况表";
                f.CanPrint = base.EditRight;
                setcols433(frm.GetChoosedYear());
                f.GridDataTable = ConvertTreeListToDataTable433(treeList1);
                DialogResult dr = f.ShowDialog();
            }

        }
        /// <summary>
        /// 分区历年全社会用电情况表
        /// </summary>
        private void tj532() {
            FormChooseYears frm = new FormChooseYears();
            foreach (TreeListColumn column in treeList1.Columns) {
                if (column.Caption.IndexOf("年") > 0) {
                    frm.ListYearsForChoose.Add((int)column.Tag);
                }
            }
            if (frm.ShowDialog() == DialogResult.OK) {
                Formtj432 f = new Formtj432();
                f.Text = treeList1.FocusedNode["Title"].ToString()+ "历年全社会用电情况表";
                f.CanPrint = base.EditRight;
                setcols433(frm.GetChoosedYear());
                f.GridDataTable = ConvertTreeListToDataTable532(treeList1);
                DialogResult dr = f.ShowDialog();
            }

        }

        private DataTable ConvertTreeListToDataTable532(DevExpress.XtraTreeList.TreeList treeList1) {

            TreeListNode node = treeList1.FocusedNode;
            DataRow row1 = ((DataRowView)node.TreeList.GetDataRecordByNode(node)).Row;
            DataRow newrow = createRow(showtable, row1);
            newrow["Title"] = "全社会用电量";
            setFh(newrow, node["ID"].ToString());
            AddNodeDataToDataTable433(showtable, node, cols, "");
            
            #region 计算增长率
            DataTable dtReturn = showtable;
            DataColumn columnBegin = null;
            foreach (DataColumn column in dtReturn.Columns) {
                if (column.Caption.IndexOf("年") > 0) {
                    if (columnBegin == null) {
                        columnBegin = column;
                    }
                } else if (column.ColumnName.IndexOf("增长率") > 0) {
                    DataColumn columnEnd = dtReturn.Columns[column.Ordinal - 2];

                    foreach (DataRow row in dtReturn.Rows) {
                        object numerator = row[columnEnd];
                        object denominator = row[columnBegin];

                        if (numerator != DBNull.Value && denominator != DBNull.Value) {
                            try {
                                int intervalYears = Convert.ToInt32(columnEnd.ColumnName.Replace("y", ""))
                                    - Convert.ToInt32(columnBegin.ColumnName.Replace("y", ""));
                                object obj = Math.Round(Math.Pow((double)numerator / (double)denominator, 1.0 / intervalYears) - 1, 4);
                                row[column] = obj;
                                if (double.IsNaN((double)row[column]) || double.IsInfinity((double)row[column]))
                                    row[column] = 0;
                            } catch {
                                row[column] = 0;
                            }
                        }
                    }

                    //本次计算增长率的列作为下次的起始列

                    columnBegin = columnEnd;
                }
            }
            #endregion
            return showtable;
        }
        private void AddNodeDataToDataTable433(DataTable dt, TreeListNode node, List<string> listColID,string padstr) {
            padstr += "　";
            foreach (TreeListNode nd in node.Nodes) {
                if (nd["Title"].ToString().Contains("新增")) continue;
                DataRow newRow = dt.NewRow();            
                string space = string.Copy(padstr);
                foreach (string colID in listColID) {
                    //分类名，第二层及以后在前面加空格
                    if (colID == "Title" && nd.ParentNode != null) {
                        newRow[colID] = space + nd[colID];
                    } else {
                        newRow[colID] = nd[colID];
                    }
                }
                setFh(newRow, nd["ID"].ToString());//负荷
                dt.Rows.Add(newRow);                
            
                AddNodeDataToDataTable433(dt, nd, listColID,padstr);
            }
        }
        
        private void createHj433(TreeListNode node ,IList<Ps_History> list1,IList<Ps_History> list2) {
            DataRow newrow = showtable.NewRow();
            string title =node["Title"].ToString();
            Ps_History ps1 =null;//电量
            Ps_History ps2 = null;//负荷
            foreach (Ps_History ps in list1) {
                if (ps.Title == title) {
                    ps1 = ps;
                    break;
                }
            }
            if (ps1 == null) return;
            foreach (Ps_History ps in list2) {
                if (ps.Title == title) {
                    ps2 = ps;
                    break;
                }
            }
            if (ps1 != null) {
                newrow = Itop.Common.DataConverter.ObjectToRow(ps1, newrow);
                newrow =createRow(showtable, newrow);
            }
            if (ps2 != null) {
                DataRow row = showtable.NewRow();
                row = Itop.Common.DataConverter.ObjectToRow(ps2, row);
                setData2(newrow, row);
            }
            if (title == "常规用电量") {
                foreach (TreeListNode n1 in node.Nodes) {
                    createHj433(n1, list1, list2);
                }
            }

        }
        private DataTable ConvertTreeListToDataTable433(DevExpress.XtraTreeList.TreeList treeList1) {
            IList<Ps_History> list =Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryGroupList",where1);
            IList<Ps_History> list2 =Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryGroupList",where2);
            
            foreach (TreeListNode node in treeList1.Nodes) {
                DataRow row = ((DataRowView)node.TreeList.GetDataRecordByNode(node)).Row;
                DataRow newrow =createRow(showtable, row);
                setFh(newrow, row["ID"].ToString());
                newrow["Title"] = "全社会用电量";
                bool flag = true;
                foreach (TreeListNode node2 in node.Nodes) {//区县
                    if (flag) {
                        flag = false;
                        foreach (TreeListNode node3 in node2.Nodes) {//
                            if (node3["Title"].ToString().Contains("同时率")) {
                                DataRow row2 = ((DataRowView)node3.TreeList.GetDataRecordByNode(node3)).Row;
                                DataRow newrow2 = createRow(showtable, row2);
                                setFh(newrow2, row2["ID"].ToString());
                            } else {
                                createHj433(node3, list, list2);
                                if (node3["Title"].ToString().Contains("大用户"))
                                    AddNodeDataToDataTable433(showtable, node3, cols, "");
                            }
                        }
                    } else {
                        foreach (TreeListNode node4 in node2.Nodes) {
                            if (node4["Title"].ToString().Contains("大用户"))
                            AddNodeDataToDataTable433(showtable, node4, cols, "");
                        }
                    }
                    DataRow row22 = findrow(showtable, " title like '%现有%' ","");
                    if (row22 != null)
                        row22["Title"] = "　"+node2["Title"];
                }
                
            }
            #region 计算增长率
            DataTable dtReturn = showtable;
            DataColumn columnBegin = null;
            foreach (DataColumn column in dtReturn.Columns) {
                if (column.Caption.IndexOf("年") > 0) {
                    if (columnBegin == null) {
                        columnBegin = column;
                    }
                } else if (column.ColumnName.IndexOf("增长率") > 0) {
                    DataColumn columnEnd = dtReturn.Columns[column.Ordinal - 2];

                    foreach (DataRow row in dtReturn.Rows) {
                        object numerator = row[columnEnd];
                        object denominator = row[columnBegin];
                        
                        if (numerator != DBNull.Value && denominator != DBNull.Value) {
                            try {
                                int intervalYears = Convert.ToInt32(columnEnd.ColumnName.Replace("y", ""))
                                    - Convert.ToInt32(columnBegin.ColumnName.Replace("y", ""));
                                object obj = Math.Round(Math.Pow((double)numerator / (double)denominator, 1.0 / intervalYears) - 1, 4);
                                row[column] =obj ;
                                if (double.IsNaN((double)row[column])  || double.IsInfinity((double)row[column]))
                                    row[column] = 0;
                                } catch {
                                row[column] = 0;
                            }
                        }
                    }

                    //本次计算增长率的列作为下次的起始列

                    columnBegin = columnEnd;
                }
            }
            #endregion
            return showtable;
        }
        private void setcols433(List<int> list) {
            showtable = new DataTable();

            cols.Clear();
            colyears.Clear();
            //showtable.Columns.Add("Col1", typeof(string)).Caption = "序号";

            //cols.Add("Col1");
            showtable.Columns.Add("Title", typeof(string)).Caption = "项目名称";
            cols.Add("Title");
            string col = "";
            int lastyear = 0;
            foreach(int year in list) {
                lastyear = year;
                col = "y" + year;
                cols.Add(col);
                
                showtable.Columns.Add(col, typeof(double)).Caption = year + "年电量";
                
            }
            if (col != "") {//最后一年加负荷列
                colyears.Add(col);
                showtable.Columns.Add(col + "_", typeof(double)).Caption = lastyear + "年负荷";
            }

            showtable.Columns.Add("_增长率", typeof(double)).Caption = "增长率";
        }
        private DataRow findrow(DataTable table, string filter,string sort) {
            DataRow[] rows = table.Select(filter, sort);
            if (rows != null && rows.Length > 0) {

                return rows[0];
            }
            return null;

        }
        private DataRow copyrow(DataTable dt, DataRow row) {
            DataRow newrow = dt.NewRow();
            newrow.ItemArray = row.ItemArray.Clone() as object[];
            return newrow;
        }
        List<string> cols = new List<string>();//复制列
        List<string> colyears = new List<string>();//负荷列
        List<string> colsum = new List<string>();//合计列
        private DataTable showtable = new DataTable();
        private void setcols431() {
            showtable = new DataTable();

            cols.Clear();
            colyears.Clear();
            colsum.Clear();
            showtable.Columns.Add("Col1", typeof(string)).Caption = "编号";

            cols.Add("Col1");
            showtable.Columns.Add("Title", typeof(string)).Caption = "企业(项目)名称";

            cols.Add("Title");
            showtable.Columns.Add("Col2", typeof(string)).Caption = "建设规模及内容";
            cols.Add("Col2");
           
            showtable.Columns.Add("Col7", typeof(string)).Caption = "计划建设年限";
            cols.Add("Col7");
            showtable.Columns.Add("Col6", typeof(string)).Caption = "工作进展";
            cols.Add("Col6");
            DataColumn dcol = showtable.Columns.Add("y1990", typeof(double));

            dcol.Caption = "用电量";
            //dcol = showtable.Columns.Add("y1990_sum", typeof(double));
            //dcol.Caption = "正常用电量";
            //dcol.Expression = "IIF(Title='小计',sum(y1990),y1990)";

            cols.Add("y1990");
            showtable.Columns.Add("y1991", typeof(double)).Caption = "负荷";
            cols.Add("y1991");
            
            colsum.Add("y1990");
            colsum.Add("y1991");
            
        }
        private void setcols432(List<int> list) {
            showtable =new DataTable();
            
            cols.Clear();
            colyears.Clear();
            colsum.Clear();
            showtable.Columns.Add("Col1", typeof(string)).Caption = "序号";

            cols.Add("Col1");
            showtable.Columns.Add("Title",typeof(string)).Caption="用户名";

            cols.Add("Title");
            showtable.Columns.Add("Col2", typeof(string)).Caption = "产品生产能力";
            cols.Add("Col2");
            DataColumn dcol= showtable.Columns.Add("y1990", typeof(double));

            dcol.Caption = "正常用电量";
            //dcol = showtable.Columns.Add("y1990_sum", typeof(double));
            //dcol.Caption = "正常用电量";
            //dcol.Expression = "IIF(Title='小计',sum(y1990),y1990)";

            cols.Add("y1990");
            showtable.Columns.Add("y1991", typeof(double)).Caption = "正常负荷";
            cols.Add("y1991");
            showtable.Columns.Add("Col11_Col12", typeof(double),"CONVERT(IIF(y1991=0,0,(y1990/y1991)*10000),'System.Int32')" ).Caption = "利用小时";
            colsum.Add("y1990");
            colsum.Add("y1991");

            if (list.Count > 0) {
                string col = "y" + list[0];
                cols.Add(col);
                colyears.Add(col);
                colsum.Add(col);
                colsum.Add(col+"_");
                showtable.Columns.Add(col, typeof(double)).Caption = list[0]+"年电量";
                showtable.Columns.Add(col+"_", typeof(double)).Caption = list[0] + "年负荷";
                string expression = string.Format("CONVERT(IIF({1}=0,0,({0}/{1})*10000),'System.Int32')", col, col + "_");
                showtable.Columns.Add(col+"_p", typeof(double), expression).Caption = "利用小时";

            }
        }
        private DataRow createRow(DataTable dt, DataRow row) {
            DataRow newrow = dt.NewRow();
            foreach (string col in cols) {
                if (dt.Columns[col].DataType == dataTable.Columns[col].DataType)
                    newrow[col] = row[col];
                else
                    newrow[col] = double.Parse(row[col].ToString());
            }
            dt.Rows.Add(newrow);
            return newrow;
        }
        private DataRow setData1(DataRow newrow, DataRow row) {           
            foreach (string col in cols) {
                if (showtable.Columns[col].DataType == dataTable.Columns[col].DataType)
                    newrow[col] = row[col];
                else
                    newrow[col] = double.Parse(row[col].ToString());
            }
            return newrow;
        }
        /// <summary>
        /// row2的负荷到newrow
        /// </summary>
        /// <param name="row1"></param>
        /// <param name="row2"></param>
        private void setData2(DataRow newrow, DataRow row2) {            
            foreach (string col in colyears) {
                    newrow[col+"_"] = row2[col];
            }
        }
        /// <summary>
        /// 设置负荷
        /// </summary>
        /// <param name="newrow"></param>
        /// <param name="row2"></param>
        private void setFh(DataRow newrow, string id) {
            DataRow row22 = findrow(dataTable2, "ID='" + id + splitstr + "'", "");
            if (row22 != null)
                setData2(newrow, row22);
        }
        string dxs = "零一二三四五六七八九十";
        private string getDX(int num) {
            if (num > 19) return num.ToString();
            string str = num.ToString();
            string ret = "";
            int count = 0;
            for (int i = str.Length - 1; i >=0; i--) {
                string s=str[i].ToString();
                if (count == 0) {
                    ret = dxs[int.Parse(s)].ToString();
                } else if (count == 1) {
                    ret =  "十" + ret;
                }
                count++;
            }
            return ret;
        }
        /// <summary>
        /// 查找大用户节点
        /// </summary>
        /// <param name="node"></param>
        private TreeListNode  findDYHParent(TreeListNode root) {
            TreeListNode retNode = null;
            foreach (TreeListNode node in root.Nodes) {
                if (node["Title"].ToString().Contains("大用户")) {
                    retNode = node;
                    break;
                }
                retNode = findDYHParent(node);
            }            
            return retNode;
        }
        /// <summary>
        /// 生成合计行
        /// </summary>
        /// <param name="dt"></param>
        private DataRow createSumRow(DataTable dt) {
            DataRow newrow = dt.NewRow();
            foreach (DataRow row in dt.Rows) {
                foreach (string col in colsum) {
                    double d1 = 0;
                    try { d1 = Convert.ToDouble(newrow[col]);} catch { }
                    
                    double d2 = 0;
                    try { d2=Convert.ToDouble(row[col]);} catch { }
                    newrow[col] = d1 + d2;
                }
            }
            return newrow;
        }
        private DataTable ConvertTreeListToDataTable511(DevExpress.XtraTreeList.TreeList treeList1) {
            
            
            //foreach (TreeListNode node in treeList1.Nodes[0].Nodes) {
            DataRow newrow = null;
            TreeListNode pnode = findDYHParent(treeList1.FocusedNode);
            int count2 = 0;//区县大用户计数
            int nt1 = 0;
                if (pnode != null && pnode.HasChildren) {
                    
                    TreeListNode node1 = pnode.Nodes[0];
                    TreeListNode findnode = null;
                    while (node1 != null) {
                        if (node1["Title"].ToString().Contains("新增")) {
                            findnode = node1;
                            break;
                        }
                        node1 = node1.NextNode;
                    }
                    if (findnode == null) return showtable;
                    string t1 = "";
                    
                    foreach (TreeListNode node2 in findnode.Nodes) {

                        DataRowView row2 = node2.TreeList.GetDataRecordByNode(node2) as DataRowView;
                        string t2 = row2["Col5"].ToString();
                        if (t2 != t1) {//新分类开始
                            count2 = 0;//分类计数清零
                            nt1++;
                            t1 = t2;
                            newrow = showtable.NewRow();
                            newrow["Title"] = t1;
                            newrow["Col1"] = "(" + getDX(nt1) + ")";
                            showtable.Rows.Add(newrow);
                        }
                        count2++;
                        newrow = createRow(showtable, row2.Row);
                        newrow["Col1"] = count2;
                        setFh(newrow, row2["ID"].ToString());
                    }
                }
            //}
            //现有小计
            DataRow row33 = createSumRow(showtable);
            row33["Title"] = "合计";
            row33["Col1"] = "(" + getDX(nt1 + 1) + ")";
            showtable.Rows.Add(row33);
            return showtable;
        }
        private DataTable ConvertTreeListToDataTable431(DevExpress.XtraTreeList.TreeList treeList1) {
            if (treeList1.Nodes.Count == 0) return showtable;
            int count1 = 0;//区县计数a
            foreach (TreeListNode nd in treeList1.Nodes)
            {
                if (nd.Nodes.Count < 4)
                    continue;

                foreach (TreeListNode node in nd.Nodes)
                {
                    TreeListNode pnode = findDYHParent(node);
                    if (pnode != null && pnode.HasChildren)
                    {
                        count1++;
                        DataRow newrow = showtable.NewRow();
                        newrow["Title"] = node["Title"];
                        newrow["Col1"] = getDX(count1);
                        showtable.Rows.Add(newrow);
                        int count2 = 0;//区县大用户计数
                        TreeListNode node1 = pnode.Nodes[0];
                        TreeListNode findnode = null;
                        while (node1 != null)
                        {
                            if (node1["Title"].ToString().Contains("新增"))
                            {
                                findnode = node1;
                                break;
                            }
                            node1 = node1.NextNode;
                        }
                        if (findnode == null) continue;
                        string t1 = "";
                        int nt1 = 0;
                        foreach (TreeListNode node2 in findnode.Nodes)
                        {

                            DataRowView row2 = node2.TreeList.GetDataRecordByNode(node2) as DataRowView;
                            string t2 = row2["Col5"].ToString();
                            if (t2 != t1)
                            {//新分类开始
                                count2 = 0;//分类计数清零
                                nt1++;
                                t1 = t2;
                                newrow = showtable.NewRow();
                                newrow["Title"] = t1;
                                newrow["Col1"] = "(" + getDX(nt1) + ")";
                                showtable.Rows.Add(newrow);
                            }
                            count2++;
                            newrow = createRow(showtable, row2.Row);
                            newrow["Col1"] = count2;
                            setFh(newrow, row2["ID"].ToString());
                        }
                    }
                }
            }
            
            //现有小计
            DataRow row33 = createSumRow(showtable);
            row33["Title"] = "合计";
            row33["Col1"] = getDX(count1 + 1);
            showtable.Rows.Add(row33);
            return showtable;
        }
      //private DataTable ConvertTreeListToDataTable(DevExpress.XtraTreeList.TreeList treeList1) {
      //      if(treeList1.Nodes.Count==0)return showtable;
      //      int count1 = 0;//区县计数
         
      //      foreach (TreeListNode node in treeList1.Nodes[0].Nodes) {
      //          TreeListNode pnode = findDYHParent(node);
      //          if (pnode != null && pnode.HasChildren) {
      //              count1++;
      //              DataRow newrow = showtable.NewRow();
      //              newrow["Title"] = node["Title"];
      //              newrow["Col1"] = getDX(count1);
      //              showtable.Rows.Add(newrow);
      //              int count2 = 0;//区县大用户计数
      //              TreeListNode node1 = pnode.Nodes[0];
      //              TreeListNode findnode = null;
      //              while (node1 != null) {
      //                  if (node1["Title"].ToString().Contains("现有")) {
      //                      findnode = node1;
      //                      break;
      //                  }
      //                  node1 = node1.NextNode;
      //              }
      //              if (findnode == null) continue;
      //              foreach (TreeListNode node2 in findnode.Nodes) {
      //                  count2++;
      //                  DataRowView row2 = node2.TreeList.GetDataRecordByNode(node2) as DataRowView;
      //                  newrow = createRow(showtable, row2.Row);
      //                  newrow["Col1"] = count2;
      //                  setFh(newrow,row2["ID"].ToString());
      //              }
      //          }
      //      }
      //      //现有小计
      //      DataRow row33 = createSumRow(showtable);
      //      row33["Title"] = "小计";
      //      row33["Col1"] = getDX(count1 + 1);
      //      showtable.Rows.Add(row33);
      //      return showtable;
      //  }
        private DataTable ConvertTreeListToDataTable(DevExpress.XtraTreeList.TreeList treeList1) {
            if(treeList1.Nodes.Count==0)return showtable;
            int count1 = 0;//区县计数
            foreach (TreeListNode nd in treeList1.Nodes)
            {
                //if (nd.Nodes.Count < 4)
                //    continue;

                foreach (TreeListNode node in nd.Nodes)
                {
                    TreeListNode pnode = findDYHParent(node);
                    if (pnode != null && pnode.HasChildren)
                    {
                        count1++;
                        DataRow newrow = showtable.NewRow();
                        newrow["Title"] = node["Title"];
                        newrow["Col1"] = getDX(count1);
                        showtable.Rows.Add(newrow);
                        int count2 = 0;//区县大用户计数
                        TreeListNode node1 = pnode.Nodes[0];
                        TreeListNode findnode = null;
                        while (node1 != null)
                        {
                            if (node1["Title"].ToString().Contains("现有"))
                            {
                                findnode = node1;
                                break;
                            }
                            node1 = node1.NextNode;
                        }
                        if (findnode == null) continue;
                        foreach (TreeListNode node2 in findnode.Nodes)
                        {
                            count2++;
                            DataRowView row2 = node2.TreeList.GetDataRecordByNode(node2) as DataRowView;
                            newrow = createRow(showtable, row2.Row);
                            newrow["Col1"] = count2;
                            setFh(newrow, row2["ID"].ToString());
                        }
                    }
                }
            }
            //现有小计
            DataRow row33 = createSumRow(showtable);
            row33["Title"] = "小计";
            row33["Col1"] = getDX(count1 + 1);
            showtable.Rows.Add(row33);
            return showtable;
        }

        private DataTable ResultDataTable(object p, object p_2) {

            return null;
        }
        #endregion 

        private void bt432_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            tj432();
        }

        private void bt433_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            tj433();
        }
        #region 复制节点
        TreeListNode cloneNode(TreeListNode cnode) {
            TreeListNode retnode = cnode.Clone() as TreeListNode;
            for (int i = cnode.Nodes.Count - 1; i >= 0; i--) {
                retnode.Nodes.Add(cloneNode(cnode.Nodes[i]));
            }
            return retnode;
        }
        private void setdatazero(Ps_History ps) {
            Type t = ps.GetType();
            foreach (PropertyInfo infor in t.GetProperties()) {
                if (infor.Name.StartsWith("y") && infor.Name.Length == 5) {
                    try { infor.SetValue(ps, 0, null); } catch { }
                }
            }
        }
        private void copyNode(TreeListNode cnode) {
            if(cnode.Level>3)return;
            DataRow row = (cnode.TreeList.GetDataRecordByNode(cnode) as DataRowView).Row;
            Ps_History ps = Itop.Common.DataConverter.RowToObject<Ps_History>(row);
            ps.ID = createID();
            setdatazero(ps);
            Common.Services.BaseService.Create<Ps_History>(ps);
            string pid = ps.ID;
            ps.ID = pid + splitstr;
            ps.ParentID = ps.ParentID + splitstr;
            ps.Forecast = type32;
            ps.ForecastID = type31;
            Common.Services.BaseService.Create<Ps_History>(ps);
            
           TreeListNode nd1=null;
            for(int i=cnode.Nodes.Count -1;i>=0;i--) {
                nd1 = cnode.Nodes[i];
                 nd1["ParentID"] = pid;
                 copyNode(nd1);                 
            }            
        }
        private void btCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 
        {
            //复制节点及子节点
            TreeListNode tln = treeList1.FocusedNode;

            if (tln == null || tln.Level == 0)
            {
                MessageBox.Show("您所选的结点不能复制！");
                return;
            }
  
            //if (MsgBox.ShowYesNo("请确认复制["+tln.GetDisplayText("Title")+"]及子类？") != DialogResult.Yes) return;
             FormHistory2_add frm = new FormHistory2_add();
            frm.Text = "复制分类";
            frm.TypeTitle = tln["Title"].ToString();
            if (frm.ShowDialog() == DialogResult.OK) {
                TreeListNode copy = cloneNode(tln);
                copy["Title"] = frm.TypeTitle;
                copy["Sort"] = frm.Sortid;
                copyNode(copy);
                LoadData();
            }
        }
        #endregion
        private void bt532_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null || tln.Level != 1) {
                MsgBox.Show("请先选择分区后重试！");
                return;
            }
            tj532();
        }
        #region 粘贴剪切板数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pnode">父节点</param>
        /// <param name="column">列名</param>
        /// <param name="findstr">查找字符</param>
        /// <param name="f1">是否深度查找</param>
        /// <returns></returns>
        ///         
        private TreeListNode findByColumnValue(TreeListNode pnode, string column,string findstr, bool f1) {
            TreeListNode retnode = null;
            foreach (TreeListNode node in pnode.Nodes) {
                if (node[column].ToString().Contains(findstr)) {
                    retnode = node;
                    break;
                }
                if (f1) {
                    retnode = findByColumnValue(node, column, findstr, f1);
                    if (retnode != null) break;
                }
            }
            return retnode;
        }
        private void createRow(string title) {

        }
        private object getSummaryValue(string columnid, TreeListNodes nodes) {
            double ret=0;
            foreach (TreeListNode node in nodes) {
                double d1 = 0;
                double.TryParse(node[columnid].ToString(), out d1);
                ret += d1;
            }
            return ret;
        }
        private void updateSummaryNode(TreeListNode pnode) {
            foreach (string col in pasteCols) {
                if(col.StartsWith("y")){
                    try {
                        object obj = getSummaryValue(col, pnode.Nodes);
                        pnode.SetValue(col, obj);
                    } catch { }
                }
            }
            updateNode(pnode);
            if (pnode.ParentNode != null)
                updateSummaryNode(pnode.ParentNode);
        }
        
        private void updateNode(TreeListNode node) {
            DataRow row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;
            Ps_History v = DataConverter.RowToObject<Ps_History>(row);
            Services.BaseService.Update<Ps_History>(v);
        }
        private void pasteData(TreeListNode tln) {
            string s1 = tln["Title"].ToString();
            //if (!s1.StartsWith("现有")) return;

            IDataObject obj1 = Clipboard.GetDataObject();
            string text = obj1.GetData("Text").ToString();
            string[] lines = text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            tln.TreeList.BeginInit();
            for (int i = 0; i < lines.Length; i++) {
                string[] items = lines[i].Split(new string[] { "\t" }, StringSplitOptions.None);
                if (items.Length != pasteCols.Count) continue;
                TreeListNode fnode = findByColumnValue(tln, "Title", items[0], false);
                if (fnode == null) continue;
                for (int j = 0; j < pasteCols.Count; j++) {
                    try {
                        if (items[j] == "") items[j] = "0";
                        fnode.SetValue(pasteCols[j], items[j]);                    
                    } catch { }
                }
                updateNode(fnode);
            }
            updateSummaryNode(tln);
            tln.TreeList.EndInit();
        }
        #endregion
        private void treeList1_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control && e.KeyCode == Keys.V) {
                TreeListNode tln = treeList1.FocusedNode;
                if (tln == null ) {
                    return;
                }
                pasteData(tln);
            }
        }
        //设定年份
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            FormYearSet fys = new FormYearSet();
            fys.TYPE = "区县发展实绩";
            fys.PID = ProjectUID;
            if (fys.ShowDialog() != DialogResult.OK)
                return;
            firstyear = fys.SYEAR;
            endyear = fys.EYEAR;
            LoadData();
        }
        //数据拷贝
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            FormProjectDataCopy fpd = new FormProjectDataCopy();
            fpd.ProjectUID = ProjectUID;

            if (fpd.ShowDialog() != DialogResult.OK)
                return;

            string pid = fpd.ProjectUID;

            Ps_History psp_Type1 = new Ps_History();
            psp_Type1.Forecast = type;
            psp_Type1.Col4 = pid;
            //电量
            IList<Ps_History> li1 = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type1);

           
            psp_Type1.Forecast = type32;
            psp_Type1.Col4 = pid;
            //负荷
            IList<Ps_History> li2 = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type1);
           

            foreach (Ps_History ph in li1) {                    
                ph.ParentID = ph.ParentID.Replace(pid, ProjectUID);
                ph.Col4 = ProjectUID;
                ph.ID = ph.ID.Replace(pid, ProjectUID);
                try {
                    Services.BaseService.Create<Ps_History>(ph);
                } catch { }

            }
            foreach (Ps_History ph in li2) {
                ph.ParentID = ph.ParentID.Replace(pid, ProjectUID);
                ph.Col4 = ProjectUID;
                ph.ID = ph.ID.Replace(pid, ProjectUID);
                try {
                    Services.BaseService.Create<Ps_History>(ph);
                } catch { }
            }

            LoadData();
        }

        private void bt431_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            tj431();
        }

        private void bt511_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null || tln.Level != 1) {
                MsgBox.Show("请先选择分区后重试！");
                return;
            }
            tj511();
        }
        private void cacsumsehui(string strname)
        {
            Ps_History v;
            DataRow[] drlist = dataTable.Select("Title like '%常规%'");
            //foreach (DataColumn dc in dataTable.Columns)
            //{
            double value = 0;
            //if (dc.ColumnName.IndexOf("y") != 0)
            //{
            //    continue;
            //}
            foreach (DataRow dr in drlist)
            {
                
                if (treeList1.FindNodeByKeyID(dr["ID"]).ParentNode.ParentNode == null)
                    continue;
                try
                {
                    value +=Math.Round( Convert.ToDouble(dr[strname]),2);
                }
                catch { }
            }
                foreach (DataRow dr in drlist)
                {
                    TreeListNode node = treeList1.FindNodeByKeyID(dr["ID"]).ParentNode.ParentNode;
                              if (node == null)
                              {
                                  int i = dataTable.Rows.IndexOf(dr);

                                 
                                  dataTable.Rows[i][strname]= value;
                                  //DataRow row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;
                                  v = DataConverter.RowToObject<Ps_History>(dataTable.Rows[i]);
                                  Common.Services.BaseService.Update<Ps_History>(v);
                              }
                    }
               
           
          
            DataRow[] drlist2 = dataTable.Select("Title like '%大用户%'");
          
            value = 0;
           
            foreach (DataRow dr in drlist2)
            {
                if (treeList1.FindNodeByKeyID(dr["ID"]).ParentNode.ParentNode == null)
                    continue;
                    
                try
                {
                    value +=Math.Round( Convert.ToDouble(dr[strname]),2);
                }
                catch { }
            }
            foreach (DataRow dr in drlist2)
            {
                TreeListNode node = treeList1.FindNodeByKeyID(dr["ID"]).ParentNode.ParentNode;
                if (node == null)
                {
                    int i = dataTable.Rows.IndexOf(dr);
                   
                    dataTable.Rows[i][strname] = value;
                    v = DataConverter.RowToObject<Ps_History>(dataTable.Rows[i]);
                    Common.Services.BaseService.Update<Ps_History>(v);
                }
            }
               
            //  dataTable.Rows[2][dc.ColumnName] = value;
            //           }

        }
        private void cacsumsehui2(string strname)
        {
            Ps_History v;
            DataRow[] drlist3 = dataTable2.Select("Title like '%常规%'");
            //foreach (DataColumn dc in dataTable2.Columns)
            //{
            double value = 0;
            //if (dc.ColumnName.IndexOf("y") != 0)
            //{
            //    continue;
            //}
            foreach (DataRow dr in drlist3)
            {
                if (treeList2.FindNodeByKeyID(dr["ID"]).ParentNode.ParentNode == null)
                    continue;
                try
                {
                    value +=Math.Round( Convert.ToDouble(dr[strname]),2);
                }
                catch { }
            }
            foreach (DataRow dr in drlist3)
            {
                TreeListNode node = treeList2.FindNodeByKeyID(dr["ID"]).ParentNode.ParentNode;
                if (node == null)
                {
                    int i = dataTable2.Rows.IndexOf(dr);

                    dataTable2.Rows[i][strname] = value;
                    v = DataConverter.RowToObject<Ps_History>(dataTable2.Rows[i]);
                    Common.Services.BaseService.Update<Ps_History>(v);
                }
            }
            DataRow[] drlist4 = dataTable2.Select("Title like '%大用户%'");
            
            value = 0;
            
            foreach (DataRow dr in drlist4)
            {
                if (treeList2.FindNodeByKeyID(dr["ID"]).ParentNode.ParentNode == null)
                    continue;
                try
                {
                    value +=Math.Round( Convert.ToDouble(dr[strname]),2);
                }
                catch { }
            }
            foreach (DataRow dr in drlist4)
            {

                TreeListNode node = treeList2.FindNodeByKeyID(dr["ID"]).ParentNode.ParentNode;
                if (node == null)
                {
                    int i = dataTable2.Rows.IndexOf(dr);

                    dataTable2.Rows[i][strname] = value;
                    v = DataConverter.RowToObject<Ps_History>(dataTable2.Rows[i]);
                    Common.Services.BaseService.Update<Ps_History>(v);
                }
            }
            //  dataTable.Rows[2][dc.ColumnName] = value;
            //}
        }
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (DataColumn dc in dataTable.Columns)
            {
                if (dc.ColumnName.IndexOf("y") != 0)
                {
                    continue;
                }
                cacsumsehui(dc.ColumnName);
                cacsumsehui2(dc.ColumnName);
            }
          
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode focusedNode = treeList1.FocusedNode;

            if (focusedNode == null)
            {
                return;
            }

            if (focusedNode["Title"].ToString().Contains("新增"))
            {
                newHistory();
                return;
            }

            FormHistory2_add frm = new FormHistory2_add();
            frm.Text = "增加分类";
            if (focusedNode.Nodes.Count > 0)
                frm.Sortid = (int)focusedNode.Nodes[focusedNode.Nodes.Count - 1]["Sort"] + 1;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_History pf = new Ps_History();
                pf.ID = createID();
                pf.Forecast = type;
                pf.ForecastID = type1;
                pf.Title = frm.TypeTitle;
                pf.Col2 = frm.COL10;
                //pf.y1990 =frm.COL11;
                //pf.y1991 = frm.COL12;
              
                pf.Col4 = ProjectUID;
                pf.Sort = frm.Sortid;
                //object obj = Services.BaseService.GetObject("SelectPs_HistoryMaxID", null);
                //if (obj != null)
                //    pf.Sort = ((int)obj) + 1;
                //else
                //    pf.Sort = 1;

                Ps_History pf2 = new Ps_History();
                pf2.ID = pf.ID + splitstr;
                pf2.Forecast = type32;
                pf2.ForecastID = type31;
                pf2.Title = pf.Title;
                if (pf.Title.Contains("用电量"))
                    pf2.Title = pf.Title.Replace("用电量", "负荷");
                pf2.Col4 = ProjectUID;
                pf2.Sort = frm.Sortid;
                try
                {
                    Services.BaseService.Create<Ps_History>(pf);
                    Services.BaseService.Create<Ps_History>(pf2);
                    treeList1.BeginInit();
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf, dataTable.NewRow()));
                    treeList1.EndInit();
                    treeList2.BeginInit();
                    dataTable2.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf2, dataTable2.NewRow()));
                    treeList2.EndInit();
                    
                    //LoadData();
                    //insertNode(focusedNode, pf);
                }
                catch (Exception ex) { MsgBox.Show("增加分类出错：" + ex.Message); }
            }
        }
    }
}