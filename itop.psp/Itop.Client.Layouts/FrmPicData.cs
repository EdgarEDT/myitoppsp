using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Client.Base;
namespace Itop.Client.Layouts
{
    public partial class FrmPicData : FormBase
    {
        public FrmPicData()
        {
            InitializeComponent();
        }


        string title = "";
        string unit = "";

        public string Title
        {
            get { return title; }
        }

        public string Unit
        {
            get { return unit; }
        }

        string linename = "";


        DevExpress.XtraGrid.GridControl gcontrol = null;
        public DevExpress.XtraGrid.GridControl Gcontrol
        {
            get { return gcontrol; }
        }

        private void FrmPicData_Load(object sender, EventArgs e)
        {
           

            InitTreeData();

            //this.ctrlFileManager1.treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);
        }

        void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            InitTreeData();
        }


        private void InitTreeData()
        {
                  
            //string str = "26474eb6-cd92-4e84-a579-2f33946acf1a";  т╜ох
            string str = "c5ec3bc7-9706-4cbd-9b8b-632d3606f933";
            //if (ctrlFileManager1.treeView1.SelectedNode == null)
            //    return;
            //if (ctrlFileManager1.treeView1.SelectedNode.ImageIndex == 9)
            //{
                //linename = ((SVGFILE)ctrlFileManager1.treeView1.SelectedNode.Tag).FILENAME;
                //str = ((SVGFILE)ctrlFileManager1.treeView1.SelectedNode.Tag).SUID;
                this.ctrlLineProperty1.InitData(str);
                this.ctrlglebeProperty1.InitDataSub(str);
                this.ctrlglebeProperty2.InitData(str);
                this.ctrlSubStationProperty1.InitData(str);
            //}   
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            title = linename;
            gcontrol = this.ctrlLineProperty1.GridControl;
            this.DialogResult = DialogResult.OK;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 1;
            title = linename;
            gcontrol = this.ctrlglebeProperty1.GridControl;
            this.DialogResult = DialogResult.OK;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            title = linename;
            gcontrol = this.ctrlglebeProperty1.GridControl;
            gcontrol.Views[0].Dispose();
            this.DialogResult = DialogResult.OK;
        }

        private void barButtonItem3_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 2;
            title = linename;
            //gcontrol = this.ctrlglebeProperty2.gridControl;
            //gcontrol.Views[0].Dispose();
            this.DialogResult = DialogResult.OK;
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 3;
            title = linename;
            gcontrol = this.ctrlSubStationProperty1.GridControl;
            //gcontrol.Views[0].Dispose();
            this.DialogResult = DialogResult.OK;
        }
    }
} 