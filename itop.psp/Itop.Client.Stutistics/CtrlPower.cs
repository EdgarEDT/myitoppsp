
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
using DevExpress.XtraGrid.Columns;
#endregion

namespace Itop.Client.Stutistics
{
	public partial class CtrlPower : UserControl
    {
        private string types1 = "";
        private string flags1 = "";
        private string types2 = "";
        private string text = "";
        public bool editright = true;
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
        public string Text
        {

            set { text = value; }
        }

        bool isselect = false;

		#region 构造方法
        public CtrlPower()
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
			get { return gridView1; }
		}

		/// <summary>
		/// 获取GridControl的数据源，即对象的列表
		/// </summary>
		public IList<PSP_PowerSubstationInfo> ObjectList
		{
			get { return this.gridControl.DataSource as IList<PSP_PowerSubstationInfo>; }
		}

		/// <summary>
		/// 获取焦点对象，即FocusedRow
		/// </summary>
		public PSP_PowerSubstationInfo FocusedObject
		{
            get { return this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as PSP_PowerSubstationInfo; }
		}
		#endregion

		#region 事件处理
		private void gridView_DoubleClick(object sender, EventArgs e)
        {
            if (!editright)
            {
                return ;
            }
			// 判断"双击允许修改"标志 
			if (!AllowUpdate)
			{
				return;
			}
            
			//如果鼠标点击在单元格中，则编辑焦点对象。
			Point point = this.gridControl.PointToClient(Control.MousePosition);
            if (GridHelper.HitCell(this.gridView1, point))
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
          
            ComponentPrint.ShowPreview(this.gridControl, this.gridView1.GroupPanelText);
		}

		/// <summary>
		/// 刷新表格中的数据
		/// </summary>
		/// <returns>ture:成功  false:失败</returns>
        public bool RefreshData1()
        {
            try
            {


                IList<PSP_PowerSubstationInfo> list = Services.BaseService.GetList<PSP_PowerSubstationInfo>("SelectPSP_PowerSubstationInfoByFlag", flags1);

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

        public bool RefreshData()
		{
			try
            {

                IList<PSP_PowerSubstationInfo> list = Services.BaseService.GetList<PSP_PowerSubstationInfo>("SelectPSP_PowerSubstationInfoByFlag", flags1);
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
            PSP_PowerSubstationInfo obj = new PSP_PowerSubstationInfo();
           
            obj.Flag = flags1;
            obj.CreateDate = DateTime.Now;

            //执行添加操作
            using (FrmPSP_PowerDialog dlg = new FrmPSP_PowerDialog())
            {
                //dlg.Text = "";
                //dlg.Type = types1;
                dlg.Flag = flags1;
                dlg.Type2 = types2;
                dlg.ctrlPSP_PowerSubstationInfo = this;
                dlg.Text = this.text;
                dlg.IsCreate = true;    //设置新建标志
                dlg.Object = obj;
                dlg.rowTitle.Properties.Caption = this.colTitle.Caption;
                dlg.rowPowerName.Properties.Caption = this.colPowerName.Caption;
                if (dlg.ShowDialog() != DialogResult.OK)
                {

                    return;
                }
            }

            //将新对象加入到链表中
            ObjectList.Add(obj);

            //刷新表格，并将焦点行定位到新对象上。
            gridControl.RefreshDataSource();
            GridHelper.FocuseRow(this.gridView1, obj);
        }

        /// <summary>
        /// 修改焦点对象
        /// </summary>
        public void UpdateObject()
        {
            //获取焦点对象
            PSP_PowerSubstationInfo obj = FocusedObject;
            if (obj == null)
            {
                return;
            }

            //创建对象的一个副本
            PSP_PowerSubstationInfo objCopy = new PSP_PowerSubstationInfo();
            DataConverter.CopyTo<PSP_PowerSubstationInfo>(obj, objCopy);

            //执行修改操作
            using (FrmPSP_PowerDialog dlg = new FrmPSP_PowerDialog())
            {
                dlg.IsSelect = isselect;
                dlg.Type = types1;
                dlg.Flag = flags1;
                dlg.Type2 = types2;
                dlg.Text = this.text;
                dlg.ctrlPSP_PowerSubstationInfo = this;

                dlg.Object = objCopy;   //绑定副本
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            //用副本更新焦点对象
            DataConverter.CopyTo<PSP_PowerSubstationInfo>(objCopy, obj);
            //刷新表格
            gridControl.RefreshDataSource();
        }

        /// <summary>
        /// 删除焦点对象
        /// </summary>
        public void DeleteObject()
        {
            //获取焦点对象
            PSP_PowerSubstationInfo obj = FocusedObject;
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
              //  Services.BaseService.Delete<PSP_PowerSubstationInfo>(obj);
                Services.BaseService.Update("DeletePSP_PowerSubstationInfoByUID", obj.UID);
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return;
            }

            this.gridView1.BeginUpdate();
            //记住当前焦点行索引
            int iOldHandle = this.gridView1.FocusedRowHandle;
            //从链表中删除
            ObjectList.Remove(obj);
            //刷新表格
            gridControl.RefreshDataSource();
            //设置新的焦点行索引
            GridHelper.FocuseRowAfterDelete(this.gridView1, iOldHandle);
            this.gridView1.EndUpdate();
        }
		#endregion


        private void gridView1_Layout(object sender, EventArgs e)
        {    
            //PowerSubstationLine psl = new PowerSubstationLine();
            //psl.Flag = flags1;
            //psl.Type2 = types2;

            //IList<PowerSubstationLine> li = Itop.Client.Common.Services.BaseService.GetList<PowerSubstationLine>("SelectPowerSubstationLineByFlagType2", psl);



            //foreach (PowerSubstationLine psp in li)
            //{
            //    foreach (GridColumn gc in this.gridView1.VisibleColumns)
            //    {
            //        try
            //        {
            //            if (gc.FieldName.Substring(0, 1) == "S")
            //            {
            //                if (gc.FieldName == psp.ClassType)
            //                    {
            //                        psp.Type = gc.VisibleIndex.ToString();

            //                    }
            //            }
            //        }
            //        catch
            //        { }

            //    }
            //    if(psp.Type!="-1")
            //       Itop.Client.Common.Services.BaseService.Update("UpdatePowerSubstationLine", psp);
            //}


        }

	}
}
