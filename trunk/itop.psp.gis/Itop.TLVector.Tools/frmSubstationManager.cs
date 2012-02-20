using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ItopVector.Core.Interface.Figure;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public delegate void OnOpenSubhandler(object sender, string sid);

    public partial class frmSubstationManager : FormBase
    {
        public string code = "";

        public string KeyID = "";
        public string SUID = "";
      //  public static ItopVectorControl vec;
        public event OnOpenSubhandler OnOpen;
        public frmSubstationManager()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        void tj_OnOpen(object sender, string sid)
        {
            if (OnOpen != null)
            {
                OnOpen(sender,sid);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmSubstationParMng mng = new frmSubstationParMng();
            mng.Show();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            frmSubstationMng f = new frmSubstationMng();
            f.ShowDialog();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            frmSubstationCodeSel f = new frmSubstationCodeSel();
            if (f.ShowDialog() == DialogResult.OK)
            {
                code = f.FocusedObject.UID;
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }
            //this.Close();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            frmSubstatinYXInfo f = new frmSubstatinYXInfo();
            f.Show();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            frmPengFen f = new frmPengFen();
            f.Show();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            frmTongJi tj = new frmTongJi();
            // frmTongJi.vec = vec;
            tj.OnOpen += new OnOpensub2handler(tj_OnOpen);
            if (tj.ShowDialog() == DialogResult.Ignore)
            {
                KeyID = tj.KeyID;
                SUID = tj.SUID;
                this.DialogResult = DialogResult.Ignore;
                this.Close();
            }
        }
    }
}