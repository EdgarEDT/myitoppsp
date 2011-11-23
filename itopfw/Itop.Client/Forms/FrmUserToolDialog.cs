using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Common;
using System.IO;
using Itop.Client.Base;

namespace Itop.Client.Forms
{
    public partial class FrmUserToolDialog : FormBase
    {
        public FrmUserToolDialog()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return txtTitle.Text; }
            set { txtTitle.Text = value; }
        }

        public string Program
        {
            get { return beProgram.Text; }
            set { beProgram.Text = value; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.txtTitle.Text = this.txtTitle.Text.Trim();
            if (this.txtTitle.Text == "")
            {
                MsgBox.Show("请输入标题！");
                this.txtTitle.Focus();
                return;
            }

            if (this.beProgram.Text == "")
            {
                MsgBox.Show("请选择程序！");
                this.beProgram.Focus();
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void beProgram_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "可执行文件(*.exe)|*.exe";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.beProgram.Text = dlg.FileName;
                this.txtTitle.Text = Path.GetFileNameWithoutExtension(dlg.FileName);
            }
        }
    }
}