using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.RightManager;
using System.Collections;
using Itop.Client.Projects;
using DevExpress.XtraTreeList.Nodes;
using Itop.Common;
using Itop.Client.Base;

namespace Itop.Client.History
{
    public partial class FormProjectDataCopy : FormBase
    {
        string projectuid = "";

        public string ProjectUID
        {
            get { return projectuid; }
            set { projectuid = value; }
        }


        public FormProjectDataCopy()
        {
            InitializeComponent();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            TreeListNode tln = treeList2.FocusedNode;
            if (tln == null)
            {
                MsgBox.Show("请选择项目或卷宗");
                return;
            }

            if (tln["UID"].ToString() == projectuid)
            {
                MsgBox.Show("当前为该项目卷宗，请选择其他项目卷宗");
                return;
            }

            projectuid = tln["UID"].ToString();

            this.DialogResult = DialogResult.OK;
        }

        private void FormProjectDataCopy_Load(object sender, EventArgs e)
        {
            string s = "  IsGuiDang!='是' order by SortID";
            IList<Project> list = Services.BaseService.GetList<Project>("SelectProjectByWhere", s);
            DataTable dt = Itop.Common.DataConverter.ToDataTable((IList)list, typeof(Project));
            this.treeList2.DataSource = dt;
        }

    }
}