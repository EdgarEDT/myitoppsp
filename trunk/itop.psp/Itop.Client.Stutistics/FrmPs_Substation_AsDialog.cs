
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

		#region �ֶ�
        int year = DateTime.Now.Year;
		protected bool _isCreate = false;
        protected Ps_Power _obj = null;

		#endregion

		#region ���췽��
		public FrmPs_Substation_AsDialog()
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
        public Ps_Power Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region �¼�����
		private void FrmPs_Substation_AsDialog_Load(object sender, EventArgs e)
		{
            int a = this.vGridControl.Rows.Count;
            for (int i = year - 4; i <= year; i++)
            {
                this.vGridControl.Rows["rowy" + i].Visible = true;
                this.vGridControl.Rows["rowy" + i].Properties.Caption = i + "��";
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

			//�����ѳɹ�
			return true;
		}
		#endregion
	}
}
