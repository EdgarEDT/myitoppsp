using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Stutistic;
using Itop.Common;
using Itop.Client.Base;
using DevExpress.XtraGrid.Views.Grid;
using Itop.Client.ExpressCalculate;

namespace Itop.Client.Stutistics
{
    public partial class FrmExpressCalNewTitle : DevExpress.XtraEditors.XtraForm
    {



        private string caption = "";
        private string strexpressiontemp = "";
        private string groupBoxtext= "";
        private string filedname = "";
        private int visualeintex = 0;
        private int SaveDecimalpoint =2;
        private bool ISUpdate = false;
        private GridView gridView1;
        public GridView GridView
        {
            get { return gridView1; }
            set { gridView1 = value; }
        }
        public int SaveDecimalPoint
        {
            get { return SaveDecimalpoint; }
            set { SaveDecimalpoint = value; }
        }
        public int VisualeIntex
        {
            get { return visualeintex; }
            set { visualeintex = value; }
        }
        public string Filedname
        {
            get { return filedname; }
            set { filedname = value; }
        }

        public string StrExpressiontemp
        {
            get { return strexpressiontemp; }
            set { strexpressiontemp = value; }
        }

        public string Caption
        {
            get { return caption; }
            set { caption = value; }
        }
       public string  GroupBoxText
       {
           get { return groupBoxtext; }
           set { groupBoxtext = value; }
       }
        public bool ISUpDate
        {
            get { return ISUpdate; }
            set { ISUpdate = value; }
        }

        public FrmExpressCalNewTitle()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (spinEdit1.Text == "")
            {
                MessageBox.Show("分类名不能空");
                return;
            }
           //if ( textBox1.Text == "") 
           // {
           //      MessageBox.Show("表达式不能空");
           //     return;
           // }
           //if ( textBox2.Text == "") 
           //{
           //    MessageBox.Show("保留小数点位数不能空");
           //    return;
           //}
            if (textBox2.Text=="")
            {
                SaveDecimalPoint = 2;
                textBox2.Text = "2";
            }
            else
                {
                    SaveDecimalPoint=Convert.ToInt32(textBox2.Text);
                    
                }
               if(SaveDecimalPoint>15)
               {
                   SaveDecimalPoint = 15;
                   textBox2.Text = "15";
               }


           ExpressionCalculator calc = new ExpressionCalculator();
           StrExpressiontemp = calc.CharConverter(calc.CharConverter(textBox1.Text));
           textBox1.Text = StrExpressiontemp;
           if (!calc.GetColumnExit(gridView1, calc.CharConverter(spinEdit1.Text),filedname))
           {
               MessageBox.Show("列名" + spinEdit1.Text + "已存在或包含括号[]，请重命名");
               return;
           }

          
           if (!calc.ExpressiontempISIllegal(StrExpressiontemp, gridView1, false, -1))
               return;
           StrExpressiontemp = textBox1.Text;
           caption =calc.CharConverter(spinEdit1.Text) ;
           SaveDecimalpoint = Convert.ToInt32(textBox2.Text);
           this.DialogResult = DialogResult.OK;
        }

        private void FrmExpressCalNewTitle_Load(object sender, EventArgs e)
        {
            textBox1.Text = strexpressiontemp;
            textBox2.Text = SaveDecimalpoint.ToString();
            spinEdit1.Text = caption;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 31 && (e.KeyChar < '0' || e.KeyChar > '9'))
            { e.Handled = true; } 
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string strExpression = "";
            if (textBox1.Text == "")
            {
                MessageBox.Show("表达式不能为空");
                return;
            }

            if (textBox2.Text == "")
            {
                textBox2.Text = "2";
                SaveDecimalpoint = 2;

            }
            else
            {
                SaveDecimalPoint = Convert.ToInt32(textBox2.Text);
                if (SaveDecimalPoint < 0)
                {
                    SaveDecimalPoint = 2;
                    textBox2.Text = "2";
                }
                else
                    if (SaveDecimalPoint > 15)
                    {
                        SaveDecimalPoint = 15;
                        textBox2.Text = "15";
                    }
            }

            ExpressionCalculator calc = new ExpressionCalculator();


            strExpression = calc.CharConverter(textBox1.Text);
            textBox1.Text = strExpression;




            if (!calc.ExpressiontempISIllegal(strExpression, gridView1, true, SaveDecimalPoint))
                return;
        }



    }
}