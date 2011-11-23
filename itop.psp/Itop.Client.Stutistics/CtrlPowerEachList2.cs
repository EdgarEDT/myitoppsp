
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
using Itop.Domain.Stutistics;
#endregion

namespace Itop.Client.Stutistics
{
	public partial class CtrlPowerEachList2 : UserControl
	{
		#region 构造方法
        public CtrlPowerEachList2()
		{
			InitializeComponent();
		}
		#endregion

		#region 字段
		protected bool _bAllowUpdate = true;
        Form9_LangFang _langfang = null;
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

        bool isbtn = false;
        public bool IsBTN
        {
            get { return isbtn; }
            set { isbtn = value; }
        }

        public bool bl = true;



        bool isjsxm = false;
        public bool  IsJSXM
        {
            get { return isjsxm; }
            set { isjsxm =value; }
        }

        public Form9_LangFang LangFang
        {
            get { return _langfang ; }
            set { _langfang = value; }
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
		public IList<PowerEachList> ObjectList
		{
			get { return this.gridControl.DataSource as IList<PowerEachList>; }
		}

		/// <summary>
		/// 获取焦点对象，即FocusedRow
		/// </summary>
		public PowerEachList FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PowerEachList; }
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
                //if(LangFang!=null)
                //  LangFang.InitSodata();
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
		public bool RefreshData(string type)
		{
			try
			{
				//IList<PowerEachList> list = Services.BaseService.GetStrongList<PowerEachList>();
                PowerEachList pe=new PowerEachList();
                pe.Types=type;
                //IList<PowerEachList> list = Services.BaseService.GetStrongList<PowerEachList>(pe);
                IList<PowerEachList> list = Services.BaseService.GetList<PowerEachList>("SelectPowerEachListList", pe);
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
		public void AddObject(string type)
		{
			//检查对象链表是否已经加载
			if (ObjectList == null)
			{
				return;
			}
			//新建对象
			PowerEachList obj = new PowerEachList();
            string uid = Guid.NewGuid().ToString();
            obj.UID = uid;
            obj.Types = type;

			//执行添加操作
			using (FrmPowerEachListDialog2 dlg = new FrmPowerEachListDialog2())
			{
                dlg.IsJSXM = isjsxm;
				dlg.IsCreate = true;    //设置新建标志
				dlg.Object = obj;
                dlg.IsBTN = isbtn;
               
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



        public void AddObjecta(string type)
        {
            //检查对象链表是否已经加载
            if (ObjectList == null)
            {
                return;
            }
            //新建对象
            PowerEachList obj = new PowerEachList();
            obj.Types = type;

            //执行添加操作
            using (FrmPowerEachListDialog2 dlg = new FrmPowerEachListDialog2())
            {
                dlg.IsCreate = true;    //设置新建标志
                dlg.Object = obj;
                dlg.IsPower = true;
                dlg.IsJSXM = isjsxm;
                dlg.IsBTN = isbtn;
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

        public void AddObjecta(string type,bool bl)
        {
            //检查对象链表是否已经加载
            if (ObjectList == null)
            {
                return;
            }
            //新建对象
            PowerEachList obj = new PowerEachList();
            obj.Types = type;

            //执行添加操作
            using (FrmPowerEachListDialog2 dlg = new FrmPowerEachListDialog2())
            {
                dlg.IsCreate = true;    //设置新建标志
                dlg.Object = obj;
                dlg.IsPower = true;
                dlg.IsJSXM = isjsxm;
                dlg.bl = bl;
                dlg.IsBTN = isbtn;
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            //将新对象加入到链表中
            ObjectList.Add(obj);

           // ObjectList.Insert(0, obj);

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
			PowerEachList obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//创建对象的一个副本
			PowerEachList objCopy = new PowerEachList();
			DataConverter.CopyTo<PowerEachList>(obj, objCopy);

			//执行修改操作
			using (FrmPowerEachListDialog2 dlg = new FrmPowerEachListDialog2())
			{
				dlg.Object = objCopy;   //绑定副本
                dlg.IsJSXM = isjsxm;
                dlg.IsBTN = isbtn;
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//用副本更新焦点对象
			DataConverter.CopyTo<PowerEachList>(objCopy, obj);
			//刷新表格
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// 删除焦点对象
		/// </summary>
		public void DeleteObject(string lb)
		{
			//获取焦点对象
			PowerEachList obj = FocusedObject;
			if (obj == null)
			{
                bl = false;
				return;
			}

            

            int coun = 0;

            switch (lb)
            {
                case "xuqiu":
                PowerTypes pt = new PowerTypes();
                pt.Flag2 = obj.UID;
                IList<PowerTypes> li = Services.BaseService.GetList<PowerTypes>("SelectPowerTypesByFlag2", pt);
                coun = li.Count;

                    break;
                case "xihua":
                    PowerProjectTypes pt1 = new PowerProjectTypes();
                    pt1.Flag2 = obj.UID;
                    IList<PowerProjectTypes> li1 = Services.BaseService.GetList<PowerProjectTypes>("SelectPowerProjectTypesByFlag2", pt1);
                    coun = li1.Count;
                    break;
                case "shebei":
                    PowerStuffTypes pt2 = new PowerStuffTypes();
                    pt2.Flag2 = obj.UID;
                    IList<PowerStuffTypes> li2 = Services.BaseService.GetList<PowerStuffTypes>("SelectPowerStuffTypesByFlag2", pt2);
                    coun = li2.Count;
                    break;
                case "fanwei":
                    PowersTypes pt3 = new PowersTypes();
                    pt3.Flag2 = obj.UID;
                    IList<PowersTypes> li3 = Services.BaseService.GetList<PowersTypes>("SelectPowersTypesByFlag2", pt3);
                    coun = li3.Count;
                    break;
                case "sb":
                    PowerProTypes pt4 = new PowerProTypes();
                    pt4.Flag2 = obj.UID;
                    IList<PowerProTypes> li4 = Services.BaseService.GetList<PowerProTypes>("SelectPowerProTypesByFlag2INParent", pt4);
                    coun = li4.Count;
                    break;
                case "gusuan":
                    PSP_PowerProValues_LangFang pt5 = new PSP_PowerProValues_LangFang();
                    pt5.Flag2 = obj.UID;
                    IList<PSP_PowerProValues_LangFang> li5 = Services.BaseService.GetList<PSP_PowerProValues_LangFang>("SelectPSP_PowerProValues_LangFangByFlag2INParent", pt5);
                    coun = li5.Count;
                    break;
            }



            if (coun > 0)
            {
                MsgBox.Show("该分类下面有记录，无法删除");
                bl = false;
                return;
            }


			//请求确认
			if (MsgBox.ShowYesNo(Strings.SubmitDelete) != DialogResult.Yes)
			{
                bl = false;
				return;
			}

			//执行删除操作
			try
			{
				Services.BaseService.Delete<PowerEachList>(obj);

                if (lb == "sb")
                {
                    Services.BaseService.Update("DeletePowerPicSelect1", obj.UID);
                    Services.BaseService.Update("DeletePowerProTypesInFlag2", obj.UID);
                }
                if (lb == "gusuan")
                {
                    Services.BaseService.Update("DeletePowerPicSelect1", obj.UID);
                    Services.BaseService.Update("DeletePowerProTypesInFlag2", obj.UID);
                }
			}
			catch
			{
                //Debug.Fail(exc.Message);
                //HandleException.TryCatch(exc);
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













        public void DeleteObject()
        {
            //获取焦点对象
            PowerEachList obj = FocusedObject;
            if (obj == null)
            {
                bl = false;
                return;
            }



            int coun = 0;


            //执行删除操作
            try
            {
                Services.BaseService.Delete<PowerEachList>(obj);
                Services.BaseService.Update("DeletePowerPicSelect1", obj.UID);
    
                
            }
            catch
            {
                //Debug.Fail(exc.Message);
                //HandleException.TryCatch(exc);
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
