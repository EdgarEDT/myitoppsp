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
using Itop.Domain.Stutistic;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using Itop.Domain.Graphics;
using System.IO;
namespace Itop.TLPsp
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
        /// ˢ�±���е�����
        /// </summary>
        /// <returns>ture:�ɹ�  false:ʧ��</returns>
        public bool RefreshData()
        {
            try
            {
                IList<PSPDEV> list = Services.BaseService.GetStrongList<PSPDEV>();

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
                //string filepath = "";
                IList list = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", duanluqi);
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
        /// ��Ӷ���
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
