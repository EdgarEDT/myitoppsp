
				
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Itop.Client.MainMenu;

namespace Itop.Client.About {
    /// <summary>
    /// 关于窗体
    /// </summary>
    public partial class AboutForm : Itop.Client.Base.DialogForm, IMenuCommand {
        public AboutForm() {
           
            InitializeComponent();
            FormView.Paint(this);
            this.BackgroundImage = Itop.Client.Resources.ImageListRes.GetAboutPhoto();
        }

        override public bool Execute() {
            ShowDialog();
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
            
    }
}