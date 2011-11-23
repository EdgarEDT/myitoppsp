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
    public partial class FormTzgsWH2pwXX : FormBase
    {
        public string type;          //����
        public string pid;            //Ͷ�ʹ�����ĿID;
        public int devicenum;         //�豸����
        public double volt = 0;        //��׼��ѹ
        public string buildyear = "2010";//����ʱ��
        public string buildend = "2020";//�깤ʱ��
        public FormTzgsWH2pwXX()
        {
            InitializeComponent();
        }
        private void initdata()
        {
            
                gridView1.Columns.Clear();
          
          
            string con = "ProjectID='" + pid + "'and Typeqf='" + type + "'";
            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Table_TZMXByValue", con);
            DevExpress.XtraGrid.Columns.GridColumn column = new DevExpress.XtraGrid.Columns.GridColumn();
            column = new DevExpress.XtraGrid.Columns.GridColumn();
            column.FieldName = "Title";
            column.Caption = "����";
            column.VisibleIndex = 0;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});
            if (type == "pw-line")
            {
                if (listTypes.Count==0)
                {
                    Ps_Table_TZMX pt = new Ps_Table_TZMX();
                    pt.ProjectID = pid;
                    pt.Title = "�ܿ���·";
                    pt.Typeqf = "pw-line";
                    pt.BuildYear = buildyear;
                    pt.BuildEnd = buildend;
                    listTypes.Add(pt);
                    Services.BaseService.Create<Ps_Table_TZMX>(pt);
                    pt = new Ps_Table_TZMX();
                    pt.ProjectID = pid;
                    pt.Title = "������·";
                    pt.Typeqf = "pw-line";
                    pt.BuildYear = buildyear;
                    pt.BuildEnd = buildend;
                    listTypes.Add(pt);
                    Services.BaseService.Create<Ps_Table_TZMX>(pt);
                }
                //column = new DevExpress.XtraGrid.Columns.GridColumn();
                //column.FieldName = "Vol";
                //column.Caption = "��̬Ͷ��"+buildend.ToString()+"��";
                //column.VisibleIndex = 1;
                //column.Width = 180;
               

                //column.OptionsColumn.AllowEdit = false;
            //    this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            //column});
            }
            else if (type == "pw-pb")
            {
                if (listTypes.Count == 0)
                {
                    Ps_Table_TZMX pt = new Ps_Table_TZMX();
                    pt.ProjectID = pid;
                    pt.Title = "�����";
                    pt.Typeqf = "pw-pb";
                    pt.BuildYear = buildyear;
                    pt.BuildEnd = buildend;
                    listTypes.Add(pt);
                    Services.BaseService.Create<Ps_Table_TZMX>(pt);
                    pt = new Ps_Table_TZMX();
                    pt.ProjectID = pid;
                    pt.Title = "��ʽ���վ";
                    pt.Typeqf = "pw-pb";
                    pt.BuildYear = buildyear;
                    pt.BuildEnd = buildend;
                    listTypes.Add(pt);
                    Services.BaseService.Create<Ps_Table_TZMX>(pt);
                    pt = new Ps_Table_TZMX();
                    pt.ProjectID = pid;
                    pt.Title = "��������ѹ��";
                    pt.Typeqf = "pw-pb";
                    pt.BuildYear = buildyear;
                    pt.BuildEnd = buildend;
                    listTypes.Add(pt);
                    Services.BaseService.Create<Ps_Table_TZMX>(pt);
                }

              
            }
            else if (type == "pw-kg")
            {

                if (listTypes.Count == 0)
                {
                    Ps_Table_TZMX pt = new Ps_Table_TZMX();
                    pt.ProjectID = pid;
                    pt.Title = "������";
                    pt.Typeqf = "pw-kg";
                    pt.BuildYear = buildyear;
                    pt.BuildEnd = buildend;
                    listTypes.Add(pt);
                    Services.BaseService.Create<Ps_Table_TZMX>(pt);
                    pt = new Ps_Table_TZMX();
                    pt.ProjectID = pid;
                    pt.Title = "������";
                    pt.Typeqf = "pw-kg";
                    pt.BuildYear = buildyear;
                    pt.BuildEnd = buildend;
                    listTypes.Add(pt);
                    Services.BaseService.Create<Ps_Table_TZMX>(pt);
                    pt = new Ps_Table_TZMX();
                    pt.ProjectID = pid;
                    pt.Title = "���Ͽ���";
                    pt.Typeqf = "pw-kg";
                    pt.BuildYear = buildyear;
                    pt.BuildEnd = buildend;
                    listTypes.Add(pt);
                    Services.BaseService.Create<Ps_Table_TZMX>(pt);
                    pt = new Ps_Table_TZMX();
                    pt.ProjectID = pid;
                    pt.Title = "���·�֧��";
                    pt.Typeqf = "pw-kg";
                    pt.BuildYear = buildyear;
                    pt.BuildEnd = buildend;
                    listTypes.Add(pt);
                    Services.BaseService.Create<Ps_Table_TZMX>(pt);
                }
            }
            int firyear = Convert.ToInt32(buildyear);
            int endyear = Convert.ToInt32(buildend);
            for (int i = firyear; i <= endyear; i++)
            {
                AddColumn(i);
            }
            column = new DevExpress.XtraGrid.Columns.GridColumn();
            column.FieldName = "LendRate";
            column.Caption = "�����ڴ�����Ϣ";
            column.VisibleIndex = 2 + endyear - firyear+1;
            column.Width = 180;
            //column.OptionsColumn.AllowEdit = false;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});
            column = new DevExpress.XtraGrid.Columns.GridColumn();
            column.FieldName = "PrepChange";
            column.Caption = "Ԥ������";
            column.VisibleIndex = 3+endyear - firyear + 1;
            column.Width = 180;
            //column.OptionsColumn.AllowEdit = false;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});
            column = new DevExpress.XtraGrid.Columns.GridColumn();
            column.FieldName = "DynInvest";
            column.Caption = "��̬Ͷ��";
            column.VisibleIndex = 4 + endyear - firyear + 1;
            column.Width = 180;
            //column.OptionsColumn.AllowEdit = false;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});
            this.gridControl1.DataSource = listTypes;

        }
        //�����ݺ�����һ��
        private void AddColumn(int year)
        {
            DevExpress.XtraGrid.Columns.GridColumn column = new DevExpress.XtraGrid.Columns.GridColumn();

            column.FieldName = "y" + year;
            // column.Tag = year;
            column.Caption = "��̬Ͷ��" + year.ToString() + "��";
            column.Name ="y" + year.ToString();
            column.Width = 120;
            column.VisibleIndex =year- Convert.ToInt32(buildyear)+2; ;
            //column.OptionsColumn.AllowEdit = false;
            //column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            column.VisibleIndex = year;//������������
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});

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
            get
            {
                return this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Ps_Table_TZMX;
            }
        }
        #endregion

        private void FormTzgsWH2pwXX_Load(object sender, EventArgs e)
        {
            initdata();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}