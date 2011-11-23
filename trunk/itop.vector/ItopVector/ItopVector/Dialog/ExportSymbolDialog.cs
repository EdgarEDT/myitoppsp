namespace ItopVector.Dialog
	
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.IO;
	using System.Text;
	using System.Xml;
	using System.Windows.Forms;
	using ItopVector.Core;
	using ItopVector.Core.Figure;
	using System.Drawing.Drawing2D;
	using ItopVector.Core.Interface.Figure;
	using ItopVector.Core.Document;


	internal class ExportSymbolDialog : Form
	{
		// Fields
		private TextBox txtID;
		private Button button2;
		private Button button4;
		private CheckBox chkShape;
		private SvgElement selectionSymbol;
		private bool firstload;
		private CheckBox checkBox1;
		private ItopVector.DrawArea.DrawArea  vectorcontrol;
		private Button button3;
		private CheckBox chkDocument;
		private Label label2;
		private GroupBox groupbox2;
		private SaveFileDialog saveFileDialog1;
		private CheckBox chkSelection;
		private Container components;
		private SvgElement allShape;
		private SvgElement selectionShape;
		private RichTextBox richTextBox1;
		private Button button1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private SvgElement allSymbol;
		private SvgElement curElement;
		private SvgDocument document;
		private ItopVector.Selector.SymbolSelector symbolSelector;
		// Methods 
		public ExportSymbolDialog(ItopVector.DrawArea.DrawArea  vectorcontrol, string filefilter)
		{
			this.firstload = true;
			this.components = null;
			this.vectorcontrol = null;
			this.allSymbol = null;
			this.allShape = null;
			this.selectionSymbol = null;
			this.selectionShape = null;
			this.document = null;
			this.InitializeComponent();
			this.saveFileDialog1.Filter = "SVG文件(*.svg)|*.svg";
			if (filefilter.Trim().Length > 0)
			{
				this.saveFileDialog1.Filter = this.saveFileDialog1.Filter + "|" + filefilter;
			}
			this.vectorcontrol = vectorcontrol;
			
			this.symbolSelector = vectorcontrol.SymbolSelector;
			if(this.symbolSelector!=null)
			{
				this.document = vectorcontrol.SymbolSelector.SymbolDoc;
			}
		}

		private void button1_click(object sender, EventArgs e)
		{
			this.firstload = false;
			SvgElement facbce1 = null;
			if (this.chkSelection.Checked)
			{//只包含选区
				if (this.selectionSymbol == null)
				{
					this.selectionSymbol = this.vectorcontrol.CreateSymbol(false, false, true);
				}
				facbce1 = this.selectionSymbol;
				if (this.chkShape.Checked)
				{//自定义形状
					if (this.selectionShape == null)
					{
						this.selectionShape = this.vectorcontrol.CreateSymbol(false, true, true);
					}
					facbce1 = this.selectionShape;
				}
			}
			else
			{//全部
				if (this.allSymbol == null)
				{
					this.allSymbol = this.vectorcontrol.CreateSymbol(true, false, true);
				}
				facbce1 = this.allSymbol;
				if (this.chkShape.Checked)
				{//自定义形状
					if (this.allShape == null)
					{
						this.allShape = this.vectorcontrol.CreateSymbol(true, true, true);
					}
					facbce1 = this.allShape;
				}
			}
			if (facbce1 != null)
			{
				for (int num1 = 0; num1 < facbce1.ChildNodes.Count; num1++)
				{
					if (facbce1.ChildNodes[num1] is SvgElement)
					{
						(facbce1.ChildNodes[num1] as SvgElement).SetAttribute("id", this.txtID.Text.Trim());
						if (!this.chkDocument.Checked)
						{
							facbce1 = facbce1.ChildNodes[num1] as SvgElement;
						}
						break;
					}
				}
			}
			string text1 = string.Empty;
			if (facbce1 != null)
			{
				this.curElement = (SvgElement)facbce1.Clone();
				text1 = facbce1.OuterXml;
				this.pictureBox1.Invalidate();
				
			}
			if (this.chkDocument.Checked)
			{
				text1 = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" + text1;
			}
			this.richTextBox1.Text = text1;
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			this.richTextBox1.WordWrap = this.checkBox1.Checked;
		}

		private void button4_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		#region 构造与析构
		private void InitializeComponent()
		{
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.groupbox2 = new System.Windows.Forms.GroupBox();
			this.chkShape = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.txtID = new System.Windows.Forms.TextBox();
			this.chkDocument = new System.Windows.Forms.CheckBox();
			this.chkSelection = new System.Windows.Forms.CheckBox();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupbox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// richTextBox1
			// 
			this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBox1.Location = new System.Drawing.Point(8, 8);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(358, 406);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = "";
			// 
			// groupbox2
			// 
			this.groupbox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupbox2.Controls.Add(this.chkShape);
			this.groupbox2.Controls.Add(this.label2);
			this.groupbox2.Controls.Add(this.button1);
			this.groupbox2.Controls.Add(this.txtID);
			this.groupbox2.Controls.Add(this.chkDocument);
			this.groupbox2.Controls.Add(this.chkSelection);
			this.groupbox2.Location = new System.Drawing.Point(374, 40);
			this.groupbox2.Name = "groupbox2";
			this.groupbox2.Size = new System.Drawing.Size(168, 152);
			this.groupbox2.TabIndex = 1;
			this.groupbox2.TabStop = false;
			this.groupbox2.Text = "导出";
			// 
			// chkShape
			// 
			this.chkShape.Location = new System.Drawing.Point(16, 120);
			this.chkShape.Name = "chkShape";
			this.chkShape.Size = new System.Drawing.Size(136, 24);
			this.chkShape.TabIndex = 6;
			this.chkShape.Text = "创建自定义形状";
			this.chkShape.Visible = false;
			this.chkShape.CheckedChanged += new System.EventHandler(this.button1_click);
			// 
			// label2
			// 
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label2.Location = new System.Drawing.Point(8, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(150, 2);
			this.label2.TabIndex = 5;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(96, 24);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(64, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "修改名称";
			this.button1.Click += new System.EventHandler(this.button1_click);
			// 
			// txtID
			// 
			this.txtID.Location = new System.Drawing.Point(8, 24);
			this.txtID.MaxLength = 20;
			this.txtID.Name = "txtID";
			this.txtID.Size = new System.Drawing.Size(80, 21);
			this.txtID.TabIndex = 2;
			this.txtID.Text = "Preset1";
			// 
			// chkDocument
			// 
			this.chkDocument.Location = new System.Drawing.Point(16, 96);
			this.chkDocument.Name = "chkDocument";
			this.chkDocument.Size = new System.Drawing.Size(136, 24);
			this.chkDocument.TabIndex = 1;
			this.chkDocument.Text = "产生完整文档代码";
			this.chkDocument.Visible = false;
			this.chkDocument.CheckedChanged += new System.EventHandler(this.button1_click);
			// 
			// chkSelection
			// 
			this.chkSelection.Checked = true;
			this.chkSelection.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSelection.Location = new System.Drawing.Point(16, 72);
			this.chkSelection.Name = "chkSelection";
			this.chkSelection.Size = new System.Drawing.Size(136, 24);
			this.chkSelection.TabIndex = 0;
			this.chkSelection.Text = "只包含选区";
			this.chkSelection.Visible = false;
			this.chkSelection.CheckedChanged += new System.EventHandler(this.button1_click);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(382, 326);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(152, 24);
			this.button2.TabIndex = 2;
			this.button2.Text = "保存到形状库(&A)";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button3.Location = new System.Drawing.Point(382, 358);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(152, 24);
			this.button3.TabIndex = 3;
			this.button3.Text = "保存到文件(&S)";
			this.button3.Visible = false;
			this.button3.Click += new System.EventHandler(this.button2_Click);
			// 
			// button4
			// 
			this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button4.Location = new System.Drawing.Point(382, 390);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(152, 24);
			this.button4.TabIndex = 4;
			this.button4.Text = "关　　　闭(&C)";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox1.Location = new System.Drawing.Point(382, 8);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(152, 24);
			this.checkBox1.TabIndex = 5;
			this.checkBox1.Text = "代码自动折行";
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.White;
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBox1.Location = new System.Drawing.Point(16, 16);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(136, 96);
			this.pictureBox1.TabIndex = 6;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.pictureBox1);
			this.groupBox1.Location = new System.Drawing.Point(374, 192);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(170, 128);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "预览";
			// 
			// ExportSymbolDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(552, 421);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.groupbox2);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.groupBox1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExportSymbolDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "导出图元";
			this.groupbox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		#endregion
		private void button2_Click(object sender, EventArgs e)
		{
			Button button1 = sender as Button;
			ItopVector.Core.Document.SvgDocument doc=this.document;
			string filepath=string.Empty;
			if(doc==null)
			{
				filepath=Application.StartupPath +@"\symbol\symbol.xml";
				if(!File.Exists(filepath))
				{
					filepath=string.Empty;
					OpenFileDialog opendlg =new OpenFileDialog();
					opendlg.Filter = "xml文件(*.xml)|*.xml";
					if (opendlg.ShowDialog(this) == DialogResult.OK)
					{
						filepath=opendlg.FileName;
					}
				}
				if(File.Exists(filepath))
				{

					try
					{
						doc= ItopVector.Selector.SymbolGroup.LoadSymbol(filepath);					
						
					}
					catch(Exception err)
					{
						MessageBox.Show(err.Message);
					}
				}
			}
			if(doc!=null)
			{
				XmlNode node=doc.SelectSingleNode("//*[@id='" + this.curElement.ID + "']");

				if(node!=null)
				{
					MessageBox.Show("已存在名为'"+this.curElement.ID+"'组或图元!","增加图元");
					return ;
				}
				
				SymbolGroupDialog dlg =new SymbolGroupDialog();
				dlg.SymbolDoc = doc;
				dlg.ToInsertElement =this.curElement;
				if(dlg.ShowDialog(this)==DialogResult.OK)
				{
					if(this.symbolSelector !=null)
					{					
						this.symbolSelector.AddSymbol(this.curElement as Symbol,dlg.SelectedElement.GetAttribute("id"));
					}
					else
					{
						dlg.SelectedElement.AppendChild(this.document.ImportNode( this.curElement,true));
					}
					doc.Save(doc.FilePath);
//					MessageBox.Show("保存成功，需要重新启动程序才有效。");
				}
			}
			
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			if (this.firstload && base.Visible)
			{
				this.button1_click(this.button1, EventArgs.Empty);
				this.pictureBox1.Invalidate();
			}
			base.OnVisibleChanged(e);
		}

		private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if(this.curElement == null)return;

			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

			using (Matrix matrix1 = new Matrix())
			{
				{
					IGraph shape1 = this.curElement as IGraph;
					if (shape1 != null)
					{
						Rectangle rectangle1 = new Rectangle(0,0,this.pictureBox1.Width,this.pictureBox1.Height);
						
						int num5 =2;
						Rectangle rectangle2 = new Rectangle(rectangle1.X + (2 * num5), rectangle1.Y + (2 * num5), rectangle1.Width - (4 * num5), rectangle1.Height - (4 * num5));
						
						GraphicsPath path1 = null;
						if (shape1 is Symbol)
						{
							path1 = (shape1 as Symbol).GPath;
						
						}
						if ((path1 != null) && (path1.PointCount > 1))
						{
							using (GraphicsPath path2 = (path1.Clone() as GraphicsPath))
							{
//								path2.Flatten();
								RectangleF ef1 = path2.GetBounds();
								if (ef1.Width == 0f)
								{
									ef1.Width = rectangle2.Width;
								}
								if (ef1.Height == 0f)
								{
									ef1.Height = rectangle2.Height;
								}
								if (!ef1.IsEmpty)
								{
									float single1 = ef1.X + (ef1.Width / 2f);
									float single2 = ef1.Y + (ef1.Height / 2f);
											
									matrix1.Translate((rectangle2.X + (((float) rectangle2.Width) / 2f)) - single1, (rectangle2.Y + (((float) rectangle2.Height) / 2f)) - single2);
									matrix1.Translate(single1, single2);
									float single3 = Math.Min((float) (((float) rectangle2.Height) / ef1.Width), (float) (((float) rectangle2.Height) / ef1.Height));
									matrix1.Scale(single3, single3);
									matrix1.Translate(-single1, -single2);
									if (shape1 is Symbol)
									{
										GraphicsContainer container1 =e.Graphics.BeginContainer();
										e.Graphics.Transform = matrix1;
										(shape1 as Symbol).Draw(e.Graphics, 0);
										e.Graphics.EndContainer(container1);
									}
								}
							}
						}
					}
				}
			}
		}
	}
}



