
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
using DevExpress.XtraEditors;
using Itop.Client.Base;
namespace Itop.Client.Chen
{
	public partial class FrmPSP_VolumeBalance_CalcDialog : FormBase
	{

		#region 字段
		protected bool _isCreate = false;
        private string  type = "";
		protected PSP_VolumeBalance_Calc _obj = null;
        string formtitle = "";
        /// <summary>
        /// 获取type对象
        /// </summary>
        public string FormTitle
        {
            get { return formtitle; }
            set { formtitle = value; }
        }

        string ctrtitle = "";
        /// <summary>
        /// 获取flag对象
        /// </summary>
        public string CtrTitle
        {
            get { return ctrtitle; }
            set { ctrtitle = value; }
        }

		#endregion

		#region 构造方法
		public FrmPSP_VolumeBalance_CalcDialog()
		{
			InitializeComponent();
		}
		#endregion

		#region 公共属性



        Hashtable hs = new Hashtable();

		/// <summary>
		/// true:创建对象　false:修改对象
		/// </summary>
		public bool IsCreate
		{
			get { return _isCreate; }
			set { _isCreate = value; }
		}
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
		/// <summary>
		/// 所邦定的对象
		/// </summary>
		public PSP_VolumeBalance_Calc Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region 事件处理
		private void FrmPSP_VolumeBalance_CalcDialog_Load(object sender, EventArgs e)
		{
			IList<PSP_VolumeBalance_Calc> list = new List<PSP_VolumeBalance_Calc>();
            ArrayList sort = new ArrayList();
			list.Add(Object);
			this.vGridControl.DataSource = list;
            if (type == "1")
            {
                this.rowLX1.Visible = false;
                this.rowLX2.Visible = false;
                //this.vGridControl.Rows["rowLX2"].Visible;
            }
            if (IsCreate && type != "1")
            _obj.LX1 = "新建";
            switch (_obj.Col1)
            { 
                case "220":
                    hs.Add("12×1", 12);
                    hs.Add("12×2", 24);
                    hs.Add("15×1", 15);
                    hs.Add("15×2", 30);
                    hs.Add("18×1", 18);
                    hs.Add("18×2", 36);
                    hs.Add("18×3", 54);
                    hs.Add("18×4", 72);
                    hs.Add("18×5", 90);
                    hs.Add("12+15", 27);
                    hs.Add("12+18", 30);
                    hs.Add("15+18", 33);


                    sort.Add("12×1");
                    sort.Add("12×2");
                    sort.Add("15×1");
                    sort.Add("15×2");
                    sort.Add("18×1");
                    sort.Add("18×2");
                    sort.Add("18×3");
                    sort.Add("18×4");
                    sort.Add("12+15");
                    sort.Add("12+18");
                    sort.Add("15+18");
                    hs.Add("其他", 0);
                    if (IsCreate && type != "1")
                    {
                        _obj.LX2 = "18×1";
                        _obj.Vol = 18;
                    }
                    break;
                case "110":
                    hs.Add("4×1", 4);
                    hs.Add("4×2", 8);
                    hs.Add("5×1", 5);
                    hs.Add("5×2", 10);
                    hs.Add("6.3×1", 6.3);
                    hs.Add("6.3×2", 12.6);
                    hs.Add("4+5", 9);
                    hs.Add("4+6.3", 10.3);
                    hs.Add("5+6.3", 11.3);

                    sort.Add("4×1");
                    sort.Add("4×2");
                    sort.Add("5×1");
                    sort.Add("5×2");
                    sort.Add("6.3×1");
                    sort.Add("6.3×2");
                    sort.Add("4+5");
                    sort.Add("4+6.3");
                    sort.Add("5+6.3");
                    hs.Add("其他", 0);
                    if (IsCreate && type != "1")
                    {
                        _obj.LX2 = "5×1";
                        _obj.Vol = 5;
                    }
                    break;
                case "35":
                    hs.Add("0.5×1", 0.5);
                    hs.Add("0.5×2", 1);
                    hs.Add("1.0×1", 1);
                    hs.Add("1.0×2", 2);

                    sort.Add("0.5×1");
                    sort.Add("0.5×2");
                    sort.Add("1.0×1");
                    sort.Add("1.0×2");
                    if (IsCreate && type != "1")
                    {
                        _obj.LX2 = "1.0×2";
                        _obj.Vol = 2;
                    }
                    hs.Add("其他", 0);
                    break;
            }
            sort.Add("其他");
            foreach (string de in sort)
            {
                repositoryItemComboBox2.Items.Add(de);
            }
            this.Text = formtitle;
            this.xtraTabControl1.TabPages[0].Text = ctrtitle;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (!InputCheck())
			{
				return;
			}
            if(_obj.Title=="")
            {
                MessageBox.Show("项目名称不能为空");
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
					Services.BaseService.Create<PSP_VolumeBalance_Calc>(_obj);
				}
				else
				{
					Services.BaseService.Update<PSP_VolumeBalance_Calc>(_obj);
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

        private void repositoryItemComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBoxEdit cb = (ComboBoxEdit)sender;
                _obj.Vol = Convert.ToDouble(hs[cb.Text]);
                this.textBox1.Focus();
              
            }
            catch { }
        }

        private void repositoryItemComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
             ComboBoxEdit cb = (ComboBoxEdit)sender;
               ArrayList sort = new ArrayList();
             hs.Clear();
             repositoryItemComboBox2.Items.Clear();
            switch (_obj.Col1)
            {
                case "220":
                    if (cb.Text == "增容")
                    {
                        hs.Add("12->15", 3);
                        hs.Add("12->18", 6);
                        hs.Add("15->18", 3);
                        sort.Add("12->15");
                        sort.Add("12->18");
                        sort.Add("15->18");
                    }
                    else
                    {
                        hs.Add("12×1", 12);
                        hs.Add("12×2", 24);
                        hs.Add("15×1", 15);
                        hs.Add("15×2", 30);
                        hs.Add("18×1", 18);
                        hs.Add("18×2", 36);
                        hs.Add("18×3", 54);
                        hs.Add("18×4", 72);
                        hs.Add("12+15", 27);
                        hs.Add("12+18", 30);
                        hs.Add("15+18", 33);

                        
                        sort.Add("12×1");
                        sort.Add("12×2");
                        sort.Add("15×1");
                        sort.Add("15×2");
                        sort.Add("18×1");
                        sort.Add("18×2");
                        sort.Add("18×3");
                        sort.Add("18×4");
                        sort.Add("18×5");
                        sort.Add("12+15");
                        sort.Add("12+18");
                        sort.Add("15+18");
                    }
                    break;
                case "110":
                    if (cb.Text != "增容")
                    {
                        hs.Add("4×1", 4);
                        hs.Add("4×2", 8);
                        hs.Add("5×1", 5);
                        hs.Add("5×2", 10);
                        hs.Add("6.3×1", 6.3);
                        hs.Add("6.3×2", 12.6);
                        hs.Add("4+5", 9);
                        hs.Add("4+6.3", 10.3);
                        hs.Add("5+6.3", 11.3);

                        sort.Add("4×1");
                        sort.Add("4×2");
                        sort.Add("5×1");
                        sort.Add("5×2");
                        sort.Add("6.3×1");
                        sort.Add("6.3×2");
                        sort.Add("4+5");
                        sort.Add("4+6.3");
                        sort.Add("5+6.3");
                        
                    }   
                    else
                    {
                        hs.Add("4->5", 1);
                        hs.Add("4->6.3", 2.3);
                        hs.Add("5->6.3", 1.3);

                        sort.Add("4->5");
                        sort.Add("4->6.3");
                        sort.Add("5->6.3");
                    }
                    break;
                case "35":
                    if (cb.Text != "增容")
                    {
                        hs.Add("0.5×1", 0.5);
                        hs.Add("0.5×2",1);
                        hs.Add("1.0×1", 1);
                        hs.Add("1.0×2", 2);

                        sort.Add("0.5×1");
                        sort.Add("0.5×2");
                        sort.Add("1.0×1");
                        sort.Add("1.0×2");

                    }
                    else
                    {
                        hs.Add("0.2->0.8", 0.6);
                        hs.Add("0.2->1.0", 0.8);
                        hs.Add("0.25->1.0", 0.75);
                        hs.Add("0.315->1.0", 0.685);
                        hs.Add("0.4->1.0", 0.6);
                        hs.Add("0.5->1.0", 0.5);
                        hs.Add("0.63->1.0", 0.37);
                        hs.Add("1.0->1.6", 0.6);


                        sort.Add("0.2->0.8");
                        sort.Add("0.2->1.0");
                        sort.Add("0.25->1.0");
                        sort.Add("0.315->1.0");
                        sort.Add("0.4->1.0");
                        sort.Add("0.5->1.0");
                        sort.Add("0.63->1.0");
                        sort.Add("1.0->1.6");
                    }
                    break;
            }
            hs.Add("其他", 0);
            sort.Add("其他");
            //foreach (DictionaryEntry de in hs)
            //{
            //    repositoryItemComboBox2.Items.Add(de.Key.ToString());
            //}
            for(int i=0;i<sort.Count;i++)
                repositoryItemComboBox2.Items.Add(sort[i].ToString());



            //ComboBoxEdit cb = (ComboBoxEdit)repositoryItemComboBox2;
            //cb.Properties.Sorted = true;
            
            _obj.LX1 = cb.Text;
            this.textBox1.Focus();
        }
	}
}
