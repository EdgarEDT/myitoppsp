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
    /// 分区负荷分布预测表
    /// </summary>
    public partial class FormForecast3 : FormBase
    {
        string type1 = "4";
        int type = 4;
        string type31 = "5";
        int type32 = 5;
        DataTable dataTable = new DataTable();
        DataTable dataTable2 = new DataTable();
        bool bLoadingData = false;
        bool _canEdit = true;
        int firstyear = 2008;
        int endyear = 2020;
        string splitstr="-";
        string where1 = "";
        string where2 = "";
        private Ps_forecast_list forecastReport = null;
        bool _isSelect = false;

        string pid = "";
        public string PID {
            set { pid = value; }
            get { return pid; }

        }

        DevExpress.XtraGrid.GridControl _gridControl;

        public DevExpress.XtraGrid.GridControl GridControl {
            get { return _gridControl; }
            set { _gridControl = value; }
        }

        public bool IsSelect {
            get { return _isSelect; }
            set { _isSelect = value; }
        }

        public bool CanEdit {
            get { return _canEdit; }
            set { _canEdit = value; EditRight = value; }
        }
        bool _canPrint = true;

        public bool CanPrint {
            get { return _canPrint; }
            set { _canPrint = value; }
        }
        private bool AddRight = false;
        private bool EditRight = false;
        public bool ADdRight {
            get { return AddRight; }
            set { AddRight = value; }
        }
        private bool DeleteRight = false;
        public bool DEleteRight {
            get { return DeleteRight; }
            set { DeleteRight = value; }
        }
        public FormForecast3() {
            InitializeComponent();
            forecastReport = new Ps_forecast_list();
            forecastReport.ID = type1;
            treeList1.OptionsView.AutoWidth = false;
            barButtonItem6.Glyph = Itop.ICON.Resource.关闭;
        }

        private void HideToolBarButton() {
            if (!base.AddRight)
            {

                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }
            if (!base.EditRight)
            {
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btCopy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }
            if (!base.DeleteRight)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }
        }

        private void Form8Forecast_Load(object sender, EventArgs e) {
            Application.DoEvents();
            Ps_YearRange py = new Ps_YearRange();
            py.Col4 = "负荷分布预测";
            py.Col5 = ProjectUID;
            HideToolBarButton();
            IList<Ps_YearRange> li = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);
            if (li.Count > 0) {
                firstyear = li[0].StartYear;
                endyear = li[0].FinishYear;
            } else {
                firstyear = 2008;
                endyear = 2020;
                py.BeginYear = 2008;
                py.FinishYear = endyear;
                py.StartYear = firstyear;
                py.EndYear = 2060;
                py.ID = Guid.NewGuid().ToString();
                Services.BaseService.Create<Ps_YearRange>(py);
            }
            LoadData();
            //RefreshChart();
            where1 = string.Format(" Forecast ={0} and ForecastID ='{1}' ", type, forecastReport.ID);
            where2 = string.Format(" Forecast ={0} and ForecastID ='{1}' ", type32, forecastReport.ID);
            initpasteCols();

        }
        /// <summary>
        /// 初始化粘贴列
        /// </summary>
        private void initpasteCols() {
            pasteCols.Add("Title");
            for (int i = firstyear; i <= endyear; i++) {
                pasteCols.Add("y" + i);
            }
        }
        List<string> pasteCols = new List<string>();//数据粘贴列
        private void LoadData() {
            //this.splitContainerControl1.SplitterPosition = (2* this.splitterControl1.Width) / 3;
            //this.splitContainerControl2.SplitterPosition = splitContainerControl2.Height / 2;
            //this.splitContainerControl2.Visible = false;
            bLoadingData = true;
            if (dataTable != null) {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
                //dataTable2.Columns.Clear();
                //treeList2.Columns.Clear();
            }
            AddFixColumn();
            for (int i = firstyear; i <= endyear; i++) {
                AddColumn(i);
            }
            //treeList1.Columns.AssignTo(treeList2.Columns);
            
            load1();
            //load2();
            bLoadingData = false;
        }
        
        private void load1() {

            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.Forecast = type;
            psp_Type.ForecastID = forecastReport.ID;
            psp_Type.Col4 = this.ProjectUID;
            IList<Ps_Forecast_Math> listTypes = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByCol4", psp_Type);

            if (listTypes.Count == 0) {
                Ps_Forecast_Math pf = new Ps_Forecast_Math();
                pf.ID = createID();
                pf.ParentID = "";
                pf.Sort = 0;
                pf.Forecast = type;
                pf.ForecastID = type1;
                pf.Title = "负荷分布";
                pf.Col4 = ProjectUID;
                Services.BaseService.Create<Ps_Forecast_Math>(pf);
                listTypes.Add(pf);
                Ps_Forecast_Math pf2 = new Ps_Forecast_Math();
                pf2.ID = createID();
                pf2.ParentID = pf.ID;
                pf2.Sort = 0;
                pf2.Forecast = type;
                pf2.ForecastID = type1;
                pf2.Title = "区县1";
                pf2.Col4 = ProjectUID;
                Services.BaseService.Create<Ps_Forecast_Math>(pf2);
                listTypes.Add(pf2);
                pf = pf2;
                pf2 = new Ps_Forecast_Math();
                pf2.ID = createID();
                pf2.ParentID = pf.ID;
                pf2.Sort = 0;
                pf2.Forecast = type;
                pf2.ForecastID = type1;
                pf2.Title = "中心城区";
                pf2.Col2 = "中心城区";
                pf2.Col4 = ProjectUID;
                Services.BaseService.Create<Ps_Forecast_Math>(pf2);
                listTypes.Add(pf2);
                pf2 = new Ps_Forecast_Math();
                pf2.ID = createID();
                pf2.ParentID = pf.ID;
                pf2.Sort = 1;
                pf2.Forecast = type;
                pf2.ForecastID = type1;
                pf2.Col4 = ProjectUID;
                pf2.Title = "市区外围";
                pf2.Col2 = "市区外围";
                Services.BaseService.Create<Ps_Forecast_Math>(pf2);
                listTypes.Add(pf2);
            }


            dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_Forecast_Math));
            
            treeList1.DataSource = dataTable;
            treeList1.Columns["Sort"].SortOrder = SortOrder.Ascending;
            Application.DoEvents();
            if (treeList1.Nodes.Count > 0)
                treeList1.Nodes[0].Expanded = false;
            
        }
        private void load2() {
            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.Forecast = type32;
            psp_Type.ForecastID = forecastReport.ID;
            IList<Ps_Forecast_Math> listTypes = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);
           
            dataTable2 = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_Forecast_Math));
            treeList2.DataSource = dataTable2;
            treeList2.Columns["Sort"].SortOrder = SortOrder.Ascending;
            Application.DoEvents();
            if (treeList2.Nodes.Count > 0)
            treeList2.Nodes[0].Expanded = true;
            
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
            //column = new TreeListColumn();
            //column.FieldName = "Col2";
            //column.Caption = "产品生产能力";
            //column.VisibleIndex = 1;
            //column.Width = 150;
            //column.OptionsColumn.AllowEdit = false;
            //column.OptionsColumn.AllowSort = false;
            //this.treeList1.Columns.AddRange(new TreeListColumn[] {
            //column});
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
            column.FieldName = "y1992";
            column.Caption = "面积";
            column.VisibleIndex = 2;
            column.Width = 50;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
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
        

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.Close();
        }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

            //FileClass.ExportToExcelOld(this.forecastReport.Title, "", this.gridControl1);
        }

        private void Save() {
            //保存
            
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
            double d=0;
            if (!double.TryParse(e.Value.ToString(),out d)) return;
            try {
                CalculateSum(e.Node, e.Column);
            } catch { }
            //Save();
            //RefreshChart();
        }
        private void treeList2_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e) {
            if (e.Column.FieldName.Substring(0, 1) != "y") return;
            CalculateSum(e.Node, e.Column);
        }
        #region 分类管理
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TreeListNode focusedNode = treeList1.FocusedNode;

            if (focusedNode == null) {
                return;
            }

            //if (focusedNode.ParentNode == null)
            //    return;

            FormForecast3_add frm = new FormForecast3_add();
            frm.Text = "增加分类";

            if (frm.ShowDialog() == DialogResult.OK) {
                Ps_Forecast_Math pf = new Ps_Forecast_Math();
                pf.ID = createID();
                pf.Forecast = type;
                pf.ForecastID = type1;
                pf.Title = frm.TypeTitle;
                pf.Col2 = frm.COL2;
                pf.Col3 = frm.COL3;
                pf.y1992 = frm.y1992;
                pf.ParentID = focusedNode["ID"].ToString();
                pf.Col4 = ProjectUID;
                pf.Sort = frm.Sortid;
                try {
                    Services.BaseService.Create<Ps_Forecast_Math>(pf);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf, dataTable.NewRow()));
                    
                } catch (Exception ex) { MsgBox.Show("增加分类出错：" + ex.Message); }
            }

        }

        private string createID() {
            string str =Guid.NewGuid().ToString();
            return str.Substring(str.Length -12);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
                return;
            if ( tln.HasChildren)
                return;


            if (MsgBox.ShowYesNo("是否删除分类 " + tln.GetValue("Title") + "？") == DialogResult.Yes) {
                Ps_Forecast_Math pf = new Ps_Forecast_Math();
                pf.ID = tln["ID"].ToString();

                try {
                    //DeletePSP_ValuesByType里面删除数据和分类
                    Common.Services.BaseService.Delete<Ps_Forecast_Math>(pf);
                    pf.ID = pf.ID + splitstr;
                    Common.Services.BaseService.Delete<Ps_Forecast_Math>(pf);
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

            if (treeList1.FocusedNode.ParentNode == null) {
                MsgBox.Show("该目录不可修改");
                return;
            }


            FormForecast3_add frm = new FormForecast3_add();
            frm.TypeTitle = treeList1.FocusedNode.GetValue("Title").ToString();
            frm.Text = "修改分类";
            Ps_Forecast_Math pf = new Ps_Forecast_Math();
            pf = Services.BaseService.GetOneByKey<Ps_Forecast_Math>(treeList1.FocusedNode["ID"].ToString());

            frm.COL2 = pf.Col2;
            frm.Sortid = pf.Sort;
            frm.COL3 = pf.Col3;
            frm.y1992 = pf.y1992;

            if (frm.ShowDialog() == DialogResult.OK) {
                                //TreeNodeToDataObject<Ps_Forecast_Math>(v, treeList1.FocusedNode);
                pf.Title = frm.TypeTitle;
                pf.Col2 = frm.COL2;
                pf.Col3 = frm.COL3;
                pf.y1992 = frm.y1992;
                pf.Sort = frm.Sortid;
                try {
                    treeList1.FocusedNode["Col2"] = pf.Col2;
                    treeList1.FocusedNode["Col3"] = pf.Col3; 
                    try { treeList1.FocusedNode.SetValue("y1992", pf.y1992); } catch { }
                    treeList1.FocusedNode.SetValue("Title", pf.Title); 
                    treeList1.FocusedNode.SetValue("Sort", pf.Sort);
                    Common.Services.BaseService.Update<Ps_Forecast_Math>(pf);

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
            if (treeList1.FocusedNode.HasChildren || !base.EditRight)
            {
                e.Cancel = true;
            }
        }
        private void treeList2_ShowingEditor(object sender, CancelEventArgs e) {
            if (treeList2.FocusedNode.HasChildren) {
                e.Cancel = true;
            }
        }
        private void CalculateSum(TreeListNode node, TreeListColumn column) {
            Ps_Forecast_Math v = Services.BaseService.GetOneByKey<Ps_Forecast_Math>(node["ID"].ToString());
            TreeNodeToDataObject<Ps_Forecast_Math>(v, node);
            Common.Services.BaseService.Update<Ps_Forecast_Math>(v);
            TreeListNode parentNode = node.ParentNode;
            if (parentNode == null) {
                return;
            }
            if (node["Title"].ToString() == "同时率") return;
            double sum = 0;
            foreach (TreeListNode nd in parentNode.Nodes) {
                if (nd["Title"].ToString() == "同时率") continue;
                object value = nd.GetValue(column.FieldName);
                if (value != null && value != DBNull.Value) {
                    sum += Convert.ToDouble(value);
                }
            }
            if (sum != 0) {
                parentNode.SetValue(column.FieldName, sum);
                v = Services.BaseService.GetOneByKey<Ps_Forecast_Math>(parentNode["ID"].ToString());
                TreeNodeToDataObject<Ps_Forecast_Math>(v, parentNode);

                Common.Services.BaseService.Update<Ps_Forecast_Math>(v);
            } else
                parentNode.SetValue(column.FieldName, null);
            CalculateSum(parentNode, column);
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

            Ps_Forecast_Math v = Services.BaseService.GetOneByKey<Ps_Forecast_Math>(id);
            if (v != null) {
                v.ParentID = e.Node["ParentID"].ToString();
                Services.BaseService.Update<Ps_Forecast_Math>(v);
                v = Services.BaseService.GetOneByKey<Ps_Forecast_Math>(id + splitstr);
                if (v != null) {
                    v.ParentID = e.Node["ParentID"].ToString() + splitstr;
                    Services.BaseService.Update<Ps_Forecast_Math>(v);
                    reloadTree2();
                }
            }
        }
        #region 统计表
        /// <summary>
        /// 现有大工业用户产能及用电情况
        /// </summary>
        private void tj432() {
            //FormChooseYears frm = new FormChooseYears();
            //foreach (TreeListColumn column in treeList1.Columns) {
            //    if (column.Caption.IndexOf("年") > 0) {
            //        frm.ListYearsForChoose.Add((int)column.Tag);
            //    }
            //}

            //if (frm.ShowDialog() == DialogResult.OK) {
            Formtj432 f = new Formtj432();
            f.Text = "现有大工业用户产能及用电情况";
            f.CanPrint = base.EditRight;
            //f.Text = Title = "本地区" + frm.ListChoosedYears[0].Year + "～" + frm.ListChoosedYears[frm.ListChoosedYears.Count - 1].Year + "年分区县供电实绩";
            //f.GridDataTable = ResultDataTable(ConvertTreeListToDataTable(treeList1), frm.ListYearsForChoose);
            List<int> list = new List<int>();
            list.Add(endyear);
            setcols432(list);
            f.GridDataTable = ConvertTreeListToDataTable(treeList1);
            f.ShowDialog();
            //}
        }
        /// <summary>
        /// 全市常规增长率+大用户法预测结果
        /// </summary>
        private void tj433() {
            FormChooseYears1 frm = new FormChooseYears1();
            foreach (TreeListColumn column in treeList1.Columns) {
                if (column.Caption.IndexOf("年") > 0) {
                    frm.ListYearsForChoose.Add((int)column.Tag);
                }
            }
            
            if (frm.ShowDialog() == DialogResult.OK) {
                
                Formtj432 f = new Formtj432();
                f.Text = "全市常规增长率+大用户法预测结果";
                f.CanPrint = base.EditRight;
                //f.Text = Title = "本地区" + frm.ListChoosedYears[0].Year + "～" + frm.ListChoosedYears[frm.ListChoosedYears.Count - 1].Year + "年分区县供电实绩";
                //f.GridDataTable = ResultDataTable(ConvertTreeListToDataTable(treeList1), frm.ListYearsForChoose);
                setcols433(frm.DT.Rows);
                f.GridDataTable = ConvertTreeListToDataTable433(treeList1);
                DialogResult dr = f.ShowDialog();
            }

        }
        /// <summary>
        /// 区县常规增长率+大用户法预测结果
        /// </summary>
        private void tj532() {
            FormChooseYears frm = new FormChooseYears();
            foreach (TreeListColumn column in treeList1.Columns) {
                if (column.Caption.IndexOf("年") > 0) {
                    frm.ListYearsForChoose.Add((int)column.Tag);
                }
            }
            if (frm.ShowDialog() == DialogResult.OK) {
                setcols433(frm.DT.Rows);
                Formtj512 f = new Formtj512();
                f.Text = treeList1.FocusedNode["Title"].ToString() + "负荷分布预测表";
                f.CanPrint = base.EditRight;
                
                f.GridDataTable = ConvertTreeListToDataTable532(treeList1);
                DialogResult dr = f.ShowDialog();
            }

        }

        private DataTable ConvertTreeListToDataTable532(DevExpress.XtraTreeList.TreeList treeList1) {

            TreeListNode node = treeList1.FocusedNode;
            DataRow row1 = ((DataRowView)node.TreeList.GetDataRecordByNode(node)).Row;
            
            //setFh(newrow, node["ID"].ToString());
            AddNodeDataToDataTable433(showtable, node, cols, "");
            DataRow newrow = createRow(showtable, row1);
            newrow["Title"] = "";
            newrow["Col2"] = "合计";
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
                DataRow newRow = dt.NewRow();            
                string space = string.Copy(padstr);
                foreach (string colID in listColID) {
                    newRow[colID] = nd[colID];
                }

                ArrayList al = new ArrayList();
                Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
                psp_Type.Forecast = type;
                psp_Type.ForecastID = forecastReport.ID;
                psp_Type.Col4 = this.ProjectUID;
                IList<Ps_Forecast_Math> listTypes = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByCol4", psp_Type);

                foreach (Ps_Forecast_Math pyl in listTypes)
                {
                    if (al.Contains(pyl.Col2))
                    {
                        continue;
                    }
                    else
                        al.Add(pyl.Col2);
                }
                string str = newRow["Title"].ToString();
                if (al.Contains(str))
                    newRow["Title"] = "小计";
                str = node["Title"].ToString();
                if (al.Contains(str))
                {
                    newRow["Col2"] = "";// node["Col2"];
                    newRow["y1992"] = DBNull.Value;// node["y1992"];
                    newRow["Col3"] = "";// node["Col3"];
                }

                //string str = newRow["Title"].ToString();
                //if (str.Contains("中心城区") || str.Contains("市区外围"))
                //    newRow["Title"] = "小计";
                //str = node["Title"].ToString();
                //if (str.Contains("中心城区") || str.Contains("市区外围"))
                //{
                //    newRow["Col2"] = "";// node["Col2"];
                //    newRow["y1992"] = DBNull.Value;// node["y1992"];
                //    newRow["Col3"] = "";// node["Col3"];
                //}
                
                dt.Rows.Add(newRow);                
            
                AddNodeDataToDataTable433(dt, nd, listColID,padstr);
            }
        }
        
        private void createHj433(TreeListNode node ,IList<Ps_Forecast_Math> list1,IList<Ps_Forecast_Math> list2) {
            DataRow newrow = showtable.NewRow();
            string title =node["Title"].ToString();
            Ps_Forecast_Math ps1 =null;//电量
            Ps_Forecast_Math ps2 = null;//负荷
            foreach (Ps_Forecast_Math ps in list1) {
                if (ps.Title == title) {
                    ps1 = ps;
                    break;
                }
            }
            if (ps1 == null) return;
            foreach (Ps_Forecast_Math ps in list2) {
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
            IList<Ps_Forecast_Math> list = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathGroupList", where1);
            IList<Ps_Forecast_Math> list2 = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathGroupList", where2);
            
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
                    DataRow row22 = findrow(showtable, " trim(title) like '现有%' ","");
                    if (row22 != null)
                        row22["Title"] = "　"+node2["Title"]+"现有";
                    row22 = findrow(showtable, " trim(title) like '新增%' ", "");
                    if (row22 != null)
                        row22["Title"] = "　" + node2["Title"] + "新增";
                }
                
            }
            #region 计算增长率
            DataTable dtReturn = showtable;
            DataColumn columnBegin = null;
            DataColumn columnPre = null;
            foreach (DataColumn column in dtReturn.Columns) {
                
                if (column.Caption.IndexOf("年电量") > 0) {
                    if (columnBegin == null) {
                        columnBegin = column;
                    }
                } else if (column.ColumnName.IndexOf("增长率") > 0) {
                    int n=1;
                    if (columnPre.Caption.IndexOf("年负荷") >= 0)
                        n = 2;
                    DataColumn columnEnd = dtReturn.Columns[column.Ordinal - n];

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
                columnPre = column;
            }
            #endregion
            return showtable;
        }
        private void setcols433(ICollection rows) {
            showtable = new DataTable();

            cols.Clear();
            colyears.Clear();
            showtable.Columns.Add("Col2", typeof(string)).Caption = "供电分区";
            showtable.Columns.Add("y1992", typeof(string)).Caption = "面积";
            showtable.Columns.Add("Col3", typeof(string)).Caption = "分类";
            cols.Add("Col2");
            cols.Add("Col3");
            cols.Add("y1992");
            showtable.Columns.Add("Title", typeof(string)).Caption = "类型";
            cols.Add("Title");
            string col = "";
            int lastyear = 0;
            foreach (DataRow row in rows) {
                int year=0;
                if (row["B"].ToString() == "True")
                    year =Convert.ToInt32(row["A"].ToString().Replace("年", ""));
                else
                    continue;
                lastyear = year;
                col = "y" + year;
                cols.Add(col);
                
                showtable.Columns.Add(col, typeof(double)).Caption = year + "";
                colyears.Add(col);
                //showtable.Columns.Add(col + "_", typeof(double)).Caption = year + "年负荷";
                //if (row["C"].ToString() == "True") {
                //    showtable.Columns.Add(col +"_增长率", typeof(double)).Caption = "增长率";
                //}
                
            }
            //if (col != "") {//最后一年加负荷列
            //    colyears.Add(col);
            //    showtable.Columns.Add(col + "_", typeof(double)).Caption = lastyear + "年负荷";
            //}

            
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
        private void setcols432(List<int> list) {
            showtable =new DataTable();
            
            cols.Clear();
            colyears.Clear();
            colsum.Clear();
            showtable.Columns.Add("Col1", typeof(string)).Caption = "序号";

            cols.Add("Col1");
            showtable.Columns.Add("Title",typeof(string)).Caption="用户名";

            cols.Add("Title");
            //showtable.Columns.Add("Col2", typeof(string)).Caption = "产品生产能力";
            //cols.Add("Col2");
            

            if (list.Count > 0) {
                string col = "y" + list[0];
                cols.Add(col);
                colyears.Add(col);
                colsum.Add(col);
                colsum.Add(col+"_");
                showtable.Columns.Add(col, typeof(double)).Caption = list[0]+"年电量";
                showtable.Columns.Add(col+"_", typeof(double)).Caption = list[0] + "年负荷";

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
        private DataTable ConvertTreeListToDataTable(DevExpress.XtraTreeList.TreeList treeList1) {
            if(treeList1.Nodes.Count==0)return showtable;
            int count1 = 0;//区县计数
            foreach (TreeListNode node in treeList1.Nodes[0].Nodes) {
                TreeListNode pnode = findDYHParent(node);
                if (pnode != null && pnode.HasChildren) {
                    count1++;
                    DataRow newrow = showtable.NewRow();
                    newrow["Title"] = node["Title"];
                    newrow["Col1"] = getDX(count1);
                    showtable.Rows.Add(newrow);
                    int count2 = 0;//区县大用户计数
                    TreeListNode node1 = pnode.Nodes[0];
                    TreeListNode findnode = null;
                    while (node1 != null) {
                        if (node1["Title"].ToString().Contains("现有")) {
                            findnode = node1;
                            break;
                        }
                        node1 = node1.NextNode;
                    }
                    if (findnode == null) continue;
                    foreach (TreeListNode node2 in findnode.Nodes) {
                        count2++;
                        DataRowView row2 = node2.TreeList.GetDataRecordByNode(node2) as DataRowView;
                        newrow = createRow(showtable, row2.Row);
                        newrow["Col1"] = count2;
                        setFh(newrow,row2["ID"].ToString());
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
        private void setdatazero(Ps_Forecast_Math ps) {
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
            Ps_Forecast_Math ps = Itop.Common.DataConverter.RowToObject<Ps_Forecast_Math>(row);
            ps.ID = createID();
            setdatazero(ps);
            Common.Services.BaseService.Create<Ps_Forecast_Math>(ps);
            string pid = ps.ID;
                        
           TreeListNode nd1=null;
            for(int i=cnode.Nodes.Count -1;i>=0;i--) {
                nd1 = cnode.Nodes[i];
                 nd1["ParentID"] = pid;
                 copyNode(nd1);                 
            }            
        }
        private void btCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //复制节点及子节点
            TreeListNode tln = treeList1.FocusedNode;

            if (tln == null || tln.Level==0　)
                return;
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
            Ps_Forecast_Math v = DataConverter.RowToObject<Ps_Forecast_Math>(row);
            Services.BaseService.Update<Ps_Forecast_Math>(v);
        }
        private void pasteData(TreeListNode tln) {
            string s1 = tln["Title"].ToString();
            //if (tln.Level!=2) return;

            IDataObject obj1 = Clipboard.GetDataObject();
            string text = obj1.GetData("Text").ToString();
            string[] lines = text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            tln.TreeList.BeginInit();
            for (int i = 0; i < lines.Length; i++) {
                string[] items = lines[i].Split(new string[] { "\t" }, StringSplitOptions.None);
                if (items.Length != pasteCols.Count) continue;
                TreeListNode fnode = findByColumnValue(tln, "Title", items[0], true);
                if (fnode == null) {
                    if (tln.Level != 2) continue;
                    Ps_Forecast_Math v = new Ps_Forecast_Math();
                    v.ID = createID();
                    v.ParentID = tln["ID"].ToString();
                    v.Forecast =(int)tln["Forecast"];
                    v.ForecastID = tln["ForecastID"].ToString();
                    v.Col4 = ProjectUID;
                    v.Title = items[0];

                    DataRow row= DataConverter.ObjectToRow(v, dataTable.NewRow());
                    
                    for (int j = 0; j < pasteCols.Count; j++) {
                        try {
                            if (items[j] == "") items[j] = "0";
                            row[pasteCols[j]] = items[j];
                        } catch { }
                    }
                    v = DataConverter.RowToObject<Ps_Forecast_Math>(row);
                    Services.BaseService.Create<Ps_Forecast_Math>(v);
                    dataTable.Rows.Add(row);

                } else {
                    for (int j = 0; j < pasteCols.Count; j++) {
                        try {
                            if (items[j] == "") items[j] = "0";
                            fnode.SetValue(pasteCols[j], items[j]);
                        } catch { }
                    }
                    updateNode(fnode);
                    if(tln.Level!=2)
                        updateSummaryNode(fnode.ParentNode);
                }
            }
            if(tln.Level==2)
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

        private void bt434_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            tj433();
        }
        void GetHistory() {
            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = type;
            psp_Type.Col4 = ProjectUID;
            IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);
            DataTable dt1 = DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));

            psp_Type.Forecast = type32;
            IList<Ps_History> listTypes2=  Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);
            DataTable dt2 = DataConverter.ToDataTable((IList)listTypes2, typeof(Ps_History));
            DataRow[] rows = dt1.Select("ParentID=''", "sort asc");
            foreach (DataRow row in rows) {
                createMath(row, dt1, dt2,"");
            }
            LoadData();
        }

        private void createMath(DataRow row, DataTable dt1, DataTable dt2,string pid) {
            string id = row["ID"].ToString();
            DataRow frow = findrow(dataTable, "Col1='"+id+"'", "");//电量
            Type mathType =typeof(Ps_Forecast_Math);
            string _pid="";
            Ps_History v = DataConverter.RowToObject<Ps_History>(row);
            if (frow == null) {//create
                
                v.ID = createID();
                v.Col1 = id;
                v.ForecastID = forecastReport.ID;
                v.Forecast = type;
                v.ParentID = pid;
                Services.BaseService.Create("InsertPs_Forecast_MathbyPs_History", v);
                _pid =v.ID;
            } else {//update
                Ps_Forecast_Math py = DataConverter.RowToObject<Ps_Forecast_Math>(frow);
                py.Title = row["Title"].ToString();
                py.Col2 = row["Col2"].ToString();
                py.ParentID = pid;
                mathType.GetProperty("y" + firstyear).SetValue(py, row["y" + firstyear], null);
                Services.BaseService.Update<Ps_Forecast_Math>(py);
                _pid = py.ID;
            }
            DataRow frow2 = findrow(dataTable2, "Col1='" + id+splitstr + "'", "");//负荷
            DataRow row2 = findrow(dt2, "ID='" + id + splitstr + "'", "");
            if (frow2 == null) {
                if (row2 != null) {
                    v = DataConverter.RowToObject<Ps_History>(row2);
                    v.ID = _pid + splitstr;
                    v.Col1 = id + splitstr;
                    v.ForecastID = forecastReport.ID;
                    v.Forecast = type32;
                    v.ParentID = pid == "" ? "" : pid + splitstr;
                    Services.BaseService.Create("InsertPs_Forecast_MathbyPs_History", v);
                }
            } else {
                Ps_Forecast_Math py = DataConverter.RowToObject<Ps_Forecast_Math>(frow2);
                py.Title = row["Title"].ToString();
                py.Col2 = row["Col2"].ToString();
                py.ParentID = pid == "" ? "" : pid + splitstr;
                object value = 0;
                if (row2 != null) value = row2["y" + firstyear];
                mathType.GetProperty("y" + firstyear).SetValue(py, value, null);
                Services.BaseService.Update<Ps_Forecast_Math>(py);
                
            }
            DataRow[] rows = dt1.Select("ParentID='"+id+"'", "sort asc");
            foreach (DataRow r in rows) {
                createMath(r, dt1, dt2,_pid);
            }
        }
        /// <summary>
        /// 更新历史数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt513_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            
        }

        private void btGetHistory_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            GetHistory();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            FormYearSet fys = new FormYearSet();
            fys.TYPE = "负荷分布预测";
            fys.PID = ProjectUID;
            if (fys.ShowDialog() != DialogResult.OK)
                return;
            firstyear = fys.SYEAR;
            endyear = fys.EYEAR;
            LoadData();
        }
    }
}