
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
    public partial class CtrlLineType : UserControl
	{
		#region ���췽��
		public CtrlLineType()
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
        public IList<LineType> ObjectList
		{
			get { return this.gridControl.DataSource as IList<LineType>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
        public LineType FocusedObject
		{
            get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as LineType; }
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
				UpdateObject();
			}

		}
		#endregion

		#region ��������
		/// <summary>
		/// ��ӡԤ��
		/// </summary>
		public void PrintPreview()
		{
			ComponentPrint.ShowPreview(this.gridControl, "��·����ά��");
		}

		/// <summary>
		/// ˢ�±���е�����
		/// </summary>
		/// <returns>ture:�ɹ�  false:ʧ��</returns>
		public bool RefreshData()
		{
			try
			{
                IList<LineType> list = Services.BaseService.GetStrongList<LineType>();
                for (int i = 0; i < list.Count;i++ )
                {
                    list[i].ObjColor=Color.FromArgb(Convert.ToInt32(list[i].Color));
                }
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
            LineType obj = new LineType();
            
			//ִ����Ӳ���
            using (FrmLineTypeDialog dlg = new FrmLineTypeDialog())
			{
				dlg.IsCreate = true;    //�����½���־
				dlg.Object = obj;
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
            obj.ObjColor = Color.FromArgb(Convert.ToInt32(obj.Color));
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
            LineType obj = FocusedObject;
			if (obj == null)
			{
				return;
			}
            int i=gridView.FocusedRowHandle;
			//���������һ������
            //LineType objCopy = new LineType();
            //DataConverter.CopyTo<LineType>(obj, objCopy);

			//ִ���޸Ĳ���
            using (FrmLineTypeDialog dlg = new FrmLineTypeDialog())
			{
                dlg.Object = obj;   //�󶨸���
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
            obj.ObjColor = Color.FromArgb(Convert.ToInt32(obj.Color));
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
            LineType obj = FocusedObject;
			if (obj == null)
			{
				return;
			}
            if (obj.UID == "3afdf1a2-0992-4d44-a597-cd89aba785c6" || obj.UID == "516a2d67-b19a-4c47-8fbf-2f85c601af2d" || obj.UID == "9c646f34-4b43-4166-99c9-15ad7ec394a3")
            {
                MsgBox.Show("�������ݲ���ɾ����");
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
                Services.BaseService.Delete<LineType>(obj);
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
