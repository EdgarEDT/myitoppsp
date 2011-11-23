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
using Itop.Client.Chen;
using Itop.Domain.Chen;
using Itop.Client.Common;
namespace Itop.Client.Chen
{
    public partial class FormPlanTable_VolumeBalance : Itop.Client.Base.FormBase
    {
        private string typeFlag = "";
        DataTable dataTable;
        public FormPlanTable_VolumeBalance()
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
        public void VolumeBalance_110()
        {
           // this.Text = "淮北供电公司电网滚动规划110千伏变电容量平衡表";
            typeFlag = "PlanTable_VolumeBalance_110";
            this.Show();
        }
        public void VolumeBalance_220()
        {
           // this.Text = "淮北供电公司电网滚动规划220千伏变电容量平衡表";
            typeFlag = "PlanTable_VolumeBalance_220";
            this.Show();
        }
        public void VolumeBalance_35()
        {
            // this.Text = "淮北供电公司电网滚动规划220千伏变电容量平衡表";
            typeFlag = "PlanTable_VolumeBalance_35";
            this.Show();
        }
        private void Form9_Load(object sender, EventArgs e)
        {
           
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
            gridView1.Columns["ListName"].Caption ="分区片名称";
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

            FormForecastReport_VolumeBalance frm = new FormForecastReport_VolumeBalance();
            frm.IsEdit = false;
            frm.TypeFlag = typeFlag;
            frm.Text = "添加分区名称";
          //  frm.TypeText = "负荷";

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

            PowerEachList report = Itop.Common.DataConverter.RowToObject<PowerEachList>(gridView1.GetDataRow(gridView1.FocusedRowHandle));
            FormForecastReport_VolumeBalance frm = new FormForecastReport_VolumeBalance();
            frm.IsEdit = true;
            frm.Psp_ForecastReport = report;
            frm.Text = "修改分区";
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
                PSP_VolumeBalance ob = new PSP_VolumeBalance();
                ob.Flag = report.UID;
                if (typeFlag.Contains("110"))
                    ob.TypeID = "110";
                else if (typeFlag.Contains("220"))
                    ob.TypeID = "220";
                IList<PSP_VolumeBalance> list = Services.BaseService.GetList<PSP_VolumeBalance>("SelectPSP_VolumeBalanceByTypeID", ob);
                foreach(PSP_VolumeBalance listtemp in list)
                    Services.BaseService.Update("DeletePSP_VolumeBalance2", listtemp);

                Common.Services.BaseService.Update("DeletePowerEachList_VolumeBalance", report);
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

            FormPSP_VolumeBalance frm = new FormPSP_VolumeBalance(report);
            if(typeFlag.Contains("110"))
            {
                if (report.ListName.Contains("110千伏变电容量平衡表"))
                    frm.Text = report.ListName;
                else
                    frm.Text = report.ListName+"110千伏变电容量平衡表";
            }
            else
                if (typeFlag.Contains("220"))
                {
                    if (report.ListName.Contains("220千伏变电容量平衡表"))
                        frm.Text = report.ListName;
                    else
                        frm.Text = report.ListName + "220千伏变电容量平衡表";
                
                }
                else
                    if (typeFlag.Contains("35"))
                    {
                        if (report.ListName.Contains("35千伏变电容量平衡表"))
                            frm.Text = report.ListName;
                        else
                            frm.Text = report.ListName + "35千伏变电容量平衡表";

                    }

            frm.ADdRight = this.AddRight;
            frm.EDitRight = this.EditRight;
            frm.DEleteRight = this.DeleteRight;
            frm.PRintRight = this.PrintRight;
            //frm.Hide();
            
            //DialogResult dr = frm.ShowDialog();
            frm.Show();
          //  if (IsSelect && dr == DialogResult.OK)
            //if (dr == DialogResult.OK)
            //{
            //    //Title = report.Title;
            //   // Unit = "单位：万千瓦时";
            //    //Gcontrol = frm.Gcontrol;
            //    DialogResult = DialogResult.OK;
            //}
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