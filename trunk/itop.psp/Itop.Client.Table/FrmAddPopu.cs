
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
using Itop.Client.Base;
namespace Itop.Client.Table
{
	public partial class FrmAddPopu : FormBase
	{
        private FrmAreaData area;
        public FrmAreaData Area
        {
            set { area = value; }
        }
        private string types1 = "";
        private string flags1 = "";
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
		protected Ps_Table_AreaData _obj = null;
		#endregion

		#region ���췽��
        public FrmAddPopu()
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
        public Ps_Table_AreaData Object
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


            IList<Ps_Table_AreaData> list = new List<Ps_Table_AreaData>();
			list.Add(_obj);
			this.vGridControl.DataSource = list;

		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (SaveRecord())
			{
                if (checkEdit1.Checked)
                {
                    area.InitGrid1();
                    _obj = new Ps_Table_AreaData();
                    IList<Ps_Table_AreaData> list1 = new List<Ps_Table_AreaData>();
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

      
        private string projectid,parentid;
        public string ProjectID
        {
            set { projectid = value; }
        }
        public string ParentID
        {
            set { parentid = value; }
        }
		protected bool SaveRecord()
		{
			try
			{
				if (IsCreate)
				{
                    string id = _obj.ID;
                    _obj.ID += "|" + projectid;
                    _obj.ParentID = parentid;
                    _obj.ProjectID = projectid;
                    _obj.SortID = OperTable.GetAreaMaxSort()+1;
                    IList<Ps_Table_AreaData> list = Common.Services.BaseService.GetList<Ps_Table_AreaData>("SelectPs_Table_AreaDataByConn", " ParentID='" + parentid + "' and ProjectID='" + projectid + "'and Yearf='" + _obj.Yearf + "'");
                    if (list.Count > 0)
                    {
                        MessageBox.Show("�����ظ���ݣ������������ݣ�");
                        _obj.ID=id;
                        return false;
                    }
                    Services.BaseService.Create<Ps_Table_AreaData>(_obj);
				}
				else
				{
                    IList<Ps_Table_AreaData> list = Common.Services.BaseService.GetList<Ps_Table_AreaData>("SelectPs_Table_AreaDataByConn", " ParentID='" + parentid + "' and ProjectID='" + projectid + "'and Yearf='" + _obj.Yearf + "'");

                    foreach (Ps_Table_AreaData pa in list)
                    {
                        if (pa.ID != _obj.ID)
                        {
                            MessageBox.Show("�����ظ���ݣ������������ݣ�");
                            return false;
                        }
                    }
                    Services.BaseService.Update<Ps_Table_AreaData>(_obj);
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
