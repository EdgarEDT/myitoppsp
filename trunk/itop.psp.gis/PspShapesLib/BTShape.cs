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
    [Description("配电变台")]
    [NetronGraphShape("配电变台", "57AF94BA-4129-45dc-2002-000000000001", "电气图元", "PspShapesLib.BTShape",
         "图元")]
    public class BTShape : BaseShape
    {
        public BTShape()
            : base() {
            devicetype = "72";
            //设置图元初始大小
            Rectangle = new RectangleF(0, 0, 18, 30);
            //初始连接点
            initConnector();
            //设置可改变大小
            //IsResizable = false;
        }
        protected BTShape(SerializationInfo info, StreamingContext context)
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
            //topConnector = new Connector(this, "topconnector", true);
            //topConnector.ConnectorLocation = ConnectorLocation.North;
            //Connectors.Add(topConnector);

            //bottomConnector = new Connector(this, "bottomconnector", true);
            //bottomConnector.ConnectorLocation = ConnectorLocation.South;
            //Connectors.Add(bottomConnector);
            base.initConnector();
        }
        public override void Paint(System.Drawing.Graphics g) {
            base.Paint(g);
            System.Drawing.RectangleF r1 = new System.Drawing.RectangleF(this.Rectangle.Location, new System.Drawing.SizeF(this.Rectangle.Width, this.Rectangle.Height * 3 / 5));
            System.Drawing.RectangleF r2 = new System.Drawing.RectangleF(new PointF(this.Rectangle.X, this.Rectangle.Y + this.Height*2 / 5), new System.Drawing.SizeF(this.Width, this.Height * 3 / 5));
            
            g.DrawEllipse(this.Pen, r1);
            g.DrawEllipse(this.Pen, r2);

        }

    }
}
