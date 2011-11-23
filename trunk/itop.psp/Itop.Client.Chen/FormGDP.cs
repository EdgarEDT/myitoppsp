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

namespace Itop.Client.Chen
{
    public partial class FormGDP : FormBase
    {
        DataTable dataTable;

        private TreeListNode lastEditNode = null;
        private TreeListColumn lastEditColumn = null;
        private string lastEditValue = string.Empty;
        ArrayList al = new ArrayList();
        private int typeFlag2 = 1;

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
        
        public FormGDP()
        {
            InitializeComponent();
        }

        private void HideToolBarButton()
        {
            if (!base.AddRight)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!base.EditRight)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!base.DeleteRight)
            {
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HideToolBarButton();
            barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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

            PSP_GDPTypes psp_Type = new PSP_GDPTypes();
            psp_Type.Flag2 = typeFlag2;
            IList listTypes = Common.Services.BaseService.GetList("SelectPSP_GDPTypesByFlag2", psp_Type);

            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_GDPTypes));

            treeList1.DataSource = dataTable;

            treeList1.Columns["Title"].Caption = "分类名";
            treeList1.Columns["Title"].Width = 180;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Flag"].VisibleIndex = -1;
            treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["Flag2"].VisibleIndex = -1;
            treeList1.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;


            PSP_GDPYeas psp_Year = new PSP_GDPYeas();
            psp_Year.Flag = typeFlag2;

            IList<PSP_GDPYeas> listYears = Common.Services.BaseService.GetList<PSP_GDPYeas>("SelectPSP_GDPYeasListByFlag", psp_Year);

            foreach (PSP_GDPYeas item in listYears)
            {
                AddColumn(item.Year);
            }
            Application.DoEvents();

            LoadValues();
            LoadValues1();//增长率
            treeList1.ExpandAll();
        }

        //读取数据
        private void LoadValues()
        {
            PSP_GDPValues psp_Values = new PSP_GDPValues();
            psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值


            IList<PSP_GDPValues> listValues = Common.Services.BaseService.GetList<PSP_GDPValues>("SelectPSP_GDPValuesByFlag2", psp_Values);

            foreach (PSP_GDPValues value in listValues)
            {
                TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
                if(node != null)
                {
                    node.SetValue(value.Year + "年",Convert.ToDouble(value.Value));
                }
            }
        }

        private void LoadValues1()
        {
            Hashtable hs = new Hashtable();
            foreach (DataColumn dc in dataTable.Columns)
            {
                if (dc.ColumnName.IndexOf("年") < 0)
                    continue;
                hs.Add(Guid.NewGuid().ToString(),dc.ColumnName);


            }

            foreach (DictionaryEntry de1 in hs)
            {
                int year = Convert.ToInt32(de1.Value.ToString().Replace("年",""));
                if(!hs.ContainsValue((year+1)+"年"))
                    continue;
                for (int i = 1; i < 4; i++)
                {
                    double s1 = 0;
                    double s2 = 0;
                    try { s1 = (double)(dataTable.Rows[i][(year + 1) + "年"]); }
                    catch { }
                    try { s2 = (double)(dataTable.Rows[i][year + "年"]); }
                    catch { }
                    if (s2 != 0)
                        dataTable.Rows[i + 4][(year + 1) + "年"] = Math.Round((s1 - s2) * 100 / s2, 2);
                    
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
            int nFixedColumns = typeof(PSP_GDPTypes).GetProperties().Length - 2;//ID和ParentID列不在treeList1.Columns中

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

            if (base.AddRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }
            if (!base.AddRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            Form_Add_GDPTitle frm = new Form_Add_GDPTitle();
            frm.Text = "增加" + focusedNode.GetValue("Title") + "的子分类";

            if(frm.ShowDialog() == DialogResult.OK)
            {
                PSP_GDPTypes psp_Type = new PSP_GDPTypes();
                psp_Type.Title = frm.GDPTitle;
                psp_Type.Flag = (int)focusedNode.GetValue("Flag");
                psp_Type.Flag2 = (int)focusedNode.GetValue("Flag2");
                psp_Type.ParentID = (int)focusedNode.GetValue("ID");

                try
                {
                    psp_Type.ID = (int)Common.Services.BaseService.Create("InsertPSP_GDPTypes", psp_Type);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));
                }
                catch(Exception ex)
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

            if (treeList1.FocusedNode.ParentNode == null)
            {
                MsgBox.Show("一级分类名称不能修改！");
                return;
            }

            if (!base.EditRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            FormTypeTitle frm = new FormTypeTitle();
            frm.TypeTitle = treeList1.FocusedNode.GetValue("Title").ToString();
            frm.Text = "修改分类名";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                PSP_GDPTypes psp_Type = new PSP_GDPTypes();
                Class1.TreeNodeToDataObject<PSP_GDPTypes>(psp_Type, treeList1.FocusedNode);
                psp_Type.Title = frm.TypeTitle;

                try
                {
                    Common.Services.BaseService.Update<PSP_GDPTypes>(psp_Type);
                    treeList1.FocusedNode.SetValue("Title", frm.TypeTitle);
                }
                catch(Exception ex)
                {
                    MsgBox.Show("修改出错：" + ex.Message);
                }
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }

            if(treeList1.FocusedNode.ParentNode == null)
            {
                MsgBox.Show("一级分类为固定内容，不能删除！");
                return;
            }

            if (treeList1.FocusedNode.HasChildren)
            {
                MsgBox.Show("此分类下有子分类，请先删除子分类！");
                return;
            }



            if (base.DeleteRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }
            if (!base.DeleteRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            if(MsgBox.ShowYesNo("是否删除分类 " + treeList1.FocusedNode.GetValue("Title") + "？") == DialogResult.Yes)
            {
                PSP_GDPTypes psp_Type = new PSP_GDPTypes();
                Class1.TreeNodeToDataObject<PSP_GDPTypes>(psp_Type, treeList1.FocusedNode);
                PSP_GDPValues psp_Values = new PSP_GDPValues();
                psp_Values.TypeID = psp_Type.ID;

                try
                {
                    //DeletePSP_ValuesByType里面删除数据和分类

                    Common.Services.BaseService.Update("DeletePSP_GDPValuesByType", psp_Values);

                    TreeListNode brotherNode = null;
                    if(treeList1.FocusedNode.ParentNode.Nodes.Count > 1)
                    {
                        brotherNode = treeList1.FocusedNode.PrevNode;
                        if(brotherNode == null)
                        {
                            brotherNode = treeList1.FocusedNode.NextNode;
                        }
                    }
                    treeList1.DeleteNode(treeList1.FocusedNode);

                    //删除后，如果同级还有其它分类，则重新计算此类的所有年份数据

                    if(brotherNode != null)
                    {
                        foreach(TreeListColumn column in treeList1.Columns)
                        {
                            if (column.FieldName.IndexOf("年") > 0)
                            {
                                CalculateSum(brotherNode, column);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MsgBox.Show("删除出错：" + ex.Message);
                }
            }
        }

        //增加年份
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            if (!base.AddRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }
            FormGDP_AddYears frm = new FormGDP_AddYears();
            frm.Flag2 = typeFlag2;
            int nFixedColumns = typeof(PSP_GDPTypes).GetProperties().Length;
            int nColumns = treeList1.Columns.Count;
            if (nFixedColumns == nColumns + 2)//相等时，表示还没有年份，新年份默认为当前年减去15年

            {
                frm.GetValue = DateTime.Now.Year - 15;
            }
            else
            {
                //有年份时，默认为最大年份加1年

                frm.GetValue = (int)treeList1.Columns[nColumns - 1].Tag + 1;
            }

            if (frm.ShowDialog() == DialogResult.OK)
            {
                AddColumn(frm.GetValue);
                //AddColumnsToGDPCompute(frm.GetValue.ToString());
            }

        }
        /// <summary>
        /// 向GDP基准年统计添加一列
        /// </summary>
        private void AddColumnsToGDPCompute(string year)
        {
            PSP_YearVisibleIndex yvi= new PSP_YearVisibleIndex();
            yvi.Year=year;  
            yvi.ModuleFlag="GDPCompute";
            PSP_YearVisibleIndex yearvi = (PSP_YearVisibleIndex)Common.Services.BaseService.GetObject("SelectPSP_YearVisibleIndexByModuleFlagAndYear", yvi);
            if (yearvi == null)
            {
                yearvi = (PSP_YearVisibleIndex)Common.Services.BaseService.GetObject("SelectPSP_YearVisibleIndexFindMaxVisibleIndex", yvi);
                if (yearvi.VisibleIndex > 0)
                {
                    yvi.VisibleIndex = yearvi.VisibleIndex+1;
                    Common.Services.BaseService.Update("UpdatePSP_YearVisibleIndexbyExists", yvi);
                }
             
            }

        }
        /// <summary>
        /// 删除某一年份
        /// </summary>

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedColumn == null)
            {
                return;
            }

            //不是年份列

            if(treeList1.FocusedColumn.FieldName.IndexOf("年") == -1)
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


            PSP_GDPValues psp_Values = new PSP_GDPValues();
            psp_Values.ID = typeFlag2;//借用ID属性存放Flag2
            psp_Values.Year = (int)treeList1.FocusedColumn.Tag;

            try
            {
                //DeletePSP_ValuesByYear删除数据和年份

                int colIndex = treeList1.FocusedColumn.AbsoluteIndex;
                Common.Services.BaseService.Update("DeletePSP_GDPValuesByYear", psp_Values);

               // deleColumnsToGDPCompute(psp_Values.Year.ToString());

                dataTable.Columns.Remove(treeList1.FocusedColumn.FieldName);
                treeList1.Columns.Remove(treeList1.FocusedColumn);
                if(colIndex >= treeList1.Columns.Count)
                {
                    colIndex--;
                }
                treeList1.FocusedColumn = treeList1.Columns[colIndex];
            }
            catch(Exception ex)
            {
                MsgBox.Show("删除出错：" + ex.Message);
            }
        }
        /// <summary>
        /// 删除GDP基准年统计的某一年份
        /// </summary>

        private void deleColumnsToGDPCompute(string year)
        {
            PSP_YearVisibleIndex yvi = new PSP_YearVisibleIndex();
            yvi.Year = year;
            yvi.ModuleFlag = "GDPCompute";
            PSP_YearVisibleIndex yearvi = (PSP_YearVisibleIndex)Common.Services.BaseService.GetObject("SelectPSP_YearVisibleIndexByModuleFlagAndYear", yvi);
            if (yearvi != null)
            {
                IList<PSP_YearVisibleIndex> yearlist = Common.Services.BaseService.GetList<PSP_YearVisibleIndex>("SelectPSP_YearVisibleYearListByModuleFlagAndVisibleIndex", yearvi);
                Common.Services.BaseService.Update("DeletePSP_YearVisibleIndexByModuleFlagAndYear", yvi);
                foreach (PSP_YearVisibleIndex updatevisible in yearlist)
                {
                    updatevisible.VisibleIndex--;
                    Common.Services.BaseService.Update("UpdatePSP_YearVisibleIndexbyExists", updatevisible);
                }

            }

        }


        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if ((e.Value.ToString() != lastEditValue
                || lastEditNode != e.Node
                || lastEditColumn != e.Column)
                && e.Column.FieldName.IndexOf("年") > 0
                && e.Column.Tag != null)
            {
                if(SaveCellValue((int)treeList1.FocusedColumn.Tag, (int)treeList1.FocusedNode.GetValue("ID"),Convert.ToDouble(e.Value)))
                {
                    CalculateSum(e.Node, e.Column);
                    LoadValues1();
                }
                else
                {
                    treeList1.FocusedNode.SetValue(treeList1.FocusedColumn.FieldName, lastEditValue);
                   
                }
            }
        }

        private bool SaveCellValue(int year, int typeID, double value)
        {
            PSP_GDPValues psp_values = new PSP_GDPValues();
            psp_values.TypeID = typeID;
            psp_values.Value = value;
            psp_values.Year = year;

            try
            {
                Common.Services.BaseService.Update<PSP_GDPValues>( psp_values);
            }
            catch(Exception ex)
            {
                MsgBox.Show("保存数据出错：" + ex.Message);
                return false;
            }
            return true;
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
        
            if(treeList1.FocusedNode.HasChildren
                || !base.EditRight)
            {
                e.Cancel = true;
            }

            if (treeList1.FocusedNode.ParentNode != null && treeList1.FocusedNode.ParentNode.GetValue("Title").ToString() == "增长率(%)")
            {
                e.Cancel = true;
            }

            //if (treeList1.FocusedNode.GetValue("Title").ToString() == "增长率(%)")
            //{
            //    e.Cancel = false ;
            //}
    
           
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
            if (parentNode.GetValue("Title").ToString() == "增长率(%)")
            {
                return;
            }
            foreach(TreeListNode nd in parentNode.Nodes)
            {
                object value = nd.GetValue(column.FieldName);
                if(value != null && value != DBNull.Value)
                {
                    sum += Convert.ToDouble(value);
                }
            }

            parentNode.SetValue(column.FieldName, sum);

            if(!SaveCellValue((int)column.Tag, (int)parentNode.GetValue("ID"), sum))
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


        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
         
            FormGDP_Compute f = new FormGDP_Compute();
            //fgdpc.ShowDialog();



            f.IsSelect = IsSelect;
            DialogResult dr = f.ShowDialog();
            //f.ShowDialog();
            if (IsSelect && dr == DialogResult.OK)
            {
                Gcontrol = f.Gcontrol;
                Unit = "";
                DialogResult = DialogResult.OK;
            }
        }

        //把树控件内容按显示顺序生成到DataTable中

        //private DataTable ConvertTreeListToDataTable(DevExpress.XtraTreeList.TreeList xTreeList)
        //{
        //    DataTable dt = new DataTable();
        //    List<string> listColID = new List<string>();
        //    listColID.Add("Flag");
        //    dt.Columns.Add("Flag", typeof(int));

        //    listColID.Add("ParentID");
        //    dt.Columns.Add("ParentID", typeof(int));

        //    listColID.Add("Title");
        //    dt.Columns.Add("Title", typeof(string));
        //    dt.Columns["Title"].Caption = "分类";
        //    foreach (TreeListColumn column in xTreeList.Columns)
        //    {
        //        if (column.FieldName.IndexOf("年") > 0)
        //        {
        //            listColID.Add(column.FieldName);
        //            dt.Columns.Add(column.FieldName, typeof(double));
        //        }
        //    }

        //    foreach(TreeListNode node in xTreeList.Nodes)
        //    {
        //        //if((int)node.GetValue("Flag") == 5)
        //        //{
        //        //    continue;
        //        //}
        //        AddNodeDataToDataTable(dt, node, listColID);
        //    }

        //    return dt;
        //}

        //private void AddNodeDataToDataTable(DataTable dt, TreeListNode node, List<string> listColID)
        //{
        //    DataRow newRow = dt.NewRow();
        //    foreach(string colID in listColID)
        //    {
        //        //分类名，第二层及以后在前面加空格
        //        if(colID == "Title" && node.ParentNode != null)
        //        {
        //            newRow[colID] = "　　" + node[colID];
        //        }
        //        else
        //        {
        //            newRow[colID] = node[colID];
        //        }
        //    }

        //    //根分类结束后加空行

        //    if (node.ParentNode == null && dt.Rows.Count > 0)
        //    {
        //        dt.Rows.Add(new object[] { });
        //    }

        //    dt.Rows.Add(newRow);

        //    foreach(TreeListNode nd in node.Nodes)
        //    {
        //        AddNodeDataToDataTable(dt, nd, listColID);
        //    }
        //}

        //根据选择的统计年份，生成统计结果数据表

        //private DataTable ResultDataTable(DataTable sourceDataTable, List<ChoosedYears> listChoosedYears)
        //{
        //    DataTable dtReturn = new DataTable();
        //    dtReturn.Columns.Add("Title", typeof(string));
        //    foreach (ChoosedYears choosedYear in listChoosedYears)
        //    {
        //        dtReturn.Columns.Add(choosedYear.Year + "年", typeof(double));
        //        if(choosedYear.WithIncreaseRate)
        //        {
        //            dtReturn.Columns.Add(choosedYear.Year + "增长率", typeof(double)).Caption = "增长率";
        //        }
        //    }

        //    int nRowSumPower = 0;//总用电量所在行
        //    int nRowTotalPower = 0;//供电量所在行
        //    int nRowMaxLoad = 0;//最高负荷所在行
        //    int nRowPopulation = 0;//人口所在行
        //    int nRowDenizen = 0;//居民用电量所在行

        //    #region 填充数据，获取总用电量所在行、最高负荷所在行、人口所在行
        //    //for (int i = 0; i < sourceDataTable.Rows.Count; i++)
        //    //{
        //    //    DataRow newRow = dtReturn.NewRow();
        //    //    DataRow sourceRow = sourceDataTable.Rows[i];
        //    //    foreach(DataColumn column in dtReturn.Columns)
        //    //    {
        //    //        if(column.Caption != "增长率")
        //    //        {
        //    //            newRow[column.ColumnName] = sourceRow[column.ColumnName];
        //    //        }
        //    //    }
        //    //    dtReturn.Rows.Add(newRow);

        //    //    if (sourceRow["Flag"] != DBNull.Value)
        //    //    {
        //    //        if((int)sourceRow["ParentID"] == 0)
        //    //        {
        //    //            switch((int)sourceRow["Flag"])
        //    //            {
        //    //                case 2://总用电量
        //    //                    nRowSumPower = i;
        //    //                    break;

        //    //                case 3://最高负荷，后面加一行Tmax
                                
                                
        //    //                    break;



        //    //                case 5://最高负荷，后面加一行Tmax
        //    //                    nRowMaxLoad = i;
        //    //                    dtReturn.Rows.Add(new object[] { "Tmax" });
        //    //                    break;

        //    //                case 7://总人口

        //    //                    nRowPopulation = i + 1;//由于之前加了一行TMax，此处行加1
        //    //                    dtReturn.Rows.Add(new object[] { "人均用电量(千瓦时/人)" });
        //    //                    dtReturn.Rows.Add(new object[] { "人均生活用电量(千瓦时/人)" });
        //    //                    break;

        //    //                default:
        //    //                    break;
        //    //            }

        //    //        if (sourceRow["Title"].ToString().IndexOf("全社会供电量") > -1)
        //    //        {
        //    //            nRowTotalPower = i;
        //    //        }


        //    //        }
        //    //        else if (sourceRow["Title"].ToString().IndexOf("居民") > -1 && sourceRow["ParentID"].ToString()=="2")
        //    //        {
        //    //            nRowDenizen = i;
        //    //        }
        //    //        //else if (sourceRow["Title"].ToString().IndexOf("供电量") > -1 && sourceRow["ParentID"].ToString()=="26")
        //    //        //else if (sourceRow["Title"].ToString().IndexOf("全社会供电量") > -1)
        //    //        //{
        //    //        //    nRowTotalPower = i;
        //    //        //}

        //    //    }
        //    //}
        //    #endregion

        //    #region 计算TMax和人均用电量
        //    foreach (ChoosedYears choosedYear in listChoosedYears)
        //    {
        //        object sumPower = dtReturn.Rows[nRowSumPower][choosedYear.Year + "年"];//总用电量
        //        object numerator = dtReturn.Rows[nRowTotalPower][choosedYear.Year + "年"];//供电量

        //        object denominator = dtReturn.Rows[nRowMaxLoad][choosedYear.Year + "年"];//最高负荷

        //        if(numerator != DBNull.Value)
        //        {
        //            if(denominator != DBNull.Value)
        //            {
        //                try
        //                {
        //                    dtReturn.Rows[nRowMaxLoad + 1][choosedYear.Year + "年"] = (int)((double)numerator / (double)denominator);
        //                }
        //                catch
        //                {
        //                }
        //            }

        //            denominator = dtReturn.Rows[nRowPopulation][choosedYear.Year + "年"];  //人口
        //            if (denominator != DBNull.Value)
        //            {
        //                try
        //                {
        //                    dtReturn.Rows[nRowPopulation + 1][choosedYear.Year + "年"] = System.Math.Round((double)sumPower / (double)denominator, 3);
        //                }
        //                catch
        //                {
        //                }
        //            }
        //        }

        //        //if (nRowDenizen != 0)
        //        numerator = dtReturn.Rows[nRowDenizen][choosedYear.Year + "年"];  //人口
                
        //        denominator = dtReturn.Rows[nRowPopulation][choosedYear.Year + "年"];  //总人口

        //        if (denominator != DBNull.Value && numerator != DBNull.Value)
        //        {
        //            try
        //            {
        //                if (nRowDenizen != 0)
        //                dtReturn.Rows[nRowPopulation + 2][choosedYear.Year + "年"] = System.Math.Round((double)numerator / (double)denominator, 3);
        //            }
        //            catch
        //            {
        //            }
        //        }
        //    }
        //    #endregion

        //    #region 计算增长率

        //    DataColumn columnBegin = null;
        //    foreach(DataColumn column in dtReturn.Columns)
        //    {
        //        if(column.ColumnName.IndexOf("年") > 0)
        //        {
        //            if(columnBegin == null)
        //            {
        //                columnBegin = column;
        //            }
        //        }
        //        else if(column.ColumnName.IndexOf("增长率") > 0)
        //        {
        //            DataColumn columnEnd = dtReturn.Columns[column.Ordinal - 1];

        //            foreach(DataRow row in dtReturn.Rows)
        //            {
        //                if(row["Title"].ToString() == "Tmax")
        //                {
        //                    continue;
        //                }

        //                object numerator = row[columnEnd];
        //                object denominator = row[columnBegin];

        //                if(numerator != DBNull.Value && denominator != DBNull.Value)
        //                {
        //                    try
        //                    {
        //                        int intervalYears = Convert.ToInt32(columnEnd.ColumnName.Replace("年", ""))
        //                            - Convert.ToInt32(columnBegin.ColumnName.Replace("年", ""));
        //                        row[column] = Math.Round(Math.Pow((double)numerator / (double)denominator, 1.0 / intervalYears) - 1, 4);
        //                    }
        //                    catch
        //                    {
        //                    }
        //                }
        //            }

        //            //本次计算增长率的列作为下次的起始列

        //            columnBegin = columnEnd;
        //        }

        //    }
        //    #endregion

        //    return dtReturn;
        //}

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

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
           
        }

        private void treeList1_EditorKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (treeList1.FocusedNode.ParentNode.GetValue("Title").ToString() == "GDP(当前价) (万元)")
                {
                    if ((Convert.ToInt32(e.KeyChar) >= 48 && Convert.ToInt32(e.KeyChar) <= 57) || Convert.ToInt32(e.KeyChar) == 8 || Convert.ToInt32(e.KeyChar) == 127)
                    { e.Handled = false; }
                    else
                    { e.Handled = true; }
                }


                if (treeList1.FocusedNode.ParentNode.GetValue("Title").ToString() == "增长率(%)")
                {
                    if ((Convert.ToInt32(e.KeyChar) >= 48 && Convert.ToInt32(e.KeyChar) <= 57) || Convert.ToInt32(e.KeyChar) == 8 || Convert.ToInt32(e.KeyChar) == 127)
                    { e.Handled = false; }
                    else
                    { e.Handled = true; }
                }
            }
            catch { }
        }

      
    }
}