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
    public partial class frmSubSelS : FormBase
    {
        public frmSubSelS()
        {
            InitializeComponent();
            ctrlSubstation_Info1.IsSelect = true;
        }
        public void ReDate(string layerId,bool IsRun,bool IsLine,string power)
        {
            //if (IsLine)
            //{
            //this.Text = "��·�б�";
            //}
            //else
            //{
            this.Text = "���վ�б�";
            //}
            ctrlSubstation_Info1.RefreshData(layerId, IsRun, power);
            //ctrlPowerProTypes1.RefreshData(layerId, IsRun, IsLine,power);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (ctrlSubstation_Info1.FocusedObject == null)
            {
                MessageBox.Show("��ѡ�����ݡ�","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}