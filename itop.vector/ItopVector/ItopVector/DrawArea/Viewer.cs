using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ItopVector.Core;
using ItopVector.Core.Animate;
using ItopVector.Core.Document;
using ItopVector.Core.Figure;
using ItopVector.Core.Func;
using ItopVector.Core.Interface;
using ItopVector.Core.Interface.Figure;

namespace ItopVector.DrawArea
{
	[ToolboxItem(false)]
	public  class Viewer : Control
    {
        // Methods
        public Viewer()
        {
            this.components = null;
            this.svgDocument = null;
            this.onlyDrawCurrent = false;
            this.margin = new SizeF(500f, 500f);
            this.virtualTop = 0f;
            this.virtualLeft = 0f;
            this.scale = 1f;
            this.selectpath = new GraphicsPath();
            this.selectMatrix = new Matrix();
            this.elementList = new SvgElementCollection();
            this.selectChanged = false;
            this.changeelements = new SvgElementCollection();
            this.drawArea = null;
            this.oldselectPoint = PointF.Empty;
            this.Bouns = new PointF[8];
            this.InitializeComponent();
//            base.SetStyle(ControlStyles.DoubleBuffer | (ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint), true);
			base.SetStyle(ControlStyles.DoubleBuffer | (ControlStyles.AllPaintingInWmPaint | (ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint)), true);
//			this.BackColor = Color.Red;
        }

        public Viewer(SvgDocument doc):this()
        {            
            this.svgDocument = doc;
            doc.OnDocumentChanged += new OnDocumentChangedEventHandler(this.DocumentChanged);
        }

        private void ChangeControlTime(object sender, int oldtime, int newtime)
        {
            base.Invalidate();
            this.selectChanged = true;
        }

        private void ChangeRecordAnim(object sender, EventArgs e)
        {
        	SvgElementCollection.ISvgElementEnumerator enumerator1 = this.svgDocument.SelectCollection.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                SvgElement element1 = (SvgElement) enumerator1.Current;
                this.InvalidateElement(element1);
            }
        }

        private void ChangeSelect(object sender, CollectionChangedEventArgs e)
        {
            this.selectChanged = true;
            base.Invalidate();
        }

        private void DealInKeyChanged(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
			if(this.svgDocument!=null)
			{
				svgDocument.OnDocumentChanged -= new OnDocumentChangedEventHandler(this.DocumentChanged);
				svgDocument.OnControlTimeChanged -= new ControlTimeChangeEventHandler(this.ChangeControlTime);
				svgDocument.SelectCollection.OnCollectionChangedEvent -= new OnCollectionChangedEventHandler(this.ChangeSelect);
				svgDocument.OnInKeyChanged -= new EventHandler(this.DealInKeyChanged);
				svgDocument.OnRecordAnimChangedEvent -= new EventHandler(this.ChangeRecordAnim);
				svgDocument.OnNotifyChange -= new EventHandler(this.NotifyChange);
			}

        }

        private void DocumentChanged(object sender, DocumentChangedEventArgs e)
        {
            if (e.ChangeAction == ChangeAction.None)
            {
                base.Invalidate();
            }
            else if (e.ChangeElements != null)
            {
                this.changeelements.AddRange(e.ChangeElements);
            }
        }

        private GraphicsPath DrawSelectElement(IGraph path, Graphics g)
        {
            if (!path.DrawVisible)
            {
                return new GraphicsPath();
            }
            if ((path is Text) && ((((Text) path).EditMode || ((Text) path).OwnerTextElement.EditMode) || ((path is TRef) || (path is TSpan))))
            {
                return new GraphicsPath();
            }
            GraphicsPath path1 = (GraphicsPath) path.GPath.Clone();
            RectangleF ef1 = PathFunc.GetBounds(path1);
			Pen pen1;
			if(tempPen==null)
			{
				pen1= new Pen(ControlPaint.Light(Color.Blue), 1f);//wl	
			}
			else
			{
				pen1=tempPen;
			}
            pen1.Alignment = PenAlignment.Outset;
            GraphicsPath path2 = new GraphicsPath();
            Matrix matrix1 = path.GraphTransform.Matrix.Clone();
            path1.Transform(matrix1);
            g.DrawPath(pen1, path1);
            switch (this.drawArea.Operation)
            {
                case ToolOperation.FreeTransform:
                case ToolOperation.Rotate:
                case ToolOperation.Scale:
                case ToolOperation.Skew:
                {
                    if (this.svgDocument.SelectCollection.Count == 1)
                    {
                        PointF[] tfArray1;
                        RectangleF ef2 = PathFunc.GetBounds(path.GPath);
                        path2.AddRectangle(ef2);
                        path2.Transform(matrix1);
                        g.DrawPath(new Pen(ControlPaint.Light(Color.Blue)), path2);
                        tfArray1 = new PointF[8] { new PointF(ef1.X, ef1.Y), new PointF(ef1.X, ef1.Bottom), new PointF(ef1.X, ef1.Y + (ef1.Height / 2f)), new PointF(ef1.X + (ef1.Width / 2f), ef1.Y), new PointF(ef1.Right, ef1.Y), new PointF(ef1.Right, ef1.Y + (ef1.Height / 2f)), new PointF(ef1.Right, ef1.Bottom), new PointF(ef1.X + (ef1.Width / 2f), ef1.Bottom) } ;
                        this.Bouns = tfArray1;
                        path.GraphTransform.Matrix.TransformPoints(tfArray1);
                        path2.Reset();
                        for (int num1 = 0; num1 < tfArray1.Length; num1++)
                        {
                            PointF tf1 = tfArray1[num1];
                            path2.AddRectangle(new RectangleF(tf1.X - 3f, tf1.Y - 3f, 5f, 5f));
                            path2.CloseFigure();
                        }
                        g.FillPath(Brushes.White, path2);
                        g.DrawPath(new Pen(ControlPaint.Light(Color.Blue)), path2);
                    }
                    break;
                }
            }
            if (this.svgDocument.SelectCollection.Count > 1)
            {
                Matrix matrix2 = path.GraphTransform.Matrix.Clone();
                this.selectMatrix = this.drawArea.CoordTransform.Clone();
                Matrix matrix3 = this.selectMatrix.Clone();
                matrix3.Invert();
                matrix2.Multiply(matrix3, MatrixOrder.Append);
                GraphicsPath path3 = (GraphicsPath) path.GPath.Clone();
                path3.Transform(matrix2);
				matrix2.Dispose();
				matrix3.Dispose();
                return path3;
            }

            this.selectMatrix = path.GraphTransform.Matrix.Clone();
			path1.Dispose();
			matrix1.Dispose();


            return (GraphicsPath) path.GPath.Clone();
        }

        private void DrawSelection(Graphics g)
        {
//			gra1=g;
            this.selectpath.Reset();
            this.selectMatrix = new Matrix();
            if (((this.svgDocument.CurrentElement is MotionAnimate) && (this.svgDocument.SelectCollection.Count == 1)) && (this.svgDocument.CurrentElement.ParentNode is IGraph))
            {
                this.selectMatrix = ((IGraph) this.svgDocument.CurrentElement.ParentNode).GraphTransform.Matrix.Clone();
            }
            else
            {
                for (int num1 = 0; num1 < this.svgDocument.SelectCollection.Count; num1++)
                {
                    SvgElement element1 = (SvgElement) this.svgDocument.SelectCollection[num1];
                    if ((element1 is IGraph) && (element1 != this.svgDocument.DocumentElement))
                    {
                        if (element1.ParentNode == null)
                        {
                            bool flag1 = true;
                            if ((element1 is Text) && (((Text) element1).EditMode || ((Text) element1).OwnerTextElement.EditMode))
                            {
                                flag1 = false;
                            }
                            if (flag1)
                            {
                                this.svgDocument.SelectCollection.Remove(element1);
                                num1--;
                                goto Label_014B;
                            }
                        }
                        if (!((IGraph) element1).IsLock && ((IGraph) element1).DrawVisible)
                        {
                            GraphicsPath path1 = this.DrawSelectElement((IGraph) element1, g);
                            if ((path1 != null) && (path1.PointCount > 1))
                            {
                                this.selectpath.AddPath(path1, false);

                            }
                        }
                    }
                Label_014B:;
                }
                if (this.svgDocument.SelectCollection.Count > 1)
                {
                    GraphicsPath path2 = (GraphicsPath) this.selectpath.Clone();

					selectedViewRectangle=PathFunc.GetBounds(path2);

                    path2.Transform(this.selectMatrix);
                    RectangleF ef1 = PathFunc.GetBounds(path2);
					this.SelectedRectangle=ef1;
                    Pen pen1 = new Pen(Color.Blue, 1f);  //wl
                    pen1.Alignment = PenAlignment.Outset;
                    GraphicsPath path3 = new GraphicsPath();
                    g.DrawPath(pen1, path2);
                    switch (this.drawArea.Operation)
                    {
                        case ToolOperation.Select:
						case ToolOperation.WindowZoom:
						case ToolOperation.Exceptant:
                        {
                            return;
                        }
                        case ToolOperation.ShapeTransform:
                        case ToolOperation.Custom11:
                        case ToolOperation.Custom12:
                        case ToolOperation.Custom13:
                        case ToolOperation.Custom14:
                        case ToolOperation.Custom15:
                        case ToolOperation.Rectangle:
                        case ToolOperation.AngleRectangle:
                        case ToolOperation.InterEnclosure:
                        case ToolOperation.InterEnclosurePrint:
                        case ToolOperation.Circle:
                        case ToolOperation.Ellipse:
                        case ToolOperation.Line:
                        case ToolOperation.PolyLine:
						case ToolOperation.XPolyLine:
						case ToolOperation.YPolyLine:
						case ToolOperation.LeadLine:
						case ToolOperation.Confines_GuoJie:
						case ToolOperation.Confines_ShengJie:
						case ToolOperation.Confines_ShiJie:
						case ToolOperation.Confines_XianJie:
						case ToolOperation.Confines_XiangJie:
						case ToolOperation.Railroad:
                        case ToolOperation.Polygon:
                        case ToolOperation.EqualPolygon:
                        case ToolOperation.Bezier:
                        case ToolOperation.FreeLines:
                        case ToolOperation.Text:
                        case ToolOperation.Image:
                        case ToolOperation.ColorSelect:
                        case ToolOperation.AreaSelect:
                        {
                            return;
                        }
                        case ToolOperation.FreeTransform:
                        case ToolOperation.Rotate:
                        case ToolOperation.Scale:
                        case ToolOperation.Skew:
                        {
                            g.DrawRectangle(new Pen(ControlPaint.Light(Color.Black)), ef1.X, ef1.Y, ef1.Width, ef1.Height);
                            PointF[] tfArray2 = new PointF[8] { new PointF(ef1.X, ef1.Y), new PointF(ef1.X, ef1.Bottom), new PointF(ef1.X, ef1.Y + (ef1.Height / 2f)), new PointF(ef1.X + (ef1.Width / 2f), ef1.Y), new PointF(ef1.Right, ef1.Y), new PointF(ef1.Right, ef1.Y + (ef1.Height / 2f)), new PointF(ef1.Right, ef1.Bottom), new PointF(ef1.X + (ef1.Width / 2f), ef1.Bottom) } ;
                            PointF[] tfArray1 = tfArray2;
                            path3.Reset();
                            PointF[] tfArray3 = tfArray1;
                            for (int num2 = 0; num2 < tfArray3.Length; num2++)
                            {
                                PointF tf1 = tfArray3[num2];
                                path3.AddRectangle(new RectangleF(tf1.X - 3f, tf1.Y - 3f, 5f, 5f));
                                path3.CloseFigure();
                            }
                            g.FillPath(Brushes.White, path3);
                            g.DrawPath(new Pen(ControlPaint.Light(Color.Blue)), path3);
                            return;
                        }
                        case ToolOperation.Roam:
                        case ToolOperation.IncreaseView:
                        case ToolOperation.DecreaseView:
                        case ToolOperation.None:
                        {
                            return;
                        }
                    }
					path2.Dispose();
					path3.Dispose();
                }
            }
        }

        private void InitializeComponent()
        {
            this.AllowDrop = true;
        }

        public void InvalidateElement(ISvgElement element)
        {
            if (element is IGraph)
            {
                GraphicsPath path1 = (GraphicsPath) ((IGraph) element).GPath.Clone();
                path1.Transform(((IGraph) element).GraphTransform.Matrix);
                RectangleF ef1 = path1.GetBounds();
                base.Invalidate(new Rectangle(((int) ef1.X) - 30, ((int) ef1.Y) - 30, ((int) ef1.Width) + 60, ((int) ef1.Height) + 60));
            }
        }

        private void NotifyChange(object sender, EventArgs e)
        {
        	SvgElementCollection.ISvgElementEnumerator enumerator1 = this.changeelements.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                SvgElement element1 = (SvgElement) enumerator1.Current;
                this.InvalidateElement(element1);
            }
            this.changeelements.Clear();
        }
	
		public Graphics tempGra()
		{
			Graphics g=this.CreateGraphics();
		  
			Matrix matrix1 = new Matrix();
			matrix1.Translate(-this.virtualLeft, -this.virtualTop);
			matrix1.Translate(this.margin.Width, this.margin.Height);
			matrix1.Scale(this.scale, this.scale);
			g.Transform=matrix1;
			return g;

		}
		public void MovePicture(float left,float top,bool move)
		{

			this.virtualLeft = Math.Max(0,left);
			this.virtualTop = Math.Max(0,top);
			offx = nleft - (int)left;
			offy = ntop - (int)top;
			if(ViewChanged!=null)
				ViewChanged(this,new ViewChangedEventArgs(this));

			if (!move) 
			{
				base.Invalidate();
                
			}
		}
		System.Drawing.Image image =null;
		int offx=0;
		int offy=0;
//		int oldOffx=0;
//		int oldOffy=0;
		int nleft=0;
		int ntop =0;
		bool beginMove =false;
		int nRule=16;

		public void BeginMove()
		{
			nleft = (int)virtualLeft;
			ntop = (int)virtualTop;
			if (drawArea.DrawMode== DrawModeType.ScreenImage)
				image = GetControlBmp();
			
			beginMove=true;
		}
		public  void MovePic(int x,int y)
		{
			base.Invalidate();
		}
		public void EndMove()
		{			
			beginMove=false;
			GC.Collect();
		}

		[System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
		public static extern bool BitBlt(
			IntPtr hdcDest, //目标设备的句柄 
			int nXDest, // 目标对象的左上角的X坐标 
			int nYDest, // 目标对象的左上角的Y坐标 
			int nWidth, // 目标对象的矩形的宽度 
			int nHeight, // 目标对象的矩形的长度 
			IntPtr hdcSrc, // 源设备的句柄 
			int nXSrc, // 源对象的左上角的X坐标 
			int nYSrc, // 源对象的左上角的Y坐标 
			System.Int32 dwRop // 光栅的操作值 
			);
		public  Bitmap GetControlBmp()
		{
			nRule =this.drawArea.ShowRule?16:0;
			//建立屏幕Graphics
			Graphics grpScreen = Graphics.FromHwnd(this.drawArea.Parent.Handle);
			//根据屏幕大小建立位图
			Bitmap bitmap = new Bitmap(Width  , Height ,grpScreen);
			//建立位图相关Graphics
			Graphics grpBitmap = Graphics.FromImage(bitmap);
			//建立屏幕上下文
			IntPtr hdcScreen = grpScreen.GetHdc();
			//建立位图上下文
			IntPtr hdcBitmap = grpBitmap.GetHdc();
			//将屏幕捕获保存在图位中
			BitBlt(hdcBitmap, nRule, nRule, bitmap.Width-nRule , bitmap.Height-nRule, hdcScreen, nRule, nRule, 0x00CC0020);
			//关闭位图句柄
			grpBitmap.ReleaseHdc(hdcBitmap);
			//关闭屏幕句柄
			grpScreen.ReleaseHdc(hdcScreen);
			//释放位图对像
			grpBitmap.Dispose();
			//释放屏幕对像
			grpScreen.Dispose();

			//返回捕获位图
			return bitmap;
		}
		protected override void OnResize(EventArgs e)
		{
			image=null;
			base.OnResize (e);
			
		}

		Graphics MG=null;
		protected override void OnPaint(PaintEventArgs e)
		{
			if (this.DesignMode) return;
			if (this.drawArea.DrawMode== DrawModeType.Normal)image=null;
			if (this.drawArea.CurrentOperation ==  ToolOperation.Roam && image!=null && beginMove)
			{
				e.Graphics.DrawImage(image,offx,offy);
						
				return;
			}
			else
			{
				Graphics g = e.Graphics;

				if (this.drawArea.DrawMode==DrawModeType.MemoryImage)
				{
					if(image ==null)
					{
						image = new Bitmap(Width,Height);
						MG = Graphics.FromImage(image);
					}					
					g = MG;
					g.Clip = new Region(new Rectangle(0, 0, Width, Height));

				}
				//拖放结束执行
				if (image!=null&&(Math.Abs(offx) > 0 || Math.Abs(offy) > 0) )
				{
					if(drawArea.DrawMode == DrawModeType.ScreenImage)
					{
						g.DrawImage(image, offx, offy);
						Rectangle rt1 = new Rectangle(nRule, nRule, image.Width-nRule, image.Height-nRule);
						Rectangle rt2 = rt1;
						Rectangle rt3 = rt1;
						rt3.Offset(offx, offy);
						rt2.Intersect(rt3);
						Region rg = new Region(rt1);
						rg.Xor(rt2);
						g.Clip = rg;
						offx = offy = 0;
						image = null;
					}
					else
					{
						System.Drawing.Image image2 = new Bitmap(image.Width, image.Height);
						g = Graphics.FromImage(image2);
						g.DrawImage(image, offx, offy);      

						Rectangle rt1 = new Rectangle(0, 0, image.Width, image.Height);
						Rectangle rt2 = rt1;
						Rectangle rt3 = rt1;
						rt3.Offset(offx, offy);
						rt2.Intersect(rt3);
						g.SetClip(rt2, CombineMode.Exclude);

						offx = offy = 0;
						image = image2;
						MG=g;
					}
				} 
				else if (this.drawArea.DrawMode==DrawModeType.MemoryImage){g.Clear(Color.Transparent);
				}
				DateTime time1=DateTime.Now;
				if (this.svgDocument != null)
				{			
					Matrix matrix1 = new Matrix();
					matrix1.Translate(-this.virtualLeft, -this.virtualTop);
					matrix1.Translate(this.margin.Width, this.margin.Height);
					matrix1.Scale(this.scale, this.scale);
					GraphicsContainer container1 = g.BeginContainer();
					g.SmoothingMode = this.svgDocument.SmoothingMode;

					if (this.svgDocument.RootElement is Group)
					{
						Group group2 = (Group) this.svgDocument.RootElement;
						group2.GraphTransform.Matrix = matrix1.Clone();
						try
						{
							group2.Draw(g, this.svgDocument.ControlTime);
						}
						catch
						{}
						this.elementList = group2.GraphList;                    
					}
					g.EndContainer(container1);
					DateTime time2=DateTime.Now;

					this.DrawSelection(g);
                
					if (this.selectChanged)
					{                   
						RectangleF ef1 = this.selectpath.GetBounds();
                    			
						this.oldselectPoint = new PointF(ef1.X + (ef1.Width / 2f), ef1.Y + (ef1.Height / 2f));
				
						this.selectChanged = false;
					}
					if ( !this.oldselectPoint.IsEmpty 	)
					{                    
						PointF[] tfArray1 = new PointF[1] { this.oldselectPoint } ;
						this.selectMatrix.TransformPoints(tfArray1);
						this.drawArea.CenterPoint = tfArray1[0];
						if(this.drawArea.Operation==ToolOperation.FreeTransform)
						{
							g.SmoothingMode =SmoothingMode.HighQuality;
							PointF tf1 = this.drawArea.CenterPoint;
							GraphicsPath path2 = new GraphicsPath();
							path2.AddEllipse(tf1.X - 2.5f, tf1.Y - 2.5f, 5f, 5f);
							g.FillPath(Brushes.White, path2);					
							g.DrawPath(Pens.Blue, path2);
							path2.Dispose();
						}
					}
					if (this.drawArea.ShowGuides)
					{
						foreach (RefLine line1 in this.drawArea.RefLines)
						{
							PointF tf2 = PointF.Empty;
							//                        int num2 = base.Width;
							if (line1.Hori)
							{
								tf2 = new PointF(0f, (float) line1.Pos);
							}
							else
							{
								tf2 = new PointF((float) line1.Pos, 0f);
								//                            num2 = base.Height;
							}
							tf2 = this.drawArea.PointToSystem(tf2);
							Pen pen1=new Pen(Color.Blue);
							pen1.DashStyle=DashStyle.Dash;
							if (line1.Hori)
							{
								g.DrawLine(pen1, 0f, tf2.Y, (float) base.Width, tf2.Y);
								continue;
							}
							g.DrawLine(pen1, tf2.X, 0f, tf2.X, (float) base.Height);
							pen1.Dispose();
						}
					}
					matrix1.Dispose();
					if(drawArea.DrawMode == DrawModeType.MemoryImage)
					{
                        e.Graphics.DrawImage(image,0,0);
					}
				}
				PointF pf= this.drawArea.GetCenterPoint();
				this.drawArea.ToolTip(string.Format("x:{0},y:{1}",pf.X,pf.Y),2);
//				drawCount++;
//				time2=DateTime.Now;
//				TimeSpan ts=time2-time1;
//				this.drawArea.ToolTip(this.svgDocument.SelectCollection.Count+","+this.ElementList.Count+","+ drawCount+","+ts.Seconds+":"+ts.Milliseconds+","+this.virtualLeft+":"+this.virtualTop,2);

			}
		}
//		 int drawCount=0;


        // Properties
        public SvgElementCollection ElementList
        {
            get
            {
                return this.elementList;
            }
        }

        public bool OnlyDrawCurrent
        {
            set
            {
                if (this.onlyDrawCurrent != value)
                {
                    this.onlyDrawCurrent = value;
                    base.Invalidate();
                }
            }
        }

		/// <summary>
		/// 视图缩放大小
		/// </summary>
        public float ScaleUnit
        {
            set
            {
                if (this.scale != value)
                {
                    this.scale = value;
					if(ViewChanged!=null)
						ViewChanged(this,new ViewChangedEventArgs(this));

					foreach(IGraph element in ElementList)
					{
                        if(element.LimitSize)element.NotifyChange();
					}
                    base.Invalidate();
                   
                }
            }
			get
			{
				return scale;
			}
        }

        public Matrix SelectMatrix
        {
            get
            {
                return this.selectMatrix;
            }
        }

        public GraphicsPath SelectPath
        {
            get
            {
                return this.selectpath;
            }
        }

        public SvgDocument SVGDocument
        {
            get
            {
                return this.svgDocument;
            }
            set
            {
                if (this.svgDocument != value)
                {
                    this.svgDocument = value;
                    if (value != null)
                    {
                        value.OnDocumentChanged += new OnDocumentChangedEventHandler(this.DocumentChanged);
                        value.OnControlTimeChanged += new ControlTimeChangeEventHandler(this.ChangeControlTime);
                        this.svgDocument.SelectCollection.OnCollectionChangedEvent += new OnCollectionChangedEventHandler(this.ChangeSelect);
                        value.OnInKeyChanged += new EventHandler(this.DealInKeyChanged);
                        value.OnRecordAnimChangedEvent += new EventHandler(this.ChangeRecordAnim);
                        value.OnNotifyChange += new EventHandler(this.NotifyChange);
                    }
                    base.Invalidate();
                }
            }
        }
		
        public float VirtualLeft
        {
            set
            {
                if (this.virtualLeft != value )
                {
                    this.virtualLeft =Math.Max(0, value);
					if(ViewChanged!=null)
						ViewChanged(this,new ViewChangedEventArgs(this));

                    base.Invalidate();
                }
            }
			get
			{
				return virtualLeft;
			}
        }

        public float VirtualTop
        {
            set
            {
                if (this.virtualTop != value)
                {
                    this.virtualTop = Math.Max(0, value);

					if(ViewChanged!=null)
						ViewChanged(this,new ViewChangedEventArgs(this));

                    base.Invalidate();
                }
            }
			get
			{
				return virtualTop;
			}
        }
		public Pen TempPen
		{
			set {tempPen=value;}
			get {return tempPen;}

		}
		public RectangleF SelectedRectangle
		{
			set{selectedRectangle=value;}
			get{return selectedRectangle;}
		}
		public RectangleF SelectedViewRectangle
		{
			set{selectedViewRectangle=value;}
			get{return selectedViewRectangle;}
		}
		public ToolTip ToolTip
		{
			get{return toolTip;}
			set{toolTip=value;}
		}
		public Graphics tempGrapgics
		{
			get{return gra1;}
		}

        public RectangleF ContentBounds
        {
            get
            {
                //contectbounds = RectangleF.Empty;
                //drawArea.ContentBounds;
                ////contectbounds=((SVG)svgDocument.RootElement).ContentBounds;
                //if (contectbounds == RectangleF.Empty)
                //{
                //    SvgElementCollection.ISvgElementEnumerator enumerator1 = elementList.GetEnumerator();// base.GraphList.GetEnumerator();
                //    Region region = new Region();
                //    while (enumerator1.MoveNext())
                //    {
                //        IGraph graph1 = (IGraph)enumerator1.Current;
                //        RectangleF rtf1 = graph1.GPath.GetBounds(graph1.Transform.Matrix);
                //        if (rtf1 == RectangleF.Empty) continue;
                //        if (contectbounds == RectangleF.Empty)
                //        {
                //            contectbounds = graph1.GPath.GetBounds(graph1.Transform.Matrix);
                //        }
                //        else
                //        {
                //            contectbounds = RectangleF.Union(contectbounds, rtf1);
                //        }
                //    }
                //}
                return drawArea.ContentBounds;
            }
        }
		//Events
		//视图发生变化
		public event ViewChangedEventHandler ViewChanged;
        private RectangleF contectbounds;

        // Fields
//        private Bitmap bmp;
        public PointF[] Bouns;
        private SvgElementCollection changeelements;
        private Container components;
        internal DrawArea drawArea;
        private SvgElementCollection elementList;
        public SizeF margin;
        public PointF oldselectPoint;
        private bool onlyDrawCurrent;
        private float scale;
        public bool selectChanged;
        private Matrix selectMatrix;
        private GraphicsPath selectpath;
        private SvgDocument svgDocument;
        private float virtualLeft;
        private float virtualTop;
		private Pen tempPen=null;
		private RectangleF selectedRectangle;
		private Graphics gra1=null;
		private RectangleF selectedViewRectangle;
		private ToolTip toolTip;
		
    }
}

