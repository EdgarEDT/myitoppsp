
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
using Itop.Domain.Chen;
#endregion

namespace Itop.Client.Chen
{
	public partial class CtrlPSP_VolumeBalance_Calc : UserControl
	{
		#region ���췽��
		public CtrlPSP_VolumeBalance_Calc()
		{
			InitializeComponent();
		}
		#endregion

		#region �ֶ�
		protected bool _bAllowUpdate = true;
        string formtitle = "";
        public string FormTitle
        {
            get { return formtitle; }
            set { formtitle = value; }
        }

        string ctrtitle = "";
        /// <summary>
        /// ��ȡflag����
        /// </summary>
        public string CtrTitle
        {
            get { return ctrtitle; }
            set { ctrtitle = value; }
        }

		#endregion

		#region ��������


        string dy = "";
        /// <summary>
        /// ��ȡ��ѹ����
        /// </summary>
        public string DY
        {
            get { return dy; }
            set { dy = value; }
        }

        string type = "";
        /// <summary>
        /// ��ȡtype����
        /// </summary>
        public string TYPE
        {
            get { return type; }
            set { type=value; }
        }

        string flag = "";
        /// <summary>
        /// ��ȡflag����
        /// </summary>
        public string FLAG
        {
            get { return flag; }
            set { flag=value; }
        }


        double sum = 0.0;
        /// <summary>
        /// ��ȡsum����
        /// </summary>
        public double SUM
        {
            get { return sum; }
            set { sum = value; }
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
		public IList<PSP_VolumeBalance_Calc> ObjectList
		{
			get { return this.gridControl.DataSource as IList<PSP_VolumeBalance_Calc>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
		public PSP_VolumeBalance_Calc FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PSP_VolumeBalance_Calc; }
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
        /// ����ϼ�
        /// </summary>
        /// <returns>ture:�ɹ�  false:ʧ��</returns>
        public bool SaveSum()
        {
            try
            {
                double s = 0.0;
                sum = 0;
                foreach (PSP_VolumeBalance_Calc pvc in ObjectList)
                {
                    sum += pvc.Vol;
                }

                PSP_VolumeBalance pv = Services.BaseService.GetOneByKey<PSP_VolumeBalance>(flag);
                if (flag == "")
                    return false;

                switch (type)
                { 
                    case "1":
                        pv.L2 = sum;
                        break;
                    case "2":
                        pv.L10 = sum;
                        break;
                    case "3":
                        pv.L11 = sum;
                        break;
                }
                Services.BaseService.Update<PSP_VolumeBalance>(pv);
            }
            catch (Exception exc)
            {
                //Debug.Fail(exc.Message);
                //HandleException.TryCatch(exc);
                return false;
            }

            return true;
        }
        /// <summary>
        /// ɾ���󱣴�ϼ�
        /// </summary>
        /// <returns>ture:�ɹ�  false:ʧ��</returns>
        public bool deleSaveSum(IList<PSP_VolumeBalance_Calc> ObjectList1,string flag)
        {
            try
            {
                double s = 0.0;
                sum = 0;
                foreach (PSP_VolumeBalance_Calc pvc in ObjectList1)
                {
                    sum += pvc.Vol;
                }

                PSP_VolumeBalance pv = Services.BaseService.GetOneByKey<PSP_VolumeBalance>(flag);
                if (flag == "")
                    return false;

                switch (type)
                {
                    case "1":
                        pv.L2 = sum;
                        break;
                    case "2":
                        pv.L10 = sum;
                        break;
                    case "3":
                        pv.L11 = sum;
                        break;
                }
                Services.BaseService.Update<PSP_VolumeBalance>(pv);
            }
            catch (Exception exc)
            {
                //Debug.Fail(exc.Message);
                //HandleException.TryCatch(exc);
                return false;
            }

            return true;
        }


		/// <summary>
		/// ˢ�±���е�����
		/// </summary>
		/// <returns>ture:�ɹ�  false:ʧ��</returns>
		public bool RefreshData()
		{
			try
			{
                PSP_VolumeBalance_Calc pvc=new PSP_VolumeBalance_Calc();
                pvc.Type=type;
                pvc.Flag=flag;


                IList<PSP_VolumeBalance_Calc> list = Services.BaseService.GetList<PSP_VolumeBalance_Calc>("SelectPSP_VolumeBalance_CalcByTypeFlag", pvc);
				this.gridControl.DataSource = list;
                SaveSum();
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
			PSP_VolumeBalance_Calc obj = new PSP_VolumeBalance_Calc();
            obj.Flag = flag;
            obj.Type = type;
            obj.Col1 = dy;
            obj.CreateTime = DateTime.Now;

			//ִ����Ӳ���
			using (FrmPSP_VolumeBalance_CalcDialog dlg = new FrmPSP_VolumeBalance_CalcDialog())
			{
				dlg.IsCreate = true;    //�����½���־
				dlg.Object = obj;
                dlg.Type = type;
                dlg.CtrTitle = ctrtitle;
                dlg.FormTitle = FormTitle;
                if (type == "1" && dy=="220")
                dlg.editorRow2.Visible = true;
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
     
			//���¶�����뵽������
			ObjectList.Add(obj);
            SaveSum();
			//ˢ�±�񣬲��������ж�λ���¶����ϡ�
			gridControl.RefreshDataSource();
			GridHelper.FocuseRow(this.gridView, obj);
            if (obj.Type == "1")
            {
                PSP_VolumeBalance ob = new PSP_VolumeBalance();
                //ob.TypeID = dy;
                ob.UID = obj.Flag;
                //string s ="";
                IList<PSP_VolumeBalance> list = Services.BaseService.GetList<PSP_VolumeBalance>("SelectPSP_VolumeBalanceByKey", ob);
                if(list!=null)
                    if (list.Count == 1)
                    {
                        
                        ob.Flag = list[0].Flag;
                        ob.TypeID = list[0].TypeID;
                        //s = obj.Flag;
                        list.Clear();
                        list = Services.BaseService.GetList<PSP_VolumeBalance>("SelectPSP_VolumeBalanceByTypeID", ob);
                        foreach (PSP_VolumeBalance pvm in list)
                        {
                            if (pvm.UID == ob.UID)
                                continue;
                            PSP_VolumeBalance_Calc pvc = new PSP_VolumeBalance_Calc();
                            pvc = obj;
                            pvc.Flag = pvm.UID;
                            pvc.UID = Guid.NewGuid().ToString();
                            pvc.Vol = 0;
                            pvc.Col2 = "";
                            Services.BaseService.Create<PSP_VolumeBalance_Calc>(pvc);
                        }
                    }
                RefreshData();
            }
           
		}

		/// <summary>
		/// �޸Ľ������
		/// </summary>
		public void UpdateObject()
		{
			//��ȡ�������
			PSP_VolumeBalance_Calc obj = FocusedObject;
			if (obj == null)
			{
				return;
			}
            PSP_VolumeBalance_Calc objtemp = new PSP_VolumeBalance_Calc ();
            objtemp.Title = obj.Title;
            objtemp.Type = obj.Type;
			//���������һ������
			PSP_VolumeBalance_Calc objCopy = new PSP_VolumeBalance_Calc();
			DataConverter.CopyTo<PSP_VolumeBalance_Calc>(obj, objCopy);

			//ִ���޸Ĳ���
			using (FrmPSP_VolumeBalance_CalcDialog dlg = new FrmPSP_VolumeBalance_CalcDialog())
			{
				dlg.Object = objCopy;   //�󶨸���
                dlg.Type = type;
                dlg.CtrTitle = ctrtitle;
                dlg.FormTitle = FormTitle;
                if (type == "1" && dy == "220")
                    dlg.editorRow2.Visible = true;
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//�ø������½������
			DataConverter.CopyTo<PSP_VolumeBalance_Calc>(objCopy, obj);
			//ˢ�±��
            SaveSum();
			gridControl.RefreshDataSource();
            if (objtemp.Title!=obj.Title)
             if (obj.Type == "1")
            {
                
                IList<PSP_VolumeBalance_Calc> list = Services.BaseService.GetList<PSP_VolumeBalance_Calc>("SelectPSP_VolumeBalance_CalcByTypeTitle", objtemp);
                foreach (PSP_VolumeBalance_Calc pvm in list)
                {
                    PSP_VolumeBalance_Calc pvc = new PSP_VolumeBalance_Calc();
                    pvc = pvm;
                    pvc.Title = obj.Title;
                 
                    Services.BaseService.Update<PSP_VolumeBalance_Calc>(pvc);
                }
            }
		}

		/// <summary>
		/// ɾ���������
		/// </summary>
		public void DeleteObject()
		{
			//��ȡ�������
			PSP_VolumeBalance_Calc obj = FocusedObject;
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
				Services.BaseService.Delete<PSP_VolumeBalance_Calc>(obj);
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
            SaveSum();
			gridControl.RefreshDataSource();
			//�����µĽ���������
			GridHelper.FocuseRowAfterDelete(this.gridView, iOldHandle);
			this.gridView.EndUpdate();
            if (obj.Type == "1")
            {
                IList<PSP_VolumeBalance_Calc> list = Services.BaseService.GetList<PSP_VolumeBalance_Calc>("SelectPSP_VolumeBalance_CalcByTypeTitle", obj);
                foreach (PSP_VolumeBalance_Calc pvm in list)
                {
                    PSP_VolumeBalance_Calc pvc = new PSP_VolumeBalance_Calc();
                    pvc = pvm;
                    pvc.Title = pvm.Title;
                    
                    Services.BaseService.Delete<PSP_VolumeBalance_Calc>(pvc);
                    IList<PSP_VolumeBalance_Calc> list1 = Services.BaseService.GetList<PSP_VolumeBalance_Calc>("SelectPSP_VolumeBalance_CalcByTypeFlag", pvm);
                    deleSaveSum(list1, pvc.Flag);
                }
            }
		}
		#endregion
	}
}
