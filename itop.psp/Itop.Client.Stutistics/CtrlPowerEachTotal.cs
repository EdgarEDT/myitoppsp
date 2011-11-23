
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
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList;
#endregion

namespace Itop.Client.Stutistics
{
	public partial class CtrlPowerEachTotal : UserControl
	{
		#region ���췽��
		public CtrlPowerEachTotal()
		{
			InitializeComponent();
		}
		#endregion

		#region �ֶ�
		protected bool _bAllowUpdate = true;
        string lineuid = "";
        DataTable dataTable = new DataTable();
        IList<PowerEachTotal> list = new List<PowerEachTotal>();
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
        public TreeList ZHJ
        {
            get { return treeList1; }
            set { treeList1 = value; }
        }
		/// <summary>
		/// ��ȡGridControl����
		/// </summary>
        //public GridControl GridControl
        //{
        //    get { return gridControl; }
        //}

        ///// <summary>
        ///// ��ȡGridView����
        ///// </summary>
        //public GridView GridView
        //{
        //    get { return gridView; }
        //}

		/// <summary>
		/// ��ȡGridControl������Դ����������б�
		/// </summary>
        //public IList<PowerEachTotal> ObjectList
        //{
        //    get { return this.gridControl.DataSource as IList<PowerEachTotal>; }
        //}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
        //public PowerEachTotal FocusedObject
        //{
        //    get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PowerEachTotal; }
        //}
		#endregion

		#region �¼�����
		private void gridView_DoubleClick(object sender, EventArgs e)
		{
            //// �ж�"˫�������޸�"��־ 
            //if (!AllowUpdate)
            //{
            //    return;
            //}

            ////���������ڵ�Ԫ���У���༭�������
            //Point point = this.gridControl.PointToClient(Control.MousePosition);
            //if (GridHelper.HitCell(this.gridView, point))
            //{
            //    UpdateObject();
            //}

		}
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
                PowerEachTotal pet = new PowerEachTotal();
                pet.PowerLineUID = lineuid;
                list.Clear();
                dataTable = new DataTable();
                list = Services.BaseService.GetList<PowerEachTotal>("SelectPowerEachTotalList", pet);
                dataTable = DataConverter.ToDataTable((IList)list, typeof(PowerEachTotal));
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

            object objs = Services.BaseService.GetObject("SelectPowerEachTotalBySortID", parentid);
            if (objs != null)
                count = (int)objs;

            //PowerEachTotal obj = new PowerEachTotal();
            //obj.SortID = count + 1;
            //obj.ParentID = parentid;
            //obj.PowerLineUID = lineuid;

            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "������Ŀ";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                PowerEachTotal obj = new PowerEachTotal();
                obj.UID = Guid.NewGuid().ToString();
                obj.SortID = count + 1;
                obj.ParentID = parentid;
                obj.PowerLineUID = lineuid;
                obj.StuffName = frm.TypeTitle;



                
                try
                {
                    Services.BaseService.Create<PowerEachTotal>(obj);
                    dataTable.Rows.Add(DataConverter.ObjectToRow(obj, dataTable.NewRow()));
                    //psp_Type.ID = (int)Common.Services.BaseService.Create("InsertPowerTypes", psp_Type);
                    //dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));
                }
                catch (Exception ex)
                {
                    MsgBox.Show("������Ŀ����" + ex.Message);
                }
            }




            //using (FrmPowerEachTotalDialog dlg = new FrmPowerEachTotalDialog())
            //{
            //    dlg.IsCreate = true;    //�����½���־
            //    dlg.Object = obj;
            //    if (dlg.ShowDialog() != DialogResult.OK)
            //    {
            //        return;
            //    }
            //}

            //dataTable.Rows.Add(DataConverter.ObjectToRow(obj, dataTable.NewRow()));

		}


        /// <summary>
        /// ��Ӷ���
        /// </summary>
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

            object objs = Services.BaseService.GetObject("SelectPowerEachTotalBySortID", parentid);
            if (objs != null)
                count = (int)objs;


            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "������Ŀ";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                PowerEachTotal obj = new PowerEachTotal();
                obj.SortID = count + 1;
                obj.ParentID = parentid;
                obj.PowerLineUID = lineuid;
                obj.StuffName = frm.TypeTitle;

                try
                {
                    Services.BaseService.Create<PowerEachTotal>(obj);
                    dataTable.Rows.Add(DataConverter.ObjectToRow(obj, dataTable.NewRow()));
                    //psp_Type.ID = (int)Common.Services.BaseService.Create("InsertPowerTypes", psp_Type);
                    //dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));
                }
                catch (Exception ex)
                {
                    MsgBox.Show("������Ŀ����" + ex.Message);
                }
            }


            //////PowerEachTotal obj = new PowerEachTotal();
            //////obj.SortID = count + 1;
            //////obj.ParentID = parentid;
            //////obj.PowerLineUID = lineuid;
            //////using (FrmPowerEachTotalDialog dlg = new FrmPowerEachTotalDialog())
            //////{
            //////    dlg.IsCreate = true;    //�����½���־
            //////    dlg.Object = obj;
            //////    if (dlg.ShowDialog() != DialogResult.OK)
            //////    {
            //////        return;
            //////    }
            //////}
            //////dataTable.Rows.Add(DataConverter.ObjectToRow(obj, dataTable.NewRow()));

        }

		/// <summary>
		/// �޸Ľ������
		/// </summary>
		public void UpdateObject()
		{



            if (treeList1.FocusedNode == null)
                return;

            string uid = treeList1.FocusedNode["UID"].ToString();
            PowerEachTotal obj = Services.BaseService.GetOneByKey<PowerEachTotal>(uid);
            //PowerEachTotal objCopy = new PowerEachTotal();
            //DataConverter.CopyTo<PowerEachTotal>(obj, objCopy);

            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "�޸���Ŀ";
            frm.TypeTitle = obj.StuffName;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                obj.StuffName = frm.TypeTitle;
                try
                {
                    Services.BaseService.Update<PowerEachTotal>(obj);
                    treeList1.FocusedNode.SetValue("StuffName", obj.StuffName);
                    //dataTable.Rows.Add(DataConverter.ObjectToRow(obj, dataTable.NewRow()));
                    //psp_Type.ID = (int)Common.Services.BaseService.Create("InsertPowerTypes", psp_Type);
                    //dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));
                }
                catch (Exception ex)
                {
                    MsgBox.Show("������Ŀ����" + ex.Message);
                }
            }




            //////Services.BaseService.Update<PowerEachTotal>(obj);


            //////FrmPowerEachTotalDialog dlg = new FrmPowerEachTotalDialog();
            //////dlg.Object = objCopy;

            //////if (dlg.ShowDialog() != DialogResult.OK)
            //////{
            //////    return;
            //////}

            //DataConverter.CopyTo<PowerEachTotal>(objCopy, obj);
            //treeList1.FocusedNode.SetValue("StuffName", obj.StuffName);
            //treeList1.FocusedNode.SetValue("LCount", obj.LCount);
            //treeList1.FocusedNode.SetValue("Lengths", obj.Lengths);
            //treeList1.FocusedNode.SetValue("Volume", obj.Volume);
            //treeList1.FocusedNode.SetValue("Type", obj.Type);
            //treeList1.FocusedNode.SetValue("Total", obj.Total);
            //treeList1.FocusedNode.SetValue("Remark", obj.Remark);
            //treeList1.FocusedNode.SetValue("IsSum", obj.IsSum);
            //treeList1.FocusedNode.SetValue("ItSum", obj.ItSum);


		
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
                Services.BaseService.DeleteByKey<PowerEachTotal>(uid);
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

            PowerEachTotal obj = new PowerEachTotal();
            obj.SortID = count;
            obj.ParentID = parentid;
            obj.PowerLineUID = lineuid;

            using (FrmPowerEachTotalDialog dlg = new FrmPowerEachTotalDialog())
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
            this.treeList1.DataSource = dataTable;
        
        }

        public void InitGrid()
        {
            FormClass fc = new FormClass();
            gridControl1.DataSource = fc.ConvertTreeListToDataTable(treeList1);
        }

        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
           // if ((e.Value.ToString() != lastEditValue
           //    || lastEditNode != e.Node
           //    || lastEditColumn != e.Column)
           //     //&& e.Column.FieldName.IndexOf("��") > 0
           //     //&& e.Column.Tag != null
           //)
           // {

           //     if (e.Column.FieldName != "Lixi" && e.Column.FieldName != "Yubei" && e.Column.FieldName != "Dongtai" && e.Column.FieldName != "Dongtai")
           //     {
           //         SaveCellValue((int)treeList1.FocusedColumn.Tag, (int)treeList1.FocusedNode.GetValue("ID"), Convert.ToDouble(e.Value));
           //     }
           //     else
           //     {
           //         int id = (int)e.Node["ID"];

           //         SaveCellValue(e.Column.FieldName, id, Convert.ToDouble(e.Value));
           //     }
           //     //

            if (e.Column.FieldName == "Lengths" || e.Column.FieldName == "LCount" || e.Column.FieldName == "Total" || e.Column.FieldName == "Volume" || e.Column.FieldName == "Type" || e.Column.FieldName == "IsSum" || e.Column.FieldName == "ItSum")
            {
                SaveValue(e.Node["UID"].ToString(), e.Node["Lengths"].ToString(), e.Node["LCount"].ToString(), e.Node["Total"].ToString(), e.Node["Volume"].ToString(), e.Node["Type"].ToString(), e.Node["IsSum"].ToString(), e.Node["ItSum"].ToString());
                CalculateSum(e.Node, e.Column);
            }

            //ItSum

           //     //}
           //     //else
           //     //{
           //     //    treeList1.FocusedNode.SetValue(treeList1.FocusedColumn.FieldName, lastEditValue);
           //     //}
           // }

           // CalculateNodeSum(e.Node);
        }


        private void SaveValue(string uid, string lengths, string lcount, string total, string volume, string type, string issum, string itsum)
        {
            PowerEachTotal pet = Services.BaseService.GetOneByKey<PowerEachTotal>(uid);
            pet.Lengths = lengths;
            pet.LCount = lcount;
            pet.Total = total;
            pet.Volume = volume;
            pet.Type = type;
            if(issum!=string.Empty)
                pet.IsSum = double.Parse(issum);
            if (itsum != string.Empty)
                pet.ItSum = double.Parse(itsum);
            Services.BaseService.Update<PowerEachTotal>(pet);
        
        }









        //////////���㾲̬��Ͷ��
        ////////private void CalculateNodeSum(TreeListNode node)
        ////////{
        ////////    double sum = 0.0;
        ////////    foreach (TreeListColumn col in treeList1.Columns)
        ////////    {
        ////////        if (col.Caption.IndexOf("��") > 0)
        ////////        {
        ////////            try
        ////////            {
        ////////                sum += (double)node[col.FieldName];
        ////////            }
        ////////            catch { }
        ////////        }
        ////////    }
        ////////    node["JingTai"] = sum;

        ////////    if (node.ParentNode != null)
        ////////    {
        ////////        CalculateNodeSum(node.ParentNode);
        ////////    }
        ////////}

        //���ӷ������ݸı�ʱ�������丸�����ֵ
        private void CalculateSum(TreeListNode node, TreeListColumn column)
        {
            TreeListNode parentNode = node.ParentNode;

            if (parentNode == null)
            {
                return;
            }

            double sum = 0;
            foreach (TreeListNode nd in parentNode.Nodes)
            {
                object value = nd.GetValue(column.FieldName);
                if (value != null && value != DBNull.Value && value.ToString() != "")
                {
                    sum += Convert.ToDouble(value);
                }
            }

            parentNode.SetValue(column.FieldName, sum);
            SaveValue(parentNode["UID"].ToString(), parentNode["Lengths"].ToString(), parentNode["LCount"].ToString(), parentNode["Total"].ToString(), parentNode["Volume"].ToString(), parentNode["Type"].ToString(), parentNode["IsSum"].ToString(), parentNode["ItSum"].ToString());
        ////////    if (column.FieldName != "Lixi" && column.FieldName != "Yubei" && column.FieldName != "Dongtai")
        ////////    {
        ////////        SaveCellValue((int)column.Tag, (int)parentNode.GetValue("ID"), sum);
        ////////    }
        ////////    else
        ////////    {
        ////////        SaveCellValue(column.FieldName, (int)parentNode.GetValue("ID"), sum);
        ////////    }

            CalculateSum(parentNode, column);
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (treeList1.FocusedNode.HasChildren)
            {
                e.Cancel = true;
            }
        }

        private InputLanguage oldInput = null;
        private void treeList1_FocusedColumnChanged(object sender, DevExpress.XtraTreeList.FocusedColumnChangedEventArgs e)
        {
            try
            {
                DevExpress.XtraEditors.Repository.RepositoryItemTextEdit edit = e.Column.ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
                if (edit != null && edit.Mask.MaskType == DevExpress.XtraEditors.Mask.MaskType.Numeric)
                {
                    oldInput = InputLanguage.CurrentInputLanguage;
                    InputLanguage.CurrentInputLanguage = null;
                }
                else
                {
                    if (oldInput != null && oldInput != InputLanguage.CurrentInputLanguage)
                    {
                        InputLanguage.CurrentInputLanguage = oldInput;
                    }
                }
            }
            catch { }
        }

	}
}
