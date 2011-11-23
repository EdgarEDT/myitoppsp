
#region 引用命名空间
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

        #region 构造方法
        public CtrlPSP_EachList()
        {
            InitializeComponent();
        }
        #endregion

        #region 字段
        protected bool _bAllowUpdate = true;
        #endregion

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
        public bool bl = true;

        bool isjsxm = false;
        public bool IsJSXM
        {
            get { return isjsxm; }
            set { isjsxm = value; }
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
        public IList<PSP_EachList> ObjectList
        {
            get { return this.gridControl.DataSource as IList<PSP_EachList>; }
        }

        /// <summary>
        /// 获取焦点对象，即FocusedRow
        /// </summary>
        public PSP_EachList FocusedObject
        {
            get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PSP_EachList; }
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
                UpdateObject();
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

        /// <summary>
        /// 刷新表格中的数据
        /// </summary>
        /// <returns>ture:成功  false:失败</returns>
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
        /// 添加对象
        /// </summary>
        /// 
        public void AddObjecta(string type, bool bl)
        {
            //检查对象链表是否已经加载
            if (ObjectList == null)
            {
                return;
            }
            //新建对象
            PSP_EachList obj = new PSP_EachList();
            obj.Types = type;
            obj.CreateDate = DateTime.Now;

            //执行添加操作
            using (FrmPSP_EachListDialog dlg = new FrmPSP_EachListDialog())
            {
                dlg.IsCreate = true;    //设置新建标志
                dlg.Object = obj;
                dlg.IsPower = true;
                dlg.IsJSXM = isjsxm;
                dlg.bl = bl;
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

        /// <summary>
        /// 修改焦点对象
        /// </summary>
        public void UpdateObject()
        {
            //获取焦点对象
            PSP_EachList obj = FocusedObject;
            if (obj == null)
            {
                return;
            }

            //创建对象的一个副本
            PSP_EachList objCopy = new PSP_EachList();
            DataConverter.CopyTo<PSP_EachList>(obj, objCopy);

            //执行修改操作
            using (FrmPSP_EachListDialog dlg = new FrmPSP_EachListDialog())
            {
                dlg.Object = objCopy;   //绑定副本
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            //用副本更新焦点对象
            DataConverter.CopyTo<PSP_EachList>(objCopy, obj);
            //刷新表格
            gridControl.RefreshDataSource();
        }

        /// <summary>
        /// 删除焦点对象
        /// </summary>
        public void DeleteObject(string lb)
        {
            //获取焦点对象
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
                MsgBox.Show("该分类下面有记录，无法删除");
                bl = false;
                return;
            }


            //请求确认
            if (MsgBox.ShowYesNo(Strings.SubmitDelete) != DialogResult.Yes)
            {
                bl = false;
                return;
            }

            //执行删除操作
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
        #endregion
    }
}
