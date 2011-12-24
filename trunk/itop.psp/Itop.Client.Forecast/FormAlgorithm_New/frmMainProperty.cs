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
using Itop.Domain.Table;
using Itop.Client.Base;
namespace Itop.Client.Forecast.FormAlgorithm_New 
{
    public partial class frmMainProperty : FormBase
    {
        glebeProperty gPro = new glebeProperty();
        glebeType gType = new glebeType();
        DataTable dt = new DataTable();
        DataTable sondt = new DataTable();
        bool IsCreate = false; 
        private bool isReadonly = false;
        private string layerID = "";
          
        ArrayList typelist = new ArrayList();
        string str220 = "";
        string str110 = "";
        string str66 = "";
        double dou_dl=0;
        double dou_fh=0;
        decimal nullvalue = 0;
        public string strID = "";
        public string Str = "";
        public bool IsReadonly
        {
            get { return isReadonly; }
            set { isReadonly = value; }
        }
        public frmMainProperty()
        {
            InitializeComponent();
        }
        public void InitData(glebeProperty gp,string _sub220,string _sub110,string _sub66)
        {
            try
            {
                str220 = _sub220;
                str110 = _sub110;
                str66 = _sub66;

                gPro.EleID = gp.EleID;
                gPro.SvgUID = gp.SvgUID;
                layerID = gp.LayerID;

                IList svglist = Services.BaseService.GetList("SelectglebePropertyByEleID", gPro);
                if (svglist.Count > 0)
                {
                    gPro = (glebeProperty)svglist[0];
                     
                    IsCreate = false;
                    if (gPro.SonUid=="")
                    {
                        gPro.SonUid = "2";
                    }
                }
                else
                {
                    IsCreate = true;
                    gPro.LayerID = layerID;
                }
                glebeType g = new glebeType();
                g.UID = "6ab9af7b-3d97-4e6c-8ed7-87b76950b90b";
                g = (glebeType)Services.BaseService.GetObject("SelectglebeTypeByKey", g);
                if (gPro.ObligateField10 == "") { gPro.ObligateField10 = "0"; }
                gPro.ObligateField10 = gPro.ObligateField10.Replace("-","");
                if (gPro.ObligateField10 != "0")
                {
                    nullvalue = Convert.ToDecimal(gPro.ObligateField10) * Convert.ToDecimal(g.TypeStyle);
                }
                bh.DataBindings.Add("Text", gPro, "UseID");
                lx.DataBindings.Add("EditValue", gPro, "TypeUID");
              
                mj.DataBindings.Add("Text", gPro, "Area");
               
                fh.DataBindings.Add("Text", gPro, "Burthen");
                dl.DataBindings.Add("Text", gPro, "Number");
                rzb.DataBindings.Add("Text", gPro, "SonUid");
                remark.DataBindings.Add("Text", gPro, "Remark");

                yk1.DataBindings.Add("Text", gPro, "ObligateField2");
                jc1.DataBindings.Add("Text", gPro, "ObligateField3");
                nl1.DataBindings.Add("Text", gPro, "ObligateField4");
                yk2.DataBindings.Add("Text", gPro, "ObligateField5");
                jc2.DataBindings.Add("Text", gPro, "ObligateField6");
                nl2.DataBindings.Add("Text", gPro, "ObligateField7");
                ph1.DataBindings.Add("Text", gPro, "ObligateField8");
                ph2.DataBindings.Add("Text", gPro, "ObligateField9");

                yk3.DataBindings.Add("Text", gPro, "ObligateField12");
                jc3.DataBindings.Add("Text", gPro, "ObligateField13");
                nl3.DataBindings.Add("Text", gPro, "ObligateField14");
                ph3.DataBindings.Add("Text", gPro, "ObligateField15");
                comboBoxEdit1.DataBindings.Add("Text", gPro, "ObligateField16");
            
                Hashtable hs = new Hashtable();
                hs.Add("ParentEleID", gPro.EleID);
                hs.Add("SvgUID", gPro.SvgUID);
                sondt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectglebePropertParentID", hs), typeof(glebeProperty));
                gridControl.DataSource = sondt;

                string temp = "";
                foreach (DataRow row in sondt.Rows)
                {
                    if (temp!=row["TypeUID"].ToString()){
                        temp=row["TypeUID"].ToString();
                        if (!typelist.Contains(row["TypeUID"]))
                        {
                            typelist.Add(row["TypeUID"]);
                        }
                    }
                }
                //if (gPro.Number==0 || gPro.Burthen==0)
                //{
                Hashtable hs1 = new Hashtable();
                if(gPro.SelSonArea!=""){
                    
                    string[] selArea = gPro.SelSonArea.Split(";".ToCharArray());
                    for (int i = 0; i < selArea.Length;i++ )
                    {
                        if(selArea[i]!=""){
                            string[] _SonArea = selArea[i].Split(",".ToCharArray());
                            hs1.Add(_SonArea[0], _SonArea[1]);                     
                        }
                    }
                }
                
                IEnumerator Ilist= sondt.Rows.GetEnumerator();
                while(Ilist.MoveNext()){
                   DataRow row= (DataRow)Ilist.Current;
                   
                   string eleid = (string)hs1[row["EleID"]];
                   if (eleid!="")
                    {
                        dou_fh = dou_fh + (Convert.ToDouble(hs1[row["EleID"]]) / Convert.ToDouble(row["Area"])) * Convert.ToDouble(row["Burthen"]);
                    }
                }
               
                gPro.Burthen = Convert.ToDecimal(dou_fh)+nullvalue;
                fh.Text = gPro.Burthen.ToString("#####.####");
                //gPro.Number = Convert.ToDecimal( dou_dl.ToString("#####.##"));
                //}
                if (fh.Text == "") { fh.Text = "0"; }
                if (dl.Text == "") { dl.Text = "0"; }
                gPro.Burthen = Convert.ToDecimal(fh.Text);
                if (gPro.Area == 0) { return; }

                pjfh.Text = Convert.ToDecimal(Convert.ToDecimal(fh.Text) / (Convert.ToDecimal(gPro.Area)+Convert.ToDecimal(gPro.ObligateField10))).ToString("#####.####");
                //辅助决策
                if ((gPro.ObligateField2 == "" || gPro.ObligateField2 == null) && (gPro.ObligateField5 == "" || gPro.ObligateField5==null))
                {
                    Reload();
                }
                if(strID==""){
                    return;
                }
                string sql = " where SUID in ( " +strID+ ")";
                IList<PSPDEV> l22=  Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
                gridControl1.DataSource = l22;

                //添加负荷预测数据
                ctrlglebeYearValue1.ParentObj = gPro;
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
        public void ReLoadViewData(glebeProperty gp, string _sub220, string _sub110,string _sub66)
        {
            double dou_fh1 = 0;
            try
            {
                str220 = _sub220;
                str110 = _sub110;
                str66 = _sub66;

                gPro.EleID = gp.EleID;
                gPro.SvgUID = gp.SvgUID;
                layerID = gp.LayerID;

                IList svglist = Services.BaseService.GetList("SelectglebePropertyByEleID", gPro);
                if (svglist.Count > 0)
                {
                    gPro = (glebeProperty)svglist[0];

                    IsCreate = false;
                    if (gPro.SonUid == "")
                    {
                        gPro.SonUid = "2";
                    }
                }
                else
                {
                    IsCreate = true;
                    gPro.LayerID = layerID;
                }
                glebeType g = new glebeType();
                g.UID = "6ab9af7b-3d97-4e6c-8ed7-87b76950b90b";
                g = (glebeType)Services.BaseService.GetObject("SelectglebeTypeByKey", g);
                if (gPro.ObligateField10 == "") { gPro.ObligateField10 = "0"; }

                nullvalue = Convert.ToDecimal(gPro.ObligateField10) * Convert.ToDecimal(g.TypeStyle);

                //bh.DataBindings.Add("Text", gPro, "UseID");
                //lx.DataBindings.Add("EditValue", gPro, "TypeUID");

                //mj.DataBindings.Add("Text", gPro, "Area");

                //fh.DataBindings.Add("Text", gPro, "Burthen");
                //dl.DataBindings.Add("Text", gPro, "Number");
                //rzb.DataBindings.Add("Text", gPro, "SonUid");
                //remark.DataBindings.Add("Text", gPro, "Remark");

                //yk1.DataBindings.Add("Text", gPro, "ObligateField2");
                //jc1.DataBindings.Add("Text", gPro, "ObligateField3");
                //nl1.DataBindings.Add("Text", gPro, "ObligateField4");
                //yk2.DataBindings.Add("Text", gPro, "ObligateField5");
                //jc2.DataBindings.Add("Text", gPro, "ObligateField6");
                //nl2.DataBindings.Add("Text", gPro, "ObligateField7");
                //ph1.DataBindings.Add("Text", gPro, "ObligateField8");
                //ph2.DataBindings.Add("Text", gPro, "ObligateField9");

                //Hashtable hs = new Hashtable();
                //hs.Add("ParentEleID", gPro.EleID);
                //hs.Add("SvgUID", gPro.SvgUID);
                //sondt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectglebePropertParentID", hs), typeof(glebeProperty));
                //gridControl.DataSource = sondt;

                //string temp = "";
                //foreach (DataRow row in sondt.Rows)
                //{
                //    if (temp != row["TypeUID"].ToString())
                //    {
                //        temp = row["TypeUID"].ToString();
                //        typelist.Add(row["TypeUID"]);
                //    }
                //}
                //if (gPro.Number==0 || gPro.Burthen==0)
                //{
                Hashtable hs1 = new Hashtable();
                if (gPro.SelSonArea != "")
                {

                    string[] selArea = gPro.SelSonArea.Split(";".ToCharArray());
                    for (int i = 0; i < selArea.Length; i++)
                    {
                        if (selArea[i] != "")
                        {
                            string[] _SonArea = selArea[i].Split(",".ToCharArray());
                            hs1.Add(_SonArea[0], _SonArea[1]);
                        }
                    }
                } 

                IEnumerator Ilist = sondt.Rows.GetEnumerator();
                while (Ilist.MoveNext())
                {
                    DataRow row = (DataRow)Ilist.Current;

                    string eleid = (string)hs1[row["EleID"]];
                    if (eleid != "")
                    {
                        dou_fh1 = dou_fh1 + (Convert.ToDouble(hs1[row["EleID"]]) / Convert.ToDouble(row["Area"])) * Convert.ToDouble(row["Burthen"]);
                    }
                }

                gPro.Burthen = Convert.ToDecimal(dou_fh1) + nullvalue;
                fh.Text = gPro.Burthen.ToString("#####.####");
                //gPro.Number = Convert.ToDecimal( dou_dl.ToString("#####.##"));
                //}
                if (fh.Text == "") { fh.Text = "0"; }
                if (dl.Text == "") { dl.Text = "0"; }
                gPro.Burthen = Convert.ToDecimal(fh.Text);

                pjfh.Text = Convert.ToDecimal(Convert.ToDecimal(fh.Text) / (Convert.ToDecimal(gPro.Area)+Convert.ToDecimal(gPro.ObligateField10))).ToString("#####.####");
                //辅助决策
                //if ((gPro.ObligateField2 == "" || gPro.ObligateField2 == null) && (gPro.ObligateField5 == "" || gPro.ObligateField5 == null))
                //{
                //    Reload();
                //}
                //空间负荷进行自动的刷新
                ctrlglebeYearValue1.ParentObj = gPro;
                ctrlglebeYearValue1.Refresh();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
        public void Reload()
        {
            string str_nl1 = "";
            string str_nl2 = "";
            string str_nl3 = "";
            decimal fh1 = 0;
            decimal fh2 = 0;
            decimal fh3 = 0;
            decimal rl1 = 0;
            decimal rl2 = 0;
            decimal rl3 = 0;
            int count220 = 0;
            int count110 = 0;
            int count66 = 0;
            if (str220 == "")
            {
                str220 = "'x'";
            }
            if (str110 == "")
            {
                str110 = "'x'";
            }
            if (str66 == "")
            {
                str66 = "'x'";
            }
            //rl1 = Convert.ToDecimal(ph1.Text) * Convert.ToDecimal(fh.Text);
            //substation _substat = new substation();
            //_substat.SvgUID = gPro.SvgUID;
            //_substat.EleID = str220;
            //_substat.glebeEleID = gPro.UID;
            //_substat.ObligateField1 = "220";
            string s1 = " UID in (" + str220 + ") ";
            IList sub220List = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", s1);
            //_substat.ObligateField1 = "110";
            //_substat.EleID = str110;
            string s2 = " UID in (" + str110 + ") ";
            IList sub110List = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", s2);
            //_substat.ObligateField1 = "66";
            //_substat.EleID = str66;
            //IList sub66List = Services.BaseService.GetList("SelectsubstationByMainGlebeByLike", _substat);
            for (int i = 0; i < sub220List.Count; i++)
            {
                PSP_Substation_Info _s = (PSP_Substation_Info)sub220List[i];
                if (_s.S4 == "公用")
                {
                    //if (_s.ObligateField2 == null) { _s.ObligateField2 = "0"; }
                    str_nl1 = str_nl1 + "已有220kV　" + _s.Title + "\r\n";
                    //供电能力：" + Convert.ToDecimal(_s.Number * Convert.ToDecimal(_s.ObligateField2.Replace("%","")) / 100).ToString("######.####") + "MW\r\n";
                    str_nl1 = str_nl1 + "　　容量：" + _s.L2 + "MVA\r\n";
                    rl1 = rl1 + Convert.ToDecimal(_s.L2);
                    fh1 = fh1 + Convert.ToDecimal(_s.L9);// Convert.ToDecimal(_s.Number * Convert.ToDecimal(_s.ObligateField2.Replace("%","")) / 100);
                    count220 = count220 + 1;
                }
                else
                {
                    str_nl1 = str_nl1 + "已有220kV用户变　" + _s.Title + "\r\n";
                    str_nl1 = str_nl1 + "　　容量：" + _s.L2 + "MVA\r\n";
                }
            }

            if (count220 > 1)
            {
                str_nl1 = str_nl1 + "合计供电能力：" + rl1.ToString() + "MWA";
            }
            if ((sub220List.Count == 0 && sub110List.Count == 0 && sub110List.Count == 0) || (sub220List.Count > 0))
            {
                //nl1.EditValue = str_nl1;
                gPro.ObligateField4 = str_nl1;
                gPro.ObligateField8 = Convert.ToString(rl1 - Convert.ToDecimal(rzb.Text) * Convert.ToDecimal(fh.Text)) + "MVA";
                //yk1.Text = Convert.ToString(fh1 - Convert.ToDecimal(fh.Text)) + "MW";
                gPro.ObligateField2 = Convert.ToString(fh1 - Convert.ToDecimal(fh.Text)) + "MW";
                if (fh1 - nullvalue - Convert.ToDecimal(fh.Text) < 0
                    || rl1 - Convert.ToDecimal(rzb.Text) * Convert.ToDecimal(fh.Text) < 0)
                {
                    gPro.ObligateField3 = "建议该区域新建一座220kV变电站。";
                    //jc1.Text = "建议该区域新建一座220kV变电站。";
                }
                else
                {
                    gPro.ObligateField3 = "该区域供电能力满足要求。";
                    //jc1.Text = "该区域供电能力满足要求。";
                }
            }

            //110KV
            for (int i = 0; i < sub110List.Count; i++)
            {
                PSP_Substation_Info _s2 = (PSP_Substation_Info)sub110List[i];
                //if (_s2.ObligateField2 == null) { _s2.ObligateField2 = "0"; }
                if (_s2.S4 == "公用")
                {

                    str_nl2 = str_nl2 + "已有110kV　" + _s2.Title + "\r\n";// + "　　供电能力：" + Convert.ToDecimal(_s2.Number * Convert.ToDecimal(_s2.ObligateField2.Replace("%","")) / 100).ToString("######.####") + "MW\r\n";

                    str_nl2 = str_nl2 + "　　容量：" + _s2.L2 + "MVA\r\n";
                    rl2 = rl2 + Convert.ToDecimal(_s2.L2);
                    fh2 = fh2 + Convert.ToDecimal(_s2.L9);//Convert.ToDecimal(_s2.Number * Convert.ToDecimal(_s2.ObligateField2.Replace("%","")) / 100);
                    count110 = count110 + 1;
                }
                else
                {
                    str_nl2 = str_nl2 + "已有110kV用户变　" + _s2.Title + "\r\n";
                    str_nl2 = str_nl2 + "　　容量：" + _s2.L2 + "MVA\r\n";
                }
            }

            if (count110 > 1)
            {
                str_nl2 = str_nl2 + "合计供电能力：" + fh2.ToString() + "MW";
            }
            if ((sub220List.Count == 0 && sub110List.Count == 0) || (sub110List.Count > 0))
            {
                //nl2.EditValue = str_nl2;
                gPro.ObligateField5 = Convert.ToString(fh2 - Convert.ToDecimal(fh.Text)) + "MW";
                gPro.ObligateField7 = str_nl2;
                gPro.ObligateField9 = Convert.ToString(rl2 - Convert.ToDecimal(rzb.Text) * Convert.ToDecimal(fh.Text)) + "MVA";
                //yk2.Text = Convert.ToString(fh2 - Convert.ToDecimal(fh.Text)) + "MW";
                if (fh2 - nullvalue - Convert.ToDecimal(fh.Text) < 0
                    || rl2 - Convert.ToDecimal(rzb.Text) * Convert.ToDecimal(fh.Text) < 0)
                {
                    gPro.ObligateField6 = "建议该区域新建一座110kV变电站。";
                    //jc2.Text = "建议该区域新建一座110kV变电站。";
                }
                else
                {
                    gPro.ObligateField6 = "该区域供电能力满足要求。";
                    //jc2.Text = "该区域供电能力满足要求。";
                }
            }
            // 66KV
            //for (int i = 0; i < sub66List.Count; i++)
            //{
            //    substation _s3 = (substation)sub66List[i];
            //    if (_s3.ObligateField2 == null) { _s3.ObligateField2 = "0"; }
            //    if (_s3.ObligateField4 == "局有")
            //    {
            //        str_nl3 = str_nl3 + "已有66kV　" + _s3.EleName + "\r\n" + "　　供电能力：" + Convert.ToDecimal(_s3.Number * Convert.ToDecimal(_s3.ObligateField2.Replace("%", "")) / 100).ToString("######.####") + "MW\r\n";

            //        str_nl3 = str_nl3 + "　　容量：" + _s3.Number + "MVA\r\n";
            //        rl3 = rl3 + _s3.Number;
            //        fh3 = fh3 + Convert.ToDecimal(_s3.Number * Convert.ToDecimal(_s3.ObligateField2.Replace("%", "")) / 100);
            //        count66 = count66 + 1;
            //    }
            //    else
            //    {
            //        str_nl3 = str_nl3 + "已有66kV用户变　" + _s3.EleName + "\r\n";
            //        str_nl3 = str_nl3 + "　　容量：" + _s3.Number + "MVA\r\n";
            //    }
            //}

            //if (count66 > 1)
            //{
            //    str_nl3 = str_nl3 + "合计供电能力：" + fh3.ToString() + "MW";
            //}
            //if ((sub220List.Count == 0 && sub110List.Count == 0 && sub66List.Count == 0) || (sub66List.Count > 0))
            //{
            //    //nl2.EditValue = str_nl2;
            //    gPro.ObligateField12 = Convert.ToString(fh3 - Convert.ToDecimal(fh.Text)) + "MW";
            //    gPro.ObligateField14 = str_nl3;
            //    gPro.ObligateField15 = Convert.ToString(rl3 - Convert.ToDecimal(rzb.Text) * Convert.ToDecimal(fh.Text)) + "MVA";
            //    //yk2.Text = Convert.ToString(fh2 - Convert.ToDecimal(fh.Text)) + "MW";
            //    if (fh3 - nullvalue - Convert.ToDecimal(fh.Text) < 0
            //        || rl3 - Convert.ToDecimal(rzb.Text) * Convert.ToDecimal(fh.Text) < 0)
            //    {
            //        gPro.ObligateField13 = "建议该区域新建一座66kV变电站。";
            //        //jc2.Text = "建议该区域新建一座110kV变电站。";
            //    }
            //    else
            //    {
            //        gPro.ObligateField13 = "该区域供电能力满足要求。";
            //        //jc2.Text = "该区域供电能力满足要求。";
            //    }
            //}

            yk1.Text = gPro.ObligateField2;
            jc1.Text = gPro.ObligateField3;
            nl1.Text = gPro.ObligateField4;
            yk2.Text = gPro.ObligateField5;
            jc2.Text = gPro.ObligateField6;
            nl2.Text = gPro.ObligateField7;
            ph1.Text = gPro.ObligateField8;
            ph2.Text = gPro.ObligateField9;

            yk3.Text = gPro.ObligateField12;
            jc3.Text = gPro.ObligateField13;
            nl3.Text = gPro.ObligateField14;
            ph3.Text = gPro.ObligateField15;
            comboBoxEdit1.Text=gPro.ObligateField16;
            remark.Text = gPro.Remark;
            this.Refresh();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {

            if (bh.Text == "")
            {
                MessageBox.Show("区域编号不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (rzb.Text == "")
            {
                MessageBox.Show("容载比不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (fh.Text == "")
            {
                MessageBox.Show("负荷不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (IsCreate)
            {
                gPro.LayerID = layerID;
                Services.BaseService.Create<glebeProperty>(gPro);
            }
            else
            {
                gPro.LayerID = layerID;
                //gPro.Number = Convert.ToDecimal(fh.Text) * Convert.ToDecimal(rzb.Text);
                Services.BaseService.Update<glebeProperty>(gPro);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmMainProperty_Load(object sender, EventArgs e)
        {
            dt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectglebeTypeList", gType), typeof(glebeType));
            lx.Properties.DataSource = dt;
            bh.Focus();
            if (IsReadonly)
            {
                bh.Properties.ReadOnly = true;
                lx.Properties.ReadOnly = true;
                mj.Properties.ReadOnly = true;
                fh.Properties.ReadOnly = true;
                dl.Properties.ReadOnly = true;
                remark.Properties.ReadOnly = true;
                comboBoxEdit1.Properties.ReadOnly = true;

                rzb.Properties.ReadOnly = true;;


                yk1.Properties.ReadOnly = true; ;
                jc1.Properties.ReadOnly = true; ;
                nl1.Properties.ReadOnly = true; ;
                yk2.Properties.ReadOnly = true; ;
                jc2.Properties.ReadOnly = true; ;
                nl2.Properties.ReadOnly = true; ;
                ph1.Properties.ReadOnly = true; ;
                ph2.Properties.ReadOnly = true; ;

                yk3.Properties.ReadOnly = true; ;
                jc3.Properties.ReadOnly = true; ;
                nl3.Properties.ReadOnly = true; ;
                ph3.Properties.ReadOnly = true; ;
                //simpleButton1.Visible = false;
                simpleButton2.Text = "关闭";
            }
            string DQ = "市区";
            string conn = "ProjectID='" + Itop.Client.MIS.ProgUID + "' and Col1='" + DQ + "' order by Sort";
            IList<PS_Table_AreaWH> list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);
            foreach (PS_Table_AreaWH area in list) {
                this.comboBoxEdit1.Properties.Items.Add(area.Title);
            }
        }
        public glebeProperty glebeProp
        {
            set { gPro = value; }
            get { return gPro; }
        }

        private void rzb_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                gPro.Number = Convert.ToDecimal(fh.Text) * Convert.ToDecimal(rzb.Text);
            }
            catch { }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (rzb.Text == "")
            {
                MessageBox.Show("容载比不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("确定要重新计算么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Reload();
            }
        }

        private void bt4_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    frmChangeInput frm = new frmChangeInput();
            //    frm.InitData(typelist);
            //    if (frm.ShowDialog()==DialogResult.OK)
            //    {
            //        for (int j = 0; j < frm.list2.Count; j++)
            //        {
            //            glebeType g = frm.list2[j];
            //            foreach (DataRow row in sondt.Rows)
            //            {
            //                if (row["TypeUID"].ToString() == g.TypeName)
            //                {
            //                    row["Burthen"] = Convert.ToDouble(row["Area"]) * Convert.ToDouble(g.TypeStyle) * Convert.ToDouble(g.ObligateField2) * Convert.ToDouble(g.ObligateField3);
            //                }
            //            }
            //        }
            //        ReLoadViewData(gPro, str220, str110,str66);
            //    }
       
               
            //}
            //catch(Exception e1){
            //    string err = e1.Message;
            //}
        }

        private void xtraTabPage2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void buttonEdit1_Properties_Click(object sender, EventArgs e)
        {
           
        }

        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
           
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            //frmAreaSel f = new frmAreaSel();
            //if (f.ShowDialog() == DialogResult.OK) {
            //    string id = f.FocusedObject.ID;
            //    PS_Table_AreaWH w = new PS_Table_AreaWH();
            //    w.ID = id;
            //    w = (PS_Table_AreaWH)Services.BaseService.GetObject("SelectPS_Table_AreaWHByKey", w);
            //    if (w != null) {
            //        w.Col2 = Str;
            //        comboBoxEdit1.Text = w.Title;
            //        Services.BaseService.Update<PS_Table_AreaWH>(w);
            //    }
            //}
        }
       
        //public void colSelAreaVisible(bool b)
        //{
        //    colSelArea.Visible = b;
        //}
    }
}