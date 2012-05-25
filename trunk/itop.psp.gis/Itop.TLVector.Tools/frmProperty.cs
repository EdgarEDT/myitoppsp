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

using Itop.Client.Base;
using Itop.Domain.Table;
namespace ItopVector.Tools
{
    public partial class frmProperty : FormBase
    {
        public glebeProperty gPro = new glebeProperty();
        glebeType gType = new glebeType();
        DataTable dt = new DataTable();
        public bool IsCreate = false;
        string rzb = "1";
        private bool isReadonly = false;
        private string layerID = "";

        public bool IsReadonly
        {
            get { return isReadonly; }
            set { isReadonly = value; }
        }

        public frmProperty()
        {
            InitializeComponent();
        }
        public void InitData(string useid,string svguid,string Area,string str_rzb,string layID)
        {
      
            //gPro.UID = useid;
            layerID = layID;
            rzb = str_rzb;
            if (Area == "") { Area = "0"; }
            try
            {
                gPro.EleID = useid;
                gPro.SvgUID = svguid;
                IList svglist = Services.BaseService.GetList("SelectglebePropertyByEleID", gPro);
                if (svglist.Count > 0)
                {
                    gPro = (glebeProperty)svglist[0];
                    IsCreate = false;
                }
                else
                {
                    IsCreate = true;
                    gPro.Area = Convert.ToDecimal(Area);
                    gPro.UID = Guid.NewGuid().ToString();
                    gPro.ObligateField11 = "是";
                    gPro.LayerID = layerID;
                }
                
                bh.DataBindings.Add("Text", gPro, "UseID");
                lx.DataBindings.Add("EditValue", gPro, "TypeUID");
                mj.DataBindings.Add("Text", gPro, "Area");
                fh.DataBindings.Add("Text", gPro, "Burthen");
                dl.DataBindings.Add("Text", gPro, "Number");
                xyxs.DataBindings.Add("Text", gPro, "ObligateField11");
                remark.DataBindings.Add("Text", gPro, "Remark");
               comboBoxEdit1.DataBindings.Add("EditValue", gPro, "ObligateField7");
               // comboBoxEdit1.DataBindings.Add("Text", gPro, "ObligateField12");
               //comboBoxEdit2.DataBindings.Add("Text", gPro, "ObligateField13");
            }
            catch(Exception e){
                MessageBox.Show(e.Message);
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(gPro.UseID==""){
                MessageBox.Show("地块编号不能为空。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            if(lx.Text==""){
                MessageBox.Show("地块类型不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
         

            if (IsCreate)
            {
                glebeProperty gle = new glebeProperty();
                gle.UseID = gPro.UseID;
                gle.SvgUID = gPro.SvgUID;
                IList list = Services.BaseService.GetList("SelectglebePropertyByUseIDCK", gle);
                if (list.Count > 0)
                {
                    MessageBox.Show("地块编号重复。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                gPro.ParentEleID = "1";
                Services.BaseService.Create<glebeProperty>(gPro);
            }
            else
            {
                gPro.Area =Convert.ToDecimal( mj.Text);
                gPro.LayerID = layerID;
                Services.BaseService.Update<glebeProperty>(gPro);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
          
        }

        private void frmProperty_Load(object sender, EventArgs e)
        {
            dt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectglebeTypeList", gType), typeof(glebeType));
            lx.Properties.DataSource = dt;
            bh.Focus();
            string DQ = "市区";
           string con = "AreaID = '" + Itop.Client.MIS.ProgUID + "'";
           IList<PSP_Substation_Info> list = Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", con);
            foreach (PSP_Substation_Info psu in list)
            {
                comboBoxEdit1.Properties.Items.Add(psu.Title);
            }
           
            //string conn = "ProjectID='" + Itop.Client.MIS.ProgUID + "' and Col1='" + DQ + "' order by Sort";
            //IList<PS_Table_AreaWH> list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);
            //foreach (PS_Table_AreaWH area in list) {
            //    this.comboBoxEdit1.Properties.Items.Add(area.Title);
            //}
            //for (int i = 0; i < 60; i++) {
            //    string y = (2011 + i).ToString();
            //    this.comboBoxEdit2.Properties.Items.Add(y);
            //}
            if(IsReadonly){
                bh.Properties.ReadOnly = true;
               //*、、*/ comboBoxEdit1.Properties.ReadOnly = true;
               // comboBoxEdit2.Properties.ReadOnly = true;
                lx.Properties.ReadOnly = true;
                mj.Properties.ReadOnly = true;
                fh.Properties.ReadOnly = true;
                dl.Properties.ReadOnly = true;
                xyxs.Properties.ReadOnly = true;
                remark.Properties.ReadOnly = true;
                comboBoxEdit1.Properties.ReadOnly = true;
                simpleButton1.Visible = false;
                simpleButton2.Text = "关闭";
            }
        }

        private void lx_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string uid = lx.EditValue.ToString();
                DataRowView rowView = (DataRowView)lx.Properties.GetDataSourceRowByKeyValue(uid);
                if(rowView!=null){            
                    string md = rowView.Row["TypeStyle"].ToString();
                    string xs = rowView.Row["ObligateField2"].ToString();
                    string rjl = rowView.Row["ObligateField3"].ToString();

                    fh.Text = Convert.ToString((Convert.ToDouble(mj.Text) * Convert.ToDouble(md)) * Convert.ToDouble(xs)* Convert.ToDouble(rjl));
                    dl.Text = Convert.ToString( Convert.ToDouble(mj.Text) * Convert.ToDouble(md) * Convert.ToDouble(rzb));
                    gPro.Burthen =Convert.ToDecimal( fh.Text);
                    gPro.Number = Convert.ToDecimal(dl.Text);
                    gPro.ObligateField1 = rowView.Row["ObligateField1"].ToString();
               }
            }
            catch { }

        }

        private void xs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string uid = lx.EditValue.ToString();
                DataRowView rowView = (DataRowView)lx.Properties.GetDataSourceRowByKeyValue(uid);
                if(rowView!=null){            
                    string md = rowView.Row["TypeStyle"].ToString();
                    string xs = rowView.Row["ObligateField2"].ToString();
                    string rjl = rowView.Row["ObligateField3"].ToString();
                    if (xyxs.Text == "是")
                    {
                        fh.Text = Convert.ToString((Convert.ToDouble(mj.Text) * Convert.ToDouble(md)) * Convert.ToDouble(xs) * Convert.ToDouble(rjl));
                    }
                    else
                    {
                        fh.Text = Convert.ToString((Convert.ToDouble(mj.Text) * Convert.ToDouble(md))  * Convert.ToDouble(rjl));
                    }
                    gPro.Burthen = Convert.ToDecimal(fh.Text);
                    //dl.Text = Convert.ToString( Convert.ToDouble(mj.Text) * Convert.ToDouble(md) * Convert.ToDouble(rzb));
                    //gPro.Burthen =Convert.ToDecimal( fh.Text);
                }
            }
            catch{
            }
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}