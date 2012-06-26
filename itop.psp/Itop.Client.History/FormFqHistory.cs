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
using System.IO;
using DevExpress.Utils;
using Dundas.Charting.WinControl;
namespace Itop.Client.History
{
    public partial class FormFqHistory : FormBase
    {

        public static FormFqHistory Historyhome = new FormFqHistory();
        public IList<Ps_History> listTypes = null;
        public string type1 = "4";
        public int type = 4;
        public DataTable dataTable = new DataTable();
        bool bLoadingData = false;
        bool _canEdit = true;
        int firstyear = 2000;
        int endyear =2008;
        bool isdoubleedit = true;
        private double _fqtsl;
        //负荷同是率
        public double fqtsl
        {
            set { _fqtsl=value;}
            get
            {
                 string connstr = " Forecast=6 and ForecastID='6' and Title='分区供电实绩参数设置' and Col4='" + ProjectUID + "'";

                IList<Ps_History> phlist = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryBYconnstr", connstr);
                if (phlist.Count==0)
	            {
                    _fqtsl=1;
	            }
                else
	            {
                    _fqtsl=phlist[0].y1990;
	            }
                return _fqtsl;
            }
        }
        public FormFqHistory()
        {
            InitializeComponent();
        }

        private void HideToolBarButton()
        {

        }

        private void InitForm()
        {
            barButtonItem1.Glyph = Itop.ICON.Resource.新建;
            barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

            barButtonItem3.Glyph = Itop.ICON.Resource.修改;
            barButtonItem3.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

            barButtonItem2.Glyph = Itop.ICON.Resource.删除;
            barButtonItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

            barSubItem1.Glyph = Itop.ICON.Resource.工具;
            barSubItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

            barButtonItem4.Glyph = Itop.ICON.Resource.刷新;
            barButtonItem4.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

            barButtonItem6.Glyph = Itop.ICON.Resource.关闭;
            barButtonItem6.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

            barButtonItem10.Glyph = Itop.ICON.Resource.授权;
            barButtonItem10.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;


            barButtonItem9.Glyph = Itop.ICON.Resource.打回重新编;
            barButtonItem9.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
        }


        private void Form8Forecast_Load(object sender, EventArgs e)
        {
            InitForm();
            Application.DoEvents();
            Ps_YearRange py=new Ps_YearRange();
            py.Col4 = "分区供电实绩";
            py.Col5=ProjectUID;

            IList<Ps_YearRange> li = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);
            if (li.Count > 0)
            {
                firstyear = li[0].StartYear;
                endyear = li[0].FinishYear;
            }
            else
            {
                firstyear = 2000;
                endyear = 2008;
                py.BeginYear = 1990;
                py.FinishYear = endyear;
                py.StartYear = firstyear;
                py.EndYear = 2060;
                py.ID = Guid.NewGuid().ToString();
                Services.BaseService.Create<Ps_YearRange>(py);
            }
            if (!AddRight)
            {
                barButtonItem7.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barSubItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!EditRight)
            {
                barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
               
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barSubItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!DeleteRight)
            {
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barSubItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }


             LoadData();
             Historyhome = this;
             
        }

        private void LoadData()
        {
           
            bLoadingData = true;
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
            }
            AddFixColumn();
            for (int i = firstyear; i <= endyear; i++)
            {
                AddColumn(i);
            }

            ArrayList al = new ArrayList();
            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = type;
            psp_Type.Col4 = ProjectUID;
            IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);

          
            for(int c=0;c<al.Count;c++)
            {
                bool bl = true;
                foreach (Ps_History ph in listTypes)
                {
                    if (al[c].ToString() == ph.Title)
                        bl = false;
                }
                if (bl)
                {
                    Ps_History pf = new Ps_History();
                    pf.ID = Guid.NewGuid().ToString()+"|"+ProjectUID;
                    pf.Forecast = type;
                    pf.ForecastID = type1;
                    pf.Title = al[c].ToString();
                    pf.Col4 = ProjectUID;
                    object obj = Services.BaseService.GetObject("SelectPs_HistoryMaxID", pf);
                    if (obj != null)
                        pf.Sort = ((int)obj) + 1;
                    else
                        pf.Sort = 1;
                    Services.BaseService.Create<Ps_History>(pf);
                    listTypes.Add(pf);
                }
            }
            dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));
                 
            treeList1.BeginInit();
            treeList1.DataSource = dataTable;

            treeList1.Columns["Sort"].SortOrder = SortOrder.Ascending;
            treeList1.EndInit();
            Application.DoEvents();
            treeList1.ExpandAll();
            bLoadingData = false;
        }

        //添加固定列
        private void AddFixColumn()
        {
            TreeListColumn column = new TreeListColumn();
            column.FieldName = "Title";
            column.Caption = "分类名";
            column.VisibleIndex = 0;
            column.Width = 210;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "Sort";
            column.VisibleIndex = -1;
            column.SortOrder = SortOrder.Ascending;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "ForecastID";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});

            column = new TreeListColumn();
            column.FieldName = "Forecast";
            column.VisibleIndex =-1;
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
            column.FieldName = "Col10";
            column.VisibleIndex = -1;

            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
        }
        //添加年份后，新增一列
        private void AddColumn(int year)
        {
            TreeListColumn column = new TreeListColumn();

            column.FieldName ="y" +year ;
            column.Tag = year;
            column.Caption = year + "年";
            column.Name = year.ToString();
            column.Width = 80;
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
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //FileClass.ExportToExcelOld(this.forecastReport.Title, "", this.gridControl1);
            FormResult fr = new FormResult();
            fr.LI = this.treeList1;
            fr.Text = this.Text;
            fr.ShowDialog();
        }

        private void Save()
        {
            
            //保存

            foreach (DataRow dataRow in dataTable.Rows)
            {

                try
                {
                    Ps_History v = Itop.Common.DataConverter.RowToObject<Ps_History>(dataRow);
                    Services.BaseService.Update("UpdatePs_HistoryByID", v);
                    
                }
                catch { }
            }
        }
    
        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            //表格数据发生变化
            if (e.Column.FieldName.IndexOf("y") >= 0)
            {
                CalculateSum(e.Node, e.Column);

                //loadGrid();
            }
        }
        public void loadGrid()
        {

            treeList1.DataSource = null;

            IList<Base_Data> li1 = Common.Services.BaseService.GetStrongList<Base_Data>();
            ArrayList al = new ArrayList();
            foreach (Base_Data bd in li1)
                al.Add(bd.Title);

            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = type;
            psp_Type.Col4 = ProjectUID;
            //IList<Ps_History> 
            listTypes.Clear();    
                listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);


            for (int c = 0; c < al.Count; c++)
            {
                bool bl = true;
                foreach (Ps_History ph in listTypes)
                {
                    if (al[c].ToString() == ph.Title)
                        bl = false;
                }
                if (bl)
                {
                    Ps_History pf = new Ps_History();
                    pf.ID = Guid.NewGuid().ToString() + "|" + ProjectUID;
                    pf.Forecast = type;
                    pf.ForecastID = type1;
                    pf.Title = al[c].ToString();
                    pf.Col4 = ProjectUID;
                    object obj = Services.BaseService.GetObject("SelectPs_HistoryMaxID", pf);
                    if (obj != null)
                        pf.Sort = ((int)obj) + 1;
                    else
                        pf.Sort = 1;
                    Services.BaseService.Create<Ps_History>(pf);
                    listTypes.Add(pf);
                }
            }
            string sql00 = " Title like '全社会最大负荷利用小时%' and Col4='" + ProjectUID + "' and Forecast=" + type;
            Ps_History p00 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", sql00);
            string sql01 = " Title like '网供最大负荷利用小时%' and Col4='" + ProjectUID + "' and Forecast=" + type;
            Ps_History p01 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", sql01);

            if (p00 == null || p01 == null)
            {
                string sql = " Title like '全社会用电量%' and Col4='" + ProjectUID + "' and Forecast=" + type;
                Ps_History p1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", sql);
                string sql2 = " Title like '全社会最大负荷%' and Col4='" + ProjectUID + "' and Forecast=" + type;
                Ps_History p2 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", sql2);
                string sql3 = " Title like '网供电量%' and Col4='" + ProjectUID + "' and Forecast=" + type;
                Ps_History p3 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", sql3);
                string sql4 = " Title like '网供最大负荷%' and Col4='" + ProjectUID + "' and Forecast=" + type;
                Ps_History p4 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", sql4);
                if (p1 != null && p2 != null && p3 != null && p4 != null)
                {
                    p00 = new Ps_History();
                    p00.ID = Guid.NewGuid().ToString();
                    p00.Title = "全社会最大负荷利用小时";
                    p00.Sort = 90;
                    p00.Forecast = type;
                    p00.ForecastID = type.ToString();
                    p00.ParentID = "";
                    p00.y2005 = p1.y2005 / p2.y2005;
                    p00.y2006 = p1.y2006 / p2.y2006;
                    p00.y2007 = p1.y2007 / p2.y2007;
                    p00.y2008 = p1.y2008 / p2.y2008;
                    p00.y2009 = p1.y2009 / p2.y2009;
                    p00.Col4 = ProjectUID;
                    Services.BaseService.Create<Ps_History>(p00);
                    p01 = new Ps_History();
                    p01.ID = Guid.NewGuid().ToString();
                    p01.Title = "网供最大负荷利用小时";
                    p01.Sort = 91;
                    p01.Forecast = type;
                    p01.ForecastID = type.ToString();
                    p01.ParentID = "";
                    p01.y2005 = p3.y2005 / p4.y2005;
                    p01.y2006 = p3.y2006 / p4.y2006;
                    p01.y2007 = p3.y2007 / p4.y2007;
                    p01.y2008 = p3.y2008 / p4.y2008;
                    p01.y2009 = p3.y2009 / p4.y2009;
                    p01.Col4 = ProjectUID;
                    Services.BaseService.Create<Ps_History>(p01);
                }
                listTypes.Add(p00);
                listTypes.Add(p01);
            }
            else
            {
                string sql = " Title like '全社会用电量%' and Col4='" + ProjectUID + "' and Forecast=" + type;
                Ps_History p1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", sql);
                string sql2 = " Title like '全社会最大负荷%' and Col4='" + ProjectUID + "' and Forecast=" + type;
                Ps_History p2 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", sql2);
                string sql3 = " Title like '网供电量%' and Col4='" + ProjectUID + "' and Forecast=" + type;
                Ps_History p3 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", sql3);
                string sql4 = " Title like '网供最大负荷%' and Col4='" + ProjectUID + "' and Forecast=" + type;
                Ps_History p4 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", sql4);
                if (p1 != null && p2 != null && p3 != null && p4 != null)
                {
                    p00 = new Ps_History();
                    p00.ID = Guid.NewGuid().ToString();
                    p00.Title = "全社会最大负荷利用小时";
                    p00.Sort = 90;
                    p00.Forecast = type;
                    p00.ForecastID = type.ToString();
                    p00.ParentID = "";
                    p00.y2005 = p1.y2005 / p2.y2005;
                    p00.y2006 = p1.y2006 / p2.y2006;
                    p00.y2007 = p1.y2007 / p2.y2007;
                    p00.y2008 = p1.y2008 / p2.y2008;
                    p00.y2009 = p1.y2009 / p2.y2009;
                    p00.Col4 = ProjectUID;
                    Services.BaseService.Update<Ps_History>(p00);
                    p01 = new Ps_History();
                    p01.ID = Guid.NewGuid().ToString();
                    p01.Title = "网供最大负荷利用小时";
                    p01.Sort = 91;
                    p01.Forecast = type;
                    p01.ForecastID = type.ToString();
                    p01.ParentID = "";
                    p01.y2005 = p3.y2005 / p4.y2005;
                    p01.y2006 = p3.y2006 / p4.y2006;
                    p01.y2007 = p3.y2007 / p4.y2007;
                    p01.y2008 = p3.y2008 / p4.y2008;
                    p01.y2009 = p3.y2009 / p4.y2009;
                    p01.Col4 = ProjectUID;
                    Services.BaseService.Update<Ps_History>(p01);
                }
                //listTypes.Add(p00);
                //listTypes.Add(p01);
            }
            /**/


            dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));
            treeList1.DataSource = dataTable;
            Application.DoEvents();
            treeList1.ExpandAll();
        }
        //增加子分类
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode focusedNode = treeList1.FocusedNode;

            if (focusedNode == null)
            {
                return;
            }

            FormFqHistory_add frm = new FormFqHistory_add();
           // FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "增加分类";

            if (frm.ShowDialog() == DialogResult.OK)
            {

                Ps_History pf = new Ps_History();
               
                pf.Forecast = type;
                pf.ForecastID = type1;
                pf.ParentID = focusedNode["ID"].ToString();
                pf.Col4 = ProjectUID;

                for (int i = 0; i < frm.Addlist.Count; i++)
                {
                    pf.ID = Guid.NewGuid().ToString() + "|" + ProjectUID;

                    pf.Title = frm.Addlist[i].ToString();
                    object obj = Services.BaseService.GetObject("SelectPs_HistoryMaxID", pf);
                    if (obj != null)
                        pf.Sort = ((int)obj) + 1;
                    else
                        pf.Sort = 1;

                    try
                    {
                        Services.BaseService.Create<Ps_History>(pf);
                        dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf, dataTable.NewRow()));

                    }
                    catch (Exception ex) { MsgBox.Show("增加分类出错：" + ex.Message); }


                }
            }
            
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
                return;
            if (tln.GetValue("Col10").ToString()=="1")
            {
                if (MessageBox.Show(tln.GetValue("Title").ToString() + "  分类为默认类别，删除将会对数据统计及报表产生重大影响！！\n\n                               请确认","警告",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
                
            }
            if (tln.HasChildren)
            {
                if (MsgBox.ShowYesNo(tln.GetValue("Title") + "该分类有子类，如果删除将会同子类一起删除" + "？") == DialogResult.Yes)
                {
                    DeleteNode(tln);
                }
                else
                {
                    return;
                }

            }
            else
            {
                if (MsgBox.ShowYesNo("是否删除分类 " + tln.GetValue("Title") + "？") == DialogResult.Yes)
                {
                    DeleteNode(tln);
                }
                else
                {
                    return;
                }
            }

        }
        //删除结点
        public void DeleteNode(TreeListNode tln)
        {
            if (tln.HasChildren)
            {
                for (int i = 0; i < tln.Nodes.Count; i++)
                {
                    DeleteNode(tln.Nodes[i]);
                }
                DeleteNode(tln);
            }
            else
            {
                Ps_History pf = new Ps_History();
                pf.ID = tln["ID"].ToString();
                 string nodestr = tln["Title"].ToString();
                try
                {
                    TreeListNode node = tln.TreeList.FindNodeByKeyID(pf.ID);
                    if (node != null)
                        tln.TreeList.DeleteNode(node);
                    RemoveDataTableRow(dataTable, pf.ID);
                    Common.Services.BaseService.Delete<Ps_History>(pf);
                }
                catch (Exception e)
                {

                    MessageBox.Show(e.Message + "删除结点出错！");
                }
           
            }


        }
        public void RemoveDataTableRow(DataTable dt, string ID)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["ID"].ToString()==ID)
                {
                    dt.Rows.RemoveAt(i);
                    break;
                }
            }
        }

        


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //修改
            if (treeList1.FocusedNode == null)
                return;

            if (treeList1.FocusedNode.GetValue("Col10").ToString() == "1")
            {
                MsgBox.Show("该类别为默认类别，不可修改！");
                return;
            }

            string nodestr = treeList1.FocusedNode.GetValue("Title").ToString();
            FormTypeTitle frm = new FormTypeTitle();
            frm.TypeTitle = treeList1.FocusedNode.GetValue("Title").ToString();
            frm.Text = "修改分类";
            string strid=treeList1.FocusedNode["ID"].ToString();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //Ps_History v = new Ps_History();
                //TreeNodeToDataObject<Ps_History>(v, treeList1.FocusedNode);
                Ps_History v = Common.Services.BaseService.GetOneByKey<Ps_History>(strid);
                v.Title = frm.TypeTitle;
                v.ID = strid;
                try
                {
                    Common.Services.BaseService.Update<Ps_History>(v);
                    treeList1.FocusedNode.SetValue("Title", frm.TypeTitle);
                }
                catch { }
            }




        }

        static public void TreeNodeToDataObject<T>(T dataObject, DevExpress.XtraTreeList.Nodes.TreeListNode treeNode)
        {
            Type type = typeof(T);
            foreach (PropertyInfo pi in type.GetProperties())
            {
                if(pi.Name.Substring(0,1)=="y")
                pi.SetValue(dataObject, treeNode.GetValue(pi.Name), null);
            }
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (treeList1.FocusedNode.HasChildren)
            {
                e.Cancel = true;
            }
            if (!EditRight || isdoubleedit)
            {
                e.Cancel = true;
            }
        }

        //重新计算表格数据
        private void CalculateSum(TreeListNode node, TreeListColumn column)
        {
            Ps_History v = Services.BaseService.GetOneByKey<Ps_History>(node["ID"].ToString());
            TreeNodeToDataObject<Ps_History>(v, node);

            Common.Services.BaseService.Update<Ps_History>(v);
            TreeListNode parentNode = node.ParentNode;

            if (parentNode == null)
            {
                return;
            }

            double sum = 0;
            foreach (TreeListNode nd in parentNode.Nodes)
            {
                object value = nd.GetValue(column.FieldName);
                if (value != null && value != DBNull.Value)
                {
                    sum += Convert.ToDouble(value);
                }
            }
            if (sum != 0)
            {
                //如果是负荷值，则乘以同是率
                if (parentNode.ParentNode == null && parentNode.GetValue("Title").ToString().Contains("负荷"))
                {
                    parentNode.SetValue(column.FieldName, sum * fqtsl);
                }
                else
                {
                    parentNode.SetValue(column.FieldName, sum);
                }
                v = Services.BaseService.GetOneByKey<Ps_History>(parentNode["ID"].ToString());
                TreeNodeToDataObject<Ps_History>(v, parentNode);

                Common.Services.BaseService.Update<Ps_History>(v);
            }
            else
                parentNode.SetValue(column.FieldName, 0);
            CalculateSum(parentNode, column);
        }
       
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormYearSet fys = new FormYearSet();
            fys.TYPE = "分区供电实绩";
            fys.PID = ProjectUID;
            if (fys.ShowDialog() != DialogResult.OK)
                return;
            firstyear = fys.SYEAR;
            endyear = fys.EYEAR;
            LoadData();
        }
        //数据拷贝
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormProjectDataCopy fpd = new FormProjectDataCopy();
            fpd.ProjectUID = ProjectUID;

            if (fpd.ShowDialog() != DialogResult.OK)
                return;

            string pid = fpd.ProjectUID;

            Hashtable ht1 = new Hashtable();
            Hashtable ht2 = new Hashtable();
            Hashtable ht3 = new Hashtable();
            IList<Base_Data> li = Common.Services.BaseService.GetStrongList<Base_Data>();
            foreach (Base_Data bd in li)
            {
                if (!ht1.ContainsKey(bd.Title))
                    ht1.Add(bd.Title, "");
                if (!ht2.ContainsKey(bd.Title))
                    ht2.Add(bd.Title, "");
            }


            Ps_History psp_Type1 = new Ps_History();
            psp_Type1.Forecast = type;
            psp_Type1.Col4 = pid;
            IList<Ps_History> li1 = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type1);

            Ps_History psp_Type2 = new Ps_History();
            psp_Type2.Forecast = type;
            psp_Type2.Col4 = ProjectUID;
            Services.BaseService.Update("DeletePs_HistoryBy", psp_Type2);

            foreach (Ps_History ph in li1)
            {
                ph.ParentID = ph.ParentID.Replace(pid, ProjectUID);
                ph.Col4 = ProjectUID;
                ph.ID = ph.ID.Replace(pid, ProjectUID);

                Services.BaseService.Create<Ps_History>(ph);

            }



            IList<Ps_History> li2 = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type2);
            LoadData();
            b();
        }
        private void b()
        {
            foreach (TreeListNode tlnn in treeList1.Nodes)
            {
                aaa(tlnn);
            }

        }
        private void aaa(TreeListNode tln)
        {
           
                if (tln.Nodes.Count == 0)
                    CalculateSum2(tln);
                else
                {
                    foreach (TreeListNode tl in tln.Nodes)
                    {
                        aaa(tl);
                    }
                }
            

         
           
        }
        //当子分类数据改变时，计算其父分类的值
        private void CalculateSum2(TreeListNode node, TreeListColumn column)
        {

            TreeListNode parentNode = node.ParentNode;

            if (parentNode == null)
            {
                return;
            }

            double sum = 0;
            foreach (TreeListNode nd in parentNode.Nodes)
            {
                object value = nd.GetValue(column.FieldName);
                try
                {
                    if (value != null && value != DBNull.Value)
                    {
                        sum += Convert.ToDouble(value);
                    }
                }

                catch (Exception ex)
                {
                    string str = ex.Message;
                }
            }
           
           

            CalculateSum2(parentNode, column);
        }

        //计算指定节点的各列
        private void CalculateSum2(TreeListNode node)
        {
            foreach (TreeListColumn column in treeList1.Columns)
            {
                if (column.FieldName.IndexOf("y") >= 0)
                {
                    //CalculateSum2(node, column);
                    CalculateSum(node, column);
                }
            }
        }
        //添加一级分类
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "增加分类";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_History pf = new Ps_History();
                pf.ID = Guid.NewGuid().ToString() + "|" + ProjectUID;
                pf.Forecast = type;
                pf.ForecastID = type1;
                pf.Title = frm.TypeTitle;
                pf.Col4 = ProjectUID;
                object obj = Services.BaseService.GetObject("SelectPs_HistoryMaxID", pf);
                if (obj != null)
                    pf.Sort = ((int)obj) + 1;
                else
                    pf.Sort = 1;
               
                try
                {
                    Services.BaseService.Create<Ps_History>(pf);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf, dataTable.NewRow()));
              
                    
                }
                catch (Exception ex) { MsgBox.Show("增加分类出错：" + ex.Message); }
            }
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MsgBox.ShowYesNo("该操作将清除所有数据，清除数据以后无法恢复,可能对其他用户的数据产生影响，请谨慎操作，你确定继续吗？") == DialogResult.No)
                return;
            Ps_History psp_Type2 = new Ps_History();
            psp_Type2.Forecast = type;
            psp_Type2.Col4 = ProjectUID;
            Services.BaseService.Update("DeletePs_HistoryBy", psp_Type2);
            LoadData();
            b();
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Insertdata();
           
        }
        private DataTable GetExcel(string filepach)
        {
            
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
            for (int i = 0; i < treeList1.Columns.Count; i++)
            {

                if (treeList1.Columns[i].VisibleIndex < 0)
                {
                    if (treeList1.Columns[i].FieldName == "ParentID")
                        capList.Add("父ID");
                    else
                    {
                        capList.Add(treeList1.Columns[i].FieldName);
                    }
                }
                else
                {
                    if (treeList1.Columns[i].FieldName != "Title")
                    capList.Add(treeList1.Columns[i].Caption);
                    else
                    {
                        capList.Add("项目");
                    }
                }
                
                filedList.Add(treeList1.Columns[i].FieldName);
            }

         
            int c = 0;
          
            IList<string> fie = new List<string>();
            int cn = 0;
            int gong = 65;
            int m = 3;
            string L1 = "";
            string S4 = "";
            string L2 = "";
            string AreaName = "";
            string temStr = "";
            for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
            {
                if (capList.Contains(fpSpread1.Sheets[0].Cells[2, j].Text))
            
                    fie.Add(filedList[capList.IndexOf(fpSpread1.Sheets[0].Cells[2, j].Text)]);
              
            }

            for (int k = 0; k < fie.Count; k++)
            {
                dt.Columns.Add(fie[k]);
            }
            for (int i = m; i <fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data); i++)
            {

                DataRow dr = dt.NewRow();
                
                for (int j = 0,fiej =0;fiej<fie.Count&& j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
                {
             
                  //  dr[fie[j]] = fpSpread1.Sheets[0].Cells[i, j].Text.Trim();
                    if (capList.IndexOf(fpSpread1.Sheets[0].Cells[2, j].Text) < 0)
                    {
                       // fiej = j - 1;
                        continue;

                    }
                   
                   // if (fie.Contains(filedList[capList.IndexOf(fpSpread1.Sheets[0].Cells[2, j].Text)]))
                    //    dr[fie[fie.IndexOf(filedList[capList.IndexOf(fpSpread1.Sheets[0].Cells[2, j].Text)])]] = fpSpread1.Sheets[0].Cells[i, j].Text.Trim();
                    dr[fie[fiej]] = fpSpread1.Sheets[0].Cells[i, j].Text.Trim();
                    fiej ++;
                 
                }
                // GetL2(dr["L4"].ToString(), out L2);
                // dr["AreaName"] = AreaName; dr["L1"] = L1; dr["S4"] = S4; dr["L2"] = L2; dr["S3"] = ""; dr["S5"] = i.ToString();
                // if (str != "")
                dt.Rows.Add(dr);
            }
            return dt;
        }
        //导入数据
        private void Insertdata()
        {

            //LayoutList ll1 = this.ctrlLayoutList1.FocusedObject;
            //if (ll1 == null)
            //    return;

            string columnname = "";
           
           
            
                DataTable dts = new DataTable();
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Excel文件(*.xls)|*.xls";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    dts = GetExcel(op.FileName);
                    IList<Ps_History> lii = new List<Ps_History>();
                    DateTime s8 = DateTime.Now;
                    int x = 0;
                    WaitDialogForm wait = new WaitDialogForm("", "正在导入数据, 请稍候...");
                    try
            {
                    for (int i = 0; i < dts.Rows.Count; i++)
                    {

                       
                              this.Cursor = Cursors.WaitCursor;
                        Ps_History l1 = new Ps_History();
                        foreach (DataColumn dc in dts.Columns)
                        {
                            columnname = dc.ColumnName;
                            if (dts.Rows[i][dc.ColumnName].ToString() == "")
                                continue;

                            switch (l1.GetType().GetProperty(dc.ColumnName).PropertyType.Name)
                            {
                                case "Double":
                                    if (dts.Rows[i][dc.ColumnName] == null || dts.Rows[i][dc.ColumnName] == DBNull.Value || dts.Rows[i][dc.ColumnName].ToString() == "")
                                    {
                                        l1.GetType().GetProperty(dc.ColumnName).SetValue(l1,0, null);
                                        break;
                                    }
                                    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, Convert.ToDouble(dts.Rows[i][dc.ColumnName]), null);
                                    break;
                                case "Int32":
                                    if (dts.Rows[i][dc.ColumnName] == null || dts.Rows[i][dc.ColumnName] == DBNull.Value || dts.Rows[i][dc.ColumnName].ToString() == "")
                                    {
                                        l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, 0, null);
                                        break;
                                    }
                                    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, Convert.ToInt32(dts.Rows[i][dc.ColumnName]), null);
                                    break;

                                default:
                                    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, dts.Rows[i][dc.ColumnName], null);
                                    break;
                            }
                        }
                       
                        l1.Forecast = type;
                        l1.Col4 = ProjectUID;
                        l1.ForecastID = type1;
                        lii.Add(l1);
                    }
                    int parenti = -4;
                    //foreach (Ps_History l1 in lii)
                    Ps_History psl1;
                    for (int i = 0; i < lii.Count; i++)
                    {



                        psl1 = lii[i];
                        psl1.Sort = i;
                        string con = "Col4='" + ProjectUID + "' and Title='" + psl1.Title + "' and Forecast='" + type + "' and ParentID='"+ psl1.ParentID +"'";
                        object obj = Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
                        if (obj != null)
                        {
                            psl1.ID = ((Ps_History)obj).ID;
                           // psl1.Sort = ((Ps_History)obj).Sort;
                           
                            if (psl1.ParentID.Contains("-"))
                            psl1.ParentID = ((Ps_History)obj).ParentID;
                            Services.BaseService.Update<Ps_History>(psl1);
                        }
                        else
                        {
                            psl1.ID = Guid.NewGuid().ToString() + "|" + ProjectUID;

                            //object obj2 = Services.BaseService.GetObject("SelectPs_HistoryMaxID", psl1);
                            //if (obj2 != null)
                            //    psl1.Sort = ((int)obj2) + 1;
                            //else
                            //    psl1.Sort = 1;
                           // psl1.Sort = i;
                            Services.BaseService.Create<Ps_History>(psl1);
                        }
                        for (int j = i + 1; j < lii.Count;j++ )
                        {
                            if (lii[j].ParentID == parenti.ToString())
                            {
                                lii[j].ParentID = psl1.ID;
                            }
                        }

                        parenti--;
                    }
                    LoadData();
                    wait.Close();
                    wait = new WaitDialogForm("", "正在重新计算数据, 请稍候...");
                    for (int i = 0; i < lii.Count; i++)
                        {
                          TreeListNode nd=  treeList1.FindNodeByKeyID(lii[i].ID);
                          if (nd != null)
                          {
                              foreach(TreeListColumn tr  in treeList1.Columns)
                                  if (tr.FieldName.IndexOf("y") >= 0)
                              {
                                  CalculateSum(nd,tr);
                                
                              }
                          
                          }
                        
                        }
             
                       

                   // b();
                    this.Cursor = Cursors.Default;
                    wait.Close();
                    MsgBox.Show("导入成功！");
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    wait.Close();
                    MsgBox.Show(columnname + ex.Message); MsgBox.Show("导入格式不正确！");
                }
                }

           
        }

        private void treeList1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            isdoubleedit = false;
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            isdoubleedit = true;
        }

        private void treeList1_FocusedColumnChanged(object sender, DevExpress.XtraTreeList.FocusedColumnChangedEventArgs e)
        {
            isdoubleedit = true;
        }
        //默认类别管理
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
             
       
            FormHistoryType frm = new FormHistoryType("2");
            List<string> templist = new List<string>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                templist.Add(dataTable.Rows[i]["ID"].ToString());
            }
            frm.ValueList = templist;
            frm.ShowDialog();
        
        }
        //导出数据
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

          
          

        }
        //直接导出数据
        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormResult fr = new FormResult();
            fr.LI = this.treeList1;
            fr.Text = this.Text;
            fr.ShowDialog();
        }
        //选择年份和增长率
        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<int> li = new List<int>();
            for (int i = firstyear; i <= endyear; i++)
            {
                li.Add(i);
            }

            FormChooseYears1 cy = new FormChooseYears1();
            cy.ListYearsForChoose = li;
            if (cy.ShowDialog() != DialogResult.OK)
                return;
            Hashtable ht = new Hashtable();
            Hashtable ht1 = new Hashtable();
            Hashtable ht2 = new Hashtable();
            foreach (DataRow a in cy.DT.Rows)
            {
                if (a["B"].ToString() == "True")
                    ht.Add(Guid.NewGuid().ToString(), Convert.ToInt32(a["A"].ToString().Replace("年", "")));

                if (a["C"].ToString() == "True")
                    ht1.Add(Guid.NewGuid().ToString(), Convert.ToInt32(a["A"].ToString().Replace("年", "")));

                if (a["D"].ToString() == "True")
                    ht2.Add(Guid.NewGuid().ToString(), Convert.ToInt32(a["A"].ToString().Replace("年", "")));
            }
            FormFqHisView fgv = new FormFqHisView();
            fgv.datatable = dataTable;
            fgv.LI = treeList1;
            fgv.ProjectUID = ProjectUID;
            fgv.HT = ht;
            fgv.HT1 = ht1;
            fgv.HT2 = ht2;
            fgv.Text = "分区数据导出";
            fgv.ShowDialog();

        }


        private void treeList1_CellValueChanged_1(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            
            //表格数据发生变化
            if (e.Column.FieldName.IndexOf("y") >= 0)
            {
                CalculateSum(e.Node, e.Column);
            }
        
        }
        //参数设置
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string connstr = " Forecast=6 and ForecastID='6' and Title='分区供电实绩参数设置' and Col4='" + ProjectUID + "'";
            IList<Ps_History> phlist = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryBYconnstr", connstr);
            if (phlist.Count == 0)
            {
                Ps_History pf = new Ps_History();
                pf.ID = Guid.NewGuid().ToString() + "|" + ProjectUID;
                pf.Forecast = 6;
                pf.ForecastID = "6";
                pf.Title = "分区供电实绩参数设置";
                pf.Col4 = ProjectUID;
                pf.y1990 = 1;
                try
                {
                    Services.BaseService.Create<Ps_History>(pf);
                    phlist.Add(pf);
                }
                catch (Exception)
                { }
            }
            FormFqHistory_Param ffp = new FormFqHistory_Param();
            ffp.Title = "分区供电实绩负荷参数设置";
            ffp.tsl = (decimal)phlist[0].y1990;
            double oldvalue = (double)phlist[0].y1990;
            if (ffp.ShowDialog() == DialogResult.OK)
            {
                phlist[0].y1990 = (double)ffp.tsl;
                try
                {
                    Common.Services.BaseService.Update<Ps_History>(phlist[0]);
                    double newvlue = (double)ffp.tsl;
                    if (oldvalue!=newvlue)
                    {
                        WaitDialogForm wait = new WaitDialogForm("", "正在重新计算负荷数据, 请稍候...");
                        ChangeFu(oldvalue, newvlue);
                        wait.Close();
                    }
                    MessageBox.Show("负荷同时率设置已完成！ ");
                }
                catch (Exception ee)
                {

                    MessageBox.Show("修改负荷参数据出错: " + ee.Message);
                }

            }
        }
        //根据同时率重新计算负荷值
        private void ChangeFu(double oldvlue,double newvalue)
        {
            foreach (TreeListNode node in treeList1.Nodes)
            {
                if (node!=null&&node.HasChildren)
                {
                    if (node.GetValue("Title").ToString().Contains("负荷"))
                    {
                        foreach (TreeListColumn tr in treeList1.Columns)
                            if (tr.FieldName.IndexOf("y") >= 0)
                            {
                                node.SetValue(tr.FieldName, ((double)node.GetValue(tr.FieldName) / oldvlue) * newvalue);
                            }
                        Ps_History v = Services.BaseService.GetOneByKey<Ps_History>(node["ID"].ToString());
                        v = Services.BaseService.GetOneByKey<Ps_History>(node["ID"].ToString());
                        TreeNodeToDataObject<Ps_History>(v, node);
                        Common.Services.BaseService.Update<Ps_History>(v);
                    }
                }
            }
        }
        //上移一位
        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }

            TreeListNode node = treeList1.FocusedNode;
            int i = 0, sortj = 0, sortj2 = 0;

            if (treeList1.FocusedNode.PrevNode != null)
            {
                string ID1 = treeList1.FocusedNode.PrevNode["ID"].ToString();
                string ID2 = treeList1.FocusedNode["ID"].ToString();
                if (ID1 != ID2)
                {
                    sortj = Convert.ToInt32(treeList1.FocusedNode.PrevNode["Sort"]);
                    sortj2 = Convert.ToInt32(treeList1.FocusedNode["Sort"]);
                    if (sortj2 == sortj)
                    {
                        sortj2 = sortj2 - 1;

                    }
                    else
                    {
                        i = sortj;
                        sortj = sortj2;
                        sortj2 = i;
                    }
                    Ps_History ph = Common.Services.BaseService.GetOneByKey<Ps_History>(ID1);
                    Ps_History ph2 = Common.Services.BaseService.GetOneByKey<Ps_History>(ID2);
                    if (ph != null)
                    {
                        ph.Sort = sortj;
                        Common.Services.BaseService.Update<Ps_History>(ph);
                    }
                    if (ph2 != null)
                    {
                        ph2.Sort = sortj2;
                        Common.Services.BaseService.Update<Ps_History>(ph2);
                    }
                    treeList1.FocusedNode.PrevNode["Sort"] = sortj;
                    treeList1.FocusedNode["Sort"] = sortj2;
                    treeList1.BeginSort();
                    treeList1.EndSort();

                }

            }
        }
        //下移一位
        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }

            TreeListNode node = treeList1.FocusedNode;
            int i = 0, sortj = 0, sortj2 = 0;

            if (treeList1.FocusedNode.NextNode != null)
            {
                string ID1 = treeList1.FocusedNode.NextNode["ID"].ToString();
                string ID2 = treeList1.FocusedNode["ID"].ToString();
                if (ID1 != ID2)
                {
                    sortj = Convert.ToInt32(treeList1.FocusedNode.NextNode["Sort"]);
                    sortj2 = Convert.ToInt32(treeList1.FocusedNode["Sort"]);
                    if (sortj2 == sortj)
                    {
                        sortj2 = sortj2 + 1;
                        i = sortj;
                        sortj = sortj2;
                        sortj2 = i;
                    }
                    else
                    {
                        i = sortj;
                        sortj = sortj2;
                        sortj2 = i;
                    }

                    Ps_History ph = Common.Services.BaseService.GetOneByKey<Ps_History>(ID1);
                    Ps_History ph2 = Common.Services.BaseService.GetOneByKey<Ps_History>(ID2);
                    if (ph != null)
                    {
                        ph.Sort = sortj;
                        Common.Services.BaseService.Update<Ps_History>(ph);
                    }
                    if (ph2 != null)
                    {
                        ph2.Sort = sortj2;
                        Common.Services.BaseService.Update<Ps_History>(ph2);
                    }


                 
                    treeList1.FocusedNode.NextNode["Sort"] = sortj;
                    treeList1.FocusedNode["Sort"] = sortj2;
                    treeList1.BeginSort();
                    treeList1.EndSort();
                }

            }
        }

      
        
       




    }
}   
