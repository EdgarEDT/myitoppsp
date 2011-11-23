using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Common;
using Itop.Domain.RightManager;
using Itop.Server.Interface;

namespace Itop.Client.Base
{
    public partial class FormModuleBase : FormBase
    {
        public ImageList il = new ImageList();

        public FormModuleBase()
        {
            InitializeComponent();
            
        }


        private void SetRightState()
        {
            il.ImageSize = new System.Drawing.Size(24, 24);
            il.ColorDepth = ColorDepth.Depth32Bit;


            il.Images.Add(Itop.Client.Resources.ImageListRes.GetImage("添加"));
            il.Images.Add(Itop.Client.Resources.ImageListRes.GetImage("修改"));
            il.Images.Add(Itop.Client.Resources.ImageListRes.GetImage("删除"));
            il.Images.Add(Itop.Client.Resources.ImageListRes.GetImage("打印"));
            il.Images.Add(Itop.Client.Resources.ImageListRes.GetImage("关闭"));
            barManager.Images = il;

            barAdd.ImageIndex = 0;
            barEdit.ImageIndex = 1;
            barDel.ImageIndex = 2;
            barPrint.ImageIndex = 3;
            barClose.ImageIndex = 4;




            barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barQuery.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;


            
            if (AddRight)
                barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        
            if (EditRight)
                barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        
            if (DeleteRight)
                barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        

        
            if (PrintRight)
                barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        
        }

        private void FormModuleBase_Load(object sender, EventArgs e)
        {
            SetRightState();
        }

        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Add();
        }

        private void barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        private void barDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Del();
        }

        private void barQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query();
        }

        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Print();
        }

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


        protected virtual void Add() { }
        protected virtual void Edit() { }
        protected virtual void Del() { }
        protected virtual void Query() { }
        protected virtual void Print() { }

        


    }
}