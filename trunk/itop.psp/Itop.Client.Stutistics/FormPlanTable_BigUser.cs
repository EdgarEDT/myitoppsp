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
namespace Itop.Client.Stutistics
{
    public partial class FormPlanTable_BigUser : Itop.Client.Base.FormBase
    {
        private string typeFlag = "PlanTable_BigUser";
        DataTable dataTable;
        public FormPlanTable_BigUser()
        {
            InitializeComponent();
        }
        private int ItemID = 0;

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

        private void FormPlanTable_BigUser_Load(object sender, EventArgs e)
        {　
            //this.Text="淮北供电公司2008--2010年110千伏及以上电网规划项目前期计划工作表";
            HideToolBarButton();

            PowerEachList report = new PowerEachList();
            report.Types = typeFlag;
            IList listReports = Common.Services.BaseService.GetList("SelectPowerEachListList", report);

            dataTable = Itop.Common.DataConverter.ToDataTable(listReports, typeof(PowerEachList));
            gridView1.BeginUpdate();
            gridControl1.DataSource = dataTable;

            gridView1.Columns["UID"].Visible = false;
            gridView1.Columns["UID"].OptionsColumn.ShowInCustomizationForm = false;
            gridView1.Columns["ParentID"].Visible = false;
            gridView1.Columns["ParentID"].OptionsColumn.ShowInCustomizationForm = false;
            gridView1.Columns["Types"].Visible = false;
            gridView1.Columns["Types"].OptionsColumn.ShowInCustomizationForm = false;
            gridView1.Columns["ListName"].Caption = "项目计划表名称";
            gridView1.Columns["ListName"].Width = 300;

            gridView1.Columns["CreateDate"].Caption = "创建日期";
            gridView1.Columns["CreateDate"].Width = 80;
            gridView1.Columns["CreateDate"].VisibleIndex = 20;
            gridView1.Columns["Remark"].Caption = "备注";
            gridView1.Columns["Remark"].VisibleIndex = 21;
           // gridView1.Columns["Remark"].Width = 150;
        
            gridView1.EndUpdate();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!base.AddRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            FormForecastReport_BigUser frm = new FormForecastReport_BigUser();
            frm.IsEdit = false;
            frm.TypeFlag = typeFlag;
            frm.Text = "添加项目计划表名称";
          //  frm.TypeText = "负荷";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                DataRow newRow = dataTable.NewRow();
                Itop.Common.DataConverter.ObjectToRow(frm.Psp_ForecastReport, newRow);
                dataTable.Rows.Add(newRow);
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                PSP_BigUser_Types psp_Type = new PSP_BigUser_Types();
                psp_Type.S2 = "S2 LIKE '%" + (Convert.ToInt32(frm.Psp_ForecastReport.ListName) - 1)+ "%' and ItemID="+ItemID;
                IList listTypes = Common.Services.BaseService.GetList("SelectPSP_BigUser_TypesByItemID", psp_Type);
                foreach (PSP_BigUser_Types psp_Typetemp in listTypes)
                {
                    psp_Type = psp_Typetemp;
                    if (psp_Type.S2 != "" && !psp_Type.S2.Contains(frm.Psp_ForecastReport.ListName))
                    {
                        psp_Type.S2 += "," + frm.Psp_ForecastReport.ListName;
                    }
                    Common.Services.BaseService.Update<PSP_BigUser_Types>(psp_Type);
                }
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

            PowerEachList report = Itop.Common.DataConverter.RowToObject<PowerEachList>(gridView1.GetDataRow(gridView1.FocusedRowHandle));
            FormForecastReport_BigUser frm = new FormForecastReport_BigUser();
            frm.IsEdit = true;
            frm.Psp_ForecastReport = report;
            frm.Text = "修改项目计划表";
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

            if (MsgBox.ShowYesNo("是否删除 " + gridView1.GetDataRow(gridView1.FocusedRowHandle)["ListName"])
                == DialogResult.No)
            {
                return;
            }

            PowerEachList report = Itop.Common.DataConverter.RowToObject<PowerEachList>(gridView1.GetDataRow(gridView1.FocusedRowHandle));

            try
            {
                Common.Services.BaseService.Update("DeletePowerEachList_BigUser", report);
                PSP_BigUser_Types psp_Type = new PSP_BigUser_Types();
                psp_Type.S2 = "S2 LIKE '%" + (Convert.ToInt32(report.ListName)) + "%' and ItemID=" + ItemID;
                IList listTypes = Common.Services.BaseService.GetList("SelectPSP_BigUser_TypesByItemID", psp_Type);
                foreach (PSP_BigUser_Types psp_Typetemp in listTypes)
                {
                    psp_Type = psp_Typetemp;
                    string[] yearitem = psp_Typetemp.S2.Split(',');
                    if (yearitem.Length == 1)
                        //DeletePSP_ValuesByType里面删除数据和分类
                        Common.Services.BaseService.Delete<PSP_BigUser_Types>(psp_Typetemp);
                    else
                    {
                        psp_Type.S2 = "";
                        foreach (string strtemp in yearitem)
                        {
                            if (strtemp != report.ListName)
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
                       
                    }
                }
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

        private void ShowDetails()
        {
            if (gridView1.FocusedRowHandle < 0)
            {
                return;
            }

            PowerEachList report = Itop.Common.DataConverter.RowToObject<PowerEachList>(gridView1.GetDataRow(gridView1.FocusedRowHandle));

            Form7 frm = new Form7(report);
            //frm.CanEdit = base.EditRight;
            //frm.CanPrint = base.PrintRight;
            //frm.IsSelect = IsSelect;
            frm.Text = report.ListName + "年大用户数据统计分析";
            frm.ADDRight= base.AddRight;
            frm.EDItRight = base.EditRight;
            frm.DELeteRight = base.DeleteRight;
            frm.PRIntRight = base.PrintRight;
            DialogResult dr = frm.ShowDialog();

          //  if (IsSelect && dr == DialogResult.OK)
            if (dr == DialogResult.OK)
            {
                //Title = report.Title;
               // Unit = "单位：万千瓦时";
                Gcontrol = frm.Gcontrol;
                DialogResult = DialogResult.OK;
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            ShowDetails();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

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