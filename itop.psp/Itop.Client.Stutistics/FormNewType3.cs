﻿using System;
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
    public partial class FormNewType3 : DevExpress.XtraEditors.XtraForm
    {
        private string type = "";
        private string flag2 = "";
        private bool _getYearOnly = false;
        private bool isUpdate = false;
        PowerProYears psp_Year = null;
        private string type1 = "";
        private string year2 = "";

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

        public string Type1
        {
            get { return type1; }
            set { type1 = value; }
        }


        public bool IsUpdate
        {
            get { return isUpdate; }
            set { isUpdate = value; }
        }

        public FormNewType3()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
            type = psp_Year.Year = spinEdit1.Text;
            if (type.ToString() == "")
            {
                MsgBox.Show("类别名称不能为空");
                return;
            
            }

            try
            {

                if (!IsUpdate)
                {

                    if (Common.Services.BaseService.GetObject("SelectPowerProYearsByYearFlag", psp_Year) == null)
                    {
                        try
                        {
                            Common.Services.BaseService.Create<PowerProYears>(psp_Year);
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
                else
                {
                    try
                    {
                        


                        PowerProTypes psp_Type = new PowerProTypes();
                        psp_Type.Flag2 = type1;
                        IList<PowerProTypes> listTypes = new List<PowerProTypes>();
                        try
                        {
                            listTypes = Common.Services.BaseService.GetList<PowerProTypes>("SelectPowerProTypesByFlag2", psp_Type);
                        }
                        catch (Exception ex)
                        { MsgBox.Show(ex.Message); }



                        foreach (PowerProTypes pstt in listTypes)
                        {
                            PowerProValues psv = new PowerProValues();
                            psv.TypeID = pstt.ID;
                            psv.Year = psp_Year.Year;
                            psv.Value = year2;
                            psv.TypeID1 = flag2;
                            Common.Services.BaseService.Update("UpdatePowerProValuesByYear", psv);
                        }


                        Common.Services.BaseService.Update<PowerProYears>(psp_Year);
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("出错啦：" + ex.Message);
                    }

                }
            }
            catch(Exception ex)
            {
                MsgBox.Show("出错啦：" + ex.Message);
            }

        }

        private void FormNewYear_Load(object sender, EventArgs e)
        {
            spinEdit1.Text = type;
            year2 = type;
            if (!IsUpdate)
            {
                psp_Year = new PowerProYears();
                psp_Year.Flag = flag2;
            }
            else
            { 
                PowerProYears ps=new PowerProYears();
                ps.Flag=flag2;
                ps.Year=type;
                psp_Year = (PowerProYears)Common.Services.BaseService.GetObject("SelectPowerProYearsByYearFlag", ps);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }



    }
}