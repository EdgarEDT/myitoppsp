
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
	public partial class CtrlglebePropertyZH : UserControl
	{
		#region ���췽��
        public CtrlglebePropertyZH()
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
                DataSet ds = new DataSet();
                //�������

                Hashtable hs = new Hashtable();
                hs.Add("ParentEleID", "0");
                hs.Add("SvgUID", svgUid);
                hs.Add("LayerID",sid);
                DataTable parentdt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectglebePropertParentIDTop", hs), typeof(glebeProperty));

             
                ds.Tables.Add(parentdt);
                //����ӱ�
                //Hashtable hs1 = new Hashtable();
                //hs1.Add("SvgUID", svgUid);
                //hs1.Add("LayerID", sid);
                //DataTable sondt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectglebePropertParentIDSub1", hs1), typeof(sonGlebeProperty));
                //ds.Tables.Add(sondt);
                
                
                ////������ϵ
                //DataColumn parentColumn = ds.Tables["glebeProperty"].Columns["EleID"];
                //DataColumn childColumn = ds.Tables["sonGlebeProperty"].Columns["ParentEleID"];
                //// Create DataRelation.
                //DataRelation relCustOrder;
                //relCustOrder = new DataRelation("gx1", parentColumn, childColumn, false);
                //// Add the relation to the DataSet.
                //ds.Relations.Add(relCustOrder);
                //������Դ
                this.gridControl.DataSource = ds;
                this.gridControl.DataMember = "glebeProperty";
                this.gridControl.Text = "���������б�";
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
                DataSet ds = new DataSet();
                //�������

                Hashtable hs = new Hashtable();
                hs.Add("ParentEleID", "0");
                hs.Add("SvgUID", svgUid);
               // hs.Add("LayerID", sid);
                DataTable parentdt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectglebePropertParentIDTopAll", hs), typeof(glebeProperty));

             
                ds.Tables.Add(parentdt);
                //����ӱ�
                //Hashtable hs1 = new Hashtable();
                //hs1.Add("SvgUID", svgUid);
                ////hs1.Add("LayerID", sid);
                //DataTable sondt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectglebePropertParentIDSub1All", hs1), typeof(sonGlebeProperty));
          
                //ds.Tables.Add(sondt);


                ////������ϵ
                //DataColumn parentColumn = ds.Tables["glebeProperty"].Columns["EleID"];
                //DataColumn childColumn = ds.Tables["sonGlebeProperty"].Columns["ParentEleID"];
                //// Create DataRelation.
                //DataRelation relCustOrder;
                //relCustOrder = new DataRelation("gx1", parentColumn, childColumn, false);
                //// Add the relation to the DataSet.
                //ds.Relations.Add(relCustOrder);
                //������Դ
                this.gridControl.DataSource = ds;
                this.gridControl.DataMember = "glebeProperty";
                this.gridControl.Text = "���������б�";
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return false;
            }
            return true;
        }
        public void InitDataSub(string svgUid,string sid)
        {
            Hashtable hs = new Hashtable();
            hs.Add("SvgUID", svgUid);
            hs.Add("LayerID",sid);
            DataTable sondt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectglebePropertParentIDSubZH", hs), typeof(glebeProperty));
            gridControl.DataSource = sondt;
            gridControl.Text = "�ؿ�ͳ��";
        }
        public void InitDataSub(string svgUid)
        {
            Hashtable hs = new Hashtable();
            hs.Add("SvgUID", svgUid);
            //hs.Add("LayerID", sid);
            DataTable sondt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectglebePropertParentIDSubAll", hs), typeof(glebeProperty));
            gridControl.DataSource = sondt;
            gridControl.Text = "�ؿ�ͳ��";
        }
		#endregion
	}
}
