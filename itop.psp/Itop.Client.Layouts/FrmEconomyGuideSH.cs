using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Layouts;
using Itop.Domain.Stutistic;
using Itop.Client.Common;
using Itop.Domain.HistoryValue;
using Itop.Common;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using Itop.Domain.Graphics;
using System.Xml;
using Itop.Client.Chen;
using Itop.Domain.Table;
using Itop.Client.Base;
using Itop.Domain.Forecast;
using Itop.Client.Forecast;
namespace Itop.Client.Layouts
{
    public partial class FrmEconomyGuideSH : FormBase
    {
        public FrmEconomyGuideSH()
        {
            InitializeComponent();
        }
        private IList<EconomyData> li = new List<EconomyData>();
        private IList<EconomyData> lis = new List<EconomyData>();
        ArrayList al = new ArrayList();
        ArrayList al1 = new ArrayList();
        private string projectid = "";
        public string ProjectID
        {
            set { projectid = value; }
            get { return projectid; }
        }
        bool bs1 = false;

        private int s1 = 0;
        private int s2 = 0;
        private int s3 = 0;
        private int s4 = 0;
        private double s5 = 0;
        private double s6 = 0;
        private double s7 = 0;
        private double s8 = 0;
        private double s9 = 0;
        private double s10 = 0;
        private double s11 = 0;
        private double s12 = 0;
        private int s13 = 0;
        private double s14 = 0;
        private double s15 = 0;
        private double s16 = 0;
        private double s17 = 0;
        private double s18 = 0;
        private double s19 = 0;
        private double s20 = 0;
        private double s21 = 0;
        private double s22 = 0;
        private double s23 = 0;
        private double s24 = 0;
        private double s25 = 0;
        private double s26 = 0;





        private double s31 = 0;
        private double s32 = 0;
        private double s33 = 0;
        private double s34 = 0;
        private double s35 = 0;
        private double s36 = 0;
        private double s37 = 0;
        private double s38 = 0;
        private double s39 = 0;
        private double s40 = 0;


        bool isYearUpdate = false;
        bool isCompute = false;

        int indexss = -1;


        public int S1
        {
            set { s1 = value; }
            get { return s1; }
        }

        public int S2
        {
            set { s2 = value; }
            get { return s2; }
        }

        public int S3
        {
            set { s3 = value; }
            get { return s3; }
        }

        public int S4
        {
            set { s4 = value; }
            get { return s4; }
        }

        public double S5
        {
            set { s5 = value; }
            get { return s5; }
        }

        public double S6
        {
            set { s6 = value; }
            get { return s6; }
        }

        public double S7
        {
            set { s7 = value; }
            get { return s7; }
        }

        public double S8
        {
            set { s8 = value; }
            get { return s8; }
        }

        public double S9
        {
            set { s9 = value; }
            get { return s9; }
        }

        public double S10
        {
            set { s10 = value; }
            get { return s10; }
        }

        public double S11
        {
            set { s11 = value; }
            get { return s11; }
        }

        public double S12
        {
            set { s12 = value; }
            get { return s12; }
        }

        public int S13
        {
            set { s13 = value; }
            get { return s13; }
        }

        public double S14
        {
            set { s14 = value; }
            get { return s14; }
        }

        public double S15
        {
            set { s15 = value; }
            get { return s15; }
        }

        public double S16
        {
            set { s16 = value; }
            get { return s16; }
        }

        public double S17
        {
            set { s17 = value; }
            get { return s17; }
        }

        public double S18
        {
            set { s18 = value; }
            get { return s18; }
        }

        public double S19
        {
            set { s19 = value; }
            get { return s19; }
        }

        public double S20
        {
            set { s20 = value; }
            get { return s20; }
        }

        public double S21
        {
            set { s21 = value; }
            get { return s21; }
        }

        public double S22
        {
            set { s22 = value; }
            get { return s22; }
        }

        public double S23
        {
            set { s23 = value; }
            get { return s23; }
        }

        public double S24
        {
            set { s24 = value; }
            get { return s24; }
        }

        public double S25
        {
            set { s25 = value; }
            get { return s25; }
        }

        public double S26
        {
            set { s26 = value; }
            get { return s26; }
        }









        public double S31
        {
            set { s31 = value; }
            get { return s31; }
        }

        public double S32
        {
            set { s32 = value; }
            get { return s32; }
        }

        public double S33
        {
            set { s33 = value; }
            get { return s33; }
        }

        public double S34
        {
            set { s34 = value; }
            get { return s34; }
        }

        public double S35
        {
            set { s35 = value; }
            get { return s35; }
        }

        public double S36
        {
            set { s36 = value; }
            get { return s36; }
        }

        public double S37
        {
            set { s37 = value; }
            get { return s37; }
        }

        public double S38
        {
            set { s38 = value; }
            get { return s38; }
        }

        public double S39
        {
            set { s39 = value; }
            get { return s39; }
        }

        public double S40
        {
            set { s40 = value; }
            get { return s40; }
        }

        public IList<EconomyData> ObjectList
        {
            set { li = value; }
            get { return li; }
        }


        public IList<EconomyData> ObjectList1
        {
            set { lis = value; }
            get { return lis; }
        }












        private bool isGuide = true;

        public bool IsGuide
        {
            set { isGuide = value;}
            get { return isGuide; }
        }

        private void FrmEconomyGuide1_Load(object sender, EventArgs e)
        {
            InitSelectBox();
            t1.Text = s1.ToString();
            t2.Text = s2.ToString();
            t3.Text = s3.ToString();
            t4.Text = s4.ToString();
            t5.Text = s5.ToString();
            //t6.Text = s6.ToString();
            t7.Text = s7.ToString();
            t8.Text = s8.ToString();
            t9.Text = s9.ToString();
            t10.Text = s10.ToString();
            t11.Text = s11.ToString();
            t12.Text = s12.ToString();
            if(s13!=0)
            t13.Text = s13.ToString();
            t14.Text = s14.ToString();
            t15.Text = s15.ToString();
            t16.Text = s16.ToString();
            t17.Text = s17.ToString();
            t18.Text = s18.ToString();
            t19.Text = s19.ToString();
            t20.Text = s20.ToString();
            t21.Text = s21.ToString();
            t22.Text = s22.ToString();
            t23.Text = s23.ToString();
            t24.Text = s24.ToString();
            t25.Text = s25.ToString();



            textEdit3.Text = s31.ToString();
            textEdit4.Text = s32.ToString();
            textEdit5.Text = s33.ToString();
            textEdit6.Text = s34.ToString();
            textEdit7.Text = s35.ToString();
            textEdit8.Text = s36.ToString();
            textEdit9.Text = s37.ToString();
            textEdit10.Text = s38.ToString();
            textEdit11.Text = s39.ToString();
            textEdit12.Text = s40.ToString();

            simpleButton1.Visible = false;


            if (li.Count == 0)
                bs1 = true;







            bs1 = true;
            //gridControl2.DataSource = li;
            //gridControl2.RefreshDataSource();

            //textEdit1.Enabled = false;
            //textEdit2.Enabled = false;
            //simpleButton3.Enabled = false;






        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {//��������-��1
            if (this.Text == "��������-��3")
            {
                foreach (EconomyData es1 in lis)
                {
                    if (es1.S2 == 0)
                    {
                        MsgBox.Show("�����۵���Ϊ0����ݣ����������룡");
                        return;
                    }
                }


                if (li.Count == 0)
                {
                    MessageBox.Show("Ͷ�ʽ��ûѡ��");
                    return;
                }

                if (lis.Count == 0)
                {
                    MessageBox.Show("�۵���û��ѡ��");
                    return;
                }

                DialogResult = DialogResult.OK;
            }

            if (simpleButton2.Text == "��һ��" && simpleButton1.Visible)
            {
                foreach (EconomyData es in li)
                {
                    if (es.S2 == 0)
                    {
                        MsgBox.Show("����Ͷ�ʽ��Ϊ0����ݣ����������룡");
                        return;
                    }
                }





                groupBox1.Visible = false;
                groupBox3.Visible = false;
                groupBox2.Visible = true;
                this.Text = "��������-��3";
                simpleButton2.Text = "���";

                if (lis.Count == 0)
                {
                    lis.Clear();
                    for (int i = 0; i < s4; i++)
                    {
                        EconomyData ed = new EconomyData();
                        ed.S1 = s1 + i;
                        ed.S2 = 0;
                        lis.Add(ed);
                        gridControl1.DataSource = lis;
                        gridControl1.RefreshDataSource();
                    }
                }
                else
                {
                    gridControl1.DataSource = lis;
                    gridControl1.RefreshDataSource();
                }

                label30.Text = "�����еġ������Ԥ�⡱ģ����ѡ������";
            }

            if (simpleButton2.Text == "��һ��" && !simpleButton1.Visible)
            {
                


                if (t1.Text == "" || t1.Text == "0")
                {
                    MsgBox.Show("��ʼ�������δ���룡");
                    return;
                }

                int curyear = 0;
                int.TryParse(t1.Text.ToString(), out curyear);
                if (curyear < 1991 || curyear > 2060)
                {
                    MsgBox.Show("��ʼ���������������");
                    return;
                }


                if (t2.Text == "" || t2.Text == "0")
                {
                    MsgBox.Show("��������δ���룡");
                    return;
                }

                if (t3.Text == "" || t3.Text == "0")
                {
                    MsgBox.Show("������δ���룡");
                    return;
                }

                if (t4.Text == "" || t4.Text == "0")
                {
                    MsgBox.Show("��Ŀ������δ���룡");
                    return;
                }


                if (int.Parse(t4.Text) > 50)
                {
                    MsgBox.Show("��Ŀ������̫�󣬲�Ҫ����50��");
                    return;

                }

                if (int.Parse(t2.Text) + int.Parse(t3.Text) > int.Parse(t4.Text))
                {
                    MsgBox.Show("��Ŀ������Ӧ�ô��ڵ��ڻ����ںͽ�������֮��");
                    return;

                }



                if (t13.Text == "" || t13.Text == "0")
                {
                    MsgBox.Show("����ϵ��δ���룡");
                    return;
                }




                label28.Text = "";
                groupBox1.Visible = false;
                groupBox2.Visible = false;
                groupBox3.Visible = true;
                this.Text = "��������-��2";




                try
                {
                    if (s1 != int.Parse(t1.Text))
                        isCompute = true;
                }
                catch { }



                try
                {
                    
                    s1 = int.Parse(t1.Text);
                }
                catch { }


                try
                {
                    if (s2 != int.Parse(t2.Text))
                        isCompute = true;
                }
                catch { }

                try
                {
                    s2 = int.Parse(t2.Text);
                }
                catch { }

                try
                {
                    s3 = int.Parse(t3.Text);
                }
                catch { }

                try
                {
                    s4 = int.Parse(t4.Text);
                }
                catch { }

                try
                {
                    s5 = double.Parse(t5.Text);
                }
                catch { }

                //try
                //{
                //    s6 = double.Parse(t6.Text);
                //}
                //catch { }

                try
                {
                    s7 = double.Parse(t7.Text);
                }
                catch { }

                try
                {
                    s8 = double.Parse(t8.Text);
                }
                catch { }

                try
                {
                    s9 = double.Parse(t9.Text);
                }
                catch { }

                try
                {
                    s10 = double.Parse(t10.Text);
                }
                catch { }

                try
                {
                    s11 = double.Parse(t11.Text);
                }
                catch { }

                try
                {
                    s12 = double.Parse(t12.Text);
                }
                catch { }

                try
                {
                    s13 = int.Parse(t13.Text);
                }
                catch { }

                try
                {
                    s14 = double.Parse(t14.Text);
                }
                catch { }

                try
                {
                    s15 = double.Parse(t15.Text);
                }
                catch { }

                try
                {
                    s16 = double.Parse(t16.Text);
                }
                catch { }

                try
                {
                    s17 = double.Parse(t17.Text);
                }
                catch { }

                try
                {
                    s18 = double.Parse(t18.Text);
                }
                catch { }

                try
                {
                    s19 = double.Parse(t19.Text);
                }
                catch { }

                try
                {
                    s20 = double.Parse(t20.Text);
                }
                catch { }

                try
                {
                    s21 = double.Parse(t21.Text);
                }
                catch { }

                try
                {
                    s22 = double.Parse(t22.Text);
                }
                catch { }

                try
                {
                    s23 = double.Parse(t23.Text);
                }
                catch { }

                try
                {
                    s24 = double.Parse(t24.Text);
                }
                catch { }

                try
                {
                    s25 = double.Parse(t25.Text);
                }
                catch { }



















                try
                {
                    s31 = double.Parse(textEdit3.Text);
                }
                catch { }

                try
                {
                    s32 = double.Parse(textEdit4.Text);
                }
                catch { }

                try
                {
                    s33 = double.Parse(textEdit5.Text);
                }
                catch { }

                try
                {
                    s34 = double.Parse(textEdit6.Text);
                }
                catch { }

                try
                {
                    s35 = double.Parse(textEdit7.Text);
                }
                catch { }

                try
                {
                    s36 = double.Parse(textEdit8.Text);
                }
                catch { }

                try
                {
                    s37 = double.Parse(textEdit9.Text);
                }
                catch { }

                try
                {
                    s38 = double.Parse(textEdit10.Text);
                }
                catch { }
                try
                {
                    s39 = double.Parse(textEdit11.Text);
                }
                catch { }


                try
                {
                    s40 = double.Parse(textEdit12.Text);
                }
                catch { }

                
                simpleButton1.Visible = true;


                if (li.Count == 0)
                {
                    li.Clear();
                    for (int i = 0; i < s2; i++)
                    {
                        EconomyData ed = new EconomyData();
                        ed.S1 = s1 + i;
                        ed.S2 = GetNum(s1 + i);// 0;
                        ed.S3 = 0;
                        li.Add(ed);
                        gridControl2.DataSource = li;
                        gridControl2.RefreshDataSource();
                    }
                }
                else
                {
                    gridControl2.DataSource = li;
                    gridControl2.RefreshDataSource();
                }
         
            }




        }
        public void InitSelectBox()
        {
            //IList list = Common.Services.BaseService.GetList("SelectPs_Table_TZGSByConn", "ProjectID='"+ProjectID+"' and ParentID='0' and Col4='bian'");


            //int x=10;
            //for (int i = 0; i < list.Count; i++)
            //{
            //    Ps_Table_TZGS tzgs=list[i] as Ps_Table_TZGS;
            //    CheckBox box = new CheckBox();
            //    box.Text = tzgs.FromID + "kV";

            //    box.Tag = tzgs;
            //    box.Checked = true;
            //    box.Size = new Size(60, 18);
            //    groupControl1.Controls.Add(box);
            //    box.Location = new Point(x + 100 * i, 35);
            //}
            //if (list.Count == 0)
            //    simpleButton4.Enabled = false;

            IList list = Common.Services.BaseService.GetList("SelectPs_Table_TZGSByConn", "ProjectID='" + ProjectID + "' and ParentID='0' ");

            DataTable dt = Itop.Common.DataConverter.ToDataTable((IList)list, typeof(Ps_Table_TZGS));

            IList listrust = new List<Ps_Table_TZGS>();
            int dy=500;
            DataRow[] dtrow500 = dt.Select(" FromID='" + dy + "'");
            dy = 220;
            DataRow[] dtrow220 = dt.Select(" FromID='" + dy + "'");
            dy = 110;
            DataRow[] dtrow110 = dt.Select(" FromID='" + dy + "'");

            if (dtrow500.Length>0)
            {
                Ps_Table_TZGS tzgs500 = new Ps_Table_TZGS();
                 tzgs500.FromID = "500";
                 for (int j = 1990; j < 2061; j++)
                 {

                     foreach (DataRow row in dtrow500)
                     {
                         double tempdb = double.Parse(tzgs500.GetType().GetProperty("y" + j).GetValue(tzgs500, null).ToString());
                         double tempdb2=double.Parse(row["y"+j].ToString());
                         tzgs500.GetType().GetProperty("y" + j).SetValue(tzgs500, tempdb + tempdb2, null);
                     }

                 }
                 
                listrust.Add(tzgs500);
            }
           
            if (dtrow220.Length>0)
            {
                Ps_Table_TZGS tzgs220 = new Ps_Table_TZGS();
                tzgs220.FromID = "220";
                for (int j = 1990; j < 2061; j++)
                {

                    foreach (DataRow row in dtrow220)
                    {
                        double tempdb = double.Parse(tzgs220.GetType().GetProperty("y" + j).GetValue(tzgs220, null).ToString());
                        double tempdb2 = double.Parse(row["y" + j].ToString());
                        tzgs220.GetType().GetProperty("y" + j).SetValue(tzgs220, tempdb + tempdb2, null);
                    }

                }

                listrust.Add(tzgs220);
            }

            if (dtrow110.Length>0)
            {
                Ps_Table_TZGS tzgs110 = new Ps_Table_TZGS();
                tzgs110.FromID = "110";
                for (int j = 1990; j < 2061; j++)
                {

                    foreach (DataRow row in dtrow110)
                    {
                        double tempdb = double.Parse(tzgs110.GetType().GetProperty("y" + j).GetValue(tzgs110, null).ToString());
                        double tempdb2 = double.Parse(row["y" + j].ToString());
                        tzgs110.GetType().GetProperty("y" + j).SetValue(tzgs110, tempdb + tempdb2, null);
                    }

                }

                listrust.Add(tzgs110);
            }
            int x = 10;
            for (int i = 0; i < listrust.Count; i++)
            {
                Ps_Table_TZGS tzgs = listrust[i] as Ps_Table_TZGS;
                CheckBox box = new CheckBox();
                box.Text = tzgs.FromID + "kV";

                box.Tag = tzgs;
                box.Checked = true;
                box.Size = new Size(60, 18);
                groupControl1.Controls.Add(box);
                box.Location = new Point(x + 100 * i, 35);
            }
            if (list.Count == 0)
                simpleButton4.Enabled = false;
        }
        public double GetNum(int year)
        {
            double num = 0.0;
            foreach (Control ctrl in groupControl1.Controls)
            {
                if (ctrl is CheckBox)
                {
                    if ((ctrl as CheckBox).Checked == true)
                    {
                        Ps_Table_TZGS tzgs=(ctrl as CheckBox).Tag as Ps_Table_TZGS;
                        num += double.Parse(tzgs.GetType().GetProperty("y" + year.ToString()).GetValue(tzgs, null).ToString());
                    }
                }
            }
            return num;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (simpleButton2.Text == "��һ��" && simpleButton1.Visible)
            {


                if (li.Count == 0)
                {
                    li.Clear();
                    for (int i = 0; i < s2; i++)
                    {
                        EconomyData ed = new EconomyData();
                        ed.S1 = s1 + i;
                        ed.S2 = 0;
                        ed.S3 = 0;
                        li.Add(ed);
                        gridControl2.DataSource = li;
                        gridControl2.RefreshDataSource();
                    }
                }
                else
                {
                    gridControl2.DataSource = li;
                    gridControl2.RefreshDataSource();
                }





                label22.Visible = true;
                groupBox1.Visible = true;
                groupBox2.Visible = false;
                this.Text = "��������-��1";
                simpleButton2.Text = "��һ��";
                simpleButton1.Visible = false;
            }
            if (this.Text == "��������-��3")
            {
                label30.Text = "";
                groupBox1.Visible = false;
                groupBox3.Visible = true;
                groupBox2.Visible = false;
                simpleButton2.Text = "��һ��";
                this.Text = "��������-��2";
                //InitPicData();
            }


        }

        private void t1_EditValueChanged(object sender, EventArgs e)
        {
            isYearUpdate = true;
        }

        private void t2_EditValueChanged(object sender, EventArgs e)
        {
            isYearUpdate = true;
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxEdit1_SelectedValueChanged(object sender, EventArgs e)
        {
            

        }

        private void comboBoxEdit2_SelectedValueChanged(object sender, EventArgs e)
        {
          
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                label26.Visible = true;
                label27.Visible = true;
                textEdit1.Visible = true;
                textEdit2.Visible = true;
                simpleButton3.Visible = true;
                simpleButton4.Visible = false;
                simpleButton5.Visible = false;
                label30.Text = "���ݻ�׼���۵������۵����������۵�����";
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                label26.Visible = false;
                label27.Visible = false;
                textEdit1.Visible = false;
                textEdit2.Visible = false;
                simpleButton3.Visible = false;
                simpleButton4.Visible = true;
                simpleButton5.Visible = true;
                label30.Text = "�����еġ������Ԥ�⡱ģ����ѡ������";
            }

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

            if (textEdit2.Text == "")
                textEdit2.Text = "0";

            if (textEdit1.Text == "")
                textEdit1.Text = "0";

            lis.Clear();
            double v2 = 1;
            double c2 = 0;
            for (int i = 0; i < s4; i++)
            {
                EconomyData ed = new EconomyData();
                ed.S1 = s1 + i;
                ed.S2 = Math.Pow(double.Parse(textEdit2.Text), i)*double.Parse(textEdit1.Text);
                lis.Add(ed);
                gridControl1.DataSource = lis;
                gridControl1.RefreshDataSource();
            }
        }


        private InputLanguage oldInput = null;
        private void t1_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("��Ŀ��ʼ������ȡ����磺2006��");
        }

        private void t2_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("��Ŀ�ӿ�ʼ���赽�����Ҫ��ʱ�䡣���磺5��");
        }

        private void t3_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("����������Ҫ��ʱ�䡣���磺10��");
        }

        private void t4_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("��Ŀ�����ڡ����磺25��");
        }

        private void t5_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("���������ʡ����磺6.00%");
        }

        private void t7_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("����Ӫ�ɱ����롰�̶��ʲ�ԭֵ���ı��ʡ����磺5%");
        }

        private void t8_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("���۾ɶ�롰ԭ����ֵ���ı��ʡ����磺4.75%");
        }

        private void t9_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("����˰�ʡ����磺33.00%");
        }

        private void t10_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("�۾ɻ����ʡ����磺100.00%");
        }

        private void t11_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("�ɱ�����������磺5.70%");
        }

        private void t12_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("���д�����ʣ����ڼ��㾻�ֽ�������ֵ�����磺8%");
        }

        private void t13_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("����ȷ�ϳɱ����ñ����۾ɷ������ڼ��꿪ʼ��̬���١����磺4��");
        }

        private void t14_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("���̶��ʲ���ֵ���롰ԭ����ֵ���ı��ʡ����磺5.00%");
        }

        private void t15_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("�ǽ�����������˰ ���磺10.00%");
        }

        private void t16_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("�����𣬹�����ʡ����磺15.00%");
        }

        private void t17_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("����ά��������˰˰�ʡ����磺7.00%");
        }

        private void t18_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("����ά��������˰˰�ʡ����磺5.00%");
        }

        private void t19_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("��������ά��������˰˰�ʡ����磺1.00%");
        }

        private void t20_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("�����Ѹ��ӡ����磺3.00%");
        }

        private void t21_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("��̬Ͷ�ʷֶ���磺80.00%");
        }

        private void t22_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("�������ʽ��롰�̶��ʲ�ԭֵ���ı��ʡ����磺1.00%");
        }

        private void t23_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("���ʱ����롰�����ʽ��롱�ı��ʡ����磺20.00%");
        }

        private void t24_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("�������ʽ��롰�����ʽ��롱�ı��ʡ����磺30.00%");
        }



        private void L(string t)
        {
            label28.Text = t;
        }

        private void t25_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
            L("�������������֮�ȡ����磺50%");
        }

        private void textEdit1_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
        }

        private void textEdit2_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxEdit1_Validated(object sender, EventArgs e)
        {
 
        }

        private void comboBoxEdit1_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void comboBoxEdit1_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            
        }

        private void comboBoxEdit1_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            

        }

        



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            li.Clear();
            for (int i = 0; i < s2; i++)
            {
                EconomyData ed = new EconomyData();
                ed.S1 = s1 + i;
                ed.S2 = GetNum(s1 + i);// 0;
                ed.S3 = 0;
                li.Add(ed);
                gridControl2.DataSource = li;
                gridControl2.RefreshDataSource();
            }
        }
        //�����������
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            

            FormLoadForecastDataDSH frm = new FormLoadForecastDataDSH(2);
            frm.ProjectUID = MIS.ProgUID;
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            DataRow row = frm.ROW;
            lis.Clear();
            int zhuanhuan = 1;
            if (row["Title"].ToString().IndexOf("��") != -1)
            {
                zhuanhuan = 10000;
            }
            for (int i = 0; i < s4; i++)
            {
                EconomyData ed = new EconomyData();
                ed.S1 = s1 + i;
                double tempdoub = Convert.ToDouble(row["y" + (s1 + i)].ToString());
                tempdoub = tempdoub * zhuanhuan;
                ed.S2 = Math.Round(tempdoub, 2);
                lis.Add(ed);

            }
            gridControl1.DataSource = lis;
            gridControl1.RefreshDataSource();
            

        }

    }
}