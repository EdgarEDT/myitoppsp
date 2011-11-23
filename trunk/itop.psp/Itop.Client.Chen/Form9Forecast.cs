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
using Itop.Domain.BaseDatas;
using Itop.Client.Common;

namespace Itop.Client.Chen
{
    public partial class Form9Forecast : FormBase
    {
        DataTable dataTable;
        DataTable dt;

        private int typeFlag2 = 4;
        private int typeFlag = 0;
        private PSP_ForecastReports forecastReport = null;
        Hashtable hash = new Hashtable();
        bool bLoadingData = false;
        bool _canEdit = true;
        bool _isSelect = false;
        int startyear=0;

        public int StartYear
        {
            get { return startyear; }
            set { startyear = value; }
        }

        public bool IsSelect
        {
            get { return _isSelect; }
            set { _isSelect = value; }
        }

        DevExpress.XtraGrid.GridControl _gridControl;

        public DevExpress.XtraGrid.GridControl GridControl
        {
            get { return _gridControl; }
            set { _gridControl = value; }
        }
        private bool EditRight = false;
        public bool CanEdit
        {
            get { return _canEdit; }
            set { _canEdit = value; EditRight = value; }
        }
        bool _canPrint = true;


        string com = "";


        public bool CanPrint
        {
            get { return _canPrint; }
            set { _canPrint = value; }
        }
        private bool AddRight = false;
        public bool ADdRight
        {
            get { return AddRight; }
            set { AddRight = value; }
        }
        private bool DeleteRight = false;
        public bool DEleteRight
        {
            get { return DeleteRight; }
            set { DeleteRight = value; }
        }
        public Form9Forecast(PSP_ForecastReports fr, int tf)
        {
            InitializeComponent();
            forecastReport = fr;
            typeFlag = tf;
            Text = fr.Title;
        }

        private void HideToolBarButton()
        {
            if (!CanEdit)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barSubItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem14.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem17.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void Form9Forecast_Load(object sender, EventArgs e)
        {
            hash.Add("ArverageIncreasingMethod", "�������ʷ�");
            hash.Add("SpringCoefficientMethod", "����ϵ����");
            hash.Add("TwiceMoveArverageMethod", "�ƶ�ƽ����");
            hash.Add("GrayMethod", "��ɫģ�ͷ�");
            hash.Add("LinearlyRegressMethod", "���Իع鷨");
            hash.Add("IndexIncreaseMethod", "ָ��������");
            hash.Add("IndexSmoothMethod", "ָ��ƽ����");
            hash.Add("LinearlyTrend", "�������Ʒ�");
            hash.Add("ProfessionalLV", "ר�Ҿ��߷�");
            HideToolBarButton();
            chart1.Series.Clear();
            Show();
            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            LoadData();
            treeList1.EndUpdate();
            RefreshChart();
            this.Cursor = Cursors.Default;
        }

        private void LoadData()
        {
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
            }
            bLoadingData = true;


            ////PSP_P_Types psp_Type = new PSP_P_Types();
            ////psp_Type.Flag2 = forecastReport.ID;
            ////IList listTypes = Common.Services.BaseService.GetList("SelectPSP_P_TypesByFlag2", psp_Type);

            string stra = " Flag2='" + forecastReport.ID + "' and id not in(80001,80002,80003,80004,80005,80006) ";
            IList listTypes = Common.Services.BaseService.GetList("SelectPSP_P_TypesByWhere", stra);

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

            for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
            {
                AddColumn(i);
            }
            Application.DoEvents();

            LoadValues();

            LoadHistoryValue();

            //InitDatas();

            treeList1.ExpandAll();
            bLoadingData = false;
        }


        //SelectPSP_P_ValuesByWhere

        //��ȡ����
        private void LoadValues()
        {
            PSP_ForecastValues psp_Value = new PSP_ForecastValues();
            psp_Value.ForecastID = forecastReport.ID;

            IList<PSP_ForecastValues> listValues = Common.Services.BaseService.GetList<PSP_ForecastValues>("SelectPSP_ForecastValuesByForecastID", psp_Value);


            foreach (PSP_ForecastValues value in listValues)
            {
                TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
                if (node != null && value.Year >= forecastReport.StartYear && value.Year <= forecastReport.EndYear)
                {
                    node.SetValue(value.Year + "��", value.Value);
                }
            }
        }


        private void InitDatas()
        {
            foreach (TreeListColumn tlc in treeList1.Columns)
            {
                //if (tlc.FieldName.IndexOf("��") >= 0 || tlc.FieldName.IndexOf("Y") >= 0)
                if (tlc.FieldName.IndexOf("��") >= 0 && int.Parse(tlc.FieldName.Replace("��", "")) == startyear)
                {
                    double s1 = 0;
                    double s2 = 0;
                    try
                    {
                        s1 = (double)dataTable.Rows[0][tlc.FieldName];
                    }
                    catch { }

                    try
                    {
                        s2 = (double)dataTable.Rows[1][tlc.FieldName];
                    }
                    catch { }

                    try
                    {
                        if (s1.ToString() == "������")
                            s1 = 0;
                        if (s2.ToString() == "������")
                            s2 = 0;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }

                }

            }



        }





        private void InitDataCompute()
        {
            foreach (TreeListColumn tlc in treeList1.Columns)
            {
                if (tlc.FieldName.IndexOf("��") >= 0)
                {
                    if (int.Parse(tlc.FieldName.Replace("��", "")) != startyear)
                    {
                        double s1 = 0;
                        double s2 = 0;
                        try
                        {
                            s1 = (double)dataTable.Rows[0][tlc.FieldName];
                        }
                        catch { }

                        try
                        {
                            s2 = (double)dataTable.Rows[1][tlc.FieldName];
                        }
                        catch { }

                        try
                        {
                            if (s1.ToString() == "������")
                                s1 = 0;
                            if (s2.ToString() == "������")
                                s2 = 0;
                            if (s1 + s2 != 0)
                                dataTable.Rows[0][tlc.FieldName] = s1 + s2;
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }

                    }
                }

            }
        }

        //�����ݺ�����һ��
        private void AddColumn(int year)
        {
            dataTable.Columns.Add(year + "��", typeof(double));

            TreeListColumn column = treeList1.Columns.Add();
            column.FieldName = year + "��";
            column.Tag = year;
            column.Caption = year + "��";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = column.ColumnHandle - 2;//������������
            column.ColumnEdit = repositoryItemTextEdit1;
        }

        //��ȡ��ʷ����
        private void LoadHistoryValue()
        {
            PSP_Years psp_Year = new PSP_Years();
            psp_Year.Flag = 1;
            IList<PSP_Years> listYears = Common.Services.BaseService.GetList<PSP_Years>("SelectPSP_YearsListByFlag", psp_Year);
            Hashtable ht = new Hashtable();
            foreach (PSP_Years item in listYears)
            {
                dataTable.Columns.Add(item.Year + "Y", typeof(double));

                TreeListColumn column = treeList1.Columns.Add();
                column.FieldName = item.Year + "Y";
                column.Tag = item.Year;
                column.VisibleIndex = -1;
                if (!ht.ContainsKey(item.Year + "Y"))
                    ht.Add(item.Year + "Y", "");
            }

            PSP_Values psp_Values = new PSP_Values();

            foreach (DataRow dr in dataTable.Rows)
            {
                psp_Values.TypeID = (int)dr["ID"];

                IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesList", psp_Values);

                foreach (PSP_Values value in listValues)
                {
                    TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
                    if (node != null)
                    {

                        if (!ht.ContainsKey(value.Year + "Y"))
                        {
                            dataTable.Columns.Add(value.Year + "Y", typeof(double));
                            TreeListColumn column = treeList1.Columns.Add();
                            column.FieldName = value.Year + "Y";
                            column.Tag = value.Year;
                            column.VisibleIndex = -1;
                            ht.Add(value.Year + "Y", "");
                        }

                        node.SetValue(value.Year + "Y", value.Value);

                        if (value.Year == forecastReport.StartYear)
                        {
                            node.SetValue(value.Year + "��", value.Value);
                        }

                          
                        

                    }
                }
            }
        }

        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.IndexOf("��") > 0
                && e.Column.Tag != null
                && !bLoadingData)
            {
                CalculateSum(e.Node, e.Column);
                RefreshChart();
            }
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (treeList1.FocusedNode.HasChildren
                || !CanEdit)
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

            parentNode.SetValue(column.FieldName, sum);

            CalculateSum(parentNode, column);
        }

        //����ָ���ڵ�ĸ��и�
        private void CalculateSum(TreeListNode node)
        {
            foreach (TreeListColumn column in treeList1.Columns)
            {
                if (column.FieldName.IndexOf("��") > 0)
                {
                    CalculateSum(node, column);
                }
            }
        }

        private void treeList1_ShownEditor(object sender, EventArgs e)
        {
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
                f.CanPrint = CanPrint;
                f.Text = forecastReport.Title;//  = "������" + frm.ListChoosedYears[0].Year + "��" + frm.ListChoosedYears[frm.ListChoosedYears.Count - 1].Year + "����߸���Ԥ���";
                f.GridDataTable = ResultDataTable(ConvertTreeListToDataTable(treeList1), frm.ListChoosedYears);
                f.IsSelect = IsSelect;
                DialogResult dr = f.ShowDialog();
                if (IsSelect && dr == DialogResult.OK)
                {
                    GridControl = f.gridControl1;
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

            #region �������
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

        //���ݽڵ㷵�ش��е���ʷ����
        private double[] GenerateHistoryValue(TreeListNode node)
        {
            double[] rt = new double[forecastReport.HistoryYears];
            for (int i = 0; i < forecastReport.HistoryYears; i++)
            {
                object obj = node.GetValue(forecastReport.StartYear - forecastReport.HistoryYears + i + 1 + "Y");
                if (obj == null || obj == DBNull.Value)
                {
                    rt[i] = 0;
                }
                else
                {
                    rt[i] = (double)obj;
                }
            }
            return rt;
        }

        private void Forecast(string methodName)
        {
            Calculator.Type = forecastReport.ID.ToString();
            Calculator.StartYear = forecastReport.StartYear;
            if (!CanEdit)
            {
                MsgBox.Show("��û��Ȩ�޽��д��������");
                return;
            }
            treeList1.BeginUpdate();
            try
            {
                int nRow = -1;

                if (dataTable.Rows.Count == 0)
                {
                    MsgBox.Show("��ʷ���ݲ���ȷ����ͨ�����������ݡ����ܼ����ʷ���ݡ�");
                    return;
                }

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    TreeListNode treeNode = treeList1.FindNodeByKeyID(dataRow["ID"]);

                    //���ӽڵ�Ĳ�����ֵ��ͨ���ӽڵ���ӵõ�
                    if (!treeNode.HasChildren)
                    {
                        int forecastYears = forecastReport.EndYear - forecastReport.StartYear;
                        double[] historyValues = GenerateHistoryValue(treeNode);

                        double[] forecastValue = null;
                        nRow++;

                        switch (methodName)
                        {
                            case "ArverageIncreasingMethod":
                                forecastValue = Calculator.ArverageIncreasingMethod(historyValues,
                                    (double)dataRow[forecastReport.StartYear + "��"], forecastYears, nRow + 1);

                                break;

                            case "SpringCoefficientMethod":
                                forecastValue = Calculator.SpringCoefficientMethod(historyValues[historyValues.Length - 1], forecastYears);
                                break;

                            case "TwiceMoveArverageMethod":
                                forecastValue = Calculator.TwiceMoveArverageMethod(historyValues, forecastYears);
                                break;

                            case "GrayMethod":
                                forecastValue = Calculator.GrayMethod(historyValues, forecastYears);
                                break;

                            case "LinearlyRegressMethod":
                                forecastValue = Calculator.LinearlyRegressMethod(historyValues, forecastYears);
                                break;

                            case "IndexIncreaseMethod":
                                forecastValue = Calculator.IndexIncreaseMethod(historyValues, forecastYears);
                                break;

                            case "IndexSmoothMethod":
                                forecastValue = Calculator.IndexSmoothMethod(historyValues, forecastYears);
                                break;

                            case "LinearlyTrend":
                                forecastValue = Calculator.LinearlyTrend(historyValues, forecastYears);
                                break;

                            case "ProfessionalLV":
                                forecastValue = Calculator.ProfessionalLV(historyValues[historyValues.Length - 1], forecastYears);
                                break;
                        }

                        for (int i = 0; i < forecastYears; i++)
                        {
                            dataRow[forecastReport.StartYear + i + 1 + "��"] = Math.Round(forecastValue[i], 2);
                        }

                        //���ڵ�����һ���ӽڵ㣬���ü��㹫ʽ
                        if (treeNode.NextNode == null)
                        {
                            CalculateSum(treeNode);
                        }


                        //bool bz = false;
                        //foreach (double d in historyValue)
                        //{
                        //    if (d == 0)
                        //        bz = true;
                        //}




                    }
                }
                //if (methodName != "ArverageIncreasingMethod")
                //InitDataCompute();
                RefreshChart();
            }
            catch
            {
                chart1.Series.Clear();
                MsgBox.Show("��ʷ���ݲ���ȷ����ͨ�����������ݡ����ܼ����ʷ���ݡ�");
            }
            finally
            {
                treeList1.EndUpdate();
            }
        }


        private void Forecast(double[,] values)
        {
            if (!CanEdit)
            {
                MsgBox.Show("��û��Ȩ�޽��д��������");
                return;
            }

            treeList1.BeginUpdate();
            //try
            {
                int forecastYears = values.GetLength(1);
                DataTable dtForecast = new DataTable();
                for (int i = 0; i < forecastYears + 20; i++)
                {
                    dtForecast.Columns.Add((2000 + i).ToString(), typeof(double));
                }
                int nRows = values.GetLength(0);
                for (int i = 0; i < nRows; i++)
                {
                    DataRow dr = dtForecast.NewRow();
                    dtForecast.Rows.Add(dr);
                    for (int j = 0; j < forecastYears + 20; j++)
                    {
                        dr[j] = 0;
                    }
                    for (int j = 0; j < forecastYears; j++)
                    {
                        dr[(2007 + j).ToString()] = values[i, j];
                    }
                }

                int nRow = 0;
                forecastYears = forecastReport.EndYear - forecastReport.StartYear;
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    TreeListNode treeNode = treeList1.FindNodeByKeyID(dataRow["ID"]);

                    //���ӽڵ�Ĳ�����ֵ��ͨ���ӽڵ���ӵõ�
                    if (!treeNode.HasChildren)
                    {

                        for (int i = 0; i < forecastYears; i++)
                        {
                            dataRow[forecastReport.StartYear + i + 1 + "��"] = Math.Round(Convert.ToDouble(dtForecast.Rows[nRow][Convert.ToString(forecastReport.StartYear + i + 1)]), 2);
                        }
                        nRow++;

                        //���ڵ�����һ���ӽڵ㣬���ü��㹫ʽ
                        if (treeNode.NextNode == null)
                        {
                            CalculateSum(treeNode);
                        }
                    }
                }
                RefreshChart();
            }
            treeList1.EndUpdate();

        }

        //�������ʷ�
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitMenu();
            barButtonItem1.ImageIndex = 8;
            com = "ArverageIncreasingMethod";
            Forecast("ArverageIncreasingMethod");
            //InitDataCompute();
        }

        //����ϵ����
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitMenu();
            barButtonItem2.ImageIndex = 8;
            com = "SpringCoefficientMethod";
            Forecast("SpringCoefficientMethod");
        }

        //�ƶ�ƽ����
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitMenu();
            barButtonItem3.ImageIndex = 8;
            com = "TwiceMoveArverageMethod";
            if (forecastReport.HistoryYears < 5)
            {
                MsgBox.Show("�ƶ�ƽ����������Ҫ5�����ʷ���ݣ�");
                return;
            }

            Forecast("TwiceMoveArverageMethod");
            //InitDataCompute();
        }

        //��ɫģ��
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitMenu();
            barButtonItem4.ImageIndex = 8;
            com = "GrayMethod";
            Forecast("GrayMethod");
            //InitDataCompute();
        }

        //���Իع�
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitMenu();
            barButtonItem5.ImageIndex = 8;
            com = "LinearlyRegressMethod";
            Forecast("LinearlyRegressMethod");
            //InitDataCompute();
        }

        //ָ������
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitMenu();
            barButtonItem7.ImageIndex = 8;
            com = "IndexIncreaseMethod";
            Forecast("IndexIncreaseMethod");
            //InitDataCompute();
        }

        //ָ��ƽ��
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitMenu();
            barButtonItem8.ImageIndex = 8;
            com = "IndexSmoothMethod";
            Forecast("IndexSmoothMethod");
            //InitDataCompute();
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitMenu();
            barButtonItem12.ImageIndex = 8;
            com = "LinearlyTrend";
            Forecast("LinearlyTrend");
        }







        private void InitMenu()
        {
            barButtonItem1.ImageIndex = -1;
            barButtonItem2.ImageIndex = -1;
            barButtonItem3.ImageIndex = -1;
            barButtonItem4.ImageIndex = -1;
            barButtonItem5.ImageIndex = -1;
            barButtonItem7.ImageIndex = -1;
            barButtonItem8.ImageIndex = -1;
            barButtonItem12.ImageIndex = -1;
            barButtonItem18.ImageIndex = -1;

        }




        //ר�ҷ�����ָ�������ʣ��ٰ�ָ���������ʼ������
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
            if (!CanEdit)
            {
                MsgBox.Show("��û��Ȩ�޽��д��������");
                return;
            }


            FormProfessionalMethod_kim frm = new FormProfessionalMethod_kim(typeFlag2, forecastReport);


            frm.ShowDialog();
            treeList1.BeginUpdate();
            LoadValues();
            treeList1.EndUpdate();
            RefreshChart();

         }

        //��������
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (!CanEdit)
            {
                MsgBox.Show("��û��Ȩ�޽��д��������");
                return;
            }
            Save();
        }


        private void Save()
        {

            textBox1.Focus();

            Cursor = Cursors.WaitCursor;
            List<PSP_ForecastValues> listValues = new List<PSP_ForecastValues>();
            foreach (TreeListNode nd in treeList1.Nodes)
            {
                AddNodeDataToList(nd, listValues);
            }

            try
            {
                Common.Services.BaseService.Update<PSP_ForecastValues>(listValues);
                //MsgBox.Show("���ݱ���ɹ���");
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            }

            Cursor = Cursors.Default;
        
        }



        //�ѽڵ����ݴ�ŵ������У������List
        private void AddNodeDataToList(TreeListNode node, IList<PSP_ForecastValues> list)
        {
            foreach (TreeListColumn col in treeList1.Columns)
            {
                if (col.FieldName.IndexOf("��") > 0)
                {
                    object obj = node.GetValue(col.FieldName);
                    if (obj != DBNull.Value)
                    {
                        PSP_ForecastValues v = new PSP_ForecastValues();
                        v.ForecastID = forecastReport.ID;
                        v.TypeID = (int)node.GetValue("ID");
                        v.Caption = node.GetValue("Title").ToString() + "," + v.TypeID;
                        v.Year = Convert.ToInt32(col.FieldName.Replace("��", ""));
                        v.Value = (double)node.GetValue(col.FieldName);

                        list.Add(v);
                    }
                }
            }

            foreach (TreeListNode nd in node.Nodes)
            {
                AddNodeDataToList(nd, list);
            }
        }

        private void RefreshChart1()
        { }
        private void CopyBaseColor(BaseColor bc1, BaseColor bc2)
        {
            bc1.UID = bc2.UID;
            bc1.Title = bc2.Title;
            bc1.Remark = bc2.Remark;
            bc1.Color = bc2.Color;
            bc1.Color1 = ColorTranslator.FromOle(bc2.Color);
        }
        private void RefreshChart()
        {
            List<PSP_ForecastValues> listValues = new List<PSP_ForecastValues>();
            foreach (TreeListNode nd in treeList1.Nodes)
            {
                AddNodeDataToList(nd, listValues);
            }
            chart1.Series.Clear();

            //if (!CheckChartData(listValues))
            //{
            //    //MsgBox.Show("Ԥ���������쳣������ͼ�޷���ʾ��");
            //    return;
            //}






            IList<BaseColor> list = Services.BaseService.GetList<BaseColor>("SelectBaseColorByWhere", "Remark='" + this.forecastReport.ID + "'");

            IList<BaseColor> li = new List<BaseColor>();
            bool bl = false;
            foreach (DataRow row in dataTable.Rows)
            {
                bl = false;
                foreach (BaseColor bc in list)
                {
                    if (row["Title"].ToString() == bc.Title)
                    {
                        bl = true;
                        BaseColor bc1 = new BaseColor();
                        bc1.Color1 = Color.Blue;
                        CopyBaseColor(bc1, bc);
                        li.Add(bc1);
                    }


                }
                if (!bl)
                {
                    BaseColor bc1 = new BaseColor();
                    bc1.UID = Guid.NewGuid().ToString();
                    bc1.Remark = this.forecastReport.ID.ToString();
                    bc1.Title = row["Title"].ToString();
                    bc1.Color = 16711680;
                    bc1.Color1 = Color.Blue;
                    Services.BaseService.Create<BaseColor>(bc1);
                    li.Add(bc1);
                }

            }
            ArrayList hs = new ArrayList();
            foreach (BaseColor bc2 in li)
            {
                hs.Add(bc2.Color1);
            }

            chart1.DataBindCrossTab(listValues, "Caption", "Year", "Value", "");
            for (int i = 0; i < chart1.Series.Count; i++)
            {
                chart1.Series[i].Color = (Color)hs[i];
                chart1.Series[i].Name = (i + 1).ToString() + "." + chart1.Series[i].Name.Substring(0, chart1.Series[i].Name.IndexOf(","));
                chart1.Series[i].Type = Dundas.Charting.WinControl.SeriesChartType.Line;
                chart1.Series[i].MarkerSize = 7;
                chart1.Series[i].MarkerStyle = (Dundas.Charting.WinControl.MarkerStyle)(2);
            }
        }

        private bool CheckChartData(List<PSP_ForecastValues> listValues)
        {
            foreach(PSP_ForecastValues value in listValues)
            {
                if (!(value.Value > (double)decimal.MinValue && value.Value < (double)decimal.MaxValue))
                {
                    return false;
                }
            }

            return true;
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormCalculator fc = new FormCalculator();
            fc.Type = forecastReport.ID.ToString();
            fc.DTable = dataTable;
            fc.TList = this.treeList1;
            fc.ISEdit = EditRight;
            fc.PForecastReports = forecastReport;
            if (fc.ShowDialog() != DialogResult.OK)
                return;
            if (com == "")
                return;
            Forecast(com);
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form1_FT fft = new Form1_FT();
            fft.TYPE = "2";
            fft.TypeFlag2 = forecastReport.ID;
            fft.ShowDialog();

            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            LoadData();
            treeList1.EndUpdate();
            RefreshChart();
            this.Cursor = Cursors.Default;
        }

        private void Form9Forecast_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save();
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dt = ConvertXtrTreeListToDataTable(treeList1);
            FileClass.ExportExcel(dt, forecastReport.Title,false,true);
        }

        private DataTable ConvertXtrTreeListToDataTable(DevExpress.XtraTreeList.TreeList xTreeList)
        {
            DataTable dt = new DataTable();
            List<string> listColID = new List<string>();
            //listColID.Add("Flag");
            // dt.Columns.Add("Flag", typeof(int));

            // listColID.Add("ParentID");
            // dt.Columns.Add("ParentID", typeof(int));

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
                AddNodesDataToDataTable(dt, node, listColID);
            }

            return dt;
        }

        private void AddNodesDataToDataTable(DataTable dt, TreeListNode node, List<string> listColID)
        {
            DataRow newRow = dt.NewRow();
            foreach (string colID in listColID)
            {
                //���������ڶ��㼰�Ժ���ǰ��ӿո�
                if (colID == "Title" && node.ParentNode != null)
                {
                    newRow["Title"] = node[colID];
                }
                else
                {
                    if(colID == "Title")
                        newRow["Title"] = node[colID];
                    else
                        newRow[colID] = node[colID];
                }
            }

            dt.Rows.Add(newRow);

            foreach (TreeListNode nd in node.Nodes)
            {
                AddNodeDataToDataTable(dt, nd, listColID);
            }
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormXiaoshi frm = new FormXiaoshi(forecastReport,typeFlag2);
            //frm.PF = forecastReport;
            frm.ShowDialog();
        }
        private void copydata(DataTable dt1, DataTable dt2/*, ArrayList a2*/)
        {
            foreach (DataColumn dc in dt1.Columns)
                dt2.Columns.Add(dc.ColumnName, dc.DataType);
            foreach (DataRow row1 in dt1.Rows)
            {
                DataRow row2 = dt2.NewRow();
                foreach (DataColumn dc in dt1.Columns)
                {


                    row2[dc.ColumnName] = row1[dc.ColumnName];
                }
              /*  if (a2.Contains(row2["Title"].ToString()))*/
                    dt2.Rows.Add(row2);
            }

        }
        //���ӷ������ݸı�ʱ�������丸�����ֵ
        private void CalculateSum2(TreeListNode node, TreeListColumn column)
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

            CalculateSum2(parentNode, column);
        }

        //����ָ���ڵ�ĸ��и�
        private void CalculateSum2(TreeListNode node)
        {
            foreach (TreeListColumn column in treeList2.Columns)
            {
                if (column.FieldName.IndexOf("��") > 0)
                {
                    CalculateSum2(node, column);
                }
            }
        }
        private void strForecast(string methodName, ref DataTable dt, ArrayList a2)
        {
            Calculator.Type = forecastReport.ID.ToString();
            Calculator.StartYear = forecastReport.StartYear;
            string strtemp = "";
            string strcatch = "";
            int[] inttemp = new int[200];

            if (!CanEdit)
            {
                MsgBox.Show("��û��Ȩ�޽��д��������");
                return;
            }


            DataTable datastemp = new DataTable();
            try
            {
                int nRow = -1;

                if (dataTable.Rows.Count == 0)
                {
                    MsgBox.Show("��ʷ���ݲ���ȷ����ͨ�����������ݡ����ܼ����ʷ���ݡ�");
                    return;
                }

                copydata(dataTable, datastemp/*, a2*/);
                treeList2.DataSource = datastemp;
                foreach (DataRow dataRow in datastemp.Rows)
                {
                    TreeListNode treeNode = treeList2.FindNodeByKeyID(dataRow["ID"]);
                    strtemp = "(" + hash[methodName] + ")";
                    //���ӽڵ�Ĳ�����ֵ��ͨ���ӽڵ���ӵõ�
                    if (!treeNode.HasChildren)
                    {
                        int forecastYears = forecastReport.EndYear - forecastReport.StartYear;
                        double[] historyValues = GenerateHistoryValue(treeNode);

                        double[] forecastValue = null;
                        nRow++;
                        switch (methodName)
                        {
                            case "ArverageIncreasingMethod":
                                forecastValue = Calculator.ArverageIncreasingMethod(historyValues,
                                    (double)dataRow[forecastReport.StartYear + "��"], forecastYears, nRow + 1);

                                break;


                            case "SpringCoefficientMethod":
                                forecastValue = Calculator.SpringCoefficientMethod(historyValues[historyValues.Length - 1], forecastYears);

                                break;
                            //case "SpringCoefficientMethod":
                            //    forecastValue = Calculator.SpringCoefficientMethod(historyValues[0], forecastYears);

                            case "TwiceMoveArverageMethod":
                                forecastValue = Calculator.TwiceMoveArverageMethod(historyValues, forecastYears);

                                break;

                            case "GrayMethod":
                                forecastValue = Calculator.GrayMethod(historyValues, forecastYears);

                                break;

                            case "LinearlyRegressMethod":
                                forecastValue = Calculator.LinearlyRegressMethod(historyValues, forecastYears);

                                break;

                            case "IndexIncreaseMethod":
                                forecastValue = Calculator.IndexIncreaseMethod(historyValues, forecastYears);

                                break;

                            case "IndexSmoothMethod":
                                forecastValue = Calculator.IndexSmoothMethod(historyValues, forecastYears);

                                break;
                                
                            case "LinearlyTrend":
                                forecastValue = Calculator.LinearlyTrend(historyValues, forecastYears);

                                break;
                            case "ProfessionalLV":
                                forecastValue = Calculator.ProfessionalLV(/*historyValues,*/ (double)dataRow[forecastReport.StartYear + "��"], forecastYears,Convert.ToInt32(dataRow["ID"].ToString()));

                                break;
                        }
                        for (int i = 0; i < forecastYears; i++)
                        {
                            dataRow[forecastReport.StartYear + i + 1 + "��"] = Math.Round(forecastValue[i], 2);

                        }
                       // dataRow["Title"] += strtemp;
                        //���ڵ�����һ���ӽڵ㣬���ü��㹫ʽ
                        if (treeNode.NextNode == null)
                        {
                            CalculateSum2(treeNode);
                        }

                    }
                 //   else dataRow["Title"] = dataRow["Title"] + strtemp;
                }





            }



            catch
            {
                strcatch = strtemp;
                chart1.Series.Clear();
                MsgBox.Show("��ʷ���ݲ���ȷ����ͨ�����������ݡ����ܼ����ʷ���ݡ�");
            }

            foreach (DataRow da in datastemp.Rows)
            {
                //if (!a2.Contains(da["Title"].ToString()))
                //    continue;
                if (!a2.Contains(da["ID"].ToString()))
                    continue;
                DataRow row2 = dt.NewRow();
                foreach (DataColumn dc in datastemp.Columns)
                {


                    row2[dc.ColumnName] = da[dc.ColumnName];
                    if (strcatch != "")
                    {
                        if (dc.ColumnName.IndexOf("��") > 0)
                            row2[dc.ColumnName] = 0;
                    }
                }
                //row2["Title"].ToString().Substring(row2["Title"].ToString().LastIndexOf("(") + 1, 5);
                //if (!hash.ContainsValue())
                if (strcatch != "")
                {

                    row2["Title"] = strcatch.Substring(1, 5) + " -- ��ʷ���ݼ��ز���ȷ!";
                    dt.Rows.Add(row2);
                    break;
                }
                if (strcatch == "")
                    //if (a2.Contains(row2["Title"].ToString()))
                    if (a2.Contains(row2["ID"].ToString()))
                    {
                        row2["Title"] = row2["Title"] + strtemp;
                        dt.Rows.Add(row2);
                    }

            }
            strcatch = "";

        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //InitMenu();
            //barButtonItem16.ImageIndex = 8;
            ArrayList a1 = new ArrayList();
            ArrayList a2 = new ArrayList();
            Form8XuanZhe form = new Form8XuanZhe();
            form.DT2 = dataTable;
            if (form.ShowDialog() != DialogResult.OK)
                return;
            a1 = form.A1;
            a2 = form.A2;
            DataTable dt = new DataTable();
            foreach (DataColumn dc in dataTable.Columns)
            {
                dt.Columns.Add(dc.ColumnName, dc.DataType);
            }

            string[] strcom = new string[8];
            strcom[0] = "ArverageIncreasingMethod";
            strcom[1] = "SpringCoefficientMethod";
            strcom[2] = "TwiceMoveArverageMethod";
            strcom[3] = "GrayMethod";
            strcom[4] = "LinearlyRegressMethod";
            strcom[5] = "IndexIncreaseMethod";
            strcom[6] = "IndexSmoothMethod";
            strcom[7] = "ProfessionalLV";

            foreach (string strtemp in strcom)
            {
                if (a1.Contains(strtemp))
                {
                    if (forecastReport.HistoryYears < 5 && strtemp == strcom[2])
                    {
                        MsgBox.Show("�ƶ�ƽ����������Ҫ5�����ʷ���ݣ�����ѡ�񽫱����ԣ�");
                        continue;
                    }
                    strForecast(strtemp, ref dt, a2);
                }
            }

            //dataGridView1.DataSource = dt;
            RefreshChart2(dt);
        }
        private void RefreshChart2(DataTable dt)
        {
            List<PSP_ForecastValues> listValues = new List<PSP_ForecastValues>();


            //chart1.DataBindCrossTab(

            foreach (DataRow nd in dt.Rows)
            {
                AddDataToList(nd, listValues, dt);
            }

            chart1.Series.Clear();

            //if (!CheckChartData(listValues))
            //{
            //    MsgBox.Show("Ԥ���������쳣������ͼ�޷���ʾ��");
            //    return;
            //}Title

            chart1.DataBindCrossTab(listValues, "Caption", "Year", "Value", "");
            //chart1.DataBindTable(
            //chart1.DataBindCrossTab(dt, "Title", "Year", "Value", "");
            for (int i = 0; i < chart1.Series.Count; i++)
            {

                chart1.Series[i].Name = (i + 1).ToString() + "." + chart1.Series[i].Name.Substring(0, chart1.Series[i].Name.IndexOf(","));
                chart1.Series[i].Type = Dundas.Charting.WinControl.SeriesChartType.Line;
                chart1.Series[i].MarkerSize = 7;
                chart1.Series[i].MarkerStyle = (Dundas.Charting.WinControl.MarkerStyle)((i + 1) % 10);

            }


        }
        private void AddDataToList(DataRow node, IList<PSP_ForecastValues> list, DataTable dt)
        {
            foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName.IndexOf("��") > 0)
                {
                    object obj = node[col.ColumnName];
                    if (obj != DBNull.Value)
                    {
                        PSP_ForecastValues v = new PSP_ForecastValues();
                        v.ForecastID = forecastReport.ID;
                        v.TypeID = Convert.ToInt32(node["ID"]);
                        v.Caption = node["Title"].ToString() + "," + v.TypeID;
                        v.Year = Convert.ToInt32(col.ColumnName.Replace("��", ""));
                        v.Value = (double)node[col.ColumnName];

                        list.Add(v);
                    }
                }
            }
        }

        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitMenu();
            barButtonItem18.ImageIndex = 8;
            com = "ProfessionalLV";
            //Forecast("ProfessionalLV");
            Save();
            if (!CanEdit)
            {
                MsgBox.Show("��û��Ȩ�޽��д��������");
                return;
            }


            Form9Forecast_Profess frm = new Form9Forecast_Profess(typeFlag2, forecastReport);


            frm.ShowDialog();
            treeList1.BeginUpdate();
            LoadValues();
            treeList1.EndUpdate();
            RefreshChart();

        }

        private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dt = ConvertXtrTreeListToDataTable(treeList1);
            FileClass.ExportExcel(dt, forecastReport.Title, false, true);
        }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "JPEG�ļ�(*.jpg)|*.jpg|BMP�ļ�(*.bmp)|*.bmp|PNG�ļ�(*.png)|*.png";
            if (sf.ShowDialog() != DialogResult.OK)
                return;

            Dundas.Charting.WinControl.ChartImageFormat ci = new Dundas.Charting.WinControl.ChartImageFormat();
            switch (sf.FilterIndex)
            {
                case 0:
                    ci = Dundas.Charting.WinControl.ChartImageFormat.Jpeg;
                    break;

                case 1:
                    ci = Dundas.Charting.WinControl.ChartImageFormat.Bmp;
                    break;

                case 2:
                    ci = Dundas.Charting.WinControl.ChartImageFormat.Png;
                    break;



            }
            this.chart1.SaveAsImage(sf.FileName, ci);
        }

        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormColor fc = new FormColor();
            fc.DT = dataTable;
            fc.ID = forecastReport.ID.ToString();
            if (fc.ShowDialog() == DialogResult.OK)
                RefreshChart();
        }

    }
}