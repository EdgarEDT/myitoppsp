using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ItopVector.Core;
using ItopVector.Core.Figure;
using DevExpress.XtraEditors.Controls;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmSelLayer : FormBase
    {
        public ArrayList list=new ArrayList();
        public ArrayList list2=new ArrayList();
        public frmSelLayer()
        {
            InitializeComponent();
        }
        public void InitData()
        {
            for (int i = 0; i < list.Count;i++ )
            {
               CheckedListBoxItem item = new DevExpress.XtraEditors.Controls.CheckedListBoxItem(((Layer)list[i]).Label);
                ckLayer.Items.Add(item);
            }
        }

        private void frmSelLayer_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            foreach (CheckedListBoxItem ck in ckLayer.CheckedItems)
            {
                list2.Add(ck.Value);
            }
        }
    }
}