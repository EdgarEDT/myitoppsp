using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
using Itop.Domain.Forecast;
using Itop.Common;

namespace Itop.Client.Forecast
{
    public partial class FormForecastModelFSH : FormBase
    {
        public FormForecastModelFSH()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        public Ps_forecast_list report;
        DataTable dt = new DataTable();

        public int selectID=0;

        private void FormForecastModelDSH_Load(object sender, EventArgs e)
        {
            AddData();
            loeModel.Properties.DataSource = dt;
            loeModel.Properties.DisplayMember = "title";
            loeModel.Properties.ValueMember = "ID";
            loeModel.Properties.NullText = "请选择预测方法";
        }
        private void AddData()
        {
            dt.Columns.Clear();

            dt.Columns.Add("title", typeof(string));
            dt.Columns.Add("ID", typeof(int));

            DataRow row1 = dt.NewRow();
            row1["title"] = "年增长率法";
            row1["ID"] = 1;
            dt.Rows.Add(row1);


            DataRow row2 = dt.NewRow();
            row2["title"] = "外推法";
            row2["ID"] = 2;
            dt.Rows.Add(row2);


            //DataRow row3 = dt.NewRow();
            //row3["title"] = "弹性系数法";
            //row3["ID"] = 3;
            //dt.Rows.Add(row3);


            DataRow row4 = dt.NewRow();
            row4["title"] = "指数平滑法";
            row4["ID"] = 4;
            dt.Rows.Add(row4);


            DataRow row5 = dt.NewRow();
            row5["title"] = "灰色理论法";
            row5["ID"] = 5;
            dt.Rows.Add(row5);


            DataRow row6 = dt.NewRow();
            row6["title"] = "大用户";
            row6["ID"] = 6;
            dt.Rows.Add(row6);


            DataRow row10 = dt.NewRow();
            row10["title"] = "专家决策法";
            row10["ID"] = 10;
            dt.Rows.Add(row10);


            DataRow row7 = dt.NewRow();
            row7["title"] = "负荷最大利用小时数法";
            row7["ID"] = 7;
            dt.Rows.Add(row7);

            DataRow row8 = dt.NewRow();
            row8["title"] = "复合算法";
            row8["ID"] = 8;
            dt.Rows.Add(row8);


            //DataRow row9 = dt.NewRow();
            //row9["title"] = "预测结果列表和综合";
            //row9["ID"] = 9;
            //dt.Rows.Add(row9);

          

        }

        private void sbtnOK_Click(object sender, EventArgs e)
        {
            if (selectID==0)
            {
                MsgBox.Show("请选择预测方法！");
                return;
            }
            else
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void loeModel_EditValueChanged(object sender, EventArgs e)
        {
            selectID = (int)loeModel.EditValue;
        }
       
    }
}
