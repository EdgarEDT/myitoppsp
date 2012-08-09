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
using DevExpress.Utils;

namespace Itop.Client.Forecast
{
    public partial class FormForecastFResult : FormBase
    {
        int type = 31;
        DataTable dataTable = new DataTable();
        private Ps_forecast_list forecastReport = null;
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
        public FormForecastFResult(Ps_forecast_list fr)
        {
            InitializeComponent();
            forecastReport = fr;
            Text = fr.Title;
            chart_user1.SetColor += new Itop.Client.Using.chart_userSH.setcolor(chart_user1_SetColor);
            barButtonItem1.Glyph = Itop.ICON.Resource.打回重新编;
            barSubItem1.Glyph = Itop.ICON.Resource.发送;
            barButtonItem6.Glyph = Itop.ICON.Resource.刷新;
            barButtonItem2.Glyph = Itop.ICON.Resource.布局;
            barButtonItem3.Glyph = Itop.ICON.Resource.关闭;
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
                if (row.ParentNode==null)
                {
                    foreach (TreeListColumn col in treeList1.Columns)
                    {
                        if (col.FieldName.IndexOf("y") > -1&&col.FieldName!="y1990")
                        {
                            object obj = row[col.FieldName];
                            if (obj != DBNull.Value)
                            {
                                Ps_Forecast_Math v = new Ps_Forecast_Math();
                                v.ForecastID = forecastReport.ID;
                                v.ID = (string)row["ID"];
                                v.Title = row["Title"].ToString();
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
            LoadData();
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

            for (int i = forecastReport.StartYear; i <= forecastReport.YcEndYear; i++)
            {
                AddColumn(i);
            }
            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.ForecastID = forecastReport.ID;
            psp_Type.Forecast = type;
            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);

            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Forecast_Math));

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
            column.FieldName = "Title";
            column.Caption = "分类名";
            column.VisibleIndex = 0;
            column.Width = 220;
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
           
            column = new TreeListColumn();
            column.FieldName = "Col2";
            column.Caption = "是否选中";
            column.VisibleIndex = 1;
            column.Width = 80;
            column.ColumnEdit = repositoryItemCheckEdit1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});

            column = new TreeListColumn();
            column.FieldName = "y1990";
            column.Caption = "权值系数";
            column.VisibleIndex = 2;
            column.Width = 80;
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
        //重新加载数据
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("确定要重新加载原如预测数据（将请除现有数据）？","询问",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
              
                //清除原有数据
                foreach (DataRow row  in dataTable.Rows)
                {
                    Common.Services.BaseService.DeleteByKey<Ps_Forecast_Math>(row["ID"].ToString());
                }
                GetResult();
                LoadData();
            }

        }
        Hashtable htable = new Hashtable();
        Hashtable resulttable = new Hashtable();
        private void GetResult()
        {
            string sql = " Col4='yes' and (Forecast=1 or Forecast=2  or Forecast=5 or Forecast=6 or Forecast=7 or Forecast=13 or Forecast=15 or Forecast=20) and ParentID=''";
            sql += "  and ForecastID='" + forecastReport.ID + "'";

            htable.Clear();
           
            IList<Ps_Forecast_Math> list = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere",sql);
            foreach (Ps_Forecast_Math pfm in list)
            {
                if (!htable.ContainsKey(pfm.Title))
                {
                    htable.Add(pfm.Title, pfm.Title);
                }
            }
            foreach (string key in htable.Keys)
            {
                resulttable.Clear();
                Ps_Forecast_Math pfm = new Ps_Forecast_Math();
                pfm.ID = Guid.NewGuid().ToString();
                pfm.Title = key.ToString()+"-推荐值";
                pfm.Forecast = type;
                pfm.ForecastID = forecastReport.ID;
                Common.Services.BaseService.Create<Ps_Forecast_Math>(pfm);

                string sqlr = " Col4='yes' and (Forecast=1 or Forecast=2  or Forecast=5 or Forecast=6 or Forecast=7 or Forecast=13 or Forecast=15 or Forecast=20) and ParentID=''";
                sqlr += "  and ForecastID='" + forecastReport.ID + "'  and Title='" + key.ToString() + "'";
                IList<Ps_Forecast_Math> listresult = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere", sqlr);
                foreach (Ps_Forecast_Math pfmr in listresult)
                {
                    string title = string.Empty;
                    if (GetMetheod(pfmr.Forecast, out title))
                    {
                        pfmr.ID = Guid.NewGuid().ToString();
                        pfmr.Forecast = type;
                        pfmr.Title = title;
                        pfmr.ParentID = pfm.ID;
                        pfmr.Col2 = "0";
                        pfmr.Col4 = "";
                        Common.Services.BaseService.Create<Ps_Forecast_Math>(pfmr);
                    }
                }
            }
           
        }
        private bool GetMetheod(int m,out string  method)
        {
            method = commonhelp.GetMethod(m);
            if (resulttable.ContainsKey(m))
            {
                return false;
            }
            else
            {
                resulttable.Add(m, commonhelp.GetMethod(m));
                return true;
            }
            
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if ((treeList1.FocusedColumn.FieldName!="Col2" && treeList1.FocusedColumn.FieldName!="y1990")||treeList1.FocusedNode.ParentNode==null)
            {
                e.Cancel = true;
            }
            if (treeList1.FocusedColumn.FieldName.Contains("y") && treeList1.FocusedNode.ParentNode == null)
            {
                e.Cancel = false;
            }
            if (treeList1.FocusedColumn.FieldName=="y1990"&&treeList1.FocusedNode["Col2"].ToString()!="1")
            {
                e.Cancel = true;
            }
        }

        private void treeList1_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Column.FieldName.IndexOf("y") > -1)
            {
                if (Convert.ToInt32(e.Column.FieldName.Replace("y", "")) >= Convert.ToInt32(firstyear.Replace("y", "")) && Convert.ToInt32(endyear.Replace("y", "")) >= Convert.ToInt32(e.Column.FieldName.Replace("y", "")))

                    e.Appearance.BackColor = Color.FromArgb(152, 122, 254);

                if (e.Column.FieldName=="y1990"&&double.Parse(e.Node["y1990"].ToString())>1)
                {
                    e.Appearance.BackColor = Color.Red;
                }

                if (commonhelp.HasValue(e.Node["ID"].ToString(), e.Column.FieldName))
                {
                    e.Appearance.ForeColor = Color.Salmon;
                }
            }
        }

        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            //表格数据发生变化
            //if (e.Column.FieldName!= "Col2"&&e.Column.FieldName!="y1990") return;
            treeList1.BeginInit();
            try
            {
                CalculateSum(e.Node, e.Column);
            }
            catch
            { }
            treeList1.EndInit();

        }
        private void CalculateSum(TreeListNode node, TreeListColumn column)
        {
            if (node.ParentNode == null)
            {
                DataRow row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;
                Ps_Forecast_Math v = DataConverter.RowToObject<Ps_Forecast_Math>(row);
                commonhelp.SetValue(v.ID, column.FieldName, 1);

            }

            //TreeListNode rootnode = node.ParentNode;
            //if (rootnode==null)
            //{
            //    return;
            //}
            //IList<Ps_Forecast_Math> relist =new List<Ps_Forecast_Math>();

            //double qzvalue = 0;


            //WaitDialogForm wait = new WaitDialogForm("", "正在更新数据，请稍后...");
            //foreach (TreeListNode cnode in rootnode.Nodes)
            //{
            //    DataRow row = (node.TreeList.GetDataRecordByNode(cnode) as DataRowView).Row;
            //    Ps_Forecast_Math v = DataConverter.RowToObject<Ps_Forecast_Math>(row);
            //    double mm = v.y1990;
            //    string select = v.Col2;
            //    if (select == "1")
            //    {
            //        qzvalue += mm;
            //        relist.Add(v);
            //    }  
            //}
            //rootnode.SetValue("y1990", qzvalue);
            //for (int i = forecastReport.StartYear; i <= forecastReport.YcEndYear; i++)
            //{
            //    wait.SetCaption((i - forecastReport.StartYear) * 100 / (forecastReport.YcEndYear - forecastReport.StartYear) + "%");
            //    double sum = 0;
            //    foreach (Ps_Forecast_Math pfm in relist)
            //    {
            //        double mm = pfm.y1990;

            //        sum += double.Parse(pfm.GetType().GetProperty("y" + i).GetValue(pfm,null).ToString()) * mm;

            //    }
            //    commonhelp.ResetValue(rootnode["ID"].ToString(), "y" + i);
            //    rootnode.SetValue("y" + i, sum);
            //}
            //RefreshChart();
            //wait.Close();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }
        private void Save()
        {
            //保存

            foreach (DataRow dataRow in dataTable.Rows)
            {

                TreeListNode row = treeList1.FindNodeByKeyID(dataRow["ID"]);


                Ps_Forecast_Math v = DataConverter.RowToObject<Ps_Forecast_Math>(dataRow); 
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
                v.Col1 = MIS.ProgUID;
                v.Col2 = row["Col2"].ToString();
                if (v.ParentID=="")
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

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        //导出数据
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FileClass.ExportToExcelOld(this.forecastReport.Title, "", this.gridControl1);
            FormResult fr = new FormResult();
            fr.LI = this.treeList1;
            fr.Text = forecastReport.Title;
            fr.ShowDialog();
        }
        //导出图形
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

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ReCount();
        }

        private void ReCount()
        {

            WaitDialogForm wait = new WaitDialogForm("", "正在更新数据，请稍后...");
            int m = 0;
            foreach (TreeListNode node in treeList1.Nodes)
            {
                m++;
                IList<Ps_Forecast_Math> relist = new List<Ps_Forecast_Math>();
                double qzvalue = 0;
                foreach (TreeListNode cnode in node.Nodes)
                {
                    DataRow row = (cnode.TreeList.GetDataRecordByNode(cnode) as DataRowView).Row;
                    Ps_Forecast_Math v = DataConverter.RowToObject<Ps_Forecast_Math>(row);
                    double mm = v.y1990;
                    string select = v.Col2;
                    if (select == "1")
                    {
                        qzvalue += mm;
                        relist.Add(v);
                    }
                }
                //wait.SetCaption(m * 100 / treeList1.Nodes.Count + "%");
                node.SetValue("y1990", qzvalue);
                for (int i = forecastReport.StartYear; i <= forecastReport.YcEndYear; i++)
                {
                    int persent = ((m - 1) * (forecastReport.YcEndYear - forecastReport.StartYear + 1) + i - forecastReport.StartYear) * 100 / (treeList1.Nodes.Count * (forecastReport.YcEndYear - forecastReport.StartYear + 1));
                    wait.SetCaption(persent + "%");
                    double sum = 0;
                    foreach (Ps_Forecast_Math pfm in relist)
                    {
                        double mm = pfm.y1990;

                        sum += double.Parse(pfm.GetType().GetProperty("y" + i).GetValue(pfm, null).ToString()) * mm;

                    }
                    commonhelp.ResetValue(node["ID"].ToString(), "y" + i);
                    node.SetValue("y" + i, sum);
                }

            }
            RefreshChart();
            wait.Close();
        }
       
    }
}
