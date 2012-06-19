using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Dundas.Charting.WinControl;
namespace Itop.Client.Using
{
    public partial class chart_userSH : UserControl
    {
        /// <summary>
        /// 外部接口RefreshChart(IList chartlistvalue, string str1, string str2, string str3, ArrayList chartcolor)
        /// chartlistvalue为数据源
        /// str1为参数1
        /// str2为参数2
        /// str3为参数3
        /// chartcolor为图表颜色数组
        /// 
        /// chart1为公有类型可以直接调用，方便导出图形
        /// </summary>
        public delegate void setcolor();
        public event  setcolor SetColor;
        public chart_userSH()
        {
            InitializeComponent();
            add_color();
            add_image();
        }
        class chart_check
        {
            public string keyname = string.Empty;
            public bool keyvalue = false;
        }
        //默认背景色
        Color chartbackcolor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
        List<chart_check> chartchecklist = new List<chart_check>();
        public void  All_Select(bool allselet)
        {
            if (allselet)
            {
                isfirst = "yes";
            }
        }
        //是否第一次载入
        string isfirst = "yes";
        //默认图形样式为线形
        string charttypestring = "point";
        /// <summary>
        /// 初始化背景颜色列表
        /// </summary>
        private void add_color()
        {
            this.comboBoxEdit1.Properties.Items.Clear();
            this.comboBoxEdit1.Properties.Items.Add("White");
            this.comboBoxEdit1.Properties.Items.Add("blue");
            this.comboBoxEdit1.Properties.Items.Add("Yellow");
            this.comboBoxEdit1.Properties.Items.Add("Green");
            this.comboBoxEdit1.Properties.Items.Add("aliceblue");
            this.comboBoxEdit1.Properties.Items.Add("burlywood");
            this.comboBoxEdit1.Properties.Items.Add("cornsilk");
            this.comboBoxEdit1.Properties.Items.Add("lavender");
            this.comboBoxEdit1.Properties.Items.Add("floralwhite");
            this.comboBoxEdit1.Properties.Items.Add("lightpink");
            this.comboBoxEdit1.Properties.Items.Add("navajowhite");
            this.comboBoxEdit1.Properties.Items.Add("plum");
            this.comboBoxEdit1.Properties.Items.Add("skyblue");
            this.comboBoxEdit1.Properties.Items.Add("snow");
            this.comboBoxEdit1.Properties.Items.Add("teal");
            this.comboBoxEdit1.Properties.Items.Add("wheat");
            this.comboBoxEdit1.Properties.Items.Add("paleturquoise");
        }
        ArrayList al = new ArrayList();
        private void add_image()
        {
            
            al.Add(Application.StartupPath + "/img/1.ico");
            al.Add(Application.StartupPath + "/img/2.ico");
            al.Add(Application.StartupPath + "/img/3.ico");
            al.Add(Application.StartupPath + "/img/4.ico");
            al.Add(Application.StartupPath + "/img/5.ico");
            al.Add(Application.StartupPath + "/img/6.ico");
            al.Add(Application.StartupPath + "/img/7.ico");
        }
        
        private IList c_chartlistvalue = null;
        private string c_str1 = string.Empty;
        private string c_str2 = string.Empty;
        private string c_str3 = string.Empty;
        private ArrayList c_chartcolor = new ArrayList();
        public void RefreshChart(IList chartlistvalue, string str1, string str2, string str3, ArrayList chartcolor)
        {
            c_chartlistvalue = chartlistvalue;
            c_str1 = str1;
            c_str2 = str2;
            c_str3 = str3;
            c_chartcolor = chartcolor;
            RefreshChart();
        }
        public void RefreshChart()
        {
            if (charttypestring == "yt" && checkE_3d.Checked)
            {
                checkE_3d.Checked = false;
                MessageBox.Show("样条范围不能显示3d效果！");
            }

            chart1.Series.Clear();
            LegendItem legendItem;
            LegendCell legendCell1;
            LegendCell legendCell2;
            LegendCell legendCell3;
            Legend legend = new Legend();
            legend.AutoFitText = false;

            chart1.DataBindCrossTab(c_chartlistvalue, c_str1, c_str2, c_str3, "");

            chart1.ChartAreas["Default"].BackColor = chartbackcolor;

            chart1.ChartAreas["Default"].AxisY.MinorGrid.LineStyle = ChartDashStyle.Dash;
            chart1.ChartAreas["Default"].AxisY.MinorGrid.LineColor = Color.Gray;

            chart1.ChartAreas["Default"].AxisX.MinorGrid.Enabled = false;
            chart1.ChartAreas["Default"].AxisY.MinorGrid.Enabled = true;

            //对应类别的颜色
            //ArrayList chartcolor = chartbasecolor();
            for (int i = 0; i < chart1.Series.Count; i++)
            {

                legendItem = new Dundas.Charting.WinControl.LegendItem();
                legendCell1 = new Dundas.Charting.WinControl.LegendCell();
                legendCell1.CellType = Dundas.Charting.WinControl.LegendCellType.Image;
                legendCell1.Name = "Cell1";

                legendCell2 = new Dundas.Charting.WinControl.LegendCell();
                legendCell3 = new Dundas.Charting.WinControl.LegendCell();
                legendCell2.CellType = Dundas.Charting.WinControl.LegendCellType.Image;
                legendCell2.Name = "Cell2";
                legendCell3.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
                legendCell3.Name = "Cell3";
                legendCell3.Text = (i + 1) + "." + chart1.Series[i].Name;
               
              
                legendCell3.TextColor = (Color)c_chartcolor[i];

                chart1.Series[i].Color = (Color)c_chartcolor[i];
                chart1.Series[i].Name = (i + 1).ToString() + "." + chart1.Series[i].Name;
               
                chart1.Series[i].Type = charttype(i);
                chart1.Series[i].ShowInLegend = false;
                legendItem.Cells.Add(legendCell1);
                legendItem.Cells.Add(legendCell3);
                legendItem.Tag = chart1.Series[i];
                //使用全选或是第一次载入
                if (checkbox1chagned || isfirst == "yes")
                {
                    if (checkEall.Checked)
                    {
                        legendItem.Cells[0].Image = string.Format(Application.StartupPath + @"/img/chk_checked.png");

                    }

                    else
                    {
                        legendItem.Cells[0].Image = string.Format(Application.StartupPath + @"/img/chk_unchecked.png");

                    }
                    string tempstr = legendItem.Cells[1].Text;
                    tempstr = tempstr.Substring(tempstr.IndexOf(".") + 1, tempstr.Length - tempstr.IndexOf(".") - 1);
                    chartcheck_value(tempstr, checkEall.Checked);

                }
                else
                {
                    string tempstr = legendItem.Cells[1].Text;
                    tempstr = tempstr.Substring(tempstr.IndexOf(".") + 1, tempstr.Length - tempstr.IndexOf(".") - 1);

                    if (chartcheck_has(tempstr))
                    {
                        legendItem.Cells[0].Image = string.Format(Application.StartupPath + @"/img/chk_checked.png");

                    }
                    else
                    {
                        legendItem.Cells[0].Image = string.Format(Application.StartupPath + @"/img/chk_unchecked.png");
                    }
                }

                legend.CustomItems.Add(legendItem);

                string tempstr1 = legendItem.Cells[1].Text;
                tempstr1 = tempstr1.Substring(tempstr1.IndexOf(".") + 1, tempstr1.Length - tempstr1.IndexOf(".") - 1);
                chart1.Series[i].Enabled = chartcheck_has(tempstr1);
            }
            legend.Name = "Default";

            this.chart1.Legends.Add(legend);
           
            isfirst = "no";

        }
        /// <summary>
        /// 查看值是否在已选列表中
        /// </summary>
        /// <param name="tempvalue"></param>
        /// <returns></returns>
        private bool chartcheck_has(string tempvalue)
        {
            bool has = false;
            for (int i = 0; i < chartchecklist.Count; i++)
            {
                if (chartchecklist[i].keyname == tempvalue)
                {
                    has = chartchecklist[i].keyvalue;
                    break;
                }
            }
            return has;
        }
        /// <summary>
        /// 改变选中列表
        /// </summary>
        /// <param name="tempvalue"></param>
        /// <param name="tempbool"></param>
        private void chartcheck_value(string tempvalue, bool tempbool)
        {
            bool has = false;
            for (int i = 0; i < chartchecklist.Count; i++)
            {
                if (chartchecklist[i].keyname == tempvalue)
                {
                    chartchecklist[i].keyvalue = tempbool;
                    has = true;
                    break;
                }
            }
            if (!has)
            {
                chart_check tempcc = new chart_check();
                tempcc.keyname = tempvalue;
                tempcc.keyvalue = tempbool;
                chartchecklist.Add(tempcc);
            }
        }
        //改变背景颜色
        private void comboBoxEdit1_SelectedValueChanged(object sender, EventArgs e)
        {     
            chartbackcolor= Color.FromName(this.comboBoxEdit1.SelectedText.ToString());
             chart1.ChartAreas["Default"].BackColor = chartbackcolor;
        }

      
        //改变图形样式
        bool checkhave = false;
        private void Change_chartType(string  stype)
        {
            checkhave = true;
            switch (stype)
            {
                case "no": charttypestring = "point";
                    checkE_point.Checked = true;
                    break;
                case "point": 
                    checkE_point.Checked = true;
                    checkE_line.Checked=false;
                    checkE_stepline.Checked=false;
                    checkE_zf.Checked=false;
                    checkE_zx.Checked=false;
                    checkE_dj.Checked=false;
                    checkE_yt.Checked=false;
                    charttypestring = "point";
                    break;
                case "line":
                    checkE_point.Checked = false;
                    checkE_line.Checked = true;
                    checkE_stepline.Checked = false;
                    checkE_zf.Checked = false;
                    checkE_zx.Checked = false;
                    checkE_dj.Checked = false;
                    checkE_yt.Checked = false;
                    charttypestring = "line";
                    break;
                case "stepline":
                    checkE_point.Checked = false;
                    checkE_line.Checked = false;
                    checkE_stepline.Checked = true;
                    checkE_zf.Checked = false;
                    checkE_zx.Checked = false;
                    checkE_dj.Checked = false;
                    checkE_yt.Checked = false;
                    charttypestring = "stepline";
                    break;
                case "zf":
                    checkE_point.Checked = false;
                    checkE_line.Checked = false;
                    checkE_stepline.Checked = false;
                    checkE_zf.Checked = true;
                    checkE_zx.Checked = false;
                    checkE_dj.Checked = false;
                    checkE_yt.Checked = false;
                    charttypestring = "zf";
                    break;
                case "zx":
                    checkE_point.Checked = false;
                    checkE_line.Checked = false;
                    checkE_stepline.Checked = false;
                    checkE_zf.Checked = false;
                    checkE_zx.Checked = true ;
                    checkE_dj.Checked = false;
                    checkE_yt.Checked = false;
                    charttypestring = "zx";
                    break;
                case "dj":
                    checkE_point.Checked = false;
                    checkE_line.Checked = false;
                    checkE_stepline.Checked = false;
                    checkE_zf.Checked = false;
                    checkE_zx.Checked = false;
                    checkE_dj.Checked = true;
                    checkE_yt.Checked = false;
                    charttypestring = "dj";
                    break;
                case "yt":
                    checkE_point.Checked = false;
                    checkE_line.Checked = false;
                    checkE_stepline.Checked = false;
                    checkE_zf.Checked = false;
                    checkE_zx.Checked = false;
                    checkE_dj.Checked = false;
                    checkE_yt.Checked = true;
                    charttypestring = "yt";
                    break;
                default:
                    break;
            }
            checkhave = false;
            RefreshChart();
        }

        private void checkE_point_CheckedChanged(object sender, EventArgs e)
        {
            if (checkhave)
            {
                return;
            }
            if (checkE_point.Checked)
            {
                Change_chartType("point");
            }
            else
            {
                Change_chartType("no");
            }
           
        }

        private void checkE_line_CheckedChanged(object sender, EventArgs e)
        {
            if (checkhave)
            {
                return;
            }
            if (checkE_line.Checked)
            {
                Change_chartType("line");
            }
            else
            {
                Change_chartType("no");
            }
        }

        private void checkE_stepline_CheckedChanged(object sender, EventArgs e)
        {
            if (checkhave)
            {
                return;
            }
            if (checkE_stepline.Checked)
            {
                Change_chartType("stepline");
            }
            else
            {
                Change_chartType("no");
            }
        }

        private void checkE_zf_CheckedChanged(object sender, EventArgs e)
        {
            if (checkhave)
            {
                return;
            }
            if (checkE_zf.Checked)
            {
                Change_chartType("zf");
            }
            else
            {
                Change_chartType("no");
            }
        }

        private void checkE_zx_CheckedChanged(object sender, EventArgs e)
        {
            if (checkhave)
            {
                return;
            }
            if (checkE_zx.Checked)
            {
                Change_chartType("zx");
            }
            else
            {
                Change_chartType("no");
            }
        }

        private void checkE_dj_CheckedChanged(object sender, EventArgs e)
        {
            if (checkhave)
            {
                return;
            }
            if (checkE_dj.Checked)
            {
                Change_chartType("dj");
            }
            else
            {
                Change_chartType("no");
            }
        }

        private void checkE_yt_CheckedChanged(object sender, EventArgs e)
        {
            if (checkhave)
            {
                return;
            }
            if (checkE_yt.Checked)
            {
                Change_chartType("yt");
            }
            else
            {
                Change_chartType("no");
            }
        }
        /// <summary>
        /// 调置图表的样式
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private SeriesChartType charttype(int i)
        {
            SeriesChartType temptype = SeriesChartType.Line;
            if (charttypestring == "point")
            {
                temptype = SeriesChartType.Line;
                chart1.ChartAreas[0].Area3DStyle.Enable3D = checkE_3d.Checked;
                chart1.ChartAreas[0].Area3DStyle.Clustered = checkE_3d.Checked;
                chart1.Series[i].MarkerImage = al[i % 7].ToString();
                chart1.Series[i].MarkerSize = 4;
                chart1.Series[i].MarkerStyle = (Dundas.Charting.WinControl.MarkerStyle)(2);

            }
            if (charttypestring == "line")
            {
                temptype = SeriesChartType.Line;

                chart1.ChartAreas[0].Area3DStyle.Enable3D = checkE_3d.Checked;
           
                chart1.ChartAreas[0].Area3DStyle.Clustered = checkE_3d.Checked;
                //chart1.ChartAreas[0].Area3DStyle.PointDepth = 20;
                chart1.Series[i].BorderWidth = 2;
                if (checkE_3d.Checked)
                {
                    chart1.Series[i].MarkerSize = 0;
                    chart1.Series[i].MarkerStyle = (Dundas.Charting.WinControl.MarkerStyle)(0);
                }
                else
                {
                    chart1.Series[i].MarkerSize = 4;
                    chart1.Series[i].MarkerStyle = (Dundas.Charting.WinControl.MarkerStyle)(2);
                }


            }
            if (charttypestring == "zf")
            {
                temptype = SeriesChartType.Column;
                chart1.ChartAreas[0].Area3DStyle.Enable3D = checkE_3d.Checked;
                chart1.ChartAreas[0].Area3DStyle.Clustered = checkE_3d.Checked;
                //chart1.ChartAreas[0].Area3DStyle.PointDepth = 20;
                //chart1.Series[i].MarkerSize = 7;
                chart1.Series[i].MarkerStyle = (Dundas.Charting.WinControl.MarkerStyle)(0);

            }
            if (charttypestring == "zx")
            {
                temptype = SeriesChartType.Column;
                chart1.ChartAreas[0].Area3DStyle.Enable3D = checkE_3d.Checked;
                chart1.ChartAreas[0].Area3DStyle.Clustered = checkE_3d.Checked;
                //chart1.ChartAreas[0].Area3DStyle.PointDepth = 20;

                chart1.ChartAreas["Default"].Area3DStyle.PointDepth = 100;
                chart1.ChartAreas["Default"].Area3DStyle.PointGapDepth = 100;
                chart1.Series[i]["DrawingStyle"] = "Cylinder";
                chart1.Series[i].MarkerSize = 7;
                chart1.Series[i].MarkerStyle = (Dundas.Charting.WinControl.MarkerStyle)(0);

            }
            if (charttypestring == "stepline")
            {
                temptype = SeriesChartType.StepLine;

                chart1.ChartAreas[0].Area3DStyle.Enable3D = checkE_3d.Checked;
                chart1.ChartAreas[0].Area3DStyle.Clustered = checkE_3d.Checked;
                //chart1.ChartAreas[0].Area3DStyle.PointDepth = 20;
                chart1.Series[i].BorderWidth = 3;


            }
            if (charttypestring == "dj")
            {

                temptype = SeriesChartType.StackedBar;

                chart1.ChartAreas[0].Area3DStyle.Enable3D = checkE_3d.Checked;
                chart1.ChartAreas[0].Area3DStyle.Clustered = checkE_3d.Checked;
                chart1.ChartAreas[0].Area3DStyle.PointDepth = 20;

            }
            if (charttypestring == "yt")
            {

                temptype = SeriesChartType.SplineRange;

                chart1.ChartAreas[0].Area3DStyle.Enable3D = checkE_3d.Checked;
                chart1.ChartAreas[0].Area3DStyle.Clustered = checkE_3d.Checked;
                // chart1.ChartAreas[0].Area3DStyle.PointDepth = 20;

            }
            return temptype;
        }
        bool checkbox1chagned = false;
        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            checkbox1chagned = true;
            RefreshChart();
            checkbox1chagned = false;

        }

        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {

            HitTestResult result = chart1.HitTest(e.X, e.Y);
            if (result != null && result.Object != null)
            {
                // When user hits the LegendItem
                if (result.Object is LegendItem)
                {
                    // Legend item result
                    LegendItem legendItem = (LegendItem)result.Object;

                    // series item selected
                    Series selectedSeries = (Series)legendItem.Tag;

                    if (selectedSeries != null)
                    {



                        if (selectedSeries.Enabled)
                        {
                            selectedSeries.Enabled = false;
                            legendItem.Cells[0].Image = string.Format(Application.StartupPath + @"/img/chk_unchecked.png");

                            //  legendItem.Cells[0].ImageTranspColor = Color.Red;
                            string tempstr = legendItem.Cells[1].Text;
                            tempstr = tempstr.Substring(tempstr.IndexOf(".") + 1, tempstr.Length - tempstr.IndexOf(".") - 1);
                            chartcheck_value(tempstr, false);
                        }

                        else
                        {
                            selectedSeries.Enabled = true;
                            legendItem.Cells[0].Image = string.Format(Application.StartupPath + @"/img/chk_checked.png");

                            // legendItem.Cells[0].ImageTranspColor = Color.Red;
                            string tempstr = legendItem.Cells[1].Text;
                            tempstr = tempstr.Substring(tempstr.IndexOf(".") + 1, tempstr.Length - tempstr.IndexOf(".") - 1);
                            chartcheck_value(tempstr, true);
                        }
                        chart1.ResetAutoValues();
                    }
                }
            }
        }

        private void checkE_3d_CheckedChanged(object sender, EventArgs e)
        {
            RefreshChart();
        }

        private void sbtnColor_Click(object sender, EventArgs e)
        {
            SetColor();
        }

        private void chart_userSH_Load(object sender, EventArgs e)
        {
            this.splitContainerControl1.SplitterPosition = 115;
        }

    }
   
        
}
