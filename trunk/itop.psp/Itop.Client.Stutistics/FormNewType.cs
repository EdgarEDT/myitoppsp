using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Stutistic;
using Itop.Common;
using Itop.Client.Base;

namespace Itop.Client.Stutistics
{
    public partial class FormNewType : DevExpress.XtraEditors.XtraForm
    {
        private string type = "";
        private string flag2 = "";
        private bool _getYearOnly = false;

        public bool GetYearOnly
        {
            get { return _getYearOnly; }
            set { _getYearOnly = value; }
        }

        public string Flag2
        {
            get { return flag2; }
            set { flag2 = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public FormNewType()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            PowerStuffYears psp_Year = new PowerStuffYears();
            psp_Year.Flag = flag2;
            type = psp_Year.Year = spinEdit1.Text;

            if (type.ToString() == "")
            {
                MsgBox.Show("类别名称不能为空");
                return;
            
            }


            if(_getYearOnly)
            {
                this.DialogResult = DialogResult.OK;
                return;
            }

            try
            {
                if (Common.Services.BaseService.GetObject("SelectPowerStuffYearsByYearFlag", psp_Year) == null)
                {
                    try
                    {
                        Common.Services.BaseService.Create<PowerStuffYears>(psp_Year);
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("出错啦：" + ex.Message);
                    }
                }
                else
                {
                    MsgBox.Show("此分类已经存在，请重新输入！");
                }
            }
            catch(Exception ex)
            {
                MsgBox.Show("出错啦：" + ex.Message);
            }

        }

        private void FormNewYear_Load(object sender, EventArgs e)
        {
           // spinEdit1.Focus();
            spinEdit1.Text = type;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }
    }
}