using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using ItopVector.Core.ClipAndMask;
using ItopVector.Core.Document;
using ItopVector.Core.Func;
using ItopVector.Core.Interface.Figure;
using ItopVector.Core.Types;
using System;

namespace ItopVector.Core.Figure
{
	/// <summary>
	/// 图元基类
	/// </summary>
	public class Graph : SvgElement, IGraph
	{
		// Methods
		internal Graph(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
		{
//			this.transform = new Transf();
			this.visible = true;
			this.showBound = false;
			this.graphTransform = new Transf();
			this.tempOpacity = 1f;
			this.tempFillOpacity = 1f;
			this.tempStrokeOpacity = 1f;
			this.boundColor = Color.Empty;
			this.isLock = false;
			this.drawVisible = true;
			this.pointsinfo = new PointInfoCollection();
			this.pointsinfo.OwerGraph = this;
			this.IsClip = false;
			this.Changed = true;
			this.connectPoints = new PointF[0];
			this.connectLines = new SvgElementCollection();
			this.canSelect=true;
			this.limitSize =false;
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
		public virtual PointF CenterPoint
		{
			get{
				RectangleF rtf =this.GPath.GetBounds(this.Transform.Matrix);
				return new PointF(rtf.X+rtf.Width/2,rtf.Y +rtf.Height/2);				
			}
			set{
				if (this is Use)
				{
					PointF pf1= CenterPoint;
					if(pf1==value)return;
					PointF pf2=value;
					Use use =this as Use;
					bool flag = this.OwnerDocument.AcceptChanges;
					this.OwnerDocument.AcceptChanges =false;
					use.X +=pf2.X - pf1.X;
					use.Y +=pf2.Y - pf1.Y;
					use.NotifyChange();
					this.OwnerDocument.AcceptChanges =flag;
				}
			}
		}
		public virtual bool LimitSize
		{
			get{return limitSize;}
			set{limitSize =value;}
		}
		/// <summary>
		/// 能否选中
		/// </summary>
		public bool CanSelect
		{
			get{return canSelect;}
			set{canSelect=value;}

		}
		/// <summary>
		/// 图元绘制方法
		/// </summary>
		/// <param name="g"></param>
		/// <param name="time"></param>
		public virtual void Draw(Graphics g, int time)
		{
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

		public virtual void NotifyChange()
		{
			this.Changed = true;
			foreach (ConnectLine connect in this.connectLines)
			{
				connect.pretime = -1;
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
		/// 图元范围矩形
		/// </summary>
		/// <returns></returns>
		public virtual RectangleF GetBounds()
		{
			return RectangleF.Empty;
		}

		/// <summary>
		/// 连接点数组
		/// </summary>
		public PointF[] ConnectPoints
		{
			get { return this.connectPoints; }
		}

		/// <summary>
		/// 解析子元件
		/// </summary>
		/// <param name="g"></param>
		/// <param name="time"></param>
		public virtual void ParseChild(Graphics g, int time)
		{
		}


		// Properties
		/// <summary>
		/// 填充色
		/// </summary>
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

		/// <summary>
		/// 修剪路径
		/// </summary>
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

		/// <summary>
		/// 元件是否可视
		/// </summary>
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
				}
			}
		}

		public virtual bool IsChanged
		{
			get { return this.Changed; }
			set { this.Changed = value; }
		}


		/// <summary>
		/// 元件路径
		/// </summary>
		public virtual GraphicsPath GPath
		{
			get { return new GraphicsPath(); }
			set
			{
			}
		}

		/// <summary>
		/// 元件二维变换矩阵
		/// </summary>
		public Transf GraphTransform
		{
			get { return this.graphTransform; }
			set { this.graphTransform = value; }
		}

		/// <summary>
		/// 是否加锁
		/// </summary>
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
				}
			}
		}

		/// <summary>
		/// 元件点集合
		/// </summary>
		public PointInfoCollection PointsInfo
		{
			get { return this.pointsinfo; }
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
					matrix1 = ((Matrix) this.svgAnimAttributes["transform"]).Clone();
				}
				Transf transf1 = new Transf();
				transf1.setMatrix(matrix1);
				return transf1;
			}
			set { AttributeFunc.SetAttributeValue(this, "transform", value.ToString()); }
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
				if (this.Visible != value)
				{
					this.visible = value;
					AttributeFunc.SetAttributeValue(this, "visibility", value ? "visible" : "hidden");
				}
			}
		}

		

		public virtual bool ShowConnectPoints
		{
			get { return this.showConnectPoints; }
			set { this.showConnectPoints = value; }
		}

		private bool showConnectPoints = false;

		public virtual ILayer Layer
		{
			get{
				if(layer==null || !layer.ID.Equals(GetAttribute("layer")) )
				{

					try
					{
						layer = OwnerDocument.Layers[GetAttribute("layer")] as ILayer;					
					}
					catch(Exception e)
					{
                        System.Windows.Forms.MessageBox.Show(e.Message);
					}
				}
				return layer;}
			set{
				if(this.layer==value)return;
				if(layer!=null)
					layer.Remove(this);				
				layer =value;
				layer.Add(this);
				AttributeFunc.SetAttributeValue(this,"layer",layer.ID);
			}
		}
		public virtual string LabelText
		{
			get
			{
				string text1 = AttributeFunc.ParseAttribute("labeltext", this, false).ToString();
				return text1;
			}
			set{
			AttributeFunc.SetAttributeValue(this,"labeltext",value);
			}

		}
		// Fields
		private ILayer layer;
		private Color boundColor;
		private bool drawVisible;
		private Transf graphTransform;
		public bool IsClip;
		private bool isLock;
		private PointInfoCollection pointsinfo;
		private bool showBound;
		private float tempFillOpacity;
		private float tempOpacity;
		private float tempStrokeOpacity;
//		private Transf transform;
		protected bool visible;
		private bool Changed;
		protected PointF[] connectPoints;
		private SvgElementCollection connectLines;
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