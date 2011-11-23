
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

		#region �ֶ�
		protected bool _isCreate = false;
		protected Common_Type _obj = null;
        private string type = "";
		#endregion

		#region ���췽��
		public FrmCommon_TypeDialog()
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
		public Common_Type Object
		{
			get { return _obj; }
			set { _obj = value; }
		}


        /// <summary>
        /// ��ȡType����
        /// </summary>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
		#endregion

		#region �¼�����
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
                MsgBox.Show("�˴����ⲻ����Ϊ�գ���");
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

			//�����ѳɹ�
			return true;
		}
		#endregion
	}
}
