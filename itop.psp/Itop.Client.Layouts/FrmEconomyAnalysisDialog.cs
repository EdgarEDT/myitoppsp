
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Itop.Domain.Layouts;
using Itop.Common;
using Itop.Client.Common;
using Itop.Client.Base;
namespace Itop.Client.Layouts
{
	public partial class FrmEconomyAnalysisDialog : FormBase
	{

		#region �ֶ�
		protected bool _isCreate = false;
		protected EconomyAnalysis _obj = null;
		#endregion

		#region ���췽��
		public FrmEconomyAnalysisDialog()
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
		public EconomyAnalysis Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region �¼�����
		private void FrmEconomyAnalysisDialog_Load(object sender, EventArgs e)
		{
			IList<EconomyAnalysis> list = new List<EconomyAnalysis>();
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
					Services.BaseService.Create<EconomyAnalysis>(_obj);
				}
				else
				{
					Services.BaseService.Update<EconomyAnalysis>(_obj);
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
