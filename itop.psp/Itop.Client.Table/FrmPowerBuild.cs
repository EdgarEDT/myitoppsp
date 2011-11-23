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
    public partial class FrmPowerBuild : FormBase
    {
        DataTable dataTable;

        private TreeListNode lastEditNode = null;
        private TreeListColumn lastEditColumn = null;
        private string lastEditValue = string.Empty;

        private int typeFlag2 = 21;

        private OperTable oper = new OperTable();
        public Ps_YearRange yAnge=new Ps_YearRange();// = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.mark + "'");
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

        public FrmPowerBuild()
        {
            InitializeComponent();
        }

        public int[] GetYears()
        {
            Ps_YearRange yr = oper.GetYearRange("Col5='"+GetProjectID+"' and Col4='"+OperTable.power+"'");
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
                barButtonItem12.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!base.EditRight)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem13.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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
            yAnge = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.power + "'");
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
            string con = "ProjectID='" + GetProjectID + "'";
            listTypes = Common.Services.BaseService.GetList("SelectPs_PowerBuildByConn", con);
            CaleHeTable(ref listTypes);
          //  AddRows(ref listTypes, "ParentID='0' and ProjectID='" + GetProjectID + "'");
           // CalcTotal(ref listTypes);
            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_PowerBuild));

            treeList1.DataSource = dataTable;

            treeList1.Columns["Title"].Caption = "项目名称";
            treeList1.Columns["Title"].Width = 250;
            treeList1.Columns["Title"].MinWidth = 250;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Title"].VisibleIndex = 0;
            treeList1.Columns["Type"].Caption = "电源类型";
            treeList1.Columns["Type"].Width = 100;
            treeList1.Columns["Type"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Type"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Type"].VisibleIndex = 1;
            treeList1.Columns["BuildFac"].Caption = "建设规模";
            treeList1.Columns["BuildFac"].Width = 100;
            treeList1.Columns["BuildFac"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["BuildFac"].OptionsColumn.AllowSort = false;
            treeList1.Columns["BuildFac"].VisibleIndex = 2;
            treeList1.Columns["BuildYear"].Caption = "开工年份";
            treeList1.Columns["BuildYear"].Width = 100;
            treeList1.Columns["BuildYear"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["BuildYear"].OptionsColumn.AllowSort = false;
            treeList1.Columns["BuildYear"].VisibleIndex = 3;
            treeList1.Columns["BuildEd"].Caption = "竣工年份";
            treeList1.Columns["BuildEd"].Width = 100;
            treeList1.Columns["BuildEd"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["BuildEd"].OptionsColumn.AllowSort = false;
            treeList1.Columns["BuildEd"].VisibleIndex = 4;

            CalcYearColumn();
            for (int i = 1; i <= 4; i++)
            {
                treeList1.Columns["Col" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["Col" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
            }
            treeList1.Columns["Sort"].VisibleIndex = -1;
            treeList1.Columns["ProjectID"].VisibleIndex = -1;
            treeList1.Columns["Sort"].SortOrder = SortOrder.Ascending;
            Application.DoEvents();
            SetValueNull();
            //treeList1.ExpandAll();
            treeList1.CollapseAll();
        }
        public void SetValueNull()
        {
            int[] year = GetYears();
            foreach (TreeListNode node in treeList1.Nodes)
            {
                if (node.GetValue("ParentID").ToString() == "0" && node.GetValue("Title").ToString() != "大型电源建设规划")
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
            string conn = "(ID='" + fuID + "' or ParentID='" + fuID + "' or Col3='"+fuID+"') and ProjectID='" + GetProjectID + "'";
            IList removeList = Common.Services.BaseService.GetList("SelectPs_PowerBuildByConn", conn);
            //CalcAddChild(ref removeList);
             IList cloneList = Common.Services.BaseService.GetList("SelectPs_PowerBuildByConn", conn);
             cloneList.Clear();
             for (int i = 0; i < listTypes.Count; i++)
             {
                 if (((Ps_PowerBuild)listTypes[i]).ParentID == fuID || ((Ps_PowerBuild)listTypes[i]).ID == fuID || ((Ps_PowerBuild)listTypes[i]).Col3 == fuID)
                 {
                     //listTypes.Remove(listTypes[i]);
                 }
                 else
                     cloneList.Add(listTypes[i]);
             }
             listTypes.Clear();
             listTypes = cloneList;
             if (removeList.Count > 0)
             {
                 CaleHeTable(ref removeList);
                 CalcXian(ref removeList);
                 // string con = "ID='" + fuID + "' and ProjectID='" + GetProjectID + "'";
                 //  AddRows(ref removeList, con);
                 for (int k = 0; k < removeList.Count; k++)
                 {
                     listTypes.Add((Ps_PowerBuild)removeList[k]);
                 }
             }
            //CalcTotal(ref listTypes);
            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_PowerBuild));
            treeList1.DataSource = dataTable;

            SetValueNull();
            //treeList1.ExpandAll();
        }

        public void LoadData2(string fuID)
        {
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                //treeList1.Columns.Clear();
            }
            string conn = "(ID='" + fuID + "' or ParentID='" + fuID + "' or Col3='" + fuID + "') and ProjectID='" + GetProjectID + "'";
            IList removeList = Common.Services.BaseService.GetList("SelectPs_PowerBuildByConn", conn);
            //CalcAddChild(ref removeList);
            IList cloneList = Common.Services.BaseService.GetList("SelectPs_PowerBuildByConn", conn);
            cloneList.Clear();
            for (int i = 0; i < listTypes.Count; i++)
            {
                if (((Ps_PowerBuild)listTypes[i]).ParentID == fuID || ((Ps_PowerBuild)listTypes[i]).ID == fuID || ((Ps_PowerBuild)listTypes[i]).Col3 == fuID)
                {
                    //listTypes.Remove(listTypes[i]);
                }
                else
                    cloneList.Add(listTypes[i]);
            }
            listTypes.Clear();
            listTypes = cloneList;
            CaleHeTable(ref removeList);
            CaleF(ref removeList);
            // string con = "ID='" + fuID + "' and ProjectID='" + GetProjectID + "'";
            //  AddRows(ref removeList, con);
            for (int k = 0; k < removeList.Count; k++)
            {
                listTypes.Add((Ps_PowerBuild)removeList[k]);
            }
            //CalcTotal(ref listTypes);
            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_PowerBuild));
            treeList1.DataSource = dataTable;

            SetValueNull();
            //treeList1.ExpandAll();
        }

        public void CaleF(ref IList list)
        {
            int he = 0;
            Ps_PowerBuild pb = new Ps_PowerBuild();
            for (int i = 0; i < list.Count; i++)
            {
                if (((Ps_PowerBuild)list[i]).Title == "大型电源建设规划")
                {
                    he = i;
                }
                else if (((Ps_PowerBuild)list[i]).Col1 == "child")
                {
                    for (int j = yAnge.StartYear; j <= yAnge.EndYear; j++)
                    {
                        pb.GetType().GetProperty("y" + j.ToString()).SetValue(pb, double.Parse(pb.GetType().GetProperty("y" + j.ToString()).GetValue(pb, null).ToString()) + double.Parse(((Ps_PowerBuild)list[i]).GetType().GetProperty("y" + j.ToString()).GetValue(((Ps_PowerBuild)list[i]), null).ToString()), null);
                    }
                }
            }
            for (int j = yAnge.BeginYear; j <= yAnge.EndYear; j++)
            {
                list[he].GetType().GetProperty("y" + j.ToString()).SetValue(list[he], double.Parse(pb.GetType().GetProperty("y" + j.ToString()).GetValue(pb, null).ToString()), null);
            }
            Common.Services.BaseService.Update<Ps_PowerBuild>((Ps_PowerBuild)list[he]);
        }

        public void CaleHeTable(ref IList heList)
        {
            for (int i = 0; i < heList.Count; i++)
            {
                if (((Ps_PowerBuild)heList[i]).Col1 == "child")
                {
                    string conn = "ParentID='" + ((Ps_PowerBuild)heList[i]).ID + "'";
                    IList tList = Common.Services.BaseService.GetList("SelectPs_Table_EditListByConn", conn);
                    for (int j = 0; j < tList.Count; j++)
                    {
                        if (((Ps_Table_Edit)tList[j]).Status == "扩建/改造")
                        {
                            for (int k = int.Parse(((Ps_Table_Edit)tList[j]).FinishYear); k <= yAnge.EndYear; k++)
                            {
                                double old = (double)((Ps_PowerBuild)heList[i]).GetType().GetProperty("y" + k.ToString()).GetValue(((Ps_PowerBuild)heList[i]), null);
                                ((Ps_PowerBuild)heList[i]).GetType().GetProperty("y" + k.ToString()).SetValue(((Ps_PowerBuild)heList[i]), double.Parse(((Ps_Table_Edit)tList[j]).Volume) + old, null);
                            }
                        }
                        else if (((Ps_Table_Edit)tList[j]).Status == "拆除")
                        {
                            for (int k = int.Parse(((Ps_Table_Edit)tList[j]).FinishYear); k <= yAnge.EndYear; k++)
                            {
                                ((Ps_PowerBuild)heList[i]).GetType().GetProperty("y" + k.ToString()).SetValue(((Ps_PowerBuild)heList[i]), 0.0, null);
                            }
                        }
                    }
                }
            }
        }

        public void CalcAddChild(ref IList list)
        {
            string con = "";
            for (int i = 0; i < list.Count; i++)
            {
                if (((Ps_PowerBuild)list[i]).Col1 == "yhe" || ((Ps_PowerBuild)list[i]).Col1 == "xhe")
                {
                    con = "ParentID='" + ((Ps_PowerBuild)list[i]).ID + "'";
                    IList cList = Common.Services.BaseService.GetList("SelectPs_PowerBuildByConn", con);
                    for (int z = 0; z < cList.Count; z++)
                        list.Add(cList[0]);
                }

            }
        }

        public void CalcXian(ref IList list)
        {


            int sx = 0, hx = 0, xx = 0, yx = 0, he = 0;
            Ps_PowerBuild sbuild = new Ps_PowerBuild();
            Ps_PowerBuild hbuild = new Ps_PowerBuild();
            Ps_PowerBuild ybuild = new Ps_PowerBuild();
            Ps_PowerBuild xbuild = new Ps_PowerBuild();
            //Ps_PowerBuild he = new Ps_PowerBuild();
            for (int i = 0; i < list.Count; i++)
            {
                if (((Ps_PowerBuild)list[i]).Col1 == "sc")
                {
                    sx = i;
                }
                else if (((Ps_PowerBuild)list[i]).Col1 == "hc")// && ((Ps_PowerBuild)list[i]).Type == "火电")
                {
                    hx = i;
                }
                else if (((Ps_PowerBuild)list[i]).Col1 == "yhe")
                {
                    yx = i;
                }
                else if (((Ps_PowerBuild)list[i]).Col1 == "xhe")
                {
                    xx = i;
                }
                else if (((Ps_PowerBuild)list[i]).Col1 == "che")
                {
                    he = i;
                }
                else if(((Ps_PowerBuild)list[i]).Col1=="child")
                {
                    for (int j = yAnge.BeginYear; j <= yAnge.EndYear; j++)
                    {
                        if (((Ps_PowerBuild)list[i]).Type == "水电")
                        {
                            sbuild.GetType().GetProperty("y" + j.ToString()).SetValue(sbuild, double.Parse(sbuild.GetType().GetProperty("y" + j.ToString()).GetValue(sbuild, null).ToString()) + double.Parse(((Ps_PowerBuild)list[i]).GetType().GetProperty("y" + j.ToString()).GetValue(((Ps_PowerBuild)list[i]), null).ToString()), null);
                        }
                        else if (((Ps_PowerBuild)list[i]).Type == "火电")
                        {
                            hbuild.GetType().GetProperty("y" + j.ToString()).SetValue(hbuild, double.Parse(hbuild.GetType().GetProperty("y" + j.ToString()).GetValue(hbuild, null).ToString()) + double.Parse(((Ps_PowerBuild)list[i]).GetType().GetProperty("y" + j.ToString()).GetValue(((Ps_PowerBuild)list[i]), null).ToString()), null);
                        }
                        if(((Ps_PowerBuild)list[i]).Col2 == "yhe")
                        {
                            ybuild.GetType().GetProperty("y" + j.ToString()).SetValue(ybuild, double.Parse(ybuild.GetType().GetProperty("y" + j.ToString()).GetValue(ybuild, null).ToString()) + double.Parse(((Ps_PowerBuild)list[i]).GetType().GetProperty("y" + j.ToString()).GetValue(((Ps_PowerBuild)list[i]), null).ToString()), null);
                        }
                        else if (((Ps_PowerBuild)list[i]).Col2 == "xhe")
                        {
                            xbuild.GetType().GetProperty("y" + j.ToString()).SetValue(xbuild, double.Parse(xbuild.GetType().GetProperty("y" + j.ToString()).GetValue(xbuild, null).ToString()) + double.Parse(((Ps_PowerBuild)list[i]).GetType().GetProperty("y" + j.ToString()).GetValue(((Ps_PowerBuild)list[i]), null).ToString()), null);
                        }
                    }
                }
            }
            //sbuild.ID = ((Ps_PowerBuild)list[sx]).ID;
            //sbuild.ParentID = ((Ps_PowerBuild)list[sx]).ParentID;
            //sbuild.ProjectID = ((Ps_PowerBuild)list[sx]).ProjectID;
            //sbuild.Sort = ((Ps_PowerBuild)list[sx]).Sort;
            //sbuild.Title = ((Ps_PowerBuild)list[sx]).Title;
            //sbuild.Type = ((Ps_PowerBuild)list[sx]).Type;
            //sbuild.Col1 = ((Ps_PowerBuild)list[sx]).Col1;
            //sbuild.Col2 = ((Ps_PowerBuild)list[sx]).Col2;
            //sbuild.BuildEd = ((Ps_PowerBuild)list[sx]).BuildEd;
            //sbuild.BuildFac = ((Ps_PowerBuild)list[sx]).BuildFac;
            //sbuild.BuildYear = ((Ps_PowerBuild)list[sx]).BuildYear;
            //hbuild.ID = ((Ps_PowerBuild)list[hx]).ID;
            //hbuild.ParentID = ((Ps_PowerBuild)list[hx]).ParentID;
            //hbuild.ProjectID = ((Ps_PowerBuild)list[hx]).ProjectID;
            //hbuild.Sort = ((Ps_PowerBuild)list[hx]).Sort;
            //hbuild.Title = ((Ps_PowerBuild)list[hx]).Title;
            //hbuild.Type = ((Ps_PowerBuild)list[hx]).Type;
            //hbuild.Col1 = ((Ps_PowerBuild)list[hx]).Col1;
            //hbuild.Col2 = ((Ps_PowerBuild)list[hx]).Col2;
            //hbuild.BuildEd = ((Ps_PowerBuild)list[hx]).BuildEd;
            //hbuild.BuildFac = ((Ps_PowerBuild)list[hx]).BuildFac;
            //hbuild.BuildYear = ((Ps_PowerBuild)list[hx]).BuildYear;

            //ybuild.ID = ((Ps_PowerBuild)list[yx]).ID;
            //ybuild.ParentID = ((Ps_PowerBuild)list[yx]).ParentID;
            //ybuild.ProjectID = ((Ps_PowerBuild)list[yx]).ProjectID;
            //ybuild.Sort = ((Ps_PowerBuild)list[yx]).Sort;
            //ybuild.Title = ((Ps_PowerBuild)list[yx]).Title;
            //ybuild.Type = ((Ps_PowerBuild)list[yx]).Type;
            //ybuild.Col1 = ((Ps_PowerBuild)list[yx]).Col1;
            //ybuild.Col2 = ((Ps_PowerBuild)list[yx]).Col2;
            //ybuild.BuildEd = ((Ps_PowerBuild)list[yx]).BuildEd;
            //ybuild.BuildFac = ((Ps_PowerBuild)list[yx]).BuildFac;
            //ybuild.BuildYear = ((Ps_PowerBuild)list[yx]).BuildYear;

            //xbuild.ID = ((Ps_PowerBuild)list[xx]).ID;
            //xbuild.ParentID = ((Ps_PowerBuild)list[xx]).ParentID;
            //xbuild.ProjectID = ((Ps_PowerBuild)list[xx]).ProjectID;
            //xbuild.Sort = ((Ps_PowerBuild)list[xx]).Sort;
            //xbuild.Title = ((Ps_PowerBuild)list[xx]).Title;
            //xbuild.Type = ((Ps_PowerBuild)list[xx]).Type;
            //xbuild.Col1 = ((Ps_PowerBuild)list[xx]).Col1;
            //xbuild.Col2 = ((Ps_PowerBuild)list[xx]).Col2;
            //xbuild.BuildEd = ((Ps_PowerBuild)list[xx]).BuildEd;
            //xbuild.BuildFac = ((Ps_PowerBuild)list[xx]).BuildFac;
            //xbuild.BuildYear = ((Ps_PowerBuild)list[xx]).BuildYear;
            //Common.Services.BaseService.Update<Ps_PowerBuild>(sbuild);
            //Common.Services.BaseService.Update<Ps_PowerBuild>(hbuild);
            //Common.Services.BaseService.Update<Ps_PowerBuild>(ybuild);
            //Common.Services.BaseService.Update<Ps_PowerBuild>(xbuild);

            for (int j = yAnge.BeginYear; j <= yAnge.EndYear; j++)
            {
                list[sx].GetType().GetProperty("y" + j.ToString()).SetValue(list[sx], double.Parse(sbuild.GetType().GetProperty("y" + j.ToString()).GetValue(sbuild, null).ToString()), null);
                list[hx].GetType().GetProperty("y" + j.ToString()).SetValue(list[hx], double.Parse(hbuild.GetType().GetProperty("y" + j.ToString()).GetValue(hbuild, null).ToString()), null);
                list[yx].GetType().GetProperty("y" + j.ToString()).SetValue(list[yx], double.Parse(ybuild.GetType().GetProperty("y" + j.ToString()).GetValue(ybuild, null).ToString()), null);
                list[xx].GetType().GetProperty("y" + j.ToString()).SetValue(list[xx], double.Parse(xbuild.GetType().GetProperty("y" + j.ToString()).GetValue(xbuild, null).ToString()), null);
                list[he].GetType().GetProperty("y" + j.ToString()).SetValue(list[he], double.Parse(list[sx].GetType().GetProperty("y" + j.ToString()).GetValue(list[sx], null).ToString()) + double.Parse(list[hx].GetType().GetProperty("y" + j.ToString()).GetValue(list[hx], null).ToString()), null);
            }
            Common.Services.BaseService.Update<Ps_PowerBuild>((Ps_PowerBuild)list[sx]);
            Common.Services.BaseService.Update<Ps_PowerBuild>((Ps_PowerBuild)list[hx]);
            Common.Services.BaseService.Update<Ps_PowerBuild>((Ps_PowerBuild)list[yx]);
            Common.Services.BaseService.Update<Ps_PowerBuild>((Ps_PowerBuild)list[xx]);
            Common.Services.BaseService.Update<Ps_PowerBuild>((Ps_PowerBuild)list[he]);
        }

        public void CalcYearColumn()
        {
            int[] year = GetYears();
            for (int i = year[0]; i < year[1]; i++)
            {
                treeList1.Columns["y" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["y" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
            }
            for (int i = year[2] + 1; i <= year[3]; i++)
            {
                treeList1.Columns["y" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["y" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
            }
            for (int i = year[1]; i <= year[2]; i++)
            {
                treeList1.Columns["y" + i.ToString()].Caption = i.ToString() + "年";
                treeList1.Columns["y" + i.ToString()].VisibleIndex = i;
                treeList1.Columns["y" + i.ToString()].Width = 60;
                treeList1.Columns["y" + i.ToString()].OptionsColumn.AllowEdit = false;
                treeList1.Columns["y" + i.ToString()].OptionsColumn.AllowSort = false;

            }
        }

        private string rongZai220 = "2.1";
        public string RongZai220
        {
            set { rongZai220 = value; }
        }

        public void AddRows(ref IList list)
        {
            //IList list = new List<Ps_PowerBuild>();
            string conn = "ParentID='0' and Title='地方电源建设规划' and ProjectID='" + GetProjectID + "'";

            int[] year = GetYears();
            IList pareList = Common.Services.BaseService.GetList("SelectPs_PowerBuildByConn", conn);
            if (pareList.Count == 0)
                return;
            try
            {
                conn = "ProjectID='" + GetProjectID + "' and Col1='sc'";
                IList list1 = Common.Services.BaseService.GetList("SelectPs_PowerBuildByConn", conn);
                conn = "ProjectID='" + GetProjectID + "' and Col1='hc'";
                IList list2 = Common.Services.BaseService.GetList("SelectPs_PowerBuildByConn", conn);
                conn = "ProjectID='" + GetProjectID + "' and Col1='xhe'";
                IList list3 = Common.Services.BaseService.GetList("SelectPs_PowerBuildByConn", conn);
                Ps_PowerBuild ps_tablehe = new Ps_PowerBuild();
                ps_tablehe.ID += "|" + GetProjectID;
                ps_tablehe.Title = "市合计";
                ps_tablehe.ParentID = ((Ps_PowerBuild)pareList[0]).ID;
                ps_tablehe.Sort = 1;
                ps_tablehe.Col1 = "dhe";
                Ps_PowerBuild table1 = new Ps_PowerBuild();
                table1.ID += "|" + GetProjectID;
                table1.Title = "年末装机容量";
                table1.Type = "水电";
                table1.ParentID = ps_tablehe.ID;
                table1.ProjectID = GetProjectID;
                table1.Col1 = "sc";
                table1.Col2 = "no";
                table1.Sort = 1;
                Ps_PowerBuild table2 = new Ps_PowerBuild();
                table2.ID += "|" + GetProjectID;
                table2.Title = "年末装机容量";
                table2.Type = "火电";
                table2.ParentID = ps_tablehe.ID;
                table2.ProjectID = GetProjectID;
                table2.Col1 = "hc";
                table2.Col2 = "no";
                table2.Sort = 2;
                Ps_PowerBuild table3 = new Ps_PowerBuild();
                table3.ID += "|" + GetProjectID;
                table3.Title = "在建及新建项目小计";
                table3.ParentID = ps_tablehe.ID;
                table3.ProjectID = GetProjectID;
                table3.Col1 = "xhe";
                table3.Col2 = "no";
                table3.Sort = 3;
                for (int i = yAnge.StartYear; i <= yAnge.FinishYear; i++)
                {
                    for (int j = 0; j < list1.Count; j++)
                    {
                        table1.GetType().GetProperty("y" + i.ToString()).SetValue(table1, double.Parse(table1.GetType().GetProperty("y" + i.ToString()).GetValue(table1, null).ToString()) + double.Parse(list1[j].GetType().GetProperty("y" + i.ToString()).GetValue(list1[j], null).ToString()), null);
                    }
                    for (int j = 0; j < list1.Count; j++)
                    {
                        table2.GetType().GetProperty("y" + i.ToString()).SetValue(table2, double.Parse(table2.GetType().GetProperty("y" + i.ToString()).GetValue(table2, null).ToString()) + double.Parse(list2[j].GetType().GetProperty("y" + i.ToString()).GetValue(list2[j], null).ToString()), null);
                    }
                    for (int j = 0; j < list1.Count; j++)
                    {
                        table3.GetType().GetProperty("y" + i.ToString()).SetValue(table3, double.Parse(table3.GetType().GetProperty("y" + i.ToString()).GetValue(table3, null).ToString()) + double.Parse(list3[j].GetType().GetProperty("y" + i.ToString()).GetValue(list3[j], null).ToString()), null);
                    }
                    ps_tablehe.GetType().GetProperty("y" + i.ToString()).SetValue(ps_tablehe, double.Parse(table1.GetType().GetProperty("y" + i.ToString()).GetValue(table1, null).ToString()) + double.Parse(table2.GetType().GetProperty("y" + i.ToString()).GetValue(table2, null).ToString()) + double.Parse(table3.GetType().GetProperty("y" + i.ToString()).GetValue(table3, null).ToString()), null);
                }
                list.Add(ps_tablehe);
                list.Add(table1);
                list.Add(table2);
                list.Add(table3);
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
            Ps_PowerBuild parent = new Ps_PowerBuild();
            parent.ID = parent.ID + "|" + GetProjectID;
            parent.ParentID = "0"; parent.Title = "全网110千伏合计"; parent.Sort = 1000;// OperTable.GetMaxSort() + 1;
            list.Add(parent);
            totoalParent = parent.ID;
            Ps_PowerBuild tablex = new Ps_PowerBuild();
            conn = "Col1='0' and ProjectID='" + GetProjectID + "'";
            IList childx = Common.Services.BaseService.GetList("SelectPs_PowerBuildSumByConn", conn);
            tablex = (Ps_PowerBuild)childx[0];
            tablex.ParentID = parent.ID;
            tablex.ID = Guid.NewGuid().ToString();
            tablex.ID += "|" + GetProjectID;
            tablex.Sort = 1;
            tablex.Col1 = "no";
            tablex.Title = "110千伏主变供电负荷";
            list.Add(tablex);

            Ps_PowerBuild table1 = new Ps_PowerBuild();
            // Ps_PowerBuild con1 = new Ps_PowerBuild();
            // con1.Col4 = rongZai220;
            conn = "Col1='0' and ProjectID='" + GetProjectID + "'";// con1.Title = "Col1='0'";
            //IList child1 = Common.Services.BaseService.GetList("SelectPs_PowerBuildSumByConn", conn);
            table1 = (Ps_PowerBuild)tablex.Clone();// (Ps_PowerBuild)child1[0];
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
            table1.Title = "110千伏需主变容量(" + rongZai220 + ")";
            list.Add(table1);

            Ps_PowerBuild table = new Ps_PowerBuild();
            //conn = "Col1='1'";
            //IList child = Common.Services.BaseService.GetList("SelectPs_PowerBuildSumByConn", conn);
            //table = (Ps_PowerBuild)child[0];
            for (int j = 0; j < list.Count; j++)
            {
                if (((Ps_PowerBuild)list[j]).Col1 == "1")
                {
                    for (int k = year[1]; k <= year[2]; k++)
                    {
                        table.GetType().GetProperty("y" + k).SetValue(table, double.Parse(table.GetType().GetProperty("y" + k).GetValue(table, null).ToString()) + double.Parse(((Ps_PowerBuild)list[j]).GetType().GetProperty("y" + k).GetValue(((Ps_PowerBuild)list[j]), null).ToString()), null);
                    }
                }
            }
            table.ParentID = parent.ID;
            table.ID = Guid.NewGuid().ToString();
            table.ID += "|" + GetProjectID;
            table.Sort = 3;
            table.Col1 = "no";
            table.Title = "110千伏主变总容量";
            list.Add(table);



            //IList allChild = Common.Services.BaseService.GetList("SelectPs_PowerBuildByConn", conn);
            for (int k = 0; k < list.Count; k++)
            {
                if (((Ps_PowerBuild)list[k]).Col1 == "1")
                {
                    Ps_PowerBuild ps1 = new Ps_PowerBuild();
                   
                    ps1 = (Ps_PowerBuild)((Ps_PowerBuild)list[k]).Clone();
                    ps1.ID = Guid.NewGuid().ToString();
                    ps1.ID += "|" + GetProjectID;
                    ps1.ParentID = parent.ID;
                    ps1.Col1 = "no";
                    ps1.BuildEd = "total";
                    list.Add(ps1);
                }
            }


            //conn = "Col1='0'";
            //Ps_PowerBuild temp1 = (Ps_PowerBuild)Common.Services.BaseService.GetList("SelectPs_PowerBuildByConn", conn)[0];
            Ps_PowerBuild table2 = new Ps_PowerBuild();
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
            table2.Title = "110千伏容载比";
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
            if (focusedNode.GetValue("Col1").ToString() != "xhe" && focusedNode.GetValue("Col1").ToString() != "yhe" && focusedNode.GetValue("Title").ToString() != "大型电源建设规划")
            {
                MessageBox.Show("不能在此分类下添加变电站");
                return;
            }

            Ps_YearRange range = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.power + "'");
            Ps_PowerBuild table = new Ps_PowerBuild();
            table.ID += "|" + GetProjectID;
            FrmPowerNew frm = new FrmPowerNew();
            frm.GetProject = GetProjectID;
            frm.ParentID = table.ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                table.Title = frm.GetTitle;
                //if (focusedNode.GetValue("ParentID").ToString() == "0")
                //{
                //    table.ParentID = focusedNode.GetValue("ID").ToString();
                //}
                //else
                    table.ParentID = focusedNode.GetValue("ID").ToString();
                table.Type = frm.PowerType;
                table.BuildFac = frm.GetVolumn;
                table.BuildYear = frm.GetYear1;
                table.BuildEd = frm.GetYear2;
                table.Col1 = "child";
                table.Col2 = focusedNode.GetValue("Col1").ToString();
                if(focusedNode.ParentNode!=null)
                    table.Col3 = focusedNode.GetValue("ParentID").ToString();
                table.ProjectID = GetProjectID;
                table.Sort = OperTable.GetPowerBuildMaxSort() + 1;
                if (table.Sort < 4)
                    table.Sort = 4;
                int sYear = int.Parse(frm.GetYear2);
                for (int i = sYear; i <= range.EndYear; i++)
                {
                    table.GetType().GetProperty("y" + i.ToString()).SetValue(table, double.Parse(frm.GetVolumn), null);
                }
                try
                {
                    Common.Services.BaseService.Create("InsertPs_PowerBuild", table);
                    //dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(table, dataTable.NewRow()));
                    if (focusedNode.ParentNode != null)
                        LoadData1(focusedNode.GetValue("ParentID").ToString());
                    else
                        LoadData2(table.ParentID);

                    FoucsLocation(table.ID, treeList1.Nodes);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加项目出错：" + ex.Message);
                }
            }
          
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

            Ps_YearRange range = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.power + "'");
            if (focusedNode.GetValue("Col1") != null && focusedNode.GetValue("Col1").ToString() == "0")
            {
                FrmChangeBian frm = new FrmChangeBian();
                frm.GetProject = GetProjectID;
                frm.Mark = OperTable.power;
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
                    Ps_PowerBuild table = new Ps_PowerBuild();
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
                        Common.Services.BaseService.Update("UpdatePs_PowerBuild", table);
                        LoadData1(table.ParentID);
                        FoucsLocation(table.ID, treeList1.Nodes);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("修改项目出错：" + ex.Message);
                    }
                }
            }
            else if (focusedNode.GetValue("Col1") != null && focusedNode.GetValue("Col1").ToString() == "child")
            {
                string conn = "ParentID='" + focusedNode.GetValue("ID").ToString() + "'";
                IList<Ps_Table_Edit> eList = Common.Services.BaseService.GetList<Ps_Table_Edit>("SelectPs_Table_EditListByConn", conn);

                FrmPsEdit frm = new FrmPsEdit();
                frm.GetProject = GetProjectID;
                frm.Mark = OperTable.power;
                frm.GridData = eList;
                frm.Title = focusedNode.GetValue("Title").ToString();
                frm.ParentID = focusedNode.GetValue("ID").ToString();
                string curVolumn = focusedNode.GetValue("y" + range.EndYear).ToString();
                frm.CurVolumn = curVolumn;
                frm.TypeTable = "power";
                frm.MaxYear = GetChildMaxYear(conn);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Ps_PowerBuild table = new Ps_PowerBuild();
                    eList = Common.Services.BaseService.GetList<Ps_Table_Edit>("SelectPs_Table_EditListByConn1", conn);
                    table = Common.Services.BaseService.GetOneByKey<Ps_PowerBuild>(focusedNode.GetValue("ID"));
                    //table.ID = focusedNode.GetValue("ID").ToString();
                    table.Title = frm.Title;
                    if (eList.Count > 0)
                    {
                        table.BuildFac = eList[0].Volume;
                        table.BuildYear = eList[0].StartYear;
                        table.BuildEd = eList[0].FinishYear;
                    }
                    try
                    {
                        Common.Services.BaseService.Update("UpdatePs_PowerBuild", table);
                        if (focusedNode.ParentNode.GetValue("Title").ToString() == "大型电源建设规划")
                            LoadData2(table.ParentID);
                        else
                            LoadData1(focusedNode.ParentNode.GetValue("ParentID").ToString());
                        FoucsLocation(table.ID, treeList1.Nodes);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("修改项目出错：" + ex.Message);
                    }
                }
            }
            else if (focusedNode.GetValue("Col1").ToString() == "che")
            {
                FrmAddPN frm = new FrmAddPN();
                frm.ParentName = focusedNode.GetValue("Title").ToString();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Ps_PowerBuild table1 = new Ps_PowerBuild();
                    table1.ID = focusedNode.GetValue("ID").ToString();
                    Ps_PowerBuild te = Common.Services.BaseService.GetOneByKey<Ps_PowerBuild>(table1.ID);
                    table1 = te;
                    table1.ParentID = focusedNode.GetValue("ParentID").ToString();
                    table1.Sort = int.Parse(focusedNode.GetValue("Sort").ToString());
                    table1.Title = frm.ParentName;
                    table1.ProjectID = GetProjectID;
                    try
                    {
                        Common.Services.BaseService.Update("UpdatePs_PowerBuild", table1);
                        LoadData2(table1.ID);
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
            if (treeList1.FocusedNode.GetValue("Col1").ToString() == "child" )
            {
                if (MsgBox.ShowYesNo("是否删除 " + treeList1.FocusedNode.GetValue("Title") + "？") == DialogResult.Yes)
                {
                    try
                    {
                        Ps_PowerBuild ny = new Ps_PowerBuild();
                        ny.ID = treeList1.FocusedNode.GetValue("ID").ToString();
                        string pare = treeList1.FocusedNode.ParentNode.GetValue("Title").ToString();
                        Common.Services.BaseService.Delete(ny);
                      //  DelAll(ny.ID);
                        if (pare != "大型电源建设规划")
                            LoadData1(treeList1.FocusedNode.ParentNode.GetValue("ParentID").ToString());// 1(ny.ParentID);
                        else
                            LoadData2(ny.ParentID);
                    }
                    catch { }
                }
            }
            else if (treeList1.FocusedNode.GetValue("Col1").ToString() == "che")
            {
                if (MsgBox.ShowYesNo("是否删除 " + treeList1.FocusedNode.GetValue("Title") + "？") == DialogResult.Yes)
                {
                    try
                    {
                        Ps_PowerBuild ny = new Ps_PowerBuild();
                        ny.ID = treeList1.FocusedNode.GetValue("ID").ToString();
                        string pare = treeList1.FocusedNode.GetValue("Title").ToString();
                        Common.Services.BaseService.Delete(ny);
                        DelAll(ny.ID);
                       LoadData1(treeList1.FocusedNode.GetValue("ID").ToString());// 1(ny.ParentID);
                       // treeList1.FocusedNode.
                    }
                    catch { }
                }
            }
            else
                MessageBox.Show("不能删除此行！");
        }
        //删除所有
        public void DelAll(string suid)
        {
            string conn = "ParentId='" + suid + "'";
            IList<Ps_PowerBuild> list = Common.Services.BaseService.GetList<Ps_PowerBuild>("SelectPs_PowerBuildByConn", conn);
            if (list.Count > 0)
            {
                foreach (Ps_PowerBuild var in list)
                {
                    string child = var.ID;
                    DelAll(child);
                    Ps_PowerBuild ny = new Ps_PowerBuild();
                    ny.ID = child;
                    Common.Services.BaseService.Delete(ny);
                }
            }
        }
        //设置容载比
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormPSP_VolumeBalanceVolumecalc calc = new FormPSP_VolumeBalanceVolumecalc();
            calc.SetSpanText = 1.9;
            if (calc.ShowDialog() == DialogResult.OK)
            {
                RongZai220 = calc.SetSpanText.ToString();
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
            Ps_YearRange range = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.power + "'");
            FormChooseYears frm = new FormChooseYears();
            for (int i = range.StartYear; i <= range.FinishYear; i++)
            {
                frm.ListYearsForChoose.Add(i);

            }
            frm.NoIncreaseRate = true;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                string con = "ProjectID='" + GetProjectID + "' ORDER BY Sort";
                listTypes = Common.Services.BaseService.GetList("SelectPs_PowerBuildByConn", con);
                //AddTotalRow(ref listTypes);
                AddRows(ref listTypes);
                DataTable dt = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_PowerBuild));


                FrmPowerPrint frma = new FrmPowerPrint();
                frma.IsSelect = _isSelect;
                frma.Text = "电源建设规划表";
                frma.IsBand = false;
               // frma.BHe = false;
                frma.SetGridWidth(70, "序号", 250);
                treeList1.DataSource = dt;
                frma.GridDataTable = ResultDataTable(ConvertTreeListToDataTable(treeList1, false), frm.ListChoosedYears);

                listTypes = Common.Services.BaseService.GetList("SelectPs_PowerBuildByConn", con);
                DataTable dt1 = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_PowerBuild));
                treeList1.DataSource = dt1;
                SetValueNull();
                //treeList1.ExpandAll();

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
            listColID.Add("BuildFac");
            dt.Columns.Add("BuildFac", typeof(string));
            listColID.Add("BuildYear");
            dt.Columns.Add("BuildYear", typeof(string));
            listColID.Add("Type");
            dt.Columns.Add("Type", typeof(string));
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
            listColID.Add("Title1");
            dt.Columns.Add("Title1", typeof(string));
            dt.Columns["Title1"].Caption = "分类1";
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
            dtReturn.Columns.Add("Title1", typeof(string));
            dtReturn.Columns.Add("Type", typeof(string));
            dtReturn.Columns.Add("BuildFac", typeof(string));
            dtReturn.Columns.Add("BuildYear", typeof(string));
            dtReturn.Columns.Add("BuildEd", typeof(string));
            foreach (ChoosedYears choosedYear in listChoosedYears)
            {
                dtReturn.Columns.Add(choosedYear.Year + "年", typeof(double));
            }
            string[] que = new string[60] { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", 
            "十一","十二","十三","十四","十五","十六","十七","十八","十九","二十","二十一","二十二","二十三","二十四","二十五","二十六","二十七",
            "二十八","二十九","三十","三十一","三十二","三十三","三十四","三十五","三十六","三十七","三十八","三十九","四十","四十一","四十二","四十三","四十四",
            "四十五","四十六","四十七","四十八","四十九","五十","五十一","五十二","五十三","五十四","五十五","五十六","五十七","五十八","五十九","六十"};
            int nRowMaxLoad = 0;//地区最高负荷所在行
            int nRowMaxPower = 0;//地区电网供电能力所在行
            int nRowMaxPowerLow = 0;//枯水期地区电网供电能力所在行
            int q=0,k=1,z=0;
            #region 填充数据
            for (int i = 0; i < sourceDataTable.Rows.Count; i++)
            {

                if (sourceDataTable.Rows[i]["Col1"].ToString() == "yhe" || sourceDataTable.Rows[i]["Col2"].ToString() == "yhe")
                    continue;
                DataRow newRow = dtReturn.NewRow();
                DataRow sourceRow = sourceDataTable.Rows[i];
                foreach (DataColumn column in dtReturn.Columns)
                {
                    if (column.ColumnName == "Title")
                    {
                        if (sourceRow["ParentID"].ToString() == "0")
                        {
                            newRow[column.ColumnName] = que[q];
                            q++;
                        }
                        else if (sourceRow["Col1"].ToString() == "che")
                        {
                            newRow[column.ColumnName] = k.ToString()+")";
                            k++;
                        }
                        else if (sourceRow["Col1"].ToString() == "dhe")
                        {
                            newRow[column.ColumnName] = "1";
                           // k++;
                        }
                        else
                        {
                            newRow[column.ColumnName] = "";
                        }
                    }
                    else if (column.ColumnName == "Title1")
                    {
                        newRow[column.ColumnName] = sourceRow["Title"];
                    }
                    else
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
            if (focusedNode == null)
            {
                return;
            }
            //if (focusedNode == null)
            //{
            //    return;
            //}
            if (focusedNode.GetValue("Title").ToString() != "地方电源建设规划")
            {
                MessageBox.Show("只能在“地方电源建设规划”分类下添加城区！");
                return;
            }

            FrmAddPN frm = new FrmAddPN();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_PowerBuild table_yd = new Ps_PowerBuild();
                table_yd.ID = table_yd.ID + "|" + GetProjectID;
                table_yd.Title = frm.ParentName;
                table_yd.ParentID = focusedNode.GetValue("ID").ToString();
                table_yd.Sort = OperTable.GetPowerBuildMaxSort() + 1;
                table_yd.ProjectID = GetProjectID;
                table_yd.Col1 = "che";
                table_yd.Col2 = "no";
                try
                {
                    Common.Services.BaseService.Create("InsertPs_PowerBuild", table_yd);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加分类出错：" + ex.Message);
                }

                Ps_PowerBuild table1 = new Ps_PowerBuild();
                table1.ID += "|" + GetProjectID;
                table1.Title = "年末装机容量";
                table1.Type = "水电";
                table1.ParentID = table_yd.ID;
                table1.ProjectID = GetProjectID;
                table1.Col1 = "sc";
                table1.Col2 = "no";
                table1.Sort = 1;
                Ps_PowerBuild table2 = new Ps_PowerBuild();
                table2.ID += "|" + GetProjectID;
                table2.Title = "年末装机容量";
                table2.Type = "火电";
                table2.ParentID = table_yd.ID;
                table2.ProjectID = GetProjectID;
                table2.Col1 = "hc";
                table2.Col2 = "no";
                table2.Sort = 2;
                Ps_PowerBuild table3 = new Ps_PowerBuild();
                table3.ID += "|" + GetProjectID;
                table3.Title = "已建项目小计";
                table3.ParentID = table_yd.ID;
                table3.ProjectID = GetProjectID;
                table3.Col1 = "yhe";
                table3.Col2 = "no";
                table3.Sort = 3;
                Ps_PowerBuild table4 = new Ps_PowerBuild();
                table4.ID += "|" + GetProjectID;
                table4.Title = "在建及新建项目小计";
                table4.ParentID = table_yd.ID;
                table4.ProjectID = GetProjectID;
                table4.Col1 = "xhe";
                table4.Col2 = "no";
                table4.Sort = 4;
                try
                {
                    Common.Services.BaseService.Create("InsertPs_PowerBuild", table1);
                    Common.Services.BaseService.Create("InsertPs_PowerBuild", table2);
                    Common.Services.BaseService.Create("InsertPs_PowerBuild", table3);
                    Common.Services.BaseService.Create("InsertPs_PowerBuild", table4);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加项目出错：" + ex.Message);
                }
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
                frma.Text = "110千伏分区分年容载比计算表";
                frma.GridDataTable = ResultDataTable(ConvertTreeListToDataTable(treeList1, true), frm.ListChoosedYears);
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
            frm.ClassName = "Ps_PowerBuild";
            frm.SelectString = "SelectPs_PowerBuildByConn";
            frm.InsertString = "InsertPs_PowerBuild";
            frm.ExistChild = true;
            frm.ChildClassName.Add("Ps_Table_Edit");
            frm.ChildInsertName.Add("InsertPs_Table_Edit");
            frm.ChildSelectString.Add("SelectPs_Table_EditListByConn");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("导入成功");
                LoadData();
            }
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("该操作将清除当前项目的所有数据，清除数据以后无法恢复,可能对其他用户的数据产生影响，请谨慎操作，你确定继续吗？", "删除", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string conn="ProjectID='"+GetProjectID+"'";
                Common.Services.BaseService.Update("DeletePs_PowerBuildByConn", conn);
              //  Common.Services.BaseService.Update("DeletePs_Table_EditByConn", conn);
                LoadData();
            }
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string conn = "ParentID='0' and ProjectID='"+GetProjectID+"' and (Title='大型电源建设规划' or Title='地方电源建设规划')";
            IList list = Common.Services.BaseService.GetList("SelectPs_PowerBuildByConn", conn);
            if (list.Count > 0)
            {
                MessageBox.Show("已经有固定列！");
                return;
            }
            Ps_PowerBuild table1 = new Ps_PowerBuild();
            table1.ID += "|" + GetProjectID;
            table1.Title = "大型电源建设规划";
            table1.ParentID = "0";
            table1.ProjectID = GetProjectID;
            table1.Sort = 1;
            Ps_PowerBuild table2 = new Ps_PowerBuild();
            table2.ID += "|" + GetProjectID;
            table2.Title = "地方电源建设规划";
            table2.ParentID = "0";
            table2.ProjectID = GetProjectID;
            table2.Sort = 2;
            Common.Services.BaseService.Create<Ps_PowerBuild>(table1);
            Common.Services.BaseService.Create<Ps_PowerBuild>(table2);
            LoadData();
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormYearSet fys = new FormYearSet();
            fys.TYPE = OperTable.power;
            fys.PID = ProjectUID;
            if (fys.ShowDialog() != DialogResult.OK)
                return;
            yAnge = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.power + "'");
            LoadData();
        }
    }
}