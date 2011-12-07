using System;
using System.Collections.Generic;
using System.Text;
//using System.Windows.Forms;
using System.Drawing;
using System.Collections.ObjectModel;
namespace TinVoronoi
{
    //离散点

    public struct Vertex
    {
        public long x;
        public long y;
        public long ID;
       
        public int isHullEdge; //凸壳顶点标记,系统初始化为0

        //相等则返回true
        public static bool Compare(Vertex a, Vertex b)
        {
            return a.x == b.x && a.y == b.y;
        }
    }
    public class Polyon
    {
        private Collection<PointF> _Vertices;
        public Polyon( Collection<PointF>vertices)
        {
            _Vertices = vertices;
        }
        public Polyon()
        {
            _Vertices=new Collection<PointF>();
        }
         /// <summary>
        /// Initializes an instance of a LineString
        /// </summary>
        /// <param name="points"></param>
        //public Polyon(List<long[]> points)
        //{
        //    Collection<Vertex> vertices = new Collection<Vertex>();
        //    long i = 0;
        //    foreach (long[] point in points)
        //    {
                
        //        Vertex vx=new Vertex(point[0],point[1],i);
        //        //vx.x = point[0];
        //        //vx.y = point[1];
        //        vertices.Add(vx);
        //        i++;
        //    }

        //    _Vertices = vertices;
        //}		

		/// <summary>
		/// Gets or sets the collection of vertices in this Geometry
		/// </summary>
		public virtual Collection<PointF> Vertices
		{
			get { return _Vertices; }
			set { _Vertices = value; }
		}
        /// <summary>
        /// Returns the specified point N in this Linestring.
        /// </summary>
        /// <remarks>This method is supplied as part of the OpenGIS Simple Features Specification</remarks>
        /// <param name="N"></param>
        /// <returns></returns>
        public PointF Point(int N)
        {
            return _Vertices[N];
        }
         /// <summary>
        /// 添加节点.
        /// </summary>
        /// <param name="V"> 节点名称</param>
        /// <returns></returns>
        public void add(PointF v)
        {
            bool flag=false;
          foreach (PointF vx in _Vertices)
          {
              if (vx.X==v.X&&vx.Y==v.Y)
              {
                  flag=true;
                  break;
              }
          }
            if (!flag)
            {
                _Vertices.Add(v);
            }
        }
        /// <summary>
        /// Returns the vertice where this Geometry begins
        /// </summary>
        /// <returns>First vertice in LineString</returns>
        public  PointF StartPoint
        {
            get
            {
                if (_Vertices.Count == 0)
                    throw new ApplicationException("No startpoint found: LineString has no vertices.");
                return this._Vertices[0];
            }
        }

        /// <summary>
        /// Gets the vertice where this Geometry ends
        /// </summary>
        /// <returns>Last vertice in LineString</returns>
        public  PointF EndPoint
        {
            get
            {
                if (_Vertices.Count == 0)
                    throw new ApplicationException("No endpoint found: LineString has no vertices.");
                return _Vertices[_Vertices.Count - 1];
            }
        }
        /// <summary>
        /// The minimum bounding box for this Geometry.
        /// </summary>
        /// <returns>BoundingBox for this geometry</returns>
        public BoundaryBox GetBoundingBox()
        {
            //if (this.Vertices == null || this.Vertices.Count == 0)
            //    return null;
            BoundaryBox bbox;
            bbox.XLeft =Convert.ToInt64(this.Vertices[0].X) ; bbox.XRight = Convert.ToInt64(this.Vertices[0].X); bbox.YBottom =Convert.ToInt64(this.Vertices[0].Y) ; bbox.YTop = Convert.ToInt64(this.Vertices[0].Y);
            for (int i = 1; i < this.Vertices.Count; i++)
            {
                bbox.XLeft = Convert.ToInt64(this.Vertices[i].X) < bbox.XLeft ? Convert.ToInt64(this.Vertices[i].X) : bbox.XLeft;
                bbox.YTop = Convert.ToInt64(this.Vertices[i].Y) < bbox.YTop ? Convert.ToInt64(this.Vertices[i].Y) : bbox.YTop;
                bbox.XRight = Convert.ToInt64(this.Vertices[i].X) > bbox.XRight ? Convert.ToInt64(this.Vertices[i].X) : bbox.XRight;
                bbox.YBottom = Convert.ToInt64(this.Vertices[i].Y) > bbox.YBottom ? Convert.ToInt64(this.Vertices[i].Y) : bbox.YBottom;
            }
            return bbox;
        }
    }
    //边

    public struct Edge
    {
        public long Vertex1ID;   //点索引

        public long Vertex2ID;
        public Boolean NotHullEdge;  //非凸壳边
        public long AdjTriangle1ID;
        public long AdjacentT1V3;    //△1的第三顶点在顶点数组的索引

        public long AdjTriangle2ID;

        public Edge(long iV1, long iV2)
        {
            Vertex1ID = iV1;
            Vertex2ID = iV2;
            NotHullEdge = false;
            AdjTriangle1ID = 0;
            AdjTriangle2ID = 0;
            AdjacentT1V3 = 0;
        }

        //相等则返回true
        public static bool Compare(Edge a, Edge b)
        {
            return ((a.Vertex1ID == b.Vertex1ID) && (a.Vertex2ID == b.Vertex2ID)) ||
                ((a.Vertex1ID == b.Vertex2ID) && (a.Vertex2ID == b.Vertex1ID));
        }

    }

    //三角形

    public struct Triangle
    {
        public long V1Index; //点在链表中的索引值

        public long V2Index;
        public long V3Index;
    }

    //外接圆心
    public struct Barycenter
    {
        public double X;
        public double Y;
    }

    public struct VoronoiEdge
    {
        public Edge VEdge;
    }

    public struct BoundaryBox
    {
        public long XLeft;
        public long YTop;
        public long XRight;
        public long YBottom;
    }

    public class DataStruct
    {

        public static int MaxVertices = 500;
        public static int MaxEdges = 2000;
        public static int MaxTriangles = 1000;
        public Vertex[] Vertex = new Vertex[MaxVertices];
        public Triangle[] Triangle = new Triangle[MaxTriangles];
        public Barycenter[] Barycenters = new Barycenter[MaxTriangles]; //外接圆心
        public Edge[] TinEdges = new Edge[MaxEdges];
        public voronoiboundary[] vetexboundary = new voronoiboundary[MaxVertices];//节点的泰森多边形影响区域
        public DataStruct()
        {
            for (int i = 0; i < MaxVertices; i++)
            {
                vetexboundary[i] = new voronoiboundary();
            }
        }
        public BoundaryBox BBOX = new BoundaryBox();  //图副边界框

        public int VerticesNum = 0;
        public int TinEdgeNum = 0;
        public int TriangleNum = 0;
        public double radius = 0;                //最小覆盖圆的半径

        public double precison = 1.0e-8;          //精度
        public Vertex maxcic;
        public int[] curset = new int[MaxVertices], posset = new int[3];
        public int set_cnt, pos_cnt;
        public Polyon multrect = new Polyon();        //多边形边界；
        public bool drawpolyonflag = false;           //是否画多边形
    }
    public class voronoiboundary
    {
        public long pointid;
        public bool hullflag = false;        //默认为否凸壳的泰森多边形
        public  List<PointF> voronicollect;  //形成voroni边界包络多边形的拐点集合
        public  List<PointF> insertboundarycollect;  //记录与外围多边形边界的交点

        public  List<PointF> vimultcollect;  //真正边界相交点的集合
       
        public voronoiboundary()
        {
            voronicollect = new List<PointF>();
            insertboundarycollect = new List<PointF>();
            vimultcollect = new List<PointF>();
        }
        public bool pointcompare(PointF f)             //比较点集合里面有没有重复的点
        {
            bool flag = true;
            foreach (PointF p in voronicollect)
            {
                if (p.X == f.X && p.Y == f.Y)
                {
                    flag = false;
                }

            }
            return flag;
        }

        public bool Vimultptcompare(PointF f)             //比较与多边形边界相交的点集合里面有没有重复的点

        {
            bool flag = true;
            foreach (PointF p in vimultcollect)
            {
                if (p.X == f.X && p.Y == f.Y)
                {
                    flag = false;
                }

            }
            return flag;
        }

        public  void clear()          //清理数据重新进行添加
        {
           voronicollect.Clear();
           insertboundarycollect.Clear();
           vimultcollect.Clear();
        }
    }

}
