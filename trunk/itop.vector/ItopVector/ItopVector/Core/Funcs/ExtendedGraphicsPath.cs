namespace ItopVector.Core.Func
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class ExtendedGraphicsPath
    {
        // Methods
        public ExtendedGraphicsPath(GraphicsPath graphicsPath)
        {
            this.graphicsPath = graphicsPath;
        }

        public void AddArc(PointF startPoint, PointF endPoint, float r1, float r2, bool largeArcFlag, bool sweepFlag, float angle)
        {
            float single1 = startPoint.X;
            float single2 = startPoint.Y;
            float single3 = endPoint.X;
            float single4 = endPoint.Y;
            if ((single1 != single3) || (single2 != single3))
            {
                if ((r1 == 0f) || (r2 == 0f))
                {
                    this.graphicsPath.AddLine(single1, single2, Convert.ToSingle(single3), Convert.ToSingle(single4));
                }
                else
                {
                    double num1 = ((double) (single1 - single3)) / 2;
                    double num2 = ((double) (single2 - single4)) / 2;
                    angle = angle % 360f;
                    double num3 = (angle * 3.1415926535897931) / 180;
                    double num4 = Math.Cos(num3);
                    double num5 = Math.Sin(num3);
                    double num6 = (num4 * num1) + (num5 * num2);
                    double num7 = (-num5 * num1) + (num4 * num2);
                    double num8 = Math.Abs(r1);
                    double num9 = Math.Abs(r2);
                    double num10 = num8 * num8;
                    double num11 = num9 * num9;
                    double num12 = num6 * num6;
                    double num13 = num7 * num7;
                    double num14 = (num12 / num10) + (num13 / num11);
                    if (num14 > 1)
                    {
                        num8 = Math.Sqrt(num14) * num8;
                        num9 = Math.Sqrt(num14) * num9;
                        num10 = num8 * num8;
                        num11 = num9 * num9;
                    }
                    double num15 = (largeArcFlag == sweepFlag) ? ((double) (-1)) : ((double) 1);
                    double num16 = (((num10 * num11) - (num10 * num13)) - (num11 * num12)) / ((num10 * num13) + (num11 * num12));
                    num16 = (num16 < 0) ? 0 : num16;
                    double num17 = num15 * Math.Sqrt(num16);
                    double num18 = num17 * ((num8 * num7) / num9);
                    double num19 = num17 * -((num9 * num6) / num8);
                    double num20 = ((double) (single1 + single3)) / 2;
                    double num21 = ((double) (single2 + single4)) / 2;
                    double num22 = num20 + ((num4 * num18) - (num5 * num19));
                    double num23 = num21 + ((num5 * num18) + (num4 * num19));
                    double num24 = num6 - num18;
                    double num25 = num7 - num19;
                    double num26 = -num6 - num18;
                    double num27 = -num7 - num19;
                    double num29 = Math.Sqrt((num24 * num24) + (num25 * num25));
                    double num28 = num24;
                    num15 = (num25 < 0) ? -1 : 1;
                    double num30 = num15 * Math.Acos(num28 / num29);
                    num30 = (num30 * 180) / 3.1415926535897931;
                    num29 = Math.Sqrt(((num24 * num24) + (num25 * num25)) * ((num26 * num26) + (num27 * num27)));
                    num28 = (num24 * num26) + (num25 * num27);
                    num15 = (((num24 * num27) - (num25 * num26)) < 0) ? -1 : 1;
                    double num31 = num15 * Math.Acos(num28 / num29);
                    num31 = (num31 * 180) / 3.1415926535897931;
                    if (!sweepFlag && (num31 > 0))
                    {
                        num31 -= 360;
                    }
                    else if (sweepFlag && (num31 < 0))
                    {
                        num31 += 360;
                    }
                    num31 = num31 % 360;
                    num30 = num30 % 360;
                    GraphicsPath path1 = new GraphicsPath();
                    path1.StartFigure();
                    path1.AddArc(Convert.ToSingle((double) (num22 - num8)), Convert.ToSingle((double) (num23 - num9)), Convert.ToSingle((double) (num8 * 2)), Convert.ToSingle((double) (num9 * 2)), Convert.ToSingle(num30), Convert.ToSingle(num31));
                    Matrix matrix1 = new Matrix();
                    matrix1.Translate(Convert.ToSingle(-num22), Convert.ToSingle(-num23));
                    path1.Transform(matrix1);
                    matrix1 = new Matrix();
                    matrix1.Rotate(angle);
                    path1.Transform(matrix1);
                    matrix1 = new Matrix();
                    matrix1.Translate(Convert.ToSingle(num22), Convert.ToSingle(num23));
                    path1.Transform(matrix1);
                    this.graphicsPath.AddPath(path1, true);
                }
            }
        }

        public void AddRoundedRect(Rectangle rect, int rx, int ry)
        {
            this.AddRoundedRect((RectangleF) rect, (float) rx, (float) ry);
        }

        public void AddRoundedRect(RectangleF rect, float rx, float ry)
        {
            this.graphicsPath.StartFigure();
            if ((rx == 0f) && (ry == 0f))
            {
                this.graphicsPath.AddRectangle(rect);
            }
            else
            {
                if (rx == 0f)
                {
                    rx = ry;
                }
                else if (ry == 0f)
                {
                    ry = rx;
                }
                rx = Math.Min((float) (rect.Width / 2f), rx);
                ry = Math.Min((float) (rect.Height / 2f), ry);
                float single1 = (rect.X + rect.Width) - rx;
                this.graphicsPath.AddLine((float) (rect.X + rx), rect.Y, single1, rect.Y);
                this.graphicsPath.AddArc((float) (single1 - rx), rect.Y, (float) (rx * 2f), (float) (ry * 2f), (float) 270f, (float) 90f);
                float single2 = rect.X + rect.Width;
                float single3 = (rect.Y + rect.Height) - ry;
                this.graphicsPath.AddLine(single2, (float) (rect.Y + ry), single2, single3);
                this.graphicsPath.AddArc((float) (single2 - (rx * 2f)), (float) (single3 - ry), (float) (rx * 2f), (float) (ry * 2f), (float) 0f, (float) 90f);
                this.graphicsPath.AddLine((float) (single2 - rx), (float) (rect.Y + rect.Height), (float) (rect.X + rx), (float) (rect.Y + rect.Height));
                this.graphicsPath.AddArc(rect.X, (float) (single3 - ry), (float) (rx * 2f), (float) (ry * 2f), (float) 90f, (float) 90f);
                this.graphicsPath.AddLine(rect.X, single3, rect.X, (float) (rect.Y + ry));
                this.graphicsPath.AddArc(rect.X, rect.Y, (float) (rx * 2f), (float) (ry * 2f), (float) 180f, (float) 90f);
                this.graphicsPath.CloseFigure();
            }
        }


        // Fields
        public GraphicsPath graphicsPath;
    }
}

