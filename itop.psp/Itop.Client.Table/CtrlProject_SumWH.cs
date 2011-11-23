
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
using Itop.Domain.Stutistics;
#endregion

namespace Itop.Client.Table
{
	public partial class CtrlProject_SumWH : UserControl
	{
		#region ���췽��
        public CtrlProject_SumWH()
		{
			InitializeComponent();
		}
		#endregion

		#region �ֶ�
		protected bool _bAllowUpdate = true;
        public bool editright = true;
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

        string type = "";
        /// <summary>
        /// ��ȡGridControl����
        /// </summary>
        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                
            }
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
		public IList<Project_Sum> ObjectList
		{
			get { return this.gridControl.DataSource as IList<Project_Sum>; }
		}
        public void Change_ZaoJia(string str1)
        {
            gridView.Columns["Num"].Caption = str1;
        }
		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
		public Project_Sum FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as Project_Sum; }
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

                if (type == "1")
                {
                    //colType.Caption = "��·��";
                    //colT1.Caption = "��������";
                    //colT2.Caption = "�����ͺ�";
                    //colT3.Caption = "�����ͺ�";

                    //gridColumn2.Visible = false;
                    //gridColumn3.Visible = false;
                    //colT1.Visible = false;
                    //colT2.Visible = false;
                    //colT3.Visible = false;
                    //colType.Visible = false;
                    this.gridView.GroupPanelText = "��·�����Ϣ";
                    gridView.Columns["Num"].Caption = "��λ��ۣ���Ԫ/kM��";


                }
                if (type == "2")
                {


                    //gridColumn4.Visible = false;
                    //gridColumn5.Visible = false;
                    //gridColumn6.Visible = false;
                    //gridColumn7.Visible = false;

                    this.gridView.GroupPanelText = "���վ�����Ϣ";
                    gridView.Columns["Num"].Caption = "��λ��ۣ���Ԫ/MVA��";
                }

                IList<Project_Sum> list = Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS5", type);
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
			Project_Sum obj = new Project_Sum();
            obj.UID = Guid.NewGuid().ToString();
            obj.S5 = type;

			//ִ����Ӳ���
			using (FrmProject_SumDialogWH dlg = new FrmProject_SumDialogWH())
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
			Project_Sum obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//���������һ������
			Project_Sum objCopy = new Project_Sum();
			DataConverter.CopyTo<Project_Sum>(obj, objCopy);

			//ִ���޸Ĳ���
			using (FrmProject_SumDialogWH dlg = new FrmProject_SumDialogWH())
			{
				dlg.Object = objCopy;   //�󶨸���
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//�ø������½������
			DataConverter.CopyTo<Project_Sum>(objCopy, obj);
			//ˢ�±��
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// ɾ���������
		/// </summary>
		public void DeleteObject()
		{
			//��ȡ�������
			Project_Sum obj = FocusedObject;
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
				Services.BaseService.Delete<Project_Sum>(obj);
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
