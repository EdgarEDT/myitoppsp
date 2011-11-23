
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
	public partial class CtrlSubStationProperty : UserControl
	{
		#region ���췽��
        public CtrlSubStationProperty()
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
            get { return bandedGridView1; }
		}

		/// <summary>
		/// ��ȡGridControl������Դ����������б�
		/// </summary>
        public IList<Dlph> ObjectList
		{
			get { return this.gridControl.DataSource as IList<Dlph>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
		public Dlph FocusedObject
		{
            get { return this.bandedGridView1.GetRow(this.bandedGridView1.FocusedRowHandle) as Dlph; }
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
            //if (GridHelper.HitCell(this.bandedGridView1, point))
            //{
            //    //UpdateObject();
            //}

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
                Hashtable hs = new Hashtable();
                hs.Add("ParentEleID","0");
                hs.Add("SvgUID", svgUid);
                hs.Add("LayerID",sid);
              
                DataTable dt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectglebePropertParentIDTop", hs), typeof(glebeProperty));
                gridControl.DataSource = dt;
                gridControl.Text = "����ƽ���";
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
                Hashtable hs = new Hashtable();
                hs.Add("ParentEleID", "0");
                hs.Add("SvgUID", svgUid);
               
                //gle.UID = sid;
                DataTable dt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectglebePropertParentIDTopAll", hs), typeof(glebeProperty));
                gridControl.DataSource = dt;
                gridControl.Text = "����ƽ���";
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
