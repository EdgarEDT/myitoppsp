
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Itop.Common;
using Itop.Client.Common;
using Itop.Domain.Forecast;
using Itop.Client.Base;
namespace Itop.Client.Forecast
{
	public partial class FrmBaseColorDialog : FormBase
	{

		#region 字段
		protected bool _isCreate = false;
		protected FORBaseColor _obj = null;
		#endregion

		#region 构造方法
		public FrmBaseColorDialog()
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
		public FORBaseColor Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region 事件处理
		private void FrmBaseColorDialog_Load(object sender, EventArgs e)
		{
			IList<FORBaseColor> list = new List<FORBaseColor>();
			list.Add(Object);
			this.vGridControl.DataSource = list;

            try
            {
                Color cl = ColorTranslator.FromOle(_obj.Color);
                _obj.Color1 = cl;
            }
            catch { }

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
            if (_obj.MinValue > _obj.MaxValue)
            {
                MsgBox.Show("最小值不能大于最大值");
                return false;
            }

            IList<FORBaseColor> lis = Services.BaseService.GetStrongList<FORBaseColor>();
            foreach (FORBaseColor bc in lis)
            {
                if ((_obj.MinValue > bc.MinValue && _obj.MinValue < bc.MaxValue) || _obj.MaxValue > bc.MinValue && _obj.MaxValue < bc.MaxValue)
                {
                    MsgBox.Show("该区间部分区域重复，请重新输入！");
                    return false;
                }
            
            }



            return true;
        }

		protected bool SaveRecord()
		{
			//创建/修改 对象
			try
			{
				if (IsCreate)
				{
					Services.BaseService.Create<FORBaseColor>(_obj);
				}
				else
				{
					Services.BaseService.Update<FORBaseColor>(_obj);
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

        private void repositoryItemColorEdit1_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit tx = (DevExpress.XtraEditors.TextEdit)sender;
            if (tx.Text.Length > 0)
            { 
                ColorConverter cc = new ColorConverter();
                Color obj= (Color)cc.ConvertFromInvariantString(tx.Text);
                _obj.Color = ColorTranslator.ToOle(obj);
            }


        }
	}
}
