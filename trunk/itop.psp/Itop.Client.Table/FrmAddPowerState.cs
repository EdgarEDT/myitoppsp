
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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraVerticalGrid.Rows;
using Itop.Domain.Table;
using Itop.Client.Stutistics;
using Itop.Client.Base;
namespace Itop.Client.Table
{
	public partial class FrmAddPowerState : FormBase
	{
        private FrmPowerState area;
        public FrmPowerState Area
        {
            set { area = value; }
        }
        private string types1 = "";
        private string flags1 = "";
        public string Type
        {
            get { return types1; }
            set { types1 = value; }       
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

        bool isselect = false;



		#region 字段
		protected bool _isCreate = false;
		protected PSP_PowerSubstationInfo _obj = null;
		#endregion

		#region 构造方法
        public FrmAddPowerState()
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

		/// <summary>
		/// 所邦定的对象
		/// </summary>
        public PSP_PowerSubstationInfo Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region 事件处理
		private void FrmSubstation_InfoDialog_Load(object sender, EventArgs e)
		{
            if (isselect)
                this.vGridControl.Enabled = false;

            if (!IsCreate)
            {
                checkEdit1.Visible = false;
            }
            if (flags1 == "1")
                this.vGridControl.Rows["editorRow8"].Visible = false;

            IList<PSP_PowerSubstationInfo> list = new List<PSP_PowerSubstationInfo>();
			list.Add(_obj);
			this.vGridControl.DataSource = list;

		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (SaveRecord())
			{
                if (checkEdit1.Checked)
                {
                    area.InitGrid1();
                    _obj = new PSP_PowerSubstationInfo();
                    _obj.UID = Guid.NewGuid().ToString();
                    IList<PSP_PowerSubstationInfo> list1 = new List<PSP_PowerSubstationInfo>();
                    list1.Add(_obj);
                    this.vGridControl.DataSource = list1;
                }
                else
                {
                    DialogResult = DialogResult.OK;
                }
			}
		}
		#endregion

		#region 辅助方法

      
        private string projectid,parentid;
        public string ProjectID
        {
            set { projectid = value; }
        }
        public string ParentID
        {
            set { parentid = value; }
        }
		protected bool SaveRecord()
		{
			try
			{
				if (IsCreate)
				{
                    _obj.UID += "|" + projectid;
                    _obj.AreaID = projectid;
                    _obj.CreateDate = DateTime.Now;
                    _obj.Flag = flags1;
                    Services.BaseService.Create<PSP_PowerSubstationInfo>(_obj);
				}
				else
				{
                    Services.BaseService.Update<PSP_PowerSubstationInfo>(_obj);
				}
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
				HandleException.TryCatch(exc);
				return false ;
			}

			//操作已成功
			return true;
		}
		#endregion

        

    }
}
