using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Itop.Domain.Graphics;

namespace Itop.TLPSP.DEVICE
{
    /// <summary>
    /// Summary description for frmNodeTreedlg.
    /// </summary>
    public class frmNodeTreedlg : Itop.Client.Base.FormBase
    {
        private TreeView treeView1;
        private SimpleButton simpleButton1;
        private SimpleButton simpleButton2;
        private PSPDEV pspdev;

        public PSPDEV Pspdev {
            get { return pspdev; }
            set { pspdev = value; }
        }
        
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public frmNodeTreedlg() {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(12, 12);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(320, 348);
            this.treeView1.TabIndex = 0;
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Location = new System.Drawing.Point(178, 366);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "确认";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(257, 366);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = "取消";
            // 
            // frmNodeTreedlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(344, 409);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.treeView1);
            this.Name = "frmNodeTreedlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择所在线路（支线）";
            this.ResumeLayout(false);

        }
        #endregion

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            initxl();
        }

        private void initxl() {
            string sql = " where RateVolt=" + pspdev.RateVolt + " and type='05' and ProjectID='" + Itop.Client.MIS.ProgUID + "'";
            IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", sql);
            
            foreach (PSPDEV dev in list) {
                TreeNode node = new TreeNode(dev.Name);
                node.Tag = dev;
                node.Nodes.Add("");
                treeView1.Nodes.Add(node);
            }
            pspdev = null;
        }
        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e) {
            if (e.Node.Nodes.Count != 1) {
                 return;
            }
            if (e.Node.Nodes[0].Text == "") {
                e.Node.Nodes.Clear();
                expand(e.Node);
            }
        }
        private void expand(TreeNode pnode) {
            if (pnode.Tag == null) return;
            PSPDEV pdev = pnode.Tag as PSPDEV;
            string sql = " where AreaID='" + pdev.SUID + "'  and ProjectID='" + Itop.Client.MIS.ProgUID + "'";
            IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", sql);
            foreach (PSPDEV dev in list) {
                TreeNode node = new TreeNode(dev.Name);
                node.Tag = dev;
                node.Nodes.Add("");
                pnode.Nodes.Add(node);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            if (treeView1.SelectedNode == null) return;
            TreeNode node = treeView1.SelectedNode;
            pspdev = node.Tag as PSPDEV;
            this.DialogResult = DialogResult.OK;
        }
    }
}

