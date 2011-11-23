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
    public partial class FormXiaoshi_Calculator : FormBase
    {
        public FormXiaoshi_Calculator()
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

            #region 年平均s
            dt = new DataTable();
            dt.Columns.Add("Name");
            foreach (DataRow dataRow in dataTable.Rows)
            {
                TreeListNode treeNode = treeList1.FindNodeByKeyID(dataRow["ID"]);
                if (!treeNode.HasChildren)
                {
                    dt.Columns.Add(dataRow["Title"].ToString().Trim(),typeof(double));
                    DevExpress.XtraVerticalGrid.Rows.EditorRow editorRow = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
                    editorRow.Properties.FieldName = dataRow["Title"].ToString().Trim();
                    editorRow.Properties.Caption = dataRow["Title"].ToString().Trim();
                    editorRow.Height = 20;
                    editorRow.Properties.RowEdit = this.repositoryItemCalcEdit4;
                    this.vGridControl2.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] { editorRow });

                }
            }
            DataRow newrow1 = dt.NewRow();
            newrow1["Name"] = "增长率(计算)";
            foreach (DataRow dataRow in dataTable.Rows)
            {
                TreeListNode treeNode = treeList1.FindNodeByKeyID(dataRow["ID"]);
                if (!treeNode.HasChildren)
                {
                    int forecastYears = forecastReport.EndYear - forecastReport.StartYear;
                    double[] historyValues = GenerateHistoryValue(treeNode);
                    newrow1[dataRow["Title"].ToString().Trim()] = Calculator.AverageIncreasing(historyValues);
                }
            }


            //dt.Rows.Add(newrow1); 
            newrow2 = dt.NewRow();
            dt.Rows.Add(newrow2);
            newrow2["Name"] = "当前增长率";

            vGridControl2.DataSource = dt;





            IList<PSP_Calc> list = Services.BaseService.GetList<PSP_Calc>("SelectPSP_CalcByFlag", type);

            if (list.Count == 0)
            {
                pc.ID = Guid.NewGuid().ToString();
                pc.Flag = type;
                pc.Col1 = "";
                pc.Value1 = 3;
                pc.Value2 = 0.4;

                Services.BaseService.Create<PSP_Calc>(pc);
            }
            else
                pc = list[0];

            //textEdit1.Text = pc.Value2.ToString();






            IList<PSP_Calc> list1 = Services.BaseService.GetList<PSP_Calc>("SelectPSP_CalcByFlag1", type);
            if (list1.Count == 0)
            {
                pc1.ID = Guid.NewGuid().ToString();
                pc1.Flag = type;
                pc1.Col1 = "1";

                int i = 1;
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName == "Name")
                        continue;

                    double value = 0;
                    try
                    {
                        value = Convert.ToDouble(newrow1[dc].ToString());
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }

                    pc1.GetType().GetProperty("Value" + i.ToString()).SetValue(pc1, value, null);
                    newrow2[dc] = value;
                    i++;
                }



                Services.BaseService.Create<PSP_Calc>(pc1);
            }
            else
            {
                pc1 = list1[0];

                int i = 1;
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName == "Name")
                        continue;

                    double value = 0;
                    try
                    {
                        value = (double)pc1.GetType().GetProperty("Value" + i.ToString()).GetValue(pc1, null);
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }
                    newrow2[dc] = value;
                    i++;
                }

            }

            #endregion

            IList<PSP_Calc_Spring> list2 = Services.BaseService.GetList<PSP_Calc_Spring>("SelectPSP_Calc_SpringByFlag", type);
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
                    Services.BaseService.Create<PSP_Calc_Spring>(pcss);
                    list2.Add(pcss);
                }
            }



        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            //try
            //{
            //    pc.Value2 = Convert.ToDouble(textEdit1.Text.Trim());
            //}
            //catch { }
            Services.BaseService.Update<PSP_Calc>(pc);



            int i = 1;
            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.ColumnName == "Name")
                    continue;

                double value = 0;
                try
                {
                    value = Convert.ToDouble(newrow2[dc].ToString());
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }

                pc1.GetType().GetProperty("Value" + i.ToString()).SetValue(pc1, value, null);
                i++;
            }
            Services.BaseService.Update<PSP_Calc>(pc1);



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


            //PSP_Calc_Spring pcs = obj as PSP_Calc_Spring;
            //Services.BaseService.Update<PSP_Calc_Spring>(pcs);


        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


    }
}