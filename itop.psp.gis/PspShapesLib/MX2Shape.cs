using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Netron.GraphLib.Attributes;
using System.Drawing;
using Netron.GraphLib;
using System.Runtime.Serialization;

namespace PspShapesLib
{
    [Serializable]
    [Description("竖母线")]
    [NetronGraphShape("竖母线", "57AF94BA-4129-45dc-0006-000000000001", "电气图元", "PspShapesLib.MX2Shape",
         "图元")]
    public class MX2Shape : BaseShape
    {
        public MX2Shape()
            : base() {
            devicetype = "01";
            //设置图元初始大小
            Rectangle = new RectangleF(0, 0, 4, 150);
            //初始连接点
            initConnector();
            //设置可改变大小
            //IsResizable = false;

            this.ShapeColor = Color.Black;
        }
        protected MX2Shape(SerializationInfo info, StreamingContext context)
            : base(info, context) {
            for (int i = 1; i < 11; i++) {
                try {
                    Connector cr = (Connector)info.GetValue("left"+i, typeof(Connector));
                    cr.BelongsTo = this;
                    Connectors.Add(cr);
                } catch { }

                try {
                    Connector cr = (Connector)info.GetValue("right" + i, typeof(Connector));
                    cr.BelongsTo = this;
                    Connectors.Add(cr);
                } catch { }
            }

            
        }

        protected override void initConnector() {
            
            for (int i = 1; i < 11; i++) {
                Connector cr = new Connector(this, "left" + i, true);
                cr.ConnectorLocation = ConnectorLocation.West;
                Connectors.Add(cr);
                cr = new Connector(this, "right" + i, true);
                cr.ConnectorLocation = ConnectorLocation.East;
                Connectors.Add(cr);
            }
        }
        /// <summary>
        /// 得到指定连接点的坐标
        /// </summary>
        /// <param name="c">连接点对象</param>
        /// <returns>浮点坐标</returns>
        public override PointF ConnectionPoint(Connector c) {
            float f1 = Math.Max(15, Rectangle.Height / 10);
           
            if (c.Name.StartsWith("left")) {
                int num = int.Parse(c.Name.Substring(4));
                PointF pf = new PointF(Rectangle.Left , Rectangle.Y+ f1 * num);
                if (pf.Y < Rectangle.Bottom) return pf;
            }
            if (c.Name.StartsWith("right")) {
                int num = int.Parse(c.Name.Substring(5));
                PointF pf = new PointF(Rectangle.Right , Rectangle.Y+ f1 * num);
                if (pf.Y < Rectangle.Bottom) return pf;
            }
            
            return new PointF(0, 0);
        }     
        public override void Paint(System.Drawing.Graphics g) {
            
            g.FillRectangle(new SolidBrush(this.ShapeColor), this.Rectangle);

        }

    }
}
