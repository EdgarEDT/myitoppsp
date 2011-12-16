namespace Itop.TLPsp.Graphical {
    partial class XtraPDrelfrm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraPDrelfrm));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.ucPddate1 = new Itop.TLPsp.Graphical.UcPddate();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ucPdtype1 = new Itop.TLPsp.Graphical.UcPdtype();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "保存.ico");
            this.imageList1.Images.SetKeyName(1, "打印.ico");
            this.imageList1.Images.SetKeyName(2, "删除.ico");
            this.imageList1.Images.SetKeyName(3, "添加下级.ico");
            this.imageList1.Images.SetKeyName(4, "新建.ico");
            this.imageList1.Images.SetKeyName(5, "修改.ico");
            this.imageList1.Images.SetKeyName(6, "添加同级.ico");
            this.imageList1.Images.SetKeyName(7, "审批.ico");
            this.imageList1.Images.SetKeyName(8, "审核.ico");
            this.imageList1.Images.SetKeyName(9, "关闭.ico");
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.ucPdtype1);
            this.splitContainerControl1.Panel1.Text = "splitContainerControl1_Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.ucPddate1);
            this.splitContainerControl1.Panel2.Text = "splitContainerControl1_Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(863, 273);
            this.splitContainerControl1.SplitterPosition = 241;
            this.splitContainerControl1.TabIndex = 9;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // ucPddate1
            // 
            this.ucPddate1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPddate1.Location = new System.Drawing.Point(0, 0);
            this.ucPddate1.Name = "ucPddate1";
            this.ucPddate1.ParentID = "";
            this.ucPddate1.ParentObj = ((Itop.Domain.Graphics.PDrelregion)(resources.GetObject("ucPddate1.ParentObj")));
            this.ucPddate1.Size = new System.Drawing.Size(616, 273);
            this.ucPddate1.TabIndex = 0;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Mask.EditMask = "###########.####";
            this.repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // ucPdtype1
            // 
            this.ucPdtype1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPdtype1.Location = new System.Drawing.Point(0, 0);
            this.ucPdtype1.Name = "ucPdtype1";
            this.ucPdtype1.Size = new System.Drawing.Size(241, 273);
            this.ucPdtype1.TabIndex = 0;
            // 
            // XtraPDrelfrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 273);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "XtraPDrelfrm";
            this.Text = "可靠性分析";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private UcPddate ucPddate1;
        private UcPdtype ucPdtype1;
    }
}