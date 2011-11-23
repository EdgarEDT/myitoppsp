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
namespace Itop.TLPsp.Graphical
{
    public partial class CtrlDlqicheck : UserControl
    {
        private PSPDEV pspDEV=new PSPDEV();
        public CtrlDlqicheck()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 获取GridControl对象
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
        /// 获取GridControl的数据源，即对象的列表
        /// </summary>
        
        public IList<PSPDEV> ObjectList
        {
            get { return this.gridControl.DataSource as IList<PSPDEV>; }
        }

        /// <summary>
        /// 获取焦点对象，即FocusedRow
        /// </summary>
        public PSPDEV FocusedObject
        {
            get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PSPDEV; }
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        public void PrintPreview()
        {
            ComponentPrint.ShowPreview(this.gridControl, this.gridView.GroupPanelText);
        }

        /// <summary>
        /// 刷新表格中的数据
        /// </summary>
        /// <returns>ture:成功  false:失败</returns>
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
        /// 刷新表格中的数据
        /// </summary>
        /// <returns>ture:成功  false:失败</returns>
        public bool RefreshData1()
        {
            try
            {
                string con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + duanluqi.SvgUID + "'AND PSPDEV.type='"+duanluqi.Type+"'order by PSPDEV.number";
                IList list = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
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
        /// 添加对象
        /// </summary>
        public void AddObject()
        {

        }

        /// <summary>
        /// 修改焦点对象
        /// </summary>
        public void UpdateObject()
        {
            //获取焦点对象

        }

        /// <summary>
        /// 删除焦点对象
        /// </summary>
        public void DeleteObject()
        {
            //获取焦点对象

        }

    }
}
