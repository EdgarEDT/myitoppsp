
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
using Itop.Domain.Forecast;
using System.Collections;
#endregion

namespace Itop.Client.Using
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

        private int forcolor = 0;
        public int For
        {
            set { forcolor = value; }
            get { return forcolor; }
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
        public IList<FORBaseColor> ObjectList
		{
			get { return this.gridControl.DataSource as IList<FORBaseColor>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
        public FORBaseColor FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as FORBaseColor; }
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
                ////////IList<FORBaseColor> list = Services.BaseService.GetStrongList<FORBaseColor>();

                ////////foreach (FORBaseColor bb in list)
                ////////{
                ////////    Color cl = ColorTranslator.FromOle(bb.Color);
                ////////    bb.Color1 = cl;
                ////////}


                ////////this.gridControl.DataSource = list;
                IList<FORBaseColor> list = Services.BaseService.GetList<FORBaseColor>("SelectFORBaseColorByWhere", "Remark='" + id +"-"+forcolor+ "'");

                IList<FORBaseColor> li = new List<FORBaseColor>();
                bool bl = false;


                ArrayList al = new ArrayList();
                foreach (DataRow row in dt.Rows)
                {
                    al.Add(row["ID"].ToString());
                }




                foreach (DataRow row in dt.Rows)
                {
                    //if (row["ParentID"].ToString().Length > 12)
                    //    continue;
                    if(al.Contains(row["ParentID"].ToString()))
                        continue;


                    bl = false;
                    foreach (FORBaseColor bc in list)
                    {
                        if (row["Title"].ToString() == bc.Title)
                        {
                            bl = true;
                            FORBaseColor bc1 = new FORBaseColor();
                            CopyBaseColor(bc1, bc);
                            li.Add(bc1);
                        }


                    }
                    if (!bl)
                    {
                        FORBaseColor bc1 = new FORBaseColor();
                        bc1.UID = Guid.NewGuid().ToString();
                        bc1.Remark = id + "-" + forcolor;
                        bc1.Title = row["Title"].ToString();
                        bc1.Color = 0;
                        bc1.Color1 = Color.Black;
                        Services.BaseService.Create<FORBaseColor>(bc1);
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
			FORBaseColor obj = new FORBaseColor();
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
			FORBaseColor obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//���������һ������
			FORBaseColor objCopy = new FORBaseColor();
			DataConverter.CopyTo<FORBaseColor>(obj, objCopy);

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
			DataConverter.CopyTo<FORBaseColor>(objCopy, obj);
			//ˢ�±��
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// ɾ���������
		/// </summary>
		public void DeleteObject()
		{
			//��ȡ�������
			FORBaseColor obj = FocusedObject;
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
				Services.BaseService.Delete<FORBaseColor>(obj);
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
            foreach (FORBaseColor bc in ObjectList)
            {
                try
                {
                    bc.Color = ColorTranslator.ToOle(bc.Color1);
                    Services.BaseService.Update<FORBaseColor>(bc);
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

        private void CopyBaseColor(FORBaseColor bc1, FORBaseColor bc2)
        {
            bc1.UID = bc2.UID;
            bc1.Title = bc2.Title;
            bc1.Remark = bc2.Remark;
            bc1.Color = bc2.Color;
            bc1.Color1 = ColorTranslator.FromOle(bc2.Color);
        }
	}
}
