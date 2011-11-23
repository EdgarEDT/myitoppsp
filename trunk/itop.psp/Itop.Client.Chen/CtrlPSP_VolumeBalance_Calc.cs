
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
using Itop.Domain.Chen;
#endregion

namespace Itop.Client.Chen
{
	public partial class CtrlPSP_VolumeBalance_Calc : UserControl
	{
		#region 构造方法
		public CtrlPSP_VolumeBalance_Calc()
		{
			InitializeComponent();
		}
		#endregion

		#region 字段
		protected bool _bAllowUpdate = true;
        string formtitle = "";
        public string FormTitle
        {
            get { return formtitle; }
            set { formtitle = value; }
        }

        string ctrtitle = "";
        /// <summary>
        /// 获取flag对象
        /// </summary>
        public string CtrTitle
        {
            get { return ctrtitle; }
            set { ctrtitle = value; }
        }

		#endregion

		#region 公共属性


        string dy = "";
        /// <summary>
        /// 获取电压对象
        /// </summary>
        public string DY
        {
            get { return dy; }
            set { dy = value; }
        }

        string type = "";
        /// <summary>
        /// 获取type对象
        /// </summary>
        public string TYPE
        {
            get { return type; }
            set { type=value; }
        }

        string flag = "";
        /// <summary>
        /// 获取flag对象
        /// </summary>
        public string FLAG
        {
            get { return flag; }
            set { flag=value; }
        }


        double sum = 0.0;
        /// <summary>
        /// 获取sum对象
        /// </summary>
        public double SUM
        {
            get { return sum; }
            set { sum = value; }
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
		public IList<PSP_VolumeBalance_Calc> ObjectList
		{
			get { return this.gridControl.DataSource as IList<PSP_VolumeBalance_Calc>; }
		}

		/// <summary>
		/// 获取焦点对象，即FocusedRow
		/// </summary>
		public PSP_VolumeBalance_Calc FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PSP_VolumeBalance_Calc; }
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
        /// 保存合计
        /// </summary>
        /// <returns>ture:成功  false:失败</returns>
        public bool SaveSum()
        {
            try
            {
                double s = 0.0;
                sum = 0;
                foreach (PSP_VolumeBalance_Calc pvc in ObjectList)
                {
                    sum += pvc.Vol;
                }

                PSP_VolumeBalance pv = Services.BaseService.GetOneByKey<PSP_VolumeBalance>(flag);
                if (flag == "")
                    return false;

                switch (type)
                { 
                    case "1":
                        pv.L2 = sum;
                        break;
                    case "2":
                        pv.L10 = sum;
                        break;
                    case "3":
                        pv.L11 = sum;
                        break;
                }
                Services.BaseService.Update<PSP_VolumeBalance>(pv);
            }
            catch (Exception exc)
            {
                //Debug.Fail(exc.Message);
                //HandleException.TryCatch(exc);
                return false;
            }

            return true;
        }
        /// <summary>
        /// 删除后保存合计
        /// </summary>
        /// <returns>ture:成功  false:失败</returns>
        public bool deleSaveSum(IList<PSP_VolumeBalance_Calc> ObjectList1,string flag)
        {
            try
            {
                double s = 0.0;
                sum = 0;
                foreach (PSP_VolumeBalance_Calc pvc in ObjectList1)
                {
                    sum += pvc.Vol;
                }

                PSP_VolumeBalance pv = Services.BaseService.GetOneByKey<PSP_VolumeBalance>(flag);
                if (flag == "")
                    return false;

                switch (type)
                {
                    case "1":
                        pv.L2 = sum;
                        break;
                    case "2":
                        pv.L10 = sum;
                        break;
                    case "3":
                        pv.L11 = sum;
                        break;
                }
                Services.BaseService.Update<PSP_VolumeBalance>(pv);
            }
            catch (Exception exc)
            {
                //Debug.Fail(exc.Message);
                //HandleException.TryCatch(exc);
                return false;
            }

            return true;
        }


		/// <summary>
		/// 刷新表格中的数据
		/// </summary>
		/// <returns>ture:成功  false:失败</returns>
		public bool RefreshData()
		{
			try
			{
                PSP_VolumeBalance_Calc pvc=new PSP_VolumeBalance_Calc();
                pvc.Type=type;
                pvc.Flag=flag;


                IList<PSP_VolumeBalance_Calc> list = Services.BaseService.GetList<PSP_VolumeBalance_Calc>("SelectPSP_VolumeBalance_CalcByTypeFlag", pvc);
				this.gridControl.DataSource = list;
                SaveSum();
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
			PSP_VolumeBalance_Calc obj = new PSP_VolumeBalance_Calc();
            obj.Flag = flag;
            obj.Type = type;
            obj.Col1 = dy;
            obj.CreateTime = DateTime.Now;

			//执行添加操作
			using (FrmPSP_VolumeBalance_CalcDialog dlg = new FrmPSP_VolumeBalance_CalcDialog())
			{
				dlg.IsCreate = true;    //设置新建标志
				dlg.Object = obj;
                dlg.Type = type;
                dlg.CtrTitle = ctrtitle;
                dlg.FormTitle = FormTitle;
                if (type == "1" && dy=="220")
                dlg.editorRow2.Visible = true;
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
     
			//将新对象加入到链表中
			ObjectList.Add(obj);
            SaveSum();
			//刷新表格，并将焦点行定位到新对象上。
			gridControl.RefreshDataSource();
			GridHelper.FocuseRow(this.gridView, obj);
            if (obj.Type == "1")
            {
                PSP_VolumeBalance ob = new PSP_VolumeBalance();
                //ob.TypeID = dy;
                ob.UID = obj.Flag;
                //string s ="";
                IList<PSP_VolumeBalance> list = Services.BaseService.GetList<PSP_VolumeBalance>("SelectPSP_VolumeBalanceByKey", ob);
                if(list!=null)
                    if (list.Count == 1)
                    {
                        
                        ob.Flag = list[0].Flag;
                        ob.TypeID = list[0].TypeID;
                        //s = obj.Flag;
                        list.Clear();
                        list = Services.BaseService.GetList<PSP_VolumeBalance>("SelectPSP_VolumeBalanceByTypeID", ob);
                        foreach (PSP_VolumeBalance pvm in list)
                        {
                            if (pvm.UID == ob.UID)
                                continue;
                            PSP_VolumeBalance_Calc pvc = new PSP_VolumeBalance_Calc();
                            pvc = obj;
                            pvc.Flag = pvm.UID;
                            pvc.UID = Guid.NewGuid().ToString();
                            pvc.Vol = 0;
                            pvc.Col2 = "";
                            Services.BaseService.Create<PSP_VolumeBalance_Calc>(pvc);
                        }
                    }
                RefreshData();
            }
           
		}

		/// <summary>
		/// 修改焦点对象
		/// </summary>
		public void UpdateObject()
		{
			//获取焦点对象
			PSP_VolumeBalance_Calc obj = FocusedObject;
			if (obj == null)
			{
				return;
			}
            PSP_VolumeBalance_Calc objtemp = new PSP_VolumeBalance_Calc ();
            objtemp.Title = obj.Title;
            objtemp.Type = obj.Type;
			//创建对象的一个副本
			PSP_VolumeBalance_Calc objCopy = new PSP_VolumeBalance_Calc();
			DataConverter.CopyTo<PSP_VolumeBalance_Calc>(obj, objCopy);

			//执行修改操作
			using (FrmPSP_VolumeBalance_CalcDialog dlg = new FrmPSP_VolumeBalance_CalcDialog())
			{
				dlg.Object = objCopy;   //绑定副本
                dlg.Type = type;
                dlg.CtrTitle = ctrtitle;
                dlg.FormTitle = FormTitle;
                if (type == "1" && dy == "220")
                    dlg.editorRow2.Visible = true;
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//用副本更新焦点对象
			DataConverter.CopyTo<PSP_VolumeBalance_Calc>(objCopy, obj);
			//刷新表格
            SaveSum();
			gridControl.RefreshDataSource();
            if (objtemp.Title!=obj.Title)
             if (obj.Type == "1")
            {
                
                IList<PSP_VolumeBalance_Calc> list = Services.BaseService.GetList<PSP_VolumeBalance_Calc>("SelectPSP_VolumeBalance_CalcByTypeTitle", objtemp);
                foreach (PSP_VolumeBalance_Calc pvm in list)
                {
                    PSP_VolumeBalance_Calc pvc = new PSP_VolumeBalance_Calc();
                    pvc = pvm;
                    pvc.Title = obj.Title;
                 
                    Services.BaseService.Update<PSP_VolumeBalance_Calc>(pvc);
                }
            }
		}

		/// <summary>
		/// 删除焦点对象
		/// </summary>
		public void DeleteObject()
		{
			//获取焦点对象
			PSP_VolumeBalance_Calc obj = FocusedObject;
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
				Services.BaseService.Delete<PSP_VolumeBalance_Calc>(obj);
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
            SaveSum();
			gridControl.RefreshDataSource();
			//设置新的焦点行索引
			GridHelper.FocuseRowAfterDelete(this.gridView, iOldHandle);
			this.gridView.EndUpdate();
            if (obj.Type == "1")
            {
                IList<PSP_VolumeBalance_Calc> list = Services.BaseService.GetList<PSP_VolumeBalance_Calc>("SelectPSP_VolumeBalance_CalcByTypeTitle", obj);
                foreach (PSP_VolumeBalance_Calc pvm in list)
                {
                    PSP_VolumeBalance_Calc pvc = new PSP_VolumeBalance_Calc();
                    pvc = pvm;
                    pvc.Title = pvm.Title;
                    
                    Services.BaseService.Delete<PSP_VolumeBalance_Calc>(pvc);
                    IList<PSP_VolumeBalance_Calc> list1 = Services.BaseService.GetList<PSP_VolumeBalance_Calc>("SelectPSP_VolumeBalance_CalcByTypeFlag", pvm);
                    deleSaveSum(list1, pvc.Flag);
                }
            }
		}
		#endregion
	}
}
