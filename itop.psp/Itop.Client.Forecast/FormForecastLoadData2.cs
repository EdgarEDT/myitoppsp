using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using Itop.Domain.GM;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using System.Collections;
using DevExpress.XtraTreeList;
using Itop.Client.Common;
using Itop.Domain.Forecast;
using Itop.Domain.HistoryValue;
using Itop.Client.Base;
namespace Itop.Client.Forecast
{
    public partial class FormForecastLoadData2 : FormBase
    {
        //���ڿ����¾ɰ汾����ѡȡ��1Ϊ�ɰ汾����Ҫ�Ƿ�������ʵ�����ݾɰ������������
        //2Ϊ�°汾���ĸ�����Դ�����ͬһ������
        //�ɰ�
        //private string Flag = "1";
        ////�°�

        public bool maxhour = false;
        public bool ISGDP = false;
      private string Flag = "2";

        DataTable dataTable;

        private string lastEditValue = string.Empty;

        private int typeFlag2 = 1;


        Hashtable hs = new Hashtable();

        public Hashtable HS
        {
            set { hs = value; }
            get { return hs; }
        
        }

        string pid = "";
        public string PID
        {
            set { pid = value; }
            get { return pid; }

        }
        string selectid = "";
        public string Selectid
        {
            set { selectid = value; }
            get { return selectid; }

        }
        int startyear = 0;

        public int StartYear
        {
            set { startyear = value; }
            get { return startyear; }

        }


        IList<Ps_History> li = new List<Ps_History>();

        public IList<Ps_History> LI
        {
            set { li = value; }
            get { return li; }

        }

        int endyear = 0;

        public int EndYear
        {
            set { endyear = value; }
            get { return endyear; }

        }

        public FormForecastLoadData2()
        {
            InitializeComponent();
        }
        //��ӹ̶���
        private void AddFixColumn()
        {
            // treeList1.Columns.Add(year + "��", typeof(double));

            DevExpress.XtraTreeList.Columns.TreeListColumn column = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            column.FieldName = "Title";
            column.Caption = "������";
            column.VisibleIndex = 0;
            column.Width = 180;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            column});
            column = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            column.FieldName = "Sort";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            column});
            column = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            column.FieldName = "ForecastID";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            column});

            column = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            column.FieldName = "Forecast";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            column});

            column = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            column.FieldName = "ID";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            column});

            column = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            column.FieldName = "ParentID";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            column});
        }
        //�����ݺ�����һ��
        private void AddColumn(int year)
        {
            // treeList1.Columns.Add(year + "��", typeof(double));
            //    TreeListColumn column = treeList1.Columns.Add();
            DevExpress.XtraTreeList.Columns.TreeListColumn column = new DevExpress.XtraTreeList.Columns.TreeListColumn();



            column.FieldName = "y" + year;
            column.Tag = year;
            column.Caption = year + "��";
            column.Name = year.ToString();
            column.Width = 100;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = year;//������������
         //   column.ColumnEdit = repositoryItemTextEdit1;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            column});

        }
        private void Form1_F_Load(object sender, EventArgs e)
        {
            DataTable dts = new DataTable();
            dts.Columns.Add("ID", typeof(int));
            dts.Columns.Add("Name", typeof(string));

            DataRow dr = dts.NewRow();
            dr["ID"] = 5;
            dr["Name"] = "�����˿�����";
            dts.Rows.Add(dr);
            if (!ISGDP)
            {
                dr = dts.NewRow();
                dr["Name"] = "��������";
                dr["ID"] = 6;
                dts.Rows.Add(dr);

                dr = dts.NewRow();
                dr["Name"] = "��������";
                dr["ID"] = 7;
                dts.Rows.Add(dr);

                dr = dts.NewRow();
                dr["Name"] = "�������������";
                dr["ID"] = 4;
                dts.Rows.Add(dr);

                if (!maxhour)
                {
                    dr = dts.NewRow();
                    dr["Name"] = "�������õ��������";
                    dr["ID"] = 2;
                    dts.Rows.Add(dr);
                }


                dr = dts.NewRow();
                dr["Name"] = "�������õ��������";
                dr["ID"] = 3;
                dts.Rows.Add(dr);
            }
       

            gridControl1.DataSource = dts;

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = this.gridView1.GetDataRow(this.gridView1.FocusedRowHandle);
            if (dr == null)
                return;

            int id1 = (int)dr["ID"];
            Selectid = id1.ToString();
            typeFlag2 = id1;
            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();

            if (Flag=="1")
            {
                if (id1 == 4)
                {
                    LoadData2();
                }
                else
                {
                    LoadData1(id1);
                }
            }
            else if (Flag=="2")
            {
                LoadData1(id1);
            }
            
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;

        }
        private int GetInsertIndex(int year)
        {
            int nFixedColumns = typeof(PSP_Types).GetProperties().Length - 2;//ID��ParentID�в���treeList1.Columns��

            int nColumns = treeList1.Columns.Count;
            int nIndex = nFixedColumns;

            //��û�����

            if (nColumns == nFixedColumns)
            {
            }
            else//�Ѿ������
            {
                bool bFind = false;

                //���Ҵ����λ��

                for (int i = nFixedColumns + 1; i < nColumns; i++)
                {
                    //Tag������
                    int tagYear1 = (int)treeList1.Columns[i - 1].Tag;
                    int tagYear2 = (int)treeList1.Columns[i].Tag;
                    if (tagYear1 < year && tagYear2 > year)
                    {
                        nIndex = i;
                        bFind = true;
                        break;
                    }
                }

                //����������С֮��

                if (!bFind)
                {
                    int tagYear1 = (int)treeList1.Columns[nFixedColumns].Tag;
                    int tagYear2 = (int)treeList1.Columns[nColumns - 1].Tag;

                    if (tagYear1 > year)//�ȵ�һ�����С
                    {
                        nIndex = nFixedColumns;
                    }
                    else
                    {
                        nIndex = nColumns;
                    }
                }
            }

            return nIndex;
        }
        private void AddColumn2(int year)
        {
            int nInsertIndex = GetInsertIndex(year);

            dataTable.Columns.Add("y" + year, typeof(double));

            TreeListColumn column = treeList1.Columns.Insert(nInsertIndex);
            column.FieldName ="y"+ year;
            column.Tag = year;
            column.Caption = year + "��";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = nInsertIndex - 2;//������������
            //column.ColumnEdit = repositoryItemTextEdit1;
            //treeList1.RefreshDataSource();
        }
        private void LoadData2()
        {
            //��ʾ�ɰ�����ع���ʵ�����������һ��
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
            }

            PSP_Types psp_Type = new PSP_Types();
            psp_Type.Flag2 = 2;
            psp_Type.ProjectID = Itop.Client.MIS.ProgUID;
            IList listTypes = Common.Services.BaseService.GetList("SelectPSP_TypesByFlag2AndProjectID", psp_Type);

            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_Types));

            treeList1.DataSource = dataTable;

            treeList1.Columns["Title"].Caption = "������";
            treeList1.Columns["Title"].Width = 180;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["ProjectID"].VisibleIndex = -1;
            treeList1.Columns["ProjectID"].OptionsColumn.ShowInCustomizationForm = false;

            treeList1.Columns["Flag"].VisibleIndex = -1;
            treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;

            treeList1.Columns["Flag2"].VisibleIndex = -1;
            treeList1.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;
            //treeList1.Columns["ParentID"].VisibleIndex = -1;
            //treeList1.Columns["ParentID"].OptionsColumn.ShowInCustomizationForm = false;
            PSP_Years psp_Year = new PSP_Years();
            psp_Year.Flag = 2;
            IList<PSP_Years> listYears = Common.Services.BaseService.GetList<PSP_Years>("SelectPSP_YearsListByFlag", psp_Year);

            foreach (PSP_Years item in listYears)
            {
                AddColumn2(item.Year);
            }
            Application.DoEvents();
            LoadValues();
            treeList1.ExpandAll();
        }
        //��ȡ���ݣ��ɷ����ع���ʵ����
        private void LoadValues()
        {
            PSP_Values psp_Values = new PSP_Values();
            psp_Values.ID = 2;//��ID�ֶδ��Flag2��ֵ


            IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesListByFlag2", psp_Values);

            foreach (PSP_Values value in listValues)
            {
                TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
                if (node != null)
                {

                    node.SetValue("y" + value.Year, value.Value);
                }
            }
        }


        private void LoadData1(int id)
        {
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
            }
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            AddFixColumn();
            for (int i = startyear; i <= endyear; i++)
            {
                AddColumn(i);
            }
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
          
            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = id;
            psp_Type.Col4 = pid;
            IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);
            dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));
            treeList1.DataSource = dataTable;
            treeList1.Columns["Sort"].SortOrder = SortOrder.Ascending;
            treeList1.Columns["Title"].Caption = "������";
            treeList1.Columns["Title"].Width = 180;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;

            Application.DoEvents();
            treeList1.CollapseAll();
            //LoadValues1(htt);
            //treeList1.Nodes[0].Expanded = true;
            //treeList1.ExpandAll();
        }

   

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            

        }

    
        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListMultiSelection tm = treeList1.Selection;
            if (tm.Count == 0)
                return;

            DataRow dr = this.gridView1.GetDataRow(this.gridView1.FocusedRowHandle);
            if (dr == null)
                return;
            int id1 = (int)dr["ID"];
            hs.Clear();
            li.Clear();
            if (Flag=="1")//�ɰ�����ع���ʵ��
            {
                if (id1 != 4)
                {
                    foreach (TreeListNode tln in tm)
                    {

                        Ps_History pt1 = new Ps_History();
                        Ps_History pt = Services.BaseService.GetOneByKey<Ps_History>((string)tln["ID"]);
                        if (tln["Title"].ToString() == "�����õ���" || tln["Title"].ToString() == "���渺��")
                        {
                            pt.Title = pt.Title + "-" + tln.ParentNode["Title"];
                        }
                        else
                            if (!tln["Title"].ToString().Contains("����") && !tln["Title"].ToString().Contains("����"))
                            {
                                if (id1 == 2)
                                    pt.Title = pt.Title + "����";
                                else if (id1 == 3)
                                    pt.Title = pt.Title + "����";


                            }
                        if (!hs.ContainsValue(pt))
                            hs.Add(Guid.NewGuid().ToString(), pt);

                    }
                }
                else
                {
                    foreach (TreeListNode tln in tm)
                    {

                        PSP_Types pt = Services.BaseService.GetOneByKey<PSP_Types>((int)tln["ID"]);

                        if (!hs.ContainsValue(pt))
                            hs.Add(Guid.NewGuid().ToString(), pt);

                    }

                }
            }
            else//�°�����ع���ʵ��
            {
                foreach (TreeListNode tln in tm)
                {

                    Ps_History pt1 = new Ps_History();
                    Ps_History pt = Services.BaseService.GetOneByKey<Ps_History>((string)tln["ID"]);
                    if (tln["Title"].ToString() == "�����õ���" || tln["Title"].ToString() == "���渺��")
                    {
                        pt.Title = pt.Title + "-" + tln.ParentNode["Title"];
                    }
                    else
                        if (!tln["Title"].ToString().Contains("����") && !tln["Title"].ToString().Contains("����"))
                        {
                            if (id1 == 2)
                                pt.Title = pt.Title + "����";
                            else if (id1 == 3)
                                pt.Title = pt.Title + "����";


                        }
                    if (!hs.ContainsValue(pt))
                        hs.Add(Guid.NewGuid().ToString(), pt);

                }

            }

           
           

            if (hs.Count > 0)
                this.DialogResult = DialogResult.OK;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


    }
}