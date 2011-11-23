
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

namespace ItopVector.Tools
{
	public partial class CtrlglebeType1 : UserControl
	{
		#region ���췽��
		public CtrlglebeType1()
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
			
		}

		/// <summary>
		/// �޸Ľ������
		/// </summary>
		public void UpdateObject()
		{
			//��ȡ�������
			
		}

		/// <summary>
		/// ɾ���������
		/// </summary>
		public void DeleteObject()
		{
			//��ȡ�������
			
		}
		#endregion
	}
}
