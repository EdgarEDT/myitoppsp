
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Common;
using Itop.Client.Common;
using DevExpress.XtraVerticalGrid;
using Itop.Client.Base;
namespace ItopVector.Tools
{
	public partial class FrmPSP_PlanListDialog : FormBase
	{

		#region 字段
		protected bool _isCreate = false;
		protected PSP_PlanList _obj = null;
		#endregion

		#region 构造方法
		public FrmPSP_PlanListDialog()
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
		public PSP_PlanList Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region 事件处理
		private void FrmPSP_PlanListDialog_Load(object sender, EventArgs e)
		{
			IList<PSP_PlanList> list = new List<PSP_PlanList>();
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
                    _obj.UID = Guid.NewGuid().ToString();
					Services.BaseService.Create<PSP_PlanList>(_obj);
				}
				else
				{
					Services.BaseService.Update<PSP_PlanList>(_obj);
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
	}
}
