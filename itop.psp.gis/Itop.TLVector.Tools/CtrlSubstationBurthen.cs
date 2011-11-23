
#region ���������ռ�
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Collections;
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
	public partial class CtrlSubstationBurthen : UserControl
	{
		#region ���췽��
        public CtrlSubstationBurthen()
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
		public IList<glebeProperty> ObjectList
		{
			get { return this.gridControl.DataSource as IList<glebeProperty>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
		public glebeProperty FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as glebeProperty; }
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
                //UpdateObject();
			}

		}
		#endregion

		#region ��������
		/// <summary>
		/// ��ӡԤ��
		/// </summary>
		public void PrintPreview()
		{
			ComponentPrint.ShowPreview(this.gridControl, gridControl.Text);
            
		}

		/// <summary>
		/// ˢ�±���е�����
		/// </summary>
		/// <returns>ture:�ɹ�  false:ʧ��</returns>
		public bool InitData(string svgUid,string sid)
		{
            try
            {
                LineInfo line = new LineInfo();
                line.SvgUID = svgUid;
                line.LayerID = sid;
                DataTable dt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectLineInfoBySvgUIDAndLayer", line), typeof(LineInfo));
                gridControl.DataSource = dt;
                gridControl.Text = "��·�б�";
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return false;
            }
            return true;
		}
        public bool InitData(string svgUid)
        {
            try
            {
                LineInfo line = new LineInfo();
                line.SvgUID = svgUid;
               // line.LayerID = sid;
                DataTable dt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectLineInfoBySvgUID", line), typeof(LineInfo));
                gridControl.DataSource = dt;
                gridControl.Text = "��·�б�";
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return false;
            }
            return true;
        }
		#endregion
	}
}
