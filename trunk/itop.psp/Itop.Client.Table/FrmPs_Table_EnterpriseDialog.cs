
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Itop.Domain.Table;
using Itop.Common;
using Itop.Client.Common;
using System.Collections;
using Itop.Client.Base;
namespace Itop.Domain.Table
{
	public partial class FrmPs_Table_EnterpriseDialog : FormBase
	{

		#region 字段
		protected bool _isCreate = false;
		protected Ps_Table_Enterprise _obj = null;
		#endregion

		#region 构造方法
		public FrmPs_Table_EnterpriseDialog()
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
		public Ps_Table_Enterprise Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region 事件处理
		private void FrmPs_Table_EnterpriseDialog_Load(object sender, EventArgs e)
		{
			IList<Ps_Table_Enterprise> list = new List<Ps_Table_Enterprise>();
			list.Add(Object);
			this.vGridControl.DataSource = list;

            string sql = " ProjectID='" + Itop.Client.MIS.ProgUID + "'";
            IList<PS_Table_AreaWH> list2= Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn",sql);
            for(int i=0;i<list2.Count;i++){
                repositoryItemComboBox2.Items.Add(list2[i].Title);
            }
            for (int i = 1900; i < 2060; i++)
            {
                repositoryItemComboBox3.Items.Add(i.ToString());
            }
            //repositoryItemComboBox2
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
                    _obj.ProjectID = Itop.Client.MIS.ProgUID;
					Services.BaseService.Create<Ps_Table_Enterprise>(_obj);
				}
				else
				{
					Services.BaseService.Update<Ps_Table_Enterprise>(_obj);
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
