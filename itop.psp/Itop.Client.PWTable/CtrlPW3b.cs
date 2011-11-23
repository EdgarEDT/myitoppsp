
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
using Itop.Domain.PWTable;
#endregion

namespace Itop.Client.PWTable
{
	public partial class CtrlPW3b : UserControl
	{
		#region 构造方法
		public CtrlPW3b()
		{
			InitializeComponent();
		}
		#endregion

		#region 字段
		protected bool _bAllowUpdate = true;
        public bool editright = true;
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

        string type = "";
        /// <summary>
        /// 获取GridControl对象
        /// </summary>
        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                
            }
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
        public IList<PW_tb3b> ObjectList
		{
            get { return this.gridControl.DataSource as IList<PW_tb3b>; }
		}

		/// <summary>
		/// 获取焦点对象，即FocusedRow
		/// </summary>
        public PW_tb3b FocusedObject
		{
            get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PW_tb3b; }
		}
		#endregion

		#region 事件处理
		private void gridView_DoubleClick(object sender, EventArgs e)
        {
            if (!editright)
                return;
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
		public bool RefreshData()
		{
			try
			{
                              
                this.gridView.GroupPanelText = "线路导线截面详情表";
                PW_tb3b p = new PW_tb3b();
                p.col7 = type;
                IList<PW_tb3b> list = Services.BaseService.GetList<PW_tb3b>("SelectPW_tb3bListbyCol7", p);
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
            PW_tb3b obj = new PW_tb3b();
            obj.UID = Guid.NewGuid().ToString();
            obj.col7 = type;

			//执行添加操作
			using (FrmPW3bDialog dlg = new FrmPW3bDialog())
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

		/// <summary>
		/// 修改焦点对象
		/// </summary>
		public void UpdateObject()
		{
			//获取焦点对象
            PW_tb3b obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//创建对象的一个副本
            PW_tb3b objCopy = new PW_tb3b();
            DataConverter.CopyTo<PW_tb3b>(obj, objCopy);

			//执行修改操作
			using (FrmPW3bDialog dlg = new FrmPW3bDialog())
			{
				dlg.Object = objCopy;   //绑定副本
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//用副本更新焦点对象
            DataConverter.CopyTo<PW_tb3b>(objCopy, obj);
			//刷新表格
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// 删除焦点对象
		/// </summary>
		public void DeleteObject()
		{
			//获取焦点对象
            PW_tb3b obj = FocusedObject;
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
                Services.BaseService.Delete<PW_tb3b>(obj);
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
