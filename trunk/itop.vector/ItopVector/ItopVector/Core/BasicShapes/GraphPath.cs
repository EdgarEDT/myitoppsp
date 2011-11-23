using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using ItopVector.Core.Document;
using ItopVector.Core.Func;
using ItopVector.Core.Interface.Figure;
using ItopVector.Core.Paint;

namespace ItopVector.Core.Figure
{
	public class GraphPath : Graph, IGraphPath
	{
		// Methods
		internal GraphPath(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
		{
			this.graphPath = new GraphicsPath();
			this.graphStroke = new Stroke();
			this.graphBrush = new SolidColor(Color.Empty);
			this.animpath = new GraphicsPath();
//			this.startPoint = PointF.Empty;
			this.updateAttribute = true;
			this.graphStroke.OnStrokeBrushChanged += new EventHandler(this.ChangeStrokeBrush);
		}

		private void ChangeStrokeBrush(object sender, EventArgs e)
		{
			ISvgBrush brush1 = this.graphStroke.Brush;
			if (this.updateAttribute)
			{
				if (brush1 is SvgElement)
				{
					string text1 = CodeFunc.CreateString(base.OwnerDocument, ((SvgElement) brush1).Name);
					if ((this.ParentNode != null) && (((SvgElement) brush1).ParentNode == null))
					{
						AttributeFunc.SetAttributeValue((SvgElement) brush1, "id", text1);
						AttributeFunc.SetAttributeValue(this, "stroke", "url(#" + text1 + ");");
						base.OwnerDocument.AddDefsElement((SvgElement) brush1);
					}
					else
					{
						this.svgAttributes["stroke"] = this.graphStroke;
						this.svgAnimAttributes["stroke"] = this.graphStroke;
					}
				}
				else if (brush1 == null)
				{
					AttributeFunc.SetAttributeValue(this, "stroke", "none");
				}
				else
				{
					AttributeFunc.SetAttributeValue(this, "stroke", ColorFunc.GetColorString(((SolidColor) brush1).Color));
					AttributeFunc.SetAttributeValue(this, "stroke-opacity", ((SolidColor) brush1).Opacity.ToString());
				}
			}
		}

		private RectangleF getParentBounds()
		{
			XmlNode node=null;
			RectangleF tf = this.GPath.GetBounds();
			for(node=this.ParentNode;!(node is SVG || node is Symbol);node=node.ParentNode){}

		
			if(node is Symbol)
			{
				tf = (node as Symbol).GPath.GetBounds();
			}
			return tf;
		}
        public virtual string LineType {
            get { return this.GetAttribute("linetype"); }
            set { Func.AttributeFunc.SetAttributeValue(this, "linetype", value); }
        }
		public override void Draw(Graphics g, int time)
		{
			if (base.DrawVisible)
			{
				Matrix matrix1 = base.Transform.Matrix.Clone();

				GraphicsContainer container1 = g.BeginContainer();

				g.SmoothingMode = base.OwnerDocument.SmoothingMode;

				Matrix matrix2 = base.GraphTransform.Matrix.Clone();
				base.GraphTransform.Matrix.Multiply(matrix1, MatrixOrder.Prepend);


				ClipAndMask.ClipPath.Clip(g, time, this);
				bool flag1 = base.Visible;
				if (!base.Visible)
				{
					g.SetClip(Rectangle.Empty);
				}
				float single1 = this.Opacity;
				if (this.svgAnimAttributes.ContainsKey("fill-opacity"))
				{
					single1 = Math.Min(single1, (float) this.svgAnimAttributes["fill-opacity"]);
				}
				ISvgBrush brush1 = this.GraphBrush;
                Stroke stroke1 =this.graphStroke;
				using (GraphicsPath path1 = (GraphicsPath) this.GPath.Clone())
				{
					path1.Transform(base.GraphTransform.Matrix);
					if (!base.ShowBound)
					{
                        if (this.IsMarkerChild) {
                            Marker marker= this.ParentNode as Marker;
                            this.IsChanged = false;
                            this.pretime = time;
                            stroke1 = marker.GraphStroke;
                            if (brush1 != null && !brush1.IsEmpty())
                                brush1 = marker.GraphBrush;
                            
                        } 
                        if (((brush1 != null) && !(this is Line)) && !(this is Polyline)) {
                            brush1.Paint(path1, g, time, single1);
                        }

                        stroke1.Paint(g, this, path1, time);
                        
						if(this is Polyline)
						{
                            if (LineType == "1") {
                                //平行线，
                                using (Pen p = new Pen(brush1.Pen.Color)) {
                                    p.Width = this.graphStroke.StrokePen.Width;
                                    p.CompoundArray = new float[] { 0f, 0.1f, 0.9f, 1f };
                                    g.DrawPath(p, path1);
                                }
                            } else if(LineType == "2") {//铁路效果
                                using (Pen p = new Pen(Color.FromArgb(120,120,120))) {
                                    p.Width = this.graphStroke.StrokePen.Width;
                                    p.CompoundArray = new float[] { 0f, 0.1f, 0.9f, 1f };
                                    g.DrawPath(p, path1);
                                }
                            }
						}
					}
					else
					{
						g.DrawPath(new Pen(base.BoundColor), path1);
					}
					this.DrawConnect(g);
				}
				matrix1.Dispose();
				ClipAndMask.ClipPath.DrawClip(g, time, this);
				g.EndContainer(container1);
				this.pretime = time;
			}
		}


		public override RectangleF GetBounds()
		{
			GraphicsPath path1 = (GraphicsPath) this.graphPath.Clone();
            //path1.Transform(Transform.Matrix);
			path1.Flatten(new Matrix(), 0.5f);
			return path1.GetBounds();
		}


		// Properties
		public float FillOpacity
		{
			get
			{
				float single1 = 1f;
				if (this.svgAnimAttributes.ContainsKey("fill-opacity"))
				{
					single1 = (float) this.svgAnimAttributes["fill-opacity"];
				}
				XmlNode node1 = this.ParentNode;
				if (this.UseElement != null)
				{
					node1 = this.UseElement;
				}
				if (node1 is IGraph)
				{
					single1 = Math.Min(single1, ((IGraph) node1).TempFillOpacity);
				}
				return single1;
			}
			set { AttributeFunc.SetAttributeValue(this, "fill-opacity", value.ToString()); }
		}

		/// <summary>
		/// 创建连接点
		/// </summary>
		protected virtual void CreateConnectPoint()
		{
			
			if ((this.graphPath != null) && (this.graphPath.PointCount > 1))
			{
				using (GraphicsPath path1 = (this.graphPath.Clone() as GraphicsPath))
				{
					//path1.Flatten();
					RectangleF ef1 = path1.GetBounds();
					PointF[] tfArray1;
					tfArray1=new PointF[5];//+graphPath.PointCount];
					tfArray1[0]=new PointF(ef1.X + (ef1.Width/2f), ef1.Y);//上中
					tfArray1[1]=new PointF(ef1.Right, (ef1.Height/2f) + ef1.Y);//右中
					tfArray1[2]=new PointF(ef1.X + (ef1.Width/2f), ef1.Bottom);//下中
					tfArray1[3]=new PointF(ef1.X, ef1.Y + (ef1.Height/2f));//左中
					tfArray1[4]=new PointF(ef1.X + (ef1.Width/2f), ef1.Y + (ef1.Height/2f));//中心
//					for(int i=0;i<graphPath.PointCount;i++)
//					{
//						tfArray1[i+5]=graphPath.PathPoints[i];
//					}
//					
					//PointF[] tfArray1 = new PointF[] {new PointF(ef1.X + (ef1.Width/2f), ef1.Y), new PointF(ef1.Right, (ef1.Height/2f) + ef1.Y), new PointF(ef1.X + (ef1.Width/2f), ef1.Bottom), new PointF(ef1.X, ef1.Y + (ef1.Height/2f))};
					base.connectPoints = tfArray1;
				}
			}
		}		

		public override GraphicsPath GPath
		{
			get
			{
				if (this.pretime != base.OwnerDocument.ControlTime)
				{
					if (base.SvgAnimAttributes.ContainsKey("d"))
					{
						this.animpath = (GraphicsPath) base.SvgAnimAttributes["d"];
					}
					string text1 = AttributeFunc.FindAttribute("fill-rule", this).ToString().Trim();
					if (text1 == "evenodd")
					{
						this.animpath.FillMode = FillMode.Alternate;
					}
					else
					{
						this.animpath.FillMode = FillMode.Winding;
					}
				}
				return this.animpath;
			}
			set
			{
				this.graphPath = value;
				if (this.Name.Trim() == "path")
				{
					string text1 = PathFunc.GetPathString(value);
					AttributeFunc.SetAttributeValue(this, "d", text1);
				}
			}
		}

		public ISvgBrush GraphBrush
		{
			get
			{
				if (this.pretime!=this.OwnerDocument.ControlTime||(this.ParentNode != null && this.IsChanged))
				{
					string text1 = AttributeFunc.ParseAttribute("fill", this, false).ToString();
					if (text1 != string.Empty)
					{
						this.graphBrush = BrushManager.Parsing(text1, base.OwnerDocument, this);
					}
					else
					{
						this.graphBrush = new SolidColor(Color.White);
					}
				}
				return this.graphBrush;
			}
			set
			{
				if (this.graphBrush == value)
				{
					return;
				}
				if (this.updateAttribute)
				{
					if (value is SvgElement)
					{
//						SvgElement element1 = (SvgElement) value;
						string text1 = CodeFunc.CreateString(base.OwnerDocument, ((SvgElement) value).Name);
						if ((this.ParentNode != null) && (((SvgElement) value).ParentNode == null))
						{
							AttributeFunc.SetAttributeValue((SvgElement) value, "id", text1);
							AttributeFunc.SetAttributeValue(this, "fill", "url(#" + text1 + ");");
							base.OwnerDocument.AddDefsElement((SvgElement) value);
							goto Label_0121;
						}
						this.svgAttributes["fill"] = value;
						this.svgAnimAttributes["fill"] = value;
					}
					else if (value == null)
					{
						AttributeFunc.SetAttributeValue(this, "fill", "none");
					}
					else if (value.IsEmpty())
					{
						AttributeFunc.SetAttributeValue(this, "fill", "none");
					}
					else
					{
						AttributeFunc.SetAttributeValue(this, "fill", ColorFunc.GetColorString(((SolidColor) value).Color));
						AttributeFunc.SetAttributeValue(this, "fill-opacity", ((SolidColor) value).Opacity.ToString());
					}
				}
				Label_0121:
				if (value == null)
				{
					this.graphBrush = new SolidColor(Color.Empty);
				}
				else
				{
					this.graphBrush = value;
				}
			}
		}

		public Stroke GraphStroke
		{
			get { return this.graphStroke; }
			set
			{
				if (this.graphStroke != value)
				{
					this.graphStroke = value;
					if (value != null)
					{
						value.OnStrokeBrushChanged += new EventHandler(this.ChangeStrokeBrush);
					}
					if (this.updateAttribute)
					{
						ISvgBrush brush1 = this.graphStroke.Brush;
						if (brush1 is SvgElement)
						{
//							SvgElement element1 = (SvgElement) brush1;
							string text1 = CodeFunc.CreateString(base.OwnerDocument, ((SvgElement) brush1).Name);
							if ((this.ParentNode != null) && (((SvgElement) brush1).ParentNode == null))
							{
								AttributeFunc.SetAttributeValue((SvgElement) brush1, "id", text1);
								AttributeFunc.SetAttributeValue(this, "stroke", "url(#" + text1 + ");");
								base.OwnerDocument.AddDefsElement((SvgElement) brush1);
							}
							else
							{
								this.svgAttributes["stroke"] = value;
								this.svgAnimAttributes["stroke"] = value;
							}
						}
						else if (brush1 == null)
						{
							AttributeFunc.SetAttributeValue(this, "stroke", "none");
						}
						else
						{
							AttributeFunc.SetAttributeValue(this, "stroke", ColorFunc.GetColorString(((SolidColor) brush1).Color));
							AttributeFunc.SetAttributeValue(this, "stroke-opacity", ((SolidColor) brush1).Opacity.ToString());
						}
					}
				}
			}
		}

		public virtual float Opacity
		{
			get
			{
				float single1 = 1f;
				if (this.svgAnimAttributes.ContainsKey("opacity"))
				{
					single1 = (float) this.svgAnimAttributes["opacity"];
				}
				XmlNode node1 = this.ParentNode;
				if (this.UseElement != null)
				{
					node1 = this.UseElement;
				}
				if (node1 is IGraph)
				{
					single1 = Math.Min(single1, ((IGraph) node1).TempOpacity);
				}
				return single1;
			}
			set { AttributeFunc.SetAttributeValue(this, "opacity", value.ToString()); }
		}

		public float StrokeOpacity
		{
			get
			{
				float single1 = 1f;
				if (this.svgAnimAttributes.ContainsKey("stroke-opacity"))
				{
					single1 = (float) this.svgAnimAttributes["stroke-opacity"];
				}
				XmlNode node1 = this.ParentNode;
				if (this.UseElement != null)
				{
					node1 = this.UseElement;
				}
				if (node1 is IGraph)
				{
					single1 = Math.Min(single1, ((IGraph) node1).TempStrokeOpacity);
				}
				return single1;
			}
			set { AttributeFunc.SetAttributeValue(this, "stroke-opacity", value.ToString()); }
		}

		public bool UpdateAttribute
		{
			get { return this.updateAttribute; }
			set { this.updateAttribute = value; }
		}


		// Fields
		private GraphicsPath animpath;
		private ISvgBrush graphBrush;
		protected GraphicsPath graphPath;
		private Stroke graphStroke;
//		private PointF startPoint;
		private bool updateAttribute;
//		public bool ShowConnectPoints = false;
	}
}