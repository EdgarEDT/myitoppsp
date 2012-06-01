
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
	public partial class CtrlglebeType : UserControl
	{
		#region ���췽��
		public CtrlglebeType()
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
		public IList<glebeType> ObjectList
		{
			get { return this.gridControl.DataSource as IList<glebeType>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
		public glebeType FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as glebeType; }
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
            ComponentPrint.ShowPreview(this.gridControl, "�ؿ鸺���ܶ�ά��");
		}

		/// <summary>
		/// ˢ�±���е�����
		/// </summary>
		/// <returns>ture:�ɹ�  false:ʧ��</returns>
		public bool RefreshData()
		{
			try
			{
				IList<glebeType> list = Services.BaseService.GetStrongList<glebeType>();
                 for (int i = 0; i < list.Count;i++ )
                {
                    list[i].ObjColor=Color.FromArgb(Convert.ToInt32(list[i].ObligateField1));
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
			glebeType obj = new glebeType();
            
			//ִ����Ӳ���
			using (FrmglebeTypeDialog dlg = new FrmglebeTypeDialog())
			{
				dlg.IsCreate = true;    //�����½���־
				dlg.Object = obj;
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
            obj.ObjColor = Color.FromArgb(Convert.ToInt32(obj.ObligateField1));
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
			glebeType obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//���������һ������
            //glebeType objCopy = new glebeType();
            //DataConverter.CopyTo<glebeType>(obj, objCopy);

			//ִ���޸Ĳ���
			using (FrmglebeTypeDialog dlg = new FrmglebeTypeDialog())
			{
				dlg.Object = obj;   //�󶨸���
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
            obj.ObjColor = Color.FromArgb(Convert.ToInt32(obj.ObligateField1));
			//�ø������½������
            //DataConverter.CopyTo<glebeType>(objCopy, obj);
			//ˢ�±��
            Services.BaseService.Update<glebeType>(obj);
			gridControl.RefreshDataSource();
            

           
		}

		/// <summary>
		/// ɾ���������
		/// </summary>
		public void DeleteObject()
		{
			//��ȡ�������
			glebeType obj = FocusedObject;
			if (obj == null)
			{
				return;
			}
            if (obj.UID == "6ab9af7b-3d97-4e6c-8ed7-87b76950b90b")
            {
                MessageBox.Show("��͸����ܶȲ���ɾ����","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
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
				Services.BaseService.Delete<glebeType>(obj);
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
