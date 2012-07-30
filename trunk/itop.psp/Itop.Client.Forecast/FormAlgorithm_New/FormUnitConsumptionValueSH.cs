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
    public partial class FormUnitConsumptionValueSH : FormBase
    {

        //产值单耗法
        //是否是亿kwh
        bool isyqwh = true;


        //产值单耗法的单位换算 默认情况下，换算为1;
        
        int unitdata = 10000;

        //生产总值(亿元）
        //用电量（亿kWh)
        //单耗（kWh/元）
        private void CheckUnit(string str)
        {
            if (str.Contains("kWh/万元"))
            {
                unitdata = 10000;
                isyqwh = true;
            }
            else
            {
                unitdata = 1000;
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

        int type = 17;
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


        public FormUnitConsumptionValueSH(Ps_forecast_list fr)
        {
            InitializeComponent();
            forecastReport = fr;
            Text = fr.Title;
            chart_user1.SetColor += new chart_userSH.setcolor(chart_user1_SetColor);
            barButtonItem1.Glyph = Itop.ICON.Resource.授权;
            barButtonItem8.Glyph = Itop.ICON.Resource.关闭;
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
        /// 加载GDP数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //FormLoadForecastDataforMaxHour frm = new FormLoadForecastDataforMaxHour();
            //frm.ProjectUID = Itop.Client.MIS.ProgUID;
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    DataRow row = frm.ROW;
            //    Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            //    psp_Type.ForecastID = forecastReport.ID;
            //    psp_Type.Forecast = type;
            //    IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);
            //    for (int j = 0; j < listTypes.Count; j++)
            //    {
            //         Ps_Forecast_Math currtenpfm=(Ps_Forecast_Math)listTypes[j];
            //        //更新GDP数据
            //        if (currtenpfm.Sort==1)
            //        {
            //            for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
            //            {
            //                currtenpfm.GetType().GetProperty("y" + i).SetValue(currtenpfm, Math.Round(double.Parse(row["y" + i.ToString()].ToString()), 1), null);
            //            }
            //            Common.Services.BaseService.Update<Ps_Forecast_Math>(currtenpfm);
            //            break;
            //        }
                   
            //    }
            //}
            //LoadData();
            //this.chart_user1.All_Select(true);
            //RefreshChart();


            //FormForecastLoadDataforMaxHour ffs = new FormForecastLoadDataforMaxHour();
            FormForecastLoadData2 ffs = new FormForecastLoadData2();
            ffs.ISGDP = true;
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
                    //更新用电量数据
                    if (currtenpfm.Sort == 1)
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
                this.simpleButton8.Enabled = false;
                this.simpleButton9.Enabled = false;
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
                this.simpleButton8.Enabled = true;
                this.simpleButton9.Enabled = true;
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
            FormResult fr = new FormResult(4);
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
            ForecastUnitConsumptionValueSet fmhs = new ForecastUnitConsumptionValueSet();
            fmhs.isdhkw = isyqwh;
            if (fmhs.ShowDialog()==DialogResult.OK)
            {
                if (fmhs.isdhkw != isyqwh)
                {
                    isyqwh = fmhs.isdhkw;

                    Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
                    psp_Type.ForecastID = forecastReport.ID;
                    psp_Type.Forecast = type;
                    IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);
                    for (int j = 0; j < listTypes.Count; j++)
                    {
                        Ps_Forecast_Math currtenpfm = (Ps_Forecast_Math)listTypes[j];
                        //更新电量单位
                        if (currtenpfm.Sort == 3)
                        {
                            if (isyqwh)
                            {
                                currtenpfm.Title = "单耗（kWh/万元）";
                            }
                            else
                            {
                                currtenpfm.Title = "单耗（Wh/元）";
                            }
                            Common.Services.BaseService.Update<Ps_Forecast_Math>(currtenpfm);
                            break;
                        }
                       
                    }
                    
                }
                LoadData();
            }
            CalcAll();
          
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



            if (listTypes.Count != 13 || ((Ps_Forecast_Math)listTypes[0]).Col1!="dc")
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
                Ps_Forecast_Math gdppfm = null;//生产总值(亿元）
                Ps_Forecast_Math dlpfm = null;//用电量（亿kWh)
                Ps_Forecast_Math dhpfm = null;//单耗（kW/万元）

                int sort = 5;
                List<Ps_Forecast_Math> newlist = new List<Ps_Forecast_Math>() ;
                Ps_Forecast_Math psp_Type1 = new Ps_Forecast_Math();
                psp_Type1.ID = Guid.NewGuid().ToString();
                psp_Type1.ForecastID = forecastReport.ID;
                psp_Type1.Forecast = type;
                psp_Type1.Title = "生产总值（亿元）";
                psp_Type1.Sort = 1;
                psp_Type1.Col1 = "dc";
                Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type1);
                newlist.Add(psp_Type1);

                        Ps_Forecast_Math psp_Type101 = new Ps_Forecast_Math();
                        psp_Type101.ID = Guid.NewGuid().ToString();
                        psp_Type101.ForecastID = forecastReport.ID;
                        psp_Type101.ParentID = psp_Type1.ID;
                        psp_Type101.Forecast = type;
                        psp_Type101.Title = "一产";
                        psp_Type101.Sort = sort++;
                        psp_Type101.Col1 = "dc";
                        psp_Type101.Col2 = "GDP";
                        Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type101);
                        newlist.Add(psp_Type101);

                        Ps_Forecast_Math psp_Type102 = new Ps_Forecast_Math();
                        psp_Type102.ID = Guid.NewGuid().ToString();
                        psp_Type102.ForecastID = forecastReport.ID;
                        psp_Type102.ParentID = psp_Type1.ID;
                        psp_Type102.Forecast = type;
                        psp_Type102.Title = "二产";
                        psp_Type102.Sort = sort++;
                        psp_Type102.Col1 = "dc";
                        psp_Type102.Col2 = "GDP";
                        Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type102);
                        newlist.Add(psp_Type102);

                        Ps_Forecast_Math psp_Type103 = new Ps_Forecast_Math();
                        psp_Type103.ID = Guid.NewGuid().ToString();
                        psp_Type103.ForecastID = forecastReport.ID;
                        psp_Type103.ParentID = psp_Type1.ID;
                        psp_Type103.Forecast = type;
                        psp_Type103.Title = "三产";
                        psp_Type103.Sort = sort++;
                        psp_Type103.Col1 = "dc";
                        psp_Type103.Col2 = "GDP";
                        Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type103);
                        newlist.Add(psp_Type103);


                Ps_Forecast_Math psp_Type2 = new Ps_Forecast_Math();
                psp_Type2.ID = Guid.NewGuid().ToString();
                psp_Type2.ForecastID = forecastReport.ID;
                psp_Type2.Forecast = type;
                psp_Type2.Title = "全社会用电量（亿kWh）";
                psp_Type2.Sort = 2;
                psp_Type2.Col1 = "dc";
                Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type2);
                newlist.Add(psp_Type2);

                    Ps_Forecast_Math psp_Type201 = new Ps_Forecast_Math();
                    psp_Type201.ID = Guid.NewGuid().ToString();
                    psp_Type201.ParentID = psp_Type2.ID;
                    psp_Type201.ForecastID = forecastReport.ID;
                    psp_Type201.Forecast = type;
                    psp_Type201.Title = "一产";
                    psp_Type201.Sort = sort++;
                    psp_Type201.Col1 = "dc";
                    psp_Type201.Col2 = "DL";
                    Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type201);
                    newlist.Add(psp_Type201);


                    Ps_Forecast_Math psp_Type202 = new Ps_Forecast_Math();
                    psp_Type202.ID = Guid.NewGuid().ToString();
                    psp_Type202.ParentID = psp_Type2.ID;
                    psp_Type202.ForecastID = forecastReport.ID;
                    psp_Type202.Forecast = type;
                    psp_Type202.Title = "二产";
                    psp_Type202.Sort = sort++;
                    psp_Type202.Col1 = "dc";
                    psp_Type202.Col2 = "DL";
                    Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type202);
                    newlist.Add(psp_Type202);


                    Ps_Forecast_Math psp_Type203 = new Ps_Forecast_Math();
                    psp_Type203.ID = Guid.NewGuid().ToString();
                    psp_Type203.ParentID = psp_Type2.ID;
                    psp_Type203.ForecastID = forecastReport.ID;
                    psp_Type203.Forecast = type;
                    psp_Type203.Title = "三产";
                    psp_Type203.Sort = sort++;
                    psp_Type203.Col1 = "dc";
                    psp_Type203.Col2 = "DL";
                    Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type203);
                    newlist.Add(psp_Type203);

                    Ps_Forecast_Math psp_Type204 = new Ps_Forecast_Math();
                    psp_Type204.ID = Guid.NewGuid().ToString();
                    psp_Type204.ParentID = psp_Type2.ID;
                    psp_Type204.ForecastID = forecastReport.ID;
                    psp_Type204.Forecast = type;
                    psp_Type204.Title = "居民";
                    psp_Type204.Sort = sort++;
                    psp_Type204.Col1 = "dc";
                    psp_Type204.Col2 = "DL";
                    Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type204);
                    newlist.Add(psp_Type204);

                Ps_Forecast_Math psp_Type3 = new Ps_Forecast_Math();
                psp_Type3.ID = Guid.NewGuid().ToString();
                psp_Type3.ForecastID = forecastReport.ID;
                psp_Type3.Forecast = type;
                psp_Type3.Title = "单耗（kWh/万元）";
                psp_Type3.Sort = 3;
                psp_Type3.Col1 = "dc";
                Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type3);
                newlist.Add(psp_Type3);

                    Ps_Forecast_Math psp_Type301 = new Ps_Forecast_Math();
                    psp_Type301.ID = Guid.NewGuid().ToString();
                    psp_Type301.ParentID = psp_Type3.ID;
                    psp_Type301.ForecastID = forecastReport.ID;
                    psp_Type301.Forecast = type;
                    psp_Type301.Title = "一产";
                    psp_Type301.Sort = sort++;
                    psp_Type301.Col1 = "dc";
                    psp_Type301.Col2 = "DH";
                    Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type301);
                    newlist.Add(psp_Type301);

                    Ps_Forecast_Math psp_Type302 = new Ps_Forecast_Math();
                    psp_Type302.ID = Guid.NewGuid().ToString();
                    psp_Type302.ParentID = psp_Type3.ID;
                    psp_Type302.ForecastID = forecastReport.ID;
                    psp_Type302.Forecast = type;
                    psp_Type302.Title = "二产";
                    psp_Type302.Sort = sort++;
                    psp_Type302.Col1 = "dc";
                    psp_Type302.Col2 = "DH";
                    Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type302);
                    newlist.Add(psp_Type302);

                    Ps_Forecast_Math psp_Type303 = new Ps_Forecast_Math();
                    psp_Type303.ID = Guid.NewGuid().ToString();
                    psp_Type303.ParentID = psp_Type3.ID;
                    psp_Type303.ForecastID = forecastReport.ID;
                    psp_Type303.Forecast = type;
                    psp_Type303.Title = "三产";
                    psp_Type303.Sort = sort++;
                    psp_Type303.Col1 = "dc";
                    psp_Type303.Col2 = "DH";
                    Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type303);
                    newlist.Add(psp_Type303);


                dataTable = Itop.Common.DataConverter.ToDataTable(newlist);
              
            }
            else 
            {
               
                    dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Forecast_Math));
                    for (int i = 0; i < listTypes.Count; i++)
                    {
                        Ps_Forecast_Math currtenpfm = (Ps_Forecast_Math)listTypes[i];
                        //更新换算单位
                        if (currtenpfm.Sort == 3)
                        {
                            CheckUnit(currtenpfm.Title.ToString());
                            break;
                        }

                      
                    }

                }
                
              this.treeList1.DataSource = dataTable;

              Application.DoEvents();

              bLoadingData = false;
              RefreshChart();

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
            //this.Cursor = Cursors.Default;
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
            repositoryItemTextEdit1.DisplayFormat.FormatString = "n4";
            repositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            repositoryItemTextEdit1.Mask.EditMask = "n4";
            repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

            column.ColumnEdit = repositoryItemTextEdit1;
            //column.DisplayFormat.FormatString = "#####################0.##";
            //column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            column.Format.FormatString = "#####################0.####";
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

            if (v.Col2=="DH")
            {
                return;
            }

            TreeListNode parentNode = node.ParentNode;
            
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
            //Calc2();
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

            Ps_Forecast_Math gdppfm = null;//生产总值(亿元）
            Ps_Forecast_Math dlpfm = null;//用电量（亿kWh)
            Ps_Forecast_Math dhpfm = null;//单耗（kW/万元）
            for (int j = 0; j < listTypes.Count; j++)
            {
                Ps_Forecast_Math currtenpfm = (Ps_Forecast_Math)listTypes[j];
                
                if (currtenpfm.Sort == 1)
                {
                    gdppfm = (Ps_Forecast_Math)listTypes[j];
                }
                if (currtenpfm.Sort == 2)
                {
                    dlpfm = (Ps_Forecast_Math)listTypes[j];
                }
                if (currtenpfm.Sort == 3)
                {
                    dhpfm = (Ps_Forecast_Math)listTypes[j];
                }
            }
            //计算数据
            
            for (int i = forecastReport.StartYear; i <= eyear; i++)
            {
                double gdp = double.Parse(gdppfm.GetType().GetProperty("y" + i).GetValue(gdppfm, null).ToString());
                double dl = double.Parse(dlpfm.GetType().GetProperty("y" + i).GetValue(dlpfm, null).ToString());
                double dh = double.Parse(dhpfm.GetType().GetProperty("y" + i).GetValue(dhpfm, null).ToString());
                if (gdp != 0)
	            {
                    dh = dl * unitdata / gdp;
	            }
                else
                {
                    dh = 0;
                }

                dhpfm.GetType().GetProperty("y" + i).SetValue(dhpfm, Math.Round(dh, 4), null);
            }
            Common.Services.BaseService.Update<Ps_Forecast_Math>(dhpfm);

            for (int i = eyear + 1; i <= forecastReport.YcEndYear; i++)
            {
                double gdp = double.Parse(gdppfm.GetType().GetProperty("y" + i).GetValue(gdppfm, null).ToString());
                double dl = double.Parse(dlpfm.GetType().GetProperty("y" + i).GetValue(dlpfm, null).ToString());
                double dh = double.Parse(dhpfm.GetType().GetProperty("y" + i).GetValue(dhpfm, null).ToString());
                dl = dh * gdp /unitdata ;
                dlpfm.GetType().GetProperty("y" + i).SetValue(dlpfm, Math.Round(dl, 4), null);
            }
            dlpfm.Col4 = "yes";
            Common.Services.BaseService.Update<Ps_Forecast_Math>(dlpfm);
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
            CanEdit = false;
            //if (treeList1.FocusedNode["Sort"].ToString() == "1")
            //{
            //    CanEdit = true;
            //}
            // if (treeList1.FocusedNode["Sort"].ToString()=="2"&&inhistory)
            //{
            //    CanEdit = true;
            //}
            // if (treeList1.FocusedNode["Sort"].ToString() == "3" && outhistory)
            // {
            //     CanEdit = true;
            // }
             if (treeList1.FocusedNode.ParentNode!=null)
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
        //加载电量数据
        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //FormForecastLoadDataforMaxHour ffs = new FormForecastLoadDataforMaxHour();
            FormForecastLoadData2 ffs = new FormForecastLoadData2();
            ffs.maxhour = false;
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
                    //更新用电量数据
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

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormForecastMaxOrBad_SH frm = new FormForecastMaxOrBad_SH(forecastReport);
            frm.Type = type;
            frm.ShowDialog();
            LoadData(); 
        }
        //设置年增长率GPD
        private void simpleButton7_Click(object sender, EventArgs e)
        {
              FormSetGDP2 frm = new FormSetGDP2();
            frm.forecastReport = forecastReport;
            frm.Text = "设置GDP值";
            frm.type = type;
            //DataRow[] rowsoldGDP = dataTable.Select("Title like '全地区GDP%'");

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_Calc pcs = new Ps_Calc();
                pcs.Forecast = type;
                pcs.ForecastID = forecastReport.ID;

                IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);
                Hashtable hs = new Hashtable();
                foreach (Ps_Calc pcl1 in list1)
                {
                    if (!hs.ContainsKey(pcl1.Year))
                    {
                        hs.Add(pcl1.Year, pcl1);
                    }
                }
                DataRow[] rows1 = dataTable.Select("Title like '一产' and Col2='GDP'");
                DataRow[] rows2 = dataTable.Select("Title like '二产' and Col2='GDP'");
                DataRow[] rows3 = dataTable.Select("Title like '三产' and Col2='GDP'");
                DataRow[] rowsparent = dataTable.Select("Title like '生产总值%' ");

                if (rows1.Length > 0 && rows2.Length > 0 && rows3.Length>0 && rowsparent.Length > 0)
                {
                    DataRow oldrow1 = rows1[0];
                    DataRow oldrow2 = rows2[0];
                    DataRow oldrow3 = rows3[0];
                    DataRow oldrowall = rowsparent[0];
                    for (int j = forecastReport.YcStartYear; j <= forecastReport.YcEndYear; j++)
                    {
                        double increasenum1 = ((Ps_Calc)hs[j]).Value1;
                        double basenum1 = double.Parse(oldrow1["y" + (j - 1)].ToString());
                        oldrow1["y" + j] = basenum1 * (1 + increasenum1);


                        double increasenum2 = ((Ps_Calc)hs[j]).Value2;
                        double basenum2 = double.Parse(oldrow2["y" + (j - 1)].ToString());
                        oldrow2["y" + j] = basenum2 * (1 + increasenum2);

                        double increasenum3 = ((Ps_Calc)hs[j]).Value3;
                        double basenum3 = double.Parse(oldrow3["y" + (j - 1)].ToString());
                        oldrow1["y" + j] = basenum3 * (1 + increasenum3);


                        oldrowall["y" + j] = basenum1 * (1 + increasenum1) + basenum2 * (1 + increasenum2)+basenum3 * (1 + increasenum3);

                    }
                    Ps_Forecast_Math pfm1 = DataConverter.RowToObject<Ps_Forecast_Math>(oldrow1);
                    Services.BaseService.Update<Ps_Forecast_Math>(pfm1);
                    Ps_Forecast_Math pfm2 = DataConverter.RowToObject<Ps_Forecast_Math>(oldrow2);
                    Services.BaseService.Update<Ps_Forecast_Math>(pfm2);
                    Ps_Forecast_Math pfm3 = DataConverter.RowToObject<Ps_Forecast_Math>(oldrow3);
                    Services.BaseService.Update<Ps_Forecast_Math>(pfm3);
                    Ps_Forecast_Math pfmall = DataConverter.RowToObject<Ps_Forecast_Math>(oldrowall);
                    Services.BaseService.Update<Ps_Forecast_Math>(pfmall);

                }
             
            }
        }
        //居民用电设置
        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormSetGDP3 frm = new FormSetGDP3();
            frm.Text = "设置居民用电增长率";
            frm.type = type;
            frm.forecastReport = forecastReport;
            if (frm.ShowDialog()==DialogResult.OK)
            {
                Ps_Calc pcs = new Ps_Calc();
                pcs.Forecast = type;
                pcs.ForecastID = forecastReport.ID;

                IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);
                Hashtable hs = new Hashtable();
                foreach (Ps_Calc pcl1 in list1)
                {
                    if (!hs.ContainsKey(pcl1.Year))
                    {
                        hs.Add(pcl1.Year, pcl1);
                    }
                }
                DataRow[] rows1 = dataTable.Select("Title like '一产' and Col2='DL'");
                DataRow[] rows2 = dataTable.Select("Title like '二产' and Col2='DL'");
                DataRow[] rows3 = dataTable.Select("Title like '三产' and Col2='DL'");
                DataRow[] rows4 = dataTable.Select("Title like '居民%' and Col2='DL'");
                DataRow[] rows5 = dataTable.Select("Title like '全社会用电量%' ");

                if (rows1.Length > 0 && rows2.Length > 0 && rows3.Length > 0 && rows4.Length > 0 && rows5.Length > 0)
                {
                    DataRow oldrow1 = rows1[0];
                    DataRow oldrow2 = rows2[0];
                    DataRow oldrow3 = rows3[0];
                    DataRow oldrow4 = rows4[0];
                    DataRow oldrow5 = rows5[0];

                    for (int j = forecastReport.YcStartYear; j <= forecastReport.YcEndYear; j++)
                    {
                        double increasenum1 = ((Ps_Calc)hs[j]).Value4;
                        double basenum1 = double.Parse(oldrow4["y" + (j - 1)].ToString());
                        oldrow4["y" + j] = basenum1 * (1 + increasenum1);

                        oldrow5["y" + j] = double.Parse(oldrow1["y" + j].ToString()) + double.Parse(oldrow2["y" + j].ToString())+ double.Parse(oldrow3["y" + j].ToString()) + double.Parse(oldrow4["y" + j].ToString());

                    }
                    Ps_Forecast_Math pfm4 = DataConverter.RowToObject<Ps_Forecast_Math>(oldrow4);
                    Services.BaseService.Update<Ps_Forecast_Math>(pfm4);
                    Ps_Forecast_Math pfm5 = DataConverter.RowToObject<Ps_Forecast_Math>(oldrow5);
                    Services.BaseService.Update<Ps_Forecast_Math>(pfm5);
                }
                

            }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            CalcAll();
        }
        private void CalcAll()
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

            Ps_Forecast_Math gdppfm = null;//生产总值(亿元）
            Ps_Forecast_Math dlpfm = null;//用电量（亿kWh)
            Ps_Forecast_Math dhpfm = null;//单耗（kW/万元）
            for (int j = 0; j < listTypes.Count; j++)
            {
                Ps_Forecast_Math currtenpfm = (Ps_Forecast_Math)listTypes[j];

                if (currtenpfm.Sort == 1)
                {
                    gdppfm = (Ps_Forecast_Math)listTypes[j];
                }
                if (currtenpfm.Sort == 2)
                {
                    dlpfm = (Ps_Forecast_Math)listTypes[j];
                }
                if (currtenpfm.Sort == 3)
                {
                    dhpfm = (Ps_Forecast_Math)listTypes[j];
                }
            }
            //计算数据

            for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
            {
                double gdp = double.Parse(gdppfm.GetType().GetProperty("y" + i).GetValue(gdppfm, null).ToString());
                double dl = double.Parse(dlpfm.GetType().GetProperty("y" + i).GetValue(dlpfm, null).ToString());
                double dh = double.Parse(dhpfm.GetType().GetProperty("y" + i).GetValue(dhpfm, null).ToString());
                if (gdp != 0)
                {
                    dh = dl * unitdata / gdp;
                }
                else
                {
                    dh = 0;
                }

                dhpfm.GetType().GetProperty("y" + i).SetValue(dhpfm, Math.Round(dh, 4), null);
            }
            Common.Services.BaseService.Update<Ps_Forecast_Math>(dhpfm);

            for (int i = forecastReport.YcStartYear; i <= forecastReport.YcEndYear; i++)
            {
                double gdp = double.Parse(gdppfm.GetType().GetProperty("y" + i).GetValue(gdppfm, null).ToString());
                double dl = double.Parse(dlpfm.GetType().GetProperty("y" + i).GetValue(dlpfm, null).ToString());
                double dh = double.Parse(dhpfm.GetType().GetProperty("y" + i).GetValue(dhpfm, null).ToString());
                dl = dh * gdp / unitdata;
                dlpfm.GetType().GetProperty("y" + i).SetValue(dlpfm, Math.Round(dl, 4), null);
            }
            dlpfm.Col4 = "yes";
            Common.Services.BaseService.Update<Ps_Forecast_Math>(dlpfm);
            LoadData();
        }
        //按三产单耗计算
        private void simpleButton9_Click(object sender, EventArgs e)
        {
            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.ForecastID = forecastReport.ID;
            psp_Type.Forecast = type;
            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);

            Ps_Forecast_Math gdppfm = null;//生产总值(亿元）
            Ps_Forecast_Math dlpfm = null;//用电量（亿kWh)
            Ps_Forecast_Math dhpfm = null;//单耗（kW/万元）


            Ps_Forecast_Math gdppfm01 = null;//生产总值(亿元）
            Ps_Forecast_Math gdppfm02 = null;//生产总值(亿元）
            Ps_Forecast_Math gdppfm03 = null;//生产总值(亿元）


            Ps_Forecast_Math dlpfm01 = null;//用电量（亿kWh)
            Ps_Forecast_Math dlpfm02 = null;//用电量（亿kWh)
            Ps_Forecast_Math dlpfm03 = null;//用电量（亿kWh)
            Ps_Forecast_Math dlpfm04 = null;//用电量（亿kWh)


            Ps_Forecast_Math dhpfm01 = null;//单耗（kW/万元）
            Ps_Forecast_Math dhpfm02 = null;//单耗（kW/万元）
            Ps_Forecast_Math dhpfm03 = null;//单耗（kW/万元）
 



            for (int j = 0; j < listTypes.Count; j++)
            {
                Ps_Forecast_Math currtenpfm = (Ps_Forecast_Math)listTypes[j];
                if (currtenpfm.Sort == 1)
                {
                    gdppfm = (Ps_Forecast_Math)listTypes[j];
                    continue;
                }
                if (currtenpfm.Sort == 2)
                {
                    dlpfm = (Ps_Forecast_Math)listTypes[j];
                    continue;
                }
                if (currtenpfm.Sort == 3)
                {
                    dhpfm = (Ps_Forecast_Math)listTypes[j];
                    continue;
                }
                if (currtenpfm.Title == "一产" && currtenpfm.Col2=="GDP")
                {
                    gdppfm01 = (Ps_Forecast_Math)listTypes[j];
                    continue;
                }
                if (currtenpfm.Title == "二产" && currtenpfm.Col2 == "GDP")
                {
                    gdppfm02 = (Ps_Forecast_Math)listTypes[j];
                    continue;
                }
                if (currtenpfm.Title == "三产" && currtenpfm.Col2 == "GDP")
                {
                    gdppfm03 = (Ps_Forecast_Math)listTypes[j];
                    continue;
                }



                if (currtenpfm.Title == "一产" && currtenpfm.Col2 == "DL")
                {
                    dlpfm01 = (Ps_Forecast_Math)listTypes[j];
                    continue;
                }
                if (currtenpfm.Title == "二产" && currtenpfm.Col2 == "DL")
                {
                    dlpfm02 = (Ps_Forecast_Math)listTypes[j];
                    continue;
                }
                if (currtenpfm.Title == "三产" && currtenpfm.Col2 == "DL")
                {
                    dlpfm03 = (Ps_Forecast_Math)listTypes[j];
                    continue;
                }
                if (currtenpfm.Title == "居民" && currtenpfm.Col2 == "DL")
                {
                    dlpfm04 = (Ps_Forecast_Math)listTypes[j];
                    continue;
                }

                if (currtenpfm.Title == "一产" && currtenpfm.Col2 == "DH")
                {
                    dhpfm01 = (Ps_Forecast_Math)listTypes[j];
                    continue;
                }
                if (currtenpfm.Title == "二产" && currtenpfm.Col2 == "DH")
                {
                    dhpfm02 = (Ps_Forecast_Math)listTypes[j];
                    continue;
                }
                if (currtenpfm.Title == "三产" && currtenpfm.Col2 == "DH")
                {
                    dhpfm03 = (Ps_Forecast_Math)listTypes[j];
                    continue;
                }



            }
            //计算数据

            for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
            {
                //总单耗
                double gdp = double.Parse(gdppfm.GetType().GetProperty("y" + i).GetValue(gdppfm, null).ToString());
                double dl = double.Parse(dlpfm.GetType().GetProperty("y" + i).GetValue(dlpfm, null).ToString());
                double dh = double.Parse(dhpfm.GetType().GetProperty("y" + i).GetValue(dhpfm, null).ToString());
                if (gdp != 0)
                {
                    dh = dl * unitdata / gdp;
                }
                else
                {
                    dh = 0;
                }

                dhpfm.GetType().GetProperty("y" + i).SetValue(dhpfm, Math.Round(dh, 4), null);



                //分单耗01
                double gdp01 = double.Parse(gdppfm01.GetType().GetProperty("y" + i).GetValue(gdppfm01, null).ToString());
                double dl01 = double.Parse(dlpfm01.GetType().GetProperty("y" + i).GetValue(dlpfm01, null).ToString());
                double dh01 = double.Parse(dhpfm01.GetType().GetProperty("y" + i).GetValue(dhpfm01, null).ToString());
                if (gdp01 != 0)
                {
                    dh01 = dl01 * unitdata / gdp01;
                }
                else
                {
                    dh01 = 0;
                }
                dhpfm01.GetType().GetProperty("y" + i).SetValue(dhpfm01, Math.Round(dh01, 4), null);


                //分单耗02
                double gdp02 = double.Parse(gdppfm02.GetType().GetProperty("y" + i).GetValue(gdppfm02, null).ToString());
                double dl02 = double.Parse(dlpfm02.GetType().GetProperty("y" + i).GetValue(dlpfm02, null).ToString());
                double dh02 = double.Parse(dhpfm02.GetType().GetProperty("y" + i).GetValue(dhpfm02, null).ToString());
                if (gdp02 != 0)
                {
                    dh02 = dl02 * unitdata / gdp02;
                }
                else
                {
                    dh02 = 0;
                }
                dhpfm02.GetType().GetProperty("y" + i).SetValue(dhpfm02, Math.Round(dh02, 4), null);

                //分单耗03
                double gdp03 = double.Parse(gdppfm03.GetType().GetProperty("y" + i).GetValue(gdppfm03, null).ToString());
                double dl03 = double.Parse(dlpfm03.GetType().GetProperty("y" + i).GetValue(dlpfm03, null).ToString());
                double dh03 = double.Parse(dhpfm03.GetType().GetProperty("y" + i).GetValue(dhpfm03, null).ToString());
                if (gdp03 != 0)
                {
                    dh03 = dl03 * unitdata / gdp03;
                }
                else
                {
                    dh03 = 0;
                }
                dhpfm03.GetType().GetProperty("y" + i).SetValue(dhpfm03, Math.Round(dh03, 4), null);

            }
            Common.Services.BaseService.Update<Ps_Forecast_Math>(dhpfm);
            Common.Services.BaseService.Update<Ps_Forecast_Math>(dhpfm01);
            Common.Services.BaseService.Update<Ps_Forecast_Math>(dhpfm02);
            Common.Services.BaseService.Update<Ps_Forecast_Math>(dhpfm03);


            for (int i = forecastReport.YcStartYear; i <= forecastReport.YcEndYear; i++)
            {
                double gdp = double.Parse(gdppfm.GetType().GetProperty("y" + i).GetValue(gdppfm, null).ToString());
                double dl = double.Parse(dlpfm.GetType().GetProperty("y" + i).GetValue(dlpfm, null).ToString());
                double dh = double.Parse(dhpfm.GetType().GetProperty("y" + i).GetValue(dhpfm, null).ToString());
                dl = dh * gdp / unitdata;
                dlpfm.GetType().GetProperty("y" + i).SetValue(dlpfm, Math.Round(dl, 4), null);

                //分电量01
                double gdp01 = double.Parse(gdppfm01.GetType().GetProperty("y" + i).GetValue(gdppfm01, null).ToString());
                double dl01 = double.Parse(dlpfm01.GetType().GetProperty("y" + i).GetValue(dlpfm01, null).ToString());
                double dh01 = double.Parse(dhpfm01.GetType().GetProperty("y" + i).GetValue(dhpfm01, null).ToString());
                dl01 = dh01 * gdp01 / unitdata;
                dlpfm01.GetType().GetProperty("y" + i).SetValue(dlpfm01, Math.Round(dl01, 4), null);

                //分电量02
                double gdp02 = double.Parse(gdppfm02.GetType().GetProperty("y" + i).GetValue(gdppfm02, null).ToString());
                double dl02 = double.Parse(dlpfm02.GetType().GetProperty("y" + i).GetValue(dlpfm02, null).ToString());
                double dh02 = double.Parse(dhpfm02.GetType().GetProperty("y" + i).GetValue(dhpfm02, null).ToString());
                dl02 = dh02 * gdp02 / unitdata;
                dlpfm02.GetType().GetProperty("y" + i).SetValue(dlpfm02, Math.Round(dl02, 4), null);

                //分电量03
                double gdp03 = double.Parse(gdppfm03.GetType().GetProperty("y" + i).GetValue(gdppfm03, null).ToString());
                double dl03 = double.Parse(dlpfm03.GetType().GetProperty("y" + i).GetValue(dlpfm03, null).ToString());
                double dh03 = double.Parse(dhpfm03.GetType().GetProperty("y" + i).GetValue(dhpfm03, null).ToString());
                dl03 = dh03 * gdp03 / unitdata;
                dlpfm03.GetType().GetProperty("y" + i).SetValue(dlpfm03, Math.Round(dl03, 4), null);


                double dl04 = double.Parse(dlpfm04.GetType().GetProperty("y" + i).GetValue(dlpfm04, null).ToString());

                dlpfm.GetType().GetProperty("y" + i).SetValue(dlpfm, Math.Round((dl01+dl02+dl03+dl04), 4), null);

            }
            dlpfm.Col4 = "yes";
            Common.Services.BaseService.Update<Ps_Forecast_Math>(dlpfm);
            Common.Services.BaseService.Update<Ps_Forecast_Math>(dlpfm01);
            Common.Services.BaseService.Update<Ps_Forecast_Math>(dlpfm02);
            Common.Services.BaseService.Update<Ps_Forecast_Math>(dlpfm03);

            LoadData();
        }
    }
}