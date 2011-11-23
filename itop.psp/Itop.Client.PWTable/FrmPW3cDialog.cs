
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

		#region �ֶ�
		protected bool _isCreate = false;
		protected PW_tb3c _obj = null;
		#endregion

		#region ���췽��
		public FrmPW3cDialog()
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
		public PW_tb3c Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region �¼�����
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

		#region ��������
		protected bool InputCheck()
		{

            if (_obj.col1 == "")
            {
                MsgBox.Show("���������಻��Ϊ�գ�");
                return false;
            }
            //if (_obj.Num == 0)
            //{
            //    MsgBox.Show("��̬Ͷ�ʲ���Ϊ�գ�");
            //    return false;
            //}
			return true;
		}

		protected bool SaveRecord()
		{
			//����/�޸� ����
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

			//�����ѳɹ�
			return true;
		}
		#endregion
	}
}
