
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
using Itop.Domain.Table;
#endregion

namespace Itop.Client.Stutistics
{
	public partial class CtrlBurdenMonth : UserControl
	{
		#region 构造方法
		public CtrlBurdenMonth()
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
        /// 获取或设置"双击允许修改"标志
        /// </summary>
        /// 
        FrmBurdenMonth fbm = null;

        public FrmBurdenMonth FBM
        {
            get { return fbm; }
            set { fbm = value; }
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
		public IList<BurdenMonth> ObjectList
		{
			get { return this.gridControl.DataSource as IList<BurdenMonth>; }
		}

		/// <summary>
		/// 获取焦点对象，即FocusedRow
		/// </summary>
		public BurdenMonth FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as BurdenMonth; }
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
                fbm.UpdataChart();
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
            string pjt = " ProjectID='" + MIS.ProgUID + "'";
            IList<PS_Table_AreaWH> lt = Common.Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", pjt);
            repositoryItemLookUpEdit1.DataSource = lt;
			try
			{
				//IList<BurdenMonth> list = Services.BaseService.GetStrongList<BurdenMonth>();

                IList<BurdenMonth> list = Services.BaseService.GetList<BurdenMonth>("SelectBurdenMonthByWhere", " uid like '%" + Itop.Client.MIS.ProgUID + "%' order by BurdenYear ");
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
			BurdenMonth obj = new BurdenMonth();
            obj.UID = Guid.NewGuid().ToString() + "|" + Itop.Client.MIS.ProgUID;
			//执行添加操作
			using (FrmBurdenMonthDialog dlg = new FrmBurdenMonthDialog())
			{
                dlg.TitleName = this.gridView.GroupPanelText;
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
			BurdenMonth obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//创建对象的一个副本
			BurdenMonth objCopy = new BurdenMonth();
			DataConverter.CopyTo<BurdenMonth>(obj, objCopy);

			//执行修改操作
			using (FrmBurdenMonthDialog dlg = new FrmBurdenMonthDialog())
            {
                dlg.TitleName = this.gridView.GroupPanelText;
				dlg.Object = objCopy;   //绑定副本
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//用副本更新焦点对象
			DataConverter.CopyTo<BurdenMonth>(objCopy, obj);
			//刷新表格
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// 删除焦点对象
		/// </summary>
		public void DeleteObject()
		{
			//获取焦点对象
			BurdenMonth obj = FocusedObject;
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
				Services.BaseService.Delete<BurdenMonth>(obj);
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

        private void gridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

            Brush brush = null;
            Rectangle r = e.Bounds;
            int year = 0;
            Color c1 = Color.FromArgb(255, 121, 121);
            Color c2 = Color.FromArgb(255, 121, 121);
            object dr = this.gridView.GetRow(e.RowHandle);
            if (dr == null)
                return;
            BurdenMonth bl = (BurdenMonth)dr;
        
            //if (e.Column.FieldName == "BurdenDate")
            //{ 
            double imax = 0;
            double j = 0;
            for (int i = 1; i <= 12; i++)
            {
                j = Convert.ToDouble(bl.GetType().GetProperty("Month" + i).GetValue(bl, null));
                if (imax < j)
                    imax = j;
            }
            if (e.Column.FieldName.Contains("Month"))
            {
                if (imax.ToString() == e.CellValue.ToString())
                {
                    brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, c1, c2, 180);
                    if (brush != null)
                    {
                        e.Graphics.FillRectangle(brush, r);
                    }
                }
            }
        }
	}
}
