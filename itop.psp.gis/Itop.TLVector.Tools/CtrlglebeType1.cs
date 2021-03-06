
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

namespace ItopVector.Tools
{
	public partial class CtrlglebeType1 : UserControl
	{
		#region 构造方法
		public CtrlglebeType1()
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
		public IList<glebeType> ObjectList
		{
			get { return this.gridControl.DataSource as IList<glebeType>; }
		}

		/// <summary>
		/// 获取焦点对象，即FocusedRow
		/// </summary>
		public glebeType FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as glebeType; }
		}
		#endregion

		#region 事件处理
		private void gridView_DoubleClick(object sender, EventArgs e)
		{
			// 判断"双击允许修改"标志 
		

		}
		#endregion

		#region 公共方法
		/// <summary>
		/// 打印预览
		/// </summary>
		public void PrintPreview()
		{
            ComponentPrint.ShowPreview(this.gridControl, "地块负荷密度维护");
		}

		/// <summary>
		/// 刷新表格中的数据
		/// </summary>
		/// <returns>ture:成功  false:失败</returns>
		public bool RefreshData()
		{
			try
			{
				IList<glebeType> list = Services.BaseService.GetStrongList<glebeType>();
                 for (int i = 0; i < list.Count;i++ )
                {
                    list[i].ObjColor=Color.FromArgb(Convert.ToInt32(list[i].ObligateField1));
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
		#endregion
	}
}
