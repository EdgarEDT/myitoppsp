
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Itop.Domain.Chen;
using Itop.Common;
using Itop.Client.Common;
using Itop.Client.Base;
namespace Itop.Client.Chen
{
	public partial class FrmPSP_35KVPingHengDialog : FormBase
	{

		#region 字段
		protected bool _isCreate = false;
		protected PSP_35KVPingHeng _obj = null;
		#endregion

		#region 构造方法
		public FrmPSP_35KVPingHengDialog()
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
		public PSP_35KVPingHeng Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region 事件处理
		private void FrmPSP_35KVPingHengDialog_Load(object sender, EventArgs e)
		{
			IList<PSP_35KVPingHeng> list = new List<PSP_35KVPingHeng>();
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
                    DateTime dtime = DateTime.Now;
                    _obj.UID = Guid.NewGuid().ToString();
                    _obj.CreateTime = dtime;
                    _obj.Title = "最大供电负荷";
                    _obj.SortID2 = 1;
					Services.BaseService.Create<PSP_35KVPingHeng>(_obj);

                    //dtime=dtime.AddSeconds(1);
                    _obj.UID = Guid.NewGuid().ToString();
                    _obj.CreateTime = dtime;
                    _obj.Title = "本地平衡负荷";
                    _obj.SortID2 = 2;
                    Services.BaseService.Create<PSP_35KVPingHeng>(_obj);

                    //dtime = dtime.AddSeconds(1);
                    _obj.UID = Guid.NewGuid().ToString();
                    _obj.CreateTime = dtime;
                    _obj.Title = "需要35千伏变电容量";
                    _obj.SortID2 = 4;
                    Services.BaseService.Create<PSP_35KVPingHeng>(_obj);

                    //dtime = dtime.AddSeconds(1);
                    _obj.UID = Guid.NewGuid().ToString();
                    _obj.CreateTime = dtime;
                    _obj.Title = "已有35千伏变电容量";
                    _obj.SortID2 = 5;
                    Services.BaseService.Create<PSP_35KVPingHeng>(_obj);

				}
				else
				{
                    Services.BaseService.Update("UpdatePSP_35KVPingHengByTypeAndTitle", _obj);
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
