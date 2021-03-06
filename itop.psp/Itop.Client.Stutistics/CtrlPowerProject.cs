
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
#endregion

namespace Itop.Client.Stutistics
{
	public partial class CtrlPowerProject : UserControl
	{
		#region 构造方法
		public CtrlPowerProject()
		{
			InitializeComponent();

            
		}
		#endregion

		#region 字段
		protected bool _bAllowUpdate = true;
        IList<PowerProject> list = new List<PowerProject>();
        DataTable dataTable = new DataTable();
        private string lineuid = "";
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


        public string  LineUID
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
        //public GridControl GridControl
        //{
        //    get { return gridControl; }
        //}

        ///// <summary>
        ///// 获取GridView对象
        ///// </summary>
        //public GridView GridView
        //{
        //    get { return gridView; }
        //}

        public TreeList TreeLists
        {
            get { return treeList1; }
        }

		/// <summary>
		/// 获取GridControl的数据源，即对象的列表
		/// </summary>
        ////public IList<PowerProject> ObjectList
        ////{
        ////    get { return this.gridControl.DataSource as IList<PowerProject>; }
        ////}

        /////// <summary>
        /////// 获取焦点对象，即FocusedRow
        /////// </summary>
        ////public PowerProject FocusedObject
        ////{
        ////    get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PowerProject; }
        ////}
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
                PowerProject pp = new PowerProject();
                pp.PowerLineUID = lineuid;

                list.Clear();
                list = Services.BaseService.GetList<PowerProject>("SelectPowerProjectList", pp);
                dataTable=new DataTable();

                dataTable = DataConverter.ToDataTable((IList)list, typeof(PowerProject));

                //foreach (PowerProject pp in list)
                //{
                //    if (pp.ParentID != "")
                //        pp.StuffName = "   " + pp.StuffName;
                //}


                this.treeList1.DataSource = dataTable;

                this.treeList1.ExpandAll();

                treeList1.MoveFirst();
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

            object objs = Services.BaseService.GetObject("SelectPowerProjectBySortID", parentid);
            if (objs != null)
                count = (int)objs;

            PowerProject obj = new PowerProject();
            obj.SortID = count + 1;
            obj.ParentID = parentid;
            obj.PowerLineUID = lineuid;
            using (FrmPowerProjectDialog dlg = new FrmPowerProjectDialog())
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

            object objs = Services.BaseService.GetObject("SelectPowerProjectBySortID", parentid);
            if (objs != null)
                count = (int)objs;

            PowerProject obj = new PowerProject();
            obj.SortID = count + 1;
            obj.ParentID = parentid;
            obj.PowerLineUID = lineuid;
            using (FrmPowerProjectDialog dlg = new FrmPowerProjectDialog())
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
            PowerProject obj = Services.BaseService.GetOneByKey<PowerProject>(uid);
            PowerProject objCopy = new PowerProject();
            DataConverter.CopyTo<PowerProject>(obj, objCopy);

            FrmPowerProjectDialog dlg = new FrmPowerProjectDialog();
            dlg.Object = objCopy;

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            DataConverter.CopyTo<PowerProject>(objCopy, obj);
            treeList1.FocusedNode.SetValue("StuffName", obj.StuffName);
            treeList1.FocusedNode.SetValue("Lengths", obj.Lengths);
            treeList1.FocusedNode.SetValue("Volume", obj.Volume);
            treeList1.FocusedNode.SetValue("Type", obj.Type);
            treeList1.FocusedNode.SetValue("Total", obj.Total);
            treeList1.FocusedNode.SetValue("PlanStartYear", obj.PlanStartYear);
            treeList1.FocusedNode.SetValue("PlanEndYear", obj.PlanEndYear);







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
                Services.BaseService.DeleteByKey<PowerProject>(uid);
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
            InsertData();

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

            PowerProject obj = new PowerProject();
            obj.SortID = count;
            obj.ParentID = parentid;
            obj.PowerLineUID = lineuid;

            using (FrmPowerProjectDialog dlg = new FrmPowerProjectDialog())
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
