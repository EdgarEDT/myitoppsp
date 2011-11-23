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
    public partial class frmSubstationProperty_wh : FormBase
    {
        substation sub = new substation();
        public string jwstr = "";
        public substation Sub
        {
            get { return sub; }
            set { sub = value; }
        }
        //glebeType gType = new glebeType();
        DataTable dt = new DataTable();
        Substation_Info p;
        public bool IsCreate = false;
        string rzb = "1";
        private bool isReadonly = false;
        private string layerID = "";
        private string LabelTxt = "";

        public string OldUID = "";
        public bool SubUpdate = false;
        public bool IsReadonly
        {
            get { return isReadonly; }
            set { isReadonly = value; }
        }

        public frmSubstationProperty_wh()
        {
            InitializeComponent();
            object[] oj = new object[60];
            for (int o = 0; o < 60; o++)
            {
                oj[o] = 1970 + o;
            }
            this.comboTcnf.Properties.Items.AddRange(oj);
        }
        public void InitData(string useid,string svguid,string layID,string lab)
        {
            LabelTxt = lab;
            layerID = layID;
            //gPro.UID = useid;
            //rzb = str_rzb;
            try
            {
                sub.EleID = useid;
                sub.SvgUID = svguid;
                IList svglist = Services.BaseService.GetList("SelectsubstationByEleID", sub);
                if (svglist.Count > 0)
                {
                    sub = (substation)svglist[0];
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
                    sub.UID = Guid.NewGuid().ToString();    
                }

                if (getLvl(LabelTxt) == "500" && sub.ObligateField2 == "")
                {
                    sub.ObligateField2 = "65.00%";
                }
                if (getLvl(LabelTxt) == "220" && sub.ObligateField2 == "")
                {
                    sub.ObligateField2 = "59.00%";
                }
                if (getLvl(LabelTxt) == "66" && sub.ObligateField2 == "")
                {
                    sub.ObligateField2 = "50.00%";
                }

                bh.DataBindings.Add("Text", sub, "EleName");
                lx.DataBindings.Add("EditValue", sub, "glebeEleID");
                //mj.DataBindings.Add("Text", gPro, "Area");
                fhl.DataBindings.Add("EditValue", sub, "ObligateField2");
                Maxfh.DataBindings.Add("Text", sub, "Burthen");
                dl.DataBindings.Add("Text", sub, "Number");
                remark.DataBindings.Add("Text", sub, "Remark");
                //comboTcnf.DataBindings.Add("Text", sub, "ObligateField5");
                jsdd.DataBindings.Add("Text", sub, "ObligateField6");
                zbts.DataBindings.Add("Text", sub, "ObligateField7");
               
                if (sub.ObligateField5 != null)
                {
                    if(sub.ObligateField5.Length==4){
                        comboTcnf.Text = sub.ObligateField5;
                    }
                    string[] s = sub.ObligateField5.Split("-".ToCharArray());
                    if (s.Length > 1)
                    {
                        comboTcnf.Text = s[0];
                        comy.Text = s[1];
                    }
                }
            
            }
            catch(Exception e){
                MessageBox.Show(e.Message);
            }

        }
        public string getLvl(string lab)
        {
            if (lab.Contains("1000"))
            {
                return "1000";
            }
            if(lab.Contains("500")){
                return "500";
            }
            if(lab.Contains("220")){
                return "220";
            }
            if (lab.Contains("110"))
            {
                return "110";
            }
            if (lab.Contains("66"))
            {
                return "66";
            }
            if (lab.Contains("35"))
            {
                return "35";
            }
            else
                return "500";
        }
        public string getSubType(string lab)
        {
            if (lab.Contains("gh"))
            {
                return "规划";
            }
            else
                return "运行";
        }
        public string getObligateField4(string lab)
        {
            if (lab.Contains("user"))
            {
                return "自维";
            }
            else
                return "局有";

        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (bh.Text == "") {
                MessageBox.Show("变电站名称不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //if (dl.Text == "") {
            //    MessageBox.Show("变电站容量不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //if (getObligateField4(LabelTxt) == "局有")
            //{
            //    if (fhl.Text == "")
            //    {
            //        MessageBox.Show("负荷率不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }
            //}
            //if (Maxfh.Text == "")
            //{
            //    MessageBox.Show("最大负荷不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //if (comboTcnf.Text != "" && comy.Text != "")
            //{
            sub.ObligateField5 = comboTcnf.Text;// +"-" + comy.Text;
            //}
            if (IsCreate)
            {
                //gPro.ParentEleID = "1";
                substation _s = new substation();
                _s.EleName = sub.EleName;
                _s.SvgUID = sub.SvgUID;
                IList mlist = Services.BaseService.GetList("SelectsubstationByEleNameCK", _s);
                if (mlist.Count > 0) {
                    MessageBox.Show("变电站名称重复。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //return;
                }

                sub.ObligateField1 = getLvl(LabelTxt);
                sub.ObligateField3 = getSubType(LabelTxt);
                sub.ObligateField4 = getObligateField4(LabelTxt);
                sub.ObligateField6 = jsdd.Text;
                sub.ObligateField7 = zbts.Text;
                Services.BaseService.Create<substation>(sub);

            }
            else
            {
                sub.LayerID = layerID;
                sub.ObligateField1 = getLvl(LabelTxt);
                sub.ObligateField3 = getSubType(LabelTxt);
                sub.ObligateField4 = getObligateField4(LabelTxt);
                sub.ObligateField6 = jsdd.Text;
                sub.ObligateField7 = zbts.Text;
                Services.BaseService.Update<substation>(sub);
            }
            if(SubUpdate){

                Substation_Info temp = (Substation_Info)Services.BaseService.GetObject("SelectSubstation_InfoByCode", sub.UID);
                if(temp!=null){
                    temp.Code = "";
                    Services.BaseService.Update<Substation_Info>(temp);
                }
                substation _s = new substation();
                _s.UID = OldUID;
                substation _temps= Services.BaseService.GetOneByKey<substation>(_s);
                if(_temps!=null){
                    if(_temps.EleID==""){
                        Services.BaseService.Update("Deletesubstation",_temps);
                    }
                }
                Services.BaseService.Update<Substation_Info>(p);
            }
            Substation_Info ppt = (Substation_Info)Services.BaseService.GetObject("SelectSubstation_InfoByCode", sub.UID);
            if (ppt != null)
            {
                ppt.L1 =Convert.ToInt32( sub.ObligateField1.ToLower().Replace("kv", ""));
                ppt.Title = sub.EleName;//名称
                //ppt.L1 = "";//台数
                try
                {
                  //  ppt.L2 = Convert.ToDouble(sub.Number);//容量
            
                    ppt.L10 = Convert.ToDouble(sub.ObligateField2);//负荷率
              
                    ppt.L9 = Convert.ToDouble(sub.Burthen);//最大负荷
                    ppt.L22 = sub.ObligateField5;
                    Services.BaseService.Update<Substation_Info>(ppt);
                }
                catch { }
            }
            

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
          
        }

        private void frmProperty_Load(object sender, EventArgs e)
        {

            //Hashtable hs = new Hashtable();
            //hs.Add("ParentEleID", "0");
            //hs.Add("SvgUID", sub.SvgUID);
           
            //IList list = Services.BaseService.GetList("SelectglebePropertParentIDTopAll", hs);
            //dt = Itop.Common.DataConverter.ToDataTable(list, typeof(glebeProperty));
            //lx.Properties.DataSource = dt;
            bh.Focus();
            if(IsReadonly){
                bh.Properties.ReadOnly = true;
                lx.Properties.ReadOnly = true;
                //mj.Properties.ReadOnly = true;
                Maxfh.Properties.ReadOnly = true;
                dl.Properties.ReadOnly = true;
                remark.Properties.ReadOnly = true;
                simpleButton1.Visible = false;
             
                simpleButton2.Text = "关闭";
            }
            IList list = Services.BaseService.GetList("Selectsubstation_Number", "");
            foreach (substation sub in list)
            {
                dl.Properties.Items.Add(sub.Number);
            }
            if (jsdd.Text == "")
            {
                jsdd.Text = jwstr;
            }
        }

        private void lx_EditValueChanged(object sender, EventArgs e)
        {
            

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
           
        }

        private void buttonEdit1_Properties_Click(object sender, EventArgs e)
        {
           
        }

        private void bh_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmSubSelS f = new frmSubSelS();
            if (getSubType(LabelTxt) == "规划")
            {
                f.ReDate(layerID, false, false, getLvl(LabelTxt));
            }
            else
            {
                f.ReDate("", true, false, getLvl(LabelTxt));
            }
            if (f.ShowDialog() == DialogResult.OK)
            {
                p = f.ctrlSubstation_Info1.FocusedObject;
                sub.EleName = p.Title;
                if (p.L2 == null || p.L2==0)
                {
                    sub.Number = 0;
                }
                else
                {
                    sub.Number = Convert.ToDecimal(p.L2);
                }
                sub.ObligateField2 = p.L1.ToString();
                if (p.L9 == null || p.L9==0)
                {
                    sub.Burthen = 0;
                }
                else
                {
                    sub.Burthen = Convert.ToDecimal(p.L9);
                }


                bh.Text = sub.EleName;
                fhl.EditValue = sub.ObligateField2;
                Maxfh.Text = sub.Burthen.ToString("#####.##");
                dl.Text = sub.Number.ToString("#####.##");
                OldUID = p.Code;
                p.Code = sub.UID;
                SubUpdate = true;
            }
           
        }

        private void dl_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void Maxfh_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}