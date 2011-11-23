
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
using System.Collections;
using Itop.Domain.Graphics;
using Itop.Client.Base;
namespace Itop.Client.Table
{
	public partial class FrmProject_SumDialogWH : FormBase
	{

		#region 字段
		protected bool _isCreate = false;
		protected Project_Sum _obj = null;
		#endregion

		#region 构造方法
        public FrmProject_SumDialogWH()
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
		public Project_Sum Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region 事件处理
		private void FrmProject_SumDialog_Load(object sender, EventArgs e)
		{
            if (Object.S5 == "1")
            {
                //rowType.Properties.Caption = "回路数";
                //rowT1.Properties.Caption = "气象条件";
                //rowT2.Properties.Caption = "导线型号";
                //rowT3.Properties.Caption = "地线型号";

                //rowType.Visible = false;
                //rowT1.Visible = false;
                //rowT2.Visible = false;
                //rowT3.Visible = false;
                //editorRow2.Visible = false;
                //editorRow3.Visible = false;



            //    IList<LineInfo> li=Services.BaseService.GetList<LineInfo>("SelectLineInfoByguihua",null);
            //    Hashtable ht=new Hashtable();
            //    foreach(LineInfo l in li)
            //    {
            //        if (!ht.ContainsKey(l.LineType))
            //        {
            //            repositoryItemComboBox4.Items.Add(l.LineType);
            //            ht.Add(l.LineType, "");
            //        }
            //    }



            }
            else
            {

                //editorRow4.Visible = false;
                //editorRow5.Visible = false;
                //editorRow6.Visible = false;
                //editorRow7.Visible = false;


            }


			IList<Project_Sum> list = new List<Project_Sum>();
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
            if (_obj.S1 == "")
            {
                MsgBox.Show("电压等级不能为空！");
                return false;
            }

            if (_obj.Name == "")
            {
                MsgBox.Show("项目名称不能为空！");
                return false;
            }

            if (Object.S5 == "1") //xianlu
            {
                //if (_obj.L1 == "")
                //{
                //    MsgBox.Show("导线型号不能为空！");
                //    return false;
                //}
            }
            else
            {
                //if (_obj.T1 == "")
                //{
                //    MsgBox.Show("主变台数不能为空！");
                //    return false;
                //}

                //if (_obj.T5 == "")
                //{
                //    MsgBox.Show("规模不能为空！");
                //    return false;
                //}
                //单台容量

            }
            //if (_obj.Num== 0)
            //{
            //    MsgBox.Show("静态投资不能为空！");
            //    return false;
            //}

            Project_Sum str = new Project_Sum();
            str.Name = _obj.Name;
            str.S5= _obj.S5;
            IList<Project_Sum> list = new List<Project_Sum>();
                
            //////////IList<Project_Sum> list    = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByNameandS5", str);
            //////////if (IsCreate)
            //////////{
            //////////    if (list.Count > 0)
            //////////    {
            //////////        MsgBox.Show("已存在同名的典型方案！");
            //////////        list.Clear();
            //////////        return false;

            //////////    }
            //////////}
            //////////else
            //////////{
            //////////    if (list.Count == 1)
            //////////    {
            //////////        //IList<Project_Sum> listtemp = list as IList<Project_Sum>;
            //////////        //////////    if (list[0].UID != _obj.UID)
            //////////        ////////// {
            //////////        //////////     MsgBox.Show("已存在同名的典型方案！");
            //////////        //////////     list.Clear();
            //////////        //////////     return false;
            //////////        ////////// }
            //////////        //////////}
            //////////        //////////else if (list.Count > 1)
            //////////        //////////{
            //////////        //////////    MsgBox.Show("已存在同名的典型方案！");
            //////////        //////////    list.Clear();
            //////////        //////////    return false;

            //////////    }

            //////////}
           // if (_obj.S5 == "1")
           // {
                str.S1= _obj.S1;
                str.Type = _obj.Type;
                str.T5 = _obj.T5;
                str.Name = _obj.Name;
                list = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumBySxt", str);
                if (IsCreate)
                {
                    if (list.Count > 0)
                    {
                        MsgBox.Show("已存在相同的方案(电压、项目名称、规格、规模完全相同)！");
                        list.Clear();
                        return false;

                    }
                }
                else
                {
                    if (list.Count== 1)
                    {
                        //IList<Project_Sum> listtemp = list as IList<Project_Sum>;
                        if (list[0].UID != _obj.UID)
                        {
                            MsgBox.Show("已存在相同的方案(电压、项目名称、规格、规模完全相同)！");
                            list.Clear();
                            return false;

                        }
                    }
                    else if (list.Count > 1)
                    {
                        MsgBox.Show("已存在相同的方案(电压、项目名称、规格、规模完全相同)！");
                        list.Clear();
                        return false;

                    }
                
                }
          //  }
            //else
            //    if (_obj.S5 == "2")
            //    {
            //        str.S1 = _obj.S1;
            //        str.T1 = _obj.T1;
            //        str.T5 = _obj.T5;
            //        str.S5 = _obj.S5;
            //        list = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByvalue3", str);
            //        if (IsCreate)
            //        {
            //            if (list.Count > 0)
            //            {
            //                MsgBox.Show("已存在相同的方案(电压、主变台数、单台容量完全相同)！");
            //                list.Clear();
            //                return false;

            //            }
            //        }
            //        else 
            //        {
            //            if (list.Count == 1)
            //            {
            //                //IList<Project_Sum> listtemp = list as IList<Project_Sum>;
            //                if (list[0].UID != _obj.UID)
            //                {
            //                    MsgBox.Show("已存在相同的方案(电压、主变台数、单台容量完全相同)！");
            //                    list.Clear();
            //                    return false;

            //                }
            //            }
            //            else if (list.Count > 1)
            //            {
            //                MsgBox.Show("已存在相同的方案(电压、主变台数、单台容量完全相同)！");
            //                list.Clear();
            //                return false;

            //            }
            //        }
                
            //    }
			return true;
		}

		protected bool SaveRecord()
		{
			//创建/修改 对象
			try
			{
				if (IsCreate)
				{
					Services.BaseService.Create<Project_Sum>(_obj);
				}
				else
				{
					Services.BaseService.Update<Project_Sum>(_obj);
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
