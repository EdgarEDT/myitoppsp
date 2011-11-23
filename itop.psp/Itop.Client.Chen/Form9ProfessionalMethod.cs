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

namespace Itop.Client.Chen
{
    public partial class Form9ProfessionalMethod : DevExpress.XtraEditors.XtraForm
    {
        private DataTable dataTable = null;
        private int typeFlag2 = 0;
        private PSP_ForecastReports forecastReport = null;
        static double defaultPercent = 0.1;
        public Form9ProfessionalMethod(int flag2, PSP_ForecastReports fr)
        {
            typeFlag2 = flag2;
            forecastReport = fr;
            InitializeComponent();
        }

        private void Form9ProfessionalMethod_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            LoadData();
            InitDatas();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
        }

        public DataTable DataTable
        {
            get { return dataTable; }
            set { dataTable = value; }
        }

        private void LoadData()
        {
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
            }

            PSP_Types psp_Type = new PSP_Types();
            //psp_Type.Flag = 4;
            psp_Type.ID = 32;
            //IList listTypes = Common.Services.BaseService.GetList("SelectPSP_TypesByFlag", psp_Type);
            IList listTypes = Common.Services.BaseService.GetList("SelectPSP_TypesByFlag3", psp_Type);
            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_Types));

            psp_Type.Flag = 5;
            listTypes = Common.Services.BaseService.GetList("SelectPSP_TypesByFlag", psp_Type);

            DataTable dt = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_Types));
            foreach (DataRow dr in dt.Rows)
            {
                DataRow newRow = dataTable.NewRow();
                newRow.ItemArray = dr.ItemArray;
                dataTable.Rows.Add(newRow);
            }

            treeList1.DataSource = dataTable;

            treeList1.Columns["Title"].Caption = "分类名";
            treeList1.Columns["Title"].Width = 180;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Flag"].VisibleIndex = -1;
            treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["Flag2"].VisibleIndex = -1;
            treeList1.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;

            for (int i = forecastReport.StartYear + 1; i <= forecastReport.EndYear; i++)
            {
                AddColumn(i);
            }

            foreach (TreeListNode nd in treeList1.Nodes)
            {
                SetDefaultValues(nd);
            }

            treeList1.ExpandAll();
        }


        private void InitDatas()
        {
            foreach (TreeListColumn tlc in treeList1.Columns)
            {
                //if (tlc.FieldName.IndexOf("年") >= 0 || tlc.FieldName.IndexOf("Y") >= 0)
                //if (tlc.FieldName.IndexOf("年") >= 0 && int.Parse(tlc.FieldName.Replace("年", "")) == startyear)
                //{
                //    double s1 = 0;
                //    double s2 = 0;
                //    try
                //    {
                //        s1 = (double)dataTable.Rows[0][tlc.FieldName];
                //    }
                //    catch { }

                //    try
                //    {
                //        s2 = (double)dataTable.Rows[1][tlc.FieldName];
                //    }
                //    catch { }

                //    try
                //    {
                //        if (s1 + s2 != 0)
                //            dataTable.Rows[0][tlc.FieldName]= s1 + s2;
                //    }
                //    catch (Exception ex) { MessageBox.Show(ex.Message); }

                //}

                dataTable.Rows[0]["Title"] = "全社会最高负荷";
                dataTable.Rows[1]["Title"] = "统调最高负荷";
            }

        }

        //
        private void SetDefaultValues(TreeListNode node)
        {
            if (node.HasChildren)
            {
                foreach (TreeListNode nd in node.Nodes)
                {
                    SetDefaultValues(nd);
                }
            }
            else
            {
                for (int i = forecastReport.StartYear + 1; i <= forecastReport.EndYear; i++)
                {
                    node.SetValue(i + "年", defaultPercent);
                }
            }
        }

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
            
            //foreach (TreeListColumn col in treeList1.Columns)
            //{
            //    try
            //    {
            //        if (col.FieldName.IndexOf("年") > 0
            //            && Convert.ToInt32(col.FieldName.Replace("年", "")) > Convert.ToInt32(e.Column.FieldName.Replace("年", "")))
            //            //&& Math.Abs(Convert.ToDouble(e.Value) - defaultPercent) > 0.00001)
            //        {
            //            e.Node.SetValue(col.FieldName, e.Value);
            //        }
            //    }
            //    catch { }
            //}
            DataRow row = null;

            switch (e.Node["Title"].ToString())
            { 
                case "全社会最高负荷":
                    row = dataTable.Rows[0];
                    break;
                case "统调最高负荷":
                    row = dataTable.Rows[1];
                    break;
            }


            foreach (DataColumn dc in dataTable.Columns)
            {
                try
                {
                    if (dc.ColumnName.IndexOf("年") > 0
                        && Convert.ToInt32(dc.ColumnName.Replace("年", "")) > Convert.ToInt32(e.Column.FieldName.Replace("年", "")))
                    //&& Math.Abs(Convert.ToDouble(e.Value) - defaultPercent) > 0.00001)
                    {
                        row[dc.ColumnName] = e.Value;
                    }
                }
                catch { }
            }





        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void repositoryItemTextEdit1_DoubleClick(object sender, EventArgs e)
        {
        }
    }
}