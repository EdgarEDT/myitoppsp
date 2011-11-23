using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace ItopVector.Selector
{
		
		public abstract class OutlookBar : UserControl
		{
			// Events
			public event EventHandler SelectedChanged;
            public event EventHandler Selected;
			// Methods
			public OutlookBar()
			{
				this.components = null;
				this.selectorGroups = new ArrayList();
				this.headHeight = 22;
				this.panelSize = new Size(48, 48);
				this.outlookSize = new Size(50, 50);
				this.shapePanel = null;
				this.currentGroup = null;
				this.itemHeight = 40;
				this.selectChanged = false;
				this.selectedIndex = -1;
				this.contentColor = ControlPaint.LightLight(Color.SkyBlue);
				this.selectedObject = null;
				this.dragBegin = false;
				this.ibDrag = false;
				this.toolTip = new ToolTip();
				this.showToolTip = false;
				this.titleColor = SystemColors.ControlLight;
				this.InitializeComponent();
				this.toolTip.AutoPopDelay = 0x1770;
				base.SetStyle(ControlStyles.DoubleBuffer | (ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint), true);
			}

			public void SelectIndex(int index)
			{
				if (index <0 )
				{
					this.selectedObject=null;
					this.selectedIndex = -1;
					this.shapePanel.Invalidate();
				}
				else
					this.SelectedPathIndexChanged(index);

			}
			protected Rectangle GetItemRect(int index)
			{
				int num1 = this.SelectedIndex;
				if ((this.currentGroup != null) && (index >= 0))
				{
					return new Rectangle(0, index * this.itemHeight, this.shapePanel.Width, this.itemHeight);
				}
				return Rectangle.Empty;
			}

			//绘制一组
			private void DrawHeaders(Graphics g, IOutlookBarBand var_symbol)
			{
				if (var_symbol != null)
				{
					RectangleF ef1 = (RectangleF) this.GetHeadRect(var_symbol);
					Color color1 = this.titleColor;
					Color color2 = ControlPaint.LightLight(color1);
					Color color3 = ControlPaint.Dark(color1);
					g.FillRectangle(new SolidBrush(color2), ef1);
					using (GraphicsPath path1 = new GraphicsPath())
					{
						ef1.Inflate(-1f, -1f);
						ef1.Width -= 1f;
						float single1 = 1f;
						float single2 = 1f;
						single1 = Math.Min((float) (ef1.Width / 2f), single1);
						single2 = Math.Min((float) (ef1.Height / 2f), single2);
						float single3 = (ef1.X + ef1.Width) - single1;
						path1.AddLine((float) (ef1.X + single1), ef1.Y, single3, ef1.Y);
						path1.AddArc((float) (single3 - single1), ef1.Y, (float) (single1 * 2f), (float) (single2 * 2f), (float) 270f, (float) 90f);
						float single4 = ef1.X + ef1.Width;
						float single5 = (ef1.Y + ef1.Height) - single2;
						path1.AddLine(single4, (float) (ef1.Y + single2), single4, single5);
						path1.AddArc((float) (single4 - (single1 * 2f)), (float) (single5 - single2), (float) (single1 * 2f), (float) (single2 * 2f), (float) 0f, (float) 90f);
						path1.AddLine((float) (single4 - single1), (float) (ef1.Y + ef1.Height), (float) (ef1.X + single1), (float) (ef1.Y + ef1.Height));
						path1.AddArc(ef1.X, (float) (single5 - single2), (float) (single1 * 2f), (float) (single2 * 2f), (float) 90f, (float) 90f);
						path1.AddLine(ef1.X, single5, ef1.X, (float) (ef1.Y + single2));
						path1.AddArc(ef1.X, ef1.Y, (float) (single1 * 2f), (float) (single2 * 2f), (float) 180f, (float) 90f);
						path1.CloseFigure();
						using (Brush brush1 = new LinearGradientBrush(ef1, Color.White, color2, LinearGradientMode.Vertical))
						{
							g.FillPath(brush1, path1);
							g.DrawPath(new Pen(color3), path1);
							using (StringFormat format1 = new StringFormat(StringFormat.GenericTypographic))
							{
								format1.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
								format1.LineAlignment = StringAlignment.Center;
								g.DrawString(var_symbol.Id, SystemInformation.MenuFont, Brushes.Black, new RectangleF(ef1.X + 4f, ef1.Y + 2f, ef1.Width - 8f, ef1.Height - 4f), format1);
							}
						}
					}
				}
			}

			private void shapePanel_MouseUp(object sender, MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Left)
				{
					if (this.selectChanged)
					{
						this.OnSelectedChanged();
					}
					this.selectChanged = false;
				}
                if (Selected!=null)
                {
                    Selected(sender, e);
                }
			}

			private void shapePanel_MouseMove(object sender, MouseEventArgs e)
			{
				if ((e.Button == MouseButtons.Left) && this.ibDrag)
				{
					Point point1 = this.shapePanel.AutoScrollPosition;
					int num1 = e.Y - point1.Y;
					int num2 = num1 / this.itemHeight;
					if (((num2 >= 0) && (this.selectedIndex >= 0)) && ((num2 != this.selectedIndex) && this.dragBegin))
					{
//						DataFormats.Format format1 = DataFormats.GetFormat("SvgElement");
//						DataObject obj1 = new DataObject(format1.Name, this.currentElement);
						Object obj1=((IShape)this.selectedObject).OwnerElement;

						base.DoDragDrop(new DataObject("SvgElement",obj1), DragDropEffects.Move | (DragDropEffects.Copy | DragDropEffects.Scroll));
						this.dragBegin = false;
					}
					else if (this.dragBegin)
					{
						Rectangle rectangle1 = new Rectangle(0, 0, this.shapePanel.Width, this.shapePanel.Height);
						if (!rectangle1.Contains(new Point(e.X, e.Y)))
						{
							Object obj1=((IShape)this.selectedObject).OwnerElement;
							base.DoDragDrop(new DataObject("SvgElement", obj1), DragDropEffects.Move | (DragDropEffects.Copy | DragDropEffects.Scroll));
							this.dragBegin = false;
						}
					}
				}
			}

			private void InvalidateShapePanel()
			{
				if (this.shapePanel != null)
				{
					if (this.currentGroup != null)
					{
						int num1 = this.selectorGroups.IndexOf(this.currentGroup);
						this.shapePanel.Location = new Point(0, (num1 + 1) * this.headHeight);
						this.shapePanel.Invalidate();
					}
					else
					{
						this.shapePanel.Visible = false;
					}
				}
			}

			protected void CreateShapePanel()
			{
				if (this.selectorGroups.Count > 0)
				{
					this.shapePanel = new Panel();
					this.shapePanel.Width = base.Width;
					base.Controls.Add(this.shapePanel);
					this.shapePanel.Anchor = AnchorStyles.Right | (AnchorStyles.Left | (AnchorStyles.Bottom | AnchorStyles.Top));
					this.LayoutShapePanel();
					this.GroupItems = this.selectorGroups[0] as IOutlookBarBand;
					this.shapePanel.Paint += new PaintEventHandler(this.shapePanel_Paint);
					this.shapePanel.MouseDown += new MouseEventHandler(this.shapePanel_MouseDown);
					this.shapePanel.MouseMove += new MouseEventHandler(this.shapePanel_MouseMove);
					this.shapePanel.MouseUp += new MouseEventHandler(this.shapePanel_MouseUp);
					if (this.showToolTip)
					{
						this.toolTip.SetToolTip(this.shapePanel,"拖动图元到编辑环境以创建实例");
					}
					this.shapePanel.BackColor = this.contentColor;
				}
			}

			private void SetSize()
			{
				if (base.Width < this.outlookSize.Width)
				{
					base.Width = this.outlookSize.Width;
				}
				if (base.Height < this.outlookSize.Height)
				{
					base.Height = this.outlookSize.Height;
				}
			}

			protected virtual void UpdateAutoScroll()
			{
				int num2 = this.SelectedIndex;
				if (this.currentGroup != null)
				{
					int num1 = this.currentGroup.Count * this.itemHeight;
					this.shapePanel.AutoScroll = true;
					this.shapePanel.AutoScrollMinSize = new Size(0, num1);
				}
			}

			protected virtual void SelectedPathIndexChanged(int index)
			{
				if ((this.currentGroup != null) && (index >= 0))
				{
					Rectangle rectangle1 = new Rectangle(0, ((index * this.itemHeight) + this.shapePanel.AutoScrollPosition.Y) - 1, this.shapePanel.Width, this.itemHeight + 2);
					this.shapePanel.Invalidate(rectangle1);
				}
			}

			protected void LayoutShapePanel()
			{
				int num1 = this.selectorGroups.Count * this.headHeight;
				this.outlookSize = new Size(this.outlookSize.Width, (num1 + this.panelSize.Height) + 20);
				this.SetSize();
				if (this.shapePanel != null)
				{
					this.shapePanel.Width = base.Width;
					this.shapePanel.Height = base.Height - (this.selectorGroups.Count * this.headHeight);
					this.shapePanel.AutoScroll = true;
				}
			}

			protected virtual void shapePanel_Paint(object sender, PaintEventArgs e)
			{
				//this.shapePanel.BackColor = this.contentColor;
				if (this.currentGroup != null)
				{
					e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
					OutlookBarItemCollection items = this.currentGroup as OutlookBarItemCollection;
					Point point1 = this.shapePanel.AutoScrollPosition;
					int num1 = Math.Max(0, (int) (-point1.Y / this.itemHeight));
					int num2 = this.shapePanel.Height / this.itemHeight;
					int num3 = Math.Min(items.Count, (int) ((num1 + num2) + 2));
					e.Graphics.TranslateTransform((float) point1.X, (float) point1.Y);
					for (int num4 = num1; num4 < num3; num4++)
					{
						IShape shape1 = items[num4];
						if (shape1 != null)
						{
							Color color1 = this.contentColor;
							Color color2 = Color.Black;
							if (shape1 == this.selectedObject)
							{
								color1 = SystemColors.Highlight;
								color2 = SystemColors.HighlightText;
							}
							Rectangle rectangle1 = this.GetItemRect(num4);
							using (Brush brush1 = new SolidBrush(color1))
							{
								e.Graphics.FillRectangle(brush1, new Rectangle(rectangle1.X, rectangle1.Y, rectangle1.Width, rectangle1.Height + 1));
								string text1 = null;
								GraphicsPath path1 = null;
								if (shape1 is Shape)
								{
									path1 = (shape1 as Shape).GraphPath;
									text1 = (shape1 as Shape).Id;
								}
								else if (shape1 is Symbol)
								{
									text1 = (shape1 as Symbol).Id;
									path1 = (shape1 as Symbol).GraphPath;
								}
								if ((path1 != null) && (path1.PointCount > 1))
								{
									using (StringFormat format1 = new StringFormat(StringFormat.GenericTypographic))
									{
										format1.LineAlignment = StringAlignment.Center;
										using (GraphicsPath path2 = (path1.Clone() as GraphicsPath))
										{
											path2.Flatten();
											RectangleF ef1 = path2.GetBounds();
											if (!ef1.IsEmpty)
											{
												float single1 = ef1.X;
												float single2 = ef1.Width / 2f;
												float single3 = ef1.Y;
												float single4 = ef1.Height / 2f;
												using (Brush brush2 = new SolidBrush(color2))
												{
													using (Matrix matrix1 = new Matrix())
													{
														matrix1.Translate((rectangle1.X - ef1.X) + 5f, rectangle1.Y - ef1.Y);
														matrix1.Translate(ef1.X, ef1.Y);
														matrix1.Scale(((float) (rectangle1.Height - 5)) / ef1.Width, ((float) (rectangle1.Height - 5)) / ef1.Height);
														matrix1.Translate(-ef1.X, -ef1.Y);
														path2.Transform(matrix1);
														e.Graphics.FillPath(brush2, path2);
													}
													RectangleF ef2 = new RectangleF((float) (((rectangle1.X + rectangle1.Height) + 5) + 10), (float) rectangle1.Y, (float) rectangle1.Width, (float) rectangle1.Height);
													e.Graphics.DrawString(text1, SystemInformation.MenuFont, brush2, ef2, format1);
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

			private void shapePanel_MouseDown(object sender, MouseEventArgs e)
			{
				this.shapePanel.Focus();
				if (e.Button == MouseButtons.Left)
				{
					Point point1 = this.shapePanel.AutoScrollPosition;
					int num1 = e.Y - point1.Y;
					this.SelectedPathIndex = num1 / this.itemHeight;
					this.dragBegin = true;
				}
			}

			protected override void Dispose(bool disposing)
			{
				if (disposing && (this.components != null))
				{
					this.components.Dispose();
				}
				base.Dispose(disposing);
			}

			private Rectangle GetHeadRect(IOutlookBarBand var_symbol)
			{
				int num1 = this.selectorGroups.IndexOf(this.currentGroup);
				num1 = Math.Max(0, num1);
				int num2 = this.selectorGroups.IndexOf(var_symbol);
				if (num2 > num1)
				{
					num2 = this.selectorGroups.Count - num2;
					return new Rectangle(0, base.Height - (num2 * this.headHeight), base.Width, this.headHeight);
				}
				return new Rectangle(0, num2 * this.headHeight, base.Width, this.headHeight);
			}

			private void InitializeComponent()
			{
				this.components = new Container();
			}

			protected override void OnGotFocus(EventArgs e)
			{
				if (this.shapePanel != null)
				{
					this.shapePanel.Focus();
				}
				base.OnGotFocus(e);
			}

			protected override void OnMouseDown(MouseEventArgs e)
			{
				base.Focus();
				if ((this.shapePanel != null) && (e.Button == MouseButtons.Left))
				{
					if (e.Y < this.shapePanel.Top)
					{
						int num1 = e.Y / this.headHeight;
						if ((num1 >= 0) && (num1 < this.selectorGroups.Count))
						{
							this.GroupItems = this.selectorGroups[num1] as IOutlookBarBand;
						}
					}
					else
					{
						int num2 = (base.Height - e.Y) / this.headHeight;
						num2 = (this.selectorGroups.Count - num2) - 1;
						if ((num2 >= 0) && (num2 < this.selectorGroups.Count))
						{
							this.GroupItems = this.selectorGroups[num2] as IOutlookBarBand;
						}
					}
				}
				base.OnMouseDown(e);
			}

			protected override void OnPaint(PaintEventArgs e)
			{
				for (int num1 = 0; num1 < this.selectorGroups.Count; num1++)
				{
					this.DrawHeaders(e.Graphics, this.selectorGroups[num1] as IOutlookBarBand);
				}
				base.OnPaint(e);
			}

			protected override void OnResize(EventArgs e)
			{
				this.LayoutShapePanel();
				base.OnResize(e);
				base.Invalidate();
			}

			protected virtual void OnSelectedChanged()
			{
				if (this.SelectedChanged != null)
				{
					this.SelectedChanged(this, EventArgs.Empty);
				}
			}


			// Properties
			private IShape SelectedItem
			{
				set
				{
					if (this.selectedObject != value)
					{
						this.selectedObject = value;
						this.selectChanged = true;
					}
				}
			}

			protected IOutlookBarBand GroupItems
			{
				get
				{
					return this.currentGroup;
				}
				set
				{
					if (this.currentGroup != value)
					{
						this.currentGroup = value;
						this.InvalidateShapePanel();
						base.Invalidate();
						this.shapePanel.AutoScrollPosition = new Point(0, 0);
						this.UpdateAutoScroll();
						this.selectedIndex = -1;
					}
				}
			}

			public Color ContentColor
			{
				get
				{
					return this.contentColor;
				}
				set
				{
					if (this.contentColor != value)
					{
						this.contentColor = value;
						this.BackColor = value;
						if (this.shapePanel != null)
						{
							this.shapePanel.BackColor = value;
							this.shapePanel.Invalidate();
						}
					}
				}
			}

			protected int SelectedIndex
			{
				get
				{
					return this.selectorGroups.IndexOf(this.GroupItems);
				}
			}

			protected object SelectedObject
			{
				get
				{
					return this.selectedObject;
				}
			}

			protected virtual int SelectedPathIndex
			{
				set
				{
					if (this.selectedIndex != value)
					{
						this.SelectedPathIndexChanged(this.selectedIndex);
						this.selectedIndex = value;
						this.SelectedPathIndexChanged(this.selectedIndex);
						if (((this.currentGroup != null) && (value >= 0)) && (value < this.currentGroup.Count))
						{
							this.SelectedItem = (this.currentGroup as OutlookBarItemCollection)[value];
						}
					}
					
				}
			}

			public Color TitleColor
			{
				get
				{
					return this.titleColor;
				}
				set
				{
					if (this.titleColor != value)
					{
						this.titleColor = value;
						base.Invalidate();
					}
				}
			}


			// Fields

			protected bool ibDrag;
			private bool dragBegin;
			private Size panelSize;//初始尺寸
			protected int selectedIndex;
			protected bool showToolTip;
			private ToolTip toolTip;
			protected IOutlookBarBand currentGroup;
			private IShape selectedObject;
			protected bool selectChanged;
			private Size outlookSize;//初始尺寸
			protected int itemHeight;
			protected Panel shapePanel;
			protected Color contentColor;
			protected Color titleColor;
			private Container components;
			protected ArrayList selectorGroups;
			private int headHeight;
		}
	


}
