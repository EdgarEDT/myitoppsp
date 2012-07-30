using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
using Itop.Domain.Forecast;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraEditors.Repository;
using System.Collections;
using Itop.Client.Projects;
using Itop.Client.Forecast.FormAlgorithm_New;
using DevExpress.XtraTreeList.Nodes;
using Itop.Common;
using Itop.Client.Using;

namespace Itop.Client.Forecast
{
    public partial class FormForecastFResultAll : FormBase
    {
        int type = 31;
        DataTable dataTable = new DataTable();
        private PublicFunction m_pf = new PublicFunction();
        bool bLoadingData = false;
        bool _canEdit = true;

        int startyear = 0;
        int endyear = 0;
        bool IsFirst = true;


        bool selectdral = true;
        private InputLanguage oldInput = null;
        public bool CanEdit
        {
            get { return _canEdit; }
            set { _canEdit = value; EditRight = value; }
        }

        private bool EditRight = false;
        public FormForecastFResultAll(string title)
        {
            InitializeComponent();
            Text = title;
            chart_user1.SetColor += new Itop.Client.Using.chart_userSH.setcolor(chart_user1_SetColor);
            barSubItem1.Glyph = Itop.ICON.Resource.发送;
            barButtonItem1.Glyph = Itop.ICON.Resource.发送;
            barButtonItem2.Glyph = Itop.ICON.Resource.工具;
            barButtonItem3.Glyph = Itop.ICON.Resource.关闭;
        }

        void chart_user1_SetColor()
        {
            DataTable dt = dataTable.Copy();
            foreach (DataRow row in dt.Rows)
            {
                row["Title"] = row["Col3"].ToString() + "-" + row["Title"].ToString();
            }
            FormColor fc = new FormColor();
            fc.DT = dt;
            fc.ID = MIS.ProgUID;
            fc.For = type;
            if (fc.ShowDialog() == DialogResult.OK)
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

            IList<FORBaseColor> list = Services.BaseService.GetList<FORBaseColor>("SelectFORBaseColorByWhere", "Remark='" + MIS.ProgUID + "-" + type + "'");

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
                    if (row["Col3"].ToString() + "-" + row["Title"].ToString() == bc.Title)
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
                    bc1.Remark = MIS.ProgUID + "-" + type;
                    bc1.Title = row["Col3"].ToString() + "-" + row["Title"].ToString();
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
                if (row.ParentNode == null)
                {
                    foreach (TreeListColumn col in treeList1.Columns)
                    {
                        if (col.FieldName.IndexOf("y") > -1 && col.FieldName != "y1990")
                        {
                            object obj = row[col.FieldName];
                            if (obj != DBNull.Value)
                            {
                                Ps_Forecast_Math v = new Ps_Forecast_Math();
                                v.ForecastID = MIS.ProgUID;
                                v.ID = (string)row["ID"];
                                v.Title = row["Col3"].ToString() + "-" + row["Title"].ToString();
                                v.Sort = Convert.ToInt32(col.FieldName.Replace("y", ""));
                                v.y1991 = (double)row[col.FieldName];

                                listValues.Add(v);
                            }
                        }
                    }
                }



            }

            this.chart_user1.RefreshChart(listValues, "Title", "Sort", "y1991", hs);
        }
        private void CopyBaseColor(FORBaseColor bc1, FORBaseColor bc2)
        {
            bc1.UID = bc2.UID;
            bc1.Title = bc2.Title;
            bc1.Remark = bc2.Remark;
            bc1.Color = bc2.Color;
            bc1.Color1 = ColorTranslator.FromOle(bc2.Color);
        }
        private void FormForecastDResult_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Updata();
            LoadData();
        }
       
        private void LoadData()
        {
           
            treeList1.DataSource = null;
            bLoadingData = true;
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                this.treeList1.Columns.Clear();

            }
            AddFixColumn();

            for (int i = startyear; i <= endyear; i++)
            {
                AddColumn(i);
            }
            string sql2 = " Col1 ='" + MIS.ProgUID + "'and Col4='yes' and Forecast=" + type + " and ParentID='' Order by Col3,Title";
            IList list = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByWhere", sql2);
            dataTable = Itop.Common.DataConverter.ToDataTable(list, typeof(Ps_Forecast_Math));

            this.treeList1.DataSource = dataTable;



            Application.DoEvents();

            bLoadingData = false;
            this.chart_user1.All_Select(true);
            RefreshChart();
        }
        //添加固定列
        private void AddFixColumn()
        {

            TreeListColumn column = new TreeListColumn();

            column = new TreeListColumn();
            column.FieldName = "Col3";
            column.Caption = "方案名称";
            column.VisibleIndex = 0;
            column.Width = 250;
            column.SortIndex = 1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});

            column = new TreeListColumn();
            column.FieldName = "Title";
            column.Caption = "分类名";
            column.VisibleIndex = 1;
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
        //导出数据
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           

        }
        Hashtable htable = new Hashtable();
        Hashtable resulttable = new Hashtable();
        private void Updata()
        {
           
            string sql = " UserID='" +  MIS.ProgUID + "' and Col1='1'";
            IList<Ps_forecast_list> listReports = Common.Services.BaseService.GetList<Ps_forecast_list>("SelectPs_forecast_listByWhere", sql);
            foreach (Ps_forecast_list pfl in listReports)
            {
                SetYear(pfl);
                string sql2 = " Col4='yes' and Forecast="+type+" and ParentID=''";
                sql2 += "  and ForecastID='" + pfl.ID + "'";
                IList<Ps_Forecast_Math> list = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere", sql2);
                foreach (Ps_Forecast_Math pfm in list)
                {
                    pfm.Col3 = pfl.Title;
                    Common.Services.BaseService.Update<Ps_Forecast_Math>(pfm);
                }
            }
           
        }
        private void SetYear(Ps_forecast_list pfl)
        {
            if (IsFirst)
            {
                startyear = pfl.StartYear;
                endyear = pfl.YcEndYear;
                IsFirst = false;
            }
            else
            {
                if (pfl.StartYear<startyear)
                {
                    startyear = pfl.StartYear;
                }
                if (pfl.YcEndYear>endyear)
                {
                    endyear = pfl.YcEndYear;
                }
            }
        }
        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        //导出数据
        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormResultSH fr = new FormResultSH();
            fr.LI = this.treeList1;
            fr.Text = this.Text;
            fr.ShowDialog();
        }
        //导出图形
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void treeList1_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Column.FieldName.IndexOf("y") > -1)
            {
                if (commonhelp.HasValue(e.Node["ID"].ToString(), e.Column.FieldName))
                {
                    e.Appearance.ForeColor = Color.Salmon;
                }
            }
        }
       
    }
}
