namespace ItopVector.Core.Figure
{
    using ItopVector.Core;
    using ItopVector.Core.Document;
    using ItopVector.Core.Func;
    using ItopVector.Core.Interface;
    using ItopVector.Core.Interface.Figure;
    using ItopVector.Core.Types;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class SVG : Group, IViewportElement, ISvgElement
    {
        // Methods
        internal SVG(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
        {
            this.viewBox = new ItopVector.Core.Types.ViewBox();
            this.FullScreen = false;
			contectbounds=RectangleF.Empty;
        }

        public override void Draw(Graphics g, int time)
        {
            float single5;
            float single6;
            float single7;
            float single8;
            float single11;
            float single12;
            float single13;
            Matrix matrix1;
            ItopVector.Core.Types.ViewBox box1 = null;
            if (this.pretime != base.OwnerDocument.ControlTime)
            {
                box1 = TypeFunc.ParseViewBox(this);
                this.ViewBox = box1;
            }
            box1 = this.ViewBox;
            parAlign align1 = parAlign.none;
            parMeetOrSlice slice1 = parMeetOrSlice.slice;
            float single1 = this.Width;
            float single2 = this.Height;
            float single3 = 0f;
            float single4 = 0f;
            if (box1 != null)
            {
                single1 = box1.width;
                single2 = box1.height;
                single3 = box1.min_x;
                single4 = box1.min_y;
                PreserveAspectRatio ratio1 = box1.psr;
                align1 = ratio1.Align;
                slice1 = ratio1.Mos;
            }
            float single9 = this.width / single1;
            float single10 = this.height / single2;
            GraphicsContainer container1 = g.BeginContainer();
            g.SmoothingMode = base.OwnerDocument.SmoothingMode;
            if (single9 >= single10)
            {
                single12 = single9;
                single13 = single10;
            }
            else
            {
                single12 = single10;
                single13 = single9;
            }
            if (slice1 == parMeetOrSlice.meet)
            {
                single11 = single13;
                single7 = single1 * single11;
                single8 = single2 * single11;
                switch (align1)
                {
                    case parAlign.xMinYMin:
                    {
                        single5 = this.X - single3;
                        single6 = this.Y - single4;
                        goto Label_03D1;
                    }
                    case parAlign.xMidYMin:
                    {
                        single5 = ((this.Width - single7) / 2f) - single3;
                        single6 = this.Y - single4;
                        goto Label_03D1;
                    }
                    case parAlign.xMaxYMin:
                    {
                        single5 = (this.Width - single7) - single3;
                        single6 = this.Y - single4;
                        goto Label_03D1;
                    }
                    case parAlign.xMinYMid:
                    {
                        single5 = this.X - single3;
                        single6 = ((this.Height - single8) / 2f) - single4;
                        goto Label_03D1;
                    }
                    case parAlign.xMidYMid:
                    {
                        single5 = ((this.Width - single7) / 2f) - single3;
                        single6 = ((this.Height - single8) / 2f) - single4;
                        goto Label_03D1;
                    }
                    case parAlign.xMaxYMid:
                    {
                        single5 = (this.Width - single7) - single3;
                        single6 = ((this.Height - single8) / 2f) - single4;
                        goto Label_03D1;
                    }
                    case parAlign.xMinYMax:
                    {
                        single5 = this.X - single3;
                        single6 = (this.Height - single8) - single4;
                        goto Label_03D1;
                    }
                    case parAlign.xMidYMax:
                    {
                        single5 = ((this.Width - single7) / 2f) - single3;
                        single6 = (this.Height - single8) - single4;
                        goto Label_03D1;
                    }
                    case parAlign.xMaxYMax:
                    {
                        single5 = (this.Width - single7) - single3;
                        single6 = (this.Height - single8) - single4;
                        goto Label_03D1;
                    }
                }
                single5 = this.X - single3;
                single6 = this.Y - single4;
                single7 = single1;
                single8 = single2;
            }
            else
            {
                single11 = single12;
                single7 = this.Width / single11;
                single8 = this.Height / single11;
                switch (align1)
                {
                    case parAlign.xMinYMin:
                    {
                        single5 = this.X + single3;
                        single6 = this.Y + single4;
                        goto Label_03D1;
                    }
                    case parAlign.xMidYMin:
                    {
                        single5 = ((single1 - single7) / 2f) + single3;
                        single6 = this.Y + single4;
                        goto Label_03D1;
                    }
                    case parAlign.xMaxYMin:
                    {
                        single5 = (single1 - single7) + single3;
                        single6 = this.Y + single4;
                        goto Label_03D1;
                    }
                    case parAlign.xMinYMid:
                    {
                        single5 = this.X + single3;
                        single6 = ((single2 - single8) / 2f) + single4;
                        goto Label_03D1;
                    }
                    case parAlign.xMidYMid:
                    {
                        single5 = ((single1 - single7) / 2f) + single3;
                        single6 = ((single2 - single8) / 2f) + single4;
                        goto Label_03D1;
                    }
                    case parAlign.xMaxYMid:
                    {
                        single5 = (single1 - single7) + single3;
                        single6 = ((single2 - single8) / 2f) + single4;
                        goto Label_03D1;
                    }
                    case parAlign.xMinYMax:
                    {
                        single5 = this.X + single3;
                        single6 = (single2 - single8) + single4;
                        goto Label_03D1;
                    }
                    case parAlign.xMidYMax:
                    {
                        single5 = ((single1 - single7) / 2f) + single3;
                        single6 = (single2 - single8) + single4;
                        goto Label_03D1;
                    }
                    case parAlign.xMaxYMax:
                    {
                        single5 = (single1 - single7) + single3;
                        single6 = (single2 - single8) + single4;
                        goto Label_03D1;
                    }
                }
                single5 = this.X + single3;
                single6 = this.Y + single4;
                single7 = single1;
                single8 = single2;
            }
        Label_03D1:
            matrix1 = base.GraphTransform.Matrix;
            if ((this.Width != 0f) && (this.Height != 0f))
            {
                float single14 = this.width / single7;
                float single15 = this.height / single8;
//                g.ScaleTransform(single14, single15);
                matrix1.Scale(single14, single15);
            }
            if (base.OwnerDocument.DocumentElement == this)
            {
//              g.TranslateTransform(single5, single6);
                matrix1.Translate(single5, single6);
            }
            else
            {
//                g.TranslateTransform(single5 - this.x, single6 - this.y);
                matrix1.Translate(single5 - this.x, single6 - this.y);
            }
			foreach(Layer layer in OwnerDocument.Layers)
			{
				if(!layer.Visible)continue;

				SvgElementCollection.ISvgElementEnumerator enumerator1 = layer.GraphList.GetEnumerator();
				contectbounds=RectangleF.Empty;
				while (enumerator1.MoveNext())
				{
					IGraph graph1 = (IGraph) enumerator1.Current;
					graph1.ShowConnectPoints=base.OwnerDocument.ShowConnectPoints;
					try
					{
						//						if(graph1.LimitSize)
						//						{
						//							PointF [] points = {new PointF( matrix1.OffsetX,matrix1.OffsetY)};
						//							matrix1.TransformVectors(points);
						//							graph1.GraphTransform.Matrix = new Matrix(1,0,0,1,points[0].X,points[0].Y);
						//						}
						//						else
						graph1.GraphTransform.Matrix = matrix1.Clone();
						using(Matrix matrix2 =new Matrix())
						{
							if(graph1 is ConnectLine){graph1.Draw(g, time);continue;}
							matrix2.Multiply(matrix1, MatrixOrder.Prepend);
							matrix2.Multiply(graph1.Transform.Matrix,MatrixOrder.Prepend);
							//(graph1 as ConnectLine).UpatePath(g);
							RectangleF rtf1=graph1.GPath.GetBounds(matrix2);
							if(!(graph1 is Text )  )
							{rtf1.Width++;rtf1.Height++;}
							if (g.IsVisible(rtf1)||(rtf1==RectangleF.Empty)||(graph1 is ConnectLine && graph1.GPath.PointCount==0))
							{
								graph1.Draw(g, time);  								
							}
							else
							{
								graph1.GraphTransform.Matrix.Multiply(graph1.Transform.Matrix,MatrixOrder.Prepend);
#if debug
								int i=0;
								i++;
#endif
							}
						}
					
					}
					catch (Exception e)
					{                    
					}
					finally
					{
						graph1=null;
					}
				}
			}
            this.pretime = time;
            g.EndContainer(container1);
        }
		public override GraphicsPath GPath
		{
			get
			{
				return base.graphPath;
			}
			set
			{
			}
		}
		public override RectangleF GetBounds()
		{
			return ContentBounds;
		}

		public RectangleF ContentBounds
		{
			get
			{
				if(contectbounds==RectangleF.Empty)
				{
					SvgElementCollection.ISvgElementEnumerator enumerator1 = base.GraphList.GetEnumerator();
					Region region=new Region();
					while (enumerator1.MoveNext())
					{
						IGraph graph1 = (IGraph) enumerator1.Current;
						RectangleF rtf1=graph1.GPath.GetBounds(graph1.Transform.Matrix);
						if(rtf1==RectangleF.Empty)continue;
						if(contectbounds==RectangleF.Empty)
						{
							contectbounds=graph1.GPath.GetBounds(graph1.Transform.Matrix);
						}
						else
						{
							contectbounds= RectangleF.Union(contectbounds,rtf1);
						}
					}
				}
				return this.contectbounds;
			}
		}
		
        // Properties
        public float Height
        {
            get
            {
                if (base.SvgAnimAttributes.ContainsKey("height"))
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
                this.height = value;
                AttributeFunc.SetAttributeValue(this, "height", this.height.ToString());
            }
        }

        public ItopVector.Core.Types.ViewBox ViewBox
        {
            get
            {
                if (this.pretime != base.OwnerDocument.ControlTime)
                {
                    this.viewBox = TypeFunc.ParseViewBox(this);
                }
                return this.viewBox;
            }
            set
            {
                this.viewBox = value;
            }
        }

        public float Width
        {
            get
            {
                if (base.SvgAnimAttributes.ContainsKey("width"))
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
                this.width = value;
                AttributeFunc.SetAttributeValue(this, "width", this.width.ToString());
            }
        }

        public float X
        {
            get
            {
                if (base.SvgAnimAttributes.ContainsKey("x"))
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
                this.x = value;
                AttributeFunc.SetAttributeValue(this, "x", this.x.ToString());
            }
        }

        public float Y
        {
            get
            {
                if (base.SvgAnimAttributes.ContainsKey("y"))
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
                this.y = value;
                AttributeFunc.SetAttributeValue(this, "y", this.y.ToString());
            }
        }

		public override System.Xml.XmlNode RemoveChild(System.Xml.XmlNode oldChild)
		{			
			if(oldChild is IGraph)
			{
				(oldChild as IGraph).Layer.Remove(oldChild as SvgElement);
			}
			return base.RemoveChild (oldChild);
		}
		public override System.Xml.XmlNode AppendChild(System.Xml.XmlNode newChild)
		{
			if(newChild is IGraph && !(newChild is Symbol))
			{
				(newChild as IGraph).Layer.Add(newChild as SvgElement);
			}
			return base.AppendChild (newChild);
		}
		public override System.Xml.XmlNode InsertAfter(System.Xml.XmlNode newChild, System.Xml.XmlNode refChild)
		{
			if(newChild is IGraph && !(newChild is Symbol))
			{
				ILayer layer = (newChild as IGraph).Layer;
				int index =0;
				if(layer!=null)
					index =layer.GraphList.IndexOf(refChild as SvgElement);
				index = index>0?index+1:1;

                layer.GraphList.Insert(index,newChild as SvgElement);

			}
			return base.InsertAfter (newChild, refChild);
		}
		public override System.Xml.XmlNode InsertBefore(System.Xml.XmlNode newChild, System.Xml.XmlNode refChild)
		{
			if(newChild is IGraph && !(newChild is Symbol))
			{
				ILayer layer = (newChild as IGraph).Layer;
				
				int index =0;
				if(layer==null)
				{
					layer = OwnerDocument.Layers[SvgDocument.currentLayer] as ILayer;					
				}				
				index =layer.GraphList.IndexOf(refChild as SvgElement);
				index = index>=0?index:0;
				layer.GraphList.Insert(index,newChild as SvgElement);
			}
			return base.InsertBefore (newChild, refChild);
		}





        // Fields
//        private SvgElementCollection DefsList;
        public bool FullScreen;
        private float height;
        private ItopVector.Core.Types.ViewBox viewBox;
        private float width;
        private float x;
        private float y;
		private RectangleF contectbounds;
    }
}

