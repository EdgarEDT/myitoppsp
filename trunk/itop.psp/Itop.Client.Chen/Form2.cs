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
using System.IO;
using DevExpress.Utils;
using Itop.Client.Projects;


namespace Itop.Client.Chen
{
    public partial class Form2 : FormBase
    {
        DataTable dataTable;

        private TreeListNode lastEditNode = null;
        private TreeListColumn lastEditColumn = null;
        private string lastEditValue = string.Empty;

        private int typeFlag2 = 2;


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
        
        public Form2()
        {
            InitializeComponent();
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
                barButtonItem11.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!base.DeleteRight)
            {
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            HideToolBarButton();

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
            if(dataTable != null)
            {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
            }

            PSP_Types psp_Type = new PSP_Types();
            psp_Type.Flag2 = typeFlag2;
            IList listTypes = Common.Services.BaseService.GetList("SelectPSP_TypesByFlag2", psp_Type);

            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_Types));

            treeList1.DataSource = dataTable;

            treeList1.Columns["Title"].Caption = "分类名";
            treeList1.Columns["Title"].Width = 180;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Flag"].VisibleIndex = -1;
            treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["Flag2"].VisibleIndex = -1;
            treeList1.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["ProjectID"].VisibleIndex = -1;
            treeList1.Columns["ProjectID"].OptionsColumn.ShowInCustomizationForm = false;
            //treeList1.Columns["ParentID"].VisibleIndex = -1;
            //treeList1.Columns["ParentID"].OptionsColumn.ShowInCustomizationForm = false;
            PSP_Years psp_Year = new PSP_Years();
            psp_Year.Flag = typeFlag2;
            IList<PSP_Years> listYears = Common.Services.BaseService.GetList<PSP_Years>("SelectPSP_YearsListByFlag", psp_Year);

            foreach (PSP_Years item in listYears)
            {
                AddColumn(item.Year);
            }
            Application.DoEvents();

            LoadValues();
            //foreach (PSP_Years item in listYears)
            //{
            //    ComputeXianSun(item.Year);
            //   // ComputeXianSun1(item.Year,treeList1.Nodes[0],treeList1.Nodes[1],treeList1.Nodes[2]);
            //}

            treeList1.ExpandAll();
        }

        //读取数据
        private void LoadValues()
        {
            PSP_Values psp_Values = new PSP_Values();
            psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值


            IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesListByFlag2", psp_Values);

            foreach(PSP_Values value in listValues)
            {
                TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
                if(node != null)
                {
                    node.SetValue(value.Year + "年", value.Value);
                }
            }
        }

       
        /// <summary>
        ///  计算线损电量
        /// </summary>
        /// <param name="year"></param>
        public void ComputeXianSun(int year)
        {
            try
            {

                if (treeList1.Nodes[0][year + "年"].ToString() != "" && treeList1.Nodes[1][year + "年"].ToString() != "")
                {
                    if (System.Configuration.ConfigurationSettings.AppSettings["LangFang"].ToString() == "廊坊")
                        treeList1.Nodes[2].SetValue(year + "年", Math.Round(Convert.ToDouble(treeList1.Nodes[1][year + "年"].ToString()) - Convert.ToDouble(treeList1.Nodes[0][year + "年"].ToString()), 1));
                    else
                        treeList1.Nodes[2].SetValue(year + "年", Math.Round(Convert.ToDouble(treeList1.Nodes[0][year + "年"].ToString()) - Convert.ToDouble(treeList1.Nodes[1][year + "年"].ToString()), 1));
                
                }
            }
            catch { }
        }

        /// <summary>
        /// 计算子分类的线损电量
        /// </summary>
        /// <param name="year"></param>
        /// <param name="node0">用电量</param>
        /// <param name="node1">供电量</param>
        /// <param name="node2">线损电量</param>
        /// <param name="column">要计算的列</param>
        public void ComputeXianSun1(int year, TreeListNode node0, TreeListNode node1, TreeListNode node2)
        {
           if(node0.HasChildren==false||node1.HasChildren==false||node2.HasChildren==false)
               return;
           for (int i = 0; i < node2.Nodes.Count; i++)
           {
               try
               {

                   if (node0.Nodes[i][year + "年"].ToString() != "" && node1.Nodes[i][year + "年"].ToString() != "")
                   {
                       ComputeXianSun1(year, node0.Nodes[i], node1.Nodes[i], node2.Nodes[i]);
                       node2.Nodes[i].SetValue(year + "年", Math.Round(Convert.ToDouble(node0.Nodes[i][year + "年"].ToString()) - Convert.ToDouble(node1.Nodes[i][year + "年"].ToString()), 1));
                   }
               }
               catch { }
           }
            ////TreeListNode childNode0 = node0.Nodes.FirstNode;

            ////if (parentNode == null)
            ////{
            ////    return;
            ////}


            ////try
            ////{

            ////    if (treeList1.Nodes[0][year + "年"].ToString() != "" && treeList1.Nodes[1][year + "年"].ToString() != "")
            ////    {
            ////        treeList1.Nodes[2].SetValue(year + "年", Math.Round(Convert.ToDouble(treeList1.Nodes[0][year + "年"].ToString())-Convert.ToDouble(treeList1.Nodes[1][year + "年"].ToString()) , 1));
            ////    }
            ////}
            ////catch { }
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

            if (focusedNode.GetValue("Title").ToString() == "线损")
            {
                MsgBox.Show("此行不允许添加子分类！");
                return;
            }
            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "增加" + focusedNode.GetValue("Title") + "的子分类";

            if(frm.ShowDialog() == DialogResult.OK)
            {
                PSP_Types psp_Type = new PSP_Types();
                psp_Type.Title = frm.TypeTitle;
                psp_Type.Flag = (int)focusedNode.GetValue("Flag");
                psp_Type.Flag2 = (int)focusedNode.GetValue("Flag2");
                psp_Type.ParentID = (int)focusedNode.GetValue("ID");

                try
                {
                    psp_Type.ID = (int)Common.Services.BaseService.Create("InsertPSP_Types", psp_Type);
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
                PSP_Types psp_Type = new PSP_Types();
                Class1.TreeNodeToDataObject<PSP_Types>(psp_Type, treeList1.FocusedNode);
                psp_Type.Title = frm.TypeTitle;

                try
                {
                    Common.Services.BaseService.Update<PSP_Types>(psp_Type);
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

            //if (treeList1.FocusedNode.ParentNode == null)
            //{
            //    MsgBox.Show("一级分类为固定内容，不能删除！");
            //    return;
            //}

            if (treeList1.FocusedNode.HasChildren)
            {
                MsgBox.Show("此分类下有子分类，请先删除子分类！");
                return;
            }

            if (!base.DeleteRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

           if(MsgBox.ShowYesNo("是否删除分类 " + treeList1.FocusedNode.GetValue("Title") + "？") == DialogResult.Yes)
            {
                PSP_Types psp_Type = new PSP_Types();
                Class1.TreeNodeToDataObject<PSP_Types>(psp_Type, treeList1.FocusedNode);
                PSP_Values psp_Values = new PSP_Values();
                psp_Values.TypeID = psp_Type.ID;

                try
                {
                    //DeletePSP_ValuesByType里面删除数据和分类

                    Common.Services.BaseService.Update("DeletePSP_ValuesByType", psp_Values);

                    TreeListNode brotherNode = null;
              
                       
                    if(treeList1.FocusedNode.ParentNode!= null&&treeList1.FocusedNode.ParentNode.Nodes.Count > 1)
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

            FormNewYear frm = new FormNewYear();
            frm.Flag2 = typeFlag2;
            int nFixedColumns = typeof(PSP_Types).GetProperties().Length;
            int nColumns = treeList1.Columns.Count;
            if (nFixedColumns == nColumns + 2)//相等时，表示还没有年份，新年份默认为当前年减去15年

            {
                frm.YearValue = DateTime.Now.Year - 15;
            }
            else
            {
                //有年份时，默认为最大年份加1年

                frm.YearValue = (int)treeList1.Columns[nColumns - 1].Tag + 1;
            }

            if (frm.ShowDialog() == DialogResult.OK)
            {
                AddColumn(frm.YearValue);
            }
        }

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
            catch(Exception ex)
            {
                MsgBox.Show("删除出错：" + ex.Message);
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
                   // ComputeXianSun((int)treeList1.FocusedColumn.Tag);
                   // ComputeXianSun1((int)treeList1.FocusedColumn.Tag,treeList1.Nodes[0],treeList1.Nodes[1],treeList1.Nodes[2]);
                }
                else
                {
                    treeList1.FocusedNode.SetValue(treeList1.FocusedColumn.FieldName, lastEditValue);
                }
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
            catch(Exception ex)
            {
                MsgBox.Show("保存数据出错：" + ex.Message);
                return false;
            }
            return true;
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            ParentNode(treeList1.FocusedNode, e);
            //if (treeList1.FocusedNode.HasChildren
            //    || !base.EditRight)
            if (!base.EditRight)
            {
                e.Cancel = true;
            }
            
        }


        private void ParentNode(TreeListNode node, CancelEventArgs e)
        { 
            if (node.GetValue("Title").ToString() == "线损")
                e.Cancel = true;
            else if (node.ParentNode == null)
            {
                e.Cancel = false;

            }
            else
            {
                ParentNode(node.ParentNode, e);
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

        //统计
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormChooseYears frm = new FormChooseYears();
            foreach (TreeListColumn column in treeList1.Columns)
            {
                if (column.FieldName.IndexOf("年") > 0)
                {
                    frm.ListYearsForChoose.Add((int)column.Tag);
                }
            }

            if(frm.ShowDialog() == DialogResult.OK)
            {
                Form1Result f = new Form1Result();
                f.CanPrint = base.EditRight;
                f.Text = Title = "本地区" + frm.ListChoosedYears[0].Year + "～" + frm.ListChoosedYears[frm.ListChoosedYears.Count - 1].Year + "年分区县供电实绩";
                f.GridDataTable = ResultDataTable(ConvertTreeListToDataTable(treeList1), frm.ListChoosedYears);
                f.IsSelect = IsSelect;
                DialogResult dr = f.ShowDialog();
                if (IsSelect && dr == DialogResult.OK)
                {
                    Gcontrol = f.gridControl1;
                    Unit = "单位：万千瓦时";
                    DialogResult = DialogResult.OK;
                }
            }
        }

        //把树控件内容按显示顺序生成到DataTable中

        private DataTable ConvertTreeListToDataTable(DevExpress.XtraTreeList.TreeList xTreeList)
        {
            DataTable dt = new DataTable();
            List<string> listColID = new List<string>();
            listColID.Add("Flag");
            dt.Columns.Add("Flag", typeof(int));

            listColID.Add("ParentID");
            dt.Columns.Add("ParentID", typeof(int));

            listColID.Add("Title");
            dt.Columns.Add("Title", typeof(string));
            dt.Columns["Title"].Caption = "分类";
            foreach (TreeListColumn column in xTreeList.Columns)
            {
                if (column.FieldName.IndexOf("年") > 0)
                {
                    listColID.Add(column.FieldName);
                    dt.Columns.Add(column.FieldName, typeof(double));
                }
            }

            foreach(TreeListNode node in xTreeList.Nodes)
            {
                AddNodeDataToDataTable(dt, node, listColID);
            }

            return dt;
        }

        private void AddNodeDataToDataTable(DataTable dt, TreeListNode node, List<string> listColID)
        {
            DataRow newRow = dt.NewRow();
            foreach(string colID in listColID)
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

            //根分类结束后加空行

            if(node.ParentNode == null && dt.Rows.Count > 0)
            {
                dt.Rows.Add(new object[] { });
            }

            dt.Rows.Add(newRow);

            foreach(TreeListNode nd in node.Nodes)
            {
                AddNodeDataToDataTable(dt, nd, listColID);
            }
        }

        //根据选择的统计年份，生成统计结果数据表

        private DataTable ResultDataTable(DataTable sourceDataTable, List<ChoosedYears> listChoosedYears)
        {
            DataTable dtReturn = new DataTable();
            dtReturn.Columns.Add("Title", typeof(string));
            foreach (ChoosedYears choosedYear in listChoosedYears)
            {
                dtReturn.Columns.Add(choosedYear.Year + "年", typeof(double));
                if(choosedYear.WithIncreaseRate)
                {
                    dtReturn.Columns.Add(choosedYear.Year + "增长率", typeof(double)).Caption = "增长率";
                }
            }

            #region 填充数据
            for (int i = 0; i < sourceDataTable.Rows.Count; i++)
            {
                DataRow newRow = dtReturn.NewRow();
                DataRow sourceRow = sourceDataTable.Rows[i];
                foreach(DataColumn column in dtReturn.Columns)
                {
                    if(column.Caption != "增长率")
                    {
                        newRow[column.ColumnName] = sourceRow[column.ColumnName];
                    }
                }
                dtReturn.Rows.Add(newRow);
            }
            #endregion

            #region 计算增长率

            DataColumn columnBegin = null;
            foreach(DataColumn column in dtReturn.Columns)
            {
                if(column.ColumnName.IndexOf("年") > 0)
                {
                    if(columnBegin == null)
                    {
                        columnBegin = column;
                    }
                }
                else if(column.ColumnName.IndexOf("增长率") > 0)
                {
                    DataColumn columnEnd = dtReturn.Columns[column.Ordinal - 1];

                    foreach(DataRow row in dtReturn.Rows)
                    {
                        object numerator = row[columnEnd];
                        object denominator = row[columnBegin];

                        if(numerator != DBNull.Value && denominator != DBNull.Value)
                        {
                            try
                            {
                                int intervalYears = Convert.ToInt32(columnEnd.ColumnName.Replace("年", ""))
                                    - Convert.ToInt32(columnBegin.ColumnName.Replace("年", ""));
                                row[column] = Math.Round(Math.Pow((double)numerator / (double)denominator, 1.0 / intervalYears) - 1, 4);
                            }
                            catch
                            {
                            }
                        }
                    }

                    //本次计算增长率的列作为下次的起始列

                    columnBegin = columnEnd;
                }
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

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "增加分类";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                PSP_Types psp_Type = new PSP_Types();
                psp_Type.Title = frm.TypeTitle;
                psp_Type.Flag2 = typeFlag2;
              

                try
                {
                    psp_Type.ID = (int)Common.Services.BaseService.Create("InsertPSP_Types", psp_Type);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加分类出错：" + ex.Message);
                }
            }
        }
        
        private void SetValuesTo0()
        {
            PSP_Values psp_Values = new PSP_Values();
            psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值


            IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesListByFlag2", psp_Values);

            foreach (PSP_Values value in listValues)
            {
                value.Value = 0;
                Common.Services.BaseService.Update<PSP_Values>(value);
              
            }
           
        }
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

              if (MsgBox.ShowYesNo("该操作将清除所有数据，清除数据以后无法恢复,可能对其他用户的数据产生影响，请谨慎操作，你确定继续吗？") == DialogResult.No)
                return;
            PSP_Types psp_Type = new PSP_Types();
            PSP_Values psp_Values = new PSP_Values();
            foreach (DataRow dr  in dataTable.Rows)
            {
                if (dr["Title"].ToString().Contains("全社会"))
                    continue;
               
               // Class1.TreeNodeToDataObject<PSP_Types>(psp_Type, treeList1.FocusedNode);

                psp_Values.TypeID = Convert.ToInt32(dr["id"]);
                //DeletePSP_ValuesByType里面删除数据和分类

                Common.Services.BaseService.Update("DeletePSP_ValuesByType", psp_Values);
               
            }
            Application.DoEvents();
            SetValuesTo0();
            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            LoadData();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;

               
                  
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormResult fr = new FormResult();
            fr.LI = this.treeList1;
            fr.Text = this.Text;
            fr.ShowDialog();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
            if ( !capList.Contains("父ID"))
            {
                capList.Add("父ID");
                filedList.Add("ParentID");
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
            for (int i = m; i < fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data); i++)
            {

                DataRow dr = dt.NewRow();

                for (int j = 0, fiej = 0; fiej < fie.Count && j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
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
                    fiej++;
                 
                }
                // GetL2(dr["L4"].ToString(), out L2);
                // dr["AreaName"] = AreaName; dr["L1"] = L1; dr["S4"] = S4; dr["L2"] = L2; dr["S3"] = ""; dr["S5"] = i.ToString();
                // if (str != "")
                dt.Rows.Add(dr);
            }
            return dt;
        }
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
                    WaitDialogForm wait = new WaitDialogForm("", "正在导入数据, 请稍候..."); 
                   try
            {
                    dts = GetExcel(op.FileName);
                    IList<PSP_Types> lii = new List<PSP_Types>();
                    IList<PSP_Values> liiValues = new List<PSP_Values>();
                    DateTime s8 = DateTime.Now;
                    PSP_Values psp_Values = new PSP_Values();
                    int x = 0;
                    for (int i = 0; i < dts.Rows.Count; i++)
                    {


                        this.Cursor = Cursors.WaitCursor;
                        PSP_Types l1 = new PSP_Types();
                        
                        l1.ID = i;
                        l1.Flag2 = typeFlag2;
                        foreach (DataColumn dc in dts.Columns)
                        {
                            columnname = dc.ColumnName;
                            //if (dts.Rows[i][dc.ColumnName].ToString() == "")
                            //    continue;
                            psp_Values = new PSP_Values();
                            switch (dc.ColumnName)
                            {
                                case "Title":
                                    
                                    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, dts.Rows[i][dc.ColumnName], null);
                                    break;

                                   
                                case "ParentID":

                                    if (dts.Rows[i][dc.ColumnName] == null || dts.Rows[i][dc.ColumnName].ToString() == "" || dts.Rows[i][dc.ColumnName] == DBNull.Value)
                                    {
                                        l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, 0, null);
                                        break;
                                    }
                                    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, Convert.ToInt32(dts.Rows[i][dc.ColumnName]), null);
                                    break;


                                default:
                                    if (dts.Rows[i][dc.ColumnName] == null || dts.Rows[i][dc.ColumnName].ToString() == "" || dts.Rows[i][dc.ColumnName] == DBNull.Value)
                                    {
                                        psp_Values.Value = 0;
                                       
                                    }
                                    else
                                    psp_Values.Value= Convert.ToDouble(dts.Rows[i][dc.ColumnName]);
                                    psp_Values.Year = Convert.ToInt32(dc.ColumnName.Replace("年",""));
                                    psp_Values.TypeID = i;
                                    psp_Values.ID = typeFlag2;
                                    liiValues.Add(psp_Values);

                                    break;
                            }
                        }
                       
                       
                        lii.Add(l1);
                        

                    }
                    int parenti = -4;
                    //foreach (Ps_History l1 in lii)
                    PSP_Types psl1;
                    PSP_Values psp_values;
                    
                    int ti = 0;
                    for (int i = 0,j=0; i < lii.Count; i++)
                    {



                        psl1 = lii[i];
                       
                        ti = psl1.ID;
                        string con = "Flag2='" + typeFlag2 + "' and Title='" + psl1.Title + "'and ParentID ='" + psl1.ParentID + "'";
                        object obj = Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
                        if (obj != null)
                        {
                            psl1.ID = ((PSP_Types)obj).ID;


                            if (psl1.ParentID < 0)
                            {
                                psl1.ParentID = ((PSP_Types)obj).ParentID;
                                psl1.Flag = ((PSP_Types)obj).Flag;
                            }
                            Services.BaseService.Update<PSP_Types>(psl1);
                        }
                        else
                        {
                           Services.BaseService.Create<PSP_Types>(psl1);
                           obj = Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
                           if (obj != null)
                           {
                               psl1.ID = ((PSP_Types)obj).ID;

                           }
                        }
                        for (int jtemp = i + 1; jtemp < lii.Count; jtemp++)
                        {
                            if (lii[jtemp].ParentID == parenti)
                            {
                                lii[jtemp].ParentID = psl1.ID;

                                lii[jtemp].Flag = psl1.Flag;
                            }
                        }

                        for (; j < liiValues.Count; j++)
                        {
                            psp_values = liiValues[j];
                            if (psp_values.TypeID > ti)
                                break;

                            con = "TypeID='" + psl1.ID + "' and Year='" + psp_values.Year + "'";
                            obj = Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                            psp_values.TypeID = psl1.ID;
                            if (obj != null)
                            {
                                psp_values.ID = ((PSP_Values)obj).ID;



                                Services.BaseService.Update<PSP_Values>(psp_values);
                            }
                            else
                            {
                                
                                Services.BaseService.Create<PSP_Values>(psp_values);
                            }
                        }

                        parenti--;
                    }
                   
                    this.Cursor = Cursors.Default;

                    wait.Close();
                    treeList1.BeginUpdate();
                    LoadData();
                    treeList1.EndUpdate();
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
    }
}