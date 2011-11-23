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
        public string type;          //类型
        public string pid;            //投资估算项目ID;
        public int devicenum;         //设备个数
        public double volt = 0;        //基准电压
        public string buildyear = "2010";//开工时间
        public string buildend = "2020";//完工时间
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
            column.Caption = "名称";
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
                    pt.Title = "架空线路";
                    pt.Typeqf = "pw-line";
                    pt.BuildYear = buildyear;
                    pt.BuildEnd = buildend;
                    listTypes.Add(pt);
                    Services.BaseService.Create<Ps_Table_TZMX>(pt);
                    pt = new Ps_Table_TZMX();
                    pt.ProjectID = pid;
                    pt.Title = "电缆线路";
                    pt.Typeqf = "pw-line";
                    pt.BuildYear = buildyear;
                    pt.BuildEnd = buildend;
                    listTypes.Add(pt);
                    Services.BaseService.Create<Ps_Table_TZMX>(pt);
                }
                //column = new DevExpress.XtraGrid.Columns.GridColumn();
                //column.FieldName = "Vol";
                //column.Caption = "静态投资"+buildend.ToString()+"年";
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
                    pt.Title = "配电室";
                    pt.Typeqf = "pw-pb";
                    pt.BuildYear = buildyear;
                    pt.BuildEnd = buildend;
                    listTypes.Add(pt);
                    Services.BaseService.Create<Ps_Table_TZMX>(pt);
                    pt = new Ps_Table_TZMX();
                    pt.ProjectID = pid;
                    pt.Title = "箱式变电站";
                    pt.Typeqf = "pw-pb";
                    pt.BuildYear = buildyear;
                    pt.BuildEnd = buildend;
                    listTypes.Add(pt);
                    Services.BaseService.Create<Ps_Table_TZMX>(pt);
                    pt = new Ps_Table_TZMX();
                    pt.ProjectID = pid;
                    pt.Title = "柱上配电变压器";
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
                    pt.Title = "开闭所";
                    pt.Typeqf = "pw-kg";
                    pt.BuildYear = buildyear;
                    pt.BuildEnd = buildend;
                    listTypes.Add(pt);
                    Services.BaseService.Create<Ps_Table_TZMX>(pt);
                    pt = new Ps_Table_TZMX();
                    pt.ProjectID = pid;
                    pt.Title = "环网柜";
                    pt.Typeqf = "pw-kg";
                    pt.BuildYear = buildyear;
                    pt.BuildEnd = buildend;
                    listTypes.Add(pt);
                    Services.BaseService.Create<Ps_Table_TZMX>(pt);
                    pt = new Ps_Table_TZMX();
                    pt.ProjectID = pid;
                    pt.Title = "柱上开关";
                    pt.Typeqf = "pw-kg";
                    pt.BuildYear = buildyear;
                    pt.BuildEnd = buildend;
                    listTypes.Add(pt);
                    Services.BaseService.Create<Ps_Table_TZMX>(pt);
                    pt = new Ps_Table_TZMX();
                    pt.ProjectID = pid;
                    pt.Title = "电缆分支箱";
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
            column.Caption = "建设期贷款利息";
            column.VisibleIndex = 2 + endyear - firyear+1;
            column.Width = 180;
            //column.OptionsColumn.AllowEdit = false;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});
            column = new DevExpress.XtraGrid.Columns.GridColumn();
            column.FieldName = "PrepChange";
            column.Caption = "预备费用";
            column.VisibleIndex = 3+endyear - firyear + 1;
            column.Width = 180;
            //column.OptionsColumn.AllowEdit = false;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});
            column = new DevExpress.XtraGrid.Columns.GridColumn();
            column.FieldName = "DynInvest";
            column.Caption = "动态投资";
            column.VisibleIndex = 4 + endyear - firyear + 1;
            column.Width = 180;
            //column.OptionsColumn.AllowEdit = false;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});
            this.gridControl1.DataSource = listTypes;

        }
        //添加年份后，新增一列
        private void AddColumn(int year)
        {
            DevExpress.XtraGrid.Columns.GridColumn column = new DevExpress.XtraGrid.Columns.GridColumn();

            column.FieldName = "y" + year;
            // column.Tag = year;
            column.Caption = "静态投资" + year.ToString() + "年";
            column.Name ="y" + year.ToString();
            column.Width = 120;
            column.VisibleIndex =year- Convert.ToInt32(buildyear)+2; ;
            //column.OptionsColumn.AllowEdit = false;
            //column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            column.VisibleIndex = year;//有两列隐藏列
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            column});

        }
        #region 公共属性
        /// <summary>
        /// 获取或设置"双击允许修改"标志
        /// </summary>
        //public bool AllowUpdate
        //{
        //    get { return _bAllowUpdate; }
        //    set { _bAllowUpdate = value; }
        //}

        ///// <summary>
        ///// 获取GridControl对象
        ///// </summary>
        //public GridControl GridControl
        //{
        //    get { return gridControl; }
        //}

        ///// <summary>
        ///// 获取bandedGridView1对象
        ///// </summary>
        //public BandedGridView GridView
        //{
        //    get { return this.gridView1; }
        //}

        /// <summary>
        /// 获取GridControl的数据源，即对象的列表
        /// </summary>
        public IList ObjectList
        {
            get { return this.gridControl1.DataSource as IList; }
        }

        /// <summary>
        /// 获取焦点对象，即FocusedRow
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