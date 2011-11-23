using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Client.Projects;
using System.Collections;

namespace Itop.TLPSP.DEVICE
{
    /// <summary>
    /// 母线属性编辑窗口
    /// </summary>
    public partial class frmDRHXHdlg : Itop.Client.Base.FormBase
    {
        WireCategory wc = new WireCategory();
        public WireCategory DeviceMx
        {
            get{
                try
                {
                    wc.WireType = textBox1.Text;
                    wc.WireLevel = spinEdit3.Value.ToString();
                    wc.WireChange =(int) spinEdit17.Value;
                    wc.WireR = (double)spinEdit1.Value;
                    wc.WireTQ = (double)spinEdit7.Value;
                    wc.WireGNDC = (double)spinEdit9.Value;
                    wc.ZeroR = (double)spinEdit13.Value;
                    wc.ZeroTQ = (double)spinEdit12.Value;
                    wc.ZeroGNDC = (double)spinEdit10.Value;
                    wc.WireLead = (Double)spinEdit2.Value;
                    wc.Type = "65";
                }
                catch (System.Exception ex)
                {
                	
                }
                return wc;
            }
            set{
                wc = value;
                try
                {
                    spinEdit17.Value = wc.WireChange;
                    spinEdit1.Value = (decimal)wc.WireR;
                    spinEdit7.Value = (decimal)wc.WireTQ;
                    spinEdit9.Value = (decimal)wc.WireGNDC;
                    spinEdit13.Value = (decimal)wc.ZeroR;
                    spinEdit12.Value = (decimal)wc.ZeroTQ;
                    spinEdit10.Value = (decimal)wc.ZeroGNDC;
                    textBox1.Text = wc.WireType;
                    spinEdit3.Value = Convert.ToDecimal(wc.WireLevel);
                    spinEdit2.Value = (decimal)wc.WireLead;
           
                }
                catch (System.Exception ex)
                {
                	
                }
                //NodeType = f;    
            }
        }
       
        public frmDRHXHdlg() {
            InitializeComponent();          
          
        }      
     
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);          
        }

   

        #region 属性   
     
        #endregion  
    }
}