using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmOutput : FormBase
    {
        public FrmOutput()
        {
            InitializeComponent();
        }
        private IList<string> column,volLev=new List<string>(),area;
        public IList<string> Column
        {
            get { return column; }
            set { column = value; }
        }
        public IList<string> VolLev
        {
            get { return volLev; }
            set { volLev = value; }
        }
        public IList<string> Area
        {
            get { return area; }
            set { area = value; }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ReturnRst();
            DialogResult = DialogResult.OK;
        }

        private void FrmOutput_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < column.Count; i++)
            {
                checkedListBoxControl1.Items.Add(column[i], true);
            }
            for (int i = 0; i < area.Count; i++)
            {
                checkedListBoxControl3.Items.Add(area[i], true);
            }
        }

        public void ReturnRst()
        {
            column.Clear(); volLev.Clear(); area.Clear();
            for (int i = 0; i < checkedListBoxControl1.Items.Count; i++)
            {
                if(checkedListBoxControl1.Items[i].CheckState!=CheckState.Checked)
                    column.Add(checkedListBoxControl1.Items[i].Value.ToString());
            }
            for (int j = 0; j < checkedListBoxControl2.Items.Count; j++)
            {
                if (checkedListBoxControl2.Items[j].CheckState == CheckState.Checked)
                {
                    string temp = checkedListBoxControl2.Items[j].Value.ToString();
                    temp = temp.Substring(0, temp.IndexOf('K'));
                    volLev.Add(temp);
                }
            }
            for (int i = 0; i < checkedListBoxControl3.Items.Count; i++)
            {
                if (checkedListBoxControl3.Items[i].CheckState == CheckState.Checked)
                    area.Add(checkedListBoxControl3.Items[i].Value.ToString());
            }
        }
    }
}