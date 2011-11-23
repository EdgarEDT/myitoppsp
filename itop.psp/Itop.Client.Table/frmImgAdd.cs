using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using System.IO;
using Itop.Client.Common;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class frmImgAdd : FormBase
    {
        private string uid = "";

        public string Uid
        {
            get { return uid; }
            set { uid = value; }
        }
        public frmImgAdd()
        {
            InitializeComponent();
        }

        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //openFileDialog1.OpenFile();
                buttonEdit1.Text = openFileDialog1.FileName;
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(buttonEdit1.Text==""){
                MessageBox.Show("请选择附件。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(buttonEdit1.Text, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                byte[] filebt = br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();
                string[] str = buttonEdit1.Text.Split("\\".ToCharArray());
                PSP_ImgInfo imgInfo = new PSP_ImgInfo();
                imgInfo.UID = Guid.NewGuid().ToString();
                imgInfo.TreeID = uid;
                imgInfo.Image = filebt;
                imgInfo.Name = str[str.Length - 1];
                imgInfo.Remark = txtRe.Text;
                Services.BaseService.Create<PSP_ImgInfo>(imgInfo);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            {
                MessageBox.Show("文件正在使用中，请关闭选择的文件后重试。");
            }
        }
    }
}