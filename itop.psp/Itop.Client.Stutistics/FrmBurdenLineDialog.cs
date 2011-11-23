
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Itop.Domain.Stutistic;
using Itop.Domain.Table;
using Itop.Common;
using Itop.Client.Common;
using System.Reflection;
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
	public partial class FrmBurdenLineDialog : FormBase
	{

		#region 字段
		protected bool _isCreate = false;
		protected BurdenLine _obj = null;
		#endregion

		#region 构造方法
		public FrmBurdenLineDialog()
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
		public BurdenLine Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region 事件处理
		private void FrmBurdenLineDialog_Load(object sender, EventArgs e)
		{
            string pjt = " ProjectID='" + MIS.ProgUID + "'";
            IList<PS_Table_AreaWH> lt = Common.Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", pjt);
            repositoryItemLookUpEdit1.DataSource = lt;
			IList<BurdenLine> list = new List<BurdenLine>();
			list.Add(Object);
			this.vGridControl.DataSource = list;
          
            //repositoryItemLookUpEdit1
            //editorRow2.Properties.Value = Object.AreaID;
           

            
            //foreach (PS_Table_AreaWH ps in lt)
            //{
            //        if (!this.repositoryItemComboBox3.Items.Contains(ps.Title))
            //        {
            //            this.repositoryItemComboBox3.Items.Add(ps.Title);
            //        }
            //}
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

            if (_obj.BurdenDate == null)
            {
                MsgBox.Show("请选择日期!");
                return false;
            }

            if (_isCreate)
            {
                BurdenLine bl=new BurdenLine();
                bl.BurdenDate=_obj.BurdenDate;
                if ((int)Services.BaseService.GetObject("SelectBurdenLineByDate", bl) > 0)
                {
                    MsgBox.Show("已存在相同日期，请重新选择!");
                    return false;
                }
            }





			try
			{
                double minData = 0;
                double maxData = 0;
                double sumData = 0;

                for (int i = 1; i <= 24; i++)
                {
                    PropertyInfo pi = _obj.GetType().GetProperty("Hour" + i.ToString());
                    if (i == 1)
                    {
                        minData = (double)pi.GetValue(_obj, null);
                        maxData = (double)pi.GetValue(_obj, null);
                        sumData = (double)pi.GetValue(_obj, null);
                    }
                    else
                    {
                        minData = Math.Min((double)pi.GetValue(_obj, null), minData);
                        maxData = Math.Max((double)pi.GetValue(_obj, null), maxData);
                        sumData += (double)pi.GetValue(_obj, null);
                    }
                }

                if (sumData==0)
                {

                    _obj.DayAverage = sumData;
                    _obj.MinAverage = sumData;
                }
                else
                {
                    _obj.DayAverage = sumData / (24 * maxData);
                    _obj.MinAverage = minData / maxData;

                }
              
                //_obj.AreaID=repositoryItemComboBox3.p

				if (IsCreate)
				{
					Services.BaseService.Create<BurdenLine>(_obj);
				}
				else
				{
					Services.BaseService.Update<BurdenLine>(_obj);
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

        private void repositoryItemCheckEdit1_CheckStateChanged(object sender, EventArgs e)
        {
            //////////////DevExpress.XtraEditors.CheckEdit CheckEdit1temp = (DevExpress.XtraEditors.CheckEdit)sender;
            //////////////if (CheckEdit1temp.Checked)
            //////////////{
            //////////////    repositoryItemCheckEdit2.BeginUpdate();
            //////////////    //repositoryItemCheckEdit2.BeginInit();
            //////////////    _obj.IsType = true;
            //////////////    _obj.IsMaxDate = false;
            //////////////    //repositoryItemCheckEdit2.EndInit();
            //////////////    repositoryItemCheckEdit2.EndUpdate();
            //////////////    //repositoryItemCheckEdit2.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;

            //////////////}
        }

        private void repositoryItemCheckEdit2_CheckStateChanged(object sender, EventArgs e)
        {
            ////////////////DevExpress.XtraEditors.CheckEdit CheckEdit1temp = (DevExpress.XtraEditors.CheckEdit)sender;
            ////////////////if (CheckEdit1temp.Checked)
            ////////////////{
            ////////////////    repositoryItemCheckEdit1.BeginUpdate();
            ////////////////    //repositoryItemCheckEdit1.BeginInit();
            ////////////////    _obj.IsType = false;
            ////////////////    _obj.IsMaxDate = true;
            ////////////////    //repositoryItemCheckEdit1.EndInit();
            ////////////////    repositoryItemCheckEdit1.EndUpdate();
            ////////////////    //repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            ////////////////    //CheckEdit1temp.Checked = false;
            ////////////////}
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            //if (_obj.IsType)
            //{
            //    vGridControl.BeginUpdate();
            //    _obj.IsMaxDate = false;
            //    vGridControl.EndUpdate();
            //}
        }

        private void repositoryItemCheckEdit2_CheckedChanged(object sender, EventArgs e)
        {
            //if (_obj.IsMaxDate)
            //{
            //    vGridControl.BeginUpdate();
            //    _obj.IsType = false;
            //    vGridControl.EndUpdate();
            //}
        }

       

       
	}
}
