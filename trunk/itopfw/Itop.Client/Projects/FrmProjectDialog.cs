
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Itop.Domain.RightManager;
using Itop.Common;
using DevExpress.XtraEditors;
using Itop.Client.Base;
//using Itop.Client.Common;

namespace Itop.Client.Projects
{
	public partial class FrmProjectDialog : FormBase
	{

		#region 字段
		protected bool _isCreate = false;
		protected Project _obj = null;
		#endregion

		#region 构造方法
		public FrmProjectDialog()
		{
			InitializeComponent();
		}
        public FrmProjectDialog(string str)
        {
            InitializeComponent();
            this.Text = "项目管理";
            this.tabPage.Text = "项目管理";
            this.rowProjectName.Properties.Caption = "项目名称";
            this.rowProjectCode.Properties.Caption = "项目说明";

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
		public Project Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region 事件处理
		private void FrmProjectDialog_Load(object sender, EventArgs e)
		{
			IList<Project> list = new List<Project>();
			list.Add(Object);
            string a = Object.Address;
            if (a != "")
            {
                Object.SortID = int.Parse(a);
            }
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
                _obj.Address = _obj.SortID.ToString();
				if (IsCreate)
				{

                    

					Services.BaseService.Create<Project>(_obj);
				}
				else
				{

                    
					Services.BaseService.Update<Project>(_obj);
				}
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
				//HandleException.TryCatch(exc);
				return false ;
			}

			//操作已成功
			return true;
		}
		#endregion

        private void repositoryItemColorEdit1_ColorChanged(object sender, EventArgs e)
        {
            //repositoryItemColorEdit1.ColorText = ((ColorEdit)sender).;
        }
	}
}
