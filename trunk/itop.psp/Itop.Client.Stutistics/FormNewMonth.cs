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
    public partial class FormNewMonth : FormBase
    {
        private int yearvalue;
        private int monthvalue;
        private int flag;
        private int flag2;
        private int itemid;
        public int YearValue
        {
            set { yearvalue = value; }
            get { return yearvalue; }
        }
        public int MonthValue
        {
            set { monthvalue = value; }
            get { return monthvalue; }
        }
        public int Flag
        {
            set { flag = value; }
            get { return flag; }
        }
        public int Flag2
        {
            set { flag2 = value; }
            get { return flag2; }
        }
        public int ItemID
        {
            set { itemid = value; }
            get { return itemid; }
        }
            public FormNewMonth()
        {
            InitializeComponent();
        }

        private void FormNewMonth_Load(object sender, EventArgs e)
        {

            
                spinEdit2.Value = monthvalue;       //spinEdit2 �·�
                spinEdit2.Properties.MaxValue = 12;
                spinEdit2.Properties.MinValue = 1;
                spinEdit1.Value = yearvalue;        //spinEdit1 ���
                spinEdit1.Properties.MaxValue = yearvalue+20;
                spinEdit1.Properties.MinValue = yearvalue-20;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            yearvalue = (int)spinEdit1.Value;
            monthvalue = (int)spinEdit2.Value;
            PSP_BigUser_Years biguseryear = new PSP_BigUser_Years();
       //     biguseryear.Year = yearvalue + "��" + monthvalue + "��";
            biguseryear.Year =  monthvalue.ToString() ;
            biguseryear.Flag = flag;
            biguseryear.ItemID = itemid;
            try {
                if (Common.Services.BaseService.GetObject("SelectPSP_BigUser_YearsByYearFlag", biguseryear) == null)
                {
                    try
                    {
                        Common.Services.BaseService.Create<PSP_BigUser_Years>(biguseryear);
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("��������" + ex.Message);
                    }
                }
                else
                {
                    MsgBox.Show("�����·��Ѿ����ڣ����������룡");

                }
                
            }
            catch
            {
               
            }
        }
    }
}