﻿namespace Itop.TLPSP.DEVICE
{
    partial class frmDeviceManager_SH
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDeviceManager_SH));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.barButtonItemIn = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemOut = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDel = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemclose = new DevExpress.XtraBars.BarButtonItem();
            this.AllDele = new DevExpress.XtraBars.BarButtonItem();
            this.UpdateNumber = new DevExpress.XtraBars.BarButtonItem();
            this.barImportPsasp = new DevExpress.XtraBars.BarButtonItem();
            this.bardevicetemplate = new DevExpress.XtraBars.BarButtonItem();
            
            this.bdzStatic = new DevExpress.XtraBars.BarButtonItem();
            this.xlStatic = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.SuspendLayout();
            // 
            // il
            // 
            this.il.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.il.ImageSize = new System.Drawing.Size(24, 24);
            this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
            this.il.Images.SetKeyName(0, "");
            this.il.Images.SetKeyName(1, "");
            this.il.Images.SetKeyName(2, "");
            this.il.Images.SetKeyName(3, "");
            this.il.Images.SetKeyName(4, "");
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 34);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.treeList1);
            this.splitContainerControl1.Panel1.Text = "splitContainerControl1_Panel1";
            this.splitContainerControl1.Panel2.Text = "splitContainerControl1_Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(664, 354);
            this.splitContainerControl1.SplitterPosition = 144;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // treeList1
            // 
            this.treeList1.Appearance.FocusedCell.BackColor = System.Drawing.Color.SkyBlue;
            this.treeList1.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.SkyBlue;
            this.treeList1.Appearance.FocusedCell.BorderColor = System.Drawing.Color.Navy;
            this.treeList1.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeList1.Appearance.FocusedCell.Options.UseBorderColor = true;
            this.treeList1.Appearance.SelectedRow.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.treeList1.Appearance.SelectedRow.ForeColor = System.Drawing.SystemColors.Window;
            this.treeList1.Appearance.SelectedRow.Options.UseBackColor = true;
            this.treeList1.Appearance.SelectedRow.Options.UseForeColor = true;
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 0);
            this.treeList1.Name = "treeList1";
            this.treeList1.Size = new System.Drawing.Size(144, 354);
            this.treeList1.TabIndex = 0;
            this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
            this.treeList1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeList1_MouseClick);
            // 
            // barButtonItemIn
            // 
            this.barButtonItemIn.Caption = "导入";
            this.barButtonItemIn.Id = 8;
            this.barButtonItemIn.ImageIndex = 1;
            this.barButtonItemIn.Name = "barButtonItemIn";
            this.barButtonItemIn.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItemIn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemIn_ItemClick);
            // 
            // barButtonItemOut
            // 
            this.barButtonItemOut.Caption = "导出";
            this.barButtonItemOut.Id = 9;
            this.barButtonItemOut.ImageIndex = 0;
            this.barButtonItemOut.Name = "barButtonItemOut";
            this.barButtonItemOut.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItemOut.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemOut_ItemClick);
            // 
            // barButtonItemDel
            // 
            this.barButtonItemDel.Caption = "删除";
            this.barButtonItemDel.Id = 10;
            this.barButtonItemDel.ImageIndex = 2;
            this.barButtonItemDel.Name = "barButtonItemDel";
            this.barButtonItemDel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItemDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemDel_ItemClick);
            // 
            // barButtonItemclose
            // 
            this.barButtonItemclose.Caption = "关闭";
            this.barButtonItemclose.Id = 10;
            this.barButtonItemclose.ImageIndex = 4;
            this.barButtonItemclose.Name = "barButtonItemclose";
            this.barButtonItemclose.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItemclose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemclose_ItemClick);
            // 
            // AllDele
            // 
            this.AllDele.Caption = "全部删除";
            this.AllDele.Id = 7;
            this.AllDele.ImageIndex = 2;
            this.AllDele.Name = "AllDele";
            this.AllDele.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.AllDele.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.AllDele_ItemClick);
            // 
            // UpdateNumber
            // 
            this.UpdateNumber.Caption = "更新编号";
            this.UpdateNumber.Id = 8;
            this.UpdateNumber.ImageIndex = 0;
            this.UpdateNumber.Name = "UpdateNumber";
            this.UpdateNumber.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.UpdateNumber.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.UpdateNumber.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.UpdateNumber_ItemClick);
            // 
            // barImportPsasp
            // 
            this.barImportPsasp.Caption = "导入PSASP";
            this.barImportPsasp.Id = 7;
            this.barImportPsasp.ImageIndex = 1;
            this.barImportPsasp.Name = "barImportPsasp";
            this.barImportPsasp.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barImportPsasp.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barImportPsasp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barImportPsasp_ItemClick);
            // 
            // bardevicetemplate
            // 
            this.bardevicetemplate.Caption = "典型参数管理";
            this.bardevicetemplate.Id = 8;
            this.bardevicetemplate.ImageIndex = 1;
            this.bardevicetemplate.Name = "bardevicetemplate";
            this.bardevicetemplate.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bardevicetemplate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bardevicetemplate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bardevicetemplate_ItemClick);
            
              // 
            // bdzStatic
            // 
            this.bdzStatic.Caption = "变电站统计";
            this.bdzStatic.Id =10;
            this.bdzStatic.ImageIndex = 1;
            this.bdzStatic.Name = "bdzStatic";
            this.bdzStatic.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bdzStatic.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bdzStatic.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bdzStatic_ItemClick);
            // 
            // xlStatic
            // 
            this.xlStatic.Caption = "线路统计";
            this.xlStatic.Id = 9;
            this.xlStatic.ImageIndex = 1;
            this.xlStatic.Name = "xlStatic";
            this.xlStatic.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.xlStatic.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.xlStatic.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.xlStatic_ItemClick);
            // 
            // frmDeviceManager_SH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 388);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "frmDeviceManager_SH";
            this.Text = "frmDeviceManager";
            this.Controls.SetChildIndex(this.splitContainerControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemIn;
        private DevExpress.XtraBars.BarButtonItem barButtonItemOut;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDel;
        private DevExpress.XtraBars.BarButtonItem barButtonItemclose;
        private DevExpress.XtraBars.BarButtonItem AllDele;
        private DevExpress.XtraBars.BarButtonItem UpdateNumber;
        private DevExpress.XtraBars.BarButtonItem barImportPsasp;
        private DevExpress.XtraBars.BarButtonItem bardevicetemplate;
       
        private DevExpress.XtraBars.BarButtonItem bdzStatic;
        private DevExpress.XtraBars.BarButtonItem xlStatic;
    }
}