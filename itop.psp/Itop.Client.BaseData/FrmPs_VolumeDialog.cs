
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Itop.Domain.BaseData;
using Itop.Common;
using Itop.Client.Common;
using System.Collections;
using DevExpress.XtraEditors;
using Itop.Client.Base;
namespace Itop.Client.BaseData
{
	public partial class FrmPs_VolumeDialog : FormBase
	{

		#region �ֶ�
		protected bool _isCreate = false;
		protected Ps_Volume _obj = null;
        private int ye = 0;
		#endregion

		#region ���췽��
		public FrmPs_VolumeDialog()
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
		public Ps_Volume Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region �¼�����
		private void FrmPs_VolumeDialog_Load(object sender, EventArgs e)
		{
			IList<Ps_Volume> list = new List<Ps_Volume>();
			list.Add(Object);
			this.vGridControl.DataSource = list;

            ye = Object.Years;
            IList<Ps_Volume> li = Services.BaseService.GetStrongList<Ps_Volume>();
            Hashtable ht = new Hashtable();

            foreach (Ps_Volume pv in li)
            {
                if (!IsCreate && ye == pv.Years)
                    continue;
                ht.Add(pv.Years, "");
            }

            for (int i = 1990; i < 2050; i++)
            {
                if (ht.ContainsKey(i))
                    continue;
                repositoryItemComboBox2.Items.Add(i);
            }

            if (IsCreate)
            {
                this.vGridControl.BeginUpdate();
                Object.Years = Convert.ToInt32(repositoryItemComboBox2.Items[0].ToString());
                Object.IsWaterFire = "ˮ��";
                this.vGridControl.EndUpdate();
            }
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
					Services.BaseService.Create<Ps_Volume>(_obj);
				}
				else
				{
					Services.BaseService.Update<Ps_Volume>(_obj);
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
