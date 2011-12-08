using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Domain.Forecast;
using Itop.Client.Base;
/*
 这个窗体是复合预测的设置窗体
 */
namespace Itop.Client.Forecast
{
    public partial class FormArgumentSetNew : FormBase
    {
        bool Edit_Year = false;
        bool Can_Edit_Year = false;
        bool Sp_CanEdit = false;
        DataRow old_row = null;
        private PublicFunction m_pf = new PublicFunction();
        private DataTable dt = new DataTable();
        private IList<Ps_Calc> m_CalcList = new List<Ps_Calc>();
        private bool m_isEdit = false;
        private int type = 20;//由于数据库中的类型不确定所这里暂时为20

        public bool ISEdit
        {
            set { m_isEdit = value; }
        }
        Ps_forecast_list forecastReport;
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
        public FormArgumentSetNew()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton6_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormArgumentSetNew_Load(object sender, EventArgs e)
        {
            //加载datatable
            m_pf.DataTableAddColumn(dt);
            m_CalcList = m_pf.SelectedForecastList(type, forecastReport);
            //初始化comboBox
            m_pf.InitCommboBox(this.comboBox2,null,0);
            m_pf.InitCommboBox(this.comboBox6, null,0);
            m_pf.InitCommboBox(this.comboBox1, forecastReport,0);
            m_pf.InitCommboBox(this.comboBox3, forecastReport,0);
            m_pf.InitCommboBox(this.comboBox4, forecastReport,0);
            m_pf.InitCommboBox(this.comboBox5, forecastReport,0);
            //加载gridControl1
            InitGridControl();
        }
        private void InitGridControl()
        {
            DataRow dr;
            dt.Clear();
            foreach (Ps_Calc pcs2 in m_CalcList)
            {
                dr = dt.NewRow();
                dr["id"] = pcs2.ID;
                dr["A"] = pcs2.CalcID;
                if (pcs2.Value5 != 0 && pcs2.Value4 != 0)
                {
                    dr["B"] = pcs2.Value4 + "年-" + pcs2.Value5 + "年";
                }

                else
                {
                    continue;
                }
                if (pcs2.Value2 != 0 && pcs2.Value3 != 0)
                {
                    dr["C"] = pcs2.Value2 + "年-" + pcs2.Value3 + "年";
                }
                else
                {
                    continue;
                }
                dr["D"] = pcs2.Col4;//权重值

                dt.Rows.Add(dr);
            }
            //dt=
            //    GetSortTable(ref dt, "C", true);
            gridControl1.DataSource = dt;
        }
        /// <summary>
        /// 方案下拉框选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.SelectedItem == null)
            {
                return;
            }
            if (comboBox2.SelectedItem.ToString() == "外推法")
            {
                if (!comboBox6.Items.Contains("外推法(年增长率法)"))
                {
                    comboBox6.Items.Clear();
                    m_pf.InitCommboBox(comboBox6,null,1);
                }
                SetSubalgorithm(true);
            }
            else if (comboBox2.SelectedItem.ToString() == "相关法")
            {
                if (!comboBox6.Items.Contains("相关法(直线)"))
                {
                    comboBox6.Items.Clear();
                    m_pf.InitCommboBox(comboBox6,null,2);
                }
                SetSubalgorithm(true);
            }
            else if (comboBox2.SelectedItem.ToString() == "弹性系数法")
            {
                simpleButton7.Visible = true;
                SetSubalgorithm(false);
            }
            else if (comboBox2.SelectedItem.ToString() == "指数平滑法")
            {
                textEdit9.Visible = true;
                spinEdit4.Visible = true;
                SetSubalgorithm(false);
                Ps_Calc pcs = new Ps_Calc();
                pcs.Forecast = 25;
                pcs.ForecastID = forecastReport.ID;
                IList<Ps_Calc> list1 = m_pf.SelectedForecastList(25, forecastReport);
               // IList<Ps_Calc> list1 = Common.Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);

                 if (list1.Count > 0)
                {
                    spinEdit4.Value = Convert.ToDecimal(list1[0].Value1);
                }
                else
                {
                    spinEdit4.Value =Convert.ToDecimal( 0.2);
                }
                
            }
            else if (comboBox2.SelectedItem.ToString() == "专家决策法")
            {
                Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
                psp_Type.ForecastID = forecastReport.ID;
                psp_Type.Forecast = 7;
                IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);
                if (listTypes.Count < 1)
                {
                    MessageBox.Show("专家决策法没有数据，选择失败");
                    comboBox2.SelectedIndex = -1;
                }
                SetSubalgorithm(false);
            }
            else
            {
                SetSubalgorithm(false);
            }

            if (comboBox2.SelectedItem.ToString() != "弹性系数法")
            {
                simpleButton7.Visible = false;
            }
            if (comboBox2.SelectedItem.ToString() != "指数平滑法")
            {
                textEdit9.Visible = false;
                spinEdit4.Visible = false;

            }
        }
        /// <summary>
        /// 设置子算法控件是否显示

        /// </summary>
        /// <param name="IsVisible"></param>
        private void SetSubalgorithm(bool IsVisible)
        {
            comboBox6.Visible = IsVisible;
            textEdit7.Visible = IsVisible;
            
        }
        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (Can_Edit_Year)
            {
                comboBox1.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
                comboBox4.SelectedIndex = -1;
                comboBox5.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                spinEdit1.Value= 1;
                Sp_CanEdit = true;
                spinEdit2.Value = 1;
                spinEdit3.Value = 1;
                Sp_CanEdit = false;
            }
           
            //comboBox6.SelectedIndex = -1;
            //SetSubalgorithm(false);
            //spinEdit1.Value = 0;
            //spinEdit2.Value = 1;
            //spinEdit3.Value = 1;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Ps_Calc pcs = new Ps_Calc();

            if(m_pf.DecideDataAddin_comboBox(comboBox2))
            {
                return;
            }
            if(m_pf.DecideDataAddin_comboBox(comboBox6))
            {
                return;
            }
            if(m_pf.DecideDataAddin_comboBox(comboBox1))
            {
                return;
            }
            if(m_pf.DecideDataAddin_comboBox(comboBox4))
            {
                return;
            }
            if(m_pf.DecideDataAddin_comboBox(comboBox3))
            {
                return;
            }
            
            if(m_pf.DecideDataAddin_comboBox(comboBox5))
            {
                return;
            }
            if(m_pf.DecideDataAddin_SpinEdit(spinEdit1))
            {
                return;
            }


            AddToDataRow();
            if(AddDataToDatatable(pcs))
            {
                return;
            }
            SaveValue(pcs, true);
            //清空spinEdit
            spinEdit1.Value = 1;
            if (gridView1.RowCount <= 1)
            {
                Can_Edit_Year = true;
            }
            else
            {
                Can_Edit_Year = false;
            }
            YearSelect();
            
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="_Calc"></param>
        /// <param name="_bCreate">true is create ,false is update</param>
        private void SaveValue(Ps_Calc _Calc, bool _bCreate)
        {
            if(!_bCreate)
            {
                //Common.Services.BaseService.GetObject("UpdatePs_CalcWhereID", _Calc);
                Common.Services.BaseService.Update("UpdatePs_CalcWhereID", _Calc);
            }
            else
            {
                Common.Services.BaseService.Create<Ps_Calc>(_Calc);
            }
        }
        /// <summary>
        /// 不能选择同一种算法

        /// </summary>
        /// <param name="strTemp"></param>
        private bool bCompare(Object strTemp)
        {
            bool bTemp = false;
            if(m_CalcList != null)
            {
                foreach (Ps_Calc m_calc in m_CalcList)
                {
                    if (strTemp.ToString() == m_calc.CalcID)
                    {
                        bTemp = true;
                        break;
                    }
                }
            }
            else
            {
                
            }
            return bTemp;
        }
        /// <summary>
        /// 添加数据到datatable中

        /// </summary>
        private bool AddDataToDatatable(Ps_Calc pcs)
        {
            DataRow dr;
            Ps_Calc _CalcTemp = new Ps_Calc();
            //刷新数据
            m_CalcList.Clear();
            m_CalcList = m_pf.SelectedForecastList(type, forecastReport);

            if(m_CalcList != null && m_CalcList.Count != 0)
            {
                _CalcTemp = m_CalcList[0];//保证各个算法的时间一致

            }
            
            bool _IsTrue = false;
            dr = dt.NewRow();
            if (comboBox6.Visible)
            {
                dr["A"] = comboBox6.SelectedItem;
                if (bCompare(dr["A"]))
                {
                    MessageBox.Show("不能用同一种算法！！！");
                    _IsTrue = true;
                }
            }
            else
            {
                dr["A"] = comboBox2.SelectedItem;
                if (bCompare(dr["A"]))
                {
                    MessageBox.Show("不能用同一种算法！！！");
                    _IsTrue = true;
                }
            }
            pcs.ID = Guid.NewGuid().ToString();
            pcs.Forecast = type;
            pcs.ForecastID = forecastReport.ID;
            //算法名

            pcs.CalcID = dr["A"].ToString();
            pcs.Col4 = spinEdit1.Value.ToString() + "%";//权重值存放到表col4字段
           //================================================================
            AddYears(pcs, dr, _CalcTemp);
            //======================================================================================
            //id
            dr["ID"] = pcs.ID;
            //权重值,把这个数据存放在数据库表中的Col4字段中

            dr["D"] = pcs.Col4 ;

            if (!_IsTrue)
            {
                dt.Rows.Add(dr);
            }
            return _IsTrue;
        }
        /// <summary>
        /// 选择历史年份数据和预测年份数据

        /// </summary>
        /// <param name="_CalcTemp">ps_calc数据表对象</param>
        /// <param name="dr">DataRow object</param>
        /// <param name="pcs">ps_Calc  database object</param>
        private void AddYears(Ps_Calc pcs, DataRow dr, Ps_Calc _CalcTemp)
        {
            if (m_CalcList == null || m_CalcList.Count == 0)
            {
                pcs.Value2 = Convert.ToDouble(comboBox3.SelectedItem.ToString().Replace("年", ""));
                pcs.Value3 = Convert.ToDouble(comboBox5.SelectedItem.ToString().Replace("年", ""));
                pcs.Value4 = Convert.ToDouble(comboBox1.SelectedItem.ToString().Replace("年", ""));
                pcs.Value5 = Convert.ToDouble(comboBox4.SelectedItem.ToString().Replace("年", ""));
                //选择历史年份
                dr["B"] = comboBox1.SelectedItem + "-" + comboBox4.SelectedItem;

                //选择预测年份
                dr["C"] = comboBox3.SelectedItem + "-" + comboBox5.SelectedItem;
            }
            else
            {
                if (Convert.ToDouble(comboBox1.SelectedItem.ToString().Replace("年", ""))
                    != _CalcTemp.Value4 || Convert.ToDouble(comboBox4.SelectedItem.ToString().Replace("年", ""))
                    != _CalcTemp.Value5)
                {
                    dr["B"] = _CalcTemp.Value4 + "年-" + _CalcTemp.Value5 + "年";
                    pcs.Value4 = _CalcTemp.Value4;
                    pcs.Value5 = _CalcTemp.Value5;
                }
                else
                {
                    dr["B"] = comboBox1.SelectedItem + "-" + comboBox4.SelectedItem;
                    pcs.Value4 = Convert.ToDouble(comboBox1.SelectedItem.ToString().Replace("年", ""));
                    pcs.Value5 = Convert.ToDouble(comboBox4.SelectedItem.ToString().Replace("年", ""));
                }
                if (Convert.ToDouble(comboBox3.SelectedItem.ToString().Replace("年", ""))
                    != _CalcTemp.Value2 || Convert.ToDouble(comboBox5.SelectedItem.ToString().Replace("年", ""))
                    != _CalcTemp.Value3)
                {
                    dr["C"] = _CalcTemp.Value2 + "年-" + _CalcTemp.Value3 + "年";
                    pcs.Value2 = _CalcTemp.Value2;
                    pcs.Value3 = _CalcTemp.Value3;
                }
                else
                {
                    dr["C"] = comboBox3.SelectedItem + "-" + comboBox5.SelectedItem;
                    pcs.Value2 = Convert.ToDouble(comboBox3.SelectedItem.ToString().Replace("年", ""));
                    pcs.Value3 = Convert.ToDouble(comboBox5.SelectedItem.ToString().Replace("年", ""));
                }
            }
        }
        /// <summary>
        /// 把datatable中的数据加载到datarow中

        /// </summary>
        private void AddToDataRow()
        {
            int yearselect1 = Convert.ToInt32(comboBox3.SelectedItem.ToString().Replace("年", ""));
            int yearselect2 = Convert.ToInt32(comboBox5.SelectedItem.ToString().Replace("年", ""));
            foreach (DataRow ndr in dt.Rows)
            {
                int year1 = Convert.ToInt32(ndr["C"].ToString().Split('-')[0].Replace("年", ""));
                int year2 = Convert.ToInt32(ndr["C"].ToString().Split('-')[1].Replace("年", ""));
                //if (!(yearselect1 > year2 || year1 > yearselect2))
                //{
                //    MessageBox.Show("所选年份包含在" + ndr["C"]);
                //    return;
                //}
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0)
            {
                return;
            }
            Ps_Calc pcs = new Ps_Calc();
            //if (m_pf.DecideDataAddin_comboBox(comboBox2))
            //{
            //    return;
            //}
            //if (m_pf.DecideDataAddin_comboBox(comboBox6))
            //{
            //    return;
            //}
            if (m_pf.DecideDataAddin_comboBox(comboBox1))
            {
                return;
            }
            if (m_pf.DecideDataAddin_comboBox(comboBox4))
            {
                return;
            }
            if (m_pf.DecideDataAddin_comboBox(comboBox3))
            {
                return;
            }

            if (m_pf.DecideDataAddin_comboBox(comboBox5))
            {
                return;
            }
            if (m_pf.DecideDataAddin_SpinEdit(spinEdit1))
            {
                return;
            }
            //刷新数据
            m_CalcList.Clear();
            m_CalcList = m_pf.SelectedForecastList(type, forecastReport);


            AddToDataRow_Update(pcs);
            ////重新刷新grid

            InitGridControl();
        }
        /// <summary>
        /// 修改用数据添加到datarow中

        /// </summary>
        private void AddToDataRow_Update(Ps_Calc _pcs)
        {
            //int yearselect1 = Convert.ToInt32(comboBox3.SelectedItem.ToString().Replace("年", ""));
            //int yearselect2 = Convert.ToInt32(comboBox5.SelectedItem.ToString().Replace("年", ""));
           
            DataRowView _drv = gridView1.GetRow(gridView1.FocusedRowHandle) as DataRowView;
            DataRow _dr = _drv.Row;
            int intex = dt.Rows.IndexOf(_dr);
            //int i = 0;
            //foreach (DataRow ndr in dt.Rows)
            //{

            //    int year1 = Convert.ToInt32(ndr["C"].ToString().Split('-')[0].Replace("年", ""));
            //    int year2 = Convert.ToInt32(ndr["C"].ToString().Split('-')[1].Replace("年", ""));
            //    //if ((!(yearselect1 > year2 || year1 > yearselect2)) && i != intex)
            //    //{
            //    //    MessageBox.Show("所选年份包含在" + ndr["C"]);
            //    //    return;
            //    //}
            //    i++;
            //}
            //if (comboBox6.Visible)
            //{
            //    _dr["A"] = comboBox6.SelectedItem;
            //    //if (bCompare(_dr["A"]))
            //    //{
            //    //    MessageBox.Show("不能用同一种算法！！！");
            //    //    return;
            //    //}
            //}
            //else
            //{
            //    _dr["A"] = comboBox2.SelectedItem;
            //    //if (bCompare(_dr["A"]))
            //    //{
            //    //    MessageBox.Show("不能用同一种算法！！！");
            //    //    return;
            //    //}
            //} 



            _pcs.ID = dt.Rows[intex]["ID"].ToString();
            //权重值

            _pcs.Col4 = spinEdit1.Value.ToString() + "%";
            //_pcs.Forecast = type;
            //_pcs.ForecastID = forecastReport.ID;
            //_pcs.CalcID = _dr["A"].ToString();
            AddYears_Update(_pcs, _dr);

            gridView1.RefreshData();
            ////刷新list列表中数据

            m_CalcList.Clear();
            m_CalcList = m_pf.SelectedForecastList(type, forecastReport);
        }
        /// <summary>
        /// 修改历史年份和预测年份

        /// </summary>
        /// <param name="pcs"></param>
        /// <param name="dr"></param>
        /// <param name="_CalcTemp"></param>
        private void AddYears_Update(Ps_Calc pcs, DataRow dr)
        {
            if (m_CalcList == null || m_CalcList.Count == 0)
            {
                pcs.Value2 = Convert.ToDouble(comboBox3.SelectedItem.ToString().Replace("年", ""));
                pcs.Value3 = Convert.ToDouble(comboBox5.SelectedItem.ToString().Replace("年", ""));
                pcs.Value4 = Convert.ToDouble(comboBox1.SelectedItem.ToString().Replace("年", ""));
                pcs.Value5 = Convert.ToDouble(comboBox4.SelectedItem.ToString().Replace("年", ""));
                //选择历史年份
                dr["B"] = comboBox1.SelectedItem + "-" + comboBox4.SelectedItem;

                //选择预测年份
                dr["C"] = comboBox3.SelectedItem + "-" + comboBox5.SelectedItem;
                SaveValue(pcs, false);
            }
            else
            {
                foreach (Ps_Calc m_Calc in m_CalcList)
                {
                    if(dr["ID"].ToString() == m_Calc.ID)
                    {
                        m_Calc.Col4 = spinEdit1.Value.ToString() + "%";
                    }
                    m_Calc.Value2 = Convert.ToDouble(comboBox3.SelectedItem.ToString().Replace("年", ""));
                    m_Calc.Value3 = Convert.ToDouble(comboBox5.SelectedItem.ToString().Replace("年", ""));
                    m_Calc.Value4 = Convert.ToDouble(comboBox1.SelectedItem.ToString().Replace("年", ""));
                    m_Calc.Value5 = Convert.ToDouble(comboBox4.SelectedItem.ToString().Replace("年", ""));
                    SaveValue(m_Calc, false);
                }
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if(gridView1.FocusedRowHandle < 0)
            {
                return;
            }
            int i = gridView1.FocusedRowHandle;
            DataRowView drv = gridView1.GetRow(i) as DataRowView;
            if (drv == null)
            {
                MessageBox.Show("删除失败！");
                gridView1.RefreshData();
                return;
            }
            Ps_Calc pcs = new Ps_Calc();
            pcs.ID = drv.Row["ID"].ToString();
            pcs = Common.Services.BaseService.GetOneByKey<Ps_Calc>(pcs);
            if (pcs != null)
                Common.Services.BaseService.Delete<Ps_Calc>(pcs);
            else
            {
                MessageBox.Show("删除失败!");
                gridView1.RefreshData();
                return;
            }
            dt.Rows.Remove(drv.Row);

            gridView1.RefreshData();
            if (gridView1.RowCount <= 1)
            {
                Can_Edit_Year = true;
            }
            else
            {
                Can_Edit_Year = false;
            }
            YearSelect();
        }
        /// <summary>
        /// 查询历史年份开始

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nFristYear = 0;//起始年

            int nEndYear = 0;//结束年

            if(comboBox1.SelectedItem == null)
            {
                return;
            }
            if(comboBox1.SelectedItem.ToString()!= "")
            {
                nFristYear = Convert.ToInt32(comboBox1.SelectedItem.ToString().Replace("年", ""));
            }
            if(comboBox3.SelectedItem != null && comboBox3.SelectedItem.ToString() != "")
            {
                nEndYear = Convert.ToInt32(comboBox3.SelectedItem.ToString().Replace("年", ""));
            }
            if(nEndYear != 0)
            {
                if(nFristYear >= nEndYear )
                {
                    comboBox1.SelectedIndex = -1;
                    return;
                }
            }
            nEndYear = 0;
            if(comboBox4.SelectedItem != null && comboBox4.SelectedItem.ToString() != "" )
            {
                nEndYear = Convert.ToInt32(comboBox4.SelectedItem.ToString().Replace("年", ""));
            }
            if(nEndYear != 0)
            {
                if(nFristYear >= nEndYear)
                {
                    comboBox1.SelectedIndex = -1;
                    return;
                }
            }
            nEndYear = 0;
            if(comboBox5.SelectedItem != null && comboBox5.SelectedItem.ToString() != "" )
            {
                nEndYear = Convert.ToInt32(comboBox5.SelectedItem.ToString().Replace("年", ""));
            }
            if (nEndYear != 0)
            {
                if (nFristYear >= nEndYear)
                {
                    comboBox1.SelectedIndex = -1;
                    return;
                }
            }
            if (comboBox1.SelectedItem != null && comboBox4.SelectedItem != null && comboBox4.SelectedItem.ToString() != "")
            {
                Sp_CanEdit = true;
                spinEdit2.Value = (decimal)(Convert.ToInt32(comboBox4.SelectedItem.ToString().Replace("年", "")) - Convert.ToInt32(comboBox1.SelectedItem.ToString().Replace("年", "")));
                Sp_CanEdit = false;
            }
        }
        /// <summary>
        /// 查询历史年份结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nFristYear = 0;
            int nEndYear = 0;
            if(comboBox4.SelectedItem == null)
            {
                return;
            }
            if(comboBox4.SelectedItem.ToString() != "")
            {
                nFristYear = Convert.ToInt32(comboBox4.SelectedItem.ToString().Replace("年", ""));
            }
            if(comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() != "")
            {
                nEndYear = Convert.ToInt32(comboBox1.SelectedItem.ToString().Replace("年", ""));
            }
            else 
            {
                comboBox1.SelectedIndex = comboBox1.Items.IndexOf(forecastReport.StartYear + "年");
            }
            if(nEndYear != 0)
            {
                if(nFristYear <= nEndYear)
                {
                    comboBox4.SelectedIndex = -1;
                    return;
                }
            }
            nEndYear = 0;
            int nTemp = comboBox3.SelectedIndex;
            comboBox3.SelectedIndex = comboBox3.Items.IndexOf((nFristYear + 1) + "年");
            if(nEndYear != 0)
            {
                if(nFristYear >= nEndYear)
                {
                    comboBox4.SelectedIndex = -1;
                    comboBox3.SelectedIndex = nTemp;
                    return;
                }
            }
            nEndYear = 0;
            if (comboBox5.SelectedItem != null && comboBox5.SelectedItem.ToString() != "")
            {
                nEndYear = Convert.ToInt32(comboBox5.SelectedItem.ToString().Replace("年", ""));
            }
            if (nEndYear != 0)
            {
                if (nFristYear >= nEndYear)
                {
                    comboBox4.SelectedIndex = -1;
                    return;
                }
            }
            if (comboBox1.SelectedItem != null && comboBox4.SelectedItem != null && comboBox1.SelectedItem.ToString() != "")
            {
                Sp_CanEdit = true;
                spinEdit2.Value = (decimal)(Convert.ToInt32(comboBox4.SelectedItem.ToString().Replace("年", "")) - Convert.ToInt32(comboBox1.SelectedItem.ToString().Replace("年", "")));
                Sp_CanEdit = false;
            }
        }
        /// <summary>
        /// 选择预测年份开始

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nFristYear = 0;
            int nEndYear = 0;
            int cob3=0;
            int cob5=0;
            if(comboBox3.SelectedItem == null)
            {
                return;
            }
            if (comboBox3.SelectedItem.ToString() != null)
            {
                cob3 = Convert.ToInt32(comboBox3.SelectedItem.ToString().Replace("年", ""));
            }
            if ((cob3+Convert.ToInt32(spinEdit3.Value.ToString()))>forecastReport.EndYear)
            {
                comboBox5.SelectedIndex = comboBox5.Items.IndexOf(forecastReport.EndYear + "年");
            }
            else
            {
                comboBox5.SelectedIndex = comboBox5.Items.IndexOf((cob3+Convert.ToInt32(spinEdit3.Value.ToString()) )+ "年");

            }
            //if(comboBox3.SelectedItem.ToString() != null)
            //{
            //    nFristYear = Convert.ToInt32(comboBox3.SelectedItem.ToString().Replace("年", ""));
            //}

            //if(comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() != "")
            //{
            //    nEndYear = Convert.ToInt32(comboBox1.SelectedItem.ToString().Replace("年", ""));
            //}
            //else 
            //{
            //    comboBox1.SelectedIndex = comboBox1.Items.IndexOf((forecastReport.StartYear + "年"));
            //}
            //if(nEndYear != 0)
            //{
            //    if(nFristYear <= nEndYear)
            //    {
            //        comboBox3.SelectedIndex = -1;
            //        return;
            //    }
            //}

            //nEndYear = 0;
            //int nTemp = comboBox4.SelectedIndex;
            //comboBox4.SelectedIndex = comboBox4.Items.IndexOf((nFristYear - 1) + "年");
            //nEndYear = nFristYear - 1;
            //if(nEndYear != 0)
            //{
            //    if(nFristYear <= nEndYear)
            //    {
            //        comboBox3.SelectedIndex = -1;
            //        comboBox4.SelectedIndex = nTemp;
            //        return;
            //    }
            //}

            //nEndYear = 0;
            //if (comboBox5.SelectedItem != null && comboBox5.SelectedItem.ToString() != "")
            //{
            //    nEndYear = Convert.ToInt32(comboBox5.SelectedItem.ToString().Replace("年", ""));
            //    Sp_CanEdit = true;
            //    spinEdit3.Value = nEndYear - nFristYear;
            //    Sp_CanEdit = false;
            //}
            //else
            //{
            //    nEndYear = nFristYear + Convert.ToInt32(spinEdit3.Value);
            //    if(nEndYear > forecastReport.EndYear - 1)
            //    {
            //        nEndYear = forecastReport.EndYear - 1;
            //    }
            //    comboBox5.SelectedIndex = comboBox4.Items.IndexOf(nEndYear + "年");
            //}
            //if (nEndYear != 0)
            //{
            //    if (nFristYear >= nEndYear)
            //    {
            //        comboBox3.SelectedIndex = -1;
            //        return;
            //    }
            //}
        }
        /// <summary>
        /// 选择预测年份结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nFristYear = 0;
            int nEndYear = 0;
            if(comboBox5.SelectedItem == null)
            {
                return;
            }
            if(comboBox5.SelectedItem.ToString() != null)
            {
                nFristYear = Convert.ToInt32(comboBox5.SelectedItem.ToString().Replace("年", ""));
            }
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() != "")
            {
                nEndYear = Convert.ToInt32(comboBox1.SelectedItem.ToString().Replace("年", ""));
            }
            if(nEndYear != 0)
            {
                if(nFristYear <= nEndYear )
                {
                    comboBox5.SelectedIndex = -1;
                    return;
                }
            }

            nEndYear = 0;
            if (comboBox4.SelectedItem != null && comboBox4.SelectedItem.ToString() != "")
            {
                nEndYear = Convert.ToInt32(comboBox4.SelectedItem.ToString().Replace("年", ""));
            }
            if (nEndYear != 0)
            {
                if (nEndYear >= nFristYear)
                {
                    comboBox5.SelectedIndex = -1;
                    return;
                }
            }

            nEndYear = 0;
            if (comboBox3.SelectedItem != null && comboBox3.SelectedItem.ToString() != "")
            {
                nEndYear = Convert.ToInt32(comboBox3.SelectedItem.ToString().Replace("年", ""));
                Sp_CanEdit = true;
                spinEdit3.Value = (decimal)(nFristYear - nEndYear);
                Sp_CanEdit = false;
            }
            if (nEndYear != 0)
            {
                if (nEndYear >= nFristYear)
                {
                    comboBox5.SelectedIndex = -1;
                    return;
                }
            }
        }
        /// <summary>
        /// gridview行改变焦点

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Refresh_YearData();
        }
        /// <summary>
        /// 设置历史时间，和预测时间的内容

        /// </summary>
        /// <param name="_combo">comboBox object</param>
        /// <param name="_dr">DataRow object</param>
        private void SetcomboBoxContent(ComboBox _combo, DataRow _dr)
        {
            switch(_combo.Name)
            {

                case "comboBox1":
                    _combo.SelectedIndex = _combo.Items.IndexOf(_dr["B"].ToString().Split('-')[0]);
                    break;
                case "comboBox4":
                    _combo.SelectedIndex = _combo.Items.IndexOf(_dr["B"].ToString().Split('-')[1]);
                    break;
                case "comboBox3":
                    _combo.SelectedIndex = _combo.Items.IndexOf(_dr["C"].ToString().Split('-')[0]);
                    break;
                case "comboBox5":
                    _combo.SelectedIndex = _combo.Items.IndexOf(_dr["C"].ToString().Split('-')[1]);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            DataTable dtTemp = GetSortTable(dt, "C", true);
            if (dtTemp.Rows.Count<=1)
            {
                MessageBox.Show("复合算法至少应选取两种算法!");
                return;
            }
            int i = 0;
            double WeightedValue = 0;
            ha.Clear();
            if(IsWeightedValue( dtTemp, WeightedValue))
            {
                for(; i < dtTemp.Rows.Count; ++i)
                {
                    ha.Add(i, dtTemp.Rows[i]["A"] + "@" + dtTemp.Rows[i]["B"].ToString().Replace("年", "") 
                        + "@" + dtTemp.Rows[i]["C"].ToString().Replace("年", "") + "@"+
                        dtTemp.Rows[i]["D"].ToString().Replace("%",""));
                }
            }
            else
            {
                MessageBox.Show("权重值的累加和不为100请重新修改！！！");
                return;
            }
            //if (dttemp.Rows.Count > 0)
            //    ha.Add(i, dttemp.Rows[i]["A"] + "@" + dttemp.Rows[i]["B"].ToString().Replace("年", "") + "@" + dttemp.Rows[i]["C"].ToString().Replace("年", ""));
            this.DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// 判断权重值是否为100%
        /// </summary>
        /// <returns></returns>
        /// <param name="dtTemp">dataTable object</param>
        /// <param name="WeightedValue">保存权重值的数组</param>
        private bool IsWeightedValue(DataTable dtTemp, double WeightedValue)
        {
            WeightedValue = 0;
            for (int i = 0; i < dtTemp.Rows.Count ; i++)
            {
                //判断权重值是否为100%
                WeightedValue += Convert.ToDouble(dtTemp.Rows[i]["D"].ToString().Split('%')[0]);
            }
            if(WeightedValue == 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 给dataview排序,得到历史时间和预测时间区间的值

        /// </summary>
        /// <param name="dt">datatable object</param>
        /// <param name="Column">列标题</param>
        /// <param name="bl">升序排列(true)，降序排列(false)</param>
        /// <returns>datatable object</returns>
        private System.Data.DataTable GetSortTable(System.Data.DataTable dt, string Column, bool bl)
        {
            string sort = " asc";
            DataTable dtTemp = new DataTable();
            if (!bl)
                sort = " desc";
            //datatable保存数据提取出截取的年份
            DataView dv = dt.DefaultView;
            dv.Sort = Column + sort;
            System.Data.DataTable dt2 = dv.ToTable();
            return dt2;
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            Refresh_YearData();
        }
        private void YearSelect()
        {
            if (Can_Edit_Year)
            {
                comboBox1.Enabled = true;
                comboBox4.Enabled = true;
                comboBox5.Enabled = true;
                spinEdit2.Enabled = true;
                spinEdit3.Enabled = true;
            }
            else
            {
                comboBox1.Enabled = false;
                comboBox4.Enabled = false;
                comboBox5.Enabled = false;
                spinEdit2.Enabled = false;
                spinEdit3.Enabled = false;
            }
        }
        private void Refresh_YearData()
        {
            //只有一个方法或者没有方法时允许修改年份
            if (gridView1.RowCount <= 1)
            {
                Can_Edit_Year = true;
            }
            else
            {
                Can_Edit_Year = false;
            }
            string strTemp = "";
            if (gridView1.FocusedRowHandle < 0)
            {
                return;
            }
            comboBox1.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            DataRowView _drv = gridView1.GetRow(gridView1.FocusedRowHandle) as DataRowView;
            if (_drv == null)
            {
                return;
            }
            DataRow dr = _drv.Row;
            string _strname = dr["A"].ToString();
            if (_strname.Contains("外推法"))
            {
                comboBox6.Items.Clear();
                m_pf.InitCommboBox(comboBox6, null, 1);
                SetSubalgorithm(true);
                comboBox6.SelectedIndex = comboBox6.Items.IndexOf(_strname);
                comboBox2.SelectedIndex = comboBox2.Items.IndexOf("外推法");
            }
            else if (_strname.Contains("相关法"))
            {
                comboBox6.Items.Clear();
                m_pf.InitCommboBox(comboBox6, null, 2);
                SetSubalgorithm(true);
                comboBox6.SelectedIndex = comboBox6.Items.IndexOf(_strname);
                comboBox2.SelectedIndex = comboBox2.Items.IndexOf("相关法");
            }
            else
            {
                SetSubalgorithm(false);
                comboBox6.SelectedIndex = -1;
                comboBox2.SelectedIndex = comboBox2.Items.IndexOf(_strname);
            }
            //刷新历史年份和预测年份

            SetcomboBoxContent(comboBox1, dr);
            SetcomboBoxContent(comboBox4, dr);
            SetcomboBoxContent(comboBox3, dr);
            SetcomboBoxContent(comboBox5, dr);
            old_row = dr;
            YearSelect();
            //spinEdit1.Value = Convert.ToInt32(dr["D"].ToString().Split('%')[0]);
            spinEdit1.Value = Convert.ToDecimal(dr["D"].ToString().Split('%')[0]);
        }
        private void gridView1_GotFocus(object sender, EventArgs e)
        {
            Refresh_YearData();
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void spinEdit3_ValueChanged(object sender, EventArgs e)
        {
            if (Sp_CanEdit)
            {
                return;
            }
            else
            {
                Sp_CanEdit = true;
                if ((Convert.ToInt32(comboBox3.SelectedItem.ToString().Replace("年", "")) + Convert.ToInt32(spinEdit3.Value.ToString())) > forecastReport.EndYear)
                {
                    MessageBox.Show("值超出允许范围！");
                    spinEdit3.Value = (Convert.ToInt32(comboBox5.SelectedItem.ToString().Replace("年", "")) - Convert.ToInt32(comboBox3.SelectedItem.ToString().Replace("年", "")));
                }
                else
                {
                    int temp=(Convert.ToInt32(comboBox3.SelectedItem.ToString().Replace("年", "")) + Convert.ToInt32(spinEdit3.Value.ToString()));
                    comboBox5.SelectedIndex = comboBox5.Items.IndexOf(temp + "年");
                    
                }
                Sp_CanEdit = false;
            }
        }

        private void spinEdit2_ValueChanged(object sender, EventArgs e)
        {
            if (Sp_CanEdit)
            {
                return;
            }
            else
            {
                Sp_CanEdit = true;
                if ((Convert.ToInt32(comboBox1.SelectedItem.ToString().Replace("年", "")) + Convert.ToInt32(spinEdit2.Value.ToString())) > (forecastReport.EndYear - 2))
                {
                    MessageBox.Show("值超出允许范围！");
                    spinEdit2.Value = Convert.ToInt32(comboBox4.SelectedItem.ToString().Replace("年", "")) - Convert.ToInt32(comboBox1.SelectedItem.ToString().Replace("年", ""));
                }
                else
                {
                    int temp = (Convert.ToInt32(comboBox1.SelectedItem.ToString().Replace("年", "")) + Convert.ToInt32(spinEdit2.Value.ToString()));
                    comboBox4.SelectedIndex = comboBox4.Items.IndexOf(temp + "年");
                }
                Sp_CanEdit = false;
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {

            FormForecastCalcTX fc = new FormForecastCalcTX();
            fc.DTable = dataTable;
            fc.ISEdit = true;
            if (!HaveSelectYear())
            {
                MessageBox.Show("请选设置好年份再选择参数！");
            }
            fc.firstyear = Convert.ToInt32(comboBox1.SelectedItem.ToString().Replace("年", ""));
            fc.endyear = Convert.ToInt32(comboBox4.SelectedItem.ToString().Replace("年", ""));
            fc.PForecastReports = forecastReport;
            if (fc.ShowDialog() != DialogResult.OK)
                return;
        }
        private bool HaveSelectYear()
        {
            if (comboBox1.SelectedItem==null||comboBox3.SelectedItem==null||comboBox4.SelectedItem==null||comboBox5.SelectedItem==null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void spinEdit4_ValueChanged(object sender, EventArgs e)
        {
            Ps_Calc pcs = new Ps_Calc();
            pcs.Forecast = 25;
            pcs.ForecastID = forecastReport.ID;
            IList<Ps_Calc> list1 = m_pf.SelectedForecastList(25, forecastReport);
            if (list1.Count > 0)
            {
                list1[0].Value1 =Convert.ToDouble(spinEdit4.Value);
                Common.Services.BaseService.Update<Ps_Calc>(list1[0]);
            }
            else
            {
                Ps_Calc pc = new Ps_Calc();
                pc.ID = Guid.NewGuid().ToString();
                pc.Forecast = 25;
                pc.ForecastID = forecastReport.ID;
                pc.Col4 = "25";
                pc.Value1 = Convert.ToDouble(spinEdit4.Value);
                Common.Services.BaseService.Create<Ps_Calc>(pc);
            }
        }
    }
}