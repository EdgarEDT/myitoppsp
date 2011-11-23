
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
#endregion

namespace Itop.Client.Stutistics
{
	public partial class CtrlPs_Substation_As : UserControl
	{
		#region 构造方法
		public CtrlPs_Substation_As()
		{
			InitializeComponent();
		}
		#endregion

		#region 字段
		protected bool _bAllowUpdate = true;
        private string type = "变电站";
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

        double glys = 0.95;
        public double GLYS
        {
            get { return glys; }
            set { glys = value; }
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
		public IList<Ps_Power> ObjectList
		{
            get { return this.gridControl.DataSource as IList<Ps_Power>; }
		}

		/// <summary>
		/// 获取焦点对象，即FocusedRow
		/// </summary>
        public Ps_Power FocusedObject
		{
            get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as Ps_Power; }
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
                int year=DateTime.Now.Year;
                IList<Ps_Power> list = Services.BaseService.GetList<Ps_Power>("SelectPs_PowerByType", type);
                foreach (Ps_Power ps in list)
                {
                    double zdfh = 0;
                    object obj = ps.GetType().GetProperty("y" + year).GetValue(ps, null);
                    if (obj != null)
                    {
                        zdfh = Convert.ToDouble(obj);
                    }
                    if (ps.RL != 0)
                        ps.Col5 = (zdfh / (ps.RL*glys)).ToString("n2");

                    double rl = ps.RL;
                    int ts = ps.TS;
                    double sss = 0;

                    switch (ts)
                    {
                        case 2:
                            sss = 0.65;
                            break;
                        case 3:
                            sss = 0.65;
                            break;
                        case 4:
                            sss = 0.87;
                            break;
                        default:
                            sss = 1;
                            break;
                    }
                    ps.Col6 = (rl * sss - zdfh / glys).ToString("n2");
                
                
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
            Ps_Power obj = new Ps_Power();
            obj.Type = type;
            obj.ID = Guid.NewGuid().ToString();
			//执行添加操作
			using (FrmPs_Substation_AsDialog dlg = new FrmPs_Substation_AsDialog())
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
            RefreshData();
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
			Ps_Power obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//创建对象的一个副本
            Ps_Power objCopy = new Ps_Power();
            DataConverter.CopyTo<Ps_Power>(obj, objCopy);

			//执行修改操作
			using (FrmPs_Substation_AsDialog dlg = new FrmPs_Substation_AsDialog())
			{
				dlg.Object = objCopy;   //绑定副本
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//用副本更新焦点对象
            DataConverter.CopyTo<Ps_Power>(objCopy, obj);
			//刷新表格
            RefreshData();
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// 删除焦点对象
		/// </summary>
		public void DeleteObject()
		{
			//获取焦点对象
			Ps_Power obj = FocusedObject;
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
                Services.BaseService.Delete<Ps_Power>(obj);
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
