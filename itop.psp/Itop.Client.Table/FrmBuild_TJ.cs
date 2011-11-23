using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmBuild_TJ : FormBase
    {
        public FrmBuild_TJ()
        {
            InitializeComponent();
           
        }
        public DataTable dt = new DataTable();
        public DevExpress.XtraTreeList.TreeList treelist;
        public string sql = "";
        public string startyear = "";
        public  string endyear = "";
        public string dianya = "";
        public string areatype = "";
        public void InitCom()
        {
           
        }
      
        
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (comboBoxEdit2.Text!="")
            {
                sql += " and Cast(BuildYear as int)>=" + comboBoxEdit2.Text;
                startyear = comboBoxEdit2.Text;
            }
            if (comboBoxEdit3.Text != "")
            {
                endyear = comboBoxEdit3.Text;
                //sql += " and Cast(BuildEd as int)<=" + comboBoxEdit3.Text;
            }
            if (comboBoxEdit9.Text != "")
            {
                sql += " and Col3='" + comboBoxEdit9.Text + "'";
            }
            if (combState.Text != "")
            {
                sql += " and State='" + combState.Text + "'";
            }
            if (comboBoxEdit12.Text != "")
            {
                sql += " and Col4='" + comboBoxEdit12.Text + "'";
            }
            if (comboBoxEdit1.Text != "")
            {
                sql += " and FormID='" + comboBoxEdit1.Text + "'";
                dianya = comboBoxEdit1.Text;
            }
            if (combWQFL.Text != "")
            {
                //sql += " and AreaType='" + combWQFL.Text + "'";
                areatype = combWQFL.Text;
            }


            if (sql.Length>5)
            {
                sql = sql.Substring(4, sql.Length - 4);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void FrmBuild_TJ_Load(object sender, EventArgs e)
        {
            for (int i = DateTime.Now.Year - 10; i <= DateTime.Now.Year+20; i++)
            {
                comboBoxEdit2.Properties.Items.Add(i.ToString());
                comboBoxEdit3.Properties.Items.Add(i.ToString());
            }
            //网区类型
            IList<string> areatyplist = null;
            if (Common.Services.BaseService.GetList<string>("SelectBuildProDifAreaType", "") != null)
            {
                areatyplist = Common.Services.BaseService.GetList<string>("SelectBuildProDifAreaType", "");

                for (int j = 0; j < areatyplist.Count; j++)
                {
                    combWQFL.Properties.Items.Add(areatyplist[j]);
                }
            }

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            FrmProjShowColumn frm = new FrmProjShowColumn();
            frm.treelist = treelist;
            if (frm.ShowDialog()==DialogResult.OK)
            {
                dt = frm.dt;
            }
        }

      


    }
}