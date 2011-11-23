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

using Itop.Client.Using;
namespace Itop.Client.Forecast.FormAlgorithm_New
{
    public partial class FormExtrapolationMethod  : FormBase
    {
        int type = 2;
        DataTable dataTable=new DataTable ();
        private Ps_forecast_list forecastReport = null;
        private PublicFunction m_pf = new PublicFunction();
        bool bLoadingData = false;
        bool _canEdit = true;
        string firstyear = "0";
        string endyear ="0";
        bool selectdral = true;

        public bool CanEdit
        {
            get { return _canEdit; }
            set { _canEdit = value; EditRight = value; }
        }

        private bool EditRight = false;

        public FormExtrapolationMethod(Ps_forecast_list fr)
        {
            InitializeComponent();
            forecastReport = fr;
            Text = fr.Title;
        }

        private void HideToolBarButton()
        {
            if (!CanEdit)
            {
                barButtonItem17.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem14.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem26.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem22.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem25.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                simpleButton6.Enabled = false;
            }
            if (!AddRight)
            {
              
            }

        }

        private void Form8Forecast_Load(object sender, EventArgs e)
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



            Ps_Forecast_Setup pfs = new Ps_Forecast_Setup();
            pfs.Forecast = type;
            pfs.ForecastID = forecastReport.ID;
            pfs.StartYear = int.Parse(firstyear.Replace("y", ""));
            pfs.EndYear = int.Parse(endyear.Replace("y", ""));

            IList<Ps_Forecast_Setup> li = Services.BaseService.GetList<Ps_Forecast_Setup>("SelectPs_Forecast_SetupByForecast", pfs);

            if (li.Count != 0)
            {

                if ((li[0].StartYear >= forecastReport.StartYear && forecastReport.EndYear >= li[0].EndYear))
                {
                    firstyear = li[0].StartYear.ToString();
                    endyear = li[0].EndYear.ToString();
                }
                else
                {
                    endyear = "0";
                    firstyear = "0";
                }
            }



        }

        private void LoadData()
        {
            //this.splitContainerControl1.SplitterPosition = (2 * this.splitterControl1.Width) / 3;
            //this.splitContainerControl2.SplitterPosition = splitContainerControl2.Height / 2;

            treeList1.DataSource = null;
           
            bLoadingData = true;
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
            }
           
            AddFixColumn();
            for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
            {
                AddColumn(i);
            }
           
            AddEndColumn();
            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.ForecastID = forecastReport.ID;
            psp_Type.Forecast = type;
            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);

            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Forecast_Math));

            dataTable.Columns.Add("N2", typeof(double));
          
            treeList1.DataSource = dataTable;

            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;

            Application.DoEvents();
            bLoadingData = false;
        }

                //�����С������
        private void AddEndColumn()
        {
            TreeListColumn column = new TreeListColumn();
            column.FieldName = "N2";
            column.Caption = "���ƽ����";
            column.VisibleIndex = 200;
            column.Width = 180;



            RepositoryItemTextEdit repositoryItemTextEdit1 = new RepositoryItemTextEdit();
            repositoryItemTextEdit1.AutoHeight = false;
            repositoryItemTextEdit1.DisplayFormat.FormatString = "n2";
            repositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            repositoryItemTextEdit1.Mask.EditMask = "n2";
            repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

            column.ColumnEdit = repositoryItemTextEdit1;

            column.Format.FormatString = "#####################0.##";
            column.Format.FormatType = DevExpress.Utils.FormatType.Numeric;

            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
        }


        //��ӹ̶���
        private void AddFixColumn()
        {
            TreeListColumn column = new TreeListColumn();
            column.FieldName = "Title";
            column.Caption = "������";
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
        //�����ݺ�����һ��
        private void AddColumn(int year)
        {
            TreeListColumn column = new TreeListColumn();

            column.FieldName = "y" + year;
            column.Tag = year;
            column.Caption = year + "��";
            column.Name = year.ToString();
            column.Width = 70;
            //column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            column.VisibleIndex = year;//������������

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
                            v.Title =  row["Title"].ToString();
                            v.Sort = Convert.ToInt32(col.FieldName.Replace("y", ""));
                            v.y1990 = (double)row[col.FieldName];

                            listValues.Add(v);
                        }
                    }
                }


            }

            this.chart_user1.RefreshChart(listValues, "Title", "Sort", "y1990", hs);
          
        }


        private InputLanguage oldInput = null;
        /// <summary>
        /// ��ȡԭʼ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormForecastLoadData ffs = new FormForecastLoadData();
            ffs.PID = MIS.ProgUID;
            ffs.StartYear = forecastReport.StartYear;
            ffs.EndYear = forecastReport.EndYear;
            if (ffs.ShowDialog() != DialogResult.OK)
                return;


            Hashtable hs = ffs.HS;

            if (hs.Count == 0)
                return;
            string id = Guid.NewGuid().ToString();
            if (ffs.Selectid != "4")
            {
                foreach (Ps_History de3 in hs.Values)
                {
                    Ps_Forecast_Math py = new Ps_Forecast_Math();
                    py.Col1 = de3.ID;
                    py.Forecast = type;
                    py.ForecastID = forecastReport.ID;
                    py = (Ps_Forecast_Math)Services.BaseService.GetObject("SelectPs_Forecast_MathByCol1", py);
                    if (py == null)
                    {

                        Ps_Forecast_Math ForecastMath = new Ps_Forecast_Math();
                        ForecastMath.Title = de3.Title;

                        for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
                        {
                            ForecastMath.GetType().GetProperty("y" + i).SetValue(ForecastMath, de3.GetType().GetProperty("y" + i).GetValue(de3, null), null);
                        }


                        id = id.Substring(0, 8);

                        ForecastMath.Col1 = de3.ID;
                        ForecastMath.ID = id + "|" + de3.ID;
                        ForecastMath.ParentID = id + "|" + de3.ParentID;
                        ForecastMath.Forecast = type;
                        ForecastMath.ForecastID = forecastReport.ID;
                        ForecastMath.Sort = de3.Sort;
                        Services.BaseService.Create("InsertPs_Forecast_MathbyPs_History", ForecastMath);
                    }
                    else
                    {

                        for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
                        {
                            py.GetType().GetProperty("y" + i).SetValue(py, de3.GetType().GetProperty("y" + i).GetValue(de3, null), null);
                        }
                        Services.BaseService.Update<Ps_Forecast_Math>(py);

                    }
                }

            }
            else if (ffs.Selectid == "4")
            {
                foreach (PSP_Types de3 in hs.Values)
                {
                    Ps_Forecast_Math py = new Ps_Forecast_Math();

                    //py = (Ps_Forecast_Math)Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere", "Title='" + de3.Title + "'" + " and Forecast='0' and ForecastID='" + forecastReport.ID + "' and Col1='"+de3.ID+"'");
                    py = (Ps_Forecast_Math)Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere", "  Forecast = '" + type + "' and ForecastID='" + forecastReport.ID + "' and Col1='" + de3.ID + "'");
                    if (py == null)
                    {

                        Ps_Forecast_Math ForecastMath = new Ps_Forecast_Math();

                        IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", "TypeID='" + de3.ID + "'");

                        foreach (PSP_Values value in listValues)
                        {

                            ForecastMath.GetType().GetProperty("y" + value.Year).SetValue(ForecastMath, value.Value, null);
                        }


                        id = id.Substring(0, 8);
                        ForecastMath.Title = de3.Title;
                        ForecastMath.Col1 = de3.ID.ToString();
                        ForecastMath.ID = id + "|" + de3.ID;
                        ForecastMath.ParentID = id + "|" + de3.ParentID;
                        ForecastMath.Forecast = type;
                        ForecastMath.ForecastID = forecastReport.ID;
                        object obj = Services.BaseService.GetObject("SelectPs_Forecast_MathMaxID", null);
                        if (obj != null)
                            ForecastMath.Sort = ((int)obj) + 1;
                        else
                            ForecastMath.Sort = 1;
                        Services.BaseService.Create("InsertPs_Forecast_MathbyPs_History", ForecastMath);
                    }
                    else
                    {


                        IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", " TypeID='" + de3.ID + "'");

                        foreach (PSP_Values value in listValues)
                        {

                            py.GetType().GetProperty("y" + value.Year).SetValue(py, value.Value, null);
                        }

                        Services.BaseService.Update<Ps_Forecast_Math>(py);

                    }
                }

            }


            LoadData();

            this.chart_user1.All_Select(true);
            RefreshChart();
        }



      
       

        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sf=new SaveFileDialog();
            sf.Filter = "JPEG�ļ�(*.jpg)|*.jpg|BMP�ļ�(*.bmp)|*.bmp|PNG�ļ�(*.png)|*.png";
            if(sf.ShowDialog()!=DialogResult.OK)
                return;

            Dundas.Charting.WinControl.ChartImageFormat ci = new Dundas.Charting.WinControl.ChartImageFormat();
            switch(sf.FilterIndex)
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

        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormColor fc = new FormColor();
            fc.DT = dataTable;
            fc.ID = forecastReport.ID.ToString();
            fc.For =type;
            if (fc.ShowDialog() == DialogResult.OK)
                RefreshChart();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


     

       


        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //FileClass.ExportToExcelOld(this.forecastReport.Title, "", this.gridControl1);
            FormResult fr = new FormResult();
            fr.LI = this.treeList1;
            fr.Text = forecastReport.Title;
            fr.ShowDialog();
        }

        private void FormAverageForecast_Resize(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {

            

        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            Brush brush = null;
            Rectangle r = e.Bounds;
            Color c3 = Color.FromArgb(152, 122, 254);
            Color c4 = Color.FromArgb(152, 122, 254);
            //foreach (GridCell cell in alist)
            //{

            if (e.Column.FieldName.IndexOf("y") > -1 && firstyear != "Title" && endyear != "Title")
                {
                    if (Convert.ToInt32(e.Column.FieldName.Replace("y", "")) >= Convert.ToInt32(firstyear.Replace("y", "")) && Convert.ToInt32(endyear.Replace("y", "")) >= Convert.ToInt32(e.Column.FieldName.Replace("y", "")))
                    brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, c3, c4, 180);
                    if (brush != null)
                    {
                        e.Graphics.FillRectangle(brush, r);
                    }
                }
        }

        private void barButtonItem26_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barButtonItem26.Caption == "��ʼ��ȡ��ʷ����")
            {
                barButtonItem26.Caption = "������ȡ��ʷ����";
                firstyear = "0";
                endyear = "0";
                selectdral = false;
                this.simpleButton2.Enabled = false;
                this.barButtonItem17.Enabled = false;
                this.simpleButton4.Enabled = false;


                treeList1.OptionsSelection.MultiSelect = true;
                treeList1.OptionsBehavior.Editable = false;
                treeList1.Refresh();
            }
            else if (barButtonItem26.Caption == "������ȡ��ʷ����")
            {
                barButtonItem26.Caption = "��ʼ��ȡ��ʷ����";
                selectdral = true;
                this.simpleButton2.Enabled = true;
                this.barButtonItem17.Enabled = true;
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



        private void barButtonItem25_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            Save();
            
        }

        private void Save()
        {

            //����

            foreach (DataRow dataRow in dataTable.Rows)
            {

                TreeListNode row = treeList1.FindNodeByKeyID(dataRow["ID"]);

                //for (int i = 0; i < this.treeList1.Nodes.Count; i++)
                //{
                //    TreeListNode row = this.treeList1.Nodes[i];
                Ps_Forecast_Math v = new Ps_Forecast_Math();
                v.ID = row["ID"].ToString();
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

                try
                {
                    Services.BaseService.Update("UpdatePs_Forecast_MathByID", v);

                }
                catch { }
            }
            MsgBox.Show("����ɹ���");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //���水ť
            Save();
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //������ݷ����仯
            RefreshChart();
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ////��������

            

        }



        //���ݽڵ㷵�ش��е���ʷ����
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

        private void button3_Click(object sender, EventArgs e)
        {

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            int syear=int.Parse(firstyear.Replace("y",""));
            int eyear=int.Parse(endyear.Replace("y",""));
            if (eyear >= forecastReport.StartYear)
                RefreshChart(syear, eyear);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int syear = int.Parse(firstyear.Replace("y", ""));
            int eyear = int.Parse(endyear.Replace("y", ""));
            if (eyear >= forecastReport.StartYear)
                RefreshChart(eyear + 1, forecastReport.EndYear);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RefreshChart();
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

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            JS(1);
        }

        

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            JS(2);
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            JS(3);
        }

        private void JS(int index)
        {
            //����Ԥ��ֵ

            int fyear = int.Parse(firstyear.Replace("y", ""));
            int syear = int.Parse(endyear.Replace("y", ""));
            int eyear = forecastReport.EndYear;

            foreach (DataRow dataRow in dataTable.Rows)
            {


                double zzl = 0;
                bool bl = false;
                double value1 = 0;
                try { value1 = (double)dataRow["y" + syear]; }
                catch { }



                if (fyear != 0 && syear != 0)
                {
                    double[] historyValues = GenerateHistoryValue(dataRow, fyear, syear);
                    double[] yn = new double[eyear - syear];
                    int M=syear - fyear;
                    double[] yn1 = new double[M];

                    double M1 = 0;
                    double M2 = 0;
                    double M3 = 0;
                    double M4 = 0;

                    switch (index)
                    { 
                        case 1:
                            yn = Calculator.One(historyValues, eyear - syear,ref M1,ref M2);
                            yn1 = Calculator.One1(M, M1, M2);
                            break;
                        case 2:
                            yn = Calculator.Second(historyValues, eyear - syear, ref M1, ref M2, ref M3);
                           
                            yn1 = Calculator.Second1(M, M1, M2,M3);
                            
                            break;
                        case 3:
                            yn = Calculator.Three(historyValues, eyear - syear, ref M1, ref M2, ref M3, ref M4);
                            yn1 = Calculator.Three1(M, M1, M2, M3,M4);
                            break;
                        case 4:
                            yn = Calculator.Index(historyValues, eyear - syear, ref M1, ref M2);
                            yn1 = Calculator.Index1(M, M1, M2);
                            break;
                        case 5:
                            yn = Calculator.LOG(historyValues, eyear - syear, ref M1, ref M2);
                            yn1 = Calculator.LOG1(M, M1, M2);
                            break;
                    
                    }
                    if (M1 == -999999 && M2 == -999999 )
                    {
                        MsgBox.Show("���㷨ģ��ʧ�ܣ������޽�");
                        break;
                    }
                   
                    for (int i = 1; i <= eyear - syear; i++)
                    {
                        dataRow["y" + (syear + i)] = yn[i - 1];
                    }
                    double n = 0;
                    double n1 =0;
                    double n2=0;
                    for (int j = 0; j < yn1.Length; j++)
                    {
                        try { n1 = yn1[j]; }
                        catch { }
                        try { n2 = (double)dataRow["y" + (fyear + j)]; }
                        catch { }

                        n += (n2 - n1) * (n2 - n1);
                    }
                    dataRow["N2"] = n;
                }
            } 


            ForecastClass fc = new ForecastClass();
            fc.MaxForecast(forecastReport, dataTable);
            RefreshChart();
        }


        private void simpleButton11_Click(object sender, EventArgs e)
        {
            JS(4);
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            JS(5);
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

            }
           
           
        }

        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
             //������ݷ����仯
                if (e.Column.FieldName.Substring(0, 1) != "y") return;
                double d = 0;
                if (!double.TryParse(e.Value.ToString(), out d)) return;
                treeList1.BeginInit();
                try
                {
                    CalculateSum(e.Node, e.Column);
                }
                catch
                { }
                treeList1.EndInit();
                RefreshChart();
           
            
        }
        private void CalculateSum(TreeListNode node, TreeListColumn column)
        {
            DataRow row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;
            Ps_Forecast_Math v = DataConverter.RowToObject<Ps_Forecast_Math>(row);
            Common.Services.BaseService.Update<Ps_Forecast_Math>(v);
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
                if (nd["Title"].ToString().Contains("ͬʱ��"))
                {
                    //��¼ͬʱ��
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

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (!CanEdit)
            {
                e.Cancel = true;
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            RefreshChart();
        }

       
        /// <summary>
        /// ��ӷ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "���ӷ���";

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
                    MsgBox.Show("���ӷ������" + ex.Message);
                }
                RefreshChart();
            }

        }
        /// <summary>
        /// ����ӷ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode row = this.treeList1.FocusedNode;
            if (row == null)
            {
                return;
            }


            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "�����ӷ���";

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
                    MsgBox.Show("���ӷ������" + ex.Message);
                }
                RefreshChart();
            }
        }
        /// <summary>
        /// �޸ķ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode row = this.treeList1.FocusedNode;
            if (row == null)
            {
                return;
            }


            string parentid = row["ParentID"].ToString();


            FormTypeTitle frm = new FormTypeTitle();
            frm.TypeTitle = row["Title"].ToString();
            frm.Text = "�޸ķ�����";

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
                    MsgBox.Show("�޸ĳ���" + ex.Message);
                }
                RefreshChart();
            }
        }
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode row = this.treeList1.FocusedNode;
            if (row == null)
            {
                return;
            }

            if (row.Nodes.Count > 0)
            {
                MsgBox.Show("���¼����࣬����ɾ��");
                return;
            }

            string parentid = row["ParentID"].ToString();



            if (MsgBox.ShowYesNo("�Ƿ�ɾ������ " + row["Title"].ToString() + "��") == DialogResult.Yes)
            {
                Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
                ForecastClass1.TreeNodeToDataObject(psp_Type, row);
                //psp_Type = Itop.Common.DataConverter.RowToObject<Ps_Forecast_Math>(row);
                Ps_Forecast_Math psp_Values = new Ps_Forecast_Math();
                psp_Values.ID = psp_Type.ID;

                try
                {
                    //DeletePSP_ValuesByType����ɾ�����ݺͷ���
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

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
                Hashtable temphs = new Hashtable();
                Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
                psp_Type.ForecastID = forecastReport.ID;
                psp_Type.Forecast = type;
                Common.Services.BaseService.Update("DeletePs_Forecast_MathForecastIDAndForecast", psp_Type);
                psp_Type.Forecast = 0;
                IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);
                //foreach (Ps_Forecast_Math psp_Typetemp in listTypes)
                //{
                //    temphs.Add(psp_Typetemp.ID, Guid.NewGuid().ToString());
                //}
               
                foreach (Ps_Forecast_Math psp_Typetemp in listTypes)
                {
                    string id = psp_Typetemp.ID;
                    psp_Type = new Ps_Forecast_Math();
                    psp_Type = psp_Typetemp;
                    psp_Type.ID = Guid.NewGuid().ToString();
                    psp_Type.Col1 = id;
                    psp_Type.Forecast = type;
                    if (psp_Type.ParentID != "")
                    {
                        if (temphs.ContainsKey(psp_Type.ParentID))
                        {
                            psp_Type.ParentID = temphs[psp_Type.ParentID].ToString();
                        }
                    }

                    Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type);
                }
               
                LoadData();
               
                this.chart_user1.All_Select(true);
                RefreshChart();
           
           
        }












    }
}