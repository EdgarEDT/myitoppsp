using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
using Itop.Client.Forecast.FormAlgorithm_New;
using Itop.Domain.Forecast;

namespace Itop.Client.Forecast
{
    public partial class FormForecastSelectDSH : FormBase
    {
        public FormForecastSelectDSH()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        public bool CnaEdit = false;
        public Ps_forecast_list forecastReport;
        //空间负荷预测
        private void simpleButton1_Click(object sender, EventArgs e)
        {

            FormSpatialforcast frm = new FormSpatialforcast(forecastReport);
            frm.CanEdit = EditRight;
            //frm.project = Itop.Client.MIS.ProgUID;
            frm.smdgroup = smdgroup;
            frm.Text = this.Text + "- " + forecastReport.Title;
            DialogResult dr = frm.ShowDialog();
        }
        //传统负荷预测
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FormForecastModelFSH frm = new FormForecastModelFSH();
            frm.Text = this.Text + "-传统负荷预测";
            frm.report = forecastReport;
            if (frm.ShowDialog()==DialogResult.OK)
            {
                ShowModel(frm.selectID, forecastReport);

            }
        }
        private void ShowModel(int id, Ps_forecast_list report)
        {
            switch (id)
            {
                case 1:
                    FormAverageGrowthRateSH FMA1 = new FormAverageGrowthRateSH(report);
                    FMA1.CanEdit = CnaEdit;
                    FMA1.Text = this.Text + "- 年增长率法";
                    FMA1.ShowDialog();
                    break;
                case 2:
                    FormExtrapolationMethodSH FMA2 = new FormExtrapolationMethodSH(report);
                    FMA2.Text = this.Text + "- 外推法";
                    FMA2.CanEdit = CnaEdit;
                    FMA2.ShowDialog();
                    break;

                case 3:
                    FormCoefficientOfElasticitySH FMA3 = new FormCoefficientOfElasticitySH(report);
                    FMA3.Text = this.Text + "- 弹性系数法";
                    FMA3.CanEdit = CnaEdit;
                    FMA3.ShowDialog();
                    break;
                case 4:
                    FormExponentialSmoothingSH FMA4 = new FormExponentialSmoothingSH(report);
                    FMA4.Text = this.Text + "- 指数平滑法";
                    FMA4.CanEdit = CnaEdit;
                    FMA4.ShowDialog();
                    break;
                case 5:
                    GrayModelSH FMA5 = new GrayModelSH(report);
                    FMA5.Text = this.Text + "- 灰色理论法";
                    FMA5.CanEdit = CnaEdit;
                    FMA5.ShowDialog();
                    break;
                case 6:
                    FormForecast11_FSH FMA6 = new FormForecast11_FSH(report);
                    FMA6.Text = this.Text + "- 大用户";
                    FMA6.CanEdit = CnaEdit;
                    FMA6.ShowDialog();
                    break;
                case 7:
                    FormMaxHourSH FMA7 = new FormMaxHourSH(report);
                    FMA7.Text = this.Text + "- 负荷最大利用小时数法";
                    FMA7.CanEdit = CnaEdit;
                    FMA7.ShowDialog();
                    break;
                case 8:
                    FormForecast9SH FMA8 = new FormForecast9SH(report);
                    FMA8.Text = this.Text + "- 复合算法";
                    FMA8.CanEdit = CnaEdit;
                    FMA8.ShowDialog();
                    break;
                case 10:
                    FormExpertSH FMA10 = new FormExpertSH(report);
                    FMA10.Text = this.Text + "- 专家决策法";
                    FMA10.CanEdit = CnaEdit;
                    FMA10.ShowDialog();
                    break;
            }


        }
        //预测结果列表和综合
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            FormForecastFResult frm = new FormForecastFResult(forecastReport);
            frm.Text = this.Text + "- 预测结果列表和综合";
            frm.CanEdit = CnaEdit;
            frm.ShowDialog();
        }
    }
}
