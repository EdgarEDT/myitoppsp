using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Resources;
using ItopVector.Resource;
using Itop.Client.Base;
namespace Itop.CADLib
{
	//public delegate void MatrixChangedEventHandler(object sender, float scalex, float scaley, float rotate, float skewx, float skewy);
	
	internal delegate void RefleshStatusEventHandler(FlashWindow flash);
	internal class FlashWindow : FormBase
	{
		private System.Windows.Forms.Label lbStatus;
        private IContainer components;

		public RefleshStatusEventHandler OnRefleshStatus;

		// Methods
		public FlashWindow()
		{
			this.components = null;
			this.InitializeComponent();		
			base.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
		}
		
		private void button1_Click(object sender, EventArgs e)
		{
			base.Close();
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
        this.lbStatus = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // lbStatus
        // 
        this.lbStatus.Location = new System.Drawing.Point(40, 32);
        this.lbStatus.Name = "lbStatus";
        this.lbStatus.Size = new System.Drawing.Size(344, 23);
        this.lbStatus.TabIndex = 0;
        // 
        // FlashWindow
        // 
        this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
        this.ClientSize = new System.Drawing.Size(400, 93);
        this.ControlBox = false;
        this.Controls.Add(this.lbStatus);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "FlashWindow";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "ItopVector";
        this.Paint += new System.Windows.Forms.PaintEventHandler(this.FlashWindow_Paint);
        this.Load += new System.EventHandler(this.FlashWindow_Load);
        this.ResumeLayout(false);

		}

		public void InvadiateImage(){}

		private void FlashWindow_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawRectangle(Pens.Blue,0,0,this.Width-1,this.Height-1);
			//base.OnPaint(e);
		}

		private void FlashWindow_Load(object sender, System.EventArgs e)
		{		
		}
		internal void RefleshStatus(string text) 
		{
			this.lbStatus.Text = text;
			base.Invalidate();
			base.Update();
		}
		internal void SplashData() 
		 {
			 base.Show();
			 base.BringToFront();
			 if (OnRefleshStatus != null) 
			 {
				OnRefleshStatus(this);
			 }
			 this.lbStatus.Text = string.Empty;
			 base.Hide();
		 }
		public void SetText(string str)
		{
			this.lbStatus.Text=str;
			this.Refresh();
		}				
	}
}
