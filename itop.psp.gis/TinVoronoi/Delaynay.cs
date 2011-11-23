using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace TinVoronoi
{
    partial class Delaynay
    {
        public DataStruct DS = new DataStruct();  //数据结构

        public void Clear()
        {
            //for (int i = 0; i < DS.VerticesNum; i++)
            //{
            //    voronoiboundary v1 = DS.vetexboundary[i];
            //    v1.clear();
              
            //}
            DS.Vertex.Initialize();
            DS.Triangle.Initialize();
            DS.Barycenters.Initialize();
            DS.TinEdges.Initialize();
            DS.vetexboundary.Initialize();
            DS.VerticesNum = 0;
        }
        //构建Voronoi图
        /// <summary>
        ///构建Voronoi图
        /// </summary>

        public void CreateVoronoi()
        {
            //清除voroni图中的所有点
            for (int i = 0; i < DS.VerticesNum; i++)
            {
                voronoiboundary v1 = DS.vetexboundary[i];
                v1.clear();
            }
            for (int i = 0; i < DS.TinEdgeNum; i++)
            {
                voronoiboundary v1 = DS.vetexboundary[DS.TinEdges[i].Vertex1ID];
                //v1.clear();
                voronoiboundary v2 = DS.vetexboundary[DS.TinEdges[i].Vertex2ID];
                //v2.clear();
                if (!DS.TinEdges[i].NotHullEdge) //△边为凸壳边
                {
                    DrawHullVorEdge(i, v1, v2);
                    continue;
                }

                //连接左/右△的外接圆心
                long index1 = DS.TinEdges[i].AdjTriangle1ID;
                long index2 = DS.TinEdges[i].AdjTriangle2ID;
                PointF p1 = new PointF(Convert.ToSingle(DS.Barycenters[index1].X), Convert.ToSingle(DS.Barycenters[index1].Y));
                PointF p2 = new PointF(Convert.ToSingle(DS.Barycenters[index2].X), Convert.ToSingle(DS.Barycenters[index2].Y));
                if (v1.pointcompare(p1))
                {
                    v1.voronicollect.Add(p1);
                }
                if (v1.pointcompare(p2))
                {
                    v1.voronicollect.Add(p2);
                }
                if (v2.pointcompare(p1))
                {
                    v2.voronicollect.Add(p1);
                }
                if (v2.pointcompare(p2))
                {
                    v2.voronicollect.Add(p2);
                }
                //圆心在box外则直接跳过
                //if (PointInBox(p1) && PointInBox(p2))
                // g.DrawLine(new Pen(Color.Blue, 2), p1, p2);
                //else   //多边形裁剪
                //    In
            }
            for (int i = 0; i < DS.VerticesNum; i++)
            {
                if (DS.vetexboundary[i].hullflag)
                {
                    if (DS.vetexboundary[i].insertboundarycollect.Count == 2)
                    {
                        PointF p1 = DS.vetexboundary[i].insertboundarycollect[0];
                        PointF p2 = DS.vetexboundary[i].insertboundarycollect[1];
                        PointF insetpoint = new PointF();
                        if (p1.X != p2.X && p1.Y != p2.Y)
                        {

                            if (Convert.ToInt64(p1.X) == DS.BBOX.XLeft && Convert.ToInt64(p2.Y) == DS.BBOX.YTop)
                            {
                                insetpoint.X = p1.X; insetpoint.Y = p2.Y;
                                DS.vetexboundary[i].voronicollect.Add(insetpoint);
                            }
                            if (Convert.ToInt64(p1.X) == DS.BBOX.XLeft && Convert.ToInt64(p2.Y) == DS.BBOX.YBottom)
                            {
                                insetpoint.X = p1.X; insetpoint.Y = p2.Y;
                                DS.vetexboundary[i].voronicollect.Add(insetpoint);
                            }
                            if (Convert.ToInt64(p1.X) == DS.BBOX.XRight && Convert.ToInt64(p2.Y) == DS.BBOX.YTop)
                            {
                                insetpoint.X = p1.X; insetpoint.Y = p2.Y;
                                DS.vetexboundary[i].voronicollect.Add(insetpoint);
                            }
                            if (Convert.ToInt64(p1.X) == DS.BBOX.XRight && Convert.ToInt64(p2.Y) == DS.BBOX.YBottom)
                            {
                                insetpoint.X = p1.X; insetpoint.Y = p2.Y;
                                DS.vetexboundary[i].voronicollect.Add(insetpoint);
                            }


                            if (Convert.ToInt64(p2.X) == DS.BBOX.XLeft && Convert.ToInt64(p1.Y) == DS.BBOX.YTop)
                            {
                                insetpoint.X = p2.X; insetpoint.Y = p1.Y;
                                DS.vetexboundary[i].voronicollect.Add(insetpoint);
                            }
                            if (Convert.ToInt64(p2.X) == DS.BBOX.XLeft && Convert.ToInt64(p1.Y) == DS.BBOX.YBottom)
                            {
                                insetpoint.X = p2.X; insetpoint.Y = p1.Y;
                                DS.vetexboundary[i].voronicollect.Add(insetpoint);
                            }
                            if (Convert.ToInt64(p2.X) == DS.BBOX.XRight && Convert.ToInt64(p1.Y) == DS.BBOX.YTop)
                            {
                                insetpoint.X = p2.X; insetpoint.Y = p1.Y;
                                DS.vetexboundary[i].voronicollect.Add(insetpoint);
                            }
                            if (Convert.ToInt64(p2.X) == DS.BBOX.XRight && Convert.ToInt64(p1.Y) == DS.BBOX.YBottom)
                            {
                                insetpoint.X = p2.X; insetpoint.Y = p1.Y;
                                DS.vetexboundary[i].voronicollect.Add(insetpoint);
                            }

                            if ((Convert.ToInt64(p2.X) == DS.BBOX.XLeft && Convert.ToInt64(p1.X) == DS.BBOX.XRight) || (Convert.ToInt64(p2.X) == DS.BBOX.XRight && Convert.ToInt64(p1.X) == DS.BBOX.XLeft))
                            {
                                long topsum = Math.Abs(2 * DS.BBOX.YTop - Convert.ToInt64(p2.Y) - Convert.ToInt64(p1.Y)); long bottomsum = Math.Abs(2 * DS.BBOX.YBottom - Convert.ToInt64(p2.Y) - Convert.ToInt64(p1.Y));

                                if (topsum > bottomsum)
                                {
                                    insetpoint.X = p2.X;
                                    insetpoint.Y = DS.BBOX.YBottom;
                                    DS.vetexboundary[i].voronicollect.Add(insetpoint);
                                    insetpoint.X = p1.X;
                                    insetpoint.Y = DS.BBOX.YBottom;
                                    DS.vetexboundary[i].voronicollect.Add(insetpoint);
                                }
                                if (topsum < bottomsum)
                                {
                                    insetpoint.X = p2.X;
                                    insetpoint.Y = DS.BBOX.YTop;
                                    DS.vetexboundary[i].voronicollect.Add(insetpoint);
                                    insetpoint.X = p1.X;
                                    insetpoint.Y = DS.BBOX.YTop;
                                    DS.vetexboundary[i].voronicollect.Add(insetpoint);
                                }

                            }


                            if ((Convert.ToInt64(p2.Y) == DS.BBOX.YTop && Convert.ToInt64(p1.Y) == DS.BBOX.YBottom) || (Convert.ToInt64(p2.Y) == DS.BBOX.YBottom && Convert.ToInt64(p1.Y) == DS.BBOX.YTop))
                            {
                                long xleftsum = Math.Abs(2 * DS.BBOX.XLeft - Convert.ToInt64(p2.X) - Convert.ToInt64(p1.X)); long xrightsum = Math.Abs(2 * DS.BBOX.XRight - Convert.ToInt64(p2.X) - Convert.ToInt64(p1.X));
                                if (xleftsum > xrightsum)
                                {
                                    insetpoint.X = DS.BBOX.XRight;
                                    insetpoint.Y = p1.Y;
                                    DS.vetexboundary[i].voronicollect.Add(insetpoint);
                                    insetpoint.X = DS.BBOX.XRight;
                                    insetpoint.Y = p2.Y;
                                    DS.vetexboundary[i].voronicollect.Add(insetpoint);
                                }
                                if (xleftsum < xrightsum)
                                {
                                    insetpoint.X = DS.BBOX.XLeft;
                                    insetpoint.Y = p2.Y;
                                    DS.vetexboundary[i].voronicollect.Add(insetpoint);
                                    insetpoint.X = DS.BBOX.XLeft;
                                    insetpoint.Y = p1.Y;
                                    DS.vetexboundary[i].voronicollect.Add(insetpoint);
                                }
                            }



                            //DS.vetexboundary[i].voronicollect.Add(insetpoint);

                        }

                    }
                }
            }
        }
        /// <summary>
        ///构建Voronoi图
        /// </summary>
        /// <param name="g">画板</param>
       
        public void CreateVoronoi(Graphics g)
        {
            //清除voroni图中的所有点
            for (int i = 0; i < DS.VerticesNum;i++ )
            {
                voronoiboundary v1 = DS.vetexboundary[i];
                v1.clear();
            }
            for (int i = 0; i < DS.TinEdgeNum; i++)
            {
                voronoiboundary v1 = DS.vetexboundary[DS.TinEdges[i].Vertex1ID];
                //v1.clear();
                voronoiboundary v2 = DS.vetexboundary[DS.TinEdges[i].Vertex2ID];
                //v2.clear();
                if (!DS.TinEdges[i].NotHullEdge) //△边为凸壳边
                {
                    DrawHullVorEdge(i, v1, v2, g);
                    continue;
                }

                //连接左/右△的外接圆心
                long index1 = DS.TinEdges[i].AdjTriangle1ID;
                long index2 = DS.TinEdges[i].AdjTriangle2ID;
                PointF p1 = new PointF(Convert.ToSingle(DS.Barycenters[index1].X), Convert.ToSingle(DS.Barycenters[index1].Y));
                PointF p2 = new PointF(Convert.ToSingle(DS.Barycenters[index2].X), Convert.ToSingle(DS.Barycenters[index2].Y));
                if (v1.pointcompare(p1))
                {
                    v1.voronicollect.Add(p1);
                }
                if (v1.pointcompare(p2))
                {
                    v1.voronicollect.Add(p2);
                }
                if (v2.pointcompare(p1))
                {
                    v2.voronicollect.Add(p1);
                }
                if (v2.pointcompare(p2))
                {
                    v2.voronicollect.Add(p2);
                }
                //圆心在box外则直接跳过
                //if (PointInBox(p1) && PointInBox(p2))
                g.DrawLine(new Pen(Color.Blue, 2), p1, p2);
                //else   //多边形裁剪
                //    In
            }
            for (int i = 0; i < DS.VerticesNum; i++)
            {
                if (DS.vetexboundary[i].hullflag)
                {
                    if (DS.vetexboundary[i].insertboundarycollect.Count == 2)
                    {
                        PointF p1 = DS.vetexboundary[i].insertboundarycollect[0];
                        PointF p2 = DS.vetexboundary[i].insertboundarycollect[1];
                        PointF insetpoint = new PointF();
                        if (p1.X != p2.X && p1.Y != p2.Y)
                        {

                            if (Convert.ToInt64(p1.X) == DS.BBOX.XLeft && Convert.ToInt64(p2.Y) == DS.BBOX.YTop)
                            {
                                insetpoint.X = p1.X; insetpoint.Y = p2.Y;
                                DS.vetexboundary[i].voronicollect.Add(insetpoint);
                            }
                            if (Convert.ToInt64(p1.X) == DS.BBOX.XLeft && Convert.ToInt64(p2.Y) == DS.BBOX.YBottom)
                            {
                                insetpoint.X = p1.X; insetpoint.Y = p2.Y;
                                DS.vetexboundary[i].voronicollect.Add(insetpoint);
                            }
                            if (Convert.ToInt64(p1.X) == DS.BBOX.XRight && Convert.ToInt64(p2.Y) == DS.BBOX.YTop)
                            {
                                insetpoint.X = p1.X; insetpoint.Y = p2.Y;
                                DS.vetexboundary[i].voronicollect.Add(insetpoint);
                            }
                            if (Convert.ToInt64(p1.X) == DS.BBOX.XRight && Convert.ToInt64(p2.Y) == DS.BBOX.YBottom)
                            {
                                insetpoint.X = p1.X; insetpoint.Y = p2.Y;
                                DS.vetexboundary[i].voronicollect.Add(insetpoint);
                            }


                            if (Convert.ToInt64(p2.X) == DS.BBOX.XLeft && Convert.ToInt64(p1.Y) == DS.BBOX.YTop)
                            {
                                insetpoint.X = p2.X; insetpoint.Y = p1.Y;
                                DS.vetexboundary[i].voronicollect.Add(insetpoint);
                            }
                            if (Convert.ToInt64(p2.X) == DS.BBOX.XLeft && Convert.ToInt64(p1.Y) == DS.BBOX.YBottom)
                            {
                                insetpoint.X = p2.X; insetpoint.Y = p1.Y;
                                DS.vetexboundary[i].voronicollect.Add(insetpoint);
                            }
                            if (Convert.ToInt64(p2.X) == DS.BBOX.XRight && Convert.ToInt64(p1.Y) == DS.BBOX.YTop)
                            {
                                insetpoint.X = p2.X; insetpoint.Y = p1.Y;
                                DS.vetexboundary[i].voronicollect.Add(insetpoint);
                            }
                            if (Convert.ToInt64(p2.X) == DS.BBOX.XRight && Convert.ToInt64(p1.Y) == DS.BBOX.YBottom)
                            {
                                insetpoint.X = p2.X; insetpoint.Y = p1.Y;
                                DS.vetexboundary[i].voronicollect.Add(insetpoint);
                            }

                            if ((Convert.ToInt64(p2.X) == DS.BBOX.XLeft && Convert.ToInt64(p1.X) == DS.BBOX.XRight) || (Convert.ToInt64(p2.X) == DS.BBOX.XRight && Convert.ToInt64(p1.X) == DS.BBOX.XLeft))
                            {
                                long topsum = Math.Abs(2 * DS.BBOX.YTop - Convert.ToInt64(p2.Y) - Convert.ToInt64(p1.Y)); long bottomsum = Math.Abs(2 * DS.BBOX.YBottom - Convert.ToInt64(p2.Y) - Convert.ToInt64(p1.Y));

                                if (topsum > bottomsum)
                                {
                                    insetpoint.X = p2.X;
                                    insetpoint.Y = DS.BBOX.YBottom;
                                    DS.vetexboundary[i].voronicollect.Add(insetpoint);
                                    insetpoint.X = p1.X;
                                    insetpoint.Y = DS.BBOX.YBottom;
                                    DS.vetexboundary[i].voronicollect.Add(insetpoint);
                                }
                                if (topsum < bottomsum)
                                {
                                    insetpoint.X = p2.X;
                                    insetpoint.Y = DS.BBOX.YTop;
                                    DS.vetexboundary[i].voronicollect.Add(insetpoint);
                                    insetpoint.X = p1.X;
                                    insetpoint.Y = DS.BBOX.YTop;
                                    DS.vetexboundary[i].voronicollect.Add(insetpoint);
                                }

                            }


                            if ((Convert.ToInt64(p2.Y) == DS.BBOX.YTop && Convert.ToInt64(p1.Y) == DS.BBOX.YBottom) || (Convert.ToInt64(p2.Y) == DS.BBOX.YBottom && Convert.ToInt64(p1.Y) == DS.BBOX.YTop))
                            {
                                long xleftsum = Math.Abs(2 * DS.BBOX.XLeft - Convert.ToInt64(p2.X) - Convert.ToInt64(p1.X)); long xrightsum = Math.Abs(2 * DS.BBOX.XRight - Convert.ToInt64(p2.X) - Convert.ToInt64(p1.X));
                                if (xleftsum > xrightsum)
                                {
                                    insetpoint.X = DS.BBOX.XRight;
                                    insetpoint.Y = p1.Y;
                                    DS.vetexboundary[i].voronicollect.Add(insetpoint);
                                    insetpoint.X = DS.BBOX.XRight;
                                    insetpoint.Y = p2.Y;
                                    DS.vetexboundary[i].voronicollect.Add(insetpoint);
                                }
                                if (xleftsum < xrightsum)
                                {
                                    insetpoint.X = DS.BBOX.XLeft;
                                    insetpoint.Y = p2.Y;
                                    DS.vetexboundary[i].voronicollect.Add(insetpoint);
                                    insetpoint.X = DS.BBOX.XLeft;
                                    insetpoint.Y = p1.Y;
                                    DS.vetexboundary[i].voronicollect.Add(insetpoint);
                                }
                            }



                            //DS.vetexboundary[i].voronicollect.Add(insetpoint);

                        }

                    }
                }
            }
        }
        //获得各个三角网顶点的影响范围
        /// <summary>
        ///获得各个三角网顶点的影响范围
        /// </summary>
      
        public void GetVoronoi()
        {
            for (int i = 0; i < DS.VerticesNum; i++)
            {
                if (DS.Vertex[i].isHullEdge != 0) //△边为凸壳边
                {
                    getHullVorEdge(i);
                    continue;
                }

                //连接左/右△的外接圆心
                long index1 = DS.TinEdges[i].AdjTriangle1ID;
                long index2 = DS.TinEdges[i].AdjTriangle2ID;
                PointF p1 = new PointF(Convert.ToSingle(DS.Barycenters[index1].X), Convert.ToSingle(DS.Barycenters[index1].Y));
                PointF p2 = new PointF(Convert.ToSingle(DS.Barycenters[index2].X), Convert.ToSingle(DS.Barycenters[index2].Y));
                //圆心在box外则直接跳过
                //if (PointInBox(p1) && PointInBox(p2))
                //  g.DrawLine(new Pen(Color.Blue, 2), p1, p2);
                //else   //多边形裁剪
                //    In
            }
        }
        //获得凸点voronoi
        public void getHullVorEdge(int i)
        {

        }

        //增量法生成Delaunay三角网
        /// <summary>
        ///增量法生成Delaunay三角网
        /// </summary>
        public void CreateTIN()
        {
            //建立凸壳并三角剖分
            CreateConvex();
            HullTriangulation();

            //逐点插入
            PlugInEveryVertex();

            //建立边的拓扑结构
            TopologizeEdge();
        }

        //逐点加入修改TIN
        /// <summary>
        ///逐点加入修改TIN
        /// </summary>
        private void PlugInEveryVertex()
        {
            Edge[] EdgesBuf = new Edge[DataStruct.MaxTriangles];  //△边缓冲区

            bool IsInCircle;
            int i, j, k;
            int EdgeCount;
            for (i = 0; i < DS.VerticesNum; i++)    //逐点加入
            {
                //跳过凸壳顶点
                if (DS.Vertex[i].isHullEdge != 0)
                    continue;

                EdgeCount = 0;
                for (j = 0; j < DS.TriangleNum; j++) //定位待插入点影响的所有△
                {
                    IsInCircle = InTriangleExtCircle(DS.Vertex[i].x, DS.Vertex[i].y, DS.Vertex[DS.Triangle[j].V1Index].x, DS.Vertex[DS.Triangle[j].V1Index].y,
                        DS.Vertex[DS.Triangle[j].V2Index].x, DS.Vertex[DS.Triangle[j].V2Index].y,
                        DS.Vertex[DS.Triangle[j].V3Index].x, DS.Vertex[DS.Triangle[j].V3Index].y);
                    if (IsInCircle)    //△j在影响范围内
                    {
                        Edge[] eee ={new Edge(DS.Triangle[j].V1Index, DS.Triangle[j].V2Index),
                            new Edge(DS.Triangle[j].V2Index, DS.Triangle[j].V3Index),
                            new Edge(DS.Triangle[j].V3Index, DS.Triangle[j].V1Index)};  //△的三边

                        #region 存储除公共边外的△边
                        bool IsNotComnEdge;
                        for (k = 0; k < 3; k++)
                        {
                            IsNotComnEdge = true;
                            for (int n = 0; n < EdgeCount; n++)
                            {
                                if (Edge.Compare(eee[k], EdgesBuf[n]))   //此边为公共边
                                {
                                    //删除已缓存的公共边
                                    IsNotComnEdge = false;
                                    EdgesBuf[n] = EdgesBuf[EdgeCount - 1];
                                    EdgeCount--;
                                    break;
                                }
                            }

                            if (IsNotComnEdge)
                            {
                                EdgesBuf[EdgeCount] = eee[k];    //边加入Buffer
                                EdgeCount++;
                            }
                        }
                        #endregion

                        //删除△j, 表尾△前移插入
                        DS.Triangle[j].V1Index = DS.Triangle[DS.TriangleNum - 1].V1Index;
                        DS.Triangle[j].V2Index = DS.Triangle[DS.TriangleNum - 1].V2Index;
                        DS.Triangle[j].V3Index = DS.Triangle[DS.TriangleNum - 1].V3Index;
                        j--;
                        DS.TriangleNum--;
                    }
                }//for 定位点

                #region 构建新△
                for (j = 0; j < EdgeCount; j++)
                {
                    DS.Triangle[DS.TriangleNum].V1Index = EdgesBuf[j].Vertex1ID;
                    DS.Triangle[DS.TriangleNum].V2Index = EdgesBuf[j].Vertex2ID;
                    DS.Triangle[DS.TriangleNum].V3Index = i;
                    DS.TriangleNum++;
                }
                #endregion
            }//逐点加入for
        }

        //计算外接圆圆心
        /// <summary>
        ///计算外接圆圆心
        /// </summary>
        public void CalculateBC()
        {
            double x1, y1, x2, y2, x3, y3;
            for (int i = 0; i < DS.TriangleNum; i++)
            {
                //计算△的外接圆心
                x1 = DS.Vertex[DS.Triangle[i].V1Index].x;
                y1 = DS.Vertex[DS.Triangle[i].V1Index].y;
                x2 = DS.Vertex[DS.Triangle[i].V2Index].x;
                y2 = DS.Vertex[DS.Triangle[i].V2Index].y;
                x3 = DS.Vertex[DS.Triangle[i].V3Index].x;
                y3 = DS.Vertex[DS.Triangle[i].V3Index].y;
                GetTriangleBarycnt(x1, y1, x2, y2, x3, y3, ref DS.Barycenters[i].X, ref DS.Barycenters[i].Y);
            }

        }

        //求△的外接圆心
        /// <summary>
        ///求△的外接圆心
        /// </summary>
        private void GetTriangleBarycnt(double x1, double y1, double x2, double y2, double x3, double y3, ref double bcX, ref double bcY)
        {
            double precision = 0.000001;
            double k1, k2;   //两条中垂线斜率

            //三点共线
            if (Math.Abs(y1 - y2) < precision && Math.Abs(y2 - y3) < precision)
            {
                MessageBox.Show("Three Points on one line!");
                Application.Exit();
            }

            //边的中点
            double MidX1 = (x1 + x2) / 2;
            double MidY1 = (y1 + y2) / 2;
            double MidX2 = (x3 + x2) / 2;
            double MidY2 = (y3 + y2) / 2;

            if (Math.Abs(y2 - y1) < precision)  //p1p2平行于X轴
            {
                k2 = -(x3 - x2) / (y3 - y2);
                bcX = MidX1;
                bcY = k2 * (bcX - MidX2) + MidY2;
            }
            else if (Math.Abs(y3 - y2) < precision)   //p2p3平行于X轴
            {
                k1 = -(x2 - x1) / (y2 - y1);
                bcX = MidX2;
                bcY = k1 * (bcX - MidX1) + MidY1;
            }
            else
            {
                k1 = -(x2 - x1) / (y2 - y1);
                k2 = -(x3 - x2) / (y3 - y2);
                bcX = (k1 * MidX1 - k2 * MidX2 + MidY2 - MidY1) / (k1 - k2);
                bcY = k1 * (bcX - MidX1) + MidY1;
            }
        }

        //判断点是否在△的外接圆中
        /// <summary>
        ///判断点是否在△的外接圆中
        /// </summary>
        private Boolean InTriangleExtCircle(double xp, double yp, double x1, double y1, double x2, double y2, double x3, double y3)
        {
            double RadiusSquare;    //半径的平方
            double DisSquare;  //距离的平方
            double BaryCntX = 0, BaryCntY = 0;
            GetTriangleBarycnt(x1, y1, x2, y2, x3, y3, ref  BaryCntX, ref BaryCntY);

            RadiusSquare = (x1 - BaryCntX) * (x1 - BaryCntX) + (y1 - BaryCntY) * (y1 - BaryCntY);
            DisSquare = (xp - BaryCntX) * (xp - BaryCntX) + (yp - BaryCntY) * (yp - BaryCntY);

            if (DisSquare <= RadiusSquare)
                return true;
            else
                return false;
        }

        //建立Edge的拓扑关系
        /// <summary>
        ///建立Edge的拓扑关系
        /// </summary>
        public void TopologizeEdge()
        {
            DS.TinEdgeNum = 0;
            DS.TinEdges = new Edge[DataStruct.MaxEdges];   //清除旧数据
            long[] Vindex = new long[3]; //3个顶点索引

            //遍历每个△的三条边
            for (int i = 0; i < DS.TriangleNum; i++)
            {
                Vindex[0] = DS.Triangle[i].V1Index;
                Vindex[1] = DS.Triangle[i].V2Index;
                Vindex[2] = DS.Triangle[i].V3Index;

                for (int j = 0; j < 3; j++)   //每条边
                {
                    Edge e = new Edge(Vindex[j], Vindex[(j + 1) % 3]);

                    //判断边在数组中是否已存在
                    int k;
                    for (k = 0; k < DS.TinEdgeNum; k++)
                    {
                        if (Edge.Compare(e, DS.TinEdges[k]))   //此边已构造
                        {
                            DS.TinEdges[k].AdjTriangle2ID = i;
                            DS.TinEdges[k].NotHullEdge = true;
                            break;
                        }
                    }//for

                    if (k == DS.TinEdgeNum)   //此边为新边
                    {
                        DS.TinEdges[DS.TinEdgeNum].Vertex1ID = e.Vertex1ID;
                        DS.TinEdges[DS.TinEdgeNum].Vertex2ID = e.Vertex2ID;
                        DS.TinEdges[DS.TinEdgeNum].AdjTriangle1ID = i;
                        DS.TinEdges[DS.TinEdgeNum].AdjacentT1V3 = Vindex[(j + 2) % 3];
                        DS.TinEdgeNum++;
                    }

                }//for,每条边
            }//for,每个△
        }
        //将所有的中心点填入voronoiboundary
        /// <summary>
        ///将所有的中心点填入voronoiboundary
        /// </summary>
        private void DrawHullVorEdge(int i, voronoiboundary v1, voronoiboundary v2)
        {
            PointF pnt1 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex1ID].x),
                 Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex1ID].y));
            PointF pnt2 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex2ID].x),
                Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex2ID].y));
            PointF pnt3 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[i].AdjacentT1V3].x),
                Convert.ToSingle(DS.Vertex[DS.TinEdges[i].AdjacentT1V3].y));    //边对应的△顶点

            PointF MidPnt = new PointF((pnt1.X + pnt2.X) / 2, (pnt2.Y + pnt2.Y) / 2);  //TinEdge中点
            PointF BaryCnt = new PointF(Convert.ToSingle(DS.Barycenters[DS.TinEdges[i].AdjTriangle1ID].X),
                Convert.ToSingle(DS.Barycenters[DS.TinEdges[i].AdjTriangle1ID].Y));  //外接圆心
            PointF EndPnt = new PointF();   //圆心连接于此点构成VEdge

            //圆心在box外则直接跳过
            //if (!(BaryCnt.X >= DS.BBOX.XLeft && BaryCnt.X <= DS.BBOX.XRight &&
            //    BaryCnt.Y >= DS.BBOX.YTop && BaryCnt.Y <= DS.BBOX.YBottom))
            //    return;

            //求斜率
            float k = 0;  //斜率
            bool KExist = true;
            if (Math.Abs(pnt1.Y - pnt2.Y) < 0.000001)
                KExist = false;     //k不存在
            else
                k = (pnt1.X - pnt2.X) / (pnt2.Y - pnt1.Y);

            //该凸壳边是△的钝角边则外接圆心在△外
            bool obtEdge = IsObtuseEdge(i);

            #region 根据△圆心在凸壳内还是在外求VEdge
            //圆心在边右则往左延伸，在左则往右

            if (!obtEdge)   //圆心在凸壳内(或边界上)/////////////////////////////////
            {
                if (!KExist)    //k不存在
                {
                    // MessageBox.Show("斜率不存在的△-"+DS.TinEdges[i].AdjTriangle1ID.ToString());
                    if (BaryCnt.Y > MidPnt.Y || BaryCnt.Y < pnt3.Y)// BaryCnt<y3 ->圆心与中点重合
                        EndPnt.Y = DS.BBOX.YTop;
                    else if (BaryCnt.Y < MidPnt.Y || BaryCnt.Y > pnt3.Y)
                        EndPnt.Y = DS.BBOX.YBottom;

                    EndPnt.X = BaryCnt.X;
                }
                else      //K存在
                {
                    if (BaryCnt.X > MidPnt.X || (BaryCnt.X == MidPnt.X && BaryCnt.X < pnt3.X))
                        EndPnt.X = DS.BBOX.XLeft;
                    else if (BaryCnt.X < MidPnt.X || (BaryCnt.X == MidPnt.X && BaryCnt.X > pnt3.X))
                        EndPnt.X = DS.BBOX.XRight;

                    EndPnt.Y = k * (EndPnt.X - BaryCnt.X) + BaryCnt.Y;
                }

            }
            else    //圆心在凸壳外/////////////////////////////////////////////
            {
                if (!KExist)    //k不存在
                {
                    if (BaryCnt.Y < MidPnt.Y || BaryCnt.Y < pnt3.Y)
                        EndPnt.Y = DS.BBOX.YTop;
                    else if (BaryCnt.Y > MidPnt.Y || BaryCnt.Y > pnt3.Y)
                        EndPnt.Y = DS.BBOX.YBottom;

                    EndPnt.X = BaryCnt.X;
                }
                else   //K存在
                {
                    if (BaryCnt.X < MidPnt.X)
                        EndPnt.X = DS.BBOX.XLeft;
                    else if (BaryCnt.X > MidPnt.X)
                        EndPnt.X = DS.BBOX.XRight;

                    EndPnt.Y = k * (EndPnt.X - BaryCnt.X) + BaryCnt.Y;
                }

            }//else 在△外

            //与外框交点在边界外的处理
            if (k != 0 && KExist)
            {
                if (EndPnt.Y < DS.BBOX.YTop)
                    EndPnt.Y = DS.BBOX.YTop;
                else if (EndPnt.Y > DS.BBOX.YBottom)
                    EndPnt.Y = DS.BBOX.YBottom;

                EndPnt.X = (EndPnt.Y - BaryCnt.Y) / k + BaryCnt.X;
            }

            #endregion
            v1.hullflag = true; v2.hullflag = true;
            if (v1.pointcompare(BaryCnt))
            {
                v1.voronicollect.Add(BaryCnt);
            }
            if (v2.pointcompare(BaryCnt))
            {
                v2.voronicollect.Add(BaryCnt);
            }
            v1.voronicollect.Add(EndPnt); v2.voronicollect.Add(EndPnt);
            v1.insertboundarycollect.Add(EndPnt); v2.insertboundarycollect.Add(EndPnt);
          //  g.DrawLine(new Pen(Color.Blue, 2), BaryCnt, EndPnt);

        }
        //将所得中心点填入其中
        /// <summary>
        ///将所得中心点填入其中，并显示出来
        /// </summary>
        private void DrawHullVorEdge(int i, voronoiboundary v1, voronoiboundary v2, Graphics g)
        {
            PointF pnt1 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex1ID].x),
                 Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex1ID].y));
            PointF pnt2 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex2ID].x),
                Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex2ID].y));
            PointF pnt3 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[i].AdjacentT1V3].x),
                Convert.ToSingle(DS.Vertex[DS.TinEdges[i].AdjacentT1V3].y));    //边对应的△顶点

            PointF MidPnt = new PointF((pnt1.X + pnt2.X) / 2, (pnt2.Y + pnt2.Y) / 2);  //TinEdge中点
            PointF BaryCnt = new PointF(Convert.ToSingle(DS.Barycenters[DS.TinEdges[i].AdjTriangle1ID].X),
                Convert.ToSingle(DS.Barycenters[DS.TinEdges[i].AdjTriangle1ID].Y));  //外接圆心
            PointF EndPnt = new PointF();   //圆心连接于此点构成VEdge

            //圆心在box外则直接跳过
            if (!(BaryCnt.X >= DS.BBOX.XLeft && BaryCnt.X <= DS.BBOX.XRight &&
                BaryCnt.Y >= DS.BBOX.YTop && BaryCnt.Y <= DS.BBOX.YBottom))
                return;

            //求斜率
            float k = 0;  //斜率
            bool KExist = true;
            if (Math.Abs(pnt1.Y - pnt2.Y) < 0.000001)
                KExist = false;     //k不存在
            else
                k = (pnt1.X - pnt2.X) / (pnt2.Y - pnt1.Y);

            //该凸壳边是△的钝角边则外接圆心在△外
            bool obtEdge = IsObtuseEdge(i);

            #region 根据△圆心在凸壳内还是在外求VEdge
            //圆心在边右则往左延伸，在左则往右

            if (!obtEdge)   //圆心在凸壳内(或边界上)/////////////////////////////////
            {
                if (!KExist)    //k不存在
                {
                    // MessageBox.Show("斜率不存在的△-"+DS.TinEdges[i].AdjTriangle1ID.ToString());
                    if (BaryCnt.Y > MidPnt.Y || BaryCnt.Y < pnt3.Y)// BaryCnt<y3 ->圆心与中点重合
                        EndPnt.Y = DS.BBOX.YTop;
                    else if (BaryCnt.Y < MidPnt.Y || BaryCnt.Y > pnt3.Y)
                        EndPnt.Y = DS.BBOX.YBottom;

                    EndPnt.X = BaryCnt.X;
                }
                else      //K存在
                {
                    if (BaryCnt.X > MidPnt.X || (BaryCnt.X == MidPnt.X && BaryCnt.X < pnt3.X))
                        EndPnt.X = DS.BBOX.XLeft;
                    else if (BaryCnt.X < MidPnt.X || (BaryCnt.X == MidPnt.X && BaryCnt.X > pnt3.X))
                        EndPnt.X = DS.BBOX.XRight;

                    EndPnt.Y = k * (EndPnt.X - BaryCnt.X) + BaryCnt.Y;
                }

            }
            else    //圆心在凸壳外/////////////////////////////////////////////
            {
                if (!KExist)    //k不存在
                {
                    if (BaryCnt.Y < MidPnt.Y || BaryCnt.Y < pnt3.Y)
                        EndPnt.Y = DS.BBOX.YTop;
                    else if (BaryCnt.Y > MidPnt.Y || BaryCnt.Y > pnt3.Y)
                        EndPnt.Y = DS.BBOX.YBottom;

                    EndPnt.X = BaryCnt.X;
                }
                else   //K存在
                {
                    if (BaryCnt.X < MidPnt.X)
                        EndPnt.X = DS.BBOX.XLeft;
                    else if (BaryCnt.X > MidPnt.X)
                        EndPnt.X = DS.BBOX.XRight;

                    EndPnt.Y = k * (EndPnt.X - BaryCnt.X) + BaryCnt.Y;
                }

            }//else 在△外

            //与外框交点在边界外的处理
            if (k != 0 && KExist)
            {
                if (EndPnt.Y < DS.BBOX.YTop)
                    EndPnt.Y = DS.BBOX.YTop;
                else if (EndPnt.Y > DS.BBOX.YBottom)
                    EndPnt.Y = DS.BBOX.YBottom;

                EndPnt.X = (EndPnt.Y - BaryCnt.Y) / k + BaryCnt.X;
            }

            #endregion
            v1.hullflag = true; v2.hullflag = true;
            if (v1.pointcompare(BaryCnt))
            {
                v1.voronicollect.Add(BaryCnt);
            }
            if (v2.pointcompare(BaryCnt))
            {
                v2.voronicollect.Add(BaryCnt);
            }
            v1.voronicollect.Add(EndPnt); v2.voronicollect.Add(EndPnt);
            v1.insertboundarycollect.Add(EndPnt); v2.insertboundarycollect.Add(EndPnt);
            g.DrawLine(new Pen(Color.Blue, 2), BaryCnt, EndPnt);

        }
        //i为TinEdge的ID号
        private void DrawHullVorEdge(int i, Graphics g)
        {

            PointF pnt1 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex1ID].x),
                Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex1ID].y));
            PointF pnt2 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex2ID].x),
                Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex2ID].y));
            PointF pnt3 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[i].AdjacentT1V3].x),
                Convert.ToSingle(DS.Vertex[DS.TinEdges[i].AdjacentT1V3].y));    //边对应的△顶点

            PointF MidPnt = new PointF((pnt1.X + pnt2.X) / 2, (pnt2.Y + pnt2.Y) / 2);  //TinEdge中点
            PointF BaryCnt = new PointF(Convert.ToSingle(DS.Barycenters[DS.TinEdges[i].AdjTriangle1ID].X),
                Convert.ToSingle(DS.Barycenters[DS.TinEdges[i].AdjTriangle1ID].Y));  //外接圆心
            PointF EndPnt = new PointF();   //圆心连接于此点构成VEdge

            //圆心在box外则直接跳过
            if (!(BaryCnt.X >= DS.BBOX.XLeft && BaryCnt.X <= DS.BBOX.XRight &&
                BaryCnt.Y >= DS.BBOX.YTop && BaryCnt.Y <= DS.BBOX.YBottom))
                return;

            //求斜率
            float k = 0;  //斜率
            bool KExist = true;
            if (Math.Abs(pnt1.Y - pnt2.Y) < 0.000001)
                KExist = false;     //k不存在
            else
                k = (pnt1.X - pnt2.X) / (pnt2.Y - pnt1.Y);

            //该凸壳边是△的钝角边则外接圆心在△外
            bool obtEdge = IsObtuseEdge(i);

            #region 根据△圆心在凸壳内还是在外求VEdge
            //圆心在边右则往左延伸，在左则往右

            if (!obtEdge)   //圆心在凸壳内(或边界上)/////////////////////////////////
            {
                if (!KExist)    //k不存在
                {
                    // MessageBox.Show("斜率不存在的△-"+DS.TinEdges[i].AdjTriangle1ID.ToString());
                    if (BaryCnt.Y > MidPnt.Y || BaryCnt.Y < pnt3.Y)// BaryCnt<y3 ->圆心与中点重合
                        EndPnt.Y = DS.BBOX.YTop;
                    else if (BaryCnt.Y < MidPnt.Y || BaryCnt.Y > pnt3.Y)
                        EndPnt.Y = DS.BBOX.YBottom;

                    EndPnt.X = BaryCnt.X;
                }
                else      //K存在
                {
                    if (BaryCnt.X > MidPnt.X || (BaryCnt.X == MidPnt.X && BaryCnt.X < pnt3.X))
                        EndPnt.X = DS.BBOX.XLeft;
                    else if (BaryCnt.X < MidPnt.X || (BaryCnt.X == MidPnt.X && BaryCnt.X > pnt3.X))
                        EndPnt.X = DS.BBOX.XRight;

                    EndPnt.Y = k * (EndPnt.X - BaryCnt.X) + BaryCnt.Y;
                }

            }
            else    //圆心在凸壳外/////////////////////////////////////////////
            {
                if (!KExist)    //k不存在
                {
                    if (BaryCnt.Y < MidPnt.Y || BaryCnt.Y < pnt3.Y)
                        EndPnt.Y = DS.BBOX.YTop;
                    else if (BaryCnt.Y > MidPnt.Y || BaryCnt.Y > pnt3.Y)
                        EndPnt.Y = DS.BBOX.YBottom;

                    EndPnt.X = BaryCnt.X;
                }
                else   //K存在
                {
                    if (BaryCnt.X < MidPnt.X)
                        EndPnt.X = DS.BBOX.XLeft;
                    else if (BaryCnt.X > MidPnt.X)
                        EndPnt.X = DS.BBOX.XRight;

                    EndPnt.Y = k * (EndPnt.X - BaryCnt.X) + BaryCnt.Y;
                }

            }//else 在△外

            //与外框交点在边界外的处理
            if (k != 0 && KExist)
            {
                if (EndPnt.Y < DS.BBOX.YTop)
                    EndPnt.Y = DS.BBOX.YTop;
                else if (EndPnt.Y > DS.BBOX.YBottom)
                    EndPnt.Y = DS.BBOX.YBottom;

                EndPnt.X = (EndPnt.Y - BaryCnt.Y) / k + BaryCnt.X;
            }

            #endregion

            g.DrawLine(new Pen(Color.Blue, 2), BaryCnt, EndPnt);

        }

        //index为TinEdge的索引号,若为钝角边则返回true
        /// <summary>
        ///index为TinEdge的索引号,若为钝角边则返回true
        /// </summary>
        private bool IsObtuseEdge(int index)
        {
            PointF EdgePnt1 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[index].Vertex1ID].x),
                Convert.ToSingle(DS.Vertex[DS.TinEdges[index].Vertex1ID].y));
            PointF EdgePnt2 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[index].Vertex2ID].x),
                Convert.ToSingle(DS.Vertex[DS.TinEdges[index].Vertex2ID].y));
            PointF Pnt3 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[index].AdjacentT1V3].x),
                 Convert.ToSingle(DS.Vertex[DS.TinEdges[index].AdjacentT1V3].y));

            PointF V1 = new PointF((EdgePnt1.X - Pnt3.X), (EdgePnt1.Y - Pnt3.Y));
            PointF V2 = new PointF((EdgePnt2.X - Pnt3.X), (EdgePnt2.Y - Pnt3.Y));
            return (V1.X * V2.X + V1.Y * V2.Y) < 0; //a·b的值<0则为钝角
        }

        //？？？？？？Unused？点在△内则返回true
        /// <summary>
        ///点在△内则返回true
        /// </summary>
        /// <param name="PntIndex">点的ID</param>
        /// <param name="index">三角形的索引ID</param>
        /// <returns>ture为在内部，false为在外部</returns>
        private bool PointInTriganle(long PntIndex, long index)
        {
            PointF pnt1 = new PointF(Convert.ToSingle(DS.Vertex[DS.Triangle[index].V1Index].x),
                Convert.ToSingle(DS.Vertex[DS.Triangle[index].V1Index].y));
            PointF pnt2 = new PointF(Convert.ToSingle(DS.Vertex[DS.Triangle[index].V2Index].x),
                Convert.ToSingle(DS.Vertex[DS.Triangle[index].V2Index].y));
            PointF pnt3 = new PointF(Convert.ToSingle(DS.Vertex[DS.Triangle[index].V3Index].x),
                 Convert.ToSingle(DS.Vertex[DS.Triangle[index].V3Index].y));
            PointF JudgePoint = new PointF(Convert.ToSingle(DS.Barycenters[index].X), Convert.ToSingle(DS.Barycenters[index].Y));  //外接圆心

            int IsPositive;    //正则等于1，负则等于-1
            double result = VectorXMultiply(JudgePoint, pnt1, pnt2);
            if (result > 0)
                IsPositive = 1;
            else
                IsPositive = -1;

            result = VectorXMultiply(JudgePoint, pnt2, pnt3);
            if ((IsPositive == 1 && result < 0) || (IsPositive == -1 && result > 0))
                return false;

            result = VectorXMultiply(JudgePoint, pnt3, pnt1);
            if ((IsPositive == 1 && result > 0) || (IsPositive == -1 && result < 0))
                return true;
            else
                return false;
        }

        private double VectorXMultiply(PointF BaryCnt, PointF pnt1, PointF pnt2)
        {
            PointF V1 = new PointF((pnt1.X - BaryCnt.X), (pnt1.Y - BaryCnt.Y));
            PointF V2 = new PointF((pnt2.X - BaryCnt.X), (pnt2.Y - BaryCnt.Y));
            return (V1.X * V2.Y - V2.X * V1.Y);
        }
        private bool PointInBox(PointF point)
        {
            return (point.X >= DS.BBOX.XLeft && point.X <= DS.BBOX.XRight &&
                    point.Y >= DS.BBOX.YTop && point.Y <= DS.BBOX.YBottom);
        }
        //求半径
        private double dis_2(Vertex from, Vertex to)
        {
            return ((from.x - to.x) * (from.x - to.x) + (from.y - to.y) * (from.y - to.y));
        }
        //判断是否在圆内
       private int in_cic(int pt)
        {
            if (Math.Sqrt(dis_2(DS.maxcic, DS.Vertex[pt])) < DS.radius + DS.precison) return 1;
            return 0;
        }
        //计算最小圆心
       public int cal_mincic()
        {
            if (DS.pos_cnt == 1 || DS.pos_cnt == 0)
                return 0;
            else if (DS.pos_cnt == 3)
            {
                double A1, B1, C1, A2, B2, C2;
                int t0 = DS.posset[0], t1 = DS.posset[1], t2 = DS.posset[2];
                A1 = 2 * (DS.Vertex[t1].x - DS.Vertex[t0].x);
                B1 = 2 * (DS.Vertex[t1].y - DS.Vertex[t0].y);
                C1 = DS.Vertex[t1].x * DS.Vertex[t1].x - DS.Vertex[t0].x * DS.Vertex[t0].x
                 + DS.Vertex[t1].y * DS.Vertex[t1].y - DS.Vertex[t0].y * DS.Vertex[t0].y;
                A2 = 2 * (DS.Vertex[t2].x - DS.Vertex[t0].x);
                B2 = 2 * (DS.Vertex[t2].y - DS.Vertex[t0].y);
                C2 = DS.Vertex[t2].x * DS.Vertex[t2].x - DS.Vertex[t0].x * DS.Vertex[t0].x
                 + DS.Vertex[t2].y * DS.Vertex[t2].y - DS.Vertex[t0].y * DS.Vertex[t0].y;
                DS.maxcic.y = Convert.ToInt64((C1 * A2 - C2 * A1) / (A2 * B1 - A1 * B2));
                DS.maxcic.x = Convert.ToInt64((C1 * B2 - C2 * B1) / (A1 * B2 - A2 * B1));
                DS.radius = Math.Sqrt(dis_2(DS.maxcic, DS.Vertex[t0]));

            }
            else if (DS.pos_cnt == 2)
            {
                DS.maxcic.x = (DS.Vertex[DS.posset[0]].x + DS.Vertex[DS.posset[1]].x) / 2;
                DS.maxcic.y = (DS.Vertex[DS.posset[0]].y + DS.Vertex[DS.posset[1]].y) / 2;
                DS.radius = Math.Sqrt(dis_2(DS.Vertex[DS.posset[0]], DS.Vertex[DS.posset[1]])) / 2;
            }
            return 1;
        }
        //计算最小覆盖圆方法
        public int mindisk()
        {
            if (DS.set_cnt == 0 || DS.pos_cnt == 3)
            {
                return cal_mincic();
            }

            int tt = DS.curset[--DS.set_cnt];
            int res = mindisk();
            DS.set_cnt++;

            if (res == 0 || 0 == in_cic(tt))
            {
                DS.set_cnt--;
                DS.posset[DS.pos_cnt++] = DS.curset[DS.set_cnt];
                res = mindisk();
                DS.pos_cnt--;
                DS.curset[DS.set_cnt++] = DS.curset[0];
                DS.curset[0] = tt;
            }

            return res;
        }
        /// <summary>
        /// 判断两条线是否相交
        /// </summary>
        /// <param name="a">线段1起点坐标</param>
        /// <param name="b">线段1终点坐标</param>
        /// <param name="c">线段2起点坐标</param>
        /// <param name="d">线段2终点坐标</param>
        /// <param name="intersection">相交点坐标</param>
        /// <returns>是否相交 0:两线平行  -1:不平行且未相交  1:两线相交</returns>
        private int GetIntersection(PointF a, PointF b, PointF c, PointF d, ref PointF intersection)
        {

            //判断异常
            if (Math.Abs(b.X - a.Y) + Math.Abs(b.X - a.X) + Math.Abs(d.Y - c.Y) + Math.Abs(d.X - c.X) == 0)
            {
                if (c.X - a.X == 0)
                //{
                //    Debug.Print("ABCD是同一个点！");
                //}
                //else
                //{
                //    Debug.Print("AB是一个点，CD是一个点，且AC不同！");
                //}
                return 0;
            }

            if (Math.Abs(b.Y - a.Y) + Math.Abs(b.X - a.X) == 0)
            {
                //if ((a.X - d.X) * (c.Y - d.Y) - (a.Y - d.Y) * (c.X - d.X) == 0)
                //{
                //    Debug.Print("A、B是一个点，且在CD线段上！");
                //}
                //else
                //{
                //    Debug.Print("A、B是一个点，且不在CD线段上！");
                //}
                return 2;
            }
            //if (Math.Abs(d.Y - c.Y) + Math.Abs(d.X - c.X) == 0)
            //{
            //    if ((d.X - b.X) * (a.Y - b.Y) - (d.Y - b.Y) * (a.X - b.X) == 0)
            //    {
            //        Debug.Print("C、D是一个点，且在AB线段上！");
            //    }
            //    else
            //    {
            //        Debug.Print("C、D是一个点，且不在AB线段上！");
            //    }
            //}

            if ((b.Y - a.Y) * (c.X - d.X) - (b.X - a.X) * (c.Y - d.Y) == 0)
            {
                //Debug.Print("线段平行，无交点！");
                return 0;
            }

            intersection.X = ((b.X - a.X) * (c.X - d.X) * (c.Y - a.Y) - c.X * (b.X - a.X) * (c.Y - d.Y) + a.X * (b.Y - a.Y) * (c.X - d.X)) / ((b.Y - a.Y) * (c.X - d.X) - (b.X - a.X) * (c.Y - d.Y));
            intersection.Y = ((b.Y - a.Y) * (c.Y - d.Y) * (c.X - a.X) - c.Y * (b.Y - a.Y) * (c.X - d.X) + a.Y * (b.X - a.X) * (c.Y - d.Y)) / ((b.X - a.X) * (c.Y - d.Y) - (b.Y - a.Y) * (c.X - d.X));

            if ((intersection.X - a.X) * (intersection.X - b.X) <= 0 && (intersection.X - c.X) * (intersection.X - d.X) <= 0 && (intersection.Y - a.Y) * (intersection.Y - b.Y) <= 0 && (intersection.Y - c.Y) * (intersection.Y - d.Y) <= 0)
            {
               // Debug.Print("线段相交于点(" + intersection.X + "," + intersection.Y + ")！");
                return 1; //'相交
            }
            else
            {
                //Debug.Print("线段相交于虚交点(" + intersection.X + "," + intersection.Y + ")！");
                return -1; //'相交但不在线段上
            }
        }
        /// <summary>
        /// 判断多边形的相交
        /// </summary>
        /// <param name="P">判断的点</param>
        /// <param name="poly">多边形</param>
        /// <returns>返回是否在内部</returns>
        //public List<PointF> insertpoly(Polyon box,List<PointF> maxvoron)
        //{ 

        //}
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

        public static bool PtInPolygon(PointF p, List<PointF> poly)
        {
            int nCross = 0;
            //PointF[] pnts = getPolygonPoints(poly);
            for (int i = 0; i < poly.Count; i++)
            {
                PointF p1 = poly[i];
                PointF p2 =poly[(i + 1) % poly.Count];
                // 求解 y=p.y 与 p1p2 的交点
                if (p1.Y == p2.Y) // p1p2 与 y=p0.y平行 
                    continue;
                if (p.Y < Math.Min(p1.Y, p2.Y)) // 交点在p1p2延长线上 
                    continue;
                if (p.Y >= Math.Max(p1.Y, p2.Y)) // 交点在p1p2延长线上 
                    continue;
                // 求交点的 X 坐标 -------------------------------------------------------------- 
                double x = (double)(p.Y - p1.Y) * (double)(p2.X - p1.X) / (double)(p2.Y - p1.Y) + p1.X;
                if (x >= p.X)
                    nCross++; // 只统计单边交点 
            }
            // 单边交点为偶数，点在多边形之外 --- 
            return (nCross % 2 == 1);
        }

        /// <summary>
        /// 判断点是否在多边形内
        /// </summary>
        /// <param name="P">判断的点</param>
        /// <param name="poly">多边形</param>
        /// <returns>返回是否在内部</returns>

        public static bool PtInPolygon(PointF p, Collection<PointF> poly)
        {
            int nCross = 0;
            //PointF[] pnts = getPolygonPoints(poly);
            for (int i = 0; i < poly.Count; i++)
            {
                PointF p1 = poly[i];
                PointF p2 = poly[(i + 1) % poly.Count];
                // 求解 y=p.y 与 p1p2 的交点
                if (p1.Y == p2.Y) // p1p2 与 y=p0.y平行 
                    continue;
                if (p.Y < Math.Min(p1.Y, p2.Y)) // 交点在p1p2延长线上 
                    continue;
                if (p.Y >= Math.Max(p1.Y, p2.Y)) // 交点在p1p2延长线上 
                    continue;
                // 求交点的 X 坐标 -------------------------------------------------------------- 
                double x = (double)(p.Y - p1.Y) * (double)(p2.X - p1.X) / (double)(p2.Y - p1.Y) + p1.X;
                if (x >= p.X)
                    nCross++; // 只统计单边交点 
            }
            // 单边交点为偶数，点在多边形之外 --- 
            return (nCross % 2 == 1);
        }
        /// <summary>
        /// 获得多边形和凸壳泰森多边形的交集
        /// </summary>
        /// <param name="multrect">外包多边形</param>
        /// <param name="tais">凸壳的泰森多边形</param>
       
       public void insectpoloy(Polyon multrect,voronoiboundary tais)
       {
           int m = multrect.Vertices.Count; int n = tais.voronicollect.Count;
           //为两个多边形的交点做一个在另一个多边形的标记 初始为零 如果被访问在内部则为1
           int[] rectpointflag=new int[m];
           int[] taispointflag=new int[n];
           for (int i=0;i<m;i++)
           {
               rectpointflag[i]=0;
           }
           for (int j=0;j<n;j++)
           {
               taispointflag[j]=0;
           }
           //如果相交则将其相交的线路编号的位置为1 及其线路的第一个节点的编号顺序
           int[,] insectflag=new int[n,m];
           PointF[,] insectpoint=new PointF[n,m];
           for (int i=0;i<n;i++)
           {
               for (int j=0;j<m;j++)
               {
                   insectflag[i,j]=0;
                   insectpoint[i,j] = new PointF();
               }
         
           }
           //将其相交的线段用标记记录
           for (int i = 0; i < n;i++ )
           {
               PointF p1 = tais.voronicollect[i];
               PointF p2 = tais.voronicollect[(i + 1) % n];
               for (int j = 0; j < m;j++ )
               {
                   PointF p=new PointF();
                   PointF p3 = multrect.Vertices[j];
                   PointF p4 = multrect.Vertices[(j + 1) % multrect.Vertices.Count];
                   if (GetIntersection(p1, p2, p3, p4, ref p) == 1)
                   {
                       insectflag[i,j] = 1;
                       insectpoint[i,j] = p;
                   }
               }
           }
           //进行交集拐点的搜索
           int starti = 0;
          sosocontinue:
           Sospoloy(starti, true, rectpointflag, taispointflag, insectflag, insectpoint, tais, multrect);
           //遍历相交的节点是否完全找完
           int resault = 0;
           for (int i = 0; i < n;i++ )
           {
               for (int j = 0; j < m;j++ )
               {
                   if (insectflag[i, j]==1)
                   {
                       starti=i;
                       goto sosocontinue;
                   }
                   
               }
           }
          //将其求得的交集存入 泰森多边形集合中
           tais.voronicollect.Clear();
           for (int i = 0; i < tais.vimultcollect.Count;i++ )
           {
               tais.voronicollect.Add(tais.vimultcollect[i]);
           }
           
           //for (int i = 0; i < n;i++ )
           //{
           //    taispointflag[i] = 1;    //说明是在内部
           //    if (PtInPolygon(tais.voronicollect[i],multrect.Vertices))
           //    {
           //         tais.insertboundarycollect.Add(tais.voronicollect[i]);
                  
           //    }
           //    for (int j=0;j<m;j++)
           //    {
           //        if (insectflag[i,j]==1)
           //        {
           //            insectflag[i,j] = 0;
           //            tais.insertboundarycollect.Add(insectpoint[i,j]);
           //            if (PtInPolygon(multrect.Vertices[j],tais.voronicollect))
           //            {
           //                tais.insertboundarycollect.Add(multrect.Vertices[j]);

           //            }
           //            if (PtInPolygon(multrect.Vertices[(j + 1) % multrect.Vertices.Count], tais.voronicollect))
           //            {
           //                tais.insertboundarycollect.Add(multrect.Vertices[(j + 1) % multrect.Vertices.Count]);
           //            }
           //        }
                  
           //    }
               
           //}
       
       }
      //搜索外围多边形
       /// <summary>
       ///搜索外围多边形
       /// </summary>
       /// <param name="starti">开始搜索节点编号</param>
       /// <param name="store">排列顺序</param>
       /// <param name="Sosflag">被搜索多边形的搜索标记</param>
       /// <param name="insectflag">相交标记数组 相交为1</param>
       ///  <param name="insectpoint">相交点坐标记录数组</param>
       ///  <param name="polyon">搜索的外围多变形</param>
       ///  <param name="vectcollect">与其相交的泰森多边形</param>
        public void Sospoloy(int starti, bool store, int[] Sosflag,int[] taisflag, int[,] insectflag, PointF[,] insectpoint, Polyon multrect, voronoiboundary tais)
        {
            int m = multrect.Vertices.Count; int n = tais.voronicollect.Count;
            if(store)
            {
                for(int j=starti;j<m;j++)
                {
                    if (Sosflag[j]==1)
                    {
                        return;
                    }
                    else
                    {
                        Sosflag[j] = 1;
                        if (PtInPolygon(multrect.Vertices[j], tais.voronicollect))
                        {
                            if (tais.Vimultptcompare(multrect.Vertices[j]))
                            {
                                tais.vimultcollect.Add(multrect.Vertices[j]);
                            }
                           
                        }
                        for (int i = 0; i < n;i++ )
                        {
                            if (insectflag[i,j] == 1)
                            {
                                insectflag[i,j] = 0;
                                //判断相交的点是否就是端点
                                if (tais.Vimultptcompare(insectpoint[i, j]))
                                {
                                    tais.vimultcollect.Add(insectpoint[i, j]);
                                }
                               
                                if (PtInPolygon(tais.voronicollect[i], multrect.Vertices))
                                {
                                    if (tais.Vimultptcompare(tais.voronicollect[i]))
                                    {
                                        tais.vimultcollect.Add(tais.voronicollect[i]);
                                    }
                                    
                                    //插入另一循环的函数 首先要判断此多边形的顺如果是逆时针如果小号在里面则从小到大的顺序 如果大号在里面则是从大到小的顺序
                                    if (i < (i + 1) % n)
                                   {
                                       if (!IsCCW(tais.voronicollect))
                                       {
                                           Sospoloy(i, false, Sosflag, taisflag, insectflag, insectpoint, tais, multrect);
                                       }
                                       else
                                           Sospoloy(i, true, Sosflag, taisflag, insectflag, insectpoint, tais, multrect);
                                   }
                                    else
                                    {
                                        if (!IsCCW(tais.voronicollect))
                                        {
                                            Sospoloy(i, true, Sosflag, taisflag, insectflag, insectpoint, tais, multrect);
                                        }
                                        else
                                            Sospoloy(i, false, Sosflag, taisflag, insectflag, insectpoint, tais, multrect);
                                    }
                                   

                                }
                                if (PtInPolygon(tais.voronicollect[(i + 1) % n], multrect.Vertices))
                                {
                                    if (tais.Vimultptcompare(tais.voronicollect[(i + 1) % n]))
                                    {
                                        tais.vimultcollect.Add(tais.voronicollect[(i + 1) % n]);
                                    }
                                    
                                    //插入另一循环的函数
                                    if (i < (i + 1) % n)
                                    {
                                        if (!IsCCW(tais.voronicollect))
                                        {
                                            Sospoloy((i + 1) % n, true, Sosflag, taisflag, insectflag, insectpoint, tais, multrect);
                                        }
                                        else
                                            Sospoloy((i + 1) % n, false, Sosflag, taisflag, insectflag, insectpoint, tais, multrect);
                                    }
                                    else
                                    {
                                        if (!IsCCW(tais.voronicollect))
                                        {
                                            Sospoloy((i + 1) % n, false, Sosflag, taisflag, insectflag, insectpoint, tais, multrect);
                                        }
                                        else
                                            Sospoloy((i + 1) % n,true, Sosflag, taisflag, insectflag, insectpoint, tais, multrect);
                                    }
                                   
                                }
                            }
                        }
                    }

                }
            }
            else
            {
                for (int j = starti+m; j >0 ; j--)
                {
                    if (Sosflag[j%m] == 1)
                    {
                        return;
                    }
                    else
                    {
                        Sosflag[j % m] = 1;
                        if (PtInPolygon(multrect.Vertices[j % m], tais.voronicollect))
                        {
                            if (tais.Vimultptcompare(multrect.Vertices[j % m]))
                            {
                                tais.vimultcollect.Add(multrect.Vertices[j % m]);
                            }
                            
                        }
                        for (int i = 0; i < n; i++)
                        {
                            if (insectflag[i,j % m] == 1)
                            {
                                insectflag[i,j % m] = 0;
                                if (tais.Vimultptcompare(insectpoint[i, j % m]))
                                {
                                    tais.vimultcollect.Add(insectpoint[i, j % m]);
                                }
                                
                                if (PtInPolygon(tais.voronicollect[i], multrect.Vertices))
                                {
                                    if (tais.Vimultptcompare(tais.voronicollect[i]))
                                    {
                                        tais.vimultcollect.Add(tais.voronicollect[i]);
                                    }
                                    
                                    //插入另一循环的函数
                                    if (i < (i + 1) % n)
                                    {
                                        if (!IsCCW(tais.voronicollect))
                                        {
                                            Sospoloy(i, true, Sosflag, taisflag, insectflag, insectpoint, tais, multrect);
                                        }
                                        else
                                            Sospoloy(i, false, Sosflag, taisflag, insectflag, insectpoint, tais, multrect);
                                    }
                                   else
                                    {
                                        if (!IsCCW(tais.voronicollect))
                                        {
                                            Sospoloy(i, false, Sosflag, taisflag, insectflag, insectpoint, tais, multrect);
                                        }
                                        else
                                            Sospoloy(i, true, Sosflag, taisflag, insectflag, insectpoint, tais, multrect);
                                    }
                                 
                                }
                                if (PtInPolygon(tais.voronicollect[(i + 1) % n], multrect.Vertices))
                                {
                                    if (tais.Vimultptcompare(tais.voronicollect[(i + 1) % n]))
                                    {
                                        tais.vimultcollect.Add(tais.voronicollect[(i + 1) % n]);
                                    }
                                   
                                    //插入另一循环的函数
                                    if (i < (i + 1) % n)
                                    {
                                        if (!IsCCW(tais.voronicollect))
                                        {
                                            Sospoloy((i + 1) % n, true, Sosflag, taisflag, insectflag, insectpoint, tais, multrect);
                                        }
                                        else
                                            Sospoloy((i + 1) % n, false, Sosflag, taisflag, insectflag, insectpoint, tais, multrect);
                                    }
                                    else
                                    {
                                        if (!IsCCW(tais.voronicollect))
                                        {
                                            Sospoloy((i + 1) % n, false, Sosflag, taisflag, insectflag, insectpoint, tais, multrect);
                                        }
                                        else
                                            Sospoloy((i + 1) % n, true, Sosflag, taisflag, insectflag, insectpoint, tais, multrect);
                                    }
                                   
                                }
                            }
                        }
                    }

                }
            }
        }
    //搜索泰森多边形
        /// <summary>
        ///搜索泰森多边形
        /// </summary>
        /// <param name="starti">开始搜索节点编号</param>
        /// <param name="store">排列顺序</param>
        /// <param name="Sosflag">被搜索多边形的搜索标记</param>
        /// <param name="insectflag">相交标记数组 相交为1</param>
        ///  <param name="insectpoint">相交点坐标记录数组</param>
        ///  <param name="polyon">搜索的外围多变形</param>
        ///  <param name="vectcollect">与其相交的泰森多边形</param>
        public void Sospoloy(int starti, bool store, int[] Sosflag, int[] taisflag,int[,] insectflag, PointF[,] insectpoint, voronoiboundary tais,Polyon multrect )
        {
            int m = multrect.Vertices.Count; int n = tais.voronicollect.Count;
            if (store)
            {
                for (int i = starti; i< n; i++)
                {
                    if (taisflag[i] == 1)
                    {
                        return;
                    }
                    else
                    {
                        taisflag[i] = 1;
                        if (PtInPolygon(tais.voronicollect[i], multrect.Vertices))
                        {
                            if (tais.Vimultptcompare(tais.voronicollect[i]))
                            {
                                tais.vimultcollect.Add(tais.voronicollect[i]);
                            }
                            
                        }
                        for (int j = 0; j < m; j++)
                        {
                            if (insectflag[i,j] == 1)
                            {
                                insectflag[i,j] = 0;
                                if (tais.Vimultptcompare(insectpoint[i, j]))
                                {
                                    tais.vimultcollect.Add(insectpoint[i, j]);
                                }
                                
                                if (PtInPolygon(multrect.Vertices[j], tais.voronicollect))
                                {
                                    if (tais.Vimultptcompare(multrect.Vertices[j]))
                                    {
                                        tais.vimultcollect.Add(multrect.Vertices[j]);
                                    }
                                    
                                    //插入另一多边形的循环 首先要判断此多边形是什么顺序
                                    if (j < (j + 1) % m)
                                    {
                                        if (!IsCCW(multrect.Vertices))
                                        {
                                            Sospoloy(j, false, Sosflag, taisflag, insectflag, insectpoint, multrect, tais);
                                        }
                                        else
                                            Sospoloy(j, true, Sosflag, taisflag, insectflag, insectpoint, multrect, tais);
                                    }
                                    else
                                    {
                                        if (!IsCCW(multrect.Vertices))
                                        {
                                            Sospoloy(j, true, Sosflag, taisflag, insectflag, insectpoint, multrect, tais);
                                        }
                                        else
                                            Sospoloy(j, false, Sosflag, taisflag, insectflag, insectpoint, multrect, tais);
                                    }
                                    



                                }
                                if (PtInPolygon(multrect.Vertices[(j + 1) % m], tais.voronicollect))
                                {
                                    if (tais.Vimultptcompare(multrect.Vertices[(j + 1) % m]))
                                    {
                                        tais.vimultcollect.Add(multrect.Vertices[(j + 1) % m]);
                                    }
                                    
                                    //插入另一多边形的循环
                                    if (j < (j + 1) % m)
                                    {
                                        if (!IsCCW(multrect.Vertices))
                                        {
                                            Sospoloy((j + 1) % m, true, Sosflag, taisflag, insectflag, insectpoint, multrect, tais);
                                        }
                                        else
                                            Sospoloy((j + 1) % m, false, Sosflag, taisflag, insectflag, insectpoint, multrect, tais);
                                    }
                                    else
                                    {
                                        if (!IsCCW(multrect.Vertices))
                                        {
                                            Sospoloy((j + 1) % m, false, Sosflag, taisflag, insectflag, insectpoint, multrect, tais);
                                        }
                                        else
                                            Sospoloy((j + 1) % m, true, Sosflag, taisflag, insectflag, insectpoint, multrect, tais);
                                    }
                                   
                                }
                            }
                        }
                    }

                }
            }
            else
            {
                for (int i= starti + n; i > 0; i--)
                {
                    if (taisflag[i % n] == 1)
                    {
                        return;
                    }
                    else
                    {
                       taisflag[i% n] = 1;
                        if (PtInPolygon(tais.voronicollect[i % n], multrect.Vertices))
                        {
                            if (tais.Vimultptcompare(tais.voronicollect[i % n]))
                            {
                                tais.vimultcollect.Add(tais.voronicollect[i % n]);
                            }
                            
                        }
                        for (int j = 0; j < m; j++)
                        {
                            if (insectflag[i%n,j] == 1)
                            {
                                insectflag[i % n,j] = 0;
                                if (tais.Vimultptcompare(insectpoint[i % n, j]))
                                {
                                    tais.vimultcollect.Add(insectpoint[i % n, j]);
                                }
                               
                                if (PtInPolygon(multrect.Vertices[j], tais.voronicollect))
                                {
                                    if (tais.Vimultptcompare(multrect.Vertices[j]))
                                    {
                                        tais.vimultcollect.Add(multrect.Vertices[j]);
                                    }
                                   
                                    //插入另一面的循环
                                    if (j < (j + 1) % m)
                                    {
                                        if (!IsCCW(multrect.Vertices))
                                        {
                                            Sospoloy(j, false, Sosflag, taisflag, insectflag, insectpoint, multrect, tais);
                                        }
                                        else
                                            Sospoloy(j, true, Sosflag, taisflag, insectflag, insectpoint, multrect, tais);
                                    }
                                   
                                    else
                                    {
                                        if (!IsCCW(multrect.Vertices))
                                        {
                                            Sospoloy(j, true, Sosflag, taisflag, insectflag, insectpoint, multrect, tais);
                                        }
                                        else
                                            Sospoloy(j,false, Sosflag, taisflag, insectflag, insectpoint, multrect, tais);
                                    }
                                }
                                if (PtInPolygon(multrect.Vertices[(j + 1) % m], tais.voronicollect))
                                {
                                    if (tais.Vimultptcompare(multrect.Vertices[(j + 1) % m]))
                                    {
                                        tais.vimultcollect.Add(multrect.Vertices[(j + 1) % m]);
                                    }
                                   
                                    //插入要循环的数据
                                    if (j < (j + 1) % m)
                                    {
                                        if (!IsCCW(multrect.Vertices))
                                        {
                                            Sospoloy((j + 1) % m, true, Sosflag, taisflag, insectflag, insectpoint, multrect, tais);
                                        }
                                        else
                                            Sospoloy((j + 1) % m, false, Sosflag, taisflag, insectflag, insectpoint, multrect, tais);
                                    }
                                   else
                                    {
                                        if (!IsCCW(multrect.Vertices))
                                        {
                                            Sospoloy((j + 1) % m, false, Sosflag, taisflag, insectflag, insectpoint, multrect, tais);
                                        }
                                        else
                                            Sospoloy((j + 1) % m, true, Sosflag, taisflag, insectflag, insectpoint, multrect, tais);
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }

        /// <summary>
        /// Tests whether a ring is oriented counter-clockwise.
        /// </summary>
        /// <param name="ring">Ring to test.</param>
        /// <returns>Returns true if ring is oriented clockwise.（顺时针）</returns>
        public static bool IsCCW(Collection<PointF> ring)
        {
            PointF PrevPoint, NextPoint;
            PointF p;

            // Check if the ring has enough vertices to be a ring
            if (ring.Count < 3) throw (new ArgumentException("Invalid LinearRing"));

            // find the point with the largest Y coordinate
            PointF hip = ring[0];
            int hii = 0;
            for (int i = 1; i < ring.Count; i++)
            {
                p = ring[i];
                if (p.Y > hip.Y)
                {
                    hip = p;
                    hii = i;
                }
            }
            // Point left to Hip
            int iPrev = hii - 1;
            if (iPrev < 0) iPrev = ring.Count - 2;
            // Point right to Hip
            int iNext = hii + 1;
            if (iNext >= ring.Count) iNext = 1;
            PrevPoint = ring[iPrev];
            NextPoint = ring[iNext];

            // translate so that hip is at the origin.
            // This will not affect the area calculation, and will avoid
            // finite-accuracy errors (i.e very small vectors with very large coordinates)
            // This also simplifies the discriminant calculation.
            double prev2x = PrevPoint.X - hip.X;
            double prev2y = PrevPoint.Y - hip.Y;
            double next2x = NextPoint.X - hip.X;
            double next2y = NextPoint.Y - hip.Y;
            // compute cross-product of vectors hip->next and hip->prev
            // (e.g. area of parallelogram they enclose)
            double disc = next2x * prev2y - next2y * prev2x;
            // If disc is exactly 0, lines are collinear.  There are two possible cases:
            //	(1) the lines lie along the x axis in opposite directions
            //	(2) the line lie on top of one another
            //  (2) should never happen, so we're going to ignore it!
            //	(Might want to assert this)
            //  (1) is handled by checking if next is left of prev ==> CCW

            if (disc == 0.0)
            {
                // poly is CCW if prev x is right of next x
                return (PrevPoint.X > NextPoint.X);
            }
            else
            {
                // if area is positive, points are ordered CCW
                return (disc > 0.0);
            }
        }

        /// <summary>
        /// Tests whether a ring is oriented counter-clockwise（逆时针）.
        /// </summary>
        /// <param name="ring">Ring to test.</param>
        /// <returns>Returns true if ring is oriented clockwise（顺时针）.</returns>
        public static bool IsCCW(List<PointF> ring)
        {
            PointF PrevPoint, NextPoint;
            PointF p;

            // Check if the ring has enough vertices to be a ring
            if (ring.Count < 3) throw (new ArgumentException("Invalid LinearRing"));

            // find the point with the largest Y coordinate
            PointF hip = ring[0];
            int hii = 0;
            for (int i = 1; i < ring.Count; i++)
            {
                p = ring[i];
                if (p.Y > hip.Y)
                {
                    hip = p;
                    hii = i;
                }
            }
            // Point left to Hip
            int iPrev = hii - 1;
            if (iPrev < 0) iPrev = ring.Count - 2;
            // Point right to Hip
            int iNext = hii + 1;
            if (iNext >= ring.Count) iNext = 1;
            PrevPoint = ring[iPrev];
            NextPoint = ring[iNext];

            // translate so that hip is at the origin.
            // This will not affect the area calculation, and will avoid
            // finite-accuracy errors (i.e very small vectors with very large coordinates)
            // This also simplifies the discriminant calculation.
            double prev2x = PrevPoint.X - hip.X;
            double prev2y = PrevPoint.Y - hip.Y;
            double next2x = NextPoint.X - hip.X;
            double next2y = NextPoint.Y - hip.Y;
            // compute cross-product of vectors hip->next and hip->prev
            // (e.g. area of parallelogram they enclose)
            double disc = next2x * prev2y - next2y * prev2x;
            // If disc is exactly 0, lines are collinear.  There are two possible cases:
            //	(1) the lines lie along the x axis in opposite directions
            //	(2) the line lie on top of one another
            //  (2) should never happen, so we're going to ignore it!
            //	(Might want to assert this)
            //  (1) is handled by checking if next is left of prev ==> CCW

            if (disc == 0.0)
            {
                // poly is CCW if prev x is right of next x
                return (PrevPoint.X > NextPoint.X);
            }
            else
            {
                // if area is positive, points are ordered CCW
                return (disc > 0.0);
            }
        } 
        //判断跨越多边形线段与多边形的交点 如果是完全包括则返回a 和b
       /// <summary>
       /// 判断跨越多边形线段与多边形的交点 如果是完全包括则返回a 和b
       /// </summary>
       /// <param name="a">线段首节点</param>
       /// <param name="b">线段末节点</param>
       /// <param name="rec">外围多边形</param>
        public List<PointF> Lineinsectpolygon(PointF a, PointF b, Polyon rect)
        {
            List<PointF> pcol = new List<PointF>();
            PointF p=new PointF();
            for (int i = 0; i < rect.Vertices.Count; i++)
            {
                PointF p1 = rect.Vertices[i];
                PointF p2 = rect.Vertices[(i + 1) % rect.Vertices.Count];
                if (GetIntersection(a,b,p1,p2,ref p)==1)
                {
                    pcol.Add(p);
                    break;
                }
                else if(GetIntersection(a,b,p1,p2,ref p)==2)
                {
                    pcol.Add(a);
                    pcol.Add(b);
                    break;
                }
            }
            return pcol;
        }
        

    }

}


