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

namespace Itop.Client.Table
{
    public partial class Frm500PHTable : FormBase
    {
        DataTable dataTable;

        private TreeListNode lastEditNode = null;
        private TreeListColumn lastEditColumn = null;
        private string lastEditValue = string.Empty;
        private OperTable oper = new OperTable();
        public Ps_YearRange yAnge = new Ps_YearRange();
        private int typeFlag2 = 21;


        private bool _isSelect = false;

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

        public string GetProjectID
        {
            get { return ProjectUID; }
        }

        public Frm500PHTable()
        {
            InitializeComponent();
        }

        public int[] GetYears()
        {
            Ps_YearRange yr = yAnge;
            int[] year = new int[4] { yr.BeginYear,yr.StartYear, yr.FinishYear,yr.EndYear };
            return year;
        }

        private void HideToolBarButton()
        {
            if (!base.AddRight)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!base.EditRight)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem12.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem13.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }
            if (!base.DeleteRight)
            {
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem11.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            HideToolBarButton();
            yAnge = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.ph500 + "'");
            Show();
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
            string conn = "ProjectID='" + GetProjectID + "' and ParentID='0'";
            IList pList = Common.Services.BaseService.GetList("SelectPs_Table_500PHListByConn", conn);
            for (int i = 0; i < pList.Count; i++)
            {
                UpdateFuHe(((Ps_Table_500PH)pList[i]).Title, ((Ps_Table_500PH)pList[i]).ID);
            }
            string con = "ProjectID='" + GetProjectID + "'";
            listTypes = Common.Services.BaseService.GetList("SelectPs_Table_500PHListByConn", con);
            CaleHeTable(ref listTypes);
            AddRows(ref listTypes, "ParentID='0' and ProjectID='" + GetProjectID + "'");
          //  CalcTotal(ref listTypes);
            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Table_500PH));

            treeList1.DataSource = dataTable;

            treeList1.Columns["Title"].Caption = "项目名称";
            treeList1.Columns["Title"].Width = 250;
            treeList1.Columns["Title"].MinWidth = 250;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Title"].VisibleIndex = 0;
            CalcYearColumn();
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
         //   SetValueNull();
            //treeList1.ExpandAll();
            treeList1.CollapseAll();
        }
        public void Loaddata2()
        {
            string con = "ProjectID='" + GetProjectID + "'";
            listTypes = Common.Services.BaseService.GetList("SelectPs_Table_500PHListByConn", con);
            CaleHeTable(ref listTypes);
            AddRows(ref listTypes, "ParentID='0' and ProjectID='" + GetProjectID + "'");
           // CalcTotal(ref listTypes);
            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Table_500PH));

            treeList1.DataSource = dataTable;
            //treeList1.ExpandAll();
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
                        node.SetValue("y" + i.ToString(), null);
                    }
                }
            }
        }
        public void LoadData1(string fuID)
        {
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                //treeList1.Columns.Clear();
            }
            string conn = "(ID='" + fuID + "' or ParentID='" + fuID + "') and ProjectID='" + GetProjectID + "'";
            IList removeList = Common.Services.BaseService.GetList("SelectPs_Table_500PHListByConn", conn);
            IList cloneList = Common.Services.BaseService.GetList("SelectPs_Table_500PHListByConn", conn);
            cloneList.Clear();
            for (int i = 0; i < listTypes.Count; i++)
            {
                if (((Ps_Table_500PH)listTypes[i]).ID == fuID || ((Ps_Table_500PH)listTypes[i]).ParentID == fuID || ((Ps_Table_500PH)listTypes[i]).ID == totoalParent || ((Ps_Table_500PH)listTypes[i]).ParentID == totoalParent)
                {
                    //listTypes.Remove(listTypes[i]);
                }
                else
                    cloneList.Add(listTypes[i]);
            }
            listTypes.Clear();
            listTypes = cloneList;
            CaleHeTable(ref removeList);
            string con = "ID='" + fuID + "' and ProjectID='" + GetProjectID + "'";
            AddRows(ref removeList, con);
            for (int k = 0; k < removeList.Count; k++)
            {
                listTypes.Add((Ps_Table_500PH)removeList[k]);
            }
          //  CalcTotal(ref listTypes);
            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Table_500PH));
            treeList1.DataSource = dataTable;

           // SetValueNull();
            //treeList1.ExpandAll();
        }

        public void CaleHeTable(ref IList heList)
        {
            Ps_YearRange range = yAnge;
            for (int i = 0; i < heList.Count; i++)
            {
                if (((Ps_Table_500PH)heList[i]).Col1 == "1")
                {
                    string conn = "ParentID='" + ((Ps_Table_500PH)heList[i]).ID + "'";
                    IList tList = Common.Services.BaseService.GetList("SelectPs_Table_EditListByConn", conn);
                    for (int j = 0; j < tList.Count; j++)
                    {
                        if (((Ps_Table_Edit)tList[j]).Status == "扩建/改造")
                        {
                            for (int k = int.Parse(((Ps_Table_Edit)tList[j]).FinishYear); k <= range.EndYear; k++)
                            {
                                double old = (double)((Ps_Table_500PH)heList[i]).GetType().GetProperty("y" + k.ToString()).GetValue(((Ps_Table_500PH)heList[i]), null);
                                ((Ps_Table_500PH)heList[i]).GetType().GetProperty("y" + k.ToString()).SetValue(((Ps_Table_500PH)heList[i]), double.Parse(((Ps_Table_Edit)tList[j]).Volume) + old, null);
                            }
                        }
                        else if (((Ps_Table_Edit)tList[j]).Status == "拆除")
                        {
                            for (int k = int.Parse(((Ps_Table_Edit)tList[j]).FinishYear); k <= range.EndYear; k++)
                            {
                                ((Ps_Table_500PH)heList[i]).GetType().GetProperty("y" + k.ToString()).SetValue(((Ps_Table_500PH)heList[i]), 0.0, null);
                            }
                        }
                    }
                }
            }
        }

        public void CalcYearColumn()
        {
            int[] year = GetYears();
            for (int i = year[0]; i < year[1]; i++)
            {
                treeList1.Columns["y" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["y" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
                treeList1.Columns["y" + i.ToString()].ColumnEdit = repositoryItemTextEdit1;
                treeList1.Columns["y" + i.ToString()].Format.FormatString = "#####################0.##";
                treeList1.Columns["y" + i.ToString()].Format.FormatType = DevExpress.Utils.FormatType.Numeric;

            }
            for (int i = year[2] + 1; i <= year[3]; i++)
            {
                treeList1.Columns["y" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["y" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
                treeList1.Columns["y" + i.ToString()].ColumnEdit = repositoryItemTextEdit1;
                treeList1.Columns["y" + i.ToString()].Format.FormatString = "#####################0.##";
                treeList1.Columns["y" + i.ToString()].Format.FormatType = DevExpress.Utils.FormatType.Numeric;

            }
            for (int i = year[1]; i <= year[2]; i++)
            {
                treeList1.Columns["y" + i.ToString()].Caption = i.ToString() + "年";
                treeList1.Columns["y" + i.ToString()].VisibleIndex = i;
                treeList1.Columns["y" + i.ToString()].Width = 60;
                treeList1.Columns["y" + i.ToString()].OptionsColumn.AllowEdit = false;
                treeList1.Columns["y" + i.ToString()].OptionsColumn.AllowSort = false;
                treeList1.Columns["y" + i.ToString()].ColumnEdit = repositoryItemTextEdit1;
                treeList1.Columns["y" + i.ToString()].Format.FormatString = "#####################0.##";
                treeList1.Columns["y" + i.ToString()].Format.FormatType = DevExpress.Utils.FormatType.Numeric;


            }
        }

        private string rongZai220 = "1.5";
        public string RongZai220
        {
            set { rongZai220 = value; }
            get { return rongZai220; }
        }
        public string RongZai(Ps_Table_500PH cur)
        {
            if (cur == null || cur.BuildYear == null || cur.BuildYear == "")
                return rongZai220;
            return cur.BuildYear;
        }
        //private Dictionary<string, double> dict = new Dictionary<string, double>();
        //public Dictionary<string, double> Dict
        //{
        //    get { return dict; }
        //    set { dict = value; }
        //}

        public void AddRows(ref IList list, string con)
        {
            //IList list = new List<Ps_Table_500PH>();
            string conn = "ParentID='0' and ProjectID='" + GetProjectID + "'";

            int[] year = GetYears();
            IList pareList = Common.Services.BaseService.GetList("SelectPs_Table_500PHListByConn", con);
            try
            {
                for (int i = 0; i < pareList.Count; i++)
                {
                    Ps_Table_500PH ps_table1 = new Ps_Table_500PH();
                    //Ps_Table_500PH con = new Ps_Table_500PH();
                    //con.Col4 = rongZai220;
                    //con.Title = "ParentID='" + ((Ps_Table_500PH)pareList[i]).ID + "' and Col1='0'";
                    //IList childList1 = Common.Services.BaseService.GetList("SelectPs_Table_500PHJiByConn", con);
                    //CaleHeTable(ref childList1);
                    //ps_table1 = (Ps_Table_500PH)childList1[0];
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (((Ps_Table_500PH)list[j]).ParentID == ((Ps_Table_500PH)pareList[i]).ID && ((Ps_Table_500PH)list[j]).Col1 == "0")
                        {
                            for (int k = year[1]; k <= year[2]; k++)
                            {
                                ps_table1.GetType().GetProperty("y" + k).SetValue(ps_table1, Math.Round(double.Parse(((Ps_Table_500PH)list[j]).GetType().GetProperty("y" + k).GetValue(((Ps_Table_500PH)list[j]), null).ToString()) * double.Parse(RongZai((Ps_Table_500PH)pareList[i])), 1), null);
                            }
                        }
                    }
                    ps_table1.ParentID = ((Ps_Table_500PH)pareList[i]).ID;
                    ps_table1.ID = Guid.NewGuid().ToString();
                    ps_table1.ID += "|" + GetProjectID;
                    ps_table1.Sort = 2;
                    ps_table1.Col1 = "no";
                    ps_table1.Title = "500千伏需要容量(" + RongZai((Ps_Table_500PH)pareList[i]) + ")";
                    list.Add(ps_table1);

                    Ps_Table_500PH ps_table = new Ps_Table_500PH();

                    //conn = "ParentID='" + ((Ps_Table_500PH)pareList[i]).ID + "' and Col1='1'";
                    //IList childList = Common.Services.BaseService.GetList("SelectPs_Table_500PHSumByConn", conn);
                    //CaleHeTable(ref childList);
                    //ps_table = (Ps_Table_500PH)childList[0];
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (((Ps_Table_500PH)list[j]).ParentID == ((Ps_Table_500PH)pareList[i]).ID && ((Ps_Table_500PH)list[j]).Col1 == "1")
                        {
                            for (int k = year[1]; k <= year[2]; k++)
                            {
                                ps_table.GetType().GetProperty("y" + k).SetValue(ps_table, double.Parse(ps_table.GetType().GetProperty("y" + k).GetValue(ps_table, null).ToString()) + double.Parse(((Ps_Table_500PH)list[j]).GetType().GetProperty("y" + k).GetValue(((Ps_Table_500PH)list[j]), null).ToString()), null);
                            }
                        }
                    }
                    ps_table.ParentID = ((Ps_Table_500PH)pareList[i]).ID;
                    ps_table.ID = Guid.NewGuid().ToString();
                    ps_table.ID += "|" + GetProjectID;
                    ps_table.Sort = 3;
                    ps_table.Col1 = "no";
                    ps_table.Title = "500千伏公用变电站总容量";
                    list.Add(ps_table);

                    // conn = "ParentID='" + ((Ps_Table_500PH)pareList[i]).ID + "' and Col1='0'";
                    Ps_Table_500PH temp = new Ps_Table_500PH();// (Ps_Table_500PH)Common.Services.BaseService.GetList("SelectPs_Table_500PHListByConn", conn)[0];
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (((Ps_Table_500PH)list[j]).ParentID == ((Ps_Table_500PH)pareList[i]).ID && ((Ps_Table_500PH)list[j]).Col1 == "0")
                        {
                            temp = (Ps_Table_500PH)list[j];
                        }
                    }
                    Ps_Table_500PH ps_table2 = new Ps_Table_500PH();
                    for (int j = year[1]; j <= year[2]; j++)
                    {
                        double d = (double)temp.GetType().GetProperty("y" + j).GetValue(temp, null);
                        if (d != 0.0)
                        {
                            double chu = (double)ps_table.GetType().GetProperty("y" + j).GetValue(ps_table, null);
                            ps_table2.GetType().GetProperty("y" + j).SetValue(ps_table2, Math.Round(chu / d, 1), null);
                        }
                    }
                    ps_table2.ParentID = ((Ps_Table_500PH)pareList[i]).ID;
                    ps_table2.ID = Guid.NewGuid().ToString();
                    ps_table2.ID += "|" + GetProjectID;
                    ps_table2.Sort = 1000;
                    ps_table2.Col1 = "no";
                    ps_table2.Title = "500千伏容载比";
                    list.Add(ps_table2);
                }

                //  CalcTotal(ref list);
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
            //return list;
        }

        private string totoalParent;
        private void CalcTotal(ref IList list)
        {
            //合计
            string conn = "ParentID='0' and ProjectID='" + GetProjectID + "'";

            int[] year = GetYears();
            Ps_Table_500PH parent = new Ps_Table_500PH();
            parent.ID += "|" + GetProjectID;
            parent.ParentID = "0"; parent.Title = "全网500千伏合计"; parent.Sort = 1000;// OperTable.GetMaxSort() + 1;
            list.Add(parent);
            totoalParent = parent.ID;
            Ps_Table_500PH tablex = new Ps_Table_500PH();
            conn = "Col1='0' and ProjectID='" + GetProjectID + "'";
            IList childx = Common.Services.BaseService.GetList("SelectPs_Table_500PHSumByConn", conn);
            tablex = (Ps_Table_500PH)childx[0];
            tablex.ParentID = parent.ID;
            tablex.ID = Guid.NewGuid().ToString();
            tablex.ID += "|" + GetProjectID;
            tablex.Sort = 1;
            tablex.Col1 = "no";
            tablex.Title = "500千伏主变供电负荷";
            list.Add(tablex);

            Ps_Table_500PH table1 = new Ps_Table_500PH();
            // Ps_Table_500PH con1 = new Ps_Table_500PH();
            // con1.Col4 = rongZai220;
            conn = "Col1='0' and ProjectID='" + GetProjectID + "'";// con1.Title = "Col1='0'";
            //IList child1 = Common.Services.BaseService.GetList("SelectPs_Table_500PHSumByConn", conn);
            table1 = (Ps_Table_500PH)tablex.Clone();// (Ps_Table_500PH)child1[0];
            for (int j = year[1]; j <= year[2]; j++)
            {
                double d = (double)table1.GetType().GetProperty("y" + j).GetValue(table1, null);
                table1.GetType().GetProperty("y" + j).SetValue(table1, Math.Round(double.Parse(rongZai220) * d, 1), null);

            }
            table1.ParentID = parent.ID;
            table1.ID = Guid.NewGuid().ToString();
            table1.ID += "|" + GetProjectID;
            table1.Sort = 2;
            table1.Col1 = "no";
            table1.Title = "500千伏需主变容量(" + rongZai220 + ")";
            list.Add(table1);

            Ps_Table_500PH table = new Ps_Table_500PH();
            //conn = "Col1='1'";
            //IList child = Common.Services.BaseService.GetList("SelectPs_Table_500PHSumByConn", conn);
            //table = (Ps_Table_500PH)child[0];
            for (int j = 0; j < list.Count; j++)
            {
                if (((Ps_Table_500PH)list[j]).Col1 == "1")
                {
                    for (int k = year[1]; k <= year[2]; k++)
                    {
                        table.GetType().GetProperty("y" + k).SetValue(table, double.Parse(table.GetType().GetProperty("y" + k).GetValue(table, null).ToString()) + double.Parse(((Ps_Table_500PH)list[j]).GetType().GetProperty("y" + k).GetValue(((Ps_Table_500PH)list[j]), null).ToString()), null);
                    }
                }
            }
            table.ParentID = parent.ID;
            table.ID = Guid.NewGuid().ToString();
            table.ID += "|" + GetProjectID;
            table.Sort = 3;
            table.Col1 = "no";
            table.Title = "500千伏主变总容量";
            list.Add(table);



            //IList allChild = Common.Services.BaseService.GetList("SelectPs_Table_500PHListByConn", conn);
            for (int k = 0; k < list.Count; k++)
            {
                if (((Ps_Table_500PH)list[k]).Col1 == "1")
                {
                    Ps_Table_500PH ps1 = new Ps_Table_500PH();
                    ps1 = (Ps_Table_500PH)((Ps_Table_500PH)list[k]).Clone();
                    ps1.ID = Guid.NewGuid().ToString();
                    ps1.ID += "|" + GetProjectID;
                    ps1.ParentID = parent.ID;
                    ps1.Col1 = "no";
                    ps1.BuildEd = "total";
                    list.Add(ps1);
                }
            }


            //conn = "Col1='0'";
            //Ps_Table_500PH temp1 = (Ps_Table_500PH)Common.Services.BaseService.GetList("SelectPs_Table_500PHListByConn", conn)[0];
            Ps_Table_500PH table2 = new Ps_Table_500PH();
            for (int j = year[1]; j <= year[2]; j++)
            {
                double d = (double)tablex.GetType().GetProperty("y" + j).GetValue(tablex, null);
                if (d != 0.0)
                {
                    double chu = (double)table.GetType().GetProperty("y" + j).GetValue(table, null);
                    table2.GetType().GetProperty("y" + j).SetValue(table2, Math.Round(chu / d, 1), null);
                }
            }
            table2.ParentID = parent.ID;
            table2.ID = Guid.NewGuid().ToString();
            table2.ID += "|" + GetProjectID; 
            table2.Sort = 1000;
            table2.Col1 = "no";
            table2.Title = "500千伏容载比";
            list.Add(table2);
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
        //增加项目
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

            Ps_YearRange range = yAnge;
            Ps_Table_500PH table = new Ps_Table_500PH();
            table.ID += "|" + GetProjectID;
            FrmPsNew frm = new FrmPsNew();
            frm.GetProject = GetProjectID;
            frm.ParentID = table.ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                table.Title = frm.GetTitle;
                if (focusedNode.GetValue("ParentID").ToString() == "0")
                {
                    table.ParentID = focusedNode.GetValue("ID").ToString();
                }
                else
                    table.ParentID = focusedNode.GetValue("ParentID").ToString();
                table.Col1 = "1";
                table.ProjectID = GetProjectID;
                table.Sort = OperTable.Get500MaxSort() + 1;
                if (table.Sort < 4)
                    table.Sort = 4;
                int sYear = int.Parse(frm.GetYear);
                for (int i = sYear; i <= range.EndYear; i++)
                {
                    table.GetType().GetProperty("y" + i.ToString()).SetValue(table, double.Parse(frm.GetVolumn), null);
                }
                try
                {
                    Common.Services.BaseService.Create("InsertPs_Table_500PH", table);
                    //dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(table, dataTable.NewRow()));
                    LoadData1(table.ParentID);
                    FoucsLocation(table.ID, treeList1.Nodes);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加项目出错：" + ex.Message);
                }
            }
            //FrmChangeBian frm = new FrmChangeBian();
            //frm.Text = "增加" + focusedNode.GetValue("Title") + "的子项目";
            //Hashtable ht = new Hashtable();
            //for (int i = range.StartYear; i <= range.FinishYear; i++)
            //{
            //    ht.Add("y" + i.ToString(), focusedNode.GetValue("y" + i.ToString()).ToString());
            //}
            //frm.TextAttr = ht;
            //if(frm.ShowDialog() == DialogResult.OK)
            //{
            //    Ps_Table_500PH table = new Ps_Table_500PH();
            //    table.Title = frm.Title;
            //    table.ParentID = focusedNode.GetValue("ID").ToString();
            //    for (int i = range.StartYear; i <= range.FinishYear; i++)
            //    {
            //        table.GetType().GetProperty("y" + i.ToString()).SetValue(table, Convert.ToDouble(frm.TextAttr["y" + i.ToString()]), null);
            //    }
            //    double end = Convert.ToDouble(frm.TextAttr["y" + range.FinishYear.ToString()]);
            //    for (int j = range.FinishYear + 1; j <= range.EndYear; j++)
            //    {
            //        table.GetType().GetProperty("y" + j.ToString()).SetValue(table, end, null);
            //    }
            //    table.Col1 = "1";
            //    table.Sort = OperTable.GetMaxSort() + 1;
            //    try
            //    {
            //        Common.Services.BaseService.Create("InsertPs_Table_500PH", table);
            //        //dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(table, dataTable.NewRow()));
            //        LoadData();
            //    }
            //    catch(Exception ex)
            //    {
            //        MsgBox.Show("增加项目出错：" + ex.Message);
            //    }
            //}
        }
        //修改
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditPsTable();
        }

        public void EditPsTable()
        {
            TreeListNode focusedNode = treeList1.FocusedNode;

            if (focusedNode == null)
            {
                return;
            }

            if (!base.EditRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            Ps_YearRange range = yAnge;
            if (focusedNode.GetValue("Col1") != null && focusedNode.GetValue("Col1").ToString() == "0")
            {
                FrmChangeBian frm = new FrmChangeBian();
                frm.GetProject = GetProjectID;
                frm.Mark = OperTable.ph500;
                frm.Text = "修改" + focusedNode.GetValue("Title");
                Hashtable ht = new Hashtable();
                for (int i = range.StartYear; i <= range.FinishYear; i++)
                {
                    ht.Add("y" + i.ToString(), focusedNode.GetValue("y" + i.ToString()).ToString());
                }
                frm.TextAttr = ht;
                frm.Title = focusedNode.GetValue("Title").ToString();
                if (focusedNode.GetValue("Col1").ToString() == "0")
                {
                    frm.SetEnable();
                    frm.BFuHe = true;
                }
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Ps_Table_500PH table = new Ps_Table_500PH();
                    table.ID = focusedNode.GetValue("ID").ToString();
                    table.Title = frm.Title;
                    table.ParentID = focusedNode.GetValue("ParentID").ToString();
                    for (int i = range.StartYear; i <= range.FinishYear; i++)
                    {
                        table.GetType().GetProperty("y" + i.ToString()).SetValue(table, Convert.ToDouble(frm.TextAttr["y" + i.ToString()]), null);
                    }
                    double end = Convert.ToDouble(frm.TextAttr["y" + range.FinishYear.ToString()]);
                    for (int j = range.FinishYear + 1; j <= range.EndYear; j++)
                    {
                        table.GetType().GetProperty("y" + j.ToString()).SetValue(table, end, null);
                    }
                    table.Col1 = focusedNode.GetValue("Col1").ToString();
                    table.ProjectID = GetProjectID;
                    table.Sort = int.Parse(focusedNode.GetValue("Sort").ToString());
                    try
                    {
                        Common.Services.BaseService.Update("UpdatePs_Table_500PH", table);
                        LoadData1(table.ParentID);
                        FoucsLocation(table.ID, treeList1.Nodes);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("修改项目出错：" + ex.Message);
                    }
                }
            }
            else if (focusedNode.GetValue("Col1") != null && focusedNode.GetValue("Col1").ToString() == "1")
            {
                string conn = "ParentID='" + focusedNode.GetValue("ID").ToString() + "'";
                IList<Ps_Table_Edit> eList = Common.Services.BaseService.GetList<Ps_Table_Edit>("SelectPs_Table_EditListByConn", conn);

                FrmPsEdit frm = new FrmPsEdit();
                frm.GetProject = GetProjectID;
                frm.Mark = OperTable.ph500;
                frm.GridData = eList;
                frm.Title = focusedNode.GetValue("Title").ToString();
                frm.ParentID = focusedNode.GetValue("ID").ToString();
                string curVolumn = focusedNode.GetValue("y" + range.EndYear).ToString();
                frm.CurVolumn = curVolumn;
                frm.MaxYear = GetChildMaxYear(conn);
                frm.TypeTable = "500";
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Ps_Table_500PH table = new Ps_Table_500PH();
                    table = Common.Services.BaseService.GetOneByKey<Ps_Table_500PH>(focusedNode.GetValue("ID"));
                    //table.ID = focusedNode.GetValue("ID").ToString();
                    table.Title = frm.Title;
                    //table.ParentID = focusedNode.GetValue("ParentID").ToString();
                    //table.Col1 = focusedNode.GetValue("Col1").ToString();
                    //table.Sort = int.Parse(focusedNode.GetValue("Sort").ToString());
                    //table.Title = frm.StrResult[0];
                    //if (frm.GetStatus == "已有")
                    //{
                    //    for (int i = range.BeginYear; i <= range.EndYear; i++)
                    //    {
                    //        if (table.GetType().GetProperty("y" + i.ToString()).GetValue(table, null).ToString() == curVolumn)
                    //            table.GetType().GetProperty("y" + i.ToString()).SetValue(table, double.Parse(frm.StrResult[2]), null);
                    //    }
                    //}
                    //else if (frm.GetStatus == "扩建")
                    //{
                    //    for (int i = int.Parse(frm.StrResult[1]); i <= range.EndYear; i++)
                    //    {
                    //        table.GetType().GetProperty("y" + i.ToString()).SetValue(table, double.Parse(frm.StrResult[2]), null);
                    //    }
                    //}
                    //else if (frm.GetStatus == "拆除")
                    //{
                    //    for (int i = int.Parse(frm.StrResult[1]); i <= range.EndYear; i++)
                    //    {
                    //        table.GetType().GetProperty("y" + i.ToString()).SetValue(table, 0.0, null);
                    //    }
                    //    table.ParentID = "-1";
                    //}
                    try
                    {
                        Common.Services.BaseService.Update("UpdatePs_Table_500PH", table);
                        LoadData1(table.ParentID);
                        FoucsLocation(table.ID, treeList1.Nodes);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("修改项目出错：" + ex.Message);
                    }
                }
            }
            else if (focusedNode.GetValue("ParentID").ToString() == "0")
            {
                FrmAddPN frm = new FrmAddPN();
                frm.ParentName = focusedNode.GetValue("Title").ToString();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Ps_Table_500PH table1 = new Ps_Table_500PH();
                    //table1 = focusedNode.TreeList.GetDataRecordByNode(focusedNode) as Ps_Table_500PH;
                    table1.ID = focusedNode.GetValue("ID").ToString();
                    table1.ParentID = focusedNode.GetValue("ParentID").ToString();
                    table1.Sort = int.Parse(focusedNode.GetValue("Sort").ToString());
                    table1.Title = frm.ParentName;
                    table1.ProjectID = GetProjectID;
                    table1.BuildYear = focusedNode.GetValue("BuildYear").ToString();
                    table1.Col1 = focusedNode.GetValue("Col1").ToString(); 
                    try
                    {
                        Common.Services.BaseService.Update("UpdatePs_Table_500PH", table1);
                        LoadData1(table1.ID);

                        FoucsLocation(table1.ID, treeList1.Nodes);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("修改分类出错：" + ex.Message);
                    }
                }
            }
            else
                MsgBox.Show("不能修改此行");
        }

        public string GetChildMaxYear(string value)
        {
            try
            {
                return Common.Services.BaseService.GetObject("SelectMaxYear", value).ToString();
            }
            catch { return ""; }
        }
        //删除
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

            if (treeList1.FocusedNode.GetValue("ParentID").ToString() == "0")
            {
                string conn = "ParentID='" + treeList1.FocusedNode.GetValue("ID").ToString() + "' and Col1='1'";
                IList list = Common.Services.BaseService.GetList("SelectPs_Table_500PHListByConn", conn);
                if (list.Count > 0)
                {
                    MsgBox.Show("这条记录下面有子记录，请先删除子记录！");
                    return;
                }
            }
            if (treeList1.FocusedNode.GetValue("Col1").ToString() == "no")
            {
                MsgBox.Show("不能删除此行！");
                return;
            }

            if (MsgBox.ShowYesNo("是否删除 " + treeList1.FocusedNode.GetValue("Title") + "？") == DialogResult.Yes)
            {
                try
                {
                    Ps_Table_500PH ny = new Ps_Table_500PH();
                    ny.ID = treeList1.FocusedNode.GetValue("ID").ToString();
                    string teID="";
                    try
                    {
                        teID = treeList1.FocusedNode.NextNode.GetValue("ID").ToString();
                    }
                    catch { }
                    string pare = treeList1.FocusedNode.GetValue("ParentID").ToString();
                    Common.Services.BaseService.Delete(ny);
                    DelAll(ny.ID);
                    if (pare != "0")
                        LoadData1(pare);// 1(ny.ParentID);
                    else
                        LoadData1(ny.ID);

                    FoucsLocation(teID, treeList1.Nodes);
                }
                catch { }
            }
        }
        //删除所有
        public void DelAll(string suid)
        {
            string conn = "ParentId='" + suid + "'";
            IList<Ps_Table_500PH> list = Common.Services.BaseService.GetList<Ps_Table_500PH>("SelectPs_Table_500PHListByConn", conn);
            if (list.Count > 0)
            {
                foreach (Ps_Table_500PH var in list)
                {
                    string child = var.ID;
                    //if (var.Sort != "1")
                    //    DelAll(child);
                    Ps_Table_500PH ny = new Ps_Table_500PH();
                    ny.ID = child;
                    Common.Services.BaseService.Delete(ny);
                }
            }
        }
        //设置容载比
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormPSP_VolumeBalanceVolumecalc calc = new FormPSP_VolumeBalanceVolumecalc();
            //calc.SetSpanText = 1.9;
            //if (calc.ShowDialog() == DialogResult.OK)
            //{
            //    RongZai220 = calc.SetSpanText.ToString();
            //    LoadData();
            //}
            FrmRZ frm = new FrmRZ();
            string conn = "ParentID='0' and ProjectID='" + GetProjectID + "'";
            IList pareList = Common.Services.BaseService.GetList("SelectPs_Table_500PHListByConn", conn);
            for (int i = 0; i < pareList.Count; i++)
            {
                string by=((Ps_Table_500PH)pareList[i]).BuildYear;
                if (by == null || by == "")
                    ((Ps_Table_500PH)pareList[i]).BuildYear = rongZai220; ;
            }
            frm.BindList = pareList;
            frm.BRst = false;
            frm.RZ = rongZai220;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                RongZai220 = frm.RZ;
                LoadData();
            }
        }
        //载入负荷数据
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            //if ((e.Value.ToString() != lastEditValue
            //    || lastEditNode != e.Node
            //    || lastEditColumn != e.Column)
            //    && e.Column.FieldName.IndexOf("年") > 0
            //    && e.Column.Tag != null)
            //{
            //    if (SaveCellValue((int)treeList1.FocusedColumn.Tag, (int)treeList1.FocusedNode.GetValue("ID"), Convert.ToDouble(e.Value)))
            //    {
            //        //CalculateSum(e.Node, e.Column);
            //    }
            //    else
            //    {
            //        treeList1.FocusedNode.SetValue(treeList1.FocusedColumn.FieldName, lastEditValue);
            //    }
            //}
        }

        private bool SaveCellValue(int year, int typeID, double value)
        {
            PSP_Values psp_values = new PSP_Values();
            psp_values.TypeID = typeID;
            psp_values.Value = value;
            psp_values.Year = year;

            try
            {
                Common.Services.BaseService.Update<PSP_Values>(psp_values);
            }
            catch (Exception ex)
            {
                MsgBox.Show("保存数据出错：" + ex.Message);
                return false;
            }
            return true;
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (treeList1.FocusedNode.HasChildren
                || !base.EditRight)
            {
                e.Cancel = true;
            }
        }

        //当子分类数据改变时，计算其父分类的值
        private void CalculateSum(TreeListNode node, TreeListColumn column)
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
                if (value != null && value != DBNull.Value)
                {
                    sum += Convert.ToDouble(value);
                }
            }

            parentNode.SetValue(column.FieldName, sum);

            if (!SaveCellValue((int)column.Tag, (int)parentNode.GetValue("ID"), sum))
            {
                return;
            }

            CalculateSum(parentNode, column);
        }

        private void treeList1_ShownEditor(object sender, EventArgs e)
        {
            lastEditColumn = treeList1.FocusedColumn;
            lastEditNode = treeList1.FocusedNode;
            lastEditValue = treeList1.FocusedNode.GetValue(lastEditColumn.FieldName).ToString();
        }

        //统计
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormChooseYears frm = new FormChooseYears();
            for (int i = yAnge.StartYear; i <= yAnge.FinishYear; i++)
            {
                frm.ListYearsForChoose.Add(i);

            }
            frm.NoIncreaseRate = true;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                //////////Form1Result f = new Form1Result();
                //////////f.CanPrint = base.PrintRight;
                //////////f.Text = Title = "本地区电力平衡表";// + frm.ListChoosedYears[0].Year + "～" + frm.ListChoosedYears[frm.ListChoosedYears.Count - 1].Year + "年经济和电力发展实绩";
                //////////f.ColTitleWidth = 250;
                //////////f.ColTitleAlign = DevExpress.Utils.HorzAlignment.Default;
                //////////f.GridDataTable = ResultDataTable(ConvertTreeListToDataTable(treeList1), frm.ListChoosedYears);
                //////////f.IsSelect = IsSelect;
                //////////DialogResult dr = f.ShowDialog();
                //////////if (IsSelect && dr == DialogResult.OK)
                //////////{
                //////////    Gcontrol = f.gridControl1;
                //////////    Unit = "单位：万千瓦";
                //////////    DialogResult = DialogResult.OK;
                //////////}


                Form21Print frma = new Form21Print();
                frma.IsSelect = _isSelect;
                frma.Text = "500千伏分区分年电容量计算表";
                DataTable dt = ConvertTreeListToDataTable(treeList1,false);
                frma.Dw1 = "单位：万千瓦、万千伏安";
                frma.GridDataTable = ResultDataTable(ConvertTreeListToDataTable(treeList1,false), frm.ListChoosedYears);


                if (frma.ShowDialog() == DialogResult.OK && _isSelect)
                {
                    //gcontrol = frm.gridControl1;
                    //title = frm.Title;
                    //unit = "单位：万元";
                    DialogResult = DialogResult.OK;
                }





            }
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
                listColID.Add("y" + i.ToString());
                dt.Columns.Add("y" + i.ToString(), typeof(double));
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
            return "y" + year.Substring(0, 4);
        }

        //根据选择的统计年份，生成统计结果数据表
        private DataTable ResultDataTable(DataTable sourceDataTable, List<ChoosedYears> listChoosedYears)
        {
            DataTable dtReturn = new DataTable();
            dtReturn.Columns.Add("Title", typeof(string));
            foreach (ChoosedYears choosedYear in listChoosedYears)
            {
                dtReturn.Columns.Add(choosedYear.Year + "年", typeof(double));
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

        private DataTable ResultDataTable1(DataTable sourceDataTable, List<ChoosedYears> listChoosedYears)
        {
            DataTable dtReturn = new DataTable();
            dtReturn.Columns.Add("Title", typeof(string));
            foreach (ChoosedYears choosedYear in listChoosedYears)
            {
                dtReturn.Columns.Add(choosedYear.Year + "年", typeof(double));
            }

            int nRowMaxLoad = 0;//地区最高负荷所在行
            int nRowMaxPower = 0;//地区电网供电能力所在行
            int nRowMaxPowerLow = 0;//枯水期地区电网供电能力所在行

            #region 填充数据
            for (int i = 0; i < sourceDataTable.Rows.Count; i++)
            {
                if (sourceDataTable.Rows[i]["Title"].ToString().IndexOf("500千伏需要容量") != -1)
                    continue;
                DataRow newRow = dtReturn.NewRow();
                DataRow sourceRow = sourceDataTable.Rows[i];
                foreach (DataColumn column in dtReturn.Columns)
                {
                    newRow[column.ColumnName] = sourceRow[ConvertYear(column.ColumnName)];
                }
                dtReturn.Rows.Add(newRow);


            }
            #endregion


            return dtReturn;
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
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
        //添加父分类
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!base.AddRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            TreeListNode focusedNode = treeList1.FocusedNode;

            //if (focusedNode == null)
            //{
            //    return;
            //}


            FrmAddPN frm = new FrmAddPN();
            frm.SetCheckVisible();
            frm.SetCheckText("不计特殊");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_Table_500PH table_yd = new Ps_Table_500PH();
                table_yd.ID += "|" + GetProjectID;
                table_yd.Title = frm.ParentName;
                table_yd.ParentID = "0";
                table_yd.Sort = OperTable.Get500MaxSort() + 1;
                table_yd.ProjectID = GetProjectID;

                
                try
                {
                    Common.Services.BaseService.Create("InsertPs_Table_500PH", table_yd);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加分类出错：" + ex.Message);
                }

                Ps_Table_500PH table1 = new Ps_Table_500PH();
                table1.ID += "|" + GetProjectID;
                table1.Title = "500千伏公用变电站供电负荷";
                table1.ParentID = table_yd.ID;
                table1.ProjectID = GetProjectID;
                table1.Col1 = "0";
                if (frm.BCheck)
                    table1.Col2 = "no";
                else
                    table1.Col2 = "no1";
                table1.Sort = 1;
                try
                {
                    Common.Services.BaseService.Create("InsertPs_Table_500PH", table1);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加项目出错：" + ex.Message);
                }
                UpdateFuHe(table_yd.Title, table_yd.ID);
                this.Cursor = Cursors.WaitCursor;
                treeList1.BeginUpdate();
                LoadData1(table_yd.ID);
                FoucsLocation(table_yd.ID, treeList1.Nodes);
                treeList1.EndUpdate();
                this.Cursor = Cursors.Default;
            }
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
        public void UpdateFuHe(string title, string id)
        {
            string title1 = title;
            if (title1.IndexOf('（') != -1)
            {
                title1 = title1.Substring(0, title1.IndexOf('（'));
            }
            string conn = "ProjectID='" + GetProjectID + "' and Title='" + title1 + "' and parentID='0'";
            IList listf = Common.Services.BaseService.GetList("SelectPs_Table_500ResultByConn", conn);
            if (listf.Count > 0)
            {
                conn = "ProjectID='" + GetProjectID + "' and ParentID='" + id + "' and Col1='0'";
                IList myList = Common.Services.BaseService.GetList("SelectPs_Table_500PHListByConn", conn);
                if (myList.Count > 0)
                {
                    conn = "ProjectID='" + GetProjectID + "' and Col1='" + ((Ps_Table_500PH)myList[0]).Col2 + "' and ParentID='" + ((Ps_Table_500Result)listf[0]).ID + "'";
                    IList listt = Common.Services.BaseService.GetList("SelectPs_Table_500ResultByConn", conn);
                    if (listt.Count == 0)
                    {
                        conn = "ProjectID='" + GetProjectID + "' and Col1='" + "no1"/*((Ps_Table_500PH)myList[0]).Col2*/ + "' and ParentID='" + ((Ps_Table_500Result)listf[0]).ID + "'";
                        listt = Common.Services.BaseService.GetList("SelectPs_Table_500ResultByConn", conn);
                    }
                    if (listt.Count > 0)
                    {
                        for (int i = yAnge.BeginYear; i <= yAnge.EndYear; i++)
                        {
                            double t1 = double.Parse(listt[0].GetType().GetProperty("yk" + i.ToString()).GetValue(listt[0], null).ToString());
                            myList[0].GetType().GetProperty("y" + i.ToString()).SetValue(myList[0], t1, null);
                        }
                        Common.Services.BaseService.Update<Ps_Table_500PH>((Ps_Table_500PH)myList[0]);
                    }
                }
            }
        }
        private void treeList1_DoubleClick(object sender, EventArgs e)
        {
            EditPsTable();
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormChooseYears frm = new FormChooseYears();
            for (int i = yAnge.StartYear; i <= yAnge.FinishYear; i++)
            {
                frm.ListYearsForChoose.Add(i);

            }
            frm.NoIncreaseRate = true;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Form21Print frma = new Form21Print();
                frma.IsSelect = _isSelect;
                frma.Text = "500千伏分区分年容载比计算表";
                frma.Dw1 = "单位：万千瓦、万千伏安";
                frma.GridDataTable = ResultDataTable1(ConvertTreeListToDataTable(treeList1, true), frm.ListChoosedYears);
                if (frma.ShowDialog() == DialogResult.OK && _isSelect)
                {
                    DialogResult = DialogResult.OK;
                }

            }
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCopy frm = new FrmCopy();
            frm.CurID = GetProjectID;
            frm.ClassName = "Ps_Table_500PH";
            frm.SelectString = "SelectPs_Table_500PHListByConn";
            frm.InsertString = "InsertPs_Table_500PH";
            frm.ExistChild = true;
            frm.ChildClassName.Add("Ps_Table_Edit");
            frm.ChildInsertName.Add("InsertPs_Table_Edit");
            frm.ChildSelectString.Add("SelectPs_Table_EditListByConn");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("导入成功");
                Loaddata2();
            }
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("该操作将清除当前项目的所有数据，清除数据以后无法恢复,可能对其他用户的数据产生影响，请谨慎操作，你确定继续吗？", "删除", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string conn = "ProjectID='" + GetProjectID + "'";
                Common.Services.BaseService.Update("DeletePs_Table_500PHByConn", conn);
                //Common.Services.BaseService.Update("DeletePs_Table_EditByConn", conn);
                Loaddata2();
            }
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormYearSet fys = new FormYearSet();
            fys.TYPE = OperTable.ph500;
            fys.PID = ProjectUID;
            if (fys.ShowDialog() != DialogResult.OK)
                return;
            yAnge = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.ph500 + "'");
            LoadData();
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string conn = "ProjectID='" + GetProjectID + "' and ParentID='0'";
            IList pList = Common.Services.BaseService.GetList("SelectPs_Table_500PHListByConn", conn);
            for (int i = 0; i < pList.Count; i++)
            {
                UpdateFuHe(((Ps_Table_500PH)pList[i]).Title, ((Ps_Table_500PH)pList[i]).ID);
            }
            Loaddata2();
        }
    }
}