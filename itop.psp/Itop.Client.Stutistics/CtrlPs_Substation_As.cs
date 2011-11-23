
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
#endregion

namespace Itop.Client.Stutistics
{
	public partial class CtrlPs_Substation_As : UserControl
	{
		#region ���췽��
		public CtrlPs_Substation_As()
		{
			InitializeComponent();
		}
		#endregion

		#region �ֶ�
		protected bool _bAllowUpdate = true;
        private string type = "���վ";
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

        double glys = 0.95;
        public double GLYS
        {
            get { return glys; }
            set { glys = value; }
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
		public IList<Ps_Power> ObjectList
		{
            get { return this.gridControl.DataSource as IList<Ps_Power>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
        public Ps_Power FocusedObject
		{
            get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as Ps_Power; }
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
                int year=DateTime.Now.Year;
                IList<Ps_Power> list = Services.BaseService.GetList<Ps_Power>("SelectPs_PowerByType", type);
                foreach (Ps_Power ps in list)
                {
                    double zdfh = 0;
                    object obj = ps.GetType().GetProperty("y" + year).GetValue(ps, null);
                    if (obj != null)
                    {
                        zdfh = Convert.ToDouble(obj);
                    }
                    if (ps.RL != 0)
                        ps.Col5 = (zdfh / (ps.RL*glys)).ToString("n2");

                    double rl = ps.RL;
                    int ts = ps.TS;
                    double sss = 0;

                    switch (ts)
                    {
                        case 2:
                            sss = 0.65;
                            break;
                        case 3:
                            sss = 0.65;
                            break;
                        case 4:
                            sss = 0.87;
                            break;
                        default:
                            sss = 1;
                            break;
                    }
                    ps.Col6 = (rl * sss - zdfh / glys).ToString("n2");
                
                
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
            Ps_Power obj = new Ps_Power();
            obj.Type = type;
            obj.ID = Guid.NewGuid().ToString();
			//ִ����Ӳ���
			using (FrmPs_Substation_AsDialog dlg = new FrmPs_Substation_AsDialog())
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
            RefreshData();
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
			Ps_Power obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//���������һ������
            Ps_Power objCopy = new Ps_Power();
            DataConverter.CopyTo<Ps_Power>(obj, objCopy);

			//ִ���޸Ĳ���
			using (FrmPs_Substation_AsDialog dlg = new FrmPs_Substation_AsDialog())
			{
				dlg.Object = objCopy;   //�󶨸���
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//�ø������½������
            DataConverter.CopyTo<Ps_Power>(objCopy, obj);
			//ˢ�±��
            RefreshData();
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// ɾ���������
		/// </summary>
		public void DeleteObject()
		{
			//��ȡ�������
			Ps_Power obj = FocusedObject;
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
                Services.BaseService.Delete<Ps_Power>(obj);
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
