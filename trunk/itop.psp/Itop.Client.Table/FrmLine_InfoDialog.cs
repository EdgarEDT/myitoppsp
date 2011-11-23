
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
using DevExpress.XtraVerticalGrid.Rows;
using System.Collections;
using DevExpress.XtraEditors;
using Itop.Domain.Graphics;
using Itop.Client.Base;
namespace Itop.Client.Table
{
	public partial class FrmLine_InfoDialog : FormBase
	{
        private string types1 = "";
        private string types2 = "";
        private string flags1 = "";
        private CtrlLine_Info ctrls;

        private IList<Line_Info> liss = new List<Line_Info>();

        public IList<Line_Info> LIST
        {
            get { return liss; }
            set { liss = value; }
        }

        public string Type
        {
            get { return types1; }
            set { types1 = value; }
        }

        public string Type2
        {
            get { return types2; }
            set { types2 = value; }
        }

        public string Flag
        {
            get { return flags1; }
            set { flags1 = value; }
        }


        public CtrlLine_Info ctrlLint_Info
        {
            get { return ctrls; }
            set { ctrls = value; }
        }


        public bool IsSelect
        {
            get { return isselect; }
            set { isselect = value; }
        }

        bool isselect = false;




		#region 字段
		protected bool _isCreate = false;
		protected Line_Info _obj = null;

		#endregion

		#region 构造方法
		public FrmLine_InfoDialog()
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
		public Line_Info Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region 事件处理
		private void FrmLine_InfoDialog_Load(object sender, EventArgs e)
		{
            if (isselect)
                this.vGridControl.Enabled = false;

            if (!IsCreate)
            {
                checkEdit1.Visible=false;
            }

            IList<Line_Info> list = new List<Line_Info>();
			list.Add(Object);
            string s = " ProjectID='" + Itop.Client.MIS.ProgUID + "'";
            IList list2 = Services.BaseService.GetList("SelectPS_Table_AreaWHByConn", s);
            repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            repositoryItemLookUpEdit1.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Title")});
            repositoryItemLookUpEdit1.DisplayMember = "Title";
            repositoryItemLookUpEdit1.NullText = "";
            repositoryItemLookUpEdit1.ValueMember = "ID";
            repositoryItemLookUpEdit1.DataSource = list2;
			this.vGridControl.DataSource = list;

            
                WireCategory wirewire = new WireCategory();
                IList list1 = Services.BaseService.GetList("SelectWireCategoryList", "");
                foreach (WireCategory sub in list1)
                {
                    if (repositoryItemComboBox7.Items.IndexOf(sub.WireType) == -1)
                    {
                        repositoryItemComboBox7.Items.Add(sub.WireType);
                    }
                }
            

            foreach (BaseRow gc in this.vGridControl.Rows)
            {
                if (gc.Properties.FieldName == "")
                    continue;
                if (gc.Properties.FieldName.Substring(0, 1) == "S" && gc.Properties.FieldName != "S4")
                    gc.Visible = false;

                if (gc.Properties.FieldName.Substring(0, 1) == "K" && types2=="66")
                    gc.Visible = false;

                if (gc.Properties.FieldName.Substring(0, 1) == "L" && types2 == "10")
                    gc.Visible = false;
            }

            PowerSubstationLine psl = new PowerSubstationLine();
            psl.Flag = flags1;
            psl.Type = types1;
            psl.Type2 = types2;

            IList<PowerSubstationLine> li = Itop.Client.Common.Services.BaseService.GetList<PowerSubstationLine>("SelectPowerSubstationLineByFlagType", psl);



            foreach (BaseRow gc1 in this.vGridControl.Rows)
            {
                if (gc1.Properties.FieldName == "")
                {
                    if (types2 == "66")
                        gc1.Visible = false;
                    continue;
                }
                    
                if (gc1.Properties.FieldName.Substring(0, 1) == "S")
                {
                    //foreach (PowerSubstationLine pss in li)
                    //{

                    //    if (gc1.Properties.FieldName == pss.ClassType)
                    //    {
                    //        gc1.Properties.Caption = pss.Title;
                    //        gc1.Visible = true;
                    //    }
                    //}
                }

            }

            //MessageBox.Show(liss.Count.ToString());

            Hashtable hs1 = new Hashtable();
            Hashtable hs2 = new Hashtable();
            Hashtable hs3 = new Hashtable();


            //foreach (Line_Info liii in liss)
            //{
            //    if (!hs1.ContainsValue(liii.K2))
            //        hs1.Add(Guid.NewGuid().ToString(), liii.K2);

            //    if (!hs2.ContainsValue(liii.K3))
            //        hs2.Add(Guid.NewGuid().ToString(), liii.K3);

            //    if (!hs3.ContainsValue(liii.K22))
            //        hs3.Add(Guid.NewGuid().ToString(), liii.K22);
            //}

            //MessageBox.Show(hs1.Count.ToString());
            //MessageBox.Show(hs2.Count.ToString());
            //MessageBox.Show(hs3.Count.ToString());
            
            //foreach (DictionaryEntry de1 in hs1)
            //{
            //    try
            //    {
            //        repositoryItemComboBox1.Items.Add(de1.Value.ToString());
            //    }
            //    catch { }
            //}

            //foreach (DictionaryEntry de2 in hs2)
            //{
            //    try
            //    {
            //        repositoryItemComboBox2.Items.Add(de2.Value.ToString());
            //    }
            //    catch { }
            //}

            //foreach (DictionaryEntry de3 in hs3)
            //{
            //    repositoryItemComboBox3.Items.Add(de3.Value.ToString());
            //}





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
                    ctrls.RefreshData(Type);
                    _obj = new Line_Info();
                    //_obj.L6 = DateTime.Now;
                    _obj.Flag = types1;
                    IList<Line_Info> list1 = new List<Line_Info>();
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
        private string projectid;
        public string ProjectID
        {
            set { projectid = value; }
        }
		protected bool SaveRecord()
		{
			//创建/修改 对象


			try
			{
                if (types2 == "66")
                {
                    if (_obj.L1 == 0)
                    {
                        MsgBox.Show("电压等级没有选择！");
                        return false;
                    
                    }

                    _obj.DY = _obj.L1;
                   // _obj.L1 = 66;
                    _obj.K1 = _obj.L1;
                    _obj.K2 = _obj.L4;
                    _obj.K5 = _obj.L5;
                }
                else if (types2 == "10")
                {
                    if (_obj.K1 == 0)
                    {
                        MsgBox.Show("电压等级没有选择！");
                        return false;

                    }
                    _obj.DY = _obj.K1;
                    _obj.L1 = _obj.K1;
                    //_obj.K1 = 10;
                    _obj.L4 = _obj.K2;
                    _obj.L5 = _obj.K5;
                }
                else
                { 
                    
                
                
                }

                _obj.Flag = types1;


                double ss1=0;
                double ss2=0;
                double ss3=0;

                double ss4 = 0;
                double ss5 = 0;

                try
                {
                    ss1=_obj.K6;
                }catch{}
                try
                {
                    ss2=_obj.K20;
                }
                catch { }
                try
                {
                    //ss3=_obj.k21;
                }
                catch { }
                try 
                { 
                    ss4 = _obj.K10; 
                }
                catch { }
                try
                {
                    ss5 = _obj.K12;
                }
                catch { }


                _obj.K5 = ss1 + ss2 + ss3;

                _obj.K13 =Convert.ToInt32(ss4 + ss5);


                double k1 = 0;//最大电流
                double k2 = 0;//安全电流
                double k3 = 0;//电压
                double k4 = 0;//配变总容量
                double g3 = 1.73205;

                double m1 = 0;
                double m2 = 0;

                try
                {
                    k1 = _obj.K16;
                }
                catch { }

                try
                {
                    k2 = Convert.ToDouble(_obj.K8);
                }
                catch { }

                try
                {
                    k3 = Convert.ToDouble(_obj.K1);
                }
                catch { }

                try
                {
                    k4 = Convert.ToDouble(_obj.K13);
                }
                catch { }

                if (k2 != 0)
                    m1 = k2*100 / k1;

                if (k4 != 0)
                    m2 = k1 * g3 * k3*100 / k4;

                _obj.K19 = m1;
                _obj.K17 = m2;

                _obj.AreaID = projectid;
                try
                {
                    if (_obj.L6 != "" && int.Parse(_obj.L6) > DateTime.Now.Year)
                        _obj.Flag = "2";
                    else
                        _obj.Flag = "1";
                }
                catch { _obj.Flag = "1"; }
                if (IsCreate)
                {
                    _obj.CreateDate = DateTime.Now;
                    _obj.UID += "|" + projectid;
                    Services.BaseService.Create<Line_Info>(_obj);
                }
                else
                {

                    if (_obj.CreateDate.Year < 1900 || _obj.CreateDate == null || _obj.CreateDate.ToString() == "")
                        _obj.CreateDate = DateTime.Now;
                    if (System.Configuration.ConfigurationSettings.AppSettings["LangFang"].ToString() == "廊坊")
                    {
                        Services.BaseService.Update("UpdateLine_InfoXingHao", _obj);
                    }
                    else
                    {
                        Services.BaseService.Update<Line_Info>(_obj);
                    }
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

        private void panelControl_Paint(object sender, PaintEventArgs e)
        {

        }
	}
}
