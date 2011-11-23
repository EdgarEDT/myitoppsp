
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
	public partial class CtrlPowerProTypes : UserControl
	{
		#region 构造方法
		public CtrlPowerProTypes()
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
		public IList<PowerProTypes> ObjectList
		{
			get { return this.gridControl.DataSource as IList<PowerProTypes>; }
		}

		/// <summary>
		/// 获取焦点对象，即FocusedRow
		/// </summary>
		public PowerProTypes FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PowerProTypes; }
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


        ///// <summary>
        ///// 加载数据
        ///// </summary>
        ///// <param name="layer">图层ID</param>
        ///// <param name="isrun">是否运行，规划  true则运行</param>
        ///// <returns></returns>
        ///// 










        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="layer">图层ID</param>
        /// <param name="isrun">是否运行，规划  true则运行</param>
        /// <param name="IsLine">是否线路，变电所</param>
        /// <returns></returns>
        public bool RefreshData(string layer, bool isrun,bool IsLine)
		{

            IList<PowerProTypes> list = new List<PowerProTypes>();

            int a1 = 1;
            if (IsLine)
            { a1 = 1; }
            else
            { a1 = 2; }

			try
			{
                if (isrun)
                {
                    
                    PowerProTypes pp1 = new PowerProTypes();
                    pp1.Flag = a1;
                    pp1.Flag2 = "-1";
                    list = Services.BaseService.GetList<PowerProTypes>("SelectPowerProTypesByFlagFlag2", pp1);
                }
                else
                {
                    PowerProTypes pp2 = new PowerProTypes();
                    pp2.Flag = a1;
                    pp2.Flag2 = layer;
                    list = Services.BaseService.GetList<PowerProTypes>("SelectPowerProTypesByFlagFlag21", pp2);
                
                }




				//IList<PowerProTypes> list = Services.BaseService.GetStrongList<PowerProTypes>();
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




        public bool RefreshData(string layer, bool isrun, bool IsLine,string power)
        {

            IList<PowerProTypes> list = new List<PowerProTypes>();

            int a1 = 1;
            if (IsLine)
            { a1 = 1; }
            else
            { a1 = 2; }

            try
            {
                if (isrun)
                {

                    PowerProTypes pp1 = new PowerProTypes();
                    pp1.Flag = a1;
                    pp1.Flag2 = "-1";
                    if (power != "")
                    {
                        switch (power)
                        {
                            case "500":
                                power = "'500'";
                                break;

                            case "220":
                                power = "'220'";
                                break;

                            case "110":
                                power = "'110'";
                                break;

                            case "35":
                                power = "'35','3'";
                                break;

                            case "10":
                                power = "'10','1'";
                                break;
                        }
                        pp1.ParentID = power;
                        list = Services.BaseService.GetList<PowerProTypes>("SelectPowerProTypesByFlagFlag2", pp1);
                    }
                    else
                    {
                        
                        list = Services.BaseService.GetList<PowerProTypes>("SelectPowerProTypesByFlagFlag3", pp1);
                    }
                }
                else
                {
                    PowerProTypes pp2 = new PowerProTypes();
                    pp2.Flag = a1;
                    pp2.Flag2 = layer;
                    if (power != "")
                    {
                        switch (power)
                        { 
                            case "500":
                                power = "'500'";
                                break;

                            case "220":
                                power = "'220'";
                                break;

                            case "110":
                                power = "'110'";
                                break;

                            case "35":
                                power = "'35','3'";
                                break;

                            case "10":
                                power = "'10','1'";
                                break;
                        }


                        pp2.ParentID = power;
                        list = Services.BaseService.GetList<PowerProTypes>("SelectPowerProTypesByFlagFlag21", pp2);
                    }
                    else
                    {
                        list = Services.BaseService.GetList<PowerProTypes>("SelectPowerProTypesByFlagFlag31", pp2);
                    }

                }




                //IList<PowerProTypes> list = Services.BaseService.GetStrongList<PowerProTypes>();
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
            //if (ObjectList == null)
            //{
            //    return;
            //}
            ////新建对象
            //PowerProTypes obj = new PowerProTypes();

            ////执行添加操作
            //using (FrmPowerProTypesDialog dlg = new FrmPowerProTypesDialog())
            //{
            //    dlg.IsCreate = true;    //设置新建标志
            //    dlg.Object = obj;
            //    if (dlg.ShowDialog() != DialogResult.OK)
            //    {
            //        return;
            //    }
            //}

            ////将新对象加入到链表中
            //ObjectList.Add(obj);

            ////刷新表格，并将焦点行定位到新对象上。
            //gridControl.RefreshDataSource();
            //GridHelper.FocuseRow(this.gridView, obj);
		}

		/// <summary>
		/// 修改焦点对象
		/// </summary>
		public void UpdateObject()
		{
			//获取焦点对象
            //PowerProTypes obj = FocusedObject;
            //if (obj == null)
            //{
            //    return;
            //}

            ////创建对象的一个副本
            //PowerProTypes objCopy = new PowerProTypes();
            //DataConverter.CopyTo<PowerProTypes>(obj, objCopy);

            ////执行修改操作
            //using (FrmPowerProTypesDialog dlg = new FrmPowerProTypesDialog())
            //{
            //    dlg.Object = objCopy;   //绑定副本
            //    if (dlg.ShowDialog() != DialogResult.OK)
            //    {
            //        return;
            //    }
            //}

            ////用副本更新焦点对象
            //DataConverter.CopyTo<PowerProTypes>(objCopy, obj);
            ////刷新表格
            //gridControl.RefreshDataSource();
		}

		/// <summary>
		/// 删除焦点对象
		/// </summary>
		public void DeleteObject()
		{
			//获取焦点对象
			PowerProTypes obj = FocusedObject;
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
				Services.BaseService.Delete<PowerProTypes>(obj);
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
