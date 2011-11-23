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
    public partial class frmElementName : FormBase
    {
        private string UID="";
        public frmElementName()
        {
            InitializeComponent();
            textBox1.Focus();
        }
        public frmElementName(string svgUID)
        {
            InitializeComponent();
            textBox1.Focus();
            UID = svgUID;
        }
        public string TextInput
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                MessageBox.Show("名称不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Focus();
                return;
            }
            try{
                int y = Convert.ToInt32(textBox1.Text.Substring(0, 4));
                                       
                
            }
            catch(Exception  e2){
                MessageBox.Show("名称前4位应包含年份信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Focus();
                    return;
            }
            //else
            //{
                PSPDIR pspDIR = new PSPDIR();
                pspDIR.FileName = textBox1.Text;
                IList list = Services.BaseService.GetList("SelectPSPDIRByFileName", pspDIR);
                if (list.Count>1||(list.Count==1&&UID==""))
                {
                    MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }
            //}
           
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)   //允许输入回退键
            {
                e.Handled = true;
            }
        }
    }
}