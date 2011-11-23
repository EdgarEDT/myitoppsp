
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
    public partial class CtrlInterface : UserControl
	{
		#region ���췽��
        public CtrlInterface()
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
        public IList<PSP_interface> ObjectList
		{
            get { return this.gridControl.DataSource as IList<PSP_interface>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
        public PSP_interface FocusedObject
		{
           
            get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PSP_interface; }
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
         
			ComponentPrint.ShowPreview(this.gridControl, "�������ݲɼ�");
		}

		/// <summary>
		/// ˢ�±���е�����
		/// </summary>
		/// <returns>ture:�ɹ�  false:ʧ��</returns>
		public bool RefreshData()
		{
            //try
            //{
            //    PSP_interface p = new PSP_interface();
            //   // p.col1=" BdzId='"typeid+"' order by Name";
            //    IList<PSP_interface> list = Services.BaseService.GetList<PSP_interface>("SelectPSP_interfaceByWhere", p);
            //    this.gridControl.DataSource = list;
            //}
            //catch (Exception exc)
            //{
            //    Debug.Fail(exc.Message);
            //    HandleException.TryCatch(exc);
            //    return false;
            //}

			return true;
		}

		/// <summary>
		/// ��Ӷ���
		/// </summary>
		public void AddObject()
		{
			//�����������Ƿ��Ѿ�����
            //if (ObjectList == null)
            //{
            //    return;
            //}
            ////�½�����
            //PSP_bdz_type obj = new PSP_bdz_type();
            
            ////ִ����Ӳ���
            //using (FrmbdzTypeDialog dlg = new FrmbdzTypeDialog())
            //{
            //    dlg.IsCreate = true;    //�����½���־
            //    dlg.Typeid = typeid;
            //    dlg.Object = obj;
            //    if (dlg.ShowDialog() != DialogResult.OK)
            //    {
            //        return;
            //    }
            //}
            //ObjectList.Add(obj);
            ////ˢ�±�񣬲��������ж�λ���¶����ϡ�
            //gridControl.RefreshDataSource();
            //GridHelper.FocuseRow(this.gridView, obj);
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
            //PSP_bdz_type obj = FocusedObject;
            //if (obj == null)
            //{
            //    return;
            //}
            //int i=gridView.FocusedRowHandle;
            ////���������һ������
            ////LineType objCopy = new LineType();
            ////DataConverter.CopyTo<LineType>(obj, objCopy);

            ////ִ���޸Ĳ���
            //using (FrmbdzTypeDialog dlg = new FrmbdzTypeDialog())
            //{
            //    dlg.Object = obj;   //�󶨸���
            //    if (dlg.ShowDialog() != DialogResult.OK)
            //    {
            //        return;
            //    }
            //}
            ////RefreshData();
            //gridControl.RefreshDataSource();
            //gridView.FocusedRowHandle = i;
		}

		/// <summary>
		/// ɾ���������
		/// </summary>
		public void DeleteObject()
		{
           
			//��ȡ�������
            PSP_interface obj = FocusedObject;
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
                int[] l = gridView.GetSelectedRows();
                for (int i = 0; i < l.Length;i++ )
                {
                    PSP_interface p_obj = gridView.GetRow(l[i]) as PSP_interface;
                    PSP_interface p = new PSP_interface();
                    p.UID = p_obj.UID;
                    p =(PSP_interface) Services.BaseService.GetObject("SelectPSP_interfaceByKey", p);
                    if (p != null)
                    {
                        p.BdzId = p.BdzId.Replace(typeid, "");
                        if (p.BdzId.Length < 20)
                        {
                            Services.BaseService.Delete<PSP_interface>(p);

                        }
                        else
                        {
                            Services.BaseService.Update<PSP_interface>(p);

                        }
                    }
                   
                }
                this.gridView.BeginUpdate();
                //��ס��ǰ����������
                int iOldHandle = this.gridView.FocusedRowHandle;
                //��������ɾ��
                //ObjectList.Remove(p_obj);
                //ˢ�±��
                gridView.DeleteSelectedRows();
                gridControl.RefreshDataSource();
                //�����µĽ���������
                //GridHelper.FocuseRowAfterDelete(this.gridView, iOldHandle);
                this.gridView.EndUpdate();
                
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
				HandleException.TryCatch(exc);
				return;
			}

			
		}
		#endregion
	}
}
