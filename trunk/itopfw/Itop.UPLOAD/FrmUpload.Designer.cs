namespace Itop.UPLOAD {
    partial class FrmUpload {
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btOpenFile = new System.Windows.Forms.Button();
            this.btUp = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btRetrieve = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.btSaveConfig = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(10, 41);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(486, 330);
            this.dataGridView1.TabIndex = 17;
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            // 
            // btOpenFile
            // 
            this.btOpenFile.Location = new System.Drawing.Point(255, 12);
            this.btOpenFile.Name = "btOpenFile";
            this.btOpenFile.Size = new System.Drawing.Size(70, 23);
            this.btOpenFile.TabIndex = 18;
            this.btOpenFile.Text = "添加...";
            this.btOpenFile.UseVisualStyleBackColor = true;
            this.btOpenFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // btUp
            // 
            this.btUp.Location = new System.Drawing.Point(87, 12);
            this.btUp.Name = "btUp";
            this.btUp.Size = new System.Drawing.Size(70, 23);
            this.btUp.TabIndex = 18;
            this.btUp.Text = "上传";
            this.btUp.UseVisualStyleBackColor = true;
            this.btUp.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 19;
            this.label1.Visible = false;
            // 
            // btRetrieve
            // 
            this.btRetrieve.Location = new System.Drawing.Point(12, 12);
            this.btRetrieve.Name = "btRetrieve";
            this.btRetrieve.Size = new System.Drawing.Size(70, 23);
            this.btRetrieve.TabIndex = 18;
            this.btRetrieve.Text = "检索";
            this.btRetrieve.UseVisualStyleBackColor = true;
            this.btRetrieve.Click += new System.EventHandler(this.button3_Click);
            // 
            // btDelete
            // 
            this.btDelete.Location = new System.Drawing.Point(166, 12);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(70, 23);
            this.btDelete.TabIndex = 18;
            this.btDelete.Text = "删除";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.button4_Click);
            // 
            // btSaveConfig
            // 
            this.btSaveConfig.Location = new System.Drawing.Point(349, 12);
            this.btSaveConfig.Name = "btSaveConfig";
            this.btSaveConfig.Size = new System.Drawing.Size(91, 23);
            this.btSaveConfig.TabIndex = 18;
            this.btSaveConfig.Text = "服务器设置";
            this.btSaveConfig.UseVisualStyleBackColor = true;
            this.btSaveConfig.Click += new System.EventHandler(this.btSaveConfig_Click);
            // 
            // FrmUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 383);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btSaveConfig);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.btRetrieve);
            this.Controls.Add(this.btUp);
            this.Controls.Add(this.btOpenFile);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FrmUpload";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "智能上传更新";
            this.Load += new System.EventHandler(this.FrmUpload_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btOpenFile;
        private System.Windows.Forms.Button btUp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btRetrieve;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btSaveConfig;
    }
}

