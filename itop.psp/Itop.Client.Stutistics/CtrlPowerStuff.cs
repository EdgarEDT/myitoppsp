
#region ���������ռ�
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
		#region ���췽��
		public CtrlPowerStuff()
		{
			InitializeComponent();
		}
		#endregion

		#region �ֶ�
		protected bool _bAllowUpdate = true;
        string lineuid = "";
        IList<PowerStuff> ilist = new List<PowerStuff>();
        DataTable dataTable = new DataTable();
        string lineName = "";
		#endregion

		#region ��������
		/// <summary>
		/// ��ȡ������"˫�������޸�"��־
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
		/// ��ȡGridControl����
		/// </summary>
        public TreeList TreeLists
		{
            get { return treeList1; }
		}

		/// <summary>
		/// ��ȡGridView����
		/// </summary>
        //public GridView GridView
        //{
        //    get { return gridView; }
        //}

		/// <summary>
		/// ��ȡGridControl������Դ����������б�
		/// </summary>
        //public IList<PowerStuff> ObjectList
        //{
        //    get { return this.gridControl.DataSource as IList<PowerStuff>; }
        //}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
        //public  FocusedObject
        //{
        //    get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PowerStuff; }
        //}
		#endregion

		#region �¼�����
        //private void gridView_DoubleClick(object sender, EventArgs e)
        //{
        //    // �ж�"˫�������޸�"��־ 
        //    if (!AllowUpdate)
        //    {
        //        return;
        //    }

        //    //���������ڵ�Ԫ���У���༭�������
        //    Point point = this.gridControl.PointToClient(Control.MousePosition);
        //    if (GridHelper.HitCell(this.gridView, point))
        //    {
        //        UpdateObject();
        //    }

        //}
		#endregion

		#region ��������
		/// <summary>
		/// ��ӡԤ��
		/// </summary>
		public void PrintPreview()
		{
            ComponentPrint.ShowPreview(this.treeList1, lineName);
		}

		/// <summary>
		/// ˢ�±���е�����
		/// </summary>
		/// <returns>ture:�ɹ�  false:ʧ��</returns>
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
		/// ��Ӷ���
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
                dlg.IsCreate = true;    //�����½���־
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
                dlg.IsCreate = true;    //�����½���־
                dlg.Object = obj;
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }
            dataTable.Rows.Add(DataConverter.ObjectToRow(obj, dataTable.NewRow()));
        }

		/// <summary>
		/// �޸Ľ������
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
		/// ɾ���������
		/// </summary>
		public void DeleteObject()
		{
            if (treeList1.FocusedNode == null)
                return;

            if (treeList1.FocusedNode.Nodes.Count > 0)
            {
                MsgBox.Show("���¼�Ŀ¼������ɾ����");
                return;
            }
            string uid = treeList1.FocusedNode["UID"].ToString();

            //����ȷ��
            if (MsgBox.ShowYesNo(Strings.SubmitDelete) != DialogResult.Yes)
            {
                return;
            }

            //ִ��ɾ������
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

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
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
                dlg.IsCreate = true;    //�����½���־
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
                dlg.IsCreate = true;    //�����½���־
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
