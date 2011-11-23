using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using Itop.Client.Base;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Itop.Common;

namespace ItopVector.Tools
{
    public partial class frmSubstationMng : FormModuleBase
    {
        protected bool _bAllowUpdate = true;
        public frmSubstationMng()
        {
            InitializeComponent();
        }

        private void frmSubstationParMng_Load(object sender, EventArgs e)
        {
            barQuery.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            loadData();
        }

        public void loadData()
        {
            try
            {
                PSP_SubstationMng sub_par = new PSP_SubstationMng();
               // sub_par.type = par;
                IList<PSP_SubstationMng> list = Services.BaseService.GetList<PSP_SubstationMng>("SelectPSP_SubstationMngList", sub_par);
                this.gridControl.DataSource = list;
            }
            catch (Exception exc)
            {
                //Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
            }
        }
        protected override void Add()
        {
            //检查对象链表是否已经加载
            if (ObjectList == null)
            {
                return;
            }
            //新建对象
            PSP_SubstationMng obj = new PSP_SubstationMng();
           // obj.type = par;
            //执行添加操作
            using (FrmPSP_SubstationMngDialog dlg = new FrmPSP_SubstationMngDialog())
            {
                dlg.IsCreate = true;    //设置新建标志
                dlg.Object = obj;
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            //将新对象加入到链表中
            ObjectList.Add(obj);

            //刷新表格，并将焦点行定位到新对象上。
            gridControl.RefreshDataSource();
            GridHelper.FocuseRow(this.gridView, obj);
        }
        protected override void Edit()
        {
            //获取焦点对象
            PSP_SubstationMng obj = FocusedObject;
            if (obj == null)
            {
                return;
            }

            //创建对象的一个副本
            PSP_SubstationMng objCopy = new PSP_SubstationMng();
            DataConverter.CopyTo<PSP_SubstationMng>(obj, objCopy);

            //执行修改操作
            using (FrmPSP_SubstationMngDialog dlg = new FrmPSP_SubstationMngDialog())
            {
                dlg.Object = objCopy;   //绑定副本
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            //用副本更新焦点对象
            DataConverter.CopyTo<PSP_SubstationMng>(objCopy, obj);
            //刷新表格
            gridControl.RefreshDataSource();
        }
        protected override void Del()
        {
            //获取焦点对象
            PSP_SubstationMng obj = FocusedObject;
            if (obj == null)
            {
                return;
            }

            //请求确认
            if (MsgBox.ShowYesNo(Strings.SubmitDelete) != DialogResult.Yes)
            {
                return;
            }

            //执行删除操作
            try
            {
                Services.BaseService.Delete<PSP_SubstationMng>(obj);
            }
            catch (Exception exc)
            {
               // Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return;
            }

            this.gridView.BeginUpdate();
            //记住当前焦点行索引
            int iOldHandle = this.gridView.FocusedRowHandle;
            //从链表中删除
            ObjectList.Remove(obj);
            //刷新表格
            gridControl.RefreshDataSource();
            //设置新的焦点行索引
            GridHelper.FocuseRowAfterDelete(this.gridView, iOldHandle);
            this.gridView.EndUpdate();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           
        }
        #region 公共属性
        /// <summary>
        /// 获取或设置"双击允许修改"标志
        /// </summary>
        public bool AllowUpdate
        {
            get { return _bAllowUpdate; }
            set { _bAllowUpdate = value; }
        }

        /// <summary>
        /// 获取GridControl对象
        /// </summary>
        public GridControl GridControl
        {
            get { return gridControl; }
        }

        /// <summary>
        /// 获取GridView对象
        /// </summary>
        public GridView GridView
        {
            get { return gridView; }
        }

        /// <summary>
        /// 获取GridControl的数据源，即对象的列表
        /// </summary>
        public IList<PSP_SubstationMng> ObjectList
        {
            get { return this.gridControl.DataSource as IList<PSP_SubstationMng>; }
        }

        /// <summary>
        /// 获取焦点对象，即FocusedRow
        /// </summary>
        public PSP_SubstationMng FocusedObject
        {
            get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PSP_SubstationMng; }
        }
        #endregion

        #region 事件处理
        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            // 判断"双击允许修改"标志 
            if (!AllowUpdate)
            {
                return;
            }

            //如果鼠标点击在单元格中，则编辑焦点对象。
            Point point = this.gridControl.PointToClient(Control.MousePosition);
            if (GridHelper.HitCell(this.gridView, point))
            {
                Edit();
            }

        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 打印预览
        /// </summary>
        public void PrintPreview()
        {
            ComponentPrint.ShowPreview(this.gridControl, this.gridView.GroupPanelText);
        }

        #endregion
    }
}