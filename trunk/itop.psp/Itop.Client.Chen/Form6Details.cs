using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.HistoryValue;
using System.Collections;
using Itop.Common;
using Itop.Client.Base;
namespace Itop.Client.Chen
{
    public partial class Form6Details : FormBase
    {
        PSP_WGBCReports _wgbcReport = null;
        DataTable dataTable = null;
        private bool _canAdd = true;

        bool _isSelect = false;

        public bool IsSelect
        {
            get { return _isSelect; }
            set { _isSelect = value; }
        }

        public bool CanAdd
        {
            get { return _canAdd; }
            set { _canAdd = value; }
        }
        private bool _canEdit = true;

        public bool CanEdit
        {
            get { return _canEdit; }
            set { _canEdit = value; }
        }
        private bool _canDelete = true;

        public bool CanDelete
        {
            get { return _canDelete; }
            set { _canDelete = value; }
        }
        private bool _canPrint = true;

        public bool CanPrint
        {
            get { return _canPrint; }
            set { _canPrint = value; }
        }
        public Form6Details(PSP_WGBCReports wgbcReport)
        {
            InitializeComponent();
            _wgbcReport = wgbcReport;
        }
        

        private void HideToolBarButton()
        {
            if (!CanAdd)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!CanPrint)
            {
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!CanDelete)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!IsSelect)
            {
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void Form6Details_Load(object sender, EventArgs e)
        {
            HideToolBarButton();
            Text = _wgbcReport.Title;
            LoadData();
        }

        private void LoadData()
        {
            gridView1.BeginUpdate();

            dataTable = new DataTable();
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("ReportID", typeof(int));
            dataTable.Columns.Add("TypeID", typeof(int));
            dataTable.Columns.Add("ParentTypeID", typeof(int));

            for (int i = 0; i < 7; i++)
            {
                dataTable.Columns.Add("Col" + (i + 1), typeof(string));
            }

            PSP_WGBCValues v = new PSP_WGBCValues();
            v.ReportID = _wgbcReport.ID;

            IList listWGBCValues = Common.Services.BaseService.GetList("SelectPSP_WGBCValuesByReportID", v);
            if(listWGBCValues.Count == 0)
            {
                InsertFixedRows();
            }

            listWGBCValues = Common.Services.BaseService.GetList("SelectPSP_WGBCValuesByReportID", v);

            foreach (PSP_WGBCValues item in listWGBCValues)
            {
                DataRow newRow = dataTable.NewRow();
                Itop.Common.DataConverter.ObjectToRow(item, newRow);

                if(item.ParentTypeID == 0 && item.TypeID > 2)
                {
                    dataTable.Rows.Add(new object[] { });
                }
                dataTable.Rows.Add(newRow);
            }
            gridControl1.DataSource = dataTable;

            gridView1.Columns["ID"].Visible = false;
            gridView1.Columns["ReportID"].Visible = false;
            gridView1.Columns["TypeID"].Visible = false;
            gridView1.Columns["ParentTypeID"].Visible = false;

            gridView1.Columns["Col1"].Caption = "";
            gridView1.Columns["Col1"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns["Col2"].Caption = "安装地点";
            gridView1.Columns["Col3"].Caption = "可投切并联电容器";
            gridView1.Columns["Col4"].Caption = "高压电抗器";
            gridView1.Columns["Col5"].Caption = "低压电抗器";
            gridView1.Columns["Col6"].Caption = "静止补偿器";
            gridView1.Columns["Col7"].Caption = "备注";

            for (int i = 0; i < gridView1.Columns.Count; i++ )
            {
                gridView1.Columns[i].ColumnEdit = repositoryItemTextEdit1;
                gridView1.Columns[i].Width = 110;
            }
            gridView1.Columns["Col3"].Width = 140;

            gridView1.EndUpdate();
        }

        private void InsertFixedRows()
        {
            InsertFixedRows(1, "全地区");
            InsertFixedRows(2, "500kV部分");
            InsertFixedRows(3, "220kV部分");
            //InsertFixedRows(4, "66kV部分");
            InsertFixedRows(4, "110V部分");
        }

        private void InsertFixedRows(int typeID, string col1)
        {
            PSP_WGBCValues v = new PSP_WGBCValues();
            v.Col1 = col1;
            v.ReportID = _wgbcReport.ID;
            v.TypeID = typeID;

            try
            {
                Common.Services.BaseService.Create("InsertPSP_WGBCFixedValues", v);
            }
            catch
            {

            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (!CanEdit)
            {
                e.Cancel = true;
            }

            if (gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ID") == DBNull.Value)
            {
                e.Cancel = true;
            }
            else if ((int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ParentTypeID") == 0)
            {
                if (gridView1.FocusedColumn.FieldName == "Col1")
                {
                    e.Cancel = true;
                }

                if (gridView1.FocusedColumn.FieldName == "Col2" && gridView1.FocusedRowHandle == 0)
                {
                    e.Cancel = true;
                }
            }
        }

        //添加
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int nFRow = gridView1.FocusedRowHandle;
            if (nFRow < 1)
            {
                return;
            }

            if (!CanAdd)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            if(gridView1.GetRowCellValue(nFRow, "ID") == DBNull.Value)
            {
                nFRow--;
            }

            int nParentPos = GetParentPos(nFRow);
            PSP_WGBCValues v = new PSP_WGBCValues();
            v.ReportID = _wgbcReport.ID;
            v.TypeID = (int)gridView1.GetRowCellValue(nFRow, "TypeID");
            v.ParentTypeID = (int)gridView1.GetRowCellValue(nParentPos, "TypeID");
            v.Col1 = GetInsertNumber(nParentPos).ToString();

            try
            {
                v.ID = (int)Common.Services.BaseService.Create<PSP_WGBCValues>(v);
                DataRow newRow = dataTable.NewRow();
                DataConverter.ObjectToRow(v, newRow);

                int nInsertPos = GetInsertPos(nFRow);
                dataTable.Rows.InsertAt(newRow, nInsertPos);
                gridView1.FocusedRowHandle = nInsertPos;
            }
            catch
            {

            }
        }

        private int GetParentPos(int nRow)
        {
            for (int i = nRow; i > 0; i--)
            {
                if (gridView1.GetRowCellValue(i, "ID") != DBNull.Value 
                    && (int)gridView1.GetRowCellValue(i, "ParentTypeID") == 0)
                {
                    return i;
                }
            }

            return 0;
        }

        private int GetInsertNumber(int nParentPos)
        {
            int n = 0;
            bool bMeetNull = false;
            for (int i = nParentPos; i < gridView1.RowCount; i++)
            {
                n++;
                if (gridView1.GetRowCellValue(i, "ID") == DBNull.Value)
                {
                    bMeetNull = true;
                    break;
                }
            }
            if(bMeetNull)
            {
                n--;
            }
            return n;
        }

        private int GetInsertPos(int nRow)
        {
            int nRt = 0;
            for (int i = nRow; i<gridView1.RowCount ; i++)
            {
                nRt = i;
                if (gridView1.GetRowCellValue(i, "ID") == DBNull.Value)
                {
                    if (i > nRow)
                    {
                        nRt = i - 1;
                    }
                    break;
                }
            }

            return nRt + 1;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ID") != DBNull.Value)
            {
                SaveRowValue(dataTable.Rows[e.RowHandle]);
            }
        }

        private void SaveRowValue(DataRow dr)
        {
            PSP_WGBCValues v = DataConverter.RowToObject<PSP_WGBCValues>(dr);
            Common.Services.BaseService.Update<PSP_WGBCValues>(v);
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int nRow = gridView1.FocusedRowHandle;
            if(nRow < 2)
            {
                MsgBox.Show("固定内容不能删除！");
                return;
            }

            if(gridView1.GetRowCellValue(nRow, "ID") == DBNull.Value)
            {
                return;
            }

            if((int)gridView1.GetRowCellValue(nRow, "ParentTypeID") == 0)
            {
                MsgBox.Show("固定内容不能删除！");
                return;
            }

            if (!CanDelete)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            if(MsgBox.ShowYesNo("是否删除 " + gridView1.GetRowCellValue(nRow, "Col1").ToString() + gridView1.GetRowCellValue(nRow, "Col2").ToString() + " ？") == DialogResult.No)
            {
                return;
            }

            PSP_WGBCValues v = DataConverter.RowToObject<PSP_WGBCValues>(dataTable.Rows[nRow]);
            try
            {
                Common.Services.BaseService.Delete<PSP_WGBCValues>(v);
                dataTable.Rows.RemoveAt(nRow);

                //改变后面的序号
                for (int i = nRow; i < gridView1.RowCount; i++)
                {
                    if(gridView1.GetRowCellValue(i, "ID") == DBNull.Value)
                    {
                        break;
                    }

                    if(Convert.ToInt32(gridView1.GetRowCellValue(i, "Col1")) == Convert.ToInt32(v.Col1) + i - nRow + 1)
                    {
                        gridView1.SetRowCellValue(i, "Col1", Convert.ToInt32(v.Col1) + i - nRow);
                    }
                }
            }
            catch
            {

            }
        }

        //打印
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!CanPrint)
            {
                MsgBox.Show("您没有打印权限！");
                return;
            }

            Common.ComponentPrint.ShowPreview(gridControl1, this.Text, true, new Font("宋体", 16, FontStyle.Bold));
        }

        //导出
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (IsSelect)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FileClass.ExportExcel(this.gridControl1);
        }

        private InputLanguage oldInput = null;
        private void gridView1_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            DevExpress.XtraEditors.Repository.RepositoryItemTextEdit edit = e.FocusedColumn.ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit; 
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