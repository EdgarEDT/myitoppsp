namespace Itop.Client.Stutistics
{
    partial class FrmBurdenLine
    {
        /// <summary>
        /// 必需的设计器变量。

        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。

        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。

        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBurdenLine));
            Dundas.Charting.WinControl.ChartArea chartArea1 = new Dundas.Charting.WinControl.ChartArea();
            Dundas.Charting.WinControl.Legend legend1 = new Dundas.Charting.WinControl.Legend();
            Dundas.Charting.WinControl.Series series1 = new Dundas.Charting.WinControl.Series();
            Dundas.Charting.WinControl.Title title1 = new Dundas.Charting.WinControl.Title();
            Dundas.Charting.WinControl.ChartArea chartArea2 = new Dundas.Charting.WinControl.ChartArea();
            Dundas.Charting.WinControl.Legend legend2 = new Dundas.Charting.WinControl.Legend();
            Dundas.Charting.WinControl.Series series2 = new Dundas.Charting.WinControl.Series();
            Dundas.Charting.WinControl.Title title2 = new Dundas.Charting.WinControl.Title();
            this.ctrlBurdenLine1 = new Itop.Client.Stutistics.CtrlBurdenLine();
            this.barBurdenLine = new DevExpress.XtraBars.BarButtonItem();
            this.barMaxBurden = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.chart1 = new Dundas.Charting.WinControl.Chart();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.chart2 = new Dundas.Charting.WinControl.Chart();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
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
            // 
            // ctrlBurdenLine1
            // 
            this.ctrlBurdenLine1.AllowUpdate = true;
            this.ctrlBurdenLine1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlBurdenLine1.Location = new System.Drawing.Point(0, 34);
            this.ctrlBurdenLine1.Name = "ctrlBurdenLine1";
            this.ctrlBurdenLine1.PID = "";
            this.ctrlBurdenLine1.Size = new System.Drawing.Size(794, 200);
            this.ctrlBurdenLine1.TabIndex = 4;
            // 
            // barBurdenLine
            // 
            this.barBurdenLine.Caption = "典型日负荷曲线表";
            this.barBurdenLine.Id = 7;
            this.barBurdenLine.Name = "barBurdenLine";
            this.barBurdenLine.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBurdenLine_ItemClick);
            // 
            // barMaxBurden
            // 
            this.barMaxBurden.Caption = "最大负荷日曲线表";
            this.barMaxBurden.Id = 7;
            this.barMaxBurden.Name = "barMaxBurden";
            this.barMaxBurden.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barMaxBurden_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "导入Excel";
            this.barButtonItem1.Id = 7;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "导入Excel";
            this.barButtonItem2.Id = 7;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "保存最新样式";
            this.barButtonItem4.Id = 7;
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem4_ItemClick);
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "保存最新样式";
            this.barButtonItem5.Id = 7;
            this.barButtonItem5.Name = "barButtonItem5";
            this.barButtonItem5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem5_ItemClick);
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "保存最新样式";
            this.barButtonItem6.Id = 7;
            this.barButtonItem6.ImageIndex = 0;
            this.barButtonItem6.Name = "barButtonItem6";
            this.barButtonItem6.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem6_ItemClick);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl1.Location = new System.Drawing.Point(0, 234);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(794, 4);
            this.splitterControl1.TabIndex = 6;
            this.splitterControl1.TabStop = false;
            // 
            // chart1
            // 
            chartArea1.BorderColor = System.Drawing.Color.Empty;
            chartArea1.Name = "Default";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Default";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series1.BorderWidth = 2;
            series1.ChartType = "Spline";
            series1.MarkerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            series1.MarkerStyle = Dundas.Charting.WinControl.MarkerStyle.Circle;
            series1.Name = "Default";
            series1.ShadowOffset = 1;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(784, 142);
            this.chart1.TabIndex = 9;
            this.chart1.Text = "chart1";
            title1.Name = "Title1";
            this.chart1.Titles.Add(title1);
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.Caption = "导出数据";
            this.barButtonItem7.Id = 7;
            this.barButtonItem7.ImageIndex = 0;
            this.barButtonItem7.Name = "barButtonItem7";
            this.barButtonItem7.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem7_ItemClick);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(3, 3);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(788, 169);
            this.xtraTabControl1.TabIndex = 10;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            this.xtraTabControl1.Text = "xtraTabControl1";
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.checkBox1);
            this.xtraTabPage1.Controls.Add(this.chart1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(784, 142);
            this.xtraTabPage1.Text = "数据曲线";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.chart2);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(784, 142);
            this.xtraTabPage2.Text = "相对曲线";
            // 
            // chart2
            // 
            chartArea2.BorderColor = System.Drawing.Color.Empty;
            chartArea2.Name = "Default";
            this.chart2.ChartAreas.Add(chartArea2);
            this.chart2.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Default";
            this.chart2.Legends.Add(legend2);
            this.chart2.Location = new System.Drawing.Point(0, 0);
            this.chart2.Name = "chart2";
            series2.BorderWidth = 2;
            series2.ChartType = "Spline";
            series2.MarkerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            series2.MarkerStyle = Dundas.Charting.WinControl.MarkerStyle.Circle;
            series2.Name = "Default";
            series2.ShadowOffset = 1;
            this.chart2.Series.Add(series2);
            this.chart2.Size = new System.Drawing.Size(784, 142);
            this.chart2.TabIndex = 10;
            this.chart2.Text = "chart2";
            title2.Name = "Title1";
            this.chart2.Titles.Add(title2);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.xtraTabControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 238);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(794, 175);
            this.panelControl1.TabIndex = 11;
            this.panelControl1.Text = "panelControl1";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(726, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(60, 16);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "显示值";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // FrmBurdenLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 413);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.ctrlBurdenLine1);
            this.Name = "FrmBurdenLine";
            this.Text = "负荷曲线表";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmBurdenLine_Load);
            this.Controls.SetChildIndex(this.ctrlBurdenLine1, 0);
            this.Controls.SetChildIndex(this.splitterControl1, 0);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage1.PerformLayout();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }



        #endregion

        private CtrlBurdenLine ctrlBurdenLine1;
        private DevExpress.XtraBars.BarButtonItem barBurdenLine;
        private DevExpress.XtraBars.BarButtonItem barMaxBurden;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
   
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private Dundas.Charting.WinControl.Chart chart1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private Dundas.Charting.WinControl.Chart chart2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.CheckBox checkBox1;//保存
    }
}