using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Itop.Client.History
{
    public partial class FormForecast3_add : DevExpress.XtraEditors.XtraForm
    {
        private string typeTitle = string.Empty;

        public string TypeTitle
        {
            get { return typeTitle; }
            set { typeTitle = value; }
        }
        /// <summary>
        /// 供电分区
        /// </summary>
        public string COL2 {
            get {
                return comboBoxEdit1.Text;
                return textEdit2.Text;
            }
            set { 
                //comboBoxEdit1.Text = value; 
                textEdit2.Text = value;
            }

          
        }
        /// <summary>
        /// 分类
        /// </summary>
        public string COL3 {
            get { return comboBoxEdit2.Text; }
            set { comboBoxEdit2.Text = value; }
        }
        /// <summary>
        /// 面积
        /// </summary>
        public double y1992 {
            get { return (double)spinEdit4.Value; }
            set { spinEdit4.Value = (decimal)value; }
        }
        public int Sortid {
            get { return (int)spinEdit1.Value; }
            set { spinEdit1.Value = value; }
        }
        
        public FormForecast3_add()
        {
            InitializeComponent();
        }

        private void FormTypeTitle_Load(object sender, EventArgs e)
        {
            textEdit1.Text = TypeTitle;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(textEdit1.Text == string.Empty)
            {
                Itop.Common.MsgBox.Show("请输入分类名称！");
                return;
            }

            //if (textEdit1.Text == typeTitle)
            //{
            //    DialogResult = DialogResult.Cancel;
            //}
            //else
            {
                typeTitle = textEdit1.Text;
                DialogResult = DialogResult.OK;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxEdit1_EditValueChanged(object sender, EventArgs e) {
            

        }

        private void comboBoxEdit1_TextChanged(object sender, EventArgs e) {
            if (comboBoxEdit1.Text == "") {
                spinEdit4.Value = 0m;
                comboBoxEdit2.Text = "";
            }
        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {
            if (textEdit2.Text == "")
            {
                spinEdit4.Value = 0m;
                comboBoxEdit2.Text = "";
            }
        }
    }
}