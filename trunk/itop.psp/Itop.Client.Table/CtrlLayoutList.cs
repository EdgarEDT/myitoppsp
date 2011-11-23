
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
using System.IO;
using Itop.Client.Stutistics;
#endregion

namespace Itop.Client.Table
{
	public partial class CtrlLayoutList : UserControl
	{
		#region 构造方法
		public CtrlLayoutList()
		{
			InitializeComponent();
		}
		#endregion

		#region 字段
		protected bool _bAllowUpdate = true;
        string type = "";
        public bool editright = true;
		#endregion

		#region 公共属性

        public string Type
        {
            set { type = value; }
            get { return type; }
        
        }


        bool isshow = true;
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow
        {
            get { return isshow; }
            set { isshow = value; }
        }


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
		public IList<LayoutList> ObjectList
		{
			get { return this.gridControl.DataSource as IList<LayoutList>; }
		}

		/// <summary>
		/// 获取焦点对象，即FocusedRow
		/// </summary>
		public LayoutList FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as LayoutList; }
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
				IList<LayoutList> list = Services.BaseService.GetStrongList<LayoutList>();
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




        public bool RefreshData1()
        {
            try
            {
                //IList<LayoutList> list = Services.BaseService.GetStrongList<LayoutList>();
                IList<LayoutList> list = Services.BaseService.GetList<LayoutList>("SelectLayoutListByTypes", type);

                string filepath = Path.GetTempPath() + "\\" + Path.GetFileName("SubstationGuiHua.xml");
                //this.ctrlSubstation_Info1.bandedGridView1.SaveLayoutToXml(filepath + "SubstationGuiHua.xml");

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
			//检查对象链表是否已经加载
			if (ObjectList == null)
			{
				return;
			}
			//新建对象
			LayoutList obj = new LayoutList();
            obj.Types = type;

			//执行添加操作
			using (FrmLayoutListDialog dlg = new FrmLayoutListDialog())
			{
				dlg.IsCreate = true;    //设置新建标志
				dlg.Object = obj;
                dlg.IsShow = isshow;
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
			LayoutList obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//创建对象的一个副本
			LayoutList objCopy = new LayoutList();
			DataConverter.CopyTo<LayoutList>(obj, objCopy);

			//执行修改操作
			using (FrmLayoutListDialog dlg = new FrmLayoutListDialog())
			{
				dlg.Object = objCopy;   //绑定副本
                dlg.IsShow = isshow;
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//用副本更新焦点对象
			DataConverter.CopyTo<LayoutList>(objCopy, obj);
			//刷新表格
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// 删除焦点对象
		/// </summary>
		public void DeleteObject()
		{
			//获取焦点对象
			LayoutList obj = FocusedObject;
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
				Services.BaseService.Delete<LayoutList>(obj);
                switch (obj.Types)
                { 
                    case "1":
                        Services.BaseService.Update("DeleteLine_InfoByFlag1", obj.UID);
                        break;

                    case "2":
                        Services.BaseService.Update("DeleteSubstation_InfoByFlag1", obj.UID);
                        break;
                
                
                }



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
