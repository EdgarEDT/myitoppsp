
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Common;
using Itop.Client.Common;
using DevExpress.XtraTreeList.Nodes;
using Itop.Client.Base;
namespace Itop.DLGH
{
	public partial class FrmInterFaceDialog : FormBase
	{

		#region 字段
		protected bool _isCreate = false;
        private string typeid = "";
        private string bdz = "";
        private string fq = "";
        private string fxt = "";
        private string uid = "";

       
        private string[] bdzlist;
        private string[] fqlist;
        private string[] fxtlist;
        protected PSP_interface _obj = null;
		#endregion

		#region 构造方法
        public FrmInterFaceDialog()
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
        public string Typeid
        {
            get { return typeid; }
            set { typeid = value; }
        }
        public string Uid
        {
            get { return uid; }
            set { uid = value; }
        }
		/// <summary>
		/// 所邦定的对象
		/// </summary>
        public PSP_interface Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region 事件处理

        public void LoadData()
        {
            PSP_interface p = new PSP_interface();
            
            p = Services.BaseService.GetOneByKey<PSP_interface>(uid);
            if (p != null)
            {
            bdz = p.BdzId;
            fq = p.FQId;
            fxt = p.FxtId;
            bdzlist = bdz.Split(",".ToCharArray());
            fqlist = fq.Split(",".ToCharArray());
            fxtlist = fxt.Split(",".ToCharArray());
            }
            object[] obj = new object[30];
            for (int i = 0; i < 30; i++)
            {
                obj[i] = 2000 + i;
            }
            this.year.Properties.Items.AddRange(obj);
        }

		private void FrmglebeTypeDialog_Load(object sender, EventArgs e)
		{
            LoadData();

            year.Text=_obj.UYear.ToString();
            month.Text=_obj.UMonth;
            textkgbh.Text=_obj.Switch_Id ;
            textkgmc.Text=_obj.Switch_Name;
            textfh.Text=_obj.LoadValue.ToString();
            textdl.Text=_obj.Number.ToString();

            PSP_bdz_type p = new PSP_bdz_type();
            p.col1 = " col1=1 order by Name";
            IList list1 = Services.BaseService.GetList("SelectPSP_bdz_typeByWhere", p);
            
            foreach (PSP_bdz_type str in list1)
            {
                bool b1 = false;
                for (int i = 0; i < bdzlist.Length;i++ )
                {
                    if(bdzlist[i]==str.id.ToString()){
                        b1 = true;
                    }
                }
                treeList1.AppendNode(new object[] {str.id,str.Name,b1}, -1);
            }

            PSP_bdz_type p2 = new PSP_bdz_type();
            p2.col1 = " col1=2 order by Name";
            IList list2 = Services.BaseService.GetList("SelectPSP_bdz_typeByWhere", p2);
            foreach (PSP_bdz_type str in list2)
            {
                bool b2 = false;
                for (int i = 0; i < fqlist.Length; i++)
                {
                    if (fqlist[i] == str.id.ToString())
                    {
                        b2 = true;
                    }
                }
                treeList2.AppendNode(new object[] { str.id, str.Name, b2 }, -1);
            }

            PSP_bdz_type p3 = new PSP_bdz_type();
            p3.col1 = " col1=3 order by Name";
            IList list3 = Services.BaseService.GetList("SelectPSP_bdz_typeByWhere", p3);
            foreach (PSP_bdz_type str in list3)
            {
                bool b3 = false;
                for (int i = 0; i < fxtlist.Length; i++)
                {
                    if (fxtlist[i] == str.id.ToString())
                    {
                        b3 = true;
                    }
                }
                treeList3.AppendNode(new object[] { str.id, str.Name, b3 }, -1);
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
            if (textkgbh.Text == "")
            {
                MessageBox.Show("开关编号不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (textkgmc.Text == "")
            {
                MessageBox.Show("开关名称不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        
			return true;
		}

		protected bool SaveRecord()
		{
			//创建/修改 对象
			try
			{
                if (IsCreate)
                {
                    //_obj.Name = textBox1.Text;
                    _obj.col1 = typeid;
                    Services.BaseService.Create<PSP_interface>(_obj);
                }
                else
                {
                    string bdzid = "";
                    string fqid = "";
                    string fxtid = "";

                    foreach (TreeListNode node in treeList1.Nodes)
                    {
                        ArrayList obj = (ArrayList)treeList1.GetDataRecordByNode(node);
                        if (obj[2].ToString() == "True")
                        {
                            bdzid = bdzid + obj[0].ToString() + ",";
                        }
                        
                    }
                    foreach (TreeListNode node in treeList2.Nodes)
                    {
                        ArrayList obj = (ArrayList)treeList2.GetDataRecordByNode(node);
                        if (obj[2].ToString() == "True")
                        {
                            fqid = fqid + obj[0].ToString() + ",";
                        }

                    }
                    foreach (TreeListNode node in treeList3.Nodes)
                    {
                        ArrayList obj = (ArrayList)treeList3.GetDataRecordByNode(node);
                        if (obj[2].ToString() == "True")
                        {
                            fxtid = fxtid + obj[0].ToString() + ",";
                        }

                    }
                    _obj.UYear =Convert.ToInt32( year.Text);
                    _obj.UMonth = month.Text;
                    _obj.Switch_Id = textkgbh.Text;
                    _obj.Switch_Name = textkgmc.Text;
                    _obj.LoadValue =Convert.ToDouble( textfh.Text);
                    _obj.Number = Convert.ToDouble(textdl.Text);
                    _obj.BdzId = bdzid;
                    _obj.FQId = fqid;
                    _obj.FxtId = fxtid;
                    Services.BaseService.Update<PSP_interface>(_obj);
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
