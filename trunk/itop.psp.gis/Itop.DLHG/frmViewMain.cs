using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
using ItopVector.Tools;
using Itop.Domain.Graphics;
using Itop.Domain.RightManager;

namespace Itop.DLGH
{
    public partial class frmViewMain : FormModuleBase
    {

        public void A()
        {
            frmViewMain a = new frmViewMain();
            a.Show();
        }
        public frmViewMain()
        {
           
            InitializeComponent();
            
            //ctrlFileManager1.OnOpenSvgDocument += new ItopVector.Tools.OnOpenDocumenthandler(ctrlFileManager1_OnOpenSvgDocument);
        }

        void ctrlFileManager1_OnOpenSvgDocument(object sender, string _svgUid)
        {
            ctrlSvgView1.OpenFromDatabase(_svgUid);
           
        }
        public void Init(bool show)
        {
            btshow = show;
        }
        public void InitData()
        {

        }
        protected override void Add()
        {
            
            //if (ctrlFileManager1.CurTreeNode.ImageIndex == 8)
            //{
            //    ctrlFileManager1.AddSvgFolder();
            //}
            //if (ctrlFileManager1.CurTreeNode.ImageIndex == 9)
            //{
            //    ctrlFileManager1.AddSvgFile();
            //}
        }
        protected override void Edit()
        {
            if (!EditRight)
            {
                MessageBox.Show("您没有此权限。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
           // ctrlFileManager1.ReName();
        }
        protected override void Del()
        {
            if (!DeleteRight)
            {
                MessageBox.Show("您没有此权限。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //if (ctrlFileManager1.CurTreeNode.ImageIndex == 9)
            //{
            //    SVGFILE svg = (SVGFILE)ctrlFileManager1.CurTreeNode.Tag;
            //    if (svg.SUID == ctrlSvgView1.svgDocument.SvgdataUid)
            //    {
            //        MessageBox.Show("当前文件正在使用中，不能删除。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }
            //}
            //ctrlFileManager1.DelTreeNode();
        }
        protected override void Print()
        {
            if (!PrintRight)
            {
                MessageBox.Show("您没有此权限。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ctrlSvgView1.Print();
        }
        private void frmViewMain_Load(object sender, EventArgs e)
        {
            barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barQuery.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (btshow)
            {
                bar.InsertItem(bar.ItemLinks[0], barButtonItem1);
            }
            ctrlSvgView1.EditRight = EditRight;
            //ctrlFileManager1.Add = AddRight;
            //ctrlFileManager1.Edit = EditRight;
            //ctrlFileManager1.Del = DeleteRight;
            bar.InsertItem(bar.ItemLinks[0], barButtonItem2);
            bar.InsertItem(bar.ItemLinks[1], barButtonItem3);
            ctrlSvgView1.OpenFromDatabase("26474eb6-cd92-4e84-a579-2f33946acf1a");
        }

        private void frmViewMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ctrlSvgView1.CloseInfo();
            //Application.UserAppDataPath = Application.StartupPath;
        }
        public Image ClipScreen()
        {
           return ctrlSvgView1.ClipScreen();
          
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            img=ClipScreen();
            this.Close();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //ctrlFileManager1.Open();
        }
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //ctrlFileManager1.Copy();
        }
        public Image img;
        bool btshow = false;
        
    }
}