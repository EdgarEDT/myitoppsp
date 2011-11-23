using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Data;
using Dundas.Charting.WinControl;
using Itop.Domain.Forecast;
using Itop.Client.Common;
using DevExpress.XtraEditors;
namespace Itop.Client.Forecast
{
    //这个类用来提供公共调用方法
    class PublicFunction
    {
        private const int _nComboBoxCount = 6;//方案的个数
        //private const int _nComboBoxCount = 7;//方案的个数
        private const int _nComboBoxCount_Subalgorithm = 6;//外推法的子算法
        private const int _nComboBoxCount_Interfix = 5;//相关法的子算法
        public string[] _StrInterfix;//存放相关法子算法的数组
        public string[] _StrScenario ;//= new string[_nComboBoxCount];//存放方案值数组
        public string[] _StrSubalgorithm;// = new string[_nComboBoxCount_Subalgorithm];//子算法数组
        /// <summary>
        /// 重新设置是否显示chart控件的多条曲线
        /// </summary>
        /// <param name="_ChartObject">chart对象</param>
        /// <param name="_CheckBoxObject">checkBox对象</param>
        /// <param name="hs">存放颜色数组</param>
        /// <param name="al">存放图片的数组</param>
        public void SetChart(Dundas.Charting.WinControl.Chart _ChartObject,System.Windows.Forms.CheckBox _CheckBoxObject,ArrayList hs,ArrayList al)
        {
            LegendItem legendItem;
            LegendCell legendCell1;
            LegendCell legendCell2;
            LegendCell legendCell3;
            Legend legend = new Legend();
            legend.AutoFitText = false;
            if(al == null)
            {
                al = new ArrayList();
                AddIco(al);
            }
            if (_ChartObject != null && _CheckBoxObject != null)
            {
                for(int i = 0; i < _ChartObject.Series.Count; ++i)
                {
                    legendItem = new Dundas.Charting.WinControl.LegendItem();
                    legendCell1 = new Dundas.Charting.WinControl.LegendCell();
                    legendCell1.CellType = Dundas.Charting.WinControl.LegendCellType.Image;
                    legendCell1.Name = "Cell1";

                    legendCell2 = new Dundas.Charting.WinControl.LegendCell();
                    legendCell3 = new Dundas.Charting.WinControl.LegendCell();
                    legendCell3.CellType = Dundas.Charting.WinControl.LegendCellType.Image;
                    legendCell3.Name = "Cell3";
                    legendCell2.Name = "Cell2";
                    legendCell2.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
                    legendCell2.Text = _ChartObject.Series[i].Name;
                    if(hs != null)
                    {
                        legendCell2.TextColor = (Color)hs[i];
                        _ChartObject.Series[i].Color = (Color)hs[i];
                    }

                    _ChartObject.Series[i].Name = (i + 1).ToString() + "." + _ChartObject.Series[i].Name;
                    _ChartObject.Series[i].Type = Dundas.Charting.WinControl.SeriesChartType.Line;
                    _ChartObject.Series[i].MarkerImage = al[i % 7].ToString();
                    _ChartObject.Series[i].MarkerSize = 7;
                    _ChartObject.Series[i].MarkerStyle = (Dundas.Charting.WinControl.MarkerStyle)(2);

                    _ChartObject.Series[i].ShowInLegend = false;

                    legendItem.Cells.Add(legendCell1);
                    legendItem.Cells.Add(legendCell2);
                    legendItem.Tag = _ChartObject.Series[i];

                    if (_CheckBoxObject.Checked)
                    {
                        legendItem.Cells[0].Image = string.Format(Application.StartupPath + @"/img/chk_checked.png");

                    }
                    else
                    {
                        legendItem.Cells[0].Image = string.Format(Application.StartupPath + @"/img/chk_unchecked.png");
                    }
                    legend.CustomItems.Add(legendItem);
                    _ChartObject.Series[i].Enabled = _CheckBoxObject.Checked;
                }

                _ChartObject.ChartAreas["Default"].AxisX.MinorGrid.Enabled = false;
                _ChartObject.ChartAreas["Default"].AxisY.MinorGrid.Enabled = true;
                _ChartObject.ChartAreas["Default"].AxisY.MinorGrid.LineStyle = ChartDashStyle.Dash;
                _ChartObject.ChartAreas["Default"].AxisY.MinorGrid.LineColor = Color.Gray;
                legend.Name = "Default";
                _ChartObject.Legends.Add(legend);
            }
        }
        /// <summary>
        /// 添加ico图片
        /// </summary>
        /// <param name="al">一个ArrayList对象</param>
        public void AddIco(ArrayList al)
        {
            al.Add(Application.StartupPath + "/img/1.ico");
            al.Add(Application.StartupPath + "/img/2.ico");
            al.Add(Application.StartupPath + "/img/3.ico");
            al.Add(Application.StartupPath + "/img/4.ico");
            al.Add(Application.StartupPath + "/img/5.ico");
            al.Add(Application.StartupPath + "/img/6.ico");
            al.Add(Application.StartupPath + "/img/7.ico");
        }
        /// <summary>
        /// 用来添加datatable的列与dev的gridconctrl列相对应
        /// </summary>
        /// <param name="dt"></param>
        public void DataTableAddColumn(DataTable dt)
        {
            if(dt ==null)
            {
                dt = new DataTable();
            }
            dt.Clear();
            dt.Columns.Add("A", typeof(string));
            dt.Columns.Add("B", typeof(string));
            dt.Columns.Add("C", typeof(string));
            dt.Columns.Add("D", typeof(string));
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("CalcID", typeof(string));
        }
        /// <summary>
        /// 查找算法模型
        /// </summary>
        /// <param name="forecastReport">Ps_forecast_list对象</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public IList<Ps_Calc> SelectedForecastList(int type, Ps_forecast_list forecastReport)
        {
            IList<Ps_Calc> _CalcList = new List<Ps_Calc>();
            //string sql = " Forecast = '" +forecastReport.ID+"' and Forecast = '"+ type +
            //        "' and Col4 != ''"; 
                   
            if(type != null && forecastReport != null)
            {
                Ps_Calc pcs = new Ps_Calc();
                pcs.Forecast = type;
                pcs.ForecastID = forecastReport.ID;

                _CalcList = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcOfCol4", pcs);
                return _CalcList;
            }
            else
            {
                return _CalcList;
            }
        }
        /// <summary>
        /// 初始化commboBox
        /// </summary>
        /// <param name="_comboBox"></param>
        /// <param name="forecastReport"></param>
        /// <param name="nOwn">判断算法是否为相关法 =2还是外推法=1,0代表父算法</param>
        public void InitCommboBox(System.Windows.Forms.ComboBox _comboBox, Ps_forecast_list forecastReport,int nOwn)
        {
            if (_comboBox.Name == "comboBox2")
            {
                if(nOwn == 0)
                {
                    InitScenario();
                    for (int i = 0; i < _nComboBoxCount; ++i)
                    {
                        _comboBox.Items.Add(_StrScenario[i]);
                    }
                }
            }
            else if(_comboBox.Name == "comboBox6")
            {
                if(nOwn == 1)
                {
                    InitSubalgorithm();
                    for (int i = 0; i < _nComboBoxCount_Subalgorithm; ++i)
                    {
                        _comboBox.Items.Add(_StrSubalgorithm[i]);
                    }
                }
                else if(nOwn == 2)
                {
                    InitInterfix();
                    for(int i = 0; i < _nComboBoxCount_Interfix; ++i)
                    {
                        _comboBox.Items.Add(_StrInterfix[i]);
                    }
                }
            }
            else
            {
                if(forecastReport!=null)
                {
                    for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
                    {
                        _comboBox.Items.Add(i + "年");
                    }
                }
            }
        }
        /// <summary>
        /// 初始化添加外推法子类算法数组
        /// </summary>
        public void InitSubalgorithm()
        {
            _StrSubalgorithm = new string[_nComboBoxCount_Subalgorithm] { 
                "外推法(年增长率法)", "外推法(直线)" ,"外推法(抛物线)","外推法(三阶)",
                "外推法(指数)","外推法(几何曲线)"};
        }
        /// <summary>
        /// 初始化父类算法数组
        /// </summary>
        public void InitScenario()
        {
            //隐藏相关法
            //_StrScenario = new string[_nComboBoxCount] { "年增长率法",
            //    "弹性系数法","相关法","灰色模型法","外推法","指数平滑法","专家决策法"};
            _StrScenario = new string[_nComboBoxCount] { "年增长率法", "弹性系数法", "灰色模型法", "外推法", "指数平滑法", "专家决策法" };
        }
        /// <summary>
        /// 初始化相关法子算法的数组
        /// </summary>
        public void InitInterfix()
        {
            _StrInterfix = new string[_nComboBoxCount_Interfix] { "相关法(直线)",
                "相关法(抛物线)","相关法(三阶)","相关法(指数)","相关法(几何曲线)"};
        }
        /// <summary>
        /// 判断数据是否添加到comboBox中
        /// </summary>
        /// <param name="_comboBox"></param>
        public bool DecideDataAddin_comboBox(System.Windows.Forms.ComboBox _comboBox)
        {
            bool _IsTrue = false;
            if(_comboBox.Name != "comboBox6")
            {
                if(_comboBox.SelectedItem == null)
                {
                    switch (_comboBox.Name)
                    {
                        case "comboBox2":
                            MessageBox.Show("“选择项目名称：”的参数设置不正确！！！");
                            break;
                        case "comboBox1":
                            MessageBox.Show("“选择历史年份：”的参数设置不正确！！！");
                            break;
                        case "comboBox4":
                            MessageBox.Show("“选择历史年份：”的参数设置不正确！！！");
                            break;
                        case "comboBox3":
                            MessageBox.Show("“选择预测年份：”的参数设置不正确！！！");
                            break;
                        case "comboBox5":
                            MessageBox.Show("“选择预测年份：”的参数设置不正确！！！");
                            break;
                        default:
                            break;
                    }
                    _IsTrue = true;
                }
                
            }
            else if(_comboBox.Name == "comboBox6")
            {
                if(_comboBox.SelectedItem == null && _comboBox.Visible)
                {
                    MessageBox.Show("“选择子算法名称：”的参数设置不正确！！！");
                    _IsTrue = true;
                }
            }
            return _IsTrue;
        }
        public bool DecideDataAddin_SpinEdit(SpinEdit _SpinEdit)
        {
            bool _IsTrue = false;
            if(_SpinEdit.Value == 0)
            {
                MessageBox.Show("“权重值：”的参数设置不正确！！！");
                _IsTrue = true;
            }
            return _IsTrue;
        }
        /// <summary>
        /// 对comboBox2，和comboBox6可编辑状态改变
        /// </summary>
        /// <param name="_IsTrue"></param>
        public void InitEnable(System.Windows.Forms.ComboBox _combo, bool _IsTrue)
        {
            _combo.Enabled = _IsTrue;
        }
    }
}
