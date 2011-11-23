
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
	public partial class FrmPSP_Calc_SpringDialog : FormBase
	{

		#region �ֶ�
		protected bool _isCreate = false;
		protected PSP_Calc_Spring _obj = null;
		#endregion

		#region ���췽��
		public FrmPSP_Calc_SpringDialog()
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
		public PSP_Calc_Spring Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region �¼�����
		private void FrmPSP_Calc_SpringDialog_Load(object sender, EventArgs e)
		{
			IList<PSP_Calc_Spring> list = new List<PSP_Calc_Spring>();
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
					Services.BaseService.Create<PSP_Calc_Spring>(_obj);
				}
				else
				{
					Services.BaseService.Update<PSP_Calc_Spring>(_obj);
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
