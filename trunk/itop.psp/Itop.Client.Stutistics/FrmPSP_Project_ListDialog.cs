
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
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
	public partial class FrmPSP_Project_ListDialog : FormBase
	{

		#region �ֶ�
		protected bool _isCreate = false;
		protected PSP_Project_List _obj = null;
		#endregion

		#region ���췽��
		public FrmPSP_Project_ListDialog()
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
		public PSP_Project_List Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region �¼�����
		private void FrmPSP_Project_ListDialog_Load(object sender, EventArgs e)
		{
			IList<PSP_Project_List> list = new List<PSP_Project_List>();
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
					Services.BaseService.Create<PSP_Project_List>(_obj);
				}
				else
				{
					Services.BaseService.Update<PSP_Project_List>(_obj);
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
