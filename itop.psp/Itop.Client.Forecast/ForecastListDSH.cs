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
using Itop.Client.Forecast.FormAlgorithm_New;

namespace Itop.Client.Forecast
{
    public partial class ForecastListDSH : Itop.Client.Base.FormBase
    {
        //设计为电量预测模块
        private int typeFlag =2;
        DataTable dataTable;
        public ForecastListDSH()
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
               // string aa=this.
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            HideToolBarButton();

            Ps_forecast_list report = new Ps_forecast_list();
            report.UserID = ProjectUID;  //SetCfgValue("lastLoginUserNumber", Application.ExecutablePath + ".config");
            report.Col1 = "2";
            IList listReports = Common.Services.BaseService.GetList("SelectPs_forecast_listByCOL1AndUserID", report);

            dataTable = Itop.Common.DataConverter.ToDataTable(listReports, typeof(Ps_forecast_list));
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
            gridView1.Columns["StartYear"].Caption = "历史起始年份";
            gridView1.Columns["EndYear"].Caption = "历史结束年份";

            gridView1.Columns["YcStartYear"].Caption = "预测起始年份";
            gridView1.Columns["YcEndYear"].Caption = "预测结束年份";


            gridView1.EndUpdate();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!base.AddRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            FormForecastEditCSH frm = new FormForecastEditCSH();
            frm.TypeFlag = 2;
            frm.IsEdit = false;
            frm.ProjectUID = ProjectUID;
     //       frm.TypeFlag = typeFlag;
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

            Ps_forecast_list report = Itop.Common.DataConverter.RowToObject<Ps_forecast_list>(gridView1.GetDataRow(gridView1.FocusedRowHandle));
            FormForecastEditCSH frm = new FormForecastEditCSH();
            frm.IsEdit = true;
            frm.Psp_ForecastReport = report;
            frm.ProjectUID = ProjectUID;
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

            Ps_forecast_list report = Itop.Common.DataConverter.RowToObject<Ps_forecast_list>(gridView1.GetDataRow(gridView1.FocusedRowHandle));

            try
            {
                Common.Services.BaseService.Delete<Ps_forecast_list>(report);
                gridView1.DeleteRow(gridView1.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                MsgBox.Show("删除出错：" + ex.Message);
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //预测结果
            FormForecastDResultAll frm = new FormForecastDResultAll(this.Text + "-预测结果");
            frm.ShowDialog();
        }

       
        private void ShowDetails()
        {
            if (!base.EditRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }
            if (gridView1.FocusedRowHandle < 0)
            {
                return;
            }

            Ps_forecast_list report = Itop.Common.DataConverter.RowToObject<Ps_forecast_list>(gridView1.GetDataRow(gridView1.FocusedRowHandle));

            FormForecastModelDSH frm = new FormForecastModelDSH();
            frm.Text = this.Text + "- " + report.Title;
            if (frm.ShowDialog()==DialogResult.OK)
            {

                ShowModel(frm.selectID, report);

            }


        }
        private void ShowModel(int id,Ps_forecast_list report)
        {
            switch (id)
            {
                case 1:
                    FormAverageGrowthRateSH FMA1 = new FormAverageGrowthRateSH(report);
                    FMA1.CanEdit = base.EditRight;
                    FMA1.Text = this.Text + "- 年增长率法";
                    FMA1.ShowDialog();
                    break;
                case 2:
                    FormExtrapolationMethodSH FMA2 = new FormExtrapolationMethodSH(report);
                    FMA2.Text = this.Text + "- 外推法";
                    FMA2.CanEdit = base.EditRight; 
                    FMA2.ShowDialog();
                    break;

                case 3:
                    FormCoefficientOfElasticitySH FMA3 = new FormCoefficientOfElasticitySH(report);
                    FMA3.Text = this.Text + "- 弹性系数法";
                    FMA3.CanEdit = base.EditRight; 
                    FMA3.ShowDialog();
                    break;
                case 4:
                    FormExponentialSmoothingSH FMA4 = new FormExponentialSmoothingSH(report);
                    FMA4.Text = this.Text + "- 指数平滑法";
                    FMA4.CanEdit = base.EditRight; 
                    FMA4.ShowDialog();
                    break;
                case 5:
                    GrayModelSH FMA5 = new GrayModelSH(report);
                    FMA5.Text = this.Text + "- 灰色理论法";
                    FMA5.CanEdit = base.EditRight; 
                    FMA5.ShowDialog();
                    break;
                case 6: 
                    FormForecast11_SH FMA6 = new FormForecast11_SH(report);
                    FMA6.Text = this.Text + "- 大用户";
                    FMA6.CanEdit = base.EditRight; 
                    FMA6.ShowDialog();
                    break;
                case 7:
                    FormUnitConsumptionValueSH FMA7 = new FormUnitConsumptionValueSH(report);
                    FMA7.Text = this.Text + "- 产值单耗法";
                    FMA7.CanEdit = base.EditRight;
                    FMA7.ShowDialog();
                    break;
                case 8: 
                    FormForecast9SH FMA8 = new FormForecast9SH(report);
                    FMA8.Text = this.Text + "- 复合算法";
                    FMA8.CanEdit = base.EditRight; 
                    FMA8.ShowDialog();
                    break;
                case 9:
                    FormForecastDResult FMA9 = new FormForecastDResult(report);
                    FMA9.Text = this.Text + "- 预测结果列表和综合";
                    FMA9.CanEdit = base.EditRight;
                    FMA9.ShowDialog();
                    break;
                case 10:
                    FormExpertSH FMA10 = new FormExpertSH(report);
                    FMA10.Text = this.Text + "- 专家决策法";
                    FMA10.CanEdit = base.EditRight;
                    FMA10.ShowDialog();
                    break;
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

        public static string SetCfgValue(string AppKey, string FileName)
        {
            System.Xml.XmlDocument xDoc = new XmlDocument();
            xDoc.Load(FileName);

            XmlNode xNode;
            XmlElement xElemKey;
            XmlElement xElemValue;

            xNode = xDoc.SelectSingleNode("//appSettings");

            xElemKey = (XmlElement)xNode.SelectSingleNode("//add[@key=\"" + AppKey + "\"]");
            if (xElemKey != null)
            {
                string[] str = xElemKey.OuterXml.Split('"');
                if (str != null)
                {
                    if (str.Length > 3)
                    {
                        return str[3];
                    }
                }
            }
            return "";
        }
        //开始预测
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowDetails();
        }


    }
}