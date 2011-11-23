using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.HistoryValue;
using System.Collections;
using Itop.Client.Common;
using Itop.Client.Base;
namespace Itop.Client.Chen
{
    public partial class FrmBalanceOfPowerRegionDialog0 : FormBase
    {
       IList<PSP_P_Values> li = new List<PSP_P_Values>();
        public IList<PSP_P_Values> LI
        {
           get
            {
                return li;
            }
        }

        DataTable dataTable = new DataTable();
        public FrmBalanceOfPowerRegionDialog0()
        {
            InitializeComponent();
        }

        private void FrmBalanceOfPowerRegionDialog0_Load(object sender, EventArgs e)
        {
            PSP_ForecastReports report = new PSP_ForecastReports();
            report.Flag = 10;
            IList listReports = Common.Services.BaseService.GetList("SelectPSP_ForecastReportsByFlag", report);

            dataTable = Itop.Common.DataConverter.ToDataTable(listReports, typeof(PSP_ForecastReports));
            gridView1.BeginUpdate();
            gridControl1.DataSource = dataTable;
            gridView1.GroupPanelText = "供电最大负荷";
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
            DataRow obj = this.gridView1.GetDataRow(this.gridView1.FocusedRowHandle);
            if (obj == null)
                return;
            
           

            PSP_P_Values ppv = new PSP_P_Values();
            ppv.Flag2 = Convert.ToInt32(obj["ID"]);
            ppv.TypeID =80003;
            li = Services.BaseService.GetList<PSP_P_Values>("SelectPSP_P_ValuesByTypeIDFlag2", ppv);
            this.DialogResult = DialogResult.OK;
            
          
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}