
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
using System.Collections;
#endregion

namespace Itop.Client.Chen
{
	public partial class CtrlPSP_VolumeBalance : UserControl
	{
		#region ���췽��
		public CtrlPSP_VolumeBalance()
		{
         
			InitializeComponent();
         
		}
		#endregion

		#region �ֶ�
		protected bool _bAllowUpdate = true;
        private int baseyear = 1990;
       private  string Volumecalc = "";
        private string loadrate = "";
        private bool AddRight = false;
        private bool EditRight = false;
        private bool DeleteRight = false;
        private bool PrintRight = false;
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
		public IList<PSP_VolumeBalance> ObjectList
		{
			get { return this.gridControl.DataSource as IList<PSP_VolumeBalance>; }
		}
        public string Volumecalc0
        {
            get { return Volumecalc; }
            set { Volumecalc = value; }
        }
        public string Loadrate
        {
            get { return loadrate; }
            set { loadrate = value; }
        }
       private string type="";
        private string flag = "";
        private string baseyeartype = "baseyear";
        public string Baseyeartype
        {
            get { return baseyeartype; }
          
        }
        public string Type
        {
            set { type = value;
            if (type == "110")
            {
                hs.Add("�����ۺ���߸���", 1);
                hs.Add("220kV����35kV��ɹ�����", 2);
            
                hs.Add("110kV������С��Դֱ�ӹ��縺��", 4);
                hs.Add("����110kV��ѹ�������", 6);
                baseyeartype = "baseyear110";
            }
            if (type == "220")
            {
                hs.Add("�ۺ���߸���", 1);
         //       hs.Add("ֱ�ӹ��縺��", 2);
              //  hs.Add("��������", 4);
                // hs.Add("110kV������С��Դֱ�ӹ��縺��", 4);
                hs.Add("����220kV��ѹ�������", 6);
                baseyeartype = "baseyear220";

            }
            else
                if (type == "35")
                {
                    hs.Add("��������", 1);
                    //       hs.Add("ֱ�ӹ��縺��", 2);
                    hs.Add("����ƽ�⸺��", 2);
                    // hs.Add("110kV������С��Դֱ�ӹ��縺��", 4);
                    hs.Add("����220kV��ѹ�������", 6);
                    baseyeartype = "baseyear35";

                }
                }
        
        }
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        public bool ADdRight
        {
            get { return AddRight; }
            set { AddRight = value; }
        }
        public bool EDitRight
        {
            get { return EditRight; }
            set { EditRight = value; }
        }
        public bool DEleteRight
        {
            get { return DeleteRight; }
            set { DeleteRight = value; }
        }
        public bool PRintRight
        {
            get { return PrintRight; }
            set { PrintRight = value; }
        }
       private  Hashtable hs = new Hashtable();



		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
		public PSP_VolumeBalance FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PSP_VolumeBalance; }
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
                int iOldHandle = this.gridView.FocusedRowHandle;
                PSP_VolumeBalance vol = new PSP_VolumeBalance();
                vol.TypeID = type;
                vol.Flag = flag;
             
                IList<PSP_VolumeBalance> list = Services.BaseService.GetList<PSP_VolumeBalance>("SelectPSP_VolumeBalanceByTypeID", vol);
				this.gridControl.DataSource = list;
                if (list != null)
                    if (list.Count > 0)
                        baseyear = list[0].Year;
            

                try

                {
                    if (Volumecalc == "")
                        Volumecalc = "0";
                    //Volumecalc = System.Configuration.ConfigurationSettings.AppSettings["Volumecalc"];
                    colL8.Caption="��"+type+"kV�������(���ر�"+Volumecalc+")";
                    GridHelper.FocuseRowAfterDelete(this.gridView, iOldHandle);
                    if (type == "35")
                       colL5.Caption = "��" + type + "ǧ��������(����ͬʱ��" + loadrate + ")";
                }
                catch { }
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
			PSP_VolumeBalance obj = new PSP_VolumeBalance();
            obj.TypeID = type;
            obj.Flag = flag;
			//ִ����Ӳ���
			using (FrmPSP_VolumeBalanceDialog dlg = new FrmPSP_VolumeBalanceDialog())
			{
                dlg.Type=type;
                dlg.Flag = flag;
                dlg.List = gridView.DataSource as IList<PSP_VolumeBalance>;
				dlg.IsCreate = true;    //�����½���־
				dlg.Object = obj;
                if (ObjectList.Count>0)
                    baseyear = ObjectList[0].Year;
                dlg.BaseYear = baseyear;
                if (type == "220")
                {
                    dlg.xtraTabControl1.TabPages[0].Text = "220ǧ���������ƽ���";
                    dlg.Text = "220ǧ���������ƽ���";
                    dlg.rowL1.Properties.Caption = "�ۺ���߸���";
                    dlg.rowL2.Properties.Caption = "ֱ�ӹ��縺��";
                    dlg.rowL2.Visible = false;
                    dlg.rowL4.Properties.Caption = "��������";
                    //dlg.rowL3.Visible = false;
                   // dlg.rowL4.Visible = true;
                    dlg.rowL6.Properties.Caption = "����220kV��ѹ�������";
                    dlg.categoryRow1.Visible = false;
                    dlg.categoryRow2.Visible = false;
                    dlg.categoryRow3.Properties.Caption = "��220kV��ѹ���縺��";
                    //dlg.rowL14.Visible = false;

                }
                else
                    if (type == "35")
                    {
                        dlg.xtraTabControl1.TabPages[0].Text = "35ǧ���������ƽ���";
                        dlg.Text = "35ǧ���������ƽ���";
                        dlg.rowL1.Properties.Caption = "��������";
                        dlg.rowL2.Properties.Caption = "����ƽ�⸺��";
                        //dlg.rowL2.Visible = false;
                        //dlg.rowL4.Properties.Caption = "��������";
                        //dlg.rowL3.Visible = false;
                        dlg.rowL4.Visible = false;
                        dlg.rowL6.Properties.Caption = "����35ǧ���������";
                        //dlg.rowL14.Visible = false;

                    }
				if (dlg.ShowDialog() != DialogResult.OK)
				{
                 
					return;
				}
			}
           
			//���¶�����뵽������
			ObjectList.Add(obj);
            reloadsum(obj);
			//ˢ�±�񣬲��������ж�λ���¶����ϡ�
			gridControl.RefreshDataSource();
			GridHelper.FocuseRow(this.gridView, obj);
            AddFuHe(obj);
            
            
            
		}

		/// <summary>
		/// �޸Ľ������
		/// </summary>
		public void UpdateObject()
		{
			//��ȡ�������
			PSP_VolumeBalance obj = FocusedObject;
            bool isresum=false;
			if (obj == null)
			{
				return;
			}

			//���������һ������
			PSP_VolumeBalance objCopy = new PSP_VolumeBalance();
			DataConverter.CopyTo<PSP_VolumeBalance>(obj, objCopy);
            if ((gridView.FocusedColumn.FieldName != "L2"&& gridView.FocusedColumn.FieldName != "L10" && gridView.FocusedColumn.FieldName != "L11")||(gridView.FocusedColumn.FieldName == "L2")&&type!="220")
            {
                //ִ���޸Ĳ���
                using (FrmPSP_VolumeBalanceDialog dlg = new FrmPSP_VolumeBalanceDialog())
                {
                    dlg.Type = type;
                    dlg.Flag = flag;
                    dlg.List = gridView.DataSource as IList<PSP_VolumeBalance>;
                    dlg.Object = objCopy;   //�󶨸���
                    baseyear = ObjectList[0].Year;
                    dlg.BaseYear = baseyear;
                    if (type == "220")
                    {
                        dlg.xtraTabControl1.TabPages[0].Text = "220ǧ���������ƽ���";
                        dlg.Text = "220ǧ���������ƽ���";
                        dlg.rowL1.Properties.Caption = "�ۺ���߸���";
                        dlg.rowL2.Properties.Caption = "ֱ�ӹ��縺��";
                        dlg.rowL2.Visible = false;
                        dlg.rowL4.Properties.Caption = "��������";
                        //dlg.rowL3.Visible = false;
                      //  dlg.rowL4.Visible = true;
                        dlg.rowL6.Properties.Caption = "����220kV��ѹ�������";
                        dlg.rowL14.Visible = false;
                        dlg.categoryRow1.Visible = false;
                        dlg.categoryRow2.Visible = false;
                        dlg.categoryRow3.Properties.Caption = "��220kV��ѹ���縺��";
                    }
                    else
                        if (type == "35")
                        {
                            dlg.xtraTabControl1.TabPages[0].Text = "35ǧ���������ƽ���";
                            dlg.Text = "35ǧ���������ƽ���";
                            dlg.rowL1.Properties.Caption = "��������";
                            dlg.rowL2.Properties.Caption = "����ƽ�⸺��";
                            dlg.rowL4.Visible = false;
                            dlg.rowL6.Properties.Caption = "����35ǧ���������";
                            dlg.categoryRow1.Properties.Caption = "����ƽ�⸺��";
                            dlg.categoryRow2.Visible = false;
                            dlg.categoryRow3.Properties.Caption = "��35ǧ����ѹ���縺��";

                        }
                    if (dlg.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    isresum=dlg.IsReSum;
                }
            }
            else
            {
                FrmBalance_Calc FrmBalance = new FrmBalance_Calc();
                FrmBalance.FormTitle = gridView.FocusedColumn.Caption;
                FrmBalance.CtrTitle = gridView.GroupPanelText; 
                PSP_VolumeBalance vol = new PSP_VolumeBalance();
                if (gridView.GetRow(gridView.FocusedRowHandle) != null)
                    vol = gridView.GetRow(gridView.FocusedRowHandle) as PSP_VolumeBalance;
                switch (gridView.FocusedColumn.FieldName)
                { 
                    case "L2":
                        
                        
                            FrmBalance.FLAG = vol.UID;
                            FrmBalance.TYPE = "1";

                            FrmBalance.DY = type;
                            FrmBalance.ADdRight = AddRight;
                            FrmBalance.EDitRight = EditRight;
                            FrmBalance.DEleteRight = DeleteRight;
                            FrmBalance.PRintRight = PrintRight;
                         break;
                     case "L10":
                         FrmBalance.FLAG = vol.UID;
                         FrmBalance.TYPE = "2";
                         FrmBalance.DY = type;
                         FrmBalance.ADdRight = AddRight;
                         FrmBalance.EDitRight = EditRight;
                         FrmBalance.DEleteRight = DeleteRight;
                         FrmBalance.PRintRight = PrintRight;
                         break;
                    case"L11":
                        FrmBalance.FLAG = vol.UID;
                        FrmBalance.TYPE = "3";
                        FrmBalance.DY = type;
                        FrmBalance.ADdRight = AddRight;
                        FrmBalance.EDitRight = EditRight;
                        FrmBalance.DEleteRight = DeleteRight;
                        FrmBalance.PRintRight = PrintRight;
                        break;
                    default:
                        break;
                }
                if (FrmBalance.FLAG != "")
                {
                    FrmBalance.ShowDialog();
                    objCopy.GetType().GetProperty(gridView.FocusedColumn.FieldName).SetValue(objCopy, FrmBalance.SUM, null);
                    
                }
            }
           
			//�ø������½������
			DataConverter.CopyTo<PSP_VolumeBalance>(objCopy, obj);
            
            if (isresum)
            {
                
                PSP_VolumeBalance ob = new PSP_VolumeBalance();
                ob.TypeID = type;
                ob.Flag = flag;
                IList<PSP_VolumeBalance> list0 = Services.BaseService.GetList<PSP_VolumeBalance>("SelectPSP_VolumeBalanceByTypeID", ob);
                if (list0 != null)
                    if (list0.Count > 0)
                        ob = list0[0];
                if(ob.UID!=obj.UID)
                    Services.BaseService.Update("UpdatePSP_VolumeBalance2", obj);
                reloadsum(ob);
            }
            else
                reloadsum(obj);
			//ˢ�±��
            gridControl.RefreshDataSource();
            
		}

		/// <summary>
		/// ɾ���������
		/// </summary>
		public void DeleteObject()
		{
			//��ȡ�������
			PSP_VolumeBalance obj = FocusedObject;
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
				Services.BaseService.Delete<PSP_VolumeBalance>(obj);
                
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
            if (ObjectList.Count == 0 || EnsureBaseYear() == obj.Year)
            {
                
                PSP_VolumeBalanceDataSource BaseYearrate = new PSP_VolumeBalanceDataSource();
                BaseYearrate.UID = baseyeartype;
                BaseYearrate.Value = 1990;
                BaseYearrate.TypeID = Convert.ToInt32(obj.TypeID);
                BaseYearrate.Flag = flag;
                Services.BaseService.Update("UpdatePSP_VolumeBalanceDataSource2", BaseYearrate);
            }
		}
		#endregion
        /// <summary>
        /// ɾ���������
        /// </summary>
        /// 
        #region ��Ӹ�����
        private void AddFuHe(PSP_VolumeBalance obj)
        {
            PSP_VolumeBalance pvc = new PSP_VolumeBalance();
            pvc.TypeID = type;
            pvc.Flag = flag;
            pvc.Year = EnsureBaseYear();
            IList<PSP_VolumeBalance> list = Services.BaseService.GetList<PSP_VolumeBalance>("SelectPSP_VolumeBalanceByTypeIDAndYear", pvc);
            if (list != null)
                if (list.Count == 1)
                {
                    PSP_VolumeBalance_Calc pvc2 = new PSP_VolumeBalance_Calc();
                    pvc2.Type ="1";
                    pvc2.Flag = list[0].UID;
                    IList<PSP_VolumeBalance_Calc> list2 = Services.BaseService.GetList<PSP_VolumeBalance_Calc>("SelectPSP_VolumeBalance_CalcByTypeFlag", pvc2);
                    PSP_VolumeBalance_Calc pvc3 = new PSP_VolumeBalance_Calc();
                    pvc3.Type = "1";
                    pvc3.Flag = obj.UID;
                    IList<PSP_VolumeBalance_Calc> list3 = Services.BaseService.GetList<PSP_VolumeBalance_Calc>("SelectPSP_VolumeBalance_CalcByTypeFlag", pvc3);
                    if (list2 != null)
                        if (list2.Count > 0)
                        {
                           
                            foreach (PSP_VolumeBalance_Calc pvctemp in list2)
                            {
                                if (list3 != null)
                                    if (list3.Count == list2.Count)
                                        break;
                                PSP_VolumeBalance_Calc _obj = new PSP_VolumeBalance_Calc();
                                _obj = pvctemp;
                                _obj.Flag = obj.UID;
                                _obj.Vol = 0;
                                _obj.UID = Guid.NewGuid().ToString();


                                Services.BaseService.Create<PSP_VolumeBalance_Calc>(_obj);
                            }
                        }
                }
        
        }
        #endregion
        #region ��ͷ���
        public void reloadsum(PSP_VolumeBalance objtemp)
        {

            double factor = 1;
            if (type == "35")
                if (loadrate != "")
                    factor = Convert.ToDouble(loadrate);
                if (objtemp.S1 == "1")
                {

                    objtemp.L5 =Math.Round(objtemp.L1 + objtemp.L3 - objtemp.L2 - objtemp.L4,2);
                    if (objtemp.L5 != 0)
                        objtemp.L7 = Math.Round(objtemp.L6 /(objtemp.L5*factor),2);
                    else
                        objtemp.L7 = 0;
                        objtemp.L8 = Math.Round((objtemp.L5 * factor) * Convert.ToDouble(Volumecalc), 2);
                    objtemp.L9 = Math.Round(objtemp.L6 - objtemp.L8,2);
                    objtemp.L12 = Math.Round(objtemp.L6 + objtemp.L10 + objtemp.L11,2);
                    if (objtemp.L5 != 0)
                        objtemp.L13 = Math.Round(objtemp.L12 / (objtemp.L5 * factor), 2);
                    else
                        objtemp.L13 = 0;
                    Services.BaseService.Update("UpdatePSP_VolumeBalance2", objtemp);
                    if (EnsureBaseYear() <=objtemp.Year)
                    sum(objtemp);
                }
                else
                {
                    PSP_VolumeBalance volumtemp = new PSP_VolumeBalance();
                    volumtemp.TypeID = type;
                    volumtemp.Flag = flag;
                    volumtemp.Year = objtemp.Year;
                    IList<PSP_VolumeBalance> list = Services.BaseService.GetList<PSP_VolumeBalance>("SelectPSP_VolumeBalanceByTypeIDLessYear", volumtemp);
                    if (list.Count > 0)
                    {
                        objtemp.L6 = Math.Round(list[list.Count - 1].L12,2);
                    }
                    else
                        objtemp.L6 = 0;
                    objtemp.L5 = Math.Round(objtemp.L1 + objtemp.L3 - objtemp.L2 - objtemp.L4,2);
                    if (objtemp.L5 != 0)
                        objtemp.L7 = Math.Round(objtemp.L6 / (objtemp.L5 * factor), 2);
                    else
                        objtemp.L7 = 0;
                    objtemp.L8 = Math.Round((objtemp.L5 * factor) * Convert.ToDouble(Volumecalc), 2);
                    objtemp.L9 = Math.Round(objtemp.L6 - objtemp.L8,2);
                    objtemp.L12 = Math.Round(objtemp.L6 + objtemp.L10 + objtemp.L11,2);
                    if (objtemp.L5 != 0)
                        objtemp.L13 = Math.Round(objtemp.L12 / (objtemp.L5 * factor), 2);
                    else
                        objtemp.L13= 0;
                    Services.BaseService.Update("UpdatePSP_VolumeBalance2", objtemp);
                    if (EnsureBaseYear() <= objtemp.Year)
                    sum(objtemp);
                }
                RefreshData();
            
        }
        private void sum(PSP_VolumeBalance objtemp)
        {
            //PSP_VolumeBalance volumtemp = new PSP_VolumeBalance();
            //volumtemp.TypeID =type;
            //volumtemp.Year = objtemp.Year;
            //IList<PSP_VolumeBalance> listtemp = Services.BaseService.GetList<PSP_VolumeBalance>("SelectPSP_VolumeBalanceByTypeIDAboveYear", volumtemp);
            double l6temp = objtemp.L12;
            double factor = 1;
            if (type == "35")
                if (loadrate != "")
                    factor = Convert.ToDouble(loadrate);
            PSP_VolumeBalance vol = new PSP_VolumeBalance();
            vol.Flag=flag;
            vol.TypeID=type;
            IList List = Services.BaseService.GetList("SelectPSP_VolumeBalanceByTypeID", vol);
            DataTable da = DataConverter.ToDataTable(List, typeof(PSP_VolumeBalance)); 
            DataRow[] dr = da.Select("Year>"+objtemp.Year);
            foreach (DataRow drtemp  in dr)
            {
                PSP_VolumeBalance psp = new PSP_VolumeBalance();
                foreach (DataColumn dm in da.Columns)
                {
                    psp.GetType().GetProperty(dm.ColumnName).SetValue(psp, drtemp[dm.ColumnName], null);
                }
               
                //psp = psptemp;
                psp.L6 = Math.Round(l6temp,2);
                psp.L5 = Math.Round(psp.L1 + psp.L3 - psp.L2 - psp.L4,2);
                if (psp.L5 != 0)
                    psp.L7 = Math.Round(psp.L6 / (psp.L5 * factor), 2);
                else
                    psp.L7 = 0;
                psp.L8 = Math.Round((psp.L5 * factor) * Convert.ToDouble(Volumecalc), 2);
                psp.L9 = Math.Round(psp.L6 - psp.L8,2);
                psp.L12 = Math.Round(psp.L6 + psp.L10 + psp.L11,2);
                if (psp.L5 != 0)
                    psp.L13 = Math.Round(psp.L12 / (psp.L5 * factor), 2);
                else
                    psp.L13 = 0;
                l6temp = Math.Round(psp.L12,2);
                Services.BaseService.Update("UpdatePSP_VolumeBalance2", psp);
                
                //foreach (DataColumn dm in da.Columns)
                //{
                //    drtemp[dm.ColumnName]= psp.GetType().GetProperty(dm.ColumnName).GetValue(psp, null);
                //}
            }




           
        }
        #endregion
        #region ȷ����׼��
        public int EnsureBaseYear()
        {
            PSP_VolumeBalanceDataSource BaseYeartemp = new PSP_VolumeBalanceDataSource();
            BaseYeartemp.TypeID =Convert.ToInt32(type);
            BaseYeartemp.Flag = flag;
            BaseYeartemp.UID = baseyeartype;
            PSP_VolumeBalanceDataSource BaseYearrate = (PSP_VolumeBalanceDataSource)Common.Services.BaseService.GetObject("SelectPSP_VolumeBalanceDataSourceByKeyTypeID", BaseYeartemp);
            if (BaseYearrate == null)
            {
                BaseYearrate = new PSP_VolumeBalanceDataSource();
                BaseYearrate.UID= baseyeartype;
                BaseYearrate.Value = 1900;
                BaseYearrate.Flag = flag;
                BaseYearrate.TypeID = Convert.ToInt32(type);
                Services.BaseService.Create<PSP_VolumeBalanceDataSource>(BaseYearrate);
                //
            }
            return Convert.ToInt32(BaseYearrate.Value);

        }
        #endregion

        private void gridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

            Brush brush = null;
            Rectangle r = e.Bounds;
            double[] max1 = new double[2] { 1.8, 2.1 };
            double[] max2 = new double[]{1.7, 2.0};
            double[] max3 = new double[] { 1.9, 2.2 };
            double[] max = new double[]{0, 0};
            Color c1 = Color.Red;
            Color c2 = Color.DarkGreen;
            Color c3 = Color.FromArgb(200,200,200);
            if (gridView.FocusedRowHandle == e.RowHandle)
            {
                brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, c3, c3, 180);
                if (brush != null)
                {
                    e.Graphics.FillRectangle(brush, r);
                }
            }
            if (e.Column.FieldName.Contains("L13"))
            {
                if (type == "110")
                {
                    //int i=0;
                    //if(e.CellValue!=null&&e.CellValue!=DBNull.Value)
                    //{  
                    //    i=Convert.ToInt32(e.CellValue);
                    max = max1;

                }
                else
                    if (type == "220")
                    {
                        //brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, c3, c4, 180);
                        //if (brush != null)
                        //{
                        //    e.Graphics.FillRectangle(brush, r);
                        max = max2;

                    }
                    else
                    {
                        max = max3;

                    
                    }
                double i = 0;
                if (e.CellValue != null && e.CellValue != DBNull.Value)
                {
                    i = Convert.ToDouble(e.CellValue);
                    if (i < max[0])
                    {
                        //c1 = Color.Blue;
                        //c2 = Color.Blue;
                        e.Appearance.ForeColor = Color.Blue;
                    }
                    //else
                    //    if (i >= max[0] &&  i <= max[1])
                    //    {
                    //        c1 = Color.White;
                    //        c2 = Color.White;

                    //    }
                    else if (i > max[1])
                    {
                        //c1 = Color.Red;
                        //c2 = Color.Red;
                        e.Appearance.ForeColor = Color.Red;
                    }
                    //if (i < max[0] || i > max[1])
                    //{
                    //brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, c3, c3, 180);
                    //if (brush != null)
                    //{
                    //    e.Graphics.FillRectangle(brush, r);
                    //}
                    //}
                }



            }
            else if (e.Column.FieldName.Contains("Year"))
            { 
           if(e.CellValue!=null)
               if(e.CellValue!=DBNull.Value)
                   if (Convert.ToInt32(e.CellValue) == EnsureBaseYear())
                   {
                       e.Appearance.ForeColor = Color.OrangeRed;
                   }
                   
                   
            }
        }

     
    }
}
