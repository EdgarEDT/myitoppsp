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
using ItopVector.Core.Types;


namespace ItopVector.Core.Figure
{
	public class Group : ContainerElement, IGraphPath
	{
		// Methods
		internal Group(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
		{
			this.graphList = new SvgElementCollection();
			this.graphPath = new GraphicsPath();
			this.graphTransform = new Transf();
			this.graphBrush = new SolidColor(Color.Empty);
			this.graphStroke = new Stroke();
			this.connectPoints = new PointF[0];
			this.visible = true;
			this.showBound = false;
			this.tempOpacity = 1f;
			this.tempFillOpacity = 1f;
			this.tempStrokeOpacity = 1f;
			this.boundColor = Color.Empty;
			this.isLock = false;
			this.drawVisible = true;
			this.updateattribute = true;
			this.Changed = true;
			this.connectLines = new ItopVector.Core.SvgElementCollection();
			this.graphStroke.OnStrokeBrushChanged += new EventHandler(this.ChangeStrokeBrush);
			canSelect=true;
			this.limitSize =false;
		}
		public virtual ILayer Layer
		{
			get
			{
				if(layer==null || !layer.ID.Equals(GetAttribute("layer")) )
				{
					layer = OwnerDocument.Layers[GetAttribute("layer")] as ILayer;					
				}
				return layer;}
			set
			{
				if(this.layer==value)return;
				if(layer!=null)
					layer.Remove(this);				
				layer =value;
				layer.Add(this);
				AttributeFunc.SetAttributeValue(this,"layer",layer.ID);
			}
		}
		public SvgElementCollection ConnectLines
		{
			get{return connectLines;}
			set{}

		}
		public void RemoveAllConnectLines()
		{
			foreach(ConnectLine cline in this.connectLines)
			{
				cline.Remove(this);
			}
		}
		/// <summary>
		/// 能否选中
		/// </summary>
		public bool CanSelect
		{
			get{return canSelect;}
			set{canSelect=value;}

		}public virtual PointF CenterPoint
		 {
			 get
			 {
				 RectangleF rtf =this.GPath.GetBounds(this.Transform.Matrix); 
				 return new PointF(rtf.X+rtf.Width/2,rtf.Y +rtf.Height/2);				
			 }
			 set
			 {
				
			 }
		 }
		public virtual bool LimitSize
		{

			get{return limitSize;}
			set{limitSize =value;}
		}
		public void AddGraph(Graph graph)
		{
			this.AppendChild(graph);
		}

		public virtual void NotifyChange()
		{
			this.Changed = true;
			foreach (ConnectLine connect in this.connectLines)
			{
				connect.pretime = -1;
			}
		}

		private void ChangeStrokeBrush(object sender, EventArgs e)
		{
			if (this.updateattribute)
			{
				ISvgBrush brush1 = this.graphStroke.Brush;
				if (brush1 is SvgElement)
				{
//					SvgElement element1 = (SvgElement) brush1;
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

		public virtual void Draw(Graphics g, int time)
		{
			if (this.DrawVisible)
			{
//				int num1 = 0;
//				int num2 = 0;
//				if (this.pretime != time)
//				{

//					AnimFunc.CreateAnimateValues(this, time, out num1, out num2);
//				}
				GraphicsContainer container1 = g.BeginContainer();
				g.SmoothingMode = base.OwnerDocument.SmoothingMode;
				if (!this.Visible)
				{
					g.SetClip(Rectangle.Empty);
				}
				Matrix matrix1 = this.Transform.Matrix.Clone();
				this.GraphTransform.Matrix.Multiply(matrix1, MatrixOrder.Prepend);
				//g.Transform = matrix1;
				ClipAndMask.ClipPath.Clip(g, time, this);
				this.TempFillOpacity = Math.Min(1f, this.FillOpacity);
				this.TempOpacity = Math.Min(1f, this.Opacity);
				this.TempStrokeOpacity = Math.Min(1f, this.StrokeOpacity);
				Matrix matrix2 = this.GraphTransform.Matrix.Clone();
				if (this.pretime != time)
				{
					this.graphPath.Reset();
					foreach(IGraph childgraph in this.graphList )
					{
						childgraph.ShowConnectPoints=false;
					}
				}
				SvgElementCollection.ISvgElementEnumerator enumerator1 = this.graphList.GetEnumerator();
				while (enumerator1.MoveNext())
				{
					IGraph graph1 = (IGraph) enumerator1.Current;
					graph1.GraphTransform.Matrix = matrix2.Clone();
					graph1.Draw(g, time);
					if ((this.pretime != time))
					{
						GraphicsPath path1 = (GraphicsPath) graph1.GPath.Clone();
						path1.Transform(graph1.Transform.Matrix);
						this.graphPath.StartFigure();
						if (path1.PointCount > 0)
						{
							this.graphPath.AddPath(path1, false);
						}
					}
				}
				if(this.pretime != time)
				{
					this.CreateConnectPoint();
				}
				this.DrawConnect(g);
				ClipAndMask.ClipPath.DrawClip(g, time, this);
				g.EndContainer(container1);
				this.pretime = time;
			}
		}
		public virtual void DrawConnect(Graphics g)
		{
			if (showConnectPoints)
			{
				PointF[] tfArray1 = this.connectPoints;
				if (tfArray1 != null && tfArray1.Length > 0)
				{
					int num1 = 3;
					tfArray1 = tfArray1.Clone() as PointF[];
					this.GraphTransform.Matrix.TransformPoints(tfArray1);
					using (Pen pen1 = new Pen(Color.Blue))
					{
						for (int num2 = 0; num2 < tfArray1.Length; num2++)
						{
							PointF tf1 = tfArray1[num2];
							g.DrawLine(pen1, tf1.X - num1, tf1.Y - num1, tf1.X + num1, tf1.Y + num1);
							g.DrawLine(pen1, tf1.X + num1, tf1.Y - num1, tf1.X - num1, tf1.Y + num1);

						}
					}
				}
			}
		}

		public void AddConnectLine(ConnectLine connectelement)
		{
			if (!this.connectLines.Contains(connectelement))
			{
				this.connectLines.Add(connectelement);
			}
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
					path1.Flatten();
					RectangleF ef1 = path1.GetBounds();
					//连接点
					PointF[] tfArray1 = new PointF[] {new PointF(ef1.X + (ef1.Width/2f), ef1.Y), new PointF(ef1.Right, (ef1.Height/2f) + ef1.Y), new PointF(ef1.X + (ef1.Width/2f), ef1.Bottom), new PointF(ef1.X, ef1.Y + (ef1.Height/2f))
													  };
					connectPoints = tfArray1;
				}
			}
		}
		public virtual RectangleF GetBounds()
		{
			GraphicsPath path1 = (GraphicsPath) this.GPath.Clone();
			path1.Flatten();
			return path1.GetBounds();
		}

		public override bool IsValidChild(XmlNode node)
		{
			if (node is IGraph)
			{
				return true;
			}
			return false;
		}

		public void RemoveGraph(Graph graph)
		{
			this.RemoveChild(graph);
		}


		// Properties
		public Color BoundColor
		{
			get { return this.boundColor; }
			set
			{
				if (this.boundColor != value)
				{
					this.boundColor = value;
				}
			}
		}

		public override SvgElementCollection ChildList
		{
			get { return this.graphList; }
		}

		public ClipPath ClipPath
		{
			get
			{
				string text1 = string.Empty;
				if (this.svgAnimAttributes.ContainsKey("clip-path"))
				{
					text1 = this.svgAnimAttributes["clip-path"].ToString();
				}
				if (text1 != string.Empty)
				{
					if (text1.EndsWith(";"))
					{
						text1 = text1.Substring(0, text1.Length - 1);
					}
					string text2 = text1.Substring(text1.IndexOf("#")).Trim();
					text2 = text2.Substring(1, text2.Length - 2);
					XmlNode node1 = NodeFunc.GetRefNode(text2, base.OwnerDocument);
					if (node1 is ClipPath)
					{
						return (ClipPath) node1;
					}
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					SvgDocument document1 = base.OwnerDocument;
					document1.NumberOfUndoOperations++;
					AttributeFunc.SetAttributeValue(this, "clip-path", string.Empty);
				}
				else
				{
					XmlNode node1 = base.OwnerDocument.AddDefsElement(value);
					string text1 = string.Empty;
					if (node1 is ClipPath)
					{
						text1 = ((ClipPath) node1).ID;
					}
					int num1 = base.OwnerDocument.FlowChilds.IndexOf(this);
					if ((num1 + 1) < base.OwnerDocument.FlowChilds.Count)
					{
						base.OwnerDocument.InsertFlowElement(num1 + 1, value);
					}
					else
					{
						base.OwnerDocument.AddFlowElement(value);
					}
					SvgDocument document2 = base.OwnerDocument;
					document2.NumberOfUndoOperations++;
					AttributeFunc.SetAttributeValue(this, "clip-path", "url(#" + text1 + ");");
				}
			}
		}

		public bool DrawVisible
		{
			get { return this.drawVisible; }
			set
			{
				if (this.drawVisible != value)
				{
					this.drawVisible = value;
					SvgElement[] elementArray1 = new SvgElement[1] {this};
					base.OwnerDocument.PostChange(elementArray1, ChangeAction.DrawVisible);
					SvgElementCollection.ISvgElementEnumerator enumerator1 = this.graphList.GetEnumerator();
					while (enumerator1.MoveNext())
					{
						IGraph graph1 = (IGraph) enumerator1.Current;
						graph1.DrawVisible = value;
					}
				}
			}
		}

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

		public virtual bool IsChanged
		{
			get { return this.Changed; }
			set { this.Changed = value; }
		}

		public virtual GraphicsPath GPath
		{
			get
			{
				if (this.IsChanged)
				{
					string text1 = AttributeFunc.FindAttribute("fill-rule", this).ToString().Trim();
					if (text1 == "evenodd")
					{
						this.graphPath.FillMode = FillMode.Alternate;
					}
					else
					{
						this.graphPath.FillMode = FillMode.Winding;
					}
				}
				if(this.graphPath.PointCount==0)
				{
					SvgElementCollection.ISvgElementEnumerator enumerator1 = this.graphList.GetEnumerator();
				
					while (enumerator1.MoveNext())
					{
						IGraph graph1 = (IGraph) enumerator1.Current;
					
						GraphicsPath path1 = (GraphicsPath) graph1.GPath.Clone();
						path1.Transform(graph1.Transform.Matrix);
						this.graphPath.StartFigure();
						if (path1.PointCount > 0)
						{
							this.graphPath.AddPath(path1, false);
						}
					}
				}
				return this.graphPath;
			}
			set { this.graphPath = value; }
		}

		public virtual ISvgBrush GraphBrush
		{
			get
			{
				if (this.ParentNode != null)
				{
					string text1 = AttributeFunc.ParseAttribute("fill", this, false).ToString();
					if (text1 != string.Empty)
					{
						this.graphBrush = BrushManager.Parsing(text1, base.OwnerDocument, this);
					}
					else
					{
						this.graphBrush = new SolidColor(Color.Black);
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
				if (this.updateattribute)
				{
					if (value is SvgElement)
					{
//						SvgElement element1 = (SvgElement) value;
						string text1 = CodeFunc.CreateString(base.OwnerDocument, ((SvgElement) value).Name);
						AttributeFunc.SetAttributeValue((SvgElement) value, "id", text1);
						AttributeFunc.SetAttributeValue(this, "fill", "url(#" + text1 + ");");
						if ((this.ParentNode != null) && (((SvgElement) value).ParentNode == null))
						{
							base.OwnerDocument.AddDefsElement((SvgElement) value);
							goto Label_0107;
						}
						this.svgAttributes["fill"] = value;
						this.svgAnimAttributes["fill"] = value;
					}
					else if (value == null)
					{
						AttributeFunc.SetAttributeValue(this, "fill", "none");
					}
					else
					{
						AttributeFunc.SetAttributeValue(this, "fill", ColorFunc.GetColorString(((SolidColor) value).Color));
						AttributeFunc.SetAttributeValue(this, "fill-opacity", ((SolidColor) value).Opacity.ToString());
					}
				}
				Label_0107:
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

		public virtual SvgElementCollection GraphList
		{
			get { return this.graphList; }
		}

		public virtual Stroke GraphStroke
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
					if (this.updateattribute)
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

		public Transf GraphTransform
		{
			get { return this.graphTransform; }
			set { this.graphTransform = value; }
		}

		public bool IsLock
		{
			get { return this.isLock; }
			set
			{
				if (this.IsLock != value)
				{
					this.isLock = value;
					SvgElement[] elementArray1 = new SvgElement[1] {this};
					base.OwnerDocument.PostChange(elementArray1, ChangeAction.Lock);
					SvgElementCollection.ISvgElementEnumerator enumerator1 = this.graphList.GetEnumerator();
					while (enumerator1.MoveNext())
					{
						IGraph graph1 = (IGraph) enumerator1.Current;
						graph1.IsLock = value;
					}
				}
			}
		}

		public float Opacity
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

		public PointInfoCollection PointsInfo
		{
			get { return new PointInfoCollection(); }
		}

		public bool ShowBound
		{
			get { return this.showBound; }
			set
			{
				if (this.showBound != value)
				{
					this.showBound = value;
					SvgElement[] elementArray1 = new SvgElement[1] {this};
					base.OwnerDocument.PostChange(elementArray1, ChangeAction.ShowBound);
					SvgElementCollection.ISvgElementEnumerator enumerator1 = this.graphList.GetEnumerator();
					while (enumerator1.MoveNext())
					{
						IGraph graph1 = (IGraph) enumerator1.Current;
						graph1.ShowBound = value;
					}
				}
			}
		}

		public bool ShowClip
		{
			set
			{
				if (this.ClipPath != null)
				{
					this.ClipPath.ShowClip = value;
				}
			}
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

		public float TempFillOpacity
		{
			get { return this.tempFillOpacity; }
			set { this.tempFillOpacity = value; }
		}

		public float TempOpacity
		{
			get { return this.tempOpacity; }
			set { this.tempOpacity = value; }
		}

		public float TempStrokeOpacity
		{
			get { return this.tempStrokeOpacity; }
			set { this.tempStrokeOpacity = value; }
		}

		public virtual Transf Transform
		{
			get
			{
				Matrix matrix1 = new Matrix();
				if (this.svgAnimAttributes.ContainsKey("transform"))
				{
					matrix1 = (Matrix) this.svgAnimAttributes["transform"];
				}
				Transf transf1 = new Transf();
				transf1.setMatrix(matrix1);
				return transf1;
			}
			set { AttributeFunc.SetAttributeValue(this, "transform", value.ToString()); }
		}

		public bool UpdateAttribute
		{
			get { return this.updateattribute; }
			set { this.updateattribute = value; }
		}

		public bool Visible
		{
			get
			{
				string text1 = AttributeFunc.ParseAttribute("visibility", this, false).ToString();
				return ((text1 != "hidden") && true);
			}
			set
			{
				if (this.visible != value)
				{
					this.visible = value;
					AttributeFunc.SetAttributeValue(this, "visibility", value ? "visible" : "hidden");
				}
			}
		}

		public virtual bool ShowConnectPoints
		{
			get { return this.showConnectPoints; }
			set
			{
				this.showConnectPoints = value;
			}
		}
		/// <summary>
		/// 连接点数组
		/// </summary>
		public PointF[] ConnectPoints
		{
			get { return this.connectPoints; }
		}
		private PointF[] connectPoints;

		private bool showConnectPoints = false;

		private ItopVector.Core.SvgElementCollection connectLines;

		// Fields
		private ILayer layer;
		private Color boundColor;
		private bool drawVisible;
		private ISvgBrush graphBrush;
		private SvgElementCollection graphList;
		protected GraphicsPath graphPath;
		private Stroke graphStroke;
		private Transf graphTransform;
		private bool isLock;
		private bool showBound;
		private float tempFillOpacity;
		private float tempOpacity;
		private float tempStrokeOpacity;
		private bool updateattribute;
		private bool visible;
		private bool Changed;
		private bool canSelect;
		private bool limitSize;
        private bool ismarkerchild = false;
        #region IGraph 成员


        public bool IsMarkerChild {
            get {
                return ismarkerchild;
            }
            set {
                ismarkerchild = value;
            }
        }

        #endregion
    }
}