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
    public partial class FrmBalanceOfPowerRegion : FormBase
    {
        DataTable dataTable;
        DataTable dataTable2;

        private TreeListNode lastEditNode = null;
        private TreeListColumn lastEditColumn = null;
        private string lastEditValue = string.Empty;
        private Hashtable hash = new Hashtable();
        private Hashtable hash2 = new Hashtable();
        private int typeFlag2 = 200;
        private bool isdel = false;

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
        
        public FrmBalanceOfPowerRegion()
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

            if (!base.EditRight && !base.AddRight)
            {
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            Show();
            hash.Clear();
            hash.Add(0, "500kV");
            hash.Add(1, "220kV");
            hash.Add(2, "110kV");
            hash.Add(3, "35kV");
            hash2.Clear();
            hash2.Add(0, "供电最大负荷");
            hash2.Add(1, "电源可能出力");
            hash2.Add(2, "外受电力");
            hash2.Add(3, "电力余缺");
            hash2.Add(4, "目标容载比");
            hash2.Add(5, "所需容量");
            hash2.Add(6, "现有容量");
            hash2.Add(7, "容量缺额");
            //hash3.Add(0, "500kv");
            //hash3.Add(1, "220kv");
            //hash3.Add(2, "110kv");
            //hash3.Add(3, "35kv");
            //hash3.Add(0, "供电最大负荷");
            //hash3.Add(1, "电源可能出力");
            //hash3.Add(2, "外受电力");
            //hash3.Add("电力余缺");
            HideToolBarButton();
            treeList2.BeginUpdate();
            LoadData2();
            treeList2.EndUpdate();
            treeList2.ExpandAll();
            if (treeList2.Nodes[0].Nodes[0].HasChildren)
            {
                treeList1.BeginUpdate();
                
                LoadData(treeList2.Nodes[0].Nodes[0].Nodes[0]);
      
                treeList1.EndUpdate();
            }
        }
        private void LoadData2()
        {
            List<PSP_Types> Ilisttemp = new List<PSP_Types>();
            PSP_Types psp_Type = new PSP_Types();
            psp_Type.Flag2 = typeFlag2;
            psp_Type.ParentID = 0;
            IList<PSP_Types> listTypes = Common.Services.BaseService.GetList<PSP_Types>("SelectPSP_TypesByParentID", psp_Type);
            foreach (PSP_Types psp_Typetemp in listTypes)
            Ilisttemp.Add(psp_Typetemp);
           
            psp_Type.Flag2 = typeFlag2;
            psp_Type.ID = listTypes[0].ID;
            listTypes = Common.Services.BaseService.GetList<PSP_Types>("SelectPSP_TypesByParentID", psp_Type);
            foreach (PSP_Types psp_Typetemp in listTypes)
                Ilisttemp.Add(psp_Typetemp);
            foreach (PSP_Types psp_Typetemp in listTypes)
            {
                psp_Type.ID= psp_Typetemp.ID;
                psp_Type.Flag2 = typeFlag2;
                IList<PSP_Types> listTypestemp = Common.Services.BaseService.GetList<PSP_Types>("SelectPSP_TypesByParentID", psp_Type);
                foreach (PSP_Types psp_Typetemp2 in listTypestemp)
                    Ilisttemp.Add(psp_Typetemp2);
                   
            }
            dataTable2 = Itop.Common.DataConverter.ToDataTable(Ilisttemp, typeof(PSP_Types));
            treeList2.DataSource = dataTable2;
            treeList2.Columns["Title"].Caption = "分类名";
            treeList2.Columns["Title"].Width = 250;
            treeList2.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList2.Columns["Flag"].VisibleIndex = -1;
            treeList2.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
            treeList2.Columns["Flag2"].VisibleIndex = -1;
            treeList2.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;

        }
        private void LoadData(TreeListNode nd)
        {
            List<PSP_Types> Ilisttemp = new List<PSP_Types>();
            if(dataTable != null)
            {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
            }
          
            PSP_Types psp_Type = new PSP_Types();
            psp_Type.Flag2 = typeFlag2;
            psp_Type.ID =Convert.ToInt32( nd["ID"]) ;
            IList listTypes = Common.Services.BaseService.GetList("SelectPSP_TypesByParentID", psp_Type);
            foreach (PSP_Types psp_Typetemp in listTypes)
                Ilisttemp.Add(psp_Typetemp);
            foreach (PSP_Types psp_Typetemp in listTypes)
            {
                psp_Type.Flag2 = typeFlag2;
                psp_Type.ID = psp_Typetemp.ID;
                IList<PSP_Types> listTypestemp = Common.Services.BaseService.GetList<PSP_Types>("SelectPSP_TypesByParentID", psp_Type);
                foreach (PSP_Types psp_Typetemp2 in listTypestemp)
                    Ilisttemp.Add(psp_Typetemp2);
            }
            dataTable = Itop.Common.DataConverter.ToDataTable(Ilisttemp, typeof(PSP_Types));

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
                    try
                    {
                        node.SetValue(value.Year + "年", value.Value);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
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
            TreeListNode focusedNode = treeList2.FocusedNode;

            if (focusedNode == null)
            {
                return;
            }
            if (focusedNode.ParentNode == null)
            {
                return;
            }

            if (focusedNode.ParentNode.ParentNode != null)
            {
                if (focusedNode.ParentNode.ParentNode.ParentNode== null)
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
                    dataTable2.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable2.NewRow()));
                    AddFixedVoltageRows( psp_Type);
                    treeList2.ExpandAll();
                }
                catch(Exception ex)
                {
                    MsgBox.Show("增加子分类出错：" + ex.Message);
                }
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList2.FocusedNode == null)
            {
                return;
            }

            if (!base.EditRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            if (treeList2.FocusedNode.ParentNode == null)
            {
                MsgBox.Show("一级分类名称不能修改！");
                return;
            }
            if (treeList2.FocusedNode.ParentNode.ParentNode == null)
            {
                //MsgBox.Show("二级分类名称不能修改！");
                return;
            }
        
            FormTypeTitle frm = new FormTypeTitle();
            frm.TypeTitle = treeList2.FocusedNode.GetValue("Title").ToString();
            frm.Text = "修改分类名";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                PSP_Types psp_Type = new PSP_Types();
                Class1.TreeNodeToDataObject<PSP_Types>(psp_Type, treeList2.FocusedNode);
                psp_Type.Title = frm.TypeTitle;

                try
                {
                    Common.Services.BaseService.Update<PSP_Types>(psp_Type);
                    treeList2.FocusedNode.SetValue("Title", frm.TypeTitle);
                }
                catch(Exception ex)
                {
                    MsgBox.Show("修改出错：" + ex.Message);
                }
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList2.FocusedNode == null)
            {
                return;
            }

            if (!base.DeleteRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            if(treeList2.FocusedNode.ParentNode == null)
            {
                MsgBox.Show("一级分类为固定内容，不能删除！");
                return;
            }
            if (treeList2.FocusedNode.ParentNode.ParentNode==null)
            {
                //MsgBox.Show("二级分类为固定内容，不能删除！");
                return;
            }
            //if (treeList1.FocusedNode.HasChildren)
            //{
            //    MsgBox.Show("此分类下有子分类，请先删除子分类！");
            //    return;
            //}

            if(MsgBox.ShowYesNo("是否删除分类 " + treeList2.FocusedNode.GetValue("Title") + "？") == DialogResult.Yes)
            {
                PSP_Types psp_Type = new PSP_Types();
                Class1.TreeNodeToDataObject<PSP_Types>(psp_Type, treeList2.FocusedNode);
                PSP_Values psp_Values = new PSP_Values();
                psp_Values.TypeID = psp_Type.ID;

                try
                {
                    //DeletePSP_ValuesByType里面删除数据和分类
                    Common.Services.BaseService.Update("DeletePSP_ValuesByType", psp_Values);
                    DeletFixedVoltageRows(treeList2.FocusedNode);
                    TreeListNode brotherNode = null;
                    if(treeList2.FocusedNode.ParentNode.Nodes.Count > 1)
                    {
                        brotherNode = treeList2.FocusedNode.PrevNode;
                        if(brotherNode == null)
                        {
                            brotherNode = treeList2.FocusedNode.NextNode;
                        }
                    }
                    if (dataTable != null)
                    {
                        dataTable.Columns.Clear();
                        treeList1.Columns.Clear();
                    }
                    treeList2.DeleteNode(treeList2.FocusedNode);

                    //删除后，如果同级还有其它分类，则重新计算此类的所有年份数据
                    if(brotherNode != null)
                    {
                        foreach(TreeListColumn column in treeList2.Columns)
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
            bool sumis0 = false;
            if (e.Value == null)
                return;
  
            if ((e.Value.ToString() != lastEditValue
                || lastEditNode != e.Node
                || lastEditColumn != e.Column)
                && e.Column.FieldName.IndexOf("年") > 0
                && e.Column.Tag != null)
            {

                if (SaveCellValue((int)e.Column.Tag, (int)treeList1.FocusedNode.GetValue("ID"), Convert.ToDouble(e.Value)))
                {
                    double numtemp=0;
                    //CalculateSum(e.Node, e.Column);
                  
                    if (treeList1.FocusedNode["Title"].ToString() == "目标容载比")
                    {
                        TreeListNode nodtemp = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") -1);
                        if (e.Value != DBNull.Value)
                            numtemp = Convert.ToDouble(e.Value);
                        else
                            numtemp = 0;
                        if (nodtemp[e.Column.Tag+"年"] != DBNull.Value)
                            numtemp = numtemp * Convert.ToDouble(nodtemp[e.Column.Tag+"年"]);
                        else
                            numtemp = 0;
                        if (numtemp != 0)
                        {
                            double numtemp2 = 0;
                            TreeListNode nod = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 1);
                            TreeListNode nod2 = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 2);
                            TreeListNode nod3 = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 3);
                            if (nod2[e.Column.Tag + "年"] != DBNull.Value)
                                numtemp2 = numtemp - Convert.ToDouble(nod2[e.Column.Tag + "年"]);
                            else
                                numtemp2 = 0;
                            SaveCellValue((int)e.Column.Tag, (int)treeList1.FocusedNode.GetValue("ID") + 1, numtemp);
                            nod.SetValue((int)e.Column.Tag + "年", numtemp);
                            if (numtemp2 != 0)
                            {
                                SaveCellValue((int)e.Column.Tag, (int)treeList1.FocusedNode.GetValue("ID") + 3, numtemp2);

                                nod3.SetValue((int)e.Column.Tag + "年", numtemp2);
                            }
                            else
                            {
                                nod3.SetValue((int)e.Column.Tag + "年", null);
                                string flagid = " year=" + e.Column.Tag + " and TypeID=" + nod3["ID"];
                                //psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值
                                //psp_Values.TypeID =Convert.ToInt32(treeList1.FocusedNode["ID"]);
                                IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", flagid);
                                if (listValues.Count == 1)
                                {
                                    Common.Services.BaseService.Delete<PSP_Values>(listValues[0]);
                                }
                            }
                        }
                        else
                        {
                            TreeListNode nod = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 1);
                            nod.SetValue((int)e.Column.Tag + "年", null);
                            string flagid = " year=" + e.Column.Tag + " and TypeID=" + nod["ID"];
                            //psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值
                            //psp_Values.TypeID =Convert.ToInt32(treeList1.FocusedNode["ID"]);
                            IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", flagid);
                            if (listValues.Count == 1)
                            {
                                Common.Services.BaseService.Delete<PSP_Values>(listValues[0]);
                            }
                            TreeListNode nod3 = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 3);
                            nod3.SetValue((int)e.Column.Tag + "年", null);
                            flagid = " year=" + e.Column.Tag + " and TypeID=" + nod3["ID"];
                            //psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值
                            //psp_Values.TypeID =Convert.ToInt32(treeList1.FocusedNode["ID"]);
                            listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", flagid);
                            if (listValues.Count == 1)
                            {
                                Common.Services.BaseService.Delete<PSP_Values>(listValues[0]);
                            }
                        
                        }
                    }
                    else
                        if (treeList1.FocusedNode["Title"].ToString() == "电力余缺")
                         {
                             TreeListNode nodtemp = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 1);
                             if (e.Value != DBNull.Value)
                                 numtemp = Convert.ToDouble(e.Value);
                             else
                                 numtemp = 0;
                            
                             if (nodtemp[e.Column.Tag+"年"] != DBNull.Value)
                                 numtemp = numtemp * Convert.ToDouble(nodtemp[e.Column.Tag+"年"]);
                             else
                                 numtemp = 0;
                             if (numtemp != 0)
                             {
                                 double numtemp2 = 0;
                                 TreeListNode nod = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 2);
                                 TreeListNode nod2 = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 3);
                                 TreeListNode nod3 = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 4);
                                 if (nod2[e.Column.Tag + "年"] != DBNull.Value)
                                     numtemp2 = numtemp - Convert.ToDouble(nod2[e.Column.Tag + "年"]);
                                 else
                                     numtemp2 = 0;
                                 SaveCellValue((int)e.Column.Tag, (int)treeList1.FocusedNode.GetValue("ID") + 2, numtemp);
                                 nod.SetValue((int)e.Column.Tag + "年", numtemp);
                                 if (numtemp2 != 0)
                                 {
                                     SaveCellValue((int)e.Column.Tag, (int)treeList1.FocusedNode.GetValue("ID") + 4, numtemp2);
                                     nod3.SetValue((int)e.Column.Tag + "年", numtemp2);
                                 }
                                 else
                                 {
                                     nod3.SetValue((int)e.Column.Tag + "年", null);
                                     string flagid = " year=" + e.Column.Tag + " and TypeID=" + nod3["ID"];
                                     //psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值
                                     //psp_Values.TypeID =Convert.ToInt32(treeList1.FocusedNode["ID"]);
                                     IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", flagid);
                                     if (listValues.Count == 1)
                                     {
                                         Common.Services.BaseService.Delete<PSP_Values>(listValues[0]);
                                     }
                                     

                                 }
                             }
                             else
                             {
                                 TreeListNode nod = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 2);
                                 nod.SetValue((int)e.Column.Tag + "年", null);
                                 string flagid = " year=" + e.Column.Tag + " and TypeID=" + nod["ID"];
                                 //psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值
                                 //psp_Values.TypeID =Convert.ToInt32(treeList1.FocusedNode["ID"]);
                                 IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", flagid);
                                 if (listValues.Count == 1)
                                 {
                                     Common.Services.BaseService.Delete<PSP_Values>(listValues[0]);
                                 }
                                 TreeListNode nod3 = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 4);
                                   nod3.SetValue((int)e.Column.Tag + "年", null);
                                     flagid = " year=" + e.Column.Tag + " and TypeID=" + nod3["ID"];
                                     //psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值
                                     //psp_Values.TypeID =Convert.ToInt32(treeList1.FocusedNode["ID"]);
                                     listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", flagid);
                                     if (listValues.Count == 1)
                                     {
                                         Common.Services.BaseService.Delete<PSP_Values>(listValues[0]);
                                     }
                                    

                             
                             }
                         }
                         else
                             if (treeList1.FocusedNode["Title"].ToString() == "现有容量")
                             {
                                 TreeListNode nodtemp = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") - 1);
                                 if (e.Value != DBNull.Value)
                                     numtemp = Convert.ToDouble(e.Value);
                                 else
                                     numtemp = 0;
                                 if (nodtemp[e.Column.Tag+"年"] != DBNull.Value)
                                     numtemp = Convert.ToDouble(nodtemp[e.Column.Tag+"年"]) - numtemp;
                                 else
                                     numtemp = 0;
                                 if (numtemp != 0)
                                 {
                                     TreeListNode nod = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 1);
                                     SaveCellValue((int)e.Column.Tag, (int)treeList1.FocusedNode.GetValue("ID") + 1, numtemp);
                                     nod.SetValue((int)e.Column.Tag + "年", numtemp);
                                 }
                                 else
                                 {
                                     TreeListNode nod = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 1);
                                     nod.SetValue((int)e.Column.Tag + "年", null);
                                     string flagid = " year=" + e.Column.Tag + " and TypeID=" + nod["ID"];
                                     //psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值
                                     //psp_Values.TypeID =Convert.ToInt32(treeList1.FocusedNode["ID"]);
                                     IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", flagid);
                                     if (listValues.Count == 1)
                                     {
                                         Common.Services.BaseService.Delete<PSP_Values>(listValues[0]);
                                     }
                                 
                                 }
                             }
                             else
                                 if (treeList1.FocusedNode["Title"].ToString() == "所需容量")
                                 {
                                     TreeListNode nodtemp = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 1);
                                     if (e.Value != DBNull.Value)
                                         numtemp = Convert.ToDouble(e.Value);
                                     else
                                         numtemp = 0;
                                     if (nodtemp[e.Column.Tag+"年"] != DBNull.Value)
                                         numtemp = numtemp- Convert.ToDouble(nodtemp[e.Column.Tag+"年"]);
                                     else
                                         numtemp = 0;
                                     if (numtemp != 0)
                                     {
                                         TreeListNode nod = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 2);
                                         SaveCellValue((int)e.Column.Tag, (int)treeList1.FocusedNode.GetValue("ID") + 2, numtemp);
                                         nod.SetValue((int)e.Column.Tag + "年", numtemp);
                                     }
                                     else
                                     {
                                         TreeListNode nod = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 2);
                                         nod.SetValue((int)e.Column.Tag + "年", null);
                                         string flagid = " year=" + e.Column.Tag + " and TypeID=" + nod["ID"];
                                         //psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值
                                         //psp_Values.TypeID =Convert.ToInt32(treeList1.FocusedNode["ID"]);
                                         IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", flagid);
                                         if (listValues.Count == 1)
                                         {
                                             Common.Services.BaseService.Delete<PSP_Values>(listValues[0]);
                                         }
                                     }
                                 }
                                 else
                                     if (treeList1.FocusedNode["Title"].ToString() == "供电最大负荷")
                                     {
                                         TreeListNode nodtemp1= treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 1);
                                         TreeListNode nodtemp2 = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 2);
                                         
                                       
                                        if (e.Value != DBNull.Value)
                                             numtemp = Convert.ToDouble(e.Value);
                                         else
                                             numtemp = 0;
                                         
                                         if (nodtemp1[e.Column.Tag + "年"] != DBNull.Value)
                                             numtemp = numtemp - Convert.ToDouble(nodtemp1[e.Column.Tag + "年"]);
                                        
                                     
                                         if (nodtemp2[e.Column.Tag + "年"] != DBNull.Value)
                                             numtemp = numtemp - Convert.ToDouble(nodtemp2[e.Column.Tag + "年"]);
                                         if (nodtemp1[e.Column.Tag + "年"] == DBNull.Value && nodtemp2[e.Column.Tag + "年"] == DBNull.Value)
                                             numtemp = 0;
                                         if (numtemp != 0)
                                         {
                                             TreeListNode nod = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 3);
                                             SaveCellValue((int)e.Column.Tag, (int)treeList1.FocusedNode.GetValue("ID") + 3, numtemp);
                                             //nod.SetValue((int)e.Column.Tag + "年", numtemp);
                                             treeList1.MoveNext();
                                             treeList1.MoveNext();
                                             treeList1.MoveNext();
                                             if (nod["ID"].ToString() == treeList1.FocusedNode["ID"].ToString())
                                                 treeList1.FocusedNode.SetValue((int)e.Column.Tag + "年", numtemp);
                                             treeList1.MovePrev();
                                             treeList1.MovePrev();
                                             treeList1.MovePrev();
                                         }
                                         else
                                         {
                                             TreeListNode nod = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 3);
                                             //nod.SetValue((int)e.Column.Tag + "年", null);
                                             treeList1.MoveNext();
                                             treeList1.MoveNext();
                                             treeList1.MoveNext();
                                             if (nod["ID"].ToString() == treeList1.FocusedNode["ID"].ToString())
                                             {
                                                 treeList1.FocusedNode.SetValue((int)e.Column.Tag + "年", 0);
                                                 //sumis0 = true;
                                             }
                                             string flagid = " year=" + e.Column.Tag + " and TypeID=" + nod["ID"];
                                             //psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值
                                             //psp_Values.TypeID =Convert.ToInt32(treeList1.FocusedNode["ID"]);
                                             IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", flagid);
                                             if (listValues.Count == 1)
                                             {
                                                 Common.Services.BaseService.Delete<PSP_Values>(listValues[0]);
                                             }
                                           
                                                 treeList1.MovePrev();
                                                 treeList1.MovePrev();
                                                 treeList1.MovePrev();
                                            nod.SetValue((int)e.Column.Tag + "年", null);
                                         
                                         }     
                                     }
                                     else
                                         if (treeList1.FocusedNode["Title"].ToString() == "电源可能出力")
                                         {
                                             TreeListNode nodtemp1 = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID")-1);
                                             TreeListNode nodtemp2 = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 1);

                                             if (nodtemp1[e.Column.Tag + "年"] != DBNull.Value)
                                                 numtemp = Convert.ToDouble(nodtemp1[e.Column.Tag + "年"]);
                                             else
                                                 numtemp = 0;
                                            
                                             if (e.Value != DBNull.Value)
                                                 numtemp = numtemp - Convert.ToDouble(e.Value);
                                            
                                              
                                            
                                                 if (nodtemp2[e.Column.Tag + "年"] != DBNull.Value)
                                                     numtemp = numtemp - Convert.ToDouble(nodtemp2[e.Column.Tag + "年"]);
                                                 if (nodtemp1[e.Column.Tag + "年"] == DBNull.Value && nodtemp2[e.Column.Tag + "年"] == DBNull.Value)
                                                     numtemp = 0;
                                             if (numtemp != 0)
                                             {
                                                 TreeListNode nod = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 2);
                                                 SaveCellValue((int)e.Column.Tag, (int)treeList1.FocusedNode.GetValue("ID") +2, numtemp);
                                                 //nod.SetValue((int)e.Column.Tag + "年", numtemp);
                                                 treeList1.MoveNext();
                                                 treeList1.MoveNext();
                                                 if (nod["ID"].ToString() == treeList1.FocusedNode["ID"].ToString())
                                                     treeList1.FocusedNode.SetValue((int)e.Column.Tag + "年", numtemp);
                                             }
                                             else
                                             {
                                                 TreeListNode nod = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") +2);
                                                 //nod.SetValue((int)e.Column.Tag + "年", null);
                                                 treeList1.MoveNext();
                                                 treeList1.MoveNext();
                                                 if (nod["ID"].ToString() == treeList1.FocusedNode["ID"].ToString())
                                                 {
                                                     treeList1.FocusedNode.SetValue((int)e.Column.Tag + "年", 0);
                                                    
                                                 }
                                                 string flagid = " year=" + e.Column.Tag + " and TypeID=" + nod["ID"];
                                                 //psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值
                                                 //psp_Values.TypeID =Convert.ToInt32(treeList1.FocusedNode["ID"]);
                                                 IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", flagid);
                                                 if (listValues.Count == 1)
                                                 {
                                                     Common.Services.BaseService.Delete<PSP_Values>(listValues[0]);
                                                 }
                                                 nod.SetValue((int)e.Column.Tag + "年", null);
                                             }
                                         }
                                         else
                                             if (treeList1.FocusedNode["Title"].ToString() == "外受电力")
                                             {
                                                 TreeListNode nodtemp1 = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") - 2);
                                                 TreeListNode nodtemp2 = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") -1);

                                                 if (nodtemp1[e.Column.Tag + "年"] != DBNull.Value)
                                                     numtemp = Convert.ToDouble(nodtemp1[e.Column.Tag + "年"]);
                                                 else
                                                     numtemp = 0;
                                                 
                                                     if (e.Value != DBNull.Value)
                                                         numtemp = numtemp - Convert.ToDouble(e.Value);
                                                    

                                              
                                                     if (nodtemp2[e.Column.Tag + "年"] != DBNull.Value)
                                                         numtemp = numtemp - Convert.ToDouble(nodtemp2[e.Column.Tag + "年"]);
                                                     if (nodtemp1[e.Column.Tag + "年"] == DBNull.Value && nodtemp2[e.Column.Tag + "年"] == DBNull.Value)
                                                         numtemp = 0;
                                                 if (numtemp != 0)
                                                 {
                                                     TreeListNode nod = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 1);
                                                     SaveCellValue((int)e.Column.Tag, (int)treeList1.FocusedNode.GetValue("ID") + 1, numtemp);
                                                     treeList1.MoveNext();
                                                     if (nod["ID"].ToString() == treeList1.FocusedNode["ID"].ToString())
                                                     treeList1.FocusedNode.SetValue((int)e.Column.Tag + "年", numtemp);
                                                 }
                                                 else
                                                 {
                                                     TreeListNode nod = treeList1.FindNodeByKeyID((int)treeList1.FocusedNode.GetValue("ID") + 1);
                                                     //nod.SetValue((int)e.Column.Tag + "年", null);
                                                     treeList1.MoveNext();
                                                     if (nod["ID"].ToString() == treeList1.FocusedNode["ID"].ToString())
                                                     {
                                                         treeList1.FocusedNode.SetValue((int)e.Column.Tag + "年", 0);
                                                         sumis0 = true;
                                                     }
                                                     string flagid = " year=" + e.Column.Tag + " and TypeID=" + nod["ID"];
                                                     //psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值
                                                     //psp_Values.TypeID =Convert.ToInt32(treeList1.FocusedNode["ID"]);
                                                     IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", flagid);
                                                     if (listValues.Count == 1)
                                                     {
                                                         Common.Services.BaseService.Delete<PSP_Values>(listValues[0]);
                                                     }
                                                     nod.SetValue((int)e.Column.Tag + "年", null);
                                                 }
                                             }
                }
                else
                {
                    treeList1.FocusedNode.SetValue(treeList1.FocusedColumn.FieldName, lastEditValue);
                }
              
            }
            if (isdel)
            {

                isdel = false;
                treeList1.FocusedNode.SetValue(treeList1.FocusedColumn.FieldName, null);
                string flagid = " year=" + e.Column.Tag + " and TypeID=" + treeList1.FocusedNode["ID"];
                //psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值

               
                //psp_Values.TypeID =Convert.ToInt32(treeList1.FocusedNode["ID"]);
                IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", flagid);
                if (listValues.Count == 1)
                {
                    Common.Services.BaseService.Delete<PSP_Values>(listValues[0]);
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
            if (/*treeList1.FocusedNode["Title"].ToString() == "供电最大负荷"
                || */treeList1.FocusedNode["Title"].ToString() == "容量缺额"
                || treeList1.FocusedNode["Title"].ToString() == "所需容量" || treeList1.FocusedNode["Title"].ToString() == "电力余缺")
                 e.Cancel = true;
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
        private void copydata(DataTable dt1, DataTable dt2)
        {
            foreach (DataColumn dc in dataTable.Columns)
                if (!dt2.Columns.Contains(dc.ColumnName))
                    dt2.Columns.Add(dc.ColumnName, dc.DataType);
                else
                    continue;
            foreach (DataRow row1 in dt1.Rows)
            {
                DataRow row2 = dt2.NewRow();
                foreach (DataColumn dc in dt1.Columns)
                {

                    
                    row2[dc.ColumnName] = row1[dc.ColumnName];
                }
                /* if(a2.Contains(row2["Title"].ToString()))*/
                dt2.Rows.Add(row2);
            }
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
            DataTable dt = new DataTable();
            PSP_Types psp_Type = new PSP_Types();
            psp_Type.Flag2 = typeFlag2;
            IList listTypes = Common.Services.BaseService.GetList("SelectPSP_TypesByFlag2", psp_Type);
            dt = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_Types));
            PSP_Years psp_Year = new PSP_Years();
            psp_Year.Flag = typeFlag2;
            IList<PSP_Years> listYears = Common.Services.BaseService.GetList<PSP_Years>("SelectPSP_YearsListByFlag", psp_Year);

            foreach (PSP_Years item in listYears)
            {
                dt.Columns.Add(item.Year + "年", typeof(double));
            }
            PSP_Values psp_Values = new PSP_Values();
            psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值

            IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesListByFlag2", psp_Values);
            foreach (PSP_Values psptemp in listValues)
            {
                foreach (DataRow dttemp in dt.Rows)
                {
                    if (Convert.ToInt32(dttemp["ID"].ToString()) == psptemp.TypeID)
                        dttemp[psptemp.Year + "年"] = psptemp.Value;
                }
            }
            if(frm.ShowDialog() == DialogResult.OK) 
            {


                FrmBalanceOfPowerRegionPrint frma = new FrmBalanceOfPowerRegionPrint();
                frma.IsSelect = _isSelect;
                frma.HASH = hash;
                frma.HASH2 = hash2;
                frma.Text = "南北区电力平衡表";
            
                frma.GridDataTable = ResultDataTable(dt, frm.ListChoosedYears);
                frma.ISPrint = PrintRight;
                if (frma.ShowDialog() == DialogResult.OK && _isSelect)
                {
        
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
        private void addrowtodt1(DataRow dr, DataRow row, ref DataTable dt1, Hashtable hatemp, int jtemp, DataRow sumrow)
        {
            foreach (DataColumn dc in dt1.Columns)
            {
                if (dc.ColumnName.IndexOf("年") > 0)
                {
                    if (dr[dc.ColumnName] == null || dr[dc.ColumnName] == DBNull.Value)
                        row[dc.ColumnName] = 0;
                    else
                    {
                        if (sumrow[dc.ColumnName] == null || sumrow[dc.ColumnName] == DBNull.Value)
                            sumrow[dc.ColumnName] = 0;
                        sumrow[dc.ColumnName] = Convert.ToDouble(dr[dc.ColumnName]) + Convert.ToDouble(sumrow[dc.ColumnName]);
                        row[dc.ColumnName] = Convert.ToDouble(dr[dc.ColumnName]);

                    }

                    ////try
                    ////{
                    ////    row[dc.ColumnName] = Convert.ToDouble(dr[dc.ColumnName].ToString());
                    ////}
                    ////catch { row[dc.ColumnName] = 0;  }
                }
                else if (dc.ColumnName == "Title2")
                {
                    row["Title2"] = dr["Title"].ToString();
                }
                else if (dc.ColumnName == "ID")
                {
                    if (hatemp[dr["Flag"]] != null)

                        if (hatemp[dr["Flag"]].ToString() == "2")
                        {
                            row[dc.ColumnName] = Convert.ToDouble(dr["ID"].ToString()) * 1000;
                            continue;
                        }
                    row[dc.ColumnName] = Convert.ToDouble(dr["ID"].ToString());


                }
                else if (dc.ColumnName =="Flag")
                    row[dc.ColumnName] = jtemp;
                else if (dc.ColumnName.IndexOf("Title") < 0)
                    row[dc.ColumnName] = dr[dc.ColumnName].ToString();
            }
            dt1.Rows.Add(row);
        } 
        //根据选择的统计年份，生成统计结果数据表
        private DataTable ResultDataTable(DataTable sourceDataTable, List<ChoosedYears> listChoosedYears)
        {
            DataCommon dcsort = new DataCommon();
            Hashtable hatemp=new Hashtable ();
           DataTable dt1 = new DataTable();
           //dt1.Columns.Add("Title");
           if (dt1 != null)
               dt1.Columns.Clear();
           dt1.Columns.Add("Title1", typeof(string));
           dt1.Columns.Add("Title2", typeof(string));
      
            //dtReturn.Columns.Add("Title", typeof(string));
            foreach (DataColumn dc in sourceDataTable.Columns)
                if (dc.ColumnName.IndexOf("年") < 0)
                    dt1.Columns.Add(dc.ColumnName, dc.DataType);
            foreach (ChoosedYears choosedYear in listChoosedYears)
            {
                dt1.Columns.Add(choosedYear.Year + "年", typeof(double));
            }
            try
            {
                #region 添加地区南北区临时行
            DataRow[] dt = dataTable2.Select("ParentID=0");
            DataRow[] dt2 = dataTable2.Select("ParentID=" + dt[0]["ID"].ToString());

            if (dt2[0]["Title"].ToString() == "北区")
            {
                hatemp.Add(dt2[0]["Flag"], 2);

                hatemp.Add(dt2[1]["Flag"], 1);
            }
            else
            {
                hatemp.Add(dt2[0]["Flag"], 1);

                hatemp.Add(dt2[1]["Flag"], 2);
            }
            sourceDataTable = dcsort.GetSortTable(sourceDataTable, "Flag,ID", true);
              #endregion
            addProvisionalline(dt[0], ref dt1, 0);
            if (dt2[0]["Title"].ToString() == "北区")
            {
                addProvisionalline(dt2[1], ref dt1, 1);
                addProvisionalline(dt2[0], ref dt1, 2);

            }
            else
            {
                addProvisionalline(dt2[0], ref dt1, 1);
                addProvisionalline(dt2[1], ref dt1, 2);
            }
           
                string title="";
                string title1 = "";
                int jtemp = 0;
                foreach (DataRow row1 in sourceDataTable.Rows)
                {

                    if (!hash.ContainsValue(row1["Title"]) && !hash2.ContainsValue(row1["Title"]))
                    {
                        title = row1["Title"].ToString(); ;
                        continue;
                    }
                    if (hash.ContainsValue(row1["Title"]))
                    {
                        title1 = row1["Title"].ToString(); ;
                        continue;
                    }

                    
                        DataRow rowtemp1 = dt1.NewRow();
                        rowtemp1["Title"] = title;
                        rowtemp1["Title1"] = title1;
                        if (hatemp[row1["Flag"]] != null)
                            if (hatemp[row1["Flag"]].ToString() == "2")
                                addrowtodt1(row1, rowtemp1, ref dt1, hatemp, jtemp,  dt1.Rows[jtemp + 64]);
                            else
                                addrowtodt1(row1, rowtemp1, ref dt1, hatemp, jtemp, dt1.Rows[jtemp + 32]);
                        jtemp++;
                        if (jtemp % 32 == 0)
                            jtemp = 0;
                }
           
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            for (int itemp = 0; itemp < 32; itemp++)
            {
                foreach (DataColumn dc in dt1.Columns)
                {
                    if (dc.ColumnName.IndexOf("年") > 0)
                    {
                        if (dt1.Rows[itemp + 32][dc.ColumnName] == null || dt1.Rows[itemp + 32][dc.ColumnName] == DBNull.Value)
                            dt1.Rows[itemp + 32][dc.ColumnName] = 0;
                        if (dt1.Rows[itemp + 64][dc.ColumnName] == null || dt1.Rows[itemp + 64][dc.ColumnName] == DBNull.Value)
                            dt1.Rows[itemp + 64][dc.ColumnName] = 0;
                        dt1.Rows[itemp][dc.ColumnName] = Convert.ToInt32(dt1.Rows[itemp + 32][dc.ColumnName]) + Convert.ToInt32(dt1.Rows[itemp + 64][dc.ColumnName]);
                    }
                }
            }
                return dt1;
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
        private void AddFixedVoltageRows(PSP_Types psp_Type)
        {
            int i = 0;
           
            for (i = 0; i < hash.Count; i++)
            {
                PSP_Types psp_Typetemp = new PSP_Types();
                psp_Typetemp.Title = hash[i].ToString();
                psp_Typetemp.Flag = psp_Type.Flag;
                psp_Typetemp.Flag2 = psp_Type.Flag2;
                psp_Typetemp.ParentID = psp_Type.ID;
                try
                {
                    psp_Typetemp.ID = (int)Common.Services.BaseService.Create("InsertPSP_Types", psp_Typetemp);
                    //dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Typetemp, dataTable.NewRow()));
                }
                catch { }
                for (int j = 0; j < hash2.Count; j++)
                {
                    PSP_Types psp_Typetemp2 = new PSP_Types();
                    psp_Typetemp2.Title = hash2[j].ToString();
                    psp_Typetemp2.Flag = psp_Typetemp.Flag;
                    psp_Typetemp2.Flag2 = psp_Typetemp.Flag2;
                    psp_Typetemp2.ParentID = psp_Typetemp.ID;
                    try
                    {
                        psp_Typetemp2.ID = (int)Common.Services.BaseService.Create("InsertPSP_Types", psp_Typetemp2);
                        //dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Typetemp, dataTable.NewRow()));
                    }
                    catch { }
                }
            }
        }
        private void DeletFixedVoltageRows(TreeListNode node)
        {
           
            PSP_Types psp_Type = new PSP_Types();
            psp_Type.Flag2 = typeFlag2;
            psp_Type.ID = Convert.ToInt32(node["ID"]);
            IList listTypes = Common.Services.BaseService.GetList("SelectPSP_TypesByParentID", psp_Type);
            try
            {
                foreach (PSP_Types psp_Typetemp in listTypes)
                {

                    PSP_Values psp_Values = new PSP_Values();
                    psp_Values.TypeID = psp_Typetemp.ID;


                    //DeletePSP_ValuesByType里面删除数据和分类
                    Common.Services.BaseService.Update("DeletePSP_ValuesByType", psp_Values);


                    PSP_Types psp_Type2 = new PSP_Types();
                    psp_Type2.Flag2 = typeFlag2;
                    psp_Type2.ID = psp_Typetemp.ID;
                    IList listTypes2 = Common.Services.BaseService.GetList("SelectPSP_TypesByParentID", psp_Type2);
                    foreach (PSP_Types psp_Typetemp2 in listTypes2)
                    {


                        psp_Values.TypeID = psp_Typetemp2.ID;


                        //DeletePSP_ValuesByType里面删除数据和分类
                        Common.Services.BaseService.Update("DeletePSP_ValuesByType", psp_Values);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void addProvisionalline(DataRow dr, ref DataTable dt, int jtemp)
        {
            int iflag = 0;
            string title=dr["Title"].ToString();
            string title1="";
            int id = 0;
            if (jtemp == 2) id = Convert.ToInt32(dr["ID"].ToString())*1000;
            else
                id= Convert.ToInt32(dr["ID"].ToString());
            for (int i = 0; i < hash.Count; i++)
            {

                title1 = hash[i].ToString();
               
                for (int j = 0; j < hash2.Count; j++)
                {
                   
                    DataRow drtemp2 = dt.NewRow();
                    foreach (DataColumn dc in dt.Columns)
                        if (dc.ColumnName == "Title2")
                        {
                            drtemp2["Title"] = title;
                            drtemp2["Title1"] = title1;
                            drtemp2[dc.ColumnName] = hash2[j].ToString();
                        }
                        else
                            if (dc.ColumnName.IndexOf("年") < 0)
                            {
                            
                                        if (dc.ColumnName == "ID")
                                            drtemp2[dc.ColumnName] = id;
                                    else
                                            if (dc.ColumnName.IndexOf("Title") < 0)
                                                drtemp2[dc.ColumnName] = dr[dc.ColumnName];
                                            else
                                            {
                                                if (dataTable.Columns.Contains(dc.ColumnName))
                                                    drtemp2[dc.ColumnName] = dr[dc.ColumnName];
                                             
                                            }
                            }
                      
                    dt.Rows.Add(drtemp2);
                    iflag++;
                }
            }
  
        }
          //把树控件内容按显示顺序生成到DataTable中
        private DataTable NewConvertTreeListToDataTable(DevExpress.XtraTreeList.TreeList xTreeList)
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
            return dt;
        }

        private void treeList2_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (treeList2.FocusedNode==null)
                return;
            if (treeList2.FocusedNode.ParentNode== null)
                return;
            if (treeList2.FocusedNode.ParentNode.ParentNode == null)
                return;
            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            LoadData(treeList2.FocusedNode);
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
        }

        private void treeList1_DoubleClick(object sender, EventArgs e)
        {
            if (treeList1.FocusedNode["Title"].ToString() == "供电最大负荷"&&(EditRight||AddRight))
            {
                FrmBalanceOfPowerRegionDialog0 frm = new FrmBalanceOfPowerRegionDialog0();
                frm.Text = "选择供电最大负荷";
                //frm.ShowDialog();
                if (frm.ShowDialog() == DialogResult.OK)
                { 
                    foreach(TreeListColumn colum in treeList1.Columns)
                    {
                        if(colum.FieldName.Contains("年"))
                        {
                            foreach (PSP_P_Values psptemp in frm.LI)
                            {
                                if (psptemp.Year + "年" == colum.FieldName)
                                {

                                    try
                                    {

                                        SaveCellValue(psptemp.Year, (int)treeList1.FocusedNode.GetValue("ID"), psptemp.Value);
                                        treeList1.FocusedNode.SetValue(colum.FieldName, psptemp.Value);
                                    }
                                    catch(Exception ex)
                                    {
                                        MessageBox.Show(ex.ToString());
                                    }
                                }
                            }
                        }
                  
                    }
                }
            }
        }

        private void treeList1_EditorKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back || e.KeyData == Keys.Space || e.KeyData == Keys.Delete)
            {
                isdel = true;
            }
            else
                isdel = false;
            if (treeList1.FocusedNode != null)
                if (treeList1.FocusedNode[treeList1.FocusedColumn.FieldName].ToString() == "0" && isdel)
                {

                    treeList1.FocusedNode.SetValue(treeList1.FocusedColumn.FieldName, null);
                    string flagid = " year=" + treeList1.FocusedColumn.FieldName.Replace("Y", "").Replace("年", "") + " and TypeID=" + treeList1.FocusedNode["ID"];
                    //psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值
                    //psp_Values.TypeID =Convert.ToInt32(treeList1.FocusedNode["ID"]);
                    IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", flagid);
                    if (listValues.Count == 1)
                    {
                        Common.Services.BaseService.Delete<PSP_Values>(listValues[0]);
                    }

                }
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode["Title"].ToString() == "供电最大负荷")
            {
                FrmBalanceOfPowerRegionDialog0 frm = new FrmBalanceOfPowerRegionDialog0();
                frm.Text = "选择供电最大负荷";
                //frm.ShowDialog();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    foreach (TreeListColumn colum in treeList1.Columns)
                    {
                        if (colum.FieldName.Contains("年"))
                        {
                            foreach (PSP_P_Values psptemp in frm.LI)
                            {
                                if (psptemp.Year + "年" == colum.FieldName)
                                {

                                    try
                                    {
                                       
                                            SaveCellValue(psptemp.Year, (int)treeList1.FocusedNode.GetValue("ID"), psptemp.Value);
                                            treeList1.FocusedNode.SetValue(colum.FieldName, psptemp.Value);
                                   
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.ToString());
                                    }
                                }
                            }
                        }

                    }
                }
            }
        }
    }
}