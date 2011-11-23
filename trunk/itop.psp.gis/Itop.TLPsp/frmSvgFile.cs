using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using System.Collections;
using Itop.Client.Common;
using Itop.Client.Base;
namespace Itop.TLPsp
{
    public partial class frmSvgFile : FormBase
    {
        private string UID = "";
        public frmSvgFile()
        {
            InitializeComponent();
            textBox1.Focus();
        }
        public frmSvgFile(PSPDIR pspDIR)
        {
            InitializeComponent();
            InitData(pspDIR);
            textBox1.Focus();
        }
        private void InitData(PSPDIR pspDIR)
        {
            if (pspDIR.FileName!=null)
            {
                textBox1.Text = pspDIR.FileName;
            }
            if (pspDIR.FileType!=null)
            {
                textBox2.Text = pspDIR.FileType;
            }
            UID = pspDIR.FileGUID;
        }
        public string SvgFileName
        {
            get 
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }

        private void simpleButton1_MouseDown(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("名称不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Focus();
                return;
            }
            else
            {
                PSPDIR pspDIR = new PSPDIR();
                pspDIR.FileName = textBox1.Text;
                IList list = Services.BaseService.GetList("SelectPSPDIRByFileName", pspDIR);
                if (list.Count > 1 || (list.Count == 1 && UID != ((PSPDIR)list[0]).FileGUID))
                {
                    MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Focus();
                    return;
                }
                else
                {
                    simpleButton1.DialogResult = DialogResult.OK;
                }
            }
        }

    }
}