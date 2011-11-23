using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client;
using Itop.Client.Base;

namespace Itop.RightManager.UI {
    public partial class FrmResources : FormBase {
        public FrmResources() {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            //base.OnLoad(e);
            //ImageList imagelist =MIS.GetImageList(32);
            //this.listView1.LargeImageList = imagelist;
            //foreach (string key in imagelist.Images.Keys)
            //{
            //    ListViewItem item = new ListViewItem(key);
            //    item.ImageKey = key;
            //    listView1.Items.Add(item);
            //}

            base.OnLoad(e);
            ImageList imagelist = MIS.GetImageList(40,"");
            add_image(imagelist);

        }
        protected void add_image(ImageList templist)
        {
            tabControl1.Controls.Clear();
           int m=0;
            if (templist.Images.Count%20==0)
	        {
                m=templist.Images.Count/20;

	        }
            else
	        {
                m=templist.Images.Count/20;
                m=m+1;
	        }
           
            for (int i = 0; i < m; i++)
            {
                System.Windows.Forms.TabPage temptabPage = new TabPage();
                temptabPage.Text = "ตฺ" + (i + 1 )+ "าณ";
                
                
                ListView templistview=new ListView ();
                templistview.DoubleClick+=new EventHandler(templistview_DoubleClick);
                temptabPage.Controls.Add(templistview);
                templistview.Dock = System.Windows.Forms.DockStyle.Fill;
                templistview.LargeImageList = templist;
                for (int j = 0; j < 20; j++)
                {
                    if (templist.Images.Count <= i * 20 + j)
                    {
                        break;
                    }
                    ListViewItem item = new ListViewItem(templist.Images.Keys[i * 20 + j].ToString());
                    item.ImageKey = templist.Images.Keys[i * 20 + j].ToString();
                    templistview.Items.Add(item);
                }
                tabControl1.Controls.Add(temptabPage);
               
            }
           
        }

        void templistview_DoubleClick(object sender, EventArgs e)
        {
                ListViewItem item = ((ListView)sender).SelectedItems[0];
                selectedImageKey = item.Text;
                this.DialogResult = DialogResult.OK;
        }

        




        string selectedImageKey = string.Empty;

        public string SelectedImageKey {
            get { return selectedImageKey; }
            set { selectedImageKey = value; }
        }

        private void listView1_DoubleClick(object sender, EventArgs e) {
            if (this.listView1.SelectedItems.Count > 0) {
                ListViewItem item = this.listView1.SelectedItems[0];
                selectedImageKey = item.Text;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           
                string keyname=txtImageName.Text.Trim();
                ImageList imagelist = MIS.GetImageList(40, keyname);
                add_image(imagelist);
            
        }
    }
}