using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Itop.Client.Forms
{
	/// <summary>
	/// userOption2 的摘要说明。
	/// </summary>
	public class userOption2 : DevExpress.XtraEditors.XtraUserControl
	{
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public userOption2()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化

		}

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxEdit2 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxEdit3 = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit3.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(32, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "外观";
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.EditValue = "";
            this.comboBoxEdit1.Location = new System.Drawing.Point(88, 21);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit1.Size = new System.Drawing.Size(136, 23);
            this.comboBoxEdit1.TabIndex = 4;
            this.comboBoxEdit1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(32, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "皮肤";
            // 
            // comboBoxEdit2
            // 
            this.comboBoxEdit2.EditValue = "";
            this.comboBoxEdit2.Location = new System.Drawing.Point(88, 56);
            this.comboBoxEdit2.Name = "comboBoxEdit2";
            this.comboBoxEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit2.Size = new System.Drawing.Size(136, 23);
            this.comboBoxEdit2.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(32, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "XP风格";
            // 
            // comboBoxEdit3
            // 
            this.comboBoxEdit3.EditValue = "";
            this.comboBoxEdit3.Location = new System.Drawing.Point(88, 91);
            this.comboBoxEdit3.Name = "comboBoxEdit3";
            this.comboBoxEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit3.Properties.Items.AddRange(new object[] {
            "True",
            "False"});
            this.comboBoxEdit3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit3.Size = new System.Drawing.Size(136, 23);
            this.comboBoxEdit3.TabIndex = 8;
            this.comboBoxEdit3.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit3_SelectedIndexChanged);
            // 
            // userOption2
            // 
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxEdit3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxEdit1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxEdit2);
            this.Name = "userOption2";
            this.Size = new System.Drawing.Size(272, 232);
            this.Load += new System.EventHandler(this.userOption2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit3.Properties)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void userOption2_Load(object sender, System.EventArgs e)
		{	
			
			foreach(string s in Enum.GetNames(typeof(DevExpress.LookAndFeel.LookAndFeelStyle)))
			{
				comboBoxEdit1.Properties.Items.Add(s);
			}
			foreach(DevExpress.Skins.SkinContainer cnt in DevExpress.Skins.SkinManager.Default.Skins)
			{
				comboBoxEdit2.Properties.Items.Add(cnt.SkinName);
			}
			comboBoxEdit1.Text=DevExpress.LookAndFeel.UserLookAndFeel.Default.Style.ToString();
			comboBoxEdit2.Text=DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName;
			comboBoxEdit3.Text=DevExpress.LookAndFeel.UserLookAndFeel.Default.UseWindowsXPTheme.ToString();
			this.comboBoxEdit2.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit2_SelectedIndexChanged);

		
		}
		private System.Windows.Forms.Label label1;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
		private System.Windows.Forms.Label label2;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit2;
		private System.Windows.Forms.Label label3;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit3;
       

		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            string skinName = "";
            if (comboBoxEdit1.SelectedIndex < 0) return;
            skinName = comboBoxEdit1.Text;
            DevExpress.LookAndFeel.LookAndFeelStyle style;
            style = (DevExpress.LookAndFeel.LookAndFeelStyle)Enum.Parse(typeof(DevExpress.LookAndFeel.LookAndFeelStyle), skinName, false);
            DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = style;
		}

		private void comboBoxEdit2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            string skinName = "";
            if (comboBoxEdit2.SelectedIndex < 0) return;
            skinName = comboBoxEdit2.Text;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);
            comboBoxEdit1.Text = "Skin";
		}

		private void comboBoxEdit3_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            bool isXp = bool.Parse(comboBoxEdit3.Text);

            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetStyle(DevExpress.LookAndFeel.UserLookAndFeel.Default.Style, isXp, false, comboBoxEdit2.Text);
		
		}
	}
}
