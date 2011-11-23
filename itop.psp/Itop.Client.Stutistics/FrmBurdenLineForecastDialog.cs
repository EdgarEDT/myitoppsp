
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
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
	public partial class FrmBurdenLineForecastDialog : FormBase
	{

		#region 字段
		protected bool _isCreate = false;
		protected BurdenLineForecast _obj = null;
		#endregion

		#region 构造方法
		public FrmBurdenLineForecastDialog()
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
		public BurdenLineForecast Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region 事件处理
		private void FrmBurdenLineForecastDialog_Load(object sender, EventArgs e)
		{
			IList<BurdenLineForecast> list = new List<BurdenLineForecast>();
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

                if (_obj.BurdenYear == 0)
                {
                    MsgBox.Show("请添加年份!");
                    return false;

                }

                if (_isCreate)
                {
                    BurdenLineForecast bl = new BurdenLineForecast();
                    bl.BurdenYear = _obj.BurdenYear;
                    if ((int)Services.BaseService.GetObject("SelectBurdenLineForecastByYear", bl) > 0)
                    {
                        MsgBox.Show("已存在相同日期，请重新选择!");
                        return false;
                    }
                }


				if (IsCreate)
				{
					Services.BaseService.Create<BurdenLineForecast>(_obj);
				}
				else
				{
					Services.BaseService.Update<BurdenLineForecast>(_obj);
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



        private void ItemTextEditBurdenYear_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit tx = (DevExpress.XtraEditors.TextEdit)sender;

            int yeardata = 0;
            try
            {
                yeardata = int.Parse(tx.Text);
            }
            catch { }
            double powerdata = 0;
            double t1 = 0;
            double t2 = 0;
            try
            {
                powerdata = (double)Services.BaseService.GetObject("SelectPowerData", yeardata);
            }
            catch { }

            BurdenLine bl = new BurdenLine();
            try
            {
                bl = new BurdenLine();
                bl.UID = yeardata.ToString();
                bl.Season = "夏季";
                IList<BurdenLine> li1 = Services.BaseService.GetList<BurdenLine>("SelectBurdenLineBySeasonandDate", bl);
                foreach (BurdenLine bl1 in li1)
                {
                    t1 = Math.Max(t1, bl1.Hour1);
                    t1 = Math.Max(t1, bl1.Hour2);
                    t1 = Math.Max(t1, bl1.Hour3);
                    t1 = Math.Max(t1, bl1.Hour4);
                    t1 = Math.Max(t1, bl1.Hour5);
                    t1 = Math.Max(t1, bl1.Hour6);
                    t1 = Math.Max(t1, bl1.Hour7);
                    t1 = Math.Max(t1, bl1.Hour8);
                    t1 = Math.Max(t1, bl1.Hour9);
                    t1 = Math.Max(t1, bl1.Hour10);
                    t1 = Math.Max(t1, bl1.Hour11);
                    t1 = Math.Max(t1, bl1.Hour12);
                    t1 = Math.Max(t1, bl1.Hour13);
                    t1 = Math.Max(t1, bl1.Hour14);
                    t1 = Math.Max(t1, bl1.Hour15);
                    t1 = Math.Max(t1, bl1.Hour16);
                    t1 = Math.Max(t1, bl1.Hour17);
                    t1 = Math.Max(t1, bl1.Hour18);
                    t1 = Math.Max(t1, bl1.Hour19);
                    t1 = Math.Max(t1, bl1.Hour20);
                    t1 = Math.Max(t1, bl1.Hour21);
                    t1 = Math.Max(t1, bl1.Hour22);
                    t1 = Math.Max(t1, bl1.Hour23);
                    t1 = Math.Max(t1, bl1.Hour24);
                }

            }
            catch { }

            try
            {
                bl = new BurdenLine();
                bl.UID = yeardata.ToString();
                bl.Season = "冬季";
                IList<BurdenLine> li2 = Services.BaseService.GetList<BurdenLine>("SelectBurdenLineBySeasonandDate", bl);
                foreach (BurdenLine bl2 in li2)
                {
                    t2 = Math.Max(t2, bl2.Hour1);
                    t2 = Math.Max(t2, bl2.Hour2);
                    t2 = Math.Max(t2, bl2.Hour3);
                    t2 = Math.Max(t2, bl2.Hour4);
                    t2 = Math.Max(t2, bl2.Hour5);
                    t2 = Math.Max(t2, bl2.Hour6);
                    t2 = Math.Max(t2, bl2.Hour7);
                    t2 = Math.Max(t2, bl2.Hour8);
                    t2 = Math.Max(t2, bl2.Hour9);
                    t2 = Math.Max(t2, bl2.Hour10);
                    t2 = Math.Max(t2, bl2.Hour11);
                    t2 = Math.Max(t2, bl2.Hour12);
                    t2 = Math.Max(t2, bl2.Hour13);
                    t2 = Math.Max(t2, bl2.Hour14);
                    t2 = Math.Max(t2, bl2.Hour15);
                    t2 = Math.Max(t2, bl2.Hour16);
                    t2 = Math.Max(t2, bl2.Hour17);
                    t2 = Math.Max(t2, bl2.Hour18);
                    t2 = Math.Max(t2, bl2.Hour19);
                    t2 = Math.Max(t2, bl2.Hour20);
                    t2 = Math.Max(t2, bl2.Hour21);
                    t2 = Math.Max(t2, bl2.Hour22);
                    t2 = Math.Max(t2, bl2.Hour23);
                    t2 = Math.Max(t2, bl2.Hour24);
                }
            }
            catch { }

            if (t1 != 0)
                Object.SummerData = powerdata / t1;
            if (t2 != 0)
                Object.WinterData = powerdata / t2;

        }

        private InputLanguage oldInput = null;
        private void vGridControl_FocusedRowChanged(object sender, DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraEditors.Repository.RepositoryItemTextEdit edit = e.Row.Properties.RowEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
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


	}
}
