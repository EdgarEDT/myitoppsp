using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Table;
using System.Text.RegularExpressions;
using Itop.Domain.Stutistic;
using Itop.Domain.Graphics;
using System.Collections;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmProShoSub : FormBase
    {
        public FrmProShoSub()
        {
            InitializeComponent(); 
            
        }
        PSP_Substation_Info currentpsi = null;
        public string Subname = "";
        public int subl1 = 0;
        public string AreaName = "";
        private void FrmProShoSub_Load(object sender, EventArgs e)
        {
            string sqlwhere = "";
            if (AreaName!="")
            {
                sqlwhere = "AreaID='" + MIS.ProgUID + "' and AreaName='" + AreaName + "' and  Flag='1' ";
            }
            else
	        {
                sqlwhere = "AreaID='" + MIS.ProgUID + "' and  Flag='1' ";

	        }
            
            IList pspList = Common.Services.BaseService.GetList("SelectSubstation_InfoByCon", sqlwhere);
            gridControl2.DataSource = pspList;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (gridView2.FocusedRowHandle!=-1)
            {
                this.DialogResult = DialogResult.OK;
            }
           
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView2.FocusedRowHandle!=-1)
	        {
               Subname = gridView2.GetRowCellValue(gridView2.FocusedRowHandle,"Title").ToString();
               subl1 = int.Parse(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "L1").ToString());
	        }
             

        }

      
      

       
    }
}