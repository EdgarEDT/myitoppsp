using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
namespace Itop.Client.Layouts
{
    public partial class FrmGHBGdata : FormBase
    {
        public string GHBGyear = "";

        public FrmGHBGdata()
        {
            InitializeComponent();
        }
        public string nowyear = DateTime.Now.Year.ToString();
        private void FrmGHBGdata_Load(object sender, EventArgs e)
        {
            AddcoBGHyear();
            //Ĭ��ѡ��ǰ��
            cobGHBYear_selectvalue(nowyear);
        }
        //ѡ��ĳһ��
        private void cobGHBYear_selectvalue(string strvalue)
        {
            for (int i = 0; i < cobGHBGYear.Items.Count; i++)
            {
                if (cobGHBGYear.Items[i].ToString()==strvalue)
                {
                    cobGHBGYear.SelectedIndex = i;
                    return;
                }
            }
        }
        //��ӹ滮�������
        private void AddcoBGHyear()
        {
            cobGHBGYear.Items.Clear();
            
            int startyear = 2008;
            int entyear = 2060;
            for (int i = startyear; i <= entyear; i++)
            {
                cobGHBGYear.Items.Add(i);
            }
        }

        //ȷ��
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        //ȡ��
        private void btnCanser_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cobGHBGYear_SelectedValueChanged(object sender, EventArgs e)
        {
            //����滮���
            GHBGyear = cobGHBGYear.SelectedItem.ToString();
        }

       
        
    }
}