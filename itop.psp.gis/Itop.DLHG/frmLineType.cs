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
    public partial class frmLineType : FormModuleBase
    {
        public frmLineType()
        {
            InitializeComponent();
        }
        protected override void Add()
        {
            if (!AddRight)
            {
                MessageBox.Show("��û�д�Ȩ�ޡ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ctrlLineType1.AddObject();
        }
        protected override void Edit()
        {
            if (!EditRight)
            {
                MessageBox.Show("��û�д�Ȩ�ޡ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ctrlLineType1.UpdateObject();
        }
        protected override void Del()
        {
            if (!DeleteRight)
            {
                MessageBox.Show("��û�д�Ȩ�ޡ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ctrlLineType1.DeleteObject();
        }
        protected override void Print()
        {
            ctrlLineType1.PrintPreview();
        }
        public void InitData()
        {
            ctrlLineType1.RefreshData();
        }

        private void frmglebeType_Load(object sender, EventArgs e)
        {
            InitData();
            ctrlLineType1.AllowUpdate = EditRight;
        }
    }
}