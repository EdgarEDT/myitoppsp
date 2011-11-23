
namespace ItopVector.Tools
{
	partial class FrmLineList1Dialog
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		/// </summary>
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
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.vGridControl = new DevExpress.XtraVerticalGrid.VGridControl();
            this.ItemTextEditUID = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditLineEleID = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditLineName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditLength = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditPointNum = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditTurnAngle = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditCoefficient = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditLength2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditcol1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditcol2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditcol3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditcol4 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.rowUID = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLineEleID = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLineName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLength = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowPointNum = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowTurnAngle = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowCoefficient = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLength2 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowcol1 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowcol2 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowcol3 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowcol4 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditUID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditLineEleID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditLineName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditPointNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditTurnAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditCoefficient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditLength2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditcol1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditcol2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditcol3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditcol4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(375, 309);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(283, 309);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // vGridControl
            // 
            this.vGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vGridControl.Location = new System.Drawing.Point(2, 2);
            this.vGridControl.Name = "vGridControl";
            this.vGridControl.RecordWidth = 292;
            this.vGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ItemTextEditUID,
            this.ItemTextEditLineEleID,
            this.ItemTextEditLineName,
            this.ItemTextEditLength,
            this.ItemTextEditPointNum,
            this.ItemTextEditTurnAngle,
            this.ItemTextEditCoefficient,
            this.ItemTextEditLength2,
            this.ItemTextEditcol1,
            this.ItemTextEditcol2,
            this.ItemTextEditcol3,
            this.ItemTextEditcol4,
            this.repositoryItemMemoEdit1,
            this.repositoryItemSpinEdit1});
            this.vGridControl.RowHeaderWidth = 172;
            this.vGridControl.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowUID,
            this.rowLineEleID,
            this.rowLineName,
            this.rowLength,
            this.rowPointNum,
            this.rowTurnAngle,
            this.rowCoefficient,
            this.rowLength2,
            this.rowcol4,
            this.rowcol1,
            this.rowcol2,
            this.rowcol3});
            this.vGridControl.Size = new System.Drawing.Size(475, 301);
            this.vGridControl.TabIndex = 0;
            // 
            // ItemTextEditUID
            // 
            this.ItemTextEditUID.AutoHeight = false;
            this.ItemTextEditUID.MaxLength = 50;
            this.ItemTextEditUID.Name = "ItemTextEditUID";
            // 
            // ItemTextEditLineEleID
            // 
            this.ItemTextEditLineEleID.AutoHeight = false;
            this.ItemTextEditLineEleID.MaxLength = 50;
            this.ItemTextEditLineEleID.Name = "ItemTextEditLineEleID";
            // 
            // ItemTextEditLineName
            // 
            this.ItemTextEditLineName.AutoHeight = false;
            this.ItemTextEditLineName.MaxLength = 50;
            this.ItemTextEditLineName.Name = "ItemTextEditLineName";
            // 
            // ItemTextEditLength
            // 
            this.ItemTextEditLength.AutoHeight = false;
            this.ItemTextEditLength.DisplayFormat.FormatString = "n2";
            this.ItemTextEditLength.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditLength.EditFormat.FormatString = "n2";
            this.ItemTextEditLength.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditLength.Mask.EditMask = "n2";
            this.ItemTextEditLength.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.ItemTextEditLength.Name = "ItemTextEditLength";
            // 
            // ItemTextEditPointNum
            // 
            this.ItemTextEditPointNum.AutoHeight = false;
            this.ItemTextEditPointNum.DisplayFormat.FormatString = "n2";
            this.ItemTextEditPointNum.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditPointNum.EditFormat.FormatString = "n2";
            this.ItemTextEditPointNum.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditPointNum.Mask.EditMask = "n2";
            this.ItemTextEditPointNum.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.ItemTextEditPointNum.Name = "ItemTextEditPointNum";
            // 
            // ItemTextEditTurnAngle
            // 
            this.ItemTextEditTurnAngle.AutoHeight = false;
            this.ItemTextEditTurnAngle.MaxLength = 4000;
            this.ItemTextEditTurnAngle.Name = "ItemTextEditTurnAngle";
            // 
            // ItemTextEditCoefficient
            // 
            this.ItemTextEditCoefficient.AutoHeight = false;
            this.ItemTextEditCoefficient.DisplayFormat.FormatString = "n2";
            this.ItemTextEditCoefficient.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditCoefficient.EditFormat.FormatString = "n2";
            this.ItemTextEditCoefficient.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditCoefficient.Mask.EditMask = "n2";
            this.ItemTextEditCoefficient.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.ItemTextEditCoefficient.Name = "ItemTextEditCoefficient";
            // 
            // ItemTextEditLength2
            // 
            this.ItemTextEditLength2.AutoHeight = false;
            this.ItemTextEditLength2.DisplayFormat.FormatString = "n2";
            this.ItemTextEditLength2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditLength2.EditFormat.FormatString = "n2";
            this.ItemTextEditLength2.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditLength2.Mask.EditMask = "n2";
            this.ItemTextEditLength2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.ItemTextEditLength2.Name = "ItemTextEditLength2";
            // 
            // ItemTextEditcol1
            // 
            this.ItemTextEditcol1.AutoHeight = false;
            this.ItemTextEditcol1.MaxLength = 50;
            this.ItemTextEditcol1.Name = "ItemTextEditcol1";
            // 
            // ItemTextEditcol2
            // 
            this.ItemTextEditcol2.AutoHeight = false;
            this.ItemTextEditcol2.MaxLength = 50;
            this.ItemTextEditcol2.Name = "ItemTextEditcol2";
            // 
            // ItemTextEditcol3
            // 
            this.ItemTextEditcol3.AutoHeight = false;
            this.ItemTextEditcol3.MaxLength = 50;
            this.ItemTextEditcol3.Name = "ItemTextEditcol3";
            // 
            // ItemTextEditcol4
            // 
            this.ItemTextEditcol4.AutoHeight = false;
            this.ItemTextEditcol4.MaxLength = 50;
            this.ItemTextEditcol4.Name = "ItemTextEditcol4";
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // rowUID
            // 
            this.rowUID.Height = 25;
            this.rowUID.Name = "rowUID";
            this.rowUID.Properties.FieldName = "UID";
            this.rowUID.Properties.ImageIndex = 25;
            this.rowUID.Properties.RowEdit = this.ItemTextEditUID;
            this.rowUID.Visible = false;
            // 
            // rowLineEleID
            // 
            this.rowLineEleID.Height = 25;
            this.rowLineEleID.Name = "rowLineEleID";
            this.rowLineEleID.Properties.FieldName = "LineEleID";
            this.rowLineEleID.Properties.ImageIndex = 25;
            this.rowLineEleID.Properties.RowEdit = this.ItemTextEditLineEleID;
            this.rowLineEleID.Visible = false;
            // 
            // rowLineName
            // 
            this.rowLineName.Height = 25;
            this.rowLineName.Name = "rowLineName";
            this.rowLineName.Properties.Caption = "方案名称";
            this.rowLineName.Properties.FieldName = "LineName";
            this.rowLineName.Properties.ImageIndex = 25;
            this.rowLineName.Properties.RowEdit = this.ItemTextEditLineName;
            // 
            // rowLength
            // 
            this.rowLength.Height = 22;
            this.rowLength.Name = "rowLength";
            this.rowLength.Properties.Caption = "线路总长";
            this.rowLength.Properties.FieldName = "Length";
            this.rowLength.Properties.ImageIndex = 25;
            this.rowLength.Properties.RowEdit = this.ItemTextEditLength;
            // 
            // rowPointNum
            // 
            this.rowPointNum.Height = 25;
            this.rowPointNum.Name = "rowPointNum";
            this.rowPointNum.Properties.Caption = "转角总数";
            this.rowPointNum.Properties.FieldName = "PointNum";
            this.rowPointNum.Properties.ImageIndex = 25;
            this.rowPointNum.Properties.RowEdit = this.repositoryItemSpinEdit1;
            // 
            // rowTurnAngle
            // 
            this.rowTurnAngle.Height = 90;
            this.rowTurnAngle.Name = "rowTurnAngle";
            this.rowTurnAngle.Properties.Caption = "转角度数";
            this.rowTurnAngle.Properties.FieldName = "TurnAngle";
            this.rowTurnAngle.Properties.ImageIndex = 25;
            this.rowTurnAngle.Properties.RowEdit = this.repositoryItemMemoEdit1;
            // 
            // rowCoefficient
            // 
            this.rowCoefficient.Height = 25;
            this.rowCoefficient.Name = "rowCoefficient";
            this.rowCoefficient.Properties.Caption = "转角系数";
            this.rowCoefficient.Properties.FieldName = "Coefficient";
            this.rowCoefficient.Properties.ImageIndex = 25;
            this.rowCoefficient.Properties.RowEdit = this.ItemTextEditCoefficient;
            // 
            // rowLength2
            // 
            this.rowLength2.Height = 25;
            this.rowLength2.Name = "rowLength2";
            this.rowLength2.Properties.Caption = "考虑转角系数后总长";
            this.rowLength2.Properties.FieldName = "Length2";
            this.rowLength2.Properties.ImageIndex = 25;
            this.rowLength2.Properties.RowEdit = this.ItemTextEditLength2;
            // 
            // rowcol1
            // 
            this.rowcol1.Height = 25;
            this.rowcol1.Name = "rowcol1";
            this.rowcol1.Properties.FieldName = "col1";
            this.rowcol1.Properties.ImageIndex = 25;
            this.rowcol1.Properties.RowEdit = this.ItemTextEditcol1;
            this.rowcol1.Visible = false;
            // 
            // rowcol2
            // 
            this.rowcol2.Height = 25;
            this.rowcol2.Name = "rowcol2";
            this.rowcol2.Properties.Caption = "终点";
            this.rowcol2.Properties.FieldName = "col2";
            this.rowcol2.Properties.ImageIndex = 25;
            this.rowcol2.Properties.RowEdit = this.ItemTextEditcol2;
            // 
            // rowcol3
            // 
            this.rowcol3.Height = 25;
            this.rowcol3.Name = "rowcol3";
            this.rowcol3.Properties.Caption = "线路总投资";
            this.rowcol3.Properties.FieldName = "col3";
            this.rowcol3.Properties.ImageIndex = 25;
            this.rowcol3.Properties.RowEdit = this.ItemTextEditcol3;
            // 
            // rowcol4
            // 
            this.rowcol4.Height = 25;
            this.rowcol4.Name = "rowcol4";
            this.rowcol4.Properties.Caption = "起点";
            this.rowcol4.Properties.FieldName = "col4";
            this.rowcol4.Properties.ImageIndex = 25;
            this.rowcol4.Properties.RowEdit = this.ItemTextEditcol4;
            // 
            // FrmLineList1Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 342);
            this.Controls.Add(this.vGridControl);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLineList1Dialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "线路信息";
            this.Load += new System.EventHandler(this.FrmLineList1Dialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditUID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditLineEleID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditLineName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditPointNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditTurnAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditCoefficient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditLength2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditcol1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditcol2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditcol3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditcol4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraVerticalGrid.VGridControl vGridControl;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditUID;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditLineEleID;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditLineName;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditLength;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditPointNum;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditTurnAngle;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditCoefficient;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditLength2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditcol1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditcol2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditcol3;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditcol4;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowUID;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLineEleID;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLineName;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLength;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowPointNum;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowTurnAngle;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowCoefficient;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLength2;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowcol1;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowcol2;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowcol3;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowcol4;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;


    }
}
