using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Forecast;
using Itop.Client.Base;
using System.Collections;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors;

namespace Itop.Client.Forecast
{
    public partial class FormLoadForecastDataFSH : FormBase
    {
        string projectUID = "";
        int typeflag = 1;

        public string ProjectUID
        {
            get { return projectUID; }
            set { projectUID = value; }

        }

        DataRow ro = null;
        public DataRow ROW
        {
            get 
            {
                ro = this.gridView2.GetDataRow(this.gridView2.FocusedRowHandle);
                return ro;
            }
            set { ro = value; }
        }


        public FormLoadForecastDataFSH()
        {
            InitializeComponent();
        }
        public FormLoadForecastDataFSH(int m)
        {
            typeflag = m;
            InitializeComponent();
        }

        private void FormLoadForecastData_Load(object sender, EventArgs e)
        {
            InitForm();
            InitData();
        }
        private void InitForm()
        {
            barEditItem1.EditValue = "�Ƽ�ֵ";
            

            barButtonItem1.Glyph = Itop.ICON.Resource.��Ȩ;
            barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

            barButtonItem2.Glyph = Itop.ICON.Resource.�ر�;
            barButtonItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
        }

        private void InitData()
        {
            Ps_forecast_list report = new Ps_forecast_list();
            report.UserID = ProjectUID;
            report.Col1 =typeflag.ToString();
            IList<Ps_forecast_list> listReports = Common.Services.BaseService.GetList<Ps_forecast_list>("SelectPs_forecast_listByCOL1AndUserID", report);

            gridControl1.DataSource = listReports;

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SelectData();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView2.FocusedRowHandle < 0)
            { MessageBox.Show("��ѡ��һ�����ݣ�"); return; }

            this.DialogResult = DialogResult.OK;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barEditItem1_EditValueChanged(object sender, EventArgs e)
        {
            SelectData();
        }


        private void SelectData()
        {
            object row = this.gridView1.GetRow(this.gridView1.FocusedRowHandle);
            if (row == null)
                return;
            
            Ps_forecast_list pf = row as Ps_forecast_list;

            string id = pf.ID;
            int m = 1;
            switch (barEditItem1.EditValue.ToString())
            {
                case "�������ʷ�":
                    m = 1;
                    break;
                case "���Ʒ�":
                    m = 2;
                    break;
                case "ָ��ƽ����":
                    m = 3;
                    break;
                case "��ɫ���۷�":
                    m = 6;
                    break;
                case "�����㷨":
                    m = 20;
                    break;
                case "�����������Сʱ��":
                   m =15;
                   break;
                 case "�Ƽ�ֵ":
                   m = 31;
                   break;
                 case "ר�Ҿ��߷�":
                   m = 7;
                   break;
            }
            DataTable dataTable = new DataTable();

            bool bLoadingData = true;
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                this.gridView2.Columns.Clear();
            }
            AddFixColumn();

            for (int i = pf.StartYear; i <= pf.EndYear; i++)
            {
                AddColumn(i);
            }

            if (m == 31)
            {
                string sql2 = " Col4='yes' and Forecast=" + m + " and ParentID='' and ForecastID='" + id + "'";
                IList list = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByWhere", sql2);
                dataTable = Itop.Common.DataConverter.ToDataTable(list, typeof(Ps_Forecast_Math));
            }
            else
            {
                Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
                psp_Type.ForecastID = id;
                psp_Type.Forecast = m;
                IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);

                dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Forecast_Math));
            }
           
            this.gridControl2.DataSource = dataTable;

            Application.DoEvents();
            bLoadingData = false;
        }

        //��ӹ̶���
        private void AddFixColumn()
        {
            // treeList1.Columns.Add(year + "��", typeof(double));

            //DevExpress.XtraTreeList.Columns.TreeListColumn column = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            GridColumn column = new GridColumn();
            column.FieldName = "Title";
            column.Caption = "������";
            column.VisibleIndex = 0;
            column.Width = 180;
            this.gridView2.Columns.Add(column);

            column = new GridColumn();
            column.FieldName = "Sort";
            column.VisibleIndex = -1;
            this.gridView2.Columns.Add(column);
            column = new GridColumn();
            column.FieldName = "ForecastID";
            column.VisibleIndex = -1;
            this.gridView2.Columns.Add(column);

            column = new GridColumn();
            column.FieldName = "Forecast";
            column.VisibleIndex = -1;
            this.gridView2.Columns.Add(column);

            column = new GridColumn();
            column.FieldName = "ID";
            column.VisibleIndex = -1;
            this.gridView2.Columns.Add(column);

            column = new GridColumn();
            column.FieldName = "ParentID";
            column.VisibleIndex = -1;
            this.gridView2.Columns.Add(column);
        }
        //�����ݺ�����һ��
        private void AddColumn(int year)
        {
            // treeList1.Columns.Add(year + "��", typeof(double));
            //    TreeListColumn column = treeList1.Columns.Add();
            //DevExpress.XtraTreeList.Columns.TreeListColumn column = new DevExpress.XtraTreeList.Columns.TreeListColumn();

            GridColumn column = new GridColumn();

            column.FieldName = "y" + year;
            column.Tag = year;
            column.Caption = year + "��";
            column.Name = year.ToString();
            column.Width = 100;
            column.VisibleIndex = year;//������������
           
            column.DisplayFormat.FormatString = "#####################0.##";
            column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //column.ColumnEdit = repositoryItemTextEdit1;
            this.gridView2.Columns.Add(column);


        }
    }
}