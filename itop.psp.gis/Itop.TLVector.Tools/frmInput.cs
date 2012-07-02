using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmInput : FormBase
    {
        private string inputText;
        private string inputType;
        public ArrayList list = new ArrayList();
        public string id = "";
        public ItopVector.Core.Document.SvgDocument symbolDoc;
        public frmInput()
        {
            InitializeComponent();
        }
        public bool hide
        {
            set
            {
                if (value)
                {
                    label2.Hide();
                    radioGroup1.Hide();
                }
               
            }
        }
        public string InputString
        {
            get
            {
                return inputText;
            }
            set
            {
                this.inputText = value;
                tbName.Text = value;
            }
        }
        public string InputType
        {
            get { return inputType; }
            set
            {
                inputType = value;
                for (int i = 0; i < radioGroup1.Properties.Items.Count;i++ )
                {
                    if(value==radioGroup1.Properties.Items[i].Description){
                        radioGroup1.SelectedIndex=i;
                    }
                }
                #if(!CITY)
                if (value == "电网规划层") {
                    label2.Hide();
                    radioGroup1.Hide();
                }
                #endif
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.tbName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("名称不能为空。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                tbName.Focus();
                return;
            }
            this.InputString = this.tbName.Text;
            this.inputType = this.radioGroup1.Properties.Items[this.radioGroup1.SelectedIndex].Description;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            frmLayerGradeSave frmSave = new frmLayerGradeSave();
            frmSave.Text = "请选择要修改到的分级。";
            frmSave.SymbolDoc = symbolDoc;
            frmSave.InitData2(id);

            if (frmSave.ShowDialog() == DialogResult.OK)
            {
                list = frmSave.GetSelectNode2();
                this.DialogResult = DialogResult.Retry;
                this.Close();
            }
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmInput_Load(object sender, EventArgs e)
        {
            if( symbolDoc==null){
                simpleButton3.Visible = false;
            }
            if (radioGroup1.Visible==false)
            {
                this.Size = new Size(this.Size.Width, 130);
            }
        }
    }
}