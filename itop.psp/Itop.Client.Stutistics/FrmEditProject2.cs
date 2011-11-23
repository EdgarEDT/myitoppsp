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
    public partial class FrmEditProject2 : FormBase
    {
        public FrmEditProject2()
        {
            InitializeComponent();
        }

        private int isupdate = 0;
        private string flag = "";
        private string poweruid = "";
        private string powerid = "";
        private Hashtable hs = new Hashtable();
        private string flag2 = "";


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

        private void FrmEditProject_Load(object sender, EventArgs e)
        {
            double? q1 = null;
            double? q2 = null;
            double? q3 = null;
            string q4 = "";
            double? q5 = null;
            double? q6 = null;
            string q7 = "";
            string q8 = "";

            string c1 = "";
            string c2 = "";
            string c3 = "";

            int t1 = 0;

            PowerProTypes ppt = new PowerProTypes();
            ppt.ID = poweruid;
            ppt.Flag2 = flag;

            PowerProTypes ps = Common.Services.BaseService.GetOneByKey<PowerProTypes>(ppt);
            if (ps != null)
            {
                groupBox1.Text = ps.Title;
              //  powerid = ps.Code;
                c1 = ps.StartYear.ToString();
                c2 = ps.EndYear.ToString();
                c3 = ps.Remark;
                t1 = ps.Flag;
                q7 = ps.Title;
                q1 = ps.L1;
                q2 = ps.L2;
                q3 = ps.L3;
                q4 = ps.L4;
                q5 = ps.L5;
                q6 = ps.L6;
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

            lt3.Text = "城区名称:";
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
                tt4.Properties.Mask.EditMask = "#####0.####";
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
                tt1.Properties.Mask.EditMask = "#####0.####";
                tt1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

                groupBox1.Controls.Add(tt1);

                
                i++;

                lt5.Text = "投产年限:";
                lt5.Location = new Point(52, 27 + 33 * i);
                groupBox1.Controls.Add(lt5);

                tt5 = new TextEdit();
                tt5.Location = new Point(157, 26 + 33 * i);
                tt5.Size = new Size(231, 21);

                tt5.Properties.DisplayFormat.FormatString = "n4";
                tt5.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                tt5.Properties.EditFormat.FormatString = "n4";
                tt5.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                tt5.Properties.Mask.EditMask = "####";
                tt5.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;


                groupBox1.Controls.Add(tt5);
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
                tt6.Properties.Mask.EditMask = "#####0.####";
                tt6.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

                groupBox1.Controls.Add(tt6);

                i++;
            
            
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

                try
                {
                    tt1.Text = q3.ToString();
                    tt2.Text = q4.ToString();

                    
                }
                catch { }

                try
                {
                    tt3.Text = q7.ToString();
                    tt4.Text = q2.ToString();
                    tt5.Text = q5.ToString();
                    tt6.Text = q6.ToString();
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
            for (int i = 0; i < te.Length; i++)
            {
                SaveCellValue(te[i].Name.Replace("Text", ""), poweruid, te[i].Text.Trim());
            }
            try 
            {
                PowerProTypes ppts = new PowerProTypes();
                ppts.ID = poweruid;
                ppts.Flag2 = flag;


                PowerProTypes ppt1 = Common.Services.BaseService.GetOneByKey<PowerProTypes>(ppts);
                try
                {

                    ppt1.Remark = ts3.Text;

               
                    if(tt1.Text!="")
                        ppt1.L3 = double.Parse(tt1.Text);
                    if (tt2.Text != "")
                        ppt1.L4 = tt2.Text;
                    if (tt3.Text != "")
                        ppt1.Title = tt3.Text;
                    if (tt4.Text != "")
                        ppt1.L2 = double.Parse(tt4.Text);
                    if (tt5.Text != "")
                        ppt1.L5 = double.Parse(tt5.Text);
                    if (tt6.Text != "")
                        ppt1.L6 = double.Parse(tt6.Text);

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

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}