
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

		#region �ֶ�
		protected bool _isCreate = false;
		protected PSP_35KVPingHeng _obj = null;
		#endregion

		#region ���췽��
		public FrmPSP_35KVPingHengDialog()
		{
			InitializeComponent();
		}
		#endregion

		#region ��������
		/// <summary>
		/// true:��������false:�޸Ķ���
		/// </summary>
		public bool IsCreate
		{
			get { return _isCreate; }
			set { _isCreate = value; }
		}

		/// <summary>
		/// ����Ķ���
		/// </summary>
		public PSP_35KVPingHeng Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region �¼�����
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

		#region ��������
		protected bool InputCheck()
		{
			return true;
		}

		protected bool SaveRecord()
		{
			//����/�޸� ����
			try
			{
				if (IsCreate)
				{
                    DateTime dtime = DateTime.Now;
                    _obj.UID = Guid.NewGuid().ToString();
                    _obj.CreateTime = dtime;
                    _obj.Title = "��󹩵縺��";
                    _obj.SortID2 = 1;
					Services.BaseService.Create<PSP_35KVPingHeng>(_obj);

                    //dtime=dtime.AddSeconds(1);
                    _obj.UID = Guid.NewGuid().ToString();
                    _obj.CreateTime = dtime;
                    _obj.Title = "����ƽ�⸺��";
                    _obj.SortID2 = 2;
                    Services.BaseService.Create<PSP_35KVPingHeng>(_obj);

                    //dtime = dtime.AddSeconds(1);
                    _obj.UID = Guid.NewGuid().ToString();
                    _obj.CreateTime = dtime;
                    _obj.Title = "��Ҫ35ǧ���������";
                    _obj.SortID2 = 4;
                    Services.BaseService.Create<PSP_35KVPingHeng>(_obj);

                    //dtime = dtime.AddSeconds(1);
                    _obj.UID = Guid.NewGuid().ToString();
                    _obj.CreateTime = dtime;
                    _obj.Title = "����35ǧ���������";
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

			//�����ѳɹ�
			return true;
		}
		#endregion
	}
}
