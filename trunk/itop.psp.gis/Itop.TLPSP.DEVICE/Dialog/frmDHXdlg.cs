using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Client.Common;

namespace Itop.TLPSP.DEVICE
{
    public partial class frmDHXdlg : Itop.Client.Base.FormBase
    {
        public string uid = "";
        private string lines = "''";
        private PSPDEV p = new PSPDEV();
        public frmDHXdlg()
        {
            InitializeComponent();
        }
        public void LoadData(string id)
        {

            string sql = " where EleID='" + id + "' and type='555'";
            p = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", sql);
            if(p!=null){
                text1.Text = p.X1.ToString();
                comboBox1.Text = p.Lable;
                text2.Text = p.Y1.ToString();
                //comboBox2.Text = p.RateVolt.ToString();
                if(p.Name!=""){
                    lines = p.Name;
                    lines = lines.Replace("@", "'");
                    LoadGrid();
                }
            }
        }
        private void frmDHXdlg_Load(object sender, EventArgs e)
        {
            LoadData(uid);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
           PSPDEV sel= (PSPDEV)DeviceHelper.SelectDevice("05",Itop.Client.MIS.ProgUID);
           if (sel != null)
           {
               lines = lines + ",'" + sel.SUID + "'";
               LoadGrid();
           }
        }
        public void LoadGrid()
        {
            string sql = " where SUID in (" + lines + ")";
            IList<PSPDEV> list =Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            gridControl.DataSource = list;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (p != null)
                {
                    p.X1 = Convert.ToDouble(text1.Value);
                    p.Y1 = Convert.ToDouble(text2.Value);
                    p.Lable = comboBox1.Text;
                    p.Type = "555";
                    lines = lines.Replace("'", "@");
                    p.Name = lines;
                    Services.BaseService.Update<PSPDEV>(p);
                }
                else
                {
                    p = new PSPDEV();
                    p.SUID = Guid.NewGuid().ToString();
                    p.X1 = Convert.ToDouble(text1.Value);
                    p.Y1 = Convert.ToDouble(text2.Value);
                    p.Lable = comboBox1.Text;
                    p.EleID = uid;
                    p.Type = "555";
                    lines = lines.Replace("'", "@");
                    p.Name = lines;
                    Services.BaseService.Create<PSPDEV>(p);
                }
            }
            catch { }
            this.Close();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (FocusedObject == null)
            {
                MessageBox.Show("请从下方列表选择一条线路。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            lines = lines.Replace(FocusedObject.SUID , "");
            //lines = lines.Substring(0, lines.Length - 1);
            LoadGrid();

        }
        public PSPDEV FocusedObject
        {
            get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PSPDEV; }
        }
    }
}