using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using System.Windows.Forms;
using ItopVector.Core.Figure;
using System.Configuration;

namespace ItopVector.Tools
{
   public class TLMath
    {
       
        
        /// <summary>
        /// 计算多边形面积 
        /// </summary>
        /// <param name="point">组成多边形的点</param>
        /// <returns></returns>
        public static decimal getPolygonArea(PointF[] point,decimal ViewScale)
        {
            if(point.Length<3){
                return 0;
            }
            decimal s;
            int i=0;
            float x1, y1, x2, y2,fx,fy;

            fx = point[0].X;
            fy = point[0].Y;
            x1 = point[0].X;
            y1 = point[0].Y;
            x2 = point[1].X;
            y2 = point[1].Y;
          
            s = linesqr(x1, y1, x2, y2);
            for (i = 2; i < point.Length; i++)
            {
                x1 = x2; y1 = y2;
                x2 = point[i].X;
                y2 = point[i].Y;
                s += linesqr(x1, y1, x2, y2);
            }
            s += linesqr(x2, y2, fx, fy);//首尾相连
            return System.Math.Abs(s) * ViewScale;
        }

        //组成多边形的一个三角形
       static decimal linesqr(float x1, float y1, float x2, float y2)
        {
            return Convert.ToDecimal((x2 - x1) * (y1 + y2) / 2.0);
        }

       public static double GetPointsLen(params PointF[] points) {
           if (points == null || points.Length < 2) return 0;
           double len=0;
           for (int i = 1; i < points.Length; i++) {
               len += Math.Sqrt(Math.Pow((points[i].X - points[i - 1].X),2) + Math.Pow((points[i].Y - points[i - 1].Y),2));
           }
           return len;
       }
       //求多边形的重心
       /// <summary>
       /// 求取多边形的重心
       /// </summary>
       /// <param name="poly">多边形</param>
       /// <returns>返回重心点</returns>
       public static PointF polyCentriod(XmlElement poly)
       {
           PointF[] p = getPolygonPoints(poly);
           PointF Centroid =new PointF();
           int n = p.Length;
           int i, j;
           float ai, atmp = 0, xtmp = 0, ytmp = 0, ltmp = 0;
           if (n >= 3)
           {
               for (i = n - 1, j = 0; j < n; i = j, j++)
               {
                   ai = p[i].X * p[j].Y - p[j].X * p[i].Y;
                   atmp += ai;
                   xtmp += (p[j].X + p[i].X) * ai;
                   ytmp += (p[j].Y + p[i].Y) * ai;
                   // ltmp += System.Math.Sqrt((p[j].X - p[i].X) * (p[j].X - p[i].X) +
                   //(p[j].Y - p[i].Y) * (p[j].Y - p[i].Y));
               }
               // area = atmp / 2;
               if (atmp != 0)
               {
                   Centroid.X = xtmp / (3 * atmp);
                   Centroid.Y = ytmp / (3 * atmp);
                   //length = ltmp;
                   //if (area < 0)
                   //    area = -area;
                   
               }
           }
           return Centroid;
          
          

       }
       public static PointF polyCentriod(PointF[] p)
       {
            
           PointF Centroid = new PointF();
           int n = p.Length;
           int i, j;
           float ai, atmp = 0, xtmp = 0, ytmp = 0, ltmp = 0;
           if (n >= 3)
           {
               for (i = n - 1, j = 0; j < n; i = j, j++)
               {
                   ai = p[i].X * p[j].Y - p[j].X * p[i].Y;
                   atmp += ai;
                   xtmp += (p[j].X + p[i].X) * ai;
                   ytmp += (p[j].Y + p[i].Y) * ai;
                   // ltmp += System.Math.Sqrt((p[j].X - p[i].X) * (p[j].X - p[i].X) +
                   //(p[j].Y - p[i].Y) * (p[j].Y - p[i].Y));
               }
               // area = atmp / 2;
               if (atmp != 0)
               {
                   Centroid.X = xtmp / (3 * atmp);
                   Centroid.Y = ytmp / (3 * atmp);
                   //length = ltmp;
                   //if (area < 0)
                   //    area = -area;

               }
           }
           return Centroid;



       }
       // 功能：判断点是否在多边形内 
       // 方法：求解通过该点的水平线与多边形各边的交点 
       // 结论：单边交点为奇数，成立!
       //参数： 
       /// <summary>
       /// 判断点是否在多边形内
       /// </summary>
       /// <param name="P">判断的点</param>
       /// <param name="poly">多边形</param>
       /// <returns>返回是否在内部</returns>
       
     public static bool PtInPolygon(PointF p, XmlElement poly)
       {
           int nCross = 0;
           PointF[] pnts = getPolygonPoints(poly);
           for (int i = 0; i < pnts.Length; i++)
           {
               PointF p1 = pnts[i];
               PointF p2 = pnts[(i + 1) % pnts.Length];
               // 求解 y=p.y 与 p1p2 的交点
               if (p1.Y == p2.Y) // p1p2 与 y=p0.y平行 
                   continue;
               if (p.Y< Math.Min(p1.Y, p2.Y)) // 交点在p1p2延长线上 
                   continue;
               if (p.Y >= Math.Max(p1.Y, p2.Y)) // 交点在p1p2延长线上 
                   continue;
               // 求交点的 X 坐标 -------------------------------------------------------------- 
               double x = (double)(p.Y - p1.Y) * (double)(p2.X - p1.X) / (double)(p2.Y - p1.Y) + p1.X;
               if (x > p.X)
                   nCross++; // 只统计单边交点 
           }
           // 单边交点为偶数，点在多边形之外 --- 
           return (nCross % 2 == 1);
       }

       /// <summary>
       /// 得到多边形的内点数组
       /// </summary>
       /// <param name="poly"></param>
       /// <returns></returns>
       public static PointF[] getPolygonPoints(XmlElement poly)
        {
            try
            {
                string str_points = poly.GetAttribute("points");
                string[] points = str_points.Split(",".ToCharArray());
                PointF[] pnts = new PointF[points.Length];

                for (int i = 0; i < points.Length; i++)
                {
                    string[] pointsXY = points[i].Split(" ".ToCharArray());
                    pnts[i].X = Convert.ToSingle(pointsXY[0]);
                    pnts[i].Y = Convert.ToSingle(pointsXY[1]);
                }

                ((GraphPath)poly).Transform.Matrix.TransformPoints(pnts);
                return pnts;
                
            }
           catch(Exception e){
               MessageBox.Show(e.Message);
               return null;
           }
        }
       public static decimal getInterPolygonArea(Region myRegion, RectangleF rect, decimal ViewScale)
        {

            // 绘制到临时图片
            Bitmap bitTemp = new Bitmap(Convert.ToInt32(rect.Width + 1), Convert.ToInt32(rect.Height + 1));
            Graphics g = Graphics.FromImage(bitTemp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.CompositingQuality = CompositingQuality.HighQuality;
            
            g.FillRectangle(Brushes.White, 0, 0, rect.Width + 1, rect.Height + 1);
            RectangleF r = myRegion.GetBounds(g);
            //平移多边形到（0，0）点
            myRegion.Translate(-r.X, -r.Y);
            g.FillRegion(Brushes.Black, myRegion);
            //bitTemp.Save("D:\\" + Guid.NewGuid().ToString() + ".bmp");
            g.Flush();
            g.Dispose();

            // 根据像素颜色计算面积
            int nSize = 0;
            int nBlackValue = Color.Black.ToArgb();
            for (int i = 0; i < rect.Width; i++)
            {
                for (int j = 0; j < rect.Height; j++)
                {
                    if (bitTemp.GetPixel(i, j).ToArgb() == nBlackValue)
                        ++nSize;
                }
            }
            return  nSize*ViewScale;
        }
  
        /// <summary>
        /// 计算2个多边形的交叉区域面积
        /// </summary>
        /// <param name="poly1"> 第一个多边形</param>
        /// <param name="poly2"> 第二个多边形</param>
        /// <param name="rect"> 绘制所有选中多边形所需要的矩形区域</param>
        /// <returns></returns>
        public static int getInterPolygonArea(PointF[] poly1, PointF[] poly2,RectangleF rect)
        {
            try
            {
                //Bitmap bitmap=null;
                GraphicsPath gp1 = new GraphicsPath();
                gp1.AddPolygon(poly1);
                GraphicsPath gp2 = new GraphicsPath();
                gp2.AddPolygon(poly2);

                // 得到两个多边形的交集
                System.Drawing.Region myRegion = new Region(gp1);
                myRegion.Intersect(gp2);
                //myRegion.GetBounds(
                // 绘制到临时图片
                
                Bitmap bitTemp = new Bitmap(Convert.ToInt32(rect.Width + 1), Convert.ToInt32(rect.Height + 1));
                Graphics g = Graphics.FromImage(bitTemp);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.FillRectangle(Brushes.White, 0, 0, rect.Width + 1, rect.Height + 1);
                RectangleF r= myRegion.GetBounds(g);
                //平移多边形到（0，0）点
                myRegion.Translate(-r.X,-r.Y);
                g.FillRegion(Brushes.Black, myRegion);
                //bitTemp.Save("D:\\"+Guid.NewGuid().ToString()+".bmp");
                g.Flush();
                g.Dispose();

                System.DateTime dt1 = System.DateTime.Now; 
                // 根据像素颜色计算面积
                int nSize = 0;
                int nBlackValue = Color.Black.ToArgb();
                for (int i = 0; i < rect.Width; i++)
                {
                    for (int j = 0; j < rect.Height; j++)
                    {
                        if (bitTemp.GetPixel(i, j).ToArgb() == nBlackValue)
                        //if (bitTemp.GetPixel(i, j).Equals(Color.Black))
                            ++nSize;
                    }
                }
             
                return nSize;
            }
            catch(Exception e){
                MessageBox.Show(e.Message);
                return 0;
            }
        }
        /// <summary>
        /// 计算直线长度
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
       public static decimal getLineLength(Line line, decimal ViewScale)
        {
            try
            {
                float x1 = line.X1;
                float y1 = line.Y1;
                float x2 = line.X2;
                float y2 = line.Y2;
                return Convert.ToDecimal( Math.Abs(Math.Sqrt(Math.Pow(Convert.ToDouble(x2 - x1), 2.0) + Math.Pow(Convert.ToDouble(y2 - y1), 2.0)))) * ViewScale;
            }
            catch(Exception e){
                MessageBox.Show(e.Message); 
                return 0;
            }
        }
       public static decimal getLength(PointF p1, PointF p2, decimal ViewScale)
       {
           try
           {
               float x1 = p1.X;
               float y1 = p1.Y;
               float x2 = p2.X;
               float y2 = p2.Y;
               return Convert.ToDecimal(Math.Abs(Math.Sqrt(Math.Pow(Convert.ToDouble(x2 - x1), 2.0) + Math.Pow(Convert.ToDouble(y2 - y1), 2.0)))) * ViewScale;
           }
           catch (Exception e)
           {
               MessageBox.Show(e.Message);
               return 0;
           }
       }
        /// <summary>
        /// 计算折线长度
        /// </summary>
        /// <param name="polyline"></param>
        /// <returns></returns>
       public static decimal getPolylineLength(Polyline polyline, decimal ViewScale)
        {
            try
            {
                decimal len = 0;
                PointF[] points =(PointF[]) polyline.Points.Clone();
                if (points.Length>1)
                {
                    for (int i = 0; i < points.Length - 1; i++)
                    {
                        len = len + Convert.ToDecimal(Math.Abs(Math.Sqrt(Math.Pow(Convert.ToDouble(points[i + 1].X - points[i].X), 2.0) + Math.Pow(Convert.ToDouble(points[i + 1].Y - points[i].Y), 2.0))));
                    }
                }
                return len * ViewScale;
            }
            catch(Exception e){
                MessageBox.Show(e.Message);
                return 0;
            }
        }
       /// <summary>
       /// 计算三点夹角
       /// line1{(x1,y1),   (x2,y2)},     line2{(x2,y2),(x3,y3)}   
       ///  夹角   = acos( (dx1*dx2+dy1*dy2)/sqrt((dx1*dx1++dy1*dy1)*(dx2*dx2+dy2*dy2)) ) 
       /// </summary>
       /// <returns></returns>
       public static double getLineAngle(PointF p1, PointF p2, PointF p3)
       {
          double dx1 = p2.X - p1.X;
          double dx2 = p3.X - p2.X;
          double dy1 = p2.Y - p1.Y;
          double dy2 = p3.Y - p2.Y;   
          return Math.Acos((dx1*dx2+dy1*dy2)/Math.Sqrt((dx1*dx1+dy1*dy1)*(dx2*dx2+dy2*dy2)));
       }
       public static double Angle(PointF cen, PointF first, PointF second)
       {
           double dx1, dx2, dy1, dy2;
           double angle;

           //double S = ((x1 - x0) * (y2 - y0) - (x2 - x0) * (y1 - y0)) / 2;
           double S = ((cen.X - first.X) * (second.Y - first.Y) - (second.X - first.X) * (cen.Y - first.Y)) / 2;

           dx1 = first.X - cen.X;
           dy1 = first.Y - cen.Y;
           dx2 = second.X - cen.X;
           dy2 = second.Y - cen.Y;
           double c = (double)Math.Sqrt(dx1 * dx1 + dy1 * dy1) * (double)Math.Sqrt(dx2 * dx2 + dy2 * dy2);
           if (c == 0) return -1;
           angle = (double)Math.Acos((dx1 * dx2 + dy1 * dy2) / c);
           return angle;
       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="p1">第一点</param>
       /// <param name="p2">第二点</param>
       /// <param name="a"></param>
       /// <param name="b"></param>
       /// <param name="c"></param>
       /// <returns></returns>
       public static PointF[] getPoint3(PointF p1, PointF p2, double a, double b, double c)
       {
        // 角C=PI/2   ,|AC|=b 
        // alpha   =   arctan((y2-y1)/(x2-x1))+arccos(b/c)) 
        //              (或arctan((y2-y1)/(x2-x1))-arccos(b/c))   ) 
        // C(x,y)=(x1+b*cos(alpha),y1+b*sin(alpha)); 
           PointF[] pn = new PointF[2];
           double ang1 = Math.Atan((p2.Y - p1.Y) / (p2.X - p1.X)) + Math.Acos(b / c);
           double ang2 = Math.Atan((p2.Y - p1.Y) / (p2.X - p1.X)) - Math.Acos(b / c);
           PointF point1 = new PointF(Convert.ToSingle( p1.X + b * Math.Cos(ang1)),Convert.ToSingle( p1.Y + b * Math.Sin(ang1)));
           PointF point2 = new PointF(Convert.ToSingle(p1.X + b * Math.Cos(ang2)),Convert.ToSingle( p1.Y + b * Math.Sin(ang2)));
           pn[0] = point1;
           pn[1] = point2;
           return pn;
       }
       public static PointF getPoint3(PointF p1, PointF p2, double ang1, double ang2)
       {
          // 【 x =（x1ctgA2+x2ctgA1+y2-y1）/（ctgA1+ctgA2）】        
          // 【 y =（y1ctgA2+y2ctgA1+x2-x1）/（ctgA1+ctgA2）】
           float x = Convert.ToSingle((p1.X * (1 / Math.Tan(ang2)) + p2.X * (1 / Math.Tan(ang1)) + p2.Y - p1.Y) / ((1 / Math.Tan(ang1)) + (1 / Math.Tan(ang2))));
           float y = Convert.ToSingle((p1.Y * (1 / Math.Tan(ang2)) + p2.Y * (1 / Math.Tan(ang1)) + p2.X - p1.X) / ((1 / Math.Tan(ang1)) + (1 / Math.Tan(ang2))));
           return new PointF(x, y);
       }
       public static float getdcNumber(decimal number1, float Scale)
       {
           int chose = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("chose"));

           if (chose == 2) {
               int n = Convert.ToInt32(Scale * 1000000);
               switch (n) {
                   case 15625:
                       return (float)(number1 * 120 / (Convert.ToDecimal(Scale) * 40));
                       break;
                   case 31250:
                       return (float)(number1 * 120 / (Convert.ToDecimal(Scale) * 20));
                       break;
                   case 62500:
                       return (float)(number1 * 120 / (Convert.ToDecimal(Scale) * 10));
                       break;
                   case 125000:
                       return (float)(number1 * 120 / (Convert.ToDecimal(Scale) * 5));
                       break;
                   case 250000:
                       return (float)(number1 * 120 / (Convert.ToDecimal(Scale) *  Convert.ToDecimal(2.5)));
                       break;
                   case 500000:
                       return(float)(number1 * 120 / (Convert.ToDecimal(Scale) *  Convert.ToDecimal(1.25))); 
                   case 1000000:
                       return (float)(number1 * 120 / (Convert.ToDecimal(Scale) *  Convert.ToDecimal(0.625)));
                       break;
                   case 2000000:
                       return (float)(number1 * 120 / (Convert.ToDecimal(Scale) *  Convert.ToDecimal(0.3125)));
                       break;
                   case 4000000:
                       return (float)(number1 * 120 / (Convert.ToDecimal(Scale) *  Convert.ToDecimal(0.15625)));
                   default:
                       return 0;
               }
           } else {
               int n = Convert.ToInt32(Scale * 1000);
               switch (n) {
                   case 10:
                       return (float)(number1 * 60 / (Convert.ToDecimal(Scale) * 40));
                       break;
                   case 20:
                       return (float)(number1 * 60 / (Convert.ToDecimal(Scale) * 20));
                       break;
                   case 40:
                       return (float)(number1 * 60 / (Convert.ToDecimal(Scale) * 10));
                   case 100:
                       return (float)(number1 * 60 / (Convert.ToDecimal(Scale) * 4));
                       break;
                   case 200:
                       return (float)(number1 * 60 / (Convert.ToDecimal(Scale) * 2));
                   case 400:
                       return (float)(number1 * 60 / (Convert.ToDecimal(Scale) * 1));
                       break;
                   case 1000:
                       return (float)(number1 * 60 / (Convert.ToDecimal(Scale) *  Convert.ToDecimal(0.4)));
                       break;
                   case 2000:
                       return (float)(number1 * 60 / (Convert.ToDecimal(Scale) *  Convert.ToDecimal(0.2)));
                       break;
                   case 4000:
                       return (float)(number1 * 60 / (Convert.ToDecimal(Scale) *  Convert.ToDecimal(0.1)));
                   default:
                       return 0;
               }
           }
       }
       public static decimal getNumber(decimal number1, float Scale)
       {
           int chose = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("chose"));
          
           if (chose==2) 
           {
               int n = Convert.ToInt32(Scale * 1000000);
               switch (n)
               {
                   case 15600:
                       return number1 * Convert.ToDecimal(Scale) / 120 * 40;
                       break;
                   case 31200:
                       return number1 * Convert.ToDecimal(Scale) / 120 * 20;
                       break;
                   case 62500:
                       return number1 * Convert.ToDecimal(Scale) / 120 * 10;
                   case 125000:
                       return number1 * Convert.ToDecimal(Scale) / 120 * 5;

                   case 250000:
                       return number1 * Convert.ToDecimal(Scale) / 120 * Convert.ToDecimal(2.5);

                   case 500000:
                       return number1 * Convert.ToDecimal(Scale) / 120 * Convert.ToDecimal(1.25);

                   case 1000000:
                       return number1 * Convert.ToDecimal(Scale) / 120 * Convert.ToDecimal(0.625);

                   case 2000000:
                       return number1 * Convert.ToDecimal(Scale) / 120 * Convert.ToDecimal(0.3125);

                   case 4000000:
                       return number1 * Convert.ToDecimal(Scale) / 120 * Convert.ToDecimal(0.15625);
                   default:
                       return 0;
               }
           }
           else
           {
               int n = Convert.ToInt32(Scale * 1000);
               switch (n)
               {
                   case 10:
                       return number1 * Convert.ToDecimal(Scale) / 60 * 40;
                       break;
                   case 20:
                       return number1 * Convert.ToDecimal(Scale) / 60 * 20;
                       break;
                   case 40:
                       return number1 * Convert.ToDecimal(Scale) / 60 * 10;
                   case 100:
                       return number1 * Convert.ToDecimal(Scale) / 60 * 4;

                   case 200:
                       return number1 * Convert.ToDecimal(Scale) / 60 * 2;

                   case 400:
                       return number1 * Convert.ToDecimal(Scale) / 60 * 1;

                   case 1000:
                       return number1 * Convert.ToDecimal(Scale) / 60 * Convert.ToDecimal(0.4);

                   case 2000:
                       return number1 * Convert.ToDecimal(Scale) / 60 * Convert.ToDecimal(0.2);

                   case 4000:
                       return number1 * Convert.ToDecimal(Scale) / 60 * Convert.ToDecimal(0.1);
                   default:
                       return 0;
               }
           }

       }
       public static decimal getNumber2(decimal number1, float Scale)
       {
           int chose = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("chose"));

           if (chose == 2)
           {
               int n = Convert.ToInt32(Scale * 1000000);
               switch (n)
               {
                   case 7812:
                       return number1 * Convert.ToDecimal(Scale) / 14400 * 80 / 2;

                   case 15625:
                       return number1 * Convert.ToDecimal(Scale) / 14400 * 40 / 2;

                   case 31250:
                       return number1 * Convert.ToDecimal(Scale) / 14400 * 20 / 2;

                  
                   case 15600:
                       return number1 * Convert.ToDecimal(Scale) / 14400 * 40 / 2;
                     
                   case 31200:
                       return number1 * Convert.ToDecimal(Scale) / 14400 * 20 / 2;
                      
                   case 62500:
                       return number1 * Convert.ToDecimal(Scale) / 14400 * 10/2 ;
                   case 125000:
                       return number1 * Convert.ToDecimal(Scale) / 14400 * 5 / 2;

                   case 250000:
                       return number1 * Convert.ToDecimal(Scale) / 14400 * Convert.ToDecimal(2.5) / 2;

                   case 500000:
                       return number1 * Convert.ToDecimal(Scale) / 14400 * Convert.ToDecimal(1.25) / 2;

                   case 1000000:
                       return number1 * Convert.ToDecimal(Scale) / 14400 * Convert.ToDecimal(0.625) / 2;

                   case 2000000:
                       return number1 * Convert.ToDecimal(Scale) / 14400 * Convert.ToDecimal(0.3125) / 2;

                   case 4000000:
                       return number1 * Convert.ToDecimal(Scale) / 14400 * Convert.ToDecimal(0.15625) / 2;
                   default:
                       return 0;
               }
           }
           else
           {
               int n = Convert.ToInt32(Scale * 1000);
               switch (n)
               {
                   case 10:
                       return number1 * Convert.ToDecimal(Scale) / 4500 * 40 / 2;
                    
                   case 20:
                       return number1 * Convert.ToDecimal(Scale) / 4500 * 20 / 2;
                     
                   case 40:
                       return number1 * Convert.ToDecimal(Scale) / 4500 * 10 / 2;

                   case 100:
                       return number1 * Convert.ToDecimal(Scale) / 4500 * 4 / 2;

                   case 200:
                       return number1 * Convert.ToDecimal(Scale) / 4500 * 2 / 2;

                   case 400:
                       return number1 * Convert.ToDecimal(Scale) / 4500 * 1 / 2;

                   case 1000:
                       return number1 * Convert.ToDecimal(Scale) / 4500 * Convert.ToDecimal(0.4) / 2;

                   case 2000:
                       return number1 * Convert.ToDecimal(Scale) / 4500 * Convert.ToDecimal(0.2) / 2;

                   case 4000:
                       return number1 * Convert.ToDecimal(Scale) / 4500 * Convert.ToDecimal(0.1) / 2;
                   default:
                       return 0;
               }
           }

       }
       

       public static PointF getUseOffset(string UseID)
       {
           switch (UseID){
               case "#Substation500-1":
               case "#gh-Substation500":
                   return new PointF(2328f, 2661f);
                 
               case "#Substation220-1":
                   return new PointF(2642f, 2867f);
               case "#Substation220-8":
               case "#Substation220-9":
              // case "#Substation220-1412":
               case "#Substation220-58":
                   return new PointF(2897f, 4864f);
               case "#Substation66-11":
               case "#Substation66-12":
               case "#Substation66-13":
               case "#Substation66-50":
               case "#Substation66-51":
                   return new PointF(2871f, 4084f);

               case "#user-Substation66-10":
               case "#user-Substation66-11":
               case "#user-Substation66-12":
               case "#user-Substation66-13":
               case "#user-Substation66-50":
               case "#user-Substation66-51":
               case "#Substation66-10":
                   return new PointF(2871f, 4084f);

               case "#user-Substation220-2":
               case "#user-Substation220-3":
                   return new PointF(2642f, 2867f);
              
                  
               case "#Substation110-1":
                   return new PointF(2838f, 2726f);
                  
               case "#user-Substation110-2":
              // case "#Substation110-1422":
               case "#user-Substation110-3":
                   return new PointF(-22f, 739f);
                  
               case "#Substation35-1":
               case "#Substation66-1":
                   return new PointF(-88f, 823f);
                  
               case "#user-Substation35-2":
              // case "#Substation35-1622":
               case "#user-Substation35-3":
                   return new PointF(-39f, 824f);
                 
           
               case "#gh-Substation220-15":
               case "#gh-Substation220-17":
               case "#gh-Substation220-66":
             
                   return new PointF(2897f, 4864f);
            
               case "#gh-Substation220-1":
                   return new PointF(230f, 770f);
                 
               case "#gh-user-Substation220-2":
                   return new PointF(-27f, 571f);
                  
               case "#gh-Substation110-1":
                   return new PointF(-22f, 739f);
                 
               case "#gh-user-Substation110-2":
                   return new PointF(-22f, 739f);

               case "#gh-Substation66-4":
               case "#gh-Substation66-5":
               case "#gh-Substation66-7":
               case "#gh-Substation66-8":
               case "#gh-Substation66-54":
               case "#gh-Substation66-55":
               case "#gh-user-Substation66-6":
               case "#gh-user-Substation66-8":
               case "#gh-user-Substation66-56":
                   return new PointF(2871f, 4084f);

               case "#gh-Substation35-1":
               case "gh-Substation66-1":
                   return new PointF(-39f, 824f);
                  
               case "#gh-user-Substation35-2":
                   return new PointF(-39f, 824f);
                  
               default:
                   return new PointF(0f,0f);
              
           }
       }
    }
    public class SubandFHcollect
    {
        public SubandFHcollect(Dictionary<XmlElement, PointF> _FHcollect, XmlElement _Sub)
        {
            FHcollect = _FHcollect;
            Sub = _Sub;
        }
        public Dictionary<XmlElement, PointF> FHcollect;
        public XmlElement Sub;
    }
    public class fhdkandcirsort : IComparable
    {
        private XmlElement xl;
        private PointF dkzx;
        private double len;
        public fhdkandcirsort(XmlElement _dk, PointF _dkzx, double _length)
        {
            xl = _dk;
            dkzx = _dkzx;
            len = _length;
        }
        public XmlElement DK
        {
            get { return xl; }
            set { xl = value; }
        }
        public PointF DKZX
        {
            get { return dkzx; }
            set { dkzx = value; }
        }
        public double Lenth
        {
            get { return len; }
            set { len = value; }
        }
         public int CompareTo(object obj)
        {
            int res = 0;
            try
            {
                fhdkandcirsort sObj = (fhdkandcirsort)obj;
                if (this.Lenth >= sObj.Lenth)
                {
                    res = 1;
                }
                else if (this.Lenth < sObj.Lenth)
                {
                    res = -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("比较异常", ex.InnerException);
            }
            return res;

        }

    }
}
