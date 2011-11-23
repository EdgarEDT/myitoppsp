
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
	public partial class CtrlPowerProTypes : UserControl
	{
		#region ���췽��
		public CtrlPowerProTypes()
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
		public IList<PowerProTypes> ObjectList
		{
			get { return this.gridControl.DataSource as IList<PowerProTypes>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
		public PowerProTypes FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PowerProTypes; }
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


        ///// <summary>
        ///// ��������
        ///// </summary>
        ///// <param name="layer">ͼ��ID</param>
        ///// <param name="isrun">�Ƿ����У��滮  true������</param>
        ///// <returns></returns>
        ///// 










        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="layer">ͼ��ID</param>
        /// <param name="isrun">�Ƿ����У��滮  true������</param>
        /// <param name="IsLine">�Ƿ���·�������</param>
        /// <returns></returns>
        public bool RefreshData(string layer, bool isrun,bool IsLine)
		{

            IList<PowerProTypes> list = new List<PowerProTypes>();

            int a1 = 1;
            if (IsLine)
            { a1 = 1; }
            else
            { a1 = 2; }

			try
			{
                if (isrun)
                {
                    
                    PowerProTypes pp1 = new PowerProTypes();
                    pp1.Flag = a1;
                    pp1.Flag2 = "-1";
                    list = Services.BaseService.GetList<PowerProTypes>("SelectPowerProTypesByFlagFlag2", pp1);
                }
                else
                {
                    PowerProTypes pp2 = new PowerProTypes();
                    pp2.Flag = a1;
                    pp2.Flag2 = layer;
                    list = Services.BaseService.GetList<PowerProTypes>("SelectPowerProTypesByFlagFlag21", pp2);
                
                }




				//IList<PowerProTypes> list = Services.BaseService.GetStrongList<PowerProTypes>();
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




        public bool RefreshData(string layer, bool isrun, bool IsLine,string power)
        {

            IList<PowerProTypes> list = new List<PowerProTypes>();

            int a1 = 1;
            if (IsLine)
            { a1 = 1; }
            else
            { a1 = 2; }

            try
            {
                if (isrun)
                {

                    PowerProTypes pp1 = new PowerProTypes();
                    pp1.Flag = a1;
                    pp1.Flag2 = "-1";
                    if (power != "")
                    {
                        switch (power)
                        {
                            case "500":
                                power = "'500'";
                                break;

                            case "220":
                                power = "'220'";
                                break;

                            case "110":
                                power = "'110'";
                                break;

                            case "35":
                                power = "'35','3'";
                                break;

                            case "10":
                                power = "'10','1'";
                                break;
                        }
                        pp1.ParentID = power;
                        list = Services.BaseService.GetList<PowerProTypes>("SelectPowerProTypesByFlagFlag2", pp1);
                    }
                    else
                    {
                        
                        list = Services.BaseService.GetList<PowerProTypes>("SelectPowerProTypesByFlagFlag3", pp1);
                    }
                }
                else
                {
                    PowerProTypes pp2 = new PowerProTypes();
                    pp2.Flag = a1;
                    pp2.Flag2 = layer;
                    if (power != "")
                    {
                        switch (power)
                        { 
                            case "500":
                                power = "'500'";
                                break;

                            case "220":
                                power = "'220'";
                                break;

                            case "110":
                                power = "'110'";
                                break;

                            case "35":
                                power = "'35','3'";
                                break;

                            case "10":
                                power = "'10','1'";
                                break;
                        }


                        pp2.ParentID = power;
                        list = Services.BaseService.GetList<PowerProTypes>("SelectPowerProTypesByFlagFlag21", pp2);
                    }
                    else
                    {
                        list = Services.BaseService.GetList<PowerProTypes>("SelectPowerProTypesByFlagFlag31", pp2);
                    }

                }




                //IList<PowerProTypes> list = Services.BaseService.GetStrongList<PowerProTypes>();
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
            //    return;
            //}
            ////�½�����
            //PowerProTypes obj = new PowerProTypes();

            ////ִ����Ӳ���
            //using (FrmPowerProTypesDialog dlg = new FrmPowerProTypesDialog())
            //{
            //    dlg.IsCreate = true;    //�����½���־
            //    dlg.Object = obj;
            //    if (dlg.ShowDialog() != DialogResult.OK)
            //    {
            //        return;
            //    }
            //}

            ////���¶�����뵽������
            //ObjectList.Add(obj);

            ////ˢ�±�񣬲��������ж�λ���¶����ϡ�
            //gridControl.RefreshDataSource();
            //GridHelper.FocuseRow(this.gridView, obj);
		}

		/// <summary>
		/// �޸Ľ������
		/// </summary>
		public void UpdateObject()
		{
			//��ȡ�������
            //PowerProTypes obj = FocusedObject;
            //if (obj == null)
            //{
            //    return;
            //}

            ////���������һ������
            //PowerProTypes objCopy = new PowerProTypes();
            //DataConverter.CopyTo<PowerProTypes>(obj, objCopy);

            ////ִ���޸Ĳ���
            //using (FrmPowerProTypesDialog dlg = new FrmPowerProTypesDialog())
            //{
            //    dlg.Object = objCopy;   //�󶨸���
            //    if (dlg.ShowDialog() != DialogResult.OK)
            //    {
            //        return;
            //    }
            //}

            ////�ø������½������
            //DataConverter.CopyTo<PowerProTypes>(objCopy, obj);
            ////ˢ�±��
            //gridControl.RefreshDataSource();
		}

		/// <summary>
		/// ɾ���������
		/// </summary>
		public void DeleteObject()
		{
			//��ȡ�������
			PowerProTypes obj = FocusedObject;
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
				Services.BaseService.Delete<PowerProTypes>(obj);
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
