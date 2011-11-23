
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
using System.IO;
#endregion

namespace Itop.TLPsp
{
	public partial class CtrlLayoutList : UserControl
	{
		#region ���췽��
		public CtrlLayoutList()
		{
			InitializeComponent();
		}
		#endregion

		#region �ֶ�
		protected bool _bAllowUpdate = true;
        string type = "";
        public bool editright = true;
		#endregion

		#region ��������

        public string Type
        {
            set { type = value; }
            get { return type; }
        
        }


        bool isshow = true;
        /// <summary>
        /// �Ƿ���ʾ
        /// </summary>
        public bool IsShow
        {
            get { return isshow; }
            set { isshow = value; }
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

		/// <summary>
		/// ��ȡGridControl������Դ����������б�
		/// </summary>
		public IList<LayoutList> ObjectList
		{
			get { return this.gridControl.DataSource as IList<LayoutList>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
		public LayoutList FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as LayoutList; }
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
			try
			{
				IList<LayoutList> list = Services.BaseService.GetStrongList<LayoutList>();
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




        public bool RefreshData1()
        {
            try
            {
                //IList<LayoutList> list = Services.BaseService.GetStrongList<LayoutList>();
                IList<LayoutList> list = Services.BaseService.GetList<LayoutList>("SelectLayoutListByTypes", type);

                string filepath = Path.GetTempPath() + "\\" + Path.GetFileName("SubstationGuiHua.xml");
                //this.ctrlSubstation_Info1.bandedGridView1.SaveLayoutToXml(filepath + "SubstationGuiHua.xml");

                if (File.Exists(filepath))
                {
                    this.gridView.RestoreLayoutFromXml(filepath);
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
			LayoutList obj = new LayoutList();
            obj.Types = type;

			//ִ����Ӳ���
			using (FrmLayoutListDialog dlg = new FrmLayoutListDialog())
			{
				dlg.IsCreate = true;    //�����½���־
				dlg.Object = obj;
                dlg.IsShow = isshow;
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
			LayoutList obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//���������һ������
			LayoutList objCopy = new LayoutList();
			DataConverter.CopyTo<LayoutList>(obj, objCopy);

			//ִ���޸Ĳ���
			using (FrmLayoutListDialog dlg = new FrmLayoutListDialog())
			{
				dlg.Object = objCopy;   //�󶨸���
                dlg.IsShow = isshow;
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//�ø������½������
			DataConverter.CopyTo<LayoutList>(objCopy, obj);
			//ˢ�±��
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// ɾ���������
		/// </summary>
		public void DeleteObject()
		{
			//��ȡ�������
			LayoutList obj = FocusedObject;
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
				Services.BaseService.Delete<LayoutList>(obj);
                switch (obj.Types)
                { 
                    case "1":
                        Services.BaseService.Update("DeleteLine_InfoByFlag1", obj.UID);
                        break;

                    case "2":
                        Services.BaseService.Update("DeleteSubstation_InfoByFlag1", obj.UID);
                        break;
                
                
                }



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
