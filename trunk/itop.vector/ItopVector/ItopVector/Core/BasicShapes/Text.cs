using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using ItopVector.Core.ClipAndMask;
using ItopVector.Core.Document;
using ItopVector.Core.Func;
using ItopVector.Core.Interface.Figure;
using ItopVector.Core.Paint;

namespace ItopVector.Core.Figure
{
	public class Text : Group, IText
	{
		// Events
		public event PostTextEditEventHandler OnPostTextEdit;

		// Methods
		internal Text(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
		{
			this.fontName = "Arial";
			this.size = 10f;
			this.x = 0f;
			this.y = 0f;
			this.dx = 0f;
			this.dy = 0f;
			this.textLength = 0f;
			this.rotate = 0f;
			this.currentPostion = PointF.Empty;
			this.doalign = true;
			this.editMode = false;
			this.chars = new ArrayList(0x10);
			this.old = false;
//			this.oldPath = null;
			this.graphPath = new GraphicsPath();
			this.textString=string.Empty;
			this.lines =new string[0];				
		}

		public override bool LimitSize
		{
			get
			{
				string text = GetAttribute("limitsize");
				return text=="true"?true:false;
			}
			set
			{
				SetAttribute("limitsize",value?"true":"false");
				base.LimitSize=value;
			}
		}

		public SvgElementCollection Break(Graphics g)
		{
			SvgElementCollection collection1 = new SvgElementCollection();
			if (this == this.OwnerTextElement)
			{
				this.currentPostion = PointF.Empty;
			}
			SvgDocument document1 = base.OwnerDocument;
			float single1 = this.Size;
			StringFormat format1 = this.GetGDIStringFormat();
			FontFamily family1 = this.GetGDIFontFamily();
			if (this.X == 0)
			{
				this.currentPostion.X = this.currentPostion.X + this.Dx;
			}
			else
			{
				this.currentPostion.X = (this.X + this.Dx);
			}
			if (this.Y == 0)
			{
				this.currentPostion.Y = this.currentPostion.Y + this.Dy;
			}
			else
			{
				this.currentPostion.Y = (this.Y + this.Dy);
			}
			int num1 = this.GetGDIStyle();
			bool flag1 = document1.AcceptChanges;
			document1.AcceptChanges = false;
			foreach (XmlNode node1 in this.ChildNodes)
			{
//				GraphicsPath path1 = new GraphicsPath();
				if (node1 is Text)
				{
					((Text) node1).currentPostion = this.currentPostion;
					collection1.AddRange(((Text) node1).Break(g));
					this.currentPostion = ((Text) node1).currentPostion;
					continue;
				}
				if (node1.NodeType == XmlNodeType.Text)
				{
					string text1 = this.TrimText(node1.Value);
					Font font1 = new Font(family1.Name, single1, (FontStyle) num1);
					float single2 = (((float) family1.GetCellAscent(FontStyle.Regular))/((float) family1.GetEmHeight(FontStyle.Regular)))*single1;
//					ISvgBrush brush1 = base.GraphBrush.Clone();
//					ISvgBrush brush2 = base.GraphStroke.Brush.Clone();
					CharEnumerator enumerator2 = text1.GetEnumerator();
					while (enumerator2.MoveNext())
					{
						char ch1 = enumerator2.Current;
						Text text2 = (Text) document1.CreateElement(document1.Prefix, "text", document1.NamespaceURI);
						text2.AppendChild(document1.CreateTextNode(ch1.ToString()));
						foreach (XmlAttribute attribute1 in this.Attributes)
						{
							text2.SetAttributeNode((XmlAttribute) attribute1.Clone());
						}
						string text3 = ch1.ToString();
						SizeF ef1 = g.MeasureString(text3, font1, new PointF(this.currentPostion.X, this.currentPostion.Y - single2), format1);
						float single3 = ef1.Width;
						AttributeFunc.SetAttributeValue(text2, "x", this.currentPostion.X.ToString());
						AttributeFunc.SetAttributeValue(text2, "y", this.currentPostion.Y.ToString());
						this.currentPostion.X += ((single3*3f)/4f);
						collection1.Add(text2);
					}
				}
			}
			document1.AcceptChanges = flag1;
			return collection1;
		}

		public override void Draw(Graphics g, int time)
		{
			if (base.DrawVisible)
			{
				GraphicsContainer container1 = g.BeginContainer();
//				if (this.pretime != time)
//				{
//					int num1 = 0;
//					int num2 = 0;
//					AnimFunc.CreateAnimateValues(this, time, out num1, out num2);
//				}
				Matrix matrix1 = this.Transform.Matrix.Clone();
				base.GraphTransform.Matrix.Multiply(matrix1, MatrixOrder.Prepend);
				if (!this.editMode)
				{
					g.SmoothingMode = base.OwnerDocument.SmoothingMode;

					if (this == this.OwnerTextElement)
					{
						ClipPath.Clip(g, time, this);
					}
					if (!base.Visible)
					{
						g.SetClip(Rectangle.Empty);
					}
					ISvgBrush brush1 = base.GraphBrush;

					if (this == this.OwnerTextElement)
					{
						this.currentPostion = PointF.Empty;
					}
					float single1 = this.Size;
					this.GPath = new GraphicsPath();
					using (StringFormat format1 = this.GetGDIStringFormat())
					{

						using (FontFamily family1 = this.GetGDIFontFamily())
						{
							if (this.X == 0)
							{
								this.currentPostion.X = this.currentPostion.X + this.Dx;
							}
							else
							{
								this.currentPostion.X = (this.X + this.Dx);
							}
							if (this.Y == 0)
							{
								this.currentPostion.Y = this.currentPostion.Y + this.Dy;
							}
							else
							{
								this.currentPostion.Y = (this.Y + this.Dy);
							}
							//this.currentPostion.Y += (this.Y + this.Dy);
							int num3 = this.GetGDIStyle();
							base.TempFillOpacity = Math.Min(1f, base.FillOpacity);
							base.TempOpacity = Math.Min(1f, base.Opacity);
							base.TempStrokeOpacity = Math.Min(1f, base.StrokeOpacity);
							this.old = true;
//							this.OwnerDocument.BeginPrint=true;
                            bool flag = false;
							foreach (XmlNode node1 in this.ChildNodes)
							{
								GraphicsPath path1 = new GraphicsPath();
								if (node1 is Text)
								{
									((Text) node1).currentPostion = this.currentPostion;
									((Text) node1).GraphTransform.Matrix = base.GraphTransform.Matrix.Clone();
									((Text) node1).Draw(g, time);
									this.currentPostion = ((Text) node1).currentPostion;
									if (((Text) node1).GPath.PointCount > 0)
									{
										this.GPath.StartFigure();
										this.GPath.AddPath(((Text) node1).GPath, false);
									}
									continue;
								}
								if (node1.NodeType == XmlNodeType.Text)
								{
									string text1 = "t";//this.TrimText(node1.Value);
									Font font1 =null;
									try
									{
										font1=new Font(family1.Name, single1, (FontStyle) num3,GraphicsUnit.Pixel);
									}
									catch
									{
										int ii=0;
										ii++;

									}
									float single2 = (((float) family1.GetCellAscent(FontStyle.Regular))/((float) family1.GetEmHeight(FontStyle.Regular)))*single1;
									SizeF ef1 = g.MeasureString(text1, font1, new PointF(this.currentPostion.X, this.currentPostion.Y - single2), format1);
									float single3 = ef1.Width;

									float single5 = (((float) family1.GetLineSpacing(FontStyle.Regular)) / ((float) family1.GetEmHeight(FontStyle.Regular))) * single1 ; //ÐÐ¾à
					
									float offy = this.currentPostion.Y -single2;
									float offx = this.currentPostion.X;
                                    
									for(int i=0 ;i<Lines.Length;i++)
									{
										if (!base.ShowBound && (this.OwnerDocument.BeginPrint || LimitSize))
										{
											GraphicsContainer gc=g.BeginContainer();
											g.TextRenderingHint =System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
											g.Transform=base.GraphTransform.Matrix;
											//g.DrawString(lines[i], font1,new SolidBrush(Color.Black),100,100);
											Color color1 =Color.Black;
											if (this.SvgAttributes.Contains("stroke"))
											{
												color1=Stroke.GetStroke(this).StrokeColor;
												if (color1.IsEmpty || color1==Color.Transparent || color1==Color.Empty )
													color1 = Color.Black;
											}
											
											g.DrawString(lines[i], font1,new SolidBrush(color1),new PointF(offx, offy),format1);
											g.EndContainer(gc);
											SizeF ef2 = g.MeasureString(lines[i], font1, new PointF(offx, offy), format1);					
											path1.AddString(lines[i],family1,num3,single1,new PointF(offx, offy),format1);										
										}
										else
										{
                                            flag = true;
											path1.AddString(lines[i], family1, num3, single1, new PointF(offx, offy), format1);
										}
										if(Vertical)
										{
											offx += single5;
										}
										else
										{
											offy += single5;
										}
									}
									this.currentPostion.X += ((single3*3f)/4f);
									this.GPath.StartFigure();
									this.GPath.AddPath(path1, false);
									float single4 = base.Opacity;
									if (this.svgAnimAttributes.ContainsKey("fill-opacity"))
									{
										single4 = Math.Min(single4, base.FillOpacity);
									}
									path1.Transform(base.GraphTransform.Matrix);
                                    if (!base.ShowBound && !(this.OwnerDocument.BeginPrint || LimitSize))
									{
										
										brush1.Paint(path1, g, time, single4);
										//								Stroke stroke1 = Stroke.GetStroke(this);
										//								stroke1.Paint(g, this,path1, time);
										base.GraphStroke.Paint(g, this, path1, 0);
										continue;
									}
									else if(flag)
									{
										g.DrawPath(new Pen(base.BoundColor), path1);
									}
								}
								path1.Dispose();
							}
						}
					}
					
					matrix1.Dispose();
					ClipPath.DrawClip(g, time, this);
					g.EndContainer(container1);
					this.pretime = time;
					this.old = false;
				}
			}
		}

		private void draw2(Graphics g, int time)
		{
			if (base.DrawVisible)
			{
				GraphicsContainer container1 = g.BeginContainer();
				Matrix matrix1 = base.Transform.Matrix.Clone();
				base.GraphTransform.Matrix.Multiply(matrix1, MatrixOrder.Prepend);
				if (!this.editMode)
				{
					g.SmoothingMode = base.OwnerDocument.SmoothingMode;

					if (this == this.OwnerTextElement)
					{
						ClipPath.Clip(g, time, this);
					}
					if (!base.Visible)
					{
						g.SetClip(Rectangle.Empty);
					}
					ISvgBrush brush1 = base.GraphBrush;

					if (this == this.OwnerTextElement)
					{
						this.currentPostion = PointF.Empty;
					}
					float single1 = this.Size;
					this.GPath = new GraphicsPath();
					using (StringFormat format1 = this.GetGDIStringFormat())
					{

						using (FontFamily family1 = this.GetGDIFontFamily())
						{
							if (this.X == 0)
							{
								this.currentPostion.X = this.currentPostion.X + this.Dx;
							}
							else
							{
								this.currentPostion.X = (this.X + this.Dx);
							}
							if (this.Y == 0)
							{
								this.currentPostion.Y = this.currentPostion.Y + this.Dy;
							}
							else
							{
								this.currentPostion.Y = (this.Y + this.Dy);
							}
							//this.currentPostion.Y += (this.Y + this.Dy);
							int num3 = this.GetGDIStyle();
							base.TempFillOpacity = Math.Min(1f, base.FillOpacity);
							base.TempOpacity = Math.Min(1f, base.Opacity);
							base.TempStrokeOpacity = Math.Min(1f, base.StrokeOpacity);
							this.old = true;
							foreach (XmlNode node1 in this.ChildNodes)
							{
								GraphicsPath path1 = new GraphicsPath();
								if (node1 is Text)
								{
									((Text) node1).currentPostion = this.currentPostion;
									((Text) node1).GraphTransform.Matrix = base.GraphTransform.Matrix.Clone();
									((Text) node1).Draw(g, time);
									this.currentPostion = ((Text) node1).currentPostion;
									if (((Text) node1).GPath.PointCount > 0)
									{
										this.GPath.StartFigure();
										this.GPath.AddPath(((Text) node1).GPath, false);
									}
									continue;
								}
								if (node1.NodeType == XmlNodeType.Text)
								{
									string text1 = "t";//this.TrimText(node1.Value);
									Font font1 = new Font(family1.Name, single1, (FontStyle) num3);
									float single2 = (((float) family1.GetCellAscent(FontStyle.Regular))/((float) family1.GetEmHeight(FontStyle.Regular)))*single1;
									SizeF ef1 = g.MeasureString(text1, font1, new PointF(this.currentPostion.X, this.currentPostion.Y - single2), format1);
									float single3 = ef1.Width;

									float single5 = (((float) family1.GetLineSpacing(FontStyle.Regular)) / ((float) family1.GetEmHeight(FontStyle.Regular))) * single1 ; //ÐÐ¾à
					
									float offy = this.currentPostion.Y -single2;
									float offx = this.currentPostion.X;
									for(int i=0 ;i<Lines.Length;i++)
									{
										if (!base.ShowBound)
										{
											GraphicsContainer gc=g.BeginContainer();
											g.Transform=base.GraphTransform.Matrix;
											//g.DrawString(lines[i], font1,new SolidBrush(Color.Black),100,100);
											g.DrawString(lines[i], font1,new SolidBrush(Color.Black),new PointF(offx, offy));
											g.EndContainer(gc);
										}
										path1.AddString(lines[i], family1, num3, single1, new PointF(offx, offy), format1);
										if(Vertical)
										{
											offx += single5;
										}
										else
										{
											offy += single5;
										}
									}
									this.currentPostion.X += ((single3*3f)/4f);
									this.GPath.StartFigure();
									this.GPath.AddPath(path1, false);
									float single4 = base.Opacity;
									if (this.svgAnimAttributes.ContainsKey("fill-opacity"))
									{
										single4 = Math.Min(single4, base.FillOpacity);
									}
									
									path1.Transform(base.GraphTransform.Matrix);
									
									if (!base.ShowBound)
									{
										
//										brush1.Paint(path1, g, time, single4);
//										//								Stroke stroke1 = Stroke.GetStroke(this);
//										//								stroke1.Paint(g, this,path1, time);
//										base.GraphStroke.Paint(g, this, path1, 0);
										continue;
									}
									else
									{
										g.DrawPath(new Pen(base.BoundColor), path1);
									}
								}
								path1.Dispose();
							}
						}
					}
					
					matrix1.Dispose();
					ClipPath.DrawClip(g, time, this);
					g.EndContainer(container1);
					this.pretime = time;
					this.old = false;
				}
			}
		}
		private ItopVector.Core.Types.Transf transform;
		public override ItopVector.Core.Types.Transf Transform
		{
			get
			{
				transform =base.Transform;
				if (LimitSize)
				{
					using (Matrix matrix1=(this.OwnerDocument.RootElement as SVG).GraphTransform.Matrix.Clone())
					{
						float f1 = 1/matrix1.Elements[0];
				
						transform.Matrix.Scale(f1,f1,MatrixOrder.Prepend);
						RectangleF rf =this.GPath.GetBounds();
						float f2 = (rf.X+rf.Width/2)*(matrix1.Elements[0] -1);
						float f3 = (rf.Y+rf.Height/2)*(matrix1.Elements[0] -1);
						transform.Matrix.Translate(f2,f3,MatrixOrder.Prepend);
					}
				}
				return transform;
			}
			set
			{
				base.Transform = value;				
			}
		}
		protected float GetComputedFontSize()
		{
			return TextFunc.GetComputedFontSize(this);
		}

		protected FontFamily GetGDIFontFamily()
		{
			return TextFunc.GetGDIFontFamily(this);
		}

		protected StringFormat GetGDIStringFormat()
		{
			return TextFunc.GetGDIStringFormat(this);
		}

		protected int GetGDIStyle()
		{
			return TextFunc.GetGDIStyle(this);
		}

		new public bool IsValidChild(XmlNode node)
		{
			if ((node is TSpan) || (node is TRef))
			{
				return true;
			}
			return false;
		}

		public bool PostEdit(string attributename, string attributevalue)
		{
			if ((this.editMode || this.OwnerTextElement.editMode) && (this.OnPostTextEdit != null))
			{
				this.OnPostTextEdit(this, attributename, attributevalue);
				return true;
			}
			return false;
		}

		protected string TrimText(string val)
		{
			return TextFunc.TrimText(val, this);
		}


		// Properties
		public ArrayList Chars
		{
			get { return this.chars; }
		}

		public float Dx
		{
			get
			{
				if (this.svgAnimAttributes.ContainsKey("dx"))
				{
					this.dx = (float) this.svgAnimAttributes["dx"];
				}
				else
				{
					this.dx = 0f;
				}
				return this.dx;
			}
			set
			{
				if (this.dx != value)
				{
					this.dx = value;
					AttributeFunc.SetAttributeValue(this, "dx", value.ToString());
				}
			}
		}

		public float Dy
		{
			get
			{
				if (this.svgAnimAttributes.ContainsKey("dy"))
				{
					this.dy = (float) this.svgAnimAttributes["dy"];
				}
				else
				{
					this.dy = 0f;
				}
				return this.dy;
			}
			set
			{
				if (this.dy != value)
				{
					this.dy = value;
					AttributeFunc.SetAttributeValue(this, "dy", value.ToString());
				}
			}
		}

		public bool EditMode
		{
			get { return this.editMode; }
			set { this.editMode = value; }
		}

		public string FontName
		{
			get
			{
				if (this.pretime != base.OwnerDocument.ControlTime)
				{
					string text1 = (string) AttributeFunc.FindAttribute("font-family", this);
					this.fontName = text1;
				}
				return (string) AnimFunc.GetAnimateValue(this, "font-family", DomType.SvgString, this.fontName);
			}
			set
			{
				if (this.fontName != value)
				{
					this.fontName = value;
					AttributeFunc.SetAttributeValue(this, "font-family", this.fontName);
				}
			}
		}

		public override GraphicsPath GPath
		{
			get { return this.graphPath; }
			set { this.graphPath = value; }
		}

		public Text OwnerTextElement
		{
			get
			{
				for (XmlNode node1 = this; node1 != null; node1 = node1.ParentNode)
				{
					if ((node1 is Text) && !(node1 is TSpan))
					{
						return (Text) node1;
					}
				}
				return null;
			}
		}

		public float Rotate
		{
			get
			{
				if (this.svgAnimAttributes.ContainsKey("rotate"))
				{
					this.rotate = (float) this.svgAnimAttributes["rotate"];
				}
				else
				{
					this.rotate = 0f;
				}
				return this.rotate;
			}
			set
			{
				if (this.rotate != value)
				{
					this.rotate = value;
					AttributeFunc.SetAttributeValue(this, "rotate", value.ToString());
				}
			}
		}

		public float Size
		{
			get
			{
				object obj1 = AttributeFunc.FindAttribute("font-size", this);
				if ((obj1 != null) && (obj1.ToString() != string.Empty))
				{
					this.size = (float) obj1;
				}
				else
				{
					this.size = 12f;
				}
				return this.size;
			}
			set
			{
				if (this.size != value)
				{
					this.size = value;
					AttributeFunc.SetAttributeValue(this, "font-size", this.size.ToString());
				}
			}
		}

		public int Style
		{
			get { return 0; }
			set
			{
			}
		}

		public float TextLength
		{
			get
			{
				if (this.svgAnimAttributes.ContainsKey("textLength"))
				{
					this.textLength = (float) this.svgAnimAttributes["textLength"];
				}
				else
				{
					this.textLength = 0f;
				}
				return this.textLength;
			}
			set
			{
				if (this.textLength != value)
				{
					this.textLength = value;
					AttributeFunc.SetAttributeValue(this, "textLength", value.ToString());
				}
			}
		}

		public virtual string TextString
		{
			get
			{
				if (this.pretime != base.OwnerDocument.ControlTime || this.textString==string.Empty)
				{
					this.textString = this.InnerText;
				}
				return this.textString;
			}
			set
			{
				if (this.textString != value)
				{
					this.textString = value;
//					this.OwnerDocument.ChangeElements.Add(this);
					this.InnerText = this.textString;
//					this.OwnerDocument.NotifyUndo();
				}
			}
		}

		public float X
		{
			get
			{
				if (this.svgAnimAttributes.ContainsKey("x"))
				{
					this.x = (float) this.svgAnimAttributes["x"];
				}
				else
				{
					this.x = 0f;
				}
				return this.x;
			}
			set
			{
				if (this.x != value)
				{
					this.x = value;
					AttributeFunc.SetAttributeValue(this, "x", value.ToString());
				}
			}
		}

		public float Y
		{
			get
			{
				if (this.svgAnimAttributes.ContainsKey("y"))
				{
					this.y = (float) this.svgAnimAttributes["y"];
				}
				else
				{
					this.y = 0f;
				}
				return this.y;
			}
			set
			{
				if (this.y != value)
				{
					this.y = value;
					AttributeFunc.SetAttributeValue(this, "y", value.ToString());
				}
			}
		}
		public string [] Lines
		{
			get
			{
				if(linesString!=TextString)
				{
					linesString=this.textString ;
					string text1=linesString.Replace("\r\n","\r");
					text1 =  text1.Replace("\r","\n");
					lines =new string[1]{text1};
					//lines =text1.Split("\r".ToCharArray());
				}
				return lines;
			}
		}
		public bool Vertical
		{
			get
			{
				string text1 = this.GetAttribute("writing-mode");
				if (text1 != "tb")
				{
					return (text1 == "tb-rl");
				}
				return true;
			}
		}

		private string[] lines;
		private string   linesString;

		// Fields
		private ArrayList chars;
		protected PointF currentPostion;
		public bool doalign;
		private float dx;
		private float dy;
		private bool editMode;
		private string fontName;
//		private GraphicsPath graphPath;
		private bool old;
//		private GraphicsPath oldPath;
		private float rotate;
		private float size;
		private float textLength;
		private string textString;
		private float x;
		private float y;
/*
		private Stroke textStroke;
*/
/*
		private ISvgBrush textBrush;
*/
//		private Struct.TextStyle ratFont;
	}
}