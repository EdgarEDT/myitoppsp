
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Itop.Domain.GM;
using Itop.Common;
using Itop.Client.Common;
using Itop.Client.Base;
namespace Itop.Client.GM
{
	public partial class FrmCommon_TypeDialog : FormBase
	{

		#region 字段
		protected bool _isCreate = false;
		protected Common_Type _obj = null;
        private string type = "";
		#endregion

		#region 构造方法
		public FrmCommon_TypeDialog()
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
		public Common_Type Object
		{
			get { return _obj; }
			set { _obj = value; }
		}


        /// <summary>
        /// 获取Type对象
        /// </summary>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
		#endregion

		#region 事件处理
		private void FrmCommon_TypeDialog_Load(object sender, EventArgs e)
		{
			IList<Common_Type> list = new List<Common_Type>();
			list.Add(Object);
			this.vGridControl.DataSource = list;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
            if (Object.Title == "" || Object.Title == null)
            {
                MsgBox.Show("此处标题不可以为空！！");
                return;
            }
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
					Services.BaseService.Create<Common_Type>(_obj);
				}
				else
				{
					Services.BaseService.Update<Common_Type>(_obj);
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
