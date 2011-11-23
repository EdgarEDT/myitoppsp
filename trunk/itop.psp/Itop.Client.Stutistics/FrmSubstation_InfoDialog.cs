
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



		#region �ֶ�
		protected bool _isCreate = false;
		protected Substation_Info _obj = null;
		#endregion

		#region ���췽��
		public FrmSubstation_InfoDialog()
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
		public Substation_Info Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region �¼�����
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
                if (gc.Properties.FieldName.Substring(0, 1) == "S" && gc.Properties.FieldName != "S1" && gc.Properties.FieldName != "S4" && gc.Properties.FieldName != "S2")
                    gc.Visible = false;
            }

            PowerSubstationLine psl = new PowerSubstationLine();
            psl.Flag = flags1;
            psl.Type = types1;
            psl.Type2 = "Substation";
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

		#region ��������

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
            if (_obj.L2 != 0)
                _obj.L10 = _obj.L9 / _obj.L2;
			//����/�޸� ����
			try
			{
                _obj.AreaID = projectid;
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

			//�����ѳɹ�
			return true;
		}
		#endregion

    }
}
