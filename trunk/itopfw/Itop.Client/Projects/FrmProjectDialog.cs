
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Itop.Domain.RightManager;
using Itop.Common;
using DevExpress.XtraEditors;
using Itop.Client.Base;
//using Itop.Client.Common;

namespace Itop.Client.Projects
{
	public partial class FrmProjectDialog : FormBase
	{

		#region �ֶ�
		protected bool _isCreate = false;
		protected Project _obj = null;
		#endregion

		#region ���췽��
		public FrmProjectDialog()
		{
			InitializeComponent();
		}
        public FrmProjectDialog(string str)
        {
            InitializeComponent();
            this.Text = "��Ŀ����";
            this.tabPage.Text = "��Ŀ����";
            this.rowProjectName.Properties.Caption = "��Ŀ����";
            this.rowProjectCode.Properties.Caption = "��Ŀ˵��";

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
		public Project Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region �¼�����
		private void FrmProjectDialog_Load(object sender, EventArgs e)
		{
			IList<Project> list = new List<Project>();
			list.Add(Object);
            string a = Object.Address;
            if (a != "")
            {
                Object.SortID = int.Parse(a);
            }
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
                _obj.Address = _obj.SortID.ToString();
				if (IsCreate)
				{

                    

					Services.BaseService.Create<Project>(_obj);
				}
				else
				{

                    
					Services.BaseService.Update<Project>(_obj);
				}
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
				//HandleException.TryCatch(exc);
				return false ;
			}

			//�����ѳɹ�
			return true;
		}
		#endregion

        private void repositoryItemColorEdit1_ColorChanged(object sender, EventArgs e)
        {
            //repositoryItemColorEdit1.ColorText = ((ColorEdit)sender).;
        }
	}
}
