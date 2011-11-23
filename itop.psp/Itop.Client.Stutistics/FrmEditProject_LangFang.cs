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
using Itop.Domain.Stutistics;
using Itop.Client.Common;
using DevExpress.XtraEditors.Controls;
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
    public partial class FrmEditProject_LangFang : FormBase
    {
        public FrmEditProject_LangFang()
        {
            InitializeComponent();
        }

        private int isupdate = 0;
        private string flag = "";
        private string poweruid = "";
        private string powerid = "";
        private Hashtable hs = new Hashtable();
        private string islineflag = "";


        private bool isstuff = false;

        public bool IsStuff
        {
            set { isstuff = value; }
            get { return isstuff; }
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

        public string IsLineFlag
        {
            set { islineflag = value; }
            get { return islineflag; }
        }


        Label[] lb = null;
        TextEdit[] te = null;


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
        Label lt7 = new Label();
        Label lt8 = new Label();
        Label lt9 = new Label();
        Label lt10 = new Label();
        Label lt11 = new Label();
        Label lt12 = new Label();


        TextEdit tt1 = new TextEdit();
        TextEdit tt2 = new TextEdit();
        TextEdit tt3 = new TextEdit();
        TextEdit tt4 = new TextEdit();
        TextEdit tt5 = new TextEdit();
        TextEdit tt6 = new TextEdit();
        ComboBoxEdit cb0 = new ComboBoxEdit();
        ComboBoxEdit cb1 = new ComboBoxEdit();
        ComboBoxEdit cb2 = new ComboBoxEdit();
        ComboBoxEdit cb3 = new ComboBoxEdit();
        ComboBoxEdit cb4 = new ComboBoxEdit();
        ComboBoxEdit cb5 = new ComboBoxEdit();

        LookUpEdit upedit = new LookUpEdit();
        string lookupedit = "";
        private void SetData()
        {
            cb1.Properties.Items.Add("220");
            cb1.Properties.Items.Add("110");
            cb1.Properties.Items.Add("35");
            cb2.Properties.Items.Add("1");
            cb2.Properties.Items.Add("2");
            cb2.Properties.Items.Add("3");
            cb3.Properties.Items.Add("220-240/180");
            cb3.Properties.Items.Add("110-63/50");
            cb4.Properties.Items.Add("室内站");
            cb4.Properties.Items.Add("室外站");
            cb5.Properties.Items.Add("150");
            cb5.Properties.Items.Add("240");
            cb5.Properties.Items.Add("300");
            cb5.Properties.Items.Add("2*240");
            cb5.Properties.Items.Add("2*300");
            cb5.Properties.Items.Add("2*400");
            cb5.Properties.Items.Add("2*630");
            cb5.Properties.Items.Add("4*400");
        }

        private void FrmEditProject_Load(object sender, EventArgs e)
        {
            SetData();
            string q1 = "";
            string q2 = "";
            string q3 = "";
            string q4 = "";
            string q5 = "";
            string q6 = "";
            string q7 = "";
            string q8 = "";
            string q9 = "";
            string q10 = "";
            string q11 = "";
            string q12 = "";

            int t1 = 0;

            PSP_PowerProValues_LangFang ppt = new PSP_PowerProValues_LangFang();
            ppt.ID = poweruid;
            ppt.Flag2 = flag;

            PSP_PowerProValues_LangFang ps = Common.Services.BaseService.GetOneByKey<PSP_PowerProValues_LangFang>(ppt);
            if (ps != null)
            {
                groupBox1.Text = ps.Title;
                powerid = ps.Code;
                t1 = ps.Flag;

               // q1 = ps.L1;
               // q2 = ps.L2;
                q3 = ps.L3;
                q4 = ps.L4;
                q5 = ps.L5;
                q6 = ps.L6;
                q7 = ps.L7;
                q8 = ps.L8;
                q9 = ps.L9;
                q10 = ps.L10.ToString();
                q11 = ps.L11;
                q12 = ps.L12.ToString();

            }


            LineInfo li22 = Common.Services.BaseService.GetOneByKey<LineInfo>(powerid);
            if (li22 != null || t1==1)
            {
                isline = true;
            }

            substation sb = Common.Services.BaseService.GetOneByKey<substation>(powerid);
            if (sb != null || t1 == 2)
            {
                isPower = true;
            }


            PowerProValues ppv = new PowerProValues();
            ppv.TypeID = poweruid;
            ppv.TypeID1 = flag;
            IList<PowerProValues> listValues = Common.Services.BaseService.GetList<PowerProValues>("SelectPowerProValuesList", ppv);




            PowerProYears pps = new PowerProYears();
            pps.Flag = flag;
            IList<PowerProYears> li = Common.Services.BaseService.GetList<PowerProYears>("SelectPowerProYearsListByFlag", pps);

            lb=new Label[li.Count];
            te=new TextEdit[li.Count];

            int i=0;


            //if (!isPower)
           // {
                ////lt1.Text = "设备名称:";
                ////lt1.Location = new Point(52, 27 + 33 * i);
                ////groupBox1.Controls.Add(lt1);

                ////tt1 = new TextEdit();
                ////tt1.Location = new Point(157, 26 + 33 * i);
                ////tt1.Size = new Size(231, 21);
                ////tt1.TextChanged += new EventHandler(tt1_TextChanged);
                ////tt1.Properties.DisplayFormat.FormatString = "n4";
                ////tt1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                ////tt1.Properties.EditFormat.FormatString = "n4";
                ////tt1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                ////tt1.Properties.Mask.EditMask = "#####0.####";
                ////tt1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;


                ////groupBox1.Controls.Add(tt1);

                ////i++;

                ////lt2.Text = "台数:";
                ////lt2.Location = new Point(52, 27 + 33 * i);
                ////groupBox1.Controls.Add(lt2);

                ////tt2 = new TextEdit();
                ////tt2.Location = new Point(157, 26 + 33 * i);
                ////tt2.Size = new Size(231, 21);
                ////groupBox1.Controls.Add(tt2);

                ////i++;
           // }


            //if (!isline)
            {
                lt3.Text = "工 程 名 称 :";
                lt3.Location = new Point(52, 27 + 33 * i);
                groupBox1.Controls.Add(lt3);

               // tt3 = new TextEdit();
                cb0.Location = new Point(157, 26 + 33 * i);
                cb0.Size = new Size(231, 21);
              
                cb0.Properties.DisplayFormat.FormatString = "n0";
                cb0.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                cb0.Properties.EditFormat.FormatString = "n0";
                cb0.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.None;
               // cb0.Properties.Mask.EditMask = "########";
                cb0.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;


                groupBox1.Controls.Add(cb0);

                i++;

                lt4.Text = "电 压 等 级 :";
                lt4.Location = new Point(52, 27 + 33 * i);
                groupBox1.Controls.Add(lt4);



                
               // tt4 = new TextEdit();
                cb1.Location = new Point(157, 26 + 33 * i);
                cb1.Size = new Size(231, 21);

                cb1.Properties.DisplayFormat.FormatString = "n4";
                cb1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                cb1.Properties.EditFormat.FormatString = "n4";
                cb1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.None;
               // cb1.Properties.Mask.EditMask = "#####0.####";
                cb1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;


                groupBox1.Controls.Add(cb1);
                i++;

                lt5.Text = "主 变 台 数 :";
                lt5.Location = new Point(52, 27 + 33 * i);
                groupBox1.Controls.Add(lt5);
                
              //  tt5 = new TextEdit();
                cb2.Location = new Point(157, 26 + 33 * i);
                cb2.Size = new Size(231, 21);

                cb2.Properties.DisplayFormat.FormatString = "####.##";
                cb2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                cb2.Properties.EditFormat.FormatString = "####.##";
                cb2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
               // cb2.Properties.Mask.EditMask = "P2";
                cb2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;


                groupBox1.Controls.Add(cb2);
                i++;

                lt6.Text = "单台容量（MVA）:";
                lt6.Location = new Point(50, 27 + 33 * i);
                lt6.Width = 105;
                groupBox1.Controls.Add(lt6);
                
                //tt6 = new TextEdit();
                cb3.Location = new Point(157, 26 + 33 * i);
                cb3.Size = new Size(231, 21);

                cb3.Properties.DisplayFormat.FormatString = "n4";
                cb3.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                cb3.Properties.EditFormat.FormatString = "n4";
                cb3.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.None;
              //  cb3.Properties.Mask.EditMask = "#####0.####";
                cb3.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;

                groupBox1.Controls.Add(cb3);

                i++;

                lt7.Text = "建 设 形 式 :";
                lt7.Location = new Point(52, 27 + 33 * i);
                groupBox1.Controls.Add(lt7);

                //tt6 = new TextEdit();
                cb4.Location = new Point(157, 26 + 33 * i);
                cb4.Size = new Size(231, 21);

                cb4.Properties.DisplayFormat.FormatString = "n4";
                cb4.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                cb4.Properties.EditFormat.FormatString = "n4";
                cb4.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.None;
               // cb4.Properties.Mask.EditMask = "#####0.####";
                cb4.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;

                groupBox1.Controls.Add(cb4);

                i++;

                lt8.Text = "线路长度（KM）:";
                lt8.Location = new Point(52, 27 + 33 * i);
                groupBox1.Controls.Add(lt8);

                //tt6 = new TextEdit();
                tt4.Location = new Point(157, 26 + 33 * i);
                tt4.Size = new Size(231, 21);

                tt4.Properties.DisplayFormat.FormatString = "n4";
                tt4.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                tt4.Properties.EditFormat.FormatString = "n4";
                tt4.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.None;
              //  tt4.Properties.Mask.EditMask = "#####0.####";
                tt4.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;

                groupBox1.Controls.Add(tt4);

                i++;

                lt9.Text = "导 线 型 号 :";
                lt9.Location = new Point(52, 27 + 33 * i);
                groupBox1.Controls.Add(lt9);

                //tt6 = new TextEdit();
                cb5.Location = new Point(157, 26 + 33 * i);
                cb5.Size = new Size(231, 21);

                cb5.Properties.DisplayFormat.FormatString = "n4";
                cb5.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                cb5.Properties.EditFormat.FormatString = "n4";
                cb5.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.None;
               // cb5.Properties.Mask.EditMask = "#####0.####";
                cb5.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;

                groupBox1.Controls.Add(cb5);

                i++;

                Project_Sum sum = new Project_Sum();
                sum.S5 = islineflag;
                sum.S1 = q4;
                IList<Project_Sum> list = Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5",sum);
                upedit.Properties.DataSource = list;
                LookUpColumnInfo a = new LookUpColumnInfo();
                a.FieldName = "T3";
                a.Caption = "接线形式";
                LookUpColumnInfo c = new LookUpColumnInfo();
                c.FieldName = "T2";
                c.Caption = "出线规模";
                LookUpColumnInfo b = new LookUpColumnInfo();
                b.FieldName = "T1";
                b.Caption = "主变台数及数量";
                LookUpColumnInfo d = new LookUpColumnInfo();
                d.Caption = "静态投资";
                d.FieldName = "Num";
                LookUpColumnInfo nn = new LookUpColumnInfo();
                nn.FieldName = "Name";
                nn.Caption = "编号";
                LookUpColumnInfo f = new LookUpColumnInfo();
                f.FieldName = "Type";
                f.Caption = "类型";
                upedit.Properties.Columns.Add(f);
                upedit.Properties.Columns.Add(nn);
                upedit.Properties.Columns.Add(b);
                upedit.Properties.Columns.Add(c);
                upedit.Properties.Columns.Add(a);
                upedit.Properties.Columns.Add(d);
                upedit.Properties.DisplayMember = "Num";
                //upedit.Properties.SearchMode = SearchMode.AutoComplete;
                //upedit.Properties.AutoSearchColumnIndex = 5;
                upedit.Properties.DropDownRows =5;
                upedit.Properties.PopupWidth = 500;
                upedit.Properties.ShowHeader = true;

                lt10.Text = "投 资 造 价 :";
                lt10.Location = new Point(52, 27 + 33 * i);
                groupBox1.Controls.Add(lt10);
                upedit.Location = new Point(157, 26 + 33 * i);
                upedit.Size = new Size(231, 21);
                upedit.Properties.DisplayFormat.FormatString = "n4";
                upedit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                upedit.Properties.EditFormat.FormatString = "n4";
                upedit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                upedit.Properties.Mask.EditMask = "###############0.####";
                upedit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                upedit.Properties.AllowNullInput=DevExpress.Utils.DefaultBoolean.True;
                upedit.TextChanged += new EventHandler(upedit_TextChanged);
                groupBox1.Controls.Add(upedit);

                i++;

                lt11.Text = "造 价 比 列 :";
                lt11.Location = new Point(52, 27 + 33 * i);
                groupBox1.Controls.Add(lt11);

                //tt6 = new TextEdit();
                tt6.Location = new Point(157, 26 + 33 * i);
                tt6.Size = new Size(231, 21);
                
                tt6.Properties.DisplayFormat.FormatString = "n4";
                tt6.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                tt6.Properties.EditFormat.FormatString = "n4";
                tt6.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                tt6.Properties.Mask.EditMask = "###############0.####";
                tt6.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                tt6.TextChanged += new EventHandler(tt6_TextChanged);
                groupBox1.Controls.Add(tt6);

                i++;

                lt12.Text = "工 程 总 价 :";
                lt12.Location = new Point(52, 27 + 33 * i);
                groupBox1.Controls.Add(lt12);
                
                //tt6 = new TextEdit();
                tt5.Location = new Point(157, 26 + 33 * i);
                tt5.Size = new Size(231, 21);
               
                tt5.Properties.DisplayFormat.FormatString = "n4";
                tt5.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                tt5.Properties.EditFormat.FormatString = "n4";
                tt5.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                tt5.Properties.Mask.EditMask = "###############0.####";
                tt5.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

                groupBox1.Controls.Add(tt5);

                i++;
            }
            
            int j = 0;
            foreach (PowerProYears ppy in li)
            {
                lb[j] = new Label();
                lb[j].Name = "Label" + ppy.Year;
                lb[j].Text = ppy.Year + ":";
                lb[j].Location = new Point(52, 27 + 33 * i);
                groupBox1.Controls.Add(lb[j]);

                te[j] = new TextEdit();
                te[j].Name = "Text" + ppy.Year;
                te[j].Location = new Point(157, 26 + 33 * i);
                te[j].Size = new Size(231, 21);
                groupBox1.Controls.Add(te[j]);

                foreach (PowerProValues ppy1 in listValues)
                {
                    if (ppy.Year == ppy1.Year)
                        te[j].Text = ppy1.Value;
                }
                j++;
                i++;
            }

            if (isline)
            {
                try
                {
                  // cb5.Properties.Items.Contains(q9.ToString());
                    
                    //tt1.Text = q3.ToString();
                    //tt2.Text = q4.ToString();

                    //if (li22 != null)
                    //{
                    //    tt1.Text = li22.Length;
                    //    tt2.Text = li22.LineType;
                    //}
                }
                catch { }



            }
            if (isPower)
            {
                try
                {
                    ////tt3.Text = q1.ToString();
                    //tt4.Text = q4.ToString();
                    //tt5.Text = q5.ToString();
                    //tt6.Text = q6.ToString();

                    //if (sb != null)
                    //{
                    //    tt5.Text = sb.Number.ToString();
                    //    tt4.Text = sb.ObligateField2;
                    //    tt6.Text = sb.Burthen.ToString();
                    //}

                }
                catch { }
            }

            PSP_PowerProValues_LangFang pplf = new PSP_PowerProValues_LangFang();

            pplf.Flag2 = flag;

            IList<PSP_PowerProValues_LangFang> plfs = Common.Services.BaseService.GetList<PSP_PowerProValues_LangFang>("SelectPSP_PowerProValues_LangFangByFlag2OrderL3", pplf);

            tt4.Text = q8.ToString();
            tt5.Text = q10.ToString();
            tt6.Text = q12.ToString();

            if (plfs.Count == 0)
            {
                cb0.Text = "";
            }
            foreach (PSP_PowerProValues_LangFang pv in plfs)
            {
                if(pv.L3!=""&&pv.L3.Length>0)
                cb0.Properties.Items.Add(pv.L3);
            }
            cb0.Text = q3.ToString();
            cb1.Text = q4.ToString();
            cb2.Text = q5.ToString();
            cb3.Text = q6.ToString();
            cb4.Text = q7.ToString();
            cb5.Text = q9.ToString();
            lookupedit = q11;
            upedit.Properties.NullText = q11;
            groupBox1.Size = new Size(434, 40+ 33 * i);
            simpleButton1.Location = new Point(296, 60 + 33 * i);
            simpleButton2.Location = new Point(389, 60 + 33 * i);
            this.Size = new Size(490, 130 + 33 * i);

        }

        void tt6_TextChanged(object sender, EventArgs e)
        { try
            {
              tt5.Text = Convert.ToString(double.Parse(tt6.Text) * double.Parse(lookupedit));
          }
          catch { }
        }

        void upedit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lookupedit = upedit.Text;
                tt5.Text = Convert.ToString(double.Parse(tt6.Text) * double.Parse(lookupedit));
            }
            catch { }
        }

      
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < te.Length; i++)
            {
                SaveCellValue(te[i].Name.Replace("Text", ""), poweruid, te[i].Text.Trim());
            }

           
            
            if (isline)
            {
                try 
                {

                    LineInfo li=Common.Services.BaseService.GetOneByKey<LineInfo>(powerid);

                    if (li != null)
                    {
                        li.LineName = cb0.Text;
                        li.Voltage = cb1.Text;
                        li.Length = tt4.Text;
                        li.LineType =cb5.Text;
                        Common.Services.BaseService.Update<LineInfo>(li);
                    }

                
                }
                catch { }
            }
            if (isPower)
            {
                try
                {
                    substation sb = Common.Services.BaseService.GetOneByKey<substation>(powerid);
                    Substation_Info sub =  new Substation_Info();
                    sub.Code = powerid;
                    Substation_Info sbinfo = (Substation_Info)Common.Services.BaseService.GetObject("SelectSubstation_InfoByCode", sub);
                    if(sbinfo!=null)
                    {
                        sbinfo.L2 = double.Parse( cb3.Text);
                        sbinfo.L3 =int.Parse( cb2.Text);
                         Common.Services.BaseService.Update("UpdateSubstation_InfoByUID", sub);
                    }
                   
                    if (sb != null)
                    {
                        try
                        {
                            sb.EleName = cb0.Text;
                            sb.ObligateField1 = cb1.Text;
                        }
                        catch { }
                        
                        Common.Services.BaseService.Update<substation>(sb);
                    }
                }
                catch { }
            }



            try 
            {
                PSP_PowerProValues_LangFang ppts = new PSP_PowerProValues_LangFang();
                ppts.ID = poweruid;
                ppts.Flag2 = flag;


                PSP_PowerProValues_LangFang ppt1 = Common.Services.BaseService.GetOneByKey<PSP_PowerProValues_LangFang>(ppts);
                try
                {
                    ppt1.L3 = cb0.Text.ToString();
                    ppt1.L4 = cb1.Text.ToString();
                    ppt1.L5 = cb2.Text.ToString();
                    ppt1.L6 = cb3.Text.ToString();
                    ppt1.L7 = cb4.Text.ToString();
                    ppt1.L8 = tt4.Text.ToString();
                    ppt1.L9 = cb5.Text.ToString();
                    ppt1.L10 = double.Parse(tt5.Text.ToString());
                    ppt1.L11 =lookupedit;
                    ppt1.L12 = double.Parse(tt6.Text.ToString());
                }
                catch { }
                Common.Services.BaseService.Update<PSP_PowerProValues_LangFang>(ppt1);
            }
            catch { }

            this.DialogResult = DialogResult.OK;
        }



        private bool SaveCellValue(string year, string typeID, string value)
        {
            PowerProValues PowerValues = new PowerProValues();
            PowerValues.TypeID = typeID;
            PowerValues.Value = value;
            PowerValues.Year = year;
            PowerValues.TypeID1 = flag;

            try
            {
                Common.Services.BaseService.Update<PowerProValues>(PowerValues);
            }
            catch (Exception ex)
            {
                MsgBox.Show("保存数据出错：" + ex.Message);
                return false;
            }
            return true;
        }

      

    }
}