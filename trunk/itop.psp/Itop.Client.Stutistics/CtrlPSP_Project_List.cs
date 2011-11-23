
#region ���������ռ�
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Itop.Common;
using Itop.Client.Common;
using Itop.Domain.Stutistics;
#endregion

namespace Itop.Client.Stutistics
{
	public partial class CtrlPSP_Project_List : UserControl
	{
		#region ���췽��
		public CtrlPSP_Project_List()
		{
			InitializeComponent();
		}
		#endregion

		#region �ֶ�
		protected bool _bAllowUpdate = true;
		#endregion

		#region ��������
		/// <summary>
		/// ��ȡ������"˫�������޸�"��־
		/// </summary>
		public bool AllowUpdate
		{
			get { return _bAllowUpdate; }
			set { _bAllowUpdate = value; }
		}

        string flag = "";
        /// <summary>
        /// ��ȡ������"˫�������޸�"��־
        /// </summary>
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }

		/// <summary>
		/// ��ȡGridControl����
		/// </summary>
		public GridControl GridControl
		{
			get { return gridControl; }
		}

		/// <summary>
		/// ��ȡGridView����
		/// </summary>
		public GridView GridView
		{
			get { return gridView; }
		}

		/// <summary>
		/// ��ȡGridControl������Դ����������б�
		/// </summary>
		public IList<PSP_Project_List> ObjectList
		{
			get { return this.gridControl.DataSource as IList<PSP_Project_List>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
		public PSP_Project_List FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PSP_Project_List; }
		}
		#endregion

		#region �¼�����
		private void gridView_DoubleClick(object sender, EventArgs e)
		{
			// �ж�"˫�������޸�"��־ 
			if (!AllowUpdate)
			{
				return;
			}

			//���������ڵ�Ԫ���У���༭�������
			Point point = this.gridControl.PointToClient(Control.MousePosition);
			if (GridHelper.HitCell(this.gridView, point))
			{
                FormAddInfo_TouZiGuSuan tz = new FormAddInfo_TouZiGuSuan();
                tz.Isupdate = true;
                tz.ParentID = "0";
                tz.FlagId = flag;
                tz.Text = "�޸���Ŀ";
                tz.PowerUId = this.FocusedObject.ID;
               
                if (tz.ShowDialog() == DialogResult.OK)
                {
                    try
                    {

                        this.RefreshData();
                    }
                    catch
                    {
                    }
                }
			}

		}
		#endregion

		#region ��������
		/// <summary>
		/// ��ӡԤ��
		/// </summary>
		public void PrintPreview()
		{
			ComponentPrint.ShowPreview(this.gridControl, this.gridView.GroupPanelText);
		}

		/// <summary>
		/// ˢ�±���е�����
		/// </summary>
		/// <returns>ture:�ɹ�  false:ʧ��</returns>
		public bool RefreshData()
		{
            PSP_Project_List pp=new PSP_Project_List();
            pp.Flag2=flag;
			try
			{
                IList<PSP_Project_List> list = Services.BaseService.GetList<PSP_Project_List>("SelectPSP_Project_ListByFlag2", pp);
				this.gridControl.DataSource = list;
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
				HandleException.TryCatch(exc);
				return false;
			}

			return true;
		}

		/// <summary>
		/// ��Ӷ���
		/// </summary>
		public void AddObject()
		{
			//�����������Ƿ��Ѿ�����
			if (ObjectList == null)
			{
				return;
			}
			//�½�����
			PSP_Project_List obj = new PSP_Project_List();

			//ִ����Ӳ���
			using (FrmPSP_Project_ListDialog dlg = new FrmPSP_Project_ListDialog())
			{
				dlg.IsCreate = true;    //�����½���־
				dlg.Object = obj;
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//���¶�����뵽������
			ObjectList.Add(obj);

			//ˢ�±�񣬲��������ж�λ���¶����ϡ�
			gridControl.RefreshDataSource();
			GridHelper.FocuseRow(this.gridView, obj);
		}

		/// <summary>
		/// �޸Ľ������
		/// </summary>
		public void UpdateObject()
		{
			//��ȡ�������
			PSP_Project_List obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//���������һ������
			PSP_Project_List objCopy = new PSP_Project_List();
			DataConverter.CopyTo<PSP_Project_List>(obj, objCopy);

			//ִ���޸Ĳ���
			using (FrmPSP_Project_ListDialog dlg = new FrmPSP_Project_ListDialog())
			{
				dlg.Object = objCopy;   //�󶨸���
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//�ø������½������
			DataConverter.CopyTo<PSP_Project_List>(objCopy, obj);
			//ˢ�±��
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// ɾ���������
		/// </summary>
		public void DeleteObject()
		{
			//��ȡ�������
			PSP_Project_List obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//����ȷ��
			if (MsgBox.ShowYesNo(Strings.SubmitDelete) != DialogResult.Yes)
			{
				return;
			}

			//ִ��ɾ������
			try
			{
				Services.BaseService.Delete<PSP_Project_List>(obj);
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
				HandleException.TryCatch(exc);
				return;
			}

			this.gridView.BeginUpdate();
			//��ס��ǰ����������
			int iOldHandle = this.gridView.FocusedRowHandle;
			//��������ɾ��
			ObjectList.Remove(obj);
			//ˢ�±��
			gridControl.RefreshDataSource();
			//�����µĽ���������
			GridHelper.FocuseRowAfterDelete(this.gridView, iOldHandle);
			this.gridView.EndUpdate();
		}
		#endregion

        private void gridControl_Click(object sender, EventArgs e)
        {

        }
	}
}
