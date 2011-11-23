using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.HistoryValue;
using Itop.Common;
using Itop.Client.Base;
using System.Collections;
using System.Reflection;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Itop.Client.Chen;
using Itop.Domain.Table;
using Itop.Client.Table;
using Itop.Client.Forecast;
using Itop.Client.Stutistics;
using Itop.Domain.Stutistics;
using Itop.Client.Common;

namespace Itop.Client.Table
{
    public partial class FormTZGSXX : FormBase
    {
        public string type;          //����
        public string pid;            //Ͷ�ʹ�����ĿID;
        public int devicenum;         //�豸����
        public double volt = 0;        //��׼��ѹ
        public string buildyear = "2010";//����ʱ��
        public string buildend = "2020";//�깤ʱ��
        public FormTZGSXX()
        {
            InitializeComponent();
        }
        #region ��������
        /// <summary>
        /// ��ȡ������"˫�������޸�"��־
        /// </summary>
        //public bool AllowUpdate
        //{
        //    get { return _bAllowUpdate; }
        //    set { _bAllowUpdate = value; }
        //}

        ///// <summary>
        ///// ��ȡGridControl����
        ///// </summary>
        //public GridControl GridControl
        //{
        //    get { return gridControl; }
        //}

        ///// <summary>
        ///// ��ȡbandedGridView1����
        ///// </summary>
        //public BandedGridView GridView
        //{
        //    get { return this.gridView1; }
        //}

        /// <summary>
        /// ��ȡGridControl������Դ����������б�
        /// </summary>
        public IList ObjectList
        {
            get { return this.gridControl1.DataSource as IList; }
        }

        /// <summary>
        /// ��ȡ������󣬼�FocusedRow
        /// </summary>
        public Ps_Table_TZMX FocusedObject
        {
            get { 
                return this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Ps_Table_TZMX; }
        }
        #endregion
        DataTable dataTable=new DataTable();
        private void initdata()
        {
            
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
               gridView1.Columns.Clear();
            }
            DevExpress.XtraGrid.Columns.GridColumn column = new DevExpress.XtraGrid.Columns.GridColumn();
            if (type=="sub")
            {
                column = new DevExpress.XtraGrid.Columns.GridColumn();
                column.FieldName = "Title";
                column.Caption = "���վ����";
                column.VisibleIndex = 0;
                column.Width = 180;
                column.OptionsColumn.AllowEdit = false;
                this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});
                column = new DevExpress.XtraGrid.Columns.GridColumn();
                column.FieldName = "Vol";
                column.Caption = "����";
                column.VisibleIndex = 1;
                column.Width = 100;
                column.OptionsColumn.AllowEdit = false;
                this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});
                column = new DevExpress.XtraGrid.Columns.GridColumn();
                column.FieldName = "Num";
                column.Caption = "���̨��";
                column.VisibleIndex = 2;
                column.Width = 80;
                column.OptionsColumn.AllowEdit = false;
                this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});
            }
            else if (type=="line")
            {
                column = new DevExpress.XtraGrid.Columns.GridColumn();
                column.FieldName = "Title";
                column.Caption = "��·����";
                column.VisibleIndex = 0;
                column.Width = 180;
                column.OptionsColumn.AllowEdit = false;
                this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});
                column = new DevExpress.XtraGrid.Columns.GridColumn();
                column.FieldName = "Vol";
                column.Caption = "����";
                column.VisibleIndex = 1;
                column.Width = 100;
                column.OptionsColumn.AllowEdit = false;
                this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});
                column = new DevExpress.XtraGrid.Columns.GridColumn();
                column.FieldName = "Linetype";
                column.Caption = "�����ͺ�";
                column.VisibleIndex = 2;
                column.Width = 180;
                column.OptionsColumn.AllowEdit = false;
                this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});

            }
            column = new DevExpress.XtraGrid.Columns.GridColumn();
            column.FieldName = "ID";
            column.Caption = "ID";
            column.VisibleIndex = -1;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});
            column = new DevExpress.XtraGrid.Columns.GridColumn();
            column.FieldName = "ProjectID";
            column.Caption = "ProjectID";
            column.VisibleIndex = -1;
            column.Width = 100;
            column.OptionsColumn.AllowEdit = false;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});
            column = new DevExpress.XtraGrid.Columns.GridColumn();
            column.FieldName = "Typeqf";
            column.Caption = "Typeqf";
            column.VisibleIndex = -1;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});
            column = new DevExpress.XtraGrid.Columns.GridColumn();
            column.FieldName = "BuildYear";
            column.Caption = "����ʱ��";
            column.VisibleIndex =3;
            column.Width = 100;
            column.OptionsColumn.AllowEdit = false;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});
            column = new DevExpress.XtraGrid.Columns.GridColumn();
            column.FieldName = "BuildEnd";
            column.Caption = "�깤ʱ��";
            column.VisibleIndex = 4;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});

            int firyear = Convert.ToInt32(buildyear);
            int endyear = Convert.ToInt32(buildend);
            for (int i = firyear; i <= endyear;i++ )
            {
                AddColumn(i);
            }

           
            column = new DevExpress.XtraGrid.Columns.GridColumn();
            column.FieldName = "LendRate";
            column.Caption = "��������";
            column.VisibleIndex = 5;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});
            column = new DevExpress.XtraGrid.Columns.GridColumn();
            column.FieldName = "PrepChange";
            column.Caption = "Ԥ������";
            column.VisibleIndex = 6;
            column.Width = 100;
            column.OptionsColumn.AllowEdit = false;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});
            column = new DevExpress.XtraGrid.Columns.GridColumn();
            column.FieldName = "DynInvest";
            column.Caption = "��̬Ͷ��";
            column.VisibleIndex = 7;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});
            string con = "ProjectID='" + pid + "'and Typeqf='" + type + "'";
            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Table_TZMXByValue", con);

            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Table_TZMX));
            //dataTable = dc.GetSortTable(dataTable, "Flag", true);

            this.gridControl1.DataSource = listTypes;

        }
        //�����ݺ�����һ��
        private void AddColumn(int year)
        {
            DevExpress.XtraGrid.Columns.GridColumn column = new DevExpress.XtraGrid.Columns.GridColumn();

            column.FieldName = "y" + year;
           // column.Tag = year;
            column.Caption ="��̬Ͷ��" +year.ToString() + "��";
            column.Name = year.ToString();
            column.Width = 120;
            column.OptionsColumn.AllowEdit = false;
            //column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            column.VisibleIndex = year;//������������
             this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});
           
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (devicenum>gridView1.RowCount||devicenum==0)
            {
                Ps_Table_TZMX px = new Ps_Table_TZMX();
                FormTZGSXXdialog fd = new FormTZGSXXdialog();

                fd.type = type;
                fd.volt = volt;
                fd.buildyear =Convert.ToInt32(buildyear);
                fd.buildend =Convert.ToInt32(buildend) ;
                fd.tzmx = px;
                fd.ShowDialog();
                if (fd.DialogResult==DialogResult.OK)
                {
                    Ps_Table_TZMX pt = fd.tzmx;
                    pt.ProjectID = pid;
                    pt.Typeqf = type;
                    pt.BuildYear = buildyear;
                    pt.BuildEnd = buildend;
                    Services.BaseService.Create<Ps_Table_TZMX>(pt);
                }
                initdata();
            }
            else
            {
                MessageBox.Show("���վ����Ŀ�Ѿ��ﵽ!");
                return;
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (FocusedObject!=null)
            {
                FormTZGSXXdialog ft = new FormTZGSXXdialog();
                ft.type = type;
                ft.volt = volt;
                ft.buildyear = Convert.ToInt32(buildyear);
                ft.buildend = Convert.ToInt32(buildend);
                ft.tzmx = FocusedObject;
                ft.ShowDialog();
                if (ft.DialogResult==DialogResult.OK)
                {
                    Services.BaseService.Update("UpdatePs_Table_TZMX",ft.tzmx);
                }
                initdata();
            }
            else
            {
                MessageBox.Show("��ѡ��һ�����ݣ�");
                return;

            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (FocusedObject!=null)
            {
                if (MessageBox.Show("��ȷ��Ҫɾ������", "ɾ��", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Services.BaseService.Delete<Ps_Table_TZMX>(FocusedObject);
                    initdata();
                }
            }
        }

        private void FormTZGSXX_Load(object sender, EventArgs e)
        {
            initdata();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}