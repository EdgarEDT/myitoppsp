
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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
#endregion

namespace Itop.Client.Stutistics
{
	public partial class CtrlLine_beizhu : UserControl
	{
        private string types1 = "";
        private string types2 = "";
        private string flags1 = "";
        private string zz = "";
        public string Type
        {
            get { return types1; }
            set { types1 = value; }
        }

        public string Type2
        {
            get { return types2; }
            set { types2 = value; }
        }

        public string Flag
        {
            get { return flags1; }
            set { flags1 = value; }
        }


        public bool IsSelect
        {
            get { return isselect; }
            set { isselect = value; }
        }

        bool isselect = false;




		#region ���췽��
		public CtrlLine_beizhu()
		{
			InitializeComponent();
		}
        public CtrlLine_beizhu(string z)
        {
            InitializeComponent();
            zz = z;
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
        /// 
        ///DevExpress.XtraGrid.Views.BandedGrid.BandedGridView

        public BandedGridView GridView
		{
			get { return this.bandedGridView2; }
		}

		/// <summary>
		/// ��ȡGridControl������Դ����������б�
		/// </summary>
		public IList<Line_beizhu> ObjectList
		{
            get { return this.gridControl.DataSource as IList<Line_beizhu>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
		public Line_beizhu FocusedObject
		{
            get { return this.bandedGridView2.GetRow(this.bandedGridView2.FocusedRowHandle) as Line_beizhu; }
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
            if (GridHelper.HitCell(this.bandedGridView2, point))
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
            ComponentPrint.ShowPreview(this.gridControl, this.bandedGridView2.GroupPanelText);
		}

		/// <summary>
		/// ˢ�±���е�����
		/// </summary>
		/// <returns>ture:�ɹ�  false:ʧ��</returns>
		public bool RefreshData()
		{
			try
			{
                IList<Line_beizhu> list = new List<Line_beizhu>();


                int dd = 0;
                Line_beizhu li = new Line_beizhu();
                li.DY = dd;
                li.Flag = zz;

                if (types2 == "66")
                { list = Services.BaseService.GetList<Line_beizhu>("SelectLine_beizhuByFlagDy", li); }
                else if (types2 == "10")
                { list = Services.BaseService.GetList<Line_beizhu>("SelectLine_beizhuByFlagDy1", li); }




                //IList<Line_Info> list = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByFlagDy", li);
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






        public bool RefreshData(string type)
        {
            IList<Line_beizhu> list = new List<Line_beizhu>();
            try
            {
                flags1 = type;
                int dd = 0;
                if (types2 == "66")
                { dd = 66; }
                else if (types2 == "10")
                { dd = 10; }

                Line_beizhu li = new Line_beizhu();
                li.DY = dd;
                li.Flag = flags1;


                if (types2 == "66")
                { list = Services.BaseService.GetList<Line_beizhu>("SelectLine_beizhuByFlagDy", li); }
                else if (types2 == "10")
                { list = Services.BaseService.GetList<Line_beizhu>("SelectLine_beizhuByFlagDy1", li); }




                //IList<Line_Info> list = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByFlagDy", li);
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




        public bool RefreshData(string layer, bool isrun, string power)
        {

            IList<Line_beizhu> lists = new List<Line_beizhu>();
            try
            {

                Line_beizhu ll1 = new Line_beizhu();
                ll1.AreaID = layer;
                ll1.DY = int.Parse(power);

                if (isrun)
                {
                    lists = Services.BaseService.GetList<Line_beizhu>("SelectLine_beizhuByXZ", ll1);
                }
                else
                {
                    lists = Services.BaseService.GetList<Line_beizhu>("SelectLine_beizhuByGH", ll1);
                }
               
                this.gridControl.DataSource = lists;


                //foreach (GridColumn gc in this.bandedGridView2.Columns)
                //{
                //    gc.Visible = false;
                //    gc.OptionsColumn.ShowInCustomizationForm = false;
                //    if (gc.FieldName == "Title" || gc.FieldName == "DY" || gc.FieldName == "K2" || gc.FieldName == "K5")
                //    {
                //        gc.Visible = true;
                //        gc.OptionsColumn.ShowInCustomizationForm = true;
                //    }
                //}
                bandedGridView2.OptionsView.ColumnAutoWidth = true;


                foreach (GridBand gc in this.bandedGridView2.Bands)
                {
                    try
                    {
                        gc.Visible = false;

                        if (gc.Columns[0].FieldName == "Title" || gc.Columns[0].FieldName == "DY" || gc.Columns[0].FieldName == "K2" || gc.Columns[0].FieldName == "K5")
                        {
                            gc.Visible = true;
                            gc.Caption = gc.Columns[0].Caption;
                            gc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        }

                        
                    }
                    catch { }

                }


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
            Line_beizhu obj = new Line_beizhu();
            obj.L6 = DateTime.Now;

			//ִ����Ӳ���
            using (FrmLine_InfoDialog_beizhu dlg = new FrmLine_InfoDialog_beizhu())
			{
                if(gridControl.DataSource!=null)
                    dlg.LIST = (IList<Line_beizhu>)gridControl.DataSource;

                dlg.Type = types1;
                dlg.Flag = flags1;
                dlg.Type2 = types2;
               // dlg.ctrlLint_beizhu = this;

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
            GridHelper.FocuseRow(this.bandedGridView2, obj);
		}

		/// <summary>
		/// �޸Ľ������
		/// </summary>
		public void UpdateObject()
		{
			//��ȡ�������



            Line_beizhu obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//���������һ������
            Line_beizhu objCopy = new Line_beizhu();
            DataConverter.CopyTo<Line_beizhu>(obj, objCopy);

			//ִ���޸Ĳ���
            using (FrmLine_InfoDialog_beizhu dlg = new FrmLine_InfoDialog_beizhu())
			{
                if (gridControl.DataSource != null)
                    dlg.LIST = (IList<Line_beizhu>)gridControl.DataSource;

                dlg.IsSelect = isselect;
               // dlg.ctrlLint_Info = this;
                dlg.Type = types1;
                dlg.Flag = flags1;
                dlg.Type2 = types2;


				dlg.Object = objCopy;   //�󶨸���
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//�ø������½������
            DataConverter.CopyTo<Line_beizhu>(objCopy, obj);
			//ˢ�±��
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// ɾ���������
		/// </summary>
		public void DeleteObject()
		{
			//��ȡ�������
            Line_beizhu obj = FocusedObject;
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
                Services.BaseService.Delete<Line_beizhu>(obj);
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
				HandleException.TryCatch(exc);
				return;
			}

            this.bandedGridView2.BeginUpdate();
			//��ס��ǰ����������
            int iOldHandle = this.bandedGridView2.FocusedRowHandle;
			//��������ɾ��
			ObjectList.Remove(obj);
			//ˢ�±��
			gridControl.RefreshDataSource();
			//�����µĽ���������
            GridHelper.FocuseRowAfterDelete(this.bandedGridView2, iOldHandle);
            this.bandedGridView2.EndUpdate();
		}
		#endregion
	}
}
