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
    public partial class Form21 : FormBase
    {
        DataTable dataTable;

        private TreeListNode lastEditNode = null;
        private TreeListColumn lastEditColumn = null;
        private string lastEditValue = string.Empty;

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
        
        public Form21()
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

        private void Form12_Load(object sender, EventArgs e)
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
            treeList1.Columns["Title"].Width = 250;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Flag"].VisibleIndex = -1;
            treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["Flag2"].VisibleIndex = -1;
            treeList1.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;

            PSP_Years psp_Year = new PSP_Years();
            psp_Year.Flag = typeFlag2;
            IList<PSP_Years> listYears = Common.Services.BaseService.GetList<PSP_Years>("SelectPSP_YearsListByFlag", psp_Year);

            foreach (PSP_Years item in listYears)
            {
                AddColumn(item.Year);
            }
            Application.DoEvents();

            LoadValues();

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

            if (!base.EditRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            if (treeList1.FocusedNode.ParentNode == null)
            {
                MsgBox.Show("一级分类名称不能修改！");
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

            if (!base.DeleteRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
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
                    //CalculateSum(e.Node, e.Column);
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
            frm.NoIncreaseRate = true;

            if(frm.ShowDialog() == DialogResult.OK)
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
                frma.Text = "本地区电力平衡表";
                DataTable dt = ConvertTreeListToDataTable(treeList1);

                frma.GridDataTable  = ResultDataTable(ConvertTreeListToDataTable(treeList1), frm.ListChoosedYears);


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
            }

            int nRowMaxLoad = 0;//地区最高负荷所在行
            int nRowMaxPower = 0;//地区电网供电能力所在行
            int nRowMaxPowerLow = 0;//枯水期地区电网供电能力所在行

            #region 填充数据
            for (int i = 0; i < sourceDataTable.Rows.Count; i++)
            {
                DataRow newRow = dtReturn.NewRow();
                DataRow sourceRow = sourceDataTable.Rows[i];
                foreach(DataColumn column in dtReturn.Columns)
                {
                    newRow[column.ColumnName] = sourceRow[column.ColumnName];
                }
                dtReturn.Rows.Add(newRow);

                if (sourceRow["Flag"] != DBNull.Value && (int)sourceRow["ParentID"] == 0)
                {
                    switch((int)sourceRow["Flag"])
                    {
                        case 61://地区最高负荷
                            nRowMaxLoad = i;
                            break;

                        case 63://地区电网供电能力所在行，后面加一行“电力盈亏”
                            nRowMaxPower = i;
                            dtReturn.Rows.Add(new object[] { "电力盈亏" });
                            break;

                        case 64://枯水期地区电网供电能力
                            nRowMaxPowerLow = i + 1;//由于之前加了一行电力盈亏，此处行加1
                            dtReturn.Rows.Add(new object[] { "枯水期地区电力盈亏" });
                            break;

                        default:
                            break;
                    }
                }
            }
            #endregion

            #region 计算电力盈亏和枯水期地区电力盈亏
            foreach (ChoosedYears choosedYear in listChoosedYears)
            {
                object maxLoad = dtReturn.Rows[nRowMaxLoad][choosedYear.Year + "年"];
                if (maxLoad != DBNull.Value)
                {
                    object maxPower = dtReturn.Rows[nRowMaxPower][choosedYear.Year + "年"];
                    if (maxPower != DBNull.Value)
                    {
                        dtReturn.Rows[nRowMaxPower + 1][choosedYear.Year + "年"] = (double)maxPower - (double)maxLoad;
                    }

                    maxPower = dtReturn.Rows[nRowMaxPowerLow][choosedYear.Year + "年"];
                    if (maxPower != DBNull.Value)
                    {
                        dtReturn.Rows[nRowMaxPowerLow + 1][choosedYear.Year + "年"] = (double)maxPower - (double)maxLoad;
                    }
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
    }
}