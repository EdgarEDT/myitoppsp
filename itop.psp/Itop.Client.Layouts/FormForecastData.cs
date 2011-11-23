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
namespace Itop.Client.Layouts
{
    public partial class FormForecastData : FormBase
    {

        DataTable dataTable;

        private string lastEditValue = string.Empty;

        private int typeFlag2 = 1;
        private int Forecast = 0;

        private string ForecastID = string.Empty;
        Hashtable hs = new Hashtable();

        public Hashtable HS
        {
            set { hs = value; }
            get { return hs; }
        
        }
      
        int startyear = 0;

        public int StartYear
        {
            set { startyear = value; }
            get { return startyear; }

        }


        IList<Ps_History> li = new List<Ps_History>();

        
        int endyear = 0;

        public int EndYear
        {
            set { endyear = value; }
            get { return endyear; }

        }
        DataTable fore_data = new DataTable();
        private void add_basedata()
        {
            fore_data.Columns.Add("Name");
            fore_data.Columns.Add("Code");
            for (int i = 0; i < data_str.Length/2; i++)
            {
                DataRow temprow = fore_data.NewRow();
                temprow["Name"] = data_str[i, 0].ToString();
                temprow["Code"] = data_str[i, 1].ToString();
                fore_data.Rows.Add(temprow);
            }
             gridControl2.DataSource = fore_data;
        }
        private void Add_Fangan()
        {
            Ps_forecast_list report = new Ps_forecast_list();
            report.UserID =MIS.ProgUID;  //SetCfgValue("lastLoginUserNumber", Application.ExecutablePath + ".config");
            report.Col1 = "1";
            IList listReports = Common.Services.BaseService.GetList("SelectPs_forecast_listByCOL1AndUserID", report);
            DataTable temptable = new DataTable();
            temptable = Itop.Common.DataConverter.ToDataTable(listReports, typeof(Ps_forecast_list));
            gridControl3.DataSource = temptable;
        }
        string[,] data_str ={ { "年增长率法", "1" }, { "外推法", "2" }, { "相关法", "3" }, { "灰色模型法", "6" }, { "专家决策法", "7" }, { "指数据平滑法", "5" }, { "弹性系数法", "4" }, { "复合算法", "20" } };
        public FormForecastData()
        {
            InitializeComponent();
            add_basedata();
            Add_Fangan();
        }
        private void LoadData(int type)
        {
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                this.treeList1.Columns.Clear();

                //treeList1.Columns.Clear();
            }
            //((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            AddFixColumn();

            for (int i = StartYear; i < EndYear; i++)
            {
                AddColumn(i);
            }
            //((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.ForecastID = ForecastID;
            psp_Type.Forecast = type;
            //psp_Type.Col4 = MIS.ProgUID;
            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);

            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Forecast_Math));

            this.treeList1.DataSource = dataTable;



            Application.DoEvents();

            
          


        }
        //添加固定列
        private void AddFixColumn()
        {
            TreeListColumn column = new TreeListColumn();
            column.FieldName = "Title";
            column.Caption = "分类名";
            column.VisibleIndex = 0;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
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
            //column.DisplayFormat.FormatString = "#####################0.##";
            //column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            column.Format.FormatString = "#####################0.##";
            column.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});

        }

        private void FormForecastData_Load(object sender, EventArgs e)
        {
           
            LoadData(0);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            
            LoadData(Convert.ToInt32(gridView2.GetFocusedRowCellValue("Code").ToString()));
        }

        private void gridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ForecastID = gridView3.GetFocusedRowCellValue("ID").ToString();
            LoadData(Forecast);
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Forecast = Convert.ToInt32(gridView2.GetFocusedRowCellValue("Code").ToString());
            LoadData(Convert.ToInt32(gridView2.GetFocusedRowCellValue("Code").ToString()));

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode node = treeList1.FocusedNode;
            Ps_Forecast_Math pt = Services.BaseService.GetOneByKey<Ps_Forecast_Math>(node["ID"].ToString());
            if (!hs.ContainsValue(pt))
                hs.Add(Guid.NewGuid().ToString(), pt);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

    }
}