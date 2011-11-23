using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Domain.Forecast;
using Itop.Client.Common;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraEditors;
using Itop.Client.Base;
namespace Itop.Client.Forecast
{
    public partial class FormForecastCalc10 : FormBase
    {
        public FormForecastCalc10()
        {
            InitializeComponent();
        }
        
        IList<Ps_Calc> list1 = new List<Ps_Calc>();
        Ps_Calc pc1 = new Ps_Calc();
        private bool isedit=false;
        int type = 10;
        DataTable dt = null;

        DataRow newrow2 = null;

        public bool ISEdit
        {

            set { isedit = value; }
        }
        Ps_forecast_list forecastReport;
        public Ps_forecast_list PForecastReports
        {
            get { return forecastReport; }
            set { forecastReport = value; }
        }

        DataTable dataTable;
        public DataTable DTable
        {
            get { return dataTable; }
            set { dataTable = value; }
        }
        Hashtable ha=new Hashtable ();
        public Hashtable Ha
        {
            get { return ha; }
            set { ha = value; }
        }

        private void HideToolBarButton()
        {

            if (!isedit)
            {
                //vGridControl2.Enabled = false;
                //simpleButton1.Visible = false;
            }
          
        }

        private void FormForecastCalc10_Load(object sender, EventArgs e)
        {
            //int firstyear = 0;
            //int endyear = 0;

            //Ps_Forecast_Setup pfs = new Ps_Forecast_Setup();
            //pfs.Forecast = type;
            //pfs.ForecastID = forecastReport.ID;

            //IList<Ps_Forecast_Setup> li = Services.BaseService.GetList<Ps_Forecast_Setup>("SelectPs_Forecast_SetupByForecast", pfs);

            //if (li.Count != 0)
            //{
            //    firstyear = li[0].StartYear;
            //    endyear = li[0].EndYear;
            //}

            Ps_Calc pcs = new Ps_Calc();
            pcs.Forecast = type;
            pcs.ForecastID = forecastReport.ID;
            list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);
            foreach (Ps_Calc pcs2 in list1)
            {
              
               if(checkEdit1.Text==pcs2.CalcID)
               {
                   if (pcs2.Value1 > 0)
                   {
                       checkEdit1.Checked = true;
                       spinEdit1.Visible = true;
                   }
                   else
                   {
                       checkEdit1.Checked = false;
                       spinEdit1.Visible = false;
                   }

                   spinEdit1.Value = OverMaxValue(pcs2.Value2, 1);
                
               }
               else
                   if(checkEdit2.Text==pcs2.CalcID)
                {
                    if (pcs2.Value1 > 0)
                    {

                        checkEdit2.Checked = true;
                        spinEdit2.Visible = true;
                    }
                    else
                    {
                        checkEdit2.Checked = false;
                        spinEdit2.Visible = false;
                    }

                    spinEdit2.Value = OverMaxValue(pcs2.Value2, 2);
                }
                else
                   if( checkEdit3.Text==pcs2.CalcID)
                   {
                       if (pcs2.Value1 > 0)
                       {
                           checkEdit3.Checked = true;
                           spinEdit3.Visible = true;
                       }
                       else
                       {
                           checkEdit3.Checked = false;
                           spinEdit3.Visible = false;
                       }

                       spinEdit3.Value = OverMaxValue(pcs2.Value2, 3);
                   }
                else
                   if(checkEdit4.Text==pcs2.CalcID)
                   {
                       if (pcs2.Value1 > 0)
                       {
                           checkEdit4.Checked = true;
                           spinEdit4.Visible = true;
                       }
                       else
                       {

                           checkEdit4.Checked = false;
                           spinEdit4.Visible = false;
                       }

                       spinEdit4.Value = OverMaxValue(pcs2.Value2, 4);
                   }
                else
                   if(checkEdit5.Text==pcs2.CalcID)
                   {
                       if (pcs2.Value1 > 0)
                       {

                           checkEdit5.Checked = true;
                           spinEdit5.Visible = true;
                       }
                       else
                       {
                           checkEdit5.Checked = false;
                           spinEdit5.Visible = false;
                       }

                       spinEdit5.Value = OverMaxValue(pcs2.Value2, 5);
                   }
                else
                   if( checkEdit6.Text==pcs2.CalcID)
                   {
                       if (pcs2.Value1 > 0)
                       {
                           checkEdit6.Checked = true;
                           spinEdit6.Visible = true;
                       }
                       else
                       {
                           checkEdit6.Checked = false;
                           spinEdit6.Visible = false;
                       }

                       spinEdit6.Value = OverMaxValue(pcs2.Value2, 6);
                   }
                else
                   if(checkEdit7.Text==pcs2.CalcID)
                   {
                       if (pcs2.Value1 > 0)
                       {
                           checkEdit7.Checked = true;
                           spinEdit7.Visible = true;
                       }
                       else
                       {
                           checkEdit7.Checked = false;
                           spinEdit7.Visible = false;
                       }

                       spinEdit7.Value = OverMaxValue(pcs2.Value2, 7);
                   }
                //else
                //   if( checkEdit8.Text==pcs2.CalcID)
                //   {
                //       if (pcs2.Value1 > 0)
                //       {
                //           checkEdit8.Checked = true;
                //           spinEdit8.Visible = true;
                //       }
                //       else
                //       {
                //           checkEdit8.Checked = false;

                //           spinEdit8.Visible = false;
                //       }

                //       spinEdit8.Value = OverMaxValue(pcs2.Value2, 8);
                //   }
                
            }

         
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ha.Clear();
            if (spinEdit1.Visible)
            {
                ha.Add(checkEdit1.Text, spinEdit1.Value);
              
            }
            savevalue(checkEdit1.Text, spinEdit1.Value, spinEdit1.Visible);
            if (spinEdit2.Visible)
            {
                ha.Add(checkEdit2.Text, spinEdit2.Value);
                
            }
            savevalue(checkEdit2.Text, spinEdit2.Value, spinEdit2.Visible);
            if (spinEdit3.Visible)
            {
                ha.Add(checkEdit3.Text, spinEdit3.Value);
                
            }
            savevalue(checkEdit3.Text, spinEdit3.Value, spinEdit3.Visible);
            if (spinEdit4.Visible)
            {
                ha.Add(checkEdit4.Text, spinEdit4.Value);
                
            }
            savevalue(checkEdit4.Text, spinEdit4.Value, spinEdit4.Visible);
            if (spinEdit5.Visible)
            {
                ha.Add(checkEdit5.Text, spinEdit5.Value);
               
            }
            savevalue(checkEdit5.Text, spinEdit5.Value, spinEdit5.Visible);
            if (spinEdit6.Visible)
            {
                ha.Add(checkEdit6.Text, spinEdit6.Value);
            
            }
            savevalue(checkEdit6.Text, spinEdit6.Value, spinEdit6.Visible);
            if (spinEdit7.Visible)
            {
                ha.Add(checkEdit7.Text, spinEdit7.Value);
              
            }
            savevalue(checkEdit7.Text, spinEdit7.Value, spinEdit7.Visible);
            //if (spinEdit8.Visible)
            //{
            //    ha.Add(checkEdit8.Text, spinEdit8.Value);
               
            //}
            //savevalue(checkEdit8.Text, spinEdit8.Value, spinEdit8.Visible);
            this.DialogResult = DialogResult.OK;
        }
        private void savevalue(string title,decimal value,bool isvalue)
        {
            bool bl = false;
            foreach (Ps_Calc pc11 in list1)
            {
                if (pc11.CalcID == title)
                {
                    bl = true;
                    if (isvalue)
                    pc11.Value1 = 1;
                    else
                    pc11.Value1 = 0;
                pc11.Value2 =Convert.ToDouble( value);
                    Services.BaseService.Update<Ps_Calc>(pc11);
                }
            }
            if (!bl)
            {
                Ps_Calc pcs = new Ps_Calc();
                pcs.ID = Guid.NewGuid().ToString();
                pcs.Forecast = type;
                pcs.ForecastID = forecastReport.ID;
                pcs.CalcID = title;

                if (isvalue)
                    pcs.Value1 = 1;
                else
                    pcs.Value1 = 0;
                pcs.Value2 = Convert.ToDouble(value);
                Services.BaseService.Create<Ps_Calc>(pcs);

            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            spinEdit1.Value = OverMaxValue(spinEdit1.Value, 1);
            spinEdit1.Visible = !(spinEdit1.Visible);
         //   spinEdit1.Properties.MaxValue = getsum(-1);
         
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            spinEdit2.Visible = !(spinEdit2.Visible);
         //   spinEdit2.Properties.MaxValue = getsum(-1);
            spinEdit2.Value = OverMaxValue(spinEdit2.Value, 2);

        }

        private void checkEdit3_CheckedChanged(object sender, EventArgs e)
        {
            spinEdit3.Value = OverMaxValue(spinEdit3.Value, 3);
            spinEdit3.Visible = !(spinEdit3.Visible);
       //     spinEdit3.Properties.MaxValue = getsum(-1);
           
        } 

        private void checkEdit4_CheckedChanged(object sender, EventArgs e)
        {
            spinEdit4.Value = OverMaxValue(spinEdit4.Value, 4);
            spinEdit4.Visible = !(spinEdit4.Visible);
       //     spinEdit4.Properties.MaxValue = getsum(-1);
           
        }

        private void checkEdit5_CheckedChanged(object sender, EventArgs e)
        {
            spinEdit5.Value = OverMaxValue(spinEdit5.Value,5);
            spinEdit5.Visible = !(spinEdit5.Visible);
        //    spinEdit5.Properties.MaxValue = getsum(-1);
           
        }

        private void checkEdit6_CheckedChanged(object sender, EventArgs e)
        {
            spinEdit6.Value = OverMaxValue(spinEdit6.Value, 6);
            spinEdit6.Visible = !(spinEdit6.Visible);
          //  spinEdit6.Properties.MaxValue = getsum(-1);
          
        }

        private void checkEdit7_CheckedChanged(object sender, EventArgs e)
        {

            spinEdit7.Value = OverMaxValue(spinEdit7.Value, 7);
            spinEdit7.Visible = !(spinEdit7.Visible);
      //      spinEdit7.Properties.MaxValue = getsum(-1);
        }

        private void checkEdit8_CheckedChanged(object sender, EventArgs e)
        {
            //spinEdit8.Value = OverMaxValue(spinEdit8.Value, 8);
            //spinEdit8.Visible = !(spinEdit8.Visible);
       //     spinEdit8.Properties.MaxValue = getsum(-1);
          
        }

        private decimal getsum(int i)
        {
            decimal spinedittemp = 0;
           
               
                if (spinEdit1.Visible&&i!=1)
                    spinedittemp += spinEdit1.Value;
                if (spinEdit2.Visible && i !=2)
                    spinedittemp += spinEdit2.Value;
                if (spinEdit3.Visible && i != 3)
                    spinedittemp += spinEdit3.Value;
                if (spinEdit4.Visible && i != 4)
                    spinedittemp +=spinEdit4.Value;
                if (spinEdit5.Visible && i != 5)
                    spinedittemp += spinEdit5.Value;
                if (spinEdit6.Visible && i != 6)
                    spinedittemp +=spinEdit6.Value;
                if (spinEdit7.Visible && i != 7)
                    spinedittemp +=spinEdit7.Value;
                //if (spinEdit8.Visible && i != 8)
                //    spinedittemp += spinEdit8.Value;
            if (spinedittemp < 1)
                return 1 - spinedittemp;
            else
                return 0;
        }

        private void spinEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            decimal valuetemp = getsum(1);
          
            if (e.NewValue.ToString()!=""&&  Convert.ToDecimal( e.NewValue) > valuetemp)
            {

                e.Cancel = true;
                spinEdit1.EditValue = valuetemp;
               
            }
      
        }
        private decimal OverMaxValue(object value,int id)
        {
            decimal valuetemp = getsum(id);
            if (value == null)
                return 0.00m;
             if (value.ToString() == "" )
                return 0.00m;
            if ((Convert.ToDecimal(value) > valuetemp))
            {

                return valuetemp;

            }
            else
                return Convert.ToDecimal(value);
        }
           
        
        private void spinEdit2_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            decimal valuetemp = getsum(2);
            if (e.NewValue.ToString() != "" && (Convert.ToDecimal(e.NewValue) > valuetemp))
            {
                e.Cancel = true;
                spinEdit2.EditValue = valuetemp;
            }
        }

        private void spinEdit3_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            decimal valuetemp = getsum(3);
            if (e.NewValue.ToString() != "" && Convert.ToDecimal(e.NewValue) > valuetemp)
            {
                e.Cancel = true;
                spinEdit3.EditValue = valuetemp;
            }
        }

        private void spinEdit4_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            decimal valuetemp = getsum(4);
            if (e.NewValue.ToString() != "" && Convert.ToDecimal(e.NewValue) > valuetemp)
            {
                e.Cancel = true;
                spinEdit4.EditValue = valuetemp;
            }
        }

        private void spinEdit5_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            decimal valuetemp = getsum(5);
            if (e.NewValue.ToString() != "" && Convert.ToDecimal(e.NewValue) > valuetemp)
            {
                e.Cancel = true;
                spinEdit5.EditValue = valuetemp;
            }
        }

        private void spinEdit6_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            decimal valuetemp = getsum(6);
            if (e.NewValue.ToString() != "" && Convert.ToDecimal(e.NewValue) > valuetemp)
            {
                e.Cancel = true;
                spinEdit6.EditValue = valuetemp;
            }
        }

        private void spinEdit7_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            decimal valuetemp = getsum(7);
            if (e.NewValue.ToString() != "" && Convert.ToDecimal(e.NewValue) > valuetemp)
            {
                e.Cancel = true;
                spinEdit7.EditValue = valuetemp;
            }
        }

        private void spinEdit8_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            //decimal valuetemp = getsum(8);
            //if (e.NewValue.ToString() != "" && Convert.ToDecimal(e.NewValue) > valuetemp)
            //{
            //    e.Cancel = true;
            //    spinEdit8.EditValue = valuetemp;
            //}
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
             spinEdit1.Value=0.0m;
             spinEdit2.Value=0.0m;
             spinEdit3.Value=0.0m;
             spinEdit4.Value=0.0m;
             spinEdit5.Value=0.0m;
             spinEdit6.Value=0.0m;
            spinEdit7.Value=0.0m;
            //spinEdit8.Value = 0.0m;
        }

        //private void spinEdit1_EditValueChanged(object sender, EventArgs e)
        //{
        //    decimal valuetemp = getsum(1);

        //    if (spinEdit1.EditValue.ToString() != "" && Convert.ToDecimal(spinEdit1.EditValue) > valuetemp)
        //    {

               
        //        spinEdit1.EditValue = valuetemp;

        //    }
        //}
        //private void spinEdit2_EditValueChanged(object sender, EventArgs e)
        //{
        //    decimal valuetemp = getsum(2);
        //    if (spinEdit2.EditValue.ToString() != "" && (Convert.ToDecimal(spinEdit2.EditValue) > valuetemp))
        //    {
        //        //  e.Cancel = true;
        //        spinEdit2.EditValue = valuetemp;
        //    }
        //}
        //private void spinEdit3_EditValueChanged(object sender, EventArgs e)
        //{
        //    decimal valuetemp = getsum(3);
        //    if (spinEdit3.EditValue.ToString() != "" && (Convert.ToDecimal(spinEdit3.EditValue) > valuetemp))
        //    {
        //      //  e.Cancel = true;
        //        spinEdit3.EditValue = valuetemp;
        //    }
        //}

        //private void spinEdit4_EditValueChanged(object sender, EventArgs e)
        //{
        //    decimal valuetemp = getsum(4);
        //    if (spinEdit4.EditValue.ToString() != "" && (Convert.ToDecimal(spinEdit4.EditValue) > valuetemp))
        //    {
        //        //  e.Cancel = true;
        //        spinEdit4.EditValue = valuetemp;
        //    }
        //}

        //private void spinEdit5_EditValueChanged(object sender, EventArgs e)
        //{
        //    decimal valuetemp = getsum(5);
        //    if (spinEdit5.EditValue.ToString() != "" && (Convert.ToDecimal(spinEdit5.EditValue) > valuetemp))
        //    {
        //        //  e.Cancel = true;
        //        spinEdit5.EditValue = valuetemp;
        //    }
        //}

        //private void spinEdit6_EditValueChanged(object sender, EventArgs e)
        //{
        //    decimal valuetemp = getsum(6);
        //    if (spinEdit6.EditValue.ToString() != "" && (Convert.ToDecimal(spinEdit6.EditValue) > valuetemp))
        //    {
        //        //  e.Cancel = true;
        //        spinEdit6.EditValue = valuetemp;
        //    }
        //}

        //private void spinEdit7_EditValueChanged(object sender, EventArgs e)
        //{
        //    decimal valuetemp = getsum(7);
        //    if (spinEdit7.EditValue.ToString() != "" && (Convert.ToDecimal(spinEdit7.EditValue) > valuetemp))
        //    {
        //        //  e.Cancel = true;
        //        spinEdit7.EditValue = valuetemp;
        //    }
        //}

        //private void spinEdit8_EditValueChanged(object sender, EventArgs e)
        //{
        //    decimal valuetemp = getsum(8);
        //    if (spinEdit8.EditValue.ToString() != "" && (Convert.ToDecimal(spinEdit8.EditValue) > valuetemp))
        //    {
        //        //  e.Cancel = true;
        //        spinEdit8.EditValue = valuetemp;
        //    }
        //}

      
    

      
      
      





    }
}