using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using Itop.Client;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using Itop.Domain.Stutistic;
using Itop.Client.Base;

namespace ItopVector.Tools
{
    public partial class frmfjxProperty : FormBase
    {
        PSP_Gra_item sub = new PSP_Gra_item();
        public string jwstr = "";
        public PSP_Gra_item Sub
        {
            get { return sub; }
            set { sub = value; }
        }
     
     
        public bool IsCreate = false;
  
        private string layerID = "";
        private string LabelTxt = "";

        public string OldUID = "";
        public bool SubUpdate = false;


        public frmfjxProperty()
        {
            InitializeComponent();
        }
        public void InitData(string useid,string svguid,string layID)
        {
           
            layerID = layID;
        
            try
            {
                sub.EleID = useid;
                sub.SvgUID = svguid;
                sub.LayerID = layID;
                IList svglist = Services.BaseService.GetList("SelectPSP_Gra_itemByEleIDKey", sub);
                if (svglist.Count > 0)
                {
                    sub = (PSP_Gra_item)svglist[0];
                    IsCreate = false;
                }
                else
                {
                    IsCreate = true;
                    //gPro.Area = Area;
                    sub.UID = Guid.NewGuid().ToString();
                    sub.LayerID = layerID;
                    sub.EleID = useid;
                    sub.SvgUID = svguid;
                   
                }


                col_bh.DataBindings.Add("Text", sub, "EleKeyID");
                col_mc.DataBindings.Add("Text", sub, "EleName");
            
                col_zg.DataBindings.Add("Text", sub, "col2");
                col_zt.DataBindings.Add("Text", sub, "col1");
                col_zb.DataBindings.Add("Text", sub, "col3");
                remark.DataBindings.Add("Text", sub, "col4");
                       
            
            }
            catch(Exception e){
                MessageBox.Show(e.Message);
            }

        }
    
      
   
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            sub.EleKeyID = col_bh.Text;
            sub.EleName = col_mc.Text;
            sub.col1 = col_zt.Text;
            sub.col2 = col_zg.Text;
            sub.col3 = col_zb.Text;
            sub.col4 = remark.Text;
         
            if (IsCreate)
            {
               
                Services.BaseService.Create<PSP_Gra_item>(sub);

            }
            else
            {
                
                Services.BaseService.Update<PSP_Gra_item>(sub);
            }
           
            

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

     
        private void frmProperty_Load(object sender, EventArgs e)
        {

       
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

       
                          
    }
}