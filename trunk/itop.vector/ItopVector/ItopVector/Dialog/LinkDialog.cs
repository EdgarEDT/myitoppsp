using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ItopVector.Resource;

namespace ItopVector.Dialog
{
	public class LinkDialog : Form
    {
        // Methods
        public LinkDialog()
        {
            this.components = null;
            this.InitializeComponent();
            this.Text = DrawAreaConfig.GetLabelForName("linkdialog");
            this.lblink.Text = DrawAreaConfig.GetLabelForName("link");
            this.lbtarget.Text = DrawAreaConfig.GetLabelForName("target");
            this.btnOK.Text = LayoutManager.GetLabelForName("ok");
            this.btncancel.Text = LayoutManager.GetLabelForName("cancel");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblink = new Label();
            this.lbtarget = new Label();
            this.txtLink = new TextBox();
            this.txttarget = new TextBox();
            this.btncancel = new Button();
            this.btnOK = new Button();
            base.SuspendLayout();
            this.lblink.Location = new Point(8, 0x10);
            this.lblink.Name = "lblink";
            this.lblink.Size = new Size(0x40, 0x10);
            this.lblink.TabIndex = 0;
            this.lbtarget.Location = new Point(8, 0x30);
            this.lbtarget.Name = "lbtarget";
            this.lbtarget.Size = new Size(0x38, 0x17);
            this.lbtarget.TabIndex = 1;
            this.txtLink.Location = new Point(80, 0x10);
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new Size(0xa8, 0x15);
            this.txtLink.TabIndex = 2;
            this.txtLink.Text = "";
            this.txttarget.Location = new Point(80, 0x30);
            this.txttarget.Name = "txttarget";
            this.txttarget.Size = new Size(0xa8, 0x15);
            this.txttarget.TabIndex = 3;
            this.txttarget.Text = "";
            this.btncancel.Location = new Point(0xc0, 80);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new Size(0x38, 0x18);
            this.btncancel.TabIndex = 4;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x80, 80);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 5;
            base.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x102, 0x6f);
            Control[] controlArray1 = new Control[6] { this.btnOK, this.btncancel, this.txttarget, this.txtLink, this.lbtarget, this.lblink } ;
            base.Controls.AddRange(controlArray1);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "LinkDialog";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            base.ResumeLayout(false);
        }


        // Properties
        public string Link
        {
            get
            {
                return this.txtLink.Text.Trim();
            }
        }

        public string Target
        {
            get
            {
                return this.txttarget.Text.Trim();
            }
        }


        // Fields
        private Button btncancel;
        private Button btnOK;
        private Container components;
        private Label lblink;
        private Label lbtarget;
        private TextBox txtLink;
        private TextBox txttarget;
    }
}

