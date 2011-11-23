
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
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
    public partial class FrmLine_InfoDialog_beizhu : FormBase
    {
        private string types1 = "";
        private string types2 = "";
        private string flags1 = "";
        private CtrlLine_beizhu ctrls;

        private IList<Line_beizhu> liss = new List<Line_beizhu>();

        public IList<Line_beizhu> LIST
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


        public CtrlLine_beizhu ctrlLint_Info1
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
        protected Line_beizhu _obj = null;

        #endregion

        #region 构造方法
        public FrmLine_InfoDialog_beizhu()
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
        public Line_beizhu Object
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
                checkEdit1.Visible = false;
            }

            IList<Line_beizhu> list = new List<Line_beizhu>();
            list.Add(Object);
            this.vGridControl.DataSource = list;




            foreach (BaseRow gc in this.vGridControl.Rows)
            {
                if (gc.Properties.FieldName == "")
                    continue;
                if (gc.Properties.FieldName.Substring(0, 1) == "S")
                    gc.Visible = false;

                if (gc.Properties.FieldName.Substring(0, 1) == "K" && types2 == "66")
                    gc.Visible = false;

                if (gc.Properties.FieldName.Substring(0, 1) == "L" && types2 == "10")
                    gc.Visible = false;
            }

            Line_beicong psl = new Line_beicong();
            psl.Flag = flags1;
            psl.Type = types1;
            psl.Type2 = types2;

            IList<Line_beicong> li = Itop.Client.Common.Services.BaseService.GetList<Line_beicong>("SelectLine_beicongByFlagType", psl);



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
                    foreach (Line_beicong pss in li)
                    {

                        if (gc1.Properties.FieldName == pss.ClassType)
                        {
                            gc1.Properties.Caption = pss.Title;
                            gc1.Visible = true;
                        }
                    }
                }

            }

            //MessageBox.Show(liss.Count.ToString());

            Hashtable hs1 = new Hashtable();
            Hashtable hs2 = new Hashtable();
            Hashtable hs3 = new Hashtable();


            foreach (Line_beizhu liii in liss)
            {
                if (!hs1.ContainsValue(liii.K2))
                    hs1.Add(Guid.NewGuid().ToString(), liii.K2);

                if (!hs2.ContainsValue(liii.K3))
                    hs2.Add(Guid.NewGuid().ToString(), liii.K3);

                if (!hs3.ContainsValue(liii.K22))
                    hs3.Add(Guid.NewGuid().ToString(), liii.K22);
            }

            //MessageBox.Show(hs1.Count.ToString());
            //MessageBox.Show(hs2.Count.ToString());
            //MessageBox.Show(hs3.Count.ToString());
            foreach (DictionaryEntry de1 in hs1)
            {
                repositoryItemComboBox1.Items.Add(de1.Value.ToString());
            }

            foreach (DictionaryEntry de2 in hs2)
            {
                repositoryItemComboBox2.Items.Add(de2.Value.ToString());
            }

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
                    ctrls.RefreshData();
                    _obj = new Line_beizhu();
                    _obj.L6 = DateTime.Now;
                    _obj.Flag = flags1;
                    IList<Line_beizhu> list1 = new List<Line_beizhu>();
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

        protected bool SaveRecord()
        {
            //创建/修改 对象



            try
            {
                if (types2 == "66")
                {
                    //if (_obj.L1 == 0)
                    //{
                    //    MsgBox.Show("电压等级没有选择！");
                    //    return false;

                    //}

                    _obj.DY = _obj.L1;
                    // _obj.L1 = 66;
                    _obj.K1 = _obj.L1;
                    _obj.K2 = _obj.L4;
                    _obj.K5 = _obj.L5;
                }
                else if (types2 == "10")
                {
                    //if (_obj.K1 == 0)
                    //{
                    //    MsgBox.Show("电压等级没有选择！");
                    //    return false;

                    //}
                    _obj.DY = _obj.K1;
                    _obj.L1 = _obj.K1;
                    //_obj.K1 = 10;
                    _obj.L4 = _obj.K2;
                    _obj.L5 = _obj.K5;
                }
                //else
                //{



                //}

                _obj.Flag = types1;


                double ss1 = 0;
                double ss2 = 0;
                double ss3 = 0;

                double ss4 = 0;
                double ss5 = 0;

                try
                {
                    ss1 = _obj.K6;
                }
                catch { }
                try
                {
                    ss2 = _obj.K20;
                }
                catch { }
                try
                {
                    ss3 = _obj.K21;
                }
                catch { }
                try
                {
                    ss3 = _obj.K21;
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

                _obj.K13 = Convert.ToInt32(ss4 + ss5);


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
                    m1 = k1 * 100 / k2;

                if (k4 != 0)
                    m2 = k1 * g3 * k3 * 100 / k4;

                _obj.K19 = m1;
                _obj.K17 = m2;








                if (IsCreate)
                {
                    Services.BaseService.Create<Line_beizhu>(_obj);
                }
                else
                {
                    Services.BaseService.Update<Line_beizhu>(_obj);
                }
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return false;
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
