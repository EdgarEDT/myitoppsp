using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using ItopVector.Core.Document;
using System.IO;
using System.Drawing.Imaging;

namespace ItopVector.Core.Figure {
    public class Polygon : GraphPath {
        // Methods
        internal Polygon(string prefix, string localname, string ns, SvgDocument doc)
            : base(prefix, localname, ns, doc) {
            this.indent = 1f;
            this.LineCount = 3;
        }


        // Properties
        public override GraphicsPath GPath {
            get {
                if (this.pretime != base.OwnerDocument.ControlTime) {
                    this.graphPath = new GraphicsPath();
                    if ((this.Points != null) && (this.Points.Length > 2)) {
                        this.graphPath.AddPolygon(this.points);
                    }
                    this.CreateConnectPoint();
                }
                return this.graphPath;
            }
            set { this.graphPath = value; }
        }
        public override void Draw(Graphics g, int time) {
            if (BackgroundImage == null) {
                base.Draw(g, time);
            } else {
                if (base.DrawVisible) {
                    Matrix matrix1 = base.Transform.Matrix.Clone();

                    GraphicsContainer container1 = g.BeginContainer();

                    g.SmoothingMode = base.OwnerDocument.SmoothingMode;

                    Matrix matrix2 = base.GraphTransform.Matrix.Clone();
                    base.GraphTransform.Matrix.Multiply(matrix1, MatrixOrder.Prepend);


                    ClipAndMask.ClipPath.Clip(g, time, this);
                    bool flag1 = base.Visible;
                    if (!base.Visible) {
                        g.SetClip(Rectangle.Empty);
                    }
                    float single1 = this.Opacity;
                    if (this.svgAnimAttributes.ContainsKey("fill-opacity")) {
                        single1 = Math.Min(single1, (float)this.svgAnimAttributes["fill-opacity"]);
                    }

                    using (GraphicsPath path1 = (GraphicsPath)this.GPath.Clone()) {
                        path1.Transform(base.GraphTransform.Matrix);
                        if (!base.ShowBound) {
                            ImageAttributes imageAttributes = new ImageAttributes();
                            ColorMatrix cmatrix1 = new ColorMatrix();
                            cmatrix1.Matrix00 = 1f;
                            cmatrix1.Matrix11 = 1f;
                            cmatrix1.Matrix22 = 1f;
                            cmatrix1.Matrix33 = single1;//透明度
                            cmatrix1.Matrix44 = 1f;
                            //设置透明度                
                            imageAttributes.SetColorMatrix(cmatrix1, ColorMatrixFlag.Default, ColorAdjustType.Default);
                            TextureBrush tbush = new TextureBrush(BackgroundImage, new RectangleF(0, 0, image1.Width, image1.Height), imageAttributes);
                            tbush.WrapMode = WrapMode.Tile;
                            tbush.TranslateTransform(path1.PathPoints[0].X, path1.PathPoints[0].Y);
                            tbush.ScaleTransform(matrix2.Elements[0] / 8, matrix2.Elements[0] / 8);
                            //tbush.RotateTransform(45);
                            g.FillPath(tbush, path1);
                            ItopVector.Core.Paint.Stroke stroke1 = ItopVector.Core.Paint.Stroke.GetStroke(this);
                            this.GraphStroke.Update(this, time);
                            float penwidth = this.GraphStroke.StrokePen.Width * matrix2.Elements[0] / 6;
                            using (Pen pen1 = new Pen(this.GraphStroke.StrokeColor, penwidth)) {
                                g.DrawPath(pen1, path1);
                            }
                            //this.GraphStroke.Paint(g, this, path1, time);

                        } else {
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
        }

        public float Indent {
            get { return this.indent; }
            set { this.indent = Math.Max(0f, Math.Min(1f, value)); }
        }

        public PointF[] Points {
            get {
                if (this.svgAnimAttributes.ContainsKey("points")) {
                    this.points = (PointF[])this.svgAnimAttributes["points"];
                } else {
                    this.points = new PointF[0];
                }
                return this.points;
            }
            set { this.points = value; }
        }
        public string BackgroundImageFile {
            get {
                return this.GetAttribute("backgroundimage");
            }
            set {
                string file = AppDomain.CurrentDomain.BaseDirectory + "\\" + value;
                if (File.Exists(file)) {
                    try {
                        image1 = Bitmap.FromFile(file);
                        this.SetAttribute("backgroundimage", value);
                    } catch { }
                } else if (value == string.Empty) {
                    image1 = null;
                    this.RemoveAttribute("backgroundimage");
                }
            }
        }
        private System.Drawing.Image BackgroundImage {
            get {
                if (image1 == null) {
                    string file = BackgroundImageFile;
                    if (!string.IsNullOrEmpty(file)) {
                        try {
                            image1 = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "\\" + file);
                        } catch { }

                    }
                }
                return image1;
            }
        }
        System.Drawing.Image image1 = null;

        // Fields
        private float indent;
        public int LineCount;
        private PointF[] points;
    }
}