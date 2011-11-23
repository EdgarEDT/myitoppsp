
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
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
	public partial class FrmSubstation_Info_LangFangDialog : FormBase
	{

        private string types1 = "";
        private string flags1 = "";
        private string types2 = "";
        private CtrlSubstation_Info_LangFang ctrls;

        public CtrlSubstation_Info_LangFang ctrlSubstation_Info
        {
            get { return ctrls; }
            set { ctrls = value; }
        }

        public string Type
        {
            get { return types1; }
            set { types1 = value; }       
        }
        public string Type2
        {
            get { return types2; }
            set { types2= value; }
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
		protected Substation_Info _obj = null;
		#endregion

		#region 构造方法
        public FrmSubstation_Info_LangFangDialog()
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
		public Substation_Info Object
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

			IList<Substation_Info> list = new List<Substation_Info>();
			list.Add(_obj);
			this.vGridControl.DataSource = list;

            foreach (BaseRow gc in this.vGridControl.Rows)
            {
                if (gc.Properties.FieldName.Substring(0, 1) == "S")
                    gc.Visible = false;
            }

            PowerSubstationLine psl = new PowerSubstationLine();
            psl.Flag = flags1;
            psl.Type = types1;
            psl.Type2 = types2;
            IList<PowerSubstationLine> li = Itop.Client.Common.Services.BaseService.GetList<PowerSubstationLine>("SelectPowerSubstationLineByFlagType", psl);

            foreach (BaseRow gc1 in this.vGridControl.Rows)
            {
                if (gc1.Properties.FieldName.Substring(0, 1) == "S")
                {
                    foreach (PowerSubstationLine pss in li)
                    {

                        if (gc1.Properties.FieldName == pss.ClassType)
                        {
                            gc1.Properties.Caption = pss.Title;
                            gc1.Visible = true;
                        }
                    }
                }

            }
		}

		private void btnOK_Click(object sender, EventArgs e)
		{

            if (_obj.AreaName == "" )
            {
                MsgBox.Show("建设地点为必填项！");
                return;
            }
            if (_obj.Title == "")
            {
                MsgBox.Show("变电站名称为必填项！");
                return;
            }
            if (isselect)
            {
                DialogResult = DialogResult.OK;
                return;
            }

			if (!InputCheck())
			{
				return;
			}

			if (SaveRecord())
			{
                if (checkEdit1.Checked)
                {
                    ctrls.RefreshData();
                    _obj = new Substation_Info();
                    _obj.Flag = flags1;
                    IList<Substation_Info> list1 = new List<Substation_Info>();
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
					Services.BaseService.Create<Substation_Info>(_obj);
				}
				else
				{
                    Services.BaseService.Update("UpdateSubstation_InfoAreaName", _obj);
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

        private void vGridControl_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            if (e.Row.Properties.FieldName.ToString() == "L2" )
            {
                _obj.L2 =double.Parse(e.Value.ToString());
                if (_obj.L2 != null && _obj.L2 != 0 && _obj.L9.ToString() != "")
                {
                    _obj.L10 =double.Parse( _obj.L9.ToString()) / _obj.L2*100;
 
                }
            }
            if ( e.Row.Properties.FieldName.ToString() == "L9")
            {
                _obj.L9 =double.Parse(e.Value.ToString());
                if (_obj.L2 != null && _obj.L2 != 0 && _obj.L9.ToString() != "")
                {
                    _obj.L10 = double.Parse(_obj.L9.ToString()) / _obj.L2 * 100;

                }
            }
            this.vGridControl.Refresh();
        
        }

    }
}
