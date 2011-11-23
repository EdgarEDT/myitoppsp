
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
using Itop.Domain.PWTable;
#endregion

namespace Itop.Client.PWTable
{
	public partial class CtrlPW_Sum : UserControl
	{
		#region ���췽��
		public CtrlPW_Sum()
		{
			InitializeComponent();

		}
		#endregion

		#region �ֶ�
		protected bool _bAllowUpdate = true;
        public bool editright = true;
        private string proguid = "";

        public string Proguid
        {
            get { return proguid; }
            set { proguid = value; }
        }

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
            get { return advBandedGridView1; }
		}

		/// <summary>
		/// ��ȡGridControl������Դ����������б�
		/// </summary>
		public IList<PW_tb3a> ObjectList
		{
            get { return this.gridControl.DataSource as IList<PW_tb3a>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
        public PW_tb3a FocusedObject
		{
            get { return this.advBandedGridView1.GetRow(this.advBandedGridView1.FocusedRowHandle) as PW_tb3a; }
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
            if (GridHelper.HitCell(this.advBandedGridView1, point))
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
            ComponentPrint.ShowPreview(this.gridControl, this.advBandedGridView1.GroupPanelText);
		}

		/// <summary>
		/// ˢ�±���е�����
		/// </summary>
		/// <returns>ture:�ɹ�  false:ʧ��</returns>
		public bool RefreshData()
		{
			try
			{

                this.advBandedGridView1.GroupPanelText = "��·�豸�����";
                PW_tb3a p = new PW_tb3a();
                p.col2 = proguid;
                IList<PW_tb3a> list = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aList", p);
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
            PW_tb3a obj = new PW_tb3a();
            obj.UID = Guid.NewGuid().ToString();
            obj.col2 = proguid;
            //obj.S5 = type;

			//ִ����Ӳ���
			using (FrmPW_SumDialog dlg = new FrmPW_SumDialog())
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
            GridHelper.FocuseRow(this.advBandedGridView1, obj);
		}
        public void SubAdd()
        {
            PW_tb3a obj = FocusedObject;
            if (obj == null)
            {
                MessageBox.Show("��ѡ���¼��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            FrmPW3b f = new FrmPW3b();
            f.addright =f.editright= editright;
            f.Type = obj.UID;
            f.Show();
        }
		/// <summary>
		/// �޸Ľ������
		/// </summary>
		public void UpdateObject()
		{
			//��ȡ�������
            PW_tb3a obj = FocusedObject;
			if (obj == null)
			{
				return;
			}
            obj.col2 = proguid;
			//���������һ������
            PW_tb3a objCopy = new PW_tb3a();
            DataConverter.CopyTo<PW_tb3a>(obj, objCopy);

			//ִ���޸Ĳ���
			using (FrmPW_SumDialog dlg = new FrmPW_SumDialog())
			{
				dlg.Object = objCopy;   //�󶨸���
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//�ø������½������
            DataConverter.CopyTo<PW_tb3a>(objCopy, obj);
			//ˢ�±��
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// ɾ���������
		/// </summary>
        public void DeleteObject()
        {
            if (MsgBox.ShowYesNo(Strings.SubmitDelete) != DialogResult.Yes)
            {
                return;
            }
            

            int[] aa = advBandedGridView1.GetSelectedRows();
            for (int i = 0; i < aa.Length; i++)
            {
                //��ȡ�������

                PW_tb3a obj = (PW_tb3a)advBandedGridView1.GetRow(aa[i]);  //FocusedObject;
                if (obj == null)
                {
                    return;
                }

                //����ȷ��


                //ִ��ɾ������
                try
                {
                    Services.BaseService.Delete<PW_tb3a>(obj);
                }
                catch (Exception exc)
                {
                    Debug.Fail(exc.Message);
                    HandleException.TryCatch(exc);
                    return;
                }


               // this.advBandedGridView1.BeginUpdate();
                //��ס��ǰ����������
                //int iOldHandle = this.advBandedGridView1.FocusedRowHandle;
                //��������ɾ��
               // ObjectList.Remove(obj);
                //ˢ�±��
                
                //�����µĽ���������
                //GridHelper.FocuseRowAfterDelete(this.advBandedGridView1, iOldHandle);
                //this.advBandedGridView1.EndUpdate();

            }
            RefreshData();
            //this.advBandedGridView1.EndUpdate();
        }
		#endregion


	}
}
