
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
using DevExpress.XtraEditors;
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
    public partial class FrmPSP_PlanTable_HuaiBeiDialog : FormBase
    {
        private string types1 = "";
        private string types2 = "";
        private string flags1 = "";
        private string deleteUID = "";
        private CtrlItemPlanTable_HuaiBei ctrls;

        public CtrlItemPlanTable_HuaiBei ctrlItemPlanTable_HuaiBei
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

        #region 字段
        protected bool _isCreate = false;
        protected PSP_PlanTable_HuaiBei _obj = null;
        #endregion

        #region 构造方法
        public FrmPSP_PlanTable_HuaiBeiDialog()
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
        public PSP_PlanTable_HuaiBei Object
        {
            get { return _obj; }
            set { _obj = value; }
        }
        #endregion

        #region 事件处理
        private void FrmPSP_PlanTable_HuaiBeiDialog_Load(object sender, EventArgs e)
        {
            if (isselect)
                this.vGridControl.Enabled = false;

            foreach (BaseRow gc in this.vGridControl.Rows)
            {
                if (gc.Properties.FieldName.Substring(0, 1) == "S")
                    gc.Visible = false;
                if (gc.Properties.FieldName.Substring(0, 2) == "SB")
                    gc.Visible = true;

            }
            if (IsCreate)
            {
                ////this.rowBD220.Enabled = false;
                ////this.rowLine220.Enabled = false;
                ////this.rowLine110.Enabled = false;
                ////this.rowBD110.Enabled = false;
                if (_obj.KeyFlag == "time1" || _obj.KeyFlag == "time2" || _obj.KeyFlag == "time3")
                {
                    _obj.ItemSB = "0";
                    _obj.ItemPF = "0";
                    _obj.KYWC = "0";
                    _obj.KYPS = "0";
                    _obj.PSYJ = "0";
                    ////_obj.CSSC = "0";
                    ////_obj.JSGFSSB = "0";
                    ////_obj.ZBSB = "0";
                    ////_obj.ZBSHDateTime = "0";
                    _obj.XZYJS = "0";
                    _obj.HPPF = "0";
                    _obj.TDYS = "0";
                    _obj.SBHZSQ = "0";
                    _obj.XMHZ = "0";
                    _obj.JHKSDateTime = "0";
                    _obj.JHTCDateTime = "0";
                   // _obj.JHTCDateTime_GuiHua = "0";
                }
                foreach (BaseRow gc in this.vGridControl.Rows)
                {
                    if (gc.Properties.FieldName == "ItemSB" || gc.Properties.FieldName == "ItemPF" || gc.Properties.FieldName == "KYWC" || gc.Properties.FieldName == "KYPS" || gc.Properties.FieldName == "PSYJ")
                        gc.Visible = false;
                    //if (gc.Properties.FieldName == "CSSC" || gc.Properties.FieldName == "JSGFSSB" || gc.Properties.FieldName == "ZBSB" || gc.Properties.FieldName == "ZBSHDateTime")
                    //    gc.Visible = false;
                    if (gc.Properties.FieldName == "XZYJS" || gc.Properties.FieldName == "HPPF" || gc.Properties.FieldName == "TDYS" || gc.Properties.FieldName == "SBHZSQ" || gc.Properties.FieldName == "XMHZ" || gc.Properties.FieldName == "JHKSDateTime" || gc.Properties.FieldName == "JHTCDateTime")
                        gc.Visible = false;

                }
            }
            IList<PSP_PlanTable_HuaiBei> list = new List<PSP_PlanTable_HuaiBei>();

            list.Add(Object);
            this.vGridControl.DataSource = list;
            //if (!(_obj.KeyFlag == "time1" || _obj.KeyFlag == "time2" || _obj.KeyFlag == "time3" || _obj.KeyFlag == "220" || _obj.KeyFlag == "110" || _obj.KeyFlag == "kuojian"))
            //{
            //     foreach (BaseRow gc in this.vGridControl.Rows)
            //    {
            //        if (gc.Properties.FieldName == "ItemSB" || gc.Properties.FieldName == "ItemPF" || gc.Properties.FieldName == "KYWC" || gc.Properties.FieldName == "KYPS" || gc.Properties.FieldName == "PSYJ" )
            //            gc.Visible = false;
            //        if (gc.Properties.FieldName == "CSSC" || gc.Properties.FieldName == "JSGFSSB" || gc.Properties.FieldName == "ZBSB" || gc.Properties.FieldName == "ZBSHDateTime" )
            //            gc.Visible = false;
            //        if (gc.Properties.FieldName == "XZYJS" || gc.Properties.FieldName == "HPPF" || gc.Properties.FieldName == "TDYS" || gc.Properties.FieldName == "SBHZSQ" || gc.Properties.FieldName == "XMHZ" || gc.Properties.FieldName == "JHKSDateTime" || gc.Properties.FieldName == "JHTCDateTime")
            //            gc.Visible = false;

            //    }
            //}
            if (_obj.ParentID == "220")
            {
                foreach (BaseRow gc in this.vGridControl.Rows)
                {
                    if (gc.Properties.FieldName == "ItemSB" || gc.Properties.FieldName == "ItemPF" || gc.Properties.FieldName == "KYWC" || gc.Properties.FieldName == "KYPS" || gc.Properties.FieldName == "PSYJ")
                        gc.Visible = false;
                    //if (gc.Properties.FieldName == "CSSC" || gc.Properties.FieldName == "JSGFSSB" || gc.Properties.FieldName == "ZBSB" || gc.Properties.FieldName == "ZBSHDateTime")
                    //    gc.Visible = false;
                    if (gc.Properties.FieldName == "XZYJS" || gc.Properties.FieldName == "HPPF" || gc.Properties.FieldName == "TDYS" || gc.Properties.FieldName == "SBHZSQ" || gc.Properties.FieldName == "XMHZ" || gc.Properties.FieldName == "JHKSDateTime" || gc.Properties.FieldName == "JHTCDateTime")
                        gc.Visible = false;
                    if (gc.Properties.FieldName == "Line110" || gc.Properties.FieldName == "BD110")
                        gc.Visible = false;
                }
            }
            if (_obj.ParentID == "110")
            {
                foreach (BaseRow gc in this.vGridControl.Rows)
                {
                    if (gc.Properties.FieldName == "ItemSB" || gc.Properties.FieldName == "ItemPF" || gc.Properties.FieldName == "KYWC" || gc.Properties.FieldName == "KYPS" || gc.Properties.FieldName == "PSYJ")
                        gc.Visible = false;
                    //if (gc.Properties.FieldName == "CSSC" || gc.Properties.FieldName == "JSGFSSB" || gc.Properties.FieldName == "ZBSB" || gc.Properties.FieldName == "ZBSHDateTime")
                    //    gc.Visible = false;
                    if (gc.Properties.FieldName == "XZYJS" || gc.Properties.FieldName == "HPPF" || gc.Properties.FieldName == "TDYS" || gc.Properties.FieldName == "SBHZSQ" || gc.Properties.FieldName == "XMHZ" || gc.Properties.FieldName == "JHKSDateTime" || gc.Properties.FieldName == "JHTCDateTime")
                        gc.Visible = false;
                    if (gc.Properties.FieldName == "Line220" || gc.Properties.FieldName == "BD220")
                        gc.Visible = false;
                }
            }
            if (_obj.KeyFlag == "220" || _obj.KeyFlag == "110" || _obj.KeyFlag == "kuojian")
            {
                foreach (BaseRow gc in this.vGridControl.Rows)
                {
                    if (gc.Properties.FieldName == "BD220" || gc.Properties.FieldName == "BD110" || gc.Properties.FieldName == "Line110" || gc.Properties.FieldName == "Line220")
                        gc.Visible = false;
                    if (gc.Properties.FieldName == "Contents" || gc.Properties.FieldName == "DeptName" || gc.Properties.FieldName == "DY" || gc.Properties.FieldName == "TZGM")
                        gc.Visible = false;
                    if (gc.Properties.FieldName == "ItemSB" || gc.Properties.FieldName == "ItemPF" || gc.Properties.FieldName == "KYWC" || gc.Properties.FieldName == "KYPS" || gc.Properties.FieldName == "PSYJ")
                        gc.Visible = false;
                    //if (gc.Properties.FieldName == "CSSC" || gc.Properties.FieldName == "JSGFSSB" || gc.Properties.FieldName == "ZBSB" || gc.Properties.FieldName == "ZBSHDateTime")
                    //    gc.Visible = false;
                    if (gc.Properties.FieldName == "XZYJS" || gc.Properties.FieldName == "HPPF" || gc.Properties.FieldName == "TDYS" || gc.Properties.FieldName == "SBHZSQ" || gc.Properties.FieldName == "XMHZ" || gc.Properties.FieldName == "JHKSDateTime" || gc.Properties.FieldName == "JHTCDateTime")
                        gc.Visible = false;
                }
            }


            if (_obj.KeyFlag == "time1" || _obj.KeyFlag == "time2" || _obj.KeyFlag == "time3")
            {
                foreach (BaseRow gc in this.vGridControl.Rows)
                {
                    if (gc.Properties.FieldName == "BD220" || gc.Properties.FieldName == "BD110" || gc.Properties.FieldName == "Line110" || gc.Properties.FieldName == "Line220")
                        gc.Visible = false;
                    if (gc.Properties.FieldName == "Contents" || gc.Properties.FieldName == "DeptName" || gc.Properties.FieldName == "DY" || gc.Properties.FieldName == "TZGM" || gc.Properties.FieldName == "JHTCDateTime_GuiHua")
                        gc.Visible = false;
                    //if (gc.Properties.FieldName == "CSSC" || gc.Properties.FieldName == "JSGFSSB" || gc.Properties.FieldName == "ZBSB" || gc.Properties.FieldName == "ZBSHDateTime")
                    //    gc.Visible = false;
                }
            }
            if (_obj.KeyFlag == "time1" || _obj.KeyFlag == "time2" || _obj.KeyFlag == "time3" || _obj.KeyFlag == "220" || _obj.KeyFlag == "110" || _obj.KeyFlag == "kuojian" || _obj.KeyFlag == "220千伏" || _obj.KeyFlag == "110千伏")
            {

                foreach (BaseRow gc in vGridControl.Rows)
                {
                    if (gc.Properties.FieldName == "Title")
                        gc.Properties.ReadOnly = true;
                }
            }
            if (_obj.KeyFlag == "220千伏" || _obj.KeyFlag == "110千伏")
            {
                foreach (BaseRow gc in this.vGridControl.Rows)
                {
                    if (gc.Properties.FieldName == "BD220" || gc.Properties.FieldName == "BD110" || gc.Properties.FieldName == "Line110" || gc.Properties.FieldName == "Line220")
                        gc.Visible = false;
                    if (gc.Properties.FieldName == "TZGM" || gc.Properties.FieldName == "JHTCDateTime_GuiHua")
                        gc.Visible = false;
                    if (gc.Properties.FieldName == "ItemSB" || gc.Properties.FieldName == "ItemPF" || gc.Properties.FieldName == "KYWC" || gc.Properties.FieldName == "KYPS" || gc.Properties.FieldName == "PSYJ")
                        gc.Visible = false;
                    //if (gc.Properties.FieldName == "CSSC" || gc.Properties.FieldName == "JSGFSSB" || gc.Properties.FieldName == "ZBSB" || gc.Properties.FieldName == "ZBSHDateTime")
                    //    gc.Visible = false;
                    if (gc.Properties.FieldName == "XZYJS" || gc.Properties.FieldName == "HPPF" || gc.Properties.FieldName == "TDYS" || gc.Properties.FieldName == "SBHZSQ" || gc.Properties.FieldName == "XMHZ" || gc.Properties.FieldName == "JHKSDateTime" || gc.Properties.FieldName == "JHTCDateTime")
                        gc.Visible = false;
                }
            }

            PowerSubstationLine psl = new PowerSubstationLine();
            psl.Flag = flags1;
            psl.Type = types1;
            psl.Type2 = types2;
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


            if (_obj.KeyFlag == "time1" || _obj.KeyFlag == "time2" || _obj.KeyFlag == "time3" || _obj.KeyFlag == "220" || _obj.KeyFlag == "110" || _obj.KeyFlag == "kuojian" || _obj.KeyFlag == "220千伏" || _obj.KeyFlag == "110千伏")
            {
            }
            else
            {
                if (_obj.DY == "220")
                {
                    _obj.ParentID = "220";
                }
                if (_obj.DY == "110")
                {
                    _obj.ParentID = "110";
                }
                if (_obj.DY == "")
                {
                    MsgBox.Show("请选择电压等级！");
                    return;
                }
                if (Convert.ToDateTime( _obj.JHTCDateTime_GuiHua).Year<=1900)
                {
                    MsgBox.Show("请填写计划投产时间（规划），必填！");
                    return;
                }
            }

            if (SaveRecord())
            {
                //if (checkEdit1.Checked)
                //{
                //    ctrls.RefreshData();
                //    _obj = new PSP_PlanTable_HuaiBei();
                //    _obj.Flag = flags1;
                //    _obj.CreateDate = DateTime.Now;
                //    IList<PSP_PlanTable_HuaiBei> list1 = new List<PSP_PlanTable_HuaiBei>();
                //    list1.Add(_obj);
                //    this.vGridControl.DataSource = list1;
                //}
                //else
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
                if (IsCreate)
                {
                    _obj.UID = Guid.NewGuid().ToString();
                    Services.BaseService.Create<PSP_PlanTable_HuaiBei>(_obj);
                }
                else
                {
                    // deleteUID = _obj.UID;
                    //  _obj.UID = Guid.NewGuid().ToString();
                    Services.BaseService.Update("UpdatePSP_PlanTable_HuaiBeiByUID", _obj);
                    //  Services.BaseService.Update("DeletePSP_PowerSubstationInfoByUID", deleteUID);
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

    

        private void repositoryItemDateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //DateEdit de = (DateEdit)sender;
            //Object.JSGFSSB = de.DateTime.ToString("yyyy年MM月");
        }
        private void repositoryItemDateEdit2_EditValueChanged(object sender, EventArgs e)
        {
            //DateEdit de = (DateEdit)sender;
            //Object.JSGFSSB = de.DateTime.Year.ToString() + "年" + de.DateTime.Month.ToString() +"月" + de.DateTime.Day.ToString()+"日";
        }
        private void repositoryItemDateEdit3_EditValueChanged(object sender, EventArgs e)
       {
        //    DateEdit de = (DateEdit)sender;
        //    Object.ZBSB = de.DateTime.Year.ToString() + "年" + de.DateTime.Month.ToString() + "月" + de.DateTime.Day.ToString() + "日";
        }

        private void repositoryItemDateEdit4_EditValueChanged(object sender, EventArgs e)
        {
            //DateEdit de = (DateEdit)sender;
            //Object.ZBSHDateTime = de.DateTime.Year.ToString() + "年" + de.DateTime.Month.ToString() + "月" + de.DateTime.Day.ToString() + "日";
        }

        private void ItemDateEditJHTCDateTime_GuiHua_EditValueChanged(object sender, EventArgs e)
        {
            //DateEdit de = (DateEdit)sender;
            //Object.JHTCDateTime_GuiHua = de.DateTime.ToString("yyyy年MM月");
        }

        private void vGridControl_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            if (e.Row.Properties.FieldName.ToString() == "DY")
            {
                _obj.DY=e.Value.ToString();

                if (_obj.DY == "220")
                {

                    this.rowBD220.Enabled = true;
                    this.rowLine220.Enabled = true;
                    this.rowLine110.Enabled = false;
                    this.rowBD110.Enabled = false;

                }
                if (_obj.DY =="110")
                {
                    this.rowBD220.Enabled = false;
                    this.rowLine220.Enabled = false;
                    this.rowLine110.Enabled = true;
                    this.rowBD110.Enabled = true;

                }
            }
        }

        private void ItemTextEditBD110_Click(object sender, EventArgs e)
        {
            if (_obj.DY != "110" && _obj.DY != "220")
                MsgBox.Show("请选择下面的电压值之后，在进行此操作！");
        }

        private void ItemTextEditBD220_Click(object sender, EventArgs e)
        {
            if (_obj.DY != "110" && _obj.DY != "220")
                MsgBox.Show("请选择下面的电压值之后，在进行此操作！");
                
        }

   

        private void ItemTextEditLine110_Click(object sender, EventArgs e)
        {
            if (_obj.DY != "110" && _obj.DY != "220")
                MsgBox.Show("请选择下面的电压值之后，在进行此操作！");
        }

        private void ItemTextEditLine220_Click(object sender, EventArgs e)
        {
            if (_obj.DY != "110" && _obj.DY != "220")
                MsgBox.Show("请选择下面的电压值之后，在进行此操作！");
        }
    }
}
