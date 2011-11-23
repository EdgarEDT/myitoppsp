using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using ItopVector.Core.Document;
using ItopVector.Core.Func;
using ItopVector.Core.Interface.Figure;

namespace ItopVector.Core.Figure
{
	public class Use : GraphPath
	{
		// Methods
		internal Use(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
		{
			this.x = 0f;
			this.y = 0f;
			this.width = 0f;
			this.height = 0f;
			this.graphId = null;
			this.refelement = null;
		}

		public override void Draw(Graphics g, int time)
		{
			if (base.DrawVisible)
			{
//				if (this.pretime != time)
//				{
//					int num1 = 0;
//					int num2 = 0;
//					AnimFunc.CreateAnimateValues(this, time, out num1, out num2);
//				}
				GraphicsContainer container1 = g.BeginContainer();
				g.SmoothingMode = base.OwnerDocument.SmoothingMode;
				Matrix matrix1 = Transform.Matrix.Clone();

//				Matrix matrix3 = base.GraphTransform.Matrix.Clone();
				
				base.GraphTransform.Matrix.Multiply(matrix1, MatrixOrder.Prepend);

				if (!base.Visible)
				{
					g.SetClip(Rectangle.Empty);
				}
				ClipAndMask.ClipPath.Clip(g, time, this);
				base.TempFillOpacity = Math.Min(1f, base.FillOpacity);
				base.TempOpacity = Math.Min(1f, base.Opacity);
				base.TempStrokeOpacity = Math.Min(1f, base.StrokeOpacity);
				SvgDocument document1 = base.OwnerDocument;
				bool flag1 = document1.AcceptChanges;
				document1.AcceptChanges = false;
				SvgElement element1 = this.RefElement;
				if (element1 is IGraph)
				{
					IGraph graph1 = (IGraph) element1;
					graph1.GraphTransform.Matrix = base.GraphTransform.Matrix.Clone();
					graph1.GraphTransform.Matrix.Translate(this.X, this.Y); //add
					((SvgElement) graph1).UseElement = this;
					
					//graph1.LimitSize = this.LimitSize;
					bool bUseStyle=false;
					if(this.GetAttribute("usestyle")=="true")
					{
						bUseStyle=true;
					 }
					else
					{
						graph1.Draw(g, time);
					}

					((SvgElement) graph1).UseElement = null;
					
					if (this.RefElement is IGraph) 
					{
//						IGraph graph1=this.RefElement as IGraph;
						GraphicsPath path1 = (GraphicsPath) graph1.GPath.Clone();
						Matrix matrix2 = new Matrix();
						matrix2.Translate(this.x, this.y);
						
						matrix2.Multiply(graph1.Transform.Matrix);
						path1.Transform(matrix2);
						this.graphPath = path1;
						if(bUseStyle)
						{
							using (GraphicsPath path2 = path1.Clone() as GraphicsPath)
							{
								path2.Transform(this.GraphTransform.Matrix);
								this.GraphBrush.Paint(path2, g, time, base.FillOpacity);
								this.GraphStroke.Paint(g, this, path2, time);
							}
						}
						//创建连接点
						this.CreateConnectPoint();
					}
					//绘连接点
					this.DrawConnect(g);
				}
				document1.AcceptChanges = flag1;
				this.pretime = time;
				
				g.EndContainer(container1);
			}
		}

		public override bool LimitSize
		{
			get
			{
				SvgElement element1 = this.RefElement;
				if (element1 is IGraph)
				{
					IGraph graph1 =element1  as IGraph;
					return graph1.LimitSize;
				}
				return base.LimitSize;
			}
			set
			{
                limitSize = value;
                base.LimitSize = value;
                SvgElement element1 = this.RefElement;
                if (element1 is IGraph) {
                    IGraph graph1 = element1 as IGraph;
                    graph1.LimitSize =value;
                }
            }
        }
        /// <summary>
        /// 恢复原始大小
        /// </summary>
        public void ResetSize() {

        }
        
        public float Scale {
            get {
                float scale = 1;
                Symbol element1 = this.RefElement as Symbol;
                if (element1!=null) {
                    scale = element1.Scale;
                }
                return scale; 
            }
            set {
                Symbol element1 = this.RefElement as Symbol;
                if (element1 != null) {
                    element1.Scale= value;
                }
            }
        }
        public ItopVector.Core.Types.Transf GetBaseTransf() {
            return base.Transform;
        }
        bool limitSize = true;
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
						Symbol symbol = RefElement as Symbol;
                        RectangleF rf = RectangleF.Empty;
                        //float f21 = symbol.Scale;
                        using (Matrix m2 = symbol.Transform.Matrix.Clone()) {
                            //m2.Scale(f21, f21, MatrixOrder.Prepend);
                            rf =symbol.GPath.GetBounds(m2);
                        }
						
						float f2 = (X+rf.X+(rf.Width)/2)*(matrix1.Elements[0] -1);
						float f3 = (Y+rf.Y+(rf.Height)/2)*(matrix1.Elements[0] -1);
						transform.Matrix.Translate(f2,f3,MatrixOrder.Prepend);

                        //transform.Matrix.Scale(f21, f21, MatrixOrder.Prepend);
					}
				}
				return transform;
			}
			set
			{
//				if (LimitSize)
//				{
//					Matrix matrix2 =new Matrix();
//					using (Matrix matrix1=(this.OwnerDocument.RootElement as SVG).GraphTransform.Matrix.Clone())
//					{
//						float f1 = 1/matrix1.Elements[0];				
//						matrix2.Scale(f1,f1,MatrixOrder.Prepend);
//						Symbol symbol = RefElement as Symbol;
//						RectangleF rf =symbol.GPath.GetBounds(symbol.Transform.Matrix);
//						float f2 = (X+rf.X+(rf.Width)/2)*(matrix1.Elements[0] -1);
//						float f3 = (Y+rf.Y+(rf.Height)/2)*(matrix1.Elements[0] -1);
//						matrix2.Translate(f2,f3,MatrixOrder.Prepend);
//						matrix2.Invert();
//						value.Matrix.Multiply(matrix2,MatrixOrder.Prepend);
//					}
//				}
				base.Transform = value;	
				NotifyChange();
			}
		}

        public override RectangleF GetBounds()
        {
            RectangleF rect =RectangleF.Empty;
            using (GraphicsPath path1 = GPath.Clone() as GraphicsPath)
            {
                path1.Transform(base.Transform.Matrix);
                //path1.Flatten(new Matrix(), 0.5f);
                rect = path1.GetBounds();
            }
            //base.GetBounds();
            return rect;
        }
        public RectangleF GetRectangle()
        {
            GraphicsPath path1 = (GraphicsPath)this.GPath.Clone();
            //path1.Transform(Transform.Matrix);
            path1.Flatten(new Matrix(), 0.5f);
            return path1.GetBounds();
        }
		// Properties
		public override GraphicsPath GPath
		{
			get 
			{
				if (graphPath!=null && graphPath.PointCount == 0 && this.RefElement is IGraph) 
				{
					IGraph graph1=this.RefElement as IGraph;
					using(GraphicsPath path1 = (GraphicsPath) graph1.GPath.Clone())
					{
						using(Matrix matrix2 = new Matrix())
						{
							matrix2.Translate(this.X, this.Y);
							matrix2.Multiply(graph1.Transform.Matrix);
							path1.Transform(matrix2);
							this.graphPath =(GraphicsPath)  path1.Clone();
						}
					}
				}
				return this.graphPath; 
			}
			set { this.graphPath = value; }
		}

		public string GraphId
		{
			get
			{
				if (this.svgAnimAttributes.ContainsKey("xlink:href"))
				{
					this.graphId = this.svgAnimAttributes["xlink:href"].ToString();
				}
				else if (this.svgAnimAttributes.ContainsKey("href"))
				{
					this.graphId = this.svgAnimAttributes["href"].ToString();
				}
				else
				{
					this.graphId = string.Empty;
				}
				if (this.graphId.Trim().StartsWith("#"))
				{
					this.graphId = this.graphId.Trim().Substring(1, this.graphId.Trim().Length - 1);
				}
				return this.graphId;
			}
			set
			{
				string text1 = value;
				if (text1.Trim().StartsWith("#"))
				{
					this.graphId = text1.Substring(1, text1.Trim().Length - 1);
				}
				else
				{
					this.graphId = text1;
				}
				AttributeFunc.SetAttributeValue(this, "xlink:href", "#" + this.graphId.ToString());
			}
		}

		public float Height
		{
			get
			{
				if (this.svgAnimAttributes.ContainsKey("height"))
				{
					this.height = (float) this.svgAnimAttributes["height"];
				}
				else
				{
					this.height = 0f;
				}
				return this.height;
			}
			set
			{
				if (this.height != value)
				{
					this.height = value;
					AttributeFunc.SetAttributeValue(this, "height", value.ToString());
				}
			}
		}

		public SvgElement RefElement
		{
			get { return this.refelement; }
			set { this.refelement = value; }
		}

		public float Width
		{
			get
			{
				if (this.svgAnimAttributes.ContainsKey("width"))
				{
					this.width = (float) this.svgAnimAttributes["width"];
				}
				else
				{
					this.width = 0f;
				}
				return this.width;
			}
			set
			{
				if (this.width != value)
				{
					this.width = value;
					AttributeFunc.SetAttributeValue(this, "width", value.ToString());
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


		// Fields
		private string graphId;
		private float height;
		private SvgElement refelement;
		private float width;
		private float x;
		private float y;
	}
}