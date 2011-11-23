
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
    public partial class CtrlPSP_EachList : UserControl
    {

        #region ���췽��
        public CtrlPSP_EachList()
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
        public bool bl = true;

        bool isjsxm = false;
        public bool IsJSXM
        {
            get { return isjsxm; }
            set { isjsxm = value; }
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
        public IList<PSP_EachList> ObjectList
        {
            get { return this.gridControl.DataSource as IList<PSP_EachList>; }
        }

        /// <summary>
        /// ��ȡ������󣬼�FocusedRow
        /// </summary>
        public PSP_EachList FocusedObject
        {
            get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PSP_EachList; }
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
        public bool RefreshData(string type)
        {
            try
            {
                 PSP_EachList pe=new PSP_EachList();
                pe.Types=type;
    

                IList<PSP_EachList> list = Services.BaseService.GetList<PSP_EachList>("SelectPSP_EachListByTypes", pe);
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
        /// 
        public void AddObjecta(string type, bool bl)
        {
            //�����������Ƿ��Ѿ�����
            if (ObjectList == null)
            {
                return;
            }
            //�½�����
            PSP_EachList obj = new PSP_EachList();
            obj.Types = type;
            obj.CreateDate = DateTime.Now;

            //ִ����Ӳ���
            using (FrmPSP_EachListDialog dlg = new FrmPSP_EachListDialog())
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
            PSP_EachList obj = FocusedObject;
            if (obj == null)
            {
                return;
            }

            //���������һ������
            PSP_EachList objCopy = new PSP_EachList();
            DataConverter.CopyTo<PSP_EachList>(obj, objCopy);

            //ִ���޸Ĳ���
            using (FrmPSP_EachListDialog dlg = new FrmPSP_EachListDialog())
            {
                dlg.Object = objCopy;   //�󶨸���
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            //�ø������½������
            DataConverter.CopyTo<PSP_EachList>(objCopy, obj);
            //ˢ�±��
            gridControl.RefreshDataSource();
        }

        /// <summary>
        /// ɾ���������
        /// </summary>
        public void DeleteObject(string lb)
        {
            //��ȡ�������
            PSP_EachList obj = FocusedObject;
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
                    pt.Flag2 = obj.ID.ToString();
                    IList<PowerTypes> li = Services.BaseService.GetList<PowerTypes>("SelectPowerTypesByFlag2", pt);
                    coun = li.Count;
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
                Services.BaseService.Delete<PSP_EachList>(obj);

                if (lb == "sb")
                {
                    Services.BaseService.Update("DeletePowerPicSelect1", obj.ID);
                    Services.BaseService.Update("DeletePowerProTypesInFlag2", obj.ID);
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
        #endregion
    }
}
