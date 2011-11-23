
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

        #region �ֶ�
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
        #region ���췽��
        public FrmPSP_EachListDialog()
        {
            InitializeComponent();
        }
        #endregion

        #region ��������
        /// <summary>
        /// true:��������false:�޸Ķ���
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
        /// ����Ķ���
        /// </summary>
        public PSP_EachList Object
        {
            get { return _obj; }
            set { _obj = value; }
        }
        #endregion

        #region �¼�����
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

        #region ��������
        protected bool InputCheck()
        {
            return true;
        }

        protected bool SaveRecord()
        {
            //����/�޸� ����
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

            //�����ѳɹ�
            return true;
        }
        #endregion
    }
}
