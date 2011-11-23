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
using Itop.Client.Common;
using Itop.Domain.Stutistic;
using Itop.Client.Chen;
using DevExpress.Utils;
using System.IO;
using DevExpress.XtraGrid.Columns;

namespace Itop.Client.Stutistics
{
    public partial class Form7 : FormBase
    {
        DataTable dataTable;

        private TreeListNode lastEditNode = null;
        private TreeListColumn lastEditColumn = null;
        private string lastEditValue = string.Empty;
        private int typeFlag = 10;

        private int ItemID = 0;

        private bool AddRight = false;

        public bool ADDRight
        {
            get { return AddRight; }
            set { AddRight = value; }
        }

        private bool EditRight = false;

        public bool EDItRight
        {
            get { return EditRight; }
            set { EditRight = value; }
        }

        private bool DeleteRight = false;

        public bool DELeteRight
        {
            get { return DeleteRight; }
            set { DeleteRight = value; }
        }

        private bool PrintRight = false;

        public bool PRIntRight
        {
            get { return PrintRight; }
            set { PrintRight = value; }
        }


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

        public Form7(PowerEachList report)
        {
            typeFlag =Convert.ToInt32( report.ListName);
            InitializeComponent();
        }


        private void HideToolBarButton()
        {
            if (!AddRight)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!EditRight)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!DeleteRight)
            {
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!AddRight && !EditRight)
            {
                //barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                //barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            HideToolBarButton();

            Show();
            Application.DoEvents();

            WaitDialogForm wait =  new WaitDialogForm("", "���ڼ�������, ���Ժ�...");
            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            LoadData();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
            wait.Close();
        }

        private void LoadData()
        {
            string baseyear = "";
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
            }

            PSP_BigUser_Types psp_Type = new PSP_BigUser_Types();
            psp_Type.S2 = "S2 LIKE '%" + typeFlag + "%' and ItemID='" + ItemID+"'";
            IList listTypes = Common.Services.BaseService.GetList("SelectPSP_BigUser_TypesByItemID", psp_Type);

            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_BigUser_Types));

            treeList1.DataSource = dataTable;

            treeList1.Columns["Title"].Caption = "��λ����";
            treeList1.Columns["Title"].Width = 180;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Flag"].VisibleIndex = -1;
            treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["Flag2"].VisibleIndex = -1;
            treeList1.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["S1"].VisibleIndex = -1;
            treeList1.Columns["S1"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["S2"].VisibleIndex = -1;
            treeList1.Columns["S2"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["S3"].VisibleIndex = -1;
            treeList1.Columns["S3"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["S4"].VisibleIndex = -1;
            treeList1.Columns["S4"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["S5"].VisibleIndex = -1;
            treeList1.Columns["S5"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["ItemID"].VisibleIndex = -1;
            treeList1.Columns["ItemID"].OptionsColumn.ShowInCustomizationForm = false;
           
            PSP_BigUser_Years psp_Year = new PSP_BigUser_Years();
            IList<PSP_BigUser_Years> listYears;
         
            psp_Year.Flag = typeFlag;
            psp_Year.ItemID= ItemID;

            listYears = Common.Services.BaseService.GetList<PSP_BigUser_Years>("SelectPSP_BigUser_YearsListByFlag", psp_Year);

            foreach (PSP_BigUser_Years item in listYears)
            {
                
                AddColumn(item.Year);
    
            }
            Application.DoEvents();

            LoadValues(true);

            treeList1.ExpandAll();
           
        }

        //��ȡ����
        private void LoadValues(bool Isfirstload)
        {
           

            PSP_BigUser_Values psp_Values = new PSP_BigUser_Values();
            IList<PSP_BigUser_Values> listValues;
           psp_Values.ItemID = ItemID;
       //     psp_Values.ID = typeFlag;//��ID�ֶδ��Flag2��ֵ
            psp_Values.Flag2 = typeFlag;
            listValues = Common.Services.BaseService.GetList<PSP_BigUser_Values>("SelectPSP_BigUser_ValuesListByFlag2", psp_Values);


            foreach (PSP_BigUser_Values value in listValues)
            {

                TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
                if (node != null)
                {
                    
                     node.SetValue(value.Year+"��",Math.Round(value.Value,2));
               //     SetRateYearValue(node, Math.Round(value.Value,2));
                }
            }
        }

        //�����ݺ�����һ��
        private void AddColumn(string year)
        {
            int i = Convert.ToInt32(year);
            int nInsertIndex = GetInsertIndex(i);
            
            dataTable.Columns.Add(year + "��", typeof(double));

            TreeListColumn column = treeList1.Columns.Insert(nInsertIndex);
          //  TreeListColumn column = treeList1.Columns.Add();
            column.FieldName = year + "��";
            column.Tag = year;
            column.Caption = year + "��";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = nInsertIndex;//- 2;//������������
      //      column.VisibleIndex = nInsertIndex;
            column.ColumnEdit = repositoryItemTextEdit1;
            //treeList1.RefreshDataSource();
            //treeList1.Refresh();
        }

        private int GetInsertIndex(int year)
        {
            int nFixedColumns = typeof(PSP_BigUser_Types).GetProperties().Length ;//ID��ParentID�в���treeList1.Columns��
            int nColumns = treeList1.Columns.Count;
            int nIndex = 1;

            //��û�����
            if (nColumns == nFixedColumns-1)
            {
            }
            else//�Ѿ������
            {
                bool bFind = false;

                //���Ҵ����λ��
                for (int i = 2; i <= nColumns - nFixedColumns+1; i++)
                {
                    //Tag������
                    int tagYear1 = Convert.ToInt32(treeList1.Columns[i - 1].Tag);
                    int tagYear2 = Convert.ToInt32(treeList1.Columns[i].Tag);
                    if (tagYear1 < (year) && tagYear2 > year)
                    {
                        nIndex = i;
                        bFind = true;
                        break;
                    }
                }

                //����������С֮��
                if (!bFind)
                {
                    int tagYear1 = Convert.ToInt32(treeList1.Columns[1].Tag);
                  //  int tagYear2 = (int)(treeList1.Columns[nColumns - 1].Tag);

                    if (tagYear1 > (year))//�ȵ�һ�����С
                    {
                        nIndex = 1;
                    }
                    else
                    {
                        nIndex = nColumns - nFixedColumns +2;
                    }
                }
            }
            
            return nIndex;
        }
        

        /// <summary>
        /// �����·�
        /// </summary>
        /// <param name="yearmonth">����####��##��</param>
        /// <returns></returns>
        private int YearMonthTOMonth(object yearmonth)
        {
            try
            {
            //    return Convert.ToInt32(yearmonth.ToString().Remove(yearmonth.ToString().IndexOf("��")).Substring(yearmonth.ToString().IndexOf("��") + 1));
                return Convert.ToInt32(yearmonth.ToString().Remove(yearmonth.ToString().IndexOf("��")));
            }
            catch
            {
                return 0;
            }
        }
      /// <summary>
      /// �������
      /// </summary>
        /// <param name="yearmonth">����####��##��</param>
      /// <returns></returns>
        private int YearMonthTOYear(object yearmonth)
        {
            

                return Convert.ToInt32(yearmonth);
          
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            

        

            if (!AddRight)
            {
                MsgBox.Show("��û��Ȩ�޽��д��������");
                return;
            }


            FormTypeTitleRemark frm = new FormTypeTitleRemark();
            frm.Text = "���Ӵ��û�";
           
            if (frm.ShowDialog() == DialogResult.OK)
            {
                PSP_BigUser_Types psp_Type = new PSP_BigUser_Types();
                PSP_BigUser_Types psp_Type2 = new PSP_BigUser_Types();
                psp_Type.Title = frm.TypeTitle;
                psp_Type.S1 = frm.TypeRemark;
               
                psp_Type.ItemID = ItemID;
                
                if (Common.Services.BaseService.GetObject("SelectPSP_BigUser_TypesByTitleItemID", psp_Type) == null)
                {
                    try
                    {
                        psp_Type.S2 = typeFlag.ToString();
                        Common.Services.BaseService.Create("InsertPSP_BigUser_Types", psp_Type);
                        psp_Type2 = (PSP_BigUser_Types)Common.Services.BaseService.GetObject("SelectPSP_BigUser_TypesByTitleItemID", psp_Type);
                        psp_Type.ID = psp_Type2.ID;
                        dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("���Ӵ��û�����" + ex.Message);
                    }
                }
                else
                {
                    psp_Type2 =(PSP_BigUser_Types)Common.Services.BaseService.GetObject("SelectPSP_BigUser_TypesByTitleItemID", psp_Type);
                    if(psp_Type2.S2.Contains(typeFlag.ToString()))
                    {
                          MsgBox.Show("�������û��Ѿ�����");
                            return;
                    }
                    else
                    {
                        
                        psp_Type.ID = psp_Type2.ID;
                        if (psp_Type2.S2 != "")
                            psp_Type.S2 = psp_Type2.S2 + "," + typeFlag.ToString();
                        Common.Services.BaseService.Update<PSP_BigUser_Types>(psp_Type);
                        dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));
                    }
                }

            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }

            if (!EditRight)
            {
                MsgBox.Show("��û��Ȩ�޽��д��������");
                return;
            }

            FormTypeTitleRemark frm = new FormTypeTitleRemark();
            frm.TypeTitle = treeList1.FocusedNode.GetValue("Title").ToString();
            if(treeList1.FocusedNode.GetValue("S1")!=DBNull.Value)
            frm.TypeRemark = treeList1.FocusedNode.GetValue("S1").ToString();
            frm.Text = "�޸ķ�����";
            PSP_BigUser_Types psp_Type = new PSP_BigUser_Types();
            psp_Type.S1 = frm.TypeRemark;
            psp_Type.Title = frm.TypeTitle;
            psp_Type.ItemID = ItemID;
            psp_Type.S2 = Convert.ToString(treeList1.FocusedNode.GetValue("S2"));
            psp_Type.ID =Convert.ToInt32( treeList1.FocusedNode.GetValue("ID"));
            if (frm.ShowDialog() == DialogResult.OK)
            {
              
                psp_Type.Title = frm.TypeTitle;
                psp_Type.S1 = frm.TypeRemark;
                PSP_BigUser_Types psp_Typetemp = new PSP_BigUser_Types();
                psp_Typetemp=(PSP_BigUser_Types)Common.Services.BaseService.GetObject("SelectPSP_BigUser_TypesByTitleItemID", psp_Type);
                if (psp_Typetemp != null)
                    {
                        if (psp_Type.ID != psp_Typetemp.ID)
                        {
                            MsgBox.Show("�������û��Ѿ�����");
                            return;
                        }
                    }
                    try
                    {
                        Common.Services.BaseService.Update<PSP_BigUser_Types>(psp_Type);
                        treeList1.FocusedNode.SetValue("Title", frm.TypeTitle);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("�޸ĳ���" + ex.Message);
                    }
                }
            
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }

            
            if (treeList1.FocusedNode.HasChildren)
            {
                MsgBox.Show("�˷��������ӷ��࣬����ɾ���ӷ��࣡");
                return;
            }

            if (!DeleteRight)
            {
                MsgBox.Show("��û��Ȩ�޽��д��������");
                return;
            }

            if (MsgBox.ShowYesNo("�Ƿ�ɾ������ " + treeList1.FocusedNode.GetValue("Title") + "��") == DialogResult.Yes)
            {
                PSP_BigUser_Types psp_Type = new PSP_BigUser_Types();
                Class1.TreeNodeToDataObject<PSP_BigUser_Types>(psp_Type, treeList1.FocusedNode);
                PSP_BigUser_Values psp_Values = new PSP_BigUser_Values();
                psp_Values.TypeID = psp_Type.ID;
                psp_Values.Flag2 = typeFlag;
                psp_Values.ItemID = ItemID;
                try
                {
                    string[] yearitem = psp_Type.S2.Split(',');
                    if (yearitem.Length == 1)
                       
                        //DeletePSP_ValuesByType����ɾ�����ݺͷ���
                        Common.Services.BaseService.Update("DeletePSP_BigUser_ValuesByType", psp_Values);
                    else
                    {
                        psp_Type.S2 = "";
                        foreach(string strtemp in yearitem)
                        {
                            if (strtemp != typeFlag.ToString())
                            {
                                if (psp_Type.S2 == "")
                                {

                                    psp_Type.S2 = strtemp;
                                }
                                else
                                    psp_Type.S2 += "," + strtemp;
                            }
                        }
                        Common.Services.BaseService.Update<PSP_BigUser_Types>(psp_Type);
                        Common.Services.BaseService.Update("DeletePSP_BigUser_ValuesByType2", psp_Values);
                    }

                    //TreeListNode brotherNode = null;
                    //if (treeList1.FocusedNode.ParentNode.Nodes.Count > 1)
                    //{
                    //    brotherNode = treeList1.FocusedNode.PrevNode;
                    //    if (brotherNode == null)
                    //    {
                    //        brotherNode = treeList1.FocusedNode.NextNode;
                    //    }
                    //}
                    treeList1.DeleteNode(treeList1.FocusedNode);

                    //ɾ�������ͬ�������������࣬�����¼������������������
                    //if (brotherNode != null)
                    //{
                    //    foreach (TreeListColumn column in treeList1.Columns)
                    //    {
                    //        if (column.FieldName.IndexOf("��") > 0)
                    //        {
                    //            CalculateSum(brotherNode, column);
                    //        }
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    MsgBox.Show("ɾ������" + ex.Message);
                }
            }
        }

        //�������
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!AddRight)
            {
                MsgBox.Show("��û��Ȩ�޽��д��������");
                return;
            }

            FormNewMonth frm = new FormNewMonth();
            frm.Flag = typeFlag;
            frm.ItemID = ItemID;
            int nFixedColumns = typeof(PSP_BigUser_Types).GetProperties().Length;
            int nColumns = treeList1.Columns.Count;
            if (nFixedColumns == nColumns + 1)//���ʱ����ʾ��û����ݣ������Ĭ��Ϊ��ǰ���ȥ15��
            {
                frm.YearValue = DateTime.Now.Year;
                frm.MonthValue = DateTime.Now.Month;
            }
            else
            {
                //�����ʱ��Ĭ��Ϊ�����ݼ�1��
                frm.YearValue =Convert.ToInt32 (treeList1.Columns[nColumns -nFixedColumns+1].Tag);
                frm.MonthValue = Convert.ToInt32(treeList1.Columns[nColumns - nFixedColumns + 1].Tag) + 1;
            }

            if (frm.ShowDialog() == DialogResult.OK)
            {
                AddColumn(frm.MonthValue.ToString());
              //  LoadValues(false);
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string baseyear = "";
       //     baseyear = EnsureBaseYear(baseyear);
            if (treeList1.FocusedColumn == null)
            {
                return;
            }

            //���������
            if (treeList1.FocusedColumn.FieldName.IndexOf("��") == -1)
            {
                return;
            }
            if (treeList1.FocusedColumn.FieldName.Contains(baseyear) && baseyear != "")
            {
                return;
            }
            if (!DeleteRight)
            {
                MsgBox.Show("��û��Ȩ�޽��д��������");
                return;
            }

            if (MsgBox.ShowYesNo("�Ƿ�ɾ�� " + treeList1.FocusedColumn.FieldName + " �����е��������ݣ�") != DialogResult.Yes)
            {
                return;
            }

            PSP_BigUser_Values psp_Values = new PSP_BigUser_Values();
            psp_Values.ID = typeFlag;//����ID���Դ��Flag2
            psp_Values.Year = (string)treeList1.FocusedColumn.Tag;
            psp_Values.ItemID = ItemID;
            psp_Values.Flag2 = typeFlag;
            try
            {
                //DeletePSP_ValuesByYearɾ�����ݺ����
                int colIndex = treeList1.FocusedColumn.AbsoluteIndex;
                Common.Services.BaseService.Update("DeletePSP_BigUser_ValuesByYear", psp_Values);
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
                MsgBox.Show("ɾ������" + ex.Message);
            }
        }

        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (treeList1.FocusedColumn.Caption.IndexOf("��") > 0)
            if ((e.Value.ToString() != lastEditValue
                || lastEditNode != e.Node
                || lastEditColumn != e.Column)
                && e.Column.FieldName.IndexOf("��") > 0
                && e.Column.Tag != null)
            {
               
                if (SaveCellValue(treeList1.FocusedColumn.Caption.Replace("��",""), (int)treeList1.FocusedNode.GetValue("ID"),Math.Round(Convert.ToDouble(e.Value),2)))
                {
                   
                }
                else
                {
                    treeList1.FocusedNode.SetValue(treeList1.FocusedColumn.FieldName, lastEditValue);
                }
            }
        }

        private bool SaveCellValue(string year, int typeID, double value)
        {
            PSP_BigUser_Values psp_values = new PSP_BigUser_Values();
           
            psp_values.Value = value;
            psp_values.Year = year;
            psp_values.ItemID = ItemID;
            psp_values.TypeID = typeID;
            psp_values.Flag2 = typeFlag;
            try
            {
                Common.Services.BaseService.Update<PSP_BigUser_Values>(psp_values);
            }
            catch (Exception ex)
            {
                MsgBox.Show("�������ݳ���" + ex.Message);
                return false;
            }
            return true;
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            string baseyear = "";
      //      baseyear = EnsureBaseYear(baseyear);
            if (treeList1.FocusedNode.HasChildren
                || !EditRight || (treeList1.FocusedColumn.Tag.ToString() != baseyear && baseyear != ""))
            {
                e.Cancel = true;
            }
        }

        //���ӷ������ݸı�ʱ�������丸�����ֵ
        private void CalculateSum(TreeListNode node, TreeListColumn column)
        {
            //TreeListNode parentNode = node.ParentNode;

            //if (parentNode == null)
            //{
            //    return;
            //}

            //double sum = 0;
            //foreach (TreeListNode nd in parentNode.Nodes)
            //{
            //    object value = nd.GetValue(column.FieldName);
            //    if (value != null && value != DBNull.Value)
            //    {
            //        sum += Convert.ToDouble(value);
            //    }
            //}

            //parentNode.SetValue(column.FieldName,Math.Round(sum,2));
            //SetRateYearValue(parentNode, sum);
            //if (!SaveCellValue((int)column.Tag, (int)parentNode.GetValue("ID"), sum))
            //{
            //    return;
            //}

            //CalculateSum(parentNode, column);
        }

        private void treeList1_ShownEditor(object sender, EventArgs e)
        {
            lastEditColumn = treeList1.FocusedColumn;
            lastEditNode = treeList1.FocusedNode;
            lastEditValue = treeList1.FocusedNode.GetValue(lastEditColumn.FieldName).ToString();
        }

        //ͳ��
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          //  FormChooseYears frm = new FormChooseYears();
            //foreach (TreeListColumn column in treeList1.Columns)
            //{
            //    if (column.FieldName.IndexOf("��") > 0)
            //    {
            //        frm.ListYearsForChoose.Add((int)column.Tag);
            //    }
            //}

            //if (frm.ShowDialog() == DialogResult.OK)
            //{
               
            //}

            WaitDialogForm wait = new WaitDialogForm("", "���ڼ�������, ���Ժ�...");
            this.Cursor = Cursors.WaitCursor;
            DataTable dt = new DataTable();
            dt = ConvertBigUserDataTable(dt,13);
            FrmBigUserPrint frma = new FrmBigUserPrint();
            frma.IsSelect = _isSelect;

            frma.Text =typeFlag+ "����û�����ͳ��(��ǧ��ʱ)";
            frma.GridDataTable = dt;
            frma.ISPrint = PrintRight;
            this.Cursor = Cursors.Default;
            wait.Close();
            if (frma.ShowDialog() == DialogResult.OK && _isSelect)
            {

                DialogResult = DialogResult.OK;
            }
        }
        private DataTable ConvertBigUserDataTable(DataTable dt,int imanth )
        {

            int i = 0;
            PSP_BigUser_Values bigvalue = new PSP_BigUser_Values();
            bigvalue.Flag2 = typeFlag - 1;
            bigvalue.ItemID = ItemID;
            IList<PSP_BigUser_Values> listValues;
            listValues = Common.Services.BaseService.GetList<PSP_BigUser_Values>("SelectPSP_BigUser_ValuesListByFlag2", bigvalue);

            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("SortID", typeof(int));
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("PartID", typeof(string));

           
            dt.Columns.Add("��ע", typeof(string));
            if (imanth > 13 && imanth < 26)
            {
                dt.Columns.Add("ȥ���ۼ�", typeof(string));
                dt.Columns.Add("ȥ��" + (imanth - 13) + "��", typeof(double));
            }
            for (i = 1; i <= 12; i++)
            {

                dt.Columns.Add(i + "��", typeof(string));
            }

            dt.Columns.Add("��������", typeof(double));
            DataCommon dsort = new DataCommon();
            DataTable dataTable2 = dsort.GetSortTable(dataTable, "ID", true);

            #region ���Ҫ��ʾ��Row
            int j = 1;
            int mintex = 0;
            double sum = 0;
            double sumtemp = 0;

            double[] bigusersum = new double[12];
            for (i = 0; i < dataTable2.Rows.Count + 1; i++)
            {
                DataRow dr = dt.NewRow();
                if (i < dataTable2.Rows.Count)
                    dr["Title"] = dataTable2.Rows[i]["Title"];
                else
                    dr["Title"] = "�󻧺ϼ�";
                sum = 0;
                sumtemp = 0;
                if (i < dataTable2.Rows.Count)
                {
                    for (mintex = 1; mintex <= 12; mintex++)
                    {
                        if (dataTable.Columns.Contains(mintex + "��"))
                        {
                            if (dataTable2.Rows[i][mintex + "��"] != null && dataTable2.Rows[i][mintex + "��"] != DBNull.Value)
                                sumtemp = Math.Round(Convert.ToDouble(dataTable2.Rows[i][mintex + "��"]) / 10000, 4);
                            else
                                sumtemp = 0;
                            dr[mintex + "��"] = sumtemp;
                        }
                        else
                        {
                            dr[mintex + "��"] = 0;
                            sumtemp = 0;
                        }
                        bigusersum[mintex - 1] += sumtemp;
                        sum += sumtemp;

                    }
                }
                else
                {
                    for (int imtemp = 1; imtemp <= 12; imtemp++)
                    {

                        dr[imtemp + "��"] = bigusersum[imtemp - 1];
                        sum += bigusersum[imtemp - 1];


                        bigusersum[imtemp - 1] = -1;
                    }
                }
                if (i < dataTable2.Rows.Count)
                dr["��ע"] = dataTable2.Rows[i]["S1"];
                dr["��������"] = sum;
                dr["SortID"] = j++;
                dr["PartID"] = i + 1;
                if (i < dataTable2.Rows.Count)
                    dr["ID"] = dataTable2.Rows[i]["ID"];
                else
                    dr["ID"] = 999999999;
                dt.Rows.Add(dr);
                dr = dt.NewRow();
                dr["Title"] = "�ۼ�";
                dr["SortID"] = j++;
                dr["PartID"] = i + 1;
                if (i < dataTable2.Rows.Count)
                    dr["ID"] = dataTable2.Rows[i]["ID"];
                else
                    dr["ID"] = 999999999;
                dt.Rows.Add(dr);
                dr = dt.NewRow();
                dr["Title"] = "��������";
                dr["SortID"] = j++;
                dr["PartID"] = i + 1;
                if (i < dataTable2.Rows.Count)
                    dr["ID"] = dataTable2.Rows[i]["ID"];
                else
                    dr["ID"] = 999999999;
                dt.Rows.Add(dr);
                dr = dt.NewRow();
                dr["Title"] = "�ۼ�����";
                dr["SortID"] = j++;
                dr["PartID"] = i + 1;
                if (i < dataTable2.Rows.Count)
                    dr["ID"] = dataTable2.Rows[i]["ID"];
                else
                    dr["ID"] = 999999999;
                dt.Rows.Add(dr);
            }

            #endregion
            #region ���Ҫ��ʾ��Value
            i = 0;
            j = 0;
            mintex = 0;
            sum = 0;
            sumtemp = 0;
            int yearmonth = 1;
            bool isHaveHisValue = true;
            double qnlji = -1;
            double qndyue = -1;
            for (j = 0; j < dt.Rows.Count; j += 4)
            {
                double ljitemp = -1;

                for (yearmonth = 1; yearmonth <= 12; yearmonth++)
                {
                    if (imanth < yearmonth)
                        break ;
                    isHaveHisValue = true;
                    if (i < listValues.Count)
                    {
                        if (listValues[i].TypeID.ToString() != dt.Rows[j]["ID"].ToString())
                        {
                            if (Convert.ToInt32(dt.Rows[j]["ID"]) != 999999999)
                            ljitemp = -1;
                            if (Convert.ToInt32(dt.Rows[j]["ID"]) == 999999999)
                                isHaveHisValue = false;
                            else if (listValues[i].TypeID > Convert.ToInt32(dt.Rows[j]["ID"]))
                                isHaveHisValue = false;
                            else if (listValues[i].TypeID < Convert.ToInt32(dt.Rows[j]["ID"]))
                            {
                                while (listValues[i].TypeID < Convert.ToInt32(dt.Rows[j]["ID"]))
                                {
                                    if (i >= listValues.Count - 1)
                                        break;
                                    i++;

                                }

                                if (listValues[i].TypeID > Convert.ToInt32(dt.Rows[j]["ID"]))
                                    isHaveHisValue = false;
                            }
                        }
                    }
                    else
                        isHaveHisValue = false;
                    if (isHaveHisValue)
                    {
                        if (listValues[i].Year != yearmonth.ToString())
                            isHaveHisValue = false;
                    }



                    if (dt.Rows[j][yearmonth + "��"] != null && dt.Rows[j][yearmonth + "��"] != DBNull.Value)
                        sum = Convert.ToDouble(dt.Rows[j][yearmonth + "��"]);
                    else
                        sum = 0;

                    switch (yearmonth)
                    {
                        case 1:

                            dt.Rows[j + 1][yearmonth + "��"] = Math.Round((sum), 0);
                            if (isHaveHisValue)
                            {
                                listValues[i].Value = Math.Round(listValues[i].Value / 10000, 4);
                                if (imanth > 13 && imanth < 26)
                                {
                                    if (dt.Rows[j]["ȥ���ۼ�"] != null && dt.Rows[j]["ȥ���ۼ�"] != DBNull.Value)
                                        dt.Rows[j]["ȥ���ۼ�"] = Convert.ToDouble(dt.Rows[j]["ȥ���ۼ�"]) + listValues[i].Value;
                                    else
                                        dt.Rows[j]["ȥ���ۼ�"] = listValues[i].Value;
                                }
                               
                                if (imanth == 14)
                                {
                                    dt.Rows[j]["ȥ��" + listValues[i].Year + "��"] = listValues[i].Value;
                                    if (qndyue==-1)
                                       qndyue= listValues[i].Value;
                                    else
                                       qndyue += listValues[i].Value;
                                }
                               
                                sumtemp = Math.Round(listValues[i].Value, 0);
                                dt.Rows[j + 2][listValues[i].Year + "��"] = Math.Round((sum - listValues[i].Value) * 100 / listValues[i].Value, 2) + "%";
                                sum = Convert.ToDouble(dt.Rows[j + 1][listValues[i].Year + "��"]);
                                dt.Rows[j + 3][listValues[i].Year + "��"] = Math.Round((sum - sumtemp) * 100 / sumtemp, 2) + "%";
                                ljitemp = sumtemp;
                                if (bigusersum[yearmonth - 1] == -1)
                                    bigusersum[yearmonth - 1] = 0;
                                bigusersum[yearmonth - 1] += listValues[i].Value;

                            }
                            if (Convert.ToInt32(dt.Rows[j]["ID"]) == 999999999)
                            {
                                if (bigusersum[yearmonth - 1] != -1)
                                {

                                    if (ljitemp == -1)
                                        ljitemp = 0;
                                    sumtemp = ljitemp + Math.Round(bigusersum[yearmonth - 1], 0);
                                    dt.Rows[j + 2][yearmonth + "��"] = Math.Round((sum - bigusersum[yearmonth - 1]) * 100 / bigusersum[yearmonth - 1], 2) + "%";
                                    sum = Convert.ToDouble(dt.Rows[j + 1][yearmonth + "��"]);
                                    dt.Rows[j + 3][yearmonth + "��"] = Math.Round((sum - sumtemp) * 100 / sumtemp, 2) + "%";
                                    ljitemp = sumtemp;
                                    //   bigusersum[yearmonth - 1] += sumtemp;
                                }
                                if (imanth == 14)
                                {
                                    if (qnlji!=-1)
                                        dt.Rows[j]["ȥ���ۼ�"] = qnlji;
                                    else
                                        dt.Rows[j]["ȥ���ۼ�"] = 0;

                                   
                                }
                            }
                            break;

                        default:

                            if (dt.Rows[j + 1][yearmonth - 1 + "��"] != null && dt.Rows[j + 1][yearmonth - 1 + "��"] != DBNull.Value)
                                sumtemp = Convert.ToDouble(dt.Rows[j + 1][yearmonth - 1 + "��"]);
                            else
                                sumtemp = 0;

                            dt.Rows[j + 1][yearmonth + "��"] = sumtemp + Math.Round((sum), 0);

                            if (isHaveHisValue)
                            {
                                listValues[i].Value = Math.Round(listValues[i].Value / 10000, 4);
                                if (imanth > 13 && imanth < 26)
                                {
                                    if (dt.Rows[j]["ȥ���ۼ�"] != null && dt.Rows[j]["ȥ���ۼ�"] != DBNull.Value)
                                        dt.Rows[j]["ȥ���ۼ�"] = Convert.ToDouble(dt.Rows[j]["ȥ���ۼ�"]) + listValues[i].Value;
                                    else
                                        dt.Rows[j]["ȥ���ۼ�"] = listValues[i].Value;
                                }
                               
                                if (imanth > 13 && imanth < 26)
                                    if (listValues[i].Year == (imanth - 13) + "")
                                    {
                                        dt.Rows[j]["ȥ��" + listValues[i].Year + "��"] = listValues[i].Value;
                                        if (qndyue == -1)
                                            qndyue = 0;
                                        qndyue += listValues[i].Value;
                                       
                                    }
                               
                                dt.Rows[j + 2][listValues[i].Year + "��"] = Math.Round((sum - listValues[i].Value) * 100 / listValues[i].Value, 2) + "%";
                                sum = Convert.ToDouble(dt.Rows[j + 1][listValues[i].Year + "��"]);
                                sumtemp = ljitemp + Math.Round((listValues[i].Value), 0);
                                ljitemp = sumtemp;
                                if (bigusersum[yearmonth - 1] == -1)
                                    bigusersum[yearmonth - 1] = 0;
                                bigusersum[yearmonth - 1] += (listValues[i].Value);
                                dt.Rows[j + 3][listValues[i].Year + "��"] = Math.Round((sum - sumtemp) * 100 / sumtemp, 2) + "%";
                            }
                            if (Convert.ToInt32(dt.Rows[j]["ID"]) == 999999999)
                            {
                                if (bigusersum[yearmonth - 1] != -1)
                                {

                                    if (ljitemp == -1)
                                        ljitemp = 0;
                                    sumtemp = ljitemp + Math.Round(bigusersum[yearmonth - 1], 0);
                                    dt.Rows[j + 2][yearmonth + "��"] = Math.Round((sum - bigusersum[yearmonth - 1]) * 100 / bigusersum[yearmonth - 1], 2) + "%";
                                    sum = Convert.ToDouble(dt.Rows[j + 1][yearmonth + "��"]);
                                    dt.Rows[j + 3][yearmonth + "��"] = Math.Round((sum - sumtemp) * 100 / sumtemp, 2) + "%";
                                    ljitemp = sumtemp;
                                    //bigusersum[yearmonth - 1] += sumtemp;
                                }
                                if (imanth > 13 && imanth < 26)
                                {
                                    if (qnlji != -1)
                                        dt.Rows[j]["ȥ���ۼ�"] = qnlji;
                                    else
                                        dt.Rows[j]["ȥ���ۼ�"] = 0;

                                    if (qndyue != -1)
                                        dt.Rows[j]["ȥ��" + (imanth-13) + "��"] = qndyue;
                                    else
                                        dt.Rows[j]["ȥ��" + (imanth - 13) + "��"] = 0;
                                }
                               
                            }
                            if (yearmonth == 12)
                            {
                                if (ljitemp != -1)
                                    dt.Rows[j + 1]["��������"] = Convert.ToDouble(dt.Rows[j + 1][yearmonth + "��"]) - ljitemp;
                                if (imanth > 13 && imanth < 26)
                                {
                                    if (qnlji == -1)
                                    {
                                        if (dt.Rows[j]["ȥ���ۼ�"] != null && dt.Rows[j]["ȥ���ۼ�"] != DBNull.Value)
                                            qnlji = Convert.ToDouble(dt.Rows[j]["ȥ���ۼ�"]);
                                    }
                                    else
                                    {
                                        if (dt.Rows[j]["ȥ���ۼ�"] != null && dt.Rows[j]["ȥ���ۼ�"] != DBNull.Value)
                                            qnlji += Convert.ToDouble(dt.Rows[j]["ȥ���ۼ�"]);
                                    }
                                }
                              

                            }
                            break;
                    }
                    if (isHaveHisValue) i++;

                }
            }
            #endregion
            return dt;
        
        
        }

        //�����ؼ����ݰ���ʾ˳�����ɵ�DataTable��
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
            dt.Columns["Title"].Caption = "����";
            foreach (TreeListColumn column in xTreeList.Columns)
            {
                if (column.FieldName.IndexOf("��") > 0)
                {
                    listColID.Add(column.FieldName);
                    dt.Columns.Add(column.FieldName, typeof(double));
                }
            }

            foreach (TreeListNode node in xTreeList.Nodes)
            {
                AddNodeDataToDataTable(dt, node, listColID);
            }

            return dt;
        }

        private void AddNodeDataToDataTable(DataTable dt, TreeListNode node, List<string> listColID)
        {
            DataRow newRow = dt.NewRow();
            foreach (string colID in listColID)
            {
                //���������ڶ��㼰�Ժ���ǰ��ӿո�
                if (colID == "Title" && node.ParentNode != null)
                {
                    newRow[colID] = "����" + node[colID];
                }
                else
                {
                    newRow[colID] = node[colID];
                }
            }

            //�����������ӿ���
            if (node.ParentNode == null && dt.Rows.Count > 0)
            {
                dt.Rows.Add(new object[] { });
            }

            dt.Rows.Add(newRow);

            foreach (TreeListNode nd in node.Nodes)
            {
                AddNodeDataToDataTable(dt, nd, listColID);
            }
        }

        //����ѡ���ͳ����ݣ�����ͳ�ƽ�����ݱ�
        private DataTable ResultDataTable(DataTable sourceDataTable, List<ChoosedYears> listChoosedYears)
        {
            DataTable dtReturn = new DataTable();
            dtReturn.Columns.Add("Title", typeof(string));
            foreach (ChoosedYears choosedYear in listChoosedYears)
            {
                dtReturn.Columns.Add(choosedYear.Year + "��", typeof(double));
                if (choosedYear.WithIncreaseRate)
                {
                    dtReturn.Columns.Add(choosedYear.Year + "������", typeof(double)).Caption = "������";
                }
            }

            int nRowTotalPower = 0;//ȫ����GDP������
            int nPopulation = 0;//�˿�������

            #region ������ݣ���ȡȫ����GDP�����С��˿�������
            for (int i = 0; i < sourceDataTable.Rows.Count; i++)
            {
                DataRow newRow = dtReturn.NewRow();
                DataRow sourceRow = sourceDataTable.Rows[i];
                foreach (DataColumn column in dtReturn.Columns)
                {
                    if (column.Caption != "������")
                    {
                        newRow[column.ColumnName] = sourceRow[column.ColumnName];
                    }
                }
                dtReturn.Rows.Add(newRow);

                if (sourceRow["Flag"] != DBNull.Value && (int)sourceRow["ParentID"] == 0)
                {
                    switch ((int)sourceRow["Flag"])
                    {
                        case 41://ȫ����GDP
                            nRowTotalPower = i;
                            break;


                        case 43://���˿�
                            nPopulation = i;
                            dtReturn.Rows.Add(new object[] { "�˾�GDP(Ԫ/��)" });
                            break;

                        default:
                            break;
                    }
                }
            }
            #endregion

            #region �˾�GDP
            foreach (ChoosedYears choosedYear in listChoosedYears)
            {
                object numerator = dtReturn.Rows[nRowTotalPower][choosedYear.Year + "��"];
                object denominator = dtReturn.Rows[nPopulation][choosedYear.Year + "��"];
                if (numerator != DBNull.Value && denominator != DBNull.Value)
                {
                    try
                    {
                        dtReturn.Rows[nPopulation + 1][choosedYear.Year + "��"] = System.Math.Round((double)numerator / (double)denominator, 3);
                    }
                    catch
                    {
                    }
                }
            }
            #endregion

            #region ����������
            DataColumn columnBegin = null;
            foreach (DataColumn column in dtReturn.Columns)
            {
                if (column.ColumnName.IndexOf("��") > 0)
                {
                    if (columnBegin == null)
                    {
                        columnBegin = column;
                    }
                }
                else if (column.ColumnName.IndexOf("������") > 0)
                {
                    DataColumn columnEnd = dtReturn.Columns[column.Ordinal - 1];

                    foreach (DataRow row in dtReturn.Rows)
                    {
                        object numerator = row[columnEnd];
                        object denominator = row[columnBegin];

                        if (numerator != DBNull.Value && denominator != DBNull.Value)
                        {
                            try
                            {
                                int intervalYears = Convert.ToInt32(columnEnd.ColumnName.Replace("��", ""))
                                    - Convert.ToInt32(columnBegin.ColumnName.Replace("��", ""));
                                row[column] = Math.Round(Math.Pow((double)numerator / (double)denominator, 1.0 / intervalYears) - 1, 4);
                            }
                            catch
                            {
                            }
                        }
                    }

                    //���μ��������ʵ�����Ϊ�´ε���ʼ��
                    columnBegin = columnEnd;
                }
            }
            #endregion

            return dtReturn;
        }

        //����������
        private void CalIncreaseRate(DataTable dt)
        {

        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

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
            FrmBigUserCurrentMonth frm = new FrmBigUserCurrentMonth();
            frm.Text = "ѡ���·�";
            foreach (TreeListColumn column in treeList1.Columns)
            {
                if (column.FieldName.IndexOf("��") > 0)
                {
                    frm.ListYearsForChoose.Add(Convert.ToInt32(column.Tag));
                }
            }
            if (frm.ListYearsForChoose.Count < 1)
                return;
            if (frm.ShowDialog() == DialogResult.OK)
            {


                WaitDialogForm wait = new WaitDialogForm("", "���ڼ�������, ���Ժ�...");
                this.Cursor = Cursors.WaitCursor;
                int month = frm.Month;
                #region ����column
                DataTable dt = new DataTable();
                dt.Columns.Add("Title", typeof(string));
                dt.Columns.Add("SortID", typeof(int));
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("PartID", typeof(string));

                dt.Columns.Add("���µ���", typeof(double));
                dt.Columns.Add("��������", typeof(string));

                dt.Columns.Add("�ۼƵ���", typeof(double));
                dt.Columns.Add("�ۼ�����", typeof(string));
                dt.Columns.Add("��ע", typeof(string));
                #endregion
                DataTable dttemp = new DataTable();
                //��ȡ����DATATABLE
                dttemp = ConvertBigUserDataTable(dttemp, month);
                #region �������
                int intex = 0;
                for (intex = 0; intex < dttemp.Rows.Count;intex+=4 )
                {
                   
                    DataRow dr = dt.NewRow();
                    dr["Title"] = dttemp.Rows[intex]["Title"];
                    dr["SortID"] = dttemp.Rows[intex]["SortID"];
                    dr["ID"] = dttemp.Rows[intex]["ID"];
                    dr["PartID"] = dttemp.Rows[intex]["PartID"];
                    double i = 0;
                    if (dttemp.Rows[intex][month + "��"] != null && dttemp.Rows[intex][month + "��"] != DBNull.Value)
                        i = Convert.ToDouble(dttemp.Rows[intex][month + "��"]);
                    dr["���µ���"] = dttemp.Rows[intex][month + "��"];
                    if(i!=0)
                    dr["��������"] = dttemp.Rows[intex + 2][month + "��"];
                    else
                    dr["��������"] ="0.00%";
                if (dttemp.Rows[intex+1][month + "��"] != null && dttemp.Rows[intex+1][month + "��"] != DBNull.Value)
                    i = Convert.ToDouble(dttemp.Rows[intex + 1][month + "��"]);
                    dr["�ۼƵ���"] = dttemp.Rows[intex + 1][month + "��"];
                    if (i != 0)
                    dr["�ۼ�����"] = dttemp.Rows[intex + 3][month + "��"];
                    else
                    dr["�ۼ�����"] = "0.00%";
                    dr["��ע"] = dttemp.Rows[intex]["��ע"];
                    dt.Rows.Add(dr);
                }
                #endregion
                FrmBigUserCurrentMonthPrint frmprint = new FrmBigUserCurrentMonthPrint();
             
                frmprint.IsSelect = _isSelect;

                frmprint.Text = typeFlag+"��"+month+"�´󻧵������(��ǧ��ʱ)";
                frmprint.GridDataTable = dt;
                frmprint.ISPrint = PrintRight;
                this.Cursor = Cursors.Default;
                wait.Close();
                if (frmprint.ShowDialog() == DialogResult.OK && _isSelect)
                {

                    DialogResult = DialogResult.OK;
                }

            }

        }

       

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
             FrmBigUserCurrentMonth frm = new FrmBigUserCurrentMonth();
             frm.Text = "ѡ���·�";
            foreach (TreeListColumn column in treeList1.Columns)
            {
                if (column.FieldName.IndexOf("��") > 0)
                {
                    frm.ListYearsForChoose.Add(Convert.ToInt32(column.Tag));
                }
            }
            if (frm.ListYearsForChoose.Count < 1)
                return;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                WaitDialogForm wait = new WaitDialogForm("", "���ڼ�������, ���Ժ�...");
                this.Cursor = Cursors.WaitCursor;
                int month = frm.Month;
                #region ����column
                DataTable dt = new DataTable();
                dt.Columns.Add("Title", typeof(string));
                dt.Columns.Add("SortID", typeof(int));
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("PartID", typeof(string));

                dt.Columns.Add("���걾��", typeof(double));
                dt.Columns.Add("ȥ��ͬ��", typeof(double));
                dt.Columns.Add("ͬ������", typeof(double));

                dt.Columns.Add("����ֹ�ۼ�", typeof(double));
                dt.Columns.Add("ȥ��ֹ�ۼ�", typeof(double));
                dt.Columns.Add("ͬ������2", typeof(double));

                #endregion
                  DataTable dttemp = new DataTable();
                //��ȡ����DATATABLE
                dttemp = ConvertBigUserDataTable(dttemp, 13+month);
                #region �������
                int intex = 0;
                for (intex = 0; intex < dttemp.Rows.Count; intex += 4)
                {
                    DataRow dr = dt.NewRow();
                    dr["Title"] = dttemp.Rows[intex]["Title"];
                    dr["SortID"] = dttemp.Rows[intex]["SortID"];
                    dr["ID"] = dttemp.Rows[intex]["ID"];
                    dr["PartID"] = dttemp.Rows[intex]["PartID"];
                    if (dttemp.Rows[intex][month + "��"] == null || dttemp.Rows[intex][month + "��"] == DBNull.Value)
                        dr["���걾��"] = 0;
                    else
                        dr["���걾��"] = dttemp.Rows[intex][month + "��"];
                    if (dttemp.Rows[intex]["ȥ��" + month + "��"] == null || dttemp.Rows[intex]["ȥ��" + month + "��"] == DBNull.Value)
                        dr["ȥ��ͬ��"] = 0;
                    else
                        dr["ȥ��ͬ��"] = dttemp.Rows[intex ]["ȥ��" + month + "��"];

                    dr["ͬ������"] =Math.Round( Convert.ToDouble(dr["���걾��"]) - Convert.ToDouble(dr["ȥ��ͬ��"]),4);
                    if (dttemp.Rows[intex]["��������"] == null || dttemp.Rows[intex]["��������"] == DBNull.Value)
                        dr["����ֹ�ۼ�"] = 0;
                    else
                        dr["����ֹ�ۼ�"] = dttemp.Rows[intex]["��������"];
                    if (dttemp.Rows[intex]["ȥ���ۼ�"] == null || dttemp.Rows[intex]["ȥ���ۼ�"] == DBNull.Value)
                        dr["ȥ��ֹ�ۼ�"] = 0;
                    else
                        dr["ȥ��ֹ�ۼ�"] = dttemp.Rows[intex]["ȥ���ۼ�"];
                    dr["ͬ������2"] =Math.Round( Convert.ToDouble(dr["����ֹ�ۼ�"]) - Convert.ToDouble(dr["ȥ��ֹ�ۼ�"]),4);
                    dt.Rows.Add(dr);
                }
                #endregion
                FrmBigUserMonthAnalysePrint frmprint = new FrmBigUserMonthAnalysePrint();

                frmprint.IsSelect = _isSelect;

                frmprint.Text = typeFlag + "��" + month + "�´�Ӫҵ�ֵ�������(��ǧ��ʱ)";
                frmprint.GridDataTable = dt;
                frmprint.ISPrint = PrintRight;
                this.Cursor = Cursors.Default;
                wait.Close();
                if (frmprint.ShowDialog() == DialogResult.OK && _isSelect)
                {

                    DialogResult = DialogResult.OK;
                }
            }
            
            
            
        }
        private void InsertSubstation_Info()
        {
           
            string columnname = "";

            try
            {
                DataTable dts = new DataTable();
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Excel�ļ�(*.xls)|*.xls";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    WaitDialogForm wait = new WaitDialogForm("", "���ڵ�������, ���Ժ�...");
                    dts = GetExcel(op.FileName);
                  
                    //DateTime s8 = DateTime.Now;
                    for (int i = 0; i < dts.Rows.Count; i++)
                    {


                        PSP_BigUser_Values l1 = new PSP_BigUser_Values();
                        int treelistid=0;
                        if (dts.Rows[i]["Title"] != null)
                        if(dts.Rows[i]["Title"].ToString()!="")
                        {
                          PSP_BigUser_Types psp_Type = new PSP_BigUser_Types();
                          PSP_BigUser_Types psp_Type2 = new PSP_BigUser_Types();
                            psp_Type.Title = dts.Rows[i]["Title"].ToString();
                            psp_Type.ItemID = ItemID;
                            if (Common.Services.BaseService.GetObject("SelectPSP_BigUser_TypesByTitleItemID", psp_Type) == null)
                            {
                                psp_Type.S2 = typeFlag.ToString();
                                Common.Services.BaseService.Create("InsertPSP_BigUser_Types", psp_Type);
                            }
                            else
                            {
                                psp_Type2 = (PSP_BigUser_Types)Common.Services.BaseService.GetObject("SelectPSP_BigUser_TypesByTitleItemID", psp_Type);
                                if (!psp_Type2.S2.Contains(typeFlag.ToString()))
                               
                                {
                                    psp_Type.ID = psp_Type2.ID;
                                    if (psp_Type2.S2 != "")
                                        psp_Type.S2 = psp_Type2.S2 + "," + typeFlag.ToString();
                                    Common.Services.BaseService.Update<PSP_BigUser_Types>(psp_Type);
                                }
                            }
                            psp_Type = (PSP_BigUser_Types)Common.Services.BaseService.GetObject("SelectPSP_BigUser_TypesByTitleItemID", psp_Type);
                            if(psp_Type!=null)
                            treelistid=psp_Type.ID;
                        }
                        foreach (DataColumn dc in dts.Columns)
                        {
                            columnname = dc.ColumnName;
                            if (dts.Rows[i][dc.ColumnName].ToString() == "")
                                continue;
                             PSP_BigUser_Years biguseryear = new PSP_BigUser_Years();
                             if (columnname != "Title")
                            {
                                 biguseryear.Year =  columnname ;
                                 biguseryear.Flag = typeFlag;
                                 biguseryear.ItemID = ItemID;
                                 if (Common.Services.BaseService.GetObject("SelectPSP_BigUser_YearsByYearFlag", biguseryear) == null)
                                     {
                                         Common.Services.BaseService.Create<PSP_BigUser_Years>(biguseryear); 
                                       //  AddColumn(columnname);
                                      }
                               
                            }
                            switch (dc.ColumnName)
                            {
                                case "Title":
                              
                                    break;
                                default:
                                    
                                    double LL2 = 0;
                                    try
                                    {
                                        LL2 = Convert.ToDouble(dts.Rows[i][dc.ColumnName].ToString());
                                       if (columnname != "Title")
                                     {
                                        l1.Value= LL2;
                                        l1.ItemID=ItemID;
                                        l1.TypeID=treelistid;
                                        l1.Flag2=typeFlag;
                                        l1.Year= columnname;
                                       Common.Services.BaseService.Update<PSP_BigUser_Values>(l1);
                                     }
                                    }
                                    catch { }
                                   
                                    break;
                            }
                        }
                    }
                    wait.Close();
                   

                    //foreach (Substation_Info lwl in lii)
                    //{
                    //    Substation_Info l1 = new Substation_Info();
                    //    //  l1.AreaName = lwl.AreaName;
                    //    l1.Title = lwl.Title;
                    //    l1.Flag = lwl.Flag;
                    //    //l1.L1 = lwl.L1;
                    //    object obj = Services.BaseService.GetObject("SelectSubstation_InfoByTitleFlag", l1);
                    //    if (obj != null)
                    //    {
                    //        lwl.UID = ((Substation_Info)obj).UID;
                    //        lwl.Code = ((Substation_Info)obj).Code;
                    //        Services.BaseService.Update("UpdateSubstation_InfoAreaName", lwl);
                    //    }
                    //    else
                    //    {
                    //        Services.BaseService.Create<Substation_Info>(lwl);
                    //    }
                    //}
                    //this.ctrlSubstation_Info_TongLing1.RefreshData1();
                     wait = new WaitDialogForm("", "���ڼ�������, ���Ժ�...");
                    this.Cursor = Cursors.WaitCursor;
                    LoadData();
                    this.Cursor = Cursors.Default;
                    wait.Close();
                }
            }
            catch (Exception ex) { MsgBox.Show(columnname + ex.Message); MsgBox.Show("�����ʽ����ȷ��"); }
        }


        private DataTable GetExcel(string filepach)
        {
            string str;
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
            int sheetcount = 0;
            if (fpSpread1.Sheets.Count > 1)
                sheetcount = 1;
            for (int k = 0; k <= fpSpread1.Sheets[sheetcount].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++)
            {
                bool bl = false;
                try
                {

                    foreach (TreeListColumn tlc in treeList1.Columns)
                    {
                        if (tlc.Caption == fpSpread1.Sheets[sheetcount].Cells[2, k].Text)
                        {
                            if (!h1.Contains(tlc.Caption))
                            {
                                dt.Columns.Add(tlc.FieldName);

                                h1.Add(tlc.Caption, tlc.FieldName);
                                //aa++;
                            }
                        }
                        else
                            if (fpSpread1.Sheets[sheetcount].Cells[2, k].Text.IndexOf("��")>0)
                            {
                                try
                                {
                                    int maonth = Convert.ToInt32(fpSpread1.Sheets[sheetcount].Cells[2, k].Text.Replace("��",""));
                                    if (maonth>0&&maonth<13)
                                        if (!h1.Contains(maonth))
                                    {
                                        dt.Columns.Add(maonth.ToString());
                                        h1.Add(fpSpread1.Sheets[sheetcount].Cells[2, k].Text, maonth);
                                        //aa++;
                                    }
                                }
                                catch { }
                            }
                    }
                }
                catch (Exception e)
                { //MessageBox.Show(e.Message+" "+gridcolumn); 
                }
            }



            int m = 3;
            for (int i = m; i < fpSpread1.Sheets[sheetcount].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data) - 4; i++)
            {
                try
                {
                    
                    DataRow dr = dt.NewRow();
                    str = "";
                    for (int j = 0; j < fpSpread1.Sheets[sheetcount].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data); j++)
                //    for (int j = 0; j < h1.Count; j++)
                    {
                        str = str + fpSpread1.Sheets[sheetcount].Cells[i, j].Text;
                        if (h1[fpSpread1.Sheets[sheetcount].Cells[2, j].Text] != null)
                            if(i%4==3)
                                dr[h1[fpSpread1.Sheets[sheetcount].Cells[2, j].Text].ToString()] = fpSpread1.Sheets[sheetcount].Cells[i, j].Text;
                    }
                    if (str != "")
                        dt.Rows.Add(dr);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }

            }
            return dt;

            ////////int m = 3;
            ////////for (int i = m; i < fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data); i++)
            ////////{
            ////////    DataRow dr = dt.NewRow();
            ////////    str = "";
            ////////    for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data)+1 ; j++)
            ////////    {
            ////////        str = str + fpSpread1.Sheets[0].Cells[i, j].Text;
            ////////        dr[h1[j.ToString()].ToString()] = fpSpread1.Sheets[0].Cells[i, j].Text;
            ////////    }
            ////////    if (str != "")
            ////////        dt.Rows.Add(dr);

            ////////}
            ////////return dt;
        }
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertSubstation_Info();
          
        }

      

        //private void SetRateYearValue(TreeListNode node, double sum)
        //{
        //    string baseyear = "";
        ////    baseyear = EnsureBaseYear(baseyear);
        //    if (baseyear != "")
        //    {
        //        double rate = 0;
        //        double sumtemp = sum;
        //        int yeartemp = 0;
        //        PSP_Years psp_Year = new PSP_Years();
        //        psp_Year.Flag = typeFlag;
        //        psp_Year.Year = Convert.ToInt32(baseyear);
        //        IList<PSP_Years> listYears = Common.Services.BaseService.GetList<PSP_Years>("SelectPSP_YearsListByFlagAndYear", psp_Year);
        //        yeartemp = Convert.ToInt32(baseyear);


        //        foreach (PSP_Years yearvalue in listYears)
        //        {

        //            if (yearvalue.Year == Convert.ToInt32(baseyear)) continue;
        //            try
        //            {
        //                PSP_BaseYearRate yearvaluetemp = new PSP_BaseYearRate();
        //                yearvaluetemp.BaseYear = yearvalue.Year.ToString();
        //                yearvaluetemp.TypeID =Convert.ToInt32(node.GetValue("ID"));
        //                IList<PSP_BaseYearRate> list1 = Common.Services.BaseService.GetList<PSP_BaseYearRate>("SelectPSP_BaseYearRateByYear", yearvaluetemp);
                   
        //                if (list1.Count == 1)
        //            {
        //                rate = Convert.ToDouble(list1[0].YearRate);
        //            }
        //            sumtemp = sumtemp * Math.Pow(1 + rate, yearvalue.Year - yeartemp);
                    
        //            node.SetValue(yearvalue.Year + "��", Math.Round(sumtemp, 2));
        //            yeartemp = yearvalue.Year;
        //            SaveCellValue((int)yearvalue.Year, (int)node.GetValue("ID"), Convert.ToDouble(sumtemp));

        //         }
        //        catch { }
        //        }

        //    }

        //}
    }
}