
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Itop.Domain.Stutistic;
using Itop.Common;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using System.Xml;
using Itop.Domain.Stutistics;
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
    public partial class FrmPSP_EachListDialog : FormBase
    {

        #region 字段
        protected bool _isCreate = false;
        protected PSP_EachList _obj = null;
        private bool isp = false;
        private DataTable dt = new DataTable();
        public bool bl = true;
        public bool bbl = true;
        //private string sid = "";
        private bool isjsxm = false;
        #endregion

        public bool IsJSXM
        {
            get { return isjsxm; }
            set { isjsxm = value; }
        }
        #region 构造方法
        public FrmPSP_EachListDialog()
        {
            InitializeComponent();
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// true:创建对象　false:修改对象
        /// </summary>
        public bool IsCreate
        {
            get { return _isCreate; }
            set { _isCreate = value; }
        }
        public bool IsPower
        {
            get { return isp; }
            set { isp = value; }
        }
        /// <summary>
        /// 所邦定的对象
        /// </summary>
        public PSP_EachList Object
        {
            get { return _obj; }
            set { _obj = value; }
        }
        #endregion

        #region 事件处理
        private void FrmPSP_EachListDialog_Load(object sender, EventArgs e)
        {
            IList<PSP_EachList> list = new List<PSP_EachList>();
            list.Add(Object);
            this.vGridControl.DataSource = list;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!InputCheck())
            {
                return;
            }

            if (SaveRecord())
            {
                DialogResult = DialogResult.OK;
            }
        }
        #endregion

        #region 辅助方法
        protected bool InputCheck()
        {
            return true;
        }

        protected bool SaveRecord()
        {
            //创建/修改 对象
            try
            {
                if (IsCreate)
                {
                    Services.BaseService.Create<PSP_EachList>(_obj);
                }
                else
                {
                    Services.BaseService.Update<PSP_EachList>(_obj);
                }
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return false;
            }

            //操作已成功
            return true;
        }
        #endregion
    }
}
