namespace ItopVector.Dialog
{
    using ItopVector.Core.Func;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class GridWindow : Form
    {
        // Methods
        public GridWindow()
        {
            this.components = null;
            this.size = (SizeF) new Size(10, 10);
            this.color = System.Drawing.Color.Gray;
            this.InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string text1 = this.txtWidth.Text.Trim();
            float single1 = this.size.Width;
            float single2 = this.size.Height;
            try
            {
                single1 = ItopVector.Core.Func.Number.parseToFloat(text1, null, SvgLengthDirection.Horizontal);
            }
            catch (Exception)
            {
                MessageBox.Show("无效的尺寸格式！");
            }
            try
            {
                single2 = ItopVector.Core.Func.Number.parseToFloat(this.txtHeight.Text.Trim(), null, SvgLengthDirection.Horizontal);
            }
            catch (Exception)
            {
                MessageBox.Show("无效的尺寸格式！");
            }
            if (((single1 < 5f) || (single1 > 100f)) || ((single2 < 5f) || (single2 > 100f)))
            {
                MessageBox.Show("尺寸需介和 100 之间！");
            }
            single1 = Math.Max(5f, Math.Min(100f, single1));
            single2 = Math.Max(5f, Math.Min(100f, single2));
            this.SizeValue = new SizeF(single1, single2);
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtHeight = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtWidth = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtHeight);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtWidth);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(168, 120);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "信  息";
			// 
			// txtHeight
			// 
			this.txtHeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtHeight.Location = new System.Drawing.Point(48, 56);
			this.txtHeight.Name = "txtHeight";
			this.txtHeight.Size = new System.Drawing.Size(96, 21);
			this.txtHeight.TabIndex = 3;
			this.txtHeight.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(24, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "高";
			// 
			// txtWidth
			// 
			this.txtWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtWidth.Location = new System.Drawing.Point(48, 24);
			this.txtWidth.Name = "txtWidth";
			this.txtWidth.Size = new System.Drawing.Size(96, 21);
			this.txtWidth.TabIndex = 1;
			this.txtWidth.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "长";
			// 
			// btnOk
			// 
			this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOk.Location = new System.Drawing.Point(184, 72);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 24);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "确  定";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancel.Location = new System.Drawing.Point(184, 104);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 24);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "取  消";
			// 
			// GridWindow
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(266, 135);
			this.ControlBox = false;
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.groupBox1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GridWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "设置网格";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}


        // Properties
        public System.Drawing.Color Color
        {
            get
            {
                return System.Drawing.Color.Empty;
            }
            set
            {
            }
        }

        public SizeF SizeValue
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
                this.txtWidth.Text = value.Width.ToString() + "px";
                this.txtHeight.Text = value.Height.ToString() + "px";
            }
        }


        // Fields
        private Button btnCancel;
        private Button btnOk;
        private System.Drawing.Color color;
        private Container components;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private SizeF size;
        private TextBox txtHeight;
        private TextBox txtWidth;
    }
}

