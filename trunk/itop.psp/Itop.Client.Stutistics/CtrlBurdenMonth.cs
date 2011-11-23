
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
using Itop.Domain.Stutistic;
using Itop.Domain.Table;
#endregion

namespace Itop.Client.Stutistics
{
	public partial class CtrlBurdenMonth : UserControl
	{
		#region ���췽��
		public CtrlBurdenMonth()
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
        /// ��ȡ������"˫�������޸�"��־
        /// </summary>
        /// 
        FrmBurdenMonth fbm = null;

        public FrmBurdenMonth FBM
        {
            get { return fbm; }
            set { fbm = value; }
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
		public IList<BurdenMonth> ObjectList
		{
			get { return this.gridControl.DataSource as IList<BurdenMonth>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
		public BurdenMonth FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as BurdenMonth; }
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
                fbm.UpdataChart();
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
            string pjt = " ProjectID='" + MIS.ProgUID + "'";
            IList<PS_Table_AreaWH> lt = Common.Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", pjt);
            repositoryItemLookUpEdit1.DataSource = lt;
			try
			{
				//IList<BurdenMonth> list = Services.BaseService.GetStrongList<BurdenMonth>();

                IList<BurdenMonth> list = Services.BaseService.GetList<BurdenMonth>("SelectBurdenMonthByWhere", " uid like '%" + Itop.Client.MIS.ProgUID + "%' order by BurdenYear ");
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
			BurdenMonth obj = new BurdenMonth();
            obj.UID = Guid.NewGuid().ToString() + "|" + Itop.Client.MIS.ProgUID;
			//ִ����Ӳ���
			using (FrmBurdenMonthDialog dlg = new FrmBurdenMonthDialog())
			{
                dlg.TitleName = this.gridView.GroupPanelText;
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
			BurdenMonth obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//���������һ������
			BurdenMonth objCopy = new BurdenMonth();
			DataConverter.CopyTo<BurdenMonth>(obj, objCopy);

			//ִ���޸Ĳ���
			using (FrmBurdenMonthDialog dlg = new FrmBurdenMonthDialog())
            {
                dlg.TitleName = this.gridView.GroupPanelText;
				dlg.Object = objCopy;   //�󶨸���
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//�ø������½������
			DataConverter.CopyTo<BurdenMonth>(objCopy, obj);
			//ˢ�±��
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// ɾ���������
		/// </summary>
		public void DeleteObject()
		{
			//��ȡ�������
			BurdenMonth obj = FocusedObject;
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
				Services.BaseService.Delete<BurdenMonth>(obj);
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

        private void gridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

            Brush brush = null;
            Rectangle r = e.Bounds;
            int year = 0;
            Color c1 = Color.FromArgb(255, 121, 121);
            Color c2 = Color.FromArgb(255, 121, 121);
            object dr = this.gridView.GetRow(e.RowHandle);
            if (dr == null)
                return;
            BurdenMonth bl = (BurdenMonth)dr;
        
            //if (e.Column.FieldName == "BurdenDate")
            //{ 
            double imax = 0;
            double j = 0;
            for (int i = 1; i <= 12; i++)
            {
                j = Convert.ToDouble(bl.GetType().GetProperty("Month" + i).GetValue(bl, null));
                if (imax < j)
                    imax = j;
            }
            if (e.Column.FieldName.Contains("Month"))
            {
                if (imax.ToString() == e.CellValue.ToString())
                {
                    brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, c1, c2, 180);
                    if (brush != null)
                    {
                        e.Graphics.FillRectangle(brush, r);
                    }
                }
            }
        }
	}
}
