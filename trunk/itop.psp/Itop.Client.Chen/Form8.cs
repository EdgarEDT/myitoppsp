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
    public partial class Form8 : Itop.Client.Base.FormBase
    {
        private int typeFlag = 1;
        DataTable dataTable;
        public Form8()
        {
            InitializeComponent();
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


        private void HideToolBarButton()
        {
            if (!base.AddRight)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!base.EditRight)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!base.DeleteRight)
            {
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            HideToolBarButton();

            PSP_ForecastReports report = new PSP_ForecastReports();
            report.Flag = typeFlag;
            IList listReports = Common.Services.BaseService.GetList("SelectPSP_ForecastReportsByFlag", report);

            dataTable = Itop.Common.DataConverter.ToDataTable(listReports, typeof(PSP_ForecastReports));
            gridView1.BeginUpdate();
            gridControl1.DataSource = dataTable;

            gridView1.Columns["ID"].Visible = false;
            gridView1.Columns["ID"].OptionsColumn.ShowInCustomizationForm = false;
            gridView1.Columns["Flag"].Visible = false;
            gridView1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
            gridView1.Columns["Title"].Caption = "预测名称";
            gridView1.Columns["Title"].Width = 300;
            gridView1.Columns["StartYear"].Caption = "起始年份";
            //gridView1.Columns["StartYear"].Width = 150;
            gridView1.Columns["EndYear"].Caption = "结束年份";
            //gridView1.Columns["EndYear"].Width = 150;
            gridView1.Columns["HistoryYears"].Caption = "历史年数";
            //gridView1.Columns["HistoryYears"].Width = 150;
            gridView1.EndUpdate();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!base.AddRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            FormForecastReport frm = new FormForecastReport();
            frm.IsEdit = false;
            frm.TypeFlag = typeFlag;
            frm.Text = "添加新预测";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                DataRow newRow = dataTable.NewRow();
                Itop.Common.DataConverter.ObjectToRow(frm.Psp_ForecastReport, newRow);
                dataTable.Rows.Add(newRow);

                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditReport();
        }

        private void EditReport()
        {
            if (gridView1.FocusedRowHandle < 0)
            {
                return;
            }

            if (!base.EditRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            PSP_ForecastReports report = Itop.Common.DataConverter.RowToObject<PSP_ForecastReports>(gridView1.GetDataRow(gridView1.FocusedRowHandle));
            FormForecastReport frm = new FormForecastReport();
            frm.IsEdit = true;
            frm.Psp_ForecastReport = report;
            frm.Text = "修改预测";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Itop.Common.DataConverter.ObjectToRow(frm.Psp_ForecastReport, gridView1.GetDataRow(gridView1.FocusedRowHandle));
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0)
            {
                return;
            }

            if (!base.DeleteRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            if (MsgBox.ShowYesNo("是否删除 " + gridView1.GetDataRow(gridView1.FocusedRowHandle)["Title"])
                == DialogResult.No)
            {
                return;
            }

            PSP_ForecastReports report = Itop.Common.DataConverter.RowToObject<PSP_ForecastReports>(gridView1.GetDataRow(gridView1.FocusedRowHandle));

            try
            {
                Common.Services.BaseService.Delete<PSP_ForecastReports>(report);
                gridView1.DeleteRow(gridView1.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                MsgBox.Show("删除出错：" + ex.Message);
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowDetails();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void ShowDetails()
        {
            if (gridView1.FocusedRowHandle < 0)
            {
                return;
            }

            PSP_ForecastReports report = Itop.Common.DataConverter.RowToObject<PSP_ForecastReports>(gridView1.GetDataRow(gridView1.FocusedRowHandle));

            Form8Forecast frm = new Form8Forecast(report, typeFlag);
            frm.CanPrint = base.PrintRight;
            frm.CanEdit = base.EditRight;
            frm.IsSelect = IsSelect;
            frm.ADdRight = AddRight;
            frm.DEleteRight = DeleteRight;
            DialogResult dr = frm.ShowDialog();

            if (IsSelect && dr == DialogResult.OK)
            {
                Title = report.Title;
                Unit = "单位：万千瓦时";
                Gcontrol = frm.GridControl;
                DialogResult = DialogResult.OK;
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            ShowDetails();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private InputLanguage oldInput = null;
        private void gridView1_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            try
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
            catch { }
        }


    }
}