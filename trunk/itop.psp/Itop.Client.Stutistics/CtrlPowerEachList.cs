
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
using Itop.Domain.Stutistics;
#endregion

namespace Itop.Client.Stutistics
{
	public partial class CtrlPowerEachList : UserControl
	{
		#region ���췽��
		public CtrlPowerEachList()
		{
			InitializeComponent();
		}
		#endregion

		#region �ֶ�
		protected bool _bAllowUpdate = true;
        Form9_LangFang _langfang = null;
        public bool editright = true;
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



        public bool bl = true;



        bool isjsxm = false;
        public bool  IsJSXM
        {
            get { return isjsxm; }
            set { isjsxm =value; }
        }

        public Form9_LangFang LangFang
        {
            get { return _langfang ; }
            set { _langfang = value; }
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
		public IList<PowerEachList> ObjectList
		{
			get { return this.gridControl.DataSource as IList<PowerEachList>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
		public PowerEachList FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PowerEachList; }
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
                if(LangFang!=null)
                  LangFang.InitSodata();
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
		public bool RefreshData(string type)
		{
			try
			{
				//IList<PowerEachList> list = Services.BaseService.GetStrongList<PowerEachList>();
                PowerEachList pe=new PowerEachList();
                pe.Types=type;
                //IList<PowerEachList> list = Services.BaseService.GetStrongList<PowerEachList>(pe);
                IList<PowerEachList> list = Services.BaseService.GetList<PowerEachList>("SelectPowerEachListList", pe);
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
		public void AddObject(string type)
		{
			//�����������Ƿ��Ѿ�����
			if (ObjectList == null)
			{
				return;
			}
			//�½�����
			PowerEachList obj = new PowerEachList();
            string uid = Guid.NewGuid().ToString();
            obj.UID = uid;
            obj.Types = type;

			//ִ����Ӳ���
			using (FrmPowerEachListDialog dlg = new FrmPowerEachListDialog())
			{
                dlg.IsJSXM = isjsxm;
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



        public void AddObjecta(string type)
        {
            //�����������Ƿ��Ѿ�����
            if (ObjectList == null)
            {
                return;
            }
            //�½�����
            PowerEachList obj = new PowerEachList();
            obj.Types = type;

            //ִ����Ӳ���
            using (FrmPowerEachListDialog dlg = new FrmPowerEachListDialog())
            {
                dlg.IsCreate = true;    //�����½���־
                dlg.Object = obj;
                dlg.IsPower = true;
                dlg.IsJSXM = isjsxm;
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

        public void AddObjecta(string type,bool bl)
        {
            //�����������Ƿ��Ѿ�����
            if (ObjectList == null)
            {
                return;
            }
            //�½�����
            PowerEachList obj = new PowerEachList();
            obj.Types = type;

            //ִ����Ӳ���
            using (FrmPowerEachListDialog dlg = new FrmPowerEachListDialog())
            {
                dlg.IsCreate = true;    //�����½���־
                dlg.Object = obj;
                dlg.IsPower = true;
                dlg.IsJSXM = isjsxm;
                dlg.bl = bl;
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            //���¶�����뵽������
            ObjectList.Add(obj);

           // ObjectList.Insert(0, obj);

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
			PowerEachList obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//���������һ������
			PowerEachList objCopy = new PowerEachList();
			DataConverter.CopyTo<PowerEachList>(obj, objCopy);

			//ִ���޸Ĳ���
			using (FrmPowerEachListDialog dlg = new FrmPowerEachListDialog())
			{
				dlg.Object = objCopy;   //�󶨸���
                dlg.IsJSXM = isjsxm;
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//�ø������½������
			DataConverter.CopyTo<PowerEachList>(objCopy, obj);
			//ˢ�±��
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// ɾ���������
		/// </summary>
		public void DeleteObject(string lb)
		{
			//��ȡ�������
			PowerEachList obj = FocusedObject;
			if (obj == null)
			{
                bl = false;
				return;
			}

            

            int coun = 0;

            switch (lb)
            {
                case "xuqiu":
                PowerTypes pt = new PowerTypes();
                pt.Flag2 = obj.UID;
                IList<PowerTypes> li = Services.BaseService.GetList<PowerTypes>("SelectPowerTypesByFlag2", pt);
                coun = li.Count;

                    break;
                case "xihua":
                    PowerProjectTypes pt1 = new PowerProjectTypes();
                    pt1.Flag2 = obj.UID;
                    IList<PowerProjectTypes> li1 = Services.BaseService.GetList<PowerProjectTypes>("SelectPowerProjectTypesByFlag2", pt1);
                    coun = li1.Count;
                    break;
                case "shebei":
                    PowerStuffTypes pt2 = new PowerStuffTypes();
                    pt2.Flag2 = obj.UID;
                    IList<PowerStuffTypes> li2 = Services.BaseService.GetList<PowerStuffTypes>("SelectPowerStuffTypesByFlag2", pt2);
                    coun = li2.Count;
                    break;
                case "fanwei":
                    PowersTypes pt3 = new PowersTypes();
                    pt3.Flag2 = obj.UID;
                    IList<PowersTypes> li3 = Services.BaseService.GetList<PowersTypes>("SelectPowersTypesByFlag2", pt3);
                    coun = li3.Count;
                    break;
                case "sb":
                    PowerProTypes pt4 = new PowerProTypes();
                    pt4.Flag2 = obj.UID;
                    IList<PowerProTypes> li4 = Services.BaseService.GetList<PowerProTypes>("SelectPowerProTypesByFlag2INParent", pt4);
                    coun = li4.Count;
                    break;
                case "gusuan":
                    PSP_PowerProValues_LangFang pt5 = new PSP_PowerProValues_LangFang();
                    pt5.Flag2 = obj.UID;
                    IList<PSP_PowerProValues_LangFang> li5 = Services.BaseService.GetList<PSP_PowerProValues_LangFang>("SelectPSP_PowerProValues_LangFangByFlag2INParent", pt5);
                    coun = li5.Count;
                    break;
            }



            if (coun > 0)
            {
                MsgBox.Show("�÷��������м�¼���޷�ɾ��");
                bl = false;
                return;
            }


			//����ȷ��
			if (MsgBox.ShowYesNo(Strings.SubmitDelete) != DialogResult.Yes)
			{
                bl = false;
				return;
			}

			//ִ��ɾ������
			try
			{
				Services.BaseService.Delete<PowerEachList>(obj);

                if (lb == "sb")
                {
                    Services.BaseService.Update("DeletePowerPicSelect1", obj.UID);
                    Services.BaseService.Update("DeletePowerProTypesInFlag2", obj.UID);
                }
                if (lb == "gusuan")
                {
                    Services.BaseService.Update("DeletePowerPicSelect1", obj.UID);
                    Services.BaseService.Update("DeletePowerProTypesInFlag2", obj.UID);
                }
			}
			catch
			{
                //Debug.Fail(exc.Message);
                //HandleException.TryCatch(exc);
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













        public void DeleteObject()
        {
            //��ȡ�������
            PowerEachList obj = FocusedObject;
            if (obj == null)
            {
                bl = false;
                return;
            }



            int coun = 0;


            //ִ��ɾ������
            try
            {
                Services.BaseService.Delete<PowerEachList>(obj);
                Services.BaseService.Update("DeletePowerPicSelect1", obj.UID);
    
                
            }
            catch
            {
                //Debug.Fail(exc.Message);
                //HandleException.TryCatch(exc);
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
