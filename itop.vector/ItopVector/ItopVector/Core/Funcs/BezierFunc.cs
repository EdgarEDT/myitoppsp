namespace ItopVector.Core.Func
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Drawing;

    public class BezierFunc
    {
        // Methods
        public BezierFunc()
        {
        }

        private static void AddjustPoints(ArrayList ptArray, PointF pt1, PointF pt2)
        {
            int num1 = ptArray.Count;
            Debug.Assert(num1 > 0);
            for (int num2 = 0; num2 < num1; num2++)
            {
                PointF tf1 = pt2;
                ptArray[num2] = tf1;
            }
        }

        public static void AdjustCurve(ref PointF[] m_pointArray)
        {
            int num1 = m_pointArray.Length;
            if (num1 >= 3)
            {
                double num8;
                bool flag1 = false;
                int num2 = 0;
                int num3 = 0;
                double num7 = num8 = BezierFunc.CalcAngle(m_pointArray[0], m_pointArray[1]);
                ArrayList list1 = new ArrayList(0x10);
                for (int num9 = 0; num9 < (num1 - 2); num9++)
                {
                    double num4 = BezierFunc.CalcAngle(m_pointArray[num9], m_pointArray[num9 + 1]);
                    double num5 = BezierFunc.CalcAngle(m_pointArray[num9 + 1], m_pointArray[num9 + 2]);
                    num7 = BezierFunc.LesserData(num7, BezierFunc.LesserData(num4, num5));
                    num8 = BezierFunc.BiggishData(num8, BezierFunc.BiggishData(num4, num5));
                    Debug.Assert(num8 >= num7);
                    double num6 = Math.Abs((double) (num8 - num7));
                    if (!flag1)
                    {
                        if (num6 > 0.39269908169872414)
                        {
                            flag1 = true;
                            if (BezierFunc.PointLine(m_pointArray[num2], m_pointArray[num3], m_pointArray[num9]) > 4)
                            {
                                num3 = num9 + 1;
                            }
                            list1.Add(m_pointArray[num9 + 2]);
                            num7 = num8 = BezierFunc.CalcAngle(m_pointArray[num9 + 1], m_pointArray[num9 + 2]);
                        }
                        else
                        {
                            num3 = num9 + 1;
                        }
                    }
                    else
                    {
                        Debug.Assert(num3 >= 0);
                        if (num6 > 0.39269908169872414)
                        {
                            Debug.Assert((num3 >= num2) && (num9 >= num3));
                            if (BezierFunc.PointLine(m_pointArray[num2], m_pointArray[num3], m_pointArray[num9]) < 4)
                            {
                                BezierFunc.AddjustPoints(list1, m_pointArray[num2], m_pointArray[num3]);
                                flag1 = false;
                            }
                            else
                            {
                                num2 = num3;
                                num3 = num9 + 1;
                                Debug.Assert((num2 >= 0) && (num2 < num1));
                            }
                            num7 = num8 = BezierFunc.CalcAngle(m_pointArray[num9 + 1], m_pointArray[num9 + 2]);
                        }
                        list1.Add(m_pointArray[num9 + 2]);
                    }
                }
                m_pointArray = new PointF[list1.Count];
                list1.CopyTo(m_pointArray);
            }
        }

        private static double BiggishData(double a, double b)
        {
            return ((a > b) ? a : b);
        }

        private static double CalcAngle(PointF pt1, PointF pt2)
        {
            if (pt1.X == pt2.X)
            {
                return 1.5707963267948966;
            }
            return Math.Atan((double) ((pt2.Y - pt1.Y) / (pt2.X - pt1.X)));
        }

        private static double CalcDistanceOfTwoPoints(PointF p1, PointF p2)
        {
            return Math.Sqrt(Math.Pow((double) (p2.Y - p1.Y), 2) + Math.Pow((double) (p2.X - p1.X), 2));
        }

        private static double CalcDistanceOfTwoPoints(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(((x2 - x1) * (x2 - x1)) + ((y2 - y1) * (y2 - y1)));
        }

        private static bool CalcSlope(PointF pt1, PointF pt2, ref double scope)
        {
            if (pt1.X == pt2.X)
            {
                return false;
            }
            scope = (pt1.Y - pt2.Y) / (pt2.X - pt1.X);
            return true;
        }

        private static PointF CalVeticalCutPos(PointF LinePort1, PointF LinePort2, PointF pt)
        {
            PointF tf1 = PointF.Empty;
            if (LinePort1 == LinePort2)
            {
                return LinePort1;
            }
            if (LinePort1.X == LinePort2.X)
            {
                tf1.X = LinePort1.X;
                tf1.Y = pt.Y;
            }
            else
            {
                double num1 = 0;
                BezierFunc.CalcSlope(LinePort1, LinePort2, ref num1);
                if (0 == num1)
                {
                    tf1.X = pt.X;
                    tf1.Y = LinePort1.Y;
                }
                else
                {
                    double num2 = 0;
                    double num3 = 0;
                    BezierFunc.CalcSlope(LinePort1, LinePort2, ref num2);
                    if (num2 != 0)
                    {
                        double num4 = LinePort1.Y - (num2 * LinePort1.X);
                        num3 = -1 / num2;
                        double num5 = pt.Y - (num3 * pt.X);
                        tf1.X = BezierFunc.double_to_int((num5 - num4) / (num2 - num3));
                        tf1.Y = BezierFunc.double_to_int((num2 * tf1.X) + num4);
                    }
                }
            }
            return tf1;
        }

        private static int double_to_int(double dVal)
        {
            int num1 = (int) dVal;
            double num2 = dVal - num1;
            if (Math.Abs(num2) > 0.99)
            {
                if (dVal > 0)
                {
                    num1++;
                    return num1;
                }
                num1--;
            }
            return num1;
        }

        private static double LesserData(double a, double b)
        {
            return ((a < b) ? a : b);
        }

        private static void LowPassFilter(ref PointF[] ps)
        {
            ArrayList list1 = new ArrayList(0x10);
            for (int num1 = 2; num1 < (ps.Length - 2); num1++)
            {
                PointF tf1 = new PointF(0f, 0f);
                for (int num2 = -2; num2 < 3; num2++)
                {
                    if (((num2 + num1) > -1) && ((num2 + num1) < ps.Length))
                    {
                        tf1.X += ps[num1 + num2].X;
                        tf1.Y += ps[num1 + num2].Y;
                    }
                }
                tf1.X *= 0.5f;
                tf1.Y *= 0.5f;
                list1.Add(new PointF(tf1.X, tf1.Y));
            }
            ps = new PointF[list1.Count];
            list1.CopyTo(ps);
        }

        private static double PointLine(PointF cPoint1, PointF cPoint2, PointF cPoint)
        {
            return BezierFunc.PointLine((double) cPoint1.X, (double) cPoint1.Y, (double) cPoint2.X, (double) cPoint2.Y, (double) cPoint.X, (double) cPoint.Y);
        }

        private static double PointLine(double x1, double y1, double x2, double y2, double x, double y)
        {
            double num1;
            if ((x1 == x2) && (y1 == y2))
            {
                num1 = BezierFunc.CalcDistanceOfTwoPoints(x, y, x1, y1);
            }
            else if (x1 == x2)
            {
                num1 = Math.Abs((double) (x - x1));
            }
            else if (y1 == y2)
            {
                num1 = Math.Abs((double) (y - y1));
            }
            else
            {
                double num2 = (y1 - y2) / (x1 - x2);
                double num3 = y1 - (x1 * num2);
                num1 = ((y - (x * num2)) - num3) / Math.Sqrt(1 + (num2 * num2));
            }
            return Math.Abs(num1);
        }

    }
}

