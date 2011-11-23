
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
	public partial class CtrlPW_Sum : UserControl
	{
		#region 构造方法
		public CtrlPW_Sum()
		{
			InitializeComponent();

		}
		#endregion

		#region 字段
		protected bool _bAllowUpdate = true;
        public bool editright = true;
        private string proguid = "";

        public string Proguid
        {
            get { return proguid; }
            set { proguid = value; }
        }

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
            get { return advBandedGridView1; }
		}

		/// <summary>
		/// 获取GridControl的数据源，即对象的列表
		/// </summary>
		public IList<PW_tb3a> ObjectList
		{
            get { return this.gridControl.DataSource as IList<PW_tb3a>; }
		}

		/// <summary>
		/// 获取焦点对象，即FocusedRow
		/// </summary>
        public PW_tb3a FocusedObject
		{
            get { return this.advBandedGridView1.GetRow(this.advBandedGridView1.FocusedRowHandle) as PW_tb3a; }
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
            if (GridHelper.HitCell(this.advBandedGridView1, point))
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
            ComponentPrint.ShowPreview(this.gridControl, this.advBandedGridView1.GroupPanelText);
		}

		/// <summary>
		/// 刷新表格中的数据
		/// </summary>
		/// <returns>ture:成功  false:失败</returns>
		public bool RefreshData()
		{
			try
			{

                this.advBandedGridView1.GroupPanelText = "线路设备情况表";
                PW_tb3a p = new PW_tb3a();
                p.col2 = proguid;
                IList<PW_tb3a> list = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aList", p);
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
            PW_tb3a obj = new PW_tb3a();
            obj.UID = Guid.NewGuid().ToString();
            obj.col2 = proguid;
            //obj.S5 = type;

			//执行添加操作
			using (FrmPW_SumDialog dlg = new FrmPW_SumDialog())
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
            GridHelper.FocuseRow(this.advBandedGridView1, obj);
		}
        public void SubAdd()
        {
            PW_tb3a obj = FocusedObject;
            if (obj == null)
            {
                MessageBox.Show("请选择记录。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            FrmPW3b f = new FrmPW3b();
            f.addright =f.editright= editright;
            f.Type = obj.UID;
            f.Show();
        }
		/// <summary>
		/// 修改焦点对象
		/// </summary>
		public void UpdateObject()
		{
			//获取焦点对象
            PW_tb3a obj = FocusedObject;
			if (obj == null)
			{
				return;
			}
            obj.col2 = proguid;
			//创建对象的一个副本
            PW_tb3a objCopy = new PW_tb3a();
            DataConverter.CopyTo<PW_tb3a>(obj, objCopy);

			//执行修改操作
			using (FrmPW_SumDialog dlg = new FrmPW_SumDialog())
			{
				dlg.Object = objCopy;   //绑定副本
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//用副本更新焦点对象
            DataConverter.CopyTo<PW_tb3a>(objCopy, obj);
			//刷新表格
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// 删除焦点对象
		/// </summary>
        public void DeleteObject()
        {
            if (MsgBox.ShowYesNo(Strings.SubmitDelete) != DialogResult.Yes)
            {
                return;
            }
            

            int[] aa = advBandedGridView1.GetSelectedRows();
            for (int i = 0; i < aa.Length; i++)
            {
                //获取焦点对象

                PW_tb3a obj = (PW_tb3a)advBandedGridView1.GetRow(aa[i]);  //FocusedObject;
                if (obj == null)
                {
                    return;
                }

                //请求确认


                //执行删除操作
                try
                {
                    Services.BaseService.Delete<PW_tb3a>(obj);
                }
                catch (Exception exc)
                {
                    Debug.Fail(exc.Message);
                    HandleException.TryCatch(exc);
                    return;
                }


               // this.advBandedGridView1.BeginUpdate();
                //记住当前焦点行索引
                //int iOldHandle = this.advBandedGridView1.FocusedRowHandle;
                //从链表中删除
               // ObjectList.Remove(obj);
                //刷新表格
                
                //设置新的焦点行索引
                //GridHelper.FocuseRowAfterDelete(this.advBandedGridView1, iOldHandle);
                //this.advBandedGridView1.EndUpdate();

            }
            RefreshData();
            //this.advBandedGridView1.EndUpdate();
        }
		#endregion


	}
}
