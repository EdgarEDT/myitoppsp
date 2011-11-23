using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Itop.Client.History
{
    public partial class FormFqHistoryDF_Edit2 : DevExpress.XtraEditors.XtraForm
    {
        private string typeTitle = string.Empty;
        private static string col5="";
        //项目类型
        private string[] strtype ={ "已建成未投产", "在建", "前期项目", "重要前期项目" };
       //项目性质
        private string[] strpro ={ "工业", "纺织", "居民", "商业" };
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
        //public string COL5 {
        //    get { return col5 = combtype.Text; return col5; }
        //    set { combtype.Text = value; }
        //}
        /// <summary>
        /// 工作进展（备注）
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
            get { return combpro.Text; }
            set { combpro.Text = value; }
        }
        /// <summary>
        /// 建设年限
        /// </summary>
        //public string COL7 {
        //    get 
        //    {
        //      return txtbuildyear.Text;
        //    }
        //    set { txtbuildyear.Text = value; }
        //}
        
        
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
        //public string COL11
        //{
        //    get
        //    {
        //       return combstartyear.Text;
        //    }
        //    set { combstartyear.Text = value; }
        //}
        /// <summary>
        /// 计划投产时间
        /// </summary>
        public string COL12
        {
            get
            {
               return comboperyear.Text;
            }
            set { comboperyear.Text = value; }
        }
        public FormFqHistoryDF_Edit2()
        {
            InitializeComponent();
            //combtype.Text = col5;
        }

        private void FormTypeTitle_Load(object sender, EventArgs e)
        {
            textEdit1.Text = TypeTitle;
            AddType();
            
        }
        //添加项目类别，性质
        private void AddType()
        {
            //combtype.Properties.Items.Clear();
            //for (int i = 0; i < strtype.Length; i++)
            //{
            //    combtype.Properties.Items.Add(strtype[i]);
            //}
            combpro.Properties.Items.Clear();
            for (int j = 0; j < strpro.Length; j++)
            {
                combpro.Properties.Items.Add(strpro[j]);
            }
            int startyear = DateTime.Now.Year-30;
            //combstartyear.Properties.Items.Clear();
            //for (int k = startyear; k < startyear + 20; k++)
            //{
            //    combstartyear.Properties.Items.Add(k.ToString());
            //}
            comboperyear.Properties.Items.Clear();
            for (int l = startyear; l < startyear + 40; l++)
            {
                comboperyear.Properties.Items.Add(l.ToString());
            }

        }
        //确定

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(textEdit1.Text == string.Empty)
            {
                Itop.Common.MsgBox.Show("请输入分类名称！");
                return;
            }
            int tempint = 0;
            //if (!int.TryParse(combstartyear.Text, out tempint))
            //{
            
            //    MessageBox.Show("计划开工时间输入错误！");
            //    return ;
            //}

            //if (!int.TryParse(txtbuildyear.Text, out tempint))
            //{
               
            //    MessageBox.Show("建设年限输入错误！");
            //    return ;
            //}
            if (!int.TryParse(comboperyear.Text, out tempint))
            {
              
                MessageBox.Show("投产时间输入错误！");
                return ;
            }
            double tempdouble = 0;
            if (!double.TryParse(spinEdit2.Text.Trim(), out tempdouble) || !double.TryParse(spinEdit3.Text.Trim(), out  tempdouble))
            {
                MessageBox.Show("电量或负荷输入错误！");
                return ;
            }
           
            typeTitle = textEdit1.Text;
            DialogResult = DialogResult.OK;
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

   }
}