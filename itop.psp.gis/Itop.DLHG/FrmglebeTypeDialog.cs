
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
namespace Itop.DLGH
{
	public partial class FrmglebeTypeDialog : FormBase
	{

		#region �ֶ�
		protected bool _isCreate = false;
		protected glebeType _obj = null;
		#endregion

		#region ���췽��
		public FrmglebeTypeDialog()
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
		public glebeType Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region �¼�����
		private void FrmglebeTypeDialog_Load(object sender, EventArgs e)
		{

            TypeID.DataBindings.Add("Text", _obj, "TypeID");
            TypeName.DataBindings.Add("Text", _obj, "TypeName");
            TypeStyle.DataBindings.Add("Text", _obj, "TypeStyle");
            txtxs.DataBindings.Add("Text", _obj, "ObligateField2");
            txtrjl.DataBindings.Add("Text", _obj, "ObligateField3");
            if (_obj.ObligateField1 == "") { _obj.ObligateField1 = Color.Black.ToArgb().ToString(); }
            if (_obj.ObligateField2 == "" || _obj.ObligateField2==null) { _obj.ObligateField2 = "1"; txtxs.Text = _obj.ObligateField2; }
            if (_obj.ObligateField3 == "" || _obj.ObligateField3 == null) { _obj.ObligateField3 = "1"; txtrjl.Text = _obj.ObligateField3; }
            TypeColor.Color = Color.FromArgb(Convert.ToInt32(_obj.ObligateField1));
            if (_obj.UID == "6ab9af7b-3d97-4e6c-8ed7-87b76950b90b")
            {
                TypeID.Properties.ReadOnly = true;
                TypeName.Properties.ReadOnly = true;
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
            if(_obj.TypeID==""){
                MessageBox.Show("�ؿ��Ų���Ϊ��","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return false;
            }
            if (_obj.TypeName == "")
            {
                MessageBox.Show("�ؿ����Ʋ���Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (_obj.TypeName == "")
            {
                MessageBox.Show("�����ܶ�ָ�겻��Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (_obj.ObligateField2 == "")
            {
                MessageBox.Show("����ϵ������Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (_obj.ObligateField3 == "")
            {
                MessageBox.Show("�ݻ��ʲ���Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
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
                    _obj.ObligateField1 = TypeColor.Color.ToArgb().ToString();
					Services.BaseService.Create<glebeType>(_obj);
				}
				else
				{
                    _obj.ObligateField1 = TypeColor.Color.ToArgb().ToString();
					Services.BaseService.Update<glebeType>(_obj);
                    
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
