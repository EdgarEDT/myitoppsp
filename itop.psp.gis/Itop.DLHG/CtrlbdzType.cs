
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
using Itop.Domain.Graphics;
#endregion

namespace Itop.DLGH
{
    public partial class CtrlbdzType : UserControl
	{
		#region ���췽��
        public CtrlbdzType()
		{
			InitializeComponent();
		}
		#endregion

		#region �ֶ�
		protected bool _bAllowUpdate = true;
        private string typeid = "";

      
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
        public string Typeid
        {
            get { return typeid; }
            set { typeid = value; }
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
        public IList<PSP_bdz_type> ObjectList
		{
			get { return this.gridControl.DataSource as IList<PSP_bdz_type>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
        public PSP_bdz_type FocusedObject
		{
            get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PSP_bdz_type; }
		}
		#endregion

		#region �¼�����
		private void gridView_DoubleClick(object sender, EventArgs e)
		{
			// �ж�"˫�������޸�"��־ 
            //if (!AllowUpdate)
            //{
            //    return;
            //}

            ////���������ڵ�Ԫ���У���༭�������
            //Point point = this.gridControl.PointToClient(Control.MousePosition);
            //if (GridHelper.HitCell(this.gridView, point))
            //{
            //    UpdateObject();
            //}

		}
		#endregion

		#region ��������
		/// <summary>
		/// ��ӡԤ��
		/// </summary>
		public void PrintPreview()
		{
			ComponentPrint.ShowPreview(this.gridControl, "�������ά��");
		}

		/// <summary>
		/// ˢ�±���е�����
		/// </summary>
		/// <returns>ture:�ɹ�  false:ʧ��</returns>
		public bool RefreshData()
		{
			try
			{
                PSP_bdz_type p=new PSP_bdz_type();
                p.col1=" col3='"+typeid+"' order by Name";
                IList<PSP_bdz_type> list = Services.BaseService.GetList<PSP_bdz_type>("SelectPSP_bdz_typeByWhere", p);
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
            PSP_bdz_type obj = new PSP_bdz_type();
            
			//ִ����Ӳ���
            using (FrmbdzTypeDialog dlg = new FrmbdzTypeDialog())
			{
				dlg.IsCreate = true;    //�����½���־
                dlg.Typeid = typeid;
				dlg.Object = obj;
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
            ObjectList.Add(obj);
            //ˢ�±�񣬲��������ж�λ���¶����ϡ�
            gridControl.RefreshDataSource();
            GridHelper.FocuseRow(this.gridView, obj);
            //RefreshData();
            //GridHelper.FocuseRow(this.gridView, obj);
            //gridView.FocusedRowHandle = gridView.RowCount;
		}

		/// <summary>
		/// �޸Ľ������
		/// </summary>
		public void UpdateObject()
		{
			//��ȡ�������
            PSP_bdz_type obj = FocusedObject;
			if (obj == null)
			{
				return;
			}
            int i=gridView.FocusedRowHandle;
			//���������һ������
            //LineType objCopy = new LineType();
            //DataConverter.CopyTo<LineType>(obj, objCopy);

			//ִ���޸Ĳ���
            using (FrmbdzTypeDialog dlg = new FrmbdzTypeDialog())
			{
                dlg.Object = obj;   //�󶨸���
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
            //RefreshData();
            gridControl.RefreshDataSource();
            gridView.FocusedRowHandle = i;
		}

		/// <summary>
		/// ɾ���������
		/// </summary>
		public void DeleteObject()
		{
			//��ȡ�������
            PSP_bdz_type obj = FocusedObject;
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
                Services.BaseService.Delete<PSP_bdz_type>(obj);
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
	}
}
