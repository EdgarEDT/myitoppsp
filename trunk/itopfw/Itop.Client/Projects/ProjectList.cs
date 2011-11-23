using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.RightManager;
using Itop.Client.Base;

namespace Itop.Client.Projects
{
    public partial class ProjectList : FormBase
    {

        public Project PJ
        {
            get 
            {
                return this.ctrlProject1.FocusedObject;
            }
        
        }


        public ProjectList()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.ctrlProject1.AddObject();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.ctrlProject1.UpdateObject();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.ctrlProject1.DeleteObject();
        }

        private void ProjectList_Load(object sender, EventArgs e)
        {
            this.ctrlProject1.RefreshData();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if(this.ctrlProject1.FocusedObject!=null)
                this.DialogResult = DialogResult.OK;
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            if (this.ctrlProject1.FocusedObject == null)
            {
                MessageBox.Show("请选择项目。", "提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            frmProgLayerManager p = new frmProgLayerManager();
            p.progid = ctrlProject1.FocusedObject.UID;
            p.Show();
        }
    }
}