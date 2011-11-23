
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

		#region �ֶ�
		protected bool _isCreate = false;
		protected Project_Sum _obj = null;
		#endregion

		#region ���췽��
        public FrmProject_SumDialogWH()
		{
			InitializeComponent();
		}
		#endregion

		#region ��������
		/// <summary>
		/// true:��������false:�޸Ķ���
		/// </summary>
		public bool IsCreate
		{
			get { return _isCreate; }
			set { _isCreate = value; }
		}

		/// <summary>
		/// ����Ķ���
		/// </summary>
		public Project_Sum Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region �¼�����
		private void FrmProject_SumDialog_Load(object sender, EventArgs e)
		{
            if (Object.S5 == "1")
            {
                //rowType.Properties.Caption = "��·��";
                //rowT1.Properties.Caption = "��������";
                //rowT2.Properties.Caption = "�����ͺ�";
                //rowT3.Properties.Caption = "�����ͺ�";

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

		#region ��������
		protected bool InputCheck()
		{
            if (_obj.S1 == "")
            {
                MsgBox.Show("��ѹ�ȼ�����Ϊ�գ�");
                return false;
            }

            if (_obj.Name == "")
            {
                MsgBox.Show("��Ŀ���Ʋ���Ϊ�գ�");
                return false;
            }

            if (Object.S5 == "1") //xianlu
            {
                //if (_obj.L1 == "")
                //{
                //    MsgBox.Show("�����ͺŲ���Ϊ�գ�");
                //    return false;
                //}
            }
            else
            {
                //if (_obj.T1 == "")
                //{
                //    MsgBox.Show("����̨������Ϊ�գ�");
                //    return false;
                //}

                //if (_obj.T5 == "")
                //{
                //    MsgBox.Show("��ģ����Ϊ�գ�");
                //    return false;
                //}
                //��̨����

            }
            //if (_obj.Num== 0)
            //{
            //    MsgBox.Show("��̬Ͷ�ʲ���Ϊ�գ�");
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
            //////////        MsgBox.Show("�Ѵ���ͬ���ĵ��ͷ�����");
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
            //////////        //////////     MsgBox.Show("�Ѵ���ͬ���ĵ��ͷ�����");
            //////////        //////////     list.Clear();
            //////////        //////////     return false;
            //////////        ////////// }
            //////////        //////////}
            //////////        //////////else if (list.Count > 1)
            //////////        //////////{
            //////////        //////////    MsgBox.Show("�Ѵ���ͬ���ĵ��ͷ�����");
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
                        MsgBox.Show("�Ѵ�����ͬ�ķ���(��ѹ����Ŀ���ơ���񡢹�ģ��ȫ��ͬ)��");
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
                            MsgBox.Show("�Ѵ�����ͬ�ķ���(��ѹ����Ŀ���ơ���񡢹�ģ��ȫ��ͬ)��");
                            list.Clear();
                            return false;

                        }
                    }
                    else if (list.Count > 1)
                    {
                        MsgBox.Show("�Ѵ�����ͬ�ķ���(��ѹ����Ŀ���ơ���񡢹�ģ��ȫ��ͬ)��");
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
            //                MsgBox.Show("�Ѵ�����ͬ�ķ���(��ѹ������̨������̨������ȫ��ͬ)��");
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
            //                    MsgBox.Show("�Ѵ�����ͬ�ķ���(��ѹ������̨������̨������ȫ��ͬ)��");
            //                    list.Clear();
            //                    return false;

            //                }
            //            }
            //            else if (list.Count > 1)
            //            {
            //                MsgBox.Show("�Ѵ�����ͬ�ķ���(��ѹ������̨������̨������ȫ��ͬ)��");
            //                list.Clear();
            //                return false;

            //            }
            //        }
                
            //    }
			return true;
		}

		protected bool SaveRecord()
		{
			//����/�޸� ����
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

			//�����ѳɹ�
			return true;
		}
		#endregion
	}
}
