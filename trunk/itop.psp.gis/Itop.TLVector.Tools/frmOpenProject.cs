using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmOpenProject : FormBase
    {
        public frmOpenProject()
        {
            InitializeComponent();
        }
        private DataTable dt = new DataTable();
        protected string projectID;
        public string ProjectID
        {
            get
            {
                return projectID;
            }
            set
            {
                projectID = value;
            }
        }
        public void Initdata()
        {
            dt.Clear();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Class");
            dt.Columns.Add("FileType");
            dt.Columns.Add("Check",typeof(bool));
            PSP_ELCPROJECT pr = new PSP_ELCPROJECT();
            pr.ProjectID = this.ProjectID;
            IList list = Services.BaseService.GetList("SelectPSP_ELCPROJECTList", pr);
            foreach(PSP_ELCPROJECT pe in list)
            {
                DataRow row = dt.NewRow();
                row["ID"] = pe.ID;
                row["Name"] = pe.Name;
                row["Class"] = pe.Class;
                row["FileType"] = pe.FileType;
                row["Check"] = false;
                dt.Rows.Add(row);
            }
            gridControl1.DataSource = dt;
        }


        private void frmOpenProject_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = dt;
           // Initdata();
        }
        public String ChaoLiuSUID
        {
            get
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (((bool)dr["Check"] == true) && (dr["FileType"].ToString()) == "潮流")
                    {
                        return dr["ID"].ToString();
                    }
                }
                return null;
            }
        }
        public String DuanLuSUID
        {
            get
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (((bool)dr["Check"] == true) && (dr["FileType"].ToString()) == "短路")
                    {
                        return dr["ID"].ToString();
                    }
                }
                return null;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (ChaoLiuSUID==null&&DuanLuSUID==null)
            {
                MessageBox.Show("请先选择电气计算方案！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.None;
                return;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}