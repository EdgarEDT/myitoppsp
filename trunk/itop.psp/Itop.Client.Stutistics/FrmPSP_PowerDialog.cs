
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
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
	public partial class FrmPSP_PowerDialog : FormBase
	{
        private string types1 = "";
        private string types2 = "";
        private string flags1 = "";
        private string deleteUID = "";
        private CtrlPower ctrls;

        public CtrlPower ctrlPSP_PowerSubstationInfo
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
            set { types2 = value; }
        }
        public string Flag
        {
            get { return flags1; }
            set { flags1 = value; }
        }
        public string DeleteUID
        {
            get { return deleteUID; }
            set { deleteUID = value; }
        }

        public bool IsSelect
        {
            get { return isselect; }
            set { isselect = value; }
        }

        bool isselect = false;
		#region �ֶ�
		protected bool _isCreate = false;
		protected PSP_PowerSubstationInfo _obj = null;
		#endregion

		#region ���췽��
        public FrmPSP_PowerDialog()
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
		public PSP_PowerSubstationInfo Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region �¼�����
		private void FrmPSP_PowerSubstationInfoDialog_Load(object sender, EventArgs e)
		{

            if (isselect)
              this.vGridControl.Enabled = false;

            if (!IsCreate)
            {
                checkEdit1.Visible = false;
            }


            IList<PSP_PowerSubstationInfo> list = new List<PSP_PowerSubstationInfo>();
            list.Add(Object);
            this.vGridControl.DataSource = list;
            this.xtraTabControl1.TabPages[0].Text = this.Text;


            PowerSubstationLine psl = new PowerSubstationLine();
            psl.Flag = flags1;
            //psl.Type = types1;
            psl.Type2 = types2;

            IList<PowerSubstationLine> li = Itop.Client.Common.Services.BaseService.GetList<PowerSubstationLine>("SelectPowerSubstationLineByFlagType2", psl);

            foreach (PowerSubstationLine pss in li)
            {
                BaseRow vrow = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
                vrow.Properties.Caption = pss.Title;
                vrow.Properties.FieldName = pss.ClassType;
                vrow.Height = 25;
                vrow.Properties.RowEdit = ItemTextEditS1;
                vrow.Visible = true;
                vGridControl.Rows.Add(vrow);

            }


            //vGridControl.Rows.Add();
            //this.rowS2 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            ////foreach (PowerSubstationLine pss in li)
            ////{
            ////foreach (BaseRow gc1 in this.vGridControl.Rows)
            ////{
            ////    if (gc1.Properties.FieldName.Substring(0, 1) == "S")
            ////    {
                   

            ////            if (gc1.Properties.FieldName == pss.ClassType)
            ////            {
            ////                gc1.Properties.Caption = pss.Title;
            ////                gc1.Index = int.Parse(pss.Type);
            ////                //gc1.VisibleIndex = int.Parse(pss.Type);
            ////                //DevExpress.XtraVerticalGrid.Rows.BaseRow aa = DevExpress.Utils.c;
            ////                gc1.Visible = true;
            ////            }
                    
            ////    }

            ////}
            ////}
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
                    _obj = new PSP_PowerSubstationInfo();
                    _obj.Flag = flags1;
                    _obj.CreateDate = DateTime.Now;
                    IList<PSP_PowerSubstationInfo> list1 = new List<PSP_PowerSubstationInfo>();
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
		protected bool InputCheck()
		{
			return true;
		}

		protected bool SaveRecord()
		{
			//����/�޸� ����
			try
            {
              
               // _obj.UID = Guid.NewGuid().ToString();
				if (IsCreate)
                {
                    _obj.UID = Guid.NewGuid().ToString();
					Services.BaseService.Create<PSP_PowerSubstationInfo>(_obj);
				}
				else
				{
                   // deleteUID = _obj.UID;
                   // _obj.UID = Guid.NewGuid().ToString();
                    Services.BaseService.Update("UpdatePSP_PowerSubstationInfo", _obj);
                  // Services.BaseService.Update("DeletePSP_PowerSubstationInfoByUID", deleteUID);
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
