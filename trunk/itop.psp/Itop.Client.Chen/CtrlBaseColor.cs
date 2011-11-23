
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
using Itop.Domain.BaseDatas;
#endregion

namespace Itop.Client.Chen
{
	public partial class CtrlBaseColor : UserControl
	{
		#region 构造方法
		public CtrlBaseColor()
		{
			InitializeComponent();
		}
		#endregion

		#region 字段
		protected bool _bAllowUpdate = true;
        ColorConverter cc = new ColorConverter();
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

        private DataTable dt = new DataTable();
        public DataTable DT
        {
            set { dt = value; }
            get { return dt; }
        }

        private string id = "";
        public string ID
        {
            set { id = value; }
            get { return id; }
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
		public IList<BaseColor> ObjectList
		{
			get { return this.gridControl.DataSource as IList<BaseColor>; }
		}

		/// <summary>
		/// 获取焦点对象，即FocusedRow
		/// </summary>
		public BaseColor FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as BaseColor; }
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
		public bool RefreshData()
		{
			try
			{
                ////////IList<BaseColor> list = Services.BaseService.GetStrongList<BaseColor>();

                ////////foreach (BaseColor bb in list)
                ////////{
                ////////    Color cl = ColorTranslator.FromOle(bb.Color);
                ////////    bb.Color1 = cl;
                ////////}


                ////////this.gridControl.DataSource = list;
                IList<BaseColor> list = Services.BaseService.GetList<BaseColor>("SelectBaseColorByWhere", "Remark='" + id + "'");

                IList<BaseColor> li = new List<BaseColor>();
                bool bl = false;
                foreach (DataRow row in dt.Rows)
                {
                    bl = false;
                    foreach (BaseColor bc in list)
                    {
                        if (row["Title"].ToString() == bc.Title)
                        {
                            bl = true;
                            BaseColor bc1 = new BaseColor();
                            CopyBaseColor(bc1, bc);
                            li.Add(bc1);
                        }


                    }
                    if (!bl)
                    {
                        BaseColor bc1 = new BaseColor();
                        bc1.UID = Guid.NewGuid().ToString();
                        bc1.Remark = id;
                        bc1.Title = row["Title"].ToString();
                        bc1.Color = 0;
                        bc1.Color1 = Color.Black;
                        Services.BaseService.Create<BaseColor>(bc1);
                        li.Add(bc1);
                    }

                }

                this.gridControl.DataSource = li;



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
			BaseColor obj = new BaseColor();
            obj.Remark = id;

			//执行添加操作
			using (FrmBaseColorDialog dlg = new FrmBaseColorDialog())
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
			BaseColor obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//创建对象的一个副本
			BaseColor objCopy = new BaseColor();
			DataConverter.CopyTo<BaseColor>(obj, objCopy);

			//执行修改操作
			using (FrmBaseColorDialog dlg = new FrmBaseColorDialog())
			{
				dlg.Object = objCopy;   //绑定副本
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//用副本更新焦点对象
			DataConverter.CopyTo<BaseColor>(objCopy, obj);
			//刷新表格
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// 删除焦点对象
		/// </summary>
		public void DeleteObject()
		{
			//获取焦点对象
			BaseColor obj = FocusedObject;
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
				Services.BaseService.Delete<BaseColor>(obj);
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



        /// <summary>
        /// 删除焦点对象
        /// </summary>
        public void SaveList()
        {
            foreach (BaseColor bc in ObjectList)
            {
                try
                {
                    bc.Color = ColorTranslator.ToOle(bc.Color1);
                    Services.BaseService.Update<BaseColor>(bc);
                }
                catch (Exception exc)
                {
                    Debug.Fail(exc.Message);
                    HandleException.TryCatch(exc);
                    return;
                }
            
            }

            

            this.gridView.BeginUpdate();
            //记住当前焦点行索引
            gridControl.RefreshDataSource();
            this.gridView.EndUpdate();
            MsgBox.Show("保存成功！");
        }
		#endregion

        private void CopyBaseColor(BaseColor bc1, BaseColor bc2)
        {
            bc1.UID = bc2.UID;
            bc1.Title = bc2.Title;
            bc1.Remark = bc2.Remark;
            bc1.Color = bc2.Color;
            bc1.Color1 = ColorTranslator.FromOle(bc2.Color);
        }
	}
}
