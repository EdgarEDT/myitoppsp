
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
using Itop.Domain.PWTable;
using Itop.Client.Base;
namespace Itop.Client.PWTable
{
	public partial class FrmPW_SumDialog : FormBase
	{

		#region �ֶ�
		protected bool _isCreate = false;
        protected PW_tb3a _obj = null;
		#endregion

		#region ���췽��
		public FrmPW_SumDialog()
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
        public PW_tb3a Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region �¼�����
		private void FrmProject_SumDialog_Load(object sender, EventArgs e)
		{
            PW_tb3c type = new PW_tb3c();
            type.col4 = Itop.Client.MIS.ProgUID;
            IList<PW_tb3c> list0 = Services.BaseService.GetList<PW_tb3c>("SelectPW_tb3cList", type);
            object[] obj = new object[list0.Count];
            for (int i = 0; i < list0.Count;i++ )
            {
                obj[i] = list0[i].col1;
            }
            this.repositoryItemComboBox1.Items.AddRange(obj);
            for(int i=1960;i<2040;i++){
                repositoryItemComboBox9.Items.Add(i.ToString());
            }

            IList<PW_tb3a> list = new List<PW_tb3a>();
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


            if (_obj.PQName == "")
            {
                MsgBox.Show("Ƭ�����Ʋ���Ϊ�գ�");
                return false;
            }
            if (_obj.PQtype == "")
            {
                MsgBox.Show("���������Ϊ�գ�");
                return false;
            }
            if (_obj.SubName == "")
            {
                MsgBox.Show("���վ���Ʋ���Ϊ�գ�");
                return false;
            }
            if (_obj.LineName == "")
            {
                MsgBox.Show("��·���Ʋ���Ϊ�գ�");
                return false;
            }
            if (_obj.LineType == "")
            {
                MsgBox.Show("��·���Ͳ���Ϊ�գ�");
                return false;
            }
          
            if (_obj.LineSX == "")
            {
                MsgBox.Show("��·���Բ���Ϊ�գ�");
                return false;
            }
            if (_obj.JXMS == "")
            {
                MsgBox.Show("����ģʽ����Ϊ�գ�");
                return false;
            }
              

			return true;
		}

		protected bool SaveRecord()
		{
			//����/�޸� ����
			try
			{
				if (IsCreate)
				{
                    _obj.Num4 = _obj.Num1 + _obj.Num2 + _obj.Num3;
                    if (_obj.SafeFH != 0)
                    {
                        _obj.FZL = Convert.ToDecimal(_obj.MaxFH / _obj.SafeFH * 100);
                    }
                    Services.BaseService.Create<PW_tb3a>(_obj);
				}
				else
				{
                    PW_tb3a p1 = Services.BaseService.GetOneByKey<PW_tb3a>(_obj.UID);

                    IList<PW_tb3a> list4 = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aByLLXName", p1);
                    for (int i = 0; i < list4.Count;i++ )
                    {
                        PW_tb3a p2 = list4[i];
                        p2.LLXMC = p2.LLXMC.Replace(p1.LineName,_obj.LineName);
                        Services.BaseService.Update<PW_tb3a>(p2);
                    }
                    if (_obj.SafeFH!=0)
                    {
                       // _obj.FZL = Convert.ToDecimal(_obj.MaxFH / _obj.SafeFH * 100);
                        _obj.FZL = Convert.ToDecimal(Convert.ToDecimal(_obj.MaxFH / _obj.SafeFH * 100).ToString("##.##"));
                    }
                    _obj.Num4 = _obj.Num1 + _obj.Num2 + _obj.Num3;
                    Services.BaseService.Update<PW_tb3a>(_obj);
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

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmLineSel f = new FrmLineSel();
            f.SelfName = _obj.LineName;

            if (f.ShowDialog() == DialogResult.OK)
            {
                //repositoryItemButtonEdit1.
                _obj.LLXMC = f.Sel_str;
                vGridControl.UpdateFocusedRecord();
            }
        }

        private void vGridControl_RowChanged(object sender, DevExpress.XtraVerticalGrid.Events.RowChangedEventArgs e)
        {
            
        }

        private void vGridControl_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            //editorRow20/editorRow21
            if (e.Row.Name == "editorRow21")
            {
                _obj.FZL =Convert.ToDecimal( Convert.ToDecimal( _obj.MaxFH / _obj.SafeFH*100).ToString("##.##"));
               
            }
            if (e.Row.Name == "editorRow6")
            {
                _obj.Num4 = _obj.Num1 + _obj.Num2 + _obj.Num3;
            }
        }
	}
}
