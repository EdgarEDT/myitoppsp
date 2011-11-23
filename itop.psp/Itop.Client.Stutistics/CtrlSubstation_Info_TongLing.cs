
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
using Itop.Domain.Stutistic;
using System.IO;
using Itop.Domain.Layouts;
using System.Collections;
using Itop.Client.ExpressCalculate;
using Itop.Domain.ExpressCalculate;
using DevExpress.XtraGrid.Columns;
#endregion

namespace Itop.Client.Stutistics
{
	public partial class CtrlSubstation_Info_TongLing : UserControl
	{
		#region ���췽��
		public CtrlSubstation_Info_TongLing()
		{
			InitializeComponent();
		}
		#endregion

		#region �ֶ�
		protected bool _bAllowUpdate = true;
        private string Expressiontype = "0";
        private string types1 = "";
        private string types2 = "";
        private string flags1 = "";
        private string xmlflag = "";
        private string flags = "";
        private string xmltype = "";
        public bool editright = true;
        public string Type1
        {
            get { return types1; }
            set { types1 = value; }
        }
        public string Type2
        {
            get { return types2; }
            set { types2 = value; }
        }
        public string Flag1
        {
            get { return flags1; }
            set { flags1 = value; }
        }
        public string Flag
        {
            get { return flags; }
            set { flags= value; }
        }

        public bool IsSelect
        {
            get { return isselect; }
            set { isselect = value; }
        }
        public string XmlFlag
        {
            get { return xmlflag; }
            set { xmlflag = value; }
        }
        public string XmlType
        {
            get { return xmltype; }
            set { xmltype = value; }
        }
        public string ExpressionType
        {
            get { return Expressiontype; }
            set { Expressiontype = value; }
        }
        bool isselect = false;
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
		public IList<Substation_Info> ObjectList
		{
			get { return this.gridControl.DataSource as IList<Substation_Info>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
		public Substation_Info FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as Substation_Info; }
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
				IList<Substation_Info> list = Services.BaseService.GetStrongList<Substation_Info>();
                int i = 1;
                foreach (Substation_Info listtemp in list)
                {
                    listtemp.AreaID = i.ToString();
                    i++;
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
        /// ˢ�±���е�����
        /// </summary>
        /// <returns>ture:�ɹ�  false:ʧ��</returns>
        public bool RefreshData1()
        {
            try
            {
                string filepath = "";
                Econ ec = new Econ();
                
                if (xmlflag == "guihua")
                    ec.UID = xmltype;
                else
                {
                    ec.UID = xmltype + "SubstationLayOut";
                }
                if (xmlflag == "guihuaExpressCal")
                    ec.UID = xmltype;
               
                IList<Econ> listxml = Services.BaseService.GetList<Econ>("SelectEconByKey", ec);
                if (listxml.Count!=0)
                {
                    MemoryStream ms = new MemoryStream(listxml[0].ExcelData);
                    this.gridView.RestoreLayoutFromStream(ms);
                }
                IList<Substation_Info> list = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByFlag",flags);
                int i = 1;
                foreach (Substation_Info listtemp in list)
                {
                    listtemp.AreaID = i.ToString();
                    i++;
                }
            //  foreach (GridColumn gc in GridView.Columns)
            //{
                
            //        if (gc.FieldName.Substring(0, 1) == "S")
            //        {
            //            PSP_ExpressionCalculator express = new PSP_ExpressionCalculator();
            //            express.FiledName = gc.FieldName;
            //            express.Flag = 0;
            //             IList<PSP_ExpressionCalculator> listtemp = Services.BaseService.GetList<PSP_ExpressionCalculator>("SelectPSP_ExpressionCalculatorByFiledNameFlag",express);
            //           if(listtemp!=null)
            //            if(listtemp.Count>0)
            //            {
                            
            //                ExpressionCalculator ca = new ExpressionCalculator();

            //                ca.RowCalculator(listtemp[0].Expression, this.gridView, ref list, gc.FieldName,listtemp[0].SaveDecimalPoint);
            //            }
            //        }

            //    }
              
             
                
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



        public void RefreshLayout()
        {
            try
            {
                Econ ec = new Econ();

                if (xmlflag == "guihua")
                    ec.UID = xmltype;
                else
                {
                    ec.UID = xmltype + "SubstationLayOut";
                }
                IList<Econ> listxml = Services.BaseService.GetList<Econ>("SelectEconByKey", ec);
                if (listxml.Count != 0)
                {
                    MemoryStream ms = new MemoryStream(listxml[0].ExcelData);
                    this.gridView.RestoreLayoutFromStream(ms);
                }
            }
            catch { }
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
			Substation_Info obj = new Substation_Info();
            obj.Flag = flags;
            if (ObjectList.Count > 0)
            {
                obj.AreaID = Convert.ToInt32(ObjectList[ObjectList.Count - 1].AreaID) + 1+"";
            }
            else
                obj.AreaID="1";
			//ִ����Ӳ���
			using (FrmSubstation_InfoDialog_TongLing dlg = new FrmSubstation_InfoDialog_TongLing())
			{
                dlg.Type1 = types1;
                dlg.Flag = flags1;
                dlg.Type2 = types2;
                dlg.ctrlSubstation_Info = this;

                dlg.IsCreate = true;    //�����½���־
                dlg.Object = obj;
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//���¶�����뵽������
			ObjectList.Add(obj);
            RowCalculate(ref obj);
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
			Substation_Info obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//���������һ������
			Substation_Info objCopy = new Substation_Info();
			DataConverter.CopyTo<Substation_Info>(obj, objCopy);

			//ִ���޸Ĳ���
			using (FrmSubstation_InfoDialog_TongLing dlg = new FrmSubstation_InfoDialog_TongLing())
			{
				dlg.Object = objCopy;   //�󶨸���
                dlg.Flag = flags1;
                dlg.Type1 = types1;
                dlg.Type2 = types2;
                dlg.ctrlSubstation_Info = this;
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//�ø������½������
			DataConverter.CopyTo<Substation_Info>(objCopy, obj);
            RowCalculate(ref  obj);
			//ˢ�±��
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// ɾ���������
		/// </summary>
		public void DeleteObject()
		{
			//��ȡ�������
			Substation_Info obj = FocusedObject;
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
				Services.BaseService.Delete<Substation_Info>(obj);
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
        public void Calculate(string filedname, string StrExpressiontemp, int SaveDecimalPoint)
        {
            if (ObjectList == null)
                return;
            IList<Substation_Info> listtemp = ObjectList;
            ExpressionCalculator ca = new ExpressionCalculator();

            if (ca.RowCalculator(StrExpressiontemp, this.gridView, ref listtemp, filedname, SaveDecimalPoint))
            {
                RowCalculate(ref listtemp);
                //ˢ�±�񣬲��������ж�λ���¶����ϡ�


            }
            else
            {
                foreach (Substation_Info Subtemp in listtemp)
                {
                    Services.BaseService.Update<Substation_Info>(Subtemp);
                }
            }
              gridControl.RefreshDataSource();
        }
        public void Calculate()
        {
            if (ObjectList == null)
                return;
            IList<Substation_Info> listtemp = ObjectList;
            RowCalculate(ref listtemp);
                //ˢ�±�񣬲��������ж�λ���¶����ϡ�
                gridControl.RefreshDataSource();
        }
        public void RowCalculate(ref  Substation_Info obj)
        {
            foreach (GridColumn gc in GridView.Columns)
            {

                if (gc.FieldName.Substring(0, 1) == "S")
                {
                    PSP_ExpressionCalculator express = new PSP_ExpressionCalculator();
                    express.FiledName = gc.FieldName;
                    express.Flag = Expressiontype;
                    IList<PSP_ExpressionCalculator> listtemp = Services.BaseService.GetList<PSP_ExpressionCalculator>("SelectPSP_ExpressionCalculatorByFiledNameFlag", express);
                    if (listtemp != null)
                        if (listtemp.Count > 0)
                        {

                            ExpressionCalculator ca = new ExpressionCalculator();

                            if (!ca.RowCalculator(listtemp[0].Expression, this.gridView, ref obj, gc.FieldName, listtemp[0].SaveDecimalPoint))
                                break;
                        }
                }

            }
             Services.BaseService.Update<Substation_Info>(obj);
        
        }
        public void RowCalculate(ref  IList<Substation_Info> list)
        {
            if(list==null)
                return;
            Substation_Info obj;
            for (int i = 0; i < list.Count; i++)
            {
                obj=list[i];
                RowCalculate(ref obj);
                list[i] = obj;
            }
        }
	}
}
