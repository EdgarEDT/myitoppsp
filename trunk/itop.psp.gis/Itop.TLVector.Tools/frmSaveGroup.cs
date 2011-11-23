using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmSaveGroup : FormBase
    {
        private string content;
        public RectangleF rect;
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        public frmSaveGroup()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(textEdit1.Text==""){
                MessageBox.Show("名称不能为空。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            UseGroup ug = new UseGroup();
            ug.UID = Guid.NewGuid().ToString();
            ug.GroupName = textEdit1.Text;
            ug.Content = content;
            ug.Remark = textEdit2.Text;
            ug.X = rect.X.ToString();
            ug.Y = rect.Y.ToString();
            ug.Width = rect.Width.ToString();
            ug.Height = rect.Height.ToString();
            Services.BaseService.Create<UseGroup>(ug);
            this.Close();
        }
    }
}