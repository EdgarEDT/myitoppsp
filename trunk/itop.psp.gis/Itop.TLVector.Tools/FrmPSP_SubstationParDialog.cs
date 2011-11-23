
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
using Itop.Client.Base;
namespace ItopVector.Tools
{
	public partial class FrmPSP_SubstationParDialog : FormBase
	{

		#region �ֶ�
		protected bool _isCreate = false;
		protected PSP_SubstationPar _obj = null;
		#endregion

		#region ���췽��
		public FrmPSP_SubstationParDialog()
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
		public PSP_SubstationPar Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region �¼�����
		private void FrmPSP_SubstationParDialog_Load(object sender, EventArgs e)
		{
			IList<PSP_SubstationPar> list = new List<PSP_SubstationPar>();
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
                    _obj.UID = Guid.NewGuid().ToString();
					Services.BaseService.Create<PSP_SubstationPar>(_obj);
				}
				else
				{
					Services.BaseService.Update<PSP_SubstationPar>(_obj);
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