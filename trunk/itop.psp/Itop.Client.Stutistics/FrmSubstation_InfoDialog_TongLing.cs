
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Itop.Domain.Stutistics;
using Itop.Common;
using Itop.Client.Common;
using Itop.Domain.Stutistic;
using DevExpress.XtraVerticalGrid.Rows;
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
	public partial class FrmSubstation_InfoDialog_TongLing : FormBase
	{
     private   string types1 = "";
        private string types2 = "";
        public string Type1
        {
            get { return types1; }
            set { types1 = value; }
        }
        public string Type2
        {
            get { return types2; }
            set { types2 = value; }
        }
        private string flags1 = "";
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
        private CtrlSubstation_Info_TongLing ctrls;

        public CtrlSubstation_Info_TongLing ctrlSubstation_Info
        {
            get { return ctrls; }
            set { ctrls = value; }
        }



		#region 字段
		protected bool _isCreate = false;
		protected Substation_Info _obj = null;
		#endregion

		#region 构造方法
		public FrmSubstation_InfoDialog_TongLing()
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
			IList<Substation_Info> list = new List<Substation_Info>();
			list.Add(Object);
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
            //psl.Type2 = "Substation";
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
			if (!InputCheck())
			{
				return;
			}
            if (_obj.Title == "")
            {
                MessageBox.Show("变电站名称不能为空！");
                return;
            }
			if (SaveRecord())
            {
                //if (checkEdit1.Checked)
                //{
                //    ctrls.RefreshData();
                //    _obj = new Substation_Info();
                //    _obj.Flag = flags1;
                //    IList<Substation_Info> list1 = new List<Substation_Info>();
                //    list1.Add(_obj);
                //    this.vGridControl.DataSource = list1;
                //}
                //else

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
					Services.BaseService.Create<Substation_Info>(_obj);
				}
				else
				{
					Services.BaseService.Update<Substation_Info>(_obj);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
	}
}
