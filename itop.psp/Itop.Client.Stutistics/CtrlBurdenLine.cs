
#region ���������ռ�
using System;
using System.Collections;
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
using Itop.Domain.HistoryValue;
using Itop.Domain.Table;
#endregion

namespace Itop.Client.Stutistics
{
	public partial class CtrlBurdenLine : UserControl
	{
		#region ���췽��
		public CtrlBurdenLine()
		{
			InitializeComponent();
		}
		#endregion

		#region �ֶ�
		protected bool _bAllowUpdate = true;
        public bool editright = true;
		#endregion

		#region ��������
        string pid = "";
        /// <summary>
        /// ��ȡ������"˫�������޸�"��־
        /// </summary>
        public string PID
        {
            get { return pid; }
            set { pid = value; }
        }

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

        FrmBurdenLine fbl;
        public FrmBurdenLine FBL
        {
            set { fbl = value; }

        }


		/// <summary>
		/// ��ȡGridControl������Դ����������б�
		/// </summary>
		public IList<BurdenLine> ObjectList
		{
			get { return this.gridControl.DataSource as IList<BurdenLine>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
		public BurdenLine FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as BurdenLine; }
		}
		#endregion

		#region �¼�����
		private void gridView_DoubleClick(object sender, EventArgs e)
        {
            if (!editright)
                return;
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
                fbl.refeshchart();
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
       
        IList<PSP_YearVisibleIndex> indexlist = null;
		public bool RefreshData()
        {
            string pjt = " ProjectID='" + MIS.ProgUID + "'";
            IList<PS_Table_AreaWH> lt = Common.Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", pjt);
            repositoryItemLookUpEdit1.DataSource = lt;
			try
			{
                IList<BurdenLine> list = Services.BaseService.GetList<BurdenLine>("SelectBurdenLineByWhere", " uid like '%" + Itop.Client.MIS.ProgUID + "%' order by BurdenDate");      
                //if (indexlist.Count > 0)
                //{
                //    VisibleColumns(indexlist);
                //}
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
        ///���а��ջ�ȡ������ʾindex��������
        /// </summary>

        private void VisibleColumns(IList<PSP_YearVisibleIndex> indexlist)
        {
            ArrayList al = new ArrayList();

            foreach (PSP_YearVisibleIndex year in indexlist)
            {
                al.Add(year.Year);

                for (int i = 0; i < GridView.Columns.Count; i++)
                {

                    if (GridView.Columns[i].FieldName.ToString() == year.Year.ToString())
                    {
                        for (int j = year.VisibleIndex - 1; j < i; j++)
                        {
                            if (!al.Contains(GridView.Columns[j].FieldName.ToString()))
                            {
                                GridView.Columns[j].VisibleIndex++;
                            }


                        }

                        GridView.Columns[i].VisibleIndex = year.VisibleIndex;
                    }
                }
            }                
        }

        /// <summary>
        /// ��ȡ����ʾ˳��index
        /// </summary>
       
        public void ColumnsVisibleIndex(string ModuleFlag)
        {
            PSP_YearVisibleIndex yvi = new PSP_YearVisibleIndex();
            yvi.ModuleFlag = ModuleFlag;
            try
            {
                IList<PSP_YearVisibleIndex> yearvisi = Common.Services.BaseService.GetList<PSP_YearVisibleIndex>("SelectPSP_YearVisibleIndexByModuleFlagSort", yvi);
                indexlist = yearvisi;
            }
            catch( Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
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
			BurdenLine obj = new BurdenLine();
            obj.UID = Guid.NewGuid().ToString() + "|" + Itop.Client.MIS.ProgUID;
            obj.BurdenDate = DateTime.Now;

			//ִ����Ӳ���
			using (FrmBurdenLineDialog dlg = new FrmBurdenLineDialog())
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
			BurdenLine obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//���������һ������
			BurdenLine objCopy = new BurdenLine();
			DataConverter.CopyTo<BurdenLine>(obj, objCopy);
            FrmBurdenLine v =new  FrmBurdenLine();
			//ִ���޸Ĳ���
			using (FrmBurdenLineDialog dlg = new FrmBurdenLineDialog())
			{
				dlg.Object = objCopy;   //�󶨸���
               
				if (dlg.ShowDialog() != DialogResult.OK)
				{
                  
					return;
				}
			}

			//�ø������½������
			DataConverter.CopyTo<BurdenLine>(objCopy, obj);
			//ˢ�±��
			gridControl.RefreshDataSource();
            v.refeshchart();
		}

		/// <summary>
		/// ɾ���������
		/// </summary>
		public void DeleteObject()
		{
			//��ȡ�������
			BurdenLine obj = FocusedObject;
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
				Services.BaseService.Delete<BurdenLine>(obj);
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
            Color c3 = Color.FromArgb(152, 122, 254);
            Color c4 = Color.FromArgb(152, 122, 254);
             object dr = this.gridView.GetRow(e.RowHandle);
            if (dr == null)
                return;
            BurdenLine bl = (BurdenLine)dr;
          
                //if (e.Column.FieldName == "BurdenDate")
                //{ 
            double imax = 0;
                    double j=0;
                         for(int i=1;i<=24;i++)
                         {
                             j=Convert.ToDouble(bl.GetType().GetProperty("Hour"+i).GetValue(bl,null));
                         if(imax<j)
                             imax=j;
                         }
                //}

                if (bl.IsType && e.Column.FieldName.Contains("Hour"))
                {
                    if (imax.ToString() == e.CellValue.ToString())
                    {
                        brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, c3, c4, 180);
                        if (brush != null)
                        {
                            e.Graphics.FillRectangle(brush, r);
                        }
                    }
                }
                if (bl.IsMaxDate && e.Column.FieldName.Contains("Hour"))
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
