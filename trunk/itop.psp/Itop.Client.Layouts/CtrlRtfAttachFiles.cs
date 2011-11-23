
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
using Itop.Domain.Layouts;
using System.IO;
using DevExpress.Utils;

#endregion

namespace Itop.Client.Layouts
{
	public partial class CtrlRtfAttachFiles : UserControl
	{
		#region ���췽��
		public CtrlRtfAttachFiles()
		{
			InitializeComponent();
		}
		#endregion

		#region �ֶ�
		protected bool _bAllowUpdate = true;
        private string catygory = "";
		#endregion

		#region ��������
        /// <summary>
        /// ��������
        /// </summary>
        public int RowCount
        {
            get
            {
                return gridView.RowCount;
            }
        }
        public string Category
        {
            get { return catygory; }
            set { catygory = value; }
        }


		/// <summary>
		/// ��ȡ������"˫�������޸�"��־
		/// </summary>
        /// 
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
		public IList<RtfAttachFiles> ObjectList
		{
			get { return this.gridControl.DataSource as IList<RtfAttachFiles>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
		public RtfAttachFiles FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as RtfAttachFiles; }
		}
		#endregion

		#region �¼�����
		private void gridView_DoubleClick(object sender, EventArgs e)
		{
            if (FocusedObject == null)
                return;

            
            string path = Application.StartupPath + "\\BlogData";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            RtfAttachFiles raf = Services.BaseService.GetOneByKey<RtfAttachFiles>(FocusedObject.UID);
            string filepath=path+"\\"+raf.FileName;
            FrmCommon.getDoc(raf.FileByte, filepath);


            WaitDialogForm wait = new WaitDialogForm("", "������������, ���Ժ�...");
            try
            {
                System.Diagnostics.Process.Start(filepath);
            }
            catch { System.Diagnostics.Process.Start(path); }
            wait.Close();

			// �ж�"˫�������޸�"��־ 
            //////if (!AllowUpdate)
            //////{
            //////    return;
            //////}

            ////////���������ڵ�Ԫ���У���༭�������
            //////Point point = this.gridControl.PointToClient(Control.MousePosition);
            //////if (GridHelper.HitCell(this.gridView, point))
            //////{
            //////    UpdateObject();
            //////}

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
				//IList<RtfAttachFiles> list = Services.BaseService.GetStrongList<RtfAttachFiles>();
                IList<RtfAttachFiles> list = Services.BaseService.GetList<RtfAttachFiles>("SelectRtfAttachFilesByCategory", catygory);
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
			//if (ObjectList == null)
			//{
			//	return;
			//}
			//�½�����
			RtfAttachFiles obj = new RtfAttachFiles();
            obj.C_UID = catygory;
            obj.CreateDate = (DateTime)Services.BaseService.GetObject("SelectSysData", null);

			//ִ����Ӳ���
			using (FrmRtfAttachFilesDialog dlg = new FrmRtfAttachFilesDialog())
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
			RtfAttachFiles obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//���������һ������
			RtfAttachFiles objCopy = new RtfAttachFiles();
			DataConverter.CopyTo<RtfAttachFiles>(obj, objCopy);

			//ִ���޸Ĳ���
			using (FrmRtfAttachFilesDialog dlg = new FrmRtfAttachFilesDialog())
			{
				dlg.Object = objCopy;   //�󶨸���
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//�ø������½������
			DataConverter.CopyTo<RtfAttachFiles>(objCopy, obj);
			//ˢ�±��
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// ɾ���������
		/// </summary>
		public void DeleteObject()
		{
			//��ȡ�������
			RtfAttachFiles obj = FocusedObject;
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
				Services.BaseService.Delete<RtfAttachFiles>(obj);
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

        private void gridControl_EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            switch (e.Button.Hint)
            { 
                case "���":
                    AddObject();
                    break;


                case "ɾ��":
                    DeleteObject();
                    break;


                case "�޸�":
                    UpdateObject();
                    break;


                case "��ӡ":
                    PrintPreview();
                    break;
            }
        }

        private void ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddObject();
        }

        private void �޸�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateObject();
        }

        private void ɾ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteObject();
        }

        private void ��ӡToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintPreview();
        }


	}
}
