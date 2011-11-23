using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Domain.Chen;
using Itop.Client.Common;
using DevExpress.XtraTreeList;
using Itop.Domain.HistoryValue;
using DevExpress.XtraTreeList.Nodes;
using Itop.Client.Base;
namespace Itop.Client.Chen
{
    public partial class FormCalculatorN1 : FormBase
    {
        public FormCalculatorN1()
        {
            InitializeComponent();
        }
        string type = "";
        PSP_Calc pc = new PSP_Calc();
        PSP_Calc pc1 = new PSP_Calc();
        

        DataTable dt = null;
        DataTable dt1 = null;
        DataRow newrow2 = null;

        DataRow newrow3 = null;
        DataRow newrow4 = null;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        PSP_ForecastReports forecastReport;
        public PSP_ForecastReports PForecastReports
        {
            get { return forecastReport; }
            set { forecastReport = value; }       
        }

        DataTable dataTable;
        public DataTable DTable
        {
            get { return dataTable; }
            set { dataTable = value; }
        }


        TreeList treeList1;
        public TreeList TList
        {
            get { return treeList1; }
            set { treeList1 = value; }
        }


        private void FormCalculator_Load(object sender, EventArgs e)
        {

            string str = " Flag='" + type + "' and Type='专家指定'";
            IList<PSP_Calc_Spring> list2 = Services.BaseService.GetList<PSP_Calc_Spring>("SelectPSP_Calc_SpringByWhere", str);
            int years = forecastReport.EndYear - forecastReport.StartYear;
            for (int i = 1; i <= years; i++)
            {
                bool bl = false;
                foreach (PSP_Calc_Spring pcs in list2)
                {
                    if ((forecastReport.StartYear + i).ToString() == pcs.Name)
                    {
                        bl = true;
                    }
                }
                if (!bl)
                {
                    PSP_Calc_Spring pcss = new PSP_Calc_Spring();
                    pcss.ID = Guid.NewGuid().ToString();
                    pcss.Name = (forecastReport.StartYear + i).ToString();
                    pcss.Value1 = 0;
                    pcss.Value2 = 0;
                    pcss.Flag = type;
                    pcss.Type = "专家指定";
                    Services.BaseService.Create<PSP_Calc_Spring>(pcss);
                    list2.Add(pcss);
                }
            }

            

            gridControl1.DataSource = list2;

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }







        //根据节点返回此行的历史数据
        private double[] GenerateHistoryValue(TreeListNode node)
        {
            double[] rt = new double[forecastReport.HistoryYears];
            for (int i = 0; i < forecastReport.HistoryYears; i++)
            {
                object obj = node.GetValue(forecastReport.StartYear - forecastReport.HistoryYears + i + 1 + "Y");
                if (obj == null || obj == DBNull.Value)
                {
                    rt[i] = 0;
                }
                else
                {
                    rt[i] = (double)obj;
                }
            }
            return rt;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            object obj = this.gridView1.GetRow(this.gridView1.FocusedRowHandle);
            if (obj == null)
                return;

            PSP_Calc_Spring pcs = obj as PSP_Calc_Spring;
            Services.BaseService.Update<PSP_Calc_Spring>(pcs);


        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            object obj = this.gridView1.GetRow(this.gridView1.FocusedRowHandle);
            if (obj == null)
                return;

            PSP_Calc_Spring pcs = (PSP_Calc_Spring)obj;

            for (int i = 0; i < forecastReport.HistoryYears; i++)
            {
                int year=forecastReport.StartYear - forecastReport.HistoryYears + i + 1;
                if (pcs.Name == year.ToString())
                    e.Cancel = true;
            }
            
        }


    }
}