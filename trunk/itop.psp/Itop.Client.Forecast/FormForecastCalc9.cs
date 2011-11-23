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
    public partial class FormForecastCalc9 : FormBase
    {
        public FormForecastCalc9()
        {
            InitializeComponent();
        }
        int firstyear = 0;
        int endyear = 0;

        IList<Ps_Calc> list1 = new List<Ps_Calc>();
        Ps_Calc pc1 = new Ps_Calc();
        private bool isedit=false;
        int type = 9;
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

        private void FormForecastCalc9_Load(object sender, EventArgs e)
        {
            
            //Ps_Forecast_Setup pfs = new Ps_Forecast_Setup();
            //pfs.Forecast = type;
            //pfs.ForecastID = forecastReport.ID;

            //IList<Ps_Forecast_Setup> li = Services.BaseService.GetList<Ps_Forecast_Setup>("SelectPs_Forecast_SetupByForecast", pfs);

            //if (li.Count != 0)
            //{
            //    firstyear = li[0].StartYear;
            //    endyear = li[0].EndYear;
            //}
            //comboBoxEdit1.Properties.Items.Add(0);
            //comboBoxEdit2.Properties.Items.Add(0);
            //comboBoxEdit3.Properties.Items.Add(0);
            //comboBoxEdit4.Properties.Items.Add(0);
            //comboBoxEdit5.Properties.Items.Add(0);
            //comboBoxEdit6.Properties.Items.Add(0);
            //comboBoxEdit7.Properties.Items.Add(0);
            //comboBoxEdit8.Properties.Items.Add(0);
            //comboBoxEdit9.Properties.Items.Add(0);
            //comboBoxEdit10.Properties.Items.Add(0);
            //comboBoxEdit11.Properties.Items.Add(0);
            //comboBoxEdit12.Properties.Items.Add(0);
            //comboBoxEdit13.Properties.Items.Add(0);
            //comboBoxEdit14.Properties.Items.Add(0);
            //comboBoxEdit15.Properties.Items.Add(0);
            //comboBoxEdit16.Properties.Items.Add(0);
            for (int i = forecastReport.StartYear + 1; i <= forecastReport.EndYear; i++)
            {
                comboBoxEdit1.Properties.Items.Add(i + "定");
                comboBoxEdit2.Properties.Items.Add(i + "定");
                comboBoxEdit3.Properties.Items.Add(i + "定");
                comboBoxEdit4.Properties.Items.Add(i + "定");
                comboBoxEdit5.Properties.Items.Add(i + "定");
                comboBoxEdit6.Properties.Items.Add(i + "定");
                comboBoxEdit7.Properties.Items.Add(i + "定");
                comboBoxEdit8.Properties.Items.Add(i + "定");
                comboBoxEdit9.Properties.Items.Add(i + "定");
                comboBoxEdit10.Properties.Items.Add(i + "定");
                comboBoxEdit11.Properties.Items.Add(i + "定");
                comboBoxEdit12.Properties.Items.Add(i + "定");
                comboBoxEdit13.Properties.Items.Add(i + "定");
                comboBoxEdit14.Properties.Items.Add(i + "定");
                comboBoxEdit15.Properties.Items.Add(i + "定");
                comboBoxEdit16.Properties.Items.Add(i + "定");
            }
            Ps_Calc pcs = new Ps_Calc();
            pcs.Forecast = type;
            pcs.ForecastID = forecastReport.ID;
            list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);
            foreach (Ps_Calc pcs2 in list1)
            {
              
               if(checkEdit1.Text==pcs2.CalcID)
               {
                   if (pcs2.Value1 > 0 )
                   {
                       checkEdit1.Checked = true;
                       comboBoxEdit1.Visible = true;
                       comboBoxEdit9.Visible = true;
                       label2.Visible = true;
                   }
                   else
                   {
                       checkEdit1.Checked = false;
                       comboBoxEdit1.Visible = false;
                       comboBoxEdit9.Visible = false;
                       label2.Visible = false;
                   }
                  
                   comboBoxEdit1.Text = OverMaxValue(getsum(1), pcs2.Value2, pcs2.Value3, 1)[0] + "定";
                   comboBoxEdit9.Text = OverMaxValue(getsum(1), pcs2.Value2, pcs2.Value3, 1)[1] + "定";
                   if (comboBoxEdit1.Text == "0定")
                       comboBoxEdit1.Text = "";
                   if (comboBoxEdit9.Text == "0定")
                       comboBoxEdit9.Text = "";
               }
               else
                   if(checkEdit2.Text==pcs2.CalcID)
                {
                    if (pcs2.Value1 > 0 )
                    {
                        checkEdit2.Checked = true;
                        comboBoxEdit2.Visible = true;
                        comboBoxEdit10.Visible = true;
                        label3.Visible = true;
                    }
                    else
                    {
                        checkEdit2.Checked = false;
                        comboBoxEdit2.Visible = false;
                        comboBoxEdit10.Visible = false;
                        label3.Visible = false;
                    }

                    comboBoxEdit2.Text = OverMaxValue(getsum(2), pcs2.Value2, pcs2.Value3, 2)[0] + "定";
                    comboBoxEdit10.Text = OverMaxValue(getsum(2), pcs2.Value2, pcs2.Value3, 2)[1] + "定";
                    if (comboBoxEdit2.Text == "0定")
                        comboBoxEdit2.Text = "";
                    if (comboBoxEdit10.Text == "0定")
                        comboBoxEdit10.Text = "";
                   }
                else
                   if( checkEdit3.Text==pcs2.CalcID)
                   {
                       if (pcs2.Value1 > 0)
                       {
                           checkEdit3.Checked = true;
                           comboBoxEdit3.Visible = true;
                           comboBoxEdit11.Visible = true;
                           label4.Visible = true;
                       }
                       else
                       {
                           checkEdit3.Checked = false;
                           comboBoxEdit3.Visible = false;
                           comboBoxEdit11.Visible = false;
                           label4.Visible = false;
                       }

                       comboBoxEdit3.Text = OverMaxValue(getsum(3), pcs2.Value2, pcs2.Value3, 3)[0] + "定";
                       comboBoxEdit11.Text = OverMaxValue(getsum(3), pcs2.Value2, pcs2.Value3, 3)[1] + "定";
                       if (comboBoxEdit3.Text == "0定")
                           comboBoxEdit3.Text = "";
                       if (comboBoxEdit11.Text == "0定")
                           comboBoxEdit11.Text = "";

                   }
                else
                   if(checkEdit4.Text==pcs2.CalcID)
                   {
                       if (pcs2.Value1 > 0)
                       {
                           checkEdit4.Checked = true;
                           comboBoxEdit4.Visible = true;
                           comboBoxEdit12.Visible = true;
                           label5.Visible = true;
                       }
                       else
                       {
                           checkEdit4.Checked = false;
                           comboBoxEdit4.Visible = false;
                           comboBoxEdit12.Visible = false;
                           label5.Visible = false;
                       }

                       comboBoxEdit4.Text = OverMaxValue(getsum(4), pcs2.Value2, pcs2.Value3, 4)[0] + "定";
                       comboBoxEdit12.Text = OverMaxValue(getsum(4), pcs2.Value2, pcs2.Value3, 4)[1] + "定";
                       if (comboBoxEdit4.Text == "0定")
                           comboBoxEdit4.Text = "";
                       if (comboBoxEdit12.Text == "0定")
                           comboBoxEdit12.Text = "";

                   }
                else
                   if(checkEdit5.Text==pcs2.CalcID)
                   {
                       if (pcs2.Value1 > 0 )
                       {
                           checkEdit5.Checked = true;
                           comboBoxEdit5.Visible = true;
                           comboBoxEdit13.Visible = true;
                           label6.Visible = true;
                       }
                       else
                       {
                           checkEdit5.Checked = false;
                           comboBoxEdit5.Visible = false;
                           comboBoxEdit13.Visible = false;
                           label6.Visible = false;
                       }

                       comboBoxEdit5.Text = OverMaxValue(getsum(5), pcs2.Value2, pcs2.Value3, 5)[0] + "定";
                       comboBoxEdit13.Text = OverMaxValue(getsum(5), pcs2.Value2, pcs2.Value3, 5)[1] + "定";
                       if (comboBoxEdit5.Text == "0定")
                           comboBoxEdit5.Text = "";
                       if (comboBoxEdit13.Text == "0定")
                           comboBoxEdit13.Text = "";

                   }
                else
                   if( checkEdit6.Text==pcs2.CalcID)
                   {
                       if (pcs2.Value1 > 0)
                       {
                           checkEdit6.Checked = true;
                           comboBoxEdit6.Visible = true;
                           comboBoxEdit14.Visible = true;
                           label7.Visible = true;
                       }
                       else
                       {
                           checkEdit6.Checked = false;
                           comboBoxEdit6.Visible = false;
                           comboBoxEdit14.Visible = false;
                           label7.Visible = false;
                       }

                       comboBoxEdit6.Text = OverMaxValue(getsum(6), pcs2.Value2, pcs2.Value3, 6)[0] + "定";
                       comboBoxEdit14.Text = OverMaxValue(getsum(6), pcs2.Value2, pcs2.Value3, 6)[1] + "定";
                       if (comboBoxEdit6.Text == "0定")
                           comboBoxEdit6.Text = "";
                       if (comboBoxEdit14.Text == "0定")
                           comboBoxEdit14.Text = "";

                   }
                else
                   if(checkEdit7.Text==pcs2.CalcID)
                   {
                       if (pcs2.Value1 > 0)
                       {
                           checkEdit7.Checked = true;
                           comboBoxEdit7.Visible = true;
                           comboBoxEdit15.Visible = true;
                           label8.Visible = true;
                       }
                       else
                       {
                           checkEdit7.Checked = false;
                           comboBoxEdit7.Visible = false;
                           comboBoxEdit15.Visible = false;
                           label8.Visible = false;
                       }

                       comboBoxEdit7.Text = OverMaxValue(getsum(7), pcs2.Value2, pcs2.Value3, 7)[0] + "定";
                       comboBoxEdit15.Text = OverMaxValue(getsum(7), pcs2.Value2, pcs2.Value3, 7)[1] + "定";
                       if (comboBoxEdit7.Text == "0定")
                           comboBoxEdit7.Text = "";
                       if (comboBoxEdit15.Text == "0定")
                           comboBoxEdit15.Text = "";

                   }
                //else
                //   if( checkEdit8.Text==pcs2.CalcID)
                //   {
                //       if (pcs2.Value1 > 0 )
                //       {
                //           checkEdit8.Checked = true;
                //           comboBoxEdit8.Visible = true;
                //           comboBoxEdit16.Visible = true;
                //           label9.Visible = true;
                //       }
                //       else
                //       {
                //           checkEdit8.Checked = false;
                //           comboBoxEdit8.Visible = false;
                //           comboBoxEdit16.Visible = false;
                //           label9.Visible = false;
                //       }

                //       comboBoxEdit8.Text = OverMaxValue(getsum(8), pcs2.Value2, pcs2.Value3, 8)[0] + "定";
                //       comboBoxEdit16.Text = OverMaxValue(getsum(8), pcs2.Value2, pcs2.Value3, 8)[1] + "定";
                //       if (comboBoxEdit8.Text == "0定")
                //           comboBoxEdit8.Text = "";
                //       if (comboBoxEdit16.Text == "0定")
                //           comboBoxEdit16.Text = "";

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
            if (comboBoxEdit1.Visible)
            {
                if (comboBoxEdit1.Text != "" && comboBoxEdit9.Text != "")
                ha.Add(checkEdit1.Text, comboBoxEdit1.Text.Replace("定", "") + "@" + comboBoxEdit9.Text.Replace("定", ""));
              
            }
            savevalue(checkEdit1.Text, comboBoxEdit1.Text.Replace("定", ""), comboBoxEdit1.Visible, comboBoxEdit9.Text.Replace("定", ""));
            if (comboBoxEdit2.Visible)
            {
                if (comboBoxEdit2.Text != "" && comboBoxEdit10.Text != "")
                ha.Add(checkEdit2.Text, comboBoxEdit2.Text.Replace("定", "") + "@" + comboBoxEdit10.Text.Replace("定", ""));
                
            }
            savevalue(checkEdit2.Text, comboBoxEdit2.Text.Replace("定", ""), comboBoxEdit2.Visible, comboBoxEdit10.Text.Replace("定", ""));
            if (comboBoxEdit3.Visible)
            {
                if (comboBoxEdit3.Text != "" && comboBoxEdit11.Text != "")
                ha.Add(checkEdit3.Text, comboBoxEdit3.Text.Replace("定","")  + "@" + comboBoxEdit11.Text.Replace("定","") );
                
            }
            savevalue(checkEdit3.Text, comboBoxEdit3.Text.Replace("定","") , comboBoxEdit3.Visible, comboBoxEdit11.Text.Replace("定","") );
            if (comboBoxEdit4.Visible)
            {
                if (comboBoxEdit4.Text != "" && comboBoxEdit12.Text != "")
                ha.Add(checkEdit4.Text, comboBoxEdit4.Text.Replace("定","")  + "@" + comboBoxEdit12.Text.Replace("定","") );
                
            }
            savevalue(checkEdit4.Text, comboBoxEdit4.Text.Replace("定","") , comboBoxEdit4.Visible, comboBoxEdit12.Text.Replace("定","") );
            if (comboBoxEdit5.Visible)
            {
                if (comboBoxEdit5.Text != "" && comboBoxEdit13.Text != "")
                ha.Add(checkEdit5.Text, comboBoxEdit5.Text.Replace("定","")  + "@" + comboBoxEdit13.Text.Replace("定","") );
               
            }
            savevalue(checkEdit5.Text, comboBoxEdit5.Text.Replace("定","") , comboBoxEdit5.Visible, comboBoxEdit13.Text.Replace("定","") );
            if (comboBoxEdit6.Visible)
            {
                if (comboBoxEdit6.Text != "" && comboBoxEdit14.Text != "")
                ha.Add(checkEdit6.Text, comboBoxEdit6.Text.Replace("定","")  + "@" + comboBoxEdit14.Text.Replace("定","") );
            
            }
            savevalue(checkEdit6.Text, comboBoxEdit6.Text.Replace("定","") , comboBoxEdit6.Visible, comboBoxEdit14.Text.Replace("定","") );
            if (comboBoxEdit7.Visible)
            {
                if (comboBoxEdit7.Text != "" && comboBoxEdit15.Text != "")
                ha.Add(checkEdit7.Text, comboBoxEdit7.Text.Replace("定","")  + "@" + comboBoxEdit15.Text.Replace("定","") );
              
            }
            savevalue(checkEdit7.Text, comboBoxEdit7.Text.Replace("定","") , comboBoxEdit7.Visible, comboBoxEdit15.Text.Replace("定","") );
            //if (comboBoxEdit8.Visible)
            //{
            //    if (comboBoxEdit8.Text != "" && comboBoxEdit16.Text != "")
            //    ha.Add(checkEdit8.Text, comboBoxEdit8.Text.Replace("定","")  + "@" + comboBoxEdit16.Text.Replace("定","") );
               
            //}
            //savevalue(checkEdit8.Text, comboBoxEdit8.Text.Replace("定","") , comboBoxEdit8.Visible, comboBoxEdit16.Text.Replace("定","") );
            this.DialogResult = DialogResult.OK;
        }
        private void savevalue(string title,string value1,bool isvalue,string value2)
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
                if (value1 != "")
                    pc11.Value2 = Convert.ToDouble(value1);
                else
                    pc11.Value2 = 0;
                if (value2 != "")
                    pc11.Value3 = Convert.ToDouble(value2);
                else
                    pc11.Value3 = 0;
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
                if (value1!="")
                pcs.Value2 = Convert.ToDouble(value1);
                else
                pcs.Value2 = 0;
            if (value2 != "")
                pcs.Value3 = Convert.ToDouble(value2);
            else
                pcs.Value3 = 0;
                Services.BaseService.Create<Ps_Calc>(pcs);

            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEdit1.Text = OverMaxValue(getsum(1), comboBoxEdit1.Text.Replace("定", ""), comboBoxEdit9.Text.Replace("定", ""), 1)[0] + "定";
            comboBoxEdit9.Text = OverMaxValue(getsum(1), comboBoxEdit1.Text.Replace("定", ""), comboBoxEdit9.Text.Replace("定", ""), 1)[1] + "定";
            if (comboBoxEdit1.Text == "0定")
                comboBoxEdit1.Text = "";
            if (comboBoxEdit9.Text == "0定")
                comboBoxEdit9.Text = "";
            comboBoxEdit1.Visible = !(comboBoxEdit1.Visible);
            comboBoxEdit9.Visible = !(comboBoxEdit9.Visible);
            label2.Visible = !(label2.Visible);
         //   spinEdit1.Properties.MaxValue = getsum(-1);
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEdit2.Text = OverMaxValue(getsum(2), comboBoxEdit2.Text.Replace("定", ""), comboBoxEdit10.Text.Replace("定", ""), 1)[0] + "定";
            comboBoxEdit10.Text = OverMaxValue(getsum(2), comboBoxEdit2.Text.Replace("定", ""), comboBoxEdit10.Text.Replace("定", ""), 1)[1] + "定";
            if (comboBoxEdit2.Text == "0定")
                comboBoxEdit2.Text = "";
            if (comboBoxEdit10.Text == "0定")
                comboBoxEdit10.Text = "";
            comboBoxEdit2.Visible = !(comboBoxEdit2.Visible);
            comboBoxEdit10.Visible = !(comboBoxEdit10.Visible);
            label3.Visible = !(label3.Visible);
         //   spinEdit2.Properties.MaxValue = getsum(-1);
       
        }

        private void checkEdit3_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEdit3.Text = OverMaxValue(getsum(3), comboBoxEdit3.Text.Replace("定", ""), comboBoxEdit11.Text.Replace("定", ""), 1)[0] + "定";
            comboBoxEdit11.Text = OverMaxValue(getsum(3), comboBoxEdit3.Text.Replace("定", ""), comboBoxEdit11.Text.Replace("定", ""), 1)[1] + "定";
            if (comboBoxEdit3.Text == "0定")
                comboBoxEdit3.Text = "";
            if (comboBoxEdit11.Text == "0定")
                comboBoxEdit11.Text = "";
            comboBoxEdit3.Visible = !(comboBoxEdit3.Visible);
            comboBoxEdit11.Visible = !(comboBoxEdit11.Visible);
            label4.Visible = !(label4.Visible);
       //     spinEdit3.Properties.MaxValue = getsum(-1);
        } 

        private void checkEdit4_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEdit4.Text = OverMaxValue(getsum(4), comboBoxEdit4.Text.Replace("定", ""), comboBoxEdit12.Text.Replace("定", ""), 1)[0] + "定";
            comboBoxEdit12.Text = OverMaxValue(getsum(4), comboBoxEdit4.Text.Replace("定", ""), comboBoxEdit12.Text.Replace("定", ""), 1)[1] + "定";
            if (comboBoxEdit4.Text == "0定")
                comboBoxEdit4.Text = "";
            if (comboBoxEdit12.Text == "0定")
                comboBoxEdit12.Text = "";
            comboBoxEdit4.Visible = !(comboBoxEdit4.Visible);
            comboBoxEdit12.Visible = !(comboBoxEdit12.Visible);
            label5.Visible = !(label5.Visible);
       //     spinEdit4.Properties.MaxValue = getsum(-1);
        }

        private void checkEdit5_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEdit5.Text = OverMaxValue(getsum(5), comboBoxEdit5.Text.Replace("定", ""), comboBoxEdit13.Text.Replace("定", ""), 1)[0] + "定";
            comboBoxEdit13.Text = OverMaxValue(getsum(5), comboBoxEdit5.Text.Replace("定", ""), comboBoxEdit13.Text.Replace("定", ""), 1)[1] + "定";
            if (comboBoxEdit5.Text == "0定")
                comboBoxEdit5.Text = "";
            if (comboBoxEdit13.Text == "0定")
                comboBoxEdit13.Text = "";
            comboBoxEdit5.Visible = !(comboBoxEdit5.Visible);
            comboBoxEdit13.Visible = !(comboBoxEdit13.Visible);
            label6.Visible = !(label6.Visible);
        //    spinEdit5.Properties.MaxValue = getsum(-1);
        }

        private void checkEdit6_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEdit6.Text = OverMaxValue(getsum(6), comboBoxEdit6.Text.Replace("定", ""), comboBoxEdit14.Text.Replace("定", ""), 1)[0] + "定";
            comboBoxEdit14.Text = OverMaxValue(getsum(6), comboBoxEdit6.Text.Replace("定", ""), comboBoxEdit14.Text.Replace("定", ""), 1)[1] + "定";
            if (comboBoxEdit6.Text == "0定")
                comboBoxEdit6.Text = "";
            if (comboBoxEdit14.Text == "0定")
                comboBoxEdit14.Text = "";
            comboBoxEdit6.Visible = !(comboBoxEdit6.Visible);
            comboBoxEdit14.Visible = !(comboBoxEdit14.Visible);
            label7.Visible = !(label7.Visible);
          //  spinEdit6.Properties.MaxValue = getsum(-1);
        }

        private void checkEdit7_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEdit7.Text = OverMaxValue(getsum(7), comboBoxEdit7.Text.Replace("定", ""), comboBoxEdit15.Text.Replace("定", ""), 1)[0] + "定";
            comboBoxEdit15.Text = OverMaxValue(getsum(7), comboBoxEdit7.Text.Replace("定", ""), comboBoxEdit15.Text.Replace("定", ""), 1)[1] + "定";
            if (comboBoxEdit7.Text == "0定")
                comboBoxEdit7.Text = "";
            if (comboBoxEdit15.Text == "0定")
                comboBoxEdit15.Text = "";
            comboBoxEdit7.Visible = !(comboBoxEdit7.Visible);
            comboBoxEdit15.Visible = !(comboBoxEdit15.Visible);
            label8.Visible = !(label8.Visible);
      //      spinEdit7.Properties.MaxValue = getsum(-1);
        }

        private void checkEdit8_CheckedChanged(object sender, EventArgs e)
        {
            //comboBoxEdit8.Text = OverMaxValue(getsum(8), comboBoxEdit8.Text.Replace("定", ""), comboBoxEdit16.Text.Replace("定", ""), 1)[0] + "定";
            //comboBoxEdit16.Text = OverMaxValue(getsum(8), comboBoxEdit8.Text.Replace("定", ""), comboBoxEdit16.Text.Replace("定", ""), 1)[1] + "定";
            //if (comboBoxEdit8.Text == "0定")
            //    comboBoxEdit8.Text = "";
            //if (comboBoxEdit16.Text == "0定")
            //    comboBoxEdit16.Text = "";
            //comboBoxEdit8.Visible = !(comboBoxEdit8.Visible);
            //comboBoxEdit16.Visible = !(comboBoxEdit16.Visible);
            //label9.Visible = !(label9.Visible);
       //     spinEdit8.Properties.MaxValue = getsum(-1);
        }

        private int[] getsum(int i)
        {
            int len = 0;
            int j = 0;


            if (comboBoxEdit1.Visible && comboBoxEdit1.Text != "" && comboBoxEdit9.Text != "" && i != 1)
            {
                len++;
            }
            if (comboBoxEdit2.Visible && comboBoxEdit2.Text != "" && comboBoxEdit10.Text != "" && i != 2)
                {
                    len++;
                }
                if (comboBoxEdit3.Visible && comboBoxEdit3.Text != "" && comboBoxEdit11.Text != "" && i != 3)
                {
                    len++;
                }
                if (comboBoxEdit4.Visible && comboBoxEdit4.Text != "" && comboBoxEdit12.Text != "" && i != 4)
                {
                    len++;
                }
                if (comboBoxEdit5.Visible && comboBoxEdit5.Text != "" && comboBoxEdit13.Text != "" && i != 5)
                {
                    len++;
                }
                if (comboBoxEdit6.Visible && comboBoxEdit6.Text != "" && comboBoxEdit14.Text != "" && i != 6)
                {
                    len++;
                }
                if (comboBoxEdit7.Visible && comboBoxEdit7.Text != "" && comboBoxEdit15.Text != "" && i != 7)
                {
                    len++;
                }
                if (comboBoxEdit8.Visible && comboBoxEdit8.Text != "" && comboBoxEdit16.Text != "" && i != 8)
                {
                    len++;
                }
                int[] spinedittemp = new int[len * 2];
                j = 0;
                if (comboBoxEdit1.Visible && comboBoxEdit1.Text != "" && comboBoxEdit9.Text != "" && i != 1)
                    {
                        spinedittemp[j] =Convert.ToInt32(  comboBoxEdit1.Text.Replace("定","") ) ;
                        spinedittemp[j + 1] = Convert.ToInt32(comboBoxEdit9.Text.Replace("定", ""));
                        j += 2;
                    }

                    if (comboBoxEdit2.Visible && comboBoxEdit2.Text != "" && comboBoxEdit10.Text != "" && i != 2)
                {
                    spinedittemp[j] = Convert.ToInt32( comboBoxEdit2.Text.Replace("定",""))  ;
                    spinedittemp[j + 1] = Convert.ToInt32( comboBoxEdit10.Text.Replace("定",""))  ;
                    j += 2;
                }

                if (comboBoxEdit3.Visible && comboBoxEdit3.Text != "" && comboBoxEdit11.Text != "" && i != 3)
                {
                    spinedittemp[j] = Convert.ToInt32( comboBoxEdit3.Text.Replace("定",""))  ;
                    spinedittemp[j + 1] = Convert.ToInt32( comboBoxEdit11.Text.Replace("定",""))  ;
                    j += 2;
                }

                if (comboBoxEdit4.Visible && comboBoxEdit4.Text != "" && comboBoxEdit12.Text != "" && i != 4)
                {
                    spinedittemp[j] = Convert.ToInt32( comboBoxEdit4.Text.Replace("定",""))  ;
                    spinedittemp[j + 1] = Convert.ToInt32( comboBoxEdit12.Text.Replace("定",""))  ;
                    j += 2;
                }

                if (comboBoxEdit5.Visible && comboBoxEdit5.Text != "" && comboBoxEdit13.Text != "" && i != 5)
                {
                    spinedittemp[j] = Convert.ToInt32( comboBoxEdit5.Text.Replace("定",""))  ;
                    spinedittemp[j + 1] = Convert.ToInt32( comboBoxEdit13.Text.Replace("定",""))  ;
                    j += 2;
                }

                if (comboBoxEdit6.Visible && comboBoxEdit6.Text != "" && comboBoxEdit14.Text != "" && i != 6)
                {
                    spinedittemp[j] = Convert.ToInt32( comboBoxEdit6.Text.Replace("定",""))  ;
                    spinedittemp[j + 1] = Convert.ToInt32( comboBoxEdit14.Text.Replace("定","") ) ;
                    j += 2;
                }

                if (comboBoxEdit7.Visible && comboBoxEdit7.Text != "" && comboBoxEdit15.Text != "" && i != 7)
                {
                    spinedittemp[j] = Convert.ToInt32( comboBoxEdit7.Text.Replace("定","") ) ;
                    spinedittemp[j + 1] =Convert.ToInt32(  comboBoxEdit15.Text.Replace("定","") ) ;
                    j += 2;
                }

                //if (comboBoxEdit8.Visible && comboBoxEdit8.Text != "" && comboBoxEdit16.Text != "" && i != 8)
                //{
                //    spinedittemp[j] =Convert.ToInt32(  comboBoxEdit8.Text.Replace("定","") ) ;
                //    spinedittemp[j + 1] = Convert.ToInt32( comboBoxEdit16.Text.Replace("定","")) ;
                //    j += 2;
                //}
                
                int[] valuetemp = new int [2];
                for (j = 0; j < spinedittemp.Length- 1; j += 2)
                {

                    for (int j2 = j + 2; j2 < spinedittemp.Length; j2 += 2)
                    {
                        if(spinedittemp[j]>spinedittemp[j2])
                        {
                          valuetemp[0] = spinedittemp[j2];
                          valuetemp[1] = spinedittemp[j2+1];
                          spinedittemp[j2]= spinedittemp[j];
                          spinedittemp[j2 + 1] = spinedittemp[j + 1];
                          spinedittemp[j] = valuetemp[0];
                          spinedittemp[j + 1] = valuetemp[1];


                        }
                    }

                }

                return spinedittemp;
        }

        //private void spinEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        //{
            

        //    if (e.NewValue.ToString() != "" && OverMaxValue( getsum(1),Convert.ToDecimal(e.NewValue), 0, 1)[0] < 1)
        //    {
               
        //            e.Cancel = true;

        //    }
      
        //}
        private decimal[] OverMaxValue(int[] valuetemp,object value1, object value2, int nkind)
        {
            //   decimal[] valuetemp = getsum(-1);
            decimal[] retuvalue = new decimal[2];
            retuvalue[0] = 0m;
            retuvalue[1] = 0m;
            if (value1 == null||value1.ToString() == "")
            {
                if (value2 != null&& value2.ToString()!= "")
                    retuvalue[1] = Convert.ToDecimal(value2);
                return retuvalue;
            }
            //if (value1.ToString() == "")
            //{
            //    return retuvalue;
            //}
            if (value2 == null || value2.ToString() == "")
            {
                if (value1 != null &&value1.ToString() != "")
                    retuvalue[0] = Convert.ToDecimal(value1);
                return retuvalue;
            }
            //{
            //    if (value2.ToString() == "")

            //        return retuvalue;
            //}
            decimal value1temp = Convert.ToDecimal(value1);
            decimal value2temp = Convert.ToDecimal(value2);
            if (value1temp > value2temp)
            {
                retuvalue[0] = 0m;
                retuvalue[1] = 0m;
                return retuvalue;
            }
            bool isfind = false;
            for (int i = 0; i < valuetemp.Length; i = i + 2)
            {
                if (i == 0)
                {


                    if (value2temp < valuetemp[0])
                    {
                        isfind = true;
                        break;
                    }

                }
                else if (i > 0 && i < valuetemp.Length)
                {
                    if (value1temp > valuetemp[i - 1] && value2temp < valuetemp[i])
                    {
                        isfind = true;
                        break;

                    }

                }
                else
                {
                    if (value1temp > valuetemp[i - 1] && value2temp < valuetemp[i])
                    {
                        isfind = true;
                        break;

                    }



                }

            }
            if (valuetemp.Length > 0)
            {
                if (value1temp > valuetemp[valuetemp.Length - 1])
                {
                    isfind = true;


                }
            }
            else
                isfind = true;

            if ((value1temp < Convert.ToDecimal(forecastReport.StartYear) || value1temp > Convert.ToDecimal(forecastReport.EndYear) && nkind != 2)

                   && (value2temp < Convert.ToDecimal(forecastReport.StartYear) || value2temp > Convert.ToDecimal(forecastReport.EndYear) && nkind != 1))
            {
                retuvalue[0] = 0m;
                retuvalue[1] = 0m;
                return retuvalue;
            }
            if (isfind)
            {
                retuvalue[0] = value1temp;
                retuvalue[1] = value2temp;
            }

            return retuvalue;

        }
           
        
        //private void spinEdit2_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        //{
        // //decimal[] valuetemp = getsum(2);
        //    if (e.NewValue.ToString() != "" && OverMaxValue( getsum(2),Convert.ToDecimal(e.NewValue), 0, 1)[0] < 1)
        //    {

        //        e.Cancel = true;

        //    }
        //}

        //private void spinEdit3_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        //{
        // //   decimal[] valuetemp = getsum(3);
        //    if (e.NewValue.ToString() != "" && OverMaxValue(getsum(3), Convert.ToDecimal(e.NewValue), 0, 1)[0] < 1)
        //    {

        //        e.Cancel = true;

        //    }
        //}

        //private void spinEdit4_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        //{
        // //   decimal[] valuetemp = getsum(4);
        //    if (e.NewValue.ToString() != "" && OverMaxValue(getsum(4), Convert.ToDecimal(e.NewValue), 0, 1)[0] < 1)
        //    {

        //        e.Cancel = true;

        //    }
        //}

        //private void spinEdit5_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        //{
        // //   decimal[] valuetemp = getsum(5);
        //    if (e.NewValue.ToString() != "" && OverMaxValue(getsum(5), Convert.ToDecimal(e.NewValue), 0, 1)[0] < 1)
        //    {

        //        e.Cancel = true;

        //    }
        //}

        //private void spinEdit6_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        //{
        //  //  decimal[] valuetemp = getsum(6);
        //    if (e.NewValue.ToString() != "" && OverMaxValue(getsum(6), Convert.ToDecimal(e.NewValue), 0, 1)[0] < 1)
        //    {

        //        e.Cancel = true;

        //    }
        //}

        //private void spinEdit7_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        //{
        ////    decimal[] valuetemp = getsum(7);
        //    if (e.NewValue.ToString() != "" && OverMaxValue(getsum(7), Convert.ToDecimal(e.NewValue), 0, 1)[0] < 1)
        //    {

        //        e.Cancel = true;

        //    }
        //}

        //private void spinEdit8_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        //{
        // //   decimal[] valuetemp = getsum(8);
        //    if (e.NewValue.ToString() != "" && OverMaxValue(getsum(8), Convert.ToDecimal(e.NewValue), 0, 1)[0] < 1)
        //    {

        //        e.Cancel = true;

        //    }
        //}

        //private void spinEdit9_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        //{
        //  //  decimal[] valuetemp = getsum(9);
        //    if (e.NewValue.ToString() != "" && OverMaxValue(getsum(1), 0, Convert.ToDecimal(e.NewValue), 2)[0] < 1)
        //    {

        //        e.Cancel = true;

        //    }
        //}

        //private void spinEdit10_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        //{
        //  //  decimal[] valuetemp = getsum(10);
        //    if (e.NewValue.ToString() != "" && OverMaxValue(getsum(2), 0, Convert.ToDecimal(e.NewValue), 2)[1] < 1)
        //    {

        //        e.Cancel = true;

        //    }
        //}

        //private void spinEdit11_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        //{
        //  //  decimal[] valuetemp = getsum(11);
        //    if (e.NewValue.ToString() != "" && OverMaxValue(getsum(3), 0, Convert.ToDecimal(e.NewValue), 2)[1] < 1)
        //    {

        //        e.Cancel = true;

        //    }
        //}

        //private void spinEdit12_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        //{
        //   // decimal[] valuetemp = getsum(12);
        //    if (e.NewValue.ToString() != "" && OverMaxValue(getsum(4), 0, Convert.ToDecimal(e.NewValue), 2)[1] < 1)
        //    {

        //        e.Cancel = true;

        //    }
        //}

        //private void spinEdit13_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        //{
        //   // decimal[] valuetemp = getsum(13);
        //    if (e.NewValue.ToString() != "" && OverMaxValue(getsum(5), 0, Convert.ToDecimal(e.NewValue), 2)[1] < 1)
        //    {

        //        e.Cancel = true;

        //    }
        //}

        //private void spinEdit14_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        //{
        //  //  decimal[] valuetemp = getsum(14);
        //    if (e.NewValue.ToString() != "" && OverMaxValue(getsum(6), 0, Convert.ToDecimal(e.NewValue), 2)[1] < 1)
        //    {

        //        e.Cancel = true;

        //    }
        //}

        //private void spinEdit15_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        //{
        //   // decimal[] valuetemp = getsum(15);
        //    if (e.NewValue.ToString() != "" && OverMaxValue(getsum(7), 0, Convert.ToDecimal(e.NewValue), 2)[1] < 1)
        //    {

        //        e.Cancel = true;

        //    }
        //}

        //private void spinEdit16_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        //{
        //   // decimal[] valuetemp = getsum(16);
          
        //}

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OverMaxValue(getsum(1), comboBoxEdit1.SelectedItem.ToString().Replace("定", ""), comboBoxEdit9.Text.Replace("定", ""), 1)[0] < 1)
            {

                comboBoxEdit1.Text = "";

            }
        }

        private void comboBoxEdit2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OverMaxValue(getsum(2), comboBoxEdit2.SelectedItem.ToString().Replace("定", ""), comboBoxEdit10.Text.Replace("定", ""), 1)[0] < 1)
            {

                comboBoxEdit2.Text = "";

            }
        }

        private void comboBoxEdit3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OverMaxValue(getsum(3), comboBoxEdit3.SelectedItem.ToString().Replace("定", ""), comboBoxEdit11.Text.Replace("定", ""), 1)[0] < 1)
            {

                comboBoxEdit3.Text = "";

            }
        }

        private void comboBoxEdit4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OverMaxValue(getsum(4), comboBoxEdit4.SelectedItem.ToString().Replace("定", ""), comboBoxEdit12.Text.Replace("定", ""), 1)[0] < 1)
            {

                comboBoxEdit4.Text = "";

            }
        }

        private void comboBoxEdit5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OverMaxValue(getsum(5), comboBoxEdit5.SelectedItem.ToString().Replace("定", ""), comboBoxEdit13.Text.Replace("定", ""), 1)[0] < 1)
            {

                comboBoxEdit5.Text = "";

            }
        }

        private void comboBoxEdit6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OverMaxValue(getsum(6), comboBoxEdit6.SelectedItem.ToString().Replace("定", ""), comboBoxEdit14.Text.Replace("定", ""), 1)[0] < 1)
            {

                comboBoxEdit6.Text = "";

            }
        }

        private void comboBoxEdit7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OverMaxValue(getsum(7), comboBoxEdit7.SelectedItem.ToString().Replace("定", ""), comboBoxEdit15.Text.Replace("定", ""), 1)[0] < 1)
            {

                comboBoxEdit7.Text = "";

            }
        }

        private void comboBoxEdit8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OverMaxValue(getsum(8), comboBoxEdit8.SelectedItem.ToString().Replace("定", ""), comboBoxEdit16.Text.Replace("定", ""), 1)[0] < 1)
            {

                comboBoxEdit8.Text = "";

            }
        }

        private void comboBoxEdit9_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBoxEdit1.Text == "" || OverMaxValue(getsum(1), comboBoxEdit1.Text.Replace("定", ""), comboBoxEdit9.SelectedItem.ToString().Replace("定", ""), 2)[1] < 1)
            {

                comboBoxEdit9.Text = "";

            }
        }

        private void comboBoxEdit10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit2.Text == "" || OverMaxValue(getsum(2), comboBoxEdit2.Text.Replace("定", ""), comboBoxEdit10.SelectedItem.ToString().Replace("定", ""), 2)[1] < 1)
            {

                comboBoxEdit10.Text = "";

            }
        }

        private void comboBoxEdit11_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit3.Text == "" || OverMaxValue(getsum(3), comboBoxEdit3.Text.Replace("定", ""), comboBoxEdit11.SelectedItem.ToString().Replace("定", ""), 2)[1] < 1)
            {

                comboBoxEdit11.Text = "";

            }
        }

        private void comboBoxEdit12_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit4.Text == "" || OverMaxValue(getsum(4), comboBoxEdit4.Text.Replace("定", ""), comboBoxEdit12.SelectedItem.ToString().Replace("定", ""), 2)[1] < 1)
            {

                comboBoxEdit12.Text = "";

            }
        }

        private void comboBoxEdit13_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit5.Text == "" || OverMaxValue(getsum(5), comboBoxEdit5.Text.Replace("定", ""), comboBoxEdit13.SelectedItem.ToString().Replace("定", ""), 2)[1] < 1)
            {

                comboBoxEdit13.Text = "";

            }
        }

        private void comboBoxEdit14_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit6.Text == "" || OverMaxValue(getsum(6), comboBoxEdit6.Text.Replace("定", ""), comboBoxEdit14.SelectedItem.ToString().Replace("定", ""), 2)[1] < 1)
            {

                comboBoxEdit14.Text = "";

            }
        }

        private void comboBoxEdit15_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit7.Text == "" || OverMaxValue(getsum(7), comboBoxEdit7.Text.Replace("定", ""), comboBoxEdit15.SelectedItem.ToString().Replace("定", ""), 2)[1] < 1)
            {

                comboBoxEdit15.Text = "";

            }
        }

        private void comboBoxEdit16_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit8.Text == "" || OverMaxValue(getsum(8), comboBoxEdit8.Text.Replace("定", ""), comboBoxEdit16.SelectedItem.ToString().Replace("定", ""), 2)[1] < 1)
            {

                comboBoxEdit16.Text = "";

            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            comboBoxEdit1.Text="";
            comboBoxEdit2.Text="";
            comboBoxEdit3.Text="";
            comboBoxEdit4.Text="";
            comboBoxEdit5.Text="";
            comboBoxEdit6.Text="";
            comboBoxEdit7.Text="";
            comboBoxEdit8.Text="";
            comboBoxEdit9.Text="";
            comboBoxEdit10.Text="";
            comboBoxEdit11.Text="";
            comboBoxEdit12.Text="";
            comboBoxEdit13.Text="";
            comboBoxEdit14.Text="";
            comboBoxEdit15.Text="";
            comboBoxEdit16.Text="";
        }

     



      
    

      
      
      





    }
}