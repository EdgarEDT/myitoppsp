using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmList : FormBase
    {
        public ArrayList list = new ArrayList();
        public frmList()
        {
            InitializeComponent();
        }

        private void frmList_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < list.Count;i++ )
            {
                listBox1.Items.Add(list[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                return;
            }
            list.Remove(listBox1.Items[listBox1.SelectedIndex]);
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}