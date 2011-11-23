using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections;
using Itop.Common;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmLineList1 : FormBase
    {
        private string eleid = "";
        public ArrayList list = new ArrayList();

        public string Eleid
        {
            get { return eleid; }
            set { eleid = value; }
        }


        public frmLineList1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            list.Clear();
            if(gridView.GetSelectedRows().Length==0){
                MessageBox.Show("请选择记录。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //if(gridView.GetSelectedRows().Length>5){
            //    MessageBox.Show("每次最多选择5条记录。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            int[] a=gridView.GetSelectedRows();
            for (int i = 0; i < a.Length;i++ )
            {
                list.Add(gridView.GetRowCellValue(i, "UID").ToString());
                //MessageBox.Show(gridView.GetRowCellValue(i,"UID").ToString());
            }
            frmPengFenLine f1 = new frmPengFenLine();
            f1.SelList = list;
            f1.PID = eleid;
            f1.ShowDialog();
        }

        private void frmLineList1_Load(object sender, EventArgs e)
        {
            RefreshData();
        }
        #region 公共属性

        /// <summary>
        /// 获取或设置"双击允许修改"标志
        /// </summary>
        //public bool AllowUpdate
        //{
        //    get { return _bAllowUpdate; }
        //    set { _bAllowUpdate = value; }
        //}

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
        public IList<LineList1> ObjectList
        {
            get { return this.gridControl.DataSource as IList<LineList1>; }
        }

        /// <summary>
        /// 获取焦点对象，即FocusedRow
        /// </summary>
        public LineList1 FocusedObject
        {
            get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as LineList1; }
        }
        #endregion

        public void UpdateObject()
        {
            //获取焦点对象
            LineList1 obj = FocusedObject;
            if (obj == null)
            {
                return;
            }

            //创建对象的一个副本

            LineList1 objCopy = new LineList1();
            DataConverter.CopyTo<LineList1>(obj, objCopy);

            //执行修改操作
            using (FrmLineList1Dialog dlg = new FrmLineList1Dialog())
            {
                dlg.Object = objCopy;   //绑定副本
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            //用副本更新焦点对象

            DataConverter.CopyTo<LineList1>(objCopy, obj);
            //刷新表格
            gridControl.RefreshDataSource();
        }
        public bool RefreshData()
        {
            try
            {
                LineList1 line = new LineList1();
                line.col1 = eleid;
                IList<LineList1> list = Services.BaseService.GetList<LineList1>("SelectLineList1ByRefLineEleID", line);
            
                this.gridControl.DataSource = list;
            }
            catch (Exception exc)
            {
                //Debug.Fail(exc.Message);
                //HandleException.TryCatch(exc);
                return false;
            }

            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridControl_DoubleClick(object sender, EventArgs e)
        {
            if (FocusedObject == null)
            {
                return;
            }
            UpdateObject();
        }
    }
}