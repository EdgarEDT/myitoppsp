using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Stutistic;
using DevExpress.XtraEditors;
using Itop.Common;
using Itop.Domain.Graphics;
using System.Collections;
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
    public partial class FrmEditProject5 : FormBase
    {
        public FrmEditProject5()
        {
            InitializeComponent();
        }

        private bool isupdate = false;
        private string flag = "";
        private string poweruid = "";
        private string powerid = "";
        private Hashtable hs = new Hashtable();
        private string flag2 = "";
        private string type = "";
        private string type1 = "";


        private bool isstuff = false;

        public bool Isupdate
        {
            set { isupdate = value; }
            get { return isupdate; }
        }
        public bool IsStuff
        {
            set { isstuff = value; }
            get { return isstuff; }
        }
        public string  Type
        {
            set { type = value; }
            get { return type; }
        }
        public string Type1
        {
            set { type1 = value; }
            get { return type1; }
        }

        /// <summary>
        /// 规划ID
        /// </summary>
        public string FlagId
        {
            set { flag = value; }
            get { return flag; }
        }

        /// <summary>
        /// 变电站，线路ID
        /// </summary>
        //public string PowerId
        //{
        //    set { powerid = value; }
        //    get { return powerid; }
        //}


        public string PowerUId
        {
            set { poweruid = value; }
            get { return poweruid; }
        }

        //public string Flag2
        //{
        //    set { flag2 = value; }
        //    get { return flag2; }
        //}

        //自定义年限使用
        Label[] lb = null;
        TextEdit[] te = null;
        //自定义其他列使用
        Label[] sb = null;
        TextEdit[] se = null;

        private bool isline = false;
        public bool IsLine
        {
            set { isline = value; }
            get { return isline; }
        }

        private bool isPower = false;
        public bool IsPower
        {
            set { isPower = value; }
            get { return isPower; }
        }
        Label la = new Label();
        TextEdit ta = new TextEdit();

        Label lb1 = new Label();
        TextEdit tb = new TextEdit();

        Label lc = new Label();
        TextEdit tc = new TextEdit();

        Label l11 = new Label();
        TextEdit t11 = new TextEdit();

        Label l22 = new Label();
        TextEdit t22 = new TextEdit();

        Label l33 = new Label();
        TextEdit t33 = new TextEdit();

        ComboBoxEdit c1 = new ComboBoxEdit();
        ArrayList ac1 = new ArrayList();
        ComboBoxEdit c2 = new ComboBoxEdit();
        ArrayList ac2 = new ArrayList();

        Label l44 = new Label();
        ComboBoxEdit c3 = new ComboBoxEdit();
        ArrayList ac3 = new ArrayList();


        Label ls1 = new Label();
        Label ls2 = new Label();
        Label ls3 = new Label();
        Label ls4 = new Label();

        TextEdit ts1 = new TextEdit();
        TextEdit ts2 = new TextEdit();
        TextEdit ts3 = new TextEdit();
        TextEdit ts4 = new TextEdit();


        Label lt1 = new Label();
        Label lt2 = new Label(); 
        Label lt3 = new Label(); 
        Label lt4 = new Label();
        Label lt5 = new Label();
        Label lt6 = new Label();

        TextEdit tt1 = new TextEdit();
        TextEdit tt2 = new TextEdit();
        TextEdit tt3 = new TextEdit();
        TextEdit tt4 = new TextEdit();
        TextEdit tt5 = new TextEdit();
        TextEdit tt6 = new TextEdit();
        DateTime dt = new DateTime();
        private void FrmEditProject_Load(object sender, EventArgs e)
        {
            string q0 = "";
            string q1 = "";
            string q2 = "";
            string q3 = "";
            double? q4 = null;
            string q5 = "";
            string q6 = "";
            string q7 = "";
            string q8 = "";
            string q9 = "";
            string q10 = "";
            string q11 = "";
            string q12 = "";
            string q13 = "";
            string q14 = "";
            string q15 = "";
            string q16 = "";
            string q17 = "";
            string q18 = "";
            string q19 = "";
            string q20 = "";
            string q21 = "";
            string q22 = "";
            string q23 = "";
            string q24 = "";


            int t1 = 0;

            PSP_PowerTypes_Liao ppt = new PSP_PowerTypes_Liao();
            try
            {
                ppt.ID = int.Parse(poweruid);
            }
            catch { }
           ppt.Flag2 = flag;

           PSP_PowerTypes_Liao ps = (PSP_PowerTypes_Liao)Common.Services.BaseService.GetOneByKey<PSP_PowerTypes_Liao>(ppt);
           // if (ps != null)
           try
           {
               groupBox1.Text = ps.Title;
               q0 = ps.Title;
               t1 = ps.Flag;
               q1 = ps.JianSheXingZhi;
               q2 = ps.RongLiang;
               q3 = ps.ChangDu;
               dt = ps.CreatTime;
               if (ps.TouZiZongEr.ToString() != "")
               {
                   q4 = double.Parse(ps.TouZiZongEr);
               }
               if (ps.S1 == "" || ps.S1 == null)
               {
                   q5 = "";

               }
               else
               {
                   q5 = ps.S1;
               }
              
               if (ps.S2 != "" && ps.S2 != null)
               {
                   q6 = ps.S2;

               }
             
               if (ps.S3 != "" && ps.S3 != null)
               {
                   q7 = ps.S3;

               }

               if (ps.S4 != "" && ps.S4 != null)
               {
                   q8 = ps.S4;

               }

              
               if (ps.S5 != "" && ps.S5 != null)
               {
                   q9 = ps.S5;

               }
           
               if (ps.S6 != "" && ps.S6 != null)
               {
                   q10 = ps.S6;

               }
              
               if (ps.S7 != "" && ps.S7 != null)
               {
                   q11 = ps.S7;

               }

               
               if (ps.S8 != "" && ps.S8 != null)
               {
                   q12 = ps.S8;

               }
               
               if (ps.S9 != "" && ps.S9 != null)
               {
                   q13 = ps.S9;

               }
              
               if (ps.S10 != "" && ps.S10 != null)
               {
                   q14 = ps.S10;

               }
             
               if (ps.S11 != "" && ps.S11 != null)
               {
                   q15 = ps.S11;

               }
              
               if (ps.S12 != "" && ps.S12 != null)
               {
                   q16 = ps.S12;

               }

            
               if (ps.S13 != "" && ps.S13 != null)
               {
                   q17 = ps.S13;

               }
             
               if (ps.S14 != "" && ps.S14 != null)
               {
                   q18 = ps.S14;

               }
              
               if (ps.S15 != "" && ps.S15 != null)
               {
                   q19 = ps.S15;

               }
              
               if (ps.S16 != "" && ps.S16 != null)
               {
                   q20 = ps.S16;

               }
            
               if (ps.S17 != "" && ps.S17 != null)
               {
                   q21 = ps.S17;

               }

          
               if (ps.S18 != "" && ps.S18 != null)
               {
                   q22 = ps.S18;

               }

           
               if (ps.S19 != "" && ps.S19 != null)
               {
                   q23 = ps.S19;

               }

    
               if (ps.S20 != "" && ps.S20 != null)
               {
                   q24 = ps.S20;

               }
               
              

           }
           catch { }
           ac1.Add(q5);
           ac1.Add(q6);
           ac1.Add(q7);
           ac1.Add(q8);
           ac1.Add(q9);
           ac1.Add(q10);
           ac1.Add(q11);
           ac1.Add(q12);
           ac1.Add(q13);
           ac1.Add(q14);
           ac1.Add(q15);
           ac1.Add(q16);
           ac1.Add(q17);
           ac1.Add(q18);
           ac1.Add(q19);
           ac1.Add(q20);
           ac1.Add(q21);
           ac1.Add(q22);
           ac1.Add(q23);
           ac1.Add(q24);


            PowerValues ppv = new PowerValues();
            if (ps == null)
            ppv.TypeID =-1;
            else
            ppv.TypeID = ps.ID;
            //ppv.TypeID1 = flag;
            IList<PowerValues> listValues = Common.Services.BaseService.GetList<PowerValues>("SelectPowerValuesList", ppv);

            PowerYears pps = new PowerYears();
            pps.Flag = flag;
            IList<PowerYears> li = Common.Services.BaseService.GetList<PowerYears>("SelectPowerYearsListByFlagSortYear", pps);

            lb=new Label[li.Count];
            te=new TextEdit[li.Count];

            int i=0;

            lt3.Text = "项目名称:";
            lt3.Location = new Point(52, 27 + 33 * i);
            groupBox1.Controls.Add(lt3);

            tt3 = new TextEdit();
            tt3.Location = new Point(157, 26 + 33 * i);
            tt3.Size = new Size(231, 21);
            groupBox1.Controls.Add(tt3);

            i++;

                lt2.Text = "建设性质:";
                lt2.Location = new Point(52, 27 + 33 * i);
                groupBox1.Controls.Add(lt2);

                tt2 = new TextEdit();
                tt2.Location = new Point(157, 26 + 33 * i);
                tt2.Size = new Size(231, 21);
                groupBox1.Controls.Add(tt2);

                i++;
     
                lt4.Text = "变电容量:";
                lt4.Location = new Point(52, 27 + 33 * i);
                groupBox1.Controls.Add(lt4);

                tt4 = new TextEdit();
                tt4.Location = new Point(157, 26 + 33 * i);
                tt4.Size = new Size(231, 21);

                tt4.Properties.DisplayFormat.FormatString = "n4";
                tt4.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                tt4.Properties.EditFormat.FormatString = "n4";
                tt4.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                tt4.Properties.Mask.EditMask = "###########0.####";
                tt4.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;


                groupBox1.Controls.Add(tt4);

                i++;
                lt1.Text = "线路长度:";
                lt1.Location = new Point(52, 27 + 33 * i);
                groupBox1.Controls.Add(lt1);

                tt1 = new TextEdit();
                tt1.Location = new Point(157, 26 + 33 * i);
                tt1.Size = new Size(231, 21);

                tt1.Properties.DisplayFormat.FormatString = "n4";
                tt1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                tt1.Properties.EditFormat.FormatString = "n4";
                tt1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                tt1.Properties.Mask.EditMask = "########0.####";
                tt1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

                groupBox1.Controls.Add(tt1);

                i++;

                lt6.Text = "总投资（万元）:";
                lt6.Location = new Point(52, 27 + 33 * i);
                groupBox1.Controls.Add(lt6);

                tt6 = new TextEdit();
                tt6.Location = new Point(157, 26 + 33 * i);
                tt6.Size = new Size(231, 21);

                tt6.Properties.DisplayFormat.FormatString = "n4";
                tt6.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                tt6.Properties.EditFormat.FormatString = "n4";
                tt6.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                tt6.Properties.Mask.EditMask = "############0.####";
                tt6.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

                groupBox1.Controls.Add(tt6);

                i++;
            
            
            int j = 0;
            foreach (PowerYears ppy in li)
            {
                lb[j] = new Label();
                lb[j].Name = "Label" + ppy.Year;
                lb[j].Text = ppy.Year +"年:";
                lb[j].Location = new Point(52, 27 + 33 * i);
                groupBox1.Controls.Add(lb[j]);

                te[j] = new TextEdit();
                te[j].Name = "Text" + ppy.Year;
                te[j].Location = new Point(157, 26 + 33 * i);
                te[j].Size = new Size(231, 21);
                te[j].Properties.DisplayFormat.FormatString = "n4";
                te[j].Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                te[j].Properties.EditFormat.FormatString = "n4";
                te[j].Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                te[j].Properties.Mask.EditMask = "##########0.####";
                te[j].Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                groupBox1.Controls.Add(te[j]);
                if(listValues.Count>0)
                foreach (PowerValues ppy1 in listValues)
                {
                    if (ppy.Year == ppy1.Year)
                        te[j].Text = ppy1.Value.ToString();
                }
                j++;
                i++;
            }


            PowerSubstationLine psl = new PowerSubstationLine();
            psl.Flag ="1" ;
            psl.Type = flag;
            psl.Type2 = type;

            IList<PowerSubstationLine> lli = Itop.Client.Common.Services.BaseService.GetList<PowerSubstationLine>("SelectPowerSubstationLineByFlagType", psl);
            sb = new Label[lli.Count];
            se = new TextEdit[lli.Count];
             j = 0;
             foreach (PowerSubstationLine pss in lli)
            {
                sb[j] = new Label();
                sb[j].Name = "Label" + pss.Title;
                sb[j].Text = pss.Title + ":";
                sb[j].Location = new Point(52, 27 + 33 * i);
                groupBox1.Controls.Add(sb[j]);

                se[j] = new TextEdit();
                se[j].Name = "Text" + pss.Title;
                se[j].Tag = "Text" + pss.ClassType;
                se[j].Location = new Point(157, 26 + 33 * i);
                se[j].Size = new Size(231, 21);
                groupBox1.Controls.Add(se[j]);
                try
                {
                    int num =0;
                    if (se[j].Tag.ToString().Length<=6)
                           num = int.Parse(se[j].Tag.ToString().Substring(se[j].Tag.ToString().Length - 1, 1))-1;
                    else
                           num = int.Parse(se[j].Tag.ToString().Substring(se[j].Tag.ToString().Length - 2, 2)) - 1;
             
                    if (ac1[num].ToString() != DBNull.Value.ToString() && ac1[num].ToString() != "")
                    {
                        se[j].Text = ac1[num].ToString();
                    }
                    else
                    {
                        se[j].Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MsgBox.Show(ex.Message);
                }
                j++;
                i++;
            }



                try
                {
                    tt1.Text = q3.ToString();
                    tt2.Text = q1.ToString();
                    tt3.Text = q0.ToString();
                    tt4.Text = q2.ToString();
                    tt6.Text = q4.ToString();
                    
                }
                catch { }
      

            groupBox1.Size = new Size(434, 30 + 33 * i);
            simpleButton1.Location = new Point(296, 50 + 33 * i);
            simpleButton2.Location = new Point(389, 50 + 33 * i);
            this.Size = new Size(490, 130 + 33 * i);

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
       
           // ac1.Clear();

            for (int i = 0; i < se.Length; i++)
            {
               
                int num = 0;
                if (se[i].Tag.ToString().Length <= 6)
                    num = int.Parse(se[i].Tag.ToString().Substring(se[i].Tag.ToString().Length - 1, 1)) - 1;
                else
                    num = int.Parse(se[i].Tag.ToString().Substring(se[i].Tag.ToString().Length - 2, 2)) - 1;
             
                ac1[num] = se[i].Text.Trim();

            }
            ////for (int i = 0; i < se.Length; i++)
            ////{
            ////   ac1.Add(se[i].Text.Trim());
                
            ////}
            ////for (int i = se.Length; i < 20; i++)
            ////{
            ////    ac1.Add("");

            ////}
            try 
            {
                PSP_PowerTypes_Liao ps = new PSP_PowerTypes_Liao();
                ps.ParentID = 0;
                ps.Flag2 = flag;
               

              
               
                try
                {

                    if(tt1.Text!="")
                        ps.ChangDu = tt1.Text;
                    if (tt2.Text != "")
                        ps.JianSheXingZhi = tt2.Text;
                    if (tt3.Text != "")
                        ps.Title = tt3.Text;
                    if (tt4.Text != "")
                        ps.RongLiang = tt4.Text;
               
                    if (tt6.Text != "")
                        ps.TouZiZongEr = tt6.Text;
                    if (ac1[0]!= null && ac1[0].ToString() != "")
                    {
                        ps.S1 = ac1[0].ToString();
                    }




                    if (ac1[1]!= null && ac1[1].ToString() != "")
                     {
                         ps.S2 = ac1[1].ToString();
                     }


                     if (ac1[2] != null && ac1[2].ToString()!= "")
                     {
                         ps.S3 = ac1[2].ToString();
                     }


                 
                     if (ac1[3] != null && ac1[3].ToString() != "")
                     {
                         ps.S4 = ac1[3].ToString();
                     }

                     if (ac1[4] != null && ac1[4].ToString() != "")
                     {

                         ps.S5 = ac1[4].ToString();
                     }

                    
                     if (ac1[5] != null && ac1[5].ToString() != "")
                     {
                         ps.S6 = ac1[5].ToString();
                     }

                    
                     if (ac1[6] != null && ac1[6].ToString() != "")
                     {
                         ps.S7 = ac1[6].ToString();
                     }


                     if (ac1[7] != null && ac1[7].ToString() != "")
                     {
                         ps.S8 = ac1[7].ToString();
                     }


                     if (ac1[8] != null && ac1[8].ToString() != "")
                     {
                         ps.S9 = ac1[8].ToString();
                     }


                     if (ac1[9] != null && ac1[9].ToString() != "")
                     {
                         ps.S10 = ac1[9].ToString();
                     }


                     if (ac1[10] != null && ac1[10].ToString() != "")
                     {
                         ps.S11 = ac1[10].ToString();
                     }


                     if (ac1[11] != null && ac1[11].ToString() != "")
                     {
                         ps.S12 = ac1[11].ToString();
                     }


                     if (ac1[12] != null && ac1[12].ToString() != "")
                     {
                         ps.S13 = ac1[12].ToString();
                     }


                     if (ac1[13] != null && ac1[13].ToString() != "")
                     {
                         ps.S14 = ac1[13].ToString();
                     }


                     if (ac1[14]!= null && ac1[14].ToString() != "")
                     {
                         ps.S15 = ac1[14].ToString();
                     }


                     if (ac1[15] != null && ac1[15].ToString() != "")
                     {
                         ps.S16 = ac1[15].ToString();
                     }


                     if (ac1[16] != null && ac1[16].ToString() != "")
                     {
                         ps.S17 = ac1[16].ToString();
                     }



                     if (ac1[17] != null && ac1[17].ToString() != "")
                     {
                         ps.S18 = ac1[17].ToString();
                     }

                     if (ac1[18] != null && ac1[18].ToString() != "")
                     {
                         ps.S19 = ac1[18].ToString();
                     }


                     if (ac1[19] != null && ac1[19].ToString() != "")
                     {
                         ps.S20 = ac1[19].ToString();
                     }

                }
                catch { }

                if (Isupdate)
                {
                    ps.ID = int.Parse(poweruid);
                    ps.Flag2 = flag;
                    if (dt.Year < 1900||dt==null||dt.ToString()=="")
                    {
                        dt = DateTime.Now;
                    }
                    ps.CreatTime = dt;
                    Common.Services.BaseService.Update("UpdatePSP_PowerTypes_LiaoByIDflag", ps);

                }
                else
                {
                    PSP_PowerTypes_Liao pptl = (PSP_PowerTypes_Liao)Common.Services.BaseService.GetObject("SelectPSP_PowerTypes_LiaoByTitleFlag2", ps);
                    if (pptl == null)
                    {
                        ps.CreatTime = DateTime.Now;
                        Common.Services.BaseService.Create("InsertPSP_PowerTypes_Liao", ps);
                    }
                    else
                    {
                        MsgBox.Show("此项目名称已经存在！");
                        return;
                    }
                }


                PSP_PowerTypes_Liao pp_tl = (PSP_PowerTypes_Liao)Common.Services.BaseService.GetObject("SelectPSP_PowerTypes_LiaoByTitleFlag2", ps);
                
                for (int i = 0; i < te.Length; i++)
                {
                    SaveCellValue(te[i].Name.Replace("Text", ""), Convert.ToString(pp_tl.ID), te[i].Text.Trim());
                }
            }
            catch (Exception ex)
            { MsgBox.Show(ex.Message); }


            this.DialogResult = DialogResult.OK;
        }



        private bool SaveCellValue(string year, string typeID, string value)
        {
            PowerValues PValues = new PowerValues();
            try
            {
                PValues.TypeID = int.Parse(typeID);
            }
           catch
            {

            }
            if (value == "")
            {
                value = "0";
 
            }
            PValues.Value =double.Parse(value);
            PValues.Year = int.Parse( year);
           
            try
            {
                Common.Services.BaseService.Update<PowerValues>(PValues);
            }
            catch (Exception ex)
            {
                MsgBox.Show("保存数据出错：" + ex.Message);
                return false;
            }
            return true;
        }


        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}