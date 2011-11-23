
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
using Itop.Domain.Graphics;
#endregion

namespace Itop.DLGH
{
    public partial class CtrlbdzType : UserControl
	{
		#region 构造方法
        public CtrlbdzType()
		{
			InitializeComponent();
		}
		#endregion

		#region 字段
		protected bool _bAllowUpdate = true;
        private string typeid = "";

      
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
        public string Typeid
        {
            get { return typeid; }
            set { typeid = value; }
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
        public IList<PSP_bdz_type> ObjectList
		{
			get { return this.gridControl.DataSource as IList<PSP_bdz_type>; }
		}

		/// <summary>
		/// 获取焦点对象，即FocusedRow
		/// </summary>
        public PSP_bdz_type FocusedObject
		{
            get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PSP_bdz_type; }
		}
		#endregion

		#region 事件处理
		private void gridView_DoubleClick(object sender, EventArgs e)
		{
			// 判断"双击允许修改"标志 
            //if (!AllowUpdate)
            //{
            //    return;
            //}

            ////如果鼠标点击在单元格中，则编辑焦点对象。
            //Point point = this.gridControl.PointToClient(Control.MousePosition);
            //if (GridHelper.HitCell(this.gridView, point))
            //{
            //    UpdateObject();
            //}

		}
		#endregion

		#region 公共方法
		/// <summary>
		/// 打印预览
		/// </summary>
		public void PrintPreview()
		{
			ComponentPrint.ShowPreview(this.gridControl, "导入分类维护");
		}

		/// <summary>
		/// 刷新表格中的数据
		/// </summary>
		/// <returns>ture:成功  false:失败</returns>
		public bool RefreshData()
		{
			try
			{
                PSP_bdz_type p=new PSP_bdz_type();
                p.col1=" col3='"+typeid+"' order by Name";
                IList<PSP_bdz_type> list = Services.BaseService.GetList<PSP_bdz_type>("SelectPSP_bdz_typeByWhere", p);
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
			//检查对象链表是否已经加载
			if (ObjectList == null)
			{
				return;
			}
			//新建对象
            PSP_bdz_type obj = new PSP_bdz_type();
            
			//执行添加操作
            using (FrmbdzTypeDialog dlg = new FrmbdzTypeDialog())
			{
				dlg.IsCreate = true;    //设置新建标志
                dlg.Typeid = typeid;
				dlg.Object = obj;
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
            ObjectList.Add(obj);
            //刷新表格，并将焦点行定位到新对象上。
            gridControl.RefreshDataSource();
            GridHelper.FocuseRow(this.gridView, obj);
            //RefreshData();
            //GridHelper.FocuseRow(this.gridView, obj);
            //gridView.FocusedRowHandle = gridView.RowCount;
		}

		/// <summary>
		/// 修改焦点对象
		/// </summary>
		public void UpdateObject()
		{
			//获取焦点对象
            PSP_bdz_type obj = FocusedObject;
			if (obj == null)
			{
				return;
			}
            int i=gridView.FocusedRowHandle;
			//创建对象的一个副本
            //LineType objCopy = new LineType();
            //DataConverter.CopyTo<LineType>(obj, objCopy);

			//执行修改操作
            using (FrmbdzTypeDialog dlg = new FrmbdzTypeDialog())
			{
                dlg.Object = obj;   //绑定副本
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
            //RefreshData();
            gridControl.RefreshDataSource();
            gridView.FocusedRowHandle = i;
		}

		/// <summary>
		/// 删除焦点对象
		/// </summary>
		public void DeleteObject()
		{
			//获取焦点对象
            PSP_bdz_type obj = FocusedObject;
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
                Services.BaseService.Delete<PSP_bdz_type>(obj);
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
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
		#endregion
	}
}
