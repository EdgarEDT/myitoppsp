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

namespace Itop.Client.Chen
{
    public partial class Form7 : FormBase
    {
        DataTable dataTable;

        private TreeListNode lastEditNode = null;
        private TreeListColumn lastEditColumn = null;
        private string lastEditValue = string.Empty;
        private string baseyearFlag = "4132ae36-36b3-47ed-a9b9-163a6479d5d3";
        private int typeFlag2 = 7;



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

        public Form7()
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
            if (!base.AddRight && !base.EditRight)
            {
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void Form7_Load(object sender, EventArgs e)
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
            string baseyear = "";
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
            }

            PSP_Types psp_Type = new PSP_Types();
            psp_Type.Flag2 = typeFlag2;
            IList listTypes = Common.Services.BaseService.GetList("SelectPSP_TypesByFlag2", psp_Type);

            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_Types));

            treeList1.DataSource = dataTable;

            treeList1.Columns["Title"].Caption = "������";
            treeList1.Columns["Title"].Width = 180;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Flag"].VisibleIndex = -1;
            treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["Flag2"].VisibleIndex = -1;
            treeList1.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;


            baseyear = EnsureBaseYear(baseyear);
            PSP_Years psp_Year = new PSP_Years();
            IList<PSP_Years> listYears;
            if (baseyear != "")
            {
                psp_Year.Flag = typeFlag2;
                psp_Year.Year = Convert.ToInt32(baseyear);
                listYears = Common.Services.BaseService.GetList<PSP_Years>("SelectPSP_YearsListByFlagAndYear", psp_Year);
            }
            else
            {
                psp_Year.Flag = typeFlag2;
                listYears = Common.Services.BaseService.GetList<PSP_Years>("SelectPSP_YearsListByFlag", psp_Year);
            }
            foreach (PSP_Years item in listYears)
            {
                
                AddColumn(item.Year);
    
            }
            Application.DoEvents();

            LoadValues(true);

            treeList1.ExpandAll();
            if (baseyear != "")
            {
                treeList1.Columns[3].AppearanceCell.BackColor = Color.GreenYellow;
                this.Cursor = Cursors.Default;
            }
        }

        //��ȡ����
        private void LoadValues(bool Isfirstload)
        {
            string baseyear = "";
            baseyear = EnsureBaseYear(baseyear);
            PSP_Values psp_Values = new PSP_Values();
            IList<PSP_Values> listValues;
            if (baseyear == "")
            {
                psp_Values.ID = typeFlag2;//��ID�ֶδ��Flag2��ֵ
                listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesListByFlag2", psp_Values);

            }
            else
            {
                Hashtable ha = new Hashtable();
                ha.Add("ID", typeFlag2);
                ha.Add("Year", Convert.ToInt32(baseyear));
                listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesListByFlag2Year", ha);
            }
            foreach (PSP_Values value in listValues)
            {

                TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
                if (node != null)
                {
                    
                    if(Isfirstload) node.SetValue(value.Year + "��",Math.Round(value.Value,2));
                    SetRateYearValue(node, Math.Round(value.Value,2));
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
            column.ColumnEdit = repositoryItemTextEdit1;
            //treeList1.RefreshDataSource();
            PSP_Types psp_Type = new PSP_Types();
            psp_Type.Flag2 = typeFlag2;
            IList listTypes = Common.Services.BaseService.GetList("SelectPSP_TypesByFlag2", psp_Type);
            foreach (PSP_Types psp_Typetemp in listTypes)
            {
                PSP_BaseYearRate psp_Yeartemp = new PSP_BaseYearRate();
                psp_Yeartemp.BaseYear = year.ToString();
                psp_Yeartemp.TypeID = psp_Typetemp.ID;
                try
                {
                    IList<PSP_BaseYearRate> list1 = Common.Services.BaseService.GetList<PSP_BaseYearRate>("SelectPSP_BaseYearRateByYear", psp_Yeartemp);

                    if (list1.Count == 0)
                    {
                        try
                        {



                            psp_Yeartemp.UID = Guid.NewGuid().ToString();
                            psp_Yeartemp.YearRate = "0";
                            Common.Services.BaseService.Create<PSP_BaseYearRate>(psp_Yeartemp);

                            this.DialogResult = DialogResult.OK;
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show("��������" + ex.Message);
                        }
                    }

                }
                catch (Exception ex)
                {
                    MsgBox.Show(ex.ToString());
                }
            }
        }

        private int GetInsertIndex(int year)
        {
            int nFixedColumns = typeof(PSP_Types).GetProperties().Length - 2;//ID��ParentID�в���treeList1.Columns��
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
            TreeListNode focusedNode = treeList1.FocusedNode;

            if (focusedNode == null)
            {
                return;
            }

            if (!base.AddRight)
            {
                MsgBox.Show("��û��Ȩ�޽��д��������");
                return;
            }


            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "����" + focusedNode.GetValue("Title") + "���ӷ���";

            if (frm.ShowDialog() == DialogResult.OK)
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
                catch (Exception ex)
                {
                    MsgBox.Show("�����ӷ������" + ex.Message);
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
                MsgBox.Show("һ���������Ʋ����޸ģ�");
                return;
            }

            if (!base.EditRight)
            {
                MsgBox.Show("��û��Ȩ�޽��д��������");
                return;
            }

            FormTypeTitle frm = new FormTypeTitle();
            frm.TypeTitle = treeList1.FocusedNode.GetValue("Title").ToString();
            frm.Text = "�޸ķ�����";

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

            if (treeList1.FocusedNode.ParentNode == null)
            {
                MsgBox.Show("һ������Ϊ�̶����ݣ�����ɾ����");
                return;
            }

            if (treeList1.FocusedNode.HasChildren)
            {
                MsgBox.Show("�˷��������ӷ��࣬����ɾ���ӷ��࣡");
                return;
            }

            if (!base.DeleteRight)
            {
                MsgBox.Show("��û��Ȩ�޽��д��������");
                return;
            }

            if (MsgBox.ShowYesNo("�Ƿ�ɾ������ " + treeList1.FocusedNode.GetValue("Title") + "��") == DialogResult.Yes)
            {
                PSP_Types psp_Type = new PSP_Types();
                Class1.TreeNodeToDataObject<PSP_Types>(psp_Type, treeList1.FocusedNode);
                PSP_Values psp_Values = new PSP_Values();
                psp_Values.TypeID = psp_Type.ID;

                try
                {
                    //DeletePSP_ValuesByType����ɾ�����ݺͷ���
                    Common.Services.BaseService.Update("DeletePSP_ValuesByType", psp_Values);

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
                    MsgBox.Show("ɾ������" + ex.Message);
                }
            }
        }

        //�������
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!base.AddRight)
            {
                MsgBox.Show("��û��Ȩ�޽��д��������");
                return;
            }

            FormNewYear frm = new FormNewYear();
            frm.Flag2 = typeFlag2;
            int nFixedColumns = typeof(PSP_Types).GetProperties().Length;
            int nColumns = treeList1.Columns.Count;
            if (nFixedColumns == nColumns + 2)//���ʱ����ʾ��û����ݣ������Ĭ��Ϊ��ǰ���ȥ15��
            {
                frm.YearValue = DateTime.Now.Year - 15;
            }
            else
            {
                //�����ʱ��Ĭ��Ϊ�����ݼ�1��
                frm.YearValue = (int)treeList1.Columns[nColumns - 1].Tag + 1;
            }

            if (frm.ShowDialog() == DialogResult.OK)
            {
                AddColumn(frm.YearValue);
                LoadValues(false);
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string baseyear = "";
            baseyear = EnsureBaseYear(baseyear);
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
            if (!base.DeleteRight)
            {
                MsgBox.Show("��û��Ȩ�޽��д��������");
                return;
            }

            if (MsgBox.ShowYesNo("�Ƿ�ɾ�� " + treeList1.FocusedColumn.FieldName + " ��������������ݣ�") != DialogResult.Yes)
            {
                return;
            }

            PSP_Values psp_Values = new PSP_Values();
            psp_Values.ID = typeFlag2;//����ID���Դ��Flag2
            psp_Values.Year = (int)treeList1.FocusedColumn.Tag;

            try
            {
                //DeletePSP_ValuesByYearɾ�����ݺ����
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
            catch (Exception ex)
            {
                MsgBox.Show("ɾ������" + ex.Message);
            }
        }

        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            string baseyear = "";
            baseyear = EnsureBaseYear(baseyear);
            if ((e.Value.ToString() != lastEditValue
                || lastEditNode != e.Node
                || lastEditColumn != e.Column)
                && e.Column.FieldName.IndexOf("��") > 0
                && e.Column.Tag != null)
            {
                if (e.Column.FieldName.Replace("��", "") != baseyear && baseyear != "") return;
                if (SaveCellValue((int)treeList1.FocusedColumn.Tag, (int)treeList1.FocusedNode.GetValue("ID"),Math.Round(Convert.ToDouble(e.Value),2)))
                {
                    SetRateYearValue(e.Node,Math.Round(Convert.ToDouble(e.Value),2));
                    CalculateSum(e.Node, e.Column);
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
            baseyear = EnsureBaseYear(baseyear);
            if (treeList1.FocusedNode.HasChildren
                || !base.EditRight || (treeList1.FocusedColumn.Tag.ToString() != baseyear && baseyear != ""))
            {
                e.Cancel = true;
            }
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

            parentNode.SetValue(column.FieldName,Math.Round(sum,2));
            SetRateYearValue(parentNode, sum);
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

        //ͳ��
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormChooseYears frm = new FormChooseYears();
            foreach (TreeListColumn column in treeList1.Columns)
            {
                if (column.FieldName.IndexOf("��") > 0)
                {
                    frm.ListYearsForChoose.Add((int)column.Tag);
                }
            }

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Form1Result f = new Form1Result();
                f.CanPrint = base.PrintRight;
                f.Text = Title = "������" + frm.ListChoosedYears[0].Year + "��" + frm.ListChoosedYears[frm.ListChoosedYears.Count - 1].Year + "�꾭�÷�չĿ��";
                f.GridDataTable = ResultDataTable(ConvertTreeListToDataTable(treeList1), frm.ListChoosedYears);
                f.IsSelect = IsSelect;
                DialogResult dr = f.ShowDialog();
                if (IsSelect && dr == DialogResult.OK)
                {
                    Gcontrol = f.gridControl1;
                    Unit = "��λ����Ԫ������";
                    DialogResult = DialogResult.OK;
                }
            }

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
            Form7_BaseYear baseyear = new Form7_BaseYear();
            baseyear.Text = "���û�׼�꣡";
            int nFixedColumns = typeof(PSP_Types).GetProperties().Length;
            int nColumns = treeList1.Columns.Count;
            if (nFixedColumns == nColumns + 2)//���ʱ����ʾ��û����ݣ�����
            {
                MsgBox.Show("��ݲ����ڣ�����Ӻ��ٲ�����");
                return;
            }
            else
            {
                string baseyearnum = "";
                baseyearnum = EnsureBaseYear(baseyearnum);
                //�����ʱ��Ĭ��Ϊ��׼��
                if (baseyearnum != "")
                    baseyear.BaseYear = baseyearnum;
                else
                    baseyear.BaseYear = treeList1.Columns[nFixedColumns-2].Tag.ToString();

            }

            if (baseyear.ShowDialog() == DialogResult.OK)
            {

                PSP_BaseYearRate byear = new PSP_BaseYearRate();
                byear.BaseYear = baseyear.BaseYear.ToString();

                byear.UID = baseyearFlag;
                Common.Services.BaseService.Update("UpdatePSP_BaseYear", byear);

                treeList1.BeginUpdate();
                LoadData();
                treeList1.EndUpdate();
                treeList1.Columns[3].AppearanceCell.BackColor = Color.GreenYellow;
                this.Cursor = Cursors.Default;
            }
            else
            { baseyear.Close(); }
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string baseyearnum = "";
            baseyearnum = EnsureBaseYear(baseyearnum);
            //�����ʱ��Ĭ��Ϊ��׼��
            if (baseyearnum == "")
            {
                MsgBox.Show("��׼��δ�趨������Ӻ��ٲ�����");
                return;
            }
            int nFixedColumns = typeof(PSP_Types).GetProperties().Length;
            int nColumns = treeList1.Columns.Count;
            if (nFixedColumns == nColumns + 1)//���ʱ����ʾ��û����ݣ�����
            {
                MsgBox.Show("û�����Ԥ���꣬����Ӻ��ٲ�����");
                return;
            }
            Form7_BaseYearRate frm = new Form7_BaseYearRate();
            frm.Text = "������������";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadValues(false);
            }
        }
        private string EnsureBaseYear(string baseyear)
        {
            PSP_BaseYearRate BaseYearrate = (PSP_BaseYearRate)Common.Services.BaseService.GetObject("SelectPSP_BaseYearRateByKey", baseyearFlag);
            if (BaseYearrate == null)
            {
                BaseYearrate = new PSP_BaseYearRate();
                BaseYearrate.UID = "4132ae36-36b3-47ed-a9b9-163a6479d5d3";
                BaseYearrate.BaseYear = "1990";
                BaseYearrate.YearRate = "0";
                Services.BaseService.Create<PSP_BaseYearRate>(BaseYearrate);
                //
            }
            
            if (BaseYearrate.BaseYear != "0")
            {
                Hashtable ha = new Hashtable();
                ha.Add("Flag", typeFlag2);
                ha.Add("Year", Convert.ToInt32(BaseYearrate.BaseYear));
                PSP_Years baseyearlistYears = (PSP_Years)Common.Services.BaseService.GetObject("SelectPSP_YearsByYearFlag", ha);
                if (baseyearlistYears != null)
                {

                    baseyear = baseyearlistYears.Year.ToString();

                }
            }
            return baseyear;
        }

        private void SetRateYearValue(TreeListNode node, double sum)
        {
            string baseyear = "";
            baseyear = EnsureBaseYear(baseyear);
            if (baseyear != "")
            {
                double rate = 0;
                double sumtemp = sum;
                int yeartemp = 0;
                PSP_Years psp_Year = new PSP_Years();
                psp_Year.Flag = typeFlag2;
                psp_Year.Year = Convert.ToInt32(baseyear);
                IList<PSP_Years> listYears = Common.Services.BaseService.GetList<PSP_Years>("SelectPSP_YearsListByFlagAndYear", psp_Year);
                yeartemp = Convert.ToInt32(baseyear);


                foreach (PSP_Years yearvalue in listYears)
                {

                    if (yearvalue.Year == Convert.ToInt32(baseyear)) continue;
                    try
                    {
                        PSP_BaseYearRate yearvaluetemp = new PSP_BaseYearRate();
                        yearvaluetemp.BaseYear = yearvalue.Year.ToString();
                        yearvaluetemp.TypeID =Convert.ToInt32(node.GetValue("ID"));
                        IList<PSP_BaseYearRate> list1 = Common.Services.BaseService.GetList<PSP_BaseYearRate>("SelectPSP_BaseYearRateByYear", yearvaluetemp);
                   
                        if (list1.Count == 1)
                    {
                        rate = Convert.ToDouble(list1[0].YearRate);
                    }
                    sumtemp = sumtemp * Math.Pow(1 + rate, yearvalue.Year - yeartemp);
                    
                    node.SetValue(yearvalue.Year + "��", Math.Round(sumtemp, 2));
                    yeartemp = yearvalue.Year;
                    SaveCellValue((int)yearvalue.Year, (int)node.GetValue("ID"), Convert.ToDouble(sumtemp));

                 }
                catch { }
                }

            }

        }
    }
}