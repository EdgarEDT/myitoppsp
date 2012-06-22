using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.HistoryValue;
using Itop.Common;
using Itop.Client.Base;
using System.Collections;
using System.Reflection;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Itop.Client.Chen;
using Itop.Domain.Table;
using Itop.Client.Table;
using Itop.Client.Forecast;
using System.Drawing.Drawing2D;
using Itop.Domain.Graphics;
using DevExpress.Utils;
namespace Itop.Client.Table
{
    public partial class Frm110ResultSH : FormBase
    {
        DataTable dataTable;

        private TreeListNode lastEditNode = null;
        private TreeListColumn lastEditColumn = null;
        private string lastEditValue = string.Empty;
        private DataCommon dc = new DataCommon();
        TreeListNode treenode;
        private int typeFlag2 = 1;
        private OperTable oper = new OperTable();
        public Ps_YearRange yAnge = new Ps_YearRange();
        private bool _isSelect = false;
        bool isdel = false;
        public bool IsSelect
        {
            get { return _isSelect; }
            set { _isSelect = value; }
        }
        private string _title = "";

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private string _unit = "";

        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }
        DevExpress.XtraGrid.GridControl _gcontrol = null;

        public DevExpress.XtraGrid.GridControl Gcontrol
        {
            get { return _gcontrol; }
            set { _gcontrol = value; }
        }

        public Frm110ResultSH()
        {
            InitializeComponent();
        }

        private void HideToolBarButton()
        {
            if (!base.AddRight)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!base.EditRight)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem14.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem12.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }
            if (!base.DeleteRight)
            {
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem11.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        public string GetProjectID
        {
            get { return ProjectUID; }
        }

        public int[] GetYears()
        {
            Ps_YearRange yr = yAnge;
            int[] year = new int[4] { yr.BeginYear, yr.StartYear, yr.FinishYear, yr.EndYear };
            return year;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HideToolBarButton();
            yAnge = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.rst110 + "'");
            Show();
            initpasteCols();
            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            LoadData();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
        }
        private IList listTypes;
        private void LoadData()
        {
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
            }


            string con = "ProjectID='" + GetProjectID + "'";
            listTypes = Common.Services.BaseService.GetList("SelectPs_Table_110ResultByConn", con);

            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Table_110Result));


            treeList1.DataSource = dataTable;

            treeList1.Columns["Title"].Caption = "项目名称";
            treeList1.Columns["Title"].Width = 250;
            treeList1.Columns["Title"].MinWidth = 250;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Title"].VisibleIndex = 0;
            CalcYearColumn();
            viewForK("yf", barCheckItem1.Checked); viewForK("yk", barCheckItem2.Checked);
            for (int i = 1; i <= 4; i++)
            {
                treeList1.Columns["Col" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["Col" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
            }
            treeList1.Columns["Sort"].VisibleIndex = -1;
            treeList1.Columns["ProjectID"].VisibleIndex = -1;
            treeList1.Columns["BuildIng"].VisibleIndex = -1;
            treeList1.Columns["BuildEd"].VisibleIndex = -1;
            treeList1.Columns["BuildYear"].VisibleIndex = -1;
            treeList1.Columns["Sort"].SortOrder = SortOrder.Ascending;
            Application.DoEvents();

            treeList1.CollapseAll();
        }

        public void SetValueNull()
        {
            int[] year = GetYears();
            foreach (TreeListNode node in treeList1.Nodes)
            {
                if (node.GetValue("ParentID").ToString() == "0")
                {
                    for (int i = year[1]; i <= year[2]; i++)
                    {
                        node.SetValue("yf" + i.ToString(), 1);
                        node.SetValue("yk" + i.ToString(), 1);
                    }
                }
            }
        }

        public void LoadData1()
        {
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                //treeList1.Columns.Clear();
            }
            string con = "ProjectID='" + GetProjectID + "'";
            listTypes = Common.Services.BaseService.GetList("SelectPs_Table_110ResultByConn", con);

            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Table_110Result));


            treeList1.DataSource = dataTable;

        }
        string totoalParent;
        public void AddTotalRow(ref IList list)
        {
            //合计
            string conn = "ParentID='0' and ProjectID='" + GetProjectID + "'";

            int[] year = GetYears();
            Ps_Table_110Result parent = new Ps_Table_110Result();
            parent.ID += "|" + GetProjectID;
            parent.ParentID = "0"; parent.Title = "110千伏合计"; parent.Sort = 1000;// OperTable.GetMaxSort() + 1;
            list.Add(parent);
            totoalParent = parent.ID;
            string[] lei = new string[7] { "一、110千伏供电负荷合计", "二、110千伏直供负荷", "三、110千伏变电站低压侧供电负荷", "四、110千伏及以下地方电源出力", "五、外网110千伏及以下送入电力", "六、外网110千伏及以下送出电力", "七、110千伏供电负荷" };
            for (int i = 0; i < lei.Length; i++)
            {
                conn = "Col1='" + Convert.ToString(i + 1) + "' and ProjectID='" + GetProjectID + "'";
                string conn1 = "ProjectID='" + GetProjectID + "' and Col2='" + Convert.ToString(i + 1) + "'";
                if (i == 6)
                {
                    conn = "Col1='no' and ProjectID='" + GetProjectID + "'";
                    conn1 = "ProjectID='" + GetProjectID + "' and Col2='no'";
                }
                Ps_Table_110Result table1 = new Ps_Table_110Result();
                table1.ID += "|" + GetProjectID;
                table1.Title = lei[i];
                table1.ParentID = parent.ID;
                table1.ProjectID = GetProjectID;
                table1.Col1 = Convert.ToString(i + 1);
                table1.BuildEd = "total";
                if (i == 6)
                    table1.Col1 = "no";
                IList tList = Common.Services.BaseService.GetList("SelectPs_Table_110ResultByConn", conn);
                for (int j = year[1]; j <= year[2]; j++)
                {
                    double first = 0.0, sec = 0.0;
                    for (int k = 0; k < tList.Count; k++)
                    {
                        first += double.Parse(((Ps_Table_110Result)tList[k]).GetType().GetProperty("yf" + j).GetValue((Ps_Table_110Result)tList[k], null).ToString());
                        sec += double.Parse(((Ps_Table_110Result)tList[k]).GetType().GetProperty("yk" + j).GetValue((Ps_Table_110Result)tList[k], null).ToString());
                    }
                    table1.GetType().GetProperty("yf" + j).SetValue(table1, first, null);
                    table1.GetType().GetProperty("yk" + j).SetValue(table1, sec, null);
                }
                table1.Sort = i + 1;
                list.Add(table1);
                //IList cList = Common.Services.BaseService.GetList("SelectPs_Table_110ResultByConn", conn1);
                //for (int j = 0; j < cList.Count; j++)
                //{
                //    Ps_Table_110Result tablex = new Ps_Table_110Result();
                //    tablex = (Ps_Table_110Result)((Ps_Table_110Result)cList[j]).Clone();
                //    tablex.BuildEd = "total";
                //    tablex.ParentID = table1.ID;
                //    list.Add(tablex);
                //}
            }

        }

        public void CalcYearColumn()
        {
            int[] year = GetYears();
            for (int i = year[0]; i < year[1]; i++)
            {
                treeList1.Columns["yf" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["yf" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
                treeList1.Columns["yk" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["yk" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
                treeList1.Columns["yk" + i.ToString()].ColumnEdit = repositoryItemTextEdit1;
                treeList1.Columns["yk" + i.ToString()].Format.FormatString = "#####################0.##";
                treeList1.Columns["yk" + i.ToString()].Format.FormatType = DevExpress.Utils.FormatType.Numeric;
                treeList1.Columns["yf" + i.ToString()].ColumnEdit = repositoryItemTextEdit1;
                treeList1.Columns["yf" + i.ToString()].Format.FormatString = "#####################0.##";
                treeList1.Columns["yf" + i.ToString()].Format.FormatType = DevExpress.Utils.FormatType.Numeric;

            }
            for (int i = year[2] + 1; i <= year[3]; i++)
            {
                treeList1.Columns["yf" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["yf" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
                treeList1.Columns["yk" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["yk" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
                treeList1.Columns["yk" + i.ToString()].ColumnEdit = repositoryItemTextEdit1;
                treeList1.Columns["yk" + i.ToString()].Format.FormatString = "#####################0.##";
                treeList1.Columns["yk" + i.ToString()].Format.FormatType = DevExpress.Utils.FormatType.Numeric;
                treeList1.Columns["yf" + i.ToString()].ColumnEdit = repositoryItemTextEdit1;
                treeList1.Columns["yf" + i.ToString()].Format.FormatString = "#####################0.##";
                treeList1.Columns["yf" + i.ToString()].Format.FormatType = DevExpress.Utils.FormatType.Numeric;

            }
            for (int i = year[1]; i <= year[2]; i++)
            {
                treeList1.Columns["yf" + i.ToString()].Caption = i.ToString() + "年丰";
                treeList1.Columns["yf" + i.ToString()].VisibleIndex = i;
                treeList1.Columns["yf" + i.ToString()].Width = 70;
                // treeList1.Columns["yf" + i.ToString()].OptionsColumn.AllowEdit = false;
                treeList1.Columns["yf" + i.ToString()].OptionsColumn.AllowSort = false;
                treeList1.Columns["yf" + i.ToString()].ColumnEdit = repositoryItemTextEdit1;
                treeList1.Columns["yk" + i.ToString()].Caption = i.ToString() + "年枯";
                treeList1.Columns["yk" + i.ToString()].VisibleIndex = i;
                treeList1.Columns["yk" + i.ToString()].Width = 70;
                //  treeList1.Columns["yk" + i.ToString()].OptionsColumn.AllowEdit = false;
                treeList1.Columns["yk" + i.ToString()].OptionsColumn.AllowSort = false;
                treeList1.Columns["yk" + i.ToString()].ColumnEdit = repositoryItemTextEdit1;
                treeList1.Columns["yk" + i.ToString()].ColumnEdit = repositoryItemTextEdit1;
                treeList1.Columns["yk" + i.ToString()].Format.FormatString = "#####################0.##";
                treeList1.Columns["yk" + i.ToString()].Format.FormatType = DevExpress.Utils.FormatType.Numeric;
                treeList1.Columns["yf" + i.ToString()].ColumnEdit = repositoryItemTextEdit1;
                treeList1.Columns["yf" + i.ToString()].Format.FormatString = "#####################0.##";
                treeList1.Columns["yf" + i.ToString()].Format.FormatType = DevExpress.Utils.FormatType.Numeric;

            }
        }

        //读取数据
        private void LoadValues()
        {
            PSP_Values psp_Values = new PSP_Values();
            psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值

            IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesListByFlag2", psp_Values);

            foreach (PSP_Values value in listValues)
            {
                TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
                if (node != null)
                {
                    node.SetValue(value.Year + "年", value.Value);
                }
            }
        }


        //添加年份后，新增一列
        private void AddColumn(int year)
        {
            int nInsertIndex = GetInsertIndex(year);

            dataTable.Columns.Add(year + "年", typeof(double));

            TreeListColumn column = treeList1.Columns.Insert(nInsertIndex);
            column.FieldName = year + "年";
            column.Tag = year;
            column.Caption = year + "年";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = nInsertIndex - 2;//有两列隐藏列
            column.ColumnEdit = repositoryItemTextEdit1;
            //treeList1.RefreshDataSource();
        }

        private int GetInsertIndex(int year)
        {
            int nFixedColumns = typeof(PSP_Types).GetProperties().Length - 2;//ID和ParentID列不在treeList1.Columns中
            int nColumns = treeList1.Columns.Count;
            int nIndex = nFixedColumns;

            //还没有年份
            if (nColumns == nFixedColumns)
            {
            }
            else//已经有年份
            {
                bool bFind = false;

                //查找此年的位置
                for (int i = nFixedColumns + 1; i < nColumns; i++)
                {
                    //Tag存放年份
                    int tagYear1 = (int)treeList1.Columns[i - 1].Tag;
                    int tagYear2 = (int)treeList1.Columns[i].Tag;
                    if (tagYear1 < year && tagYear2 > year)
                    {
                        nIndex = i;
                        bFind = true;
                        break;
                    }
                }

                //不在最大和最小之间
                if (!bFind)
                {
                    int tagYear1 = (int)treeList1.Columns[nFixedColumns].Tag;
                    int tagYear2 = (int)treeList1.Columns[nColumns - 1].Tag;

                    if (tagYear1 > year)//比第一个年份小
                    {
                        nIndex = nFixedColumns;
                    }
                    else
                    {
                        nIndex = nColumns;
                    }
                }
            }

            return nIndex;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode focusedNode = treeList1.FocusedNode;

            if (focusedNode == null)
            {
                return;
            }

            if (!base.AddRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }
            FindNodes(treeList1.FocusedNode);
            string nodestr = treenode.GetValue("Title").ToString();
            if (treeList1.FocusedNode.GetValue("Col1").ToString() == "no" || focusedNode.GetValue("ParentID").ToString() == "0")
            {
                MsgBox.Show(focusedNode.GetValue("Title").ToString() + "不允许添加子分类！");
                return;
            }

            FrmAddPN frm = new FrmAddPN();
            frm.Text = "增加" + focusedNode.GetValue("Title") + "的子分类";
            frm.SetLabelName = "子分类名称";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_Table_110Result table1 = new Ps_Table_110Result();
                table1.ID += "|" + GetProjectID;
                table1.Title = frm.ParentName;
                table1.ParentID = focusedNode.GetValue("ID").ToString();
                table1.ProjectID = GetProjectID;
                table1.Col1 = "child";
                table1.Col2 = treeList1.FocusedNode.GetValue("Col1").ToString();
                table1.Sort = OperTable.Get110ResultMaxSort() + 1;

                try
                {
                    Common.Services.BaseService.Create("InsertPs_Table_110Result", table1);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(table1, dataTable.NewRow()));
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加子分类出错：" + ex.Message);
                }
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }
            //FindNodes(treeList1.FocusedNode);

            string nodestr = treeList1.FocusedNode.GetValue("Col1").ToString();


            string parentid = treeList1.FocusedNode["ParentID"].ToString();

            if (!base.EditRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }
            if (nodestr == "1" || nodestr == "2" || nodestr == "3" || nodestr == "4" || nodestr == "5" || nodestr == "6" || nodestr == "no")
            {
                MsgBox.Show("固定分类不许修改！");
                return;
            }
            FrmAddPN frm = new FrmAddPN();
            //frm.TypeTitle = treeList1.FocusedNode.GetValue("Title").ToString();
            frm.ParentName = treeList1.FocusedNode.GetValue("Title").ToString();
            frm.Text = "修改分类名";
            frm.SetLabelName = "分类名称";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_Table_110Result table1 = new Ps_Table_110Result();
                table1 = Common.Services.BaseService.GetOneByKey<Ps_Table_110Result>(treeList1.FocusedNode.GetValue("ID"));
                table1.Title = frm.ParentName;

                try
                {
                    Common.Services.BaseService.Update<Ps_Table_110Result>(table1);
                    treeList1.FocusedNode.SetValue("Title", frm.ParentName);
                }
                catch { }
                //catch(Exception ex)
                //{
                //    MsgBox.Show("修改出错：" + ex.Message);
                //}
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }
            if (!base.DeleteRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            string nodestr = treeList1.FocusedNode.GetValue("Col1").ToString();
            if (nodestr == "1" || nodestr == "2" || nodestr == "3" || nodestr == "4" || nodestr == "5" || nodestr == "6" || nodestr == "no")
            {
                MsgBox.Show("固定分类不许删除！");
                return;
            }
            string parentid = treeList1.FocusedNode["ParentID"].ToString();



            if (treeList1.FocusedNode.HasChildren)
            {
                MsgBox.Show("此分类下有子分类，请先删除子分类！");
                return;
            }





            if (MsgBox.ShowYesNo("是否删除分类 " + treeList1.FocusedNode.GetValue("Title") + "？") == DialogResult.Yes)
            {
                Ps_Table_110Result table1 = new Ps_Table_110Result();
                // Class1.TreeNodeToDataObject<PSP_Types>(psp_Type, treeList1.FocusedNode);
                table1.ID = treeList1.FocusedNode.GetValue("ID").ToString();

                try
                {
                    //DeletePSP_ValuesByType里面删除数据和分类
                    Common.Services.BaseService.Delete<Ps_Table_110Result>(table1);//("DeletePs_Table_110Result", table1);

                    TreeListNode brotherNode = null;
                    if (treeList1.FocusedNode.ParentNode.Nodes.Count > 1)
                    {
                        brotherNode = treeList1.FocusedNode.PrevNode;
                        if (brotherNode == null)
                        {
                            brotherNode = treeList1.FocusedNode.NextNode;
                        }
                    }
                    treeList1.DeleteNode(treeList1.FocusedNode);

                    //删除后，如果同级还有其它分类，则重新计算此类的所有年份数据
                    if (brotherNode != null)
                    {
                        foreach (TreeListColumn column in treeList1.Columns)
                        {
                            if (column.Caption.IndexOf("年") > 0)
                            {
                                CalculateSum(brotherNode, column);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MsgBox.Show("删除出错：" + ex.Message);
                    this.Cursor = Cursors.WaitCursor;
                    treeList1.BeginUpdate();
                    LoadData();
                    treeList1.EndUpdate();
                    this.Cursor = Cursors.Default;
                }
            }
        }

        //设定年份
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (!base.AddRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }
            FormYearSet fys = new FormYearSet();
            fys.TYPE = OperTable.rst110;
            fys.PID = ProjectUID;
            if (fys.ShowDialog() != DialogResult.OK)
                return;
            yAnge = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.rst110 + "'");
            LoadData();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedColumn == null)
            {
                return;
            }

            //不是年份列
            if (treeList1.FocusedColumn.FieldName.IndexOf("年") == -1)
            {
                return;
            }

            if (!base.DeleteRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            if (MsgBox.ShowYesNo("是否删除 " + treeList1.FocusedColumn.FieldName + " 及该年的所有数据？") != DialogResult.Yes)
            {
                return;
            }


            PSP_Values psp_Values = new PSP_Values();
            psp_Values.ID = typeFlag2;//借用ID属性存放Flag2
            psp_Values.Year = (int)treeList1.FocusedColumn.Tag;

            try
            {
                //DeletePSP_ValuesByYear删除数据和年份
                int colIndex = treeList1.FocusedColumn.AbsoluteIndex;
                Common.Services.BaseService.Update("DeletePSP_ValuesByYear", psp_Values);
                dataTable.Columns.Remove(treeList1.FocusedColumn.FieldName);
                treeList1.Columns.Remove(treeList1.FocusedColumn);
                if (colIndex >= treeList1.Columns.Count)
                {
                    colIndex--;
                }
                treeList1.FocusedColumn = treeList1.Columns[colIndex];
            }
            catch (Exception ex)
            {
                MsgBox.Show("删除出错：" + ex.Message);
            }
        }
        bool bPast = false;
        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (bPast)
                return;
            if (e.Value != null)
                if ((e.Value.ToString() != lastEditValue
                    || lastEditNode != e.Node
                    || lastEditColumn != e.Column)
                    && e.Column.Caption.IndexOf("年") > 0)
                {


                    if (SaveCellValue((string)treeList1.FocusedColumn.FieldName, (string)treeList1.FocusedNode.GetValue("ID"), Convert.ToDouble(e.Value)))
                    {
                        CalculateSum(e.Node, e.Column);
                        if (e.Value.ToString() == "0" && isdel)
                        {

                            treeList1.FocusedNode.SetValue(treeList1.FocusedColumn.FieldName, null);


                        }
                    }
                    else
                    {
                        treeList1.FocusedNode.SetValue(treeList1.FocusedColumn.FieldName, lastEditValue);
                    }



                }

            FindNodes(treeList1.FocusedNode);

            //string nodestr = treenode.GetValue("Title").ToString();


            isdel = false;
        }


        private bool SaveCellValue(string year, string typeID, double value)
        {
            Ps_Table_110Result psp = new Ps_Table_110Result();
            Ps_Table_110Result old = Common.Services.BaseService.GetOneByKey<Ps_Table_110Result>(typeID);
            psp = (Ps_Table_110Result)old.Clone();
            psp.GetType().GetProperty(year).SetValue(psp, Math.Round(value, 1), null);

            try
            {
                Common.Services.BaseService.Update<Ps_Table_110Result>(psp);
            }
            catch (Exception ex)
            {
                MsgBox.Show("保存数据出错：" + ex.Message);
                return false;
            }
            return true;
        }

        public void CalcTotalChange(string year, string name, double value)
        {
            if (name == "一、110千伏以下负荷")
                name = "一、110千伏供电负荷合计";

        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (treeList1.FocusedNode.HasChildren
                || !base.EditRight)
            {
                e.Cancel = true;
            }
            FindNodes(treeList1.FocusedNode);
            string nodestr = treenode.GetValue("Title").ToString();
            if (nodestr == "单产耗能(万千瓦时/万元)")
            {
                e.Cancel = true;
            }
            if (nodestr == "人均用电量(千瓦时/人)")
            {
                e.Cancel = true;
            }
            if (nodestr == "Tmax")
            {
                e.Cancel = true;
            }
            if (treeList1.FocusedNode.GetValue("Col1").ToString() == "no")
                e.Cancel = true;
            if (treeList1.FocusedNode.GetValue("BuildEd").ToString() == "total")
                e.Cancel = true;
            if (treeList1.FocusedNode.GetValue("Col3").ToString() == "35xia")
                e.Cancel = true;
        }

        private void FindNodes(TreeListNode node)
        {
            if (node.ParentNode == null)
            {
                treenode = node;
                return;

            }

            FindNodes(node.ParentNode);
            return;
        }
        //当子分类数据改变时，计算其父分类的值
        private void CalculateSum(TreeListNode node, TreeListColumn column)
        {
            TreeListNode parentNode = node.ParentNode;

            if (parentNode == null)
            {
                return;
            }
            if (parentNode.GetValue("ParentID").ToString() == "0")
            {
                CalcTotal(node, column);
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
                parentNode.SetValue(column.FieldName, sum);
            else
                parentNode.SetValue(column.FieldName, 0.0);
            if (sum != 0)
            {
                if (!SaveCellValue((string)column.FieldName, (string)parentNode.GetValue("ID"), sum))
                {
                    return;
                }
            }
            else
            {
                //if (parentNode.HasChildren)
                //{
                //    string flagid = " year=" + treeList1.FocusedColumn.FieldName.Replace("Y", "").Replace("年", "") + " and TypeID=" + parentNode["ID"];
                //    //psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值
                //    //psp_Values.TypeID =Convert.ToInt32(treeList1.FocusedNode["ID"]);
                //    IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", flagid);
                //    if (listValues.Count == 1)
                //    {
                //        Common.Services.BaseService.Delete<PSP_Values>(listValues[0]);
                //    }
                //}
                if (!SaveCellValue((string)column.FieldName, (string)parentNode.GetValue("ID"), 0.0))
                {

                }
            }


            CalculateSum(parentNode, column);
        }
        List<string> pasteCols = new List<string>();
        private void initpasteCols()
        {
            pasteCols.Add("Title");
            for (int i = yAnge.StartYear; i <= yAnge.FinishYear; i++)
            {
                pasteCols.Add("yf" + i);
                pasteCols.Add("yk" + i);
            }
        }

        private void pasteData(TreeListNode tln)
        {
            string s1 = tln["Title"].ToString();
            bPast = true;

            IDataObject obj1 = Clipboard.GetDataObject();
            string text = obj1.GetData("Text").ToString();
            string[] lines = text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            double x = 0.0;
            tln.TreeList.BeginInit();
            for (int i = 0; i < lines.Length; i++)
            {
                string[] items = lines[i].Split(new string[] { "\t" }, StringSplitOptions.None);
                if (items.Length != pasteCols.Count) continue;
                TreeListNode fnode = findByColumnValue(tln, "Title", items[0], false);
                if (fnode == null) continue;
                for (int j = 0; j < pasteCols.Count; j++)
                {
                    try
                    {
                        if (j != 0 && !double.TryParse(items[j], out x))
                            continue;
                        if (j > 0)
                            fnode.SetValue(pasteCols[j], double.Parse(items[j]));
                        else
                            fnode.SetValue(pasteCols[j], items[j]);
                        if (j > 0)
                        {
                            SaveCellValue(pasteCols[j], fnode.GetValue("ID").ToString(), double.Parse(items[j]));
                            CalculateSum(fnode, fnode.TreeList.Columns[pasteCols[j]]);
                        }
                    }
                    catch (Exception e) { string ddd = e.Message; }
                }
                //  updateNode(fnode);
            }
            // updateSummaryNode(tln);
            tln.TreeList.EndInit();
            bPast = false;
        }
        private TreeListNode findByColumnValue(TreeListNode pnode, string column, string findstr, bool f1)
        {
            TreeListNode retnode = null;
            foreach (TreeListNode node in pnode.Nodes)
            {
                if (node[column].ToString().Contains(findstr))
                {
                    retnode = node;
                    break;
                }
                if (f1)
                    retnode = findByColumnValue(node, column, findstr, f1);
            }
            return retnode;
        }
        private void treeList1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                TreeListNode tln = treeList1.FocusedNode;
                if (tln == null)
                {
                    return;
                }
                pasteData(tln);
            }
        }
        public void CalcTotal(TreeListNode node, TreeListColumn column)
        {
            Hashtable atable = new Hashtable();

            double dv1 = 0, dv3 = 0, dv4 = 0, dv5 = 0, dv6 = 0, dv7 = 0, dv8 = 0, dv9 = 0, dv10 = 0, drzb = 0;//BuildYear  rzb

            TreeListNode to = null;
            double.TryParse(node.ParentNode["BuildYear"].ToString(), out drzb);
            foreach (TreeListNode nd in node.ParentNode.Nodes)
            {
                if (!atable.ContainsKey(nd["Col3"].ToString()))
                {
                    atable.Add(nd["Col3"].ToString(), nd);
                }
            }

            double.TryParse(((TreeListNode)atable["1"]).GetValue(column.FieldName).ToString(), out dv1);
            double.TryParse(((TreeListNode)atable["3"]).GetValue(column.FieldName).ToString(), out dv3);
            double.TryParse(((TreeListNode)atable["4"]).GetValue(column.FieldName).ToString(), out dv4);
            double.TryParse(((TreeListNode)atable["5"]).GetValue(column.FieldName).ToString(), out dv5);
            double.TryParse(((TreeListNode)atable["8"]).GetValue(column.FieldName).ToString(), out dv8);
            dv6 = dv1 - dv3 - dv4 - dv5;
            dv7 = dv6 * drzb;
            if (dv6 != 0)
            {
                dv9 = dv8 / dv6;
            }
            dv10 = dv8 - dv7;
            ((TreeListNode)atable["6"]).SetValue(column.FieldName, dv6);
            SaveCellValue((string)column.FieldName, (string)((TreeListNode)atable["6"]).GetValue("ID"), dv6);

            ((TreeListNode)atable["7"]).SetValue(column.FieldName, dv7);
            SaveCellValue((string)column.FieldName, (string)((TreeListNode)atable["7"]).GetValue("ID"), dv7);

            ((TreeListNode)atable["9"]).SetValue(column.FieldName, dv9);
            SaveCellValue((string)column.FieldName, (string)((TreeListNode)atable["9"]).GetValue("ID"), dv9);

            ((TreeListNode)atable["10"]).SetValue(column.FieldName, dv10);
            SaveCellValue((string)column.FieldName, (string)((TreeListNode)atable["10"]).GetValue("ID"), dv10);


        }

        private void treeList1_ShownEditor(object sender, EventArgs e)
        {
            lastEditColumn = treeList1.FocusedColumn;
            lastEditNode = treeList1.FocusedNode;
            lastEditValue = treeList1.FocusedNode.GetValue(lastEditColumn.FieldName).ToString();
        }



        //把树控件内容按显示顺序生成到DataTable中
        private DataTable ConvertTreeListToDataTable(DevExpress.XtraTreeList.TreeList xTreeList, bool bRemove)
        {
            DataTable dt = new DataTable();
            List<string> listColID = new List<string>();
            listColID.Add("BuildIng");
            dt.Columns.Add("BuildIng", typeof(string));
            listColID.Add("BuildYear");
            dt.Columns.Add("BuildYear", typeof(string));

            listColID.Add("BuildEd");
            dt.Columns.Add("BuildEd", typeof(string));
            listColID.Add("Sort");
            dt.Columns.Add("Sort", typeof(int));
            listColID.Add("ProjectID");
            dt.Columns.Add("ProjectID", typeof(string));

            listColID.Add("ParentID");
            dt.Columns.Add("ParentID", typeof(string));

            listColID.Add("Title");
            dt.Columns.Add("Title", typeof(string));
            dt.Columns["Title"].Caption = "分类";
            for (int i = yAnge.StartYear; i <= yAnge.FinishYear; i++)
            {
                listColID.Add("yf" + i.ToString());
                dt.Columns.Add("yf" + i.ToString(), typeof(double));
                listColID.Add("yk" + i.ToString());
                dt.Columns.Add("yk" + i.ToString(), typeof(double));
            }
            for (int i = 1; i < 6; i++)
            {
                listColID.Add("Col" + i.ToString());
                dt.Columns.Add("Col" + i.ToString(), typeof(string));
            }
            foreach (TreeListNode node in xTreeList.Nodes)
            {
                AddNodeDataToDataTable(dt, node, listColID, bRemove);
            }

            return dt;
        }

        private void AddNodeDataToDataTable(DataTable dt, TreeListNode node, List<string> listColID, bool bRemove)
        {
            DataRow newRow = dt.NewRow();
            foreach (string colID in listColID)
            {
                //分类名，第二层及以后在前面加空格
                if (colID == "Title" && node.ParentNode != null)
                {
                    newRow[colID] = "　　" + node[colID];
                }
                else
                {
                    newRow[colID] = node[colID];
                }
            }
            if (bRemove)
            {
                if (newRow["Col1"].ToString() != "1" && newRow["BuildEd"].ToString() != "total")
                    dt.Rows.Add(newRow);
            }
            else
                dt.Rows.Add(newRow);

            foreach (TreeListNode nd in node.Nodes)
            {
                AddNodeDataToDataTable(dt, nd, listColID, bRemove);
            }
        }


        public string ConvertYear(string year)
        {
            if (year.IndexOf("年") == -1)
                return year;
            else if (year.IndexOf("丰") != -1)
                return "yf" + year.Substring(0, 4);
            else if (year.IndexOf("枯") != -1)
                return "yk" + year.Substring(0, 4);
            else
                return "";
        }

        //根据选择的统计年份，生成统计结果数据表
        private DataTable ResultDataTable(DataTable sourceDataTable, List<Itop.Client.Chen.ChoosedYears> listChoosedYears)
        {
            DataTable dtReturn = new DataTable();
            dtReturn.Columns.Add("Title", typeof(string));
            foreach (Itop.Client.Chen.ChoosedYears choosedYear in listChoosedYears)
            {
                dtReturn.Columns.Add(choosedYear.Year + "年丰", typeof(double));
                dtReturn.Columns.Add(choosedYear.Year + "年枯", typeof(double));
            }

            int nRowMaxLoad = 0;//地区最高负荷所在行
            int nRowMaxPower = 0;//地区电网供电能力所在行
            int nRowMaxPowerLow = 0;//枯水期地区电网供电能力所在行

            #region 填充数据
            for (int i = 0; i < sourceDataTable.Rows.Count; i++)
            {
                DataRow newRow = dtReturn.NewRow();
                DataRow sourceRow = sourceDataTable.Rows[i];
                foreach (DataColumn column in dtReturn.Columns)
                {
                    newRow[column.ColumnName] = sourceRow[ConvertYear(column.ColumnName)];
                }
                dtReturn.Rows.Add(newRow);


            }
            #endregion

            //#region 计算电力盈亏和枯水期地区电力盈亏
            //foreach (ChoosedYears choosedYear in listChoosedYears)
            //{
            //    object maxLoad = dtReturn.Rows[nRowMaxLoad][choosedYear.Year + "年"];
            //    if (maxLoad != DBNull.Value)
            //    {
            //        object maxPower = dtReturn.Rows[nRowMaxPower][choosedYear.Year + "年"];
            //        if (maxPower != DBNull.Value)
            //        {
            //            dtReturn.Rows[nRowMaxPower + 1][choosedYear.Year + "年"] = (double)maxPower - (double)maxLoad;
            //        }

            //        maxPower = dtReturn.Rows[nRowMaxPowerLow][choosedYear.Year + "年"];
            //        if (maxPower != DBNull.Value)
            //        {
            //            dtReturn.Rows[nRowMaxPowerLow + 1][choosedYear.Year + "年"] = (double)maxPower - (double)maxLoad;
            //        }
            //    }
            //}
            //#endregion

            return dtReturn;
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private InputLanguage oldInput = null;
        private void treeList1_FocusedColumnChanged(object sender, DevExpress.XtraTreeList.FocusedColumnChangedEventArgs e)
        {
            try
            {
                DevExpress.XtraEditors.Repository.RepositoryItemTextEdit edit = e.Column.ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
                if (edit != null && edit.Mask.MaskType == DevExpress.XtraEditors.Mask.MaskType.Numeric)
                {
                    oldInput = InputLanguage.CurrentInputLanguage;
                    InputLanguage.CurrentInputLanguage = null;
                }
                else
                {
                    if (oldInput != null && oldInput != InputLanguage.CurrentInputLanguage)
                    {
                        InputLanguage.CurrentInputLanguage = oldInput;
                    }
                }
            }
            catch { }
        }
        private void FoucsLocation(string id, TreeListNodes tlns)
        {
            foreach (TreeListNode tln in tlns)
            {
                if (tln["ID"].ToString() == id)
                {
                    treeList1.FocusedNode = tln;
                    return;
                }
                FoucsLocation(id, tln.Nodes);
            }

        }
        //添加城区
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!base.AddRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            TreeListNode focusedNode = treeList1.FocusedNode;

            FrmAddPN frm = new FrmAddPN();
            if (frm.ShowDialog() == DialogResult.OK)
            {

                if (treeList1.Nodes.Count > 0)
                {
                    for (int i = 0; i < treeList1.Nodes.Count; i++)
                    {
                        if (treeList1.Nodes[i].GetValue("Title").ToString() == frm.ParentName && treeList1.Nodes[i].GetValue("ParentID").ToString() == "0")
                        {
                            MessageBox.Show(frm.ParentName + " 地区已存在！");
                            return;
                        }
                       
                    }
                    AddArea(frm.ParentName);
                }
                else
                {
                    AddArea(frm.ParentName);
                }

            }
        }

        private void AddArea(string Areaname)
        {
            Ps_YearRange range = yAnge;
            Ps_Table_110Result table_yd = new Ps_Table_110Result();
            table_yd.ID += "|" + GetProjectID;
            table_yd.BuildYear = "1.9";
            table_yd.Title = Areaname;
            table_yd.ParentID = "0";
            table_yd.Sort = OperTable.Get110ResultMaxSort() + 1;
            table_yd.ProjectID = GetProjectID;
            for (int i = range.BeginYear; i <= range.EndYear; i++)
            {
                table_yd.GetType().GetProperty("yf" + i.ToString()).SetValue(table_yd, null, null);
                table_yd.GetType().GetProperty("yk" + i.ToString()).SetValue(table_yd, null, null);
            }
            try
            {
                Common.Services.BaseService.Create("InsertPs_Table_110Result", table_yd);
            }
            catch (Exception ex)
            {
                MsgBox.Show("增加城区出错：" + ex.Message);
            }
            // string[] lei = new string[7] { "一、110千伏以下负荷", "二、110千伏直供负荷","三、110千伏变电站低压侧供电负荷","四、110千伏及以下地方电源出力", "五、外网110千伏及以下送入电力", "六、外网110千伏及以下送出电力", "七、110千伏供电负荷" };
            string[] lei = new string[11] { "1、全社会最高负荷", "2、110kV以下电源", "3、直供负荷", "4、外区供电", "5、区内电源出力", "6、需110kV变电供电负荷", "7、需110kV主变容量", "8、安排110kV变电容量", "9、实际110kV容载比", "10、变电容量盈亏", "11、变电站个数" };


            for (int i = 0; i < lei.Length; i++)
            {
                Ps_Table_110Result table1 = new Ps_Table_110Result();
                table1.ID += "|" + GetProjectID;
                table1.Title = lei[i];
                table1.ParentID = table_yd.ID;
                table1.ProjectID = GetProjectID;
                table1.Col3 = Convert.ToString(i + 1);
                table1.Col1 = "no";
                if (i == 0 || i == 2 || i == 3)
                {
                    table1.Col1 = "";
                }
                //2、110kV以下电源
                if (i == 1 || i == 4 || i == 7 || i == 10)
                {
                    table1.Col1 = "no";
                }

                table1.Sort = i + 1;
                try
                {
                    Common.Services.BaseService.Create("InsertPs_Table_110Result", table1);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加项目出错：" + ex.Message);
                }
            }
            UpdataDY(table_yd.ID);
            UpDataRL(table_yd.ID);
            UpDataBDZ(table_yd.ID);
            UpData(table_yd.ID);

            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            //treeList1.ExpandAll();
            LoadData1();
            FoucsLocation(table_yd.ID, treeList1.Nodes);
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
        }
        public void Update110(string chen, string pid)
        {
            try
            {
                int[] year = GetYears();
                string con1 = "", con2 = "", con3 = "", con4 = "", con5 = "", con7 = "";
                double oldshuiyf = 0.0, old4yf = 0.0, oldshuiyk = 0.0, old4yk = 0.0, oldyf = 0.0, oldyk = 0.0, old1yf = 0.0, old1yk = 0.0, newyf = 0.0, newyk = 0.0, new1yf = 0.0, new1yk = 0.0,
                    oldheyf = 0.0, oldheyk = 0.0, oldhuoyf = 0.0, oldhuoyk = 0.0, newhuoyf = 0.0, newhuoyk = 0.0;
                //110kv 小水电
                con1 = "ProjectID='" + GetProjectID + "' and Col3='shui' and Col4='" + chen + "'";
                IList listup = Common.Services.BaseService.GetList("SelectPs_Table_110ResultByConn", con1);
                //110kv 小火电
                con4 = "ProjectID='" + GetProjectID + "' and Col3='huo' and Col4='" + chen + "'";
                IList listdown = Common.Services.BaseService.GetList("SelectPs_Table_110ResultByConn", con4);
                if (listup.Count > 0)
                {
                    //第四项
                    con1 = "ProjectID='" + GetProjectID + "' and Col1='4' and ParentID='" + pid + "'";
                    IList list4 = Common.Services.BaseService.GetList("SelectPs_Table_110ResultByConn", con1);
                    Ps_Table_110Result res4 = (Ps_Table_110Result)((Ps_Table_110Result)list4[0]).Clone();
                    // 第四项之水电
                    con2 = "ProjectID='" + GetProjectID + "' and Col3='shui' and ParentID='" + res4.ID + "'";
                    IList listshui = Common.Services.BaseService.GetList("SelectPs_Table_110ResultByConn", con2);
                    Ps_Table_110Result reshui = (Ps_Table_110Result)((Ps_Table_110Result)listshui[0]).Clone();
                    //35kv以下水电
                    con3 = "ProjectID='" + GetProjectID + "' and Col3='35xia' and ParentID='" + reshui.ID + "'";
                    IList list35x = Common.Services.BaseService.GetList("SelectPs_Table_110ResultByConn", con3);
                    // 第四项之火电
                    con4 = "ProjectID='" + GetProjectID + "' and Col3='huo' and ParentID='" + res4.ID + "'";
                    IList listhuo = Common.Services.BaseService.GetList("SelectPs_Table_110ResultByConn", con4);
                    Ps_Table_110Result reshuo = (Ps_Table_110Result)((Ps_Table_110Result)listhuo[0]).Clone();
                    //35kv以下火电
                    con5 = "ProjectID='" + GetProjectID + "' and Col3='35xia' and ParentID='" + reshuo.ID + "'";
                    IList list35xhuo = Common.Services.BaseService.GetList("SelectPs_Table_110ResultByConn", con5);
                    //区域项
                    con7 = "ProjectID='" + GetProjectID + "' and Col1='no' and ParentID='" + pid + "'";
                    IList list7 = Common.Services.BaseService.GetList("SelectPs_Table_110ResultByConn", con7);
                    //区域项
                    Ps_Table_110Result reshe = (Ps_Table_110Result)((Ps_Table_110Result)list7[0]).Clone();
                    //35以下水
                    Ps_Table_110Result res35shui = (Ps_Table_110Result)((Ps_Table_110Result)list35x[0]).Clone();
                    //35以下火
                    Ps_Table_110Result res35huo = (Ps_Table_110Result)((Ps_Table_110Result)list35xhuo[0]).Clone();
                    for (int i = year[0]; i <= year[3]; i++)
                    {
                        //35以下水丰原
                        oldyf = double.Parse(list35x[0].GetType().GetProperty("yf" + i.ToString()).GetValue(list35x[0], null).ToString());
                        //35以下水枯原
                        oldyk = double.Parse(list35x[0].GetType().GetProperty("yk" + i.ToString()).GetValue(list35x[0], null).ToString());
                        //35以下水丰新
                        newyf = double.Parse(listup[0].GetType().GetProperty("yf" + i.ToString()).GetValue(listup[0], null).ToString());
                        //35以下水枯新
                        newyk = double.Parse(listup[0].GetType().GetProperty("yk" + i.ToString()).GetValue(listup[0], null).ToString());
                        //35以下火丰原
                        old1yf = double.Parse(listhuo[0].GetType().GetProperty("yf" + i.ToString()).GetValue(list35xhuo[0], null).ToString());
                        //35以下火枯原
                        old1yk = double.Parse(listhuo[0].GetType().GetProperty("yk" + i.ToString()).GetValue(list35xhuo[0], null).ToString());
                        //35以下火丰新
                        new1yf = double.Parse(listdown[0].GetType().GetProperty("yf" + i.ToString()).GetValue(listdown[0], null).ToString());
                        //35以下火枯新
                        new1yk = double.Parse(listdown[0].GetType().GetProperty("yk" + i.ToString()).GetValue(listdown[0], null).ToString());
                        //水或火电丰值有变化
                        if (oldyf != newyf || old1yf != new1yf)
                        {
                            //35以下水赋新值
                            res35shui.GetType().GetProperty("yf" + i.ToString()).SetValue(res35shui, newyf, null);
                            //35以下火赋新值
                            res35huo.GetType().GetProperty("yf" + i.ToString()).SetValue(res35huo, new1yf, null);
                            //35以下水丰原值
                            oldshuiyf = double.Parse(listshui[0].GetType().GetProperty("yf" + i.ToString()).GetValue(listshui[0], null).ToString());
                            //35以下火丰原值
                            oldhuoyf = double.Parse(listhuo[0].GetType().GetProperty("yf" + i.ToString()).GetValue(listhuo[0], null).ToString());
                            //小水电重新计算
                            reshui.GetType().GetProperty("yf" + i.ToString()).SetValue(reshui, oldshuiyf - oldyf + newyf, null);
                            //小火电重新计算
                            reshuo.GetType().GetProperty("yf" + i.ToString()).SetValue(reshuo, oldhuoyf - old1yf + new1yf, null);
                            //总体第四项原值
                            old4yf = double.Parse(list4[0].GetType().GetProperty("yf" + i.ToString()).GetValue(list4[0], null).ToString());
                            //总体第四项重新赋值
                            res4.GetType().GetProperty("yf" + i.ToString()).SetValue(res4, old4yf - oldyf + newyf - old1yf + new1yf, null);
                            //包括第四项的分区原值
                            oldheyf = double.Parse(list7[0].GetType().GetProperty("yf" + i.ToString()).GetValue(list7[0], null).ToString());
                            //分区重新赋值
                            reshe.GetType().GetProperty("yf" + i.ToString()).SetValue(reshe, oldheyf - oldyf + newyf - old1yf + new1yf, null);
                        }
                        //水或火电枯值有变化
                        if (oldyk != newyk || old1yk != new1yk)
                        {
                            //res35.GetType().GetProperty("yk" + i.ToString()).SetValue(res35, newyk, null);
                            //res35huo.GetType().GetProperty("yk" + i.ToString()).SetValue(res35huo, newhuoyk, null);
                            //reshuo.GetType().GetProperty("yk" + i.ToString()).SetValue(reshuo, newhuoyk, null);
                            //oldshuiyk = double.Parse(listshui[0].GetType().GetProperty("yk" + i.ToString()).GetValue(listshui[0], null).ToString());
                            //reshui.GetType().GetProperty("yk" + i.ToString()).SetValue(reshui, oldshuiyk - oldyk + newyk, null);
                            //old4yk = double.Parse(list4[0].GetType().GetProperty("yk" + i.ToString()).GetValue(list4[0], null).ToString());
                            //res4.GetType().GetProperty("yk" + i.ToString()).SetValue(res4, old4yk - oldyk + newyk - oldhuoyk + newhuoyk, null);
                            //oldheyk = double.Parse(list7[0].GetType().GetProperty("yk" + i.ToString()).GetValue(list7[0], null).ToString());
                            //reshe.GetType().GetProperty("yk" + i.ToString()).SetValue(reshe, oldheyk + oldyk - newyk + oldhuoyk - newhuoyk, null);
                            //35以下水赋新值
                            res35shui.GetType().GetProperty("yk" + i.ToString()).SetValue(res35shui, newyk, null);
                            //35以下火赋新值
                            res35huo.GetType().GetProperty("yk" + i.ToString()).SetValue(res35huo, new1yk, null);
                            //35以下水丰原值
                            oldshuiyk = double.Parse(listshui[0].GetType().GetProperty("yk" + i.ToString()).GetValue(listshui[0], null).ToString());
                            //35以下火丰原值
                            oldhuoyk = double.Parse(listhuo[0].GetType().GetProperty("yk" + i.ToString()).GetValue(listhuo[0], null).ToString());
                            //小水电重新计算
                            reshui.GetType().GetProperty("yk" + i.ToString()).SetValue(reshui, oldshuiyk - oldyk + newyk, null);
                            //小火电重新计算
                            reshuo.GetType().GetProperty("yk" + i.ToString()).SetValue(reshuo, oldhuoyk - old1yk + new1yk, null);
                            //总体第四项原值
                            old4yk = double.Parse(list4[0].GetType().GetProperty("yk" + i.ToString()).GetValue(list4[0], null).ToString());
                            //总体第四项重新赋值
                            res4.GetType().GetProperty("yk" + i.ToString()).SetValue(res4, old4yk - oldyk + newyk - old1yk + new1yk, null);
                            //包括第四项的分区原值
                            oldheyk = double.Parse(list7[0].GetType().GetProperty("yk" + i.ToString()).GetValue(list7[0], null).ToString());
                            //分区重新赋值
                            reshe.GetType().GetProperty("yk" + i.ToString()).SetValue(reshe, oldheyk - oldyk + newyk - old1yk + new1yk, null);

                        }
                    }
                    Common.Services.BaseService.Update<Ps_Table_110Result>(res35shui);
                    Common.Services.BaseService.Update<Ps_Table_110Result>(res35huo);
                    Common.Services.BaseService.Update<Ps_Table_110Result>(reshui);
                    Common.Services.BaseService.Update<Ps_Table_110Result>(reshuo);
                    Common.Services.BaseService.Update<Ps_Table_110Result>(res4);
                    Common.Services.BaseService.Update<Ps_Table_110Result>(reshe);
                }
            }
            catch { }
        }
        /// <summary>
        /// 更新电源
        /// </summary>
        /// <param name="ParentID"></param>
        public void UpdataDY(string ParentID)
        {

            Hashtable rtable = new Hashtable();
            Ps_Table_110Result pt = Common.Services.BaseService.GetOneByKey<Ps_Table_110Result>(ParentID);
            string AreaName = pt.Title;

            string conn = "ProjectID='" + GetProjectID + "' and ParentID='" + ParentID + "'";

            Ps_Table_110Result col2, col5;

            IList<Ps_Table_110Result> listchild = Common.Services.BaseService.GetList<Ps_Table_110Result>("SelectPs_Table_110ResultByConn", conn);
            for (int i = 0; i < listchild.Count; i++)
            {
                rtable.Add(listchild[i].Col3, listchild[i]);
            }
            col2 = (Ps_Table_110Result)rtable["2"];

            //删除其下的
            string conncol2 = "ProjectID='" + GetProjectID + "' and ParentID='" + col2.ID + "'";


            IList<Ps_Table_110Result> col2list = Common.Services.BaseService.GetList<Ps_Table_110Result>("SelectPs_Table_110ResultByConn", conncol2);
            foreach (Ps_Table_110Result ptr in col2list)
            {
                Common.Services.BaseService.Delete<Ps_Table_110Result>(ptr);
            }


            col5 = (Ps_Table_110Result)rtable["5"];

            //更新110kV以下电源  更新 5、区内电源出力
            //
            string conn1 = " AreaID='" + GetProjectID + "' and S9='" + AreaName + "' and cast(S1 as int)=110";
            IList<PSP_PowerSubstation_Info> list = Common.Services.BaseService.GetList<PSP_PowerSubstation_Info>("SelectPSP_PowerSubstation_InfoListByWhere", conn1);

            int startyear = yAnge.BeginYear;
            int endyear = yAnge.EndYear;

            int startyear2 = yAnge.BeginYear;
            int endyear2 = yAnge.EndYear;


            double yf = 0;
            double yk = 0;
            for (int i = yAnge.BeginYear; i <= yAnge.EndYear; i++)
            {
                col2.GetType().GetProperty("yf" + i.ToString()).SetValue(col2, 0, null);
                col2.GetType().GetProperty("yk" + i.ToString()).SetValue(col2, 0, null);

                col5.GetType().GetProperty("yf" + i.ToString()).SetValue(col5, 0, null);
                col5.GetType().GetProperty("yk" + i.ToString()).SetValue(col5, 0, null);
            }

            for (int j = 0; j < list.Count; j++)
            {
                string dyid = list[j].UID;
                Ps_Table_110Result newdy = new Ps_Table_110Result();
                newdy.ParentID = col2.ID;
                newdy.ProjectID = GetProjectID;
                newdy.Col1 = "no";
                newdy.Title = list[j].Title;

                startyear = yAnge.BeginYear;
                endyear = yAnge.EndYear;

                if (list[j].S29 != string.Empty)
                {
                    int.TryParse(list[j].S29, out startyear);
                }
                if (list[j].S30 != string.Empty)
                {
                    int.TryParse(list[j].S30, out endyear);
                }



                string connjz = " RelatetableID ='" + dyid + "' and (S2='新建'  or S2='扩容')";
                IList<Psp_Attachtable> listatt = Common.Services.BaseService.GetList<Psp_Attachtable>("SelectPsp_AttachtableByCont", connjz);
                for (int i = yAnge.BeginYear; i <= yAnge.EndYear; i++)
                {
                    //电源下是否有机组容量，如果没有机组则直接用源的容量，如果有机组，将所有机组按年份计算求和
                    if (listatt.Count > 0)
                    {
                        for (int k = 0; k < listatt.Count; k++)
                        {
                            startyear2 = yAnge.BeginYear;
                            endyear2 = yAnge.EndYear;
                            if (listatt[k].startYear != string.Empty)
                            {
                                int.TryParse(listatt[k].startYear, out startyear2);
                            }
                            if (listatt[k].endYear != string.Empty)
                            {
                                int.TryParse(listatt[k].endYear, out endyear2);
                            }


                            if (startyear2 <= i && i < endyear2)
                            {
                                double dyf = double.Parse(newdy.GetType().GetProperty("yf" + i.ToString()).GetValue(newdy, null).ToString());
                                double dyk = double.Parse(newdy.GetType().GetProperty("yk" + i.ToString()).GetValue(newdy, null).ToString());

                                // 累计机组容量
                                newdy.GetType().GetProperty("yf" + i.ToString()).SetValue(newdy, Math.Round(dyf + listatt[k].ZHI, 2), null);
                                newdy.GetType().GetProperty("yk" + i.ToString()).SetValue(newdy, Math.Round(dyk + listatt[k].ZHI, 2), null);


                                double d5f = double.Parse(col5.GetType().GetProperty("yf" + i.ToString()).GetValue(col5, null).ToString());
                                double d5k = double.Parse(col5.GetType().GetProperty("yk" + i.ToString()).GetValue(col5, null).ToString());
                                // 累计机组容量*丰期机组出力率  或枯期机组出力率
                                col5.GetType().GetProperty("yf" + i.ToString()).SetValue(col5, Math.Round(d5f + listatt[k].ZHI * listatt[k].D1, 2), null);
                                col5.GetType().GetProperty("yk" + i.ToString()).SetValue(col5, Math.Round(d5k + listatt[k].ZHI * listatt[k].D2, 2), null);

                            }


                        }


                    }
                    else
                    {
                        if (startyear <= i && i < endyear)
                        {
                            double.TryParse(list[j].S2, out yf);
                            newdy.GetType().GetProperty("yf" + i.ToString()).SetValue(newdy, Math.Round(yf, 2), null);
                            newdy.GetType().GetProperty("yk" + i.ToString()).SetValue(newdy, Math.Round(yf, 2), null);
                            double d5f = double.Parse(col5.GetType().GetProperty("yf" + i.ToString()).GetValue(col5, null).ToString());
                            double d5k = double.Parse(col5.GetType().GetProperty("yk" + i.ToString()).GetValue(col5, null).ToString());

                            col5.GetType().GetProperty("yf" + i.ToString()).SetValue(col5, Math.Round(d5f + yf, 2), null);
                            col5.GetType().GetProperty("yk" + i.ToString()).SetValue(col5, Math.Round(d5k + yf, 2), null);

                        }
                    }

                    double d2f = double.Parse(col2.GetType().GetProperty("yf" + i.ToString()).GetValue(col2, null).ToString());
                    double d2k = double.Parse(col2.GetType().GetProperty("yk" + i.ToString()).GetValue(col2, null).ToString());

                    double ddyf = double.Parse(newdy.GetType().GetProperty("yf" + i.ToString()).GetValue(newdy, null).ToString());
                    double ddyk = double.Parse(newdy.GetType().GetProperty("yk" + i.ToString()).GetValue(newdy, null).ToString());

                    col2.GetType().GetProperty("yf" + i.ToString()).SetValue(col2, Math.Round(d2f + ddyf, 2), null);
                    col2.GetType().GetProperty("yk" + i.ToString()).SetValue(col2, Math.Round(d2k + ddyk, 2), null);
                }

                Common.Services.BaseService.Create<Ps_Table_110Result>(newdy);
            }
            Common.Services.BaseService.Update<Ps_Table_110Result>(col2);
            Common.Services.BaseService.Update<Ps_Table_110Result>(col5);

        }
        /// <summary>
        /// 更新容量
        /// </summary>
        /// <param name="ParentID"></param>
        public void UpDataRL(string ParentID)
        {
            Hashtable rtable = new Hashtable();
            Ps_Table_110Result pt = Common.Services.BaseService.GetOneByKey<Ps_Table_110Result>(ParentID);
            string AreaName = pt.Title;

            string conn = "ProjectID='" + GetProjectID + "' and ParentID='" + ParentID + "'";

            Ps_Table_110Result col8;

            IList<Ps_Table_110Result> listchild = Common.Services.BaseService.GetList<Ps_Table_110Result>("SelectPs_Table_110ResultByConn", conn);
            for (int i = 0; i < listchild.Count; i++)
            {
                rtable.Add(listchild[i].Col3, listchild[i]);
            }
            col8 = (Ps_Table_110Result)rtable["8"];

            //删除其下的
            string conncol8 = "ProjectID='" + GetProjectID + "' and ParentID='" + col8.ID + "'";


            IList<Ps_Table_110Result> col8list = Common.Services.BaseService.GetList<Ps_Table_110Result>("SelectPs_Table_110ResultByConn", conncol8);
            foreach (Ps_Table_110Result ptr in col8list)
            {
                Common.Services.BaseService.Delete<Ps_Table_110Result>(ptr);
            }



            //更新安排110kV变电容量

            string conn1 = " AreaID='" + GetProjectID + "' and AreaName='" + AreaName + "' and L1=110";
            IList<PSP_Substation_Info> list = Common.Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", conn1);

            int startyear = yAnge.BeginYear;
            int endyear = yAnge.EndYear;

            int startyear2 = yAnge.BeginYear;
            int endyear2 = yAnge.EndYear;


            double yf = 0;
            double yk = 0;
            for (int i = yAnge.BeginYear; i <= yAnge.EndYear; i++)
            {
                col8.GetType().GetProperty("yf" + i.ToString()).SetValue(col8, 0, null);
                col8.GetType().GetProperty("yk" + i.ToString()).SetValue(col8, 0, null);
            }
            for (int j = 0; j < list.Count; j++)
            {
                string dyid = list[j].UID;
                Ps_Table_110Result newdy = new Ps_Table_110Result();
                newdy.ParentID = col8.ID;
                newdy.ProjectID = GetProjectID;
                newdy.Col1 = "no";
                newdy.Title = list[j].Title;

                startyear = yAnge.BeginYear;
                endyear = yAnge.EndYear;

                if (list[j].L28 != string.Empty)
                {
                    int.TryParse(list[j].L28, out startyear);
                }
                if (list[j].L29 != string.Empty)
                {
                    int.TryParse(list[j].L29, out endyear);
                }

                string connjz = " RelatetableID ='" + dyid + "' and (S2='新建'  or S2='扩容')";
                IList<Psp_Attachtable> listatt = Common.Services.BaseService.GetList<Psp_Attachtable>("SelectPsp_AttachtableByCont", connjz);
                for (int i = yAnge.BeginYear; i <= yAnge.EndYear; i++)
                {


                    //变电站下是否有机组容量，如果没有机组则直接用源的容量，如果有机组，将所有机组按年份计算求和
                    if (listatt.Count > 0)
                    {
                        for (int k = 0; k < listatt.Count; k++)
                        {
                            startyear2 = yAnge.BeginYear;
                            endyear2 = yAnge.EndYear;

                            if (listatt[k].startYear != string.Empty)
                            {
                                int.TryParse(listatt[k].startYear, out startyear2);
                            }
                            if (listatt[k].endYear != string.Empty)
                            {
                                int.TryParse(listatt[k].endYear, out endyear2);
                            }


                            if (startyear2 <= i && i < endyear2)
                            {
                                double dyf = double.Parse(newdy.GetType().GetProperty("yf" + i.ToString()).GetValue(newdy, null).ToString());
                                double dyk = double.Parse(newdy.GetType().GetProperty("yk" + i.ToString()).GetValue(newdy, null).ToString());

                                // 累计机组容量
                                newdy.GetType().GetProperty("yf" + i.ToString()).SetValue(newdy, Math.Round(dyf + listatt[k].ZHI, 2), null);
                                newdy.GetType().GetProperty("yk" + i.ToString()).SetValue(newdy, Math.Round(dyk + listatt[k].ZHI, 2), null);
                            }


                        }


                    }
                    else
                    {
                        if (startyear <= i && i < endyear)
                        {

                            newdy.GetType().GetProperty("yf" + i.ToString()).SetValue(newdy, Math.Round(list[j].L2, 2), null);
                            newdy.GetType().GetProperty("yk" + i.ToString()).SetValue(newdy, Math.Round(list[j].L2, 2), null);


                        }
                    }

                    double d8f = double.Parse(col8.GetType().GetProperty("yf" + i.ToString()).GetValue(col8, null).ToString());
                    double d8k = double.Parse(col8.GetType().GetProperty("yk" + i.ToString()).GetValue(col8, null).ToString());

                    double ddyf = double.Parse(newdy.GetType().GetProperty("yf" + i.ToString()).GetValue(newdy, null).ToString());
                    double ddyk = double.Parse(newdy.GetType().GetProperty("yk" + i.ToString()).GetValue(newdy, null).ToString());


                    col8.GetType().GetProperty("yf" + i.ToString()).SetValue(col8, Math.Round(d8f + ddyf, 2), null);
                    col8.GetType().GetProperty("yk" + i.ToString()).SetValue(col8, Math.Round(d8k + ddyk, 2), null);
                }

                Common.Services.BaseService.Create<Ps_Table_110Result>(newdy);
            }
            Common.Services.BaseService.Update<Ps_Table_110Result>(col8);


        }
        /// <summary>
        /// 更新变电站
        /// </summary>
        /// <param name="ParentID"></param>
        public void UpDataBDZ(string ParentID)
        {
            Hashtable rtable = new Hashtable();
            Ps_Table_110Result pt = Common.Services.BaseService.GetOneByKey<Ps_Table_110Result>(ParentID);
            string AreaName = pt.Title;

            string conn = "ProjectID='" + GetProjectID + "' and ParentID='" + ParentID + "'";

            Ps_Table_110Result col11;

            IList<Ps_Table_110Result> listchild = Common.Services.BaseService.GetList<Ps_Table_110Result>("SelectPs_Table_110ResultByConn", conn);
            for (int i = 0; i < listchild.Count; i++)
            {
                rtable.Add(listchild[i].Col3, listchild[i]);
            }
            col11 = (Ps_Table_110Result)rtable["11"];


            //删除其下的
            string conncol11 = "ProjectID='" + GetProjectID + "' and ParentID='" + col11.ID + "'";


            IList<Ps_Table_110Result> col11list = Common.Services.BaseService.GetList<Ps_Table_110Result>("SelectPs_Table_110ResultByConn", conncol11);
            foreach (Ps_Table_110Result ptr in col11list)
            {
                Common.Services.BaseService.Delete<Ps_Table_110Result>(ptr);
            }

            //更新变电站数量  11.变电站个数

            int startyear = yAnge.BeginYear;
            int endyear = yAnge.EndYear;

            int startyear2 = yAnge.BeginYear;
            int endyear2 = yAnge.EndYear;


            Ps_Table_110Result yybdz = new Ps_Table_110Result();
            yybdz.Title = "已有变电站";
            yybdz.ProjectID = GetProjectID;
            yybdz.Col1 = "no";
            yybdz.Sort = 0;
            yybdz.ParentID = col11.ID;


            Ps_Table_110Result xzbdz = new Ps_Table_110Result();
            xzbdz.Title = "新增变电站";
            xzbdz.ProjectID = GetProjectID;
            xzbdz.Col1 = "no";
            xzbdz.Sort = 1;
            xzbdz.ParentID = col11.ID;

            for (int i = yAnge.BeginYear; i <= yAnge.EndYear; i++)
            {
                //已有变电站
                string conn1 = " AreaID='" + GetProjectID + "' and AreaName='" + AreaName + "' and L1=110  and cast(S2 as int)<" + i;
                IList<PSP_Substation_Info> list1 = Common.Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", conn1);
                int yybdznumber = 0;

                for (int j = 0; j < list1.Count; j++)
                {

                    string dyid = list1[j].UID;
                    string connjz = " RelatetableID ='" + dyid + "' and (S2='新建' or S2='扩容')";

                    IList<Psp_Attachtable> listatt = Common.Services.BaseService.GetList<Psp_Attachtable>("SelectPsp_AttachtableByCont", connjz);
                    //如果变站下没有机组，则变电站有效
                    if (listatt.Count > 0)
                    {
                        int jznumber = 0;
                        //找到是否有指定年份还在使用的机组，如果有则变电站有效
                        for (int k = 0; k < listatt.Count; k++)
                        {
                            startyear2 = yAnge.BeginYear;
                            endyear2 = yAnge.EndYear;

                            if (listatt[k].startYear != string.Empty)
                            {
                                int.TryParse(listatt[k].startYear, out startyear2);
                            }
                            if (listatt[k].endYear != string.Empty)
                            {
                                int.TryParse(listatt[k].endYear, out endyear2);
                            }
                            if (startyear2 <= i && i <= endyear2)
                            {
                                jznumber++;
                                break;
                            }
                        }
                        if (jznumber > 0)
                        {
                            yybdznumber++;
                        }

                    }
                    else
                    {
                        yybdznumber++;
                    }

                }
                yybdz.GetType().GetProperty("yf" + i.ToString()).SetValue(yybdz, yybdznumber, null);
                yybdz.GetType().GetProperty("yk" + i.ToString()).SetValue(yybdz, yybdznumber, null);



                //新增变电站
                string conn2 = " AreaID='" + GetProjectID + "' and AreaName='" + AreaName + "' and L1=110  and cast(S2 as int)=" + i;
                IList<PSP_Substation_Info> list2 = Common.Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", conn2);
                int xzbdznumber = 0;

                for (int j = 0; j < list2.Count; j++)
                {

                    string dyid = list2[j].UID;
                    string connjz = " RelatetableID ='" + dyid + "' and (S2='新建' or S2='扩容')";

                    IList<Psp_Attachtable> listatt = Common.Services.BaseService.GetList<Psp_Attachtable>("SelectPsp_AttachtableByCont", connjz);
                    //如果变站下没有机组，则变电站有效
                    if (listatt.Count > 0)
                    {
                        int jznumber = 0;
                        //找到是否有指定年份还在使用的机组，如果有则变电站有效
                        for (int k = 0; k < listatt.Count; k++)
                        {
                            startyear2 = yAnge.BeginYear;
                            endyear2 = yAnge.EndYear;

                            if (listatt[k].startYear != string.Empty)
                            {
                                int.TryParse(listatt[k].startYear, out startyear2);
                            }
                            if (listatt[k].endYear != string.Empty)
                            {
                                int.TryParse(listatt[k].endYear, out endyear2);
                            }
                            if (startyear2 <= i && i <= endyear2)
                            {
                                jznumber++;
                                break;
                            }
                        }
                        if (jznumber > 0)
                        {
                            xzbdznumber++;
                        }

                    }
                    else
                    {
                        xzbdznumber++;
                    }

                }
                xzbdz.GetType().GetProperty("yf" + i.ToString()).SetValue(xzbdz, xzbdznumber, null);
                xzbdz.GetType().GetProperty("yk" + i.ToString()).SetValue(xzbdz, xzbdznumber, null);

                col11.GetType().GetProperty("yf" + i.ToString()).SetValue(col11, yybdznumber + xzbdznumber, null);
                col11.GetType().GetProperty("yk" + i.ToString()).SetValue(col11, yybdznumber + xzbdznumber, null);


            }

            Common.Services.BaseService.Create<Ps_Table_110Result>(yybdz);
            Common.Services.BaseService.Create<Ps_Table_110Result>(xzbdz);
            Common.Services.BaseService.Update<Ps_Table_110Result>(col11);
        }



        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }
            if (!base.DeleteRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }
            if (treeList1.FocusedNode.GetValue("ParentID").ToString() != "0")
            {
                MsgBox.Show("此分类不是总分类！");
                return;
            }

            if (MsgBox.ShowYesNo("总分类及其下属分类都将删除，是否删除总分类 " + treeList1.FocusedNode.GetValue("Title") + "？") == DialogResult.Yes)
            {
                Ps_Table_110Result table1 = new Ps_Table_110Result();
                // Class1.TreeNodeToDataObject<PSP_Types>(psp_Type, treeList1.FocusedNode);
                table1.ID = treeList1.FocusedNode.GetValue("ID").ToString();
                DelAll(table1.ID);
                try
                {
                    //DeletePSP_ValuesByType里面删除数据和分类
                    Common.Services.BaseService.Delete<Ps_Table_110Result>(table1);//("DeletePs_Table_110Result", table1);


                    treeList1.DeleteNode(treeList1.FocusedNode);
                    //treeList1.ExpandAll();
                    //删除后，如果同级还有其它分类，则重新计算此类的所有年份数据

                }
                catch (Exception ex)
                {
                    //MsgBox.Show("删除出错：" + ex.Message);
                    this.Cursor = Cursors.WaitCursor;
                    treeList1.BeginUpdate();
                    LoadData();
                    treeList1.EndUpdate();
                    this.Cursor = Cursors.Default;
                }
            }
        }

        public void DelAll(string suid)
        {
            string conn = "ParentId='" + suid + "'";
            IList<Ps_Table_110Result> list = Common.Services.BaseService.GetList<Ps_Table_110Result>("SelectPs_Table_110ResultByConn", conn);
            if (list.Count > 0)
            {
                foreach (Ps_Table_110Result var in list)
                {
                    string child = var.ID;
                    DelAll(child);
                    Ps_Table_110Result ny = new Ps_Table_110Result();
                    ny.ID = child;
                    Common.Services.BaseService.Delete(ny);
                }
            }
            else
                return;
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCopy frm = new FrmCopy();
            frm.CurID = GetProjectID;
            frm.ClassName = "Ps_Table_110Result";
            frm.SelectString = "SelectPs_Table_110ResultByConn";
            frm.InsertString = "InsertPs_Table_110Result";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("导入成功");
                LoadData1();
            }
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("该操作将清除当前项目的所有数据，清除数据以后无法恢复,可能对其他用户的数据产生影响，请谨慎操作，你确定继续吗？", "删除", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string conn = "ProjectID='" + GetProjectID + "'";
                Common.Services.BaseService.Update("DeletePs_Table_110ResultByConn", conn);
                LoadData1();
            }
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormYearSet fys = new FormYearSet();
            fys.TYPE = OperTable.rst110;
            fys.PID = ProjectUID;
            if (fys.ShowDialog() != DialogResult.OK)
                return;
            yAnge = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.rst110 + "'");
            LoadData();
        }


        //改变负荷时对其它变量进行计算 1.全社会最高负荷
        public void UpdateFuHe(string pid)
        {
            UpData(pid);

        }
        //根据ID值按计算关系重新计算数据
        private void UpData(string pid)
        {
            Hashtable rtable = new Hashtable();
            string conn = "ProjectID='" + GetProjectID + "' and ParentID='" + pid + "'";

            Ps_Table_110Result col1, col2, col3, col4, col5, col6, col7, col8, col9, col10;

            IList<Ps_Table_110Result> list = Common.Services.BaseService.GetList<Ps_Table_110Result>("SelectPs_Table_110ResultByConn", conn);
            for (int i = 0; i < list.Count; i++)
            {
                rtable.Add(list[i].Col3, list[i]);
            }
            int m = 1;
            col1 = (Ps_Table_110Result)rtable[m++.ToString()];
            col2 = (Ps_Table_110Result)rtable[m++.ToString()];
            col3 = (Ps_Table_110Result)rtable[m++.ToString()];
            col4 = (Ps_Table_110Result)rtable[m++.ToString()];
            col5 = (Ps_Table_110Result)rtable[m++.ToString()];
            col6 = (Ps_Table_110Result)rtable[m++.ToString()];
            col7 = (Ps_Table_110Result)rtable[m++.ToString()];
            col8 = (Ps_Table_110Result)rtable[m++.ToString()];
            col9 = (Ps_Table_110Result)rtable[m++.ToString()];
            col10 = (Ps_Table_110Result)rtable[m++.ToString()];

            Ps_Table_110Result city = Common.Services.BaseService.GetOneByKey<Ps_Table_110Result>(pid);
            double rzb = 0;
            if (double.TryParse(city.BuildYear, out rzb))
            {

            }

            for (int i = yAnge.BeginYear; i <= yAnge.EndYear; i++)
            {
                //更新6.需110kV变电供电负荷 6=1-3-4-5

                double d1f = double.Parse(col1.GetType().GetProperty("yf" + i.ToString()).GetValue(col1, null).ToString());
                double d1k = double.Parse(col1.GetType().GetProperty("yk" + i.ToString()).GetValue(col1, null).ToString());

                double d3f = double.Parse(col3.GetType().GetProperty("yf" + i.ToString()).GetValue(col3, null).ToString());
                double d3k = double.Parse(col3.GetType().GetProperty("yk" + i.ToString()).GetValue(col3, null).ToString());

                double d4f = double.Parse(col4.GetType().GetProperty("yf" + i.ToString()).GetValue(col4, null).ToString());
                double d4k = double.Parse(col4.GetType().GetProperty("yk" + i.ToString()).GetValue(col4, null).ToString());

                double d5f = double.Parse(col5.GetType().GetProperty("yf" + i.ToString()).GetValue(col5, null).ToString());
                double d5k = double.Parse(col5.GetType().GetProperty("yk" + i.ToString()).GetValue(col5, null).ToString());

                double d6f = d1f - d3f - d4f - d5f;
                double d6k = d1k - d3k - d4k - d5k;

                col6.GetType().GetProperty("yf" + i.ToString()).SetValue(col6, Math.Round(d6f, 2), null);
                col6.GetType().GetProperty("yk" + i.ToString()).SetValue(col6, Math.Round(d6k, 2), null);

                //更新7.需110kV主变容量7=6*容载比
                double d7f = d6f * rzb;
                double d7k = d6k * rzb;


                col7.GetType().GetProperty("yf" + i.ToString()).SetValue(col7, Math.Round(d7f, 2), null);
                col7.GetType().GetProperty("yk" + i.ToString()).SetValue(col7, Math.Round(d7k, 2), null);


                //9.实际110kV容载比 9=8/6
                double d8f = double.Parse(col8.GetType().GetProperty("yf" + i.ToString()).GetValue(col8, null).ToString());
                double d8k = double.Parse(col8.GetType().GetProperty("yk" + i.ToString()).GetValue(col8, null).ToString());

                double d9k = 0;
                double d9f = 0;

                if (d6f != 0)
                {
                    d9f = d8f / d6f;
                }

                if (d6k != 0)
                {
                    d9k = d8k / d6k;
                }

                col9.GetType().GetProperty("yf" + i.ToString()).SetValue(col9, Math.Round(d9f, 2), null);
                col9.GetType().GetProperty("yk" + i.ToString()).SetValue(col9, Math.Round(d9k, 2), null);
                //10.变电容量盈亏 10=8-7
                double d10f = d8f - d7f;
                double d10k = d8k - d7k;

                col10.GetType().GetProperty("yf" + i.ToString()).SetValue(col10, Math.Round(d10f, 2), null);
                col10.GetType().GetProperty("yk" + i.ToString()).SetValue(col10, Math.Round(d10k, 2), null);

            }

            Common.Services.BaseService.Update<Ps_Table_110Result>(col6);
            Common.Services.BaseService.Update<Ps_Table_110Result>(col7);
            Common.Services.BaseService.Update<Ps_Table_110Result>(col9);
            Common.Services.BaseService.Update<Ps_Table_110Result>(col10);

        }
        //载入负荷数据
        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string conn = "ParentID='0' and ProjectID='" + GetProjectID + "'";
            IList cList = Common.Services.BaseService.GetList("SelectPs_Table_110ResultByConn", conn);
            if (cList.Count > 0)
            {
                FrmImportSellgm frm1 = new FrmImportSellgm();
                frm1.BindList = cList;
                if (frm1.ShowDialog() == DialogResult.OK)
                {
                    FormLoadForecastDataFSH frm = new FormLoadForecastDataFSH();
                    frm.ProjectUID = GetProjectID;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        //设置丰枯比
                        FrmFKbi fkb = new FrmFKbi();
                        if (fkb.ShowDialog() == DialogResult.OK)
                        {
                            DataRow row = frm.ROW;
                            foreach (string str in frm1.OutList)
                            {
                                IList tempList = Common.Services.BaseService.GetList("SelectPs_Table_110ResultByConn", "ParentID='" + str + "' and ProjectID='" + GetProjectID + "' and Col3='1' and Title='1、全社会最高负荷'");
                                if (tempList.Count > 0)
                                {
                                    Ps_Table_110Result psr = tempList[0] as Ps_Table_110Result;
                                    for (int i = yAnge.BeginYear; i <= yAnge.EndYear; i++)
                                    {
                                        psr.GetType().GetProperty("yk" + i.ToString()).SetValue(psr, Math.Round(double.Parse(row["y" + i.ToString()].ToString()) * fkb.GetVal, 1), null);
                                        psr.GetType().GetProperty("yf" + i.ToString()).SetValue(psr, Math.Round(double.Parse(row["y" + i.ToString()].ToString()), 1), null);
                                    }
                                    Common.Services.BaseService.Update<Ps_Table_110Result>(psr);
                                    UpdateFuHe(psr.ParentID);
                                }
                            }
                            LoadData1();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("没有数据！");
            }
        }


        private void barCheckItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viewForK("yf", barCheckItem1.Checked);
        }
        public void viewForK(string str, bool check)
        {
            int[] year = GetYears();
            if (check)
            {
                for (int i = year[1]; i <= year[2]; i++)
                {
                    treeList1.Columns[str + i.ToString()].VisibleIndex = i;
                }
            }
            else
            {
                for (int i = year[1]; i <= year[2]; i++)
                {
                    treeList1.Columns[str + i.ToString()].VisibleIndex = -1;
                }
            }
        }
        private void barCheckItem2_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viewForK("yk", barCheckItem2.Checked);
        }

        private void treeList1_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Column.FieldName == "Title")
            {
                return;
            }
            Brush backBrush, foreBrush;
            if (e.Node.GetValue("ParentID").ToString() == "0" && e.Column.FieldName.IndexOf("y") == 0)
            {
                //Color.Bisque
                backBrush = new LinearGradientBrush(e.Bounds, Color.LightYellow, Color.LightYellow, LinearGradientMode.Horizontal);
                foreBrush = new SolidBrush(Color.Black);
                e.Graphics.FillRectangle(backBrush, e.Bounds);
                //e.Graphics.DrawString(e.CellText, e.Appearance.Font, foreBrush, e.Bounds, e.Appearance.GetStringFormat());
                e.Graphics.DrawString(string.Empty, e.Appearance.Font, foreBrush, e.Bounds, e.Appearance.GetStringFormat());
                e.Handled = true;
            }
        }
        //导出数据
        private void barButtonItem4_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormChooseYears frm = new FormChooseYears();
            for (int i = yAnge.StartYear; i <= yAnge.FinishYear; i++)
            {
                frm.ListYearsForChoose.Add(i);

            }
            frm.NoIncreaseRate = true;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                FrmResultPrint frma = new FrmResultPrint();
                frma.IsSelect = _isSelect;
                frma.Text = "110千伏供电平衡表";
                frma.Dw1 = "单位：万千瓦";

                frma.GridDataTable = ResultDataTable(ConvertTreeListToDataTable(treeList1, false), frm.ListChoosedYears);


                frma.YearList = frm.ListChoosedYears;
                if (frma.ShowDialog() == DialogResult.OK && _isSelect)
                {
                    DialogResult = DialogResult.OK;
                }

            }
        }
        //更新区域
        private void barButtonItem5_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string conn = "ProjectID='" + GetProjectID + "' and ParentID='0'";
            IList<Ps_Table_110Result> list = Common.Services.BaseService.GetList<Ps_Table_110Result>("SelectPs_Table_110ResultByConn", conn);
            Hashtable AreaTable = new Hashtable();
            for (int i = 0; i < list.Count; i++)
            {
                AreaTable.Add(list[i].Title, list[i].Title);
            }
            string connarea = "ProjectID='" + Itop.Client.MIS.ProgUID + "'order by Sort";
            IList<PS_Table_AreaWH> listarea = Common.Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", connarea);
            int m = 0;
            WaitDialogForm frm = new WaitDialogForm("", "正在更新区域，请稍后...");
            foreach (PS_Table_AreaWH area in listarea)
            {
                m++;
                if (!AreaTable.Contains(area.Title))
                {

                    AddArea(area.Title);
                }
                double complete = m * 100 / listarea.Count;
                frm.Caption = "已完成" + Math.Round(complete, 0) + "%";
            }
            frm.Hide();
            LoadData1();
    
        }

        //更新变电站
        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //更新第2 5，8，11项

            string conn = "ProjectID='" + GetProjectID + "' and ParentID='0'";
            IList<Ps_Table_110Result> list = Common.Services.BaseService.GetList<Ps_Table_110Result>("SelectPs_Table_110ResultByConn", conn);
            int m = 0;
            WaitDialogForm frm = new WaitDialogForm("", "正在更新电源及变电站，请稍后...");
            foreach (Ps_Table_110Result pt in list)
            {
                m++;
                UpdataDY(pt.ID);
                UpDataRL(pt.ID);
                UpDataBDZ(pt.ID);
                UpData(pt.ID);
                double complete = m * 100 / list.Count;
                frm.Caption = "已完成" + Math.Round(complete, 0) + "%";
            }
            LoadData1();
            frm.Hide();
          
        }
        private string rongZai110 = "1.9";
        public string RongZai110
        {
            set { rongZai110 = value; }
            get { return rongZai110; }
        }
        public string RongZai(Ps_Table_200PH cur)
        {
            if (cur == null || cur.BuildYear == null || cur.BuildYear == "")
                return rongZai110;
            return cur.BuildYear;
        }
        //设置容载比 存放在BuildYear字段中
        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmRZ frm = new FrmRZ();
            string conn = "ParentID='0' and ProjectID='" + GetProjectID + "'";
            IList pareList = Common.Services.BaseService.GetList("SelectPs_Table_110ResultByConn", conn);
            for (int i = 0; i < pareList.Count; i++)
            {
                string by = ((Ps_Table_110Result)pareList[i]).BuildYear;
                if (by == null || by == "")
                    ((Ps_Table_110Result)pareList[i]).BuildYear = RongZai110; ;
            }
            frm.BindList = pareList;
            frm.RZ = RongZai110;
            frm.BRst = false;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                RongZai110 = frm.RZ;
                UpDataRZB();
            }
        }
        private void UpDataRZB()
        {
            string conn = "ProjectID='" + GetProjectID + "' and ParentID='0'";
            IList<Ps_Table_110Result> list = Common.Services.BaseService.GetList<Ps_Table_110Result>("SelectPs_Table_110ResultByConn", conn);
            foreach (Ps_Table_110Result pt in list)
            {
                UpData(pt.ID);
            }
            LoadData1();
        }
    }
}