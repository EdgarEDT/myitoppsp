
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
using Itop.Domain.BaseDatas;
#endregion

namespace Itop.Client.Chen
{
	public partial class CtrlBaseColor : UserControl
	{
		#region ���췽��
		public CtrlBaseColor()
		{
			InitializeComponent();
		}
		#endregion

		#region �ֶ�
		protected bool _bAllowUpdate = true;
        ColorConverter cc = new ColorConverter();
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

        private DataTable dt = new DataTable();
        public DataTable DT
        {
            set { dt = value; }
            get { return dt; }
        }

        private string id = "";
        public string ID
        {
            set { id = value; }
            get { return id; }
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
		public IList<BaseColor> ObjectList
		{
			get { return this.gridControl.DataSource as IList<BaseColor>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
		public BaseColor FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as BaseColor; }
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
                ////////IList<BaseColor> list = Services.BaseService.GetStrongList<BaseColor>();

                ////////foreach (BaseColor bb in list)
                ////////{
                ////////    Color cl = ColorTranslator.FromOle(bb.Color);
                ////////    bb.Color1 = cl;
                ////////}


                ////////this.gridControl.DataSource = list;
                IList<BaseColor> list = Services.BaseService.GetList<BaseColor>("SelectBaseColorByWhere", "Remark='" + id + "'");

                IList<BaseColor> li = new List<BaseColor>();
                bool bl = false;
                foreach (DataRow row in dt.Rows)
                {
                    bl = false;
                    foreach (BaseColor bc in list)
                    {
                        if (row["Title"].ToString() == bc.Title)
                        {
                            bl = true;
                            BaseColor bc1 = new BaseColor();
                            CopyBaseColor(bc1, bc);
                            li.Add(bc1);
                        }


                    }
                    if (!bl)
                    {
                        BaseColor bc1 = new BaseColor();
                        bc1.UID = Guid.NewGuid().ToString();
                        bc1.Remark = id;
                        bc1.Title = row["Title"].ToString();
                        bc1.Color = 0;
                        bc1.Color1 = Color.Black;
                        Services.BaseService.Create<BaseColor>(bc1);
                        li.Add(bc1);
                    }

                }

                this.gridControl.DataSource = li;



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
			BaseColor obj = new BaseColor();
            obj.Remark = id;

			//ִ����Ӳ���
			using (FrmBaseColorDialog dlg = new FrmBaseColorDialog())
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
			BaseColor obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//���������һ������
			BaseColor objCopy = new BaseColor();
			DataConverter.CopyTo<BaseColor>(obj, objCopy);

			//ִ���޸Ĳ���
			using (FrmBaseColorDialog dlg = new FrmBaseColorDialog())
			{
				dlg.Object = objCopy;   //�󶨸���
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//�ø������½������
			DataConverter.CopyTo<BaseColor>(objCopy, obj);
			//ˢ�±��
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// ɾ���������
		/// </summary>
		public void DeleteObject()
		{
			//��ȡ�������
			BaseColor obj = FocusedObject;
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
				Services.BaseService.Delete<BaseColor>(obj);
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



        /// <summary>
        /// ɾ���������
        /// </summary>
        public void SaveList()
        {
            foreach (BaseColor bc in ObjectList)
            {
                try
                {
                    bc.Color = ColorTranslator.ToOle(bc.Color1);
                    Services.BaseService.Update<BaseColor>(bc);
                }
                catch (Exception exc)
                {
                    Debug.Fail(exc.Message);
                    HandleException.TryCatch(exc);
                    return;
                }
            
            }

            

            this.gridView.BeginUpdate();
            //��ס��ǰ����������
            gridControl.RefreshDataSource();
            this.gridView.EndUpdate();
            MsgBox.Show("����ɹ���");
        }
		#endregion

        private void CopyBaseColor(BaseColor bc1, BaseColor bc2)
        {
            bc1.UID = bc2.UID;
            bc1.Title = bc2.Title;
            bc1.Remark = bc2.Remark;
            bc1.Color = bc2.Color;
            bc1.Color1 = ColorTranslator.FromOle(bc2.Color);
        }
	}
}
