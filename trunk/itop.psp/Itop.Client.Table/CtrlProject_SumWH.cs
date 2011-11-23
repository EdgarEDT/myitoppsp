
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
using Itop.Domain.Stutistics;
#endregion

namespace Itop.Client.Table
{
	public partial class CtrlProject_SumWH : UserControl
	{
		#region 构造方法
        public CtrlProject_SumWH()
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
		public IList<Project_Sum> ObjectList
		{
			get { return this.gridControl.DataSource as IList<Project_Sum>; }
		}
        public void Change_ZaoJia(string str1)
        {
            gridView.Columns["Num"].Caption = str1;
        }
		/// <summary>
		/// 获取焦点对象，即FocusedRow
		/// </summary>
		public Project_Sum FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as Project_Sum; }
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

                if (type == "1")
                {
                    //colType.Caption = "回路数";
                    //colT1.Caption = "气象条件";
                    //colT2.Caption = "导线型号";
                    //colT3.Caption = "地线型号";

                    //gridColumn2.Visible = false;
                    //gridColumn3.Visible = false;
                    //colT1.Visible = false;
                    //colT2.Visible = false;
                    //colT3.Visible = false;
                    //colType.Visible = false;
                    this.gridView.GroupPanelText = "线路造价信息";
                    gridView.Columns["Num"].Caption = "单位造价（万元/kM）";


                }
                if (type == "2")
                {


                    //gridColumn4.Visible = false;
                    //gridColumn5.Visible = false;
                    //gridColumn6.Visible = false;
                    //gridColumn7.Visible = false;

                    this.gridView.GroupPanelText = "变电站造价信息";
                    gridView.Columns["Num"].Caption = "单位造价（万元/MVA）";
                }

                IList<Project_Sum> list = Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS5", type);
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
			Project_Sum obj = new Project_Sum();
            obj.UID = Guid.NewGuid().ToString();
            obj.S5 = type;

			//执行添加操作
			using (FrmProject_SumDialogWH dlg = new FrmProject_SumDialogWH())
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
			Project_Sum obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//创建对象的一个副本
			Project_Sum objCopy = new Project_Sum();
			DataConverter.CopyTo<Project_Sum>(obj, objCopy);

			//执行修改操作
			using (FrmProject_SumDialogWH dlg = new FrmProject_SumDialogWH())
			{
				dlg.Object = objCopy;   //绑定副本
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//用副本更新焦点对象
			DataConverter.CopyTo<Project_Sum>(objCopy, obj);
			//刷新表格
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// 删除焦点对象
		/// </summary>
		public void DeleteObject()
		{
			//获取焦点对象
			Project_Sum obj = FocusedObject;
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
				Services.BaseService.Delete<Project_Sum>(obj);
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
