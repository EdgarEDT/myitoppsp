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
    public partial class Form14_Huai : FormBase
    {
        private DataTable dataTable;
        private DataTable dtRegion;

        private TreeListNode lastEditNode = null;
        private TreeListColumn lastEditColumn = null;
        private string lastEditValue = string.Empty;

        private int typeFlag2 = 143;
        private int flag =3;//220kV为1，110kV为2,35KV为3

        private bool _isLoadingData = false;



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

        public Form14_Huai()
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

        private void Form14_Load(object sender, EventArgs e)
        {
            HideToolBarButton();

            Show();
            Application.DoEvents();
            LoadData();

            if(dataTable.Rows.Count == 0)
            {
                if(MsgBox.ShowYesNo("此表尚为空表，是否现在添加年份？") == DialogResult.Yes)
                {
                    AddYear();
                }
            }
        }

        private void LoadData()
        {
            _isLoadingData = true;
            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            if (dataTable != null)
            {
                treeList1.Columns.Clear();
            }

            PSP_Types psp_Type = new PSP_Types();
            psp_Type.Flag2 = typeFlag2;
            IList listTypes = Common.Services.BaseService.GetList("SelectPSP_TypesByFlag2", psp_Type);

            if (listTypes == null || listTypes.Count == 0)
            {
                MsgBox.Show("地区分类不存在，程序无法继续执行！");
                Application.ExitThread();
                return;
            }

            dtRegion = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_Types));
            dtRegion.PrimaryKey = new DataColumn[] { dtRegion.Columns["ID"] };

            dataTable = new DataTable();

            dataTable.Columns.Add("ID", typeof(string));//ID.ToString()+Year.ToString()
            dataTable.Columns.Add("ParentID", typeof(string));//ParentID.ToString()+Year.ToString()
            dataTable.Columns.Add("Year", typeof(int));
            dataTable.Columns.Add("TypeID", typeof(int));
            dataTable.Columns.Add("Flag", typeof(int));
            dataTable.Columns.Add("Title", typeof(string));
            for (int i = 1; i < 8; i++)
            {
                dataTable.Columns.Add("Col" + i, typeof(double));
            }

            treeList1.DataSource = dataTable;

            treeList1.Columns["Year"].VisibleIndex = -1;
            treeList1.Columns["Year"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["TypeID"].VisibleIndex = -1;
            treeList1.Columns["TypeID"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["Flag"].VisibleIndex = -1;
            treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["Title"].Caption = "";
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Col1"].Caption = "本地区最高负荷";
            treeList1.Columns["Col1"].Width = 110;
            treeList1.Columns["Col2"].Caption = "66kV及以下装机";
            treeList1.Columns["Col2"].Width = 110;
            treeList1.Columns["Col3"].Caption = "需220kV变电容量";
            treeList1.Columns["Col3"].Width = 110;
            treeList1.Columns["Col4"].Caption = "本年已有变电容量";
            treeList1.Columns["Col4"].Width = 130;
            treeList1.Columns["Col5"].Caption = "本年已有变电项目";
            treeList1.Columns["Col5"].Width = 130;
            treeList1.Columns["Col6"].Caption = "需新增变电容量";
            treeList1.Columns["Col6"].Width = 110;
            treeList1.Columns["Col7"].Caption = "新增项目";
            treeList1.Columns["Col7"].Width = 80;
            treeList1.Columns["Title"].Width = 180;
            for (int i = 1; i < 8; i++ )
            {
                treeList1.Columns["Col" + i].OptionsColumn.AllowSort = false;
                treeList1.Columns["Col" + i].ColumnEdit = repositoryItemTextEdit1;
            }
            
            treeList1.Columns["Year"].SortOrder = SortOrder.Ascending;

            Application.DoEvents();

            LoadValues();

            treeList1.ExpandAll();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
            _isLoadingData = false;
        }

        //读取数据,添加行头标题名称
        private void LoadValues()
        {
            PSP_CapBalance psp_Values = new PSP_CapBalance();
            psp_Values.Flag = flag;

            IList<PSP_CapBalance> listValues = Common.Services.BaseService.GetList<PSP_CapBalance>("SelectPSP_CapBalanceByFlag", psp_Values);

            foreach (PSP_CapBalance value in listValues)
            {
                //
                if(treeList1.FindNodeByFieldValue("Year", value.Year) == null)
                {
                    foreach(DataRow dr in dtRegion.Rows)
                    {
                        DataRow newRow = dataTable.NewRow();
                        newRow["ID"] = value.Year.ToString() + dr["ID"];
                        newRow["ParentID"] = value.Year.ToString() + dr["ParentID"];
                        newRow["Year"] = value.Year;
                        newRow["TypeID"] = dr["ID"];
                        newRow["Flag"] = dr["Flag"];
                        if ((int)dr["ParentID"] == 0)
                        {
                            newRow["Title"] = value.Year + "年" + dr["Title"];
                        }
                        else
                        {
                            newRow["Title"] = dr["Title"];
                        }

                        dataTable.Rows.Add(newRow);
                    }
                }
            }

            foreach (PSP_CapBalance value in listValues)
            {
                TreeListNode node = treeList1.FindNodeByKeyID(value.Year.ToString() + value.TypeID);
                node.SetValue("Col1", value.Col1);
                node["Col2"] = value.Col2;
                node["Col3"] = value.Col3;
                node["Col4"] = value.Col4;
                node["Col5"] = value.Col5;
                node["Col6"] = value.Col6;
                node["Col7"] = value.Col7;
            }
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

            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "增加" + focusedNode.GetValue("Title") + "的下级地区";

            if(frm.ShowDialog() == DialogResult.OK)
            {
                PSP_Types psp_Type = new PSP_Types();
                psp_Type.Title = frm.TypeTitle;
                psp_Type.Flag = (int)focusedNode["Flag"];
                psp_Type.Flag2 = typeFlag2;
                psp_Type.ParentID = (int)focusedNode.GetValue("TypeID");

                try
                {
                    psp_Type.ID = (int)Common.Services.BaseService.Create("InsertPSP_Types", psp_Type);

                    treeList1.BeginUpdate();
                    int year = (int)focusedNode.GetValue("Year");
                    foreach (TreeListNode nd in treeList1.Nodes)
                    {
                        AddChildRegion(nd, psp_Type.Title, psp_Type.ParentID, psp_Type.ID, psp_Type.Flag);
                    }
                    treeList1.EndUpdate();

                    DataRow newRegionRow = dtRegion.NewRow();
                    Itop.Common.DataConverter.ObjectToRow(psp_Type, newRegionRow);
                    dtRegion.Rows.Add(newRegionRow);
                }
                catch(Exception ex)
                {
                    MsgBox.Show("增加下级地区出错：" + ex.Message);
                }
            }
        }

        private void AddChildRegion(TreeListNode node, string title, int parentID, int id, int flag)
        {
            int year = (int)node.GetValue("Year");
            string pid = year.ToString() + parentID;

            if (pid == node["ID"].ToString())
            {
                DataRow newRow = dataTable.NewRow();

                newRow["ID"] = year.ToString() + id;
                newRow["ParentID"] = pid;
                newRow["Year"] = year;
                newRow["Flag"] = flag;
                newRow["TypeID"] = id;
                newRow["Title"] = title;

                dataTable.Rows.Add(newRow);
            }
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                AddChildRegion(node.Nodes[i], title, parentID, id, flag);
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

            if (focusedNode.ParentNode == null)
            {
                MsgBox.Show("全地区名称不能修改！");
                return;
            }

            FormTypeTitle frm = new FormTypeTitle();
            frm.TypeTitle = focusedNode.GetValue("Title").ToString();
            frm.Text = "修改地区名称";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                PSP_Types psp_Type = new PSP_Types();
                psp_Type.Flag = (int)focusedNode["Flag"];
                psp_Type.Flag2 = typeFlag2;
                psp_Type.ParentID = Convert.ToInt32(focusedNode.GetValue("ParentID").ToString().Remove(0, focusedNode.GetValue("Year").ToString().Length));
                psp_Type.Title = frm.TypeTitle;
                psp_Type.ID = (int)focusedNode.GetValue("TypeID");

                try
                {
                    Common.Services.BaseService.Update<PSP_Types>(psp_Type);

                    treeList1.BeginUpdate();
                    foreach (TreeListNode nd in treeList1.Nodes)
                    {
                        UpdateNodeText(nd, psp_Type.Title, psp_Type.ID);
                    }
                    treeList1.EndUpdate();

                    foreach (DataRow dr in dtRegion.Rows)
                    {
                        if ((int)dr["ID"] == psp_Type.ID)
                        {
                            dr["Title"] = psp_Type.Title;
                            break;
                        }
                    }

                }
                catch(Exception ex)
                {
                    MsgBox.Show("修改出错：" + ex.Message);
                }
            }
        }

        private void UpdateNodeText(TreeListNode node, string text, int typeID)
        {
            if (typeID == (int)node["TypeID"])
            {
                node.SetValue("Title", text);
            }
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                UpdateNodeText(node.Nodes[i], text, typeID);
            }
        }

        //删除地区
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

            if(treeList1.FocusedNode.ParentNode == null)
            {
                MsgBox.Show("一级地区为固定内容，不能删除！");
                return;
            }

            if (treeList1.FocusedNode.HasChildren)
            {
                MsgBox.Show("此地区还有下级地区，请先删除下级地区！");
                return;
            }

            if(MsgBox.ShowYesNo("是否删除 " + treeList1.FocusedNode.GetValue("Title") + " 及此地区的所有数据？") == DialogResult.Yes)
            {
                PSP_CapBalance psp_value = new PSP_CapBalance();
                psp_value.TypeID = (int)treeList1.FocusedNode["TypeID"];
                psp_value.Flag = flag;

                try
                {
                    //DeletePSP_CapBalanceByTypeID里面删除数据和分类
                    Common.Services.BaseService.Update("DeletePSP_CapBalanceByTypeID", psp_value);

                    treeList1.BeginUpdate();
                    Cursor = Cursors.WaitCursor;

                    //删除时，从后面开始删除
                    for (int i = treeList1.Nodes.Count - 1; i > -1; i--)
                    {
                        DeleteNode(treeList1.Nodes[i], psp_value.TypeID);
                    }

                    foreach (DataRow dr in dtRegion.Rows)
                    {
                        if ((int)dr["ID"] == psp_value.TypeID)
                        {
                            dtRegion.Rows.Remove(dr);
                            break;
                        }
                    }

                    treeList1.EndUpdate();
                    Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MsgBox.Show("删除出错：" + ex.Message);
                }
            }
        }

        private void DeleteNode(TreeListNode node, int typeID)
        {
            if ((int)node["TypeID"] != typeID)
            {
                //删除时，从后面开始删除
                for (int i = node.Nodes.Count - 1; i > -1; i--)
                {
                    DeleteNode(node.Nodes[i], typeID);
                }
            }
            else
            {
                TreeListNode brotherNode = null;
                if (node.ParentNode.Nodes.Count > 1)
                {
                    brotherNode = node.PrevNode;
                    if (brotherNode == null)
                    {
                        brotherNode = node.NextNode;
                    }
                }
                treeList1.DeleteNode(node);

                //删除后，如果同级还有其它分类，则重新计算此类的所有年份数据
                if (brotherNode != null)
                {
                    foreach (TreeListColumn column in treeList1.Columns)
                    {
                        if (column.FieldName.IndexOf("Col") == 0)
                        {
                            CalculateSum(brotherNode, column);
                        }
                    }
                }
            }
        }

        //增加年份
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddYear();
        }

        private void AddYear()
        {
            if (!base.AddRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            FormNewYear frm = new FormNewYear();
            frm.GetYearOnly = true;
            if (treeList1.Nodes.Count == 0)//表示还没有年份，新年份默认为当前年减去1年
            {
                frm.YearValue = DateTime.Now.Year - 1;
            }
            else
            {
                //有年份时，默认为最大年份加1年
                frm.YearValue = (int)treeList1.Nodes.LastNode["Year"] + 1;
            }

            if (frm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            int year = frm.YearValue;
            if(treeList1.FindNodeByFieldValue("Year", year) != null)
            {
                MsgBox.Show("此年份已经存在！");
                return;
            }

            foreach (DataRow dr in dtRegion.Rows)
            {
                DataRow newRow = dataTable.NewRow();
                newRow["ID"] = year.ToString() + dr["ID"];
                newRow["ParentID"] = year.ToString() + dr["ParentID"];
                newRow["Year"] = year;
                newRow["TypeID"] = dr["ID"];
                newRow["Flag"] = dr["Flag"];
                if ((int)dr["ParentID"] == 0)
                {
                    newRow["Title"] = year + "年" + dr["Title"];
                }
                else
                {
                    newRow["Title"] = dr["Title"];
                }

                dataTable.Rows.Add(newRow);
            }
            SaveNodeValue(treeList1.FindNodeByKeyID(year.ToString() + dtRegion.Rows[0]["ID"]));
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

            if (MsgBox.ShowYesNo("是否删除 " + treeList1.FocusedNode["Year"] + " 及该年的所有数据？") != DialogResult.Yes)
            {
                return;
            }

            PSP_CapBalance psp_value = new PSP_CapBalance();
            psp_value.Year = (int)treeList1.FocusedNode["Year"];
            psp_value.Flag = flag;

            try
            {
                //DeletePSP_ValuesByYear删除数据和年份
                Common.Services.BaseService.Update("DeletePSP_CapBalanceByYear", psp_value);
                treeList1.Nodes.Remove(treeList1.FocusedNode.RootNode);
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
                && e.Column.FieldName.IndexOf("Col") == 0
                && !_isLoadingData)
            {
                CalculateSum(e.Node, e.Column);
                SaveNodeValue(e.Node);
            }
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if(treeList1.FocusedNode.HasChildren
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
            foreach(TreeListNode nd in parentNode.Nodes)
            {
                object value = nd.GetValue(column.FieldName);
                if(value != null && value != DBNull.Value)
                {
                    sum += Convert.ToDouble(value);
                }
            }

            parentNode.SetValue(column.FieldName, sum);

            if(!SaveNodeValue(parentNode))
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
            frm.NoIncreaseRate = true;

            foreach (TreeListNode node in treeList1.Nodes)
            {
                frm.ListYearsForChoose.Add((int)node["Year"]);
            }

            if(frm.ShowDialog() == DialogResult.OK)
            {
                Form14Result f = new Form14Result();
                f.CanPrint = base.PrintRight;
                f.Text = Title = "本地区电网35kV变电容量平衡表";
                f.GridDataTable = ConvertTreeListToDataTable(treeList1, frm.ListChoosedYears);
                f.IsSelect = IsSelect;
                DialogResult dr = f.ShowDialog();
                if (IsSelect && dr == DialogResult.OK)
                {
                    Gcontrol = f.gridControl1;
                    Unit = "单位：万千瓦、万千伏安";
                    DialogResult = DialogResult.OK;
                }
            }
        }

        //把树控件内容按显示顺序生成到DataTable中
        private DataTable ConvertTreeListToDataTable(DevExpress.XtraTreeList.TreeList xTreeList, List<ChoosedYears> listChoosedYears)
        {
            DataTable dt = new DataTable();
            List<string> listColID = new List<string>();
            List<int> listYears = new List<int>();

            foreach(ChoosedYears cy in listChoosedYears)
            {
                listYears.Add(cy.Year);
            }

            listColID.Add("Year");
            dt.Columns.Add("Year", typeof(int));

            listColID.Add("Title");
            dt.Columns.Add("Title", typeof(string));

            foreach (TreeListColumn column in xTreeList.Columns)
            {
                if (column.FieldName.IndexOf("Col") == 0)
                {
                    listColID.Add(column.FieldName);
                    DataColumn dc = dt.Columns.Add(column.FieldName, typeof(double));
                    dc.Caption = column.Caption;
                }
            }

            foreach(TreeListNode node in xTreeList.Nodes)
            {
                if(listYears.IndexOf((int)node["Year"]) > -1)
                {
                    AddNodeDataToDataTable(dt, node, listColID);
                }
            }

            return dt;
        }

        private void AddNodeDataToDataTable(DataTable dt, TreeListNode node, List<string> listColID)
        {
            DataRow newRow = dt.NewRow();
            foreach(string colID in listColID)
            {
                newRow[colID] = node[colID];
                if(colID == "Title")
                {
                    if(node.ParentNode == null)
                    {
                        newRow[colID] = node[colID].ToString().Remove(0, node["Year"].ToString().Length + 1);
                    }
                    else
                    {
                        newRow[colID] = "　　" + node[colID];
                    }
                }
            }
            dt.Rows.Add(newRow);

            foreach(TreeListNode nd in node.Nodes)
            {
                AddNodeDataToDataTable(dt, nd, listColID);
            }
        }

        private bool SaveNodeValue(TreeListNode node)
        {
            PSP_CapBalance psp_value = new PSP_CapBalance();
            TreeNodeToDataObject<PSP_CapBalance>(psp_value, node);
            psp_value.Flag = flag;
            psp_value.TypeID = (int)node["TypeID"];
            psp_value.Year = (int)node["Year"];

            try
            {
                Common.Services.BaseService.Update<PSP_CapBalance>(psp_value);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void TreeNodeToDataObject<T>(T dataObject, DevExpress.XtraTreeList.Nodes.TreeListNode treeNode)
        {
            Type type = typeof(T);
            foreach (PropertyInfo pi in type.GetProperties())
            {
                if (pi.Name.IndexOf("Col") == 0 && treeNode.GetValue(pi.Name) != DBNull.Value)
                {
                    pi.SetValue(dataObject, treeNode.GetValue(pi.Name), null);
                }
            }
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

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

        }
    }
}