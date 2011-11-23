using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Netron.GraphLib.Attributes;
using System.Drawing;
using Netron.GraphLib;
using System.Runtime.Serialization;
using System.Drawing.Drawing2D;

namespace PspShapesLib
{
    [Serializable]
    [Description("三圈变电压")]
    [NetronGraphShape("三圈变电压", "57AF94BA-4129-45dc-0004-000000000001", "电气图元", "PspShapesLib.BYQ3Shape",
         "图元")]
    public class BYQ3Shape : BaseShape
    {
        public BYQ3Shape()
            : base() {
            devicetype = "03";
            //设置图元初始大小
            Rectangle = new RectangleF(0, 0, 30, 30);
            //初始连接点
            initConnector();
            //设置可改变大小
            //IsResizable = false;
        }
        protected BYQ3Shape(SerializationInfo info, StreamingContext context)
            : base(info, context) {
            rightConnector = (Connector)info.GetValue("rightconnector", typeof(Connector));
            topConnector = (Connector)info.GetValue("topconnector", typeof(Connector));
            bottomConnector = (Connector)info.GetValue("bottomconnector", typeof(Connector));
            leftConnector = (Connector)info.GetValue("leftconnector", typeof(Connector));

            leftConnector.BelongsTo = this;
            rightConnector.BelongsTo = this;
            topConnector.BelongsTo = this;
            bottomConnector.BelongsTo = this;

            Connectors.Add(leftConnector);
            Connectors.Add(rightConnector);
            Connectors.Add(topConnector);
            Connectors.Add(bottomConnector);
        }
        protected override void initConnector() {
            base.initConnector();
            //topConnector = new Connector(this, "topconnector", true);
            //topConnector.ConnectorLocation = ConnectorLocation.North;
            //Connectors.Add(topConnector);
            //rightConnector = new Connector(this, "rightconnector", true);
            //rightConnector.ConnectorLocation = ConnectorLocation.East;
            //Connectors.Add(rightConnector);
            //bottomConnector = new Connector(this, "bottomconnector", true);
            //bottomConnector.ConnectorLocation = ConnectorLocation.South;
            //Connectors.Add(bottomConnector);
        }
        /// <summary>
        /// 得到指定连接点的坐标
        /// </summary>
        /// <param name="c">连接点对象</param>
        /// <returns>浮点坐标</returns>
        public override PointF ConnectionPoint(Connector c) {
            PointF pf = PointF.Empty;
            if (c == leftConnector) pf=new PointF(Rectangle.Left, Rectangle.Top + (Rectangle.Height * 1 / 2));
            if (c == rightConnector) pf=new PointF(Rectangle.Right, Rectangle.Top + Rectangle.Height / 2);
            if (c == topConnector) pf = new PointF(Rectangle.Left + Rectangle.Width / 3, Rectangle.Top);
            if (c == bottomConnector) pf = new PointF(Rectangle.Left + Rectangle.Width / 3, Rectangle.Bottom);
            //Matrix mx = GetTransForm();
            //PointF[] pfs = new PointF[] { pf };
            //mx.TransformPoints(pfs);
            //pf = pfs[0];
            return pf;
        }  
        
        public override void Paint(System.Drawing.Graphics g) {
            base.Paint(g);
            float f1 = Width * 2 / 3;
            System.Drawing.RectangleF r1 = new System.Drawing.RectangleF(this.Rectangle.Location, new System.Drawing.SizeF(f1, this.Rectangle.Height * 3 / 5));
            System.Drawing.RectangleF r2 = new System.Drawing.RectangleF(new PointF(this.Rectangle.X, this.Rectangle.Y + this.Height * 2 / 5), new System.Drawing.SizeF(f1, this.Height * 3 / 5));
            System.Drawing.RectangleF r3= new System.Drawing.RectangleF(new PointF(this.Rectangle.X+this.Width/3, this.Rectangle.Y + this.Height * 1 / 5), new System.Drawing.SizeF(f1, this.Height * 3 / 5));
            GraphicsContainer gc = g.BeginContainer(Rectangle,Rectangle,GraphicsUnit.Pixel);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.Transform = GetTransForm();
            g.DrawEllipse(this.Pen, r1);
            g.DrawEllipse(this.Pen, r2);
            g.DrawEllipse(this.Pen, r3);
            g.EndContainer(gc);
        }
        
    }
}
