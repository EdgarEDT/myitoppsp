
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
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
	public partial class FrmPowerEachTotalDialog : FormBase
	{

		#region 字段
		protected bool _isCreate = false;
		protected PowerEachTotal _obj = null;
        protected bool _isInsert = false;
		#endregion

		#region 构造方法
		public FrmPowerEachTotalDialog()
		{
			InitializeComponent();
		}
		#endregion

		#region 公共属性
		/// <summary>
		/// true:创建对象　false:修改对象
		/// </summary>
        /// 
        public bool IsInsert
        {
            get { return _isInsert; }
            set { _isInsert = value; }
        }

		public bool IsCreate
		{
			get { return _isCreate; }
			set { _isCreate = value; }
		}

		/// <summary>
		/// 所邦定的对象
		/// </summary>
		public PowerEachTotal Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region 事件处理
		private void FrmPowerEachTotalDialog_Load(object sender, EventArgs e)
		{
			IList<PowerEachTotal> list = new List<PowerEachTotal>();
			list.Add(Object);
			this.vGridControl.DataSource = list;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (!InputCheck())
			{
				return;
			}

			if (SaveRecord())
			{
				DialogResult = DialogResult.OK;
			}
		}
		#endregion

		#region 辅助方法
		protected bool InputCheck()
		{
			return true;
		}

		protected bool SaveRecord()
		{
			//创建/修改 对象
			try
			{
				if (IsCreate)
				{
                    //Services.BaseService.Create<PowerEachTotal>(_obj);

                    if (!_isInsert)
                    {
                        //if (Services.BaseService.GetObject("SelectPowerEachTotalBySortID", null) != null)
                        //{
                        //    _obj.SortID = (int)Services.BaseService.GetObject("SelectPowerEachTotalBySortID", null) + 1;
                        //}
                        //else
                        //{
                        //    _obj.SortID = 0;
                        //}

                        Services.BaseService.Create<PowerProject>(_obj);
                    }
                    else
                    {
                        Services.BaseService.Update("UpdatePowerEachTotalBySortID", _obj);
                        Services.BaseService.Create<PowerProject>(_obj);
                    }

                    //Services.BaseService.Create<PowerEachTotal>(_obj);
				}
				else
				{
					Services.BaseService.Update<PowerEachTotal>(_obj);
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

        private InputLanguage oldInput = null;
        private void vGridControl_FocusedRowChanged(object sender, DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraEditors.Repository.RepositoryItemTextEdit edit = e.Row.Properties.RowEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            if (edit != null && edit.Mask.MaskType == DevExpress.XtraEditors.Mask.MaskType.Numeric)
            {
                oldInput = InputLanguage.CurrentInputLanguage;
                InputLanguage.CurrentInputLanguage = null;
            }
            else
            {
                if (oldInput != null && oldInput != InputLanguage.CurrentInputLanguage)
                {
                    InputLanguage.CurrentInputLanguage = oldInput;
                }
            }
        }
	}
}
