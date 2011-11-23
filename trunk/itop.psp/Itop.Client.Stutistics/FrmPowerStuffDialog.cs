
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
	public partial class FrmPowerStuffDialog : FormBase
	{

		#region �ֶ�
		protected bool _isCreate = false;
		protected PowerStuff _obj = null;
        protected bool _isInsert = false;
		#endregion

		#region ���췽��
		public FrmPowerStuffDialog()
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



        public bool IsInsert
        {
            get { return _isInsert; }
            set { _isInsert = value; }
        }

		/// <summary>
		/// ����Ķ���
		/// </summary>
		public PowerStuff Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region �¼�����
		private void FrmPowerStuffDialog_Load(object sender, EventArgs e)
		{
			IList<PowerStuff> list = new List<PowerStuff>();
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

            //if (_obj.ParentID != "")
            //{
            //    if ((int)Services.BaseService.GetObject("SelectPowerStuffByParentID", _obj.ParentID) == 0)
            //    {
            //        MsgBox.Show("��������ϼ�Ŀ¼");
            //        return false;
            //    }
            //}


            //if (_obj.PowerLineUID != "" && IsCreate)
            //{
            //    if ((int)Services.BaseService.GetObject("SelectPowerStuffByParentID", _obj.PowerLineUID) > 0)
            //    {
            //        MsgBox.Show("�Ѿ��������ͬ����Ŀ");
            //        return false;
            //    }
            //}


			try
			{
				if (IsCreate)
				{
                    if (!_isInsert)
                    {
                        Services.BaseService.Create<PowerStuff>(_obj);
                    }
                    else
                    {
                        Services.BaseService.Update("UpdatePowerStuffBySortID", _obj);
                        Services.BaseService.Create<PowerStuff>(_obj);
                    }

                    //Services.BaseService.Create<PowerStuff>(_obj);
				}
				else
				{
					Services.BaseService.Update<PowerStuff>(_obj);
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

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //FrmPowerLine dlg = new FrmPowerLine();
            //dlg.IsSelect = true;

            //if (dlg.ShowDialog() == DialogResult.OK)
            //{
            //    repositoryItemButtonEdit1.NullText = dlg.LineName;
            //    //_obj.StuffName = dlg.LineName;
            //    _obj.PowerLineUID = dlg.LineUID;
            //    _obj.ParentID = dlg.LineParentUID;

            //    if (!dlg.LineState)
            //    {
            //        _obj.StuffName = "    " + dlg.LineName;
            //        repositoryItemButtonEdit1.NullText = "    " + dlg.LineName;
            //    }
            //    else
            //    {
            //        _obj.StuffName = dlg.LineName;
            //        repositoryItemButtonEdit1.NullText = dlg.LineName;
            //    }
            //    textBox1.Focus();
            //}

        }
	}
}
