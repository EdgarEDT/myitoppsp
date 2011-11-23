
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Common;
using Itop.Client.Common;
using Itop.Client.Base;
namespace Itop.DLGH
{
	public partial class FrmbdzTypeDialog : FormBase
	{

		#region 字段
		protected bool _isCreate = false;
        private string typeid = "";

      
		protected PSP_bdz_type _obj = null;
		#endregion

		#region 构造方法
        public FrmbdzTypeDialog()
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
        public string Typeid
        {
            get { return typeid; }
            set { typeid = value; }
        }
		/// <summary>
		/// 所邦定的对象
		/// </summary>
        public PSP_bdz_type Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region 事件处理
		private void FrmglebeTypeDialog_Load(object sender, EventArgs e)
		{
			
            textBox1.Text = Object.Name;
         //   memoEdit1.Text = Object.col2;

           
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
            if (textBox1.Text == "")
            {
                MessageBox.Show("名称不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        
			return true;
		}

		protected bool SaveRecord()
		{
			//创建/修改 对象
			try
			{
                //if (IsCreate)
                //{
                //    _obj.Name = textBox1.Text;
                //  //  _obj.col1 = typeid;
                //   // _obj.col2 = memoEdit1.Text;
                //    Services.BaseService.Create<PSP_bdz_type>(_obj);
                //}
                //else
                //{
                    _obj.Name = textBox1.Text;

                    Services.BaseService.Update<PSP_bdz_type>(_obj);
                //}
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
