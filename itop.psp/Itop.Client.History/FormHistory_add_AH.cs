using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Itop.Client.History
{
    public partial class FormHistory_add_AH : DevExpress.XtraEditors.XtraForm
    {
        private string typeTitle = string.Empty;

        public string TypeTitle
        {
            get { return typeTitle; }
            set { typeTitle = value; }
        }
        public string COL10 {
            get { return textEdit2.Text; }
            set { textEdit2.Text = value; }
        }
      
        public int Sortid {
            get { return (int)spinEdit1.Value; }
            set { spinEdit1.Value = value; }
        }
       
        public FormHistory_add_AH()
        {
            InitializeComponent();
        }

        private void FormTypeTitle_Load(object sender, EventArgs e)
        {
            string ProjectID = Itop.Client.MIS.ProgUID;
            string connstr = " ProjectID='" + ProjectID + "'";
            try
            {
                IList<string> strlist = Common.Services.BaseService.GetList<string>("SelectPS_Table_AreaWH_Title", connstr);
                if (strlist.Count > 0)
                {
                    cobArea.DataSource = strlist;
                }
            }
            catch (Exception)
            {

                throw;
            }
            textEdit1.Text = TypeTitle;
            cobArea.SelectedText = TypeTitle;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(textEdit1.Text == string.Empty)
            {
                Itop.Common.MsgBox.Show("请输入分类名称！");
                return;
            }

            else
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
        private void chkArea_CheckedChanged(object sender, EventArgs e)
        {
            if (chkArea.Checked)
            {
                cobArea.Visible = true;
                textEdit1.Visible = false;
            }
            else
            {
                cobArea.Visible = false;
                textEdit1.Visible = true;
                textEdit1.Text = TypeTitle;
            }
        }

        private void cobArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            textEdit1.Text = cobArea.Text;
        }
    }
}