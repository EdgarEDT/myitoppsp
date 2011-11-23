
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
using System.Reflection;
using Itop.Client.ExpressCalculate;
using Itop.Client.Base;
namespace Itop.Client.Table
{
	public partial class FrmSubstation_InfoDialog_AHTL : FormBase
	{

        private string types1 = "";
        private string flags1 = "";
        private CtrlSubstation_Info_AHTL ctrls;

        public CtrlSubstation_Info_AHTL ctrlSubstation_Info
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
		public FrmSubstation_InfoDialog_AHTL()
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
        public void InitArea()
        {
            try
            {
                //if (Assembly.LoadFile(Application.StartupPath + "\\Itop.Domain.Table.dll").GetModule("PS_Table_AreaWH") != null)
                //{
                    string conn = "ProjectID='" + projectid + "' order by Sort";
                    IList<PS_Table_AreaWH> list = Common.Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);
                    foreach (PS_Table_AreaWH area in list)
                    {
                        repositoryItemComboBox3.Items.Add(area.Title);
                    }
                
                conn = "ProjectID='" + projectid + "' order by Sort";
                    IList<PS_Table_Area_TYPE> list1 = Common.Services.BaseService.GetList<PS_Table_Area_TYPE>("SelectPS_Table_Area_TYPEByConn", conn);
                    foreach (PS_Table_Area_TYPE area in list1)
                    {
                        repositoryItemComboBox4.Items.Add(area.Title);
                    }
                //}
            }
            catch { }
        }
		#region 事件处理
        private void FrmSubstation_InfoDialog_Load(object sender, EventArgs e)
        {
            try
            {
                if (isselect)
                    this.vGridControl.Enabled = false;

                if (!IsCreate)
                {
                    checkEdit1.Visible = false;
                }

                InitArea();
                IList<Substation_Info> list = new List<Substation_Info>();
                list.Add(_obj);
                this.vGridControl.DataSource = list;






                //foreach (BaseRow gc in this.vGridControl.Rows)
                //{
                //    if (gc.Properties.FieldName.Substring(0, 1) == "S" && gc.Properties.FieldName != "S1" && gc.Properties.FieldName != "S4" && gc.Properties.FieldName != "S2")
                //        gc.Visible = false;
                //}

                PowerSubstationLine psl = new PowerSubstationLine();
                psl.Flag = flags1;
                psl.Type = types1;
                psl.Type2 = "Substation";
                IList<PowerSubstationLine> li = Itop.Client.Common.Services.BaseService.GetList<PowerSubstationLine>("SelectPowerSubstationLineByFlagType", psl);



                //foreach (BaseRow gc1 in this.vGridControl.Rows)
                //{
                //    if (gc1.Properties.FieldName.Substring(0, 1) == "S")
                //    {
                //        foreach (PowerSubstationLine pss in li)
                //        {

                //            if (gc1.Properties.FieldName == pss.ClassType)
                //            {
                //                gc1.Properties.Caption = pss.Title;
                //                gc1.Visible = true;
                //            }
                //        }
                //    }

                //}
            }
            catch { }
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
                    ctrls.CalcTotal();
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

        public void SetVisible()
        {
            try
            {
                vGridControl.Rows["editorRow1"].Visible = false;
                vGridControl.Rows["editorRow2"].Visible = false;
                vGridControl.Rows["RowL7"].Visible = false;
                vGridControl.Rows["RowL8"].Visible = false;
            }
            catch { }
        }
		protected bool InputCheck()
		{
			return true;
		}
        private string projectid;
        public string ProjectID
        {
            set { projectid = value; }
        }
		protected bool SaveRecord()
		{
            try
            {
                ExpressionCalculator ca = new ExpressionCalculator();
                if (_obj.L4 != "")
                {
                    if (ca.ISIllegal(ca.CharConverter(_obj.L4)))
                    {
                        _obj.L2 = ca.Calculator(ca.CharConverter(_obj.L4), 2);
                    }
                }

                if (_obj.L13 != "0")
                    _obj.S6 = string.Format("{0:f}", double.Parse(_obj.L14) * 100 / double.Parse(_obj.L13));


            }
            catch { }
            if (_obj.L2 != 0)
                _obj.L10 = _obj.L9*100 / _obj.L2;
          
            try
            {
                int x = int.Parse(_obj.S2);
                if (x > DateTime.Now.Year)
                    _obj.Flag = "2";
                else
                    _obj.Flag = "1";
            }
            catch { _obj.Flag = "1"; }

            //创建/修改 对象
           // _obj.L30 = projectid;
			try
			{
                _obj.AreaID = projectid;
				if (IsCreate)
				{
                    _obj.UID += "|" + projectid;
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

    }
}
