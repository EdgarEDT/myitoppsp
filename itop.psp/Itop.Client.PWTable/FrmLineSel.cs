using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Common;
using Itop.Domain.PWTable;
using System.Collections;
using Itop.Client.Base;
namespace Itop.Client.PWTable
{
    public partial class FrmLineSel : FormBase
    {
        private string sel_str = "";
        private string selfName = "";

        public string SelfName
        {
            get { return selfName; }
            set { selfName = value; }
        }


        public string Sel_str
        {
            get { return sel_str; }
            set { sel_str = value; }
        }

        public FrmLineSel()
        {
            InitializeComponent();
        }

        private void FrmLineSel_Load(object sender, EventArgs e)
        {
            PW_tb3a p = new PW_tb3a();
            p.col2= Itop.Client.MIS.ProgUID;
            IList<PW_tb3a> list = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aListName", p);
            ArrayList alist=new ArrayList();
            for (int i = 0; i < list.Count;i++ )
            {
                if (!alist.Contains(list[i].LineName) && list[i].LineName!=selfName)
                {
                    alist.Add(list[i].LineName);
                }
            }
            for (int j = 0; j < alist.Count; j++)
            {
                checkedListBox1.Items.Add(alist[j].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count;i++ )
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    sel_str = sel_str + checkedListBox1.Items[i].ToString() + ",";
                }
            }
            if(sel_str.Length>0){
                sel_str = sel_str.Substring(0, sel_str.Length - 1);
            }
           this.DialogResult = DialogResult.OK;
        }
    }
}