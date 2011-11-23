using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using Itop.Domain.GM;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using System.Collections;
using Itop.Domain.HistoryValue;
using DevExpress.XtraTreeList;
using Itop.Client.Common;
using Itop.Common;

namespace Itop.Client.Chen
{
    public partial class Form1_FT : Itop.Client.Base.FormBase
    {
        private TreeListNode lastEditNode = null;
        private TreeListColumn lastEditColumn = null;
        private string lastEditValue = string.Empty;
        DataTable dataTable;


        private int typeFlag2 = 1;
        private string type = "";

        public int TypeFlag2
        {
            get { return typeFlag2; }
            set { typeFlag2 = value; }
        }


        public string TYPE
        {
            get { return type; }
            set { type = value; }
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

        public void InitForm1_F1()
        {
            type = "1";
            this.Show();
        }

        public void InitForm1_F2()
        {
            type = "2";
            this.Show();
        }



        public Form1_FT()
        {
            InitializeComponent();
        }

        private void Form1_F_Load(object sender, EventArgs e)
        {



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

            PSP_P_Types psp_Type = new PSP_P_Types();
            psp_Type.Flag2 = typeFlag2;
            IList listTypes = Common.Services.BaseService.GetList("SelectPSP_P_TypesByFlag2", psp_Type);

            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_P_Types));

            treeList1.DataSource = dataTable;

            treeList1.Columns["Title"].Caption = "������";
            treeList1.Columns["Title"].Width = 180;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Flag"].VisibleIndex = -1;
            treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["Flag2"].VisibleIndex = -1;
            treeList1.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;

            PSP_P_Years psp_Year = new PSP_P_Years();
            psp_Year.Flag = typeFlag2;
            IList<PSP_P_Years> listYears = Common.Services.BaseService.GetList<PSP_P_Years>("SelectPSP_P_YearsListByFlag", psp_Year);

            Hashtable hashtable= new Hashtable();
            foreach (PSP_P_Years item in listYears)
            {
                AddColumn(item.Year);
                if (!hashtable.ContainsValue(item.Year))
                    hashtable.Add(Guid.NewGuid().ToString(), item.Year);
            }
            Application.DoEvents();

            LoadValues(hashtable);

            treeList1.ExpandAll();
        }

        //��ȡ����
        private void LoadValues(Hashtable hashtable)
        {
            PSP_P_Values PSP_P_Values = new PSP_P_Values();
            PSP_P_Values.ID = typeFlag2;//��ID�ֶδ��Flag2��ֵ

            IList<PSP_P_Values> listValues = Common.Services.BaseService.GetList<PSP_P_Values>("SelectPSP_P_ValuesListByFlag2", PSP_P_Values);

            foreach (PSP_P_Values value in listValues)
            {
                TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
                if (node != null)
                {
                    try
                    {
                        if(hashtable.ContainsValue(value.Year))
                            node.SetValue(value.Year + "��", value.Value);
                    }
                    catch { }
                }
            }
        }

        //�����ݺ�����һ��
        private void AddColumn(int year)
        {
            int nInsertIndex = GetInsertIndex(year);

            dataTable.Columns.Add(year + "��", typeof(double));

            TreeListColumn column = treeList1.Columns.Insert(nInsertIndex);
            column.FieldName = year + "��";
            column.Tag = year;
            column.Caption = year + "��";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = nInsertIndex - 2;//������������
            //column.ColumnEdit = repositoryItemTextEdit1;
            //treeList1.RefreshDataSource();
        }

        private int GetInsertIndex(int year)
        {
            int nFixedColumns = typeof(PSP_P_Types).GetProperties().Length - 2;//ID��ParentID�в���treeList1.Columns��
            int nColumns = treeList1.Columns.Count;
            int nIndex = nFixedColumns;

            //��û�����
            if (nColumns == nFixedColumns)
            {
            }
            else//�Ѿ������
            {
                bool bFind = false;

                //���Ҵ����λ��
                for (int i = nFixedColumns + 1; i < nColumns; i++)
                {
                    //Tag������
                    int tagYear1 = (int)treeList1.Columns[i - 1].Tag;
                    int tagYear2 = (int)treeList1.Columns[i].Tag;
                    if (tagYear1 < year && tagYear2 > year)
                    {
                        nIndex = i;
                        bFind = true;
                        break;
                    }
                }

                //����������С֮��
                if (!bFind)
                {
                    int tagYear1 = (int)treeList1.Columns[nFixedColumns].Tag;
                    int tagYear2 = (int)treeList1.Columns[nColumns - 1].Tag;

                    if (tagYear1 > year)//�ȵ�һ�����С
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

            Form1_Fs ffs = new Form1_Fs();
            ffs.Type = type;
            
            if (ffs.ShowDialog() != DialogResult.OK)
                return;
            int fl2 = ffs.FL2;
            bool bl = ffs.IsJingJi;
            Hashtable hs = ffs.HS;

            if (hs.Count == 0)
                return;

            if (bl)
            {
                fl2 = ffs.Typeflag2;              //�����ʵ�ʵ���ʵ�ʻ��Ƿ�������ʵ��
                PSP_Years py = new PSP_Years();
                py.Flag = fl2;
                IList<PSP_Years> listyear = Services.BaseService.GetList<PSP_Years>("SelectPSP_YearsListByFlag", py);



                Hashtable hsa = new Hashtable();
                foreach (DictionaryEntry de3 in hs)
                {
                    PSP_Types pt = (PSP_Types)de3.Value;

                    PSP_P_Types ppta = new PSP_P_Types();
                    ppta.ID = pt.ID;
                    ppta.Flag2 = typeFlag2;

                    IList<PSP_P_Types> listppt = Services.BaseService.GetList<PSP_P_Types>("SelectPSP_P_TypesByFlag2ID", ppta);
                    if (listppt.Count > 0)
                        continue;

                    PSP_P_Types ppt = new PSP_P_Types();
                    InitTypes(pt, ppt);
                    Services.BaseService.Create<PSP_P_Types>(ppt);

                    hsa.Add(Guid.NewGuid().ToString(), "");

                    PSP_Values pv = new PSP_Values();
                    pv.TypeID = pt.ID;

                    IList<PSP_Values> listppt1 = Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesList", pv);

                    Hashtable httss = new Hashtable();

                    foreach (PSP_Values pv1 in listppt1)
                    {
                        


                        PSP_P_Values ppv = new PSP_P_Values();
                        InitValues(pv1, ppv);
                        try
                        {
                            Services.BaseService.Create<PSP_P_Values>(ppv);
                        }
                        catch { }
                    }

                }

                if (hsa.Count > 0)
                {
                    foreach (PSP_Years py1 in listyear)
                    {
                        PSP_P_Years ppy = new PSP_P_Years();
                        InitYears(py1, ppy);
                        try
                        {
                            Services.BaseService.Create<PSP_P_Years>(ppy);
                        }
                        catch { }
                    }
                }

            }
            else
            {

                PSP_P_Years py = new PSP_P_Years();
                py.Flag = fl2;
                IList<PSP_P_Years> listyear = Services.BaseService.GetList<PSP_P_Years>("SelectPSP_P_YearsListByFlag", py);



                Hashtable hsa = new Hashtable();
                foreach (DictionaryEntry de3 in hs)
                {
                    PSP_P_Types pt = new PSP_P_Types();

                    try{

                        if (de3.Value == null)
                        {
                            MsgBox.Show("��ʷ���ݲ���ȷ����ͨ�����������ݡ����ܼ����ʷ���ݡ�");
                            continue;
                        }

                        pt = (PSP_P_Types)de3.Value;
                }
                catch { continue; }
                    pt.Flag2 = typeFlag2;

                    IList<PSP_P_Types> listppt = Services.BaseService.GetList<PSP_P_Types>("SelectPSP_P_TypesByFlag2ID", pt);
                    if (listppt.Count > 0)
                        continue;

                    Services.BaseService.Create<PSP_P_Types>(pt);

                    hsa.Add(Guid.NewGuid().ToString(), "");

                    PSP_P_Values pv = new PSP_P_Values();
                    pv.TypeID = pt.ID;

                    IList<PSP_P_Values> listppt1 = Services.BaseService.GetList<PSP_P_Values>("SelectPSP_P_ValuesList", pv);

                    foreach (PSP_P_Values pv1 in listppt1)
                    {
                        try
                        {
                            pv1.Flag2 = typeFlag2;
                            Services.BaseService.Create<PSP_P_Values>(pv1);
                        }
                        catch { }
                    }

                }

                if (hsa.Count > 0)
                {
                    foreach (PSP_P_Years py1 in listyear)
                    {
                        try
                        {
                            py1.Flag = typeFlag2;
                            Services.BaseService.Create<PSP_P_Years>(py1);
                        }
                        catch { }
                    }
                }

            }

            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            LoadData();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;





        }

        private void InitTypes(PSP_Types pv, PSP_P_Types pv1)
        {
            pv1.ID = pv.ID;
            pv1.Flag = pv.Flag;
            pv1.Flag2 = typeFlag2;
            pv1.ParentID = pv.ParentID;
            pv1.Title = pv.Title;
        }

        private void InitYears(PSP_Years pv, PSP_P_Years pv1)
        {
            pv1.ID = pv.ID;
            pv1.Flag = typeFlag2;
            pv1.Year = pv.Year;
        }

        private void InitValues(PSP_Values pv, PSP_P_Values pv1)
        {
            pv1.ID = pv.ID;
            pv1.TypeID = pv.TypeID;
            pv1.Flag2 = typeFlag2;
            pv1.Value = pv.Value;
            pv1.Year = pv.Year;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (!base.AddRight)
            //{
            //    MsgBox.Show("��û��Ȩ�޽��д��������");
            //    return;
            //}

            TreeListNode focusedNode = treeList1.FocusedNode;

            if (focusedNode == null)
            {
                return;
            }


            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "���ӷ���";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                PSP_P_Types psp_Type = new PSP_P_Types();
                object obj = Services.BaseService.GetObject("SelectPSP_P_TypesMaxID", null);
                if (obj != null)
                    psp_Type.ID = ((int)obj) + 1;
                else
                    psp_Type.ID = 1;


                
                psp_Type.Title = frm.TypeTitle;

                int flag = psp_Type.ID + 1;
                if (focusedNode != null)
                {
                    flag = (int)focusedNode.GetValue("Flag");
                }
                psp_Type.Flag = flag;
                psp_Type.Flag2 = typeFlag2;
                psp_Type.ParentID = 0;

                try
                {
                    Common.Services.BaseService.Create("InsertPSP_P_Types", psp_Type);
                    //psp_Type.ID = (int)Common.Services.BaseService.Create("InsertPSP_P_Types", psp_Type);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));

                    this.Cursor = Cursors.WaitCursor;
                    treeList1.BeginUpdate();
                    LoadData();
                    treeList1.EndUpdate();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MsgBox.Show("���ӷ������" + ex.Message);
                }
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode focusedNode = treeList1.FocusedNode;

            if (focusedNode == null)
            {
                return;
            }

            //if (!base.AddRight)
            //{
            //    MsgBox.Show("��û��Ȩ�޽��д��������");
            //    return;
            //}

            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "����" + focusedNode.GetValue("Title") + "���ӷ���";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                PSP_P_Types psp_Type = new PSP_P_Types();
                object obj = Services.BaseService.GetObject("SelectPSP_P_TypesMaxID", null);
                if (obj != null)
                    psp_Type.ID = ((int)obj) + 1;
                else
                    psp_Type.ID = 1;
                psp_Type.Title = frm.TypeTitle;
                psp_Type.Flag = (int)focusedNode.GetValue("Flag");
                psp_Type.Flag2 = (int)focusedNode.GetValue("Flag2");
                psp_Type.ParentID = (int)focusedNode.GetValue("ID");

                try
                {

                    Services.BaseService.Create("InsertPSP_P_Types", psp_Type);
                    //psp_Type.ID = (int)Common.Services.BaseService.Create("InsertPSP_P_Types", psp_Type);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));
                }
                catch (Exception ex)
                {
                    MsgBox.Show("�����ӷ������" + ex.Message);
                }
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }

            string parentid = treeList1.FocusedNode["ParentID"].ToString();
            string flag = treeList1.FocusedNode["flag"].ToString();


            //if (!base.EditRight)
            //{
            //    MsgBox.Show("��û��Ȩ�޽��д��������");
            //    return;
            //}

            FormTypeTitle frm = new FormTypeTitle();
            frm.TypeTitle = treeList1.FocusedNode.GetValue("Title").ToString();
            frm.Text = "�޸ķ�����";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                PSP_P_Types psp_Type = new PSP_P_Types();
                Class1.TreeNodeToDataObject<PSP_P_Types>(psp_Type, treeList1.FocusedNode);
                psp_Type.Title = frm.TypeTitle;

                try
                {
                    Common.Services.BaseService.Update<PSP_P_Types>(psp_Type);
                    treeList1.FocusedNode.SetValue("Title", frm.TypeTitle);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("�޸ĳ���" + ex.Message);
                }
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }

            string parentid = treeList1.FocusedNode["ParentID"].ToString();
            string flag = treeList1.FocusedNode["flag"].ToString();


            if (treeList1.FocusedNode.HasChildren)
            {
                MsgBox.Show("�˷��������ӷ��࣬����ɾ���ӷ��࣡");
                return;
            }

            //if (!base.DeleteRight)
            //{
            //    MsgBox.Show("��û��Ȩ�޽��д��������");
            //    return;
            //}

            if (MsgBox.ShowYesNo("�Ƿ�ɾ������ " + treeList1.FocusedNode.GetValue("Title") + "��") == DialogResult.Yes)
            {
                PSP_P_Types psp_Type = new PSP_P_Types();
                Class1.TreeNodeToDataObject<PSP_P_Types>(psp_Type, treeList1.FocusedNode);
                PSP_P_Values psp_Values = new PSP_P_Values();
                psp_Values.TypeID = psp_Type.ID;
                psp_Values.Flag2 = typeFlag2;
                try
                {
                    //DeletePSP_ValuesByType����ɾ�����ݺͷ���
                    Common.Services.BaseService.Update("DeletePSP_P_ValuesByType", psp_Values);

                    TreeListNode brotherNode = null;
                    if (treeList1.FocusedNode.ParentNode.Nodes.Count > 1)
                    {
                        brotherNode = treeList1.FocusedNode.PrevNode;
                        if (brotherNode == null)
                        {
                            brotherNode = treeList1.FocusedNode.NextNode;
                        }
                    }
                    treeList1.DeleteNode(treeList1.FocusedNode);

                    //ɾ�������ͬ�������������࣬�����¼������������������
                    if (brotherNode != null)
                    {
                        foreach (TreeListColumn column in treeList1.Columns)
                        {
                            if (column.FieldName.IndexOf("��") > 0)
                            {
                                CalculateSum(brotherNode, column);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.WaitCursor;
                    treeList1.BeginUpdate();
                    LoadData();
                    treeList1.EndUpdate();
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


        //���ӷ������ݸı�ʱ�������丸�����ֵ
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



        private bool SaveCellValue(int year, int typeID, double value)
        {
            PSP_P_Values psp_values = new PSP_P_Values();
            psp_values.TypeID = typeID;
            psp_values.Value = value;
            psp_values.Year = year;
            psp_values.Flag2 = typeFlag2;

            try
            {
                Common.Services.BaseService.Update<PSP_P_Values>(psp_values);
            }
            catch (Exception ex)
            {
                MsgBox.Show("�������ݳ���" + ex.Message);
                return false;
            }
            return true;
        }

        private void treeList1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if ((e.Value.ToString() != lastEditValue
                || lastEditNode != e.Node
                || lastEditColumn != e.Column)
                && e.Column.FieldName.IndexOf("��") > 0
                && e.Column.Tag != null)
            {
                if (SaveCellValue((int)treeList1.FocusedColumn.Tag, (int)treeList1.FocusedNode.GetValue("ID"), Convert.ToDouble(e.Value)))
                {
                    CalculateSum(e.Node, e.Column);
                }
                else
                {
                    treeList1.FocusedNode.SetValue(treeList1.FocusedColumn.FieldName, lastEditValue);
                }
            }
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (treeList1.FocusedNode.HasChildren)
             //   || !base.EditRight)
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

        private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {

        }
    }
}