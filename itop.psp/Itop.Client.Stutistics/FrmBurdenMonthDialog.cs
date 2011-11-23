
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
using Itop.Domain.Table;
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
	public partial class FrmBurdenMonthDialog : FormBase
	{

		#region �ֶ�
		protected bool _isCreate = false;
		protected BurdenMonth _obj = null;
        private  int iyear=0;
		#endregion

		#region ���췽��
		public FrmBurdenMonthDialog()
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
		public BurdenMonth Object
		{
			get { return _obj; }
			set { _obj = value; }
		}

        string titlename = "";

        public string TitleName
        {
            get { return titlename; }
            set { titlename = value; }
        
        }

		#endregion

		#region �¼�����
		private void FrmBurdenMonthDialog_Load(object sender, EventArgs e)
		{
			IList<BurdenMonth> list = new List<BurdenMonth>();
			list.Add(Object);
			this.vGridControl.DataSource = list;
            string pjt = " ProjectID='" + MIS.ProgUID + "'";

            IList<PS_Table_AreaWH> lt = Common.Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", pjt);
            repositoryItemLookUpEdit1.DataSource = lt;
            

            this.Text = titlename;
            this.tabPage.Text = titlename;

            if (!IsCreate)
            {
                iyear = Object.BurdenYear;
            }
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
            if (Object.AreaID == "" || Object.AreaID==null)
            {
                MsgBox.Show("��ѡ�����!");
                return;
            }
            if (IsCreate && !InputCheck())
            {
                MsgBox.Show("�Ѵ��ڴ���ȣ����������롣");
                return;
            }
            if (!IsCreate && !InputCheck() && iyear != _obj.BurdenYear)
            {
                MsgBox.Show("�Ѵ��ڴ���ȣ����������롣");
                return;
            }


            //if (!InputCheck())
            //{
            //    return;
            //}

			if (SaveRecord())
			{
				DialogResult = DialogResult.OK;
			}
		}
		#endregion

		#region ��������
		protected bool InputCheck()
		{

            IList<BurdenMonth> bm = Services.BaseService.GetList<BurdenMonth>("SelectBurdenMonthByWhere", "BurdenYear ='" + _obj.BurdenYear + "' and uid like '%" + Itop.Client.MIS.ProgUID + "%'");
            if (bm.Count>0)
            {
                
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
					Services.BaseService.Create<BurdenMonth>(_obj);
				}
				else
				{
					Services.BaseService.Update<BurdenMonth>(_obj);
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

        private InputLanguage oldInput = null;
        private void vGridControl_FocusedRowChanged(object sender, DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraEditors.Repository.RepositoryItemTextEdit edit = e.Row.Properties.RowEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            if (edit != null && edit.Mask.MaskType == DevExpress.XtraEditors.Mask.MaskType.Numeric)
            {
                oldInput = InputLanguage.CurrentInputLanguage;
                InputLanguage.CurrentInputLanguage = null;
            }
            else
            {
                if (oldInput != null && oldInput != InputLanguage.CurrentInputLanguage)
                {
                    InputLanguage.CurrentInputLanguage = oldInput;
                }
            }
        }
	}
}
