using System;
using System.Collections.Generic;
using System.Text;
using Netron.GraphLib.Interfaces;
using Netron.GraphLib;
using System.Drawing;
using PspShapesLib;

namespace Itop.TLPSP.DEVICE
{
    /// <summary>
    /// 接线图布局类
    /// </summary>
    public class AutojxtLayout : GraphLayout
    {
        #region 变量
        private Random rnd = new Random();
        Dictionary<string, BaseShape> mxDic = new Dictionary<string, BaseShape>();
        Dictionary<string, BaseShape> shapeDic = new Dictionary<string, BaseShape>();
        int maxWidth = 3000;
        #endregion

        #region 属性


        #endregion 

        #region 初始化
        public AutojxtLayout(IGraphSite site)
            : base(site) {
            this.mSite = site;
        }
        #endregion

        #region 方法

        /// <summary>
        /// 开始布局
        /// 
        /// </summary>
        /// 
        float curWidth = 0;
        float curHeight = 0;
        public override void StartLayout() {
            if (nodes.Count == 0) return;
            foreach (Shape shape in this.nodes) {
                if (shape is BaseShape) {
                    BaseShape bshape = shape as BaseShape;
                    if (bshape.DeviceType == "01") {
                        mxDic.Add(bshape.DeviceID, bshape);
                    } else {
                        shapeDic.Add(bshape.DeviceID, bshape);
                    }
                }
            }
            if (mxDic.Count == 0) return;
            curWidth = 0;
            curHeight = 0;
            float mWidth = maxWidth;
            BaseShape shapemx = null;
            BaseShape oldshape = null;
            foreach (BaseShape shape1 in mxDic.Values) {
                shapemx = shape1;

                layershapes.Add(shapemx);
                if (oldshape == null) {
                    curWidth = 10;
                    curHeight = 10;
                } else {
                    
                    shapemx.X = curWidth+20;
                }
                oldshape = shapemx;
                layeroutmx(shapemx);
            }
                layershapes.Clear();
            int ii = 0;


        }
        public void StartLayout2() {
            if (nodes.Count == 0) return;
            foreach (Shape shape in this.nodes) {
                if (shape is BaseShape) {
                    BaseShape bshape = shape as BaseShape;
                    if (bshape.DeviceType == "01") {
                        mxDic.Add(bshape.DeviceID, bshape);
                    } else {
                        shapeDic.Add(bshape.DeviceID, bshape);
                    }
                }
            }
            if (mxDic.Count == 0) return;
           curWidth = 0;
            curHeight = 0;
            float mWidth = maxWidth;
            BaseShape shapemx = null;
            foreach (BaseShape shape1 in mxDic.Values) {
                shapemx = shape1;
                break;
            }
            layershapes.Add(shapemx);
            curWidth = shapemx.X;
            curHeight = shapemx.Y;
            layeroutmx(shapemx);
            layershapes.Clear();
            int ii = 0;

            //PointF[] e = new PointF[this.nodes.Count];
            //PointF[] s = new PointF[this.nodes.Count];
            //Point p;

            //int steps = 50;

            //for (int k = 0; k < this.nodes.Count; k++) {
            //    p = new Point(rnd.Next(10, (int)(mSite.Width+2000 - nodes[k].Width - 10)), rnd.Next(10, (int)(mSite.Height+1000 - nodes[k].Height - 10)));
            //    e[k] = p;
            //    s[k] = nodes[k].Rectangle.Location;
            //}


            //for (int j = 1; j < steps + 1; j++) {
            //    for (int k = 0; k < nodes.Count; k++) {
            //        nodes[k].X = s[k].X + j * (e[k].X - s[k].X) / steps;
            //        nodes[k].Y = s[k].Y + j * (e[k].Y - s[k].Y) / steps;
            //    }
            //    mSite.Invalidate();
            //}

        }

        ShapeCollection layershapes = new ShapeCollection();
        ShapeCollection movedshapes = new ShapeCollection();
        /// <summary>
        /// 布局线路　
        /// </summary>
        /// <param name="pshape"></param>
        void layeroutmx(BaseShape pshape) {
            
            ShapeCollection childshapes = pshape.GetChildShapes();

            if (pshape is MX1Shape) {

                MX1Shape mx1= pshape as MX1Shape;
                mx1.Width = Math.Max(mx1.Width, 40 * childshapes.Count);
                mx1.ConnectorNumber = childshapes.Count;
            }
            foreach (Connector cr in pshape.Connectors) {
                if (cr.ConnectorLocation == ConnectorLocation.South) {
                    foreach (Connection cn in cr.Connections) {
                        Connector crto = cn.From;
                        if (cn.From == cr) {
                            crto = cn.To;
                        }
                        if (!movedshapes.Contains(crto.BelongsTo)) {
                            movedshapes.Add(crto.BelongsTo);
                            PointF pf1 = crto.BelongsTo.ConnectionPoint(crto);
                            PointF pf2 = cr.BelongsTo.ConnectionPoint(cr);
                            float offx = pf2.X - pf1.X;
                            float offy = 50;
                            crto.BelongsTo.X += offx;
                            crto.BelongsTo.Y += offy;
                        }

                    }
                }
            }
            float xlright = 0;//线路右边范围
            int xlnum = -1;//有设备的线路
            foreach (BaseShape sp1 in childshapes)
            {
                if (!layershapes.Contains(sp1))
                {
                    layershapes.Add(sp1);
                    {

                        if (sp1.GetChildShapes().Count > 1) 
                        {
                            if (xlright > sp1.X) {
                                sp1.X = xlright + 30;
                            }
                            if (sp1.DeviceType != "01") {
                                sp1.Y -= xlnum * 3; xlnum++;
                            }
                            layeroutsb(sp1, ref xlright);
                        }
                    }
                }
            }
            curWidth = Math.Max(xlright,pshape.X+pshape.Width);
        }
        /// <summary>
        /// 判断是否偶数
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public bool IsEven(int a)
        {
            int  s  =   1 ;       
            return  ((a  &  s)  ==   0 );  
        }
        /// <summary>
        /// 布局子设备
        /// </summary>
        /// <param name="pshape"></param>
        /// <param name="xlright"></param>
        void layeroutsb(BaseShape pshape, ref float xlright) {            
            ShapeCollection childshapes = pshape.GetChildShapes();           
            float offx = pshape.X;
            int wnum = 0;
            int hnum = 0;
            for (int i = 0; i < childshapes.Count;i++ )
            {
                BaseShape sp1 = childshapes[i] as BaseShape;
                 if (!layershapes.Contains(sp1)) {
                    layershapes.Add(sp1);
                    if (sp1.DeviceType == "01") {
                        layeroutmx(sp1);
                    }
                    else {
                        if (IsEven(sp1.Grade))
                        {
                            sp1.X =pshape.X + sp1.Width + 50;
                            sp1.Y = pshape.Y + (pshape.Height - sp1.Height) / 2;
                            sp1.Y+=30*wnum;
                            wnum++;
                            xlright = Math.Max(sp1.X + sp1.Width, xlright);
                            layeroutsb(sp1, ref xlright);
                        }
                        else
                        {
                            sp1.X = pshape.X + (pshape.Width - sp1.Width) / 2;
                            sp1.X+=30 * hnum;
                            hnum++;
                            xlright = Math.Max(sp1.X + sp1.Width, xlright);
                            sp1.Y = pshape.Y + pshape.Height + 30;
                            layeroutsb(sp1, ref xlright);
                        } 
                    }
                }      
            }
           
        }
        #endregion
    }
}
