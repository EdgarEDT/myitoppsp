
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
using System.IO;
using DevExpress.XtraGrid.Columns;
#endregion

namespace Itop.Client.Stutistics
{
	public partial class CtrlPSP_PowerSubstationInfo : UserControl
    {
        private string types1 = "";
        private string flags1 = "";
        private string types2 = "";
        private string text = "";
        public bool editright = true;
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
        public string Text
        {

            set { text = value; }
        }

        bool isselect = false;

		#region ���췽��
		public CtrlPSP_PowerSubstationInfo()
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
			get { return gridView1; }
		}

		/// <summary>
		/// ��ȡGridControl������Դ����������б�
		/// </summary>
		public IList<PSP_PowerSubstationInfo> ObjectList
		{
			get { return this.gridControl.DataSource as IList<PSP_PowerSubstationInfo>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
		public PSP_PowerSubstationInfo FocusedObject
		{
            get { return this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as PSP_PowerSubstationInfo; }
		}
		#endregion

		#region �¼�����
		private void gridView_DoubleClick(object sender, EventArgs e)
        {
            if (!editright)
            {
                return ;
            }
			// �ж�"˫�������޸�"��־ 
			if (!AllowUpdate)
			{
				return;
			}
            
			//���������ڵ�Ԫ���У���༭�������
			Point point = this.gridControl.PointToClient(Control.MousePosition);
            if (GridHelper.HitCell(this.gridView1, point))
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
          
            ComponentPrint.ShowPreview(this.gridControl, this.gridView1.GroupPanelText);
		}

		/// <summary>
		/// ˢ�±���е�����
		/// </summary>
		/// <returns>ture:�ɹ�  false:ʧ��</returns>
        public bool RefreshData1()
        {
            try
            {

                IList<PSP_PowerSubstationInfo> list = Services.BaseService.GetList<PSP_PowerSubstationInfo>("SelectPSP_PowerSubstationInfoByFlag", flags1 + "|" + Itop.Client.MIS.ProgUID.Substring(0, 20));
                //string filepath = Path.GetTempPath() + "\\" + Path.GetFileName("PowerSubstationLayOut.xml");
               // if (File.Exists(filepath))
                //{
                 //this.bandedGridView1.RestoreLayoutFromXml(filepath);
                //this.gridView1.re
               // }
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

        public bool RefreshData()
		{
			try
            {

                IList<PSP_PowerSubstationInfo> list = Services.BaseService.GetList<PSP_PowerSubstationInfo>("SelectPSP_PowerSubstationInfoByFlag", flags1 + "|" + Itop.Client.MIS.ProgUID.Substring(0, 20));
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
            PSP_PowerSubstationInfo obj = new PSP_PowerSubstationInfo();
           
            obj.Flag = flags1;
            obj.CreateDate = DateTime.Now;

            //ִ����Ӳ���
            using (FrmPSP_PowerSubstationInfoDialog dlg = new FrmPSP_PowerSubstationInfoDialog())
            {
                //dlg.Text = "";
                dlg.Type = types1;
                dlg.Flag = flags1 ;
                dlg.Type2 = types2 + "|" + Itop.Client.MIS.ProgUID.Substring(0, 20);
                dlg.ctrlPSP_PowerSubstationInfo = this;
                dlg.Text = this.text;
                dlg.IsCreate = true;    //�����½���־
                dlg.Object = obj;
                dlg.rowTitle.Properties.Caption = this.colTitle.Caption;
                dlg.rowPowerName.Properties.Caption = this.colPowerName.Caption;
                if (dlg.ShowDialog() != DialogResult.OK)
                {

                    return;
                }
            }

            //���¶�����뵽������
            ObjectList.Add(obj);

            //ˢ�±�񣬲��������ж�λ���¶����ϡ�
            gridControl.RefreshDataSource();
            GridHelper.FocuseRow(this.gridView1, obj);
        }

        /// <summary>
        /// �޸Ľ������
        /// </summary>
        public void UpdateObject()
        {
            //��ȡ�������
            PSP_PowerSubstationInfo obj = FocusedObject;
            if (obj == null)
            {
                return;
            }

            //���������һ������
            PSP_PowerSubstationInfo objCopy = new PSP_PowerSubstationInfo();
            DataConverter.CopyTo<PSP_PowerSubstationInfo>(obj, objCopy);

            //ִ���޸Ĳ���
            using (FrmPSP_PowerSubstationInfoDialog dlg = new FrmPSP_PowerSubstationInfoDialog())
            {
                dlg.IsSelect = isselect;
                dlg.Type = types1;
                dlg.Flag = flags1;
                dlg.Type2 = types2 + "|" + Itop.Client.MIS.ProgUID.Substring(0, 20);
                dlg.Text = this.text;
                dlg.ctrlPSP_PowerSubstationInfo = this;

                dlg.Object = objCopy;   //�󶨸���
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            //�ø������½������
            DataConverter.CopyTo<PSP_PowerSubstationInfo>(objCopy, obj);
            //ˢ�±��
            gridControl.RefreshDataSource();
        }

        /// <summary>
        /// ɾ���������
        /// </summary>
        public void DeleteObject()
        {
            //��ȡ�������
            PSP_PowerSubstationInfo obj = FocusedObject;
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
              //  Services.BaseService.Delete<PSP_PowerSubstationInfo>(obj);
                Services.BaseService.Update("DeletePSP_PowerSubstationInfoByUID", obj.UID);
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return;
            }

            this.gridView1.BeginUpdate();
            //��ס��ǰ����������
            int iOldHandle = this.gridView1.FocusedRowHandle;
            //��������ɾ��
            ObjectList.Remove(obj);
            //ˢ�±��
            gridControl.RefreshDataSource();
            //�����µĽ���������
            GridHelper.FocuseRowAfterDelete(this.gridView1, iOldHandle);
            this.gridView1.EndUpdate();
        }
		#endregion
         

        private void gridView1_Layout(object sender, EventArgs e)
        {    
            //string filepath = Path.GetTempPath();
            //this.ctrlPSP_PowerSubstationInfo1.bandedGridView1.SaveLayoutToXml(filepath + "SubstationLayOut.xml");
            PowerSubstationLine psl = new PowerSubstationLine();
            psl.Flag = flags1;
            psl.Type2 = types2 + "|" + Itop.Client.MIS.ProgUID.Substring(0, 20);

            IList<PowerSubstationLine> li = Itop.Client.Common.Services.BaseService.GetList<PowerSubstationLine>("SelectPowerSubstationLineByFlagType2", psl);



            foreach (PowerSubstationLine psp in li)
            {
                foreach (GridColumn gc in this.gridView1.VisibleColumns)
                {
                    try
                    {
                        if (gc.FieldName.Substring(0, 1) == "S")
                        {
                            if (gc.FieldName == psp.ClassType)
                                {
                                    //gc.Caption = pss.Title;
                                    psp.Type = gc.VisibleIndex.ToString();
                                    //gc.Visible = true;
                                    //gc.Columns[0].Caption = pss.Title;
                                    //gc.Columns[0].Visible = true;
                                    //gc.Columns[0].OptionsColumn.ShowInCustomizationForm = true;

                                }
                        }
                    }
                    catch
                    { }

                }
                if(psp.Type!="-1")
                   Itop.Client.Common.Services.BaseService.Update("UpdatePowerSubstationLine", psp);
            }


        }

	}
}
