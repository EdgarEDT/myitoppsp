using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Itop.Client.Common;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmSubstationCodeSel : FormBase
    {
        public frmSubstationCodeSel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FocusedObject==null)
            {
                MessageBox.Show("请选择记录。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmSubstationCodeSel_Load(object sender, EventArgs e)
        {
            loadData();
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
        public IList<PSP_SubstationMng> ObjectList
        {
            get { return this.gridControl.DataSource as IList<PSP_SubstationMng>; }
        }

        /// <summary>
        /// 获取焦点对象，即FocusedRow
        /// </summary>
        public PSP_SubstationMng FocusedObject
        {
            get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PSP_SubstationMng; }
        }
        #endregion
        public void loadData()
        {
            try
            {
                PSP_SubstationMng sub_par = new PSP_SubstationMng();
                // sub_par.type = par;
                IList<PSP_SubstationMng> list = Services.BaseService.GetList<PSP_SubstationMng>("SelectPSP_SubstationMngList", sub_par);
                this.gridControl.DataSource = list;
            }
            catch (Exception exc)
            {
                //Debug.Fail(exc.Message);
                //HandleException.TryCatch(exc);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}