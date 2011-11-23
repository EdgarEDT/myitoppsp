using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Itop.Client.Chen
{
    public partial class FormTypeTitleAH : DevExpress.XtraEditors.XtraForm
    {
        private string typeTitle = string.Empty;
        public string TypeTitle
        {
            get { return typeTitle; }
            set { typeTitle = value; }
        }

        public FormTypeTitleAH()
        {
            InitializeComponent();
        }

        private void FormTypeTitleAH_Load(object sender, EventArgs e)
        {
            textEdit1.Text = TypeTitle;
            cobArea.SelectedText = typeTitle;
            string ProjectID = Itop.Client.MIS.ProgUID;
            string connstr = " ProjectID='" + ProjectID + "'";
            try
            {
                IList<string> strlist = Common.Services.BaseService.GetList<string>("SelectPS_Table_AreaWH_Title", connstr);
                if (strlist.Count>0)
                {
                    cobArea.DataSource = strlist;
                }    
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (chkArea.Checked)
            {

                if (cobArea.Text=="")
                {
                    Itop.Common.MsgBox.Show("请选择地区名称！");
                    return;
                }
                else if (cobArea.Text==typeTitle)
                {
                    DialogResult = DialogResult.Cancel;
                }
                else
                {
                    typeTitle = cobArea.Text;
                    DialogResult = DialogResult.OK;
                }
            }
            else
            {
                if (textEdit1.Text=="")
                {
                    Itop.Common.MsgBox.Show("请添写分类名称！");
                    return;
                }
                else if (textEdit1.Text == typeTitle)
                {
                    DialogResult = DialogResult.Cancel;
                }
                else
                {
                    typeTitle = textEdit1.Text;
                    DialogResult = DialogResult.OK;
                }
            }
            
           
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

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
            }
        }

        private void cobArea_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}