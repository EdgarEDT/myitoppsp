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
using Itop.Domain.Stutistic;
using Itop.Client.Forecast;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using System.Drawing.Drawing2D;
namespace Itop.Client.Table
{
    public partial class FrmTableWGAH : FormBase
    {
        DataTable dataTable;
        int year = DateTime.Now.Year;
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

        public FrmTableWGAH()
        {
            InitializeComponent();
        }

        public int[] GetYears()
        {
            Ps_YearRange yr = yAnge;
            int[] year = new int[4] { yr.BeginYear, yr.StartYear, yr.FinishYear, yr.EndYear };
            return year;
        }
        //根据授权显示哪些菜单
        private void HideToolBarButton()
        {
            
            if (!base.AddRight)
            {
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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
            //取得要计算值的起始年份及开始和结束年份
            yAnge = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.BbWg + "'");
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
            IList pList = Common.Services.BaseService.GetList("SelectPs_Table_WGListByConn", conn);
            string con = "ProjectID='" + GetProjectID + "'";
            //ParentID!='-1'
            listTypes = Common.Services.BaseService.GetList("SelectPs_Table_WGListByConn", con);
            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Table_WG));

           
            treeList1.DataSource = dataTable;

            treeList1.Columns["Title"].Caption = "分区名称";
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
                        node.SetValue("y" + i.ToString(), null);
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
                treeList1.Columns["y" + i.ToString()].Tag = i;
                treeList1.Columns["y" + i.ToString()].ColumnEdit = repositoryItemTextEdit1;
                treeList1.Columns["y" + i.ToString()].Format.FormatString = "#####################0.##";
                treeList1.Columns["y" + i.ToString()].Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            }
            for (int i = year[2] + 1; i <= year[3]; i++)
            {
                treeList1.Columns["y" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["y" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
                treeList1.Columns["y" + i.ToString()].Tag = i;
                treeList1.Columns["y" + i.ToString()].ColumnEdit = repositoryItemTextEdit1;

                treeList1.Columns["y" + i.ToString()].Format.FormatString = "#####################0.##";
                treeList1.Columns["y" + i.ToString()].Format.FormatType = DevExpress.Utils.FormatType.Numeric;

            }
            for (int i = year[1]; i <= year[2]; i++)
            {
                treeList1.Columns["y" + i.ToString()].Caption = i.ToString() + "年";
                treeList1.Columns["y" + i.ToString()].VisibleIndex = i;
                treeList1.Columns["y" + i.ToString()].Width = 60;
                //treeList1.Columns["y" + i.ToString()].OptionsColumn.AllowEdit = false;
                treeList1.Columns["y" + i.ToString()].OptionsColumn.AllowSort = false;
                treeList1.Columns["y" + i.ToString()].Tag = i;
                treeList1.Columns["y" + i.ToString()].ColumnEdit = repositoryItemTextEdit1;

                treeList1.Columns["y" + i.ToString()].Format.FormatString = "#####################0.##";
                treeList1.Columns["y" + i.ToString()].Format.FormatType = DevExpress.Utils.FormatType.Numeric;


            }

          
        }

        private string rongZai110 = "1.5";
        public string RongZai110
        {
            set { rongZai110 = value; }
            get { return rongZai110; }
        }
        public string RongZai(Ps_Table_WG cur)
        {
            if (cur == null || cur.BuildYear == null || cur.BuildYear == "")
                return rongZai110;
            return cur.BuildYear;
        }
        private string totoalParent;
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
            column.Format.FormatString = "n2";
            column.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            //column.OptionsColumn.AllowEdit = true;
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
        //修改
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditPsTable();
        }
        //数据联动修改
        private void DataChange(string Sort,string ID)
        {
           
            Ps_YearRange range = yAnge;
            #region (将分区内所有数据重新计算)
            IList zfqlist = Common.Services.BaseService.GetList("SelectPs_Table_WGListByConn", " ProjectId='" + GetProjectID + "' and ParentID='" + ID + "'");
            
            //修改地区无功电源合计
             IList dqwg4list = Common.Services.BaseService.GetList("SelectPs_Table_WGListByConn", " ProjectId='" + GetProjectID + "' and ParentID='" + zfqlist[3].GetType().GetProperty("ID").GetValue(zfqlist[3], null).ToString() + "' ");

            IList dqwg41list = Common.Services.BaseService.GetList("SelectPs_Table_WGListByConn", " ProjectId='" + GetProjectID + "' and ParentID='" + dqwg4list[0].GetType().GetProperty("ID").GetValue(dqwg4list[0], null).ToString() + "' ");
            IList dqwg42list = Common.Services.BaseService.GetList("SelectPs_Table_WGListByConn", " ProjectId='" + GetProjectID + "' and ParentID='" + dqwg4list[1].GetType().GetProperty("ID").GetValue(dqwg4list[1], null).ToString() + "' ");
            
            for (int i = range.BeginYear; i <= range.EndYear; i++)
            {
                double tempdb1 = double.Parse(zfqlist[0].GetType().GetProperty("y" + i.ToString()).GetValue(zfqlist[0], null).ToString());
                //修改地区最大自然无功负荷(Ql=1.3*Pg)
                double tempdb2 = tempdb1 * 1.3;
                zfqlist[1].GetType().GetProperty("y" + i.ToString()).SetValue(zfqlist[1], Math.Round(tempdb2, 2), null);
                //修改地区无功电源需安装容量(1.15Ql)
                double tempdb3 = tempdb2 * 1.15;
                zfqlist[2].GetType().GetProperty("y" + i.ToString()).SetValue(zfqlist[2], Math.Round(tempdb3, 2), null);
                
                //修改地区发电设备无功出力
                double tempdb41=0;
                for (int j = 0; j < dqwg41list.Count; j++)
                {
                    tempdb41 += double.Parse(dqwg41list[j].GetType().GetProperty("y" + i.ToString()).GetValue(dqwg41list[j], null).ToString());
                }
                dqwg4list[0].GetType().GetProperty("y" + i.ToString()).SetValue(dqwg4list[0], Math.Round(tempdb41, 2), null);

                //修改地区已有无功补偿容量
                double tempdb42 = 0;
                for (int j = 0; j < dqwg42list.Count; j++)
                {
                    tempdb42 += double.Parse(dqwg42list[j].GetType().GetProperty("y" + i.ToString()).GetValue(dqwg42list[j], null).ToString());
                }
                dqwg4list[1].GetType().GetProperty("y" + i.ToString()).SetValue(dqwg4list[1], Math.Round(tempdb42, 2), null);
                double tempdb43 = double.Parse(dqwg4list[2].GetType().GetProperty("y" + i.ToString()).GetValue(dqwg4list[2], null).ToString());
                double tempdb44 = double.Parse(dqwg4list[3].GetType().GetProperty("y" + i.ToString()).GetValue(dqwg4list[3], null).ToString());
                double tempdb45 = double.Parse(dqwg4list[4].GetType().GetProperty("y" + i.ToString()).GetValue(dqwg4list[4], null).ToString());
                double tempdb4 = tempdb41 + tempdb42 + tempdb43 + tempdb44 + tempdb45;
                zfqlist[3].GetType().GetProperty("y" + i.ToString()).SetValue(zfqlist[3], Math.Round(tempdb4, 2), null);
                //
                double tempdb5 = tempdb4 - tempdb3;
                zfqlist[4].GetType().GetProperty("y" + i.ToString()).SetValue(zfqlist[4], Math.Round(tempdb5, 2), null);
                dqwg4list[0].GetType().GetProperty("y" + i.ToString()).SetValue(dqwg4list[0], Math.Round(tempdb41, 2), null);
                dqwg4list[1].GetType().GetProperty("y" + i.ToString()).SetValue(dqwg4list[1], Math.Round(tempdb42, 2), null);

            }
               try
                {
                    for (int i = 1; i < zfqlist.Count; i++)
                    {
                        Common.Services.BaseService.Update("UpdatePs_Table_WG", zfqlist[i]);
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        Common.Services.BaseService.Update("UpdatePs_Table_WG", dqwg4list[i]);
                    }
                }
                catch (Exception ew)
                {
                    MessageBox.Show("修正数据关系出错" + ew.Message);
                }
               #endregion
 
            
        }
        
        //修改数据
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
            //有些公式计算值不能修改
            if (focusedNode.GetValue("Sort").ToString() == "2" || focusedNode.GetValue("Sort").ToString() == "3" || focusedNode.GetValue("Sort").ToString() == "4" || focusedNode.GetValue("Sort").ToString() == "5" || focusedNode.GetValue("Sort").ToString() == "7" || focusedNode.GetValue("Sort").ToString() == "8")
            {
                MsgBox.Show("此行值不能直接修改，请修改相关数据");
                return;
            }
            if (focusedNode.GetValue("Col1") != null && focusedNode.GetValue("Col1").ToString() == "0")
            {
                FrmChangeBian frm = new FrmChangeBian();
                frm.GetProject = GetProjectID;
                frm.Mark = OperTable.BbWg;
                frm.Text = "修改" + focusedNode.GetValue("Title");
                frm.label1.Text = focusedNode.ParentNode.GetValue("Title").ToString() + " 地区：";
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
                    Ps_Table_WG table = new Ps_Table_WG();
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
                        Common.Services.BaseService.Update("UpdatePs_Table_WG", table);
                        if (table.Sort != 6)
                        {
                             if (table.Sort == 1 || table.Sort == 9 || table.Sort == 10 || table.Sort == 11||table.Sort>=12)
                            {
                              DataChange(GetLastParent(treeList1.FocusedNode,"Sort"), GetLastParent(treeList1.FocusedNode,"ID"));
                            }
                        }
                        LoadData();
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
                frm.Mark = OperTable.ph110;
                frm.GridData = eList;
                frm.Title = focusedNode.GetValue("Title").ToString();
                frm.ParentID = focusedNode.GetValue("ID").ToString();
                string curVolumn = focusedNode.GetValue("y" + range.EndYear).ToString();
                frm.CurVolumn = curVolumn;
                frm.MaxYear = GetChildMaxYear(conn);
                frm.TypeTable = "110";
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Ps_Table_WG table = new Ps_Table_WG();
                    table = Common.Services.BaseService.GetOneByKey<Ps_Table_WG>(focusedNode.GetValue("ID"));
                    table.Title = frm.Title;
                   
                    try
                    {
                        Common.Services.BaseService.Update("UpdatePs_Table_WG", table);
                        LoadData();
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
                    if (frm.ParentName!=focusedNode.GetValue("Title").ToString())
                    {
                        string title = frm.ParentName;
                        string connstr = " Title='" + title + "' and ProjectID='" + GetProjectID + "'";
                        IList templist = Common.Services.BaseService.GetList("SelectPs_Table_WGListByConn", connstr);
                        if (templist.Count > 0)
                        {
                            MessageBox.Show("修改未成功, "+title + " 地区已存在！","提示");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("您未做修改！","提示");
                        return;
                    }
                    

                    Ps_Table_WG table1 = new Ps_Table_WG();
                    //table1 = focusedNode.TreeList.GetDataRecordByNode(focusedNode) as Ps_Table_WG;
                    table1.ID = focusedNode.GetValue("ID").ToString();
                    table1.ParentID = focusedNode.GetValue("ParentID").ToString();
                    table1.Sort = int.Parse(focusedNode.GetValue("Sort").ToString());
                    table1.Title = frm.ParentName;
                    table1.ProjectID = GetProjectID;
                    table1.BuildYear = focusedNode.GetValue("BuildYear").ToString();
                    table1.Col1 = focusedNode.GetValue("Col1").ToString(); 
                    try
                    {
                        Common.Services.BaseService.Update("UpdatePs_Table_WG", table1);
                        LoadData();

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
            int sort=int.Parse(treeList1.FocusedNode.GetValue("Sort").ToString());
            if (treeList1.FocusedNode.GetValue("ParentID").ToString() != "0"&&sort<12)
            {
                MsgBox.Show("不能删除此记录，您可以选择删除整个地区！");
                return;
            }
            else
            {
                if (MsgBox.ShowYesNo("是否删除 " + treeList1.FocusedNode.GetValue("Title") + " 记录？") == DialogResult.Yes)
                {
                    try
                    {
                        Ps_Table_WG ny = new Ps_Table_WG();
                        ny.ID = treeList1.FocusedNode.GetValue("ID").ToString();
                        string teID = "";
                        if (treeList1.FocusedNode.ParentNode!=null)
                        {
                            teID = treeList1.FocusedNode.ParentNode.GetValue("ID").ToString();
                        }
                        else
                        {
                             try
                        {
                            teID = treeList1.FocusedNode.NextNode.GetValue("ID").ToString();
                        }
                        catch
                             { }
                        }
                       
                        string pare = treeList1.FocusedNode.GetValue("ParentID").ToString();
                        Common.Services.BaseService.Delete(ny);
                        DelAll(ny.ID);
                        //如果不是地区结点，则要对数据进行重新计算
                        if (treeList1.FocusedNode.GetValue("ParentID").ToString()!="0")
                        {
                            DataChange(GetLastParent(treeList1.FocusedNode, "Sort"), GetLastParent(treeList1.FocusedNode, "ID"));
                        }
                        LoadData();

                        FoucsLocation(teID, treeList1.Nodes);
                    }
                    catch { }
                }
            }
           
        }
        //删除所有

        public void DelAll(string suid)
        {
            string conn = "ParentId='" + suid + "'";
            IList<Ps_Table_WG> list = Common.Services.BaseService.GetList<Ps_Table_WG>("SelectPs_Table_WGListByConn", conn);
            if (list.Count > 0)
            {
                foreach (Ps_Table_WG var in list)
                {
                    string child = var.ID;
                    //if (var.Sort.ToString()=="2")
                        DelAll(child);
                    Ps_Table_WG ny = new Ps_Table_WG();
                    ny.ID = child;
                    Common.Services.BaseService.Delete(ny);
                }
            }
        }
        //反回所在结点的第地区结点ID 或者Sort
        public string GetLastParent(TreeListNode tl, string str)
        {
            if (str=="ID")
            {
                if (tl.ParentNode==null)
                {
                    return tl.GetValue("ID").ToString();
                }
                else
                {
                    return GetLastParent(tl.ParentNode, str);
                }
            }
            else if(str == "Sort")
            {
                if (tl.ParentNode == null)
                {
                    return tl.GetValue("Sort").ToString();
                }
                else
                {
                    return GetLastParent(tl.ParentNode, str);
                }
            }
            else
            {
                return "错误！";
            }
        }

        //添加电厂
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (treeList1.FocusedNode == null)
            {
                return;
            }

            if (!base.AddRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }
            string tempID = treeList1.FocusedNode.GetValue("ID").ToString();
            string Title = "";
            string AreaName="";
            if (treeList1.FocusedNode != null)
            {
              string ParentID = treeList1.FocusedNode.GetValue("ParentID").ToString();
              int Sort = int.Parse(treeList1.FocusedNode.GetValue("Sort").ToString());
              string ID=treeList1.FocusedNode.GetValue("ID").ToString();
              string addID = "";
              //当前点为第一类结点
              //SelectPs_Table_WG_Sort7or8 a.ParentID=第一类结点ID b.Sort=7/8
               if (ParentID=="0")
              {
                   AreaName=treeList1.FocusedNode.GetValue("Title").ToString();
                  addID = Common.Services.BaseService.GetObject("SelectPs_Table_WG_Sort7or8", " and b.ProjectID='" + GetProjectID + "' and a.ParentID='" + ID + "' and b.Sort=7").ToString();
              }
              else
              {
                  //当前点为第一子类
                  if (Sort == 1 || Sort == 2 || Sort == 3 || Sort == 4 || Sort == 5 || Sort == 6)
                  {
                      AreaName=treeList1.FocusedNode.ParentNode.GetValue("Title").ToString();
                      ID=treeList1.FocusedNode.ParentNode.GetValue("ID").ToString();
                      addID = Common.Services.BaseService.GetObject("SelectPs_Table_WG_Sort7or8", " and b.ProjectID='" + GetProjectID + "' and a.ParentID='" + ID + "' and b.Sort=7").ToString();
                  }
                  //当前点为第二子类
                  if (Sort == 7 || Sort == 8 || Sort == 9 || Sort == 10 || Sort == 11)
                  {
                      AreaName=treeList1.FocusedNode.ParentNode.ParentNode.GetValue("Title").ToString();
                      ID = treeList1.FocusedNode.ParentNode.ParentNode.GetValue("ID").ToString();
                      addID = Common.Services.BaseService.GetObject("SelectPs_Table_WG_Sort7or8", " and b.ProjectID='" + GetProjectID + "' and a.ParentID='" + ID + "' and b.Sort=7").ToString();
                  }
                  //当前点为第三子类
                  if (Sort >= 12)
                  {AreaName=treeList1.FocusedNode.ParentNode.ParentNode.ParentNode.GetValue("Title").ToString();
                       ID=treeList1.FocusedNode.ParentNode.ParentNode.ParentNode.GetValue("ID").ToString();
                      addID = Common.Services.BaseService.GetObject("SelectPs_Table_WG_Sort7or8", " and b.ProjectID='" + GetProjectID + "' and a.ParentID='" + ID + "' and b.Sort=7").ToString();
                  }
              }
            FormTypeTitleWG frm = new FormTypeTitleWG();

            frm.Text = "增加" +AreaName+ "地区的发电设备无功出力电厂";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Title = frm.TypeTitle;
                try
                {
                    AddPower(addID,Title);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加电厂出错：" + ex.Message);
                }
            }
              LoadData();
            }

            FoucsLocation(tempID, treeList1.Nodes);
        }
        private void AddPower(string ParentID, string Title)
        {
            Ps_Table_WG table1 = new Ps_Table_WG();
            table1.ID += "|" + GetProjectID;
            table1.Title = Title;
            table1.ParentID = ParentID;
            table1.ProjectID = GetProjectID;
            table1.Col1 = "0";
            table1.Sort = 15;
            try
            {
                Common.Services.BaseService.Create("InsertPs_Table_WG", table1);
            }
            catch (Exception ex)
            {
                MsgBox.Show("增加电厂出错：" + ex.Message);
                return;
            }
        }
        //增加变电站
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (treeList1.FocusedNode == null)
            {
                return;
            }

            if (!base.AddRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }
            string tempID = treeList1.FocusedNode.GetValue("ID").ToString();
            string Title = "";
            string AreaName = "";
            if (treeList1.FocusedNode != null)
            {
                string ParentID = treeList1.FocusedNode.GetValue("ParentID").ToString();
                int Sort = int.Parse(treeList1.FocusedNode.GetValue("Sort").ToString());
                string ID = treeList1.FocusedNode.GetValue("ID").ToString();
                string addID = "";
                //当前点为第一类结点
                //SelectPs_Table_WG_Sort7or8 a.ParentID=第一类结点ID b.Sort=7/8
                if (ParentID == "0")
                {
                    AreaName = treeList1.FocusedNode.GetValue("Title").ToString();
                    addID = Common.Services.BaseService.GetObject("SelectPs_Table_WG_Sort7or8", " and b.ProjectID='" + GetProjectID + "' and a.ParentID='" + ID + "' and b.Sort=8").ToString();
                }
                else
                {
                    //当前点为第一子类
                    if (Sort == 1 || Sort == 2 || Sort == 3 || Sort == 4 || Sort == 5 || Sort == 6)
                    {
                        AreaName = treeList1.FocusedNode.ParentNode.GetValue("Title").ToString();
                        ID = treeList1.FocusedNode.ParentNode.GetValue("ID").ToString();
                        addID = Common.Services.BaseService.GetObject("SelectPs_Table_WG_Sort7or8", " and b.ProjectID='" + GetProjectID + "' and a.ParentID='" + ID + "' and b.Sort=8").ToString();
                    }
                    //当前点为第二子类
                    if (Sort == 7 || Sort == 8 || Sort == 9 || Sort == 10 || Sort == 11)
                    {
                        AreaName = treeList1.FocusedNode.ParentNode.ParentNode.GetValue("Title").ToString();
                        ID = treeList1.FocusedNode.ParentNode.ParentNode.GetValue("ID").ToString();
                        addID = Common.Services.BaseService.GetObject("SelectPs_Table_WG_Sort7or8", " and b.ProjectID='" + GetProjectID + "' and a.ParentID='" + ID + "' and b.Sort=8").ToString();
                    }
                    //当前点为第三子类
                    if (Sort >= 12)
                    {
                        AreaName = treeList1.FocusedNode.ParentNode.ParentNode.ParentNode.GetValue("Title").ToString();
                        ID = treeList1.FocusedNode.ParentNode.ParentNode.ParentNode.GetValue("ID").ToString();
                        addID = Common.Services.BaseService.GetObject("SelectPs_Table_WG_Sort7or8", " and b.ProjectID='" + GetProjectID + "' and a.ParentID='" + ID + "' and b.Sort=8").ToString();
                    }
                }
                FormTypeTitleWG frm = new FormTypeTitleWG();

                frm.Text = "增加" + AreaName + "地区已有无功补偿容量电站";

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Title = frm.TypeTitle;
                    try
                    {
                        AddStation(addID, Title);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("增加变电站出错：" + ex.Message);
                    }
                }
                LoadData();
            }

            FoucsLocation(tempID, treeList1.Nodes);

        }
        private void AddStation(string ParentID, string Title)
        {
            Ps_Table_WG table1 = new Ps_Table_WG();
            table1.ID += "|" + GetProjectID;
            table1.Title = Title;
            table1.ParentID = ParentID;
            table1.ProjectID = GetProjectID;
            table1.Col1 = "0";
            table1.Sort = 16;
            try
            {
                Common.Services.BaseService.Create("InsertPs_Table_WG", table1);
            }
            catch (Exception ex)
            {
                MsgBox.Show("增加变电站出错：" + ex.Message);
                return;
            }
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
        private bool can_edit = true;
        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (treeList1.FocusedNode.HasChildren
                || !base.EditRight||!can_edit)
            {
                e.Cancel = true;
            }
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

        private DataTable ResultDataTable(DataTable sourceDataTable, List<Itop.Client.Chen.ChoosedYears> listChoosedYears)
        {
            DataTable dtReturn = new DataTable();
            dtReturn.Columns.Add("Title", typeof(string));
            foreach (Itop.Client.Chen.ChoosedYears choosedYear in listChoosedYears)
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

        private DataTable ResultDataTable1(DataTable sourceDataTable, List<Itop.Client.Chen.ChoosedYears> listChoosedYears)
        {
            DataTable dtReturn = new DataTable();
            dtReturn.Columns.Add("Title", typeof(string));
            foreach (Itop.Client.Chen.ChoosedYears choosedYear in listChoosedYears)
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
        private void add_area()
        {
            if (!base.AddRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }
            TreeListNode focusedNode = treeList1.FocusedNode;
            FrmAddPN frm = new FrmAddPN();
            frm.SetCheckVisible();
            frm.SetCheckText("不计特殊");
            frm.checkEdit1.Visible = false;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string title = frm.ParentName;
                string connstr = " Title='" + title + "' and ProjectID='" + GetProjectID + "'";
                IList templist = Common.Services.BaseService.GetList("SelectPs_Table_WGListByConn", connstr);
                if (templist.Count > 0)
                {
                    MessageBox.Show(title + " 地区已存在！", "提示");
                    return;
                }
                Ps_Table_WG table_yd = new Ps_Table_WG();
                table_yd.ID += "|" + GetProjectID;
                table_yd.Title = frm.ParentName;
                table_yd.ParentID = "0";
                table_yd.Sort = OperTable.GetWGMaxSort() + 1;
                table_yd.ProjectID = GetProjectID;
                try
                {
                    Common.Services.BaseService.Create("InsertPs_Table_WG", table_yd);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加分类出错：" + ex.Message);
                    return;
                }
                string[] lei = new string[6] { "地区最大有功负荷(Pg)", "地区最大自然无功负荷(Ql=1.3*Pg)", "地区无功电源需安装容量(1.15Ql)", "地区无功电源合计", "无功平衡", "无功平衡（枯水期）" };
                string[] nextlei = new string[5] { "地区发电设备无功出力", "地区已有无功补偿容量", "220kV线路充电功率", "110kV线路充电功率", "从网外可送入的无功容量" };
                for (int i = 0; i < lei.Length; i++)
                {
                    string parentID = "";
                    Ps_Table_WG table1 = new Ps_Table_WG();
                    table1.ID += "|" + GetProjectID;
                    parentID = table1.ID;
                    table1.Title = lei[i];
                    table1.ParentID = table_yd.ID;
                    table1.ProjectID = GetProjectID;
                    table1.Col1 = "0";
                    if (frm.BCheck)
                        table1.Col2 = "no";
                    else
                        table1.Col2 = "no1";
                    table1.Sort = i + 1;
                    try
                    {
                        Common.Services.BaseService.Create("InsertPs_Table_WG", table1);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("增加项目出错：" + ex.Message);
                        return;
                    }
                    if (lei[i].ToString() == "地区无功电源合计")
                    {
                        for (int j = 0; j < nextlei.Length; j++)
                        {
                            Ps_Table_WG table2 = new Ps_Table_WG();
                            table2.ID += "|" + GetProjectID;
                            table2.Title = nextlei[j];
                            table2.ParentID = parentID;
                            table2.ProjectID = GetProjectID;
                            table2.Col1 = "0";
                            if (frm.BCheck)
                                table2.Col2 = "no";
                            else
                                table2.Col2 = "no1";
                            table2.Sort = lei.Length + j + 1;
                            try
                            {
                                Common.Services.BaseService.Create("InsertPs_Table_WG", table2);
                            }
                            catch (Exception ex)
                            {
                                MsgBox.Show("增地区无功电源合计子项出错：" + ex.Message);
                                return;
                            }
                            if (nextlei[j].ToString() == "地区发电设备无功出力")
                            {
                                //分区名
                                string AreaName = title;
                                string connstrpower = " AreaID='" + GetProjectID + "' and S9='" + AreaName + "'";
                                IList<string> titlelist = Common.Services.BaseService.GetList<string>("SelectPSP_PowerSubstation_Info_Title", connstrpower);
                                if (titlelist.Count > 0)
                                {
                                    for (int k = 0; k < titlelist.Count; k++)
                                    {
                                        Ps_Table_WG table3 = new Ps_Table_WG();
                                        table3.ID += "|" + GetProjectID;
                                        table3.Title = titlelist[k];
                                        table3.ParentID = table2.ID;
                                        table3.ProjectID = GetProjectID;
                                        table3.Col1 = "0";
                                        if (frm.BCheck)
                                            table3.Col2 = "no";
                                        else
                                            table3.Col2 = "no1";
                                        table3.Sort = 12 + k + 1;

                                        //SelectPSPDEV_SUM_WGpower a.Titile=电厂名, C.OpeerationYear<=年份(发电机),b.ProjectID=GetjID
                                        for (int m = yAnge.BeginYear; m <= yAnge.EndYear; m++)
                                        {
                                            string yearstr = m.ToString();
                                            string con = " Cast(C.OperationYear as int )<=" + m + " and a.Title='" + table3.Title + "'";
                                            double vol = 0;
                                            if (Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_WGpower", con) != null)
                                            {
                                                vol = double.Parse(Common.Services.BaseService.GetObject("SelectPSPDEV_SUM_WGpower", con).ToString());
                                            }
                                            table3.GetType().GetProperty("y" + m.ToString()).SetValue(table3, Math.Round(vol, 2), null);
                                        }

                                        try
                                        {
                                            Common.Services.BaseService.Create("InsertPs_Table_WG", table3);
                                        }
                                        catch (Exception ex)
                                        {
                                            MsgBox.Show("增加地区发电设备无功出力子项出错：" + ex.Message);
                                        }
                                    }
                                }

                            }
                            if (nextlei[j].ToString() == "地区已有无功补偿容量")
                            {
                                string[,] bianstr = new string[2, 2] { { "220kV变电站", "220" }, { "110kV变电站", "110" } };
                                //分区名
                                string AreaName = title;
                                string connstrpower = " AreaID='" + GetProjectID + "' and S9='" + AreaName + "'";
                                //220kV变电站（110）已有无功补偿容量分两部分计算，以当前年份为准，小于等于当前年的数据从现装表里取，大于当前年的数据从估算表里取

                                for (int k = 0; k < bianstr.GetLongLength(1); k++)
                                {
                                    string con = " S2!='' and AreaID='" + GetProjectID + "' and AreaName='" + AreaName + "' and L1=" + bianstr[k, 1];

                                    IList<PSP_Substation_Info> sublist = Common.Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", con);
                                    for (int l = 0; l < sublist.Count; l++)
                                    {
                                        Ps_Table_WG table4 = new Ps_Table_WG();
                                        table4.ID += "|" + GetProjectID;
                                        table4.Title = sublist[l].Title + "(" + bianstr[k, 0] + ")";
                                        table4.ParentID = table2.ID;
                                        table4.ProjectID = GetProjectID;
                                        table4.Col1 = "0";
                                        if (frm.BCheck)
                                            table4.Col2 = "no";
                                        else
                                            table4.Col2 = "no1";
                                        table4.Sort = 13 + k + l + 1;
                                        //Cast(Substring(S2,1,4) as int)<=m 
                                        //从现有数据PSP_Substation_Info取值
                                        for (int m = yAnge.BeginYear; m <= yAnge.EndYear; m++)
                                        {
                                            if (int.Parse(sublist[l].S2.Substring(0, 4)) <= m)
                                            {
                                                double vol = 0;
                                                if (sublist[l].L5 != "")
                                                {
                                                    vol = Double.Parse(sublist[l].L5.ToString());
                                                }

                                                table4.GetType().GetProperty("y" + m.ToString()).SetValue(table4, Math.Round(vol, 2), null);
                                            }
                                        }
                                        try
                                        {
                                            Common.Services.BaseService.Create("InsertPs_Table_WG", table4);
                                        }
                                        catch (Exception ex)
                                        {
                                            MsgBox.Show("增加地区已有无功补偿容量子项出错：" + ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //UpdateFuHe(table_yd.Title, table_yd.ID,"yf");
                //修改相关数据
                DataChange(table_yd.Sort.ToString(), table_yd.ID.ToString());
                this.Cursor = Cursors.WaitCursor;
                treeList1.BeginUpdate();
                LoadData();
                FoucsLocation(table_yd.ID, treeList1.Nodes);
                treeList1.EndUpdate();

                this.Cursor = Cursors.Default;
            }
        }
        //添加地区
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!base.AddRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }
            TreeListNode focusedNode = treeList1.FocusedNode;
            FrmAddPN frm = new FrmAddPN();
            frm.SetCheckVisible();
            frm.SetCheckText("不计特殊");
            frm.checkEdit1.Visible = false;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string title = frm.ParentName;
                string connstr=" Title='"+title+"' and ProjectID='"+GetProjectID+"'";
                IList templist = Common.Services.BaseService.GetList("SelectPs_Table_WGListByConn", connstr);
                if (templist.Count>0)
                {
                    MessageBox.Show(title + " 地区已存在！", "提示");
                    return;
                }
                Ps_Table_WG table_yd = new Ps_Table_WG();
                table_yd.ID += "|" + GetProjectID;
                table_yd.Title = frm.ParentName;
                table_yd.ParentID = "0";
                table_yd.Sort = OperTable.GetWGMaxSort() + 1;
                table_yd.ProjectID = GetProjectID;
                try
                {
                    Common.Services.BaseService.Create("InsertPs_Table_WG", table_yd);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加分类出错：" + ex.Message);
                    return;
                }
                string[] lei = new string[6] { "地区最大有功负荷(Pg)", "地区最大自然无功负荷(Ql=1.3*Pg)", "地区无功电源需安装容量(1.15Ql)", "地区无功电源合计", "无功平衡", "无功平衡（枯水期）"};
                string[] nextlei = new string[5] { "地区发电设备无功出力", "地区已有无功补偿容量", "220kV线路充电功率", "110kV线路充电功率", "从网外可送入的无功容量" };
                for (int i = 0; i < lei.Length; i++)
                {
                    string parentID = "";
                    Ps_Table_WG table1 = new Ps_Table_WG();
                    table1.ID += "|" + GetProjectID;
                    parentID = table1.ID;
                    table1.Title = lei[i];
                    table1.ParentID = table_yd.ID;
                    table1.ProjectID = GetProjectID;
                    table1.Col1 = "0";
                    if (frm.BCheck)
                        table1.Col2 = "no";
                    else
                        table1.Col2 = "no1";
                    table1.Sort = i + 1;
                    try
                    {
                        Common.Services.BaseService.Create("InsertPs_Table_WG", table1);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("增加项目出错：" + ex.Message);
                        return;
                    }
                    if (lei[i].ToString() == "地区无功电源合计")
                    {
                        for (int j = 0; j < nextlei.Length; j++)
                        {
                            Ps_Table_WG table2 = new Ps_Table_WG();
                            table2.ID += "|" + GetProjectID;
                            table2.Title = nextlei[j];
                            table2.ParentID = parentID;
                            table2.ProjectID = GetProjectID;
                            table2.Col1 = "0";
                            if (frm.BCheck)
                                table2.Col2 = "no";
                            else
                                table2.Col2 = "no1";
                            table2.Sort = lei.Length + j + 1;
                            try
                            {
                                Common.Services.BaseService.Create("InsertPs_Table_WG", table2);
                            }
                            catch (Exception ex)
                            {
                                MsgBox.Show("增地区无功电源合计子项出错：" + ex.Message);
                                return;
                            }
                            
                        }
                    }
                }
                //UpdateFuHe(table_yd.Title, table_yd.ID,"yf");
                //修改相关数据
                //DataChange(table_yd.Sort.ToString(),table_yd.ID.ToString());
                this.Cursor = Cursors.WaitCursor;
                treeList1.BeginUpdate();
                LoadData();
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
                frma.Text = "500千伏分区分年容载比计算表";
                frma.Dw1 = "单位：万千瓦、万千伏安";
                frma.GridDataTable = ResultDataTable1(ConvertTreeListToDataTable(treeList1, true), frm.ListChoosedYears);
                if (frma.ShowDialog() == DialogResult.OK && _isSelect)
                {
                    DialogResult = DialogResult.OK;
                }

            }
        }
        //拷贝数据
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCopy frm = new FrmCopy();
            frm.CurID = GetProjectID;
            frm.ClassName = "Ps_Table_WG";
            frm.SelectString = "SelectPs_Table_WGListByConn";
            frm.InsertString = "InsertPs_Table_WG";
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
        //删除所有数据
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("该操作将清除当前项目的所有数据，清除数据以后无法恢复,可能对其他用户的数据产生影响，请谨慎操作，你确定继续吗？", "删除", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string conn = "ProjectID='" + GetProjectID + "'";
                Common.Services.BaseService.Update("DeletePs_Table_WGByConn", conn);
                //Common.Services.BaseService.Update("DeletePs_Table_EditByConn", conn);
                LoadData();
               
            }
        }
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormYearSet fys = new FormYearSet();
            fys.TYPE = OperTable.BbWg;
            fys.PID = ProjectUID;
            if (fys.ShowDialog() != DialogResult.OK)
                return;
            yAnge = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.BbWg + "'");
            LoadData();
        }
        public void CreateEdit(string parentid, string sYear, string eYear, string status, string vol, string mark, string col5)
        {
            Ps_Table_Edit edit = new Ps_Table_Edit();
            edit.ID += "|" + GetProjectID;
            edit.ParentID = parentid;
            edit.StartYear = sYear;
            edit.FinishYear = eYear;
            edit.ProjectID = GetProjectID;
            edit.Status = status;
            edit.Volume = vol;
            edit.Col4 = mark;
            edit.Col5 = col5;
            try
            {
                edit.Sort = OperTable.GetChildMaxSort() + 1;
            }
            catch { edit.Sort = 4; }
            if (edit.Sort < 4)
                edit.Sort = 4;
            Common.Services.BaseService.Create("InsertPs_Table_Edit", edit);
        }

        public void ChangeEdit(string parentid, string sYear, string eYear, string status, string vol, string mark, string col5)
        {
            IList list = Common.Services.BaseService.GetList("SelectPs_Table_EditListByConn", "ParentID='" + parentid + "' and Col5='" + col5 + "' and ProjectID='" + GetProjectID + "'");
            if (list.Count > 0)
            {
                Ps_Table_Edit edit = list[0] as Ps_Table_Edit;
                edit.StartYear = sYear;
                edit.FinishYear = eYear;
                edit.Volume = vol;
                edit.Col4 = mark;
                edit.Col5 = col5;
                edit.Status = status;
                try
                {
                    edit.Sort = OperTable.GetChildMaxSort() + 1;
                }
                catch { edit.Sort = 4; }
                if (edit.Sort < 4)
                    edit.Sort = 4;
                Common.Services.BaseService.Update("UpdatePs_Table_Edit", edit);
            }
            else
                CreateEdit(parentid, sYear, eYear, status, vol, mark, col5);
        }
        //显示丰/枯
        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }
        private void print()
        {
            FormChooseYears frm = new FormChooseYears();
            frm.NoIncreaseRate = true;
            foreach (TreeListColumn column in treeList1.Columns)
            {
                if (column.Caption.IndexOf("年") > 0)
                {
                    frm.ListYearsForChoose.Add((int)column.Tag);
                }
            }

            if (frm.ShowDialog() == DialogResult.OK)
            {
                //FrmResultTJ f = new FrmResultTJ();
                ////f.CanPrint = base.PrintRight;
                //f.Text = Title = "本地区无功平衡表";// +frm.ListChoosedYears[0].Year + "～" + frm.ListChoosedYears[frm.ListChoosedYears.Count - 1].Year + "年经济和电力发展实绩";
                //f.Dw1 = Unit = "单位：万千瓦、万千乏";
                ////f.ColTitleWidth = 230;
                //Ps_Table_WG table_yd = new Ps_Table_WG();
                //f.GridDataTable = ResultDataTable(ConvertTreeListToDataTable(treeList1,false), frm.ListChoosedYears);
                //f.IsSelect = IsSelect;
                //f.YearList = frm.ListChoosedYears;
                //DialogResult dr = f.ShowDialog();
                //if (IsSelect && dr == DialogResult.OK)
                //{
                //    Gcontrol = f.gridControl1;
                //    DialogResult = DialogResult.OK;
                //}
                Form1ResultTJ f = new Form1ResultTJ();
                f.CanPrint = base.PrintRight;
                f.Text = Title = "本地区无功平衡表";// +frm.ListChoosedYears[0].Year + "～" + frm.ListChoosedYears[frm.ListChoosedYears.Count - 1].Year + "年经济和电力发展实绩";
                f.UnitHeader = Unit = "单位：万千瓦、万千乏";
                f.ColTitleWidth = 230;
                Ps_Table_WG table_yd = new Ps_Table_WG();
                f.GridDataTable = ResultDataTable(ConvertTreeListToDataTable(treeList1, false), frm.ListChoosedYears);
                f.IsSelect = IsSelect;
                DialogResult dr = f.ShowDialog();
                if (IsSelect && dr == DialogResult.OK)
                {
                    Gcontrol = f.gridControl1;
                    DialogResult = DialogResult.OK;
                }
            }

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!base.PrintRight)
            {
                MessageBox.Show("您无权使用本功能");
                return;
            }
            print();
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            TreeListNode focusedNode = treeList1.FocusedNode;
            if (focusedNode.GetValue("Sort").ToString() == "2" || focusedNode.GetValue("Sort").ToString() == "3" || focusedNode.GetValue("Sort").ToString() == "4" || focusedNode.GetValue("Sort").ToString() == "5" || focusedNode.GetValue("Sort").ToString() == "7" || focusedNode.GetValue("Sort").ToString() == "8")
            {
                can_edit = false;

            }
            else
            {
                can_edit = true;
            }
        }

        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            Detial_Nodechangevalue(e.Node, e.Column);
        }
        private void Detial_Nodechangevalue(TreeListNode node, TreeListColumn column)
        {
            Ps_Table_WG v = Services.BaseService.GetOneByKey<Ps_Table_WG>(node["ID"].ToString());
            TreeNodeToDataObject<Ps_Table_WG>(v, node);
            Common.Services.BaseService.Update<Ps_Table_WG>(v);
            int tempsort = Convert.ToInt32(node.GetValue("Sort").ToString());
            //TreeListNode parentNode = node.ParentNode;
            //TreeListNode focusedNode = treeList1.FocusedNode;

            if (tempsort != 6)
            {
                if (tempsort == 1 || tempsort == 9 || tempsort == 10 || tempsort == 11 || tempsort >= 12)
                {
                    Data_Change(tempsort, node, column);
                }
            }
            //LoadData();
            //FoucsLocation(table.ID, treeList1.Nodes);
        }

        static public void TreeNodeToDataObject<T>(T dataObject, DevExpress.XtraTreeList.Nodes.TreeListNode treeNode)
        {
            Type type = typeof(T);
            foreach (PropertyInfo pi in type.GetProperties())
            {
                if (pi.Name.Substring(0, 1) == "y")
                    pi.SetValue(dataObject, treeNode.GetValue(pi.Name), null);
            }
        }
        private void Data_Change(int sort, TreeListNode node, TreeListColumn column)
        {
            TreeListNode lastparentnode = FindParentNode(node, sort);
            
            double tempvalue = Convert.ToDouble(node.GetValue(column.FieldName));
            switch (sort)
            {
                case 1: FindNode(lastparentnode, 2).SetValue(column.FieldName, tempvalue * 1.3);
                    Ps_Table_WG v = Services.BaseService.GetOneByKey<Ps_Table_WG>(FindNode(lastparentnode, 2)["ID"].ToString());
                    TreeNodeToDataObject<Ps_Table_WG>(v, FindNode(lastparentnode, 2));
                    Common.Services.BaseService.Update<Ps_Table_WG>(v);

                       FindNode(lastparentnode,3).SetValue(column.FieldName, tempvalue * 1.3*1.15);
                   Ps_Table_WG v1 = Services.BaseService.GetOneByKey<Ps_Table_WG>(FindNode(lastparentnode, 3)["ID"].ToString());
                   TreeNodeToDataObject<Ps_Table_WG>(v1, FindNode(lastparentnode, 3));
                   Common.Services.BaseService.Update<Ps_Table_WG>(v1);

                    double tempdb1=Convert.ToDouble(FindNode(lastparentnode, 4).GetValue(column.FieldName))-tempvalue * 1.3 * 1.15;
                   
                    FindNode(lastparentnode, 5).SetValue(column.FieldName,tempdb1 );
                   Ps_Table_WG v2 = Services.BaseService.GetOneByKey<Ps_Table_WG>(FindNode(lastparentnode, 5)["ID"].ToString());
                   TreeNodeToDataObject<Ps_Table_WG>(v2, FindNode(lastparentnode, 5));
                   Common.Services.BaseService.Update<Ps_Table_WG>(v2);
                   break;
               case 9:
               case 10:
               case 11:
                   sum_78910(lastparentnode, column);
                   break;
                default:
                    sum_node(node.ParentNode, column);
                    sum_78910(lastparentnode, column);

                    break;
            }

        }
        /// <summary>
        /// 处理子结点重新求和并保存
        /// </summary>
        /// <param name="node"></param>
        /// <param name="column"></param>
        ///
        private void sum_node(TreeListNode node, TreeListColumn column)
        {
            double tempsum = 0;
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                tempsum += Convert.ToDouble(node.Nodes[i].GetValue(column.FieldName));
            }
            node.SetValue(column, tempsum);
            Ps_Table_WG v= Services.BaseService.GetOneByKey<Ps_Table_WG>(node["ID"].ToString());
            TreeNodeToDataObject<Ps_Table_WG>(v, node);
            Common.Services.BaseService.Update<Ps_Table_WG>(v);
        }
        private void sum_78910(TreeListNode lastparentnode, TreeListColumn column)
        {
            double tempdb7 = Convert.ToDouble(FindNode(lastparentnode, 7).GetValue(column.FieldName));
            double tempdb8 = Convert.ToDouble(FindNode(lastparentnode, 8).GetValue(column.FieldName));
            double tempdb9 = Convert.ToDouble(FindNode(lastparentnode, 9).GetValue(column.FieldName));
            double tempdb10 = Convert.ToDouble(FindNode(lastparentnode, 10).GetValue(column.FieldName));
            double tempdb11 = Convert.ToDouble(FindNode(lastparentnode, 11).GetValue(column.FieldName));
            double tempdb4 = tempdb7 + tempdb8 + tempdb9 + tempdb10 + tempdb11;
            FindNode(lastparentnode, 4).SetValue(column.FieldName, tempdb4);
            Ps_Table_WG v4 = Services.BaseService.GetOneByKey<Ps_Table_WG>(FindNode(lastparentnode, 4)["ID"].ToString());
            TreeNodeToDataObject<Ps_Table_WG>(v4, FindNode(lastparentnode, 4));
            Common.Services.BaseService.Update<Ps_Table_WG>(v4);
            double tempdb5 = tempdb4 - Convert.ToDouble(FindNode(lastparentnode, 3).GetValue(column.FieldName));
            FindNode(lastparentnode, 5).SetValue(column.FieldName, tempdb5);
            Ps_Table_WG v5 = Services.BaseService.GetOneByKey<Ps_Table_WG>(FindNode(lastparentnode, 5)["ID"].ToString());
            TreeNodeToDataObject<Ps_Table_WG>(v5, FindNode(lastparentnode, 5));
            Common.Services.BaseService.Update<Ps_Table_WG>(v5);
        }
        private TreeListNode FindParentNode(TreeListNode nownode, int sort)
        {
            switch (sort)
            {
                case 1: return nownode.ParentNode;
                    break;
                case 9:
                case 10:
                case 11:
                    return nownode.ParentNode.ParentNode;
                default: return nownode.ParentNode.ParentNode.ParentNode;
                    break;
            }
        }
        private TreeListNode FindNode(TreeListNode parentnode, int sort)
        {
            TreeListNode tempnode = null;
            foreach (TreeListNode temp in parentnode.Nodes)
            {
                if (Convert.ToInt32(temp.GetValue("Sort").ToString()) == sort)
                {
                    tempnode = temp;
                    break;
                }
                else
                {
                    if (temp.HasChildren)
                    {
                        tempnode = FindNode(temp, sort);
                    }
                }
            }
            return tempnode;
        }

        private void treeList1_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Column.FieldName=="Title")
            {
                return;
            }
            Brush backBrush, foreBrush;
            if (e.Node.GetValue("ParentID").ToString() =="0"&&e.Column.FieldName.IndexOf("y")==0)
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
    }
}