
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
//using Itop.Client.Common;
using Itop.Domain.RightManager;
using Itop.Client.Projects;
//using Itop.Domain.Graphics;
#endregion

namespace Itop.Client.Projects
{
	public partial class CtrlProject : UserControl
	{
		#region ���췽��
		public CtrlProject()
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
		public IList<Project> ObjectList
		{
			get { return this.gridControl.DataSource as IList<Project>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
		public Project FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as Project; }
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
			if (Itop.Client.Projects.Common.GridHelper.HitCell(this.gridView, point))
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
			//ComponentPrint.ShowPreview(this.gridControl, this.gridView.GroupPanelText);
		}

		/// <summary>
		/// ˢ�±���е�����
		/// </summary>
		/// <returns>ture:�ɹ�  false:ʧ��</returns>
		public bool RefreshData()
		{
			try
			{
                string s = " GuiDangName in ('" + Itop.Client.MIS.UserNumber + "','') and IsGuiDang!='��'";
                IList<Project> list = Services.BaseService.GetList<Project>("SelectProjectByWhere",s);
				this.gridControl.DataSource = list;
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
				//HandleException.TryCatch(exc);
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
            string guid = Guid.NewGuid().ToString();

			Project obj = new Project();
            obj.UID = guid.Substring(0, 8);
            obj.CreateDate = DateTime.Now;
            obj.StartDate = DateTime.Now.Date;
            obj.PlanCompleteDate = DateTime.Now;
            obj.CompleteDate = DateTime.Now;
            obj.QualityDate = DateTime.Now;
            obj.BecomeEffective = DateTime.Now;
            obj.GuiDangTime = DateTime.Now;
            obj.GuiDangName = Itop.Client.MIS.UserNumber;

			//ִ����Ӳ���
			using (FrmProjectDialog dlg = new FrmProjectDialog())
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
			Itop.Client.Projects.Common.GridHelper.FocuseRow(this.gridView, obj);
		}

		/// <summary>
		/// �޸Ľ������
		/// </summary>
		public void UpdateObject()
		{
			//��ȡ�������
			Project obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//���������һ������
			Project objCopy = new Project();
			DataConverter.CopyTo<Project>(obj, objCopy);

			//ִ���޸Ĳ���
			using (FrmProjectDialog dlg = new FrmProjectDialog())
			{
				dlg.Object = objCopy;   //�󶨸���
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//�ø������½������
			DataConverter.CopyTo<Project>(objCopy, obj);
			//ˢ�±��
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// ɾ���������
		/// </summary>
		public void DeleteObject()
		{
			//��ȡ�������
			Project obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//����ȷ��
			if (MsgBox.ShowYesNo("�Ƿ�ɾ����") != DialogResult.Yes)
			{
				return;
			}

			//ִ��ɾ������
			try
			{

                Services.BaseService.Update("UpdateProjectByGuiDangName", obj);
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
				//HandleException.TryCatch(exc);
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
			Itop.Client.Projects.Common.GridHelper.FocuseRowAfterDelete(this.gridView, iOldHandle);
			this.gridView.EndUpdate();
		}
		#endregion
	}
}
