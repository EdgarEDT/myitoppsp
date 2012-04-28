using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Table;
using Itop.Domain.Forecast;
using System.Collections;
using DevExpress.XtraGrid.Columns;
using Itop.Client.Projects;
using DevExpress.XtraGrid.Views.BandedGrid;
using Itop.Client.Using;
using Itop.Client.Common;
using Itop.Client.Base;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
namespace Itop.Client.History
{
    public partial class FormFqHisView : FormBase
    {
        Hashtable ht = new Hashtable();
        Hashtable ht1 = new Hashtable();
        Hashtable ht2 = new Hashtable();

       public   DataTable datatable = new DataTable();
       DataTable DT = new DataTable();
       TreeList xTreeList = new TreeList();
       public TreeList LI
       {
           get { return xTreeList; }
           set { xTreeList = value; }
       }


        bool IsFist = true;
        int  RealFistYear = 0;
        string projectUID = ""; 
        int firstyear = 1990;
        int endyear = 2020;
        public string ProjectUID
        {
            set { projectUID = value; }
        }

        public Hashtable HT
        {
            set { ht = value; }
        }
        public Hashtable HT1
        {
            set { ht1 = value; }
        }
        public Hashtable HT2
        {
            set { ht2 = value; }
        }
        public FormFqHisView()
        {
            InitializeComponent();
        }

        private void FormGdpView_Load(object sender, EventArgs e)
        {
            InitData();
            InitForm();
        }

        private void InitData()
        {
            Ps_YearRange py = new Ps_YearRange();
            py.Col4 = "分区供电实绩";
            py.Col5 = projectUID;

            IList<Ps_YearRange> li = Itop.Client.Common.Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);
            if (li.Count > 0)
            {
                firstyear = li[0].StartYear;
                endyear = li[0].FinishYear;
            }
            else
            {
                firstyear = 1990;
                endyear = 2020;
                py.BeginYear = 1990;
                py.FinishYear = endyear;
                py.StartYear = firstyear;
                py.EndYear = 2060;
                py.ID = Guid.NewGuid().ToString();
                Itop.Client.Common.Services.BaseService.Create<Ps_YearRange>(py);
            }
   
            DT.Columns.Add("Title", typeof(string));
            DT.Columns.Add("ID", typeof(string));
            DT.Columns.Add("ParentID", typeof(string));

            GridColumn gridColumn1 = new GridColumn();
            gridColumn1.Caption = "ID";
            gridColumn1.FieldName = "ID";
            gridColumn1.Visible = false;
            gridColumn1.Width = 70;
            gridView1.Columns.Add(gridColumn1);

             GridColumn gridColumn2 = new GridColumn();
            gridColumn2.Caption = "ParentID";
            gridColumn2.FieldName = "ParentID";
            gridColumn2.Visible = false;
            gridColumn2.Width = 70;
            gridView1.Columns.Add(gridColumn2);

            int m=-1;
            this.gridControl1.BeginInit();
            this.gridControl1.BeginUpdate();



            for (int i = firstyear; i <= endyear; i++)
            {
                DT.Columns.Add("y" + i, typeof(double));
                if (!ht.ContainsValue(i))
                    continue;
                if (IsFist)
                {
                    RealFistYear = i;
                    IsFist = false;
                }
                m++;
                //DT.Columns.Add("y" + i, typeof(double));
                GridColumn gridColumn = new GridColumn();
                gridColumn.Caption = i+"年";
                gridColumn.FieldName = "y" + i;
                gridColumn.Visible = true;
                gridColumn.VisibleIndex = 2*m+10;
                gridColumn.Width = 70;
                gridColumn.DisplayFormat.FormatString = "n2";
                gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridView1.Columns.Add(gridColumn);

                if (ht1.ContainsValue(i))
                {
                    gridColumn = new GridColumn();
                    gridColumn.Caption = "年均增长率(%)";
                    gridColumn.FieldName = "m" + i;
                    gridColumn.Visible = true;
                    gridColumn.Width = 100;
                    gridColumn.VisibleIndex = 2 * m + 11;
                    gridColumn.DisplayFormat.FormatString = "n2";
                    gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gridView1.Columns.Add(gridColumn);
                    DT.Columns.Add("m" + i, typeof(double));
                }

                if (ht2.ContainsValue(i))
                {

                    gridColumn = new GridColumn();
                    gridColumn.Caption = "逐年增长率(%)";
                    gridColumn.FieldName = "x" + i;
                    gridColumn.Visible = true;
                    gridColumn.Width = 100;
                    gridColumn.VisibleIndex = 2 * m + 12;
                    gridColumn.DisplayFormat.FormatString = "n2";
                    gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gridView1.Columns.Add(gridColumn);
                    DT.Columns.Add("x" + i, typeof(double));

                }
            
            }

            this.gridControl1.EndUpdate();
            this.gridControl1.EndInit();
            AddDT(LI.Nodes);
            double d = 0;
            //年均增长率
            foreach(DataRow drw1 in DT.Rows)
            {
               
                foreach (DataColumn dc in DT.Columns)
                {
                    if (dc.ColumnName.IndexOf("m") >= 0)
                    {  try
                        {
                         
                            string s = dc.ColumnName.Replace("m", "");
                            int y1 = int.Parse(s);
                            double d1 = 0;
                            try
                            {
                                d1 = (double)drw1["y" + s];
                            }
                            catch { }
                            int peryear = 0;
                            for (int i = y1-1; i >0; i--)
                            {
                                if ( ht.ContainsValue(i))
                                {
                                    peryear = i;
                                    break;
                                }
                            }
                            try
                            {
                                d = (double)drw1["y" + peryear];
                            }
                            catch { }

                             double sss = Math.Round(Math.Pow(d1 / d, 1.0 / (y1 - peryear)) - 1, 4);
                                sss *= 100;

                                if (sss.ToString() == "非数字" || sss.ToString() == "正无穷大")
                                    sss = 0;
                                drw1["m" + s] = sss;
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }
                       
                    }
                }
            }
            //逐年增长率
            double dd = 0;
            foreach (DataRow drw1 in DT.Rows)
            {
                
                foreach (DataColumn dc in DT.Columns)
                {
                    if (dc.ColumnName.IndexOf("x") >= 0)
                    {
                        try
                        {
                            string s = dc.ColumnName.Replace("x", "");
                            int y1 = int.Parse(s);
                            double d1 = 0;
                            try
                            {
                                d1 = (double)drw1["y" + s];
                            }
                            catch { }
                            try
                            {
                                dd = (double)drw1["y" + (y1-1)];
                            }
                            catch { }
                           
                            double sss = Math.Round(Math.Pow(d1 / dd, 1.0 / 1) - 1, 4);
                            sss *= 100;
                            if (sss.ToString() == "非数字" || sss.ToString() == "正无穷大")
                                sss = 0;
                            drw1["x" + s] = sss;
                        }
                        catch (Exception ex)
                        {
                            
                            throw;
                        }
                        
                    }
                }
            }

            this.gridControl1.DataSource = DT;


        
        }
        private void AddDT(TreeListNodes Nodes)
        {
            foreach (TreeListNode Node in Nodes)
            {
                DataRow newrow = DT.NewRow();


                newrow["Title"] = AddStr(Node.Level) + Node["Title"].ToString();
                
                newrow["ID"] = Node["ID"].ToString();
                newrow["ParentID"] = Node["ParentID"].ToString();
                for (int i = firstyear; i <= endyear; i++)
                {
                    newrow["y" + i] = Node["y" + i];
                }
                DT.Rows.Add(newrow);
                if (Node.Nodes.Count>0)
                {
                    AddDT(Node.Nodes);
                }
            }
        }
        private string AddStr(int leng)
        {
            string str=" ";
            for (int i = 0; i < leng; i++)
			{
                str += "   ";
			}
            return str;
        }
        private void InitForm()
        {
            barButtonItem1.Glyph = Itop.ICON.Resource.打印;
            barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

            barButtonItem2.Glyph = Itop.ICON.Resource.关闭;
            barButtonItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

            barButtonItem3.Glyph = Itop.ICON.Resource.发送;
            barButtonItem3.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ComponentPrint.ShowPreview(this.gridControl1, this.gridView1.GroupPanelText);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            FileClass.ExportToExcelOld(this.gridView1.GroupPanelText, "", this.gridControl1);
        }
    }
}