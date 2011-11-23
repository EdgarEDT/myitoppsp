
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Itop.Domain.Stutistic;
using Itop.Common;
using Itop.Client.Common;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraVerticalGrid.Rows;
using Itop.Domain.Table;
using Itop.Client.Base;
namespace Itop.Client.Table
{
	public partial class FrmAddGDP : FormBase
	{
        private FrmGDP area;
        public FrmGDP Area
        {
            set { area = value; }
        }
        private string types1 = "";
        private string flags1 = "";
       
        public string Type
        {
            get { return types1; }
            set { types1 = value; }       
        }

        public string Flag
        {
            get { return flags1; }
            set { flags1 = value; }
        }

       
        public bool IsSelect
        {
            get { return isselect; }
            set { isselect = value; }
        }

        bool isselect = false;



		#region 字段
		protected bool _isCreate = false;
		protected Ps_Table_GDP _obj = null;
		#endregion

		#region 构造方法
        public FrmAddGDP()
		{
			InitializeComponent();
		}
		#endregion

		#region 公共属性
		/// <summary>
		/// true:创建对象　false:修改对象
		/// </summary>
		public bool IsCreate
		{
			get { return _isCreate; }
			set { _isCreate = value; }
		}

		/// <summary>
		/// 所邦定的对象
		/// </summary>
        public Ps_Table_GDP Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region 事件处理
		private void FrmSubstation_InfoDialog_Load(object sender, EventArgs e)
		{
            if (isselect)
                this.vGridControl.Enabled = false;

            if (!IsCreate)
            {
                checkEdit1.Visible = false;
            }


            IList<Ps_Table_GDP> list = new List<Ps_Table_GDP>();
			list.Add(_obj);
			this.vGridControl.DataSource = list;

		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (SaveRecord())
			{
                if (checkEdit1.Checked)
                {
                    area.InitGrid1();
                    _obj = new Ps_Table_GDP();
                    IList<Ps_Table_GDP> list1 = new List<Ps_Table_GDP>();
                    list1.Add(_obj);
                    this.vGridControl.DataSource = list1;
                }
                else
                {
                    DialogResult = DialogResult.OK;
                }
			}
		}
		#endregion

		#region 辅助方法

      
        private string projectid,parentid;
        public string ProjectID
        {
            set { projectid = value; }
        }
        public string ParentID
        {
            set { parentid = value; }
        }
		protected bool SaveRecord()
		{
			try
			{
                _obj.GDPper = Math.Round((_obj.Population == 0.0 ? 0.0 : 10000 * _obj.GDP / _obj.Population),2);
				if (IsCreate)
				{
                    //Ps_Table_GDP pg = new Ps_Table_GDP();
                    //pg.ID = _obj.ID;
                    //pg.GDP = _obj.GDP;
                    //pg.Population = _obj.Population;
                    //pg.Yearf = _obj.Yearf;
                    //pg.GDPrate = _obj.GDPrate;
                        _obj.ID += "|" + projectid;
                        _obj.ParentID = parentid;
                        _obj.ProjectID = projectid;
                        _obj.SortID = OperTable.GetGDPMaxSort() + 1;
                        IList<Ps_Table_GDP> list = Common.Services.BaseService.GetList<Ps_Table_GDP>("SelectPs_Table_GDPByConn", " ProjectID='" + _obj.ProjectID + "' and ParentID='" + _obj.ParentID + "' and Yearf='" + _obj.Yearf + "'");
                        if (list.Count>0)
                        {
                            MessageBox.Show("创建的年份不能够重复！");
                            _obj = new Ps_Table_GDP();
                            IList<Ps_Table_GDP> list1 = new List<Ps_Table_GDP>();
                            list1.Add(_obj);
                            this.vGridControl.DataSource = list1;
                            return false;
                        }
                        Services.BaseService.Create<Ps_Table_GDP>(_obj);
                   
                   
				}
				else
				{
                    IList<Ps_Table_GDP> list = Common.Services.BaseService.GetList<Ps_Table_GDP>("SelectPs_Table_GDPByConn", " ProjectID='" +_obj.ProjectID + "' and ParentID='" +_obj.ParentID+ "' and Yearf='" +_obj.Yearf + "'");
                     foreach (Ps_Table_GDP pg in list)
                    {
                         if (pg.ID!=_obj.ID)
                         {
                             MessageBox.Show("已经存在此年数据，请重新修改！");
                             return false;
                         }
                    }
                    
                    Services.BaseService.Update<Ps_Table_GDP>(_obj);
				}
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
				HandleException.TryCatch(exc);
				return false ;
			}

			//操作已成功
			return true;
		}
		#endregion

        private void vGridControl_FocusedRowChanged(object sender, DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs e)
        {
            if (e.Row.Properties.FieldName == "GDPrate")
            {
                int year = _obj.Yearf;
                if (year != 0)
                {
                    IList<Ps_Table_GDP> list = Common.Services.BaseService.GetList<Ps_Table_GDP>("SelectPs_Table_GDPByConn", " ProjectID='" + projectid + "' and ParentID='" + parentid + "' and Yearf<" + year + " order by Yearf Desc");
                    if (list.Count > 0)
                    {
                        _obj.GDPrate = Math.Round((_obj.GDP-list[0].GDP) / ((year - list[0].Yearf)*list[0].GDP),2);
                    }
                }
            }
        }

    }
}
