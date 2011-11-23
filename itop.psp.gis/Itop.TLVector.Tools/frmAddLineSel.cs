using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmAddLineSel : FormBase
    {
        private List<string> strList;
        private IList lineList;

        Hashtable tb = new Hashtable();

        public bool newLine = false;
        private string lineCode="";


        public IList LineList
        {
            get { return lineList; }
            set { lineList = value; }
        }

        public string LineCode
        {
            get { return lineCode; }
            set { lineCode = value; }
        }



        public List<string> StrList
        {
            get { return strList; }
            set { strList = value; }
        }


        public frmAddLineSel()
        {
            InitializeComponent();
        }

        private void frmAddLineSel_Load(object sender, EventArgs e)
        {
            InitData();
        }
        public void InitData()
        {
            if(lineList!=null){
                for (int i = 0; i < lineList.Count;i++ )
                {
                    tb.Add(((LineInfo)lineList[i]).LineName, lineList[i]);
                }

                for (int j = 0; j < lineList.Count; j++)
                {
                    listBoxControl1.Items.Add(((LineInfo)lineList[j]).LineName);
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            newLine = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            object obj=tb[listBoxControl1.SelectedValue];
            //lineCode = listBoxControl1.SelectedValue.ToString();
            if (obj == null)
            {
                MessageBox.Show("请选择线路。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            lineCode = ((LineInfo)obj).EleID;
            newLine = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}