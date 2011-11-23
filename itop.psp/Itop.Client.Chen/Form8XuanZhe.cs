using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Client.Base;
namespace Itop.Client.Chen
{
    public partial class Form8XuanZhe : FormBase
    {
        private DataTable dt1 = new DataTable();
        private DataTable dt2 = new DataTable();
        private ArrayList a1 = new ArrayList();
        private ArrayList a2 = new ArrayList();
        public DataTable DT1
        { 
            get{return dt1;}
        }

        public DataTable DT2
        {
            set { dt2=value; }
        }
        public ArrayList A1
        {
            get { return a1; }
        }
        public ArrayList A2
        {
            get { return a2; }
        }
        public Form8XuanZhe()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
            string str="";
            a1.Clear();
            a1.Clear();
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                //if (System.DBNull.Value == gridView1.GetRowCellValue(i, "B"))
                //    continue;
                if ((bool)gridView1.GetRowCellValue(i, "B"))
                {
                    
                    switch (gridView1.GetRowCellValue(i, "A").ToString())
                    {
                        case "年增长率法":
                            str = "ArverageIncreasingMethod";
                            break;


                        case "弹性系数法":
                            str = "SpringCoefficientMethod";
                            break;


                        case "移动平均法":

                            str = "TwiceMoveArverageMethod";
                            break;

                        case "灰色模型法":

                            str = "GrayMethod";
                            break;

                        case  "线性回归法":

                            str = "LinearlyRegressMethod";
                            break;

                        case "指数增长法":

                            str = "IndexIncreaseMethod";
                            break;

                        case "指数平滑法":

                            str = "IndexSmoothMethod";
                            break;

                        case "线性趋势法":

                            str = "LinearlyTrend";
                            break;
                        case "专家决策法":

                            str = "ProfessionalLV";
                            break;
                        case "分区县年增长率法":
                            str = "FQXArverageIncreasingMethod";
                            break;

                        case "分区县移动平均法":

                            str = "FQXTwiceMoveArverageMethod";
                            break;

                        case "分区县灰色模型法":

                            str = "FQXGrayMethod";
                            break;

                        case "分区县线性回归法":

                            str = "FQXLinearlyRegressMethod";
                            break;

                        case "分区县指数增长法":

                            str = "FQXIndexIncreaseMethod";
                            break;

                        case "分区县指数平滑法":

                            str = "FQXIndexSmoothMethod";
                            break;

                        case "分区县线性趋势法":

                            str = "FQXLinearlyTrend";
                            break;
                        case "分产业年增长率法":
                            str = "FCYArverageIncreasingMethod";
                            break;

                        case "分产业移动平均法":

                            str = "FCYTwiceMoveArverageMethod";
                            break;

                        case "分产业灰色模型法":

                            str = "FCYGrayMethod";
                            break;

                        case "分产业线性回归法":

                            str = "FCYLinearlyRegressMethod";
                            break;

                        case "分产业指数增长法":

                            str = "FCYIndexIncreaseMethod";
                            break;

                        case "分产业指数平滑法":

                            str = "FCYIndexSmoothMethod";
                            break;

                        case "分产业线性趋势法":

                            str = "FCYLinearlyTrend";
                            break;
                    }
                  
                    a1.Add(str);
                 
                }
            }

            if (a1.Count < 2)
            {

                MessageBox.Show("请至少选择 2 个预测方法！");

                return;
            }
            
            foreach (DataRow dr in dt2.Rows)
            {
                if (System.DBNull.Value == dr["B"])
                    continue;
                if (Convert.ToBoolean( dr["B"]))
                {
                    //a2.Add( dr["Title"].ToString());
                    a2.Add(dr["ID"].ToString());
                }
            
            }
            if (a2.Count < 1)
            {
                MessageBox.Show("请至少选择 1 个项目名称！");
               
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void Form8XuanZhe_Load(object sender, EventArgs e)
        {
            dt1.Columns.Add("A", typeof(string));
            dt1.Columns.Add("B", typeof(bool));

            DataRow row = dt1.NewRow();
            row["A"] = "年增长率法";
            row["B"] = true;
            dt1.Rows.Add(row);

            row = dt1.NewRow();
            row["A"] = "弹性系数法";
            row["B"] = false;
            dt1.Rows.Add(row);

            row = dt1.NewRow();
            row["A"] = "移动平均法";
            row["B"] = false;
            dt1.Rows.Add(row);

            row = dt1.NewRow();
            row["A"] = "灰色模型法";
            row["B"] = false;
            dt1.Rows.Add(row);

            row = dt1.NewRow();
            row["A"] = "线性回归法";
            row["B"] = false;
            dt1.Rows.Add(row);

            row = dt1.NewRow();
            row["A"] = "指数增长法";
            row["B"] = false;
            dt1.Rows.Add(row);

            row = dt1.NewRow();
            row["A"] = "指数平滑法";
            row["B"] = false;
            dt1.Rows.Add(row);

            row = dt1.NewRow();
            row["A"] = "专家决策法";
            row["B"] = false;
            dt1.Rows.Add(row);
            //row = dt1.NewRow();
            //row["A"] = "分区县年增长率法";
            //row["B"] = false;
            //dt1.Rows.Add(row);

            //row = dt1.NewRow();
            //row["A"] = "分区县移动平均法";
            //row["B"] = false;
            //dt1.Rows.Add(row);

            //row = dt1.NewRow();
            //row["A"] = "分区县灰色模型法";
            //row["B"] = false;
            //dt1.Rows.Add(row);

            //row = dt1.NewRow();
            //row["A"] = "分区县线性回归法";
            //row["B"] = false;
            //dt1.Rows.Add(row);

            //row = dt1.NewRow();
            //row["A"] = "分区县指数增长法";
            //row["B"] = false;
            //dt1.Rows.Add(row);

            //row = dt1.NewRow();
            //row["A"] = "分区县指数平滑法";
            //row["B"] = false;
            //dt1.Rows.Add(row);

            //row = dt1.NewRow();
            //row["A"] = "分产业年增长率法";
            //row["B"] = false;
            //dt1.Rows.Add(row);

            //row = dt1.NewRow();
            //row["A"] = "分产业移动平均法";
            //row["B"] = false;
            //dt1.Rows.Add(row);

            //row = dt1.NewRow();
            //row["A"] = "分产业灰色模型法";
            //row["B"] = false;
            //dt1.Rows.Add(row);

            //row = dt1.NewRow();
            //row["A"] = "分产业线性回归法";
            //row["B"] = false;
            //dt1.Rows.Add(row);

            //row = dt1.NewRow();
            //row["A"] = "分产业指数增长法";
            //row["B"] = false;
            //dt1.Rows.Add(row);

            //row = dt1.NewRow();
            //row["A"] = "分产业指数平滑法";
            //row["B"] = false;
            //dt1.Rows.Add(row);

            gridControl1.DataSource = dt1;
            if(!dt2.Columns.Contains("B"))
            dt2.Columns.Add("B", typeof(bool));
            foreach (DataRow dr in dt2.Rows)
                dr["B"] = false;
            treeList1.DataSource = dt2;
            treeList1.ExpandAll();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            //if(checkEdit1.Checked)
            foreach (DataRow dr in dt1.Rows)
                dr["B"] = checkEdit1.Checked;
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            //if (checkEdit2.Checked)
            foreach (DataRow dr in dt2.Rows)
                dr["B"] = checkEdit2.Checked;
        }

       
       

        //private void repositoryItemCheckEdit1_EditValueChanged(object sender, EventArgs e)
        //{
        //    DevExpress.XtraEditors.CheckEdit chk = (DevExpress.XtraEditors.CheckEdit)sender;
        //    if (!chk.Checked)
        //        if (checkEdit1.Checked)
        //            checkEdit1.Checked = false;
        //}

        //private void repositoryItemCheckEdit3_EditValueChanged(object sender, EventArgs e)
        //{
        //    DevExpress.XtraEditors.CheckEdit chk = (DevExpress.XtraEditors.CheckEdit)sender;
        //    if (!chk.Checked)
        //        if (checkEdit2.Checked)
        //            checkEdit2.Checked = false;
        //}

       
    }
}