
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Itop.Domain.Chen;
using Itop.Common;
using Itop.Client.Common;
using System.Collections;
using Itop.Client.Base;
namespace Itop.Client.Chen
{
	public partial class FrmPSP_VolumeBalanceDialog : FormBase
	{

		#region 字段
		protected bool _isCreate = false;
		protected PSP_VolumeBalance _obj = null;
        private IList<PSP_VolumeBalance> list;
        public IList<PSP_VolumeBalance> List
        {
            set { list = value; }
        }
        private int baseyear=1990;
        public int BaseYear
        {
            set { baseyear = value; }
        }
		#endregion

		#region 构造方法
		public FrmPSP_VolumeBalanceDialog()
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
        private string baseyeartype = "baseyear";
        private string type="";
        private string flag = "";
        private  Hashtable hs = new Hashtable();
        public string Type
        {
            set { type = value;
                    if (type == "110")
                    {
                        hs.Add("综合最高负荷", 1);
                        hs.Add("220kV主变35kV侧可供负荷", 2);
                      //  hs.Add("小电厂需备用容量", 3);
                        hs.Add("110kV及以下小电源直接供电负荷", 4);
                        hs.Add("现有110kV降压变电容量", 6);
                        baseyeartype = "baseyear110";
                    }
                    if (type == "220")
                    {
                        hs.Add("综合最高负荷", 1);
                       // hs.Add("直接供电负荷", 2);
                      //  hs.Add("外网供电", 4);
                       // hs.Add("110kV及以下小电源直接供电负荷", 4);
                        hs.Add("现有220kV降压变电容量", 6);
                        baseyeartype = "baseyear220";

                    }
                    if (type == "35")
                    {
                        hs.Add("本区负荷", 1);
                        //       hs.Add("直接供电负荷", 2);
                        hs.Add("本地平衡负荷", 2);
                        // hs.Add("110kV及以下小电源直接供电负荷", 4);
                        hs.Add("现有220kV降压变电容量", 6);
                        baseyeartype = "baseyear35";

                    }
                }
        
        }
        public string  Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        bool isresum = false;
        public bool IsReSum
        {
            get { return isresum; }
           
        }
		/// <summary>
		/// 所邦定的对象
		/// </summary>
		public PSP_VolumeBalance Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region 事件处理
		private void FrmPSP_VolumeBalanceDialog_Load(object sender, EventArgs e)
		{
			IList<PSP_VolumeBalance> list = new List<PSP_VolumeBalance>();
			list.Add(Object);
			this.vGridControl.DataSource = list;
            inita();
		}
        private void inita()
        {
            repositoryItemComboBox1.Items.Clear();
            
            for (int i = DateTime.Now.Year - 10; i <= DateTime.Now.Year + 20; i++)
            {
                bool isexit = true;
                foreach (PSP_VolumeBalance PSP_VolumeBalanceTEMP in list)
                {
                    if (PSP_VolumeBalanceTEMP.Year == i)
                    {
                        isexit = false;
                        break;
                    }
                }
                if (isexit)
                repositoryItemComboBox1.Items.Add(i);
            }
            if (IsCreate)
            {
                if (list != null)
                    if (list.Count > 0)
                    {
                        _obj.Year = list[list.Count - 1].Year + 1;
                    }
                    else
                        _obj.Year = DateTime.Now.Year;
            }
            if (_obj.Year != EnsureBaseYear())
            {
                _obj.S1 = "0";
                rowL6.Visible = false;
               

            }
            else
            {
              
                _obj.S1 = "1";
                rowL6.Visible = true;
               
            }
        
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

				if (IsCreate)
				{
					Services.BaseService.Create<PSP_VolumeBalance>(_obj);
				}
				else
				{
					Services.BaseService.Update<PSP_VolumeBalance>(_obj);
				}
            
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
				HandleException.TryCatch(exc);
				return false ;
			}
            if (_obj.S1 == "1")
            {
                PSP_VolumeBalance ob = new PSP_VolumeBalance();
                ob.Year = EnsureBaseYear();
                ob.TypeID = type;
                ob.Flag = flag;
                IList<PSP_VolumeBalance> list0 = Services.BaseService.GetList<PSP_VolumeBalance>("SelectPSP_VolumeBalanceByTypeIDAndYear", ob);
                if (list0 != null)
                {
                    if (list0.Count == 1)
                    {
                        list0[0].S1 = "0";
                        Services.BaseService.Update<PSP_VolumeBalance>(list0[0]);
                    }
                }
                PSP_VolumeBalanceDataSource BaseYearrate = new PSP_VolumeBalanceDataSource();
                BaseYearrate.UID = baseyeartype;
                BaseYearrate.Flag = flag;
                BaseYearrate.Value = _obj.Year;
                BaseYearrate.TypeID = Convert.ToInt32(_obj.TypeID);
                Services.BaseService.Update("UpdatePSP_VolumeBalanceDataSource2", BaseYearrate);
            }
            else if (_obj.Year == EnsureBaseYear() /*|| (EnsureBaseYear() == baseyear)*/)
            {
                PSP_VolumeBalance ob = new PSP_VolumeBalance();
                ob.Flag = flag;
                ob.TypeID = type;
                IList<PSP_VolumeBalance> list0 = Services.BaseService.GetList<PSP_VolumeBalance>("SelectPSP_VolumeBalanceByTypeID", ob);
                if (list0 != null)
                    if (list0.Count > 0)
                        ob = list0[0];
                PSP_VolumeBalanceDataSource BaseYearrate = new PSP_VolumeBalanceDataSource();
                BaseYearrate.Flag = flag;
                BaseYearrate.UID = baseyeartype;
                BaseYearrate.Value = baseyear;
                BaseYearrate.TypeID = Convert.ToInt32(_obj.TypeID);
                Services.BaseService.Update("UpdatePSP_VolumeBalanceDataSource2", BaseYearrate);
                PSP_VolumeBalanceDataSource volumtemp = new PSP_VolumeBalanceDataSource();
                volumtemp.TypeID = Convert.ToInt32(type);
                volumtemp.Flag = flag;
                IList<PSP_VolumeBalanceDataSource> list = Services.BaseService.GetList<PSP_VolumeBalanceDataSource>("SelectPSP_VolumeBalanceDataSourceByTypeID", volumtemp);
                        if (ob != null)
                            if (ob.UID != "")
                                foreach (PSP_VolumeBalanceDataSource psptemp in list)
                                {
                                    if (psptemp.UID == baseyeartype)
                                        continue;
                                    if (hs[psptemp.Name] != null)
                                    ob.GetType().GetProperty("L" + hs[psptemp.Name].ToString()).SetValue(ob, psptemp.Value, null);
                                }
                        try
                        {
                            ob.S1 = "1";
                            Services.BaseService.Update<PSP_VolumeBalance>(ob);
                            isresum = true;
                        }

                        catch (Exception ex)
                        {
                            System.Console.WriteLine(ex.ToString());
                        }

            }
			//操作已成功
			return true;
		}
		#endregion
        PSP_VolumeBalance objtemp=new PSP_VolumeBalance ();
        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            
              DevExpress.XtraEditors.CheckEdit CheckEdit1temp = (DevExpress.XtraEditors.CheckEdit)sender;
                  PSP_VolumeBalanceDataSource volumtemp=new PSP_VolumeBalanceDataSource ();
                volumtemp.TypeID =Convert.ToInt32(type);
                volumtemp.Flag = flag;
                IList<PSP_VolumeBalanceDataSource> list = Services.BaseService.GetList<PSP_VolumeBalanceDataSource>("SelectPSP_VolumeBalanceDataSourceByTypeID", volumtemp);
              if (CheckEdit1temp.Checked)
              {
                  for (int i = 0; i < _obj.GetType().GetProperties().Length;i++ )
                      objtemp.GetType().GetProperty(_obj.GetType().GetProperties()[i].Name).SetValue(objtemp,_obj.GetType().GetProperty(_obj.GetType().GetProperties()[i].Name).GetValue(_obj, null), null);
                          foreach (PSP_VolumeBalanceDataSource psptemp in list)
                          {

                              try
                              {
                                  if (psptemp.UID == baseyeartype)
                                      continue;
                                  if (hs[psptemp.Name] != null)
                                  _obj.GetType().GetProperty("L" + hs[psptemp.Name].ToString()).SetValue(_obj, psptemp.Value, null);
                              }
                              catch (Exception ex)
                              {
                                  System.Console.WriteLine(ex.ToString());
                              }

                          }

                      

                rowL6.Visible = true;
                _obj.S1 = "1";
              }
              else
              {
                  //if (list.Count == 5 && hs.Count == 5)
                  //{
                  //    foreach (PSP_VolumeBalanceDataSource psptemp in list)
                  //    {
                  //        try
                  //        {
                  //            _obj.GetType().GetProperty("L" + hs[psptemp.Name].ToString()).SetValue(_obj, 0, null);
                  //        }
                  //        catch (Exception ex)
                  //        {
                  //            System.Console.WriteLine(ex.ToString());
                  //        }

                  //    }

                  //}
                  if (objtemp.Year != 0)
                  {
                      for (int i = 0; i < _obj.GetType().GetProperties().Length; i++)
                          _obj.GetType().GetProperty(objtemp.GetType().GetProperties()[i].Name).SetValue(_obj, objtemp.GetType().GetProperty(_obj.GetType().GetProperties()[i].Name).GetValue(objtemp, null), null);
                    
                  }
                  _obj.S1 = "0";
                  rowL6.Visible = false;
              }
        }
        #region 确定基准年
        private int EnsureBaseYear()
        {
            PSP_VolumeBalanceDataSource BaseYeartemp = new PSP_VolumeBalanceDataSource();
            BaseYeartemp.TypeID = Convert.ToInt32(type);
                BaseYeartemp.UID = baseyeartype ;
                BaseYeartemp.Flag = flag;
            PSP_VolumeBalanceDataSource BaseYearrate = (PSP_VolumeBalanceDataSource)Common.Services.BaseService.GetObject("SelectPSP_VolumeBalanceDataSourceByKeyTypeID", BaseYeartemp);
            if (BaseYearrate == null)
            {
                BaseYearrate = new PSP_VolumeBalanceDataSource();
                BaseYearrate.UID = BaseYeartemp.UID;
                BaseYearrate.Value = 1900;
                BaseYearrate.TypeID = Convert.ToInt32(type);
                BaseYearrate.Flag = flag;
                Services.BaseService.Create<PSP_VolumeBalanceDataSource>(BaseYearrate);
                //
            }
            return Convert.ToInt32(BaseYearrate.Value);

        }
        #endregion
	}
}
