using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using Itop.Domain.HistoryValue;
using System.Collections;
using Itop.Common;
using Itop.Domain.Chen;

namespace Itop.Client.Chen
{
    public partial class Form9Forecast_Profess : DevExpress.XtraEditors.XtraForm
    {
        private DataTable dataTable = null;
        private DataTable dataTable2 = null;
        private int typeFlag = 10000;
        private int typeFlag2 = 0;
        private PSP_ForecastReports forecastReport = null;
        static double defaultPercent = 0.1;
        bool bl1 = false;
        bool bl2 = false;
        private bool formselect = false;
        public DataTable DataTable
        {
            get { return dataTable; }
            set { dataTable = value; }
        }
        public bool FormSelect
        {
            set { formselect = value; }
        }

        public Form9Forecast_Profess(int flag2, PSP_ForecastReports fr)
        {
            typeFlag2 = flag2;
            forecastReport = fr;
            InitializeComponent();
        }

        private void FormProfessionalMethod_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;



            treeList2.BeginUpdate();
            LoadData2();
            treeList2.EndUpdate();


            treeList1.BeginUpdate();
            LoadData();
            treeList1.EndUpdate();
            RefreshChart1();
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
            IList listTypes =null;
            if (!formselect)
            {
                psp_Type.Flag2 = forecastReport.ID;
                 listTypes = Common.Services.BaseService.GetList("SelectPSP_P_TypesByFlag2", psp_Type);

                dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_P_Types));

                treeList1.DataSource = dataTable;
            }
            else
            {

                string str = " Flag='" + typeFlag + "' and Flag2='" + forecastReport.ID + "' " + " and ID in(80001,80002,80003) ";

                 listTypes = Common.Services.BaseService.GetList("SelectPSP_P_TypesByWhere", str);

               
                dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_P_Types));
                //DataView datemp = dataTable.DefaultView;
                //datemp.RowFilter = "ID in(80001,80002,80003,80004)";
                treeList1.DataSource = dataTable;
            
            }
            treeList1.Columns["Title"].Caption = "分类名";
            treeList1.Columns["Title"].Width = 180;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Flag"].VisibleIndex = -1;
            treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["Flag2"].VisibleIndex = -1;
            treeList1.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;
             PSP_PowerProject_ProfessValues psp_profess = new PSP_PowerProject_ProfessValues();    
            for (int i = forecastReport.StartYear + 1; i <= forecastReport.EndYear; i++)
            {
                AddColumn(i);
                foreach (PSP_P_Types psp_Typetemp in listTypes)
                {
                    psp_profess.TypeID = psp_Typetemp.ID;
                    psp_profess.Value=0.1;
                    psp_profess.Year = i;
                    psp_profess.Flag2 = psp_Typetemp.Flag2;
                    try
                    {
                        Common.Services.BaseService.Update("CreatPSP_PowerProject_ProfessValues", psp_profess);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("加载数据出错：" + ex.Message);
                        
                    }
                }
            }
           
           
            //foreach (TreeListNode nd in treeList1.Nodes)
            //{
            //    SetDefaultValues(nd);
            //}
            LoadValueData();
            treeList1.ExpandAll();
        }






        #region 加载基础数据

        private void LoadData2()
        {
            //bLoadingData = true;
            if (dataTable2 != null)
            {
                dataTable2.Columns.Clear();
                treeList2.Columns.Clear();
            }

            //PSP_P_Types psp_Type = new PSP_P_Types();
            //psp_Type.Flag2 = forecastReport.ID;
            //IList listTypes = Common.Services.BaseService.GetList("SelectPSP_P_TypesByFlag2", psp_Type);
            if (!formselect)
            {
                string stra = " Flag2='" + forecastReport.ID + "' and id not in(80001,80002,80003,80004,80005,80006) ";
                IList listTypes = Common.Services.BaseService.GetList("SelectPSP_P_TypesByWhere", stra);
                dataTable2 = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_P_Types));

                treeList2.DataSource = dataTable2;
            }
            else
            {

                string stra = " Flag2='" + forecastReport.ID + "' and id in(80001,80002,80003) ";
                IList listTypes = Common.Services.BaseService.GetList("SelectPSP_P_TypesByWhere", stra);
                dataTable2 = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_P_Types));

                treeList2.DataSource = dataTable2;
            }
            treeList2.Columns["Title"].Caption = "分类名";
            treeList2.Columns["Title"].Width = 180;
            treeList2.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList2.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList2.Columns["Flag"].VisibleIndex = -1;
            treeList2.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
            treeList2.Columns["Flag2"].VisibleIndex = -1;
            treeList2.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;

            for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
            {
                AddColumn2(i);
            }
            Application.DoEvents();

            LoadValues();

            //LoadHistoryValue();
            treeList2.ExpandAll();
            //bLoadingData = false;
        }

        //读取数据
        private void LoadValues()
        {
            if (!formselect)
            {
                PSP_ForecastValues psp_Value = new PSP_ForecastValues();
                psp_Value.ForecastID = forecastReport.ID;

                IList<PSP_ForecastValues> listValues = Common.Services.BaseService.GetList<PSP_ForecastValues>("SelectPSP_ForecastValuesByForecastID", psp_Value);
                foreach (PSP_ForecastValues value in listValues)
                {
                    TreeListNode node = treeList2.FindNodeByFieldValue("ID", value.TypeID);
                    if (node != null && value.Year >= forecastReport.StartYear && value.Year <= forecastReport.EndYear)
                    {
                        node.SetValue(value.Year + "年", value.Value);
                    }
                }
            }
            else
            {
                string str = "Flag2=" + forecastReport.ID;
                IList<PSP_P_Values> listValues = Common.Services.BaseService.GetList<PSP_P_Values>("SelectPSP_P_ValuesByWhere", str);
                foreach (PSP_P_Values value in listValues)
                {
                    TreeListNode node = treeList2.FindNodeByFieldValue("ID", value.TypeID);
                    if (node != null && value.Year >= forecastReport.StartYear && value.Year <= forecastReport.EndYear)
                    {
                        node.SetValue(value.Year + "年", value.Value);
                    }
                }
            }
           
        }

        //添加年份后，新增一列
        private void AddColumn2(int year)
        {
            dataTable2.Columns.Add(year + "年", typeof(double));

            TreeListColumn column = treeList2.Columns.Add();
            column.FieldName = year + "年";
            column.Tag = year;
            column.Caption = year + "年";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = column.ColumnHandle - 2;//有两列隐藏列
            column.OptionsColumn.AllowEdit = false;
            //column.ColumnEdit = repositoryItemTextEdit1;
        }

        //读取历史数据
        private void LoadHistoryValue()
        {
            IList<PSP_P_Years> listYears;
            if (!formselect)
            {
                PSP_P_Years psp_Year = new PSP_P_Years();
                psp_Year.Flag = forecastReport.ID;
                listYears = Common.Services.BaseService.GetList<PSP_P_Years>("SelectPSP_P_YearsListByFlag", psp_Year);
            }
                 
            else
            {
                PSP_P_Years psp_Year = new PSP_P_Years();


                psp_Year.Flag = forecastReport.ID;

                psp_Year.Flag2 = typeFlag;
                listYears = Common.Services.BaseService.GetList<PSP_P_Years>("SelectPSP_P_YearsListhaveflag2ByFlag", psp_Year);
            
            }
                foreach (PSP_P_Years item in listYears)
                {
                    try
                    {
                        dataTable2.Columns.Add(item.Year + "Y", typeof(double));

                        TreeListColumn column = treeList2.Columns.Add();
                        column.FieldName = item.Year + "Y";
                        column.Tag = item.Year;
                        column.VisibleIndex = -1;
                    }
                    catch { }
                }

                PSP_Values psp_Values = new PSP_Values();
                psp_Values.ID = forecastReport.ID;//用ID字段存放Flag2的值

                IList<PSP_P_Values> listValues = Common.Services.BaseService.GetList<PSP_P_Values>("SelectPSP_P_ValuesListByFlag2", psp_Values);

                foreach (PSP_P_Values value in listValues)
                {
                    TreeListNode node = treeList2.FindNodeByFieldValue("ID", value.TypeID);
                    if (node != null)
                    {
                        node.SetValue(value.Year + "Y", value.Value);

                        if (value.Year == forecastReport.StartYear)
                        {
                            node.SetValue(value.Year + "年", value.Value);
                        }
                    }
                }
       
        }





#endregion
















        //////
        ////private void SetDefaultValues(TreeListNode node)
        ////{
        ////    if (node.HasChildren)
        ////    {
        ////        foreach (TreeListNode nd in node.Nodes)
        ////        {
        ////            SetDefaultValues(nd);
        ////        }
        ////    }
        ////    else
        ////    {
        ////        for (int i = forecastReport.StartYear + 1; i <= forecastReport.EndYear; i++)
        ////        {
        ////            node.SetValue(i + "年", defaultPercent);
        ////        }
        ////    }
        ////}

        //添加年份后，新增一列
        private void AddColumn(int year)
        {
            dataTable.Columns.Add(year + "年", typeof(double));

            TreeListColumn column = treeList1.Columns.Add();
            column.FieldName = year + "年";
            column.Tag = year;
            column.Caption = year + "年";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = column.ColumnHandle - 2;//有两列隐藏列
            column.ColumnEdit = repositoryItemTextEdit1;
            column.Format.FormatType = DevExpress.Utils.FormatType.Custom;
            column.Format.FormatString = "p2";
            //column.OptionsColumn.AllowEdit = false;
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (treeList1.FocusedNode.HasChildren)
            {
                e.Cancel = true;
            }
        }

        private void treeList1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            bl2 = true;
            if(!bl1)
            InittreeList2();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            InittreeList2();
            
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bl2)
            {
                if (MessageBox.Show("是否保存数据?", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    InitSave();
                else bl2 = false;
            
            
            }
            this.Close();

            //this.DialogResult = DialogResult.Cancel;
        }



        private void InittreeList2()
        {
            bl1 = true;
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
                return;

            TreeListColumn tnc = treeList1.FocusedColumn;
            if (tnc.Caption.IndexOf("年") < 0)
                return;


            //FormNewZengZhangLv fnf = new FormNewZengZhangLv();

            //if (fnf.ShowDialog() != DialogResult.OK)
            //    return;



            double fuhe =0;
            try
            {
                fuhe = double.Parse(tln[tnc.FieldName].ToString());
            }
            catch { return; }


            int a = 0;
            try { a = int.Parse(tnc.Caption.Replace("年", "")); }
            catch { }
            int b = forecastReport.EndYear;

            double fh = 0;


            for (int i = a; i <= b; i++)
            {
                fh = 0;
                //try { fh = (double)tln[i + "年"]; }
                //catch { }

                tln.SetValue(i + "年", fh + fuhe);
            }
            
            
            
            
            
            
            treeList2.BeginUpdate();
            try
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow dr = dataTable2.Rows[i];
                    TreeListNode node = treeList2.FindNodeByKeyID(dr["ID"]);
                    if (!node.HasChildren)
                    {
                        foreach (DataColumn col in dataTable.Columns)
                        {
                            if (col.ColumnName.IndexOf("年") > 0)
                            {
                                int nPreYear = Convert.ToInt32(col.ColumnName.Replace("年", "")) - 1;
                                dr[col.ColumnName] = Math.Round((double)dr[nPreYear + "年"] * (1 + (double)dataTable.Rows[i][col]), 2);
                            }
                        }

                        //父节点的最后一个子节点，调用计算公式
                        if (node.NextNode == null)
                        {
                            CalculateSum(node);
                        }
                    }
                }
                //RefreshChart();
            }
            catch
            {
                //chart1.Series.Clear();
                MsgBox.Show("计算出错：出现异常数据，请查检历史数据和增长率！");
            }
            finally
            {
                treeList2.EndUpdate();
            }

            RefreshChart1();
            bl1 = false;
        }










        private void InittreeList3()
        {

            foreach (TreeListNode tln in treeList1.Nodes)
            {
                foreach (TreeListColumn tnc in treeList1.Columns)
                {

                    if (tnc.Caption.IndexOf("年") < 0)
                        continue;

                    double fuhe = 0;
                    try
                    {
                        fuhe = double.Parse(tln[tnc.FieldName].ToString());
                    }
                    catch {  }


                    int a = 0;
                    try { a = int.Parse(tnc.Caption.Replace("年", "")); }
                    catch { }
                    int b = forecastReport.EndYear;

                    double fh = 0;


                    for (int i = a; i <= b; i++)
                    {
                        fh = 0;
                        tln.SetValue(i + "年", fh + fuhe);
                    }






                    treeList2.BeginUpdate();
                    try
                    {
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            DataRow dr = dataTable2.Rows[i];
                            TreeListNode node = treeList2.FindNodeByKeyID(dr["ID"]);
                            if (!node.HasChildren)
                            {
                                foreach (DataColumn col in dataTable.Columns)
                                {
                                    if (col.ColumnName.IndexOf("年") > 0)
                                    {
                                        int nPreYear = Convert.ToInt32(col.ColumnName.Replace("年", "")) - 1;
                                        dr[col.ColumnName] = Math.Round((double)dr[nPreYear + "年"] * (1 + (double)dataTable.Rows[i][col]), 2);
                                    }
                                }

                                //父节点的最后一个子节点，调用计算公式
                                if (node.NextNode == null)
                                {
                                    CalculateSum(node);
                                }
                            }
                        }
                        //RefreshChart();
                    }
                    catch
                    {
                        //chart1.Series.Clear();
                        MsgBox.Show("计算出错：出现异常数据，请查检历史数据和增长率！");
                    }
                    finally
                    {
                        treeList2.EndUpdate();
                    }
                }
            }
            RefreshChart1();
            bl1 = false;
        }






        //读取数据
        private void LoadValueData()
        {
            //PSP_Values psp_Values = new PSP_Values();
            //psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值

            IList<PSP_PowerProject_ProfessValues> listValues = Common.Services.BaseService.GetList<PSP_PowerProject_ProfessValues>("SelectPSP_PowerProject_ProfessValuesByFlag2", forecastReport.ID);

            foreach (PSP_PowerProject_ProfessValues value in listValues)
            {
                TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
                if (node != null&& value.Year >= forecastReport.StartYear+1 && value.Year <= forecastReport.EndYear)
                {
                    
                    node.SetValue(value.Year + "年", value.Value);
                }
            }
        }


        //
        ////////////private void SetDefaultValues(TreeListNode node)
        ////////////{
        ////////////    if (node.HasChildren)
        ////////////    {
        ////////////        foreach (TreeListNode nd in node.Nodes)
        ////////////        {
        ////////////            SetDefaultValues(nd);
        ////////////        }
        ////////////    }
        ////////////    else
        ////////////    {

        ////////////        for (int i = forecastReport.StartYear + 1; i <= forecastReport.EndYear; i++)
        ////////////        {
        ////////////            //foreach (DataRow row in dataTable2.Rows)
        ////////////            //{
        ////////////            //    if (row["Title"].ToString() != node["Title"].ToString())
        ////////////            //        continue;
        ////////////            PSP_PowerProject_ProfessValues psp_profess = new PSP_PowerProject_ProfessValues();
        ////////////            psp_profess.TypeID = psp_Typetemp.ID;
        ////////////            psp_profess.Year = i;
        ////////////            psp_profess.Flag2 = psp_Typetemp.Flag2;
        ////////////                try
        ////////////                {
        ////////////                    double d1 = (double)row[i.ToString() + "年"];
        ////////////                    double d2 = (double)row[(i - 1).ToString() + "年"];
        ////////////                    node.SetValue(i + "年", (d1 - d2) / d2);
        ////////////                }
        ////////////                catch { }
        ////////////            //}


        ////////////        }
        ////////////    }
        ////////////}

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitSave();
            //this.Close();
        }


        private void InitSave()
        {
            List<PSP_ForecastValues> listValues = new List<PSP_ForecastValues>();
            foreach (TreeListNode nd in treeList2.Nodes)
            {
                AddNodeDataToList(nd, listValues);
            }
            Common.Services.BaseService.Update<PSP_ForecastValues>(listValues);
                IList<PSP_PowerProject_ProfessValues> listValuestemp = Common.Services.BaseService.GetList<PSP_PowerProject_ProfessValues>("SelectPSP_PowerProject_ProfessValuesByFlag2", forecastReport.ID);
                PSP_PowerProject_ProfessValues psp_profess = new PSP_PowerProject_ProfessValues();
                try
                {
                    foreach (PSP_PowerProject_ProfessValues value in listValuestemp)
                    {
                        if (value.Year > forecastReport.EndYear || value.Year <=forecastReport.StartYear)
                            continue;
                        TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
                        if (node != null)
                        {
                            psp_profess.TypeID = value.TypeID;
                            psp_profess.Value = Convert.ToDouble(node[value.Year + "年"].ToString());
                            psp_profess.Year = value.Year;
                            psp_profess.Flag2 = value.Flag2;
                            //node.SetValue(value.Year + "年", value.Value);
                            Common.Services.BaseService.Update<PSP_PowerProject_ProfessValues>(psp_profess);
                        }
                    }
                   
                    MsgBox.Show("数据保存成功！");
                    bl2 = false;
            }
            catch (Exception ex)
            {
                //MsgBox.Show(ex.Message);
            }
        
        
        }




        private void AddNodeDataToList(TreeListNode node, IList<PSP_ForecastValues> list)
        {
            foreach (TreeListColumn col in treeList2.Columns)
            {
                if (col.FieldName.IndexOf("年") > 0)
                {
                    object obj = node.GetValue(col.FieldName);
                    if (obj != DBNull.Value)
                    {
                        PSP_ForecastValues v = new PSP_ForecastValues();
                        v.ForecastID = forecastReport.ID;
                        v.TypeID = (int)node.GetValue("ID");
                        v.Caption = node.GetValue("Title").ToString() + "," + v.TypeID;
                        v.Year = Convert.ToInt32(col.FieldName.Replace("年", ""));
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

        private void 添加负荷ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeListNode tln = treeList2.FocusedNode;
            if (tln == null)
                return;

            TreeListColumn tnc = treeList2.FocusedColumn;
            if (tnc.Caption.IndexOf("年") < 0)
                return;


            FormNewZengZhangLv fnf = new FormNewZengZhangLv();

            if (fnf.ShowDialog() != DialogResult.OK)
                return;

            double fuhe = fnf.Fuhe;


            int a = 0;
            try { a = int.Parse(tnc.Caption.Replace("年", "")); }
            catch { }
            int b = forecastReport.EndYear;

            double fh = 0;


            for (int i = a; i <= b; i++)
            {
                fh = 0;
                try { fh = (double)tln[i + "年"]; }
                catch { }

                tln.SetValue(i + "年", fh + fuhe);
            }


            treeList1.BeginUpdate();
            LoadData();
            treeList1.EndUpdate();
            RefreshChart1();
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
            foreach (TreeListColumn column in treeList2.Columns)
            {
                if (column.FieldName.IndexOf("年") > 0)
                {
                    CalculateSum(node, column);
                }
            }
        }

        private void RefreshChart1()
        {
        }
        //////private void RefreshChart()
        //////{
        //////    List<PSP_ForecastValues> listValues = new List<PSP_ForecastValues>();
        //////    foreach (TreeListNode nd in treeList2.Nodes)
        //////    {
        //////        AddNodeDataToList(nd, listValues);
        //////    }

        //////    chart1.Series.Clear();

        //////    //if (!CheckChartData(listValues))
        //////    //{
        //////    //    //MsgBox.Show("预测数据有异常，曲线图无法显示！");
        //////    //    return;
        //////    //}

        //////    chart1.DataBindCrossTab(listValues, "Caption", "Year", "Value", "");
        //////    for (int i = 0; i < chart1.Series.Count; i++)
        //////    {
        //////        chart1.Series[i].Name = (i + 1).ToString() + "." + chart1.Series[i].Name.Substring(0, chart1.Series[i].Name.IndexOf(","));
        //////        chart1.Series[i].Type = Dundas.Charting.WinControl.SeriesChartType.Line;
        //////        chart1.Series[i].MarkerSize = 7;
        //////        chart1.Series[i].MarkerStyle = (Dundas.Charting.WinControl.MarkerStyle)((i + 1) % 10);
        //////    }
        //////}

        private bool CheckChartData(List<PSP_ForecastValues> listValues)
        {
            foreach (PSP_ForecastValues value in listValues)
            {
                if (!(value.Value > (double)decimal.MinValue && value.Value < (double)decimal.MaxValue))
                {
                    return false;
                }
            }

            return true;
        }

        private void 指定增长率ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
                return;

            TreeListColumn tnc = treeList1.FocusedColumn;
            if (tnc.Caption.IndexOf("年") < 0)
                return;


            FormNewZengZhangLv fnf = new FormNewZengZhangLv();

            if (fnf.ShowDialog() != DialogResult.OK)
                return;

            double fuhe = fnf.Fuhe;


            int a = 0;
            try { a = int.Parse(tnc.Caption.Replace("年", "")); }
            catch { }
            int b = forecastReport.EndYear;

            double fh = 0;


            for (int i = a; i <= b; i++)
            {
                fh = 0;
                //try { fh = (double)tln[i + "年"]; }
                //catch { }

                tln.SetValue(i + "年", fh + fuhe);
            }




            treeList2.BeginUpdate();
            try
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow dr = dataTable2.Rows[i];
                    TreeListNode node = treeList2.FindNodeByKeyID(dr["ID"]);
                    if (!node.HasChildren)
                    {
                        foreach (DataColumn col in dataTable.Columns)
                        {
                            if (col.ColumnName.IndexOf("年") > 0)
                            {
                                int nPreYear = Convert.ToInt32(col.ColumnName.Replace("年", "")) - 1;
                                dr[col.ColumnName] = Math.Round((double)dr[nPreYear + "年"] * (1 + (double)dataTable.Rows[i][col]), 2);
                            }
                        }

                        //父节点的最后一个子节点，调用计算公式
                        if (node.NextNode == null)
                        {
                            CalculateSum(node);
                        }
                    }
                }
                //RefreshChart();
            }
            catch
            {
                //chart1.Series.Clear();
                MsgBox.Show("计算出错：出现异常数据，请查检历史数据和增长率！");
            }
            finally
            {
                treeList2.EndUpdate();
            }
        }

        private void Form9Forecast_Profess_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bl2)
            {
                if (MessageBox.Show("是否保存数据?", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    InitSave();
                bl2 = false;
            }
            else
            {
                SumAll();
                List<PSP_ForecastValues> listValues = new List<PSP_ForecastValues>();
                foreach (TreeListNode nd in treeList2.Nodes)
                {
                    AddNodeDataToList(nd, listValues);
                }
                Common.Services.BaseService.Update<PSP_ForecastValues>(listValues);
            }
        }
        private void SumAll()
        {

            treeList2.BeginUpdate();
            try
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow dr = dataTable2.Rows[i];
                    TreeListNode node = treeList2.FindNodeByKeyID(dr["ID"]);
                    if (!node.HasChildren)
                    {
                        foreach (DataColumn col in dataTable.Columns)
                        {
                            if (col.ColumnName.IndexOf("年") > 0)
                            {
                                int nPreYear = Convert.ToInt32(col.ColumnName.Replace("年", "")) - 1;
                                dr[col.ColumnName] = Math.Round((double)dr[nPreYear + "年"] * (1 + (double)dataTable.Rows[i][col]), 2);
                            }
                        }

                        //父节点的最后一个子节点，调用计算公式
                        if (node.NextNode == null)
                        {
                            CalculateSum(node);
                        }
                    }
                }
                //RefreshChart();
            }
            catch
            {
                //chart1.Series.Clear();
                MsgBox.Show("计算出错：出现异常数据，请查检历史数据和增长率！");
            }
            finally
            {
                treeList2.EndUpdate();
            }

            RefreshChart1();
        }

    }
}