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
        public DataStruct DS = new DataStruct();  //���ݽṹ

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
        //����Voronoiͼ
        /// <summary>
        ///����Voronoiͼ
        /// </summary>

        public void CreateVoronoi()
        {
            //���voroniͼ�е����е�
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
                if (!DS.TinEdges[i].NotHullEdge) //����Ϊ͹�Ǳ�
                {
                    DrawHullVorEdge(i, v1, v2);
                    continue;
                }

                //������/�ҡ������Բ��
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
                //Բ����box����ֱ������
                //if (PointInBox(p1) && PointInBox(p2))
                // g.DrawLine(new Pen(Color.Blue, 2), p1, p2);
                //else   //����βü�
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
        ///����Voronoiͼ
        /// </summary>
        /// <param name="g">����</param>
       
        public void CreateVoronoi(Graphics g)
        {
            //���voroniͼ�е����е�
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
                if (!DS.TinEdges[i].NotHullEdge) //����Ϊ͹�Ǳ�
                {
                    DrawHullVorEdge(i, v1, v2, g);
                    continue;
                }

                //������/�ҡ������Բ��
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
                //Բ����box����ֱ������
                //if (PointInBox(p1) && PointInBox(p2))
                g.DrawLine(new Pen(Color.Blue, 2), p1, p2);
                //else   //����βü�
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
        //��ø��������������Ӱ�췶Χ
        /// <summary>
        ///��ø��������������Ӱ�췶Χ
        /// </summary>
      
        public void GetVoronoi()
        {
            for (int i = 0; i < DS.VerticesNum; i++)
            {
                if (DS.Vertex[i].isHullEdge != 0) //����Ϊ͹�Ǳ�
                {
                    getHullVorEdge(i);
                    continue;
                }

                //������/�ҡ������Բ��
                long index1 = DS.TinEdges[i].AdjTriangle1ID;
                long index2 = DS.TinEdges[i].AdjTriangle2ID;
                PointF p1 = new PointF(Convert.ToSingle(DS.Barycenters[index1].X), Convert.ToSingle(DS.Barycenters[index1].Y));
                PointF p2 = new PointF(Convert.ToSingle(DS.Barycenters[index2].X), Convert.ToSingle(DS.Barycenters[index2].Y));
                //Բ����box����ֱ������
                //if (PointInBox(p1) && PointInBox(p2))
                //  g.DrawLine(new Pen(Color.Blue, 2), p1, p2);
                //else   //����βü�
                //    In
            }
        }
        //���͹��voronoi
        public void getHullVorEdge(int i)
        {

        }

        //����������Delaunay������
        /// <summary>
        ///����������Delaunay������
        /// </summary>
        public void CreateTIN()
        {
            //����͹�ǲ������ʷ�
            CreateConvex();
            HullTriangulation();

            //������
            PlugInEveryVertex();

            //�����ߵ����˽ṹ
            TopologizeEdge();
        }

        //�������޸�TIN
        /// <summary>
        ///�������޸�TIN
        /// </summary>
        private void PlugInEveryVertex()
        {
            Edge[] EdgesBuf = new Edge[DataStruct.MaxTriangles];  //���߻�����

            bool IsInCircle;
            int i, j, k;
            int EdgeCount;
            for (i = 0; i < DS.VerticesNum; i++)    //������
            {
                //����͹�Ƕ���
                if (DS.Vertex[i].isHullEdge != 0)
                    continue;

                EdgeCount = 0;
                for (j = 0; j < DS.TriangleNum; j++) //��λ�������Ӱ������С�
                {
                    IsInCircle = InTriangleExtCircle(DS.Vertex[i].x, DS.Vertex[i].y, DS.Vertex[DS.Triangle[j].V1Index].x, DS.Vertex[DS.Triangle[j].V1Index].y,
                        DS.Vertex[DS.Triangle[j].V2Index].x, DS.Vertex[DS.Triangle[j].V2Index].y,
                        DS.Vertex[DS.Triangle[j].V3Index].x, DS.Vertex[DS.Triangle[j].V3Index].y);
                    if (IsInCircle)    //��j��Ӱ�췶Χ��
                    {
                        Edge[] eee ={new Edge(DS.Triangle[j].V1Index, DS.Triangle[j].V2Index),
                            new Edge(DS.Triangle[j].V2Index, DS.Triangle[j].V3Index),
                            new Edge(DS.Triangle[j].V3Index, DS.Triangle[j].V1Index)};  //��������

                        #region �洢����������ġ���
                        bool IsNotComnEdge;
                        for (k = 0; k < 3; k++)
                        {
                            IsNotComnEdge = true;
                            for (int n = 0; n < EdgeCount; n++)
                            {
                                if (Edge.Compare(eee[k], EdgesBuf[n]))   //�˱�Ϊ������
                                {
                                    //ɾ���ѻ���Ĺ�����
                                    IsNotComnEdge = false;
                                    EdgesBuf[n] = EdgesBuf[EdgeCount - 1];
                                    EdgeCount--;
                                    break;
                                }
                            }

                            if (IsNotComnEdge)
                            {
                                EdgesBuf[EdgeCount] = eee[k];    //�߼���Buffer
                                EdgeCount++;
                            }
                        }
                        #endregion

                        //ɾ����j, ��β��ǰ�Ʋ���
                        DS.Triangle[j].V1Index = DS.Triangle[DS.TriangleNum - 1].V1Index;
                        DS.Triangle[j].V2Index = DS.Triangle[DS.TriangleNum - 1].V2Index;
                        DS.Triangle[j].V3Index = DS.Triangle[DS.TriangleNum - 1].V3Index;
                        j--;
                        DS.TriangleNum--;
                    }
                }//for ��λ��

                #region �����¡�
                for (j = 0; j < EdgeCount; j++)
                {
                    DS.Triangle[DS.TriangleNum].V1Index = EdgesBuf[j].Vertex1ID;
                    DS.Triangle[DS.TriangleNum].V2Index = EdgesBuf[j].Vertex2ID;
                    DS.Triangle[DS.TriangleNum].V3Index = i;
                    DS.TriangleNum++;
                }
                #endregion
            }//������for
        }

        //�������ԲԲ��
        /// <summary>
        ///�������ԲԲ��
        /// </summary>
        public void CalculateBC()
        {
            double x1, y1, x2, y2, x3, y3;
            for (int i = 0; i < DS.TriangleNum; i++)
            {
                //����������Բ��
                x1 = DS.Vertex[DS.Triangle[i].V1Index].x;
                y1 = DS.Vertex[DS.Triangle[i].V1Index].y;
                x2 = DS.Vertex[DS.Triangle[i].V2Index].x;
                y2 = DS.Vertex[DS.Triangle[i].V2Index].y;
                x3 = DS.Vertex[DS.Triangle[i].V3Index].x;
                y3 = DS.Vertex[DS.Triangle[i].V3Index].y;
                GetTriangleBarycnt(x1, y1, x2, y2, x3, y3, ref DS.Barycenters[i].X, ref DS.Barycenters[i].Y);
            }

        }

        //��������Բ��
        /// <summary>
        ///��������Բ��
        /// </summary>
        private void GetTriangleBarycnt(double x1, double y1, double x2, double y2, double x3, double y3, ref double bcX, ref double bcY)
        {
            double precision = 0.000001;
            double k1, k2;   //�����д���б��

            //���㹲��
            if (Math.Abs(y1 - y2) < precision && Math.Abs(y2 - y3) < precision)
            {
                MessageBox.Show("Three Points on one line!");
                Application.Exit();
            }

            //�ߵ��е�
            double MidX1 = (x1 + x2) / 2;
            double MidY1 = (y1 + y2) / 2;
            double MidX2 = (x3 + x2) / 2;
            double MidY2 = (y3 + y2) / 2;

            if (Math.Abs(y2 - y1) < precision)  //p1p2ƽ����X��
            {
                k2 = -(x3 - x2) / (y3 - y2);
                bcX = MidX1;
                bcY = k2 * (bcX - MidX2) + MidY2;
            }
            else if (Math.Abs(y3 - y2) < precision)   //p2p3ƽ����X��
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

        //�жϵ��Ƿ��ڡ������Բ��
        /// <summary>
        ///�жϵ��Ƿ��ڡ������Բ��
        /// </summary>
        private Boolean InTriangleExtCircle(double xp, double yp, double x1, double y1, double x2, double y2, double x3, double y3)
        {
            double RadiusSquare;    //�뾶��ƽ��
            double DisSquare;  //�����ƽ��
            double BaryCntX = 0, BaryCntY = 0;
            GetTriangleBarycnt(x1, y1, x2, y2, x3, y3, ref  BaryCntX, ref BaryCntY);

            RadiusSquare = (x1 - BaryCntX) * (x1 - BaryCntX) + (y1 - BaryCntY) * (y1 - BaryCntY);
            DisSquare = (xp - BaryCntX) * (xp - BaryCntX) + (yp - BaryCntY) * (yp - BaryCntY);

            if (DisSquare <= RadiusSquare)
                return true;
            else
                return false;
        }

        //����Edge�����˹�ϵ
        /// <summary>
        ///����Edge�����˹�ϵ
        /// </summary>
        public void TopologizeEdge()
        {
            DS.TinEdgeNum = 0;
            DS.TinEdges = new Edge[DataStruct.MaxEdges];   //���������
            long[] Vindex = new long[3]; //3����������

            //����ÿ������������
            for (int i = 0; i < DS.TriangleNum; i++)
            {
                Vindex[0] = DS.Triangle[i].V1Index;
                Vindex[1] = DS.Triangle[i].V2Index;
                Vindex[2] = DS.Triangle[i].V3Index;

                for (int j = 0; j < 3; j++)   //ÿ����
                {
                    Edge e = new Edge(Vindex[j], Vindex[(j + 1) % 3]);

                    //�жϱ����������Ƿ��Ѵ���
                    int k;
                    for (k = 0; k < DS.TinEdgeNum; k++)
                    {
                        if (Edge.Compare(e, DS.TinEdges[k]))   //�˱��ѹ���
                        {
                            DS.TinEdges[k].AdjTriangle2ID = i;
                            DS.TinEdges[k].NotHullEdge = true;
                            break;
                        }
                    }//for

                    if (k == DS.TinEdgeNum)   //�˱�Ϊ�±�
                    {
                        DS.TinEdges[DS.TinEdgeNum].Vertex1ID = e.Vertex1ID;
                        DS.TinEdges[DS.TinEdgeNum].Vertex2ID = e.Vertex2ID;
                        DS.TinEdges[DS.TinEdgeNum].AdjTriangle1ID = i;
                        DS.TinEdges[DS.TinEdgeNum].AdjacentT1V3 = Vindex[(j + 2) % 3];
                        DS.TinEdgeNum++;
                    }

                }//for,ÿ����
            }//for,ÿ����
        }
        //�����е����ĵ�����voronoiboundary
        /// <summary>
        ///�����е����ĵ�����voronoiboundary
        /// </summary>
        private void DrawHullVorEdge(int i, voronoiboundary v1, voronoiboundary v2)
        {
            PointF pnt1 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex1ID].x),
                 Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex1ID].y));
            PointF pnt2 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex2ID].x),
                Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex2ID].y));
            PointF pnt3 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[i].AdjacentT1V3].x),
                Convert.ToSingle(DS.Vertex[DS.TinEdges[i].AdjacentT1V3].y));    //�߶�Ӧ�ġ�����

            PointF MidPnt = new PointF((pnt1.X + pnt2.X) / 2, (pnt2.Y + pnt2.Y) / 2);  //TinEdge�е�
            PointF BaryCnt = new PointF(Convert.ToSingle(DS.Barycenters[DS.TinEdges[i].AdjTriangle1ID].X),
                Convert.ToSingle(DS.Barycenters[DS.TinEdges[i].AdjTriangle1ID].Y));  //���Բ��
            PointF EndPnt = new PointF();   //Բ�������ڴ˵㹹��VEdge

            //Բ����box����ֱ������
            //if (!(BaryCnt.X >= DS.BBOX.XLeft && BaryCnt.X <= DS.BBOX.XRight &&
            //    BaryCnt.Y >= DS.BBOX.YTop && BaryCnt.Y <= DS.BBOX.YBottom))
            //    return;

            //��б��
            float k = 0;  //б��
            bool KExist = true;
            if (Math.Abs(pnt1.Y - pnt2.Y) < 0.000001)
                KExist = false;     //k������
            else
                k = (pnt1.X - pnt2.X) / (pnt2.Y - pnt1.Y);

            //��͹�Ǳ��ǡ��Ķ۽Ǳ������Բ���ڡ���
            bool obtEdge = IsObtuseEdge(i);

            #region ���ݡ�Բ����͹���ڻ���������VEdge
            //Բ���ڱ������������죬����������

            if (!obtEdge)   //Բ����͹����(��߽���)/////////////////////////////////
            {
                if (!KExist)    //k������
                {
                    // MessageBox.Show("б�ʲ����ڵġ�-"+DS.TinEdges[i].AdjTriangle1ID.ToString());
                    if (BaryCnt.Y > MidPnt.Y || BaryCnt.Y < pnt3.Y)// BaryCnt<y3 ->Բ�����е��غ�
                        EndPnt.Y = DS.BBOX.YTop;
                    else if (BaryCnt.Y < MidPnt.Y || BaryCnt.Y > pnt3.Y)
                        EndPnt.Y = DS.BBOX.YBottom;

                    EndPnt.X = BaryCnt.X;
                }
                else      //K����
                {
                    if (BaryCnt.X > MidPnt.X || (BaryCnt.X == MidPnt.X && BaryCnt.X < pnt3.X))
                        EndPnt.X = DS.BBOX.XLeft;
                    else if (BaryCnt.X < MidPnt.X || (BaryCnt.X == MidPnt.X && BaryCnt.X > pnt3.X))
                        EndPnt.X = DS.BBOX.XRight;

                    EndPnt.Y = k * (EndPnt.X - BaryCnt.X) + BaryCnt.Y;
                }

            }
            else    //Բ����͹����/////////////////////////////////////////////
            {
                if (!KExist)    //k������
                {
                    if (BaryCnt.Y < MidPnt.Y || BaryCnt.Y < pnt3.Y)
                        EndPnt.Y = DS.BBOX.YTop;
                    else if (BaryCnt.Y > MidPnt.Y || BaryCnt.Y > pnt3.Y)
                        EndPnt.Y = DS.BBOX.YBottom;

                    EndPnt.X = BaryCnt.X;
                }
                else   //K����
                {
                    if (BaryCnt.X < MidPnt.X)
                        EndPnt.X = DS.BBOX.XLeft;
                    else if (BaryCnt.X > MidPnt.X)
                        EndPnt.X = DS.BBOX.XRight;

                    EndPnt.Y = k * (EndPnt.X - BaryCnt.X) + BaryCnt.Y;
                }

            }//else �ڡ���

            //����򽻵��ڱ߽���Ĵ���
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
        //���������ĵ���������
        /// <summary>
        ///���������ĵ��������У�����ʾ����
        /// </summary>
        private void DrawHullVorEdge(int i, voronoiboundary v1, voronoiboundary v2, Graphics g)
        {
            PointF pnt1 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex1ID].x),
                 Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex1ID].y));
            PointF pnt2 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex2ID].x),
                Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex2ID].y));
            PointF pnt3 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[i].AdjacentT1V3].x),
                Convert.ToSingle(DS.Vertex[DS.TinEdges[i].AdjacentT1V3].y));    //�߶�Ӧ�ġ�����

            PointF MidPnt = new PointF((pnt1.X + pnt2.X) / 2, (pnt2.Y + pnt2.Y) / 2);  //TinEdge�е�
            PointF BaryCnt = new PointF(Convert.ToSingle(DS.Barycenters[DS.TinEdges[i].AdjTriangle1ID].X),
                Convert.ToSingle(DS.Barycenters[DS.TinEdges[i].AdjTriangle1ID].Y));  //���Բ��
            PointF EndPnt = new PointF();   //Բ�������ڴ˵㹹��VEdge

            //Բ����box����ֱ������
            if (!(BaryCnt.X >= DS.BBOX.XLeft && BaryCnt.X <= DS.BBOX.XRight &&
                BaryCnt.Y >= DS.BBOX.YTop && BaryCnt.Y <= DS.BBOX.YBottom))
                return;

            //��б��
            float k = 0;  //б��
            bool KExist = true;
            if (Math.Abs(pnt1.Y - pnt2.Y) < 0.000001)
                KExist = false;     //k������
            else
                k = (pnt1.X - pnt2.X) / (pnt2.Y - pnt1.Y);

            //��͹�Ǳ��ǡ��Ķ۽Ǳ������Բ���ڡ���
            bool obtEdge = IsObtuseEdge(i);

            #region ���ݡ�Բ����͹���ڻ���������VEdge
            //Բ���ڱ������������죬����������

            if (!obtEdge)   //Բ����͹����(��߽���)/////////////////////////////////
            {
                if (!KExist)    //k������
                {
                    // MessageBox.Show("б�ʲ����ڵġ�-"+DS.TinEdges[i].AdjTriangle1ID.ToString());
                    if (BaryCnt.Y > MidPnt.Y || BaryCnt.Y < pnt3.Y)// BaryCnt<y3 ->Բ�����е��غ�
                        EndPnt.Y = DS.BBOX.YTop;
                    else if (BaryCnt.Y < MidPnt.Y || BaryCnt.Y > pnt3.Y)
                        EndPnt.Y = DS.BBOX.YBottom;

                    EndPnt.X = BaryCnt.X;
                }
                else      //K����
                {
                    if (BaryCnt.X > MidPnt.X || (BaryCnt.X == MidPnt.X && BaryCnt.X < pnt3.X))
                        EndPnt.X = DS.BBOX.XLeft;
                    else if (BaryCnt.X < MidPnt.X || (BaryCnt.X == MidPnt.X && BaryCnt.X > pnt3.X))
                        EndPnt.X = DS.BBOX.XRight;

                    EndPnt.Y = k * (EndPnt.X - BaryCnt.X) + BaryCnt.Y;
                }

            }
            else    //Բ����͹����/////////////////////////////////////////////
            {
                if (!KExist)    //k������
                {
                    if (BaryCnt.Y < MidPnt.Y || BaryCnt.Y < pnt3.Y)
                        EndPnt.Y = DS.BBOX.YTop;
                    else if (BaryCnt.Y > MidPnt.Y || BaryCnt.Y > pnt3.Y)
                        EndPnt.Y = DS.BBOX.YBottom;

                    EndPnt.X = BaryCnt.X;
                }
                else   //K����
                {
                    if (BaryCnt.X < MidPnt.X)
                        EndPnt.X = DS.BBOX.XLeft;
                    else if (BaryCnt.X > MidPnt.X)
                        EndPnt.X = DS.BBOX.XRight;

                    EndPnt.Y = k * (EndPnt.X - BaryCnt.X) + BaryCnt.Y;
                }

            }//else �ڡ���

            //����򽻵��ڱ߽���Ĵ���
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
        //iΪTinEdge��ID��
        private void DrawHullVorEdge(int i, Graphics g)
        {

            PointF pnt1 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex1ID].x),
                Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex1ID].y));
            PointF pnt2 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex2ID].x),
                Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex2ID].y));
            PointF pnt3 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[i].AdjacentT1V3].x),
                Convert.ToSingle(DS.Vertex[DS.TinEdges[i].AdjacentT1V3].y));    //�߶�Ӧ�ġ�����

            PointF MidPnt = new PointF((pnt1.X + pnt2.X) / 2, (pnt2.Y + pnt2.Y) / 2);  //TinEdge�е�
            PointF BaryCnt = new PointF(Convert.ToSingle(DS.Barycenters[DS.TinEdges[i].AdjTriangle1ID].X),
                Convert.ToSingle(DS.Barycenters[DS.TinEdges[i].AdjTriangle1ID].Y));  //���Բ��
            PointF EndPnt = new PointF();   //Բ�������ڴ˵㹹��VEdge

            //Բ����box����ֱ������
            if (!(BaryCnt.X >= DS.BBOX.XLeft && BaryCnt.X <= DS.BBOX.XRight &&
                BaryCnt.Y >= DS.BBOX.YTop && BaryCnt.Y <= DS.BBOX.YBottom))
                return;

            //��б��
            float k = 0;  //б��
            bool KExist = true;
            if (Math.Abs(pnt1.Y - pnt2.Y) < 0.000001)
                KExist = false;     //k������
            else
                k = (pnt1.X - pnt2.X) / (pnt2.Y - pnt1.Y);

            //��͹�Ǳ��ǡ��Ķ۽Ǳ������Բ���ڡ���
            bool obtEdge = IsObtuseEdge(i);

            #region ���ݡ�Բ����͹���ڻ���������VEdge
            //Բ���ڱ������������죬����������

            if (!obtEdge)   //Բ����͹����(��߽���)/////////////////////////////////
            {
                if (!KExist)    //k������
                {
                    // MessageBox.Show("б�ʲ����ڵġ�-"+DS.TinEdges[i].AdjTriangle1ID.ToString());
                    if (BaryCnt.Y > MidPnt.Y || BaryCnt.Y < pnt3.Y)// BaryCnt<y3 ->Բ�����е��غ�
                        EndPnt.Y = DS.BBOX.YTop;
                    else if (BaryCnt.Y < MidPnt.Y || BaryCnt.Y > pnt3.Y)
                        EndPnt.Y = DS.BBOX.YBottom;

                    EndPnt.X = BaryCnt.X;
                }
                else      //K����
                {
                    if (BaryCnt.X > MidPnt.X || (BaryCnt.X == MidPnt.X && BaryCnt.X < pnt3.X))
                        EndPnt.X = DS.BBOX.XLeft;
                    else if (BaryCnt.X < MidPnt.X || (BaryCnt.X == MidPnt.X && BaryCnt.X > pnt3.X))
                        EndPnt.X = DS.BBOX.XRight;

                    EndPnt.Y = k * (EndPnt.X - BaryCnt.X) + BaryCnt.Y;
                }

            }
            else    //Բ����͹����/////////////////////////////////////////////
            {
                if (!KExist)    //k������
                {
                    if (BaryCnt.Y < MidPnt.Y || BaryCnt.Y < pnt3.Y)
                        EndPnt.Y = DS.BBOX.YTop;
                    else if (BaryCnt.Y > MidPnt.Y || BaryCnt.Y > pnt3.Y)
                        EndPnt.Y = DS.BBOX.YBottom;

                    EndPnt.X = BaryCnt.X;
                }
                else   //K����
                {
                    if (BaryCnt.X < MidPnt.X)
                        EndPnt.X = DS.BBOX.XLeft;
                    else if (BaryCnt.X > MidPnt.X)
                        EndPnt.X = DS.BBOX.XRight;

                    EndPnt.Y = k * (EndPnt.X - BaryCnt.X) + BaryCnt.Y;
                }

            }//else �ڡ���

            //����򽻵��ڱ߽���Ĵ���
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

        //indexΪTinEdge��������,��Ϊ�۽Ǳ��򷵻�true
        /// <summary>
        ///indexΪTinEdge��������,��Ϊ�۽Ǳ��򷵻�true
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
            return (V1.X * V2.X + V1.Y * V2.Y) < 0; //a��b��ֵ<0��Ϊ�۽�
        }

        //������������Unused�����ڡ����򷵻�true
        /// <summary>
        ///���ڡ����򷵻�true
        /// </summary>
        /// <param name="PntIndex">���ID</param>
        /// <param name="index">�����ε�����ID</param>
        /// <returns>tureΪ���ڲ���falseΪ���ⲿ</returns>
        private bool PointInTriganle(long PntIndex, long index)
        {
            PointF pnt1 = new PointF(Convert.ToSingle(DS.Vertex[DS.Triangle[index].V1Index].x),
                Convert.ToSingle(DS.Vertex[DS.Triangle[index].V1Index].y));
            PointF pnt2 = new PointF(Convert.ToSingle(DS.Vertex[DS.Triangle[index].V2Index].x),
                Convert.ToSingle(DS.Vertex[DS.Triangle[index].V2Index].y));
            PointF pnt3 = new PointF(Convert.ToSingle(DS.Vertex[DS.Triangle[index].V3Index].x),
                 Convert.ToSingle(DS.Vertex[DS.Triangle[index].V3Index].y));
            PointF JudgePoint = new PointF(Convert.ToSingle(DS.Barycenters[index].X), Convert.ToSingle(DS.Barycenters[index].Y));  //���Բ��

            int IsPositive;    //�������1���������-1
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
        //��뾶
        private double dis_2(Vertex from, Vertex to)
        {
            return ((from.x - to.x) * (from.x - to.x) + (from.y - to.y) * (from.y - to.y));
        }
        //�ж��Ƿ���Բ��
       private int in_cic(int pt)
        {
            if (Math.Sqrt(dis_2(DS.maxcic, DS.Vertex[pt])) < DS.radius + DS.precison) return 1;
            return 0;
        }
        //������СԲ��
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
        //������С����Բ����
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
        /// �ж��������Ƿ��ཻ
        /// </summary>
        /// <param name="a">�߶�1�������</param>
        /// <param name="b">�߶�1�յ�����</param>
        /// <param name="c">�߶�2�������</param>
        /// <param name="d">�߶�2�յ�����</param>
        /// <param name="intersection">�ཻ������</param>
        /// <returns>�Ƿ��ཻ 0:����ƽ��  -1:��ƽ����δ�ཻ  1:�����ཻ</returns>
        private int GetIntersection(PointF a, PointF b, PointF c, PointF d, ref PointF intersection)
        {

            //�ж��쳣
            if (Math.Abs(b.X - a.Y) + Math.Abs(b.X - a.X) + Math.Abs(d.Y - c.Y) + Math.Abs(d.X - c.X) == 0)
            {
                if (c.X - a.X == 0)
                //{
                //    Debug.Print("ABCD��ͬһ���㣡");
                //}
                //else
                //{
                //    Debug.Print("AB��һ���㣬CD��һ���㣬��AC��ͬ��");
                //}
                return 0;
            }

            if (Math.Abs(b.Y - a.Y) + Math.Abs(b.X - a.X) == 0)
            {
                //if ((a.X - d.X) * (c.Y - d.Y) - (a.Y - d.Y) * (c.X - d.X) == 0)
                //{
                //    Debug.Print("A��B��һ���㣬����CD�߶��ϣ�");
                //}
                //else
                //{
                //    Debug.Print("A��B��һ���㣬�Ҳ���CD�߶��ϣ�");
                //}
                return 2;
            }
            //if (Math.Abs(d.Y - c.Y) + Math.Abs(d.X - c.X) == 0)
            //{
            //    if ((d.X - b.X) * (a.Y - b.Y) - (d.Y - b.Y) * (a.X - b.X) == 0)
            //    {
            //        Debug.Print("C��D��һ���㣬����AB�߶��ϣ�");
            //    }
            //    else
            //    {
            //        Debug.Print("C��D��һ���㣬�Ҳ���AB�߶��ϣ�");
            //    }
            //}

            if ((b.Y - a.Y) * (c.X - d.X) - (b.X - a.X) * (c.Y - d.Y) == 0)
            {
                //Debug.Print("�߶�ƽ�У��޽��㣡");
                return 0;
            }

            intersection.X = ((b.X - a.X) * (c.X - d.X) * (c.Y - a.Y) - c.X * (b.X - a.X) * (c.Y - d.Y) + a.X * (b.Y - a.Y) * (c.X - d.X)) / ((b.Y - a.Y) * (c.X - d.X) - (b.X - a.X) * (c.Y - d.Y));
            intersection.Y = ((b.Y - a.Y) * (c.Y - d.Y) * (c.X - a.X) - c.Y * (b.Y - a.Y) * (c.X - d.X) + a.Y * (b.X - a.X) * (c.Y - d.Y)) / ((b.X - a.X) * (c.Y - d.Y) - (b.Y - a.Y) * (c.X - d.X));

            if ((intersection.X - a.X) * (intersection.X - b.X) <= 0 && (intersection.X - c.X) * (intersection.X - d.X) <= 0 && (intersection.Y - a.Y) * (intersection.Y - b.Y) <= 0 && (intersection.Y - c.Y) * (intersection.Y - d.Y) <= 0)
            {
               // Debug.Print("�߶��ཻ�ڵ�(" + intersection.X + "," + intersection.Y + ")��");
                return 1; //'�ཻ
            }
            else
            {
                //Debug.Print("�߶��ཻ���齻��(" + intersection.X + "," + intersection.Y + ")��");
                return -1; //'�ཻ�������߶���
            }
        }
        /// <summary>
        /// �ж϶���ε��ཻ
        /// </summary>
        /// <param name="P">�жϵĵ�</param>
        /// <param name="poly">�����</param>
        /// <returns>�����Ƿ����ڲ�</returns>
        //public List<PointF> insertpoly(Polyon box,List<PointF> maxvoron)
        //{ 

        //}
        // ���ܣ��жϵ��Ƿ��ڶ������ 
        // ���������ͨ���õ��ˮƽ�������θ��ߵĽ��� 
        // ���ۣ����߽���Ϊ����������!
        //������ 
        /// <summary>
        /// �жϵ��Ƿ��ڶ������
        /// </summary>
        /// <param name="P">�жϵĵ�</param>
        /// <param name="poly">�����</param>
        /// <returns>�����Ƿ����ڲ�</returns>

        public static bool PtInPolygon(PointF p, List<PointF> poly)
        {
            int nCross = 0;
            //PointF[] pnts = getPolygonPoints(poly);
            for (int i = 0; i < poly.Count; i++)
            {
                PointF p1 = poly[i];
                PointF p2 =poly[(i + 1) % poly.Count];
                // ��� y=p.y �� p1p2 �Ľ���
                if (p1.Y == p2.Y) // p1p2 �� y=p0.yƽ�� 
                    continue;
                if (p.Y < Math.Min(p1.Y, p2.Y)) // ������p1p2�ӳ����� 
                    continue;
                if (p.Y >= Math.Max(p1.Y, p2.Y)) // ������p1p2�ӳ����� 
                    continue;
                // �󽻵�� X ���� -------------------------------------------------------------- 
                double x = (double)(p.Y - p1.Y) * (double)(p2.X - p1.X) / (double)(p2.Y - p1.Y) + p1.X;
                if (x >= p.X)
                    nCross++; // ֻͳ�Ƶ��߽��� 
            }
            // ���߽���Ϊż�������ڶ����֮�� --- 
            return (nCross % 2 == 1);
        }

        /// <summary>
        /// �жϵ��Ƿ��ڶ������
        /// </summary>
        /// <param name="P">�жϵĵ�</param>
        /// <param name="poly">�����</param>
        /// <returns>�����Ƿ����ڲ�</returns>

        public static bool PtInPolygon(PointF p, Collection<PointF> poly)
        {
            int nCross = 0;
            //PointF[] pnts = getPolygonPoints(poly);
            for (int i = 0; i < poly.Count; i++)
            {
                PointF p1 = poly[i];
                PointF p2 = poly[(i + 1) % poly.Count];
                // ��� y=p.y �� p1p2 �Ľ���
                if (p1.Y == p2.Y) // p1p2 �� y=p0.yƽ�� 
                    continue;
                if (p.Y < Math.Min(p1.Y, p2.Y)) // ������p1p2�ӳ����� 
                    continue;
                if (p.Y >= Math.Max(p1.Y, p2.Y)) // ������p1p2�ӳ����� 
                    continue;
                // �󽻵�� X ���� -------------------------------------------------------------- 
                double x = (double)(p.Y - p1.Y) * (double)(p2.X - p1.X) / (double)(p2.Y - p1.Y) + p1.X;
                if (x >= p.X)
                    nCross++; // ֻͳ�Ƶ��߽��� 
            }
            // ���߽���Ϊż�������ڶ����֮�� --- 
            return (nCross % 2 == 1);
        }
        /// <summary>
        /// ��ö���κ�͹��̩ɭ����εĽ���
        /// </summary>
        /// <param name="multrect">��������</param>
        /// <param name="tais">͹�ǵ�̩ɭ�����</param>
       
       public void insectpoloy(Polyon multrect,voronoiboundary tais)
       {
           int m = multrect.Vertices.Count; int n = tais.voronicollect.Count;
           //Ϊ��������εĽ�����һ������һ������εı�� ��ʼΪ�� ������������ڲ���Ϊ1
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
           //����ཻ�����ཻ����·��ŵ�λ��Ϊ1 ������·�ĵ�һ���ڵ�ı��˳��
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
           //�����ཻ���߶��ñ�Ǽ�¼
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
           //���н����յ������
           int starti = 0;
          sosocontinue:
           Sospoloy(starti, true, rectpointflag, taispointflag, insectflag, insectpoint, tais, multrect);
           //�����ཻ�Ľڵ��Ƿ���ȫ����
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
          //������õĽ������� ̩ɭ����μ�����
           tais.voronicollect.Clear();
           for (int i = 0; i < tais.vimultcollect.Count;i++ )
           {
               tais.voronicollect.Add(tais.vimultcollect[i]);
           }
           
           //for (int i = 0; i < n;i++ )
           //{
           //    taispointflag[i] = 1;    //˵�������ڲ�
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
      //������Χ�����
       /// <summary>
       ///������Χ�����
       /// </summary>
       /// <param name="starti">��ʼ�����ڵ���</param>
       /// <param name="store">����˳��</param>
       /// <param name="Sosflag">����������ε��������</param>
       /// <param name="insectflag">�ཻ������� �ཻΪ1</param>
       ///  <param name="insectpoint">�ཻ�������¼����</param>
       ///  <param name="polyon">��������Χ�����</param>
       ///  <param name="vectcollect">�����ཻ��̩ɭ�����</param>
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
                                //�ж��ཻ�ĵ��Ƿ���Ƕ˵�
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
                                    
                                    //������һѭ���ĺ��� ����Ҫ�жϴ˶���ε�˳�������ʱ�����С�����������С�����˳�� ���������������ǴӴ�С��˳��
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
                                    
                                    //������һѭ���ĺ���
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
                                    
                                    //������һѭ���ĺ���
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
                                   
                                    //������һѭ���ĺ���
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
    //����̩ɭ�����
        /// <summary>
        ///����̩ɭ�����
        /// </summary>
        /// <param name="starti">��ʼ�����ڵ���</param>
        /// <param name="store">����˳��</param>
        /// <param name="Sosflag">����������ε��������</param>
        /// <param name="insectflag">�ཻ������� �ཻΪ1</param>
        ///  <param name="insectpoint">�ཻ�������¼����</param>
        ///  <param name="polyon">��������Χ�����</param>
        ///  <param name="vectcollect">�����ཻ��̩ɭ�����</param>
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
                                    
                                    //������һ����ε�ѭ�� ����Ҫ�жϴ˶������ʲô˳��
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
                                    
                                    //������һ����ε�ѭ��
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
                                   
                                    //������һ���ѭ��
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
                                   
                                    //����Ҫѭ��������
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
        /// <returns>Returns true if ring is oriented clockwise.��˳ʱ�룩</returns>
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
        /// Tests whether a ring is oriented counter-clockwise����ʱ�룩.
        /// </summary>
        /// <param name="ring">Ring to test.</param>
        /// <returns>Returns true if ring is oriented clockwise��˳ʱ�룩.</returns>
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
        //�жϿ�Խ������߶������εĽ��� �������ȫ�����򷵻�a ��b
       /// <summary>
       /// �жϿ�Խ������߶������εĽ��� �������ȫ�����򷵻�a ��b
       /// </summary>
       /// <param name="a">�߶��׽ڵ�</param>
       /// <param name="b">�߶�ĩ�ڵ�</param>
       /// <param name="rec">��Χ�����</param>
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


