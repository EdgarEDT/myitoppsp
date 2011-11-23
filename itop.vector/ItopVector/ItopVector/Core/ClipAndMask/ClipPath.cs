namespace ItopVector.Core.ClipAndMask
{
    using ItopVector.Core;
    using ItopVector.Core.Document;
    using ItopVector.Core.Figure;
    using ItopVector.Core.Func;
    using ItopVector.Core.Interface.Figure;
    using ItopVector.Core.Types;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Xml;

    public class ClipPath : Group
    {
        // Methods
        internal ClipPath(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
        {
            this.gp = null;
            this.cliprule = null;
            this.showClip = true;
            this.graphPath = new GraphicsPath();
            this.RefMatrix = new Matrix();
        }

        public static void Clip(Graphics g, int time, IGraph graph)
        {
            ClipPath path1 = graph.ClipPath;
            if ((path1 != null) && path1.ShowClip)
            {
                GraphicsPath path2 = path1.GetGraphicsPath(g, time);
                if (path1.ClipPathUnit == Units.ObjectBoundingBox)
                {
                    GraphicsPath path3 = (GraphicsPath) graph.GPath.Clone();
                    RectangleF ef1 = path3.GetBounds();
                    float single1 = ef1.Left;
                    RectangleF ef2 = path3.GetBounds();
                    float single2 = ef2.Top;
                    RectangleF ef3 = path3.GetBounds();
                    float single3 = ef3.Width;
                    RectangleF ef4 = path3.GetBounds();
                    float single4 = ef4.Height;
                    Matrix matrix1 = new Matrix();
                    matrix1.Scale(single3, single4);
                    path2.Transform(matrix1);
                    g.SetClip(path2, CombineMode.Intersect);
                    g.TranslateClip(single1, single2);
                }
                else
                {
                    g.SetClip(path2, CombineMode.Intersect);
                }
            }
        }

        public override void Draw(Graphics g, int time)
        {
            Matrix matrix1 = base.GraphTransform.Matrix.Clone();
            if (this.pretime != time)
            {
                this.graphPath.Reset();
            }
            SvgElementCollection.ISvgElementEnumerator enumerator1 = base.GraphList.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                IGraph graph1 = (IGraph) enumerator1.Current;
                graph1.GraphTransform.Matrix = matrix1.Clone();
                graph1.Draw(g, time);
                if ((this.pretime != time) && !base.OwnerDocument.PlayAnim)
                {
                    GraphicsPath path1 = (GraphicsPath) graph1.GPath.Clone();
                    path1.Transform(graph1.Transform.Matrix);
                    this.graphPath.AddPath(path1, false);
                }
            }
            this.pretime = time;
        }

        public static void DrawClip(Graphics g, int time, IGraph graph)
        {
            ClipPath path1 = graph.ClipPath;
            if (path1 != null)
            {
                path1.GraphTransform.Matrix = graph.GraphTransform.Matrix.Clone();
                if (!path1.ShowClip)
                {
                    path1.RefMatrix = graph.Transform.Matrix.Clone();
                    path1.Draw(g, time);
                }
            }
        }

        public GraphicsPath GetGraphicsPath(Graphics g, int time)
        {
            if ((this.gp == null) || (this.pretime != base.OwnerDocument.ControlTime))
            {
                this.gp = new GraphicsPath();
                foreach (XmlNode node1 in this.ChildNodes)
                {
                    if (node1 is IGraph)
                    {
                        IGraph graph1 = (IGraph) node1;
                        int num1 = 0;
                        int num2 = 0;
                        AnimFunc.CreateAnimateValues(graph1, time, out num1, out num2);
                        ClipPath.Clip(g, time, graph1);
                        GraphicsPath path1 = (GraphicsPath) graph1.GPath.Clone();
                        path1.Transform(graph1.Transform.Matrix);
                        this.gp.FillMode = (this.ClipRule == "evenodd") ? FillMode.Alternate : FillMode.Winding;
                        this.gp.AddPath(path1, true);
                    }
                }
            }
            return this.gp;
        }

        public void UpdateChild(XmlNode node)
        {
            if (node is IGraph)
            {
                IGraph graph1 = (IGraph) node;
                SvgDocument document1 = base.OwnerDocument;
                bool flag1 = document1.AcceptChanges;
                document1.AcceptChanges = false;
                if (!this.RefMatrix.IsIdentity)
                {
                    Matrix matrix1 = this.RefMatrix.Clone();
                    matrix1.Invert();
                    Transf transf1 = new Transf();
                    transf1.setMatrix(matrix1);
                    graph1.Transform = transf1;
                }
                document1.AcceptChanges = flag1;
            }
        }


        // Properties
        public override bool CanAnimate
        {
            get
            {
                return false;
            }
        }

        public Units ClipPathUnit
        {
            get
            {
                if ((this.pretime != base.OwnerDocument.ControlTime) && !base.OwnerDocument.PlayAnim)
                {
                    string text1 = AttributeFunc.FindAttribute("clipPathUnits", this).ToString().Trim();
                    this.clipPathUnit = (text1 == "objectBoundingBox") ? Units.ObjectBoundingBox : Units.UserSpaceOnUse;
                }
                return this.clipPathUnit;
            }
            set
            {
                if (this.clipPathUnit != value)
                {
                    this.clipPathUnit = value;
                    AttributeFunc.SetAttributeValue(this, "clipPathUnits", this.clipPathUnit.ToString());
                }
            }
        }

        private string ClipRule
        {
            get
            {
                if ((this.pretime != base.OwnerDocument.ControlTime) && !base.OwnerDocument.PlayAnim)
                {
                    this.cliprule = AttributeFunc.FindAttribute("clip-rule", this).ToString();
                }
                return this.cliprule;
            }
        }

        public new bool ShowClip
        {
            get
            {
                return this.showClip;
            }
            set
            {
                this.showClip = value;
            }
        }


        // Fields
        private Units clipPathUnit;
        private string cliprule;
        private GraphicsPath gp;
        public Matrix RefMatrix;
        private bool showClip;
    }
}

