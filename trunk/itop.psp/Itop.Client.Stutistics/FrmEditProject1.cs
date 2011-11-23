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
    public partial class FrmEditProject1 : FormBase
    {
        public FrmEditProject1()
        {
            InitializeComponent();
        }

        private int isupdate = 0;
        private string flag = "";
        private string poweruid = "";
        private string powerid = "";
        private Hashtable hs = new Hashtable();
        private string flag2 = "";



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

        TextEdit ts1 = new TextEdit();
        TextEdit ts2 = new TextEdit();
        TextEdit ts3 = new TextEdit();




        private void FrmEditProject_Load(object sender, EventArgs e)
        {
            string q1 = "";
            PowerProTypes ppt = new PowerProTypes();
            ppt.ID = poweruid;
            ppt.Flag2 = flag;

            PowerProTypes ps = Common.Services.BaseService.GetOneByKey<PowerProTypes>(ppt);
            if (ps != null)
            {
                groupBox1.Text = ps.Title;
                powerid = ps.Code;
                q1 = ps.Remark;
            }


            LineInfo li22 = Common.Services.BaseService.GetOneByKey<LineInfo>(powerid);
            if (li22 != null)
            {
                isline = true;
            }

            substation sb = Common.Services.BaseService.GetOneByKey<substation>(powerid);
            if (sb != null)
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
            foreach (PowerProYears ppy in li)
            {
                lb[i] = new Label();
                lb[i].Name= "Label"+ppy.Year;
                lb[i].Text = ppy.Year+":";
                lb[i].Location = new Point(52, 27 + 33 * i);
                groupBox1.Controls.Add(lb[i]);

                te[i] = new TextEdit();
                te[i].Name = "Text" + ppy.Year;
                te[i].Location = new Point(157, 26 + 33 * i);
                te[i].Size = new Size(231, 21);
                groupBox1.Controls.Add(te[i]);

                foreach (PowerProValues ppy1 in listValues)
                {
                    if (ppy.Year == ppy1.Year)
                        te[i].Text = ppy1.Value;
                }

                i++;
            
            }

            //ls1.Text = "计划开始时间:";
            //ls1.Location = new Point(52, 27 + 33 * i);
            //groupBox1.Controls.Add(ls1);

            //ts1 = new TextEdit();
            //ts1.Location = new Point(157, 27 + 33 * i);
            //ts1.Size = new Size(231, 21);


            //ts1.Properties.DisplayFormat.FormatString = "n0";
            //ts1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //ts1.Properties.EditFormat.FormatString = "n0";
            //ts1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //ts1.Properties.Mask.EditMask = "####";
            //ts1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //groupBox1.Controls.Add(ts1);


            //ls2.Text = "预计投产时间:";
            //ls2.Location = new Point(52, 60 + 33 * i);
            //groupBox1.Controls.Add(ls2);

            //ts2 = new TextEdit();
            //ts2.Location = new Point(157, 60 + 33 * i);
            //ts2.Size = new Size(231, 21);

            //ts2.Properties.DisplayFormat.FormatString = "n0";
            //ts2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //ts2.Properties.EditFormat.FormatString = "n0";
            //ts2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //ts2.Properties.Mask.EditMask = "####";
            //ts2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //groupBox1.Controls.Add(ts2);




            ls3.Text = "备注:";
            ls3.Location = new Point(52, 27 + 33 * i);
            groupBox1.Controls.Add(ls3);

            ts3 = new TextEdit();
            ts3.Location = new Point(157, 27 + 33 * i);
            ts3.Size = new Size(231, 21);
            groupBox1.Controls.Add(ts3);

            ts3.Text = q1;


            if (isline)
            {
                la.Text = "长度:";
                la.Location = new Point(52, 61 + 33 * i);
                groupBox1.Controls.Add(la);

                ta = new TextEdit();
                ta.Location = new Point(157, 60 + 33 * i);
                ta.Size = new Size(231, 21);
                groupBox1.Controls.Add(ta);

                lb1.Text = "型号:";
                lb1.Location = new Point(52, 61 + 33 * (i + 1));
                groupBox1.Controls.Add(lb1);

                tb = new TextEdit();
                tb.Location = new Point(157, 60 + 33 * (i + 1));
                tb.Size = new Size(231, 21);
                groupBox1.Controls.Add(tb);

                try
                {
                    if (li22 != null)
                    {
                        ta.Text = li22.Length;
                        tb.Text = li22.LineType;
                    }
                }
                catch { }



            }
            if (isPower)
            {
                l11.Text = "容量:";
                l11.Location = new Point(52, 61 + 33 * i);
                groupBox1.Controls.Add(l11);

                t11 = new TextEdit();
                t11.Location = new Point(157, 60 + 33 * i);
                t11.Size = new Size(231, 21);
                groupBox1.Controls.Add(t11);


                l33.Text = "负荷率:";
                l33.Location = new Point(52, 61 + 33 * (i + 1));
                groupBox1.Controls.Add(l33);

                t33 = new TextEdit();
                t33.Location = new Point(157, 60 + 33 * (i + 1));
                t33.Size = new Size(231, 21);
                groupBox1.Controls.Add(t33);

                try
                {

                    if (sb != null)
                    {
                        t11.Text = sb.Burthen.ToString();
                        t33.Text = sb.ObligateField2;
                    }

                }
                catch { }
            }

            groupBox1.Size = new Size(434, 124 + 33 * i);
            simpleButton1.Location = new Point(296, 144 + 33 * i);
            simpleButton2.Location = new Point(389, 144 + 33 * i);
            this.Size = new Size(490, 224 + 33 * i);

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
                        li.Length = ta.Text;
                        li.LineType = tb.Text;// tb.Text;
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

                    if (sb != null)
                    {
                        try
                        {
                            sb.Burthen = decimal.Parse(t11.Text);
                        }
                        catch { }
                        sb.ObligateField2 = t33.Text;
                        Common.Services.BaseService.Update<substation>(sb);
                    }
                }
                catch { }
            }



            try 
            {
                PowerProTypes ppts = new PowerProTypes();
                ppts.ID = poweruid;
                ppts.Flag2 = flag;

                PowerProTypes ppt1 = Common.Services.BaseService.GetOneByKey<PowerProTypes>(ppts);
                try
                {
                    //ppt1.StartYear = int.Parse(ts1.Text);
                    //ppt1.EndYear = int.Parse(ts2.Text);
                    ppt1.Remark = ts3.Text;
                }
                catch { }
                Common.Services.BaseService.Update<PowerProTypes>(ppt1);




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