using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Stutistics;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmSubSel : FormBase
    {
        public frmSubSel()
        {
            InitializeComponent();
            ctrlLine_Info1.IsSelect = true;
        }
        public void ReDate(string layerId,bool IsRun,bool IsLine,string power)
        {
            //if (IsLine)
            //{
                this.Text = "��·�б�";
            //}
            //else
            //{
            //    this.Text = "���վ�б�";
            //}
            ctrlLine_Info1.RefreshData(layerId, IsRun, power);
            //ctrlPowerProTypes1.RefreshData(layerId, IsRun, IsLine,power);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (ctrlLine_Info1.FocusedObject == null)
            {
                MessageBox.Show("��ѡ�����ݡ�","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ctrlLine_Info1_Load(object sender, EventArgs e)
        {

        }
    }
}