
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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
#endregion

namespace Itop.Client.Stutistics
{
	public partial class CtrlLine_beizhu : UserControl
	{
        private string types1 = "";
        private string types2 = "";
        private string flags1 = "";
        private string zz = "";
        public string Type
        {
            get { return types1; }
            set { types1 = value; }
        }

        public string Type2
        {
            get { return types2; }
            set { types2 = value; }
        }

        public string Flag
        {
            get { return flags1; }
            set { flags1 = value; }
        }


        public bool IsSelect
        {
            get { return isselect; }
            set { isselect = value; }
        }

        bool isselect = false;




		#region 构造方法
		public CtrlLine_beizhu()
		{
			InitializeComponent();
		}
        public CtrlLine_beizhu(string z)
        {
            InitializeComponent();
            zz = z;
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
        /// 
        ///DevExpress.XtraGrid.Views.BandedGrid.BandedGridView

        public BandedGridView GridView
		{
			get { return this.bandedGridView2; }
		}

		/// <summary>
		/// 获取GridControl的数据源，即对象的列表
		/// </summary>
		public IList<Line_beizhu> ObjectList
		{
            get { return this.gridControl.DataSource as IList<Line_beizhu>; }
		}

		/// <summary>
		/// 获取焦点对象，即FocusedRow
		/// </summary>
		public Line_beizhu FocusedObject
		{
            get { return this.bandedGridView2.GetRow(this.bandedGridView2.FocusedRowHandle) as Line_beizhu; }
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
            if (GridHelper.HitCell(this.bandedGridView2, point))
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
            ComponentPrint.ShowPreview(this.gridControl, this.bandedGridView2.GroupPanelText);
		}

		/// <summary>
		/// 刷新表格中的数据
		/// </summary>
		/// <returns>ture:成功  false:失败</returns>
		public bool RefreshData()
		{
			try
			{
                IList<Line_beizhu> list = new List<Line_beizhu>();


                int dd = 0;
                Line_beizhu li = new Line_beizhu();
                li.DY = dd;
                li.Flag = zz;

                if (types2 == "66")
                { list = Services.BaseService.GetList<Line_beizhu>("SelectLine_beizhuByFlagDy", li); }
                else if (types2 == "10")
                { list = Services.BaseService.GetList<Line_beizhu>("SelectLine_beizhuByFlagDy1", li); }




                //IList<Line_Info> list = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByFlagDy", li);
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






        public bool RefreshData(string type)
        {
            IList<Line_beizhu> list = new List<Line_beizhu>();
            try
            {
                flags1 = type;
                int dd = 0;
                if (types2 == "66")
                { dd = 66; }
                else if (types2 == "10")
                { dd = 10; }

                Line_beizhu li = new Line_beizhu();
                li.DY = dd;
                li.Flag = flags1;


                if (types2 == "66")
                { list = Services.BaseService.GetList<Line_beizhu>("SelectLine_beizhuByFlagDy", li); }
                else if (types2 == "10")
                { list = Services.BaseService.GetList<Line_beizhu>("SelectLine_beizhuByFlagDy1", li); }




                //IList<Line_Info> list = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByFlagDy", li);
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




        public bool RefreshData(string layer, bool isrun, string power)
        {

            IList<Line_beizhu> lists = new List<Line_beizhu>();
            try
            {

                Line_beizhu ll1 = new Line_beizhu();
                ll1.AreaID = layer;
                ll1.DY = int.Parse(power);

                if (isrun)
                {
                    lists = Services.BaseService.GetList<Line_beizhu>("SelectLine_beizhuByXZ", ll1);
                }
                else
                {
                    lists = Services.BaseService.GetList<Line_beizhu>("SelectLine_beizhuByGH", ll1);
                }
               
                this.gridControl.DataSource = lists;


                //foreach (GridColumn gc in this.bandedGridView2.Columns)
                //{
                //    gc.Visible = false;
                //    gc.OptionsColumn.ShowInCustomizationForm = false;
                //    if (gc.FieldName == "Title" || gc.FieldName == "DY" || gc.FieldName == "K2" || gc.FieldName == "K5")
                //    {
                //        gc.Visible = true;
                //        gc.OptionsColumn.ShowInCustomizationForm = true;
                //    }
                //}
                bandedGridView2.OptionsView.ColumnAutoWidth = true;


                foreach (GridBand gc in this.bandedGridView2.Bands)
                {
                    try
                    {
                        gc.Visible = false;

                        if (gc.Columns[0].FieldName == "Title" || gc.Columns[0].FieldName == "DY" || gc.Columns[0].FieldName == "K2" || gc.Columns[0].FieldName == "K5")
                        {
                            gc.Visible = true;
                            gc.Caption = gc.Columns[0].Caption;
                            gc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        }

                        
                    }
                    catch { }

                }


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
            Line_beizhu obj = new Line_beizhu();
            obj.L6 = DateTime.Now;

			//执行添加操作
            using (FrmLine_InfoDialog_beizhu dlg = new FrmLine_InfoDialog_beizhu())
			{
                if(gridControl.DataSource!=null)
                    dlg.LIST = (IList<Line_beizhu>)gridControl.DataSource;

                dlg.Type = types1;
                dlg.Flag = flags1;
                dlg.Type2 = types2;
               // dlg.ctrlLint_beizhu = this;

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
            GridHelper.FocuseRow(this.bandedGridView2, obj);
		}

		/// <summary>
		/// 修改焦点对象
		/// </summary>
		public void UpdateObject()
		{
			//获取焦点对象



            Line_beizhu obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//创建对象的一个副本
            Line_beizhu objCopy = new Line_beizhu();
            DataConverter.CopyTo<Line_beizhu>(obj, objCopy);

			//执行修改操作
            using (FrmLine_InfoDialog_beizhu dlg = new FrmLine_InfoDialog_beizhu())
			{
                if (gridControl.DataSource != null)
                    dlg.LIST = (IList<Line_beizhu>)gridControl.DataSource;

                dlg.IsSelect = isselect;
               // dlg.ctrlLint_Info = this;
                dlg.Type = types1;
                dlg.Flag = flags1;
                dlg.Type2 = types2;


				dlg.Object = objCopy;   //绑定副本
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//用副本更新焦点对象
            DataConverter.CopyTo<Line_beizhu>(objCopy, obj);
			//刷新表格
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// 删除焦点对象
		/// </summary>
		public void DeleteObject()
		{
			//获取焦点对象
            Line_beizhu obj = FocusedObject;
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
                Services.BaseService.Delete<Line_beizhu>(obj);
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
				HandleException.TryCatch(exc);
				return;
			}

            this.bandedGridView2.BeginUpdate();
			//记住当前焦点行索引
            int iOldHandle = this.bandedGridView2.FocusedRowHandle;
			//从链表中删除
			ObjectList.Remove(obj);
			//刷新表格
			gridControl.RefreshDataSource();
			//设置新的焦点行索引
            GridHelper.FocuseRowAfterDelete(this.bandedGridView2, iOldHandle);
            this.bandedGridView2.EndUpdate();
		}
		#endregion
	}
}
