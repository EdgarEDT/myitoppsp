using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Common;
using Itop.Client.Base;
using System.Collections;
using System.Reflection;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Itop.Domain.Forecast;
using System.Xml;
namespace Itop.Client.GYGH
{
    public partial class FrmSelforcast : FormBase
    {
        public FrmSelforcast()
        {
            InitializeComponent();
        }
        
        public Ps_forecast_list report
        {
            get
            {
               // return this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Ps_forecast_list;
                return Itop.Common.DataConverter.RowToObject<Ps_forecast_list>(gridView1.GetDataRow(gridView1.FocusedRowHandle));
            }
           
        }
        private string ProjectUID = Itop.Client.MIS.ProgUID;
        private void FrmSelforcast_Load(object sender, EventArgs e)
        {
            Ps_forecast_list report = new Ps_forecast_list();
            report.UserID = ProjectUID;  //SetCfgValue("lastLoginUserNumber", Application.ExecutablePath + ".config");
            report.Col1 = "1";
            IList listReports = Common.Services.BaseService.GetList("SelectPs_forecast_listByCOL1AndUserID", report);

           System.Data.DataTable dataTable = Itop.Common.DataConverter.ToDataTable(listReports, typeof(Ps_forecast_list));
            gridView1.BeginUpdate();
            gridControl1.DataSource = dataTable;

            gridView1.Columns["ID"].Visible = false;
            gridView1.Columns["ID"].OptionsColumn.ShowInCustomizationForm = false;
            gridView1.Columns["UserID"].Visible = false;
            gridView1.Columns["UserID"].OptionsColumn.ShowInCustomizationForm = false;
            gridView1.Columns["Col1"].Visible = false;
            gridView1.Columns["Col1"].OptionsColumn.ShowInCustomizationForm = false;
            gridView1.Columns["Col2"].Visible = false;
            gridView1.Columns["Col2"].OptionsColumn.ShowInCustomizationForm = false;

            gridView1.Columns["Title"].Caption = "预测名称";
            gridView1.Columns["Title"].Width = 300;
            gridView1.Columns["StartYear"].Caption = "起始年份";
            gridView1.Columns["EndYear"].Caption = "结束年份";
            gridView1.EndUpdate();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}