
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Itop.Domain.Stutistics;
using Itop.Common;
using Itop.Client.Common;
using System.Collections;
using Itop.Domain.PWTable;
using Itop.Client.Base;
namespace Itop.Client.PWTable
{
	public partial class FrmPW3cDialog : FormBase
	{

		#region 字段
		protected bool _isCreate = false;
		protected PW_tb3c _obj = null;
		#endregion

		#region 构造方法
		public FrmPW3cDialog()
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
		public PW_tb3c Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region 事件处理
		private void FrmProject_SumDialog_Load(object sender, EventArgs e)
		{
                     
            IList<PW_tb3c> list = new List<PW_tb3c>();
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

            if (_obj.col1 == "")
            {
                MsgBox.Show("供电区分类不能为空！");
                return false;
            }
            //if (_obj.Num == 0)
            //{
            //    MsgBox.Show("静态投资不能为空！");
            //    return false;
            //}
			return true;
		}

		protected bool SaveRecord()
		{
			//创建/修改 对象
			try
			{
				if (IsCreate)
				{
                    _obj.col4 = Itop.Client.MIS.ProgUID;
                    Services.BaseService.Create<PW_tb3c>(_obj);
				}
				else
				{
                    Services.BaseService.Update<PW_tb3c>(_obj);
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
