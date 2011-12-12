using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Itop.Common;
using Itop.Client.Common;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using Itop.Domain.Graphics;
using System.IO;
namespace Itop.TLPSP.DEVICE
{
    public partial class CtrlDlqicheck : UserControl
    {
        private PSPDEV pspDEV=new PSPDEV();
        public CtrlDlqicheck()
        {
            InitializeComponent();
        }
        /// <summary>
        /// ��ȡGridControl����
        /// </summary>
        public GridControl GridControl
        {
            get { return gridControl; }
        }
        public PSPDEV duanluqi
        {
            get { return pspDEV; }
            set { pspDEV = value; }
        }
        /// <summary>
        /// ��ȡGridControl������Դ����������б�
        /// </summary>
        
        public IList<PSPDEV> ObjectList
        {
            get { return this.gridControl.DataSource as IList<PSPDEV>; }
        }

        /// <summary>
        /// ��ȡ������󣬼�FocusedRow
        /// </summary>
        public PSPDEV FocusedObject
        {
            get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PSPDEV; }
        }

        /// <summary>
        /// ��ӡԤ��
        /// </summary>
        public void PrintPreview()
        {
            ComponentPrint.ShowPreview(this.gridControl, this.gridView.GroupPanelText);
        }

        /// <summary>
        /// ˢ�±����е�����
        /// </summary>
        /// <returns>ture:�ɹ�  false:ʧ��</returns>
        public bool RefreshData()
        {
            try
            {
                IList<PSPDEV> list = UCDeviceBase.DataService.GetStrongList<PSPDEV>();

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
        /// ˢ�±����е�����
        /// </summary>
        /// <returns>ture:�ɹ�  false:ʧ��</returns>
        public bool RefreshData1()
        {
            try
            {
                string con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + duanluqi.SvgUID + "'AND PSPDEV.type='"+duanluqi.Type+"'order by PSPDEV.number";
                IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                string filepath = Path.GetTempPath() + "\\" + Path.GetFileName("duanluqi.xml");
                if (File.Exists(filepath))
                {
                    this.gridView.RestoreLayoutFromXml(filepath);
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
        /// ���Ӷ���
        /// </summary>
        public void AddObject()
        {

        }

        /// <summary>
        /// �޸Ľ������
        /// </summary>
        public void UpdateObject()
        {
            //��ȡ�������

        }

        /// <summary>
        /// ɾ���������
        /// </summary>
        public void DeleteObject()
        {
            //��ȡ�������

        }

    }
}