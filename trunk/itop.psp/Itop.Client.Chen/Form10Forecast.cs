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
using Itop.Domain.Stutistic;

namespace Itop.Client.Chen
{
    public partial class Form10Forecast : FormBase
    {
        DataTable dataTable;

        private int typeFlag = 0;
        private PSP_ForecastReports forecastReport = null;
        bool bLoadingData = false;
        private double _increasingRate = 0.1385;
        bool _canEdit = true;

        bool _isSelect = false;

        public bool IsSelect
        {
            get { return _isSelect; }
            set { _isSelect = value; }
        }

        public bool CanEdit
        {
            get { return _canEdit; }
            set { _canEdit = value; }
        }
        bool _canPrint = true;

        public bool CanPrint
        {
            get { return _canPrint; }
            set { _canPrint = value; }
        }

        public Form10Forecast(PSP_ForecastReports fr, int tf)
        {
            InitializeComponent();
            forecastReport = fr;
            typeFlag = tf;
            Text = fr.Title;
        }

        private void HideToolBarButton()
        {
            if (!_canPrint)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!_canEdit)
            {
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (!IsSelect)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void Form10Forecast_Load(object sender, EventArgs e)
        {
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
            dataTable = new DataTable();
            dataTable.Columns.Add("TypeID", typeof(int));
            dataTable.Columns.Add("Title", typeof(string));
            for (int i = 1; i < 16; i++)
            {
                DataColumn dc = dataTable.Columns.Add(i.ToString() +  "��", typeof(double));
            }

            treeList1.DataSource = dataTable;
            treeList1.Columns["Title"].Caption = "�·�";
            for (int i = 1; i < 14; i++)
            {
                treeList1.Columns[i.ToString() + "��"].Caption = i.ToString();
                treeList1.Columns[i.ToString() + "��"].Width = 50; ;
                treeList1.Columns[i.ToString() + "��"].ColumnEdit = repositoryItemTextEdit1;
            }
            treeList1.Columns["13��"].Caption = "�ϼ�";
            treeList1.Columns["13��"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["14��"].Caption = "����󸺺�";
            treeList1.Columns["14��"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["15��"].Caption = "���������ʦ�";
            treeList1.Columns["15��"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["15��"].Format.FormatString = "p2";

            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Title"].Width = 140;
            treeList1.Columns["14��"].Width = 80;
            treeList1.Columns["15��"].Width = 80;

            dataTable.Rows.Add(new object[] { 1, forecastReport.StartYear.ToString() + "������󸺺�" });
            dataTable.Rows.Add(new object[] { 2, forecastReport.EndYear.ToString() + "������󸺺�" });

            Application.DoEvents();

            LoadValues();

            treeList1.ExpandAll();
            bLoadingData = false;
        }

        //��ȡ����
        private void LoadValues()
        {
            PSP_ForecastValues psp_Value = new PSP_ForecastValues();
            psp_Value.ForecastID = forecastReport.ID;

            IList<PSP_ForecastValues> listValues = Common.Services.BaseService.GetList<PSP_ForecastValues>("SelectPSP_ForecastValuesByForecastID", psp_Value);

            if (listValues.Count == 0)
            {
                BurdenMonth bm1 = new BurdenMonth();
                bm1.BurdenYear = forecastReport.StartYear;

                BurdenMonth bm = (BurdenMonth)Common.Services.BaseService.GetObject("SelectBurdenMonthByBurdenYear", bm1);
                if (bm != null)
                {
                    //TreeListNode node = treeList1.FindNodeByFieldValue("TypeID", value.TypeID);
                    //if (node != null)
                    //{
                    //    node.SetValue(value.Year + "��", value.Value);
                    //}
                    treeList1.MoveFirst();
                    TreeListNode node = treeList1.FocusedNode;
                    node.SetValue("1��", bm.Month1);
                    node.SetValue("2��", bm.Month2);
                    node.SetValue("3��", bm.Month3);
                    node.SetValue("4��", bm.Month4);
                    node.SetValue("5��", bm.Month5);
                    node.SetValue("6��", bm.Month6);
                    node.SetValue("7��", bm.Month7);
                    node.SetValue("8��", bm.Month8);
                    node.SetValue("9��", bm.Month9);
                    node.SetValue("10��", bm.Month10);
                    node.SetValue("11��", bm.Month11);
                    node.SetValue("12��", bm.Month12);


                    double bmsum=bm.Month1+bm.Month2+bm.Month3+bm.Month4+bm.Month5+bm.Month6;
                    bmsum+=bm.Month7+bm.Month8+bm.Month9+bm.Month10+bm.Month11+bm.Month12;

                    double bmmax = Math.Max(bm.Month1, bm.Month2);
                    bmmax = Math.Max(bmmax, bm.Month3);
                    bmmax = Math.Max(bmmax, bm.Month4);
                    bmmax = Math.Max(bmmax, bm.Month5);
                    bmmax = Math.Max(bmmax, bm.Month6);
                    bmmax = Math.Max(bmmax, bm.Month7);
                    bmmax = Math.Max(bmmax, bm.Month8);
                    bmmax = Math.Max(bmmax, bm.Month9);
                    bmmax = Math.Max(bmmax, bm.Month10);
                    bmmax = Math.Max(bmmax, bm.Month11);
                    bmmax = Math.Max(bmmax, bm.Month12);




                    node.SetValue("13��", bmsum);
                    node.SetValue("14��", bmmax);
                    node.SetValue("15��", bmsum / (bmmax*12));

                }



            }

            else
            {
                foreach (PSP_ForecastValues value in listValues)
                {
                    TreeListNode node = treeList1.FindNodeByFieldValue("TypeID", value.TypeID);
                    if (node != null)
                    {
                        node.SetValue(value.Year + "��", value.Value);
                    }
                }
            }
        }

        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.IndexOf("��") > 0
                && !bLoadingData
                && Convert.ToInt32(e.Column.FieldName.Replace("��","")) < 13)
            {
                CalculateSum(e.Node);
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

        //���������ֵ
        private void CalculateSum(TreeListNode node)
        {
            double total = 0.0;//��
            double max = 0.0;//���ֵ

            for (int i = 1; i < 13; i++ )
            {
                object v = node.GetValue(i.ToString() + "��");
                if (v != DBNull.Value)
                {
                    double dv = (double)v;
                    total += dv;
                    if(max < dv)
                    {
                        max = dv;
                    }
                }
            }

            node.SetValue("13��", Math.Round(total, 4));
            node.SetValue("14��", Math.Round(max, 4));
            node.SetValue("15��", Math.Round(total / 12.0 / max, 4));
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
                f.Text = "������" + frm.ListChoosedYears[0].Year + "��" + frm.ListChoosedYears[frm.ListChoosedYears.Count - 1].Year + "����߸���Ԥ���";
                f.GridDataTable = ResultDataTable(ConvertTreeListToDataTable(treeList1), frm.ListChoosedYears);
                f.ShowDialog();
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

        //���㣬ָ�������ʣ��ٰ�ָ���������ʼ���
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!CanEdit)
            {
                MsgBox.Show("��û��Ȩ�޽��д��������");
                return;
            }

            FormIncreasingRate frm = new FormIncreasingRate();
            frm.IncreasingRate = _increasingRate;
            if (frm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            _increasingRate = frm.IncreasingRate;
            treeList1.BeginUpdate();
            try
            {
                double increase = Math.Pow((1 + _increasingRate), forecastReport.EndYear - forecastReport.StartYear);
                for (int i = 1; i < 13; i++)
                {
                    dataTable.Rows[1][i.ToString() + "��"] = Math.Round((double)dataTable.Rows[0][i.ToString() + "��"] * increase, 2);
                }
                CalculateSum(treeList1.Nodes[1]);
                RefreshChart();
            }
            catch(Exception ex)
            {
                chart1.Series.Clear();
                MsgBox.Show("������������쳣���ݣ�" + ex.Message);
            }
            finally
            {
                treeList1.EndUpdate();
            }
        }

        //��������
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            textEdit1.Focus();
            if (!CanEdit)
            {
                MsgBox.Show("��û��Ȩ�޽��д��������");
                return;
            }
            Cursor = Cursors.WaitCursor;
            List<PSP_ForecastValues> listValues = new List<PSP_ForecastValues>();
            foreach (TreeListNode nd in treeList1.Nodes)
            {
                AddNodeDataToList(nd, listValues, true);
            }

            try
            {
                Common.Services.BaseService.Update<PSP_ForecastValues>(listValues);
                MsgBox.Show("���ݱ���ɹ���");
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            }

            Cursor = Cursors.Default;
        }

        //�ѽڵ����ݴ�ŵ������У������List
        private void AddNodeDataToList(TreeListNode node, IList<PSP_ForecastValues> list, bool allCol)
        {
            for (int i = 1; i < (allCol ? 16 : 13); i++)
            {
                object obj = node.GetValue(i.ToString() + "��");
                double value = 0.0;
                if (obj != DBNull.Value)
                {
                    value = (double)obj;
                }
                PSP_ForecastValues v = new PSP_ForecastValues();
                v.ForecastID = forecastReport.ID;
                v.TypeID = (int)node.GetValue("TypeID");
                v.Caption = node.GetValue("Title").ToString();
                v.Year = i;
                v.Value = value;

                list.Add(v);
            }

            foreach (TreeListNode nd in node.Nodes)
            {
                AddNodeDataToList(nd, list, allCol);
            }
        }

        private void RefreshChart()
        {
            List<PSP_ForecastValues> listValues = new List<PSP_ForecastValues>();
            foreach (TreeListNode nd in treeList1.Nodes)
            {
                AddNodeDataToList(nd, listValues, false);
            }
            chart1.Series.Clear();

            if (!CheckChartData(listValues))
            {
                MsgBox.Show("�������쳣������ͼ�޷���ʾ��");
                return;
            }

            chart1.DataBindCrossTab(listValues, "Caption", "Year", "Value", "");
            for (int i = 0; i < chart1.Series.Count; i++)
            {
                chart1.Series[i].Name = chart1.Series[i].Name.Substring(0, chart1.Series[i].Name.IndexOf("��") + 1);
                chart1.Series[i].Type = Dundas.Charting.WinControl.SeriesChartType.Line;
                chart1.Series[i].MarkerSize = 7;
                chart1.Series[i].MarkerStyle = (Dundas.Charting.WinControl.MarkerStyle)(i + 1);
            }
        }

        private bool CheckChartData(List<PSP_ForecastValues> listValues)
        {
            foreach(PSP_ForecastValues value in listValues)
            {
                if(!(value.Value > double.MinValue && value.Value < double.MaxValue))
                {
                    return false;
                }
            }

            return true;
        }

        //��ӡ
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!CanPrint)
            {
                MsgBox.Show("��û�д�ӡȨ�ޣ�");
                return;
            }

            Common.ComponentPrint.ShowPreview(treeList1, this.Text, true, new Font("����", 16, FontStyle.Bold));
        }

        //����
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (IsSelect)
            {
                gridControl1.BeginUpdate();
                gridControl1.DataSource = dataTable;

                gridView1.Columns["Title"].Caption = "�·�";
                for (int i = 1; i < 14; i++)
                {
                    gridView1.Columns[i.ToString() + "��"].Caption = i.ToString();
                    gridView1.Columns[i.ToString() + "��"].Width = 50; ;
                }
                gridView1.Columns["13��"].Caption = "";
                gridView1.Columns["14��"].Caption = "����󸺺�";
                gridView1.Columns["15��"].Caption = "���������ʦ�";
                gridView1.Columns["15��"].DisplayFormat.FormatString = "p2";
                gridView1.Columns["15��"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;

                gridView1.Columns["Title"].Width = 140;
                gridView1.Columns["14��"].Width = 80;
                gridView1.Columns["15��"].Width = 80;
                gridView1.Columns["TypeID"].Visible = false;
                gridControl1.EndUpdate();

                DialogResult = DialogResult.OK;

            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControl1.BeginUpdate();
            gridControl1.DataSource = dataTable;

            gridView1.Columns["Title"].Caption = "�·�";
            for (int i = 1; i < 14; i++)
            {
                gridView1.Columns[i.ToString() + "��"].Caption = i.ToString();
                gridView1.Columns[i.ToString() + "��"].Width = 50; ;
            }
            gridView1.Columns["13��"].Caption = "";
            gridView1.Columns["14��"].Caption = "����󸺺�";
            gridView1.Columns["15��"].Caption = "���������ʦ�";
            gridView1.Columns["15��"].DisplayFormat.FormatString = "p2";
            gridView1.Columns["15��"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;

            gridView1.Columns["Title"].Width = 140;
            gridView1.Columns["14��"].Width = 80;
            gridView1.Columns["15��"].Width = 80;
            gridView1.Columns["TypeID"].Visible = false;
            gridControl1.EndUpdate();

            //FileClass.ExportExcel(this.gridControl1);
            FileClass.ExportExcel("����󸺺�Ԥ��", "", this.gridControl1);
        }

        private InputLanguage oldInput = null;
        private void treeList1_FocusedColumnChanged(object sender, DevExpress.XtraTreeList.FocusedColumnChangedEventArgs e)
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
    }
}