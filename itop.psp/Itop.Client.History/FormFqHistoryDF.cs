using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Itop.Common;
using Itop.Client.Base;
using System.Collections;
using System.Reflection;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Itop.Client.Common;
using Itop.Domain.Forecast;
using DevExpress.XtraEditors.Repository;
using Itop.Client.Using;
using Itop.Domain.Table;
using DevExpress.Utils;
namespace Itop.Client.History
{
    /// <summary>
    /// �õ�������
    /// </summary>
    public partial class FormFqHistoryDF : FormBase
    {
       public  string type1 = "2";
       public   int type = 2;
       public string type31 = "3";
       public  int type32 = 3;
       public  DataTable dataTable = new DataTable();
       public DataTable dataTable2 = new DataTable();
        bool bLoadingData = false;
        bool _canEdit = true;
        int firstyear = 2000;
        int endyear = 2008;
        public string splitstr = "-";
        string where1 = "";
        string where2 = "";
        bool expandocoll = false;
        List<string> AreaList = new List<string>();
        public static FormFqHistoryDF Historyhome = null;
        public FormFqHistoryDF() {
            InitializeComponent();
            treeList1.OptionsView.AutoWidth = false;
            treeList2.OptionsView.AutoWidth = false;

            barButtonItem6.Glyph = Itop.ICON.Resource.�ر�;
        }

        private void HideToolBarButton() {

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
        }
        private void Form8Forecast_Load(object sender, EventArgs e) {
            Application.DoEvents();
            Ps_YearRange py = new Ps_YearRange();
            py.Col4 = "���ط�չʵ��";
            py.Col5 = ProjectUID;

            IList<Ps_YearRange> li = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);
            if (li.Count > 0) {
                firstyear = li[0].StartYear;
                endyear = li[0].FinishYear;
            } else {
                firstyear = 2001;
                endyear = DateTime.Today.Year -1;
                py.BeginYear = 2001;
                py.FinishYear = endyear;
                py.StartYear = firstyear;
                py.EndYear = 2060;
                py.ID = Guid.NewGuid().ToString();
                Services.BaseService.Create<Ps_YearRange>(py);
            }
            if (!AddRight)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!EditRight)
            {
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btCopy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!DeleteRight)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                
            }
            Add_AreaName();
            Historyhome = this;
            LoadData();
            //RefreshChart();
            where1 = string.Format(" Forecast ={0} and Col4 ='{1}' ", type, ProjectUID);
            where2 = string.Format(" Forecast ={0} and Col4 ='{1}' ", type32, ProjectUID);
            initpasteCols();
        }
        /// <summary>
        /// ��ʼ��ճ����
        /// </summary>
        private void initpasteCols() {
            pasteCols.Add("Title");
            pasteCols.Add("Col2");
            pasteCols.Add("y1990");
            pasteCols.Add("y1991");
            for (int i = firstyear; i <= endyear; i++) {
                pasteCols.Add("y" + i);
            }
        }
        /// <summary>
        /// ��ʼ����������
        /// </summary>
        private void Add_AreaName()
        {
            string ProjectID = Itop.Client.MIS.ProgUID;
            string connstr = " ProjectID='" + ProjectID + "'";
            try
            {
               AreaList = (List<string >)Common.Services.BaseService.GetList<string>("SelectPS_Table_AreaWH_Title", connstr);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        List<string> pasteCols = new List<string>();//����ճ����
        private void LoadData() {
            treeList2.DataSource = null;
            treeList2.Columns.Clear();
            //this.splitContainerControl1.SplitterPosition = (2* this.splitterControl1.Width) / 3;
            this.splitContainerControl2.SplitterPosition = splitContainerControl2.Height / 2;
            bLoadingData = true;
            if (dataTable != null) {
                dataTable.Columns.Clear();
                treeList1.ClearNodes();
                treeList1.Columns.Clear();
                dataTable2.Columns.Clear();
                treeList2.Columns.Clear();
            }
            AddFixColumn();
            for (int i = firstyear; i <= endyear; i++) {
                AddColumn(i);
            }
            treeList1.Columns.AssignTo(treeList2.Columns);
            //treeList2.Columns["Sort"].SortOrder = SortOrder.Ascending;
            treeList2.Columns.RemoveAt(1);
            treeList2.Columns.RemoveAt(1);
            treeList1.Columns.RemoveAt(3);
            
            load1();
            load2();
            treeList1.CollapseAll();
            treeList2.CollapseAll();
            if (treeList1.Nodes.Count > 1)
            {
                treeList1.MoveFirst();
                treeList1.Nodes[1].Expanded = true;
            }
           
            if (treeList2.Nodes.Count > 1)
            {
                treeList2.MoveFirst();
                treeList2.FocusedNode = treeList2.Nodes[0];
                treeList2.Nodes[1].Expanded = true;
            }
            bLoadingData = false;
        }
        
        private void load1() {
            ArrayList al = new ArrayList();
            al.Add("ȫ���");
            //Dictionary<string, string> al = new Dictionary<string, string>();
            //al.Add("ȫ����õ�������kWh��", "");
            //al.Add(

            //IList<Base_Data> li1 = Common.Services.BaseService.GetStrongList<Base_Data>();
            //foreach (Base_Data bd in li1)
            //    al.Add(bd.Title);

            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = type;
            psp_Type.Col4 = ProjectUID;
            IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);
            //foreach (Ps_History ph in listTypes)
            //    {
            //    Common.Services.BaseService.Delete<Ps_History>(ph);

            //    }
            if (listTypes.Count == 0) {
                Ps_History pf = new Ps_History();
                pf.ID = createID();
                //pf.ParentID = "";
                pf.Sort =  - 1;
                pf.Forecast = type;
                pf.ForecastID = type1;
                pf.Title = "ȫ���";
                pf.Col4 = ProjectUID;
                Services.BaseService.Create<Ps_History>(pf);
                listTypes.Add(pf);
                Ps_History pf2 = new Ps_History();
                pf2.ID = pf.ID + splitstr;
                //pf.ParentID = "";
                pf2.Sort = -1;
                pf2.Forecast = type32;
                pf2.ForecastID = type31;
                pf2.Title = pf.Title;
                pf2.Col4 = ProjectUID;
                Services.BaseService.Create<Ps_History>(pf2);
            }
            dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));
            treeList1.BeginInit();
            treeList1.DataSource = dataTable;
            
            treeList1.Columns["Sort"].SortOrder = SortOrder.Ascending;
            treeList1.EndInit();
            //Application.DoEvents();
            //treeList1.Nodes[0].Expanded = true;
            
        }
        private void load2() {
            ArrayList al = new ArrayList();
            al.Add("ȫ���");
            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = type32;
            psp_Type.Col4 = ProjectUID;
            IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);
            //foreach (Ps_History ph in listTypes)
            //    {
            //    Common.Services.BaseService.Delete<Ps_History>(ph);

            //    }
            if (listTypes.Count == 0) {
                
                TreeListNode node = treeList1.FindNodeByFieldValue("Title", al[0].ToString());
                Ps_History pf = new Ps_History();
                pf.ID = node["ID"] + splitstr;
                //pf.ParentID = "";
                pf.Sort = -1;
                pf.Forecast = type32;
                pf.ForecastID = type31;
                pf.Title = al[0].ToString();
                pf.Col4 = ProjectUID;
                Services.BaseService.Create<Ps_History>(pf);
                listTypes.Add(pf);
            }
            dataTable2 = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));
            treeList2.DataSource = dataTable2;
            treeList2.Columns["Sort"].SortOrder = SortOrder.Ascending;
            //Application.DoEvents();
            //treeList2.Nodes[0].Expanded = true;
            
        }
        //��ӹ̶���
        private void AddFixColumn() {
            TreeListColumn column = new TreeListColumn();
            column.FieldName = "Title";
            column.Caption = "������";
            column.VisibleIndex = 0;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "Col2";
            column.Caption = "��Ʒ��������";
            column.VisibleIndex = -1;
            column.Width = 150;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "y1990";
            column.Caption = "��������";
            column.VisibleIndex = 2;
            column.Width = 70;
            //column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});



            //������һ�з��ڸ�����ʾ����
           
            column = new TreeListColumn();
            column.FieldName = "y1991";
            column.Caption = "��������";
            column.VisibleIndex = 3;
            column.Width = 70;
            //column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});



            
            column = new TreeListColumn();
            column.FieldName = "Sort";
            column.Caption = "���";
            column.VisibleIndex = -1;
            column.Width = 50;
            column.OptionsColumn.AllowEdit = false;
            //column.SortOrder = SortOrder.Ascending;
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
            column.FieldName = "COL5";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] { column });

            column = new TreeListColumn();
            column.FieldName = "COL6";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] { column });

            column = new TreeListColumn();
            column.FieldName = "COL7";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] { column });
            column = new TreeListColumn();
            column.FieldName = "Col10";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] { column });
        }
        //�����ݺ�����һ��
        private void AddColumn(int year) {
            TreeListColumn column = new TreeListColumn();

            column.FieldName = "y" + year;
            column.Tag = year;
            column.Caption = year + "��";
            column.Name = year.ToString();
            column.Width = 60;
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

        private void CopyBaseColor(FORBaseColor bc1, FORBaseColor bc2) {
            bc1.UID = bc2.UID;
            bc1.Title = bc2.Title;
            bc1.Remark = bc2.Remark;
            bc1.Color = bc2.Color;
            bc1.Color1 = ColorTranslator.FromOle(bc2.Color);
        }
        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "JPEG�ļ�(*.jpg)|*.jpg|BMP�ļ�(*.bmp)|*.bmp|PNG�ļ�(*.png)|*.png";
            if (sf.ShowDialog() != DialogResult.OK)
                return;

            Dundas.Charting.WinControl.ChartImageFormat ci = new Dundas.Charting.WinControl.ChartImageFormat();
            switch (sf.FilterIndex) {
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
            //this.chart1.SaveAsImage(sf.FileName, ci);
        }

        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            FormColor fc = new FormColor();
            fc.DT = dataTable;
            fc.ID = "������չʵ��";
            //fc.For =0;
            //if (fc.ShowDialog() == DialogResult.OK)
            //RefreshChart();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.Close();
        }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

            //FileClass.ExportToExcelOld(this.forecastReport.Title, "", this.gridControl1);
        }

        private void Save() {
            //����
            foreach (DataRow dataRow in dataTable.Rows) {

                try {
                    Ps_History v = Itop.Common.DataConverter.RowToObject<Ps_History>(dataRow);
                    Services.BaseService.Update("UpdatePs_HistoryByID", v);

                } catch { }
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            List<int> li = new List<int>();
            for (int i = firstyear; i <= endyear; i++) {
                li.Add(i);
            }

            FormChooseYears cy = new FormChooseYears();
            cy.ListYearsForChoose = li;
            if (cy.ShowDialog() != DialogResult.OK)
                return;
            Hashtable ht = new Hashtable();

            foreach (DataRow a in cy.DT.Rows) {
                if (a["B"].ToString() == "True")
                    ht.Add(Guid.NewGuid().ToString(), Convert.ToInt32(a["A"].ToString().Replace("��", "")));
            }
            FormGdpView fgv = new FormGdpView();
            fgv.ProjectUID = ProjectUID;
            fgv.HT = ht;
            fgv.ShowDialog();
        }


        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e) {
            //������ݷ����仯
            if (e.Node!=null)
            {
                if (e.Column.FieldName.Substring(0, 1) != "y") return;

                //if ((e.Node.ParentNode.ParentNode == null && !e.Node.HasChildren))
                //{
                //    ;
                //    Common.Services.BaseService.Update<Ps_History>(DataConverter.RowToObject<Ps_History>((e.Node.TreeList.GetDataRecordByNode(e.Node) as DataRowView).Row));
                //    return;
                //}
                double d = 0;
                if (e.Value!=null)
                {
                    if (!double.TryParse(e.Value.ToString(), out d)) return;
                }
               
                try
                {
                    if (!jstjflag)
                    {
                        CalculateSum(e.Node, e.Column);
                    }
                   
                }
                catch { }
                if (e.Node.ParentNode != null)
                {
                    if (!e.Node.ParentNode["Title"].ToString().Contains("ȫ����"))
                        cacsumsehui(e.Column.FieldName);
                }
            }
            
           
            //Save();
            //RefreshChart();
        }
        private void treeList2_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (e.Node!=null)
            {
                if (e.Column.FieldName.Substring(0, 1) != "y") return;

                //if ((e.Node.ParentNode.ParentNode == null && !e.Node.HasChildren))
                //{
                //    ;
                //    Common.Services.BaseService.Update<Ps_History>(DataConverter.RowToObject<Ps_History>((e.Node.TreeList.GetDataRecordByNode(e.Node) as DataRowView).Row));
                //    return;
                //}
                if (!jstjflag)
                {
                    //CalculateSum2(e.Node, e.Column);
                    CalculateSum3(e.Node, e.Column);
                }
               
                if (e.Node.ParentNode != null)
                {
                    if (!e.Node.ParentNode["Title"].ToString().Contains("ȫ����"))
                        cacsumsehui2(e.Column.FieldName);
                }
            }
           
           
        }
        #region �������
        bool xgflag = false;
        /// <summary>
        /// Ϊ���û���ӽ��ɺ������Ӹ��ɺ͵���
        /// </summary>
        private void ptdyhzhi(Ps_History pf,string dyhtype,string fhordl,int byear,int eyear,double ptlv)
        {
          if (dyhtype=="����")
          {
              if (fhordl=="����")
              {
                   for (int i = eyear; i <= 2030; i++)
                   {
                       
                       string y="y"+i.ToString();
                       double zh = pf.y1990;
                       pf.GetType().GetProperty(y).SetValue(pf, zh, null);
                       if (xgflag)
                       {
                           DataRow[] dr = dataTable.Select("ID='" + pf.ID + "'");
                           dr[0][y] = zh;
                       }
                      
                       //setphvalue(pf, y, zh);
                   }
              }
              else if (fhordl=="����")
              {
                  for (int i = eyear; i <= 2030; i++)
                  {
                      
                      string y = "y" + i.ToString();
                      double zh = pf.y1991;
                      pf.GetType().GetProperty(y).SetValue(pf, zh, null);
                      if (xgflag)
                      {
                          DataRow[] dr = dataTable2.Select("ID='" + pf.ID + "'");
                          dr[0][y] = zh;
                      }
                     
                      //setphvalue(pf, y, zh);
                  }
              }
             
          }
          if (dyhtype == "����")
          {
              if (fhordl == "����")
              {
                  //int startyear = (string.IsNullOrEmpty(pf.Col11) ? 0 : Convert.ToInt32(pf.Col11));
                  //int finshyear = (string.IsNullOrEmpty(pf.Col12) ? 0 : Convert.ToInt32(pf.Col12));
                  double value = pf.y1990;
                 
                  int intervalYears = eyear - byear;
                  double basex = (double)Math.Round(value / (Math.Pow(1 + ptlv, intervalYears)), 2);
                 
                  for (int i = byear; i < eyear; i++)
                  {
                     
                      double zhi = Math.Round((Math.Pow(1 + ptlv, i - byear) * basex), 2);
                      string y = "y" + i.ToString();
                      pf.GetType().GetProperty(y).SetValue(pf, zhi, null);
                      if (xgflag)
                      {
                          DataRow[] dr = dataTable.Select("ID='" + pf.ID + "'");
                          dr[0][y] = zhi;
                      }
                     
                     // setphvalue(pf, y, zhi);
                      
                  }
                  for (int i = eyear ; i <= 2030; i++)
                  {
                      
                      string y = "y" + i.ToString();
                      pf.GetType().GetProperty(y).SetValue(pf, value, null);
                     // setphvalue(pf, y, value);
                      if (xgflag)
                      {
                          DataRow[] dr = dataTable.Select("ID='" + pf.ID + "'");
                          dr[0][y] = value;
                      }
                  }
                  

              }
              else if (fhordl == "����")
              {
                  double value = pf.y1991;
                  
                  int intervalYears = eyear - byear;
                  double basex = (double)Math.Round(value / (Math.Pow(1 + ptlv, intervalYears)), 2);
                  for (int i = byear; i < eyear; i++)
                  {
                      double zhi = Math.Round((Math.Pow(1 + ptlv, i - byear) * basex), 2);
                      string y = "y" + i.ToString();
                      pf.GetType().GetProperty(y).SetValue(pf, zhi, null);
                      //setphvalue(pf, y, zhi);
                      if (xgflag)
                      {
                          DataRow[] dr = dataTable2.Select("ID='" + pf.ID + "'");
                          dr[0][y] = zhi;
                      }
                     
                  }
                  for (int i = eyear ; i <= 2030; i++)
                  {
                      string y = "y" + i.ToString();
                      pf.GetType().GetProperty(y).SetValue(pf, value, null);
                      //setphvalue(pf, y, value);
                      if (xgflag)
                      {
                          DataRow[] dr = dataTable2.Select("ID='" + pf.ID + "'");
                          dr[0][y] = value;
                      }
                  }
              }

          }
        }
         List<TreeListNode> adddlnodescollect = new List<TreeListNode>();
        List<TreeListNode> addfhnodescollect = new List<TreeListNode>();
        List<TreeListNode> editdlnodescollect = new List<TreeListNode>();
        List<TreeListNode> editfhnodescollect = new List<TreeListNode>();
        /// <summary>
        /// �½��û�����
        /// </summary>
        private void newHistory() {
            TreeListNode focusedNode = treeList1.FocusedNode;
            FormFqHistoryDF_Edit frm = new FormFqHistoryDF_Edit();
            xgflag = false;
            frm.Text = "�������û�";
            int sort = 0;
            if (focusedNode.Nodes.Count > 0)
            {
                TreeListNode lastnode=focusedNode.Nodes[focusedNode.Nodes.Count - 1];
                sort = (int)lastnode["Sort"] + 1;
               
            }
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_History pf = new Ps_History();
                pf.ID = createID();
                pf.Forecast = type;
                pf.ForecastID = type1;
                pf.Title = frm.TypeTitle;
                pf.Col2 = frm.COL10;
                pf.Col11 = frm.COL11;
                pf.Col12 = frm.COL12;
                pf.Col13 = frm.COL13;
                pf.ParentID = focusedNode["ID"].ToString();
                pf.Col4 = ProjectUID;
                pf.Col5 = frm.COL5;
                pf.Col6 = frm.COL6;
                pf.Col7 = frm.COL7;
                pf.y1990 = frm.y1990;
                pf.y1992=frm.y1992;
                pf.Sort = sort;
                int byear =!string.IsNullOrEmpty(pf.Col11)? Convert.ToInt32(pf.Col11) : 2000;
                int eyear = !string.IsNullOrEmpty(pf.Col12)? Convert.ToInt32(pf.Col12) : 2011;
                if (byear < 1991)
                {
                    byear = 1991;
                }
                if (eyear < 1991)
                {
                    eyear = 1991;
                }
                ptdyhzhi(pf, "����", "����", byear, eyear, pf.y1992);

                Ps_History pf2 = new Ps_History();
                pf2.ID = pf.ID + splitstr;
                pf2.Forecast = type32;
                pf2.ForecastID = type31;
                pf2.Title = pf.Title;
                pf2.ParentID = pf.ParentID + splitstr;
                pf2.Col4 = ProjectUID;
                pf2.Col5 = frm.COL5;
                pf2.Col6 = frm.COL6;
                pf2.Col7 = frm.COL7;
                pf2.Sort = sort;
                pf2.Col11 = frm.COL11;
                pf2.Col12 = frm.COL12;
                pf2.Col13 = frm.COL13;
                pf2.y1991 = frm.y1991;
                pf2.y1992=frm.y1992;
                //byear = Convert.ToInt32(pf2.COL11) != 0 ? Convert.ToInt32(pf2.COL11) : 2000;
                //eyear = Convert.ToInt32(pf2.COL12) != 0 ? Convert.ToInt32(pf2.COL12) : 2011;
                ptdyhzhi(pf2, "����", "����", byear, eyear, pf2.y1992);
                try {
                    Services.BaseService.Create<Ps_History>(pf);
                    Services.BaseService.Create<Ps_History>(pf2);
                    treeList1.BeginInit();
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf, dataTable.NewRow()));
                    treeList1.EndInit();
                    treeList2.BeginInit();
                    dataTable2.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf2, dataTable2.NewRow()));
                    treeList2.EndInit();

                    //CalculateSum(treeList1.FindNodeByKeyID(pf.ID), treeList1.Columns["y1990"]);
                    //CalculateSum(treeList2.FindNodeByKeyID(pf2.ID), treeList2.Columns["y1991"]);

                    CalaculateSumnew(Itop.Common.DataConverter.ObjectToRow(pf, dataTable.NewRow()), "y1990", dataTable);
                    CalaculateSumnew(Itop.Common.DataConverter.ObjectToRow(pf2, dataTable2.NewRow()), "y1991", dataTable2);
                    //CalaculateSumnew1(Itop.Common.DataConverter.ObjectToRow(pf, dataTable.NewRow()),  dataTable);
                    //CalaculateSumnew1(Itop.Common.DataConverter.ObjectToRow(pf2, dataTable2.NewRow()),  dataTable2);

                    //�������
                    //for (int i = byear; i <= endyear; i++)
                    //{
                    //    string y = "y" + i.ToString();
                    //    //CalculateSum(treeList1.FindNodeByKeyID(pf.ID), treeList1.Columns[y]);
                    //    //CalculateSum(treeList2.FindNodeByKeyID(pf2.ID), treeList2.Columns[y]);
                    //    CalaculateSumnew(Itop.Common.DataConverter.ObjectToRow(pf, dataTable.NewRow()), y, dataTable);
                    //    CalaculateSumnew(Itop.Common.DataConverter.ObjectToRow(pf2, dataTable2.NewRow()), y, dataTable2);
                    //}
                   LoadData();
                    //insertNode(focusedNode, pf);
                } catch (Exception ex) { MsgBox.Show("���ӷ������" + ex.Message); }
            }

        }
        private void newHistory2()
        {
            TreeListNode focusedNode = treeList1.FocusedNode;
            FormFqHistoryDF_Edit2 frm = new FormFqHistoryDF_Edit2();
            frm.Text = "���д��û�";
            int sort = 0;
            xgflag = false;
            if (focusedNode.Nodes.Count > 0)
            {
                TreeListNode lastnode = focusedNode.Nodes[focusedNode.Nodes.Count - 1];
                sort = (int)lastnode["Sort"] + 1;

            }
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_History pf = new Ps_History();
                pf.ID = createID();
                pf.Forecast = type;
                pf.ForecastID = type1;
                pf.Title = frm.TypeTitle;
                pf.Col2 = frm.COL10;
                //pf.Col11 = frm.COL11;
                pf.Col12 = frm.COL12;
                pf.Col13 = frm.COL13;
                pf.ParentID = focusedNode["ID"].ToString();
                pf.Col4 = ProjectUID;
                //pf.Col5 = frm.COL5;
                pf.Col6 = frm.COL6;
               // pf.Col7 = frm.COL7;
                pf.y1990 = frm.y1990;
               
                pf.Sort = sort;
                int byear = !string.IsNullOrEmpty(pf.Col11)? Convert.ToInt32(pf.Col11) : 2000;
                int eyear = !string.IsNullOrEmpty(pf.Col12) ? Convert.ToInt32(pf.Col12) : 2011;
                if (byear < 1991)
                {
                    byear = 1991;
                }
                if (eyear < 1991)
                {
                    eyear = 1991;
                }
                ptdyhzhi(pf, "����", "����", byear, eyear, 0.2);

                Ps_History pf2 = new Ps_History();
                pf2.ID = pf.ID + splitstr;
                pf2.Forecast = type32;
                pf2.ForecastID = type31;
                pf2.Title = pf.Title;
                pf2.ParentID = pf.ParentID + splitstr;
                pf2.Col4 = ProjectUID;
                //pf2.Col5 = frm.COL5;
                pf2.Col6 = frm.COL6;
               // pf2.Col7 = frm.COL7;
                pf2.Sort = sort;
                //pf2.Col11 = frm.COL11;
                pf2.Col12 = frm.COL12;
                pf2.Col13 = frm.COL13;
                pf2.y1991 = frm.y1991;
                //byear = Convert.ToInt32(pf2.COL11) != 0 ? Convert.ToInt32(pf2.COL11) : 2000;
                //eyear = Convert.ToInt32(pf2.COL12) != 0 ? Convert.ToInt32(pf2.COL12) : 2011;
                ptdyhzhi(pf2, "����", "����", byear, eyear, 0.2);

                try
                {
                    Services.BaseService.Create<Ps_History>(pf);
                    Services.BaseService.Create<Ps_History>(pf2);
                    treeList1.BeginInit();
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf, dataTable.NewRow()));
                    treeList1.EndInit();
                    treeList2.BeginInit();
                    dataTable2.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf2, dataTable2.NewRow()));
                    treeList2.EndInit();
                    //CalculateSum(treeList1.FindNodeByKeyID(pf.ID), treeList1.Columns["y1990"]);
                    //CalculateSum(treeList2.FindNodeByKeyID(pf2.ID), treeList2.Columns["y1991"]);
                    CalaculateSumnew(Itop.Common.DataConverter.ObjectToRow(pf, dataTable.NewRow()), "y1990", dataTable);
                    CalaculateSumnew(Itop.Common.DataConverter.ObjectToRow(pf2, dataTable2.NewRow()), "y1991", dataTable2);
                    //CalaculateSumnew1(Itop.Common.DataConverter.ObjectToRow(pf, dataTable.NewRow()), dataTable);
                    //CalaculateSumnew1(Itop.Common.DataConverter.ObjectToRow(pf2, dataTable2.NewRow()), dataTable2);
                    //for (int i = eyear; i <= endyear; i++)
                    //{
                    //    string y = "y" + i.ToString();
                    //    //CalculateSum(treeList1.FindNodeByKeyID(pf.ID), treeList1.Columns[y]);
                    //    //CalculateSum(treeList2.FindNodeByKeyID(pf2.ID), treeList2.Columns[y]);
                    //    CalaculateSumnew(Itop.Common.DataConverter.ObjectToRow(pf, dataTable.NewRow()), y, dataTable);
                    //    CalaculateSumnew(Itop.Common.DataConverter.ObjectToRow(pf2, dataTable2.NewRow()), y, dataTable2);
                    //}
                    LoadData();
                    //insertNode(focusedNode, pf);
                }
                catch (Exception ex) { MsgBox.Show("���ӷ������" + ex.Message); }
            }

        }
        //�޸��������û�
        private void editHistory() 
        {
            FormFqHistoryDF_Edit frm = new FormFqHistoryDF_Edit();
            frm.TypeTitle = treeList1.FocusedNode.GetValue("Title").ToString();
            frm.Text = "�޸Ĵ��û�";
            xgflag = true;
            Ps_History v = new Ps_History();
            Ps_History v2 = new Ps_History();
            v = Services.BaseService.GetOneByKey<Ps_History>(treeList1.FocusedNode["ID"].ToString());
            v2 = Services.BaseService.GetOneByKey<Ps_History>(treeList1.FocusedNode["ID"].ToString()+splitstr);
            
            frm.COL10 = v.Col2;
            frm.COL5 = v.Col5;
            frm.COL6 = v.Col6;
            frm.COL7 = v.Col7;
            frm.COL11 = v.Col11;
            frm.COL12 = v.Col12;
            frm.COL13 = v.Col13;

            frm.y1990 = v.y1990;
            frm.y1991 = v2.y1991;
            frm.y1992 = v.y1992;
            if (frm.ShowDialog() == DialogResult.OK) {
               
                v.Title = frm.TypeTitle;                                
                v.Col2 = frm.COL10;
                v.Col5 = frm.COL5;
                v.Col6 = frm.COL6;
                v.Col7 = frm.COL7;
                v.Col11 = frm.COL11;
                v.Col12 = frm.COL12;
                v.Col13 = frm.COL13;
                v.y1990 = frm.y1990;
                v.y1992 = frm.y1992;
                int byear = !string.IsNullOrEmpty(v.Col11) ? Convert.ToInt32(v.Col11) : 2000;
                int eyear = !string.IsNullOrEmpty(v.Col12) ? Convert.ToInt32(v.Col12) : 2011;
                if (byear<1991)
                {
                    byear = 1991;
                }
                if (eyear < 1991)
                {
                    eyear = 1991;
                }
                ptdyhzhi(v, "����", "����", byear, eyear, v.y1992);
               
                v2.Title = v.Title;
                v2.Col5 = frm.COL5;
                v2.Col6 = frm.COL6;
                v2.Col7 = frm.COL7;
                v2.Col11 = frm.COL11;
                v2.Col12 = frm.COL12;
                v2.Col13 = frm.COL13;
                v2.y1991 = frm.y1991;
                v2.y1992 = frm.y1992;
                //byear = Convert.ToInt32(v2.COL11) != 0 ? Convert.ToInt32(v2.COL11) : 2000;
                //eyear = Convert.ToInt32(v2.COL12) != 0 ? Convert.ToInt32(v2.COL12) : 2011;
                ptdyhzhi(v2, "����", "����", byear, eyear, v2.y1992);
                try {
                    try { treeList1.FocusedNode["Col2"] = v.Col2; } catch { }
                    treeList1.FocusedNode["Col5"] = v.Col5;
                    treeList1.FocusedNode["Col6"] = v.Col6;
                    treeList1.FocusedNode["Col7"] = v.Col7;
                    try { treeList1.FocusedNode.SetValue("Title", v.Title); } catch { }
                    Common.Services.BaseService.Update<Ps_History>(v);
                    Common.Services.BaseService.Update<Ps_History>(v2);
                    TreeListNode node = treeList2.FindNodeByKeyID(v2.ID);
                    try { node["Title"] = v2.Title; } catch { }
                    //CalculateSum(treeList1.FindNodeByKeyID(v.ID), treeList1.Columns["y1990"]);
                    //CalculateSum(treeList2.FindNodeByKeyID(v2.ID), treeList2.Columns["y1991"]);
                    DataRow[] dr = dataTable.Select("ID='" + v.ID + "'");
                    if (dr.Length>0)
                    {
                        dr[0]["y1990"] = v.y1990;
                    }
                    dr = dataTable2.Select("ID='" + v2.ID + "'");
                    if (dr.Length > 0)
                    {
                        dr[0]["y1991"] = v2.y1991;
                    }
                    CalaculateSumnew(Itop.Common.DataConverter.ObjectToRow(v, dataTable.NewRow()), "y1990", dataTable);
                    CalaculateSumnew(Itop.Common.DataConverter.ObjectToRow(v2, dataTable2.NewRow()), "y1991", dataTable2);
                    treeList1.BeginInit();
                   
                    treeList1.EndInit();
                    treeList2.BeginInit();
                   
                    treeList2.EndInit();
                    //for (int i = firstyear; i <= endyear; i++)
                    //{
                    //    string y = "y" + i.ToString();
                    //    CalculateSum(treeList1.FindNodeByKeyID(v.ID), treeList1.Columns[y]);
                    //    CalculateSum(treeList2.FindNodeByKeyID(v2.ID), treeList2.Columns[y]);
                    //}

                } catch (Exception ex) { MsgBox.Show("�޸ķ������" + ex.Message); }

            }
        }
        private void editHistory2()
        {
            FormFqHistoryDF_Edit2 frm = new FormFqHistoryDF_Edit2();
            frm.TypeTitle = treeList1.FocusedNode.GetValue("Title").ToString();
            frm.Text = "�޸Ĵ��û�";
            Ps_History v = new Ps_History();
            Ps_History v2 = new Ps_History();
            v = Services.BaseService.GetOneByKey<Ps_History>(treeList1.FocusedNode["ID"].ToString());
            v2 = Services.BaseService.GetOneByKey<Ps_History>(treeList1.FocusedNode["ID"].ToString() + splitstr);
            xgflag = true;
            frm.COL10 = v.Col2;
            //frm.COL5 = v.Col5;
            frm.COL6 = v.Col6;
           // frm.COL7 = v.Col7;
            //frm.COL11 = v.Col11;
            frm.COL12 = v.Col12;
            frm.COL13 = v.Col13;

            frm.y1990 = v.y1990;
            frm.y1991 = v2.y1991;

            if (frm.ShowDialog() == DialogResult.OK)
            {

                v.Title = frm.TypeTitle;
                v.Col2 = frm.COL10;
                //v.Col5 = frm.COL5;
                v.Col6 = frm.COL6;
               // v.Col7 = frm.COL7;
               // v.Col11 = frm.COL11;
                v.Col12 = frm.COL12;
                v.Col13 = frm.COL13;
                v.y1990 = frm.y1990;
                int byear = !string.IsNullOrEmpty(v.Col11) ? Convert.ToInt32(v.Col11) : 2000;
                int eyear = !string.IsNullOrEmpty(v.Col12) ? Convert.ToInt32(v.Col12) : 2011;
                if (byear < 1991)
                {
                    byear = 1991;
                }
                if (eyear < 1991)
                {
                    eyear = 1991;
                }
                ptdyhzhi(v, "����", "����", byear, eyear, 0.2);


                v2.Title = v.Title;
                //v2.Col5 = frm.COL5;
                v2.Col6 = frm.COL6;
                //v2.Col7 = frm.COL7;
               // v2.Col11 = frm.COL11;
                v2.Col12 = frm.COL12;
                v2.Col13 = frm.COL13;
                v2.y1991 = frm.y1991;
                ptdyhzhi(v2, "����", "����", byear, eyear, 0.2);
                try
                {
                    try { treeList1.FocusedNode["Col2"] = v.Col2; }
                    catch { }
                    treeList1.FocusedNode["Col5"] = v.Col5;
                    treeList1.FocusedNode["Col6"] = v.Col6;
                    treeList1.FocusedNode["Col7"] = v.Col7;
                    try { treeList1.FocusedNode.SetValue("Title", v.Title); }
                    catch { }
                    Common.Services.BaseService.Update<Ps_History>(v);
                    Common.Services.BaseService.Update<Ps_History>(v2);
                    TreeListNode node = treeList2.FindNodeByKeyID(v2.ID);
                    try { node["Title"] = v2.Title; }
                    catch { }
                    //CalculateSum(treeList1.FindNodeByKeyID(v.ID), treeList1.Columns["y1990"]);
                    //CalculateSum(treeList2.FindNodeByKeyID(v2.ID), treeList2.Columns["y1991"]);
                    DataRow[] dr = dataTable.Select("ID='" + v.ID + "'");
                    if (dr.Length > 0)
                    {
                        dr[0]["y1990"] = v.y1990;
                    }
                    dr = dataTable2.Select("ID='" + v2.ID + "'");
                    if (dr.Length > 0)
                    {
                        dr[0]["y1991"] = v2.y1991;
                    }
                    CalaculateSumnew(Itop.Common.DataConverter.ObjectToRow(v, dataTable.NewRow()), "y1990", dataTable);
                    CalaculateSumnew(Itop.Common.DataConverter.ObjectToRow(v2, dataTable2.NewRow()), "y1991", dataTable2);
                    treeList1.BeginInit();
                   
                    treeList1.EndInit();
                    treeList2.BeginInit();

                    treeList2.EndInit();
                    //for (int i = firstyear; i <= endyear; i++)
                    //{
                    //    string y = "y" + i.ToString();
                    //    CalculateSum(treeList1.FindNodeByKeyID(v.ID), treeList1.Columns[y]);
                    //    CalculateSum(treeList2.FindNodeByKeyID(v2.ID), treeList2.Columns[y]);
                    //}
                }
                catch (Exception ex) { MsgBox.Show("�޸ķ������" + ex.Message); }

            }
        }
        //��������
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode focusedNode = treeList1.FocusedNode;

            if (focusedNode == null)
            {
                return;
            }

            if (focusedNode["Title"].ToString().Contains("�����û�"))
            {
                newHistory();
                return;
            }
            if (focusedNode["Title"].ToString().Contains("�����û�"))
            {
                newHistory2();
                return;
            }
            if (focusedNode["Title"].ToString().Contains("ͬʱ��"))
            {
                MessageBox.Show("ͬʱ���²���������࣡");
                return;
            }
            FormFqHistory_add ffh = new FormFqHistory_add();
            ffh.Text = "��������";
            int sort = 0;
            if (focusedNode.Nodes.Count > 0)
                sort = (int)focusedNode.Nodes[focusedNode.Nodes.Count - 1]["Sort"] + 1;
          
            if (ffh.ShowDialog()==DialogResult.OK)
            {
                for (int i = 0; i < ffh.Addlist.Count; i++)
                {
                    Ps_History pf = new Ps_History();
                    pf.ID = createID();
                    pf.Forecast = type;
                    pf.ForecastID = type1;
                    pf.Title = ffh.Addlist[i].ToString();
                    pf.ParentID = focusedNode["ID"].ToString();
                    pf.Col4 = ProjectUID;
                    pf.Sort = sort+i;
                    
                    Ps_History pf2 = new Ps_History();
                    pf2.ID = pf.ID + splitstr;
                    pf2.Forecast = type32;
                    pf2.ForecastID = type31;
                    pf2.Title = ffh.Addlist[i].ToString();
                   
                    if (pf.Title.Contains("����"))
                        pf2.Title=pf2.Title.Replace("����", "����");
                        //pf2.Title.Replace("����", "����");
                    else
                        if (pf.Title.Contains("����"))
                        {
                            pf.Title = pf.Title.Replace("����", "����");
                                                   
                        }
                    if (pf.Title.Contains("�õ���"))
                        pf2.Title = pf2.Title.Replace("�õ���", "����");
                      //��ͬʱ��������Ϊ1
                    pf2.ParentID = pf.ParentID + splitstr;
                    pf2.Col4 = ProjectUID;
                    pf2.Sort = sort + i;
                    if (pf.Title.Contains("ͬʱ��"))
                    {
                        pf.Sort = 0;
                        pf2.Sort = 0;
                        for (int j = 1990; j <= 2060; j++)
                        {
                            string y = "y" + j.ToString();
                            pf2.GetType().GetProperty(y).SetValue(pf2, 1.0, null);
                            pf.GetType().GetProperty(y).SetValue(pf, 1.0, null);
                        }
                    }
                    try
                    {

                        Services.BaseService.Create<Ps_History>(pf);
                        Services.BaseService.Create<Ps_History>(pf2);
                        treeList1.BeginInit();

                        dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf, dataTable.NewRow()));
                        treeList1.EndInit();
                        treeList2.BeginInit();
                        dataTable2.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf2, dataTable2.NewRow()));
                        treeList2.EndInit();
                    }
                    catch (Exception ex) { MsgBox.Show("���ӷ������" + ex.Message); }
                }
            }
        }
        private void insertNode(TreeListNode pnode,Ps_History obj)
        {
            
        }
        private string createID() {
            string str =Guid.NewGuid().ToString();
            return ProjectUID + "_" + str.Substring(str.Length -12);
        }
        //ɾ�����
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln==null)
	        {
                return;
	        }
            string IDstr=tln.GetValue("ID").ToString();
            IDstr+=splitstr;
            TreeListNode tln2=treeList2.FindNodeByKeyID(IDstr);
            if (tln == null)
                return;
            if (tln.HasChildren)
            {
                if (MessageBox.Show( tln.GetValue("Title").ToString() + " ������ӽ�㣬ɾ������ͬ�ӽ��һͬɾ����","ѯ��", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DeleteNode(tln);
                    DeleteNode(tln2);
                }
            }
            else
            {
                if (MsgBox.ShowYesNo("�Ƿ�ɾ������ " + tln.GetValue("Title") + "��") == DialogResult.Yes)
                {
                    DeleteNode(tln);
                    DeleteNode(tln2);
                }
              
            }

            
        }
        public void DeleteNode(TreeListNode tln)
        {
            if (tln==null)
            {
                return;
            }
            if (tln.HasChildren)
            {
                for (int i = 0; i < tln.Nodes.Count; i++)
                {
                    DeleteNode(tln.Nodes[i]);
                }
                DeleteNode(tln);
            }
            else
            {
                Ps_History pf = new Ps_History();
                pf.ID = tln["ID"].ToString();
                try
                {
                    //TreeListNode node = tln.TreeList.FindNodeByKeyID(pf.ID);
                    //if (node != null)
                    DataTable tempdt = (DataTable)tln.TreeList.DataSource;
                    tln.TreeList.DeleteNode(tln);
                    RemoveDataTableRow(tempdt, pf.ID);
                    Common.Services.BaseService.Delete<Ps_History>(pf);
                   
                }
                catch (Exception e)
                {

                    MessageBox.Show(e.Message + "ɾ��������");
                }
               
            }
           
        }
        private  void RemoveDataTableRow(DataTable dt, string ID)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["ID"].ToString() == ID)
                {
                    dt.Rows.RemoveAt(i);
                    break;
                }
            }
        }
        /// <summary>
        /// �޸����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (treeList1.FocusedNode == null)
            {
                return;
            }
            if (treeList1.FocusedNode.GetValue("Col10").ToString() == "1")
            {
                MessageBox.Show("Ĭ��������޸ģ�");
                return;
            }
            if (treeList1.FocusedNode.ParentNode != null&&treeList1.FocusedNode.ParentNode["Title"].ToString().Contains("�����û�"))
            {
                editHistory();
                return;
            }
            if (treeList1.FocusedNode.ParentNode != null&&treeList1.FocusedNode.ParentNode["Title"].ToString().Contains("�����û�"))
            {
                editHistory2();
                return;
            }


            FormFqHistoryDF_add frm = new FormFqHistoryDF_add();
            frm.TypeTitle = treeList1.FocusedNode.GetValue("Title").ToString();
            frm.Text = "�޸ķ���";
            Ps_History v = new Ps_History();
            v = Services.BaseService.GetOneByKey<Ps_History>(treeList1.FocusedNode["ID"].ToString());
            if (frm.ShowDialog() == DialogResult.OK)
            {      
                v.Title = frm.TypeTitle;
                Ps_History v2 = Services.BaseService.GetOneByKey<Ps_History>(v.ID + splitstr);
                 if (v.Title.Contains("����"))
                 {
                      v2.Title = v.Title.Replace("����", "����");
                      if (v.Title.Contains("�õ���"))
                      {
                          v2.Title = v.Title.Replace("�õ���", "����");
                      }
                 }
                else
                 {
                     v2.Title = v.Title;
                 }

                try {
                    
                    try {treeList1.FocusedNode.SetValue("Title", v.Title);} catch { }
                    TreeListNode node = treeList2.FindNodeByKeyID(v2.ID);
                    try { node["Title"] = v2.Title; } catch { }
                    Common.Services.BaseService.Update<Ps_History>(v);
                    Common.Services.BaseService.Update<Ps_History>(v2);
                } catch (Exception ex) { MsgBox.Show("�޸ķ������" + ex.Message); }

            }
          
        }
        #endregion
        static public void TreeNodeToDataObject<T>(T dataObject, DevExpress.XtraTreeList.Nodes.TreeListNode treeNode) {
            Type type = typeof(T);
            foreach (PropertyInfo pi in type.GetProperties()) {
                if (pi.Name.Substring(0, 1) == "y") {
                    object obj =treeNode.GetValue(pi.Name);
                    try {
                        pi.SetValue(dataObject, obj != DBNull.Value ? obj : 0, null);
                    } catch (Exception err) { MessageBox.Show(err.Message); }
                }
                //if (pi.Name == "COL10" || pi.Name == "y1991" || pi.Name == "y1991") {
                //    pi.SetValue(dataObject, treeNode.GetValue(pi.Name), null);
                //}
            }
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e) {
            if (treeList1.FocusedNode.HasChildren) {
                e.Cancel = true;
            }
            if(!EditRight)
            {
             e.Cancel = true;
            }
        }
        private void treeList2_ShowingEditor(object sender, CancelEventArgs e) {
            if (treeList2.FocusedNode.HasChildren) {
                e.Cancel = true;
            }
            if (!EditRight)
            {
                e.Cancel = true;
            }
        }
        //private void CalculateSum(TreeListNode node, TreeListColumn column) {
            
        //    Ps_History v = Services.BaseService.GetOneByKey<Ps_History>(node["ID"].ToString());
        //    TreeNodeToDataObject<Ps_History>(v, node);
        //    Common.Services.BaseService.Update<Ps_History>(v);
        //    TreeListNode parentNode = node.ParentNode;
        //    if (parentNode == null) {
        //        return;
        //    }
        //    if (node["Title"].ToString() == "ͬʱ��") return;
        //    double sum = 0;
        //    foreach (TreeListNode nd in parentNode.Nodes) {
        //        if (nd["Title"].ToString() == "ͬʱ��") continue;
        //        if (nd.ParentNode.ParentNode == null && !nd.HasChildren)
        //        {
        //            continue;
        //        }
        //        object value = nd.GetValue(column.FieldName);
        //        if (value != null && value != DBNull.Value) {
        //            sum += Convert.ToDouble(value);
        //        }
        //    }
        //    if (sum != 0) {
        //        parentNode.SetValue(column.FieldName, sum);
        //        v = Services.BaseService.GetOneByKey<Ps_History>(parentNode["ID"].ToString());
        //        TreeNodeToDataObject<Ps_History>(v, parentNode);

        //        Common.Services.BaseService.Update<Ps_History>(v);
        //    } else
        //        parentNode.SetValue(column.FieldName, null);
        //    CalculateSum(parentNode, column);
        //}
        
        private void sumDL(TreeListNode parentNode, int byear, int endyear)
        {

            if (parentNode == null || byear >= endyear) return;
            DataRow row = (parentNode.TreeList.GetDataRecordByNode(parentNode) as DataRowView).Row;

            for (int i = byear; i <= endyear; i++)
            {
                string fname = "y" + i;
                double sum = 0;
                foreach (TreeListNode nd in parentNode.Nodes)
                {
                    object value = nd.GetValue(fname);
                    if (value != null && value != DBNull.Value)
                    {
                        sum += Convert.ToDouble(value);
                    }
                }
                row[fname] = sum;
            }
            Ps_History v = DataConverter.RowToObject<Ps_History>(row);
            Common.Services.BaseService.Update<Ps_History>(v);
            sumDL(parentNode.ParentNode, byear, endyear);
        }
        //��һ��������
        private void CalaculateSumnew(DataRow row,string field,DataTable dat)
        {
           // DataRow row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;
            Ps_History hs = DataConverter.RowToObject<Ps_History>(row);
            //Common.Services.BaseService.Update<Ps_History>(hs);
           
        
           
             string parentid = hs.ParentID;
             DataRow[] parentNode = dat.Select("ID='" + parentid + "'");
             
            if (string.IsNullOrEmpty(parentid))
            {
                if (hs.Title.Contains("���غϼ�"))
                {
                    DataRow[] drlist = dat.Select("Title like '%ȫ���%'");
                    double value = 0;
                    foreach (DataRow dr in drlist)
                    {
                        if (dr["Title"].ToString().Contains("ȫ����"))
                        {
                            int i = dat.Rows.IndexOf(dr);
                            dat.Rows[i][field] =hs.GetType().GetProperty(field).GetValue(hs,null);
                          Ps_History  v = DataConverter.RowToObject<Ps_History>(dat.Rows[i]);


                            Common.Services.BaseService.Update<Ps_History>(v);
                        }


                    }
                }
                return;
            }

            DataRow[] drlist1 = dat.Select("ParentID='"+parentid+"'");
            double sum = 0;
            bool TSL_falg = false;
            double Tsl_double = 0;
            foreach (DataRow nd in drlist1)
            {
                DataRow[] childrennode = dat.Select("ParentID='" + nd["ID"].ToString() + "'");
                if (nd["Title"].ToString() == "ͬʱ��")
                {
                    //��¼ͬʱ��
                    if (Convert.ToDouble(nd[field].ToString()) != 0)
                    {
                        TSL_falg = true;
                        Tsl_double = Convert.ToDouble(nd[field].ToString());
                    }
                    continue;
                }
                if (parentNode[0]["Title"].ToString().Contains("�ϼ�") && childrennode.Length == 0) continue;
                object value = nd[field].ToString();
                if (value != null && value != DBNull.Value)
                {
                    sum += Convert.ToDouble(value);
                }
            }
            if (sum != 0)
            {
                //���ͬʱ�ʲ�Ϊ0�������ͬʱ��
                if (TSL_falg)
                {
                    sum = sum * Tsl_double;
                }
                parentNode[0][field] = sum;
                Ps_History v = DataConverter.RowToObject<Ps_History>(parentNode[0]);
                //row = (parentNode.TreeList.GetDataRecordByNode(parentNode) as DataRowView).Row;
                // v = DataConverter.RowToObject<Ps_History>(row);

                //v = Services.BaseService.GetOneByKey<Ps_History>(parentNode["ID"].ToString());
                //TreeNodeToDataObject<Ps_History>(v, parentNode);

                Common.Services.BaseService.Update<Ps_History>(v);
            }
            //else
            //    //parentNode.SetValue(column.FieldName, null);
            //    parentNode[0][field] = 0;
                //parentNode.SetValue(column.FieldName, 0);
            //Ps_History vparent = DataConverter.RowToObject<Ps_History>(parentNode[0]);
            CalaculateSumnew(parentNode[0], field, dat);

            ///////
            //TreeListNode parentNode = node.ParentNode;
          
        }
        //����ʼ�����ֹ�궼���м��㣻
        private void CalaculateSumnew1(DataRow row, DataTable dat)
        {
            // DataRow row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;
            Ps_History hs = DataConverter.RowToObject<Ps_History>(row);
            //Common.Services.BaseService.Update<Ps_History>(hs);
            string parentid = hs.ParentID;
            DataRow[] parentNode = dat.Select("ID='" + parentid + "'");
            for (int j = firstyear; j <= endyear; j++)
            {
                string field = "y" + j.ToString();
              

                if (string.IsNullOrEmpty(parentid))
                {
                    if (hs.Title.Contains("���غϼ�"))
                    {
                        DataRow[] drlist = dat.Select("Title like '%ȫ���%'");
                        double value = 0;
                        foreach (DataRow dr in drlist)
                        {
                            if (dr["Title"].ToString().Contains("ȫ����"))
                            {
                                int i = dat.Rows.IndexOf(dr);
                                dat.Rows[i][field] = hs.GetType().GetProperty(field).GetValue(hs, null);
                                Ps_History v = DataConverter.RowToObject<Ps_History>(dat.Rows[i]);


                                Common.Services.BaseService.Update<Ps_History>(v);
                            }


                        }
                    }
                    return;
                }

                DataRow[] drlist1 = dat.Select("ParentID='" + parentid + "'");
                double sum = 0;
                bool TSL_falg = false;
                double Tsl_double = 0;
                foreach (DataRow nd in drlist1)
                {
                    DataRow[] childrennode = dat.Select("ParentID='" + nd["ID"].ToString() + "'");
                    if (nd["Title"].ToString() == "ͬʱ��")
                    {
                        //��¼ͬʱ��
                        if (Convert.ToDouble(nd[field].ToString()) != 0)
                        {
                            TSL_falg = true;
                            Tsl_double = Convert.ToDouble(nd[field].ToString());
                        }
                        continue;
                    }
                    if (parentNode[0]["Title"].ToString().Contains("�ϼ�") && childrennode.Length == 0) continue;
                    object value = nd[field].ToString();
                    if (value != null && value != DBNull.Value)
                    {
                        sum += Convert.ToDouble(value);
                    }
                }
                if (sum != 0)
                {
                    //���ͬʱ�ʲ�Ϊ0�������ͬʱ��
                    if (TSL_falg)
                    {
                        sum = sum * Tsl_double;
                    }
                    parentNode[0][field] = sum;
                    Ps_History v = DataConverter.RowToObject<Ps_History>(parentNode[0]);
                    //row = (parentNode.TreeList.GetDataRecordByNode(parentNode) as DataRowView).Row;
                    // v = DataConverter.RowToObject<Ps_History>(row);

                    //v = Services.BaseService.GetOneByKey<Ps_History>(parentNode["ID"].ToString());
                    //TreeNodeToDataObject<Ps_History>(v, parentNode);

                    Common.Services.BaseService.Update<Ps_History>(v);
                }
                //else
                //    //parentNode.SetValue(column.FieldName, null);
                //    parentNode[0][field] = 0;
                //parentNode.SetValue(column.FieldName, 0);
                //Ps_History vparent = DataConverter.RowToObject<Ps_History>(parentNode[0]);
            }

           
            CalaculateSumnew1(parentNode[0], dat);

            ///////
            //TreeListNode parentNode = node.ParentNode;

        }
        private void CalculateSum(TreeListNode node, TreeListColumn column)
        {
            
            //Ps_Forecast_Math v = Services.BaseService.GetOneByKey<Ps_Forecast_Math>(node["ID"].ToString());
            //TreeNodeToDataObject<Ps_Forecast_Math>(v, node);
            DataRow row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;
            Ps_History v = DataConverter.RowToObject<Ps_History>(row);
            Common.Services.BaseService.Update<Ps_History>(v);
            TreeListNode parentNode = node.ParentNode;
            if (parentNode == null)
            {
                if (node["Title"].ToString().Contains("���غϼ�"))
                {
                    DataRow[] drlist = dataTable.Select("Title like '%ȫ���%'");
                    double value = 0;
                    foreach (DataRow dr in drlist)
                    {
                        if(dr["Title"].ToString().Contains("ȫ����"))
                        {
                         int i = dataTable.Rows.IndexOf(dr);
                            dataTable.Rows[i][column.FieldName]=node[column.FieldName];
                            v = DataConverter.RowToObject<Ps_History>(dataTable.Rows[i]);


                            Common.Services.BaseService.Update<Ps_History>(v);
                        }
                    
                    
                    }
                }
                return;
            }
            //if (node["Title"].ToString() == "ͬʱ��") return;

            double sum = 0;
            bool TSL_falg = false;
            double Tsl_double = 0;
            foreach (TreeListNode nd in parentNode.Nodes)
            {
                if (nd["Title"].ToString() == "ͬʱ��")
                {
                    //��¼ͬʱ��
                    if (Convert.ToDouble(nd[column].ToString())!=0)
                    {
                        TSL_falg = true;
                        Tsl_double = Convert.ToDouble(nd[column].ToString());
                    }
                    continue;
                }
                if (parentNode["Title"].ToString().Contains("�ϼ�") && !nd.HasChildren) continue;
                object value = nd.GetValue(column.FieldName);
                if (value != null && value != DBNull.Value)
                {
                    sum += Convert.ToDouble(value);
                }
            }
            if (sum != 0)
            {
                //���ͬʱ�ʲ�Ϊ0�������ͬʱ��
                if (TSL_falg)
                {
                    sum = sum * Tsl_double;
                }
                parentNode.SetValue(column.FieldName, sum);
                row = (parentNode.TreeList.GetDataRecordByNode(parentNode) as DataRowView).Row;
                v = DataConverter.RowToObject<Ps_History>(row);

                //v = Services.BaseService.GetOneByKey<Ps_History>(parentNode["ID"].ToString());
                //TreeNodeToDataObject<Ps_History>(v, parentNode);

                Common.Services.BaseService.Update<Ps_History>(v);
            }
            else
                //parentNode.SetValue(column.FieldName, null);
                parentNode.SetValue(column.FieldName, 0);
            CalculateSum(parentNode, column);
        }
       //�޸ĸ��ɲ���ȵ�ԭ��
        private void CalculateSum3(TreeListNode node, TreeListColumn column)
        {
            DataRow row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;
            Ps_History v = DataConverter.RowToObject<Ps_History>(row);
            Common.Services.BaseService.Update<Ps_History>(v);
            TreeListNode parentNode = node.ParentNode;
            if (parentNode == null)
            {
                if (node["Title"].ToString().Contains("���غϼ�"))
                {
                    DataRow[] drlist = dataTable2.Select("Title like '%ȫ���%'");
                    double value = 0;
                    foreach (DataRow dr in drlist)
                    {
                        if (dr["Title"].ToString().Contains("ȫ����"))
                        {
                            int i = dataTable2.Rows.IndexOf(dr);
                            dataTable2.Rows[i][column.FieldName] = node[column.FieldName];
                            v = DataConverter.RowToObject<Ps_History>(dataTable2.Rows[i]);


                            Common.Services.BaseService.Update<Ps_History>(v);
                        }


                    }
                }
                return;
            }
            //if (node["Title"].ToString() == "ͬʱ��") return;

            double sum = 0;
            bool TSL_falg = false;
            double Tsl_double = 0;
            foreach (TreeListNode nd in parentNode.Nodes)
            {
                if (nd["Title"].ToString() == "ͬʱ��")
                {
                    //��¼ͬʱ��
                    if (Convert.ToDouble(nd[column].ToString()) != 0)
                    {
                        TSL_falg = true;
                        Tsl_double = Convert.ToDouble(nd[column].ToString());
                    }
                    continue;
                }
                if (parentNode["Title"].ToString().Contains("�ϼ�") && !nd.HasChildren) continue;
                object value = nd.GetValue(column.FieldName);
                if (value != null && value != DBNull.Value)
                {
                    sum += Convert.ToDouble(value);
                }
            }
            if (sum != 0)
            {
                //���ͬʱ�ʲ�Ϊ0�������ͬʱ��
                if (TSL_falg)
                {
                    sum = sum * Tsl_double;
                }
                parentNode.SetValue(column.FieldName, sum);
                row = (parentNode.TreeList.GetDataRecordByNode(parentNode) as DataRowView).Row;
                v = DataConverter.RowToObject<Ps_History>(row);

                //v = Services.BaseService.GetOneByKey<Ps_History>(parentNode["ID"].ToString());
                //TreeNodeToDataObject<Ps_History>(v, parentNode);

                Common.Services.BaseService.Update<Ps_History>(v);
            }
            else
                //parentNode.SetValue(column.FieldName, null);
                parentNode.SetValue(column.FieldName, 0);
            CalculateSum3(parentNode, column);
        }
        private void CalculateSum2(TreeListNode node, TreeListColumn column)
        {
            //Ps_Forecast_Math v = Services.BaseService.GetOneByKey<Ps_History>(node["ID"].ToString());
            //TreeNodeToDataObject<Ps_History>(v, node);
            DataRow row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;
            Ps_History v = DataConverter.RowToObject<Ps_History>(row);
            Common.Services.BaseService.Update<Ps_History>(v);
            TreeListNode parentNode = node.ParentNode;
            if (parentNode == null)
            {
                if (node["Title"].ToString().Contains("���غϼ�"))
                {
                    DataRow[] drlist = dataTable2.Select("Title like '%ȫ���%'");
                    double value = 0;
                    foreach (DataRow dr in drlist)
                    {
                        if (dr["Title"].ToString().Contains("ȫ����"))
                        {
                            int i = dataTable2.Rows.IndexOf(dr);
                            dataTable2.Rows[i][column.FieldName] = node[column.FieldName];
                            v = DataConverter.RowToObject<Ps_History>(dataTable2.Rows[i]);


                            Common.Services.BaseService.Update<Ps_Forecast_Math>(v);
                        }


                    }
                }
                return;
            }
            //if (node["Title"].ToString() == "ͬʱ��") return;
            //if (node["Title"].ToString() == "ͬʱ��") 
            //{ 


            //}
            double sum = 0;
            double valuetemp = 0;
            bool istsl = false;
            foreach (TreeListNode nd in parentNode.Nodes)
            {

                object value = null;
                if (nd["Title"].ToString() == "ͬʱ��")
                {
                    istsl = true;
                    value = nd.GetValue(column.FieldName);
                    if (value != null && value != DBNull.Value)
                    {
                        valuetemp = Convert.ToDouble(value);
                        continue;
                    }

                }
                if (!nd.HasChildren && istsl && parentNode["Title"].ToString().Contains("�ϼ�"))
                    continue;
                value = nd.GetValue(column.FieldName);
                if (value != null && value != DBNull.Value)
                {
                    if (istsl)
                        sum += valuetemp * Convert.ToDouble(value);
                    else
                        sum += Convert.ToDouble(value);
                }
            }
            if (sum != 0)
            {
                parentNode.SetValue(column.FieldName, sum);
                row = (parentNode.TreeList.GetDataRecordByNode(parentNode) as DataRowView).Row;
                v = DataConverter.RowToObject<Ps_History>(row);

                //v = Services.BaseService.GetOneByKey<Ps_History>(parentNode["ID"].ToString());
                //TreeNodeToDataObject<Ps_History>(v, parentNode);

                Common.Services.BaseService.Update<Ps_History>(v);
            }
            else
               // parentNode.SetValue(column.FieldName, null);
                parentNode.SetValue(column.FieldName, 0);
            CalculateSum2(parentNode, column);
        }
     
        private void reloadTree2() {
            dataTable2.Clear();
            load2();
        }
        private void treeList1_AfterDragNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e) {
            string id = e.Node["ID"].ToString();

            Ps_History v = Services.BaseService.GetOneByKey<Ps_History>(id);
            if (v != null) {
                v.ParentID = e.Node["ParentID"].ToString();
                Services.BaseService.Update<Ps_History>(v);
                v = Services.BaseService.GetOneByKey<Ps_History>(id + splitstr);
                if (v != null) {
                    v.ParentID = e.Node["ParentID"].ToString() + splitstr;
                    Services.BaseService.Update<Ps_History>(v);
                    reloadTree2();
                }
            }
        }
        #region ͳ�Ʊ�
        /// <summary>
        /// ���⽨��Ҫ��ҵ��Ŀ���õ����������
        /// </summary>
        private void tj431() {

            Formtj432 f = new Formtj432();
            f.Text = "ȫ���⽨��Ҫ��ҵ��Ŀ���õ����������";
            f.CanPrint = base.EditRight;
            
            setcols431();
            f.GridDataTable = ConvertTreeListToDataTable431(treeList1);
            f.ShowDialog();
            //}
        }
        private void tj511() {

            Formtj432 f = new Formtj432();
            f.Text = treeList1.FocusedNode["Title"].ToString() + "�ص㽨�蹤ҵ���õ������";
            f.CanPrint = base.EditRight;

            setcols431();
            showtable.Columns["Col2"].Caption = "��Ŀ����";
            f.GridDataTable = ConvertTreeListToDataTable511(treeList1);
            f.ShowDialog();
            //}
        }
        /// <summary>
        /// ���д�ҵ�û����ܼ��õ����
        /// </summary>
        private void tj432() {
            
            Formtj432 f = new Formtj432();
            f.Text = "���д�ҵ�û����ܼ��õ����";
            f.CanPrint = base.EditRight;
            List<int> list = new List<int>();
            list.Add(endyear);
            setcols432(list);
            f.GridDataTable = ConvertTreeListToDataTable(treeList1);
            f.ShowDialog();
            //}
        }
        /// <summary>
        /// ȫ����������ģ�õ������
        /// </summary>
        private void tj433() {
            FormChooseYears frm = new FormChooseYears();
            foreach (TreeListColumn column in treeList1.Columns) {
                if (column.Caption.IndexOf("��") > 0) {
                    frm.ListYearsForChoose.Add((int)column.Tag);
                }
            }
            if (frm.ShowDialog() == DialogResult.OK) {
                Formtj432 f = new Formtj432();
                f.Text = "ȫ����������ģ�õ������";
                f.CanPrint = base.EditRight;
                setcols433(frm.GetChoosedYear());
                f.GridDataTable = ConvertTreeListToDataTable433(treeList1);
                DialogResult dr = f.ShowDialog();
            }

        }
        /// <summary>
        /// ��������ȫ����õ������
        /// </summary>
        private void tj532() {
            FormChooseYears frm = new FormChooseYears();
            foreach (TreeListColumn column in treeList1.Columns) {
                if (column.Caption.IndexOf("��") > 0) {
                    frm.ListYearsForChoose.Add((int)column.Tag);
                }
            }
            if (frm.ShowDialog() == DialogResult.OK) {
                Formtj432 f = new Formtj432();
                f.Text = treeList1.FocusedNode["Title"].ToString()+ "����ȫ����õ������";
                f.CanPrint = base.EditRight;
                setcols433(frm.GetChoosedYear());
                f.GridDataTable = ConvertTreeListToDataTable532(treeList1);
                DialogResult dr = f.ShowDialog();
            }

        }

        private DataTable ConvertTreeListToDataTable532(DevExpress.XtraTreeList.TreeList treeList1) {

            TreeListNode node = treeList1.FocusedNode;
            DataRow row1 = ((DataRowView)node.TreeList.GetDataRecordByNode(node)).Row;
            DataRow newrow = createRow(showtable, row1);
            newrow["Title"] = "ȫ����õ���";
            setFh(newrow, node["ID"].ToString());
            AddNodeDataToDataTable433(showtable, node, cols, "");
            
            #region ����������
            DataTable dtReturn = showtable;
            DataColumn columnBegin = null;
            foreach (DataColumn column in dtReturn.Columns) {
                if (column.Caption.IndexOf("��") > 0) {
                    if (columnBegin == null) {
                        columnBegin = column;
                    }
                } else if (column.ColumnName.IndexOf("������") > 0) {
                    DataColumn columnEnd = dtReturn.Columns[column.Ordinal - 2];

                    foreach (DataRow row in dtReturn.Rows) {
                        object numerator = row[columnEnd];
                        object denominator = row[columnBegin];

                        if (numerator != DBNull.Value && denominator != DBNull.Value) {
                            try {
                                int intervalYears = Convert.ToInt32(columnEnd.ColumnName.Replace("y", ""))
                                    - Convert.ToInt32(columnBegin.ColumnName.Replace("y", ""));
                                object obj = Math.Round(Math.Pow((double)numerator / (double)denominator, 1.0 / intervalYears) - 1, 4);
                                row[column] = obj;
                                if (double.IsNaN((double)row[column]) || double.IsInfinity((double)row[column]))
                                    row[column] = 0;
                            } catch {
                                row[column] = 0;
                            }
                        }
                    }

                    //���μ��������ʵ�����Ϊ�´ε���ʼ��

                    columnBegin = columnEnd;
                }
            }
            #endregion
            return showtable;
        }
        private void AddNodeDataToDataTable433(DataTable dt, TreeListNode node, List<string> listColID,string padstr) {
            padstr += "��";
            foreach (TreeListNode nd in node.Nodes) {
                if (nd["Title"].ToString().Contains("����")) continue;
                DataRow newRow = dt.NewRow();            
                string space = string.Copy(padstr);
                foreach (string colID in listColID) {
                    //���������ڶ��㼰�Ժ���ǰ��ӿո�
                    if (colID == "Title" && nd.ParentNode != null) {
                        newRow[colID] = space + nd[colID];
                    } else {
                        newRow[colID] = nd[colID];
                    }
                }
                setFh(newRow, nd["ID"].ToString());//����
                dt.Rows.Add(newRow);                
            
                AddNodeDataToDataTable433(dt, nd, listColID,padstr);
            }
        }
        
        private void createHj433(TreeListNode node ,IList<Ps_History> list1,IList<Ps_History> list2) {
            DataRow newrow = showtable.NewRow();
            string title =node["Title"].ToString();
            Ps_History ps1 =null;//����
            Ps_History ps2 = null;//����
            foreach (Ps_History ps in list1) {
                if (ps.Title == title) {
                    ps1 = ps;
                    break;
                }
            }
            if (ps1 == null) return;
            foreach (Ps_History ps in list2) {
                if (ps.Title == title) {
                    ps2 = ps;
                    break;
                }
            }
            if (ps1 != null) {
                newrow = Itop.Common.DataConverter.ObjectToRow(ps1, newrow);
                newrow =createRow(showtable, newrow);
            }
            if (ps2 != null) {
                DataRow row = showtable.NewRow();
                row = Itop.Common.DataConverter.ObjectToRow(ps2, row);
                setData2(newrow, row);
            }
            if (title == "�����õ���") {
                foreach (TreeListNode n1 in node.Nodes) {
                    createHj433(n1, list1, list2);
                }
            }

        }
        private DataTable ConvertTreeListToDataTable433(DevExpress.XtraTreeList.TreeList treeList1) {
            IList<Ps_History> list =Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryGroupList",where1);
            IList<Ps_History> list2 =Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryGroupList",where2);
            
            foreach (TreeListNode node in treeList1.Nodes) {
                DataRow row = ((DataRowView)node.TreeList.GetDataRecordByNode(node)).Row;
                DataRow newrow =createRow(showtable, row);
                setFh(newrow, row["ID"].ToString());
                newrow["Title"] = "ȫ����õ���";
                bool flag = true;
                foreach (TreeListNode node2 in node.Nodes) {//����
                    if (flag) {
                        flag = false;
                        foreach (TreeListNode node3 in node2.Nodes) {//
                            if (node3["Title"].ToString().Contains("ͬʱ��")) {
                                DataRow row2 = ((DataRowView)node3.TreeList.GetDataRecordByNode(node3)).Row;
                                DataRow newrow2 = createRow(showtable, row2);
                                setFh(newrow2, row2["ID"].ToString());
                            } else {
                                createHj433(node3, list, list2);
                                if (node3["Title"].ToString().Contains("�û�"))
                                    AddNodeDataToDataTable433(showtable, node3, cols, "");
                            }
                        }
                    } else {
                        foreach (TreeListNode node4 in node2.Nodes) {
                            if (node4["Title"].ToString().Contains("�û�"))
                            AddNodeDataToDataTable433(showtable, node4, cols, "");
                        }
                    }
                    DataRow row22 = findrow(showtable, " title like '%����%' ","");
                    if (row22 != null)
                        row22["Title"] = "��"+node2["Title"];
                }
                
            }
            #region ����������
            DataTable dtReturn = showtable;
            DataColumn columnBegin = null;
            foreach (DataColumn column in dtReturn.Columns) {
                if (column.Caption.IndexOf("��") > 0) {
                    if (columnBegin == null) {
                        columnBegin = column;
                    }
                } else if (column.ColumnName.IndexOf("������") > 0) {
                    DataColumn columnEnd = dtReturn.Columns[column.Ordinal - 2];

                    foreach (DataRow row in dtReturn.Rows) {
                        object numerator = row[columnEnd];
                        object denominator = row[columnBegin];
                        
                        if (numerator != DBNull.Value && denominator != DBNull.Value) {
                            try {
                                int intervalYears = Convert.ToInt32(columnEnd.ColumnName.Replace("y", ""))
                                    - Convert.ToInt32(columnBegin.ColumnName.Replace("y", ""));
                                object obj = Math.Round(Math.Pow((double)numerator / (double)denominator, 1.0 / intervalYears) - 1, 4);
                                row[column] =obj ;
                                if (double.IsNaN((double)row[column])  || double.IsInfinity((double)row[column]))
                                    row[column] = 0;
                                } catch {
                                row[column] = 0;
                            }
                        }
                    }

                    //���μ��������ʵ�����Ϊ�´ε���ʼ��

                    columnBegin = columnEnd;
                }
            }
            #endregion
            return showtable;
        }
        private void setcols433(List<int> list) {
            showtable = new DataTable();

            cols.Clear();
            colyears.Clear();
            //showtable.Columns.Add("Col1", typeof(string)).Caption = "���";

            //cols.Add("Col1");
            showtable.Columns.Add("Title", typeof(string)).Caption = "��Ŀ����";
            cols.Add("Title");
            string col = "";
            int lastyear = 0;
            foreach(int year in list) {
                lastyear = year;
                col = "y" + year;
                cols.Add(col);
                
                showtable.Columns.Add(col, typeof(double)).Caption = year + "�����";
                
            }
            if (col != "") {//���һ��Ӹ�����
                colyears.Add(col);
                showtable.Columns.Add(col + "_", typeof(double)).Caption = lastyear + "�긺��";
            }

            showtable.Columns.Add("_������", typeof(double)).Caption = "������";
        }
        private DataRow findrow(DataTable table, string filter,string sort) {
            DataRow[] rows = table.Select(filter, sort);
            if (rows != null && rows.Length > 0) {

                return rows[0];
            }
            return null;

        }
        private DataRow copyrow(DataTable dt, DataRow row) {
            DataRow newrow = dt.NewRow();
            newrow.ItemArray = row.ItemArray.Clone() as object[];
            return newrow;
        }
        List<string> cols = new List<string>();//������
        List<string> colyears = new List<string>();//������
        List<string> colsum = new List<string>();//�ϼ���
        private DataTable showtable = new DataTable();
        private void setcols431()
        {
            showtable = new DataTable();

            cols.Clear();
            colyears.Clear();
            colsum.Clear();
            showtable.Columns.Add("Col1", typeof(string)).Caption = "���";

            cols.Add("Col1");
            showtable.Columns.Add("Title", typeof(string)).Caption = "��ҵ(��Ŀ)����";

            cols.Add("Title");
            showtable.Columns.Add("Col2", typeof(string)).Caption = "�����ģ������";
            cols.Add("Col2");
           
            showtable.Columns.Add("Col7", typeof(string)).Caption = "�ƻ���������";
            cols.Add("Col7");
            showtable.Columns.Add("Col6", typeof(string)).Caption = "������չ";
            cols.Add("Col6");
            DataColumn dcol = showtable.Columns.Add("y1990", typeof(double));

            dcol.Caption = "�õ���";
            //dcol = showtable.Columns.Add("y1990_sum", typeof(double));
            //dcol.Caption = "�����õ���";
            //dcol.Expression = "IIF(Title='С��',sum(y1990),y1990)";

            cols.Add("y1990");
            showtable.Columns.Add("y1991", typeof(double)).Caption = "����";
            cols.Add("y1991");
            
            colsum.Add("y1990");
            colsum.Add("y1991");
            
        }
        private void setcols432(List<int> list) {
            showtable =new DataTable();
            
            cols.Clear();
            colyears.Clear();
            colsum.Clear();
            showtable.Columns.Add("Col1", typeof(string)).Caption = "���";

            cols.Add("Col1");
            showtable.Columns.Add("Title",typeof(string)).Caption="�û���";

            cols.Add("Title");
            showtable.Columns.Add("Col2", typeof(string)).Caption = "��Ʒ��������";
            cols.Add("Col2");
            DataColumn dcol= showtable.Columns.Add("y1990", typeof(double));

            dcol.Caption = "�����õ���";
            //dcol = showtable.Columns.Add("y1990_sum", typeof(double));
            //dcol.Caption = "�����õ���";
            //dcol.Expression = "IIF(Title='С��',sum(y1990),y1990)";

            cols.Add("y1990");
            showtable.Columns.Add("y1991", typeof(double)).Caption = "��������";
            cols.Add("y1991");
            showtable.Columns.Add("Col11_Col12", typeof(double),"CONVERT(IIF(y1991=0,0,(y1990/y1991)*10000),'System.Int32')" ).Caption = "����Сʱ";
            colsum.Add("y1990");
            colsum.Add("y1991");

            if (list.Count > 0) {
                string col = "y" + list[0];
                cols.Add(col);
                colyears.Add(col);
                colsum.Add(col);
                colsum.Add(col+"_");
                showtable.Columns.Add(col, typeof(double)).Caption = list[0]+"�����";
                showtable.Columns.Add(col+"_", typeof(double)).Caption = list[0] + "�긺��";
                string expression = string.Format("CONVERT(IIF({1}=0,0,({0}/{1})*10000),'System.Int32')", col, col + "_");
                showtable.Columns.Add(col+"_p", typeof(double), expression).Caption = "����Сʱ";

            }
        }
        private DataRow createRow(DataTable dt, DataRow row) {
            DataRow newrow = dt.NewRow();
            foreach (string col in cols) {
                if (dt.Columns[col].DataType == dataTable.Columns[col].DataType)
                    newrow[col] = row[col];
                else
                    newrow[col] = double.Parse(row[col].ToString());
            }
            dt.Rows.Add(newrow);
            return newrow;
        }
        private DataRow setData1(DataRow newrow, DataRow row) {           
            foreach (string col in cols) {
                if (showtable.Columns[col].DataType == dataTable.Columns[col].DataType)
                    newrow[col] = row[col];
                else
                    newrow[col] = double.Parse(row[col].ToString());
            }
            return newrow;
        }
        /// <summary>
        /// row2�ĸ��ɵ�newrow
        /// </summary>
        /// <param name="row1"></param>
        /// <param name="row2"></param>
        private void setData2(DataRow newrow, DataRow row2) {            
            foreach (string col in colyears) {
                    newrow[col+"_"] = row2[col];
            }
        }
        /// <summary>
        /// ���ø���
        /// </summary>
        /// <param name="newrow"></param>
        /// <param name="row2"></param>
        private void setFh(DataRow newrow, string id) {
            DataRow row22 = findrow(dataTable2, "ID='" + id + splitstr + "'", "");
            if (row22 != null)
                setData2(newrow, row22);
        }
        string dxs = "��һ�����������߰˾�ʮ";
        private string getDX(int num) {
            if (num > 19) return num.ToString();
            string str = num.ToString();
            string ret = "";
            int count = 0;
            for (int i = str.Length - 1; i >=0; i--) {
                string s=str[i].ToString();
                if (count == 0) {
                    ret = dxs[int.Parse(s)].ToString();
                } else if (count == 1) {
                    ret =  "ʮ" + ret;
                }
                count++;
            }
            return ret;
        }
        /// <summary>
        /// ���Ҵ��û��ڵ�
        /// </summary>
        /// <param name="node"></param>
        private TreeListNode  findDYHParent(TreeListNode root) {
            TreeListNode retNode = null;
            foreach (TreeListNode node in root.Nodes) {
                if (node["Title"].ToString().Contains("�û�")) {
                    retNode = node;
                    break;
                }
                retNode = findDYHParent(node);
            }            
            return retNode;
        }
        /// <summary>
        /// ���ɺϼ���
        /// </summary>
        /// <param name="dt"></param>
        private DataRow createSumRow(DataTable dt) {
            DataRow newrow = dt.NewRow();
            foreach (DataRow row in dt.Rows) {
                foreach (string col in colsum) {
                    double d1 = 0;
                    try { d1 = Convert.ToDouble(newrow[col]);} catch { }
                    
                    double d2 = 0;
                    try { d2=Convert.ToDouble(row[col]);} catch { }
                    newrow[col] = d1 + d2;
                }
            }
            return newrow;
        }
        private DataTable ConvertTreeListToDataTable511(DevExpress.XtraTreeList.TreeList treeList1) {
            
            
            //foreach (TreeListNode node in treeList1.Nodes[0].Nodes) {
            DataRow newrow = null;
            TreeListNode pnode = findDYHParent(treeList1.FocusedNode);
            int count2 = 0;//���ش��û�����
            int nt1 = 0;
                if (pnode != null && pnode.HasChildren) {
                    
                    TreeListNode node1 = pnode.Nodes[0];
                    TreeListNode findnode = null;
                    while (node1 != null) {
                        if (node1["Title"].ToString().Contains("����")) {
                            findnode = node1;
                            break;
                        }
                        node1 = node1.NextNode;
                    }
                    if (findnode == null) return showtable;
                    string t1 = "";
                    
                    foreach (TreeListNode node2 in findnode.Nodes) {

                        DataRowView row2 = node2.TreeList.GetDataRecordByNode(node2) as DataRowView;
                        string t2 = row2["Col5"].ToString();
                        if (t2 != t1) {//�·��࿪ʼ
                            count2 = 0;//�����������
                            nt1++;
                            t1 = t2;
                            newrow = showtable.NewRow();
                            newrow["Title"] = t1;
                            newrow["Col1"] = "(" + getDX(nt1) + ")";
                            showtable.Rows.Add(newrow);
                        }
                        count2++;
                        newrow = createRow(showtable, row2.Row);
                        newrow["Col1"] = count2;
                        setFh(newrow, row2["ID"].ToString());
                    }
                }
            //}
            //����С��
            DataRow row33 = createSumRow(showtable);
            row33["Title"] = "�ϼ�";
            row33["Col1"] = "(" + getDX(nt1 + 1) + ")";
            showtable.Rows.Add(row33);
            return showtable;
        }
        private DataTable ConvertTreeListToDataTable431(DevExpress.XtraTreeList.TreeList treeList1) {
            if (treeList1.Nodes.Count == 0) return showtable;
            int count1 = 0;//���ؼ���a
            foreach (TreeListNode nd in treeList1.Nodes)
            {
                if (nd.Nodes.Count < 4)
                    continue;

                foreach (TreeListNode node in nd.Nodes)
                {
                    TreeListNode pnode = findDYHParent(node);
                    if (pnode != null && pnode.HasChildren)
                    {
                        count1++;
                        DataRow newrow = showtable.NewRow();
                        newrow["Title"] = node["Title"];
                        newrow["Col1"] = getDX(count1);
                        showtable.Rows.Add(newrow);
                        int count2 = 0;//���ش��û�����
                        TreeListNode node1 = pnode.Nodes[0];
                        TreeListNode findnode = null;
                        while (node1 != null)
                        {
                            if (node1["Title"].ToString().Contains("����"))
                            {
                                findnode = node1;
                                break;
                            }
                            node1 = node1.NextNode;
                        }
                        if (findnode == null) continue;
                        string t1 = "";
                        int nt1 = 0;
                        foreach (TreeListNode node2 in findnode.Nodes)
                        {

                            DataRowView row2 = node2.TreeList.GetDataRecordByNode(node2) as DataRowView;
                            string t2 = row2["Col5"].ToString();
                            if (t2 != t1)
                            {//�·��࿪ʼ
                                count2 = 0;//�����������
                                nt1++;
                                t1 = t2;
                                newrow = showtable.NewRow();
                                newrow["Title"] = t1;
                                newrow["Col1"] = "(" + getDX(nt1) + ")";
                                showtable.Rows.Add(newrow);
                            }
                            count2++;
                            newrow = createRow(showtable, row2.Row);
                            newrow["Col1"] = count2;
                            setFh(newrow, row2["ID"].ToString());
                        }
                    }
                }
            }
            
            //����С��
            DataRow row33 = createSumRow(showtable);
            row33["Title"] = "�ϼ�";
            row33["Col1"] = getDX(count1 + 1);
            showtable.Rows.Add(row33);
            return showtable;
        }
      //private DataTable ConvertTreeListToDataTable(DevExpress.XtraTreeList.TreeList treeList1) {
      //      if(treeList1.Nodes.Count==0)return showtable;
      //      int count1 = 0;//���ؼ���
         
      //      foreach (TreeListNode node in treeList1.Nodes[0].Nodes) {
      //          TreeListNode pnode = findDYHParent(node);
      //          if (pnode != null && pnode.HasChildren) {
      //              count1++;
      //              DataRow newrow = showtable.NewRow();
      //              newrow["Title"] = node["Title"];
      //              newrow["Col1"] = getDX(count1);
      //              showtable.Rows.Add(newrow);
      //              int count2 = 0;//���ش��û�����
      //              TreeListNode node1 = pnode.Nodes[0];
      //              TreeListNode findnode = null;
      //              while (node1 != null) {
      //                  if (node1["Title"].ToString().Contains("����")) {
      //                      findnode = node1;
      //                      break;
      //                  }
      //                  node1 = node1.NextNode;
      //              }
      //              if (findnode == null) continue;
      //              foreach (TreeListNode node2 in findnode.Nodes) {
      //                  count2++;
      //                  DataRowView row2 = node2.TreeList.GetDataRecordByNode(node2) as DataRowView;
      //                  newrow = createRow(showtable, row2.Row);
      //                  newrow["Col1"] = count2;
      //                  setFh(newrow,row2["ID"].ToString());
      //              }
      //          }
      //      }
      //      //����С��
      //      DataRow row33 = createSumRow(showtable);
      //      row33["Title"] = "С��";
      //      row33["Col1"] = getDX(count1 + 1);
      //      showtable.Rows.Add(row33);
      //      return showtable;
      //  }
        private DataTable ConvertTreeListToDataTable(DevExpress.XtraTreeList.TreeList treeList1) {
            if(treeList1.Nodes.Count==0)return showtable;
            int count1 = 0;//���ؼ���
            foreach (TreeListNode nd in treeList1.Nodes)
            {
                //if (nd.Nodes.Count < 4)
                //    continue;

                foreach (TreeListNode node in nd.Nodes)
                {
                    TreeListNode pnode = findDYHParent(node);
                    if (pnode != null && pnode.HasChildren)
                    {
                        count1++;
                        DataRow newrow = showtable.NewRow();
                        newrow["Title"] = node["Title"];
                        newrow["Col1"] = getDX(count1);
                        showtable.Rows.Add(newrow);
                        int count2 = 0;//���ش��û�����
                        TreeListNode node1 = pnode.Nodes[0];
                        TreeListNode findnode = null;
                        while (node1 != null)
                        {
                            if (node1["Title"].ToString().Contains("����"))
                            {
                                findnode = node1;
                                break;
                            }
                            node1 = node1.NextNode;
                        }
                        if (findnode == null) continue;
                        foreach (TreeListNode node2 in findnode.Nodes)
                        {
                            count2++;
                            DataRowView row2 = node2.TreeList.GetDataRecordByNode(node2) as DataRowView;
                            newrow = createRow(showtable, row2.Row);
                            newrow["Col1"] = count2;
                            setFh(newrow, row2["ID"].ToString());
                        }
                    }
                }
            }
            //����С��
            DataRow row33 = createSumRow(showtable);
            row33["Title"] = "С��";
            row33["Col1"] = getDX(count1 + 1);
            showtable.Rows.Add(row33);
            return showtable;
        }

        private DataTable ResultDataTable(object p, object p_2) {

            return null;
        }
        #endregion 

        private void bt432_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tj432();
        }

        private void bt433_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tj433();
        }
        #region ���ƽڵ�
        TreeListNode cloneNode(TreeListNode cnode) {
            TreeListNode retnode = cnode.Clone() as TreeListNode;
            for (int i = cnode.Nodes.Count - 1; i >= 0; i--) {
                retnode.Nodes.Add(cloneNode(cnode.Nodes[i]));
            }
            return retnode;
        }
        private void setdatazero(Ps_History ps) {
            Type t = ps.GetType();
            foreach (PropertyInfo infor in t.GetProperties()) {
                if (infor.Name.StartsWith("y") && infor.Name.Length == 5) {
                    try { infor.SetValue(ps, 0, null); } catch { }
                }
            }
        }
        private void copyNode(TreeListNode cnode) {
            if (cnode.Level > 3)
            {
                MessageBox.Show("���ڵ�����Ľڵ㲻�ܽ��и���!");
                return;
            } 
            DataRow row = (cnode.TreeList.GetDataRecordByNode(cnode) as DataRowView).Row;
            Ps_History ps = Itop.Common.DataConverter.RowToObject<Ps_History>(row);
            ps.ID = createID();
            setdatazero(ps);
            Common.Services.BaseService.Create<Ps_History>(ps);
            string pid = ps.ID;
            ps.ID = pid + splitstr;
            ps.ParentID = ps.ParentID + splitstr;
            ps.Forecast = type32;
            ps.ForecastID = type31;
            Common.Services.BaseService.Create<Ps_History>(ps);
            
           TreeListNode nd1=null;
            for(int i=cnode.Nodes.Count -1;i>=0;i--) {
                nd1 = cnode.Nodes[i];
                 nd1["ParentID"] = pid;
                 copyNode(nd1);                 
            }            
        }
        //���ƽڵ㼰�ӽڵ�
        private void btCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 
        {
  
            TreeListNode tln = treeList1.FocusedNode;

            if (tln == null || tln.Level == 0)
            {
                MessageBox.Show("����ѡ�Ľ�㲻�ܸ��ƣ�");
                return;
            }
  
            //if (MsgBox.ShowYesNo("��ȷ�ϸ���["+tln.GetDisplayText("Title")+"]�����ࣿ") != DialogResult.Yes) return;
             FormFqHistoryDF_add frm = new FormFqHistoryDF_add();
            frm.Text = "���Ʒ���";
            frm.TypeTitle = tln["Title"].ToString();
            if (frm.ShowDialog() == DialogResult.OK) {
                TreeListNode copy = cloneNode(tln);
                copy["Title"] = frm.TypeTitle;
               // copy["Sort"] = frm.Sortid;
                copyNode(copy);
                LoadData();
            }
        }
        #endregion
        //�����������õ������
        private void bt532_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null || tln.Level != 1) {
                MsgBox.Show("����ѡ����������ԣ�");
                return;
            }
            tj532();
        }
        #region ճ�����а�����
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pnode">���ڵ�</param>
        /// <param name="column">����</param>
        /// <param name="findstr">�����ַ�</param>
        /// <param name="f1">�Ƿ���Ȳ���</param>
        /// <returns></returns>
        ///         
        private TreeListNode findByColumnValue(TreeListNode pnode, string column,string findstr, bool f1) {
            TreeListNode retnode = null;
            foreach (TreeListNode node in pnode.Nodes) {
                if (node[column].ToString().Contains(findstr)) {
                    retnode = node;
                    break;
                }
                if (f1) {
                    retnode = findByColumnValue(node, column, findstr, f1);
                    if (retnode != null) break;
                }
            }
            return retnode;
        }
        private void createRow(string title) {

        }
        private object getSummaryValue(string columnid, TreeListNodes nodes) {
            double ret=0;
            foreach (TreeListNode node in nodes) {
                double d1 = 0;
                double.TryParse(node[columnid].ToString(), out d1);
                ret += d1;
            }
            return ret;
        }
        private void updateSummaryNode(TreeListNode pnode) {
            foreach (string col in pasteCols) {
                if(col.StartsWith("y")){
                    try {
                        object obj = getSummaryValue(col, pnode.Nodes);
                        pnode.SetValue(col, obj);
                    } catch { }
                }
            }
            updateNode(pnode);
            if (pnode.ParentNode != null)
                updateSummaryNode(pnode.ParentNode);
        }
        
        private void updateNode(TreeListNode node) {
            DataRow row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;
            Ps_History v = DataConverter.RowToObject<Ps_History>(row);
            Services.BaseService.Update<Ps_History>(v);
        }
        private void pasteData(TreeListNode tln) {
            string s1 = tln["Title"].ToString();
            //if (!s1.StartsWith("����")) return;

            IDataObject obj1 = Clipboard.GetDataObject();
            string text = obj1.GetData("Text").ToString();
            string[] lines = text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            tln.TreeList.BeginInit();
            for (int i = 0; i < lines.Length; i++) {
                string[] items = lines[i].Split(new string[] { "\t" }, StringSplitOptions.None);
                if (items.Length != pasteCols.Count) continue;
                TreeListNode fnode = findByColumnValue(tln, "Title", items[0], false);
                if (fnode == null) continue;
                for (int j = 0; j < pasteCols.Count; j++) {
                    try {
                        if (items[j] == "") items[j] = "0";
                        fnode.SetValue(pasteCols[j], items[j]);                    
                    } catch { }
                }
                updateNode(fnode);
            }
            updateSummaryNode(tln);
            tln.TreeList.EndInit();
        }
        #endregion
        private void treeList1_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control && e.KeyCode == Keys.V) {
                TreeListNode tln = treeList1.FocusedNode;
                if (tln == null ) {
                    return;
                }
                pasteData(tln);
            }
        }
        //�趨���
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            FormYearSet fys = new FormYearSet();
            fys.TYPE = "���ط�չʵ��";
            fys.PID = ProjectUID;
            if (fys.ShowDialog() != DialogResult.OK)
                return;
            firstyear = fys.SYEAR;
            endyear = fys.EYEAR;
            //����ͳ�Ƽ���
            //WaitDialogForm wait = null;
            //try
            //{

               
            //    wait = new WaitDialogForm("", "���ڴ�����...");
            //    wait.Show();
                
            //   jstjaddyear1();
            //    jstjflag = false;
            //   // LoadData();
            //    wait.Close();
            //}
            //catch (System.Exception ex)
            //{
            //    wait.Close();
            //}
             

            LoadData();

        }
        private bool jstjflag = false;
        private void jstjaddyear(WaitDialogForm wf)
        {
           
                jstjflag = true;
                DataRow[] rows1 = dataTable.Select("Title like '%����%'");
                DataRow[] rows2 = dataTable2.Select("Title like '%����%'");

                //���ƽ̯��
                //double ftlv = 0.2;
                //Ps_Calc pcs = new Ps_Calc();
                //pcs.Forecast = type;
                //pcs.ForecastID = forecastReport.ID;
                //IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);
                //if (list1.Count > 0)
                //{
                //    ftlv = list1[0].Value1;
                //}
                //��ȡ���еĴ��û�
                wf.SetCaption("����ͳ���������û�����");
                foreach (DataRow row in rows1)
                {
                    DataRow[] rows = dataTable.Select("ParentID='" + row["ID"].ToString() + "'");
                    if (rows != null && rows.Length != 0)
                    {
                        CalaculateSumnew1(rows[rows.Length - 1], dataTable);
                        //for (int i = firstyear; i <= endyear; i++)
                        //{

                        //    string y = "y" + i.ToString();
                        //    //CalculateSum(treeList1.FindNodeByKeyID(rows[rows.Length - 1]["ID"].ToString()), treeList1.Columns[y]);
                        //    CalaculateSumnew(rows[rows.Length - 1], y, dataTable);

                        //}

                    }
                }
                wf.SetCaption("����ͳ���������û�����");
                foreach (DataRow row in rows2)
                {
                    DataRow[] rows = dataTable2.Select("ParentID='" + row["ID"].ToString() + "'");
                    if (rows != null && rows.Length != 0)
                    {
                        CalaculateSumnew1(rows[rows.Length - 1], dataTable2);
                        //for (int i = firstyear; i <= endyear; i++)
                        //{
                        //    string y = "y" + i.ToString();
                        //    //CalculateSum(treeList2.FindNodeByKeyID(rows[rows.Length - 1]["ID"].ToString()), treeList2.Columns[y]);
                        //    CalaculateSumnew(rows[rows.Length - 1], y, dataTable2);
                        //}


                    }
                }
                rows1 = dataTable.Select("Title like '%����%'");
                rows2 = dataTable2.Select("Title like '%����%'");

                //���ƽ̯��
                //double ftlv = 0.2;
                //Ps_Calc pcs = new Ps_Calc();
                //pcs.Forecast = type;
                //pcs.ForecastID = forecastReport.ID;
                //IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);
                //if (list1.Count > 0)
                //{
                //    ftlv = list1[0].Value1;
                //}
                //��ȡ���еĴ��û�
                wf.SetCaption("����ͳ�����д��û�����");
                foreach (DataRow row in rows1)
                {
                    DataRow[] rows = dataTable.Select("ParentID='" + row["ID"].ToString() + "'");
                    if (rows != null && rows.Length != 0)
                    {
                        CalaculateSumnew1(rows[rows.Length - 1], dataTable);
                        //for (int i = firstyear; i <= endyear; i++)
                        //{
                        //    string y = "y" + i.ToString();
                        //    //CalculateSum(treeList1.FindNodeByKeyID(rows[rows.Length - 1]["ID"].ToString()), treeList1.Columns[y]);
                        //    CalaculateSumnew(rows[rows.Length - 1], y, dataTable);
                        //}

                    }
                }
                wf.SetCaption("����ͳ�����д��û�����");
                foreach (DataRow row in rows2)
                {
                    DataRow[] rows = dataTable2.Select("ParentID='" + row["ID"].ToString() + "'");
                    if (rows != null && rows.Length != 0)
                    {
                        CalaculateSumnew1(rows[rows.Length - 1], dataTable2);
                        //for (int i = firstyear; i <= endyear; i++)
                        //{
                        //    string y = "y" + i.ToString();
                        //   // CalculateSum(treeList2.FindNodeByKeyID(rows[rows.Length - 1]["ID"].ToString()), treeList2.Columns[y]);
                        //    CalaculateSumnew(rows[rows.Length - 1], y, dataTable2);
                        //}


                    }
                }
        
            
        }
        //�������ʱ�����ܵ�ͳ��
         private void jstjaddyear1()
         {
             jstjflag = true;
             DataRow[] rows1 = dataTable.Select("Title like '%����%'");
             DataRow[] rows2 = dataTable2.Select("Title like '%����%'");

             //���ƽ̯��
             //double ftlv = 0.2;
             //Ps_Calc pcs = new Ps_Calc();
             //pcs.Forecast = type;
             //pcs.ForecastID = forecastReport.ID;
             //IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);
             //if (list1.Count > 0)
             //{
             //    ftlv = list1[0].Value1;
             //}
             //��ȡ���еĴ��û�
            
             foreach (DataRow row in rows1)
             {
                 DataRow[] rows = dataTable.Select("ParentID='" + row["ID"].ToString() + "'");
                 if (rows != null && rows.Length != 0)
                 {
                     CalaculateSumnew1(rows[rows.Length - 1], dataTable);
                     //for (int i = firstyear; i <= endyear; i++)
                     //{

                     //    string y = "y" + i.ToString();
                     //    //CalculateSum(treeList1.FindNodeByKeyID(rows[rows.Length - 1]["ID"].ToString()), treeList1.Columns[y]);
                     //    CalaculateSumnew(rows[rows.Length - 1], y, dataTable);

                     //}

                 }
             }
            
             foreach (DataRow row in rows2)
             {
                 DataRow[] rows = dataTable2.Select("ParentID='" + row["ID"].ToString() + "'");
                 if (rows != null && rows.Length != 0)
                 {
                     CalaculateSumnew1(rows[rows.Length - 1], dataTable2);
                     //for (int i = firstyear; i <= endyear; i++)
                     //{
                     //    string y = "y" + i.ToString();
                     //    //CalculateSum(treeList2.FindNodeByKeyID(rows[rows.Length - 1]["ID"].ToString()), treeList2.Columns[y]);
                     //    CalaculateSumnew(rows[rows.Length - 1], y, dataTable2);
                     //}


                 }
             }
             rows1 = dataTable.Select("Title like '%����%'");
             rows2 = dataTable2.Select("Title like '%����%'");

             //���ƽ̯��
             //double ftlv = 0.2;
             //Ps_Calc pcs = new Ps_Calc();
             //pcs.Forecast = type;
             //pcs.ForecastID = forecastReport.ID;
             //IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);
             //if (list1.Count > 0)
             //{
             //    ftlv = list1[0].Value1;
             //}
             //��ȡ���еĴ��û�
             
             foreach (DataRow row in rows1)
             {
                 DataRow[] rows = dataTable.Select("ParentID='" + row["ID"].ToString() + "'");
                 if (rows != null && rows.Length != 0)
                 {
                     CalaculateSumnew1(rows[rows.Length - 1], dataTable);
                     //for (int i = firstyear; i <= endyear; i++)
                     //{
                     //    string y = "y" + i.ToString();
                     //    //CalculateSum(treeList1.FindNodeByKeyID(rows[rows.Length - 1]["ID"].ToString()), treeList1.Columns[y]);
                     //    CalaculateSumnew(rows[rows.Length - 1], y, dataTable);
                     //}

                 }
             }
            
             foreach (DataRow row in rows2)
             {
                 DataRow[] rows = dataTable2.Select("ParentID='" + row["ID"].ToString() + "'");
                 if (rows != null && rows.Length != 0)
                 {
                     CalaculateSumnew1(rows[rows.Length - 1], dataTable2);
                     //for (int i = firstyear; i <= endyear; i++)
                     //{
                     //    string y = "y" + i.ToString();
                     //   // CalculateSum(treeList2.FindNodeByKeyID(rows[rows.Length - 1]["ID"].ToString()), treeList2.Columns[y]);
                     //    CalaculateSumnew(rows[rows.Length - 1], y, dataTable2);
                     //}


                 }
             }
         }
        //�������ʱ�����ܵ�ͳ��
        private void jstjaddyear()
        {
            jstjflag = true;
            DataRow[] rows1 = dataTable.Select("Title like '%����%'");
            DataRow[] rows2 = dataTable2.Select("Title like '%����%'");
           
            //���ƽ̯��
            //double ftlv = 0.2;
            //Ps_Calc pcs = new Ps_Calc();
            //pcs.Forecast = type;
            //pcs.ForecastID = forecastReport.ID;
            //IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);
            //if (list1.Count > 0)
            //{
            //    ftlv = list1[0].Value1;
            //}
            //��ȡ���еĴ��û�
            foreach (DataRow row in rows1)
            {
                DataRow[] rows = dataTable.Select("ParentID='" + row["ID"].ToString() + "'");
                if (rows != null && rows.Length != 0)
                {
                    for (int i = firstyear; i <= endyear; i++)
                    {
                     
                        string y = "y" + i.ToString();
                        CalculateSum(treeList1.FindNodeByKeyID(rows[rows.Length-1]["ID"].ToString()), treeList1.Columns[y]);
                      
                    }
                 
                }
            }

            foreach (DataRow row in rows2)
            {
                DataRow[] rows = dataTable2.Select("ParentID='" + row["ID"].ToString() + "'");
                if (rows != null && rows.Length != 0)
                {
                    for (int i = firstyear; i <= endyear; i++)
                    {
                        string y = "y" + i.ToString();
                        CalculateSum(treeList2.FindNodeByKeyID(rows[rows.Length - 1]["ID"].ToString()), treeList2.Columns[y]);
                        
                    }
                   

                }
            }
            rows1 = dataTable.Select("Title like '%����%'");
            rows2 = dataTable2.Select("Title like '%����%'");

            //���ƽ̯��
            //double ftlv = 0.2;
            //Ps_Calc pcs = new Ps_Calc();
            //pcs.Forecast = type;
            //pcs.ForecastID = forecastReport.ID;
            //IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);
            //if (list1.Count > 0)
            //{
            //    ftlv = list1[0].Value1;
            //}
            //��ȡ���еĴ��û�
            foreach (DataRow row in rows1)
            {
                DataRow[] rows = dataTable.Select("ParentID='" + row["ID"].ToString() + "'");
                if (rows != null && rows.Length != 0)
                {
                    for (int i = firstyear; i <= endyear; i++)
                    {
                        string y = "y" + i.ToString();
                        CalculateSum(treeList1.FindNodeByKeyID(rows[rows.Length - 1]["ID"].ToString()), treeList1.Columns[y]);

                    }

                }
            }

            foreach (DataRow row in rows2)
            {
                DataRow[] rows = dataTable2.Select("ParentID='" + row["ID"].ToString() + "'");
                if (rows != null && rows.Length != 0)
                {
                    for (int i = firstyear; i <= endyear; i++)
                    {
                        string y = "y" + i.ToString();
                        CalculateSum(treeList2.FindNodeByKeyID(rows[rows.Length - 1]["ID"].ToString()), treeList2.Columns[y]);

                    }


                }
            }
        }
        //���ݿ���
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            FormProjectDataCopy fpd = new FormProjectDataCopy();
            fpd.ProjectUID = ProjectUID;

            if (fpd.ShowDialog() != DialogResult.OK)
                return;

            string pid = fpd.ProjectUID;

            Ps_History psp_Type1 = new Ps_History();
            psp_Type1.Forecast = type;
            psp_Type1.Col4 = pid;
            //����
            IList<Ps_History> li1 = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type1);

           
            psp_Type1.Forecast = type32;
            psp_Type1.Col4 = pid;
            //����
            IList<Ps_History> li2 = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type1);
           

            foreach (Ps_History ph in li1) {                    
                ph.ParentID = ph.ParentID.Replace(pid, ProjectUID);
                ph.Col4 = ProjectUID;
                ph.ID = ph.ID.Replace(pid, ProjectUID);
                try {
                    Services.BaseService.Create<Ps_History>(ph);
                } catch { }

            }
            foreach (Ps_History ph in li2) {
                ph.ParentID = ph.ParentID.Replace(pid, ProjectUID);
                ph.Col4 = ProjectUID;
                ph.ID = ph.ID.Replace(pid, ProjectUID);
                try {
                    Services.BaseService.Create<Ps_History>(ph);
                } catch { }
            }

            LoadData();
        }
        //ȫ���⽨��ҵ��Ŀ
        private void bt431_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            tj431();
        }
        //�����ص㽨����Ŀ
        private void bt511_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null || tln.Level != 1) {
                MsgBox.Show("����ѡ����������ԣ�");
                return;
            }
            tj511();
        }
        private void cacsumsehui(string strname)
        {
            Ps_History v;
            DataRow[] drlist = dataTable.Select("Title like '%����%'");
            //foreach (DataColumn dc in dataTable.Columns)
            //{
            double value = 0;
            //if (dc.ColumnName.IndexOf("y") != 0)
            //{
            //    continue;
            //}
            foreach (DataRow dr in drlist)
            {
                
                if (treeList1.FindNodeByKeyID(dr["ID"]).ParentNode.ParentNode == null)
                    continue;
                try
                {
                    value +=Math.Round( Convert.ToDouble(dr[strname]),2);
                }
                catch { }
            }
                foreach (DataRow dr in drlist)
                {
                    TreeListNode node = treeList1.FindNodeByKeyID(dr["ID"]).ParentNode.ParentNode;
                              if (node == null)
                              {
                                  int i = dataTable.Rows.IndexOf(dr);

                                 
                                  dataTable.Rows[i][strname]= value;
                                  //DataRow row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;
                                  v = DataConverter.RowToObject<Ps_History>(dataTable.Rows[i]);
                                  Common.Services.BaseService.Update<Ps_History>(v);
                              }
                    }
               
           
          
            DataRow[] drlist2 = dataTable.Select("Title like '%�û�%'");
          
            value = 0;
           
            foreach (DataRow dr in drlist2)
            {
                if (treeList1.FindNodeByKeyID(dr["ID"]).ParentNode.ParentNode == null)
                    continue;
                    
                try
                {
                    value +=Math.Round( Convert.ToDouble(dr[strname]),2);
                }
                catch { }
            }
            foreach (DataRow dr in drlist2)
            {
                TreeListNode node = treeList1.FindNodeByKeyID(dr["ID"]).ParentNode.ParentNode;
                if (node == null)
                {
                    int i = dataTable.Rows.IndexOf(dr);
                   
                    dataTable.Rows[i][strname] = value;
                    v = DataConverter.RowToObject<Ps_History>(dataTable.Rows[i]);
                    Common.Services.BaseService.Update<Ps_History>(v);
                }
            }
               
            //  dataTable.Rows[2][dc.ColumnName] = value;
            //           }

        }
        private void cacsumsehui2(string strname)
        {
            Ps_History v;
            DataRow[] drlist3 = dataTable2.Select("Title like '%����%'");
            //foreach (DataColumn dc in dataTable2.Columns)
            //{
            double value = 0;
            //if (dc.ColumnName.IndexOf("y") != 0)
            //{
            //    continue;
            //}
            foreach (DataRow dr in drlist3)
            {
                if (treeList2.FindNodeByKeyID(dr["ID"]).ParentNode.ParentNode == null)
                    continue;
                try
                {
                    value +=Math.Round( Convert.ToDouble(dr[strname]),2);
                }
                catch { }
            }
            foreach (DataRow dr in drlist3)
            {
                TreeListNode node = treeList2.FindNodeByKeyID(dr["ID"]).ParentNode.ParentNode;
                if (node == null)
                {
                    int i = dataTable2.Rows.IndexOf(dr);

                    dataTable2.Rows[i][strname] = value;
                    v = DataConverter.RowToObject<Ps_History>(dataTable2.Rows[i]);
                    Common.Services.BaseService.Update<Ps_History>(v);
                }
            }
            DataRow[] drlist4 = dataTable2.Select("Title like '%�û�%'");
            
            value = 0;
            
            foreach (DataRow dr in drlist4)
            {
                if (treeList2.FindNodeByKeyID(dr["ID"]).ParentNode.ParentNode == null)
                    continue;
                try
                {
                    value +=Math.Round( Convert.ToDouble(dr[strname]),2);
                }
                catch { }
            }
            foreach (DataRow dr in drlist4)
            {

                TreeListNode node = treeList2.FindNodeByKeyID(dr["ID"]).ParentNode.ParentNode;
                if (node == null)
                {
                    int i = dataTable2.Rows.IndexOf(dr);

                    dataTable2.Rows[i][strname] = value;
                    v = DataConverter.RowToObject<Ps_History>(dataTable2.Rows[i]);
                    Common.Services.BaseService.Update<Ps_History>(v);
                }
            }
            //  dataTable.Rows[2][dc.ColumnName] = value;
            //}
        }
        //����ȫ����õ���
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (DataColumn dc in dataTable.Columns)
            {
                if (dc.ColumnName.IndexOf("y") != 0)
                {
                    continue;
                }
                cacsumsehui(dc.ColumnName);
                cacsumsehui2(dc.ColumnName);
            }
          
        }
        //���һ�����
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode focusedNode = treeList1.FocusedNode;

            if (focusedNode == null)
            {
                return;
            }

            if (focusedNode["Title"].ToString().Contains("����"))
            {
                newHistory();
                return;
            }

            FormFqHistoryDF_add frm = new FormFqHistoryDF_add();
            frm.Text = "���ӷ���";
            int sort = 0;
            if (focusedNode.Nodes.Count > 0)
                sort = (int)focusedNode.Nodes[focusedNode.Nodes.Count - 1]["Sort"] + 1;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_History pf = new Ps_History();
                pf.ID = createID();
                pf.Forecast = type;
                pf.ForecastID = type1;
                pf.Title = frm.TypeTitle;
                pf.Col4 = ProjectUID;
                pf.Sort = sort;
                Ps_History pf2 = new Ps_History();
                pf2.ID = pf.ID + splitstr;
                pf2.Forecast = type32;
                pf2.ForecastID = type31;
                pf2.Title = pf.Title;
                if (pf.Title.Contains("�õ���"))
                    pf2.Title = pf.Title.Replace("�õ���", "����");
                pf2.Col4 = ProjectUID;
                pf2.Sort = sort;
                try
                {
                    Services.BaseService.Create<Ps_History>(pf);
                    Services.BaseService.Create<Ps_History>(pf2);
                    treeList1.BeginInit();
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf, dataTable.NewRow()));
                    treeList1.EndInit();
                    treeList2.BeginInit();
                    dataTable2.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf2, dataTable2.NewRow()));
                    treeList2.EndInit();
                    
                    //LoadData();
                    //insertNode(focusedNode, pf);
                }
                catch (Exception ex) { MsgBox.Show("���ӷ������" + ex.Message); }
            }
        }
        //Ĭ��������
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode tempnode = treeList1.FocusedNode;
            if (tempnode==null)
            {
                return;
            }
            bool isArea = false;
            for (int i = 0; i < AreaList.Count; i++)
            {
                if (tempnode.GetValue("Title").ToString()==AreaList[i])
                {
                    isArea = true;
                    break;
                }
            }
            if (!isArea)
            {
                MessageBox.Show("��ѡ��һ������������Ĭ��������!");
                return;
            }
            FormHistoryType frm = new FormHistoryType("3");
            List<string> templist = new List<string>();
            templist = Traversal_Node(treeList1.FocusedNode, templist, "ID");
            frm.ValueList = templist;
            frm.AreaId = tempnode.GetValue("ID").ToString();
            frm.AreaName = tempnode.GetValue("Title").ToString();
            frm.ShowDialog();
        }
        /// <summary>
        /// ��������ĳһ��㼰�ӽ��
        /// </summary>
        /// <param name="obj_Node">Ҫ�����Ľ��</param>
        /// <param name="obj_StrList">Ҫ���ص��ַ����б�</param>
        /// <param name="ColName">������������</param>
        /// <returns></returns>
        private List<string> Traversal_Node(TreeListNode obj_Node, List<string> obj_StrList, string ColName)
        {
           
            obj_StrList.Add(obj_Node.GetValue(ColName).ToString());
        
            if (!obj_Node.HasChildren)
            {
                  return obj_StrList;
            }
            else
            {
                foreach (TreeListNode node in obj_Node.Nodes)
                {
                    obj_StrList= Traversal_Node(node, obj_StrList, ColName);
                }
                return obj_StrList;
            }
            
        }
        //�����������
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MsgBox.ShowYesNo("�ò���������������ݣ���������Ժ��޷��ָ�,���ܶ������û������ݲ���Ӱ�죬�������������ȷ��������") == DialogResult.No)
                return;
            Ps_History psp_Type2 = new Ps_History();
            psp_Type2.Forecast = type;
            psp_Type2.Col4 = ProjectUID;
            Services.BaseService.Update("DeletePs_HistoryBy", psp_Type2);
            psp_Type2.Forecast = type32;
            psp_Type2.Col4 = ProjectUID;
            Services.BaseService.Update("DeletePs_HistoryBy", psp_Type2);
            LoadData();

        }
        //����չ�� ����չ��
        private void treeList1_AfterExpand(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (expandocoll)
            {
                return;
            }
            expandocoll = true;
            string IDstr = e.Node.GetValue("ID").ToString();
            IDstr+=splitstr;
            TreeListNode tln = treeList2.FindNodeByKeyID(IDstr);
            if (tln!=null)
            {
                tln.Expanded = e.Node.Expanded;

            }
            
            expandocoll = false;

        }
        //�����ڵ����� ��������
        private void treeList1_AfterCollapse(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (expandocoll)
            {
                return;
            }
            expandocoll = true;
            string IDstr = e.Node.GetValue("ID").ToString();
            IDstr += splitstr;
            TreeListNode tln = treeList2.FindNodeByKeyID(IDstr);
            if (tln!=null)
            {
                tln.Expanded = e.Node.Expanded;
            }
           
            expandocoll = false;
        }
        //���ɽڵ���������������
        private void treeList2_AfterCollapse(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (expandocoll)
            {
                return;
            }
            expandocoll = true;
            string IDstr = e.Node.GetValue("ID").ToString();
            IDstr = IDstr.Substring(0, IDstr.LastIndexOf("-"));
            TreeListNode tln = treeList1.FindNodeByKeyID(IDstr);
            if (tln!=null)
            {
                tln.Expanded = e.Node.Expanded;
            }
           
            expandocoll = false;
        }
        //���ɽڵ�չ��������չ��
        private void treeList2_AfterExpand(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (expandocoll)
            {
                return;
            }
            expandocoll = true;
            string IDstr = e.Node.GetValue("ID").ToString();
            IDstr = IDstr.Substring(0, IDstr.LastIndexOf("-"));
            TreeListNode tln = treeList1.FindNodeByKeyID(IDstr);
            if (tln!=null)
            {
                tln.Expanded = e.Node.Expanded;
            }
        
            expandocoll = false;
        }
        //�������͸��ɽ�㱣��ͬ��
        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node != null)
            {
                string id = e.Node["ID"] + splitstr;
                TreeListNode node = treeList2.FindNodeByKeyID(id);
                if (node != null)
                {
                    treeList2.Selection.Clear();
                    treeList2.SetFocusedNode(node);
                }
            }
        }
        //���ɽ��͵�����㱣��ͬ��
        private void treeList2_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node != null)
            {
                 string IDstr = e.Node["ID"].ToString();
                 IDstr = IDstr.Substring(0, IDstr.LastIndexOf("-"));
                 TreeListNode node = treeList1.FindNodeByKeyID(IDstr);
                if (node != null)
                {
                    treeList1.Selection.Clear();
                    treeList1.SetFocusedNode(node);
                }
            }
        }
        //����
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }

            TreeListNode node = treeList1.FocusedNode;
            int i = 0, sortj = 0, sortj2 = 0;

            if (treeList1.FocusedNode.PrevNode != null)
            {
                string ID1 = treeList1.FocusedNode.PrevNode["ID"].ToString();
                string ID2 = treeList1.FocusedNode["ID"].ToString();
                if (ID1 != ID2)
                {
                    sortj = Convert.ToInt32(treeList1.FocusedNode.PrevNode["Sort"]);
                    sortj2 = Convert.ToInt32(treeList1.FocusedNode["Sort"]);
                    if (sortj2 == sortj)
                    {
                        sortj2 = sortj2 - 1;

                    }
                    else
                    {
                        i = sortj;
                        sortj = sortj2;
                        sortj2 = i;
                    }
                    Ps_History pj = Common.Services.BaseService.GetOneByKey<Ps_History>(ID1);
                    pj.Sort = sortj;
                    Ps_History pj2 = Common.Services.BaseService.GetOneByKey<Ps_History>(ID2);
                    pj2.Sort = sortj2;

                    Ps_History ph = Common.Services.BaseService.GetOneByKey<Ps_History>(ID1+splitstr);
                    Ps_History ph2 = Common.Services.BaseService.GetOneByKey<Ps_History>(ID2 +splitstr);
                    if (ph != null)
                    {
                        ph.Sort = pj.Sort;
                        Common.Services.BaseService.Update<Ps_History>(ph);
                    }
                    if (ph2 != null)
                    {
                        ph2.Sort = pj2.Sort;
                        Common.Services.BaseService.Update<Ps_History>(ph2);
                    }

                    Common.Services.BaseService.Update<Ps_History>(pj);
                    Common.Services.BaseService.Update<Ps_History>(pj2);
                    treeList1.FocusedNode.PrevNode["Sort"] = sortj;
                    treeList1.FocusedNode["Sort"] = sortj2;
                    treeList2.FocusedNode.PrevNode["Sort"] = sortj;
                    treeList2.FocusedNode["Sort"] = sortj2;
                    treeList1.BeginSort();
                    treeList1.EndSort();
                    treeList2.BeginSort();
                    treeList2.EndSort();

                }

            }
        }
        //����ʷ���ݸ�ֵ
        private void setphvalue(Ps_History ph, string flied, double value)
        {
            Type tp = ph.GetType();
            foreach (PropertyInfo pi in tp.GetProperties())
            {
                if (pi.Name==flied)
                {
                    pi.SetValue(ph, value, null);
                }
                
            }
        }

        //����
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }

            TreeListNode node = treeList1.FocusedNode;
            int i = 0, sortj = 0, sortj2 = 0;

            if (treeList1.FocusedNode.NextNode != null)
            {
                string ID1 = treeList1.FocusedNode.NextNode["ID"].ToString();
                string ID2 = treeList1.FocusedNode["ID"].ToString();
                if (ID1 != ID2)
                {
                    sortj = Convert.ToInt32(treeList1.FocusedNode.NextNode["Sort"]);
                    sortj2 = Convert.ToInt32(treeList1.FocusedNode["Sort"]);
                    if (sortj2 == sortj)
                    {
                        sortj2 = sortj2 + 1;
                        i = sortj;
                        sortj = sortj2;
                        sortj2 = i;
                    }
                    else
                    {
                        i = sortj;
                        sortj = sortj2;
                        sortj2 = i;
                    }

                    Ps_History pj = Common.Services.BaseService.GetOneByKey<Ps_History>(ID1);
                    pj.Sort = sortj;
                    Ps_History pj2 = Common.Services.BaseService.GetOneByKey<Ps_History>(ID2);
                    pj2.Sort = sortj2;

                    Ps_History ph = Common.Services.BaseService.GetOneByKey<Ps_History>(ID1+splitstr);
                    Ps_History ph2 = Common.Services.BaseService.GetOneByKey<Ps_History>(ID2+splitstr);
                    if (ph != null)
                    {
                        ph.Sort = pj.Sort;
                        Common.Services.BaseService.Update<Ps_History>(ph);
                    }
                    if (ph2 != null)
                    {
                        ph2.Sort = pj2.Sort;
                        Common.Services.BaseService.Update<Ps_History>(ph2);
                    }

                    Common.Services.BaseService.Update<Ps_History>(pj);
                    Common.Services.BaseService.Update<Ps_History>(pj2);
                    treeList1.FocusedNode.NextNode["Sort"] = sortj;
                    treeList1.FocusedNode["Sort"] = sortj2;
                    treeList2.FocusedNode.NextNode["Sort"] = sortj;
                    treeList2.FocusedNode["Sort"] = sortj2;
                    treeList1.BeginSort();
                    treeList1.EndSort();
                    treeList2.BeginSort();
                    treeList2.EndSort();
                }

            }
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            WaitDialogForm wait = null;
            wait = new WaitDialogForm("", "���ڼ����������Ժ�...");
            wait.Show();
            try
            {
                jstjaddyear(wait);
            }
            catch (System.Exception ex)
            {
            	
            }
            jstjflag = false;
            LoadData();
            wait.Close();

        }

        
    }
}