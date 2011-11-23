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
    public partial class FormXiaoshi : FormBase
    {
        DataTable dataTable;
        DataTable dt;

        bool bl = false;

        private int typeFlag2 = 0;
        private int typeFlag = 10000;
        private PSP_ForecastReports forecastReport = null;
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

        public bool CanEdit
        {
            get { return _canEdit; }
            set { _canEdit = value; }
        }
        bool _canPrint = true;


        string com = "";
        string lb = "统调";


        public bool CanPrint
        {
            get { return _canPrint; }
            set { _canPrint = value; }
        }

        public FormXiaoshi(PSP_ForecastReports fr,int flag2)
        {
            InitializeComponent();
            forecastReport = fr;
            typeFlag2 = flag2;
            //Text = fr.Title;
        }

        public FormXiaoshi() { }

        private void HideToolBarButton()
        {
            if (!CanEdit)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void Form9Forecast_Load(object sender, EventArgs e)
        {
            
            System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
            string FuheCanshu1 = "";
            try
            {
                FuheCanshu1 = configurationAppSettings.GetValue("FuheCanshu1", typeof(string)).ToString();
            }
            catch
            { 
            }
            if (FuheCanshu1 != "")
            {
                lb = FuheCanshu1;
                barButtonItemA2.Caption = lb + "用电量";
                barButtonItemA4.Caption = lb + "最大负荷";
            
            }








            HideToolBarButton();
            chart1.Series.Clear();
            Show();
            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            LoadData();
            Compute();
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


            string[,] ia = new string[6, 2];
            ia[0, 0] = "80001";
            ia[1, 0] = "80002";
            ia[2, 0] = "80003";
            ia[3, 0] = "80004";
            ia[4, 0] = "80005";
            ia[5, 0] = "80006";

            ia[0, 1] = "全社会用电量";
            ia[1, 1] = lb+ "用电量";
            ia[2, 1] = "全社会最大负荷";
            ia[3, 1] = lb + "最大负荷";
            ia[4, 1] = "全社会最大负荷利用小时";
            ia[5, 1] = lb + "最大负荷利用小时";



            for (int i = 0; i < 6;i++ )
            {
                string str2 = " ID=" + int.Parse(ia[i, 0]) + " and Flag='" + typeFlag + "' and Flag2='" + forecastReport.ID + "' ";
                IList listTypes1 = Common.Services.BaseService.GetList("SelectPSP_P_TypesByWhere", str2);
                if (listTypes1.Count == 0)
                {
                    PSP_P_Types ppttt = new PSP_P_Types();
                    ppttt.ID = int.Parse(ia[i, 0]);
                    ppttt.Flag2 = forecastReport.ID;
                    ppttt.Flag = typeFlag;
                    ppttt.Title = ia[i, 1];
                    Services.BaseService.Create<PSP_P_Types>(ppttt);
                }


            }







            PSP_P_Types psp_Type = new PSP_P_Types();
            psp_Type.Flag = typeFlag;

            string str = " Flag='" + typeFlag + "' and Flag2='" + forecastReport.ID + "' ";

            IList listTypes = Common.Services.BaseService.GetList("SelectPSP_P_TypesByWhere", str);

            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_P_Types));

            treeList1.DataSource = dataTable;

            treeList1.Columns["Title"].Caption = "分类名";
            treeList1.Columns["Title"].Width = 180;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Flag"].VisibleIndex = -1;
            treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["Flag2"].VisibleIndex = -1;
            treeList1.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;

            string str1 = " flag2=10000 and flag=" + forecastReport.ID;
            IList<PSP_P_Years> listyears = Common.Services.BaseService.GetList<PSP_P_Years>("SelectPSP_P_YearsByWhere", str1);

            foreach (PSP_P_Years pppy in listyears)
            {
                AddColumn(pppy.Year);
            }



            Application.DoEvents();

            LoadValues();

            treeList1.ExpandAll();
            bLoadingData = false;
        }

        //读取数据
        private void LoadValues()
        {
            string str = " TypeID in(80001,80002,80003,80004,80005,80006) and Flag2=" + forecastReport.ID;
            IList<PSP_P_Values> listValues = Common.Services.BaseService.GetList<PSP_P_Values>("SelectPSP_P_ValuesByWhere", str);

            foreach (PSP_P_Values value in listValues)
            {
                TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
                if (node != null)
                {
                    if (value.TypeID == 80003 || value.TypeID == 80004)
                    {
                        value.Value = Math.Round(value.Value, 2);
                        node.SetValue(value.Year + "年", value.Value);
                    }
                    else
                    {
                        value.Value = Math.Round(value.Value);
                        node.SetValue(value.Year + "年", value.Value);
                    }
                }
            }
        }


      


     

        //添加年份后，新增一列
        private void AddColumn(int year)
        {
            if (!dataTable.Columns.Contains(year + "年"))
            dataTable.Columns.Add(year + "年", typeof(double));

            TreeListColumn column = treeList1.Columns.Add();
            column.FieldName = year + "年";
            column.Tag = year;
            column.Caption = year + "年";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = column.ColumnHandle - 2;//有两列隐藏列
            column.ColumnEdit = repositoryItemSpinEdit1;// repositoryItemTextEdit1;
        }

        private void Compute()
        {
            int year=0;
            double d1=0;
            double d2=0;
            double d3=0;
            double d4=0;

            foreach (DataColumn dc in dataTable.Columns)
            {
                if (dc.ColumnName.IndexOf("年") >= 0)
                {
                    try                     
                    {
                        year = int.Parse(dc.ColumnName.Replace("年", ""));
                        if (year <= forecastReport.StartYear)
                        {
                            try
                            {d1 = (double)dataTable.Rows[0][dc.ColumnName];}
                            catch { }
                            try
                            {d2 = (double)dataTable.Rows[1][dc.ColumnName];}
                            catch { }
                            try
                            {d3 = (double)dataTable.Rows[2][dc.ColumnName];}
                            catch { }
                            try
                            { d4 = (double)dataTable.Rows[3][dc.ColumnName]; }
                            catch { }
                            try
                            {
                                if(d3!=0)
                                dataTable.Rows[4][dc.ColumnName] =  Math.Round(d1  / d3);
                            }
                            catch { }
                            try
                            {
                                if (d4 != 0)
                                    dataTable.Rows[5][dc.ColumnName] = Math.Round(d2 / d4);
                            }
                            catch { }
                        }
                        else
                        {
                            try
                            {d1 = (double)dataTable.Rows[0][dc.ColumnName];}
                            catch { }
                            try
                            {d2 = (double)dataTable.Rows[1][dc.ColumnName];}
                            catch { }
                            try
                            {d3 = (double)dataTable.Rows[4][dc.ColumnName];}
                            catch { d3 = 0; }
                            try
                            { d4 = (double)dataTable.Rows[5][dc.ColumnName]; }
                            catch { d4 = 0; }

                            try
                            {
                                if (d3 != 0)
                                dataTable.Rows[2][dc.ColumnName] = Math.Round( d1  / d3,2);
                            }
                            catch { }
                            try
                            {
                                if (d4 != 0)
                                dataTable.Rows[3][dc.ColumnName] = Math.Round( d2  / d4,2);
                            }
                            catch { }
                        }



                    }
                    catch { }
                
                
                }
            
            
            }
        
        
        }

        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            bl = true;
            Compute();
            RefreshChart();
            //if (e.Column.FieldName.IndexOf("年") > 0
            //    && e.Column.Tag != null
            //    && !bLoadingData)
            //{
            //    CalculateSum(e.Node, e.Column);
            //    RefreshChart();
            //}
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (treeList1.FocusedNode.HasChildren
                || !CanEdit)
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

        //计算指定节点的各列各
        private void CalculateSum(TreeListNode node)
        {
            foreach (TreeListColumn column in treeList1.Columns)
            {
                if (column.FieldName.IndexOf("年") > 0)
                {
                    CalculateSum(node, column);
                }
            }
        }

        private void treeList1_ShownEditor(object sender, EventArgs e)
        {
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

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Form1Result f = new Form1Result();
                f.CanPrint = CanPrint;
                f.Text = forecastReport.Title;//  = "本地区" + frm.ListChoosedYears[0].Year + "～" + frm.ListChoosedYears[frm.ListChoosedYears.Count - 1].Year + "年最高负荷预测表";
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

            foreach (TreeListNode nd in node.Nodes)
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
                if (choosedYear.WithIncreaseRate)
                {
                    dtReturn.Columns.Add(choosedYear.Year + "增长率", typeof(double)).Caption = "增长率";
                }
            }

            #region 填充数据
            for (int i = 0; i < sourceDataTable.Rows.Count; i++)
            {
                DataRow newRow = dtReturn.NewRow();
                DataRow sourceRow = sourceDataTable.Rows[i];
                foreach (DataColumn column in dtReturn.Columns)
                {
                    if (column.Caption != "增长率")
                    {
                        newRow[column.ColumnName] = sourceRow[column.ColumnName];
                    }
                }
                dtReturn.Rows.Add(newRow);
            }
            #endregion

            #region 计算增长率
            DataColumn columnBegin = null;
            foreach (DataColumn column in dtReturn.Columns)
            {
                if (column.ColumnName.IndexOf("年") > 0)
                {
                    if (columnBegin == null)
                    {
                        columnBegin = column;
                    }
                }
                else if (column.ColumnName.IndexOf("增长率") > 0)
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

        //根据节点返回此行的历史数据
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
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }
            treeList1.BeginUpdate();
            try
            {
                int nRow = -1;

                if (dataTable.Rows.Count == 0)
                {
                    MsgBox.Show("历史数据不正确，请通过“加载数据”功能检查历史数据。");
                    return;
                }

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    TreeListNode treeNode = treeList1.FindNodeByKeyID(dataRow["ID"]);

                    //有子节点的不用求值，通过子节点相加得到
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
                                    (double)dataRow[forecastReport.StartYear + "年"], forecastYears, nRow + 1);

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
                        }

                        for (int i = 0; i < forecastYears; i++)
                        {
                            dataRow[forecastReport.StartYear + i + 1 + "年"] = Math.Round(forecastValue[i], 2);
                        }

                        //父节点的最后一个子节点，调用计算公式
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
                MsgBox.Show("历史数据不正确，请通过“加载数据”功能检查历史数据。");
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
                MsgBox.Show("您没有权限进行此项操作！");
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

                    //有子节点的不用求值，通过子节点相加得到
                    if (!treeNode.HasChildren)
                    {

                        for (int i = 0; i < forecastYears; i++)
                        {
                            dataRow[forecastReport.StartYear + i + 1 + "年"] = Math.Round(Convert.ToDouble(dtForecast.Rows[nRow][Convert.ToString(forecastReport.StartYear + i + 1)]), 2);
                        }
                        nRow++;

                        //父节点的最后一个子节点，调用计算公式
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

        //年增长率法
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitMenu();
            barButtonItem1.ImageIndex = 8;
            com = "ArverageIncreasingMethod";
            Forecast("ArverageIncreasingMethod");
            //InitDataCompute();
        }

        //弹性系数法
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitMenu();
            barButtonItem2.ImageIndex = 8;
            com = "SpringCoefficientMethod";
            Forecast("SpringCoefficientMethod");
        }

        //移动平均法
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitMenu();
            barButtonItem3.ImageIndex = 8;
            com = "TwiceMoveArverageMethod";
            if (forecastReport.HistoryYears < 5)
            {
                MsgBox.Show("移动平均法至少需要5年的历史数据！");
                return;
            }

            Forecast("TwiceMoveArverageMethod");
            //InitDataCompute();
        }

        //灰色模型
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitMenu();
            barButtonItem4.ImageIndex = 8;
            com = "GrayMethod";
            Forecast("GrayMethod");
            //InitDataCompute();
        }

        //线性回归
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitMenu();
            barButtonItem5.ImageIndex = 8;
            com = "LinearlyRegressMethod";
            Forecast("LinearlyRegressMethod");
            //InitDataCompute();
        }

        //指数增长
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitMenu();
            barButtonItem7.ImageIndex = 8;
            com = "IndexIncreaseMethod";
            Forecast("IndexIncreaseMethod");
            //InitDataCompute();
        }

        //指数平滑
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

        }




        //专家方案，指定增长率，再按指定的增长率计算电量
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
            if (!CanEdit)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }


            FormProfessionalMethod_kim frm = new FormProfessionalMethod_kim(typeFlag2, forecastReport);


            frm.ShowDialog();
            treeList1.BeginUpdate();
            LoadValues();
            treeList1.EndUpdate();
            RefreshChart();

         }

        //保存数据
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (!CanEdit)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }
            Save();
        }


        private void Save()
        {

            textBox1.Focus();

            Cursor = Cursors.WaitCursor;
            List<PSP_P_Values> listValues = new List<PSP_P_Values>();
            foreach (TreeListNode nd in treeList1.Nodes)
            {
                AddNodeDataToList(nd, listValues);
            }

            try
            {
                Common.Services.BaseService.Update<PSP_P_Values>(listValues);
                //MsgBox.Show("数据保存成功！");
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            }

            Cursor = Cursors.Default;
        
        }



        //把节点数据存放到对象中，插入进List
        private void AddNodeDataToList(TreeListNode node, IList<PSP_P_Values> list)
        {
            foreach (TreeListColumn col in treeList1.Columns)
            {
                if (col.FieldName.IndexOf("年") > 0)
                {
                    object obj = node.GetValue(col.FieldName);
                    if (obj != DBNull.Value)
                    {
                        PSP_P_Values v = new PSP_P_Values();
                        v.Flag2 = forecastReport.ID;
                        v.TypeID = (int)node.GetValue("ID");
                        v.Caption = node.GetValue("Title").ToString() + "," + v.TypeID;
                        v.Year = Convert.ToInt32(col.FieldName.Replace("年", ""));
                        v.Value = (double)node.GetValue(col.FieldName);

                        list.Add(v);
                    }
                }
            }
        }

        private void RefreshChart1()
        { }

        private void RefreshChart()
        {
            List<PSP_P_Values> listValues = new List<PSP_P_Values>();
            foreach (TreeListNode nd in treeList1.Nodes)
            {
                if(nd["ID"].ToString()=="80003" || nd["ID"].ToString()=="80004")
                AddNodeDataToList(nd, listValues);
            }
            chart1.Series.Clear();

            //if (!CheckChartData(listValues))
            //{
            //    //MsgBox.Show("预测数据有异常，曲线图无法显示！");
            //    return;
            //}

            chart1.DataBindCrossTab(listValues, "Caption", "Year", "Value", "");
            for (int i = 0; i < chart1.Series.Count; i++)
            {
                chart1.Series[i].Name = (i + 1).ToString() + "." + chart1.Series[i].Name.Substring(0, chart1.Series[i].Name.IndexOf(","));
                chart1.Series[i].Type = Dundas.Charting.WinControl.SeriesChartType.Line;
                chart1.Series[i].MarkerSize = 7;
                chart1.Series[i].MarkerStyle = (Dundas.Charting.WinControl.MarkerStyle)((i + 1) % 10);
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
            ThisClose();
            Close();
        }

        private void ThisClose()
        {
            this.textBox2.Focus();
            if (bl)
            {
                if (MessageBox.Show("数据发生改变，是否保存？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                Save();
                bl = false;

            }
        
        
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
            fft.TYPE = "10000";
            fft.TypeFlag2 = 10000;
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
            ThisClose();
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
            dt.Columns["Title"].Caption = "分类";
            foreach (TreeListColumn column in xTreeList.Columns)
            {
                if (column.FieldName.IndexOf("年") > 0)
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
                //分类名，第二层及以后在前面加空格
                if (colID == "Title" && node.ParentNode != null)
                {
                    newRow[colID] = node[colID];
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

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormXiaoshi frm = new FormXiaoshi();
            //frm.PF = forecastReport;
            frm.ShowDialog();
        }
        private void copydata(DataTable dt1, DataTable dt2, ArrayList a2)
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
                if (a2.Contains(row2["Title"].ToString()))
                    dt2.Rows.Add(row2);
            }

        }
        //当子分类数据改变时，计算其父分类的值
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

        //计算指定节点的各列各
        private void CalculateSum2(TreeListNode node)
        {
            foreach (TreeListColumn column in treeList2.Columns)
            {
                if (column.FieldName.IndexOf("年") > 0)
                {
                    CalculateSum2(node, column);
                }
            }
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
            //    MsgBox.Show("预测数据有异常，曲线图无法显示！");
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
                if (col.ColumnName.IndexOf("年") > 0)
                {
                    object obj = node[col.ColumnName];
                    if (obj != DBNull.Value)
                    {
                        PSP_ForecastValues v = new PSP_ForecastValues();
                        v.ForecastID = forecastReport.ID;
                        v.TypeID = Convert.ToInt32(node["ID"]);
                        v.Caption = node["Title"].ToString() + "," + v.TypeID;
                        v.Year = Convert.ToInt32(col.ColumnName.Replace("年", ""));
                        v.Value = (double)node[col.ColumnName];

                        list.Add(v);
                    }
                }
            }
        }

        private void barButtonItemA1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetFFData(80001, "1", "全社会用电量");
        }

        private void barButtonItemA2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetFFData(80002, "1", lb+ "用电量");
        }

        private void barButtonItemA3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetFFData(80003, "2", "全社会最大负荷");
        }

        private void barButtonItemA4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetFFData(80004, "2", lb + "最大负荷");
        }


        private void GetFFData(int sid,string type,string title)
        {
            FormXiaoshi_Fs ffs = new FormXiaoshi_Fs();
            ffs.Type = type;
            ffs.ForecastReports = forecastReport;

            if (ffs.ShowDialog() != DialogResult.OK)
                return;

            Hashtable hs = ffs.HS;

            if (hs.Count != 3)
                return;

            int uid = (int)hs["H1"];
            TreeListNode tn = (TreeListNode)hs["H2"];
            bool bool1 = (bool)hs["H3"];

            PSP_P_Types pt=new PSP_P_Types();
            if (bool1)
            {
                PSP_Types pt1 = Services.BaseService.GetOneByKey<PSP_Types>(uid);
                InitTypes(pt1, pt);
            }
            else
            {
                pt = Services.BaseService.GetOneByKey<PSP_P_Types>(uid);
            }

            #region 设定types

            string str2 = " ID=" + sid + " and Flag='" + typeFlag + "' and Flag2='" + forecastReport.ID + "' ";
            IList listTypes = Common.Services.BaseService.GetList("SelectPSP_P_TypesByWhere", str2);
            if (listTypes.Count != 0)
            {
                PSP_P_Values ppvvv = new PSP_P_Values();
                ppvvv.TypeID = sid;
                ppvvv.Flag2 = forecastReport.ID;
                Services.BaseService.Update("DeletePSP_P_ValuesByType", ppvvv);
            }

            pt.ID = sid;
            pt.Flag2 = forecastReport.ID;
            pt.Flag = typeFlag;
            pt.Title = title;
            Services.BaseService.Create<PSP_P_Types>(pt);
            #endregion



            



            #region 设定year value

            Hashtable hta = new Hashtable();


            ArrayList al = new ArrayList();
            foreach (TreeListColumn tlc in tn.TreeList.Columns)
            {
                if (tlc.FieldName.IndexOf("年") >= 0)
                {
                    int year = 0;
                    try
                    {
                        year=int.Parse(tlc.FieldName.Replace("年", ""));
                    }
                    catch { }

                    if (year != 0 && year > forecastReport.EndYear)
                        continue;

                    hta.Add(Guid.NewGuid().ToString(), year);

                    try
                    {
                        PSP_P_Values pv = new PSP_P_Values();
                        pv.Year=year;
                        pv.TypeID=sid;
                        try
                        {
                            pv.Value = double.Parse(tn[tlc.FieldName].ToString());
                        }
                        catch { }
                        pv.Flag2 = forecastReport.ID;
                        Services.BaseService.Create<PSP_P_Values>(pv);
                    }
                    catch { }


                    try
                    {
                        PSP_P_Years ppy = new PSP_P_Years();
                        ppy.Flag = forecastReport.ID;
                        ppy.Flag2 = typeFlag;
                        ppy.Year = year;
                        Services.BaseService.Create<PSP_P_Years>(ppy);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message); 
                    }
                }
            }


            for (int m = forecastReport.StartYear; m <= forecastReport.EndYear; m++)
            {
                if (!hta.ContainsValue(m))
                {
                    try
                    {
                        PSP_P_Years ppy = new PSP_P_Years();
                        ppy.Flag = forecastReport.ID;
                        ppy.Flag2 = typeFlag;
                        ppy.Year = m;
                        Services.BaseService.Create<PSP_P_Years>(ppy);
                    }
                    catch (Exception ex)
                    { //MessageBox.Show(ex.Message); 
                    }
                
                }


            }



            #endregion




            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            LoadData();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
            Compute();
            RefreshChart();
        }

        private void treeList1_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            Brush brush = null;
            Rectangle r = e.Bounds;
            int year = 0;
            Color c1 = Color.FromArgb(231, 235, 255);
            Color c2 = Color.FromArgb(231, 235, 255);
            if (e.Column.FieldName.IndexOf("年") >= 0 && (e.Node["ID"].ToString() == "80005" || e.Node["ID"].ToString() == "80006") && !e.Node.Focused)
            {
                try
                {
                    year = int.Parse(e.Column.FieldName.Replace("年", ""));
                    if (year > forecastReport.StartYear)
                    {
                        brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, c1, c2, 180);
                        if (brush != null)
                        {
                            e.Graphics.FillRectangle(brush, r);
                        }
                    }
                }
                catch { }


            }
            else
                if (e.Column.FieldName.IndexOf("年") >= 0 && (e.Node["ID"].ToString() == "80005" || e.Node["ID"].ToString() == "80006"))
                {
                    Color c3 = Color.FromArgb(48, 23, 145);
                    try
                    {
                        year = int.Parse(e.Column.FieldName.Replace("年", ""));
                        if (year > forecastReport.StartYear)
                        {
                            brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, c3, c3, 180);
                            if (brush != null)
                            {
                                e.Graphics.FillRectangle(brush, r);
                            }
                        }
                    }
                    catch { }

                }
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

        private void barButtonItem13_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //删除

            if (treeList1.FocusedColumn == null)
            {
                return;
            }

            //不是年份列
            if (treeList1.FocusedColumn.FieldName.IndexOf("年") == -1)
            {
                return;
            }

            ////if (!base.DeleteRight)
            ////{
            ////    MsgBox.Show("您没有权限进行此项操作！");
            ////    return;
            ////}

            if (MsgBox.ShowYesNo("是否删除 " + treeList1.FocusedColumn.FieldName + " 及该年的所有数据？") != DialogResult.Yes)
            {
                return;
            }


            PSP_P_Values psp_Values = new PSP_P_Values();
            psp_Values.ID = forecastReport.ID;//借用ID属性存放Flag2
            psp_Values.Year = (int)treeList1.FocusedColumn.Tag;

            try
            {
                //DeletePSP_ValuesByYear删除数据和年份
                int colIndex = treeList1.FocusedColumn.AbsoluteIndex;
                Common.Services.BaseService.Update("DeletePSP_P_ValuesByYear", psp_Values);
                dataTable.Columns.Remove(treeList1.FocusedColumn.FieldName);
                treeList1.Columns.Remove(treeList1.FocusedColumn);
                if (colIndex >= treeList1.Columns.Count)
                {
                    colIndex--;
                }
                treeList1.FocusedColumn = treeList1.Columns[colIndex];
                RefreshChart();
            }
            catch (Exception ex)
            {
                MsgBox.Show("删除出错：" + ex.Message);
            }

        }

        private void barButtonItem9_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ////////增加
            //////if (!base.AddRight)
            //////{
            //////    MsgBox.Show("您没有权限进行此项操作！");
            //////    return;
            //////}
            FormNewYearXiaoshi frm = new FormNewYearXiaoshi();
            frm.Flag2 = forecastReport.ID;
            int nFixedColumns = typeof(PSP_P_Types).GetProperties().Length;
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

    }
}