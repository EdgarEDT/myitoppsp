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
using Itop.Domain.Table;
using Itop.Client.Chen;

namespace Itop.Client.Table
{
    public partial class Frmps_Ydtable : FormBase
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

        public Frmps_Ydtable()
        {
            InitializeComponent();
        }

        private void HideToolBarButton()
        {
            if (!base.AddRight)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                //barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!base.EditRight)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!base.DeleteRight)
            {
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                //barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        public string GetProjectID
        {
            get { return ProjectUID; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HideToolBarButton();
            yAnge = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.elec + "'");
            Show();
            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            LoadData();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
        }

        private void LoadData()
        {
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
            }

            LoadValues();
            //treeList1.Columns["Suid"].VisibleIndex = -1;
            //treeList1.Columns["ParentId"].VisibleIndex = -1;
            treeList1.Columns["SortId"].VisibleIndex = -1;
            treeList1.Columns["Col1"].VisibleIndex = -1;
            treeList1.Columns["Col2"].VisibleIndex = -1;
            treeList1.Columns["Col3"].VisibleIndex = -1;
            treeList1.Columns["Col4"].VisibleIndex = -1;
            treeList1.Columns["Col5"].VisibleIndex = -1;
            treeList1.Columns["CompName"].Caption = "企业(项目)名称";
            treeList1.Columns["CompName"].Width = 180;
            treeList1.Columns["CompName"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["CompName"].OptionsColumn.AllowSort = false;
            treeList1.Columns["BuildSize"].Caption = "建设规模及内容";
            treeList1.Columns["BuildSize"].Width = 350;
            treeList1.Columns["BuildSize"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["BuildSize"].OptionsColumn.AllowSort = false;
            treeList1.Columns["PlanYear"].Caption = "计划建设年限";
            treeList1.Columns["PlanYear"].Width = 100;
            treeList1.Columns["PlanYear"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["PlanYear"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Progress"].Caption = "工作进展";
            treeList1.Columns["Progress"].Width = 120;
            treeList1.Columns["Progress"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Progress"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Power"].Caption = "用电量/亿千瓦时";
            treeList1.Columns["Power"].Width = 100;
            treeList1.Columns["Power"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Power"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Weight"].Caption = "负荷/万千瓦";
            treeList1.Columns["Weight"].Width = 100;
            treeList1.Columns["Weight"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Weight"].OptionsColumn.AllowSort = false;
            
            Application.DoEvents();


            //treeList1.ExpandAll();
            treeList1.CollapseAll();
        }

        //读取数据
        private void LoadValues()
        {
            IList tList = Common.Services.BaseService.GetList("SelectPs_Table_YdList", "");
            dataTable = Itop.Common.DataConverter.ToDataTable(tList, typeof(Ps_Table_Yd));
            //dataTable = dc.GetSortTable(dataTable, "Flag", true);
            treeList1.DataSource = dataTable;
        }
        

        //添加子分类
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

            //string nodestr = (treenode.GetValue("SortId") == null) ? "" : treenode.GetValue("SortId").ToString();
            if (focusedNode.GetValue("SortId") != null && focusedNode.GetValue("SortId").ToString() == "1")
            {
                MsgBox.Show(focusedNode.GetValue("CompName").ToString() + "不允许添加子分类！");
                return;
            }
            FrmAddPN frm = new FrmAddPN();
            frm.SetFrmName = "增加" + focusedNode.GetValue("CompName").ToString() + "的子分类";
            frm.SetLabelName = "子分类名称：";
            if(frm.ShowDialog() == DialogResult.OK)
            {
                Ps_Table_Yd table_yd = new Ps_Table_Yd();
                table_yd.CompName = frm.ParentName;
                table_yd.ParentId = focusedNode.GetValue("Suid").ToString();
                table_yd.Col1 = DateTime.Now.ToString();
                try
                {
                    Common.Services.BaseService.Create("InsertPs_Table_Yd", table_yd);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(table_yd, dataTable.NewRow()));
                }
                catch(Exception ex)
                {
                    MsgBox.Show("增加子分类出错：" + ex.Message);
                }
            }
        }
        //修改
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }
            if (!base.EditRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }
            if (treeList1.FocusedNode.GetValue("SortId") != null && treeList1.FocusedNode.GetValue("SortId").ToString() == "1")
            {
                FrmAddXM frm = new FrmAddXM();
                frm.SetFrmName = "修改" + treeList1.FocusedNode.GetValue("CompName").ToString() + "的项目名";
                frm.Comp = treeList1.FocusedNode.GetValue("CompName").ToString();
                frm.Build = treeList1.FocusedNode.GetValue("BuildSize").ToString();
                frm.Progre = treeList1.FocusedNode.GetValue("Progress").ToString();
                frm.Plan = treeList1.FocusedNode.GetValue("PlanYear").ToString();
                frm.Pow = treeList1.FocusedNode.GetValue("Power").ToString();
                frm.Weig = treeList1.FocusedNode.GetValue("Weight").ToString();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Ps_Table_Yd table_dy = new Ps_Table_Yd();
                    Itop.Client.Chen.Class1.TreeNodeToDataObject<Ps_Table_Yd>(table_dy, treeList1.FocusedNode);
                    table_dy.CompName = frm.Comp;
                    table_dy.BuildSize = frm.Build;
                    table_dy.Progress = frm.Progre;
                    table_dy.PlanYear = frm.Plan;
                    table_dy.Power = frm.Pow;
                    table_dy.Weight = frm.Weig;
                    try
                    {
                        Common.Services.BaseService.Update<Ps_Table_Yd>(table_dy);
                        treeList1.FocusedNode.SetValue("CompName", frm.Comp);
                        LoadData();
                    }
                    catch { }
                }
            }
            else
            {
                FrmAddPN frm = new FrmAddPN();
                frm.SetFrmName = "修改" + treeList1.FocusedNode.GetValue("CompName").ToString() + "的分类名";
                frm.ParentName = treeList1.FocusedNode.GetValue("CompName").ToString();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Ps_Table_Yd table_dy = new Ps_Table_Yd();
                    Itop.Client.Chen.Class1.TreeNodeToDataObject<Ps_Table_Yd>(table_dy, treeList1.FocusedNode);
                    table_dy.CompName = frm.ParentName;

                    try
                    {
                        Common.Services.BaseService.Update<Ps_Table_Yd>(table_dy);
                        treeList1.FocusedNode.SetValue("CompName", frm.ParentName);
                        LoadData();
                    }
                    catch { }
                }
            }
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
            if (treeList1.FocusedNode.GetValue("SortId").ToString() == "1")
            {
                if (MessageBox.Show("确定要删除项目？", "删除项目", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Ps_Table_Yd ny = new Ps_Table_Yd();
                    ny.Suid = treeList1.FocusedNode.GetValue("Suid").ToString();
                    Common.Services.BaseService.Delete(ny);
                }
            }
            else
            {
                if (MessageBox.Show("这样会把该分类下面的项目一起删掉，确定要删除分类？", "删除分类", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DelAll(treeList1.FocusedNode.GetValue("Suid").ToString());
                }
            }
            LoadData();
        }
        //删除所有
        public void DelAll(string suid)
        {
            string conn = "ParentId="+suid;
            IList<Ps_Table_Yd> list = Common.Services.BaseService.GetList<Ps_Table_Yd>("SelectPs_Table_YdListByConn", conn);
            if (list != null)
            {
                foreach (Ps_Table_Yd var in list)
                {
                    string child = var.Suid;
                    if (var.SortId != "1")
                        DelAll(child);
                    Ps_Table_Yd ny=new Ps_Table_Yd();
                    ny.Suid=child;
                    Common.Services.BaseService.Delete(ny);
                }
            }
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
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
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_Table_Yd table_yd = new Ps_Table_Yd();
                table_yd.CompName = frm.ParentName;
                table_yd.ParentId = "0";
                table_yd.Col1 = DateTime.Now.ToString();
                try
                {
                    Common.Services.BaseService.Create("InsertPs_Table_Yd", table_yd);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(table_yd, dataTable.NewRow()));

                    this.Cursor = Cursors.WaitCursor;
                    treeList1.BeginUpdate();
                    LoadData();
                    treeList1.EndUpdate();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加分类出错：" + ex.Message);
                }
            }
        }

        
        //添加项目
        private void barButtonItem4_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
            FrmAddXM frm = new FrmAddXM();
            frm.SetFrmName = "添加" + focusedNode.GetValue("CompName").ToString() + "的子项目";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_Table_Yd table_yd = new Ps_Table_Yd();
                table_yd.CompName = frm.Comp;
                table_yd.ParentId = focusedNode.GetValue("Suid").ToString();
                table_yd.PlanYear = frm.Plan;
                table_yd.Power = frm.Pow;
                table_yd.Progress = frm.Progre;
                table_yd.SortId = "1";
                table_yd.Weight = frm.Weig;
                table_yd.BuildSize = frm.Build;
                table_yd.Col1 = DateTime.Now.ToString();
                try
                {
                    Common.Services.BaseService.Create("InsertPs_Table_Yd", table_yd);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(table_yd, dataTable.NewRow()));

                    this.Cursor = Cursors.WaitCursor;
                    treeList1.BeginUpdate();
                    LoadData();
                    treeList1.EndUpdate();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加项目出错：" + ex.Message);
                }

            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmTotal frm = new FrmTotal();
            frm.ShowDialog();
        }

    }
}