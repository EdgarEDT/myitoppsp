
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
	public partial class FrmPs_Substation_AsDialog : FormBase
	{

		#region 字段
        int year = DateTime.Now.Year;
		protected bool _isCreate = false;
        protected Ps_Power _obj = null;

		#endregion

		#region 构造方法
		public FrmPs_Substation_AsDialog()
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
        public Ps_Power Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region 事件处理
		private void FrmPs_Substation_AsDialog_Load(object sender, EventArgs e)
		{
            int a = this.vGridControl.Rows.Count;
            for (int i = year - 4; i <= year; i++)
            {
                this.vGridControl.Rows["rowy" + i].Visible = true;
                this.vGridControl.Rows["rowy" + i].Properties.Caption = i + "年";
            }

            IList<Ps_Power> list = new List<Ps_Power>();
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
                    Services.BaseService.Create<Ps_Power>(_obj);
				}
				else
				{
                    Services.BaseService.Update<Ps_Power>(_obj);
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
