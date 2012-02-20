using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmLineList2 : FormBase
    {
        public ArrayList list = new ArrayList();
        PSP_LineData data = new PSP_LineData();
        public frmLineList2()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox82_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmLineList2_Load(object sender, EventArgs e)
        {
            data= Services.BaseService.GetOneByKey<PSP_LineData>("a");
            q1.Text = data.q1;
            q2.Text = data.q2;
            q3.Text = data.q3;
            q4.Text = data.q4;
            q5.Text = data.q5;
            q6.Text = data.q6;
            q7.Text = data.q7;
            q8.Text = data.q8;
            q9.Text = data.q9;
            q10.Text = data.q10;
            q11.Text = data.q11;
            q12.Text = data.q12;
            q13.Text = data.q13;
            q14.Text = data.q14;
            q15.Text = data.q15;
            q16.Text = data.q16;
            q17.Text = data.q17;
            q18.Text = data.q18;
            q19.Text = data.q19;
            q20.Text = data.q20;
            q21.Text = data.q21;
            q22.Text = data.q22;
            q23.Text = data.q23;
            q24.Text = data.q24;
            q25.Text = data.q25;
            for (int i = 0; i < list.Count;i++ )
            {
               LineList1 line= Services.BaseService.GetOneByKey<LineList1>(list[i].ToString());
               this.Controls["lb" + (i + 1)].Text = line.LineName;
               this.Controls["groupBox" + (i + 2)].Tag = line.UID;
               this.Controls["groupBox"+(i+2)].Enabled = true;
            }
            if (groupBox2.Enabled == true)
            {
                
                PSP_LineData d1 = Services.BaseService.GetOneByKey<PSP_LineData>(groupBox2.Tag.ToString());
                //d1.UID = groupBox2.Tag.ToString();
                if (d1 != null)
                {
                    comboBox1.Text = d1.q1;
                    comboBox2.Text = d1.q2;
                    comboBox3.Text = d1.q3;
                    comboBox4.Text = d1.q4;
                    comboBox5.Text = d1.q5;
                    comboBox6.Text = d1.q6;
                    comboBox7.Text = d1.q7;
                    comboBox8.Text = d1.q8;
                    comboBox9.Text = d1.q9;
                    comboBox10.Text = d1.q10;
                    comboBox11.Text = d1.q11;
                    comboBox12.Text = d1.q12;
                    comboBox13.Text = d1.q13;
                    comboBox14.Text = d1.q14;
                    comboBox15.Text = d1.q15;
                    t1.Text = d1.q16;
                    t2.Text = d1.q17;
                    t3.Text = d1.q18;
                    t4.Text = d1.q19;
                    comboBox16.Text = d1.q20;
                    comboBox17.Text = d1.q21;
                    comboBox18.Text = d1.q22;
                    comboBox19.Text = d1.q23;
                    comboBox20.Text = d1.q24;
                    comboBox21.Text = d1.q25;
                    textBox1.Text = d1.col1;
                }
            }
            if (groupBox3.Enabled == true)
            {
            
                PSP_LineData d1 = Services.BaseService.GetOneByKey<PSP_LineData>(groupBox3.Tag.ToString());
                //d1.UID = groupBox3.Tag.ToString();
                if (d1 != null)
                {
                    b1c1.Text = d1.q1;
                    b1c2.Text = d1.q2;
                    b1c3.Text = d1.q3;
                    b1c4.Text = d1.q4;
                    b1c5.Text = d1.q5;
                    b1c6.Text = d1.q6;
                    b1c7.Text = d1.q7;
                    b1c8.Text = d1.q8;
                    b1c9.Text = d1.q9;
                    b1c10.Text = d1.q10;
                    b1c11.Text = d1.q11;
                    b1c12.Text = d1.q12;
                    b1c13.Text = d1.q13;
                    b1c14.Text = d1.q14;
                    b1c15.Text = d1.q15;
                    t5.Text = d1.q16;
                    t6.Text = d1.q17;
                    t7.Text = d1.q18;
                    t8.Text = d1.q19;
                    b1c16.Text = d1.q20;
                    b1c17.Text = d1.q21;
                    b1c18.Text = d1.q22;
                    b1c19.Text = d1.q23;
                    b1c20.Text = d1.q24;
                    b1c21.Text = d1.q25;
                    textBox2.Text = d1.col1;
                }
            }
            if (groupBox4.Enabled == true)
            {

                PSP_LineData d1 = Services.BaseService.GetOneByKey<PSP_LineData>(groupBox4.Tag.ToString());
                //d1.UID = groupBox4.Tag.ToString();
                if (d1 != null)
                {
                    b2c1.Text = d1.q1;
                    b2c2.Text = d1.q2;
                    b2c3.Text = d1.q3;
                    b2c4.Text = d1.q4;
                    b2c5.Text = d1.q5;
                    b2c6.Text = d1.q6;
                    b2c7.Text = d1.q7;
                    b2c8.Text = d1.q8;
                    b2c9.Text = d1.q9;
                    b2c10.Text = d1.q10;
                    b2c11.Text = d1.q11;
                    b2c12.Text = d1.q12;
                    b2c13.Text = d1.q13;
                    b2c14.Text = d1.q14;
                    b2c15.Text = d1.q15;
                    t9.Text = d1.q16;
                    t10.Text = d1.q17;
                    t11.Text = d1.q18;
                    t12.Text = d1.q19;
                    b2c16.Text = d1.q20;
                    b2c17.Text = d1.q21;
                    b2c18.Text = d1.q22;
                    b2c19.Text = d1.q23;
                    b2c20.Text = d1.q24;
                    b2c21.Text = d1.q25;
                    textBox3.Text = d1.col1;
                }
         
            }
            if (groupBox5.Enabled == true)
            {
               
                PSP_LineData d1 = Services.BaseService.GetOneByKey<PSP_LineData>(groupBox5.Tag.ToString());
                //d1.UID = groupBox5.Tag.ToString();
                if (d1 != null)
                {
                    b3c1.Text = d1.q1;
                    b3c2.Text = d1.q2;
                    b3c3.Text = d1.q3;
                    b3c4.Text = d1.q4;
                    b3c5.Text = d1.q5;
                    b3c6.Text = d1.q6;
                    b3c7.Text = d1.q7;
                    b3c8.Text = d1.q8;
                    b3c9.Text = d1.q9;
                    b3c10.Text = d1.q10;
                    b3c11.Text = d1.q11;
                    b3c12.Text = d1.q12;
                    b3c13.Text = d1.q13;
                    b3c14.Text = d1.q14;
                    b3c15.Text = d1.q15;
                    t13.Text = d1.q16;
                    t14.Text = d1.q17;
                    t15.Text = d1.q18;
                    t16.Text = d1.q19;
                    b3c16.Text = d1.q20;
                    b3c17.Text = d1.q21;
                    b3c18.Text = d1.q22;
                    b3c19.Text = d1.q23;
                    b3c20.Text = d1.q24;
                    b3c21.Text = d1.q25;
                    textBox4.Text = d1.col1;
                }
            }
            if (groupBox6.Enabled == true)
            {
               
                PSP_LineData d1 = Services.BaseService.GetOneByKey<PSP_LineData>(groupBox6.Tag.ToString());
               // d1.UID = groupBox6.Tag.ToString();
                if (d1 != null)
                {
                    b4c1.Text = d1.q1;
                    b4c2.Text = d1.q2;
                    b4c3.Text = d1.q3;
                    b4c4.Text = d1.q4;
                    b4c5.Text = d1.q5;
                    b4c6.Text = d1.q6;
                    b4c7.Text = d1.q7;
                    b4c8.Text = d1.q8;
                    b4c9.Text = d1.q9;
                    b4c10.Text = d1.q10;
                    b4c11.Text = d1.q11;
                    b4c12.Text = d1.q12;
                    b4c13.Text = d1.q13;
                    b4c14.Text = d1.q14;
                    b4c15.Text = d1.q15;
                    t17.Text = d1.q16;
                    t18.Text = d1.q17;
                    t19.Text = d1.q18;
                    t20.Text = d1.q19;
                    b4c16.Text = d1.q20;
                    b4c17.Text = d1.q21;
                    b4c18.Text = d1.q22;
                    b4c19.Text = d1.q23;
                    b4c20.Text = d1.q24;
                    b4c21.Text =d1.q25;
                    textBox5.Text = d1.col1;
                }
             }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllNumber();
         
        }
        public void GetAllNumber()
        {
            if (groupBox2.Enabled == true)
            {
                textBox1.Text = Convert.ToString(getNumber(comboBox1.Text) * Convert.ToDouble(q1.Text) +
                 getNumber(comboBox2.Text) * Convert.ToDouble(q2.Text) +
                 getNumber(comboBox3.Text) * Convert.ToDouble(q3.Text) +
                 getNumber(comboBox4.Text) * Convert.ToDouble(q4.Text) +
                 getNumber(comboBox5.Text) * Convert.ToDouble(q5.Text) +
                 getNumber(comboBox6.Text) * Convert.ToDouble(q6.Text) +
                 getNumber(comboBox7.Text) * Convert.ToDouble(q7.Text) +
                 getNumber(comboBox8.Text) * Convert.ToDouble(q8.Text) +
                 getNumber(comboBox9.Text) * Convert.ToDouble(q9.Text) +
                 getNumber(comboBox10.Text) * Convert.ToDouble(q10.Text) +
                 getNumber(comboBox11.Text) * Convert.ToDouble(q11.Text) +
                 getNumber(comboBox12.Text) * Convert.ToDouble(q12.Text) +
                 getNumber(comboBox13.Text) * Convert.ToDouble(q13.Text) +
                 getNumber(comboBox14.Text) * Convert.ToDouble(q14.Text) +
                 getNumber(comboBox15.Text) * Convert.ToDouble(q15.Text) +
                 getNumber(comboBox16.Text) * Convert.ToDouble(q20.Text) +
                 getNumber(comboBox17.Text) * Convert.ToDouble(q21.Text) +
                 getNumber(comboBox18.Text) * Convert.ToDouble(q22.Text) +
                 getNumber(comboBox19.Text) * Convert.ToDouble(q23.Text) +
                 getNumber(comboBox20.Text) * Convert.ToDouble(q24.Text) +
                 getNumber(comboBox21.Text) * Convert.ToDouble(q25.Text) +
                 Convert.ToDouble(t1.Text) * Convert.ToDouble(q16.Text) +
                 Convert.ToDouble(t2.Text) * Convert.ToDouble(q17.Text) +
                 Convert.ToDouble(t3.Text) * Convert.ToDouble(q18.Text) +
                 Convert.ToDouble(t4.Text) * Convert.ToDouble(q19.Text));
            }
            if (groupBox3.Enabled == true)
            {
                textBox2.Text = Convert.ToString(getNumber(b1c1.Text) * Convert.ToDouble(q1.Text) +
                getNumber(b1c2.Text) * Convert.ToDouble(q2.Text) +
                getNumber(b1c3.Text) * Convert.ToDouble(q3.Text) +
                getNumber(b1c4.Text) * Convert.ToDouble(q4.Text) +
                getNumber(b1c5.Text) * Convert.ToDouble(q5.Text) +
                getNumber(b1c6.Text) * Convert.ToDouble(q6.Text) +
                getNumber(b1c7.Text) * Convert.ToDouble(q7.Text) +
                getNumber(b1c8.Text) * Convert.ToDouble(q8.Text) +
                getNumber(b1c9.Text) * Convert.ToDouble(q9.Text) +
                getNumber(b1c10.Text) * Convert.ToDouble(q10.Text) +
                getNumber(b1c11.Text) * Convert.ToDouble(q11.Text) +
                getNumber(b1c12.Text) * Convert.ToDouble(q12.Text) +
                getNumber(b1c13.Text) * Convert.ToDouble(q13.Text) +
                getNumber(b1c14.Text) * Convert.ToDouble(q14.Text) +
                getNumber(b1c15.Text) * Convert.ToDouble(q15.Text) +
                getNumber(b1c16.Text) * Convert.ToDouble(q20.Text) +
                getNumber(b1c17.Text) * Convert.ToDouble(q21.Text) +
                getNumber(b1c18.Text) * Convert.ToDouble(q22.Text) +
                getNumber(b1c19.Text) * Convert.ToDouble(q23.Text) +
                getNumber(b1c20.Text) * Convert.ToDouble(q24.Text) +
                getNumber(b1c21.Text) * Convert.ToDouble(q25.Text) +
                Convert.ToDouble(t5.Text) * Convert.ToDouble(q16.Text) +
                Convert.ToDouble(t6.Text) * Convert.ToDouble(q17.Text) +
                Convert.ToDouble(t7.Text) * Convert.ToDouble(q18.Text) +
                Convert.ToDouble(t8.Text) * Convert.ToDouble(q19.Text));
            }
            if (groupBox4.Enabled == true)
            {
                textBox3.Text = Convert.ToString(getNumber(b2c1.Text) * Convert.ToDouble(q1.Text) +
               getNumber(b2c2.Text) * Convert.ToDouble(q2.Text) +
               getNumber(b2c3.Text) * Convert.ToDouble(q3.Text) +
               getNumber(b2c4.Text) * Convert.ToDouble(q4.Text) +
               getNumber(b2c5.Text) * Convert.ToDouble(q5.Text) +
               getNumber(b2c6.Text) * Convert.ToDouble(q6.Text) +
               getNumber(b2c7.Text) * Convert.ToDouble(q7.Text) +
               getNumber(b2c8.Text) * Convert.ToDouble(q8.Text) +
               getNumber(b2c9.Text) * Convert.ToDouble(q9.Text) +
               getNumber(b2c10.Text) * Convert.ToDouble(q10.Text) +
               getNumber(b2c11.Text) * Convert.ToDouble(q11.Text) +
               getNumber(b2c12.Text) * Convert.ToDouble(q12.Text) +
               getNumber(b2c13.Text) * Convert.ToDouble(q13.Text) +
               getNumber(b2c14.Text) * Convert.ToDouble(q14.Text) +
               getNumber(b2c15.Text) * Convert.ToDouble(q15.Text) +
               getNumber(b2c16.Text) * Convert.ToDouble(q20.Text) +
               getNumber(b2c17.Text) * Convert.ToDouble(q21.Text) +
               getNumber(b2c18.Text) * Convert.ToDouble(q22.Text) +
               getNumber(b2c19.Text) * Convert.ToDouble(q23.Text) +
               getNumber(b2c20.Text) * Convert.ToDouble(q24.Text) +
               getNumber(b2c21.Text) * Convert.ToDouble(q25.Text) +
               Convert.ToDouble(t9.Text) * Convert.ToDouble(q16.Text) +
               Convert.ToDouble(t10.Text) * Convert.ToDouble(q17.Text) +
               Convert.ToDouble(t11.Text) * Convert.ToDouble(q18.Text) +
               Convert.ToDouble(t12.Text) * Convert.ToDouble(q19.Text));
            }

            if (groupBox5.Enabled == true)
            {
                textBox4.Text = Convert.ToString(getNumber(b3c1.Text) * Convert.ToDouble(q1.Text) +
              getNumber(b3c2.Text) * Convert.ToDouble(q2.Text) +
              getNumber(b3c3.Text) * Convert.ToDouble(q3.Text) +
              getNumber(b3c4.Text) * Convert.ToDouble(q4.Text) +
              getNumber(b3c5.Text) * Convert.ToDouble(q5.Text) +
              getNumber(b3c6.Text) * Convert.ToDouble(q6.Text) +
              getNumber(b3c7.Text) * Convert.ToDouble(q7.Text) +
              getNumber(b3c8.Text) * Convert.ToDouble(q8.Text) +
              getNumber(b3c9.Text) * Convert.ToDouble(q9.Text) +
              getNumber(b3c10.Text) * Convert.ToDouble(q10.Text) +
              getNumber(b3c11.Text) * Convert.ToDouble(q11.Text) +
              getNumber(b3c12.Text) * Convert.ToDouble(q12.Text) +
              getNumber(b3c13.Text) * Convert.ToDouble(q13.Text) +
              getNumber(b3c14.Text) * Convert.ToDouble(q14.Text) +
              getNumber(b3c15.Text) * Convert.ToDouble(q15.Text) +
              getNumber(b3c16.Text) * Convert.ToDouble(q20.Text) +
              getNumber(b3c17.Text) * Convert.ToDouble(q21.Text) +
              getNumber(b3c18.Text) * Convert.ToDouble(q22.Text) +
              getNumber(b3c19.Text) * Convert.ToDouble(q23.Text) +
              getNumber(b3c20.Text) * Convert.ToDouble(q24.Text) +
              getNumber(b3c21.Text) * Convert.ToDouble(q25.Text) +
              Convert.ToDouble(t13.Text) * Convert.ToDouble(q16.Text) +
              Convert.ToDouble(t14.Text) * Convert.ToDouble(q17.Text) +
              Convert.ToDouble(t15.Text) * Convert.ToDouble(q18.Text) +
              Convert.ToDouble(t16.Text) * Convert.ToDouble(q19.Text));
            }

            if (groupBox6.Enabled == true)
            {
                textBox5.Text = Convert.ToString(getNumber(b4c1.Text) * Convert.ToDouble(q1.Text) +
              getNumber(b4c2.Text) * Convert.ToDouble(q2.Text) +
              getNumber(b4c3.Text) * Convert.ToDouble(q3.Text) +
              getNumber(b4c4.Text) * Convert.ToDouble(q4.Text) +
              getNumber(b4c5.Text) * Convert.ToDouble(q5.Text) +
              getNumber(b4c6.Text) * Convert.ToDouble(q6.Text) +
              getNumber(b4c7.Text) * Convert.ToDouble(q7.Text) +
              getNumber(b4c8.Text) * Convert.ToDouble(q8.Text) +
              getNumber(b4c9.Text) * Convert.ToDouble(q9.Text) +
              getNumber(b4c10.Text) * Convert.ToDouble(q10.Text) +
              getNumber(b4c11.Text) * Convert.ToDouble(q11.Text) +
              getNumber(b4c12.Text) * Convert.ToDouble(q12.Text) +
              getNumber(b4c13.Text) * Convert.ToDouble(q13.Text) +
              getNumber(b4c14.Text) * Convert.ToDouble(q14.Text) +
              getNumber(b4c15.Text) * Convert.ToDouble(q15.Text) +
              getNumber(b4c16.Text) * Convert.ToDouble(q20.Text) +
              getNumber(b4c17.Text) * Convert.ToDouble(q21.Text) +
              getNumber(b4c18.Text) * Convert.ToDouble(q22.Text) +
              getNumber(b4c19.Text) * Convert.ToDouble(q23.Text) +
              getNumber(b4c20.Text) * Convert.ToDouble(q24.Text) +
              getNumber(b4c21.Text) * Convert.ToDouble(q25.Text) +
              Convert.ToDouble(t17.Text) * Convert.ToDouble(q16.Text) +
              Convert.ToDouble(t18.Text) * Convert.ToDouble(q17.Text) +
              Convert.ToDouble(t19.Text) * Convert.ToDouble(q18.Text) +
              Convert.ToDouble(t20.Text) * Convert.ToDouble(q19.Text));
            }
        }
        public int getNumber(string str)
        {
            int i = 0;
            if (str == "很好")
            {
                i = 9;
            }
            if (str == "好")
            {
                i = 7;
            }
            if (str == "一般")
            {
                i = 5;
            }
            if (str == "差")
            {
                i = 3;
            }
            if (str == "很差")
            {
                i = 1;
            }
            if (str == "极差")
            {
                i = 0;
            }
            return i;
        }
        public void CKRight()
        {
           
        }
        private void button3_Click(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            decimal i = Convert.ToDecimal(q1.Text) +
          Convert.ToDecimal(q2.Text) +
          Convert.ToDecimal(q3.Text) +
          Convert.ToDecimal(q4.Text) +
          Convert.ToDecimal(q5.Text) +
          Convert.ToDecimal(q6.Text) +
          Convert.ToDecimal(q7.Text) +
          Convert.ToDecimal(q8.Text) +
          Convert.ToDecimal(q9.Text) +
          Convert.ToDecimal(q10.Text) +
          Convert.ToDecimal(q11.Text) +
          Convert.ToDecimal(q12.Text) +
          Convert.ToDecimal(q13.Text) +
          Convert.ToDecimal(q14.Text) +
          Convert.ToDecimal(q15.Text) +
          Convert.ToDecimal(q16.Text) +
          Convert.ToDecimal(q17.Text) +
          Convert.ToDecimal(q18.Text) +
          Convert.ToDecimal(q19.Text) +
          Convert.ToDecimal(q20.Text) +
          Convert.ToDecimal(q21.Text) +
          Convert.ToDecimal(q22.Text) +
          Convert.ToDecimal(q23.Text) +
          Convert.ToDecimal(q24.Text) +
          Convert.ToDecimal(q25.Text);
            if (i > 1 || i < 1)
            {
                MessageBox.Show("权重系数和必须为1，请调整权重系数。\r\n 当前权重和为" + i, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            GetAllNumber();
            data.q1 = q1.Text;
            data.q2 = q2.Text;
            data.q3 = q3.Text;
            data.q4 = q4.Text;
            data.q5 = q5.Text;
            data.q6 = q6.Text;
            data.q7 = q7.Text;
            data.q8 = q8.Text;
            data.q9 = q9.Text;
            data.q10 = q10.Text;
            data.q11 = q11.Text;
            data.q12 = q12.Text;
            data.q13 = q13.Text;
            data.q14 = q14.Text;
            data.q15 = q15.Text;
            data.q16 = q16.Text;
            data.q17 = q17.Text;
            data.q18 = q18.Text;
            data.q19 = q19.Text;
            data.q20 = q20.Text;
            data.q21 = q21.Text;
            data.q22 = q22.Text;
            data.q23 = q23.Text;
            data.q24 = q24.Text;
            data.q25 = q25.Text;
            Services.BaseService.Update<PSP_LineData>(data);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (groupBox2.Enabled == true)
            {
                PSP_LineData d1 = new PSP_LineData();
                PSP_LineData temp = Services.BaseService.GetOneByKey<PSP_LineData>(groupBox2.Tag.ToString());
                d1.UID = groupBox2.Tag.ToString();
                d1.q1 = comboBox1.Text;
                d1.q2 = comboBox2.Text;
                d1.q3 = comboBox3.Text;
                d1.q4 = comboBox4.Text;
                d1.q5 = comboBox5.Text;
                d1.q6 = comboBox6.Text;
                d1.q7 = comboBox7.Text;
                d1.q8 = comboBox8.Text;
                d1.q9 = comboBox9.Text;
                d1.q10 = comboBox10.Text;
                d1.q11 = comboBox11.Text;
                d1.q12 = comboBox12.Text;
                d1.q13 = comboBox13.Text;
                d1.q14 = comboBox14.Text;
                d1.q15 = comboBox15.Text;
                d1.q16 = t1.Text;
                d1.q17 = t2.Text;
                d1.q18 = t3.Text;
                d1.q19 = t4.Text;
                d1.q20 = comboBox16.Text;
                d1.q21 = comboBox17.Text;
                d1.q22 = comboBox18.Text;
                d1.q23 = comboBox19.Text;
                d1.q24 = comboBox20.Text;
                d1.q25 = comboBox21.Text;
                d1.col1 = textBox1.Text;
                if (temp == null)
                {
                    Services.BaseService.Create<PSP_LineData>(d1);
                }
                else
                {
                    Services.BaseService.Update<PSP_LineData>(d1);
                }
            }
            if (groupBox3.Enabled == true)
            {
                PSP_LineData d1 = new PSP_LineData();
                PSP_LineData temp = Services.BaseService.GetOneByKey<PSP_LineData>(groupBox3.Tag.ToString());
                d1.UID = groupBox3.Tag.ToString();
                d1.q1 = b1c1.Text;
                d1.q2 = b1c2.Text;
                d1.q3 = b1c3.Text;
                d1.q4 = b1c4.Text;
                d1.q5 = b1c5.Text;
                d1.q6 = b1c6.Text;
                d1.q7 = b1c7.Text;
                d1.q8 = b1c8.Text;
                d1.q9 = b1c9.Text;
                d1.q10 = b1c10.Text;
                d1.q11 = b1c11.Text;
                d1.q12 = b1c12.Text;
                d1.q13 = b1c13.Text;
                d1.q14 = b1c14.Text;
                d1.q15 = b1c15.Text;
                d1.q16 = t5.Text;
                d1.q17 = t6.Text;
                d1.q18 = t7.Text;
                d1.q19 = t8.Text;
                d1.q20 = b1c16.Text;
                d1.q21 = b1c17.Text;
                d1.q22 = b1c18.Text;
                d1.q23 = b1c19.Text;
                d1.q24 = b1c20.Text;
                d1.q25 = b1c21.Text;
                d1.col1 = textBox2.Text;
                if (temp == null)
                {
                    Services.BaseService.Create<PSP_LineData>(d1);
                }
                else
                {
                    Services.BaseService.Update<PSP_LineData>(d1);
                }
            }
            if (groupBox4.Enabled == true)
            {
                PSP_LineData d1 = new PSP_LineData();
                PSP_LineData temp = Services.BaseService.GetOneByKey<PSP_LineData>(groupBox4.Tag.ToString());
                d1.UID = groupBox4.Tag.ToString();
                d1.q1 = b2c1.Text;
                d1.q2 = b2c2.Text;
                d1.q3 = b2c3.Text;
                d1.q4 = b2c4.Text;
                d1.q5 = b2c5.Text;
                d1.q6 = b2c6.Text;
                d1.q7 = b2c7.Text;
                d1.q8 = b2c8.Text;
                d1.q9 = b2c9.Text;
                d1.q10 = b2c10.Text;
                d1.q11 = b2c11.Text;
                d1.q12 = b2c12.Text;
                d1.q13 = b2c13.Text;
                d1.q14 = b2c14.Text;
                d1.q15 = b2c15.Text;
                d1.q16 = t9.Text;
                d1.q17 = t10.Text;
                d1.q18 = t11.Text;
                d1.q19 = t12.Text;
                d1.q20 = b2c16.Text;
                d1.q21 = b2c17.Text;
                d1.q22 = b2c18.Text;
                d1.q23 = b2c19.Text;
                d1.q24 = b2c20.Text;
                d1.q25 = b2c21.Text;
                d1.col1 = textBox3.Text;
                if (temp == null)
                {
                    Services.BaseService.Create<PSP_LineData>(d1);
                }
                else
                {
                    Services.BaseService.Update<PSP_LineData>(d1);
                }
            }
            if (groupBox5.Enabled == true)
            {
                PSP_LineData d1 = new PSP_LineData();
                PSP_LineData temp = Services.BaseService.GetOneByKey<PSP_LineData>(groupBox5.Tag.ToString());
                d1.UID = groupBox5.Tag.ToString();
                d1.q1 = b3c1.Text;
                d1.q2 = b3c2.Text;
                d1.q3 = b3c3.Text;
                d1.q4 = b3c4.Text;
                d1.q5 = b3c5.Text;
                d1.q6 = b3c6.Text;
                d1.q7 = b3c7.Text;
                d1.q8 = b3c8.Text;
                d1.q9 = b3c9.Text;
                d1.q10 = b3c10.Text;
                d1.q11 = b3c11.Text;
                d1.q12 = b3c12.Text;
                d1.q13 = b3c13.Text;
                d1.q14 = b3c14.Text;
                d1.q15 = b3c15.Text;
                d1.q16 = t13.Text;
                d1.q17 = t14.Text;
                d1.q18 = t15.Text;
                d1.q19 = t16.Text;
                d1.q20 = b3c16.Text;
                d1.q21 = b3c17.Text;
                d1.q22 = b3c18.Text;
                d1.q23 = b3c19.Text;
                d1.q24 = b3c20.Text;
                d1.q25 = b3c21.Text;
                d1.col1 = textBox4.Text;
                if (temp == null)
                {
                    Services.BaseService.Create<PSP_LineData>(d1);
                }
                else
                {
                    Services.BaseService.Update<PSP_LineData>(d1);
                }
            }
            if (groupBox6.Enabled == true)
            {
                PSP_LineData d1 = new PSP_LineData();
                PSP_LineData temp = Services.BaseService.GetOneByKey<PSP_LineData>(groupBox6.Tag.ToString());
                d1.UID = groupBox6.Tag.ToString();
                d1.q1 = b4c1.Text;
                d1.q2 = b4c2.Text;
                d1.q3 = b4c3.Text;
                d1.q4 = b4c4.Text;
                d1.q5 = b4c5.Text;
                d1.q6 = b4c6.Text;
                d1.q7 = b4c7.Text;
                d1.q8 = b4c8.Text;
                d1.q9 = b4c9.Text;
                d1.q10 = b4c10.Text;
                d1.q11 = b4c11.Text;
                d1.q12 = b4c12.Text;
                d1.q13 = b4c13.Text;
                d1.q14 = b4c14.Text;
                d1.q15 = b4c15.Text;
                d1.q16 = t17.Text;
                d1.q17 = t18.Text;
                d1.q18 = t19.Text;
                d1.q19 = t20.Text;
                d1.q20 = b4c16.Text;
                d1.q21 = b4c17.Text;
                d1.q22 = b4c18.Text;
                d1.q23 = b4c19.Text;
                d1.q24 = b4c20.Text;
                d1.q25 = b4c21.Text;
                d1.col1 = textBox5.Text;
                if (temp == null)
                {
                    Services.BaseService.Create<PSP_LineData>(d1);
                }
                else
                {
                    Services.BaseService.Update<PSP_LineData>(d1);
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}