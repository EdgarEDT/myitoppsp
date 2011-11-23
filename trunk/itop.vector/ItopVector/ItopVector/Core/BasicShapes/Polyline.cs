namespace ItopVector.Core.Figure {
    using ItopVector.Core.Document;
    using ItopVector.Core.Func;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Text.RegularExpressions;
    using System.Text;
    using ItopVector.Core.Paint;
    using System.Drawing.Imaging;
    using System.IO;

    public class Polyline : GraphPath {
        // Methods
        internal Polyline(string prefix, string localname, string ns, SvgDocument doc)
            : base(prefix, localname, ns, doc) {
        }

        public override void Draw(Graphics g, int time) {
            if (LineType!="" && int.Parse(LineType) > 2) {
                Draw2(g, time);
            } else {
                base.Draw(g, time);
                PaintMarkers(g);
            }
        }
        public override string LineType {
            get {
                return base.LineType;
            }
            set {
                base.LineType = value;
                //if (Value == "3") {
                //    Func.AttributeFunc.SetAttributeValue(this, "stroke-width", "80");
                //}
            }
        }
        public float Width {
            get {
                float single2 = 1f;
                object obj1 = AttributeFunc.FindAttribute("stroke-width", this);
                if (obj1 is float) {
                    single2 = (float)obj1;
                }
                return single2;
            }
        }
        
        /// <summary>
        /// 马路
        /// </summary>
        /// <param name="g"></param>
        /// <param name="time"></param>
        private  void Draw2(Graphics g, int time) {
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
                float single1 = this.StrokeOpacity;
                if (this.svgAnimAttributes.ContainsKey("fill-opacity")) {
                    single1 = Math.Min(single1, (float)this.svgAnimAttributes["fill-opacity"]);
                }
                ISvgBrush brush1 = this.GraphBrush;
                using (GraphicsPath path1 = (GraphicsPath)this.GPath.Clone()) {
                    path1.Transform(base.GraphTransform.Matrix);
                    if (!base.ShowBound) {

                        float width1 = Width * GraphTransform.Matrix.Elements[0];
                        Stroke stroke = Stroke.GetStroke(this);
                        Color color1 = Color.FromArgb(75, 75, 75);
                        if (stroke.StrokeColor.ToArgb()!=Color.Black.ToArgb()) color1 = stroke.StrokeColor;
                        using (Pen p = new Pen(Color.FromArgb((int)(single1 * 255), color1))) {
                            p.Width = width1;
                            g.DrawPath(p, path1);
                        }
                        if (LineType == "3") {
                            using (Pen p = new Pen(Color.Yellow)) {
                                if (width1 > 30)
                                    p.Width = 2;
                                else
                                    p.Width = 1;
                                //p.DashPattern = new float[] { 10, 10 };
                                g.DrawPath(p, path1);
                            }
                        } else {
                            //using (Pen p = new Pen(Color.Yellow)) {
                            //    if (width1 > 30)
                            //        p.Width = 2;
                            //    else
                            //        p.Width = 1;
                            //    g.DrawPath(p, path1);
                            //}
                        }
                        if (LineType == "4") {
                            
                            using (Pen p =  new Pen(Color.FromArgb((int)(single1*255),color1))) {
                                p.Width = width1;
                                float f22 = width1 / 4f;
                                ImageAttributes imageAttributes = new ImageAttributes();
                                ColorMatrix cmatrix1 = new ColorMatrix();
                                cmatrix1.Matrix00 = 1f;
                                cmatrix1.Matrix11 = 1f;
                                cmatrix1.Matrix22 = 1f;
                                cmatrix1.Matrix33 = single1;//透明度
                                cmatrix1.Matrix44 = 1f;
                                //设置透明度                
                                imageAttributes.SetColorMatrix(cmatrix1, ColorMatrixFlag.Default, ColorAdjustType.Default);
                                if (BackgroundImage == null) BackgroundImageFile = "road.png";
                                TextureBrush tbush = new TextureBrush(BackgroundImage,new Rectangle(0,0,BackgroundImage.Width,BackgroundImage.Height),imageAttributes);
                                tbush.WrapMode = WrapMode.Tile;
                                for (int i = 0; i < path1.PointCount - 1;i++ ) {
                                    float k = (path1.PathPoints[i+1].Y - path1.PathPoints[i].Y) / (path1.PathPoints[i+1].X - path1.PathPoints[i].X);
                                    
                                    float y1 = path1.PathPoints[i].Y - path1.PathPoints[i+1].Y;
                                    float y2 = path1.PathPoints[i].X - path1.PathPoints[i+1].X;
                                    float k2 = (float)Math.Abs(k);
                                    float angle = (float)Math.Atan(k2) * 180 / (float)Math.PI;
                                    if (k < 0) {  angle = 360-angle; }

                                    PointF[] pts = new PointF[] { new PointF(path1.PathPoints[i].X, path1.PathPoints[i].Y - 26) };
                                    Matrix matrix11 = new Matrix();
                                    matrix11.RotateAt(angle, path1.PathPoints[i]);
                                    matrix11.Translate(path1.PathPoints[i].X, path1.PathPoints[i].Y);
                                    matrix11.Scale(width1 / 50, width1 / 50);
                                    //tbush.ScaleTransform(width1 / 50, width1 / 50, MatrixOrder.Append);
                                    //tbush.RotateTransform(angle, MatrixOrder.Append);
                                    //tbush.TranslateTransform(path1.PathPoints[i].X, path1.PathPoints[i].Y , MatrixOrder.Append);
                                    tbush.Transform = matrix11;
                                    
                                    p.Brush = tbush.Clone() as TextureBrush;                                    
                                    p.Alignment = PenAlignment.Center;
                                   
                                    g.DrawLine(p, path1.PathPoints[i], path1.PathPoints[i + 1]);
                                    tbush.ResetTransform();
                                }                                
                            }
                            if (BackgroundImageFile == "road.png") {
                                using (Pen p = new Pen(Color.Yellow)) {
                                    if (width1 > 30)
                                        p.Width = 2;
                                    else
                                        p.Width = 1;
                                    g.DrawPath(p, path1);
                                }
                            }
                        }
                       
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

        // Properties
        public override GraphicsPath GPath {
            get {
                if (this.pretime != base.OwnerDocument.ControlTime) {
                    this.graphPath = new GraphicsPath();
                    if ((this.Points != null) && (this.Points.Length > 1)) {
                        this.graphPath.AddLines(this.points);

                    }
                    //连接点
                    this.CreateConnectPoint();
                }
                return this.graphPath;
            }
            set {
                this.graphPath = value;
            }
        }

        public PointF[] Points {
            get {
                if (this.svgAnimAttributes.ContainsKey("points")) {
                    this.points = (PointF[])this.svgAnimAttributes["points"];
                } else {
                    this.points = new PointF[0];
                }
                //base.Transform.Matrix.TransformPoints(this.points);
                return this.points;
            }
            set {
                if (this.points != value) {
                    this.points = value;
                    StringBuilder text1 = new StringBuilder();
                    for (int num1 = 0; num1 < value.Length; num1++) {
                        PointF tf1 = value[num1];
                        if (num1 != (value.Length - 1)) {
                            string[] textArray1 = new string[4] { tf1.X.ToString(), " ", tf1.Y.ToString(), "," };
                            text1.Append(string.Concat(textArray1));
                        } else {
                            text1.Append(tf1.X.ToString() + " " + tf1.Y.ToString());
                        }
                    }
                    this.svgAnimAttributes["points"] = value;
                    AttributeFunc.SetAttributeValue(this, "points", text1.ToString());
                }
            }
        }

        public PointF[] Pt {
            get {
                if (this.svgAnimAttributes.ContainsKey("points"))
                {
                    PointF p1 = new PointF(this.Points[0].X, this.Points[0].Y);
                    PointF p2 = new PointF(this.Points[this.Points.Length - 1].X, this.Points[this.Points.Length - 1].Y);
                    PointF[] pt2 = new PointF[] { p1, p2 };
                    base.Transform.Matrix.TransformPoints(pt2);
                    return pt2;
                }
                else 
                {
                    PointF[] pt2 = new PointF[0];
                    return pt2;
                }

            }
        }
        public PointF[] FirstTwoPoint
        {
            get
            {
                if (this.svgAnimAttributes.ContainsKey("points")) {
                    PointF p1 = new PointF(this.Points[0].X, this.Points[0].Y);
                    PointF p2 = new PointF(this.Points[1].X, this.Points[1].Y);
                    PointF[] pt2 = new PointF[] { p1, p2 };
                    base.Transform.Matrix.TransformPoints(pt2);
                    return pt2;
                }
                else
                {
                    PointF[] pt2 = new PointF[0];
                    return pt2; 
                }

            }
        }
        private string extractMarkerUrl(string propValue) {
            Regex reUrl = new Regex(@"^url\((?<uri>.+)\)$");

            Match match = reUrl.Match(propValue);
            if (match.Success) {
                return match.Groups["uri"].Value;
            } else {
                return String.Empty;
            }
        }
        protected void PaintMarkers(Graphics g) {
            string markerStartUrl = extractMarkerUrl(this.GetAttribute("marker-start"));
            string markerEndUrl = extractMarkerUrl(this.GetAttribute("marker-end"));
            PointF[] points1 = this.Points.Clone() as PointF[];
            int num1 = 0;
            int num11 = 1;

            int num3 = 0;
            int num33 = 1;

            if (points1.Length > 3) {
                num33 = points1.Length - 1;
                num3 = num33 - 1;
            }


            base.GraphTransform.Matrix.TransformPoints(points1);

            float angle = 0f;//(float)(180*Math.Atan2(points1[1].Y - points1[0].Y,points1[1].X-points1[0].X)/Math.PI);

            GraphicsContainer container1 = g.BeginContainer();

            Marker element1;
            if (markerStartUrl.Length > 0) {
                angle = (float)(180 * Math.Atan2(points1[num11].Y - points1[num1].Y, points1[num11].X - points1[num1].X) / Math.PI);

                element1 = (Marker)NodeFunc.GetRefNode(markerStartUrl, this.OwnerDocument);
                if (element1 is Marker) {
                    ((Marker)element1).GraphTransform.Matrix = new Matrix();
                    Matrix matrix1 = ((Marker)element1).MarkerTransForm;

                    matrix1.Rotate(angle);
                    matrix1.Translate(points1[num1].X, points1[num1].Y, MatrixOrder.Append);
                    element1.GraphStroke = this.GraphStroke;
                    element1.IsMarkerChild = true;
                    ((Marker)element1).Draw(g, 0);
                }
            }



            if (markerEndUrl.Length > 0) {
                angle = (float)(180 * Math.Atan2(points1[num33].Y - points1[num3].Y, points1[num33].X - points1[num3].X) / Math.PI);

                element1 = (Marker)NodeFunc.GetRefNode(markerEndUrl, this.OwnerDocument);
                if (element1 is Marker) {
                    ((Marker)element1).GraphTransform.Matrix = new Matrix();
                    Matrix matrix1 = ((Marker)element1).MarkerTransForm;

                    matrix1.Rotate(angle);
                    matrix1.Translate(points1[num33].X, points1[num33].Y, MatrixOrder.Append);
                    element1.GraphStroke = this.GraphStroke;
                    element1.IsMarkerChild = true;
                    ((Marker)element1).Draw(g, 0);
                }
            }
            g.EndContainer(container1);
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
                } else if (string.IsNullOrEmpty(value)) {
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
        private PointF[] points;
    }
}

