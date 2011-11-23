
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
using System.Collections;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
#endregion

namespace Itop.Client.Stutistics
{
	public partial class CtrlPowerStuff : UserControl
	{
		#region 构造方法
		public CtrlPowerStuff()
		{
			InitializeComponent();
		}
		#endregion

		#region 字段
		protected bool _bAllowUpdate = true;
        string lineuid = "";
        IList<PowerStuff> ilist = new List<PowerStuff>();
        DataTable dataTable = new DataTable();
        string lineName = "";
		#endregion

		#region 公共属性
		/// <summary>
		/// 获取或设置"双击允许修改"标志
		/// </summary>
        /// 

        public string LineName
        {
            get { return lineName; }
            set { lineName = value; }
        }

        public string LineUID
        {
            get { return lineuid; }
            set { lineuid = value; }
        }


		public bool AllowUpdate
		{
			get { return _bAllowUpdate; }
			set { _bAllowUpdate = value; }
		}

		/// <summary>
		/// 获取GridControl对象
		/// </summary>
        public TreeList TreeLists
		{
            get { return treeList1; }
		}

		/// <summary>
		/// 获取GridView对象
		/// </summary>
        //public GridView GridView
        //{
        //    get { return gridView; }
        //}

		/// <summary>
		/// 获取GridControl的数据源，即对象的列表
		/// </summary>
        //public IList<PowerStuff> ObjectList
        //{
        //    get { return this.gridControl.DataSource as IList<PowerStuff>; }
        //}

		/// <summary>
		/// 获取焦点对象，即FocusedRow
		/// </summary>
        //public  FocusedObject
        //{
        //    get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PowerStuff; }
        //}
		#endregion

		#region 事件处理
        //private void gridView_DoubleClick(object sender, EventArgs e)
        //{
        //    // 判断"双击允许修改"标志 
        //    if (!AllowUpdate)
        //    {
        //        return;
        //    }

        //    //如果鼠标点击在单元格中，则编辑焦点对象。
        //    Point point = this.gridControl.PointToClient(Control.MousePosition);
        //    if (GridHelper.HitCell(this.gridView, point))
        //    {
        //        UpdateObject();
        //    }

        //}
		#endregion

		#region 公共方法
		/// <summary>
		/// 打印预览
		/// </summary>
		public void PrintPreview()
		{
            ComponentPrint.ShowPreview(this.treeList1, lineName);
		}

		/// <summary>
		/// 刷新表格中的数据
		/// </summary>
		/// <returns>ture:成功  false:失败</returns>
		public bool RefreshData()
		{
			try
			{
                try
                {
                    ilist.Clear();
                    PowerStuff ps = new PowerStuff();
                    ps.PowerLineUID = lineuid;

                    ilist = Services.BaseService.GetList<PowerStuff>("SelectPowerStuffList", ps);
                }
                catch (Exception ex) { MsgBox.Show(ex.Message); }
                dataTable = new DataTable();
                dataTable = DataConverter.ToDataTable((IList)ilist, typeof(PowerStuff));
                treeList1.DataSource = dataTable;
                this.treeList1.ExpandAll();

                treeList1.MoveFirst();

         
                //this.treeList1.DataSource = dataTable;
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
            int count = 0;
            string parentid = "";
            if (treeList1.FocusedNode != null)
            {
                parentid = treeList1.FocusedNode["ParentID"].ToString();
            }

            object objs = Services.BaseService.GetObject("SelectPowerStuffBySortID", parentid);
            if (objs != null)
                count = (int)objs;

            PowerStuff obj = new PowerStuff();
            obj.SortID= count + 1;
            obj.ParentID = parentid;
            obj.PowerLineUID = lineuid;
            using (FrmPowerStuffDialog dlg = new FrmPowerStuffDialog())
            {
                dlg.IsCreate = true;    //设置新建标志
                dlg.Object = obj;
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            dataTable.Rows.Add(DataConverter.ObjectToRow(obj, dataTable.NewRow()));
		}



        public void AddObject1()
        {

            int count = 0;
            string parentid = "";
            if (treeList1.FocusedNode == null)
                return;

            if (treeList1.FocusedNode != null)
            {
                parentid = treeList1.FocusedNode["UID"].ToString();
            }

            object objs = Services.BaseService.GetObject("SelectPowerStuffBySortID", parentid);
            if (objs != null)
                count = (int)objs;

            PowerStuff obj = new PowerStuff();
            obj.SortID = count + 1;
            obj.ParentID = parentid;
            obj.PowerLineUID = lineuid;
            using (FrmPowerStuffDialog dlg = new FrmPowerStuffDialog())
            {
                dlg.IsCreate = true;    //设置新建标志
                dlg.Object = obj;
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }
            dataTable.Rows.Add(DataConverter.ObjectToRow(obj, dataTable.NewRow()));
        }

		/// <summary>
		/// 修改焦点对象
		/// </summary>
		public void UpdateObject()
		{
            if (treeList1.FocusedNode == null)
                return;

            string uid = treeList1.FocusedNode["UID"].ToString();
            PowerStuff obj = Services.BaseService.GetOneByKey<PowerStuff>(uid);
            PowerStuff objCopy = new PowerStuff();
            DataConverter.CopyTo<PowerStuff>(obj, objCopy);

            FrmPowerStuffDialog dlg = new FrmPowerStuffDialog();
            dlg.Object = objCopy;

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            DataConverter.CopyTo<PowerStuff>(objCopy, obj);
            treeList1.FocusedNode.SetValue("StuffName", obj.StuffName);
            treeList1.FocusedNode.SetValue("Lengths", obj.Lengths);
            treeList1.FocusedNode.SetValue("Volume", obj.Volume);
            treeList1.FocusedNode.SetValue("Type", obj.Type);
            treeList1.FocusedNode.SetValue("Total", obj.Total);
            treeList1.FocusedNode.SetValue("Remark", obj.Remark);


		}

		/// <summary>
		/// 删除焦点对象
		/// </summary>
		public void DeleteObject()
		{
            if (treeList1.FocusedNode == null)
                return;

            if (treeList1.FocusedNode.Nodes.Count > 0)
            {
                MsgBox.Show("有下级目录，不能删除！");
                return;
            }
            string uid = treeList1.FocusedNode["UID"].ToString();

            //请求确认
            if (MsgBox.ShowYesNo(Strings.SubmitDelete) != DialogResult.Yes)
            {
                return;
            }

            //执行删除操作
            try
            {
                Services.BaseService.DeleteByKey<PowerStuff>(uid);
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return;
            }
            this.treeList1.Nodes.Remove(this.treeList1.FocusedNode);

		}
		#endregion

        private void 插入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = 0;
            string parentid = "";
            if (treeList1.FocusedNode == null)
                return;

            if (treeList1.FocusedNode != null)
            {
                parentid = treeList1.FocusedNode["ParentID"].ToString();
                count = int.Parse(treeList1.FocusedNode["SortID"].ToString());
            }

            //object objs = Services.BaseService.GetObject("SelectPowerProjectBySortID", parentid);
            //if (objs != null)
            //    count = (int)objs;

            PowerStuff obj = new PowerStuff();
            obj.SortID = count;
            obj.ParentID = parentid;
            obj.PowerLineUID = lineuid;

            using (FrmPowerStuffDialog dlg = new FrmPowerStuffDialog())
            {
                dlg.IsCreate = true;    //设置新建标志
                dlg.Object = obj;
                dlg.IsInsert = true;
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }
            dataTable.Rows.Add(DataConverter.ObjectToRow(obj, dataTable.NewRow()));

            
        }



        public void InsertData()
        {
            int count = 0;
            string parentid = "";
            if (treeList1.FocusedNode == null)
                return;

            if (treeList1.FocusedNode != null)
            {
                parentid = treeList1.FocusedNode["ParentID"].ToString();
                count = int.Parse(treeList1.FocusedNode["SortID"].ToString());
            }

            //object objs = Services.BaseService.GetObject("SelectPowerProjectBySortID", parentid);
            //if (objs != null)
            //    count = (int)objs;

            PowerStuff obj = new PowerStuff();
            obj.SortID = count;
            obj.ParentID = parentid;
            obj.PowerLineUID = lineuid;

            using (FrmPowerStuffDialog dlg = new FrmPowerStuffDialog())
            {
                dlg.IsCreate = true;    //设置新建标志
                dlg.Object = obj;
                dlg.IsInsert = true;
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }
            dataTable.Rows.Add(DataConverter.ObjectToRow(obj, dataTable.NewRow()));

            treeList1.DataSource = dataTable;
            
        }


        

        public void InitGrid()
        {
            FormClass fc = new FormClass();
            gridControl1.DataSource = fc.ConvertTreeListToDataTable(treeList1);
        }







    
	}
}
