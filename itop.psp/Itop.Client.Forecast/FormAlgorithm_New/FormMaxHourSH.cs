using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Itop.Common;
using Itop.Client.Base;
using Itop.Domain.HistoryValue;
using System.Collections;
using System.Reflection;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Itop.Client.Common;
using Itop.Domain.Forecast;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using Dundas.Charting.WinControl;
using DevExpress.Utils;
using Itop.Client.Using;
namespace Itop.Client.Forecast.FormAlgorithm_New
{
    public partial class FormMaxHourSH : FormBase
    {

        //最大小时数法
        //是否是亿kwh
        bool isyqwh = true;


        TreeListNode node = null;

        //最大小时数法的单位换算 默认情况下电量为（亿kWh）,负荷为（MW），换算为10000;
        
        int unitdata = 1000000;
        private void CheckUnit(string str)
        {
            if (str.Contains("亿kWh"))
            {
                unitdata = 100000;
                isyqwh = true;
            }
            else
            {
                unitdata = 10;
                isyqwh = false;
            }
        }
        //历史起始年
        int syear
        {
            get
            {
                int result=0;
                if (firstyear.Contains("y"))
                {
                    firstyear = firstyear.Replace("y", "");
                }

                if (int.TryParse(firstyear, out result))
                {
                }
                return result;
            }
        }
        //历史结束年
        int eyear
        {
            get
            {
                int result = 0;
                if (endyear.Contains("y"))
                {
                    endyear = endyear.Replace("y", "");
                }
                if (int.TryParse(endyear, out result))
                {
                }
                return result;
            }
        }
        int currentyear
        {
            get
            {
                if (treeList1.FocusedColumn!=null&&treeList1.FocusedColumn.FieldName.Contains("y"))
                {
                    return int.Parse(treeList1.FocusedColumn.FieldName.Replace("y", ""));
                }
                else
                {
                    return 0;
                }
            }
        }
        bool inhistory
        {
            get
            {
                if (currentyear>=syear&&currentyear<=eyear&&syear!=eyear)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        bool outhistory
        {
            get
            {
                if (currentyear >eyear)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        int type = 15;
        DataTable dataTable = new DataTable();
        private Ps_forecast_list forecastReport;
        private PublicFunction m_pf = new PublicFunction();
        bool bLoadingData = false;
        bool _canEdit = true;
        string firstyear = "0";
        string endyear = "0";
        bool selectdral = true;
        private InputLanguage oldInput = null;

        public bool CanEdit
        {
            get { return _canEdit; }
            set { _canEdit = value; EditRight = value; }
        }

        private bool EditRight = false;


        public FormMaxHourSH(Ps_forecast_list fr)
        {
            InitializeComponent();
            forecastReport = fr;
            Text = fr.Title;
            chart_user1.SetColor += new chart_userSH.setcolor(chart_user1_SetColor);
        }

        void chart_user1_SetColor()
        {
            FormColor fc = new FormColor();
            fc.DT = dataTable;
            fc.ID = forecastReport.ID.ToString();
            fc.For = type;
            if (fc.ShowDialog() == DialogResult.OK)
                RefreshChart();
        }
        /// <summary>
        /// 参数设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ArgumentSet();
        }
        /// <summary>
        /// 加载电量数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            FormLoadForecastDataforMaxHourSH frm = new FormLoadForecastDataforMaxHourSH();
            frm.ProjectUID = Itop.Client.MIS.ProgUID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DataRow row = frm.ROW;
                Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
                psp_Type.ForecastID = forecastReport.ID;
                psp_Type.Forecast = type;
                IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);
                for (int j = 0; j < listTypes.Count; j++)
                {
                     Ps_Forecast_Math currtenpfm=(Ps_Forecast_Math)listTypes[j];
                    //更新电量数据
                    if (currtenpfm.Sort==1)
	                {
                        for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
                        {
                            currtenpfm.GetType().GetProperty("y" + i).SetValue(currtenpfm, Math.Round(double.Parse(row["y" + i.ToString()].ToString()), 1), null);
                            commonhelp.ResetValue(currtenpfm.ID, "y" + i);
                        }
                        Common.Services.BaseService.Update<Ps_Forecast_Math>(currtenpfm);
                        break;
	                }
                   
                }
            }
            LoadData();
            this.chart_user1.All_Select(true);
            RefreshChart();
        }
        /// <summary>
        /// 开始截取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.Nodes.Count == 0)
            {
                MessageBox.Show("无数据，不能操作！");
                return;
            }
            if (barButtonItem3.Caption == "开始截取历史数据")
            {
                barButtonItem3.Caption = "结束截取历史数据";
                firstyear = "0";
                endyear = "0";
                selectdral = false;
                this.simpleButton2.Enabled = false;
                this.barButtonItem2.Enabled = false;
                this.simpleButton4.Enabled = false;


                treeList1.OptionsSelection.MultiSelect = true;
                treeList1.OptionsBehavior.Editable = false;
                treeList1.Refresh();
            }
            else if (barButtonItem3.Caption == "结束截取历史数据")
            {
                barButtonItem3.Caption = "开始截取历史数据";
                selectdral = true;
                this.simpleButton2.Enabled = true;
                this.barButtonItem2.Enabled = true;
                this.simpleButton4.Enabled = true;
                if (firstyear != "Title")
                {
                    Ps_Forecast_Setup pfs = new Ps_Forecast_Setup();
                    pfs.ID = Guid.NewGuid().ToString();
                    pfs.Forecast = type;
                    pfs.ForecastID = forecastReport.ID;
                    pfs.StartYear = int.Parse(firstyear.Replace("y", ""));
                    pfs.EndYear = int.Parse(endyear.Replace("y", ""));

                    IList<Ps_Forecast_Setup> li = Services.BaseService.GetList<Ps_Forecast_Setup>("SelectPs_Forecast_SetupByForecast", pfs);

                    if (li.Count == 0)
                        Services.BaseService.Create<Ps_Forecast_Setup>(pfs);
                    else
                        Services.BaseService.Update("UpdatePs_Forecast_SetupByForecast", pfs);
                }

                treeList1.OptionsSelection.MultiSelect = false;
                treeList1.OptionsBehavior.Editable = true;
            }
        }
        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FileClass.ExportToExcelOld(this.forecastReport.Title, "", this.gridControl1);
            FormResult fr = new FormResult();
            fr.LI = this.treeList1;
            fr.Text = forecastReport.Title;
            fr.ShowDialog();
        }
        /// <summary>
        /// 导出图形
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "JPEG文件(*.jpg)|*.jpg|BMP文件(*.bmp)|*.bmp|PNG文件(*.png)|*.png";
            if (sf.ShowDialog() != DialogResult.OK)
                return;

            Dundas.Charting.WinControl.ChartImageFormat ci = new Dundas.Charting.WinControl.ChartImageFormat();
            switch (sf.FilterIndex)
            {
                case 0:
                    ci = Dundas.Charting.WinControl.ChartImageFormat.Jpeg;
                    break;

                case 1:
                    ci = Dundas.Charting.WinControl.ChartImageFormat.Bmp;
                    break;

                case 2:
                    ci = Dundas.Charting.WinControl.ChartImageFormat.Png;
                    break;



            }
            this.chart_user1.chart1.SaveAsImage(sf.FileName, ci);
        }
        /// <summary>
        /// 图表颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormColor fc = new FormColor();
            fc.DT = dataTable;
            fc.ID = forecastReport.ID.ToString();
            fc.For = type;
            if (fc.ShowDialog() == DialogResult.OK)
                RefreshChart();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }
        /// <summary>
        /// 历史数据折线图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int syear = int.Parse(firstyear.Replace("y", ""));
            int eyear = int.Parse(endyear.Replace("y", ""));
            if (eyear >= forecastReport.StartYear)
                RefreshChart(syear, eyear);
        }
        private void RefreshChart(int syear, int eyear)
        {

            IList<FORBaseColor> list = Services.BaseService.GetList<FORBaseColor>("SelectFORBaseColorByWhere", "Remark='" + this.forecastReport.ID.ToString() + "-" + type + "'");

            IList<FORBaseColor> li = new List<FORBaseColor>();
            bool bl = false;
            foreach (DataRow row in dataTable.Rows)
            {
                bl = false;
                foreach (FORBaseColor bc in list)
                {
                    if (row["Title"].ToString() == bc.Title)
                    {
                        bl = true;
                        FORBaseColor bc1 = new FORBaseColor();
                        bc1.Color1 = Color.Blue;
                        CopyBaseColor(bc1, bc);
                        li.Add(bc1);
                    }


                }
                if (!bl)
                {
                    FORBaseColor bc1 = new FORBaseColor();
                    bc1.UID = Guid.NewGuid().ToString();
                    bc1.Remark = this.forecastReport.ID.ToString() + "-" + type;
                    bc1.Title = row["Title"].ToString();
                    bc1.Color = 16711680;
                    bc1.Color1 = Color.Blue;
                    Services.BaseService.Create<FORBaseColor>(bc1);
                    li.Add(bc1);
                }

            }
            ArrayList hs = new ArrayList();
            foreach (FORBaseColor bc2 in li)
            {
                hs.Add(bc2.Color1);
            }

            List<Ps_Forecast_Math> listValues = new List<Ps_Forecast_Math>();

            for (int i = 0; i < this.treeList1.Nodes.Count; i++)
            {
                TreeListNode row = treeList1.Nodes[i];
                foreach (TreeListColumn col in treeList1.Columns)
                {
                    if (col.FieldName.IndexOf("y") > -1)
                    {
                        int yyear = int.Parse(col.FieldName.Replace("y", ""));
                        if (yyear >= syear && yyear <= eyear)
                        {
                            object obj = row[col.FieldName];
                            if (obj != DBNull.Value)
                            {
                                Ps_Forecast_Math v = new Ps_Forecast_Math();
                                v.ForecastID = forecastReport.ID;
                                v.ID = (string)row["ID"];
                                v.Title = row["Title"].ToString();
                                v.Sort = Convert.ToInt32(col.FieldName.Replace("y", ""));
                                v.y1990 = (double)row[col.FieldName];

                                listValues.Add(v);
                            }
                        }
                    }
                }


            }

            this.chart_user1.RefreshChart(listValues, "Title", "Sort", "y1990", hs);

        }
        /// <summary>
        /// 预测数据折线图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            int syear = int.Parse(firstyear.Replace("y", ""));
            int eyear = int.Parse(endyear.Replace("y", ""));
            if (eyear >= forecastReport.StartYear)
                RefreshChart(eyear + 1, forecastReport.YcEndYear);
        }
        /// <summary>
        /// 全部折线图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            RefreshChart();
        }
        /// <summary>
        /// 计算预测值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Calc2();
        }
        /// <summary>
        /// 计算参数设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            ArgumentSet();
        }
        /// <summary>
        /// 参数设置
        /// </summary>
        private void ArgumentSet()
        {
            if (firstyear == "0" || endyear == "0")
            {
                MsgBox.Show("请设置历史数据起始年结束年后再点计算单位设置");
                return;
            }
            ForecastMaxHourSet  fmhs=new ForecastMaxHourSet ();
            fmhs.isyqwh = isyqwh;
            if (fmhs.ShowDialog()==DialogResult.OK)
            {
                if (fmhs.isyqwh!=isyqwh)
                {
                    isyqwh=fmhs.isyqwh;

                    Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
                    psp_Type.ForecastID = forecastReport.ID;
                    psp_Type.Forecast = type;
                    IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);
                    for (int j = 0; j < listTypes.Count; j++)
                    {
                        Ps_Forecast_Math currtenpfm = (Ps_Forecast_Math)listTypes[j];
                        //更新电量单位
                        if (currtenpfm.Sort == 1)
                        {
                            if (isyqwh)
                            {
                                currtenpfm.Title = "全社会用电量（亿kWh）";
                            }
                            else
                            {
                                currtenpfm.Title = "全社会用电量（万kWh）";
                            }
                            Common.Services.BaseService.Update<Ps_Forecast_Math>(currtenpfm);
                            break;
                        }
                       
                    }
                    
                }
                LoadData();
            }
            Calc2();
          
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            Save();
        }
        

        private void LoadData()
        {
            treeList1.DataSource = null;
            bLoadingData = true;
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                this.treeList1.Columns.Clear();

                //treeList1.Columns.Clear();
            }
            //((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            AddFixColumn();

            for (int i = forecastReport.StartYear; i <= forecastReport.YcEndYear; i++)
            {
                AddColumn(i);
            }
            //((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.ForecastID = forecastReport.ID;
            psp_Type.Forecast = type;
            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);

            if (listTypes.Count != 3 || ((Ps_Forecast_Math)listTypes[0]).Col1 != "mh")
            {

                try
                {  //删除数据
                    for (int i = 0; i < listTypes.Count; i++)
                    {
                        Common.Services.BaseService.Delete<Ps_Forecast_Math>((Ps_Forecast_Math)listTypes[i]);
                    }
                }
                catch
                {

                }
                List<Ps_Forecast_Math> newlist = new List<Ps_Forecast_Math>() ;
                Ps_Forecast_Math psp_Type1 = new Ps_Forecast_Math();
                psp_Type1.ID = Guid.NewGuid().ToString();
                psp_Type1.ForecastID = forecastReport.ID;
                psp_Type1.Forecast = type;
                psp_Type1.Title = "全社会用电量（亿kWh）";
                psp_Type1.Sort = 1;
                psp_Type1.Col1 = "mh";
                Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type1);
                newlist.Add(psp_Type1);


                Ps_Forecast_Math psp_Type2 = new Ps_Forecast_Math();
                psp_Type2.ID = Guid.NewGuid().ToString();
                psp_Type2.ForecastID = forecastReport.ID;
                psp_Type2.Forecast = type;
                psp_Type2.Title = "负荷(MW)";
                psp_Type2.Sort = 2;
                psp_Type2.Col1 = "mh";
                Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type2);
                newlist.Add(psp_Type2);

                Ps_Forecast_Math psp_Type3 = new Ps_Forecast_Math();
                psp_Type3.ID = Guid.NewGuid().ToString();
                psp_Type3.ForecastID = forecastReport.ID;
                psp_Type3.Forecast = type;
                psp_Type3.Title = "最大小时数(时)";
                psp_Type3.Sort = 3;
                psp_Type3.Col1 = "mh";
                Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type3);
                newlist.Add(psp_Type3);


                dataTable = Itop.Common.DataConverter.ToDataTable(newlist);
              
            }
            else
            {

                dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Forecast_Math));
                for (int i = 0; i < listTypes.Count; i++)
                {
                    Ps_Forecast_Math currtenpfm = (Ps_Forecast_Math)listTypes[i];
                    //更新换算单位
                    if (currtenpfm.Sort == 1)
                    {
                        CheckUnit(currtenpfm.Title.ToString());
                    }
                    break;
                }
               
            }

             this.treeList1.DataSource = dataTable;

            



            Application.DoEvents();

            bLoadingData = false;
            RefreshChart();
        }
        /// <summary>
        /// 处理最大小时数法的数，如果不是三行并且符合标准，则删除，重新添加固定的三行
        /// </summary>
        /// <param name="list"></param>
        private void DealMaxHourData(IList list)
        {

            
            
        }
        
       
        private void RefreshChart()
        {
            ArrayList ht = new ArrayList();
            ht.Add(Color.Red);
            ht.Add(Color.Blue);
            ht.Add(Color.Green);
            ht.Add(Color.Yellow);
            ht.Add(Color.HotPink);
            ht.Add(Color.LawnGreen);
            ht.Add(Color.Khaki);
            ht.Add(Color.LightSlateGray);
            ht.Add(Color.LightSeaGreen);
            ht.Add(Color.Lime);
            ht.Add(Color.Black);
            ht.Add(Color.Brown);
            ht.Add(Color.Crimson);
            int m = 0;

            IList<FORBaseColor> list = Services.BaseService.GetList<FORBaseColor>("SelectFORBaseColorByWhere", "Remark='" + this.forecastReport.ID.ToString() + "-" + type + "'");

            IList<FORBaseColor> li = new List<FORBaseColor>();
            bool bl = false;
            ArrayList aldatablr = new ArrayList();
            foreach (DataRow row in dataTable.Rows)
            {
                aldatablr.Add(row["ID"].ToString());
            }
            foreach (DataRow row in dataTable.Rows)
            {
                if (aldatablr.Contains(row["ParentID"].ToString()))
                    continue;
                bl = false;
                foreach (FORBaseColor bc in list)
                {
                    if (row["Title"].ToString() == bc.Title)
                    {
                        bl = true;
                        FORBaseColor bc1 = new FORBaseColor();
                        bc1.Color1 = Color.Blue;
                        CopyBaseColor(bc1, bc);
                        li.Add(bc1);
                    }


                }
                if (!bl)
                {
                    FORBaseColor bc1 = new FORBaseColor();
                    bc1.UID = Guid.NewGuid().ToString();
                    bc1.Remark = this.forecastReport.ID.ToString() + "-" + type;
                    bc1.Title = row["Title"].ToString();
                    bc1.Color = 16711680;
                    if (m == 0)
                    {
                        Random rd = new Random();
                        m = rd.Next(100);
                    }
                    Color cl = (Color)ht[m % 13];
                    bc1.Color = ColorTranslator.ToOle(cl);
                    bc1.Color1 = cl;
                    //bc1.Color1 = Color.Blue;
                    Services.BaseService.Create<FORBaseColor>(bc1);
                    li.Add(bc1);
                }
                m++;

            }
            ArrayList hs = new ArrayList();
            foreach (FORBaseColor bc2 in li)
            {
                hs.Add(bc2.Color1);
            }

            List<Ps_Forecast_Math> listValues = new List<Ps_Forecast_Math>();

            for (int i = 0; i < treeList1.Nodes.Count; i++)
            {
                TreeListNode row = treeList1.Nodes[i];
                foreach (TreeListColumn col in treeList1.Columns)
                {
                    if (col.FieldName.IndexOf("y") > -1)
                    {
                        object obj = row[col.FieldName];
                        if (obj != DBNull.Value)
                        {
                            Ps_Forecast_Math v = new Ps_Forecast_Math();
                            v.ForecastID = forecastReport.ID;
                            v.ID = (string)row["ID"];
                            v.Title = row["Title"].ToString();
                            v.Sort = Convert.ToInt32(col.FieldName.Replace("y", ""));
                            v.y1990 = (double)row[col.FieldName];

                            listValues.Add(v);
                        }
                    }
                }


            }

            this.chart_user1.RefreshChart(listValues, "Title", "Sort", "y1990", hs);
        }
        private void HideToolBarButton()
        {
            if (!CanEdit)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                //barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                simpleButton6.Enabled = false;
                simpleButton4.Enabled = false;
            }
            if (!AddRight)
            {

            }
            //SelectDaYongHu();
        }
        /// <summary>
        /// 查找对应大用户
        /// </summary>
        //private void SelectDaYongHu()
        //{
        //    //"UserID='" + Itop.Client.MIS.ProgUID + "' and Col1='2' and Title='" + forecastReport.Title + "'  and StartYear='" + forecastReport.StartYear + "'" + "'  and EndYear='" + forecastReport.EndYear + "'"
        //    IList<Ps_forecast_list> listReports = Common.Services.BaseService.GetList<Ps_forecast_list>("SelectPs_forecast_listByWhere", "UserID='" + Itop.Client.MIS.ProgUID + "' and Col1='2' and Title='" + forecastReport.Title + "'");
        //    if (listReports.Count < 1)
        //    {
        //        barButtonItem1.Caption = "无对应大用户方案";
        //        barButtonItem1.Enabled = false;

        //    }
        //    else if (listReports.Count == 1)
        //    {
        //        object obj = Common.Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere",
        //                    "ForecastID='" + listReports[0].ID + "'");
        //        if (obj == null)
        //        {

        //            barButtonItem1.Caption = "对应大用户方案无数据";
        //            barButtonItem1.Enabled = false;
        //        }

        //    }
        //    else
        //    {

        //        barButtonItem1.Caption = "有多个同名大用户方案";
        //        barButtonItem1.Enabled = false;

        //    }

        //}

        private void FormAverageGrowthRate_Load(object sender, EventArgs e)
        {
            HideToolBarButton();
            //chart1.Series.Clear();
            //Show();
            Application.DoEvents();
            //this.Cursor = Cursors.WaitCursor;
            //treeList1.BeginUpdate();
            LoadData();

            //treeList1.EndUpdate();
            RefreshChart();
            //this.Cursor = Cursors.Default
     
            SetHistoryYear();
        }
        //设置历史年份
        private void SetHistoryYear()
        {
            Ps_Forecast_Setup pfs = new Ps_Forecast_Setup();
            pfs.ID = Guid.NewGuid().ToString();
            pfs.Forecast = type;
            pfs.ForecastID = forecastReport.ID;
            pfs.StartYear = forecastReport.StartYear;
            pfs.EndYear = forecastReport.EndYear;

            IList<Ps_Forecast_Setup> li = Services.BaseService.GetList<Ps_Forecast_Setup>("SelectPs_Forecast_SetupByForecast", pfs);
            if (li.Count == 0)
                Services.BaseService.Create<Ps_Forecast_Setup>(pfs);
            else
                Services.BaseService.Update("UpdatePs_Forecast_SetupByForecast", pfs);

            IList<Ps_Forecast_Setup> li2 = Services.BaseService.GetList<Ps_Forecast_Setup>("SelectPs_Forecast_SetupByForecast", pfs);

            if (li2.Count != 0)
            {
                firstyear = li2[0].StartYear.ToString();
                endyear = li2[0].EndYear.ToString();

            }
        }
        //添加固定列
        private void AddFixColumn()
        {
            TreeListColumn column = new TreeListColumn();
            column.FieldName = "Title";
            column.Caption = "分类名";
            column.VisibleIndex = 0;
            column.Width = 180;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "Sort";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "ForecastID";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});

            column = new TreeListColumn();
            column.FieldName = "Forecast";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});

            column = new TreeListColumn();
            column.FieldName = "ID";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "ParentID";
            column.VisibleIndex = -1;

            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
        }
        //添加年份后，新增一列
        private void AddColumn(int year)
        {
            TreeListColumn column = new TreeListColumn();

            column.FieldName = "y" + year;
            column.Tag = year;
            column.Caption = year + "年";
            column.Name = year.ToString();
            column.Width = 70;
            //column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            column.VisibleIndex = year;//有两列隐藏列

            // 
            // repositoryItemTextEdit1
            //
            RepositoryItemTextEdit repositoryItemTextEdit1 = new RepositoryItemTextEdit();
            repositoryItemTextEdit1.AutoHeight = false;
            repositoryItemTextEdit1.DisplayFormat.FormatString = "n2";
            repositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            repositoryItemTextEdit1.Mask.EditMask = "n2";
            repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

            column.ColumnEdit = repositoryItemTextEdit1;
            //column.DisplayFormat.FormatString = "#####################0.##";
            //column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            column.Format.FormatString = "#####################0.##";
            column.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});

        }
        private void CopyBaseColor(FORBaseColor bc1, FORBaseColor bc2)
        {
            bc1.UID = bc2.UID;
            bc1.Title = bc2.Title;
            bc1.Remark = bc2.Remark;
            bc1.Color = bc2.Color;
            bc1.Color1 = ColorTranslator.FromOle(bc2.Color);
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
          
            //表格数据发生变化
            if (e.Column.FieldName.Substring(0, 1) != "y") return;
            double d = 0;
            if (!double.TryParse(e.Value.ToString(), out d)) return;
            //treeList1.BeginInit();
            try
            {
                CalculateSum(e.Node, e.Column);
            }
            catch 
            { }
            //treeList1.EndInit();
            RefreshChart();
           
        }
        private void CalculateSum(TreeListNode node, TreeListColumn column)
        {
            DataRow row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;
            Ps_Forecast_Math v = DataConverter.RowToObject<Ps_Forecast_Math>(row);
            Common.Services.BaseService.Update<Ps_Forecast_Math>(v);


            commonhelp.SetValue(v.ID, column.FieldName, 1);

            TreeListNode parentNode = node.ParentNode;
            if (parentNode == null)
            {
                return;
            }
            double sum = 0;
            bool TSL_falg = false;
            double Tsl_double = 0;
            foreach (TreeListNode nd in parentNode.Nodes)
            {
                if (nd["Title"].ToString().Contains("同时率"))
                {
                    //记录同时率
                    if (Convert.ToDouble(nd[column].ToString()) != 0)
                    {
                        TSL_falg = true;
                        Tsl_double = Convert.ToDouble(nd[column].ToString());
                    }
                    continue;
                }
                object value = nd.GetValue(column.FieldName);
                if (value != null && value != DBNull.Value)
                {
                    sum += Convert.ToDouble(value);
                }
            }
            if (sum != 0)
            {
                if (TSL_falg)
                {
                    sum = sum * Tsl_double;
                }
                parentNode.SetValue(column.FieldName, sum);
            }
            else
            {
                parentNode.SetValue(column.FieldName, null);
            }
            CalculateSum(parentNode, column);
        }
        private void Save()
        {
            //保存

            foreach (DataRow dataRow in dataTable.Rows)
            {

                TreeListNode row = treeList1.FindNodeByKeyID(dataRow["ID"]);

                //for (int i = 0; i < this.treeList1.Nodes.Count; i++)
                //{
                //    TreeListNode row = this.treeList1.Nodes[i];
                Ps_Forecast_Math v = new Ps_Forecast_Math();
               
                v.ID = row["ID"].ToString();
                v.Sort = int.Parse(row["Sort"].ToString());
                v.Col1 = row["Col1"].ToString();
                v.Col2 = row["Col2"].ToString();
                v.Col3 = row["Col3"].ToString();
                v.Col4 = row["Col4"].ToString();
                foreach (TreeListColumn col in this.treeList1.Columns)
                {
                    if (col.FieldName.IndexOf("y") > -1)
                    {
                        object obj = row[col.FieldName];
                        if (obj != DBNull.Value)
                        {
                            v.GetType().GetProperty(col.FieldName).SetValue(v, obj, null);
                        }
                    }
                }
                if (v.Sort==2)
                {
                    v.Col4 = "yes";
                }

                try
                {
                    Services.BaseService.Update("UpdatePs_Forecast_MathByID", v);

                }
                catch { }
            }
            MsgBox.Show("保存成功！");
        }
        /// <summary>
        /// 计算2
        /// </summary>
        private void Calc2()
        {
            if (firstyear == "0" || endyear == "0")
            {
                MsgBox.Show("请设置历史数据起始年结束年后再点参数设置");
                return;
            }
            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.ForecastID = forecastReport.ID;
            psp_Type.Forecast = type;
            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);

            Ps_Forecast_Math dlpfm = null;//电量
            Ps_Forecast_Math fhpfm = null;//负荷
            Ps_Forecast_Math hourpfm = null;//最大小时
            for (int j = 0; j < listTypes.Count; j++)
            {
                Ps_Forecast_Math currtenpfm = (Ps_Forecast_Math)listTypes[j];
                
                if (currtenpfm.Sort == 1)
                {
                    dlpfm = (Ps_Forecast_Math)listTypes[j];
                }
                if (currtenpfm.Sort == 2)
                {
                    fhpfm = (Ps_Forecast_Math)listTypes[j];
                }
                if (currtenpfm.Sort == 3)
                {
                    hourpfm = (Ps_Forecast_Math)listTypes[j];
                }
            }
            //计算数据
            
            for (int i = forecastReport.StartYear; i <= eyear; i++)
            {
                double dl = double.Parse(dlpfm.GetType().GetProperty("y" + i).GetValue(dlpfm, null).ToString());
                double fh = double.Parse(fhpfm.GetType().GetProperty("y" + i).GetValue(fhpfm, null).ToString());
                double hour = double.Parse(hourpfm.GetType().GetProperty("y" + i).GetValue(hourpfm, null).ToString());
                if (fh!=0)
	            {
                    hour = dl * unitdata / fh;
	            }
                else
                {
                    hour = 0;
                }

                hourpfm.GetType().GetProperty("y" + i).SetValue(hourpfm, Math.Round(hour, 0), null);
            }
            Common.Services.BaseService.Update<Ps_Forecast_Math>(hourpfm);

            for (int i = eyear + 1; i <= forecastReport.YcEndYear; i++)
            {
                double dl = double.Parse(dlpfm.GetType().GetProperty("y" + i).GetValue(dlpfm, null).ToString());
                double fh = double.Parse(fhpfm.GetType().GetProperty("y" + i).GetValue(fhpfm, null).ToString());
                double hour = double.Parse(hourpfm.GetType().GetProperty("y" + i).GetValue(hourpfm, null).ToString());
                if (hour != 0)
                {
                    fh = dl * unitdata / hour;
                }
                else
                {
                    fh = 0;
                }

                fhpfm.GetType().GetProperty("y" + i).SetValue(fhpfm, Math.Round(fh, 2), null);
            }
            Common.Services.BaseService.Update<Ps_Forecast_Math>(fhpfm);
            LoadData();
        }

        private void treeList1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!selectdral)
            {
                if (firstyear == "0")
                {
                    if (treeList1.FocusedColumn.FieldName.Contains("y"))
                    {
                        firstyear = treeList1.FocusedColumn.FieldName;
                    }
                }
                else
                {
                    if (treeList1.FocusedColumn.FieldName.Contains("y"))
                    {
                        endyear = treeList1.FocusedColumn.FieldName;
                    }

                    if (Convert.ToInt32(firstyear.Replace("y", "")) > Convert.ToInt32(endyear.Replace("y", "")))
                    {
                        string itemp = firstyear;
                        firstyear = endyear;
                        endyear = itemp;

                    }
                }
                treeList1.Refresh();
            }
        }

        private void treeList1_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Column.FieldName.IndexOf("y") > -1 && firstyear != "Title" && endyear != "Title")
            {
                if (Convert.ToInt32(e.Column.FieldName.Replace("y", "")) >= Convert.ToInt32(firstyear.Replace("y", "")) && Convert.ToInt32(endyear.Replace("y", "")) >= Convert.ToInt32(e.Column.FieldName.Replace("y", "")))

                    e.Appearance.BackColor = Color.FromArgb(152, 122, 254);
                if (commonhelp.HasValue(e.Node["ID"].ToString(), e.Column.FieldName))
                {
                    e.Appearance.ForeColor = Color.Salmon;
                }
            }
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            node=treeList1.FocusedNode;
            CanEdit = false;
            if (treeList1.FocusedNode["Sort"].ToString() == "1")
            {
                CanEdit = true;
            }
             if (treeList1.FocusedNode["Sort"].ToString()=="2"&&inhistory)
            {
                CanEdit = true;
            }
             if (treeList1.FocusedNode["Sort"].ToString() == "3" && outhistory)
             {
                 CanEdit = true;
             }

            if (!CanEdit)
            {
                e.Cancel = true;
            }
            
        }
        private void updateAllPan(Ps_Forecast_Math psp_TypePan, Ps_forecast_list listReports)
        {
            string strtemp = "";
            if (psp_TypePan.Forecast == 2)
            {
                strtemp = " and Title!='同时率'";
            }
            else
                if (psp_TypePan.Forecast == 3)
                {
                    strtemp = "";
                }
            IList<Ps_Forecast_Math> mathlist = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere",
                          "Forecast='" + psp_TypePan.Forecast + "' and ForecastID='" + psp_TypePan.ForecastID + "' and ParentID='" + psp_TypePan.ID + "'" + strtemp + " order by sort desc");
            Ps_Forecast_Math matcgui;
            Ps_Forecast_Math matdyh;
            Ps_Forecast_Math mattsl;


            //if (psp_TypePan.Title.Contains("各县合计"))
            //{
            matcgui = new Ps_Forecast_Math();//常规
            matdyh = new Ps_Forecast_Math();//大用户
            mattsl = new Ps_Forecast_Math();//同时率
            //}
            double value = 0;
            double value2 = 0;
            for (int i = listReports.StartYear; i <= listReports.EndYear; i++)
            {
                value = 0;
                value2 = 0;
                foreach (Ps_Forecast_Math mat in mathlist)
                {
                    if (psp_TypePan.Title.Contains("各县合计") && (mat.Title.Contains("常规") || mat.Title.Contains("大用户") || mat.Title.Contains("同时率")))
                    {
                        if (mat.Title.Contains("常规"))
                        {
                            matcgui = mat;
                        }
                        else
                            if (mat.Title.Contains("大用户"))
                            {
                                matdyh = mat;
                            }
                            else
                                if (mat.Title.Contains("同时率"))
                                {
                                    mattsl = mat;
                                }
                        if (!mat.Title.Contains("同时率"))
                            continue;
                    }
                    if (mat.Title == "同时率")
                        value2 = value2 * Math.Round(Convert.ToDouble(mat.GetType().GetProperty("y" + i.ToString()).GetValue(mat, null)), 2);
                    else
                        value2 += Math.Round(Convert.ToDouble(mat.GetType().GetProperty("y" + i.ToString()).GetValue(mat, null)), 2);

                }
                value += value2;
                psp_TypePan.GetType().GetProperty("y" + i.ToString()).SetValue(psp_TypePan, value, null);
                Common.Services.BaseService.Update<Ps_Forecast_Math>(psp_TypePan);
            }
            if (psp_TypePan.ParentID != "")
            {
                psp_TypePan = Common.Services.BaseService.GetOneByKey<Ps_Forecast_Math>(psp_TypePan.ParentID);
                updateAllPan(psp_TypePan, listReports);
            }
            else
                if (psp_TypePan.Title.Contains("各县合计"))
                {
                    value = 0;
                    value2 = 0;
                    for (int i = listReports.StartYear; i <= listReports.EndYear; i++)
                    {
                        value2 = Math.Round(Convert.ToDouble(mattsl.GetType().GetProperty("y" + i.ToString()).GetValue(mattsl, null)), 2);
                        if (psp_TypePan.Forecast == 3)
                        {
                            if (value2 != 0)
                            {
                                value2 = Math.Round(Convert.ToDouble(psp_TypePan.GetType().GetProperty("y" + i.ToString()).GetValue(psp_TypePan, null)), 2) / value2;

                                value = value2 - Math.Round(Convert.ToDouble(matdyh.GetType().GetProperty("y" + i.ToString()).GetValue(matdyh, null)), 2);
                                matcgui.GetType().GetProperty("y" + i.ToString()).SetValue(matcgui, value, null);
                                Common.Services.BaseService.Update<Ps_Forecast_Math>(matcgui);
                            }

                        }
                        else
                            if (psp_TypePan.Forecast == 2)
                            {
                                value2 = Math.Round(Convert.ToDouble(psp_TypePan.GetType().GetProperty("y" + i.ToString()).GetValue(psp_TypePan, null)), 2);
                                value = value2 - Math.Round(Convert.ToDouble(matdyh.GetType().GetProperty("y" + i.ToString()).GetValue(matdyh, null)), 2);
                                matcgui.GetType().GetProperty("y" + i.ToString()).SetValue(matcgui, value, null);
                                Common.Services.BaseService.Update<Ps_Forecast_Math>(matcgui);

                            }

                    }


                }

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
        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "增加分类";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();

                psp_Type.ID = Guid.NewGuid().ToString();

                psp_Type.Forecast = type;
                psp_Type.ForecastID = forecastReport.ID;

                psp_Type.Title = frm.TypeTitle;
                object obj = Services.BaseService.GetObject("SelectPs_Forecast_MathMaxID", null);
                if (obj != null)
                    psp_Type.Sort = ((int)obj) + 1;
                else
                    psp_Type.Sort = 1;


                try
                {
                    Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type);
                    //psp_Type.ID = (int)Common.Services.BaseService.Create("InsertPSP_P_Types", psp_Type);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));


                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加分类出错：" + ex.Message);
                }
                RefreshChart();
            }
        }
        /// <summary>
        /// 添加子分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode row = this.treeList1.FocusedNode;
            if (row == null)
            {
                return;
            }


            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "增加子分类";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
                psp_Type.ParentID = row["ID"].ToString();

                psp_Type.ID = Guid.NewGuid().ToString();

                psp_Type.Forecast = type;
                psp_Type.ForecastID = forecastReport.ID;

                psp_Type.Title = frm.TypeTitle;
                object obj = Services.BaseService.GetObject("SelectPs_Forecast_MathMaxID", null);
                if (obj != null)
                    psp_Type.Sort = ((int)obj) + 1;
                else
                    psp_Type.Sort = 1;


                try
                {
                    Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type);
                    //psp_Type.ID = (int)Common.Services.BaseService.Create("InsertPSP_P_Types", psp_Type);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));


                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加分类出错：" + ex.Message);
                }
                RefreshChart();
            }
        }
        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode row = this.treeList1.FocusedNode;
            if (row == null)
            {
                return;
            }


            string parentid = row["ParentID"].ToString();


            FormTypeTitle frm = new FormTypeTitle();
            frm.TypeTitle = row["Title"].ToString();
            frm.Text = "修改分类名";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
                ForecastClass1.TreeNodeToDataObject(psp_Type, row);


                //psp_Type = Itop.Common.DataConverter.RowToObject<Ps_Forecast_Math>(row);
                psp_Type.Title = frm.TypeTitle;

                try
                {
                    Common.Services.BaseService.Update<Ps_Forecast_Math>(psp_Type);
                    row.SetValue("Title", frm.TypeTitle);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("修改出错：" + ex.Message);
                }
                RefreshChart();
            }
        }
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode row = this.treeList1.FocusedNode;
            if (row == null)
            {
                return;
            }

            if (row.Nodes.Count > 0)
            {
                MsgBox.Show("有下级分类，不可删除");
                return;
            }

            string parentid = row["ParentID"].ToString();



            if (MsgBox.ShowYesNo("是否删除分类 " + row["Title"].ToString() + "？") == DialogResult.Yes)
            {
                Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
                ForecastClass1.TreeNodeToDataObject(psp_Type, row);
                //psp_Type = Itop.Common.DataConverter.RowToObject<Ps_Forecast_Math>(row);
                Ps_Forecast_Math psp_Values = new Ps_Forecast_Math();
                psp_Values.ID = psp_Type.ID;

                try
                {
                    //DeletePSP_ValuesByType里面删除数据和分类
                    Common.Services.BaseService.Delete<Ps_Forecast_Math>(psp_Values);
                    FORBaseColor bc1 = new FORBaseColor();

                    bc1.Remark = forecastReport.ID + "-" + type;
                    bc1.Title = row["Title"].ToString();
                    Common.Services.BaseService.Update("DeleteFORBaseColorByTitleRemark", bc1);

                    this.treeList1.Nodes.Remove(row);
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.WaitCursor;
                    LoadData();
                    this.Cursor = Cursors.Default;
                }
                RefreshChart();
            }
        }
        //加载负荷数据
        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //FormForecastLoadDataforMaxHour ffs = new FormForecastLoadDataforMaxHour();
            FormForecastLoadData2 ffs = new FormForecastLoadData2();
            ffs.maxhour = true;
            ffs.PID = MIS.ProgUID;
            ffs.StartYear = forecastReport.StartYear;
            ffs.EndYear = forecastReport.EndYear;
            if (ffs.ShowDialog() != DialogResult.OK)
                return;


            Hashtable hs = ffs.HS;

            if (hs.Count == 0)
                return;
            string id = Guid.NewGuid().ToString();
            foreach (Ps_History de3 in hs.Values)
            {
                Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
                psp_Type.ForecastID = forecastReport.ID;
                psp_Type.Forecast = type;
                IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);
                for (int j = 0; j < listTypes.Count; j++)
                {
                    Ps_Forecast_Math currtenpfm = (Ps_Forecast_Math)listTypes[j];
                    //更新负荷数据
                    if (currtenpfm.Sort == 2)
                    {
                        for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
                        {
                            currtenpfm.GetType().GetProperty("y" + i).SetValue(currtenpfm, de3.GetType().GetProperty("y" + i).GetValue(de3, null), null);
                            commonhelp.ResetValue(currtenpfm.ID, "y" + i);
                        }
                        Common.Services.BaseService.Update<Ps_Forecast_Math>(currtenpfm);
                        break;
                    }
                    
                }
               
            }

            LoadData();

            this.chart_user1.All_Select(true);
            RefreshChart();
        }

    }
}