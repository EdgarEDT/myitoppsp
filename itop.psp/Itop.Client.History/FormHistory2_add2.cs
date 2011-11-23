using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Itop.Client.History
{
    public partial class FormHistory2_add2 : DevExpress.XtraEditors.XtraForm
    {
        private string typeTitle = string.Empty;
        private static string col5="";
        public string TypeTitle
        {
            get { return typeTitle; }
            set { typeTitle = value; }
        }
        /// <summary>
        /// 建设规模
        /// </summary>
        public string COL10 {
            get { return textEdit2.Text; }
            set { textEdit2.Text = value; }
        }
        /// <summary>
        /// 分类
        /// </summary>
        public string COL5 {
            get { return col5 = comboBoxEdit1.Text; return col5; }
            set { comboBoxEdit1.Text = value; }
        }
        /// <summary>
        /// 工作进展
        /// </summary>
        public string COL6 {
            get { return comboBoxEdit2.Text; }
            set { comboBoxEdit2.Text = value; }
        }
        /// <summary>
        /// 项目性质
        /// </summary>
        public string COL13
        {
            get { return comboBoxEdit3.Text; }
            set { comboBoxEdit3.Text = value; }
        }
        /// <summary>
        /// 建设年限
        /// </summary>
        public string COL7 {
            get { return textEdit3.Text; }
            set { textEdit3.Text = value; }
        }
        
        public int Sortid {
            get { return (int)spinEdit1.Value; }
            set { spinEdit1.Value = value; }
        }
        /// <summary>
        /// 电量
        /// </summary>
        public double y1990 {
            get { return (double)spinEdit2.Value; }
            set { spinEdit2.Value = (decimal)value; }
        }
        /// <summary>
        /// 负荷
        /// </summary>
        public double y1991 {
            get { return (double)spinEdit3.Value; }
            set { spinEdit3.Value = (decimal)value; }
        }
        /// <summary>
        /// 计划开工时间
        /// </summary>
        public string COL11
        {
            get { return dateTimePicker1.Text; }
            set { dateTimePicker1.Text = value; }
        }
        /// <summary>
        /// 计划投产时间
        /// </summary>
        public string COL12
        {
            get { return dateTimePicker2.Text; }
            set { dateTimePicker2.Text = value; ; }
        }
        public FormHistory2_add2()
        {
            InitializeComponent();
            comboBoxEdit1.Text = col5;
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

        private void textEdit1_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control && e.KeyCode == Keys.B) {
                pasteData();                
            }
        }

        private void pasteData() {
            IDataObject obj1 = Clipboard.GetDataObject();
            string text = obj1.GetData("Text").ToString();
            string[] lines = text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Length; i++) {
                string[] items = lines[i].Split(new string[] { "\t" }, StringSplitOptions.None);
                if (items.Length !=4) continue;
                textEdit1.Text = items[0];
                textEdit2.Text = items[1];
                textEdit3.Text = items[2];
                comboBoxEdit2.Text = items[3];
            }
        }
    }
}