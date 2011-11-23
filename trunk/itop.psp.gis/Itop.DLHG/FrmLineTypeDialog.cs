
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
	public partial class FrmLineTypeDialog : FormBase
	{

		#region �ֶ�
		protected bool _isCreate = false;
		protected LineType _obj = null;
		#endregion

		#region ���췽��
        public FrmLineTypeDialog()
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
        public LineType Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region �¼�����
		private void FrmglebeTypeDialog_Load(object sender, EventArgs e)
		{
			
            textBox1.Text = Object.TypeName;
            lineWidth.Text = Object.ObligateField1;
            if (Object.Color == "") { Object.Color = Color.Black.ToArgb().ToString(); }
            colorEdit1.Color = Color.FromArgb(Convert.ToInt32(Object.Color));
            if (_obj.UID == "3afdf1a2-0992-4d44-a597-cd89aba785c6" || _obj.UID == "516a2d67-b19a-4c47-8fbf-2f85c601af2d" || _obj.UID == "9c646f34-4b43-4166-99c9-15ad7ec394a3")
            {
                textBox1.Properties.ReadOnly = true;
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
            if (textBox1.Text == "")
            {
                MessageBox.Show("��ѹ�ȼ�����Ϊ��","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return false;
            }
            if (lineWidth.Text == "")
            {
                MessageBox.Show("������Ȳ���Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (lineWidth.Text == "0")
            {
                MessageBox.Show("������Ȳ���Ϊ0", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    _obj.TypeName = textBox1.Text;
                    _obj.Color = colorEdit1.Color.ToArgb().ToString();
                    _obj.ObligateField1 = lineWidth.Text;
                    Services.BaseService.Create<LineType>(_obj);
				}
				else
				{
                    _obj.TypeName = textBox1.Text;
                    _obj.Color = colorEdit1.Color.ToArgb().ToString();
                    _obj.ObligateField1 = lineWidth.Text;
                    Services.BaseService.Update<LineType>(_obj);
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

        private void lineWidth_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void lineWidth_KeyUp(object sender, KeyEventArgs e)
        {
            lineWidth.Text = lineWidth.Text.Replace("-", "");
        }
	}
}
