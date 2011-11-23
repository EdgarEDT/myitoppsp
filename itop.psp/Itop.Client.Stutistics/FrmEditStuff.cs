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
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
    public partial class FrmEditStuff : FormBase
    {
        public FrmEditStuff()
        {
            InitializeComponent();
        }

        private int isupdate = 0;
        private string flag = "";
        private string poweruid = "";
        private string powerid = "";



        ///// <summary>
        ///// 规划ID
        ///// </summary>
        //public int FlagId
        //{
        //    set { flag = value; }
        //    get { return flag; }
        //}

        /// <summary>
        /// 变电站，线路ID
        /// </summary>
        public string PowerId
        {
            set { powerid = value; }
            get { return powerid; }
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
        TextEdit tb1 = new TextEdit();

        Label lc = new Label();
        TextEdit tc = new TextEdit();

        Label l11 = new Label();
        TextEdit t11 = new TextEdit();

        Label l22 = new Label();
        TextEdit t22 = new TextEdit();

        Label l33 = new Label();
        TextEdit t33 = new TextEdit();

        private void FrmEditProject_Load(object sender, EventArgs e)
        {


            PowerProTypes ps = (PowerProTypes)Common.Services.BaseService.GetObject("SelectPowerProTypesByCode", powerid);
            if (ps != null)
            {
                groupBox1.Text = ps.Title;
                poweruid = ps.ID;
            }


            PowerProValues ppv = new PowerProValues();
            ppv.TypeID = poweruid;
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

            if (isline)
            {
                la.Text = "长度";
                la.Location = new Point(52, 27 + 33 * i);
                groupBox1.Controls.Add(la);

                TextEdit ta = new TextEdit();
                ta.Location = new Point(157, 26 + 33 * i);
                ta.Size = new Size(231, 21);
                groupBox1.Controls.Add(ta);

                lb1.Text = "类型";
                lb1.Location = new Point(52, 27 + 33 * (i + 1));
                groupBox1.Controls.Add(lb1);

                TextEdit tb = new TextEdit();
                tb.Location = new Point(157, 26 + 33 * (i + 1));
                tb.Size = new Size(231, 21);
                groupBox1.Controls.Add(tb);

                lc.Text = "电压";
                lc.Location = new Point(52, 27 + 33 * (i + 2));
                groupBox1.Controls.Add(lc);

                TextEdit tc = new TextEdit();
                tc.Location = new Point(157, 26 + 33 * (i + 2));
                tc.Size = new Size(231, 21);
                groupBox1.Controls.Add(tc);
            }
            if(isPower)
            {
                l11.Text = "容量";
                l11.Location = new Point(52, 27 + 33 * i);
                groupBox1.Controls.Add(l11);

                TextEdit t11 = new TextEdit();
                t11.Location = new Point(157, 26 + 33 * i);
                t11.Size = new Size(231, 21);
                groupBox1.Controls.Add(t11);

                l22.Text = "电压等级";
                l22.Location = new Point(52, 27 + 33 * (i + 1));
                groupBox1.Controls.Add(l22);

                TextEdit t22 = new TextEdit();
                t22.Location = new Point(157, 26 + 33 * (i + 1));
                t22.Size = new Size(231, 21);
                groupBox1.Controls.Add(t22);


                l33.Text = "负荷率";
                l33.Location = new Point(52, 27 + 33 * (i + 2));
                groupBox1.Controls.Add(l33);

                TextEdit t33 = new TextEdit();
                t33.Location = new Point(157, 26 + 33 * (i + 2));
                t33.Size = new Size(231, 21);
                groupBox1.Controls.Add(t33);
            }
            





        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < te.Length; i++)
            {
                try
                {
                    SaveCellValue(te[i].Name.Replace("Text", ""), poweruid, te[i].Text.Trim());
                }
                catch { }
            }

            if (isline)
            {
                try 
                {

                    LineInfo li=Common.Services.BaseService.GetOneByKey<LineInfo>(powerid);

                    if (li == null)
                    {
                        li.UID = powerid;
                        li.Length = ta.Text;
                        li.LineType = tb1.Text;
                        li.Voltage = tc.Text;
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

                    if (sb == null)
                    {
                        sb.UID = powerid;
                        try
                        {
                            sb.Burthen = decimal.Parse(t11.Text);
                        }
                        catch { }
                        sb.ObligateField1 = t22.Text;
                        sb.ObligateField1 = t33.Text;
                        Common.Services.BaseService.Update<substation>(sb);
                    }
                }
                catch { }
            }

        }



        private bool SaveCellValue(string year, string typeID, string value)
        {
            PowerProValues PowerValues = new PowerProValues();
            PowerValues.TypeID = typeID;
            PowerValues.Value = value;
            PowerValues.Year = year;

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