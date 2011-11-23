    using ItopVector.Core.Document;
    using ItopVector.Core.Func;
    using ItopVector.Core.Interface;
    using ItopVector.Core.Types;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
namespace ItopVector.Core.Animate
{

    public class MotionAnimate : Animate, IPath
    {
        // Methods
        internal MotionAnimate(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
        {
            this.pointsinfo = new PointInfoCollection();
            this.Rotate = "auto";
            this.CenterPoint = PointF.Empty;
            this.OriMatrix = new Matrix();
        }

        public void Draw(Graphics g, int time)
        {
        }

        public override object FT(float time, DomType domtype)
        {
            string text2;
            string text1 = string.Empty;
            Matrix matrix1 = new Matrix();
            float single1 = 0f;
            float single2 = 0f;
            float single3 = 0f;
            GraphicsPath path1 = this.GPath;
            bool flag1 = true;
            if (path1 == null)
            {
                flag1 = false;
            }
            else if (path1.PointCount <= 0)
            {
                flag1 = false;
            }
            if (flag1)
            {
                PointF[] tfArray1;
                GraphicsPath path2 = (GraphicsPath) this.motionPath.Clone();
                path2.Flatten(new Matrix(), 0.01f);
                if (path2.PathTypes[path2.PathTypes.Length - 1] >= 0x80)
                {
                    tfArray1 = new PointF[path2.PathPoints.Length + 1];
                    path2.PathPoints.CopyTo(tfArray1, 0);
                    tfArray1[tfArray1.Length - 1] = path2.PathPoints[0];
                }
                else
                {
                    tfArray1 = path2.PathPoints;
                }
                int num1 = tfArray1.Length;
                int num2 = base.Begin;
                float single4 = base.Duration;
                float single5 = single4 / ((float) (num1 - 1));
                int num3 = (int) ((time - num2) / single5);
                if ((num3 + 1) >= num1)
                {
                    single1 = tfArray1[num3].X;
                    single2 = tfArray1[num3].Y;
                    if ((num3 - 1) >= 0)
                    {
                        PointF tf1 = tfArray1[num3 - 1];
                        if (tf1.X == single1)
                        {
                            single3 = 0f;
                        }
                        else
                        {
                            single3 = (float) ((Math.Atan((double) ((tf1.Y - single2) / (tf1.X - single1))) / 3.1415926535897931) * 180);
                        }
                    }
                    goto Label_02C5;
                }
                PointF tf2 = tfArray1[num3];
                PointF tf3 = tfArray1[num3 + 1];
                float single6 = base.Begin + (num3 * single5);
                float single7 = base.Begin + ((num3 + 1) * single5);
                if (tf3.X == tf2.X)
                {
                    single3 = 0f;
                }
                else
                {
                    single3 = (float) ((Math.Atan((double) ((tf3.Y - tf2.Y) / (tf3.X - tf2.X))) / 3.1415926535897931) * 180);
                }
                switch (this.CalcMode)
                {
                    case CalcMode.linear:
                    {
                        single1 = AnimFunc.Linear(tf2.X, single6, tf3.X, single7, time);
                        single2 = AnimFunc.Linear(tf2.Y, single6, tf3.Y, single7, time);
                        goto Label_02C5;
                    }
                    case CalcMode.paced:
                    {
                        goto Label_02C5;
                    }
                    case CalcMode.spline:
                    {
                        goto Label_02C5;
                    }
                    case CalcMode.discrete:
                    {
                        text1 = tfArray1[num3].X.ToString() + " " + tfArray1[num3].Y.ToString();
                        goto Label_02C5;
                    }
                }
                goto Label_02C5;
            }
            matrix1 = (Matrix) base.FT(time, DomType.SvgMatrix);
        Label_02C5:
            text2 = this.GetAttribute("rotate").Trim();
            if ((text2 == null) || (text2 == string.Empty))
            {
                text2 = "0";
            }
            if (text2 == "auto")
            {
                matrix1.Rotate(single3, MatrixOrder.Append);
                matrix1.Translate(single1, single2, MatrixOrder.Append);
                return matrix1;
            }
            if (text2 == "auto-reverse")
            {
                single3 -= 180f;
                matrix1.Rotate(single3, MatrixOrder.Append);
                matrix1.Translate(single1, single2, MatrixOrder.Append);
                return matrix1;
            }
            try
            {
                single3 = float.Parse(text2);
                matrix1.Rotate(single3);
            }
            catch (Exception)
            {
            }
            matrix1.Translate(single1, single2, MatrixOrder.Append);
            return matrix1;
        }


        // Properties
        public GraphicsPath GPath
        {
            get
            {
                if ((this.pretime != base.OwnerDocument.ControlTime) && !base.OwnerDocument.PlayAnim)
                {
                    object obj1 = AttributeFunc.FindAttribute("path", this);
                    if (obj1 is GraphicsPath)
                    {
                        this.motionPath = (GraphicsPath) obj1;
                    }
                    else if (obj1 is string)
                    {
                        this.motionPath = PathFunc.PathDataParse((string) obj1);
                    }
                }
                return this.motionPath;
            }
            set
            {
                this.motionPath = value;
                AttributeFunc.SetAttributeValue(this, "path", PathFunc.GetPathString(value));
                this.pretime = -1;
            }
        }

        public PointInfoCollection PointsInfo
        {
            get
            {
                return this.pointsinfo;
            }
        }


        // Fields
        public PointF CenterPoint;
        private GraphicsPath motionPath;
        public Matrix OriMatrix;
        private PointInfoCollection pointsinfo;
        public string Rotate;
    }
}

