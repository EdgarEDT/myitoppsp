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
    public partial class FormCalculatorFS : FormBase
    {
        public FormCalculatorFS()
        {
            InitializeComponent();
        }
        string type = "";
        PSP_Calc pc = new PSP_Calc();
        PSP_Calc pc1 = new PSP_Calc();
        private bool isedit=false;

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
        public bool ISEdit
        {
            
            set { isedit = value; }
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

        private void HideToolBarButton()
        {

            if (!isedit)
            {
                vGridControl2.Enabled = false;
                gridControl1.Enabled = false;
                spinEdit1.Enabled = false;
                spinEdit2.Enabled = false;
                simpleButton1.Visible = false;
            }
          
        }

        private void FormCalculator_Load(object sender, EventArgs e)
        {
            HideToolBarButton();
            #region 年平均s
            dt = new DataTable();
            dt.Columns.Add("Name");
            //MessageBox.Show("1");
            foreach (DataRow dataRow in dataTable.Rows)
            {
                TreeListNode treeNode = treeList1.FindNodeByKeyID(dataRow["ID"]);
                if (treeNode != null && !treeNode.HasChildren)
                {
                        dt.Columns.Add(dataRow["Title"].ToString().Trim(), typeof(double));
                        DevExpress.XtraVerticalGrid.Rows.EditorRow editorRow = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
                        editorRow.Properties.FieldName = dataRow["Title"].ToString().Trim();
                        editorRow.Properties.Caption = dataRow["Title"].ToString().Trim();
                        editorRow.Height = 20;
                        editorRow.Properties.RowEdit = this.repositoryItemCalcEdit4;
                        this.vGridControl2.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] { editorRow });
                }
            }
            DataRow newrow1 = dt.NewRow();
            newrow1["Name"] = "历史增长率";
            foreach (DataRow dataRow in dataTable.Rows)
            {
                TreeListNode treeNode = treeList1.FindNodeByKeyID(dataRow["ID"]);
                if (treeNode != null && !treeNode.HasChildren)
                {
                    int forecastYears = forecastReport.EndYear - forecastReport.StartYear;
                    double[] historyValues = GenerateHistoryValue(treeNode);
                    newrow1[dataRow["Title"].ToString().Trim()] = Calculator.AverageIncreasing(historyValues);
                }
            }


            dt.Rows.Add(newrow1); 
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

            spinEdit1.Value = Convert.ToDecimal(pc.Value1);
            spinEdit2.Value = Convert.ToDecimal(pc.Value2);
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

            string str11 = " Flag='" + type + "' and Type='弹性系数'";
            IList<PSP_Calc_Spring> list2 = Services.BaseService.GetList<PSP_Calc_Spring>("SelectPSP_Calc_SpringByWhere", str11);
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
                    pcss.Type = "弹性系数";
                    Services.BaseService.Create<PSP_Calc_Spring>(pcss);
                    list2.Add(pcss);
                }
            }

            Hashtable htt1 = new Hashtable();
            
            for (int i = 0; i < forecastReport.HistoryYears; i++)
            {
                double value1 = 0.0;
                double value2 = 0.0;
                double value3 = 0.0;
                double value4 = 0.0;
                double value5 = 0.0;
                int yeara=forecastReport.StartYear - forecastReport.HistoryYears + i + 1;
                PSP_Calc_Spring pcss = new PSP_Calc_Spring();
                pcss.ID = Guid.NewGuid().ToString();
                
                pcss.Name = yeara.ToString();
                pcss.Flag = type;
                string str = " TypeID=1 and Year=" + (yeara-1);
                IList<PSP_Values> pg = Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", str);
                if (pg.Count > 0)
                    value3 = pg[0].Value;

                str = " TypeID=1 and Year=" + yeara;
                IList<PSP_Values> pg1 = Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", str);
                if (pg1.Count > 0)
                    value5 = pg1[0].Value;
                pcss.Value2 = (value5 - value3) / value3;

                
                 str = " TypeID=2 and Year=" + (yeara-1);
                IList<PSP_Values> pv1 = Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", str);
                if (pv1.Count > 0)
                    value1 = pv1[0].Value;

                str = " TypeID=2 and Year=" + yeara;
                IList<PSP_Values> pv2 = Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", str);
                if (pv2.Count > 0)
                    value2 = pv2[0].Value;

                if (value1 != 0 && value3 != 0)
                {
                    value4 = (value2 - value1) / (value1 * (value5 - value3) / value3);
                }

                pcss.Value1 = value4;
                       
                
                list2.Add(pcss);
            }

            gridControl1.DataSource = list2;




            ////////string str12 = " Flag='" + type + "' and Type='专家指定'";
            ////////IList<PSP_Calc_Spring> list3 = Services.BaseService.GetList<PSP_Calc_Spring>("SelectPSP_Calc_SpringByWhere", str12);
            ////////int years1 = forecastReport.EndYear - forecastReport.StartYear;
            ////////for (int i = 1; i <= years1; i++)
            ////////{
            ////////    bool bl = false;
            ////////    foreach (PSP_Calc_Spring pcs in list3)
            ////////    {
            ////////        if ((forecastReport.StartYear + i).ToString() == pcs.Name)
            ////////        {
            ////////            bl = true;
            ////////        }
            ////////    }
            ////////    if (!bl)
            ////////    {
            ////////        PSP_Calc_Spring pcss = new PSP_Calc_Spring();
            ////////        pcss.ID = Guid.NewGuid().ToString();
            ////////        pcss.Name = (forecastReport.StartYear + i).ToString();
            ////////        pcss.Value1 = 0;
            ////////        pcss.Value2 = 0;
            ////////        pcss.Flag = type;
            ////////        pcss.Type = "专家指定";
            ////////        Services.BaseService.Create<PSP_Calc_Spring>(pcss);
            ////////        list3.Add(pcss);
            ////////    }
            ////////}
            ////////gridControl2.DataSource = list3;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            pc.Value1 = Convert.ToDouble(spinEdit1.Value);
            pc.Value2 = Convert.ToDouble(spinEdit2.Value);
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

        private void vGridControl2_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (vGridControl2.FocusedRecord == 0)
                e.Cancel = true;
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

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //object obj = this.gridView2.GetRow(this.gridView2.FocusedRowHandle);
            //if (obj == null)
            //    return;

            //PSP_Calc_Spring pcs = obj as PSP_Calc_Spring;
            //Services.BaseService.Update<PSP_Calc_Spring>(pcs);
        }


    }
}