
#region 引用命名空间
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Collections;
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
	public partial class CtrlSubstationBurthen : UserControl
	{
		#region 构造方法
        public CtrlSubstationBurthen()
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
		public IList<glebeProperty> ObjectList
		{
			get { return this.gridControl.DataSource as IList<glebeProperty>; }
		}

		/// <summary>
		/// 获取焦点对象，即FocusedRow
		/// </summary>
		public glebeProperty FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as glebeProperty; }
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
                //UpdateObject();
			}

		}
		#endregion

		#region 公共方法
		/// <summary>
		/// 打印预览
		/// </summary>
		public void PrintPreview()
		{
			ComponentPrint.ShowPreview(this.gridControl, gridControl.Text);
            
		}

		/// <summary>
		/// 刷新表格中的数据
		/// </summary>
		/// <returns>ture:成功  false:失败</returns>
		public bool InitData(string svgUid,string sid)
		{
            try
            {
                LineInfo line = new LineInfo();
                line.SvgUID = svgUid;
                line.LayerID = sid;
                DataTable dt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectLineInfoBySvgUIDAndLayer", line), typeof(LineInfo));
                gridControl.DataSource = dt;
                gridControl.Text = "线路列表";
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return false;
            }
            return true;
		}
        public bool InitData(string svgUid)
        {
            try
            {
                LineInfo line = new LineInfo();
                line.SvgUID = svgUid;
               // line.LayerID = sid;
                DataTable dt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectLineInfoBySvgUID", line), typeof(LineInfo));
                gridControl.DataSource = dt;
                gridControl.Text = "线路列表";
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return false;
            }
            return true;
        }
		#endregion
	}
}
