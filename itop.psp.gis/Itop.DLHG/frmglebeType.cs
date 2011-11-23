using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
using Itop.Domain.Graphics;

namespace Itop.DLGH
{
    public partial class frmglebeType : FormModuleBase
    {
        public frmglebeType()
        {
            InitializeComponent();
        }
        protected override void Add()
        {
            if (!AddRight)
            {
                MessageBox.Show("您没有此权限。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ctrlglebeType1.AddObject();
        }
        protected override void Edit()
        {
            if (!EditRight)
            {
                MessageBox.Show("您没有此权限。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ctrlglebeType1.UpdateObject();
        }
        protected override void Del()
        {
            if (!DeleteRight)
            {
                MessageBox.Show("您没有此权限。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ctrlglebeType1.DeleteObject();
        }
        protected override void Print()
        {
            ctrlglebeType1.PrintPreview();
        }
        public void InitData()
        {
            ctrlglebeType1.RefreshData();
        }

        private void frmglebeType_Load(object sender, EventArgs e)
        {
            InitData();
            ctrlglebeType1.AllowUpdate = EditRight;
        }
    }
}