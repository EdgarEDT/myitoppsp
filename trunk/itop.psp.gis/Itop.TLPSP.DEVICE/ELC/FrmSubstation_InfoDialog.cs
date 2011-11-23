
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
namespace Itop.TLPSP.DEVICE
{
	public partial class FrmSubstation_InfoDialog : FormBase
	{

        private string types1 = "";
        private string flags1 = "";
        private CtrlSubstation_Info ctrls;

        public CtrlSubstation_Info ctrlSubstation_Info
        {
            get { return ctrls; }
            set { ctrls = value; }
        }

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
		protected Substation_Info _obj = null;
		#endregion

		#region 构造方法
		public FrmSubstation_InfoDialog()
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
        private string projectid;
        public string ProjectID
        {
            set { projectid = value; }
        }
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
            psl.Type2 = "Substation";
            IList<PowerSubstationLine> li = UCDeviceBase.DataService.GetList<PowerSubstationLine>("SelectPowerSubstationLineByFlagType", psl);



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
            if (_obj.L2 != 0)
                _obj.L10 = _obj.L9 / _obj.L2*100;  //修改负载率的过程
			//创建/修改 对象
			try
			{
                _obj.AreaID = projectid;
				if (IsCreate)
				{
					UCDeviceBase.DataService.Create<Substation_Info>(_obj);
				}
				else
				{
					UCDeviceBase.DataService.Update<Substation_Info>(_obj);
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
