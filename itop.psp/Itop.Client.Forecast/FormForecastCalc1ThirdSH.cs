using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Forecast;
using System.Collections;
using Itop.Client.Common;
using Itop.Client.Base;
namespace Itop.Client.Forecast
{
    public partial class FormForecastCalc1ThirdSH : FormBase
    {
        public FormForecastCalc1ThirdSH()
        {
            InitializeComponent();
        }
        int firstyear = 0;
        int endyear = 0;
        
        IList<Ps_Calc> list1 = new List<Ps_Calc>();
        Ps_Calc pc1 = new Ps_Calc();
        private bool isedit = false;
        int type = 1;
        DataTable dt = new DataTable ();
       
        DataRow newrow2 = null;

        public bool ISEdit
        {

            set { isedit = value; }
        }
        Ps_forecast_list forecastReport;
        public int Firstyear
        {
            get { return firstyear; }
            set { firstyear = value; }
        }
        public int Type
        {
            get { return type; }
            set { type = value; }
        }
        public int Endyear
        {
            get { return endyear; }
            set { endyear = value; }
        }
        public Ps_forecast_list PForecastReports
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
        public DataTable Returndt
        {
            get { return dt; }
            set { dt = value; }
        }
        Hashtable ha = new Hashtable();
        public Hashtable Ha
        {
            get { return ha; }
            set { ha = value; }
        }
        ArrayList algotemlist = new ArrayList();
        public ArrayList Algotemlist
        {
            get { return algotemlist; }
            set { algotemlist = value; }
        }
        private void HideToolBarButton()
        {

            if (!isedit)
            {
                //vGridControl2.Enabled = false;
                //simpleButton1.Visible = false;
            }

        }

        private void FormForecastCalc1Third_Load(object sender, EventArgs e)
        {
            dt.Columns.Clear();
           
           

           
            dt.Columns.Add("A", typeof(string));
            dt.Columns.Add("B", typeof(string));
            dt.Columns.Add("CalcID", typeof(string));
            dt.Columns.Add("C", typeof(string));
            dt.Columns.Add("ID", typeof(string));
            Ps_Calc pcs = new Ps_Calc();
            pcs.Forecast = type;
            pcs.ForecastID = forecastReport.ID;
            list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);
            
            for (int i =endyear+1; i <= forecastReport.YcEndYear; i++)
            {

               
                comboBox4.Items.Add(i + "年");
                comboBox5.Items.Add(i + "年");
               
            }
            foreach (DataRow dr2 in dataTable.Rows)
            {
                comboBox1.Items.Add(dr2["Title"]);
                comboBox2.Items.Add(dr2["ID"]);
            }
            DataRow dr;
            foreach (Ps_Calc pcs2 in list1)
            {
                dr = dt.NewRow();
                dr["ID"] = pcs2.ID;
                dr["A"] = pcs2.Col1;
                dr["B"] = pcs2.Value4+"%";
                dr["CalcID"] = pcs2.CalcID;

                if (pcs2.Value2 != 0 && pcs2.Value3 != 0 && (!(pcs2.Value2 < endyear || pcs2.Value3 > forecastReport.YcEndYear)) && comboBox1.Items.IndexOf(pcs2.Col1) > -1 && (comboBox1.Items.IndexOf(pcs2.Col1) == comboBox2.Items.IndexOf(pcs2.CalcID)))
                {
                    dr["C"] = pcs2.Value2 + "年-" + pcs2.Value3 + "年";
                }
                else
                {
                    continue;
                }

            dt.Rows.Add(dr);
            }
            dt=GetSortTable( dt, "A,C", true);
            gridControl1.DataSource = dt;


           
        }
        public System.Data.DataTable GetSortTable(System.Data.DataTable dt, string Column, bool bl)
        {
            string sort = " asc";
            if (!bl)
                sort = " desc";

            DataView dv = dt.DefaultView;
            dv.Sort = Column + sort;
            System.Data.DataTable dt2 = dv.ToTable();
            return dt2;
        }


        private void savevalue(Ps_Calc pc11,bool iscreat)
        {
           if(!iscreat)
           {
          
                    Services.BaseService.Update<Ps_Calc>(pc11);
            
            }
           else
            {
                //Ps_Calc pcs = new Ps_Calc();
                //pcs.ID = Guid.NewGuid().ToString();
                //pcs.Forecast = type;
                //pcs.ForecastID = forecastReport.ID;
                //pcs.CalcID = title;



                Services.BaseService.Create<Ps_Calc>(pc11);

            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ifirst = 0;
            int isec = 0;
            if (comboBox4.SelectedItem == null)
            {
                return;
            }
            if (comboBox4.SelectedItem.ToString() != "")
            {
                ifirst = Convert.ToInt32(comboBox4.SelectedItem.ToString().Replace("年", ""));
            }

            if (comboBox5.SelectedItem != null && comboBox5.SelectedItem.ToString() != ""&&!spinEdit1.Visible)
            {
                isec = Convert.ToInt32(comboBox5.SelectedItem.ToString().Replace("年", ""));
            }
            else
            {
                isec = ifirst + Convert.ToInt32(spinEdit1.Value);
                if (isec > forecastReport.YcEndYear )
                {

                    isec = forecastReport.YcEndYear;
                }
                comboBox5.SelectedIndex = comboBox5.Items.IndexOf(isec + "年");
            }
            if (isec != 0)
            {
                if (ifirst > isec)
                {
                    comboBox4.SelectedIndex = -1;
                    return;
                }
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ifirst = 0;
            int isec = 0;
            if (comboBox5.SelectedItem == null)
            {
                return;
            }
            if (comboBox5.SelectedItem.ToString() != "")
            {
                ifirst = Convert.ToInt32(comboBox5.SelectedItem.ToString().Replace("年", ""));
            }
           
       
            if (comboBox4.SelectedItem != null && comboBox4.SelectedItem.ToString() != "")
            {
                isec = Convert.ToInt32(comboBox4.SelectedItem.ToString().Replace("年", ""));
                spinEdit1.Value=(decimal)(ifirst-isec);
            }
            if (isec != 0)
            {
                if (isec > ifirst)
                {
                    comboBox5.SelectedIndex = -1;
                    return;
                }
            }
        }
    
       
    
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            if (gridView1.FocusedRowHandle<0)
            {
                return;
            }
           
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            DataRowView drv = gridView1.GetRow(gridView1.FocusedRowHandle) as DataRowView;
            if (drv == null)
            {

                return;
            }
            DataRow dr = drv.Row;

            spinEdit2.Value = Convert.ToDecimal(dr["B"].ToString().Replace("%", ""));

            comboBox1.SelectedIndex = comboBox1.Items.IndexOf(dr["A"]);
            comboBox2.SelectedIndex = comboBox2.Items.IndexOf(dr["CalcID"]);
            comboBox4.SelectedIndex = comboBox4.Items.IndexOf(dr["C"].ToString().Split('-')[0]);
            comboBox5.SelectedIndex = comboBox5.Items.IndexOf(dr["C"].ToString().Split('-')[1]);
        }

        private void button6_Click(object sender, EventArgs e)
        {
         
            
         
        }

      
        //根据节点返回此行的历史数据
        private double[] GenerateHistoryValue(DataRow node, int syear, int eyear)
        {
            double[] rt = new double[eyear - syear + 1];
            for (int i = 0; i < eyear - syear + 1; i++)
            {
                object obj = node["y" + (syear + i)];
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = comboBox1.SelectedIndex;
            DataRow[] dr= dataTable.Select("id='" + comboBox2.SelectedItem + "'");
            double[] historyValues = GenerateHistoryValue(dr[0], firstyear, endyear);
            label6.Text = "历史年平均增长率：" + Math.Round(Calculator.AverageIncreasing(historyValues) * 100, 2) + "%";
        }


        //确定

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DataTable dttemp = GetSortTable(dt, "A,C", true);
            int i = 0;
            ha.Clear();
            int year4 = 0;
            foreach (string strtemp in comboBox1.Items)
            {
                bool isfirst = false;
                i = 0;
                for (; i < dttemp.Rows.Count - 1; i++)
                {
                    if (strtemp != dttemp.Rows[i]["A"].ToString())

                        continue;
                    if (!isfirst)
                    {

                        int year1 = Convert.ToInt32(dttemp.Rows[i]["C"].ToString().Split('-')[0].Replace("年", ""));
                        if (year1 > endyear + 1)
                        {
                            year1 -= 1;
                            MessageBox.Show("出错，" + dttemp.Rows[i]["A"] + "的" + endyear + "至" + year1 + "增长率未设置");
                            return;
                        }
                        isfirst = true;
                    }
                    if (strtemp != dttemp.Rows[i + 1]["A"].ToString())
                    {
                        year4 = Convert.ToInt32(dttemp.Rows[i]["C"].ToString().Split('-')[1].Replace("年", ""));
                        year4 += 1;
                        if (year4 < forecastReport.EndYear)
                        {
                            MessageBox.Show("出错，" + dttemp.Rows[i]["A"] + "的" + year4 + "至" + (forecastReport.EndYear) + "增长率未设置");
                            return;
                        }
                        continue;
                    }
                    int year2 = Convert.ToInt32(dttemp.Rows[i]["C"].ToString().Split('-')[1].Replace("年", ""));
                    int year3 = Convert.ToInt32(dttemp.Rows[i + 1]["C"].ToString().Split('-')[0].Replace("年", ""));

                    if (year2 - year3 != -1)
                    {
                        MessageBox.Show("出错，" + dttemp.Rows[i]["A"] + "的" + dttemp.Rows[i]["C"] + "与" + dttemp.Rows[i + 1]["C"].ToString() + "不连续");
                        return;
                    }
                    //if(i==0)
                    //ha.Add(i, dttemp.Rows[i]["A"].ToString().Replace("%", "") + "@" + dttemp.Rows[i]["C"].ToString().Replace("年", ""));
                    //ha.Add(i+1, dttemp.Rows[i+1]["A"].ToString().Replace("%", "") + "@" + dttemp.Rows[i+1]["C"].ToString().Replace("年", ""));




                }
                if (dttemp.Rows.Count > 0)
                {
                    if (strtemp != dttemp.Rows[i]["A"].ToString())
                        continue;

                    //ha.Add(i, dttemp.Rows[i]["A"].ToString().Replace("%", "") + "@" + dttemp.Rows[i]["C"].ToString().Replace("年", ""));
                    if (!isfirst)
                    {

                        int year1 = Convert.ToInt32(dttemp.Rows[i]["C"].ToString().Split('-')[0].Replace("年", ""));
                        if (year1 > endyear + 1)
                        {
                            year1 -= 1;
                            MessageBox.Show("出错，" + dttemp.Rows[i]["A"] + "的" + endyear + "至" + year1 + "增长率未设置");
                            return;
                        }
                        isfirst = true;
                    }
                    year4 = Convert.ToInt32(dttemp.Rows[i]["C"].ToString().Split('-')[1].Replace("年", ""));
                    year4 += 1;
                    if (year4 < forecastReport.EndYear)
                    {
                        MessageBox.Show("出错，" + dttemp.Rows[i]["A"] + "的" + year4 + "至" + (forecastReport.EndYear) + "增长率未设置");
                        return;
                    }
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        //修改
        private void simpleButton1_Click(object sender, EventArgs e)
        {


            if (gridView1.FocusedRowHandle < 0)
            {
                return;
            }


            if (comboBox4.SelectedItem == null || comboBox5.SelectedItem == null)
            {
                MessageBox.Show("参数设置不正确！");
                return;
            }
            int yearselect1 = Convert.ToInt32(comboBox4.SelectedItem.ToString().Replace("年", ""));
            int yearselect2 = Convert.ToInt32(comboBox5.SelectedItem.ToString().Replace("年", ""));
            string strname = comboBox1.SelectedItem.ToString();
            DataRowView drv = gridView1.GetRow(gridView1.FocusedRowHandle) as DataRowView;
            DataRow dr = drv.Row;
            int intex = dt.Rows.IndexOf(dr);
            int i = 0;
            foreach (DataRow ndr in dt.Rows)
            {
                if (strname != ndr["A"].ToString())
                {
                    i++;
                    continue;
                }
                int year1 = Convert.ToInt32(ndr["C"].ToString().Split('-')[0].Replace("年", ""));
                int year2 = Convert.ToInt32(ndr["C"].ToString().Split('-')[1].Replace("年", ""));
                if ((!(yearselect1 > year2 || year1 > yearselect2)) && i != intex)
                {
                    MessageBox.Show(ndr["A"] + "的" + "所选年份包含在" + ndr["C"]);
                    return;
                }
                i++;
            }




            dt.Rows[intex]["B"] = spinEdit2.Value + "%";



            dt.Rows[intex]["C"] = comboBox4.SelectedItem + "-" + comboBox5.SelectedItem;

            Ps_Calc pcs = new Ps_Calc();
            pcs.ID = dt.Rows[intex]["ID"].ToString();
            pcs.Forecast = type;
            pcs.ForecastID = forecastReport.ID;
            pcs.CalcID = comboBox2.SelectedItem.ToString();
            pcs.Value2 = Convert.ToDouble(comboBox4.SelectedItem.ToString().Replace("年", ""));
            pcs.Value3 = Convert.ToDouble(comboBox5.SelectedItem.ToString().Replace("年", ""));
            pcs.Value4 = Convert.ToDouble(dr["B"].ToString().Replace("%", ""));
            pcs.Col1 = comboBox1.SelectedItem.ToString();
            dt.Rows[intex]["ID"] = pcs.ID;
            dt.Rows[intex]["CalcID"] = pcs.CalcID;
            dt.Rows[intex]["A"] = pcs.Col1;
            savevalue(pcs, false);
            gridView1.RefreshData();
        }



        //删除
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0)
            {
                return;
            }
            int i = gridView1.FocusedRowHandle;
            //gridView1.FocusedRowHandle = -1;
            DataRowView drv = gridView1.GetRow(i) as DataRowView;
            if (drv == null)
            {
                MessageBox.Show("删除失败！");
                gridView1.RefreshData();
                return;
            }
            Ps_Calc pcs = new Ps_Calc();
            pcs.ID = drv.Row["ID"].ToString();
            pcs = Services.BaseService.GetOneByKey<Ps_Calc>(pcs);
            if (pcs != null)
                Services.BaseService.Delete<Ps_Calc>(pcs);
            else
            {
                MessageBox.Show("删除失败!");
                gridView1.RefreshData();
                return;
            }
            dt.Rows.Remove(drv.Row);

            gridView1.RefreshData();
        }
       
        //重置
        private void simpleButton3_Click(object sender, EventArgs e)
        {

            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
        }


        //增加
        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox4.SelectedItem == null || comboBox5.SelectedItem == null)
            {
                MessageBox.Show("参数设置不正确！");
                return;
            }
            int yearselect1 = Convert.ToInt32(comboBox4.SelectedItem.ToString().Replace("年", ""));
            int yearselect2 = Convert.ToInt32(comboBox5.SelectedItem.ToString().Replace("年", ""));
            string strname = comboBox1.SelectedItem.ToString();
            foreach (DataRow ndr in dt.Rows)
            {
                if (strname != ndr["A"].ToString())
                    continue;
                int year1 = Convert.ToInt32(ndr["C"].ToString().Split('-')[0].Replace("年", ""));
                int year2 = Convert.ToInt32(ndr["C"].ToString().Split('-')[1].Replace("年", ""));
                if (!(yearselect1 > year2 || year1 > yearselect2))
                {
                    MessageBox.Show(ndr["A"] + "的" + "所选年份包含在" + ndr["C"]);
                    return;
                }
            }
            DataRow dr;
            dr = dt.NewRow();

            dr["B"] = spinEdit2.Value + "%"; ;
            Ps_Calc pcs = new Ps_Calc();
            pcs.ID = Guid.NewGuid().ToString();
            pcs.Forecast = type;
            pcs.ForecastID = forecastReport.ID;
            pcs.CalcID = comboBox2.SelectedItem.ToString();
            pcs.Value2 = Convert.ToDouble(comboBox4.SelectedItem.ToString().Replace("年", ""));
            pcs.Value3 = Convert.ToDouble(comboBox5.SelectedItem.ToString().Replace("年", ""));
            pcs.Value4 = Convert.ToDouble(dr["B"].ToString().Replace("%", ""));
            pcs.Col1 = comboBox1.SelectedItem.ToString();
            dr["C"] = comboBox4.SelectedItem + "-" + comboBox5.SelectedItem;
            dr["ID"] = pcs.ID;
            dr["CalcID"] = pcs.CalcID;
            dr["A"] = pcs.Col1;
            dt.Rows.Add(dr);


            savevalue(pcs, true);


            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;

        }

       
      
    }
}